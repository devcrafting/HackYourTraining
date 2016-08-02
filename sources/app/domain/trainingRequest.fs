namespace Domain.TrainingRequest

// MODEL
type TrainingRequest = {
    ProposedBy: Person
    Trainer: Person
    Location: string
    Month: string
    Year: string
    InterestedTrainees: Person list }
and Person = { Name: string; TwitterAccount: string option }

// UPDATE
module Actions =
    type Action = 
        TrainingRequestsLoaded of TrainingRequest 
    let homeUpdate model action =
        match action with
        | TrainingRequestsLoaded x -> (x,[], [])
