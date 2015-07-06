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
    Magnitude = Random.between 0.005 0.015
}
    
let createMany n create = 
    [for i in 1 .. n do yield create()]
    |> List.map (fun a -> {
                            a with 
                                Position = PlayArea.randomPoint()
                                Velocity = randomVelocity()
                                Shape = a.Shape |> Polygon.rotate(Random.direction())
                          })

let moveOne asteroid =
    let newPos = 
        asteroid.Position 
        |> Physics.updatePosition asteroid.Velocity 
        |> PlayArea.wrap
    {asteroid with Position = newPos}

let moveAll asteroids = 
    asteroids 
    |> List.map moveOne

let collision position objectSize asteroid =
    let asteroidSize = asteroid.Shape 
                    |> List.map (fun p -> p.Magnitude)
                    |> List.average
    let range = asteroidSize + objectSize
    Physics.inRange position asteroid.Position range