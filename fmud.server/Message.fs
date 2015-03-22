namespace fmud
    module Message =
        open System
        open DomainTypes
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
                
                match msg with
                | [] -> 
                    let join list =
                        list
                        |> List.rev
                        |> Array.ofList
                        |> String.concat ""
                    join first,join second, join third
                | H::T ->
                    match H with
                    | Capitalise ->
                        processStream true T first second third
                    | Literal s ->
                        let append = cap s
                        processStream false T (append::first) (append::second) (append::third)
                    | Space ->
                        processStream false T (" "::first) (" "::second) (" "::third)
                    | Short mob ->
                        let s,s',s'' = per3 GameObject.GetShort mob person1 person2
                        processStream false T (cap s::first) (cap s'::second) (cap s''::third)
                    | Possessive mob ->
                        let s,s',s'' = per3 Living.GetPossessive mob person1 person2
                        processStream false T (cap s::first) (cap s'::second) (cap s''::third)
                    | Verb (f,s) ->
                        processStream false T (cap f::first) (cap s::second) (cap s::third)
                    | _ -> failwith "unknown"
            processStream false msg [] [] []
            