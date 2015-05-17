module Domain

open System.Drawing

open Geometry
      
module Settings =
    let RotateSpeed = 15.0<degree>
    let MoveSpeed = 0.05

type Ship = { Position: Point; Velocity: Vector; Orientation: float<degree> } with
    member this.Verticies = 
        let rotate = rotatePoint 0.1
        let angleOffset = this.Orientation + 90.0<degree>
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
    Running : GameRunning
    Ship : Ship
}

//For state changes based on user events. 
//Probably a good idea to have a seperate union for state changes base on internal game events
type UserStateChange = 
    | EndGame
    | ChangePosition of Point 
    | Rotate of float<degree>
    | NoChange

let initialState = { 
    Running = Continue
    Ship = { Position = {X = 0.0; Y = 0.0;}; Velocity = {Dx = 0.0; Dy = 0.0}; Orientation = 0.0<degree> }
}