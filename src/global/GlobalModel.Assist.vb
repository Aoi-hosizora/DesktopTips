Imports System.IO
Imports System.Text
Imports Newtonsoft.Json

Partial Public Class GlobalModel

#Region "本地文件"

    ' ~\.config\DesktopTips
    Private Shared ReadOnly StorageFilepath As String = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) & "\.config\DesktopTips"
    Public Shared ReadOnly StorageFilename As String = StorageFilepath & "\data.json"

    ''' <summary>
    ''' 加载文件并写入 GlobalModel
    ''' </summary>
    Public Shared Sub LoadAllData()
        Dim obj As FileModel
        Try
            Dim json = Load(StorageFilename)
            obj = JsonConvert.DeserializeObject(Of FileModel)(json)
        Catch ex As Exception
            Throw New FileLoadException(ex.Message)
        End Try

        Tabs = obj.Tabs
        Colors = obj.Colors
        CurrentTab = obj.Tabs.FirstOrDefault()
        For Each tab In Tabs
            If CheckDuplicateTab(tab.Title, Tabs, tab) Then
                Throw New FileLoadException("文件中存在重复分组，请检查文件。")
            End If
        Next
        ReorderColor(Colors, Tabs)
    End Sub

    ''' <summary>
    ''' 将 GlobalModel 内容写入文件
    ''' </summary>
    Public Shared Sub SaveAllData()
        Dim obj As New FileModel(Colors, Tabs)
        Dim json = JsonConvert.SerializeObject(obj, Formatting.Indented)
        Save(json, StorageFilename)
    End Sub

    ''' <summary>
    ''' 加载指定文件并读取内容
    ''' </summary>
    Private Shared Function Load(name As String) As String
        Dim fs As New FileStream(name, FileMode.OpenOrCreate)
        Dim reader As New StreamReader(fs, Encoding.UTF8)
        Dim json = reader.ReadToEnd().Replace("\n", "\r\n") ' Lf -> CrLf
        reader.Close()
        fs.Close()
        If String.IsNullOrWhiteSpace(json) Then
            json = JsonConvert.SerializeObject(FileModel.DefaultModel(), Formatting.Indented)
            Save(json, StorageFilename)
        End If
        Return json
    End Function

    ''' <summary>
    ''' 保存内容到指定文件
    ''' </summary>
    Private Shared Sub Save(json As String, name As String)
        Dim fs As New FileStream(name, FileMode.Create)
        Dim writer As New StreamWriter(fs, Encoding.UTF8)
        json = json.Replace("\r\n", "\n") ' CrLf -> Lf 
        writer.Write(json)
        writer.Close()
        fs.Close()
    End Sub

#End Region

#Region "检查分组和颜色"

    ''' <summary>
    ''' 检查是否存在重复分组名
    ''' </summary>
    Public Shared Function CheckDuplicateTab(newTitle As String, tabList As List(Of Tab), Optional currTab As Tab = Nothing) As Boolean
        Return tabList.Any(Function(tab)
            ' 存在一个分组的标题和 newTitle 相同，并且该分组不是 currTab
            Return tab.Title.Trim() = newTitle.Trim() AndAlso (currTab Is Nothing OrElse tab.Title.Trim() <> currTab.Title.Trim())
        End Function)
    End Function

    ''' <summary>
    ''' 处理颜色顺序和编号
    ''' </summary>
    Public Shared Sub ReorderColor(ByRef colorList As List(Of TipColor), tabList As List(Of Tab))
        colorList = colorList.OrderBy(Function(c) c.Id).ToList()
        For i = 0 To colorList.Count - 1
            Dim tipColor = colorList.ElementAt(i)
            Dim targetIdx = i ' 更新颜色 id 为 targetIdx
            If tipColor.Id <> targetIdx Then
                tabList.SelectMany(Function(t) t.Tips).Where(Function(t) t.ColorId = tipColor.Id).ToList().
                    ForEach(Sub(tip) tip.ColorId = targetIdx)
                tipColor.Id = targetIdx
            End If
        Next
    End Sub

#End Region
End Class
