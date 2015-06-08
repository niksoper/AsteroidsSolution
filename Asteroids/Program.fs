module Program

open System

open OpenTK
open OpenTK.Graphics
open OpenTK.Graphics.OpenGL
open OpenTK.Input

open Domain
open Geometry

(*
    Note: While we are calling this an Asteroids clone, we may deviate from it...

    References: 
    http://www.opentk.com/
    Reactive Programming intro with observables and f# http://fsharpforfunandprofit.com/posts/concurrency-reactive/ 

    Tasks:
        - Refactor the open GL Program.fs 
        - Reach functionality goal of : 
            Deal with basic ship movement: 
                1. Left and Right arrow should rotate the ship
                2. Forward arrow should cause acceleration
                3. Backwards arrow should cause deceleration
                4. Asteroids style position wrap around when an object leaves the screen. PLay the asteroids game if you cant remember it!
        - Test as much as possible!
*)

[<EntryPoint>]
let main _ = 
    use game = new GameWindow(800, 600, GraphicsMode.Default, "Asteroids")

    let checkStillRunning (state :GameState) =
        match state.Running with 
        | Continue -> ()
        | Stop -> game.Exit()

    let renderFrame (state: GameState)  =

        //OpenGL Stuff to set view
        GL.Clear(ClearBufferMask.ColorBufferBit ||| ClearBufferMask.DepthBufferBit)
        let mutable modelview = Matrix4.LookAt(Vector3.Zero, Vector3.UnitZ, Vector3.UnitY)
        GL.MatrixMode(MatrixMode.Modelview)
        GL.LoadMatrix(&modelview)

        state.Ship |> Render.ship

        // Game is double buffered
        game.SwapBuffers()

    let keyDown   = game.KeyDown |> Observable.map Keyboard.keyDownStateChange
    let keyUp     = game.KeyUp |> Observable.map Keyboard.keyUpStateChange
    
    let keyboard  = Observable.merge keyDown keyUp

    let tick      = game.UpdateFrame |> Observable.map (fun t -> Tick (t.Time * 1.0<second>))

    let keyboardAndTime = Observable.merge keyboard tick

    game.Load   |> Observable.add (WindowHandlers.load game)
    game.Resize |> Observable.add (WindowHandlers.resize game)   

    let updateGameState (state: GameState) change =
        match change with
        | StartAccelerate a -> {state with Ship = state.Ship |> ShipChange.accelerate a}
        | StopAccelerate    -> state
        | StartRotate spin  -> {state with Ship = state.Ship |> ShipChange.updateSpin spin}
        | StopRotate        -> {state with Ship = state.Ship |> ShipChange.updateSpin 0.0<degree>}
        | Tick t            -> {state with Ship = state.Ship |> ShipChange.move t}
        | EndGame           -> {state with Running = Stop}
        | NoChange          -> state


    let currentGameState = ref Domain.initialState

    keyboardAndTime
    |> Observable.scan updateGameState Domain.initialState 
    |> Observable.add (fun state -> currentGameState := state)

    game.RenderFrame
    |> Observable.add (fun _ -> renderFrame !currentGameState)

    game.UpdateFrame
    |> Observable.add (fun _ -> checkStillRunning !currentGameState)
        
    game.Run(60.0)

    0 

