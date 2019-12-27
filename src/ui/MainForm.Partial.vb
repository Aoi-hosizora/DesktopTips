﻿Imports System.Text.RegularExpressions
Imports System.Threading
Imports Newtonsoft.Json
Imports QRCoder

Imports DD = DevComponents.DotNetBar

Partial Class MainForm

    '' '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '' ''''''''''''''''''''''''''''''''''''''''''''''''' 移动 大小 透明 '''''''''''''''''''''''''''''''''''''''''''''''''

#Region "鼠标拖动窗口"

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

#End Region

#Region "窗口大小调整"

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

#Region "Timer 显示关闭 移进移出"

    ''' <summary>
    ''' 透明度
    ''' </summary>
    Private MaxOpacity As Double = 0.6
    ''' <summary>
    ''' 透明速度
    ''' </summary>
    Private Const OpacitySpeed As Double = 0.08

    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''''''''''''''''''''''''''''''''''''''''''''''''''' Timer '''''''''''''''''''''''''''''''''''''''''''''''''''

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

    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '''''''''''''''''''''''''''''''''''''''''''''''''' 鼠标移动 ''''''''''''''''''''''''''''''''''''''''''''''''''

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

    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '''''''''''''''''''''''''''''''''''''''''''''''''' 鼠标移动 ''''''''''''''''''''''''''''''''''''''''''''''''''

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
    Private Sub PopMenu_PopupFinalizedAndClosed(sender As Object, e As System.EventArgs) Handles _
        ListPopupMenu.PopupFinalized, TabPopupMenu.PopupFinalized, ListPopupMenuMove.PopupFinalized, TabStrip.PopupClose

        IsMenuPopuping = False
        FormMouseLeave(sender, e)
    End Sub

#End Region

    '' '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '' ''''''''''''''''''''''''''''''''''''''''''''''''' 菜单 加载 折叠 '''''''''''''''''''''''''''''''''''''''''''''''''

#Region "透明度选项加载"

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

    ''' <summary>
    ''' 透明度点击
    ''' </summary>
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

#Region "折叠菜单"

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

    '' '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '' ''''''''''''''''''''''''''''''''''''''''''''''''' 焦点 选中 滚动 '''''''''''''''''''''''''''''''''''''''''''''''''

#Region "焦点 取消选择"

    ''' <summary>
    ''' 按钮焦点
    ''' </summary>
    Private Sub ButtonFocus_Handle(sender As Object, e As System.Windows.Forms.MouseEventArgs) _
        Handles ButtonAddItem.MouseDown, ButtonRemoveItem.MouseDown, ButtonCloseForm.MouseDown, ButtonListSetting.MouseDown, _
        ButtonItemUp.MouseDown, ButtonItemDown.MouseDown, ButtonResizeFlag.MouseDown

        LabelFocus.Select()
    End Sub

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

#End Region

#Region "滚动条"

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

    '' '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '' '''''''''''''''''''''''''''''''''''''''''''''''''''' 数据同步 ''''''''''''''''''''''''''''''''''''''''''''''''''''

#Region "数据同步"

    Private Shared QR_CODE_MAGIC As String = "DESKTOP_TIPS_ANDROID://"

    Private Shared ipRe As New Regex("^((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$")
    Private Shared portRe As New Regex("^([0-9]|[1-9]\d{1,3}|[1-5]\d{4}|6[0-4]\d{4}|65[0-4]\d{2}|655[0-2]\d|6553[0-5])$")

    ''' <summary>
    ''' 显示二维码窗口
    ''' </summary>
    Public Function GetQrCodeForm(ByVal Data As String) As EscCloseForm

        Dim qrGenerator As New QRCodeGenerator
        Dim qrCodeData As QRCodeData = qrGenerator.CreateQrCode(Data, QRCodeGenerator.ECCLevel.Q)
        Dim qrCode As New QRCode(qrCodeData)
        Dim qrCodeImg As Bitmap = qrCode.GetGraphic(7)

        Dim qrCodeForm As New EscCloseForm With {.Name = "qrCodeForm", .FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog, _
                                                 .MaximizeBox = False, .MinimizeBox = False, .ShowInTaskbar = False, _
                                                 .StartPosition = FormStartPosition.CenterScreen, .Size = qrCodeImg.Size}

        Dim pictureBox As New PictureBox With {.Name = "pictureBox", .SizeMode = PictureBoxSizeMode.Zoom, .Image = qrCodeImg}

        qrCodeForm.Controls.Add(pictureBox)
        pictureBox.Dock = DockStyle.Fill

        Return qrCodeForm

    End Function

    ''' <summary>
    ''' 同步到移动端 (本地 C -> 安卓 S) !!! 常用
    ''' 远程监听地址 -> 确定远程地址 -> 本地发送数据 -> 等待 ACK
    ''' </summary>
    Private Sub ListPopupMenuSyncDataTo_Click(sender As System.Object, e As System.EventArgs) Handles ListPopupMenuSyncDataTo.Click
        Dim setting As SettingUtil.AppSetting = SettingUtil.LoadAppSettings()

        Dim input As String = InputBox("请输入移动端的地址：", "同步到移动端", setting.LastMobileIP)
        input = input.Replace("：", ":").Trim()

        If String.IsNullOrWhiteSpace(input) Then Return ' 空内容
        Dim sp As String() = input.Split(New Char() {":"})

        Dim ip As String, port As Integer
        While sp.Length <> 2 OrElse Not ipRe.IsMatch(sp(0)) OrElse Not portRe.IsMatch(sp(1)) OrElse Not Integer.TryParse(sp(1), port)
            MessageBox.Show("所输入的地址格式不正确。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error)

            input = InputBox("请输入移动端的地址：", "同步到移动端", input).Replace("：", ":").Trim()

            If String.IsNullOrWhiteSpace(input) Then Return ' 空内容
            sp = input.Replace("：", ":").Trim().Split(New Char() {":"})
        End While

        setting.LastMobileIP = input
        SettingUtil.SaveAppSettings(setting)

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

        Dim setting As SettingUtil.AppSetting = SettingUtil.LoadAppSettings()
        Dim input As String = InputBox("请输入本地监听的端口：", "从移动端同步", setting.LastLocalPort)

        Dim port As Integer
        If String.IsNullOrWhiteSpace(input) Then Return ' 空内容
        While Not Integer.TryParse(input, port) OrElse Not (port >= 1 And port <= 65535) ' 端口错误
            MessageBox.Show("所输入的端口格式不正确，应为在 [1, 65535] 内的纯数字。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error)

            input = InputBox("请输入本地监听的端口：", "从移动端同步", input)
            If String.IsNullOrWhiteSpace(input) Then Return ' 空内容
        End While

        setting.LastLocalPort = input.Trim()
        SettingUtil.SaveAppSettings(setting)

        ' ---------------------------------------------
        ' 获得端口 << 监听

        Dim thread As Thread = Nothing
        Dim cancelFlag As Boolean = False

        Dim qrCodeForm As EscCloseForm = GetQrCodeForm(QR_CODE_MAGIC & ip & ":" & port)

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
                                                                               Me, New String() {"打开文件夹", "确定"})
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