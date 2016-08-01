﻿Imports Microsoft.Graphics.Canvas.UI
Imports Microsoft.Graphics.Canvas.UI.Xaml
Imports Nukepayload2.N2Engine.Core
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

    End Sub

    Protected Overrides Sub OnDraw(sender As ICanvasAnimatedControl, args As CanvasAnimatedDrawEventArgs)

    End Sub

    Protected Overrides Sub OnGameLoopStarting(sender As ICanvasAnimatedControl, args As Object)

    End Sub

    Protected Overrides Sub OnGameLoopStopped(sender As ICanvasAnimatedControl, args As Object)

    End Sub

    Protected Overrides Sub OnUpdate(sender As ICanvasAnimatedControl, args As CanvasAnimatedUpdateEventArgs)

    End Sub
End Class