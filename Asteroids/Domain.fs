module Domain

open System.Drawing

open Geometry

module Settings =
    let RotateSpeed = 9.0<degree>
    let MoveSpeed = 0.05

let print a = printfn "%A" a

//type Thrust =
//    | Forward of float
//    | None

type Ship = { 
        Position: Point
        Thrust: float option
        Velocity: Vector
        Orientation: float<degree> 
        Spin: float<degree>
    } with

    member this.Verticies = 
        let rotate = rotatePoint 0.1
        let angleOffset = this.Orientation
        let points = 
            [this.Position |> (rotate <| 0.0<degree> + angleOffset)
             this.Position |> (rotate <| 120.0<degree> + angleOffset)
             this.Position |> (rotate <| -120.0<degree> + angleOffset)]
        let colors = [Color.Blue; Color.Red; Color.Red]
        List.zip colors points

type GameRunning =
    | Continue
    | Stop

type GameState = {
    Time: double
    Running : GameRunning
    Ship : Ship
}

//For state changes based on user events. 
//Probably a good idea to have a seperate union for state changes base on internal game events
type UserStateChange = 
    | EndGame
    | ChangePosition of Point 
    | Rotate of float<degree>
    | SpeedUp
    | SlowDown
    | NoChange

type TriggerStateChange =
    | StartAccelerate of float
    | StopAccelerate
    | StartRotate of float<degree>
    | StopRotate
    | Tick of float<second>
    | EndGame
    | NoChange

let initialState = { 
    Time = 0.0
    Running = Continue
    Ship = 
    { 
        Position = {X = 0.0; Y = 0.0;}
        Thrust = None
        Velocity = {Dx = 0.0; Dy = 0.0}
        Orientation = 90.0<degree> 
        Spin = 0.0<degree>
    }
}