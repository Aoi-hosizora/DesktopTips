Partial Public Class GlobalModel

    ''' <summary>
    ''' TipContent -> TipIndex
    ''' </summary>
    Public Shared Function GetIndexFromContent(ByVal Content As String, ByVal Tips As List(Of TipItem)) As Integer
        For Each Tip As TipItem In Tips
            If Tip.TipContent = Content Then
                Return Tips.IndexOf(Tip)
            End If
        Next
        Return -1
    End Function

    ''' <summary>
    ''' 判断是否重复分组标题
    ''' </summary>
    Public Shared Function CheckDuplicateTab(ByVal NewTitle As String, ByRef Tabs As List(Of Tab)) As Tab
        For Each Tab As Tab In Tabs
            If Tab.Title = NewTitle.Trim() Then
                Return Tab
            End If
        Next
        Return Nothing
    End Function

    ''' <summary>
    ''' TabTitle -> Tab
    ''' </summary>
    Public Shared Function GetTabFromTitle(ByVal Title As String, ByRef Tabs As List(Of Tab)) As Tab
        For Each Tab As Tab In Tabs
            If Tab.Title = Title Then
                Return Tab
            End If
        Next
        Return Nothing
    End Function

    ''' <summary>
    ''' TabTitle -> Tab
    ''' </summary>
    Public Shared Function GetTabFromTitle(ByVal Title As String) As Tab
        Return GetTabFromTitle(Title, GlobalModel.Tabs)
    End Function
End Class
