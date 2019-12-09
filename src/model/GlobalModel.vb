﻿Imports System.Runtime.Serialization.Formatters.Binary
Imports System.IO
Imports Newtonsoft.Json

Public Class GlobalModel

    ''' <summary>
    ''' 保存文件目录
    ''' </summary>
    Public Shared StorageFileDir As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\DesktopTips"
    Public Shared StorageJsonFile As String = StorageFileDir & "\data.json"
    Public Shared StorageBackupJsonFile As String = StorageFileDir & "\data.backup.%s.json"

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
    ''' <param name="OtherList">不为 Nothing 则保存为 Backup</param>
    Public Shared Function SaveTabData(Optional ByRef OtherList As List(Of Tab) = Nothing) As String
        If OtherList Is Nothing Then
            ' 保存 GlobalModel 的数据
            Dim Json As String = JsonConvert.SerializeObject(Tabs, Formatting.Indented)

            Dim fs As New FileStream(StorageJsonFile, FileMode.Create)
            Dim sw As New StreamWriter(fs, System.Text.Encoding.UTF8)
            sw.Write(Json)
            sw.Close()
            Return ""
        Else
            ' 保存 OtherList 的数据
            Dim Json As String = JsonConvert.SerializeObject(OtherList, Formatting.Indented)

            Dim FileName = StorageBackupJsonFile.Replace("%s", Format(DateTime.Now(), "yyyyMMddHHmmssfff")).Clone()
            Dim fs As New FileStream(FileName, FileMode.Create)
            Dim sw As New StreamWriter(fs, System.Text.Encoding.UTF8)
            sw.Write(Json)
            sw.Close()
            Return FileName
        End If
    End Function

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

        Try
            Tabs = JsonConvert.DeserializeObject(Of List(Of Tab))(Json)
            If Tabs Is Nothing Then
                Throw New Exception
            End If
        Catch ex As Exception
            Throw New FileLoadException("读取文件失败，请检查文件。", StorageJsonFile)
        End Try

        For Each Tab As Tab In Tabs
            Dim dup As Tab = Tab.CheckDuplicateTab(Tab.Title, Tabs)
            If dup IsNot Nothing AndAlso Not dup.Equals(Tab) Then
                Throw New FileLoadException("文件中存在重复分组，请检查文件。")
            End If
        Next
        CurrTabIdx = If(CurrTabIdx = -1, 0, CurrTabIdx)
    End Sub

End Class