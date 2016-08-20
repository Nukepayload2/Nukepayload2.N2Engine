Public Class RPGSaveBase
    Public Property GameVersion As Version
    Public Property SaveTime As Date
    Public Property ElapsedTime As Date
    Public Property Difficulty As Diffifulty
    ''' <summary>
    ''' 这是第几次通关了
    ''' </summary>
    Public Property Round As Integer
    ''' <summary>
    ''' 用于验证游戏存档的完整性。如果游戏存档文件损坏，或者被简单地编辑，都会引发验证码的不一致。
    ''' </summary>
    Public Property Checksum As Long
End Class
