Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.Reflection
Imports Microsoft.DirectX.AudioVideoPlayback

Public Class FrmManageSlideshowTwo
    Private picsArrayList As ArrayList
    Private SlideShowPicsArrayList As ArrayList
    Private AnimationTwoList As New ArrayList
    Private AnimationList As ArrayList
    Private cmbCategorySelected As Integer
    Private video As Video

    Private Sub FrmManageSlideshow_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadCategory()
        FillCategories()
        UpdateTreeView()
    End Sub

    Public Sub LoadCategory()
        'TODO: This line of code loads data into the 'CategoriesDSfrmManageSlide.Category' table. You can move, or remove it, as needed.
        Me.CategoryTableAdapter.Connection.ConnectionString = DataLayer.conString
        Me.CategoryTableAdapter.Fill(Me.CategoriesDSfrmManageSlide.Category)
    End Sub

    Public Sub UpdateTreeView()
        lstSlideShowPics.Nodes.Clear()
        AnimationTwoList.Clear()

        FillSlideShowPics()
        LoadAnimationList()
    End Sub

    Private Sub LoadAnimationList()
        Me.Cursor = Cursors.WaitCursor
        If (Not bgwThree.IsBusy) Then
            bgwThree.RunWorkerAsync()
        End If


    End Sub

    Private Sub bgwThree_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwThree.DoWork
        AnimationList = BinaryDeserialize()
    End Sub

    Private Sub loadCompleteThree(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwThree.RunWorkerCompleted
        lstSlideShowPics.BeginUpdate()
        For Each item As TreeNode In lstSlideShowPics.Nodes
            If item.Parent Is Nothing Then
                Dim animationSearch = AnimationList.IndexOf(item.Text)
                Dim animationOneSearch = AnimationTwoList.IndexOf(item.Text)

                If animationSearch <> -1 Then
                    If (item.Nodes.Count = 0) Then
                        item.Nodes.Add(AnimationList(animationSearch + 1).ToString)
                        AnimationTwoList.Insert(animationOneSearch + 1, AnimationList(animationSearch + 1).ToString)
                    End If
                End If
            End If
        Next
        lstSlideShowPics.EndUpdate()
        Me.Cursor = Cursors.Arrow
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
        MainMenu.tabTwo.loadSlideShowPic()
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        If lstPics.SelectedItem = Nothing Then
            MessageBox.Show("Please select an item picture first", "Error")
            Return
        End If
        AddToSlideShow()

        'Update the slideshow at run time
        MainMenu.tabTwo.loadSlideShowPic()
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
            Dim response As Boolean = DataLayer.AddToSlideShow(CInt(itemId), 2)

            ' Add to menuItems arraylist
            SlideShowPicsArrayList.Add(itemId)
            SlideShowPicsArrayList.Add(itemPic)
            SlideShowPicsArrayList.Add(lstPics.SelectedItem)

            ' Update UI
            lstSlideShowPics.Nodes.Add(lstPics.SelectedItem)
        End If

        'Restore item's animation (if any)
        LoadAnimationList()

        'Update the digital board at run time
        MainMenu.digital.updateSlideShow()
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
        If lstSlideShowPics.SelectedNode Is Nothing Then
            MessageBox.Show("Please select an item first", "Error")
            Return
        End If
        RemoveFromSlideShow()

        'Update the slideshow at run time
        MainMenu.tabTwo.loadSlideShowPic()
    End Sub

    Private Sub RemoveFromSlideShow()
        Dim item As String = lstSlideShowPics.SelectedNode.ToString().Substring(10)
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
        Dim response As Boolean = DataLayer.RemoveFromSlideShow(CInt(itemId), 2)

        'Update ui
        lstSlideShowPics.Nodes.Remove(lstSlideShowPics.SelectedNode)

        'Update the digital board at run time
        MainMenu.digital.updateSlideShow()
    End Sub

    Private Sub FillSlideShowPics()
        Me.Cursor = Cursors.WaitCursor
        If (Not bgwTwo.IsBusy) Then
            bgwTwo.RunWorkerAsync()
        End If

    End Sub

    Private Sub bgwTwo_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwTwo.DoWork
        SlideShowPicsArrayList = DataLayer.GetSlideShowItems(2)
    End Sub

    Private Sub loadCompleteTwo(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwTwo.RunWorkerCompleted
        For i = 2 To SlideShowPicsArrayList.Count Step 3
            lstSlideShowPics.Nodes.Add(SlideShowPicsArrayList(i))
            AnimationTwoList.Add(SlideShowPicsArrayList(i))
        Next
        Me.Cursor = Cursors.Arrow
    End Sub

    Private Sub lstPics_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstPics.SelectedIndexChanged
        Try
            If lstPics.SelectedItem <> Nothing Then
                If picsArrayList(picsArrayList.IndexOf(lstPics.SelectedItem.ToString()) - 1).ToString.Contains(".avi") Then
                    video = New Video(Directory.GetCurrentDirectory() & "\images\" & picsArrayList(picsArrayList.IndexOf(lstPics.SelectedItem.ToString()) - 1))
                    video.Owner = PictureBox1
                    video.Size = New Size(386, 112)
                    video.Play()
                    AddHandler video.Ending, AddressOf BackLoopHandler
                Else
                    If (video IsNot Nothing) Then
                        video.Dispose()
                    End If
                    Dim myImage As System.Drawing.Image = Image.FromFile(Directory.GetCurrentDirectory() & "\images\" & picsArrayList(picsArrayList.IndexOf(lstPics.SelectedItem.ToString()) - 1))
                    PictureBox1.Image = myImage
                End If

            End If
        Catch ex As FileNotFoundException
            PictureBox1.Image = Nothing
        End Try
    End Sub

    Private Sub lstSlideShowPics_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstSlideShowPics.AfterSelect
        Try
            If lstSlideShowPics.SelectedNode.Parent Is Nothing Then
                If SlideShowPicsArrayList(SlideShowPicsArrayList.IndexOf(lstSlideShowPics.SelectedNode.Text) - 1).ToString.Contains(".avi") Then
                    PictureBox1.Image = Nothing
                    video = New Video(Directory.GetCurrentDirectory() & "\images\" & SlideShowPicsArrayList(SlideShowPicsArrayList.IndexOf(lstSlideShowPics.SelectedNode.Text) - 1))
                    video.Owner = PictureBox1
                    video.Size = New Size(386, 112)
                    video.Play()
                    AddHandler video.Ending, AddressOf BackLoopHandler
                Else
                    If (video IsNot Nothing) Then
                        video.Dispose()
                    End If
                    Dim myImage As System.Drawing.Image = Image.FromFile(Directory.GetCurrentDirectory() & "\images\" & SlideShowPicsArrayList(SlideShowPicsArrayList.IndexOf(lstSlideShowPics.SelectedNode.Text) - 1))

                    PictureBox1.Size = New Size(386, 112)
                    PictureBox1.Image = myImage
                End If
            Else
                MainMenu.vwOne.changeAnimaSelected(lstSlideShowPics.SelectedNode.Text)
            End If
        Catch ex As FileNotFoundException
            PictureBox1.Image = Nothing
        End Try
    End Sub

    Sub BackLoopHandler(sender As Object, args As EventArgs)
        If (Not video.Disposed) Then
            video.Stop()
            'video.Dispose()
        End If
    End Sub

    Public Sub AddAnimation(ByVal anima As AnimationTypes)
        If lstSlideShowPics.SelectedNode Is Nothing Then
            MessageBox.Show("Please Select an Item", "Error")
        ElseIf lstSlideShowPics.SelectedNode.Text.ToString.Contains(".avi") Then
            MessageBox.Show("Cannot add animation to video", "Error")
        Else
            If (lstSlideShowPics.SelectedNode.Nodes.Count = 0) Then
                lstSlideShowPics.SelectedNode.Nodes.Add(anima.ToString())
            Else
                MessageBox.Show(lstSlideShowPics.SelectedNode.Text & " already has animation: " & lstSlideShowPics.SelectedNode.Nodes(0).ToString.Substring(10), "Error")
                Return
            End If

            AnimationList.Add(lstSlideShowPics.SelectedNode.Text)
            AnimationList.Add(anima.ToString)

            BinarySerialize(AnimationList)
        End If
    End Sub

    Private Sub lstSlideShowPics_KeyDown(sender As System.Object, e As KeyEventArgs) Handles lstSlideShowPics.KeyDown
        If lstSlideShowPics.SelectedNode Is Nothing Then
            MessageBox.Show("Please Select an Item", "Error")
        Else
            If e.KeyCode = Keys.Delete Then
                If lstSlideShowPics.SelectedNode.Parent Is Nothing Then
                    RemoveFromSlideShow()
                Else
                    RemoveAnimation()
                End If
            End If
        End If
    End Sub

    Private Sub RemoveAnimation()
        Dim item = lstSlideShowPics.SelectedNode.Parent.Text
        Dim itemIndex = AnimationList.IndexOf(item)

        'Update the arraylist and the binary file
        AnimationList.RemoveAt(itemIndex)
        AnimationList.RemoveAt(itemIndex)
        BinarySerialize(AnimationList)

        'Update UI
        lstSlideShowPics.SelectedNode.Parent.Nodes.Clear()
    End Sub

    Private Shared Sub BinarySerialize(list As ArrayList)
        Using str As FileStream = File.Create("AnimationListTwo.bin")
            Dim bf As New BinaryFormatter()
            bf.Serialize(str, list)
        End Using
    End Sub

    ' Deserialize an ArrayList object from a binary file.
    Private Shared Function BinaryDeserialize() As ArrayList
        Dim people As New ArrayList

        Try
            Using str As FileStream = File.OpenRead("AnimationListTwo.bin")
                Dim bf As New BinaryFormatter()
                people = DirectCast(bf.Deserialize(str), ArrayList)
            End Using
        Catch ex As FileNotFoundException

        End Try

        Return people
    End Function

    Private Sub btnUp_Click(sender As Object, e As EventArgs) Handles btnUp.Click
        If lstSlideShowPics.SelectedNode Is Nothing Or isAnimation(lstSlideShowPics.SelectedNode.ToString) Then
            MessageBox.Show("Please Select an Item", "Error")
        Else
            If lstSlideShowPics.SelectedNode.Parent Is Nothing Then
                MoveUp()
                UpdateTreeView()
            End If
        End If
    End Sub

    Private Sub btnDown_Click(sender As Object, e As EventArgs) Handles btnDown.Click
        If lstSlideShowPics.SelectedNode Is Nothing Or isAnimation(lstSlideShowPics.SelectedNode.ToString) Then
            MessageBox.Show("Please Select an Item", "Error")
        Else
            If lstSlideShowPics.SelectedNode.Parent Is Nothing Then
                MoveDown()
                UpdateTreeView()
            End If
        End If
    End Sub

    Private Function isAnimation(ByVal node As String) As Boolean
        For Each Str As String In [Enum].GetNames(GetType(AnimationTypes))
            If node = Str Then
                Return True
            End If
        Next
        Return False
    End Function

    Private Sub MoveUp()
        Dim item = lstSlideShowPics.SelectedNode.Text
        Dim itemIndex = AnimationTwoList.IndexOf(item)
        Dim animationTypes = [Enum].GetNames(GetType(AnimationTypes))

        If itemIndex > 0 Then

            Dim isEnum As Boolean = False
            For Each Str As String In animationTypes
                If AnimationTwoList(itemIndex - 1) = Str Then
                    isEnum = True
                End If
            Next

            If isEnum Then
                DataLayer.SwitchSlideShowOrder(SlideShowPicsArrayList(SlideShowPicsArrayList.IndexOf(item) - 2), SlideShowPicsArrayList(SlideShowPicsArrayList.IndexOf(AnimationTwoList(itemIndex - 2)) - 2), 2)
            Else
                DataLayer.SwitchSlideShowOrder(SlideShowPicsArrayList(SlideShowPicsArrayList.IndexOf(item) - 2), SlideShowPicsArrayList(SlideShowPicsArrayList.IndexOf(AnimationTwoList(itemIndex - 1)) - 2), 2)
            End If
        End If
    End Sub

    Private Sub MoveDown()
        Dim item = lstSlideShowPics.SelectedNode.Text
        Dim itemIndex = AnimationTwoList.IndexOf(item)
        Dim animationTypes = [Enum].GetNames(GetType(AnimationTypes))

        If itemIndex <> AnimationTwoList.Count - 1 Then

            Dim isEnum As Boolean = False
            For Each Str As String In animationTypes
                If AnimationTwoList(itemIndex + 1) = Str Then
                    isEnum = True
                End If
            Next

            If isEnum Then
                DataLayer.SwitchSlideShowOrder(SlideShowPicsArrayList(SlideShowPicsArrayList.IndexOf(item) - 2), SlideShowPicsArrayList(SlideShowPicsArrayList.IndexOf(AnimationTwoList(itemIndex + 2)) - 2), 2)
            Else
                DataLayer.SwitchSlideShowOrder(SlideShowPicsArrayList(SlideShowPicsArrayList.IndexOf(item) - 2), SlideShowPicsArrayList(SlideShowPicsArrayList.IndexOf(AnimationTwoList(itemIndex + 1)) - 2), 2)
            End If
        End If
    End Sub
End Class