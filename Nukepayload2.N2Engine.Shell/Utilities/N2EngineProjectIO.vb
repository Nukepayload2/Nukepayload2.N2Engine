Imports Newtonsoft.Json
Imports Nukepayload2.N2Engine.Shell.Models
Imports Windows.Storage
Imports Windows.Storage.AccessCache
Imports Windows.Storage.Pickers

Namespace Utilities

    Public Class N2EngineProjectIO

        ''' <summary>
        ''' 保存N2引擎项目存储文件。如果之前没保存过，选取一个新的文件。
        ''' </summary>
        ''' <exception cref="OperationCanceledException"/>
        Public Shared Async Function SaveAsync(model As N2EngineProject) As Task
            Dim file = model.TargetFile
            If file IsNot Nothing Then
                StorageApplicationPermissions.MostRecentlyUsedList.AddOrReplace(file.Name, file)
                Dim doc = JsonConvert.SerializeObject(model)
                model.IsBusy = True
                Try
                    Await FileIO.WriteTextAsync(file, doc)
                Catch ex As Exception
                    model.ErrorMessage = ex.Message
                Finally
                    model.IsBusy = False
                End Try
            Else
                Await PickNewFileAndSaveAsAsync(model)
            End If
        End Function

        ''' <summary>
        ''' 选取一个新的N2引擎项目并打开。
        ''' </summary>
        ''' <exception cref="OperationCanceledException"/>
        Public Shared Async Function OpenAsync() As Task(Of N2EngineProject)
            Dim picker As New FileOpenPicker
            picker.FileTypeFilter.Add(N2EngineProject.FileExtension)
            Dim targetFile = Await picker.PickSingleFileAsync
            If targetFile IsNot Nothing Then
                Return Await OpenAsync(targetFile)
            Else
                Throw New OperationCanceledException
            End If
        End Function

        ''' <summary>
        ''' 在指定的文件读取N2引擎项目。
        ''' </summary>
        ''' <param name="targetFile">存储N2引擎项目的文件</param>
        Public Shared Async Function OpenAsync(targetFile As IStorageFile) As Task(Of N2EngineProject)
            StorageApplicationPermissions.MostRecentlyUsedList.AddOrReplace(targetFile.Name, targetFile)
            Dim content = Await FileIO.ReadTextAsync(targetFile)
            Dim document = JsonConvert.DeserializeObject(Of N2EngineProject)(content)
            document.TargetFile = targetFile
            Return document
        End Function

        ''' <summary>
        ''' 选取一个N2引擎项目存储，然后保存文件。
        ''' </summary>
        ''' <exception cref="OperationCanceledException"/>
        Public Shared Async Function PickNewFileAndSaveAsAsync(model As N2EngineProject) As Task
            Dim file = model.TargetFile
            Dim picker As New FileSavePicker
            picker.FileTypeChoices.Add("N2引擎项目", {N2EngineProject.FileExtension})
            file = Await picker.PickSaveFileAsync
            model.TargetFile = file
            If file Is Nothing Then
                Throw New OperationCanceledException
            End If
            Await SaveAsync(model)
        End Function
    End Class

End Namespace