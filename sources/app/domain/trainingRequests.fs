namespace Domain.TrainingRequest.Repositories

open Domain.TrainingRequest

type TrainingRequests = {
    getFromFriendlyUrl: string -> TrainingRequest
}