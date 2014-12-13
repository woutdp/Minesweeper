Public Class GameGrid
    Private ReadOnly _grid As Grid(Of GameTile)

    Public Sub New(width As Integer, height As Integer)
        Dim rawGrid = CreateRandomGrid(width, height)

        _grid = New Grid(Of GameTile)(width, height, Function(p) CreateTile(p, rawGrid(p)))
    End Sub

    Private Shared Function CreateTile(pos As Position, contents As Integer?) As GameTile
        If contents.HasValue Then
            Return New GameTile(pos, contents.Value)
        Else
            Return Nothing
        End If
    End Function

    Private Shared Function CreateRandomGrid(width As Integer, height As Integer) As Grid(Of Integer?)
        Debug.Assert(width > 0)
        Debug.Assert(height > 0)

        Dim range = Enumerable.Range(0, width * height \ 2).Select(Function(n As Integer) New Nullable(Of Integer)(n))
        Dim numbers = New List(Of Nullable(Of Integer))()

        numbers.AddRange(range)
        numbers.AddRange(range)
        numbers.Shuffle()

        Dim grid = New Grid(Of Integer?)(width, height, CType(Nothing, Integer?))
        Dim gridPositions = New HashSet(Of Position)(grid.AllPositions)

        If (width * height) Mod 2 = 1 Then
            Dim x = width \ 2
            Dim y = height \ 2
            Dim p = New Position(x, y)

            gridPositions.Remove(p)
        End If

        Debug.Assert(numbers.Count = gridPositions.Count)

        ' POI
        For Each pair In numbers.Zip(gridPositions, Function(n, p) Tuple.Create(n, p))
            grid(pair.Item2) = pair.Item1
        Next

        Return grid
    End Function

    Public ReadOnly Property Width As Integer
        Get
            Return _grid.Width
        End Get
    End Property

    Public ReadOnly Property Height As Integer
        Get
            Return _grid.Height
        End Get
    End Property

    Default ReadOnly Property Item(pos As Position) As GameTile
        Get
            Return _grid(pos)
        End Get
    End Property

    Public ReadOnly Property AllPositions As IEnumerable(Of Position)
        Get
            Return _grid.AllPositions
        End Get
    End Property

    Public ReadOnly Property AllTilesPaired As Boolean
        Get
            Return AllPositions.All(Function(p) If(_grid(p) IsNot Nothing, _grid(p).Paired.Value, True))
        End Get
    End Property

    Public ReadOnly Property UnpairedPositions As IEnumerable(Of Position)
        Get
            Return From pos In TiledPositions Where Not _grid(pos).Paired.Value Select pos
        End Get
    End Property

    Public ReadOnly Property TiledPositions As IEnumerable(Of Position)
        Get
            Return From pos In AllPositions Where _grid(pos) IsNot Nothing
        End Get
    End Property

End Class

Public Class GameTile

    Private ReadOnly _contents As Integer

    Private ReadOnly _position As Position

    Private ReadOnly _paired As ObservableVariable(Of Boolean)

    Private ReadOnly _shown As ObservableVariable(Of Boolean)

    Public Sub New(pos As Position, contents As Integer)
        _contents = contents
        _position = pos
        _paired = New ObservableVariable(Of Boolean)(False)
        _shown = New ObservableVariable(Of Boolean)(False)
    End Sub

    Public ReadOnly Property Contents As Integer
        Get
            Return _contents
        End Get
    End Property

    Public ReadOnly Property Paired As ObservableVariable(Of Boolean)
        Get
            Return _paired
        End Get
    End Property

    Public ReadOnly Property Shown As ObservableVariable(Of Boolean)
        Get
            Return _shown
        End Get
    End Property

    Public ReadOnly Property Position As Position
        Get
            Return _position
        End Get
    End Property
End Class
