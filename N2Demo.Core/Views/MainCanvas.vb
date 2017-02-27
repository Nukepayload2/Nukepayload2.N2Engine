Imports System.Numerics
Imports System.Reflection
Imports Nukepayload2.N2Engine.Animations
Imports Nukepayload2.N2Engine.Behaviors
Imports Nukepayload2.N2Engine.Foundation
Imports Nukepayload2.N2Engine.Information
Imports Nukepayload2.N2Engine.Media
Imports Nukepayload2.N2Engine.ParticleSystems
Imports Nukepayload2.N2Engine.Resources
Imports Nukepayload2.N2Engine.UI
Imports Nukepayload2.N2Engine.UI.Controls
Imports Nukepayload2.N2Engine.UI.Elements
Imports Nukepayload2.N2Engine.UI.ParticleSystemViews

''' <summary>
''' 一个释放随机颜色火花的视图
''' </summary>
Public Class MainCanvas
    Inherits GameCanvas

#Region "可见元素"
    ' 普通元素
    Dim redEllipse As New EllipseElement
    Dim greenRect As New RectangleElement
    Dim sparks As New SparkParticleSystemView
    Dim charaSheet As New SpriteElement
    Dim scrollViewer As New GameVirtualizingScrollViewer
    Dim tblTheElder As New GameTextBlock
    Dim tblKeyDownCount As New GameTextBlock
    Dim tblLastMouseAction As New GameTextBlock
    Dim tblLastTouchAction As New GameTextBlock
    Dim swarmSysView As New SwarmParticleSystemView With {.Name = "喵喵喵？"}
    ' 控件
    WithEvents Joystick As New VirtualJoystick(Function(vec) vec.X < 300.0F)
    WithEvents BtnClickMe As New GameButton
#End Region

#Region "外部资源"
    Dim characterSheetSprite As BitmapResource
    Dim flameSprite As BitmapResource
    Dim primaryBgm As New Uri("n2-res:///ProgramDirectory/Audios/Musics/Theme1.wma")
    Dim clockSound As New Uri("n2-res:///ProgramDirectory/Audios/Sounds/Explosion4.wma")

#End Region

#Region "动画"
    Dim characterSheetCheckAnimation As BitmapDiscreteAnimation
    Dim characterSheetCheckAnimationState As IEnumerator(Of BitmapResource)

#End Region

#Region "媒体播放"
    WithEvents MusicPlayer As IMusicPlayer
    Dim soundPlayer As ISoundVoicePlayer
    Dim isSoundLoaded As Boolean
    Dim soundPlaying As Boolean

#End Region

    ' 数据
    Dim viewModel As New SparksViewModel
    ' 存档管理器
    Dim savMgr As SampleSaveFileManager
    ' 游戏控制。这些数据不会被保存。
    Dim isPaused As Boolean
    ' 字体管理。要使用文本相关的控件必须初始化字体管理。
    Dim fontMgr As New FontManager

    Sub New()
        ' 准备存档
        Environment.SharedLogicAssembly = [GetType].GetTypeInfo.Assembly
        savMgr = New SampleSaveFileManager
        ' 准备资源路由
        ApplyRoute()
    End Sub

    Public Async Function LoadSceneAsync() As Task
        ' 读档
        Await LoadSaveFileAsync()
        ' 图像资源同步准备
        ' 乱飞的火焰粒子系统
        Dim ghostFlameSheet = viewModel.GhostFlameSheet
        flameSprite = BitmapResource.Create(ghostFlameSheet.Source)
        Dim flameAnim = MakeAnimation(viewModel.GhostFlameSheet, flameSprite, Function(res) res.Skip(6).Take(3))
        flameAnim.NextAnimation = flameAnim
        viewModel.GhostFlameSys = New SwarmParticleSystem(20, Integer.MaxValue, 0.004, flameAnim, BackBufferInformation.Size)
        ' 人物贴图表
        characterSheetSprite = BitmapResource.Create(viewModel.CharacterSheet.Source)
        characterSheetCheckAnimation = MakeAnimation(viewModel.CharacterSheet, characterSheetSprite, Function(a) a)
        characterSheetCheckAnimation.NextAnimation = characterSheetCheckAnimation ' TODO: 实现循环信息之后用循环信息实现循环播放。
        characterSheetCheckAnimationState = characterSheetCheckAnimation.GetEnumerator
        ' 字体
        Await fontMgr.LoadAsync
        ' 可见对象树
        BuildVisualTree()
        ' 加载声音系统
        Await LoadSoundAsync()
    End Function

    Private Function MakeAnimation(spriteSheet As ISpriteSheet, bmp As BitmapResource,
             filter As Func(Of IEnumerable(Of BitmapResource), IEnumerable(Of BitmapResource))) As BitmapDiscreteAnimation
        Dim spriteSize = spriteSheet.SpriteSize
        Dim gridSize = spriteSheet.GridSize
        Return New BitmapDiscreteAnimation(filter(
                   bmp.Split(spriteSize.Width \ gridSize.Width,
                   spriteSize.Height \ gridSize.Height,
                   gridSize.Width, gridSize.Height)))
    End Function

    ' TODO: 用设计器生成这个方法，而不是只让用户手动打代码。
    Private Sub BuildVisualTree()
        ' 指定字体
        tblTheElder.Font = fontMgr.SegoeUI14Black
        tblKeyDownCount.Font = fontMgr.SegoeUI14Black
        tblLastMouseAction.Font = fontMgr.SegoeUI14Black
        tblLastTouchAction.Font = fontMgr.SegoeUI14Black
        BtnClickMe.Font = fontMgr.SegoeUI14Black
        ' 设置特效
        Dim rectTransform As New CompositeTransform
        With rectTransform
            .Rotate.Bind(Function() viewModel.GreenRectangle.Rotate)
            .Skew.Bind(Function() viewModel.GreenRectangle.Skew)
            .Scale.Bind(Function() viewModel.GreenRectangle.Scale)
            .Origin.Bind(Function() viewModel.GreenRectangle.Position + viewModel.GreenRectangle.RelativeOrigin * viewModel.GreenRectangle.Size)
        End With
        greenRect.Transform = rectTransform
        ' 绑定画布的数据
        IsFrozen.Bind(Function() isPaused)
        Location.Bind(New Vector2)
        ZIndex.Bind(0)
        ' 添加子元素
        AddChild(sparks.Bind(Function(s) s.Data, Function() viewModel.SparkSys))
        AddChild(scrollViewer.
            OnUpdate(viewModel.ShakingViewer.UpdateAction).
            Bind(Function(m) m.Location, Function() viewModel.ShakingViewer.Offset).
            Bind(Function(m) m.ZIndex, 0).
            AddChild(redEllipse.
                Bind(Function(r) r.Fill, Function() viewModel.RedCircle.Color).
                Bind(Function(r) r.Location, Function() viewModel.RedCircle.Position, Sub(p) viewModel.RedCircle.Position = p).
                Bind(Function(r) r.Size, Function() viewModel.RedCircle.Size)).
            AddChild(greenRect.
                Bind(Function(r) r.Stroke, Function() viewModel.GreenRectangle.Color).
                Bind(Function(r) r.Location, Function() viewModel.GreenRectangle.Position).
                Bind(Function(r) r.Size, Function() viewModel.GreenRectangle.Size)).
            AddChild(charaSheet.
                Bind(Function(r) r.Sprite, Function()
                                               If Not characterSheetCheckAnimationState.MoveNext Then
                                                   characterSheetCheckAnimationState.Reset()
                                                   characterSheetCheckAnimationState.MoveNext()
                                               End If
                                               Return characterSheetCheckAnimationState.Current
                                           End Function).
                Bind(Function(r) r.Location, Function() viewModel.CharacterSheet.Location).
                Bind(Function(r) r.Size, Function() viewModel.CharacterSheet.Size)).
            AddChild(tblTheElder.
                Bind(Function(r) r.Text, Function() viewModel.ElderText).
                Bind(Function(r) r.Location, Vector2.Zero)).
            AddChild(swarmSysView.
                Bind(Function(r) r.Data, Function() viewModel.GhostFlameSys).
                Bind(Function(r) r.Location, New Vector2)).
            AddChild(tblKeyDownCount.
                Bind(Function(r) r.Text, Function() "现在按下的键数量：" + viewModel.PressedKeyCount.ToString).
                Bind(Function(r) r.Location, New Vector2(0.0F, 50.0F))).
            AddChild(tblLastMouseAction.
                Bind(Function(r) r.Text, Function() "上一次鼠标状态：" + viewModel.LastMouseState).
                Bind(Function(r) r.Location, New Vector2(0.0F, 70.0F))).
            AddChild(tblLastTouchAction.
                Bind(Function(r) r.Text, Function() "上一次触摸状态：" + viewModel.LastTouchState).
                Bind(Function(r) r.Location, New Vector2(0.0F, 90.0F)))
        )
        AddChild(Joystick.
            Bind(Function(r) r.Fill, New Color(&H7FFF3F3F)).
            Bind(Function(r) r.Stroke, New Color(&H9F000000)).
            Bind(Function(r) r.FillSize, New Vector2(64.0F)).
            Bind(Function(r) r.StrokeSize, New Vector2(72.0F)).
            Bind(Function(r) r.Location, New Vector2)
        )
        AddChild(BtnClickMe.
            Bind(Function(r) r.Background, Function() If(BtnClickMe.IsPointerOver,
                viewModel.ButtonStatus.PointerOverBackground, viewModel.ButtonStatus.Background)).
            Bind(Function(r) r.BorderColor, Function() If(BtnClickMe.IsPressed,
                viewModel.ButtonStatus.PressedBorderColor, viewModel.ButtonStatus.BorderColor)).
            Bind(Function(r) r.TextOffset, Function() viewModel.ButtonStatus.TextOffset).
            Bind(Function(r) r.Size, Function() viewModel.ButtonStatus.Size).
            Bind(Function(r) r.Text, Function() viewModel.ButtonStatus.Text).
            Bind(Function(r) r.Location, Function() viewModel.ButtonStatus.Position)
        )
        ' 放置触发器
        Dim verticalShakeTrigger As New VerticalShakeTrigger
        verticalShakeTrigger.Attach(Me)
        Dim rotateRectangleTrigger As New KeyboardPlaneTransformTestTrigger
        rotateRectangleTrigger.Attach(Me)
        ' 增加行为
        Dim joystickControl As New VirtualJoystickMoveBehavior(Joystick) With {
            .MaxSpeed = 5.0F, .SpeedMultiple = 0.1F
        }
        joystickControl.Attach(redEllipse)
    End Sub

    Private Async Function LoadSaveFileAsync() As Task
        Dim sav = Await savMgr.LoadMasterSaveFileAsync
        ' 同步数据
        If sav IsNot Nothing Then
            viewModel = sav.SaveData.LastState
        Else
            savMgr.MasterSaveFile.SaveData = New SampleMasterData With {
                .LastState = viewModel
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
        viewModel.SparkSys.Location = pos
        viewModel.ShakingViewer.Shake(50.0F, 0)
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
        viewModel.PressedKeyCount += 1
    End Sub

    Private Sub SparksView_KeyUp(sender As GameVisual, e As GameKeyboardRoutedEventArgs) Handles Me.KeyUp
        viewModel.PressedKeyCount -= 1
    End Sub

    Private Sub SparksView_MouseButtonDown(sender As GameVisual, e As GameMouseRoutedEventArgs) Handles Me.MouseButtonDown
        viewModel.LastMouseState = $"在 {e.Position} 按下 {e.MouseButtons} 按钮，热键是 {e.KeyModifiers} 。"
    End Sub

    Private Sub SparksView_MouseButtonUp(sender As GameVisual, e As GameMouseRoutedEventArgs) Handles Me.MouseButtonUp
        viewModel.LastMouseState = $"在 {e.Position} 松开 {e.MouseButtons} 按钮，热键是 {e.KeyModifiers} 。"
        OnTappedAsync(e.Position)
    End Sub

    Private Sub SparksView_MouseMove(sender As GameVisual, e As GameMouseRoutedEventArgs) Handles Me.MouseMove
        viewModel.LastMouseState = $"移动到 {e.Position} ，热键是 {e.KeyModifiers} 。"
    End Sub

    Private Sub SparksView_MouseWheelChanged(sender As GameVisual, e As GameMouseRoutedEventArgs) Handles Me.MouseWheelChanged
        viewModel.LastMouseState = $"在 {e.Position} 滚动滚轮 {e.WheelDelta}，热键是 {e.KeyModifiers}。"
    End Sub

    Private Sub SparksView_TouchDown(sender As GameVisual, e As GameTouchRoutedEventArgs) Handles Me.TouchDown
        viewModel.LastTouchState = $"在 {e.Position} 按下触摸屏，触摸点的ID是 {e.PointerId}。"
    End Sub

    Private Sub SparksView_TouchMove(sender As GameVisual, e As GameTouchRoutedEventArgs) Handles Me.TouchMove
        viewModel.LastTouchState = $"在 {e.Position} 滑动触摸屏，上一个点是 {e.LastPosition}，触摸点的ID是 {e.PointerId}。"
    End Sub

    Private Sub SparksView_TouchUp(sender As GameVisual, e As GameTouchRoutedEventArgs) Handles Me.TouchUp
        viewModel.LastTouchState = $"在 {e.Position} 松开触摸屏，触摸点的ID是 {e.PointerId}。"
        OnTappedAsync(e.Position)
    End Sub

    Private Sub BtnClickMe_Click(sender As GameButton, e As EventArgs) Handles BtnClickMe.Click
        viewModel.ButtonStatus.ClickCount += 1
    End Sub
End Class