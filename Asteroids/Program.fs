module Program

open System

open OpenTK
open OpenTK.Graphics

open Domain
open Physics

[<EntryPoint>]
let main _ = 
    use game = new GameWindow(800, 600, GraphicsMode.Default, "Asteroids")

    let checkStillRunning state =
        match state.Running with 
        | Continue -> ()
        | Stop -> game.Exit()

    let renderFrame (state: GameState)  =
        WindowHandlers.clearAndSetMatrix()
        state.Ship |> Render.ship
        game.SwapBuffers()

    game.Load   |> Observable.add (WindowHandlers.load game)
    game.Resize |> Observable.add (WindowHandlers.resize game)   

    let currentGameState = ref Domain.initialState

    let keyboardStream = game |> Keyboard.stream
    let tick = game.UpdateFrame |> Observable.map (fun t -> Tick (t.Time * 1.0<second>))

    keyboardStream
    |> Observable.merge tick
    |> Observable.scan StateChange.update Domain.initialState 
    |> Observable.add (fun state -> currentGameState := state)

    game.RenderFrame
    |> Observable.add (fun _ -> renderFrame !currentGameState)

    game.UpdateFrame
    |> Observable.add (fun _ -> checkStillRunning !currentGameState)
        
    game.Run(60.0)

    0 

