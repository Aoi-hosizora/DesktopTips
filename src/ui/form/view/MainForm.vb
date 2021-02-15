Imports DD = DevComponents.DotNetBar

Public Class MainForm

#Region "加载设置、界面、文件 启动退出 热键响应" ' TODO

    ''' <summary>
    ''' 热键 ID，用于：注册、注销、响应热键
    ''' </summary>
    Private Const HotkeyId As Integer = 0

    ''' <summary>
    ''' 列表元素高度 (HEIGHT_17)
    ''' </summary>
    Private ReadOnly Property ListItemHeight As Integer
        Get
            Return m_ListView.ItemHeight
        End Get
    End Property

    ''' <summary>
    ''' 除去列表以外的高度 (HEIGHT_27)
    ''' </summary>
    Private ReadOnly Property ListOutHeight As Integer
        Get
            Return m_num_ListCount.Height + 5
        End Get
    End Property

    ''' <summary>
    ''' 启动后处理，加载设置、更新界面
    ''' </summary>
    Private Sub InitUiFromSetting()
        ' 大小 位置 透明度 置顶
        Top = My.Settings.Top
        Left = My.Settings.Left
        Width = My.Settings.Width
        Height = m_num_ListCount.Value * ListItemHeight + ListOutHeight
        MaxOpacity = My.Settings.MaxOpacity
        TopMost = My.Settings.TopMost

        ' 一些控件状态
        m_num_ListCount.Value = My.Settings.ListCount                                           ' 列表高度
        m_popup_LoadPosition.Enabled = My.Settings.SaveLeft <> -1 And My.Settings.SaveTop <> -1 ' 恢复位置
        m_popup_TopMost.Checked = My.Settings.TopMost                                           ' 窗口置顶

        ' 热键，可能会弹出失败
        If My.Settings.IsUseHotKey Then
            _globalPresenter.RegisterHotKey(Handle, My.Settings.HotKey, HotkeyId)
        End If
    End Sub

    ''' <summary>
    ''' 退出前处理，保存窗口位置大小、注销热键
    ''' </summary>
    Private Sub HandleBeforeExit()
        ' 位置 大小
        My.Settings.Top = Top
        My.Settings.Left = Left
        My.Settings.Width = Width
        My.Settings.ListCount = m_num_ListCount.Value
        My.Settings.Save()

        ' 热键，忽略失败
        _globalPresenter.UnregisterHotKey(Handle, HotkeyId)
    End Sub

    ''' <summary>
    ''' 加载文件，并更新列表，用于：窗口加载、刷新
    ''' </summary>
    Private Sub LoadFileAndUpdate()
        _globalPresenter.LoadFile()
        m_ListView.DataSource = GlobalModel.CurrentTab.Tips
        m_ListView.Update()
        m_ListView.ClearSelected()
        m_TabView.DataSource = GlobalModel.Tabs
        m_TabView.Update()
        m_TabView.SelectedTabIndex = 0
    End Sub

    ''' <summary>
    ''' 窗口加载事件，加载设置、界面、文件
    ''' </summary>
    Private Sub On_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' 加载设置，初始化UI，包括窗口属性、菜单、辅助按钮
        InitUiFromSetting()
        SetupOpacityButtons()
        SetupAssistButtons()

        ' 是否允许鼠标移开回调
        MouseLeaveCallback = Function() HasNoMenuPopuping

        ' 加载文件，更新内容
        LoadFileAndUpdate()
    End Sub

    ''' <summary>
    ''' 退出应用，用于：按钮事件、菜单事件
    ''' </summary>
    Private Sub ExitApplication(sender As Object, e As EventArgs) Handles m_btn_Exit.Click, m_popup_Exit.Click
        Dim ok = MessageBoxEx.Show("确定退出 DesktopTips 吗？", "关闭", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, Me)
        If ok = vbYes Then
            Close()
        End If
    End Sub

    ''' <summary>
    ''' 窗口关闭事件，保存设置、注销热键
    ''' </summary>
    Private Sub On_Form_Closed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        HandleBeforeExit()
    End Sub

    ''' <summary>
    ''' 失去焦点，透明化窗口
    ''' </summary>
    Private Sub On_Form_Deactivate(sender As Object, e As EventArgs) Handles Me.Deactivate
        If Opacity > MaxOpacity Then
            FormOpacityDown()
        End If
    End Sub

    ''' <summary>
    ''' 响应热键，置窗口前台并激活
    ''' </summary>
    Protected Overrides Sub WndProc(ByRef m As Message)
        If m.Msg = NativeMethod.WM_HOTKEY AndAlso m.WParam.ToInt32() = HotkeyId Then
            NativeMethod.SetForegroundWindow(Handle)
            Activate()
            FormOpacityUp()
        End If
        MyBase.WndProc(m)
    End Sub

#End Region

#Region "标签: 增删改 移动 复制粘贴全选 完成 打开链接 查找" ' TODO

    ''' <summary>
    ''' 插入标签，用于：按钮事件、菜单事件
    ''' </summary>
    Private Sub InsertTip(sender As Object, e As EventArgs) Handles m_btn_InsertTip.Click, m_popup_InsertTip.Click
        If _tipPresenter.Insert() Then
            m_ListView.Update()
            m_ListView.SetSelectedItems(0)
            m_ListView.SetSelectedItems(m_ListView.ItemCount - 1)
        End If
    End Sub

    ''' <summary>
    ''' 删除标签，用于：按钮事件、菜单事件
    ''' </summary>
    Private Sub DeleteTip(sender As Object, e As EventArgs) Handles m_btn_RemoveTips.Click, m_popup_RemoveTips.Click
        Dim index As Integer = m_ListView.SelectedIndex
        If m_ListView.SelectedItems IsNot Nothing AndAlso _tipPresenter.Delete(m_ListView.SelectedItems) Then
            m_ListView.Update()
            m_ListView.SetSelectedItems(index)
        End If
    End Sub

    ''' <summary>
    ''' 修改标签或浏览连接，用于：双击列表事件、菜单事件
    ''' </summary>
    Private Sub UpdateTipOrViewLinks(sender As Object, e As EventArgs) Handles m_ListView.DoubleClick, m_popup_UpdateTip.Click
        Dim index = m_ListView.SelectedIndex
        If My.Computer.Keyboard.CtrlKeyDown AndAlso m_ListView.SelectedItems.Count > 0 Then
            _tipPresenter.ViewAllLinks(m_ListView.SelectedItems) ' 浏览链接
        ElseIf m_ListView.SelectedCount = 1 AndAlso _tipPresenter.Update(m_ListView.SelectedItem) Then
            m_ListView.Update()
            m_ListView.SetSelectedItems(index)
        End If
    End Sub

    ''' <summary>
    ''' 置顶标签，用于：菜单事件
    ''' </summary>
    Private Sub MoveTipTop(sender As Object, e As EventArgs) Handles m_popup_MoveTopTop.Click
        If _tipPresenter.MoveTop(m_ListView.SelectedItem) Then
            m_ListView.Update()
            m_ListView.SetSelectedItems(0)
        End If
    End Sub

    ''' <summary>
    ''' 置底标签，用于：菜单事件
    ''' </summary>
    Private Sub MoveTipBottom(sender As Object, e As EventArgs) Handles m_popup_MoveTipBottom.Click
        If _tipPresenter.MoveBottom(m_ListView.SelectedItem) Then
            m_ListView.Update()
            m_ListView.SetSelectedItems(m_ListView.ItemCount - 1)
        End If
    End Sub

    ''' <summary>
    ''' 上移标签，用于：按钮事件、菜单事件
    ''' </summary>
    Private Sub MoveTipUp(sender As Object, e As EventArgs) Handles m_popup_MoveTipUp.Click, m_btn_MoveTipUp.Click
        If m_ListView.SelectedIndex >= 1 AndAlso _tipPresenter.MoveUp(m_ListView.SelectedItem) Then
            m_ListView.Update()
            m_ListView.SetSelectedItems(m_ListView.SelectedIndex - 1)
            If sender.Tag = "True" Then
                NativeMethod.MouseMoveUp(Cursor.Position, ListItemHeight)
            End If
        End If
    End Sub

    ''' <summary>
    ''' 下移标签，用于：按钮事件、菜单事件
    ''' </summary>
    Private Sub MoveDownTip(sender As Object, e As EventArgs) Handles m_popup_MoveTipDown.Click, m_btn_MoveTipDown.Click
        If m_ListView.SelectedIndex <= m_ListView.ItemCount - 2 AndAlso _tipPresenter.MoveDown(m_ListView.SelectedItem) Then
            m_ListView.Update()
            m_ListView.SetSelectedItems(m_ListView.SelectedIndex + 1)
            If sender.Tag = "True" Then
                NativeMethod.MouseMoveDown(Cursor.Position, ListItemHeight)
            End If
        End If
    End Sub

    ''' <summary>
    ''' 复制，用于：菜单事件
    ''' </summary>
    Private Sub CopyTip(sender As Object, e As EventArgs) Handles m_popup_CopyTips.Click
        _tipPresenter.Copy(m_ListView.SelectedItems)
    End Sub

    ''' <summary>
    ''' 粘贴到标签，用于：菜单事件
    ''' </summary>
    Private Sub PasteAppendToTip(sender As Object, e As EventArgs) Handles m_popup_PasteAppendToTip.Click
        If m_ListView.SelectedCount = 1 AndAlso _tipPresenter.Paste(m_ListView.SelectedItem) Then
            m_ListView.Update()
        End If
    End Sub

    ''' <summary>
    ''' 全选列表，用于：菜单事件
    ''' </summary>
    Private Sub SelectAllTips(sender As Object, e As EventArgs) Handles m_popup_SelectAllTips.Click
        For i = 0 To m_ListView.ItemCount - 1
            m_ListView.SetSelected(i, True)
        Next
    End Sub

    ''' <summary>
    ''' 标记为完成，用于：菜单事件
    ''' </summary>
    Private Sub CheckDoneTip(sender As Object, e As EventArgs) Handles m_menu_CheckDone.Click
        Dim indices = m_ListView.SelectedIndices.Cast(Of Integer)().ToList()
        If m_ListView.SelectedItems IsNot Nothing AndAlso _tipPresenter.CheckTipsDone(m_ListView.SelectedItems) Then
            m_ListView.Update()
            m_ListView.SetSelectedItems(indices.ToArray())
        End If
    End Sub

    ''' <summary>
    ''' 浏览链接，用于：菜单事件
    ''' </summary>
    Private Sub ViewTipAllLinks(sender As Object, e As EventArgs) Handles m_popup_ViewLinksInTips.Click
        _tipPresenter.ViewAllLinks(m_ListView.SelectedItems)
    End Sub

    ''' <summary>
    ''' 查找，用于：菜单事件
    ''' </summary>
    Private Sub FindTips(sender As Object, e As EventArgs) Handles m_popup_FindTips.Click
        _tipPresenter.Search()
    End Sub

#End Region

#Region "标签: 高亮 设置颜色 高亮菜单 打开 浏览 刷新" ' TODO

    ''' <summary>
    ''' 高亮或取消高亮，用于：菜单事件
    ''' </summary>
    Private Sub HighlightTips(sender As Object, e As EventArgs)
        If m_ListView.SelectedCount = 0 Then
            Return
        End If
        Dim indices = m_ListView.SelectedIndices.Cast(Of Integer)().ToList()
        Dim items = m_ListView.SelectedItems.ToList()
        Dim color = CType(sender.Tag, TipColor) ' 颜色存在 Tag 中

        Dim ok As Boolean
        Dim hasColor1 = m_ListView.SelectedCount = 1 AndAlso items.First().IsHighLight AndAlso items.First().ColorId = color.Id ' 已经高亮并且是当前颜色
        Dim hasColor2 = m_ListView.SelectedCount > 1 AndAlso items.Where(Function (i) i.ColorId = color.Id).Count = items.Count ' 所有选择项都是同种颜色
        If Not hasColor1 OrElse Not hasColor2 Then
            ok = _tipPresenter.HighlightTips(m_ListView.SelectedItems, color) ' 进行高亮
        Else
            ok = _tipPresenter.HighlightTips(m_ListView.SelectedItems, Nothing)
        End If
        If ok Then
            m_ListView.Update()
            m_ListView.SetSelectedItems(indices.ToArray())
            m_menu_HighlightSubMenu.ClosePopup() ' 需要手动关闭弹出菜单
        End If
    End Sub

    ''' <summary>
    ''' 设置高亮颜色，用于：菜单事件
    ''' </summary>
    Private Sub SetupHighlightColors(sender As Object, e As EventArgs) Handles m_popup_SetupColors.Click
        _tipPresenter.SetupHighlightColor(Sub() m_ListView.Update())
    End Sub

    ''' <summary>
    ''' 弹出高亮菜单，添加颜色、设置选中，用于：菜单弹出事件
    ''' </summary>
    Private Sub PopupOpenHighlightMenu(sender As Object, e As DD.PopupOpenEventArgs) Handles m_menu_HighlightSubMenu.PopupOpen
        ' 插入高亮颜色菜单
        m_menu_HighlightSubMenu.SubItems.Clear()
        For Each colorItem In GlobalModel.Colors
            Dim btn As New DD.ButtonItem() With {.Text = $"{colorItem.Id}: {colorItem.Name}", .Tag = colorItem}
            Dim bm As New Bitmap(16, 16)
            Dim g As Graphics = Graphics.FromImage(bm)
            g.FillRectangle(New SolidBrush(colorItem.Color), New Rectangle(2, 2, 12, 12))
            btn.Image = bm
            AddHandler btn.Click, AddressOf HighlightTips
            m_menu_HighlightSubMenu.SubItems.Add(btn)
        Next
        m_menu_HighlightSubMenu.SubItems.Add(m_popup_SetupColors)

        ' 检查当前高亮情况，设置 Checked
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

    ''' <summary>
    ''' 打开文件，用于：菜单事件
    ''' </summary>
    Private Sub OpenFileDir(sender As Object, e As EventArgs) Handles m_popup_OpenDir.Click
        _globalPresenter.OpenFileDir()
    End Sub

    ''' <summary>
    ''' 浏览当前列表内容，用于：菜单事件
    ''' </summary>
    Private Sub ViewCurrentTips(sender As Object, e As EventArgs) Handles m_popup_ViewCurrentTips.Click
        _tipPresenter.ViewList(m_ListView.Items, False)
    End Sub

    ''' <summary>
    ''' 浏览当前列表高亮内容，用于：菜单事件
    ''' </summary>
    Private Sub ViewCurrentHighlights(sender As Object, e As EventArgs) Handles m_popup_ViewCurrentHighlights.Click
        _tipPresenter.ViewList(m_ListView.Items, True)
    End Sub

    ''' <summary>
    ''' 浏览所有列表内容，用于：菜单事件
    ''' </summary>
    Private Sub ViewAllTips(sender As Object, e As EventArgs) Handles m_popup_ViewAllTips.Click
        _tipPresenter.ViewList(GlobalModel.Tabs.SelectMany(Function(tab) 
            Return tab.Tips.Select(Function(tip) New TipItem(tip.Id, $"[{tab.Title}] - {tip.Content}", tip.ColorId))
        End Function), False)
    End Sub

    ''' <summary>
    ''' 浏览所有列表高亮内容，用于：菜单事件
    ''' </summary>
    Private Sub ViewAllHighlights(sender As Object, e As EventArgs) Handles m_popup_ViewAllHighlights.Click
        _tipPresenter.ViewList(GlobalModel.Tabs.SelectMany(Function(tab) 
            Return tab.Tips.Select(Function(tip) New TipItem(tip.Id, $"[{tab.Title}] - {tip.Content}", tip.ColorId))
        End Function), True)
    End Sub

    ''' <summary>
    ''' 刷新数据，用于：菜单事件
    ''' </summary>
    Private Sub RefreshData(sender As Object, e As EventArgs) Handles m_popup_Refresh.Click
        LoadFileAndUpdate()
    End Sub

    ''' <summary>
    ''' 用户操作列表相关，包括更新选择项、辅助按钮，用于：列表选择事件、列表按下事件
    ''' </summary>
    Private Sub ChangeSelectedTipsOrMouseDown(sender As Object, e As EventArgs) Handles m_ListView.SelectedIndexChanged, m_ListView.MouseDown
        CheckListMenuEnable()
        If m_ListView.SelectedCount = 1 Then
            ShowAssistButtons()
        Else
            HideAssistButtons()
        End If
    End Sub

#End Region

#Region "分组: 增删改 拖动 选择" ' TODO

    ''' <summary>
    ''' 插入分组，用于：菜单事件
    ''' </summary>
    Private Sub InsertTab(sender As Object, e As EventArgs) Handles m_popup_NewTab.Click
        If _tabPresenter.Insert() Then
            m_TabView.Update()
            m_TabView.SelectedTabIndex = m_TabView.TabCount - 1
        End If
    End Sub

    ''' <summary>
    ''' 删除分组，用于：菜单事件
    ''' </summary>
    Private Sub DeleteTab(sender As Object, e As EventArgs) Handles m_popup_DeleteTab.Click
        If m_TabView.SelectedTab IsNot Nothing Then
            If m_TabView.TabCount = 1 Then
                MessageBoxEx.Show("无法删除最后一个分组。", "删除", MessageBoxButtons.OK, MessageBoxIcon.Error, Me)
            Else
                Dim currentIndex = m_TabView.SelectedTabIndex
                If _tabPresenter.Delete(m_TabView.SelectedTab.TabSource) Then
                    m_TabView.Update()
                    m_TabView.SelectedTabIndex = currentIndex - 1
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' 修改分组，用于：菜单事件
    ''' </summary>
    Private Sub UpdateTab(sender As Object, e As EventArgs) Handles m_popup_RenameTab.Click
        If m_TabView.SelectedTab IsNot Nothing AndAlso _tabPresenter.Update(m_TabView.SelectedTab.TabSource) Then
            m_TabView.Update()
        End If
    End Sub

    ''' <summary>
    ''' 插入或修改分组，用于：分组双击事件
    ''' </summary>
    Private Sub CreateOrUpdateTab(sender As Object, e As EventArgs) Handles m_TabView.DoubleClick
        If m_TabView.GetItemFromPoint(m_TabView.PointToClient(Cursor.Position)) Is Nothing Then
            InsertTab(sender, New EventArgs)
        Else
            UpdateTab(sender, New EventArgs)
        End If
    End Sub

    ''' <summary>
    ''' 调整分组顺序，用于：菜单事件
    ''' </summary>
    Private Sub ReorderTab(sender As Object, e As DD.SuperTabStripTabMovedEventArgs) Handles m_TabView.TabMoved
        Dim newTabs As List(Of Tab) = e.NewOrder.Select(Function(item) CType(item, TabView.TabViewItem).TabSource).ToList()
        GlobalModel.Tabs = newTabs
        GlobalModel.CurrentTab = m_TabView.SelectedTab.TabSource
        _globalPresenter.SaveFile()
    End Sub

    ''' <summary>
    ''' 用户操作分组相关，包括修改当前分组，用于：分组选择事件
    ''' </summary>
    Private Sub ChangeSelectedTab(sender As Object, e As DD.SuperTabStripSelectedTabChangedEventArgs) Handles m_TabView.SelectedTabChanged
        HideAssistButtons() ' 隐藏辅助按钮
        If m_TabView.SelectedTabIndex <> -1 AndAlso m_TabView.SelectedTab.TabSource IsNot Nothing Then
            GlobalModel.CurrentTab = m_TabView.SelectedTab.TabSource
            m_ListView.DataSource = GlobalModel.CurrentTab.Tips ' 修改当前分组和列表内容
            m_ListView.Update()
            m_ListView.ClearSelected()
        End If
    End Sub

    ''' <summary>
    ''' 点击分组隐藏标签选择，用于：分组点击事件
    ''' </summary>
    Private Sub ClickTabView(sender As Object, e As EventArgs) Handles m_TabView.ItemClick
        m_ListView.ClearSelected()
    End Sub

#End Region

#Region "标签: 移动至分组 移动至分组菜单" ' TODO

    ''' <summary>
    ''' 移动至分组，用于：菜单事件
    ''' </summary>
    Private Sub MoveTipToTab(sender As DD.ButtonItem, e As EventArgs)
        Dim src As Tab = GlobalModel.CurrentTab
        Dim dest As Tab = sender.Tag(0)
        Dim moveAll As Boolean = sender.Tag(1) ' 移动所有
        Dim items As List(Of TipItem) = If(moveAll, m_ListView.Items.ToList(), m_ListView.SelectedItems.ToList())
        If _tabPresenter.MoveItems(items, src, dest) Then
            m_TabView.Update()
            m_TabView.SetSelected(dest)
            m_ListView.ClearSelected()
            m_ListView.SetSelectedItems(items.Select(Function(i) GlobalModel.CurrentTab.Tips.IndexOf(i)).ToArray())
        End If
    End Sub

    ''' <summary>
    ''' 生成当前未选择的按钮列表，用于：设置移动至分组菜单
    ''' </summary>
    Private Function GenerateUnselectedTabButtons(moveAll As Boolean) As IEnumerable(Of DD.ButtonItem)
        Dim btns As New List(Of DD.ButtonItem)
        Dim currIdx = 0 ' 当前 (&n)
        For Each tab In GlobalModel.Tabs
            If m_TabView.SelectedTab IsNot Nothing AndAlso m_TabView.SelectedTab.TabSource.Title <> tab.Title Then
                Dim button As New DD.ButtonItem() With {.Tag = New Object() {tab, moveAll}, .Text = $"{tab.Title}(&{currIdx + 1})"} ' Tag 包括：分组、移动对象
                AddHandler Button.Click, AddressOf MoveTipToTab
                currIdx += 1
                btns.Add(Button)
            End If
        Next
        Return btns
    End Function

    ''' <summary>
    ''' 设置移动至分组菜单，添加分组、设置选中，用于：列表菜单、分组菜单的菜单弹出事件
    ''' </summary>
    Private Sub PopupOpenMoveToMenu(sender As Object, e As DD.PopupOpenEventArgs) Handles m_menu_MoveTipsSubMenu.PopupOpen, m_menu_MoveToTabSubMenu.PopupOpen
        ' 列表菜单
        Dim moveSomeBtns = GenerateUnselectedTabButtons(False)
        Dim canMoveSome = moveSomeBtns.Count <> 0 AndAlso m_ListView.SelectedCount <> 0
        m_menu_MoveTipsSubMenu.SubItems.Clear()
        For Each btn In moveSomeBtns
            m_menu_MoveTipsSubMenu.SubItems.Add(btn)
        Next
        m_menu_MoveTipsSubMenu.Enabled = canMoveSome
        m_menu_MoveTipsSubMenu.ShowSubItems = canMoveSome

        ' 分组菜单
        Dim moveAllBtns = GenerateUnselectedTabButtons(True)
        Dim canMoveAll = moveAllBtns.Count <> 0 AndAlso m_ListView.ItemCount <> 0
        m_menu_MoveToTabSubMenu.SubItems.Clear()
        For Each btn In moveAllBtns
            m_menu_MoveToTabSubMenu.SubItems.Add(btn)
        Next
        m_menu_MoveToTabSubMenu.Enabled = canMoveAll
        m_menu_MoveToTabSubMenu.ShowSubItems = canMoveAll
    End Sub

#End Region

#Region "显示: 可用性检查 弹出菜单 弹出前处理 按键弹出菜单" ' TODO

    ''' <summary>
    ''' 列表菜单的可用性检查，用于：列表选择事件、列表菜单弹出事件
    ''' </summary>
    Private Sub CheckListMenuEnable()
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

        ' 删改 复制粘贴
        m_btn_RemoveTips.Enabled = isNotEmpty
        m_popup_RemoveTips.Enabled = isNotEmpty
        m_popup_UpdateTip.Enabled = isSingle
        m_popup_CopyTips.Enabled = isNotEmpty
        m_popup_PasteAppendToTip.Enabled = isSingle

        ' 浏览器 标记完成
        m_popup_ViewLinksInTips.Enabled = _tipPresenter.GetLinks(m_ListView.SelectedItems).Count >= 1
        m_menu_CheckDone.Enabled = isNotEmpty
        m_menu_CheckDone.Checked = m_ListView.SelectedItems.Any(Function(item) item.Done)

        ' 列表菜单标签
        m_popup_SelectedTipsCountLabel.Visible = isNotEmpty
        m_popup_SelectedTipsTextLabel.Visible = isNotEmpty
    End Sub

    ''' <summary>
    ''' 弹出菜单的检查以及位置设置，用于：各种菜单的弹出
    ''' </summary>
    Private Sub Popup(item As DD.ButtonItem, Optional needCheck As Boolean = True)
        If Not needCheck OrElse (Not DD.ButtonItem.IsOnPopup(item) AndAlso Not DD.ButtonItem.IsOnPopup(m_menu_ListPopupMenu)) Then
            ' 不需要检查 或者 没有菜单在弹出
            Dim x = Left + m_btn_OpenListPopup.Left
            Dim y = Top + m_btn_OpenListPopup.Top + m_btn_OpenListPopup.Height - 1
            item.Popup(x, y) ' 在界面的菜单按钮处弹出
        End If
    End Sub

    ''' <summary>
    ''' 弹出列表菜单，用于：按钮事件
    ''' </summary>
    Private Sub OpenListPopupMenu(sender As Object, e As EventArgs) Handles m_btn_OpenListPopup.Click
        Popup(m_menu_ListPopupMenu, False) ' 直接打开菜单，不需要检查
    End Sub

    ''' <summary>
    ''' 弹出列表菜单，用于：列表键盘事件
    ''' </summary>
    Private Sub OpenListPopupMenuByKeyDown(sender As Object, e As KeyEventArgs) Handles m_ListView.KeyDown
        If e.KeyCode = Keys.OemPeriod And e.Control = True Then
            Popup(m_menu_ListPopupMenu, False) ' Ctrl+.
        End If
    End Sub

    ''' <summary>
    ''' 弹出高亮菜单，用于：菜单快捷键事件
    ''' </summary>
    Private Sub OpenHighlightPopupMenu(sender As DD.ButtonItem, e As EventArgs) Handles m_menu_HighlightSubMenu.Click
        If m_ListView.SelectedCount > 0 Then
            Popup(m_menu_HighlightSubMenu)
        End If
    End Sub

    ''' <summary>
    ''' 列表菜单和分组菜单共用的菜单项，用于菜单弹出事件
    ''' </summary>
    Private ReadOnly _commonMenus As DD.BaseItem() = {m_popup_OtherLabel, m_menu_FileSubMenu, m_popup_Refresh, m_menu_WindowSubMenu, m_popup_Exit}

    ''' <summary>
    ''' 弹出列表菜单，用于：菜单弹出事件
    ''' </summary>
    Private Sub PopupOpenListMenu(sender As Object, e As DD.PopupOpenEventArgs) Handles m_menu_ListPopupMenu.PopupOpen
        On Error Resume Next
        ' 移除并添加公共菜单
        m_menu_TabPopupMenu.SubItems.RemoveRange(_commonMenus)
        m_menu_ListPopupMenu.SubItems.RemoveRange(_commonMenus)
        m_menu_ListPopupMenu.SubItems.AddRange(_commonMenus)

        Dim selItems = m_ListView.SelectedItems.ToList()
        Dim tipString = ""
        Dim labelSize = m_popup_SelectedTipsTextLabel.Width - m_popup_SelectedTipsTextLabel.PaddingLeft - m_popup_SelectedTipsTextLabel.PaddingRight - 8
        For i = 0 To selItems.Count - 1
            If i >= 15 Then
                tipString &= vbNewLine & $"...... (剩下 {selItems.Count - 15} 项)"
                Exit For
            End If
            If tipString.Length > 0 Then tipString &= vbNewLine
            Dim content = selItems.ElementAt(i).Content.Replace(vbNewLine, "↴")
            tipString &= CommonUtil.TrimForEllipsis(content, Font, labelSize)
        Next
        CheckListMenuEnable()
        Dim highlightCount = m_ListView.Items.Where(Function(t) t.IsHighLight).Count

        m_popup_SelectedTipsTextLabel.Text = tipString
        m_popup_TipsCountLabel.Text = $"列表 (共 {m_ListView.ItemCount} 项，高亮 {highlightCount} 项)"
        m_popup_SelectedTipsCountLabel.Text = $"当前选中 (共 {m_ListView.SelectedCount} 项)"

        PopupOpenHighlightMenu(sender, e)
        PopupOpenMoveToMenu(sender, e)
    End Sub

    ''' <summary>
    ''' 弹出分组菜单，用于：菜单弹出事件
    ''' </summary>
    Private Sub PopupOpenTabMenu(sender As Object, e As DD.PopupOpenEventArgs) Handles m_menu_TabPopupMenu.PopupOpen
        On Error Resume Next
        ' 移除并添加公共菜单
        m_menu_TabPopupMenu.SubItems.RemoveRange(_commonMenus)
        m_menu_ListPopupMenu.SubItems.RemoveRange(_commonMenus)
        m_menu_ListPopupMenu.SubItems.AddRange(_commonMenus)

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

        PopupOpenMoveToMenu(sender, e)
    End Sub

    ''' <summary>
    ''' 记录当前是否有菜单弹出，用于：菜单弹出和关闭事件、窗口离开判断事件
    ''' </summary>
    Private _isMenuPopuping As Boolean = False

    ''' <summary>
    ''' 判断当前没有菜单弹出，封装了所有弹出菜单条件
    ''' </summary>
    Private ReadOnly Property HasNoMenuPopuping As Boolean
        Get
            Return m_menu_ListPopupMenu.PopupControl Is Nothing AndAlso
                m_menu_TabPopupMenu.PopupControl Is Nothing AndAlso
                m_menu_HighlightSubMenu.PopupControl Is Nothing AndAlso
                Not _isMenuPopuping
        End Get
    End Property

    ''' <summary>
    ''' 菜单弹出，记录并提高透明度
    ''' </summary>
    Private Sub On_SomeMenu_PopupOpen(sender As Object, e As EventArgs) Handles m_menu_ListPopupMenu.PopupOpen, m_menu_TabPopupMenu.PopupOpen, m_TabView.PopupOpen,
                                                                                m_menu_HighlightSubMenu.PopupOpen
        _isMenuPopuping = True ' 所有菜单弹出都记录
        FormOpacityUp()
    End Sub

    ''' <summary>
    ''' 菜单关闭，记录并提高透明度
    ''' </summary>
    Private Sub On_SomeMenu_Close(sender As Object, e As EventArgs) Handles m_menu_ListPopupMenu.PopupFinalized, m_menu_TabPopupMenu.PopupFinalized, m_TabView.PopupClose,
                                                                            m_menu_HighlightSubMenu.PopupFinalized
        If m_menu_ListPopupMenu.PopupControl Is Nothing Then ' 当前菜单并不是子菜单弹出
            _isMenuPopuping = False ' 记录弹出结束
            FormOpacityDown()
        End If
    End Sub

#End Region

#Region "显示: 透明度菜单 辅助按钮 宽度高度调整 热键 置顶 加载保存位置" ' TODO

    ''' <summary>
    ''' 所有透明度等级
    ''' </summary>
    Private ReadOnly _opacities() As Double = {0.2, 0.4, 0.6, 0.8, 1}

    ''' <summary>
    ''' 所有透明度选项按钮
    ''' </summary>
    Private ReadOnly _opacityButtons As New List(Of DD.ButtonItem)

    ''' <summary>
    ''' 设置透明度按钮菜单，包括点击事件，用于：窗口加载
    ''' </summary>
    Private Sub SetupOpacityButtons()
        _opacityButtons.Clear()
        m_menu_OpacitySubMenu.SubItems.Clear()
        For i = 0 To _opacities.Length - 1
            ' 创建按钮以及绑定事件
            Dim op = _opacities(i)
            Dim newBtn As New DD.ButtonItem With { .Text = $"{CInt(op * 100)}%", .Tag = op }
            AddHandler newBtn.Click, Sub(sender As DD.ButtonItem, e As EventArgs)
                MaxOpacity = sender.Tag ' 修改窗口属性
                My.Settings.MaxOpacity = MaxOpacity
                My.Settings.Save()
                _opacityButtons.ForEach(Sub(b) b.Checked = False)
                sender.Checked = True
            End Sub
            _opacityButtons.Add(newBtn)

            ' 插入到界面
            m_menu_OpacitySubMenu.SubItems.Add(newBtn)
            If Math.Abs(MaxOpacity - newBtn.Tag) < 0.02 Then ' eps
                newBtn.Checked = True
            End If
        Next
    End Sub

    ''' <summary>
    ''' 设置辅助按钮，包括尺寸、不可见、滚轮隐藏，用于：窗口加载
    ''' </summary>
    Private Sub SetupAssistButtons()
        m_btn_MoveTipUp.Visible = False
        m_btn_MoveTipUp.Height = (ListItemHeight + 1) / 2
        m_btn_MoveTipUp.Width = ListItemHeight
        m_btn_MoveTipDown.Visible = False
        m_btn_MoveTipDown.Height = (ListItemHeight + 1) / 2
        m_btn_MoveTipDown.Width = ListItemHeight
        m_ListView.WheeledFunc = Sub() HideAssistButtons()
    End Sub

    ''' <summary>
    ''' 显示辅助按钮，调整位置，用于：列表选择和鼠标按下事件、列表和窗口大小调整事件
    ''' </summary>
    Private Sub ShowAssistButtons()
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

    ''' <summary>
    ''' 隐藏辅助按钮，调整位置，用于：滚轮事件、列表选择和鼠标按下事件、分组选择事件
    ''' </summary>
    Private Sub HideAssistButtons()
        m_btn_MoveTipUp.Visible = False
        m_btn_MoveTipDown.Visible = False
    End Sub

    ''' <summary>
    ''' 按下 Resize 按钮，调整窗口大小
    ''' </summary>
    Private Sub On_BtnResize_MouseMove(sender As Object, e As MouseEventArgs) Handles m_btn_Resize.MouseMove
        If e.Button = MouseButtons.Left Then
            Width = PushDownWindowSize.Width + Cursor.Position.X - PushDownMousePosition.X
            m_ListView.Refresh()
        End If
    End Sub

    ''' <summary>
    ''' 谈起 Resize 按钮，完成窗口大小调整，刷新列表
    ''' </summary>
    Private Sub On_BtnResize_MouseUp(sender As Object, e As MouseEventArgs) Handles m_btn_Resize.MouseUp
        m_ListView.Refresh()
    End Sub

    ''' <summary>
    ''' 大小修改，更新辅助按钮显示，用于：列表和窗口大小修改事件
    ''' </summary>
    Private Sub ChangeListViewOrFormSize(sender As Object, e As EventArgs) Handles m_ListView.SizeChanged, Me.SizeChanged
        If m_btn_MoveTipUp.Visible = True AndAlso m_ListView.SelectedCount = 1 Then
            ShowAssistButtons()
        End If
    End Sub

    ''' <summary>
    ''' 显示调整列表高度输入框，用于：菜单事件
    ''' </summary>
    Private Sub ShowListShownCountBox(sender As Object, e As EventArgs) Handles m_popup_ShowSetListCount.Click
        m_popup_ShowSetListCount.Checked = Not m_popup_ShowSetListCount.Checked
        m_num_ListCount.Visible = m_popup_ShowSetListCount.Checked
    End Sub

    ''' <summary>
    ''' 修改列表高度，用于：值修改事件
    ''' </summary>
    Private Sub ChangeListShownCount(sender As Object, e As EventArgs) Handles m_num_ListCount.ValueChanged
        Dim targetHeight As Integer = m_num_ListCount.Value * ListItemHeight + ListOutHeight
        If Height = targetHeight Then Return
        Dim direction As Integer = targetHeight - Height
        Height = targetHeight

        Dim dx As Integer = Cursor.Position.X * UInt16.MaxValue / My.Computer.Screen.Bounds.Width
        Dim dy As Integer = (Cursor.Position.Y + direction) * UInt16.MaxValue / My.Computer.Screen.Bounds.Height
        Const flag = NativeMethod.MouseEvent.MOUSEEVENTF_MOVE Or NativeMethod.MouseEvent.MOUSEEVENTF_ABSOLUTE
        NativeMethod.mouse_event(flag, dx, dy, 0, 0)
    End Sub

    ''' <summary>
    ''' 设置激活窗口热键，用于：菜单事件
    ''' </summary>
    Private Sub SetupHotkey(sender As Object, e As EventArgs) Handles m_popup_SetupHotkey.Click
        _globalPresenter.SetupHotKey(Handle, HotkeyId)
    End Sub

    ''' <summary>
    ''' 设置调整置顶，用于：菜单事件
    ''' </summary>
    Private Sub SetTopMost(sender As Object, e As EventArgs) Handles m_popup_TopMost.Click
        m_popup_TopMost.Checked = Not m_popup_TopMost.Checked
        TopMost = m_popup_TopMost.Checked
        My.Settings.TopMost = TopMost
        My.Settings.Save()
    End Sub

    ''' <summary>
    ''' 加载保存的位置，用于：菜单事件
    ''' </summary>
    Private Sub LoadPosition(sender As Object, e As EventArgs) Handles m_popup_LoadPosition.Click
        If My.Settings.SaveTop >= 0 And My.Settings.SaveLeft >= 0 Then
            Top = My.Settings.SaveTop
            Left = My.Settings.SaveLeft
        End If
    End Sub

    ''' <summary>
    ''' 保存窗口位置，用于：菜单事件
    ''' </summary>
    Private Sub SavePosition(sender As Object, e As EventArgs) Handles m_popup_SavePosition.Click
        Dim flag = If(My.Settings.SaveTop >= 0 And My.Settings.SaveLeft >= 0, "是否保存当前窗口的位置，并覆盖原先保存的位置？", "是否保存当前窗口的位置？")
        Dim ok = MessageBoxEx.Show(flag, "保存位置", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, Me, {"保存并覆盖", "取消"})
        If ok = vbOK Then
            m_popup_LoadPosition.Enabled = True
            My.Settings.SaveTop = Top
            My.Settings.SaveLeft = Left
            My.Settings.Save()
        End If
    End Sub

#End Region
End Class
