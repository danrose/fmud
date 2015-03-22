namespace fmud
    [<AutoOpen>]
    module DomainTypes =
        open System
        open SharedFunctions

        type PropertyTimeout =
            | Unlimited
            | Timed of TimeSpan

        type Limit =
            | Unlimited
            | Limit of int

        type PropertyEntry = { key: string; value: string; timeout: PropertyTimeout }

        type LightLevel = | LightLevel of int

        type MoveResult = Invalid | CantRemoveFromSource | CantAddToDestination | TooHeavy | Ok

        /// Colour and style information
        type Colour = Default | Red | Orange | Yellow | Green | Blue | Indigo | Violet | Grey | White | Pink

        type Decoration = NoDecoration | Bold | Italic

        let DefaultStyle = Colour.Default,Decoration.NoDecoration


        [<AbstractClass>]
        type GameObject(id: Guid) =  
            
            let mutable name = ""
            let mutable short = ""
            let mutable long = ""
            let mutable determinate = ""
            let mutable alias = Set.empty<string>
            let mutable plurals = Set.empty<string>
            let mutable adjectives = Set.empty<string>

            member this.id = id
            member this.Name with get() = name and set(n) = name <- n
            member this.Short with get() = short and set(n) = short <- n
            member this.Determinate with get() = determinate and set(n) = determinate <- n
            member this.Long with get() = long and set(n) = long <- n
            member this.Alias with get() = alias and set(n) = alias <- n
            member this.Plurals with get() = plurals and set(n) = plurals <- n
            member this.Adjectives with get() = adjectives and set(n) = adjectives <- n

             // name and descriptions

            interface IDisposable with 
                member this.Dispose() =
                    ()

            member this.Destroy =
                ()
        and ExtraLook = 
            {key: string; func: GameObject -> string;}

        [<AbstractClass>]
        type public MobileObject(id: Guid) =
            inherit GameObject(id)

            let mutable (environment:Container option) = None
            member this.Environment with get() = environment and set(e) = environment <- e

            abstract CanEnterContainer: Container -> bool
            default this.CanEnterContainer c =
                true
            abstract member CanLeaveContainer: Container -> bool
            default this.CanLeaveContainer c =
                true

        and IHaveContainer =
            abstract Container:Container with get
        and EnvPair = { source:MobileObject;destination:Container; }
        and public Container =
            | Container of ResizeArray<MobileObject>

        type MsgToken = 
            | Space
            | Literal of string
            | Possessive of MobileObject
            | Short of MobileObject
            | Long of MobileObject
            | OneShort of MobileObject
            | TheShort of MobileObject
            | Pronoun of MobileObject
            | Objective of MobileObject
            | Verb of string * string
            | Style of Colour * Decoration

        type Message = MsgToken list

        type public Player(id: Guid) =
            inherit MobileObject(id)


      //  [<AbstractClass>]
     //   type public Room(id: Guid) =
      //      inherit GameObject(id)

       //     let container = Container()

        //    interface IHaveContainer with
        //        member this.Container with get() = container

        //    member this.a = 1

                
