Option Strict On

Imports System.Numerics
Imports FarseerPhysics.Dynamics
Imports Nukepayload2.N2Engine.ActionGames.Core
Imports Nukepayload2.N2Engine.Animations
Imports Nukepayload2.N2Engine.Foundation
Imports Nukepayload2.N2Engine.PhysicsIntegration
Imports Nukepayload2.N2Engine.Resources
Imports Nukepayload2.N2Engine.UI
Imports Nukepayload2.N2Engine.UI.Controls
Imports Nukepayload2.N2Engine.UI.Elements
Imports Nukepayload2.N2Engine.Utilities

''' <summary>
''' 用于测试代码的视图。内容不定。
''' </summary>
Public Class MainCanvas
    Inherits GameCanvas

#Region "可见元素"
    ' 普通元素
    Dim ground As New RectangleElement
    Dim character As New SpriteElement
    Dim chara As PrimaryCharacter
    Dim physicsGrid As StageOneGrid
    Dim scrollViewer As New GameVirtualizingScrollViewer
    Dim controlPanel As New PanelLayer
    Dim tblJumpLogicStatus As New GameTextBlock
    Dim stageGrid As StageOneGrid
    ' 控件
    WithEvents Joystick As New VirtualJoystick(Function(vec) vec.X < 300.0F)
    WithEvents BtnJump As New GameButton With {.ClickMode = ClickModes.Press}
#End Region

#Region "外部资源"
    Dim characterSheetSprite As BitmapResource
    Dim primaryBgm As New Uri("n2-res:///ProgramDirectory/Audios/Musics/Theme1.wma")

#End Region

#Region "动画"
    Dim flameGuyAnimation As BitmapDiscreteAnimation
    Dim flameGuyAnimationState As IEnumerator(Of BitmapResource)

#End Region

    ' 数据
    Dim viewModel As New SparksViewModel
    ' 游戏控制。这些数据不会被保存。
    Dim isPaused As Boolean

    Private Sub MainCanvas_LoadTextureResources() Handles Me.LoadTextureResources
        ' 人物贴图表
        characterSheetSprite = BitmapResource.Create(viewModel.CharacterSheet.Source)
        flameGuyAnimation = MakeAnimation(viewModel.CharacterSheet, characterSheetSprite, Function(a) a.Take(3))
        flameGuyAnimation.NextAnimation = flameGuyAnimation ' TODO: 实现循环信息之后用循环信息实现循环播放。
        flameGuyAnimationState = flameGuyAnimation.GetEnumerator
    End Sub

    Private Sub VisualTreeDataBind()
        stageGrid = New StageOneGrid
        chara = New PrimaryCharacter(character, New RectangleCollider(1, viewModel.CharacterSheet.Size.ToPhysicsUnit))

        character.Sprite = New RelayPropertyBinder(Of BitmapResource)(
            Function()
                If Not flameGuyAnimationState.MoveNext Then
                    flameGuyAnimationState.Reset()
                    flameGuyAnimationState.MoveNext()
                End If
                Return flameGuyAnimationState.Current
            End Function)
        character.Size = New CachedPropertyBinder(Of Vector2)(
            viewModel.CharacterSheet, NameOf(viewModel.CharacterSheet.Size))

        IsFrozen.Value = isPaused

        Children.Add(scrollViewer)
        With scrollViewer
            .Location = New CachedPropertyBinder(Of Vector2)(
                viewModel.ScrollingViewer, NameOf(viewModel.ScrollingViewer.Offset))
            .Size = New CachedPropertyBinder(Of Vector2)(
                viewModel.ScrollingViewer, NameOf(viewModel.ScrollingViewer.BufferSize))
        End With

        Dim layoutRoot = stageGrid.LayoutRoot
        scrollViewer.Children.Add(layoutRoot)
        With layoutRoot
            .Size = New CachedPropertyBinder(Of Vector2)(
                viewModel.ScrollingViewer, NameOf(viewModel.ScrollingViewer.BufferSize))
            .Children.Add(chara)
        End With

        Children.Add(controlPanel)
        With controlPanel
            .Children.Add(Joystick)
            With Joystick
                .Fill.Value = New Color(&H7FFF3F3F)
                .Stroke.Value = New Color(&H9F000000)
                .FillSize.Value = New Vector2(64.0F)
                .StrokeSize.Value = New Vector2(72.0F)
            End With
            .Children.Add(BtnJump)
            With BtnJump
                .Background = New RelayPropertyBinder(Of Color)(Function() If(BtnJump.IsPointerOver,
                    viewModel.ButtonStatus.PointerOverBackground, viewModel.ButtonStatus.Background))
                .BorderColor = New RelayPropertyBinder(Of Color)(Function() If(BtnJump.IsPressed,
                    viewModel.ButtonStatus.PressedBorderColor, viewModel.ButtonStatus.BorderColor))
                .TextOffset = New CachedPropertyBinder(Of Vector2)(
                    viewModel.ButtonStatus, NameOf(viewModel.ButtonStatus.TextOffset))
                .Size = New CachedPropertyBinder(Of Vector2)(
                    viewModel.ButtonStatus, NameOf(viewModel.ButtonStatus.Size))
                .Text = New CachedPropertyBinder(Of String)(
                    viewModel.ButtonStatus, NameOf(viewModel.ButtonStatus.Text))
                .Location = New CachedPropertyBinder(Of Vector2)(
                    viewModel.ButtonStatus, NameOf(viewModel.ButtonStatus.Position))
            End With
            .Children.Add(tblJumpLogicStatus)
            With tblJumpLogicStatus
                .Text = New CachedPropertyBinder(Of String)(
                    viewModel, NameOf(viewModel.JumpLogicStatusText))
            End With
        End With

        character.Location = New RelayPropertyBinder(Of Vector2)(
            Function()
                Dim phyPos = chara.Body.Position.ToDisplayUnit
                Dim chaSize = character.Size.Value
                Return New Vector2(phyPos.X + chaSize.X / 2, phyPos.Y + chaSize.Y)
            End Function)

        chara.Body.Position = viewModel.CharacterSheet.Location.ToPhysicsUnit
        chara.Body.FixedRotation = True
        chara.Body.BodyType = BodyType.Dynamic
    End Sub

    Private Sub SetFontsForControls() Handles Me.BindFonts
        tblJumpLogicStatus.Font = fontMgr.SegoeUI14Black
        BtnJump.Font = fontMgr.SegoeUI14Black
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

    ' 播放背景音乐的控制。将由 RepeatBGMBehavior 代替。
    Private Async Sub MusicPlayer_SingleSongCompleteAsync(sender As Object, e As EventArgs) Handles MusicPlayer.SingleSongComplete
        ' 单曲循环
        Await MusicPlayer.SetPlayingIndexAsync(0)
        MusicPlayer.Play()
    End Sub

    Private Sub MainCanvas_AttachBehaviors(sender As Object, e As EventArgs) Handles Me.AttachBehaviors
        ' 平台跳跃控制
        Dim jumpBehavior As New PlatformJumpingBehavior(BtnJump, Joystick)
        jumpBehavior.Attach(chara)
        Dim cameraLook As New CameraLookAtBehavior(scrollViewer)
        cameraLook.Attach(chara)
    End Sub
End Class