Imports Newtonsoft.Json

<Serializable, JsonObject>
Public Class TipItem
    <JsonProperty("content")>
    Public Property Content As String

    <JsonProperty("color")>
    Public Property ColorId As Integer ' Ordered

    <JsonIgnore>
    Public ReadOnly Property Color As TipColor
        Get
            Return GlobalModel.Colors.ElementAtOrDefault(ColorId)
        End Get
    End Property

    <JsonIgnore>
    Public ReadOnly Property IsHighLight As Boolean
        Get
            Return Color IsNot Nothing AndAlso Color.Color <> Drawing.Color.Black
        End Get
    End Property

    Public Sub New(content As String, Optional colorIndex As Integer = 0)
        Me.Content = content
        Me.ColorId = colorIndex
    End Sub

    Public Overrides Function ToString() As String
        Return Content
    End Function
End Class
