module Physics

open System

[<Measure>] type degree
[<Measure>] type radian
[<Measure>] type second

type Point = { X: float; Y: float }
type Vector = { Direction: float<degree>; Magnitude: float }

let degreesPerRadian = 57.2957795131<degree/radian>
let radians degrees = degrees / degreesPerRadian

let cos (angle: float<degree>) = Math.Cos(float (radians angle))
let sin (angle: float<degree>) = Math.Sin(float (radians angle))

let rotate length x y angle =
    let dx =  length * (cos angle)
    let dy =  length * (sin angle)
    (x + dx, y + dy)

let updatePosition v pos = 
    rotate v.Magnitude pos.X pos.Y v.Direction

let private constrain min max m =
    if m > max then max
    elif m < min then min
    else m

let updateVelocity a v = 
    let m = v.Magnitude + a

    {v with Magnitude = m |> constrain 0.0 0.07}