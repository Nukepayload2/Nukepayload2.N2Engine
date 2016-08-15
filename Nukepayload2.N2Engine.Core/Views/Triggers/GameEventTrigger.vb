Imports System.Reflection
''' <summary>
''' 编写方便但是性能不好的事件触发器
''' </summary>
Public Class GameEventTrigger
    Implements IGameTrigger

    Sub New(eventName As String, action As [Delegate])
        Me.EventName = eventName
        Me.Action = action
    End Sub

    Public Property EventName As String
    Public Property Action As [Delegate]

    Public Sub Attach(visual As GameVisual) Implements IGameTrigger.Attach
        Dim ev = visual.GetType.GetRuntimeEvent(EventName)
        ev.AddEventHandler(visual, Action)
        visual.AddTrigger(Me)
    End Sub

    Public Sub Detach(visual As GameVisual) Implements IGameTrigger.Detach
        Dim ev = visual.GetType.GetRuntimeEvent(EventName)
        ev.RemoveEventHandler(visual, Action)
        visual.RemoveTrigger(Me)
    End Sub
End Class
''' <summary>
''' 高性能但是编写麻烦的事件触发器
''' </summary>
Public Class GameCustomEventTrigger(Of TSender As GameVisual)
    Inherits GameTriggerBase(Of TSender)

    Sub New(registerEvent As Action(Of TSender), unregisterEvent As Action(Of TSender))
        Me.RegisterEvent = registerEvent
        Me.UnregisterEvent = unregisterEvent
    End Sub

    Public Property RegisterEvent As Action(Of TSender)
    Public Property UnregisterEvent As Action(Of TSender)

    Public Overrides Sub Attach(visual As TSender)
        RegisterEvent.Invoke(visual)
        visual.AddTrigger(Me)
    End Sub

    Public Overrides Sub Detach(visual As TSender)
        UnregisterEvent.Invoke(visual)
        visual.RemoveTrigger(Me)
    End Sub
End Class