Imports Foundation
Imports Nukepayload2.N2Engine.Core
Imports Nukepayload2.N2Engine.Core.Storage

Friend Class SaveManagerImpl
    Public Overrides Function OpenSaveFolderAsync(Location As SaveLocations) As Task(Of PlatformSaveDirectoryBase)
        Dim folder As String
        Dim localData = NSSearchPath.GetDirectories(NSSearchPathDirectory.DocumentDirectory, NSSearchPathDomain.User).Last
        Select Case Location
            Case SaveLocations.LocalMaster
                folder = localData
            Case SaveLocations.LocalPartial
                folder = Path.Combine(localData, "Partial")
            Case SaveLocations.Roaming
                Throw New PlatformNotSupportedException("iOS 不支持 iCloud 漫游数据")
            Case Else
                Throw New PlatformNotSupportedException("iOS 不支持 开发商共享数据")
        End Select

    End Function
End Class