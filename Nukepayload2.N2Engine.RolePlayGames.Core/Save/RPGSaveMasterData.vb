''' <summary>
''' 典型的RPG存档文件中的主数据。这种文件不大，通常适合漫游。
''' </summary>
Public Class RPGSaveMasterData
    Inherits RPGSaveBase

    ''' <summary>
    ''' 已经带来持久影响的角色
    ''' </summary>
    Public Property PermanentAffectedCharacters As New List(Of CharacterPermanentInfluence)
    ''' <summary>
    ''' 收藏
    ''' </summary>
    Public Property PlayerCollection As PlayerCollectionProperties
    ''' <summary>
    ''' 成就
    ''' </summary>
    Public Property Achievements As New List(Of Achievement)
End Class