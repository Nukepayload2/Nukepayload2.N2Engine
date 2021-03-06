﻿Imports Microsoft.Xna.Framework
Imports Nukepayload2.N2Engine.UI.ParticleSystemViews

Friend Class SparkParticleSystemRenderer

    Sub New(view As SparkParticleSystemView)
        MyBase.New(view)
    End Sub

    Friend Overrides Sub OnCreateResources(sender As Game, args As MonogameCreateResourcesEventArgs)
        Dim view = DirectCast(Me.View, SparkParticleSystemView)
        view.Data.Value.RemoveFromGameCanvasCallback = AddressOf view.RemoveFromGameCanvas
    End Sub

    Friend Overrides Sub OnDraw(sender As Game, args As MonogameDrawEventArgs)
        Dim ds = args.DrawingContext
        Dim view = DirectCast(Me.View, SparkParticleSystemView)
        Dim SparkSys = view.Data.Value
        For Each part In SparkSys.Particles
            ds.DrawFilledRectangle(part.Location.X, part.Location.Y, part.SparkSize, part.SparkSize, part.SparkColor.AsXnaColor)
        Next
    End Sub
End Class
