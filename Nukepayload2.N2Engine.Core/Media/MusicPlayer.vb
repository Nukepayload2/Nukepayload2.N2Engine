Imports Nukepayload2.N2Engine.Platform

Namespace Media
    Public Class MusicPlayer
        Implements IMusicPlayer, IDisposable

        Dim platformPlayer As IMusicPlayer
        Sub New()
            platformPlayer = PlatformActivator.CreateBaseInstance(Of MusicPlayer, IMusicPlayer)()
            AddHandler platformPlayer.SingleSongComplete, AddressOf OnSingleSongComplete
        End Sub

        Public ReadOnly Property PlayingIndex As Integer Implements IMusicPlayer.PlayingIndex
            Get
                Return platformPlayer.PlayingIndex
            End Get
        End Property

        Public ReadOnly Property Sources As IReadOnlyList(Of Uri) Implements IMusicPlayer.Sources
            Get
                Return platformPlayer.Sources
            End Get
        End Property

        Public Property Volume As Double Implements IMusicPlayer.Volume
            Get
                Return platformPlayer.Volume
            End Get
            Set(value As Double)
                platformPlayer.Volume = value
            End Set
        End Property

        Public Custom Event SingleSongComplete As EventHandler Implements IMusicPlayer.SingleSongComplete
            AddHandler(value As EventHandler)
                AddHandler platformPlayer.SingleSongComplete, value
            End AddHandler
            RemoveHandler(value As EventHandler)
                RemoveHandler platformPlayer.SingleSongComplete, value
            End RemoveHandler
            RaiseEvent(sender As Object, e As EventArgs)
                OnSingleSongComplete(sender, e)
            End RaiseEvent
        End Event

        Private Async Sub OnSingleSongComplete(sender As Object, e As EventArgs)
            If Sources.Count > 0 Then
                Dim newIndex = (PlayingIndex + 1) Mod Sources.Count
                Await SetPlayingIndexAsync(newIndex)
            End If
        End Sub

        Public Sub Pause() Implements IMusicPlayer.Pause
            platformPlayer.Pause()
        End Sub

        Public Sub Play() Implements IMusicPlayer.Play
            platformPlayer.Play()
        End Sub

        Public Sub StopMusic() Implements IMusicPlayer.StopMusic
            platformPlayer.StopMusic()
        End Sub

        Public Async Function SetPlayingIndexAsync(value As Integer) As Task Implements IMusicPlayer.SetPlayingIndexAsync
            Await platformPlayer.SetPlayingIndexAsync(value)
        End Function

        Public Async Function SetSourcesAsync(value As IReadOnlyList(Of Uri)) As Task Implements IMusicPlayer.SetSourcesAsync
            Await platformPlayer.SetSourcesAsync(value)
        End Function

#Region "IDisposable Support"
        Private disposedValue As Boolean ' 要检测冗余调用

        ' IDisposable
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not disposedValue Then
                If disposing Then
                    ' TODO: 释放托管状态(托管对象)。
                    RemoveHandler platformPlayer.SingleSongComplete, AddressOf OnSingleSongComplete
                End If

                ' TODO: 释放未托管资源(未托管对象)并在以下内容中替代 Finalize()。
                ' TODO: 将大型字段设置为 null。
            End If
            disposedValue = True
        End Sub

        ' TODO: 仅当以上 Dispose(disposing As Boolean)拥有用于释放未托管资源的代码时才替代 Finalize()。
        'Protected Overrides Sub Finalize()
        '    ' 请勿更改此代码。将清理代码放入以上 Dispose(disposing As Boolean)中。
        '    Dispose(False)
        '    MyBase.Finalize()
        'End Sub

        ' Visual Basic 添加此代码以正确实现可释放模式。
        Public Sub Dispose() Implements IDisposable.Dispose
            ' 请勿更改此代码。将清理代码放入以上 Dispose(disposing As Boolean)中。
            Dispose(True)
            ' TODO: 如果在以上内容中替代了 Finalize()，则取消注释以下行。
            ' GC.SuppressFinalize(Me)
        End Sub
#End Region
    End Class
End Namespace