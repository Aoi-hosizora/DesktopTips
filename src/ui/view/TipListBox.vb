Public Class TipListBox
    Inherits ListBox

    Public Sub New()
        DrawMode = DrawMode.OwnerDrawFixed
        DisplayMember = "Content"
    End Sub

#Region "Properties And Methods"

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

    Public Sub SetSelectOnly(index As Integer)
        SetSelected(0, True)
        ClearSelected()
        SetSelected(index, True)
    End Sub

#End Region

#Region "Draw"


    Private ReadOnly HOVER_BACK_COLOR As Color = Color.FromArgb(229, 243, 255)
    Private ReadOnly FOCUS_BACK_COLOR As Color = Color.FromArgb(205, 232, 255)
    Private ReadOnly FOCUS_BORDER_COLOR As Color = Color.FromArgb(153, 209, 255)

    Protected Overrides Sub OnDrawItem(e As DrawItemEventArgs)
        Dim g = e.Graphics
        g.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
        Dim b = New Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width - 1, e.Bounds.Height)

        If e.Index < 0 OrElse e.Index >= ItemCount Then Return
        Dim item As TipItem = Items(e.Index)
        Dim color As Color = If(item.Color?.Color, e.ForeColor)

        e.DrawBackground()
        If e.Index = _mouseIndex Then ' Hover
            g.FillRectangle(New SolidBrush(HOVER_BACK_COLOR), b)
            If e.State And DrawItemState.Selected Then ' Selected + Hover
                g.FillRectangle(New SolidBrush(FOCUS_BACK_COLOR), b)
                g.DrawRectangle(New Pen(FOCUS_BORDER_COLOR), b)
            Else
                g.DrawRectangle(New Pen(HOVER_BACK_COLOR), b)
            End If
        Else if e.State And DrawItemState.Selected Then ' Selected
            g.FillRectangle(New SolidBrush(FOCUS_BACK_COLOR), b)
            g.DrawRectangle(New Pen(FOCUS_BACK_COLOR), b)
        Else
            g.FillRectangle(New SolidBrush(e.BackColor), b)
            g.DrawRectangle(New Pen(e.BackColor), b)
        End If
        g.DrawString(item.ToString(), e.Font, New SolidBrush(color), b, StringFormat.GenericDefault)
        ' e.DrawFocusRectangle()
    End Sub

    Private _mouseIndex As Integer = - 1

    Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
        MyBase.OnMouseMove(e)
        Dim index = IndexFromPoint(e.Location)
        If index <> _mouseIndex Then
            If _mouseIndex > - 1 Then Invalidate(GetItemRectangle(_mouseIndex))
            _mouseIndex = Index
            If _mouseIndex > - 1 Then Invalidate(GetItemRectangle(_mouseIndex))
        End If
    End Sub

    Protected Overrides Sub OnMouseLeave(e As EventArgs)
        MyBase.OnMouseLeave(e)
        If _mouseIndex > - 1 Then
            If _mouseIndex > - 1 Then Invalidate(GetItemRectangle(_mouseIndex))
            _mouseIndex = - 1
            If _mouseIndex > - 1 Then Invalidate(GetItemRectangle(_mouseIndex))
        End If
    End Sub

#End Region
End Class
