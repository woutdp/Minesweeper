Option Explicit On
Option Strict On

Public Class Form1
    Dim grid(3, 3) As Label

    Dim score As Label
    Dim highScore As Label
    Dim scoreAmount As Integer = 0
    Dim highScoreAmount As Integer = 0

    Dim WithEvents restart As Button
    Dim WithEvents joker As Button
    Dim jokersAvailable As Integer = 1
    Dim jokerActive As Boolean = False
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Initialize Grid
        Dim tileSize As Integer = 80 'Size of the tiles in x and y
        Dim space As Integer = 7 ' Space in between tiles
        Dim spaceXBorder As Integer = 30 'How much from left border
        Dim spaceYBorder As Integer = 30 'How much from top border

        For i As Integer = grid.GetLowerBound(0) To grid.GetUpperBound(0)
            For j As Integer = grid.GetLowerBound(1) To grid.GetUpperBound(1)
                grid(i, j) = New Label
                grid(i, j).Size = New Size(tileSize, tileSize)
                grid(i, j).TextAlign = ContentAlignment.MiddleCenter
                grid(i, j).Font = New Font("Calibri", 20, FontStyle.Bold)
                grid(i, j).ForeColor = ColorTranslator.FromOle(RGB(119, 110, 101))
                grid(i, j).Name = "lblTile_" & i & "_" & j
                grid(i, j).Location = New Point(i * (tileSize + space) + spaceXBorder,
                                                j * (tileSize + space) + spaceYBorder)
                grid(i, j).BackColor = ColorTranslator.FromOle(RGB(205, 193, 180))
                grid(i, j).Text = ""

                AddHandler grid(i, j).Click, AddressOf lblTile_Click
                Me.Controls.Add(grid(i, j))
            Next
        Next

        'Initialize score
        score = New Label
        score.AutoSize = True
        score.TextAlign = ContentAlignment.MiddleCenter
        score.Font = New Font("Calibri", 20, FontStyle.Bold)
        score.Name = "lblScore"
        score.Location = New Point((tileSize + space) * 4 + spaceXBorder + 20, spaceYBorder)
        score.Text = "Score:" & Environment.NewLine & CStr(scoreAmount)
        score.ForeColor = Color.White
        Me.Controls.Add(score)

        'Initialize highscore
        highScore = New Label
        highScore.AutoSize = True
        highScore.TextAlign = ContentAlignment.MiddleCenter
        highScore.Font = New Font("Calibri", 12, FontStyle.Bold)
        highScore.Name = "lblHighScore"
        highScore.Location = New Point((tileSize + space) * 4 + spaceXBorder + 20, spaceYBorder + 100)
        highScore.Text = "Highscore:" & Environment.NewLine & CStr(highScoreAmount)
        highScore.ForeColor = Color.White
        Me.Controls.Add(highScore)

        'Initialize joker
        joker = New Button
        joker.AutoSize = True
        joker.TextAlign = ContentAlignment.MiddleCenter
        joker.Font = New Font("Calibri", 12, FontStyle.Regular)
        joker.Name = "btnJoker"
        joker.Location = New Point((tileSize + space) * 4 + spaceXBorder + 20, spaceYBorder + 250)
        joker.Text = "Joker"
        joker.ForeColor = Color.Black
        joker.BackColor = Color.White
        Me.Controls.Add(joker)

        'Initialize restart
        restart = New Button
        restart.AutoSize = True
        restart.TextAlign = ContentAlignment.MiddleCenter
        restart.Font = New Font("Calibri", 12, FontStyle.Regular)
        restart.Name = "btnRestart"
        restart.Location = New Point((tileSize + space) * 4 + spaceXBorder + 20, spaceYBorder + 300)
        restart.Text = "Restart"
        restart.ForeColor = Color.Black
        restart.BackColor = Color.White
        Me.Controls.Add(restart)

        InitializeBoard()
    End Sub
    Private Sub Form1_KeyUp(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        'MsgBox(e.KeyCode.ToString) 'or .KeyData or .KeyValue
        Dim keyCode As Keys = e.KeyCode
        Select Case keyCode
            Case Keys.Up, Keys.Down, Keys.Left, Keys.Right
                MoveTiles(keyCode)
            Case Keys.R
                InitializeBoard()
        End Select

        CorrectVisualsBoard()
        CheckGameOver()
    End Sub
    Public Sub MoveTiles(direction As Keys)
        Dim keepTryingToMove As Boolean = True
        Dim wasEverTrue As Boolean = False
        While keepTryingToMove = True
            keepTryingToMove = False
            For i As Integer = grid.GetLowerBound(0) To grid.GetUpperBound(0)
                For j As Integer = grid.GetLowerBound(1) To grid.GetUpperBound(1)
                    If Not grid(i, j).Text.Equals("") Then
                        If MoveTile(grid(i, j), i, j, direction) = True Then
                            keepTryingToMove = True
                            wasEverTrue = True
                        End If
                    End If
                Next
            Next
        End While

        If wasEverTrue Then GenerateNewTile()
    End Sub
    Public Sub CheckGameOver()
        Dim noGameOver As Boolean = False
        For i As Integer = grid.GetLowerBound(0) To grid.GetUpperBound(0)
            For j As Integer = grid.GetLowerBound(1) To grid.GetUpperBound(1)
                If grid(i, j).Text.Equals("") Then
                    noGameOver = True
                End If
            Next
        Next

        If Not noGameOver Then
            Dim result As Integer = MessageBox.Show("Your score is " & CStr(scoreAmount) & Environment.NewLine & "Would you like to play again?", "Game Over", MessageBoxButtons.YesNo)
            If result = DialogResult.No Then
                Close()
            ElseIf result = DialogResult.Yes Then
                InitializeBoard()
            End If
        End If
    End Sub

    Public Function MoveTile(tile As Label, i As Integer, j As Integer, direction As Keys) As Boolean
        ' do the necesarry checks to see if a tile can move, if not, return False
        Dim checkTile As Label = tile
        Select Case direction
            Case Keys.Up
                If j = 0 Then Return False
                checkTile = grid(i, j - 1)
            Case Keys.Down
                If j = 3 Then Return False
                checkTile = grid(i, j + 1)
            Case Keys.Left
                If i = 0 Then Return False
                checkTile = grid(i - 1, j)
            Case Keys.Right
                If i = 3 Then Return False
                checkTile = grid(i + 1, j)
        End Select

        'If it can move, return true
        If checkTile.Text.Equals("") Then
            checkTile.Text = tile.Text
            tile.Text = ""
            Return True
        End If
        If checkTile.Text.Equals(tile.Text) Then
            checkTile.Text = CStr(CInt(tile.Text) * 2)
            AddScore(CInt(checkTile.Text))
            tile.Text = ""
            Return True
        End If
        Return False
    End Function
    Public Sub InitializeBoard()
        'Empty the board
        For i As Integer = grid.GetLowerBound(0) To grid.GetUpperBound(0)
            For j As Integer = grid.GetLowerBound(1) To grid.GetUpperBound(1)
                grid(i, j).Text = ""
            Next
        Next

        'Generate a random square
        Randomize()
        Dim k As Integer = CInt(Int(4 * Rnd()))
        Randomize()
        Dim l As Integer = CInt(Int(4 * Rnd()))

        ' 2 or 4 (10% chance for a 4)
        Randomize()
        Dim randN As Integer = CInt(Int(10 * Rnd()))
        If randN Mod 10 = 0 Then
            grid(k, l).Text = "4"
        Else
            grid(k, l).Text = "2"
        End If

        SetScore(0)
        jokersAvailable = 1
        jokerActive = False
        CorrectVisualsBoard()
    End Sub
    Public Sub InitializeBoard(allSolutions As Boolean)
        For i As Integer = grid.GetLowerBound(0) To grid.GetUpperBound(0)
            For j As Integer = grid.GetLowerBound(1) To grid.GetUpperBound(1)
                Dim num As String
                num = CStr(Math.Pow(2, i + (j * 4)))
                If num.Equals("1") Then
                    num = ""
                End If

                grid(i, j).Text = num
            Next
        Next

        SetScore(0)
        CorrectVisualsBoard()
    End Sub
    Public Sub GenerateNewTile()
        Randomize()
        Dim k As Integer = CInt(Int(4 * Rnd()))
        Randomize()
        Dim l As Integer = CInt(Int(4 * Rnd()))
        While Not grid(k, l).Text.Equals("")
            Randomize()
            k = CInt(Int(4 * Rnd()))
            Randomize()
            l = CInt(Int(4 * Rnd()))
        End While

        ' 2 or 4 (10% chance for a 4)
        Randomize()
        Dim randN As Integer = CInt(Int(10 * Rnd()))
        If randN Mod 10 = 0 Then
            grid(k, l).Text = "4"
        Else
            grid(k, l).Text = "2"
        End If

    End Sub
    Public Sub AddScore(amount As Integer)
        scoreAmount = scoreAmount + amount
        score.Text = "Score:" & Environment.NewLine & CStr(scoreAmount)
    End Sub
    Public Sub SetScore(amount As Integer)
        scoreAmount = amount
        score.Text = "Score:" & Environment.NewLine & CStr(scoreAmount)
    End Sub
    Public Sub CorrectVisualsBoard()
        Dim colInvalid As Color = ColorTranslator.FromOle(RGB(255, 0, 0))
        Dim colNothing As Color = ColorTranslator.FromOle(RGB(205, 193, 180))
        Dim col2 As Color = ColorTranslator.FromOle(RGB(238, 228, 218))
        Dim col4 As Color = ColorTranslator.FromOle(RGB(237, 224, 200))
        Dim col8 As Color = ColorTranslator.FromOle(RGB(242, 177, 121))
        Dim col16 As Color = ColorTranslator.FromOle(RGB(245, 149, 99))
        Dim col32 As Color = ColorTranslator.FromOle(RGB(246, 124, 95))
        Dim col64 As Color = ColorTranslator.FromOle(RGB(246, 94, 59))
        Dim col128 As Color = ColorTranslator.FromOle(RGB(237, 207, 114))
        Dim col256 As Color = ColorTranslator.FromOle(RGB(237, 204, 97))
        Dim col512 As Color = ColorTranslator.FromOle(RGB(237, 200, 80))
        Dim col1024 As Color = ColorTranslator.FromOle(RGB(237, 200, 80))
        Dim col2048 As Color = ColorTranslator.FromOle(RGB(237, 200, 80))
        Dim col4096 As Color = ColorTranslator.FromOle(RGB(237, 200, 80))
        Dim col8192 As Color = ColorTranslator.FromOle(RGB(237, 200, 80))
        Dim col16384 As Color = ColorTranslator.FromOle(RGB(237, 200, 80))
        Dim col32768 As Color = ColorTranslator.FromOle(RGB(237, 200, 80))

        For i As Integer = grid.GetLowerBound(0) To grid.GetUpperBound(0)
            For j As Integer = grid.GetLowerBound(1) To grid.GetUpperBound(1)
                Select Case grid(i, j).Text
                    Case ""
                        grid(i, j).BackColor = colNothing
                    Case "2"
                        grid(i, j).BackColor = col2
                    Case "4"
                        grid(i, j).BackColor = col4
                    Case "8"
                        grid(i, j).BackColor = col8
                    Case "16"
                        grid(i, j).BackColor = col16
                    Case "32"
                        grid(i, j).BackColor = col32
                    Case "64"
                        grid(i, j).BackColor = col64
                    Case "128"
                        grid(i, j).BackColor = col128
                    Case "256"
                        grid(i, j).BackColor = col256
                    Case "512"
                        grid(i, j).BackColor = col512
                    Case "1024"
                        grid(i, j).BackColor = col1024
                    Case "2048"
                        grid(i, j).BackColor = col2048
                    Case "4096"
                        grid(i, j).BackColor = col4096
                    Case "8192"
                        grid(i, j).BackColor = col8192
                    Case "16384"
                        grid(i, j).BackColor = col16384
                    Case "32768"
                        grid(i, j).BackColor = col32768
                    Case Else
                        grid(i, j).BackColor = colInvalid
                End Select
                If Not grid(i, j).Text.Equals("") Then
                    If CInt(grid(i, j).Text) > 7 Then
                        grid(i, j).ForeColor = ColorTranslator.FromOle(RGB(249, 246, 242))
                    Else
                        grid(i, j).ForeColor = ColorTranslator.FromOle(RGB(119, 110, 101))
                    End If
                End If

                'Make the size of the text fit
                If Not grid(i, j).Text.Equals("") Then
                    If CInt(grid(i, j).Text) > 9000 Then
                        grid(i, j).Font = New Font("Calibri", 15, FontStyle.Bold)
                    ElseIf CInt(grid(i, j).Text) > 512 Then
                        grid(i, j).Font = New Font("Calibri", 18, FontStyle.Bold)
                    ElseIf CInt(grid(i, j).Text) > 0 Then
                        grid(i, j).Font = New Font("Calibri", 20, FontStyle.Bold)
                    End If
                End If
            Next
        Next

        'Disable joker button if no jokers are available
        If jokersAvailable = 0 Then
            joker.Enabled = False
        Else
            joker.Enabled = True
        End If

        'Check if score is bigger than the highscore and change the text accordingly
        If highScoreAmount < scoreAmount Then
            highScoreAmount = scoreAmount
            highScore.Text = "Highscore:" & Environment.NewLine & CStr(highScoreAmount)
        End If

    End Sub
    Private Sub btnRestart_Click(sender As Object, e As EventArgs) Handles restart.Click
        InitializeBoard()
    End Sub
    Private Sub btnJoker_Click(sender As Object, e As EventArgs) Handles joker.Click
        If jokersAvailable > 0 Then
            jokersAvailable = jokersAvailable - 1
            jokerActive = True
        Else
            MsgBox("No jokers left!")
        End If
        CorrectVisualsBoard()
    End Sub
    Private Sub lblTile_Click(sender As Object, e As EventArgs)
        If jokerActive Then
            Dim amount As Integer = 0
            For i As Integer = grid.GetLowerBound(0) To grid.GetUpperBound(0)
                For j As Integer = grid.GetLowerBound(1) To grid.GetUpperBound(1)
                    If Not grid(i, j).Text = "" Then
                        amount = amount + 1
                    End If
                Next
            Next
            If amount > 1 Then
                DirectCast(sender, Label).Text = ""
                jokerActive = False
                CorrectVisualsBoard()
            End If
        End If
    End Sub
End Class