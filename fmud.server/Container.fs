module fmud.Container
    open System

    /// creates an empty container
    let public empty() =
        new ResizeArray<MobileObject>() |> Container

    /// unwraps a container to the underlying array
    let unwrap c =
        let (Container arr) = c in arr

    /// main access point for getting a container's inventory
    let public inventory c =
        unwrap c |> List.ofSeq

    let public allowAdd (destination:Container) (source:Container) =
        true

    let public remove ob c =
        (unwrap c).Remove(ob)

    let public add ob c =
        (unwrap c).Add(ob)

    let public count c =
        (unwrap c).Count
