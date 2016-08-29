Namespace Global.Nukepayload2.N2Engine.Storage
    Friend Class SaveManagerImpl
        Public Overrides Async Function OpenSaveFolderAsync(Location As SaveLocations) As Task(Of PlatformSaveDirectoryBase)
            Return Await SaveFolder.CreateAsync(Location)
        End Function
    End Class
End Namespace