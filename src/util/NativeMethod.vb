Imports System.Runtime.InteropServices
Imports System.Text

Public Class NativeMethod

#Region "常量"

    Public Const GW_CHILD = 5
    Public Const GW_HWNDNEXT = 2

    Public Const WS_EX_APPWINDOW = &H40000
    Public Const WS_EX_TOOLWINDOW = &H80
    Public Const WS_EX_TOPMOST = &H8
    Public Const WS_EX_LAYERED = &H80000

    Public Const WM_HOTKEY = &H312
    Public Const WM_KEYDOWN = &H100
    Public Const WM_SYSKEYDOWN = &H104
    Public Const WM_NCPAINT = &H85
    Public Const WM_NCMOUSEMOVE = &HA0
    Public Const WM_NCMOUSELEAVE = &H2A2
    Public Const WM_NCLBUTTONDOWN = &HA1
    Public Const WM_NCRBUTTONDOWN = &HA4
    Public Const WM_NCMBUTTONDOWN = &HA7
    Public Const WM_NCLBUTTONUP = &HA2
    Public Const WM_NCRBUTTONUP = &HA5
    Public Const WM_NCMBUTTONUP = &HA8

    Public Const CS_DROPSHADOW = &H20000

    ' https://www.neowin.net/forum/topic/794078-cc-keep-window-visible-while-using-aero-peek/
    Public Const DWMWA_NCRENDERING_POLICY = 2
    Public Const DWMWA_EXCLUDED_FROM_PEEK = 12
    Public Const DWMNC_ENABLED = 2

    Public Const LWA_ALPHA = &H2
    Public Const LWA_COLORKEY = &H1

    Public Const BS_COMMANDLINK As Integer = &HE
    Public Const BCM_SETNOTE As Integer = &H1609

#End Region

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

    <DllImport("user32.dll")>
    Public Shared Function SetForegroundWindow(hWnd As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function

    <DllImport("user32.dll")>
    Public Shared Function GetWindow(hWnd As IntPtr, uCmd As UInt32) As IntPtr
    End Function

    <DllImport("user32.dll")>
    Public Shared Function GetClassName(hWnd As IntPtr, pClassName As StringBuilder, nMaxCount As Integer) As Integer
    End Function

    Public Shared Function GetWindowClassName(handle As IntPtr) As String
        Dim sb As New StringBuilder(256)
        GetClassName(handle, sb, sb.Capacity)
        Return sb.ToString()
    End Function

    <DllImport("user32.dll")>
    Public Shared Function SetWindowPos(hWnd As IntPtr, hWndInsertAfter As IntPtr, X As Integer, Y As Integer, cx As Integer, cy As Integer, uFlags As Integer) As Boolean
    End Function

    <DllImport("user32.dll")>
    Public Shared Function SetWindowText(hwnd As IntPtr, lpString As String) As Boolean
    End Function

    <DllImport("user32.dll")>
    Public Shared Function SetLayeredWindowAttributes(hwnd As IntPtr, crKey As COLORREF, bAlpha As Byte, dwFlags As UInteger) As Boolean
    End Function

    <StructLayout(LayoutKind.Sequential)>
    Public Structure COLORREF
        Public R As Byte
        Public G As Byte
        Public B As Byte

        Public Sub New(color As Color)
            R = color.R
            G = color.G
            B = color.B
        End Sub
    End Structure

    <DllImport("user32.dll")>
    Public Shared Function SendMessage(hWnd As IntPtr, msg As Integer, wParam As IntPtr, <MarshalAs(UnmanagedType.LPWStr)> lParam As String) As IntPtr
    End Function

#End Region

#Region "DWM"

    Public Structure MARGINS
        Public LeftWidth As Integer
        Public RightWidth As Integer
        Public TopHeight As Integer
        Public BottomHeight As Integer
    End Structure

    <DllImport("dwmapi.dll")>
    Public Shared Function DwmExtendFrameIntoClientArea(hWnd As IntPtr, ByRef pMarInset As MARGINS) As Integer
    End Function

    <DllImport("dwmapi.dll")>
    Public Shared Function DwmSetWindowAttribute(hwnd As IntPtr, attr As Integer, ByRef attrValue As Integer, attrSize As Integer) As Integer
    End Function

    <DllImport("dwmapi.dll")>
    Public Shared Function DwmIsCompositionEnabled(ByRef pfEnabled As Integer) As Integer
    End Function

    Public Shared Function CheckAeroEnabled() As Boolean
        If Environment.OSVersion.Version.Major < 6 Then
            Return False
        End If
        Dim ok = 0
        DwmIsCompositionEnabled(ok)
        Return ok = 1
    End Function

#End Region
End Class
