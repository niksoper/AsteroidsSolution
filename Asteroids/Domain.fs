module Domain

open System.Drawing

open Geometry
      
type Ship = { Position: Point; Velocity: Vector } with
    member this.Verticies = 
        let points = 
            [{ X = this.Position.X - 0.1;   Y = this.Position.Y - 0.1 }
             { X = this.Position.X + 0.1;  Y = this.Position.Y - 0.1 }
             { X = this.Position.X;        Y = this.Position.Y + 0.1 }]
        let colors = [Color.Red; Color.Red; Color.Blue]
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
    | NoChange

let initialState = { 
    Running = Continue
    Ship = { Position = {X = 0.0; Y = 0.0;}; Velocity = {Dx = 0.0; Dy = 0.0} }
}