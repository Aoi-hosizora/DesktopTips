Public Class HotkeyBox
    Inherits TextBox

#Region "属性"

    Private _currentKey As Keys = Keys.None

    Public Property CurrentKey As Keys
        Get
            Return _currentKey
        End Get
        Set
            _currentKey = value
            Dim e As New KeyEventArgs(value)
            ShowHotKeyValue(e.Modifiers, e.KeyCode)
        End Set
    End Property

#End Region

#Region "方法"

    Private Sub ShowHotKeyValue(modifiers As Keys, keyCode As Keys)
        Dim hotKeyValue% = 0
        Dim hotKeyString$ = ""
        If Modifiers <> Keys.None Then
            Select Case Modifiers
                Case Keys.Control
                    hotKeyString += "Ctrl + "
                Case Keys.Alt
                    hotKeyString += "Alt + "
                Case Keys.Shift
                    hotKeyString += "Shift + "
                Case Keys.Control Or Keys.Alt
                    hotKeyString += "Ctrl + Alt + "
                Case Keys.Control Or Keys.Shift
                    hotKeyString += "Ctrl + Shift + "
                Case Keys.Alt Or Keys.Shift
                    hotKeyString += "Alt + Shift + "
                Case Keys.Control Or Keys.Alt Or Keys.Shift
                    hotKeyString += "Ctrl + Alt + Shift + "
            End Select
            hotKeyValue = CInt(Modifiers)
            If KeyCode <> Keys.None And KeyCode <> Keys.ControlKey And KeyCode <> Keys.Menu And KeyCode <> Keys.ShiftKey Then
                hotKeyString += KeyCodeToString(KeyCode)
                hotKeyValue += CInt(KeyCode)
            End If
        Else
            If KeyCode = Keys.Delete Or KeyCode = Keys.Back Then
                hotKeyString = ""
                hotKeyValue = -1
            ElseIf KeyCode <> Keys.None Then
                hotKeyString += KeyCodeToString(KeyCode)
                hotKeyValue += CInt(KeyCode)
            End If
        End If
        hotKeyValue = If(hotKeyValue = 0, -1, hotKeyValue)

        If CurrentKey <> hotKeyValue Then
            CurrentKey = hotKeyValue
        End If
        Text = hotKeyString
        SelectionStart = Text.Length
    End Sub

    Private Shared Function KeyCodeToString(keyCode As Keys) As String
        If KeyCode >= Keys.D0 And KeyCode <= Keys.D9 Then
            Return KeyCode.ToString().Remove(0, 1)
        ElseIf KeyCode >= Keys.NumPad0 And KeyCode <= Keys.NumPad9 Then
            Return KeyCode.ToString().Replace("Pad", "")
        Else
            Return KeyCode.ToString()
        End If
    End Function

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
