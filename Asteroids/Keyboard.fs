module Keyboard

open OpenTK
open OpenTK.Input

open Physics
open Game

let rotateSpeed     = 9.0<degree>
let acceleration    = 0.001
let braking         = -0.0008

let private keyDownStateChange (keyArgs: KeyboardKeyEventArgs) =
    match keyArgs.Key with
    | Key.Up        -> StartAccelerate acceleration
    | Key.Down      -> StartAccelerate braking
    | Key.Right     -> StartRotate rotateSpeed
    | Key.Left      -> StartRotate -rotateSpeed
    | Key.Escape    -> EndGame
    | _             -> NoChange

let private keyUpStateChange (keyArgs: KeyboardKeyEventArgs) =
    match keyArgs.Key with
    | Key.Up    -> StopAccelerate
    | Key.Down  -> StopAccelerate
    | Key.Left  -> StopRotate
    | Key.Right -> StopRotate
    | _         -> NoChange

let stream (window: GameWindow) =
    let keyDown  = window.KeyDown |> Observable.map keyDownStateChange
    let keyUp    = window.KeyUp   |> Observable.map keyUpStateChange
    Observable.merge keyDown keyUp