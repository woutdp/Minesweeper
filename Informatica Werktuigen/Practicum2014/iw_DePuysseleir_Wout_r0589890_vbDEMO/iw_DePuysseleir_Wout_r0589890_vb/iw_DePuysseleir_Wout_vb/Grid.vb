Option Explicit On
Option Strict On

Imports System.Runtime.InteropServices

Public Class Grid
    Private m_tileAmountX As Integer
    Private m_tileAmountY As Integer
    Private m_gridWidth As Integer
    Private m_gridHeight As Integer
    Private m_roundnessCorners As Integer = 4
    Private m_lastReturnOfMovement As Boolean = False

    Private m_tile(,) As GridTile
    Private m_game As Game
    Private m_lblBackground As Label
    Private m_newTileTimer As Stopwatch

    Private m_generateDelay As Integer = 200
    Private WithEvents m_timer As Timer
    Private m_time As Integer = 5

    Public Sub New(ByRef game As Game, Optional width As Integer = 4, Optional height As Integer = 4)
        m_timer = New Timer()
        m_newTileTimer = New Stopwatch()
        m_game = game
        m_tileAmountX = width
        m_tileAmountY = height

        Dim tileSize As Integer = 90 'Size of the tiles in x and y
        Dim space As Integer = 12 ' Space in between tiles

        m_gridWidth = (tileSize + space) * m_tileAmountX - space
        m_gridHeight = (tileSize + space) * m_tileAmountY - space

        ReDim m_tile(m_tileAmountX - 1, m_tileAmountY - 1)
        For i As Integer = m_tile.GetLowerBound(0) To m_tile.GetUpperBound(0)
            For j As Integer = m_tile.GetLowerBound(1) To m_tile.GetUpperBound(1)
                m_tile(i, j) = New GridTile(i, j, Me)
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
                m_tile(i, j).SendToBack()
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
        m_lblBackground.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, m_lblBackground.Width, m_lblBackground.Height, 12, 12))
        m_game.Controls.Add(m_lblBackground)
        m_lblBackground.SendToBack()

    End Sub
    Public Sub SendBackgroundToBack()
        For i As Integer = m_tile.GetLowerBound(0) To m_tile.GetUpperBound(0)
            For j As Integer = m_tile.GetLowerBound(1) To m_tile.GetUpperBound(1)
                m_tile(i, j).SendToBack()
            Next
        Next
        m_lblBackground.SendToBack()
    End Sub
    Public Sub GameUpdate(deltaTime As Long)
        For i As Integer = m_tile.GetLowerBound(0) To m_tile.GetUpperBound(0)
            For j As Integer = m_tile.GetLowerBound(1) To m_tile.GetUpperBound(1)
                m_tile(i, j).GameUpdate(deltaTime)
            Next
        Next

        If m_newTileTimer.ElapsedMilliseconds > m_generateDelay And m_lastReturnOfMovement = True Then
            GenerateNewTile(True)
        ElseIf m_newTileTimer.ElapsedMilliseconds > m_generateDelay Then
            m_newTileTimer.Reset()
        End If
    End Sub
    Private Sub lblTile_Click(sender As Object, e As EventArgs)
        If m_game.jokerActive Then
            Dim amount As Integer = 0
            For i As Integer = m_tile.GetLowerBound(0) To m_tile.GetUpperBound(0)
                For j As Integer = m_tile.GetLowerBound(1) To m_tile.GetUpperBound(1)
                    If Not m_tile(i, j).value = 0 Then
                        amount = amount + 1
                    End If
                Next
            Next
            If amount > 1 Then
                DirectCast(sender, GridTile).value = 0
                DirectCast(sender, GridTile).UpdateValue()
                m_game.jokerActive = False
                m_game.CorrectGame()
            End If
        End If
    End Sub
    Public Sub CorrectGrid()
        For i As Integer = m_tile.GetLowerBound(0) To m_tile.GetUpperBound(0)
            For j As Integer = m_tile.GetLowerBound(1) To m_tile.GetUpperBound(1)
                m_tile(i, j).CorrectTile()
            Next
        Next
    End Sub
    Public Sub CheckGameOver()
        If m_game.reverse = False Then
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
        Else
            Dim gameOver As Boolean = False
            For i As Integer = m_tile.GetLowerBound(0) To m_tile.GetUpperBound(0)
                For j As Integer = m_tile.GetLowerBound(1) To m_tile.GetUpperBound(1)
                    If m_tile(i, j).Text.Equals("512") Then
                        gameOver = True
                    End If
                Next
                If gameOver Then Exit For
            Next

            If gameOver Then
                Dim result As Integer = MessageBox.Show("Your score is " & CStr(m_game.score) & Environment.NewLine & "Would you like to play again?", "Game Over", MessageBoxButtons.YesNo)
                If result = DialogResult.No Then
                    m_game.Close()
                ElseIf result = DialogResult.Yes Then
                    InitializeBoard()
                End If
            End If

        End If
    End Sub
    Public Sub MoveTiles(direction As Keys)
        Dim keepTryingToMove As Boolean = True
        Dim wasEverTrue As Boolean = False
        While keepTryingToMove = True
            keepTryingToMove = False
            Select Case direction
                Case Keys.Up
                    For i As Integer = m_tile.GetLowerBound(0) To m_tile.GetUpperBound(0)
                        For j As Integer = m_tile.GetLowerBound(1) To m_tile.GetUpperBound(1)
                            If m_tile(i, j).MoveTile(direction) = True Then
                                keepTryingToMove = True
                                wasEverTrue = True
                            End If
                        Next
                    Next
                Case Keys.Down
                    For i As Integer = m_tile.GetLowerBound(0) To m_tile.GetUpperBound(0)
                        For j As Integer = m_tile.GetUpperBound(1) To m_tile.GetLowerBound(1) Step -1
                            If m_tile(i, j).MoveTile(direction) = True Then
                                keepTryingToMove = True
                                wasEverTrue = True
                            End If
                        Next
                    Next
                Case Keys.Left
                    For j As Integer = m_tile.GetLowerBound(1) To m_tile.GetUpperBound(1)
                        For i As Integer = m_tile.GetLowerBound(0) To m_tile.GetUpperBound(0)
                            If m_tile(i, j).MoveTile(direction) = True Then
                                keepTryingToMove = True
                                wasEverTrue = True
                            End If
                        Next
                    Next
                Case Keys.Right
                    For j As Integer = m_tile.GetLowerBound(1) To m_tile.GetUpperBound(1)
                        For i As Integer = m_tile.GetUpperBound(0) To m_tile.GetLowerBound(0) Step -1
                            If m_tile(i, j).MoveTile(direction) = True Then
                                keepTryingToMove = True
                                wasEverTrue = True
                            End If
                        Next
                    Next
            End Select
        End While

        If m_game.generatingTimer = False And wasEverTrue = True Then
            GenerateNewTile()
        End If
        If m_game.generatingTimer = True And wasEverTrue = True Then
            ResetTimer()
            m_game.timerLabel.Text = "Timer:" & Environment.NewLine & CStr(m_time)
        End If

        m_lastReturnOfMovement = wasEverTrue
    End Sub
    Private Function PreCheckMoveTile(tile As GridTile, i As Integer, j As Integer, direction As Keys) As Boolean
        ' do the necesarry checks to see if a tile can move, if not, return False
        Dim checkTile As GridTile = tile
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
        If checkTile.value = 0 Or checkTile.value = tile.value Then
            Return True
        End If

        Return False
    End Function
    Public Sub InitializeBoard(Optional allSolutions As Boolean = False)
        If m_game.generatingTimer = True Then
            m_timer.Start()
            m_timer.Interval = 1000
        End If

        If m_game.reverse = False Then
            If allSolutions = False Then
                'Empty the board
                For i As Integer = m_tile.GetLowerBound(0) To m_tile.GetUpperBound(0)
                    For j As Integer = m_tile.GetLowerBound(1) To m_tile.GetUpperBound(1)
                        m_tile(i, j).value = 0
                        m_tile(i, j).UpdateValue()
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
                    m_tile(k, l).value = 4
                    m_tile(k, l).UpdateValue()
                Else
                    m_tile(k, l).value = 2
                    m_tile(k, l).UpdateValue()
                End If
            Else
                For i As Integer = m_tile.GetLowerBound(0) To m_tile.GetUpperBound(0)
                    For j As Integer = m_tile.GetLowerBound(1) To m_tile.GetUpperBound(1)
                        Dim num As Integer
                        num = CInt(Math.Pow(2, i + (j * Convert.ToDouble(m_tile.GetUpperBound(1)))))
                        If num = 1 Then
                            num = 0
                        End If

                        m_tile(i, j).value = num
                        m_tile(i, j).UpdateValue()
                    Next
                Next
            End If
            m_lastReturnOfMovement = False

            m_game.SetScore(0)
            m_game.jokersAvailable = 1
            m_game.jokerActive = False
            m_game.CorrectGame()
        Else
            'Empty the board
            For i As Integer = m_tile.GetLowerBound(0) To m_tile.GetUpperBound(0)
                For j As Integer = m_tile.GetLowerBound(1) To m_tile.GetUpperBound(1)
                    m_tile(i, j).value = 0
                    m_tile(i, j).UpdateValue()

                    Randomize()
                    Dim randN As Integer = CInt(Int(2 * Rnd()))
                    If randN Mod 2 = 0 Then
                        m_tile(i, j).value = 2048
                        m_tile(i, j).UpdateValue()
                    Else
                        m_tile(i, j).value = 1024
                        m_tile(i, j).UpdateValue()
                    End If
                Next
            Next

            m_lastReturnOfMovement = False

            m_game.SetScore(0)
            m_game.jokersAvailable = 1
            m_game.jokerActive = False
            m_game.CorrectGame()
        End If
    End Sub
    Private Sub GenerateNewTile(Optional immediateTile As Boolean = False)
        If m_game.reverse = False Then
            If immediateTile = False Then
                If m_newTileTimer.ElapsedMilliseconds > 0 Then
                    m_newTileTimer.Reset()
                    Randomize()
                    Dim k As Integer = CInt(Int(m_tileAmountX * Rnd()))
                    Dim l As Integer = CInt(Int(m_tileAmountY * Rnd()))
                    While Not m_tile(k, l).value = 0
                        k = CInt(Int(m_tileAmountX * Rnd()))
                        l = CInt(Int(m_tileAmountY * Rnd()))
                    End While

                    ' 2 or 4 (10% chance for a 4)
                    Dim randN As Integer = CInt(Int(10 * Rnd()))
                    If randN Mod 10 = 0 Then
                        m_tile(k, l).value = 4
                    Else
                        m_tile(k, l).value = 2
                    End If
                    m_tile(k, l).UpdateValue()
                    m_tile(k, l).CorrectTile()
                End If
                m_newTileTimer.Start()
            Else
                m_newTileTimer.Reset()
                Randomize()
                Dim k As Integer = CInt(Int(m_tileAmountX * Rnd()))
                Dim l As Integer = CInt(Int(m_tileAmountY * Rnd()))
                While Not m_tile(k, l).value = 0
                    k = CInt(Int(m_tileAmountX * Rnd()))
                    l = CInt(Int(m_tileAmountY * Rnd()))
                End While

                ' 2 or 4 (10% chance for a 4)
                Dim randN As Integer = CInt(Int(10 * Rnd()))
                If randN Mod 10 = 0 Then
                    m_tile(k, l).value = 4
                Else
                    m_tile(k, l).value = 2
                End If
                m_tile(k, l).UpdateValue()
                m_tile(k, l).CorrectTile()
            End If
        Else
            If m_game.generatingTimer = False Then
                If immediateTile = False Then
                    If m_newTileTimer.ElapsedMilliseconds > 0 Then
                        m_newTileTimer.Reset()
                        Randomize()
                        Dim k As Integer = CInt(Int(m_tileAmountX * Rnd()))
                        Dim l As Integer = CInt(Int(m_tileAmountY * Rnd()))
                        While Not m_tile(k, l).value = 0
                            k = CInt(Int(m_tileAmountX * Rnd()))
                            l = CInt(Int(m_tileAmountY * Rnd()))
                        End While

                        ' 2 or 4 (10% chance for a 4)
                        Dim randN As Integer = CInt(Int(2 * Rnd()))
                        If randN Mod 2 = 0 Then
                            m_tile(k, l).value = 2048
                        Else
                            m_tile(k, l).value = 1024
                        End If
                        m_tile(k, l).UpdateValue()
                        m_tile(k, l).CorrectTile()
                    End If
                    m_newTileTimer.Start()
                Else
                    m_newTileTimer.Reset()
                    Randomize()
                    Dim k As Integer = CInt(Int(m_tileAmountX * Rnd()))
                    Dim l As Integer = CInt(Int(m_tileAmountY * Rnd()))
                    While Not m_tile(k, l).value = 0
                        k = CInt(Int(m_tileAmountX * Rnd()))
                        l = CInt(Int(m_tileAmountY * Rnd()))
                    End While

                    ' 2 or 4 (10% chance for a 4)
                    Dim randN As Integer = CInt(Int(10 * Rnd()))
                    If randN Mod 10 = 0 Then
                        m_tile(k, l).value = 2048
                    Else
                        m_tile(k, l).value = 1024
                    End If
                    m_tile(k, l).UpdateValue()
                    m_tile(k, l).CorrectTile()
                End If
            Else
                'If we are working with a timer (opdracht 2)
                If immediateTile = False Then
                    If m_newTileTimer.ElapsedMilliseconds > 0 Then
                        m_newTileTimer.Reset()
                        Randomize()
                        Dim k As Integer = CInt(Int(m_tileAmountX * Rnd()))
                        Dim l As Integer = CInt(Int(m_tileAmountY * Rnd()))
                        While Not m_tile(k, l).value = 0
                            k = CInt(Int(m_tileAmountX * Rnd()))
                            l = CInt(Int(m_tileAmountY * Rnd()))
                        End While

                        ' 2 or 4 (10% chance for a 4)
                        Dim randN As Integer = CInt(Int(2 * Rnd()))
                        If randN Mod 2 = 0 Then
                            m_tile(k, l).value = 2048
                        Else
                            m_tile(k, l).value = 1024
                        End If
                        m_tile(k, l).UpdateValue()
                        m_tile(k, l).CorrectTile()
                    End If
                    m_newTileTimer.Start()
                Else
                    m_newTileTimer.Reset()
                    Randomize()
                    Dim k As Integer = CInt(Int(m_tileAmountX * Rnd()))
                    Dim l As Integer = CInt(Int(m_tileAmountY * Rnd()))
                    While Not m_tile(k, l).value = 0
                        k = CInt(Int(m_tileAmountX * Rnd()))
                        l = CInt(Int(m_tileAmountY * Rnd()))
                    End While

                    ' 2 or 4 (10% chance for a 4)
                    Dim randN As Integer = CInt(Int(10 * Rnd()))
                    If randN Mod 10 = 0 Then
                        m_tile(k, l).value = 2048
                    Else
                        m_tile(k, l).value = 1024
                    End If
                    m_tile(k, l).UpdateValue()
                    m_tile(k, l).CorrectTile()
                End If
            End If
        End If
    End Sub
    Public Sub ResetTimer()
        m_time = 5
    End Sub
    Private Sub TimerTick() Handles m_timer.Tick
        m_time -= 1
        m_game.timerLabel.Text = "Timer:" & Environment.NewLine & CStr(m_time)

        If m_time <= 0 Then
            GenerateNewTile()
            m_time = 5
        End If
    End Sub
    Public Property tile(i As Integer, j As Integer) As GridTile
        Get
            Return m_tile(i, j)
        End Get
        Private Set(ByVal value As GridTile)
            m_tile(i, j) = value
        End Set
    End Property
    Public Property roundnessCorners() As Integer
        Get
            Return m_roundnessCorners
        End Get
        Private Set(ByVal value As Integer)
            m_roundnessCorners = value
        End Set
    End Property
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
    Public Property tileAmountX() As Integer
        Get
            Return m_tileAmountX
        End Get
        Private Set(ByVal value As Integer)
            m_tileAmountX = value
        End Set
    End Property
    Public Property tileAmountY() As Integer
        Get
            Return m_tileAmountY
        End Get
        Private Set(ByVal value As Integer)
            m_tileAmountY = value
        End Set
    End Property
    Public Property game() As Game
        Get
            Return m_game
        End Get
        Private Set(ByVal value As Game)
            m_game = value
        End Set
    End Property
    'This function was found on the internet and just makes the labels with rounded edges
    <DllImport("Gdi32.dll", EntryPoint:="CreateRoundRectRgn")> _
    Private Shared Function CreateRoundRectRgn(ByVal iLeft As Integer, ByVal iTop As Integer, ByVal iRight As Integer, ByVal iBottom As Integer, ByVal iWidth As Integer, ByVal iHeight As Integer) As IntPtr

    End Function
End Class
