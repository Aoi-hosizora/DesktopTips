<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SearchForm
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
        Me.ButtonBack = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'ListView
        '
        Me.ListView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListView.FormattingEnabled = True
        Me.ListView.ItemHeight = 17
        Me.ListView.Location = New System.Drawing.Point(7, 31)
        Me.ListView.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ListView.Name = "ListView"
        Me.ListView.Size = New System.Drawing.Size(348, 259)
        Me.ListView.TabIndex = 0
        '
        'LabelResult
        '
        Me.LabelResult.AutoSize = True
        Me.LabelResult.Location = New System.Drawing.Point(12, 7)
        Me.LabelResult.Name = "LabelResult"
        Me.LabelResult.Size = New System.Drawing.Size(183, 17)
        Me.LabelResult.TabIndex = 1
        Me.LabelResult.Text = """xxx"" 的搜索结果：(共找到 0 项)"
        '
        'ButtonSearch
        '
        Me.ButtonSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonSearch.Location = New System.Drawing.Point(199, 297)
        Me.ButtonSearch.Name = "ButtonSearch"
        Me.ButtonSearch.Size = New System.Drawing.Size(75, 26)
        Me.ButtonSearch.TabIndex = 2
        Me.ButtonSearch.Text = "新查找(&F)"
        Me.ButtonSearch.UseVisualStyleBackColor = True
        '
        'ButtonBack
        '
        Me.ButtonBack.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonBack.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.ButtonBack.Location = New System.Drawing.Point(280, 297)
        Me.ButtonBack.Name = "ButtonBack"
        Me.ButtonBack.Size = New System.Drawing.Size(75, 26)
        Me.ButtonBack.TabIndex = 3
        Me.ButtonBack.Text = "返回(&B)"
        Me.ButtonBack.UseVisualStyleBackColor = True
        '
        'SearchForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.ButtonBack
        Me.ClientSize = New System.Drawing.Size(362, 330)
        Me.Controls.Add(Me.ButtonBack)
        Me.Controls.Add(Me.ButtonSearch)
        Me.Controls.Add(Me.LabelResult)
        Me.Controls.Add(Me.ListView)
        Me.Font = New System.Drawing.Font("Microsoft YaHei UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "SearchForm"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "搜索"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ListView As System.Windows.Forms.ListBox
    Friend WithEvents LabelResult As System.Windows.Forms.Label
    Friend WithEvents ButtonSearch As System.Windows.Forms.Button
    Friend WithEvents ButtonBack As System.Windows.Forms.Button
End Class
