Public Class NewGameForm

    Private _boardSize As Size

    Private _gridSizes As List(Of Size)

    Private _playerChoices As List(Of PlayerChoice)

    Private _playerControls As List(Of ComboBox)

    Public Shared Function GetNewGameData(parent As IWin32Window) As Tuple(Of Game, List(Of Player))
        Dim form = New NewGameForm()
        Dim result = form.ShowDialog(parent)

        If result = Windows.Forms.DialogResult.Yes Then
            Return form.Result
        Else
            Return Nothing
        End If
    End Function

    Private Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        CollectPlayerChoiceControls()
        CreateGridSizes()
        CreatePlayerChoices()

        _difficulties.SelectedIndex = 2
        _player1.SelectedIndex = 1
        _player2.SelectedIndex = 3
        _player3.SelectedIndex = 0
        _player4.SelectedIndex = 0
    End Sub

    Private ReadOnly Property Result As Tuple(Of Game, List(Of Player))
        Get
            Dim playerChoices = (From control In _playerControls
                                 Where control.SelectedIndex > 0
                                 Select _playerChoices(control.SelectedIndex)).ToList()
            Dim playerCount = playerChoices.Count
            Dim size = _gridSizes(_difficulties.SelectedIndex)
            Dim grid = New GameGrid(size.Width, size.Height)
            Dim game = New Game(grid, playerCount)
            Dim players = playerChoices.Zip(Enumerable.Range(0, playerCount), Function(pc, idx) pc.CreatePlayer(game, idx)).ToList()

            Return Tuple.Create(game, players)
        End Get
    End Property

    Private Sub CollectPlayerChoiceControls()
        _playerControls = New List(Of ComboBox) From {_player1, _player2, _player3, _player4}
    End Sub

    Private Sub CreateGridSizes()
        _gridSizes = New List(Of Size) From {New Size(3, 3),
                                             New Size(4, 3),
                                             New Size(4, 4),
                                             New Size(5, 4),
                                             New Size(6, 4),
                                             New Size(6, 5),
                                             New Size(7, 5)}

        For Each s In _gridSizes
            _difficulties.Items.Add(String.Format("{0} x {1}", s.Width, s.Height))
        Next
    End Sub

    Private Sub CreatePlayerChoices()
        _playerChoices = New List(Of PlayerChoice) From {New NoPlayerChoice(),
                                                         New HumanPlayerChoice(),
                                                         New ComputerPlayerChoice(1.0, "Triviaal"),
                                                         New ComputerPlayerChoice(0.8, "Gemakkelijk"),
                                                         New ComputerPlayerChoice(0.5, "Medium"),
                                                         New ComputerPlayerChoice(0.25, "Moeilijk"),
                                                         New ComputerPlayerChoice(0.0, "Perfect")}

        For Each Control In _playerControls
            For Each pc In _playerChoices
                Control.Items.Add(pc.Description)
            Next
        Next

    End Sub

    Private Sub _startButton_Click(sender As System.Object, e As System.EventArgs) Handles _startButton.Click
        Me.DialogResult = Windows.Forms.DialogResult.Yes
        Me.Close()
    End Sub

    Private Sub _cancelButton_Click(sender As System.Object, e As System.EventArgs) Handles _cancelButton.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private MustInherit Class PlayerChoice
        Private ReadOnly _description As String

        Protected Sub New(description As String)
            Debug.Assert(description IsNot Nothing)

            _description = description
        End Sub

        Public ReadOnly Property Description As String
            Get
                Return _description
            End Get
        End Property

        Public MustOverride Function CreatePlayer(game As Game, index As Integer) As Player
    End Class

    Private Class HumanPlayerChoice
        Inherits PlayerChoice

        Public Sub New()
            MyBase.New("Menselijke speler")
        End Sub

        Public Overrides Function CreatePlayer(game As Game, index As Integer) As Player
            Return New HumanPlayer(game, index)
        End Function
    End Class

    Private Class ComputerPlayerChoice
        Inherits PlayerChoice

        Private ReadOnly _forgetfulness As Single

        Public Sub New(forgetfulness As Single, difficultyDescription As String)
            MyBase.New(String.Format("Artificiële intelligentie ({0})", difficultyDescription))

            _forgetfulness = forgetfulness
        End Sub

        Public Overrides Function CreatePlayer(game As Game, index As Integer) As Player
            Return New ArtificialPlayer(game, index, New ArtificialIntelligence(game.Grid, _forgetfulness))
        End Function
    End Class

    Private Class NoPlayerChoice
        Inherits PlayerChoice

        Public Sub New()
            MyBase.New("-")
        End Sub

        Public Overrides Function CreatePlayer(game As Game, index As Integer) As Player
            Return Nothing
        End Function
    End Class
End Class
