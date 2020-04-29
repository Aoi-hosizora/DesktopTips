Public Class ColorDialog
    Public Delegate Sub SaveFunc()

    Public SaveCallback As SaveFunc

    Private Sub ColorDialog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ColorListView.Items.Clear()
        For Each colorItem As TipColor In GlobalModel.Colors
            AddToListView(New TipColor(colorItem))
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
            Dim col As Integer = item.SubItems.IndexOf(ColorListView.HitTest(point).SubItem)

            If col = ColumnHeaderName.DisplayIndex Then
                EditColorName(item)
            ElseIf col = ColumnHeaderHex.DisplayIndex OrElse col = ColumnHeaderRgb.DisplayIndex OrElse col = ColumnHeaderView.DisplayIndex Then
                EditColorValue(item)
            End If
        End If
    End Sub

    Private Sub ButtonAdd_Click(sender As Object, e As EventArgs) Handles ButtonAdd.Click
        Dim title As String = InputBox("新颜色的标签：", "新建", "颜色").Trim()
        If title <> "" Then
            Dim tipColor As New TipColor(ColorListView.Items.Count, title)
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

    Private Sub EditColorName(item As ListViewItem)
        Dim tipColor = CType(item.Tag, TipColor)
        Dim newName = InputBox("修改颜色标签：", "修改", tipColor.Name).Trim()
        If newName <> "" Then
            tipColor.Name = newName
            item.Tag = tipColor
            item.SubItems(ColumnHeaderName.DisplayIndex).Text = newName
            UpdateListViewColumn()
        End If
    End Sub

    Private Sub EditColorValue(item As ListViewItem)
        Dim tipColor = CType(item.Tag, TipColor)
        Dim colorDlg As New Windows.Forms.ColorDialog
        colorDlg.Color = tipColor.Color
        colorDlg.ShowDialog()

        tipColor.Color = colorDlg.Color
        item.Tag = tipColor
        item.SubItems(ColumnHeaderHex.DisplayIndex).Text = tipColor.HexColor
        item.SubItems(ColumnHeaderRgb.DisplayIndex).Text = tipColor.RgbColor
        item.SubItems(ColumnHeaderView.DisplayIndex).BackColor = tipColor.Color
        UpdateListViewColumn()
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
        Dim changed = False
        For Each item As ListViewItem In ColorListView.Items
            Dim colorInList = CType(item.Tag, TipColor)
            Dim colorInModel As TipColor = GlobalModel.Colors.FirstOrDefault(Function (c) c.Id = colorInList.Id)
            If colorInModel Is Nothing Then
                changed = True
                Exit For
            End If
            If colorInList.Name <> colorInModel.Name OrElse colorInList.Color <> colorInModel.Color Then
                changed = True
                Exit For
            End If
        Next

        If changed Then
            Dim ok = MessageBoxEx.Show("颜色设置已修改，返回而不保存？",
                "保存", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, Me, {"保存并返回", "直接返回", "取消"})
            If ok = vbCancel Then
                Return
            ElseIf ok = vbYes AndAlso SaveCallback IsNot Nothing Then
                SaveCallback.Invoke()
            End If
        End If
        Me.Close()
    End Sub
End Class
