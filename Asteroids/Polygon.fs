module Polygon

open Physics

let private make points createRadius =
    let a = 360 / points
    [ 
        for i in 1 .. points do
            yield { 
                Magnitude = createRadius()
                Direction = (i * a) |> float |> degree
            } 
    ]
            
let regular points radius = 
    make points (fun () -> radius)

let irregular points minRadius maxRadius = 
    make points (fun () -> Random.between minRadius maxRadius)

let rotate angle polygon = 
    polygon
    |> List.map (fun p -> {p with Direction = p.Direction + angle})