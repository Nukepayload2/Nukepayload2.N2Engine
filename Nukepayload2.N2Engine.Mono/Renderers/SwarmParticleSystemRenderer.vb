Imports Microsoft.Xna.Framework
Imports Nukepayload2.N2Engine.ParticleSystems
Imports Nukepayload2.N2Engine.UI.ParticleSystemViews

Friend Class SwarmParticleSystemRenderer

    Sub New(view As SwarmParticleSystemView)
        MyBase.New(view)
    End Sub

    Friend Overrides Sub OnCreateResources(sender As Game, args As MonogameCreateResourcesEventArgs)
        Dim view = DirectCast(Me.View, SwarmParticleSystemView)
        SpriteParticleSystemHelper.Load(Of SpriteParticle, SwarmParticleSystem)(sender, args, view)
    End Sub

    Friend Overrides Sub OnDraw(sender As Game, args As MonogameDrawEventArgs)
        Dim ds = args.DrawingContext
        Dim view = DirectCast(Me.View, SwarmParticleSystemView)
        SpriteParticleSystemHelper.Draw(Of SpriteParticle, SwarmParticleSystem)(args.DrawingContext, view)
    End Sub
End Class
