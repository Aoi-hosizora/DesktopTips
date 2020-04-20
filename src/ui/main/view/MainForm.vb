Imports System.IO
Imports System.Text
Imports DD = DevComponents.DotNetBar

Public Class MainForm

#Region "加载设置列表 启动退出程序"

    ''' <summary>
    ''' 加载设置，应用到UI
    ''' MainForm_Load 用
    ''' </summary>
    Public Sub LoadSetting()
        Dim setting As SettingUtil.AppSetting = _globalPresenter.LoadSetting()

        Me.Top = setting.Top
        Me.Left = setting.Left
        Me.Height = setting.Height
        Me.Width = setting.Width
        Me.MaxOpacity = setting.MaxOpacity
        Me.TopMost = setting.TopMost

        ListPopupMenuFold.Checked = setting.IsFold
        ListPopupMenuWinHighColor.SelectedColor = setting.HighLightColor
        ListPopupMenuLoadPos.Enabled = Not (setting.SaveLeft = -1 Or setting.SaveTop = -1)
        NumericUpDownListCnt.Value = (Me.Height - 27) \ 17
        RegisterShotcut(setting.HotKey)
    End Sub

    ''' <summary>
    ''' 保存设置
    ''' MainForm_FormClosed 用
    ''' </summary>
    Public Sub SaveSetting()
        Dim setting As SettingUtil.AppSetting = _globalPresenter.LoadSetting()

        setting.Top = Me.Top
        setting.Left = Me.Left
        setting.Height = Me.Height
        setting.Width = Me.Width
        setting.MaxOpacity = Me.MaxOpacity
        setting.TopMost = Me.TopMost
        setting.IsFold = ListPopupMenuFold.Checked
        setting.HighLightColor = ListPopupMenuWinHighColor.SelectedColor

        _globalPresenter.SaveSetting(setting)
    End Sub

    ''' <summary>
    ''' 加载文件，显示当前分组
    ''' MainForm_Load TabStrip_SelectedTabChanged 用
    ''' </summary>
    Private Sub LoadList()
        _globalPresenter.LoadList() ' GlobalModel 中

        ListView.Items.Clear()
        For Each Tip As TipItem In GlobalModel.Tabs.Item(GlobalModel.CurrTabIdx).Tips
            ListView.Items.Add(Tip)
        Next
        LabelNothing.Visible = ListView.Items.Count = 0
    End Sub

    ''' <summary>
    ''' 保存到文件
    ''' MainForm_FormClosed 增删改 用
    ''' </summary>
    Private Sub SaveList()
        _globalPresenter.SaveList(ListView.Items)
    End Sub

    ''' <summary>
    ''' 新建分组显示
    ''' MainForm_Load TabPopupMenuNewTab_Click 用
    ''' </summary>
    Private Sub AddShownTab(ByVal title As String)
        Dim newTabItem = New DD.SuperTabItem() With {
            .GlobalItem = False,
            .Name = "TabItemCustom_" & GlobalModel.Tabs.Count,
            .Text = title
        }
        AddHandler newTabItem.MouseDown, AddressOf TabStrip_MouseDown
        Me.TabStrip.Tabs.AddRange(New DD.BaseItem() {newTabItem})
    End Sub

    Private Sub ButtonExit_Click(sender As System.Object, e As System.EventArgs) Handles ButtonCloseForm.Click, ListPopupMenuExit.Click
        Dim ok = MessageBox.Show("确定退出 DesktopTips 吗？",
                                 "关闭", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
        If ok = vbYes Then
            TimerMouseIn.Enabled = False
            TimerMouseOut.Enabled = False
            TimerEndForm.Enabled = True
        End If
    End Sub

    Private Sub MainForm_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        SaveList()
        SaveSetting()
        UnregisterShotcut()
    End Sub

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' 加载设置
        LoadSetting()
        Me.Top = Me.Top - (MaxOpacity / OpacitySpeed)
        Me.Refresh()

        ' 窗口显示
        TimerShowForm.Enabled = True
        TimerShowForm.Start()

        ListPopupMenuWinTop.Checked = Me.TopMost
        ButtonRemoveItem.Enabled = False
        Me.TabStrip.Tabs.Remove(Me.TabItemTest)
        Me.TabStrip.Tabs.Remove(Me.TabItemTest2)

        FoldMenu(ListPopupMenuFold.Checked)

        ' 窗口动画
        SetupMouseEnterLeave()
        SetupUpDownButtonsLayout()
        FormOpacity_Load()

        ' 列表
        LoadList()
        For i = 0 To GlobalModel.Tabs.Count - 1
            AddShownTab(GlobalModel.Tabs.Item(i).Title)
        Next
    End Sub

#End Region

    '' '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '' ''''''''''''''''''''''''''''''''''''''''''''''''' 列表 数据 操作 '''''''''''''''''''''''''''''''''''''''''''''''''

#Region "增删改 移动 置顶"

    ''' <summary>
    ''' 增 ButtonAddItem ListPopupMenuAddItem
    ''' </summary>
    Private Sub ButtonAddItem_Click(sender As System.Object, e As System.EventArgs) Handles ButtonAddItem.Click, ListPopupMenuAddItem.Click, LabelNothing.DoubleClick
        Dim msg As String = InputBox("新的提醒标签：", "添加")
        If msg <> "" Then
            ListView.Items.Add(New TipItem(msg.Trim()))
            ListView.SetSelected(0, True)
            ListView.ClearSelected()
            ListView.SetSelected(ListView.Items.Count() - 1, True)
            SaveList()
        End If
    End Sub

    ''' <summary>
    ''' 删 ButtonRemoveItem ListPopupMenuRemoveItem
    ''' </summary>
    Private Sub ButtonRemoveItem_Click(sender As System.Object, e As System.EventArgs) Handles ButtonRemoveItem.Click, ListPopupMenuRemoveItem.Click
        If ListView.SelectedItem IsNot Nothing Then
            Dim SelectItemIdics As New List(Of TipItem)(ListView.SelectedItems.Cast(Of TipItem)())

            Dim sb As New StringBuilder
            For Each item As TipItem In ListView.SelectedItems.Cast(Of TipItem)()
                sb.AppendLine(item.TipContent)
            Next
            Dim ok As Integer = MessageBoxEx.Show("确定删除以下 " & SelectItemIdics.Count & " 个提醒标签吗？" & Chr(10) & Chr(10) & sb.ToString, "删除", MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, Me)
            If (ok = vbOK) Then
                For Each item As TipItem In SelectItemIdics
                    ListView.Items.Remove(item)
                Next
                SaveList()
                ListView.Refresh()
            End If
        End If
    End Sub

    ''' <summary>
    ''' 改 ListView.DoubleClick ListPopupMenuEditItem
    ''' </summary>
    Private Sub ListView_DoubleClick(sender As Object, e As System.EventArgs) Handles ListView.DoubleClick, ListPopupMenuEditItem.Click
        Dim tmpIdx As Integer = ListView.SelectedIndex
        If ListView.SelectedItem IsNot Nothing AndAlso ListView.SelectedIndices.Count = 1 Then
            Dim tip As TipItem = CType(ListView.SelectedItem, TipItem)
            Dim newstr As String = InputBox("修改提醒标签 """ & tip.TipContent & """ 为：", "修改", tip.TipContent)
            If newstr <> "" Then
                tip.TipContent = newstr.Trim()
                ListView.Items(tmpIdx) = tip
                ListView.SelectedIndex = tmpIdx
                SaveList()
            End If
        Else
            ButtonAddItem_Click(sender, New System.EventArgs)
        End If
    End Sub

    ''' <summary>
    ''' 置顶
    ''' </summary>
    Private Sub ListPopupMenuMoveTop_Click(sender As System.Object, e As System.EventArgs) Handles ListPopupMenuMoveTop.Click
        Dim currItem As Object = ListView.SelectedItem
        ListView.Items.Remove(currItem)
        ListView.Items.Insert(0, currItem)
        ListView.SetSelected(0, True)
        SaveList()
    End Sub

    ''' <summary>
    ''' 置底
    ''' </summary>
    Private Sub ListPopupMenuMoveBottom_Click(sender As System.Object, e As System.EventArgs) Handles ListPopupMenuMoveBottom.Click
        'Dim currIdx As Integer = ListView.SelectedIndex
        Dim currItem As Object = ListView.SelectedItem
        ListView.Items.Remove(currItem)
        ListView.Items.Insert(ListView.Items.Count, currItem)
        ListView.SetSelected(ListView.Items.Count - 1, True)
        SaveList()
    End Sub

    ''' <summary>
    ''' 上移
    ''' </summary>
    Private Sub MoveUp_Click(sender As System.Object, e As System.EventArgs) Handles ListPopupMenuMoveUp.Click, ButtonItemUp.Click
        Dim currIdx As Integer = ListView.SelectedIndex
        If currIdx >= 1 Then
            Dim currItem As Object = ListView.SelectedItem
            ListView.Items.Remove(currItem)
            ListView.Items.Insert(currIdx - 1, currItem)
            ListView.SetSelected(currIdx - 1, True)
            If sender.Tag = "True" Then
                Dim dx As Integer = Cursor.Position.X * UInt16.MaxValue / My.Computer.Screen.Bounds.Width
                Dim dy As Integer = (Cursor.Position.Y - 17) * UInt16.MaxValue / My.Computer.Screen.Bounds.Height
                NativeMethod.mouse_event(NativeMethod.MouseEvent.MOUSEEVENTF_MOVE Or NativeMethod.MouseEvent.MOUSEEVENTF_ABSOLUTE, dx, dy, 0, 0)
            End If
            SaveList()
        End If
    End Sub

    ''' <summary>
    ''' 下移
    ''' </summary>
    Private Sub MoveDown_Click(sender As System.Object, e As System.EventArgs) Handles ListPopupMenuMoveDown.Click, ButtonItemDown.Click
        Dim currIdx As Integer = ListView.SelectedIndex
        If currIdx <= ListView.Items.Count() - 2 Then
            Dim currItem As Object = ListView.SelectedItem
            ListView.Items.Remove(currItem)
            ListView.Items.Insert(currIdx + 1, currItem)
            ListView.SetSelected(currIdx + 1, True)
            If sender.Tag = "True" Then
                Dim dx As Integer = Cursor.Position.X * UInt16.MaxValue / My.Computer.Screen.Bounds.Width
                Dim dy As Integer = (Cursor.Position.Y + 17) * UInt16.MaxValue / My.Computer.Screen.Bounds.Height
                NativeMethod.mouse_event(NativeMethod.MouseEvent.MOUSEEVENTF_MOVE Or NativeMethod.MouseEvent.MOUSEEVENTF_ABSOLUTE, dx, dy, 0, 0)
            End If
            SaveList()
        End If
    End Sub

#End Region

#Region "复制 全选 查找 粘贴"

    ''' <summary>
    ''' 复制
    ''' </summary>
    Private Sub ListPopupMenuCopy_Click(sender As System.Object, e As System.EventArgs) Handles ListPopupMenuCopy.Click
        Dim Sb As New StringBuilder
        For Each item As TipItem In ListView.SelectedItems.Cast(Of TipItem)()
            Sb.AppendLine(item.TipContent)
        Next
        Clipboard.SetText(Sb.ToString())
    End Sub

    ''' <summary>
    ''' 全选
    ''' </summary>
    Private Sub ListPopupMenuSelectAll_Click(sender As System.Object, e As System.EventArgs) Handles ListPopupMenuSelectAll.Click
        For i = 0 To ListView.Items.Count - 1
            ListView.SetSelected(i, True)
        Next
    End Sub

    ''' <summary>
    ''' 查找文字
    ''' </summary>
    Public Sub ListPopupMenuFind_Click(sender As System.Object, e As System.EventArgs) Handles ListPopupMenuFind.Click
        Dim SearchResult As New List(Of Tuple(Of Integer, Integer))
        Dim SearchText$ = InputBox("请输入查找的文字：", "查找").Trim()

        If SearchText <> "" Then
            Dim spl() As String = SearchText.Split(" ")
            For Each Tab As Tab In GlobalModel.Tabs
                For Each Tip As TipItem In Tab.Tips
                    If Tip.TipContent.ToLower.Contains(SearchText.ToLower) Then
                        SearchResult.Add(New Tuple(Of Integer, Integer)(GlobalModel.Tabs.IndexOf(Tab), Tab.Tips.IndexOf(Tip)))
                    End If
                Next
            Next

            SearchDialog.Close()
            If SearchResult.Count = 0 Then
                MessageBoxEx.Show("未找到 """ & SearchText & """ 。", "查找", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, Me)
            Else
                SearchDialog._SearchText = SearchText
                SearchDialog._SearchResult = SearchResult
                SearchDialog.Show(Me)
            End If
        End If
    End Sub

    ''' <summary>
    ''' 粘贴附加在最后
    ''' </summary>
    Private Sub ListPopupMenuPasteAppend_Click(sender As System.Object, e As System.EventArgs) Handles ListPopupMenuPasteAppend.Click
        Dim clip$ = Clipboard.GetText()
        If Not String.IsNullOrWhiteSpace(clip) Then
            Dim currIdx% = ListView.SelectedIndex

            Dim tip As TipItem = GlobalModel.Tabs(GlobalModel.CurrTabIdx).Tips(currIdx)
            Dim motoStr$ = tip.TipContent

            Dim ok As DialogResult = MessageBoxEx.Show(
                "是否向当前选中项 """ & GlobalModel.Tabs(GlobalModel.CurrTabIdx).Tips(currIdx).TipContent & """ 末尾添加剪贴板内容 """ & clip & """？",
                "附加内容",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, Me)

            If ok = Windows.Forms.DialogResult.OK Then
                tip.TipContent += " " & Clipboard.GetText().Trim()
                SaveList()
            End If
        End If
    End Sub

#End Region

#Region "高亮 列表绘制"

    ''' <summary>
    ''' 高亮，可多选
    ''' </summary>
    Private Sub ListPopupMenuHighLight_Click(sender As System.Object, e As System.EventArgs) Handles ListPopupMenuHighLight.Click
        Dim IsHighLight As Boolean = CType(ListView.SelectedItems(0), TipItem).IsHighLight
        For Each Item As TipItem In ListView.SelectedItems
            Item.IsHighLight = Not IsHighLight
        Next
        SaveList()
        ListView.Refresh()
    End Sub

    ''' <summary>
    ''' 高亮颜色修改
    ''' </summary>
    Private Sub ListPopupMenuWinHighColor_SelectedColorChanged(sender As System.Object, e As System.EventArgs) Handles ListPopupMenuWinHighColor.SelectedColorChanged
        ListView.Refresh()
    End Sub

    ''' <summary>
    ''' 重写绘制 高亮
    ''' </summary>
    Private Sub ListView_DrawItem(sender As Object, e As System.Windows.Forms.DrawItemEventArgs) Handles ListView.DrawItem
        If e.Index <> -1 Then
            e.DrawBackground()
            e.DrawFocusRectangle()

            Dim NowItem = ListView.Items(e.Index)
            Dim ColoredBrush As SolidBrush = New SolidBrush(Color.Black)

            If CType(NowItem, TipItem).IsHighLight Then
                ColoredBrush = New SolidBrush(ListPopupMenuWinHighColor.SelectedColor)
            Else
                ColoredBrush = New SolidBrush(e.ForeColor)
            End If

            e.Graphics.DrawString(NowItem.ToString, e.Font, ColoredBrush, e.Bounds, StringFormat.GenericDefault)
        End If
    End Sub

    ''' <summary>
    ''' 显示高亮部分
    ''' </summary>
    Private Sub ListPopupMenuHighLightList_Click(sender As System.Object, e As System.EventArgs) Handles ListPopupMenuViewHighLight.Click
        Dim sb As New StringBuilder
        Dim idx As Integer = 0
        For Each Item As TipItem In ListView.Items.Cast(Of TipItem)()
            If Item.IsHighLight Then
                sb.AppendLine(Item.TipContent)
                idx += 1
            End If
        Next
        ShowTextForm("查看高亮 (共 " & idx & " 项)", sb.ToString, ListPopupMenuWinHighColor.SelectedColor)
    End Sub

#End Region

#Region "打开文件 浏览文件 浏览器"

    ''' <summary>
    ''' 打开文件所在位置
    ''' </summary>
    Private Sub ListPopupMenuOpenFile_Click(sender As System.Object, e As System.EventArgs) Handles ListPopupMenuOpenDir.Click
        'Dim path As String = FileDir
        'Process.Start(path)
        'C:\Users\Windows 10\AppData\Roaming\DesktopTips

        System.Diagnostics.Process.Start("explorer.exe", "/select,""" & GlobalModel.STORAGE_FILENAME & """")
    End Sub

    ''' <summary>
    ''' 浏览文件
    ''' </summary>
    Private Sub ListPopupMenuViewFile_Click(sender As System.Object, e As System.EventArgs) Handles ListPopupMenuViewFile.Click
        Dim sb As New StringBuilder
        For Each Item As TipItem In ListView.Items.Cast(Of TipItem)()
            sb.AppendLine(Item.TipContent & If(Item.IsHighLight, " [高亮]", ""))
        Next
        ShowTextForm("浏览文件 (共 " & ListView.Items.Count & " 项)", sb.ToString(), Color.Black)
    End Sub

    ''' <summary>
    ''' 获取当前列表所选中的所有链接
    ''' </summary>
    Private Function GetSelectionItemLinks() As List(Of String)
        Dim IsSingle As Boolean = ListView.SelectedItems.Count = 1

        ' 整合
        Dim Sel As List(Of TipItem) = New List(Of TipItem)
        If IsSingle Then
            Sel.Add(CType(ListView.SelectedItem, TipItem))
        Else
            Sel = ListView.SelectedItems.Cast(Of TipItem).ToList()
        End If

        ' 结果
        Dim links As List(Of String) = New List(Of String)
        For Each item As TipItem In Sel
            For Each link As String In item.TipContent.Split(New Char() {" "}, StringSplitOptions.RemoveEmptyEntries)
                If link.StartsWith("http://") Or link.StartsWith("https://") Then
                    links.Add(link)
                End If
            Next
        Next

        Return links
    End Function

    ''' <summary>
    ''' 打开所有链接
    ''' </summary>
    Private Sub ListPopupMenuOpenAllLink_Click(sender As System.Object, e As System.EventArgs) Handles ListPopupMenuOpenAllLink.Click
        Dim links As List(Of String) = GetSelectionItemLinks()
        If links.Count = 0 Then
            MessageBox.Show("所选项不包含任何链接。", "打开链接", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            Dim ok As MessageBoxButtons = MessageBox.Show( _
                "是否打开以下 " & links.Count & " 个链接：" + Chr(10) + Chr(10) + String.Join(Chr(10), links), _
                "打开链接", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
            If ok = MsgBoxResult.Ok Then
                CommonUtil.OpenWebsInDefaultBrowser(links)
            End If
        End If
    End Sub

    ''' <summary>
    ''' 打开部分连接
    ''' </summary>
    Private Sub ListPopupMenuViewAllLink_Click(sender As System.Object, e As System.EventArgs) Handles ListPopupMenuViewAllLink.Click
        Dim links As List(Of String) = GetSelectionItemLinks()

        LinkDialog.ListView.Items.Clear()
        For Each link$ In links
            LinkDialog.ListView.Items.Add(link)
        Next
        LinkDialog.Show(Me)
    End Sub

#End Region

    '' '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '' ''''''''''''''''''''''''''''''''''''''''''''''''' 界面 显示 菜单 '''''''''''''''''''''''''''''''''''''''''''''''''

#Region "文本窗口"

    ''' <summary>
    ''' 显示文本窗体
    ''' </summary>
    ''' <param name="Title">窗口标题</param>
    ''' <param name="Content">窗口文本内容</param>
    ''' <param name="TextColor">文字颜色</param>
    Private Sub ShowTextForm(ByVal Title As String, ByVal Content As String, ByVal TextColor As Color)
        Dim WinSize As Size = New Size(500, 300)
        Dim TextSize As Size = New Size(WinSize.Width - 16, WinSize.Height - 39)

        Dim TextBox As New TextBox With {.Text = Content, .ReadOnly = True, .Multiline = True, .ScrollBars = ScrollBars.Both, .WordWrap = False, _
                                         .Size = TextSize, .BackColor = Color.White, .ForeColor = TextColor, .Font = New System.Drawing.Font("Microsoft YaHei UI", 9.0!), _
                                         .Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Top}

        Dim Win As New EscCloseForm With {.FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable, .Text = Title, .Size = WinSize, .TopMost = True}
        Win.Controls.Add(TextBox)
        Win.Show()

        Win.Top = Me.Top
        Win.Left = Me.Left - WinSize.Width - 15
        TextBox.Select(0, 0)
    End Sub

#End Region

#Region "列表选择 可用判断 屏幕提示 右键列表"

    ''' <summary>
    ''' 列表选择 -> 辅助按钮显示 可用判断
    ''' </summary>
    Private Sub ListView_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles ListView.SelectedIndexChanged
        If ListView.SelectedIndices.Count = 1 Then
            SetSelectedItemButtonShow(ListView.GetItemRectangle(ListView.SelectedIndex))
        Else
            SetSelectedItemButtonHide()
        End If

        CheckListPopupEnable()
        ListView.Refresh()
    End Sub

    ''' <summary>
    ''' 选择可用性
    ''' ListView_SelectedIndexChanged 和 ButtonListSetting_Click 用
    ''' </summary>
    Private Sub CheckListPopupEnable()
        Dim IsNotNull As Boolean = ListView.SelectedIndex <> -1
        Dim IsSingle As Boolean = ListView.SelectedIndices.Count = 1
        Dim IsTop As Boolean = ListView.SelectedIndex = 0
        Dim IsBottom As Boolean = ListView.SelectedIndex = ListView.Items.Count() - 1

        ' 辅助按钮
        ButtonItemUp.Enabled = IsNotNull And IsSingle And Not IsTop
        ButtonItemDown.Enabled = IsNotNull And IsSingle And Not IsBottom

        ' 位置按钮
        ListPopupMenuMoveUp.Enabled = IsNotNull And IsSingle And Not IsTop
        ListPopupMenuMoveTop.Enabled = IsNotNull And IsSingle And Not IsTop
        ListPopupMenuMoveBottom.Enabled = IsNotNull And IsSingle And Not IsBottom
        ListPopupMenuMoveDown.Enabled = IsNotNull And IsSingle And Not IsBottom

        ' 删改复制
        ButtonRemoveItem.Enabled = IsNotNull
        ListPopupMenuRemoveItem.Enabled = IsNotNull
        ListPopupMenuEditItem.Enabled = IsSingle
        ListPopupMenuHighLight.Enabled = IsNotNull
        ListPopupMenuCopy.Enabled = IsNotNull

        If IsNotNull Then
            ListPopupMenuHighLight.Checked = CType(ListView.SelectedItem, TipItem).IsHighLight
        Else
            ListPopupMenuHighLight.Checked = False
        End If

        ' 移动
        ListPopupMenuMove.Enabled = IsNotNull

        ' 附加
        ListPopupMenuPasteAppend.Enabled = IsSingle

        ' 浏览器
        If IsSingle Then
            Dim item As TipItem = CType(ListView.SelectedItem, TipItem)
            ListPopupMenuBrowser.Enabled = item.TipContent.IndexOf("http://") <> -1 Or item.TipContent.IndexOf("https://") <> -1
        Else
            Dim ok As Boolean = False
            For Each item As TipItem In ListView.SelectedItems.Cast(Of TipItem)()
                If item.TipContent.IndexOf("http://") <> -1 Or item.TipContent.IndexOf("https://") <> -1 Then
                    ok = True
                    Exit For
                End If
            Next
            ListPopupMenuBrowser.Enabled = ok
        End If
    End Sub

    ''' <summary>
    ''' ListView_MouseMoveHover 用
    ''' </summary>
    Dim HoverIdx As Integer = -1

    ''' <summary>
    ''' 悬浮弹出提示
    ''' </summary>
    Private Sub ListView_MouseMove(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles ListView.MouseMove
        Dim Idx = ListView.IndexFromPoint(e.Location)
        If HoverIdx <> Idx AndAlso Idx <> -1 AndAlso Idx < ListView.Items.Count Then
            HoverToolTip.Hide(ListView)
            HoverToolTip.SetToolTip(ListView, CType(ListView.Items.Item(Idx), TipItem).TipContent)
            HoverIdx = Idx
        End If
    End Sub

    ''' <summary>
    ''' 右键列表同时选中项
    ''' </summary>
    Private Sub ListView_RightMouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles ListView.MouseUp
        Dim idx As Integer = ListView.IndexFromPoint(e.X, e.Y)
        If e.Button = Windows.Forms.MouseButtons.Right AndAlso idx <> -1 AndAlso idx < ListView.Items.Count Then
            If ListView.SelectedIndices.Count <= 1 Then
                ListView.ClearSelected()
                ListView.SetSelected(idx, True)

                If ListView.Items.Count > 0 Then
                    Dim rect As Rectangle = ListView.GetItemRectangle(ListView.Items.Count - 1)
                    If e.Y > rect.Top + rect.Height Then ListView.ClearSelected()
                End If
            End If
        End If
    End Sub

#End Region

#Region "辅助按钮"

    ''' <summary>
    ''' 设置辅助按钮布局
    ''' </summary>
    Private Sub SetupUpDownButtonsLayout()
        Dim height As Integer = ListView.ItemHeight

        ButtonItemUp.Visible = False
        ButtonItemUp.Height = (height + 1) / 2
        ButtonItemUp.Width = height

        ButtonItemDown.Visible = False
        ButtonItemDown.Height = (height + 1) / 2
        ButtonItemDown.Width = height
    End Sub

    ''' <summary>
    ''' 显示辅助按钮
    ''' </summary>
    ''' <param name="rect">ListView.GetItemRectangle(ListView.SelectedIndex)</param>
    Private Sub SetSelectedItemButtonShow(ByVal rect As Rectangle)
        rect.Offset(ListView.Location)
        rect.Offset(2, 2)

        ButtonItemUp.Top = rect.Top
        ButtonItemUp.Left = rect.Left + rect.Width - ButtonItemDown.Width
        ButtonItemUp.Visible = True

        ButtonItemDown.Top = ButtonItemUp.Top + ButtonItemUp.Height - 1
        ButtonItemDown.Left = ButtonItemUp.Left
        ButtonItemDown.Visible = True
    End Sub

    ''' <summary>
    ''' 隐藏辅助按钮
    ''' </summary>
    Private Sub SetSelectedItemButtonHide()
        ButtonItemUp.Visible = False
        ButtonItemDown.Visible = False
    End Sub

    ''' <summary>
    ''' 辅助按钮位置调整
    ''' </summary>
    Private Sub ListView_SizeChanged(sender As Object, e As System.EventArgs) Handles ListView.SizeChanged
        ' ListView.Refresh()
        If ButtonItemUp.Visible = True AndAlso ListView.SelectedIndices.Count = 1 Then
            Dim Rect As Rectangle = ListView.GetItemRectangle(ListView.SelectedIndex)
            SetSelectedItemButtonShow(Rect)
        End If
    End Sub

#End Region

#Region "弹出菜单 调整列表高度"

    ''' <summary>
    ''' 显示菜单标题
    ''' </summary>
    Private Sub ListPopMenu_PopupOpen(sender As Object, e As DD.PopupOpenEventArgs) Handles ListPopupMenu.PopupOpen
        e.Cancel = True
        ListPopupMenuLabelSelItem.Visible = ListView.SelectedIndex <> -1
        ListPopupMenuLabelSelItemText.Visible = ListView.SelectedIndex <> -1

        ListPopupMenuLabelSelItem.Text = "当前选中 (共 " & ListView.SelectedIndices.Count & " 项)"

        Dim sb As New StringBuilder
        For Each item As TipItem In ListView.SelectedItems.Cast(Of TipItem)()
            sb.AppendLine(item.TipContent)
        Next
        ListPopupMenuLabelSelItemText.Text = sb.ToString

        Dim HLItemCnt As Integer = 0
        For Each Tip As TipItem In GlobalModel.Tabs.Item(GlobalModel.CurrTabIdx).Tips
            HLItemCnt += If(Tip.IsHighLight, 1, 0)
        Next
        ListPopupMenuLabelItemList.Text = "列表 (共 " & ListView.Items.Count & " 项，高亮 " & HLItemCnt & " 项)"

        e.Cancel = False
        ListPopupMenu.Refresh()
    End Sub

    ''' <summary>
    ''' 显示弹出菜单
    ''' </summary>
    Private Sub ButtonListSetting_Click(sender As System.Object, e As System.EventArgs) Handles ButtonListSetting.Click
        CheckListPopupEnable()
        ListPopupMenu.Popup(Me.Left + sender.Left, Me.Top + sender.Top + sender.Height - 1)
    End Sub

    ''' <summary>
    ''' Popup 显示调整大小
    ''' </summary>
    Private Sub ListPopupMenuListHeight_Click(sender As System.Object, e As System.EventArgs) Handles ListPopupMenuListHeight.Click
        NumericUpDownListCnt.Visible = ListPopupMenuListHeight.Checked
    End Sub

    ''' <summary>
    ''' 右键 Setting 显示调整大小
    ''' </summary>
    Private Sub ButtonSetting_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles ButtonListSetting.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then
            ListPopupMenuListHeight.Checked = Not ListPopupMenuListHeight.Checked
            ListPopupMenuListHeight_Click(sender, New System.EventArgs)
        End If
    End Sub

    ''' <summary>
    ''' Popup 调整大小
    ''' </summary>
    Private Sub NumericUpDownListCnt_ValueChanged(sender As System.Object, e As System.EventArgs) Handles NumericUpDownListCnt.ValueChanged
        Dim MoveY As Integer
        If Me.Height < NumericUpDownListCnt.Value * ListView.ItemHeight + 27 Then
            MoveY = 17
        ElseIf Me.Height > NumericUpDownListCnt.Value * ListView.ItemHeight + 27 Then
            MoveY = -17
        End If
        Me.Height = NumericUpDownListCnt.Value * ListView.ItemHeight + 27

        Dim dx As Integer = Cursor.Position.X * UInt16.MaxValue / My.Computer.Screen.Bounds.Width
        Dim dy As Integer = (Cursor.Position.Y + MoveY) * UInt16.MaxValue / My.Computer.Screen.Bounds.Height

        NativeMethod.mouse_event(NativeMethod.MouseEvent.MOUSEEVENTF_MOVE Or NativeMethod.MouseEvent.MOUSEEVENTF_ABSOLUTE, dx, dy, 0, 0)
    End Sub

#End Region

#Region "置顶 加载位置 保存位置"

    ''' <summary>
    ''' Popup 置顶
    ''' </summary>
    Private Sub ListPopupMenuWinTop_Click(sender As System.Object, e As System.EventArgs) Handles ListPopupMenuWinTop.Click
        ListPopupMenuWinTop.Checked = Not ListPopupMenuWinTop.Checked
        Me.TopMost = ListPopupMenuWinTop.Checked
    End Sub

    ''' <summary>
    ''' Popup 加载位置
    ''' </summary>
    Private Sub ListPopupMenuLoadPos_Click(sender As System.Object, e As System.EventArgs) Handles ListPopupMenuLoadPos.Click
        Dim setting As SettingUtil.AppSetting = SettingUtil.LoadAppSettings()
        Me.Top = setting.SaveTop
        Me.Left = setting.SaveLeft
    End Sub

    ''' <summary>
    ''' Popup 保存位置
    ''' </summary>
    Private Sub ListPopupMenuSavePos_Click(sender As System.Object, e As System.EventArgs) Handles ListPopupMenuSavePos.Click

        Dim ok As DialogResult =
            MessageBox.Show("确定保存当前位置，注意该操作会覆盖之前保存的窗口位置。",
                            "保存位置",
                            MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)

        If ok = vbOK Then
            Dim setting As SettingUtil.AppSetting = SettingUtil.LoadAppSettings()
            setting.SaveTop = Me.Top
            setting.SaveLeft = Me.Left
            SettingUtil.SaveAppSettings(setting)

            ListPopupMenuLoadPos.Enabled = True
        End If
    End Sub

#End Region

#Region "快捷键设置 响应"

    ''' <summary>
    ''' RegisterHotKey 注册的热键 Id
    ''' </summary>
    ''' <remarks></remarks>
    Private Const HotKeyId As Integer = 0

    ''' <summary>
    '''注册快捷键
    ''' </summary>
    Public Function RegisterShotcut(ByVal HotKey As Keys) As Boolean
        If Not NativeMethod.RegisterHotKey( _
            Handle, HotKeyId, _
            CommonUtil.GetNativeModifiers(CommonUtil.GetModifiersFromKey(HotKey)), _
            CommonUtil.GetKeyCodeFromKey(HotKey)) Then

            MessageBox.Show("快捷键已被占用，请重新设置", "快捷键", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' 注销快捷键
    ''' </summary>
    Public Sub UnregisterShotcut()
        NativeMethod.UnregisterHotKey(Handle, HotKeyId)
    End Sub

    ''' <summary>
    ''' 设置快捷键响应
    ''' </summary>
    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        If m.Msg = NativeMethod.WM_HOTKEY Then
            If m.WParam.ToInt32() = HotKeyId Then
                Me.Activate()
                Me.TopMost = True
                Me.TopMost = False
                TimerMouseIn.Enabled = True
                TimerMouseIn.Start()
            End If
        End If
        MyBase.WndProc(m)
    End Sub

    ''' <summary>
    ''' Popup 快捷键设置
    ''' </summary>
    Private Sub ListPopupMenuShotcutSetting_Click(sender As System.Object, e As System.EventArgs) Handles ListPopupMenuShotcutSetting.Click
        Dim setting As SettingUtil.AppSetting = SettingUtil.LoadAppSettings()
        HotKeyDialog.HotKeyEditBox.CurrentKey = setting.HotKey
        HotKeyDialog.CheckBoxIsValid.Checked = setting.IsUseHotKey
        HotKeyDialog.ShowDialog(Me)
    End Sub

#End Region

    '' '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '' ''''''''''''''''''''''''''''''''''''''''''''''''''' 分组 移动 '''''''''''''''''''''''''''''''''''''''''''''''''''

#Region "分组 增删改"

    ''' <summary>
    ''' 新建分组
    ''' </summary>
    Private Sub TabPopupMenuNewTab_Click(sender As System.Object, e As System.EventArgs) Handles TabPopupMenuNewTab.Click
        Dim tabName As String = InputBox("请输入新分组的标题: ", "新建", "分组")
        tabName = tabName.Trim()
        If tabName <> "" Then
            If Tab.CheckDuplicateTab(tabName, GlobalModel.Tabs) IsNot Nothing Then
                MessageBoxEx.Show("分组标题 """ & tabName & """ 已存在。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, Me)
                TabPopupMenuNewTab_Click(sender, New System.EventArgs)
            Else
                AddShownTab(tabName)
                GlobalModel.Tabs.Add(New Tab(tabName))
                GlobalModel.SaveAllData()
            End If
        End If
    End Sub

    ''' <summary>
    ''' 删除分组
    ''' </summary>
    Private Sub TabPopupMenuDeleteTab_Click(sender As System.Object, e As System.EventArgs) Handles TabPopupMenuDeleteTab.Click
        If TabStrip.Tabs.Count = 1 Then
            MessageBoxEx.Show("无法删除最后的分组。", "删除", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, Me)
        Else
            Dim TabTitle As String = TabStrip.SelectedTab.Text
            Dim Tab As Tab = Tab.GetTabFromTitle(TabTitle)
            If Tab.Tips.Count <> 0 Then
                MessageBoxEx.Show("分组内存在 " & Tab.Tips.Count & " 条记录无法删除，请先将记录移动到别的分组。", "删除", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, Me)
            Else
                Dim ok As DialogResult = MessageBoxEx.Show("是否删除分组 """ + TabStrip.SelectedTab.Text + """？", "删除", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, Me)
                If ok = vbOK Then
                    TabStrip.Tabs.RemoveAt(TabStrip.SelectedTabIndex)
                    GlobalModel.Tabs.RemoveAt(GlobalModel.Tabs.IndexOf(Tab.GetTabFromTitle(TabTitle)))
                    GlobalModel.SaveAllData()
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' 重命名分组 !!!
    ''' </summary>
    Private Sub TabPopupMenuRenameTab_Click(sender As System.Object, e As System.EventArgs) Handles TabPopupMenuRenameTab.Click
        If TabStrip.SelectedTabIndex <> -1 Then
            Dim OldName As String = TabStrip.SelectedTab.Text
            Dim NewName As String = InputBox("重命名分组 """ & OldName & """ 为: ", "重命名", OldName)
            NewName = NewName.Trim()
            If NewName <> "" Then
                If Tab.CheckDuplicateTab(NewName, GlobalModel.Tabs) IsNot Nothing Then
                    MessageBoxEx.Show("分组标题 """ & NewName & """ 已存在。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, Me)
                    TabPopupMenuRenameTab_Click(sender, New System.EventArgs)
                Else
                    Tab.GetTabFromTitle(OldName).Title = NewName
                    GlobalModel.SaveAllData()
                    TabStrip.SelectedTab.Text = NewName
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' 双击Tab，新建或重命名
    ''' </summary>
    Private Sub TabStrip_DoubleClick(sender As Object, e As System.EventArgs) Handles TabStrip.DoubleClick
        Dim ePos As New Point(Cursor.Position.X - Me.Left - sender.Left, Cursor.Position.Y - Me.Top - sender.Top)
        If TabStrip.GetItemFromPoint(ePos) Is Nothing Then
            TabPopupMenuNewTab_Click(sender, New EventArgs)
        Else
            TabPopupMenuRenameTab_Click(sender, New EventArgs)
        End If
    End Sub

#End Region

#Region "分组选择 位置移动"

    ''' <summary>
    ''' 右键点击 Tab 选中
    ''' </summary>
    Private Sub TabStrip_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles TabStrip.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Dim sel As DD.BaseItem = TabStrip.GetItemFromPoint(e.Location)
            If sel IsNot Nothing Then
                TabStrip.SelectedTab = sel
            End If
        End If
    End Sub

    ''' <summary>
    ''' Tab 选择更改
    ''' </summary>
    Private Sub TabStrip_SelectedTabChanged(sender As Object, e As DD.SuperTabStripSelectedTabChangedEventArgs) Handles TabStrip.SelectedTabChanged
        SetSelectedItemButtonHide()
        If TabStrip.SelectedTabIndex <> -1 And GlobalModel.Tabs.Count <> 0 Then
            GlobalModel.CurrTabIdx = GlobalModel.Tabs.IndexOf(Tab.GetTabFromTitle(TabStrip.SelectedTab.Text))
            LoadList()
        End If
    End Sub

    ''' <summary>
    ''' Tab 顺序更改
    ''' </summary>
    Private Sub TabStrip_TabMoved(sender As Object, e As DD.SuperTabStripTabMovedEventArgs) Handles TabStrip.TabMoved
        Dim NewTabs As New List(Of Tab)
        For Each TabItem As DD.SuperTabItem In e.NewOrder
            NewTabs.Add(Tab.GetTabFromTitle(TabItem.Text))
        Next
        GlobalModel.Tabs = NewTabs
        GlobalModel.CurrTabIdx = GlobalModel.Tabs.IndexOf(Tab.GetTabFromTitle(TabStrip.SelectedTab.Text))
        GlobalModel.SaveAllData()
    End Sub

#End Region

#Region "分组内 Tip 移动"

    ''' <summary>
    ''' 获得非活动分组集，移动至分组 用
    ''' </summary>
    ''' <param name="ClassNamePreFix">分组类名前缀 ListPopupMenuMove / TabPopupMenuMove</param>
    ''' <returns>分组按钮</returns>
    Private Function GetUnActivityTabMoveButtonList(ByVal ClassNamePreFix As String) As List(Of DD.ButtonItem)
        Dim Tabs As New List(Of String)
        Dim TabBtns As New List(Of DD.ButtonItem)

        For Each Tab As Tab In GlobalModel.Tabs
            If Tab.Title <> GlobalModel.Tabs.Item(GlobalModel.CurrTabIdx).Title Then
                Tabs.Add(Tab.Title)
            End If
        Next

        For i = 0 To Tabs.Count - 1
            Dim Btn As New DD.ButtonItem With { _
                .Name = ClassNamePreFix & Tabs.Item(i), _
                .Tag = Tabs.Item(i), _
                .Text = Tabs.Item(i) & If(i < 10, "(&" & i & ")", "")
            }
            AddHandler Btn.Click, AddressOf MoveTab

            TabBtns.Add(Btn)
        Next
        Return TabBtns
    End Function

    ''' <summary>
    ''' 弹出列表右键菜单
    ''' </summary>
    Private Sub ListPopMenu_PopupOpen_Move(sender As Object, e As DD.PopupOpenEventArgs) Handles ListPopupMenu.PopupOpen
        Me.ListPopupMenuMove.SubItems.Clear()
        For Each Btn As DD.ButtonItem In GetUnActivityTabMoveButtonList("ListPopupMenuMove")
            Me.ListPopupMenuMove.SubItems.Add(Btn)
        Next
        Me.ListPopupMenuMove.Enabled = Not Me.ListPopupMenuMove.SubItems.Count = 0 And Not ListView.Items.Count = 0
        If Not Me.ListPopupMenuMove.Enabled Then
            Me.ListPopupMenuMove.SubItems.Clear()
        End If
    End Sub

    ''' <summary>
    ''' 弹出分组右键菜单
    ''' </summary>
    Private Sub TabPopupMenu_PopupOpen(sender As Object, e As DD.PopupOpenEventArgs) Handles TabPopupMenu.PopupOpen
        Me.TabPopupMenuMove.SubItems.Clear()
        For Each Btn As DD.ButtonItem In GetUnActivityTabMoveButtonList("TabPopupMenuMove")
            Me.TabPopupMenuMove.SubItems.Add(Btn)
        Next
        Me.TabPopupMenuMove.Enabled = Not Me.TabPopupMenuMove.SubItems.Count = 0 And Not ListView.Items.Count = 0
        If Not Me.TabPopupMenuMove.Enabled Then
            Me.TabPopupMenuMove.SubItems.Clear()
        End If

        TabPopupMenuLabel.Text = "分组 (共 " & GlobalModel.Tabs.Count & " 组)"
    End Sub

    ''' <summary>
    ''' 快捷键弹出 移动至分组
    ''' </summary>
    Private Sub ListPopupMenuMove_Click(sender As System.Object, e As System.EventArgs) Handles ListPopupMenuMove.Click
        If ListView.SelectedIndices.Count <> 0 AndAlso Not DD.BaseItem.IsOnPopup(ListPopupMenuMove) Then
            ListPopMenu_PopupOpen_Move(sender, New DD.PopupOpenEventArgs)
            ListPopupMenuMove.Popup(Me.Left + ButtonListSetting.Left, Me.Top + ButtonListSetting.Top + ButtonListSetting.Height - 1)
        End If
    End Sub

    ''' <summary>
    ''' 移动至分组
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub MoveTab(sender As System.Object, e As System.EventArgs)

        Dim Src As String = GlobalModel.Tabs.Item(GlobalModel.CurrTabIdx).Title
        Dim Dest As String = sender.Tag

        Dim SelectItems As List(Of TipItem)
        Dim Flag As String

        If ListView.SelectedItem Is Nothing Then
            ' 移动全部
            SelectItems = New List(Of TipItem)(ListView.Items.Cast(Of TipItem)())
            Flag = "确定将当前分组 """ & Src & """ 的全部内容 (共 " & ListView.Items.Count & " 项) 移动至分组 """ & Dest & """ 吗？"
        Else
            ' 移动已选
            SelectItems = New List(Of TipItem)(ListView.SelectedItems.Cast(Of TipItem)())
            Dim sb As New StringBuilder
            For Each Item As TipItem In SelectItems
                sb.AppendLine(Item.TipContent)
            Next
            Flag = "确定将当前分组 """ & Src & """ 所选内容 (共 " & _
                                                     ListView.SelectedItems.Count & " 项) 移动至分组 """ & Dest & """ 吗？" _
                                                     & Chr(10) & Chr(10) & sb.ToString
        End If

        Dim ok As MessageBoxButtons = MessageBoxEx.Show(Flag, "移动至分组...", MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, Me)
        If ok = vbOK Then
            For Each Item As TipItem In SelectItems
                Tab.GetTabFromTitle(Dest).Tips.Add(Item)
                Tab.GetTabFromTitle(Src).Tips.Remove(Item)
            Next
            GlobalModel.SaveAllData()

            For Each TabItem As DD.SuperTabItem In TabStrip.Tabs
                If TabItem.Text = Dest Then
                    TabStrip.SelectedTab = TabItem
                    Exit For
                End If
            Next

            For Each Item As TipItem In SelectItems
                Dim Idx As Integer = TipItem.GetIndexFromContent(Item.TipContent, Tab.GetTabFromTitle(Dest).Tips)
                If Idx <> -1 Then
                    ListView.SetSelected(Idx, True)
                End If
            Next
        End If
    End Sub

#End Region

End Class
