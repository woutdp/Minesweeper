Public Class Position
    Private _x As Integer

    Private _y As Integer

    Public Sub New(x As Integer, y As Integer)
        _x = x
        _y = y
    End Sub

    Public Overrides Function ToString() As String
        Return String.Format("({0}, {1})", _x, _y)
    End Function

    Public Overrides Function Equals(obj As Object) As Boolean
        If obj IsNot Nothing AndAlso TypeOf (obj) Is Position Then
            Dim other = DirectCast(obj, Position)

            Return _x = other._x AndAlso _y = other._y
        Else
            Return False
        End If
    End Function

    Public ReadOnly Property X As Integer
        Get
            Return _x
        End Get
    End Property

    Public ReadOnly Property Y As Integer
        Get
            Return _y
        End Get
    End Property

    Public Overrides Function GetHashCode() As Integer
        Return _x.GetHashCode() Xor _y.GetHashCode()
    End Function

End Class
