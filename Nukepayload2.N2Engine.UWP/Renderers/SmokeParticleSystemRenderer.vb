Imports Microsoft.Graphics.Canvas.UI
Imports Microsoft.Graphics.Canvas.UI.Xaml
Imports Nukepayload2.N2Engine.ParticleSystems
Imports Nukepayload2.N2Engine.UI.ParticleSystemViews

Friend Class SmokeParticleSystemRenderer

    Sub New(view As SmokeParticleSystemView)
        MyBase.New(view)
    End Sub

    Friend Overrides Sub OnCreateResources(sender As CanvasAnimatedControl, args As CanvasCreateResourcesEventArgs)
        Dim view = DirectCast(MyBase.View, SmokeParticleSystemView)
        SpriteParticleSystemHelper.Load(Of SpriteParticle, SmokeParticleSystem)(sender, args, view)
    End Sub

    Friend Overrides Sub OnDraw(sender As ICanvasAnimatedControl, args As CanvasAnimatedDrawEventArgs)
        Dim drawingSession = args.DrawingSession
        Dim view = DirectCast(MyBase.View, SmokeParticleSystemView)
        SpriteParticleSystemHelper.Draw(Of SpriteParticle, SmokeParticleSystem)(drawingSession, view)
    End Sub

End Class