<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmDigitalBBTwo
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmDigitalBBTwo))
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.flowPanel = New System.Windows.Forms.FlowLayoutPanel()
        Me.flowPanelTwo = New System.Windows.Forms.FlowLayoutPanel()
        Me.lblNow = New System.Windows.Forms.Label()
        Me.lblLogo = New System.Windows.Forms.PictureBox()
        Me.TimerDelay = New System.Windows.Forms.Timer(Me.components)
        Me.bgwFull = New System.ComponentModel.BackgroundWorker()
        Me.bgwLoad = New System.ComponentModel.BackgroundWorker()
        Me.bgwW = New System.ComponentModel.BackgroundWorker()
        Me.bgwN = New System.ComponentModel.BackgroundWorker()
        Me.FullPictureBox = New Project.AnimationControl()
        Me.PictureBox1 = New Project.AnimationControl()
        Me.lblNews = New Project.MarqueeLabel()
        CType(Me.lblLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Timer1
        '
        Me.Timer1.Interval = 2000
        '
        'flowPanel
        '
        Me.flowPanel.BackColor = System.Drawing.Color.Transparent
        Me.flowPanel.Location = New System.Drawing.Point(293, 0)
        Me.flowPanel.Name = "flowPanel"
        Me.flowPanel.Size = New System.Drawing.Size(160, 487)
        Me.flowPanel.TabIndex = 17
        '
        'flowPanelTwo
        '
        Me.flowPanelTwo.BackColor = System.Drawing.Color.Transparent
        Me.flowPanelTwo.Location = New System.Drawing.Point(293, 0)
        Me.flowPanelTwo.Name = "flowPanelTwo"
        Me.flowPanelTwo.Size = New System.Drawing.Size(160, 487)
        Me.flowPanelTwo.TabIndex = 17
        '
        'lblNow
        '
        Me.lblNow.AutoSize = True
        Me.lblNow.BackColor = System.Drawing.Color.Transparent
        Me.lblNow.Font = New System.Drawing.Font("Palatino Linotype", 17.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNow.ForeColor = System.Drawing.Color.Teal
        Me.lblNow.Location = New System.Drawing.Point(14, 431)
        Me.lblNow.Name = "lblNow"
        Me.lblNow.Size = New System.Drawing.Size(0, 31)
        Me.lblNow.TabIndex = 18
        '
        'lblLogo
        '
        Me.lblLogo.BackColor = System.Drawing.Color.Transparent
        Me.lblLogo.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLogo.Image = CType(resources.GetObject("lblLogo.Image"), System.Drawing.Image)
        Me.lblLogo.Location = New System.Drawing.Point(46, 410)
        Me.lblLogo.Name = "lblLogo"
        Me.lblLogo.Size = New System.Drawing.Size(125, 116)
        Me.lblLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.lblLogo.TabIndex = 20
        Me.lblLogo.TabStop = False
        '
        'TimerDelay
        '
        Me.TimerDelay.Interval = 3000
        '
        'bgwLoad
        '
        '
        'bgwW
        '
        Me.bgwW.WorkerReportsProgress = True
        '
        'bgwN
        '
        Me.bgwN.WorkerReportsProgress = True
        '
        'FullPictureBox
        '
        Me.FullPictureBox.AnimationType = Project.AnimationTypes.RighTotLeft
        Me.FullPictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.FullPictureBox.Location = New System.Drawing.Point(0, 0)
        Me.FullPictureBox.MinimumSize = New System.Drawing.Size(100, 100)
        Me.FullPictureBox.Name = "FullPictureBox"
        Me.FullPictureBox.Opacity = 0.0R
        Me.FullPictureBox.Size = New System.Drawing.Size(287, 369)
        Me.FullPictureBox.TabIndex = 25
        Me.FullPictureBox.Text = "AnimationControl1"
        Me.FullPictureBox.Transparent = True
        Me.FullPictureBox.Visible = False
        '
        'PictureBox1
        '
        Me.PictureBox1.AnimationType = Project.AnimationTypes.RighTotLeft
        Me.PictureBox1.Location = New System.Drawing.Point(0, 0)
        Me.PictureBox1.MinimumSize = New System.Drawing.Size(100, 100)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Opacity = 0.0R
        Me.PictureBox1.Size = New System.Drawing.Size(287, 180)
        Me.PictureBox1.TabIndex = 27
        Me.PictureBox1.Transparent = True
        '
        'lblNews
        '
        Me.lblNews.BackColor = System.Drawing.Color.Transparent
        Me.lblNews.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNews.Location = New System.Drawing.Point(0, 487)
        Me.lblNews.Name = "lblNews"
        Me.lblNews.NewsColor = System.Drawing.SystemColors.ControlText
        Me.lblNews.Size = New System.Drawing.Size(453, 39)
        Me.lblNews.TabIndex = 22
        Me.lblNews.Text = " "
        '
        'FrmDigitalBB
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Wheat
        Me.ClientSize = New System.Drawing.Size(453, 526)
        Me.Controls.Add(Me.lblLogo)
        Me.Controls.Add(Me.FullPictureBox)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.lblNews)
        Me.Controls.Add(Me.lblNow)
        Me.Controls.Add(Me.flowPanel)
        Me.Controls.Add(Me.flowPanelTwo)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "FrmDigitalBB"
        Me.Text = "Digital Board"
        CType(Me.lblLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents flowPanel As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents flowPanelTwo As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents lblNow As System.Windows.Forms.Label
    Friend WithEvents lblLogo As System.Windows.Forms.PictureBox
    Friend WithEvents lblNews As MarqueeLabel
    Friend WithEvents TimerDelay As System.Windows.Forms.Timer
    Friend WithEvents PictureBox1 As Project.AnimationControl
    Friend WithEvents FullPictureBox As Project.AnimationControl
    Friend WithEvents bgwFull As System.ComponentModel.BackgroundWorker
    Friend WithEvents bgwLoad As System.ComponentModel.BackgroundWorker
    Friend WithEvents bgwW As System.ComponentModel.BackgroundWorker
    Friend WithEvents bgwN As System.ComponentModel.BackgroundWorker
End Class
