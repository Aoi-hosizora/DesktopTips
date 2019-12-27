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
        Me.MyStyleManager = New DevComponents.DotNetBar.StyleManager(Me.components)
        Me.LabelFocus = New System.Windows.Forms.Label()
        Me.ListView = New System.Windows.Forms.ListBox()
        Me.ButtonCloseForm = New DevComponents.DotNetBar.ButtonX()
        Me.ButtonListSetting = New DevComponents.DotNetBar.ButtonX()
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
        Me.ListPopupMenuItemContainer = New DevComponents.DotNetBar.ItemContainer()
        Me.ListPopupMenuAddItem = New DevComponents.DotNetBar.ButtonItem()
        Me.ListPopupMenuRemoveItem = New DevComponents.DotNetBar.ButtonItem()
        Me.ListPopupMenuEditItem = New DevComponents.DotNetBar.ButtonItem()
        Me.ListPopupMenuMoveUp = New DevComponents.DotNetBar.ButtonItem()
        Me.ListPopupMenuMoveDown = New DevComponents.DotNetBar.ButtonItem()
        Me.ListPopupMenuCopy = New DevComponents.DotNetBar.ButtonItem()
        Me.ListPopupMenuSelectAll = New DevComponents.DotNetBar.ButtonItem()
        Me.ListPopupMenuHighLight = New DevComponents.DotNetBar.ButtonItem()
        Me.ListPopupMenuMoveTop = New DevComponents.DotNetBar.ButtonItem()
        Me.ListPopupMenuMoveBottom = New DevComponents.DotNetBar.ButtonItem()
        Me.ListPopupMenuViewHighLight = New DevComponents.DotNetBar.ButtonItem()
        Me.ListPopupMenuFind = New DevComponents.DotNetBar.ButtonItem()
        Me.ListPopupMenuMove = New DevComponents.DotNetBar.ButtonItem()
        Me.ListPopupMenuLabelItemFile = New DevComponents.DotNetBar.LabelItem()
        Me.ListPopupMenuOpenDir = New DevComponents.DotNetBar.ButtonItem()
        Me.ListPopupMenuViewFile = New DevComponents.DotNetBar.ButtonItem()
        Me.ListPopupMenuOpenBrowser = New DevComponents.DotNetBar.ButtonItem()
        Me.ListPopupMenuSyncData = New DevComponents.DotNetBar.ButtonItem()
        Me.ListPopupMenuSyncDataTo = New DevComponents.DotNetBar.ButtonItem()
        Me.ListPopupMenuSyncDataFrom = New DevComponents.DotNetBar.ButtonItem()
        Me.ListPopupMenuLabelItemWindow = New DevComponents.DotNetBar.LabelItem()
        Me.ListPopupMenuWinSetting = New DevComponents.DotNetBar.ButtonItem()
        Me.ListPopupMenuShotcutSetting = New DevComponents.DotNetBar.ButtonItem()
        Me.ListPopupMenuListHeight = New DevComponents.DotNetBar.ButtonItem()
        Me.ListPopupMenuOpacity = New DevComponents.DotNetBar.ButtonItem()
        Me.ListPopupMenuFold = New DevComponents.DotNetBar.ButtonItem()
        Me.ListPopupMenuWinTop = New DevComponents.DotNetBar.ButtonItem()
        Me.ListPopupMenuWinHighColor = New DevComponents.DotNetBar.ColorPickerDropDown()
        Me.ListPopupMenuLoadPos = New DevComponents.DotNetBar.ButtonItem()
        Me.ListPopupMenuSavePos = New DevComponents.DotNetBar.ButtonItem()
        Me.ListPopupMenuExit = New DevComponents.DotNetBar.ButtonItem()
        Me.TabPopupMenu = New DevComponents.DotNetBar.ButtonItem()
        Me.TabPopupMenuLabel = New DevComponents.DotNetBar.LabelItem()
        Me.TabPopupMenuNewTab = New DevComponents.DotNetBar.ButtonItem()
        Me.TabPopupMenuDeleteTab = New DevComponents.DotNetBar.ButtonItem()
        Me.TabPopupMenuRenameTab = New DevComponents.DotNetBar.ButtonItem()
        Me.TabPopupMenuMove = New DevComponents.DotNetBar.ButtonItem()
        Me.CmdsPopupMenu = New DevComponents.DotNetBar.ButtonItem()
        Me.CmdsPopupMenuAppend = New DevComponents.DotNetBar.ButtonItem()
        Me.TabStrip = New DevComponents.DotNetBar.SuperTabStrip()
        Me.TabItemTest = New DevComponents.DotNetBar.SuperTabItem()
        Me.TabItemTest2 = New DevComponents.DotNetBar.SuperTabItem()
        Me.LabelNothing = New System.Windows.Forms.Label()
        Me.ButtonItemUp = New DevComponents.DotNetBar.ButtonX()
        Me.ButtonItemDown = New DevComponents.DotNetBar.ButtonX()
        Me.ButtonResizeFlag = New DevComponents.DotNetBar.ButtonX()
        Me.HoverToolTip = New System.Windows.Forms.ToolTip(Me.components)
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
        Me.ButtonRemoveItem.Location = New System.Drawing.Point(293, 239)
        Me.ButtonRemoveItem.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ButtonRemoveItem.Name = "ButtonRemoveItem"
        Me.ButtonRemoveItem.Shape = New DevComponents.DotNetBar.RoundRectangleShapeDescriptor()
        Me.ButtonRemoveItem.Size = New System.Drawing.Size(24, 23)
        Me.ButtonRemoveItem.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.ButtonRemoveItem.TabIndex = 5
        Me.ButtonRemoveItem.Text = "－"
        '
        'ButtonAddItem
        '
        Me.ButtonAddItem.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.ButtonAddItem.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonAddItem.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.ButtonAddItem.Location = New System.Drawing.Point(270, 239)
        Me.ButtonAddItem.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ButtonAddItem.Name = "ButtonAddItem"
        Me.ButtonAddItem.Shape = New DevComponents.DotNetBar.RoundRectangleShapeDescriptor()
        Me.ButtonAddItem.Size = New System.Drawing.Size(24, 23)
        Me.ButtonAddItem.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.ButtonAddItem.TabIndex = 4
        Me.ButtonAddItem.Text = "＋"
        '
        'MyStyleManager
        '
        Me.MyStyleManager.ManagerColorTint = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.MyStyleManager.ManagerStyle = DevComponents.DotNetBar.eStyle.Office2007Blue
        Me.MyStyleManager.MetroColorParameters = New DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.White, System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(87, Byte), Integer), CType(CType(154, Byte), Integer)))
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
        Me.ListView.BackColor = System.Drawing.Color.Snow
        Me.ContextMenuBar1.SetContextMenuEx(Me.ListView, Me.ListPopupMenu)
        Me.ListView.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable
        Me.ListView.FormattingEnabled = True
        Me.ListView.ItemHeight = 17
        Me.ListView.Items.AddRange(New Object() {"Test", "Test", "Test", "Test", "Test", "Test", "Test", "Test", "Test", "Test", "Test", "Test"})
        Me.ListView.Location = New System.Drawing.Point(23, 0)
        Me.ListView.Name = "ListView"
        Me.ListView.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.ListView.Size = New System.Drawing.Size(303, 240)
        Me.ListView.TabIndex = 0
        '
        'ButtonCloseForm
        '
        Me.ButtonCloseForm.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.ButtonCloseForm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ButtonCloseForm.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.ButtonCloseForm.Location = New System.Drawing.Point(0, 239)
        Me.ButtonCloseForm.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ButtonCloseForm.Name = "ButtonCloseForm"
        Me.ButtonCloseForm.Shape = New DevComponents.DotNetBar.RoundRectangleShapeDescriptor()
        Me.ButtonCloseForm.Size = New System.Drawing.Size(24, 23)
        Me.ButtonCloseForm.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.SuperTooltip.SetSuperTooltip(Me.ButtonCloseForm, New DevComponents.DotNetBar.SuperTooltipInfo("退出", "", "退出程序，保存数据。", Nothing, Nothing, DevComponents.DotNetBar.eTooltipColor.Gray, True, True, New System.Drawing.Size(180, 68)))
        Me.ButtonCloseForm.TabIndex = 1
        Me.ButtonCloseForm.Text = "×"
        '
        'ButtonListSetting
        '
        Me.ButtonListSetting.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.ButtonListSetting.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ButtonListSetting.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.ButtonListSetting.Location = New System.Drawing.Point(23, 239)
        Me.ButtonListSetting.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ButtonListSetting.Name = "ButtonListSetting"
        Me.ButtonListSetting.Shape = New DevComponents.DotNetBar.RoundRectangleShapeDescriptor()
        Me.ButtonListSetting.Size = New System.Drawing.Size(24, 23)
        Me.ButtonListSetting.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.SuperTooltip.SetSuperTooltip(Me.ButtonListSetting, New DevComponents.DotNetBar.SuperTooltipInfo("设置", "", "左键单击显示弹出菜单，右键点击显示设置列表高度。", Nothing, Nothing, DevComponents.DotNetBar.eTooltipColor.Gray, True, True, New System.Drawing.Size(180, 68)))
        Me.ButtonListSetting.TabIndex = 2
        Me.ButtonListSetting.Text = "≡"
        '
        'NumericUpDownListCnt
        '
        Me.NumericUpDownListCnt.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.NumericUpDownListCnt.Location = New System.Drawing.Point(46, 239)
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
        Me.ContextMenuBar1.Items.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.ListPopupMenu, Me.TabPopupMenu, Me.CmdsPopupMenu})
        Me.ContextMenuBar1.Location = New System.Drawing.Point(46, 40)
        Me.ContextMenuBar1.Name = "ContextMenuBar1"
        Me.ContextMenuBar1.Size = New System.Drawing.Size(240, 27)
        Me.ContextMenuBar1.Stretch = True
        Me.ContextMenuBar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.ContextMenuBar1.TabIndex = 4
        Me.ContextMenuBar1.TabStop = False
        '
        'ListPopupMenu
        '
        Me.ListPopupMenu.AutoExpandOnClick = True
        Me.ListPopupMenu.Name = "ListPopupMenu"
        Me.ListPopupMenu.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.ListPopupMenuLabelSelItem, Me.ListPopupMenuLabelSelItemText, Me.ListPopupMenuLabelItemList, Me.ListPopupMenuItemContainer, Me.ListPopupMenuMoveTop, Me.ListPopupMenuMoveBottom, Me.ListPopupMenuViewHighLight, Me.ListPopupMenuFind, Me.ListPopupMenuMove, Me.ListPopupMenuLabelItemFile, Me.ListPopupMenuOpenDir, Me.ListPopupMenuViewFile, Me.ListPopupMenuOpenBrowser, Me.ListPopupMenuSyncData, Me.ListPopupMenuLabelItemWindow, Me.ListPopupMenuWinSetting, Me.ListPopupMenuExit})
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
        Me.ListPopupMenuLabelSelItem.Text = "当前选中 (共 0 项)"
        '
        'ListPopupMenuLabelSelItemText
        '
        Me.ListPopupMenuLabelSelItemText.BackColor = System.Drawing.Color.Transparent
        Me.ListPopupMenuLabelSelItemText.BorderSide = DevComponents.DotNetBar.eBorderSide.None
        Me.ListPopupMenuLabelSelItemText.BorderType = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.ListPopupMenuLabelSelItemText.EnableMarkup = False
        Me.ListPopupMenuLabelSelItemText.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(21, Byte), Integer), CType(CType(110, Byte), Integer))
        Me.ListPopupMenuLabelSelItemText.Name = "ListPopupMenuLabelSelItemText"
        Me.ListPopupMenuLabelSelItemText.PaddingLeft = 5
        Me.ListPopupMenuLabelSelItemText.PaddingRight = 5
        Me.ListPopupMenuLabelSelItemText.SingleLineColor = System.Drawing.Color.FromArgb(CType(CType(197, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(197, Byte), Integer))
        Me.ListPopupMenuLabelSelItemText.Text = "https://www.youtube.com/watch?v=wb94Z3Ck_uU&t=1015s"
        Me.ListPopupMenuLabelSelItemText.Width = 185
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
        Me.ListPopupMenuLabelItemList.Text = "列表 (共 0 项，高亮 0 项)"
        '
        'ListPopupMenuItemContainer
        '
        '
        '
        '
        Me.ListPopupMenuItemContainer.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.ListPopupMenuItemContainer.HorizontalItemAlignment = DevComponents.DotNetBar.eHorizontalItemsAlignment.Center
        Me.ListPopupMenuItemContainer.ItemSpacing = -1
        Me.ListPopupMenuItemContainer.Name = "ListPopupMenuItemContainer"
        Me.ListPopupMenuItemContainer.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.ListPopupMenuAddItem, Me.ListPopupMenuRemoveItem, Me.ListPopupMenuEditItem, Me.ListPopupMenuMoveUp, Me.ListPopupMenuMoveDown, Me.ListPopupMenuCopy, Me.ListPopupMenuSelectAll, Me.ListPopupMenuHighLight})
        '
        '
        '
        Me.ListPopupMenuItemContainer.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        'ListPopupMenuAddItem
        '
        Me.ListPopupMenuAddItem.BeginGroup = True
        Me.ListPopupMenuAddItem.Image = Global.DesktopTips.My.Resources.Resources.Plus
        Me.ListPopupMenuAddItem.Name = "ListPopupMenuAddItem"
        Me.ListPopupMenuAddItem.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlN)
        Me.ListPopupMenuAddItem.Text = "添加(&N)"
        Me.ListPopupMenuAddItem.Tooltip = "添加"
        '
        'ListPopupMenuRemoveItem
        '
        Me.ListPopupMenuRemoveItem.Image = Global.DesktopTips.My.Resources.Resources.Minus
        Me.ListPopupMenuRemoveItem.Name = "ListPopupMenuRemoveItem"
        Me.ListPopupMenuRemoveItem.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.Del)
        Me.ListPopupMenuRemoveItem.Text = "删除(&X)"
        Me.ListPopupMenuRemoveItem.Tooltip = "删除"
        '
        'ListPopupMenuEditItem
        '
        Me.ListPopupMenuEditItem.Image = Global.DesktopTips.My.Resources.Resources.Edit
        Me.ListPopupMenuEditItem.Name = "ListPopupMenuEditItem"
        Me.ListPopupMenuEditItem.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.F2)
        Me.ListPopupMenuEditItem.Text = "编辑(&E)"
        Me.ListPopupMenuEditItem.Tooltip = "编辑"
        '
        'ListPopupMenuMoveUp
        '
        Me.ListPopupMenuMoveUp.BeginGroup = True
        Me.ListPopupMenuMoveUp.Image = Global.DesktopTips.My.Resources.Resources.Up
        Me.ListPopupMenuMoveUp.Name = "ListPopupMenuMoveUp"
        Me.ListPopupMenuMoveUp.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlU)
        Me.ListPopupMenuMoveUp.Text = "上移(&U)"
        Me.ListPopupMenuMoveUp.Tooltip = "上移"
        '
        'ListPopupMenuMoveDown
        '
        Me.ListPopupMenuMoveDown.Image = Global.DesktopTips.My.Resources.Resources.Down
        Me.ListPopupMenuMoveDown.Name = "ListPopupMenuMoveDown"
        Me.ListPopupMenuMoveDown.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlD)
        Me.ListPopupMenuMoveDown.Text = "下移(&D)"
        Me.ListPopupMenuMoveDown.Tooltip = "下移"
        '
        'ListPopupMenuCopy
        '
        Me.ListPopupMenuCopy.BeginGroup = True
        Me.ListPopupMenuCopy.Image = Global.DesktopTips.My.Resources.Resources.Copy
        Me.ListPopupMenuCopy.Name = "ListPopupMenuCopy"
        Me.ListPopupMenuCopy.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlC)
        Me.ListPopupMenuCopy.Text = "复制(&C)"
        '
        'ListPopupMenuSelectAll
        '
        Me.ListPopupMenuSelectAll.Image = Global.DesktopTips.My.Resources.Resources.SelectAll
        Me.ListPopupMenuSelectAll.Name = "ListPopupMenuSelectAll"
        Me.ListPopupMenuSelectAll.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlA)
        Me.ListPopupMenuSelectAll.Text = "全选(&A)"
        Me.ListPopupMenuSelectAll.Tooltip = "全选"
        '
        'ListPopupMenuHighLight
        '
        Me.ListPopupMenuHighLight.Image = Global.DesktopTips.My.Resources.Resources.Star
        Me.ListPopupMenuHighLight.Name = "ListPopupMenuHighLight"
        Me.ListPopupMenuHighLight.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlH)
        Me.ListPopupMenuHighLight.Text = "高亮(&H)"
        Me.ListPopupMenuHighLight.Tooltip = "高亮"
        '
        'ListPopupMenuMoveTop
        '
        Me.ListPopupMenuMoveTop.BeginGroup = True
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
        'ListPopupMenuViewHighLight
        '
        Me.ListPopupMenuViewHighLight.Name = "ListPopupMenuViewHighLight"
        Me.ListPopupMenuViewHighLight.Text = "浏览所有高亮(&L)"
        '
        'ListPopupMenuFind
        '
        Me.ListPopupMenuFind.BeginGroup = True
        Me.ListPopupMenuFind.Image = Global.DesktopTips.My.Resources.Resources.Find
        Me.ListPopupMenuFind.Name = "ListPopupMenuFind"
        Me.ListPopupMenuFind.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlF)
        Me.ListPopupMenuFind.Text = "查找(&F)"
        '
        'ListPopupMenuMove
        '
        Me.ListPopupMenuMove.Image = Global.DesktopTips.My.Resources.Resources.Right
        Me.ListPopupMenuMove.Name = "ListPopupMenuMove"
        Me.ListPopupMenuMove.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlM)
        Me.ListPopupMenuMove.Text = "移动至(&M)"
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
        'ListPopupMenuOpenDir
        '
        Me.ListPopupMenuOpenDir.Name = "ListPopupMenuOpenDir"
        Me.ListPopupMenuOpenDir.Text = "打开文件所在位置(&O)"
        '
        'ListPopupMenuViewFile
        '
        Me.ListPopupMenuViewFile.Name = "ListPopupMenuViewFile"
        Me.ListPopupMenuViewFile.Text = "浏览当前列表内容(&V)"
        '
        'ListPopupMenuOpenBrowser
        '
        Me.ListPopupMenuOpenBrowser.Name = "ListPopupMenuOpenBrowser"
        Me.ListPopupMenuOpenBrowser.Text = "打开浏览器链接(&B)"
        '
        'ListPopupMenuSyncData
        '
        Me.ListPopupMenuSyncData.Name = "ListPopupMenuSyncData"
        Me.ListPopupMenuSyncData.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.ListPopupMenuSyncDataTo, Me.ListPopupMenuSyncDataFrom})
        Me.ListPopupMenuSyncData.Text = "数据同步(&Y)"
        '
        'ListPopupMenuSyncDataTo
        '
        Me.ListPopupMenuSyncDataTo.Name = "ListPopupMenuSyncDataTo"
        Me.ListPopupMenuSyncDataTo.Text = "同步到移动端(&T)"
        '
        'ListPopupMenuSyncDataFrom
        '
        Me.ListPopupMenuSyncDataFrom.Name = "ListPopupMenuSyncDataFrom"
        Me.ListPopupMenuSyncDataFrom.Text = "从移动端同步(&F)"
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
        'ListPopupMenuWinSetting
        '
        Me.ListPopupMenuWinSetting.Name = "ListPopupMenuWinSetting"
        Me.ListPopupMenuWinSetting.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.ListPopupMenuShotcutSetting, Me.ListPopupMenuListHeight, Me.ListPopupMenuOpacity, Me.ListPopupMenuFold, Me.ListPopupMenuWinTop, Me.ListPopupMenuWinHighColor, Me.ListPopupMenuLoadPos, Me.ListPopupMenuSavePos})
        Me.ListPopupMenuWinSetting.Text = "显示(&S)"
        '
        'ListPopupMenuShotcutSetting
        '
        Me.ListPopupMenuShotcutSetting.Name = "ListPopupMenuShotcutSetting"
        Me.ListPopupMenuShotcutSetting.Text = "快捷键设置(&R)"
        '
        'ListPopupMenuListHeight
        '
        Me.ListPopupMenuListHeight.AutoCheckOnClick = True
        Me.ListPopupMenuListHeight.Name = "ListPopupMenuListHeight"
        Me.ListPopupMenuListHeight.Text = "列表高度设置(&E)"
        '
        'ListPopupMenuOpacity
        '
        Me.ListPopupMenuOpacity.BeginGroup = True
        Me.ListPopupMenuOpacity.Name = "ListPopupMenuOpacity"
        Me.ListPopupMenuOpacity.Text = "不透明度(&P)"
        '
        'ListPopupMenuFold
        '
        Me.ListPopupMenuFold.Name = "ListPopupMenuFold"
        Me.ListPopupMenuFold.Text = "折叠菜单(&F)"
        '
        'ListPopupMenuWinTop
        '
        Me.ListPopupMenuWinTop.Name = "ListPopupMenuWinTop"
        Me.ListPopupMenuWinTop.Text = "窗口置顶(&W)"
        '
        'ListPopupMenuWinHighColor
        '
        Me.ListPopupMenuWinHighColor.Image = Global.DesktopTips.My.Resources.Resources.HighLightColor
        Me.ListPopupMenuWinHighColor.Name = "ListPopupMenuWinHighColor"
        Me.ListPopupMenuWinHighColor.Text = "高亮颜色(&C)"
        '
        'ListPopupMenuLoadPos
        '
        Me.ListPopupMenuLoadPos.BeginGroup = True
        Me.ListPopupMenuLoadPos.Name = "ListPopupMenuLoadPos"
        Me.ListPopupMenuLoadPos.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlR)
        Me.ListPopupMenuLoadPos.Text = "恢复位置(&L)"
        '
        'ListPopupMenuSavePos
        '
        Me.ListPopupMenuSavePos.Name = "ListPopupMenuSavePos"
        Me.ListPopupMenuSavePos.Text = "保存当前位置(&S)"
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
        Me.TabPopupMenu.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.TabPopupMenuLabel, Me.TabPopupMenuNewTab, Me.TabPopupMenuDeleteTab, Me.TabPopupMenuRenameTab, Me.TabPopupMenuMove})
        Me.TabPopupMenu.Text = "TabPopup"
        '
        'TabPopupMenuLabel
        '
        Me.TabPopupMenuLabel.BackColor = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.TabPopupMenuLabel.BorderSide = DevComponents.DotNetBar.eBorderSide.Bottom
        Me.TabPopupMenuLabel.BorderType = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.TabPopupMenuLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(21, Byte), Integer), CType(CType(110, Byte), Integer))
        Me.TabPopupMenuLabel.Name = "TabPopupMenuLabel"
        Me.TabPopupMenuLabel.PaddingBottom = 1
        Me.TabPopupMenuLabel.PaddingLeft = 10
        Me.TabPopupMenuLabel.PaddingTop = 1
        Me.TabPopupMenuLabel.SingleLineColor = System.Drawing.Color.FromArgb(CType(CType(197, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(197, Byte), Integer))
        Me.TabPopupMenuLabel.Text = "分组 (共 0 组)"
        '
        'TabPopupMenuNewTab
        '
        Me.TabPopupMenuNewTab.Image = Global.DesktopTips.My.Resources.Resources.Plus
        Me.TabPopupMenuNewTab.Name = "TabPopupMenuNewTab"
        Me.TabPopupMenuNewTab.Text = "新建分组(&N)"
        '
        'TabPopupMenuDeleteTab
        '
        Me.TabPopupMenuDeleteTab.Image = Global.DesktopTips.My.Resources.Resources.Minus
        Me.TabPopupMenuDeleteTab.Name = "TabPopupMenuDeleteTab"
        Me.TabPopupMenuDeleteTab.Text = "删除分组(&D)"
        '
        'TabPopupMenuRenameTab
        '
        Me.TabPopupMenuRenameTab.Image = Global.DesktopTips.My.Resources.Resources.Edit
        Me.TabPopupMenuRenameTab.Name = "TabPopupMenuRenameTab"
        Me.TabPopupMenuRenameTab.Text = "重命名(&R)"
        '
        'TabPopupMenuMove
        '
        Me.TabPopupMenuMove.BeginGroup = True
        Me.TabPopupMenuMove.Image = Global.DesktopTips.My.Resources.Resources.Right
        Me.TabPopupMenuMove.Name = "TabPopupMenuMove"
        Me.TabPopupMenuMove.Text = "移动至分组(&M)"
        '
        'CmdsPopupMenu
        '
        Me.CmdsPopupMenu.AutoExpandOnClick = True
        Me.CmdsPopupMenu.Name = "CmdsPopupMenu"
        Me.CmdsPopupMenu.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.CmdsPopupMenuAppend})
        Me.CmdsPopupMenu.Text = "Cmds"
        Me.CmdsPopupMenu.Visible = False
        '
        'CmdsPopupMenuAppend
        '
        Me.CmdsPopupMenuAppend.Name = "CmdsPopupMenuAppend"
        Me.CmdsPopupMenuAppend.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlV)
        Me.CmdsPopupMenuAppend.Text = "附加"
        '
        'TabStrip
        '
        Me.TabStrip.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TabStrip.AutoSelectAttachedControl = False
        Me.TabStrip.BackColor = System.Drawing.Color.DarkRed
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
        Me.TabStrip.SelectedTabFont = New System.Drawing.Font("Microsoft YaHei UI Light", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabStrip.SelectedTabIndex = 0
        Me.TabStrip.Size = New System.Drawing.Size(25, 239)
        Me.TabStrip.TabAlignment = DevComponents.DotNetBar.eTabStripAlignment.Left
        Me.TabStrip.TabCloseButtonHot = Nothing
        Me.TabStrip.TabFont = New System.Drawing.Font("Microsoft YaHei UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabStrip.TabHorizontalSpacing = 2
        Me.TabStrip.TabIndex = 9
        Me.TabStrip.Tabs.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.TabItemTest, Me.TabItemTest2})
        Me.TabStrip.TabStyle = DevComponents.DotNetBar.eSuperTabStyle.VisualStudio2008Dock
        Me.TabStrip.TabVerticalSpacing = 3
        Me.TabStrip.Text = "TabStrip"
        Me.TabStrip.TextAlignment = DevComponents.DotNetBar.eItemAlignment.Center
        '
        'TabItemTest
        '
        Me.TabItemTest.GlobalItem = False
        Me.TabItemTest.Name = "TabItemTest"
        Me.TabItemTest.Text = "Test"
        Me.TabItemTest.Visible = False
        '
        'TabItemTest2
        '
        Me.TabItemTest2.GlobalItem = False
        Me.TabItemTest2.Name = "TabItemTest2"
        Me.TabItemTest2.Text = "Test"
        Me.TabItemTest2.Visible = False
        '
        'LabelNothing
        '
        Me.LabelNothing.BackColor = System.Drawing.Color.Snow
        Me.ContextMenuBar1.SetContextMenuEx(Me.LabelNothing, Me.ListPopupMenu)
        Me.LabelNothing.ForeColor = System.Drawing.Color.DimGray
        Me.LabelNothing.Location = New System.Drawing.Point(213, 197)
        Me.LabelNothing.Name = "LabelNothing"
        Me.LabelNothing.Size = New System.Drawing.Size(56, 20)
        Me.LabelNothing.TabIndex = 10
        Me.LabelNothing.Text = "无内容"
        Me.LabelNothing.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ButtonItemUp
        '
        Me.ButtonItemUp.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.ButtonItemUp.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.ButtonItemUp.Font = New System.Drawing.Font("Yu Gothic UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonItemUp.Image = Global.DesktopTips.My.Resources.Resources.UpIcon
        Me.ButtonItemUp.Location = New System.Drawing.Point(139, 144)
        Me.ButtonItemUp.Name = "ButtonItemUp"
        Me.ButtonItemUp.Shape = New DevComponents.DotNetBar.RoundRectangleShapeDescriptor()
        Me.ButtonItemUp.Size = New System.Drawing.Size(17, 9)
        Me.ButtonItemUp.TabIndex = 7
        Me.ButtonItemUp.Tag = "True"
        Me.ButtonItemUp.Tooltip = "上移(U)"
        '
        'ButtonItemDown
        '
        Me.ButtonItemDown.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.ButtonItemDown.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.ButtonItemDown.Font = New System.Drawing.Font("Yu Gothic UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.ButtonItemDown.Image = Global.DesktopTips.My.Resources.Resources.DownIcon
        Me.ButtonItemDown.Location = New System.Drawing.Point(139, 152)
        Me.ButtonItemDown.Name = "ButtonItemDown"
        Me.ButtonItemDown.Shape = New DevComponents.DotNetBar.RoundRectangleShapeDescriptor()
        Me.ButtonItemDown.Size = New System.Drawing.Size(17, 9)
        Me.ButtonItemDown.TabIndex = 8
        Me.ButtonItemDown.Tag = "True"
        Me.ButtonItemDown.Tooltip = "下移(D)"
        '
        'ButtonResizeFlag
        '
        Me.ButtonResizeFlag.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.ButtonResizeFlag.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonResizeFlag.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.ButtonResizeFlag.Cursor = System.Windows.Forms.Cursors.SizeWE
        Me.ButtonResizeFlag.Location = New System.Drawing.Point(316, 239)
        Me.ButtonResizeFlag.Name = "ButtonResizeFlag"
        Me.ButtonResizeFlag.Shape = New DevComponents.DotNetBar.RoundRectangleShapeDescriptor()
        Me.ButtonResizeFlag.Size = New System.Drawing.Size(10, 23)
        Me.ButtonResizeFlag.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.ButtonResizeFlag.TabIndex = 6
        Me.ButtonResizeFlag.Text = "::"
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.DarkRed
        Me.ClientSize = New System.Drawing.Size(326, 262)
        Me.Controls.Add(Me.ButtonItemDown)
        Me.Controls.Add(Me.ButtonItemUp)
        Me.Controls.Add(Me.ContextMenuBar1)
        Me.Controls.Add(Me.LabelNothing)
        Me.Controls.Add(Me.ButtonResizeFlag)
        Me.Controls.Add(Me.ListView)
        Me.Controls.Add(Me.ButtonListSetting)
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
        Me.MinimumSize = New System.Drawing.Size(136, 112)
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
    Friend WithEvents MyStyleManager As DevComponents.DotNetBar.StyleManager
    Friend WithEvents LabelFocus As System.Windows.Forms.Label
    Friend WithEvents ListView As System.Windows.Forms.ListBox
    Friend WithEvents ButtonCloseForm As DevComponents.DotNetBar.ButtonX
    Friend WithEvents ButtonListSetting As DevComponents.DotNetBar.ButtonX
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
    Friend WithEvents ListPopupMenuOpenDir As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ListPopupMenuLabelItemList As DevComponents.DotNetBar.LabelItem
    Friend WithEvents ListPopupMenuLabelItemWindow As DevComponents.DotNetBar.LabelItem
    Friend WithEvents ListPopupMenuEditItem As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ListPopupMenuViewFile As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ListPopupMenuExit As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ListPopupMenuHighLight As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ListPopupMenuViewHighLight As DevComponents.DotNetBar.ButtonItem
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
    Friend WithEvents TabItemTest As DevComponents.DotNetBar.SuperTabItem
    Friend WithEvents TabPopupMenu As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents TabPopupMenuNewTab As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents TabPopupMenuDeleteTab As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ListPopupMenuSelectAll As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents TabPopupMenuRenameTab As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ListPopupMenuMove As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents TabPopupMenuMove As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents TabPopupMenuLabel As DevComponents.DotNetBar.LabelItem
    Friend WithEvents ButtonResizeFlag As DevComponents.DotNetBar.ButtonX
    Friend WithEvents TabItemTest2 As DevComponents.DotNetBar.SuperTabItem
    Friend WithEvents ListPopupMenuItemContainer As DevComponents.DotNetBar.ItemContainer
    Friend WithEvents ListPopupMenuFold As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ListPopupMenuWinSetting As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ListPopupMenuWinHighColor As DevComponents.DotNetBar.ColorPickerDropDown
    Friend WithEvents HoverToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents ListPopupMenuFind As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents LabelNothing As System.Windows.Forms.Label
    Friend WithEvents ListPopupMenuOpenBrowser As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ListPopupMenuCopy As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ListPopupMenuSavePos As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ListPopupMenuLoadPos As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ListPopupMenuSyncData As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ListPopupMenuSyncDataTo As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ListPopupMenuSyncDataFrom As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents CmdsPopupMenu As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents CmdsPopupMenuAppend As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ListPopupMenuShotcutSetting As DevComponents.DotNetBar.ButtonItem

End Class
