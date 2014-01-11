<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAdmin
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
        Me.txtPasswd = New System.Windows.Forms.TextBox()
        Me.btnBgColor = New System.Windows.Forms.Button()
        Me.btnLogo = New System.Windows.Forms.Button()
        Me.ColorDialog1 = New System.Windows.Forms.ColorDialog()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.btnWL = New System.Windows.Forms.Button()
        Me.btnHL = New System.Windows.Forms.Button()
        Me.btnHLB = New System.Windows.Forms.Button()
        Me.ColorDialog2 = New System.Windows.Forms.ColorDialog()
        Me.ColorDialog3 = New System.Windows.Forms.ColorDialog()
        Me.btnWeatherColor = New System.Windows.Forms.Button()
        Me.btnNewsColor = New System.Windows.Forms.Button()
        Me.ColorDialog4 = New System.Windows.Forms.ColorDialog()
        Me.ColorDialog5 = New System.Windows.Forms.ColorDialog()
        Me.cbxTM = New System.Windows.Forms.CheckBox()
        Me.cbxTS = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(154, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Please Enter Admin Password: "
        '
        'txtPasswd
        '
        Me.txtPasswd.Location = New System.Drawing.Point(15, 25)
        Me.txtPasswd.Name = "txtPasswd"
        Me.txtPasswd.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPasswd.Size = New System.Drawing.Size(186, 20)
        Me.txtPasswd.TabIndex = 1
        '
        'btnBgColor
        '
        Me.btnBgColor.Location = New System.Drawing.Point(15, 62)
        Me.btnBgColor.Name = "btnBgColor"
        Me.btnBgColor.Size = New System.Drawing.Size(114, 23)
        Me.btnBgColor.TabIndex = 2
        Me.btnBgColor.Text = "Background Color"
        Me.btnBgColor.UseVisualStyleBackColor = True
        Me.btnBgColor.Visible = False
        '
        'btnLogo
        '
        Me.btnLogo.Location = New System.Drawing.Point(15, 91)
        Me.btnLogo.Name = "btnLogo"
        Me.btnLogo.Size = New System.Drawing.Size(114, 23)
        Me.btnLogo.TabIndex = 3
        Me.btnLogo.Text = "Logo"
        Me.btnLogo.UseVisualStyleBackColor = True
        Me.btnLogo.Visible = False
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.DefaultExt = "jpg"
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        Me.OpenFileDialog1.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Fi" & _
    "les (*.gif)|*.gif |ICO Files (*.ico)|*.ico"
        '
        'btnWL
        '
        Me.btnWL.Location = New System.Drawing.Point(15, 121)
        Me.btnWL.Name = "btnWL"
        Me.btnWL.Size = New System.Drawing.Size(114, 23)
        Me.btnWL.TabIndex = 4
        Me.btnWL.Text = "Weather Location"
        Me.btnWL.UseVisualStyleBackColor = True
        Me.btnWL.Visible = False
        '
        'btnHL
        '
        Me.btnHL.Location = New System.Drawing.Point(15, 179)
        Me.btnHL.Name = "btnHL"
        Me.btnHL.Size = New System.Drawing.Size(114, 23)
        Me.btnHL.TabIndex = 5
        Me.btnHL.Text = "Highlight"
        Me.btnHL.UseVisualStyleBackColor = True
        Me.btnHL.Visible = False
        '
        'btnHLB
        '
        Me.btnHLB.Location = New System.Drawing.Point(15, 209)
        Me.btnHLB.Name = "btnHLB"
        Me.btnHLB.Size = New System.Drawing.Size(114, 23)
        Me.btnHLB.TabIndex = 6
        Me.btnHLB.Text = "Highlight Border"
        Me.btnHLB.UseVisualStyleBackColor = True
        Me.btnHLB.Visible = False
        '
        'btnWeatherColor
        '
        Me.btnWeatherColor.Location = New System.Drawing.Point(15, 150)
        Me.btnWeatherColor.Name = "btnWeatherColor"
        Me.btnWeatherColor.Size = New System.Drawing.Size(114, 23)
        Me.btnWeatherColor.TabIndex = 7
        Me.btnWeatherColor.Text = "Weather Color"
        Me.btnWeatherColor.UseVisualStyleBackColor = True
        Me.btnWeatherColor.Visible = False
        '
        'btnNewsColor
        '
        Me.btnNewsColor.Location = New System.Drawing.Point(15, 239)
        Me.btnNewsColor.Name = "btnNewsColor"
        Me.btnNewsColor.Size = New System.Drawing.Size(114, 23)
        Me.btnNewsColor.TabIndex = 8
        Me.btnNewsColor.Text = "News Color"
        Me.btnNewsColor.UseVisualStyleBackColor = True
        Me.btnNewsColor.Visible = False
        '
        'cbxTM
        '
        Me.cbxTM.Location = New System.Drawing.Point(153, 62)
        Me.cbxTM.Name = "cbxTM"
        Me.cbxTM.Size = New System.Drawing.Size(114, 23)
        Me.cbxTM.TabIndex = 9
        Me.cbxTM.Text = "Two Menus"
        Me.cbxTM.UseVisualStyleBackColor = True
        '
        'cbxTS
        '
        Me.cbxTS.Location = New System.Drawing.Point(153, 91)
        Me.cbxTS.Name = "cbxTS"
        Me.cbxTS.Size = New System.Drawing.Size(114, 23)
        Me.cbxTS.TabIndex = 9
        Me.cbxTS.Text = "Two SlideShows"
        Me.cbxTS.UseVisualStyleBackColor = True
        '
        'frmAdmin
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(421, 462)
        Me.Controls.Add(Me.cbxTS)
        Me.Controls.Add(Me.cbxTM)
        Me.Controls.Add(Me.btnNewsColor)
        Me.Controls.Add(Me.btnWeatherColor)
        Me.Controls.Add(Me.btnHLB)
        Me.Controls.Add(Me.btnHL)
        Me.Controls.Add(Me.btnWL)
        Me.Controls.Add(Me.btnLogo)
        Me.Controls.Add(Me.btnBgColor)
        Me.Controls.Add(Me.txtPasswd)
        Me.Controls.Add(Me.Label1)
        Me.Name = "frmAdmin"
        Me.Text = "Admin"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtPasswd As System.Windows.Forms.TextBox
    Friend WithEvents btnBgColor As System.Windows.Forms.Button
    Friend WithEvents btnLogo As System.Windows.Forms.Button
    Friend WithEvents ColorDialog1 As System.Windows.Forms.ColorDialog
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents btnWL As System.Windows.Forms.Button
    Friend WithEvents btnHL As System.Windows.Forms.Button
    Friend WithEvents btnHLB As System.Windows.Forms.Button
    Friend WithEvents ColorDialog2 As System.Windows.Forms.ColorDialog
    Friend WithEvents ColorDialog3 As System.Windows.Forms.ColorDialog
    Friend WithEvents btnWeatherColor As System.Windows.Forms.Button
    Friend WithEvents btnNewsColor As System.Windows.Forms.Button
    Friend WithEvents ColorDialog4 As System.Windows.Forms.ColorDialog
    Friend WithEvents ColorDialog5 As System.Windows.Forms.ColorDialog
    Friend WithEvents cbxTM As System.Windows.Forms.CheckBox
    Friend WithEvents cbxTS As System.Windows.Forms.CheckBox
End Class
