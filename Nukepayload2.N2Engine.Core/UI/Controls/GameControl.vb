Imports Nukepayload2.N2Engine.UI.Elements

Namespace UI.Controls
    ''' <summary>
    ''' (未实施) 游戏引擎集成的简单控件
    ''' </summary>
    Public Class GameControl
        Inherits GameElement
        ''' <summary>
        ''' 将控件转化为具体的游戏元素形式
        ''' </summary>
        Public Property Template As Func(Of GameControl, GameElement)
    End Class
End Namespace