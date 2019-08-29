Imports System.Text
Imports System.Runtime.InteropServices

Public Class MessageBoxEx

    Public Shared Function Show(text As String, caption As String, buttons As MessageBoxButtons, Optional buttonTitles() As String = Nothing)
        Return Show(text, caption, buttons, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, {"d"})
    End Function

    Public Shared Function Show(text As String, caption As String, buttons As MessageBoxButtons, _
                                icon As MessageBoxIcon, defaultButton As MessageBoxDefaultButton, _
                                Optional buttonTitles() As String = Nothing)

        Dim frm As New MessageForm(buttons, buttonTitles)
        frm.Opacity = 0
        frm.Show()
        frm.WatchForActivate = True
        Dim result As DialogResult = MessageBox.Show(frm, text, caption, buttons, icon, defaultButton)
        frm.Close()
        Return result
    End Function

    Class MessageForm
        Inherits Form

        Private _handle As IntPtr
        Private _buttons As MessageBoxButtons
        Private _buttonTitles() As String = Nothing
        Private _DlgFont As New Font("Microsoft YaHei UI", SystemFonts.DefaultFont.Size - 1.1)
        Public Property WatchForActivate As Boolean

        Public Sub New(buttons As MessageBoxButtons, buttonTitles() As String)
            _buttons = buttons
            _buttonTitles = buttonTitles

            Me.Text = ""
            Me.StartPosition = FormStartPosition.CenterScreen
            Me.Location = New Point(-32000, -32000)
            Me.Font = _DlgFont
            Me.ShowInTaskbar = False
        End Sub

        Protected Overrides Sub OnShown(e As System.EventArgs)
            MyBase.OnShown(e)
            SetWindowPos(Me.Handle, IntPtr.Zero, 0, 0, 0, 0, 659)
        End Sub

        Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
            If WatchForActivate And m.Msg = &H6 Then
                _WatchForActivate = False
                _handle = m.LParam

                'Dim r As New RECT
                'GetWindowRect(_handle, r)
                'Dim newH As Integer = (r.Right - r.Left) * 1.2
                'Dim newW As Integer = (r.Bottom - r.Top) * 1.5
                'MoveWindow(_handle, r.Left, r.Top, newH, newW, True)

                CheckMsgBox()
            End If
            MyBase.WndProc(m)
        End Sub

        Private Sub CheckMsgBox()
            Dim h As IntPtr = GetWindow(_handle, GW_CHILD)
            Dim buttonTitleIndex As Integer = 0

            While h <> IntPtr.Zero
                If GetWindowClassName(h).Equals("Static") Then
                    'SendMessage(h, WM_SETFONT, _DlgFont.ToHfont(), IntPtr.Zero)
                ElseIf GetWindowClassName(h).Equals("Button") Then
                    If _buttonTitles IsNot Nothing AndAlso _buttonTitles.Length <> 0 Then
                        If _buttonTitles.Length > buttonTitleIndex Then
                            SetWindowText(h, _buttonTitles(buttonTitleIndex))
                            buttonTitleIndex += 1
                        End If
                    End If
                End If
                h = GetWindow(h, GW_HWNDNEXT)
            End While

        End Sub
    End Class

    Private Const GW_CHILD As Integer = 5
    Private Const GW_HWNDNEXT As Integer = 2
    Private Const WM_SETFONT As Integer = &H30

    Public Structure RECT
        Public Left As Integer
        Public Top As Integer
        Public Right As Integer
        Public Bottom As Integer
    End Structure

    Private Declare Function GetWindow Lib "user32" (hWnd As IntPtr, wCmd As Int32) As IntPtr
    Private Declare Function GetClassNameW Lib "user32" (hWnd As IntPtr, <MarshalAs(UnmanagedType.LPWStr)> lpString As StringBuilder, nMaxCount As Integer) As Integer
    Private Declare Function GetWindowRect Lib "user32" (hWnd As IntPtr, ByRef lpRect As RECT) As Boolean

    Private Declare Function SetWindowPos Lib "user32" (hWnd As IntPtr, hWndInsertAfter As IntPtr, x As Integer, y As Integer, Width As Integer, Height As Integer, flags As Integer) As Boolean
    Private Declare Function SetWindowText Lib "user32" Alias "SetWindowTextA" (hWnd As IntPtr, wCmdlpString As String) As Boolean
    Private Declare Function SendMessage Lib "user32" Alias "SendMessageA" (hWnd As IntPtr, Msg As Integer, wParam As IntPtr, lParam As IntPtr) As IntPtr
    Private Declare Function MoveWindow Lib "user32" (hWnd As IntPtr, X As Integer, Y As Integer, nWidth As Integer, nHeight As Integer, bRepaint As Boolean) As Boolean

    Private Shared Function GetWindowClassName(handle As IntPtr) As String
        Dim sb As New StringBuilder(256)
        GetClassNameW(handle, sb, sb.Capacity)
        Return sb.ToString()
    End Function

End Class
