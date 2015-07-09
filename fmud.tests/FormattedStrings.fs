namespace fmud.tests

open NUnit.Framework
open fmud
open StyledString

[<TestFixture>]
type ``String formatting tests for colours``() = 
    
    [<Test>]
    member this.``Generate a non-formatted string``() =
        let formatted = fromTuples Inform.Default [None,"Hello There"]
        inform formatted === Inform.Default
        value formatted === "Hello There"
        formatting formatted == []

    [<Test>]
    member this.``Basic format from start of string``() =
        let formatted = fromTuples Inform.Default [Some (Colour.Green,Decoration.NoDecoration), "Hello There"]
        inform formatted === Inform.Default
        value formatted === "Hello There"
        formatting formatted == [standard Green @ 0]

    [<Test>]
    member this.``Append without format change continues with same``() =
        let formatted = fromTuples Inform.Default [standard Green, "Hello there"; None, " little fish" ]
        inform formatted === Inform.Default
        value formatted === "Hello there little fish"
        formatting formatted == [standard Green @ 0]

    [<Test>]
    member this.``Format mid-string``() =
        let formatted = fromTuples Inform.Default [None, "Hello there"; standard Red, " little fish" ]
        inform formatted === Inform.Default
        value formatted === "Hello there little fish"
        formatting formatted === [standard Red @ 11]

    [<Test>]
    member this.``Switch format mid-string``() =
        let formatted = fromTuples Inform.Default [None, "The iron golem"; bold Red, " wakes "; standard Yellow, "late" ]
        inform formatted === Inform.Default
        value formatted === "The iron golem wakes late"
        formatting formatted |> List.rev == [bold Red @ 14; standard Yellow @ 21]