Public Class GlobalModel

    ''' <summary>
    ''' 分组集
    ''' </summary>
    Public Shared Tabs As New List(Of Tab)

    ''' <summary>
    ''' 当前分组编号
    ''' </summary>
    Public Shared CurrTabIdx As Integer = -1

    ''' <summary>
    ''' 当前分组
    ''' </summary>
    Public Shared ReadOnly Property CurrentTab() As Tab
        Get
            Return Tabs.ElementAt(CurrTabIdx)
        End Get
    End Property

End Class
