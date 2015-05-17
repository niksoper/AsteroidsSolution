﻿module Program

open System

open OpenTK
open OpenTK.Graphics
open OpenTK.Graphics.OpenGL
open OpenTK.Input

open Domain
open Geometry
open Render

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

    let updateFrame (state :GameState) =
        match state.Running with 
        | Continue -> ()
        | Stop -> game.Exit()

    let renderFrame (state: GameState)  =

        //OpenGL Stuff to set view
        GL.Clear(ClearBufferMask.ColorBufferBit ||| ClearBufferMask.DepthBufferBit)
        let mutable modelview = Matrix4.LookAt(Vector3.Zero, Vector3.UnitZ, Vector3.UnitY)
        GL.MatrixMode(MatrixMode.Modelview)
        GL.LoadMatrix(&modelview)

        state.Ship |> draw ship

        // Game is double buffered
        game.SwapBuffers()

    //Handle keydownEvents and transform them into state changes 
    //Hint (To get the best behaviour, you may need to deal with key up, etc events)
    let keyDown (args: KeyboardKeyEventArgs) =
        match args.Key with
        | Key.Escape ->  UserStateChange.EndGame
        | Key.Up -> UserStateChange.ChangePosition {X = 0.0; Y = Domain.Settings.MoveSpeed}
        | Key.Down -> UserStateChange.ChangePosition {X = 0.0; Y = -Domain.Settings.MoveSpeed}
        | Key.Right -> UserStateChange.Rotate Domain.Settings.RotateSpeed
        | Key.Left -> UserStateChange.Rotate -Domain.Settings.RotateSpeed
        | _ -> UserStateChange.NoChange

    let updateGameState (state: GameState)  change = 
        match change with 
        | ChangePosition posChange -> 
            let pos = state.Ship.Position
            let newPos = {X = pos.X + posChange.X; Y = pos.Y + posChange.Y}
            printfn "%A" newPos // Delete this line: This is only to show bounds of coordinates. 
            let newShip = {state.Ship with Position = newPos}
            {state with Ship = newShip}
        | Rotate rotation ->
            let newOrientation = (state.Ship.Orientation + rotation) % 360.0<degree>
            let newShip = { state.Ship with Orientation = newOrientation}
            printfn "%A" newShip.Orientation
            {state with Ship = newShip}
        | EndGame -> {state with Running=Stop}
        | NoChange -> state

    use loadSubscription = game.Load.Subscribe (WindowHandlers.load game)
    use resizeSubscription = game.Resize.Subscribe (WindowHandlers.resize game)   

    (*Below, the game state is being stored in a reference cell instead of being passed through the observable functions
        The reason is that my original approach that passed the state on directly caused a memory leak. 
        I'm not sure if it was due to my original code, or an issue with the library, but this a simple alternative that means the code is nearly the same,
        with a little local mutability.
        
        Also, reference cell is used instead of a mutable value because mutable values cannot be captured by lambdas. 
        For a longer explaination see: https://lorgonblog.wordpress.com/2008/11/12/on-lambdas-capture-and-mutability/ 
        Also msdn reference : https://msdn.microsoft.com/en-us/library/dd233186.aspx*)
    let currentGameState = ref Domain.initialState

    use updateGameStateSub = 
        game.KeyDown
        |> Observable.map keyDown
        |> Observable.scan updateGameState Domain.initialState 
        |> Observable.subscribe (fun state -> currentGameState := state)

    use renderFrameSub = 
        game.RenderFrame
        |> Observable.subscribe(fun _ -> renderFrame !currentGameState)

    use updateFrameSub = 
        game.UpdateFrame
        |> Observable.subscribe(fun _ -> updateFrame !currentGameState)
        
    game.Run(60.0)

    0 
