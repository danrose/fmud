namespace fmud.tests

open NUnit.Framework
open fmud
open System

type ConsoleWatcher() =
    inherit LivingObject(Guid.NewGuid())
    interface Watcher with
        member this.DisplayMessage str =
            Console.WriteLine str

[<TestFixture>]
type Movement() = 
    let (===) (x:obj) (y:obj) =
        Assert.AreEqual(x, y)

    let itemName name item =
        item |> GameObject.setName (Description name)
        item |> GameObject.setShort (Description name)

    [<Test>]
    member this.MoveFromAtoB() =
        let containerA = Container.empty()
        let containerB = Container.empty()

        Console.WriteLine "foio"

        let item1 = { new MobileObject(System.Guid.NewGuid()) with member x.ToString() = base.ToString() }
        item1 |> itemName "ball"
        // create a ball

        // set up a watcher
        let watcher = ConsoleWatcher()
        let observer = { new LivingObject(Guid.NewGuid()) with member x.ToString() = base.ToString() }
        observer.Watchers <- watcher :> Watcher :: observer.Watchers

        // move observer and ball to container
        let move1 = Movement.moveSilently observer containerA
        let move2 = Movement.moveSilently item1 containerA

        Ok === move1
        Ok === move2

        // move the ball to the second container
        let finalMove = Movement.move [ Literal "The ball hopped along to the north"; ] [] item1 containerB
        Ok === finalMove
        ()
