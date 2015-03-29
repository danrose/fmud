namespace fmud
    module Movement =
        open System
        open DomainTypes
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

        let move msgOut msgIn mob destination =
            match moveValidations {source=mob; destination=destination} with
            | Success _ -> 
                match environment mob with
                | Some env
                    -> //env.Remove mob
                      // destination.Add mob
                       Ok
                | None -> 
                    destination |> add mob
                    Ok
            | Failure reason 
                -> reason

