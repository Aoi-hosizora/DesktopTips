Imports Newtonsoft.Json

<Serializable, JsonObject>
Public Class TipItem
    <JsonProperty("content")>
    Public Property Content As String

    <JsonProperty("color")>
    Public Property ColorId As Integer ' Ordered

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

    Public Sub New(content As String, Optional colorId As Integer = - 1)
        Me.Content = content
        Me.ColorId = colorId
    End Sub

    Public Overrides Function ToString() As String
        Return Content
    End Function
End Class
