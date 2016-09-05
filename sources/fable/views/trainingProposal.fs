module Views.TrainingProposal

open Fable.Core 
open Fable.Import
open Fable.Import.Browser

open Fable.Helpers.Virtualdom
open Fable.Helpers.Virtualdom.App
open Fable.Helpers.Virtualdom.Html

open Domain.TrainingProposal

open System

let trainerPhoto (trainer:Person) = 
    match trainer.TwitterAccount with
    | None -> "/images/unknown.jpg"
    | Some account -> sprintf "https://twitter.com/%s/profile_image?size=original" account
let trainingTitle model = sprintf "%s" model.Subject
let twitterLink person = 
    match person.TwitterAccount with
    | Some account -> sprintf "https://www.twitter.com/%s" account
    | None -> String.Empty 
let listPerson persons =
    persons
    |> List.map (fun trainer -> 
        span [ attribute "class" "hyt-proposal-people"] [
            a [ twitterLink trainer |> attribute "href" ] [ text trainer.Name ]])
let displayDate = function
    | ToDetermine s -> text s
    | _ -> text ""

// VIEW
let trainingProposal model =
    [div [ attribute "class" "row"] [
        div [ attribute "class" "container-fluid hyt-proposal-header"] [
            div [ attribute "class" "row" ] (
                (model.Trainers |> List.map (fun trainer ->
                    div [ attribute "class" "col-md-1 col-xs-4" ] [
                        div [ attribute "class" "hyt-training-detail-picture"] [
                            img [ trainerPhoto trainer |> attribute "src" ]]]))
                @ [ h2 [ attribute "class" "hyt-training-detail-title col-md-9"] [ trainingTitle model |> text ]])
            div [ attribute "class" "row hyt-content hyt-where-when-howmuch" ][
                div [ attribute "class" "col-md-1"] [
                    span [ attribute "class" "glyphicon glyphicon-calendar glyphicon-big" ] []]
                div [ attribute "class" "col-md-3"] [
                    model.Date |> displayDate ]
                div [ attribute "class" "col-md-1"] [
                    span [ attribute "class" "glyphicon glyphicon-map-marker glyphicon-big" ] []]
                div [ attribute "class" "col-md-3"] [
                    text model.Location ]
                div [ attribute "class" "col-md-1"] [
                    span [ attribute "class" "glyphicon glyphicon-piggy-bank glyphicon-big" ] []]
                div [ attribute "class" "col-md-3"] [
                    text model.Price ]]]
        input [ attribute "type" "checkbox"; attribute "class" "hyt-content hyt-toggle-box"; attribute "id" "who-are-the-trainers" ]
        label [ attribute "for" "who-are-the-trainers"; attribute "class" "hyt-content" ] ((text "Who are ") :: (listPerson model.Trainers))
        p [ attribute "class" "hyt-content"] [
            text "Emilien, Florent and Clément are three independant developers, working frequently together on different projects. They worked with CQRS and Event Sourcing on several projects during the last 4 years. They have contributed to create "
            a [attribute "href" "https://github.com/DevLyon/mixter" ] [ text "CQRS/ES koans" ]
            text " which have been presented in several conferences."]
        input [ attribute "type" "checkbox"; attribute "class" "hyt-content hyt-toggle-box"; attribute "id" "price-details" ]
        label [ attribute "for" "price-details"; attribute "class" "hyt-content" ] [ text "Price details" ]
        p [ attribute "class" "hyt-content"] [
            text "Les prix sont HT. 1000€/pers les 3 jours avec un minimum de 4 personnes. Pour chaque inscrit supplémentaire, le prix baisse pour tous (en rajoutant 500€ au coût global), soit 4500€ pour 5 = 900€/pers...jusqu'à 7000€ pour 10 (max) = 700€/pers."]
        input [ attribute "type" "checkbox"; attribute "class" "hyt-content hyt-toggle-box"; attribute "id" "description"; attribute "checked" "checked" ]
        label [ attribute "for" "description"; attribute "class" "hyt-content" ] [ text "Description" ]
        p [ attribute "class" "hyt-content"] [
            text "La formation est organisée autour d'ateliers pour un maximum de pratique:"
            ul [] [
                li [] [ text "Jour 1: découverte de DDD avec un Event Storming, discussion sur les implémentations possibles (choix par Bounded Context/Subdomain), notion d'architecture émergente"]
                li [] [ text "Jour 2: implémentation de fonctionnalités du cas du jour 1 avec Event Sourcing et CQRS (from scratch)"]
                li [] [ text "Jour 3: implémentation CQRS sur un code legacy"]]]
        input [ attribute "type" "checkbox"; attribute "class" "hyt-content hyt-toggle-box"; attribute "id" "who-is-interested" ]
        label [ attribute "for" "who-is-interested"; attribute "class" "hyt-content" ] [
            text (sprintf "Currently, %d people gave their feedback, and you?" (List.length model.InterestedTrainees)) ]
        p [ attribute "class" "hyt-content"] (model.InterestedTrainees |> List.map (fun trainee -> 
                span [ attribute "class" "hyt-proposal-people"] [
                    a [ twitterLink trainee |> attribute "href" ] [ text trainee.Name ]]))
        p [ attribute "class" "hyt-centered-button"] [
            a [ attribute "href" "https://goo.gl/forms/rYYfFJTtT00Q35Sg1"; attribute "class" "btn btn-success"] [ text "I give my feedback on this proposition"]]
        ]]

open About

let view model =
    div [] 
        ([trainingProposal model; About.about()] |> List.concat)

// HTTP
(*let load update =
    let getTrainingProposal = XMLHttpRequest.Create()
    getTrainingProposal.onreadystatechange <- fun _ ->
        if getTrainingProposal.readyState = 4. then
            match getTrainingProposal.status with
            | 200. | 0. ->
                JS.JSON.parse getTrainingProposal.responseText
                |> unbox |> Actions.TrainingRequestsLoaded |> update
            | _ -> ()
        null
    getTrainingProposal.``open``("GET", "/trainingRequest/sfgdfg/proposal/sdfdfg/data", true)
    getTrainingProposal.setRequestHeader("Content-Type", "application/json")
    getTrainingProposal.send(None)*)

// Start
let initialModel = 
    { Subject = "Initiation à DDD et implémentation en CQRS et Event Sourcing"
      Date = ToDetermine "3 jours en octobre (cf. feedback)"
      Location = "Lyon/Villeurbanne (à préciser)"
      Trainers = [ { Name = "Florent Pellet"; TwitterAccount = Some "florentpellet" };
        { Name = "Emilien Pecoul"; TwitterAccount = Some "ouarzy" };
        { Name = "Clément Bouillier"; TwitterAccount = Some "clem_bouillier" } ]
      Content = ""
      TargetProfiles = "Débutant avec ou sans premières expériences, beaucoup de pratique"
      Price = "1000€ HT/pers. dégressif en fonction du nombre (cf. détail)"
      InterestedTrainees = [] }

let homeApp =
    createApp { Model = initialModel; View = view; Update = Actions.homeUpdate }
    //|> withInit load
    |> withStartNode "#container"

homeApp |> start renderer
