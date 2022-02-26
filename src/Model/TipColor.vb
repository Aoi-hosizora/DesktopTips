Imports Newtonsoft.Json

<Serializable, JsonObject>
Public Class TipColor
    <JsonProperty("id")>
    Public Property Id As Integer

    <JsonProperty("name")>
    Public Property Name As String

    <JsonIgnore>
    Public Property Color As Color

    <JsonProperty("style")>
    Public Property Style As FontStyle ' 0: Regular, 1: Bold, 2: Italic, 4: Underline

    <JsonProperty("color")>
    Public Property HexColor As String
        Get
            Return ColorTranslator.ToHtml(Color.FromArgb(Color.ToArgb()))
        End Get
        Set
            Color = ColorTranslator.FromHtml(value)
        End Set
    End Property

    <JsonProperty("created_at")>
    Public Property CreatedAt As DateTime

    <JsonProperty("updated_at")>
    Public Property UpdatedAt As DateTime

    <JsonIgnore>
    Public ReadOnly Property IsDefaultCreatedAt As Boolean
        Get
            Return CreatedAt.Year = 1 ' 0001-01-01T00:00:00
        End Get
    End Property

    <JsonIgnore>
    Public ReadOnly Property IsDefaultUpdatedAt As Boolean
        Get
            Return UpdatedAt.Year = 1
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
