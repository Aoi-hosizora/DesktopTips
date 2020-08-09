Public Class TipsEditDialog

    ''' <summary>
    ''' 显示自定义对话框
    ''' </summary>
    Public Overloads Shared Function ShowDialog(message As String, title As String, Optional content As String = "") As String
        Dim dlg As New TipsEditDialog
        dlg.Width = 400
        dlg.TextBoxContent.Height = 175

        With dlg.LabelMessage
            .Text = message
            .Location = New Point(12, 9)
            .AutoSize = True
            Dim ih = dlg.ButtonCancel.Top + dlg.ButtonCancel.Height - .Top
            Dim aw = dlg.Width - 2 * .Left - dlg.ButtonOK.Width - (dlg.Width - dlg.ButtonOK.Width - dlg.ButtonOK.Left)
            .MinimumSize = New Size(0, ih)
            .MaximumSize = New Size(aw, 0)
        End With

        Dim titleBarHeight = dlg.RectangleToScreen(dlg.ClientRectangle).Top - dlg.Top
        With dlg
            .TextBoxContent.Top = 2 * .LabelMessage.Top + .LabelMessage.Height
            .Height = .TextBoxContent.Top + .TextBoxContent.Height + .TextBoxContent.Left + titleBarHeight
        End With

        dlg.Text = title
        dlg.TextBoxContent.Text = content
        dlg.TextBoxContent.Focus()
        dlg.ButtonOK.Enabled = dlg.TextBoxContent.Text.Trim() <> ""

        Dim ok = dlg.ShowDialog()
        If ok = vbCancel Then
            Return ""
        Else
            Return dlg.TextBoxContent.Text
        End If
    End Function

    Private Sub ButtonOK_Click(sender As Object, e As EventArgs) Handles ButtonOK.Click
        Me.Close()
        DialogResult = DialogResult.OK
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
        DialogResult = DialogResult.Cancel
    End Sub

    Private Sub TextBoxContent_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextBoxContent.TextChanged
        ButtonOK.Enabled = TextBoxContent.Text.Trim() <> ""
    End Sub

End Class
