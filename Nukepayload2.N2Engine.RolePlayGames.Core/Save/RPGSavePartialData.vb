Public Class RPGSavePartialData
    Inherits RPGSaveBase
    ''' <summary>
    ''' 变更过的 人物/怪物 的数据
    ''' </summary>
    ''' <returns></returns>
    Public Property Characters As New List(Of GameCharacterData)
    ''' <summary>
    ''' 每章节的任务
    ''' </summary>
    Public Property Missions As New List(Of ChapterData)
End Class
