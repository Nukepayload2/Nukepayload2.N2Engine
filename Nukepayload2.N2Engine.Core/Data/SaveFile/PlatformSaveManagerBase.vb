Imports Nukepayload2.N2Engine.Core.Storage

Public MustInherit Class PlatformSaveManagerBase
    ''' <summary>
    ''' 异步打开存档文件夹
    ''' </summary>
    Public MustOverride Function OpenSaveFolderAsync(Location As SaveLocations) As Task(Of PlatformSaveDirectoryBase)
End Class
