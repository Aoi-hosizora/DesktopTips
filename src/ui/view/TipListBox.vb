Public Class TipListBox
    Inherits ListBox

    Public Class ColorPair
        Public Property Fore As Boolean
        Public Property Color As Color

        Public Sub New(fore As Boolean, color As Color)
            Me.Fore = fore
            Me.Color = color
        End Sub
    End Class

    Public Property Colors As IEnumerable(Of ColorPair)
    Public Overloads Property DataSource As IEnumerable(Of TipItem)
    Public Overloads Property Items As IEnumerable(Of TipItem)

    Public Overloads ReadOnly Property SelectedItem As TipItem
        Get
            Return CType(MyBase.SelectedItem, TipItem)
        End Get
    End Property

    Public Overloads ReadOnly Property SelectedItems As IEnumerable(Of TipItem)
        Get
            Return MyBase.SelectedItems.Cast(Of TipItem)()
        End Get
    End Property

    Public Sub SetSelectedOnly(id As Integer)
        ClearSelected()
        SetSelected(id, True)
    End Sub

    Protected Overrides Sub OnDrawItem(e As DrawItemEventArgs)
        MyBase.OnDrawItem(e)
        If e.Index <> -1 Then
            e.DrawBackground()
            e.DrawFocusRectangle()
            Dim curr As TipItem = Items(e.Index)
            Dim color As ColorPair = curr.HighLightColor
            Dim brush As New SolidBrush(If(color Is Nothing Or color.Fore, e.ForeColor, color.Color))
            e.Graphics.DrawString(curr.ToString(), e.Font, brush, e.Bounds, StringFormat.GenericDefault)
        End If
    End Sub

End Class
