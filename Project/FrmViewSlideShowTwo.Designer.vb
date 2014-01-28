<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmViewSlideShowTwo
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
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.TimerDelay = New System.Windows.Forms.Timer(Me.components)
        Me.cmbAnimationType = New System.Windows.Forms.ComboBox()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.trkOne = New System.Windows.Forms.TrackBar()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.bgw = New System.ComponentModel.BackgroundWorker()
        Me.PictureBox1 = New Project.AnimationControl()
        CType(Me.trkOne, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Timer1
        '
        Me.Timer1.Interval = 2000
        '
        'TimerDelay
        '
        Me.TimerDelay.Interval = 2000
        '
        'cmbAnimationType
        '
        Me.cmbAnimationType.FormattingEnabled = True
        Me.cmbAnimationType.Location = New System.Drawing.Point(12, 318)
        Me.cmbAnimationType.Name = "cmbAnimationType"
        Me.cmbAnimationType.Size = New System.Drawing.Size(253, 21)
        Me.cmbAnimationType.TabIndex = 1
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(281, 318)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(68, 21)
        Me.btnAdd.TabIndex = 2
        Me.btnAdd.Text = "Add"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(12, 372)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(95, 15)
        Me.Label4.TabIndex = 30
        Me.Label4.Text = "Slide Frequency"
        '
        'trkOne
        '
        Me.trkOne.LargeChange = 2
        Me.trkOne.Location = New System.Drawing.Point(113, 361)
        Me.trkOne.Maximum = 20
        Me.trkOne.Minimum = 6
        Me.trkOne.Name = "trkOne"
        Me.trkOne.Size = New System.Drawing.Size(215, 45)
        Me.trkOne.TabIndex = 29
        Me.trkOne.TickStyle = System.Windows.Forms.TickStyle.TopLeft
        Me.trkOne.Value = 6
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(110, 342)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(35, 13)
        Me.Label1.TabIndex = 31
        Me.Label1.Text = "Quick"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(304, 342)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(30, 13)
        Me.Label2.TabIndex = 31
        Me.Label2.Text = "Slow"
        '
        'bgw
        '
        '
        'PictureBox1
        '
        Me.PictureBox1.AnimationType = Project.AnimationTypes.LeftToRight
        Me.PictureBox1.Location = New System.Drawing.Point(2, 1)
        Me.PictureBox1.MinimumSize = New System.Drawing.Size(100, 100)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Opacity = 0.0R
        Me.PictureBox1.Size = New System.Drawing.Size(366, 295)
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.Text = "AnimationControl1"
        Me.PictureBox1.Transparent = True
        '
        'FrmViewSlideShowTwo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.ClientSize = New System.Drawing.Size(370, 452)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.trkOne)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.cmbAnimationType)
        Me.Controls.Add(Me.PictureBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmViewSlideShowTwo"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Slide Show"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.trkOne, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents PictureBox1 As Project.AnimationControl
    Friend WithEvents TimerDelay As System.Windows.Forms.Timer
    Friend WithEvents cmbAnimationType As System.Windows.Forms.ComboBox
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents trkOne As System.Windows.Forms.TrackBar
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents bgw As System.ComponentModel.BackgroundWorker
End Class
