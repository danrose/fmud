namespace fmud.tests

open NUnit.Framework
open fmud

[<TestFixture>]
type Containers() = 
    let (===) (x:obj) (y:obj) =
        Assert.AreEqual(x, y)

    [<Test>]
    member this.EmptyContainer() =
        let c = Container.empty
        Assert.IsEmpty(c |> Container.inventory)