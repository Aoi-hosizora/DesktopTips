Public Class HighlightTipsDialog

    Private _selectedCallback As Action(Of Integer, Integer) ' 参数为 TabIndex, TipIndex
    Private _currentTab As Tab
    Private _currentColorId As Integer

    Public Overloads Shared Function ShowDialog(tt As Tuple(Of Tab, Integer), selectedCallback As Action(Of Integer, Integer)) As DialogResult
        Dim dlg As New HighlightTipsDialog
        With dlg
            ._selectedCallback = selectedCallback
            ._currentTab = tt.Item1
            ._currentColorId = tt.Item2
            Dim tips = ._currentTab.Tips.FindAll(Function(t) t.ColorId = ._currentColorId)
            Dim colorName = If(GlobalModel.Colors.ElementAtOrDefault(._currentColorId)?.Name, "五高亮")
            .ListBoxTips.DisplayMember = "Content"
            .ListBoxTips.Items.AddRange(tips.Cast(Of Object)().ToArray())
            .LabelTitle.Text = $"分组 ""{ ._currentTab.Title}"" 中高亮颜色为 ""{colorName}"" 的标签：(共 ${.ListBoxTips.Items.Count} 项)"
            Return .ShowDialog()
        End With
    End Function

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

    Private Sub ListBoxTips_DoubleClick(sender As Object, e As EventArgs) Handles ListBoxTips.DoubleClick
        If ListBoxTips.SelectedIndex = -1 Then Return
        Dim currentTip = CType(ListBoxTips.SelectedItem, TipItem)
        If _selectedCallback IsNot Nothing Then
            Dim tabIdx = GlobalModel.Tabs.FindIndex(Function(t) t.Title = _currentTab.Title)
            Dim tipIdx = _currentTab.Tips.IndexOf(currentTip)
            _selectedCallback.Invoke(tabIdx, tipIdx)
        End If
    End Sub

    Private Sub ListBoxTips_DrawItem(sender As Object, e As DrawItemEventArgs) Handles ListBoxTips.DrawItem
        If e.Index < 0 OrElse e.Index >= ListBoxTips.Items.Count Then
            Return
        End If

        Dim item = CType(ListBoxTips.Items(e.Index), TipItem)
        Dim c = GlobalModel.Colors.ElementAtOrDefault(item.ColorId)
        Dim s = item.Content.Replace(vbNewLine, "↴") ' ¬ ↴ ⇁ ¶
        Dim hColor = If(c?.Color, Color.Black)
        Dim hStyle = If(c?.Style, FontStyle.Regular)

        Dim g = e.Graphics
        Dim b = New Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height)
        g.SmoothingMode = Drawing2D.SmoothingMode.Default
        g.FillRectangle(New SolidBrush(e.BackColor), b)
        g.DrawRectangle(New Pen(e.BackColor), b)
        e.DrawBackground()
        g.DrawString(s, New Font(e.Font, hStyle), New SolidBrush(hColor), b, StringFormat.GenericDefault)
    End Sub
End Class
