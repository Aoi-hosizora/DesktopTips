Imports System.ComponentModel

Public Class TipListBox
    Inherits ListBox

    Public Sub New()
        DrawMode = DrawMode.OwnerDrawFixed
        DisplayMember = "Content"
    End Sub

    Private ReadOnly Property baseItems As ObjectCollection
        Get
            return MyBase.Items
        End Get
    End Property

#Region "Properties and functions"

    <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)>
    Public Overloads ReadOnly Property Items As New TipListBoxItemCollection(Me)

    Public ReadOnly Property ItemCount As Integer = Items.Count

    Public Overloads Property SelectedItem As TipItem
        Get
            Return CType(MyBase.SelectedItem, TipItem)
        End Get
        Set
            MyBase.SelectedItem = value
        End Set
    End Property

    Public Overloads ReadOnly Property SelectedItems As TipListBoxSelectedItemCollection
        Get
            Dim i As New TipListBoxSelectedItemCollection()
            For Each item As Object In MyBase.SelectedItems
                i.Add(CType(item, TipItem))
            Next
            Return i
        End Get
    End Property


    Public Overloads ReadOnly Property SelectedCount As Integer = SelectedIndices.Count

    Protected Overrides Sub OnDrawItem(e As DrawItemEventArgs)
        MyBase.OnDrawItem(e)
        e.DrawBackground()
        e.DrawFocusRectangle()
        If e.Index >= 0 AndAlso e.Index < Items.Count Then
            Dim item As TipItem = Items(e.Index)
            If item IsNot Nothing Then
                e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
                Dim color As TipColor = item.Color
                Dim brush As New SolidBrush(If(color Is Nothing, e.ForeColor, color.Color))
                e.Graphics.DrawString(item.ToString(), e.Font, brush, e.Bounds, StringFormat.GenericDefault)
            End If
        End If
    End Sub

    Public Sub SetSelectOnly(index As Integer)
        MyBase.SetSelected(0, True)
        MyBase.ClearSelected()
        MyBase.SetSelected(index, True)
    End Sub

#End Region

#Region "Collection Class"

    Public Class TipListBoxItemCollection
        Inherits ObjectModel.Collection(Of TipItem)

        Private ReadOnly _listBox As TipListBox

        Public Sub New(listBox As TipListBox)
            _listBox = listBox
        End Sub

        Public Function ToTipItems() As IEnumerable(Of TipItem)
            Return Cast(Of TipItem)()
        End Function

        Public Overloads Function Add(i As TipItem) As TipItem
            Me.InsertItem(Me.Items.Count, i)
            Return i
        End Function

        Protected Overrides Sub ClearItems()
            MyBase.ClearItems()
            _listBox.baseItems.Clear()
        End Sub

        Protected Overrides Sub InsertItem(index As Integer, i As TipItem)
            MyBase.InsertItem(index, i)
            _listBox.baseItems.Insert(index, i)
        End Sub

        Protected Overrides Sub RemoveItem(index As Integer)
            MyBase.RemoveItem(index)
            _listBox.baseItems.RemoveAt(index)
        End Sub

        Protected Overrides Sub SetItem(index As Integer, i As TipItem)
            MyBase.SetItem(index, i)
            _listBox.baseItems(index) = i
        End Sub

        Public Sub AddRange(i As IEnumerable(Of TipItem))
            For Each k As TipItem In i
                Me.InsertItem(Me.Items.Count, k)
            Next
        End Sub
    End Class

    Public Class TipListBoxSelectedItemCollection
        Inherits ObjectModel.Collection(Of TipItem)

        Public Sub New()
        End Sub

        Public Function ToTipItems() As IEnumerable(Of TipItem)
            Return Cast(Of TipItem)()
        End Function
    End Class

#End Region
End Class
