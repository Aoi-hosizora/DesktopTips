Public Class TipListBox
    Inherits ListBox

    Public Sub New()
        DrawMode = DrawMode.OwnerDrawFixed
        DisplayMember = "Content"
    End Sub

#Region "Properties and functions"

    ' https://docs.microsoft.com/en-us/dotnet/visual-basic/programming-guide/language-features/procedures/auto-implemented-properties

    Public Overloads ReadOnly Property Items As IEnumerable(Of TipItem)
        Get
            Return CType(DataSource, IEnumerable(Of TipItem))
        End Get
    End Property

    Public Overloads Property SelectedItem As TipItem
        Get
            Return CType(MyBase.SelectedItem, TipItem)
        End Get
        Set
            MyBase.SelectedItem = value
        End Set
    End Property

    Public Overloads ReadOnly Property SelectedItems As IEnumerable(Of TipItem)
        Get
            Return MyBase.SelectedItems.Cast(Of TipItem)()
        End Get
    End Property

    Public Overloads ReadOnly Property SelectedIndex As Integer
        Get
            Return Mybase.SelectedIndex
        End Get
    End Property

    Public ReadOnly Property SelectedCount As Integer
        Get
            Return SelectedIndices.Count
        End Get
    End Property

    Public ReadOnly Property ItemCount As Integer
        Get
            Return Items.Count
        End Get
    End Property

    Protected Overrides Sub OnDrawItem(e As DrawItemEventArgs)
        MyBase.OnDrawItem(e)
        e.DrawBackground()
        e.DrawFocusRectangle()
        If e.Index >= 0 AndAlso e.Index < ItemCount Then
            Dim item As TipItem = Items.ElementAtOrDefault(e.Index)
            If item IsNot Nothing Then
                e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
                Dim color As TipColor = item.Color
                Dim brush As New SolidBrush(If(color Is Nothing OrElse color.Color = e.ForeColor, e.ForeColor, color.Color))
                e.Graphics.DrawString(item.ToString(), e.Font, brush, e.Bounds, StringFormat.GenericDefault)
            End If
        End If
    End Sub

    Public Sub SetSelectOnly(index As Integer)
        SetSelected(0, True)
        ClearSelected()
        SetSelected(index, True)
    End Sub

#End Region
End Class
