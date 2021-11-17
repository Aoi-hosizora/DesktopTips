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
    Public Shared Function TrimForEllipsis(text As String, font As Font, maxSize As Integer) As String
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
    ''' 文本类型
    ''' </summary>
    Public Enum TextType
        Plain = 0
        Markdown
        HTML
    End Enum

    ''' <summary>
    ''' 下标转文本类型
    ''' </summary>
    Public Shared Function IndexToTextType(i As Integer) As TextType
        Select Case i
            Case 0
                Return TextType.Plain
            Case 1
                Return TextType.Markdown
            Case Else
                Return TextType.HTML
        End Select
    End Function

    ''' <summary>
    ''' 文本类型转下标
    ''' </summary>
    Public Shared Function TextTypeToIndex(t As TextType) As Integer
        Select Case t
            Case TextType.Plain
                Return 0
            Case TextType.Markdown
                Return 1
            Case Else
                Return 2
        End Select
    End Function

    ''' <summary>
    ''' 转义字符串为 XML 兼容
    ''' </summary>
    Public Shared Function EscapeForXML(s As String) As String
        Return s.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;")
    End Function

    ''' <summary>
    ''' Markdown 文本类型转为 Markup 样式
    ''' </summary>
    Public Shared Function Markdown2Markup(s As String) As String
        s = s.Replace("\\", "＼").Replace("\*", "＊").Replace("\_", "＿").Replace("\~", "～").Replace("\=", "＝")
        s = s.Replace("\+", "＋").Replace("\-", "－").Replace("\`", "｀").Replace("\#", "＃")
        s = New Regex("(?<!\\)\*\*(.+?)\*\*").Replace(s, "<b>$1</b>")                           ' **...**
        s = New Regex("(?<!\\)\*(.+?)\*").Replace(s, "<i>$1</i>")                               ' *...*
        s = New Regex("(?<!\\)__(.+?)__").Replace(s, "<u>$1</u>")                               ' __...__
        s = New Regex("(?<!\\)~~(.+?)~~").Replace(s, "<s>$1</s>")                               ' ~~...~~
        s = New Regex("(?<!\\)==(.+?)==").Replace(s, "<font color=""red"">$1</font>")           ' ==...==
        s = New Regex("(?<!\\)=:(.+?)=(.+?)==").Replace(s, "<font color=""$1"">$2</font>")      ' =:x=...==
        s = New Regex("(?<=^|\s|>)\+ (.+)").Replace(s, "•　$1")                                 ' + x
        s = New Regex("(?<=^|\s|>)\- (.+)").Replace(s, "◦　$1")                                 ' - x
        s = New Regex("(?<=^|\s|>)&gt; (.+)\n?").Replace(s, "<font color=""#D0D7DE"">▍ $1</font><br/>") ' > x
        s = New Regex("(?<!\\)```(?:\r\n)*([\s\S]+?)(?:\r\n)*```", RegexOptions.Multiline).Replace(s, "<font face=""consolas"">$1</font>")
        s = New Regex("(?<!\\)`(.+?)`").Replace(s, "<font face=""consolas"">$1</font>")

        s = New Regex("(?<=^|\n|>)###### (.+)\n?").Replace(s, "<h6>$1</h6>")
        s = New Regex("(?<=^|\n|>)##### (.+)\n?").Replace(s, "<h5>$1</h5>")
        s = New Regex("(?<=^|\n|>)#### (.+)\n?").Replace(s, "<h4>$1</h4>")
        s = New Regex("(?<=^|\n|>)### (.+)\n?").Replace(s, "<h3>$1</h3>")
        s = New Regex("(?<=^|\n|>)## (.+)\n?").Replace(s, "<h2>$1</h2>")
        s = New Regex("(?<=^|\n|>)# (.+)\n?").Replace(s, "<h1>$1</h1>")

        s = s.Replace("＼", "\").Replace("＊", "*").Replace("＿", "_").Replace("～", "~").Replace("＝", "=")
        s = s.Replace("＋", "+").Replace("－", "-").Replace("｀", "`").Replace("＃", "#")
        s = s.Replace(vbNewLine, "<br/>")
        Return s
    End Function

    ''' <summary>
    ''' HTML 文本类型转为 Markup 样式
    ''' </summary>
    Public Shared Function SugarText2Markup(s As String) As String
        s = New Regex("\n").Replace(s, "<br/>")
        s = New Regex("<l w([0-9]+)>(.*?)</l>").Replace(s, "<span width=""$1"">$2</span> ")
        Return s
    End Function

    ''' <summary>
    ''' 格式化处理文本
    ''' </summary>
    Public Shared Function FormatText(s As String) As String
        s = s.Trim()
        's = New Regex("\r\n[ \t]+").Replace(s, vbNewLine)
        s = New Regex("[ \t]+\r\n").Replace(s, vbNewLine)
        s = New Regex("\r\n(\r\n)+").Replace(s, vbNewLine + vbNewLine)
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
