<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class HighlightTipsDialog
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ListBoxTips = New System.Windows.Forms.ListBox()
        Me.LabelTitle = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.ButtonCancel.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ButtonCancel.Location = New System.Drawing.Point(302, 277)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(75, 25)
        Me.ButtonCancel.TabIndex = 10
        Me.ButtonCancel.Text = "返回(&B)"
        Me.ButtonCancel.UseVisualStyleBackColor = True
        '
        'ListBoxTips
        '
        Me.ListBoxTips.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListBoxTips.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.ListBoxTips.FormattingEnabled = True
        Me.ListBoxTips.IntegralHeight = False
        Me.ListBoxTips.ItemHeight = 17
        Me.ListBoxTips.Location = New System.Drawing.Point(7, 32)
        Me.ListBoxTips.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ListBoxTips.Name = "ListBoxTips"
        Me.ListBoxTips.ScrollAlwaysVisible = True
        Me.ListBoxTips.Size = New System.Drawing.Size(370, 238)
        Me.ListBoxTips.TabIndex = 5
        '
        'LabelTitle
        '
        Me.LabelTitle.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelTitle.Location = New System.Drawing.Point(7, 8)
        Me.LabelTitle.Name = "LabelTitle"
        Me.LabelTitle.Size = New System.Drawing.Size(370, 17)
        Me.LabelTitle.TabIndex = 7
        Me.LabelTitle.Text = "分组 """" 中高亮颜色为 """" 的标签：(共 0 项)"
        '
        'HighlightTipsDialog
        '
        Me.AcceptButton = Me.ButtonCancel
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.ButtonCancel
        Me.ClientSize = New System.Drawing.Size(384, 311)
        Me.Controls.Add(Me.ButtonCancel)
        Me.Controls.Add(Me.LabelTitle)
        Me.Controls.Add(Me.ListBoxTips)
        Me.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.0!)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "HighlightTipsDialog"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "高亮标签列表"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ListBoxTips As ListBox
    Private WithEvents LabelTitle As Label
End Class
