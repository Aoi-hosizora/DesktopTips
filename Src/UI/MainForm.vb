﻿Imports System.IO
Imports System.Text
Imports SU = DesktopTips.StorageUtil

Public Class MainForm

    Declare Sub mouse_event Lib "user32" (ByVal dwFlags As Integer, ByVal dx As Integer, ByVal dy As Integer, ByVal cButtons As Integer, ByVal dwExtraInfo As Integer)
    Private Declare Function SetCursorPos Lib "user32" (ByVal x As Long, ByVal y As Long) As Long

    Enum MouseEvent
        MOUSEEVENTF_LEFTDOWN = &H2      '左键按下
        MOUSEEVENTF_LEFTUP = &H4        '左键释放
        MOUSEEVENTF_MIDDLEDOWN = &H20   '中键按下
        MOUSEEVENTF_MIDDLEUP = &H40     '中键释放
        MOUSEEVENTF_RIGHTDOWN = &H8     '右键按下
        MOUSEEVENTF_RIGHTUP = &H10      '右键释放
        MOUSEEVENTF_MOVE = &H1          '指针移动
    End Enum

#Region "PosMove"

    Private PushDownMouseInScreen As Point
    Private PushDownWindowPos As Point
    Private PushDownWindowSize As Point
    Private IsMouseDown As Boolean

    Private Sub Flag_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles ListView.MouseDown, TabStrip.MouseDown, ButtonResizeFlag.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Left Then
            IsMouseDown = True
        End If
        PushDownMouseInScreen = Cursor.Position
        PushDownWindowPos = New Point(Me.Left, Me.Top)
        PushDownWindowSize = New Point(Me.Width, Me.Height)
    End Sub

    Private Sub Flag_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles ListView.MouseUp, TabStrip.MouseUp, ButtonResizeFlag.MouseUp
        Me.Cursor = Cursors.Default
        IsMouseDown = False
    End Sub

    Private Sub ListView_TabStrip_MouseMove(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles ListView.MouseMove, TabStrip.MouseMove
        If IsMouseDown Then
            Me.Cursor = Cursors.SizeAll
            Me.Top = PushDownWindowPos.Y + Cursor.Position.Y - PushDownMouseInScreen.Y
            Me.Left = PushDownWindowPos.X + Cursor.Position.X - PushDownMouseInScreen.X
        End If
    End Sub

    Private Sub ButtonResizeFlag_MouseMove(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles ButtonResizeFlag.MouseMove
        If IsMouseDown Then
            Me.Width = PushDownWindowSize.X + Cursor.Position.X - PushDownMouseInScreen.X
        End If
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

    Private Sub ButtonCloseForm_Click(sender As System.Object, e As System.EventArgs) Handles ButtonCloseForm.Click, ListPopupMenuExit.Click
        TimerMouseIn.Interval = 10000
        TimerMouseIn.Stop()
        TimerMouseIn.Enabled = False
        TimerMouseOut.Interval = 10000
        TimerMouseOut.Stop()
        TimerMouseOut.Enabled = False

        TimerEndForm.Enabled = True
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
        If ListPopupMenu.PopupControl Is Nothing Then
            TimerMouseIn.Stop()
            TimerMouseIn.Enabled = False
            TimerMouseOut.Enabled = True
            TimerMouseOut.Start()
        End If
    End Sub

    ''' <summary>
    ''' Popup 关闭，不能使用 Close
    ''' </summary>
    Private Sub ListPopMenu_PopupFinalized(sender As Object, e As System.EventArgs) Handles ListPopupMenu.PopupFinalized
        FormMouseLeave(sender, e)
    End Sub

#End Region

#Region "Setting & SetupUI"

    Private Sub MainForm_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Dim setting As SettingUtil.AppSetting
        setting.Top = Me.Top
        setting.Left = Me.Left
        setting.Height = Me.Height
        setting.Width = Me.Width
        setting.MaxOpacity = MaxOpacity
        setting.TopMost = Me.TopMost
        setting.IsFold = ListPopupMenuFold.Checked

        SettingUtil.SaveAppSettings(setting)
    End Sub

    Private Sub MainForm_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim setting As SettingUtil.AppSetting = SettingUtil.LoadAppSettings()
        Me.Top = setting.Top
        Me.Left = setting.Left
        Me.Height = setting.Height
        Me.Width = setting.Width
        MaxOpacity = setting.MaxOpacity
        Me.TopMost = setting.TopMost
        ListPopupMenuFold.Checked = setting.IsFold

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
    End Sub

    Private Sub SetupUpDownButtonsPos()
        Dim height As Integer = ListView.ItemHeight
        ButtonItemUp.Visible = False
        ButtonItemUp.Height = height
        ButtonItemUp.Width = height

        ButtonItemDown.Visible = False
        ButtonItemDown.Height = height
        ButtonItemDown.Width = height
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
                New DevComponents.DotNetBar.BaseItem() {Me.ListPopupMenuMoveUp, Me.ListPopupMenuMoveDown, _
                                                        Me.ListPopupMenuAddItem, Me.ListPopupMenuRemoveItem, Me.ListPopupMenuEditItem, Me.ListPopupMenuSelectAll, _
                                                        Me.ListPopupMenuHighLight})

            Me.ListPopupMenu.SubItems.AddRange( _
                New DevComponents.DotNetBar.BaseItem() {Me.ListPopupMenuLabelSelItem, Me.ListPopupMenuLabelSelItemText, _
 _
                                                        Me.ListPopupMenuLabelItemList, Me.ListPopupMenuItemContainer, _
                                                        Me.ListPopupMenuMoveTop, Me.ListPopupMenuMoveBottom, Me.ListPopupMenuViewHighLight, Me.ListPopupMenuMove, _
 _
                                                        Me.ListPopupMenuLabelItemFile, Me.ListPopupMenuOpenDir, Me.ListPopupMenuViewFile, Me.ListPopupMenuLabelItemWindow, _
                                                        Me.ListPopupMenuListHeight, Me.ListPopupMenuWinSetting, Me.ListPopupMenuExit})

            Me.ListPopupMenuMoveTop.BeginGroup = True
            Me.ListPopupMenuViewHighLight.BeginGroup = True
            For Each Item As DevComponents.DotNetBar.ButtonItem In Me.ListPopupMenuItemContainer.SubItems
                Item.Tooltip = Item.Text
            Next
        Else
            For Each Item As DevComponents.DotNetBar.ButtonItem In Me.ListPopupMenuItemContainer.SubItems
                Item.Tooltip = ""
            Next
            Me.ListPopupMenuMoveTop.BeginGroup = False
            Me.ListPopupMenuViewHighLight.BeginGroup = False

            Me.ListPopupMenuItemContainer.SubItems.Clear()
            Me.ListPopupMenu.SubItems.Clear()
            Me.ListPopupMenu.SubItems.AddRange( _
                New DevComponents.DotNetBar.BaseItem() {Me.ListPopupMenuLabelSelItem, Me.ListPopupMenuLabelSelItemText, _
 _
                                                        Me.ListPopupMenuLabelItemList, Me.ListPopupMenuMoveUp, Me.ListPopupMenuMoveDown, Me.ListPopupMenuMoveTop, Me.ListPopupMenuMoveBottom, _
                                                        Me.ListPopupMenuAddItem, Me.ListPopupMenuRemoveItem, Me.ListPopupMenuEditItem, Me.ListPopupMenuSelectAll, _
                                                        Me.ListPopupMenuHighLight, Me.ListPopupMenuViewHighLight, Me.ListPopupMenuMove, _
 _
                                                        Me.ListPopupMenuLabelItemFile, Me.ListPopupMenuOpenDir, Me.ListPopupMenuViewFile, Me.ListPopupMenuLabelItemWindow, _
                                                        Me.ListPopupMenuListHeight, Me.ListPopupMenuWinSetting, Me.ListPopupMenuExit})

        End If
    End Sub

#End Region

#Region "Opacity"

    Dim ops() As Double = {0.2, 0.4, 0.6, 0.7, 0.8, 1.0}
    Dim opBtns(ops.Length - 1) As DevComponents.DotNetBar.ButtonItem

    ''' <summary>
    ''' 动态添加透明度
    ''' </summary>
    Private Sub FormOpacity_Load()
        For i = 0 To ops.Length - 1
            opBtns(i) = New DevComponents.DotNetBar.ButtonItem With { _
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

        Dim btn As DevComponents.DotNetBar.ButtonItem = CType(sender, DevComponents.DotNetBar.ButtonItem)

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

#Region "List Sel Draw"

    ''' <summary>
    ''' 窗口失去焦点，取消选择
    ''' </summary>
    Private Sub MainForm_Deactivate(sender As Object, e As System.EventArgs) Handles Me.Deactivate
        ListView.ClearSelected()
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
        If e.Button = Windows.Forms.MouseButtons.Right And idx <> -1 And idx <> 65535 Then
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
        Handles ButtonAddItem.MouseDown, ButtonRemoveItem.MouseDown, ButtonCloseForm.MouseDown, ButtonSetting.MouseDown, _
        ButtonItemUp.MouseDown, ButtonItemDown.MouseDown, ButtonResizeFlag.MouseDown

        LabelFocus.Select()
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
                ColoredBrush = New SolidBrush(Color.Red)
            Else
                ColoredBrush = New SolidBrush(e.ForeColor)
            End If

            e.Graphics.DrawString(NowItem.ToString, e.Font, ColoredBrush, e.Bounds, StringFormat.GenericDefault)
        End If
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
        SU.LoadTabTipsData()
        ListView.Items.Clear()

        For Each Tip As TipItem In SU.Tabs.Item(SU.CurrTabIdx).Tips
            ListView.Items.Add(Tip)
        Next

        If Not IsAlreadyLoadTab Then
            For i = 0 To SU.Tabs.Count - 1
                AddTab(SU.Tabs.Item(i).Title)
            Next
        End If
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
    End Sub

    Private Sub MainForm_SaveList_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        ' SaveList()
    End Sub
#End Region

#Region "增删改移动"

    ''' <summary>
    ''' 增 ButtonAddItem ListPopupMenuAddItem
    ''' </summary>
    Private Sub ButtonAddItem_Click(sender As System.Object, e As System.EventArgs) Handles ButtonAddItem.Click, ListPopupMenuAddItem.Click
        Dim msg As String = InputBox("新的提醒标签：", "添加")
        If msg <> "" Then
            ListView.Items.Add(New TipItem(msg.Trim()))
            ListView.SelectedIndex = ListView.Items.Count() - 1
            SaveList()
        End If
    End Sub

    ''' <summary>
    ''' 删 ButtonRemoveItem ListPopupMenuRemoveItem
    ''' </summary>
    Private Sub ButtonRemoveItem_Click(sender As System.Object, e As System.EventArgs) Handles ButtonRemoveItem.Click, ListPopupMenuRemoveItem.Click
        Dim SelectItemIdics As New List(Of TipItem)(ListView.SelectedItems.Cast(Of TipItem)())
        If ListView.SelectedItem IsNot Nothing Then

            Dim ok As Integer
            If ListView.SelectedIndices.Count = 1 Then
                ok = MsgBox("确定删除以下提醒标签吗？" & Chr(10) & Chr(10) & CType(ListView.SelectedItem, TipItem).TipContent, MsgBoxStyle.OkCancel, "删除")
            Else
                Dim sb As New StringBuilder
                For Each item As TipItem In ListView.SelectedItems.Cast(Of TipItem)()
                    sb.AppendLine(item.TipContent)
                Next
                ok = MsgBox("确定删除以下所有提醒标签吗？" & Chr(10) & Chr(10) & sb.ToString, MsgBoxStyle.OkCancel, "删除")
            End If
            If (ok = vbOK) Then
                For Each item As TipItem In SelectItemIdics
                    ListView.Items.Remove(item)
                Next
                SaveList()
            End If
        End If
    End Sub

    ''' <summary>
    ''' 改 ListView.DoubleClick ListPopupMenuEditItem
    ''' </summary>
    Private Sub ListView_DoubleClick(sender As Object, e As System.EventArgs) Handles ListView.DoubleClick, ListPopupMenuEditItem.Click
        Dim tmpIdx As Integer = ListView.SelectedIndex
        If (ListView.SelectedItem IsNot Nothing) Then
            If ListView.SelectedIndices.Count = 1 Then

                Dim tip As TipItem = CType(ListView.SelectedItem, TipItem)
                Dim newstr As String = InputBox("修改提醒标签 """ & tip.TipContent & """ 为：", "修改", tip.TipContent)
                If newstr <> "" Then
                    tip.TipContent = newstr.Trim()
                    ListView.Items(tmpIdx) = tip
                    ListView.SelectedIndex = tmpIdx
                    SaveList()
                End If
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
                mouse_event(MouseEvent.MOUSEEVENTF_MOVE, 0, -10, 0, 0)
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
                mouse_event(MouseEvent.MOUSEEVENTF_MOVE, 0, 10, 0, 0)
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

        ButtonItemDown.Top = rect.Top
        ButtonItemDown.Left = rect.Left + rect.Width - ButtonItemDown.Width
        ButtonItemDown.Visible = True

        ButtonItemUp.Top = rect.Top
        ButtonItemUp.Left = rect.Left + rect.Width - 2 * ButtonItemUp.Width
        ButtonItemUp.Visible = True

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
        If ButtonItemUp.Visible = True Then
            If ListView.SelectedIndices.Count = 1 Then
                Dim Rect As Rectangle = ListView.GetItemRectangle(ListView.SelectedIndex)
                SetSelectedItemButtonShow(Rect)
            End If
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
    ''' 选择可用性
    ''' </summary>
    Private Sub SelCheck()
        Dim IsNotNull As Boolean = ListView.Items.Count <> 0 And ListView.SelectedIndices.Count <> 0
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

        ' 删改
        ButtonRemoveItem.Enabled = IsNotNull
        ListPopupMenuRemoveItem.Enabled = IsNotNull
        ListPopupMenuEditItem.Enabled = IsSingle
        ListPopupMenuHighLight.Enabled = IsNotNull

        If IsNotNull Then
            ListPopupMenuHighLight.Checked = CType(ListView.SelectedItem, TipItem).IsHighLight
        Else
            ListPopupMenuHighLight.Checked = False
        End If

        ' 移动
        ListPopupMenuMove.Enabled = IsNotNull
    End Sub

    ''' <summary>
    ''' 全选
    ''' </summary>
    Private Sub ListPopupMenuSelectAll_Click(sender As System.Object, e As System.EventArgs) Handles ListPopupMenuSelectAll.Click
        For i = 0 To ListView.Items.Count - 1
            ListView.SetSelected(i, True)
        Next
    End Sub

#End Region

#Region "调整大小 弹出菜单"

    ''' <summary>
    ''' 调整大小
    ''' </summary>
    Private Sub NumericUpDownListCnt_ValueChanged(sender As System.Object, e As System.EventArgs) Handles NumericUpDownListCnt.ValueChanged
        Dim MoveY As Integer
        If Me.Height < NumericUpDownListCnt.Value * ListView.ItemHeight + 27 Then
            MoveY = 10
        ElseIf Me.Height > NumericUpDownListCnt.Value * ListView.ItemHeight + 27 Then
            MoveY = -10
        End If
        Me.Height = NumericUpDownListCnt.Value * ListView.ItemHeight + 27
        mouse_event(MouseEvent.MOUSEEVENTF_MOVE, 0, MoveY, 0, 0)
    End Sub

    ''' <summary>
    ''' 显示弹出菜单
    ''' </summary>
    Private Sub ButtonChangeHeight_Click(sender As System.Object, e As System.EventArgs) Handles ButtonSetting.Click
        ListPopupMenu.Popup(Me.Left + sender.Left, Me.Top + sender.Top + sender.Height - 1)
    End Sub

    ''' <summary>
    ''' 右键 Setting 显示调整大小
    ''' </summary>
    Private Sub ButtonSetting_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles ButtonSetting.MouseUp
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

#Region "打开窗口"

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
        ShowForm("查看高亮 (共 " & idx & " 项)", sb.ToString, Color.Red)
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
    ''' 显示弹出菜单标题
    ''' </summary>
    Private Sub ListPopMenu_PopupOpen(sender As Object, e As DevComponents.DotNetBar.PopupOpenEventArgs) Handles ListPopupMenu.PopupOpen
        e.Cancel = True
        ListPopupMenuLabelSelItem.Visible = ListView.SelectedIndex <> -1
        ListPopupMenuLabelSelItemText.Visible = ListView.SelectedIndex <> -1
        ListPopupMenuLabelSelItem.Text = "当前选中 (共 " & ListView.SelectedIndices.Count & " 项)"
        If ListView.SelectedIndices.Count = 1 Then
            ListPopupMenuLabelSelItemText.Text = CType(ListView.SelectedItem, TipItem).TipContent
        ElseIf ListView.SelectedIndices.Count <> 0 Then
            Dim sb As New StringBuilder
            For Each item As TipItem In ListView.SelectedItems.Cast(Of TipItem)()
                sb.AppendLine(item.TipContent)
            Next
            ListPopupMenuLabelSelItemText.Text = sb.ToString
        End If
        e.Cancel = False
        ListPopupMenu.Refresh()
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

        Dim NewSuperTabItem = New DevComponents.DotNetBar.SuperTabItem()
        NewSuperTabItem.GlobalItem = False
        NewSuperTabItem.Name = "TabItemCustom_" & SU.Tabs.Count
        NewSuperTabItem.Text = Title

        AddHandler NewSuperTabItem.MouseDown, AddressOf TabStrip_MouseDown
        AddHandler NewSuperTabItem.DoubleClick, AddressOf ListPopupMenuRenameTab_Click

        Me.TabStrip.Tabs.AddRange(New DevComponents.DotNetBar.BaseItem() {NewSuperTabItem})
    End Sub

    ''' <summary>
    ''' 新建分组
    ''' </summary>
    Private Sub ListPopupMenuNewTab_Click(sender As System.Object, e As System.EventArgs) Handles TabPopupMenuNewTab.Click
        Dim tabName As String = InputBox("请输入新分组的标题: ", "新建", "分组")
        If tabName <> "" Then
            If Tab.CheckDuplicateTab(tabName.Trim(), SU.Tabs) Then
                MessageBox.Show("分组标题 """ & tabName & """ 已存在。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
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
            MessageBox.Show("无法删除最后的分组。", "删除", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
        Else
            Dim TabTitle As String = TabStrip.SelectedTab.Text
            Dim Tab As Tab = Tab.GetTabFromTitle(TabTitle)
            If Tab.Tips.Count <> 0 Then
                MessageBox.Show("分组内存在 " & Tab.Tips.Count & " 条记录无法删除，请先将记录移动到别的分组。", "删除", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                Dim ok As DialogResult = MessageBox.Show("是否删除分组 """ + TabStrip.SelectedTab.Text + """？", "删除", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
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
            If NewName.Trim() <> "" Then
                Tab.GetTabFromTitle(OldName).Title = NewName
                SU.SaveTabData()
                TabStrip.SelectedTab.Text = NewName
            End If
        End If
    End Sub

    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

    ''' <summary>
    ''' 右键点击Tab选中
    ''' </summary>
    Private Sub TabStrip_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles TabStrip.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Dim sel As DevComponents.DotNetBar.BaseItem = TabStrip.GetItemFromPoint(e.Location)
            If Not sel Is Nothing Then
                TabStrip.SelectedTab = sel
            End If
        End If
    End Sub

    ''' <summary>
    ''' Tab选择更改
    ''' </summary>
    Private Sub TabStrip_SelectedTabChanged(sender As Object, e As DevComponents.DotNetBar.SuperTabStripSelectedTabChangedEventArgs) Handles TabStrip.SelectedTabChanged
        SetSelectedItemButtonHide()
        If TabStrip.SelectedTabIndex <> -1 And SU.Tabs.Count <> 0 Then
            SU.CurrTabIdx = SU.Tabs.IndexOf(Tab.GetTabFromTitle(TabStrip.SelectedTab.Text))
            LoadList(True)
        End If
    End Sub

    ''' <summary>
    ''' Tab 顺序更改
    ''' </summary>
    Private Sub TabStrip_TabMoved(sender As Object, e As DevComponents.DotNetBar.SuperTabStripTabMovedEventArgs) Handles TabStrip.TabMoved
        Dim NewTabs As New List(Of Tab)
        For Each TabItem As DevComponents.DotNetBar.SuperTabItem In e.NewOrder
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
    Private Function GetUnActivityTabMoveButtonList(ByVal ClassNamePreFix As String) As List(Of DevComponents.DotNetBar.ButtonItem)
        Dim Tabs As New List(Of String)
        Dim TabBtns As New List(Of DevComponents.DotNetBar.ButtonItem)

        For Each Tab As Tab In SU.Tabs
            If Tab.Title <> SU.Tabs.Item(SU.CurrTabIdx).Title Then
                Tabs.Add(Tab.Title)
            End If
        Next

        For i = 0 To Tabs.Count - 1
            Dim Btn As New DevComponents.DotNetBar.ButtonItem With { _
                .Name = ClassNamePreFix & Tabs.Item(i), _
                .Tag = Tabs.Item(i), _
                .Text = Tabs.Item(i)
            }
            AddHandler Btn.Click, AddressOf MoveTab

            TabBtns.Add(Btn)
        Next
        Return TabBtns
    End Function

    ''' <summary>
    ''' 弹出列表右键菜单
    ''' </summary>
    Private Sub ListPopMenu_PopupOpen_Move(sender As Object, e As DevComponents.DotNetBar.PopupOpenEventArgs) Handles ListPopupMenu.PopupOpen
        Me.ListPopupMenuMove.SubItems.Clear()
        For Each Btn As DevComponents.DotNetBar.ButtonItem In GetUnActivityTabMoveButtonList("ListPopupMenuMove")
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
    Private Sub TabPopupMenu_PopupOpen(sender As Object, e As DevComponents.DotNetBar.PopupOpenEventArgs) Handles TabPopupMenu.PopupOpen
        Me.TabPopupMenuMove.SubItems.Clear()
        For Each Btn As DevComponents.DotNetBar.ButtonItem In GetUnActivityTabMoveButtonList("TabPopupMenuMove")
            Me.TabPopupMenuMove.SubItems.Add(Btn)
        Next
        Me.TabPopupMenuMove.Enabled = Not Me.TabPopupMenuMove.SubItems.Count = 0 And Not ListView.Items.Count = 0
        If Not Me.TabPopupMenuMove.Enabled Then
            Me.TabPopupMenuMove.SubItems.Clear()
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

        Dim ok As MsgBoxResult = MessageBox.Show(Flag, "移动至分组...", MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2)
        If ok = vbOK Then
            For Each Item As TipItem In SelectItems
                Tab.GetTabFromTitle(Dest).Tips.Add(Item)
                Tab.GetTabFromTitle(Src).Tips.Remove(Item)
            Next
            SU.SaveTabData()
        End If

        For Each TabItem As DevComponents.DotNetBar.SuperTabItem In TabStrip.Tabs
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
    End Sub

#End Region

End Class
