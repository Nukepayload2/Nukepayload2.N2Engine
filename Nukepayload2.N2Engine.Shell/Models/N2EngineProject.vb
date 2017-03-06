Imports Newtonsoft.Json
Imports Nukepayload2.N2Engine.Shell.Commands
Imports Windows.Storage

Namespace Models
    ''' <summary>
    ''' N2引擎项目
    ''' </summary>
    Public Class N2EngineProject
        Implements INotifyPropertyChanged

        Friend Const FileExtension = ".n2proj"

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        Public Shared Property ActiveProject As N2EngineProject

#Region "文件内容"
        <JsonProperty>
        Public Property Title$
        <JsonProperty>
        Public Property AuthorInformation$
        <JsonProperty>
        Public Property Content$
#End Region

        Public Property TargetFile As IStorageFile

#Region "命令"

        Public Property SaveAsCommand As ICommand = New ProjectSaveAsCommand

        Public Property SaveFileCommand As ICommand = New ProjectSaveCommand

        Public Property OpenFileCommand As ICommand = New ProjectOpenCommand

        Public Property CloseFileCommand As ICommand = New ProjectCloseCommand

#End Region

#Region "文档状态"

        Dim _IsBusy As Boolean
        Public Property IsBusy As Boolean
            Get
                Return _IsBusy
            End Get
            Set(value As Boolean)
                _IsBusy = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(IsBusy)))
            End Set
        End Property

        Dim _ErrorMessage As String
        Public Property ErrorMessage As String
            Get
                Return _ErrorMessage
            End Get
            Set(value As String)
                _ErrorMessage = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(ErrorMessage)))
            End Set
        End Property

#End Region

        Sub New()
            _ActiveProject = Me
        End Sub

    End Class

End Namespace
