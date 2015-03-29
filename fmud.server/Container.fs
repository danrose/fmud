namespace fmud
    module Container =
        open System
        open DomainTypes

        /// creates an empty container
        let public empty() =
            new ResizeArray<MobileObject>() |> Container

        /// unwraps a container to the underlying array
        let unwrap c =
            let (Container arr) = c in arr

        /// main access point for getting a container's inventory
        let public inventory c =
            c |> unwrap |> List.ofSeq

        let public allowAdd (destination:Container) (source:Container) =
            true

        let public remove ob c =
            (c |> unwrap).Remove(ob)

        let public add ob c =
            (c |> unwrap).Add(ob)
