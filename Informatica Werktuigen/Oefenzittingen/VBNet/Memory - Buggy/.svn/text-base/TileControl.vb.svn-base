Imports System.Drawing.Imaging
Imports System.Reflection

Public Class TileControl

    Private _gameTile As GameTile

    Private ReadOnly _borderImage As Image

    Private ReadOnly _hiddenImage As Image

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
        SetStyle(ControlStyles.ResizeRedraw, True)

        _borderImage = Configuration.TileBorderImage
        _hiddenImage = Configuration.HiddenTileImage
    End Sub

    Public Property Tile As GameTile
        Get
            Return _gameTile
        End Get
        Set(value As GameTile)
            Unobserve(_gameTile)
            _gameTile = value
            Observe(_gameTile)

            Invalidate()
        End Set
    End Property

    Private Sub OnDispose() Handles Me.Disposed
        Unobserve(_gameTile)
        _gameTile = Nothing
    End Sub

    Private Sub Observe(gameTile As GameTile)
        If gameTile IsNot Nothing Then
            AddHandler gameTile.Paired.ValueChanged, AddressOf OnTileChange
            AddHandler gameTile.Shown.ValueChanged, AddressOf OnTileChange
        End If
    End Sub

    Private Sub Unobserve(gameTile As GameTile)
        If gameTile IsNot Nothing Then
            RemoveHandler gameTile.Paired.ValueChanged, AddressOf OnTileChange
            RemoveHandler gameTile.Shown.ValueChanged, AddressOf OnTileChange
        End If
    End Sub

    Private Sub OnTileChange(oldValue As Boolean, newValue As Boolean)
        Invalidate()
    End Sub

    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        MyBase.OnPaint(e)

        If _gameTile IsNot Nothing Then
            PaintBorder(e.Graphics)
            PaintInterior(e.Graphics)
        End If
    End Sub

    Private Sub PaintInterior(g As Graphics)
        Debug.Assert(_gameTile IsNot Nothing)

        Dim image = GetInteriorImage()
        Dim rect = Me.ClientRectangle
        rect.Inflate(-3, -3)

        g.DrawImage(image, rect)
    End Sub

    Private Sub PaintBorder(g As Graphics)
        Dim bc As Color = GetBackgroundColor()

        ' Past de kleur van de knop aan
        Dim matrix()() As Single = { _
           New Single() {CType(bc.R / 255, Single), 0, 0, 0, 0}, _
           New Single() {0, CType(bc.G / 255, Single), 0, 0, 0}, _
           New Single() {0, 0, CType(bc.B / 255, Single), 0, 0}, _
           New Single() {0, 0, 0, 1, 0}, _
           New Single() {0.0F, 0.0F, 0.0F, 0, 1}}

        Dim imageAttrs As New ImageAttributes
        imageAttrs.SetColorMatrix(New ColorMatrix(matrix))

        ' Teken de knop
        g.DrawImage(_borderImage, New Rectangle(0, 0, Width, Height), 0, 0, _borderImage.Width, _borderImage.Height, GraphicsUnit.Pixel, imageAttrs)
    End Sub

    Private Function GetBackgroundColor() As Color
        Debug.Assert(_gameTile IsNot Nothing)

        If _gameTile.Shown.Value Then
            If _gameTile.Paired.Value Then
                Return Color.Lavender
            Else
                Return Color.Salmon
            End If
        Else
            Return Color.White
        End If
    End Function

    Private Function GetInteriorImage() As Image
        Debug.Assert(_gameTile IsNot Nothing)

        If _gameTile.Shown.Value Then
            Return Configuration.Icons(_gameTile.Contents)
        Else
            Return Configuration.HiddenTileImage
        End If
    End Function

End Class
