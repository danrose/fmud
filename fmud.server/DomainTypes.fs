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

        member this.AddAlias (a:string) =
            if a.Contains(" ") then
                for x in a.Split(' ') do
                    this.AddAlias x
                    ()
            else
                match aliases |> List.exists ((=)a) with
                    | true -> ()
                    | false -> 
                        aliases <- aliases @ [a]
                        ()

        member this.RemoveAlias (a:string) =
            if a.Contains(" ") then
                (Array.forall (fun x -> this.RemoveAlias(x) = false) (a.Split(' '))) = false
            else      
                match aliases |> List.exists ((=)a) with
                        | false -> false
                        | true -> 
                            let newList = seq { for x in aliases do if x <> a then yield x }
                            aliases <- List.ofSeq newList
                            true
        
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


                
