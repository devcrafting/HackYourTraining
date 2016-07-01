#I "packages/FAKE/tools" //used for linux build
#r "packages/FAKE/tools/FakeLib.dll"

open Fake
open System
open System.IO
open System.Diagnostics

let build msbuild = 
    MSBuildHelper.MSBuildLoggers <- []
    msbuild "./build" "Build" [__SOURCE_DIRECTORY__ + "/HackYourTraining.fsproj"]

let buildDebug () = build MSBuildDebug
let buildRelease () = build MSBuildRelease

let runAndForget () = 
    fireAndForget (fun info -> 
        info.FileName <- "./build/HackYourTraining.exe"
        info.Arguments <- Path.Combine(__SOURCE_DIRECTORY__, "www") + " 8083")

let stop () = killProcess "HackYourTraining"

let reload = stop >> buildDebug >> ignore >> runAndForget

let waitUserStopRequest () = 
    () |> traceLine |> traceLine
    traceImportant "Press any key to stop."
    () |> traceLine |> traceLine

    System.Console.ReadLine() |> ignore
    
let watchSource action =
    !! (__SOURCE_DIRECTORY__ </> "*.fs") 
        |> WatchChanges (fun _ -> action ())
        |> ignore

let reloadOnChange () =
    watchSource reload

let askStop = waitUserStopRequest >> stop

let buildDocker () = 
    fireAndForget (fun info -> 
        info.FileName <- "docker"
        info.Arguments <- "build -t hackyourtraining .")

Target "build" (buildDebug >> ignore)

Target "run" (runAndForget >> askStop)

Target "watch" (runAndForget >> reloadOnChange >> askStop)

Target "publish" (buildRelease >> ignore >> buildDocker >> ignore)

"build"
    ==> "run"

"build"
    ==> "watch"

RunTargetOrDefault "build"