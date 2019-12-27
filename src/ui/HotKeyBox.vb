Imports System.ComponentModel

Public Class HotKeyBox
    Inherits TextBox

    Private _CurrentKey As Keys = Keys.None

    Public Property CurrentKey As Keys
        Get
            Return _CurrentKey
        End Get
        Set(value As Keys)
            _CurrentKey = value
            Dim e As New KeyEventArgs(value)
            ShowHotKeyValue(e.Modifiers, e.KeyCode)
        End Set
    End Property

    Public Shared Function GetKeyModifiers(keys As Keys) As Keys
        Dim e As New KeyEventArgs(keys)
        Return e.Modifiers
    End Function

    Public Shared Function GetKeyCode(keys As Keys) As Keys
        Dim e As New KeyEventArgs(keys)
        Return e.KeyCode
    End Function

    Private Sub ShowHotKeyValue(ByVal Modifiers As Keys, ByVal KeyCode As Keys)
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

        If Me.CurrentKey <> hotKeyValue Then
            Me.CurrentKey = hotKeyValue
        End If
        Me.Text = hotKeyString
        Me.SelectionStart = Me.Text.Length
    End Sub

    Private Function KeyCodeToString(ByVal KeyCode As Keys) As String
        If KeyCode >= Keys.D0 And KeyCode <= Keys.D9 Then
            Return KeyCode.ToString().Remove(0, 1)
        ElseIf KeyCode >= Keys.NumPad0 And KeyCode <= Keys.NumPad9 Then
            Return KeyCode.ToString().Replace("Pad", "")
        Else
            Return KeyCode.ToString()
        End If
    End Function

    Private Sub CheckHotkey()
        If Me.Text.Trim().EndsWith("+") Or String.IsNullOrWhiteSpace(Me.Text) Then
            Me.CurrentKey = -1
            Me.Text = ""
            Me.SelectionStart = 0
        End If
    End Sub

    '' ''''''''''

    Protected Overrides Sub OnKeyDown(e As System.Windows.Forms.KeyEventArgs)
        MyBase.OnKeyDown(e)
        e.SuppressKeyPress = False
        e.Handled = True
        ShowHotKeyValue(e.Modifiers, e.KeyCode)
    End Sub

    Protected Overrides Sub OnKeyPress(e As System.Windows.Forms.KeyPressEventArgs)
        MyBase.OnKeyPress(e)
        e.Handled = True
    End Sub

    Protected Overrides Sub OnKeyUp(e As System.Windows.Forms.KeyEventArgs)
        MyBase.OnKeyUp(e)
        CheckHotkey()
    End Sub

    Protected Overrides Sub OnLostFocus(e As System.EventArgs)
        MyBase.OnLostFocus(e)
        CheckHotkey()
    End Sub
End Class
