Imports System.Threading

''' <summary>
''' TipItem 列表
''' </summary>
Public Class TipListBox
    Inherits ListBox
    Implements ICollectionView

    Public Sub New()
        DisplayMember = "Content"
        DrawMode = DrawMode.OwnerDrawFixed
        Controls.Add(_labelNothing)
    End Sub

    ''' <summary>
    ''' 实现 ICollectionView 接口
    ''' </summary>
    Private ReadOnly Property BaseItems As IList Implements ICollectionView.BaseItems
        Get
            Return MyBase.Items
        End Get
    End Property

#Region "属性"

    ''' <summary>
    ''' 所有 Items，类型为 TipItemsCollection
    ''' </summary>
    Public Overloads ReadOnly Property Items As TipItemsCollection
        Get
            Return New TipItemsCollection(Me, MyBase.Items.Cast(Of TipItem)())
        End Get
    End Property

    Public Overloads ReadOnly Property ItemCount As Integer
        Get
            Return Items.Count
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

    ''' <summary>
    ''' 滚动条滚动时回调
    ''' </summary>
    Public Property WheeledFunc As Action

#End Region

#Region "方法"

    ''' <summary>
    ''' 更新 DataSource 的显示
    ''' </summary>
    Public Overloads Sub Update()
        Dim topIdx = TopIndex
        MyBase.Update()
        Dim temp = CType(DataSource, List(Of TipItem))
        DataSource = Nothing
        DataSource = temp
        Refresh()
        AdjustLabelNothing()
        TopIndex = topIdx
    End Sub

    Protected Overrides Sub OnSizeChanged(e As EventArgs)
        MyBase.OnSizeChanged(e)
        AdjustLabelNothing()
    End Sub

    ''' <summary>
    ''' 清空选中，并设置正在选中的行
    ''' </summary>
    Public Sub SetSelectedItems(ParamArray indices As Integer())
        If ItemCount = 0 Then Return
        ClearSelected()
        For Each index In indices
            index = If(index < 0, 0, If(index >= ItemCount, ItemCount - 1, index))
            SetSelected(index, True)
        Next
    End Sub

    ''' <summary>
    ''' 判断给定 Point 是否超过列表内容范围
    ''' </summary>
    Public Function PointOutOfRange(p as Point) As Boolean
        If ItemCount = 0 Then Return True
        Dim rect As Rectangle = GetItemRectangle(ItemCount - 1)
        Return p.Y > rect.Top + rect.Height
    End Function

    ''' <summary>
    ''' 单击列表重载，超过范围清除选中，右键列表选中
    ''' </summary>
    Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
        MyBase.OnMouseDown(e)
        HideTooltip()
        If PointOutOfRange(e.Location) Then
            ClearSelected()
        ElseIf e.Button = MouseButtons.Right AndAlso SelectedCount <= 1 Then
            SetSelectedItems(IndexFromPoint(e.X, e.Y))
        End If
    End Sub

    ''' <summary>
    ''' 按下键盘重载，Esc 键清除选择
    ''' </summary>
    Protected Overrides Sub OnKeyDown(e As KeyEventArgs)
        MyBase.OnKeyDown(e)
        If e.KeyCode = Keys.Escape Then
            e.Handled = True
            ClearSelected()
        End If
    End Sub

    Private _vScrolling As Boolean = False
    Private _ncMouseLeave As Boolean = False

    ''' <summary>
    ''' 利用 WM_NCMOUSEMOVE 信息触发的 Non-Client Area MouseMove 事件
    ''' </summary>
    Public Event NcMouseMove As MouseEventHandler

    ''' <summary>
    ''' 利用 WM_NCMOUSELEAVE 信息触发的 Non-Client Area MouseMove 事件
    ''' </summary>
    Public Event NcMouseLeave As MouseEventHandler

    Protected Overrides Sub WndProc(ByRef m As Message)
        MyBase.WndProc(m)
        Select Case m.Msg
            Case NativeMethod.WM_NCMOUSEMOVE
                _ncMouseLeave = False
                RaiseEvent NcMouseMove(Me, Nothing)
            Case NativeMethod.WM_NCMOUSELEAVE
                _ncMouseLeave = True
                If Not _vScrolling Then
                    RaiseEvent NcMouseLeave(Me, Nothing)
                End If
            Case NativeMethod.WM_VSCROLL
                _vScrolling = NativeMethod.LOWORD(m.WParam) <> NativeMethod.SB_ENDSCROLL
                If Not _vScrolling AndAlso _ncMouseLeave Then
                    RaiseEvent NcMouseLeave(Me, Nothing)
                End If
        End Select
    End Sub

#End Region

#Region "重绘和显示悬浮卡片"

    ''' <summary>
    ''' 当前悬浮的下标
    ''' </summary>
    Private _hoverIndex As Integer = -1

    Private ReadOnly _hoverBackColor As Color = Color.FromArgb(229, 243, 255)
    Private ReadOnly _focusBackColor As Color = Color.FromArgb(205, 232, 255)
    Private ReadOnly _focusBorderColor As Color = Color.FromArgb(153, 209, 255)

    ''' <summary>
    ''' 重绘列表颜色和高亮
    ''' </summary>
    Protected Overrides Sub OnDrawItem(e As DrawItemEventArgs)
        If e.Index < 0 OrElse e.Index >= ItemCount Then Return
        Dim item = Items.ElementAt(e.Index)
        Dim itemColor = If(item.Color?.Color, Color.Black)
        Dim itemStyle = If(item.Color?.Style, FontStyle.Regular)

        Dim g = e.Graphics
        Dim b = New Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width - 1, e.Bounds.Height)
        g.SmoothingMode = Drawing2D.SmoothingMode.HighQuality

        e.DrawBackground()
        If e.Index = _hoverIndex Then ' Hover
            If e.State And DrawItemState.Selected Then ' Selected + Hover
                g.FillRectangle(New SolidBrush(_focusBackColor), b)
                g.DrawRectangle(New Pen(_focusBorderColor), b)
            Else ' Only Hover
                g.FillRectangle(New SolidBrush(_hoverBackColor), b)
                g.DrawRectangle(New Pen(_hoverBackColor), b)
            End If
        ElseIf e.State And DrawItemState.Selected Then ' Selected
            g.FillRectangle(New SolidBrush(_focusBackColor), b)
            g.DrawRectangle(New Pen(_focusBackColor), b)
        Else ' Normal
            g.FillRectangle(New SolidBrush(e.BackColor), b)
            g.DrawRectangle(New Pen(e.BackColor), b)
        End If

        Dim t = item.ToString().Replace(vbNewLine, "↴") ' ¬ ↴ ⇁ ¶
        If itemStyle = FontStyle.Regular AndAlso Not item.Done Then
            g.DrawString(t, e.Font, New SolidBrush(itemColor), b, StringFormat.GenericDefault)
        Else
            Dim style As FontStyle = itemStyle
            If item.Done Then
                style = style Or FontStyle.Strikeout
            End If
            g.DrawString(t, New Font(e.Font, style), New SolidBrush(itemColor), b, StringFormat.GenericDefault)
        End If
    End Sub

    ''' <summary>
    ''' 用于显示悬浮卡片的线程
    ''' </summary>
    Private _hoverThread As Thread

    ''' <summary>
    ''' 鼠标移动，超过列表范围的渲染，记录 _hoverIndex，显示 ToolTip，更新界面显示
    ''' </summary>
    Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
        MyBase.OnMouseMove(e)
        If PointOutOfRange(e.Location) Then ' 鼠标超过最后一行
            If _hoverIndex > -1 Then
                _hoverIndex = -1
                Invalidate(GetItemRectangle(ItemCount - 1)) ' 通知最后一行取消高亮
            End If
            If Not My.Computer.Keyboard.CtrlKeyDown Then
                HideTooltip() ' Ctrl 没按下时隐藏悬浮卡片
            End If
            Return
        End If

        Dim index = IndexFromPoint(e.Location)
        If index = _hoverIndex Then Return ' 不用更新
        If _hoverIndex > -1 Then
            Invalidate(GetItemRectangle(_hoverIndex)) ' 更新前一瞬间的高亮
        End If
        _hoverIndex = index
        If _hoverIndex > -1 Then
            Invalidate(GetItemRectangle(_hoverIndex)) ' 更新当前的高亮
            If Not My.Computer.Keyboard.CtrlKeyDown Then
                ' <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                HideTooltip() ' Ctrl 没按下时隐藏悬浮卡片
                If e.Button = MouseButtons.None Then
                    _hoverThread = New Thread(New ParameterizedThreadStart(Sub(idx As Integer)
                                                                               If _hoverIndex <> idx Then Return
                                                                               Thread.Sleep(_hoverWaitingDuration)
                                                                               Invoke(Sub() ShowTooltip(Items(idx))) ' 显示悬浮卡片
                                                                           End Sub))
                    _hoverThread.Start(_hoverIndex) ' 启动计时线程，准备显示悬浮卡片
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' 鼠标移出，更新界面，记录 _hoverIndex 为 -1
    ''' </summary>
    Protected Overrides Sub OnMouseLeave(e As EventArgs)
        MyBase.OnMouseLeave(e)
        If _hoverIndex > -1 Then
            Invalidate(GetItemRectangle(_hoverIndex)) ' 更新前一瞬间的高亮
            ' <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
            _hoverIndex = -1
            If Not My.Computer.Keyboard.CtrlKeyDown Then
                HideTooltip() ' Ctrl 没按下时隐藏悬浮卡片
            End If
        End If
    End Sub

#End Region

#Region "其他布局: 悬浮卡片 无内容标签 滚动条"

    Private ReadOnly _hoverWaitingDuration = 350 ' ms

    ''' <summary>
    ''' 显示悬浮卡片
    ''' </summary>
    Private Sub ShowTooltip(item As TipItem)
        Dim curPos = Cursor.Position
        HoverCardView.HoverCursorPosition = curPos
        HoverCardView.HoverParentPosition = Parent.PointToClient(curPos)
        HoverCardView.HoverParentSize = Parent.Size
        HoverCardView.HoverTipItem = item
        HoverCardView.HoverTab = GlobalModel.CurrentTab
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

    Private WithEvents _labelNothing As New Label With {
        .BackColor = Color.Snow, .ForeColor = Color.DimGray, .Visible = False,
        .Text= "无内容", .AutoSize = False, .TextAlign = ContentAlignment.MiddleCenter }

    ''' <summary>
    ''' 调整无内容标签的显示
    ''' </summary>
    Private Sub AdjustLabelNothing()
        _labelNothing.Visible = ItemCount = 0
        _labelNothing.Top = 0
        _labelNothing.Left = 0
        _labelNothing.Height = Height
        _labelNothing.Width = Width
    End Sub

    ''' <summary>
    ''' 鼠标滚动执行回调
    ''' </summary>
    Protected Overrides Sub OnMouseWheel(e As MouseEventArgs)
        MyBase.OnMouseWheel(e)
        If WheeledFunc IsNot Nothing Then
            WheeledFunc.Invoke()
        End If
    End Sub

    ''' <summary>
    ''' 滚动条拖动执行回调
    ''' </summary>
    Protected Overrides Sub OnMouseCaptureChanged(e As EventArgs)
        MyBase.OnMouseCaptureChanged(e)
        If WheeledFunc IsNot Nothing AndAlso Cursor.Position.X > Parent.Left + Left + Width - 20 Then
            WheeledFunc.Invoke()
        End If
    End Sub

#End Region

#Region "内部类"

    ''' <summary>
    ''' TipItem 列表集合
    ''' </summary>
    Public Class TipItemsCollection
        Inherits BaseItemsCollection(Of TipItem)

        Public Sub New(listBox As ICollectionView, items As IEnumerable)
            MyBase.New(listBox, items)
        End Sub
    End Class

#End Region
End Class
