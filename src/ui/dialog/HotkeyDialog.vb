Public Class HotkeyDialog
    Public Delegate Sub Callback(key As Keys, use As Boolean)

    Public OkCallback As Callback

    Private Sub CheckBoxIsValid_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxIsValid.CheckedChanged
        HotkeyEditBox.Enabled = CheckBoxIsValid.Checked
    End Sub

    Private Sub ButtonOK_Click(sender As Object, e As EventArgs) Handles ButtonOK.Click
        If OkCallback IsNot Nothing Then
            OkCallback.Invoke(HotkeyEditBox.CurrentKey, CheckBoxIsValid.Checked)
        End If
        Me.Close()
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonDefault_Click(sender As Object, e As EventArgs) Handles ButtonDefault.Click
        HotkeyEditBox.CurrentKey = Keys.F4
    End Sub
End Class
