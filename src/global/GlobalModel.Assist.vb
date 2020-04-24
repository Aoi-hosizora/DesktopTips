Imports System.IO
Imports Newtonsoft.Json

Partial Public Class GlobalModel

#Region "存储"

    Public Shared STORAGE_FILEPATH As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\DesktopTips"
    Public Shared STORAGE_FILENAME As String = STORAGE_FILEPATH & "\model.json"
    Public Shared STORAGE_BACKUP_FILENAME As String = STORAGE_FILEPATH & "\model.backup.%s.json"

    Private Shared Function load(name As String) As String
        Dim fs As New FileStream(name, FileMode.OpenOrCreate)
        Dim reader As New StreamReader(fs, System.Text.Encoding.UTF8)
        Dim json As String = reader.ReadToEnd()
        reader.Close()
        fs.Close()

        If String.IsNullOrWhiteSpace(json) Then
            Dim obj As New FileModel With {.Colors = New List(Of TipColor), .Tabs = New List(Of Tab)}
            obj.Colors.Add(New TipColor())
            obj.Tabs.Add(New Tab())
            json = JsonConvert.SerializeObject(obj, Formatting.Indented)
        End If
        Return json
    End Function

    Private Shared Sub save(json As String, name As String)
        Dim fs As New FileStream(name, FileMode.Create)
        Dim writer As New StreamWriter(fs, System.Text.Encoding.UTF8)
        writer.Write(json)
        writer.Close()
        fs.Close()
    End Sub

    Private Shared Sub save(tabs As List(Of Tab), colors As List(Of TipColor), name As String)
        Dim obj As New FileModel With {.Tabs = tabs, .Colors = colors}
        Dim json As String = JsonConvert.SerializeObject(obj, Formatting.Indented)
        save(json, name)
    End Sub

    Public Shared Sub LoadAllData()
        Dim obj As FileModel
        Dim json As String = load(STORAGE_FILENAME)
        Try
            obj = JsonConvert.DeserializeObject(Of FileModel)(json)
        Catch ex As Exception
            Throw New FileLoadException("读取文件失败，请检查文件。", STORAGE_FILENAME)
        End Try

        Tabs = obj.Tabs
        Colors = obj.Colors
        CurrentTab = obj.Tabs.FirstOrDefault()

        For Each tab As Tab In Tabs
            Dim dup As Tab = GlobalModel.CheckDuplicateTab(tab.Title, Tabs)
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
        Return -1
    End Function

    Public Shared Function GetIndexFromContent(content As String) As Integer
        Return GetIndexFromContent(content, GlobalModel.CurrentTab().Tips)
    End Function

    Public Shared Function GetTabFromTitle(Title As String, Tabs As List(Of Tab)) As Tab
        For Each Tab As Tab In Tabs
            If Tab.Title = Title Then
                Return Tab
            End If
        Next
        Return Nothing
    End Function

    Public Shared Function GetTabFromTitle(Title As String) As Tab
        Return GetTabFromTitle(Title, GlobalModel.Tabs)
    End Function

    Public Shared Function CheckDuplicateTab(newTitle As String, tabs As List(Of Tab)) As Tab
        For Each Tab As Tab In tabs
            If Tab.Title = newTitle.Trim() Then
                Return Tab
            End If
        Next
        Return Nothing
    End Function

    Public Shared Function CheckDuplicateTab(newTitle As String) As Tab
        Return CheckDuplicateTab(newTitle, Tabs)
    End Function

#End Region

End Class
