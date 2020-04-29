Imports System.Text
Imports DD = DevComponents.DotNetBar

Public Class TempForm

#Region "加载设置 加载列表内容和界面 失去焦点 启动退出 系统事件"

    Private Sub LoadSetting() ' On_Form_Load 用
        Me.Top = My.Settings.Top
        Me.Left = My.Settings.Left
        Me.Width = My.Settings.Width
        Me.Height = m_num_ListCount.Value * 17 + 27
        Me.MaxOpacity = My.Settings.MaxOpacity
        Me.TopMost = My.Settings.TopMost

        m_num_ListCount.Value = My.Settings.ListCount                                                   ' 列表高度
        m_popup_FoldMenu.Checked = My.Settings.IsFold                                                   ' 折叠菜单
        m_popup_LoadPosition.Enabled = My.Settings.SaveLeft <> - 1 And My.Settings.SaveTop <> - 1       ' 恢复位置
        m_popup_TopMost.Checked = My.Settings.TopMost                                                   ' 窗口置顶

        foldMenu(My.Settings.IsFold)                                                                    ' 折叠菜单
        If My.Settings.IsUseHotKey
            _globalPresenter.RegisterHotKey(Handle, My.Settings.HotKey, HOTKEY_ID)                      ' 注册热键
        End If
    End Sub

    Private Sub LoadFile() ' On_Form_Load 用
        _globalPresenter.LoadFile()
        m_TipListBox.DataSource = GlobalModel.CurrentTab.Tips
        m_TipListBox.Update()
        m_TipListBox.ClearSelected()
        m_TabView.DataSource = GlobalModel.Tabs
        m_TabView.Update()
        m_TabView.SelectedTabIndex = 0
    End Sub

    Private Const HOTKEY_ID As Integer = 0

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

    Private Sub ExitApplication(sender As Object, e As EventArgs) Handles m_btn_Exit.Click, m_popup_Exit.Click
        Dim ok = MessageBox.Show("确定退出 DesktopTips 吗？",
            "关闭", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
        If ok = vbYes Then
            Me.Close()
        End If
    End Sub

    Private Sub On_Form_Deactivate(sender As Object, e As EventArgs) Handles Me.Deactivate
        m_TipListBox.ClearSelected()
        If Me.Opacity > MaxOpacity Then
            FormOpacityDown()
        End If
    End Sub

    Private Sub On_Form_Closed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        My.Settings.Top = Me.Top
        My.Settings.Left = Me.Left
        My.Settings.Width = Me.Width
        My.Settings.ListCount = m_num_ListCount.Value
        My.Settings.Save()
        _globalPresenter.UnregisterHotKey(Handle, HOTKEY_ID)
    End Sub

    Private Sub On_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' 窗口显示
        LoadSetting()
        m_TabView.Tabs.Remove(m_tab_TestTab1)
        m_TabView.Tabs.Remove(m_tab_TestTab2)
        SetupOpacityButtons()
        SetupAssistButtons()

        ' 窗口透明度降低
        Me.CanMouseLeave = Function() As Boolean
            Return m_menu_ListPopupMenu.PopupControl Is Nothing AndAlso
                   m_menu_TabPopupMenu.PopupControl Is Nothing AndAlso
                   m_TabView.ContextMenu Is Nothing AndAlso
                   m_menu_MoveTipsSubMenu.PopupControl Is Nothing AndAlso
                   _isMenuPopuping = False
        End Function

        ' 列表
        LoadFile()
    End Sub

#End Region

#Region "标签: 增删改 移动 复制粘贴 全选"

    Private Sub InsertTip(sender As Object, e As EventArgs) Handles m_btn_InsertTip.Click, m_popup_InsertTip.Click
        If _tipPresenter.Insert() Then
            m_TipListBox.Update()
            m_TipListBox.SetSelectOnly(m_TipListBox.ItemCount - 1, True)
        End If
    End Sub

    Private Sub DeleteTip(sender As Object, e As EventArgs) Handles m_btn_RemoveTips.Click, m_popup_RemoveTips.Click
        Dim index = m_TipListBox.SelectedIndex
        If m_TipListBox.SelectedItems IsNot Nothing AndAlso _tipPresenter.Delete(m_TipListBox.SelectedItems) Then
            m_TipListBox.Update()
            m_TipListBox.SetSelectOnly(index)
        End If
    End Sub

    Private Sub UpdateTip(sender As Object, e As EventArgs) Handles m_TipListBox.DoubleClick, m_popup_UpdateTip.Click
        Dim index = m_TipListBox.SelectedIndex
        If m_TipListBox.SelectedCount = 1 AndAlso _tipPresenter.Update(m_TipListBox.SelectedItem) Then
            m_TipListBox.Update()
            m_TipListBox.SetSelectOnly(index)
        End If
    End Sub

    Private Sub MoveTipTop(sender As Object, e As EventArgs) Handles m_popup_MoveTopTop.Click
        If _tipPresenter.MoveTop(m_TipListBox.SelectedItem) Then
            m_TipListBox.Update()
            m_TipListBox.SetSelectOnly(0)
        End If
    End Sub

    Private Sub MoveTipBottom(sender As Object, e As EventArgs) Handles m_popup_MoveTipBottom.Click
        If _tipPresenter.MoveBottom(m_TipListBox.SelectedItem) Then
            m_TipListBox.Update()
            m_TipListBox.SetSelectOnly(m_TipListBox.ItemCount - 1)
        End If
    End Sub

    Private Sub MoveTipUp(sender As Object, e As EventArgs) Handles m_popup_MoveTipUp.Click, m_btn_MoveTipUp.Click
        If m_TipListBox.SelectedIndex >= 1 AndAlso _tipPresenter.MoveUp(m_TipListBox.SelectedItem) Then
            m_TipListBox.Update()
            m_TipListBox.SetSelectOnly(m_TipListBox.SelectedIndex - 1)
            If sender.Tag = "True" Then
                NativeMethod.MouseMoveUp(Cursor.Position, 17)
            End If
        End If
    End Sub

    Private Sub MoveDownTip(sender As Object, e As EventArgs) Handles m_popup_MoveTipDown.Click, m_btn_MoveTipDown.Click
        If m_TipListBox.SelectedIndex <= m_TipListBox.ItemCount - 2 AndAlso _tipPresenter.MoveDown(m_TipListBox.SelectedItem) Then
            m_TipListBox.Update()
            m_TipListBox.SetSelectOnly(m_TipListBox.SelectedIndex + 1)
            If sender.Tag = "True" Then
                NativeMethod.MouseMoveDown(Cursor.Position, 17)
            End If
        End If
    End Sub

    Private Sub CopyTip(sender As Object, e As EventArgs) Handles m_popup_CopyTips.Click
        _tipPresenter.Copy(m_TipListBox.SelectedItems)
    End Sub

    Private Sub PasteAppendToTip(sender As Object, e As EventArgs) Handles m_popup_PasteAppendToTip.Click
        If m_TipListBox.SelectedCount = 1 AndAlso _tipPresenter.Paste(m_TipListBox.SelectedItem) Then
            m_TipListBox.Update()
        End If
    End Sub

    Private Sub SelectAllTips(sender As Object, e As EventArgs) Handles m_popup_SelectAllTips.Click
        For i = 0 To m_TipListBox.ItemCount - 1
            m_TipListBox.SetSelected(i, True)
        Next
    End Sub

#End Region

#Region "标签: 查找 打开浏览文件 浏览器"

    Private Sub FindTips(sender As Object, e As EventArgs) Handles m_popup_FileTips.Click
        _tipPresenter.Search()
    End Sub

    Private Sub OpenFileDir(sender As Object, e As EventArgs) Handles m_popup_OpenDir.Click
        _globalPresenter.OpenFileDir()
    End Sub

    Private Sub ViewCurrentTipList(sender As Object, e As EventArgs) Handles m_popup_ViewTabList.Click
        _tipPresenter.ViewCurrentList(m_TipListBox.Items)
    End Sub

    Private Sub OpenTipAllLinks(sender As Object, e As EventArgs) Handles m_popup_OpenAllLinksInTips.Click
        _tipPresenter.OpenAllLinks(m_TipListBox.SelectedItems)
    End Sub

    Private Sub ViewTipAllLinks(sender As Object, e As EventArgs) Handles m_popup_ViewAllLinksInTips.Click
        _tipPresenter.ViewAllLinks(m_TipListBox.SelectedItems)
    End Sub

#End Region

#Region "分组: 增删改拖动"

    Private Sub InsertTab(sender As Object, e As EventArgs) Handles m_popup_NewTab.Click
        If _tabPresenter.Insert() Then
            m_TabView.Update()
            m_TabView.SelectedTabIndex = m_TabView.Tabs.Count - 1
        End If
    End Sub

    Private Sub DeleteTab(sender As Object, e As EventArgs) Handles m_popup_DeleteTab.Click
        If m_TabView.SelectedTabIndex <> - 1 Then
            If m_TabView.Tabs.Count = 1 Then
                MessageBoxEx.Show("无法删除最后的分组。", "删除", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, Me)
            Else
                Dim currentIndex As Integer = m_TabView.SelectedTabIndex
                If _tabPresenter.Delete(m_TabView.SelectedTab.TabSource) Then
                    m_TabView.Update()
                    m_TabView.SelectedTabIndex = currentIndex - 1
                End If
            End If
        End If
    End Sub

    Private Sub UpdateTab(sender As Object, e As EventArgs) Handles m_popup_RenameTab.Click
        If m_TabView.SelectedTabIndex <> - 1 Then
            If _tabPresenter.Update(m_TabView.SelectedTab.TabSource)
                m_TabView.Update()
            End If
        End If
    End Sub

    Private Sub TabStrip_TabMoved(sender As Object, e As DD.SuperTabStripTabMovedEventArgs) Handles m_TabView.TabMoved
        Dim newTabs As List(Of Tab) = (From item As TabView.TabViewItem In e.NewOrder Select item.TabSource).ToList()
        GlobalModel.Tabs = newTabs
        GlobalModel.CurrentTab = m_TabView.SelectedTab.TabSource
        _globalPresenter.SaveFile()
    End Sub

    Private Sub On_TabView_DoubleClick(sender As Object, e As EventArgs) Handles m_TabView.DoubleClick
        Dim pos As New Point(Cursor.Position.X - Me.Left - sender.Left, Cursor.Position.Y - Me.Top - sender.Top)
        If m_TabView.GetItemFromPoint(pos) Is Nothing Then
            InsertTab(sender, New EventArgs)
        Else
            UpdateTab(sender, New EventArgs)
        End If
    End Sub

#End Region

#Region "分组: 选择 移动至"

    Private Sub On_TabView_SelectedTabChanged(sender As Object, e As DD.SuperTabStripSelectedTabChangedEventArgs) Handles m_TabView.SelectedTabChanged
        HideAssistButtons()
        If m_TabView.SelectedTabIndex <> - 1 AndAlso m_TabView.SelectedTab.TabSource IsNot Nothing Then
            GlobalModel.CurrentTab = m_TabView.SelectedTab.TabSource
            m_TipListBox.DataSource = GlobalModel.CurrentTab.Tips
            m_TipListBox.Update()
            m_TipListBox.ClearSelected()
        End If
    End Sub

    Private Function GetUnselectedTabMoveButtonList(all As Boolean) As IEnumerable(Of DD.ButtonItem) ' SetupMoveToButtons 用
        Dim buttons As New List(Of DD.ButtonItem)
        Dim idx = 0
        For Each tab In GlobalModel.Tabs
            If m_TabView.SelectedTab IsNot Nothing AndAlso m_TabView.SelectedTab.TabSource.Title = tab.Title Then Continue For
            idx += 1
            Dim button As New DD.ButtonItem() With {.GlobalItem = False, .Tag = New Object() {tab, all}, .Text = $"{tab.Title}(&{idx})"}
            AddHandler button.Click, AddressOf MoveTipToTab
            buttons.Add(button)
        Next
        Return buttons
    End Function

    Private Sub MoveTipToTab(sender As DD.ButtonItem, e As EventArgs)
        Dim src As Tab = GlobalModel.CurrentTab
        Dim dest As Tab = sender.Tag(0)
        Dim all As Boolean = sender.Tag(1)
        Dim items As IEnumerable(Of TipItem) = If(all, m_TipListBox.Items.ToList(), m_TipListBox.SelectedItems)

        Dim tipItems As IEnumerable(Of TipItem) = If(TryCast(items, TipItem()), items.ToArray())
        If _tabPresenter.MoveItems(tipItems, src, dest) Then
            m_TabView.Update()
            m_TabView.SetSelected(dest)

            m_TipListBox.ClearSelected()
            For Each item As TipItem In tipItems
                Dim idx As Integer = GlobalModel.CurrentTab.Tips.IndexOf(item)
                If idx <> - 1
                    m_TipListBox.SetSelected(idx, True)
                End If
            Next
        End If
    End Sub

    Private Sub On_BtnMoveTipsAndMoveToTab_Click(sender As DD.ButtonItem, e As EventArgs) Handles m_menu_MoveTipsSubMenu.Click, m_menu_MoveToTabSubMenu.Click
        If Not DD.BaseItem.IsOnPopup(sender) Then
            sender.Popup(Me.Left + m_btn_OpenListPopup.Left, Me.Top + m_btn_OpenListPopup.Top + m_btn_OpenListPopup.Height - 1)
        End If
    End Sub

#End Region

#Region "显示: 透明度 辅助按钮 移动至菜单"

    Private ReadOnly _opacities() As Double = {0.2, 0.4, 0.6, 0.8, 1}
    Private ReadOnly _opacityButtons(_opacities.Length - 1) As DD.ButtonItem
    Private Const _eps As Double = 0.01

    Private Sub SetupOpacityButtons() ' On_Form_Load 用
        For i = 0 To _opacities.Length - 1
            _opacityButtons(i) = New DD.ButtonItem With {
                .Name = $"ListPopupMenuOpacity_{CInt(_opacities(i) * 100)}",
                .Text = $"{CInt(_opacities(i) * 100)}%", .Tag = _opacities(i)
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

            m_menu_OpacitySubMenu.SubItems.Add(_opacityButtons(i))
            If Math.Abs(Me.MaxOpacity - _opacityButtons(i).Tag) < _eps Then
                _opacityButtons(i).Checked = True
            End If
        Next
    End Sub

    Private Sub SetupAssistButtons() ' On_Form_Load 用
        Dim itemHeight As Integer = m_TipListBox.ItemHeight

        m_btn_MoveTipUp.Visible = False
        m_btn_MoveTipUp.Height = (itemHeight + 1) / 2
        m_btn_MoveTipUp.Width = itemHeight
        m_btn_MoveTipDown.Visible = False
        m_btn_MoveTipDown.Height = (itemHeight + 1) / 2
        m_btn_MoveTipDown.Width = itemHeight

        m_TipListBox.OnWheeledAction = Sub() HideAssistButtons()
    End Sub

    Private Sub ShowAssistButtons() ' On_ListViewAndForm_SizeChanged On_ListView_SelectedIndexChangedAndMouseDown 用
        Dim rect As Rectangle = m_TipListBox.GetItemRectangle(m_TipListBox.SelectedIndex)
        rect.Offset(m_TipListBox.Location)
        rect.Offset(2, 2)

        m_btn_MoveTipUp.Top = rect.Top
        m_btn_MoveTipUp.Left = rect.Left + rect.Width - m_btn_MoveTipDown.Width
        m_btn_MoveTipUp.Visible = True
        m_btn_MoveTipDown.Top = m_btn_MoveTipUp.Top + m_btn_MoveTipUp.Height - 1
        m_btn_MoveTipDown.Left = m_btn_MoveTipUp.Left
        m_btn_MoveTipDown.Visible = True
    End Sub

    Private Sub HideAssistButtons() ' SetupAssistButtons (OnWheeledAction) On_ListView_SelectedIndexChangedAndMouseDown On_TabView_SelectedTabChanged 用
        m_btn_MoveTipUp.Visible = False
        m_btn_MoveTipDown.Visible = False
    End Sub

    Private Sub On_ListViewAndForm_SizeChanged(sender As Object, e As EventArgs) Handles m_TipListBox.SizeChanged, Me.SizeChanged
        If m_btn_MoveTipUp.Visible = True AndAlso m_TipListBox.SelectedCount = 1 Then
            ShowAssistButtons()
        End If
    End Sub

    Private Sub SetupMoveToButtons(all As Boolean)
        Dim buttons As IEnumerable(Of DD.ButtonItem) = GetUnselectedTabMoveButtonList(all)
        If Not all Then ' TipPopup
            m_menu_MoveTipsSubMenu.SubItems.Clear()
            For Each btn As DD.ButtonItem In buttons
                m_menu_MoveTipsSubMenu.SubItems.Add(btn)
            Next
            If buttons.Count = 0 OrElse m_TipListBox.SelectedCount = 0 Then
                m_menu_MoveTipsSubMenu.Enabled = False
                m_menu_MoveTipsSubMenu.SubItems.Clear()
            End If
        Else ' TabPopup
            m_menu_MoveToTabSubMenu.SubItems.Clear()
            For Each btn As DD.ButtonItem In buttons
                m_menu_MoveToTabSubMenu.SubItems.Add(btn)
            Next
            If buttons.Count = 0 OrElse m_TipListBox.ItemCount = 0 Then
                m_menu_MoveToTabSubMenu.Enabled = False
                m_menu_MoveToTabSubMenu.SubItems.Clear()
            End If
        End If
    End Sub

#End Region

#Region "显示: 可用性判断 列表选择 大小调整 菜单与透明度"

    Private Sub CheckListItemEnabled() ' On_ListView_SelectedIndexChangedAndMouseDown On_BtnOpenPopupMenu_Click 用
        Dim isNotNull As Boolean = m_TipListBox.SelectedIndex <> - 1
        Dim isSingle As Boolean = m_TipListBox.SelectedCount = 1
        Dim isTop As Boolean = m_TipListBox.SelectedIndex = 0
        Dim isBottom As Boolean = m_TipListBox.SelectedIndex = m_TipListBox.ItemCount - 1

        ' 辅助按钮 位置按钮
        m_btn_MoveTipUp.Enabled = isNotNull And isSingle And Not isTop
        m_btn_MoveTipDown.Enabled = isNotNull And isSingle And Not isBottom
        m_popup_MoveTipUp.Enabled = isNotNull And isSingle And Not isTop
        m_popup_MoveTopTop.Enabled = isNotNull And isSingle And Not isTop
        m_popup_MoveTipBottom.Enabled = isNotNull And isSingle And Not isBottom
        m_popup_MoveTipDown.Enabled = isNotNull And isSingle And Not isBottom

        ' 删改复制粘贴
        m_btn_RemoveTips.Enabled = isNotNull
        m_popup_RemoveTips.Enabled = isNotNull
        m_popup_UpdateTip.Enabled = isSingle
        m_popup_HighlightTip.Enabled = isNotNull
        m_popup_HighlightTip.Checked = isNotNull AndAlso m_TipListBox.SelectedItem.IsHighLight
        m_popup_CopyTips.Enabled = isNotNull
        m_popup_PasteAppendToTip.Enabled = isSingle

        ' 浏览器
        m_menu_BrowserSubMenu.Enabled = _tipPresenter.GetLinks(m_TipListBox.SelectedItems).Count >= 1

        ' 移动
        m_menu_MoveTipsSubMenu.Enabled = isNotNull
    End Sub

    Private Sub On_ListView_SelectedIndexChangedAndMouseDown(sender As Object, e As EventArgs) Handles m_TipListBox.SelectedIndexChanged, m_TipListBox.MouseDown
        CheckListItemEnabled()
        If m_TipListBox.SelectedCount = 1 Then ShowAssistButtons() Else HideAssistButtons()
        m_TipListBox.Refresh()
    End Sub

    Private Sub On_TabView_ItemClick(sender As Object, e As EventArgs) Handles m_TabView.ItemClick
        m_TipListBox.ClearSelected()
    End Sub

    Private Sub On_BtnResize_MouseMove(sender As Object, e As MouseEventArgs) Handles m_btn_Resize.MouseMove
        If e.Button = MouseButtons.Left Then
            Me.Width = PushDownWindowSize.Width + Cursor.Position.X - PushDownMouseInScreen.X
        End If
    End Sub

    Private _isMenuPopuping As Boolean = False

    Private Sub On_TabStrip_PopupOpen(sender As Object, e As EventArgs) Handles m_TabView.PopupOpen
        _isMenuPopuping = True
    End Sub

    Private Sub On_SomePopup_FinishedAndClose(sender As Object, e As EventArgs) _
        Handles m_menu_ListPopupMenu.PopupFinalized, m_menu_TabPopupMenu.PopupFinalized, m_menu_MoveTipsSubMenu.PopupFinalized, m_TabView.PopupClose
        _isMenuPopuping = False
        FormOpacityDown()
    End Sub

#End Region

#Region "显示: 其他 弹出菜单 列表数量 折叠菜单 热键置顶 加载保存位置"

    Private Sub On_BtnOpenPopupMenu_Click(sender As Object, e As EventArgs) Handles m_btn_OpenListPopup.Click
        CheckListItemEnabled()
        m_menu_ListPopupMenu.Popup(Me.Left + sender.Left, Me.Top + sender.Top + sender.Height - 1)
    End Sub

    Private Sub On_ListPopupMenu_PopupOpen(sender As Object, e As DD.PopupOpenEventArgs) Handles m_menu_ListPopupMenu.PopupOpen
        m_popup_SelectedTipsCountLabel.Visible = m_TipListBox.SelectedCount > 0
        m_popup_SelectedTipsTextLabel.Visible = m_TipListBox.SelectedCount > 0

        Dim sb As New StringBuilder
        For Each item As TipItem In m_TipListBox.SelectedItems
            sb.AppendLine(item.Content)
        Next
        Dim highlightCount = 0
        For Each tip As TipItem In GlobalModel.CurrentTab.Tips
            If tip.IsHighLight Then highlightCount += 1
        Next

        m_popup_SelectedTipsTextLabel.Text = sb.ToString()
        m_popup_TipsCountLabel.Text = $"列表 (共 {m_TipListBox.ItemCount} 项，高亮 {highlightCount} 项)"
        m_popup_SelectedTipsCountLabel.Text = $"当前选中 (共 {m_TipListBox.SelectedCount} 项)"

        SetupMoveToButtons(all := False)
        m_menu_ListPopupMenu.Refresh()
    End Sub

    Private Sub On_TabPopupMenu_PopupOpen(sender As Object, e As DD.PopupOpenEventArgs) Handles m_menu_TabPopupMenu.PopupOpen
        m_popup_TabLabel.Text = $"分组 (共 {GlobalModel.Tabs.Count} 组)"
        SetupMoveToButtons(all := True)
        m_menu_TabPopupMenu.Refresh()
    End Sub

    Private Sub On_NumericListCount_ValueChanged(sender As Object, e As EventArgs) Handles m_num_ListCount.ValueChanged
        Dim targetHeight As Integer = m_num_ListCount.Value * 17 + 27
        If Me.Height = targetHeight Then Return
        Dim direction As Integer = targetHeight - Me.Height
        Me.Height = targetHeight

        Dim dx As Integer = Cursor.Position.X * UInt16.MaxValue / My.Computer.Screen.Bounds.Width
        Dim dy As Integer = (Cursor.Position.Y + direction) * UInt16.MaxValue / My.Computer.Screen.Bounds.Height
        Const flag = NativeMethod.MouseEvent.MOUSEEVENTF_MOVE Or NativeMethod.MouseEvent.MOUSEEVENTF_ABSOLUTE
        NativeMethod.mouse_event(flag, dx, dy, 0, 0)
    End Sub

    Private Sub On_BtnShowNumericSetListCount_Click(sender As Object, e As EventArgs) Handles m_popup_ShowSetListCount.Click
        m_popup_ShowSetListCount.Checked = Not m_popup_ShowSetListCount.Checked
        m_num_ListCount.Visible = m_popup_ShowSetListCount.Checked
    End Sub

    Private Sub On_BtnFoldMenu_Click(sender As System.Object, e As EventArgs) Handles m_popup_FoldMenu.Click
        m_popup_FoldMenu.Checked = Not m_popup_FoldMenu.Checked
        My.Settings.IsFold = m_popup_FoldMenu.Checked
        My.Settings.Save()
        foldMenu(m_popup_FoldMenu.Checked)
    End Sub

    Private Sub On_BtnSetupHotkey_Click(sender As Object, e As EventArgs) Handles m_popup_SetupHotkey.Click
        _globalPresenter.SetupHotKey(Handle, HOTKEY_ID)
    End Sub

    Private Sub On_BtnTopMost_Click(sender As Object, e As EventArgs) Handles m_popup_TopMost.Click
        m_popup_TopMost.Checked = Not m_popup_TopMost.Checked
        Me.TopMost = m_popup_TopMost.Checked
        My.Settings.TopMost = Me.TopMost
        My.Settings.Save()
    End Sub

    Private Sub On_BtnLoadPosition_Click(sender As Object, e As EventArgs) Handles m_popup_LoadPosition.Click
        If My.Settings.SaveTop >= 0 And My.Settings.SaveLeft >= 0
            Me.Top = My.Settings.SaveTop
            Me.Left = My.Settings.SaveLeft
        End If
    End Sub

    Private Sub On_BtnSavePosition_Click(sender As Object, e As EventArgs) Handles m_popup_SavePosition.Click
        Dim ok = MessageBox.Show("确定保存当前位置，注意该操作会覆盖之前保存的窗口位置。",
            "保存位置", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
        If ok = vbOK Then
            m_popup_LoadPosition.Enabled = True
            My.Settings.SaveTop = Me.Top
            My.Settings.SaveLeft = Me.Left
            My.Settings.Save()
        End If
    End Sub

#End Region
End Class
