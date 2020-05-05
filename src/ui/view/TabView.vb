Imports System.Threading
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
            Dim sel As TabViewItem = GetItemFromPoint(e.Location)
            If sel IsNot Nothing Then
                SelectedTab = sel
            End If
        End If
    End Sub

#End Region

#Region "悬浮卡片"

    Private _hoverIndex As Integer = - 1
    Private _hoverThread As Thread

    Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
        MyBase.OnMouseMove(e)
        Dim sel As TabViewItem = GetItemFromPoint(e.Location)
        If sel Is Nothing Then
            _hoverIndex = - 1
            HideAndCloseTooltip()
            Return
        End If
        If Tabs.IndexOf(sel) = _hoverIndex Then Return

        _hoverIndex = Tabs.IndexOf(sel)
        If _hoverIndex > - 1 Then
            HideAndCloseTooltip()
            If e.Button = MouseButtons.None Then
                _hoverThread = New Thread(New ParameterizedThreadStart(Sub (idx As Integer)
                    If _hoverIndex <> idx Then Return
                    Thread.Sleep(_hoverWaitingDuration)
                    Me.Invoke(Sub() ShowTooltip(Tabs(idx).TabSource))
                End Sub))
                _hoverThread.Start(_hoverIndex)
            End If
        End If
    End Sub

    Protected Overrides Sub OnMouseLeave(e As EventArgs)
        MyBase.OnMouseLeave(e)
        _hoverIndex = - 1
        HideAndCloseTooltip()
    End Sub

    Private Const _hoverCardWidth As Integer = 200
    Private Const _hoverDistance As Integer = 7
    Private Const _hoverWaitingDuration As Integer = 350

    Private Sub HideAndCloseTooltip()
        HoverCardView.Close()
        If _hoverThread IsNot Nothing Then _hoverThread.Abort()
    End Sub

    Private Sub ShowTooltip(item As Tab)
        Me.Focus()
        Dim curPos = Cursor.Position
        Dim cliPos = Parent.PointToClient(curPos)
        Dim x = curPos.X - (cliPos.X + _hoverCardWidth + _hoverDistance)
        If x < 0 Then
            x = curPos.X + Parent.Width - cliPos.X + _hoverDistance
        End If
        Dim y = curPos.Y

        HoverCardView.WidthFunc = Function() _hoverCardWidth
        HoverCardView.HoverTabFunc = Function() item
        HoverCardView.Opacity = 0
        HoverCardView.PreLocation = New Point(x, y)
        HoverCardView.Show()
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

        ' Public Sub New(tabView As ICollectionView)
        '     MyBase.New(tabView)
        ' End Sub

        Public Sub New(tabView As ICollectionView, items As IEnumerable(Of TabViewItem))
            MyBase.New(tabView, items)
        End Sub
    End Class

#End Region
End Class
