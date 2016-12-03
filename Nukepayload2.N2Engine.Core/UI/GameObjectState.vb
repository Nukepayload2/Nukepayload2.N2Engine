Imports Nukepayload2.N2Engine.Triggers
Imports Nukepayload2.N2Engine.UI.Elements

Namespace UI
    ''' <summary>
    ''' 表示游戏对象的一个状态
    ''' </summary>
    Public Class GameObjectState
        Inherits GameObject
        ''' <summary>
        ''' 获取其父 <see cref="GameObjectStateManager"/> 对象 
        ''' </summary>
        Public Property Parent As GameObjectStateManager
        ''' <summary>
        ''' 与本状态关联的存储在相应 <see cref="GameVisual"/> 的触发器。
        ''' </summary>
        Public Property Triggers As IGameTrigger
        ''' <summary>
        ''' 当任意一个 Trigger 被满足时，执行动作。
        ''' </summary>
        Public Property Actions As IGameAction

    End Class
End Namespace