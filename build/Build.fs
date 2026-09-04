// ========================================================================================================
// === F# / Project fake build ==================================================================== 1.8.0 =
// --------------------------------------------------------------------------------------------------------
// Options:
//  - no-clean   - disables clean of dirs in the first step (required on CI)
//  - no-lint    - lint will be executed, but the result is not validated
// ========================================================================================================

open Fake.Core
open Fake.Core.TargetOperators
open Fake.IO.FileSystemOperators
open Fake.IO.Globbing.Operators

open ProjectBuild
open Utils

[<EntryPoint>]
let main args =
    args |> Args.init

    let spec =
        Spec.defaultConsoleApplication [
            OSXArm64
            Windows
            Linux
        ]
        |> Spec.mapConsoleApplication (fun spec -> {
            spec with
                RuntimeMode = RuntimeMode.AutoDetect
                PublishSingleFile = false
        })

    Targets.init {
        Project = {
            Name = "TUC.Console"
            Summary = "Console application for .tuc commands."
            Git = Git.init ()
        }
        Specs = spec
    }

    PlantUml.init spec.RuntimeIds

    args |> Args.run
