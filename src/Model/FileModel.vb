Imports Newtonsoft.Json

' Namespace Global.DesktopTips.Model
' End Namespace

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
        Dim now = DateTime.Now

        Dim colorList As New List(Of TipColor)
        colorList.AddRange({ _
            New TipColor(0, "红色", Color.Red) With { .CreatedAt = now, .UpdatedAt = now },
            New TipColor(1, "蓝色", Color.Blue) With { .CreatedAt = now, .UpdatedAt = now },
            New TipColor(2, "橙色", Color.Orange) With { .CreatedAt = now, .UpdatedAt = now }
        })

        Dim tab1 As New Tab("默认") With { .CreatedAt = now, .UpdatedAt = now }
        Dim tab2 As New Tab("备用") With { .CreatedAt = now, .UpdatedAt = now }
        Dim tabList As New List(Of Tab) From {tab1, tab2}
        tab1.Tips.AddRange({ _
            New TipItem("普通标签") With { .CreatedAt = now, .UpdatedAt = now },
            New TipItem("红色高亮标签", 0) With { .CreatedAt = now, .UpdatedAt = now },
            New TipItem("蓝色高亮标签", 1) With { .CreatedAt = now, .UpdatedAt = now },
            New TipItem("橙色高亮标签 已完成", 2, True) With { .CreatedAt = now, .UpdatedAt = now }
        })

        Return New FileModel(colorList, tabList)
    End Function
End Class
