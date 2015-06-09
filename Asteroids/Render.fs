module Render

open OpenTK.Graphics.OpenGL
open System.Drawing

open Physics
open Domain

let private Z = 4.0

let private triangle coloredCorners =
    PrimitiveType.Triangles |> GL.Begin
    coloredCorners
    |> List.iter (fun (c: Color, p: Physics.Point) -> GL.Color3(c); GL.Vertex3(p.X, p.Y, Z))
    GL.End()

let private dot (color: Color) (position: Physics.Point) =
    PrimitiveType.Points |> GL.Begin
    GL.Color3(color)
    GL.Vertex3(position.X, position.Y, Z)
    GL.End()

let private line (color: Color) (position: Physics.Point * Physics.Point) =
    let p1, p2 = position
    PrimitiveType.Lines |> GL.Begin
    GL.Color3(color)
    GL.Vertex3(p1.X, p1.Y, Z)
    GL.Vertex3(p2.X, p2.Y, Z)
    GL.End()

let private thruster (color: Color) s =
    match s.Thrust with
    | Some a ->
        let x = s.Position.X
        let y = s.Position.Y
        let lineLength = 0.2
        let lineAngle = 
            if a > 0.0 then s.Orientation - 180.0<degree>
            else s.Orientation
        let ex,ey = Physics.rotate lineLength x y lineAngle
        let lineEnd = {X = ex; Y = ey}
        line color (s.Position, lineEnd)
    | None -> ()

let ship (s: Ship) =
    s.Verticies |> triangle
    s.Position |> dot Color.White
    s |> thruster Color.Gold