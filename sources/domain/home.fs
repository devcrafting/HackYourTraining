module Domain.Home

// MODEL
type Home = { TrainingProposals: TrainingProposal list } 
and TrainingProposal = { InterestedTrainees: InterestedTrainee list }
and InterestedTrainee = { Name: string; TwitterUrl: string }

// UPDATE
type HomeAction = 
    TrainingProposalsLoaded of TrainingProposal 

let homeUpdate model action =
    match action with
    | TrainingProposalsLoaded x -> (x,[])
