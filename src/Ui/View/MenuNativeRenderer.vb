Imports System
Imports System.Diagnostics.CodeAnalysis
Imports System.Drawing
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports System.Windows.Forms.VisualStyles

Public Class MenuNativeRenderer
    Inherits ToolStripSystemRenderer

    Private _renderer As VisualStyleRenderer
    Private Const MenuClass = "Menu"

#Region "Helpers"

    Private Function EnsureRenderer() As Boolean
        If Not VisualStyleRenderer.IsSupported Then
            Return False
        End If
        If Not VisualStyleRenderer.IsElementDefined(VisualStyleElement.CreateElement(MenuClass, MenuParts.BarBackground, MenuBarStates.Active)) Then
            Return False
        End If
        If _renderer Is Nothing Then
            _renderer = New VisualStyleRenderer(VisualStyleElement.Button.PushButton.Normal)
        End If
        Return True
    End Function

    Private Function GetItemState(item As ToolStripItem) As Integer
        Dim hot = item.Selected
        If (item.IsOnDropDown) Then
            If (item.Enabled) Then
                Return If(hot, MenuPopupItemStates.Hover, MenuPopupItemStates.Normal)
            End If
            Return If(hot, MenuPopupItemStates.DisabledHover, MenuPopupItemStates.Disabled)
        End If
        If (item.Pressed) Then
            Return If(item.Enabled, MenuBarItemStates.Pushed, MenuBarItemStates.DisabledPushed)
        End If
        If (item.Enabled) Then
            Return If(hot, MenuBarItemStates.Hover, MenuBarItemStates.Normal)
        End If
        Return If(hot, MenuBarItemStates.DisabledHover, MenuBarItemStates.Disabled)
    End Function

    Private Function GetBackgroundRectangle(item As ToolStripItem) As Rectangle
        If Not item.IsOnDropDown Then
            Return New Rectangle(New Point(), item.Bounds.Size)
        End If
        Dim rect = item.Bounds
        rect.X = item.ContentRectangle.X + 1
        rect.Width = item.ContentRectangle.Width - 1
        rect.Y = 0
        Return rect
    End Function

    Private Function GetThemeMargins(dc As IDeviceContext, marginType As MarginTypes) As Padding
        Try
            Dim margins As NativeMethods.MARGINS
            Dim ok = NativeMethods.GetThemeMargins(_renderer.Handle, dc.GetHdc(), _renderer.Part, _renderer.State, marginType, IntPtr.Zero, margins) = 0
            Return If(Not ok, New Padding(0), New Padding(margins.cxLeftWidth, margins.cyTopHeight, margins.cxRightWidth, margins.cyBottomHeight))
        Finally
            dc.ReleaseHdc()
        End Try
    End Function

#End Region

#Region "Overrides"

    Protected Overrides Sub Initialize(toolStrip As ToolStrip)
        If TypeOf toolStrip.Parent Is ToolStripPanel Then
            toolStrip.BackColor = Color.Transparent
        End If
        MyBase.Initialize(toolStrip)
    End Sub

    Protected Overrides Sub InitializePanel(toolStripPanel As ToolStripPanel)
        For Each control As Control In toolStripPanel.Controls
            If (TypeOf control Is ToolStrip) Then
                Initialize(CType(control, ToolStrip))
            End If
        Next
        MyBase.InitializePanel(toolStripPanel)
    End Sub

    Protected Overrides Sub OnRenderItemText(e As ToolStripItemTextRenderEventArgs)
        If EnsureRenderer() Then
            Dim partId = If(e.Item.IsOnDropDown, MenuParts.PopupItem, MenuParts.BarItem)
            _renderer.SetParameters(MenuClass, partId, GetItemState(e.Item))
            e.TextColor = _renderer.GetColor(ColorProperty.TextColor)
        End If
        MyBase.OnRenderItemText(e)
    End Sub

    Protected Overrides Sub OnRenderArrow(e As ToolStripArrowRenderEventArgs)
        If EnsureRenderer() Then
            Dim partId = If(e.Item.IsOnDropDown, MenuParts.PopupItem, MenuParts.BarItem)
            _renderer.SetParameters(MenuClass, partId, GetItemState(e.Item))
            e.ArrowColor = _renderer.GetColor(ColorProperty.TextColor)
        End If
        MyBase.OnRenderArrow(e)
    End Sub

    Protected Overrides Sub OnRenderSeparator(e As ToolStripSeparatorRenderEventArgs)
        If Not EnsureRenderer() OrElse Not e.ToolStrip.IsDropDown Then
            MyBase.OnRenderSeparator(e)
            Return
        End If
        _renderer.SetParameters(MenuClass, MenuParts.PopupSeparator, 0)
        Dim rect = New Rectangle(e.ToolStrip.DisplayRectangle.Left, 0, e.ToolStrip.DisplayRectangle.Width, e.Item.Height)
        _renderer.DrawBackground(e.Graphics, rect, rect)
    End Sub

    Protected Overrides Sub OnRenderMenuItemBackground(e As ToolStripItemRenderEventArgs)
        If Not EnsureRenderer() Then
            MyBase.OnRenderMenuItemBackground(e)
            Return
        End If
        Dim partId = If(e.Item.IsOnDropDown, MenuParts.PopupItem, MenuParts.BarItem)
        _renderer.SetParameters(MenuClass, partId, GetItemState(e.Item))
        Dim bgRect = GetBackgroundRectangle(e.Item)
        _renderer.DrawBackground(e.Graphics, bgRect, bgRect)
    End Sub

    Protected Overrides Sub OnRenderImageMargin(e As ToolStripRenderEventArgs)
        If Not EnsureRenderer() Then
            MyBase.OnRenderImageMargin(e)
            Return
        End If
        If Not e.ToolStrip.IsDropDown Then
            Return
        End If
        _renderer.SetParameters(MenuClass, MenuParts.PopupGutter, 0)
        Dim margins = GetThemeMargins(e.Graphics, MarginTypes.Sizing)
        Dim extraWidth = e.ToolStrip.Width - e.ToolStrip.DisplayRectangle.Width - margins.Left - margins.Right - 1 - e.AffectedBounds.Width
        Dim rect = e.AffectedBounds
        rect.Y += 2
        rect.Height -= 4
        Dim sepWidth = _renderer.GetPartSize(e.Graphics, ThemeSizeType.True).Width
        If e.ToolStrip.RightToLeft = RightToLeft.Yes Then
            rect = New Rectangle(rect.X - extraWidth, rect.Y, sepWidth, rect.Height)
            rect.X += sepWidth
        Else
            rect = New Rectangle(rect.Width + extraWidth - sepWidth, rect.Y, sepWidth, rect.Height)
        End If
        _renderer.DrawBackground(e.Graphics, rect)
    End Sub

    Protected Overrides Sub OnRenderItemCheck(e As ToolStripItemImageRenderEventArgs)
        If Not EnsureRenderer() Then
            MyBase.OnRenderItemCheck(e)
            Return
        End If
        Dim bgRect = GetBackgroundRectangle(e.Item)
        bgRect.Width = bgRect.Height
        If e.Item.RightToLeft = RightToLeft.Yes Then
            bgRect = New Rectangle(e.ToolStrip.ClientSize.Width - bgRect.X - bgRect.Width, bgRect.Y, bgRect.Width, bgRect.Height)
        End If
        _renderer.SetParameters(MenuClass, MenuParts.PopupCheckBackground, If(e.Item.Enabled, MenuPopupCheckBackgroundStates.Normal, MenuPopupCheckBackgroundStates.Disabled))
        _renderer.DrawBackground(e.Graphics, bgRect)
        Dim checkRect = e.ImageRectangle
        checkRect.X = bgRect.X + bgRect.Width / 2 - checkRect.Width / 2
        checkRect.Y = bgRect.Y + bgRect.Height / 2 - checkRect.Height / 2
        _renderer.SetParameters(MenuClass, MenuParts.PopupCheck, If(e.Item.Enabled, MenuPopupCheckStates.CheckmarkNormal, MenuPopupCheckStates.CheckmarkDisabled))
        _renderer.DrawBackground(e.Graphics, checkRect)
    End Sub

#End Region

#Region "Enums"

    Private Enum MenuParts
        BarBackground = 7
        BarItem = 8
        PopupBackground = 9
        PopupBorders = 10
        PopupCheck = 11
        PopupCheckBackground = 12
        PopupGutter = 13
        PopupItem = 14
        PopupSeparator = 15
        PopupSubmenu = 16
    End Enum

    Private Enum MenuBarStates
        Active = 1
        Inactive = 2
    End Enum

    Private Enum MenuBarItemStates
        Normal = 1
        Hover = 2
        Pushed = 3
        Disabled = 4
        DisabledHover = 5
        DisabledPushed = 6
    End Enum

    Private Enum MenuPopupItemStates
        Normal = 1
        Hover = 2
        Disabled = 3
        DisabledHover = 4
    End Enum

    Private Enum MenuPopupCheckStates
        CheckmarkNormal = 1
        CheckmarkDisabled = 2
        BulletNormal = 3
        BulletDisabled = 4
    End Enum

    Private Enum MenuPopupCheckBackgroundStates
        Disabled = 1
        Normal = 2
        Bitmap = 3
    End Enum

    Private Enum MarginTypes
        Sizing = 3601
        Content = 3602
        Caption = 3603
    End Enum

#End Region

    Private Class NativeMethods
        <SuppressMessage("ReSharper", "InconsistentNaming")>
        <SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Global")>
        <StructLayout(LayoutKind.Sequential)>
        Public Structure MARGINS
            Public cxLeftWidth As Integer
            Public cxRightWidth As Integer
            Public cyTopHeight As Integer
            Public cyBottomHeight As Integer
        End Structure

        <DllImport("uxtheme.dll")>
        Public Shared Function GetThemeMargins(hTheme As IntPtr, hdc As IntPtr, iPartId As Integer, iStateId As Integer, iPropId As Integer, rect As IntPtr, ByRef pMargins As MARGINS) As Integer
        End Function

    End Class

End Class
