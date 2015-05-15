namespace fmud
    module Living =
        open System
        open DomainTypes
        open GameObject
        open MobileObject

        let filterObjects (except: LivingObject list) (list: MobileObject list) =
            let ret = 
                list 
                |> List.filter (fun x ->
                    match x with
                    | :? LivingObject -> not(except |> List.exists (fun exc -> exc.id = x.id))
                    | _ -> false)
                |> List.map (fun x -> x :?> LivingObject)
            ret
                
        
        let getGender (ob:LivingObject) =
            ob.Gender

        let setGender gender (ob:LivingObject) =
            ob.Gender <- gender

        let getPossessive (who:Perspective) (ob:LivingObject) =
            perspective who ob "your" (fun x ->
                match ob.Gender with
                | Neuter -> Description "its"
                | Male -> Description "his"
                | Female -> Description "her"
            )

        let getObjective (who:Perspective) (ob:LivingObject) =
            perspective who ob "you" (fun x ->
                match ob.Gender with
                | Neuter -> Description "it"
                | Male -> Description "him"
                | Female -> Description "her"
            )

        let getWatchers (ob:LivingObject) =
            ob.Watchers

