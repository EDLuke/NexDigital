<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSetup
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
        Me.cmbCategories = New System.Windows.Forms.ComboBox()
        Me.CategoryBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.CategoriesFrmSetupDS = New Project.CategoriesFrmSetupDS()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnMoveDown = New System.Windows.Forms.Button()
        Me.btnMoveUp = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnRemove = New System.Windows.Forms.Button()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.lstItems = New System.Windows.Forms.ListBox()
        Me.lstMenuItems = New System.Windows.Forms.ListBox()
        Me.CategoryTableAdapter = New Project.CategoriesFrmSetupDSTableAdapters.CategoryTableAdapter()
        Me.btnName = New System.Windows.Forms.RadioButton()
        Me.btnPrice = New System.Windows.Forms.RadioButton()
        Me.btnDesp = New System.Windows.Forms.RadioButton()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.ToolTip2 = New System.Windows.Forms.ToolTip(Me.components)
        Me.ToolTip3 = New System.Windows.Forms.ToolTip(Me.components)
        Me.bgwLoadOne = New System.ComponentModel.BackgroundWorker()
        Me.bgwLoadTwo = New System.ComponentModel.BackgroundWorker()
        CType(Me.CategoryBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CategoriesFrmSetupDS, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmbCategories
        '
        Me.cmbCategories.DataSource = Me.CategoryBindingSource
        Me.cmbCategories.DisplayMember = "CategoryName"
        Me.cmbCategories.FormattingEnabled = True
        Me.cmbCategories.Location = New System.Drawing.Point(9, 32)
        Me.cmbCategories.Name = "cmbCategories"
        Me.cmbCategories.Size = New System.Drawing.Size(140, 21)
        Me.cmbCategories.TabIndex = 0
        Me.cmbCategories.ValueMember = "CategoryId"
        '
        'CategoryBindingSource
        '
        Me.CategoryBindingSource.DataMember = "Category"
        Me.CategoryBindingSource.DataSource = Me.CategoriesFrmSetupDS
        Me.CategoryBindingSource.Sort = "CategoryName"
        '
        'CategoriesFrmSetupDS
        '
        Me.CategoriesFrmSetupDS.DataSetName = "CategoriesFrmSetupDS"
        Me.CategoriesFrmSetupDS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnMoveDown)
        Me.GroupBox1.Controls.Add(Me.btnMoveUp)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.btnRemove)
        Me.GroupBox1.Controls.Add(Me.btnAdd)
        Me.GroupBox1.Controls.Add(Me.lstItems)
        Me.GroupBox1.Controls.Add(Me.cmbCategories)
        Me.GroupBox1.Controls.Add(Me.lstMenuItems)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(391, 391)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Select Menu Items"
        '
        'btnMoveDown
        '
        Me.btnMoveDown.Location = New System.Drawing.Point(340, 197)
        Me.btnMoveDown.Name = "btnMoveDown"
        Me.btnMoveDown.Size = New System.Drawing.Size(45, 23)
        Me.btnMoveDown.TabIndex = 9
        Me.btnMoveDown.Text = "ᐯ"
        Me.btnMoveDown.UseVisualStyleBackColor = True
        '
        'btnMoveUp
        '
        Me.btnMoveUp.Location = New System.Drawing.Point(340, 132)
        Me.btnMoveUp.Name = "btnMoveUp"
        Me.btnMoveUp.Size = New System.Drawing.Size(45, 23)
        Me.btnMoveUp.TabIndex = 8
        Me.btnMoveUp.Text = "ᐱ"
        Me.btnMoveUp.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(7, 16)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(85, 13)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Select Category:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(232, 56)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(65, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Menu Items:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 56)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(110, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Items in this Category:"
        '
        'btnRemove
        '
        Me.btnRemove.Location = New System.Drawing.Point(184, 197)
        Me.btnRemove.Name = "btnRemove"
        Me.btnRemove.Size = New System.Drawing.Size(45, 23)
        Me.btnRemove.TabIndex = 4
        Me.btnRemove.Text = "<"
        Me.btnRemove.UseVisualStyleBackColor = True
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(184, 132)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(45, 23)
        Me.btnAdd.TabIndex = 3
        Me.btnAdd.Text = ">"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'lstItems
        '
        Me.lstItems.FormattingEnabled = True
        Me.lstItems.Location = New System.Drawing.Point(9, 81)
        Me.lstItems.Name = "lstItems"
        Me.lstItems.Size = New System.Drawing.Size(169, 303)
        Me.lstItems.TabIndex = 1
        '
        'lstMenuItems
        '
        Me.lstMenuItems.FormattingEnabled = True
        Me.lstMenuItems.Location = New System.Drawing.Point(235, 81)
        Me.lstMenuItems.Name = "lstMenuItems"
        Me.lstMenuItems.Size = New System.Drawing.Size(99, 303)
        Me.lstMenuItems.TabIndex = 2
        '
        'CategoryTableAdapter
        '
        Me.CategoryTableAdapter.ClearBeforeFill = True
        '
        'btnName
        '
        Me.btnName.Appearance = System.Windows.Forms.Appearance.Button
        Me.btnName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnName.Location = New System.Drawing.Point(12, 412)
        Me.btnName.Name = "btnName"
        Me.btnName.Size = New System.Drawing.Size(104, 31)
        Me.btnName.TabIndex = 22
        Me.btnName.Text = "Name"
        Me.btnName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnName.UseVisualStyleBackColor = True
        '
        'btnPrice
        '
        Me.btnPrice.Appearance = System.Windows.Forms.Appearance.Button
        Me.btnPrice.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrice.Location = New System.Drawing.Point(122, 412)
        Me.btnPrice.Name = "btnPrice"
        Me.btnPrice.Size = New System.Drawing.Size(104, 31)
        Me.btnPrice.TabIndex = 23
        Me.btnPrice.Text = "Price"
        Me.btnPrice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnPrice.UseVisualStyleBackColor = True
        '
        'btnDesp
        '
        Me.btnDesp.Appearance = System.Windows.Forms.Appearance.Button
        Me.btnDesp.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDesp.Location = New System.Drawing.Point(232, 412)
        Me.btnDesp.Name = "btnDesp"
        Me.btnDesp.Size = New System.Drawing.Size(104, 31)
        Me.btnDesp.TabIndex = 24
        Me.btnDesp.Text = "Description"
        Me.btnDesp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnDesp.UseVisualStyleBackColor = True
        '
        'bgwLoadOne
        '
        '
        'bgwLoadTwo
        '
        '
        'frmSetup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(415, 455)
        Me.Controls.Add(Me.btnDesp)
        Me.Controls.Add(Me.btnPrice)
        Me.Controls.Add(Me.btnName)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmSetup"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "s"
        CType(Me.CategoryBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CategoriesFrmSetupDS, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cmbCategories As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents lstMenuItems As System.Windows.Forms.ListBox
    Friend WithEvents lstItems As System.Windows.Forms.ListBox
    Friend WithEvents btnRemove As System.Windows.Forms.Button
    Friend WithEvents CategoriesFrmSetupDS As Project.CategoriesFrmSetupDS
    Friend WithEvents CategoryBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents CategoryTableAdapter As Project.CategoriesFrmSetupDSTableAdapters.CategoryTableAdapter
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnMoveDown As System.Windows.Forms.Button
    Friend WithEvents btnMoveUp As System.Windows.Forms.Button
    Friend WithEvents btnName As System.Windows.Forms.RadioButton
    Friend WithEvents btnPrice As System.Windows.Forms.RadioButton
    Friend WithEvents btnDesp As System.Windows.Forms.RadioButton
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents ToolTip2 As System.Windows.Forms.ToolTip
    Friend WithEvents ToolTip3 As System.Windows.Forms.ToolTip
    Friend WithEvents bgwLoadOne As System.ComponentModel.BackgroundWorker
    Friend WithEvents bgwLoadTwo As System.ComponentModel.BackgroundWorker
End Class
