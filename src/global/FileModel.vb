Imports Newtonsoft.Json

<JsonObject>
Public Class FileModel
    <JsonProperty("colors")>
    Public Property Colors As List(Of TipColor)

    <JsonProperty("tabs")>
    Public Property Tabs As List(Of Tab)
End Class
