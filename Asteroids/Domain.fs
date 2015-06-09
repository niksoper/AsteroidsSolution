module Domain

open System.Drawing

open Physics

module Settings =
    let RotateSpeed = 9.0<degree>
    let Acceleration = 0.005
    let XMax = 2.2
    let YMax = 1.7

let print a = printfn "%A" a

type Ship = { 
        Age: float<second>
        Position: Point
        Thrust: float option
        Velocity: Vector
        Orientation: float<degree> 
        Spin: float<degree>
    } with

    member this.Verticies = 
        let rotateAboutPosition = rotate 0.1 this.Position.X this.Position.Y
        let angleOffset = this.Orientation
        let points = 
            [rotateAboutPosition <| 0.0<degree> + angleOffset
             rotateAboutPosition <| 120.0<degree> + angleOffset
             rotateAboutPosition <| -120.0<degree> + angleOffset]
             |> List.map (fun (x, y) -> {X = x; Y = y})
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
        Age = 0.0<second>
        Position = {X = 0.0; Y = 0.0;}
        Thrust = None
        Velocity = {Direction = 90.0<degree>; Magnitude = 0.0}
        Orientation = 90.0<degree> 
        Spin = 0.0<degree>
    }
}