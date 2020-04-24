Imports System.ComponentModel

Public Class TipListBox
    Inherits ListBox

    Public Sub New()
        _items = New TipListBoxItemCollection(Me)
        DrawMode = DrawMode.OwnerDrawFixed
    End Sub

    Private _items As TipListBoxItemCollection
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)>
    Public Overloads ReadOnly Property Items As TipListBoxItemCollection
        Get
            Return _items
        End Get
    End Property

    Private ReadOnly Property baseItems As ObjectCollection
        Get
            Return MyBase.Items
        End Get
    End Property

    Public Overloads Property SelectedItem As TipItem
        Get
            Return DirectCast(MyBase.SelectedItem, TipItem)
        End Get
        Set(ByVal value As TipItem)
            MyBase.SelectedItem = value
        End Set
    End Property

    Public Overloads ReadOnly Property SelectedItems As TipListBoxSelectedItemCollection
        Get
            Dim items As New TipListBoxSelectedItemCollection()
            For Each item As Object In MyBase.SelectedItems
                items.Add(DirectCast(item, TipItem))
            Next
            Return items
        End Get
    End Property

    Protected Overrides Sub OnDrawItem(e As DrawItemEventArgs)
        MyBase.OnDrawItem(e)
        e.DrawBackground()
        e.DrawFocusRectangle()
        If e.Index >= 0 AndAlso e.Index < Me.Items.Count Then
            Dim item As TipItem = Items(e.Index)
            If item IsNot Nothing Then
                Dim color As TipColor = item.Color
                Dim brush As New SolidBrush(If(color Is Nothing, e.ForeColor, color.Color))
                e.Graphics.DrawString(item.ToString(), e.Font, brush, e.Bounds, StringFormat.GenericDefault)
            End If
        End If
    End Sub

    Public Class TipListBoxItemCollection
        Inherits ObjectModel.Collection(Of TipItem)

        Private _listBox As TipListBox

        Public Sub New(ByVal listBox As TipListBox)
            _listBox = listBox
        End Sub

        Public Overloads Function Add(ByVal item As TipItem) As TipItem
            Me.InsertItem(Me.Items.Count, item)
            Return item
        End Function

        Protected Overrides Sub InsertItem(ByVal index As Integer, ByVal item As TipItem)
            MyBase.InsertItem(index, item)
            _listBox.baseItems.Insert(index, item)
        End Sub

        Protected Overrides Sub RemoveItem(ByVal index As Integer)
            MyBase.RemoveItem(index)
            _listBox.baseItems.RemoveAt(index)
        End Sub

        Protected Overrides Sub SetItem(ByVal index As Integer, ByVal item As TipItem)
            MyBase.SetItem(index, item)
            _listBox.baseItems(index) = item
        End Sub

        Protected Overrides Sub ClearItems()
            MyBase.ClearItems()
            _listBox.baseItems.Clear()
        End Sub

        Public Sub AddRange(ByVal items As IEnumerable(Of TipItem))
            For Each item As TipItem In items
                Me.InsertItem(Me.Items.Count, item)
            Next
        End Sub
    End Class

    Public Class TipListBoxSelectedItemCollection
        Inherits ObjectModel.Collection(Of TipItem)
    End Class

End Class
