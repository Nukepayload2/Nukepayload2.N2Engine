Imports Nukepayload2.N2Engine.Animations
Imports Nukepayload2.N2Engine.UI.Effects

Namespace UI.Elements
    ''' <summary>
    ''' 舞台 - 场景 - 层 - 元素 游戏对象树模型下的场景。
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
                Throw New ArgumentException("camera.Name 不能为空")
            End If
            If Cameras.ContainsKey(camera.Name) Then
                Throw New InvalidOperationException("试图添加一个重名的摄像机")
            End If
            Cameras.Add(camera.Name, camera)
        End Sub

        Public Overrides Function GetChildEffectSources() As IEnumerable(Of IGameEffectSource)
            Return Children
        End Function

        Dim _Template As Func(Of GameScene, GameVirtualizingScrollViewer) = Function(s) New GameVirtualizingScrollViewer
        ''' <summary>
        ''' 根据此场景的数据，创建一个虚拟化滚动面板用于呈现。默认情况下会创建一个与场景没有关联的面板。
        ''' </summary>
        Public Property Template As Func(Of GameScene, GameVirtualizingScrollViewer)
            Get
                Return _Template
            End Get
            Set(value As Func(Of GameScene, GameVirtualizingScrollViewer))
                _Template = value
                RaiseEvent TemplateChanged(Me, EventArgs.Empty)
            End Set
        End Property
        ''' <summary>
        ''' 模板发生变化
        ''' </summary>
        Public Event TemplateChanged As GameObjectEventHandler(Of GameScene, EventArgs)
    End Class

End Namespace