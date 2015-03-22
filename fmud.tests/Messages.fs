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
    member this.SecondPersonShort() =
        let bob,alice = createPeople()

        let message = [ Short bob; Space; Verb ("hug","hugs"); Space; Short alice; Literal "." ]
        let processed = Message.createPerspectives message bob (Some alice)

        ("you hug Alice.", "Bob hugs you.", "Bob hugs Alice.") === processed
