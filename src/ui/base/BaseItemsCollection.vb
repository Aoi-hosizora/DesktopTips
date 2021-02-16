''' <summary>
''' 抽象容器类的 CollectionView，要求必须提供 BaseItems 只读属性，以便 CollectionView 能够快速重载 ItemsCollection
''' </summary>
Public Interface ICollectionView
    ReadOnly Property BaseItems As IList
End Interface

''' <summary>
''' 给 ICollectionView 提供 ItemsCollection 重载
''' </summary>
Public MustInherit Class BaseItemsCollection(Of T)
    Inherits ObjectModel.Collection(Of T)

    Private ReadOnly _owner As ICollectionView

    Protected Sub New(owner As ICollectionView)
        _owner = owner
    End Sub

    Protected Sub New(owner As ICollectionView, obj As IEnumerable(Of T))
        ' 直接添加到 ObjectModel 的 Items 中，不能够调用 AddRange 去添加到 BaseItems 中
        ' System.ArgumentException: データソース プロパティを設定したときに Items コレクションを変更することはできません。
        _owner = owner
        For Each o As T In obj
            Items.Add(o)
        Next
    End Sub

    Public Overloads Sub Add(obj As T)
        InsertItem(Items.Count, obj)
    End Sub

    Public Overloads Sub AddRange(obj As IEnumerable(Of T))
        For Each o As T In obj
            InsertItem(Items.Count, o)
        Next
    End Sub

    Protected Overrides Sub InsertItem(index As Integer, obj As T)
        MyBase.InsertItem(index, obj)
        _owner.BaseItems.Insert(index, obj)
    End Sub

    Protected Overrides Sub RemoveItem(index As Integer)
        MyBase.RemoveItem(index)
        _owner.BaseItems.RemoveAt(index)
    End Sub

    Protected Overrides Sub ClearItems()
        MyBase.ClearItems()
        _owner.BaseItems.Clear()
    End Sub

    Protected Overrides Sub SetItem(index As Integer, obj As T)
        MyBase.SetItem(index, obj)
        _owner.BaseItems(index) = obj
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
End Class
