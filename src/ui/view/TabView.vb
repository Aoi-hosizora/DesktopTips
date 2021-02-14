Imports System.Threading

''' <summary>
''' Tab 列表
''' </summary>
Public Class TabView
    Inherits DevComponents.DotNetBar.SuperTabStrip
    Implements ICollectionView

    Public Sub New()
    End Sub

    ''' <summary>
    ''' 实现 ICollectionView 接口
    ''' </summary>
    Private ReadOnly Property BaseItems As IList Implements ICollectionView.BaseItems
        Get
            Return MyBase.Tabs
        End Get
    End Property

#Region "属性"

    ''' <summary>
    ''' 数据源
    ''' </summary>
    Public Property DataSource As Object

    ''' <summary>
    ''' 所有 Tabs，类型为 TabItemsCollection
    ''' </summary>
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

#Region "方法"

    ''' <summary>
    ''' 更新 DataSource 的显示
    ''' </summary>
    Public Overloads Sub Update()
        Tabs.Clear()
        For Each t In DataSource
            Tabs.Add(New TabViewItem(t))
        Next
        Refresh()
    End Sub

    ''' <summary>
    ''' 设置正在选中的 Tab
    ''' </summary>
    Public Sub SetSelected(tab As Tab)
        For Each item In Tabs
            If item.TabSource.Title = tab.Title Then
                SelectedTab = item
                Return
            End If
        Next
    End Sub

    Protected Overrides Sub OnSizeChanged(e As EventArgs)
        MyBase.OnSizeChanged(e)
    End Sub

    ''' <summary>
    ''' 单击列表重载，右键列表选中
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

    ''' <summary>
    ''' 当前悬浮的下标
    ''' </summary>
    Private _hoverIndex As Integer = -1

    ''' <summary>
    ''' 用于显示悬浮卡片的线程
    ''' </summary>
    Private _hoverThread As Thread

    Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
        MyBase.OnMouseMove(e)
        Dim sel As TabViewItem = GetItemFromPoint(e.Location)
        If sel Is Nothing Then
            _hoverIndex = -1
            If Not My.Computer.Keyboard.CtrlKeyDown Then
                HideTooltip() ' Ctrl 没按下时隐藏悬浮卡片
            End If
            Return
        End If
        If Tabs.IndexOf(sel) = _hoverIndex Then Return ' 不用更新
        _hoverIndex = Tabs.IndexOf(sel)

        If _hoverIndex > -1 And Not My.Computer.Keyboard.CtrlKeyDown Then
            ' <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
            HideTooltip() ' Ctrl 没按下时隐藏悬浮卡片
            If e.Button = MouseButtons.None Then
                _hoverThread = New Thread(New ParameterizedThreadStart(Sub (idx As Integer)
                    If _hoverIndex <> idx Then Return
                    Thread.Sleep(_hoverWaitingDuration)
                    Invoke(Sub() ShowTooltip(Tabs(idx).TabSource)) ' 显示悬浮卡片
                End Sub))
                _hoverThread.Start(_hoverIndex) ' 启动计时线程，准备显示悬浮卡片
            End If
        End If
    End Sub

    ''' <summary>
    ''' 鼠标移出，更新界面，记录 _hoverIndex 为 -1
    ''' </summary>
    Protected Overrides Sub OnMouseLeave(e As EventArgs)
        MyBase.OnMouseLeave(e)
        If _hoverIndex > -1 Then
            ' <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
            _hoverIndex = -1
            If Not My.Computer.Keyboard.CtrlKeyDown Then
                HideTooltip() ' Ctrl 没按下时隐藏悬浮卡片
            End If
        End If
    End Sub

    Private _hoverWaitingDuration As Integer = 350

    ''' <summary>
    ''' 显示悬浮卡片
    ''' </summary>
    Private Sub ShowTooltip(item As Tab)
        Dim curPos =  Cursor.Position
        HoverCardView.HoverCursorPosition = curPos
        HoverCardView.HoverParentPosition = Parent.PointToClient(curPos)
        HoverCardView.HoverParentSize = Parent.Size
        HoverCardView.HoverTabFunc = Function() item
        HoverCardView.Opacity = 0
        HoverCardView.Show()
    End Sub

    ''' <summary>
    ''' 隐藏悬浮卡片
    ''' </summary>
    Private Sub HideTooltip()
        HoverCardView.Close()
        If _hoverThread IsNot Nothing Then _hoverThread.Abort()
    End Sub

#End Region

#Region "内部类"

    ''' <summary>
    ''' TabItem 的显示，实现 DD.SuperTabItem
    ''' </summary>
    Public Class TabViewItem
        Inherits DevComponents.DotNetBar.SuperTabItem

        ''' <summary>
        ''' 每一个 Tab Item 的数据源
        ''' </summary>
        Public Property TabSource As Tab

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(tab As Tab)
            MyBase.New()
            TabSource = tab
        End Sub

        Public Overrides Property Text As String
            Get
                If TabSource Is Nothing Then
                    Return ""
                End If
                Return TabSource.Title
            End Get
            Set
                ' Nothing
            End Set
        End Property
    End Class

    ''' <summary>
    ''' TabViewItem 列表集合
    ''' </summary>
    Public Class TabItemsCollection
        Inherits BaseItemsCollection(Of TabViewItem)

        Public Sub New(tabView As ICollectionView, items As IEnumerable(Of TabViewItem))
            MyBase.New(tabView, items)
        End Sub
    End Class

#End Region
End Class
