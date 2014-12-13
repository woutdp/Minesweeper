Public Class frmHalloWereld
    Private Sub btnEngels_Click(sender As Object, e As EventArgs) Handles btnEngels.MouseEnter
        lblOutput.Text = "Hello World"
    End Sub

    Private Sub btnNederlands_Click(sender As Object, e As EventArgs) Handles btnNederlands.MouseEnter
        lblOutput.Text = "Hallo Wereld"
    End Sub

    Private Sub btnStop_Click(sender As Object, e As EventArgs) Handles btnStop.Click
        Application.Exit()
    End Sub
End Class
