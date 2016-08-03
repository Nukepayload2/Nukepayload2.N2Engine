''' <summary>
''' 典型的RPG的玩家数据
''' </summary>
Public Class GameCharacterData
    ''' <summary>
    ''' 基础属性。通常是跑地图的时候用。
    ''' </summary>
    Public Property BodyProperties As BodyProperties
    ''' <summary>
    ''' 战斗属性
    ''' </summary>
    Public Property BattleProperties As BattleProperties
    ''' <summary>
    ''' 个人信息
    ''' </summary>
    Public Property PersonalInformation As CharacterInformation
    ''' <summary>
    ''' 身上的小仓库
    ''' </summary>
    Public Property Repository As CharacterRepository
    ''' <summary>
    ''' 随从包括跟随的NPC和怪物等。不能是主要人物。
    ''' </summary>
    Public Property Retinues As New List(Of GameCharacterData)
    ''' <summary>
    ''' 附加的 Buff 和 Debuff
    ''' </summary>
    Public Property BuffDebuff As New List(Of BuffDebuffStatus)
    ''' <summary>
    ''' 每章节的任务
    ''' </summary>
    Public Property Missions As New List(Of ChapterData)
    Public Property Skills As New List(Of SkillStatus)
    Public Property Money As Integer
End Class