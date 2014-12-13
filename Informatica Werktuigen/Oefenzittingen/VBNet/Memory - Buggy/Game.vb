﻿Public Class Game

    Private _grid As GameGrid

    Private _scores As List(Of ObservableVariable(Of Integer))

    Private _activePlayer As ObservableVariable(Of Integer?)

    Private _state As ObservableVariable(Of State)

    Private _gameState As IObservable(Of GameState)

    Public Sub New(grid As GameGrid, playerCount As Integer)
        Debug.Assert(grid IsNot Nothing)
        Debug.Assert(playerCount > 0)

        _grid = grid
        _scores = New List(Of ObservableVariable(Of Integer))

        For i = 0 To playerCount - 1
            _scores.Add(New ObservableVariable(Of Integer)(0))
        Next

        _activePlayer = New ObservableVariable(Of Integer?)(0)
        _state = New ObservableVariable(Of State)(New InitializationState(Me))
        _gameState = New DerivedObservable(Of State, GameState)(_state, Function(s) s.GameState)
    End Sub

    Public Sub StartGame()
        SetState(New WaitingForFirstPickState(Me))
    End Sub

    Public ReadOnly Property GameState As IObservable(Of GameState)
        Get
            Return _gameState
        End Get
    End Property

    Public Function GetPlayersWithHighestScore() As IEnumerable(Of Integer)
        Dim result = New List(Of Integer)
        Dim highestScore = (From score In _scores Select score.Value).Max()

        Return From i In Enumerable.Range(0, PlayerCount)
               Where _scores(i).Value = highestScore
               Select i
    End Function

    Public Sub PickTile(pos As Position)
        _state.Value.PickTile(pos)
    End Sub

    Public Sub Tick(delta As Double)
        _state.Value.Tick(delta)
    End Sub

    Public ReadOnly Property Grid As GameGrid
        Get
            Return _grid
        End Get
    End Property

    Public Function GetPlayerScore(i As Integer) As IObservable(Of Integer)
        Return _scores(i)
    End Function

    Public ReadOnly Property ActivePlayer As IObservable(Of Integer?)
        Get
            Return _activePlayer
        End Get
    End Property

    Private Sub SetState(newState As State)
        _state.SetValue(newState)
    End Sub

    Private Sub IncrementScoreOfActivePlayer()
        _scores(ActivePlayer.Value.Value).SetValue(_scores(ActivePlayer.Value.Value).Value + 1)
    End Sub

    Public ReadOnly Property PlayerCount As Integer
        Get
            Return _scores.Count
        End Get
    End Property

    Private Sub NextPlayer()
        Dim current = _activePlayer.Value
        Dim nextPlayer = (current + 1) Mod PlayerCount

        _activePlayer.SetValue(nextPlayer)
    End Sub

    Private Sub SetNoActivePlayer()
        _activePlayer.SetValue(Nothing)
    End Sub

    Private MustInherit Class State
        Protected _parent As Game

        Protected Sub New(parent As Game)
            Debug.Assert(parent IsNot Nothing)

            _parent = parent
        End Sub

        Public ReadOnly Property Parent As Game
            Get
                Return _parent
            End Get
        End Property

        Public MustOverride Sub PickTile(pos As Position)

        Public MustOverride Sub Tick(delta As Double)

        Public MustOverride ReadOnly Property GameState As GameState
    End Class

    Private Class InitializationState
        Inherits State

        Public Sub New(parent As Game)
            MyBase.New(parent)
        End Sub

        Public Overrides ReadOnly Property GameState As GameState
            Get
                Return GameState.Initialization
            End Get
        End Property

        Public Overrides Sub PickTile(pos As Position)
            ' NOP
        End Sub

        Public Overrides Sub Tick(delta As Double)
            ' NOP
        End Sub
    End Class

    Private Class WaitingForFirstPickState
        Inherits State

        Public Sub New(parent As Game)
            MyBase.New(parent)
        End Sub

        Public Overrides Sub PickTile(firstPick As Position)
            Debug.Assert(firstPick IsNot Nothing)

            If Not _parent.Grid(firstPick).Shown.Value Then
                _parent.Grid(firstPick).Shown.SetValue(True)
                _parent.SetState(New WaitingForSecondPickState(_parent, firstPick))
            End If
        End Sub

        Public Overrides Sub Tick(delta As Double)
            ' NOP
        End Sub

        Public Overrides ReadOnly Property GameState As GameState
            Get
                Return GameState.WaitingForFirstPick
            End Get
        End Property
    End Class

    Private Class WaitingForSecondPickState
        Inherits State

        Private ReadOnly _firstPick As Position

        Public Sub New(parent As Game, firstPick As Position)
            MyBase.New(parent)
            _firstPick = firstPick
        End Sub

        Public Overrides Sub PickTile(secondPick As Position)
            Debug.Assert(secondPick IsNot Nothing)

            If Not _parent.Grid(secondPick).Shown.Value Then
                _parent.Grid(secondPick).Shown.SetValue(True)

                If _parent.Grid(_firstPick).Contents = _parent.Grid(secondPick).Contents Then
                    _parent.Grid(_firstPick).Paired.SetValue(True)
                    _parent.Grid(secondPick).Paired.SetValue(True)

                    _parent.IncrementScoreOfActivePlayer()

                    If _parent.Grid.AllTilesPaired Then
                        _parent.SetState(New WaitingForFirstPickState(_parent))
                    Else
                        _parent.SetState(New WaitingForFirstPickState(_parent))
                    End If
                Else
                    _parent.NextPlayer()
                    _parent.SetState(New LingeringState(_parent, _firstPick, secondPick))
                End If
            End If
        End Sub

        Public Overrides Sub Tick(delta As Double)
            ' NOP
        End Sub

        Public Overrides ReadOnly Property GameState As GameState
            Get
                Return GameState.WaitingForSecondPick
            End Get
        End Property
    End Class

    Private Class LingeringState
        Inherits State

        Private ReadOnly _firstPick As Position

        Private ReadOnly _secondPick As Position

        Private _timeLeft As Double

        Public Sub New(parent As Game, firstPick As Position, secondPick As Position)
            MyBase.New(parent)

            Debug.Assert(firstPick IsNot Nothing)
            Debug.Assert(secondPick IsNot Nothing)

            _timeLeft = Configuration.LingerDuration
            _firstPick = firstPick
            _secondPick = secondPick
        End Sub

        Public Overrides Sub PickTile(pos As Position)
            HideTiles()

            _parent.SetState(New WaitingForFirstPickState(_parent))
            _parent.PickTile(pos)
        End Sub

        Public Overrides Sub Tick(delta As Double)
            _timeLeft = _timeLeft - delta

            If _timeLeft <= 0 Then
                HideTiles()

                _parent.SetState(New WaitingForFirstPickState(_parent))
            End If
        End Sub

        Private Sub HideTiles()
            _parent.Grid(_firstPick).Shown.SetValue(False)
            _parent.Grid(_secondPick).Shown.SetValue(False)
        End Sub

        Public Overrides ReadOnly Property GameState As GameState
            Get
                Return GameState.Lingering
            End Get
        End Property
    End Class

    Private Class EndedState
        Inherits State

        Public Sub New(parent As Game)
            MyBase.New(parent)

            _parent.SetNoActivePlayer()
        End Sub

        Public Overrides Sub PickTile(pos As Position)
            ' NOP
        End Sub

        Public Overrides Sub Tick(delta As Double)
            ' NOP
        End Sub

        Public Overrides ReadOnly Property GameState As GameState
            Get
                Return GameState.Ended
            End Get
        End Property
    End Class

End Class

Public Enum GameState
    Initialization
    WaitingForFirstPick
    WaitingForSecondPick
    Lingering
    Ended
End Enum