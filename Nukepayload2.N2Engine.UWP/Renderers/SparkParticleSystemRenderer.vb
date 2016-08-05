Imports System.Numerics
Imports Microsoft.Graphics.Canvas.UI
Imports Microsoft.Graphics.Canvas.UI.Xaml
Imports Nukepayload2.N2Engine.Core
Imports Nukepayload2.N2Engine.UWP.Marshal
Imports Windows.UI

''' <summary>
''' 火花粒子系统的渲染器
''' </summary>
<PlatformImpl(GetType(SparkParticleSystemView))>
Public Class SparkParticleSystemRenderer
    Inherits GameElementRenderer(Of SparkParticleSystemView)

    Sub New(view As SparkParticleSystemView)
        MyBase.New(view)
    End Sub

    Protected Overrides Sub OnCreateResources(sender As CanvasAnimatedControl, args As CanvasCreateResourcesEventArgs)
        Debug.WriteLine("准备火花资源")
    End Sub

    Protected Overrides Sub OnDraw(sender As ICanvasAnimatedControl, args As CanvasAnimatedDrawEventArgs)
        Dim ds = args.DrawingSession
        Dim SparkSys = View.Data.Value
        ds.DrawText($"粒子数量: {SparkSys.Particles.Count}", New Vector2(20, 20), Colors.Black)
        For Each part In SparkSys.Particles
            ds.FillRectangle(New Rect(part.Location.ToPoint, New Size(part.SparkSize, part.SparkSize)), part.SparkColor.AsWindowsColor)
        Next
    End Sub

    Protected Overrides Sub OnGameLoopStarting(sender As ICanvasAnimatedControl, args As Object)

    End Sub

    Protected Overrides Sub OnGameLoopStopped(sender As ICanvasAnimatedControl, args As Object)

    End Sub

    Protected Overrides Sub OnUpdate(sender As ICanvasAnimatedControl, args As CanvasAnimatedUpdateEventArgs)
        View.UpdateCommand.Execute()
    End Sub

    Public Overrides Sub DisposeResources()
        MyBase.DisposeResources()
        Debug.WriteLine("释放全部火花资源")
    End Sub
End Class