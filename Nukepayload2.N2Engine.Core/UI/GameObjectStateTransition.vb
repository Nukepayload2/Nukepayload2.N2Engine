Namespace UI
    ''' <summary>
    ''' 游戏对象状态切换的定义
    ''' </summary>
    Public Class GameObjectStateTransition
        Public Property FromState As GameObjectState
        Public Property ToState As GameObjectState
        Public Property Action As IGameAction
    End Class
End Namespace
