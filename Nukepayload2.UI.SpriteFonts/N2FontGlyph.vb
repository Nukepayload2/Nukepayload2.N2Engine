''' <summary>
''' 字体的形状信息
''' </summary>
Public Class N2FontGlyph
    Sub New(left As Integer, top As Integer, width As Integer, height As Integer)
        Me.Left = left
        Me.Top = top
        Me.Width = width
        Me.Height = height
    End Sub
    Sub New()

    End Sub
    Public Property Left As Integer
    Public Property Top As Integer
    Public Property Width As Integer
    Public Property Height As Integer

End Class