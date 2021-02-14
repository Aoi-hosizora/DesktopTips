Public Class MessageBoxEx
    ''' <summary>
    ''' 显示 MessageBoxEx，给定 defBtn, mainFrm 和 btnTitles
    ''' </summary>
    Public Shared Function Show(text As String, caption As String, buttons As MessageBoxButtons, icon As MessageBoxIcon,
                                Optional defBtn As MessageBoxDefaultButton = MessageBoxDefaultButton.Button1,
                                Optional mainForm As Form = Nothing, Optional buttonTitles() As String = Nothing) As DialogResult
        Dim frm As New MessageForm(buttonTitles, mainForm)
        frm.Show()
        Dim result = MessageBox.Show(frm, text, caption, buttons, icon, defBtn)
        frm.Close()
        Return result
    End Function

    ''' <summary>
    ''' 显示 MessageBoxEx，给定 mainFrm 和 btnTitles
    ''' </summary>
    Public Shared Function Show(text As String, caption As String, buttons As MessageBoxButtons, icon As MessageBoxIcon,
                                Optional mainForm As Form = Nothing, Optional buttonTitles() As String = Nothing) As DialogResult
        Return Show(text, caption, buttons, icon, MessageBoxDefaultButton.Button1, mainForm, buttonTitles)
    End Function

    ''' <summary>
    ''' 显示 MessageBoxEx，给定标准参数
    ''' </summary>
    Public Shared Function Show(text As String, caption As String, buttons As MessageBoxButtons, icon As MessageBoxIcon) As DialogResult
        Return Show(text, caption, buttons, icon, MessageBoxDefaultButton.Button1, Nothing, Nothing)
    End Function

    ''' <summary>
    ''' MessageForm 内部类
    ''' </summary>
    Private Class MessageForm
        Inherits Form

        Private ReadOnly _buttonTitles() As String = Nothing
        Private ReadOnly _mainForm As Form
        Private _forHook = false

        Public Sub New(buttonTitles() As String, mainForm As Form)
            _buttonTitles = buttonTitles
            _mainForm = mainForm

            Text = ""
            StartPosition = FormStartPosition.CenterScreen
            ShowInTaskbar = False
            Opacity = 0
        End Sub

        Public Overloads Sub Show()
            MyBase.Show()
            _forHook = True ' Show 后需要 hook
        End Sub

        Protected Overrides Sub OnShown(e As EventArgs)
            MyBase.OnShown(e)
            NativeMethod.SetWindowPos(Handle, IntPtr.Zero, 0, 0, 0, 0, 659)
        End Sub

        Protected Overrides Sub OnFormClosed(e As FormClosedEventArgs)
            If _mainForm IsNot Nothing Then
                NativeMethod.SetForegroundWindow(_mainForm.Handle)
                _mainForm.Activate()
            End If
            MyBase.OnFormClosed(e)
        End Sub

        Protected Overrides Sub WndProc(ByRef m As Message)
            MyBase.WndProc(m)
            If _forHook And m.Msg = &H6 Then
                _forHook = False
                HookMsgBox(m.LParam)
            End If
        End Sub

        ''' <summary>
        ''' Hook MessageBox Form
        ''' </summary>
        Private Sub HookMsgBox(hdl As IntPtr)
            Dim index = 0
            Dim h As IntPtr = NativeMethod.GetWindow(hdl, NativeMethod.GW_CHILD)
            While h <> IntPtr.Zero
                If NativeMethod.GetWindowClassName(h).Equals("Button") Then
                    If _buttonTitles IsNot Nothing AndAlso _buttonTitles.Length > index Then
                        NativeMethod.SetWindowText(h, _buttonTitles(index)) ' <<<<<<<
                        index += 1
                    End If
                End If
                h = NativeMethod.GetWindow(h, NativeMethod.GW_HWNDNEXT)
            End While
        End Sub
    End Class
End Class
