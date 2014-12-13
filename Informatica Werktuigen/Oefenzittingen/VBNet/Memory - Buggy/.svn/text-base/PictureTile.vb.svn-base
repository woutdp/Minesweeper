Imports System.Reflection
Imports System.Drawing.Imaging

''' <summary>
''' Stelt een "tile" (vakje) voor in het spelrooster. Het houdt de toestand van een tile bij en is verantwoordelijk voor het tekenen ervan.
''' </summary>
Public Class PictureTile
    ' Velden
    Private Shared _buttonImage As Image
    Private Shared _qmark As Image
    Private _tileSize As Integer
    Private _x As Integer
    Private _y As Integer
    Private _isVisible As Boolean
    Private _hasBeenGuessed As Boolean
    Private _mustRedraw As Boolean
    Private _backColor As Color
    Private _hiddenImage As Image


    ''' <summary>
    ''' Constructor.
    ''' </summary>
    ''' <param name="tileSize">Grootte</param>
    ''' <param name="image">Geassocieerd plaatje</param>
    ''' <param name="x">X-positie in het spelrooster</param>
    ''' <param name="y">Y-positie in het spelrooster</param>
    Public Sub New(tileSize As Integer, image As Image, x As Integer, y As Integer)
        _hiddenImage = image
        _isVisible = False
        _hasBeenGuessed = False
        _mustRedraw = True
        _tileSize = tileSize
        _backColor = Color.White
        _x = x
        _y = y
    End Sub

    ''' <summary>
    ''' Tekent de tile.
    ''' </summary>
    ''' <param name="g">Te gebruiken graphics object (dient om kunnen tekenen naar het scherm)</param>
    ''' <param name="x">X positie op het scherm</param>
    ''' <param name="y">Y positie op het scherm</param>
    ''' <remarks></remarks>
    Public Sub Draw(g As Graphics, x As Integer, y As Integer)
        ' Teken een witte rechthoek (met schone lei beginnen)
        g.FillRectangle(Brushes.White, x, y, _tileSize, _tileSize)

        ' Het tekenen bestaat uit het tonen van een "knop" (een kader) met daarin een tekening

        ' Kies juiste achtergrondkleur voor de knop
        Dim bc As Color = Color.White
        If _isVisible Then bc = _backColor

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
        g.DrawImage(ButtonImage, New Rectangle(x, y, _tileSize, _tileSize), 0, 0, _buttonImage.Width, _buttonImage.Height, GraphicsUnit.Pixel, imageAttrs)

        ' Teken de juiste inhoud in de knop
        If _isVisible Then
            ' Teken het plaatje
            g.DrawImage(_hiddenImage, New Rectangle(x + 3, y + 3, _tileSize - 6, _tileSize - 6), New Rectangle(0, 0, _hiddenImage.Width, _hiddenImage.Height), GraphicsUnit.Pixel)
        Else
            ' Teken een vraagteken
            g.DrawImage(QuestionMark, New Rectangle(x + 3, y + 3, _tileSize - 6, _tileSize - 6), New Rectangle(0, 0, _hiddenImage.Width, _hiddenImage.Height), GraphicsUnit.Pixel)
        End If

        _mustRedraw = False
    End Sub

    Public ReadOnly Property HiddenImage() As Image
        Get
            Return _hiddenImage
        End Get
    End Property

    Public Property IsVisible() As Boolean
        Get
            Return _isVisible
        End Get
        Set(ByVal value As Boolean)
            _isVisible = value
            _mustRedraw = True
        End Set
    End Property

    Public Property HasBeenGuessed() As Boolean
        Get
            Return _hasBeenGuessed
        End Get
        Set(ByVal value As Boolean)
            _hasBeenGuessed = value
        End Set
    End Property

    Public ReadOnly Property MustRedraw() As Boolean
        Get
            Return _mustRedraw
        End Get
    End Property

    Public Property BackColor() As Color
        Get
            Return _backColor
        End Get
        Set(ByVal value As Color)
            _backColor = value
            _mustRedraw = True
        End Set
    End Property
    Public ReadOnly Property X() As Integer
        Get
            Return _x
        End Get
    End Property
    Public ReadOnly Property Y() As Integer
        Get
            Return _y
        End Get
    End Property

    Public Shared ReadOnly Property QuestionMark As Image
        Get
            If _qmark Is Nothing Then
                _qmark = New Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("Memory.qmark.png"))
            End If
            Return _qmark
        End Get
    End Property

    Private Shared ReadOnly Property ButtonImage() As Image
        Get
            If _buttonImage Is Nothing Then
                _buttonImage = New Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("Memory.Button.png"))
            End If
            Return _buttonImage
        End Get
    End Property

End Class
