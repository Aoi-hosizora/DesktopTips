Public Class BaseEscCloseForm
    Inherits Form
    
    Public Function EscCallback() As Boolean
        Return True
    End Function

    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
        If msg.Msg = NativeMethod.WM_KEYDOWN OrElse msg.Msg = NativeMethod.WM_SYSKEYDOWN Then
            If keyData = Keys.Escape Then
                If EscCallback() Then
                    Me.Close()
                End If
            End If
        End If
        Return False
    End Function
End Class
