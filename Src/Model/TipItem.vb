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

    ''' <summary>
    ''' 从 TipContent 获取 TipItem Index
    ''' </summary>
    Public Shared Function GetTipIndexFromContent(ByVal Content As String, ByVal Tips As List(Of TipItem)) As Integer
        For Each Tip As TipItem In Tips
            If Tip.TipContent = Content Then
                Return Tips.IndexOf(Tip)
            End If
        Next
        Return -1
    End Function
End Class
