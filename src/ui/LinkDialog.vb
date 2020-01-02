Public Class LinkDialog

    Private Sub ButtonOpen_Click(sender As System.Object, e As System.EventArgs) Handles ButtonOpen.Click
        Dim links As New List(Of String)
        For Each item In ListView.SelectedItems
            links.Add(CStr(item))
        Next
        CommonUtil.OpenWebsInDefaultBrowser(links)
        Me.Close()
    End Sub

    Private Sub ButtonBack_Click(sender As System.Object, e As System.EventArgs) Handles ButtonBack.Click
        Me.Close()
    End Sub

    Private Sub ListView_DoubleClick(sender As Object, e As System.EventArgs) Handles ListView.DoubleClick
        If ListView.SelectedItems.Count() = 1 Then
            Me.TopMost = False
            Dim newVal$ = InputBox("修改链接 " & ListView.SelectedItems(0).ToString() & " 为：", "修改链接", ListView.SelectedItems(0).ToString())
            Me.TopMost = True
            If Not String.IsNullOrWhiteSpace(newVal) Then
                ListView.Items(ListView.Items.IndexOf(ListView.SelectedItems(0))) = newVal
            End If
        End If
    End Sub

    Private Sub ListView_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles ListView.MouseDown
        If ListView.IndexFromPoint(e.X, e.Y) = -1 Then
            ListView.ClearSelected()
        End If
    End Sub

    Private Sub LoadListViewLabel(sender As System.Object, e As System.EventArgs) Handles MyBase.Load, ListView.SelectedValueChanged
        Dim count% = ListView.Items.Count()
        Dim selCount% = ListView.SelectedItems.Count()
        Me.LabelTitle.Text = String.Format("所选内容包含了 {0} 个链接 (选中 {1} 项)", count, selCount)
        ButtonOpen.Enabled = selCount <> 0
    End Sub
End Class