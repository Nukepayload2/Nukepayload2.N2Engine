Imports Microsoft.Graphics.Canvas.UI
Imports Microsoft.Graphics.Canvas.UI.Xaml
Imports Nukepayload2.N2Engine.UI.Elements

Public MustInherit Class GameCanvasContainerRenderer
    Inherits UWPRenderer
    Sub New(container As GameVisualContainer)
        MyBase.New(container)
    End Sub
    Protected Overrides Sub OnDraw(sender As ICanvasAnimatedControl, args As CanvasAnimatedDrawEventArgs)
        Throw New NotImplementedException()
    End Sub
    Protected Overrides Sub OnUpdate(sender As ICanvasAnimatedControl, args As CanvasAnimatedUpdateEventArgs)
        Throw New NotImplementedException()
    End Sub
End Class
