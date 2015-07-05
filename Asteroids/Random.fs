module Random

open Physics

let between =
    let rand = new System.Random()
    (fun min max -> 
        let range = max - min
        min + (rand.NextDouble() * range)) 

let direction = 
    (fun () ->
        between 0.0 360.0
        |> degree)