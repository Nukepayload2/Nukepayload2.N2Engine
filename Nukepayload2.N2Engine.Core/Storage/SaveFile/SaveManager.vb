Imports Nukepayload2.N2Engine.Foundation
Imports Nukepayload2.N2Engine.Platform

Namespace Storage

    Public MustInherit Class SaveManager
        Inherits SingleInstance(Of SaveManager)
        Sub New()
            PlatformSaveManager = PlatformActivator.CreateBaseInstance(Of SaveManager, PlatformSaveManagerBase)
            OnFileInitializing()
        End Sub

        Public Property PlatformSaveManager As PlatformSaveManagerBase
        Protected MustOverride Sub OnFileInitializing()

    End Class
End Namespace