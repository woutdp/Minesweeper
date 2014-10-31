Option Explicit On
Option Strict On

Imports System.Runtime.InteropServices

Public Class Game
    Dim m_gridTileAmountX As Integer = 2
    Dim m_gridTileAmountY As Integer = 2
    Dim m_menuWidth As Integer = 150
    Dim m_grid(m_gridTileAmountX - 1, m_gridTileAmountY - 1) As Label
    Dim m_roundnessCorners As Integer = 15

    Dim m_lblScore As Label
    Dim m_lblHighScore As Label
    Dim m_score As Integer = 0
    Dim m_highScore As Integer = 0

    Dim WithEvents m_btnRestart As Button
    Dim WithEvents m_btnJoker As Button
    Dim m_jokersAvailable As Integer = 1
    Dim m_bJokerActive As Boolean = False

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Initialize Grid
        Dim tileSize As Integer = 80 'Size of the tiles in x and y
        Dim space As Integer = 7 ' Space in between tiles
        Dim spaceXBorder As Integer = 30 'How much from left border
        Dim spaceYBorder As Integer = 30 'How much from top border

        Dim gridWidth As Integer = (tileSize + space) * m_gridTileAmountX + spaceXBorder * 2
        Dim gridHeight As Integer = (tileSize + space) * m_gridTileAmountY + spaceYBorder * 3

        Me.Size = New Size(gridWidth + m_menuWidth, gridHeight)

        For i As Integer = m_grid.GetLowerBound(0) To m_grid.GetUpperBound(0)
            For j As Integer = m_grid.GetLowerBound(1) To m_grid.GetUpperBound(1)
                m_grid(i, j) = New Label
                m_grid(i, j).Size = New Size(tileSize, tileSize)
                m_grid(i, j).TextAlign = ContentAlignment.MiddleCenter
                m_grid(i, j).Font = New Font("Calibri", 20, FontStyle.Bold)
                m_grid(i, j).ForeColor = ColorTranslator.FromOle(RGB(119, 110, 101))
                m_grid(i, j).Name = "lblTile_" & i & "_" & j
                m_grid(i, j).Location = New Point(i * (tileSize + space) + spaceXBorder,
                                                j * (tileSize + space) + spaceYBorder)
                m_grid(i, j).BackColor = ColorTranslator.FromOle(RGB(205, 193, 180))
                m_grid(i, j).Text = ""
                m_grid(i, j).Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(2, 2, m_grid(i, j).Width - 2, m_grid(i, j).Height - 2, m_roundnessCorners, m_roundnessCorners))

                AddHandler m_grid(i, j).Click, AddressOf lblTile_Click
                Me.Controls.Add(m_grid(i, j))
            Next
        Next

        'Initialize m_lblScore
        m_lblScore = New Label
        m_lblScore.AutoSize = True
        m_lblScore.TextAlign = ContentAlignment.MiddleCenter
        m_lblScore.Font = New Font("Calibri", 20, FontStyle.Bold)
        m_lblScore.Name = "lblScore"
        m_lblScore.Location = New Point(gridWidth, spaceYBorder)
        m_lblScore.Text = "Score:" & Environment.NewLine & CStr(m_score)
        m_lblScore.ForeColor = Color.White
        Me.Controls.Add(m_lblScore)

        'Initialize highscore
        m_lblHighScore = New Label
        m_lblHighScore.AutoSize = True
        m_lblHighScore.TextAlign = ContentAlignment.MiddleCenter
        m_lblHighScore.Font = New Font("Calibri", 12, FontStyle.Bold)
        m_lblHighScore.Name = "lblHighScore"
        m_lblHighScore.Location = New Point(gridWidth, spaceYBorder + 100)
        m_lblHighScore.Text = "Highscore:" & Environment.NewLine & CStr(m_highScore)
        m_lblHighScore.ForeColor = Color.White
        Me.Controls.Add(m_lblHighScore)

        'Initialize m_btnJoker
        m_btnJoker = New Button
        m_btnJoker.Size = New Size(100, 40)
        m_btnJoker.TextAlign = ContentAlignment.MiddleCenter
        m_btnJoker.Font = New Font("Calibri", 12, FontStyle.Regular)
        m_btnJoker.Name = "btnJoker"
        m_btnJoker.Location = New Point(gridWidth, gridHeight - 170)
        m_btnJoker.Text = "Joker"
        m_btnJoker.ForeColor = Color.Black
        m_btnJoker.BackColor = Color.White
        Me.Controls.Add(m_btnJoker)

        'Initialize m_btnRestart
        m_btnRestart = New Button
        m_btnRestart.Size = New Size(100, 40)
        m_btnRestart.TextAlign = ContentAlignment.MiddleCenter
        m_btnRestart.Font = New Font("Calibri", 12, FontStyle.Regular)
        m_btnRestart.Name = "btnRestart"
        m_btnRestart.Location = New Point(gridWidth, gridHeight - 120)
        m_btnRestart.Text = "Restart"
        m_btnRestart.ForeColor = Color.Black
        m_btnRestart.BackColor = Color.White
        Me.Controls.Add(m_btnRestart)

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

        CorrectGame()
        CheckGameOver()
    End Sub
    Private Sub MoveTiles(direction As Keys)
        Dim keepTryingToMove As Boolean = True
        Dim wasEverTrue As Boolean = False
        While keepTryingToMove = True
            keepTryingToMove = False
            For i As Integer = m_grid.GetLowerBound(0) To m_grid.GetUpperBound(0)
                For j As Integer = m_grid.GetLowerBound(1) To m_grid.GetUpperBound(1)
                    If Not m_grid(i, j).Text.Equals("") Then
                        If MoveTile(m_grid(i, j), i, j, direction) = True Then
                            keepTryingToMove = True
                            wasEverTrue = True
                        End If
                    End If
                Next
            Next
        End While

        If wasEverTrue Then GenerateNewTile()
    End Sub
    Private Sub CheckGameOver()
        Dim noGameOver As Boolean = False
        For i As Integer = m_grid.GetLowerBound(0) To m_grid.GetUpperBound(0)
            For j As Integer = m_grid.GetLowerBound(1) To m_grid.GetUpperBound(1)
                If PreCheckMoveTile(m_grid(i, j), i, j, Keys.Up) Then
                    noGameOver = True
                    Exit For
                ElseIf PreCheckMoveTile(m_grid(i, j), i, j, Keys.Down) Then
                    noGameOver = True
                    Exit For
                ElseIf PreCheckMoveTile(m_grid(i, j), i, j, Keys.Left) Then
                    noGameOver = True
                    Exit For
                ElseIf PreCheckMoveTile(m_grid(i, j), i, j, Keys.Right) Then
                    noGameOver = True
                    Exit For
                End If
            Next
            If noGameOver Then Exit For
        Next

        If Not noGameOver Then
            Dim result As Integer = MessageBox.Show("Your score is " & CStr(m_score) & Environment.NewLine & "Would you like to play again?", "Game Over", MessageBoxButtons.YesNo)
            If result = DialogResult.No Then
                Close()
            ElseIf result = DialogResult.Yes Then
                InitializeBoard()
            End If
        End If
    End Sub

    Private Function MoveTile(tile As Label, i As Integer, j As Integer, direction As Keys) As Boolean
        ' do the necesarry checks to see if a tile can move, if not, return False
        Dim checkTile As Label = tile
        Select Case direction
            Case Keys.Up
                If j = 0 Then Return False
                checkTile = m_grid(i, j - 1)
            Case Keys.Down
                If j = m_gridTileAmountY - 1 Then Return False
                checkTile = m_grid(i, j + 1)
            Case Keys.Left
                If i = 0 Then Return False
                checkTile = m_grid(i - 1, j)
            Case Keys.Right
                If i = m_gridTileAmountX - 1 Then Return False
                checkTile = m_grid(i + 1, j)
        End Select

        'If it can move, return true
        If checkTile.Text.Equals("") Then
            checkTile.Text = tile.Text
            tile.Text = ""
            Return True
        ElseIf checkTile.Text.Equals(tile.Text) Then
            checkTile.Text = CStr(CInt(tile.Text) * 2)
            AddScore(CInt(checkTile.Text))
            tile.Text = ""
            Return True
        End If
        Return False
    End Function
    Private Function PreCheckMoveTile(tile As Label, i As Integer, j As Integer, direction As Keys) As Boolean
        ' do the necesarry checks to see if a tile can move, if not, return False
        Dim checkTile As Label = tile
        Select Case direction
            Case Keys.Up
                If j = 0 Then Return False
                checkTile = m_grid(i, j - 1)
            Case Keys.Down
                If j = m_gridTileAmountY - 1 Then Return False
                checkTile = m_grid(i, j + 1)
            Case Keys.Left
                If i = 0 Then Return False
                checkTile = m_grid(i - 1, j)
            Case Keys.Right
                If i = m_gridTileAmountX - 1 Then Return False
                checkTile = m_grid(i + 1, j)
        End Select

        'If it can move, return true
        If checkTile.Text.Equals("") Or checkTile.Text.Equals(tile.Text) Then
            Return True
        End If

        Return False
    End Function
    Private Sub InitializeBoard()
        'Empty the board
        For i As Integer = m_grid.GetLowerBound(0) To m_grid.GetUpperBound(0)
            For j As Integer = m_grid.GetLowerBound(1) To m_grid.GetUpperBound(1)
                m_grid(i, j).Text = ""
            Next
        Next

        'Generate a random square
        Randomize()
        Dim k As Integer = CInt(Int(m_gridTileAmountX * Rnd()))
        Dim l As Integer = CInt(Int(m_gridTileAmountY * Rnd()))

        ' 2 or 4 (10% chance for a 4)
        Randomize()
        Dim randN As Integer = CInt(Int(10 * Rnd()))
        If randN Mod 10 = 0 Then
            m_grid(k, l).Text = "4"
        Else
            m_grid(k, l).Text = "2"
        End If

        SetScore(0)
        m_jokersAvailable = 1
        m_bJokerActive = False
        CorrectGame()
    End Sub
    Private Sub InitializeBoard(allSolutions As Boolean)
        For i As Integer = m_grid.GetLowerBound(0) To m_grid.GetUpperBound(0)
            For j As Integer = m_grid.GetLowerBound(1) To m_grid.GetUpperBound(1)
                Dim num As String
                num = CStr(Math.Pow(2, i + (j * 4)))
                If num.Equals("1") Then
                    num = ""
                End If

                m_grid(i, j).Text = num
            Next
        Next

        SetScore(0)
        m_jokersAvailable = 1
        m_bJokerActive = False
        CorrectGame()
    End Sub
    Private Sub GenerateNewTile()
        Randomize()
        Dim k As Integer = CInt(Int(m_gridTileAmountX * Rnd()))
        Dim l As Integer = CInt(Int(m_gridTileAmountY * Rnd()))
        While Not m_grid(k, l).Text.Equals("")
            k = CInt(Int(m_gridTileAmountX * Rnd()))
            l = CInt(Int(m_gridTileAmountY * Rnd()))
        End While

        ' 2 or 4 (10% chance for a 4)
        Dim randN As Integer = CInt(Int(10 * Rnd()))
        If randN Mod 10 = 0 Then
            m_grid(k, l).Text = "4"
        Else
            m_grid(k, l).Text = "2"
        End If

    End Sub
    Private Sub AddScore(amount As Integer)
        m_score = m_score + amount
        m_lblScore.Text = "Score:" & Environment.NewLine & CStr(m_score)
    End Sub
    Private Sub SetScore(amount As Integer)
        m_score = amount
        m_lblScore.Text = "Score:" & Environment.NewLine & CStr(m_score)
    End Sub
    Private Sub CorrectGame()
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

        For i As Integer = m_grid.GetLowerBound(0) To m_grid.GetUpperBound(0)
            For j As Integer = m_grid.GetLowerBound(1) To m_grid.GetUpperBound(1)
                Select Case m_grid(i, j).Text
                    Case ""
                        m_grid(i, j).BackColor = colNothing
                    Case "2"
                        m_grid(i, j).BackColor = col2
                    Case "4"
                        m_grid(i, j).BackColor = col4
                    Case "8"
                        m_grid(i, j).BackColor = col8
                    Case "16"
                        m_grid(i, j).BackColor = col16
                    Case "32"
                        m_grid(i, j).BackColor = col32
                    Case "64"
                        m_grid(i, j).BackColor = col64
                    Case "128"
                        m_grid(i, j).BackColor = col128
                    Case "256"
                        m_grid(i, j).BackColor = col256
                    Case "512"
                        m_grid(i, j).BackColor = col512
                    Case "1024"
                        m_grid(i, j).BackColor = col1024
                    Case "2048"
                        m_grid(i, j).BackColor = col2048
                    Case "4096"
                        m_grid(i, j).BackColor = col4096
                    Case "8192"
                        m_grid(i, j).BackColor = col8192
                    Case "16384"
                        m_grid(i, j).BackColor = col16384
                    Case "32768"
                        m_grid(i, j).BackColor = col32768
                    Case Else
                        m_grid(i, j).BackColor = colInvalid
                End Select
                If Not m_grid(i, j).Text.Equals("") Then
                    If CInt(m_grid(i, j).Text) > 7 Then
                        m_grid(i, j).ForeColor = ColorTranslator.FromOle(RGB(249, 246, 242))
                    Else
                        m_grid(i, j).ForeColor = ColorTranslator.FromOle(RGB(119, 110, 101))
                    End If
                End If

                'Make the size of the text fit
                If Not m_grid(i, j).Text.Equals("") Then
                    If CInt(m_grid(i, j).Text) > 9000 Then
                        m_grid(i, j).Font = New Font("Calibri", 15, FontStyle.Bold)
                    ElseIf CInt(m_grid(i, j).Text) > 512 Then
                        m_grid(i, j).Font = New Font("Calibri", 18, FontStyle.Bold)
                    ElseIf CInt(m_grid(i, j).Text) > 0 Then
                        m_grid(i, j).Font = New Font("Calibri", 20, FontStyle.Bold)
                    End If
                End If
            Next
        Next

        'Disable m_btnJoker button if no jokers are available
        If m_jokersAvailable = 0 Then
            m_btnJoker.Enabled = False
        Else
            m_btnJoker.Enabled = True
        End If

        'Check if m_lblScore is bigger than the highscore and change the text accordingly
        If m_highScore < m_score Then
            m_highScore = m_score
            m_lblHighScore.Text = "Highscore:" & Environment.NewLine & CStr(m_highScore)
        End If

    End Sub
    Private Sub btnRestart_Click(sender As Object, e As EventArgs) Handles m_btnRestart.Click
        InitializeBoard()
    End Sub
    Private Sub btnJoker_Click(sender As Object, e As EventArgs) Handles m_btnJoker.Click
        If m_jokersAvailable > 0 Then
            m_jokersAvailable = m_jokersAvailable - 1
            m_bJokerActive = True
        Else
            MsgBox("No jokers left!")
        End If
        CorrectGame()
    End Sub
    Private Sub lblTile_Click(sender As Object, e As EventArgs)
        If m_bJokerActive Then
            Dim amount As Integer = 0
            For i As Integer = m_grid.GetLowerBound(0) To m_grid.GetUpperBound(0)
                For j As Integer = m_grid.GetLowerBound(1) To m_grid.GetUpperBound(1)
                    If Not m_grid(i, j).Text = "" Then
                        amount = amount + 1
                    End If
                Next
            Next
            If amount > 1 Then
                DirectCast(sender, Label).Text = ""
                m_bJokerActive = False
                CorrectGame()
            End If
        End If
    End Sub
    'This function was found on the internet and just makes the labels with rounded edges
    <DllImport("Gdi32.dll", EntryPoint:="CreateRoundRectRgn")> _
    Private Shared Function CreateRoundRectRgn(ByVal iLeft As Integer, ByVal iTop As Integer, ByVal iRight As Integer, ByVal iBottom As Integer, ByVal iWidth As Integer, ByVal iHeight As Integer) As IntPtr

    End Function
End Class