module ShipChange

open Physics

open Domain


let private constrain pos =
    let xMax = 2.2
    let yMax = 1.7
    let xBound = 
        if pos.X > xMax then -xMax
        elif pos.X < -xMax then xMax
        else pos.X
    let yBound = 
        if pos.Y > yMax then -yMax
        elif pos.Y < -yMax then yMax
        else pos.Y
    {X = xBound; Y = yBound}
    
let private applyThrust a ship =
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
        |> constrain

    let newOrientation = ship.Orientation + ship.Spin

    {ship with Orientation = newOrientation; Position = constrainedNewPos; Velocity = newVelocity}  
