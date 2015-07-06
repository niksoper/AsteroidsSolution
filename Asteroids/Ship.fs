module Ship

open System.Drawing

open Physics

type Bullet = {
    Position: Point
    Velocity: Vector
    Traveled: float
}

type T = { 
        Position: Point
        Thrust: float option
        Velocity: Vector
        Orientation: float<degree> 
        Spin: float<degree>
        Bullets: Bullet list
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

let shoot (ship:T) =
    let bullet: Bullet = {
        Velocity = { Magnitude = 0.07; Direction = ship.Orientation }
        Position = ship.Position
        Traveled = 0.0
    }
    {ship with Bullets = bullet :: ship.Bullets}

let moveBullet (bullet: Bullet) =
    let newPos = 
        bullet.Position 
        |> Physics.updatePosition bullet.Velocity 
        |> PlayArea.wrap
    let traveled = bullet.Traveled + bullet.Velocity.Magnitude
    {bullet with 
        Position = newPos
        Traveled = traveled}

let moveBullets ship =
    let newBullets = 
        ship.Bullets 
        |> List.map moveBullet
        |> List.filter (fun b -> b.Traveled < 4.0)
    {ship with Bullets = newBullets}