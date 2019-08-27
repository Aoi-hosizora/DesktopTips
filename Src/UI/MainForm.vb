Imports System.IO
Imports System.Text

Public Class MainForm

    Declare Sub mouse_event Lib "user32" (ByVal dwFlags As Integer, ByVal dx As Integer, ByVal dy As Integer, ByVal cButtons As Integer, ByVal dwExtraInfo As Integer)
    Private Declare Function SetCursorPos Lib "user32" (ByVal x As Long, ByVal y As Long) As Long
    ' Private Declare Sub mouse_event Lib "user32" (ByVal dwFlags As Long, ByVal dx As Long, ByVal dy As Long, ByVal cButtons As Long, ByVal dwExtraInfo As Long)

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
    Private IsChangeSize As Boolean

    Private Sub ListView_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles ListView.MouseDown, TabStrip.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Left Then
            IsMouseDown = True
        End If
        PushDownMouseInScreen = Cursor.Position
        PushDownWindowPos = New Point(Me.Left, Me.Top)
        PushDownWindowSize = New Point(Me.Width, Me.Height)
    End Sub

    Private Sub ListView_MouseMove(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles ListView.MouseMove, TabStrip.MouseMove
        Dim rr As Integer = If(ListView.Items.Count > NumericUpDownListCnt.Value, 25, 8)

        If sender.Equals(ListView) Then
            If e.X > sender.Width - rr Or IsChangeSize = True Then
                Me.Cursor = Cursors.SizeWE
                If IsMouseDown Then
                    IsChangeSize = True
                    Me.Width = PushDownWindowSize.X + Cursor.Position.X - PushDownMouseInScreen.X
                End If
                Return
            End If
        End If

        Me.Cursor = Cursors.Default
        If IsMouseDown And Not IsChangeSize Then
            Me.Top = PushDownWindowPos.Y + Cursor.Position.Y - PushDownMouseInScreen.Y
            Me.Left = PushDownWindowPos.X + Cursor.Position.X - PushDownMouseInScreen.X
        End If
    End Sub

    Private Sub ListView_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles ListView.MouseUp, TabStrip.MouseUp
        IsMouseDown = False
        IsChangeSize = False
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

        NumericUpDownListCnt.Value = (Me.Height - 27) \ 17
        ButtonRemoveItem.Enabled = False

        Me.Top = Me.Top - (MaxOpacity / OpacitySpeed)
        TimerShowForm.Enabled = True
        TimerShowForm.Start()

        ListPopupMenuWinTop.Checked = Me.TopMost

        Me.TabStrip.Tabs.Remove(Me.TabItemTest)

        ListView.Items.Clear()
        SetupMouseEnterLeave()
        SetupUpDownButtonsPos()
        LoadList()
        FormOpacity_Load()
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

#Region "Opacity"

    Dim ops() As Double = {0.2, 0.4, 0.6, 0.7, 0.8, 1.0}
    Dim opBtns(ops.Length - 1) As DevComponents.DotNetBar.ButtonItem

    ''' <summary>
    ''' 动态添加透明度
    ''' </summary>
    Private Sub FormOpacity_Load()
        For i = 0 To ops.Length - 1
            opBtns(i) = New DevComponents.DotNetBar.ButtonItem With { _
                .Name = "PopMenuButtonOpacity" & CInt(ops(i) * 100), _
                .Tag = ops(i), _
                .Text = CInt(ops(i) * 100) & "%"
            }

            AddHandler opBtns(i).Click, AddressOf PopMenuButtonOpacity_Click
            Me.ListPopupMenuOpacity.SubItems.Add(opBtns(i))

            If MaxOpacity = opBtns(i).Tag Then
                opBtns(i).Checked = True
            End If
        Next
    End Sub

    Private Sub PopMenuButtonOpacity_Click(sender As System.Object, e As System.EventArgs)

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

        If ListView.SelectedIndex <> -1 Then
            Dim rect As Rectangle = ListView.GetItemRectangle(ListView.SelectedIndex)
            If e.Y > rect.Top + rect.Height Then
                ListView.ClearSelected()
            End If
        End If

    End Sub

    ''' <summary>
    ''' 右键列表同时选中项
    ''' </summary>
    Private Sub ListView_RightMouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles ListView.MouseUp

        Dim idx As Integer = ListView.IndexFromPoint(e.X, e.Y)
        If e.Button = Windows.Forms.MouseButtons.Right And idx <> -1 And idx <> 65535 Then
            If ListView.SelectedIndices.Count = 1 Then
                ListView.ClearSelected()
                ListView.SetSelected(idx, True)
            End If
        End If
    End Sub

    ''' <summary>
    ''' 按钮焦点
    ''' </summary>
    Private Sub ButtonFocus_Handle(sender As Object, e As System.Windows.Forms.MouseEventArgs) _
        Handles ButtonAddItem.MouseDown, ButtonRemoveItem.MouseDown, ButtonCloseForm.MouseDown, ButtonSetting.MouseDown, _
        ButtonItemUp.MouseDown, ButtonItemDown.MouseDown

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

    Private Sub MainForm_SaveList_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        SaveList(True)
    End Sub

    ''' <summary>
    ''' MyBase.Load
    ''' </summary>
    ''' <param name="IsAlreadyLoadTab">是否已经初始化分组 (True: 不需要更新 分组 和 Curr)</param>
    Private Sub LoadList(Optional ByVal IsAlreadyLoadTab = False)
        StorageUtil.LoadTabTipsData(Not IsAlreadyLoadTab)
        ListView.Items.Clear()

        For Each Tip As TipItem In TabTips.GetTipsFromTab(StorageUtil.CurrentTab.TabTitle, StorageUtil.StorageTipItems)
            ListView.Items.Add(Tip)
        Next

        If Not IsAlreadyLoadTab Then
            For i = 0 To StorageUtil.StorageTabs.Count - 1
                AddTab(StorageUtil.StorageTabs.Item(i).TabTitle)
            Next
        End If
    End Sub

    ''' <summary>
    ''' 保存数据
    ''' </summary>
    ''' <param name="IsSaveAll">是否保存所有分组</param>
    Private Sub SaveList(Optional ByVal IsSaveAll As Boolean = False)
        Dim tabIdx As Integer = Tab.GetTabIndexFromTabTitle(StorageUtil.CurrentTab.TabTitle, StorageUtil.StorageTabs)
        Dim Tips As New List(Of TipItem)
        For Each Tip As TipItem In ListView.Items.Cast(Of TipItem)()
            Tips.Add(Tip)
        Next

        StorageUtil.StorageTipItems.Item(TabTips.GetIndexFromTab(StorageUtil.CurrentTab.TabTitle, StorageUtil.StorageTipItems)) _
            = New TabTips(StorageUtil.CurrentTab, Tips)

        If IsSaveAll Then
            StorageUtil.SaveTabData()
        Else
            StorageUtil.SaveTabData(StorageUtil.CurrentTab)
        End If
    End Sub

#End Region

#Region "增删改移动"

    ''' <summary>
    ''' 增 ButtonAddItem ListPopupMenuAddItem
    ''' </summary>
    Private Sub ButtonAddItem_Click(sender As System.Object, e As System.EventArgs) Handles ButtonAddItem.Click, ListPopupMenuAddItem.Click
        Dim msg As String = InputBox("新的提醒标签：", "添加")
        If msg <> "" Then
            ListView.Items.Add(New TipItem(msg.Trim(), StorageUtil.CurrentTab))
            ListView.SelectedIndex = ListView.Items.Count() - 1
            SaveList()
        End If
    End Sub

    ''' <summary>
    ''' 删 ButtonRemoveItem ListPopupMenuRemoveItem
    ''' </summary>
    Private Sub ButtonRemoveItem_Click(sender As System.Object, e As System.EventArgs) Handles ButtonRemoveItem.Click, ListPopupMenuRemoveItem.Click
        Dim SelectItemIdics As New List(Of TipItem)
        If ListView.SelectedItem IsNot Nothing Then
            SelectItemIdics.Clear()
            For Each idx As TipItem In ListView.SelectedItems
                SelectItemIdics.Add(idx)
            Next

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
        Dim tmpIdx As Integer
        If (ListView.SelectedItem IsNot Nothing) Then
            If ListView.SelectedIndices.Count = 1 Then
                tmpIdx = ListView.SelectedIndex

                Dim tip As TipItem = CType(ListView.SelectedItem, TipItem)
                Dim newstr As String = InputBox("修改提醒标签 """ & tip.TipContent & """ 为：", "修改", tip.TipContent)
                If newstr <> "" Then
                    tip.TipContent = newstr.Trim()
                    ListView.Items(tmpIdx) = tip
                    ListView.SelectedIndex = tmpIdx
                    SaveList()
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' 置顶
    ''' </summary>
    Private Sub PopMenuButtonMoveTop_Click(sender As System.Object, e As System.EventArgs) Handles ListPopupMenuMoveTop.Click
        Dim currItem As Object = ListView.SelectedItem
        ListView.Items.Remove(currItem)
        ListView.Items.Insert(0, currItem)
        ListView.SetSelected(0, True)

        SaveList()
    End Sub

    ''' <summary>
    ''' 置底
    ''' </summary>
    Private Sub PopMenuButtonMoveBottom_Click(sender As System.Object, e As System.EventArgs) Handles ListPopupMenuMoveBottom.Click
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

#End Region

#Region "选择"

    ''' <summary>
    ''' 高亮判断 辅助按钮显示
    ''' </summary>
    Private Sub ListView_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles ListView.SelectedIndexChanged

        ButtonRemoveItem.Enabled = ListView.SelectedItem IsNot Nothing

        If ListView.SelectedIndex <> -1 Then
            If CType(ListView.SelectedItem, TipItem).IsHighLight Then
                ListPopupMenuHighLight.Checked = True
            Else
                ListPopupMenuHighLight.Checked = False
            End If
        End If

        If ListView.SelectedIndices.Count = 1 Then
            SetSelectedItemButtonShow(ListView.GetItemRectangle(ListView.SelectedIndex))
            ListPopupMenuEditItem.Enabled = True
        Else
            SetSelectedItemButtonHide()
            ListPopupMenuEditItem.Enabled = False
        End If

        ButtonItemUp.Enabled = Not ListView.SelectedIndex = 0
        ListPopupMenuMoveUp.Enabled = Not ListView.SelectedIndex = 0
        ListPopupMenuMoveTop.Enabled = Not ListView.SelectedIndex = 0
        ListPopupMenuMoveBottom.Enabled = Not ListView.SelectedIndex = ListView.Items.Count() - 1

        ButtonItemDown.Enabled = Not ListView.SelectedIndex = ListView.Items.Count() - 1
        ListPopupMenuMoveDown.Enabled = Not ListView.SelectedIndex = ListView.Items.Count() - 1

        ListView.Refresh()
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
            PopMenuButtonListHeight_Click(sender, New System.EventArgs)
        End If
    End Sub

    ''' <summary>
    ''' Popup 显示调整大小
    ''' </summary>
    Private Sub PopMenuButtonListHeight_Click(sender As System.Object, e As System.EventArgs) Handles ListPopupMenuListHeight.Click
        NumericUpDownListCnt.Visible = ListPopupMenuListHeight.Checked
    End Sub

#End Region

#Region "打开窗口"

    ''' <summary>
    ''' 打开文件所在位置
    ''' </summary>
    Private Sub PopMenuButtonOpenFile_Click(sender As System.Object, e As System.EventArgs) Handles ListPopupMenuOpenDir.Click
        'Dim path As String = FileDir
        'Process.Start(path)
        'C:\Users\Windows 10\AppData\Roaming\DesktopTips

        ' System.Diagnostics.Process.Start("explorer.exe", "/select,""" & StorageUtil.StorageFileName & """")
        System.Diagnostics.Process.Start("explorer.exe", """" & StorageUtil.StorageFileDir & """")
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
    Private Sub PopMenuButtonViewFile_Click(sender As System.Object, e As System.EventArgs) Handles ListPopupMenuViewFile.Click
        'If File.Exists(StorageUtil.StorageFileName) Then
        '    Dim reader As TextReader = File.OpenText(StorageUtil.StorageFileName)
        '    Dim Count As Integer = CInt(reader.ReadLine)
        '    Dim Content As String = reader.ReadToEnd()
        '    reader.Close()

        '    ' 清除 ,,,
        '    Content = Content.Replace(HighLightedSign, "")
        '    Content = Content.Replace(UnHighLightSign, "")
        '    ShowForm("浏览文件 (共 " & Count & " 项)", Content, Color.Black)
        'End If
        Dim sb As New StringBuilder
        For Each Item As TipItem In ListView.Items.Cast(Of TipItem)()
            sb.AppendLine(Item.TipContent & If(Item.IsHighLight, " [高亮]", ""))
        Next
        ShowForm("浏览文件 (共 " & ListView.Items.Count & " 项)", sb.ToString(), Color.Black)
    End Sub

    ''' <summary>
    ''' 显示高亮部分
    ''' </summary>
    Private Sub PopMenuButtonHighLightList_Click(sender As System.Object, e As System.EventArgs) Handles ListPopupMenuViewHighLight.Click
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
    Private Sub PopMenuButtonWinTop_Click(sender As System.Object, e As System.EventArgs) Handles ListPopupMenuWinTop.Click
        sender.checked = Not sender.checked
        Me.TopMost = sender.checked
    End Sub

    ''' <summary>
    ''' 高亮
    ''' </summary>
    Private Sub PopMenuButtonHighLight_Click(sender As System.Object, e As System.EventArgs) Handles ListPopupMenuHighLight.Click
        CType(ListView.SelectedItem, TipItem).IsHighLight = Not CType(ListView.SelectedItem, TipItem).IsHighLight
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
        Dim ClassName As String = "TabItemCustom_" + StorageUtil.StorageTabs.Count.ToString()

        If IsAddToStorage Then
            Dim NewTab As New Tab(Title, ClassName)
            StorageUtil.StorageTabs.Add(NewTab)
            StorageUtil.StorageTipItems.Add(New TabTips(NewTab, New List(Of TipItem)))
            StorageUtil.SaveTabData(NewTab)
        End If

        Dim NewSuperTabItem = New DevComponents.DotNetBar.SuperTabItem()
        NewSuperTabItem.GlobalItem = False
        NewSuperTabItem.Name = ClassName
        NewSuperTabItem.Text = Title
        AddHandler NewSuperTabItem.MouseDown, AddressOf TabStrip_MouseDown

        Me.TabStrip.Tabs.AddRange(New DevComponents.DotNetBar.BaseItem() {NewSuperTabItem})
    End Sub

    ''' <summary>
    ''' 新建分组
    ''' </summary>
    Private Sub PopMenuButtonNewTab_Click(sender As System.Object, e As System.EventArgs) Handles PopMenuButtonNewTab.Click
        Dim tabName As String = InputBox("请输入新分组的标题: ", "新建", "分组")
        If tabName <> "" Then
            If Tab.CheckDuplicateTab(tabName.Trim(), StorageUtil.StorageTabs) Then
                MessageBox.Show("分组标题 """ & tabName & """ 已存在。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
                PopMenuButtonNewTab_Click(sender, New System.EventArgs)
            Else
                AddTab(tabName, True)
            End If
        End If
    End Sub

    ''' <summary>
    ''' 删除分组
    ''' </summary>
    Private Sub PopMenuButtonDeleteTab_Click(sender As System.Object, e As System.EventArgs) Handles PopMenuButtonDeleteTab.Click
        If TabStrip.Tabs.Count = 1 Then
            MessageBox.Show("无法删除最后的分组。", "删除", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
        Else
            Dim ok As DialogResult = MessageBox.Show("是否删除分组 """ + TabStrip.SelectedTab.Text + """？", "删除", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
            If ok = vbOK Then
                StorageUtil.DeleteTabData(TabStrip.SelectedTab.Text)
                TabStrip.Tabs.RemoveAt(TabStrip.SelectedTabIndex)
            End If
        End If
    End Sub

    ''' <summary>
    ''' 右键点击Tab选中
    ''' </summary>
    Private Sub TabStrip_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles TabStrip.MouseDown, TabItemTest.MouseDown
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
        If TabStrip.SelectedTabIndex <> -1 And StorageUtil.StorageTabs.Count <> 0 Then
            StorageUtil.CurrentTab = StorageUtil.StorageTabs.Item(Tab.GetTabIndexFromTabTitle(TabStrip.SelectedTab.Text, StorageUtil.StorageTabs))
            LoadList(True)
        End If
    End Sub

    ''' <summary>
    ''' Tab 顺序更改
    ''' </summary>
    Private Sub TabStrip_TabMoved(sender As Object, e As DevComponents.DotNetBar.SuperTabStripTabMovedEventArgs) Handles TabStrip.TabMoved
        StorageUtil.StorageTabs.Clear()
        For Each TabItems As DevComponents.DotNetBar.SuperTabItem In e.NewOrder
            StorageUtil.StorageTabs.Add(New Tab(TabItems.Text, TabItems.Name))
        Next
        StorageUtil.SaveTabOrder()
    End Sub

#End Region

End Class
