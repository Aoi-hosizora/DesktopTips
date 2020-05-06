Imports System.Threading

Public Class TipListBox
    Inherits ListBox
    Implements ICollectionView

    Public Sub New()
        Me.DisplayMember = "Content"
        Me.DrawMode = DrawMode.OwnerDrawFixed
        Me.Controls.Add(_labelNothing)
    End Sub

    Private ReadOnly Property baseItems As IList Implements ICollectionView.baseItems
        Get
            Return MyBase.Items
        End Get
    End Property

#Region "重载属性"

    Public Overloads ReadOnly Property Items As TipItemsCollection
        Get
            Return New TipItemsCollection(Me, MyBase.Items.Cast(Of TipItem)())
        End Get
    End Property

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

    Public Overloads ReadOnly Property SelectedCount As Integer
        Get
            Return SelectedItems.Count
        End Get
    End Property

    Public Overloads ReadOnly Property ItemCount As Integer
        Get
            Return Items.Count
        End Get
    End Property

#End Region

#Region "自定义函数"

    Public Overloads Sub Update()
        Dim topIdx = TopIndex
        MyBase.Update()
        Dim obj = CType(MyBase.DataSource, List(Of TipItem))
        MyBase.DataSource = Nothing
        MyBase.DataSource = obj
        adjustLabelNothing()
        TopIndex = topIdx
    End Sub

    Public Sub SetSelectOnly(index As Integer)
        If ItemCount = 0 Then Return
        ClearSelected()
        If index >= ItemCount Then
            index = ItemCount - 1
        End If
        SetSelected(index, True)
    End Sub

    Public Function PointOutOfRange(p as Point) As Boolean
        If ItemCount = 0 Then Return True
        Dim rect As Rectangle = GetItemRectangle(ItemCount - 1)
        Return p.Y > rect.Top + rect.Height
    End Function

#End Region

#Region "重绘 悬浮"

    Private ReadOnly HOVER_BACK_COLOR As Color = Color.FromArgb(229, 243, 255)
    Private ReadOnly FOCUS_BACK_COLOR As Color = Color.FromArgb(205, 232, 255)
    Private ReadOnly FOCUS_BORDER_COLOR As Color = Color.FromArgb(153, 209, 255)

    Protected Overrides Sub OnDrawItem(e As DrawItemEventArgs)
        Dim g = e.Graphics
        g.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
        Dim b = New Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width - 1, e.Bounds.Height)

        If e.Index < 0 OrElse e.Index >= ItemCount Then Return
        Dim item As TipItem = Items(e.Index)
        Dim color As Color = If(item.Color?.Color, Color.Black)

        e.DrawBackground()
        If e.Index = _hoverIndex Then ' Hover
            g.FillRectangle(New SolidBrush(HOVER_BACK_COLOR), b)
            If e.State And DrawItemState.Selected Then ' Selected + Hover
                g.FillRectangle(New SolidBrush(FOCUS_BACK_COLOR), b)
                g.DrawRectangle(New Pen(FOCUS_BORDER_COLOR), b)
            Else
                g.DrawRectangle(New Pen(HOVER_BACK_COLOR), b)
            End If
        ElseIf e.State And DrawItemState.Selected Then ' Selected
            g.FillRectangle(New SolidBrush(FOCUS_BACK_COLOR), b)
            g.DrawRectangle(New Pen(FOCUS_BACK_COLOR), b)
        Else
            g.FillRectangle(New SolidBrush(e.BackColor), b)
            g.DrawRectangle(New Pen(e.BackColor), b)
        End If
        g.DrawString(item.ToString(), e.Font, New SolidBrush(color), b, StringFormat.GenericDefault)
        ' e.DrawFocusRectangle()
    End Sub

    Private _hoverIndex As Integer = - 1
    Private _hoverThread As Thread

    ''' <summary>
    ''' 鼠标移动，判断是否超出范围
    ''' 显示 ToolTip 和 记录 _hoverIndex，更新界面显示
    ''' </summary>
    Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
        MyBase.OnMouseMove(e)
        Dim index = IndexFromPoint(e.Location) ' 当前位置或最后一行
        If PointOutOfRange(e.Location) Then ' 鼠标超过最后一行
            If _hoverIndex > - 1 Then ' 还没记录
                _hoverIndex = - 1
                Invalidate(GetItemRectangle(ItemCount - 1)) ' 通知最后一行取消高亮
            End If
            If Not My.Computer.Keyboard.CtrlKeyDown Then
                HideAndCloseTooltip() ' Ctrl 按下不变
            End If
            Return
        End If
        If index = _hoverIndex Then Return ' 没必要更新

        If _hoverIndex > - 1 Then
            Invalidate(GetItemRectangle(_hoverIndex)) ' 更新前一瞬间的高亮
        End If
        _hoverIndex = index
        If _hoverIndex > - 1 Then ' 通知当前高亮行
            Invalidate(GetItemRectangle(_hoverIndex)) ' 更新当前的高亮
            If Not My.Computer.Keyboard.CtrlKeyDown Then
                HideAndCloseTooltip() ' Ctrl 按下不变
                If e.Button = MouseButtons.None Then
                    _hoverThread = New Thread(New ParameterizedThreadStart(Sub(idx As Integer) 
                        If _hoverIndex <> idx Then Return
                        Thread.Sleep(_hoverWaitingDuration)
                        Me.Invoke(Sub() ShowTooltip(Items(idx)))
                    End Sub))
                    _hoverThread.Start(_hoverIndex)
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' 鼠标移出列表，更新界面并且置 _hoverIndex 为 -1
    ''' </summary>
    Protected Overrides Sub OnMouseLeave(e As EventArgs)
        MyBase.OnMouseLeave(e)
        If _hoverIndex > - 1 Then
            Invalidate(GetItemRectangle(_hoverIndex))
            _hoverIndex = - 1
            If Not My.Computer.Keyboard.CtrlKeyDown Then
                HideAndCloseTooltip()
            End If
        End If
    End Sub

#End Region

#Region "其他布局: 文本提示 右键选中 滚动条"

    Private Const _hoverCardWidth As Integer = 200
    Private Const _hoverDistance As Integer = 7
    Private Const _hoverWaitingDuration As Integer = 350

    Private Sub HideAndCloseTooltip()
        HoverCardView.Close()
        If _hoverThread IsNot Nothing Then _hoverThread.Abort()
    End Sub

    Private Sub ShowTooltip(item As TipItem)
        Me.Focus()
        Dim curPos = Cursor.Position
        Dim cliPos = Parent.PointToClient(curPos)
        Dim x = curPos.X - (cliPos.X + _hoverCardWidth + _hoverDistance)
        If x < 0 Then
            x = curPos.X + Parent.Width - cliPos.X + _hoverDistance
        End If
        Dim y = curPos.Y

        HoverCardView.WidthFunc = Function() _hoverCardWidth
        HoverCardView.HoverTipFunc = Function() item
        HoverCardView.HoverTabFunc = Function() GlobalModel.CurrentTab
        HoverCardView.Opacity = 0
        HoverCardView.PreLocation = New Point(x, y)
        HoverCardView.Show()
    End Sub

    Private WithEvents _labelNothing As New Label With {
        .BackColor = Color.Snow, .ForeColor = Color.DimGray,  
        .Visible = False, .AutoSize = False,
        .Text= "无内容", .TextAlign = ContentAlignment.MiddleCenter
        }

    ''' <summary>
    ''' 调整 无内容标签 的显示
    ''' </summary>
    Private Sub adjustLabelNothing()
        _labelNothing.Visible = ItemCount = 0
        _labelNothing.Top = 0
        _labelNothing.Left = 0
        _labelNothing.Height = Height
        _labelNothing.Width = Width
    End Sub

    ''' <summary>
    ''' 调整大小时更新 无内容标签 的显示
    ''' </summary>
    Protected Overrides Sub OnSizeChanged(e As EventArgs)
        MyBase.OnSizeChanged(e)
        adjustLabelNothing()
    End Sub

    ''' <summary>
    ''' 单击列表，判断是否超过范围，或选中
    ''' </summary>
    Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
        MyBase.OnMouseDown(e)
        HideAndCloseTooltip()
        If PointOutOfRange(e.Location) Then
            ClearSelected()
        ElseIf e.Button = MouseButtons.Right AndAlso SelectedCount <= 1 Then
            SetSelectOnly(IndexFromPoint(e.X, e.Y))
        End If
    End Sub

    Public Property WheeledFunc As Action

    ''' <summary>
    ''' 鼠标滚动执行 WheeledFunc
    ''' </summary>
    Protected Overrides Sub OnMouseWheel(e As MouseEventArgs)
        MyBase.OnMouseWheel(e)
        If WheeledFunc IsNot Nothing Then
            WheeledFunc.Invoke()
        End If
    End Sub

    ''' <summary>
    ''' 滚动条拖动执行 WheeledFunc
    ''' </summary>
    Protected Overrides Sub OnMouseCaptureChanged(e As EventArgs)
        MyBase.OnMouseCaptureChanged(e)
        If WheeledFunc IsNot Nothing AndAlso Cursor.Position.X > Parent.Left + Left + Width - 20 Then
            WheeledFunc.Invoke()
        End If
    End Sub

#End Region

#Region "内部类"

    Public Class TipItemsCollection
        Inherits BaseItemsCollection(Of TipItem)

        ' Public Sub New(listBox As ICollectionView)
        '     MyBase.New(listBox)
        ' End Sub

        Public Sub New(listBox As ICollectionView, items As IEnumerable)
            MyBase.New(listBox, items)
        End Sub
    End Class

#End Region
End Class
