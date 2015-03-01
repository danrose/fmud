namespace fmud
    module MobileObject =
        open System
        open DomainTypes

        let Environment (ob:MobileObject) =
            ob.Environment

        let destinationIsSource (p:EnvPair) =
            match p.source :> obj with
            | :? IHaveContainer as con -> 
                if con.Container = p.destination then
                    Failure Invalid
                else
                    Success p
            | _ -> Success p

        let canRemoveFromSource (p:EnvPair) =
            match Environment p.source with
            | None ->
                Success p
            | Some env ->  
                match p.source |> env.AllowRemove with
                    | false -> 
                        Failure CantRemoveFromSource
                    | true ->
                        Success p

        let canEnterDestination (p:EnvPair) =
            match p.source |> p.destination.AllowAdd with
                | false -> 
                    Failure CantAddToDestination
                | true ->
                    Success p

        let moveValidations = 
            destinationIsSource
            >> bind canRemoveFromSource
            >> bind canEnterDestination

        let Move msgOut msgIn mob destination =
            match moveValidations {source=mob; destination=destination} with
            | Success _ -> 
                match Environment mob with
                | Some env
                    -> env.Remove mob
                       destination.Add mob
                       Ok
                | None 
                    -> Invalid
            | Failure reason -> reason