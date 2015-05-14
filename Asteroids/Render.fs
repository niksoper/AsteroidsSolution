module Render

open OpenTK.Graphics.OpenGL

open System.Drawing

let draw f = f

let triangle coloredCorners =
    PrimitiveType.Triangles |> GL.Begin
    coloredCorners
    |> List.iter (fun (c: Color, p: Geometry.Point) -> GL.Color3(c); GL.Vertex3(p.X, p.Y, 4.))
    GL.End()