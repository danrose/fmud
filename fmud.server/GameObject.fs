namespace fmud
    module GameObject =
        open System
        open DomainTypes

        type MatchOption = 
        | Id of Guid
        | Query of string

        let IdMatch query =
            match query with
            | Id id
                -> false
            | Query qs
                -> false

        let GetName (ob:GameObject) =
            ob.Name

        let SetName (ob:GameObject) name =
            ob.Name <- name

        let GetShort (ob:GameObject) =
            ob.Short

        let SetShort (ob:GameObject) short =
            ob.Short <- short

        let GetLong (ob:GameObject) =
            ob.Long

        let SetLong (ob:GameObject) long =
            ob.Long <- long

        let GetDeterminate (ob:GameObject) =
            ob.Determinate

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
            
            



