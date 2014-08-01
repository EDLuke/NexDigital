<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainMenu
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
        Me.tabSec = New System.Windows.Forms.TabControl()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.btnStart = New System.Windows.Forms.Button()
        Me.btnFull = New System.Windows.Forms.CheckBox()
        Me.btnSec = New System.Windows.Forms.CheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.bgwLoad = New System.ComponentModel.BackgroundWorker()
        Me.tabMenuSetup = New System.Windows.Forms.TabPage()
        Me.tabMenuSetupTwo = New System.Windows.Forms.TabPage()
        Me.tabItemSetup = New System.Windows.Forms.TabPage()
        Me.tabMain = New System.Windows.Forms.TabControl()
        Me.tabMgSlideOne = New System.Windows.Forms.TabPage()
        Me.lblTrial = New System.Windows.Forms.Label()
        Me.tabMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'tabSec
        '
        Me.tabSec.Appearance = System.Windows.Forms.TabAppearance.Buttons
        Me.tabSec.Location = New System.Drawing.Point(434, 12)
        Me.tabSec.Name = "tabSec"
        Me.tabSec.SelectedIndex = 0
        Me.tabSec.Size = New System.Drawing.Size(399, 500)
        Me.tabSec.TabIndex = 1
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(751, 520)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(84, 26)
        Me.btnExit.TabIndex = 2
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnStart
        '
        Me.btnStart.Location = New System.Drawing.Point(12, 520)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(84, 26)
        Me.btnStart.TabIndex = 3
        Me.btnStart.Text = "Show"
        Me.btnStart.UseVisualStyleBackColor = True
        '
        'btnFull
        '
        Me.btnFull.BackColor = System.Drawing.SystemColors.ControlDark
        Me.btnFull.Location = New System.Drawing.Point(106, 520)
        Me.btnFull.Name = "btnFull"
        Me.btnFull.Size = New System.Drawing.Size(84, 26)
        Me.btnFull.TabIndex = 4
        Me.btnFull.Text = "Full Screen"
        Me.btnFull.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnFull.UseVisualStyleBackColor = False
        '
        'btnSec
        '
        Me.btnSec.BackColor = System.Drawing.SystemColors.ControlDark
        Me.btnSec.Location = New System.Drawing.Point(200, 520)
        Me.btnSec.Name = "btnSec"
        Me.btnSec.Size = New System.Drawing.Size(102, 26)
        Me.btnSec.TabIndex = 5
        Me.btnSec.Text = "Second Screen"
        Me.btnSec.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Label1.Location = New System.Drawing.Point(308, 526)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(260, 13)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Press Alt + F4 to close Digital Board when Full-Screen"
        '
        'bgwLoad
        '
        '
        'tabMenuSetup
        '
        Me.tabMenuSetup.BackColor = System.Drawing.SystemColors.Control
        Me.tabMenuSetup.Location = New System.Drawing.Point(4, 25)
        Me.tabMenuSetup.Name = "tabMenuSetup"
        Me.tabMenuSetup.Padding = New System.Windows.Forms.Padding(3)
        Me.tabMenuSetup.Size = New System.Drawing.Size(408, 471)
        Me.tabMenuSetup.TabIndex = 3
        Me.tabMenuSetup.Text = "Menu Setup"
        '
        'tabMenuSetupTwo
        '
        Me.tabMenuSetupTwo.BackColor = System.Drawing.SystemColors.Control
        Me.tabMenuSetupTwo.Location = New System.Drawing.Point(4, 25)
        Me.tabMenuSetupTwo.Name = "tabMenuSetupTwo"
        Me.tabMenuSetupTwo.Padding = New System.Windows.Forms.Padding(3)
        Me.tabMenuSetupTwo.Size = New System.Drawing.Size(408, 471)
        Me.tabMenuSetupTwo.TabIndex = 4
        Me.tabMenuSetupTwo.Text = "Menu Setup Two"
        '
        'tabItemSetup
        '
        Me.tabItemSetup.BackColor = System.Drawing.SystemColors.Control
        Me.tabItemSetup.Location = New System.Drawing.Point(4, 25)
        Me.tabItemSetup.Name = "tabItemSetup"
        Me.tabItemSetup.Padding = New System.Windows.Forms.Padding(3)
        Me.tabItemSetup.Size = New System.Drawing.Size(408, 471)
        Me.tabItemSetup.TabIndex = 1
        Me.tabItemSetup.Text = "Items Setup"
        '
        'tabMain
        '
        Me.tabMain.Appearance = System.Windows.Forms.TabAppearance.Buttons
        Me.tabMain.Controls.Add(Me.tabItemSetup)
        Me.tabMain.Controls.Add(Me.tabMenuSetup)
        Me.tabMain.Controls.Add(Me.tabMenuSetupTwo)
        Me.tabMain.Controls.Add(Me.tabMgSlideOne)
        Me.tabMain.Location = New System.Drawing.Point(12, 12)
        Me.tabMain.Name = "tabMain"
        Me.tabMain.SelectedIndex = 0
        Me.tabMain.Size = New System.Drawing.Size(416, 500)
        Me.tabMain.TabIndex = 0
        '
        'tabMgSlideOne
        '
        Me.tabMgSlideOne.BackColor = System.Drawing.SystemColors.Control
        Me.tabMgSlideOne.Location = New System.Drawing.Point(4, 25)
        Me.tabMgSlideOne.Name = "tabMgSlideOne"
        Me.tabMgSlideOne.Padding = New System.Windows.Forms.Padding(3)
        Me.tabMgSlideOne.Size = New System.Drawing.Size(408, 471)
        Me.tabMgSlideOne.TabIndex = 5
        Me.tabMgSlideOne.Text = "Manage SlideShow One"
        '
        'lblTrial
        '
        Me.lblTrial.AutoSize = True
        Me.lblTrial.BackColor = System.Drawing.SystemColors.ControlDark
        Me.lblTrial.Location = New System.Drawing.Point(574, 526)
        Me.lblTrial.Name = "lblTrial"
        Me.lblTrial.Size = New System.Drawing.Size(0, 13)
        Me.lblTrial.TabIndex = 6
        '
        'MainMenu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(860, 555)
        Me.ControlBox = False
        Me.Controls.Add(Me.lblTrial)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnSec)
        Me.Controls.Add(Me.btnFull)
        Me.Controls.Add(Me.btnStart)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.tabSec)
        Me.Controls.Add(Me.tabMain)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.IsMdiContainer = True
        Me.Name = "MainMenu"
        Me.Text = "MainMenu"
        Me.tabMain.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tabSec As System.Windows.Forms.TabControl
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnStart As System.Windows.Forms.Button
    Friend WithEvents btnFull As System.Windows.Forms.CheckBox
    Friend WithEvents btnSec As System.Windows.Forms.CheckBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents bgwLoad As System.ComponentModel.BackgroundWorker
    Friend WithEvents tabMenuSetup As System.Windows.Forms.TabPage
    Friend WithEvents tabMenuSetupTwo As System.Windows.Forms.TabPage
    Friend WithEvents tabItemSetup As System.Windows.Forms.TabPage
    Friend WithEvents tabMain As System.Windows.Forms.TabControl
    Friend WithEvents tabMgSlideOne As System.Windows.Forms.TabPage
    Friend WithEvents lblTrial As System.Windows.Forms.Label

End Class
