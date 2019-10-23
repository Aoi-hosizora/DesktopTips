Imports QRCoder

Public Class GenQrCode

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

    Public Shared Function GetQrCodeForm(ByVal Data As String) As EscCloseForm

        Dim qrGenerator As New QRCodeGenerator
        Dim qrCodeData As QRCodeData = qrGenerator.CreateQrCode(Data, QRCodeGenerator.ECCLevel.Q)
        Dim qrCode As New QRCode(qrCodeData)
        Dim qrCodeImg As Bitmap = qrCode.GetGraphic(7)

        Dim qrCodeForm As New EscCloseForm With {.Name = "qrCodeForm", .FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog, _
                                                 .MaximizeBox = False, .MinimizeBox = False, .ShowInTaskbar = False, _
                                                 .StartPosition = FormStartPosition.CenterScreen, .Size = qrCodeImg.Size}

        Dim pictureBox As New PictureBox With {.Name = "pictureBox", .SizeMode = PictureBoxSizeMode.Zoom, .Image = qrCodeImg}

        qrCodeForm.Controls.Add(pictureBox)
        pictureBox.Dock = DockStyle.Fill

        Return qrCodeForm
    End Function

End Class
