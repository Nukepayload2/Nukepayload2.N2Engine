Imports System.Reflection
Imports Nukepayload2.N2Engine.Information
Imports Nukepayload2.N2Engine.Media

Partial Public Class MainCanvas
    ' 存档管理器
    Dim savMgr As SampleSaveFileManager
    ' 字体管理。要使用文本相关的控件必须初始化字体管理。
    Dim fontMgr As New FontManager

#Region "媒体播放"
    WithEvents MusicPlayer As IMusicPlayer
    Dim soundPlayer As ISoundVoicePlayer
    Dim isSoundLoaded As Boolean
    Dim soundPlaying As Boolean

#End Region

    Public Async Function LoadSceneAsync() As Task
        ' 读档
        Await LoadSaveFileAsync()
        ' 加载纹理
        RaiseEvent LoadTextureResources(Me, EventArgs.Empty)
        ' 字体
        Await fontMgr.LoadAsync
        ' 可见对象树
        BuildVisualTree()
        ' 加载声音系统
        Await LoadSoundAsync()
    End Function

    ' TODO: 用设计器生成这个方法，而不是只让用户手动打代码。
    Private Sub BuildVisualTree()
        ' 指定字体
        RaiseEvent BindFonts(Me, EventArgs.Empty)
        ' 设置特效
        RaiseEvent ApplyEffects(Me, EventArgs.Empty)
        ' 确定可见对象树的结构，绑定画布的数据
        VisualTreeDataBind()
        ' 放置触发器
        RaiseEvent SetTriggers(Me, EventArgs.Empty)
        ' 增加行为
        RaiseEvent AttachBehaviors(Me, EventArgs.Empty)
    End Sub

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

    Sub New()
        ' 准备存档
        GameEnvironment.SharedLogicAssembly = [GetType].GetTypeInfo.Assembly
        savMgr = New SampleSaveFileManager
        ' 准备资源路由
        ApplyRoute()
    End Sub

    Event LoadTextureResources As EventHandler
    Event BindFonts As EventHandler
    Event AttachBehaviors As EventHandler
    Event SetTriggers As EventHandler
    Event ApplyEffects As EventHandler
End Class
