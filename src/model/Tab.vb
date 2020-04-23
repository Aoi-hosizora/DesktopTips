Imports Newtonsoft.Json

<JsonObject()>
Public Class Tab

    <JsonProperty(PropertyName:="Title")>
    Public Property Title As String
    Public Property Tips As List(Of TipItem)

    Public Sub New(ByVal Title As String, Optional ByVal Tips As List(Of TipItem) = Nothing)
        Me.Title = Title
        If Tips Is Nothing Then
            Tips = New List(Of TipItem)
        End If
        Me.Tips = Tips
    End Sub

    Public Overrides Function ToString() As String
        Return Title
    End Function
End Class
