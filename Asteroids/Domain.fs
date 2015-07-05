module Domain

open System.Drawing

open Physics
open Asteroid

type Ship = { 
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
    Running : GameRunning
    Ship : Ship
    Asteroids: Asteroid.T list
}

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

let asteroids = Asteroid.createMany 5 (fun () -> Asteroid.createIrregular 9 0.1 0.5)

let initialState = { 
    Running = Continue
    Asteroids = asteroids
    Ship = 
    { 
        Position = {X = 0.0; Y = 0.0;}
        Thrust = None
        Velocity = {Direction = 90.0<degree>; Magnitude = 0.0}
        Orientation = 90.0<degree> 
        Spin = 0.0<degree>
    }
}