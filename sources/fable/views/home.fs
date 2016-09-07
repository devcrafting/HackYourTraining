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
     hr []
     p [ attribute "class" "hyt-content"] [
        text "You did not find training you would like to assist, but you have a subject you would like to be trained, but you have to go elsewhere to get it, or wait too long, or pay way too much..."
        br []
        text "Good news ! You can make a training request on HackYourTraining to get a chance to gather enough people interested in the same subject, around your location and approximately when you want! It takes few minutes to fill in the form, there are NO commitment, just cool things that could happen!"]
     p [ attribute "class" "hyt-centered-button"] [
        a [ attribute "href" "https://goo.gl/forms/WvfYhziP1qcLMWIn2"; attribute "class" "btn btn-success"] [ text "Make a training request" ]]]

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
