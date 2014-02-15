Public Class frmAllItems

    Public selectedItemId As Integer

    Private searchText As String
    Private searched As Boolean = False
    Private searchItem As System.Data.DataRowView

    Private Sub frmAllItems_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        loadItems()
    End Sub

    Private Sub fmrAllItems_MouseWheel(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseWheel
        If (e.Delta > 0) Then
            If lstAllItems.SelectedIndex - 1 >= 0 Then
                lstAllItems.SelectedIndex -= 1
            End If
        Else
            If lstAllItems.SelectedIndex + 1 < lstAllItems.Items.Count Then
                lstAllItems.SelectedIndex += 1
            End If
        End If
    End Sub

    Private Sub lstAllItems_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstAllItems.SelectedIndexChanged
        If Not (lstAllItems.SelectedItem Is Nothing) Then
            selectedItemId = lstAllItems.SelectedItem.Row.ItemArray(0)
            If Digital_Board.mainFrm.tabTwo.Text = "Update Items" Then
                Digital_Board.mainFrm.tabTwo.setFormData(selectedItemId)
            End If
        End If

        Me.ActiveControl = txtSearch
    End Sub

    Private Sub lstAllItems_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lstAllItems.MouseDoubleClick
        Digital_Board.mainFrm.tabSec.SelectTab(1)
        Digital_Board.mainFrm.tabTwo.setFormData(Digital_Board.mainFrm.tabOne.selectedItemId)
    End Sub

    Public Sub loadItems()
        Me.Cursor = Cursors.WaitCursor
        If (Not bgwLoad.IsBusy) Then
            bgwLoad.RunWorkerAsync()
        End If

    End Sub

    Private Sub loadComplete(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwLoad.RunWorkerCompleted
        Me.ItemsTableAdapter.Connection.ConnectionString = DataLayer.conString
        Me.ItemsTableAdapter.Fill(Me.ItemsDSfrmAllItems.Items)

        Try
            If (lstAllItems.SelectedIndex = -1) Then
                lstAllItems.SelectedIndex = 0
            End If
            selectedItemId = lstAllItems.SelectedItem.Row.ItemArray(0)

        Catch ex As Exception

        End Try
        Me.Cursor = Cursors.Arrow
    End Sub

    Public Sub selectUp()
        If lstAllItems.SelectedIndex - 1 >= 0 Then
            lstAllItems.SelectedIndex = lstAllItems.SelectedIndex - 1
        Else
            lstAllItems.SelectedIndex = 0
        End If
    End Sub

    Public Sub selectDown()
        If lstAllItems.SelectedIndex + 1 < lstAllItems.Items.Count Then
            lstAllItems.SelectedIndex = lstAllItems.SelectedIndex + 1
        Else
            lstAllItems.SelectedIndex = lstAllItems.Items.Count - 1
        End If
    End Sub

    Private Sub bgwSearch_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwSearch.DoWork

    End Sub

    Private Sub bgwSearch_RunWorkComplete(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwSearch.RunWorkerCompleted
        Try
            For Each item In lstAllItems.Items
                If searchText.Length <= item.Row.ItemArray(1).Length Then
                    If String.Equals(searchText, item.Row.ItemArray(1).subString(0, txtSearch.Text.Length), StringComparison.InvariantCultureIgnoreCase) Then
                        searchItem = item
                        searched = True
                        lstAllItems.SelectedItem = searchItem
                        txtSearch.Select(txtSearch.Text.Length, 0)
                        Exit For
                    End If
                End If

            Next
        Catch ex As Exception

        End Try
    End Sub

    Private Sub bgwSearch_ReportProgress(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles bgwSearch.ProgressChanged
        
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        searchText = txtSearch.Text
        searched = False

        If Not bgwSearch.IsBusy And searchText <> "" Then
            bgwSearch.RunWorkerAsync()
        End If
    End Sub
End Class