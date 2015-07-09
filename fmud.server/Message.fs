module fmud.Message
    open System
    open GameObject

    let per3 func mob first second =
        let x = func (Calculate first) mob
        let x' = func (Calculate second) mob
        let x'' = func Third mob
        x,x',x''

    let public createPerspectives msg person1 maybePerson2 =
        let person2 = 
            match maybePerson2 with
            | None -> 
                person1
            | Some p -> 
                p
            
        let rec processStream shouldCap msg first second third =
            let cap s = if shouldCap then capitalise s else s
            let continueWith = processStream false
            
            let inline Str s style = style
            let inline write rest s1 s2 s3 = continueWith rest (cap s1::first) (cap s2::second) (cap s3::third)
            let inline changeStyle rest colour decoration = processStream shouldCap rest first second third
            let inline capFirst rest = processStream true rest first second third
              
            match msg with
            | [] -> 
                let join list =
                    list
                    |> List.rev
                    |> Array.ofList
                    |> String.concat ""
                join first, join second, join third
            | H::T ->
                match H with
                | Style (colour,decoration) -> changeStyle T colour decoration
                | Capitalise -> capFirst T
                | Literal s -> write T s s s
                | Space -> write T " " " " " "
                | Short mob ->
                    let s,s',s'' = per3 GameObject.getShort mob person1 person2
                    continueWith T (cap s::first) (cap s'::second) (cap s''::third)
                | Possessive mob ->
                    let s,s',s'' = per3 Living.getPossessive mob person1 person2
                    continueWith T (cap s::first) (cap s'::second) (cap s''::third)
                | Verb (f,s) ->
                    continueWith T (cap f::first) (cap s::second) (cap s::third)
                | _ -> failwith "unknown"
        processStream false msg [] [] []

    let public tellPerson message person =
        person 
        |> Living.getWatchers
        |> List.iter (fun x -> x.DisplayMessage message)

    let public tellPeople message people =
        people
        |> List.iter (tellPerson message)

    let public tellContainer message exclude container =
        container
        |> Container.inventory
        |> Living.filterObjects exclude
        |> List.iter (tellPerson message)

    let public tellRoom message exclude (room:IHaveContainer) =
        tellContainer message exclude room.Container