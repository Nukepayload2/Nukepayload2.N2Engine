Imports Microsoft.Graphics.Canvas.Geometry
Imports Microsoft.Graphics.Canvas.UI.Xaml
Imports Nukepayload2.N2Engine.UI.Elements
Imports Nukepayload2.N2Engine.UWP.Marshal

Friend Class BezierCubicElementRenderer
    Sub New(view As BezierCubicElement)
        MyBase.New(view)
    End Sub

    Protected Overrides Sub OnDraw(sender As ICanvasAnimatedControl, args As CanvasAnimatedDrawEventArgs)
        Dim view = DirectCast(Me.View, BezierCubicElement)
        Dim pb As New CanvasPathBuilder(sender)
        pb.BeginFigure(View.StartPoint.Value)
        pb.AddCubicBezier(View.ControlPoint1.Value, View.ControlPoint2.Value, View.EndPoint.Value)
        pb.EndFigure(CanvasFigureLoop.Open)
        args.DrawingSession.DrawGeometry(CanvasGeometry.CreatePath(pb), View.Location.Value, View.Stroke.Value.AsWindowsColor)
    End Sub

End Class
