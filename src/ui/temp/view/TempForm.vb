Imports DD = DevComponents.DotNetBar

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

#End Region

End Class