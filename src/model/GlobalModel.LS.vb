Imports System.Runtime.Serialization.Formatters.Binary
Imports System.IO
Imports Newtonsoft.Json

Partial Public Class GlobalModel

    ' Tabs          As New List(Of Tab)
    ' CurrTabIdx    As Integer

    ''' <summary>
    ''' 保存文件目录
    ''' </summary>
    Public Shared STORAGE_FILEPATH As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\DesktopTips"
    Public Shared STORAGE_FILENAME As String = STORAGE_FILEPATH & "\data.json"
    Public Shared STORAGE_BACKUP_FILENAME As String = STORAGE_FILEPATH & "\data.backup.%s.json"

    Private Shared Function load(name As String) As String
        Dim fs As New FileStream(name, FileMode.OpenOrCreate)
        Dim sr As New StreamReader(fs, System.Text.Encoding.UTF8)
        Dim json As String = sr.ReadToEnd()
        fs.Close()

        If json = "" Then
            json = "[{""Title"": ""默认"", ""Tips"": []}]"
        End If
        Return json
    End Function

    Private Shared Sub save(json As String, name As String)
        Dim fs As New FileStream(name, FileMode.Create)
        Dim sw As New StreamWriter(fs, System.Text.Encoding.UTF8)
        sw.Write(json)
        sw.Close()
    End Sub

    ''' <summary>
    ''' 加载所有分组
    ''' </summary>
    Public Shared Sub LoadAllData()
        Tabs.Clear()
        Dim json As String = load(STORAGE_FILENAME)
        Try
            Tabs = JsonConvert.DeserializeObject(Of List(Of Tab))(json)
            If Tabs Is Nothing Then
                Throw New Exception
            End If
        Catch ex As Exception
            Throw New FileLoadException("读取文件失败，请检查文件。", STORAGE_FILENAME)
        End Try

        For Each Tab As Tab In Tabs
            Dim dup As Tab = Tab.CheckDuplicateTab(Tab.Title, Tabs)
            If dup IsNot Nothing AndAlso Not dup.Equals(Tab) Then
                Throw New FileLoadException("文件中存在重复分组，请检查文件。")
            End If
        Next
        If CurrTabIdx = -1 Then
            CurrTabIdx = 0
        End If
    End Sub

    ''' <summary>
    ''' 保存所有分组
    ''' </summary>
    Public Shared Sub SaveAllData()
        Dim json As String = JsonConvert.SerializeObject(Tabs, Formatting.Indented)
        save(json, STORAGE_FILENAME)
    End Sub

    ''' <summary>
    ''' 保存Backup
    ''' </summary>
    Public Shared Function SaveBackupData(ByRef backup As List(Of Tab)) As String
        Dim json As String = JsonConvert.SerializeObject(backup, Formatting.Indented)
        Dim filename = STORAGE_BACKUP_FILENAME.Replace("%s", Format(DateTime.Now(), "yyyyMMddHHmmssfff")).Clone()
        save(json, filename)
        Return filename
    End Function

End Class
