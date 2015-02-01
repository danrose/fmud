// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.
open Owin
open Microsoft.AspNet.SignalR
open Microsoft.AspNet.SignalR.Hubs
open Microsoft.Owin.Hosting
open Microsoft.Owin.Cors
open System
open Microsoft.Owin

type Startup() =
    member this.Configuration(app:IAppBuilder) =
        app.UseCors(CorsOptions.AllowAll) |> ignore
        app.MapSignalR() |> ignore

[<assembly: OwinStartup(typeof<Startup>)>]
do ()

type GameHub() =
    inherit Hub()
    member this.AcceptCommand(what: string) =
        let all = this.Clients.All :?> ClientProxy
        printfn "Received '%s' from %A" what this.Context.ConnectionId
        all.Invoke("Print", "Huh? " + what)


[<EntryPoint>]
let main argv = 
    let url = "http://127.0.0.1:8088/"
    use app = WebApp.Start(url)
    printfn "Started server on %s" url
    let mutable input = ""
    while(input <> "quit") do
        
        input <- Console.ReadLine()
        
    0
