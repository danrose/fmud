module DomainTypes
    open System.Collections.Generic
    open System
    open BasicTypes 

    [<AbstractClass>]
    type GameObject = 
        interface IDisposable
        member Id: Guid
        
        member IdMatch: string -> bool
        member IdMatch: Guid -> bool

        // name and descriptions
        abstract member GetName: unit -> string
        member SetName: string -> unit
        member SetShort: string -> unit
        abstract member GetShort: unit -> string
        member SetLong: string -> unit
        abstract member GetLong: unit -> string
        member AddAlias: string -> unit
        member RemoveAlias: string -> bool
        abstract member GetAliases: unit -> seq<string>
        member AddPlural: string -> unit
        member RemovePlural: string -> bool
        abstract member GetPlurals: unit -> seq<string>
        member AddAdjective: string -> unit
        member RemoveAdjective: string -> bool
        abstract member GetAdjectives: unit -> seq<string>
//        member CalculateExtraLooks: unit -> string
//        member AddExtraLook: ExtraLook -> unit
//        member RemoveExtraLook: string -> unit
//        abstract member QueryExtraLooks: unit -> seq<ExtraLook>
        member SetDeterminate: string -> unit
        abstract member GetDeterminate: unit -> string
        
        // properties
//        member AddProperty: PropertyEntry -> bool
//        member RemoveProperty: string -> bool
//        abstract member QueryProperty: string -> string option
//        abstract member QueryPropertyTimeLeft: string -> TimeSpan option
//        abstract member QueryProperties: unit -> Map<string,PropertyEntry>

        // sensory info
//        member SetLightLevel: LightLevel -> unit
//        abstract member QueryLightLevel: unit -> LightLevel
//        abstract member QueryOwnLightLevel: unit -> LightLevel
//        abstract member InformLightLevelChanged: unit -> unit

        // lifecycle methods
//        abstract member Create: unit -> unit
//        abstract member Init: unit -> unit
//        abstract member Destroy: unit -> unit

    and ExtraLook = 
        {key: string; func: GameObject -> string;}

    [<AbstractClass>]
    type MobileObject = 
        inherit GameObject

        // environment
        member Environment: unit -> Container

        // container flags
        abstract member CanEnterContainer: Container -> bool
        abstract member CanLeaveContainer: Container -> bool

        // weight
//        member SetWeight: int -> unit
//        member GetWeight: unit -> int

        // movement
        abstract member Move: Container -> string -> string -> MoveResult
    and [<Class>]Container =
       // member SetWeightLimit: Limit -> unit
       // member GetWeightLimit: unit -> Limit
      //  member Inventory: unit -> seq<GameObject>
      //  member InventoryString: unit -> string
      //  member DeepInventory: unit -> seq<GameObject>
        abstract member AllowAdd: GameObject -> bool
        abstract member AllowRemove: GameObject -> bool