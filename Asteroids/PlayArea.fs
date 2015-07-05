module PlayArea

open Physics

let private xMax = 2.2
let private yMax = 1.7

let wrap pos =
    let xBound = 
        if pos.X > xMax then -xMax
        elif pos.X < -xMax then xMax
        else pos.X
    let yBound = 
        if pos.Y > yMax then -yMax
        elif pos.Y < -yMax then yMax
        else pos.Y
    {X = xBound; Y = yBound}

let randomPoint() = 
    {
        X = Random.between -xMax xMax
        Y = Random.between -yMax yMax    
    }