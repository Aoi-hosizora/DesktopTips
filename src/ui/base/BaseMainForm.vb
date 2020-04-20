Imports DD = DevComponents.DotNetBar

''' <summary>
''' 实现了 透明度动画 窗口拖动
''' </summary>
Public Class BaseMainForm
    Inherits Form

    Private WithEvents TimerShowForm As Timer = New Timer() With {.Interval = 1, .Enabled = False}
    Private WithEvents TimerHideForm As Timer = New Timer() With {.Interval = 1, .Enabled = False}
    Private WithEvents TimerMouseIn As Timer = New Timer() With {.Interval = 10, .Enabled = False}
    Private WithEvents TimerMouseOut As Timer = New Timer() With {.Interval = 10, .Enabled = False}

    Protected MaxOpacity As Double = 0.6
    Protected OpacitySpeed As Double = 0.08

    Protected Overrides Sub OnLoad(e As System.EventArgs)
        MyBase.OnLoad(e)
        Me.Opacity = 0
        OnShowForm()

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
        Next
    End Sub

    Protected Overrides Sub OnClosing(e As System.ComponentModel.CancelEventArgs)
        OnHideForm()
        MyBase.OnClosing(e)
    End Sub

#Region "Timer"

    Private Sub TimerShowForm_Tick(sender As System.Object, e As System.EventArgs) Handles TimerShowForm.Tick
        Me.Opacity += OpacitySpeed
        Me.Top += 1
        If Me.Opacity >= MaxOpacity Then
            Me.Opacity = MaxOpacity
            TimerShowForm.Enabled = False
        End If
    End Sub

    Private Sub TimerHideForm_Tick(sender As System.Object, e As System.EventArgs) Handles TimerHideForm.Tick
        Me.Opacity -= OpacitySpeed
        Me.Top -= 1
        If Me.Opacity <= 0 Then
            Me.Close()
            TimerHideForm.Enabled = False
        End If
    End Sub

    Private Sub TimerMouseIn_Tick(sender As System.Object, e As System.EventArgs) Handles TimerMouseIn.Tick
        Me.Opacity += 0.05
        If Me.Opacity >= 1 Then
            Me.Opacity = 1
            TimerMouseIn.Enabled = False
        End If
    End Sub

    Private Sub TimerMouseOut_Tick(sender As System.Object, e As System.EventArgs) Handles TimerMouseOut.Tick
        Me.Opacity -= 0.02
        If Me.Opacity <= MaxOpacity Then
            Me.Opacity = MaxOpacity
            TimerMouseOut.Enabled = False
        End If
    End Sub

#End Region

#Region "Opecity"

    Private Sub FormMouseMove(sender As Object, e As System.EventArgs)
        If Cursor.Position.X >= Me.Left And Cursor.Position.X <= Me.Right And Cursor.Position.Y >= Me.Top And Cursor.Position.Y <= Me.Bottom Then
            OnOpecityUp()
        End If
    End Sub

    Private Sub FormMouseLeave(sender As Object, e As System.EventArgs)
        OnOpecityDown()
    End Sub

    Protected Sub OnShowForm()
        TimerMouseIn.Enabled = False
        TimerMouseOut.Enabled = False
        TimerHideForm.Enabled = False
        TimerShowForm.Enabled = True
    End Sub

    Protected Sub OnHideForm()
        TimerMouseIn.Enabled = False
        TimerMouseOut.Enabled = False
        TimerShowForm.Enabled = False
        TimerHideForm.Enabled = True
    End Sub

    Protected Sub OnOpecityUp()
        TimerHideForm.Enabled = False
        TimerShowForm.Enabled = False
        TimerMouseOut.Enabled = False
        TimerMouseIn.Enabled = True
    End Sub

    Protected Sub OnOpecityDown()
        TimerHideForm.Enabled = False
        TimerShowForm.Enabled = False
        TimerMouseIn.Enabled = False
        TimerMouseOut.Enabled = True
    End Sub

#End Region

#Region "Move"

    Protected isMouseDown As Boolean
    Protected pushDownMouseInScreen As Point
    Protected pushDownWindowPos As Point
    Protected pushDownWindowSize As Point

    Private Sub FormMouseDown(sender As Object, e As MouseEventArgs)
        isMouseDown = e.Button = MouseButtons.Left
        pushDownMouseInScreen = Cursor.Position
        pushDownWindowPos = New Point(Me.Left, Me.Top)
        pushDownWindowSize = New Point(Me.Width, Me.Height)
    End Sub

    Private Sub FormMouseUp(sender As Object, e As MouseEventArgs)
        isMouseDown = False
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub FormMouseResize(sender As Object, e As MouseEventArgs)
        If isMouseDown Then
            Me.Cursor = Cursors.SizeAll
            Me.Top = pushDownWindowPos.Y + Cursor.Position.Y - pushDownMouseInScreen.Y
            Me.Left = pushDownWindowPos.X + Cursor.Position.X - pushDownMouseInScreen.X
        End If
    End Sub

#End Region

End Class
