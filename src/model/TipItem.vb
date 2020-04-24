Imports Newtonsoft.Json

<JsonObject()>
Public Class TipItem

    <JsonProperty("content")>
    Public Property Content As String

    <JsonProperty("color")>
    Public Property ColorIndex As Integer

    <JsonIgnore()>
    Public ReadOnly Property Color As TipColor
        Get
            If ColorIndex < 0 Or ColorIndex >= GlobalModel.Colors.Count Then
                Return Nothing
            Else
                Return GlobalModel.Colors.ElementAt(ColorIndex)
            End If
        End Get
    End Property

    <JsonIgnore()>
    Public ReadOnly Property IsHighLight As Boolean
        Get
            Return Color IsNot Nothing AndAlso Color.Color <> Drawing.Color.Black
        End Get
    End Property

    Public Sub New(content As String, Optional colorIndex As Integer = -1)
        Me.Content = content
        Me.ColorIndex = colorIndex
    End Sub

    Public Overrides Function ToString() As String
        Return Content
    End Function

End Class
