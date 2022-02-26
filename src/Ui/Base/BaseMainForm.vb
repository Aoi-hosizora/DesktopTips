Imports Microsoft.Win32
Imports DD = DevComponents.DotNetBar

''' <summary>
''' 实现 透明度动画 窗口拖动 隐藏图标 隐藏Peek
''' </summary>
Public Class BaseMainForm
    Inherits Form

    Private WithEvents _timerShowForm As New Timer With {.Interval = 1, .Enabled = False}
    Private WithEvents _timerCloseForm As New Timer With {.Interval = 1, .Enabled = False}
    Private WithEvents _timerMouseIn As New Timer With {.Interval = 10, .Enabled = False}
    Private WithEvents _timerMouseOut As New Timer With {.Interval = 10, .Enabled = False}
    Private WithEvents _labelFocus As New Label With {.Visible = False}

    ''' <summary>
    ''' 窗口动画，最大透明度
    ''' </summary>
    Protected Property MaxOpacity As Double = 0.6

    ''' <summary>
    ''' MouseLeave 回调，是否触发对应事件
    ''' </summary>
    Protected Property MouseLeaveCallback As Func(Of Boolean)

    ''' <summary>
    ''' 是否在屏幕大小变更时保持（黏贴）到屏幕右边的距离
    ''' </summary>
    Protected Property StickToScreenRightBound As Boolean = True

    Protected Overrides Sub OnLoad(e As EventArgs)
        If Not DesignMode Then
            Controls.Add(_labelFocus)
            Opacity = 0
        End If
        RefreshAppearance()

        MyBase.OnLoad(e)
        If Not DesignMode Then
            ShowForm()
            RemoveFocus()
        End If

        If Not DesignMode Then
            Dim ctrls As New List(Of Control)
            ctrls.Add(Me)
            ctrls.AddRange(Controls.Cast(Of Control)())
            AddHandlers(ctrls)
        End If

        If Not DesignMode Then
            If StickToScreenRightBound Then
                AddHandler SystemEvents.DisplaySettingsChanged, AddressOf DisplaySettingsChanged
                DisplaySettingsChanged(Me, New EventArgs())
            End If
        End If
    End Sub

    Protected Overrides Sub OnSizeChanged(e As EventArgs)
        MyBase.OnSizeChanged(e)
        If Not DesignMode Then
            RefreshAppearance()
        End If
    End Sub

    ''' <summary>
    ''' 刷新窗口外观，包括背景色和全透明颜色
    ''' </summary>
    Private Sub RefreshAppearance()
        BackColor = Color.DarkRed
        TransparencyKey = Color.DarkRed
    End Sub

    ''' <summary>
    ''' 隐藏焦点框
    ''' </summary>
    Private Sub RemoveFocus()
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
                AddHandler ctrl.MouseDown, Sub(sender As Object, e As EventArgs) RemoveFocus()
            End If

            ' 所有控件: 鼠标移动
            AddHandler ctrl.MouseMove, AddressOf FormMouseMove
            AddHandler ctrl.MouseLeave, AddressOf FormMouseLeave
            If ctrl.GetType() = GetType(TipListBox) Then
                Dim tl = CType(ctrl, TipListBox)
                AddHandler tl.NcMouseMove, AddressOf FormMouseMove
                AddHandler tl.NcMouseLeave, AddressOf FormMouseLeave
            End If

            ' 非Tab/Num: 鼠标点击
            If Not isTab AndAlso Not isNum Then
                AddHandler ctrl.MouseDown, AddressOf FormMouseDown
                AddHandler ctrl.MouseUp, AddressOf FormMouseUp
            End If

            ' 非Btn/Tab/Num: 鼠标拖动
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
            If Not DesignMode Then
                cp.ExStyle = cp.ExStyle And Not NativeMethod.WS_EX_APPWINDOW ' 不显示在TaskBar
                cp.ExStyle = cp.ExStyle Or NativeMethod.WS_EX_TOOLWINDOW ' 不显示在Alt-Tab
                cp.ExStyle = cp.ExStyle Or NativeMethod.WS_EX_LAYERED
            End If
            Return cp
        End Get
    End Property

    ''' <summary>
    ''' 窗口的 WndProc 方法
    ''' </summary>
    Protected Overrides Sub WndProc(ByRef m As Message)
        If Not DesignMode Then
            Select Case m.Msg
                Case NativeMethod.WM_NCPAINT
                    If NativeMethod.CheckAeroEnabled() Then
                        Dim val = NativeMethod.DWMNC_ENABLED
                        Const intSize = 4
                        NativeMethod.DwmSetWindowAttribute(Handle, NativeMethod.DWMWA_EXCLUDED_FROM_PEEK, val, intSize)
                    End If
                    Exit Select
            End Select
        End If
        MyBase.WndProc(m)
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

    Private _closing As Boolean = False

    ''' <summary>
    ''' 鼠标移动，不透明化窗口
    ''' </summary>
    Private Sub FormMouseMove(sender As Object, e As EventArgs)
        If Not _closing Then
            If Cursor.Position.X >= Left And Cursor.Position.X <= Right And Cursor.Position.Y >= Top And Cursor.Position.Y <= Bottom Then
                FormOpacityUp()
            End If
        End If
    End Sub

    ''' <summary>
    ''' 鼠标移出，透明化窗口，需要判断 MouseLeaveCallback
    ''' </summary>
    Private Sub FormMouseLeave(sender As Object, e As EventArgs)
        If MouseLeaveCallback IsNot Nothing AndAlso MouseLeaveCallback.Invoke() Then
            FormOpacityDown()
        End If
    End Sub

    Protected Overrides Sub OnFormClosing(e As FormClosingEventArgs)
        MyBase.OnFormClosing(e)
        _closing = True
        e.Cancel = Opacity > 0
        CloseForm()
    End Sub

    Private Sub ShowForm()
        _timerMouseIn.Enabled = False
        _timerMouseOut.Enabled = False
        _timerCloseForm.Enabled = False
        _timerShowForm.Enabled = True
    End Sub

    Private Sub CloseForm()
        _timerMouseIn.Enabled = False
        _timerMouseOut.Enabled = False
        _timerShowForm.Enabled = False
        _timerCloseForm.Enabled = True
    End Sub

    Public Sub FormOpacityUp()
        _timerShowForm.Enabled = False
        _timerCloseForm.Enabled = False
        _timerMouseOut.Enabled = False
        _timerMouseIn.Enabled = True
    End Sub

    Public Sub FormOpacityDown()
        _timerShowForm.Enabled = False
        _timerCloseForm.Enabled = False
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

    Private _prevHeight As Integer
    Private _prevWidth As Integer

    Private Sub DisplaySettingsChanged(sender As Object, e As EventArgs)
        Dim currHeight = Screen.PrimaryScreen.Bounds.Height
        Dim currWidth = Screen.PrimaryScreen.Bounds.Width

        ' 修改窗口位置
        If Left + Width > currWidth Then
            Left = currWidth - Width ' 保证窗体仍然在屏幕内
        ElseIf _prevHeight <> 0 AndAlso _prevWidth <> 0 Then
            Left += currWidth - _prevWidth ' 默认靠右
        End If

        _prevHeight = currHeight
        _prevWidth = currWidth
    End Sub

#End Region
End Class
