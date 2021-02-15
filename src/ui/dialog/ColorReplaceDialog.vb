''' <summary>
''' 替换颜色
''' </summary>
Public Class ColorReplaceDialog
    ''' <summary>
    ''' 完成回调，参数表示 TipColor 的 Id
    ''' </summary>
    Public Property OkCallback As Action(Of Integer)

    ''' <summary>
    ''' 指定的颜色
    ''' </summary>
    Public Property SelectedColor As TipColor

    ''' <summary>
    ''' 指定所有颜色，不用插入 Nothing 来表示取消高亮
    ''' </summary>
    Public Property AllColors As IEnumerable(Of TipColor)

    Private Sub ColorSelectDialog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Controls.Remove(TestCommandLink1)
        Controls.Remove(TestCommandLink2)
        CommandPanel.Controls.Clear()

        TitleWarningBox.Text = $"请选择一个颜色替换颜色 ""{SelectedColor.Name}"""
        Dim currentPos As New Point(12, 7)
        Dim marginInPanel As New Size(12, 7)
        Const buttonHeight = 57
        Dim buttonWidth = CommandPanel.Width - 2 * marginInPanel.Width

        Dim l = AllColors.ToList()
        l.Insert(0, Nothing)
        AllColors = l
        ButtonExit.TabIndex = AllColors.Count() + 1
        For i = 0 To AllColors.Count() - 1
            Dim c = AllColors.ElementAt(i)
            Dim btnTitle = If(c?.Name, "取消高亮")
            Dim hint = If(c Is Nothing, "", $"{c.HexColor} | {ColorToRgb(c.Color)}")
            Dim button As New CommandLink() With {
                .Text = btnTitle, .Tag = c, .CommandLinkNote = hint, 
                .TabIndex = i + 1,.Location = currentPos, .Size = New Size(buttonWidth - 20, buttonHeight) }
            currentPos.Y += buttonHeight + marginInPanel.Height
            AddHandler button.Click, AddressOf ColorCommandLink_Click
            CommandPanel.Controls.Add(button)
        Next
    End Sub

    Private Sub ButtonExit_Click(sender As Object, e As EventArgs) Handles ButtonExit.Click
        Close()
    End Sub

    ''' <summary>
    ''' 选择颜色
    ''' </summary>
    Private Sub ColorCommandLink_Click(sender As CommandLink, e As EventArgs)
        Dim color = TryCast(sender.Tag, TipColor)
        Dim flag = If (color IsNot Nothing, $"替换成""{color.Name}""", "取消高亮")
        Dim id = If(color?.Id, -1)
        Dim ok = MessageBoxEx.Show($"确定将和颜色 ""{SelectedColor.Name}"" 相关联的所有项目{flag}，并删除原颜色吗？", "删除",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question, Me)
        If ok = vbYes
            OkCallback?.Invoke(id)
            Close()
        End If
    End Sub

    ''' <summary>
    ''' Color 转 RGB 字符串
    ''' </summary>
    Private Function ColorToRgb(c As Color) As String
        Return String.Format("{0}, {1}, {2}", c.R, c.G, c.B)
    End Function
End Class
