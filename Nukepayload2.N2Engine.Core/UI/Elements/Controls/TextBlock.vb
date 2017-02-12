Imports Nukepayload2.N2Engine.Foundation
Imports Nukepayload2.N2Engine.UI.Text

Namespace UI.Controls
    ''' <summary>
    ''' 文本块控件。用于显示文字。
    ''' </summary>
    Public Class TextBlock
        Inherits GameControl
        ''' <summary>
        ''' 要显示的文字
        ''' </summary>
        Public ReadOnly Property Text As New PropertyBinder(Of String)
        ''' <summary>
        ''' 所用的字体
        ''' </summary>
        Public Property Font As GameFont
    End Class

End Namespace