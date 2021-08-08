<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TipEditDialog
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
        Me.LabelMessage = New System.Windows.Forms.Label()
        Me.ContextMenuStripOK = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.MenuOK = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuSave = New System.Windows.Forms.ToolStripMenuItem()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.TextBoxContent = New System.Windows.Forms.TextBox()
        Me.ButtonShowOrigin = New System.Windows.Forms.Button()
        Me.CheckBoxStyle = New System.Windows.Forms.CheckBox()
        Me.TextBoxOrigin = New System.Windows.Forms.TextBox()
        Me.SplitContainerTextBox = New System.Windows.Forms.SplitContainer()
        Me.ButtonOK = New DesktopTips.MenuButton()
        Me.SuperTooltip1 = New DevComponents.DotNetBar.SuperTooltip()
        Me.ContextMenuStripOK.SuspendLayout()
        CType(Me.SplitContainerTextBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerTextBox.Panel1.SuspendLayout()
        Me.SplitContainerTextBox.Panel2.SuspendLayout()
        Me.SplitContainerTextBox.SuspendLayout()
        Me.SuspendLayout()
        '
        'LabelMessage
        '
        Me.LabelMessage.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelMessage.AutoEllipsis = True
        Me.LabelMessage.Location = New System.Drawing.Point(12, 9)
        Me.LabelMessage.Name = "LabelMessage"
        Me.LabelMessage.Size = New System.Drawing.Size(352, 56)
        Me.LabelMessage.TabIndex = 0
        Me.LabelMessage.Text = "内容"
        '
        'ContextMenuStripOK
        '
        Me.ContextMenuStripOK.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MenuOK, Me.MenuSave})
        Me.ContextMenuStripOK.Name = "ContextMenuStripOK"
        Me.ContextMenuStripOK.Size = New System.Drawing.Size(116, 48)
        '
        'MenuOK
        '
        Me.MenuOK.Name = "MenuOK"
        Me.MenuOK.Size = New System.Drawing.Size(115, 22)
        Me.MenuOK.Text = "确定(&O)"
        '
        'MenuSave
        '
        Me.MenuSave.Name = "MenuSave"
        Me.MenuSave.Size = New System.Drawing.Size(115, 22)
        Me.MenuSave.Text = "保存(&S)"
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.ButtonCancel.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ButtonCancel.Location = New System.Drawing.Point(370, 40)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(75, 25)
        Me.ButtonCancel.TabIndex = 2
        Me.ButtonCancel.Text = "取消"
        Me.ButtonCancel.UseVisualStyleBackColor = True
        '
        'TextBoxContent
        '
        Me.TextBoxContent.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBoxContent.Location = New System.Drawing.Point(0, 0)
        Me.TextBoxContent.Multiline = True
        Me.TextBoxContent.Name = "TextBoxContent"
        Me.TextBoxContent.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TextBoxContent.Size = New System.Drawing.Size(215, 128)
        Me.TextBoxContent.TabIndex = 3
        Me.TextBoxContent.Text = "文本框"
        '
        'ButtonShowOrigin
        '
        Me.ButtonShowOrigin.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonShowOrigin.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ButtonShowOrigin.Location = New System.Drawing.Point(367, 205)
        Me.ButtonShowOrigin.Name = "ButtonShowOrigin"
        Me.ButtonShowOrigin.Size = New System.Drawing.Size(75, 25)
        Me.ButtonShowOrigin.TabIndex = 6
        Me.ButtonShowOrigin.Text = "显示原文"
        Me.ButtonShowOrigin.UseVisualStyleBackColor = True
        '
        'CheckBoxStyle
        '
        Me.CheckBoxStyle.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxStyle.AutoSize = True
        Me.CheckBoxStyle.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.CheckBoxStyle.Location = New System.Drawing.Point(13, 207)
        Me.CheckBoxStyle.Name = "CheckBoxStyle"
        Me.CheckBoxStyle.Size = New System.Drawing.Size(152, 22)
        Me.SuperTooltip1.SetSuperTooltip(Me.CheckBoxStyle, New DevComponents.DotNetBar.SuperTooltipInfo("Markdown 样式", "", "开启此选项后，会将一些 Markdown 语法显示为样式。如：" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "**Bold**" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "*Italic*" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "_Underline_" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "~~Strokline~~" &
            "", Nothing, Nothing, DevComponents.DotNetBar.eTooltipColor.Gray, True, True, New System.Drawing.Size(257, 145)))
        Me.CheckBoxStyle.TabIndex = 5
        Me.CheckBoxStyle.Text = "使用 Markdown 样式"
        Me.CheckBoxStyle.UseVisualStyleBackColor = True
        '
        'TextBoxOrigin
        '
        Me.TextBoxOrigin.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBoxOrigin.Location = New System.Drawing.Point(0, 0)
        Me.TextBoxOrigin.Multiline = True
        Me.TextBoxOrigin.Name = "TextBoxOrigin"
        Me.TextBoxOrigin.ReadOnly = True
        Me.TextBoxOrigin.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TextBoxOrigin.Size = New System.Drawing.Size(211, 128)
        Me.TextBoxOrigin.TabIndex = 4
        Me.TextBoxOrigin.Text = "文本框"
        '
        'SplitContainerTextBox
        '
        Me.SplitContainerTextBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainerTextBox.Location = New System.Drawing.Point(12, 71)
        Me.SplitContainerTextBox.Name = "SplitContainerTextBox"
        '
        'SplitContainerTextBox.Panel1
        '
        Me.SplitContainerTextBox.Panel1.Controls.Add(Me.TextBoxContent)
        '
        'SplitContainerTextBox.Panel2
        '
        Me.SplitContainerTextBox.Panel2.Controls.Add(Me.TextBoxOrigin)
        Me.SplitContainerTextBox.Size = New System.Drawing.Size(430, 128)
        Me.SplitContainerTextBox.SplitterDistance = 215
        Me.SplitContainerTextBox.TabIndex = 7
        '
        'ButtonOK
        '
        Me.ButtonOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOK.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.ButtonOK.Location = New System.Drawing.Point(370, 9)
        Me.ButtonOK.Menu = Me.ContextMenuStripOK
        Me.ButtonOK.Name = "ButtonOK"
        Me.ButtonOK.Size = New System.Drawing.Size(75, 25)
        Me.SuperTooltip1.SetSuperTooltip(Me.ButtonOK, New DevComponents.DotNetBar.SuperTooltipInfo("保存修改并退出", "", "保存时文本会进行一些格式化操作，包括：" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "1. 删除行前行末空白符" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "2. 最多仅保留两个换行" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10), Nothing, Nothing, DevComponents.DotNetBar.eTooltipColor.Gray, True, True, New System.Drawing.Size(265, 100)))
        Me.ButtonOK.TabIndex = 1
        Me.ButtonOK.Text = "确定"
        Me.ButtonOK.UseVisualStyleBackColor = True
        '
        'SuperTooltip1
        '
        Me.SuperTooltip1.DefaultFont = New System.Drawing.Font("Microsoft YaHei UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        '
        'TipEditDialog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.ButtonCancel
        Me.ClientSize = New System.Drawing.Size(454, 236)
        Me.Controls.Add(Me.SplitContainerTextBox)
        Me.Controls.Add(Me.CheckBoxStyle)
        Me.Controls.Add(Me.LabelMessage)
        Me.Controls.Add(Me.ButtonOK)
        Me.Controls.Add(Me.ButtonShowOrigin)
        Me.Controls.Add(Me.ButtonCancel)
        Me.Font = New System.Drawing.Font("Microsoft YaHei", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(380, 275)
        Me.Name = "TipEditDialog"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "标题"
        Me.ContextMenuStripOK.ResumeLayout(False)
        Me.SplitContainerTextBox.Panel1.ResumeLayout(False)
        Me.SplitContainerTextBox.Panel1.PerformLayout()
        Me.SplitContainerTextBox.Panel2.ResumeLayout(False)
        Me.SplitContainerTextBox.Panel2.PerformLayout()
        CType(Me.SplitContainerTextBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerTextBox.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LabelMessage As System.Windows.Forms.Label
    Friend WithEvents ButtonOK As DesktopTips.MenuButton
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents TextBoxContent As System.Windows.Forms.TextBox
    Friend WithEvents ContextMenuStripOK As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents MenuOK As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuSave As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ButtonShowOrigin As Button
    Friend WithEvents CheckBoxStyle As CheckBox
    Friend WithEvents TextBoxOrigin As TextBox
    Friend WithEvents SplitContainerTextBox As SplitContainer
    Friend WithEvents SuperTooltip1 As DevComponents.DotNetBar.SuperTooltip
End Class
