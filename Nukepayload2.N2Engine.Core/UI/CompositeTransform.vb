﻿Imports Nukepayload2.N2Engine.Foundation

Namespace UI
    ''' <summary>
    ''' 表示一个合成的二维变换
    ''' </summary>
    Public Class CompositeTransform
        Inherits PlaneTransform
        ''' <summary>
        ''' 平移变换。在 Win2D 中单位是设备无关单位，在 MonoGame 中单位是像素。
        ''' </summary>
        Public ReadOnly Property Translate As New PropertyBinder(Of Vector2)(New Vector2(0F, 0F))
        ''' <summary>
        ''' 绕Z轴进行旋转变换。单位是度。
        ''' </summary>
        Public ReadOnly Property Rotate As New PropertyBinder(Of Single)(0F)
        ''' <summary>
        ''' 扭曲变换。在 Win2D 中单位是设备无关单位，在 MonoGame 中单位是像素。
        ''' </summary>
        Public ReadOnly Property Skew As New PropertyBinder(Of Vector2)(New Vector2(0.0F, 0.0F))
        ''' <summary>
        ''' 扭曲变换。用百分比表示。
        ''' </summary>
        Public ReadOnly Property Scale As New PropertyBinder(Of Vector2)(New Vector2(1.0F, 1.0F))
        ''' <summary>
        ''' 获取当前变换产生的变换矩阵
        ''' </summary>
        Public Overrides Function GetTransformMatrix() As Matrix3x2
            Dim mat = Matrix3x2.Identity
            If Translate.CanRead Then
                mat *= Matrix3x2.CreateTranslation(Translate.Value)
            End If
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
            Return mat
        End Function
    End Class
End Namespace