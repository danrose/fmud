module fmud.GameObject
    open System
    open Descriptors

    type MatchOption = 
    | Id of Guid
    | Query of string

    type Perspective =
        | First
        | Third
        | Calculate of GameObject

    let idMatch query =
        match query with
        | Id id -> false
        | Query qs -> false

    let perspective (who:Perspective) (ob:GameObject) fp (f: GameObject -> Descriptor) =
        match who with
        | Calculate w when w = ob -> fp
        | First -> fp
        | _ -> f ob |> Descriptors.eval

    let getName (who:Perspective) (ob:GameObject) =
        perspective who ob "you" (fun ob -> ob.Name)

    let setName name (ob:GameObject) =
        ob.Name <- name

    let getShort (who:Perspective) (ob:GameObject) =
        perspective who ob "you" (fun ob -> ob.Short)

    let setShort short (ob:GameObject) =
        ob.Short <- short

    let getMainAlias (ob:GameObject) =
        Descriptors.eval ob.MainPlural

    let setMainAlias alias (ob:GameObject) =
        ob.MainPlural <- alias

    let getLong (ob:GameObject) =
        match Descriptors.eval ob.Long with
        | s when String.IsNullOrEmpty s -> "Nothing to see."
        | s -> s
 
    let setLong long (ob:GameObject) =
        ob.Long <- long

    let getDeterminate (who:Perspective) (ob:GameObject) =
        perspective who ob "" (fun ob -> ob.Determinate)

    let setDeterminate (ob:GameObject) determinate =
        ob.Determinate <- determinate

    let getAliases (ob:GameObject) =
        ob.Alias |> Seq.toList

    let addAlias (ob:GameObject) (a:string) =
        ob.Alias <- ob.Alias @+ a

    let removeAlias (ob:GameObject) (a:string) =
        ob.Alias <- ob.Alias @- a

    let getAdjectives (ob:GameObject) =
        ob.Adjectives |> Seq.toList

    let addAdjectives (ob:GameObject) (a:string) =
        ob.Adjectives <- ob.Adjectives @+ a

    let removeAdjective (ob:GameObject) (a:string) =
        ob.Adjectives <- ob.Adjectives @- a

    let getPlural (ob:GameObject) =
        ob.Plurals |> Seq.toList

    let addPlural (ob:GameObject) (p:string) =
        ob.Plurals <- ob.Plurals @+ p

    let removePlural (ob:GameObject) (p:string) =
        ob.Plurals <- ob.Plurals @- p

            
            



