Imports Nukepayload2.N2Engine.Core

Public Class CharacterInformation
    Inherits GameResourceModelBase
    Public Property NickName$
    ''' <summary>
    ''' 对每个<see cref="ResourceId"/>指向的人的好感
    ''' </summary>
    Public Property Favor As Dictionary(Of String, Integer)
    ''' <summary>
    ''' 生日
    ''' </summary>
    Public Property Birthday As Date
    ''' <summary>
    ''' 性别
    ''' </summary>
    Public Property IsMale As Boolean
    ''' <summary>
    ''' 年龄
    ''' </summary>
    Public Property Age As Date
End Class