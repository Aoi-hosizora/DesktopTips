Imports Newtonsoft.Json

<JsonObject()>
Public Class Tab

    <JsonProperty("title")>
    Public Property Title As String

    <JsonProperty("tips")>
    Public Property Tips As List(Of TipItem)

    Public Sub New()
        Me.New("默认", New List(Of TipItem))
    End Sub

    Public Sub New(title As String, tabs As List(Of TipItem))
        Me.Title = title
        Me.Tips = tabs
    End Sub

    Public Overrides Function ToString() As String
        Return Me.Title
    End Function

End Class
