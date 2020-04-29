Public Class HotKeyDialog

    Public Delegate Sub Callback(key As Keys, use As Boolean)
    Public OkCallback As Callback

    Private Sub CheckBoxIsValid_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles CheckBoxIsValid.CheckedChanged
        HotkeyEditBox.Enabled = CheckBoxIsValid.Checked
    End Sub

    Private Sub ButtonOK_Click(sender As System.Object, e As System.EventArgs) Handles ButtonOK.Click
        OkCallback.Invoke(HotkeyEditBox.CurrentKey, CheckBoxIsValid.Checked)
        Me.Close()
    End Sub

    Private Sub ButtonCancel_Click(sender As System.Object, e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonDefault_Click(sender As System.Object, e As System.EventArgs) Handles ButtonDefault.Click
        HotkeyEditBox.CurrentKey = Keys.F4
    End Sub
End Class
