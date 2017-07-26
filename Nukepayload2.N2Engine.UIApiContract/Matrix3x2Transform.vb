Imports Nukepayload2.N2Engine.Foundation

Namespace UI
    ''' <summary>
    ''' 表示二维矩阵变换
    ''' </summary>
    Public Class Matrix3x2Transform
        Inherits PlaneTransform
        Public ReadOnly Property Matrix As New PropertyBinder(Of Matrix3x2)
        Public Overrides Function GetTransformMatrix() As Matrix3x2
            Return Matrix.Value
        End Function
    End Class

End Namespace