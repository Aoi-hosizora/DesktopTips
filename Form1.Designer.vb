<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.ButtonRemoveItem = New DevComponents.DotNetBar.ButtonX()
        Me.ButtonAddItem = New DevComponents.DotNetBar.ButtonX()
        Me.StyleManager1 = New DevComponents.DotNetBar.StyleManager(Me.components)
        Me.LabelFocus = New System.Windows.Forms.Label()
        Me.ListView = New System.Windows.Forms.ListBox()
        Me.ButtonCloseForm = New DevComponents.DotNetBar.ButtonX()
        Me.ButtonChangeHeight = New DevComponents.DotNetBar.ButtonX()
        Me.NumericUpDown1 = New System.Windows.Forms.NumericUpDown()
        Me.TimerShowForm = New System.Windows.Forms.Timer(Me.components)
        Me.TimerEndForm = New System.Windows.Forms.Timer(Me.components)
        Me.TimerMouseIn = New System.Windows.Forms.Timer(Me.components)
        Me.TimerMouseOut = New System.Windows.Forms.Timer(Me.components)
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ButtonRemoveItem
        '
        Me.ButtonRemoveItem.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.ButtonRemoveItem.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonRemoveItem.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.ButtonRemoveItem.Location = New System.Drawing.Point(176, 88)
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
        Me.ButtonAddItem.Location = New System.Drawing.Point(153, 88)
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
        Me.ListView.FormattingEnabled = True
        Me.ListView.ItemHeight = 17
        Me.ListView.Location = New System.Drawing.Point(0, 0)
        Me.ListView.Name = "ListView"
        Me.ListView.Size = New System.Drawing.Size(200, 89)
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
        Me.ButtonChangeHeight.TabIndex = 1
        Me.ButtonChangeHeight.Text = "≡"
        '
        'NumericUpDown1
        '
        Me.NumericUpDown1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.NumericUpDown1.Location = New System.Drawing.Point(46, 88)
        Me.NumericUpDown1.Minimum = New Decimal(New Integer() {5, 0, 0, 0})
        Me.NumericUpDown1.Name = "NumericUpDown1"
        Me.NumericUpDown1.Size = New System.Drawing.Size(45, 23)
        Me.NumericUpDown1.TabIndex = 3
        Me.NumericUpDown1.Value = New Decimal(New Integer() {8, 0, 0, 0})
        Me.NumericUpDown1.Visible = False
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
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.DarkRed
        Me.ClientSize = New System.Drawing.Size(200, 112)
        Me.Controls.Add(Me.ListView)
        Me.Controls.Add(Me.ButtonChangeHeight)
        Me.Controls.Add(Me.ButtonCloseForm)
        Me.Controls.Add(Me.ButtonAddItem)
        Me.Controls.Add(Me.ButtonRemoveItem)
        Me.Controls.Add(Me.LabelFocus)
        Me.Controls.Add(Me.NumericUpDown1)
        Me.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(150, 112)
        Me.Name = "Form1"
        Me.Opacity = 0.0R
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Text = "DesktopTips"
        Me.TransparencyKey = System.Drawing.Color.DarkRed
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents NumericUpDown1 As System.Windows.Forms.NumericUpDown
    Friend WithEvents TimerShowForm As System.Windows.Forms.Timer
    Friend WithEvents TimerEndForm As System.Windows.Forms.Timer
    Friend WithEvents TimerMouseIn As System.Windows.Forms.Timer
    Friend WithEvents TimerMouseOut As System.Windows.Forms.Timer

End Class
