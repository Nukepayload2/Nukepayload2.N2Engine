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
        If view.Transform IsNot Nothing Then
            ' 对矩形进行矩阵变换会导致形状不规则。因此需要按照多边形绘制。
            Dim matrix = view.Transform.GetTransformMatrix
            Dim pb As New CanvasPathBuilder(sender)
            pb.BeginFigure(loc.ApplyTransform(matrix))
            pb.AddLine(New Vector2(loc.X + size.X, loc.Y).ApplyTransform(matrix))
            pb.AddLine((loc + size).ApplyTransform(matrix))
            pb.AddLine(New Vector2(loc.X, loc.Y + size.Y).ApplyTransform(matrix))
            pb.EndFigure(CanvasFigureLoop.Closed)
            If view.Fill.CanRead Then
                args.DrawingSession.FillGeometry(CanvasGeometry.CreatePath(pb), loc, view.Fill.Value.AsWindowsColor)
            End If
            If view.Stroke.CanRead Then
                args.DrawingSession.DrawGeometry(CanvasGeometry.CreatePath(pb), loc, view.Stroke.Value.AsWindowsColor)
            End If
        Else
            If view.Fill.CanRead Then
                args.DrawingSession.FillRectangle(loc.X, loc.Y, size.X, size.Y, view.Fill.Value.AsWindowsColor)
            End If
            If view.Stroke.CanRead Then
                args.DrawingSession.DrawRectangle(loc.X, loc.Y, size.X, size.Y, view.Stroke.Value.AsWindowsColor)
            End If
        End If
    End Sub
End Class
