Imports Newtonsoft.Json

<Serializable, JsonObject>
Public Class FileModel
    <JsonProperty("colors")>
    Public Property Colors As List(Of TipColor)

    <JsonProperty("tabs")>
    Public Property Tabs As List(Of Tab)

    Public Sub New(colors As List(Of TipColor), tabs As List(Of Tab))
        Me.Colors = colors
        Me.Tabs = tabs
    End Sub

    ''' <summary>
    ''' 默认文件内容
    ''' </summary>
    Public Shared Function DefaultModel() As FileModel
        Dim colorList As New List(Of TipColor) From {New TipColor(0, "红色", Color.Red), New TipColor(1, "蓝色", Color.Blue)}
        Dim tabList As New List(Of Tab) From {New Tab("默认")}
        tabList.First().Tips.AddRange({ _
            New TipItem("普通标签"), New TipItem("红色高亮标签", 0), New TipItem("蓝色高亮标签", 1)
        })
        Return New FileModel(colorList, tabList)
    End Function
End Class
