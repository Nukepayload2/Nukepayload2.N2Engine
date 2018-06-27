Imports Nukepayload2.N2Engine.Resources
Imports Nukepayload2.N2Engine.Threading

Namespace UI
    ''' <summary>
    ''' 游戏中最基础的对象，可以是可见的，也可以不可见。
    ''' </summary>
    Public MustInherit Class GameObject
        ''' <summary>
        ''' (未实施) 用于在游戏画布线程执行某些代码
        ''' </summary>
        Public Property Dispatcher As GameDispatcher
        ''' <summary>
        ''' (未实施) 描述游戏对象切换状态时的行为
        ''' </summary>
        Public Property StateManager As GameObjectStateManager
        ''' <summary>
        ''' 查找游戏对象树时，可按名称查找
        ''' </summary>
        Public Property Name As String
        ''' <summary>
        ''' 平台无关的资源
        ''' </summary>
        Public Property Resources As New Dictionary(Of String, GameResourceBase)
        ''' <summary>
        ''' 初始化 <see cref="Resources"/>
        ''' </summary>
        Protected Overridable Sub CreateResources()

        End Sub

        Sub New()
            CreateResources()
        End Sub

    End Class
End Namespace