Imports Nukepayload2.N2Engine.Platform
Imports Nukepayload2.N2Engine.UI
Imports Nukepayload2.N2Engine.UI.Elements

Partial Public Class GameElementRenderer(Of T As GameElement)
    ''' <summary>
    ''' 警告：如果你的渲染器使用了 <see cref="PlatformImplAttribute"/> 标注它属于哪个 <see cref="GameVisual"/>, 则不要显式调用这个构造函数。因为游戏元素渲染器会被相应的游戏元素自动创建。
    ''' </summary>
    Sub New(view As T)
        MyBase.New(view)
    End Sub

End Class
