Imports Microsoft.Graphics.Canvas.UI.Xaml
Imports Nukepayload2.N2Engine.UI.Elements
''' <summary>
''' 游戏中的非布局可见元素的渲染器。会被相应的游戏元素自动创建。
''' </summary>
''' <typeparam name="T"></typeparam>
Public MustInherit Class GameElementRenderer(Of T As GameElement)
    Inherits UWPRenderer(Of T)

    Protected Overrides Sub OnUpdate(sender As ICanvasAnimatedControl, args As CanvasAnimatedUpdateEventArgs)
        View.UpdateCommand.Execute()
    End Sub
End Class
