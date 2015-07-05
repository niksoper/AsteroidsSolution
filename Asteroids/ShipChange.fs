module ShipChange

open Physics

open Domain

let private applyThrust a (ship: Ship) =
    let thrustVelocity = {ship.Velocity with Direction = ship.Orientation}
    updateVelocity a thrustVelocity
    
let accelerate a ship = 
    {ship with Thrust = Some(a)}

let coast ship = 
    {ship with Thrust = None}

let updateSpin newSpin ship = 
    {ship with Spin = newSpin}

let move t ship =
    let newVelocity = 
        match ship.Thrust with
        | Some a -> ship |> applyThrust a
        | None   -> ship.Velocity

    let constrainedNewPos = 
        ship.Position
        |> updatePosition newVelocity
        |> PlayArea.constrain

    let newOrientation = ship.Orientation + ship.Spin

    {ship with Orientation = newOrientation; Position = constrainedNewPos; Velocity = newVelocity}  
