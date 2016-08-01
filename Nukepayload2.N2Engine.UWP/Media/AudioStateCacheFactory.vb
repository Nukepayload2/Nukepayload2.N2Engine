Imports Windows.Media.Audio
Namespace Media
    Public Class AudioStateCacheFactory

        Public Shared Function CreateCache(graph As AudioGraph, deviceOutputNode As AudioDeviceOutputNode, maxSubmixCount As Integer, soundMixListFullOperation As SoundMixListFullOperation) As Cache(Of AudioState)
            Dim cache As Cache(Of AudioState)
            Dim loader = Sub(ByRef item As AudioState)
                             If item Is Nothing Then item = New AudioState
                             item.SubmixNode = graph.CreateSubmixNode
                             item.SubmixNode.AddOutgoingConnection(deviceOutputNode)
                             item.SubmixNode.OutgoingGain = 1
                         End Sub
            Dim timePredic = Function(item As AudioState) item.FileInputNode.Position >= item.FileInputNode.Duration
            Select Case soundMixListFullOperation
                Case SoundMixListFullOperation.TryFirstFit
                    cache = New FirstFitCache(Of AudioState)(loader, timePredic, maxSubmixCount, False)
                Case SoundMixListFullOperation.FirstFit
                    cache = New FirstFitCache(Of AudioState)(loader, timePredic, maxSubmixCount, True)
                Case SoundMixListFullOperation.PreemptiveLRU
                    cache = New LRUCache(Of AudioState)(loader, maxSubmixCount)
                Case SoundMixListFullOperation.PreemptiveFIFO
                    cache = New FifoCache(Of AudioState)(loader, maxSubmixCount)
                Case SoundMixListFullOperation.PreemptiveSRT
                    cache = New SRTCache(Of AudioState)(loader, maxSubmixCount,
                             Function(item As AudioState) (If(item.FileInputNode?.Duration, TimeSpan.Zero) - If(item.FileInputNode?.Position, TimeSpan.Zero)))
                Case Else
                    Throw New ArgumentException(NameOf(soundMixListFullOperation))
            End Select
            Return cache
        End Function
    End Class

End Namespace
