namespace fmud.tests

open NUnit.Framework
open fmud

[<TestFixture>]
type SetOperators() = 
    let (===) (x:obj) (y:obj) =
        Assert.AreEqual(x, y)

    [<Test>]
    member this.SimpleAddEntry() =
        let set = Set.empty.Add("abc").Add("def");
        set @+ "foo" === Set.empty.Add("abc").Add("def").Add("foo")

    [<Test>]
    member this.AddMultipleEntries() =
        let set = Set.empty.Add("abc").Add("def");
        set @+ "foo bar ping" === Set.empty.Add("abc").Add("def").Add("foo").Add("bar").Add("ping")

    [<Test>]
    member this.SimpleRemoveEntry() =
        let set = Set.empty.Add("abc").Add("def");
        set @- "abc" === Set.empty.Add("def")

    [<Test>]
    member this.RemoveMultipleEntries() =
        let set = Set.empty.Add("abc").Add("def");
        set @- "def abc" === Set.empty
