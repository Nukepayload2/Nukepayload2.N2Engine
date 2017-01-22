Imports Microsoft.Xna.Framework
Imports Nukepayload2.N2Engine.UI.Elements
Friend Class BezierCubicElementRenderer
    Sub New(view As BezierCubicElement)
        MyBase.New(view)
    End Sub

    Friend Overrides Sub OnDraw(sender As Game, args As MonogameDrawEventArgs)
        Dim view = DirectCast(Me.View, BezierCubicElement)
        Dim loc = View.Location.Value.AsXnaVector2
        args.DrawingContext.DrawBezierCubic(loc + view.StartPoint.Value.AsXnaVector2,
                                            loc + view.ControlPoint1.Value.AsXnaVector2,
                                            loc + view.ControlPoint2.Value.AsXnaVector2,
                                            loc + view.EndPoint.Value.AsXnaVector2,
                                            view.Stroke.Value.AsXnaColor)
    End Sub
End Class