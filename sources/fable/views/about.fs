module Views.About
    open Fable.Helpers.Virtualdom.Html

    let about () =
        [div [ attribute "class" "row"] [
            h1 [attribute "class" "hyt-title"; attribute "id" "/about"] [ text "About" ]]
         div [ attribute "class" "row" ] [
            p [ attribute "class" "hyt-content" ] [
                text "This project is open. You can contribute on "
                a [ attribute "href" "https://github.com/fpellet/HackYourTraining"] [ text "GitHub"]
                text " and on "
                a [ attribute "href" "https://softwarecraftsmanship.slack.com/messages/hackyourtraining/"] [ text "Slack"]
                text ". You can follow us also on "
                a [ attribute "href" "http://twitter.com/HackYrTraining"] [ text "Twitter."]
                br []
                text "For now, it is only a first experimentation, we are far from having all features implemented.
                    If your are interested to propose a training, please join us/contribute."]]]