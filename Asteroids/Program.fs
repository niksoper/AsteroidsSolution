module Program

open System
open System.Drawing

open OpenTK
open OpenTK.Graphics
open OpenTK.Input

[<EntryPoint>]
let main _ = 

    use game = new GameWindow(800, 600, GraphicsMode.Default, "Observable")
    
    let print x = printfn "%A" x

    let mapRandom x = 
        let rnd = new Random();
        rnd.Next(10)

    let checkEven = function
        | x when x % 2 = 0 -> Choice1Of2 <| sprintf "%d!!! We love even numbers :=D" x
        | x -> Choice2Of2 x

    let evens, odds = 
        game.MouseDown
        |> Observable.map mapRandom
        |> Observable.split checkEven

    evens |> Observable.add print
    odds |> Observable.add print
    
    game.Run(60.0)

    0 

