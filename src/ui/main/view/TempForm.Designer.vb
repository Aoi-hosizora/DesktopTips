﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TempForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(TempForm))
        Me.TipListBox1 = New DesktopTips.TipListBox()
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.SuspendLayout()
        '
        'TipListBox1
        '
        Me.TipListBox1.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.0!)
        Me.TipListBox1.FormattingEnabled = True
        Me.TipListBox1.ItemHeight = 17
        Me.TipListBox1.Location = New System.Drawing.Point(83, 75)
        Me.TipListBox1.Name = "TipListBox1"
        Me.TipListBox1.SelectedItem = Nothing
        Me.TipListBox1.Size = New System.Drawing.Size(300, 242)
        Me.TipListBox1.TabIndex = 0
        '
        'ListBox1
        '
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.ItemHeight = 12
        Me.ListBox1.Location = New System.Drawing.Point(477, 93)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(36, 148)
        Me.ListBox1.TabIndex = 1
        '
        'TempForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(525, 438)
        Me.Controls.Add(Me.ListBox1)
        Me.Controls.Add(Me.TipListBox1)
        Me.Name = "TempForm"
        Me.Text = "TempForm"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TipListBox1 As DesktopTips.TipListBox
    Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
End Class
