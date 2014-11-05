Option Explicit On
Option Strict On

Imports System.Runtime.InteropServices

Public Class SizeForm
    Private m_maxSize As Integer = 10
    Private m_gridSize(m_maxSize - 1, m_maxSize - 1) As GridTile
    private m_game As Game
    Private m_lblX As Label
    Private m_lblY As Label

    Public Sub New(ByRef game As Game)
        m_game = game
        InitializeComponent()
    End Sub

    Private Sub SizeForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Initialize Grid
        Dim tileSize As Integer = 20 'Size of the tiles in x and y
        Dim spaceXBorder As Integer = 5 'How much from left border
        Dim spaceYBorder As Integer = 5 'How much from top border

        Dim gridWidth As Integer = (tileSize) * (m_maxSize) + spaceXBorder
        Dim gridHeight As Integer = (tileSize) * (m_maxSize + 2) + spaceYBorder * 4
        Me.Size = New Size(gridWidth + 100, gridHeight)

        For i As Integer = m_gridSize.GetLowerBound(0) To m_gridSize.GetUpperBound(0)
            For j As Integer = m_gridSize.GetLowerBound(1) To m_gridSize.GetUpperBound(1)
                m_gridSize(i, j) = New GridTile(New Label, i, j)
                m_gridSize(i, j).Size = New Size(tileSize, tileSize)
                m_gridSize(i, j).Name = "lblTile_" & i & "_" & j
                m_gridSize(i, j).Location = New Point(i * (tileSize) + spaceXBorder,
                                                j * (tileSize) + spaceYBorder)
                m_gridSize(i, j).BackColor = ColorTranslator.FromOle(RGB(205, 193, 180))
                m_gridSize(i, j).Text = ""
                m_gridSize(i, j).Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, m_gridSize(i, j).Width - 0, m_gridSize(i, j).Height - 0, 3, 3))

                If m_gridSize(i, j).widthPos = m_gridSize(i, j).heightPos Then
                    m_gridSize(i, j).BackColor = ColorTranslator.FromOle(RGB(220, 220, 180))
                End If
                AddHandler m_gridSize(i, j).Click, AddressOf lblTile_Click
                AddHandler m_gridSize(i, j).MouseEnter, AddressOf lblTile_Enter
                Me.Controls.Add(m_gridSize(i, j))
            Next
        Next

        m_lblX = New Label
        m_lblX.AutoSize = True
        m_lblX.TextAlign = ContentAlignment.MiddleCenter
        m_lblX.Font = New Font("Calibri", 10, FontStyle.Bold)
        m_lblX.Name = "lblX"
        m_lblX.Location = New Point(gridWidth, spaceYBorder)
        m_lblX.Text = "Width:" & ""
        m_lblX.ForeColor = Color.White
        Me.Controls.Add(m_lblX)

        m_lblY = New Label
        m_lblY.AutoSize = True
        m_lblY.TextAlign = ContentAlignment.MiddleCenter
        m_lblY.Font = New Font("Calibri", 10, FontStyle.Bold)
        m_lblY.Name = "lblX"
        m_lblY.Location = New Point(gridWidth, spaceYBorder + 20)
        m_lblY.Text = "Width:" & ""
        m_lblY.ForeColor = Color.White
        Me.Controls.Add(m_lblY)
    End Sub
    Private Sub lblTile_Click(sender As Object, e As EventArgs)
        Dim sentTile As GridTile = DirectCast(sender, GridTile)
        m_game.BoardResize(sentTile.widthPos + 1, sentTile.heightPos + 1)
        Me.Close()
    End Sub
    Private Sub lblTile_Enter(sender As Object, e As EventArgs)
        Dim sentTile As GridTile = DirectCast(sender, GridTile)
        ' Make all tiles empty
        For i As Integer = m_gridSize.GetLowerBound(0) To m_gridSize.GetUpperBound(0)
            For j As Integer = m_gridSize.GetLowerBound(1) To m_gridSize.GetUpperBound(1)
                If m_gridSize(i, j).widthPos = m_gridSize(i, j).heightPos Then
                    m_gridSize(i, j).BackColor = ColorTranslator.FromOle(RGB(220, 220, 180))
                Else
                    m_gridSize(i, j).BackColor = ColorTranslator.FromOle(RGB(205, 193, 180))
                End If
            Next
        Next
        ' Fill in all tiles that need to be selected
        For i As Integer = 0 To sentTile.widthPos
            For j As Integer = 0 To sentTile.heightPos
                m_gridSize(i, j).BackColor = Color.White
            Next
        Next
        ' Make the selected tile a different colour
        sentTile.BackColor = Color.Wheat

        m_lblX.Text = "Width:  " & CStr(sentTile.widthPos + 1)
        m_lblY.Text = "Height: " & CStr(sentTile.heightPos + 1)
    End Sub
    'This function was found on the internet and just makes the labels with rounded edges
    <DllImport("Gdi32.dll", EntryPoint:="CreateRoundRectRgn")> _
    Private Shared Function CreateRoundRectRgn(ByVal iLeft As Integer, ByVal iTop As Integer, ByVal iRight As Integer, ByVal iBottom As Integer, ByVal iWidth As Integer, ByVal iHeight As Integer) As IntPtr

    End Function
End Class