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
        If view.Transform IsNot Nothing Then
            Dim matrix = view.Transform.GetTransformMatrix
            pb.BeginFigure(view.StartPoint.Value.ApplyTransform(matrix))
            For t = 0F To 0.99F Step 0.01F
                pb.AddLine(view.GetVertex(t).ApplyTransform(matrix))
            Next
        Else
            pb.BeginFigure(view.StartPoint.Value)
            For t = 0F To 0.99F Step 0.01F
                pb.AddLine(view.GetVertex(t))
            Next
        End If
        pb.EndFigure(CanvasFigureLoop.Open)
        args.DrawingSession.DrawGeometry(CanvasGeometry.CreatePath(pb), view.Location.Value, view.Stroke.Value.AsWindowsColor)
    End Sub

End Class
