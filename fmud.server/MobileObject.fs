namespace fmud
    module MobileObject =
        open System
        open DomainTypes

        let environment (ob:MobileObject) =
            ob.Environment