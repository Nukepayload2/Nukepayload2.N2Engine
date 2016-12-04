Namespace Media
    Public Module MediaPlayerExtensions
        <Extension>
        Public Async Function PlayNextAsync(player As IMusicPlayer) As Task
            If ((player.Sources?.Any()).GetValueOrDefault()) Then
                If (player.PlayingIndex < player.Sources.Count - 1) Then
                    Await player.SetPlayingIndexAsync(player.PlayingIndex + 1)
                Else
                    Await player.SetPlayingIndexAsync(0)
                End If
            End If
        End Function
    End Module
End Namespace