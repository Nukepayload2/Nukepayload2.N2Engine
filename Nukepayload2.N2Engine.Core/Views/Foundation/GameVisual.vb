''' <summary>
''' 游戏中的基本可见元素
''' </summary>
Public MustInherit Class GameVisual
    Inherits GameObject
    ''' <summary>
    ''' 渲染时需要处理的特效
    ''' </summary>
    Public ReadOnly Property Effect As New PropertyBinder(Of GameEffect)

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
End Class