module Keyboard

open OpenTK
open OpenTK.Input

open Physics
open Domain

let rotateSpeed     = 9.0<degree>
let acceleration    = 0.005
let braking         = -0.002

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

let stream (game: GameWindow) =
    let keyDown  = game.KeyDown |> Observable.map keyDownStateChange
    let keyUp    = game.KeyUp   |> Observable.map keyUpStateChange
    Observable.merge keyDown keyUp