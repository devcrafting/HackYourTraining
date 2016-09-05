module Views.Home

open Fable.Core 
open Fable.Import
open Fable.Import.Browser

open Fable.Helpers.Virtualdom
open Fable.Helpers.Virtualdom.App
open Fable.Helpers.Virtualdom.Html

open Domain.Home

// VIEW
let trainerPhoto (trainer:Person) = 
    match trainer.TwitterAccount with
    | None -> "/images/unknown.jpg"
    | Some account -> sprintf "https://twitter.com/%s/profile_image?size=original" account
let trainingTitle model = sprintf "%s with %s (%s - %s %s)" model.Subject model.Trainer.Name model.Location model.Month model.Year

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
            text "Then, the goal is that anyone could request a training somewhere with a subject and a \"trainer of his dream\" 
                (if you know one else you can request to receive several proposals)."
            br [];br []
            text "You can show your interest in a training, given you would be able to assist on some conditions? Note there is
                absolutely NO commitment."]]]

let proposals model =
    [div [ attribute "class" "row"] [
        h1 [attribute "class" "hyt-title"] [text "Last training requests & proposals"]]
     div [ attribute "class" "row"] [
        div [ attribute "class" "col-md-1 col-xs-4" ] [
            div [ attribute "class" "hyt-training-detail-picture"] [
                img [ trainerPhoto model.Trainer |> attribute "src" ]]]
        div [ attribute "class" "col-md-11 col-xs-8"] [
            h2 [ attribute "class" "hyt-training-detail-title"] [
                a [ attribute "href" "/trainingRequest/2016-09-Lyon-GregYoung-CQRS-ES" ] [ trainingTitle model |> text ]]
            div [ attribute "class" "pull-right" ] [
                span [ attribute "class" "label label-warning" ] [ text "Requested trainer did not make a proposal" ]
                span [ attribute "class" "label label-success" ] [ text "1 alternative proposal" ]]]]
     hr []]

open About

let homeView model =
    div [] 
        ([home;proposals model;About.about()] |> List.concat)

// HTTP
// let load update =
//     let getInterestedTrainees = XMLHttpRequest.Create()
//     getInterestedTrainees.onreadystatechange <- fun _ ->
//         if getInterestedTrainees.readyState = 4. then
//             match getInterestedTrainees.status with
//             | 200. | 0. ->
//                 JS.JSON.parse getInterestedTrainees.responseText
//                 |> unbox |> TrainingProposalsLoaded |> update
//             | _ -> ()
//         null
//     getInterestedTrainees.``open``("GET", "/interestedTrainees", true)
//     getInterestedTrainees.setRequestHeader("Content-Type", "application/json")
//     getInterestedTrainees.send(None)

// Start
let initialModel = 
    { 
        ProposedBy = { Name = "Emilien Pecoul"; TwitterAccount = Some "ouarzy" }
        Trainer = { Name = "Greg Young"; TwitterAccount = Some "gregyoung" }
        Subject = "CQRS/Event Sourcing"
        Location = "Lyon"
        Month = "September"
        Year = "2016"
        PendingTrainerProposal = false
        NbProposals = 1
    }

let homeApp =
    createApp { Model = initialModel; View = homeView; Update = homeUpdate }
    //|> withInit load
    |> withStartNode "#container"

homeApp |> start renderer
