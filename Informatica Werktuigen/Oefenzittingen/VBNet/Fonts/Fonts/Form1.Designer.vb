<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Me.boxType = New System.Windows.Forms.GroupBox()
        Me.radDoorstreept = New System.Windows.Forms.RadioButton()
        Me.radOnderlijnd = New System.Windows.Forms.RadioButton()
        Me.radCursief = New System.Windows.Forms.RadioButton()
        Me.radVet = New System.Windows.Forms.RadioButton()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.lstSize = New System.Windows.Forms.ListBox()
        Me.txtOutput = New System.Windows.Forms.TextBox()
        Me.btnInput = New System.Windows.Forms.Button()
        Me.btnEinde = New System.Windows.Forms.Button()
        Me.boxType.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'boxType
        '
        Me.boxType.Controls.Add(Me.radDoorstreept)
        Me.boxType.Controls.Add(Me.radOnderlijnd)
        Me.boxType.Controls.Add(Me.radCursief)
        Me.boxType.Controls.Add(Me.radVet)
        Me.boxType.Location = New System.Drawing.Point(16, 101)
        Me.boxType.Name = "boxType"
        Me.boxType.Size = New System.Drawing.Size(203, 134)
        Me.boxType.TabIndex = 0
        Me.boxType.TabStop = False
        Me.boxType.Text = "Lettertypestijl"
        '
        'radDoorstreept
        '
        Me.radDoorstreept.AutoSize = True
        Me.radDoorstreept.Location = New System.Drawing.Point(6, 102)
        Me.radDoorstreept.Name = "radDoorstreept"
        Me.radDoorstreept.Size = New System.Drawing.Size(104, 21)
        Me.radDoorstreept.TabIndex = 8
        Me.radDoorstreept.TabStop = True
        Me.radDoorstreept.Text = "Doorstreept"
        Me.radDoorstreept.UseVisualStyleBackColor = True
        '
        'radOnderlijnd
        '
        Me.radOnderlijnd.AutoSize = True
        Me.radOnderlijnd.Location = New System.Drawing.Point(6, 75)
        Me.radOnderlijnd.Name = "radOnderlijnd"
        Me.radOnderlijnd.Size = New System.Drawing.Size(94, 21)
        Me.radOnderlijnd.TabIndex = 7
        Me.radOnderlijnd.TabStop = True
        Me.radOnderlijnd.Text = "Onderlijnd"
        Me.radOnderlijnd.UseVisualStyleBackColor = True
        '
        'radCursief
        '
        Me.radCursief.AutoSize = True
        Me.radCursief.Location = New System.Drawing.Point(6, 48)
        Me.radCursief.Name = "radCursief"
        Me.radCursief.Size = New System.Drawing.Size(73, 21)
        Me.radCursief.TabIndex = 6
        Me.radCursief.TabStop = True
        Me.radCursief.Text = "Cursief"
        Me.radCursief.UseVisualStyleBackColor = True
        '
        'radVet
        '
        Me.radVet.AutoSize = True
        Me.radVet.Location = New System.Drawing.Point(6, 21)
        Me.radVet.Name = "radVet"
        Me.radVet.Size = New System.Drawing.Size(50, 21)
        Me.radVet.TabIndex = 5
        Me.radVet.TabStop = True
        Me.radVet.Text = "Vet"
        Me.radVet.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.lstSize)
        Me.GroupBox2.Location = New System.Drawing.Point(237, 101)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(203, 134)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Lettertypegrootte"
        '
        'lstSize
        '
        Me.lstSize.FormattingEnabled = True
        Me.lstSize.ItemHeight = 16
        Me.lstSize.Items.AddRange(New Object() {"8", "10", "12", "14", "16", "18"})
        Me.lstSize.Location = New System.Drawing.Point(6, 21)
        Me.lstSize.Name = "lstSize"
        Me.lstSize.Size = New System.Drawing.Size(191, 100)
        Me.lstSize.TabIndex = 0
        '
        'txtOutput
        '
        Me.txtOutput.Location = New System.Drawing.Point(71, 30)
        Me.txtOutput.Name = "txtOutput"
        Me.txtOutput.ReadOnly = True
        Me.txtOutput.Size = New System.Drawing.Size(311, 22)
        Me.txtOutput.TabIndex = 2
        '
        'btnInput
        '
        Me.btnInput.Location = New System.Drawing.Point(16, 252)
        Me.btnInput.Name = "btnInput"
        Me.btnInput.Size = New System.Drawing.Size(203, 47)
        Me.btnInput.TabIndex = 3
        Me.btnInput.Text = "Geef tekst in"
        Me.btnInput.UseVisualStyleBackColor = True
        '
        'btnEinde
        '
        Me.btnEinde.Location = New System.Drawing.Point(237, 252)
        Me.btnEinde.Name = "btnEinde"
        Me.btnEinde.Size = New System.Drawing.Size(203, 47)
        Me.btnEinde.TabIndex = 4
        Me.btnEinde.Text = "Einde"
        Me.btnEinde.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(457, 312)
        Me.Controls.Add(Me.btnEinde)
        Me.Controls.Add(Me.btnInput)
        Me.Controls.Add(Me.txtOutput)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.boxType)
        Me.Name = "Form1"
        Me.Text = "Fonts"
        Me.boxType.ResumeLayout(False)
        Me.boxType.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents boxType As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents txtOutput As System.Windows.Forms.TextBox
    Friend WithEvents btnInput As System.Windows.Forms.Button
    Friend WithEvents btnEinde As System.Windows.Forms.Button
    Friend WithEvents radDoorstreept As System.Windows.Forms.RadioButton
    Friend WithEvents radOnderlijnd As System.Windows.Forms.RadioButton
    Friend WithEvents radCursief As System.Windows.Forms.RadioButton
    Friend WithEvents radVet As System.Windows.Forms.RadioButton
    Friend WithEvents lstSize As System.Windows.Forms.ListBox

    Private Sub btnEinde_Click(sender As Object, e As EventArgs) Handles btnEinde.Click
        Application.Exit()
    End Sub
    Private Sub btnInput_Click(sender As Object, e As EventArgs) Handles btnInput.Click
        txtOutput.ReadOnly = Not txtOutput.ReadOnly
    End Sub
    Public Function UpdateForm()
        Dim fontStyle As FontStyle
        For Each Ctrl In boxType.Controls
            If Ctrl.checked Then
                Select Case Ctrl.Name
                    Case "radVet"
                        fontStyle = fontStyle.Bold
                    Case "radCursief"
                        fontStyle = fontStyle.Italic
                    Case "radOnderlijnd"
                        fontStyle = fontStyle.Underline
                    Case "radDoorstreept"
                        fontStyle = fontStyle.Strikeout
                End Select
            End If
        Next
        MsgBox(CInt(lstSize.SelectedIndex))
        txtOutput.Font = New Drawing.Font("Arial", 12, fontStyle)
    End Function
    Private Sub radVet_CheckedChanged(sender As Object, e As EventArgs) Handles radVet.CheckedChanged
        UpdateForm()
    End Sub
    Private Sub radCursief_CheckedChanged(sender As Object, e As EventArgs) Handles radCursief.CheckedChanged
        UpdateForm()
    End Sub
    Private Sub radOnderlijnd_CheckedChanged(sender As Object, e As EventArgs) Handles radOnderlijnd.CheckedChanged
        UpdateForm()
    End Sub
    Private Sub radDoorstreept_CheckedChanged(sender As Object, e As EventArgs) Handles radDoorstreept.CheckedChanged
        UpdateForm()
    End Sub
    Private Sub lstSize_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstSize.SelectedIndexChanged

    End Sub
End Class
