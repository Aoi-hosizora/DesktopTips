Public Class TipListBox
    Inherits ListBox

    Public Sub New()
        DrawMode = DrawMode.OwnerDrawFixed
        DisplayMember = "Content"
        Controls.Add(_labelNothing)
    End Sub

#Region "属性 自定义函数"

    ' https://docs.microsoft.com/en-us/dotnet/visual-basic/programming-guide/language-features/procedures/auto-implemented-properties

    Public Overloads ReadOnly Property Items As IEnumerable(Of TipItem)
        Get
            Return MyBase.Items.Cast(Of TipItem)()
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

    Public ReadOnly Property SelectedCount As Integer
        Get
            Return SelectedIndices.Count
        End Get
    End Property

    Public ReadOnly Property ItemCount As Integer
        Get
            Return Items.Count
        End Get
    End Property

    Public Overloads Sub Update()
        MyBase.Update()
        Dim obj = CType(MyBase.DataSource, List(Of TipItem))
        MyBase.DataSource = Nothing
        MyBase.DataSource = obj
        adjustLabelNothing()
    End Sub

    Public Sub SetSelectOnly(index As Integer, Optional toTop As Boolean = False)
        If ItemCount = 0 Then Return
        If toTop Then
            SetSelected(0, True)
        End If
        ClearSelected()
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
        Dim color As Color = If(item.Color?.Color, e.ForeColor)

        e.DrawBackground()
        If e.Index = _mouseIndex Then ' Hover
            g.FillRectangle(New SolidBrush(HOVER_BACK_COLOR), b)
            If e.State And DrawItemState.Selected Then ' Selected + Hover
                g.FillRectangle(New SolidBrush(FOCUS_BACK_COLOR), b)
                g.DrawRectangle(New Pen(FOCUS_BORDER_COLOR), b)
            Else
                g.DrawRectangle(New Pen(HOVER_BACK_COLOR), b)
            End If
        Else if e.State And DrawItemState.Selected Then ' Selected
            g.FillRectangle(New SolidBrush(FOCUS_BACK_COLOR), b)
            g.DrawRectangle(New Pen(FOCUS_BACK_COLOR), b)
        Else
            g.FillRectangle(New SolidBrush(e.BackColor), b)
            g.DrawRectangle(New Pen(e.BackColor), b)
        End If
        g.DrawString(item.ToString(), e.Font, New SolidBrush(color), b, StringFormat.GenericDefault)
        ' e.DrawFocusRectangle()
    End Sub

    Private _mouseIndex As Integer = - 1

    Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
        MyBase.OnMouseMove(e)
        Dim index = IndexFromPoint(e.Location)
        If Not PointOutOfRange(e.Location) AndAlso index <> _mouseIndex Then
            If index > - 1 Then
                _hoverTooltip.Hide(Me)
                _hoverTooltip.SetToolTip(Me, Items(index).Content)
            End If
            If _mouseIndex > - 1 Then Invalidate(GetItemRectangle(_mouseIndex))
            _mouseIndex = Index
            If _mouseIndex > - 1 Then Invalidate(GetItemRectangle(_mouseIndex))
        End If
    End Sub

    Protected Overrides Sub OnMouseLeave(e As EventArgs)
        MyBase.OnMouseLeave(e)
        If _mouseIndex > - 1 Then
            If _mouseIndex > - 1 Then Invalidate(GetItemRectangle(_mouseIndex))
            _mouseIndex = - 1
            If _mouseIndex > - 1 Then Invalidate(GetItemRectangle(_mouseIndex))
        End If
    End Sub

#End Region

#Region "其他: 右键选中 文本提示 滚动条"

    Public Delegate Sub OnWheeled

    Public Property OnWheeledAction As OnWheeled

    Private ReadOnly _hoverTooltip As New ToolTip

    Private WithEvents _labelNothing As New Label With {
        .BackColor = Color.Snow, .ForeColor = Color.DimGray,  
        .Visible = False, .AutoSize = False,
        .Text= "无内容", .TextAlign = ContentAlignment.MiddleCenter
        }

    Private Sub adjustLabelNothing()
        _labelNothing.Visible = ItemCount = 0
        _labelNothing.Top = 0
        _labelNothing.Left = 0
        _labelNothing.Height = Height
        _labelNothing.Width = Width
    End Sub

    Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
        MyBase.OnMouseDown(e)
        If e.Button = MouseButtons.Right AndAlso SelectedCount <= 1 Then
            If PointOutOfRange(e.Location) Then
                ClearSelected()
            Else
                SetSelectOnly(IndexFromPoint(e.X, e.Y))
            End If
        End If
    End Sub

    Protected Overrides Sub OnMouseWheel(e As MouseEventArgs)
        MyBase.OnMouseWheel(e)
        If OnWheeledAction IsNot Nothing Then
            OnWheeledAction.Invoke()
        End If
    End Sub

    Protected Overrides Sub OnMouseCaptureChanged(e As EventArgs)
        MyBase.OnMouseCaptureChanged(e)
        If OnWheeledAction IsNot Nothing Then
            If Cursor.Position.X > Parent.Left + Left + Width - 20 Then
                OnWheeledAction.Invoke()
            End If
        End If
    End Sub

    Protected Overrides Sub OnSizeChanged(e As EventArgs)
        MyBase.OnSizeChanged(e)
        adjustLabelNothing()
    End Sub

#End Region
End Class
