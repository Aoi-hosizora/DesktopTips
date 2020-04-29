<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SearchDialog
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
        Me.ListView = New System.Windows.Forms.ListBox()
        Me.LabelResult = New System.Windows.Forms.Label()
        Me.ButtonSearch = New System.Windows.Forms.Button()
        Me.ButtonClose = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'ListView
        '
        Me.ListView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListView.FormattingEnabled = True
        Me.ListView.HorizontalScrollbar = True
        Me.ListView.IntegralHeight = False
        Me.ListView.ItemHeight = 17
        Me.ListView.Location = New System.Drawing.Point(7, 31)
        Me.ListView.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ListView.Name = "ListView"
        Me.ListView.ScrollAlwaysVisible = True
        Me.ListView.Size = New System.Drawing.Size(370, 239)
        Me.ListView.TabIndex = 0
        '
        'LabelResult
        '
        Me.LabelResult.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelResult.Location = New System.Drawing.Point(7, 7)
        Me.LabelResult.Name = "LabelResult"
        Me.LabelResult.Size = New System.Drawing.Size(370, 17)
        Me.LabelResult.TabIndex = 1
        Me.LabelResult.Text = """xxx"" 的搜索结果：(共找到 0 项)"
        '
        'ButtonSearch
        '
        Me.ButtonSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonSearch.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ButtonSearch.Location = New System.Drawing.Point(221, 277)
        Me.ButtonSearch.Name = "ButtonSearch"
        Me.ButtonSearch.Size = New System.Drawing.Size(75, 25)
        Me.ButtonSearch.TabIndex = 2
        Me.ButtonSearch.Text = "新查找(&F)"
        Me.ButtonSearch.UseVisualStyleBackColor = True
        '
        'ButtonClose
        '
        Me.ButtonClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.ButtonClose.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ButtonClose.Location = New System.Drawing.Point(302, 277)
        Me.ButtonClose.Name = "ButtonClose"
        Me.ButtonClose.Size = New System.Drawing.Size(75, 25)
        Me.ButtonClose.TabIndex = 3
        Me.ButtonClose.Text = "返回(&B)"
        Me.ButtonClose.UseVisualStyleBackColor = True
        '
        'SearchDialog
        '
        Me.AcceptButton = Me.ButtonClose
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.ButtonClose
        Me.ClientSize = New System.Drawing.Size(384, 311)
        Me.Controls.Add(Me.ButtonClose)
        Me.Controls.Add(Me.ButtonSearch)
        Me.Controls.Add(Me.LabelResult)
        Me.Controls.Add(Me.ListView)
        Me.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(300, 250)
        Me.Name = "SearchDialog"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "搜索"
        Me.TopMost = True
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ListView As System.Windows.Forms.ListBox
    Friend WithEvents LabelResult As System.Windows.Forms.Label
    Friend WithEvents ButtonSearch As System.Windows.Forms.Button
    Friend WithEvents ButtonClose As System.Windows.Forms.Button
End Class
