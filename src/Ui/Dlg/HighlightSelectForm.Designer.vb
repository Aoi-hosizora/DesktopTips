<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class HighlightSelectForm
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
        Me.LabelTab = New System.Windows.Forms.Label()
        Me.ComboBoxTab = New System.Windows.Forms.ComboBox()
        Me.LabelHighlight = New System.Windows.Forms.Label()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOK = New System.Windows.Forms.Button()
        Me.ListBoxHighlight = New System.Windows.Forms.ListBox()
        Me.LabelCount = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'LabelTab
        '
        Me.LabelTab.AutoSize = True
        Me.LabelTab.Location = New System.Drawing.Point(12, 12)
        Me.LabelTab.Name = "LabelTab"
        Me.LabelTab.Size = New System.Drawing.Size(39, 17)
        Me.LabelTab.TabIndex = 0
        Me.LabelTab.Text = "分组 :"
        '
        'ComboBoxTab
        '
        Me.ComboBoxTab.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxTab.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxTab.FormattingEnabled = True
        Me.ComboBoxTab.Location = New System.Drawing.Point(57, 9)
        Me.ComboBoxTab.Name = "ComboBoxTab"
        Me.ComboBoxTab.Size = New System.Drawing.Size(285, 25)
        Me.ComboBoxTab.TabIndex = 1
        '
        'LabelHighlight
        '
        Me.LabelHighlight.AutoSize = True
        Me.LabelHighlight.Location = New System.Drawing.Point(12, 40)
        Me.LabelHighlight.Name = "LabelHighlight"
        Me.LabelHighlight.Size = New System.Drawing.Size(63, 17)
        Me.LabelHighlight.TabIndex = 2
        Me.LabelHighlight.Text = "高亮颜色 :"
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.ButtonCancel.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ButtonCancel.Location = New System.Drawing.Point(267, 213)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(75, 25)
        Me.ButtonCancel.TabIndex = 4
        Me.ButtonCancel.Text = "取消(&C)"
        Me.ButtonCancel.UseVisualStyleBackColor = True
        '
        'ButtonOK
        '
        Me.ButtonOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOK.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ButtonOK.Location = New System.Drawing.Point(186, 213)
        Me.ButtonOK.Name = "ButtonOK"
        Me.ButtonOK.Size = New System.Drawing.Size(75, 25)
        Me.ButtonOK.TabIndex = 5
        Me.ButtonOK.Text = "选择(&O)"
        Me.ButtonOK.UseVisualStyleBackColor = True
        '
        'ListBoxHighlight
        '
        Me.ListBoxHighlight.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListBoxHighlight.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.ListBoxHighlight.FormattingEnabled = True
        Me.ListBoxHighlight.IntegralHeight = False
        Me.ListBoxHighlight.ItemHeight = 17
        Me.ListBoxHighlight.Location = New System.Drawing.Point(12, 63)
        Me.ListBoxHighlight.Name = "ListBoxHighlight"
        Me.ListBoxHighlight.ScrollAlwaysVisible = True
        Me.ListBoxHighlight.Size = New System.Drawing.Size(330, 143)
        Me.ListBoxHighlight.TabIndex = 6
        '
        'LabelCount
        '
        Me.LabelCount.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelCount.Location = New System.Drawing.Point(167, 40)
        Me.LabelCount.Name = "LabelCount"
        Me.LabelCount.Size = New System.Drawing.Size(175, 17)
        Me.LabelCount.TabIndex = 7
        Me.LabelCount.Text = "标签数 : 0 / 0"
        Me.LabelCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'HighlightSelectForm
        '
        Me.AcceptButton = Me.ButtonOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.ButtonCancel
        Me.ClientSize = New System.Drawing.Size(354, 246)
        Me.Controls.Add(Me.LabelCount)
        Me.Controls.Add(Me.ListBoxHighlight)
        Me.Controls.Add(Me.ButtonOK)
        Me.Controls.Add(Me.ButtonCancel)
        Me.Controls.Add(Me.LabelHighlight)
        Me.Controls.Add(Me.ComboBoxTab)
        Me.Controls.Add(Me.LabelTab)
        Me.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.0!)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(275, 220)
        Me.Name = "HighlightSelectForm"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "选择高亮颜色"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents LabelTab As Label
    Friend WithEvents ComboBoxTab As ComboBox
    Friend WithEvents LabelHighlight As Label
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ButtonOK As Button
    Friend WithEvents ListBoxHighlight As ListBox
    Friend WithEvents LabelCount As Label
End Class
