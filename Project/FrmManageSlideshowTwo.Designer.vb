﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmManageSlideshowTwo
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnDown = New System.Windows.Forms.Button()
        Me.btnUp = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnRemove = New System.Windows.Forms.Button()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.lstSlideShowPics = New System.Windows.Forms.ListBox()
        Me.lstPics = New System.Windows.Forms.ListBox()
        Me.cmbCategories = New System.Windows.Forms.ComboBox()
        Me.CategoryBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.CategoriesDSfrmManageSlide = New Project.categoriesDSfrmManageSlide()
        Me.CategoryTableAdapter = New Project.categoriesDSfrmManageSlideTableAdapters.CategoryTableAdapter()
        Me.bgwOne = New System.ComponentModel.BackgroundWorker()
        Me.bgwTwo = New System.ComponentModel.BackgroundWorker()
        Me.bgwThree = New System.ComponentModel.BackgroundWorker()
        Me.GroupBox1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CategoryBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CategoriesDSfrmManageSlide, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnDown)
        Me.GroupBox1.Controls.Add(Me.btnUp)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.PictureBox1)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.btnRemove)
        Me.GroupBox1.Controls.Add(Me.btnAdd)
        Me.GroupBox1.Controls.Add(Me.lstSlideShowPics)
        Me.GroupBox1.Controls.Add(Me.lstPics)
        Me.GroupBox1.Controls.Add(Me.cmbCategories)
        Me.GroupBox1.Location = New System.Drawing.Point(5, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(400, 435)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Select Pictures"
        '
        'btnDown
        '
        Me.btnDown.Location = New System.Drawing.Point(358, 179)
        Me.btnDown.Name = "btnDown"
        Me.btnDown.Size = New System.Drawing.Size(32, 23)
        Me.btnDown.TabIndex = 10
        Me.btnDown.Text = "ᐯ"
        Me.btnDown.UseVisualStyleBackColor = True
        '
        'btnUp
        '
        Me.btnUp.Location = New System.Drawing.Point(358, 135)
        Me.btnUp.Name = "btnUp"
        Me.btnUp.Size = New System.Drawing.Size(32, 23)
        Me.btnUp.TabIndex = 9
        Me.btnUp.Text = "ᐱ"
        Me.btnUp.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(145, 16)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(247, 13)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Press DELETE to delete selected item or animation"
        '
        'PictureBox1
        '
        Me.PictureBox1.Location = New System.Drawing.Point(6, 312)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(386, 112)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 7
        Me.PictureBox1.TabStop = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 18)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(85, 13)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Select Category:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(192, 58)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(97, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Pics in Slide Show:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 58)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(81, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Select Pictures:"
        '
        'btnRemove
        '
        Me.btnRemove.Location = New System.Drawing.Point(152, 180)
        Me.btnRemove.Name = "btnRemove"
        Me.btnRemove.Size = New System.Drawing.Size(32, 23)
        Me.btnRemove.TabIndex = 4
        Me.btnRemove.Text = "<"
        Me.btnRemove.UseVisualStyleBackColor = True
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(152, 136)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(32, 23)
        Me.btnAdd.TabIndex = 3
        Me.btnAdd.Text = ">"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'lstSlideShowPics
        '
        Me.lstSlideShowPics.FormattingEnabled = True
        Me.lstSlideShowPics.Location = New System.Drawing.Point(192, 81)
        Me.lstSlideShowPics.Name = "lstSlideShowPics"
        Me.lstSlideShowPics.Size = New System.Drawing.Size(158, 225)
        Me.lstSlideShowPics.TabIndex = 2
        '
        'lstPics
        '
        Me.lstPics.FormattingEnabled = True
        Me.lstPics.Location = New System.Drawing.Point(9, 81)
        Me.lstPics.Name = "lstPics"
        Me.lstPics.Size = New System.Drawing.Size(139, 225)
        Me.lstPics.TabIndex = 1
        '
        'cmbCategories
        '
        Me.cmbCategories.DataSource = Me.CategoryBindingSource
        Me.cmbCategories.DisplayMember = "CategoryName"
        Me.cmbCategories.FormattingEnabled = True
        Me.cmbCategories.Location = New System.Drawing.Point(8, 34)
        Me.cmbCategories.Name = "cmbCategories"
        Me.cmbCategories.Size = New System.Drawing.Size(140, 21)
        Me.cmbCategories.TabIndex = 0
        Me.cmbCategories.ValueMember = "CategoryId"
        '
        'CategoryBindingSource
        '
        Me.CategoryBindingSource.DataMember = "Category"
        Me.CategoryBindingSource.DataSource = Me.CategoriesDSfrmManageSlide
        '
        'CategoriesDSfrmManageSlide
        '
        Me.CategoriesDSfrmManageSlide.DataSetName = "categoriesDSfrmManageSlide"
        Me.CategoriesDSfrmManageSlide.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'CategoryTableAdapter
        '
        Me.CategoryTableAdapter.ClearBeforeFill = True
        '
        'FrmManageSlideshow
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(416, 450)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "FrmManageSlideshow"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Manage Slideshow"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CategoryBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CategoriesDSfrmManageSlide, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnRemove As System.Windows.Forms.Button
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents lstSlideShowPics As System.Windows.Forms.ListBox
    Friend WithEvents lstPics As System.Windows.Forms.ListBox
    Friend WithEvents cmbCategories As System.Windows.Forms.ComboBox
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents CategoriesDSfrmManageSlide As Project.categoriesDSfrmManageSlide
    Friend WithEvents CategoryBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents CategoryTableAdapter As Project.categoriesDSfrmManageSlideTableAdapters.CategoryTableAdapter
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents btnDown As System.Windows.Forms.Button
    Friend WithEvents btnUp As System.Windows.Forms.Button
    Friend WithEvents bgwOne As System.ComponentModel.BackgroundWorker
    Friend WithEvents bgwTwo As System.ComponentModel.BackgroundWorker
    Friend WithEvents bgwThree As System.ComponentModel.BackgroundWorker
End Class
