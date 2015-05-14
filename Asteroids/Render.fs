module Render

open OpenTK
open OpenTK.Graphics
open OpenTK.Graphics.OpenGL
open OpenTK.Input

open Domain

let drawShip ship =
    // Draw triangle based on ship position
    (*Note the 4. (or 4.0) for the z coordinate of the vertices is 4, instead of zero because of the specific projection. 
        For now, simply keep it and abstract out the coordinates so that you can just use X and Y, while keeping Z contstant. 

        One other thing to note about the coordinates: The screen coordinate system is not between nice numbers. 
        I attempted to clean that up, but I've had no luck so far. 
        *) 

    let shipPos = ship.Position

    PrimitiveType.Triangles |> GL.Begin
    GL.Color3(1., 0., 0.); GL.Vertex3(shipPos.X + -0.1, shipPos.Y + -0.1, 4.) 
    GL.Color3(1., 0., 0.); GL.Vertex3(shipPos.X + 0.1, shipPos.Y + -0.1, 4.)
    GL.Color3(0.2, 0.9, 1.); GL.Vertex3(shipPos.X + 0., shipPos.Y + 0.1, 4.)
    GL.End()

    //Draw Ship Centre - Note: I've added this so you can see where the ship position is. 
    PrimitiveType.Points |> GL.Begin

    GL.Color3(1., 1., 1.); GL.Vertex3(shipPos.X, shipPos.Y, 4.) 
    GL.End()