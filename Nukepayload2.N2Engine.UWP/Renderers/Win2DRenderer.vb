Imports System.Numerics
Imports Microsoft.Graphics.Canvas
Imports Microsoft.Graphics.Canvas.UI
Imports Microsoft.Graphics.Canvas.UI.Xaml
Imports Nukepayload2.N2Engine.Renderers
Imports Nukepayload2.N2Engine.UI.Elements
''' <summary>
''' Win2D 渲染器的基类。
''' </summary>
Public MustInherit Class Win2DRenderer
    Inherits RendererBase

    Sub New(view As GameVisual)
        MyBase.New(view)
    End Sub
    ''' <summary>
    ''' 派生类继承时，定义手动引发 CreateResources 事件的行为 
    ''' </summary>
    Friend Overridable Sub OnCreateResources(sender As CanvasAnimatedControl, args As CanvasCreateResourcesEventArgs)

    End Sub
    ''' <summary>
    ''' 派生类继承时，异步加载资源。
    ''' </summary>
    Friend Overridable Async Function CreateResourceAsync(sender As CanvasAnimatedControl, args As CanvasCreateResourcesEventArgs) As Task

    End Function
    ''' <summary>
    ''' 派生类继承时，定义手动引发 Draw 事件的行为 
    ''' </summary>
    Friend MustOverride Sub OnDraw(sender As ICanvasAnimatedControl, args As CanvasAnimatedDrawEventArgs)
    ''' <summary>
    ''' 派生类继承时，定义手动引发 GameLoopStarting 事件的行为 
    ''' </summary>
    Friend Overridable Sub OnGameLoopStarting(sender As ICanvasAnimatedControl, args As Object)

    End Sub
    ''' <summary>
    ''' 派生类继承时，定义手动引发 GameLoopStopped 事件的行为 
    ''' </summary>
    Friend Overridable Sub OnGameLoopStopped(sender As ICanvasAnimatedControl, args As Object)

    End Sub
    ''' <summary>
    ''' 派生类继承时，定义手动引发 Update 事件的行为 
    ''' </summary>
    Friend Overridable Sub OnUpdate(sender As ICanvasAnimatedControl, args As CanvasAnimatedUpdateEventArgs)
        Dim tim = args.Timing
        View.UpdateAction.Invoke(New UpdatingEventArgs(tim.ElapsedTime, tim.TotalTime, tim.IsRunningSlowly))
    End Sub

    Public Overrides Sub DisposeResources()

    End Sub

    Protected Sub DrawWithTransform2D(drawingSession As CanvasDrawingSession, draw As Action(Of CanvasDrawingSession))
        Using cl = New CanvasCommandList(drawingSession)
            Using ds = cl.CreateDrawingSession
                Dim transformMatrix = View.Transform.GetTransformMatrix
                ds.Transform = transformMatrix
                draw(ds)
            End Using
            drawingSession.DrawImage(cl)
        End Using
    End Sub

    Protected Sub DrawImageWithTransform2D(loc As Vector2, width As Double, height As Double, drawingSession As CanvasDrawingSession, texture As CanvasBitmap)
        DrawWithTransform2D(drawingSession, Sub(ds) ds.DrawImage(texture, New Rect(loc.X, loc.Y, width, height)))
    End Sub
End Class
