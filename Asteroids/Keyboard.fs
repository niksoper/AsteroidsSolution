module Keyboard

open OpenTK.Input

open Domain

let keyDownStateChange (keyArgs: OpenTK.Input.KeyboardKeyEventArgs) =
    match keyArgs.Key with
    | Key.Up        -> StartAccelerate Settings.Acceleration
    | Key.Down      -> StartAccelerate Settings.Braking
    | Key.Right     -> StartRotate Settings.RotateSpeed
    | Key.Left      -> StartRotate -Settings.RotateSpeed
    | Key.Escape    -> EndGame
    | _             -> NoChange

let keyUpStateChange (keyArgs: OpenTK.Input.KeyboardKeyEventArgs) =
    match keyArgs.Key with
    | Key.Up    -> StopAccelerate
    | Key.Down  -> StopAccelerate
    | Key.Left  -> StopRotate
    | Key.Right -> StopRotate
    | _         -> NoChange

