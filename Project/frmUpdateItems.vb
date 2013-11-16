Imports System.IO
Imports System.Reflection

Public Class frmUpdateItem

    Dim currentItemId As Integer

    Private Sub frmUpdateItem_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Public Sub setFormData(ByVal itemid As Integer)
        If itemid <> 0 Or itemid <> Nothing Then
            currentItemId = itemid

            Dim result As ArrayList = DataLayer.GetItemDetails(currentItemId)

            txtName.Text = result.Item(0).ToString()
            txtDescription.Text = result.Item(1).ToString()
            txtPrice.Text = result.Item(2).ToString()
            txtPicture.Text = result.Item(3).ToString()

        End If
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Dispose()
    End Sub

    Private Sub btnButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnButton.Click

        ' read only image files
        OpenFileDialog1.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif"

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
        Dim picture As String
        If OpenFileDialog1.FileName = "OpenFileDialog1" Then
            picture = txtPicture.Text
        Else
            picture = OpenFileDialog1.SafeFileName
            ' Copy image file
            FileCopy(OpenFileDialog1.FileName, Directory.GetCurrentDirectory() & "\images\" & picture)
        End If

        Dim result As Boolean = DataLayer.UpdateItem(currentItemId, item, desc, price, picture)
        MessageBox.Show("Item has been updated!", "Success")
        MainMenu.tabOne.loadItems()

        ' Update the digital board at run time
        MainMenu.reloadData()
    End Sub
End Class