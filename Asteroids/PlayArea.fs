module PlayArea

open Physics

let xMax = 2.2
let yMax = 1.7

let constrain pos =
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