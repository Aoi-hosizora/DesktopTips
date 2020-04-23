Public Class TipColor

    Public Property Id As Integer
    Public Property Name As String
    Public Property Color As Color

    Public Sub New()
        Me.New(Color.Black)
    End Sub

    Public Sub New(color As Color)
        Me.Color = color
    End Sub
End Class
