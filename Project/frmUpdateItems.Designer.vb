<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmUpdateItem
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
        Me.btnUpdateItem = New System.Windows.Forms.Button()
        Me.btnButton = New System.Windows.Forms.Button()
        Me.txtPicture = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtPrice = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtDescription = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.chkFull = New System.Windows.Forms.CheckBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cmbCategories = New System.Windows.Forms.ComboBox()
        Me.DatabaseDataSet = New Project.DatabaseDataSet()
        Me.CategoryBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.CategoryTableAdapter = New Project.DatabaseDataSetTableAdapters.CategoryTableAdapter()
        Me.btnRemovePic = New System.Windows.Forms.Button()
        CType(Me.DatabaseDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CategoryBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnUpdateItem
        '
        Me.btnUpdateItem.Location = New System.Drawing.Point(12, 254)
        Me.btnUpdateItem.Name = "btnUpdateItem"
        Me.btnUpdateItem.Size = New System.Drawing.Size(75, 23)
        Me.btnUpdateItem.TabIndex = 25
        Me.btnUpdateItem.Text = "Update Item"
        Me.btnUpdateItem.UseVisualStyleBackColor = True
        '
        'btnButton
        '
        Me.btnButton.Location = New System.Drawing.Point(306, 194)
        Me.btnButton.Name = "btnButton"
        Me.btnButton.Size = New System.Drawing.Size(75, 23)
        Me.btnButton.TabIndex = 24
        Me.btnButton.Text = "Browse"
        Me.btnButton.UseVisualStyleBackColor = True
        '
        'txtPicture
        '
        Me.txtPicture.Location = New System.Drawing.Point(61, 196)
        Me.txtPicture.Name = "txtPicture"
        Me.txtPicture.ReadOnly = True
        Me.txtPicture.Size = New System.Drawing.Size(239, 20)
        Me.txtPicture.TabIndex = 21
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 199)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(43, 13)
        Me.Label4.TabIndex = 20
        Me.Label4.Text = "Picture:"
        '
        'txtPrice
        '
        Me.txtPrice.Location = New System.Drawing.Point(104, 158)
        Me.txtPrice.Name = "txtPrice"
        Me.txtPrice.Size = New System.Drawing.Size(142, 20)
        Me.txtPrice.TabIndex = 19
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 161)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(34, 13)
        Me.Label3.TabIndex = 18
        Me.Label3.Text = "Price:"
        '
        'txtDescription
        '
        Me.txtDescription.Location = New System.Drawing.Point(104, 59)
        Me.txtDescription.Multiline = True
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.Size = New System.Drawing.Size(263, 89)
        Me.txtDescription.TabIndex = 17
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 62)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(86, 13)
        Me.Label2.TabIndex = 16
        Me.Label2.Text = "Item Description:"
        '
        'txtName
        '
        Me.txtName.Location = New System.Drawing.Point(104, 6)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(142, 20)
        Me.txtName.TabIndex = 15
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(61, 13)
        Me.Label1.TabIndex = 14
        Me.Label1.Text = "Item Name:"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'chkFull
        '
        Me.chkFull.AutoSize = True
        Me.chkFull.Location = New System.Drawing.Point(83, 232)
        Me.chkFull.Name = "chkFull"
        Me.chkFull.Size = New System.Drawing.Size(15, 14)
        Me.chkFull.TabIndex = 31
        Me.chkFull.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(12, 232)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(65, 13)
        Me.Label5.TabIndex = 30
        Me.Label5.Text = "Full Picture: "
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(12, 35)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(72, 13)
        Me.Label6.TabIndex = 16
        Me.Label6.Text = "ItemCategory:"
        '
        'cmbCategories
        '
        Me.cmbCategories.DataSource = Me.CategoryBindingSource
        Me.cmbCategories.DisplayMember = "CategoryName"
        Me.cmbCategories.FormattingEnabled = True
        Me.cmbCategories.Location = New System.Drawing.Point(104, 32)
        Me.cmbCategories.Name = "cmbCategories"
        Me.cmbCategories.Size = New System.Drawing.Size(142, 21)
        Me.cmbCategories.TabIndex = 32
        Me.cmbCategories.ValueMember = "CategoryId"
        '
        'DatabaseDataSet
        '
        Me.DatabaseDataSet.DataSetName = "DatabaseDataSet"
        Me.DatabaseDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'CategoryBindingSource
        '
        Me.CategoryBindingSource.DataMember = "Category"
        Me.CategoryBindingSource.DataSource = Me.DatabaseDataSet
        '
        'CategoryTableAdapter
        '
        Me.CategoryTableAdapter.ClearBeforeFill = True
        '
        'btnRemovePic
        '
        Me.btnRemovePic.Location = New System.Drawing.Point(277, 222)
        Me.btnRemovePic.Name = "btnRemovePic"
        Me.btnRemovePic.Size = New System.Drawing.Size(104, 23)
        Me.btnRemovePic.TabIndex = 33
        Me.btnRemovePic.Text = "Remove Picture"
        Me.btnRemovePic.UseVisualStyleBackColor = True
        '
        'frmUpdateItem
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(436, 286)
        Me.Controls.Add(Me.btnRemovePic)
        Me.Controls.Add(Me.cmbCategories)
        Me.Controls.Add(Me.chkFull)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.btnUpdateItem)
        Me.Controls.Add(Me.btnButton)
        Me.Controls.Add(Me.txtPicture)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtPrice)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtDescription)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtName)
        Me.Controls.Add(Me.Label1)
        Me.Name = "frmUpdateItem"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Update Items"
        CType(Me.DatabaseDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CategoryBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnUpdateItem As System.Windows.Forms.Button
    Friend WithEvents btnButton As System.Windows.Forms.Button
    Friend WithEvents txtPicture As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtPrice As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtDescription As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents chkFull As System.Windows.Forms.CheckBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cmbCategories As System.Windows.Forms.ComboBox
    Friend WithEvents CategoryBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents DatabaseDataSet As Project.DatabaseDataSet
    Friend WithEvents CategoryTableAdapter As Project.DatabaseDataSetTableAdapters.CategoryTableAdapter
    Friend WithEvents btnRemovePic As System.Windows.Forms.Button
End Class
