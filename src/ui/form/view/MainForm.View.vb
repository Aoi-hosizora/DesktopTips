Partial Public Class MainForm
    Implements MainFormContract.IView

    Private ReadOnly _globalPresenter As MainFormContract.IGlobalPresenter = New MainFormGlobalPresenter(Me)
    Private ReadOnly _tipPresenter As MainFormContract.ITipPresenter = New MainFormTipPresenter(Me)
    Private ReadOnly _tabPresenter As MainFormContract.ITabPresenter = New MainFormTabPresenter(Me)

    Public Function GetMe() As MainForm Implements MainFormContract.IView.GetMe
        Return Me
    End Function

    Public Sub ShowTextForm(title As String, content As String, textColor As Color) Implements MainFormContract.IView.ShowTextForm
        Dim formSize = New Size(500, 300)

        Dim textBox As New TextBox With {
            .Text = content, .ReadOnly = True, .Multiline = True, .ScrollBars = ScrollBars.Both, .WordWrap = False,
            .BackColor = Color.White, .ForeColor = textColor, .Font = New Font("Microsoft YaHei UI", 9.0!),
            .Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Top}
        Dim form As New BaseEscCloseForm With {
            .Text = title, .Size = formSize, .TopMost = True, .FormBorderStyle = FormBorderStyle.Sizable}
        form.Controls.Add(textBox)
        textBox.Dock = DockStyle.Fill

        form.Show()
        form.Top = Me.Top
        form.Left = Me.Left - formSize.Width - 15
        textBox.Select(0, 0)
    End Sub

    Public Sub FocusItem(tabIdx As Integer, tipIdx As Integer) Implements MainFormContract.IView.FocusItem
        If tabIdx < m_TabView.TabCount Then
            m_TabView.SelectedTabIndex = tabIdx
            If tipIdx < m_ListView.ItemCount Then
                m_ListView.SetSelectOnly(tipIdx)
            End If
        End If
    End Sub
End Class
