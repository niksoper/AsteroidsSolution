module Program

open System
open System.Drawing

open OpenTK
open OpenTK.Graphics
open OpenTK.Input

[<EntryPoint>]
let main _ = 

    use game = new GameWindow(800, 600, GraphicsMode.Default, "Observable")
    
    let mapRandom x = 
        let rnd = new Random();
        rnd.Next(10)

    let checkEven x = 
        x % 2 = 0

    let evens, odds = 
        game.MouseDown
        |> Observable.map mapRandom
        |> Observable.partition checkEven

    evens |> Observable.add (fun even -> printfn "%d!!! We love even numbers :=D" even)
    odds |> Observable.add (fun odd -> printfn "%d. How sad, an odd number :-(" odd)
    
    game.Run(60.0)

    0 

