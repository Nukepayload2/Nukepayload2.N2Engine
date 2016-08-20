Public MustInherit Class SaveManager
    Inherits SingleInstance(Of SaveManager)
    Sub New()
        PlatformSaveManager = DirectCast(PlatformActivator.CreateInstance(Of SaveManager), PlatformSaveManagerBase)
        OnFileInitializing()
    End Sub

    Public Property PlatformSaveManager As PlatformSaveManagerBase
    Protected MustOverride Sub OnFileInitializing()

End Class
