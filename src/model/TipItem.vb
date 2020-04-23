﻿Imports Newtonsoft.Json

<JsonObject()>
Public Class TipItem

    <JsonProperty(PropertyName:="Content")>
    Public Property TipContent As String
    Public Property IsHighLight As Boolean = False
    <JsonIgnore()>
    Public Property HighLightColor As ColorPair

    Public Sub New(content As String, Optional highLight As Boolean = False, Optional color As ColorPair = Nothing)
        Me.TipContent = content
        Me.IsHighLight = highLight
        Me.HighLightColor = color
    End Sub

    Public Overrides Function ToString() As String
        Return TipContent
    End Function

    ''' <summary>
    ''' TipContent -> TipIndex
    ''' </summary>
    Public Shared Function GetIndexFromContent(ByVal Content As String, ByVal Tips As List(Of TipItem)) As Integer
        For Each Tip As TipItem In Tips
            If Tip.TipContent = Content Then
                Return Tips.IndexOf(Tip)
            End If
        Next
        Return -1
    End Function
End Class
