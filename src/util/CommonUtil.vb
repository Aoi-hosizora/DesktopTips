Imports Microsoft.Win32
Imports System.Text.RegularExpressions

Public Class CommonUtil
    ''' <summary>
    ''' 绘制颜色填充的正方形 Bitmap
    ''' </summary>
    Public Shared Function DrawColoredSquare(size As Integer, rect As Rectangle, color As Color) As Bitmap
        Dim bmp As New Bitmap(size, size)
        Dim g As Graphics = Graphics.FromImage(bmp)
        Using brush As New SolidBrush(color)
            g.FillRectangle(brush, rect)
        End Using
        Return bmp
    End Function

    ''' <summary>
    ''' Trim 字符串用于添加末尾省略号
    ''' </summary>
    Public Shared Function TrimForEllipsis(text As String, font As Font, maxSize As Integer)
        Dim g = (New Label()).CreateGraphics()
        Dim trimmed = text
        Dim currentSize = CInt(g.MeasureString(trimmed, font).Width)
        Dim ratio = maxSize / currentSize
        Dim cnt = 0 ' 防止 while 死循环
        While ratio < 1.0
            cnt += 1
            If cnt > 10 Then Exit While
            trimmed = trimmed.Substring(0, CInt(trimmed.Length * ratio) - 3) & "..."
            currentSize = CInt(g.MeasureString(trimmed, font).Width)
            ratio = maxSize / currentSize
        End While
        Return trimmed
    End Function

    ''' <summary>
    ''' 转义字符串为 XML 兼容
    ''' </summary>
    Public Shared Function EscapeForXML(s As String) As String
        Return s.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;")
    End Function

    ''' <summary>
    ''' Markdown 样式转为 Markup 样式
    ''' </summary>
    Public Shared Function Markdown2Markup(s As String) As String
        s = s.Replace("\\", "＼")
        s = New Regex("(?<!\\)\*\*([^\n]+?)\*\*").Replace(s, "<b>$1</b>")
        s = New Regex("(?<!\\)\*([^\n]+?)\*").Replace(s, "<i>$1</i>").Replace("\*", "*")
        s = New Regex("(?<!\\)_([^\n]+?)_").Replace(s, "<u>$1</u>").Replace("\_", "_")
        s = New Regex("(?<!\\)~~([^\n]+?)~~").Replace(s, "<s>$1</s>").Replace("\~", "~")
        s = s.Replace("＼", "\").Replace(vbNewLine, "<br/>")
        Return s
    End Function

    ''' <summary>
    ''' 获取 Keys 的 Modifiers 部分
    ''' </summary>
    Public Shared Function GetModifiersFromKey(key As Keys) As Keys
        Dim e As New KeyEventArgs(key)
        Return e.Modifiers
    End Function

    ''' <summary>
    ''' 获取 Keys 的 KeyCode 部分
    ''' </summary>
    Public Shared Function GetKeyCodeFromKey(key As Keys) As Keys
        Dim e As New KeyEventArgs(key)
        Return e.KeyCode
    End Function

    ''' <summary>
    ''' 从 .NET Keys Modifiers 转 Window Modifiers
    ''' </summary>
    Public Shared Function GetNativeModifiers(modifiers As Keys) As NativeMethod.KeyModifiers
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
    Public Shared Function GetDefaultBrowserPath() As String
        ' コンピューター\HKEY_CURRENT_USER\Software\Microsoft\Windows\Shell\Associations\UrlAssociations\http\UserChoice
        Dim userChoiceKey As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\Shell\Associations\UrlAssociations\http\UserChoice")
        If userChoiceKey Is Nothing Then userChoiceKey = Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\Shell\Associations\UrlAssociations\https\UserChoice")
        If userChoiceKey Is Nothing Then Return ""
        Dim progId As String = userChoiceKey.GetValue("ProgId").ToString()
        userChoiceKey.Close()

        Dim commandKey As RegistryKey = Registry.ClassesRoot.OpenSubKey(progId + "\shell\open\command")
        If commandKey Is Nothing Then Return ""
        Dim openWith As String = commandKey.GetValue("").ToString()
        commandKey.Close()

        ' コンピューター\HKEY_CLASSES_ROOT\ChromeHTML\shell\open\command
        ' "C:\Program Files (x86)\Google\Chrome\Application\chrome.exe" -- "%1"
        Dim openWithParam As String() = openWith.Split(New String() {""""}, StringSplitOptions.RemoveEmptyEntries)
        If openWithParam.Count = 0 Then
            Return ""
        End If
        Return openWithParam(0)
    End Function
End Class
