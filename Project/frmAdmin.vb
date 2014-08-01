Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary

Public Class frmAdmin

    Private adminList(4) As String
    '0 Logo Image
    '1 Weather Location
    '2 Style (TS as Two SlideShows, TM as Two Menus)
    '3 SlideNum (One as One SlideShow, Two as Two SlideShows)
    '4 Trial End Date

    Private Sub showAdminControl()
        btnLogo.Visible = True
        btnBgColor.Visible = True
        btnWL.Visible = True
        btnHL.Visible = True
        btnHLB.Visible = True
        btnWeatherColor.Visible = True
        btnNewsColor.Visible = True
        cbxTM.Visible = True
        cbxTS.Visible = True
        lblNote.Visible = True
    End Sub

    Public Sub hideAdminControl()
        btnLogo.Visible = False
        btnBgColor.Visible = False
        btnWL.Visible = False
        btnHL.Visible = False
        btnHLB.Visible = False
        btnWeatherColor.Visible = False
        btnNewsColor.Visible = False
        cbxTM.Visible = False
        cbxTS.Visible = False
        lblNote.Visible = False
        lblSlideShow.Visible = False
        cmbSlideNum.Visible = False
    End Sub

    Private Sub frmAdmin_KeyDown(sender As System.Object, e As KeyEventArgs) Handles txtPasswd.KeyDown
        If e.KeyCode = Keys.Enter Then
            checkPasswd()
        End If
    End Sub

    Public Sub checkPasswd()
        If txtPasswd.Text = "shakespeare" Then
            showAdminControl()
        Else
            txtPasswd.Clear()
            hideAdminControl()
        End If
    End Sub

    Private Sub btnBgColor_Click(sender As Object, e As EventArgs) Handles btnBgColor.Click
        If ColorDialog1.ShowDialog <> Windows.Forms.DialogResult.Cancel Then
            Digital_Board.digital.changeBackColor(ColorDialog1.Color)
            Digital_Board.setup.despBgColor = ColorDialog1.Color
            Digital_Board.setup.BinarySerialize()
            Digital_Board.digital.updateDespPanel()
        End If
    End Sub

    Private Sub btnLogo_Click(sender As Object, e As EventArgs) Handles btnLogo.Click
        Dim picture As String = ""
        Try
            Dim result As DialogResult = OpenFileDialog1.ShowDialog()

            If result = Windows.Forms.DialogResult.OK Then
                picture = OpenFileDialog1.SafeFileName
            End If

            Try
                ' Copy image file
                If (Not File.Exists(Directory.GetCurrentDirectory() & "\images\" & picture)) Then
                    FileCopy(OpenFileDialog1.FileName, Directory.GetCurrentDirectory() & "\images\" & picture)
                End If

                Digital_Board.digital.changeRestLogo(Image.FromFile(Directory.GetCurrentDirectory() & "\images\" & picture))
                adminList(0) = Directory.GetCurrentDirectory() & "\images\" & picture
            Catch ex As Exception

            End Try

        Catch ex As FileNotFoundException
            picture = ""
        End Try

        BinarySerialize()
    End Sub

    Private Sub BinarySerialize()
        Using str As FileStream = File.Create("Admin.bin")
            Dim bf As New BinaryFormatter()
            bf.Serialize(str, adminList)
        End Using
    End Sub

    Private Sub BinaryDeserialize()
        Dim styleString As String
        If File.Exists("Admin.bin") Then
            Using str As FileStream = File.OpenRead("Admin.bin")
                Dim bf As New BinaryFormatter()
                Dim adminList = DirectCast(bf.Deserialize(str), String())
                styleString = adminList(2)
            End Using
            If styleString = "TS" Then
                cbxTS.Checked = True
            ElseIf styleString = "TM" Then
                cbxTM.Checked = True
            End If
        End If

    End Sub

    Private Sub btnWL_Click(sender As Object, e As EventArgs) Handles btnWL.Click
        Dim newWeatherLocation As String = InputBox("Please enter a Location Code", "Weather Location", "2396503")
        Digital_Board.digital.weatherLocationCode = newWeatherLocation
        adminList(1) = newWeatherLocation

        BinarySerialize()
    End Sub

    Private Sub frmAdmin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        loadDefaultAdminList()
    End Sub

    Private Sub loadDefaultAdminList()
        adminList(1) = "2396503"
    End Sub

    Private Sub btnHL_Click(sender As Object, e As EventArgs) Handles btnHL.Click
        If ColorDialog2.ShowDialog <> Windows.Forms.DialogResult.Cancel Then
            Digital_Board.setup.itemPanelColor = ColorDialog2.Color
            Digital_Board.setup.BinarySerialize()
            Digital_Board.digital.updateDespPanel()
        End If
    End Sub

    Private Sub btnHLB_Click(sender As Object, e As EventArgs) Handles btnHLB.Click
        If ColorDialog3.ShowDialog <> Windows.Forms.DialogResult.Cancel Then
            Digital_Board.setup.itemBorderColor = ColorDialog3.Color
            Digital_Board.setup.BinarySerialize()
            Digital_Board.digital.updateDespPanel()
        End If
    End Sub

    Private Sub btnWeatherColor_Click(sender As Object, e As EventArgs) Handles btnWeatherColor.Click
        If ColorDialog4.ShowDialog <> Windows.Forms.DialogResult.Cancel Then
            Digital_Board.setup.weatherColor = ColorDialog4.Color
            Digital_Board.setup.BinarySerialize()
            Digital_Board.digital.updateDespPanel()
        End If
    End Sub

    Private Sub btnNewsColor_Click(sender As Object, e As EventArgs) Handles btnNewsColor.Click
        If ColorDialog5.ShowDialog <> Windows.Forms.DialogResult.Cancel Then
            Digital_Board.setup.newsColor = ColorDialog5.Color
            Digital_Board.setup.BinarySerialize()
            Digital_Board.digital.updateDespPanel()
        End If
    End Sub

    Private Sub cbxTM_CheckedChanged(sender As Object, e As EventArgs) Handles cbxTM.CheckedChanged
        If cbxTM.Checked = True Then
            cbxTS.Checked = False
            adminList(2) = "TM"
        End If
        BinarySerialize()
    End Sub

    Private Sub cbxTS_CheckedChanged(sender As Object, e As EventArgs) Handles cbxTS.CheckedChanged
        If cbxTS.Checked = True Then
            cbxTM.Checked = False
            adminList(2) = "TS"
            lblSlideShow.Visible = True
            cmbSlideNum.Visible = True
        Else
            lblSlideShow.Visible = False
            cmbSlideNum.Visible = False
        End If
        BinarySerialize()
    End Sub

    Private Sub cmbSlideNum_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSlideNum.SelectedIndexChanged
        adminList(3) = cmbSlideNum.SelectedItem.ToString
        BinarySerialize()
        If (Digital_Board.digital Is FrmDigitalBBTwo) Then
            Digital_Board.digital.checkFullSeparte()
        End If
    End Sub

    Private Sub btnTrial_Click(sender As Object, e As EventArgs) Handles btnTrial.Click
        Dim dateString As String
        dateString = DateValue(Now).AddDays(30).ToString("yyyy-MM-dd")
        adminList(4) = dateString
        BinarySerialize()
    End Sub
End Class