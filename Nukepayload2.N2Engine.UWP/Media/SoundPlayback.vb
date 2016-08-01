Option Strict On
Imports Windows.Media.Effects
Imports Windows.Storage
Namespace Media
    ''' <summary>
    ''' 一个针对<see cref="MusicPlayback"/>的扩充，使其可以混音播放音效。
    ''' </summary>
    Public Class SoundPlayback
        ''' <summary>
        ''' 使用一个<see cref="MusicPlayback"/>创建声音回放器。
        ''' </summary>
        ''' <param name="basePlayback">用于共享设备资源的音乐回放器</param>
        ''' <param name="maxSubMixCount">最多多少声音同时播放</param>
        Sub New(basePlayback As MusicPlayback, Optional maxSubmixCount As Integer = 8,
                Optional soundMixListFullOperation As SoundMixListFullOperation = SoundMixListFullOperation.PreemptiveSRT)
            Me.BasePlayback = basePlayback
            If maxSubmixCount < 1 Then
                Throw New ArgumentOutOfRangeException(NameOf(maxSubmixCount))
            End If
            Me.MaxSubmixCount = maxSubmixCount
            SoundCache = AudioStateCacheFactory.CreateCache(basePlayback.Graph, basePlayback.DeviceOutput, maxSubmixCount, soundMixListFullOperation)
        End Sub

        Public ReadOnly Property BasePlayback As MusicPlayback
        Public ReadOnly Property MaxSubmixCount As Integer
        Public ReadOnly Property SoundCache As Cache(Of AudioState)

        ''' <summary>
        ''' 异步加载一个声音文件，然后播放。有时声音正好在缓存中，此时不必重新加载。
        ''' </summary>
        ''' <param name="file"></param>
        ''' <returns></returns>
        Public Async Function PlayAsync(file As StorageFile, outgoingGain As Double, ParamArray effects() As IAudioEffectDefinition) As Task
            Await SoundCache.PutAsync(file, Function(item) If(item.FileInputNode?.SourceFile.IsEqual(file), False),
                Sub(hitSound)
                    With hitSound.FileInputNode
                        .Seek(TimeSpan.Zero)
                        .Start()
                    End With
                    hitSound.SubmixNode.Start()
                End Sub,
                Async Function(swapOutSound)
                    swapOutSound.FileInputNode?.Dispose()
                    Dim newNodeResult = Await BasePlayback.Graph.CreateFileInputNodeAsync(file)
                    If newNodeResult.Status <> Windows.Media.Audio.AudioFileNodeCreationStatus.Success Then
                        Throw New IOException($"读取文件错误: {newNodeResult.Status}")
                    End If
                    Dim fileNode = newNodeResult.FileInputNode
                    fileNode.AddOutgoingConnection(swapOutSound.SubmixNode, outgoingGain)
                    For Each effect In effects
                        fileNode.EffectDefinitions.Add(effect)
                    Next
                    swapOutSound.FileInputNode = fileNode
                    Return swapOutSound
                End Function)
        End Function
    End Class
End Namespace
