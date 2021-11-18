''' <summary>
''' 设置高亮颜色
''' </summary>
Public Class ColorDialog
    ''' <summary>
    ''' 保存时的回调
    ''' </summary>
    Public SaveCallback As Action

    Private Sub ColorDialog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ColorListView.Items.Clear()
        For Each colorItem As TipColor In GlobalModel.Colors
            AddToListView(colorItem)
        Next
        UpdateListViewColumn()
        ButtonRemove.Enabled = False
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Close()
    End Sub

    Private Sub ButtonAdd_Click(sender As Object, e As EventArgs) Handles ButtonAdd.Click
        Dim title As String = InputBox("新颜色的标签：", "新建", "颜色").Trim()
        If title <> "" Then
            Dim id As Integer = ColorListView.Items.Count
            Dim now = DateTime.Now
            Dim tipColor As New TipColor(id, title) With {.CreatedAt = now, .UpdatedAt = now} ' 默认红色、普通样式
            AddToListView(tipColor)
            UpdateListViewColumn()
            SetListViewSelect(id)
            GlobalModel.Colors.Add(tipColor)
            SaveCallback?.Invoke()
        End If
    End Sub

    Private Sub ButtonRemove_Click(sender As Object, e As EventArgs) Handles ButtonRemove.Click
        If ColorListView.SelectedItems.Count <> 1 Then Return
        Dim item As ListViewItem = ColorListView.SelectedItems(0)
        Dim delColor = CType(item.Tag, TipColor)

        Dim ok = MessageBoxEx.Show($"删除所选颜色 ""{delColor.Name}""？", "删除", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, Me)
        If ok <> vbOK Then Return

        ' 删除检查
        Dim tips As IEnumerable(Of TipItem) = GlobalModel.Tabs.SelectMany(Function(t) t.Tips).Where(Function(t) t.ColorId = delColor.Id)
        If tips.Count = 0 Then
            RemoveFromListView(delColor) ' 直接删除并结束
            Return
        End If

        ' 存在相关联标签
        Dim tipString As String = String.Join(vbNewLine, tips.Select(Function(t) t.Content))
        If tipString.Count > 800 Then
            tipString = tipString.Substring(0, 797) & "..."
        End If

        Dim ok2 = MessageBoxEx.Show($"颜色 ""{delColor.Name}"" 拥有以下 {tips.Count} 个已存在的标签，是否删除？{vbNewLine}{vbNewLine}{tipString}",
            "删除", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, Me, {"修改高亮颜色", "直接删除", "取消"})
        If ok2 = vbNo Then
            ' 直接删除
            GlobalModel.Tabs.ForEach(Sub(t) t.Tips.RemoveAll(Function(tip) tip.ColorId = delColor.Id))
            RemoveFromListView(delColor)
        ElseIf ok2 = vbYes Then
            ' 修改高亮颜色
            ColorReplaceDialog.SelectedColor = delColor
            ColorReplaceDialog.AllColors = GlobalModel.Colors.Where(Function(c) c.Id <> delColor.Id).ToList()
            ColorReplaceDialog.OkCallback = Sub(id As Integer)
                                                Dim now = DateTime.Now
                                                For Each tip As TipItem In tips
                                                    tip.ColorId = id
                                                    tip.UpdatedAt = now
                                                Next
                                                RemoveFromListView(delColor)
                                            End Sub
            ColorReplaceDialog.ShowDialog(Me) ' 修改
        End If
    End Sub

    Private Sub ColorListView_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ColorListView.SelectedIndexChanged
        ButtonRemove.Enabled = ColorListView.SelectedIndices.Count = 1
    End Sub

    Private Sub ColorDialog_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        ColorListView.Select()
    End Sub

    ''' <summary>
    ''' 双击编辑标签或者颜色
    ''' </summary>
    Private Sub ColorListView_DoubleClick(sender As Object, e As EventArgs) Handles ColorListView.DoubleClick
        Dim point As Point = ColorListView.PointToClient(Cursor.Position)
        If ColorListView.SelectedItems.Count = 1 Then
            Dim item As ListViewItem = ColorListView.SelectedItems.Item(0)
            Dim col As Integer = item.SubItems.IndexOf(ColorListView.HitTest(point).SubItem)

            If col = ColumnHeaderName.DisplayIndex Then
                EditColorName(item)
            ElseIf col = ColumnHeaderHex.DisplayIndex OrElse col = ColumnHeaderRgb.DisplayIndex Then
                EditColorValue(item)
            ElseIf col = ColumnHeaderStyle.DisplayIndex Then
                EditTextStyle(item)
            End If
        End If
    End Sub

    ''' <summary>
    ''' 高亮 ListView 的指定行
    ''' </summary>
    Private Sub SetListViewSelect(index As Integer)
        For Each i As ListViewItem In ColorListView.Items
            i.Selected = False
            i.Focused = False
        Next
        If index >= ColorListView.Items.Count Then
            index = ColorListView.Items.Count - 1
        End If
        If index >= 0 Then
            Dim item = ColorListView.Items(index)
            item.Selected = True ' <<<
            item.Focused = True
            ColorListView.Select()
        End If
    End Sub

    ''' <summary>
    ''' 调整 ListView 的列宽
    ''' </summary>
    Private Sub UpdateListViewColumn()
        For i = 0 To ColumnHeaderRgb.DisplayIndex
            ColorListView.AutoResizeColumn(i, ColumnHeaderAutoResizeStyle.ColumnContent)
        Next
    End Sub

#Region "添加删除修改"

    ''' <summary>
    ''' 将给定 TipColor 插入到列表，注意没有修改 GlobalModel
    ''' </summary>
    Private Sub AddToListView(c As TipColor)
        Dim item As ListViewItem = ColorListView.Items.Add(c.Id)
        item.Tag = c ' Tag 保存原值
        item.UseItemStyleForSubItems = False
        item.SubItems.Add(c.Name) ' 标签
        item.SubItems.Add(ColorToRgb(c.Color)) ' RGB
        item.SubItems.Add(c.HexColor) ' Hex
        Dim subItem1 As ListViewItem.ListViewSubItem = item.SubItems.Add(StyleToString(c.Style)) ' 样式
        subItem1.Font = New Font(Font, c.Style)
        Dim subItem2 As ListViewItem.ListViewSubItem = item.SubItems.Add("") ' 预览
        subItem2.BackColor = c.Color

        Dim tooltip = $"{c.Name} - {c.HexColor}, {StyleToString(c.Style)}" & vbNewLine
        tooltip &= "创建于 " & If(c.IsDefaultCreatedAt, "未知时间", c.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss")) & vbNewLine
        tooltip &= "更新于 " & If(c.IsDefaultUpdatedAt, "未知时间", c.UpdatedAt.ToString("yyyy-MM-dd HH:mm:ss"))
        item.ToolTipText = tooltip
    End Sub

    ''' <summary>
    ''' 从 GlobalModel 和列表删除 TipColor，注意同时调用了 SaveCallback
    ''' </summary>
    Private Sub RemoveFromListView(delColor As TipColor)
        GlobalModel.Colors.Remove(delColor) ' 删除
        GlobalModel.ReorderColors(GlobalModel.Colors, GlobalModel.Tabs) ' 处理顺序和 id
        SaveCallback?.Invoke()
        ColorDialog_Load(Me, New EventArgs()) ' 再次加载
        SetListViewSelect(delColor.Id) ' 选择下一项
    End Sub

    ''' <summary>
    ''' 编辑颜色标签，修改列表显示和 GlobalModel
    ''' </summary>
    Private Sub EditColorName(item As ListViewItem)
        Dim currTipColor = CType(item.Tag, TipColor)
        Dim newName = InputBox("修改颜色标签：", "修改", currTipColor.Name).Trim()
        If newName <> "" Then
            currTipColor.Name = newName
            currTipColor.UpdatedAt = DateTime.Now
            item.Tag = currTipColor
            item.SubItems(ColumnHeaderName.DisplayIndex).Text = newName
            UpdateListViewColumn()

            GlobalModel.Colors(currTipColor.Id).Name = newName ' 修改
            SaveCallback?.Invoke()
        End If
    End Sub

    ''' <summary>
    ''' 编辑颜色值，修改列表显示和 GlobalModel
    ''' </summary>
    Private Sub EditColorValue(item As ListViewItem)
        Dim tipColor = CType(item.Tag, TipColor)
        Dim colorDlg As New Windows.Forms.ColorDialog
        colorDlg.Color = tipColor.Color
        colorDlg.ShowDialog()
        Dim newColor As Color = colorDlg.Color

        tipColor.Color = newColor
        tipColor.UpdatedAt = DateTime.Now
        item.Tag = tipColor
        item.SubItems(ColumnHeaderHex.DisplayIndex).Text = tipColor.HexColor
        item.SubItems(ColumnHeaderRgb.DisplayIndex).Text = ColorToRgb(tipColor.Color)
        item.SubItems(ColumnHeaderView.DisplayIndex).BackColor = tipColor.Color
        UpdateListViewColumn()

        GlobalModel.Colors(tipColor.Id).Color = newColor ' 修改
        SaveCallback?.Invoke()
    End Sub

    ''' <summary>
    ''' 编辑文字样式，修改列表显示和 GlobalModel
    ''' </summary>
    Private Sub EditTextStyle(item As ListViewItem)
        Dim tipColor = CType(item.Tag, TipColor)
        Dim style As FontStyle = ColorStyleDialog.ShowDialog(tipColor.Style)

        tipColor.Style = style
        item.Tag = tipColor
        item.SubItems(ColumnHeaderStyle.DisplayIndex).Text = StyleToString(style)
        item.SubItems(ColumnHeaderStyle.DisplayIndex).Font = New Font(Font, style)
        UpdateListViewColumn()

        GlobalModel.Colors(tipColor.Id).Style = style ' 修改
        SaveCallback?.Invoke()
    End Sub

    ''' <summary>
    ''' Color 转 RGB 字符串
    ''' </summary>
    Private Function ColorToRgb(c As Color) As String
        Return String.Format("{0}, {1}, {2}", c.R, c.G, c.B)
    End Function

    Private Function StyleToString(style As FontStyle) As String
        If style = FontStyle.Regular Then
            Return "常规"
        Else
            Dim s = ""
            If (style And FontStyle.Bold) > 0 Then
                s &= "Bo"
            End If
            If (style And FontStyle.Italic) > 0 Then
                s &= "It"
            End If
            If (style And FontStyle.Underline) > 0 Then
                s &= "Un"
            End If
            Return s
        End If
    End Function

#End Region
End Class
