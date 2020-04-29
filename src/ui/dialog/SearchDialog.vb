Public Class SearchDialog
    Public Property SearchText As String
    Public Property SearchResult As List(Of Tuple(Of Integer, Integer))

    Private Sub SearchForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim showText As String = """{0}"" 的搜索结果：(共找到 " & SearchResult.Count & " 项)"
        Dim searchResultText As String = SearchText

        Dim graphics As Graphics = CreateGraphics()
        Dim sizeF As SizeF = graphics.MeasureString(String.Format(showText, searchResultText), LabelResult.Font)

        While sizeF.Width >= LabelResult.Width - 10
            searchResultText = searchResultText.Substring(0, searchResultText.Length - 1)
            sizeF = graphics.MeasureString(String.Format(showText, searchResultText), LabelResult.Font)
        End While

        ' Add Ellipsis
        LabelResult.Text = String.Format(showText, searchResultText & If(SearchText.Length <> searchResultText.Length, "...", ""))

        ListView.Items.Clear()
        For Each tuple As Tuple(Of Integer, Integer) In SearchResult
            Dim tab As Tab = GlobalModel.Tabs.Item(tuple.Item1)
            ListView.Items.Add("[" & tab.Title & "] - " & tab.Tips.Item(tuple.Item2).Content)
        Next
    End Sub

    Private Sub ButtonBack_Click(sender As Object, e As EventArgs) Handles ButtonBack.Click
        Me.Close()
        MainForm.Focus()
    End Sub

    Private Sub ButtonSearch_Click(sender As Object, e As EventArgs) Handles ButtonSearch.Click
        Me.Close()
        MainForm.ListPopupMenuFind_Click(MainForm.ListPopupMenuFind, New EventArgs)
    End Sub

    Private Sub ListView_DoubleClick(sender As Object, e As EventArgs) Handles ListView.DoubleClick
        If ListView.SelectedIndex <> - 1 And ListView.SelectedIndex <> 65535 Then
            MainForm.Focus()
            Dim tpl As Tuple(Of Integer, Integer) = SearchResult.ElementAt(ListView.SelectedIndex)
            MainForm.TabStrip.SelectedTabIndex = tpl.Item1
            MainForm.ListView.SelectedIndex = tpl.Item2
        End If
    End Sub
End Class
