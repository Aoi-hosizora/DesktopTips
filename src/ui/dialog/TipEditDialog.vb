''' <summary>
''' 编辑 TipItem
''' </summary>
Public Class TipEditDialog
    ''' <summary>
    ''' 未修改前的内容
    ''' </summary>
    Private _originContent As String = ""

    ''' <summary>
    ''' 未保存前的内容
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
    Public Overloads Shared Function ShowDialog(message As String, title As String, Optional content As String = "", Optional ByRef textType As CommonUtil.TextType = CommonUtil.TextType.Plain,
                                                Optional saveCallback As Action(Of String) = Nothing) As String
        Dim dlg As New TipEditDialog
        With dlg
            .Text = title
            .LabelMessage.Text = message
            ._originContent = content
            ._previousContent = content
            .TextBoxOrigin.Text = content
            .TextBoxContent.Text = content
            .ButtonOK.Enabled = content <> ""
            .SplitContainerTextBox.Panel2Collapsed = True
            .ComboBoxTextType.SelectedIndex = CommonUtil.TextTypeToIndex(textType)
            ._saveCallback = saveCallback
            ._changed = False
            .Text = .Text.TrimStart("*")

            Dim newSize = TextRenderer.MeasureText(content, .Font, Size.Empty)
            Dim maxWidth = Screen.PrimaryScreen.Bounds.Width * 8 / 9
            Dim maxHeight = Screen.PrimaryScreen.Bounds.Height * 3 / 5
            .Width += Math.Min(newSize.Width + 30, maxWidth) - .TextBoxContent.Width
            .Height += Math.Min(newSize.Height + 50, maxHeight) - .TextBoxContent.Height

            .TextBoxContent.Focus()
            .TextBoxContent.Select()

            If .ShowDialog() = vbCancel Then Return ""
            textType = CommonUtil.IndexToTextType(.ComboBoxTextType.SelectedIndex)
            Return .TextBoxContent.Text.Trim()
        End With
    End Function

    Private Sub TipEditDialog_Load(sender As Object, e As EventArgs) Handles Me.Load
        MenuSave.Enabled = _saveCallback IsNot Nothing
    End Sub

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

    Private Sub OK_Click(sender As Object, e As EventArgs) Handles ButtonOK.Click, MenuOK.Click
        _changed = False
        DialogResult = DialogResult.OK
        Close()
    End Sub

    Private Sub Save_Click(sender As Object, e As EventArgs) Handles MenuSave.Click
        _changed = False
        _previousContent = TextBoxContent.Text.Trim()
        _saveCallback?.Invoke(_previousContent)
        Text = Text.TrimStart("*")
    End Sub

    Private Sub Cancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        _changed = TextBoxContent.Text.Trim() <> _previousContent.Trim()
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

    Private Sub ShowOrigin_Click(sender As Object, e As EventArgs) Handles ButtonShowOrigin.Click
        If SplitContainerTextBox.Panel2Collapsed Then
            SplitContainerTextBox.Panel2Collapsed = False
            Width = Width * 2 - 40
            ButtonShowOrigin.Text = "隐藏原文"
        Else
            SplitContainerTextBox.Panel2Collapsed = True
            Width = (Width + 40) / 2
            ButtonShowOrigin.Text = "显示原文"
        End If
    End Sub

    Private Sub TextBoxContent_TextChanged(sender As Object, e As EventArgs) Handles TextBoxContent.TextChanged
        ButtonOK.Enabled = TextBoxContent.Text.Trim() <> ""
        _changed = TextBoxContent.Text.Trim() <> _previousContent.Trim()
        If Not Text.StartsWith("*") Then
            Text = "*" & Text
        End If
    End Sub

    Private Sub TipEditDialog_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = (Keys.Enter And e.Control) Then
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
        If e.KeyCode = (Keys.A And e.Control) Then
            e.Handled = True
            TextBoxContent.SelectAll() ' Ctrl+A
        ElseIf e.KeyCode = (Keys.S And e.Control) Then
            If _saveCallback IsNot Nothing Then
                e.Handled = True
                Save_Click(sender, New EventArgs) ' Ctrl+S
            End If
        End If
    End Sub
End Class
