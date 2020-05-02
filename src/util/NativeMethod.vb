﻿Imports System.Runtime.InteropServices
Imports System.Text

Public Class NativeMethod

#Region "光标信息与移动"

    <DllImport("user32.dll")>
    Public Shared Sub mouse_event(dwFlags As MouseEvent, dx As UInteger, dy As UInteger, dwData As UInteger, dwExtraInfo As Integer)
    End Sub

    Public Enum MouseEvent As UInteger
        MOUSEEVENTF_LEFTDOWN = &H2      '左键按下
        MOUSEEVENTF_LEFTUP = &H4        '左键释放
        MOUSEEVENTF_MIDDLEDOWN = &H20   '中键按下
        MOUSEEVENTF_MIDDLEUP = &H40     '中键释放
        MOUSEEVENTF_RIGHTDOWN = &H8     '右键按下
        MOUSEEVENTF_RIGHTUP = &H10      '右键释放
        MOUSEEVENTF_MOVE = &H1          '指针移动
        MOUSEEVENTF_ABSOLUTE = &H8000   '绝对移动
    End Enum

    ''' <summary>
    ''' 鼠标上移
    ''' </summary>
    Public Shared Sub MouseMoveUp(currPos As Point, px As Integer)
        Dim dx As Integer = currPos.X * UInt16.MaxValue / My.Computer.Screen.Bounds.Width
        Dim dy As Integer = (currPos.Y - px) * UInt16.MaxValue / My.Computer.Screen.Bounds.Height
        mouse_event(MouseEvent.MOUSEEVENTF_MOVE Or MouseEvent.MOUSEEVENTF_ABSOLUTE, dx, dy, 0, 0)
    End Sub

    ''' <summary>
    ''' 鼠标下移
    ''' </summary>
    Public Shared Sub MouseMoveDown(currPos As Point, px As Integer)
        Dim dx As Integer = currPos.X * UInt16.MaxValue / My.Computer.Screen.Bounds.Width
        Dim dy As Integer = (currPos.Y + px) * UInt16.MaxValue / My.Computer.Screen.Bounds.Height
        mouse_event(MouseEvent.MOUSEEVENTF_MOVE Or MouseEvent.MOUSEEVENTF_ABSOLUTE, dx, dy, 0, 0)
    End Sub

#End Region

#Region "全局快捷键"

    Public Const GWL_WNDPROC = (- 4)
    Public Const WM_HOTKEY = &H312

    Public Enum KeyModifiers As UInteger
        MOD_ALT = &H1
        MOD_CONTROL = &H2
        MOD_SHIFT = &H4
        MOD_WINDOWS = &H8
    End Enum

    <DllImport("user32.dll")>
    Public Shared Function RegisterHotKey(hwnd As IntPtr, id As Integer, fsModifiers As Integer, vk As Integer) As Boolean
    End Function

    <DllImport("user32.dll")>
    Public Shared Function UnregisterHotKey(hwnd As IntPtr, id As Integer) As Boolean
    End Function

#End Region

#Region "窗口相关"

    Public Const GW_CHILD As Integer = 5
    Public Const GW_HWNDNEXT As Integer = 2
    Public Const WS_EX_APPWINDOW = 16384
    Public Const WS_EX_TOOLWINDOW = 128
    Public Const WM_KEYDOWN = 256
    Public Const WM_SYSKEYDOWN = 260

    <DllImport("user32.dll")>
    Public Shared Function SetForegroundWindow(hWnd As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function

    <DllImport("user32.dll")>
    Public Shared Function GetWindow(hWnd As IntPtr, uCmd As UInt32) As IntPtr
    End Function

    <DllImport("user32.dll")>
    Public Shared Function GetClassName(hWnd As IntPtr, pClassName As StringBuilder, nMaxCount As Integer) As Integer
    End Function

    <DllImport("user32.dll")>
    Public Shared Function SetWindowPos(hWnd As IntPtr, hWndInsertAfter As IntPtr, X As Integer, Y As Integer, cx As Integer, cy As Integer, uFlags As Integer) As Boolean
    End Function

    <DllImport("user32.dll")>
    Public Shared Function SetWindowText(hwnd As IntPtr, lpString As String) As Boolean
    End Function

    Public Shared Function GetWindowClassName(handle As IntPtr) As String
        Dim sb As New StringBuilder(256)
        GetClassName(handle, sb, sb.Capacity)
        Return sb.ToString()
    End Function

#End Region

#Region "DWM"

    Public Structure MARGINS
        Public leftWidth As Integer
        Public rightWidth As Integer
        Public topHeight As Integer
        Public bottomHeight As Integer
    End Structure

    Public Const CS_DROPSHADOW As Integer = &H20000
    Public Const WM_NCPAINT As Integer = &H85

    <DllImport("dwmapi.dll")>
    Public Shared Function DwmExtendFrameIntoClientArea(hWnd As IntPtr, ByRef pMarInset As MARGINS) As Integer
    End Function

    <DllImport("dwmapi.dll")>
    Public Shared Function DwmSetWindowAttribute(hwnd As IntPtr, ByVal attr As Integer, ByRef attrValue As Integer, attrSize As Integer) As Integer
    End Function

    <DllImport("dwmapi.dll")>
    Public Shared Function DwmIsCompositionEnabled(ByRef pfEnabled As Integer) As Integer
    End Function

    Public Shared Function CheckAeroEnabled() As Boolean
        Dim ok = 0
        DwmIsCompositionEnabled(ok)
        Return ok = 1
    End Function

#End Region
End Class
