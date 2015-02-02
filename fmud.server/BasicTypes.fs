module BasicTypes
    open System

    type PropertyTimeout =
        | Unlimited
        | Timed of TimeSpan

    type PropertyEntry = 
        { key: string; value: string; timeout: PropertyTimeout }

    type LightLevel = 
        | LightLevel of int

    type MoveResult =
        | InvalidDestination
        | TooHeavy
        | Ok

    let anyWithSideEffects<'a> (f:'a -> bool) (s:seq<'a>) =
        let mutable ret = false
        for x in s do
            ret <- ret || f(x)
        ret

    let trySplit (s:String) =
        if s.Contains(" ") then
            [|s|]
        else
            s.Split(' ')

    let addToList (list: string list) (setter: string list -> unit) (recursiveCall: string -> unit) (a:string) =
        match trySplit(a) with
        | [|_|] ->
            match list |> List.exists ((=)a) with
                | true -> ()
                | false -> 
                    setter(list @ [a])
        | list -> 
            Seq.iter recursiveCall list

    let removeFromList (list: string list) (setter: string list -> unit) (recursiveCall: string -> bool) (a:string) =
        match trySplit(a) with
        | [|_|] ->
            match list |> List.exists ((=)a) with
                    | false -> false
                    | true -> 
                        let newList = seq { for x in list do if x <> a then yield x }
                        setter(List.ofSeq newList)
                        true
        | list -> 
                list |> anyWithSideEffects (fun x -> recursiveCall(x)) = false