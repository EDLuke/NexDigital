<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSpecials
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
        Me.lstvSpecials = New System.Windows.Forms.ListView
        Me.SuspendLayout()
        '
        'lstvSpecials
        '
        Me.lstvSpecials.Location = New System.Drawing.Point(3, 12)
        Me.lstvSpecials.Name = "lstvSpecials"
        Me.lstvSpecials.Size = New System.Drawing.Size(475, 645)
        Me.lstvSpecials.TabIndex = 1
        Me.lstvSpecials.UseCompatibleStateImageBehavior = False
        '
        'frmSpecials
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(482, 653)
        Me.Controls.Add(Me.lstvSpecials)
        Me.Name = "frmSpecials"
        Me.Text = "Today's Specials"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lstvSpecials As System.Windows.Forms.ListView
End Class
