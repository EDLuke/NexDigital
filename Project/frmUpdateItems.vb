Imports System.IO
Imports System.Reflection

Public Class frmUpdateItem

    Dim currentItemId As Integer

    Private Sub frmUpdateItem_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Me.CategoryTableAdapter.Connection.ConnectionString = DataLayer.conString
        Me.CategoryTableAdapter.Fill(Me.DatabaseDataSet.Category)
    End Sub

    Private Sub cmbCategories_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCategories.MouseEnter
        cmbCategories.Select()
    End Sub

    Private Sub cmbCategories_MouseWheel(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles cmbCategories.MouseWheel
        If (e.Delta > 0) Then
            If cmbCategories.SelectedIndex - 1 >= 0 Then
                cmbCategories.SelectedIndex -= 1
            End If
        Else
            If cmbCategories.SelectedIndex + 1 < cmbCategories.Items.Count Then
                cmbCategories.SelectedIndex += 1
            End If
        End If
    End Sub

    Public Sub setFormData(ByVal itemid As Integer)
        If itemid <> 0 Or itemid <> Nothing Then
            currentItemId = itemid

            Dim result As ArrayList = DataLayer.GetItemDetails(currentItemId)

            txtName.Text = result.Item(0).ToString()
            txtDescription.Text = result.Item(1).ToString()
            txtPrice.Text = result.Item(2).ToString()
            txtPicture.Text = result.Item(3).ToString()
            chkFull.Checked = CBool(result.Item(4).ToString())
            cmbCategories.SelectedValue = CInt(result.Item(5).ToString())
        End If
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Dispose()
    End Sub

    Private Sub btnButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnButton.Click

        ' read only image files
        OpenFileDialog1.Filter = "PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|AVI Files (*.avi)|*.avi"

        Dim result As DialogResult = OpenFileDialog1.ShowDialog()

        If result = Windows.Forms.DialogResult.OK Then
            txtPicture.Text = OpenFileDialog1.FileName
        End If
    End Sub

    Private Sub btnUpdateItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateItem.Click

        If txtName.Text = "" Or txtPrice.Text = "" Then
            MessageBox.Show("Please fill all fields", "Error")
            Return
        End If

        Dim item As String = txtName.Text
        Dim desc As String = txtDescription.Text
        Dim price As Double = CDbl(txtPrice.Text)
        Dim full As Boolean = chkFull.Checked
        Dim categoryid As Integer = cmbCategories.SelectedValue
        Dim picture As String
        If OpenFileDialog1.FileName = "OpenFileDialog1" Then
            picture = txtPicture.Text
        Else
            picture = OpenFileDialog1.SafeFileName
            ' Copy image file
            FileCopy(OpenFileDialog1.FileName, Directory.GetCurrentDirectory() & "\images\" & picture)
        End If

        Dim result As Boolean = DataLayer.UpdateItem(currentItemId, item, desc, price, picture, full, categoryid)
        MessageBox.Show("Item has been updated!", "Success")
        Digital_Board.mainFrm.tabOne.loadItems()

        ' Update the digital board at run time
        Digital_Board.reloadData()
    End Sub

    Private Sub btnRemovePic_Click(sender As Object, e As EventArgs) Handles btnRemovePic.Click
        If txtPicture.Text = "" Then
            MessageBox.Show("No picture was assigned.", "Error")
        Else
            txtPicture.Text = ""
            DataLayer.RemoveFromSlideShow(currentItemId, 1)
            DataLayer.RemoveFromSlideShow(currentItemId, 2)
        End If
    End Sub
End Class