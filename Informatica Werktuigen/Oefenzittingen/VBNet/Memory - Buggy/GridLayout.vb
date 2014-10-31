Public Class GridLayout

    Private _width As Integer

    Private _height As Integer

    Private _columnCount As Integer

    Private _rowCount As Integer

    Private _margin As Integer

    Public Sub New(width As Integer, height As Integer, cols As Integer, rows As Integer, margin As Integer)
        _width = width
        _height = height
        _columnCount = cols
        _rowCount = rows
        _margin = margin
    End Sub

    Default Public ReadOnly Property Item(pos As Position) As Rectangle
        Get
            Debug.Assert(pos IsNot Nothing)
            Debug.Assert(0 <= pos.X AndAlso pos.X < _columnCount)
            Debug.Assert(0 <= pos.Y AndAlso pos.Y < _rowCount)

            ' POI: divide first, then multiply
            Dim x = pos.X * _width \ _columnCount
            Dim y = pos.Y * _height \ _rowCount
            Dim w = _width \ _columnCount
            Dim h = _height \ _rowCount

            Dim rect = New Rectangle(x, y, w, h)
            rect.Inflate(-_margin, -_margin)

            Return rect
        End Get
    End Property

End Class
