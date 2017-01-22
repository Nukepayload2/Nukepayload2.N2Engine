Imports Microsoft.Xna.Framework
Imports Nukepayload2.N2Engine.UI.Elements

Friend Class LineElementRenderer
    Sub New(view As LineElement)
        MyBase.New(view)
    End Sub

    Friend Overrides Sub OnDraw(sender As Game, args As MonogameDrawEventArgs)
        Dim view = DirectCast(Me.View, LineElement)
        Dim loc = view.Location.Value.AsXnaVector2
        args.DrawingContext.DrawLine(loc + view.StartPoint.Value.AsXnaVector2,
                                     loc + view.EndPoint.Value.AsXnaVector2,
                                     view.Stroke.Value.AsXnaColor)
    End Sub
End Class