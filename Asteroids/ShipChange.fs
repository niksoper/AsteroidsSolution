module ShipChange

open Geometry

open Domain

let accelerate a ship = 
    {ship with Thrust = Some(a)}
    
let updateSpin newSpin ship = {ship with Spin = newSpin}

let move t ship =
    let newOrientation = ship.Orientation + ship.Spin
    let newPos = 
        match ship.Thrust with
        | Some a    -> rotatePoint a ship.Orientation ship.Position
        | None      -> ship.Position
    {ship with Orientation = newOrientation; Position = newPos}  