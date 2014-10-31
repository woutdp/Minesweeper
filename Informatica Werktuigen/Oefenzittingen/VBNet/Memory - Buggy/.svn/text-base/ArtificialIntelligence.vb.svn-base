Public Class ArtificialIntelligence

    Private _memory As ArtificialIntelligenceMemory

    Public Sub New(grid As GameGrid, forgetfulness As Single)
        Debug.Assert(grid IsNot Nothing)

        _memory = New ArtificialIntelligenceMemory(grid, forgetfulness)
    End Sub

    Public Function MakePicks() As Tuple(Of Position, Position)
        _memory.Alzheimer()

        Dim pair = _memory.FindUnfoundPair()

        If pair IsNot Nothing Then
            Return pair
        Else
            Dim unpaired = _memory.Grid.UnpairedPositions.ToList
            Dim firstPick = unpaired.PickRandom()
            Dim secondPick = _memory.FindOther(firstPick)

            If secondPick Is Nothing Then
                unpaired.Remove(firstPick)
                secondPick = unpaired.PickRandom()
            End If

            Return Tuple.Create(firstPick, secondPick)
        End If
    End Function

End Class
