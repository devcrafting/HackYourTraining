#r "../../packages/Suave/lib/net40/Suave.dll"
#r "System.Data.Linq.dll"
#r "System.Data.dll"
#r "../../packages/FSharp.Data.TypeProviders/lib/net40/FSharp.Data.TypeProviders.dll"
#load "app.fs"

open System
open System.IO
open System.Net
open Suave
open app

let rootDir = Path.Combine(__SOURCE_DIRECTORY__, "../..")

startWebServer { defaultConfig with 
                        homeFolder = Some (Path.Combine(rootDir, "build/www"))
                        bindings = [ HttpBinding.mk HTTP IPAddress.Loopback 8083us ] } (app (Path.Combine(rootDir, "node_modules"))) 
