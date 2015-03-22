namespace fmud
    module Message =
        open System
        open DomainTypes
        open GameObject

        let per3 (func: Perspective -> GameObject -> string) mob first second =
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
            
            let rec processStream msg first second third =
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
                    | Literal s ->
                        processStream T (s::first) (s::second) (s::third)
                    | Space ->
                        processStream T (" "::first) (" "::second) (" "::third)
                    | Short mob ->
                        let s,s',s'' = per3 GameObject.GetShort mob person1 person2
                        processStream T (s::first) (s'::second) (s''::third)
                    | Verb (f,s) ->
                        processStream T (f::first) (s::second) (s::third)
                    | _ -> failwith "unknown"
            processStream msg [] [] []
            