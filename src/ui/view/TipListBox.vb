Public Class TipListBox
    Inherits ListBox

    Public Sub New()
        DrawMode = DrawMode.OwnerDrawFixed
        DisplayMember = "Content"
    End Sub

#Region "Properties and functions"

    ' https://docs.microsoft.com/en-us/dotnet/visual-basic/programming-guide/language-features/procedures/auto-implemented-properties

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

    Public Overloads Sub Update()
        Dim obj = CType(MyBase.DataSource, List(Of TipItem))
        MyBase.DataSource = Nothing
        MyBase.DataSource = obj
        MyBase.Update()
    End Sub
    
    Protected Overrides Sub OnDrawItem(e As DrawItemEventArgs)
        MyBase.OnDrawItem(e)
        e.DrawBackground()
        e.DrawFocusRectangle()
        If e.Index >= 0 AndAlso e.Index < ItemCount Then
            Dim item As TipItem = Items(e.Index)
            If item IsNot Nothing Then
                Dim color As Color = If(item.Color?.Color, e.ForeColor)
                Dim brush As New SolidBrush(color)
                e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
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
