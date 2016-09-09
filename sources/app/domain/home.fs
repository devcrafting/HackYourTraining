module Domain.Home

// MODEL
type Home = { TrainingRequests: TrainingRequest list } 
and TrainingRequest = { 
    ProposedBy: Person
    Trainer: Person
    Subject: string
    Location: string
    Month: string
    Year: string
    PendingTrainerProposal: bool
    NbProposals: int
}
and Person = { Name: string; TwitterAccount: string option }

// UPDATE
type HomeAction = 
    TrainingRequestsLoaded of TrainingRequest 

let homeUpdate model action =
    match action with
    | TrainingRequestsLoaded x -> (x,[],[])
