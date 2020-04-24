Imports System.Text.RegularExpressions
Imports System.Threading
Imports Newtonsoft.Json
Imports QRCoder
Imports DD = DevComponents.DotNetBar

Partial Public Class TempForm

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

            Me.ListPopupMenuItemContainer.SubItems.AddRange(
                New DD.BaseItem() {Me.ListPopupMenuMoveUp, Me.ListPopupMenuMoveDown, Me.ListPopupMenuAddItem, Me.ListPopupMenuRemoveItem,
                                   Me.ListPopupMenuEditItem, Me.ListPopupMenuCopy, Me.ListPopupMenuSelectAll, Me.ListPopupMenuHighLight})

            Me.ListPopupMenu.SubItems.AddRange(
                New DD.BaseItem() {Me.ListPopupMenuLabelSelItem, Me.ListPopupMenuLabelSelItemText, Me.ListPopupMenuLabelItemList,
                                   Me.ListPopupMenuItemContainer,
                                   Me.ListPopupMenuMoveTop, Me.ListPopupMenuMoveBottom, Me.ListPopupMenuViewHighLight, Me.ListPopupMenuFind, Me.ListPopupMenuMove, Me.ListPopupMenuOthers,
                                   Me.ListPopupMenuLabelItemFile, Me.ListPopupMenuOpenDir, Me.ListPopupMenuViewFile, Me.ListPopupMenuBrowser, Me.ListPopupMenuSyncData,
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
            Me.ListPopupMenu.SubItems.AddRange(
                New DD.BaseItem() {Me.ListPopupMenuLabelSelItem, Me.ListPopupMenuLabelSelItemText, Me.ListPopupMenuLabelItemList,
                                   Me.ListPopupMenuAddItem, Me.ListPopupMenuRemoveItem, Me.ListPopupMenuEditItem,
                                   Me.ListPopupMenuMoveUp, Me.ListPopupMenuMoveDown, Me.ListPopupMenuMoveTop, Me.ListPopupMenuMoveBottom,
                                   Me.ListPopupMenuCopy, Me.ListPopupMenuSelectAll, Me.ListPopupMenuHighLight, Me.ListPopupMenuViewHighLight,
                                   Me.ListPopupMenuFind, Me.ListPopupMenuMove, Me.ListPopupMenuOthers,
                                   Me.ListPopupMenuLabelItemFile, Me.ListPopupMenuOpenDir, Me.ListPopupMenuViewFile, Me.ListPopupMenuBrowser, Me.ListPopupMenuSyncData,
                                   Me.ListPopupMenuLabelItemWindow, Me.ListPopupMenuWinSetting, Me.ListPopupMenuExit})
        End If
    End Sub

#End Region

#Region "数据同步"

    Private Shared QR_CODE_MAGIC As String = "DESKTOP_TIPS_ANDROID://"

    Private Shared IP_RE As New Regex("^((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$")
    Private Shared PORT_RE As New Regex("^([0-9]|[1-9]\d{1,3}|[1-5]\d{4}|6[0-4]\d{4}|65[0-4]\d{2}|655[0-2]\d|6553[0-5])$")

    ''' <summary>
    ''' 显示二维码窗口
    ''' </summary>
    Public Function GetQrCodeForm(ByVal Data As String) As BaseEscCloseForm
        Dim qrGenerator As New QRCodeGenerator
        Dim qrCodeData As QRCodeData = qrGenerator.CreateQrCode(Data, QRCodeGenerator.ECCLevel.Q)
        Dim qrCode As New QRCode(qrCodeData)
        Dim qrCodeImg As Bitmap = qrCode.GetGraphic(7)

        Dim qrCodeForm As New BaseEscCloseForm With {
            .Name = "qrCodeForm", .FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog,
            .MaximizeBox = False, .MinimizeBox = False, .ShowInTaskbar = False,
            .StartPosition = FormStartPosition.CenterScreen, .Size = qrCodeImg.Size
        }
        Dim pictureBox As New PictureBox With {
            .Name = "pictureBox", .SizeMode = PictureBoxSizeMode.Zoom, .Image = qrCodeImg, .Dock = DockStyle.Fill
        }
        qrCodeForm.Controls.Add(pictureBox)
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
        While sp.Length <> 2 OrElse Not IP_RE.IsMatch(sp(0)) OrElse Not PORT_RE.IsMatch(sp(1)) OrElse Not Integer.TryParse(sp(1), port)
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

        Dim qrCodeForm As BaseEscCloseForm = GetQrCodeForm(QR_CODE_MAGIC & ip & ":" & port)

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

                                Dim backupFileName As String = GlobalModel.SaveBackupData(newList)
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
                                '        backupFileName = GlobalModel.SaveBackupData(GlobalModel.Tabs) ' 备份
                                '        GlobalModel.Tabs.AddRange(newItems)
                                '        'Case Windows.Forms.DialogResult.No ' 删除
                                '        '    backupFileName = GlobalModel.SaveBackupData(GlobalModel.Tabs)
                                '        '    GlobalModel.Tabs.RemoveAll(New Predicate(Of Tab)(Function(item As Tab) deletedItems.IndexOf(item) <> -1))
                                '    Case Windows.Forms.DialogResult.No ' 覆盖
                                '        backupFileName = GlobalModel.SaveBackupData(GlobalModel.Tabs)
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
