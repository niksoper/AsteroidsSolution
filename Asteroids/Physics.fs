module Physics

open System

[<Measure>] type degree
[<Measure>] type radian
[<Measure>] type second

type Point = { X: float; Y: float }
type Vector = { Direction: float<degree>; Magnitude: float }

let degree f = f * 1.0<degree> 

let degreesPerRadian = 57.2957795131<degree/radian>
let radians degrees = degrees / degreesPerRadian

let cos angle = Math.Cos(float (radians angle))
let sin angle = Math.Sin(float (radians angle))

let rotate length x y angle =
    let dx =  length * (cos angle)
    let dy =  length * (sin angle)
    (x + dx, y + dy)

let updatePosition v pos = 
    let x,y = rotate v.Magnitude pos.X pos.Y v.Direction
    {X = x; Y = y}

let constrain min max m =
    if m > max then max
    elif m < min then min
    else m

let inRange p1 p2 range =
    let dx = p1.X - p2.X
    let dy = p1.Y - p2.Y
    let distance = Math.Sqrt(dx*dx + dy*dy)
    distance <= range