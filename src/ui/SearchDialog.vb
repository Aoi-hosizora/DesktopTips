﻿Imports SU = DesktopTips.GlobalModel

Public Class SearchDialog

    Public Property _SearchText As String
    Public Property _SearchResult As List(Of Tuple(Of Integer, Integer))

    Private Sub SearchForm_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim ShowLabel As String = """{0}"" 的搜索结果：(共找到 " & _SearchResult.Count & " 项)"
        Dim SearchTextLong As String = _SearchText

        Dim graphics As Graphics = CreateGraphics()
        Dim sizeF As SizeF = graphics.MeasureString(String.Format(ShowLabel, SearchTextLong), LabelResult.Font)

        While sizeF.Width >= LabelResult.Width - 10
            SearchTextLong = SearchTextLong.Substring(0, SearchTextLong.Length - 1)
            sizeF = graphics.MeasureString(String.Format(ShowLabel, SearchTextLong), LabelResult.Font)
        End While

        ' Add Ellipsis
        LabelResult.Text = String.Format(ShowLabel, SearchTextLong & If(_SearchText.Length <> SearchTextLong.Length, "...", ""))

        ListView.Items.Clear()
        For Each Tpl As Tuple(Of Integer, Integer) In _SearchResult
            Dim tab As Tab = SU.Tabs.Item(Tpl.Item1)
            ListView.Items.Add("[" & tab.Title & "] - " & tab.Tips.Item(Tpl.Item2).TipContent)
        Next
    End Sub

    Private Sub ButtonBack_Click(sender As System.Object, e As System.EventArgs) Handles ButtonBack.Click
        Me.Close()
        MainForm.Focus()
    End Sub

    Private Sub ButtonSearch_Click(sender As System.Object, e As System.EventArgs) Handles ButtonSearch.Click
        Me.Close()
        MainForm.ListPopupMenuFind_Click(MainForm.ListPopupMenuFind, New EventArgs)
    End Sub

    Private Sub ListView_DoubleClick(sender As Object, e As System.EventArgs) Handles ListView.DoubleClick
        If ListView.SelectedIndex <> -1 And ListView.SelectedIndex <> 65535 Then
            MainForm.Focus()
            Dim Tpl As Tuple(Of Integer, Integer) = _SearchResult.ElementAt(ListView.SelectedIndex)
            MainForm.TabStrip.SelectedTabIndex = Tpl.Item1
            MainForm.ListView.SelectedIndex = Tpl.Item2
        End If
    End Sub
End Class