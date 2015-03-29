namespace fmud.tests

open NUnit.Framework
open fmud

[<TestFixture>]
type Containers() = 
    let (===) (x:obj) (y:obj) =
        Assert.AreEqual(x, y)

    [<Test>]
    member this.EmptyContainer() =
        let c = Container.empty()
        Assert.IsEmpty(c |> Container.inventory)

    [<Test>]
    member this.AddItem() =
        let c = Container.empty()
        let item = { new MobileObject(System.Guid.NewGuid()) with member x.ToString() = base.ToString() }
        item |> GameObject.SetName (Description "ball")
        item |> GameObject.SetShort (Description "ball")

        let moveResult = Movement.move "" "" item c
        
        Ok === moveResult
        c 
        |> Container.inventory 
        |> List.head === item