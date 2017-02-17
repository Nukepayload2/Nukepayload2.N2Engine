Imports System.Numerics
Imports Microsoft.Graphics.Canvas.Geometry
Imports Microsoft.Graphics.Canvas.UI.Xaml
Imports Nukepayload2.N2Engine.Numerics
Imports Nukepayload2.N2Engine.UI.Elements
Imports Nukepayload2.N2Engine.UWP.Marshal

Friend Class RectangleElementRenderer
    Sub New(view As RectangleElement)
        MyBase.New(view)
    End Sub

    Friend Overrides Sub OnDraw(sender As ICanvasAnimatedControl, args As CanvasAnimatedDrawEventArgs)
        Dim view = DirectCast(Me.View, RectangleElement)
        Dim loc = view.Location.Value
        Dim size = view.Size.Value
        DrawGeometry(args.DrawingSession, view, loc, size)
    End Sub

    Private Shared Sub DrawGeometry(ds As Microsoft.Graphics.Canvas.CanvasDrawingSession, view As RectangleElement, loc As Vector2, size As Vector2)
        If view.Fill.CanRead Then
            ds.FillRectangle(loc.X, loc.Y, size.X, size.Y, view.Fill.Value.AsWindowsColor)
        End If
        If view.Stroke.CanRead Then
            ds.DrawRectangle(loc.X, loc.Y, size.X, size.Y, view.Stroke.Value.AsWindowsColor)
        End If
    End Sub
End Class
