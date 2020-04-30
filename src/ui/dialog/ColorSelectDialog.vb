Public Class ColorSelectDialog
    Public Property GetDelColorFunc As Func(Of TipColor)
    Public Property GetColorsFunc As Func(Of IEnumerable(Of TipColor))
    Public Property OkFunc As Action(Of Integer) ' TipColor.Id

    Private _delColor As TipColor
    Private _colors As IEnumerable(Of TipColor)

    Private Sub ColorSelectDialog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Controls.Remove(TestCommandLink1)
        Controls.Remove(TestCommandLink2)
        If GetColorsFunc Is Nothing OrElse GetDelColorFunc Is Nothing Then
            Me.Close()
            Return
        End If
        _delColor = GetDelColorFunc.Invoke()
        _colors = GetColorsFunc.Invoke()
        TitleWarningBox.Text = $"请选择一个颜色代替被删除的颜色 ""{_delColor.Name}"""

        Dim currentPos As New Point(12, 7)
        Dim marginInPanel As New Size(12, 7)
        Const buttonHeight = 57
        Dim buttonWidth = CommandPanel.Width - 2 * marginInPanel.Width

        For Each c As TipColor In _colors
            Dim button As New CommandLink() With {.Text = c.Name, .Tag = c, .CommandLinkNote = $"{c.HexColor} | {c.RgbColor}"}
            button.Location = currentPos
            currentPos.Y += buttonHeight + marginInPanel.Height
            button.Size = New Size(buttonWidth, buttonHeight)
            AddHandler button.Click, AddressOf ColorCommandLink_Click
            CommandPanel.Controls.Add(button)
        Next
    End Sub

    Private Sub ColorCommandLink_Click(sender As CommandLink, e As EventArgs)
        Dim color = CType(sender.Tag, TipColor)
        Dim ok = MessageBoxEx.Show($"确定将和颜色 ""{_delColor.Name}"" 相关联的所有项目替换成 ""{color.Name}"" 并删除吗？", "删除",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question, Me)
        If ok = vbYes
            If OkFunc IsNot Nothing Then
                OkFunc.Invoke(color.Id)
            End If
            Me.Close()
        End If
    End Sub

    Private Sub ButtonExit_Click(sender As Object, e As EventArgs) Handles ButtonExit.Click
        Me.Close()
    End Sub
End Class
