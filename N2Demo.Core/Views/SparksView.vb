Imports System.Numerics
Imports Nukepayload2.N2Engine.Resources
Imports Nukepayload2.N2Engine.UI
Imports Nukepayload2.N2Engine.UI.Elements
Imports Nukepayload2.N2Engine.UI.Views
Imports Nukepayload2.N2Engine.Media
Imports Nukepayload2.N2Engine.Information
Imports System.Reflection
Imports Nukepayload2.N2Engine.UI.Controls

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
    Dim tblTheElder As New TextBlock

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
    ' 游戏控制。这些数据不会被保存。
    Dim isPaused As Boolean
    ' 字体管理。要使用文本相关的控件必须初始化字体管理。
    Dim fontMgr As New FontManager

    Sub New()
        ' 准备存档
        GameInformation.SharedLogicAssembly = [GetType].GetTypeInfo.Assembly
        savMgr = New SampleSaveFileManager
        ' 准备资源路由
        ApplyRoute()
    End Sub

    Public Async Function LoadSceneAsync() As Task
        ' 图像资源同步准备
        characterSheetSprite = BitmapResource.Create(sparksData.CharacterSheet.Source)
        ' 字体
        Await fontMgr.LoadAsync
        ' 可见对象树
        BuildVisualTree()
        ' 加载声音系统
        Await LoadSoundAsync()
        ' 读档
        Await LoadSaveFileAsync()
    End Function

    Private Sub BuildVisualTree()
        tblTheElder.Font = fontMgr.SegoeUI14Black
        IsFrozen.Bind(Function() isPaused)
        Location.Bind(New Vector2)
        ZIndex.Bind(0)
        AddChild(sparks.Bind(Function(s) s.Data, Function() sparksData.SparkSys))
        AddChild(scrollViewer.
            OnUpdate(sparksData.ShakingViewer.UpdateAction).
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
                Bind(Function(r) r.Location, Function() sparksData.CharacterSheet.Location).
                Bind(Function(r) r.Size, Function() sparksData.CharacterSheet.Size)).
            AddChild(tblTheElder.
                Bind(Function(r) r.Text, Function() sparksData.ElderText).
                Bind(Function(r) r.Location, Function() Vector2.Zero))
        )
    End Sub

    Private Async Function LoadSaveFileAsync() As Task
        Dim sav = Await savMgr.LoadMasterSaveFileAsync
        ' 同步数据
        If sav IsNot Nothing Then
            sparksData = sav.SaveData.LastState
            scrollViewer.OnUpdate(sparksData.ShakingViewer.UpdateAction)
        Else
            savMgr.MasterSaveFile.SaveData = New SampleMasterData With {
                .LastState = sparksData
            }
        End If
    End Function

    Private Async Function LoadSoundAsync() As Task
        Dim audio = Await CreateAudioPlayersAsync()
        MusicPlayer = audio.Item1
        soundPlayer = audio.Item2
        Await MusicPlayer.SetSourcesAsync({primaryBgm})
        MusicPlayer.Volume = 0.7
        MusicPlayer.Play()
        soundPlayer.SoundVolume = 0.8

        isSoundLoaded = True
    End Function

    ''' <summary>
    ''' 平台特定实现点击此视图时，调用此方法。将由 TappedTrigger 代替。
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
                isPaused = True
                savMgr.MasterSaveFile.Status = Nukepayload2.N2Engine.Storage.SaveFileStatus.Modified
                Await savMgr.SaveMasterSaveFileAsync()
                isPaused = False
            End If
        End If
    End Sub

    ' 播放背景音乐的控制。将由 RepeatBGMBehavior 代替。
    Private Async Sub MusicPlayer_SingleSongCompleteAsync(sender As Object, e As EventArgs) Handles MusicPlayer.SingleSongComplete
        ' 单曲循环
        Await MusicPlayer.SetPlayingIndexAsync(0)
        MusicPlayer.Play()
    End Sub
End Class