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

    Public Overloads ReadOnly Property TabCount As Integer
        Get
            Return Tabs.Count
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

#End Region

#Region "自定义函数"

    Public Overloads Sub Update()
        Tabs.Clear()
        For Each t As Tab In DataSource
            Tabs.Add(New TabViewItem(t))
        Next
        Refresh()
    End Sub

    Public Sub SetSelected(tab As Tab)
        For Each item As TabViewItem In Tabs
            If item.TabSource.Title = tab.Title Then
                SelectedTab = item
                Return
            End If
        Next
    End Sub

    ''' <summary>
    ''' 右键 Tab 选中
    ''' </summary>
    Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
        MyBase.OnMouseDown(e)
        If e.Button = MouseButtons.Right Then
            Dim sel As TabViewITem = GetItemFromPoint(e.Location)
            If sel IsNot Nothing Then
                SelectedTab = sel
            End If
        End If
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
            TabSource = tab
        End Sub

        Public Property TabSource As Tab

        Public Overrides Property Text As String
            Get
                If TabSource Is Nothing Then
                    Return ""
                End If
                Return TabSource.Title
            End Get
            Set
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
