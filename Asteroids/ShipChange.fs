module ShipChange

open Geometry

open Domain

let private newVelocity thrust direction (u:Vector) =
    let x,y = rotate thrust u.Dx u.Dy direction
    {Dx = x; Dy = y}
    
let accelerate a ship = 
    {ship with Thrust = Some(a)}

let updateSpin newSpin ship = {ship with Spin = newSpin}

let move t ship =
    let p = ship.Position
    let v = ship.Velocity
    
    let newOrientation = ship.Orientation + ship.Spin
    let velocityChange = 
        match ship.Thrust with
        | Some a    -> newVelocity a newOrientation v
        | None      -> v

    print velocityChange

    let newV = {
        Dx = v.Dx + velocityChange.Dx
        Dy = v.Dy + velocityChange.Dy
    }

    let newPos = {X = p.X + newV.Dx; Y = p.Y + newV.Dy}
    {ship with Orientation = newOrientation; Position = newPos; Velocity = newV}  