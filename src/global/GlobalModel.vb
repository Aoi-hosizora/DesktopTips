Public Class GlobalModel
    ''' <summary>
    ''' 分组集合
    ''' </summary>
    Public Shared Tabs As New List(Of Tab)

    ''' <summary>
    ''' 颜色集合
    ''' </summary>
    Public Shared Colors As New List(Of TipColor)

    ''' <summary>
    ''' 当前分组编号
    ''' </summary>
    Private Shared _currentTabIndex As Integer = -1

    ''' <summary>
    ''' 当前分组
    ''' </summary>
    Public Shared Property CurrentTab As Tab
        Get
            If _currentTabIndex < 0 Then
                Return Nothing
            End If
            Return Tabs.ElementAt(_currentTabIndex)
        End Get
        Set
            _currentTabIndex = Tabs.IndexOf(value)
        End Set
    End Property
End Class
