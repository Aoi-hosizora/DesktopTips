Imports Newtonsoft.Json

<Serializable, JsonObject>
Public Class Tab
    <JsonProperty("title")>
    Public Property Title As String

    <JsonProperty("tips")>
    Public Property Tips As List(Of TipItem)

    ' <JsonProperty("created_at")>
    ' Public Property CreatedAt As DateTime
    
    ' <JsonProperty("updated_at")>
    ' Public Property UpdatedAt As DateTime

    Public Sub New(title As String, Optional tips As List(Of TipItem) = Nothing)
        Me.Title = title
        Me.Tips = If(tips Is Nothing, New List(Of TipItem), tips)
    End Sub

    Public Overrides Function ToString() As String
        Return Title
    End Function
End Class
