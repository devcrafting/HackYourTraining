module app

open System
open System.Linq
open Suave
open Suave.Web
open Suave.Http
open Suave.Successful
open Suave.Redirection
open Suave.Files
open Suave.Filters
open Suave.Operators
open Suave.RequestErrors

open Newtonsoft.Json

open Microsoft.FSharp.Reflection
open Newtonsoft.Json.Converters

type OptionConverter() =
    inherit JsonConverter()
    
    override x.CanConvert(t) = 
        t.IsGenericType && t.GetGenericTypeDefinition() = typedefof<option<_>>

    override x.WriteJson(writer, value, serializer) =
        let value = 
            if value = null then null
            else 
                let _,fields = FSharpValue.GetUnionFields(value, value.GetType())
                fields.[0]  
        serializer.Serialize(writer, value)

    override x.ReadJson(reader, t, existingValue, serializer) =        
        let innerType = t.GetGenericArguments().[0]
        let innerType = 
            if innerType.IsValueType then (typedefof<Nullable<_>>).MakeGenericType([|innerType|])
            else innerType        
        let value = serializer.Deserialize(reader, innerType)
        let cases = FSharpType.GetUnionCases(t)
        if value = null then FSharpValue.MakeUnion(cases.[0], [||])
        else FSharpValue.MakeUnion(cases.[1], [|value|])

let JSON v =
    let settings = new JsonSerializerSettings()
    settings.Converters.Add(new OptionConverter())
    JsonConvert.SerializeObject(v, settings)
    |> OK
    >=> Writers.setMimeType "application/json;charset=utf-8"

open Domain.TrainingRequest
open Domain.TrainingRequest.Repositories

let trainingRequests = { 
    getFromFriendlyUrl = Infra.TrainingRequests.getFromFriendlyName
}

let app nodeModulesDir : WebPart =
    choose 
        [
            Filters.GET >=> choose 
                [ Filters.path "/" >=> Files.browseFileHome "index.html"
                  pathScan "/node_modules/%s" (fun path -> Files.browseFile nodeModulesDir (sprintf "%s" path) )
                  pathScan "/trainingRequest/%s/index" (fun friendlyUrl -> trainingRequests.getFromFriendlyUrl friendlyUrl |> JSON)
                  pathScan "/%s/%s/%s/%s" (fun (model, friendlyUrl, page, pageFriendlyUrl) -> Files.browseFileHome (sprintf "%s.html" page) ) 
                  pathScan "/%s/%s" (fun (model, friendlyUrl) -> Files.browseFileHome (sprintf "%s.html" model) ) 
                  // OBSOLETE routes to be replaced
                  pathScan "/%s" (fun path -> Files.browseFileHome (sprintf "%s/index.html" path)) // speaker answer
                  path "/interestedTrainees" >=> request (fun req -> trainingRequests.getFromFriendlyUrl "" |> JSON) //old data
                  Files.browseHome ]
            RequestErrors.NOT_FOUND "Page not found." 
        ]