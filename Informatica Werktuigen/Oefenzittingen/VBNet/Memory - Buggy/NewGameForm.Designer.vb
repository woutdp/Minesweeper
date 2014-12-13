<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class NewGameForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me._difficulties = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me._player1 = New System.Windows.Forms.ComboBox()
        Me._player2 = New System.Windows.Forms.ComboBox()
        Me._player3 = New System.Windows.Forms.ComboBox()
        Me._player4 = New System.Windows.Forms.ComboBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me._startButton = New System.Windows.Forms.Button()
        Me._cancelButton = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(59, 119)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(129, 17)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Moeilijkheidsgraad:"
        '
        '_difficulties
        '
        Me._difficulties.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me._difficulties.FormattingEnabled = True
        Me._difficulties.Location = New System.Drawing.Point(196, 116)
        Me._difficulties.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me._difficulties.Name = "_difficulties"
        Me._difficulties.Size = New System.Drawing.Size(191, 24)
        Me._difficulties.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(59, 153)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(65, 17)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Speler 1:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(59, 186)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(65, 17)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Speler 2:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(59, 219)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(65, 17)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Speler 3:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(59, 252)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(65, 17)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "Speler 4:"
        '
        '_player1
        '
        Me._player1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me._player1.FormattingEnabled = True
        Me._player1.Location = New System.Drawing.Point(132, 149)
        Me._player1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me._player1.Name = "_player1"
        Me._player1.Size = New System.Drawing.Size(255, 24)
        Me._player1.TabIndex = 6
        '
        '_player2
        '
        Me._player2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me._player2.FormattingEnabled = True
        Me._player2.Location = New System.Drawing.Point(132, 182)
        Me._player2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me._player2.Name = "_player2"
        Me._player2.Size = New System.Drawing.Size(255, 24)
        Me._player2.TabIndex = 7
        '
        '_player3
        '
        Me._player3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me._player3.FormattingEnabled = True
        Me._player3.Location = New System.Drawing.Point(132, 215)
        Me._player3.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me._player3.Name = "_player3"
        Me._player3.Size = New System.Drawing.Size(255, 24)
        Me._player3.TabIndex = 8
        '
        '_player4
        '
        Me._player4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me._player4.FormattingEnabled = True
        Me._player4.Location = New System.Drawing.Point(132, 249)
        Me._player4.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me._player4.Name = "_player4"
        Me._player4.Size = New System.Drawing.Size(255, 24)
        Me._player4.TabIndex = 9
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.PictureBox1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(539, 91)
        Me.Panel1.TabIndex = 10
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(99, 37)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(421, 46)
        Me.Label7.TabIndex = 2
        Me.Label7.Text = "Hieronder kan je de instellingen kiezen voor het nieuwe spel dat je gaat starten." & _
    ""
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(99, 16)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(142, 17)
        Me.Label6.TabIndex = 1
        Me.Label6.Text = "Nieuw spel starten"
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.Memory.My.Resources.Resources._29
        Me.PictureBox1.Location = New System.Drawing.Point(4, 4)
        Me.PictureBox1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(85, 79)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        '_startButton
        '
        Me._startButton.Location = New System.Drawing.Point(423, 310)
        Me._startButton.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me._startButton.Name = "_startButton"
        Me._startButton.Size = New System.Drawing.Size(100, 28)
        Me._startButton.TabIndex = 11
        Me._startButton.Text = "Start!"
        Me._startButton.UseVisualStyleBackColor = True
        '
        '_cancelButton
        '
        Me._cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me._cancelButton.Location = New System.Drawing.Point(315, 310)
        Me._cancelButton.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me._cancelButton.Name = "_cancelButton"
        Me._cancelButton.Size = New System.Drawing.Size(100, 28)
        Me._cancelButton.TabIndex = 12
        Me._cancelButton.Text = "Annuleer"
        Me._cancelButton.UseVisualStyleBackColor = True
        '
        'NewGameForm
        '
        Me.AcceptButton = Me._startButton
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me._cancelButton
        Me.ClientSize = New System.Drawing.Size(539, 353)
        Me.ControlBox = False
        Me.Controls.Add(Me._cancelButton)
        Me.Controls.Add(Me._startButton)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me._player4)
        Me.Controls.Add(Me._player3)
        Me.Controls.Add(Me._player2)
        Me.Controls.Add(Me._player1)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me._difficulties)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "NewGameForm"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Raad je plaatje"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents _difficulties As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents _player1 As System.Windows.Forms.ComboBox
    Friend WithEvents _player2 As System.Windows.Forms.ComboBox
    Friend WithEvents _player3 As System.Windows.Forms.ComboBox
    Friend WithEvents _player4 As System.Windows.Forms.ComboBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents _startButton As System.Windows.Forms.Button
    Friend WithEvents _cancelButton As System.Windows.Forms.Button
End Class
