#r "../../node_modules/fable-core/Fable.Core.dll"
#load "../../node_modules/fable-import-virtualdom/Fable.Helpers.Virtualdom.fs"
#load "about.fsx"
#load "../domain/trainingProposal.fs"

open Fable.Core 
open Fable.Import
open Fable.Import.Browser

open Fable.Helpers.Virtualdom
open Fable.Helpers.Virtualdom.App
open Fable.Helpers.Virtualdom.Html

open Domain.TrainingProposal

open System

let trainerPhoto (trainer:String) = (trainer.ToLower()).Replace(" ", "_") + ".jpg"
let trainingTitle model = sprintf " %s (%s - %s)" model.Trainer.Name model.Location model.Month
let twitterLink person = 
    match person.TwitterAccount with
    | Some account -> sprintf "https://www.twitter.com/%s" account
    | None -> String.Empty 

// VIEW
let proposal model =
    [div [ attribute "class" "row"] [
        h2 [ attribute "class" "hyt-training-detail-title"] [ 
            img [ trainerPhoto model.Trainer.Name |> attribute "src"; attribute "class" "hyt-training-detail-picture"]
            trainingTitle model |> text]
        p [ attribute "class" "hyt-content"] [
            a [ twitterLink model.ProposedBy |> attribute "href" ] [ text model.ProposedBy.Name]
            text " propose a "
            a [ twitterLink model.Trainer |> attribute "href" ] [ text model.Trainer.Name ]
            text " training on CQRS/Event Sourcing in "
            a [ attribute "href" "https://www.google.fr/maps/place/Lyon"] [ text model.Location]
            sprintf " in %s %s." model.Month model.Year |> text ]
        p [ attribute "class" "hyt-content"] [
            text "Greg Young coined the term CQRS (Command Query Responsibility Segregation) and it was instantly picked up by the community who have elaborated upon it ever since."
            br []
            text "Greg is an independent consultant and serial entrepreneur. He has 10+ years of varied experience in computer science from embedded operating systems to business
                systems and he brings a pragmatic and often times unusual viewpoint to discussions."
            br []
            text "He's a frequent contributor to "
            a [ attribute "href" "https://www.infoq.com/"] [ text "InfoQ"]
            text ", speaker/trainer at "
            a [ attribute "href" "https://skillsmatter.com/"] [ text "Skills Matter"]
            text " and also a well-known speaker at international conferences. "
            text "Greg also writes about CQRS, DDD and other hot topics on "
            a [ attribute "href" "www.codebetter.com"] [ text "www.codebetter.com."]]
        a [ attribute "href" "https://goo.gl/forms/rYYfFJTtT00Q35Sg1"; attribute "class" "btn btn-success hyt-proposal-registerButton"] [ text "I am interested"]
        p [ attribute "class" "hyt-content"] (
            (text (sprintf "Currently interested people (%d/15): " (List.length model.InterestedTrainees)))
            :: (model.InterestedTrainees |> List.map (fun trainee -> 
                span [ attribute "class" "hyt-proposal-people"] [
                    a [ twitterLink trainee |> attribute "href" ] [ text trainee.Name ]])))]]

open About

let homeView model =
    div [] 
        ([proposal model;About.about()] |> List.concat)

// HTTP
let load update =
    let getInterestedTrainees = XMLHttpRequest.Create()
    getInterestedTrainees.onreadystatechange <- fun _ ->
        if getInterestedTrainees.readyState = 4. then
            match getInterestedTrainees.status with
            | 200. | 0. ->
                JS.JSON.parse getInterestedTrainees.responseText
                |> unbox |> TrainingProposalsLoaded |> update
            | _ -> ()
        null
    getInterestedTrainees.``open``("GET", "/interestedTrainees", true)
    getInterestedTrainees.setRequestHeader("Content-Type", "application/json")
    getInterestedTrainees.send(None)

// Start
let initialModel = 
    { ProposedBy = { Name = "Emilien Pecoul"; TwitterAccount = None }
      Trainer = { Name = "Greg Young"; TwitterAccount = None }
      Location = "Lyon"
      Month = "September"
      Year = "2016"
      InterestedTrainees = [] }

let homeApp =
    createApp { Model = initialModel; View = homeView; Update = homeUpdate }
    |> withInit load
    |> withStartNode "#container"

homeApp |> start renderer
