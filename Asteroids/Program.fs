module Program

open System

open OpenTK
open OpenTK.Graphics

open Game
open Physics

[<EntryPoint>]
let main _ = 
    use window = new GameWindow(800, 600, GraphicsMode.Default, "Asteroids")

    let checkStillRunning state =
        match state.Running with 
        | Continue -> ()
        | Stop -> window.Exit()

    let renderFrame (state: GameState)  =
        WindowHandlers.clearAndSetMatrix()
        state.Ship |> Render.ship
        state.Asteroids |> Seq.iter Render.asteroid
        window.SwapBuffers()

    window.Load   |> Observable.add (WindowHandlers.load window)
    window.Resize |> Observable.add (WindowHandlers.resize window)   

    let currentGameState = ref Game.initialState

    let keyboardStream = window |> Keyboard.stream
    let tick = window.UpdateFrame |> Observable.map (fun t -> Tick (t.Time * 1.0<second>))

    keyboardStream
    |> Observable.merge tick
    |> Observable.scan StateChange.update Game.initialState 
    |> Observable.add (fun state -> currentGameState := state)

    window.RenderFrame
    |> Observable.add (fun _ -> renderFrame !currentGameState)

    window.UpdateFrame
    |> Observable.add (fun _ -> checkStillRunning !currentGameState)
        
    window.Run(60.0)

    0 

