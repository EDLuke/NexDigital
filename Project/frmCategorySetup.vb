Public Class frmCategorySetup

    
    Public Sub loadCategory()
        'TODO: This line of code loads data into the 'ItemsDSfrmAllItems.Items' table. You can move, or remove it, as needed.
        Me.CategoryTableAdapter.Connection.ConnectionString = DataLayer.conString
        Me.CategoryTableAdapter.Fill(Me.CategoriesDataSet.Category)

        Try
            If (lstAllCate.SelectedIndex = -1) Then
                lstAllCate.SelectedIndex = 0
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub OneTimeAdd()
        For i As Integer = 0 To 100
            Dim temp As String = i.ToString() + ""
            'Dim result = DataLayer.CreateCategory(temp)
        Next
    End Sub

    Private Sub frmCategorySetup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        loadCategory()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to delete " & lstAllCate.SelectedItem.Row.ItemArray(1) & "? All items in this category will be deleted as well", "Confirm", MessageBoxButtons.YesNo)

        If result = Windows.Forms.DialogResult.Yes Then
            DataLayer.DeleteCategory(CInt(lstAllCate.SelectedItem.Row.ItemArray(0)))
            MessageBox.Show("Category Deleted!", "Success")
        End If
        ' Fill new categories
        Me.CategoryTableAdapter.Fill(Me.CategoriesDataSet.Category)
        Digital_Board.mainFrm.tabOne.loadItems()
        Digital_Board.mainFrm.reloadData()
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If txtCategory.Text = "" Then
            MessageBox.Show("Please enter Category", "Enter Category")
            Return
        End If
        Dim CategoryName As String = txtCategory.Text

        ' Insert into database
        Dim result As Boolean = DataLayer.CreateCategory(CategoryName)
        If result = True Then
            MessageBox.Show("Category created!", "Success")
            txtCategory.Text = ""
        End If

        ' Fill new categories
        Me.CategoryTableAdapter.Fill(Me.CategoriesDataSet.Category)
        Digital_Board.mainFrm.reloadData()
    End Sub
End Class