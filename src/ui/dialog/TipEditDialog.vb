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
End Class
