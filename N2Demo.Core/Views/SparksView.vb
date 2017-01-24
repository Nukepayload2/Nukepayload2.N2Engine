﻿Imports System.Numerics
Imports Nukepayload2.N2Engine.Resources
Imports Nukepayload2.N2Engine.UI
Imports Nukepayload2.N2Engine.UI.Elements
Imports Nukepayload2.N2Engine.UI.Views
Imports Nukepayload2.N2Engine.Media
Imports System.Threading

Public Class SparksView
    Inherits GameCanvas

    Dim redEllipse As New EllipseElement
    Dim greenRect As New RectangleElement
    Dim sparks As New SparkParticleSystemView
    Dim charaSheet As New SpriteElement
    Dim sparksData As New SparksViewModel
    Dim scrollViewer As New GameVisualizingScrollViewer
    Dim characterSheetSprite As BitmapResource
    Dim primaryBgm As New Uri("n2-res:///ProgramDirectory/Audios/Musics/Theme1.wma")
    Dim clockSound As New Uri("n2-res:///ProgramDirectory/Audios/Sounds/Explosion4.wma")
    Dim soundPlayer As ISoundVoicePlayer
    Dim isSoundLoaded As Boolean

    WithEvents MusicPlayer As IMusicPlayer

    Sub New()
        ApplyRoute()
        LoadSceneAsync()
    End Sub

    Private Async Sub LoadSceneAsync()
        ' 图像资源同步准备
        characterSheetSprite = BitmapResource.Create(sparksData.CharacterSheet.Source)

        Bind(Function(m) m.Location, New Vector2).
        Bind(Function(m) m.ZIndex, 0).
        AddChild(sparks.
            Bind(Function(s) s.Data, Function() sparksData.SparkSys)).
        AddChild(
            scrollViewer.
                OnUpdate(sparksData.ShakingViewer.UpdateCommand).
                Bind(Function(m) m.Location, Function() sparksData.ShakingViewer.Offset).
                Bind(Function(m) m.ZIndex, 0).
                AddChild(redEllipse.
                    Bind(Function(r) r.Fill, Function() sparksData.RedCircle.Color).
                    Bind(Function(r) r.Location, Function() sparksData.RedCircle.Position).
                    Bind(Function(r) r.Size, Function() sparksData.RedCircle.Size)).
                AddChild(greenRect.
                    Bind(Function(r) r.Stroke, Function() sparksData.GreenRectangle.Color).
                    Bind(Function(r) r.Location, Function() sparksData.GreenRectangle.Position).
                    Bind(Function(r) r.Size, Function() sparksData.GreenRectangle.Size)).
                AddChild(charaSheet.
                    Bind(Function(r) r.Sprite, Function() characterSheetSprite).
                    Bind(Function(r) r.Location, Function() sparksData.CharacterSheet.Size).
                    Bind(Function(r) r.Size, Function() sparksData.CharacterSheet.Location))
        )

        ' 延迟加载声音系统
        Dim audio = Await CreateAudioPlayersAsync()
        MusicPlayer = audio.Item1
        soundPlayer = audio.Item2
        Await MusicPlayer.SetSourcesAsync({primaryBgm})
        MusicPlayer.Volume = 0.7
        MusicPlayer.Play()
        soundPlayer.SoundVolume = 0.8

        isSoundLoaded = True
    End Sub

    Dim soundPlaying As Boolean
    ''' <summary>
    ''' 平台特定实现点击此视图时，调用此方法。通用的输入事件完成后，此方法将过时。
    ''' </summary>
    Public Async Sub OnTappedAsync(pos As Vector2)
        sparksData.SparkSys.Location = pos
        sparksData.ShakingViewer.Shake(50.0F, 0)
        If isSoundLoaded Then
            If Not soundPlaying Then
                soundPlaying = True
                Await soundPlayer.PlaySoundAsync(clockSound)
                Await Task.Delay(1000)
                soundPlaying = False
            End If
        End If
    End Sub

    Private Async Sub MusicPlayer_SingleSongCompleteAsync(sender As Object, e As EventArgs) Handles MusicPlayer.SingleSongComplete
        Await MusicPlayer.SetPlayingIndexAsync(0)
        MusicPlayer.Play()
    End Sub
End Class