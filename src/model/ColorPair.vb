Public Class ColorPair
    Public Property Color As Color
    Public Property Fore As Boolean

    Public Sub New()
        Me.New(Color.Black, True)
    End Sub

    Public Sub New(color As Color, Optional fore As Boolean = False)
        Me.Color = color
        Me.Fore = fore
    End Sub
End Class
