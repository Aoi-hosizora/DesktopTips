Public Class HotKeyDialog

    Private Sub CheckBoxIsValid_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles CheckBoxIsValid.CheckedChanged
        HotKeyBox1.Enabled = CheckBoxIsValid.Checked
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOK_Click(sender As System.Object, e As System.EventArgs) Handles ButtonOK.Click
        Dim setting As SettingUtil.AppSetting = SettingUtil.LoadAppSettings()

        Dim toUseHotKey As Boolean = CheckBoxIsValid.Checked
        Dim toHotKey As Keys = HotKeyBox1.CurrentKey
        MainForm.UnregisterShotcut()

        If Not toUseHotKey OrElse MainForm.RegisterShotcut(toHotKey) Then
            setting.IsUseHotKey = toUseHotKey
            setting.HotKey = toHotKey
            SettingUtil.SaveAppSettings(setting)
            Me.Close()
        End If
    End Sub

    Private Sub HotKeyDialog_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class