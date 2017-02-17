Imports Microsoft.Graphics.Canvas.Geometry
Imports Microsoft.Graphics.Canvas.UI.Xaml
Imports Nukepayload2.N2Engine.Numerics
Imports Nukepayload2.N2Engine.UI.Elements
Imports Nukepayload2.N2Engine.UWP.Marshal

Friend Class BezierCubicElementRenderer
    Sub New(view As BezierCubicElement)
        MyBase.New(view)
    End Sub

    Friend Overrides Sub OnDraw(sender As ICanvasAnimatedControl, args As CanvasAnimatedDrawEventArgs)
        Dim view = DirectCast(Me.View, BezierCubicElement)
        Dim pb As New CanvasPathBuilder(sender)
        pb.BeginFigure(view.StartPoint.Value)
        pb.AddCubicBezier(view.ControlPoint1.Value, view.ControlPoint2.Value, view.EndPoint.Value)
        pb.EndFigure(CanvasFigureLoop.Open)
        args.DrawingSession.DrawGeometry(CanvasGeometry.CreatePath(pb), view.Location.Value, view.Stroke.Value.AsWindowsColor)
    End Sub

End Class
