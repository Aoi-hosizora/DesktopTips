<Serializable()>
Public Class Tab

    Public Property TabTitle As String
    Public Property TabClassName As String

    Public Sub New(ByVal Title As String, ByVal ClassName As String)
        Me.TabTitle = Title
        Me.TabClassName = ClassName
    End Sub

    Public Overrides Function ToString() As String
        Return Me.TabTitle
    End Function

    ''' <summary>
    ''' 从 Tab 获取 TabIndex
    ''' </summary>
    ''' <param name="TabTitle">Tab.TabTitle</param>
    ''' <param name="Tabs">List(Of Tab)</param>
    Public Shared Function GetTabIndexFromTabTitle(ByVal TabTitle As String, ByRef Tabs As List(Of Tab)) As Integer
        For Each t As Tab In Tabs
            If t.TabTitle = TabTitle Then
                Return Tabs.IndexOf(t)
            End If
        Next
        Return -1
    End Function

    ''' <summary>
    ''' 判断是否重复分组标题
    ''' </summary>
    ''' <param name="NewTitle">检索的新标题</param>
    ''' <param name="Tabs">List(Of Tab)</param>
    ''' <returns>重复 True</returns>
    Public Shared Function CheckDuplicateTab(ByVal NewTitle As String, ByRef Tabs As List(Of Tab)) As Boolean
        For Each Tab As Tab In Tabs
            If Tab.TabTitle = NewTitle.Trim() Then
                Return True
            End If
        Next
        Return False
    End Function


End Class
