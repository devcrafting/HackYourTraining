module Views.TrainingRequest

open Fable.Core 
open Fable.Import
open Fable.Import.Browser

open Fable.Helpers.Virtualdom
open Fable.Helpers.Virtualdom.App
open Fable.Helpers.Virtualdom.Html

open Domain.TrainingRequest

open System

let trainerPhoto (trainer:Person) = 
    match trainer.TwitterAccount with
    | None -> "/images/unknown.jpg"
    | Some account -> sprintf "https://twitter.com/%s/profile_image?size=original" account
let trainingTitle model = sprintf "%s with %s (%s - %s)" model.Subject model.Trainer.Name model.Location model.Month
let twitterLink person = 
    match person.TwitterAccount with
    | Some account -> sprintf "https://www.twitter.com/%s" account
    | None -> String.Empty 

// VIEW
let trainingRequest model =
    [div [ attribute "class" "row"] [
        div [ attribute "class" "col-md-1 col-xs-4" ] [
            div [ attribute "class" "hyt-training-detail-picture"] [
                img [ trainerPhoto model.Trainer |> attribute "src" ]]]
        h2 [ attribute "class" "col-md-11 col-xs-8 hyt-training-detail-title"] [ 
            trainingTitle model |> text]]
     div [ attribute "class" "row"] [
        p [ attribute "class" "hyt-content"] [
            a [ twitterLink model.ProposedBy |> attribute "href" ] [ text model.ProposedBy.Name]
            text " propose a "
            a [ twitterLink model.Trainer |> attribute "href" ] [ text model.Trainer.Name ]
            text (sprintf " training on %s in " model.Subject)
            a [ attribute "href" (sprintf "https://www.google.fr/maps/place/%s" model.Location)] [ text model.Location]
            sprintf " in %s %s." model.Month model.Year |> text ]
        input [ attribute "type" "checkbox"; attribute "class" "hyt-content hyt-toggle-box"; attribute "id" "who-is-trainer" ]
        label [ attribute "for" "who-is-trainer"; attribute "class" "hyt-content" ] [ text (sprintf "Who is %s" model.Trainer.Name) ]
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
        input [ attribute "type" "checkbox"; attribute "class" "hyt-content hyt-toggle-box"; attribute "id" "who-is-interested" ]
        label [ attribute "for" "who-is-interested"; attribute "class" "hyt-content" ] [
            text (sprintf "Currently, %d people are interested, and you?" (List.length model.InterestedTrainees)) ]
        p [ attribute "class" "hyt-content"] (model.InterestedTrainees |> List.map (fun trainee -> 
                span [ attribute "class" "hyt-proposal-people"] [
                    a [ twitterLink trainee |> attribute "href" ] [ text trainee.Name ]]))
        p [ attribute "class" "hyt-centered-button"] [
            a [ attribute "href" "https://goo.gl/forms/rYYfFJTtT00Q35Sg1"; attribute "class" "btn btn-success"] [ text "I am interested"]]
        p [ attribute "class" "hyt-content hyt-answer-hightlight row"] [
            span [ attribute "class" "glyphicon glyphicon-alert glyphicon-big col-md-1" ] []
            span [ attribute "class" "col-md-*" ] [ text "Sorry, Greg did not propose a training corresponding to this request. It is now opened to any trainer who would like to propose an alternative." ]]
        p [ attribute "class" "hyt-content"] [
            ul [] [
                li [] (
                    [ a [ attribute "href" "2016-09-Lyon-GregYoung-CQRS-ES/proposal/ouarzy-clem_bouillier-florentpellet" ][ text "View proposal (in french)" ]
                      text " from " ]
                    @ ([ { Name = "Emilien Pecoul"; TwitterAccount = Some "ouarzy" }
                         { Name = "Clément Bouillier"; TwitterAccount = Some "clem_bouillier" }
                         { Name = "Florent Pellet"; TwitterAccount = Some "florentpellet" } ] 
                        |> List.map (fun trainer -> 
                            span [ attribute "class" "hyt-proposal-people"] [
                                a [ twitterLink trainer |> attribute "href" ] [ text trainer.Name ]]))
                    @ [ text "" ]
                )
        ]]
        p [ attribute "class" "hyt-centered-button"] [
            a [ attribute "href" "https://goo.gl/forms/bDIayOikoiTbVxZw1"; attribute "class" "btn btn-success"] [ text "Make another training proposal" ]]
    ]]

open About

let homeView model =
    div [] 
        ([trainingRequest model; About.about()] |> List.concat)

// HTTP
let load update =
    let getTrainingRequest = XMLHttpRequest.Create()
    getTrainingRequest.onreadystatechange <- fun _ ->
        if getTrainingRequest.readyState = 4. then
            match getTrainingRequest.status with
            | 200. | 0. ->
                JS.JSON.parse getTrainingRequest.responseText
                |> unbox |> Actions.TrainingRequestsLoaded |> update
            | _ -> ()
        null
    getTrainingRequest.``open``("GET", "/trainingRequest/sfgdfg/index", true)
    getTrainingRequest.setRequestHeader("Content-Type", "application/json")
    getTrainingRequest.send(None)

// Start
let initialModel = 
    { ProposedBy = { Name = "?"; TwitterAccount = None }
      Trainer = { Name = "?"; TwitterAccount = None }
      Subject = "?"
      Location = "?"
      Month = "?"
      Year = "?"
      InterestedTrainees = [] }

let homeApp =
    createApp { Model = initialModel; View = homeView; Update = Actions.homeUpdate }
    |> withInit load
    |> withStartNode "#container"

homeApp |> start renderer
