Imports Microsoft.Xna.Framework
Imports Nukepayload2.N2Engine.UI.ParticleSystemViews

Friend Class RailgunParticleSystemRenderer

    Sub New(view As RailgunParticleSystemView)
        MyBase.New(view)
    End Sub

    Friend Overrides Sub OnCreateResources(sender As Game, args As MonogameCreateResourcesEventArgs)
        Dim view = DirectCast(Me.View, RailgunParticleSystemView)
        view.Data.Value.RemoveFromGameCanvasCallback = AddressOf view.RemoveFromGameCanvas
    End Sub

    Friend Overrides Sub OnDraw(sender As Game, args As MonogameDrawEventArgs)
        Dim ds = args.DrawingContext
        Dim view = DirectCast(Me.View, RailgunParticleSystemView)
        Dim sys = view.Data.Value
        Dim color = sys.Color.AsXnaColor
        For Each part In sys.Particles
            ds.DrawFilledRectangle(part.Location.X, part.Location.Y, 1.0F, 1.0F, color)
        Next
    End Sub
End Class
