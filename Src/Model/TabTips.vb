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
        Dim Idx = GetTabTipsIndexFromTabTitle(TabTitle, StorageUtil.StorageTipItems)
        If Idx <> -1 Then
            Return StorageUtil.StorageTipItems.Item(Idx)
        End If
        Return Nothing
    End Function

    ''' <summary>
    ''' 从 TabTitle 获取 TipItem()
    ''' </summary>
    ''' <param name="TabTitle">分组标题</param>
    ''' <param name="TabTips">List(Of TabTips)</param>
    ''' <returns>指定的分组内容集</returns>
    Public Shared Function GetTipsFromTabTitle(ByVal TabTitle As String, ByVal TabTips As List(Of TabTips)) As List(Of TipItem)
        For Each TabTip As TabTips In TabTips
            If TabTip.Tab.TabTitle = TabTitle Then
                Return TabTip.Tips
            End If
        Next
        Return Nothing
    End Function

    ''' <summary>
    ''' 从 TabTitle 获取 TipItem()
    ''' </summary>
    Public Shared Function GetTipsFromTabTitle(ByVal TabTitle As String) As List(Of TipItem)
        Return GetTipsFromTabTitle(TabTitle, StorageUtil.StorageTipItems)
    End Function
End Class
