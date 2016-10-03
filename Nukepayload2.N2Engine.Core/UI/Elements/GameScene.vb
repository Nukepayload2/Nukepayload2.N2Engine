Imports Nukepayload2.N2Engine.Animations

Namespace UI.Elements
    ''' <summary>
    ''' 场景 - 层 - 元素 游戏对象树模型下的根节点。
    ''' </summary>
    Public Class GameScene
        Inherits GameVisual
        ''' <summary>
        ''' 场景中的层
        ''' </summary>
        Public ReadOnly Property Children As New LinkedList(Of GameLayer)
        ''' <summary>
        ''' 场景的过渡动画，例如淡入淡出。
        ''' </summary>
        Public Property Transitions As TransitionAnimation
        ''' <summary>
        ''' 命名的摄像机集合。添加时务必确保摄像机有名称且与注册时的名称一致。
        ''' </summary>
        Public ReadOnly Property Cameras As New Dictionary(Of String, GameCamera)
        ''' <summary>
        ''' 添加一个摄像机
        ''' </summary>
        Public Sub AddCamera(camera As GameCamera)
            If camera Is Nothing Then
                Throw New ArgumentNullException(NameOf(camera))
            End If
            If String.IsNullOrEmpty(camera.Name) Then
                Throw New ArgumentNullException("Camera.Name")
            End If
            If Cameras.ContainsKey(camera.Name) Then
                Throw New InvalidOperationException("试图添加一个重名的摄像机")
            End If
            Cameras.Add(camera.Name, camera)
        End Sub
        ''' <summary>
        ''' 根据此场景的数据，创建一个虚拟化滚动面板用于呈现。默认情况下会创建一个与场景没有关联的面板。
        ''' </summary>
        Public Property Template As Func(Of GameScene, VisualizingScrollViewer) = Function(s) New VisualizingScrollViewer
    End Class

End Namespace