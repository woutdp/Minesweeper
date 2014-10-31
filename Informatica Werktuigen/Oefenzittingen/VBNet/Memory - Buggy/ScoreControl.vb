Public Class ScoreControl

    Private _data As IScoreData

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        SetStyle(ControlStyles.ResizeRedraw, True)
        SetStyle(ControlStyles.OptimizedDoubleBuffer, True)

    End Sub

    Private Sub Observe(data As IScoreData)
        If data IsNot Nothing Then
            AddHandler data.Updated, AddressOf UpdateVisuals
        End If
    End Sub

    Private Sub Unobserve(data As IScoreData)
        If data IsNot Nothing Then
            RemoveHandler data.Updated, AddressOf UpdateVisuals
        End If
    End Sub

    Public Property Data As IScoreData
        Get
            Return _data
        End Get
        Set(value As IScoreData)
            Unobserve(_data)
            _data = value
            Observe(value)

            UpdateVisuals()
        End Set
    End Property

    Private Sub UpdateVisuals()
        Invalidate()
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)

        If _data IsNot Nothing Then
            If _data.Active Then
                PaintActiveRectangle(e.Graphics)
            End If

            PaintScore(e.Graphics)
        End If
    End Sub

    Private Sub PaintActiveRectangle(g As Graphics)
        Dim rect = Me.ClientRectangle
        rect.Inflate(-2, -2)

        g.DrawRectangle(Pens.Black, rect)
    End Sub

    Private Sub PaintScore(g As Graphics)
        Dim format = New StringFormat()
        format.Alignment = StringAlignment.Center
        format.LineAlignment = StringAlignment.Center

        g.DrawString(_data.Score.ToString(), Me.Font, Brushes.Black, ComputeScoreRectangle(), format)
    End Sub

    Private Function ComputeScoreRectangle() As RectangleF
        Dim rect = Me.ClientRectangle
        rect.Inflate(-4, -4)

        Return rect
    End Function

End Class

Public Interface IScoreData
    ReadOnly Property Score As Integer

    ReadOnly Property Active As Boolean

    ReadOnly Property Name As String

    Event Updated()
End Interface

Public Class ScoreData
    Implements IScoreData

    Private ReadOnly _game As Game

    Private ReadOnly _playerIndex As Integer

    Public Sub New(game As Game, playerIndex As Integer)
        Debug.Assert(game IsNot Nothing)
        Debug.Assert(playerIndex < game.PlayerCount)

        _game = game
        _playerIndex = playerIndex

        AddHandler _game.GetPlayerScore(playerIndex).ValueChanged, AddressOf OnScoreChanged
        AddHandler _game.ActivePlayer.ValueChanged, AddressOf OnActivePlayerChanged
    End Sub

    Public ReadOnly Property Active As Boolean Implements IScoreData.Active
        Get
            Return _game.ActivePlayer.Value.HasValue AndAlso _game.ActivePlayer.Value.Value = _playerIndex
        End Get
    End Property

    Public ReadOnly Property Name As String Implements IScoreData.Name
        Get
            Return String.Format("Player {0}", _playerIndex + 1)
        End Get
    End Property

    Public ReadOnly Property Score As Integer Implements IScoreData.Score
        Get
            Return _game.GetPlayerScore(_playerIndex).Value
        End Get
    End Property

    Private Sub OnGameStateChanged()
        RaiseEvent Updated()
    End Sub

    Private Sub OnScoreChanged(oldValue As Integer, newValue As Integer)
        OnGameStateChanged()
    End Sub

    Private Sub OnActivePlayerChanged(oldValue As Integer?, newValue As Integer?)
        OnGameStateChanged()
    End Sub

    Public Event Updated() Implements IScoreData.Updated
End Class