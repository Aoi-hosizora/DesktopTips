Public Class HighlightSelectForm
    Public Overloads Shared Function ShowDialog(tab As Tab) As Tuple(Of Tab, Integer)
        Dim dlg As New HighlightSelectForm
        With dlg
            .ComboBoxTab.DisplayMember = "Title"
            .ComboBoxTab.Items.Clear()
            .ComboBoxTab.Items.AddRange(GlobalModel.Tabs.Cast(Of Object)().ToArray())
            .ComboBoxTab.SelectedIndex = Math.Max(GlobalModel.Tabs.FindIndex(Function(t) t.Title = tab.Title), 0)

            If .ShowDialog() = DialogResult.Cancel Then Return Nothing
            If .ComboBoxTab.SelectedItem Is Nothing OrElse .ListBoxHighlight.SelectedItem Is Nothing Then Return Nothing
            Dim selectedTab = CType(.ComboBoxTab.SelectedItem, Tab)
            Dim selectedColor = CType(.ListBoxHighlight.SelectedItem, Integer)
            Return New Tuple(Of Tab, Integer)(selectedTab, selectedColor)
        End With
    End Function

    Private Sub ComboBoxTab_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxTab.SelectedIndexChanged
        Dim tab = GlobalModel.Tabs.Find(Function(t) t.Title = ComboBoxTab.Text)
        ListBoxHighlight.Items.Clear()
        Dim highlights = tab.Tips.Select(Function(t) t.ColorId).Distinct().Cast(Of Object)().ToArray()
        Array.Sort(highlights)
        ListBoxHighlight.Items.AddRange(highlights)
        ListBoxHighlight.SelectedIndex = 0
    End Sub

    Private Sub ListBoxHighlight_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBoxHighlight.SelectedIndexChanged
        Dim ok = ListBoxHighlight.Items.Count > 0 AndAlso ListBoxHighlight.SelectedIndex >= 0
        ButtonOK.Enabled = ok
        If ok Then
            Dim tab = CType(ComboBoxTab.SelectedItem, Tab)
            Dim colorId = CInt(ListBoxHighlight.SelectedItem)
            Dim total = tab.Tips.Count
            Dim highlighted = tab.Tips.FindAll(Function(t) t.ColorId = colorId).Count
            LabelCount.Text = $"标签数 : {highlighted} / {total}"
        End If
    End Sub

    Private Sub ButtonOK_Click(sender As Object, e As EventArgs) Handles ButtonOK.Click
        DialogResult = DialogResult.OK
        Close()
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

    Private Sub ListBoxHighlight_DrawItem(sender As Object, e As DrawItemEventArgs) Handles ListBoxHighlight.DrawItem
        If e.Index < 0 OrElse e.Index >= ListBoxHighlight.Items.Count Then
            Return
        End If

        Dim colorId = CType(ListBoxHighlight.Items(e.Index), Integer)
        Dim c = GlobalModel.Colors.ElementAtOrDefault(colorId)
        Dim hName = If(c?.Name, "无高亮")
        Dim hColor = If(c?.Color, Color.Black)
        Dim hStyle = If(c?.Style, FontStyle.Regular)

        Dim g = e.Graphics
        Dim b = New Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height)
        g.SmoothingMode = Drawing2D.SmoothingMode.Default
        g.FillRectangle(New SolidBrush(e.BackColor), b)
        g.DrawRectangle(New Pen(e.BackColor), b)
        e.DrawBackground()
        g.DrawString(hName, New Font(e.Font, hStyle), New SolidBrush(hColor), b, StringFormat.GenericDefault)
    End Sub
End Class
