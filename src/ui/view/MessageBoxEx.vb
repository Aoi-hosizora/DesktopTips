Public Class MessageBoxEx
    Public Shared Function Show(text As String, caption As String, buttons As MessageBoxButtons, icon As MessageBoxIcon,
                                Optional defBtn As MessageBoxDefaultButton = MessageBoxDefaultButton.Button1,
                                Optional mainFrm As Form = Nothing,
                                Optional btnTitles() As String = Nothing) As DialogResult
        Dim frm As New MessageForm(btnTitles, mainFrm)
        frm.Show()
        frm.WatchForActivate = true
        Dim result = MessageBox.Show(frm, text, caption, buttons, icon, defBtn)
        frm.Close()
        Return result
    End Function

    Public Shared Function Show(text As String, caption As String, buttons As MessageBoxButtons, icon As MessageBoxIcon,
                                Optional mainFrm As Form = Nothing,
                                Optional btnTitles() As String = Nothing) As DialogResult
        Return Show(text, caption, buttons, icon, MessageBoxDefaultButton.Button1, mainFrm, btnTitles)
    End Function

    Public Shared Function Show(text As String, caption As String, buttons As MessageBoxButtons, icon As MessageBoxIcon) As DialogResult
        Return Show(text, caption, buttons, icon, MessageBoxDefaultButton.Button1, Nothing, Nothing)
    End Function

    Private Class MessageForm
        Inherits Form

        Private _handle As IntPtr
        Private ReadOnly _buttonTitles() As String = Nothing
        Private ReadOnly _mainFrm As Form

        Public Property WatchForActivate As Boolean

        Public Sub New(buttonTitles() As String, mainFrm As Form)
            _buttonTitles = buttonTitles
            _mainFrm = mainFrm

            Me.Text = ""
            Me.StartPosition = FormStartPosition.CenterScreen
            Me.ShowInTaskbar = False
            Me.Opacity = 0
        End Sub

        Protected Overrides Sub OnShown(e As EventArgs)
            MyBase.OnShown(e)
            NativeMethod.SetWindowPos(Me.Handle, IntPtr.Zero, 0, 0, 0, 0, 659)
        End Sub

        Protected Overrides Sub OnFormClosed(e As FormClosedEventArgs)
            If _mainFrm IsNot Nothing Then
                NativeMethod.SetForegroundWindow(_mainFrm.Handle)
                _mainFrm.Activate()
            End If
            MyBase.OnFormClosed(e)
        End Sub

        Protected Overrides Sub WndProc(ByRef m As Message)
            If WatchForActivate And m.Msg = &H6 Then
                _WatchForActivate = False
                _handle = m.LParam
                HookMsgBox()
            End If
            MyBase.WndProc(m)
        End Sub

        Private Sub HookMsgBox()
            Dim h As IntPtr = NativeMethod.GetWindow(_handle, NativeMethod.GW_CHILD)
            Dim buttonTitleIndex = 0

            While h <> IntPtr.Zero
                If NativeMethod.GetWindowClassName(h).Equals("Button") Then
                    If _buttonTitles IsNot Nothing AndAlso _buttonTitles.Length > buttonTitleIndex Then
                        NativeMethod.SetWindowText(h, _buttonTitles(buttonTitleIndex))
                        buttonTitleIndex += 1
                    End If
                End If
                h = NativeMethod.GetWindow(h, NativeMethod.GW_HWNDNEXT)
            End While
        End Sub
    End Class
End Class
