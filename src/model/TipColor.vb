Imports Newtonsoft.Json

<JsonObject>
Public Class TipColor
    <JsonProperty("id")>
    Public Property Id As Integer

    <JsonProperty("name")>
    Public Property Name As String

    <JsonProperty("color")>
    Public Property HexColor As String
        Get
            Return ColorTranslator.ToHtml(Color.FromArgb(_color.ToArgb()))
        End Get
        Set
            _color = ColorTranslator.FromHtml(value)
        End Set
    End Property

    <JsonIgnore>
    Public Property Color As Color = _color

    <JsonIgnore>
    Public Readonly Property RgbColor As String
        Get
            Return String.Format("{0}, {1}, {2}", Color.R, Color.G, Color.B)
        End Get
    End Property

    Public Sub New()
        Me.New(0, "默认高亮")
    End Sub

    Public Sub New(id As Integer, name As String)
        Me.New(id, name, Color.Red)
    End Sub

    Public Sub New(tipColor As TipColor)
        Me.New(tipColor.Id, tipColor.Name, tipColor.Color)
    End Sub

    Public Sub New(id As Integer, name As String, color As Color)
        Me.Id = id
        Me.Name = name
        Me.Color = color
    End Sub
End Class
