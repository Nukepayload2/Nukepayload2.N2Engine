Partial Public Structure MonoAPIContract
    '对于条件编译的说明：
#If WINDOWS_PHONE_APP Then
    '编写WP的代码
#ElseIf WINDOWS_DESKTOP Then
    '编写Win32的代码
#ElseIf ANDROID_APP Then
    '编写安卓代码
#ElseIf iOS_APP Then
    '编写苹果代码
#ElseIf WINDOWS_UWP Then
#If MONO Then
    '编写Mono UWP代码
#Else
    '编写Win2D UWP代码
#End If
#End If

#If OPENGL Then
    '编写OpenGL代码
#Else
    '编写DX代码
#End If
End Structure