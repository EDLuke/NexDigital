Imports System.IO
Imports System.Reflection
Imports System.Threading

Public Class FrmManageSlideshow
    Private picsArrayList As ArrayList
    Private SlideShowPicsArrayList As ArrayList
    Private cmbCategorySelected As Integer

    Private Sub FrmManageSlideshow_shown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown
        LoadCategory()
        FillCategories()
        UpdateListView()
    End Sub

    Public Sub LoadCategory()
        'TODO: This line of code loads data into the 'CategoriesDSfrmManageSlide.Category' table. You can move, or remove it, as needed.
        Me.CategoryTableAdapter.Connection.ConnectionString = DataLayer.conString
        Me.CategoryTableAdapter.Fill(Me.CategoriesDSfrmManageSlide.Category)
    End Sub

    'MouseEnter Handler for the three Controls
    Private Sub lstPics_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstPics.MouseEnter
        lstPics.Select()
    End Sub

    Private Sub lstMenuItems_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstSlideShowPics.MouseEnter
        lstSlideShowPics.Select()
    End Sub

    Private Sub cmbCategories_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCategories.MouseEnter
        cmbCategories.Select()
    End Sub

    Private Sub lstMenuItems_MouseWheel(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lstSlideShowPics.MouseWheel
        If (lstSlideShowPics.SelectedItem Is Nothing) Then
            lstSlideShowPics.SelectedItem = lstSlideShowPics.Items(0)
        Else
            If (e.Delta > 0) Then
                If lstSlideShowPics.SelectedIndex - 1 >= 0 Then
                    lstSlideShowPics.SelectedIndex -= 1
                End If
            Else
                If lstSlideShowPics.SelectedIndex + 1 < lstSlideShowPics.Items.Count Then
                    lstSlideShowPics.SelectedIndex += 1
                End If
            End If
        End If

    End Sub

    Private Sub lstPics_MouseWheel(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lstPics.MouseWheel
        If (e.Delta > 0) Then
            If lstPics.SelectedIndex - 1 >= 0 Then
                lstPics.SelectedIndex -= 1
            End If
        Else
            If lstPics.SelectedIndex + 1 < lstPics.Items.Count Then
                lstPics.SelectedIndex += 1
            End If
        End If
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

    Public Sub UpdateListView()
        lstSlideShowPics.Items.Clear()
        FillSlideShowPics()
    End Sub

    Private Sub cmbCategories_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCategories.SelectedIndexChanged
        FillCategories()
    End Sub

    Public Sub FillCategories()
        If cmbCategories.SelectedValue <> Nothing Then
            cmbCategorySelected = CInt(cmbCategories.SelectedValue.ToString())
            Me.Cursor = Cursors.WaitCursor
            If (Not bgwOne.IsBusy) Then
                bgwOne.RunWorkerAsync()
            End If
        End If
    End Sub

    Private Sub bgwOne_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwOne.DoWork
        picsArrayList = DataLayer.GetPicsOfCategoryItems(cmbCategorySelected)
    End Sub

    Private Sub loadCompleteOne(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwOne.RunWorkerCompleted
        lstPics.Items.Clear()
        For i = 2 To picsArrayList.Count Step 3
            lstPics.Items.Add(picsArrayList(i))
        Next
        Me.Cursor = Cursors.Arrow
    End Sub

    Private Sub lstItems_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstPics.DoubleClick
        If lstPics.SelectedItem = Nothing Then
            MessageBox.Show("Please select an item picture first", "Error")
            Return
        End If
        AddToSlideShow()

        'Update the slideshow at run time
        Digital_Board.mainFrm.tabTwo.loadSlideShowPic()
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        If lstPics.SelectedItem = Nothing Then
            MessageBox.Show("Please select an item picture first", "Error")
            Return
        End If
        AddToSlideShow()

        'Update the slideshow at run time
        Digital_Board.mainFrm.tabTwo.loadSlideShowPic()
    End Sub

    Private Sub AddToSlideShow()
        Dim item As String = lstPics.SelectedItem.ToString()
        Dim itemId As String = -1
        Dim itemPic As String = -1
        ' Get itemid of selected item
        For i = 2 To picsArrayList.Count Step 3
            If picsArrayList(i).ToString() = item Then
                itemId = picsArrayList(i - 2)
                itemPic = picsArrayList(i - 1)
                Exit For
            End If
        Next

        If checkRep(item) Then
            MessageBox.Show("Item already exists in slide show.", "Error")
            Return
        Else
            ' Update in database
            Dim response As Boolean = DataLayer.AddToSlideShow(CInt(itemId), 1)

            ' Add to menuItems arraylist
            SlideShowPicsArrayList.Add(itemId)
            SlideShowPicsArrayList.Add(itemPic)
            SlideShowPicsArrayList.Add(lstPics.SelectedItem)

            ' Update UI
            lstSlideShowPics.Items.Add(lstPics.SelectedItem)
        End If

        'Update the digital board at run time
        Digital_Board.digital.updateSlideShow()
    End Sub

    Private Function checkRep(input As String) As Boolean
        For i = 2 To SlideShowPicsArrayList.Count Step 3
            If input = SlideShowPicsArrayList(i) Then
                Return True
            End If
        Next
        Return False
    End Function

    Private Sub btnRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        If lstSlideShowPics.SelectedItem Is Nothing Then
            MessageBox.Show("Please select an item first", "Error")
            Return
        End If
        RemoveFromSlideShow()

        'Update the slideshow at run time
        Digital_Board.mainFrm.tabTwo.loadSlideShowPic()
    End Sub

    Private Sub RemoveFromSlideShow()
        Dim item As String = lstSlideShowPics.SelectedItem.ToString()
        Dim itemId As String = -1
        ' Get itemid of selected item
        For i = 2 To SlideShowPicsArrayList.Count Step 3

            If SlideShowPicsArrayList(i).ToString() = item Then
                itemId = SlideShowPicsArrayList(i - 2)
                ' Remove from menu items array list
                SlideShowPicsArrayList.RemoveAt(i)
                SlideShowPicsArrayList.RemoveAt(i - 2)
                SlideShowPicsArrayList.RemoveAt(i - 2)
                ' End the for loop
                Exit For
            End If
        Next
        ' Update in database
        Dim response As Boolean = DataLayer.RemoveFromSlideShow(CInt(itemId), 1)

        'Update ui
        lstSlideShowPics.Items.Remove(lstSlideShowPics.SelectedItem)

        'Update the digital board at run time
        Digital_Board.digital.updateSlideShow()
    End Sub

    Private Sub FillSlideShowPics()
        Me.Cursor = Cursors.WaitCursor
        If (Not bgwTwo.IsBusy) Then
            bgwTwo.RunWorkerAsync()
        End If

    End Sub

    Private Sub bgwTwo_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwTwo.DoWork
        SlideShowPicsArrayList = DataLayer.GetSlideShowItems(1)
    End Sub

    Private Sub loadCompleteTwo(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwTwo.RunWorkerCompleted
        For i = 2 To SlideShowPicsArrayList.Count Step 3
            lstSlideShowPics.Items.Add(SlideShowPicsArrayList(i))
        Next
        Me.Cursor = Cursors.Arrow
    End Sub

    Private Sub lstPics_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstPics.SelectedIndexChanged
        Try
            If lstPics.SelectedItem <> Nothing Then
                Dim myImage As System.Drawing.Image = Image.FromFile(Directory.GetCurrentDirectory() & "\images\" & picsArrayList(picsArrayList.IndexOf(lstPics.SelectedItem.ToString()) - 1))
                PictureBox1.Image = myImage
            End If
        Catch ex As FileNotFoundException
            PictureBox1.Image = Nothing
        End Try
    End Sub

    Private Sub lstSlideShowPics_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstSlideShowPics.SelectedIndexChanged
        Try
            If (lstSlideShowPics.SelectedIndex <> -1) Then
                Dim myImage As System.Drawing.Image = Image.FromFile(Directory.GetCurrentDirectory() & "\images\" & SlideShowPicsArrayList(SlideShowPicsArrayList.IndexOf(lstSlideShowPics.SelectedItem.ToString) - 1))
                PictureBox1.Image = myImage
            End If
        Catch ex As FileNotFoundException
            PictureBox1.Image = Nothing
        End Try
    End Sub

    Private Sub lstSlideShowPics_KeyDown(sender As System.Object, e As KeyEventArgs) Handles lstSlideShowPics.KeyDown
        If lstSlideShowPics.SelectedItem Is Nothing Then
            MessageBox.Show("Please Select an Item", "Error")
        Else
            If e.KeyCode = Keys.Delete Then
                RemoveFromSlideShow()
            End If
        End If
    End Sub

    Private Sub btnUp_Click(sender As Object, e As EventArgs) Handles btnUp.Click
        If lstSlideShowPics.SelectedItem Is Nothing Then
            MessageBox.Show("Please Select an Item", "Error")
        Else
            MoveUp()
            UpdateListView()
        End If
    End Sub

    Private Sub btnDown_Click(sender As Object, e As EventArgs) Handles btnDown.Click
        If lstSlideShowPics.SelectedItem Is Nothing Then
            MessageBox.Show("Please Select an Item", "Error")
        Else
            MoveDown()
            UpdateListView()
        End If
    End Sub

    Private Sub MoveUp()
        Dim item = lstSlideShowPics.SelectedItem.ToString

        If lstSlideShowPics.SelectedIndex > 0 Then
            DataLayer.SwitchSlideShowOrder(SlideShowPicsArrayList(SlideShowPicsArrayList.IndexOf(item) - 2), SlideShowPicsArrayList(SlideShowPicsArrayList.IndexOf(item) - 5), 1)
        End If
    End Sub

    Private Sub MoveDown()
        Dim item = lstSlideShowPics.SelectedItem.ToString

        If lstSlideShowPics.SelectedIndex <> lstSlideShowPics.Items.Count - 1 Then
            DataLayer.SwitchSlideShowOrder(SlideShowPicsArrayList(SlideShowPicsArrayList.IndexOf(item) - 2), SlideShowPicsArrayList(SlideShowPicsArrayList.IndexOf(item) + 1), 1)
        End If
    End Sub
End Class