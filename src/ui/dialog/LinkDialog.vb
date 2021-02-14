Public Class LinkDialog
    Public GetFunc As Func(Of IEnumerable(Of String))
    Public OpenBrowserFunc As Action(Of IEnumerable(Of String), Boolean)

    Private Sub Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetFunc Is Nothing Then
            Close()
            Return
        End If

        ListView.Items.Clear()
        For Each link As String In GetFunc.Invoke()
            ListView.Items.Add(link)
        Next
        ListView_SelectedValueChanged(sender, e)

        CheckBoxOpenInNew.Checked = My.Settings.OpenInNewBrowser
    End Sub

    Private Sub ButtonOpen_Click(sender As Object, e As EventArgs) Handles ButtonOpen.Click
        Dim links As New List(Of String)
        For Each item In ListView.SelectedItems
            links.Add(CStr(item))
        Next
        If OpenBrowserFunc IsNot Nothing Then
            OpenBrowserFunc.Invoke(links, CheckBoxOpenInNew.Checked)
        End If
        Close()
    End Sub

    Private Sub ButtonOpenAll_Click(sender As System.Object, e As EventArgs) Handles ButtonOpenAll.Click
        If OpenBrowserFunc IsNot Nothing Then
            OpenBrowserFunc.Invoke(ListView.Items.Cast(Of String)(), CheckBoxOpenInNew.Checked)
        End If
        Close()
    End Sub

    Private Sub ListView_DoubleClick(sender As Object, e As EventArgs) Handles ListView.DoubleClick
        Dim ok = MessageBoxEx.Show($"是否打开以下链接？{vbNewLine}{vbNewLine}{ListView.SelectedItem}", "打开链接", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
        If ok = VbOk Then
            If OpenBrowserFunc IsNot Nothing Then
                OpenBrowserFunc.Invoke({CStr(ListView.SelectedItem)}, CheckBoxOpenInNew.Checked)
            End If
            Close()
        End If
    End Sub

    Private Sub ListView_SelectedValueChanged(sender As Object, e As EventArgs) Handles MyBase.Load, ListView.SelectedValueChanged
        LabelTitle.Text = $"所选内容包含了 {ListView.Items.Count} 个链接 (选中 {ListView.SelectedItems.Count} 项)"
        ButtonOpen.Enabled = ListView.SelectedItems.Count <> 0
    End Sub

    Private Sub ListView_MouseDown(sender As Object, e As MouseEventArgs) Handles ListView.MouseDown
        If ListView.IndexFromPoint(e.X, e.Y) = -1 Then
            ListView.ClearSelected()
        End If
    End Sub

    Private Sub ButtonBack_Click(sender As Object, e As EventArgs) Handles ButtonBack.Click
        Close()
    End Sub

    Private Sub LinkDialog_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter And e.Control Then
            e.Handled = True
            For i = 0 To ListView.Items.Count - 1
                ListView.SetSelected(i, True)
            Next i
        End If
    End Sub

    Private Sub ListView_KeyDown(sender As Object, e As KeyEventArgs) Handles ListView.KeyDown
        If e.KeyCode = Keys.A And e.Control Then
            e.Handled = True
            For i = 0 To ListView.Items.Count - 1
                ListView.SetSelected(i, True)
            Next i
        End If
    End Sub

    Private Sub CheckBoxOpenInNew_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxOpenInNew.CheckedChanged
        My.Settings.OpenInNewBrowser = CheckBoxOpenInNew.Checked
        My.Settings.Save()
    End Sub
End Class
