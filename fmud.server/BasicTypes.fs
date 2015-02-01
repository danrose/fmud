module BasicTypes
    open System

    type PropertyTimeout =
        | Unlimited
        | Timed of TimeSpan

    type PropertyEntry = 
        { key: string; value: string; timeout: PropertyTimeout }

    type LightLevel = 
        | LightLevel of int

    type MoveResult =
        | InvalidDestination
        | TooHeavy
        | Ok