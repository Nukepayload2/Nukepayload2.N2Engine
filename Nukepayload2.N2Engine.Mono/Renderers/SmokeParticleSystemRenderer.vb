Imports Microsoft.Xna.Framework
Imports Nukepayload2.N2Engine.ParticleSystems
Imports Nukepayload2.N2Engine.UI.ParticleSystemViews

Friend Class SmokeParticleSystemRenderer

    Sub New(view As SmokeParticleSystemView)
        MyBase.New(view)
    End Sub

    Friend Overrides Sub OnCreateResources(sender As Game, args As MonogameCreateResourcesEventArgs)
        Dim view = DirectCast(Me.View, SmokeParticleSystemView)
        SpriteParticleSystemHelper.Load(Of SpriteParticle, SmokeParticleSystem)(sender, args, view)
    End Sub

    Friend Overrides Sub OnDraw(sender As Game, args As MonogameDrawEventArgs)
        Dim ds = args.DrawingContext
        Dim view = DirectCast(Me.View, SmokeParticleSystemView)
        SpriteParticleSystemHelper.Draw(Of SpriteParticle, SmokeParticleSystem)(args.DrawingContext, view)
    End Sub
End Class
