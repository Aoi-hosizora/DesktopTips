<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TempForm
    Inherits BaseMainForm

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

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.m_btn_RemoveTips = New DevComponents.DotNetBar.ButtonX()
        Me.m_btn_InsertTip = New DevComponents.DotNetBar.ButtonX()
        Me.m_StyleManager = New DevComponents.DotNetBar.StyleManager(Me.components)
        Me.m_TipListBox = New DesktopTips.TipListBox()
        Me.m_btn_Exit = New DevComponents.DotNetBar.ButtonX()
        Me.m_btn_OpenListPopup = New DevComponents.DotNetBar.ButtonX()
        Me.m_num_ListCount = New System.Windows.Forms.NumericUpDown()
        Me.m_SuperTooltip = New DevComponents.DotNetBar.SuperTooltip()
        Me.m_menu_ContextMenuBar = New DevComponents.DotNetBar.ContextMenuBar()
        Me.m_menu_ListPopupMenu = New DevComponents.DotNetBar.ButtonItem()
        Me.m_popup_SelectedTipsCountLabel = New DevComponents.DotNetBar.LabelItem()
        Me.m_popup_SelectedTipsTextLabel = New DevComponents.DotNetBar.LabelItem()
        Me.m_popup_TipsCountLabel = New DevComponents.DotNetBar.LabelItem()
        Me.m_popup_ListItemContainer = New DevComponents.DotNetBar.ItemContainer()
        Me.m_popup_InsertTip = New DevComponents.DotNetBar.ButtonItem()
        Me.m_popup_RemoveTips = New DevComponents.DotNetBar.ButtonItem()
        Me.m_popup_UpdateTip = New DevComponents.DotNetBar.ButtonItem()
        Me.m_popup_MoveTipUp = New DevComponents.DotNetBar.ButtonItem()
        Me.m_popup_MoveTipDown = New DevComponents.DotNetBar.ButtonItem()
        Me.m_popup_CopyTips = New DevComponents.DotNetBar.ButtonItem()
        Me.m_popup_SelectAllTips = New DevComponents.DotNetBar.ButtonItem()
        Me.m_popup_HighlightTip = New DevComponents.DotNetBar.ButtonItem()
        Me.m_popup_MoveTopTop = New DevComponents.DotNetBar.ButtonItem()
        Me.m_popup_MoveTipBottom = New DevComponents.DotNetBar.ButtonItem()
        Me.m_popup_ViewHighlightTips = New DevComponents.DotNetBar.ButtonItem()
        Me.m_popup_FileTips = New DevComponents.DotNetBar.ButtonItem()
        Me.m_menu_MoveTipsSubMenu = New DevComponents.DotNetBar.ButtonItem()
        Me.m_menu_OtherSubMenu = New DevComponents.DotNetBar.ButtonItem()
        Me.m_popup_PasteAppendToTip = New DevComponents.DotNetBar.ButtonItem()
        Me.m_menu_FileSubMenu = New DevComponents.DotNetBar.LabelItem()
        Me.m_popup_OpenDir = New DevComponents.DotNetBar.ButtonItem()
        Me.m_popup_ViewTabList = New DevComponents.DotNetBar.ButtonItem()
        Me.m_menu_BrowserSubMenu = New DevComponents.DotNetBar.ButtonItem()
        Me.m_popup_OpenAllLinksInTips = New DevComponents.DotNetBar.ButtonItem()
        Me.m_popup_ViewAllLinksInTips = New DevComponents.DotNetBar.ButtonItem()
        Me.m_menu_SyncDataSubMenu = New DevComponents.DotNetBar.ButtonItem()
        Me.m_popup_SyncDataTo = New DevComponents.DotNetBar.ButtonItem()
        Me.m_popup_SyncDataFrom = New DevComponents.DotNetBar.ButtonItem()
        Me.m_popup_WindowLabel = New DevComponents.DotNetBar.LabelItem()
        Me.m_menu_WindowSubMenu = New DevComponents.DotNetBar.ButtonItem()
        Me.m_popup_SetupHotkey = New DevComponents.DotNetBar.ButtonItem()
        Me.m_popup_ShowSetListCount = New DevComponents.DotNetBar.ButtonItem()
        Me.m_menu_OpacitySubMenu = New DevComponents.DotNetBar.ButtonItem()
        Me.m_popup_FoldMenu = New DevComponents.DotNetBar.ButtonItem()
        Me.m_popup_TopMost = New DevComponents.DotNetBar.ButtonItem()
        Me.m_popup_HighlightColor = New DevComponents.DotNetBar.ColorPickerDropDown()
        Me.m_popup_LoadPosition = New DevComponents.DotNetBar.ButtonItem()
        Me.m_popup_SavePosition = New DevComponents.DotNetBar.ButtonItem()
        Me.m_popup_Exit = New DevComponents.DotNetBar.ButtonItem()
        Me.m_menu_TabPopupMenu = New DevComponents.DotNetBar.ButtonItem()
        Me.m_popup_TabLabel = New DevComponents.DotNetBar.LabelItem()
        Me.m_popup_NewTab = New DevComponents.DotNetBar.ButtonItem()
        Me.m_popup_DeleteTab = New DevComponents.DotNetBar.ButtonItem()
        Me.m_popup_RenameTab = New DevComponents.DotNetBar.ButtonItem()
        Me.m_menu_MoveToTabSubMenu = New DevComponents.DotNetBar.ButtonItem()
        Me.m_TabStrip = New DevComponents.DotNetBar.SuperTabStrip()
        Me.m_tab_TestTab = New DevComponents.DotNetBar.SuperTabItem()
        Me.m_tab_TestTab2 = New DevComponents.DotNetBar.SuperTabItem()
        Me.m_btn_MoveTipUp = New DevComponents.DotNetBar.ButtonX()
        Me.m_btn_MoveTipDown = New DevComponents.DotNetBar.ButtonX()
        Me.m_btn_Resize = New DevComponents.DotNetBar.ButtonX()
        CType(Me.m_num_ListCount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.m_menu_ContextMenuBar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.m_TabStrip, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'm_btn_RemoveTips
        '
        Me.m_btn_RemoveTips.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.m_btn_RemoveTips.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.m_btn_RemoveTips.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.m_btn_RemoveTips.Enabled = False
        Me.m_btn_RemoveTips.Location = New System.Drawing.Point(293, 239)
        Me.m_btn_RemoveTips.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.m_btn_RemoveTips.Name = "m_btn_RemoveTips"
        Me.m_btn_RemoveTips.Shape = New DevComponents.DotNetBar.RoundRectangleShapeDescriptor()
        Me.m_btn_RemoveTips.Size = New System.Drawing.Size(24, 23)
        Me.m_btn_RemoveTips.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.m_btn_RemoveTips.TabIndex = 5
        Me.m_btn_RemoveTips.Text = "－"
        '
        'm_btn_InsertTip
        '
        Me.m_btn_InsertTip.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.m_btn_InsertTip.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.m_btn_InsertTip.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.m_btn_InsertTip.Location = New System.Drawing.Point(270, 239)
        Me.m_btn_InsertTip.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.m_btn_InsertTip.Name = "m_btn_InsertTip"
        Me.m_btn_InsertTip.Shape = New DevComponents.DotNetBar.RoundRectangleShapeDescriptor()
        Me.m_btn_InsertTip.Size = New System.Drawing.Size(24, 23)
        Me.m_btn_InsertTip.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.m_btn_InsertTip.TabIndex = 4
        Me.m_btn_InsertTip.Text = "＋"
        '
        'm_StyleManager
        '
        Me.m_StyleManager.ManagerColorTint = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.m_StyleManager.ManagerStyle = DevComponents.DotNetBar.eStyle.Office2007Blue
        Me.m_StyleManager.MetroColorParameters = New DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.White, System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(87, Byte), Integer), CType(CType(154, Byte), Integer)))
        '
        'm_TipListBox
        '
        Me.m_TipListBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.m_TipListBox.BackColor = System.Drawing.Color.Snow
        Me.m_menu_ContextMenuBar.SetContextMenuEx(Me.m_TipListBox, Me.m_menu_ListPopupMenu)
        Me.m_TipListBox.DisplayMember = "Content"
        Me.m_TipListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable
        Me.m_TipListBox.FormattingEnabled = True
        Me.m_TipListBox.ItemHeight = 17
        Me.m_TipListBox.Location = New System.Drawing.Point(23, 0)
        Me.m_TipListBox.Name = "m_TipListBox"
        Me.m_TipListBox.OnWheeledAction = Nothing
        Me.m_TipListBox.SelectedItem = Nothing
        Me.m_TipListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.m_TipListBox.Size = New System.Drawing.Size(303, 240)
        Me.m_TipListBox.TabIndex = 0
        '
        'm_btn_Exit
        '
        Me.m_btn_Exit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.m_btn_Exit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.m_btn_Exit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.m_btn_Exit.Location = New System.Drawing.Point(0, 239)
        Me.m_btn_Exit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.m_btn_Exit.Name = "m_btn_Exit"
        Me.m_btn_Exit.Shape = New DevComponents.DotNetBar.RoundRectangleShapeDescriptor()
        Me.m_btn_Exit.Size = New System.Drawing.Size(24, 23)
        Me.m_btn_Exit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.m_SuperTooltip.SetSuperTooltip(Me.m_btn_Exit, New DevComponents.DotNetBar.SuperTooltipInfo("退出", "", "退出程序，保存数据。", Nothing, Nothing, DevComponents.DotNetBar.eTooltipColor.Gray, True, True, New System.Drawing.Size(180, 68)))
        Me.m_btn_Exit.TabIndex = 1
        Me.m_btn_Exit.Text = "×"
        '
        'm_btn_OpenListPopup
        '
        Me.m_btn_OpenListPopup.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.m_btn_OpenListPopup.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.m_btn_OpenListPopup.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.m_btn_OpenListPopup.Location = New System.Drawing.Point(23, 239)
        Me.m_btn_OpenListPopup.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.m_btn_OpenListPopup.Name = "m_btn_OpenListPopup"
        Me.m_btn_OpenListPopup.Shape = New DevComponents.DotNetBar.RoundRectangleShapeDescriptor()
        Me.m_btn_OpenListPopup.Size = New System.Drawing.Size(24, 23)
        Me.m_btn_OpenListPopup.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.m_SuperTooltip.SetSuperTooltip(Me.m_btn_OpenListPopup, New DevComponents.DotNetBar.SuperTooltipInfo("设置", "", "显示列表上下文菜单。", Nothing, Nothing, DevComponents.DotNetBar.eTooltipColor.Gray, True, True, New System.Drawing.Size(180, 68)))
        Me.m_btn_OpenListPopup.TabIndex = 2
        Me.m_btn_OpenListPopup.Text = "≡"
        '
        'm_num_ListCount
        '
        Me.m_num_ListCount.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.m_num_ListCount.Location = New System.Drawing.Point(46, 239)
        Me.m_num_ListCount.Minimum = New Decimal(New Integer() {5, 0, 0, 0})
        Me.m_num_ListCount.Name = "m_num_ListCount"
        Me.m_num_ListCount.Size = New System.Drawing.Size(35, 23)
        Me.m_num_ListCount.TabIndex = 3
        Me.m_num_ListCount.Value = New Decimal(New Integer() {8, 0, 0, 0})
        Me.m_num_ListCount.Visible = False
        '
        'm_SuperTooltip
        '
        Me.m_SuperTooltip.DefaultFont = New System.Drawing.Font("Microsoft YaHei UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        '
        'm_menu_ContextMenuBar
        '
        Me.m_menu_ContextMenuBar.AntiAlias = True
        Me.m_menu_ContextMenuBar.Font = New System.Drawing.Font("Yu Gothic UI", 9.0!)
        Me.m_menu_ContextMenuBar.Items.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.m_menu_ListPopupMenu, Me.m_menu_TabPopupMenu})
        Me.m_menu_ContextMenuBar.Location = New System.Drawing.Point(46, 40)
        Me.m_menu_ContextMenuBar.Name = "m_menu_ContextMenuBar"
        Me.m_menu_ContextMenuBar.Size = New System.Drawing.Size(240, 27)
        Me.m_menu_ContextMenuBar.Stretch = True
        Me.m_menu_ContextMenuBar.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.m_menu_ContextMenuBar.TabIndex = 4
        Me.m_menu_ContextMenuBar.TabStop = False
        '
        'm_menu_ListPopupMenu
        '
        Me.m_menu_ListPopupMenu.AutoExpandOnClick = True
        Me.m_menu_ListPopupMenu.Name = "m_menu_ListPopupMenu"
        Me.m_menu_ListPopupMenu.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.m_popup_SelectedTipsCountLabel, Me.m_popup_SelectedTipsTextLabel, Me.m_popup_TipsCountLabel, Me.m_popup_ListItemContainer, Me.m_popup_MoveTopTop, Me.m_popup_MoveTipBottom, Me.m_popup_ViewHighlightTips, Me.m_popup_FileTips, Me.m_menu_MoveTipsSubMenu, Me.m_menu_OtherSubMenu, Me.m_menu_FileSubMenu, Me.m_popup_OpenDir, Me.m_popup_ViewTabList, Me.m_menu_BrowserSubMenu, Me.m_menu_SyncDataSubMenu, Me.m_popup_WindowLabel, Me.m_menu_WindowSubMenu, Me.m_popup_Exit})
        Me.m_menu_ListPopupMenu.Text = "ListPopup"
        '
        'm_popup_SelectedTipsCountLabel
        '
        Me.m_popup_SelectedTipsCountLabel.BackColor = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.m_popup_SelectedTipsCountLabel.BorderSide = DevComponents.DotNetBar.eBorderSide.Bottom
        Me.m_popup_SelectedTipsCountLabel.BorderType = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.m_popup_SelectedTipsCountLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(21, Byte), Integer), CType(CType(110, Byte), Integer))
        Me.m_popup_SelectedTipsCountLabel.Name = "m_popup_SelectedTipsCountLabel"
        Me.m_popup_SelectedTipsCountLabel.PaddingBottom = 1
        Me.m_popup_SelectedTipsCountLabel.PaddingLeft = 10
        Me.m_popup_SelectedTipsCountLabel.PaddingTop = 1
        Me.m_popup_SelectedTipsCountLabel.SingleLineColor = System.Drawing.Color.FromArgb(CType(CType(197, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(197, Byte), Integer))
        Me.m_popup_SelectedTipsCountLabel.Text = "当前选中 (共 0 项)"
        '
        'm_popup_SelectedTipsTextLabel
        '
        Me.m_popup_SelectedTipsTextLabel.BackColor = System.Drawing.Color.Transparent
        Me.m_popup_SelectedTipsTextLabel.BorderSide = DevComponents.DotNetBar.eBorderSide.None
        Me.m_popup_SelectedTipsTextLabel.BorderType = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.m_popup_SelectedTipsTextLabel.EnableMarkup = False
        Me.m_popup_SelectedTipsTextLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(21, Byte), Integer), CType(CType(110, Byte), Integer))
        Me.m_popup_SelectedTipsTextLabel.Name = "m_popup_SelectedTipsTextLabel"
        Me.m_popup_SelectedTipsTextLabel.PaddingLeft = 5
        Me.m_popup_SelectedTipsTextLabel.PaddingRight = 5
        Me.m_popup_SelectedTipsTextLabel.SingleLineColor = System.Drawing.Color.FromArgb(CType(CType(197, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(197, Byte), Integer))
        Me.m_popup_SelectedTipsTextLabel.Text = "https://www.youtube.com/watch?v=wb94Z3Ck_uU&t=1015s"
        Me.m_popup_SelectedTipsTextLabel.Width = 185
        Me.m_popup_SelectedTipsTextLabel.WordWrap = True
        '
        'm_popup_TipsCountLabel
        '
        Me.m_popup_TipsCountLabel.BackColor = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.m_popup_TipsCountLabel.BeginGroup = True
        Me.m_popup_TipsCountLabel.BorderSide = DevComponents.DotNetBar.eBorderSide.Bottom
        Me.m_popup_TipsCountLabel.BorderType = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.m_popup_TipsCountLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(21, Byte), Integer), CType(CType(110, Byte), Integer))
        Me.m_popup_TipsCountLabel.Name = "m_popup_TipsCountLabel"
        Me.m_popup_TipsCountLabel.PaddingBottom = 1
        Me.m_popup_TipsCountLabel.PaddingLeft = 10
        Me.m_popup_TipsCountLabel.PaddingTop = 1
        Me.m_popup_TipsCountLabel.SingleLineColor = System.Drawing.Color.FromArgb(CType(CType(197, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(197, Byte), Integer))
        Me.m_popup_TipsCountLabel.Text = "列表 (共 0 项，高亮 0 项)"
        '
        'm_popup_ListItemContainer
        '
        '
        '
        '
        Me.m_popup_ListItemContainer.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.m_popup_ListItemContainer.HorizontalItemAlignment = DevComponents.DotNetBar.eHorizontalItemsAlignment.Center
        Me.m_popup_ListItemContainer.ItemSpacing = -1
        Me.m_popup_ListItemContainer.Name = "m_popup_ListItemContainer"
        Me.m_popup_ListItemContainer.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.m_popup_InsertTip, Me.m_popup_RemoveTips, Me.m_popup_UpdateTip, Me.m_popup_MoveTipUp, Me.m_popup_MoveTipDown, Me.m_popup_CopyTips, Me.m_popup_SelectAllTips, Me.m_popup_HighlightTip})
        '
        '
        '
        Me.m_popup_ListItemContainer.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        'm_popup_InsertTip
        '
        Me.m_popup_InsertTip.BeginGroup = True
        Me.m_popup_InsertTip.Image = Global.DesktopTips.My.Resources.Resources.Plus
        Me.m_popup_InsertTip.Name = "m_popup_InsertTip"
        Me.m_popup_InsertTip.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlN)
        Me.m_popup_InsertTip.Text = "添加(&N)"
        Me.m_popup_InsertTip.Tooltip = "添加"
        '
        'm_popup_RemoveTips
        '
        Me.m_popup_RemoveTips.Image = Global.DesktopTips.My.Resources.Resources.Minus
        Me.m_popup_RemoveTips.Name = "m_popup_RemoveTips"
        Me.m_popup_RemoveTips.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.Del)
        Me.m_popup_RemoveTips.Text = "删除(&X)"
        Me.m_popup_RemoveTips.Tooltip = "删除"
        '
        'm_popup_UpdateTip
        '
        Me.m_popup_UpdateTip.Image = Global.DesktopTips.My.Resources.Resources.Edit
        Me.m_popup_UpdateTip.Name = "m_popup_UpdateTip"
        Me.m_popup_UpdateTip.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.F2)
        Me.m_popup_UpdateTip.Text = "编辑(&E)"
        Me.m_popup_UpdateTip.Tooltip = "编辑"
        '
        'm_popup_MoveTipUp
        '
        Me.m_popup_MoveTipUp.BeginGroup = True
        Me.m_popup_MoveTipUp.Image = Global.DesktopTips.My.Resources.Resources.Up
        Me.m_popup_MoveTipUp.Name = "m_popup_MoveTipUp"
        Me.m_popup_MoveTipUp.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlU)
        Me.m_popup_MoveTipUp.Text = "上移(&U)"
        Me.m_popup_MoveTipUp.Tooltip = "上移"
        '
        'm_popup_MoveTipDown
        '
        Me.m_popup_MoveTipDown.Image = Global.DesktopTips.My.Resources.Resources.Down
        Me.m_popup_MoveTipDown.Name = "m_popup_MoveTipDown"
        Me.m_popup_MoveTipDown.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlD)
        Me.m_popup_MoveTipDown.Text = "下移(&D)"
        Me.m_popup_MoveTipDown.Tooltip = "下移"
        '
        'm_popup_CopyTips
        '
        Me.m_popup_CopyTips.BeginGroup = True
        Me.m_popup_CopyTips.Image = Global.DesktopTips.My.Resources.Resources.Copy
        Me.m_popup_CopyTips.Name = "m_popup_CopyTips"
        Me.m_popup_CopyTips.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlC)
        Me.m_popup_CopyTips.Text = "复制(&C)"
        '
        'm_popup_SelectAllTips
        '
        Me.m_popup_SelectAllTips.Image = Global.DesktopTips.My.Resources.Resources.SelectAll
        Me.m_popup_SelectAllTips.Name = "m_popup_SelectAllTips"
        Me.m_popup_SelectAllTips.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlA)
        Me.m_popup_SelectAllTips.Text = "全选(&A)"
        Me.m_popup_SelectAllTips.Tooltip = "全选"
        '
        'm_popup_HighlightTip
        '
        Me.m_popup_HighlightTip.Image = Global.DesktopTips.My.Resources.Resources.Star
        Me.m_popup_HighlightTip.Name = "m_popup_HighlightTip"
        Me.m_popup_HighlightTip.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlH)
        Me.m_popup_HighlightTip.Text = "高亮(&H)"
        Me.m_popup_HighlightTip.Tooltip = "高亮"
        '
        'm_popup_MoveTopTop
        '
        Me.m_popup_MoveTopTop.BeginGroup = True
        Me.m_popup_MoveTopTop.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far
        Me.m_popup_MoveTopTop.Name = "m_popup_MoveTopTop"
        Me.m_popup_MoveTopTop.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlT)
        Me.m_popup_MoveTopTop.Text = "移至顶部(&T)"
        '
        'm_popup_MoveTipBottom
        '
        Me.m_popup_MoveTipBottom.Name = "m_popup_MoveTipBottom"
        Me.m_popup_MoveTipBottom.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlB)
        Me.m_popup_MoveTipBottom.Text = "移至底部(&B)"
        '
        'm_popup_ViewHighlightTips
        '
        Me.m_popup_ViewHighlightTips.Name = "m_popup_ViewHighlightTips"
        Me.m_popup_ViewHighlightTips.Text = "浏览所有高亮(&L)"
        '
        'm_popup_FileTips
        '
        Me.m_popup_FileTips.BeginGroup = True
        Me.m_popup_FileTips.Image = Global.DesktopTips.My.Resources.Resources.Find
        Me.m_popup_FileTips.Name = "m_popup_FileTips"
        Me.m_popup_FileTips.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlF)
        Me.m_popup_FileTips.Text = "查找(&F)"
        '
        'm_menu_MoveTipsSubMenu
        '
        Me.m_menu_MoveTipsSubMenu.Image = Global.DesktopTips.My.Resources.Resources.Right
        Me.m_menu_MoveTipsSubMenu.Name = "m_menu_MoveTipsSubMenu"
        Me.m_menu_MoveTipsSubMenu.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlM)
        Me.m_menu_MoveTipsSubMenu.Text = "移动至(&M)"
        '
        'm_menu_OtherSubMenu
        '
        Me.m_menu_OtherSubMenu.BeginGroup = True
        Me.m_menu_OtherSubMenu.Name = "m_menu_OtherSubMenu"
        Me.m_menu_OtherSubMenu.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.m_popup_PasteAppendToTip})
        Me.m_menu_OtherSubMenu.Text = "其他(&E)"
        '
        'm_popup_PasteAppendToTip
        '
        Me.m_popup_PasteAppendToTip.Image = Global.DesktopTips.My.Resources.Resources.PasteHS
        Me.m_popup_PasteAppendToTip.Name = "m_popup_PasteAppendToTip"
        Me.m_popup_PasteAppendToTip.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlV)
        Me.m_popup_PasteAppendToTip.Text = "粘贴附加(&P)"
        '
        'm_menu_FileSubMenu
        '
        Me.m_menu_FileSubMenu.BackColor = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.m_menu_FileSubMenu.BorderSide = DevComponents.DotNetBar.eBorderSide.Bottom
        Me.m_menu_FileSubMenu.BorderType = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.m_menu_FileSubMenu.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(21, Byte), Integer), CType(CType(110, Byte), Integer))
        Me.m_menu_FileSubMenu.Name = "m_menu_FileSubMenu"
        Me.m_menu_FileSubMenu.PaddingBottom = 1
        Me.m_menu_FileSubMenu.PaddingLeft = 10
        Me.m_menu_FileSubMenu.PaddingTop = 1
        Me.m_menu_FileSubMenu.SingleLineColor = System.Drawing.Color.FromArgb(CType(CType(197, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(197, Byte), Integer))
        Me.m_menu_FileSubMenu.Text = "文件"
        '
        'm_popup_OpenDir
        '
        Me.m_popup_OpenDir.Name = "m_popup_OpenDir"
        Me.m_popup_OpenDir.Text = "打开文件所在位置(&O)"
        '
        'm_popup_ViewTabList
        '
        Me.m_popup_ViewTabList.Name = "m_popup_ViewTabList"
        Me.m_popup_ViewTabList.Text = "浏览当前列表内容(&V)"
        '
        'm_menu_BrowserSubMenu
        '
        Me.m_menu_BrowserSubMenu.Name = "m_menu_BrowserSubMenu"
        Me.m_menu_BrowserSubMenu.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.m_popup_OpenAllLinksInTips, Me.m_popup_ViewAllLinksInTips})
        Me.m_menu_BrowserSubMenu.Text = "浏览器(&B)"
        '
        'm_popup_OpenAllLinksInTips
        '
        Me.m_popup_OpenAllLinksInTips.Name = "m_popup_OpenAllLinksInTips"
        Me.m_popup_OpenAllLinksInTips.Text = "打开所有链接(&A)"
        '
        'm_popup_ViewAllLinksInTips
        '
        Me.m_popup_ViewAllLinksInTips.Name = "m_popup_ViewAllLinksInTips"
        Me.m_popup_ViewAllLinksInTips.Text = "打开部分链接(&P)"
        '
        'm_menu_SyncDataSubMenu
        '
        Me.m_menu_SyncDataSubMenu.BeginGroup = True
        Me.m_menu_SyncDataSubMenu.Name = "m_menu_SyncDataSubMenu"
        Me.m_menu_SyncDataSubMenu.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.m_popup_SyncDataTo, Me.m_popup_SyncDataFrom})
        Me.m_menu_SyncDataSubMenu.Text = "数据同步(&Y)"
        '
        'm_popup_SyncDataTo
        '
        Me.m_popup_SyncDataTo.Name = "m_popup_SyncDataTo"
        Me.m_popup_SyncDataTo.Text = "同步到移动端(&T)"
        '
        'm_popup_SyncDataFrom
        '
        Me.m_popup_SyncDataFrom.Name = "m_popup_SyncDataFrom"
        Me.m_popup_SyncDataFrom.Text = "从移动端同步(&F)"
        '
        'm_popup_WindowLabel
        '
        Me.m_popup_WindowLabel.BackColor = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.m_popup_WindowLabel.BorderSide = DevComponents.DotNetBar.eBorderSide.Bottom
        Me.m_popup_WindowLabel.BorderType = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.m_popup_WindowLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(21, Byte), Integer), CType(CType(110, Byte), Integer))
        Me.m_popup_WindowLabel.Name = "m_popup_WindowLabel"
        Me.m_popup_WindowLabel.PaddingBottom = 1
        Me.m_popup_WindowLabel.PaddingLeft = 10
        Me.m_popup_WindowLabel.PaddingTop = 1
        Me.m_popup_WindowLabel.SingleLineColor = System.Drawing.Color.FromArgb(CType(CType(197, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(197, Byte), Integer))
        Me.m_popup_WindowLabel.Text = "窗口"
        '
        'm_menu_WindowSubMenu
        '
        Me.m_menu_WindowSubMenu.Name = "m_menu_WindowSubMenu"
        Me.m_menu_WindowSubMenu.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.m_popup_SetupHotkey, Me.m_popup_ShowSetListCount, Me.m_menu_OpacitySubMenu, Me.m_popup_FoldMenu, Me.m_popup_TopMost, Me.m_popup_HighlightColor, Me.m_popup_LoadPosition, Me.m_popup_SavePosition})
        Me.m_menu_WindowSubMenu.Text = "显示(&S)"
        '
        'm_popup_SetupHotkey
        '
        Me.m_popup_SetupHotkey.Name = "m_popup_SetupHotkey"
        Me.m_popup_SetupHotkey.Text = "快捷键设置(&R)"
        '
        'm_popup_ShowSetListCount
        '
        Me.m_popup_ShowSetListCount.Name = "m_popup_ShowSetListCount"
        Me.m_popup_ShowSetListCount.Text = "列表高度设置(&E)"
        '
        'm_menu_OpacitySubMenu
        '
        Me.m_menu_OpacitySubMenu.BeginGroup = True
        Me.m_menu_OpacitySubMenu.Name = "m_menu_OpacitySubMenu"
        Me.m_menu_OpacitySubMenu.Text = "不透明度(&P)"
        '
        'm_popup_FoldMenu
        '
        Me.m_popup_FoldMenu.Name = "m_popup_Fold"
        Me.m_popup_FoldMenu.Text = "折叠菜单(&F)"
        '
        'm_popup_TopMost
        '
        Me.m_popup_TopMost.Name = "m_popup_TopMost"
        Me.m_popup_TopMost.Text = "窗口置顶(&W)"
        '
        'm_popup_HighlightColor
        '
        Me.m_popup_HighlightColor.Image = Global.DesktopTips.My.Resources.Resources.HighLightColor
        Me.m_popup_HighlightColor.Name = "m_popup_HighlightColor"
        Me.m_popup_HighlightColor.Text = "高亮颜色(&C)"
        '
        'm_popup_LoadPosition
        '
        Me.m_popup_LoadPosition.BeginGroup = True
        Me.m_popup_LoadPosition.Name = "m_popup_LoadPosition"
        Me.m_popup_LoadPosition.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlR)
        Me.m_popup_LoadPosition.Text = "恢复位置(&L)"
        '
        'm_popup_SavePosition
        '
        Me.m_popup_SavePosition.Name = "m_popup_SavePosition"
        Me.m_popup_SavePosition.Text = "保存当前位置(&S)"
        '
        'm_popup_Exit
        '
        Me.m_popup_Exit.Name = "m_popup_Exit"
        Me.m_popup_Exit.Text = "退出(&E)"
        '
        'm_menu_TabPopupMenu
        '
        Me.m_menu_TabPopupMenu.AutoExpandOnClick = True
        Me.m_menu_TabPopupMenu.Name = "m_menu_TabPopupMenu"
        Me.m_menu_TabPopupMenu.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.m_popup_TabLabel, Me.m_popup_NewTab, Me.m_popup_DeleteTab, Me.m_popup_RenameTab, Me.m_menu_MoveToTabSubMenu})
        Me.m_menu_TabPopupMenu.Text = "TabPopup"
        '
        'm_popup_TabLabel
        '
        Me.m_popup_TabLabel.BackColor = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.m_popup_TabLabel.BorderSide = DevComponents.DotNetBar.eBorderSide.Bottom
        Me.m_popup_TabLabel.BorderType = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.m_popup_TabLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(21, Byte), Integer), CType(CType(110, Byte), Integer))
        Me.m_popup_TabLabel.Name = "m_popup_TabLabel"
        Me.m_popup_TabLabel.PaddingBottom = 1
        Me.m_popup_TabLabel.PaddingLeft = 10
        Me.m_popup_TabLabel.PaddingTop = 1
        Me.m_popup_TabLabel.SingleLineColor = System.Drawing.Color.FromArgb(CType(CType(197, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(197, Byte), Integer))
        Me.m_popup_TabLabel.Text = "分组 (共 0 组)"
        '
        'm_popup_NewTab
        '
        Me.m_popup_NewTab.Image = Global.DesktopTips.My.Resources.Resources.Plus
        Me.m_popup_NewTab.Name = "m_popup_NewTab"
        Me.m_popup_NewTab.Text = "新建分组(&N)"
        '
        'm_popup_DeleteTab
        '
        Me.m_popup_DeleteTab.Image = Global.DesktopTips.My.Resources.Resources.Minus
        Me.m_popup_DeleteTab.Name = "m_popup_DeleteTab"
        Me.m_popup_DeleteTab.Text = "删除分组(&D)"
        '
        'm_popup_RenameTab
        '
        Me.m_popup_RenameTab.Image = Global.DesktopTips.My.Resources.Resources.Edit
        Me.m_popup_RenameTab.Name = "m_popup_RenameTab"
        Me.m_popup_RenameTab.Text = "重命名(&R)"
        '
        'm_menu_MoveToTabSubMenu
        '
        Me.m_menu_MoveToTabSubMenu.BeginGroup = True
        Me.m_menu_MoveToTabSubMenu.Image = Global.DesktopTips.My.Resources.Resources.Right
        Me.m_menu_MoveToTabSubMenu.Name = "m_menu_MoveToTabSubMenu"
        Me.m_menu_MoveToTabSubMenu.Text = "移动至分组(&M)"
        '
        'm_TabStrip
        '
        Me.m_TabStrip.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.m_TabStrip.AutoSelectAttachedControl = False
        Me.m_TabStrip.BackColor = System.Drawing.Color.DarkRed
        '
        '
        '
        Me.m_TabStrip.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.m_TabStrip.ContainerControlProcessDialogKey = True
        Me.m_menu_ContextMenuBar.SetContextMenuEx(Me.m_TabStrip, Me.m_menu_TabPopupMenu)
        '
        '
        '
        '
        '
        '
        Me.m_TabStrip.ControlBox.CloseBox.Name = ""
        '
        '
        '
        Me.m_TabStrip.ControlBox.MenuBox.Name = ""
        Me.m_TabStrip.ControlBox.Name = ""
        Me.m_TabStrip.ControlBox.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.m_TabStrip.ControlBox.MenuBox, Me.m_TabStrip.ControlBox.CloseBox})
        Me.m_TabStrip.HorizontalText = False
        Me.m_TabStrip.Location = New System.Drawing.Point(0, 0)
        Me.m_TabStrip.Name = "m_TabStrip"
        Me.m_TabStrip.ReorderTabsEnabled = True
        Me.m_TabStrip.RotateVerticalText = True
        Me.m_TabStrip.SelectedTabFont = New System.Drawing.Font("Microsoft YaHei UI Light", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.m_TabStrip.SelectedTabIndex = 0
        Me.m_TabStrip.Size = New System.Drawing.Size(25, 239)
        Me.m_TabStrip.TabAlignment = DevComponents.DotNetBar.eTabStripAlignment.Left
        Me.m_TabStrip.TabCloseButtonHot = Nothing
        Me.m_TabStrip.TabFont = New System.Drawing.Font("Microsoft YaHei UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.m_TabStrip.TabHorizontalSpacing = 2
        Me.m_TabStrip.TabIndex = 9
        Me.m_TabStrip.Tabs.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.m_tab_TestTab, Me.m_tab_TestTab2})
        Me.m_TabStrip.TabStyle = DevComponents.DotNetBar.eSuperTabStyle.VisualStudio2008Dock
        Me.m_TabStrip.TabVerticalSpacing = 3
        Me.m_TabStrip.Text = "m_TabStrip"
        Me.m_TabStrip.TextAlignment = DevComponents.DotNetBar.eItemAlignment.Center
        '
        'm_tab_TestTab
        '
        Me.m_tab_TestTab.GlobalItem = False
        Me.m_tab_TestTab.Name = "m_tab_TestTab"
        Me.m_tab_TestTab.Text = "Test"
        Me.m_tab_TestTab.Visible = False
        '
        'm_tab_TestTab2
        '
        Me.m_tab_TestTab2.GlobalItem = False
        Me.m_tab_TestTab2.Name = "m_tab_TestTab2"
        Me.m_tab_TestTab2.Text = "Test"
        Me.m_tab_TestTab2.Visible = False
        '
        'm_btn_MoveTipUp
        '
        Me.m_btn_MoveTipUp.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.m_btn_MoveTipUp.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.m_btn_MoveTipUp.Font = New System.Drawing.Font("Yu Gothic UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.m_btn_MoveTipUp.Image = Global.DesktopTips.My.Resources.Resources.UpIcon
        Me.m_btn_MoveTipUp.Location = New System.Drawing.Point(139, 144)
        Me.m_btn_MoveTipUp.Name = "m_btn_MoveTipUp"
        Me.m_btn_MoveTipUp.Shape = New DevComponents.DotNetBar.RoundRectangleShapeDescriptor()
        Me.m_btn_MoveTipUp.Size = New System.Drawing.Size(17, 9)
        Me.m_btn_MoveTipUp.TabIndex = 7
        Me.m_btn_MoveTipUp.Tag = "True"
        Me.m_btn_MoveTipUp.Tooltip = "上移(U)"
        Me.m_btn_MoveTipUp.Visible = False
        '
        'm_btn_MoveTipDown
        '
        Me.m_btn_MoveTipDown.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.m_btn_MoveTipDown.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.m_btn_MoveTipDown.Font = New System.Drawing.Font("Yu Gothic UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.m_btn_MoveTipDown.Image = Global.DesktopTips.My.Resources.Resources.DownIcon
        Me.m_btn_MoveTipDown.Location = New System.Drawing.Point(139, 152)
        Me.m_btn_MoveTipDown.Name = "m_btn_MoveTipDown"
        Me.m_btn_MoveTipDown.Shape = New DevComponents.DotNetBar.RoundRectangleShapeDescriptor()
        Me.m_btn_MoveTipDown.Size = New System.Drawing.Size(17, 9)
        Me.m_btn_MoveTipDown.TabIndex = 8
        Me.m_btn_MoveTipDown.Tag = "True"
        Me.m_btn_MoveTipDown.Tooltip = "下移(D)"
        Me.m_btn_MoveTipDown.Visible = False
        '
        'm_btn_Resize
        '
        Me.m_btn_Resize.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.m_btn_Resize.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.m_btn_Resize.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.m_btn_Resize.Cursor = System.Windows.Forms.Cursors.SizeWE
        Me.m_btn_Resize.Location = New System.Drawing.Point(316, 239)
        Me.m_btn_Resize.Name = "m_btn_Resize"
        Me.m_btn_Resize.Shape = New DevComponents.DotNetBar.RoundRectangleShapeDescriptor()
        Me.m_btn_Resize.Size = New System.Drawing.Size(10, 23)
        Me.m_btn_Resize.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.m_btn_Resize.TabIndex = 6
        Me.m_btn_Resize.Text = "::"
        '
        'TempForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.DarkRed
        Me.ClientSize = New System.Drawing.Size(326, 262)
        Me.Controls.Add(Me.m_btn_MoveTipDown)
        Me.Controls.Add(Me.m_btn_MoveTipUp)
        Me.Controls.Add(Me.m_menu_ContextMenuBar)
        Me.Controls.Add(Me.m_btn_Resize)
        Me.Controls.Add(Me.m_TipListBox)
        Me.Controls.Add(Me.m_btn_OpenListPopup)
        Me.Controls.Add(Me.m_btn_Exit)
        Me.Controls.Add(Me.m_btn_InsertTip)
        Me.Controls.Add(Me.m_btn_RemoveTips)
        Me.Controls.Add(Me.m_num_ListCount)
        Me.Controls.Add(Me.m_TabStrip)
        Me.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(136, 112)
        Me.Name = "TempForm"
        Me.Opacity = 0.0R
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Text = "DesktopTips"
        Me.TransparencyKey = System.Drawing.Color.DarkRed
        CType(Me.m_num_ListCount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.m_menu_ContextMenuBar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.m_TabStrip, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents m_btn_RemoveTips As DevComponents.DotNetBar.ButtonX
    Friend WithEvents m_btn_InsertTip As DevComponents.DotNetBar.ButtonX
    Friend WithEvents m_StyleManager As DevComponents.DotNetBar.StyleManager
    Friend WithEvents m_TipListBox As TipListBox
    Friend WithEvents m_btn_Exit As DevComponents.DotNetBar.ButtonX
    Friend WithEvents m_btn_OpenListPopup As DevComponents.DotNetBar.ButtonX
    Friend WithEvents m_num_ListCount As System.Windows.Forms.NumericUpDown
    Friend WithEvents m_SuperTooltip As DevComponents.DotNetBar.SuperTooltip
    Friend WithEvents m_menu_ContextMenuBar As DevComponents.DotNetBar.ContextMenuBar
    Friend WithEvents m_menu_ListPopupMenu As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents m_popup_MoveTipUp As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents m_popup_MoveTipDown As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents m_popup_InsertTip As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents m_popup_RemoveTips As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents m_popup_TopMost As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents m_popup_OpenDir As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents m_popup_TipsCountLabel As DevComponents.DotNetBar.LabelItem
    Friend WithEvents m_popup_WindowLabel As DevComponents.DotNetBar.LabelItem
    Friend WithEvents m_popup_UpdateTip As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents m_popup_ViewTabList As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents m_popup_Exit As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents m_popup_HighlightTip As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents m_popup_ViewHighlightTips As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents m_menu_OpacitySubMenu As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents m_menu_FileSubMenu As DevComponents.DotNetBar.LabelItem
    Friend WithEvents m_popup_ShowSetListCount As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents m_btn_MoveTipUp As DevComponents.DotNetBar.ButtonX
    Friend WithEvents m_btn_MoveTipDown As DevComponents.DotNetBar.ButtonX
    Friend WithEvents m_popup_SelectedTipsCountLabel As DevComponents.DotNetBar.LabelItem
    Friend WithEvents m_popup_SelectedTipsTextLabel As DevComponents.DotNetBar.LabelItem
    Friend WithEvents m_popup_MoveTipBottom As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents m_popup_MoveTopTop As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents m_TabStrip As DevComponents.DotNetBar.SuperTabStrip
    Friend WithEvents m_tab_TestTab As DevComponents.DotNetBar.SuperTabItem
    Friend WithEvents m_menu_TabPopupMenu As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents m_popup_NewTab As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents m_popup_DeleteTab As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents m_popup_SelectAllTips As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents m_popup_RenameTab As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents m_menu_MoveTipsSubMenu As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents m_menu_MoveToTabSubMenu As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents m_popup_TabLabel As DevComponents.DotNetBar.LabelItem
    Friend WithEvents m_btn_Resize As DevComponents.DotNetBar.ButtonX
    Friend WithEvents m_tab_TestTab2 As DevComponents.DotNetBar.SuperTabItem
    Friend WithEvents m_popup_ListItemContainer As DevComponents.DotNetBar.ItemContainer
    Friend WithEvents m_popup_FoldMenu As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents m_menu_WindowSubMenu As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents m_popup_HighlightColor As DevComponents.DotNetBar.ColorPickerDropDown
    Friend WithEvents m_popup_FileTips As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents m_menu_BrowserSubMenu As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents m_popup_CopyTips As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents m_popup_SavePosition As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents m_popup_LoadPosition As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents m_menu_SyncDataSubMenu As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents m_popup_SyncDataTo As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents m_popup_SyncDataFrom As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents m_popup_PasteAppendToTip As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents m_menu_OtherSubMenu As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents m_popup_OpenAllLinksInTips As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents m_popup_ViewAllLinksInTips As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents m_popup_SetupHotkey As DevComponents.DotNetBar.ButtonItem

End Class
