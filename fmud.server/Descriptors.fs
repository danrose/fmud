namespace fmud
    module Descriptors =
        open System
        open DomainTypes

        let eval descriptor =
            match descriptor with
            | Description str -> str
            | Callback cb -> cb()