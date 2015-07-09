module fmud.Descriptors
    let eval descriptor =
        match descriptor with
        | Description str -> str
        | Callback cb -> cb()