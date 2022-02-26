Public Class ColorStyleDialog

    Private Sub ButtonOK_Click(sender As Object, e As EventArgs) Handles ButtonOK.Click
        DialogResult = DialogResult.OK
        Close()
    End Sub

    Private Sub Button_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

    Private Sub RadioButton_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonRegular.CheckedChanged, RadioButtonCustom.CheckedChanged
        CheckBoxBold.Enabled = RadioButtonCustom.Checked
        CheckBoxItalic.Enabled = RadioButtonCustom.Checked
        CheckBoxUnderline.Enabled = RadioButtonCustom.Checked
        If Not RadioButtonCustom.Checked Then
            CheckBoxBold.Checked = False
            CheckBoxItalic.Checked = False
            CheckBoxUnderline.Checked = False
        End If
    End Sub

    Private Sub ColorStyleDialog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LabelSample.Font = New Font(Font, GetFontStyle())
    End Sub

    Private Sub Style_Changed(sender As Object, e As EventArgs) Handles RadioButtonRegular.CheckedChanged, RadioButtonCustom.CheckedChanged, CheckBoxBold.CheckedChanged, CheckBoxItalic.CheckedChanged, CheckBoxUnderline.CheckedChanged
        LabelSample.Font = New Font(Font, GetFontStyle())
    End Sub

    Public Overloads Shared Function ShowDialog(style As FontStyle) As FontStyle
        Dim dlg As New ColorStyleDialog
        With dlg
            If style = FontStyle.Regular Then
                .RadioButtonRegular.Checked = True
            Else
                .RadioButtonCustom.Checked = True
                .CheckBoxBold.Checked = (style And FontStyle.Bold) > 0
                .CheckBoxItalic.Checked = (style And FontStyle.Italic) > 0
                .CheckBoxUnderline.Checked = (style And FontStyle.Underline) > 0
            End If
            If .ShowDialog() = vbCancel Then Return style
            Return .GetFontStyle()
        End With
    End Function

    Public Function GetFontStyle() As FontStyle
        If RadioButtonRegular.Checked = True Then
            Return FontStyle.Regular
        Else
            Dim r As FontStyle
            If CheckBoxBold.Checked = True Then
                r = r Or FontStyle.Bold
            End If
            If CheckBoxItalic.Checked = True Then
                r = r Or FontStyle.Italic
            End If
            If CheckBoxUnderline.Checked = True Then
                r = r Or FontStyle.Underline
            End If
            Return r
        End If
    End Function
End Class
