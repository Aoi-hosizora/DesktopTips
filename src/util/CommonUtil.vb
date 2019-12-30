Imports Microsoft.Win32

Public Class CommonUtil

    ''' <summary>
    ''' 获取 Keys 的 Modifiers 部分
    ''' </summary>
    Public Shared Function GetModifiersFromKey(ByVal key As Keys) As Keys
        Dim e As New KeyEventArgs(key)
        Return e.Modifiers
    End Function

    ''' <summary>
    ''' 获取 Keys 的 KeyCode 部分
    ''' </summary>
    Public Shared Function GetKeyCodeFromKey(ByVal key As Keys) As Keys
        Dim e As New KeyEventArgs(key)
        Return e.KeyCode
    End Function

    ''' <summary>
    ''' 从 .NET Keys Modifiers 转 Window Modifiers
    ''' </summary>
    Public Shared Function GetNativeModifiers(ByVal modifiers As Keys) As NativeMethod.KeyModifiers
        Select Case modifiers
            Case Keys.Shift ' 1 << 16
                Return NativeMethod.KeyModifiers.MOD_SHIFT ' 1 << 2
            Case Keys.Control ' 1 << 17
                Return NativeMethod.KeyModifiers.MOD_CONTROL ' 1 << 1
            Case Keys.Alt ' 1 << 18
                Return NativeMethod.KeyModifiers.MOD_ALT ' 1 << 0
            Case Keys.Control Or Keys.Alt
                Return NativeMethod.KeyModifiers.MOD_CONTROL Or NativeMethod.KeyModifiers.MOD_ALT
            Case Keys.Control Or Keys.Shift
                Return NativeMethod.KeyModifiers.MOD_CONTROL Or NativeMethod.KeyModifiers.MOD_SHIFT
            Case Keys.Shift Or Keys.Alt
                Return NativeMethod.KeyModifiers.MOD_SHIFT Or NativeMethod.KeyModifiers.MOD_ALT
            Case Keys.Control Or Keys.Alt Or Keys.Shift
                Return NativeMethod.KeyModifiers.MOD_CONTROL Or NativeMethod.KeyModifiers.MOD_ALT Or NativeMethod.KeyModifiers.MOD_SHIFT
            Case Else
                Return 0
        End Select
    End Function

    ''' <summary>
    ''' 获取默认浏览器
    ''' </summary>
    Public Shared Function GetDefaultWebBrowserFilePath() As String
        ' コンピューター\HKEY_CURRENT_USER\Software\Microsoft\Windows\Shell\Associations\UrlAssociations\http\UserChoice
        Dim UserChoiceKey As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\Shell\Associations\UrlAssociations\http\UserChoice")
        If UserChoiceKey Is Nothing Then UserChoiceKey = Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\Shell\Associations\UrlAssociations\https\UserChoice")
        If UserChoiceKey Is Nothing Then Return ""
        Dim progId$ = UserChoiceKey.GetValue("ProgId").ToString()
        UserChoiceKey.Close()

        Dim RootCommandKey As RegistryKey = Registry.ClassesRoot.OpenSubKey(progId + "\shell\open\command")
        If RootCommandKey Is Nothing Then Return ""
        Dim OpenWith$ = RootCommandKey.GetValue("").ToString()
        RootCommandKey.Close()

        ' コンピューター\HKEY_CLASSES_ROOT\ChromeHTML\shell\open\command
        ' "C:\Program Files (x86)\Google\Chrome\Application\chrome.exe" -- "%1"
        Dim OpenWithParam As String() = OpenWith.Split(New String() {""""}, StringSplitOptions.RemoveEmptyEntries)
        If OpenWithParam.Count = 0 Then Return ""
        Return OpenWithParam(0)
    End Function

    ''' <summary>
    ''' 在浏览器 (新窗口 if Chrome) 打开所有页面
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub OpenWebsInDefaultBrowser(ByVal links As List(Of String))
        Dim DefaultBrowserPath$ = GetDefaultWebBrowserFilePath()
        If DefaultBrowserPath.ToLower().Contains("chrome") Then
            Dim p As New Process()
            p.StartInfo.FileName = DefaultBrowserPath
            p.StartInfo.Arguments = "--new-window " & String.Join(" ", links)
            p.StartInfo.WindowStyle = ProcessWindowStyle.Minimized
            p.Start()
        Else
            For Each link$ In links
                Process.Start(link)
            Next
        End If
    End Sub

End Class
