open Suave
open app
open System
open System.Net
open System.IO

[<EntryPoint>]
let main argv = 
    let publicDirectory = argv.[0]
    let nodeModulesDirectory = argv.[1]
    let port = argv.[2] |> UInt16.Parse

    Console.WriteLine(publicDirectory)

    startWebServer { defaultConfig with 
                        homeFolder = Some publicDirectory
                        bindings = [ HttpBinding.mk HTTP IPAddress.Any port ] } (app nodeModulesDirectory)
    printfn "%A" argv
    0 // return an integer exit code
