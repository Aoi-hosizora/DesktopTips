Public Class HotkeyDialog
    Public Delegate Sub DoFunc(key As Keys, use As Boolean)

    Public DoCallback As DoFunc

    Private Sub CheckBoxIsValid_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxIsValid.CheckedChanged
        HotkeyEditBox.Enabled = CheckBoxIsValid.Checked
    End Sub

    Private Sub ButtonOK_Click(sender As Object, e As EventArgs) Handles ButtonOK.Click
        Me.Close()
        If DoCallback IsNot Nothing Then
            DoCallback.Invoke(HotkeyEditBox.CurrentKey, CheckBoxIsValid.Checked)
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonDefault_Click(sender As Object, e As EventArgs) Handles ButtonDefault.Click
        HotkeyEditBox.CurrentKey = Keys.F4
    End Sub
End Class
