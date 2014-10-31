Public Class Grid(Of T)
    Private ReadOnly _grid As T(,)

    Public Sub New(width As Integer, height As Integer, initializer As Func(Of Position, T))
        ReDim _grid(width - 1, height - 1)

        For x = 0 To width - 1
            For y = 0 To height - 1
                _grid(x, y) = initializer(New Position(x, y))
            Next
        Next
    End Sub

    Public Sub New(width As Integer, height As Integer, initialValue As T)
        Me.New(width, height, Function(p As Position) initialValue)
    End Sub

    Public ReadOnly Property Width As Integer
        Get
            Return _grid.GetLength(0)
        End Get
    End Property

    Public ReadOnly Property Height As Integer
        Get
            Return _grid.GetLength(1)
        End Get
    End Property

    Default Public Property Item(pos As Position) As T
        Get
            Return _grid(pos.X, pos.Y)
        End Get
        Set(value As T)
            _grid(pos.X, pos.Y) = value
        End Set
    End Property

    Public ReadOnly Property AllPositions As IEnumerable(Of Position)
        Get
            Return From x In Enumerable.Range(0, Me.Width) From y In Enumerable.Range(0, Me.Height) Select New Position(x, y)
        End Get
    End Property

End Class
