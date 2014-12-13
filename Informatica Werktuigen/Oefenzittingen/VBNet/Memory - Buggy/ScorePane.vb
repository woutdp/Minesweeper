Public Class ScorePane

    Private ReadOnly _scoreControls As ScoreControl()

    Public Const MAX_PLAYERS As Integer = 4

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        ReDim _scoreControls(MAX_PLAYERS - 1)

        For i = 0 To _scoreControls.Length - 1
            _scoreControls(i) = New ScoreControl()
            _scoreControls(i).BackColor = Configuration.GetPlayerColor(i)
            Me.Controls.Add(_scoreControls(i))
        Next

    End Sub

    Public Function GetScoreControl(i As Integer) As ScoreControl
        Debug.Assert(0 <= i AndAlso i < _scoreControls.Length)

        Return _scoreControls(i)
    End Function

    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)

        RepositionChildren()
    End Sub

    Public ReadOnly Property PlayerCount As Integer
        Get
            Return _scoreControls.Length
        End Get
    End Property

    Private Sub RepositionChildren()
        Dim h = Me.Height \ PlayerCount

        For i = 0 To PlayerCount - 1
            GetScoreControl(i).SetBounds(0, h * i, Me.Width, h)
        Next
    End Sub
End Class
