Imports System.Numerics
Imports Nukepayload2.N2Engine.Resources
Imports Nukepayload2.N2Engine.UI
Imports Nukepayload2.N2Engine.UI.Elements
Imports Nukepayload2.N2Engine.UI.Views
Imports Nukepayload2.N2Engine.Media
Imports Nukepayload2.N2Engine.Information
Imports System.Reflection

''' <summary>
''' 一个释放随机颜色火花的视图
''' </summary>
Public Class SparksView
    Inherits GameCanvas

#Region "可见元素"
    Dim redEllipse As New EllipseElement
    Dim greenRect As New RectangleElement
    Dim sparks As New SparkParticleSystemView
    Dim charaSheet As New SpriteElement
    Dim scrollViewer As New GameVisualizingScrollViewer

#End Region

#Region "外部资源"
    Dim characterSheetSprite As BitmapResource
    Dim primaryBgm As New Uri("n2-res:///ProgramDirectory/Audios/Musics/Theme1.wma")
    Dim clockSound As New Uri("n2-res:///ProgramDirectory/Audios/Sounds/Explosion4.wma")

#End Region

#Region "媒体播放"
    WithEvents MusicPlayer As IMusicPlayer
    Dim soundPlayer As ISoundVoicePlayer
    Dim isSoundLoaded As Boolean
    Dim soundPlaying As Boolean

#End Region

    ' 数据
    Dim sparksData As New SparksViewModel
    ' 存档管理器
    Dim savMgr As SampleSaveFileManager

    Sub New()
        ' 准备存档
        GameInformation.SharedLogicAssembly = [GetType].GetTypeInfo.Assembly
        savMgr = New SampleSaveFileManager
        ' 准备资源路由
        ApplyRoute()
        ' 注册这个程序集为共享逻辑程序集
        LoadSceneAsync()
    End Sub

    Private Async Sub LoadSceneAsync()
        ' 图像资源同步准备
        characterSheetSprite = BitmapResource.Create(sparksData.CharacterSheet.Source)

        ' 可见对象树
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

        ' 读档
        Dim sav = Await savMgr.LoadMasterSaveFileAsync
        ' 同步数据
        If sav IsNot Nothing Then
            sparksData = sav.SaveData.LastState
        Else
            savMgr.MasterSaveFile.SaveData.LastState = sparksData
        End If
    End Sub

    ''' <summary>
    ''' 平台特定实现点击此视图时，调用此方法。通用的输入事件完成后，此方法将过时。
    ''' </summary>
    Public Async Sub OnTappedAsync(pos As Vector2)
        sparksData.SparkSys.Location = pos
        sparksData.ShakingViewer.Shake(50.0F, 0)
        If isSoundLoaded Then
            If Not soundPlaying Then
                soundPlaying = True
                ' 播放声音，最短间隔 1000 毫秒。
                Await soundPlayer.PlaySoundAsync(clockSound)
                Await Task.Delay(1000)
                soundPlaying = False
                ' 存档
                savMgr.MasterSaveFile.Status = Nukepayload2.N2Engine.Storage.SaveFileStatus.Modified
                Await savMgr.SaveMasterSaveFileAsync()
            End If
        End If
    End Sub

    Private Async Sub MusicPlayer_SingleSongCompleteAsync(sender As Object, e As EventArgs) Handles MusicPlayer.SingleSongComplete
        ' 单曲循环
        Await MusicPlayer.SetPlayingIndexAsync(0)
        MusicPlayer.Play()
    End Sub
End Class