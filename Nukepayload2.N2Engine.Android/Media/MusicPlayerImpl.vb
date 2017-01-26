Imports Android.App
Imports Android.Media
Imports Nukepayload2.N2Engine.Media
Imports AndroidUri = Android.Net.Uri

Friend Class MusicPlayerImpl
    Dim mp As MediaPlayer
    Dim _volume As Double

    Public Async Function LoadAsync() As Task Implements IMusicPlayer.LoadAsync
        mp = New MediaPlayer
        Await Task.Delay(0)
        Dim am = AudioManager.FromContext(Application.Context)
        Dim vol = am.GetStreamVolume(Stream.Music)
        Dim vMax = am.GetStreamMaxVolume(Stream.Music)
        _volume = vol / vMax
        mp.SetOnCompletionListener(New OnCompletionListener(Me))
        mp.SetOnPreparedListener(New PreparedListener(Me))
    End Function

    Private Sub RaiseComplete()
        RaiseEvent SingleSongComplete(Me, EventArgs.Empty)
    End Sub

    Private Class OnCompletionListener
        Inherits Java.Lang.Object
        Implements MediaPlayer.IOnCompletionListener

        Dim _parent As MusicPlayerImpl
        Sub New(parent As MusicPlayerImpl)
            _parent = parent
        End Sub

        Public Sub OnCompletion(mp As MediaPlayer) Implements MediaPlayer.IOnCompletionListener.OnCompletion
            _parent.RaiseComplete()
        End Sub

    End Class

    Public Property Volume As Double Implements IMusicPlayer.Volume
        Get
            Return _volume
        End Get
        Set(value As Double)
            _volume = value
            mp.SetVolume(value, value)
        End Set
    End Property

    Public Sub Pause() Implements IMusicPlayer.Pause
        mp.Pause()
    End Sub

    Public Sub Play() Implements IMusicPlayer.Play
        mp.Start()
    End Sub

    Public Sub [Stop]() Implements IMusicPlayer.Stop
        mp.Stop()
    End Sub

    Dim waitingForPrepared As Boolean

    Private Class PreparedListener
        Inherits Java.Lang.Object
        Implements MediaPlayer.IOnPreparedListener

        Dim parent As MusicPlayerImpl

        Sub New(parent As MusicPlayerImpl)
            Me.parent = parent
        End Sub

        Public Sub OnPrepared(mp As MediaPlayer) Implements MediaPlayer.IOnPreparedListener.OnPrepared
            parent.waitingForPrepared = False
        End Sub
    End Class

    Public Async Function SetPlayingIndexAsync(value As Integer) As Task Implements IMusicPlayer.SetPlayingIndexAsync
        Try
            Dim uri = Sources(value)
            Dim absolutePath = Resources.ResourceLoader.GetForCurrentView.GetResourceUri(uri).ToString
            Debug.WriteLine(absolutePath)
            mp.Reset()
            Dim musicUri = AndroidUri.Parse(absolutePath)
            Await mp.SetDataSourceAsync(Application.Context, musicUri)
            mp.SetAudioStreamType(Stream.Music)
            waitingForPrepared = True
            mp.PrepareAsync()
            Do While waitingForPrepared
                Await Task.Delay(50)
            Loop
            Volume = Volume
            _PlayingIndex = value
        Catch ex As Exception
            Debug.WriteLine("播放声音出现问题：")
            Debug.WriteLine(ex)
            Debug.WriteLine("播放声音问题描述结束。")
            Throw
        End Try
    End Function

    Private Sub RemoveGlobalHandlers()
        mp.Release()
        mp.Dispose()
    End Sub
End Class
