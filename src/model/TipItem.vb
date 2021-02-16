Imports Newtonsoft.Json

<Serializable, JsonObject>
Public Class TipItem
    <JsonProperty("id")>
    Public Property Id As Integer

    <JsonProperty("content")>
    Public Property Content As String

    <JsonProperty("color")>
    Public Property ColorId As Integer

    <JsonProperty("done")>
    Public Property Done As Boolean

    ' <JsonProperty("images")>
    ' Public Property Images As List(Of String)

    <JsonProperty("created_at")>
    Public Property CreatedAt As DateTime

    <JsonProperty("updated_at")>
    Public Property UpdatedAt As DateTime

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

    <JsonIgnore>
    Public Property Color As TipColor
        Get
            Return GlobalModel.Colors.ElementAtOrDefault(ColorId)
        End Get
        Set
            ColorId = value.Id
        End Set
    End Property

    <JsonIgnore>
    Public ReadOnly Property IsHighLight As Boolean
        Get
            Return Color IsNot Nothing
        End Get
    End Property

    Public Sub New(id As Integer, content As String, Optional colorId As Integer = -1, Optional done As Boolean = False)
        Me.Id = id
        Me.Content = content
        Me.ColorId = colorId
        Me.Done = done
    End Sub

    Public Overrides Function ToString() As String
        Return Content
    End Function
End Class
