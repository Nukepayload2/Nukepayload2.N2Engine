Imports Nukepayload2.N2Engine.Core
Imports System.Reflection

Public Class Resources
    Public Property ResourceLoader As New ResourceLoader
    Sub New()
        ResourceLoader.AddRoute("CoreImages", GetType(Resources).GetTypeInfo.Assembly)
    End Sub
End Class