''' <summary>
''' 典型的RPG存档文件中的漫游数据
''' </summary>
Public Class RPGSaveRoamingData
    Public Property GameVersion As Version
    Public Property SaveTime As Date
    Public Property ElapsedTime As Date
    Public Property Difficulty As Diffifulty
    Public Property Round As Integer
    Public Property PrimaryCharacters As New List(Of GameCharacterData)
    Public Property PlayerCollection As PlayerCollectionProperties
    Public Property Achievements As New List(Of Achievement)
    Public Property Checksum As Long
End Class