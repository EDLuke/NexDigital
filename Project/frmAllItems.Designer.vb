<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAllItems
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
        Me.lstAllItems = New System.Windows.Forms.ListBox()
        Me.ItemsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ItemsDSfrmAllItems = New Project.ItemsDSfrmAllItems()
        Me.ItemsTableAdapter = New Project.ItemsDSfrmAllItemsTableAdapters.ItemsTableAdapter()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.bgwSearch = New System.ComponentModel.BackgroundWorker()
        CType(Me.ItemsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ItemsDSfrmAllItems, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lstAllItems
        '
        Me.lstAllItems.DataSource = Me.ItemsBindingSource
        Me.lstAllItems.DisplayMember = "Name"
        Me.lstAllItems.FormattingEnabled = True
        Me.lstAllItems.Location = New System.Drawing.Point(12, 37)
        Me.lstAllItems.Name = "lstAllItems"
        Me.lstAllItems.Size = New System.Drawing.Size(389, 407)
        Me.lstAllItems.TabIndex = 0
        Me.lstAllItems.ValueMember = "ItemId"
        '
        'ItemsBindingSource
        '
        Me.ItemsBindingSource.DataMember = "Items"
        Me.ItemsBindingSource.DataSource = Me.ItemsDSfrmAllItems
        Me.ItemsBindingSource.Sort = "Name"
        '
        'ItemsDSfrmAllItems
        '
        Me.ItemsDSfrmAllItems.DataSetName = "ItemsDSfrmAllItems"
        Me.ItemsDSfrmAllItems.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'ItemsTableAdapter
        '
        Me.ItemsTableAdapter.ClearBeforeFill = True
        '
        'txtSearch
        '
        Me.txtSearch.Location = New System.Drawing.Point(13, 11)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(388, 20)
        Me.txtSearch.TabIndex = 1
        '
        'bgwSearch
        '
        Me.bgwSearch.WorkerReportsProgress = True
        Me.bgwSearch.WorkerSupportsCancellation = True
        '
        'frmAllItems
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(421, 462)
        Me.Controls.Add(Me.txtSearch)
        Me.Controls.Add(Me.lstAllItems)
        Me.Name = "frmAllItems"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "All Items"
        CType(Me.ItemsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ItemsDSfrmAllItems, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lstAllItems As System.Windows.Forms.ListBox
    Friend WithEvents ItemsDSfrmAllItems As Project.ItemsDSfrmAllItems
    Friend WithEvents ItemsBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents ItemsTableAdapter As Project.ItemsDSfrmAllItemsTableAdapters.ItemsTableAdapter
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents bgwSearch As System.ComponentModel.BackgroundWorker
End Class
