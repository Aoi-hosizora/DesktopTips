<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LinkDialog
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
        Me.ListView = New System.Windows.Forms.ListBox()
        Me.LabelTitle = New System.Windows.Forms.Label()
        Me.ButtonOpen = New System.Windows.Forms.Button()
        Me.ButtonBack = New System.Windows.Forms.Button()
        Me.ButtonOpenAll = New System.Windows.Forms.Button()
        Me.CheckBoxOpenInNew = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'ListView
        '
        Me.ListView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListView.FormattingEnabled = True
        Me.ListView.HorizontalScrollbar = True
        Me.ListView.IntegralHeight = False
        Me.ListView.ItemHeight = 17
        Me.ListView.Location = New System.Drawing.Point(7, 31)
        Me.ListView.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ListView.Name = "ListView"
        Me.ListView.ScrollAlwaysVisible = True
        Me.ListView.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.ListView.Size = New System.Drawing.Size(370, 209)
        Me.ListView.TabIndex = 0
        '
        'LabelTitle
        '
        Me.LabelTitle.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelTitle.Location = New System.Drawing.Point(7, 7)
        Me.LabelTitle.Name = "LabelTitle"
        Me.LabelTitle.Size = New System.Drawing.Size(370, 17)
        Me.LabelTitle.TabIndex = 1
        Me.LabelTitle.Text = "所选内容包含了 0 个链接"
        '
        'ButtonOpen
        '
        Me.ButtonOpen.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOpen.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ButtonOpen.Location = New System.Drawing.Point(140, 274)
        Me.ButtonOpen.Name = "ButtonOpen"
        Me.ButtonOpen.Size = New System.Drawing.Size(75, 25)
        Me.ButtonOpen.TabIndex = 2
        Me.ButtonOpen.Text = "打开(&O)"
        Me.ButtonOpen.UseVisualStyleBackColor = True
        '
        'ButtonBack
        '
        Me.ButtonBack.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonBack.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.ButtonBack.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ButtonBack.Location = New System.Drawing.Point(302, 274)
        Me.ButtonBack.Name = "ButtonBack"
        Me.ButtonBack.Size = New System.Drawing.Size(75, 25)
        Me.ButtonBack.TabIndex = 4
        Me.ButtonBack.Text = "返回(&B)"
        Me.ButtonBack.UseVisualStyleBackColor = True
        '
        'ButtonOpenAll
        '
        Me.ButtonOpenAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOpenAll.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ButtonOpenAll.Location = New System.Drawing.Point(221, 274)
        Me.ButtonOpenAll.Name = "ButtonOpenAll"
        Me.ButtonOpenAll.Size = New System.Drawing.Size(75, 25)
        Me.ButtonOpenAll.TabIndex = 3
        Me.ButtonOpenAll.Text = "打开所有(&A)"
        Me.ButtonOpenAll.UseVisualStyleBackColor = True
        '
        'CheckBoxOpenInNew
        '
        Me.CheckBoxOpenInNew.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxOpenInNew.AutoSize = True
        Me.CheckBoxOpenInNew.Location = New System.Drawing.Point(12, 247)
        Me.CheckBoxOpenInNew.Name = "CheckBoxOpenInNew"
        Me.CheckBoxOpenInNew.Size = New System.Drawing.Size(135, 21)
        Me.CheckBoxOpenInNew.TabIndex = 1
        Me.CheckBoxOpenInNew.Text = "在新窗口打开浏览器"
        Me.CheckBoxOpenInNew.UseVisualStyleBackColor = True
        '
        'LinkDialog
        '
        Me.AcceptButton = Me.ButtonBack
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.ButtonBack
        Me.ClientSize = New System.Drawing.Size(384, 311)
        Me.Controls.Add(Me.CheckBoxOpenInNew)
        Me.Controls.Add(Me.ButtonBack)
        Me.Controls.Add(Me.ButtonOpenAll)
        Me.Controls.Add(Me.ButtonOpen)
        Me.Controls.Add(Me.LabelTitle)
        Me.Controls.Add(Me.ListView)
        Me.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(300, 250)
        Me.Name = "LinkDialog"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "包含的链接"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ListView As System.Windows.Forms.ListBox
    Friend WithEvents ButtonOpen As System.Windows.Forms.Button
    Friend WithEvents ButtonBack As System.Windows.Forms.Button
    Private WithEvents LabelTitle As System.Windows.Forms.Label
    Friend WithEvents ButtonOpenAll As System.Windows.Forms.Button
    Friend WithEvents CheckBoxOpenInNew As System.Windows.Forms.CheckBox
End Class
