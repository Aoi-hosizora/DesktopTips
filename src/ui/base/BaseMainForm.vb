Imports DD = DevComponents.DotNetBar

''' <summary>
''' 实现了 透明度动画 窗口拖动 状态栏不显示
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

    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)
        Me.Controls.Add(_labelFocus)
        Me.Opacity = 0
        ShowForm()

        AddHandler Me.MouseMove, AddressOf FormMouseMove
        AddHandler Me.MouseLeave, AddressOf FormMouseLeave

        AddHandler Me.MouseDown, AddressOf FormMouseDown
        AddHandler Me.MouseUp, AddressOf FormMouseUp
        AddHandler Me.MouseMove, AddressOf FormMouseResize

        For Each ctrl As Control In Me.Controls
            AddHandler ctrl.MouseMove, AddressOf FormMouseMove
            AddHandler ctrl.MouseLeave, AddressOf FormMouseLeave

            AddHandler ctrl.MouseDown, AddressOf FormMouseDown
            AddHandler ctrl.MouseUp, AddressOf FormMouseUp
            If ctrl.GetType() <> GetType(Button) And ctrl.GetType() <> GetType(DD.ButtonX) Then
                AddHandler ctrl.MouseMove, AddressOf FormMouseResize
            End If

            If ctrl.GetType() = GetType(DD.ButtonX) Then
                AddHandler ctrl.MouseDown, AddressOf HideButtonXFocus
            End If
        Next
        HideButtonXFocus(Me, e)
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

    Protected Sub FormOpacityUp()
        _timerCloseForm.Enabled = False
        _timerShowForm.Enabled = False
        _timerMouseOut.Enabled = False
        _timerMouseIn.Enabled = True
    End Sub

    Protected Sub FormOpacityDown()
        _timerCloseForm.Enabled = False
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

    Private Sub FormMouseResize(sender As Object, e As MouseEventArgs)
        If IsMouseDown Then
            Me.Cursor = Cursors.SizeAll
            Me.Top = _pushDownWindowPos.Y + Cursor.Position.Y - _pushDownMouseInScreen.Y
            Me.Left = _pushDownWindowPos.X + Cursor.Position.X - _pushDownMouseInScreen.X
        End If
    End Sub

#End Region
End Class
