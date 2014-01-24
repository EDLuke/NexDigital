Public Class frmSpecials
    Dim menuItemsArrayList As ArrayList


    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        

    End Sub

    Private Sub lstvSpecials_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstvSpecials.SelectedIndexChanged

        menuItemsArrayList = DataLayer.GetMenuItems()
        Dim strSpecials As String
        strSpecials = menuItemsArrayList.ToString()

        For i = 1 To menuItemsArrayList.Count Step 2
            strSpecials += menuItemsArrayList(i) + ","
        Next
        lstvSpecials.Text = strSpecials.ToString()


    End Sub
End Class