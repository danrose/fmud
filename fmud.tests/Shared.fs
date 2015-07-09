namespace fmud.tests

open NUnit.Framework
open fmud
open System

[<AutoOpen>]
module TestHelper =
    let (===) (x:obj) (y:obj) =
        Assert.AreEqual(y, x)

    let (==) (x:'a) (y:'a) = Assert.AreEqual(y, x)
