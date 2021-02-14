''' <summary>
''' 设置快捷键
''' </summary>
Public Class HotkeyDialog
    ''' <summary>
    ''' 完成回调，参数表示 Key 和开启快捷键
    ''' </summary>
    Public OkCallback As Action(Of Keys, Boolean)

    Private Sub CheckBoxIsValid_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxIsValid.CheckedChanged
        HotkeyEditBox.Enabled = CheckBoxIsValid.Checked
    End Sub

    Private Sub ButtonOK_Click(sender As Object, e As EventArgs) Handles ButtonOK.Click
        Close()
        OkCallback?.Invoke(HotkeyEditBox.CurrentKey, CheckBoxIsValid.Checked) ' 触发回调
    End Sub

    Private Sub ButtonDefault_Click(sender As Object, e As EventArgs) Handles ButtonDefault.Click
        HotkeyEditBox.CurrentKey = Keys.F4 ' 默认快捷键
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Close()
    End Sub
End Class
