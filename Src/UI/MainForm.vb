Imports System.IO
Imports System.Text
Imports System.Threading

Imports SU = DesktopTips.GlobalModel
Imports DD = DevComponents.DotNetBar
Imports QRCoder
Imports System.Text.RegularExpressions
Imports Newtonsoft.Json

Public Class MainForm

#Region "PosMove"

    Private PushDownMouseInScreen As Point
    Private PushDownWindowPos As Point
    Private PushDownWindowSize As Point
    Private IsMouseDown As Boolean

    ''' <summary>
    ''' 点下
    ''' </summary>
    Private Sub Flag_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles ListView.MouseDown, TabStrip.MouseDown, LabelNothing.MouseDown, ButtonResizeFlag.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Left Then
            IsMouseDown = True
        End If
        PushDownMouseInScreen = Cursor.Position
        PushDownWindowPos = New Point(Me.Left, Me.Top)
        PushDownWindowSize = New Point(Me.Width, Me.Height)
    End Sub

    ''' <summary>
    ''' 放开
    ''' </summary>
    Private Sub Flag_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles ListView.MouseUp, TabStrip.MouseUp, LabelNothing.MouseUp, ButtonResizeFlag.MouseUp
        Me.Cursor = Cursors.Default
        IsMouseDown = False
        ListView.Refresh()
    End Sub

    ''' <summary>
    ''' 重新进入
    ''' </summary>
    Private Sub Flag_MouseEnter(sender As Object, e As System.EventArgs) Handles ListView.MouseEnter, TabStrip.MouseEnter, LabelNothing.MouseEnter, ButtonResizeFlag.MouseEnter
        If IsMouseDown = True Then
            Me.Cursor = Cursors.Default
            IsMouseDown = False
            ListView.Refresh()
        End If
    End Sub

    ''' <summary>
    ''' 窗口移动
    ''' </summary>
    Private Sub ListView_TabStrip_MouseMove(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles ListView.MouseMove, TabStrip.MouseMove, LabelNothing.MouseMove
        If IsMouseDown Then
            Me.Cursor = Cursors.SizeAll
            Me.Top = PushDownWindowPos.Y + Cursor.Position.Y - PushDownMouseInScreen.Y
            Me.Left = PushDownWindowPos.X + Cursor.Position.X - PushDownMouseInScreen.X
        End If
    End Sub

    ''' <summary>
    ''' 大小调整
    ''' </summary>
    Private Sub ButtonResizeFlag_MouseMove(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles ButtonResizeFlag.MouseMove
        If IsMouseDown Then
            Me.Width = PushDownWindowSize.X + Cursor.Position.X - PushDownMouseInScreen.X
        End If
    End Sub

    Private Sub MainForm_SizeChanged(sender As Object, e As System.EventArgs) Handles Me.SizeChanged
        LabelNothing.Top = ListView.Top + 1
        LabelNothing.Left = ListView.Left + 1
        LabelNothing.Height = ListView.Height - 2
        LabelNothing.Width = ListView.Width - 2

        ' LabelNothing.Anchor = AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Top Or AnchorStyles.Bottom
    End Sub

#End Region

#Region "Show End Act DAct"

    ''' <summary>
    ''' 透明度
    ''' </summary>
    Private MaxOpacity As Double = 0.6
    ''' <summary>
    ''' 透明速度
    ''' </summary>
    Private Const OpacitySpeed As Double = 0.08

    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '''''''''''''''''''''''''''''''''''''''''''''' Load / End Form ''''''''''''''''''''''''''''''''''''''''''''''
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

    Private Sub TimerShowForm_Tick(sender As System.Object, e As System.EventArgs) Handles TimerShowForm.Tick
        Me.Opacity += OpacitySpeed
        Me.Top += 1
        If Me.Opacity >= MaxOpacity Then
            Me.Opacity = MaxOpacity
            TimerShowForm.Enabled = False
        End If
    End Sub

    Private Sub TimerEndForm_Tick(sender As System.Object, e As System.EventArgs) Handles TimerEndForm.Tick
        Me.Opacity -= OpacitySpeed
        Me.Top -= 1
        If Me.Opacity <= 0 Then
            Me.Top = Me.Top + (1 / OpacitySpeed)
            Me.Close()
            TimerEndForm.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' 窗口关闭判断，不能用在 FormClosing 事件处理
    ''' </summary>
    Private Sub ButtonCloseForm_Click(sender As System.Object, e As System.EventArgs) Handles ButtonCloseForm.Click, ListPopupMenuExit.Click
        Dim ok As Integer = MessageBox.Show("确定退出 DesktopTips 吗？", "关闭", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
        If ok = vbYes Then
            TimerMouseIn.Interval = 10000
            TimerMouseIn.Stop()
            TimerMouseIn.Enabled = False
            TimerMouseOut.Interval = 10000
            TimerMouseOut.Stop()
            TimerMouseOut.Enabled = False

            TimerEndForm.Enabled = True
        End If
    End Sub

    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '''''''''''''''''''''''''''''''''''''''''''''' Act / DAct Form ''''''''''''''''''''''''''''''''''''''''''''''
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

    Private Sub TimerMouseIn_Tick(sender As System.Object, e As System.EventArgs) Handles TimerMouseIn.Tick
        Me.Opacity += 0.05
        If Me.Opacity >= 1 Then
            TimerMouseIn.Stop()
            TimerMouseIn.Enabled = False
        End If
    End Sub

    Private Sub TimerMouseOut_Tick(sender As System.Object, e As System.EventArgs) Handles TimerMouseOut.Tick
        Me.Opacity -= 0.02
        If Me.Opacity <= MaxOpacity Then
            Me.Opacity = MaxOpacity
            TimerMouseOut.Stop()
            TimerMouseOut.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' 设置移入移出事件
    ''' </summary>
    Private Sub SetupMouseEnterLeave()
        AddHandler Me.MouseMove, AddressOf FormMouseMove
        AddHandler Me.MouseLeave, AddressOf FormMouseLeave
        For Each Ctrl As Control In Me.Controls
            AddHandler Ctrl.MouseMove, AddressOf FormMouseMove
            AddHandler Ctrl.MouseLeave, AddressOf FormMouseLeave
        Next
    End Sub

    ''' <summary>
    ''' 鼠标移动
    ''' </summary>
    Private Sub FormMouseMove(sender As Object, e As System.EventArgs)
        If Cursor.Position.X >= Me.Left And Cursor.Position.X <= Me.Right And _
            Cursor.Position.Y >= Me.Top And Cursor.Position.Y <= Me.Bottom Then
            TimerMouseOut.Stop()
            TimerMouseOut.Enabled = False
            TimerMouseIn.Enabled = True
            TimerMouseIn.Start()
        End If
    End Sub

    ''' <summary>
    ''' 鼠标移出，并且没有popup
    ''' </summary>
    Private Sub FormMouseLeave(sender As Object, e As System.EventArgs)
        If ListPopupMenu.PopupControl Is Nothing _
            AndAlso TabPopupMenu.PopupControl Is Nothing _
            AndAlso TabStrip.ContextMenu Is Nothing _
            AndAlso ListPopupMenuMove.PopupControl Is Nothing _
            AndAlso IsMenuPopuping = False Then

            TimerMouseIn.Stop()
            TimerMouseIn.Enabled = False
            TimerMouseOut.Enabled = True
            TimerMouseOut.Start()
        End If
    End Sub

    Private IsMenuPopuping As Boolean = False

    ''' <summary>
    ''' TabStrip 菜单弹出
    ''' </summary>
    Private Sub TabStrip_PopupOpen(sender As System.Object, e As System.EventArgs) Handles TabStrip.PopupOpen
        IsMenuPopuping = True
    End Sub

    ''' <summary>
    ''' Popup 关闭，不能使用 Close
    ''' </summary>
    Private Sub PopMenu_PopupFinalizedAndClosed(sender As Object, e As System.EventArgs) Handles ListPopupMenu.PopupFinalized, TabPopupMenu.PopupFinalized, ListPopupMenuMove.PopupFinalized, TabStrip.PopupClose
        IsMenuPopuping = False
        FormMouseLeave(sender, e)
    End Sub

#End Region

#Region "Setting & SetupUI"

    Public HotKey As Keys = Keys.F4

    Public Sub SaveSetting()
        NativeMethod.UnRegisterHotKey(Handle, 0)

        Dim setting As SettingUtil.AppSetting = SettingUtil.LoadAppSettings()
        setting.Top = Me.Top
        setting.Left = Me.Left
        setting.Height = Me.Height
        setting.Width = Me.Width
        setting.MaxOpacity = MaxOpacity
        setting.TopMost = Me.TopMost
        setting.IsFold = ListPopupMenuFold.Checked
        setting.HighLightColor = ListPopupMenuWinHighColor.SelectedColor

        SettingUtil.SaveAppSettings(setting)
    End Sub

    Public Sub LoadSetting()
        Dim setting As SettingUtil.AppSetting = SettingUtil.LoadAppSettings()

        Me.Top = setting.Top
        Me.Left = setting.Left
        Me.Height = setting.Height
        Me.Width = setting.Width

        MaxOpacity = setting.MaxOpacity
        Me.TopMost = setting.TopMost
        ListPopupMenuFold.Checked = setting.IsFold
        ListPopupMenuWinHighColor.SelectedColor = setting.HighLightColor

        ListPopupMenuLoadPos.Enabled = Not (setting.SaveLeft = -1 Or setting.SaveTop = -1)
    End Sub

    ''' <summary>
    ''' 窗口关闭后保存文件和设置
    ''' </summary>
    Private Sub MainForm_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        SaveList()
        SaveSetting()
    End Sub

    Private Sub MainForm_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        LoadSetting()
        Me.Refresh()

        NumericUpDownListCnt.Value = (Me.Height - 27) \ 17
        ButtonRemoveItem.Enabled = False

        Me.Top = Me.Top - (MaxOpacity / OpacitySpeed)
        TimerShowForm.Enabled = True
        TimerShowForm.Start()

        ListPopupMenuWinTop.Checked = Me.TopMost

        Me.TabStrip.Tabs.Remove(Me.TabItemTest)
        Me.TabStrip.Tabs.Remove(Me.TabItemTest2)

        ListView.Items.Clear()
        SetupMouseEnterLeave()
        SetupUpDownButtonsPos()
        LoadList()
        FormOpacity_Load()
        FoldMenu(ListPopupMenuFold.Checked)

        If Not NativeMethod.RegisterHotKey(Handle, 0, Nothing, HotKey) Then
            MessageBox.Show("快捷键已被占用，请重新设置", "快捷键", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub SetupUpDownButtonsPos()
        Dim height As Integer = ListView.ItemHeight

        ButtonItemUp.Visible = False
        ButtonItemUp.Height = (height + 1) / 2
        ButtonItemUp.Width = height

        ButtonItemDown.Visible = False
        ButtonItemDown.Height = (height + 1) / 2
        ButtonItemDown.Width = height
    End Sub

    ''' <summary>
    ''' 设置快捷键相应
    ''' </summary>
    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        If m.Msg = NativeMethod.WM_HOTKEY Then
            Me.Activate()
            Me.TopMost = True
            Me.TopMost = False
            TimerMouseIn.Enabled = True
            TimerMouseIn.Start()
        End If
        MyBase.WndProc(m)
    End Sub

#End Region

#Region "Fold"

    ''' <summary>
    ''' 折叠菜单
    ''' </summary>
    Private Sub ListPopupMenuFold_Click(sender As System.Object, e As System.EventArgs) Handles ListPopupMenuFold.Click
        ListPopupMenuFold.Checked = Not ListPopupMenuFold.Checked
        FoldMenu(ListPopupMenuFold.Checked)
    End Sub

    ''' <summary>
    ''' 折叠菜单
    ''' </summary>
    ''' <param name="IsFold"></param>
    Private Sub FoldMenu(ByVal IsFold As Boolean)
        If IsFold Then
            Me.ListPopupMenuItemContainer.SubItems.Clear()
            Me.ListPopupMenu.SubItems.Clear()

            Me.ListPopupMenuItemContainer.SubItems.AddRange( _
                New DD.BaseItem() {Me.ListPopupMenuMoveUp, Me.ListPopupMenuMoveDown, Me.ListPopupMenuAddItem, Me.ListPopupMenuRemoveItem, _
                                   Me.ListPopupMenuEditItem, Me.ListPopupMenuCopy, Me.ListPopupMenuSelectAll, Me.ListPopupMenuHighLight})

            Me.ListPopupMenu.SubItems.AddRange( _
                New DD.BaseItem() {Me.ListPopupMenuLabelSelItem, Me.ListPopupMenuLabelSelItemText, Me.ListPopupMenuLabelItemList, _
                                   Me.ListPopupMenuItemContainer, _
                                   Me.ListPopupMenuMoveTop, Me.ListPopupMenuMoveBottom, Me.ListPopupMenuViewHighLight, Me.ListPopupMenuFind, Me.ListPopupMenuMove, _
                                   Me.ListPopupMenuLabelItemFile, Me.ListPopupMenuOpenDir, Me.ListPopupMenuViewFile, Me.ListPopupMenuOpenBrowser, Me.ListPopupMenuSyncData, _
                                   Me.ListPopupMenuLabelItemWindow, Me.ListPopupMenuWinSetting, Me.ListPopupMenuExit})

            Me.ListPopupMenuAddItem.BeginGroup = True
            Me.ListPopupMenuMoveUp.BeginGroup = True
            Me.ListPopupMenuMoveTop.BeginGroup = True
            Me.ListPopupMenuCopy.BeginGroup = True

            For Each Item As DD.ButtonItem In Me.ListPopupMenuItemContainer.SubItems
                Item.Tooltip = Item.Text
            Next
        Else
            For Each Item As DD.ButtonItem In Me.ListPopupMenuItemContainer.SubItems
                Item.Tooltip = ""
            Next

            Me.ListPopupMenuAddItem.BeginGroup = False
            Me.ListPopupMenuMoveUp.BeginGroup = True
            Me.ListPopupMenuMoveTop.BeginGroup = False
            Me.ListPopupMenuCopy.BeginGroup = True

            Me.ListPopupMenuItemContainer.SubItems.Clear()
            Me.ListPopupMenu.SubItems.Clear()
            Me.ListPopupMenu.SubItems.AddRange( _
                New DD.BaseItem() {Me.ListPopupMenuLabelSelItem, Me.ListPopupMenuLabelSelItemText, Me.ListPopupMenuLabelItemList, _
                                   Me.ListPopupMenuAddItem, Me.ListPopupMenuRemoveItem, Me.ListPopupMenuEditItem, _
                                   Me.ListPopupMenuMoveUp, Me.ListPopupMenuMoveDown, Me.ListPopupMenuMoveTop, Me.ListPopupMenuMoveBottom, _
                                   Me.ListPopupMenuCopy, Me.ListPopupMenuSelectAll, Me.ListPopupMenuHighLight, Me.ListPopupMenuViewHighLight, _
                                   Me.ListPopupMenuFind, Me.ListPopupMenuMove, _
                                   Me.ListPopupMenuLabelItemFile, Me.ListPopupMenuOpenDir, Me.ListPopupMenuViewFile, Me.ListPopupMenuOpenBrowser, Me.ListPopupMenuSyncData, _
                                   Me.ListPopupMenuLabelItemWindow, Me.ListPopupMenuWinSetting, Me.ListPopupMenuExit})

        End If
    End Sub

#End Region

#Region "Opacity"

    Dim ops() As Double = {0.2, 0.4, 0.6, 0.7, 0.8, 1.0}
    Dim opBtns(ops.Length - 1) As DD.ButtonItem

    ''' <summary>
    ''' 动态添加透明度
    ''' </summary>
    Private Sub FormOpacity_Load()
        For i = 0 To ops.Length - 1
            opBtns(i) = New DD.ButtonItem With { _
                .Name = "ListPopupMenuOpacity" & CInt(ops(i) * 100), _
                .Tag = ops(i), _
                .Text = CInt(ops(i) * 100) & "%"
            }

            AddHandler opBtns(i).Click, AddressOf ListPopupMenuOpacity_Click
            Me.ListPopupMenuOpacity.SubItems.Add(opBtns(i))

            If MaxOpacity = opBtns(i).Tag Then
                opBtns(i).Checked = True
            End If
        Next
    End Sub

    Private Sub ListPopupMenuOpacity_Click(sender As System.Object, e As System.EventArgs)

        Dim btn As DD.ButtonItem = CType(sender, DD.ButtonItem)

        MaxOpacity = CDbl(btn.Tag())

        For Each btnS In opBtns
            btnS.Checked = False
        Next

        btn.Checked = True

        TimerMouseOut.Enabled = True
        TimerMouseOut.Start()
    End Sub

#End Region

    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

#Region "List Sel"

    ''' <summary>
    ''' 窗口失去焦点，取消选择
    ''' </summary>
    Private Sub MainForm_Deactivate(sender As Object, e As System.EventArgs) Handles Me.Deactivate
        ListView.ClearSelected()
        If Me.Opacity <> MaxOpacity Then
            TimerMouseOut.Enabled = True
            TimerMouseOut.Start()
        End If
    End Sub

    ''' <summary>
    ''' 边栏点击，取消选择
    ''' </summary>
    Private Sub TabStrip_Click(sender As System.Object, e As System.EventArgs) Handles TabStrip.Click
        ListView.ClearSelected()
    End Sub

    ''' <summary>
    ''' 没有选择列表项，取消选择
    ''' </summary>
    Private Sub ListView_MouseDown_Sel(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles ListView.MouseDown, TabStrip.MouseDown
        ListView_SelectedIndexChanged(sender, New System.EventArgs)

        If ListView.Items.Count > 0 Then
            Dim rect As Rectangle = ListView.GetItemRectangle(ListView.Items.Count - 1)
            If e.Y > rect.Top + rect.Height Then ListView.ClearSelected()
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

    ''' <summary>
    ''' 按钮焦点
    ''' </summary>
    Private Sub ButtonFocus_Handle(sender As Object, e As System.Windows.Forms.MouseEventArgs) _
        Handles ButtonAddItem.MouseDown, ButtonRemoveItem.MouseDown, ButtonCloseForm.MouseDown, ButtonListSetting.MouseDown, _
        ButtonItemUp.MouseDown, ButtonItemDown.MouseDown, ButtonResizeFlag.MouseDown

        LabelFocus.Select()
    End Sub

#End Region

#Region "Wheel"

    ' 滚动条获得焦点
    ' Me.Left + ListView.Left + ListView.Width - 20
    Private Sub ListView_MouseCaptureChanged(sender As Object, e As System.EventArgs) Handles ListView.MouseCaptureChanged
        If Cursor.Position.X > Me.Left + ListView.Left + ListView.Width - 20 Then
            SetSelectedItemButtonHide()
        End If
    End Sub

    ' 滚动
    Private Sub ListView_MouseWheel(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles ListView.MouseWheel
        SetSelectedItemButtonHide()
    End Sub

#End Region

#Region "File Content"

    ''' <summary>
    ''' MyBase.Load
    ''' </summary>
    ''' <param name="IsAlreadyLoadTab">是否已经初始化分组 (True: 不需要更新 分组 和 Curr)</param>
    Private Sub LoadList(Optional ByVal IsAlreadyLoadTab = False)
        Try
            SU.LoadTabTipsData()
        Catch ex As FileLoadException
            Dim Msg As String = "错误：" & ex.Message & Chr(10) & "是否打开文件位置检查文件？"
            Dim ok As MsgBoxResult = MessageBoxEx.Show(Msg, "错误", MessageBoxButtons.YesNo, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, {"開く", "キャンセル"})
            If ok = vbYes Then
                ListPopupMenuOpenFile_Click(ListPopupMenuOpenDir, New EventArgs)
            End If
            Application.Exit()
            Return
        End Try

        ListView.Items.Clear()
        For Each Tip As TipItem In SU.Tabs.Item(SU.CurrTabIdx).Tips
            ListView.Items.Add(Tip)
        Next

        If Not IsAlreadyLoadTab Then
            For i = 0 To SU.Tabs.Count - 1
                AddTab(SU.Tabs.Item(i).Title)
            Next
        End If

        LabelNothing.Visible = ListView.Items.Count = 0
    End Sub

    ''' <summary>
    ''' 保存数据
    ''' </summary>
    Private Sub SaveList()
        Dim Tips As New List(Of TipItem)
        For Each Tip As TipItem In ListView.Items.Cast(Of TipItem)()
            Tips.Add(Tip)
        Next

        SU.Tabs.Item(SU.CurrTabIdx).Tips = New List(Of TipItem)(Tips)
        SU.SaveTabData()
        LabelNothing.Visible = ListView.Items.Count = 0
    End Sub

#End Region

#Region "增删改移动"

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
            Dim ok As Integer = MessageBoxEx.Show("确定删除以下 " & SelectItemIdics.Count & " 个提醒标签吗？" & Chr(10) & Chr(10) & sb.ToString, "删除", MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
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

#Region "辅助按钮"

    ''' <summary>
    ''' 点击项，显示按钮
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
    ''' 隐藏按钮
    ''' </summary>
    Private Sub SetSelectedItemButtonHide()
        ButtonItemUp.Visible = False
        ButtonItemDown.Visible = False
    End Sub

    ''' <summary>
    ''' 调整大小，辅助按钮位置调整
    ''' </summary>
    Private Sub ListView_SizeChanged(sender As Object, e As System.EventArgs) Handles ListView.SizeChanged
        ' ListView.Refresh()
        If ButtonItemUp.Visible = True AndAlso ListView.SelectedIndices.Count = 1 Then
            Dim Rect As Rectangle = ListView.GetItemRectangle(ListView.SelectedIndex)
            SetSelectedItemButtonShow(Rect)
        End If
    End Sub

#End Region

#Region "选择 Enable"

    ''' <summary>
    ''' 高亮判断 辅助按钮显示
    ''' </summary>
    Private Sub ListView_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles ListView.SelectedIndexChanged
        If ListView.SelectedIndices.Count = 1 Then
            SetSelectedItemButtonShow(ListView.GetItemRectangle(ListView.SelectedIndex))
        Else
            SetSelectedItemButtonHide()
        End If

        SelCheck()
        ListView.Refresh()
    End Sub

    ''' <summary>
    ''' ListView_MouseMoveHover 用 HoverIdx
    ''' </summary>
    Dim HoverIdx As Integer = -1

    ''' <summary>
    ''' 悬浮弹出提示
    ''' </summary>
    Private Sub ListView_MouseMoveHover(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles ListView.MouseMove
        Dim Idx = ListView.IndexFromPoint(e.Location)
        If HoverIdx <> Idx AndAlso Idx <> -1 AndAlso Idx < ListView.Items.Count Then
            HoverToolTip.Hide(ListView)
            HoverToolTip.SetToolTip(ListView, CType(ListView.Items.Item(Idx), TipItem).TipContent)
            HoverIdx = Idx
        End If
    End Sub

    ''' <summary>
    ''' 选择可用性
    ''' </summary>
    Private Sub SelCheck()
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
        CmdsPopupMenuAppend.Enabled = IsSingle

        ' 浏览器
        If IsSingle Then
            Dim item As TipItem = CType(ListView.SelectedItem, TipItem)
            ListPopupMenuOpenBrowser.Enabled = item.TipContent.IndexOf("http://") <> -1 Or item.TipContent.IndexOf("https://") <> -1
        Else
            Dim ok As Boolean = False
            For Each item As TipItem In ListView.SelectedItems.Cast(Of TipItem)()
                If item.TipContent.IndexOf("http://") <> -1 Or item.TipContent.IndexOf("https://") <> -1 Then
                    ok = True
                    Exit For
                End If
            Next
            ListPopupMenuOpenBrowser.Enabled = ok
        End If
    End Sub

#End Region

#Region "复制 全选 查找"

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
    ''' 查找结果
    ''' </summary>
    Public SearchResult As New List(Of Tuple(Of Integer, Integer))

    ''' <summary>
    ''' 搜索文字
    ''' </summary>
    ''' <remarks></remarks>
    Public SearchText As String

    ''' <summary>
    ''' 查找文字
    ''' </summary>
    Public Sub ListPopupMenuFind_Click(sender As System.Object, e As System.EventArgs) Handles ListPopupMenuFind.Click
        SearchResult.Clear()
        SearchText = InputBox("请输入查找的文字：", "查找").Trim()

        If SearchText <> "" Then
            Dim spl() As String = SearchText.Split(" ")
            For Each Tab As Tab In SU.Tabs
                For Each Tip As TipItem In Tab.Tips
                    If Tip.TipContent.ToLower.Contains(SearchText.ToLower) Then
                        SearchResult.Add(New Tuple(Of Integer, Integer)(SU.Tabs.IndexOf(Tab), Tab.Tips.IndexOf(Tip)))
                    End If
                Next
            Next

            SearchForm.Close()
            If SearchResult.Count = 0 Then
                MessageBoxEx.Show("未找到 """ & SearchText & """ 。", "查找", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
            Else
                SearchForm.Show(Me)
            End If
        End If
    End Sub

#End Region

#Region "调整大小 弹出菜单 快捷键"

    ''' <summary>
    ''' 调整大小
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

    ''' <summary>
    ''' 显示弹出菜单
    ''' </summary>
    Private Sub ButtonChangeHeight_Click(sender As System.Object, e As System.EventArgs) Handles ButtonListSetting.Click
        SelCheck()
        ListPopupMenu.Popup(Me.Left + sender.Left, Me.Top + sender.Top + sender.Height - 1)
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
    ''' Popup 显示调整大小
    ''' </summary>
    Private Sub ListPopupMenuListHeight_Click(sender As System.Object, e As System.EventArgs) Handles ListPopupMenuListHeight.Click
        NumericUpDownListCnt.Visible = ListPopupMenuListHeight.Checked
    End Sub

#End Region

#Region "打开窗口 浏览器"

    ''' <summary>
    ''' 打开文件所在位置
    ''' </summary>
    Private Sub ListPopupMenuOpenFile_Click(sender As System.Object, e As System.EventArgs) Handles ListPopupMenuOpenDir.Click
        'Dim path As String = FileDir
        'Process.Start(path)
        'C:\Users\Windows 10\AppData\Roaming\DesktopTips

        System.Diagnostics.Process.Start("explorer.exe", "/select,""" & SU.StorageJsonFile & """")
    End Sub

    ''' <summary>
    ''' 显示文本窗体
    ''' </summary>
    ''' <param name="Title">窗口标题</param>
    ''' <param name="Content">窗口文本内容</param>
    ''' <param name="TextColor">文字颜色</param>
    Private Sub ShowForm(ByVal Title As String, ByVal Content As String, ByVal TextColor As Color)
        Dim WinSize As Size = New Size(500, 300)
        Dim TextSize As Size = New Size(WinSize.Width - 16, WinSize.Height - 39)

        Dim TextBox As New TextBox With {.Text = Content, .ReadOnly = True, .Multiline = True, .ScrollBars = ScrollBars.Both, .WordWrap = False, _
                                         .Size = TextSize, .BackColor = Color.White, .ForeColor = TextColor, .Font = New System.Drawing.Font("Microsoft YaHei UI", 9.0!), _
                                         .Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Top}

        Dim Win As New Form With {.FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable, .Text = Title, .Size = WinSize, .TopMost = True}
        Win.Controls.Add(TextBox)
        Win.Show()

        Win.Top = Me.Top
        Win.Left = Me.Left - WinSize.Width - 15
        TextBox.Select(0, 0)
    End Sub

    ''' <summary>
    ''' 浏览文件
    ''' </summary>
    Private Sub ListPopupMenuViewFile_Click(sender As System.Object, e As System.EventArgs) Handles ListPopupMenuViewFile.Click
        Dim sb As New StringBuilder
        For Each Item As TipItem In ListView.Items.Cast(Of TipItem)()
            sb.AppendLine(Item.TipContent & If(Item.IsHighLight, " [高亮]", ""))
        Next
        ShowForm("浏览文件 (共 " & ListView.Items.Count & " 项)", sb.ToString(), Color.Black)
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
        ShowForm("查看高亮 (共 " & idx & " 项)", sb.ToString, ListPopupMenuWinHighColor.SelectedColor)
    End Sub

    ''' <summary>
    ''' 打开浏览器
    ''' </summary>
    Private Sub ListPopupMenuOpenBrowser_Click(sender As System.Object, e As System.EventArgs) Handles ListPopupMenuOpenBrowser.Click
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
        Dim sp() As String
        For Each item As TipItem In Sel
            sp = item.TipContent.Split(New Char() {" "}, StringSplitOptions.RemoveEmptyEntries)
            For Each link As String In sp
                If link.StartsWith("http://") Or link.StartsWith("https://") Then
                    links.Add(link)
                End If
            Next
        Next

        ' 打开
        If links.Count = 0 Then
            MessageBox.Show("所选项不包含任何链接。", "打开链接", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            Dim ok As MessageBoxButtons = _
                MessageBox.Show("是否打开以下链接：" + Chr(10) + Chr(10) + String.Join(Chr(10), links), "打开链接", MessageBoxButtons.OKCancel, MessageBoxIcon.Information)
            If ok = MsgBoxResult.Ok Then
                For Each link As String In links
                    Process.Start(link)
                Next
            End If
        End If
    End Sub

#End Region

#Region "弹出菜单 高亮 置顶"

    ''' <summary>
    ''' TopMost
    ''' </summary>
    Private Sub ListPopupMenuWinTop_Click(sender As System.Object, e As System.EventArgs) Handles ListPopupMenuWinTop.Click
        ListPopupMenuWinTop.Checked = Not ListPopupMenuWinTop.Checked
        Me.TopMost = ListPopupMenuWinTop.Checked
    End Sub

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
    ''' 显示弹出菜单标题
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
        For Each Tip As TipItem In SU.Tabs.Item(SU.CurrTabIdx).Tips
            HLItemCnt += If(Tip.IsHighLight, 1, 0)
        Next
        ListPopupMenuLabelItemList.Text = "列表 (共 " & ListView.Items.Count & " 项，高亮 " & HLItemCnt & " 项)"

        e.Cancel = False
        ListPopupMenu.Refresh()
    End Sub

#End Region

#Region "保存位置"

    ''' <summary>
    ''' 保存位置
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

    ''' <summary>
    ''' 加载位置
    ''' </summary>
    Private Sub ListPopupMenuLoadPos_Click(sender As System.Object, e As System.EventArgs) Handles ListPopupMenuLoadPos.Click
        Dim setting As SettingUtil.AppSetting = SettingUtil.LoadAppSettings()
        Me.Top = setting.SaveTop
        Me.Left = setting.SaveLeft
    End Sub

#End Region

    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

#Region "附加不可见功能"

    ''' <summary>
    ''' 粘贴附加在最后
    ''' </summary>
    Private Sub CmdsPopupMenuAppend_Click(sender As System.Object, e As System.EventArgs) Handles CmdsPopupMenuAppend.Click
        Dim clip$ = Clipboard.GetText()
        If Not String.IsNullOrWhiteSpace(clip) Then
            Dim currIdx% = ListView.SelectedIndex

            Dim tip As TipItem = SU.Tabs(SU.CurrTabIdx).Tips(currIdx)
            Dim motoStr$ = tip.TipContent

            tip.TipContent += " " & Clipboard.GetText().Trim()
            SaveList()

            Dim ok As DialogResult = MessageBoxEx.Show(
                "已经添加剪贴板内容，当前选中项内容为 """ & SU.Tabs(SU.CurrTabIdx).Tips(currIdx).TipContent & """，是否还原？",
                "附加内容",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, New String() {"OK", "元に戻す"})

            If ok = Windows.Forms.DialogResult.Cancel Then
                tip.TipContent = motoStr
                SaveList()
            End If
        End If
    End Sub

#End Region

    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

#Region "Tab"

    ''' <summary>
    ''' 添加分组
    ''' </summary>
    ''' <param name="Title">分组标题</param>
    ''' <param name="IsAddToStorage">是否添加到存储</param>
    Private Sub AddTab(ByVal Title As String, Optional ByVal IsAddToStorage As Boolean = False)

        If IsAddToStorage Then
            Dim NewTab As New Tab(Title)
            SU.Tabs.Add(NewTab)
            SU.SaveTabData()
        End If

        Dim NewSuperTabItem = New DD.SuperTabItem()
        NewSuperTabItem.GlobalItem = False
        NewSuperTabItem.Name = "TabItemCustom_" & SU.Tabs.Count
        NewSuperTabItem.Text = Title

        AddHandler NewSuperTabItem.MouseDown, AddressOf TabStrip_MouseDown
        ' AddHandler NewSuperTabItem.DoubleClick, AddressOf ListPopupMenuRenameTab_Click

        Me.TabStrip.Tabs.AddRange(New DD.BaseItem() {NewSuperTabItem})
    End Sub

    ''' <summary>
    ''' 新建分组
    ''' </summary>
    Private Sub ListPopupMenuNewTab_Click(sender As System.Object, e As System.EventArgs) Handles TabPopupMenuNewTab.Click
        Dim tabName As String = InputBox("请输入新分组的标题: ", "新建", "分组")
        tabName = tabName.Trim()
        If tabName <> "" Then
            If Tab.CheckDuplicateTab(tabName, SU.Tabs) IsNot Nothing Then
                MessageBoxEx.Show("分组标题 """ & tabName & """ 已存在。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
                ListPopupMenuNewTab_Click(sender, New System.EventArgs)
            Else
                AddTab(tabName, True)
            End If
        End If
    End Sub

    ''' <summary>
    ''' 删除分组
    ''' </summary>
    Private Sub ListPopupMenuDeleteTab_Click(sender As System.Object, e As System.EventArgs) Handles TabPopupMenuDeleteTab.Click
        If TabStrip.Tabs.Count = 1 Then
            MessageBoxEx.Show("无法删除最后的分组。", "删除", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
        Else
            Dim TabTitle As String = TabStrip.SelectedTab.Text
            Dim Tab As Tab = Tab.GetTabFromTitle(TabTitle)
            If Tab.Tips.Count <> 0 Then
                MessageBoxEx.Show("分组内存在 " & Tab.Tips.Count & " 条记录无法删除，请先将记录移动到别的分组。", "删除", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            Else
                Dim ok As DialogResult = MessageBoxEx.Show("是否删除分组 """ + TabStrip.SelectedTab.Text + """？", "删除", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                If ok = vbOK Then
                    TabStrip.Tabs.RemoveAt(TabStrip.SelectedTabIndex)
                    SU.Tabs.RemoveAt(SU.Tabs.IndexOf(Tab.GetTabFromTitle(TabTitle)))
                    SU.SaveTabData()
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' 重命名分组 !!!
    ''' </summary>
    Private Sub ListPopupMenuRenameTab_Click(sender As System.Object, e As System.EventArgs) Handles TabPopupMenuRenameTab.Click
        If TabStrip.SelectedTabIndex <> -1 Then
            Dim OldName As String = TabStrip.SelectedTab.Text
            Dim NewName As String = InputBox("重命名分组 """ & OldName & """ 为: ", "重命名", OldName)
            NewName = NewName.Trim()
            If NewName <> "" Then
                If Tab.CheckDuplicateTab(NewName, SU.Tabs) IsNot Nothing Then
                    MessageBoxEx.Show("分组标题 """ & NewName & """ 已存在。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
                    ListPopupMenuRenameTab_Click(sender, New System.EventArgs)
                Else
                    Tab.GetTabFromTitle(OldName).Title = NewName
                    SU.SaveTabData()
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
            ListPopupMenuNewTab_Click(sender, New EventArgs)
        Else
            ListPopupMenuRenameTab_Click(sender, New EventArgs)
        End If
    End Sub

    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

    ''' <summary>
    ''' 右键点击Tab选中
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
    ''' Tab选择更改
    ''' </summary>
    Private Sub TabStrip_SelectedTabChanged(sender As Object, e As DD.SuperTabStripSelectedTabChangedEventArgs) Handles TabStrip.SelectedTabChanged
        SetSelectedItemButtonHide()
        If TabStrip.SelectedTabIndex <> -1 And SU.Tabs.Count <> 0 Then
            SU.CurrTabIdx = SU.Tabs.IndexOf(Tab.GetTabFromTitle(TabStrip.SelectedTab.Text))
            LoadList(True)
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
        SU.Tabs = NewTabs
        SU.CurrTabIdx = SU.Tabs.IndexOf(Tab.GetTabFromTitle(TabStrip.SelectedTab.Text))
        SU.SaveTabData()
    End Sub

#End Region

#Region "Move"

    ''' <summary>
    ''' 获得非活动分组集，移动至分组 用
    ''' </summary>
    ''' <param name="ClassNamePreFix">分组类名前缀 ListPopupMenuMove / TabPopupMenuMove</param>
    ''' <returns>分组按钮</returns>
    Private Function GetUnActivityTabMoveButtonList(ByVal ClassNamePreFix As String) As List(Of DD.ButtonItem)
        Dim Tabs As New List(Of String)
        Dim TabBtns As New List(Of DD.ButtonItem)

        For Each Tab As Tab In SU.Tabs
            If Tab.Title <> SU.Tabs.Item(SU.CurrTabIdx).Title Then
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

        TabPopupMenuLabel.Text = "分组 (共 " & SU.Tabs.Count & " 组)"
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

        Dim Src As String = SU.Tabs.Item(SU.CurrTabIdx).Title
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

        Dim ok As MessageBoxButtons = MessageBoxEx.Show(Flag, "移动至分组...", MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
        If ok = vbOK Then
            For Each Item As TipItem In SelectItems
                Tab.GetTabFromTitle(Dest).Tips.Add(Item)
                Tab.GetTabFromTitle(Src).Tips.Remove(Item)
            Next
            SU.SaveTabData()

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

    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

#Region "数据同步"

    Private Shared QR_CODE_MAGIC As String = "DESKTOP_TIPS_ANDROID://"

    Private Shared ipRe As New Regex("^((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$")
    Private Shared portRe As New Regex("^([0-9]|[1-9]\d{1,3}|[1-5]\d{4}|6[0-4]\d{4}|65[0-4]\d{2}|655[0-2]\d|6553[0-5])$")


    ''' <summary>
    ''' 同步到移动端 (本地 C -> 安卓 S) !!! 常用
    ''' 远程监听地址 -> 确定远程地址 -> 本地发送数据 -> 等待 ACK
    ''' </summary>
    Private Sub ListPopupMenuSyncDataTo_Click(sender As System.Object, e As System.EventArgs) Handles ListPopupMenuSyncDataTo.Click
        Dim input As String = InputBox("请输入移动端的地址：", "同步到移动端", "127.0.0.1:8776")

        If String.IsNullOrWhiteSpace(input) Then Return ' 空内容
        Dim sp As String() = input.Split(New Char() {":"})

        Dim ip As String, port As Integer
        While sp.Length <> 2 OrElse Not ipRe.IsMatch(sp(0)) OrElse Not portRe.IsMatch(sp(1)) OrElse Not Integer.TryParse(sp(1), port)
            MessageBox.Show("所输入的地址格式不正确。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error)

            input = InputBox("请输入移动端的地址：", "同步到移动端", input)
            If String.IsNullOrWhiteSpace(input) Then Return ' 空内容
            sp = input.Split(New Char() {":"})
        End While
        ip = sp(0)

        ' ----------------------------------------------
        ' Ip, Port << 发送到移动端

        Dim thread As New Thread(New ThreadStart(Sub() _
            SyncData.SendTabs(Ip:=ip, Port:=port, cb:= _
                Sub(ok As Exception)
                    If ok Is Nothing Then
                        Me.Invoke(New Action(Sub() _
                            MessageBox.Show("数据发送成功，请在移动端确认。", "同步到移动端", MessageBoxButtons.OK, MessageBoxIcon.Information)))
                    Else
                        Me.Invoke(New Action(Sub() _
                            MessageBox.Show("数据发送错误。" + Chr(10) + ok.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error)))
                    End If
                End Sub)))

        thread.Start()

    End Sub

    ''' <summary>
    ''' 从移动端同步 (安卓 C -> 本地 S) !!! 危险
    ''' 确定端口 -> 监听本地地址 -> 电脑端发送 -> 本地接受处理
    ''' </summary>
    Private Sub ListPopupMenuSyncDataFrom_Click(sender As System.Object, e As System.EventArgs) Handles ListPopupMenuSyncDataFrom.Click
        Dim ip As String = SyncData.GetLanIP()
        If String.IsNullOrWhiteSpace(ip) Then ' 地址错误
            MessageBox.Show("本机获取局域网内地址错误。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim input As String = InputBox("请输入本地监听的端口：", "从移动端同步", "8776")

        Dim port As Integer
        If String.IsNullOrWhiteSpace(input) Then Return ' 空内容
        While Not Integer.TryParse(input, port) OrElse Not (port >= 1 And port <= 65535) ' 端口错误
            MessageBox.Show("所输入的端口格式不正确，应为在 [1, 65535] 内的纯数字。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error)

            input = InputBox("请输入本地监听的端口：", "从移动端同步", input)
            If String.IsNullOrWhiteSpace(input) Then Return ' 空内容
        End While

        ' ---------------------------------------------
        ' 获得端口 << 监听

        Dim thread As Thread = Nothing
        Dim cancelFlag As Boolean = False

        Dim qrCodeForm As Form = SyncData.GetQrCodeForm(QR_CODE_MAGIC & ip & ":" & port)
        qrCodeForm.Text = "连接二维码 (" & ip & ":" & port & ")"
        AddHandler qrCodeForm.FormClosed, _
            Sub()
                If SyncData.rcvServerSocket IsNot Nothing Then
                    cancelFlag = True
                    If thread IsNot Nothing Then
                        thread.Interrupt()
                        thread = Nothing
                    End If
                    Try
                        SyncData.rcvServerSocket.Stop()
                    Catch ex As Exception
                        Console.WriteLine(ex.Message)
                    End Try
                    SyncData.rcvServerSocket = Nothing
                End If
            End Sub
        qrCodeForm.Show()

        ' 受け付け可能になっていません。このメソッドを呼び出す前に、Start() メソッドを呼び出してください。
        ' 
        thread = New Thread(New ThreadStart(Sub() _
            SyncData.ReceiveTabs(Port:=port, cb:= _
                Sub(ret As String, ok As Exception)
                    ' 待っている状態からスレッドが中断されました。
                    Try
                        Me.Invoke(New Action( _
                            Sub()
                                If cancelFlag Then ' 手动关闭
                                    qrCodeForm.Close()
                                    Return
                                End If

                                qrCodeForm.Close() ' 不关闭，自动关
                                If ok IsNot Nothing Then
                                    MessageBox.Show("数据接收错误。" + Chr(10) + ok.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                    Return
                                End If

                                ' TODO
                                Dim newList As List(Of Tab)
                                Try
                                    newList = JsonConvert.DeserializeObject(Of List(Of Tab))(ret)
                                    If newList Is Nothing Then
                                        Throw New Exception
                                    End If
                                Catch ex As Exception
                                    MessageBox.Show("数据格式有问题。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                    Return
                                End Try

                                Dim backupFileName As String = GlobalModel.SaveTabData(newList)
                                Dim isOpen As DialogResult = MessageBoxEx.Show("接收数据成功，新数据保存在 """ & backupFileName & """。", "同步", _
                                                                               MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, _
                                                                               New String() {"打开文件夹", "确定"})
                                If isOpen = Windows.Forms.DialogResult.Yes Then
                                    System.Diagnostics.Process.Start("explorer.exe", "/select,""" & backupFileName & """")
                                End If

                                ' !!!
                                'Dim newItems As List(Of Tab) = newList.Except(GlobalModel.Tabs).ToList()
                                'Dim deletedItems As List(Of Tab) = GlobalModel.Tabs.Except(newList).ToList()

                                'Dim result As DialogResult = MessageBoxEx.Show( _
                                '    "数据接收完成，共有 " & newItems.Count & " 条新记录，删除了" & deletedItems.Count & " 条记录。" & _
                                '    "是否只添加新的记录，还是删除被删除的记录，或者是覆盖所有同步数据？", "同步数据", _
                                '    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, _
                                '    New String() {"添加", "覆盖", "取消"})

                                'Dim backupFileName$ = ""

                                'Select Case result
                                '    Case Windows.Forms.DialogResult.Yes ' 添加
                                '        backupFileName = GlobalModel.SaveTabData(GlobalModel.Tabs) ' 备份
                                '        GlobalModel.Tabs.AddRange(newItems)
                                '        'Case Windows.Forms.DialogResult.No ' 删除
                                '        '    backupFileName = GlobalModel.SaveTabData(GlobalModel.Tabs)
                                '        '    GlobalModel.Tabs.RemoveAll(New Predicate(Of Tab)(Function(item As Tab) deletedItems.IndexOf(item) <> -1))
                                '    Case Windows.Forms.DialogResult.No ' 覆盖
                                '        backupFileName = GlobalModel.SaveTabData(GlobalModel.Tabs)
                                '        GlobalModel.Tabs = newList
                                '    Case Windows.Forms.DialogResult.Cancel
                                '        backupFileName = ""
                                'End Select

                                'If backupFileName <> "" Then
                                '    Dim isOpen As DialogResult = MessageBoxEx.Show("已完成同步，原始数据保存在 """ & backupFileName & """。", "同步", _
                                '                                                   MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, _
                                '                                                   New String() {"打开文件夹", "确定"})
                                '    If isOpen = Windows.Forms.DialogResult.Yes Then
                                '        System.Diagnostics.Process.Start("explorer.exe", "/select,""" & backupFileName & """")
                                '    End If
                                'End If

                            End Sub))
                    Catch ex As Exception
                        Console.WriteLine(ex.Message)
                    End Try
                End Sub)))

        thread.Start()

    End Sub

#End Region

End Class
