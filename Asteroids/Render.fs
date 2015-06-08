module Render

open OpenTK.Graphics.OpenGL
open System.Drawing

open Domain
open Geometry

let private Z = 4.0

let private triangle coloredCorners =
    PrimitiveType.Triangles |> GL.Begin
    coloredCorners
    |> List.iter (fun (c: Color, p: Geometry.Point) -> GL.Color3(c); GL.Vertex3(p.X, p.Y, Z))
    GL.End()

let private dot (color: Color) (position: Geometry.Point) =
    PrimitiveType.Points |> GL.Begin
    GL.Color3(color)
    GL.Vertex3(position.X, position.Y, Z)
    GL.End()

let private line (color: Color) (position: Geometry.Point * Geometry.Point) =
    let p1, p2 = position
    PrimitiveType.Lines |> GL.Begin
    GL.Color3(color)
    GL.Vertex3(p1.X, p1.Y, Z)
    GL.Vertex3(p2.X, p2.Y, Z)
    GL.End()

let ship (s: Ship) =
    s.Verticies |> triangle
    s.Position |> dot Color.White
    match s.Thrust with
    | None -> ()
    | Some _ -> 
        let endPoint = s.Position |> Geometry.rotatePoint 0.1 (s.Orientation - 90.0<degree>)
        line Color.Red (s.Position, endPoint)