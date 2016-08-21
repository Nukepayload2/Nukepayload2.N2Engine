Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Input
Imports Nukepayload2.N2Engine.Core
''' <summary>
''' 火花粒子系统的渲染器
''' </summary>
<PlatformImpl(GetType(SparkParticleSystemView))>
Friend Class SparkParticleSystemRenderer
    Inherits GameElementRenderer(Of SparkParticleSystemView)

    Sub New(view As SparkParticleSystemView)
        MyBase.New(view)
    End Sub

    Public Overrides Sub DisposeResources()
        Debug.WriteLine("释放全部火花资源")
    End Sub

    Protected Overrides Sub OnCreateResources(sender As Game, args As MonogameCreateResourcesEventArgs)
        Debug.WriteLine("准备火花资源")
    End Sub

    Protected Overrides Sub OnDraw(sender As Game, args As MonogameDrawEventArgs)
        Dim ds = args.DrawingContext
        Dim SparkSys = View.Data.Value
        For Each part In SparkSys.Particles
            ds.DrawFilledRectangle(part.Location.X, part.Location.Y, part.SparkSize, part.SparkSize, part.SparkColor.AsXnaColor)
        Next
    End Sub

    Protected Overrides Sub OnGameLoopStarting(sender As Game, args As Object)

    End Sub

    Protected Overrides Sub OnGameLoopStopped(sender As Game, args As Object)

    End Sub

    Protected Overrides Sub OnUpdate(sender As Game, args As MonogameUpdateEventArgs)
        View.UpdateCommand.Execute()
    End Sub
End Class
