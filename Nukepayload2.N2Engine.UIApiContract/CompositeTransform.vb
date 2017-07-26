Imports Nukepayload2.N2Engine.Foundation

Namespace UI
    ''' <summary>
    ''' 表示一个合成的二维变换
    ''' </summary>
    Public Class CompositeTransform
        Inherits PlaneTransform
        ''' <summary>
        ''' 平移变换。这个操作最先进行。在 Win2D 中单位是设备无关单位，在 MonoGame 中单位是像素。
        ''' </summary>
        Public ReadOnly Property Translate As New PropertyBinder(Of Vector2)
        ''' <summary>
        ''' 绕Z轴进行旋转变换。单位是度。这个操作第二个执行。
        ''' </summary>
        Public ReadOnly Property Rotate As New PropertyBinder(Of Single)
        ''' <summary>
        ''' 扭曲变换。在 Win2D 中单位是设备无关单位，在 MonoGame 中单位是像素。这个操作第三个执行。
        ''' </summary>
        Public ReadOnly Property Skew As New PropertyBinder(Of Vector2)
        ''' <summary>
        ''' 扭曲变换。用百分比表示。这个操作最后执行。
        ''' </summary>
        Public ReadOnly Property Scale As New PropertyBinder(Of Vector2)
        ''' <summary>
        ''' 旋转，扭曲 和 缩放变换原点。默认是左上角。如果绑定为元素的大小，则表示右下角。
        ''' </summary>
        Public ReadOnly Property Origin As New PropertyBinder(Of Vector2)
        ''' <summary>
        ''' 获取当前变换按照平移，旋转，扭曲，缩放的顺序得到的变换矩阵。
        ''' </summary>
        Public Overrides Function GetTransformMatrix() As Matrix3x2
            Dim mat = Matrix3x2.Identity
            If Translate.CanRead Then
                mat *= Matrix3x2.CreateTranslation(Translate.Value)
            End If
            If Not Origin.CanRead Then
                If Rotate.CanRead Then
                    mat *= Matrix3x2.CreateRotation(Rotate.Value)
                End If
                If Skew.CanRead Then
                    Dim skw = Skew.Value
                    mat *= Matrix3x2.CreateSkew(skw.X, skw.Y)
                End If
                If Scale.CanRead Then
                    mat *= Matrix3x2.CreateScale(Scale.Value)
                End If
            Else
                Dim center = Origin.Value
                If Rotate.CanRead Then
                    mat *= Matrix3x2.CreateRotation(Rotate.Value, center)
                End If
                If Skew.CanRead Then
                    Dim skw = Skew.Value
                    mat *= Matrix3x2.CreateSkew(skw.X, skw.Y, center)
                End If
                If Scale.CanRead Then
                    mat *= Matrix3x2.CreateScale(Scale.Value, center)
                End If
            End If
            Return mat
        End Function
    End Class
End Namespace