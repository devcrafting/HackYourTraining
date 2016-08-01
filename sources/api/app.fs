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

open FSharp.Data.TypeProviders
open FSharp.Linq

let interestedTrainees = 
    ["Emilien Pecoul", "https://twitter.com/Ouarzy";
     "Florent Pellet", "https://twitter.com/florentpellet";
     "Clément Bouillier", "https://twitter.com/clem_bouillier";
     "Jean Helou", "https://twitter.com/jeanhelou"
     "Yannick Ringapin", "https://twitter.com/BlackBeard486";
     "Kevin Lejeune", "https://twitter.com/kevin_le_jeune";
     "Karol Chmist", "https://twitter.com/karolchmist";
     "Gregory Cica", "";
     "François Miton", "";
     "Bertrand Saintes", "";
     "Samuel Pecoul", "https://twitter.com/SamPecoul";
     "Nadège Rouelle", "https://twitter.com/nadegerouelle";
     "Romain Berthon", "https://twitter.com/RomainTrm";
     "Benjamin Garel", "https://twitter.com/bgarel";
     "Romain Dequidt", "https://twitter.com/romain_dequidt";
     "Lucas Courot", "https://twitter.com/lucas_courot";
     "Jean Detoeuf", "https://twitter.com/thebignet";
     "Agnès Crepet", "https://twitter.com/agnes_crepet";
     "Mathieu Petitdant", "https://twitter.com/mpetitdant"
     ]

let getInterestedTrainees =
    interestedTrainees
    |> List.map (fun trainee -> sprintf "{ \"Name\": \"%s\", \"TwitterUrl\": \"%s\" }" (fst trainee) (snd trainee))
    |> String.concat ","
    |> sprintf "{ \"InterestedTrainees\": [ %s ],  \"ProposedBy\": { \"Name\":\"Emilien Pecoul\", \"TwitterAccount\":\"ouarzy\" }, \"Trainer\": { \"Name\":\"Greg Young\", \"TwitterAccount\":\"gregyoung\" }, \"Location\": \"Lyon\", \"Month\": \"September\", \"Year\": \"2016\" }" 

let app nodeModulesDir : WebPart =
    choose 
        [ 
            Filters.GET >=> choose 
                [ Filters.path "/" >=> Files.browseFileHome "index.html"
                  pathScan "/node_modules/%s" (fun path -> Files.browseFile nodeModulesDir (sprintf "%s" path) )
                  pathScan "/%s" (fun path -> Files.browseFileHome (sprintf "%s/index.html" path))
                  Files.browseHome ] 
            GET >=> choose
                [ path "/interestedTrainees" >=> request (fun req -> OK getInterestedTrainees)]   
            RequestErrors.NOT_FOUND "Page not found." 
        ]