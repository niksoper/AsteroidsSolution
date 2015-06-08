module Geometry

open System

[<Measure>] type degree
[<Measure>] type radian
[<Measure>] type second

type Point = { X: float; Y: float }
type Vector = { Dx: float; Dy: float }

let degreesPerRadian = 57.2957795131<degree/radian>
let radians degrees = degrees / degreesPerRadian

let cos (angle: float<degree>) = Math.Cos(float (radians angle))
let sin (angle: float<degree>) = Math.Sin(float (radians angle))

let rotate length x y angle =
    let dx =  length * (cos angle)
    let dy =  length * (sin angle)
    (x + dx, y + dy)
