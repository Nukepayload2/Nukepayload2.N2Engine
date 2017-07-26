Imports Nukepayload2.N2Engine.Input

Namespace UI.Elements
    ''' <summary>
    ''' 为触摸, 笔或者鼠标轻扫发生的事件提供数据
    ''' </summary>
    Public Class TappedEventArgs
        Inherits EventArgs

        Sub New(location As Vector2, deviceType As PointerDeviceType)
            Me.Location = location
            Me.DeviceType = deviceType
        End Sub
        ''' <summary>
        ''' 点击的位置
        ''' </summary>
        Public ReadOnly Property Location As Vector2
        ''' <summary>
        ''' 这个轻扫动作是由哪种设备捕获的。
        ''' </summary>
        Public ReadOnly Property DeviceType As PointerDeviceType
    End Class
End Namespace
