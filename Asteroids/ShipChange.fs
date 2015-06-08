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
    
    let xBound = 
        if newPos.X > Settings.XMax then -Settings.XMax
        elif newPos.X < -Settings.XMax then Settings.XMax
        else newPos.X

    let yBound = 
        if newPos.Y > Settings.YMax then -Settings.YMax
        elif newPos.Y < -Settings.YMax then Settings.YMax
        else newPos.Y

    let boundPos = {X = xBound; Y = yBound}

    {ship with Orientation = newOrientation; Position = boundPos}  