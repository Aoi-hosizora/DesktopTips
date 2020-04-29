Public Class SearchDialog
    Public SearchFunc As Action
    Public HighlightFunc As Action(Of Integer, Integer) ' TabIndex, TipIndex
    Public GetFunc As Func(Of IEnumerable(Of Tuple(Of Integer, Integer))) ' TabIndex, TipIndex

    Public Property SearchText As String
    Private _searchResult As New List(Of Tuple(Of Integer, Integer))

    Private Sub SearchForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetFunc Is Nothing Then
            Me.Close()
            Return
        End If
        _searchResult = GetFunc.Invoke()
        ShowSearchList()
    End Sub

    Private Sub ShowSearchList()
        LabelResult.AutoEllipsis = True
        LabelResult.Text = $"共找到 {_searchResult.Count} 个搜索结果: ""{SearchText}"""
        ListView.Items.Clear()
        For Each tuple As Tuple(Of Integer, Integer) In _searchResult
            Dim tab As Tab = GlobalModel.Tabs.Item(tuple.Item1)
            ListView.Items.Add($"[{tab.Title}] - {tab.Tips.Item(tuple.Item2).Content}")
        Next
    End Sub

    Private Sub ButtonClose_Click(sender As Object, e As EventArgs) Handles ButtonClose.Click
        Me.Close()
    End Sub

    Private Sub ButtonSearch_Click(sender As Object, e As EventArgs) Handles ButtonSearch.Click
        Me.Close()
        If SearchFunc IsNot Nothing Then
            SearchFunc.Invoke()
        End If
    End Sub

    Private Sub ListView_DoubleClick(sender As Object, e As EventArgs) Handles ListView.DoubleClick
        If ListView.SelectedIndex <> - 1 Then
            If HighlightFunc IsNot Nothing Then
                Dim result As Tuple(Of Integer, Integer) = _searchResult.ElementAt(ListView.SelectedIndex)
                HighlightFunc.Invoke(result.Item1, result.Item2)
            End If
        End If
    End Sub

    Private Sub ListView_MouseDown(sender As Object, e As MouseEventArgs) Handles ListView.MouseDown
        If ListView.IndexFromPoint(e.X, e.Y) = - 1 Then
            ListView.ClearSelected()
        End If
    End Sub
End Class
