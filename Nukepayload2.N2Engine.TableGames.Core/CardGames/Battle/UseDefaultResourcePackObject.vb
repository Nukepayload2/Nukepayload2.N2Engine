Namespace Battle
    Public MustInherit Class UseDefaultResourcePackObject
        Public Property RouteKey As String = "/Resources/"
        Public MustOverride ReadOnly Property Uid As String
        Protected Iterator Function LoadSounds(Category As String, Optional SoundCount As Integer = 1) As IEnumerable(Of Stream)
            If SoundCount > 1 Then
                For i = 1 To SoundCount
                    Yield BattleSoundPlayer.Current.SoundStreamFromResourcePack("/" & Uid & "/" & Uid & Category & i)
                Next
            Else
                Yield BattleSoundPlayer.Current.SoundStreamFromResourcePack("/" & Uid & "/" & Uid & Category)
            End If
        End Function
        ''' <summary>
        ''' 加载在ResourceName文件夹中带<code>ResourceName</code>前缀的Image文件
        ''' </summary>
        ''' <param name="Name"></param>
        ''' <returns></returns>
        Protected Function LoadImage(Name As String) As Uri
            Return BattleBitmapLoader.Current.ImageFromResourcePack("/" & Uid & "/" & Uid & Name)
        End Function
        ''' 加载在ResourceName文件夹中的Image文件
        Protected Function LoadCustomImage(Name As String) As Uri
            Return BattleBitmapLoader.Current.ImageFromResourcePack("/" & Uid & "/" & Name)
        End Function
        Protected Function GetString(PropertyName As String) As String
            Dim str = BattleStringLoader.Cuttent.GetString(RouteKey + Uid & "_" & PropertyName)
            Return If(String.IsNullOrEmpty(str), "Missing:" & Uid & "_" & PropertyName, str)
        End Function
    End Class

End Namespace
