Imports System.Runtime.CompilerServices
Imports System.Text

Public Module Extensions

    Private rng As Random = New Random

    <Extension()> Public Sub Swap(Of T)(xs As List(Of T), i As Integer, j As Integer)
        Dim temp As T

        temp = xs(i)
        xs(i) = xs(j)
        xs(j) = temp
    End Sub

    <Extension()> Public Sub Shuffle(Of T)(xs As List(Of T))
        For i As Integer = 0 To xs.Count
            Dim x As Integer = rng.Next(xs.Count)
            Dim y As Integer = rng.Next(xs.Count)
            xs.Swap(x, y)
        Next
    End Sub

    <Extension()> Public Function UnorderedPairs(Of T)(xs As IEnumerable(Of T)) As IEnumerable(Of Tuple(Of T, T))
        Dim lst = xs.ToList()
        Dim result = New List(Of Tuple(Of T, T))

        For i = 0 To lst.Count - 1
            For j = i + 1 To lst.Count - 1
                result.Add(Tuple.Create(lst(i), lst(j)))
            Next
        Next

        Return result
    End Function

    <Extension()> Public Function PickRandom(Of T)(xs As List(Of T)) As T
        Return xs(rng.Next(xs.Count))
    End Function

    <Extension()> Public Function Intersperse(strs As IEnumerable(Of String), infix As String) As String
        Dim sb = New StringBuilder()
        Dim first = True

        For Each s In strs
            If first Then
                sb.Append(s)
                first = False
            Else
                sb.Append(infix)
                sb.Append(s)
            End If
        Next

        Return sb.ToString()
    End Function

End Module
