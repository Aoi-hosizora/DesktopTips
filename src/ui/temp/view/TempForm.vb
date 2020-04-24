﻿Imports DD = DevComponents.DotNetBar

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
        Me.MaxOpacity = My.Settings.Opacity
        Me.TopMost = My.Settings.TopMost

        ListPopupMenuFold.Checked = My.Settings.IsFold                                              ' 折叠菜单
        ListPopupMenuLoadPos.Enabled = Not (My.Settings.SaveLeft = -1 Or My.Settings.SaveTop = -1)  ' 恢复位置
        NumericUpDownListCnt.Value = (Me.Height - 27) \ 17                                          ' 列表高度
        ListPopupMenuWinTop.Checked = My.Settings.TopMost                                           ' 窗口置顶键
        _globalPresenter.RegisterShotcut(Handle, My.Settings.HotKey, HOTKEY_ID)                     ' 注册快捷
        FoldMenu(My.Settings.IsFold)                                                                ' 折叠菜单
    End Sub

    ''' <summary>
    ''' 加载文件，显示当前分组
    ''' MainForm_Load TabStrip_SelectedTabChanged 用
    ''' </summary>
    Private Sub LoadList()
        _globalPresenter.LoadList() ' GlobalModel 中
        ListView.DataSource = GlobalModel.CurrentTab.Tips
        ListView.Refresh()
        LabelNothing.Visible = ListView.Items.Count = 0
    End Sub

    ''' <summary>
    ''' 保存到文件
    ''' MainForm_FormClosed 增删改 用
    ''' </summary>
    Private Sub SaveList()
        _globalPresenter.SaveList()
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
        ' AddHandler newTabItem.MouseDown, AddressOf TabStrip_MouseDown
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
        _globalPresenter.UnregisterShotcut(Handle, HOTKEY_ID)
    End Sub

    ''' <summary>
    ''' 窗口加载
    ''' </summary>
    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' 窗口显示
        LoadSetting()
        Me.Refresh()
        Me.TabStrip.Tabs.Remove(Me.TabItemTest)
        Me.TabStrip.Tabs.Remove(Me.TabItemTest2)
        ButtonRemoveItem.Enabled = False
        ' SetupAssistButtonsLayout()
        ' SetupOpecityButtonsLayout()

        ' 窗口动画
        CanMouseLeave = Function() As Boolean
                            Return ListPopupMenu.PopupControl Is Nothing AndAlso
                                TabPopupMenu.PopupControl Is Nothing AndAlso
                                TabStrip.ContextMenu Is Nothing AndAlso
                                ListPopupMenuMove.PopupControl Is Nothing 'AndAlso isMenuPopuping = False
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
    Private Sub InsertTip(sender As Object, e As EventArgs) Handles ButtonAddItem.Click, ListPopupMenuAddItem.Click, LabelNothing.DoubleClick
        _listPresenter.Insert()
        ListView.Refresh()
    End Sub

    ''' <summary>
    ''' 删
    ''' </summary>
    Private Sub DeleteTip(sender As Object, e As EventArgs) Handles ButtonRemoveItem.Click, ListPopupMenuRemoveItem.Click
        If ListView.SelectedItems IsNot Nothing Then
            _listPresenter.Delete(ListView.SelectedItems.ToTipItems())
            ListView.Refresh()
        End If
    End Sub

    ''' <summary>
    ''' 改
    ''' </summary>
    Private Sub UpdateTip(sender As Object, e As EventArgs) Handles ListView.DoubleClick, ListPopupMenuEditItem.Click
        If ListView.SelectedItem IsNot Nothing AndAlso ListView.SelectedCount = 1 Then
            _listPresenter.Update(ListView.SelectedItem)
            ListView.Refresh()
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
            _listPresenter.Paste(ListView.SelectedItem)
            ListView.Refresh()
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

#End Region

#End Region

End Class