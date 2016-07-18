#r "../node_modules/fable-core/Fable.Core.dll"
#load "../node_modules/fable-import-virtualdom/Fable.Helpers.Virtualdom.fs"
#load "./views/about.fsx"
#load "./domain/home.fs"

open Fable.Core 
open Fable.Import
open Fable.Import.Browser

open Fable.Helpers.Virtualdom
open Fable.Helpers.Virtualdom.App
open Fable.Helpers.Virtualdom.Html

open Domain.Home

// VIEW
let home = 
    [div [ attribute "class" "row hyt-home" ] [
        div [ attribute "class" "hyt-home-content"] [ 
            h1 [] [text "Hack Your Training"]
            h2 [] [text "Training by the community and for the community"]]];
     div [ attribute "class" "row"] [
        p [ attribute "class" "hyt-content"] [
            text "We would like training to be popularized and pulled by the community to raise the bar
                (instead of pushed corporate/\"one size fit all\" training)."
            br [];br []
            text "Then, the goal is that anyone could propose a training somewhere with a subject and a \"trainer of his dream\"."
            br [];br []
            text "You can show your interest in a training, given you would be able to assist on some conditions? Note there is
                absolutely NO commitment."]]]

let proposals model =
    [div [ attribute "class" "row"] [
        h1 [attribute "class" "hyt-title"] [text "Last training proposals"]]
     div [ attribute "class" "row"] [
        img [ attribute "src" "greg_young.jpg"; attribute "class" "hyt-proposal-picture"]
        h2 [ attribute "class" "hyt-proposal-title"] [ text "Greg Young (Lyon - September)"]
        a [ attribute "href" "https://goo.gl/forms/rYYfFJTtT00Q35Sg1"; attribute "class" "btn btn-success hyt-proposal-registerButton"] [ text "I'am interested"]
        p [ attribute "class" "hyt-content"] [
            a [ attribute "href" "https://twitter.com/Ouarzy"] [ text "Emilien Pecoul"]
            text " propose a "
            a [ attribute "href" "https://twitter.com/gregyoung"] [ text "Greg Young"]
            text " training on CQRS/Event Sourcing in "
            a [ attribute "href" "https://www.google.fr/maps/place/Lyon"] [ text "Lyon"]
            text " in September 2016"]
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
        p [ attribute "class" "hyt-content"] (
            (text (sprintf "Currently interested people (%d/15): " (List.length model.InterestedTrainees)))
            :: (model.InterestedTrainees |> List.map (fun trainee -> 
                span [ attribute "class" "hyt-proposal-people"] [
                    a [ attribute "href" trainee.TwitterUrl] [ text trainee.Name ]])))]]

open About

let homeView model =
    div [] 
        ([home;proposals model;About.about()] |> List.concat)

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
    { InterestedTrainees = [] }

let homeApp =
    createApp { Model = initialModel; View = homeView; Update = homeUpdate }
    |> withInit load
    |> withStartNode "#container"

homeApp |> start renderer
