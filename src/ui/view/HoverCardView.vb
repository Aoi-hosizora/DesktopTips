Imports System.Drawing.Drawing2D
Imports DD = DevComponents.DotNetBar

''' <summary>
''' 悬浮卡片
''' </summary>
Public Class HoverCardView
    Inherits Form

#Region "属性"

    ''' <summary>
    ''' 悬浮卡片的 TipItem 内容，可为 Nothing
    ''' </summary>
    Public Property HoverTipFunc As Func(Of TipItem)

    ''' <summary>
    ''' 悬浮卡片的 Tab 内容，不能为 Nothing
    ''' </summary>
    Public Property HoverTabFunc As Func(Of Tab)

    ''' <summary>
    ''' 悬浮卡片显示时的光标位置
    ''' </summary>
    Public Property HoverCursorPosition As Point

    ''' <summary>
    ''' 悬浮卡片显示时 Parent 的光标位置
    ''' </summary>
    Public Property HoverParentPosition As Point

    ''' <summary>
    ''' 悬浮卡片显示时 Parent 的大小
    ''' </summary>
    Public Property HoverParentSize As Size

    ''' <summary>
    ''' 悬浮卡片显示的间隙
    ''' </summary>
    Private ReadOnly Property HoverGapDistance As Integer = 7

    ''' <summary>
    ''' 卡片窗口大小
    ''' </summary>
    Private ReadOnly Property CardWidth As Integer
        Get
            If HoverTipFunc IsNot Nothing Then
                Dim value = HoverTipFunc.Invoke()
                Select Case value.Content.Length
                    Case <= 100 : Return 200
                    Case <= 300 : Return 250
                    Case <= 800 : Return 400
                    Case Else : Return 500
                End Select
            Else If HoverTabFunc IsNot Nothing Then
                Return 200
            End If
            Return -1
        End Get
    End Property

    ''' <summary>
    ''' 卡片窗口位置
    ''' </summary>
    Private ReadOnly Property CardPosition As Point
        Get
            If CardWidth < 0 Then
                Return New Point(0, 0)
            End If
            Dim curPos = HoverCursorPosition
            Dim cliPos = HoverParentPosition
            Dim x = curPos.X - cliPos.X + HoverParentSize.Width + HoverGapDistance
            If x >= Screen.PrimaryScreen.Bounds.Width - CardWidth Then
                x = curPos.X - (cliPos.X + CardWidth + HoverGapDistance)
            End If
            Dim y = curPos.Y
            Return New Point(x, y)
        End Get
    End Property

    Protected Overrides ReadOnly Property ShowWithoutActivation As Boolean = True

    ''' <summary>
    ''' 显示的 TipItem
    ''' </summary>
    Private _tip As TipItem

    ''' <summary>
    ''' 显示的 Tab
    ''' </summary>
    Private _tab As Tab

#End Region

    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)
        AutoScaleMode = AutoScaleMode.Font
        Font = New Font("Microsoft YaHei UI", 9.0!)
        FormBorderStyle = FormBorderStyle.None
        ShowInTaskbar = false
        Opacity = 0

        If CardWidth <= 0 Then
            Close()
            Return
        End If
        If HoverTabFunc IsNot Nothing Then _tab = HoverTabFunc.Invoke()
        If HoverTipFunc IsNot Nothing Then _tip = HoverTipFunc.Invoke()
        Width = CardWidth ' <<<
        Dim pos = CardPosition
        Left = pos.X ' <<<
        Top = pos.Y ' <<<
        If Height + Top > My.Computer.Screen.Bounds.Height Then
            Top = My.Computer.Screen.Bounds.Height - Height
        End If
        Top -= 1 / OpacitySpeed

        InitLabel()
        _timerCloseForm.Enabled = false
        _timerShowForm.Enabled = true
    End Sub

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

    Protected Overrides Sub WndProc(ByRef m As Message)
        Select Case m.Msg
            Case NativeMethod.WM_NCPAINT
                If NativeMethod.CheckAeroEnabled() Then
                    Dim val = NativeMethod.DWMNC_ENABLED
                    Const intSize = 4
                    NativeMethod.DwmSetWindowAttribute(Handle, NativeMethod.DWMWA_NCRENDERING_POLICY, val, intSize)
                    NativeMethod.DwmExtendFrameIntoClientArea(Handle, New NativeMethod.MARGINS() With {
                        .BottomHeight = 1, .LeftWidth = 1, .RightWidth = 1, .TopHeight = 1 })
                End If
                Exit Select
        End Select
        MyBase.WndProc(m)
    End Sub

#Region "Timer"

    Private Const OpacitySpeed = 0.1
    Private WithEvents _timerShowForm As New Timer() With {.Interval = 1, .Enabled = False}
    Private WithEvents _timerCloseForm As New Timer() With {.Interval = 1, .Enabled = False}

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

#End Region

#Region "布局"

    Private ReadOnly _borderColor As Color = Color.FromArgb(200, 200, 200)
    Private ReadOnly _startColor As Color = Color.white
    Private ReadOnly _endColor As Color = Color.FromArgb(229, 229, 240)

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)
        Dim g = e.Graphics
        Dim brush = New LinearGradientBrush(ClientRectangle, _startColor, _endColor, LinearGradientMode.Vertical)
        g.FillRectangle(brush, ClientRectangle)
        ControlPaint.DrawBorder(e.Graphics, ClientRectangle, _borderColor, ButtonBorderStyle.Solid)
    End Sub

    Private ReadOnly _titleLabel As New DD.LabelX With { .BackColor = Color.Transparent,.AutoSize = True, .WordWrap = False  }
    Private ReadOnly _contentLabel As New DD.LabelX With { .BackColor = Color.Transparent,.AutoSize = True, .WordWrap = False  }
    Private ReadOnly _button As New DD.ButtonX With { .BackColor = Color.Transparent, .Text = "×", .Tooltip = "关闭", .AccessibleRole = AccessibleRole.PushButton }

    Private ReadOnly _titleLeft = 10
    Private ReadOnly _contentLeft = 8
    Private ReadOnly _titleTop = 5
    Private ReadOnly _contentTop = 2
    Private ReadOnly _bottom = 6
    Private ReadOnly _buttonSize = 14
    Private ReadOnly _buttonMargin = 5

    ''' <summary>
    ''' 加载 Label 布局
    ''' </summary>
    Private Sub InitLabel()
        _titleLabel.BackgroundStyle.CornerType = DD.eCornerType.Square
        _titleLabel.MaximumSize = New Size(Width - _titleLeft * 2 - 2 * _buttonMargin, 0)
        _contentLabel.BackgroundStyle.CornerType = DD.eCornerType.Square
        _contentLabel.MaximumSize = New Size(Width - _contentLeft * 2, 0)
        _button.ColorTable = DD.eButtonColor.Orange
        _button.Size = New Size(_buttonSize, _buttonSize)
        _button.Style = DD.eDotNetBarStyle.StyleManagerControlled
        _button.Shape = New DD.RoundRectangleShapeDescriptor()
        AddHandler _button.MouseDown, Sub() _titleLabel.Focus()
        AddHandler _button.Click, Sub() Close()

        Controls.Clear()
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
            If _tip.Done Then
                highlight &= " 已完成"
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

#End Region
End Class
