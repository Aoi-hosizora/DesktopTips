﻿Partial Public Class TempForm
    Implements TempFormContract.IView

    Private ReadOnly _globalPresenter As TempFormContract.IGlobalPresenter = New TempFormGlobalPresenter(Me)
    Private ReadOnly _tipPresenter As TempFormContract.ITipPresenter = New TempFormTipPresenter(Me)
    Private ReadOnly _tabPresenter As TempFormContract.ITabPresenter = New TempFormTabPresenter(Me)

    Public Function GetMe() As TempForm Implements TempFormContract.IView.GetMe
        Return Me
    End Function

    Public Sub ShowTextForm(title As String, content As String, textColor As Color) Implements TempFormContract.IView.ShowTextForm
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

    Public Sub FocusItem(tabIdx As Integer, tipIdx As Integer) Implements TempFormContract.IView.FocusItem
        If tabIdx < m_TabView.Tabs.Count
            m_TabView.SelectedTabIndex = tabIdx
            If tipIdx < m_TipListBox.ItemCount Then
                m_TipListBox.SetSelectOnly(tipIdx)
            End If
        End If
    End Sub
End Class
