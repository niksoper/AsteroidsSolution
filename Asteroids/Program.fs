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

    let printSingleWhen event =
        event
        |> Observable.map mapSingle
        |> Observable.subscribe print

    use mouseSub = printSingleWhen game.MouseDown
    use keySub = printSingleWhen game.KeyDown

    game.KeyDown
    |> Observable.filter (fun args -> args.Key = Key.Escape)
    |> Observable.add (fun _ -> 
        print "Disposing mouse down subscription"
        mouseSub.Dispose())

    game.Run(60.0)

    0 

