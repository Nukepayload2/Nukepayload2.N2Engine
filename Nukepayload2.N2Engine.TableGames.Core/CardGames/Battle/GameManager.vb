Imports System.Text

Namespace Battle
    Public Class GameManager
        Implements IGameCore
        Public Shared ReadOnly Property Current As IGameCore
        Public ReadOnly Property Round As Integer Implements IGameCore.Round
        Public ReadOnly Property ClientAera As IList(Of ICustomCommand) Implements IGameCore.ClientAera
        Public Property GameCoreStarted As Boolean Implements IGameCore.GameCoreStarted
        ''' <summary>
        ''' 没Out都在里面
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property Players As IList(Of IPlayer) Implements IGameCore.Players
        Public ReadOnly Property Window As IGameCoreWindow Implements IGameCore.Window
        Public ReadOnly Property MilitaryOfficerCampPicker As IMilitaryOfficerCampPicker Implements IGameCore.MilitaryOfficerCampPicker
        Sub New(Window As IGameCoreWindow,
            ClientAera As IList(Of ICustomCommand),
            Players As IList(Of IPlayer),
            MilitaryOfficerCampPicker As IMilitaryOfficerCampPicker,
            Settings As IBattleGameSettingsManager)
            Me.Window = Window
            Me.ClientAera = ClientAera
            Me.Players = New List(Of IPlayer)
            For Each pl In From p In Players Order By p.OrderId
                Me.Players.Add(pl)
            Next
            Me.MilitaryOfficerCampPicker = MilitaryOfficerCampPicker
            Round = 0
            GameCoreStarted = False
            _Current = Me
        End Sub
        Public Property LeadStateOwner As IPlayer Implements IGameCore.LeadStateOwner
        Public ReadOnly Property Log As New StringBuilder Implements IGameCore.Log

        Public Async Function LeadStateAsync(Player As IPlayer) As Task Implements IGameCore.LeadStateAsync
            LeadStateOwner = Player
            If Player.PlayerControl Then
                Await Player.HMLead()
            Else
                Await Player.AILead()
            End If
        End Function

        Public Async Function StartStageAsync(Player As IPlayer) As Task Implements IGameCore.StartStageAsync
            If Player.PlayerControl Then
                Await Player.HMStart()
            Else
                Await Player.AIStart()
            End If
        End Function

        Private Async Sub OnPlayerOut(SeqID As Integer)
            Dim pdeath = Aggregate p In From pl In Players Where pl.OrderId = SeqID Into First
            Log.AppendFormat("小组{1}的Player{0}Out：", pdeath.ControllerInformation.PlayerName, pdeath.ControllerInformation.PlayerGroup)
            If (Aggregate co In From pl In Players Where pl.Alive Into Count) < 2 Then
                Dim reason As GameExitReason
                If pdeath.PlayerControl Then
                    reason = GameExitReason.Defeated
                Else
                    reason = GameExitReason.Won
                End If
                Await GameCoreEndedAsync(reason)
            End If
        End Sub
        Public Async Function GameCoreStartAsync() As Task Implements IGameCore.GameCoreStartAsync
            AddHandler PlayerOut, AddressOf OnPlayerOut
            For Each p In Players
                For Each tj In p.MilitaryOfficer.DisposeResourcesAbilities
                    Await tj.RoundStartingAsync(p)
                Next
            Next
            Await StartStageAsync(Players.First)
        End Function

        Public Async Function GameCoreEndedAsync(Reasom As GameExitReason) As Task Implements IGameCore.GameCoreEndedAsync
            RemoveHandler PlayerOut, AddressOf OnPlayerOut
            For Each p In Players
                For Each tj In p.MilitaryOfficer.DisposeResourcesAbilities
                    Await tj.RoundEndingAsync(p)
                Next
            Next
            Select Case Reasom
                Case GameExitReason.Won
                    Await Window.ShowPlayerWinScreen()
                Case GameExitReason.Defeated
                    Await Window.ShowPlayerDefeatedScreen()
                Case GameExitReason.Exited
                    Await Window.BackToTitleScreen()
                Case Else
                    Await Window.ExitApplication()
            End Select
        End Function

        Public Async Function PlayerMaydayAsync(Sender As IPlayer, e As PlayerDeathJudgementEventArgs) As Task Implements IGameCore.PlayerMaydayAsync
            If e.UnableToSave Then
                Sender.Alive = False
            Else
                If e.NeedCure Then
                    For Each p In From pl In Players Where pl.Alive
                        Await p.ProcessMayday(Sender, e)
                    Next
                    If Sender.Health < 1 Then
                        Sender.Alive = False
                        Players.Remove(Sender)
                    End If
                End If
            End If
        End Function

        Public Async Function EndedStageAsync(Player As IPlayer) As Task Implements IGameCore.EndedStageAsync
            If Player.PlayerControl Then
                Await Player.HMEnded()
            Else
                Await Player.AIEnded()
            End If
            Await StartStageAsync(NextPlayer)
        End Function

        Public Function NextPlayer() As IPlayer Implements IGameCore.NextPlayer
            Dim p = From pl In Players Where pl.Alive Order By pl.OrderId
            If p.Count < 2 Then
                Throw New InvalidOperationException("GameCore应该Ended，但Is仍在GetNextPlayer")
            Else
                If p.Last Is LeadStateOwner Then
                    Return p.First
                Else
                    Return Aggregate a In From n In p Where n.OrderId > LeadStateOwner.OrderId Into Min
                End If
            End If
        End Function
    End Class

End Namespace
