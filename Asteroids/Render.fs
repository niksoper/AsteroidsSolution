module Render

open Domain

open OpenTK
open OpenTK.Graphics
open OpenTK.Graphics.OpenGL
open OpenTK.Input

open System.Drawing

let triangle corners =
    PrimitiveType.Triangles |> GL.Begin
    corners
    |> List.zip [Color.Red; Color.Red; Color.Blue]
    |> List.iter (fun (c: Color, p: Geometry.Point) -> GL.Color3(c); GL.Vertex3(p.X, p.Y, 4.))
    GL.End()