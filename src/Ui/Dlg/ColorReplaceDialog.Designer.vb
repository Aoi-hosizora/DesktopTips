﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ColorReplaceDialog
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ColorReplaceDialog))
        Me.CommandPanel = New System.Windows.Forms.Panel()
        Me.TitleWarningBox = New DevComponents.DotNetBar.Controls.WarningBox()
        Me.ButtonExit = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'CommandPanel
        '
        Me.CommandPanel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CommandPanel.AutoScroll = True
        Me.CommandPanel.BackColor = System.Drawing.Color.White
        Me.CommandPanel.Location = New System.Drawing.Point(0, 47)
        Me.CommandPanel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.CommandPanel.Name = "CommandPanel"
        Me.CommandPanel.Size = New System.Drawing.Size(304, 328)
        Me.CommandPanel.TabIndex = 0
        '
        'TitleWarningBox
        '
        Me.TitleWarningBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.TitleWarningBox.CloseButtonVisible = False
        Me.TitleWarningBox.Dock = System.Windows.Forms.DockStyle.Top
        Me.TitleWarningBox.Image = CType(resources.GetObject("TitleWarningBox.Image"), System.Drawing.Image)
        Me.TitleWarningBox.Location = New System.Drawing.Point(0, 0)
        Me.TitleWarningBox.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.TitleWarningBox.Name = "TitleWarningBox"
        Me.TitleWarningBox.OptionsButtonVisible = False
        Me.TitleWarningBox.OptionsText = ""
        Me.TitleWarningBox.Size = New System.Drawing.Size(304, 47)
        Me.TitleWarningBox.TabIndex = 0
        Me.TitleWarningBox.Text = "请选择一个颜色替换颜色 ""xxx"""
        '
        'ButtonExit
        '
        Me.ButtonExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.ButtonExit.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ButtonExit.Location = New System.Drawing.Point(217, 382)
        Me.ButtonExit.Name = "ButtonExit"
        Me.ButtonExit.Size = New System.Drawing.Size(75, 25)
        Me.ButtonExit.TabIndex = 1
        Me.ButtonExit.Text = "取消(&X)"
        Me.ButtonExit.UseVisualStyleBackColor = True
        '
        'ColorReplaceDialog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.ButtonExit
        Me.ClientSize = New System.Drawing.Size(304, 419)
        Me.Controls.Add(Me.ButtonExit)
        Me.Controls.Add(Me.CommandPanel)
        Me.Controls.Add(Me.TitleWarningBox)
        Me.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ColorReplaceDialog"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "替换颜色"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CommandPanel As System.Windows.Forms.Panel
    Friend WithEvents TitleWarningBox As DevComponents.DotNetBar.Controls.WarningBox
    Friend WithEvents TestCommandLink1 As CommandLink
    Friend WithEvents ButtonExit As System.Windows.Forms.Button
    Friend WithEvents TestCommandLink2 As DesktopTips.CommandLink

End Class
