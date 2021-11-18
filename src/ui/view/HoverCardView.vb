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
                Dim sze = TextRenderer.MeasureText(HoverTipItem.Content, Font, Size.Empty)
                Dim wantWidth = sze.Width + _contentHMargin * 2 + 30
                Dim maxWidth = Screen.PrimaryScreen.Bounds.Width * 5 / 7
                Dim minWidth = 200
                Return Math.Max(Math.Min(wantWidth, maxWidth), minWidth)
            ElseIf HoverTab IsNot Nothing Then
                Return 200
            End If
            Return 1
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

            ' 出现在右边
            Dim x = curPos.X - HoverParentPosition.X + HoverParentSize.Width + HoverGapDistance
            If Screen.AllScreens.Length > 1 AndAlso curPos.X > Screen.PrimaryScreen.Bounds.Width Then
                x -= Screen.PrimaryScreen.Bounds.Width ' 扩展屏幕
            End If

            ' 出现在左边
            If HoverTipItem Is Nothing Or x >= Screen.PrimaryScreen.Bounds.Width - CardWidth Then
                x = curPos.X - HoverParentPosition.X - CardWidth - HoverGapDistance
                If Screen.AllScreens.Length > 1 AndAlso curPos.X > Screen.PrimaryScreen.Bounds.Width Then
                    x += Screen.PrimaryScreen.Bounds.Width ' 扩展屏幕
                End If
                If x < 0 Then
                    x = 0 ' 极端情况
                    curPos.Y += 10
                End If
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
    Private ReadOnly _splitterColor As Color = Color.FromArgb(40, 66, 139)

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)
        Dim g = e.Graphics
        Dim brush = New LinearGradientBrush(ClientRectangle, _startColor, _endColor, LinearGradientMode.Vertical)
        g.FillRectangle(brush, ClientRectangle)
        ControlPaint.DrawBorder(e.Graphics, ClientRectangle, _borderColor, ButtonBorderStyle.Solid)
    End Sub

    Private ReadOnly _titleLabel As New DD.LabelX With {.BackColor = Color.Transparent, .AutoSize = True, .WordWrap = False}
    Private ReadOnly _contentLabel As New DD.LabelX With {.BackColor = Color.Transparent, .AutoSize = True, .WordWrap = True}
    Private ReadOnly _metaLabel As New DD.LabelX With {.BackColor = Color.Transparent, .AutoSize = False, .WordWrap = False, .Font = New Font("微软雅黑", 8.0!), .TextLineAlignment = StringAlignment.Far}
    Private ReadOnly _button As New DD.ButtonX With {.Text = "×", .Tooltip = "关闭", .BackColor = Color.Transparent, .AccessibleRole = AccessibleRole.PushButton, .Style = DD.eDotNetBarStyle.StyleManagerControlled, .Shape = New DD.RoundRectangleShapeDescriptor()}

    Private ReadOnly _titleHMargin = 10
    Private ReadOnly _titleVMargin = 5
    Private ReadOnly _contentHMargin = 8
    Private ReadOnly _contentVMargin = 2
    Private ReadOnly _mataHMargin = 10
    Private ReadOnly _metaVMargin = 8
    Private ReadOnly _metaHeight = 44
    Private _metaExtraHeight = 0
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
        _metaLabel.BackgroundStyle.CornerType = DD.eCornerType.Square
        _metaLabel.PaddingLeft = _mataHMargin
        _metaLabel.BackgroundStyle.BorderTop = DD.eStyleBorderType.Solid
        _metaLabel.BackgroundStyle.BorderTopWidth = 1
        _metaLabel.BackgroundStyle.BorderTopColor = _splitterColor
        _button.ColorTable = DD.eButtonColor.Orange
        _button.Size = New Size(_buttonSize, _buttonSize)
        AddHandler _button.MouseDown, Sub() _titleLabel.Focus()
        AddHandler _button.Click, Sub() Close()

        Controls.Clear()
        Controls.Add(_button)
        Controls.Add(_titleLabel)
        Controls.Add(_contentLabel)
        Controls.Add(_metaLabel)

        Dim tip = HoverTipItem
        Dim tab = HoverTab
        If tip IsNot Nothing Then ' For Tip
            Dim title1 = tab.Title
            Dim title2 = "未高亮"
            If tip.IsHighLight Then
                title2 = $"{TipColorToString(tip.Color)}高亮"
            End If
            If tip.Done Then
                title2 &= " 已完成"
            End If

            ' See https://en.wikipedia.org/wiki/Thin_space
            ' Dim body = tip.Content.Replace(" ", "  ").Replace("&", "&&").Replace(vbNewLine, "<br/>")
            Dim bodyCount = tip.Content.Length
            Dim body = tip.Content.Replace("&", "&&")
            If tip.TextType = CommonUtil.TextType.Markdown Then
                body = CommonUtil.Markdown2Markup(CommonUtil.EscapeForXML(tip.Content))
            ElseIf tip.TextType = CommonUtil.TextType.HTML Then
                body = CommonUtil.SugarText2Markup(tip.Content)
            End If
            If body.Length > 5000 Then
                body = body.Substring(0, 4997) & "..."
            End If
            Dim time = "创建于 " & If(tip.IsDefaultCreatedAt, "未知时间", tip.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss"))
            time &= "<br/>更新于 " & If(tip.IsDefaultUpdatedAt, "未知时间", tip.UpdatedAt.ToString("yyyy-MM-dd HH:mm:ss"))

            _titleLabel.EnableMarkup = True
            _titleLabel.Text = $"<b>{title1} - {title2}</b>"
            _contentLabel.EnableMarkup = tip.TextType <> CommonUtil.TextType.Plain
            _contentLabel.Text = body
            _metaLabel.Text = $"标签字符总数 {bodyCount}<br/>{time}"
            _metaExtraHeight += 17
        Else ' For Tab
            Dim title = CommonUtil.EscapeForXML(tab.Title) & " 分组"
            Dim body = $"总共有 {tab.Tips.Count} 项" & If(tab.Tips.Count = 0, "", "，其中：")
            Dim counts = tab.Tips.GroupBy(Function(t) t.Color).Select(Function(g) New Tuple(Of TipColor, Integer)(g.Key, g.Count())).OrderBy(Function(g) g.Item1?.Id)
            For Each g In counts
                If g.Item1 Is Nothing Then
                    body &= $"<br/>•  <font>无高亮</font>：{g.Item2} 项"
                Else
                    body &= $"<br/>•  {TipColorToString(g.Item1)}：{g.Item2} 项"
                End If
            Next
            Dim time = "创建于 " & If(tab.IsDefaultCreatedAt, "未知时间", tab.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss"))
            time &= "<br/>更新于 " & If(tab.IsDefaultUpdatedAt, "未知时间", tab.UpdatedAt.ToString("yyyy-MM-dd HH:mm:ss"))

            _titleLabel.EnableMarkup = True
            _titleLabel.Text = $"<b>{title}</b>"
            _contentLabel.EnableMarkup = True
            _contentLabel.Text = body
            _metaLabel.Text = time
        End If

        _titleLabel.Location = New Point(_titleHMargin, _titleVMargin)
        _contentLabel.Location = New Point(_contentHMargin, _titleLabel.Top + _titleLabel.Height + _contentVMargin)
        _metaLabel.Size = New Size(Width, _metaHeight + _metaExtraHeight)
        _metaLabel.Location = New Point(0, _contentLabel.Top + _contentLabel.Height + _metaVMargin)
        _button.Location = New Point(Width - _buttonMargin - _buttonSize, _buttonMargin)
        Height = _metaLabel.Top + _metaLabel.Height + _bottom
    End Sub

    Private Function TipColorToString(tc As TipColor) As String
        Dim r = tc.Name
        If (tc.Style And FontStyle.Bold) > 0 Then
            r = $"<b>{r}</b>"
        End If
        If (tc.Style And FontStyle.Italic) > 0 Then
            r = $"<i>{r}</i>"
        End If
        If (tc.Style And FontStyle.Underline) > 0 Then
            r = $"<u>{r}</u>"
        End If
        Return $"<font color=""{tc.HexColor}"">{r}</font>"
    End Function

#End Region
End Class
