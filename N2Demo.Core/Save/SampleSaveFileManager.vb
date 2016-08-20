Imports Nukepayload2.N2Engine.Core

Public Class SampleSaveFileManager
    Inherits SaveManager

    Public Property MasterSaveFile As New SaveFile(Of SampleMasterData)(New SampleMasterData)
    Public Property SavePollution As New SaveFile(Of SampleSavePollution)(New SampleSavePollution)
    Public Property PartialSaveFiles As List(Of SaveFile(Of SamplePartialData))

    Protected Overrides Sub OnFileInitializing()
        MasterSaveFile.Master().
                       HasBaseName("N2Demo").
                       Roaming()
        SavePollution.Master().
                      HasBaseName("ExtraFlags").
                      Roaming().
                      HasSaveId(233).
                      Indelible()
        PartialSaveFiles = New PartialSaveFileLoader(Of SamplePartialData)(PlatformSaveManager.LocalPartialData).
                           HasBaseName("Branch")

    End Sub
End Class
