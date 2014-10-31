﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GameForm
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
        Me.components = New System.ComponentModel.Container()
        Me._timer = New System.Windows.Forms.Timer(Me.components)
        Me._menu = New System.Windows.Forms.MenuStrip()
        Me.GameToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StartNewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me._aboutMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me._scorePane = New Memory.ScorePane()
        Me._boardControl = New Memory.BoardControl()
        Me._menu.SuspendLayout()
        Me.SuspendLayout()
        '
        '_timer
        '
        Me._timer.Enabled = True
        '
        '_menu
        '
        Me._menu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.GameToolStripMenuItem, Me.AboutToolStripMenuItem})
        Me._menu.Location = New System.Drawing.Point(0, 0)
        Me._menu.Name = "_menu"
        Me._menu.Padding = New System.Windows.Forms.Padding(8, 2, 0, 2)
        Me._menu.Size = New System.Drawing.Size(887, 28)
        Me._menu.TabIndex = 2
        Me._menu.Text = "_menu"
        '
        'GameToolStripMenuItem
        '
        Me.GameToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.StartNewToolStripMenuItem, Me.ToolStripMenuItem1, Me.ExitToolStripMenuItem})
        Me.GameToolStripMenuItem.Name = "GameToolStripMenuItem"
        Me.GameToolStripMenuItem.Size = New System.Drawing.Size(60, 24)
        Me.GameToolStripMenuItem.Text = "&Game"
        '
        'StartNewToolStripMenuItem
        '
        Me.StartNewToolStripMenuItem.Name = "StartNewToolStripMenuItem"
        Me.StartNewToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2
        Me.StartNewToolStripMenuItem.Size = New System.Drawing.Size(164, 24)
        Me.StartNewToolStripMenuItem.Text = "Start &new"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(161, 6)
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(164, 24)
        Me.ExitToolStripMenuItem.Text = "E&xit"
        '
        'AboutToolStripMenuItem
        '
        Me.AboutToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me._aboutMenuItem})
        Me.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
        Me.AboutToolStripMenuItem.Size = New System.Drawing.Size(62, 24)
        Me.AboutToolStripMenuItem.Text = "&About"
        '
        '_aboutMenuItem
        '
        Me._aboutMenuItem.Name = "_aboutMenuItem"
        Me._aboutMenuItem.Size = New System.Drawing.Size(119, 24)
        Me._aboutMenuItem.Text = "&About"
        '
        '_scorePane
        '
        Me._scorePane.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me._scorePane.BackColor = System.Drawing.SystemColors.Control
        Me._scorePane.Location = New System.Drawing.Point(16, 33)
        Me._scorePane.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me._scorePane.Name = "_scorePane"
        Me._scorePane.Size = New System.Drawing.Size(113, 517)
        Me._scorePane.TabIndex = 1
        Me._scorePane.Visible = False
        '
        '_boardControl
        '
        Me._boardControl.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me._boardControl.BackColor = System.Drawing.SystemColors.ControlDark
        Me._boardControl.Grid = Nothing
        Me._boardControl.Location = New System.Drawing.Point(137, 33)
        Me._boardControl.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me._boardControl.Name = "_boardControl"
        Me._boardControl.Size = New System.Drawing.Size(733, 517)
        Me._boardControl.TabIndex = 0
        Me._boardControl.Text = "BoardControl1"
        Me._boardControl.Visible = False
        '
        'GameForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(887, 565)
        Me.Controls.Add(Me._scorePane)
        Me.Controls.Add(Me._boardControl)
        Me.Controls.Add(Me._menu)
        Me.MainMenuStrip = Me._menu
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "GameForm"
        Me.Text = "Memory"
        Me._menu.ResumeLayout(False)
        Me._menu.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents _boardControl As Memory.BoardControl
    Friend WithEvents _timer As System.Windows.Forms.Timer
    Friend WithEvents _scorePane As Memory.ScorePane
    Friend WithEvents _menu As System.Windows.Forms.MenuStrip
    Friend WithEvents GameToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StartNewToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AboutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents _aboutMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
