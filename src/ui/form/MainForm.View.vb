Partial Public Class MainForm
    Implements MainFormContract.IView

    Private ReadOnly _globalPresenter As MainFormContract.IGlobalPresenter = New MainFormGlobalPresenter(Me)
    Private ReadOnly _tipPresenter As MainFormContract.ITipPresenter = New MainFormTipPresenter(Me)
    Private ReadOnly _tabPresenter As MainFormContract.ITabPresenter = New MainFormTabPresenter(Me)

    Public Function GetMe() As MainForm Implements MainFormContract.IView.GetMe
        Return Me
    End Function

    Public Sub FocusItem(tabIdx As Integer, tipIdx As Integer) Implements MainFormContract.IView.FocusItem
        If tabIdx < m_TabView.TabCount Then
            m_TabView.SelectedTabIndex = tabIdx
            If tipIdx < m_ListView.ItemCount Then
                m_ListView.SetSelectedItems(tipIdx)
            End If
        End If
    End Sub

    Public Sub ShowTextForm(title As String, contents As List(Of Tuple(Of String, Color))) Implements MainFormContract.IView.ShowTextForm
        Dim formSize = New Size(500, 300)
        Dim form As New BaseEscCbForm With {.Text = title, .Size = formSize, .TopMost = True, .FormBorderStyle = FormBorderStyle.Sizable}
        Dim f = New Font("Microsoft YaHei UI", 10.0!)
        Dim richBox As New RichTextBox With {
            .ReadOnly = True, .Multiline = True, .ScrollBars = RichTextBoxScrollBars.ForcedBoth, .WordWrap = False,
            .BackColor = Color.White, .Font = f, .HideSelection = False, .DetectUrls = False, .ShortcutsEnabled = True,
            .Location = New Point(0, 0), .Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Top}
        form.Controls.Add(richBox)
        richBox.Dock = DockStyle.Fill
        AddHandler form.Load, Sub()
                                  form.Top = Top
                                  form.Left = Left - formSize.Width - 8
                                  richBox.Select(0, 0)
                              End Sub

        For Each content In contents
            richBox.SelectionColor = content.Item2
            richBox.SelectionFont = f
            richBox.SelectedText &= content.Item1 & vbNewLine
        Next
        form.Show()
    End Sub
End Class
