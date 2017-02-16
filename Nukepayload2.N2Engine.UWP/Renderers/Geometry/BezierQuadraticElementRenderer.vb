Imports Microsoft.Graphics.Canvas.Geometry
Imports Microsoft.Graphics.Canvas.UI.Xaml
Imports Nukepayload2.N2Engine.Numerics
Imports Nukepayload2.N2Engine.UI.Elements
Imports Nukepayload2.N2Engine.UWP.Marshal

Friend Class BezierQuadraticElementRenderer
    Sub New(view As BezierQuadraticElement)
        MyBase.New(view)
    End Sub

    Friend Overrides Sub OnDraw(sender As ICanvasAnimatedControl, args As CanvasAnimatedDrawEventArgs)
        Dim view = DirectCast(Me.View, BezierQuadraticElement)
        Dim pb As New CanvasPathBuilder(sender)
        If view.Transform IsNot Nothing Then
            Dim matrix = view.Transform.GetTransformMatrix
            pb.BeginFigure(view.StartPoint.Value.ApplyTransform(matrix))
            pb.AddQuadraticBezier(view.ControlPoint.Value.ApplyTransform(matrix), view.EndPoint.Value.ApplyTransform(matrix))
        Else
            pb.BeginFigure(view.StartPoint.Value)
            pb.AddQuadraticBezier(view.ControlPoint.Value, view.EndPoint.Value)
        End If
        pb.EndFigure(CanvasFigureLoop.Open)
        args.DrawingSession.DrawGeometry(CanvasGeometry.CreatePath(pb), view.Location.Value, view.Stroke.Value.AsWindowsColor)
    End Sub
End Class
