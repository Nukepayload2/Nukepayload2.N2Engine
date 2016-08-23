
Namespace Battle
    Public MustInherit Class BattleGameSettingsManager
        Implements IBattleGameSettingsManager
        Public Shared ReadOnly Property Current As IBattleGameSettingsManager
        Public MustOverride Property MultiEngineer As Boolean Implements IBattleGameSettingsManager.MultiEngineer
        Public MustOverride Property AttackNearAlly As Boolean Implements IBattleGameSettingsManager.AttackNearAlly
        Public MustOverride Property BadWeather As Boolean Implements IBattleGameSettingsManager.BadWeather
        Public MustOverride Property AllowModCards As Boolean Implements IBattleGameSettingsManager.AllowModCards
        Public MustOverride Property IsSuperWeaponEnabled As Boolean Implements IBattleGameSettingsManager.IsSuperWeaponEnabled
        Public MustOverride Property IsCrateEnabled As Boolean Implements IBattleGameSettingsManager.IsCrateEnabled
        Public MustOverride Property TechLevelLimit As Integer Implements IBattleGameSettingsManager.TechLevelLimit
        Public MustOverride Property CardsPerRound As Integer Implements IBattleGameSettingsManager.CardsPerRound
        Public MustOverride Property HumanPlayerTimeout As TimeSpan Implements IBattleGameSettingsManager.HumanPlayerTimeout

        Const GeneralSetting As String = "General"
        Protected MustOverride Sub SetGeneralValue(Of T)(Value As T, Key As String)
        Protected MustOverride Function GetGeneralValue(Of T As New)(Key As String) As T
        Protected MustOverride Function GetGeneralValue(Key As String) As String

        Protected MustOverride Async Function SaveSettings() As Task Implements IBattleGameSettingsManager.SaveSettings
        Protected MustOverride Async Function LoadSettings() As Task Implements IBattleGameSettingsManager.LoadSettings

    End Class

End Namespace
