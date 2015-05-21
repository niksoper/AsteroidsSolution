module Domain

open System.Drawing

open Geometry

module Settings =
    let RotateSpeed = 15.0<degree>
    let MoveSpeed = 0.05

type Thrust =
    | Forward
    | None

type Ship = { 
        Position: Point
        Thrust: Thrust
        Velocity: Vector
        Orientation: float<degree> 
    } with

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

let initialState = { 
    Time = 0.0
    Running = Continue
    Ship = 
    { 
        Position = {X = 0.0; Y = 0.0;}
        Thrust = None
        Velocity = {Dx = 0.0; Dy = 0.0}
        Orientation = 0.0<degree> 
    }
}

module ShipChange =

    let move ship = 
        let pos = ship.Position
        let v = ship.Velocity
        let newPos = {X = pos.X + v.Dx; Y = pos.Y + v.Dy}
        //printfn "%A" newPos 
        {ship with Position = newPos}

    let thrust ship = {ship with Thrust = Forward}
    let reverse ship = {ship with Thrust = None}

    let rotate angle ship =
        let newOrientation = (ship.Orientation + angle) % 360.0<degree>
        //printfn "%A" newOrientation
        { ship with Orientation = newOrientation}

    let thrust' ship =
        let pos = ship.Position
        let v = {ship.Velocity with Dy = Settings.MoveSpeed}
        //printfn "%A" v
        {ship with Velocity = v}

    let transform (ship: Ship) = 
        match ship.Thrust with
        |  Forward -> 
            {ship with Velocity = {Dx = 0.0; Dy = Settings.MoveSpeed}}
            |> move
        | _ -> ship