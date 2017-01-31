Imports Nukepayload2.N2Engine.Foundation
Imports Nukepayload2.N2Engine.Platform

Namespace Storage
    ''' <summary>
    ''' 存档管理器
    ''' </summary>
    Public MustInherit Class SaveManager
        Inherits SingleInstance(Of SaveManager)
        Sub New()
            PlatformSaveManager = PlatformActivator.CreateBaseInstance(Of SaveManager, PlatformSaveManagerBase)
            OnFileInitializing()
        End Sub
        ''' <summary>
        ''' 用于执行存档管理功能
        ''' </summary>
        ''' <returns></returns>
        Public Property PlatformSaveManager As PlatformSaveManagerBase
        ''' <summary>
        ''' 存档开始加载
        ''' </summary>
        Protected MustOverride Sub OnFileInitializing()
        ''' <summary>
        ''' 存档已加载
        ''' </summary>
        Protected Overridable Sub OnFileInitialized()
            RaiseEvent FileInitialized(Me, EventArgs.Empty)
        End Sub
        ''' <summary>
        ''' 存档文件已经加载时引发此事件。
        ''' </summary>
        Public Event FileInitialized As EventHandler
    End Class
End Namespace