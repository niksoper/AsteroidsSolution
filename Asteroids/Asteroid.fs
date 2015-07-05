module Asteroid

open Physics

type T = {
        Position: Point
        Velocity: Vector
        Shape: Vector list    
    }

let createRegular points radius = {
    Position = { X = 0.0; Y = 0.0 }
    Velocity = { Direction = 0.0<degree>; Magnitude = 0.0 }
    Shape = Polygon.regular points radius
}

let createIrregular points minRadius maxRadius = {
    Position = { X = 0.0; Y = 0.0 }
    Velocity = { Direction = 0.0<degree>; Magnitude = 0.0 }
    Shape = Polygon.irregular points minRadius maxRadius
}

let randomVelocity() = {
    Direction = Random.direction()
    Magnitude = Random.between 0.005 0.03
}
    
let createMany n create = 
    [for i in 1 .. n do yield create()]
    |> List.map (fun a -> {
                            a with 
                                Position = PlayArea.randomPoint()
                                Velocity = randomVelocity()
                                Shape = a.Shape |> Polygon.rotate(Random.direction())
                          })

let move asteroids =
    asteroids 
    |> List.map (fun a -> {a with Position = a.Position |> Physics.updatePosition a.Velocity |> PlayArea.constrain})
