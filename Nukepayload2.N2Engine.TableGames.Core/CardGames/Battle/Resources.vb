Imports Nukepayload2.N2Engine.Core

Namespace Battle
    Public Module Constants
        Public Const SoundEffectDirectory As String = "/SE/"
        Public Const BackgroundMusicDirectory As String = "/BGM/"
        Public Const ImageDirectory As String = "/Images/" 'TODO: 注册游戏程序集到资源加载器
        Public Const ExtensionDirectory As String = "/Extensions/"
        Public Const FontDirectory As String = "/Fonts/"
    End Module
    Public Module UriSettings
        Public Property UriPrefix As String = ResourceLoader.EmbeddedScheme + "ms-appx://"
    End Module
    ''' <summary>
    ''' 包装一个流ResourcePreLoader
    ''' </summary>
    Public MustInherit Class ResourcePreLoader
        Implements IBattleImageLoader
        Public Overridable Function ImageFromResourcePack(Name As String) As Uri Implements IBattleImageLoader.ImageFromResourcePack
            Debug.WriteLine("Load:" & UriPrefix & ImageDirectory & Name & ".png")
            Return New Uri(UriPrefix & ImageDirectory & Name & ".png", UriKind.Absolute)
        End Function
    End Class
    Public MustInherit Class BattleBitmapLoader
        Inherits ResourcePreLoader
        Protected Shared _Current As BattleBitmapLoader
        Public Shared Property Current As BattleBitmapLoader
            Get
                Return _Current
            End Get
            Protected Set(value As BattleBitmapLoader)
                _Current = value
            End Set
        End Property
    End Class
    Public MustInherit Class BattleSoundPlayer
        Inherits ResourcePreLoader
        Implements ISoundPlayer
        Protected Shared _Current As BattleSoundPlayer
        Public Shared Property Current As BattleSoundPlayer
            Get
                Return _Current
            End Get
            Protected Set(value As BattleSoundPlayer)
                _Current = value
            End Set
        End Property
        Public MustOverride Function SoundStreamFromResourcePack(Name As String) As Stream
        Protected BGMNames As List(Of String)
        Protected PlayIndex As Integer
        Public StreamCache As New Dictionary(Of String, Stream)
        Public ReadOnly Property BackgroundMusic As IEnumerable(Of Stream) Implements ISoundPlayer.BackgroundMusic
            Get
                Return From s In BGMNames Select StreamCache(s)
            End Get
        End Property
        Public Sub NextOne() Implements ISoundPlayer.NextOne
            PlayIndex = If(PlayIndex >= BGMNames.Count - 1, 0, PlayIndex + 1)
            PlayBackgroundMusic()
        End Sub
        Public MustOverride Sub PlayBackgroundMusic() Implements ISoundPlayer.PlayBackgroundMusic
        Public MustOverride Function PlaySoundEffect(Sound As Stream) As Task Implements ISoundPlayer.PlaySoundEffect
        Public MustOverride Function PlaySoundEffect(SoundID As String) As Task Implements ISoundPlayer.PlaySoundEffect
        Public MustOverride Sub PauseBackgroundMusic() Implements ISoundPlayer.PauseBackgroundMusic
        Public MustOverride Sub ResumeBackgroundMusic() Implements ISoundPlayer.ResumeBackgroundMusic
        Public MustOverride Function PlaySoundEffect(Sound As Uri) As Task Implements ISoundPlayer.PlaySoundEffect
        Public MustOverride Function PlaySoundEffect(Sound As IEnumerable(Of Stream)) As Task Implements ISoundPlayer.PlaySoundEffect
    End Class
    Public Class BattleStringLoader
        Implements IBattleStringLoader
        Dim resLdr As New ResourceLoader
        Public Shared ReadOnly Property Cuttent As IBattleStringLoader
        Sub New()
            _Cuttent = Me
        End Sub
        Public Function GetString(Name As String) As String Implements IBattleStringLoader.GetString
            Try
                Return resLdr.GetString(Name)
            Catch ex As Exception
                Return "Missing:" & Name
            End Try
        End Function
    End Class
End Namespace

