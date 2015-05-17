module Render

open OpenTK.Graphics.OpenGL
open System.Drawing

open Domain

let Z = 4.0
let draw f = f

let triangle coloredCorners =
    PrimitiveType.Triangles |> GL.Begin
    coloredCorners
    |> List.iter (fun (c: Color, p: Geometry.Point) -> GL.Color3(c); GL.Vertex3(p.X, p.Y, Z))
    GL.End()

let dot (color: Color) (position: Geometry.Point) =
    PrimitiveType.Points |> GL.Begin
    GL.Color3(color)
    GL.Vertex3(position.X, position.Y, Z)
    GL.End()

let ship (s: Ship) =
    s.Verticies |> draw triangle
    s.Position |> draw dot Color.White