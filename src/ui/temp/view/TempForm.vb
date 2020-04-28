Imports System.Text
Imports DD = DevComponents.DotNetBar

Public Class TempForm

#Region "加载设置 加载列表内容和界面 启动退出 系统事件"

    Private Sub LoadSetting() ' On_Form_Load 用
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

    Private Sub LoadFile() ' On_Form_Load 用
        _globalPresenter.LoadFile()
        ListView.DataSource = GlobalModel.CurrentTab.Tips
        ListView.Update()
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

    Private Sub On_Form_Deactivate(sender As Object, e As EventArgs) Handles Me.Deactivate
        ListView.ClearSelected()
        If Me.Opacity > MaxOpacity Then
            FormOpacityDown()
        End If
    End Sub

    Private Sub On_Form_Closed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        My.Settings.Top = Me.Top
        My.Settings.Left = Me.Left
        My.Settings.Width = Me.Width
        My.Settings.Height = Me.Height
        My.Settings.Save()
        _globalPresenter.UnregisterHotKey(Handle, HOTKEY_ID)
    End Sub

    Private Sub On_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' 窗口显示
        LoadSetting()
        Me.TabStrip.Tabs.Remove(Me.TabItemTest)
        Me.TabStrip.Tabs.Remove(Me.TabItemTest2)
        SetupOpacityButtons()
        SetupAssistButtons()

        ' 窗口透明度降低
        Me.CanMouseLeave = Function() As Boolean
            Return ListPopupMenu.PopupControl Is Nothing AndAlso
                   TabPopupMenu.PopupControl Is Nothing AndAlso
                   TabStrip.ContextMenu Is Nothing AndAlso
                   ListPopupMenuMove.PopupControl Is Nothing AndAlso
                   _isMenuPopuping = False
        End Function

        ' 列表
        LoadFile()
        For i = 0 To GlobalModel.Tabs.Count - 1
            AddTabToShow(GlobalModel.Tabs.Item(i))
        Next
    End Sub

#End Region

#Region "增删改 移动 复制粘贴 全选"

    Private Sub InsertTip(sender As Object, e As EventArgs) Handles ButtonAddItem.Click, ListPopupMenuAddItem.Click
        If _listPresenter.Insert() Then
            ListView.Update()
            ListView.SetSelectOnly(ListView.ItemCount - 1, True)
        End If
    End Sub

    Private Sub DeleteTip(sender As Object, e As EventArgs) Handles ButtonRemoveItem.Click, ListPopupMenuRemoveItem.Click
        Dim index = ListView.SelectedIndex
        If ListView.SelectedItems IsNot Nothing AndAlso _listPresenter.Delete(ListView.SelectedItems) Then
            ListView.Update()
            ListView.SetSelectOnly(index)
        End If
    End Sub

    Private Sub UpdateTip(sender As Object, e As EventArgs) Handles ListView.DoubleClick, ListPopupMenuEditItem.Click
        Dim index = ListView.SelectedIndex
        If ListView.SelectedCount = 1 AndAlso _listPresenter.Update(ListView.SelectedItem) Then
            ListView.Update()
            ListView.SetSelectOnly(index)
        End If
    End Sub

    Private Sub MoveTipTop(sender As Object, e As EventArgs) Handles ListPopupMenuMoveTop.Click
        If _listPresenter.MoveTop(ListView.SelectedItem) Then
            ListView.Update()
            ListView.SetSelectOnly(0)
        End If
    End Sub

    Private Sub MoveTipBottom(sender As Object, e As EventArgs) Handles ListPopupMenuMoveBottom.Click
        If _listPresenter.MoveBottom(ListView.SelectedItem) Then
            ListView.Update()
            ListView.SetSelectOnly(ListView.ItemCount - 1)
        End If
    End Sub

    Private Sub MoveTipUp(sender As Object, e As EventArgs) Handles ListPopupMenuMoveUp.Click, ButtonItemUp.Click
        If ListView.SelectedIndex >= 1 AndAlso _listPresenter.MoveUp(ListView.SelectedItem) Then
            ListView.Update()
            ListView.SetSelectOnly(ListView.SelectedIndex - 1)
            If sender.Tag = "True" Then
                NativeMethod.MouseMoveUp(Cursor.Position, 17)
            End If
        End If
    End Sub

    Private Sub MoveDownTip(sender As Object, e As EventArgs) Handles ListPopupMenuMoveDown.Click, ButtonItemDown.Click
        If ListView.SelectedIndex <= ListView.ItemCount - 2 AndAlso _listPresenter.MoveDown(ListView.SelectedItem) Then
            ListView.Update()
            ListView.SetSelectOnly(ListView.SelectedIndex + 1)
            If sender.Tag = "True" Then
                NativeMethod.MouseMoveDown(Cursor.Position, 17)
            End If
        End If
    End Sub

    Private Sub CopyTip(sender As Object, e As EventArgs) Handles ListPopupMenuCopy.Click
        _listPresenter.Copy(ListView.SelectedItems)
    End Sub

    Private Sub PasteAppendToTip(sender As Object, e As EventArgs) Handles ListPopupMenuPasteAppend.Click
        If ListView.SelectedCount = 1 AndAlso _listPresenter.Paste(ListView.SelectedItem) Then
            ListView.Update()
        End If
    End Sub

    Private Sub SelectAllTips(sender As Object, e As EventArgs) Handles ListPopupMenuSelectAll.Click
        For i = 0 To ListView.ItemCount - 1
            ListView.SetSelected(i, True)
        Next
    End Sub

#End Region

#Region "查找 打开浏览文件 浏览器"

    Private Sub FindTips(sender As Object, e As EventArgs) Handles ListPopupMenuFind.Click
        _listPresenter.Search()
    End Sub

    Private Sub OpenFileDir(sender As Object, e As EventArgs) Handles ListPopupMenuOpenDir.Click
        _globalPresenter.OpenFileDir()
    End Sub

    Private Sub ViewCurrentTipList(sender As Object, e As EventArgs) Handles ListPopupMenuViewFile.Click
        _listPresenter.ViewCurrentList(ListView.Items)
    End Sub

    Private Sub OpenTipAllLinks(sender As Object, e As EventArgs) Handles ListPopupMenuOpenAllLink.Click
        _listPresenter.OpenAllLinks(ListView.SelectedItems)
    End Sub

    Private Sub ViewTipAllLinks(sender As Object, e As EventArgs) Handles ListPopupMenuViewAllLink.Click
        _listPresenter.ViewAllLinks(ListView.SelectedItems)
    End Sub

#End Region

#Region "分组显示 透明度 辅助按钮"

    Private Sub AddTabToShow(tab As Tab) ' MainForm_Load On_BtnNewTab_Click 用
        Dim newTabItem = New DD.SuperTabItem() With {
            .Name = "TabItemCustom_" & GlobalModel.Tabs.Count,
            .Text = tab.Title, .Tag = tab
            }
        ' AddHandler newTabItem.MouseDown, AddressOf TabStrip_MouseDown
        Me.TabStrip.Tabs.Add(newTabItem)
    End Sub

    Private ReadOnly _opacities() As Double = {0.2, 0.4, 0.6, 0.8, 1}
    Private ReadOnly _opacityButtons(_opacities.Length - 1) As DD.ButtonItem
    Private Const _eps As Double = 1e-2

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

            ListPopupMenuOpacity.SubItems.Add(_opacityButtons(i))
            If Math.Abs(Me.MaxOpacity - _opacityButtons(i).Tag) < _eps Then
                _opacityButtons(i).Checked = True
            End If
        Next
    End Sub

    Private Sub SetupAssistButtons() ' On_Form_Load 用
        Dim itemHeight As Integer = ListView.ItemHeight

        ButtonItemUp.Visible = False
        ButtonItemUp.Height = (itemHeight + 1) / 2
        ButtonItemUp.Width = itemHeight
        ButtonItemDown.Visible = False
        ButtonItemDown.Height = (itemHeight + 1) / 2
        ButtonItemDown.Width = itemHeight

        ListView.OnWheeledAction = Sub() HideAssistButtons()
    End Sub

    Private Sub ShowAssistButtons() ' On_ListViewAndForm_SizeChanged On_ListView_SelectedIndexChangedAndMouseDown 用
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

    Private Sub HideAssistButtons() ' SetupAssistButtons (OnWheeledAction) On_ListView_SelectedIndexChangedAndMouseDown 用
        ButtonItemUp.Visible = False
        ButtonItemDown.Visible = False
    End Sub

    Private Sub On_ListViewAndForm_SizeChanged(sender As Object, e As EventArgs) Handles ListView.SizeChanged, Me.SizeChanged
        If ButtonItemUp.Visible = True AndAlso ListView.SelectedCount = 1 Then
            ShowAssistButtons()
        End If
    End Sub

#End Region

#Region "可用性判断 列表选择 大小调整 菜单与透明度"

    Private Sub CheckListItemEnabled() ' On_ListView_SelectedIndexChangedAndMouseDown On_BtnOpenPopupMenu_Click 用
        Dim isNotNull As Boolean = ListView.SelectedIndex <> - 1
        Dim isSingle As Boolean = ListView.SelectedCount = 1
        Dim isTop As Boolean = ListView.SelectedIndex = 0
        Dim isBottom As Boolean = ListView.SelectedIndex = ListView.ItemCount - 1

        ' 辅助按钮 位置按钮
        ButtonItemUp.Enabled = isNotNull And isSingle And Not isTop
        ButtonItemDown.Enabled = isNotNull And isSingle And Not isBottom
        ListPopupMenuMoveUp.Enabled = isNotNull And isSingle And Not isTop
        ListPopupMenuMoveTop.Enabled = isNotNull And isSingle And Not isTop
        ListPopupMenuMoveBottom.Enabled = isNotNull And isSingle And Not isBottom
        ListPopupMenuMoveDown.Enabled = isNotNull And isSingle And Not isBottom

        ' 删改复制粘贴
        ButtonRemoveItem.Enabled = isNotNull
        ListPopupMenuRemoveItem.Enabled = isNotNull
        ListPopupMenuEditItem.Enabled = isSingle
        ListPopupMenuHighLight.Enabled = isNotNull
        ListPopupMenuHighLight.Checked = isNotNull AndAlso ListView.SelectedItem.IsHighLight
        ListPopupMenuCopy.Enabled = isNotNull
        ListPopupMenuPasteAppend.Enabled = isSingle

        ' 浏览器
        ListPopupMenuBrowser.Enabled = _listPresenter.GetLinks(ListView.SelectedItems).Count >= 1

        ' 移动
        ListPopupMenuMove.Enabled = isNotNull
    End Sub

    Private Sub On_ListView_SelectedIndexChangedAndMouseDown(sender As Object, e As EventArgs) Handles ListView.SelectedIndexChanged, ListView.MouseDown
        CheckListItemEnabled()
        If ListView.SelectedCount = 1 Then ShowAssistButtons() Else HideAssistButtons()
        ListView.Refresh()
    End Sub

    Private Sub On_ListViewAndTabStrip_MouseDown(sender As Object, e As MouseEventArgs) Handles ListView.MouseDown, TabStrip.MouseDown
        If ListView.PointOutOfRange(e.Location) Then
            ListView.ClearSelected()
        End If
    End Sub

    Private Sub On_BtnResize_MouseMove(sender As Object, e As MouseEventArgs) Handles ButtonResizeFlag.MouseMove
        If e.Button = MouseButtons.Left Then
            Me.Width = PushDownWindowSize.Width + Cursor.Position.X - PushDownMouseInScreen.X
        End If
    End Sub

    Private _isMenuPopuping As Boolean = False

    Private Sub On_TabStrip_PopupOpen(sender As Object, e As EventArgs) Handles TabStrip.PopupOpen
        _isMenuPopuping = True
    End Sub

    Private Sub On_SomePopup_FinishedAndClose(sender As Object, e As EventArgs) _
        Handles ListPopupMenu.PopupFinalized, TabPopupMenu.PopupFinalized, ListPopupMenuMove.PopupFinalized, TabStrip.PopupClose
        _isMenuPopuping = False
        FormOpacityDown()
    End Sub

#End Region

#Region "其他: 退出 弹出菜单 列表数量 热键置顶 加载保存位置"

    Private Sub On_BtnExit_Click(sender As Object, e As EventArgs) Handles ButtonCloseForm.Click, ListPopupMenuExit.Click
        Dim ok = MessageBox.Show("确定退出 DesktopTips 吗？",
            "关闭", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
        If ok = vbYes Then
            Me.Close()
        End If
    End Sub

    Private Sub On_BtnOpenPopupMenu_Click(sender As Object, e As EventArgs) Handles ButtonListSetting.Click
        CheckListItemEnabled()
        ListPopupMenu.Popup(Me.Left + sender.Left, Me.Top + sender.Top + sender.Height - 1)
    End Sub

    Private Sub On_ListPopupMenu_PopupOpen(sender As Object, e As DD.PopupOpenEventArgs) Handles ListPopupMenu.PopupOpen
        e.Cancel = True
        ListPopupMenuLabelSelItem.Visible = ListView.SelectedCount > 0
        ListPopupMenuLabelSelItemText.Visible = ListView.SelectedCount > 0

        Dim sb As New StringBuilder
        For Each item As TipItem In ListView.SelectedItems
            sb.AppendLine(item.Content)
        Next
        Dim highlightCount = 0
        For Each tip As TipItem In GlobalModel.CurrentTab.Tips
            If tip.IsHighLight Then highlightCount += 1
        Next

        ListPopupMenuLabelSelItemText.Text = sb.ToString()
        ListPopupMenuLabelItemList.Text = $"列表 (共 {ListView.ItemCount} 项，高亮 {highlightCount} 项)"
        ListPopupMenuLabelSelItem.Text = $"当前选中 (共 {ListView.SelectedCount} 项)"
        e.Cancel = False
        ListPopupMenu.Refresh()
    End Sub

    Private Sub On_NumericListCount_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDownListCnt.ValueChanged
        Dim y As Integer
        If Me.Height < NumericUpDownListCnt.Value * ListView.ItemHeight + 27 Then
            y = 17
        ElseIf Me.Height > NumericUpDownListCnt.Value * ListView.ItemHeight + 27 Then
            y = - 17
        End If
        Me.Height = NumericUpDownListCnt.Value * ListView.ItemHeight + 27

        Dim dx As Integer = Cursor.Position.X * UInt16.MaxValue / My.Computer.Screen.Bounds.Width
        Dim dy As Integer = (Cursor.Position.Y + y) * UInt16.MaxValue / My.Computer.Screen.Bounds.Height
        Const flag = NativeMethod.MouseEvent.MOUSEEVENTF_MOVE Or NativeMethod.MouseEvent.MOUSEEVENTF_ABSOLUTE
        NativeMethod.mouse_event(flag, dx, dy, 0, 0)
    End Sub

    Private Sub On_BtnShowNumeric_Click(sender As Object, e As EventArgs) Handles ListPopupMenuListHeight.Click
        ListPopupMenuListHeight.Checked = Not ListPopupMenuListHeight.Checked
        NumericUpDownListCnt.Visible = ListPopupMenuListHeight.Checked
    End Sub

    Private Sub On_BtnSettingHotkey_Click(sender As Object, e As EventArgs) Handles ListPopupMenuHotkeySetting.Click, ListPopupMenuShotcutSetting.Click
        _globalPresenter.SetupHotKey(Handle, HOTKEY_ID)
    End Sub

    Private Sub On_BtnTopMost_Click(sender As Object, e As EventArgs) Handles ListPopupMenuWinTop.Click
        ListPopupMenuWinTop.Checked = Not ListPopupMenuWinTop.Checked
        Me.TopMost = ListPopupMenuWinTop.Checked
        My.Settings.TopMost = Me.TopMost
        My.Settings.Save()
    End Sub

    Private Sub On_BtnLoadPos_Click(sender As Object, e As EventArgs) Handles ListPopupMenuLoadPos.Click
        Me.Top = My.Settings.SaveTop
        Me.Left = My.Settings.SaveLeft
    End Sub

    Private Sub On_BtnSave_Click(sender As Object, e As EventArgs) Handles ListPopupMenuSavePos.Click
        Dim ok = MessageBox.Show("确定保存当前位置，注意该操作会覆盖之前保存的窗口位置。",
            "保存位置", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
        If ok = vbOK Then
            ListPopupMenuLoadPos.Enabled = True
            My.Settings.SaveTop = Me.Top
            My.Settings.SaveLeft = Me.Left
            My.Settings.Save()
        End If
    End Sub

#End Region
End Class
