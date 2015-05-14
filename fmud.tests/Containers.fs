namespace fmud.tests

open NUnit.Framework
open fmud

[<TestFixture>]
type Containers() = 
    let (===) (x:obj) (y:obj) =
        Assert.AreEqual(x, y)

    let itemName name item =
        item |> GameObject.setName (Description name)
        item |> GameObject.setShort (Description name)

    [<Test>]
    member this.EmptyContainer() =
        let c = Container.empty()
        Assert.IsEmpty(c |> Container.inventory)

    [<Test>]
    member this.AddItem() =
        let c = Container.empty()
        let item = { new MobileObject(System.Guid.NewGuid()) with member x.ToString() = base.ToString() }
        item |> itemName "ball"

        let moveResult = Movement.moveSilently item c
        
        Ok === moveResult
        c 
        |> Container.inventory 
        |> List.head === item

    [<Test>]
    member this.AddItems() =
        let c = Container.empty()

        let item1 = { new MobileObject(System.Guid.NewGuid()) with member x.ToString() = base.ToString() }
        item1 |> itemName "ball"

        let item2 = { new MobileObject(System.Guid.NewGuid()) with member x.ToString() = base.ToString() }
        item2 |> itemName "goat"

        let moveResult1 = Movement.moveSilently item1 c
        let moveResult2 = Movement.moveSilently item2 c
        
        Ok === moveResult1
        Ok === moveResult2
        2 === Container.count c
        
        match c |> Container.inventory with
        | [item1;item2] -> true
        | _ -> failwith "List did not match."
        |> ignore

    [<Test>]
    member this.RemoveItem() =
        let c = Container.empty()

        let item1 = { new MobileObject(System.Guid.NewGuid()) with member x.ToString() = base.ToString() }
        item1 |> itemName "ball"

        let item2 = { new MobileObject(System.Guid.NewGuid()) with member x.ToString() = base.ToString() }
        item2 |> itemName "goat"

        let moveResult1 = Movement.moveSilently item1 c
        let moveResult2 = Movement.moveSilently item2 c
        
        Ok === moveResult1
        Ok === moveResult2
        2 === Container.count c
        
        c |> Container.remove item1 |> ignore

        1 === Container.count c

        c 
        |> Container.inventory 
        |> List.head === item2