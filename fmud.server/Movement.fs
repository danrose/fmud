module fmud.Movement
    open System
    open Container
    open GameObject
    open MobileObject

    let destinationIsSource (p:EnvPair) =
        match p.source :> obj with
        | :? IHaveContainer as con -> 
            if con.Container = p.destination then
                Failure Invalid
            else
                Success p
        | _ -> Success p

    let canRemoveFromSource (p:EnvPair) =
        match environment p.source with
        | None ->
            Success p
        | Some env ->  
            match true with //p.source |> env.AllowRemove with
                | false -> 
                    Failure CantRemoveFromSource
                | true ->
                    Success p

    let canEnterDestination (p:EnvPair) =
        match true with //p.source |> p.destination.AllowAdd with
            | false -> 
                Failure CantAddToDestination
            | true ->
                Success p

    let moveValidations = 
        destinationIsSource
        >> bind canRemoveFromSource
        >> bind canEnterDestination

    let move msgOut msgIn (mob:MobileObject) destination =
        match moveValidations {source=mob; destination=destination} with
        | Failure reason  -> reason
        | Success _ -> 
            // if they are currently in an environment then remove them from it
            match environment mob with
            | Some env ->
                // remove them from their existing env 
                env |> remove mob |> ignore  

                // determines which perspectives are valid and whether
                // the object should be informed
                let inform msg firstPerson =
                    if msg <> noMessage then
                        let p1,_,p3 = Message.createPerspectives msg mob None
                        match mob with
                        | :? LivingObject as who ->
                            if firstPerson then who |> Message.tellPerson p1
                            env |> Message.tellContainer p3 [who]
                        | _ -> 
                            env |> Message.tellContainer p3 []

                inform msgOut true
                inform msgIn false             
            | _ -> 
                printfn "No environment"
                ()
            
            destination |> add mob
            mob.Environment <- Some destination
            Ok  

    let moveSilently mob destination = move [] [] mob destination
