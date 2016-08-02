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

let JSON v =
    let settings = new JsonSerializerSettings()
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
                  pathScan "/%s/%s/%s" (fun (model, friendlyUrl, page) -> Files.browseFileHome (sprintf "%s.html" model) ) 
                  pathScan "/%s/%s" (fun (model, friendlyUrl) -> Files.browseFileHome (sprintf "%s.html" model) ) 
                  // OBSOLETE routes to be replaced
                  pathScan "/%s" (fun path -> Files.browseFileHome (sprintf "%s/index.html" path))
                  path "/interestedTrainees" >=> request (fun req -> trainingRequests.getFromFriendlyUrl "" |> JSON)
                  Files.browseHome ]
            RequestErrors.NOT_FOUND "Page not found." 
        ]