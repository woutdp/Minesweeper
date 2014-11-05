Option Explicit On
Option Strict On

Imports System.Runtime.InteropServices

Public Class Game
    Private m_menuWidth As Integer = 140
    Private m_spaceXBorder As Integer = 30
    Private m_spaceYBorder As Integer = 30
    Private m_grid As Grid

    Private m_lblScore As Label
    Private m_lblHighScore As Label
    Private m_score As Integer = 0
    Private m_highScore As Integer = 0

    Private WithEvents m_btnResize As Button
    Private WithEvents m_btnRestart As Button
    Private WithEvents m_btnJoker As Button
    Private m_jokersAvailable As Integer = 1
    Private m_bJokerActive As Boolean = False

    Private timer As Stopwatch
    Private interval As Long
    Private startTick As Long

    Public Sub New()
        m_grid = New Grid(Me, 4, 4)
        InitializeComponent()
    End Sub

    Public Sub New(Optional width As Integer = 4, Optional height As Integer = 4, Optional highscore As Integer = 0)
        m_grid = New Grid(Me, width, height)
        m_highScore = highscore
        InitializeComponent()
    End Sub
    Private Sub Main() Handles Me.Load
        'Change the size of the form
        timer = New Stopwatch()
        interval = 16

        Dim tempHeight As Integer
        If m_grid.gridHeight + m_spaceYBorder * 2 + 50 < 410 Then
            tempHeight = 410
            Size = New Size(m_grid.gridWidth + m_menuWidth + spaceXBorder * 2, tempHeight)
        Else
            tempHeight = m_grid.gridHeight + m_spaceYBorder * 2 + 50
            Size = New Size(m_grid.gridWidth + m_menuWidth + spaceXBorder * 2, tempHeight)
        End If

        'Initialize m_lblScore
        m_lblScore = New Label
        m_lblScore.AutoSize = True
        m_lblScore.TextAlign = ContentAlignment.MiddleCenter
        m_lblScore.Font = New Font("Calibri", 20, FontStyle.Bold)
        m_lblScore.Name = "lblScore"
        m_lblScore.Location = New Point(m_grid.gridWidth + spaceXBorder * 2, m_spaceYBorder)
        m_lblScore.Text = "Score:" & Environment.NewLine & CStr(m_score)
        m_lblScore.ForeColor = ColorTranslator.FromOle(RGB(187, 173, 160))
        Me.Controls.Add(m_lblScore)

        'Initialize highscore
        m_lblHighScore = New Label
        m_lblHighScore.AutoSize = True
        m_lblHighScore.TextAlign = ContentAlignment.MiddleCenter
        m_lblHighScore.Font = New Font("Calibri", 12, FontStyle.Bold)
        m_lblHighScore.Name = "lblHighScore"
        m_lblHighScore.Location = New Point(m_grid.gridWidth + m_spaceXBorder * 2, m_spaceYBorder + 100)
        m_lblHighScore.Text = "Highscore:" & Environment.NewLine & CStr(m_highScore)
        m_lblHighScore.ForeColor = ColorTranslator.FromOle(RGB(187, 173, 160))
        Me.Controls.Add(m_lblHighScore)

        'Initialize m_btnResize
        m_btnResize = New Button
        m_btnResize.Size = New Size(100, 40)
        m_btnResize.TextAlign = ContentAlignment.MiddleCenter
        m_btnResize.Font = New Font("Calibri", 12, FontStyle.Regular)
        m_btnResize.Name = "btnResize"
        m_btnResize.Location = New Point(m_grid.gridWidth + m_spaceXBorder * 2, tempHeight - 220)
        m_btnResize.Text = "Resize"
        m_btnResize.BackColor = ColorTranslator.FromOle(RGB(143, 122, 102))
        m_btnResize.ForeColor = Color.White
        m_btnResize.FlatStyle = FlatStyle.Flat
        m_btnResize.FlatAppearance.BorderSize = 0
        Me.Controls.Add(m_btnResize)

        'Initialize m_btnJoker
        m_btnJoker = New Button
        m_btnJoker.Size = New Size(100, 40)
        m_btnJoker.TextAlign = ContentAlignment.MiddleCenter
        m_btnJoker.Font = New Font("Calibri", 12, FontStyle.Regular)
        m_btnJoker.Name = "btnJoker"
        m_btnJoker.Location = New Point(m_grid.gridWidth + m_spaceXBorder * 2, tempHeight - 170)
        m_btnJoker.Text = "Joker"
        m_btnJoker.BackColor = ColorTranslator.FromOle(RGB(143, 122, 102))
        m_btnJoker.ForeColor = Color.White
        m_btnJoker.FlatStyle = FlatStyle.Flat
        m_btnJoker.FlatAppearance.BorderSize = 0
        Me.Controls.Add(m_btnJoker)

        'Initialize m_btnRestart
        m_btnRestart = New Button
        m_btnRestart.Size = New Size(100, 40)
        m_btnRestart.TextAlign = ContentAlignment.MiddleCenter
        m_btnRestart.Font = New Font("Calibri", 12, FontStyle.Regular)
        m_btnRestart.Name = "btnRestart"
        m_btnRestart.Location = New Point(m_grid.gridWidth + m_spaceXBorder * 2, tempHeight - 120)
        m_btnRestart.Text = "Restart"
        m_btnRestart.BackColor = ColorTranslator.FromOle(RGB(143, 122, 102))
        m_btnRestart.ForeColor = Color.White
        m_btnRestart.FlatStyle = FlatStyle.Flat
        m_btnRestart.FlatAppearance.BorderSize = 0
        Me.Controls.Add(m_btnRestart)

        'Initialize the board; set on true for testing all the tiles
        m_grid.InitializeBoard()
    End Sub
    Private Sub Game_Shown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown
        GameLoop()
    End Sub
    Private Sub GameLoop()
        timer.Start()
        Do While (Me.Created)
            startTick = timer.ElapsedMilliseconds
            GameUpdate()
            Application.DoEvents()
            Do While timer.ElapsedMilliseconds - startTick < interval

            Loop
        Loop
    End Sub
    Private Sub GameUpdate()
        m_grid.GameUpdate(interval)
    End Sub
    Private Sub Game_KeyUp(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Dim keyCode As Keys = e.KeyCode
        Select Case keyCode
            Case Keys.Up, Keys.Down, Keys.Left, Keys.Right
                m_grid.MoveTiles(keyCode)
            Case Keys.R
                m_grid.InitializeBoard()
        End Select

        CorrectGame(False)
        m_grid.CheckGameOver()
    End Sub
    Public Sub AddScore(amount As Integer)
        m_score = m_score + amount
        m_lblScore.Text = "Score:" & Environment.NewLine & CStr(m_score)
    End Sub
    Public Sub SetScore(amount As Integer)
        m_score = amount
        m_lblScore.Text = "Score:" & Environment.NewLine & CStr(m_score)
    End Sub
    Public Sub BoardResize(width As Integer, height As Integer)
        Dim new_game As Game = New Game(width, height, m_highScore)
        new_game.Show()
        Me.Close()
    End Sub
    Public Sub CorrectGame(Optional gridToo As Boolean = True)
        'Correct the grid
        If gridToo = True Then m_grid.CorrectGrid()

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
    Private Sub btnResize_Click(sender As Object, e As EventArgs) Handles m_btnResize.Click
        Dim sizeForm As SizeForm = New SizeForm(Me)
        sizeForm.Show()
    End Sub
    Private Sub btnJoker_Click(sender As Object, e As EventArgs) Handles m_btnJoker.Click
        If m_jokersAvailable > 0 Then
            m_jokersAvailable = m_jokersAvailable - 1
            m_bJokerActive = True
            MsgBox("Click on a square to delete it. Use it wisely!", , "2048 - Joker")
        Else
            MsgBox("No jokers left!")
        End If
        CorrectGame()
    End Sub
    Private Sub btnRestart_Click(sender As Object, e As EventArgs) Handles m_btnRestart.Click
        m_grid.InitializeBoard()
    End Sub
    Public Property jokerActive() As Boolean
        Get
            Return m_bJokerActive
        End Get
        Set(ByVal value As Boolean)
            m_bJokerActive = value
        End Set
    End Property
    Public Property jokersAvailable() As Integer
        Get
            Return m_jokersAvailable
        End Get
        Set(ByVal value As Integer)
            m_jokersAvailable = value
        End Set
    End Property
    Public Property spaceXBorder() As Integer
        Get
            Return m_spaceXBorder
        End Get
        Private Set(ByVal value As Integer)
            m_spaceXBorder = value
        End Set
    End Property
    Public Property spaceYBorder() As Integer
        Get
            Return m_spaceYBorder
        End Get
        Private Set(ByVal value As Integer)
            m_spaceYBorder = value
        End Set
    End Property
    Public Property score() As Integer
        Get
            Return m_score
        End Get
        Private Set(ByVal value As Integer)
            m_score = value
        End Set
    End Property
    'This function was found on the internet and just makes the labels with rounded edges
    <DllImport("Gdi32.dll", EntryPoint:="CreateRoundRectRgn")> _
    Private Shared Function CreateRoundRectRgn(ByVal iLeft As Integer, ByVal iTop As Integer, ByVal iRight As Integer, ByVal iBottom As Integer, ByVal iWidth As Integer, ByVal iHeight As Integer) As IntPtr

    End Function
End Class