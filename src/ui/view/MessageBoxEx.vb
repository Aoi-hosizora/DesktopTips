﻿Imports System.Text
Imports System.Runtime.InteropServices

''' <summary>
''' MessageBox 帮助类
''' </summary>
Public Class MessageBoxEx

    Public Shared Function Show(text As String,
                                caption As String,
                                buttons As MessageBoxButtons,
                                Optional icon As MessageBoxIcon = MessageBoxIcon.None,
                                Optional defaultButton As MessageBoxDefaultButton = MessageBoxDefaultButton.Button1,
                                Optional mainForm As Form = Nothing,
                                Optional buttonTitles() As String = Nothing)

        Dim frm As New MessageForm(buttons, buttonTitles, mainForm) With {.Opacity = 0, .WatchForActivate = True}
        Dim result As DialogResult = MessageBox.Show(frm, text, caption, buttons, icon, defaultButton)
        frm.Close()
        Return result
    End Function

    Private Class MessageForm : Inherits Form

        Private _handle As IntPtr
        Private _buttons As MessageBoxButtons
        Private _buttonTitles() As String = Nothing
        Private _mainForm As Form

        Public Property WatchForActivate As Boolean

        Public Sub New(buttons As MessageBoxButtons, buttonTitles() As String, mainForm As Form)
            _buttons = buttons
            _buttonTitles = buttonTitles
            _mainForm = mainForm

            Me.Text = ""
            Me.StartPosition = FormStartPosition.CenterScreen
            Me.ShowInTaskbar = False
        End Sub

        Protected Overrides Sub OnShown(e As System.EventArgs)
            MyBase.OnShown(e)
            NativeMethod.SetWindowPos(Me.Handle, IntPtr.Zero, 0, 0, 0, 0, 659)
        End Sub

        Protected Overrides Sub OnFormClosed(e As System.Windows.Forms.FormClosedEventArgs)
            If _mainForm IsNot Nothing Then
                NativeMethod.SetForegroundWindow(_mainForm.Handle)
                _mainForm.Activate()
            End If
            MyBase.OnFormClosed(e)
        End Sub

        Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
            If WatchForActivate And m.Msg = &H6 Then
                _WatchForActivate = False
                _handle = m.LParam
                HookMsgBox()
            End If
            MyBase.WndProc(m)
        End Sub

        Private Sub HookMsgBox()
            Dim h As IntPtr = NativeMethod.GetWindow(_handle, NativeMethod.GW_CHILD)
            Dim buttonTitleIndex As Integer = 0

            While h <> IntPtr.Zero
                If NativeMethod.GetWindowClassName(h).Equals("Button") AndAlso
                    _buttonTitles IsNot Nothing AndAlso _buttonTitles.Length <> 0 AndAlso
                    _buttonTitles.Length > buttonTitleIndex Then

                    NativeMethod.SetWindowText(h, _buttonTitles(buttonTitleIndex))
                    buttonTitleIndex += 1
                End If
                h = NativeMethod.GetWindow(h, NativeMethod.GW_HWNDNEXT)
            End While
        End Sub
    End Class

End Class