Public Class BoardControl

    Private _gameGrid As GameGrid

    Public Property Grid As GameGrid
        Get
            Return _gameGrid
        End Get
        Set(value As GameGrid)
            _gameGrid = value
            RecreateChildren()
        End Set
    End Property

    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)
        RepositionChildren()
    End Sub

    Private Sub RecreateChildren()
        ClearChildren()

        If _gameGrid IsNot Nothing Then
            For Each pos In _gameGrid.AllPositions
                CreateChild(pos)
            Next

            RepositionChildren()
        End If
    End Sub

    Private Sub ClearChildren()
        Dim copy = New List(Of TileControl)

        For Each ctrl As TileControl In Me.Controls
            copy.Add(ctrl)
        Next

        Me.Controls.Clear()

        For Each ctrl In copy
            ctrl.Dispose()
        Next
    End Sub

    Private Sub OnDispose() Handles Me.Disposed
        ClearChildren()
    End Sub

    Private Sub CreateChild(pos As Position)
        If _gameGrid(pos) IsNot Nothing Then
            Dim tile As GameTile = _gameGrid(pos)
            Dim control = New TileControl()

            control.Tile = tile
            AddHandler control.Click, AddressOf OnTileClicked

            Me.Controls.Add(control)
        End If
    End Sub

    Private Sub OnTileClicked(sender As Object, args As EventArgs)
        RaiseEvent TileClicked(CType(sender, TileControl))
    End Sub

    Private Sub PositionChild(control As TileControl, lay As GridLayout)
        Debug.Assert(control IsNot Nothing)
        Debug.Assert(control.Tile IsNot Nothing)

        Dim pos = control.Tile.Position
        Dim rect = lay(pos)
        control.SetBounds(rect.X, rect.Y, rect.Width, rect.Height)
    End Sub

    Private Function ComputeTileRectangle(pos As Position) As Rectangle
        ' POI: divide first, then multiply
        Dim x = pos.X * Width \ _gameGrid.Width
        Dim y = pos.Y * Height \ _gameGrid.Height
        Dim w = Width \ _gameGrid.Width
        Dim h = Height \ _gameGrid.Height

        Return New Rectangle(x, y, w, h)
    End Function

    Private Sub RepositionChildren()
        If _gameGrid IsNot Nothing Then
            Dim layout = New GridLayout(Width, Height, _gameGrid.Width, _gameGrid.Height, 5)

            For Each child In Me.Controls
                PositionChild(CType(child, TileControl), layout)
            Next
        End If
    End Sub

    Public Event TileClicked(tile As TileControl)

End Class

