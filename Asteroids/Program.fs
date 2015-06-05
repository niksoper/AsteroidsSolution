module Program

open System

open OpenTK
open OpenTK.Graphics

[<EntryPoint>]
let main _ = 
    use game = new GameWindow(800, 600, GraphicsMode.Default, "Asteroids")
            
    let print x = printfn "%A" x
    let mapSingle = (fun _ -> 1)

    let printSingleWhen event =
        event
        |> Observable.map mapSingle
        |> Observable.subscribe print

    use mouseDownSub    = printSingleWhen game.MouseDown
    use keyDownSub      = printSingleWhen game.KeyDown

    game.Run(60.0)

    0 

