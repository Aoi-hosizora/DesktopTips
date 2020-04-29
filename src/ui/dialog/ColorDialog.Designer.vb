﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ColorDialog
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
        Me.LabelColor = New System.Windows.Forms.Label()
        Me.ColorListView = New System.Windows.Forms.ListView()
        Me.ColumnHeaderId = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeaderName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeaderHex = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeaderRgb = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeaderView = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ButtonAdd = New System.Windows.Forms.Button()
        Me.ButtonRemove = New System.Windows.Forms.Button()
        Me.ButtonSave = New System.Windows.Forms.Button()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'LabelColor
        '
        Me.LabelColor.AutoSize = True
        Me.LabelColor.Location = New System.Drawing.Point(12, 9)
        Me.LabelColor.Name = "LabelColor"
        Me.LabelColor.Size = New System.Drawing.Size(92, 17)
        Me.LabelColor.TabIndex = 0
        Me.LabelColor.Text = "高亮颜色列表："
        '
        'ColorListView
        '
        Me.ColorListView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeaderId, Me.ColumnHeaderName, Me.ColumnHeaderHex, Me.ColumnHeaderRgb, Me.ColumnHeaderView})
        Me.ColorListView.FullRowSelect = True
        Me.ColorListView.GridLines = True
        Me.ColorListView.HideSelection = False
        Me.ColorListView.Location = New System.Drawing.Point(12, 29)
        Me.ColorListView.Name = "ColorListView"
        Me.ColorListView.Size = New System.Drawing.Size(394, 371)
        Me.ColorListView.TabIndex = 1
        Me.ColorListView.UseCompatibleStateImageBehavior = False
        Me.ColorListView.View = System.Windows.Forms.View.Details
        '
        'ColumnHeaderId
        '
        Me.ColumnHeaderId.Text = "ID"
        '
        'ColumnHeaderName
        '
        Me.ColumnHeaderName.Text = "标签"
        '
        'ColumnHeaderHex
        '
        Me.ColumnHeaderHex.Text = "Hex"
        '
        'ColumnHeaderRgb
        '
        Me.ColumnHeaderRgb.Text = "RGB"
        '
        'ColumnHeaderView
        '
        Me.ColumnHeaderView.Text = "预览"
        '
        'ButtonAdd
        '
        Me.ButtonAdd.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonAdd.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ButtonAdd.Location = New System.Drawing.Point(420, 29)
        Me.ButtonAdd.Name = "ButtonAdd"
        Me.ButtonAdd.Size = New System.Drawing.Size(75, 25)
        Me.ButtonAdd.TabIndex = 2
        Me.ButtonAdd.Text = "添加(&N)"
        Me.ButtonAdd.UseVisualStyleBackColor = True
        '
        'ButtonRemove
        '
        Me.ButtonRemove.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonRemove.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ButtonRemove.Location = New System.Drawing.Point(420, 60)
        Me.ButtonRemove.Name = "ButtonRemove"
        Me.ButtonRemove.Size = New System.Drawing.Size(75, 25)
        Me.ButtonRemove.TabIndex = 2
        Me.ButtonRemove.Text = "删除(&D)"
        Me.ButtonRemove.UseVisualStyleBackColor = True
        '
        'ButtonSave
        '
        Me.ButtonSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonSave.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ButtonSave.Location = New System.Drawing.Point(420, 344)
        Me.ButtonSave.Name = "ButtonSave"
        Me.ButtonSave.Size = New System.Drawing.Size(75, 25)
        Me.ButtonSave.TabIndex = 2
        Me.ButtonSave.Text = "保存(&S)"
        Me.ButtonSave.UseVisualStyleBackColor = True
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ButtonCancel.Location = New System.Drawing.Point(420, 375)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(75, 25)
        Me.ButtonCancel.TabIndex = 2
        Me.ButtonCancel.Text = "返回(&X)"
        Me.ButtonCancel.UseVisualStyleBackColor = True
        '
        'ColorDialog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(507, 412)
        Me.Controls.Add(Me.ButtonCancel)
        Me.Controls.Add(Me.ButtonSave)
        Me.Controls.Add(Me.ButtonRemove)
        Me.Controls.Add(Me.ButtonAdd)
        Me.Controls.Add(Me.ColorListView)
        Me.Controls.Add(Me.LabelColor)
        Me.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ColorDialog"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "设置高亮颜色"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LabelColor As System.Windows.Forms.Label
    Friend WithEvents ColorListView As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeaderId As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeaderName As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeaderHex As System.Windows.Forms.ColumnHeader
    Friend WithEvents ButtonAdd As System.Windows.Forms.Button
    Friend WithEvents ButtonRemove As System.Windows.Forms.Button
    Friend WithEvents ButtonSave As System.Windows.Forms.Button
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ColumnHeaderView As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeaderRgb As System.Windows.Forms.ColumnHeader

End Class
