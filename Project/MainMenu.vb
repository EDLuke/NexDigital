Imports System.Threading
Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary

Public Class MainMenu

#Region "Field"
    Private _tabOne As New Object
    Private _tabTwo As New Object

    Property tabTwo() As Object
        Get
            Return _tabTwo
        End Get
        Set(ByVal value As Object)
            _tabTwo = value
            Me.Invalidate()
        End Set
    End Property

    Property tabOne() As Object
        Get
            Return _tabOne
        End Get
        Set(ByVal value As Object)
            _tabOne = value
            Me.Invalidate()
        End Set
    End Property
#End Region

    Private Sub MainMenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Cursor = Cursors.WaitCursor
        bgwLoad.RunWorkerAsync()
    End Sub

    Private Sub loadComplete(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwLoad.RunWorkerCompleted
        changeStyle()
        showMainForm()
        tabMain.SelectedIndex = 0
        showSecForm()
        tabSec.SelectedIndex = 0
        updateSecForm()

        Me.Cursor = Cursors.Arrow
    End Sub

    Private Sub tabMain_SelectedIndexChanged(sender As Object, e As EventArgs) Handles tabMain.SelectedIndexChanged
        tabSec.Controls.Clear()
        showMainForm()
        showSecForm()
        updateSecForm()
        _tabOne.Select()
    End Sub

    Private Sub changeStyle()
        If TypeOf (Digital_Board.digital) Is FrmDigitalBB Then
            tabMenuSetupTwo.Name = "tabMgSlideTwo"
            tabMenuSetupTwo.Text = "Manage SlideShow Two"
        ElseIf TypeOf (Digital_Board.digital) Is FrmDigitalBBTwo Then
            tabMenuSetupTwo.Name = "tabMenuSetupTwo"
            tabMenuSetupTwo.Text = "Menu Setup Two"
        End If
    End Sub

    Private Sub tabSec_SelectedIndexChanged(sender As Object, e As EventArgs) Handles tabSec.SelectedIndexChanged
        If tabSec.Controls.Count <> 0 Then
            updateSecForm()
        End If
    End Sub

    Private Sub showMainForm()
        Select Case tabMain.SelectedTab.Name
            Case "tabItemSetup"
                _tabOne = Digital_Board.all
            Case "tabMenuSetup"
                _tabOne = Digital_Board.setup
            Case "tabMenuSetupTwo"
                _tabOne = Digital_Board.setupTwo
            Case "tabMgSlideOne"
                _tabOne = Digital_Board.mgOne
            Case "tabMgSlideTwo"
                _tabOne = Digital_Board.mgTwo
        End Select

        tabMain.SelectedTab.Controls.Add(_tabOne)
        _tabOne.Show()
        _tabOne.Select()
    End Sub

    Private Sub showSecForm()
        Select Case tabMain.SelectedTab.Name
            Case "tabItemSetup"
                tabSec.Controls.Add(New TabPage("Add Item"))
                tabSec.Controls.Add(New TabPage("Update Item"))
                tabSec.Controls.Add(New TabPage("Delete Item"))
                tabSec.Controls.Add(New TabPage("Category Setup"))
                tabSec.Controls.Add(New TabPage("Admin"))
            Case "tabMenuSetup"
                tabSec.Controls.Add(New TabPage("Font"))
                tabSec.Controls.Add(New TabPage("Color"))
            Case "tabMenuSetupTwo"
                tabSec.Controls.Add(New TabPage("Font Two"))
                tabSec.Controls.Add(New TabPage("Color Two"))
            Case "tabMgSlideOne"
                tabSec.Controls.Add(New TabPage("View SlideShow One"))
            Case "tabMgSlideTwo"
                tabSec.Controls.Add(New TabPage("View SlideShow Two"))
        End Select
    End Sub

    Public Sub updateSecForm()
        Select Case tabSec.SelectedTab.Text
            Case "Add Item"
                _tabTwo = Digital_Board.addItem
            Case "Update Item"
                _tabTwo = Digital_Board.updateItem
                Try
                    Digital_Board.updateItem.setFormData(_tabOne.selectedItemId)
                Catch ex As Exception

                End Try
            Case "Delete Item"
                checkDelete()
                Return
            Case "Category Setup"
                _tabTwo = Digital_Board.cateSetup
            Case "Admin"
                _tabTwo = Digital_Board.admin
            Case "Font"
                _tabTwo = Digital_Board.fontD
            Case "Color"
                _tabTwo = Digital_Board.colorD
            Case "Font Two"
                _tabTwo = Digital_Board.fontD2
            Case "Color Two"
                _tabTwo = Digital_Board.colorD2
            Case "View SlideShow One"
                _tabTwo = Digital_Board.vwOne
            Case "View SlideShow Two"
                _tabTwo = Digital_Board.vwTwo
        End Select

        Digital_Board.admin.hideAdminControl()
        tabSec.SelectedTab.Controls.Add(_tabTwo)
        _tabTwo.Show()

        If (_tabTwo IsNot Digital_Board.vwOne) Then
            Digital_Board.vwOne.StopTimer()
        Else
            Digital_Board.vwOne.StartTimer()
        End If

        If (_tabTwo IsNot Digital_Board.vwTwo) Then
            Digital_Board.vwTwo.StopTimer()
        Else
            Digital_Board.vwTwo.StartTimer()
        End If
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Digital_Board.digital.Dispose()
        GC.Collect()
        Me.Dispose()
    End Sub

    Private Sub checkDelete()
        Dim Box As MsgBoxResult = MsgBox("Are You Sure You Wish To Delete This Item?", MsgBoxStyle.YesNo)
        If Box = MsgBoxResult.Yes Then
            Dim result As Boolean = DataLayer.DeleteItem(CInt(_tabOne.selectedItemId))
            If result = True Then
                MsgBox("Item Deleted!")
                _tabOne.loadItems()
                Digital_Board.reloadData()
                Try
                    'Update the digital board at run time
                    Digital_Board.digital.updateDespPanel()
                    Digital_Board.digital.updateSlideShow()
                Catch ex As Exception

                End Try
                tabSec.SelectedIndex = 0
            End If
        Else
            tabSec.SelectTab(0)
        End If
    End Sub

    Private Sub MainMenu_KeyDown(sender As System.Object, e As KeyEventArgs) Handles tabMain.KeyDown
        If _tabOne.Text = "All Items" Then
            If e.KeyCode = Keys.Delete Then
                checkDelete()
            ElseIf e.KeyCode = Keys.Up Then
                _tabOne.selectUp()
            ElseIf e.KeyCode = Keys.Down Then
                _tabOne.selectDown()
            End If
        End If
    End Sub

    Private Sub btnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click
        Digital_Board.setup.BinaryDeserialize()

        If Digital_Board.digital.IsDisposed() Then
            Digital_Board.digitalSetup()
        End If

        Digital_Board.digital.loadform()
        Application.DoEvents()

        If btnFull.Checked = True Then
            Digital_Board.digital.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        Else
            Digital_Board.digital.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
        End If

        Dim screens = Screen.AllScreens
        If screens.Length = 2 Then
            If btnSec.Checked = True Then
                setFormLocation(Digital_Board.digital, screens(1), True)
            Else
                setFormLocation(Digital_Board.digital, screens(0), False)
            End If
        End If

        Digital_Board.digital.Show()
    End Sub

    Private Sub btnFull_Click(sender As Object, e As EventArgs) Handles btnFull.CheckedChanged
        Me.ActiveControl = btnStart
    End Sub

    Private Sub btnSec_Click(sender As Object, e As EventArgs) Handles btnSec.Click
        Me.ActiveControl = btnStart
    End Sub

    Private Sub setFormLocation(ByRef form As Form, ByRef screen As Screen, ByVal second As Boolean)
        If second Then
            form.StartPosition = FormStartPosition.Manual
            Dim bounds = screen.Bounds
            'form.Location = New Point(screen.Bounds.X, screen.Bounds.Y)
            form.Left = screen.Bounds.X
        Else
            form.StartPosition = FormStartPosition.WindowsDefaultLocation
            Dim bounds = screen.Bounds
            'form.Location = New Point(0, screen.Bounds.Y)
            form.Left = 0
        End If

    End Sub
End Class