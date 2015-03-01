namespace fmud.tests

open NUnit.Framework
open fmud

[<TestFixture>]
type SetOperators() = 

    [<Test>]
    member this.SimpleAddEntry() =
        let set = Set.empty.Add("abc").Add("def");
        Assert.AreEqual(set @+ "foo", Set.empty.Add("abc").Add("def").Add("foo"))

    [<Test>]
    member this.AddMultipleEntries() =
        let set = Set.empty.Add("abc").Add("def");
        Assert.AreEqual(set @+ "foo bar ping", Set.empty.Add("abc").Add("def").Add("foo").Add("bar").Add("ping"))

    [<Test>]
    member this.SimpleRemoveEntry() =
        let set = Set.empty.Add("abc").Add("def");
        Assert.AreEqual(set @- "abc", Set.empty.Add("def"))

    [<Test>]
    member this.RemoveMultipleEntries() =
        let set = Set.empty.Add("abc").Add("def");
        Assert.AreEqual(set @- "def abc", Set.empty)
