Imports System.Numerics
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
    Dim stageGrid As New StageOneGrid
    ' 控件
    WithEvents Joystick As New VirtualJoystick(Function(vec) vec.X < 300.0F)
    WithEvents BtnJump As New GameButton
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
        flameGuyAnimation = MakeAnimation(viewModel.CharacterSheet, characterSheetSprite, Function(a) a)
        flameGuyAnimation.NextAnimation = flameGuyAnimation ' TODO: 实现循环信息之后用循环信息实现循环播放。
        flameGuyAnimationState = flameGuyAnimation.GetEnumerator
    End Sub

    Private Sub VisualTreeDataBind()
        chara = New PrimaryCharacter(character, New RectangleCollider(1, viewModel.CharacterSheet.Size))
        character.Bind(Function(r) r.Sprite, Function()
                                                 If Not flameGuyAnimationState.MoveNext Then
                                                     flameGuyAnimationState.Reset()
                                                     flameGuyAnimationState.MoveNext()
                                                 End If
                                                 Return flameGuyAnimationState.Current
                                             End Function).
        Bind(Function(r) r.Location, Function() viewModel.CharacterSheet.Location).
        Bind(Function(r) r.Size, Function() viewModel.CharacterSheet.Size)

        IsFrozen.Bind(Function() isPaused)
        Location.Bind(New Vector2)
        ZIndex.Bind(0)
        AddChild(scrollViewer.
            Bind(Function(m) m.Location, Function() viewModel.ScrollingViewer.Offset).
            Bind(Function(m) m.ZIndex, 0).
            AddChild(stageGrid.LayoutRoot.
                Bind(Function(r) r.Location, New Vector2).
                Bind(Function(r) r.Size, New Vector2(512, 512)).
                AddChild(chara)
            ).
            AddChild(tblJumpLogicStatus.
                Bind(Function(r) r.Text, Function() viewModel.JumpLogicStatusText).
                Bind(Function(r) r.Location, Vector2.Zero)
            )
        ).
        AddChild(controlPanel.
            Bind(Function(r) r.Location, New Vector2).
            AddChild(Joystick.
                Bind(Function(r) r.Fill, New Color(&H7FFF3F3F)).
                Bind(Function(r) r.Stroke, New Color(&H9F000000)).
                Bind(Function(r) r.FillSize, New Vector2(64.0F)).
                Bind(Function(r) r.StrokeSize, New Vector2(72.0F)).
                Bind(Function(r) r.Location, New Vector2)
            ).
            AddChild(BtnJump.
                Bind(Function(r) r.Background, Function() If(BtnJump.IsPointerOver,
                    viewModel.ButtonStatus.PointerOverBackground, viewModel.ButtonStatus.Background)).
                Bind(Function(r) r.BorderColor, Function() If(BtnJump.IsPressed,
                    viewModel.ButtonStatus.PressedBorderColor, viewModel.ButtonStatus.BorderColor)).
                Bind(Function(r) r.TextOffset, Function() viewModel.ButtonStatus.TextOffset).
                Bind(Function(r) r.Size, Function() viewModel.ButtonStatus.Size).
                Bind(Function(r) r.Text, Function() viewModel.ButtonStatus.Text).
                Bind(Function(r) r.Location, Function() viewModel.ButtonStatus.Position)
            )
        )
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

End Class