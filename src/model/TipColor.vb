Imports Newtonsoft.Json

<JsonObject()>
Public Class TipColor

    <JsonProperty("id")>
    Public Property Id As Integer

    <JsonProperty("name")>
    Public Property Name As String

    <JsonProperty("color")>
    Public Property Color As Color

    Public Sub New()
        Me.New(0, "默认", Color.Black)
    End Sub

    Public Sub New(id As Integer, name As String, color As Color)
        Me.Id = id
        Me.Name = name
        Me.Color = color
    End Sub

End Class
