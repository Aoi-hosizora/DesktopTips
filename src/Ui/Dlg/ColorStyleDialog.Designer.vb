<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ColorStyleDialog
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.CheckBoxBold = New System.Windows.Forms.CheckBox()
        Me.RadioButtonRegular = New System.Windows.Forms.RadioButton()
        Me.RadioButtonCustom = New System.Windows.Forms.RadioButton()
        Me.CheckBoxItalic = New System.Windows.Forms.CheckBox()
        Me.CheckBoxUnderline = New System.Windows.Forms.CheckBox()
        Me.GroupBoxStyle = New System.Windows.Forms.GroupBox()
        Me.GroupBoxSample = New System.Windows.Forms.GroupBox()
        Me.LabelSample = New System.Windows.Forms.Label()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOK = New System.Windows.Forms.Button()
        Me.GroupBoxStyle.SuspendLayout()
        Me.GroupBoxSample.SuspendLayout()
        Me.SuspendLayout()
        '
        'CheckBoxBold
        '
        Me.CheckBoxBold.AutoSize = True
        Me.CheckBoxBold.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.CheckBoxBold.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBoxBold.Location = New System.Drawing.Point(97, 50)
        Me.CheckBoxBold.Name = "CheckBoxBold"
        Me.CheckBoxBold.Size = New System.Drawing.Size(57, 22)
        Me.CheckBoxBold.TabIndex = 1
        Me.CheckBoxBold.Text = "加粗"
        Me.CheckBoxBold.UseVisualStyleBackColor = True
        '
        'RadioButtonRegular
        '
        Me.RadioButtonRegular.AutoSize = True
        Me.RadioButtonRegular.Checked = True
        Me.RadioButtonRegular.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.RadioButtonRegular.Location = New System.Drawing.Point(11, 22)
        Me.RadioButtonRegular.Name = "RadioButtonRegular"
        Me.RadioButtonRegular.Size = New System.Drawing.Size(80, 22)
        Me.RadioButtonRegular.TabIndex = 2
        Me.RadioButtonRegular.TabStop = True
        Me.RadioButtonRegular.Text = "常规样式"
        Me.RadioButtonRegular.UseVisualStyleBackColor = True
        '
        'RadioButtonCustom
        '
        Me.RadioButtonCustom.AutoSize = True
        Me.RadioButtonCustom.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.RadioButtonCustom.Location = New System.Drawing.Point(97, 22)
        Me.RadioButtonCustom.Name = "RadioButtonCustom"
        Me.RadioButtonCustom.Size = New System.Drawing.Size(92, 22)
        Me.RadioButtonCustom.TabIndex = 2
        Me.RadioButtonCustom.TabStop = True
        Me.RadioButtonCustom.Text = "自定义样式"
        Me.RadioButtonCustom.UseVisualStyleBackColor = True
        '
        'CheckBoxItalic
        '
        Me.CheckBoxItalic.AutoSize = True
        Me.CheckBoxItalic.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.CheckBoxItalic.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBoxItalic.Location = New System.Drawing.Point(97, 78)
        Me.CheckBoxItalic.Name = "CheckBoxItalic"
        Me.CheckBoxItalic.Size = New System.Drawing.Size(57, 22)
        Me.CheckBoxItalic.TabIndex = 1
        Me.CheckBoxItalic.Text = "斜体"
        Me.CheckBoxItalic.UseVisualStyleBackColor = True
        '
        'CheckBoxUnderline
        '
        Me.CheckBoxUnderline.AutoSize = True
        Me.CheckBoxUnderline.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.CheckBoxUnderline.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.0!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBoxUnderline.Location = New System.Drawing.Point(97, 106)
        Me.CheckBoxUnderline.Name = "CheckBoxUnderline"
        Me.CheckBoxUnderline.Size = New System.Drawing.Size(69, 22)
        Me.CheckBoxUnderline.TabIndex = 1
        Me.CheckBoxUnderline.Text = "下划线"
        Me.CheckBoxUnderline.UseVisualStyleBackColor = True
        '
        'GroupBoxStyle
        '
        Me.GroupBoxStyle.Controls.Add(Me.RadioButtonRegular)
        Me.GroupBoxStyle.Controls.Add(Me.RadioButtonCustom)
        Me.GroupBoxStyle.Controls.Add(Me.CheckBoxBold)
        Me.GroupBoxStyle.Controls.Add(Me.CheckBoxItalic)
        Me.GroupBoxStyle.Controls.Add(Me.CheckBoxUnderline)
        Me.GroupBoxStyle.Location = New System.Drawing.Point(12, 12)
        Me.GroupBoxStyle.Name = "GroupBoxStyle"
        Me.GroupBoxStyle.Size = New System.Drawing.Size(200, 133)
        Me.GroupBoxStyle.TabIndex = 3
        Me.GroupBoxStyle.TabStop = False
        Me.GroupBoxStyle.Text = "样式设置"
        '
        'GroupBoxSample
        '
        Me.GroupBoxSample.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBoxSample.Controls.Add(Me.LabelSample)
        Me.GroupBoxSample.Location = New System.Drawing.Point(12, 151)
        Me.GroupBoxSample.Name = "GroupBoxSample"
        Me.GroupBoxSample.Size = New System.Drawing.Size(200, 76)
        Me.GroupBoxSample.TabIndex = 4
        Me.GroupBoxSample.TabStop = False
        Me.GroupBoxSample.Text = "样式浏览"
        '
        'LabelSample
        '
        Me.LabelSample.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelSample.Location = New System.Drawing.Point(3, 19)
        Me.LabelSample.Name = "LabelSample"
        Me.LabelSample.Size = New System.Drawing.Size(194, 54)
        Me.LabelSample.TabIndex = 0
        Me.LabelSample.Text = "中文 あいう AaBbCc 123"
        Me.LabelSample.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.ButtonCancel.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ButtonCancel.Location = New System.Drawing.Point(240, 43)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(75, 25)
        Me.ButtonCancel.TabIndex = 5
        Me.ButtonCancel.Text = "取消"
        Me.ButtonCancel.UseVisualStyleBackColor = True
        '
        'ButtonOK
        '
        Me.ButtonOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOK.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ButtonOK.Location = New System.Drawing.Point(240, 12)
        Me.ButtonOK.Name = "ButtonOK"
        Me.ButtonOK.Size = New System.Drawing.Size(75, 25)
        Me.ButtonOK.TabIndex = 5
        Me.ButtonOK.Text = "确定"
        Me.ButtonOK.UseVisualStyleBackColor = True
        '
        'ColorStyleDialog
        '
        Me.AcceptButton = Me.ButtonOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.ButtonCancel
        Me.ClientSize = New System.Drawing.Size(327, 239)
        Me.Controls.Add(Me.ButtonOK)
        Me.Controls.Add(Me.ButtonCancel)
        Me.Controls.Add(Me.GroupBoxSample)
        Me.Controls.Add(Me.GroupBoxStyle)
        Me.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ColorStyleDialog"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "设置高亮字体样式"
        Me.GroupBoxStyle.ResumeLayout(False)
        Me.GroupBoxStyle.PerformLayout()
        Me.GroupBoxSample.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CheckBoxBold As CheckBox
    Friend WithEvents RadioButtonRegular As RadioButton
    Friend WithEvents RadioButtonCustom As RadioButton
    Friend WithEvents CheckBoxItalic As CheckBox
    Friend WithEvents CheckBoxUnderline As CheckBox
    Friend WithEvents GroupBoxStyle As GroupBox
    Friend WithEvents GroupBoxSample As GroupBox
    Friend WithEvents LabelSample As Label
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ButtonOK As Button
End Class
