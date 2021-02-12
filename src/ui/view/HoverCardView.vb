Imports System.Drawing.Drawing2D
Imports DD = DevComponents.DotNetBar

Public Class HoverCardView
    Inherits Form

    Private WithEvents _timerShowForm As New Timer() With {.Interval = 1, .Enabled = False}
    Private WithEvents _timerCloseForm As New Timer() With {.Interval = 1, .Enabled = False}

    Private ReadOnly Property OpacitySpeed = 0.1
    Public Property PreLocation As New Point()

    Public Property WidthFunc As Func(Of Integer)
    Public Property HoverTipFunc As Func(Of TipItem)
    Public Property HoverTabFunc As Func(Of Tab)

    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)
        AutoScaleMode = AutoScaleMode.Font
        Font = New Font("Microsoft YaHei UI", 9.0!)
        FormBorderStyle = FormBorderStyle.None
        ShowInTaskbar = false
        Opacity = 0
        If WidthFunc Is Nothing OrElse HoverTabFunc Is Nothing Then
            Close()
            Return
        End If
        Width = WidthFunc.Invoke()
        _tab = HoverTabFunc.Invoke()
        If Not HoverTipFunc Is Nothing Then
            _tip = HoverTipFunc.Invoke()
        End If
        InitLabel()

        Left = PreLocation.X
        Top = PreLocation.Y
        If Height + Top > My.Computer.Screen.Bounds.Height Then
            Top = My.Computer.Screen.Bounds.Height - Height
        End If
        Top -= 1 / OpacitySpeed

        _timerCloseForm.Enabled = false
        _timerShowForm.Enabled = true
    End Sub

    Protected Overrides ReadOnly Property ShowWithoutActivation As Boolean = True

    Protected Overrides ReadOnly Property CreateParams As CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ExStyle = cp.ExStyle And Not NativeMethod.WS_EX_APPWINDOW ' 不显示在TaskBar
            cp.ExStyle = cp.ExStyle Or NativeMethod.WS_EX_TOOLWINDOW ' 不显示在Alt-Tab
            cp.ExStyle = cp.ExStyle Or NativeMethod.WS_EX_TOPMOST ' TopMost
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
        Opacity += OpacitySpeed
        Top += 1
        If Opacity >= 1 Then
            Opacity = 1
            _timerShowForm.Enabled = False
        End If
    End Sub

    Private Sub TimerCloseForm_Tick(sender As Object, e As EventArgs) Handles _timerCloseForm.Tick
        Opacity -= OpacitySpeed
        Top -= 1
        If Opacity <= 0 Then
            Close()
            _timerCloseForm.Enabled = False
        End If
    End Sub

    Private ReadOnly _titleLabel As New DD.LabelX()
    Private ReadOnly _contentLabel As New DD.LabelX()
    Private WithEvents _button As New DD.ButtonX()

    Private _tip As TipItem
    Private _tab As Tab

    Private ReadOnly _titleLeft = 10
    Private ReadOnly _contentLeft = 8
    Private ReadOnly _titleTop = 5
    Private ReadOnly _contentTop = 2
    Private ReadOnly _bottom = 6
    Private ReadOnly _buttonSize = 14
    Private ReadOnly _buttonMargin = 5

    Private Sub ButtonClose_MouseDown(sender As Object, e As MouseEventArgs) Handles _button.MouseDown
        _titleLabel.Focus()
    End Sub

    Private Sub ButtonClose_Clicked(sender As Object, e As EventArgs) Handles _button.Click
        Close()
    End Sub

    Private Sub InitLabel()
        _titleLabel.BackColor = Color.Transparent
        _titleLabel.BackgroundStyle.CornerType = DD.eCornerType.Square
        _titleLabel.AutoSize = True
        _titleLabel.MaximumSize = New Size(Width - _titleLeft * 2 - 2 * _buttonMargin, 0)
        _titleLabel.WordWrap = False

        _contentLabel.BackColor = Color.Transparent
        _contentLabel.BackgroundStyle.CornerType = DD.eCornerType.Square
        _contentLabel.AutoSize = True
        _contentLabel.MaximumSize = New Size(Width - _contentLeft * 2, 0)
        _contentLabel.WordWrap = True

        _button.BackColor = Color.Transparent
        _button.Text = "×"
        _button.ColorTable = DD.eButtonColor.Orange
        _button.Size = New Size(_buttonSize, _buttonSize)
        _button.Style = DD.eDotNetBarStyle.StyleManagerControlled
        _button.AccessibleRole = AccessibleRole.PushButton
        _button.Shape = New DD.RoundRectangleShapeDescriptor()
        _button.Tooltip = "关闭"

        Controls.Add(_button)
        Controls.Add(_contentLabel)
        Controls.Add(_titleLabel)

        If _tip IsNot Nothing Then
            ' For Tip
            Dim title = _tab.Title
            Dim highlight = "未高亮"
            Dim body = _tip.Content.Replace("&", "&&")
            If body.Length > 1000 Then
                body = body.Substring(0, 1000) & "..."
            End If
            If _tip.IsHighLight Then
                highlight = $"<font color=""{_tip.Color.HexColor}"">{_tip.Color.Name}</font>高亮"
            End If

            _titleLabel.Text = $"<b>{title} - {highlight}</b>"
            _contentLabel.Text = body
        Else
            ' For Tab
            Dim title = _tab.Title & " 分组"
            Dim counts = _tab.Tips.GroupBy(Function(t) t.Color).Select(Function(g) New Tuple(Of TipColor, Integer)(g.Key, g.Count())).OrderBy(Function(g) g.Item1?.Id)
            Dim body = $"总共有 {_tab.Tips.Count} 项，其中："
            For Each g In counts
                If g.Item1 Is Nothing Then
                    body &= $"<br />无高亮：{g.Item2} 项"
                Else
                    body &= $"<br /><font color=""{g.Item1.HexColor}"">{g.Item1.Name}</font>：{g.Item2} 项"
                End If
            Next

            _titleLabel.Text = $"<b>{title}</b>"
            _contentLabel.Text = body
        End If

        _titleLabel.Location = New Point(_titleLeft, _titleTop)
        _contentLabel.Location = New Point(_contentLeft, _titleLabel.Top + _titleLabel.Height + _contentTop)
        _button.Location = New Point(Width - _buttonMargin - _buttonSize, _buttonMargin)
        Height = _contentLabel.Top + _contentLabel.Height + _bottom
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
                If NativeMethod.CheckAeroEnabled() Then
                    Dim val = NativeMethod.DWMNC_ENABLED
                    Const intSize = 4
                    NativeMethod.DwmSetWindowAttribute(Handle, NativeMethod.DWMWA_NCRENDERING_POLICY, val, intSize)
                    NativeMethod.DwmExtendFrameIntoClientArea(Handle, New NativeMethod.MARGINS() With {
                        .BottomHeight = 1, .LeftWidth = 1, .RightWidth = 1, .TopHeight = 1
                        })
                End If
                Exit Select
        End Select
        MyBase.WndProc(m)
    End Sub

#End Region
End Class