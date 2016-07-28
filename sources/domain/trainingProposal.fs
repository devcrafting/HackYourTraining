module Domain.TrainingProposal

// MODEL
type TrainingProposal = {
    ProposedBy: Person
    Trainer: Person
    Location: string
    Month: string
    Year: string
    InterestedTrainees: Person list }
and Person = { Name: string; TwitterAccount: string option }

// UPDATE
type HomeAction = 
    TrainingProposalsLoaded of TrainingProposal 

let homeUpdate model action =
    match action with
    | TrainingProposalsLoaded x -> (x,[], [])
