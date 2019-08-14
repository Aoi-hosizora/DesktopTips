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
        Me.ListPopMenu = New DevComponents.DotNetBar.ButtonItem()
        Me.PopupMenuLabelItemList = New DevComponents.DotNetBar.LabelItem()
        Me.PopMenuButtonMoveUp = New DevComponents.DotNetBar.ButtonItem()
        Me.PopMenuButtonMoveDown = New DevComponents.DotNetBar.ButtonItem()
        Me.PopMenuButtonMoveTop = New DevComponents.DotNetBar.ButtonItem()
        Me.PopMenuButtonHighLight = New DevComponents.DotNetBar.ButtonItem()
        Me.PopMenuButtonHighLightList = New DevComponents.DotNetBar.ButtonItem()
        Me.PopMenuButtonAddItem = New DevComponents.DotNetBar.ButtonItem()
        Me.PopMenuButtonRemoveItem = New DevComponents.DotNetBar.ButtonItem()
        Me.PopMenuButtonEditItem = New DevComponents.DotNetBar.ButtonItem()
        Me.PopupMenuLabelItemFile = New DevComponents.DotNetBar.LabelItem()
        Me.PopMenuButtonOpenFile = New DevComponents.DotNetBar.ButtonItem()
        Me.PopMenuButtonViewFile = New DevComponents.DotNetBar.ButtonItem()
        Me.PopupMenuLabelItemWindow = New DevComponents.DotNetBar.LabelItem()
        Me.PopMenuButtonOpacity = New DevComponents.DotNetBar.ButtonItem()
        Me.PopMenuButtonWinTop = New DevComponents.DotNetBar.ButtonItem()
        Me.PopMenuButtonExit = New DevComponents.DotNetBar.ButtonItem()
        CType(Me.NumericUpDownListCnt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ContextMenuBar1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ButtonRemoveItem
        '
        Me.ButtonRemoveItem.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.ButtonRemoveItem.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonRemoveItem.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.ButtonRemoveItem.Location = New System.Drawing.Point(103, 88)
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
        Me.ButtonAddItem.Location = New System.Drawing.Point(80, 88)
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
        Me.ContextMenuBar1.SetContextMenuEx(Me.ListView, Me.ListPopMenu)
        Me.ListView.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.ListView.FormattingEnabled = True
        Me.ListView.ItemHeight = 17
        Me.ListView.Location = New System.Drawing.Point(0, 0)
        Me.ListView.Name = "ListView"
        Me.ListView.Size = New System.Drawing.Size(127, 89)
        Me.ListView.TabIndex = 0
        '
        'ButtonCloseForm
        '
        Me.ButtonCloseForm.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.ButtonCloseForm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ButtonCloseForm.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.ButtonCloseForm.Location = New System.Drawing.Point(0, 88)
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
        Me.ButtonChangeHeight.Location = New System.Drawing.Point(23, 88)
        Me.ButtonChangeHeight.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ButtonChangeHeight.Name = "ButtonChangeHeight"
        Me.ButtonChangeHeight.Shape = New DevComponents.DotNetBar.RoundRectangleShapeDescriptor()
        Me.ButtonChangeHeight.Size = New System.Drawing.Size(24, 23)
        Me.ButtonChangeHeight.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.SuperTooltip.SetSuperTooltip(Me.ButtonChangeHeight, New DevComponents.DotNetBar.SuperTooltipInfo("设置", "", "左键设置列表显示行高，右键打开文件所在路径。", Nothing, Nothing, DevComponents.DotNetBar.eTooltipColor.Gray, True, True, New System.Drawing.Size(180, 68)))
        Me.ButtonChangeHeight.TabIndex = 1
        Me.ButtonChangeHeight.Text = "≡"
        '
        'NumericUpDownListCnt
        '
        Me.NumericUpDownListCnt.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.NumericUpDownListCnt.Location = New System.Drawing.Point(46, 88)
        Me.NumericUpDownListCnt.Minimum = New Decimal(New Integer() {5, 0, 0, 0})
        Me.NumericUpDownListCnt.Name = "NumericUpDownListCnt"
        Me.NumericUpDownListCnt.Size = New System.Drawing.Size(35, 23)
        Me.NumericUpDownListCnt.TabIndex = 3
        Me.NumericUpDownListCnt.Value = New Decimal(New Integer() {8, 0, 0, 0})
        Me.NumericUpDownListCnt.Visible = False
        '
        'TimerShowForm
        '
        Me.TimerShowForm.Enabled = True
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
        Me.ContextMenuBar1.Items.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.ListPopMenu})
        Me.ContextMenuBar1.Location = New System.Drawing.Point(14, 30)
        Me.ContextMenuBar1.Name = "ContextMenuBar1"
        Me.ContextMenuBar1.Size = New System.Drawing.Size(126, 27)
        Me.ContextMenuBar1.Stretch = True
        Me.ContextMenuBar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.ContextMenuBar1.TabIndex = 4
        Me.ContextMenuBar1.TabStop = False
        Me.ContextMenuBar1.Text = "ContextMenuBar1"
        '
        'ListPopMenu
        '
        Me.ListPopMenu.AutoExpandOnClick = True
        Me.ListPopMenu.Name = "ListPopMenu"
        Me.ListPopMenu.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.PopupMenuLabelItemList, Me.PopMenuButtonMoveUp, Me.PopMenuButtonMoveDown, Me.PopMenuButtonMoveTop, Me.PopMenuButtonHighLight, Me.PopMenuButtonHighLightList, Me.PopMenuButtonAddItem, Me.PopMenuButtonRemoveItem, Me.PopMenuButtonEditItem, Me.PopupMenuLabelItemFile, Me.PopMenuButtonOpenFile, Me.PopMenuButtonViewFile, Me.PopupMenuLabelItemWindow, Me.PopMenuButtonOpacity, Me.PopMenuButtonWinTop, Me.PopMenuButtonExit})
        Me.ListPopMenu.Text = "ListPopMenu"
        '
        'PopupMenuLabelItemList
        '
        Me.PopupMenuLabelItemList.BackColor = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.PopupMenuLabelItemList.BorderSide = DevComponents.DotNetBar.eBorderSide.Bottom
        Me.PopupMenuLabelItemList.BorderType = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PopupMenuLabelItemList.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(21, Byte), Integer), CType(CType(110, Byte), Integer))
        Me.PopupMenuLabelItemList.Name = "PopupMenuLabelItemList"
        Me.PopupMenuLabelItemList.PaddingBottom = 1
        Me.PopupMenuLabelItemList.PaddingLeft = 10
        Me.PopupMenuLabelItemList.PaddingTop = 1
        Me.PopupMenuLabelItemList.SingleLineColor = System.Drawing.Color.FromArgb(CType(CType(197, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(197, Byte), Integer))
        Me.PopupMenuLabelItemList.Text = "列表"
        '
        'PopMenuButtonMoveUp
        '
        Me.PopMenuButtonMoveUp.Image = Global.DesktopTips.My.Resources.Resources._112_UpArrowLong_Orange_16x16_72
        Me.PopMenuButtonMoveUp.Name = "PopMenuButtonMoveUp"
        Me.PopMenuButtonMoveUp.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlU)
        Me.PopMenuButtonMoveUp.Text = "上移(&U)"
        '
        'PopMenuButtonMoveDown
        '
        Me.PopMenuButtonMoveDown.Image = Global.DesktopTips.My.Resources.Resources._112_DownArrowLong_Blue_16x16_72
        Me.PopMenuButtonMoveDown.Name = "PopMenuButtonMoveDown"
        Me.PopMenuButtonMoveDown.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlD)
        Me.PopMenuButtonMoveDown.Text = "下移(&D)"
        '
        'PopMenuButtonMoveTop
        '
        Me.PopMenuButtonMoveTop.Name = "PopMenuButtonMoveTop"
        Me.PopMenuButtonMoveTop.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlT)
        Me.PopMenuButtonMoveTop.Text = "移至顶部(&T)"
        '
        'PopMenuButtonHighLight
        '
        Me.PopMenuButtonHighLight.BeginGroup = True
        Me.PopMenuButtonHighLight.Name = "PopMenuButtonHighLight"
        Me.PopMenuButtonHighLight.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlH)
        Me.PopMenuButtonHighLight.Text = "高亮(&H)"
        '
        'PopMenuButtonHighLightList
        '
        Me.PopMenuButtonHighLightList.Name = "PopMenuButtonHighLightList"
        Me.PopMenuButtonHighLightList.Text = "查看高亮(&L)"
        '
        'PopMenuButtonAddItem
        '
        Me.PopMenuButtonAddItem.BeginGroup = True
        Me.PopMenuButtonAddItem.Image = Global.DesktopTips.My.Resources.Resources._112_Plus_Green_16x16_72
        Me.PopMenuButtonAddItem.Name = "PopMenuButtonAddItem"
        Me.PopMenuButtonAddItem.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlA)
        Me.PopMenuButtonAddItem.Text = "添加(&A)"
        '
        'PopMenuButtonRemoveItem
        '
        Me.PopMenuButtonRemoveItem.Image = Global.DesktopTips.My.Resources.Resources._112_Minus_Grey_16x16_72
        Me.PopMenuButtonRemoveItem.Name = "PopMenuButtonRemoveItem"
        Me.PopMenuButtonRemoveItem.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.Del)
        Me.PopMenuButtonRemoveItem.Text = "删除(&X)"
        '
        'PopMenuButtonEditItem
        '
        Me.PopMenuButtonEditItem.Name = "PopMenuButtonEditItem"
        Me.PopMenuButtonEditItem.Text = "编辑(&E)"
        '
        'PopupMenuLabelItemFile
        '
        Me.PopupMenuLabelItemFile.BackColor = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.PopupMenuLabelItemFile.BorderSide = DevComponents.DotNetBar.eBorderSide.Bottom
        Me.PopupMenuLabelItemFile.BorderType = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PopupMenuLabelItemFile.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(21, Byte), Integer), CType(CType(110, Byte), Integer))
        Me.PopupMenuLabelItemFile.Name = "PopupMenuLabelItemFile"
        Me.PopupMenuLabelItemFile.PaddingBottom = 1
        Me.PopupMenuLabelItemFile.PaddingLeft = 10
        Me.PopupMenuLabelItemFile.PaddingTop = 1
        Me.PopupMenuLabelItemFile.SingleLineColor = System.Drawing.Color.FromArgb(CType(CType(197, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(197, Byte), Integer))
        Me.PopupMenuLabelItemFile.Text = "文件"
        '
        'PopMenuButtonOpenFile
        '
        Me.PopMenuButtonOpenFile.Name = "PopMenuButtonOpenFile"
        Me.PopMenuButtonOpenFile.Text = "打开文件所在位置(&O)"
        '
        'PopMenuButtonViewFile
        '
        Me.PopMenuButtonViewFile.Name = "PopMenuButtonViewFile"
        Me.PopMenuButtonViewFile.Text = "浏览文件(&V)"
        '
        'PopupMenuLabelItemWindow
        '
        Me.PopupMenuLabelItemWindow.BackColor = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.PopupMenuLabelItemWindow.BorderSide = DevComponents.DotNetBar.eBorderSide.Bottom
        Me.PopupMenuLabelItemWindow.BorderType = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PopupMenuLabelItemWindow.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(21, Byte), Integer), CType(CType(110, Byte), Integer))
        Me.PopupMenuLabelItemWindow.Name = "PopupMenuLabelItemWindow"
        Me.PopupMenuLabelItemWindow.PaddingBottom = 1
        Me.PopupMenuLabelItemWindow.PaddingLeft = 10
        Me.PopupMenuLabelItemWindow.PaddingTop = 1
        Me.PopupMenuLabelItemWindow.SingleLineColor = System.Drawing.Color.FromArgb(CType(CType(197, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(197, Byte), Integer))
        Me.PopupMenuLabelItemWindow.Text = "窗口"
        '
        'PopMenuButtonOpacity
        '
        Me.PopMenuButtonOpacity.Name = "PopMenuButtonOpacity"
        Me.PopMenuButtonOpacity.Text = "不透明度(&P)"
        '
        'PopMenuButtonWinTop
        '
        Me.PopMenuButtonWinTop.Name = "PopMenuButtonWinTop"
        Me.PopMenuButtonWinTop.Text = "窗口置顶(&W)"
        '
        'PopMenuButtonExit
        '
        Me.PopMenuButtonExit.Name = "PopMenuButtonExit"
        Me.PopMenuButtonExit.Text = "退出(&E)"
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.DarkRed
        Me.ClientSize = New System.Drawing.Size(127, 112)
        Me.Controls.Add(Me.ContextMenuBar1)
        Me.Controls.Add(Me.ListView)
        Me.Controls.Add(Me.ButtonChangeHeight)
        Me.Controls.Add(Me.ButtonCloseForm)
        Me.Controls.Add(Me.ButtonAddItem)
        Me.Controls.Add(Me.ButtonRemoveItem)
        Me.Controls.Add(Me.LabelFocus)
        Me.Controls.Add(Me.NumericUpDownListCnt)
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
    Friend WithEvents ListPopMenu As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents PopMenuButtonMoveUp As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents PopMenuButtonMoveDown As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents PopMenuButtonAddItem As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents PopMenuButtonRemoveItem As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents PopMenuButtonMoveTop As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents PopMenuButtonWinTop As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents PopMenuButtonOpenFile As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents PopupMenuLabelItemList As DevComponents.DotNetBar.LabelItem
    Friend WithEvents PopupMenuLabelItemWindow As DevComponents.DotNetBar.LabelItem
    Friend WithEvents PopMenuButtonEditItem As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents PopMenuButtonViewFile As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents PopMenuButtonExit As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents PopMenuButtonHighLight As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents PopMenuButtonHighLightList As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents PopMenuButtonOpacity As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents PopupMenuLabelItemFile As DevComponents.DotNetBar.LabelItem

End Class
