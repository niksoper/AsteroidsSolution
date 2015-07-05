module StateChange

open Physics
open Domain

let tick t state =
    let newShip = state.Ship |> ShipChange.move t
    let newAsteroids = state.Asteroids |> Asteroid.move
    {state with 
        Ship = newShip
        Asteroids = newAsteroids}

let update state change =
    match change with
    | StartAccelerate a -> {state with Ship = state.Ship |> ShipChange.accelerate a}
    | StopAccelerate    -> {state with Ship = state.Ship |> ShipChange.coast}
    | StartRotate spin  -> {state with Ship = state.Ship |> ShipChange.updateSpin spin}
    | StopRotate        -> {state with Ship = state.Ship |> ShipChange.updateSpin 0.0<degree>}
    | EndGame           -> {state with Running = Stop}
    | Tick t            -> state |> tick t
    | NoChange          -> state