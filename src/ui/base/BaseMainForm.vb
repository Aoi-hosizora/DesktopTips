Imports DD = DevComponents.DotNetBar

''' <summary>
''' 实现 透明度动画 窗口拖动 不显示图标
''' </summary>
Public Class BaseMainForm
    Inherits Form

    Private WithEvents _timerShowForm As New Timer() With {.Interval = 1, .Enabled = False}
    Private WithEvents _timerCloseForm As New Timer() With {.Interval = 1, .Enabled = False}
    Private WithEvents _timerMouseIn As New Timer() With {.Interval = 10, .Enabled = False}
    Private WithEvents _timerMouseOut As New Timer() With {.Interval = 10, .Enabled = False}
    Private WithEvents _labelFocus As New Label() With {.Visible = False}

    ''' <summary>
    ''' 窗口动画，最大透明度
    ''' </summary>
    Protected Property MaxOpacity As Double = 0.6

    ''' <summary>
    ''' MouseLeave 回调，是否触发对应事件
    ''' </summary>
    Protected Property MouseLeaveCallback As Func(Of Boolean)

    Protected Overrides Sub OnLoad(e As EventArgs)
        Controls.Add(_labelFocus)
        Opacity = 0
        RefreshAppearance()

        MyBase.OnLoad(e)
        ShowForm()
        HideFocus()

        Dim ctrls As New List(Of Control)
        ctrls.Add(Me)
        ctrls.AddRange(Controls.Cast(Of Control)())
        AddHandlers(ctrls)
    End Sub

    Protected Overrides Sub OnSizeChanged(e As EventArgs)
        MyBase.OnSizeChanged(e)
        RefreshAppearance()
    End Sub

    ''' <summary>
    ''' 取消焦点框
    ''' </summary>
    Private Sub HideFocus()
        _labelFocus.Focus()
        _labelFocus.Select()
    End Sub

    ''' <summary>
    ''' 注册事件 Handlers 到给定控件
    ''' </summary>
    Private Sub AddHandlers(ctrls As IEnumerable(Of Control))
        For Each ctrl As Control In ctrls
            Dim isFrm As Boolean = ctrl.GetType() = GetType(MainForm) OrElse ctrl.GetType() = GetType(Form)
            Dim isBtn As Boolean = ctrl.GetType() = GetType(Button) OrElse ctrl.GetType() = GetType(DD.ButtonX)
            Dim isNum As Boolean = ctrl.GetType() = GetType(NumericUpDown)
            Dim isTab As Boolean = ctrl.GetType() = GetType(TabView) OrElse ctrl.GetType() = GetType(TabView.TabViewItem) OrElse
                ctrl.GetType() = GetType(DD.SuperTabStrip) OrElse ctrl.GetType() = GetType(DD.SuperTabItem)

            ' 窗口: 取消活动窗口
            If isFrm Then
                AddHandler CType(ctrl, Form).Deactivate, AddressOf FormDeactivate
            End If

            ' 按钮: 消除焦点框
            If isBtn Then
                AddHandler ctrl.MouseDown, Sub(sender As Object, e As EventArgs) HideFocus()
            End If

            ' 所有控件: 鼠标移动
            AddHandler ctrl.MouseMove, AddressOf FormMouseMove
            AddHandler ctrl.MouseLeave, AddressOf FormMouseLeave

            ' 非 Tab/Num: 鼠标点击
            If Not isTab AndAlso Not isNum Then
                AddHandler ctrl.MouseDown, AddressOf FormMouseDown
                AddHandler ctrl.MouseUp, AddressOf FormMouseUp
            End If

            ' 非 Btn/Tab/Num: 鼠标拖动
            If Not isBtn AndAlso Not isTab AndAlso Not isNum Then
                AddHandler ctrl.MouseMove, AddressOf FormMouseDrag
            End If

            ' 递归
            AddHandlers(ctrl.Controls.Cast(Of Control)())
        Next
    End Sub

    ''' <summary>
    ''' 窗口的 CreateParams 属性
    ''' </summary>
    Protected Overrides ReadOnly Property CreateParams As CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ExStyle = cp.ExStyle And Not NativeMethod.WS_EX_APPWINDOW ' 不显示在TaskBar
            cp.ExStyle = cp.ExStyle Or NativeMethod.WS_EX_TOOLWINDOW ' 不显示在Alt-Tab
            cp.ExStyle = cp.ExStyle Or NativeMethod.WS_EX_LAYERED
            Return cp
        End Get
    End Property

    ''' <summary>
    ''' 窗口的 WndProc 方法
    ''' </summary>
    Protected Overrides Sub WndProc(ByRef m As Message)
        Select Case m.Msg
            Case NativeMethod.WM_NCPAINT
                If NativeMethod.CheckAeroEnabled() Then
                    Dim val = NativeMethod.DWMNC_ENABLED
                    Const intSize = 4
                    NativeMethod.DwmSetWindowAttribute(Handle, NativeMethod.DWMWA_EXCLUDED_FROM_PEEK, val, intSize)
                End If
                Exit Select
        End Select
        MyBase.WndProc(m)
    End Sub

    ''' <summary>
    ''' 刷新窗口外观，包括背景色和全透明颜色
    ''' </summary>
    Private Sub RefreshAppearance()
        BackColor = Color.DarkRed
        TransparencyKey = Color.DarkRed
    End Sub

#Region "Timer"

    Private Sub TimerShowForm_Tick(sender As Object, e As EventArgs) Handles _timerShowForm.Tick
        Opacity += 0.08
        Top += 1
        If Opacity >= MaxOpacity Then
            Opacity = MaxOpacity
            _timerShowForm.Enabled = False
        End If
    End Sub

    Private Sub TimerCloseForm_Tick(sender As Object, e As EventArgs) Handles _timerCloseForm.Tick
        Opacity -= 0.08
        Top -= 1
        If Opacity <= 0 Then
            Close()
            _timerCloseForm.Enabled = False
        End If
    End Sub

    Private Sub TimerMouseIn_Tick(sender As Object, e As EventArgs) Handles _timerMouseIn.Tick
        Opacity += 0.05
        If Opacity >= 1 Then
            Opacity = 1
            _timerMouseIn.Enabled = False
        End If
    End Sub

    Private Sub TimerMouseOut_Tick(sender As Object, e As EventArgs) Handles _timerMouseOut.Tick
        Opacity -= 0.02
        If Opacity <= MaxOpacity Then
            Opacity = MaxOpacity
            _timerMouseOut.Enabled = False
        End If
    End Sub

#End Region

#Region "Opecity"

    Private Sub FormMouseMove(sender As Object, e As EventArgs)
        If Cursor.Position.X >= Left And Cursor.Position.X <= Right And Cursor.Position.Y >= Top And Cursor.Position.Y <= Bottom Then
            FormOpacityUp()
        End If
    End Sub

    Private Sub FormMouseLeave(sender As Object, e As EventArgs)
        If MouseLeaveCallback IsNot Nothing AndAlso MouseLeaveCallback.Invoke() Then
            FormOpacityDown()
        End If
    End Sub

    Protected Overrides Sub OnFormClosing(e As FormClosingEventArgs)
        MyBase.OnFormClosing(e)
        e.Cancel = Opacity > 0
        CloseForm()
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

    Private _isMouseDown As Boolean
    Protected ReadOnly Property PushDownMousePosition As Point = _pushDownMousePosition
    Protected ReadOnly Property PushDownWindowSize As Size = _pushDownWindowSize
    Protected ReadOnly Property PushDownWindowPosition As Point = _pushDownWindowPosition

    Private Sub FormMouseDown(sender As Object, e As MouseEventArgs)
        _isMouseDown = e.Button = MouseButtons.Left
        _pushDownMousePosition = Cursor.Position
        _pushDownWindowSize = New Size(Width, Height)
        _pushDownWindowPosition = New Point(Left, Top)
    End Sub

    Private Sub FormMouseUp(sender As Object, e As MouseEventArgs)
        _isMouseDown = False
        Cursor = Cursors.Default
    End Sub

    Private Sub FormDeactivate(sender As Object, e As EventArgs)
        _isMouseDown = False
        Cursor = Cursors.Default
    End Sub

    Private Sub FormMouseDrag(sender As Object, e As MouseEventArgs)
        If _isMouseDown Then
            Cursor = Cursors.SizeAll
            Top = _pushDownWindowPosition.Y + Cursor.Position.Y - _pushDownMousePosition.Y
            Left = _pushDownWindowPosition.X + Cursor.Position.X - _pushDownMousePosition.X
        End If
    End Sub

#End Region
End Class
