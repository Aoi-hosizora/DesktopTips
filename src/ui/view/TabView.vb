Imports DevComponents.DotNetBar

Public Class TabView
    Inherits SuperTabStrip
    Implements ICollectionView

    Public Sub New()
    End Sub

    Public Property DataSource As Object

    Private ReadOnly Property baseItems As IList Implements ICollectionView.baseItems
        Get
            Return MyBase.Tabs
        End Get
    End Property

#Region "重载属性"

    Public Overloads ReadOnly Property Tabs As TabItemsCollection
        Get
            Return New TabItemsCollection(Me, MyBase.Tabs.Cast(Of TabViewItem)())
        End Get
    End Property

    Public Overloads Property SelectedTab As TabViewItem
        Get
            Return CType(MyBase.SelectedTab, TabViewItem)
        End Get
        Set
            MyBase.SelectedTab = value
        End Set
    End Property

    Public Overloads ReadOnly Property SelectedTabValue As Tab
        Get
            Return SelectedTab.Source
        End Get
    End Property

#End Region

#Region "自定义函数"

    Public Overloads Sub Update()
        MyBase.Tabs.Clear()
        For Each t As Tab In DataSource
            MyBase.Tabs.Add(New TabViewItem(t))
        Next
    End Sub

#End Region

#Region "内部类"

    Public Class TabViewItem
        Inherits SuperTabItem

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(tab As Tab)
            MyBase.New()
            Source = tab
        End Sub

        Private _source As Tab

        Public Property Source As Tab
            Get
                Return _source
            End Get
            Set
                _source = value
                Text = value.Title
            End Set
        End Property
    End Class

    Public Class TabItemsCollection
        Inherits BaseItemsCollection(Of TabViewItem)

        Public Sub New(tabView As ICollectionView)
            MyBase.New(tabView)
        End Sub

        Public Sub New(tabView As ICollectionView, items As IEnumerable(Of TabViewItem))
            MyBase.New(tabView, items)
        End Sub
    End Class

#End Region
End Class
