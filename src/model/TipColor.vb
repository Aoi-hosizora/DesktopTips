Imports Newtonsoft.Json

<Serializable, JsonObject>
Public Class TipColor
    <JsonProperty("id")>
    Public Property Id As Integer

    <JsonProperty("name")>
    Public Property Name As String

    <JsonIgnore>
    Public Property Color As Color

    <JsonProperty("color")>
    Public Property HexColor As String
        Get
            Return ColorTranslator.ToHtml(Color.FromArgb(Color.ToArgb()))
        End Get
        Set
            Color = ColorTranslator.FromHtml(value)
        End Set
    End Property

    <JsonIgnore>
    Public Readonly Property RgbColor As String
        Get
            Return String.Format("{0}, {1}, {2}", Color.R, Color.G, Color.B)
        End Get
    End Property

    Public Sub New()
        Me.New(0, "默认高亮", Color.Red) ' Json 序列化需要默认构造函数
    End Sub

    Public Sub New(id As Integer, name As String)
        Me.New(id, name, Color.Red)
    End Sub

    Public Sub New(id As Integer, name As String, color As Color)
        Me.Id = id
        Me.Name = name
        Me.Color = color
    End Sub
End Class
