Imports Nukepayload2.N2Engine.Foundation

Namespace UI
    ''' <summary>
    ''' 表示一个平面变换
    ''' </summary>
    Public MustInherit Class PlaneTransform
        Public MustOverride Function GetTransformMatrix() As Matrix3x2
        Public ReadOnly Property Origin As New PropertyBinder(Of Vector2)
    End Class
End Namespace