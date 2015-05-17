module Geometry

open System

[<Measure>] type degree
[<Measure>] type radian

type Point = { X: float; Y: float }
type Vector = { Dx: float; Dy: float }

let degreesPerRadian = 57.2957795131<degree/radian>
let radians degrees = degrees / degreesPerRadian

let cos (angle: float<radian>) = Math.Cos(float angle)
let sin (angle: float<radian>) = Math.Sin(float angle)

let rotatePoint length (angle: float<degree>) point =
    let angleR = radians angle
    let x =  length * (cos angleR)
    let y =  length * (sin angleR)
    { X = point.X + x; Y = point.Y + y}