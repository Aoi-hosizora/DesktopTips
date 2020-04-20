Public Class EscCloseForm
    Inherits Form

    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, keyData As System.Windows.Forms.Keys) As Boolean
        Dim WM_KEYDOWN As Integer = 256
        Dim WM_SYSKEYDOWN As Integer = 260
        If msg.Msg = WM_KEYDOWN OrElse msg.Msg = WM_SYSKEYDOWN Then
            If keyData = Keys.Escape Then
                Me.Close()
            End If
        End If
        Return False
    End Function
End Class
