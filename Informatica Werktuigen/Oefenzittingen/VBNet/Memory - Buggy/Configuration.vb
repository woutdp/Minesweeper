Imports System.Reflection
Imports System.Xml
Imports System.IO

' Dit is een module en geen klasse, omdat er maar één (gedeelde) instantie
' van 'Configuration' in het programma nodig is
Public Module Configuration

    Private _lingerDuration As Double
    Private _aiDelay As Double
    Private _maxSize As Size
    Private _minSize As Size
    Private _buttonSize As Integer
    Private _backgroundLogo As Image
    Private _backgroundTile As Image
    Private _hiddenTileImage As Image
    Private _tileBorderImage As Image
    Private _scoreTile As Image

    Private _playerColors As List(Of Color)
    Private _icons As List(Of Image)


    Sub New()
        'Assembly.GetExecutingAssembly().GetManifestResourceNames()
        'Assembly.GetExecutingAssembly().GetManifestResourceStream()

        ' BUG1: fout in naam steken; dan met QuickWatch kijken naar Assembly.GetExecutingAssembly().GetManifestResourceNames
        Dim configStream As Stream
        configStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Memory.Config.xml")

        ' enkel data proberen uit te lezen indien het configuratiebestand
        ' geopend kon worden
        If Not configStream Is Nothing Then
            Dim config As New XmlDocument()
            config.Load(configStream)

            _playerColors = ReadPlayerColors(config.SelectSingleNode("/Configuration/Parameters/PlayerColors"))

            _aiDelay = Double.Parse(config.SelectSingleNode("/Configuration/Parameters/AIDelay").InnerText) / 1000
            _lingerDuration = Double.Parse(config.SelectSingleNode("/Configuration/Parameters/LingerDuration").InnerText) / 1000
            _maxSize = XmlToSize(config.SelectSingleNode("/Configuration/Parameters/GridSize/Max"))
            _minSize = XmlToSize(config.SelectSingleNode("/Configuration/Parameters/GridSize/Min"))
            _buttonSize = Integer.Parse(config.SelectSingleNode("/Configuration/Parameters/ButtonSize").Attributes("size").Value)

            _backgroundTile = XmlToImage(config.SelectSingleNode("/Configuration/Parameters/BackgroundTile"))
            _backgroundLogo = XmlToImage(config.SelectSingleNode("/Configuration/Parameters/BackgroundLogo"))
            _scoreTile = XmlToImage(config.SelectSingleNode("/Configuration/Parameters/ScoreTile"))
            _tileBorderImage = New Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("Memory.Button.png"))
            _hiddenTileImage = New Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("Memory.qmark.png"))

            _icons = New List(Of Image)
            For Each node As XmlNode In config.SelectNodes("/Configuration/Icons/Icon")
                _icons.Add(XmlToImage(node))
            Next
        End If
    End Sub

    Public ReadOnly Property AIDelay As Double
        Get
            Return _aiDelay
        End Get
    End Property

    Public Function GetPlayerColor(i As Integer) As Color
        Return _playerColors(i)
    End Function

    Private Function ReadPlayerColors(node As XmlNode) As List(Of Color)
        Dim result = New List(Of Color)

        For Each child As XmlNode In node.ChildNodes
            result.Add(Color.FromName(child.InnerText))
        Next

        Return result
    End Function

    Public ReadOnly Property LingerDuration As Double
        Get
            Return _lingerDuration
        End Get
    End Property

    Public ReadOnly Property TileBorderImage As Image
        Get
            Return _tileBorderImage
        End Get
    End Property

    Public ReadOnly Property HiddenTileImage As Image
        Get
            Return _hiddenTileImage
        End Get
    End Property

    Private Function XmlToSize(node As XmlNode) As Size
        Dim ret As Size
        If Not node Is Nothing Then
            If Not node.Attributes("width") Is Nothing Then
                ret.Width = Integer.Parse(node.Attributes("width").Value)
            End If
            If Not node.Attributes("height") Is Nothing Then
                ret.Height = Integer.Parse(node.Attributes("height").Value)
            End If
        End If
        Return ret
    End Function

    Private Function XmlToImage(node As XmlNode) As Image
        If node Is Nothing OrElse node.Attributes("source") Is Nothing Then
            Return Nothing
        Else
            Dim imageStream As Stream
            imageStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(node.Attributes("source").Value)
            Return New Bitmap(imageStream)
        End If
    End Function

    Public ReadOnly Property Icons() As List(Of Image)
        Get
            Return _icons
        End Get
    End Property

    Public ReadOnly Property MaxGridSize() As Size
        Get
            Return _maxSize
        End Get
    End Property

    Public ReadOnly Property MinGridSize() As Size
        Get
            Return _minSize
        End Get
    End Property

    Public ReadOnly Property ButtonSize() As Integer
        Get
            Return _buttonSize
        End Get
    End Property

    Public ReadOnly Property BackgroundTile() As Image
        Get
            Return _backgroundTile
        End Get
    End Property
    Public ReadOnly Property ScoreTile() As Image
        Get
            Return _scoreTile
        End Get
    End Property

    Public ReadOnly Property BackgroundLogo() As Image
        Get
            Return _backgroundLogo
        End Get
    End Property

End Module
