namespace fmud.tests

open NUnit.Framework
open fmud

[<TestFixture>]
type Rooms() = 
    let (===) (x:obj) (y:obj) =
        Assert.AreEqual(x, y)

    [<Test>]
    member this.SimpleAddEntry() =
        let set = Set.empty.Add("abc").Add("def");
        set @+ "foo" === Set.empty.Add("abc").Add("def").Add("foo")