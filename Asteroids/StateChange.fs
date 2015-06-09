module StateChange

open Physics
open Domain

let update state change =
    match change with
    | StartAccelerate a -> {state with Ship = state.Ship |> ShipChange.accelerate a}
    | StopAccelerate    -> {state with Ship = state.Ship |> ShipChange.coast}
    | StartRotate spin  -> {state with Ship = state.Ship |> ShipChange.updateSpin spin}
    | StopRotate        -> {state with Ship = state.Ship |> ShipChange.updateSpin 0.0<degree>}
    | Tick t            -> {state with Ship = state.Ship |> ShipChange.move t}
    | EndGame           -> {state with Running = Stop}
    | NoChange          -> state