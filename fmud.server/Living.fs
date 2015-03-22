namespace fmud
    module Living =
        open System
        open DomainTypes
        open GameObject
        open MobileObject

        let GetGender (ob:LivingObject) =
            ob.Gender

        let SetGender gender (ob:LivingObject) =
            ob.Gender <- gender

        let GetPossessive (who:Perspective) (ob:LivingObject) =
            perspective who ob "your" (fun x ->
                match ob.Gender with
                | Neuter -> "its"
                | Male -> "his"
                | Female -> "her"
            )

        let GetObjective (who:Perspective) (ob:LivingObject) =
            perspective who ob "you" (fun x ->
                match ob.Gender with
                | Neuter -> "it"
                | Male -> "him"
                | Female -> "her"
            )

