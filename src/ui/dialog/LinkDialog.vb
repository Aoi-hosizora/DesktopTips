Public Class LinkDialog
    Public Delegate Function GetLinksFunc() As IEnumerable(Of String)

    Public Delegate Sub OpenBrowserFunc(links As IEnumerable(Of String))

    Public GetLinksCallback As GetLinksFunc
    Public OpenBrowserCallback As OpenBrowserFunc

    Private Sub Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetLinksCallback Is Nothing Then
            Me.Close()
            Return
        End If
        ListView.Items.Clear()
        For Each link As String In GetLinksCallback.Invoke()
            ListView.Items.Add(link)
        Next
        ListView_SelectedValueChanged(sender, e)
    End Sub

    Private Sub ListView_SelectedValueChanged(sender As Object, e As EventArgs) Handles MyBase.Load, ListView.SelectedValueChanged
        Me.LabelTitle.Text = $"所选内容包含了 {ListView.Items.Count} 个链接 (选中 {ListView.SelectedItems.Count} 项)"
        ButtonOpen.Enabled = ListView.SelectedItems.Count <> 0
    End Sub

    Private Sub ButtonOpen_Click(sender As Object, e As EventArgs) Handles ButtonOpen.Click
        Dim links As New List(Of String)
        For Each item In ListView.SelectedItems
            links.Add(CStr(item))
        Next
        If OpenBrowserCallback IsNot Nothing Then
            OpenBrowserCallback.Invoke(links)
        End If
        Me.Close()
    End Sub

    Private Sub ButtonBack_Click(sender As Object, e As EventArgs) Handles ButtonBack.Click
        Me.Close()
    End Sub

    Private Sub ListView_DoubleClick(sender As Object, e As EventArgs) Handles ListView.DoubleClick
        Dim ok = MessageBox.Show($"是否打开以下链接？{vbNewLine}{vbNewLine}{ListView.SelectedItem}", "打开链接", MessageBoxButtons.OKCancel)
        If ok = VbOk Then
            OpenBrowserCallback.Invoke({CStr(ListView.SelectedItem)})
            Me.Close()
        End If
    End Sub

    Private Sub ListView_MouseDown(sender As Object, e As MouseEventArgs) Handles ListView.MouseDown
        If ListView.IndexFromPoint(e.X, e.Y) = - 1 Then
            ListView.ClearSelected()
        End If
    End Sub
End Class
