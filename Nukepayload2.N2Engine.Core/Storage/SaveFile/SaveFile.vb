Namespace Storage

    ''' <summary>
    ''' 空的存档文件。建议使用泛型版本的这个类作为存档文件。
    ''' </summary>
    Public Class SaveFile
        ''' <summary>
        ''' 这个存档当前的状态
        ''' </summary>
        Public Property Status As SaveFileStatus

        Private _IsIndelible As Boolean
        ''' <summary>
        ''' 使用存档擦除功能时，如果不强制完全擦除，则不会擦除这个存档。
        ''' </summary>
        Public Property IsIndelible As Boolean
            Get
                Return _IsIndelible
            End Get
            Friend Set
                _IsIndelible = Value
            End Set
        End Property

        Private _IsMaster As Boolean
        ''' <summary>
        ''' 是主存档，而不是分支存档。
        ''' </summary>
        Public Property IsMaster As Boolean
            Get
                Return _IsMaster
            End Get
            Friend Set
                _IsMaster = Value
            End Set
        End Property

        Private _BaseName As String
        ''' <summary>
        ''' 不带编号的文件名
        ''' </summary>
        Public Property BaseName As String
            Get
                Return _BaseName
            End Get
            Friend Set
                _BaseName = Value
            End Set
        End Property

        Private _SaveId As Integer?
        ''' <summary>
        ''' 存档文件的编号
        ''' </summary>
        Public Property SaveId As Integer?
            Get
                Return _SaveId
            End Get
            Friend Set
                _SaveId = Value
            End Set
        End Property
        ''' <summary>
        ''' 合成一个文件名
        ''' </summary>
        Public ReadOnly Property FileName As String
            Get
                Return BaseName + If(SaveId.HasValue, SaveId.Value.ToString, String.Empty) + ".n2sav"
            End Get
        End Property

        ''' <summary>
        ''' 读取存档时，这个属性存放原始的文件名
        ''' </summary>
        Public Property OriginalFileName As String

        Private _IsRoaming As Boolean

        ''' <summary>
        ''' 这个存档会自动上传到游戏账号
        ''' </summary>
        Public Property IsRoaming As Boolean
            Get
                Return _IsRoaming
            End Get
            Friend Set
                _IsRoaming = Value
            End Set
        End Property

    End Class

    Public Class SaveFile(Of TData)
        Inherits SaveFile

        Private _SaveData As TData

        Sub New(saveData As TData)
            Me.SaveData = saveData
        End Sub

        ''' <summary>
        ''' 游戏存储的数据
        ''' </summary>
        Public Property SaveData As TData
            Get
                Return _SaveData
            End Get
            Set
                _SaveData = Value
                Status = If(Status = SaveFileStatus.Initialized, SaveFileStatus.Loaded, SaveFileStatus.Modified)
            End Set
        End Property
    End Class
End Namespace