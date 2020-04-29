Public Class ColorDialog
    Public Delegate Sub SaveFunc()

    Public SaveCallback As SaveFunc

    Private Sub ColorDialog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ColorListView.Items.Clear()
        For Each colorItem As TipColor In GlobalModel.Colors
            AddToListView(colorItem)
        Next
        UpdateListViewColumn()
        ButtonRemove.Enabled = False
    End Sub

    Private Sub ColorListView_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ColorListView.SelectedIndexChanged
        ButtonRemove.Enabled = ColorListView.SelectedIndices.Count = 1
    End Sub

    Private Sub ColorListView_DoubleClick(sender As Object, e As EventArgs) Handles ColorListView.DoubleClick
        Dim point As Point = ColorListView.PointToClient(Cursor.Position)
        If ColorListView.SelectedItems.Count = 1 Then
            Dim item As ListViewItem = ColorListView.SelectedItems.Item(0)
            Dim subItem As ListViewItem.ListViewSubItem = ColorListView.HitTest(point).SubItem
            Dim col As Integer = item.SubItems.IndexOf(subItem)
            If col = ColumnHeaderName.DisplayIndex Then
                MsgBox(subItem.Text)
            ElseIf col = ColumnHeaderHex.DisplayIndex OrElse col = ColumnHeaderRgb.DisplayIndex OrElse col = ColumnHeaderView.DisplayIndex Then
                MsgBox(subItem.Text & " 0")
            End If
        End If
    End Sub

    Private Sub ButtonAdd_Click(sender As Object, e As EventArgs) Handles ButtonAdd.Click
        Dim title As String = InputBox("新颜色的标签：", "颜色", "颜色").Trim()
        If title <> "" Then
            Dim tipColor As New TipColor(GlobalModel.Colors.Count, title)
            AddToListView(tipColor)
            UpdateListViewColumn()
            SelectListView(ColorListView.Items.Count - 1)
        End If
    End Sub

    Private Sub ButtonRemove_Click(sender As Object, e As EventArgs) Handles ButtonRemove.Click
        If ColorListView.SelectedItems.Count = 1 Then
            Dim item As ListViewItem = ColorListView.SelectedItems(0)
            Dim tipColor = CType(item.Tag, TipColor)
            Dim ok = MessageBox.Show($"删除所选颜色 {tipColor.Name}？", "删除", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
            If ok = vbOK Then
                ColorListView.Items.RemoveAt(ColorListView.SelectedIndices(0))
            End If
        End If
    End Sub

#Region "列表函数"

    Private Sub ColorDialog_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        ColorListView.Select()
    End Sub

    Private Sub UpdateListViewColumn()
        For i = 0 To ColumnHeaderRgb.DisplayIndex
            ColorListView.AutoResizeColumn(i, ColumnHeaderAutoResizeStyle.ColumnContent)
        Next
    End Sub

    Private Sub AddToListView(c As TipColor)
        Dim item As ListViewItem = ColorListView.Items.Add(c.Id)
        item.Tag = c
        item.UseItemStyleForSubItems = False
        item.SubItems.Add(c.Name)
        item.SubItems.Add(c.HexColor)
        item.SubItems.Add(c.RgbColor)
        Dim subItem As ListViewItem.ListViewSubItem = item.SubItems.Add("")
        subItem.BackColor = c.Color
    End Sub

    Private Sub SelectListView(index As Integer)
        For Each i As ListViewItem In ColorListView.Items
            i.Selected = False
            i.Focused = False
        Next
        Dim item = ColorListView.Items(index)
        item.Selected = True
        item.Focused = True
        ColorListView.Select()
    End Sub

#End Region

    Private Sub ButtonSave_Click(sender As Object, e As EventArgs) Handles ButtonSave.Click
        If SaveCallback IsNot Nothing Then
            SaveCallback.Invoke()
        End If
        Me.Close()
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub
End Class
