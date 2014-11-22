Option Explicit On
Option Strict On

Imports System.Runtime.InteropServices
Public Class GridTile
    Inherits Label
    Private m_widthPos As Integer
    Private m_heightPos As Integer
    Private m_x As Double
    Private m_y As Double
    Private m_grid As Grid
    Private m_isJustMerged As Boolean = False
    Private m_goToTile As GridTile
    Private m_animTime As Double = 0.1
    Private m_currentTime As Double = 0
    Private m_moving As Boolean = False
    Private m_animTile As GridTile
    Private m_value As Integer = 0
    Public Sub New(i As Integer, j As Integer, Optional ByRef grid As Grid = Nothing)
        m_widthPos = i
        m_heightPos = j
        m_grid = grid
    End Sub
    Public Function MoveTile(direction As Keys) As Boolean
        'If the tile is empty return false
        If m_value = 0 Then Return False
        ' do the necesarry checks to see if a tile can move, if not, return False
        Dim checkTile As GridTile = Nothing
        Select Case direction
            Case Keys.Up
                If m_heightPos = 0 Then Return False
                checkTile = m_grid.tile(m_widthPos, m_heightPos - 1)
            Case Keys.Down
                If m_heightPos = m_grid.tileAmountY - 1 Then Return False
                checkTile = m_grid.tile(m_widthPos, m_heightPos + 1)
            Case Keys.Left
                If m_widthPos = 0 Then Return False
                checkTile = m_grid.tile(m_widthPos - 1, m_heightPos)
            Case Keys.Right
                If m_widthPos = m_grid.tileAmountX - 1 Then Return False
                checkTile = m_grid.tile(m_widthPos + 1, m_heightPos)
        End Select

        'If it can move, return true
        'If the value of the going to tile is 0
        If checkTile.value = 0 Then
            Dim theEnd As Boolean = False
            While Not theEnd
                Select Case direction
                    Case Keys.Up
                        If (checkTile.value > 0 And checkTile.value <> m_value) Or (checkTile.value = m_value And checkTile.isJustMerged) Then
                            theEnd = True ' if value is higher than 0 and value is not equal then 
                            checkTile = checkTile.DownOf()
                        End If
                        If checkTile.heightPos = 0 Or Not checkTile.value = 0 Then theEnd = True ' IF at the top OR the value is not 0 THEN merge with that tile
                        If theEnd = False Then checkTile = checkTile.UpOf()
                    Case Keys.Down
                        If (checkTile.value <> 0 And checkTile.value <> m_value) Or (checkTile.value = m_value And checkTile.isJustMerged) Then
                            theEnd = True ' if value is higher than 0 and value is not equal then 
                            checkTile = checkTile.UpOf()
                        End If
                        If m_grid.tileAmountY <= checkTile.heightPos + 1 Or Not checkTile.value = 0 Then theEnd = True
                        If theEnd = False Then checkTile = checkTile.DownOf()
                    Case Keys.Left
                        If (checkTile.value <> 0 And checkTile.value <> m_value) Or (checkTile.value = m_value And checkTile.isJustMerged) Then
                            theEnd = True ' if value is higher than 0 and value is not equal then 
                            checkTile = checkTile.RightOf()
                        End If
                        If checkTile.widthPos = 0 Or Not checkTile.value = 0 Then theEnd = True
                        If theEnd = False Then checkTile = checkTile.LeftOf()
                    Case Keys.Right
                        If (checkTile.value <> 0 And checkTile.value <> m_value) Or (checkTile.value = m_value And checkTile.isJustMerged) Then
                            theEnd = True ' if value is higher than 0 and value is not equal then 
                            checkTile = checkTile.LeftOf()
                        End If
                        If m_grid.tileAmountX <= checkTile.widthPos + 1 Or Not checkTile.value = 0 Then theEnd = True
                        If theEnd = False Then checkTile = checkTile.RightOf()
                End Select
            End While
            If Not checkTile.value = 0 And checkTile.isJustMerged() = False And isJustMerged = False And m_value = checkTile.value Then
                'Merging of tiles
                checkTile.value = m_value + checkTile.value
                m_grid.game.AddScore(checkTile.value)

                checkTile.isJustMerged() = True
                PhysicalMove(checkTile)
                Return True
            ElseIf checkTile.value = 0 Then
                checkTile.value = m_value
                UpdateValue()
                PhysicalMove(checkTile)
                Return True
            End If

        'If the value is equal
        ElseIf checkTile.value = m_value And checkTile.isJustMerged() = False And isJustMerged = False Then
            'Merging of tiles
            checkTile.value = m_value + checkTile.value
            m_grid.game.AddScore(checkTile.value)

            checkTile.isJustMerged() = True
            PhysicalMove(checkTile)
            Return True
        End If
        Return False
    End Function
    Private Function UpOf() As GridTile
        Return m_grid.tile(m_widthPos, m_heightPos - 1)
    End Function
    Private Function DownOf() As GridTile
        Return m_grid.tile(m_widthPos, m_heightPos + 1)
    End Function
    Private Function LeftOf() As GridTile
        Return m_grid.tile(m_widthPos - 1, m_heightPos)
    End Function
    Private Function RightOf() As GridTile
        Return m_grid.tile(m_widthPos + 1, m_heightPos)
    End Function
    Private Sub PhysicalMove(toTile As GridTile)
        EndAnimation()
        If m_animTile Is Nothing Then
            'create a new animationtile
            m_animTile = New GridTile(m_widthPos, m_heightPos)
            m_animTile.Size = New Size(Width, Height)
            m_animTile.Font = Font
            m_animTile.TextAlign = ContentAlignment.MiddleCenter
            m_animTile.ForeColor = ForeColor
            m_animTile.Name = "lblTile_" & m_widthPos & "a_" & m_heightPos & "a"
            m_animTile.SetBounds(Location.X, Location.Y, Width, Height)
            m_animTile.BackColor = BackColor
            m_animTile.value = m_value
            m_animTile.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, m_animTile.Width, m_animTile.Height, m_grid.roundnessCorners, m_grid.roundnessCorners))
            m_grid.game.Controls.Add(m_animTile)
            m_grid.game.Controls.SetChildIndex(m_animTile, 1)
            m_animTile.UpdateValue()
            m_animTile.CorrectTile()

            m_value = 0
            UpdateValue()
            CorrectTile()

            m_x = Location.X
            m_y = Location.Y
            m_goToTile = toTile
            m_grid.game.Controls.SetChildIndex(m_goToTile, m_grid.tileAmountX * m_grid.tileAmountY)
            m_goToTile.SendToBack()
            m_grid.SendBackgroundToBack()
            m_moving = True
        End If
    End Sub
    Private Sub EndAnimation()
        m_moving = False
        m_currentTime = 0

        m_grid.game.Controls.Remove(m_animTile)
        m_animTile = Nothing

        If m_goToTile IsNot Nothing Then
            m_goToTile.UpdateValue()
            m_goToTile.CorrectTile()
            m_goToTile = Nothing
        End If
    End Sub
    Public Sub GameUpdate(deltaTime As Long)
        If m_moving Then
            m_currentTime += deltaTime / 1000
            m_x = ((m_goToTile.Location.X - m_x) / (m_animTime - m_currentTime)) * (deltaTime / 1000) + m_x
            m_y = ((m_goToTile.Location.Y - m_y) / (m_animTime - m_currentTime)) * (deltaTime / 1000) + m_y
            m_animTile.SetBounds(CInt(m_x), CInt(m_y), Width, Height)
            If m_currentTime >= m_animTime Then
                EndAnimation()
            End If
        End If
    End Sub
    Public Sub CorrectTile()
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

        'Correct the colors
        Select Case m_value
            Case 0
                BackColor = colNothing
            Case 2
                BackColor = col2
            Case 4
                BackColor = col4
            Case 8
                BackColor = col8
            Case 16
                BackColor = col16
            Case 32
                BackColor = col32
            Case 64
                BackColor = col64
            Case 128
                BackColor = col128
            Case 256
                BackColor = col256
            Case 512
                BackColor = col512
            Case 1024
                BackColor = col1024
            Case 2048
                BackColor = col2048
            Case 4096
                BackColor = col4096
            Case 8192
                BackColor = col8192
            Case 16384
                BackColor = col16384
            Case 32768
                BackColor = col32768
            Case Else
                BackColor = colInvalid
        End Select
        If Not value = 0 Then
            If value > 7 Then
                ForeColor = ColorTranslator.FromOle(RGB(249, 246, 242))
            Else
                ForeColor = ColorTranslator.FromOle(RGB(119, 110, 101))
            End If
        End If

        'Make the size of the text fit
        If Not value = 0 Then
            If value > 9999999 Then
                Font = New Font("Calibri", 11, FontStyle.Bold)
            ElseIf value > 99999 Then
                Font = New Font("Calibri", 13, FontStyle.Bold)
            ElseIf value > 9999 Then
                Font = New Font("Calibri", 17, FontStyle.Bold)
            ElseIf value > 999 Then
                Font = New Font("Calibri", 20, FontStyle.Bold)
            ElseIf value > 99 Then
                Font = New Font("Calibri", 24, FontStyle.Bold)
            ElseIf value > 0 Then
                Font = New Font("Calibri", 26, FontStyle.Bold)
            End If
        End If
        m_isJustMerged = False
    End Sub
    Public Property value() As Integer
        Get
            Return m_value
        End Get
        Set(ByVal value As Integer)
            m_value = value
        End Set
    End Property
    Public Sub UpdateValue()
        If m_value = 0 Then
            Text = ""
        Else
            Text = CStr(m_value)
        End If
    End Sub
    Public Property widthPos() As Integer
        Get
            Return m_widthPos
        End Get
        Private Set(ByVal value As Integer)
            m_widthPos = value
        End Set
    End Property
    Public Property heightPos() As Integer
        Get
            Return m_heightPos
        End Get
        Private Set(ByVal value As Integer)
            m_heightPos = value
        End Set
    End Property
    Public Property isJustMerged() As Boolean
        Get
            Return m_isJustMerged
        End Get
        Set(ByVal value As Boolean)
            m_isJustMerged = value
        End Set
    End Property
    'This function was found on the internet and just makes the labels with rounded edges
    <DllImport("Gdi32.dll", EntryPoint:="CreateRoundRectRgn")> _
    Private Shared Function CreateRoundRectRgn(ByVal iLeft As Integer, ByVal iTop As Integer, ByVal iRight As Integer, ByVal iBottom As Integer, ByVal iWidth As Integer, ByVal iHeight As Integer) As IntPtr

    End Function
End Class
