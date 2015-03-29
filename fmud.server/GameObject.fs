namespace fmud
    module GameObject =
        open System
        open DomainTypes
        open Descriptors

        type MatchOption = 
        | Id of Guid
        | Query of string

        type Perspective =
            | First
            | Third
            | Calculate of GameObject

        let IdMatch query =
            match query with
            | Id id
                -> false
            | Query qs
                -> false

        let perspective (who:Perspective) (ob:GameObject) fp (f: GameObject -> Descriptor) =
            match who with
            | Calculate w when w = ob ->
                fp
            | First ->
                fp
            | _ -> 
                f ob
                |> Descriptors.eval

        let GetName (who:Perspective) (ob:GameObject) =
            perspective who ob "you" (fun ob -> ob.Name)

        let SetName (ob:GameObject) name =
            ob.Name <- name

        let GetShort (who:Perspective) (ob:GameObject) =
            perspective who ob "you" (fun ob -> ob.Short)

        let SetShort short (ob:GameObject) =
            ob.Short <- short

        let GetLong (ob:GameObject) =
            match Descriptors.eval ob.Long with
            | s when String.IsNullOrEmpty s -> "Nothing to see."
            | s -> s
 
        let SetLong (ob:GameObject) long =
            ob.Long <- long

        let GetDeterminate (who:Perspective) (ob:GameObject) =
            perspective who ob "" (fun ob -> ob.Determinate)

        let SetDeterminate (ob:GameObject) determinate =
            ob.Determinate <- determinate

        let GetAliases (ob:GameObject) =
            ob.Alias |> Seq.toList

        let AddAlias (ob:GameObject) (a:string) =
            ob.Alias <- ob.Alias @+ a

        let RemoveAlias (ob:GameObject) (a:string) =
            ob.Alias <- ob.Alias @- a

        let GetAdjectives (ob:GameObject) =
            ob.Adjectives |> Seq.toList

        let AddAdjectives (ob:GameObject) (a:string) =
            ob.Adjectives <- ob.Adjectives @+ a

        let RemoveAdjective (ob:GameObject) (a:string) =
            ob.Adjectives <- ob.Adjectives @- a

        let GetPlural (ob:GameObject) =
            ob.Plurals |> Seq.toList

        let AddPlural (ob:GameObject) (p:string) =
            ob.Plurals <- ob.Plurals @+ p

        let RemovePlural (ob:GameObject) (p:string) =
            ob.Plurals <- ob.Plurals @- p

            
            



