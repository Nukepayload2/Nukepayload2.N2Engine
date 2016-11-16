Imports Microsoft.Graphics.Canvas.Geometry
Imports Microsoft.Graphics.Canvas.UI.Xaml
Imports Nukepayload2.N2Engine.UI.Elements
Imports Nukepayload2.N2Engine.UWP.Marshal

Friend Class BezierQuadraticElementRenderer
    Sub New(view As BezierQuadraticElement)
        MyBase.New(view)
    End Sub

    Protected Overrides Sub OnDraw(sender As ICanvasAnimatedControl, args As CanvasAnimatedDrawEventArgs)
        Dim view = DirectCast(Me.View, BezierQuadraticElement)
        Dim pb As New CanvasPathBuilder(sender)
        pb.BeginFigure(View.StartPoint.Value)
        pb.AddQuadraticBezier(View.ControlPoint.Value, View.EndPoint.Value)
        pb.EndFigure(CanvasFigureLoop.Open)
        args.DrawingSession.DrawGeometry(CanvasGeometry.CreatePath(pb), View.Location.Value, View.Stroke.Value.AsWindowsColor)
    End Sub
End Class
