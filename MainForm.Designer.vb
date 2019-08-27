<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainForm
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Protected Overrides ReadOnly Property CreateParams As System.Windows.Forms.CreateParams
        Get
            Const WS_EX_APPWINDOW As Integer = 16384
            Const WS_EX_TOOLWINDOW As Integer = 128
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ExStyle = cp.ExStyle And (Not WS_EX_APPWINDOW) '不显示在TaskBar
            cp.ExStyle = cp.ExStyle Or WS_EX_TOOLWINDOW '不显示在Alt-Tab
            Return cp
        End Get
    End Property

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        Me.ButtonRemoveItem = New DevComponents.DotNetBar.ButtonX()
        Me.ButtonAddItem = New DevComponents.DotNetBar.ButtonX()
        Me.StyleManager1 = New DevComponents.DotNetBar.StyleManager(Me.components)
        Me.LabelFocus = New System.Windows.Forms.Label()
        Me.ListView = New System.Windows.Forms.ListBox()
        Me.ButtonCloseForm = New DevComponents.DotNetBar.ButtonX()
        Me.ButtonChangeHeight = New DevComponents.DotNetBar.ButtonX()
        Me.NumericUpDownListCnt = New System.Windows.Forms.NumericUpDown()
        Me.TimerShowForm = New System.Windows.Forms.Timer(Me.components)
        Me.TimerEndForm = New System.Windows.Forms.Timer(Me.components)
        Me.TimerMouseIn = New System.Windows.Forms.Timer(Me.components)
        Me.TimerMouseOut = New System.Windows.Forms.Timer(Me.components)
        Me.SuperTooltip = New DevComponents.DotNetBar.SuperTooltip()
        Me.ContextMenuBar1 = New DevComponents.DotNetBar.ContextMenuBar()
        Me.ListPopupMenu = New DevComponents.DotNetBar.ButtonItem()
        Me.ListPopupMenuLabelSelItem = New DevComponents.DotNetBar.LabelItem()
        Me.ListPopupMenuLabelSelItemText = New DevComponents.DotNetBar.LabelItem()
        Me.ListPopupMenuLabelItemList = New DevComponents.DotNetBar.LabelItem()
        Me.ListPopupMenuMoveUp = New DevComponents.DotNetBar.ButtonItem()
        Me.ListPopupMenuMoveDown = New DevComponents.DotNetBar.ButtonItem()
        Me.ListPopupMenuMoveTop = New DevComponents.DotNetBar.ButtonItem()
        Me.ListPopupMenuMoveBottom = New DevComponents.DotNetBar.ButtonItem()
        Me.ListPopupMenuHighLight = New DevComponents.DotNetBar.ButtonItem()
        Me.ListPopupMenuHighLightList = New DevComponents.DotNetBar.ButtonItem()
        Me.ListPopupMenuAddItem = New DevComponents.DotNetBar.ButtonItem()
        Me.ListPopupMenuRemoveItem = New DevComponents.DotNetBar.ButtonItem()
        Me.ListPopupMenuEditItem = New DevComponents.DotNetBar.ButtonItem()
        Me.ListPopupMenuLabelItemFile = New DevComponents.DotNetBar.LabelItem()
        Me.ListPopupMenuOpenFile = New DevComponents.DotNetBar.ButtonItem()
        Me.ListPopupMenuViewFile = New DevComponents.DotNetBar.ButtonItem()
        Me.ListPopupMenuLabelItemWindow = New DevComponents.DotNetBar.LabelItem()
        Me.ListPopupMenuListHeight = New DevComponents.DotNetBar.ButtonItem()
        Me.ListPopupMenuOpacity = New DevComponents.DotNetBar.ButtonItem()
        Me.ListPopupMenuWinTop = New DevComponents.DotNetBar.ButtonItem()
        Me.ListPopupMenuExit = New DevComponents.DotNetBar.ButtonItem()
        Me.TabPopupMenu = New DevComponents.DotNetBar.ButtonItem()
        Me.PopMenuButtonNewTab = New DevComponents.DotNetBar.ButtonItem()
        Me.PopMenuButtonDeleteTab = New DevComponents.DotNetBar.ButtonItem()
        Me.TabStrip = New DevComponents.DotNetBar.SuperTabStrip()
        Me.SuperTabItemDefault = New DevComponents.DotNetBar.SuperTabItem()
        Me.ButtonItemUp = New DevComponents.DotNetBar.ButtonX()
        Me.ButtonItemDown = New DevComponents.DotNetBar.ButtonX()
        CType(Me.NumericUpDownListCnt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ContextMenuBar1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TabStrip, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ButtonRemoveItem
        '
        Me.ButtonRemoveItem.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.ButtonRemoveItem.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonRemoveItem.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.ButtonRemoveItem.Location = New System.Drawing.Point(268, 275)
        Me.ButtonRemoveItem.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ButtonRemoveItem.Name = "ButtonRemoveItem"
        Me.ButtonRemoveItem.Shape = New DevComponents.DotNetBar.RoundRectangleShapeDescriptor()
        Me.ButtonRemoveItem.Size = New System.Drawing.Size(24, 23)
        Me.ButtonRemoveItem.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.ButtonRemoveItem.TabIndex = 1
        Me.ButtonRemoveItem.Text = "－"
        '
        'ButtonAddItem
        '
        Me.ButtonAddItem.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.ButtonAddItem.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonAddItem.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.ButtonAddItem.Location = New System.Drawing.Point(245, 275)
        Me.ButtonAddItem.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ButtonAddItem.Name = "ButtonAddItem"
        Me.ButtonAddItem.Shape = New DevComponents.DotNetBar.RoundRectangleShapeDescriptor()
        Me.ButtonAddItem.Size = New System.Drawing.Size(24, 23)
        Me.ButtonAddItem.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.ButtonAddItem.TabIndex = 1
        Me.ButtonAddItem.Text = "＋"
        '
        'StyleManager1
        '
        Me.StyleManager1.ManagerColorTint = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.StyleManager1.ManagerStyle = DevComponents.DotNetBar.eStyle.Office2007Blue
        Me.StyleManager1.MetroColorParameters = New DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.White, System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(87, Byte), Integer), CType(CType(154, Byte), Integer)))
        '
        'LabelFocus
        '
        Me.LabelFocus.AutoSize = True
        Me.LabelFocus.Location = New System.Drawing.Point(92, 30)
        Me.LabelFocus.Name = "LabelFocus"
        Me.LabelFocus.Size = New System.Drawing.Size(48, 17)
        Me.LabelFocus.TabIndex = 2
        Me.LabelFocus.Text = "FOCUS"
        Me.LabelFocus.Visible = False
        '
        'ListView
        '
        Me.ListView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ContextMenuBar1.SetContextMenuEx(Me.ListView, Me.ListPopupMenu)
        Me.ListView.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.ListView.FormattingEnabled = True
        Me.ListView.ItemHeight = 17
        Me.ListView.Items.AddRange(New Object() {"Test", "Test", "Test", "Test", "Test", "Test", "Test", "Test", "Test", "Test", "Test", "Test"})
        Me.ListView.Location = New System.Drawing.Point(28, 0)
        Me.ListView.Name = "ListView"
        Me.ListView.Size = New System.Drawing.Size(264, 276)
        Me.ListView.TabIndex = 0
        '
        'ButtonCloseForm
        '
        Me.ButtonCloseForm.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.ButtonCloseForm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ButtonCloseForm.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.ButtonCloseForm.Location = New System.Drawing.Point(0, 275)
        Me.ButtonCloseForm.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ButtonCloseForm.Name = "ButtonCloseForm"
        Me.ButtonCloseForm.Shape = New DevComponents.DotNetBar.RoundRectangleShapeDescriptor()
        Me.ButtonCloseForm.Size = New System.Drawing.Size(24, 23)
        Me.ButtonCloseForm.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.SuperTooltip.SetSuperTooltip(Me.ButtonCloseForm, New DevComponents.DotNetBar.SuperTooltipInfo("退出", "", "退出程序，保存数据。", Nothing, Nothing, DevComponents.DotNetBar.eTooltipColor.Gray, True, True, New System.Drawing.Size(180, 68)))
        Me.ButtonCloseForm.TabIndex = 1
        Me.ButtonCloseForm.Text = "×"
        '
        'ButtonChangeHeight
        '
        Me.ButtonChangeHeight.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.ButtonChangeHeight.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ButtonChangeHeight.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.ButtonChangeHeight.Location = New System.Drawing.Point(23, 275)
        Me.ButtonChangeHeight.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ButtonChangeHeight.Name = "ButtonChangeHeight"
        Me.ButtonChangeHeight.Shape = New DevComponents.DotNetBar.RoundRectangleShapeDescriptor()
        Me.ButtonChangeHeight.Size = New System.Drawing.Size(24, 23)
        Me.ButtonChangeHeight.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.SuperTooltip.SetSuperTooltip(Me.ButtonChangeHeight, New DevComponents.DotNetBar.SuperTooltipInfo("设置", "", "左键单击显示弹出菜单，右键点击显示设置列表高度。", Nothing, Nothing, DevComponents.DotNetBar.eTooltipColor.Gray, True, True, New System.Drawing.Size(180, 68)))
        Me.ButtonChangeHeight.TabIndex = 1
        Me.ButtonChangeHeight.Text = "≡"
        '
        'NumericUpDownListCnt
        '
        Me.NumericUpDownListCnt.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.NumericUpDownListCnt.Location = New System.Drawing.Point(46, 275)
        Me.NumericUpDownListCnt.Minimum = New Decimal(New Integer() {5, 0, 0, 0})
        Me.NumericUpDownListCnt.Name = "NumericUpDownListCnt"
        Me.NumericUpDownListCnt.Size = New System.Drawing.Size(35, 23)
        Me.NumericUpDownListCnt.TabIndex = 3
        Me.NumericUpDownListCnt.Value = New Decimal(New Integer() {8, 0, 0, 0})
        Me.NumericUpDownListCnt.Visible = False
        '
        'TimerShowForm
        '
        Me.TimerShowForm.Interval = 1
        '
        'TimerEndForm
        '
        Me.TimerEndForm.Interval = 1
        '
        'TimerMouseIn
        '
        Me.TimerMouseIn.Interval = 10
        '
        'TimerMouseOut
        '
        Me.TimerMouseOut.Interval = 10
        '
        'SuperTooltip
        '
        Me.SuperTooltip.DefaultFont = New System.Drawing.Font("Microsoft YaHei UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        '
        'ContextMenuBar1
        '
        Me.ContextMenuBar1.AntiAlias = True
        Me.ContextMenuBar1.Font = New System.Drawing.Font("Yu Gothic UI", 9.0!)
        Me.ContextMenuBar1.Items.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.ListPopupMenu, Me.TabPopupMenu})
        Me.ContextMenuBar1.Location = New System.Drawing.Point(95, 20)
        Me.ContextMenuBar1.Name = "ContextMenuBar1"
        Me.ContextMenuBar1.Size = New System.Drawing.Size(174, 27)
        Me.ContextMenuBar1.Stretch = True
        Me.ContextMenuBar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.ContextMenuBar1.TabIndex = 4
        Me.ContextMenuBar1.TabStop = False
        Me.ContextMenuBar1.Text = "ContextMenuBar1"
        '
        'ListPopupMenu
        '
        Me.ListPopupMenu.AutoExpandOnClick = True
        Me.ListPopupMenu.Name = "ListPopupMenu"
        Me.ListPopupMenu.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.ListPopupMenuLabelSelItem, Me.ListPopupMenuLabelSelItemText, Me.ListPopupMenuLabelItemList, Me.ListPopupMenuMoveUp, Me.ListPopupMenuMoveDown, Me.ListPopupMenuMoveTop, Me.ListPopupMenuMoveBottom, Me.ListPopupMenuHighLight, Me.ListPopupMenuHighLightList, Me.ListPopupMenuAddItem, Me.ListPopupMenuRemoveItem, Me.ListPopupMenuEditItem, Me.ListPopupMenuLabelItemFile, Me.ListPopupMenuOpenFile, Me.ListPopupMenuViewFile, Me.ListPopupMenuLabelItemWindow, Me.ListPopupMenuListHeight, Me.ListPopupMenuOpacity, Me.ListPopupMenuWinTop, Me.ListPopupMenuExit})
        Me.ListPopupMenu.Text = "ListPopup"
        '
        'ListPopupMenuLabelSelItem
        '
        Me.ListPopupMenuLabelSelItem.BackColor = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.ListPopupMenuLabelSelItem.BorderSide = DevComponents.DotNetBar.eBorderSide.Bottom
        Me.ListPopupMenuLabelSelItem.BorderType = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.ListPopupMenuLabelSelItem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(21, Byte), Integer), CType(CType(110, Byte), Integer))
        Me.ListPopupMenuLabelSelItem.Name = "ListPopupMenuLabelSelItem"
        Me.ListPopupMenuLabelSelItem.PaddingBottom = 1
        Me.ListPopupMenuLabelSelItem.PaddingLeft = 10
        Me.ListPopupMenuLabelSelItem.PaddingTop = 1
        Me.ListPopupMenuLabelSelItem.SingleLineColor = System.Drawing.Color.FromArgb(CType(CType(197, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(197, Byte), Integer))
        Me.ListPopupMenuLabelSelItem.Text = "当前选中"
        '
        'ListPopupMenuLabelSelItemText
        '
        Me.ListPopupMenuLabelSelItemText.BackColor = System.Drawing.Color.Transparent
        Me.ListPopupMenuLabelSelItemText.BorderSide = DevComponents.DotNetBar.eBorderSide.None
        Me.ListPopupMenuLabelSelItemText.BorderType = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.ListPopupMenuLabelSelItemText.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(21, Byte), Integer), CType(CType(110, Byte), Integer))
        Me.ListPopupMenuLabelSelItemText.Name = "ListPopupMenuLabelSelItemText"
        Me.ListPopupMenuLabelSelItemText.PaddingBottom = 1
        Me.ListPopupMenuLabelSelItemText.PaddingLeft = 5
        Me.ListPopupMenuLabelSelItemText.PaddingRight = 5
        Me.ListPopupMenuLabelSelItemText.PaddingTop = 1
        Me.ListPopupMenuLabelSelItemText.SingleLineColor = System.Drawing.Color.FromArgb(CType(CType(197, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(197, Byte), Integer))
        Me.ListPopupMenuLabelSelItemText.Text = "LabelItem2LabelItem2LabelItem2LabelItem2LabelItem2"
        Me.ListPopupMenuLabelSelItemText.Width = 180
        Me.ListPopupMenuLabelSelItemText.WordWrap = True
        '
        'ListPopupMenuLabelItemList
        '
        Me.ListPopupMenuLabelItemList.BackColor = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.ListPopupMenuLabelItemList.BeginGroup = True
        Me.ListPopupMenuLabelItemList.BorderSide = DevComponents.DotNetBar.eBorderSide.Bottom
        Me.ListPopupMenuLabelItemList.BorderType = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.ListPopupMenuLabelItemList.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(21, Byte), Integer), CType(CType(110, Byte), Integer))
        Me.ListPopupMenuLabelItemList.Name = "ListPopupMenuLabelItemList"
        Me.ListPopupMenuLabelItemList.PaddingBottom = 1
        Me.ListPopupMenuLabelItemList.PaddingLeft = 10
        Me.ListPopupMenuLabelItemList.PaddingTop = 1
        Me.ListPopupMenuLabelItemList.SingleLineColor = System.Drawing.Color.FromArgb(CType(CType(197, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(197, Byte), Integer))
        Me.ListPopupMenuLabelItemList.Text = "列表"
        '
        'ListPopupMenuMoveUp
        '
        Me.ListPopupMenuMoveUp.Image = Global.DesktopTips.My.Resources.Resources._112_UpArrowLong_Orange_16x16_72
        Me.ListPopupMenuMoveUp.Name = "ListPopupMenuMoveUp"
        Me.ListPopupMenuMoveUp.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlU)
        Me.ListPopupMenuMoveUp.Text = "上移(&U)"
        '
        'ListPopupMenuMoveDown
        '
        Me.ListPopupMenuMoveDown.Image = Global.DesktopTips.My.Resources.Resources._112_DownArrowLong_Blue_16x16_72
        Me.ListPopupMenuMoveDown.Name = "ListPopupMenuMoveDown"
        Me.ListPopupMenuMoveDown.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlD)
        Me.ListPopupMenuMoveDown.Text = "下移(&D)"
        '
        'ListPopupMenuMoveTop
        '
        Me.ListPopupMenuMoveTop.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far
        Me.ListPopupMenuMoveTop.Name = "ListPopupMenuMoveTop"
        Me.ListPopupMenuMoveTop.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlT)
        Me.ListPopupMenuMoveTop.Text = "移至顶部(&T)"
        '
        'ListPopupMenuMoveBottom
        '
        Me.ListPopupMenuMoveBottom.Name = "ListPopupMenuMoveBottom"
        Me.ListPopupMenuMoveBottom.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlB)
        Me.ListPopupMenuMoveBottom.Text = "移至底部(&B)"
        '
        'ListPopupMenuHighLight
        '
        Me.ListPopupMenuHighLight.BeginGroup = True
        Me.ListPopupMenuHighLight.Image = Global.DesktopTips.My.Resources.Resources.FavoriteStar_FrontFacing_16x16_72
        Me.ListPopupMenuHighLight.Name = "ListPopupMenuHighLight"
        Me.ListPopupMenuHighLight.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlH)
        Me.ListPopupMenuHighLight.Text = "高亮(&H)"
        '
        'ListPopupMenuHighLightList
        '
        Me.ListPopupMenuHighLightList.Name = "ListPopupMenuHighLightList"
        Me.ListPopupMenuHighLightList.Text = "查看高亮(&L)"
        '
        'ListPopupMenuAddItem
        '
        Me.ListPopupMenuAddItem.BeginGroup = True
        Me.ListPopupMenuAddItem.Image = Global.DesktopTips.My.Resources.Resources._112_Plus_Green_16x16_72
        Me.ListPopupMenuAddItem.Name = "ListPopupMenuAddItem"
        Me.ListPopupMenuAddItem.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlA)
        Me.ListPopupMenuAddItem.Text = "添加(&A)"
        '
        'ListPopupMenuRemoveItem
        '
        Me.ListPopupMenuRemoveItem.Image = Global.DesktopTips.My.Resources.Resources._112_Minus_Grey_16x16_72
        Me.ListPopupMenuRemoveItem.Name = "ListPopupMenuRemoveItem"
        Me.ListPopupMenuRemoveItem.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.Del)
        Me.ListPopupMenuRemoveItem.Text = "删除(&X)"
        '
        'ListPopupMenuEditItem
        '
        Me.ListPopupMenuEditItem.Image = Global.DesktopTips.My.Resources.Resources._126_Edit_16x16_72
        Me.ListPopupMenuEditItem.Name = "ListPopupMenuEditItem"
        Me.ListPopupMenuEditItem.Text = "编辑(&E)"
        '
        'ListPopupMenuLabelItemFile
        '
        Me.ListPopupMenuLabelItemFile.BackColor = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.ListPopupMenuLabelItemFile.BorderSide = DevComponents.DotNetBar.eBorderSide.Bottom
        Me.ListPopupMenuLabelItemFile.BorderType = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.ListPopupMenuLabelItemFile.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(21, Byte), Integer), CType(CType(110, Byte), Integer))
        Me.ListPopupMenuLabelItemFile.Name = "ListPopupMenuLabelItemFile"
        Me.ListPopupMenuLabelItemFile.PaddingBottom = 1
        Me.ListPopupMenuLabelItemFile.PaddingLeft = 10
        Me.ListPopupMenuLabelItemFile.PaddingTop = 1
        Me.ListPopupMenuLabelItemFile.SingleLineColor = System.Drawing.Color.FromArgb(CType(CType(197, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(197, Byte), Integer))
        Me.ListPopupMenuLabelItemFile.Text = "文件"
        '
        'ListPopupMenuOpenFile
        '
        Me.ListPopupMenuOpenFile.Name = "ListPopupMenuOpenFile"
        Me.ListPopupMenuOpenFile.Text = "打开文件所在位置(&O)"
        '
        'ListPopupMenuViewFile
        '
        Me.ListPopupMenuViewFile.Name = "ListPopupMenuViewFile"
        Me.ListPopupMenuViewFile.Text = "浏览文件(&V)"
        '
        'ListPopupMenuLabelItemWindow
        '
        Me.ListPopupMenuLabelItemWindow.BackColor = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.ListPopupMenuLabelItemWindow.BorderSide = DevComponents.DotNetBar.eBorderSide.Bottom
        Me.ListPopupMenuLabelItemWindow.BorderType = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.ListPopupMenuLabelItemWindow.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(21, Byte), Integer), CType(CType(110, Byte), Integer))
        Me.ListPopupMenuLabelItemWindow.Name = "ListPopupMenuLabelItemWindow"
        Me.ListPopupMenuLabelItemWindow.PaddingBottom = 1
        Me.ListPopupMenuLabelItemWindow.PaddingLeft = 10
        Me.ListPopupMenuLabelItemWindow.PaddingTop = 1
        Me.ListPopupMenuLabelItemWindow.SingleLineColor = System.Drawing.Color.FromArgb(CType(CType(197, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(197, Byte), Integer))
        Me.ListPopupMenuLabelItemWindow.Text = "窗口"
        '
        'ListPopupMenuListHeight
        '
        Me.ListPopupMenuListHeight.AutoCheckOnClick = True
        Me.ListPopupMenuListHeight.Name = "ListPopupMenuListHeight"
        Me.ListPopupMenuListHeight.Text = "列表高度设置(&E)"
        '
        'ListPopupMenuOpacity
        '
        Me.ListPopupMenuOpacity.Name = "ListPopupMenuOpacity"
        Me.ListPopupMenuOpacity.Text = "不透明度(&P)"
        '
        'ListPopupMenuWinTop
        '
        Me.ListPopupMenuWinTop.Name = "ListPopupMenuWinTop"
        Me.ListPopupMenuWinTop.Text = "窗口置顶(&W)"
        '
        'ListPopupMenuExit
        '
        Me.ListPopupMenuExit.Name = "ListPopupMenuExit"
        Me.ListPopupMenuExit.Text = "退出(&E)"
        '
        'TabPopupMenu
        '
        Me.TabPopupMenu.AutoExpandOnClick = True
        Me.TabPopupMenu.Name = "TabPopupMenu"
        Me.TabPopupMenu.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.PopMenuButtonNewTab, Me.PopMenuButtonDeleteTab})
        Me.TabPopupMenu.Text = "TabPopup"
        '
        'PopMenuButtonNewTab
        '
        Me.PopMenuButtonNewTab.Name = "PopMenuButtonNewTab"
        Me.PopMenuButtonNewTab.Text = "新建分组(&N)"
        '
        'PopMenuButtonDeleteTab
        '
        Me.PopMenuButtonDeleteTab.Name = "PopMenuButtonDeleteTab"
        Me.PopMenuButtonDeleteTab.Text = "删除分组(&D)"
        '
        'TabStrip
        '
        Me.TabStrip.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TabStrip.AutoSelectAttachedControl = False
        '
        '
        '
        Me.TabStrip.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.TabStrip.ContainerControlProcessDialogKey = True
        Me.ContextMenuBar1.SetContextMenuEx(Me.TabStrip, Me.TabPopupMenu)
        '
        '
        '
        '
        '
        '
        Me.TabStrip.ControlBox.CloseBox.Name = ""
        '
        '
        '
        Me.TabStrip.ControlBox.MenuBox.Name = ""
        Me.TabStrip.ControlBox.Name = ""
        Me.TabStrip.ControlBox.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.TabStrip.ControlBox.MenuBox, Me.TabStrip.ControlBox.CloseBox})
        Me.TabStrip.HorizontalText = False
        Me.TabStrip.Location = New System.Drawing.Point(0, 0)
        Me.TabStrip.Name = "TabStrip"
        Me.TabStrip.ReorderTabsEnabled = True
        Me.TabStrip.RotateVerticalText = True
        Me.TabStrip.SelectedTabFont = New System.Drawing.Font("Microsoft YaHei UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.TabStrip.SelectedTabIndex = 0
        Me.TabStrip.Size = New System.Drawing.Size(28, 275)
        Me.TabStrip.TabAlignment = DevComponents.DotNetBar.eTabStripAlignment.Left
        Me.TabStrip.TabCloseButtonHot = Nothing
        Me.TabStrip.TabFont = New System.Drawing.Font("Microsoft YaHei UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabStrip.TabIndex = 7
        Me.TabStrip.Tabs.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.SuperTabItemDefault})
        Me.TabStrip.Text = "SuperTabStrip1"
        Me.TabStrip.TextAlignment = DevComponents.DotNetBar.eItemAlignment.Center
        '
        'SuperTabItemDefault
        '
        Me.SuperTabItemDefault.GlobalItem = False
        Me.SuperTabItemDefault.Name = "SuperTabItemDefault"
        Me.SuperTabItemDefault.Text = "默认"
        '
        'ButtonItemUp
        '
        Me.ButtonItemUp.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.ButtonItemUp.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.ButtonItemUp.Font = New System.Drawing.Font("Yu Gothic UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.ButtonItemUp.Location = New System.Drawing.Point(123, 151)
        Me.ButtonItemUp.Name = "ButtonItemUp"
        Me.ButtonItemUp.Shape = New DevComponents.DotNetBar.RoundRectangleShapeDescriptor()
        Me.ButtonItemUp.Size = New System.Drawing.Size(17, 17)
        Me.ButtonItemUp.TabIndex = 5
        Me.ButtonItemUp.Tag = "True"
        Me.ButtonItemUp.Text = "↑"
        '
        'ButtonItemDown
        '
        Me.ButtonItemDown.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.ButtonItemDown.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.ButtonItemDown.Font = New System.Drawing.Font("Yu Gothic UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.ButtonItemDown.Location = New System.Drawing.Point(139, 151)
        Me.ButtonItemDown.Name = "ButtonItemDown"
        Me.ButtonItemDown.Shape = New DevComponents.DotNetBar.RoundRectangleShapeDescriptor()
        Me.ButtonItemDown.Size = New System.Drawing.Size(17, 17)
        Me.ButtonItemDown.TabIndex = 6
        Me.ButtonItemDown.Tag = "True"
        Me.ButtonItemDown.Text = "↓"
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.DarkRed
        Me.ClientSize = New System.Drawing.Size(292, 299)
        Me.Controls.Add(Me.ButtonItemDown)
        Me.Controls.Add(Me.ButtonItemUp)
        Me.Controls.Add(Me.ContextMenuBar1)
        Me.Controls.Add(Me.ListView)
        Me.Controls.Add(Me.ButtonChangeHeight)
        Me.Controls.Add(Me.ButtonCloseForm)
        Me.Controls.Add(Me.ButtonAddItem)
        Me.Controls.Add(Me.ButtonRemoveItem)
        Me.Controls.Add(Me.LabelFocus)
        Me.Controls.Add(Me.NumericUpDownListCnt)
        Me.Controls.Add(Me.TabStrip)
        Me.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(127, 112)
        Me.Name = "MainForm"
        Me.Opacity = 0.0R
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Text = "DesktopTips"
        Me.TransparencyKey = System.Drawing.Color.DarkRed
        CType(Me.NumericUpDownListCnt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ContextMenuBar1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TabStrip, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ButtonRemoveItem As DevComponents.DotNetBar.ButtonX
    Friend WithEvents ButtonAddItem As DevComponents.DotNetBar.ButtonX
    Friend WithEvents StyleManager1 As DevComponents.DotNetBar.StyleManager
    Friend WithEvents LabelFocus As System.Windows.Forms.Label
    Friend WithEvents ListView As System.Windows.Forms.ListBox
    Friend WithEvents ButtonCloseForm As DevComponents.DotNetBar.ButtonX
    Friend WithEvents ButtonChangeHeight As DevComponents.DotNetBar.ButtonX
    Friend WithEvents NumericUpDownListCnt As System.Windows.Forms.NumericUpDown
    Friend WithEvents TimerShowForm As System.Windows.Forms.Timer
    Friend WithEvents TimerEndForm As System.Windows.Forms.Timer
    Friend WithEvents TimerMouseIn As System.Windows.Forms.Timer
    Friend WithEvents TimerMouseOut As System.Windows.Forms.Timer
    Friend WithEvents SuperTooltip As DevComponents.DotNetBar.SuperTooltip
    Friend WithEvents ContextMenuBar1 As DevComponents.DotNetBar.ContextMenuBar
    Friend WithEvents ListPopupMenu As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ListPopupMenuMoveUp As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ListPopupMenuMoveDown As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ListPopupMenuAddItem As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ListPopupMenuRemoveItem As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ListPopupMenuWinTop As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ListPopupMenuOpenFile As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ListPopupMenuLabelItemList As DevComponents.DotNetBar.LabelItem
    Friend WithEvents ListPopupMenuLabelItemWindow As DevComponents.DotNetBar.LabelItem
    Friend WithEvents ListPopupMenuEditItem As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ListPopupMenuViewFile As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ListPopupMenuExit As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ListPopupMenuHighLight As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ListPopupMenuHighLightList As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ListPopupMenuOpacity As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ListPopupMenuLabelItemFile As DevComponents.DotNetBar.LabelItem
    Friend WithEvents ListPopupMenuListHeight As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ButtonItemUp As DevComponents.DotNetBar.ButtonX
    Friend WithEvents ButtonItemDown As DevComponents.DotNetBar.ButtonX
    Friend WithEvents ListPopupMenuLabelSelItem As DevComponents.DotNetBar.LabelItem
    Friend WithEvents ListPopupMenuLabelSelItemText As DevComponents.DotNetBar.LabelItem
    Friend WithEvents ListPopupMenuMoveBottom As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ListPopupMenuMoveTop As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents TabStrip As DevComponents.DotNetBar.SuperTabStrip
    Friend WithEvents SuperTabItemDefault As DevComponents.DotNetBar.SuperTabItem
    Friend WithEvents TabPopupMenu As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents PopMenuButtonNewTab As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents PopMenuButtonDeleteTab As DevComponents.DotNetBar.ButtonItem

End Class
