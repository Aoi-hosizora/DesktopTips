Imports Newtonsoft.Json

<Serializable, JsonObject>
Public Class Tab
    <JsonProperty("title")>
    Public Property Title As String

    <JsonProperty("created_at")>
    Public Property CreatedAt As DateTime

    <JsonProperty("updated_at")>
    Public Property UpdatedAt As DateTime

    <JsonProperty("tips")>
    Public Property Tips As List(Of TipItem)

    <JsonIgnore>
    Public ReadOnly Property IsDefaultCreatedAt As Boolean
        Get
            Return CreatedAt.Year = 1 ' 0001-01-01T00:00:00
        End Get
    End Property

    <JsonIgnore>
    Public ReadOnly Property IsDefaultUpdatedAt As Boolean
        Get
            Return UpdatedAt.Year = 1
        End Get
    End Property

    Public Sub New(title As String, Optional tips As List(Of TipItem) = Nothing)
        Me.Title = title
        Me.Tips = If(tips Is Nothing, New List(Of TipItem), tips)
    End Sub

    Public Overrides Function ToString() As String
        Return Title
    End Function
End Class
