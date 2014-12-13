Imports System.Reflection

Public NotInheritable Class AboutBox
    Private Sub AboutBox_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Randomize()
        _logo = New Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("Memory.about.png"))
        AnimationTimer.Enabled = True
        AboutLabel.Text = String.Format("Raad je plaatje - Demo-applicatie voor het vak Informaticawerktuigen. {0} Versie {1}. Extra punten voor de eerste die het easter egg vindt.", My.Application.Info.Copyright, My.Application.Info.Version)
    End Sub

    Private Sub AboutBox_Paint(sender As System.Object, e As System.Windows.Forms.PaintEventArgs) Handles MyBase.Paint
        Using blackPen As New Pen(Color.Black)
            e.Graphics.DrawRectangle(blackPen, 0, 0, Me.ClientSize.Width - 1, Me.ClientSize.Height - 1)
            e.Graphics.DrawRectangle(blackPen, 1, 1, Me.ClientSize.Width - 3, Me.ClientSize.Height - 3)
        End Using
        e.Graphics.DrawImage(_logo, New Rectangle(10, 60, _logo.Width, _logo.Height \ 3), New Rectangle(0, _state * (_logo.Height \ 3), _logo.Width, _logo.Height \ 3), GraphicsUnit.Pixel)
    End Sub

    Private Sub OkButton_Click(sender As System.Object, e As System.EventArgs) Handles OkButton.Click
        Me.Close()
    End Sub

    Private Sub AnimationTimer_Tick(sender As System.Object, e As System.EventArgs) Handles AnimationTimer.Tick
        If _state = 1 Or _state = 2 Then ' heeft de koe zijn ogen gesloten, of staat er 'meuh!'?
            _state = 0
            AnimationTimer.Interval = 1000
        Else
            Dim newState As Integer = CInt(Int((8 * Rnd()))) ' genereer een getal uit [0 .. 9]
            ' twee kansen op tien dat de koe knipoogt; ze moet ook minstens twee keer geknipperd
            ' hebben alvorens ze terug 'meuh!' kan zeggen
            If newState = 0 Or newState = 1 Or (newState = 2 And _blink < 2) Then
                _state = 1
                AnimationTimer.Interval = 200 ' het knipperen van de ogen duurt geen seconde
                _blink += 1
            ElseIf newState = 2 And _blink > 2 Then ' één kans op tien dat de koe 'meuh!' zegt (indien ze genoeg geknipperd heeft)
                _state = 2
                _blink = 0
                AnimationTimer.Interval = 2000 ' de koe zegt twee seconden 'meuh!'
            End If
        End If
        ' de koe hertekenen
        Me.Invalidate()
    End Sub

    Private _state As Integer
    Private _logo As Image
    Private _blink As Integer
End Class
