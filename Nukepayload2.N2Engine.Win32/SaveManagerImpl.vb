Imports System.IO
Imports Nukepayload2.N2Engine.Core
Imports Nukepayload2.N2Engine.Core.Storage
Imports Nukepayload2.N2Engine.Storage

Friend Class SaveManagerImpl
    Public Overrides Function OpenSaveFolderAsync(Location As SaveLocations) As Task(Of PlatformSaveDirectoryBase)
        Dim folder As String
        Select Case Location
            Case SaveLocations.LocalMaster
                folder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)
            Case SaveLocations.LocalPartial
                folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Partial")
            Case SaveLocations.Roaming
                folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
            Case Else
                folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), VendorName)
        End Select
        Return SaveFolder.CreateAsync(folder)
    End Function
End Class