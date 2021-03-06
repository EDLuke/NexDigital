﻿Imports System.IO.File
Imports System.IO
Imports System.Reflection


Public Class frmAddItem

    Private Sub frmAddNewItem_Shown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown
        LoadItems()
    End Sub

    Public Sub LoadItems()
        Me.Cursor = Cursors.WaitCursor
        If (Not bgw.IsBusy) Then
            bgw.RunWorkerAsync()
        End If
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

    Private Sub loadComplete(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgw.RunWorkerCompleted
        Me.CategoryTableAdapter.Connection.ConnectionString = DataLayer.conString
        Me.CategoryTableAdapter.Fill(Me.DatabaseDataSet.Category)
        Me.Cursor = Cursors.Arrow
    End Sub

    Private Sub btnButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnButton.Click
        ' read only image files
        OpenFileDialog1.Filter = "PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|AVI Files (*.avi)|*.avi"

        Dim result As DialogResult = OpenFileDialog1.ShowDialog()

        If result = Windows.Forms.DialogResult.OK Then
            txtPicture.Text = OpenFileDialog1.FileName
        End If
    End Sub

    Private Sub btnAddItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddItem.Click

        If txtName.Text = "" Then
            MessageBox.Show("Please fill all fields", "Error")
            Return
        End If

        Dim categoryId As Integer = CInt(cmbCategories.SelectedValue.ToString())

        Dim item As String = txtName.Text
        Dim desc As String = txtDescription.Text
        Dim price As Double
        If (txtPrice.Text = "") Then
            price = 0
        Else
            price = CDbl(txtPrice.Text)
        End If
        Dim picture As String
        Try
            picture = OpenFileDialog1.SafeFileName

            If picture <> "" Then
                If (Not System.IO.Directory.Exists(Directory.GetCurrentDirectory() & "\images")) Then
                    System.IO.Directory.CreateDirectory(Directory.GetCurrentDirectory() & "\images")
                End If

                ' Copy image file
                FileCopy(OpenFileDialog1.FileName, Directory.GetCurrentDirectory() & "\images\" & picture)
            End If


        Catch ex As FileNotFoundException
            picture = ""
        End Try


        Dim strPrice = price.ToString("C")
        ' Insert into database

        DataLayer.InsertItem(categoryId, item, desc, price, picture)


        MessageBox.Show("Item has been added", "Success")
        Digital_Board.mainFrm.tabOne.loadItems()
        ClearTextBoxes()
    End Sub

    Private Sub ClearTextBoxes()
        txtName.Text = ""
        txtDescription.Text = ""
        txtPicture.Text = ""
        txtPrice.Text = ""
    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Dispose()
    End Sub
End Class