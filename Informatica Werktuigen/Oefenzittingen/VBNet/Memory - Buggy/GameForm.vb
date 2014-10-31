Public Class GameForm
    Private _lastTick As DateTime

    Private _data As ObservableVariable(Of Data)

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _data = New ObservableVariable(Of Data)(Nothing)
        _lastTick = DateTime.Now        
        Me.BackgroundImage = Configuration.BackgroundLogo        

        AddHandler _data.ValueChanged, AddressOf OnNewGameData

    End Sub

    Private Sub OnNewGameData(oldValue As Data, newValue As Data)
        Unlink()
        Link(newValue.Game)
    End Sub

    Private Sub InitializeNewGame(game As Game, players As List(Of Player))
        Debug.Assert(game IsNot Nothing)
        Debug.Assert(players IsNot Nothing)

        _data.SetValue(New Data(game, players))
        game.StartGame()
    End Sub

    Private ReadOnly Property CurrentGame As Game
        Get
            If _data.Value IsNot Nothing Then
                Return _data.Value.Game
            Else
                Return Nothing
            End If
        End Get
    End Property

    Public ReadOnly Property CurrentPlayerList As IList(Of Player)
        Get
            If _data.Value IsNot Nothing Then
                Return _data.Value.Players
            Else
                Return Nothing
            End If
        End Get
    End Property

    Private Sub OnTileClicked(control As TileControl)
        Dim pos = control.Tile.Position

        If ActivePlayer IsNot Nothing Then
            ActivePlayer.PickFromUI(control.Tile.Position)
        End If
    End Sub

    Private ReadOnly Property ActivePlayer As Player
        Get
            If CurrentGame.ActivePlayer.Value.HasValue Then
                Return CurrentPlayerList(CurrentGame.ActivePlayer.Value.Value)
            Else
                Return Nothing
            End If
        End Get
    End Property

    Private Sub _timer_Tick(sender As Object, e As EventArgs) Handles _timer.Tick
        If CurrentGame IsNot Nothing Then
            Dim now = DateTime.Now
            Dim delta = (now - _lastTick).TotalMilliseconds / 1000.0

            CurrentGame.Tick(delta)
            If ActivePlayer IsNot Nothing Then
                ActivePlayer.Tick(delta)
            End If

            _lastTick = now
        End If
    End Sub

    Private Sub Link(game As Game)
        Debug.Assert(game IsNot Nothing)

        LinkMe(game)
        LinkBoard(game)
        LinkScoreControls(game)
    End Sub

    Private Sub LinkMe(game As Game)
        Debug.Assert(game IsNot Nothing)

        Me.BackgroundImage = Nothing
        AddHandler game.GameState.ValueChanged, AddressOf OnGameStatusUpdated
    End Sub

    Private Sub OnGameStatusUpdated(oldValue As GameState, newValue As GameState)
        If _data.Value.Game.GameState.Value = GameState.Ended Then
            OnGameEnded()
        End If
    End Sub

    Private Sub OnGameEnded()
        Dim winners = _data.Value.Game.GetPlayersWithHighestScore().Select(Function(n) n + 1).ToList()
        Dim msg As String

        If winners.Count > 1 Then
            Dim winnerString = (winners.Select(Function(s) s.ToString())).Intersperse(" ")
            msg = "Gelijkspel tussen spelers " + winnerString
        Else
            msg = String.Format("Speler {0} is de winnaar!", winners(0).ToString)
        End If

        MessageBox.Show(Me, msg)
        Unlink()
    End Sub

    Private Sub Unlink()
        UnlinkMe()
        UnlinkBoard()
        UnlinkScoreControls()

        Me.BackgroundImage = Configuration.BackgroundLogo
    End Sub

    Private Sub UnlinkMe()
        Me.BackgroundImage = Configuration.BackgroundLogo
    End Sub

    Private Sub LinkBoard(game As Game)
        Debug.Assert(game IsNot Nothing)

        _boardControl.Grid = game.Grid
        AddHandler _boardControl.TileClicked, AddressOf OnTileClicked
        _boardControl.Visible = True
    End Sub

    Private Sub LinkScoreControls(game As Game)
        Debug.Assert(game IsNot Nothing)

        _scorePane.Visible = True

        For i = 0 To game.PlayerCount - 1
            _scorePane.GetScoreControl(i).Data = New ScoreData(game, i)
        Next
    End Sub

    Private Sub UnlinkBoard()
        _boardControl.Visible = False
        RemoveHandler _boardControl.TileClicked, AddressOf OnTileClicked
    End Sub

    Private Sub UnlinkScoreControls()
        _scorePane.Visible = False
        If CurrentGame IsNot Nothing Then
            For i = 0 To ScorePane.MAX_PLAYERS - 1
                _scorePane.GetScoreControl(i).Data = Nothing
            Next
        End If
    End Sub

    Private Sub OnExit(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Close()
    End Sub

    Protected Overrides Sub OnClosed(e As EventArgs)
        Unlink()

        MyBase.OnClosed(e)
    End Sub

    Private Sub OnNewGame(sender As Object, e As EventArgs) Handles StartNewToolStripMenuItem.Click
        Dim pair = NewGameForm.GetNewGameData(Me)

        If pair IsNot Nothing Then
            Dim game = pair.Item1
            Dim players = pair.Item2

            InitializeNewGame(game, players)
        End If
    End Sub

    Private Class Data
        Private ReadOnly _game As Game

        Private ReadOnly _players As IList(Of Player)

        Public Sub New(game As Game, players As IEnumerable(Of Player))
            _game = game
            _players = players.ToList()

            Debug.Assert(_game Is Nothing OrElse _game.PlayerCount = _players.Count)
        End Sub

        Public ReadOnly Property Game As Game
            Get
                Return _game
            End Get
        End Property

        Public ReadOnly Property Players As IList(Of Player)
            Get
                Return _players
            End Get
        End Property

    End Class

    Private Sub _aboutMenuItem_Click(sender As Object, e As EventArgs) Handles _aboutMenuItem.Click
        Dim form = New AboutBox()
        form.ShowDialog(Me)
    End Sub
End Class