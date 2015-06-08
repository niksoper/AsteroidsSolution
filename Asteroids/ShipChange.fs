module ShipChange

open Geometry

open Domain

let private updatePosition v pos = {X = pos.X + v.Dx; Y = pos.Y + v.Dy}
let private updateVelocity a v = {Dx = v.Dx + a.Dx; Dy = v.Dy + a.Dy}

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

let private newVelocity thrust direction (u:Vector) =
    let x,y = rotate thrust u.Dx u.Dy direction
    {Dx = x; Dy = y}
    
let accelerate a ship = 
    {ship with Thrust = Some(a)}

let updateSpin newSpin ship = {ship with Spin = newSpin}

let move t ship =
    let p = ship.Position
    let v = ship.Velocity
    
    let velocityChange = 
        match ship.Thrust with
        | Some a    -> newVelocity a ship.Orientation v
        | None      -> v

    let newV = v |> updateVelocity velocityChange
    let newPos = p |> updatePosition newV
    let constrainednewPos = newPos |> constrain

    let newOrientation = ship.Orientation + ship.Spin

    {ship with Orientation = newOrientation; Position = constrainednewPos; Velocity = newV}  
