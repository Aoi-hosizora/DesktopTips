Imports System.IO
Imports System.Text
Imports DD = DevComponents.DotNetBar

Public Class MainForm

#Region "加载设置 加载列表内容和界面 启动关闭窗口 系统响应"

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

        ListPopupMenuFold.Checked = setting.IsFold                                          ' 折叠菜单
        ListPopupMenuWinHighColor.SelectedColor = setting.HighLightColor                    ' 高亮颜色
        ListPopupMenuLoadPos.Enabled = Not (setting.SaveLeft = -1 Or setting.SaveTop = -1)  ' 恢复位置
        NumericUpDownListCnt.Value = (Me.Height - 27) \ 17                                  ' 列表高度
        ListPopupMenuWinTop.Checked = setting.TopMost                                       ' 窗口置顶

        _globalPresenter.RegisterShotcut(Handle, setting.HotKey, HOTKEY_ID)                 ' 注册快捷键
        FoldMenu(setting.IsFold)                                                            ' 折叠菜单
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
        For Each tip As TipItem In GlobalModel.CurrentTab.Tips
            ListView.Items.Add(tip)
        Next
        LabelNothing.Visible = ListView.Items.Count = 0
    End Sub

    ''' <summary>
    ''' 保存到文件
    ''' MainForm_FormClosed 增删改 用
    ''' </summary>
    Private Sub SaveList()
        _globalPresenter.SaveList(ListView.Items)
        ListView.Refresh()
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
                FormOpecityUp()
            End If
        End If
        MyBase.WndProc(m)
    End Sub

    ''' <summary>
    ''' 窗口关闭之后处理
    ''' </summary>
    Private Sub MainForm_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        SaveList()
        SaveSetting()
        _globalPresenter.UnregisterShotcut(Handle, HOTKEY_ID)
    End Sub

    ''' <summary>
    ''' 窗口加载
    ''' </summary>
    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' 加载设置
        LoadSetting()
        Me.Refresh()
        FormOpecityUp()

        ' 窗口显示
        Me.TabStrip.Tabs.Remove(Me.TabItemTest)
        Me.TabStrip.Tabs.Remove(Me.TabItemTest2)
        ButtonRemoveItem.Enabled = False
        SetupAssistButtonsLayout()
        SetupOpecityButtonsLayout()

        ' 窗口动画
        CanMouseLeave = Function() As Boolean
                            Return ListPopupMenu.PopupControl Is Nothing AndAlso
                                TabPopupMenu.PopupControl Is Nothing AndAlso
                                TabStrip.ContextMenu Is Nothing AndAlso
                                ListPopupMenuMove.PopupControl Is Nothing AndAlso
                                isMenuPopuping = False
                        End Function

        ' 列表
        LoadList()
        For i = 0 To GlobalModel.Tabs.Count - 1
            AddShownTab(GlobalModel.Tabs.Item(i).Title)
        Next
    End Sub

#End Region

#Region "列表内容"

#Region "增删改 移动 置顶 复制粘贴 全选 查找"

    ''' <summary>
    ''' 增
    ''' </summary>
    Private Sub ButtonAddItem_Click(sender As Object, e As EventArgs) Handles ButtonAddItem.Click, ListPopupMenuAddItem.Click, LabelNothing.DoubleClick
        Dim item As TipItem = _listPresenter.Insert()
        If item IsNot Nothing Then
            ListView.Items.Add(item)
            ListView.ClearSelected()
            ListView.SetSelected(ListView.Items.Count() - 1, True)
            SaveList()
        End If
    End Sub

    ''' <summary>
    ''' 删
    ''' </summary>
    Private Sub ButtonRemoveItem_Click(sender As Object, e As EventArgs) Handles ButtonRemoveItem.Click, ListPopupMenuRemoveItem.Click
        If ListView.SelectedItem IsNot Nothing Then
            Dim selected As New List(Of TipItem)(ListView.SelectedItems.Cast(Of TipItem)())
            If _listPresenter.Delete(selected) Then
                For Each item As TipItem In selected
                    ListView.Items.Remove(item)
                Next
                SaveList()
            End If
        End If
    End Sub

    ''' <summary>
    ''' 改
    ''' </summary>
    Private Sub ListView_DoubleClick(sender As Object, e As EventArgs) Handles ListView.DoubleClick, ListPopupMenuEditItem.Click
        If ListView.SelectedItem IsNot Nothing AndAlso ListView.SelectedIndices.Count = 1 Then
            Dim thisIdx As Integer = ListView.SelectedIndex
            Dim tip As TipItem = CType(ListView.SelectedItem, TipItem)
            If _listPresenter.Update(tip) Then
                ListView.Items(thisIdx) = tip
                ListView.SelectedIndex = thisIdx
                SaveList()
            End If
        End If
    End Sub

    ''' <summary>
    ''' 置顶
    ''' </summary>
    Private Sub ListPopupMenuMoveTop_Click(sender As Object, e As EventArgs) Handles ListPopupMenuMoveTop.Click
        Dim item As TipItem = CType(ListView.SelectedItem, TipItem)
        ListView.Items.Remove(item)
        ListView.Items.Insert(0, item)
        ListView.SetSelected(0, True)
        SaveList()
    End Sub

    ''' <summary>
    ''' 置底
    ''' </summary>
    Private Sub ListPopupMenuMoveBottom_Click(sender As Object, e As EventArgs) Handles ListPopupMenuMoveBottom.Click
        Dim item As TipItem = CType(ListView.SelectedItem, TipItem)
        ListView.Items.Remove(item)
        ListView.Items.Insert(ListView.Items.Count, item)
        ListView.SetSelected(ListView.Items.Count - 1, True)
        SaveList()
    End Sub

    ''' <summary>
    ''' 上移
    ''' </summary>
    Private Sub MoveUp_Click(sender As Object, e As EventArgs) Handles ListPopupMenuMoveUp.Click, ButtonItemUp.Click
        Dim currIdx As Integer = ListView.SelectedIndex
        If currIdx >= 1 Then
            Dim item As TipItem = CType(ListView.SelectedItem, TipItem)
            ListView.Items.Remove(item)
            ListView.Items.Insert(currIdx - 1, item)
            ListView.SetSelected(currIdx - 1, True)
            If sender.Tag = "True" Then NativeMethod.MouseMoveUp(Cursor.Position, 17) ' 如果是辅助按钮
            SaveList()
        End If
    End Sub

    ''' <summary>
    ''' 下移
    ''' </summary>
    Private Sub MoveDown_Click(sender As Object, e As EventArgs) Handles ListPopupMenuMoveDown.Click, ButtonItemDown.Click
        Dim currIdx As Integer = ListView.SelectedIndex
        If currIdx <= ListView.Items.Count() - 2 Then
            Dim item As TipItem = CType(ListView.SelectedItem, TipItem)
            ListView.Items.Remove(item)
            ListView.Items.Insert(currIdx + 1, item)
            ListView.SetSelected(currIdx + 1, True)
            If sender.Tag = "True" Then NativeMethod.MouseMoveDown(Cursor.Position, 17) ' 如果是辅助按钮
            SaveList()
        End If
    End Sub

    ''' <summary>
    ''' 复制
    ''' </summary>
    Private Sub ListPopupMenuCopy_Click(sender As Object, e As EventArgs) Handles ListPopupMenuCopy.Click
        _listPresenter.Copy(ListView.SelectedItems.Cast(Of TipItem)())
    End Sub

    ''' <summary>
    ''' 粘贴附加在最后
    ''' </summary>
    Private Sub ListPopupMenuPasteAppend_Click(sender As Object, e As EventArgs) Handles ListPopupMenuPasteAppend.Click
        If ListView.SelectedItem IsNot Nothing AndAlso ListView.SelectedIndices.Count = 1 Then
            Dim thisIdx As Integer = ListView.SelectedIndex
            Dim item As TipItem = CType(ListView.SelectedItem, TipItem)
            If _listPresenter.Paste(item) Then
                ListView.Items(thisIdx) = item
                ListView.SelectedIndex = thisIdx
                SaveList()
            End If
        End If
    End Sub

    ''' <summary>
    ''' 全选
    ''' </summary>
    Private Sub ListPopupMenuSelectAll_Click(sender As Object, e As EventArgs) Handles ListPopupMenuSelectAll.Click
        For i = 0 To ListView.Items.Count - 1
            ListView.SetSelected(i, True)
        Next
    End Sub

    ''' <summary>
    ''' 查找文字
    ''' </summary>
    Public Sub ListPopupMenuFind_Click(sender As Object, e As EventArgs) Handles ListPopupMenuFind.Click
        SaveList()
        _listPresenter.Search()
    End Sub

#End Region

#Region "打开浏览文件 浏览器 高亮 列表绘制"

    ''' <summary>
    ''' 打开文件所在位置
    ''' </summary>
    Private Sub ListPopupMenuOpenFile_Click(sender As Object, e As EventArgs) Handles ListPopupMenuOpenDir.Click
        _globalPresenter.OpenFileDir()
    End Sub

    ''' <summary>
    ''' 浏览当前列表
    ''' </summary>
    Private Sub ListPopupMenuViewFile_Click(sender As Object, e As EventArgs) Handles ListPopupMenuViewFile.Click
        _listPresenter.ViewCurrentList(ListView.Items.Cast(Of TipItem)())
    End Sub

    ''' <summary>
    ''' 浏览高亮部分
    ''' </summary>
    Private Sub ListPopupMenuHighLightList_Click(sender As Object, e As EventArgs) Handles ListPopupMenuViewHighLight.Click
        _listPresenter.ViewHighlightList(ListView.Items.Cast(Of TipItem)(), ListPopupMenuWinHighColor.SelectedColor)
    End Sub

    ''' <summary>
    ''' 打开所有链接
    ''' </summary>
    Private Sub ListPopupMenuOpenAllLink_Click(sender As Object, e As EventArgs) Handles ListPopupMenuOpenAllLink.Click
        _listPresenter.OpenAllLinks(ListView.SelectedItems.Cast(Of TipItem)())
    End Sub

    ''' <summary>
    ''' 打开部分连接
    ''' </summary>
    Private Sub ListPopupMenuViewAllLink_Click(sender As Object, e As EventArgs) Handles ListPopupMenuViewAllLink.Click
        _listPresenter.OpenSomeLinks(ListView.SelectedItems.Cast(Of TipItem)())
    End Sub

    ''' <summary>
    ''' 高亮
    ''' </summary>
    Private Sub ListPopupMenuHighLight_Click(sender As Object, e As EventArgs) Handles ListPopupMenuHighLight.Click
        Dim highLight As Boolean = CType(ListView.SelectedItems(0), TipItem).IsHighLight
        For Each item As TipItem In ListView.SelectedItems
            item.IsHighLight = Not highLight
        Next
        SaveList()
        ListView.Refresh()
    End Sub

    ''' <summary>
    ''' 高亮颜色修改
    ''' </summary>
    Private Sub ListPopupMenuWinHighColor_SelectedColorChanged(sender As Object, e As EventArgs) Handles ListPopupMenuWinHighColor.SelectedColorChanged
        ListView.Refresh()
    End Sub

    ''' <summary>
    ''' 重写绘制 高亮
    ''' </summary>
    Private Sub ListView_DrawItem(sender As Object, e As DrawItemEventArgs) Handles ListView.DrawItem
        If e.Index <> -1 Then
            e.DrawBackground()
            e.DrawFocusRectangle()
            Dim currItem = ListView.Items(e.Index)
            Dim brush As New SolidBrush(If(CType(currItem, TipItem).IsHighLight, ListPopupMenuWinHighColor.SelectedColor, e.ForeColor))
            e.Graphics.DrawString(currItem.ToString, e.Font, brush, e.Bounds, StringFormat.GenericDefault)
        End If
    End Sub

#End Region

#End Region

#Region "界面显示"

#Region "透明度 辅助按钮"

    Dim _opecities() As Double = {0.2, 0.4, 0.6, 0.7, 0.8, 1.0}
    Dim _opecityButtons(_opecities.Length - 1) As DD.ButtonItem

    ''' <summary>
    ''' 动态添加透明度
    ''' MainForm_Load 用
    ''' </summary>
    Private Sub SetupOpecityButtonsLayout()
        For i = 0 To _opecities.Length - 1
            _opecityButtons(i) = New DD.ButtonItem With {
                .Name = "ListPopupMenuOpacity" & CInt(_opecities(i) * 100),
                .Tag = _opecities(i),
                .Text = CInt(_opecities(i) * 100) & "%"
            }

            AddHandler _opecityButtons(i).Click, AddressOf ListPopupMenuOpacity_Click
            Me.ListPopupMenuOpacity.SubItems.Add(_opecityButtons(i))

            If Me.MaxOpacity = _opecityButtons(i).Tag Then
                _opecityButtons(i).Checked = True
            End If
        Next
    End Sub

    ''' <summary>
    ''' 透明度点击
    ''' </summary>
    Private Sub ListPopupMenuOpacity_Click(sender As Object, e As EventArgs)
        Dim btn As DD.ButtonItem = CType(sender, DD.ButtonItem)
        Me.MaxOpacity = CDbl(btn.Tag())
        For Each opBtn In _opecityButtons
            opBtn.Checked = False
        Next
        btn.Checked = True
        FormOpecityDown()
    End Sub

    ''' <summary>
    ''' 设置辅助按钮布局
    ''' MainForm_Load 用
    ''' </summary>
    Private Sub SetupAssistButtonsLayout()
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
    Private Sub ListView_SizeChanged(sender As Object, e As EventArgs) Handles ListView.SizeChanged
        If ButtonItemUp.Visible = True AndAlso ListView.SelectedIndices.Count = 1 Then
            ShowAssistButtons()
        End If
    End Sub

#End Region

#Region "窗口焦点 弹出菜单标记 大小调整"

    ''' <summary>
    ''' 窗口失去焦点，取消选择
    ''' </summary>
    Private Sub MainForm_Deactivate(sender As Object, e As EventArgs) Handles Me.Deactivate
        ListView.ClearSelected()
        If Me.Opacity <> MaxOpacity Then
            FormOpecityDown()
        End If
    End Sub

    ''' <summary>
    ''' 没有选择列表项，取消选择
    ''' </summary>
    Private Sub ListView_MouseDown_Sel(sender As Object, e As MouseEventArgs) Handles ListView.MouseDown, TabStrip.MouseDown
        ListView_SelectedIndexChanged(sender, New System.EventArgs)

        If ListView.Items.Count > 0 Then
            Dim rect As Rectangle = ListView.GetItemRectangle(ListView.Items.Count - 1)
            If e.Y > rect.Top + rect.Height Then ListView.ClearSelected()
        End If
    End Sub

    ''' <summary>
    ''' 判断透明度是否可以变化
    ''' </summary>
    Private isMenuPopuping As Boolean = False

    ''' <summary>
    ''' TabStrip 菜单弹出
    ''' </summary>
    Private Sub TabStrip_PopupOpen(sender As Object, e As EventArgs) Handles TabStrip.PopupOpen
        isMenuPopuping = True
    End Sub

    ''' <summary>
    ''' 菜单关闭
    ''' </summary>
    Private Sub PopMenu_PopupFinalizedAndClosed(sender As Object, e As EventArgs) Handles ListPopupMenu.PopupFinalized, TabPopupMenu.PopupFinalized, ListPopupMenuMove.PopupFinalized, TabStrip.PopupClose
        isMenuPopuping = False
        FormOpecityDown()
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
        Dim isNotNull As Boolean = ListView.SelectedIndex <> -1
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
        ListPopupMenuHighLight.Checked = If(isNotNull, CType(ListView.SelectedItem, TipItem).IsHighLight, False)
        ListPopupMenuCopy.Enabled = isNotNull

        ' 移动
        ListPopupMenuMove.Enabled = isNotNull

        ' 附加
        ListPopupMenuPasteAppend.Enabled = isSingle

        ' 浏览器
        ListPopupMenuBrowser.Enabled = ListView.SelectedItems.Cast(Of TipItem)().
            Where(Function(item As TipItem) item.TipContent.IndexOf("http://") <> -1 Or item.TipContent.IndexOf("https://") <> -1).Count >= 1
    End Sub

    ''' <summary>
    ''' 列表选择, 辅助按钮显示 可用判断
    ''' </summary>
    Private Sub ListView_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView.SelectedIndexChanged
        CheckItemEnabled()
        If ListView.SelectedIndices.Count = 1 Then
            ShowAssistButtons()
        Else
            HideAssistButtons()
        End If
        ListView.Refresh()
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
        Static hoverIdx As Integer = -1
        Dim idx = ListView.IndexFromPoint(e.Location)
        If hoverIdx <> idx AndAlso idx <> -1 AndAlso idx < ListView.Items.Count Then
            HoverToolTip.Hide(ListView)
            HoverToolTip.SetToolTip(ListView, CType(ListView.Items.Item(idx), TipItem).TipContent)
            hoverIdx = idx
        End If
    End Sub

#End Region

#Region "弹出菜单 列表高度 快捷键"

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

    ''' <summary>
    ''' 弹出菜单 调整标题
    ''' </summary>
    Private Sub ListPopMenu_PopupOpen(sender As Object, e As DD.PopupOpenEventArgs) Handles ListPopupMenu.PopupOpen
        e.Cancel = True
        ListPopupMenuLabelSelItem.Visible = ListView.SelectedIndex <> -1
        ListPopupMenuLabelSelItemText.Visible = ListView.SelectedIndex <> -1

        Dim cnt As Integer = ListView.SelectedIndices.Count
        Dim hlCnt As Integer = 0
        For Each Tip As TipItem In GlobalModel.CurrentTab.Tips
            If Tip.IsHighLight Then hlCnt += 1
        Next

        Dim sb As New StringBuilder
        For Each item As TipItem In ListView.SelectedItems.Cast(Of TipItem)()
            sb.AppendLine(item.TipContent)
        Next

        ListPopupMenuLabelSelItemText.Text = sb.ToString
        ListPopupMenuLabelItemList.Text = "列表 (共 " & cnt & " 项，高亮 " & hlCnt & " 项)"
        ListPopupMenuLabelSelItem.Text = "当前选中 (共 " & cnt & " 项)"
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
    Private Sub ListPopupMenuShotcutSetting_Click(sender As Object, e As EventArgs) Handles ListPopupMenuShotcutSetting.Click
        _globalPresenter.SetupHotKey(Handle, HOTKEY_ID)
    End Sub

#End Region

#Region "退出程序 置顶 加载位置 保存位置"

    ''' <summary>
    ''' 退出程序
    ''' </summary>
    Private Sub ButtonExit_Click(sender As Object, e As EventArgs) Handles ButtonCloseForm.Click, ListPopupMenuExit.Click
        Dim ok = MessageBox.Show("确定退出 DesktopTips 吗？",
                                 "关闭", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
        If ok = vbYes Then
            Me.Close()
        End If
    End Sub

    ''' <summary>
    ''' 置顶
    ''' </summary>
    Private Sub ListPopupMenuWinTop_Click(sender As Object, e As EventArgs) Handles ListPopupMenuWinTop.Click
        ListPopupMenuWinTop.Checked = Not ListPopupMenuWinTop.Checked
        Me.TopMost = ListPopupMenuWinTop.Checked
    End Sub

    ''' <summary>
    ''' 加载位置
    ''' </summary>
    Private Sub ListPopupMenuLoadPos_Click(sender As Object, e As EventArgs) Handles ListPopupMenuLoadPos.Click
        Dim setting As SettingUtil.AppSetting = _globalPresenter.LoadSetting()
        Me.Top = setting.SaveTop
        Me.Left = setting.SaveLeft
    End Sub

    ''' <summary>
    ''' 保存位置
    ''' </summary>
    Private Sub ListPopupMenuSavePos_Click(sender As Object, e As EventArgs) Handles ListPopupMenuSavePos.Click
        Dim ok As DialogResult = MessageBox.Show(
            "确定保存当前位置，注意该操作会覆盖之前保存的窗口位置。",
            "保存位置", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
        If ok = vbOK Then
            Dim setting As SettingUtil.AppSetting = _globalPresenter.LoadSetting()
            setting.SaveTop = Me.Top
            setting.SaveLeft = Me.Left
            _globalPresenter.SaveSetting(setting)
            ListPopupMenuLoadPos.Enabled = True
        End If
    End Sub

#End Region

#End Region

#Region "分组"

#Region "分组 选择 双击"

    ''' <summary>
    ''' 右键点击 Tab 选中
    ''' </summary>
    Private Sub TabStrip_MouseDown(sender As Object, e As MouseEventArgs) Handles TabStrip.MouseDown
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
        HideAssistButtons()
        If TabStrip.SelectedTabIndex <> -1 And GlobalModel.Tabs.Count <> 0 Then
            GlobalModel.CurrTabIdx = GlobalModel.Tabs.IndexOf(Tab.GetTabFromTitle(TabStrip.SelectedTab.Text))
            LoadList()
        End If
    End Sub

    ''' <summary>
    ''' 双击Tab，新建或重命名
    ''' </summary>
    Private Sub TabStrip_DoubleClick(sender As Object, e As EventArgs) Handles TabStrip.DoubleClick
        Dim pos As New Point(Cursor.Position.X - Me.Left - sender.Left, Cursor.Position.Y - Me.Top - sender.Top)
        If TabStrip.GetItemFromPoint(pos) Is Nothing Then
            TabPopupMenuNewTab_Click(sender, New EventArgs)
        Else
            TabPopupMenuRenameTab_Click(sender, New EventArgs)
        End If
    End Sub

    ''' <summary>
    ''' 边栏点击，取消选择
    ''' </summary>
    Private Sub TabStrip_Click(sender As Object, e As EventArgs) Handles TabStrip.Click
        ListView.ClearSelected()
    End Sub

#End Region

#Region "分组 增删改 位置移动"

    ''' <summary>
    ''' 新建分组
    ''' </summary>
    Private Sub TabPopupMenuNewTab_Click(sender As Object, e As EventArgs) Handles TabPopupMenuNewTab.Click
        Dim tab As Tab = _groupPresenter.Insert()
        If tab IsNot Nothing Then
            AddShownTab(tab.Title)
        End If
    End Sub

    ''' <summary>
    ''' 删除分组
    ''' </summary>
    Private Sub TabPopupMenuDeleteTab_Click(sender As Object, e As EventArgs) Handles TabPopupMenuDeleteTab.Click
        If TabStrip.Tabs.Count = 1 Then
            MessageBoxEx.Show("无法删除最后的分组。", "删除", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, Me)
        Else
            If _groupPresenter.Delete(TabStrip.SelectedTab.Text) Then
                TabStrip.Tabs.RemoveAt(TabStrip.SelectedTabIndex)
            End If
        End If
    End Sub

    ''' <summary>
    ''' 重命名分组
    ''' </summary>
    Private Sub TabPopupMenuRenameTab_Click(sender As Object, e As EventArgs) Handles TabPopupMenuRenameTab.Click
        If TabStrip.SelectedTabIndex <> -1 Then
            Dim newName As String = _groupPresenter.Update(TabStrip.SelectedTab.Text)
            If Not String.IsNullOrWhiteSpace(newName) Then
                TabStrip.SelectedTab.Text = newName
            End If
        End If
    End Sub

    ''' <summary>
    ''' Tab 顺序更改
    ''' </summary>
    Private Sub TabStrip_TabMoved(sender As Object, e As DD.SuperTabStripTabMovedEventArgs) Handles TabStrip.TabMoved
        Dim newTabs As New List(Of Tab)
        For Each TabItem As DD.SuperTabItem In e.NewOrder
            newTabs.Add(Tab.GetTabFromTitle(TabItem.Text))
        Next
        GlobalModel.Tabs = newTabs
        GlobalModel.CurrTabIdx = GlobalModel.Tabs.IndexOf(Tab.GetTabFromTitle(TabStrip.SelectedTab.Text))
        GlobalModel.SaveAllData()
    End Sub

#End Region

#Region "分组内 Tip 移动"

    ''' <summary>
    ''' 获得非活动分组集，移动至分组 用
    ''' </summary>
    ''' <param name="prefix">分组类名前缀 ListPopupMenuMove / TabPopupMenuMove</param>
    Private Function GetUnactivityTabMoveButtonList(ByVal prefix As String) As List(Of DD.ButtonItem)
        Dim tabs As New List(Of String)
        Dim btns As New List(Of DD.ButtonItem)

        For Each tab As Tab In GlobalModel.Tabs
            If tab.Title <> GlobalModel.CurrentTab.Title Then
                tabs.Add(tab.Title)
            End If
        Next

        For i = 0 To Tabs.Count - 1
            Dim btn As New DD.ButtonItem With {
                .Name = prefix & tabs.Item(i),
                .Tag = tabs.Item(i),
                .Text = tabs.Item(i) & If(i < 10, "(&" & i & ")", "")
            }
            AddHandler btn.Click, AddressOf MoveTab
            btns.Add(Btn)
        Next

        Return btns
    End Function

    ''' <summary>
    ''' 弹出列表右键菜单
    ''' </summary>
    Private Sub ListPopMenu_PopupOpen_Move(sender As Object, e As DD.PopupOpenEventArgs) Handles ListPopupMenu.PopupOpen
        ListPopupMenuMove.SubItems.Clear()
        For Each btn As DD.ButtonItem In GetUnactivityTabMoveButtonList("ListPopupMenuMove")
            ListPopupMenuMove.SubItems.Add(btn)
        Next
        ListPopupMenuMove.Enabled = Not Me.ListPopupMenuMove.SubItems.Count = 0 And Not ListView.Items.Count = 0
        If Not ListPopupMenuMove.Enabled Then
            ListPopupMenuMove.SubItems.Clear()
        End If
    End Sub

    ''' <summary>
    ''' 弹出分组右键菜单
    ''' </summary>
    Private Sub TabPopupMenu_PopupOpen(sender As Object, e As DD.PopupOpenEventArgs) Handles TabPopupMenu.PopupOpen
        TabPopupMenuLabel.Text = "分组 (共 " & GlobalModel.Tabs.Count & " 组)"

        TabPopupMenuMove.SubItems.Clear()
        For Each Btn As DD.ButtonItem In GetUnactivityTabMoveButtonList("TabPopupMenuMove")
            TabPopupMenuMove.SubItems.Add(Btn)
        Next
        TabPopupMenuMove.Enabled = Not Me.TabPopupMenuMove.SubItems.Count = 0 And Not ListView.Items.Count = 0
        If Not TabPopupMenuMove.Enabled Then
            TabPopupMenuMove.SubItems.Clear()
        End If
    End Sub

    ''' <summary>
    ''' 快捷键弹出 移动至分组
    ''' </summary>
    Private Sub ListPopupMenuMove_Click(sender As Object, e As EventArgs) Handles ListPopupMenuMove.Click
        If ListView.SelectedIndices.Count <> 0 AndAlso Not DD.BaseItem.IsOnPopup(ListPopupMenuMove) Then
            ListPopMenu_PopupOpen_Move(sender, New DD.PopupOpenEventArgs)
            ListPopupMenuMove.Popup(Me.Left + ButtonListSetting.Left, Me.Top + ButtonListSetting.Top + ButtonListSetting.Height - 1)
        End If
    End Sub

    ''' <summary>
    ''' 移动至分组
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub MoveTab(sender As Object, e As EventArgs)
        Dim src As String = GlobalModel.CurrentTab.Title
        Dim dest As String = sender.Tag
        Dim all As Boolean = ListView.SelectedItem Is Nothing
        Dim items As New List(Of TipItem)(If(all, ListView.Items.Cast(Of TipItem)(), ListView.SelectedItems.Cast(Of TipItem)()))
        If _groupPresenter.MoveItems(all, items, src, dest) Then
            For Each item As DD.SuperTabItem In TabStrip.Tabs
                If item.Text = dest Then
                    TabStrip.SelectedTab = item
                    Exit For
                End If
            Next
            For Each item As TipItem In items
                Dim idx As Integer = TipItem.GetIndexFromContent(item.TipContent, Tab.GetTabFromTitle(dest).Tips)
                If idx <> -1 Then
                    ListView.SetSelected(idx, True)
                End If
            Next
        End If
    End Sub

#End Region

#End Region

End Class
