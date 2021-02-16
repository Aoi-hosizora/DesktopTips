Imports System.IO
Imports System.Threading.Tasks

''' <summary>
''' 编辑 TipItem
''' </summary>
Public Class TipEditDialog
    ''' <summary>
    ''' 未修改前的内容
    ''' </summary>
    Private _previousContent As String = ""

    ''' <summary>
    ''' 是否已经修改
    ''' </summary>
    Private _changed As Boolean = False

    ''' <summary>
    ''' 判断是否已经关闭
    ''' </summary>
    Private _closed As Boolean = False

    ''' <summary>
    ''' 是否允许保存，并且指定保存回调
    ''' </summary>
    Private _saveCallback As Action(Of String)

    ''' <summary>
    ''' 显示编辑对话框
    ''' </summary>
    Public Overloads Shared Function ShowDialog(message As String, title As String, Optional content As String = "", Optional saveCallback As Action(Of String) = Nothing) As String
        Dim dlg As New TipEditDialog
        With dlg
            .Text = title
            .LabelMessage.Text = message ' <<<
            .TextBoxContent.Text = content ' <<<
            ._previousContent = content
            .ButtonOK.Enabled = content <> ""
            ._saveCallback = saveCallback

            Dim newWidth = TextRenderer.MeasureText(message, .Font, .LabelMessage.Size).Width
            If newWidth > Screen.PrimaryScreen.Bounds.Width * 8 / 9 Then
                newWidth = Screen.PrimaryScreen.Bounds.Width * 8 / 9
            End If
            .Width += newWidth - .LabelMessage.Width
            Dim newHeight% = TextRenderer.MeasureText(message, .Font, .LabelMessage.Size).Height
            If newHeight > Screen.PrimaryScreen.Bounds.Height * 3 / 5 Then
                newHeight = Screen.PrimaryScreen.Bounds.Height * 3 / 5
            End If
            Dim dh = newHeight - .LabelMessage.Height
            .Height += dh
            .SplitContainer.SplitterDistance += dh

            .TextBoxContent.Focus()
            .TextBoxContent.Select()
        End With

        If dlg.ShowDialog() = vbCancel Then Return ""
        Return dlg.TextBoxContent.Text.Trim()
    End Function

    ''' <summary>
    ''' 关闭检查
    ''' </summary>
    Private Sub TipsEditDialog_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If _changed Then
            Dim result = MessageBoxEx.Show("内容已经变更，确定不保存退出吗？", "退出", MessageBoxButtons.OKCancel, MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1, Me, {"不保存退出", "取消"})
            If result = vbCancel Then
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub TipEditDialog_Load(sender As Object, e As EventArgs) Handles Me.Load
        MenuSave.Enabled = _saveCallback IsNot Nothing
    End Sub

    Private Sub TipEditDialog_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        _closed = True
    End Sub

    Private Sub OK_Click(sender As Object, e As EventArgs) Handles ButtonOK.Click, MenuOK.Click
        _changed = False
        DialogResult = DialogResult.OK
        Close()
    End Sub

    Private Sub MenuSave_Click(sender As Object, e As EventArgs) Handles MenuSave.Click
        _changed = False
        _previousContent = TextBoxContent.Text.Trim()
        _saveCallback?.Invoke(_previousContent)
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        _changed = TextBoxContent.Text.Trim() <> _previousContent.Trim()
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

    Private Sub TextBoxContent_TextChanged(sender As Object, e As EventArgs) Handles TextBoxContent.TextChanged
        ButtonOK.Enabled = TextBoxContent.Text.Trim() <> ""
        _changed = TextBoxContent.Text.Trim() <> _previousContent.Trim()
    End Sub

    Private Sub TipEditDialog_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter And e.Control Then
            e.Handled = True
            OK_Click(sender, New EventArgs) ' Ctrl+Enter
        End If
    End Sub

    Private Sub TextBoxContent_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles TextBoxContent.PreviewKeyDown
        If e.KeyData = Keys.Tab Then
            e.IsInputKey = True ' Tab
        ElseIf e.KeyData = (Keys.Tab Or Keys.ShiftKey) Then
            e.IsInputKey = True ' Ctrl+Tab
        End If
    End Sub

    Private Sub TextBoxContent_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxContent.KeyDown
        If e.KeyCode = Keys.A And e.Control Then
            e.Handled = True
            TextBoxContent.SelectAll() ' Ctrl+A
        Else If e.KeyCode = Keys.S And e.Control Then
            e.Handled = True
            MenuSave_Click(sender, New EventArgs) ' Ctrl+S
        End If
    End Sub

    Private Sub MenuUploadImage_Click(sender As Object, e As EventArgs) Handles MenuUploadImage.Click
        ' 获取 Token
        Dim token = My.Settings.SmToken
        If token = "x" Then
            MessageBox.Show("没有设置 SmToken，请先设置。", "上传图片", MessageBoxButtons.Ok, MessageBoxIcon.Exclamation)
            Return
        End If

        ' 选择文件
        Dim dlg As New OpenFileDialog
        dlg.InitialDirectory = Environment.SpecialFolder.Desktop
        dlg.Filter = "JPEG 图片 (*.jpg, *.jpeg)|*.jpg;*.jpeg|PNG 图片 (*.png)|*.png|BMP 图片 (*.bmp)|*.bmp|GIF 图片 (*.gif)|*.gif"
        dlg.FilterIndex = 0
        dlg.CheckFileExists = True
        If dlg.ShowDialog() <> vbOk Then Return
        Dim ok = MessageBox.Show($"确定上传图片 ""{Path.GetFileName(dlg.Filename)}""？", "上传图片", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If ok = vbNo Then Return

        ' 上传
        UploadImage(dlg.Filename, token)
    End Sub

    Private Sub MenuClipboardImage_Click(sender As Object, e As EventArgs) Handles MenuClipboardImage.Click
        ' 获取 Token
        Dim token = My.Settings.SmToken
        If token = "x" Then
            MessageBox.Show("没有设置 SmToken，请先设置。", "插入图片", MessageBoxButtons.Ok, MessageBoxIcon.Exclamation)
            Return
        End If

        ' 获取图片并保存
        Dim image = Clipboard.GetImage()
        If image Is Nothing Then
            MessageBox.Show("剪贴板内没有图片。", "插入图片", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If
        Dim filename = Path.GetTempFileName()
        image.Save(filename)
        MessageBox.Show(filename)

        ' 上传
        UploadImage(filename, token, True)
    End Sub

    ''' <summary>
    ''' 上传图片
    ''' </summary>
    Private Sub UploadImage(filename As String, token As String, Optional needDelete As Boolean = False)
        SmmsUtil.UploadImage(filename, token).ContinueWith(Sub(t As Task(Of SmmsUtil.TaskResult(Of Tuple(Of String, String))))
            ' 窗口未关闭
            If Not _closed Then
                Dim r = t.Result
                If Not r.Success Then
                    MessageBox.Show($"图片上传失败，请重试。{vbNewLine}错误信息：{r.Ex.Message}", "上传图片", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                End If

                ' 成功
                Dim tuple = r.Result
                Dim ok2 = MessageBoxEx.Show($"图片 ""{tuple.Item1}"" 上传成功，获得图片链接：{vbNewLine}{tuple.Item2}", "上传图片", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information,
                    Me, {"插入", "复制", "取消"})
                If ok2 = vbNo Then
                    Clipboard.SetText(tuple.Item2)
                Else If ok2 = vbYes
                    TextBoxContent.Text &= vbNewLine & vbNewLine & $"![{tuple.Item1}]({tuple.Item2})"
                End If
            End If

            ' 删除
            If needDelete Then
                Dim file = New FileInfo(filename)
                file.Delete()
            End If
        End Sub)
    End Sub
End Class
