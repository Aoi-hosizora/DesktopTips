Imports Newtonsoft.Json

Public Class ColorConverte
    Inherits JsonConverter

    Public Overrides Sub WriteJson(writer As JsonWriter, value As Object, serializer As JsonSerializer)
        Dim c As Color = CType(value, Color)
        ' Dim hex = If(c.IsEmpty, "", "#" & (c.ToArgb() And &HFFFFFF).ToString("X6"))
        Dim hex As String = ColorTranslator.ToHtml(Color.FromArgb(c.ToArgb()))
        writer.WriteValue(hex)
    End Sub

    Public Overrides Function ReadJson(reader As JsonReader, objectType As Type, existingValue As Object, serializer As JsonSerializer) As Object
        Dim hex As String = reader.ReadAsString()
        If String.IsNullOrWhiteSpace(hex) Or Not hex.StartsWith("#") Then
            Return Color.Empty
        Else
            Return ColorTranslator.FromHtml(hex)
        End If
    End Function

    Public Overrides Function CanConvert(objectType As Type) As Boolean
        Return objectType = GetType(Color)
    End Function

End Class
