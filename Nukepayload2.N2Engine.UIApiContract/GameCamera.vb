Imports Nukepayload2.N2Engine.UI.Elements

Namespace UI
    ''' <summary>
    ''' 用于便捷地切换 <see cref="GameVirtualizingScrollViewer"/> 的视图状态 
    ''' </summary>
    Public Class GameCamera
        Inherits GameObject
        ''' <summary>
        ''' 当前场景切换到的摄像机
        ''' </summary>
        Public Shared ReadOnly Property Current As GameCamera
        ''' <summary>
        ''' 当此值不为空时，对游戏场景产生一个透视变换
        ''' </summary>
        Public Property Perspective As Matrix4x4?
        ''' <summary>
        ''' 视图的中心点
        ''' </summary>
        Public Property LookingAt As Vector2
        ''' <summary>
        ''' 导致场景的当前摄像机切换到此摄像机
        ''' </summary>
        Public Sub Active()
            _Current = Me
        End Sub
    End Class
End Namespace
