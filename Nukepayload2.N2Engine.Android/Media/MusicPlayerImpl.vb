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
    End Function

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

    Public Async Function SetPlayingIndexAsync(value As Integer) As Task Implements IMusicPlayer.SetPlayingIndexAsync
        Dim uri = Sources(value)
        Await mp.SetDataSourceAsync(AndroidUri.Parse(Resources.ResourceLoader.GetForCurrentView.GetResourceUri(uri).AbsolutePath))
        _PlayingIndex = value
    End Function
End Class
