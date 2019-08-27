<Serializable()>
Public Class Tab

    Public Property TabTitle As String
    Public Property TabClassName As String

    Public Sub New(ByVal Title As String, ByVal ClassName As String)
        Me.TabTitle = Title
        Me.TabClassName = ClassName
    End Sub
End Class
