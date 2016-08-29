Imports Nukepayload2.N2Engine.Storage

Public Class SampleSaveFileManager
    Inherits SaveManager

    Public Property MasterSaveFile As New SaveFile(Of SampleMasterData)(New SampleMasterData)
    Public Property SavePollution As New SaveFile(Of SampleSavePollution)(New SampleSavePollution)
    Public Property PartialSaveFiles As List(Of SaveFile(Of SamplePartialData))

    Protected Overrides Async Sub OnFileInitializing()
        MasterSaveFile.Master().
                       HasBaseName("N2Demo").
                       Roaming()
        SavePollution.Master().
                      HasBaseName("ExtraFlags").
                      Roaming().
                      HasSaveId(233).
                      Indelible()
        PartialSaveFiles = (Await PartialSaveFileLoader(Of SamplePartialData).CreateAsync(Await PlatformSaveManager.OpenSaveFolderAsync(SaveLocations.LocalPartial))).
                           HasBaseName("Branch")

    End Sub
End Class
