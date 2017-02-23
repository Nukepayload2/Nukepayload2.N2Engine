Imports System.Numerics

Namespace Foundation
    ''' <summary>
    ''' 表示一个矩形的边界
    ''' </summary>
    Public Structure RectangleBounds
        Public Sub New(offset As Vector2, size As Vector2)
            Me.New()
            Me.Offset = offset
            Me.Size = size
        End Sub

        ''' <summary>
        ''' 相对于原点的起始位置
        ''' </summary>
        Public Offset As Vector2
        ''' <summary>
        ''' 矩形的大小
        ''' </summary>
        Public Size As Vector2
    End Structure

End Namespace