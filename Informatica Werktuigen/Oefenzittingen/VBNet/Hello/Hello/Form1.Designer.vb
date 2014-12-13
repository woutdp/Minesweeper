<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmHalloWereld
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
        Me.btnEngels = New System.Windows.Forms.Button()
        Me.btnNederlands = New System.Windows.Forms.Button()
        Me.btnStop = New System.Windows.Forms.Button()
        Me.lblOutput = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'btnEngels
        '
        Me.btnEngels.Location = New System.Drawing.Point(42, 142)
        Me.btnEngels.Name = "btnEngels"
        Me.btnEngels.Size = New System.Drawing.Size(113, 37)
        Me.btnEngels.TabIndex = 0
        Me.btnEngels.Text = "Engels"
        Me.btnEngels.UseVisualStyleBackColor = True
        '
        'btnNederlands
        '
        Me.btnNederlands.Location = New System.Drawing.Point(161, 142)
        Me.btnNederlands.Name = "btnNederlands"
        Me.btnNederlands.Size = New System.Drawing.Size(140, 37)
        Me.btnNederlands.TabIndex = 1
        Me.btnNederlands.Text = "Nederlands"
        Me.btnNederlands.UseVisualStyleBackColor = True
        '
        'btnStop
        '
        Me.btnStop.Location = New System.Drawing.Point(307, 142)
        Me.btnStop.Name = "btnStop"
        Me.btnStop.Size = New System.Drawing.Size(140, 37)
        Me.btnStop.TabIndex = 2
        Me.btnStop.Text = "Stop"
        Me.btnStop.UseVisualStyleBackColor = True
        '
        'lblOutput
        '
        Me.lblOutput.Location = New System.Drawing.Point(39, 26)
        Me.lblOutput.Name = "lblOutput"
        Me.lblOutput.Size = New System.Drawing.Size(408, 85)
        Me.lblOutput.TabIndex = 3
        Me.lblOutput.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'frmHalloWereld
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(498, 216)
        Me.Controls.Add(Me.lblOutput)
        Me.Controls.Add(Me.btnStop)
        Me.Controls.Add(Me.btnNederlands)
        Me.Controls.Add(Me.btnEngels)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "frmHalloWereld"
        Me.Text = "Hallo Wereld!"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnEngels As System.Windows.Forms.Button
    Friend WithEvents btnNederlands As System.Windows.Forms.Button
    Friend WithEvents btnStop As System.Windows.Forms.Button
    Friend WithEvents lblOutput As System.Windows.Forms.Label

End Class
