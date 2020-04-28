Imports System.Collections.ObjectModel
Imports DevComponents.DotNetBar

Public Class TipsTabView
    Inherits SuperTabStrip

    Public Sub New()
    End Sub

    Public Property DataSource As Object

    Private ReadOnly Property baseTabs As SubItemsCollection
        Get
            Return MyBase.Tabs
        End Get
    End Property

    Public Overloads ReadOnly Property Tabs As TipsTabItemsCollection
        Get
            Dim items As New TipsTabItemsCollection(Me)
            For Each t As SuperTabItem In MyBase.Tabs
                items.Add(CType(t, TipsTabViewItem))
            Next
            Return items
        End Get
    End Property

    Public Overloads Property SelectedTab As TipsTabViewItem
        Get
            Return CType(MyBase.SelectedTab, TipsTabViewItem)
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

    Public Overloads Sub Update()
        MyBase.Tabs.Clear()
        For Each t As Tab In DataSource
            MyBase.Tabs.Add(New TipsTabViewItem(t))
        Next
    End Sub

    Public Class TipsTabViewItem
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

    Public Class TipsTabItemsCollection
        Inherits Collection(Of TipsTabViewItem)

        Private _tabView As TipsTabView

        Public Sub New(tabView As TipsTabView)
            _tabView = tabView
        End Sub

        Public Overloads Function Add(tabItem As TipsTabViewItem) As TipsTabViewItem
            Me.InsertItem(Me.Items.Count, tabItem)
            Return tabItem
        End Function

        Protected Overrides Sub InsertItem(index As Integer, tabItem As TipsTabViewItem)
            MyBase.InsertItem(index, tabItem)
            _tabView.baseTabs.Insert(index, tabItem)
        End Sub

        Protected Overrides Sub RemoveItem(index As Integer)
            MyBase.RemoveItem(index)
            _tabView.baseTabs.RemoveAt(index)
        End Sub

        Protected Overrides Sub ClearItems()
            MyBase.ClearItems()
            _tabView.baseTabs.Clear()
        End Sub

        Protected Overrides Sub SetItem(index As Integer, tabItem As TipsTabViewItem)
            MyBase.SetItem(index, tabItem)
            _tabView.baseTabs(index) = tabItem
        End Sub

        Public Sub AddRange(tabItems As IEnumerable(Of TipsTabViewItem))
            For Each i As TipsTabViewItem In tabItems
                Me.InsertItem(Me.Items.Count, i)
            Next
        End Sub
    End Class
End Class
