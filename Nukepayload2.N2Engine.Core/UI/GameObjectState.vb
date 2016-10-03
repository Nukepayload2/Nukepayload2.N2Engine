Imports Nukepayload2.N2Engine.Triggers
Imports Nukepayload2.N2Engine.UI.Elements

Namespace UI
    Public Class GameObjectState
        Inherits GameObject
        ''' <summary>
        ''' 获取其父 <see cref="GameObjectStateManager"/> 对象 
        ''' </summary>
        Public Property Parent As GameObjectStateManager
        ''' <summary>
        ''' 与本状态关联的存储在相应 <see cref="GameVisual"/> 的触发器。任意一个触发器满足条件，都会引发动作列表。
        ''' </summary>
        Public Property Triggers As New List(Of IGameTrigger)
        ''' <summary>
        ''' 当任意一个 Trigger 被满足时，执行这些动作。
        ''' </summary>
        Public Property Actions As New List(Of ITimeAction)

    End Class
End Namespace