Public Class ColorDialog
    Public SaveFunc As Action

    Private Sub ColorDialog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ColorListView.Items.Clear()
        For Each colorItem As TipColor In GlobalModel.Colors
            AddToListView(colorItem)
        Next
        UpdateListViewColumn()
        ButtonRemove.Enabled = False
    End Sub

    Private Sub ButtonAdd_Click(sender As Object, e As EventArgs) Handles ButtonAdd.Click
        Dim title As String = InputBox("新颜色的标签：", "新建", "颜色").Trim()
        If title <> "" Then
            Dim id As Integer = ColorListView.Items.Count
            Dim tipColor As New TipColor(id, title)
            AddToListView(tipColor)
            UpdateListViewColumn()
            SelectListView(id)

            GlobalModel.Colors.Add(tipColor)
            refreshSave()
        End If
    End Sub

    Private Sub ButtonRemove_Click(sender As Object, e As EventArgs) Handles ButtonRemove.Click
        If ColorListView.SelectedItems.Count <> 1 Then Return
        Dim item As ListViewItem = ColorListView.SelectedItems(0)
        Dim tipColor = CType(item.Tag, TipColor)

        Dim ok = MessageBoxEx.Show($"删除所选颜色 ""{tipColor.Name}""？", "删除", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
        If ok <> vbOK Then Return

        Dim tips As IEnumerable(Of TipItem) = GlobalModel.Tabs.SelectMany(Function(t) t.Tips).Where(Function(t) t.ColorId = tipColor.Id)
        If tips.Count <> 0 Then
            Dim tipString As String = String.Join(vbNewLine, tips.Select(Function(t) t.Content))
            ok = MessageBoxEx.Show($"颜色 ""{tipColor.Name}"" 拥有一下 {tips.Count} 个已存在的标签，是否删除？{vbNewLine}{vbNewLine}{tipString}",
                "删除", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1,
                Me, {"修改高亮颜色", "直接删除", "取消"})
            If ok = vbCancel Then
                Return
            ElseIf ok = vbNo Then
                GlobalModel.Tabs.ForEach(Sub(t) t.Tips.RemoveAll(Function(tip) tip.ColorId = tipColor.Id))
            Else
                MsgBox("TODO")
                Return
            End If
        End If
        GlobalModel.Colors.Remove(tipColor)
        GlobalModel.HandleWithColorOrder(GlobalModel.Colors, GlobalModel.Tabs)
        refreshSave()

        ColorDialog_Load(sender, e)
        SelectListView(tipColor.Id)
    End Sub

    Private Sub EditColorName(item As ListViewItem)
        Dim tipColor = CType(item.Tag, TipColor)
        Dim newName = InputBox("修改颜色标签：", "修改", tipColor.Name).Trim()
        If newName <> "" Then
            tipColor.Name = newName
            item.Tag = tipColor
            item.SubItems(ColumnHeaderName.DisplayIndex).Text = newName
            UpdateListViewColumn()

            GlobalModel.Colors(tipColor.Id).Name = newName
            refreshSave()
        End If
    End Sub

    Private Sub EditColorValue(item As ListViewItem)
        Dim tipColor = CType(item.Tag, TipColor)
        Dim colorDlg As New Windows.Forms.ColorDialog
        colorDlg.Color = tipColor.Color
        colorDlg.ShowDialog()
        Dim newColor As Color = colorDlg.Color

        tipColor.Color = newColor
        item.Tag = tipColor
        item.SubItems(ColumnHeaderHex.DisplayIndex).Text = tipColor.HexColor
        item.SubItems(ColumnHeaderRgb.DisplayIndex).Text = tipColor.RgbColor
        item.SubItems(ColumnHeaderView.DisplayIndex).BackColor = tipColor.Color
        UpdateListViewColumn()

        GlobalModel.Colors(tipColor.Id).Color = newColor
        refreshSave()
    End Sub

    Private Sub ColorListView_DoubleClick(sender As Object, e As EventArgs) Handles ColorListView.DoubleClick
        Dim point As Point = ColorListView.PointToClient(Cursor.Position)
        If ColorListView.SelectedItems.Count = 1 Then
            Dim item As ListViewItem = ColorListView.SelectedItems.Item(0)
            Dim col As Integer = item.SubItems.IndexOf(ColorListView.HitTest(point).SubItem)

            If col = ColumnHeaderName.DisplayIndex Then
                EditColorName(item)
            ElseIf col = ColumnHeaderHex.DisplayIndex OrElse col = ColumnHeaderRgb.DisplayIndex OrElse col = ColumnHeaderView.DisplayIndex Then
                EditColorValue(item)
            End If
        End If
    End Sub

#Region "列表函数"

    Private Sub ColorListView_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ColorListView.SelectedIndexChanged
        ButtonRemove.Enabled = ColorListView.SelectedIndices.Count = 1
    End Sub

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
        If index >= ColorListView.Items.Count
            index = ColorListView.Items.Count - 1
        End If
        If index >= 0 Then
            Dim item = ColorListView.Items(index)
            item.Selected = True
            item.Focused = True
            ColorListView.Select()
        End If
    End Sub

#End Region

    Private Sub refreshSave()
        If SaveFunc IsNot Nothing Then
            SaveFunc.Invoke()
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub
End Class
