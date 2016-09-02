namespace Domain.TrainingProposal

open System

type TrainingProposal = {
    Subject: string
    Date: Date
    Location: string
    Trainers: Person list
    Content: string
    TargetProfiles: string
    Price: string
    InterestedTrainees: Person list }
and Date = DateRange of DateRange | ToDetermine of string
and DateRange = {
    StartDate: DateTime
    EndDate: DateTime
}
and Person = { Name: string; TwitterAccount: string option }

module Actions =
    type Action = 
        TrainingProposalLoaded of TrainingProposal 
    let homeUpdate model action =
        match action with
        | TrainingProposalLoaded x -> (x,[], [])
