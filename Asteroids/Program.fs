module Program

open System

open OpenTK
open OpenTK.Graphics
open OpenTK.Input

[<EntryPoint>]
let main _ = 
    use game = new GameWindow(800, 600, GraphicsMode.Default, "Asteroids")
            
    let print x = printfn "%A" x

    let mapRandom x = 
        let rnd = new Random();
        rnd.Next(10)

    let checkEven x = 
        x % 2 = 0

    let printSingleIf predicate event =
        event
        |> Observable.map mapRandom
        |> Observable.filter predicate
        |> Observable.subscribe print

    use mouseDownSub    = game.MouseDown |> printSingleIf checkEven
    use keyDownSub      = game.KeyDown |> printSingleIf checkEven

    game.Run(60.0)

    0 

