Imports Nukepayload2.N2Engine.Shell.Utilities
Imports Windows.Storage
Imports Windows.Storage.AccessCache

Namespace Services

    Public Class RecentAccessListService

        Public Shared Async Function GetRecentFilesOrderByDateAccessedDescendingAsync() As Task(Of IEnumerable(Of StorageFile))
            Dim acclst = StorageApplicationPermissions.MostRecentlyUsedList
            Dim files As New List(Of StorageFile)
            For Each md In acclst.Entries
                files.Add(Await acclst.GetFileAsync(md.Token))
            Next
            Dim recentWithAccess = From f In Await Async Function()
                                                       Dim recentAccList As New List(Of (RecentFile As StorageFile, LastAccess As DateTimeOffset))
                                                       For Each recFile In files.Distinct(New CompareStorageFile)
                                                           Const DateAccessed = "System.DateAccessed"
                                                           Dim props = Await recFile.Properties.RetrievePropertiesAsync({DateAccessed})
                                                           recentAccList.Add((recFile, props(DateAccessed)))
                                                       Next
                                                       Return recentAccList
                                                   End Function()
                                   Order By f.LastAccess Descending Select f.RecentFile
            Return recentWithAccess
        End Function
    End Class

End Namespace