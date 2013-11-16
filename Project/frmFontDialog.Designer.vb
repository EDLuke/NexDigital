<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFontDialog
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
        Me.lstFont = New System.Windows.Forms.ListBox()
        Me.lstFontStyle = New System.Windows.Forms.ListBox()
        Me.lstFontSize = New System.Windows.Forms.ListBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.flowPanel = New System.Windows.Forms.FlowLayoutPanel()
        Me.btnOk = New System.Windows.Forms.Button()
        Me.lblSel = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lstFont
        '
        Me.lstFont.FormattingEnabled = True
        Me.lstFont.Location = New System.Drawing.Point(8, 25)
        Me.lstFont.Name = "lstFont"
        Me.lstFont.Size = New System.Drawing.Size(171, 173)
        Me.lstFont.TabIndex = 0
        '
        'lstFontStyle
        '
        Me.lstFontStyle.FormattingEnabled = True
        Me.lstFontStyle.Location = New System.Drawing.Point(185, 25)
        Me.lstFontStyle.Name = "lstFontStyle"
        Me.lstFontStyle.Size = New System.Drawing.Size(113, 173)
        Me.lstFontStyle.TabIndex = 1
        '
        'lstFontSize
        '
        Me.lstFontSize.FormattingEnabled = True
        Me.lstFontSize.Location = New System.Drawing.Point(304, 25)
        Me.lstFontSize.Name = "lstFontSize"
        Me.lstFontSize.Size = New System.Drawing.Size(76, 173)
        Me.lstFontSize.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(5, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(31, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Font:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(185, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(57, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Font Style:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(304, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(30, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Size:"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.flowPanel)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(5, 218)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(379, 206)
        Me.GroupBox1.TabIndex = 6
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Preview"
        '
        'flowPanel
        '
        Me.flowPanel.Location = New System.Drawing.Point(10, 20)
        Me.flowPanel.Name = "flowPanel"
        Me.flowPanel.Size = New System.Drawing.Size(358, 166)
        Me.flowPanel.TabIndex = 0
        '
        'btnOk
        '
        Me.btnOk.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOk.Location = New System.Drawing.Point(223, 432)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(75, 33)
        Me.btnOk.TabIndex = 7
        Me.btnOk.Text = "Change"
        Me.btnOk.UseVisualStyleBackColor = True
        '
        'lblSel
        '
        Me.lblSel.AutoSize = True
        Me.lblSel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSel.Location = New System.Drawing.Point(12, 440)
        Me.lblSel.Name = "lblSel"
        Me.lblSel.Size = New System.Drawing.Size(0, 16)
        Me.lblSel.TabIndex = 8
        '
        'frmFontDialog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(423, 510)
        Me.Controls.Add(Me.lblSel)
        Me.Controls.Add(Me.btnOk)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lstFontSize)
        Me.Controls.Add(Me.lstFontStyle)
        Me.Controls.Add(Me.lstFont)
        Me.Name = "frmFontDialog"
        Me.Text = "Font"
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lstFont As System.Windows.Forms.ListBox
    Friend WithEvents lstFontStyle As System.Windows.Forms.ListBox
    Friend WithEvents lstFontSize As System.Windows.Forms.ListBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnOk As System.Windows.Forms.Button
    Friend WithEvents flowPanel As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents lblSel As System.Windows.Forms.Label
End Class
