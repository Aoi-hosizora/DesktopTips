Public Class SearchDialog
    Public Delegate Sub ReSearchFunc()

    Public Delegate Sub TurnToFunc(tabIndex As Integer, tipIndex As Integer)

    Public Delegate Function SearchFunc() As List(Of Tuple(Of Integer, Integer)) ' TabIndex, TipIndex

    Public ReSearchCallback As ReSearchFunc
    Public TurnToCallback As TurnToFunc
    Public SearchCallback As SearchFunc

    Public Property SearchText As String
    Private _searchResult As New List(Of Tuple(Of Integer, Integer))

    Private Sub SearchForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If SearchCallback Is Nothing Then
            Me.Close()
            Return
        End If
        _searchResult = SearchCallback.Invoke()
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
        If ReSearchCallback IsNot Nothing Then
            ReSearchCallback.Invoke()
        End If
    End Sub

    Private Sub ListView_DoubleClick(sender As Object, e As EventArgs) Handles ListView.DoubleClick
        If ListView.SelectedIndex <> - 1 Then
            If TurnToCallback IsNot Nothing Then
                Dim result As Tuple(Of Integer, Integer) = _searchResult.ElementAt(ListView.SelectedIndex)
                TurnToCallback.Invoke(result.Item1, result.Item2)
            End If
        End If
    End Sub
End Class
