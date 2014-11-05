Public Class Form1
    Dim timer As Stopwatch
    Dim backBuffer As Image
    Dim graphics As Graphics
    Dim clientWidth As Integer
    Dim clientHeight As Integer
    Dim interval As Long
    Dim startTick As Long
    Dim imageRect As Rectangle
    Dim direction As Point
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.DoubleBuffered = True
        Me.MaximizeBox = False
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
        timer = New Stopwatch()
        clientWidth = 300
        clientHeight = 300
        interval = 16
        Me.ClientSize = New Size(clientWidth, clientHeight)
        backBuffer = New Bitmap(clientWidth, clientHeight)
        graphics = graphics.FromImage(backBuffer)
        direction = New Point(2, 3)
        imageRect = New Rectangle(0, 0, 55, 65)
    End Sub
    Private Sub Form1_Shown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown
        GameLoop()
    End Sub

    Private Sub GameLoop()
        timer.Start()
        Do While (Me.Created)
            startTick = timer.ElapsedMilliseconds
            'GameLogic()
            'RenderScene()
            Console.WriteLine(interval)
            Application.DoEvents()
            Do While timer.ElapsedMilliseconds - startTick < interval

            Loop
        Loop
    End Sub
End Class
