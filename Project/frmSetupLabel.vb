Public Class frmSetupLabel

    Private Sub frmSetupLabel_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub btnChangeColor_Click(sender As Object, e As EventArgs) Handles btnChangeColor.Click
        If ColorDialog1.ShowDialog <> Windows.Forms.DialogResult.Cancel Then
            Label1.ForeColor = ColorDialog1.Color
        End If
    End Sub

    Private Sub btnChangeFont_Click(sender As Object, e As EventArgs) Handles btnChangeFont.Click
        If FontDialog1.ShowDialog <> Windows.Forms.DialogResult.Cancel Then
            Label1.Font = FontDialog1.Font
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label1.Text = TimeOfDay
    End Sub

    Private Sub btnChangeDone_Click(sender As Object, e As EventArgs) Handles btnChangeDone.Click
        FrmDigitalBB.Label.ForeColor = Label1.ForeColor
        FrmDigitalBB.Label.Font = Label1.Font
        Dispose()
    End Sub
End Class