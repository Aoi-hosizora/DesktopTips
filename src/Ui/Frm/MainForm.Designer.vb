﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MainForm
    Inherits BaseMainForm

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        Me.m_btn_RemoveTips = New DevComponents.DotNetBar.ButtonX()
        Me.m_btn_InsertTip = New DevComponents.DotNetBar.ButtonX()
        Me.m_StyleManager = New DevComponents.DotNetBar.StyleManager(Me.components)
        Me.m_ListView = New DesktopTips.TipListBox()
        Me.m_btn_Exit = New DevComponents.DotNetBar.ButtonX()
        Me.m_btn_OpenListPopup = New DevComponents.DotNetBar.ButtonX()
        Me.m_SuperTooltip = New DevComponents.DotNetBar.SuperTooltip()
        Me.m_btn_Resize = New DevComponents.DotNetBar.ButtonX()
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
        Me.m_popup_PasteAppendToTip = New DevComponents.DotNetBar.ButtonItem()
        Me.m_popup_SelectAllTips = New DevComponents.DotNetBar.ButtonItem()
        Me.m_popup_MoveTipTop = New DevComponents.DotNetBar.ButtonItem()
        Me.m_popup_MoveTipBottom = New DevComponents.DotNetBar.ButtonItem()
        Me.m_popup_MoveTipTo = New DevComponents.DotNetBar.ButtonItem()
        Me.m_menu_HighlightSubMenu = New DevComponents.DotNetBar.ButtonItem()
        Me.m_popup_SetupColors = New DevComponents.DotNetBar.ButtonItem()
        Me.m_popup_CheckDone = New DevComponents.DotNetBar.ButtonItem()
        Me.m_popup_ShowHighlightList = New DevComponents.DotNetBar.ButtonItem()
        Me.m_popup_ViewLinksInTips = New DevComponents.DotNetBar.ButtonItem()
        Me.m_popup_FileTips = New DevComponents.DotNetBar.ButtonItem()
        Me.m_menu_MoveTipsSubMenu = New DevComponents.DotNetBar.ButtonItem()
        Me.m_popup_OtherLabel = New DevComponents.DotNetBar.LabelItem()
        Me.m_popup_OpenDir = New DevComponents.DotNetBar.ButtonItem()
        Me.m_popup_Refresh = New DevComponents.DotNetBar.ButtonItem()
        Me.m_menu_WindowSubMenu = New DevComponents.DotNetBar.ButtonItem()
        Me.m_popup_SetupHotkey = New DevComponents.DotNetBar.ButtonItem()
        Me.m_menu_OpacitySubMenu = New DevComponents.DotNetBar.ButtonItem()
        Me.m_popup_TopMost = New DevComponents.DotNetBar.ButtonItem()
        Me.m_popup_NotifyIcon = New DevComponents.DotNetBar.ButtonItem()
        Me.m_popup_LoadPosition = New DevComponents.DotNetBar.ButtonItem()
        Me.m_popup_SavePosition = New DevComponents.DotNetBar.ButtonItem()
        Me.m_popup_ClearPosition = New DevComponents.DotNetBar.ButtonItem()
        Me.m_popup_Exit = New DevComponents.DotNetBar.ButtonItem()
        Me.m_menu_TabPopupMenu = New DevComponents.DotNetBar.ButtonItem()
        Me.m_popup_CurrentTabLabel = New DevComponents.DotNetBar.LabelItem()
        Me.m_popup_CurrentTabTextLabel = New DevComponents.DotNetBar.LabelItem()
        Me.m_popup_TabCountLabel = New DevComponents.DotNetBar.LabelItem()
        Me.m_popup_NewTab = New DevComponents.DotNetBar.ButtonItem()
        Me.m_popup_DeleteTab = New DevComponents.DotNetBar.ButtonItem()
        Me.m_popup_RenameTab = New DevComponents.DotNetBar.ButtonItem()
        Me.m_menu_MoveToTabSubMenu = New DevComponents.DotNetBar.ButtonItem()
        Me.m_menu_IconPopupMenu = New DevComponents.DotNetBar.ButtonItem()
        Me.m_popup_IconShow = New DevComponents.DotNetBar.ButtonItem()
        Me.m_popup_IconExit = New DevComponents.DotNetBar.ButtonItem()
        Me.m_TabView = New DesktopTips.TabView()
        Me.m_btn_MoveTipUp = New DevComponents.DotNetBar.ButtonX()
        Me.m_btn_MoveTipDown = New DevComponents.DotNetBar.ButtonX()
        Me.m_NotifyIcon = New System.Windows.Forms.NotifyIcon(Me.components)
        CType(Me.m_menu_ContextMenuBar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.m_TabView, System.ComponentModel.ISupportInitialize).BeginInit()
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
        'm_ListView
        '
        Me.m_ListView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.m_ListView.BackColor = System.Drawing.Color.Snow
        Me.m_menu_ContextMenuBar.SetContextMenuEx(Me.m_ListView, Me.m_menu_ListPopupMenu)
        Me.m_ListView.DisplayMember = "Content"
        Me.m_ListView.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable
        Me.m_ListView.FormattingEnabled = True
        Me.m_ListView.ItemHeight = 17
        Me.m_ListView.Location = New System.Drawing.Point(23, 0)
        Me.m_ListView.Name = "m_ListView"
        Me.m_ListView.SelectedItem = Nothing
        Me.m_ListView.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.m_ListView.Size = New System.Drawing.Size(303, 240)
        Me.m_ListView.TabIndex = 0
        Me.m_ListView.WheeledFunc = Nothing
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
        'm_SuperTooltip
        '
        Me.m_SuperTooltip.DefaultFont = New System.Drawing.Font("Microsoft YaHei UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        '
        'm_btn_Resize
        '
        Me.m_btn_Resize.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.m_btn_Resize.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.m_btn_Resize.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.m_btn_Resize.Cursor = System.Windows.Forms.Cursors.SizeAll
        Me.m_btn_Resize.Location = New System.Drawing.Point(316, 239)
        Me.m_btn_Resize.Name = "m_btn_Resize"
        Me.m_btn_Resize.Shape = New DevComponents.DotNetBar.RoundRectangleShapeDescriptor()
        Me.m_btn_Resize.Size = New System.Drawing.Size(10, 23)
        Me.m_btn_Resize.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.m_SuperTooltip.SetSuperTooltip(Me.m_btn_Resize, New DevComponents.DotNetBar.SuperTooltipInfo("调整大小", "", "左键拖动调整宽度，右键拖动调整高度。", Nothing, Nothing, DevComponents.DotNetBar.eTooltipColor.Gray, True, True, New System.Drawing.Size(180, 68)))
        Me.m_btn_Resize.TabIndex = 6
        Me.m_btn_Resize.Text = "::"
        '
        'm_menu_ContextMenuBar
        '
        Me.m_menu_ContextMenuBar.AntiAlias = True
        Me.m_menu_ContextMenuBar.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.m_menu_ContextMenuBar.Items.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.m_menu_ListPopupMenu, Me.m_menu_TabPopupMenu, Me.m_menu_IconPopupMenu})
        Me.m_menu_ContextMenuBar.Location = New System.Drawing.Point(39, 45)
        Me.m_menu_ContextMenuBar.Name = "m_menu_ContextMenuBar"
        Me.m_menu_ContextMenuBar.Size = New System.Drawing.Size(275, 26)
        Me.m_menu_ContextMenuBar.Stretch = True
        Me.m_menu_ContextMenuBar.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.m_menu_ContextMenuBar.TabIndex = 4
        Me.m_menu_ContextMenuBar.TabStop = False
        '
        'm_menu_ListPopupMenu
        '
        Me.m_menu_ListPopupMenu.AutoExpandOnClick = True
        Me.m_menu_ListPopupMenu.Name = "m_menu_ListPopupMenu"
        Me.m_menu_ListPopupMenu.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.m_popup_SelectedTipsCountLabel, Me.m_popup_SelectedTipsTextLabel, Me.m_popup_TipsCountLabel, Me.m_popup_ListItemContainer, Me.m_popup_MoveTipTop, Me.m_popup_MoveTipBottom, Me.m_popup_MoveTipTo, Me.m_menu_HighlightSubMenu, Me.m_popup_CheckDone, Me.m_popup_ShowHighlightList, Me.m_popup_ViewLinksInTips, Me.m_popup_FileTips, Me.m_menu_MoveTipsSubMenu, Me.m_popup_OtherLabel, Me.m_popup_OpenDir, Me.m_popup_Refresh, Me.m_menu_WindowSubMenu, Me.m_popup_Exit})
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
        Me.m_popup_ListItemContainer.Name = "m_popup_ListItemContainer"
        Me.m_popup_ListItemContainer.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.m_popup_InsertTip, Me.m_popup_RemoveTips, Me.m_popup_UpdateTip, Me.m_popup_MoveTipUp, Me.m_popup_MoveTipDown, Me.m_popup_CopyTips, Me.m_popup_PasteAppendToTip, Me.m_popup_SelectAllTips})
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
        Me.m_popup_CopyTips.Tooltip = "复制"
        '
        'm_popup_PasteAppendToTip
        '
        Me.m_popup_PasteAppendToTip.Image = Global.DesktopTips.My.Resources.Resources.PasteHS
        Me.m_popup_PasteAppendToTip.Name = "m_popup_PasteAppendToTip"
        Me.m_popup_PasteAppendToTip.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlV)
        Me.m_popup_PasteAppendToTip.Text = "粘贴附加(&P)"
        Me.m_popup_PasteAppendToTip.Tooltip = "粘贴附加"
        '
        'm_popup_SelectAllTips
        '
        Me.m_popup_SelectAllTips.Image = Global.DesktopTips.My.Resources.Resources.SelectAll
        Me.m_popup_SelectAllTips.Name = "m_popup_SelectAllTips"
        Me.m_popup_SelectAllTips.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlA)
        Me.m_popup_SelectAllTips.Text = "全选(&A)"
        Me.m_popup_SelectAllTips.Tooltip = "全选"
        '
        'm_popup_MoveTipTop
        '
        Me.m_popup_MoveTipTop.BeginGroup = True
        Me.m_popup_MoveTipTop.Image = Global.DesktopTips.My.Resources.Resources.Top
        Me.m_popup_MoveTipTop.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far
        Me.m_popup_MoveTipTop.Name = "m_popup_MoveTipTop"
        Me.m_popup_MoveTipTop.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlShiftT)
        Me.m_popup_MoveTipTop.Text = "置顶(&T)"
        '
        'm_popup_MoveTipBottom
        '
        Me.m_popup_MoveTipBottom.Image = Global.DesktopTips.My.Resources.Resources.Bottom
        Me.m_popup_MoveTipBottom.Name = "m_popup_MoveTipBottom"
        Me.m_popup_MoveTipBottom.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlShiftB)
        Me.m_popup_MoveTipBottom.Text = "置底(&B)"
        '
        'm_popup_MoveTipTo
        '
        Me.m_popup_MoveTipTo.AutoCheckOnClick = True
        Me.m_popup_MoveTipTo.Name = "m_popup_MoveTipTo"
        Me.m_popup_MoveTipTo.Text = "移至指定位置(&P)..."
        '
        'm_menu_HighlightSubMenu
        '
        Me.m_menu_HighlightSubMenu.BeginGroup = True
        Me.m_menu_HighlightSubMenu.Image = Global.DesktopTips.My.Resources.Resources.Star
        Me.m_menu_HighlightSubMenu.Name = "m_menu_HighlightSubMenu"
        Me.m_menu_HighlightSubMenu.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlH)
        Me.m_menu_HighlightSubMenu.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.m_popup_SetupColors})
        Me.m_menu_HighlightSubMenu.Text = "高亮(&H)"
        '
        'm_popup_SetupColors
        '
        Me.m_popup_SetupColors.BeginGroup = True
        Me.m_popup_SetupColors.Image = Global.DesktopTips.My.Resources.Resources.HighLightColor
        Me.m_popup_SetupColors.Name = "m_popup_SetupColors"
        Me.m_popup_SetupColors.Text = "高亮颜色管理(&S)"
        '
        'm_popup_CheckDone
        '
        Me.m_popup_CheckDone.Image = Global.DesktopTips.My.Resources.Resources.Done
        Me.m_popup_CheckDone.Name = "m_popup_CheckDone"
        Me.m_popup_CheckDone.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlJ)
        Me.m_popup_CheckDone.Text = "标记为完成(&J)"
        '
        'm_popup_ShowHighlightList
        '
        Me.m_popup_ShowHighlightList.Name = "m_popup_ShowHighlightList"
        Me.m_popup_ShowHighlightList.Text = "显示高亮标签列表(&I)..."
        '
        'm_popup_ViewLinksInTips
        '
        Me.m_popup_ViewLinksInTips.BeginGroup = True
        Me.m_popup_ViewLinksInTips.Image = CType(resources.GetObject("m_popup_ViewLinksInTips.Image"), System.Drawing.Image)
        Me.m_popup_ViewLinksInTips.Name = "m_popup_ViewLinksInTips"
        Me.m_popup_ViewLinksInTips.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlL)
        Me.m_popup_ViewLinksInTips.Text = "浏览链接(&L)..."
        '
        'm_popup_FileTips
        '
        Me.m_popup_FileTips.Image = Global.DesktopTips.My.Resources.Resources.Find
        Me.m_popup_FileTips.Name = "m_popup_FileTips"
        Me.m_popup_FileTips.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlF)
        Me.m_popup_FileTips.Text = "查找(&F)..."
        '
        'm_menu_MoveTipsSubMenu
        '
        Me.m_menu_MoveTipsSubMenu.Image = Global.DesktopTips.My.Resources.Resources.Right
        Me.m_menu_MoveTipsSubMenu.Name = "m_menu_MoveTipsSubMenu"
        Me.m_menu_MoveTipsSubMenu.Text = "移动至分组(&M)"
        '
        'm_popup_OtherLabel
        '
        Me.m_popup_OtherLabel.BackColor = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.m_popup_OtherLabel.BorderSide = DevComponents.DotNetBar.eBorderSide.Bottom
        Me.m_popup_OtherLabel.BorderType = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.m_popup_OtherLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(21, Byte), Integer), CType(CType(110, Byte), Integer))
        Me.m_popup_OtherLabel.Name = "m_popup_OtherLabel"
        Me.m_popup_OtherLabel.PaddingBottom = 1
        Me.m_popup_OtherLabel.PaddingLeft = 10
        Me.m_popup_OtherLabel.PaddingTop = 1
        Me.m_popup_OtherLabel.SingleLineColor = System.Drawing.Color.FromArgb(CType(CType(197, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(197, Byte), Integer))
        Me.m_popup_OtherLabel.Text = "其他"
        '
        'm_popup_OpenDir
        '
        Me.m_popup_OpenDir.Image = Global.DesktopTips.My.Resources.Resources.OpenFolder
        Me.m_popup_OpenDir.Name = "m_popup_OpenDir"
        Me.m_popup_OpenDir.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.F11)
        Me.m_popup_OpenDir.Text = "打开文件位置(&F)..."
        '
        'm_popup_Refresh
        '
        Me.m_popup_Refresh.Image = Global.DesktopTips.My.Resources.Resources.Refresh
        Me.m_popup_Refresh.Name = "m_popup_Refresh"
        Me.m_popup_Refresh.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.F5)
        Me.m_popup_Refresh.Text = "重新加载(&R)"
        '
        'm_menu_WindowSubMenu
        '
        Me.m_menu_WindowSubMenu.BeginGroup = True
        Me.m_menu_WindowSubMenu.Name = "m_menu_WindowSubMenu"
        Me.m_menu_WindowSubMenu.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.m_popup_SetupHotkey, Me.m_menu_OpacitySubMenu, Me.m_popup_TopMost, Me.m_popup_NotifyIcon, Me.m_popup_LoadPosition, Me.m_popup_SavePosition, Me.m_popup_ClearPosition})
        Me.m_menu_WindowSubMenu.Text = "设置与显示(&S)"
        '
        'm_popup_SetupHotkey
        '
        Me.m_popup_SetupHotkey.Name = "m_popup_SetupHotkey"
        Me.m_popup_SetupHotkey.Text = "快捷键设置(&R)"
        '
        'm_menu_OpacitySubMenu
        '
        Me.m_menu_OpacitySubMenu.BeginGroup = True
        Me.m_menu_OpacitySubMenu.Image = Global.DesktopTips.My.Resources.Resources.Opacity
        Me.m_menu_OpacitySubMenu.Name = "m_menu_OpacitySubMenu"
        Me.m_menu_OpacitySubMenu.Text = "不透明度(&P)"
        '
        'm_popup_TopMost
        '
        Me.m_popup_TopMost.Image = Global.DesktopTips.My.Resources.Resources.Pin
        Me.m_popup_TopMost.Name = "m_popup_TopMost"
        Me.m_popup_TopMost.Text = "窗口置顶(&W)"
        '
        'm_popup_NotifyIcon
        '
        Me.m_popup_NotifyIcon.Name = "m_popup_NotifyIcon"
        Me.m_popup_NotifyIcon.Text = "显示托盘图标(&I)"
        '
        'm_popup_LoadPosition
        '
        Me.m_popup_LoadPosition.BeginGroup = True
        Me.m_popup_LoadPosition.Image = Global.DesktopTips.My.Resources.Resources.WindowPosition
        Me.m_popup_LoadPosition.Name = "m_popup_LoadPosition"
        Me.m_popup_LoadPosition.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlR)
        Me.m_popup_LoadPosition.Text = "恢复位置大小(&L)"
        '
        'm_popup_SavePosition
        '
        Me.m_popup_SavePosition.Name = "m_popup_SavePosition"
        Me.m_popup_SavePosition.Text = "保存当前位置大小(&S)"
        '
        'm_popup_ClearPosition
        '
        Me.m_popup_ClearPosition.Name = "m_popup_ClearPosition"
        Me.m_popup_ClearPosition.Text = "清空保存的位置大小(&C)"
        '
        'm_popup_Exit
        '
        Me.m_popup_Exit.Image = Global.DesktopTips.My.Resources.Resources.Close
        Me.m_popup_Exit.Name = "m_popup_Exit"
        Me.m_popup_Exit.Text = "退出(&E)"
        '
        'm_menu_TabPopupMenu
        '
        Me.m_menu_TabPopupMenu.AutoExpandOnClick = True
        Me.m_menu_TabPopupMenu.Name = "m_menu_TabPopupMenu"
        Me.m_menu_TabPopupMenu.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.m_popup_CurrentTabLabel, Me.m_popup_CurrentTabTextLabel, Me.m_popup_TabCountLabel, Me.m_popup_NewTab, Me.m_popup_DeleteTab, Me.m_popup_RenameTab, Me.m_menu_MoveToTabSubMenu})
        Me.m_menu_TabPopupMenu.Text = "TabPopup"
        '
        'm_popup_CurrentTabLabel
        '
        Me.m_popup_CurrentTabLabel.BackColor = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.m_popup_CurrentTabLabel.BorderSide = DevComponents.DotNetBar.eBorderSide.Bottom
        Me.m_popup_CurrentTabLabel.BorderType = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.m_popup_CurrentTabLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(21, Byte), Integer), CType(CType(110, Byte), Integer))
        Me.m_popup_CurrentTabLabel.Name = "m_popup_CurrentTabLabel"
        Me.m_popup_CurrentTabLabel.PaddingBottom = 1
        Me.m_popup_CurrentTabLabel.PaddingLeft = 10
        Me.m_popup_CurrentTabLabel.PaddingTop = 1
        Me.m_popup_CurrentTabLabel.SingleLineColor = System.Drawing.Color.FromArgb(CType(CType(197, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(197, Byte), Integer))
        Me.m_popup_CurrentTabLabel.Text = "当前分组"
        '
        'm_popup_CurrentTabTextLabel
        '
        Me.m_popup_CurrentTabTextLabel.BackColor = System.Drawing.Color.White
        Me.m_popup_CurrentTabTextLabel.BorderSide = DevComponents.DotNetBar.eBorderSide.None
        Me.m_popup_CurrentTabTextLabel.BorderType = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.m_popup_CurrentTabTextLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(21, Byte), Integer), CType(CType(110, Byte), Integer))
        Me.m_popup_CurrentTabTextLabel.Name = "m_popup_CurrentTabTextLabel"
        Me.m_popup_CurrentTabTextLabel.PaddingLeft = 5
        Me.m_popup_CurrentTabTextLabel.PaddingRight = 5
        Me.m_popup_CurrentTabTextLabel.SingleLineColor = System.Drawing.Color.FromArgb(CType(CType(197, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(197, Byte), Integer))
        Me.m_popup_CurrentTabTextLabel.Text = "LabelItem2LabelItem2LabelItem2LabelItem"
        Me.m_popup_CurrentTabTextLabel.Width = 150
        Me.m_popup_CurrentTabTextLabel.WordWrap = True
        '
        'm_popup_TabCountLabel
        '
        Me.m_popup_TabCountLabel.BackColor = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.m_popup_TabCountLabel.BeginGroup = True
        Me.m_popup_TabCountLabel.BorderSide = DevComponents.DotNetBar.eBorderSide.Bottom
        Me.m_popup_TabCountLabel.BorderType = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.m_popup_TabCountLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(21, Byte), Integer), CType(CType(110, Byte), Integer))
        Me.m_popup_TabCountLabel.Name = "m_popup_TabCountLabel"
        Me.m_popup_TabCountLabel.PaddingBottom = 1
        Me.m_popup_TabCountLabel.PaddingLeft = 10
        Me.m_popup_TabCountLabel.PaddingTop = 1
        Me.m_popup_TabCountLabel.SingleLineColor = System.Drawing.Color.FromArgb(CType(CType(197, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(197, Byte), Integer))
        Me.m_popup_TabCountLabel.Text = "分组 (共 0 组)"
        '
        'm_popup_NewTab
        '
        Me.m_popup_NewTab.Image = Global.DesktopTips.My.Resources.Resources.Plus
        Me.m_popup_NewTab.Name = "m_popup_NewTab"
        Me.m_popup_NewTab.Text = "新建分组(&N)..."
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
        Me.m_popup_RenameTab.Text = "重命名(&R)..."
        '
        'm_menu_MoveToTabSubMenu
        '
        Me.m_menu_MoveToTabSubMenu.BeginGroup = True
        Me.m_menu_MoveToTabSubMenu.Image = Global.DesktopTips.My.Resources.Resources.Right
        Me.m_menu_MoveToTabSubMenu.Name = "m_menu_MoveToTabSubMenu"
        Me.m_menu_MoveToTabSubMenu.Text = "移动至分组(&M)"
        '
        'm_menu_IconPopupMenu
        '
        Me.m_menu_IconPopupMenu.AutoExpandOnClick = True
        Me.m_menu_IconPopupMenu.Name = "m_menu_IconPopupMenu"
        Me.m_menu_IconPopupMenu.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.m_popup_IconShow, Me.m_popup_IconExit})
        Me.m_menu_IconPopupMenu.Text = "IconPopup"
        '
        'm_popup_IconShow
        '
        Me.m_popup_IconShow.Name = "m_popup_IconShow"
        Me.m_popup_IconShow.Text = "显示(&S)"
        '
        'm_popup_IconExit
        '
        Me.m_popup_IconExit.Name = "m_popup_IconExit"
        Me.m_popup_IconExit.Text = "退出(&X)"
        '
        'm_TabView
        '
        Me.m_TabView.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.m_TabView.AutoSelectAttachedControl = False
        Me.m_TabView.BackColor = System.Drawing.Color.DarkRed
        '
        '
        '
        Me.m_TabView.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.m_TabView.ContainerControlProcessDialogKey = True
        Me.m_menu_ContextMenuBar.SetContextMenuEx(Me.m_TabView, Me.m_menu_TabPopupMenu)
        '
        '
        '
        '
        '
        '
        Me.m_TabView.ControlBox.CloseBox.Name = ""
        '
        '
        '
        Me.m_TabView.ControlBox.MenuBox.Name = ""
        Me.m_TabView.ControlBox.Name = ""
        Me.m_TabView.ControlBox.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.m_TabView.ControlBox.MenuBox, Me.m_TabView.ControlBox.CloseBox})
        Me.m_TabView.DataSource = Nothing
        Me.m_TabView.HorizontalText = False
        Me.m_TabView.Location = New System.Drawing.Point(0, 0)
        Me.m_TabView.Name = "m_TabView"
        Me.m_TabView.ReorderTabsEnabled = True
        Me.m_TabView.RotateVerticalText = True
        Me.m_TabView.SelectedTab = Nothing
        Me.m_TabView.SelectedTabFont = New System.Drawing.Font("Microsoft YaHei UI Light", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.m_TabView.SelectedTabIndex = -1
        Me.m_TabView.Size = New System.Drawing.Size(10, 239)
        Me.m_TabView.TabAlignment = DevComponents.DotNetBar.eTabStripAlignment.Left
        Me.m_TabView.TabCloseButtonHot = Nothing
        Me.m_TabView.TabFont = New System.Drawing.Font("Microsoft YaHei UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.m_TabView.TabHorizontalSpacing = 2
        Me.m_TabView.TabIndex = 9
        Me.m_TabView.TabStyle = DevComponents.DotNetBar.eSuperTabStyle.VisualStudio2008Dock
        Me.m_TabView.TabVerticalSpacing = 3
        Me.m_TabView.Text = "m_TabView"
        Me.m_TabView.TextAlignment = DevComponents.DotNetBar.eItemAlignment.Center
        '
        'm_btn_MoveTipUp
        '
        Me.m_btn_MoveTipUp.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.m_btn_MoveTipUp.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.m_btn_MoveTipUp.Font = New System.Drawing.Font("Yu Gothic UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.m_btn_MoveTipUp.Image = Global.DesktopTips.My.Resources.Resources.UpIcon
        Me.m_btn_MoveTipUp.Location = New System.Drawing.Point(229, 9)
        Me.m_btn_MoveTipUp.Name = "m_btn_MoveTipUp"
        Me.m_btn_MoveTipUp.Shape = New DevComponents.DotNetBar.RoundRectangleShapeDescriptor()
        Me.m_btn_MoveTipUp.Size = New System.Drawing.Size(17, 9)
        Me.m_btn_MoveTipUp.TabIndex = 7
        Me.m_btn_MoveTipUp.Tag = "Assist"
        Me.m_btn_MoveTipUp.Tooltip = "上移(U)"
        '
        'm_btn_MoveTipDown
        '
        Me.m_btn_MoveTipDown.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.m_btn_MoveTipDown.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.m_btn_MoveTipDown.Font = New System.Drawing.Font("Yu Gothic UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.m_btn_MoveTipDown.Image = Global.DesktopTips.My.Resources.Resources.DownIcon
        Me.m_btn_MoveTipDown.Location = New System.Drawing.Point(229, 17)
        Me.m_btn_MoveTipDown.Name = "m_btn_MoveTipDown"
        Me.m_btn_MoveTipDown.Shape = New DevComponents.DotNetBar.RoundRectangleShapeDescriptor()
        Me.m_btn_MoveTipDown.Size = New System.Drawing.Size(17, 9)
        Me.m_btn_MoveTipDown.TabIndex = 8
        Me.m_btn_MoveTipDown.Tag = "Assist"
        Me.m_btn_MoveTipDown.Tooltip = "下移(D)"
        '
        'm_NotifyIcon
        '
        Me.m_NotifyIcon.Text = "DesktopTips"
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(326, 262)
        Me.Controls.Add(Me.m_btn_MoveTipDown)
        Me.Controls.Add(Me.m_btn_MoveTipUp)
        Me.Controls.Add(Me.m_menu_ContextMenuBar)
        Me.Controls.Add(Me.m_ListView)
        Me.Controls.Add(Me.m_TabView)
        Me.Controls.Add(Me.m_btn_Resize)
        Me.Controls.Add(Me.m_btn_OpenListPopup)
        Me.Controls.Add(Me.m_btn_Exit)
        Me.Controls.Add(Me.m_btn_InsertTip)
        Me.Controls.Add(Me.m_btn_RemoveTips)
        Me.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(136, 112)
        Me.Name = "MainForm"
        Me.Opacity = 0R
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Text = "DesktopTips"
        Me.TransparencyKey = System.Drawing.Color.DarkRed
        CType(Me.m_menu_ContextMenuBar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.m_TabView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents m_btn_RemoveTips As DevComponents.DotNetBar.ButtonX
    Friend WithEvents m_btn_InsertTip As DevComponents.DotNetBar.ButtonX
    Friend WithEvents m_StyleManager As DevComponents.DotNetBar.StyleManager
    Friend WithEvents m_ListView As TipListBox
    Friend WithEvents m_btn_Exit As DevComponents.DotNetBar.ButtonX
    Friend WithEvents m_btn_OpenListPopup As DevComponents.DotNetBar.ButtonX
    Friend WithEvents m_SuperTooltip As DevComponents.DotNetBar.SuperTooltip
    Friend WithEvents m_menu_ContextMenuBar As DevComponents.DotNetBar.ContextMenuBar
    Friend WithEvents m_menu_ListPopupMenu As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents m_popup_MoveTipUp As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents m_popup_MoveTipDown As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents m_popup_MoveTipTo As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents m_popup_InsertTip As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents m_popup_RemoveTips As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents m_popup_TopMost As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents m_popup_OpenDir As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents m_popup_TipsCountLabel As DevComponents.DotNetBar.LabelItem
    Friend WithEvents m_popup_UpdateTip As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents m_popup_ViewCurrentTips As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents m_popup_Exit As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents m_menu_HighlightSubMenu As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents m_popup_CheckDone As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents m_menu_OpacitySubMenu As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents m_popup_OtherLabel As DevComponents.DotNetBar.LabelItem
    Friend WithEvents m_btn_MoveTipUp As DevComponents.DotNetBar.ButtonX
    Friend WithEvents m_popup_SelectedTipsCountLabel As DevComponents.DotNetBar.LabelItem
    Friend WithEvents m_popup_SelectedTipsTextLabel As DevComponents.DotNetBar.LabelItem
    Friend WithEvents m_popup_MoveTipBottom As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents m_popup_MoveTipTop As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents m_TabView As TabView
    Friend WithEvents m_menu_TabPopupMenu As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents m_popup_NewTab As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents m_popup_DeleteTab As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents m_popup_SelectAllTips As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents m_popup_RenameTab As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents m_menu_MoveTipsSubMenu As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents m_menu_MoveToTabSubMenu As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents m_popup_TabCountLabel As DevComponents.DotNetBar.LabelItem
    Friend WithEvents m_btn_Resize As DevComponents.DotNetBar.ButtonX
    Friend WithEvents m_popup_ListItemContainer As DevComponents.DotNetBar.ItemContainer
    Friend WithEvents m_menu_WindowSubMenu As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents m_popup_CopyTips As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents m_popup_LoadPosition As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents m_popup_SavePosition As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents m_popup_ClearPosition As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents m_popup_PasteAppendToTip As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents m_popup_ViewLinksInTips As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents m_popup_SetupHotkey As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents m_popup_Refresh As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents m_popup_SetupColors As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents m_popup_ViewAllTips As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents m_btn_MoveTipDown As DevComponents.DotNetBar.ButtonX
    Friend WithEvents m_popup_CurrentTabLabel As DevComponents.DotNetBar.LabelItem
    Friend WithEvents m_popup_CurrentTabTextLabel As DevComponents.DotNetBar.LabelItem
    Friend WithEvents m_popup_FindTips As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents m_NotifyIcon As NotifyIcon
    Friend WithEvents m_menu_IconPopupMenu As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents m_popup_IconShow As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents m_popup_IconExit As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents m_popup_FileTips As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents m_popup_NotifyIcon As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents m_popup_ShowHighlightList As DevComponents.DotNetBar.ButtonItem
End Class
