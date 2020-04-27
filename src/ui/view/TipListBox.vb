Imports System.ComponentModel

Public Class TipListBox
    Inherits ListBox

    Public Sub New()
        _items = New TipListBoxItemCollection(Me)
        DrawMode = DrawMode.OwnerDrawFixed
    End Sub

    Private ReadOnly Property baseItems() As ObjectCollection
        Get
            Return MyBase.Items
        End Get
    End Property

#Region "Properties and functions"

    Private _items As TipListBoxItemCollection

    <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)>
    Public Overloads ReadOnly Property Items As TipListBoxItemCollection
        Get
            Return _items
        End Get
    End Property

    Public Overloads Property SelectedItem As TipItem
        Get
            Return CType(MyBase.SelectedItem, TipItem)
        End Get
        Set(value As TipItem)
            MyBase.SelectedItem = value
        End Set
    End Property

    Public Overloads ReadOnly Property SelectedItems As TipListBoxSelectedItemCollection
        Get
            Return MyBase.SelectedItems.Cast(Of TipItem)()
        End Get
    End Property

    Public Overloads ReadOnly Property SelectedCount As Integer
        Get
            Return SelectedIndices.Count
        End Get
    End Property

    Protected Overrides Sub OnDrawItem(e As DrawItemEventArgs)
        MyBase.OnDrawItem(e)
        e.DrawBackground()
        e.DrawFocusRectangle()
        If e.Index >= 0 AndAlso e.Index < Me.Items.Count Then
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
        Inherits System.Collections.ObjectModel.Collection(Of TipItem)

        Private _listBox As TipListBox

        Public Sub New(listBox As TipListBox)
            _listBox = listBox
        End Sub

        Public Function ToTipItems() As IEnumerable(Of TipItem)
            Return Cast(Of TipItem)()
        End Function

        Public Overloads Function Add(item As TipItem) As TipItem
            Me.InsertItem(Me.Items.Count, item)
            Return item
        End Function

        Protected Overrides Sub ClearItems()
            MyBase.ClearItems()
            _listBox.baseItems.Clear()
        End Sub

        Protected Overrides Sub InsertItem(index As Integer, item As TipItem)
            MyBase.InsertItem(index, item)
            _listBox.baseItems.Insert(index, item)
        End Sub

        Protected Overrides Sub RemoveItem(index As Integer)
            MyBase.RemoveItem(index)
            _listBox.baseItems.RemoveAt(index)
        End Sub

        Protected Overrides Sub SetItem(index As Integer, item As TipItem)
            MyBase.SetItem(index, item)
            _listBox.baseItems(index) = item
        End Sub

        Public Sub AddRange(items As IEnumerable(Of TipItem))
            For Each item As TipItem In items
                Me.InsertItem(Me.Items.Count, item)
            Next
        End Sub
    End Class

    Public Class TipListBoxSelectedItemCollection
        Inherits System.Collections.ObjectModel.Collection(Of TipItem)

        Public Sub New(listBox As TipListBox)
            MyBase.New(listBox)
        End Sub

        Public Function ToTipItems() As IEnumerable(Of TipItem)
            Return Cast(Of TipItem)()
        End Function
    End Class

#End Region

End Class
