Imports Newtonsoft.Json

<Serializable, JsonObject>
Public Class FileModel
    <JsonProperty("colors")>
    Public Property Colors As List(Of TipColor)

    <JsonProperty("tabs")>
    Public Property Tabs As List(Of Tab)
End Class
