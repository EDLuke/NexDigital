Public Class frmFontDialogTwo

    Private labelName As New MenuItemLabel
    Private labelDetail As New ArrayList
    Private selectedFont As Font

    Private Sub frmFontDialog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Populate list boxes
        Dim fontRow As Integer = 0
        Dim fontPos As Integer = 0
        For Each fnt As FontFamily In FontFamily.Families
            lstFont.Items.Add(fnt.Name)
            If fnt.Name = MainMenu.setupTwo.despFontArray(0).Name Then
                fontPos = fontRow
            End If
            fontRow += 1
        Next
        lstFontStyle.Items.AddRange({"Regular", "Italic", "Bold", "Bold Italic"})
        lstFontSize.Items.AddRange({"8", "9", "10", "12", "14", "16", "18", "20", "24", "36", "48", "72"})
        lstFont.SelectedIndex = fontPos
        lstFontStyle.SelectedIndex = 0
        lstFontSize.SelectedIndex = 6

        loadPanel()
    End Sub

    Public Sub loadPanel()
        labelDetail = DataLayer.GetItemDetails(MainMenu.setup.selectedItemId)

        If labelDetail.Count <> 0 Then

            labelName.Width = flowPanel.Width

            labelName.TitleFont = MainMenu.setup.despFontArray(0)
            labelName.PriceFont = MainMenu.setup.despFontArray(1)
            labelName.TitleColor = MainMenu.setup.despColorArray(0)
            labelName.PriceColor = MainMenu.setup.despColorArray(1)

            labelName.TitleFont = MainMenu.setup.despFontArray(0)
            labelName.PriceFont = MainMenu.setup.despFontArray(1)
            labelName.DespFont = MainMenu.setup.despFontArray(2)
            labelName.TitleColor = MainMenu.setup.despColorArray(0)
            labelName.PriceColor = MainMenu.setup.despColorArray(1)
            labelName.DespColor = MainMenu.setup.despColorArray(2)
            MenuItemLabel.BorderColor = MainMenu.setup.itemBorderColor

            labelName.Title = labelDetail(0)
            labelName.Money = labelDetail(2)
            labelName.Desp = labelDetail(1)

            flowPanel.Controls.Add(labelName)
        End If

    End Sub

    Public Sub changeSelected(ByVal str As String)
        lblSel.Text = str & " Font is selected"
    End Sub

    Private Sub updateSample()
        Dim sel = chooseSelected()

        Dim fontStyleTemp As New FontStyle
        Select Case lstFontStyle.SelectedItem.ToString
            Case "Regular"
                fontStyleTemp = FontStyle.Regular
            Case "Bold"
                fontStyleTemp = FontStyle.Bold
            Case "Italic"
                fontStyleTemp = FontStyle.Italic
            Case "Bold Italic"
                selectedFont = New Font(lstFont.SelectedItem.ToString, CInt(lstFontSize.SelectedItem.ToString), FontStyle.Bold Or FontStyle.Italic)
                Select Case sel
                    Case 0
                        labelName.TitleFont = selectedFont
                    Case 1
                        labelName.PriceFont = selectedFont
                    Case 2
                        labelName.DespFont = selectedFont
                    Case -1
                        Return
                End Select
                Return
        End Select
        selectedFont = New Font(lstFont.SelectedItem.ToString, CInt(lstFontSize.SelectedItem.ToString), fontStyleTemp)

        Select Case sel
            Case 0
                labelName.TitleFont = selectedFont
            Case 1
                labelName.PriceFont = selectedFont
            Case 2
                labelName.DespFont = selectedFont
            Case -1
                Return
        End Select
    End Sub

    Private Function chooseSelected() As Integer
        If MainMenu.setup.btnArray(0) Then
            selectedFont = labelName.TitleFont
            lblSel.Text = "Name Font is selected"
            Return 0
        ElseIf MainMenu.setup.btnArray(1) Then
            selectedFont = labelName.PriceFont
            lblSel.Text = "Price Font is selected"
            Return 1
        ElseIf MainMenu.setup.btnArray(2) Then
            selectedFont = labelName.DespFont
            lblSel.Text = "Description Font is selected"
            Return 2
        End If
        Return -1
    End Function

    Private Sub lstFont_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstFont.SelectedIndexChanged
        If lstFont.SelectedIndex <> -1 And lstFontStyle.SelectedIndex <> -1 And lstFontSize.SelectedIndex <> -1 Then
            updateSample()
        End If

    End Sub

    Private Sub lstFontStyle_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstFontStyle.SelectedIndexChanged
        If lstFont.SelectedIndex <> -1 And lstFontStyle.SelectedIndex <> -1 And lstFontSize.SelectedIndex <> -1 Then
            updateSample()
        End If
    End Sub

    Private Sub lstFontSize_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstFontSize.SelectedIndexChanged
        If lstFont.SelectedIndex <> -1 And lstFontStyle.SelectedIndex <> -1 And lstFontSize.SelectedIndex <> -1 Then
            updateSample()
        End If
    End Sub

    Private Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        Dim sel = chooseSelected()
        Dim str As String = ""

        Select Case sel
            Case 0
                MainMenu.setup.despFontArray(0) = selectedFont
                str = "Title"
            Case 1
                MainMenu.setup.despFontArray(1) = selectedFont
                str = "Price"
            Case 2
                MainMenu.setup.despFontArray(2) = selectedFont
                str = "Description"
            Case -1
                Return
        End Select

        MainMenu.setup.BinarySerialize()
        MainMenu.digital.updateDespPanel()

        MsgBox(str & "'s Font has changed")
    End Sub
End Class