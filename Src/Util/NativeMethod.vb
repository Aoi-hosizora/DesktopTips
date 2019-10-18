Public Class NativeMethod

#Region "光标信息与移动"

    Public Declare Sub mouse_event Lib "user32" _
        (ByVal dwFlags As Integer, ByVal dx As Integer, ByVal dy As Integer, ByVal cButtons As Integer, ByVal dwExtraInfo As Integer)

    Public Declare Function SetCursorPos Lib "user32" _
        (ByVal x As Long, ByVal y As Long) As Long

    Public Enum MouseEvent As UInteger
        MOUSEEVENTF_LEFTDOWN = &H2      '左键按下
        MOUSEEVENTF_LEFTUP = &H4        '左键释放
        MOUSEEVENTF_MIDDLEDOWN = &H20   '中键按下
        MOUSEEVENTF_MIDDLEUP = &H40     '中键释放
        MOUSEEVENTF_RIGHTDOWN = &H8     '右键按下
        MOUSEEVENTF_RIGHTUP = &H10      '右键释放
        MOUSEEVENTF_MOVE = &H1          '指针移动
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

    Public Declare Auto Function RegisterHotKey Lib "user32.dll" Alias "RegisterHotKey" _
        (ByVal hwnd As IntPtr, ByVal id As Integer, ByVal fsModifiers As Integer, ByVal vk As Integer) As Boolean

    Public Declare Auto Function UnRegisterHotKey Lib "user32.dll" Alias "UnregisterHotKey" _
        (ByVal hwnd As IntPtr, ByVal id As Integer) As Boolean

#End Region

End Class
