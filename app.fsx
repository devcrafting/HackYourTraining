#r "packages/Suave/lib/net40/Suave.dll"
#r "System.Data.Linq.dll"
#r "System.Data.dll"
#r "packages/FSharp.Data.TypeProviders/lib/net40/FSharp.Data.TypeProviders.dll"
#load "app.fs"

open System
open System.IO
open System.Net
open Suave
open app

startWebServer { defaultConfig with 
                        homeFolder = Some (Path.Combine(__SOURCE_DIRECTORY__, "www"))
                        bindings = [ HttpBinding.mk HTTP IPAddress.Loopback 8083us ] } (app (Path.Combine(__SOURCE_DIRECTORY__, "node_modules"))) 
