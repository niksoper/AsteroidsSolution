module Ship

open System.Drawing

open Physics

type T = { 
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

let updateVelocity a v = 
    let m = v.Magnitude + a
    {v with Magnitude = m |> constrain -0.02 0.03}

let private applyThrust a (ship: T) =
    let thrustVelocity = {ship.Velocity with Direction = ship.Orientation}
    updateVelocity a thrustVelocity
    
let accelerate a ship = 
    {ship with Thrust = Some(a)}

let coast ship = 
    {ship with Thrust = None}

let updateSpin newSpin ship = 
    {ship with Spin = newSpin}

let move t ship =
    let newVelocity = 
        match ship.Thrust with
        | Some a -> ship |> applyThrust a
        | None   -> ship.Velocity

    let constrainedNewPos = 
        ship.Position
        |> updatePosition newVelocity
        |> PlayArea.wrap

    let newOrientation = ship.Orientation + ship.Spin

    {ship with Orientation = newOrientation; Position = constrainedNewPos; Velocity = newVelocity}  
