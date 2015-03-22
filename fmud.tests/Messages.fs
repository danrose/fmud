namespace fmud.tests

open NUnit.Framework
open fmud
open System

[<TestFixture>]
type Messages() = 
    let (===) (x:obj) (y:obj) =
        Assert.AreEqual(x, y)

    let createPeople() =
        let bob = new Player(Guid.NewGuid())
        bob |> GameObject.SetShort "Bob"

        let alice = new Player(Guid.NewGuid())
        alice |> GameObject.SetShort "Alice"

        bob,alice
        

    [<Test>]
    member this.UninflectedMessage() =
        let bob,alice = createPeople()

        let message = [ Literal "Bob is a dope." ]
        let processed = Message.createPerspectives message bob (Some alice)

        ("Bob is a dope.", "Bob is a dope.", "Bob is a dope.") === processed

    [<Test>]
    member this.TwoLiterals() =
        let bob,alice = createPeople()

        let message = [ Literal "Bob"; Literal " is a dope." ]
        let processed = Message.createPerspectives message bob (Some alice)

        ("Bob is a dope.", "Bob is a dope.", "Bob is a dope.") === processed

    [<Test>]
    member this.Spaces() =
        let bob,alice = createPeople()

        let message = [ Literal "Bob"; Space; Literal "is a dope." ]
        let processed = Message.createPerspectives message bob (Some alice)

        ("Bob is a dope.", "Bob is a dope.", "Bob is a dope.") === processed

    [<Test>]
    member this.SimpleVerb() =
        let bob,alice = createPeople()

        let message = [ Literal "Bob"; Space; Verb ("hug","hugs"); Space; Literal "Alice." ]
        let processed = Message.createPerspectives message bob (Some alice)

        ("Bob hug Alice.", "Bob hugs Alice.", "Bob hugs Alice.") === processed

    [<Test>]
    member this.FirstPersonShort() =
        let bob,alice = createPeople()

        let message = [ Short bob; Space; Verb ("hug","hugs"); Space; Literal "Alice." ]
        let processed = Message.createPerspectives message bob (Some alice)

        ("you hug Alice.", "Bob hugs Alice.", "Bob hugs Alice.") === processed

    [<Test>]
    member this.CapFirst() =
        let bob,alice = createPeople()

        let message = [ Capitalise; Short bob; Space; Verb ("hug","hugs"); Space; Literal "Alice." ]
        let processed = Message.createPerspectives message bob (Some alice)

        ("You hug Alice.", "Bob hugs Alice.", "Bob hugs Alice.") === processed

    [<Test>]
    member this.SecondPersonShort() =
        let bob,alice = createPeople()

        let message = [ Capitalise; Short bob; Space; Verb ("hug","hugs"); Space; Short alice; Literal "." ]
        let processed = Message.createPerspectives message bob (Some alice)

        ("You hug Alice.", "Bob hugs you.", "Bob hugs Alice.") === processed

    [<Test>]
    member this.PossessiveNeuter() =
        let bob,alice = createPeople()

        let message = [ Capitalise; Short alice; Space; Verb ("punch","punches"); Space; Short bob; Literal " in "; Possessive bob; Literal " face." ]
        let processed = Message.createPerspectives message alice (Some bob)

        ("You punch Bob in its face.", "Alice punches you in your face.", "Alice punches Bob in its face.") === processed

    [<Test>]
    member this.PossessiveMale() =
        let bob,alice = createPeople()
        bob.Gender <- Male

        let message = [ Capitalise; Short alice; Space; Verb ("punch","punches"); Space; Short bob; Literal " in "; Possessive bob; Literal " face." ]
        let processed = Message.createPerspectives message alice (Some bob)

        ("You punch Bob in his face.", "Alice punches you in your face.", "Alice punches Bob in his face.") === processed

    [<Test>]
    member this.PossessiveFemale() =
        let bob,alice = createPeople()
        bob.Gender <- Female

        let message = [ Capitalise; Short alice; Space; Verb ("punch","punches"); Space; Short bob; Literal " in "; Possessive bob; Literal " face." ]
        let processed = Message.createPerspectives message alice (Some bob)

        ("You punch Bob in her face.", "Alice punches you in your face.", "Alice punches Bob in her face.") === processed
