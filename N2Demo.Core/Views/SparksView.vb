Imports System.Numerics
Imports System.Reflection
Imports Nukepayload2.N2Engine.Behaviors
Imports Nukepayload2.N2Engine.Information
Imports Nukepayload2.N2Engine.Media
Imports Nukepayload2.N2Engine.Resources
Imports Nukepayload2.N2Engine.UI
Imports Nukepayload2.N2Engine.UI.Controls
Imports Nukepayload2.N2Engine.UI.Elements
Imports Nukepayload2.N2Engine.UI.ParticleSystemViews

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
    Dim scrollViewer As New GameVirtualizingScrollViewer
    Dim tblTheElder As New GameTextBlock
    Dim tblKeyDownCount As New GameTextBlock
    Dim tblLastMouseAction As New GameTextBlock
    Dim tblLastTouchAction As New GameTextBlock

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

    ' TODO: 用设计器生成这个方法，而不是只让用户手动打代码。
    Private Sub BuildVisualTree()
        ' 指定字体
        tblTheElder.Font = fontMgr.SegoeUI14Black
        tblKeyDownCount.Font = fontMgr.SegoeUI14Black
        tblLastMouseAction.Font = fontMgr.SegoeUI14Black
        tblLastTouchAction.Font = fontMgr.SegoeUI14Black
        ' 设置特效
        Dim rectTransform As New CompositeTransform
        With rectTransform
            .Rotate.Bind(Function() sparksData.GreenRectangle.Rotate)
            .Skew.Bind(Function() sparksData.GreenRectangle.Skew)
            .Scale.Bind(Function() sparksData.GreenRectangle.Scale)
            .Origin.Bind(Function() sparksData.GreenRectangle.Position + sparksData.GreenRectangle.RelativeOrigin * sparksData.GreenRectangle.Size)
        End With
        greenRect.Transform = rectTransform
        ' 绑定画布的数据
        IsFrozen.Bind(Function() isPaused)
        Location.Bind(New Vector2)
        ZIndex.Bind(0)
        ' 添加子元素
        AddChild(sparks.Bind(Function(s) s.Data, Function() sparksData.SparkSys))
        AddChild(scrollViewer.
            OnUpdate(sparksData.ShakingViewer.UpdateAction).
            Bind(Function(m) m.Location, Function() sparksData.ShakingViewer.Offset).
            Bind(Function(m) m.ZIndex, 0).
            AddChild(redEllipse.
                Bind(Function(r) r.Fill, Function() sparksData.RedCircle.Color).
                Bind(Function(r) r.Location, Function() sparksData.RedCircle.Position, Sub(p) sparksData.RedCircle.Position = p).
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
                Bind(Function(r) r.Location, Vector2.Zero)).
            AddChild(tblKeyDownCount.
                Bind(Function(r) r.Text, Function() "现在按下的键数量：" + sparksData.PressedKeyCount.ToString).
                Bind(Function(r) r.Location, New Vector2(0.0F, 50.0F))).
            AddChild(tblLastMouseAction.
                Bind(Function(r) r.Text, Function() "上一次鼠标状态：" + sparksData.LastMouseState).
                Bind(Function(r) r.Location, New Vector2(0.0F, 70.0F))).
            AddChild(tblLastTouchAction.
                Bind(Function(r) r.Text, Function() "上一次触摸状态：" + sparksData.LastTouchState).
                Bind(Function(r) r.Location, New Vector2(0.0F, 90.0F)))
        )
        ' 放置触发器
        Dim verticalShakeTrigger As New VerticalShakeTrigger
        verticalShakeTrigger.Attach(Me)
        Dim rotateRectangleTrigger As New KeyboardPlaneTransformTestTrigger
        rotateRectangleTrigger.Attach(Me)
        ' 增加行为
        Dim shake As New ShakeBehavior(New Vector2(10, -5))
        shake.Attach(redEllipse)
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
    ''' 点击视图的处理。
    ''' </summary>
    Private Async Sub OnTappedAsync(pos As Vector2)
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

    Private Sub SparksView_KeyDown(sender As GameVisual, e As GameKeyboardRoutedEventArgs) Handles Me.KeyDown
        sparksData.PressedKeyCount += 1
    End Sub

    Private Sub SparksView_KeyUp(sender As GameVisual, e As GameKeyboardRoutedEventArgs) Handles Me.KeyUp
        sparksData.PressedKeyCount -= 1
    End Sub

    Private Sub SparksView_MouseButtonDown(sender As GameVisual, e As GameMouseRoutedEventArgs) Handles Me.MouseButtonDown
        sparksData.LastMouseState = $"在 {e.Position} 按下 {e.MouseButtons} 按钮，热键是 {e.KeyModifiers} 。"
    End Sub

    Private Sub SparksView_MouseButtonUp(sender As GameVisual, e As GameMouseRoutedEventArgs) Handles Me.MouseButtonUp
        sparksData.LastMouseState = $"在 {e.Position} 松开 {e.MouseButtons} 按钮，热键是 {e.KeyModifiers} 。"
        OnTappedAsync(e.Position)
    End Sub

    Private Sub SparksView_MouseMove(sender As GameVisual, e As GameMouseRoutedEventArgs) Handles Me.MouseMove
        sparksData.LastMouseState = $"移动到 {e.Position} ，热键是 {e.KeyModifiers} 。"
    End Sub

    Private Sub SparksView_MouseWheelChanged(sender As GameVisual, e As GameMouseRoutedEventArgs) Handles Me.MouseWheelChanged
        sparksData.LastMouseState = $"在 {e.Position} 滚动滚轮 {e.WheelDelta}，热键是 {e.KeyModifiers}。"
    End Sub

    Private Sub SparksView_TouchDown(sender As GameVisual, e As GameTouchRoutedEventArgs) Handles Me.TouchDown
        sparksData.LastTouchState = $"在 {e.Position} 按下触摸屏，触摸点的ID是 {e.PointerId}。"
    End Sub

    Private Sub SparksView_TouchMove(sender As GameVisual, e As GameTouchRoutedEventArgs) Handles Me.TouchMove
        sparksData.LastTouchState = $"在 {e.Position} 滑动触摸屏，上一个点是 {e.LastPosition}，触摸点的ID是 {e.PointerId}。"
    End Sub

    Private Sub SparksView_TouchUp(sender As GameVisual, e As GameTouchRoutedEventArgs) Handles Me.TouchUp
        sparksData.LastTouchState = $"在 {e.Position} 松开触摸屏，触摸点的ID是 {e.PointerId}。"
        OnTappedAsync(e.Position)
    End Sub
End Class