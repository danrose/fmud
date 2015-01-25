// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.

open Microsoft.AspNet.SignalR
open Microsoft.AspNet.SignalR.Client
open System

[<EntryPoint>]
let main argv = 
    let url = "http://127.0.0.1:8088/"
    
    let connection = new HubConnection(url)
    let proxy = connection.CreateHubProxy("GameHub")
    let callback = new Action<string>(fun msg -> printfn "Received %s" msg)
    proxy.On<string>("Print", callback) |> ignore
    Async.AwaitIAsyncResult(connection.Start())
    printfn "Client connected to %s" url
    let mutable input = ""
    while true do
        input <- Console.ReadLine()
        proxy.Invoke("AcceptCommand", input) |> ignore
    0