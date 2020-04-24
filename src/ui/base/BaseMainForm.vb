Imports DD = DevComponents.DotNetBar

''' <summary>
''' 实现了 透明度动画 窗口拖动 状态栏不显示
''' </summary>
Public Class BaseMainForm
    Inherits Form

    Private WithEvents TimerShowForm As New Timer() With {.Interval = 1, .Enabled = False}
    Private WithEvents TimerCloseForm As New Timer() With {.Interval = 1, .Enabled = False}
    Private WithEvents TimerMouseIn As New Timer() With {.Interval = 10, .Enabled = False}
    Private WithEvents TimerMouseOut As New Timer() With {.Interval = 10, .Enabled = False}
    Private WithEvents LabelFocus As New Label() With {.Visible = False}

    ''' <summary>
    ''' 窗口在未透明时的最大透明度
    ''' </summary>
    Public Property MaxOpacity As Double = 0.6

    Public Delegate Function MouseLeaveOption() As Boolean
    Public Property CanMouseLeave As MouseLeaveOption

    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)
        Me.Controls.Add(LabelFocus)
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
        LabelFocus.Focus()
        LabelFocus.Select()
    End Sub

    Protected Overrides ReadOnly Property CreateParams As System.Windows.Forms.CreateParams
        Get
            Const WS_EX_APPWINDOW As Integer = 16384
            Const WS_EX_TOOLWINDOW As Integer = 128
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ExStyle = cp.ExStyle And Not WS_EX_APPWINDOW ' 不显示在TaskBar
            cp.ExStyle = cp.ExStyle Or WS_EX_TOOLWINDOW ' 不显示在Alt-Tab
            Return cp
        End Get
    End Property

#Region "Timer"

    Private Sub TimerShowForm_Tick(sender As Object, e As EventArgs) Handles TimerShowForm.Tick
        Me.Opacity += 0.08
        Me.Top += 1
        If Me.Opacity >= MaxOpacity Then
            Me.Opacity = MaxOpacity
            TimerShowForm.Enabled = False
        End If
    End Sub

    Private Sub TimerCloseForm_Tick(sender As Object, e As EventArgs) Handles TimerCloseForm.Tick
        Me.Opacity -= 0.08
        Me.Top -= 1
        If Me.Opacity <= 0 Then
            MyBase.Close()
            TimerCloseForm.Enabled = False
        End If
    End Sub

    Private Sub TimerMouseIn_Tick(sender As Object, e As EventArgs) Handles TimerMouseIn.Tick
        Me.Opacity += 0.05
        If Me.Opacity >= 1 Then
            Me.Opacity = 1
            TimerMouseIn.Enabled = False
        End If
    End Sub

    Private Sub TimerMouseOut_Tick(sender As Object, e As EventArgs) Handles TimerMouseOut.Tick
        Me.Opacity -= 0.02
        If Me.Opacity <= Me.MaxOpacity Then
            Me.Opacity = Me.MaxOpacity
            TimerMouseOut.Enabled = False
        End If
    End Sub

#End Region

#Region "Opecity"

    Private Sub FormMouseMove(sender As Object, e As EventArgs)
        If Cursor.Position.X >= Me.Left And Cursor.Position.X <= Me.Right And Cursor.Position.Y >= Me.Top And Cursor.Position.Y <= Me.Bottom Then
            FormOpecityUp()
        End If
    End Sub

    Private Sub FormMouseLeave(sender As Object, e As EventArgs)
        If CanMouseLeave.Invoke() Then
            FormOpecityDown()
        End If
    End Sub

    Private Sub ShowForm()
        TimerMouseIn.Enabled = False
        TimerMouseOut.Enabled = False
        TimerCloseForm.Enabled = False
        TimerShowForm.Enabled = True
    End Sub

    Private Sub CloseForm()
        TimerMouseIn.Enabled = False
        TimerMouseOut.Enabled = False
        TimerShowForm.Enabled = False
        TimerCloseForm.Enabled = True
    End Sub

    Protected Sub FormOpecityUp()
        TimerCloseForm.Enabled = False
        TimerShowForm.Enabled = False
        TimerMouseOut.Enabled = False
        TimerMouseIn.Enabled = True
    End Sub

    Protected Sub FormOpecityDown()
        TimerCloseForm.Enabled = False
        TimerShowForm.Enabled = False
        TimerMouseIn.Enabled = False
        TimerMouseOut.Enabled = True
    End Sub

#End Region

#Region "Move"

    Protected ReadOnly Property IsMouseDown As Boolean
        Get
            Return _IsMouseDown
        End Get
    End Property

    Protected ReadOnly Property PushDownMouseInScreen As Point
        Get
            Return _PushDownMouseInScreen
        End Get
    End Property

    Protected ReadOnly Property PushDownWindowPos As Point
        Get
            Return _pushDownWindowPos
        End Get
    End Property

    Protected ReadOnly Property PushDownWindowSize As Size
        Get
            Return _PushDownWindowSize
        End Get
    End Property

    Private _isMouseDown As Boolean
    Private _pushDownMouseInScreen As Point
    Private _pushDownWindowPos As Point
    Private _PushDownWindowSize As Size

    Private Sub FormMouseDown(sender As Object, e As MouseEventArgs)
        _isMouseDown = e.Button = MouseButtons.Left
        _pushDownMouseInScreen = Cursor.Position
        _pushDownWindowPos = New Point(Me.Left, Me.Top)
        _PushDownWindowSize = New Size(Me.Width, Me.Height)
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
