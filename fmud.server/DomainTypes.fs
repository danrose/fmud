module DomainTypes
    open System.Collections.Generic
    open System
    open BasicTypes

    [<AbstractClass>]
    type GameObject(id: Guid) = 
        let mutable name = ""
        let mutable short = ""
        let mutable long = ""
        let mutable aliases = List.empty<string>
        let mutable plurals = List.empty<string>
        let mutable adjectives = List.empty<string>

        let setAliases = (fun x -> aliases <- x)
        let setAdjectives = (fun x -> adjectives <- x)
        let setPlurals = (fun x -> plurals <- x)

        member this.Id with get() = id

        member this.IdMatch (s:string) =
            false

        member this.IdMatch (i:Guid) =
            i = id

         // name and descriptions
        abstract GetName: unit -> string
        default this.GetName() = name
        member this.SetName (s:string) = name <- s

        abstract GetShort: unit -> string
        default this.GetShort() = short
        member this.SetShort (s:string) = short <- s

        abstract GetLong: unit -> string
        default this.GetLong() = long
        member this.SetLong (s:string) = long <- s

        member this.AddAlias (a:string) = addToList aliases setAliases this.AddAlias a
        member this.RemoveAlias (a:string) = removeFromList aliases setAliases this.RemoveAlias a
        abstract GetAliases: unit -> seq<string>
        default this.GetAliases() = Seq.ofList aliases

        member this.AddAdjective (a:string) = addToList adjectives setAdjectives this.AddAdjective a
        member this.RemoveAdjective (a:string) = removeFromList adjectives setAdjectives this.RemoveAdjective a
        abstract GetAdjectives: unit -> seq<string>
        default this.GetAdjectives() = Seq.ofList adjectives
          
        member this.AddPlural (a:string) = addToList plurals setPlurals this.AddPlural a
        member this.RemovePlural (a:string) = removeFromList plurals setPlurals this.RemovePlural a
        abstract GetPlurals: unit -> seq<string>
        default this.GetPlurals() = Seq.ofList plurals

        interface IDisposable with 
            member this.Dispose() =
                ()

        member this.Destroy =
            ()
    and ExtraLook = 
        {key: string; func: GameObject -> string;}

    [<AbstractClass>]
    type MobileObject(id: Guid) =
        inherit GameObject(id)


                
