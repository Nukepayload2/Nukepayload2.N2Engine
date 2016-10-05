Imports Microsoft.Graphics.Canvas.UI
Imports Microsoft.Graphics.Canvas.UI.Xaml
Imports Nukepayload2.N2Engine.UI.Elements

Friend Class LineElementRenderer
    Sub New(view As LineElement)
        MyBase.New(view)
    End Sub

    Protected Overrides Sub OnCreateResources(sender As CanvasAnimatedControl, args As CanvasCreateResourcesEventArgs)
        Throw New NotImplementedException()
    End Sub

    Protected Overrides Sub OnDraw(sender As ICanvasAnimatedControl, args As CanvasAnimatedDrawEventArgs)
        Throw New NotImplementedException()
    End Sub

    Protected Overrides Sub OnGameLoopStarting(sender As ICanvasAnimatedControl, args As Object)
        Throw New NotImplementedException()
    End Sub

    Protected Overrides Sub OnGameLoopStopped(sender As ICanvasAnimatedControl, args As Object)
        Throw New NotImplementedException()
    End Sub

    Protected Overrides Sub OnUpdate(sender As ICanvasAnimatedControl, args As CanvasAnimatedUpdateEventArgs)
        Throw New NotImplementedException()
    End Sub
End Class
