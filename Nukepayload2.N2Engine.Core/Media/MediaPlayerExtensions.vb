Public Module MediaPlayerExtensions
    <Extension>
    Public Async Function PlayNextAsync(player As IMusicPlayer) As Task
        If ((player.Sources?.Any()).GetValueOrDefault()) Then
            Dim ubound = player.Sources.Count - 1
            If (player.PlayingIndex < ubound) Then
                Await player.SetPlayingIndexAsync(player.PlayingIndex + 1)
            Else
                Await player.SetPlayingIndexAsync(ubound)
            End If
        End If
    End Function
End Module