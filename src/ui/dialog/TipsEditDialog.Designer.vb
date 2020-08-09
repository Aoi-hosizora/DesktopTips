<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TipsEditDialog
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
        Me.LabelMessage = New System.Windows.Forms.Label()
        Me.ButtonOK = New System.Windows.Forms.Button()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.TextBoxContent = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'LabelMessage
        '
        Me.LabelMessage.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelMessage.Location = New System.Drawing.Point(12, 9)
        Me.LabelMessage.Name = "LabelMessage"
        Me.LabelMessage.Size = New System.Drawing.Size(281, 85)
        Me.LabelMessage.TabIndex = 0
        Me.LabelMessage.Text = "内容"
        '
        'ButtonOK
        '
        Me.ButtonOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOK.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.ButtonOK.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ButtonOK.Font = New System.Drawing.Font("Yu Gothic UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonOK.Location = New System.Drawing.Point(307, 9)
        Me.ButtonOK.Name = "ButtonOK"
        Me.ButtonOK.Size = New System.Drawing.Size(75, 25)
        Me.ButtonOK.TabIndex = 2
        Me.ButtonOK.Text = "OK"
        Me.ButtonOK.UseVisualStyleBackColor = True
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.ButtonCancel.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ButtonCancel.Font = New System.Drawing.Font("Yu Gothic UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonCancel.Location = New System.Drawing.Point(307, 40)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(75, 25)
        Me.ButtonCancel.TabIndex = 3
        Me.ButtonCancel.Text = "キャンセル"
        Me.ButtonCancel.UseVisualStyleBackColor = True
        '
        'TextBoxContent
        '
        Me.TextBoxContent.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxContent.Location = New System.Drawing.Point(12, 103)
        Me.TextBoxContent.Multiline = True
        Me.TextBoxContent.Name = "TextBoxContent"
        Me.TextBoxContent.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TextBoxContent.Size = New System.Drawing.Size(370, 99)
        Me.TextBoxContent.TabIndex = 1
        Me.TextBoxContent.Text = "文本框"
        '
        'TipsEditDialog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.ButtonCancel
        Me.ClientSize = New System.Drawing.Size(394, 214)
        Me.Controls.Add(Me.TextBoxContent)
        Me.Controls.Add(Me.ButtonCancel)
        Me.Controls.Add(Me.ButtonOK)
        Me.Controls.Add(Me.LabelMessage)
        Me.Font = New System.Drawing.Font("微软雅黑", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "TipsEditDialog"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "标题"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LabelMessage As System.Windows.Forms.Label
    Friend WithEvents ButtonOK As System.Windows.Forms.Button
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents TextBoxContent As System.Windows.Forms.TextBox
End Class
