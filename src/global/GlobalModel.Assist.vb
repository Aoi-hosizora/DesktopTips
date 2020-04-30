Imports System.IO
Imports System.Text
Imports Newtonsoft.Json

Partial Public Class GlobalModel

#Region "加载与存储"

    Private Shared STORAGE_FILEPATH As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\DesktopTips"
    Public Shared STORAGE_FILENAME As String = STORAGE_FILEPATH & "\model.json"
    Private Shared STORAGE_BACKUP_FILENAME As String = STORAGE_FILEPATH & "\model.backup.%s.json"

    Private Shared Function getDefaultModel() As FileModel
        Dim colorList As New List(Of TipColor) From {New TipColor(0, "红色", Color.Red), New TipColor(1, "蓝色", Color.Blue)}
        Dim tabList As New List(Of Tab) From {New Tab()}
        tabList.First().Tips.AddRange({New TipItem("实例普通标签"), New TipItem("实例红色高亮标签", 0), New TipItem("实例蓝色高亮标签", 1)})
        Return New FileModel With {.Colors = colorList, .Tabs = tabList}
    End Function

    Private Shared Function Load(name As String) As String
        Dim fs As New FileStream(name, FileMode.OpenOrCreate)
        Dim reader As New StreamReader(fs, Encoding.UTF8)
        Dim json As String = reader.ReadToEnd()
        reader.Close()
        fs.Close()

        If String.IsNullOrWhiteSpace(json) Then
            Dim obj = getDefaultModel()
            json = JsonConvert.SerializeObject(obj, Formatting.Indented)
            save(json, STORAGE_FILENAME)
        End If
        Return json
    End Function

    Private Shared Sub save(json As String, name As String)
        Dim fs As New FileStream(name, FileMode.Create)
        Dim writer As New StreamWriter(fs, Encoding.UTF8)
        writer.Write(json)
        writer.Close()
        fs.Close()
    End Sub

    Private Shared Sub save(tabList As List(Of Tab), colorList As List(Of TipColor), name As String)
        Dim obj As New FileModel With {.Tabs = tabList, .Colors = colorList}
        Dim json As String = JsonConvert.SerializeObject(obj, Formatting.Indented)
        save(json, name)
    End Sub

    Public Shared Sub LoadAllData()
        Dim obj As FileModel
        Dim json As String = Load(STORAGE_FILENAME)
        Try
            obj = JsonConvert.DeserializeObject(Of FileModel)(json)
        Catch ex As Exception
            Throw New FileLoadException(ex.Message)
        End Try

        Tabs = obj.Tabs
        Colors = obj.Colors
        CurrentTab = obj.Tabs.FirstOrDefault()

        For Each tab As Tab In Tabs
            If CheckDuplicateTab(tab.Title, Tabs, tab) Then
                Throw New FileLoadException("文件中存在重复分组，请检查文件。")
            End If
        Next

        HandleWithColorOrder(Colors, Tabs)
    End Sub

    Public Shared Sub SaveAllData()
        save(Tabs, Colors, STORAGE_FILENAME)
    End Sub

    Public Shared Function SaveBackupData(backup As List(Of Tab)) As String
        Dim filename = String.Format(STORAGE_BACKUP_FILENAME, DateTime.Now().ToString("yyyyMMddHHmmssfff"))
        save(backup, Colors, filename)
        Return filename
    End Function

#End Region

#Region "检查冗余分组名以及颜色"

    Public Shared Function CheckDuplicateTab(newTitle As String, tabList As List(Of Tab), Optional currTab As Tab = Nothing) As Boolean
        Return tabList.Any(Function(tab)
            Return tab.Title = newTitle.Trim() AndAlso (currTab Is Nothing OrElse tab.Title <> currTab.Title)
        End Function)
    End Function

    Public Shared Sub HandleWithColorOrder(ByRef colorList As List(Of TipColor), tabList As List(Of Tab))
        colorList = colorList.OrderBy(Function(c) c.Id).ToList()
        For i = 0 To colorList.Count - 1
            Dim tipColor As TipColor = colorList.ElementAt(i)
            Dim targetIdx = i
            If tipColor.Id = targetIdx Then
                Continue For
            End If

            tabList.SelectMany(Function(t) t.Tips).Where(Function(t) t.ColorId = tipColor.Id).ToList().
                ForEach(Sub(tip) tip.ColorId = targetIdx)
            tipColor.Id = targetIdx
        Next
    End Sub

#End Region
End Class
