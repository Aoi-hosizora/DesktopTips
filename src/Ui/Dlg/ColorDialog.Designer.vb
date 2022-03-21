<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
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
        Me.ColumnHeaderRgb = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeaderHex = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeaderStyle = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeaderView = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ButtonAdd = New System.Windows.Forms.Button()
        Me.ButtonRemove = New System.Windows.Forms.Button()
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
        Me.ColorListView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ColorListView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeaderId, Me.ColumnHeaderName, Me.ColumnHeaderRgb, Me.ColumnHeaderHex, Me.ColumnHeaderStyle, Me.ColumnHeaderView})
        Me.ColorListView.FullRowSelect = True
        Me.ColorListView.GridLines = True
        Me.ColorListView.HideSelection = False
        Me.ColorListView.Location = New System.Drawing.Point(12, 29)
        Me.ColorListView.Name = "ColorListView"
        Me.ColorListView.ShowItemToolTips = True
        Me.ColorListView.Size = New System.Drawing.Size(414, 367)
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
        'ColumnHeaderRgb
        '
        Me.ColumnHeaderRgb.Text = "RGB"
        '
        'ColumnHeaderHex
        '
        Me.ColumnHeaderHex.Text = "Hex"
        '
        'ColumnHeaderStyle
        '
        Me.ColumnHeaderStyle.Text = "样式"
        '
        'ColumnHeaderView
        '
        Me.ColumnHeaderView.Text = "预览"
        '
        'ButtonAdd
        '
        Me.ButtonAdd.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonAdd.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ButtonAdd.Location = New System.Drawing.Point(440, 29)
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
        Me.ButtonRemove.Location = New System.Drawing.Point(440, 60)
        Me.ButtonRemove.Name = "ButtonRemove"
        Me.ButtonRemove.Size = New System.Drawing.Size(75, 25)
        Me.ButtonRemove.TabIndex = 3
        Me.ButtonRemove.Text = "删除(&D)"
        Me.ButtonRemove.UseVisualStyleBackColor = True
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.ButtonCancel.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ButtonCancel.Location = New System.Drawing.Point(440, 371)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(75, 25)
        Me.ButtonCancel.TabIndex = 4
        Me.ButtonCancel.Text = "返回(&X)"
        Me.ButtonCancel.UseVisualStyleBackColor = True
        '
        'ColorDialog
        '
        Me.AcceptButton = Me.ButtonCancel
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.ButtonCancel
        Me.ClientSize = New System.Drawing.Size(527, 408)
        Me.Controls.Add(Me.ButtonCancel)
        Me.Controls.Add(Me.ButtonRemove)
        Me.Controls.Add(Me.ButtonAdd)
        Me.Controls.Add(Me.ColorListView)
        Me.Controls.Add(Me.LabelColor)
        Me.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.0!)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ColorDialog"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "高亮颜色管理"
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
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ColumnHeaderView As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeaderRgb As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeaderStyle As ColumnHeader
End Class
