Imports System.Text

Namespace Battle
    Public MustInherit Class HandCardBase
        Inherits CardCommandBase
        Implements IHandCard
        Sub New()
            PatternPoints = RandomPatternPoints()
            Debug.WriteLine([GetType]().Name)
        End Sub
        Sub New(PatternPoints As Poker, ImmuneToPsychicalControl As Boolean)
            Me.PatternPoints = PatternPoints
            Me.ImmuneToPsychicalControl = ImmuneToPsychicalControl
        End Sub
        Public MustOverride ReadOnly Property AICategory As Integer Implements IHandCard.AICategory
        Public Property ImmuneToPsychicalControl As Boolean Implements IHandCard.ImmuneToPsychicalControl
        Public MustOverride ReadOnly Property RoundLimit As Integer Implements IHandCard.RoundLimit
        Public ReadOnly Property IsPeach As Boolean Implements IHandCard.IsPeach
            Get
                Return PatternPoints.Pattern = Patterns.Heart OrElse PatternPoints.Pattern = Patterns.Spade
            End Get
        End Property
        Public MustOverride ReadOnly Property IsModCard As Boolean Implements ICard.IsModCard
        Public MustOverride ReadOnly Property IsSuperWeapon As Boolean Implements IHandCard.IsSuperWeapon
        Public ReadOnly Property IsBlack As Boolean Implements IHandCard.IsBlack
            Get
                Return PatternPoints.Pattern = Patterns.Club OrElse PatternPoints.Pattern = Patterns.Spade
            End Get
        End Property
        ''' <summary>
        ''' 表示单位Tech程Degree。Tech程Degree越高，在初期发Card时越难发ToValue。
        ''' 超出TechLevelLimit的CardUnableToBe发Card。
        ''' </summary>
        ''' <returns></returns>
        Public MustOverride ReadOnly Property TechLevel As Integer Implements IHandCard.TechLevel
        Public MustOverride ReadOnly Property LeadStateCanBeUsed As Boolean Implements IHandCard.LeadStateCanBeUsed
        Public Property PatternPoints As Poker Implements IHandCard.PatternPoints
        Public Overridable ReadOnly Property Name As String Implements ICard.Name
            Get
                Return Me.GetType.Name
            End Get
        End Property
        Public Overridable ReadOnly Property BriefDescription As String Implements ICard.BriefDescription
            Get
                Return GetString(NameOf(BriefDescription))
            End Get
        End Property
        Public Overridable ReadOnly Property ExtendedDescription As String Implements ICard.ExtendedDescription
            Get
                Return GetString(NameOf(ExtendedDescription))
            End Get
        End Property
        ''' <summary>
        ''' Card上Show的ThumbnailIcon,DefaultIs空。
        ''' 如果Is空的则加载HandCard时会变成DefaultIcon。
        ''' </summary> 
        ''' <returns></returns>
        Public Overridable ReadOnly Property Icon As Uri Implements IHandCard.Icon
            Get
                Return LoadImage("Icon_s")
            End Get
        End Property
        ''' <summary>
        ''' Card工具提示上Show的LargeIcon,DefaultIs空。
        ''' 如果Is空的则加载HandCard时会变成DefaultIcon。
        ''' </summary> 
        ''' <returns></returns>
        Public Overridable ReadOnly Property LargeIcon As Uri Implements IHandCard.LargeIcon
            Get
                Return LoadImage("Icon")
            End Get
        End Property
        ''' <summary>
        ''' Whether按照SelectPlayer对其它成员生效，DefaultIsFalse
        ''' </summary>
        ''' <returns></returns>
        Public Overridable ReadOnly Property IsAoe As Boolean Implements IHandCard.IsAoe
            Get
                Return False
            End Get
        End Property
        ''' <summary>
        ''' 如果IsAoe，BackToSelect过的CanAttack的Player列表。
        ''' </summary>
        ''' <exception cref="InvalidOperationException">不IsAoe时Raise</exception>
        ''' <returns></returns>
        Public Overridable Function SelectPlayer(Players As IEnumerable(Of IPlayer), CurrentPlayer As IPlayer) As IEnumerable(Of IPlayer) Implements IHandCard.SelectPlayer
            If IsAoe Then
                Return From p In Players Where p IsNot CurrentPlayer
            Else
                Throw New InvalidOperationException("非AoeUnableTo群攻。Name：" & Name)
            End If
        End Function
        ''' <summary>
        ''' 如果这张Card不Is群攻的，那么它最多CanSelect几个Target。
        ''' DefaultIs1。
        ''' </summary>
        ''' <returns></returns>
        Public Overridable ReadOnly Property LargestTargetCount As Integer Implements IHandCard.LargestTargetCount
            Get
                Return 1
            End Get
        End Property
        ''' <summary>
        ''' 如果玩这个GameCore时Use了鼠Mark，SettingsCursor的外观。
        ''' 如果Is空的则UseGameCoreSettings的Default鼠Mark。
        ''' DefaultIs空的。
        ''' </summary>
        ''' <returns></returns>
        Public Overridable ReadOnly Property Cursor As Uri Implements ICard.Cursor
            Get
                Return Nothing
            End Get
        End Property
        ''' <summary>
        ''' CardBeSelected时Play的SoundFromValue这些Sound里选出来。DefaultIs空集。
        ''' </summary> 
        Public Overridable ReadOnly Property SelectSound As IEnumerable(Of Stream) Implements ICard.SelectSound
            Get
                Return LoadSounds("Select")
            End Get
        End Property
        ''' <summary>
        ''' CardBeUse时Play的SoundFromValue这些Sound里选出来。DefaultIs空集。
        ''' </summary> 
        Public Overridable ReadOnly Property UseSound As IEnumerable(Of Stream) Implements ICard.UseSound
            Get
                Return LoadSounds("Use")
            End Get
        End Property
        ''' <summary>
        ''' MilitaryOfficerDeath和Attack性Card失效时Play的SoundFromValue这些Sound里选出来。DefaultIs空集。
        ''' </summary> 
        Public Overridable ReadOnly Property DeathSound As IEnumerable(Of Stream) Implements ICard.DeathSound
            Get
                Return LoadSounds("Die")
            End Get
        End Property
        ''' <summary>
        ''' Is不Is一种SpecifiedCamp才CanUse的Card，DefaultIsFalse
        ''' </summary>
        ''' <returns></returns>
        Public Overridable ReadOnly Property IsCampSpecifiedCard As Boolean Implements IHandCard.IsCampSpecifiedCard
            Get
                Return SpecifiedCamp.FirstOrDefault IsNot Nothing
            End Get
        End Property
        ''' <summary>
        ''' 如果IsSpecifiedCamp才Can用的Card，Is哪些CampCan用。如果指定为空集且IsCampSpecifiedCard则会屏蔽这张Card。Default为空集。
        ''' </summary>
        ''' <returns></returns>
        Public Overridable ReadOnly Property SpecifiedCamp As IEnumerable(Of Type) Implements IHandCard.SpecifiedCamp
            Get
                Return {}
            End Get
        End Property
        Dim _CustomAttribs As New Dictionary(Of String, Object)
        Public ReadOnly Property CustomProperties As IDictionary(Of String, Object) Implements IHandCard.CustomProperties
            Get
                Return _CustomAttribs
            End Get
        End Property

        Public ReadOnly Property Pattern As String Implements IHandCard.Pattern
            Get
                Select Case PatternPoints.Pattern
                    Case Patterns.Diamonds
                        Return "♦"
                    Case Patterns.Club
                        Return "♣"
                    Case Patterns.Heart
                        Return "♥"
                    Case Else
                        Return "♠"
                End Select
            End Get
        End Property

        Public ReadOnly Property Points As String Implements IHandCard.Points
            Get
                Return PatternPoints.Point
            End Get
        End Property

        ''' <summary>
        ''' 加载一些临时的Resource
        ''' </summary> 
        Public Overridable Async Function Initialized() As Task Implements ICard.Initialized
            Await DoNothingTask()
        End Function
        Public Overridable Async Function OnLostAsync(SourcePlayer As IPlayer, TargetPlayer As IPlayer) As Task Implements IHandCard.OnLostAsync
            Await DoNothingTask()
        End Function
        Public Overridable Async Function OnGotAsync(SourcePlayer As IPlayer, TargetPlayer As IPlayer) As Task Implements IHandCard.OnGotAsync
            Await DoNothingTask()
        End Function
        ''' <summary>
        ''' 用于释放临时的Resource
        ''' </summary> 
        Public Overridable Async Function DisposeResources() As Task Implements ICard.DisposeResources
            Await DoNothingTask()
        End Function
        ''' <summary>
        ''' 注意：如果要重写此方法，还要Use HumanResponse 或 AIResponse 或 ResponseFail则要先调用基类的此方法。
        ''' </summary> 
        Public Overridable Async Function Responsed(SourcePlayer As IPlayer, TargetPlayer As IPlayer, e As CardResponseEventArgs) As Task(Of Boolean) Implements IHandCard.Responsed
            If Not e.Process Then
                If Not e.Timeout Then
                    If e.IsHumanPlayer Then
                        If Await HumanResponse(SourcePlayer, TargetPlayer) Then Return True
                    Else
                        If Await AIResponse(SourcePlayer, TargetPlayer) Then Return True
                    End If
                End If
                Await ResponseFail(SourcePlayer, TargetPlayer)
                Return False
            Else
                Await NoNeedResponse(SourcePlayer, TargetPlayer)
                Return True
            End If
        End Function
        Public Overridable Async Function ResponseFail(SourcePlayer As IPlayer, TargetPlayer As IPlayer) As Task
            Await DoNothingTask()
        End Function
        Public Overridable Async Function NoNeedResponse(SourcePlayer As IPlayer, TargetPlayer As IPlayer) As Task
            Await DoNothingTask()
        End Function
        ''' <summary>
        ''' BackTo值表示HumanWhetherSuccessResponseCard的要求。
        ''' DefaultIsResponseFail
        ''' </summary> 
        Public Overridable Async Function HumanResponse(SourcePlayer As IPlayer, TargetPlayer As IPlayer) As Task(Of Boolean)
            Return Await ResFalseTask()
        End Function
        ''' <summary>
        ''' BackTo值表示AIWhetherSuccessResponseCard的要求。
        ''' DefaultIsResponseFail
        ''' </summary> 
        Public Overridable Async Function AIResponse(SourcePlayer As IPlayer, TargetPlayer As IPlayer) As Task(Of Boolean)
            Return Await ResFalseTask()
        End Function
        Public Overridable Sub RandomGetPatternPoints() Implements IHandCard.RandomGetPatternPoints
            PatternPoints = New Poker(CType(RndEx(0, 3), Patterns), RndEx(1, 13))
        End Sub
    End Class

    Public Class Attack
        Inherits HandCardBase
        Public Overrides ReadOnly Property AICategory As Integer =
             CardCategories.Attack
        Public Overrides ReadOnly Property RoundLimit As Integer = DefaultRoundLimits.NoLimit
        Public Overrides ReadOnly Property IsModCard As Boolean = False
        Public Overrides ReadOnly Property IsSuperWeapon As Boolean = False
        Public Overrides ReadOnly Property TechLevel As Integer = DefaultTechLevels.Essential
        Public Overrides ReadOnly Property LeadStateCanBeUsed As Boolean = True
        Public Overrides ReadOnly Property Uid As String = "Attack"

        Public Overrides Async Function ResponseFail(SourcePlayer As IPlayer, TargetPlayer As IPlayer) As Task
            Await MyBase.ResponseFail(SourcePlayer, TargetPlayer)
            Await TargetPlayer.SetHealthAsync(SourcePlayer, TargetPlayer.Health - 1)
        End Function
        Protected Async Function AIResponseInternal(SourcePlayer As IPlayer, TargetPlayer As IPlayer, CardTypes As IEnumerable(Of CardCategories)) As Task(Of Boolean)
            Dim ca = Await TargetPlayer.PickCards(CardTypes)
            If ca.Count <> 0 Then
                Await TargetPlayer.LostHandCard(ca.First)
                Return True
            Else
                Return False
            End If
        End Function
        Public Overrides Async Function AIResponse(SourcePlayer As IPlayer, TargetPlayer As IPlayer) As Task(Of Boolean)
            Await MyBase.AIResponse(SourcePlayer, TargetPlayer)
            Return Await AIResponseInternal(SourcePlayer, TargetPlayer, {CardCategories.Dodge})
        End Function
        Protected Async Function HMResponseInternal(SourcePlayer As IPlayer, TargetPlayer As IPlayer, CardTypes As IEnumerable(Of CardCategories)) As Task(Of Boolean)
            Dim types As New StringBuilder
            Dim i As Integer = 0, le As Integer = CardTypes.Count
            For Each t In CardTypes
                types.Append(t.ToString)
                i += 1
                If i < CardTypes.Count Then
                    types.Append(" 或者 ")
                End If
            Next
            Dim typ = types.ToString
            If Await GameManager.Current.Window.MessageBox.ShowDialogAsync("WhetherUse一张" & typ & "？", "Response " + Name,
                                                N2MsgBoxButtons.YesNo,
                                                N2MsgBoxStyles.Message) =
                                                N2MsgBoxState.Ok Then
                Dim ca = Await TargetPlayer.PickCards(CardTypes)
                If ca.Count <> 0 Then
                    If ca.Count > 1 Then
                        Await GameManager.Current.Window.MessageBox.ShowDialogAsync("Select一张" & typ & "就够了，点击确定UseSelect的第一张。", "Count错误",,
                                             N2MsgBoxStyles.Warning)
                    End If
                    Await TargetPlayer.LostHandCard(ca.First)
                    Return True
                End If
            End If
            Return False
        End Function
        Public Overrides Async Function HumanResponse(SourcePlayer As IPlayer, TargetPlayer As IPlayer) As Task(Of Boolean)
            Await MyBase.HumanResponse(SourcePlayer, TargetPlayer)
            Return Await HMResponseInternal(SourcePlayer, TargetPlayer, {CardCategories.Dodge})
        End Function
    End Class
    Public Class Dodge
        Inherits HandCardBase
        Public Overrides ReadOnly Property Uid As String = "Dodge"
        Public Overrides ReadOnly Property AICategory As Integer = CardCategories.Dodge
        Public Overrides ReadOnly Property LeadStateCanBeUsed As Boolean = False
        Public Overrides ReadOnly Property RoundLimit As Integer = DefaultRoundLimits.NoLimit
        Public Overrides ReadOnly Property IsModCard As Boolean = False
        Public Overrides ReadOnly Property IsSuperWeapon As Boolean = False
        Public Overrides ReadOnly Property TechLevel As Integer = DefaultTechLevels.Essential
    End Class
    Public Class Repair
        Inherits HandCardBase
        Public Overrides ReadOnly Property Uid As String = "Repair"
        Public Overrides ReadOnly Property AICategory As Integer = CardCategories.Heals

        Public Overrides ReadOnly Property LeadStateCanBeUsed As Boolean = True
        Public Overrides ReadOnly Property RoundLimit As Integer = DefaultRoundLimits.NoLimit
        Public Overrides ReadOnly Property IsModCard As Boolean = False
        Public Overrides ReadOnly Property IsSuperWeapon As Boolean = False
        Public Overrides ReadOnly Property TechLevel As Integer = DefaultTechLevels.SupportAbility

        Public Overrides Async Function ResponseFail(SourcePlayer As IPlayer, TargetPlayer As IPlayer) As Task
            Await MyBase.ResponseFail(SourcePlayer, TargetPlayer)
            Await TargetPlayer.SetHealthAsync(SourcePlayer, TargetPlayer.Health + 1)
        End Function
    End Class
    Public Class Engineer
        Inherits HandCardBase
        Public Overrides ReadOnly Property AICategory As Integer = CardCategories.Heals
        Public Overrides ReadOnly Property LeadStateCanBeUsed As Boolean = True
        Public Overrides ReadOnly Property RoundLimit As Integer = DefaultRoundLimits.NoLimit
        Public Overrides ReadOnly Property IsModCard As Boolean = False
        Public Overrides ReadOnly Property IsSuperWeapon As Boolean = False
        Public Overrides ReadOnly Property TechLevel As Integer = DefaultTechLevels.MidInfantry
        Public Overrides Async Function AIResponse(SourcePlayer As IPlayer, TargetPlayer As IPlayer) As Task(Of Boolean)
            Await MyBase.AIResponse(SourcePlayer, TargetPlayer)
            If SourcePlayer.ControllerInformation.PlayerGroup = TargetPlayer.ControllerInformation.PlayerGroup Then
                Return False
            Else
                Return TargetPlayer.HasCards({CardCategories.Attack, CardCategories.SpecialAttack})
            End If
        End Function

        Public Overrides ReadOnly Property Uid As String = "Engineer"
        Public Overrides Async Function HumanResponse(SourcePlayer As IPlayer, TargetPlayer As IPlayer) As Task(Of Boolean)
            Await MyBase.HumanResponse(SourcePlayer, TargetPlayer)
            If SourcePlayer.ControllerInformation.PlayerGroup = TargetPlayer.ControllerInformation.PlayerGroup Then
                Return False
            Else
                Dim cards = From c In TargetPlayer.GetCards({CardCategories.Attack, CardCategories.SpecialAttack})
                            Where c.Points >= PatternPoints.Point
                If 0 = cards.Count Then
                    Return False
                Else
                    Return Await TargetPlayer.DisplayHandCard(cards(RndEx(0, cards.Count - 1)))
                End If
            End If
        End Function
        Public Overrides Async Function ResponseFail(SourcePlayer As IPlayer, TargetPlayer As IPlayer) As Task
            Await MyBase.ResponseFail(SourcePlayer, TargetPlayer)
            If SourcePlayer.ControllerInformation.PlayerGroup = TargetPlayer.ControllerInformation.PlayerGroup Then
                Await TargetPlayer.SetHealthAsync(SourcePlayer, TargetPlayer.Health + 1)
            Else
                Await TargetPlayer.SetHealthAsync(SourcePlayer, TargetPlayer.Health - 1)
                Await SourcePlayer.SetHealthAsync(SourcePlayer, SourcePlayer.Health + 1)
            End If
        End Function
    End Class
    Public MustInherit Class DefaultAmmoredAttack
        Inherits Attack
        Public Overrides ReadOnly Property AICategory As Integer = CardCategories.SpecialAttack
        Public Overrides ReadOnly Property LeadStateCanBeUsed As Boolean = True
        Public Overrides ReadOnly Property RoundLimit As Integer = DefaultRoundLimits.HighAmmor
        Public Overrides ReadOnly Property IsSuperWeapon As Boolean = False
        Public Overrides ReadOnly Property TechLevel As Integer = DefaultTechLevels.HighAmmor
    End Class
    Public Class AoeInvide
        Inherits Attack
        Public Overrides ReadOnly Property AICategory As Integer
            Get
                Return CardCategories.TacticsAttack
            End Get
        End Property

        Public Overrides ReadOnly Property Uid As String = "AoeInvide"
        Public Overrides ReadOnly Property IsAoe As Boolean = True

        Public Overrides Async Function AIResponse(SourcePlayer As IPlayer, TargetPlayer As IPlayer) As Task(Of Boolean)
            Await MyBase.AIResponse(SourcePlayer, TargetPlayer)
            Return Await AIResponseInternal(SourcePlayer, TargetPlayer, {CardCategories.Attack,
                                CardCategories.SpecialAttack})
        End Function
        Public Overrides Async Function HumanResponse(SourcePlayer As IPlayer, TargetPlayer As IPlayer) As Task(Of Boolean)
            Await MyBase.HumanResponse(SourcePlayer, TargetPlayer)
            Return Await HMResponseInternal(SourcePlayer, TargetPlayer, {CardCategories.Attack,
                                CardCategories.SpecialAttack})
        End Function
    End Class
    Public Class AoeAttak
        Inherits Attack
        Public Overrides ReadOnly Property AICategory As Integer
            Get
                Return CardCategories.TacticsAttack
            End Get
        End Property
        Public Overrides ReadOnly Property IsAoe As Boolean = True
        Public Overrides ReadOnly Property Uid As String = "AoeAttak"
        Public Overrides Function SelectPlayer(Players As IEnumerable(Of IPlayer), CurrentPlayer As IPlayer) As IEnumerable(Of IPlayer)
            Return From p In Players Where p.ControllerInformation.PlayerGroup = CurrentPlayer.ControllerInformation.PlayerGroup
        End Function
    End Class
    Public MustInherit Class SuperWeaponOrLauncher
        Inherits HandCardBase
        Public Overrides ReadOnly Property AICategory As Integer = CardCategories.SuperWeapon
        Public Overrides ReadOnly Property LeadStateCanBeUsed As Boolean = True
        Public Overrides ReadOnly Property RoundLimit As Integer = DefaultRoundLimits.SuperWeapon
        Public Overrides ReadOnly Property IsSuperWeapon As Boolean = True
        Public Overrides ReadOnly Property TechLevel As Integer = DefaultTechLevels.SuperWeapon
    End Class
End Namespace