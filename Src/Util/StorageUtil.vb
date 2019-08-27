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
    Public Shared Function GetTipsFromTab(ByVal Tab As Tab) As List(Of TipItem)
        Return StorageTipItems.Item(StorageTabs.IndexOf(Tab))
    End Function

    ''' <summary>
    ''' 从当前分组获取分组内容
    ''' </summary>
    Public Shared Function GetTipsFromTab() As List(Of TipItem)
        Return StorageTipItems.Item(StorageTabs.IndexOf(CurrentTab))
    End Function

#End Region

#Region "Save"

    ''' <summary>
    ''' 保存指定分组
    ''' </summary>
    ''' <param name="Tab">分组信息</param>
    Public Shared Sub SaveTabData(ByVal Tab As Tab)
        If Tab Is Nothing Then Return

        Dim StorageTipsName As String = StorageFileDir & "\" & Tab.TabTitle & ".dat"

        SaveBinary(StorageTipItems.Item(StorageTabs.IndexOf(Tab)), StorageTipsName)
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
    Private Shared Function LoadTabTipsData(ByVal Tab As Tab) As List(Of TipItem)
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
            StorageTabs.Add(New Tab("默认", "SuperTabItemDefault"))
        End If
    End Sub

    ''' <summary>
    ''' 加载所有分组
    ''' </summary>
    Public Shared Sub LoadTabTipsData()
        LoadTabData()
        For Each Tab As Tab In StorageTabs
            StorageTipItems.Add(LoadTabTipsData(Tab))
        Next
        CurrentTab = StorageTabs.Item(0)
    End Sub

#End Region

End Class
