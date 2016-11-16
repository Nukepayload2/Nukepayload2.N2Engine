﻿Imports Microsoft.Xna.Framework
Imports Nukepayload2.N2Engine.UI.Elements

Friend Class BezierQuadraticElementRenderer
    Sub New(view As BezierQuadraticElement)
        MyBase.New(view)
    End Sub

    Friend Overrides Sub OnDraw(sender As Game, args As MonogameDrawEventArgs)
        Dim view = DirectCast(Me.View, BezierQuadraticElement)
        Dim loc = view.Location.Value.AsXnaVector2
        args.DrawingContext.DrawBezierQuadratic(loc + View.StartPoint.Value.AsXnaVector2,
                                                loc + View.ControlPoint.Value.AsXnaVector2,
                                                loc + View.EndPoint.Value.AsXnaVector2,
                                                View.Stroke.Value.AsXnaColor)
    End Sub
End Class
