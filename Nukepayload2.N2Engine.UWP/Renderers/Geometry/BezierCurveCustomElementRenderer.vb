Imports Microsoft.Graphics.Canvas.Geometry
Imports Microsoft.Graphics.Canvas.UI.Xaml
Imports Nukepayload2.N2Engine.Numerics
Imports Nukepayload2.N2Engine.UI.Elements
Imports Nukepayload2.N2Engine.UWP.Marshal

Friend Class BezierCurveCustomElementRenderer
    Sub New(view As BezierCurveCustomElement)
        MyBase.New(view)
    End Sub

    Friend Overrides Sub OnDraw(sender As ICanvasAnimatedControl, args As CanvasAnimatedDrawEventArgs)
        Dim view = DirectCast(Me.View, BezierCurveCustomElement)
        Dim pb As New CanvasPathBuilder(sender)
        pb.BeginFigure(view.StartPoint.Value)
        For t = 0F To 0.99F Step 0.01F
            pb.AddLine(view.GetVertex(t))
        Next
        pb.EndFigure(CanvasFigureLoop.Open)
        If view.Transform IsNot Nothing Then
            Dim matrix = view.Transform.GetTransformMatrix
            DrawWithTransform2D(args.DrawingSession,
                                Sub(ds)
                                    ds.DrawGeometry(CanvasGeometry.CreatePath(pb), view.Location.Value, view.Stroke.Value.AsWindowsColor)
                                End Sub)
        Else
            args.DrawingSession.DrawGeometry(CanvasGeometry.CreatePath(pb), view.Location.Value, view.Stroke.Value.AsWindowsColor)
        End If
    End Sub

End Class
