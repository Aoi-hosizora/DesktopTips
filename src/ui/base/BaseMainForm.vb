Imports DD = DevComponents.DotNetBar

''' <summary>
''' 实现了 透明度动画 窗口拖动 不显示图标 功能
''' </summary>
Public Class BaseMainForm
    Inherits Form

    Private WithEvents _timerShowForm As New Timer() With {.Interval = 1, .Enabled = False}
    Private WithEvents _timerCloseForm As New Timer() With {.Interval = 1, .Enabled = False}
    Private WithEvents _timerMouseIn As New Timer() With {.Interval = 10, .Enabled = False}
    Private WithEvents _timerMouseOut As New Timer() With {.Interval = 10, .Enabled = False}
    Private WithEvents _labelFocus As New Label() With {.Visible = False}

    ''' <summary>
    ''' 窗口在未透明时的最大透明度
    ''' </summary>
    Protected Property MaxOpacity As Double = 0.6

    Protected Delegate Function MouseLeaveOption() As Boolean

    Protected Property CanMouseLeave As MouseLeaveOption

    Protected Property IsLoading As Boolean = True

    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)
        Me.Controls.Add(_labelFocus)
        Me.Opacity = 0
        ShowForm()

        Dim ctrls As New List(Of Control)
        ctrls.Add(Me)
        ctrls.AddRange(Me.Controls.Cast(Of Control)())
        InitHandler(ctrls)
        HideButtonXFocus(Me, e)
        IsLoading = False
    End Sub

    Private Sub InitHandler(ctrls As IEnumerable(Of Control))
        For Each ctrl As Control In ctrls
            Dim isBtn As Boolean = ctrl.GetType() = GetType(Button) OrElse ctrl.GetType() = GetType(DD.ButtonX)
            Dim isNum As Boolean = ctrl.GetType() = GetType(NumericUpDown)
            Dim isTab As Boolean = ctrl.GetType() = GetType(TabView.TabViewItem) OrElse ctrl.GetType() = GetType(DD.SuperTabItem) OrElse
                                   ctrl.GetType() = GetType(TabView) OrElse ctrl.GetType() = GetType(DD.SuperTabStrip)

            ' 任何控件都可以监听 鼠标移动
            AddHandler ctrl.MouseMove, AddressOf FormMouseMove
            AddHandler ctrl.MouseLeave, AddressOf FormMouseLeave

            ' 非 Tab 和 Num 才可以监听 鼠标点击 (Button 由于存在 ResizeFlag)
            If Not isTab AndAlso Not isNum Then
                AddHandler ctrl.MouseDown, AddressOf FormMouseDown
                AddHandler ctrl.MouseUp, AddressOf FormMouseUp
            End If

            ' 非 Button 和 Tab 和 Num 才可以监听拖动
            If Not isBtn AndAlso Not isTab AndAlso Not isNum Then
                AddHandler ctrl.MouseMove, AddressOf FormMouseDownMove
            End If

            ' Button 消除焦点框
            If isBtn Then
                AddHandler ctrl.MouseDown, AddressOf HideButtonXFocus
            End If

            ' 递归
            InitHandler(ctrl.Controls.Cast(Of Control)())
        Next
    End Sub

    Protected Overrides Sub OnFormClosing(e As FormClosingEventArgs)
        MyBase.OnFormClosing(e)
        e.Cancel = Opacity > 0
        CloseForm()
    End Sub

    Private Sub HideButtonXFocus(sender As Object, e As EventArgs)
        _labelFocus.Focus()
        _labelFocus.Select()
    End Sub

    Protected Overrides ReadOnly Property CreateParams As CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ExStyle = cp.ExStyle And Not NativeMethod.WS_EX_APPWINDOW ' 不显示在TaskBar
            cp.ExStyle = cp.ExStyle Or NativeMethod.WS_EX_TOOLWINDOW ' 不显示在Alt-Tab
            Return cp
        End Get
    End Property

#Region "Timer"

    Private Sub TimerShowForm_Tick(sender As Object, e As EventArgs) Handles _timerShowForm.Tick
        Me.Opacity += 0.08
        Me.Top += 1
        If Me.Opacity >= MaxOpacity Then
            Me.Opacity = MaxOpacity
            _timerShowForm.Enabled = False
        End If
    End Sub

    Private Sub TimerCloseForm_Tick(sender As Object, e As EventArgs) Handles _timerCloseForm.Tick
        Me.Opacity -= 0.08
        Me.Top -= 1
        If Me.Opacity <= 0 Then
            MyBase.Close()
            _timerCloseForm.Enabled = False
        End If
    End Sub

    Private Sub TimerMouseIn_Tick(sender As Object, e As EventArgs) Handles _timerMouseIn.Tick
        Me.Opacity += 0.05
        If Me.Opacity >= 1 Then
            Me.Opacity = 1
            _timerMouseIn.Enabled = False
        End If
    End Sub

    Private Sub TimerMouseOut_Tick(sender As Object, e As EventArgs) Handles _timerMouseOut.Tick
        Me.Opacity -= 0.02
        If Me.Opacity <= Me.MaxOpacity Then
            Me.Opacity = Me.MaxOpacity
            _timerMouseOut.Enabled = False
        End If
    End Sub

#End Region

#Region "Opecity"

    Private Sub FormMouseMove(sender As Object, e As EventArgs)
        If Cursor.Position.X >= Me.Left And Cursor.Position.X <= Me.Right And Cursor.Position.Y >= Me.Top And Cursor.Position.Y <= Me.Bottom Then
            FormOpacityUp()
        End If
    End Sub

    Private Sub FormMouseLeave(sender As Object, e As EventArgs)
        If CanMouseLeave.Invoke() Then
            FormOpacityDown()
        End If
    End Sub

    Private Sub ShowForm()
        _timerMouseOut.Enabled = False
        _timerCloseForm.Enabled = False
        _timerShowForm.Enabled = True
    End Sub

    Private Sub CloseForm()
        _timerMouseIn.Enabled = False
        _timerShowForm.Enabled = False
        _timerCloseForm.Enabled = True
    End Sub

    Public Sub FormOpacityUp()
        _timerCloseForm.Enabled = False
        _timerMouseOut.Enabled = False
        _timerMouseIn.Enabled = True
    End Sub

    Public Sub FormOpacityDown()
        _timerShowForm.Enabled = False
        _timerMouseIn.Enabled = False
        _timerMouseOut.Enabled = True
    End Sub

#End Region

#Region "Move"

    Protected ReadOnly Property IsMouseDown As Boolean = _isMouseDown
    Protected ReadOnly Property PushDownMouseInScreen As Point = _pushDownMouseInScreen
    Protected ReadOnly Property PushDownWindowSize As Size = _pushDownWindowSize
    Protected ReadOnly Property PushDownWindowPos As Point = _pushDownWindowPos

    Private Sub FormMouseDown(sender As Object, e As MouseEventArgs)
        _isMouseDown = e.Button = MouseButtons.Left
        _pushDownMouseInScreen = Cursor.Position
        _PushDownWindowSize = New Size(Me.Width, Me.Height)
        _pushDownWindowPos = New Point(Me.Left, Me.Top)
    End Sub

    Private Sub FormMouseUp(sender As Object, e As MouseEventArgs)
        _isMouseDown = False
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub FormMouseDownMove(sender As Object, e As MouseEventArgs)
        If IsMouseDown Then
            Me.Cursor = Cursors.SizeAll
            Me.Top = _pushDownWindowPos.Y + Cursor.Position.Y - _pushDownMouseInScreen.Y
            Me.Left = _pushDownWindowPos.X + Cursor.Position.X - _pushDownMouseInScreen.X
        End If
    End Sub

#End Region
End Class
