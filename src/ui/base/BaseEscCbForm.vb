''' <summary>
''' 实现 Esc 按键回调
''' </summary>
Public Class BaseEscCbForm
    Inherits Form

    ''' <summary>
    ''' Esc 回调函数
    ''' </summary>
    ''' <returns>是否退出窗口</returns>
    Public Function EscCallback() As Boolean
        Return True
    End Function

    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
        If msg.Msg = NativeMethod.WM_KEYDOWN OrElse msg.Msg = NativeMethod.WM_SYSKEYDOWN Then
            Select Case keyData
                Case Keys.Escape : If EscCallback() Then Close()
            End Select
        End If
        Return False
    End Function
End Class
