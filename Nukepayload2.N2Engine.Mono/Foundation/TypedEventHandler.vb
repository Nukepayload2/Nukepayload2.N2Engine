#If Not (WINDOWS_UWP OrElse WINDOWS_PHONE_APP) Then
Public Delegate Sub TypedEventHandler(Of TSender, TResult)(
  sender As TSender,
  args As TResult
)
#End If