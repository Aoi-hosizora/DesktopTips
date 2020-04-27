Imports System.IO
Imports System.Text
Imports Newtonsoft.Json

Partial Public Class GlobalModel

#Region "存储"

    Private Shared STORAGE_FILEPATH As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\DesktopTips"
    Public Shared STORAGE_FILENAME As String = STORAGE_FILEPATH & "\model.json"
    Private Shared STORAGE_BACKUP_FILENAME As String = STORAGE_FILEPATH & "\model.backup.%s.json"

    Private Shared Function getDefaultModel() As FileModel
        Dim colorList As New List(Of TipColor) From {New TipColor(), New TipColor(1, "红色", Color.Red), New TipColor(2, "蓝色", Color.Blue)}
        Dim tabList As New List(Of Tab) From {New Tab()}
        tabList.First().Tips.AddRange({New TipItem("实例普通标签"), New TipItem("实例红色高亮标签", 1), New TipItem("实例蓝色高亮标签", 2)})
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
            Throw New FileLoadException("读取文件失败，请检查文件。", STORAGE_FILENAME)
        End Try

        Tabs = obj.Tabs
        Colors = obj.Colors
        CurrentTab = obj.Tabs.FirstOrDefault()

        For Each tab As Tab In Tabs
            Dim dup As Tab = CheckDuplicateTab(tab.Title, Tabs)
            If dup IsNot Nothing AndAlso Not dup.Equals(tab) Then
                Throw New FileLoadException("文件中存在重复分组，请检查文件。")
            End If
        Next
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

#Region "Get Check"

    Public Shared Function GetIndexFromContent(content As String, tips As List(Of TipItem)) As Integer
        For Each tip As TipItem In tips
            If tip.Content = content Then
                Return tips.IndexOf(tip)
            End If
        Next
        Return - 1
    End Function

    Public Shared Function GetIndexFromContent(content As String) As Integer
        Return GetIndexFromContent(content, CurrentTab().Tips)
    End Function

    Public Shared Function GetTabFromTitle(title As String, tabList As List(Of Tab)) As Tab
        For Each tab As Tab In tabList
            If tab.Title = title Then
                Return tab
            End If
        Next
        Return Nothing
    End Function

    Public Shared Function GetTabFromTitle(title As String) As Tab
        Return GetTabFromTitle(title, Tabs)
    End Function

    Public Shared Function CheckDuplicateTab(newTitle As String, tabList As List(Of Tab)) As Tab
        For Each tab As Tab In tabList
            If tab.Title = newTitle.Trim() Then
                Return tab
            End If
        Next
        Return Nothing
    End Function

    Public Shared Function CheckDuplicateTab(newTitle As String) As Tab
        Return CheckDuplicateTab(newTitle, Tabs)
    End Function

#End Region
End Class
