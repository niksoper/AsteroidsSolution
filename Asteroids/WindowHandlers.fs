﻿module WindowHandlers

open System

open OpenTK
open OpenTK.Graphics.OpenGL

let load (game: GameWindow) _ =
    // Some game and OpenGL Setup
    game.VSync <- VSyncMode.On
    GL.Enable(EnableCap.Blend)
    GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.One)

let resize (game: GameWindow) _ = 
    //Setup of projection matrix for game
    GL.Viewport(game.ClientRectangle.X, game.ClientRectangle.Y, game.ClientRectangle.Width, game.ClientRectangle.Height)
    let mutable projection = Matrix4.CreatePerspectiveFieldOfView(float32 (Math.PI / 4.), float32 game.Width / float32 game.Height, 0.001f, 5.0f)
    GL.MatrixMode(MatrixMode.Projection)
    GL.LoadMatrix(&projection)

let clearAndSetMatrix() =
    GL.Clear(ClearBufferMask.ColorBufferBit ||| ClearBufferMask.DepthBufferBit)
    let mutable modelview = Matrix4.LookAt(Vector3.Zero, Vector3.UnitZ, Vector3.UnitY)
    GL.MatrixMode(MatrixMode.Modelview)
    GL.LoadMatrix(&modelview)