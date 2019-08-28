Imports Newtonsoft.Json

<JsonObject()>
Public Class Tab

    <JsonProperty(PropertyName:="Title")>
    Public Property Title As String
    Public Property Tips As List(Of TipItem)

    Public Sub New(ByVal Title As String, Optional ByVal Tips As List(Of TipItem) = Nothing)
        Me.Title = Title
        If Tips Is Nothing Then
            Tips = New List(Of TipItem)
        End If
        Me.Tips = Tips
    End Sub

    Public Overrides Function ToString() As String
        Return Title
    End Function

    ''' <summary>
    ''' 判断是否重复分组标题
    ''' </summary>
    Public Shared Function CheckDuplicateTab(ByVal NewTitle As String, ByRef Tabs As List(Of Tab)) As Boolean
        For Each Tab As Tab In Tabs
            If Tab.Title = NewTitle.Trim() Then
                Return True
            End If
        Next
        Return False
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
        Return GetTabFromTitle(Title, StorageUtil.Tabs)
    End Function
End Class
