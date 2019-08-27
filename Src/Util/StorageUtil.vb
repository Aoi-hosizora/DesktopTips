Imports System.Runtime.Serialization.Formatters.Binary
Imports System.IO

Public Class StorageUtil

    ''' <summary>
    ''' 保存文件目录
    ''' </summary>
    Public Shared StorageFileDir As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\DesktopTips"
    ''' <summary>
    ''' 分组保存文件路径
    ''' </summary>
    Private Shared StorageTabsInfo As String = StorageFileDir & "\Tabs.dip"

    ''' <summary>
    ''' 分组集
    ''' </summary>
    Public Shared StorageTabs As New List(Of Tab)
    ''' <summary>
    ''' 内容集
    ''' </summary>
    Public Shared StorageTipItems As New List(Of List(Of TipItem))
    ''' <summary>
    ''' 当前分组
    ''' </summary>
    Public Shared CurrentTab As Tab

#Region "Bin Save Load"

    ''' <summary>
    ''' 二进制数据保存
    ''' </summary>
    ''' <param name="Obj">保存对象</param>
    ''' <param name="FileName">保存文件名</param>
    Private Shared Sub SaveBinary(ByRef Obj As Object, ByVal FileName As String)
        Dim fs = New FileStream(FileName, FileMode.Create)
        Dim binaryFormatter = New BinaryFormatter()
        Dim ms = New MemoryStream()

        binaryFormatter.Serialize(ms, Obj)
        Dim buffer As Byte() = ms.GetBuffer()
        fs.Write(buffer, 0, buffer.Length)
        fs.Close()
    End Sub

    ''' <summary>
    ''' 二进制数据加载
    ''' </summary>
    ''' <param name="FileName">打开文件名</param>
    Private Shared Function LoadBinary(ByVal FileName As String) As Object
        Dim fs = New FileStream(FileName, FileMode.Open)
        Dim binaryFormatter = New BinaryFormatter()

        Dim Obj As Object = binaryFormatter.Deserialize(fs)
        fs.Close()
        Return Obj
    End Function

#End Region

#Region "List"

    ''' <summary>
    ''' 从分组信息获取分组内容
    ''' </summary>
    ''' <param name="Tab">分组信息</param>
    Public Shared Function GetTipsFromTab(ByRef Tab As Tab) As List(Of TipItem)
        ' TODO
        Return StorageTipItems.Item(GetTabIndexFromTab(Tab))
    End Function

    ''' <summary>
    ''' 从 Tab 获取 TabIndex (IndexOf 问题)
    ''' </summary>
    ''' <param name="Tab"></param>
    Public Shared Function GetTabIndexFromTab(ByRef Tab As Tab) As Integer
        For Each t As Tab In StorageTabs
            If t.TabTitle = Tab.TabTitle Then
                Return StorageTabs.IndexOf(t)
            End If
        Next
        Return 0
    End Function

    ''' <summary>
    ''' 判断是否重复分组标题
    ''' </summary>
    ''' <param name="NewTitle">检索的新标题</param>
    ''' <returns>重复 True</returns>
    Public Shared Function CheckDuplicateTab(ByVal NewTitle As String) As Boolean
        For Each Tab As Tab In StorageTabs
            If Tab.TabTitle = NewTitle.Trim() Then
                Return True
            End If
        Next
        Return False
    End Function

#End Region

#Region "Save"

    ''' <summary>
    ''' 保存指定分组
    ''' </summary>
    ''' <param name="Tab">分组信息</param>
    Public Shared Sub SaveTabData(ByRef Tab As Tab)
        If Tab Is Nothing Then Return

        Dim StorageTipsName As String = StorageFileDir & "\" & Tab.TabTitle & ".dat"

        ' TODO
        SaveBinary(StorageTipItems.Item(GetTabIndexFromTab(Tab)), StorageTipsName)
        SaveBinary(StorageTabs, StorageTabsInfo)
    End Sub

    ''' <summary>
    ''' 保存所有分组
    ''' </summary>
    Public Shared Sub SaveTabData()
        For Each Tab As Tab In StorageTabs
            SaveTabData(Tab)
        Next
    End Sub

#End Region

#Region "Load"

    ''' <summary>
    ''' 加载指定分组
    ''' </summary>
    ''' <param name="Tab">分组信息</param>
    ''' <returns>指定分组</returns>
    Private Shared Function LoadTabTipsData(ByRef Tab As Tab) As List(Of TipItem)
        Dim StorageTipsName As String = StorageFileDir & "\" & Tab.TabTitle & ".dat"
        Dim Tips As List(Of TipItem)
        If File.Exists(StorageTipsName) Then
            Tips = LoadBinary(StorageTipsName)
        Else
            Tips = New List(Of TipItem)
        End If
        Return Tips
    End Function

    ''' <summary>
    ''' 加载分组信息
    ''' </summary>
    Private Shared Sub LoadTabData()
        If File.Exists(StorageTabsInfo) Then
            StorageTabs = CType(LoadBinary(StorageTabsInfo), List(Of Tab))
        Else
            StorageTabs.Add(New Tab("默认"))
        End If
    End Sub

    ''' <summary>
    ''' 加载所有分组
    ''' </summary>
    Public Shared Sub LoadTabTipsData(Optional ByVal IsChangeCurr = True)
        LoadTabData()
        StorageTipItems.Clear()
        For Each Tab As Tab In StorageTabs
            StorageTipItems.Add(LoadTabTipsData(Tab))
        Next
        If IsChangeCurr Then
            CurrentTab = StorageTabs.Item(0)
        End If
    End Sub

#End Region

#Region "Delete"

    ''' <summary>
    ''' 删除分组并保存
    ''' </summary>
    ''' <param name="TabTitle">分组标题</param>
    Public Shared Sub DeleteTabData(ByVal TabTitle As String)
        For Each Tab As Tab In StorageTabs
            If Tab.TabTitle = TabTitle Then
                StorageTabs.Remove(Tab)
                Exit For
            End If
        Next

        For Each Tips As List(Of TipItem) In StorageTipItems
            If Tips.Count > 0 Then
                If Tips(0).TipTab.TabTitle = TabTitle Then
                    StorageTipItems.Remove(Tips)
                End If
            End If
        Next
        SaveTabData()
    End Sub

#End Region

End Class
