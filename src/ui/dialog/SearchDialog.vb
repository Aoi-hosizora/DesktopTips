''' <summary>
''' 搜索
''' </summary>
Public Class SearchDialog
    ''' <summary>
    ''' 搜索的关键字
    ''' </summary>
    Public Property SearchText As String

    ''' <summary>
    ''' 搜索的结果，元组包含 TabIndex, TipIndex
    ''' </summary>
    Public Property SearchResult As IEnumerable(Of Tuple(Of Integer, Integer))

    ''' <summary>
    ''' 选择回调，参数为 TabIndex, TipIndex
    ''' </summary>
    Public Property SelectCallback As Action(Of Integer, Integer)

    ''' <summary>
    ''' 新搜索回调
    ''' </summary>
    Public Property NewSearchCallback As Action

    Private Sub SearchForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LabelResult.AutoEllipsis = True
        LabelResult.Text = $"共找到 {SearchResult.Count} 个搜索结果: ""{SearchText}"""
        ListView.Items.Clear()
        For Each tuple As Tuple(Of Integer, Integer) In SearchResult
            Dim tab As Tab = GlobalModel.Tabs.Item(tuple.Item1)
            ListView.Items.Add($"[{tab.Title}] - {tab.Tips.Item(tuple.Item2).Content}")
        Next
    End Sub

    Private Sub ListView_DoubleClick(sender As Object, e As EventArgs) Handles ListView.DoubleClick
        If ListView.SelectedIndex <> -1 Then
            If SelectCallback IsNot Nothing Then
                Dim result As Tuple(Of Integer, Integer) = SearchResult.ElementAt(ListView.SelectedIndex)
                SelectCallback.Invoke(result.Item1, result.Item2)
            End If
        End If
    End Sub

    Private Sub ButtonSearch_Click(sender As Object, e As EventArgs) Handles ButtonSearch.Click
        Close()
        If NewSearchCallback IsNot Nothing Then
            NewSearchCallback.Invoke()
        End If
    End Sub

    Private Sub ListView_MouseDown(sender As Object, e As MouseEventArgs) Handles ListView.MouseDown
        If ListView.IndexFromPoint(e.X, e.Y) = -1 Then
            ListView.ClearSelected()
        End If
    End Sub

    Private Sub ButtonClose_Click(sender As Object, e As EventArgs) Handles ButtonClose.Click
        Close()
    End Sub
End Class
