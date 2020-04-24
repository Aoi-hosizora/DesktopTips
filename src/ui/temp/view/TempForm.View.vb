Partial Public Class TempForm
    Implements TempFormContract.IView

    Private ReadOnly _globalPresenter As TempFormContract.IGlobalPresenter = New TempFormGlobalPresenter(Me)
    Private ReadOnly _listPresenter As TempFormContract.IListPresenter = New TempFormListPresenter(Me)

    ''' <summary>
    ''' 引用 Form
    ''' </summary>
    Public Function GetMe() As TempForm Implements TempFormContract.IView.GetMe
        Return Me
    End Function

    ''' <summary>
    ''' 显示文本窗体
    ''' </summary>
    Public Sub ShowTextForm(title As String, content As String, textColor As Color) Implements TempFormContract.IView.ShowTextForm
        Dim formSize As Size = New Size(500, 300)
        Dim textSize As Size = New Size(formSize.Width - 16, formSize.Height - 39)

        Dim textBox As New TextBox With {
            .Text = content, .ReadOnly = True, .Multiline = True, .ScrollBars = ScrollBars.Both, .WordWrap = False,
            .Size = textSize, .BackColor = Color.White, .ForeColor = textColor, .Font = New System.Drawing.Font("Microsoft YaHei UI", 9.0!),
            .Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Top
        }
        Dim form As New BaseEscCloseForm With {
            .Text = title, .Size = formSize, .TopMost = True, .FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
        }
        form.Controls.Add(textBox)
        form.Show()

        form.Top = Me.Top
        form.Left = Me.Left - formSize.Width - 15
        textBox.Select(0, 0)
    End Sub

End Class
