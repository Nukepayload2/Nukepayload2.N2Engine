Imports System.Text
Imports Nukepayload2.N2Engine.Foundation

Namespace Battle

#Region "结构"
    ''' <summary>
    ''' 表示HandCard的PatternPoints
    ''' </summary>
    Public Class Poker
        Public Property Pattern As Patterns
        Public Property Point As String
        Sub New(Pattern As Patterns, Point As Integer)
            Me.Pattern = Pattern
            Dim tmp = String.Empty
            Select Case Point
                Case 1
                    tmp = "A"
                Case 11
                    tmp = "J"
                Case 12
                    tmp = "Q"
                Case 13
                    tmp = "K"
                Case Else
                    tmp = Point.ToString
            End Select
            Me.Point = tmp
        End Sub
    End Class
    ''' <summary>
    ''' 表示Player的姓Name和分组Information
    ''' </summary>
    Public Class PlayerInformation
        Public Property PlayerName As String
        Public Property PlayerGroup As String
        Shared Operator =(a As PlayerInformation, b As PlayerInformation) As Boolean
            Return a.PlayerName = b.PlayerName AndAlso a.PlayerGroup = b.PlayerGroup
        End Operator
        Shared Operator <>(a As PlayerInformation, b As PlayerInformation) As Boolean
            Return a.PlayerName <> b.PlayerName OrElse a.PlayerGroup <> b.PlayerGroup
        End Operator
        Sub New(PlayerName As String, GroupName As String)
            Me.PlayerName = PlayerName
            PlayerGroup = GroupName
        End Sub
    End Class
#End Region

#Region "枚举"
    Public Enum N2MsgBoxStyles
        Normal
        Message
        Warning
        Fault
    End Enum
    ''' <summary>
    ''' 表示MessageBox上的Button
    ''' </summary>
    Public Enum N2MsgBoxButtons
        Ok
        OkCancel
        Yes
        YesNo
    End Enum
    ''' <summary>
    ''' 表示InputBox上面的Button
    ''' </summary>
    Public Enum N2InputBoxButtons
        Ok
        OkCancel
        OkHelp
        OkCancelHelp
    End Enum
    ''' <summary>
    ''' MessageBoxBackTo的状态
    ''' </summary>
    Public Enum N2MsgBoxState
        Unselected
        Ok
        Cancel
    End Enum

    ''' <summary>
    ''' 用于ControlAI的Lead方式，通常对高级的AI有效，对简单的意义不Large。
    ''' </summary>
    Public Enum AIPersonality
        Defensive
        Aggressive
        Rush
        Balanced
        Guerrilla
    End Enum
    ''' <summary>
    ''' 表示AI思考的复杂程Degree
    ''' </summary>
    Public Enum AILevel
        HumanManaged
        None
        Low
        Medium
        High
        Brutal
    End Enum
    ''' <summary>
    ''' 告诉GameCoreManger应该怎样EndedCurrent局
    ''' </summary>
    Public Enum GameExitReason
        Crashed
        Exited
        Won
        Defeated
        ConnectionBroken
    End Enum

    ''' <summary>
    ''' 扑克Card的四种Pattern，在HandCardUse这些Pattern。
    ''' </summary>
    Public Enum Patterns
        ''' <summary>
        ''' ♣
        ''' </summary>
        Club
        ''' <summary>
        ''' ♠
        ''' </summary>
        Spade
        ''' <summary>
        ''' ♥
        ''' </summary>
        Heart
        ''' <summary>
        ''' ♦
        ''' </summary>
        Diamonds
    End Enum
    ''' <summary>
    ''' 每个人回合的三种Stage
    ''' </summary>
    Public Enum LeadStates
        Before
        Leading
        After
    End Enum

    ''' <summary>
    ''' 用于让AI识别Card的Types。 
    ''' 此Types可以用Or运算符来混合多个值
    ''' </summary>
    ''' <remarks></remarks>
    <Flags>
    Public Enum CardCategories
        Undefined = 0
        Heals = 1
        Attack = 1 << 1
        SpecialAttack = 1 << 2
        Dodge = 1 << 3
        SuperWeapon = 1 << 4
        Ammor = 1 << 5
        TacticsAttack = 1 << 6
        TacticsSupport = 1 << 7
        TacticsRisk = 1 << 8
        PowerPlant = 1 << 9
        AdvancedPowerPlant = 1 << 10
        Truncate = 1 << 11 - 1
    End Enum
    Enum DefaultRoundLimits
        NoLimit
        HighAmmor = 4
        DefenseAbility = 4
        Commando = 7 'HighInfantry 和 HeroAmmor 
        StrikeAbility = 8
        SuperWeapon = 9
    End Enum
    Enum DefaultTechLevels
        Essential = 1 '杀，闪，电
        SupportAbility '维修
        MidInfantry  '工程师,疯狂依文
        DropPods '空降
        DefenseAbility = 5 '防御建筑
        HighInfantry = 6 '鲍里斯，谭雅，尤里X
        HighAmmor = 7 '天启，光棱坦克，脑车
        HeroAmmor = 8 '保留。添加Mark为Mod的Card，类似：根除者虫甲，MARV，救赎者，万夫Extended攻城机甲...
        StrikeAbility = 9 '磁力卫星等
        SuperWeapon = 10 '六LargeSW
    End Enum
#End Region

#Region "Properties和字段"

#End Region
#Region "委托"

#End Region
#Region "接口"

    ''' <summary>
    ''' 各种钩子，用于打造神奇的Effect。
    ''' </summary>
    Public Interface IEffectHook

        ReadOnly Property Resources As IEnumerable(Of KeyValuePair(Of String, String))
        ReadOnly Property InlineResources As IEnumerable(Of KeyValuePair(Of String, Stream))
        ''' <summary>
        ''' Whether经Enabled这种Effect
        ''' </summary> 
        Property IsEnabled As Boolean
        ''' <summary>
        ''' Effect的Name
        ''' </summary> 
        ReadOnly Property Name As String
        ''' <summary>
        ''' Effect的简Brief描述
        ''' </summary> 
        ReadOnly Property Description As String
        ''' <summary>
        ''' 刚开局时触发的Effect
        ''' </summary> 
        Function RoundStartingAsync(Player As IPlayer) As Task
        ''' <summary>
        ''' 刚Ended时触发的Effect
        ''' </summary> 
        Function RoundEndingAsync(Player As IPlayer) As Task
        ''' <summary>
        ''' PlayerDefeated时触发的Effect
        ''' </summary>
        Function PlayerDefeatedAsync(CurPlayer As IPlayer, DefeatedPlayer As IPlayer) As Task
        ''' <summary>
        ''' 发Card之前进行的动作。
        ''' BackToFalse则这回合UnableTo拿Card。
        ''' </summary>
        ''' <returns>这回合CanUnableTo拿Card</returns>
        Function BeforeSendCardsAsync(Player As IPlayer) As Task(Of Boolean)
        ''' <summary>
        ''' 发Card之After进行的动作。
        ''' BackToFalse则这回合UnableToLead。
        ''' </summary>
        ''' <returns>这回合CanUnableToLead</returns>
        Function BeforeLeadAsync(Player As IPlayer) As Task(Of Boolean)
        ''' <summary>
        ''' Discard之前进行的动作。
        ''' BackToFalse则这回合UnableToDiscard。
        ''' </summary>
        ''' <returns>这回合CanUnableToDiscard</returns>
        Function BeforeDiscardAsync(Player As IPlayer) As Task(Of Boolean)
        ''' <summary>
        ''' 切换Player前进行的动作。
        ''' BackToFalse则这回合UnableTo切换ToValueNextPlayer。
        ''' </summary>
        ''' <returns>这回合CanUnableTo切换ToValueNextPlayer</returns>
        Function RoundEndedAsync(Player As IPlayer) As Task(Of Boolean)
        ''' <summary>
        ''' 进行ToValue下一回合时触发
        ''' </summary> 
        Function RoundIncrementAsync() As Task
        ''' <summary>
        ''' HealthWillChanged时触发。BackToFalse则会阻止Health的变化
        ''' </summary>
        ''' <returns>Whether允许Health的变化</returns>
        Function HealthChangingAsync(Player As IPlayer, SourcePlayer As IPlayer, NewValue As Integer) As Task(Of Boolean)
        ''' <summary>
        ''' HealthUpperBoundWillChanged时触发。BackToFalse则会阻止Health的变化
        ''' </summary> 
        ''' <returns>Whether允许HealthUpperBound的变化</returns>
        Function HealthUpperBoundChangingAsync(Player As IPlayer, SourcePlayer As IPlayer, NewValue As Integer) As Task(Of Boolean)
        ''' <summary>
        ''' PowerValueWillChanged时触发。BackToFalse则会阻止PowerValue的变化
        ''' </summary> 
        ''' <returns>Whether允许PowerValue的变化</returns>
        Function PowerChangingAsync(Player As IPlayer, SourcePlayer As IPlayer, NewValue As Integer) As Task(Of Boolean)
        ''' <summary>
        ''' MarkWillIncrementDecrement时触发。BackToFalse则会阻止Mark的变化
        ''' </summary> 
        ''' <returns>Whether允许Mark的Decrement</returns>
        Function MarkLosingAsync(Player As IPlayer, Source As IPlayer, NewValue As IEnumerable(Of IMark)) As Task(Of Boolean)
        ''' <summary>
        ''' MarkWillIncrementIncrement时触发。BackToFalse则会阻止Mark的变化
        ''' </summary> 
        ''' <returns>Whether允许Mark的Increment</returns>
        Function MarkAddedAsync(Player As IPlayer, Source As IPlayer, NewValue As IEnumerable(Of IMark)) As Task(Of Boolean)
        ''' <summary>
        ''' BackToFalse则UnableToFromValue其它PlayerAeraGot这张Card
        ''' </summary>
        ''' <returns>Can否FromValue其它PlayerAeraGot这张Card</returns>
        Function HandCardIncrementAsync(Player As IPlayer, HandCard As IHandCard) As Task(Of Boolean)
        ''' <summary>
        ''' BackToFalse则UnableToMilitaryOfficer这张Card移动ToValueDiscardAera
        ''' </summary>
        ''' <returns>Can否MilitaryOfficer这张Card移动ToValueDiscardAera</returns>
        Function HandCardDecrementAsync(Player As IPlayer, HandCard As IHandCard) As Task(Of Boolean)
        ''' <summary>
        ''' BackToFalse则不允许Use这张Card
        ''' </summary>
        ''' <returns>Can否Use这张Card</returns>
        Function UseCardAsync(Target As IPlayer, Source As IPlayer, Card As IHandCard) As Task(Of Boolean)
        ''' <summary>
        ''' BackToFalse则不允许阻止这张Card，直接触发Card的Effect。
        ''' </summary>
        ''' <returns>Can否阻止这张Card生效</returns>
        Function ResponseCardAsync(Target As IPlayer, Source As IPlayer, Card As IHandCard) As Task(Of Boolean)
        ''' <summary>
        ''' 经完成对Card的Response
        ''' </summary> 
        ''' <returns>保留的值</returns>
        Function ResponseCardEndedAsync(Target As IPlayer, Source As IPlayer, Card As IHandCard, SuccessResponse As Boolean) As Task(Of Boolean)
        ''' <summary>
        ''' Health&lt;1时触发
        ''' </summary> 
        ''' <returns>Whether进行Mayday</returns>
        Function DyingAsync(Player As IPlayer) As Task(Of Boolean)
        ''' <summary>
        ''' WillMayday之After触发
        ''' </summary> 
        ''' <returns>Whether立刻认为MaydayFail</returns>
        Function CallHelpAsync(Player As IPlayer) As Task(Of Boolean)
        ''' <summary>
        ''' ControlWhetherCanDisplayHandCard,修改Display的结果
        ''' </summary> 
        ''' <returns>WhetherShowDisplay的HandCard</returns>
        Function WillDisplayHandCardAsync(Player As IPlayer, DisplaySuccess As StrongBox(Of Boolean)) As Task(Of Boolean)

    End Interface
    ''' <summary>
    ''' 表示MilitaryOfficer拥有的MilitaryOfficerAbility
    ''' </summary>
    Public Interface IMilitaryOfficerAbility
        Inherits IEffectHook
        ReadOnly Property IsCampAbility As Boolean
    End Interface
    ''' <summary>
    ''' 表示Mark拥有的特殊Effect
    ''' </summary>
    Public Interface IMarkAbility
        Inherits IEffectHook
        ''' <summary>
        ''' 在IncrementAfter触发这个Procedure
        ''' </summary> 
        Function Increment(Player As IPlayer) As Task
        ''' <summary>
        ''' 在DecrementAfter触发这个Procedure
        ''' </summary> 
        Function Decrement(Player As IPlayer) As Task
        Property Owner As IMark
    End Interface
    ''' <summary>
    ''' 表示MilitaryOfficerCard上附加的Mark
    ''' </summary>
    Public Interface IMark
        Inherits IMarkCommand
        Property Count As Integer
        ReadOnly Property Abilities As IList(Of IMarkAbility)
        ReadOnly Property Content As String
        ReadOnly Property Description As String
        ReadOnly Property Icon As Uri
    End Interface
    ''' <summary>
    ''' 用来PlaySound，具体实现Is平台Attached的
    ''' </summary>
    Public Interface ISoundPlayer
        ReadOnly Property BackgroundMusic As IEnumerable(Of Stream)
        Sub PlayBackgroundMusic()
        Sub PauseBackgroundMusic()
        Sub ResumeBackgroundMusic()
        Sub NextOne()
        Function PlaySoundEffect(SoundID As String) As Task
        Function PlaySoundEffect(Sound As Stream) As Task
        Function PlaySoundEffect(Sound As Uri) As Task
        Function PlaySoundEffect(Sound As IEnumerable(Of Stream)) As Task
    End Interface
    ''' <summary>
    ''' 表示一种Camp
    ''' </summary>
    Public Interface ICamp
        ReadOnly Property SkirmishAvailable As Boolean
        ReadOnly Property OwnedHandCard As IList(Of IHandCard)
        ReadOnly Property OwnedMilitaryOfficer As IList(Of IMilitaryOfficer)
        ReadOnly Property Name As String
        ReadOnly Property BriefDescription As String
        ReadOnly Property ExtendedDescription As String
        ReadOnly Property DefaultAbility As IList(Of IMilitaryOfficerAbility)
        ReadOnly Property ThumbnailIcon As Uri
        ReadOnly Property LargeIcon As Uri
        ReadOnly Property DefaultCampColorArgb As Color
    End Interface
    Public Interface IMarkManager
        ReadOnly Property RegisteredMarks As IList(Of Type)
        Function CreateMarkControl(Mark As Type) As IMark
    End Interface
    ''' <summary>
    ''' Is一种MilitaryOfficerCardPicker
    ''' </summary>
    Public Interface IMilitaryOfficerCampPicker
        Function SelectCamp(ManualSelect As Boolean) As ICamp
        Function SelectMilitaryOfficer(ManualSelect As Boolean) As IMilitaryOfficer
    End Interface
    ''' <summary>
    ''' 进行GameCore设定
    ''' </summary>
    Public Interface IBattleGameSettingsManager
        Property MultiEngineer As Boolean
        Property AttackNearAlly As Boolean
        Property BadWeather As Boolean
        Property AllowModCards As Boolean
        Property IsSuperWeaponEnabled As Boolean
        Property IsCrateEnabled As Boolean
        Property TechLevelLimit As Integer
        ''' <summary>
        ''' 每个地图的Resource状况不一样。
        ''' 注意这项UnableTo写ToValueSettings文件里去
        ''' </summary>
        ''' <returns></returns>
        Property CardsPerRound As Integer
        Property HumanPlayerTimeout As TimeSpan
        Function SaveSettings() As Task
        Function LoadSettings() As Task
    End Interface
    ''' <summary>
    ''' 表示战场的Weather
    ''' </summary>
    Public Interface IWeather
        Inherits IEffectHook
        ''' <summary>
        ''' 影响战场的主题Color
        ''' </summary>
        ''' <returns></returns>
        Property ColorArgb As Integer
    End Interface
    ''' <summary>
    ''' GameCoreManger
    ''' </summary>
    Public Interface IGameCore
        ReadOnly Property Log As StringBuilder
        ReadOnly Property Window As IGameCoreWindow
        ReadOnly Property ClientAera As IList(Of ICustomCommand)
        ReadOnly Property MilitaryOfficerCampPicker As IMilitaryOfficerCampPicker
        ''' <summary>
        ''' 表示活着的Player
        ''' </summary>
        ReadOnly Property Players As IList(Of IPlayer)
        ReadOnly Property Round As Integer
        Property LeadStateOwner As IPlayer
        Function NextPlayer() As IPlayer
        Property GameCoreStarted As Boolean
        Function PlayerMaydayAsync(Sender As IPlayer, e As PlayerDeathJudgementEventArgs) As Task
        Function GameCoreStartAsync() As Task
        Function StartStageAsync(Player As IPlayer) As Task
        Function LeadStateAsync(Player As IPlayer) As Task
        Function EndedStageAsync(Player As IPlayer) As Task
        Function GameCoreEndedAsync(Reasom As GameExitReason) As Task
    End Interface
    ''' <summary>
    ''' ControlSave读档
    ''' </summary>
    Public Interface ISaveManger
        Function SaveGameCore(strm As Stream) As Task
        Function LoadGameCore(strm As Stream) As Task
    End Interface
    ''' <summary>
    ''' 可发Card，GetJudgementCard
    ''' </summary>
    Public Interface ICardManger
        ReadOnly Property AvailableHandCardTypes As IList(Of Type)
        ReadOnly Property AvailableHandCard As IEnumerable(Of IHandCard)
        Function RandomHandCard() As IHandCard
        Function CreateHandCard(Card As Type) As IHandCard
    End Interface
    Public Interface ICampManger
        ReadOnly Property RegisteredCamp As IList(Of ICamp)
        ReadOnly Property SkirmishCamp As IEnumerable(Of ICamp)
    End Interface
    ''' <summary>
    ''' 表示SuperWeapon的Effect
    ''' </summary>
    Public Interface ISuperWeapon
        Inherits IEffectHook
        Function LaunchedAsync(Sender As IPlayer, Target As IList(Of IPlayer)) As Task
    End Interface
    Public Interface ICheatsEffect
        Inherits IEffectHook
        Function EnablingAsync() As Task(Of Boolean)
        Function EnabledAsync() As Task
        Function WillDisableAsync() As Task(Of Boolean)
        Function DisabledAsync() As Task
    End Interface
    ''' <summary>
    ''' Card桌上的Player
    ''' </summary>
    Public Interface IPlayer
        Property HandCardUpperBoundAddition As Integer
        Property OrderId As Integer
        Property ContinuesRoundCount As Integer
        ReadOnly Property HandCardUpperBound As Integer
        Property CurrentStage As LeadStates
        Property Personality As AIPersonality
        Property Difficulty As AILevel
        Property Alive As Boolean
        Property ControllerInformation As PlayerInformation
        ReadOnly Property MilitaryOfficer As IMilitaryOfficer
        Property PlayerControl As Boolean
        Property Leading As Boolean
        ReadOnly Property AllHandCard As IEnumerable(Of IHandCard)
        ReadOnly Property AllMark As IEnumerable(Of IMark)
        ReadOnly Property SelectedHandCard As IEnumerable(Of IHandCard)
        ReadOnly Property Health As Integer
        ReadOnly Property HealthUpperBound As Integer
        ReadOnly Property PowerValue As Integer
        Sub ControlsIncrementHandCard(SourcePlayer As IPlayer, Card As IHandCard)
        Sub ControlsRemoveHandCard(SourcePlayer As IPlayer, Card As IHandCard)
        Sub ControlsIncrementMark(SourcePlayer As IPlayer, Mark As IMark)
        Sub ControlsDecrementMark(SourcePlayer As IPlayer, Mark As IMark)
        Function LostMark(Card As IMark) As Task
        Function GotMark(Card As IMark) As Task
        Function LostHandCard(Card As IHandCard) As Task
        Function LostHandCard(Card As IEnumerable(Of IHandCard)) As Task
        Function LostSelectedHandCard(Card As IEnumerable(Of IHandCard)) As Task
        Function LostHandCard(Index As Integer) As Task(Of IHandCard)
        Function RandomLostHandCard(Count As Integer) As Task(Of IEnumerable(Of IHandCard))
        Function GotHandCard(Card As IHandCard) As Task
        Function GotHandCard(Card As IEnumerable(Of IHandCard)) As Task
        Function GetHandCardPosition(Card As IHandCard) As Integer '存在首个Index否则<0
        Function GetMarkPosition(Mark As IMark) As Integer '存在首个Index否则<0
        Function ClearHandCard() As Task
        Function ProcessMayday(TargetPlayer As IPlayer, e As PlayerDeathJudgementEventArgs) As Task
        Function AIStart() As Task
        Function AILead() As Task
        Function AIEnded() As Task
        Function AIResponse(Player As IPlayer, Card As IHandCard) As Task
        Function HMStart() As Task
        Function HMLead() As Task
        Function HMEnded() As Task
        Function HMResponse(Player As IPlayer, Card As IHandCard) As Task
        Function HMResponseTimeout(Player As IPlayer, Card As IHandCard) As Task
        Function UseHandCard(Target As IEnumerable(Of IPlayer), Card As IHandCard) As Task
        Function UseHandCard(Target As IPlayer, Card As IHandCard) As Task
        '把Card放ToValueDiscard堆，然After收回。BackTo True CardDisplaySuccess BackTo False CardDisplayFail
        Function DisplayHandCard(Card As IHandCard) As Task(Of Boolean)
        Function HasCards(Category As Integer) As Boolean
        Function HasCards(Category As IEnumerable(Of CardCategories)) As Boolean
        Function GetCards(Category As IEnumerable(Of CardCategories)) As IEnumerable(Of IHandCard)
        ''' <summary>
        ''' Human或AI在CardAeraSelectSpecified的HandCard.AI会SelectAll符合条件的Card
        ''' </summary>
        ''' <param name="Category"></param>
        ''' <returns></returns>
        Function PickCards(Category As IEnumerable(Of CardCategories)) As Task(Of IEnumerable(Of IHandCard))
        Function SetPowerAsync(Source As IPlayer, value As Integer) As Task
        Function SetHealthUpperBoundAsync(Source As IPlayer, value As Integer) As Task
        Function SetHealthAsync(Source As IPlayer, value As Integer) As Task
    End Interface
    ''' <summary>
    ''' Thumbnail李子外挂的核心
    ''' </summary>
    Public Interface ICheatsEngine
        ReadOnly Property CheatsPlayer As IList(Of IPlayer) '在开局的时候把Effect加ToValuePlayer身上
        ReadOnly Property CheatsEffect As IList(Of ICheatsEffect)
    End Interface
    ''' <summary>
    ''' HandCard和MilitaryOfficerCard的Attributes
    ''' </summary>
    Public Interface ICard
        ''' <summary>
        ''' 触屏版 未实施,可Settings为Nothing.
        ''' </summary>
        ''' <returns></returns>
        ReadOnly Property Cursor As Uri
        ReadOnly Property SelectSound As IEnumerable(Of Stream)
        ReadOnly Property UseSound As IEnumerable(Of Stream)
        ReadOnly Property DeathSound As IEnumerable(Of Stream)
        ReadOnly Property Icon As Uri
        ReadOnly Property LargeIcon As Uri
        ReadOnly Property IsModCard As Boolean
        ReadOnly Property Name As String
        ReadOnly Property BriefDescription As String
        ReadOnly Property ExtendedDescription As String
        Function Initialized() As Task
        Function DisposeResources() As Task
    End Interface
    ''' <summary>
    ''' MilitaryOfficer的Attributes
    ''' </summary>
    Public Interface IMilitaryOfficer
        Inherits ICard, IMilitaryOfficerCommand
        Property Camp As ICamp
        Property DisposeResourcesAbilities As IList(Of IMilitaryOfficerAbility)
    End Interface
    ''' <summary>
    ''' HandCard的Attributes
    ''' </summary>
    Public Interface IHandCard
        Inherits ICard, IHandCardCommand
        ReadOnly Property CustomProperties As IDictionary(Of String, Object)
        Function SelectPlayer(Players As IEnumerable(Of IPlayer), CurrentPlayer As IPlayer) As IEnumerable(Of IPlayer)
        Property PatternPoints As Poker
        ReadOnly Property Pattern As String
        ReadOnly Property Points As String
        ReadOnly Property IsBlack As Boolean
        ReadOnly Property IsPeach As Boolean
        ReadOnly Property AICategory As Integer 'CardCategory
        ReadOnly Property ImmuneToPsychicalControl As Boolean
        ReadOnly Property IsSuperWeapon As Boolean
        Sub RandomGetPatternPoints()
        Function OnGotAsync(SourcePlayer As IPlayer, TargetPlayer As IPlayer) As Task
        Function OnLostAsync(SourcePlayer As IPlayer, TargetPlayer As IPlayer) As Task
        Function Responsed(SourcePlayer As IPlayer, TargetPlayer As IPlayer, e As CardResponseEventArgs) As Task(Of Boolean)
        ReadOnly Property TechLevel As Integer
        ReadOnly Property RoundLimit As Integer
        ReadOnly Property LeadStateCanBeUsed As Boolean
        ReadOnly Property IsAoe As Boolean
        ReadOnly Property LargestTargetCount As Integer
        ReadOnly Property IsCampSpecifiedCard As Boolean
        ReadOnly Property SpecifiedCamp As IEnumerable(Of Type)
    End Interface

    'UIAttached接口。ViewModel对View有Command时才会Use。
    ''' <summary>
    ''' 所有与UserControlAttached操作的UI元素都要实现这个接口。
    ''' </summary>
    Public Interface ICustomCommand
        ReadOnly Property Enabled As PropertyBinder(Of Boolean)
    End Interface
    ''' <summary>
    ''' MilitaryOfficerCard和HandCard
    ''' </summary>
    Public Interface ICardCommand
        Inherits ICustomCommand '在CardBox添加和移除
        ReadOnly Property BackSurfaceUp As PropertyBinder(Of Boolean)
        ReadOnly Property BeSelected As PropertyBinder(Of Boolean)
        ReadOnly Property OnClicked() As Func(Of Task)
        Sub EnabledCommand(BackSurfaceUp As PropertyBinder(Of Boolean),
                                    BeSelected As PropertyBinder(Of Boolean),
                                    Enabled As PropertyBinder(Of Boolean),
                                    OnClicked As Func(Of Task))
    End Interface
    ''' <summary>
    ''' 存放叠加的Mark
    ''' </summary>
    Public Interface IMarkAera
        Inherits IList(Of IMark)
    End Interface
    ''' <summary>
    ''' MilitaryOfficerCard上附加的MarkControl
    ''' </summary>
    Public Interface IMarkCommand
        Inherits ICustomCommand 'MarkAera添加移除
    End Interface
    ''' <summary>
    ''' MilitaryOfficer槽内的MilitaryOfficerCard
    ''' </summary>
    Public Interface IMilitaryOfficerCommand
        Inherits ICardCommand
        ReadOnly Property MarkAera As IMarkAera
        ReadOnly Property HealthBar As IHealthBarControl
        ReadOnly Property PowerBar As IPowerBar
        ReadOnly Property NameLabel As INameLabel
        Overloads Sub EnabledCommand(MarkAera As IMarkAera, HealthBar As IHealthBarControl, PowerBar As IPowerBar, NameLabel As INameLabel,
                           BackSurfaceUp As PropertyBinder(Of Boolean),
                           BeSelected As PropertyBinder(Of Boolean),
                           Enabled As PropertyBinder(Of Boolean),
                           OnClicked As Func(Of Task))
    End Interface
    ''' <summary>
    ''' 可Use可Judgement的HandCard
    ''' </summary>
    Public Interface IHandCardCommand
        Inherits ICardCommand
    End Interface

    ''' <summary>
    ''' 让UserInput一段字，相当于InputBox
    ''' </summary>
    Public Interface IInputBox
        Function ShowDialogAsync(Prompt As String, Title As String, Optional Buttons As N2InputBoxButtons = N2InputBoxButtons.OkCancel) As Task(Of String)
    End Interface
    ''' <summary>
    ''' 弹Box通知User或让UserSelect，相当于MsgBox
    ''' </summary>
    Public Interface IMessageBox
        Function ShowDialogAsync(Prompt As String, Title As String, Optional Buttons As N2MsgBoxButtons = N2MsgBoxButtons.Ok, Optional Style As N2MsgBoxStyles = N2MsgBoxStyles.Message) As Task(Of N2MsgBoxState)
        Function ShowAsync(Prompt As String, Title As String, Optional Buttons As N2MsgBoxButtons = N2MsgBoxButtons.Ok, Optional Style As N2MsgBoxStyles = N2MsgBoxStyles.Message) As Task(Of N2MsgBoxState)
    End Interface
    ''' <summary>
    ''' DiscardAera和HumanPlayer的CardAeraViewModel
    ''' </summary>
    Public Interface IHandCardBox
        Inherits IList(Of IHandCard)
    End Interface
    ''' <summary>
    ''' ShowPlayer的Health
    ''' </summary>
    Public Interface IHealthBarControl
        Property Health As Integer
        Property HealthUpperBound As Integer
    End Interface
    ''' <summary>
    ''' ShowPowerValue百分比
    ''' </summary>
    Public Interface IPowerBar '新东西 
        Property PowerValueUpperBound As Integer
        Property PowerValueUse As Integer
    End Interface
    ''' <summary>
    ''' ShowPlayer的Information
    ''' </summary>
    Public Interface INameLabel '新东西,Card局初始化时更新上面的数据
        Property PlayerName As String
        Property ThumbnailTeamName As String
        Property ActId As Integer
    End Interface
    ''' <summary>
    ''' ButtonControl，Name一般IsButton
    ''' </summary>
    Public Interface IButton
        Inherits ICustomCommand
        Function OnClick() As Task
    End Interface
    ''' <summary>
    ''' GameCore界面最外层的Box架，内容会跟着它移动
    ''' </summary>
    Public Interface IGameCoreWindow
        ReadOnly Property MessageBox As IMessageBox
        ReadOnly Property InputBox As IInputBox
        ReadOnly Property JudgementAera As IHandCardBox
        Function ShowPlayerWinScreen() As Task
        Function ShowPlayerDefeatedScreen() As Task
        Function BackToTitleScreen() As Task
        Function ExitApplication() As Task
        Function Vibration(Amplitude As Double, ExtendedDegree As TimeSpan) As Task
    End Interface
    ''' <summary>
    ''' Player查看或操作CurrentMilitaryOfficer的PlayerAera
    ''' </summary>
    Public Interface IPlayerAera
        Inherits ICustomCommand '可滚动的PlayerBox中添加移除
        ReadOnly Property MilitaryOfficerCard As IMilitaryOfficer
        ReadOnly Property HandCardAera As IHandCardBox
        Property HumanPlayerLeadMode As Boolean
        Property HumanPlayerDiscardMode As Boolean
        ReadOnly Property HumanPlayerProcedureEndedButton As IButton
        Function PickCards() As Task(Of IEnumerable(Of IHandCard))
    End Interface

    ''' <summary>
    ''' DeviceAttached的接口 
    ''' </summary>
    Public Interface IDeviceControl
        Sub Vibration(Time As TimeSpan) 'CanVibration的就Vibration吧！配合一下GameCoreAera的晃动与SoundEffect就完美了！

    End Interface
#End Region
End Namespace