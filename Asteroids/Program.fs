module Program

open System
open System.Drawing

open OpenTK
open OpenTK.Graphics
open OpenTK.Input

[<EntryPoint>]
let main _ = 

    use game = new GameWindow(800, 600, GraphicsMode.Default, "Observable")

    let higherOrLower = function
        | a,b when a > b -> "Higher!"
        | a,b when a < b -> "Lower!"
        | _ -> "Same"

    game.MouseDown
    |> Observable.map (fun args -> args.Y)
    |> Observable.pairwise
    |> Observable.map higherOrLower
    |> Observable.add (fun msg -> printfn "%s" msg)
    
    game.Run(60.0)

    0 

