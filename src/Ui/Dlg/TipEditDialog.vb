''' <summary>
''' 编辑 TipItem
''' </summary>
Public Class TipEditDialog
    ''' <summary>
    ''' 原始标题
    ''' </summary>
    Private _originTitle As String = ""

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
            ._originTitle = title
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
            .ContextMenuStripOK.Renderer = New MenuNativeRenderer()

            Dim newSize = TextRenderer.MeasureText(content, .Font, Size.Empty)
            Dim maxWidth = Screen.PrimaryScreen.Bounds.Width * 8 / 9
            Dim maxHeight = Screen.PrimaryScreen.Bounds.Height * 3 / 5
            .Width += Math.Min(newSize.Width + 45, maxWidth) - .TextBoxContent.Width
            .Height += Math.Min(newSize.Height + 50, maxHeight) - .TextBoxContent.Height

            .TextBoxContent.Focus()
            .TextBoxContent.Select()

            If .ShowDialog() = DialogResult.Cancel Then Return ""
            textType = CommonUtil.IndexToTextType(.ComboBoxTextType.SelectedIndex)
            Return .TextBoxContent.Text.Trim()
        End With
    End Function

    Private Sub TipEditDialog_Load(sender As Object, e As EventArgs) Handles Me.Load
        MenuSave.Enabled = _saveCallback IsNot Nothing
    End Sub

    ''' <summary>
    ''' 更新标题，包括变更和缩放
    ''' </summary>
    Private Sub UpdateTitle()
        If Not _changed Then
            Text = _originTitle
        Else
            Text = "*" & _originTitle
        End If
        If Math.Abs(TextBoxContent.Font.Size - Font.Size) > 0.01 Then
            Dim ratio As Single = TextBoxContent.Font.Size / Font.Size
            Text &= " (" & ratio.ToString("P0") & ")"
        End If
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

    ''' <summary>
    ''' 更新字符总数提示
    ''' </summary>
    Private Sub RefreshCountLabel()
        Dim totalLen = TextBoxContent.Text.Count
        Dim total = If(totalLen > 9999, $"超过 {totalLen}", totalLen.ToString())
        Dim currLen = TextBoxContent.SelectedText.Count
        Dim curr = If(currLen > 9999, $"超过 {currLen}", currLen.ToString())
        LabelCount.Text = If(currLen > 0, $"字符总数：{curr} / {total}", $"字符总数：{total}")
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
        UpdateTitle()
    End Sub

    Private Sub Cancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        _changed = TextBoxContent.Text.Trim() <> _previousContent.Trim()
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

    Private Sub MenuShowOrigin_Click(sender As Object, e As EventArgs) Handles MenuShowOrigin.Click
        MenuShowOrigin.Checked = Not MenuShowOrigin.Checked
        If MenuShowOrigin.Checked Then
            SplitContainerTextBox.Panel2Collapsed = False
            Width = Width * 2 - 40
        Else
            SplitContainerTextBox.Panel2Collapsed = True
            Width = (Width + 40) / 2
        End If
    End Sub

    Private Sub TextBoxContent_TextChanged(sender As Object, e As EventArgs) Handles TextBoxContent.TextChanged
        ButtonOK.Enabled = TextBoxContent.Text.Trim() <> ""
        _changed = TextBoxContent.Text.Trim() <> _previousContent.Trim()
        UpdateTitle()
        RefreshCountLabel()
    End Sub

    Private Sub TextBoxContent_MouseXXX(sender As Object, e As MouseEventArgs) Handles TextBoxContent.MouseDown, TextBoxContent.MouseUp, TextBoxContent.MouseMove
        If e.Button = MouseButtons.Left Then
            RefreshCountLabel()
        End If
    End Sub

    Private Sub TextBoxContent_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBoxContent.KeyPress
        RefreshCountLabel()
    End Sub

    Private Sub TextBoxContent_KeyUp(sender As Object, e As KeyEventArgs) Handles TextBoxContent.KeyUp
        RefreshCountLabel()
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

    ''' <summary>
    ''' 缩放文本框，输入 MouseWheel 的 Delta
    ''' </summary>
    Private Sub ZoomTextBox(delta As Double)
        Dim origin = TextBoxContent.Font
        Dim newSize = origin.Size + (delta / 120)
        If newSize > 1 AndAlso newSize < Single.MaxValue Then
            TextBoxContent.Font = New Font(origin.FontFamily, newSize)
            UpdateTitle()
        End If
    End Sub

    Private Sub TextBoxContent_MouseWheel(sender As Object, e As MouseEventArgs) Handles TextBoxContent.MouseWheel
        Console.WriteLine(e.Delta)
        If My.Computer.Keyboard.CtrlKeyDown Then
            ZoomTextBox(e.Delta)
        End If
    End Sub

    Private Sub MenuZoomUp_Click(sender As Object, e As EventArgs) Handles MenuZoomUp.Click
        ZoomTextBox(SystemInformation.MouseWheelScrollDelta)
    End Sub

    Private Sub MenuZoomDown_Click(sender As Object, e As EventArgs) Handles MenuZoomDown.Click
        ZoomTextBox(-SystemInformation.MouseWheelScrollDelta)
    End Sub

    Private Sub MenuZoomRestore_Click(sender As Object, e As EventArgs) Handles MenuZoomRestore.Click
        TextBoxContent.Font = Font
        UpdateTitle()
    End Sub
End Class
