Imports Windows.Media.Audio
Imports Windows.Media.Render
Imports Windows.Storage

Namespace Media
    ''' <summary>
    ''' 使用 <see cref="AudioGraph"/> 回放音乐。支持 wma,wav,mp3,m4a 等格式。
    ''' </summary>
    Public Class MusicPlayback
        Implements IDisposable, INotifyPropertyChanged

        ''' <summary>
        ''' 音频图
        ''' </summary>
        Public ReadOnly Property Graph As AudioGraph
        ''' <summary>
        ''' 音乐文件和它的播放属性（如，进度，速度）
        ''' </summary>
        Public ReadOnly Property MusicFileInput As AudioFileInputNode
        ''' <summary>
        ''' 输出设备节点。用于添加音效等。
        ''' </summary>
        Public ReadOnly Property DeviceOutput As AudioDeviceOutputNode
        ''' <summary>
        ''' 表示音乐的输出增益。
        ''' </summary>
        Public Property Volume As Double
            Get
                Return If(DeviceOutput?.OutgoingGain, 0)
            End Get
            Set
                If DeviceOutput IsNot Nothing Then
                    DeviceOutput.OutgoingGain = Value
                Else
                    Throw New InvalidOperationException("尚未初始化输出设备节点, 无法调节输出增益。")
                End If
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(Volume)))
            End Set
        End Property

        Dim _IsPlaying As Boolean
        ''' <summary>
        ''' 是否正在播放音乐
        ''' </summary>
        Public Property IsPlaying As Boolean
            Get
                Return _IsPlaying
            End Get
            Set(value As Boolean)
                _IsPlaying = value
                If _IsPlaying Then
                    Graph.Start()
                Else
                    Graph.[Stop]()
                End If
                RaisePropertyChanged(NameOf(IsPlaying))
            End Set
        End Property

        Dim _FileLoaded As Boolean
        ''' <summary>
        ''' 是否已经加载了媒体
        ''' </summary>
        Public Property FileLoaded As Boolean
            Get
                Return _FileLoaded
            End Get
            Protected Set
                _FileLoaded = Value
                RaisePropertyChanged(NameOf(FileLoaded))
            End Set
        End Property
        ''' <summary>
        ''' 播放速度。默认是1 。
        ''' </summary>
        Public Property Speed As Double
            Get
                Return If(MusicFileInput?.PlaybackSpeedFactor, 1.0)
            End Get
            Set
                If MusicFileInput IsNot Nothing Then
                    MusicFileInput.PlaybackSpeedFactor = Value
                Else
                    Throw New InvalidOperationException("还没加载文件，不能调节速度")
                End If
                RaisePropertyChanged(NameOf(Speed))
            End Set
        End Property

        Dim _IsLooping As Boolean
        Public Property IsLooping As Boolean
            Get
                Return _IsLooping
            End Get
            Set(value As Boolean)
                _IsLooping = value
                If _IsLooping Then
                    ' 确保还没播放完
                    If MusicFileInput.Position >= MusicFileInput.Duration Then
                        ' 播放完了就回开始
                        MusicFileInput.Seek(MusicFileInput.StartTime.Value)
                    End If
                    ' 无限循环
                    MusicFileInput.LoopCount = Nothing
                Else
                    ' 停止循环
                    MusicFileInput.LoopCount = 0
                End If
            End Set
        End Property
        ''' <summary>
        ''' 为派生类提供的属性变更通知
        ''' </summary>
        ''' <param name="propName"></param>
        Protected Sub RaisePropertyChanged(propName As String)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propName))
        End Sub
        ''' <summary>
        ''' 加载某个音频文件
        ''' </summary>
        Public Async Function LoadFileAsync(file As StorageFile) As Task
            ' 没选文件
            If file Is Nothing Then
                Return
            End If

            ' 如果另一个文件加载了
            If MusicFileInput IsNot Nothing Then
                MusicFileInput.Dispose()
                ' 不再播放
                If IsPlaying Then
                    IsPlaying = False
                End If
            End If

            Dim fileInputResult = Await Graph.CreateFileInputNodeAsync(file)
            If AudioFileNodeCreationStatus.Success <> fileInputResult.Status Then
                ' 读取文件错误
                Throw New IOException($"读取文件错误: {fileInputResult.Status}")
            End If

            _MusicFileInput = fileInputResult.FileInputNode
            MusicFileInput.AddOutgoingConnection(DeviceOutput)

            Speed = 1
            ' 读取成功
            FileLoaded = True
        End Function
        ''' <summary>
        ''' 加载音频图和输出设备。这个操作应该在Loading界面显示的时候进行。
        ''' </summary>
        ''' <returns></returns>
        Public Async Function LoadAsync() As Task
            Dim settings As New AudioGraphSettings(AudioRenderCategory.Media)
            Dim result = Await AudioGraph.CreateAsync(settings)
            If result.Status <> AudioGraphCreationStatus.Success Then
                Throw New InvalidOperationException($"无法创建 AudioGraph : { result.Status}")
            End If
            _Graph = result.Graph
            Dim deviceOutputNodeResult = Await Graph.CreateDeviceOutputNodeAsync()
            If deviceOutputNodeResult.Status <> AudioDeviceNodeCreationStatus.Success Then
                Throw New InvalidOperationException($"设备输入节点创建失败: {deviceOutputNodeResult.Status}")
            End If
            _DeviceOutput = deviceOutputNodeResult.DeviceOutputNode
        End Function

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

#Region "IDisposable Support"
        Private disposedValue As Boolean ' 要检测冗余调用

        ' IDisposable
        Protected Sub Dispose(disposing As Boolean)
            If Not disposedValue Then
                If disposing Then
                    Graph?.Dispose()
                    MusicFileInput?.Dispose()
                    DeviceOutput?.Dispose()
                End If
            End If
            disposedValue = True
        End Sub
        Public Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
        End Sub
#End Region
    End Class
End Namespace
