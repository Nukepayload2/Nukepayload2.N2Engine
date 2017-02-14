Imports System.Reflection
Imports Nukepayload2.N2Engine.Foundation
Imports Nukepayload2.N2Engine.UI
Imports Nukepayload2.N2Engine.UI.Elements

Namespace Triggers
    ''' <summary>
    ''' 事件触发器的基类
    ''' </summary>
    Public MustInherit Class GameEventTrigger(Of TSender As GameObject, TEventArgs As EventArgs)
        Implements IGameTrigger
        ''' <summary>
        ''' 新建事件触发器
        ''' </summary>
        ''' <param name="eventName">事件名称。建议使用 NameOf (在 Visual C# 和 Visual F# 为 nameof) 运算符获取事件名称。</param>
        Sub New(eventName As String)
            Me.EventName = eventName
        End Sub
        ''' <summary>
        ''' 事件的名称
        ''' </summary>
        Public ReadOnly Property EventName As String
        ''' <summary>
        ''' 事件发生后的动作
        ''' </summary>
        Public MustOverride Sub Action(sender As TSender, e As TEventArgs)

        ''' <summary>
        ''' 将触发器附加到相关的可见对象上
        ''' </summary>
        ''' <param name="visual">要附加的可见对象</param>
        Public Sub Attach(visual As GameVisual) Implements IGameTrigger.Attach
            Dim ev = visual.GetType.GetRuntimeEvent(EventName)
            Dim handler As GameObjectEventHandler(Of TSender, TEventArgs) = AddressOf Action
            ev.AddEventHandler(visual, handler)
            visual.AddTrigger(Me)
        End Sub
        ''' <summary>
        ''' 将触发器脱离可见对象
        ''' </summary>
        ''' <param name="visual">要脱离的可见对象</param>
        Public Sub Detach(visual As GameVisual) Implements IGameTrigger.Detach
            Dim ev = visual.GetType.GetRuntimeEvent(EventName)
            Dim handler As GameObjectEventHandler(Of TSender, TEventArgs) = AddressOf Action
            ev.RemoveEventHandler(visual, handler)
            visual.RemoveTrigger(Me)
        End Sub
    End Class
    ''' <summary>
    ''' 事件触发就设定值的触发器
    ''' </summary>
    Public Class GameEventSetValueTrigger(Of T)
        Implements IGameTrigger
        ''' <summary>
        ''' 新建事件触发器
        ''' </summary>
        ''' <param name="eventName">事件名称。建议使用 NameOf (在 Visual C# 和 Visual F# 为 nameof) 运算符获取事件名称。</param>
        Sub New(eventName As String, getNewData As PropertyBinder(Of T), writeTo As PropertyBinder(Of T))
            Me.EventName = eventName
            Me.GetNewData = getNewData
            Me.WriteTo = writeTo
        End Sub
        ''' <summary>
        ''' 事件的名称
        ''' </summary>
        Public ReadOnly Property EventName As String
        ''' <summary>
        ''' 值的源头
        ''' </summary>
        Public Property GetNewData As PropertyBinder(Of T)
        ''' <summary>
        ''' 写入目标
        ''' </summary>
        Public Property WriteTo As PropertyBinder(Of T)

        Protected Overridable Sub Action()
            WriteTo.Value = GetNewData.Value
        End Sub
        ''' <summary>
        ''' 将触发器附加到相关的可见对象上
        ''' </summary>
        ''' <param name="visual">要附加的可见对象</param>
        Public Sub Attach(visual As GameVisual) Implements IGameTrigger.Attach
            Dim ev = visual.GetType.GetRuntimeEvent(EventName)
            Dim handler As Action = AddressOf Action
            ev.AddEventHandler(visual, handler)
            visual.AddTrigger(Me)
        End Sub
        ''' <summary>
        ''' 将触发器脱离可见对象
        ''' </summary>
        ''' <param name="visual">要脱离的可见对象</param>
        Public Sub Detach(visual As GameVisual) Implements IGameTrigger.Detach
            Dim ev = visual.GetType.GetRuntimeEvent(EventName)
            Dim handler As Action = AddressOf Action
            ev.RemoveEventHandler(visual, handler)
            visual.RemoveTrigger(Me)
        End Sub
    End Class
    ''' <summary>
    ''' 中继事件触发器
    ''' </summary>
    Public Class GameRelayEventTrigger
        Implements IGameTrigger
        ''' <summary>
        ''' 新建事件触发器
        ''' </summary>
        ''' <param name="eventName">事件名称。建议使用 NameOf (在 Visual C# 和 Visual F# 为 nameof) 运算符获取事件名称。</param>
        ''' <param name="action">事件处理程序的委托</param>
        Sub New(eventName As String, action As [Delegate])
            Me.EventName = eventName
            Me.Action = action
        End Sub
        ''' <summary>
        ''' 事件的名称
        ''' </summary>
        Public ReadOnly Property EventName As String
        ''' <summary>
        ''' 事件的动作
        ''' </summary>
        Public Property Action As [Delegate]
        ''' <summary>
        ''' 将触发器附加到相关的可见对象上
        ''' </summary>
        ''' <param name="visual">要附加的可见对象</param>
        Public Sub Attach(visual As GameVisual) Implements IGameTrigger.Attach
            Dim ev = visual.GetType.GetRuntimeEvent(EventName)
            ev.AddEventHandler(visual, Action)
            visual.AddTrigger(Me)
        End Sub
        ''' <summary>
        ''' 将触发器脱离可见对象
        ''' </summary>
        ''' <param name="visual">要脱离的可见对象</param>
        Public Sub Detach(visual As GameVisual) Implements IGameTrigger.Detach
            Dim ev = visual.GetType.GetRuntimeEvent(EventName)
            ev.RemoveEventHandler(visual, Action)
            visual.RemoveTrigger(Me)
        End Sub
    End Class

    ''' <summary>
    ''' 自定义注册和取消注册事件过程的中继事件触发器
    ''' </summary>
    Public Class CustomGameRelayEventTrigger
        Implements IGameTrigger

        Sub New(registerEvent As Action(Of GameVisual), unregisterEvent As Action(Of GameVisual))
            Me.RegisterEvent = registerEvent
            Me.UnregisterEvent = unregisterEvent
        End Sub
        ''' <summary>
        ''' 注册事件的委托
        ''' </summary>
        Public Property RegisterEvent As Action(Of GameVisual)
        ''' <summary>
        ''' 取消注册事件的委托
        ''' </summary>
        Public Property UnregisterEvent As Action(Of GameVisual)
        ''' <summary>
        ''' 附加到执行可见对象
        ''' </summary>
        ''' <param name="visual">要附加的可见对象</param>
        Public Sub Attach(visual As GameVisual) Implements IGameTrigger.Attach
            RegisterEvent.Invoke(visual)
        End Sub
        ''' <summary>
        ''' 从指定的可见对象分离
        ''' </summary>
        ''' <param name="visual">要分离的可见对象</param>
        Public Sub Detach(visual As GameVisual) Implements IGameTrigger.Detach
            UnregisterEvent.Invoke(visual)
        End Sub
    End Class

End Namespace