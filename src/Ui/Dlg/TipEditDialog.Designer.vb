<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class TipEditDialog
    Inherits System.Windows.Forms.Form

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(TipEditDialog))
        Me.LabelMessage = New System.Windows.Forms.Label()
        Me.ContextMenuStripOK = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.MenuOK = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuSave = New System.Windows.Forms.ToolStripMenuItem()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.TextBoxContent = New System.Windows.Forms.TextBox()
        Me.ButtonShowOrigin = New System.Windows.Forms.Button()
        Me.TextBoxOrigin = New System.Windows.Forms.TextBox()
        Me.SplitContainerTextBox = New System.Windows.Forms.SplitContainer()
        Me.SuperTooltip1 = New DevComponents.DotNetBar.SuperTooltip()
        Me.LabelTextType = New System.Windows.Forms.Label()
        Me.ButtonOK = New DesktopTips.MenuButton()
        Me.LabelCount = New System.Windows.Forms.Label()
        Me.ComboBoxTextType = New System.Windows.Forms.ComboBox()
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
        Me.LabelMessage.Size = New System.Drawing.Size(297, 56)
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
        Me.ButtonCancel.Location = New System.Drawing.Point(315, 39)
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
        Me.TextBoxContent.Size = New System.Drawing.Size(187, 128)
        Me.TextBoxContent.TabIndex = 3
        Me.TextBoxContent.Text = "文本框"
        '
        'ButtonShowOrigin
        '
        Me.ButtonShowOrigin.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonShowOrigin.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ButtonShowOrigin.Location = New System.Drawing.Point(315, 205)
        Me.ButtonShowOrigin.Name = "ButtonShowOrigin"
        Me.ButtonShowOrigin.Size = New System.Drawing.Size(75, 26)
        Me.ButtonShowOrigin.TabIndex = 6
        Me.ButtonShowOrigin.Text = "显示原文"
        Me.ButtonShowOrigin.UseVisualStyleBackColor = True
        '
        'TextBoxOrigin
        '
        Me.TextBoxOrigin.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBoxOrigin.Location = New System.Drawing.Point(0, 0)
        Me.TextBoxOrigin.Multiline = True
        Me.TextBoxOrigin.Name = "TextBoxOrigin"
        Me.TextBoxOrigin.ReadOnly = True
        Me.TextBoxOrigin.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TextBoxOrigin.Size = New System.Drawing.Size(184, 128)
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
        Me.SplitContainerTextBox.Size = New System.Drawing.Size(375, 128)
        Me.SplitContainerTextBox.SplitterDistance = 187
        Me.SplitContainerTextBox.TabIndex = 7
        '
        'SuperTooltip1
        '
        Me.SuperTooltip1.DefaultFont = New System.Drawing.Font("Microsoft YaHei UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        '
        'LabelTextType
        '
        Me.LabelTextType.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelTextType.AutoSize = True
        Me.LabelTextType.Location = New System.Drawing.Point(12, 209)
        Me.LabelTextType.Name = "LabelTextType"
        Me.LabelTextType.Size = New System.Drawing.Size(68, 17)
        Me.SuperTooltip1.SetSuperTooltip(Me.LabelTextType, New DevComponents.DotNetBar.SuperTooltipInfo("文本类型", "", resources.GetString("LabelTextType.SuperTooltip"), Nothing, Nothing, DevComponents.DotNetBar.eTooltipColor.Gray, True, False, New System.Drawing.Size(405, 425)))
        Me.LabelTextType.TabIndex = 4
        Me.LabelTextType.Text = "文本类型："
        '
        'ButtonOK
        '
        Me.ButtonOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOK.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.ButtonOK.Location = New System.Drawing.Point(315, 9)
        Me.ButtonOK.Menu = Me.ContextMenuStripOK
        Me.ButtonOK.Name = "ButtonOK"
        Me.ButtonOK.Size = New System.Drawing.Size(75, 25)
        Me.SuperTooltip1.SetSuperTooltip(Me.ButtonOK, New DevComponents.DotNetBar.SuperTooltipInfo("保存修改并退出", "", "保存时文本会进行一些格式化操作，包括：" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "1. 删除行末的空白符（空格、制表符）" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "2. 删除文末的所有空行" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "3. 文中最多保留两个连续空行" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "4. Mar" &
            "kdown 类型下会将部分全角字符换写为半角（包括＋－＊＿＝＼～｀＃）" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10), Nothing, Nothing, DevComponents.DotNetBar.eTooltipColor.Gray, True, True, New System.Drawing.Size(285, 142)))
        Me.ButtonOK.TabIndex = 1
        Me.ButtonOK.Text = "确定"
        Me.ButtonOK.UseVisualStyleBackColor = True
        '
        'LabelCount
        '
        Me.LabelCount.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelCount.Location = New System.Drawing.Point(185, 209)
        Me.LabelCount.Name = "LabelCount"
        Me.LabelCount.Size = New System.Drawing.Size(124, 17)
        Me.SuperTooltip1.SetSuperTooltip(Me.LabelCount, New DevComponents.DotNetBar.SuperTooltipInfo("字符总数", "", "字符总数包括空格、制表符、换行符、Markdown 语法与 HTML 标签的任何字符。", Nothing, Nothing, DevComponents.DotNetBar.eTooltipColor.Gray, True, True, New System.Drawing.Size(310, 75)))
        Me.LabelCount.TabIndex = 9
        Me.LabelCount.Text = "字符总数：超过 9999"
        Me.LabelCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ComboBoxTextType
        '
        Me.ComboBoxTextType.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxTextType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxTextType.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ComboBoxTextType.Font = New System.Drawing.Font("Microsoft YaHei", 8.0!)
        Me.ComboBoxTextType.FormattingEnabled = True
        Me.ComboBoxTextType.IntegralHeight = False
        Me.ComboBoxTextType.Items.AddRange(New Object() {"纯文本", "Markdown", "HTML"})
        Me.ComboBoxTextType.Location = New System.Drawing.Point(80, 206)
        Me.ComboBoxTextType.Name = "ComboBoxTextType"
        Me.ComboBoxTextType.Size = New System.Drawing.Size(99, 24)
        Me.ComboBoxTextType.TabIndex = 8
        '
        'TipEditDialog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.ButtonCancel
        Me.ClientSize = New System.Drawing.Size(399, 236)
        Me.Controls.Add(Me.LabelCount)
        Me.Controls.Add(Me.LabelTextType)
        Me.Controls.Add(Me.ComboBoxTextType)
        Me.Controls.Add(Me.SplitContainerTextBox)
        Me.Controls.Add(Me.LabelMessage)
        Me.Controls.Add(Me.ButtonOK)
        Me.Controls.Add(Me.ButtonShowOrigin)
        Me.Controls.Add(Me.ButtonCancel)
        Me.Font = New System.Drawing.Font("Microsoft YaHei", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(415, 275)
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
    Friend WithEvents TextBoxOrigin As TextBox
    Friend WithEvents SplitContainerTextBox As SplitContainer
    Friend WithEvents SuperTooltip1 As DevComponents.DotNetBar.SuperTooltip
    Friend WithEvents ComboBoxTextType As ComboBox
    Friend WithEvents LabelTextType As Label
    Friend WithEvents LabelCount As Label
End Class
