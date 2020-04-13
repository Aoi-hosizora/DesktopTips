Imports System.Runtime.InteropServices
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

#End Region

#Region "全局快捷键"

    Public Const GWL_WNDPROC = (-4)
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

    <DllImport("user32.dll")>
    Public Shared Function SetForegroundWindow(ByVal hWnd As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
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

End Class
