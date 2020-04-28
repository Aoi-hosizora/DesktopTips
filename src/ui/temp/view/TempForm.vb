Imports System.Text
Imports DD = DevComponents.DotNetBar

Public Class TempForm

#Region "加载设置 加载列表内容和界面 启动退出 系统事件"

    ''' <summary>
    ''' 加载设置，应用到UI
    ''' MainForm_Load 用
    ''' </summary>
    Private Sub LoadSetting()
        Me.Top = My.Settings.Top
        Me.Left = My.Settings.Left
        Me.Height = My.Settings.Height
        Me.Width = My.Settings.Width
        Me.MaxOpacity = My.Settings.MaxOpacity
        Me.TopMost = My.Settings.TopMost

        ListPopupMenuFold.Checked = My.Settings.IsFold                                                  ' 折叠菜单
        ListPopupMenuLoadPos.Enabled = Not (My.Settings.SaveLeft = - 1 Or My.Settings.SaveTop = - 1)    ' 恢复位置
        NumericUpDownListCnt.Value = (Me.Height - 27) \ 17                                              ' 列表高度
        ListPopupMenuWinTop.Checked = My.Settings.TopMost                                               ' 窗口置顶
        _globalPresenter.RegisterHotKey(Handle, My.Settings.HotKey, HOTKEY_ID)                          ' 注册热键
        FoldMenu(My.Settings.IsFold)                                                                    ' 折叠菜单
    End Sub

    ''' <summary>
    ''' 加载文件，显示当前分组
    ''' MainForm_Load TabStrip_SelectedTabChanged 用
    ''' </summary>
    Private Sub LoadFile()
        _globalPresenter.LoadFile()
        ListView.DataSource = GlobalModel.CurrentTab.Tips
        ListView.Refresh()
        LabelNothing.Visible = ListView.ItemCount = 0
    End Sub

    ''' <summary>
    ''' RegisterHotKey 注册的热键 Id
    ''' </summary>
    Private Const HOTKEY_ID As Integer = 0

    ''' <summary>
    ''' 设置快捷键响应
    ''' </summary>
    Protected Overrides Sub WndProc(ByRef m As Message)
        If m.Msg = NativeMethod.WM_HOTKEY Then
            If m.WParam.ToInt32() = HOTKEY_ID Then
                Me.Activate()
                NativeMethod.SetForegroundWindow(Handle)
                FormOpacityUp()
            End If
        End If
        MyBase.WndProc(m)
    End Sub

    ''' <summary>
    ''' 窗口关闭之后处理
    ''' </summary>
    Private Sub ClosedForm(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        My.Settings.Top = Me.Top
        My.Settings.Left = Me.Left
        My.Settings.Width = Me.Width
        My.Settings.Height = Me.Height
        My.Settings.Save()
        _globalPresenter.UnregisterHotKey(Handle, HOTKEY_ID)
    End Sub

    ''' <summary>
    ''' 窗口加载
    ''' </summary>
    Private Sub LoadForm(sender As Object, e As EventArgs) Handles MyBase.Load
        ' 窗口显示
        LoadSetting()
        Me.Refresh()
        Me.TabStrip.Tabs.Remove(Me.TabItemTest)
        Me.TabStrip.Tabs.Remove(Me.TabItemTest2)
        ButtonRemoveItem.Enabled = False
        SetupAssistButtonsLayout()
        SetupOpacityButtonsLayout()

        ' 窗口动画
        CanMouseLeave = Function() As Boolean
            Return ListPopupMenu.PopupControl Is Nothing AndAlso
                   TabPopupMenu.PopupControl Is Nothing AndAlso
                   TabStrip.ContextMenu Is Nothing AndAlso
                   ListPopupMenuMove.PopupControl Is Nothing AndAlso
                   _isMenuPopuping = False
        End Function

        ' 列表
        LoadFile()
        For i = 0 To GlobalModel.Tabs.Count - 1
            AddShownTab(GlobalModel.Tabs.Item(i))
        Next
    End Sub

#End Region

#Region "增删改 移动 复制粘贴 全选"

    ''' <summary>
    ''' 插入标签
    ''' </summary>
    Private Sub InsertTip(sender As Object, e As EventArgs) Handles ButtonAddItem.Click, ListPopupMenuAddItem.Click, LabelNothing.DoubleClick
        If _listPresenter.Insert() Then
            ListView.Update()
            ListView.SetSelectOnly(ListView.ItemCount - 1, True)
        End If
    End Sub

    ''' <summary>
    ''' 删除标签
    ''' </summary>
    Private Sub DeleteTip(sender As Object, e As EventArgs) Handles ButtonRemoveItem.Click, ListPopupMenuRemoveItem.Click
        Dim index = ListView.SelectedIndex
        If ListView.SelectedItems IsNot Nothing AndAlso _listPresenter.Delete(ListView.SelectedItems) Then
            ListView.Update()
            ListView.SetSelectOnly(index)
        End If
    End Sub

    ''' <summary>
    ''' 修改标签
    ''' </summary>
    Private Sub UpdateTip(sender As Object, e As EventArgs) Handles ListView.DoubleClick, ListPopupMenuEditItem.Click
        Dim index = ListView.SelectedIndex
        If ListView.SelectedCount = 1 AndAlso _listPresenter.Update(ListView.SelectedItem) Then
            ListView.Update()
            ListView.SetSelectOnly(index)
        End If
    End Sub

    ''' <summary>
    ''' 置顶
    ''' </summary>
    Private Sub MoveTipTop(sender As Object, e As EventArgs) Handles ListPopupMenuMoveTop.Click
        If _listPresenter.MoveTop(ListView.SelectedItem) Then
            ListView.Update()
            ListView.SetSelectOnly(0)
        End If
    End Sub

    ''' <summary>
    ''' 置底
    ''' </summary>
    Private Sub MoveTipBottom(sender As Object, e As EventArgs) Handles ListPopupMenuMoveBottom.Click
        If _listPresenter.MoveBottom(ListView.SelectedItem) Then
            ListView.Update()
            ListView.SetSelectOnly(ListView.ItemCount - 1)
        End If
    End Sub

    ''' <summary>
    ''' 上移
    ''' </summary>
    Private Sub MoveTipUp(sender As Object, e As EventArgs) Handles ListPopupMenuMoveUp.Click, ButtonItemUp.Click
        Dim currIdx = ListView.SelectedIndex
        If currIdx >= 1 AndAlso _listPresenter.MoveUp(ListView.SelectedItem) Then
            ListView.Update()
            ListView.SetSelectOnly(currIdx - 1)
            If sender.Tag = "True" Then
                NativeMethod.MouseMoveUp(Cursor.Position, 17)
            End If
        End If
    End Sub

    ''' <summary>
    ''' 下移
    ''' </summary>
    Private Sub MoveDownTip(sender As Object, e As EventArgs) Handles ListPopupMenuMoveDown.Click, ButtonItemDown.Click
        Dim currIdx = ListView.SelectedIndex
        If currIdx <= ListView.ItemCount - 2 AndAlso _listPresenter.MoveDown(ListView.SelectedItem) Then
            ListView.Update()
            ListView.SetSelectOnly(currIdx + 1)
            If sender.Tag = "True" Then
                NativeMethod.MouseMoveDown(Cursor.Position, 17)
            End If
        End If
    End Sub

    ''' <summary>
    ''' 复制标签
    ''' </summary>
    Private Sub CopyTip(sender As Object, e As EventArgs) Handles ListPopupMenuCopy.Click
        _listPresenter.Copy(ListView.SelectedItems)
    End Sub

    ''' <summary>
    ''' 粘贴到最后
    ''' </summary>
    Private Sub PasteAppendToTip(sender As Object, e As EventArgs) Handles ListPopupMenuPasteAppend.Click
        If ListView.SelectedIndices.Count = 1 AndAlso _listPresenter.Paste(ListView.SelectedItem) Then
            ListView.Update()
        End If
    End Sub

    ''' <summary>
    ''' 全选
    ''' </summary>
    Private Sub SelectAllTips(sender As Object, e As EventArgs) Handles ListPopupMenuSelectAll.Click
        For i = 0 To ListView.ItemCount - 1
            ListView.SetSelected(i, True)
        Next
    End Sub

#End Region

#Region "查找 打开浏览文件 浏览器"

    ''' <summary>
    ''' 查找
    ''' </summary>
    Private Sub FindTips(sender As Object, e As EventArgs) Handles ListPopupMenuFind.Click
        _listPresenter.Search()
    End Sub

    ''' <summary>
    ''' 打开文件所在位置
    ''' </summary>
    Private Sub OpenFileDir(sender As Object, e As EventArgs) Handles ListPopupMenuOpenDir.Click
        _globalPresenter.OpenFileDir()
    End Sub

    ''' <summary>
    ''' 浏览当前列表
    ''' </summary>
    Private Sub ViewCurrentTipList(sender As Object, e As EventArgs) Handles ListPopupMenuViewFile.Click
        _listPresenter.ViewCurrentList(ListView.Items)
    End Sub

    ''' <summary>
    ''' 打开所有链接
    ''' </summary>
    Private Sub OpenTipAllLinks(sender As Object, e As EventArgs) Handles ListPopupMenuOpenAllLink.Click
        _listPresenter.OpenAllLinks(ListView.SelectedItems)
    End Sub

    ''' <summary>
    ''' 浏览所有链接，打开部分链接
    ''' </summary>
    Private Sub ViewTipAllLinks(sender As Object, e As EventArgs) Handles ListPopupMenuViewAllLink.Click
        _listPresenter.ViewAllLinks(ListView.SelectedItems)
    End Sub

#End Region

#Region "分组显示 透明度 辅助按钮"

    ''' <summary>
    ''' 新建分组显示
    ''' MainForm_Load TabPopupMenuNewTab_Click 用
    ''' </summary>
    Private Sub AddShownTab(tab As Tab)
        Dim newTabItem = New DD.SuperTabItem() With {
            .GlobalItem = False,
            .Name = "TabItemCustom_" & GlobalModel.Tabs.Count,
            .Text = tab.Title,
            .Tag = tab
            }
        ' AddHandler newTabItem.MouseDown, AddressOf TabStrip_MouseDown
        Me.TabStrip.Tabs.Add(newTabItem)
    End Sub

    Private ReadOnly _opacities() As Double = {0.2, 0.4, 0.6, 0.8, 1}
    Private ReadOnly _opacityButtons(_opacities.Length - 1) As DD.ButtonItem
    Private Const _eps As Double = 1e-2

    ''' <summary>
    ''' 动态添加透明度
    ''' MainForm_Load 用
    ''' </summary>
    Private Sub SetupOpacityButtonsLayout()
        For i = 0 To _opacities.Length - 1
            _opacityButtons(i) = New DD.ButtonItem With {
                .Name = $"ListPopupMenuOpacity_{CInt(_opacities(i) * 100)}",
                .Text = $"{CInt(_opacities(i) * 100)}%",
                .Tag = _opacities(i)
                }
            AddHandler _opacityButtons(i).Click, Sub(sender As DD.ButtonItem, e As EventArgs)
                Me.MaxOpacity = sender.Tag
                My.Settings.MaxOpacity = Me.MaxOpacity
                My.Settings.Save()

                For Each opBtn In _opacityButtons
                    opBtn.Checked = False
                Next
                sender.Checked = True
                FormOpacityDown()
            End Sub

            ListPopupMenuOpacity.SubItems.Add(_opacityButtons(i))
            If Math.Abs(Me.MaxOpacity - _opacityButtons(i).Tag) < _eps Then
                _opacityButtons(i).Checked = True
            End If
        Next
    End Sub

    ''' <summary>
    ''' 设置辅助按钮布局
    ''' MainForm_Load 用
    ''' </summary>
    Private Sub SetupAssistButtonsLayout()
        Dim itemHeight As Integer = ListView.ItemHeight

        ButtonItemUp.Visible = False
        ButtonItemUp.Height = (itemHeight + 1) / 2
        ButtonItemUp.Width = itemHeight
        ButtonItemDown.Visible = False
        ButtonItemDown.Height = (itemHeight + 1) / 2
        ButtonItemDown.Width = itemHeight
    End Sub

    ''' <summary>
    ''' 显示辅助按钮
    ''' </summary>
    Private Sub ShowAssistButtons()
        Dim rect As Rectangle = ListView.GetItemRectangle(ListView.SelectedIndex)
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
    Private Sub HideAssistButtons()
        ButtonItemUp.Visible = False
        ButtonItemDown.Visible = False
    End Sub

    ''' <summary>
    ''' 调整大小 隐藏辅助按钮
    ''' </summary>
    Private Sub SizeChangedListView(sender As Object, e As EventArgs) Handles ListView.SizeChanged
        If ButtonItemUp.Visible = True AndAlso ListView.SelectedIndices.Count = 1 Then
            ShowAssistButtons()
        End If
    End Sub

#End Region

#Region "窗口焦点 弹出菜单标记 大小调整 无内容标签更新"

    ''' <summary>
    ''' 窗口失去焦点，取消选择
    ''' </summary>
    Private Sub DeactivateMainForm(sender As Object, e As EventArgs) Handles Me.Deactivate
        ListView.ClearSelected()
        If Me.Opacity > MaxOpacity Then
            FormOpacityDown()
        End If
    End Sub

    ''' <summary>
    ''' 没有选择列表项，取消选择
    ''' </summary>
    Private Sub ListView_MouseDown_Sel(sender As Object, e As MouseEventArgs) Handles ListView.MouseDown, TabStrip.MouseDown
        ListView_SelectedIndexChanged(sender, New EventArgs)
        If ListView.Items.Count > 0 Then
            Dim rect As Rectangle = ListView.GetItemRectangle(ListView.Items.Count - 1)
            If e.Y > rect.Top + rect.Height Then ListView.ClearSelected()
        End If
    End Sub

    ''' <summary>
    ''' 判断透明度是否可以变化
    ''' </summary>
    Private _isMenuPopuping As Boolean = False

    ''' <summary>
    ''' TabStrip 菜单弹出
    ''' </summary>
    Private Sub TabStrip_PopupOpen(sender As Object, e As EventArgs) Handles TabStrip.PopupOpen
        _isMenuPopuping = True
    End Sub

    ''' <summary>
    ''' 菜单关闭
    ''' </summary>
    Private Sub PopMenu_PopupFinalizedAndClosed(sender As Object, e As EventArgs) _
        Handles ListPopupMenu.PopupFinalized, TabPopupMenu.PopupFinalized, ListPopupMenuMove.PopupFinalized, TabStrip.PopupClose
        _isMenuPopuping = False
        FormOpacityDown()
    End Sub

    ''' <summary>
    ''' 大小调整
    ''' </summary>
    Private Sub ButtonResizeFlag_MouseMove(sender As Object, e As MouseEventArgs) Handles ButtonResizeFlag.MouseMove
        If IsMouseDown Then
            Me.Width = PushDownWindowSize.Width + Cursor.Position.X - PushDownMouseInScreen.X
        End If
    End Sub

    ''' <summary>
    ''' 窗口调整大小更新 Label
    ''' </summary>
    Private Sub MainForm_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged
        LabelNothing.Top = ListView.Top + 1
        LabelNothing.Left = ListView.Left + 1
        LabelNothing.Height = ListView.Height - 2
        LabelNothing.Width = ListView.Width - 2
    End Sub

#End Region

#Region "可用性判断 列表选择 滚动条 屏幕提示"

    ''' <summary>
    ''' 选择可用性
    ''' ListView_SelectedIndexChanged 和 ButtonListSetting_Click 用
    ''' </summary>
    Private Sub CheckItemEnabled()
        Dim isNotNull As Boolean = ListView.SelectedIndex <> - 1
        Dim isSingle As Boolean = ListView.SelectedIndices.Count = 1
        Dim isTop As Boolean = ListView.SelectedIndex = 0
        Dim isBottom As Boolean = ListView.SelectedIndex = ListView.Items.Count() - 1

        ' 辅助按钮
        ButtonItemUp.Enabled = isNotNull And isSingle And Not isTop
        ButtonItemDown.Enabled = isNotNull And isSingle And Not isBottom

        ' 位置按钮
        ListPopupMenuMoveUp.Enabled = isNotNull And isSingle And Not isTop
        ListPopupMenuMoveTop.Enabled = isNotNull And isSingle And Not isTop
        ListPopupMenuMoveBottom.Enabled = isNotNull And isSingle And Not isBottom
        ListPopupMenuMoveDown.Enabled = isNotNull And isSingle And Not isBottom

        ' 删改复制
        ButtonRemoveItem.Enabled = isNotNull
        ListPopupMenuRemoveItem.Enabled = isNotNull
        ListPopupMenuEditItem.Enabled = isSingle
        ListPopupMenuHighLight.Enabled = isNotNull
        ListPopupMenuHighLight.Checked = isNotNull AndAlso ListView.SelectedItem.IsHighLight
        ListPopupMenuCopy.Enabled = isNotNull

        ' 移动
        ListPopupMenuMove.Enabled = isNotNull

        ' 附加
        ListPopupMenuPasteAppend.Enabled = isSingle

        ' 浏览器
        ListPopupMenuBrowser.Enabled = ListView.SelectedItems.Cast(Of TipItem)().
                                           Where(Function (item) item.Content.IndexOf("http://", StringComparison.Ordinal) <> - 1 Or
                                                                 item.Content.IndexOf("https://", StringComparison.Ordinal) <> - 1).
                                           Count >= 1
    End Sub

    ''' <summary>
    ''' 列表选择, 辅助按钮显示 可用判断
    ''' </summary>
    Private Sub ListView_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView.SelectedIndexChanged
        CheckItemEnabled()
        If ListView.SelectedIndices.Count = 1 Then ShowAssistButtons() Else HideAssistButtons()
        ListView.Refresh()
    End Sub

    ''' <summary>
    ''' 右键列表同时选中项
    ''' </summary>
    Private Sub ListView_RightMouseUp(sender As Object, e As MouseEventArgs) Handles ListView.MouseUp
        Dim idx As Integer = ListView.IndexFromPoint(e.X, e.Y)
        If e.Button = MouseButtons.Right And idx >= 0 And idx < ListView.Items.Count And ListView.SelectedIndices.Count <= 1 Then
            ListView.ClearSelected()
            ListView.SetSelected(idx, True)
            If ListView.Items.Count > 0 Then
                Dim rect As Rectangle = ListView.GetItemRectangle(ListView.Items.Count - 1)
                If e.Y > rect.Top + rect.Height Then ListView.ClearSelected()
            End If
        End If
    End Sub

    ' 滚动条获得焦点
    Private Sub ListView_MouseCaptureChanged(sender As Object, e As EventArgs) Handles ListView.MouseCaptureChanged
        If Cursor.Position.X > Me.Left + ListView.Left + ListView.Width - 20 Then
            HideAssistButtons()
        End If
    End Sub

    ' 滚动
    Private Sub ListView_MouseWheel(sender As Object, e As MouseEventArgs) Handles ListView.MouseWheel
        HideAssistButtons()
    End Sub

    ''' <summary>
    ''' 悬浮弹出提示
    ''' </summary>
    Private Sub ListView_MouseMove(sender As Object, e As MouseEventArgs) Handles ListView.MouseMove
        Static hoverIdx As Integer = - 1
        Dim idx = ListView.IndexFromPoint(e.Location)
        If hoverIdx <> idx AndAlso idx <> - 1 AndAlso idx < ListView.Items.Count Then
            HoverToolTip.Hide(ListView)
            HoverToolTip.SetToolTip(ListView, ListView.TipItems.ElementAt(idx).Content)
            hoverIdx = idx
        End If
    End Sub

#End Region

#Region "其他菜单中的显示设置"

    ''' <summary>
    ''' 退出程序
    ''' </summary>
    Private Sub ExitApplication(sender As Object, e As EventArgs) Handles ButtonCloseForm.Click, ListPopupMenuExit.Click
        Dim ok = MessageBox.Show("确定退出 DesktopTips 吗？",
            "关闭", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
        If ok = vbYes Then
            Me.Close()
        End If
    End Sub

    ''' <summary>
    ''' 弹出菜单 调整标题
    ''' </summary>
    Private Sub PopupOpenListPopMenu(sender As Object, e As DD.PopupOpenEventArgs) Handles ListPopupMenu.PopupOpen
        e.Cancel = True
        ListPopupMenuLabelSelItem.Visible = ListView.SelectedIndex <> - 1
        ListPopupMenuLabelSelItemText.Visible = ListView.SelectedIndex <> - 1

        Dim cnt As Integer = ListView.SelectedCount
        Dim hlCnt = 0
        For Each tip As TipItem In GlobalModel.CurrentTab.Tips
            If tip.IsHighLight Then hlCnt += 1
        Next

        Dim sb As New StringBuilder
        For Each item As TipItem In ListView.SelectedItems
            sb.AppendLine(item.Content)
        Next

        ListPopupMenuLabelSelItemText.Text = sb.ToString()
        ListPopupMenuLabelItemList.Text = $"列表 (共 {cnt} 项，高亮 {hlCnt} 项)"
        ListPopupMenuLabelSelItem.Text = $"当前选中 (共 {cnt} 项)"
        e.Cancel = False
        ListPopupMenu.Refresh()
    End Sub

    ''' <summary>
    ''' 显示弹出菜单
    ''' </summary>
    Private Sub ButtonListSetting_Click(sender As Object, e As EventArgs) Handles ButtonListSetting.Click
        CheckItemEnabled()
        ListPopupMenu.Popup(Me.Left + sender.Left, Me.Top + sender.Top + sender.Height - 1)
    End Sub

    ''' <summary>
    ''' 调整大小
    ''' </summary>
    Private Sub NumericUpDownListCnt_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDownListCnt.ValueChanged
        Dim y As Integer
        If Me.Height < NumericUpDownListCnt.Value * ListView.ItemHeight + 27 Then
            y = 17
        ElseIf Me.Height > NumericUpDownListCnt.Value * ListView.ItemHeight + 27 Then
            y = - 17
        End If
        Me.Height = NumericUpDownListCnt.Value * ListView.ItemHeight + 27

        Dim dx As Integer = Cursor.Position.X * UInt16.MaxValue / My.Computer.Screen.Bounds.Width
        Dim dy As Integer = (Cursor.Position.Y + y) * UInt16.MaxValue / My.Computer.Screen.Bounds.Height

        NativeMethod.mouse_event(NativeMethod.MouseEvent.MOUSEEVENTF_MOVE Or NativeMethod.MouseEvent.MOUSEEVENTF_ABSOLUTE, dx, dy, 0, 0)
    End Sub

    ''' <summary>
    ''' 显示调整大小
    ''' </summary>
    Private Sub ListPopupMenuListHeight_Click(sender As Object, e As EventArgs) Handles ListPopupMenuListHeight.Click
        ListPopupMenuListHeight.Checked = Not ListPopupMenuListHeight.Checked
        NumericUpDownListCnt.Visible = ListPopupMenuListHeight.Checked
    End Sub

    ''' <summary>
    ''' 快捷键设置
    ''' </summary>
    Private Sub ListPopupMenuHotkeySetting_Click(sender As Object, e As EventArgs) Handles ListPopupMenuShotcutSetting.Click
        _globalPresenter.SetupHotKey(Handle, HOTKEY_ID)
    End Sub

    ''' <summary>
    ''' 置顶
    ''' </summary>
    Private Sub ListPopupMenuWinTop_Click(sender As Object, e As EventArgs) Handles ListPopupMenuWinTop.Click
        ListPopupMenuWinTop.Checked = Not ListPopupMenuWinTop.Checked
        Me.TopMost = ListPopupMenuWinTop.Checked
        My.Settings.TopMost = Me.TopMost
        My.Settings.Save()
    End Sub

    ''' <summary>
    ''' 加载位置
    ''' </summary>
    Private Sub ListPopupMenuLoadPos_Click(sender As Object, e As EventArgs) Handles ListPopupMenuLoadPos.Click
        Me.Top = My.Settings.SaveTop
        Me.Left = My.Settings.SaveLeft
    End Sub

    ''' <summary>
    ''' 保存位置
    ''' </summary>
    Private Sub ListPopupMenuSavePos_Click(sender As Object, e As EventArgs) Handles ListPopupMenuSavePos.Click
        Dim ok As DialogResult = MessageBox.Show(
            "确定保存当前位置，注意该操作会覆盖之前保存的窗口位置。",
            "保存位置", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
        If ok = vbOK Then
            My.Settings.SaveTop = Me.Top
            My.Settings.SaveLeft = Me.Left
            My.Settings.Save()
            ListPopupMenuLoadPos.Enabled = True
        End If
    End Sub

#End Region
End Class
