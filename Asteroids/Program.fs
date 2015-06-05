module Program

open System

open OpenTK
open OpenTK.Graphics
open OpenTK.Input

[<EntryPoint>]
let main _ = 
    use game = new GameWindow(800, 600, GraphicsMode.Default, "Asteroids")
            
    let print x = printfn "%A" x
    let mapSingle x = 1

    let mapSingleWhen event =
        event
        |> Observable.map mapSingle

    let mouseDownStream = mapSingleWhen game.MouseDown
    let keyDownStream   = mapSingleWhen game.KeyDown

    let printCount stream =
        stream
        |> Observable.scan (+) 0
        |> Observable.add print

    printCount mouseDownStream 
    printCount keyDownStream 

    game.Run(60.0)

    0 

