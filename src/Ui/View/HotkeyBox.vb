''' <summary>
''' 用于输入快捷键的 TextBox
''' </summary>
Public Class HotkeyBox
    Inherits TextBox

#Region "属性"

    Private _currentKey As Keys = Keys.None

    ''' <summary>
    ''' 当前的快捷键
    ''' </summary>
    Public Property CurrentKey As Keys
        Get
            Return _currentKey
        End Get
        Set
            _currentKey = value
            Dim e As New KeyEventArgs(value)
            ShowHotKeyValue(e.Modifiers, e.KeyCode) ' 显示
        End Set
    End Property

#End Region

#Region "方法"

    ''' <summary>
    ''' 显示快捷键
    ''' </summary>
    Private Sub ShowHotKeyValue(modifiers As Keys, keyCode As Keys)
        Dim value = 0
        Dim str = ""
        If modifiers <> Keys.None Then
            Select Case modifiers
                Case Keys.Control
                    str += "Ctrl + "
                Case Keys.Alt
                    str += "Alt + "
                Case Keys.Shift
                    str += "Shift + "
                Case Keys.Control Or Keys.Alt
                    str += "Ctrl + Alt + "
                Case Keys.Control Or Keys.Shift
                    str += "Ctrl + Shift + "
                Case Keys.Alt Or Keys.Shift
                    str += "Alt + Shift + "
                Case Keys.Control Or Keys.Alt Or Keys.Shift
                    str += "Ctrl + Alt + Shift + "
            End Select
            value = CInt(modifiers)
            If keyCode <> Keys.None And keyCode <> Keys.ControlKey And keyCode <> Keys.Menu And keyCode <> Keys.ShiftKey Then
                str += KeyCodeToString(keyCode)
                value += CInt(keyCode)
            End If
        Else
            If keyCode = Keys.Delete Or keyCode = Keys.Back Then
                str = ""
                value = -1
            ElseIf keyCode <> Keys.None Then
                str += KeyCodeToString(keyCode)
                value += CInt(keyCode)
            End If
        End If
        value = If(value = 0, -1, value)

        If CurrentKey <> value Then
            CurrentKey = value
        End If
        Text = str
        SelectionStart = Text.Length
    End Sub

    ''' <summary>
    ''' keyCode 转字符串
    ''' </summary>
    Private Function KeyCodeToString(keyCode As Keys) As String
        If KeyCode >= Keys.D0 And KeyCode <= Keys.D9 Then
            Return KeyCode.ToString().Remove(0, 1)
        ElseIf KeyCode >= Keys.NumPad0 And KeyCode <= Keys.NumPad9 Then
            Return KeyCode.ToString().Replace("Pad", "")
        Else
            Return KeyCode.ToString()
        End If
    End Function

    ''' <summary>
    ''' 检查控件状态，并修改显示，比如置空
    ''' </summary>
    Private Sub CheckHotkey()
        If Text.Trim().EndsWith("+") Or String.IsNullOrWhiteSpace(Text) Then
            CurrentKey = -1
            Text = ""
            SelectionStart = 0
        End If
    End Sub

#End Region

#Region "重载事件"

    Protected Overrides Sub OnKeyDown(e As KeyEventArgs)
        MyBase.OnKeyDown(e)
        e.SuppressKeyPress = False
        e.Handled = True
        ShowHotKeyValue(e.Modifiers, e.KeyCode)
    End Sub

    Protected Overrides Sub OnKeyPress(e As KeyPressEventArgs)
        MyBase.OnKeyPress(e)
        e.Handled = True
    End Sub

    Protected Overrides Sub OnKeyUp(e As KeyEventArgs)
        MyBase.OnKeyUp(e)
        CheckHotkey()
    End Sub

    Protected Overrides Sub OnLostFocus(e As EventArgs)
        MyBase.OnLostFocus(e)
        CheckHotkey()
    End Sub

#End Region
End Class
