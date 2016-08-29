Imports System.Reflection
Imports Nukepayload2.N2Engine.Resources

Public Class Resources
    Public Property ResourceLoader As New ResourceLoader
    Sub New()
        ResourceLoader.AddRoute("CoreImages", GetType(Resources).GetTypeInfo.Assembly)
    End Sub
End Class