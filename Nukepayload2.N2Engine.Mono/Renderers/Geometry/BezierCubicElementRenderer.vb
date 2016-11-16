Imports Microsoft.Xna.Framework
Imports Nukepayload2.N2Engine.UI.Elements
Friend Class BezierCubicElementRenderer
    Sub New(view As BezierCubicElement)
        MyBase.New(view)
    End Sub

    Friend Overrides Sub OnDraw(sender As Game, args As MonogameDrawEventArgs)
        Dim view = DirectCast(Me.View, BezierCubicElement)
        Dim loc = View.Location.Value.AsXnaVector2
        args.DrawingContext.DrawBezierCubic(loc + View.StartPoint.Value.AsXnaVector2,
                                            loc + View.ControlPoint1.Value.AsXnaVector2,
                                            loc + View.ControlPoint2.Value.AsXnaVector2,
                                            loc + View.EndPoint.Value.AsXnaVector2,
                                            View.Stroke.Value.AsXnaColor)
    End Sub
End Class