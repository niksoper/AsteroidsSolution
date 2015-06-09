module ShipChange

open Physics

open Domain


let private constrain pos =
    let xBound = 
        if pos.X > Settings.XMax then -Settings.XMax
        elif pos.X < -Settings.XMax then Settings.XMax
        else pos.X
    let yBound = 
        if pos.Y > Settings.YMax then -Settings.YMax
        elif pos.Y < -Settings.YMax then Settings.YMax
        else pos.Y
    {X = xBound; Y = yBound}
    
let accelerate a ship = 
    {ship with Thrust = Some(a)}

let coast ship = 
    {ship with Thrust = None}

let updateSpin newSpin ship = {ship with Spin = newSpin}

let move t ship =
    let p = ship.Position
    let v = ship.Velocity
    
    let newV = 
        match ship.Thrust with
        | Some a    -> updateVelocity a v
        | None      -> v

    let x,y = p |> updatePosition newV
    let constrainedNewPos = {X = x; Y = y} |> constrain

    let newOrientation = ship.Orientation + ship.Spin

    {ship with Orientation = newOrientation; Position = constrainedNewPos; Velocity = newV}  
