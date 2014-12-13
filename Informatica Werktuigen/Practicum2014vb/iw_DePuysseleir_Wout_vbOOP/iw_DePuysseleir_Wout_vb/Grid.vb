Option Explicit On
Option Strict On

Imports System.Runtime.InteropServices

Public Class Grid
    Private m_tileAmountX As Integer
    Private m_tileAmountY As Integer
    Private m_gridWidth As Integer
    Private m_gridHeight As Integer
    Private m_roundnessCorners As Integer = 4

    Private m_tile(,) As GridTile
    Private m_game As Game
    Private m_lblBackground As Label

    Public Sub New(ByRef game As Game, Optional width As Integer = 4, Optional height As Integer = 4)
        m_game = game
        m_tileAmountX = width
        m_tileAmountY = height

        Dim tileSize As Integer = 90 'Size of the tiles in x and y
        Dim space As Integer = 7 ' Space in between tiles

        m_gridWidth = (tileSize + space) * m_tileAmountX - space
        m_gridHeight = (tileSize + space) * m_tileAmountY - space

        ReDim m_tile(m_tileAmountX - 1, m_tileAmountY - 1)
        For i As Integer = m_tile.GetLowerBound(0) To m_tile.GetUpperBound(0)
            For j As Integer = m_tile.GetLowerBound(1) To m_tile.GetUpperBound(1)
                m_tile(i, j) = New GridTile(i, j)
                m_tile(i, j).Size = New Size(tileSize, tileSize)
                m_tile(i, j).TextAlign = ContentAlignment.MiddleCenter
                m_tile(i, j).Font = New Font("Calibri", 20, FontStyle.Bold)
                m_tile(i, j).ForeColor = ColorTranslator.FromOle(RGB(119, 110, 101))
                m_tile(i, j).Name = "lblTile_" & i & "_" & j
                m_tile(i, j).Location = New Point(i * (tileSize + space) + m_game.spaceXBorder,
                                                j * (tileSize + space) + m_game.spaceYBorder)
                m_tile(i, j).BackColor = ColorTranslator.FromOle(RGB(205, 193, 180))
                m_tile(i, j).Text = ""
                m_tile(i, j).Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, m_tile(i, j).Width, m_tile(i, j).Height, m_roundnessCorners, m_roundnessCorners))

                AddHandler m_tile(i, j).Click, AddressOf lblTile_Click
                game.Controls.Add(m_tile(i, j))
            Next
        Next

        'Generate a background for the board
        m_lblBackground = New Label
        m_lblBackground.Name = "lblBackground"
        Dim tempX As Integer = CInt(Convert.ToDouble(m_game.spaceXBorder) / 2)
        Dim tempY As Integer = CInt(Convert.ToDouble(m_game.spaceYBorder) / 2)
        m_lblBackground.Location = New Point(tempX, tempY)
        m_lblBackground.Size = New Size(m_gridWidth + tempX * 2, m_gridHeight + tempY * 2)
        m_lblBackground.BackColor = ColorTranslator.FromOle(RGB(187, 173, 160))
        m_lblBackground.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, m_lblBackground.Width - 0, m_lblBackground.Height - 0, 15, 15))
        m_game.Controls.Add(m_lblBackground)

    End Sub
    Private Sub lblTile_Click(sender As Object, e As EventArgs)
        If m_game.jokerActive Then
            Dim amount As Integer = 0
            For i As Integer = m_tile.GetLowerBound(0) To m_tile.GetUpperBound(0)
                For j As Integer = m_tile.GetLowerBound(1) To m_tile.GetUpperBound(1)
                    If Not m_tile(i, j).Text = "" Then
                        amount = amount + 1
                    End If
                Next
            Next
            If amount > 1 Then
                DirectCast(sender, Label).Text = ""
                m_game.jokerActive = False
                m_game.CorrectGame()
            End If
        End If
    End Sub
    Public Sub CorrectGrid()
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

        For i As Integer = m_tile.GetLowerBound(0) To m_tile.GetUpperBound(0)
            For j As Integer = m_tile.GetLowerBound(1) To m_tile.GetUpperBound(1)
                Select Case m_tile(i, j).Text
                    Case ""
                        m_tile(i, j).BackColor = colNothing
                    Case "2"
                        m_tile(i, j).BackColor = col2
                    Case "4"
                        m_tile(i, j).BackColor = col4
                    Case "8"
                        m_tile(i, j).BackColor = col8
                    Case "16"
                        m_tile(i, j).BackColor = col16
                    Case "32"
                        m_tile(i, j).BackColor = col32
                    Case "64"
                        m_tile(i, j).BackColor = col64
                    Case "128"
                        m_tile(i, j).BackColor = col128
                    Case "256"
                        m_tile(i, j).BackColor = col256
                    Case "512"
                        m_tile(i, j).BackColor = col512
                    Case "1024"
                        m_tile(i, j).BackColor = col1024
                    Case "2048"
                        m_tile(i, j).BackColor = col2048
                    Case "4096"
                        m_tile(i, j).BackColor = col4096
                    Case "8192"
                        m_tile(i, j).BackColor = col8192
                    Case "16384"
                        m_tile(i, j).BackColor = col16384
                    Case "32768"
                        m_tile(i, j).BackColor = col32768
                    Case Else
                        m_tile(i, j).BackColor = colInvalid
                End Select
                If Not m_tile(i, j).Text.Equals("") Then
                    If Convert.ToDouble(m_tile(i, j).Text) > 7 Then
                        m_tile(i, j).ForeColor = ColorTranslator.FromOle(RGB(249, 246, 242))
                    Else
                        m_tile(i, j).ForeColor = ColorTranslator.FromOle(RGB(119, 110, 101))
                    End If
                End If

                'Make the size of the text fit
                If Not m_tile(i, j).Text.Equals("") Then
                    If Convert.ToDouble(m_tile(i, j).Text) > 9999999 Then
                        m_tile(i, j).Font = New Font("Calibri", 11, FontStyle.Bold)
                    ElseIf Convert.ToDouble(m_tile(i, j).Text) > 99999 Then
                        m_tile(i, j).Font = New Font("Calibri", 13, FontStyle.Bold)
                    ElseIf CInt(m_tile(i, j).Text) > 9999 Then
                        m_tile(i, j).Font = New Font("Calibri", 17, FontStyle.Bold)
                    ElseIf CInt(m_tile(i, j).Text) > 999 Then
                        m_tile(i, j).Font = New Font("Calibri", 20, FontStyle.Bold)
                    ElseIf CInt(m_tile(i, j).Text) > 99 Then
                        m_tile(i, j).Font = New Font("Calibri", 24, FontStyle.Bold)
                    ElseIf CInt(m_tile(i, j).Text) > 0 Then
                        m_tile(i, j).Font = New Font("Calibri", 26, FontStyle.Bold)
                    End If
                End If
            Next
        Next
    End Sub
    Public Sub CheckGameOver()
        Dim noGameOver As Boolean = False
        For i As Integer = m_tile.GetLowerBound(0) To m_tile.GetUpperBound(0)
            For j As Integer = m_tile.GetLowerBound(1) To m_tile.GetUpperBound(1)
                If PreCheckMoveTile(m_tile(i, j), i, j, Keys.Up) Then
                    noGameOver = True
                    Exit For
                ElseIf PreCheckMoveTile(m_tile(i, j), i, j, Keys.Down) Then
                    noGameOver = True
                    Exit For
                ElseIf PreCheckMoveTile(m_tile(i, j), i, j, Keys.Left) Then
                    noGameOver = True
                    Exit For
                ElseIf PreCheckMoveTile(m_tile(i, j), i, j, Keys.Right) Then
                    noGameOver = True
                    Exit For
                End If
            Next
            If noGameOver Then Exit For
        Next

        If Not noGameOver Then
            Dim result As Integer = MessageBox.Show("Your score is " & CStr(m_game.score) & Environment.NewLine & "Would you like to play again?", "Game Over", MessageBoxButtons.YesNo)
            If result = DialogResult.No Then
                m_game.Close()
            ElseIf result = DialogResult.Yes Then
                InitializeBoard()
            End If
        End If
    End Sub
    Public Sub MoveTiles(direction As Keys)
        Dim keepTryingToMove As Boolean = True
        Dim wasEverTrue As Boolean = False
        While keepTryingToMove = True
            keepTryingToMove = False
            For i As Integer = m_tile.GetLowerBound(0) To m_tile.GetUpperBound(0)
                For j As Integer = m_tile.GetLowerBound(1) To m_tile.GetUpperBound(1)
                    If Not m_tile(i, j).Text.Equals("") Then
                        If MoveTile(m_tile(i, j), i, j, direction) = True Then
                            keepTryingToMove = True
                            wasEverTrue = True
                        End If
                    End If
                Next
            Next
        End While

        If wasEverTrue Then GenerateNewTile()
    End Sub
    Private Function MoveTile(tile As Label, i As Integer, j As Integer, direction As Keys) As Boolean
        ' do the necesarry checks to see if a tile can move, if not, return False
        Dim checkTile As Label = tile
        Select Case direction
            Case Keys.Up
                If j = 0 Then Return False
                checkTile = m_tile(i, j - 1)
            Case Keys.Down
                If j = m_tileAmountY - 1 Then Return False
                checkTile = m_tile(i, j + 1)
            Case Keys.Left
                If i = 0 Then Return False
                checkTile = m_tile(i - 1, j)
            Case Keys.Right
                If i = m_tileAmountX - 1 Then Return False
                checkTile = m_tile(i + 1, j)
        End Select

        'If it can move, return true
        If checkTile.Text.Equals("") Then
            checkTile.Text = tile.Text
            tile.Text = ""
            Return True
        ElseIf checkTile.Text.Equals(tile.Text) Then
            checkTile.Text = CStr(CInt(tile.Text) * 2)
            m_game.AddScore(CInt(checkTile.Text))
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
                checkTile = m_tile(i, j - 1)
            Case Keys.Down
                If j = m_tileAmountY - 1 Then Return False
                checkTile = m_tile(i, j + 1)
            Case Keys.Left
                If i = 0 Then Return False
                checkTile = m_tile(i - 1, j)
            Case Keys.Right
                If i = m_tileAmountX - 1 Then Return False
                checkTile = m_tile(i + 1, j)
        End Select

        'If it can move, return true
        If checkTile.Text.Equals("") Or checkTile.Text.Equals(tile.Text) Then
            Return True
        End If

        Return False
    End Function
    Public Sub InitializeBoard(Optional allSolutions As Boolean = False)
        If allSolutions = False Then
            'Empty the board
            For i As Integer = m_tile.GetLowerBound(0) To m_tile.GetUpperBound(0)
                For j As Integer = m_tile.GetLowerBound(1) To m_tile.GetUpperBound(1)
                    m_tile(i, j).Text = ""
                Next
            Next

            'Generate a random square
            Randomize()
            Dim k As Integer = CInt(Int(m_tileAmountX * Rnd()))
            Dim l As Integer = CInt(Int(m_tileAmountY * Rnd()))

            ' 2 or 4 (10% chance for a 4)
            Randomize()
            Dim randN As Integer = CInt(Int(10 * Rnd()))
            If randN Mod 10 = 0 Then
                m_tile(k, l).Text = "4"
            Else
                m_tile(k, l).Text = "2"
            End If
        Else
            For i As Integer = m_tile.GetLowerBound(0) To m_tile.GetUpperBound(0)
                For j As Integer = m_tile.GetLowerBound(1) To m_tile.GetUpperBound(1)
                    Dim num As String
                    num = CStr(Math.Pow(2, i + (j * Convert.ToDouble(m_tile.GetUpperBound(1)))))
                    If num.Equals("1") Then
                        num = ""
                    End If

                    m_tile(i, j).Text = num
                Next
            Next
        End If

        m_game.SetScore(0)
        m_game.jokersAvailable = 1
        m_game.jokerActive = False
        m_game.CorrectGame()
    End Sub
    Private Sub GenerateNewTile()
        Randomize()
        Dim k As Integer = CInt(Int(m_tileAmountX * Rnd()))
        Dim l As Integer = CInt(Int(m_tileAmountY * Rnd()))
        While Not m_tile(k, l).Text.Equals("")
            k = CInt(Int(m_tileAmountX * Rnd()))
            l = CInt(Int(m_tileAmountY * Rnd()))
        End While

        ' 2 or 4 (10% chance for a 4)
        Dim randN As Integer = CInt(Int(10 * Rnd()))
        If randN Mod 10 = 0 Then
            m_tile(k, l).Text = "4"
        Else
            m_tile(k, l).Text = "2"
        End If

    End Sub
    Public Property gridWidth() As Integer
        Get
            Return m_gridWidth
        End Get
        Private Set(ByVal value As Integer)
            m_gridWidth = value
        End Set
    End Property
    Public Property gridHeight() As Integer
        Get
            Return m_gridHeight
        End Get
        Private Set(ByVal value As Integer)
            m_gridHeight = value
        End Set
    End Property
    'This function was found on the internet and just makes the labels with rounded edges
    <DllImport("Gdi32.dll", EntryPoint:="CreateRoundRectRgn")> _
    Private Shared Function CreateRoundRectRgn(ByVal iLeft As Integer, ByVal iTop As Integer, ByVal iRight As Integer, ByVal iBottom As Integer, ByVal iWidth As Integer, ByVal iHeight As Integer) As IntPtr

    End Function
End Class
