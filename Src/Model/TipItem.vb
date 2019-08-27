<Serializable()>
Public Class TipItem

    Public Property TipContent As String
    Public Property IsHighLight As Boolean = False
    Public Property TipTab As Tab

    Public Sub New(ByVal Content As String, ByVal TipTab As Tab)
        Me.TipContent = Content
        Me.TipTab = TipTab
    End Sub

    Public Overrides Function ToString() As String
        Return TipContent
    End Function
End Class
