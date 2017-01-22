Imports Nukepayload2.N2Engine.N2Math

Namespace Battle
    Public MustInherit Class Player
        Implements IPlayer
        Protected PlayerAera As IPlayerAera

        Private _Alive As Boolean = True
        Public Property Alive As Boolean Implements IPlayer.Alive
            Get
                Return Alive
            End Get
            Set(value As Boolean)
                _Alive = value
                If Not _Alive Then RaisePlayerOut(Me)
            End Set
        End Property
        Public Property Personality As AIPersonality Implements IPlayer.Personality
        Public Property ControllerInformation As PlayerInformation Implements IPlayer.ControllerInformation
        Public Property Leading As Boolean Implements IPlayer.Leading
        Dim _PlayerControl As Boolean
        Public Property Difficulty As AILevel Implements IPlayer.Difficulty
        Public Property CurrentStage As LeadStates Implements IPlayer.CurrentStage
        Public Property ContinuesRoundCount As Integer Implements IPlayer.ContinuesRoundCount

        Sub New(PlayerAera As IPlayerAera,
            Personality As AIPersonality,
            ControllerInformation As PlayerInformation,
            PlayerControl As Boolean,
            Optional Difficulty As AILevel = AILevel.Low,
            Optional Alive As Boolean = True)
            Me.PlayerAera = PlayerAera
            Me.Difficulty = Difficulty
            Me.PlayerControl = PlayerControl '必须在Difficulty之After
            Me.Alive = Alive
            Me.Personality = Personality
            Me.ControllerInformation = ControllerInformation
            Leading = False
            CurrentStage = LeadStates.Before
            ContinuesRoundCount = 0
        End Sub
        Protected MustOverride Property MilitaryOfficerCardHasDecoration As Boolean
        Public ReadOnly Property AllHandCard As IEnumerable(Of IHandCard) Implements IPlayer.AllHandCard
            Get
                Return CType(PlayerAera.HandCardAera, IEnumerable(Of IHandCard))
            End Get
        End Property

        Public ReadOnly Property AllMark As IEnumerable(Of IMark) Implements IPlayer.AllMark
            Get
                Return CType(PlayerAera.MilitaryOfficerCard.MarkAera, IEnumerable(Of IMark)) 'todo:if contain inc else addnew
            End Get
        End Property

        Public Property PlayerControl As Boolean Implements IPlayer.PlayerControl
            Set(value As Boolean)
                _PlayerControl = value
                Difficulty = AILevel.HumanManaged
            End Set
            Get
                Return _PlayerControl
            End Get
        End Property
        Public ReadOnly Property SelectedHandCard As IEnumerable(Of IHandCard) Implements IPlayer.SelectedHandCard
            Get
                Dim lst As New List(Of IHandCard)
                lst.AddRange(From card In PlayerAera.HandCardAera Where card.BeSelected.Value)
                Return lst
            End Get
        End Property
        Protected Overridable Function GetAttackablePlayers(Player As IPlayer) As IEnumerable(Of IPlayer)
            Select Case Difficulty
                Case AILevel.Low
                    Dim lst As New List(Of IPlayer)
                    lst.AddRange(From Pl In GameManager.Current.Players Where Pl.ControllerInformation.PlayerGroup <> Pl.ControllerInformation.PlayerGroup)
                    Return lst
                Case Else
                    Throw New NotImplementedException
            End Select
        End Function
        Protected Overridable Function SelectAttackablePlayer(Players As IEnumerable(Of IPlayer)) As IPlayer
            Select Case Difficulty
                Case AILevel.Low
                    If Players.Count > 0 Then
                        Return Players(RndEx(0, Players.Count - 1))
                    Else
                        Return Nothing
                    End If
                Case Else
                    Throw New NotImplementedException
            End Select
        End Function

        Protected Overridable Function GetHelpablePlayers(Player As IPlayer) As IEnumerable(Of IPlayer)
            Select Case Difficulty
                Case AILevel.Low
                    Dim lst As New List(Of IPlayer)
                    lst.AddRange(From p In GameManager.Current.Players Where p.ControllerInformation.PlayerGroup = p.ControllerInformation.PlayerGroup)
                    Return lst
                Case Else
                    Throw New NotImplementedException
            End Select
        End Function
        Protected Overridable Function SelectHelpablePlayer(Players As IEnumerable(Of IPlayer)) As IPlayer
            Select Case Difficulty
                Case AILevel.Low
                    If Players.Count > 0 Then
                        Return Players(RndEx(0, Players.Count - 1))
                    Else
                        Return Nothing
                    End If
                Case Else
                    Throw New NotImplementedException
            End Select
        End Function
        Protected Overridable Async Function DefaultAttackHandCardProcess(Card As IHandCard, IsDangerous As Boolean) As Task
            Select Case Personality
                Case AIPersonality.Aggressive
                    Dim pl = SelectAttackablePlayer(GetAttackablePlayers(Me))
                    If pl IsNot Nothing Then Await UseHandCard(pl, Card)
                Case Else
                    Throw New NotImplementedException
            End Select
        End Function
        Protected Overridable Async Function DefaultSupportHandCardProcess(Card As IHandCard) As Task
            Select Case Personality
                Case AIPersonality.Aggressive
                    Dim pl = SelectHelpablePlayer(GetHelpablePlayers(Me))
                    If pl IsNot Nothing Then Await UseHandCard(pl, Card)
                Case Else
                    Throw New NotImplementedException
            End Select
        End Function
        Protected Overridable Async Function DefaultSelfHandCardProcess(Card As IHandCard, IsDangerous As Boolean) As Task
            Select Case Personality
                Case AIPersonality.Aggressive
                    Await UseHandCard(Me, Card)
                Case Else
                    Throw New NotImplementedException
            End Select
        End Function
        Public Overridable Async Function AILead() As Task Implements IPlayer.AILead
            If Await LeadStateCommon() Then
                'Lead
                Select Case Difficulty
                    Case AILevel.None
                        '直接Ended。什么也NoNeed打
                    Case AILevel.Low
                        For i As Integer = PlayerAera.HandCardAera.Count - 1 To 0 Step -1
                            Dim fl = PlayerAera.HandCardAera(i).AICategory
                            If fl.HasFlags(CardCategories.PowerPlant) OrElse
                                 fl.HasFlags(CardCategories.Ammor) Then
                                Await DefaultSelfHandCardProcess(PlayerAera.HandCardAera(i), False)
                            ElseIf fl.HasFlags(CardCategories.AdvancedPowerPlant) Then
                                Await DefaultSelfHandCardProcess(PlayerAera.HandCardAera(i), True)
                            ElseIf fl.HasFlags(CardCategories.Heals) Then

                                If PlayerAera.MilitaryOfficerCard.HealthBar.HealthUpperBound > PlayerAera.MilitaryOfficerCard.HealthBar.Health Then
                                    Await UseHandCard(Me, PlayerAera.HandCardAera(i))
                                End If
                            ElseIf fl.HasFlags(CardCategories.Attack) OrElse
                                 fl.HasFlags(CardCategories.SpecialAttack) OrElse
                                 fl.HasFlags(CardCategories.SuperWeapon) OrElse
                                 fl.HasFlags(CardCategories.TacticsAttack) Then
                                Await DefaultAttackHandCardProcess(PlayerAera.HandCardAera(i), False)
                            ElseIf fl.HasFlags(CardCategories.TacticsRisk) Then
                                Await DefaultAttackHandCardProcess(PlayerAera.HandCardAera(i), True)
                            ElseIf fl.HasFlags(CardCategories.TacticsSupport) Then
                                Await DefaultSupportHandCardProcess(PlayerAera.HandCardAera(i))
                            ElseIf fl.HasFlags(CardCategories.Undefined) Then
                                Throw New ArgumentNullException("CardCategory")
                            End If
                        Next
                    Case Else
                        Throw New NotImplementedException("未实现的Function")
                End Select
            End If
            Await GameManager.Current.EndedStageAsync(Me)
        End Function

        Public Async Function AIResponse(Player As IPlayer, Card As IHandCard) As Task Implements IPlayer.AIResponse
            Dim suc = Card.Responsed(Player, Me, New CardResponseEventArgs(Not Await ValueEffectProc(NameOf(IEffectHook.ResponseCardAsync), Me, Player, Card), False))
            Await ValueEffectProc(NameOf(IEffectHook.ResponseCardEndedAsync), Me, Player, Card, suc)
        End Function

        Public ReadOnly Property HandCardUpperBound As Integer Implements IPlayer.HandCardUpperBound
            Get
                Return PlayerAera.MilitaryOfficerCard.HealthBar.Health + PlayerAera.MilitaryOfficerCard.PowerBar.PowerValueUpperBound + HandCardUpperBoundAddition
            End Get
        End Property

        Public ReadOnly Property Health As Integer Implements IPlayer.Health
            Get
                Return PlayerAera.MilitaryOfficerCard.HealthBar.Health
            End Get
        End Property

        Public Async Function SetHealthAsync(Source As IPlayer, value As Integer) As Task Implements IPlayer.SetHealthAsync
            If Await ValueEffectProc(NameOf(IEffectHook.PowerChangingAsync), Me, Source, value) Then
                PlayerAera.MilitaryOfficerCard.HealthBar.Health = If(PlayerAera.MilitaryOfficerCard.HealthBar.HealthUpperBound > value, value, PlayerAera.MilitaryOfficerCard.HealthBar.HealthUpperBound)
                If PlayerAera.MilitaryOfficerCard.HealthBar.Health <= 0 AndAlso Await StageEffectProc(NameOf(IEffectHook.DyingAsync), Me) Then
                    Await GameManager.Current.PlayerMaydayAsync(Me, New PlayerDeathJudgementEventArgs(True, Await StageEffectProc(NameOf(IEffectHook.CallHelpAsync), Me)))
                End If
            End If
        End Function

        Public ReadOnly Property PowerValue As Integer Implements IPlayer.PowerValue
            Get
                Return PlayerAera.MilitaryOfficerCard.PowerBar.PowerValueUpperBound
            End Get
        End Property

        Public Async Function SetPowerAsync(Source As IPlayer, value As Integer) As Task Implements IPlayer.SetPowerAsync
            If Await ValueEffectProc(NameOf(IEffectHook.PowerChangingAsync), Me, Source, value) Then
                PlayerAera.MilitaryOfficerCard.PowerBar.PowerValueUpperBound = value
            End If
        End Function

        Public ReadOnly Property MilitaryOfficer As IMilitaryOfficer Implements IPlayer.MilitaryOfficer
            Get
                Return PlayerAera.MilitaryOfficerCard
            End Get
        End Property

        Public Property OrderId As Integer Implements IPlayer.OrderId
            Get
                Dim v = PlayerAera?.MilitaryOfficerCard?.NameLabel?.ActId
                Return If(v.HasValue, v.Value, 0)
            End Get
            Set(value As Integer)
                PlayerAera.MilitaryOfficerCard.NameLabel.ActId = value
            End Set
        End Property

        Public ReadOnly Property HealthUpperBound As Integer Implements IPlayer.HealthUpperBound
            Get
                Return PlayerAera.MilitaryOfficerCard.HealthBar.HealthUpperBound
            End Get
        End Property

        Public Async Function SetHealthUpperBoundAsync(Source As IPlayer, value As Integer) As Task Implements IPlayer.SetHealthUpperBoundAsync
            If Await ValueEffectProc(NameOf(IEffectHook.HealthUpperBoundChangingAsync), Me, Source, value) Then
                PlayerAera.MilitaryOfficerCard.HealthBar.HealthUpperBound = value
                If PlayerAera.MilitaryOfficerCard.HealthBar.Health > value Then PlayerAera.MilitaryOfficerCard.HealthBar.Health = value
                If PlayerAera.MilitaryOfficerCard.HealthBar.Health <= 0 AndAlso Await StageEffectProc(NameOf(IEffectHook.DyingAsync), Me) Then
                    Await GameManager.Current.PlayerMaydayAsync(Me, New PlayerDeathJudgementEventArgs(True, True)) '必死无疑
                End If
            End If
        End Function

        Public Property HandCardUpperBoundAddition As Integer = 0 Implements IPlayer.HandCardUpperBoundAddition


        ''' <summary>
        ''' ProcessMilitaryOfficerCard的装饰，Mark和摸Card
        ''' </summary> 
        Protected Overridable Async Function StartStageCommon() As Task
            MilitaryOfficerCardHasDecoration = True
            ContinuesRoundCount += 1
            Dim CanGetCards As Boolean = Await StageEffectProc(NameOf(IEffectHook.RoundStartingAsync), Me)
            If CanGetCards Then
                CurrentStage = LeadStates.Before
                Dim lc As New List(Of IHandCard)
                For i As Integer = 1 To BattleGameSettingsManager.Current.CardsPerRound
                    lc.Add(CardManger.Current.RandomHandCard)
                Next
                Await GotHandCard(lc)
            End If
            Await GameManager.Current.LeadStateAsync(Me)
        End Function

        '压制Obsolete产生的警告,vb 14 +
#Disable Warning BC40000, BC40008
        ''' <summary>
        ''' 用于ProcessStageAttached的Effect
        ''' </summary>
        ''' <returns>Can否通过Stage</returns>
        Protected Overridable Async Function StageEffectProc(EffectName As String, Player As IPlayer) As Task(Of Boolean)
            Return Await WeakEffectProc(EffectName, Player)
        End Function
        ''' <summary>
        ''' 用于Process值Attached的Effect,例如HealthChanged，CardBeResponse，CardCanUnableToUse
        ''' </summary>
        ''' <returns>Can否通过Stage</returns>
        Protected Overridable Async Function ValueEffectProc(Of T)(EffectName As String, Player As IPlayer, Source As IPlayer, Attached值 As T) As Task(Of Boolean)
            Return Await WeakEffectProc(EffectName, Player, Source, Attached值)
        End Function
        ''' <summary>
        ''' 用于Process与前一StageAttached的值Attached的Effect,例如CardResponseEnded
        ''' </summary>
        ''' <returns>Can否通过Stage</returns>
        Protected Overridable Async Function ValueEffectProc(Of T1, T2)(EffectName As String, Player As IPlayer, Source As IPlayer, Attached值 As T1, 前一Stage参数 As T2) As Task(Of Boolean)
            Return Await WeakEffectProc(EffectName, Player, Source, Attached值, 前一Stage参数)
        End Function
        ''' <summary>
        ''' 用于Process与Source头无关的值Attached的Effect,例如GotHandCard
        ''' </summary>
        ''' <returns>Can否通过Stage</returns>
        Protected Overridable Async Function ValueEffectProcNoSource(Of T)(EffectName As String, Player As IPlayer, Attached值 As T) As Task(Of Boolean)
            Return Await WeakEffectProc(EffectName, Player, Attached值)
        End Function
#Enable Warning BC40000, BC40008

        ''' <summary>
        ''' 基于反射的弱TypesEffectProcess。用于判断WhetherCan通过TargetStage。
        ''' 不要直接Use这个API，直接用非常容易出错。
        ''' </summary>
        ''' <param name="StageName">StageName，必须匹配函数Name</param>
        ''' <param name="Params">调用这个Stage时应Use的参数</param>
        ''' <returns>WhetherCan通过相应的Stage</returns>
        <Obsolete("为了提高安全性，请改用ValueChangeEffectProcNoSource,ValueChangeEffectProc或StageEffectProc等强Types的API")>
        Protected Overridable Async Function WeakEffectProc(StageName As String, ParamArray Params As Object()) As Task(Of Boolean)
            Dim CanPassStage As Boolean = True
            For Each bj In PlayerAera.MilitaryOfficerCard.MarkAera
                For Each tj In bj.Abilities
                    If tj.IsEnabled AndAlso Not Await tj.EnterStage(StageName, Params) Then CanPassStage = False
                Next
            Next
            If CanPassStage Then
                For Each tj In PlayerAera.MilitaryOfficerCard.DisposeResourcesAbilities
                    If tj.IsEnabled AndAlso Not Await tj.EnterStage(StageName, Params) Then CanPassStage = False
                Next
            End If
            Return CanPassStage
        End Function
        ''' <summary>
        ''' ProcessMark并BackToWhetherCanLead
        ''' </summary> 
        ''' <returns>WhetherCanLead</returns>
        Protected Overridable Async Function LeadStateCommon() As Task(Of Boolean)
            Dim CanUseCards As Boolean = Await StageEffectProc(NameOf(IEffectHook.BeforeLeadAsync), Me)
            CurrentStage = LeadStates.Leading
            Return CanUseCards
        End Function

        ''' <summary>
        ''' ProcessMark并BackToWhetherCanDiscard
        ''' </summary> 
        ''' <returns>WhetherCanDiscard</returns>
        Protected Overridable Async Function EndedStageCommon() As Task(Of Boolean)
            Dim CanRemoveCards As Boolean = Await StageEffectProc(NameOf(IEffectHook.BeforeDiscardAsync), Me)
            CurrentStage = LeadStates.After
            Return CanRemoveCards
        End Function

        ''' <summary>
        ''' ProcessMark并BackToWhetherCanDiscard
        ''' </summary> 
        Protected Overridable Async Sub AfterEndedStageCommon()
            Dim CanGoNextStage As Boolean = Await StageEffectProc(NameOf(IEffectHook.RoundEndedAsync), Me)
            MilitaryOfficerCardHasDecoration = False
            If Not CanGoNextStage Then
                Await GameManager.Current.LeadStateAsync(Me)
            Else
                ContinuesRoundCount = 0
            End If
        End Sub

        'MilitaryOfficerCard
        Public Async Function AIStart() As Task Implements IPlayer.AIStart
            Await StartStageCommon()
        End Function

        Public Async Function AIEnded() As Task Implements IPlayer.AIEnded
            If Await EndedStageCommon() Then
                'Discard。
                Select Case Difficulty
                    Case AILevel.None
                        '不动的...不Discard了吧。
                    Case AILevel.Low
                        Dim lc As New List(Of IHandCard)
                        Do While AllHandCard.Count - lc.Count > HandCardUpperBound AndAlso AllHandCard.Count > 0
                            Dim card = AllHandCard(RndEx(0, AllHandCard.Count))
                            If Not lc.Contains(card) Then
                                lc.Add(card)
                            End If
                        Loop
                        Await LostHandCard(lc)
                    Case Else
                        Throw New NotImplementedException
                End Select
            End If
            AfterEndedStageCommon()
        End Function

        Public Async Function HMLead() As Task Implements IPlayer.HMLead
            If Await LeadStateCommon() Then
                PlayerAera.HumanPlayerLeadMode = True
                Dim Cancel As New System.Threading.CancellationToken
                Cancel.Register(AddressOf PlayerAera.HumanPlayerProcedureEndedButton.OnClick)
                Await Task.Delay(BattleGameSettingsManager.Current.HumanPlayerTimeout, Cancel)
                PlayerAera.HumanPlayerLeadMode = False
            End If
            Await GameManager.Current.EndedStageAsync(Me)
        End Function

        Public Async Function HMResponse(Player As IPlayer, Card As IHandCard) As Task Implements IPlayer.HMResponse
            Await Card.Responsed(Player, Me, New CardResponseEventArgs(Not Await ValueEffectProc(NameOf(IEffectHook.ResponseCardAsync), Me, Player, Card), True))
        End Function

        Public Async Function HMResponseTimeout(Player As IPlayer, Card As IHandCard) As Task Implements IPlayer.HMResponseTimeout
            Await Card.Responsed(Player, Me, New CardResponseEventArgs(Not Await ValueEffectProc(NameOf(IEffectHook.ResponseCardAsync), Me, Player, Card), True, True))
        End Function

        Public Async Function HMStart() As Task Implements IPlayer.HMStart
            Await StartStageCommon()
        End Function

        Public Async Function HMEnded() As Task Implements IPlayer.HMEnded
            If Await EndedStageCommon() Then
                PlayerAera.HumanPlayerDiscardMode = True
                Dim Cancel As New System.Threading.CancellationToken
                Cancel.Register(AddressOf PlayerAera.HumanPlayerProcedureEndedButton.OnClick)
                Await Task.Delay(BattleGameSettingsManager.Current.HumanPlayerTimeout, Cancel)
                PlayerAera.HumanPlayerDiscardMode = False
            End If
            AfterEndedStageCommon()
        End Function

        Public Async Function UseHandCard(Target As IEnumerable(Of IPlayer), Card As IHandCard) As Task Implements IPlayer.UseHandCard
            If Card.IsAoe Then
                Target = Card.SelectPlayer(GameManager.Current.Players, Me)
            End If
            For Each mb In Target
                If mb.PlayerControl Then
                    Await mb.HMResponse(Me, Card)
                Else
                    Await mb.AIResponse(Me, Card)
                End If
            Next
        End Function

        Protected Sub EnableNonHealCards(Enable As Boolean)
            EnableNonSpecificCards(Enable, {CardCategories.Heals})
        End Sub
        Protected Sub EnableNonSpecificCards(Enable As Boolean, type As IEnumerable(Of CardCategories))
            For Each tp In type
                For Each c In From ca In AllHandCard Where Not CInt(tp).HasFlags(ca.AICategory)
                    c.Enabled.Value = Enable
                Next
            Next
        End Sub
        Protected Sub EnableSpecificCards(Enable As Boolean, type As IEnumerable(Of CardCategories))
            For Each tp In type
                For Each c In From ca In AllHandCard Where CInt(tp).HasFlags(ca.AICategory)
                    c.Enabled.Value = Enable
                Next
            Next
        End Sub

        Public Async Function ProcessMayday(Player As IPlayer, e As PlayerDeathJudgementEventArgs) As Task Implements IPlayer.ProcessMayday
            If HasCards(CardCategories.Heals) Then
                If PlayerControl Then
                    If Await GameManager.Current.Window.MessageBox.ShowDialogAsync("是否救" + Player.ControllerInformation.PlayerName + ", 组" + Player.ControllerInformation.PlayerGroup + "?", "PlayerMayday", N2MsgBoxButtons.YesNo) = N2MsgBoxState.Ok Then
                        EnableNonHealCards(False)
                        For Each card In Await PlayerAera.PickCards()
                            Await UseHandCard(Player, card)
                        Next
                        EnableNonHealCards(True)
                    End If
                Else
                    For Each c In AllHandCard
                        If c.AICategory = CardCategories.Heals Then
                            Await UseHandCard(Player, c)
                        End If
                        If Player.Health > 0 Then Exit For
                    Next
                End If
            End If
        End Function

        Public Async Function ClearHandCard() As Task Implements IPlayer.ClearHandCard
            Await LostHandCard(AllHandCard)
        End Function


        Public Async Function LostHandCard(Index As Integer) As Task(Of IHandCard) Implements IPlayer.LostHandCard
            Dim r = AllHandCard(Index)
            Await LostHandCard({AllHandCard(Index)})
            Return r
        End Function

        Public Async Function LostHandCard(Card As IHandCard) As Task Implements IPlayer.LostHandCard
            Await LostHandCard({Card})
        End Function

        Public Async Function LostSelectedHandCard(Card As IEnumerable(Of IHandCard)) As Task Implements IPlayer.LostSelectedHandCard
            Dim lsele As New List(Of IHandCard)
            For Each c In AllHandCard
                If c.BeSelected.Value Then
                    lsele.Add(c)
                End If
            Next
            Await LostHandCard(lsele)
        End Function

        Public Function GetHandCardPosition(Card As IHandCard) As Integer Implements IPlayer.GetHandCardPosition
            Dim i As Integer = 0
            For Each c In AllHandCard
                If c.Equals(Card) Then
                    Return i
                End If
                i += 1
            Next
            Return -1
        End Function

        Public Function GetMarkPosition(Mark As IMark) As Integer Implements IPlayer.GetMarkPosition
            Dim i As Integer = 0
            For Each c In AllMark
                If c.Equals(Mark) Then
                    Return i
                End If
                i += 1
            Next
            Return -1
        End Function

        Public Async Function GotHandCard(Card As IHandCard) As Task Implements IPlayer.GotHandCard
            Await GotHandCard({Card})
        End Function

        Public Async Function RandomLostHandCard(Count As Integer) As Task(Of IEnumerable(Of IHandCard)) Implements IPlayer.RandomLostHandCard
            If Count > AllHandCard.Count Then
                Dim r = AllHandCard.ToArray
                Await ClearHandCard()
                Return r
            Else
                Dim ids As New List(Of Integer)
                Dim cards As New List(Of IHandCard)
                Do While ids.Count < Count
                    Dim ran = RandomGenerator.RandomInt32(0, Count)
                    If Not ids.Contains(ran) Then
                        ids.Add(ran)
                        cards.Add(AllHandCard(ran))
                    End If
                Loop
                Dim r = cards.ToArray
                Await LostHandCard(cards)
                Return r
            End If
        End Function

        Public Async Function UseHandCard(Target As IPlayer, Card As IHandCard) As Task Implements IPlayer.UseHandCard
            Await UseHandCard({Target}, Card)
        End Function
        Public MustOverride Async Function LostHandCard(Card As IEnumerable(Of IHandCard)) As Task Implements IPlayer.LostHandCard

        Public MustOverride Async Function GotHandCard(Card As IEnumerable(Of IHandCard)) As Task Implements IPlayer.GotHandCard

        Public Async Function PickCards(Category As IEnumerable(Of CardCategories)) As Task(Of IEnumerable(Of IHandCard)) Implements IPlayer.PickCards
            If PlayerControl Then
                EnableNonSpecificCards(False, Category)
                Dim ca = Await PlayerAera.PickCards
                EnableNonSpecificCards(True, Category)
                Return ca
            Else
                Return From ca In AllHandCard Where Category.Contains(ca.AICategory)
            End If
        End Function

        Protected MustOverride Function DisplayHandCardImpl(Card As IHandCard) As Task

        Public Async Function DisplayHandCard(Card As IHandCard) As Task(Of Boolean) Implements IPlayer.DisplayHandCard
            If PlayerControl AndAlso Await GameManager.Current.Window.MessageBox.ShowDialogAsync("WhetherDisplayCard" + Card.ToString + "？", "Display", N2MsgBoxButtons.YesNo) = N2MsgBoxState.Ok OrElse Not PlayerControl Then
                Dim result As New StrongBox(Of Boolean)
                If Await ValueEffectProcNoSource(NameOf(IEffectHook.WillDisplayHandCardAsync), Me, result) Then
                    Await DisplayHandCardImpl(Card)
                End If
                Return result.Value
            Else
                Return False
            End If
        End Function
        Public Function HasCards(Category As IEnumerable(Of CardCategories)) As Boolean Implements IPlayer.HasCards
            For Each fl In Category
                For Each c In AllHandCard
                    If (c.AICategory And fl) <> 0 Then
                        Return True
                    End If
                Next
            Next
            Return False
        End Function

        Public Function GetCards(Category As IEnumerable(Of CardCategories)) As IEnumerable(Of IHandCard) Implements IPlayer.GetCards
            Dim ls As New List(Of IHandCard)
            For Each fl In Category
                For Each c In AllHandCard
                    If (c.AICategory And fl) <> 0 Then
                        ls.Add(c)
                    End If
                Next
            Next
            Return ls
        End Function
        Protected Friend MustOverride Async Function PlayLostMarkAnimation() As Task
        Protected Friend MustOverride Async Function PlayGotMarkAnimation() As Task
        Public Async Function LostMark(Mark As IMark) As Task Implements IPlayer.LostMark
            If Await ValueEffectProcNoSource(NameOf(IEffectHook.MarkLosingAsync), Me, Mark) Then
                PlayerAera.MilitaryOfficerCard.MarkAera.Remove(Mark)
                Await PlayLostMarkAnimation()
            End If
        End Function
        Public Async Function GotMark(Mark As IMark) As Task Implements IPlayer.GotMark
            If Await ValueEffectProcNoSource(NameOf(IEffectHook.MarkAddedAsync), Me, Mark) Then
                PlayerAera.MilitaryOfficerCard.MarkAera.Add(Mark)
                Await PlayGotMarkAnimation()
            End If
        End Function
        Public Async Sub ControlsIncrementHandCard(Player As IPlayer, Card As IHandCard) Implements IPlayer.ControlsIncrementHandCard
            Await GotHandCard(Card)
            Await Card.OnGotAsync(Player, Me)
        End Sub

        Public Async Sub ControlsRemoveHandCard(Player As IPlayer, Card As IHandCard) Implements IPlayer.ControlsRemoveHandCard
            Await Card.OnLostAsync(Player, Me)
            Await LostHandCard(Card)
        End Sub

        Public Async Sub ControlsIncrementMark(Player As IPlayer, Mark As IMark) Implements IPlayer.ControlsIncrementMark
            For Each bj In PlayerAera.MilitaryOfficerCard.MarkAera
                If bj.GetType.FullName = Mark.GetType.FullName Then
                    bj.Count += 1
                    For Each tj In bj.Abilities
                        Await tj.Increment(Player)
                    Next
                    Return
                End If
            Next
            Await GotMark(Mark)
            For Each tj In Mark.Abilities
                Await tj.Increment(Player)
            Next
        End Sub

        Public Async Sub ControlsDecrementMark(Player As IPlayer, Mark As IMark) Implements IPlayer.ControlsDecrementMark
            For Each bj In PlayerAera.MilitaryOfficerCard.MarkAera
                If bj.GetType.FullName = Mark.GetType.FullName Then
                    If bj.Count > 1 Then
                        bj.Count -= 1
                    Else
                        Await LostMark(bj)
                    End If
                    For Each tj In bj.Abilities
                        Await tj.Decrement(Player)
                    Next
                    Return
                End If
            Next
        End Sub

        Public Function HasCards(Category As Integer) As Boolean Implements IPlayer.HasCards
            Return HasCards((Iterator Function()
                                 For i = 0 To CInt(Math.Log(CardCategories.Truncate) / Math.Log(2)) - 1
                                     Dim v = (Category And 1 << i)
                                     If v <> 0 Then
                                     Yield CType(v, CardCategories)
                                 End If
                             Next
                         End Function).Invoke)
        End Function
    End Class

End Namespace
