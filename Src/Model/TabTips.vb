<Serializable()>
Public Class TabTips

    Public Property Tab As Tab
    Public Property Tips As List(Of TipItem)

    Public Sub New(ByRef Tab As Tab, ByRef Tips As List(Of TipItem))
        Me.Tab = Tab
        Me.Tips = Tips
    End Sub

    ''' <summary>
    ''' 从分组信息获取分组索引
    ''' </summary>
    ''' <param name="TabTitle">分组标题</param>
    ''' <param name="TabTips">List(Of TabTips)</param>
    ''' <returns>指定的分组索引</returns>
    Public Shared Function GetTabTipsIndexFromTabTitle(ByVal TabTitle As String, ByVal TabTips As List(Of TabTips)) As Integer
        For Each TabTip As TabTips In TabTips
            If TabTip.Tab.TabTitle = TabTitle Then
                Return TabTips.IndexOf(TabTip)
            End If
        Next
        Return -1
    End Function

    ''' <summary>
    ''' 从分组信息获取分组
    ''' </summary>
    ''' <param name="TabTitle">分组标题</param>
    ''' <returns>指定的分组</returns>
    Public Shared Function GetTabTipsFromTabTitle(ByVal TabTitle As String) As TabTips
        Return StorageUtil.StorageTipItems.Item(GetTabTipsIndexFromTabTitle(TabTitle, StorageUtil.StorageTipItems))
    End Function

    ''' <summary>
    ''' 从分组信息获取分组内容
    ''' </summary>
    ''' <param name="TabTitle">分组标题</param>
    ''' <param name="TabTips">List(Of TabTips)</param>
    ''' <returns>指定的分组内容集</returns>
    Public Shared Function GetTipsFromTab(ByVal TabTitle As String, ByVal TabTips As List(Of TabTips)) As List(Of TipItem)
        For Each TabTip As TabTips In TabTips
            If TabTip.Tab.TabTitle = TabTitle Then
                Return TabTip.Tips
            End If
        Next
        Return Nothing
    End Function
End Class
