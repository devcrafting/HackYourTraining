module Infra.TrainingRequests

open Domain.TrainingRequest

let getFromFriendlyName friendlyUrl = 
    {
        ProposedBy = { Name = "Emilien Pecoul"; TwitterAccount = Some "ouarzy" }
        Subject = "CQRS/Event Sourcing"
        Location = "Lyon"
        Month = "September"
        Year = "2016"
        Trainer = { Name = "Greg Young"; TwitterAccount = Some "gregyoung" }
        InterestedTrainees = 
        [
            { Name = "Emilien Pecoul"; TwitterAccount = Some "Ouarzy" }
            { Name = "Florent Pellet"; TwitterAccount = Some "florentpellet" }
            { Name = "Clément Bouillier"; TwitterAccount = Some "clem_bouillier" }
            { Name = "Jean Helou"; TwitterAccount = Some "jeanhelou" }
            { Name = "Yannick Ringapin"; TwitterAccount = Some "BlackBeard486" }
            { Name = "Kevin Lejeune"; TwitterAccount = Some "kevin_le_jeune" }
            { Name = "Karol Chmist"; TwitterAccount = Some "karolchmist" }
            { Name = "Gregory Cica"; TwitterAccount = None }
            { Name = "François Miton"; TwitterAccount = None }
            { Name = "Bertrand Saintes"; TwitterAccount = None }
            { Name = "Samuel Pecoul"; TwitterAccount = Some "SamPecoul" }
            { Name = "Nadège Rouelle"; TwitterAccount = Some "nadegerouelle" }
            { Name = "Romain Berthon"; TwitterAccount = Some "RomainTrm" }
            { Name = "Benjamin Garel"; TwitterAccount = Some "bgarel" }
            { Name = "Romain Dequidt"; TwitterAccount = Some "romain_dequidt" }
            { Name = "Lucas Courot"; TwitterAccount = Some "lucas_courot" }
            { Name = "Jean Detoeuf"; TwitterAccount = Some "thebignet" }
            { Name = "Agnès Crepet"; TwitterAccount = Some "agnes_crepet" }
            { Name = "Mathieu Petitdant"; TwitterAccount = Some "mpetitdant" }
        ]
    }