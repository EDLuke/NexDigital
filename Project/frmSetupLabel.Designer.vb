<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSetupLabel
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnChangeColor = New System.Windows.Forms.Button()
        Me.ColorDialog1 = New System.Windows.Forms.ColorDialog()
        Me.FontDialog1 = New System.Windows.Forms.FontDialog()
        Me.btnChangeFont = New System.Windows.Forms.Button()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnChangeDone = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(33, 76)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(210, 47)
        Me.Label1.TabIndex = 0
        '
        'btnChangeColor
        '
        Me.btnChangeColor.Location = New System.Drawing.Point(86, 135)
        Me.btnChangeColor.Name = "btnChangeColor"
        Me.btnChangeColor.Size = New System.Drawing.Size(107, 23)
        Me.btnChangeColor.TabIndex = 1
        Me.btnChangeColor.Text = "Color"
        Me.btnChangeColor.UseVisualStyleBackColor = True
        '
        'btnChangeFont
        '
        Me.btnChangeFont.Location = New System.Drawing.Point(86, 164)
        Me.btnChangeFont.Name = "btnChangeFont"
        Me.btnChangeFont.Size = New System.Drawing.Size(107, 23)
        Me.btnChangeFont.TabIndex = 2
        Me.btnChangeFont.Text = "Font"
        Me.btnChangeFont.UseVisualStyleBackColor = True
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(34, 26)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(128, 28)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Preview:"
        '
        'btnChangeDone
        '
        Me.btnChangeDone.Location = New System.Drawing.Point(101, 215)
        Me.btnChangeDone.Name = "btnChangeDone"
        Me.btnChangeDone.Size = New System.Drawing.Size(75, 23)
        Me.btnChangeDone.TabIndex = 4
        Me.btnChangeDone.Text = "Done"
        Me.btnChangeDone.UseVisualStyleBackColor = True
        '
        'frmSetupLabel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 261)
        Me.Controls.Add(Me.btnChangeDone)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.btnChangeFont)
        Me.Controls.Add(Me.btnChangeColor)
        Me.Controls.Add(Me.Label1)
        Me.Name = "frmSetupLabel"
        Me.Text = "frmSetupLabel"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnChangeColor As System.Windows.Forms.Button
    Friend WithEvents ColorDialog1 As System.Windows.Forms.ColorDialog
    Friend WithEvents FontDialog1 As System.Windows.Forms.FontDialog
    Friend WithEvents btnChangeFont As System.Windows.Forms.Button
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnChangeDone As System.Windows.Forms.Button
End Class
