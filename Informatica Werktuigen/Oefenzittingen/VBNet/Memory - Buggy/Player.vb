Public MustInherit Class Player
    Private _game As Game

    Private _playerIndex As Integer

    Protected Sub New(game As Game, playerIndex As Integer)
        Debug.Assert(game IsNot Nothing)

        _game = game
        _playerIndex = playerIndex
    End Sub

    Protected ReadOnly Property Game As Game
        Get
            Return _game
        End Get
    End Property

    Protected ReadOnly Property PlayerIndex As Integer
        Get
            Return _playerIndex
        End Get
    End Property

    Public MustOverride Sub PickFromUI(pos As Position)

    Public MustOverride Sub Tick(delta As Double)
End Class

Public Class HumanPlayer
    Inherits Player

    Public Shared ReadOnly Property Constructor As IPlayerConstructor
        Get
            Return New FunctionPlayerConstructor(Function(g, i) New HumanPlayer(g, i))
        End Get
    End Property

    Public Sub New(game As Game, playerIndex As Integer)
        MyBase.New(game, playerIndex)
    End Sub

    Public Overrides Sub PickFromUI(pos As Position)
        Debug.Assert(pos IsNot Nothing)

        Me.Game.PickTile(pos)
    End Sub

    Public Overrides Sub Tick(delta As Double)
        ' NOP
    End Sub
End Class

Public Class ArtificialPlayer
    Inherits Player

    Private ReadOnly _ai As ArtificialIntelligence

    Private _state As State

    Public Shared Function Constructor(ai As ArtificialIntelligence) As IPlayerConstructor
        Return New FunctionPlayerConstructor(Function(g, i) New ArtificialPlayer(g, i, ai))
    End Function

    Public Sub New(game As Game, playerIndex As Integer, ai As ArtificialIntelligence)
        MyBase.New(game, playerIndex)

        Debug.Assert(ai IsNot Nothing)

        _ai = ai
        _state = New IdleState(Me)
        AddHandler game.GameState.ValueChanged, AddressOf OnGameStateUpdate

    End Sub

    Public Overrides Sub PickFromUI(pos As Position)
        ' NOP
    End Sub

    Public Overrides Sub Tick(delta As Double)
        _state.Tick(delta)
    End Sub

    Private Sub OnGameStateUpdate(oldValue As GameState, newValue As GameState)
        _state.OnGameStateUpdate()
    End Sub

    Private Sub SetState(state As State)
        _state = state
    End Sub

    Private MustInherit Class State
        Private ReadOnly _parent As ArtificialPlayer

        Protected Sub New(parent As ArtificialPlayer)
            Debug.Assert(parent IsNot Nothing)

            _parent = parent
        End Sub

        Public MustOverride Sub Tick(delta As Double)

        Public MustOverride Sub OnGameStateUpdate()

        Protected ReadOnly Property Parent As ArtificialPlayer
            Get
                Return _parent
            End Get
        End Property
    End Class

    Private Class IdleState
        Inherits State

        Public Sub New(parent As ArtificialPlayer)
            MyBase.New(parent)
        End Sub

        Public Overrides Sub OnGameStateUpdate()
            If Me.Parent.Game.GameState.Value = GameState.WaitingForFirstPick AndAlso Me.Parent.Game.ActivePlayer.Value = Me.Parent.PlayerIndex Then
                Dim picks = Me.Parent._ai.MakePicks()

                Parent.SetState(New DelayingFirstPickState(Me.Parent, picks.Item1, picks.Item2))
            End If
        End Sub

        Public Overrides Sub Tick(delta As Double)
            ' NOP
        End Sub
    End Class

    Private MustInherit Class DelayingState
        Inherits State

        Private _timeLeft As Double

        Public Sub New(parent As ArtificialPlayer)
            MyBase.New(parent)

            _timeLeft = Configuration.AIDelay
        End Sub

        Public Overrides Sub OnGameStateUpdate()
            ' NOP
        End Sub

        Public Overrides Sub Tick(delta As Double)
            _timeLeft = _timeLeft - delta

            If _timeLeft <= 0 Then
                TimeUp()
            End If
        End Sub

        Protected MustOverride Sub TimeUp()
    End Class

    Private Class DelayingFirstPickState
        Inherits DelayingState

        Private _firstPick As Position

        Private _secondPick As Position

        Public Sub New(parent As ArtificialPlayer, firstPick As Position, secondPick As Position)
            MyBase.New(parent)

            Debug.Assert(firstPick IsNot Nothing)
            Debug.Assert(secondPick IsNot Nothing)

            _firstPick = firstPick
            _secondPick = secondPick
        End Sub

        Protected Overrides Sub TimeUp()
            Debug.Assert(Me.Parent.Game.ActivePlayer.Value.Value = Me.Parent.PlayerIndex)
            Debug.Assert(Me.Parent.Game.GameState.Value = GameState.WaitingForFirstPick)

            ' POI: Changing order will fail
            Me.Parent.SetState(New DelayingSecondPickState(Me.Parent, _secondPick))
            Me.Parent.Game.PickTile(_firstPick)
        End Sub
    End Class

    Private Class DelayingSecondPickState
        Inherits DelayingState

        Private _secondPick As Position

        Public Sub New(parent As ArtificialPlayer, secondPick As Position)
            MyBase.New(parent)

            Debug.Assert(secondPick IsNot Nothing)

            _secondPick = secondPick
        End Sub

        Protected Overrides Sub TimeUp()
            Debug.Assert(Me.Parent.Game.ActivePlayer.Value.Value = Me.Parent.PlayerIndex)
            Debug.Assert(Me.Parent.Game.GameState.Value = GameState.WaitingForSecondPick)

            ' POI: does it work if AI is sole player?
            Me.Parent.SetState(New IdleState(Me.Parent))
            Me.Parent.Game.PickTile(_secondPick)
        End Sub
    End Class
End Class

Public Interface IPlayerConstructor
    Function Create(game As Game, index As Integer) As Player
End Interface

Public Class FunctionPlayerConstructor
    Implements IPlayerConstructor

    Private _factory As Func(Of Game, Integer, Player)

    Public Sub New(factory As Func(Of Game, Integer, Player))
        Debug.Assert(factory IsNot Nothing)

        _factory = factory
    End Sub

    Public Function Create(game As Game, index As Integer) As Player Implements IPlayerConstructor.Create
        Return _factory(game, index)
    End Function
End Class
