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
        pb.BeginFigure(view.Point1.Value)
        pb.AddLine(view.Point2.Value)
        pb.AddLine(view.Point3.Value)
        pb.EndFigure(CanvasFigureLoop.Closed)
        Dim loc = view.Location.Value
        If view.Transform IsNot Nothing Then
            Dim matrix = view.Transform.GetTransformMatrix
            DrawWithTransform2D(args.DrawingSession,
                                Sub(ds)
                                    DrawGeometry(ds, view, pb, loc)
                                End Sub)
        Else
            DrawGeometry(args.DrawingSession, view, pb, loc)
        End If
    End Sub

    Private Shared Sub DrawGeometry(ds As Microsoft.Graphics.Canvas.CanvasDrawingSession, view As TriangleElement, pb As CanvasPathBuilder, loc As System.Numerics.Vector2)
        If view.Fill.CanRead Then
            ds.FillGeometry(CanvasGeometry.CreatePath(pb), loc, view.Fill.Value.AsWindowsColor)
        End If
        If view.Stroke.CanRead Then
            ds.DrawGeometry(CanvasGeometry.CreatePath(pb), loc, view.Stroke.Value.AsWindowsColor)
        End If
    End Sub
End Class
