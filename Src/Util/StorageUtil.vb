Imports System.Runtime.Serialization.Formatters.Binary
Imports System.IO
Imports Newtonsoft.Json

Public Class StorageUtil

    ''' <summary>
    ''' 保存文件目录
    ''' </summary>
    Public Shared StorageFileDir As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\DesktopTips"
    Public Shared StorageJsonFile As String = StorageFileDir & "\data.json"

    ''' <summary>
    ''' 分组集
    ''' </summary>
    Public Shared Tabs As New List(Of Tab)

    ''' <summary>
    ''' 当前分组
    ''' </summary>
    Public Shared CurrTabIdx As Integer = -1

    ''' <summary>
    ''' 保存所有分组
    ''' </summary>
    Public Shared Sub SaveTabData()
        Dim Json As String = JsonConvert.SerializeObject(Tabs, Formatting.Indented)

        Dim fs As New FileStream(StorageJsonFile, FileMode.Create)
        Dim sw As New StreamWriter(fs, System.Text.Encoding.UTF8)
        sw.Write(Json)
        sw.Close()
    End Sub

    ''' <summary>
    ''' 加载所有分组
    ''' </summary>
    Public Shared Sub LoadTabTipsData()
        Tabs.Clear()
        Dim fs As New FileStream(StorageJsonFile, FileMode.OpenOrCreate)
        Dim sr As New StreamReader(fs, System.Text.Encoding.UTF8)
        Dim Json As String = sr.ReadToEnd()
        fs.Close()

        Json = If(Json = "", "[{""Title"": ""默认"", ""Tips"": []}]", Json)
        Tabs = JsonConvert.DeserializeObject(Of List(Of Tab))(Json)
        CurrTabIdx = If(CurrTabIdx = -1, 0, CurrTabIdx)
    End Sub

End Class
