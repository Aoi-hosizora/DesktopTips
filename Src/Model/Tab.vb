<Serializable()>
Public Class Tab

    Public Property TabTitle As String
    Public Property TabClassName As String

    Public Sub New(ByVal Title As String, Optional ByVal ClassName As String = "TabItemDefault")
        Me.TabTitle = Title
        Me.TabClassName = ClassName
    End Sub

    Public Overrides Function ToString() As String
        Return Me.TabTitle
    End Function
End Class
