module Game

open System.Drawing

open Physics
open Asteroid
open Ship

type GameRunning =
    | Continue
    | Stop

type GameState = {
    Running : GameRunning
    Ship : Ship.T
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
    | Shoot
    | Tick of float<second>
    | EndGame
    | NoChange

let initialState = { 
    Running = Continue
    Asteroids = Asteroid.createMany 5 (fun () -> Asteroid.createIrregular 9 0.03 0.3)
    Ship = 
    { 
        Position = {X = 0.0; Y = 0.0;}
        Thrust = None
        Velocity = {Direction = 90.0<degree>; Magnitude = 0.0}
        Orientation = 90.0<degree> 
        Spin = 0.0<degree>
        Bullets = []
    }
}