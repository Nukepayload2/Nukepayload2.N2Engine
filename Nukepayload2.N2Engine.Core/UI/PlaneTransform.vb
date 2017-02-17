Imports Nukepayload2.N2Engine.Foundation

Namespace UI
    ''' <summary>
    ''' 表示一个平面变换
    ''' </summary>
    Public MustInherit Class PlaneTransform
        ''' <summary>
        ''' 获取表示平面变换的 3x2 矩阵.
        ''' </summary>
        Public MustOverride Function GetTransformMatrix() As Matrix3x2
    End Class
End Namespace