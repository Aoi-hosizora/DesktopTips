Imports System.Drawing.Drawing2D
Imports DD = DevComponents.DotNetBar

''' <summary>
''' 悬浮卡片
''' </summary>
Public Class HoverCardView
    Inherits BaseEscCbForm

#Region "属性"

    ''' <summary>
    ''' 悬浮卡片的 TipItem 内容，可为 Nothing
    ''' </summary>
    Public Property HoverTipItem As TipItem

    ''' <summary>
    ''' 悬浮卡片的 Tab 内容，不能为 Nothing
    ''' </summary>
    Public Property HoverTab As Tab

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
            If HoverTipItem IsNot Nothing Then
                Dim sze = TextRenderer.MeasureText(HoverTipItem.Content, Font)
                Dim newWidth = sze.Width + _contentHMargin * 2 + 18 ' Extra 18
                If newWidth > Screen.PrimaryScreen.Bounds.Width * 2 / 5 Then
                    newWidth = Screen.PrimaryScreen.Bounds.Width * 2 / 5
                End If
                If newWidth < 200 Then
                    newWidth = 200
                End If
                Return newWidth
                ' Select Case HoverTipItem.Content.Length
                '     Case <= 100 : Return 200
                '     Case <= 300 : Return 250
                '     Case <= 800 : Return 400
                '     Case Else : Return 500
                ' End Select
            Else If HoverTab IsNot Nothing Then
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
            If Screen.AllScreens.Length > 1 And curPos.X > Screen.PrimaryScreen.Bounds.Width Then
                x -= Screen.PrimaryScreen.Bounds.Width ' 扩展屏幕
            End If
            If HoverTipItem Is Nothing Or x >= Screen.PrimaryScreen.Bounds.Width - CardWidth Then
                x = curPos.X - (cliPos.X + CardWidth + HoverGapDistance)
            Else If Screen.AllScreens.Length > 1 And curPos.X > Screen.PrimaryScreen.Bounds.Width Then
                x += Screen.PrimaryScreen.Bounds.Width ' 扩展屏幕
            End If
            Return New Point(x, curPos.Y)
        End Get
    End Property

    Protected Overrides ReadOnly Property ShowWithoutActivation As Boolean = True

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
        Width = CardWidth ' <<<
        InitialLayout()
        Dim pos = CardPosition
        Left = pos.X ' <<<
        Top = pos.Y ' <<<
        If Height + Top > Screen.PrimaryScreen.Bounds.Height - 10 Then
            Top = Screen.PrimaryScreen.Bounds.Height - Height - 10
        End If
        If Top < 0 Then Top = 0
        Top -= 1 / OpacitySpeed

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
    Private ReadOnly _splitterColor As Color = Color.FromArgb(60, 86, 159)

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)
        Dim g = e.Graphics
        Dim brush = New LinearGradientBrush(ClientRectangle, _startColor, _endColor, LinearGradientMode.Vertical)
        g.FillRectangle(brush, ClientRectangle)
        ControlPaint.DrawBorder(e.Graphics, ClientRectangle, _borderColor, ButtonBorderStyle.Solid)
    End Sub

    Private ReadOnly _titleLabel As New DD.LabelX With { .BackColor = Color.Transparent, .AutoSize = True, .WordWrap = False }
    Private ReadOnly _contentLabel As New DD.LabelX With { .BackColor = Color.Transparent, .AutoSize = True, .WordWrap = True }
    Private ReadOnly _timeLabel As New DD.LabelX With { .BackColor = Color.Transparent, .AutoSize = False, .WordWrap = False, .Font = New Font("微软雅黑", 8.0!), .TextLineAlignment = StringAlignment.Far }
    Private ReadOnly _button As New DD.ButtonX With { .Text = "×", .Tooltip = "关闭", .BackColor = Color.Transparent, .AccessibleRole = AccessibleRole.PushButton, .Style = DD.eDotNetBarStyle.StyleManagerControlled, .Shape = New DD.RoundRectangleShapeDescriptor() }

    Private ReadOnly _titleHMargin = 10
    Private ReadOnly _titleVMargin = 5
    Private ReadOnly _contentHMargin = 8
    Private ReadOnly _contentVMargin = 2
    Private ReadOnly _timeHMargin = 10
    Private ReadOnly _timeVMargin = 8
    Private ReadOnly _timeHeight = 44
    Private ReadOnly _buttonSize = 16
    Private ReadOnly _buttonMargin = 5
    Private ReadOnly _bottom = 4

    ''' <summary>
    ''' 加载布局
    ''' </summary>
    Private Sub InitialLayout()
        _titleLabel.BackgroundStyle.CornerType = DD.eCornerType.Square
        _titleLabel.MaximumSize = New Size(Width - _titleHMargin * 2 - 2 * _buttonMargin, 0)
        _contentLabel.BackgroundStyle.CornerType = DD.eCornerType.Square
        _contentLabel.MaximumSize = New Size(Width - _contentHMargin * 2, 0)
        _timeLabel.BackgroundStyle.CornerType = DD.eCornerType.Square
        _timeLabel.PaddingLeft = _timeHMargin
        _timeLabel.BackgroundStyle.BorderTop = DD.eStyleBorderType.Solid
        _timeLabel.BackgroundStyle.BorderTopWidth = 1
        _timeLabel.BackgroundStyle.BorderTopColor = _splitterColor
        _button.ColorTable = DD.eButtonColor.Orange
        _button.Size = New Size(_buttonSize, _buttonSize)
        AddHandler _button.MouseDown, Sub() _titleLabel.Focus()
        AddHandler _button.Click, Sub() Close()

        Controls.Clear()
        Controls.Add(_button)
        Controls.Add(_titleLabel)
        Controls.Add(_contentLabel)
        Controls.Add(_timeLabel)

        Dim tip = HoverTipItem
        Dim tab = HoverTab
        If tip IsNot Nothing Then ' For Tip
            Dim title1 = tab.Title
            Dim title2 = "未高亮"
            If tip.IsHighLight Then
                title2 = $"<font color=""{tip.Color.HexColor}"">{tip.Color.Name}</font>高亮"
            End If
            If tip.Done Then
                title2 &= " 已完成"
            End If

            ' See https://en.wikipedia.org/wiki/Thin_space
            ' Dim body = tip.Content.Replace(" ", "  ").Replace("&", "&&").Replace(vbNewLine, "<br/>")
            Dim body = tip.Content.Replace("&", "&&")
            If body.Length > 2000 Then
                body = body.Substring(0, 2000) & "..."
            End If
            Dim time = "创建于 " & If(tip.IsDefaultCreatedAt, "未知时间", tip.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss"))
            time &= "<br/>更新于 " & If(tip.IsDefaultUpdatedAt, "未知时间", tip.UpdatedAt.ToString("yyyy-MM-dd HH:mm:ss"))

            _titleLabel.Text = $"<b>{title1} - {title2}</b>"
            _contentLabel.EnableMarkup = False
            _contentLabel.Text = body
            _timeLabel.Text = time
        Else ' For Tab
            Dim title = tab.Title & " 分组"
            Dim body = $"总共有 {tab.Tips.Count} 项" & If(tab.Tips.Count = 0, "", "，其中：")
            Dim counts = tab.Tips.GroupBy(Function(t) t.Color).Select(Function(g) New Tuple(Of TipColor, Integer)(g.Key, g.Count())).OrderBy(Function(g) g.Item1?.Id)
            For Each g In counts
                If g.Item1 Is Nothing Then
                    body &= $"<br/><font>无高亮</font>：{g.Item2} 项"
                Else
                    body &= $"<br/><font color=""{g.Item1.HexColor}"">{g.Item1.Name}</font>：{g.Item2} 项"
                End If
            Next
            Dim time = "创建于 " & If(tab.IsDefaultCreatedAt, "未知时间", tab.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss"))
            time &= "<br/>更新于 " & If(tab.IsDefaultUpdatedAt, "未知时间", tab.UpdatedAt.ToString("yyyy-MM-dd HH:mm:ss"))

            _titleLabel.Text = $"<b>{title}</b>"
            _contentLabel.EnableMarkup = True
            _contentLabel.Text = body
            _timeLabel.Text = time
        End If

        _titleLabel.Location = New Point(_titleHMargin, _titleVMargin)
        _contentLabel.Location = New Point(_contentHMargin, _titleLabel.Top + _titleLabel.Height + _contentVMargin)
        _timeLabel.Size = New Size(Width, _timeHeight)
        _timeLabel.Location = New Point(0, _contentLabel.Top + _contentLabel.Height + _timeVMargin)
        _button.Location = New Point(Width - _buttonMargin - _buttonSize, _buttonMargin)
        Height = _timeLabel.Top + _timeLabel.Height + _bottom
    End Sub

#End Region
End Class
