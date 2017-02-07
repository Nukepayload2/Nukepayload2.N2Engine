Imports Nukepayload2.N2Engine.Behaviors
Imports Nukepayload2.N2Engine.Foundation
Imports Nukepayload2.N2Engine.Renderers
Imports Nukepayload2.N2Engine.Triggers
Imports Nukepayload2.N2Engine.UI.Effects

Namespace UI.Elements
    ''' <summary>
    ''' 游戏中的基本可见对象
    ''' </summary>
    Public MustInherit Class GameVisual
        Inherits GameObject
        Implements IGameEffectSource

        ''' <summary>
        ''' 父级的容器（如果存在）。
        ''' </summary>
        Public Property Parent As GameVisualContainer
        ''' <summary>
        ''' 与这个可见对象关联的渲染器
        ''' </summary>
        Public Property Renderer As RendererBase
        ''' <summary>
        ''' (未实施) 元素与背景混合时的行为
        ''' </summary>
        Public Property CompositeMode As GameCompositeModes
        ''' <summary>
        ''' (未实施) 对元素进行裁剪
        ''' </summary>
        Public Property Clip As GameClip
        ''' <summary>
        ''' 位置。这通常是物体的左上角的坐标。
        ''' </summary>
        Public Overridable ReadOnly Property Location As New PropertyBinder(Of Vector2)
        ''' <summary>
        ''' 大小。用于简易版本的碰撞检测。
        ''' </summary>
        Public ReadOnly Property Size As New PropertyBinder(Of Vector2)
        ''' <summary>
        ''' (未实施) 不透明度。范围是0到1。
        ''' </summary>
        Public ReadOnly Property Opacity As New PropertyBinder(Of Single)
        ''' <summary>
        ''' (未实施) Z序越高越靠外，反之靠里
        ''' </summary>
        Public ReadOnly Property ZIndex As New PropertyBinder(Of Integer)
        ''' <summary>
        ''' (未实施) 是否在碰撞检测中可见
        ''' </summary>
        Public ReadOnly Property IsHitTestVisible As New PropertyBinder(Of Boolean)
        ''' <summary>
        ''' (未实施) 是否可见
        ''' </summary>
        Public ReadOnly Property IsVisible As New PropertyBinder(Of Boolean)
        ''' <summary>
        ''' (未实施) 渲染时需要处理的特效
        ''' </summary>
        Public ReadOnly Property Effect As GameEffect
        ''' <summary>
        ''' (未实施) 二维变换
        ''' </summary>
        Public ReadOnly Property Transform As PlaneTransform
        ''' <summary>
        ''' 如果这个对象被冻结，则不会主动进行更新。
        ''' </summary>
        Public ReadOnly Property IsFrozen As New PropertyBinder(Of Boolean)

        Dim _Triggers As New List(Of IGameTrigger)
        ''' <summary>
        ''' 已经安装了哪些触发器
        ''' </summary>
        Public ReadOnly Property Triggers As IReadOnlyList(Of IGameTrigger)
            Get
                Return _Triggers
            End Get
        End Property

        Friend Sub AddTrigger(trigger As IGameTrigger)
            _Triggers.Add(trigger)
        End Sub
        Friend Sub RemoveTrigger(trigger As IGameTrigger)
            _Triggers.Remove(trigger)
        End Sub

        Dim _Behaviors As New List(Of IGameBehavior)
        ''' <summary>
        ''' 已经安装了哪些附加行为
        ''' </summary>
        Public ReadOnly Property Behaviors As IReadOnlyList(Of IGameBehavior)
            Get
                Return _Behaviors
            End Get
        End Property

        Friend Sub AddBehavior(trigger As IGameBehavior)
            _Behaviors.Add(trigger)
        End Sub
        Friend Sub RemoveBehavior(trigger As IGameBehavior)
            _Behaviors.Remove(trigger)
        End Sub

        ''' <summary>
        ''' 主动请求从游戏画布移除
        ''' </summary>
        Public Event RemoveFromGameCanvasReuqested As EventHandler
        ''' <summary>
        ''' 从游戏画布移除
        ''' </summary>
        Public Sub RemoveFromGameCanvas()
            RaiseEvent RemoveFromGameCanvasReuqested(Me, EventArgs.Empty)
        End Sub

        ''' <summary>
        ''' 绑定更新的命令。默认值是引发Updating事件。
        ''' </summary>
        ''' <returns></returns>
        Public Overridable Property UpdateCommand As IGameCommand = New SimpleCommand(Sub() RaiseEvent Updating(Me, EventArgs.Empty))
        ''' <summary>
        ''' 需要更新的时候引发这个事件
        ''' </summary>
        Public Event Updating As GameObjectEventHandler(Of GameVisual, EventArgs)

        Sub New()
            CreateRenderer()
        End Sub

        Protected Overridable Sub CreateRenderer()
            Renderer = RendererBase.CreateVisualRenderer(Me)
        End Sub

        Public MustOverride Function GetChildEffectSources() As IEnumerable(Of IGameEffectSource) Implements IGameEffectSource.GetChildEffectSources

#Region "基本交互事件"
        ' 键盘
        Public Event KeyDown As GameObjectEventHandler(Of GameVisual, GameKeyboardEventArgs)
        Public Event KeyUp As GameObjectEventHandler(Of GameVisual, GameKeyboardEventArgs)
        ' 鼠标
        Public Event MouseButtonDown As GameObjectEventHandler(Of GameVisual, GameMouseEventArgs)
        Public Event MouseButtonUp As GameObjectEventHandler(Of GameVisual, GameMouseEventArgs)
        Public Event MouseWheelChanged As GameObjectEventHandler(Of GameVisual, GameMouseEventArgs)
        ' 触摸屏
        Public Event TouchDown As GameObjectEventHandler(Of GameVisual, GameTouchEventArgs)
        Public Event TouchUp As GameObjectEventHandler(Of GameVisual, GameTouchEventArgs)
#End Region
    End Class
End Namespace