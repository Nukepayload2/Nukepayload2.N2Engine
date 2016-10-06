Imports Microsoft.Xna.Framework
Imports Nukepayload2.N2Engine.UI.Elements

Friend Class LineElementRenderer
    Sub New(view As LineElement)
        MyBase.New(view)
    End Sub

    Protected Overrides Sub OnDraw(sender As Game, args As MonogameDrawEventArgs)
        Dim loc = View.Location.Value.AsXnaVector2
        args.DrawingContext.DrawLine(loc + View.StartPoint.Value.AsXnaVector2,
                                     loc + View.EndPoint.Value.AsXnaVector2,
                                     View.Stroke.Value.AsXnaColor)
    End Sub
End Class