Imports System.Drawing.Drawing2D
Imports DD = DevComponents.DotNetBar

''' <summary>
''' 悬浮卡片
''' </summary>
Public Class HoverCardView
    Inherits BaseEscCbForm

#Region "成员和属性"

    ''' <summary>
    ''' 悬浮卡片显示时的光标位置
    ''' </summary>
    Private _cursorPositionBefore As Point

    ''' <summary>
    ''' 悬浮卡片显示时 Parent 的光标位置
    ''' </summary>
    Private _cursorPositionInParentBefore As Point

    ''' <summary>
    ''' 悬浮卡片显示时 Parent 的大小
    ''' </summary>
    Private _parentSizeBefore As Size

    ''' <summary>
    ''' 悬浮卡片的 TipItem 内容
    ''' </summary>
    Private _hoverTipItem As TipItem

    ''' <summary>
    ''' 悬浮卡片的 Tab 内容
    ''' </summary>
    Private _hoverTab As Tab

    ''' <summary>
    ''' 关闭回调
    ''' </summary>
    Private _onClose As Action

    ''' <summary>
    ''' 悬浮卡片与 Parent 的显示间隔距离
    ''' </summary>
    Private Const HoverGapDistance As Integer = 7

    ''' <summary>
    ''' 悬浮窗口与屏幕边界的最小间隔距离
    ''' </summary>
    Private Const MinScreenGap As Integer = 1

    ''' <summary>
    ''' 卡片最小宽度
    ''' </summary>
    Private Const MinWidth = 200

    ''' <summary>
    ''' 显示窗口时不激活窗口
    ''' </summary>
    Protected Overrides ReadOnly Property ShowWithoutActivation As Boolean = True

    ''' <summary>
    ''' 卡片窗口大小
    ''' </summary>
    Private Function CalculateCardWidth() As Integer
        If _hoverTipItem IsNot Nothing Then
            Dim sze = TextRenderer.MeasureText(_hoverTipItem.Content, Font, Size.Empty)
            Dim wantWidth% = sze.Width + _contentHMargin * 2 + 45
            Dim maxWidth% = Screen.FromPoint(_cursorPositionBefore).Bounds.Width * 5 / 7
            Return Math.Max(Math.Min(wantWidth, maxWidth), MinWidth)
        End If
        If _hoverTab IsNot Nothing Then
            Return MinWidth
        End If
        Return -1
    End Function

    ''' <summary>
    ''' 卡片窗口位置
    ''' </summary>
    Private Function CalculateCardPosition(cardWidth As Integer) As Point
        Dim curPos = _cursorPositionBefore,
            parCurPos = _cursorPositionInParentBefore

        ' 默认出现在右边
        Dim x = curPos.X - parCurPos.X + _parentSizeBefore.Width + HoverGapDistance ' 右
        Dim overflow As Boolean, scr = Screen.FromPoint(curPos)
        If scr.Primary Then
            overflow = x >= Screen.PrimaryScreen.Bounds.Width - cardWidth
        Else ' 存在扩展屏幕
            overflow = x >= Screen.PrimaryScreen.Bounds.Width + scr.Bounds.Width - cardWidth
        End If

        ' 出现在左边
        If _hoverTipItem Is Nothing Or overflow Then
            x = curPos.X - parCurPos.X - cardWidth - HoverGapDistance ' 左
        End If

        ' 极端情况
        If x < MinScreenGap Then
            Return New Point(MinScreenGap, curPos.Y + 20)
        End If
        Return New Point(x, curPos.Y)
    End Function

#End Region

#Region "显示相关的方法和属性"


    ''' <summary>
    ''' 使用 TipItem 或 Tab 显示悬浮卡片
    ''' </summary>
    Public Shared Sub ShowCardView(cursor As Point, cursorInParent As Point, parentSize As Size,
                                   Optional ti As TipItem = Nothing, Optional ta As Tab = Nothing, Optional onClose As Action = Nothing)
        ' Dim view As New HoverCardView
        HoverCardView._cursorPositionBefore = cursor
        HoverCardView._cursorPositionInParentBefore = cursorInParent
        HoverCardView._parentSizeBefore = parentSize
        HoverCardView._hoverTipItem = ti
        HoverCardView._hoverTab = ta
        HoverCardView._onClose = onClose
        HoverCardView.Opacity = 0
        HoverCardView.Show()
    End Sub

    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)
        AutoScaleMode = AutoScaleMode.Font
        Font = New Font("Microsoft YaHei UI", 9.0!)
        FormBorderStyle = FormBorderStyle.None
        ShowInTaskbar = false
        Opacity = 0
        HorizontalScroll.Maximum = 0
        AutoScroll = True
        AddHandler Scroll, Sub() If VerticalScroll.Visible Then Refresh()
        AddHandler MouseWheel, Sub() If VerticalScroll.Visible Then Refresh()

        Dim cardWidth = CalculateCardWidth()
        If cardWidth <= 0 Then
            MessageBox.Show("Something wrong with HoverCardView.")
            Return
        End If
        Width = cardWidth ' <<<
        Height = InitialLayout()
        Dim pos = CalculateCardPosition(cardWidth) ' <<<
        Left = pos.X
        Top = pos.Y

        Dim scr = Screen.FromPoint(_cursorPositionBefore)
        If Height > scr.Bounds.Height - MinScreenGap * 2 Then
            Top = MinScreenGap
            Height = scr.Bounds.Height - MinScreenGap * 2
            Width += 17
            Left = Math.Max(Left - 17, MinScreenGap)
        ElseIf Height + Top > scr.Bounds.Height - MinScreenGap Then
            Top = Math.Max(MinScreenGap, scr.Bounds.Height - Height - MinScreenGap)
        End If
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

#End Region

#Region "Timer"

    Private Const OpacitySpeed = 0.1
    Private WithEvents _timerShowForm As New Timer() With {.Interval = 1, .Enabled = False}
    Private WithEvents _timerCloseForm As New Timer() With {.Interval = 1, .Enabled = False}

    Protected Overrides Sub OnFormClosing(e As FormClosingEventArgs)
        MyBase.OnFormClosing(e)
        _onClose?.Invoke()
        e.Cancel = Opacity > 0
        _timerShowForm.Enabled = False
        _timerCloseForm.Enabled = true
    End Sub

    Private Sub TimerShowForm_Tick(sender As Object, e As EventArgs) Handles _timerShowForm.Tick
        Opacity += OpacitySpeed
        Top += 1
        If Opacity >= 1 Then
            Opacity = 1
            Dim scr = Screen.FromPoint(_cursorPositionBefore)
            If Height + Top > scr.Bounds.Height - MinScreenGap Then
                Top = Math.Max(MinScreenGap, scr.Bounds.Height - Height - MinScreenGap)
            End If
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
    Private ReadOnly _startColor As Color = Color.White
    Private ReadOnly _endColor As Color = Color.FromArgb(240, 240, 252)
    Private ReadOnly _splitterColor As Color = Color.FromArgb(75, 0, 0, 0)

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
    Private Function InitialLayout() As Integer
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

        Dim tip = _hoverTipItem
        Dim tab = _hoverTab
        If tip IsNot Nothing Then ' For Tip
            Dim title1 = tab.Title
            Dim title2 = "未高亮"
            If tip.IsHighLight Then
                title2 = $"{tip.Color.StyledMarkupName}高亮"
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
                    body &= $"<br/>•  {g.Item1.StyledMarkupName}：{g.Item2} 项"
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
        Return _metaLabel.Top + _metaLabel.Height + _bottom
    End Function

#End Region
End Class
