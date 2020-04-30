Public Class HotkeyDialog
    Public RegisterFunc As Action(Of Keys, Boolean)

    Private Sub CheckBoxIsValid_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxIsValid.CheckedChanged
        HotkeyEditBox.Enabled = CheckBoxIsValid.Checked
    End Sub

    Private Sub ButtonOK_Click(sender As Object, e As EventArgs) Handles ButtonOK.Click
        Me.Close()
        If RegisterFunc IsNot Nothing Then
            RegisterFunc.Invoke(HotkeyEditBox.CurrentKey, CheckBoxIsValid.Checked)
        End If
    End Sub

    Private Sub ButtonDefault_Click(sender As Object, e As EventArgs) Handles ButtonDefault.Click
        HotkeyEditBox.CurrentKey = Keys.F4
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub
End Class
