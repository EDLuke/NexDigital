Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary

Public Class frmSetup

    Dim itemsArrayList As ArrayList

    Dim cmbCategorySelected As Integer

    Public selectedItemId As Integer

    Public btnArray(2) As Boolean

    Public menuItemsArrayList As New ArrayList

    'The background color of the panel
    Public despBgColor As Color = Color.Tan

    'The background color of the Highlighted Item Background
    Public itemPanelColor As Color = Color.FromArgb(153, 204, 255)

    'The background color of the Highlighted Item Border
    Public itemBorderColor As Color = Color.FromArgb(51, 51, 255)

    'The color of Weather Text
    Public weatherColor As Color = Color.Azure

    'The color of News Text
    Public newsColor As Color = Color.Black

    'Stores the fonts for name, price and description
    Public despFontArray(2) As Font

    'Stores the colors for name, price and description
    Public despColorArray(2) As Color

    'Stores the frequency for the slide shows
    Public slideShowFreq As Integer

    Private tempSelected As Integer

    Private Sub frmSetup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadCategory()
        loadToolTip()
        loadDefaults()
        FillCategories()
        FillMenuItems()
        updateUI()

        If lstMenuItems.Items.Count <> 0 Then
            lstMenuItems.SelectedIndex = 0
        End If
    End Sub

    Public Sub LoadCategory()
        'TODO: This line of code loads data into the 'CategoriesFrmSetupDS.Category' table. You can move, or remove it, as needed.
        Me.CategoryTableAdapter.Connection.ConnectionString = DataLayer.conString
        Me.CategoryTableAdapter.Fill(Me.CategoriesFrmSetupDS.Category)
        If (cmbCategories.SelectedValue IsNot Nothing) Then
            cmbCategories.SelectedIndex = -1
        End If
        clearUI()
    End Sub

    Private Sub loadToolTip()
        ToolTip1.SetToolTip(btnName, "The same font and color will apply to all items' Name")
        ToolTip2.SetToolTip(btnPrice, "The same font and color will apply to all items' Price")
        ToolTip3.SetToolTip(btnDesp, "The same font and color will apply to all items' Description")
    End Sub

    Public Sub loadDefaults()
        If despFontArray(0) Is Nothing And despFontArray(1) Is Nothing And despFontArray(2) Is Nothing Then
            despFontArray(0) = New Font("Modern No. 20", 20, FontStyle.Regular)
            despFontArray(1) = New Font("Papyrus", 13, FontStyle.Regular)
            despFontArray(2) = New Font("SuperFrench", 13, FontStyle.Regular)
        End If

        slideShowFreq = 1000
    End Sub

    Private Sub cmbCategories_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCategories.SelectedIndexChanged
        FillCategories()
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        If checkSelect(lstItems) Then
            Return
        End If
        AddToMenu()
        updateUI()

        If lstMenuItems.Items.Count <> 0 Then
            lstMenuItems.SelectedIndex = 0
        End If
    End Sub

    Private Sub btnRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        If checkSelect(lstMenuItems) Then
            Return
        End If
        RemoveFromMenu()
        updateUI()

        If lstMenuItems.Items.Count <> 0 Then
            lstMenuItems.SelectedIndex = 0
        End If
    End Sub

    Private Sub lstItems_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lstItems.MouseDoubleClick
        If checkSelect(lstItems) Then
            Return
        End If
        AddToMenu()
        updateUI()

        If lstMenuItems.Items.Count <> 0 Then
            lstMenuItems.SelectedIndex = 0
        End If
    End Sub

    Private Sub btnMoveUp_Click(sender As Object, e As EventArgs) Handles btnMoveUp.Click
        tempSelected = lstMenuItems.SelectedIndex

        If checkSelect(lstMenuItems) Then
            Return
        End If
        If lstMenuItems.SelectedIndex <> 0 Then
            bgwUp.RunWorkerAsync()
            'MoveUpMenu(1)
        End If
        updateUI()

        If tempSelected - 1 >= 0 Then
            lstMenuItems.SelectedIndex = tempSelected - 1
        Else
            lstMenuItems.SelectedIndex = 0
        End If
    End Sub

    Private Sub bgwUp_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwUp.DoWork
        MoveUpMenu(1)
    End Sub

    Private Sub bgwUp_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwUp.RunWorkerCompleted
        updateUI()

        ' Update the digital board at run time
        MainMenu.digital.updateDespPanel()

        If tempSelected - 1 >= 0 Then
            lstMenuItems.SelectedIndex = tempSelected - 1
        Else
            lstMenuItems.SelectedIndex = 0
        End If
    End Sub

    Private Sub MoveUpMenu(ByVal menu As Integer)
        'Store the item information to temp
        Dim tempItemId As String = menuItemsArrayList(tempSelected * 4)
        Dim tempItem As String = menuItemsArrayList(tempSelected * 4 + 1)
        Dim tempItemPrice As String = menuItemsArrayList(tempSelected * 4 + 2)
        Dim tempItemDesp As String = menuItemsArrayList(tempSelected * 4 + 3)

        'Update in the datebase, switch the position of the item with the item before it
        DataLayer.SwitchMenuOrder(CInt(tempItemId), CInt(menuItemsArrayList((tempSelected - 1) * 4)), menu)

        'Remove them in the ArrayList
        menuItemsArrayList.RemoveAt(tempSelected * 4)
        menuItemsArrayList.RemoveAt(tempSelected * 4)
        menuItemsArrayList.RemoveAt(tempSelected * 4)
        menuItemsArrayList.RemoveAt(tempSelected * 4)

        'Then insert them in the place in front of them
        menuItemsArrayList.Insert((tempSelected - 1) * 4, tempItemId)
        menuItemsArrayList.Insert((tempSelected - 1) * 4 + 1, tempItem)
        menuItemsArrayList.Insert((tempSelected - 1) * 4 + 2, tempItemPrice)
        menuItemsArrayList.Insert((tempSelected - 1) * 4 + 3, tempItemDesp)


    End Sub

    Private Sub btnMoveDown_Click(sender As Object, e As EventArgs) Handles btnMoveDown.Click
        tempSelected = lstMenuItems.SelectedIndex

        If checkSelect(lstMenuItems) Then
            Return
        End If
        If lstMenuItems.SelectedIndex <> menuItemsArrayList.Count / 4 - 1 Then
            MoveDownMenu(1)
        End If
        updateUI()

        If tempSelected + 1 < lstMenuItems.Items.Count Then
            lstMenuItems.SelectedIndex = tempSelected + 1
        Else
            lstMenuItems.SelectedIndex = lstMenuItems.Items.Count - 1
        End If
    End Sub

    Private Sub MoveDownMenu(ByVal menu As Integer)
        'Store the item information to temp
        Dim tempItemId As String = menuItemsArrayList(lstMenuItems.SelectedIndex * 4)
        Dim tempItem As String = menuItemsArrayList(lstMenuItems.SelectedIndex * 4 + 1)
        Dim tempItemPrice As String = menuItemsArrayList(lstMenuItems.SelectedIndex * 4 + 2)
        Dim tempItemDesp As String = menuItemsArrayList(lstMenuItems.SelectedIndex * 4 + 3)

        'Update in the datebase, switch the position of the item with the item after it
        DataLayer.SwitchMenuOrder(CInt(tempItemId), CInt(menuItemsArrayList((lstMenuItems.SelectedIndex + 1) * 4)), menu)

        'Remove them in the ArrayList
        menuItemsArrayList.RemoveAt(lstMenuItems.SelectedIndex * 4)
        menuItemsArrayList.RemoveAt(lstMenuItems.SelectedIndex * 4)
        menuItemsArrayList.RemoveAt(lstMenuItems.SelectedIndex * 4)
        menuItemsArrayList.RemoveAt(lstMenuItems.SelectedIndex * 4)

        'Then insert them in the place in front of them
        menuItemsArrayList.Insert((lstMenuItems.SelectedIndex + 1) * 4, tempItemId)
        menuItemsArrayList.Insert((lstMenuItems.SelectedIndex + 1) * 4 + 1, tempItem)
        menuItemsArrayList.Insert((lstMenuItems.SelectedIndex + 1) * 4 + 2, tempItemPrice)
        menuItemsArrayList.Insert((lstMenuItems.SelectedIndex + 1) * 4 + 3, tempItemDesp)

        ' Update the digital board at run time
        MainMenu.digital.updateDespPanel()
    End Sub

    Private Function checkSelect(ByVal e As ListBox) As Boolean
        If e.SelectedItem = Nothing Then
            MessageBox.Show("Please select an item first", "Error")
            Return True
        End If
        Return False
    End Function

    Private Sub AddToMenu()
        Dim item As String = lstItems.SelectedItem.ToString()
        Dim itemId As String = -1
        Dim itemPrice As String = -1
        Dim itemDesp As String = -1

        ' Get itemid and itemPrice of selected item
        For i = 1 To itemsArrayList.Count Step 4
            If itemsArrayList(i).ToString() = item Then
                itemId = itemsArrayList(i - 1)
                itemPrice = itemsArrayList(i + 1)
                itemDesp = itemsArrayList(i + 2)
                ' End the for loop
                Exit For
            End If
        Next

        If checkRep(item) Then
            'Do not add to menuItems arrayList if the item already exists
            MessageBox.Show("The item is already in the menu.", "Error")
        Else
            ' Update in database
            Dim response As Boolean = DataLayer.AddToMenu(CInt(itemId), 1)

            ' Add to menuItems arraylist
            menuItemsArrayList.Add(itemId)
            menuItemsArrayList.Add(item)
            menuItemsArrayList.Add(itemPrice)
            menuItemsArrayList.Add(itemDesp)
        End If

        ' Update the digital board at run time
        MainMenu.digital.updateDespPanel()
    End Sub

    Private Sub RemoveFromMenu()
        Dim item As String = lstMenuItems.SelectedItem.ToString()
        Dim itemId As String = -1
        Dim itemPrice As String = -1
        Dim itemDesp As String = -1

        For i = 1 To menuItemsArrayList.Count Step 4

            If menuItemsArrayList(i).ToString() = item Then
                itemId = menuItemsArrayList(i - 1)
                item = menuItemsArrayList(i)
                itemPrice = menuItemsArrayList(i + 1)
                itemDesp = menuItemsArrayList(i + 2)
                ' Remove from menus array list
                menuItemsArrayList.RemoveAt(i)
                menuItemsArrayList.RemoveAt(i - 1)
                menuItemsArrayList.RemoveAt(i - 1)
                menuItemsArrayList.RemoveAt(i - 1)
                ' End the for loop
                Exit For
            End If

        Next
        ' Update in database
        Dim response As Boolean = DataLayer.RemoveFromMenu(CInt(itemId), 1)

        ' Update the digital board at run time
        MainMenu.digital.updateDespPanel()
    End Sub

    Public Sub FillCategories()
        Me.Cursor = Cursors.WaitCursor
        If cmbCategories.SelectedValue <> Nothing Then
            cmbCategorySelected = CInt(cmbCategories.SelectedValue.ToString())
            If (Not bgwLoadTwo.IsBusy) Then
                bgwLoadOne.RunWorkerAsync()
            End If
        End If
    End Sub

    Public Sub FillMenuItems()
        Me.Cursor = Cursors.WaitCursor
        If (Not bgwLoadTwo.IsBusy) Then
            bgwLoadTwo.RunWorkerAsync()
        End If
    End Sub

    Private Sub bgwOne_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwLoadOne.DoWork
        itemsArrayList = DataLayer.GetItemOfCategory(cmbCategorySelected)
    End Sub

    Private Sub loadCompleteOne(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwLoadOne.RunWorkerCompleted
        updateUI()
        Me.Cursor = Cursors.Arrow
    End Sub

    Private Sub bgwTwo_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwLoadTwo.DoWork
        menuItemsArrayList = DataLayer.GetMenuItems(1)
    End Sub

    Private Sub loadCompleteTwo(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwLoadTwo.RunWorkerCompleted
        updateUI()
        Me.Cursor = Cursors.Arrow
    End Sub

    Private Sub clearUI()
        lstMenuItems.Items.Clear()
        lstItems.Items.Clear()

    End Sub

    Private Function checkRep(input As String) As Boolean
        For i = 1 To menuItemsArrayList.Count Step 4
            If input = menuItemsArrayList(i) Then
                Return True
            End If
        Next
        Return False
    End Function

    Private Sub updateUI()
        clearUI()

        If Not itemsArrayList Is Nothing Then
            'Fill CategoryBox
            For i = 1 To itemsArrayList.Count Step 4
                lstItems.Items.Add(itemsArrayList(i))
            Next
        End If
        If Not menuItemsArrayList Is Nothing Then
            'Fill MenuBox
            For i = 1 To menuItemsArrayList.Count Step 4
                lstMenuItems.Items.Add(menuItemsArrayList(i))
            Next
        End If

    End Sub

    Private Sub btnName_CheckedChanged(sender As Object, e As EventArgs) Handles btnName.CheckedChanged
        btnArray(0) = Not btnArray(0)

        Select Case MainMenu.tabTwo.Name
            Case "Font"
                despFontArray(0) = MainMenu.tabTwo.btnOk_Click
            Case "Color"
                despColorArray(0) = MainMenu.tabTwo.btnOk_Click
        End Select

        MainMenu.tabTwo.changeSelected(btnName.Text)
    End Sub

    Private Sub btnPrice_CheckedChanged(sender As Object, e As EventArgs) Handles btnPrice.CheckedChanged
        btnArray(1) = Not btnArray(1)

        Select Case MainMenu.tabTwo.Name
            Case "Font"
                despFontArray(1) = MainMenu.tabTwo.btnOk_Click
            Case "Color"
                despColorArray(1) = MainMenu.tabTwo.btnOk_Click
        End Select

        MainMenu.tabTwo.changeSelected(btnPrice.Text)
    End Sub

    Private Sub btnDesp_CheckedChanged(sender As Object, e As EventArgs) Handles btnDesp.CheckedChanged
        btnArray(2) = Not btnArray(2)

        Select Case MainMenu.tabTwo.Name
            Case "Font"
                despFontArray(2) = MainMenu.tabTwo.btnOk_Click
            Case "Color"
                despColorArray(2) = MainMenu.tabTwo.btnOk_Click
        End Select

        MainMenu.tabTwo.changeSelected(btnDesp.Text)
    End Sub

    Private Sub lstMenuItems_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstMenuItems.SelectedIndexChanged
        selectedItemId = menuItemsArrayList(lstMenuItems.SelectedIndex * 4)
        MainMenu.fontD.loadPanel()
        MainMenu.colorD.loadPanel()
    End Sub

    Public Sub BinarySerialize()
        Dim tempColor As New ArrayList
        tempColor.Add(despBgColor)
        tempColor.Add(itemPanelColor)
        tempColor.Add(itemBorderColor)
        tempColor.Add(despColorArray(0))
        tempColor.Add(despColorArray(1))
        tempColor.Add(despColorArray(2))
        tempColor.Add(weatherColor)
        tempColor.Add(newsColor)

        Using str As FileStream = File.Create("SetupColor.bin")
            Dim bf As New BinaryFormatter()
            bf.Serialize(str, tempColor)
        End Using

        Using str As FileStream = File.Create("SetupFont.bin")
            Dim bf As New BinaryFormatter()
            bf.Serialize(str, despFontArray)
        End Using

        Using str As FileStream = File.Create("SetupFrq.bin")
            Dim bf As New BinaryFormatter()
            bf.Serialize(str, slideShowFreq)
        End Using
    End Sub

    Public Sub BinaryDeserialize()
        Try
            Using str As FileStream = File.OpenRead("SetupColor.bin")
                Dim bf As New BinaryFormatter()
                Dim tempColor = DirectCast(bf.Deserialize(str), ArrayList)
                despBgColor = tempColor(0)
                itemPanelColor = tempColor(1)
                itemBorderColor = tempColor(2)
                despColorArray(0) = tempColor(3)
                despColorArray(1) = tempColor(4)
                despColorArray(2) = tempColor(5)
                weatherColor = tempColor(6)
                newsColor = tempColor(7)
            End Using
        Catch ex As Exception

        End Try

        Try
            Using str As FileStream = File.OpenRead("SetupFont.bin")
                Dim bf As New BinaryFormatter()
                Dim tempFont = DirectCast(bf.Deserialize(str), Font())
                despFontArray(0) = tempFont(0)
                despFontArray(1) = tempFont(1)
                despFontArray(2) = tempFont(2)
            End Using
        Catch ex As Exception

        End Try
        Try
            Using str As FileStream = File.OpenRead("SetupFrq.bin")
                Dim bf As New BinaryFormatter()
                slideShowFreq = DirectCast(bf.Deserialize(str), Integer)
            End Using
        Catch ex As Exception

        End Try
    End Sub
End Class