namespace fmud.tests

open NUnit.Framework
open fmud
open System

[<AutoOpen>]
module TestHelper =
    let (===) (x:obj) (y:obj) =
        Assert.AreEqual(x, y)
