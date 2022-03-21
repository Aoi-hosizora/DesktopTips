<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class HotkeyDialog
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
        Me.ButtonOK = New System.Windows.Forms.Button()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.CheckBoxIsValid = New System.Windows.Forms.CheckBox()
        Me.LabelHint = New System.Windows.Forms.Label()
        Me.ButtonDefault = New System.Windows.Forms.Button()
        Me.HotkeyEditBox = New DesktopTips.HotkeyBox()
        Me.SuspendLayout()
        '
        'ButtonOK
        '
        Me.ButtonOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOK.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ButtonOK.Location = New System.Drawing.Point(12, 99)
        Me.ButtonOK.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ButtonOK.Name = "ButtonOK"
        Me.ButtonOK.Size = New System.Drawing.Size(75, 25)
        Me.ButtonOK.TabIndex = 2
        Me.ButtonOK.Text = "设置(&S)"
        Me.ButtonOK.UseVisualStyleBackColor = True
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.ButtonCancel.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ButtonCancel.Location = New System.Drawing.Point(174, 99)
        Me.ButtonCancel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(75, 25)
        Me.ButtonCancel.TabIndex = 4
        Me.ButtonCancel.Text = "取消(&X)"
        Me.ButtonCancel.UseVisualStyleBackColor = True
        '
        'CheckBoxIsValid
        '
        Me.CheckBoxIsValid.AutoSize = True
        Me.CheckBoxIsValid.Checked = True
        Me.CheckBoxIsValid.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxIsValid.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.CheckBoxIsValid.Location = New System.Drawing.Point(12, 12)
        Me.CheckBoxIsValid.Name = "CheckBoxIsValid"
        Me.CheckBoxIsValid.Size = New System.Drawing.Size(170, 22)
        Me.CheckBoxIsValid.TabIndex = 0
        Me.CheckBoxIsValid.Text = "是否启用快捷显示热键(&H)"
        Me.CheckBoxIsValid.UseVisualStyleBackColor = True
        '
        'LabelHint
        '
        Me.LabelHint.AutoSize = True
        Me.LabelHint.Location = New System.Drawing.Point(11, 42)
        Me.LabelHint.Name = "LabelHint"
        Me.LabelHint.Size = New System.Drawing.Size(128, 17)
        Me.LabelHint.TabIndex = 1
        Me.LabelHint.Text = "请键入快捷显示热键："
        '
        'ButtonDefault
        '
        Me.ButtonDefault.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonDefault.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ButtonDefault.Location = New System.Drawing.Point(93, 99)
        Me.ButtonDefault.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ButtonDefault.Name = "ButtonDefault"
        Me.ButtonDefault.Size = New System.Drawing.Size(75, 25)
        Me.ButtonDefault.TabIndex = 3
        Me.ButtonDefault.Text = "默认(&D)"
        Me.ButtonDefault.UseVisualStyleBackColor = True
        '
        'HotkeyEditBox
        '
        Me.HotkeyEditBox.CurrentKey = System.Windows.Forms.Keys.None
        Me.HotkeyEditBox.Location = New System.Drawing.Point(12, 69)
        Me.HotkeyEditBox.Name = "HotkeyEditBox"
        Me.HotkeyEditBox.Size = New System.Drawing.Size(237, 23)
        Me.HotkeyEditBox.TabIndex = 2
        '
        'HotkeyDialog
        '
        Me.AcceptButton = Me.ButtonOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.ButtonCancel
        Me.ClientSize = New System.Drawing.Size(261, 137)
        Me.Controls.Add(Me.HotkeyEditBox)
        Me.Controls.Add(Me.LabelHint)
        Me.Controls.Add(Me.CheckBoxIsValid)
        Me.Controls.Add(Me.ButtonCancel)
        Me.Controls.Add(Me.ButtonDefault)
        Me.Controls.Add(Me.ButtonOK)
        Me.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "HotkeyDialog"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "设置热键"
        Me.TopMost = True
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ButtonOK As System.Windows.Forms.Button
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents CheckBoxIsValid As System.Windows.Forms.CheckBox
    Friend WithEvents LabelHint As System.Windows.Forms.Label
    Friend WithEvents HotkeyEditBox As DesktopTips.HotkeyBox
    Friend WithEvents ButtonDefault As System.Windows.Forms.Button
End Class
