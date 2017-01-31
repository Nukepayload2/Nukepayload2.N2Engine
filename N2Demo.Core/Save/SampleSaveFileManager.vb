Imports Nukepayload2.N2Engine.Storage
''' <summary>
''' 这是示例的存档文件
''' </summary>
Public Class SampleSaveFileManager
    Inherits SaveManager

    ''' <summary>
    ''' 可删除的主要存档文件。主存档文件保存到 N2Demo 文件。
    ''' </summary>
    Public Property MasterSaveFile As New SaveFile(Of SampleMasterData)(New SampleMasterData) With {
        .IsMaster = True, .BaseName = "N2Demo"
    }

    ''' <summary>
    ''' 针对主存档的存档污染文件。主存档污染文件保存到 ExtraFlags233 文件，可漫游。
    ''' </summary>
    ''' <returns></returns>
    Public Property SavePollution As New SaveFile(Of SampleSavePollution)(New SampleSavePollution) With {
        .IsMaster = True, .BaseName = "ExtraFlags", .IsRoaming = True, .SaveId = 233, .IsIndelible = True
    }

    ''' <summary>
    ''' 分支存档。
    ''' </summary>
    Public Property PartialSaveFiles As List(Of SaveFile(Of SamplePartialData))

    ''' <summary>
    ''' 加载存档文件
    ''' </summary>
    Protected Overrides Async Sub OnFileInitializing()
        ' 主存档污染文件保存到 ExtraFlags233 文件，可漫游。
        Dim savFolder = Await PlatformSaveManager.OpenSaveFolderAsync(SaveLocations.Local)
        ' 存档加载器可以隐式转换存档的加载结果
        PartialSaveFiles = Await BranchSaveFilesLoader(Of SamplePartialData).CreateAsync(savFolder, "Branch")
        ' 加载完存档一定要告知存档已经加载。
        OnFileInitialized()
    End Sub

    ''' <summary>
    ''' 加载主要的存档文件
    ''' </summary>
    Public Async Function LoadMasterSaveFileAsync() As Task(Of SaveFile(Of SampleMasterData))
        Dim localDir = Await PlatformSaveManager.OpenSaveFolderAsync(SaveLocations.Local)
        Dim actualMasterFiles = From sav In Await localDir.GetSaveFilesAsync(Of SampleMasterData)
                                Where sav.IsMaster AndAlso sav.BaseName = MasterSaveFile.BaseName
        If actualMasterFiles.Any Then
            MasterSaveFile = actualMasterFiles.First
            Await localDir.LoadAsync(MasterSaveFile, Function(s) s)
            Return MasterSaveFile
        End If
        Return Nothing
    End Function

    ''' <summary>
    ''' 保存主要的存档文件
    ''' </summary>
    Public Async Function SaveMasterSaveFileAsync() As Task
        Dim localDir = Await PlatformSaveManager.OpenSaveFolderAsync(SaveLocations.Local)
        Await localDir.SaveAsync(MasterSaveFile, Function(s) s)
    End Function
End Class
