Imports Nukepayload2.N2Engine.Shell.Commands
Imports Nukepayload2.N2Engine.Shell.Models
Imports Nukepayload2.N2Engine.Shell.Services
Imports Windows.Storage

Namespace ViewModels
    Public Class WelcomeViewModel
        Inherits SingleInstance(Of WelcomeViewModel)
        Implements INotifyPropertyChanged

        Public ReadOnly Property RecentAccessTable As New ObservableCollection(Of StorageFile)

        Public ReadOnly Property OpenCommand As New ProjectOpenCommand

        Public Property SupportedLanguages As ProgrammingLanguage() = {
            SupportedProgrammingLanguages.VisualBasic,
            SupportedProgrammingLanguages.CSharp,
            SupportedProgrammingLanguages.FSharp
        }

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        Public Property ProjectTemplates As New List(Of ProjectTemplate) From {
            New ProjectTemplate("空白模板", "空白.png", "创建游戏类型中立的项目。", New CreateEmptyCommand)
        }

        Public Async Function LoadAsync() As Task
            Dim recentWithAccess = Await RecentAccessListService.GetRecentFilesOrderByDateAccessedDescendingAsync()
            RecentAccessTable.Clear()
            For Each file In recentWithAccess
                RecentAccessTable.Add(file)
            Next
        End Function
    End Class
End Namespace