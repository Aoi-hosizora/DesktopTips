''' <summary>
''' 包含的链接
''' </summary>
Public Class LinkDialog
    ''' <summary>
    ''' 对话框标题
    ''' </summary>
    Public Message As String

    ''' <summary>
    ''' 链接
    ''' </summary>
    Public Links As IEnumerable(Of String)

    ''' <summary>
    ''' 打开回调，包含链接和是否新窗口打开
    ''' </summary>
    Public OkCallback As Action(Of IEnumerable(Of String), Boolean)

    Private Sub Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ListView.Items.Clear()
        For Each link As String In Links
            ListView.Items.Add(link)
        Next
        ListView_SelectedValueChanged(sender, e)

        CheckBoxOpenInNew.Checked = My.Settings.OpenInNewBrowser
    End Sub

    Private Sub ButtonOpen_Click(sender As Object, e As EventArgs) Handles ButtonOpen.Click
        Dim l As New List(Of String)
        For Each item In ListView.SelectedItems
            l.Add(CStr(item))
        Next
        OkCallback?.Invoke(l, CheckBoxOpenInNew.Checked)
        Close()
    End Sub

    Private Sub ButtonOpenAll_Click(sender As System.Object, e As EventArgs) Handles ButtonOpenAll.Click
        OkCallback?.Invoke(ListView.Items.Cast(Of String)(), CheckBoxOpenInNew.Checked)
        Close()
    End Sub

    Private Sub ListView_DoubleClick(sender As Object, e As EventArgs) Handles ListView.DoubleClick
        Dim ok = MessageBoxEx.Show($"是否打开以下链接？{vbNewLine}{vbNewLine}{ListView.SelectedItem}", "打开链接", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
        If ok = VbOk Then
            If OkCallback IsNot Nothing Then
                OkCallback.Invoke({CStr(ListView.SelectedItem)}, CheckBoxOpenInNew.Checked)
            End If
        End If
    End Sub

    Private Sub ListView_SelectedValueChanged(sender As Object, e As EventArgs) Handles MyBase.Load, ListView.SelectedValueChanged
        LabelTitle.Text = Message & $" (选中 {ListView.SelectedItems.Count} 项)"
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
