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
        Me.ButtonOK = New DesktopTips.MenuButton()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.TextBoxContent = New System.Windows.Forms.TextBox()
        Me.SplitContainer = New System.Windows.Forms.SplitContainer()
        Me.ContextMenuStripOK = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.MenuOK = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuSave = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuUploadImage = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuClipboardImage = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.SplitContainer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer.Panel1.SuspendLayout()
        Me.SplitContainer.Panel2.SuspendLayout()
        Me.SplitContainer.SuspendLayout()
        Me.ContextMenuStripOK.SuspendLayout()
        Me.SuspendLayout()
        '
        'LabelMessage
        '
        Me.LabelMessage.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelMessage.AutoEllipsis = True
        Me.LabelMessage.Location = New System.Drawing.Point(12, 9)
        Me.LabelMessage.Name = "LabelMessage"
        Me.LabelMessage.Size = New System.Drawing.Size(200, 56)
        Me.LabelMessage.TabIndex = 0
        Me.LabelMessage.Text = "内容"
        '
        'ButtonOK
        '
        Me.ButtonOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOK.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.ButtonOK.Location = New System.Drawing.Point(220, 9)
        Me.ButtonOK.Menu = Me.ContextMenuStripOK
        Me.ButtonOK.Name = "ButtonOK"
        Me.ButtonOK.Size = New System.Drawing.Size(75, 25)
        Me.ButtonOK.TabIndex = 1
        Me.ButtonOK.Text = "确定"
        Me.ButtonOK.UseVisualStyleBackColor = True
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.ButtonCancel.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ButtonCancel.Location = New System.Drawing.Point(220, 40)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(75, 25)
        Me.ButtonCancel.TabIndex = 2
        Me.ButtonCancel.Text = "取消"
        Me.ButtonCancel.UseVisualStyleBackColor = True
        '
        'TextBoxContent
        '
        Me.TextBoxContent.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxContent.Location = New System.Drawing.Point(12, 0)
        Me.TextBoxContent.Multiline = True
        Me.TextBoxContent.Name = "TextBoxContent"
        Me.TextBoxContent.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TextBoxContent.Size = New System.Drawing.Size(280, 90)
        Me.TextBoxContent.TabIndex = 4
        Me.TextBoxContent.Text = "文本框"
        '
        'SplitContainer
        '
        Me.SplitContainer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer.Name = "SplitContainer"
        Me.SplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer.Panel1
        '
        Me.SplitContainer.Panel1.Controls.Add(Me.LabelMessage)
        Me.SplitContainer.Panel1.Controls.Add(Me.ButtonOK)
        Me.SplitContainer.Panel1.Controls.Add(Me.ButtonCancel)
        Me.SplitContainer.Panel1MinSize = 70
        '
        'SplitContainer.Panel2
        '
        Me.SplitContainer.Panel2.Controls.Add(Me.TextBoxContent)
        Me.SplitContainer.Panel2MinSize = 100
        Me.SplitContainer.Size = New System.Drawing.Size(304, 181)
        Me.SplitContainer.SplitterDistance = 70
        Me.SplitContainer.SplitterWidth = 5
        Me.SplitContainer.TabIndex = 3
        '
        'ContextMenuStripOK
        '
        Me.ContextMenuStripOK.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MenuOK, Me.MenuSave, Me.MenuUploadImage, Me.MenuClipboardImage})
        Me.ContextMenuStripOK.Name = "ContextMenuStripOK"
        Me.ContextMenuStripOK.Size = New System.Drawing.Size(153, 70)
        '
        'MenuOK
        '
        Me.MenuOK.Name = "MenuOK"
        Me.MenuOK.Size = New System.Drawing.Size(152, 22)
        Me.MenuOK.Text = "确定(&O)"
        '
        'MenuSave
        '
        Me.MenuSave.Name = "MenuSave"
        Me.MenuSave.Size = New System.Drawing.Size(152, 22)
        Me.MenuSave.Text = "保存(&S)"
        '
        'MenuUploadImage
        '
        Me.MenuUploadImage.Name = "MenuUploadImage"
        Me.MenuUploadImage.Size = New System.Drawing.Size(152, 22)
        Me.MenuUploadImage.Text = "上传图片(&S)"
        '
        'MenuClipboardImage
        '
        Me.MenuClipboardImage.Name = "MenuClipboardImage"
        Me.MenuClipboardImage.Size = New System.Drawing.Size(152, 22)
        Me.MenuClipboardImage.Text = "插入剪贴板的图片(&C)"
        '
        'TipEditDialog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.ButtonCancel
        Me.ClientSize = New System.Drawing.Size(320, 220)
        Me.Controls.Add(Me.SplitContainer)
        Me.Font = New System.Drawing.Font("微软雅黑", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(320, 220)
        Me.Name = "TipEditDialog"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "标题"
        Me.SplitContainer.Panel1.ResumeLayout(False)
        Me.SplitContainer.Panel2.ResumeLayout(False)
        Me.SplitContainer.Panel2.PerformLayout()
        CType(Me.SplitContainer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer.ResumeLayout(False)
        Me.ContextMenuStripOK.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LabelMessage As System.Windows.Forms.Label
    Friend WithEvents ButtonOK As DesktopTips.MenuButton
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents TextBoxContent As System.Windows.Forms.TextBox
    Friend WithEvents SplitContainer As System.Windows.Forms.SplitContainer
    Friend WithEvents ContextMenuStripOK As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents MenuOK As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuSave As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuUploadImage As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuClipboardImage As System.Windows.Forms.ToolStripMenuItem
End Class
