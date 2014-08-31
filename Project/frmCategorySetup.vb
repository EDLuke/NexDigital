Imports System.Threading

Public Class frmCategorySetup

    Private Sub frmCategorySetup_Shown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown
        loadCategory()
    End Sub

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

    Private Sub lstAllCate_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstAllCate.MouseEnter
        lstAllCate.Select()
    End Sub

    Private Sub lstAllCate_MouseWheel(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles lstAllCate.MouseWheel
        If (e.Delta > 0) Then
            If lstAllCate.SelectedIndex - 1 >= 0 Then
                lstAllCate.SelectedIndex -= 1
            End If
        Else
            If lstAllCate.SelectedIndex + 1 < lstAllCate.Items.Count Then
                lstAllCate.SelectedIndex += 1
            End If
        End If
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
        Me.Cursor = Cursors.WaitCursor
        Thread.Sleep(1000) 'Temporary solution
        Me.Cursor = Cursors.Default
        Digital_Board.reloadData()
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
        Digital_Board.reloadData()
    End Sub
End Class