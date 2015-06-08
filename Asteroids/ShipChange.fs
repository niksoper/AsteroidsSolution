module ShipChange

open Geometry

open Domain

let accelerate a ship = 
    printfn "Accelerating at %f" a
    {ship with Thrust = Some(a)}
    
let updateSpin newSpin ship = {ship with Spin = newSpin}

let move t ship =
    let newOrientation = ship.Orientation + ship.Spin
    {ship with Orientation = newOrientation}  


//let move ship = 
//    let pos = ship.Position
//    let v = ship.Velocity
//    let newPos = {X = pos.X + v.Dx; Y = pos.Y + v.Dy}
//    //printfn "%A" newPos 
//    {ship with Position = newPos}
//
//let thrust ship = {ship with Thrust = Forward}
//let reverse ship = {ship with Thrust = None}
//
//let rotate angle ship =
//    let newOrientation = (ship.Orientation + angle) % 360.0<degree>
//    //printfn "%A" newOrientation
//    { ship with Orientation = newOrientation}
//
//let thrust' ship =
//    let pos = ship.Position
//    let v = {ship.Velocity with Dy = Settings.MoveSpeed}
//    //printfn "%A" v
//    {ship with Velocity = v}
//
//let transform (ship: Ship) = 
//    match ship.Thrust with
//    |  Forward -> 
//        {ship with Velocity = {Dx = 0.0; Dy = Settings.MoveSpeed}}
//        |> move
//    | _ -> ship