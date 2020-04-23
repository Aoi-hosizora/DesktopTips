Imports Newtonsoft.Json

<JsonObject()>
Public Class TipItem

    <JsonProperty(PropertyName:="Content")>
    Public Property TipContent As String
    Public Property IsHighLight As Boolean = False
    <JsonIgnore()>
    Public Property HighLightColor As TipColor

    Public Sub New(content As String, Optional highLight As Boolean = False, Optional color As TipColor = Nothing)
        Me.TipContent = content
        Me.IsHighLight = highLight
        Me.HighLightColor = color
    End Sub

    Public Overrides Function ToString() As String
        Return TipContent
    End Function
End Class
