﻿Imports Newtonsoft.Json

<JsonObject>
Public Class TipColor
    <JsonProperty("id")>
    Public Property Id As Integer

    <JsonProperty("name")>
    Public Property Name As String

    <JsonProperty("color")>
    Private Property HexColor As String
        Get
            Return ColorTranslator.ToHtml(Color.FromArgb(_color.ToArgb()))
        End Get
        Set
            _color = ColorTranslator.FromHtml(value)
        End Set
    End Property

    <JsonIgnore>
    Public Property Color As Color = _color

    Public Sub New()
        Me.New(0, "默认", Color.Black)
    End Sub

    Public Sub New(id As Integer, name As String, color As Color)
        Me.Id = id
        Me.Name = name
        Me.Color = color
    End Sub
End Class