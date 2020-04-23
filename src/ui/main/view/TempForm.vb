Public Class TempForm

    Private Sub TempForm_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        TipListBox1.Items.Add(New TipItem("a", True, New ColorPair(Color.Red)))
        TipListBox1.Items.Add(New TipItem("b", True, New ColorPair(Color.Blue)))
        TipListBox1.Items.Add(New TipItem("c", True, New ColorPair(Color.Yellow)))
        TipListBox1.Items.Add(New TipItem("d"))
    End Sub

    Private Sub TipListBox1_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles TipListBox1.SelectedIndexChanged

    End Sub
End Class
