Imports DD = DevComponents.DotNetBar

Public Class MainForm

#Region "加载设置 加载列表内容和界面 失去焦点 启动退出 系统事件" ' TODO

    Private ReadOnly Property HEIGHT_17 As Integer
        Get
            Return m_ListView.ItemHeight
        End Get
    End Property

    Private ReadOnly Property HEIGHT_27 As Integer
        Get
            Return m_num_ListCount.Height + 5
        End Get
    End Property

    Private Sub LoadSetting()
        Me.Top = My.Settings.Top
        Me.Left = My.Settings.Left
        Me.Width = My.Settings.Width
        Me.Height = m_num_ListCount.Value * HEIGHT_17 + HEIGHT_27
        Me.MaxOpacity = My.Settings.MaxOpacity
        Me.TopMost = My.Settings.TopMost

        m_num_ListCount.Value = My.Settings.ListCount                                                   ' 列表高度
        m_popup_LoadPosition.Enabled = My.Settings.SaveLeft <> - 1 And My.Settings.SaveTop <> - 1       ' 恢复位置
        m_popup_TopMost.Checked = My.Settings.TopMost                                                   ' 窗口置顶
        If My.Settings.IsUseHotKey Then
            _globalPresenter.RegisterHotKey(Handle, My.Settings.HotKey, HOTKEY_ID)                      ' 注册热键
        End If
    End Sub

    Private Sub LoadFileAndUpdate() ' 窗口加载 刷新 用
        _globalPresenter.LoadFile()
        m_ListView.DataSource = GlobalModel.CurrentTab.Tips
        m_ListView.Update()
        m_ListView.ClearSelected()
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
        Dim ok = MessageBoxEx.Show("确定退出 DesktopTips 吗？", "关闭", MessageBoxButtons.YesNo, MessageBoxIcon.Question, Me)
        If ok = vbYes Then
            Me.Close()
        End If
    End Sub

    Private Sub On_Form_Deactivate(sender As Object, e As EventArgs) Handles Me.Deactivate
        m_ListView.ClearSelected()
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
        ' 加载页面 设置
        Me.IsLoading = True
        LoadSetting()
        SetupOpacityButtons()
        SetupAssistButtons()

        Me.CanMouseLeaveFunc = Function() As Boolean
            Return m_menu_ListPopupMenu.PopupControl Is Nothing AndAlso
                   m_menu_TabPopupMenu.PopupControl Is Nothing AndAlso
                   m_menu_MoveTipsSubMenu.PopupControl Is Nothing AndAlso
                   m_menu_HighlightSubMenu.PopupControl Is Nothing AndAlso
                   _isMenuPopuping = False
        End Function
        Me.IsLoading = False

        ' 加载文件 更新内容
        LoadFileAndUpdate()
    End Sub

#End Region

#Region "标签: 增删改 移动 复制粘贴 全选" ' TODO

    Private Sub InsertTip(sender As Object, e As EventArgs) Handles m_btn_InsertTip.Click, m_popup_InsertTip.Click
        If _tipPresenter.Insert() Then
            m_ListView.Update()
            m_ListView.SetSelectOnly(0)
            m_ListView.SetSelectOnly(m_ListView.ItemCount - 1)
        End If
    End Sub

    Private Sub DeleteTip(sender As Object, e As EventArgs) Handles m_btn_RemoveTips.Click, m_popup_RemoveTips.Click
        Dim index As Integer = m_ListView.SelectedIndex
        If m_ListView.SelectedItems IsNot Nothing AndAlso _tipPresenter.Delete(m_ListView.SelectedItems) Then
            m_ListView.Update()
            m_ListView.SetSelectOnly(index)
        End If
    End Sub

    Private Sub UpdateTip(sender As Object, e As EventArgs) Handles m_ListView.DoubleClick, m_popup_UpdateTip.Click
        Dim index = m_ListView.SelectedIndex
        If m_ListView.ControlKey Then
            If m_ListView.SelectedItems.Count > 0 Then
                _tipPresenter.ViewAllLinks(m_ListView.SelectedItems)
            End If
        ElseIf m_ListView.SelectedCount = 1 AndAlso _tipPresenter.Update(m_ListView.SelectedItem) Then
            m_ListView.Update()
            m_ListView.SetSelectOnly(index)
        End If
    End Sub

    Private Sub MoveTipTop(sender As Object, e As EventArgs) Handles m_popup_MoveTopTop.Click
        If _tipPresenter.MoveTop(m_ListView.SelectedItem) Then
            m_ListView.Update()
            m_ListView.SetSelectOnly(0)
        End If
    End Sub

    Private Sub MoveTipBottom(sender As Object, e As EventArgs) Handles m_popup_MoveTipBottom.Click
        If _tipPresenter.MoveBottom(m_ListView.SelectedItem) Then
            m_ListView.Update()
            m_ListView.SetSelectOnly(m_ListView.ItemCount - 1)
        End If
    End Sub

    Private Sub MoveTipUp(sender As Object, e As EventArgs) Handles m_popup_MoveTipUp.Click, m_btn_MoveTipUp.Click
        If m_ListView.SelectedIndex >= 1 AndAlso _tipPresenter.MoveUp(m_ListView.SelectedItem) Then
            m_ListView.Update()
            m_ListView.SetSelectOnly(m_ListView.SelectedIndex - 1)
            If sender.Tag = "True" Then
                NativeMethod.MouseMoveUp(Cursor.Position, HEIGHT_17)
            End If
        End If
    End Sub

    Private Sub MoveDownTip(sender As Object, e As EventArgs) Handles m_popup_MoveTipDown.Click, m_btn_MoveTipDown.Click
        If m_ListView.SelectedIndex <= m_ListView.ItemCount - 2 AndAlso _tipPresenter.MoveDown(m_ListView.SelectedItem) Then
            m_ListView.Update()
            m_ListView.SetSelectOnly(m_ListView.SelectedIndex + 1)
            If sender.Tag = "True" Then
                NativeMethod.MouseMoveDown(Cursor.Position, HEIGHT_17)
            End If
        End If
    End Sub

    Private Sub CopyTip(sender As Object, e As EventArgs) Handles m_popup_CopyTips.Click
        _tipPresenter.Copy(m_ListView.SelectedItems)
    End Sub

    Private Sub PasteAppendToTip(sender As Object, e As EventArgs) Handles m_popup_PasteAppendToTip.Click
        If m_ListView.SelectedCount = 1 AndAlso _tipPresenter.Paste(m_ListView.SelectedItem) Then
            m_ListView.Update()
        End If
    End Sub

    Private Sub SelectAllTips(sender As Object, e As EventArgs) Handles m_popup_SelectAllTips.Click
        For i = 0 To m_ListView.ItemCount - 1
            m_ListView.SetSelected(i, True)
        Next
    End Sub

#End Region

#Region "标签: 查找 打开 浏览 浏览器" ' TODO

    Private Sub FindTips(sender As Object, e As EventArgs) Handles m_popup_FindTips.Click
        _tipPresenter.Search()
    End Sub

    Private Sub OpenFileDir(sender As Object, e As EventArgs) Handles m_popup_OpenDir.Click
        _globalPresenter.OpenFileDir()
    End Sub

    Private Sub ViewCurrentTips(sender As Object, e As EventArgs) Handles m_popup_ViewCurrentTips.Click
        _tipPresenter.ViewList(m_ListView.Items, False)
    End Sub

    Private Sub ViewAllTips(sender As Object, e As EventArgs) Handles m_popup_ViewAllTips.Click
        _tipPresenter.ViewList(GlobalModel.Tabs.SelectMany(Function(tab) 
            Return tab.Tips.Select(Function(tip) New TipItem($"[{tab.Title}] - {tip.Content}", tip.ColorId))
        End Function), False)
    End Sub

    Private Sub ViewCurrentHighlights(sender As Object, e As EventArgs) Handles m_popup_ViewCurrentHighlights.Click
        _tipPresenter.ViewList(m_ListView.Items, True)
    End Sub

    Private Sub ViewAllHighlights(sender As Object, e As EventArgs) Handles m_popup_ViewAllHighlights.Click
        _tipPresenter.ViewList(GlobalModel.Tabs.SelectMany(Function(tab) 
            Return tab.Tips.Select(Function(tip) New TipItem($"[{tab.Title}] - {tip.Content}", tip.ColorId))
        End Function), True)
    End Sub

    Private Sub ViewTipAllLinks(sender As Object, e As EventArgs) Handles m_popup_ViewLinksInTips.Click
        _tipPresenter.ViewAllLinks(m_ListView.SelectedItems)
    End Sub

#End Region

#Region "标签: 高亮 设置颜色 刷新" ' TODO

    Private Sub HighLightTips(sender As Object, e As EventArgs)
        If m_ListView.SelectedCount > 0 Then
            Dim indices As New List(Of Integer)(m_ListView.SelectedIndices.Cast(Of Integer)().ToList())
            Dim color = CType(sender.Tag, TipColor)
            Dim isSingle = m_ListView.SelectedCount = 1
            Dim items = m_ListView.SelectedItems.ToList()
            Dim item = items.First()
            Dim ok As Boolean

            If isSingle Then
                If item.IsHighLight AndAlso item.ColorId = color.Id Then ' 已经高亮并且是当前颜色
                    ok = _tipPresenter.HighlightTips(m_ListView.SelectedItems, Nothing)
                Else
                    ok = _tipPresenter.HighlightTips(m_ListView.SelectedItems, color)
                End If
            Else
                If items.Where(Function(i) i.ColorId = color.Id).Count = items.Count Then ' 所有都是同一个颜色
                    ok = _tipPresenter.HighlightTips(m_ListView.SelectedItems, Nothing)
                Else
                    ok = _tipPresenter.HighlightTips(m_ListView.SelectedItems, color)
                End If
            End If

            If ok Then
                m_ListView.Update()
                m_ListView.SetSelectOnly(indices.ToArray())
                m_menu_HighlightSubMenu.ClosePopup()
            End If
        End If
    End Sub

    Private Sub CheckHighlightChecked() ' 设置弹出高亮菜单 用
        Dim isSel = m_ListView.SelectedCount > 0
        For Each btn As DD.ButtonItem In m_menu_HighlightSubMenu.SubItems
            Dim color = TryCast(btn.Tag, TipColor)
            If color IsNot Nothing Then
                btn.Enabled = isSel
                m_menu_HighlightSubMenu.Checked = isSel AndAlso m_ListView.SelectedItems.Any(Function(i) i.IsHighLight)
                btn.Checked = isSel AndAlso m_ListView.SelectedItems.All(Function(t) t.ColorId = color.Id)
            End If
        Next
    End Sub

    Private Sub On_BtnSetupHighlightColors_Click(sender As Object, e As EventArgs) Handles m_popup_SetupColors.Click
        _tipPresenter.SetupHighlightColor(Sub()
            m_ListView.Update()
        End Sub)
    End Sub

    Private Sub On_BtnRefresh_Click(sender As Object, e As EventArgs) Handles m_popup_Refresh.Click
        LoadFileAndUpdate()
    End Sub

#End Region

#Region "分组: 增删改 拖动分组" ' TODO

    Private Sub InsertTab(sender As Object, e As EventArgs) Handles m_popup_NewTab.Click
        If _tabPresenter.Insert() Then
            m_TabView.Update()
            m_TabView.SelectedTabIndex = m_TabView.TabCount - 1
        End If
    End Sub

    Private Sub DeleteTab(sender As Object, e As EventArgs) Handles m_popup_DeleteTab.Click
        If m_TabView.SelectedTab IsNot Nothing Then
            If m_TabView.TabCount = 1 Then
                MessageBoxEx.Show("无法删除最后一个分组。", "删除", MessageBoxButtons.OK, MessageBoxIcon.Error, Me)
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
        If m_TabView.SelectedTab IsNot Nothing AndAlso _tabPresenter.Update(m_TabView.SelectedTab.TabSource) Then
            m_TabView.Update()
        End If
    End Sub

    Private Sub On_TabView_TabMoved(sender As Object, e As DD.SuperTabStripTabMovedEventArgs) Handles m_TabView.TabMoved
        Dim newTabs As List(Of Tab) = e.NewOrder.Select(Function(item) CType(item, TabView.TabViewItem).TabSource).ToList()
        GlobalModel.Tabs = newTabs
        GlobalModel.CurrentTab = m_TabView.SelectedTab.TabSource
        _globalPresenter.SaveFile()
    End Sub

    Private Sub On_TabView_DoubleClick(sender As Object, e As EventArgs) Handles m_TabView.DoubleClick
        If m_TabView.GetItemFromPoint(m_TabView.PointToClient(Cursor.Position)) Is Nothing Then
            InsertTab(sender, New EventArgs)
        Else
            UpdateTab(sender, New EventArgs)
        End If
    End Sub

#End Region

#Region "分组: 选择 移动至" ' TODO

    Private Sub On_TabView_SelectedTabChanged(sender As Object, e As DD.SuperTabStripSelectedTabChangedEventArgs) Handles m_TabView.SelectedTabChanged
        HideAssistButtons()
        If m_TabView.SelectedTabIndex <> - 1 AndAlso m_TabView.SelectedTab.TabSource IsNot Nothing Then
            GlobalModel.CurrentTab = m_TabView.SelectedTab.TabSource
            m_ListView.DataSource = GlobalModel.CurrentTab.Tips
            m_ListView.Update()
            m_ListView.ClearSelected()
        End If
    End Sub

    Private Function GetUnselectedTabMoveButtonList(all As Boolean) As IEnumerable(Of DD.ButtonItem) ' SetupMoveToButtons (2) 用
        Dim btns As New List(Of DD.ButtonItem)
        Dim currIdx = 0
        For Each tab In GlobalModel.Tabs
            If m_TabView.SelectedTab IsNot Nothing AndAlso m_TabView.SelectedTab.TabSource.Title <> tab.Title Then
                Dim button As New DD.ButtonItem() With {.Tag = New Object() {tab, all}, .Text = $"{tab.Title}(&{currIdx + 1})"}
                AddHandler Button.Click, AddressOf MoveTipToTab
                currIdx += 1
                btns.Add(Button)
            End If
        Next
        Return btns
    End Function

    Private Sub MoveTipToTab(sender As DD.ButtonItem, e As EventArgs)
        Dim src As Tab = GlobalModel.CurrentTab
        Dim dest As Tab = sender.Tag(0)
        Dim all As Boolean = sender.Tag(1)
        Dim items As IEnumerable(Of TipItem) = If(all, m_ListView.Items.ToList(), m_ListView.SelectedItems)

        Dim tipItems As IEnumerable(Of TipItem) = If(TryCast(items, TipItem()), items.ToArray())
        If _tabPresenter.MoveItems(tipItems, src, dest) Then
            m_TabView.Update()
            m_TabView.SetSelected(dest)

            m_ListView.ClearSelected()
            For Each item As TipItem In tipItems
                Dim idx As Integer = GlobalModel.CurrentTab.Tips.IndexOf(item)
                If idx <> - 1 Then
                    m_ListView.SetSelected(idx, True)
                End If
            Next
        End If
    End Sub

#End Region

#Region "显示: 透明度 辅助按钮 移动至菜单 高亮菜单" ' TODO

    Private ReadOnly _opacities() As Double = {0.2, 0.4, 0.6, 0.8, 1}
    Private ReadOnly _opacityButtons(_opacities.Length - 1) As DD.ButtonItem
    Private Const _eps As Double = 0.01

    Private Sub SetupOpacityButtons() ' 窗口加载 用
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

    Private Sub SetupAssistButtons() ' 窗口加载 用
        m_btn_MoveTipUp.Visible = False
        m_btn_MoveTipUp.Height = (HEIGHT_17 + 1) / 2
        m_btn_MoveTipUp.Width = HEIGHT_17
        m_btn_MoveTipDown.Visible = False
        m_btn_MoveTipDown.Height = (HEIGHT_17 + 1) / 2
        m_btn_MoveTipDown.Width = HEIGHT_17
        m_ListView.WheeledFunc = Sub() HideAssistButtons()
    End Sub

    Private Sub ShowAssistButtons() ' 列表选中更改 列表按下 列表大小调整 窗口大小调整 用
        Dim rect As Rectangle = m_ListView.GetItemRectangle(m_ListView.SelectedIndex)
        rect.Offset(m_ListView.Location)
        rect.Offset(2, 2)

        m_btn_MoveTipUp.Top = rect.Top
        m_btn_MoveTipUp.Left = rect.Left + rect.Width - m_btn_MoveTipDown.Width
        m_btn_MoveTipUp.Visible = True
        m_btn_MoveTipDown.Top = m_btn_MoveTipUp.Top + m_btn_MoveTipUp.Height - 1
        m_btn_MoveTipDown.Left = m_btn_MoveTipUp.Left
        m_btn_MoveTipDown.Visible = True
    End Sub

    Private Sub HideAssistButtons() ' 列表选中更改 列表按下 列表滚动 分组选中更改 用
        m_btn_MoveTipUp.Visible = False
        m_btn_MoveTipDown.Visible = False
    End Sub

    Private Sub SetupMoveToButtons() ' 移动至菜单弹出 用
        Dim partBtns As IEnumerable(Of DD.ButtonItem) = GetUnselectedTabMoveButtonList(False) ' TipPopup
        Dim allBtns As IEnumerable(Of DD.ButtonItem) = GetUnselectedTabMoveButtonList(True) ' TabPopup
        Dim canMovePart = partBtns.Count <> 0 AndAlso m_ListView.SelectedCount <> 0
        Dim canMoveAll = allBtns.Count <> 0 AndAlso m_ListView.ItemCount <> 0

        m_menu_MoveTipsSubMenu.SubItems.Clear()
        For Each btn As DD.ButtonItem In partBtns
            m_menu_MoveTipsSubMenu.SubItems.Add(btn)
        Next
        m_menu_MoveTipsSubMenu.Enabled = canMovePart
        m_menu_MoveTipsSubMenu.ShowSubItems = canMovePart

        m_menu_MoveToTabSubMenu.SubItems.Clear()
        For Each btn As DD.ButtonItem In allBtns
            m_menu_MoveToTabSubMenu.SubItems.Add(btn)
        Next
        m_menu_MoveToTabSubMenu.Enabled = canMoveAll
        m_menu_MoveToTabSubMenu.ShowSubItems = canMoveAll
    End Sub

    Private Sub SetupHighLightButtons() ' 高亮菜单弹出 用
        m_menu_HighlightSubMenu.SubItems.Clear()
        For Each colorItem In GlobalModel.Colors
            Dim btn As New DD.ButtonItem() With {.Text = $"{colorItem.Id}: {colorItem.Name}", .Tag = colorItem}
            Dim bm As New Bitmap(16, 16)
            Dim g As Graphics = Graphics.FromImage(bm)
            g.FillRectangle(New SolidBrush(colorItem.Color), New Rectangle(2, 2, 12, 12))
            btn.Image = bm
            AddHandler btn.Click, AddressOf HighLightTips
            m_menu_HighlightSubMenu.SubItems.Add(btn)
        Next
        m_menu_HighlightSubMenu.SubItems.Add(m_popup_SetupColors)

        CheckHighlightChecked()
    End Sub

#End Region

#Region "显示: 可用性判断 列表选择 大小调整" ' TODO

    Private Sub CheckListItemEnabled() ' 列表选中更改 列表菜单弹出 用
        Dim isNotEmpty As Boolean = m_ListView.SelectedCount > 0
        Dim isSingle As Boolean = m_ListView.SelectedCount = 1
        Dim isTop As Boolean = m_ListView.SelectedIndex = 0
        Dim isBottom As Boolean = m_ListView.SelectedIndex = m_ListView.ItemCount - 1

        ' 辅助按钮 位置按钮
        m_btn_MoveTipUp.Enabled = isSingle And Not isTop
        m_btn_MoveTipDown.Enabled = isSingle And Not isBottom
        m_popup_MoveTipUp.Enabled = isSingle And Not isTop
        m_popup_MoveTopTop.Enabled = isSingle And Not isTop
        m_popup_MoveTipBottom.Enabled = isSingle And Not isBottom
        m_popup_MoveTipDown.Enabled = isSingle And Not isBottom

        ' 删改复制粘贴
        m_btn_RemoveTips.Enabled = isNotEmpty
        m_popup_RemoveTips.Enabled = isNotEmpty
        m_popup_UpdateTip.Enabled = isSingle
        m_popup_CopyTips.Enabled = isNotEmpty
        m_popup_PasteAppendToTip.Enabled = isSingle

        ' 高亮
        ' CheckHighlightChecked()

        ' 浏览器
        m_popup_ViewLinksInTips.Enabled = _tipPresenter.GetLinks(m_ListView.SelectedItems).Count >= 1

        ' 菜单标签
        m_popup_SelectedTipsCountLabel.Visible = isNotEmpty
        m_popup_SelectedTipsTextLabel.Visible = isNotEmpty
    End Sub

    Private Sub On_ListView_SelectedIndexChangedAndMouseDown(sender As Object, e As EventArgs) Handles m_ListView.SelectedIndexChanged, m_ListView.MouseDown
        CheckListItemEnabled()
        If m_ListView.SelectedCount = 1 Then
            ShowAssistButtons()
        Else
            HideAssistButtons()
        End If
    End Sub

    Private Sub On_ListViewAndForm_SizeChanged(sender As Object, e As EventArgs) Handles m_ListView.SizeChanged, Me.SizeChanged
        If m_btn_MoveTipUp.Visible = True AndAlso m_ListView.SelectedCount = 1 Then
            ShowAssistButtons()
        End If
    End Sub

    Private Sub On_TabView_ItemClick(sender As Object, e As EventArgs) Handles m_TabView.ItemClick
        m_ListView.ClearSelected()
    End Sub

    Private Sub On_BtnResize_MouseMove(sender As Object, e As MouseEventArgs) Handles m_btn_Resize.MouseMove
        If e.Button = MouseButtons.Left Then
            Me.Width = PushDownWindowSize.Width + Cursor.Position.X - PushDownMousePosition.X
            m_ListView.Refresh()
        End If
    End Sub

    Private Sub On_BtnResize_MouseUp(sender As Object, e As MouseEventArgs) Handles m_btn_Resize.MouseUp
        m_ListView.Refresh()
    End Sub

#End Region

#Region "显示: 弹出菜单 透明度" ' TODO

    Private Sub Popup(item As DD.ButtonItem, isCheck As Boolean)
        If Not isCheck OrElse (Not DD.ButtonItem.IsOnPopup(item) And Not DD.ButtonItem.IsOnPopup(m_menu_ListPopupMenu)) Then
            Dim x = Me.Left + m_btn_OpenListPopup.Left
            Dim y = Me.Top + m_btn_OpenListPopup.Top + m_btn_OpenListPopup.Height - 1
            item.Popup(x, y)
        End If
    End Sub

    Private Sub On_BtnOpenPopupMenu_Click(sender As Object, e As EventArgs) Handles m_btn_OpenListPopup.Click
        Popup(m_menu_ListPopupMenu, False)
    End Sub

    Private Sub On_BtnMoveTipsSubMenu_Click(sender As DD.ButtonItem, e As EventArgs) Handles m_menu_MoveTipsSubMenu.Click
        If m_menu_MoveTipsSubMenu.SubItems.Count <> 0 Then
            Popup(m_menu_MoveTipsSubMenu, True)
        End If
    End Sub

    Private Sub On_BtnHighLightTips_Click(sender As DD.ButtonItem, e As EventArgs) Handles m_menu_HighlightSubMenu.Click
        If m_ListView.SelectedCount > 0 Then
            Popup(m_menu_HighlightSubMenu, True)
        End If
    End Sub

    Private Sub On_ListPopupMenu_PopupOpen(sender As Object, e As DD.PopupOpenEventArgs) Handles m_menu_ListPopupMenu.PopupOpen
        On Error Resume Next
        m_menu_TabPopupMenu.SubItems.RemoveRange({m_popup_OtherLabel, m_menu_FileSubMenu, m_popup_Refresh, m_menu_SyncDataSubMenu, m_menu_WindowSubMenu, m_popup_Exit})
        m_menu_ListPopupMenu.SubItems.RemoveRange({m_popup_OtherLabel, m_menu_FileSubMenu, m_popup_Refresh, m_menu_SyncDataSubMenu, m_menu_WindowSubMenu, m_popup_Exit})
        m_menu_ListPopupMenu.SubItems.AddRange({m_popup_OtherLabel, m_menu_FileSubMenu, m_popup_Refresh, m_menu_SyncDataSubMenu, m_menu_WindowSubMenu, m_popup_Exit})

        CheckListItemEnabled()
        Dim tipString = String.Join(vbNewLine, m_ListView.SelectedItems.Select(Function(t) t.Content))
        Dim highlightCount = m_ListView.Items.Where(Function(t) t.IsHighLight).Count

        If tipString.Length > 500 Then
            tipString = tipString.Substring(0, 500) & "..."
        End If
        m_popup_SelectedTipsTextLabel.Text = tipString
        m_popup_TipsCountLabel.Text = $"列表 (共 {m_ListView.ItemCount} 项，高亮 {highlightCount} 项)"
        m_popup_SelectedTipsCountLabel.Text = $"当前选中 (共 {m_ListView.SelectedCount} 项)"

        On_BtnMoveTipsAndMoveToTabSubMenu_PopupOpen(sender, e)
        On_BtnHighlightTipsSubMenu_PopupOpen(sender, e)
    End Sub

    Private Sub On_TabPopupMenu_PopupOpen(sender As Object, e As DD.PopupOpenEventArgs) Handles m_menu_TabPopupMenu.PopupOpen
        On Error Resume Next
        m_menu_ListPopupMenu.SubItems.RemoveRange({m_popup_OtherLabel, m_menu_FileSubMenu, m_popup_Refresh, m_menu_SyncDataSubMenu, m_menu_WindowSubMenu, m_popup_Exit})
        m_menu_TabPopupMenu.SubItems.RemoveRange({m_popup_OtherLabel, m_menu_FileSubMenu, m_popup_Refresh, m_menu_SyncDataSubMenu, m_menu_WindowSubMenu, m_popup_Exit})
        m_menu_TabPopupMenu.SubItems.AddRange({m_popup_OtherLabel, m_menu_FileSubMenu, m_popup_Refresh, m_menu_SyncDataSubMenu, m_menu_WindowSubMenu, m_popup_Exit})

        Dim counts = GlobalModel.CurrentTab.Tips.GroupBy(Function(t) t.Color).Select(Function(g) New Tuple(Of TipColor, Integer)(g.Key, g.Count())).OrderBy(Function(g) g.Item1?.Id)
        Dim body = $"总共 {GlobalModel.CurrentTab.Tips.Count} 项"
        body = counts.Aggregate(body, Function(current, g)
            If g.Item1 Is Nothing Then
                Return current & $"<br />无高亮 {g.Item2} 项"
            Else
                Return current & $"<br /><font color=""{g.Item1.HexColor}"">{g.Item1.Name}</font> {g.Item2} 项"
            End If
        End Function)

        m_popup_TabCountLabel.Text = $"分组 (共 {GlobalModel.Tabs.Count} 组)"
        m_popup_CurrentTabLabel.Text = $"当前分组：{GlobalModel.CurrentTab.Title}"
        m_popup_CurrentTabTextLabel.Text = body

        On_BtnMoveTipsAndMoveToTabSubMenu_PopupOpen(sender, e)
    End Sub

    Private Sub On_BtnMoveTipsAndMoveToTabSubMenu_PopupOpen(sender As Object, e As DD.PopupOpenEventArgs) Handles m_menu_MoveTipsSubMenu.PopupOpen, m_menu_MoveToTabSubMenu.PopupOpen
        SetupMoveToButtons()
    End Sub

    Private Sub On_BtnHighlightTipsSubMenu_PopupOpen(sender As Object, e As DD.PopupOpenEventArgs) Handles m_menu_HighlightSubMenu.PopupOpen
        SetupHighLightButtons()
    End Sub

    ' 记录 当前是否有菜单弹出
    Private _isMenuPopuping As Boolean = False

    Private Sub On_SomeMenu_PopupOpen(sender As Object, e As EventArgs) _
        Handles m_menu_ListPopupMenu.PopupOpen, m_menu_TabPopupMenu.PopupOpen, m_TabView.PopupOpen,
                m_menu_MoveTipsSubMenu.PopupOpen, m_menu_HighlightSubMenu.PopupOpen
        _isMenuPopuping = True
        FormOpacityUp()
    End Sub

    Private Sub On_SomeMenu_FinishedAndClose(sender As Object, e As EventArgs) _
        Handles m_menu_ListPopupMenu.PopupFinalized, m_menu_TabPopupMenu.PopupFinalized, m_TabView.PopupClose,
                m_menu_MoveTipsSubMenu.PopupFinalized, m_menu_HighlightSubMenu.PopupFinalized
        If m_menu_ListPopupMenu.PopupControl Is Nothing Then
            _isMenuPopuping = False
            FormOpacityDown()
        End If
    End Sub

#End Region

#Region "显示: 列表高度 热键 置顶 加载保存位置" ' TODO

    Private Sub On_NumericListCount_ValueChanged(sender As Object, e As EventArgs) Handles m_num_ListCount.ValueChanged
        Dim targetHeight As Integer = m_num_ListCount.Value * HEIGHT_17 + HEIGHT_27
        If Me.Height = targetHeight Then Return
        Dim direction As Integer = targetHeight - Me.Height
        Me.Height = targetHeight

        If Not IsLoading Then
            Dim dx As Integer = Cursor.Position.X * UInt16.MaxValue / My.Computer.Screen.Bounds.Width
            Dim dy As Integer = (Cursor.Position.Y + direction) * UInt16.MaxValue / My.Computer.Screen.Bounds.Height
            Const flag = NativeMethod.MouseEvent.MOUSEEVENTF_MOVE Or NativeMethod.MouseEvent.MOUSEEVENTF_ABSOLUTE
            NativeMethod.mouse_event(flag, dx, dy, 0, 0)
        End If
    End Sub

    Private Sub On_BtnShowNumericSetListCount_Click(sender As Object, e As EventArgs) Handles m_popup_ShowSetListCount.Click
        m_popup_ShowSetListCount.Checked = Not m_popup_ShowSetListCount.Checked
        m_num_ListCount.Visible = m_popup_ShowSetListCount.Checked
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
        If My.Settings.SaveTop >= 0 And My.Settings.SaveLeft >= 0 Then
            Me.Top = My.Settings.SaveTop
            Me.Left = My.Settings.SaveLeft
        End If
    End Sub

    Private Sub On_BtnSavePosition_Click(sender As Object, e As EventArgs) Handles m_popup_SavePosition.Click
        Dim flag = If(My.Settings.SaveTop >= 0 And My.Settings.SaveLeft >= 0, "是否保存当前窗口的位置，并覆盖原先保存的位置？", "是否保存当前窗口的位置？")
        Dim ok = MessageBoxEx.Show(flag, "保存位置", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, Me, {"保存并覆盖", "取消"})
        If ok = vbOK Then
            m_popup_LoadPosition.Enabled = True
            My.Settings.SaveTop = Me.Top
            My.Settings.SaveLeft = Me.Left
            My.Settings.Save()
        End If
    End Sub

#End Region
End Class
