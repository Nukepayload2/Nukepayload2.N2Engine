Imports Nukepayload2.N2Engine.Foundation

Namespace UI
    ''' <summary>
    ''' 镶嵌式裁剪。使用一个外部的矩形框决定裁剪掉的边缘。
    ''' </summary>
    Public Class GameInsetClip
        Inherits GameClip
        ''' <summary>
        ''' 定义从四周进行的裁剪。顺序为：w左，x上，y右，z下。
        ''' </summary>
        Public Property Thickness As PropertyBinder(Of Vector4) = New ManualPropertyBinder(Of Vector4)
    End Class
End Namespace