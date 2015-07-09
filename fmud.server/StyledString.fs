module fmud.StyledString
    let inform =
        function
        | SString (inform,_,_) -> inform

    let value =
        function
        | SString (_,msg,_) -> msg

    let formatting =
        function
        | SString (_,_,form) -> form

    let private createStyle (decoration:Decoration) (colour:Colour) = Some(colour,decoration)
    
    let standard = createStyle NoDecoration
    let bold = createStyle Bold
    let underlined = createStyle Italic
    let (@) (opt:(Colour*Decoration) option) (i:int) = 
        match opt with
        | None -> i,Colour.Default,Decoration.NoDecoration
        | Some (colour,dec) -> i,colour,dec

    let reset = Some DefaultStyle

    let fromTuples inform tuples =
        let bui = new System.Text.StringBuilder()
        let markers =
            tuples
            |> List.fold (fun (accum: int*((int*Colour*Decoration) list)) item ->
                let style,(msg:string) = item
                let i,styles = accum
                let i' = i + msg.Length

                // mutate the builder
                bui.Append(msg) |> ignore

                match style with
                | None -> i',styles
                | Some s -> 
                    match s with
                    | (colour,decoration) ->
                        i',((i,colour,decoration)::styles)

            ) (0,[])
        SString (inform,bui.ToString(), snd markers)
            

