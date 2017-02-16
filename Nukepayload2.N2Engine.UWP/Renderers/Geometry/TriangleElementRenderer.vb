Imports Microsoft.Graphics.Canvas.Geometry
Imports Microsoft.Graphics.Canvas.UI.Xaml
Imports Nukepayload2.N2Engine.Numerics
Imports Nukepayload2.N2Engine.UI.Elements
Imports Nukepayload2.N2Engine.UWP.Marshal

Friend Class TriangleElementRenderer
    Sub New(view As TriangleElement)
        MyBase.New(view)
    End Sub

    Friend Overrides Sub OnDraw(sender As ICanvasAnimatedControl, args As CanvasAnimatedDrawEventArgs)
        Dim view = DirectCast(Me.View, TriangleElement)
        Dim pb As New CanvasPathBuilder(sender)
        If view.Transform IsNot Nothing Then
            Dim matrix = view.Transform.GetTransformMatrix
            pb.BeginFigure(view.Point1.Value.ApplyTransform(matrix))
            pb.AddLine(view.Point2.Value.ApplyTransform(matrix))
            pb.AddLine(view.Point3.Value.ApplyTransform(matrix))
        Else
            pb.BeginFigure(view.Point1.Value)
            pb.AddLine(view.Point2.Value)
            pb.AddLine(view.Point3.Value)
        End If
        pb.EndFigure(CanvasFigureLoop.Closed)
        Dim loc = view.Location.Value
        If view.Fill.CanRead Then
            args.DrawingSession.FillGeometry(CanvasGeometry.CreatePath(pb), loc, view.Fill.Value.AsWindowsColor)
        End If
        If view.Stroke.CanRead Then
            args.DrawingSession.DrawGeometry(CanvasGeometry.CreatePath(pb), loc, view.Stroke.Value.AsWindowsColor)
        End If
    End Sub
End Class
