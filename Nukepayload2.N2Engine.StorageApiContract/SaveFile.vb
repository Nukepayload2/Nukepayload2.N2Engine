Namespace Storage

    ''' <summary>
    ''' 包含数据的存档
    ''' </summary>
    ''' <typeparam name="TData">存档数据模型</typeparam>
    Public Class SaveFile(Of TData)
        ''' <summary>
        ''' 这个存档当前的状态
        ''' </summary>
        Public Property Status As SaveFileStatus
        ''' <summary>
        ''' 使用存档擦除功能时，如果不强制完全擦除，则不会擦除这个存档。
        ''' </summary>
        Public Property IsIndelible As Boolean
        ''' <summary>
        ''' 是主存档，而不是分支存档。
        ''' </summary>
        Public Property IsMaster As Boolean
        ''' <summary>
        ''' 不带编号的文件名
        ''' </summary>
        Public Property BaseName As String
        ''' <summary>
        ''' 存档文件的编号
        ''' </summary>
        Public Property SaveId As Integer?

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

        ''' <summary>
        ''' 这个存档会自动上传到游戏账号
        ''' </summary>
        Public Property IsRoaming As Boolean

        Private _SaveData As TData

        ''' <summary>
        ''' 仅指定存档类型，不加载存档。
        ''' </summary>
        Sub New()

        End Sub

        ''' <summary>
        ''' 加载存档
        ''' </summary>
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