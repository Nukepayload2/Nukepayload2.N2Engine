Public Interface IExtendable
    Event ExtensionInstallationRequested As EventHandler
    Sub InstallExtensions()
    Property Extensions As List(Of IGameExtensionInfo)

End Interface