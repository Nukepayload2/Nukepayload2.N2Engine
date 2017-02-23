Imports System.IO

Namespace Global.Nukepayload2.N2Engine.Storage
    Friend Class SaveManagerImpl
        Public Overrides Function OpenSaveFolderAsync(Location As SaveLocations) As Task(Of PlatformSaveDirectoryBase)
            Dim folder As String
            Select Case Location
                Case SaveLocations.Local
                    folder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)
                    folder = AddFolder(folder, VendorName)
                    folder = AddFolder(folder, "N2Engine")
                    folder = AddFolder(folder, Information.Environment.SharedLogicAssembly.FullName)
                Case SaveLocations.Roaming
                    folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
                    folder = AddFolder(folder, VendorName)
                    folder = AddFolder(folder, "N2Engine")
                    folder = AddFolder(folder, Information.Environment.SharedLogicAssembly.FullName)
                Case Else
                    folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
                    folder = AddFolder(folder, VendorName)
            End Select
            Return SaveFolder.CreateAsync(folder)
        End Function

        Private Shared Function AddFolder(folder As String, newFolder As String) As String
            folder = Path.Combine(folder, newFolder)
            If Not Directory.Exists(folder) Then
                Directory.CreateDirectory(folder)
            End If
            Return folder
        End Function
    End Class
End Namespace