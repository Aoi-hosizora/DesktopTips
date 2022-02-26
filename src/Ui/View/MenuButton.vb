Imports System.Drawing.Drawing2D

''' <summary>
''' 菜单按钮
''' </summary>
Public Class MenuButton
    Inherits Button

    ''' <summary>
    ''' 弹出菜单
    ''' </summary>
    Public Property Menu As ContextMenuStrip

    Private _text As String
    Private ReadOnly _splitWidth = 20

    Public Overrides Property Text As String
        Get
            Return _text
        End Get
        Set
            If value <> _text Then
                _text = value
                Invalidate()
            End If
        End Set
    End Property

    Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
        Dim splitRect As New Rectangle(Width - _splitWidth, 0, _splitWidth, Height)
        If Menu IsNot Nothing AndAlso e.Button = MouseButtons.Left AndAlso splitRect.Contains(e.Location) Then
            Menu.Show(Me, New Point(0, Height))
        Else
            MyBase.OnMouseDown(e)
        End If
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        Dim motoText = Text
        _text &= "　"
        MyBase.OnPaint(e)
        _text = motoText

        If Menu IsNot Nothing Then
            Dim arrowX = ClientRectangle.Width - 14
            Dim arrowY = ClientRectangle.Height \ 2 - 1
            Dim brush = If(Enabled, SystemBrushes.ControlText, SystemBrushes.ControlDark)
            e.Graphics.FillPolygon(brush, {New Point(arrowX, arrowY), New Point(arrowX + 7, arrowY), New Point(arrowX + 3, arrowY + 4)})
            
            Dim lineX = ClientRectangle.Width - _splitWidth
            Dim lineYFrom = arrowY - 5
            Dim lineYTo = arrowY + 8
            Using linePen As New Pen(Brushes.DarkGray) With { .DashStyle = DashStyle.Solid }
                e.Graphics.DrawLine(linePen, lineX, lineYFrom, lineX, lineYTo)
            End Using
        End If
    End Sub
End Class
