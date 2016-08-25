Imports Nukepayload2.N2Engine.Core
Imports Nukepayload2.N2Engine.Core.Storage
Imports Nukepayload2.N2Engine.WinRT.Storage

Friend Class SaveManagerImpl
    Public Overrides Async Function OpenSaveFolderAsync(Location As SaveLocations) As Task(Of PlatformSaveDirectoryBase)
        Return Await SaveFolder.CreateAsync(Location)
    End Function
End Class