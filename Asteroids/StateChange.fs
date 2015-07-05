module StateChange

open Physics
open Game

let tick t state =
    let newShip = state.Ship |> Ship.move t
    let newAsteroids = state.Asteroids |> Asteroid.moveAll
    {state with 
        Ship = newShip
        Asteroids = newAsteroids}

let update state change =
    match change with
    | StartAccelerate a -> {state with Ship = state.Ship |> Ship.accelerate a}
    | StopAccelerate    -> {state with Ship = state.Ship |> Ship.coast}
    | StartRotate spin  -> {state with Ship = state.Ship |> Ship.updateSpin spin}
    | StopRotate        -> {state with Ship = state.Ship |> Ship.updateSpin 0.0<degree>}
    | EndGame           -> {state with Running = Stop}
    | Tick t            -> state |> tick t
    | NoChange          -> state