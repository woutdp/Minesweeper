Public Class ArtificialIntelligenceMemory

    Private ReadOnly _gameGrid As GameGrid

    Private ReadOnly _remembered As HashSet(Of Position)

    Private ReadOnly _forgetfulness As Single

    Public Sub New(gameBoard As GameGrid, forgetfulness As Single)
        Debug.Assert(gameBoard IsNot Nothing)

        _gameGrid = gameBoard
        _forgetfulness = forgetfulness
        _remembered = New HashSet(Of Position)
        Observe(gameBoard)
    End Sub

    Private Sub Observe(board As GameGrid)
        For Each p In board.TiledPositions
            Dim watcher = New TileWatcher(Me, board(p))
        Next
    End Sub

    Private Sub OnTileFlipped(tile As GameTile)
        Debug.Assert(tile IsNot Nothing)

        If tile.Shown.Value Then
            Remember(tile)
        End If
    End Sub

    Private Sub Remember(tile As GameTile)
        _remembered.Add(tile.Position)
    End Sub

    Public ReadOnly Property Grid As GameGrid
        Get
            Return _gameGrid
        End Get
    End Property

    Public Sub Alzheimer()
        Dim forget = New List(Of Position)()

        For Each pos In _remembered
            If Rnd() < _forgetfulness Then
                forget.Add(pos)
            End If
        Next

        For Each pos In forget
            _remembered.Remove(pos)
        Next
    End Sub

    Public Function FindUnfoundPair() As Tuple(Of Position, Position)
        For Each pos In _remembered
            If Not _gameGrid(pos).Paired.Value Then
                Dim other = FindOther(pos)

                If other IsNot Nothing Then
                    Return Tuple.Create(pos, other)
                End If
            End If
        Next

        Return Nothing
    End Function

    Public Function FindOther(pos As Position) As Position
        For Each p In _gameGrid.TiledPositions
            If Not pos.Equals(p) AndAlso _remembered.Contains(p) AndAlso _gameGrid(p).Contents = _gameGrid(pos).Contents Then
                Return p
            End If
        Next

        Return Nothing
    End Function

    Private Class TileWatcher
        Private ReadOnly _tile As GameTile

        Private ReadOnly _parent As ArtificialIntelligenceMemory

        Public Sub New(parent As ArtificialIntelligenceMemory, tile As GameTile)
            Debug.Assert(parent IsNot Nothing)
            Debug.Assert(tile IsNot Nothing)

            _parent = parent
            _tile = tile
            AddHandler tile.Shown.ValueChanged, AddressOf OnUpdate
        End Sub

        Private Sub OnUpdate(oldValue As Boolean, newValue As Boolean)
            _parent.OnTileFlipped(_tile)
        End Sub
    End Class

End Class
