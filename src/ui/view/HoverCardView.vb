Imports System.Drawing.Drawing2D
Imports DD = DevComponents.DotNetBar

Public Class HoverCardView
    Inherits Form

    Private WithEvents _timerShowForm As New Timer() With {.Interval = 1, .Enabled = False}
    Private WithEvents _timerCloseForm As New Timer() With {.Interval = 1, .Enabled = False}

    Public ReadOnly Property OpacitySpeed = 0.1

    Public Property WidthFunc As Func(Of Integer)
    Public Property HoverTipFunc As Func(Of TipItem)
    Public Property HoverTabFunc As Func(Of Tab)
    Public Property FocusFormFunc As Action

    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)
        Me.AutoScaleMode = AutoScaleMode.Font
        Me.Font = New Font("Microsoft YaHei UI", 9.0!)
        Me.FormBorderStyle = FormBorderStyle.None
        ' Me.BackColor = Color.Snow
        Me.ShowInTaskbar = false
        Me.TopMost = true
        Me.Opacity = 0
        Me.Width = If(WidthFunc IsNot Nothing, WidthFunc.Invoke(), 200)

        If HoverTipFunc Is Nothing OrElse HoverTabFunc Is Nothing OrElse FocusFormFunc Is Nothing Then
            Me.Close()
            Return
        End If
        _tip = HoverTipFunc.Invoke()
        _tab = HoverTabFunc.Invoke()
        initLabel()

        _timerCloseForm.Enabled = false
        _timerShowForm.Enabled = true
    End Sub

    Protected Overrides ReadOnly Property CreateParams As CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ExStyle = cp.ExStyle And Not NativeMethod.WS_EX_APPWINDOW ' 不显示在TaskBar
            cp.ExStyle = cp.ExStyle Or NativeMethod.WS_EX_TOOLWINDOW ' 不显示在Alt-Tab
            If Not NativeMethod.CheckAeroEnabled() Then
                cp.ClassStyle = cp.ClassStyle Or NativeMethod.CS_DROPSHADOW
            End If
            Return cp
        End Get
    End Property

    Protected Overrides Sub OnFormClosing(e As FormClosingEventArgs)
        MyBase.OnFormClosing(e)
        e.Cancel = Opacity > 0
        _timerShowForm.Enabled = False
        _timerCloseForm.Enabled = true
    End Sub

    Private Sub TimerShowForm_Tick(sender As Object, e As EventArgs) Handles _timerShowForm.Tick
        Me.Opacity += OpacitySpeed
        Me.Top += 1
        If Me.Opacity >= 1 Then
            Me.Opacity = 1
            FocusFormFunc.Invoke()
            _timerShowForm.Enabled = False
        End If
    End Sub

    Private Sub TimerCloseForm_Tick(sender As Object, e As EventArgs) Handles _timerCloseForm.Tick
        Me.Opacity -= OpacitySpeed
        Me.Top -= 1
        If Me.Opacity <= 0 Then
            MyBase.Close()
            _timerCloseForm.Enabled = False
        End If
    End Sub

    Private _titleLabel As New DD.LabelX()
    Private _contentLabel As New DD.LabelX()

    Private _tip As TipItem
    Private _tab As Tab

    Private Sub initLabel()
        _titleLabel.BackgroundStyle.CornerType = DD.eCornerType.Square
        _titleLabel.AutoSize = True
        _titleLabel.MaximumSize = New Size(Me.Width - 8 * 2, 0)
        _titleLabel.WordWrap = False
        _titleLabel.BackColor = Color.Transparent

        _contentLabel.BackgroundStyle.CornerType = DD.eCornerType.Square
        _contentLabel.AutoSize = True
        _contentLabel.MaximumSize = New Size(Me.Width - 8 * 2, 0)
        _contentLabel.WordWrap = True
        _contentLabel.BackColor = Color.Transparent

        Me.Controls.Add(_contentLabel)
        Me.Controls.Add(_titleLabel)

        Dim title = _tab.Title
        Dim highlight = "未高亮"
        Dim body = _tip.Content
        If _tip.IsHighLight Then
            highlight = $"<font color=""{_tip.Color.HexColor}"">{_tip.Color.Name}</font>高亮"
        End If

        _titleLabel.Text = $"<b>{title} - {highlight}</b>"
        _contentLabel.Text = body

        _titleLabel.Location = New Point(8, 4)
        _contentLabel.Location = New Point(8, _titleLabel.Top + _titleLabel.Height + 4)

        Me.Height = _contentLabel.Top + _contentLabel.Height + 8
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)
        Dim g = e.Graphics
        Dim brush = New LinearGradientBrush(ClientRectangle, Color.White, Color.FromArgb(229, 229, 240), LinearGradientMode.Vertical)
        g.FillRectangle(brush, ClientRectangle)
        ControlPaint.DrawBorder(e.Graphics, ClientRectangle, Color.FromArgb(200, 200, 200), ButtonBorderStyle.Solid)
    End Sub

#Region "P/Invoke Stuff"

    Protected Overrides Sub WndProc(ByRef m As Message)
        Select Case m.Msg
            Case NativeMethod.WM_NCPAINT
                Dim val = 2
                If NativeMethod.CheckAeroEnabled() Then
                    NativeMethod.DwmSetWindowAttribute(Handle, 2, val, 4)
                    NativeMethod.DwmExtendFrameIntoClientArea(Handle, New NativeMethod.MARGINS() With {.bottomHeight = 1, .leftWidth = 1, .rightWidth = 1, .topHeight = 1})
                End If
                Exit Select
        End Select
        MyBase.WndProc(m)
    End Sub

#End Region
End Class