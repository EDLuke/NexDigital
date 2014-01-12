Imports System.Threading
Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary

Public Class MainMenu

#Region "Field"
    Private _tabOne As New Object
    Private _tabTwo As New Object
    Private digitalForm As Integer = 0

    Public slideNum As String = ""
    Public digital As Object
    Public all As Object
    Public admin As Object
    Public addItem As Object
    Public updateItem As Object
    Public cateSetup As Object
    Public setup As Object
    Public setupTwo As Object
    Public fontD As Object
    Public colorD As Object
    Public fontD2 As Object
    Public colorD2 As Object
    Public mgOne As Object
    Public mgTwo As Object
    Public vwOne As Object
    Public vwTwo As Object

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

    Public Class PowerManager

#Region "Constructor"
        Private Sub New()
            'keep compiler from creating default constructor to create utility class
        End Sub
#End Region
        Private Declare Function SetThreadExecutionState Lib "kernel32" (ByVal esFlags As EXECUTION_STATE) As EXECUTION_STATE

        Public Enum EXECUTION_STATE As Integer

            ES_CONTINUOUS = &H80000000

            ES_DISPLAY_REQUIRED = &H2

            ES_SYSTEM_REQUIRED = &H1

        End Enum

        Public Shared Function PowerSaveOff() As EXECUTION_STATE
            Return SetThreadExecutionState(EXECUTION_STATE.ES_SYSTEM_REQUIRED Or EXECUTION_STATE.ES_DISPLAY_REQUIRED Or EXECUTION_STATE.ES_CONTINUOUS)
        End Function

        Public Shared Function PowerSaveOn() As EXECUTION_STATE
            Return SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS)
        End Function

    End Class

    Private Sub MainMenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Cursor = Cursors.WaitCursor
        bgwLoad.RunWorkerAsync()
    End Sub

    Private Sub bgwLoad_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwLoad.DoWork
        styleSetup()
        formSetup()
        mdiSetup()
        idleSetup()
    End Sub

    Private Sub styleSetup()
        Try
            Dim styleString As String = "TS"
            Using str As FileStream = File.OpenRead("Admin.bin")
                Dim bf As New BinaryFormatter()
                Dim adminList = DirectCast(bf.Deserialize(str), String())
                styleString = adminList(2)
            End Using
            If styleString = "TS" Then
                digitalForm = 2
                
            ElseIf styleString = "TM" Then
                digitalForm = 1

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub formSetup()
        Select Case digitalForm
            Case 2
                digital = New FrmDigitalBBTwo
            Case Else
                digital = New FrmDigitalBB
        End Select

        all = New frmAllItems
        admin = New frmAdmin
        addItem = New frmAddItem
        updateItem = New frmUpdateItem
        cateSetup = New frmCategorySetup
        setup = New frmSetup
        setupTwo = New frmSetupTwo
        fontD = New frmFontDialog
        colorD = New frmColorDialog
        fontD2 = New frmFontDialogTwo
        colorD2 = New frmColorDialogTwo
        mgOne = New FrmManageSlideshow
        vwOne = New FrmViewSlideShow
        mgTwo = New FrmManageSlideshowTwo
        vwTwo = New FrmViewSlideShowTwo
    End Sub

    Public Sub reloadData()
        digital.updateDespPanel()
        digital.updateSlideShow()
        addItem.LoadItems()
        all.loadItems()
        cateSetup.loadCategory()
        setup.LoadCategory()
        setup.FillCategories()
        setup.FillMenuItems()
        setupTwo.LoadCategory()
        setupTwo.FillCategories()
        setupTwo.FillMenuItems()
        mgOne.LoadCategory()
        mgOne.FillCategories()
        mgOne.UpdateTreeView()
        vwOne.loadSlideShowPic()
        vwOne.loadTimerFreq()
        mgTwo.LoadCategory()
        mgTwo.FillCategories()
        mgTwo.UpdateTreeView()
        vwTwo.loadSlideShowPic()
        vwTwo.loadTimerFreq()
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

    Private Sub idleSetup()
        PowerManager.PowerSaveOff()
    End Sub

    Private Sub mdiSetup()
        Dim formArray As New ArrayList
        formArray.Add(all)
        formArray.Add(addItem)
        formArray.Add(admin)
        formArray.Add(updateItem)
        formArray.Add(cateSetup)
        formArray.Add(setup)
        formArray.Add(setupTwo)
        formArray.Add(fontD)
        formArray.Add(fontD2)
        formArray.Add(colorD)
        formArray.Add(colorD2)
        formArray.Add(mgOne)
        formArray.Add(vwOne)
        formArray.Add(mgTwo)
        formArray.Add(vwTwo)

        For Each formTemp As Form In formArray
            formTemp.TopLevel = False
            formTemp.FormBorderStyle = Windows.Forms.FormBorderStyle.None
            formTemp.AutoSize = False
            formTemp.Width = tabItemSetup.Width
            formTemp.Height = tabItemSetup.Height
        Next
    End Sub

    Private Sub tabMain_SelectedIndexChanged(sender As Object, e As EventArgs) Handles tabMain.SelectedIndexChanged
        tabSec.Controls.Clear()
        showMainForm()
        showSecForm()
        updateSecForm()
    End Sub

    Private Sub changeStyle()
        Select Case digitalForm
            Case 2
                tabMenuSetupTwo.Name = "tabMgSlideTwo"
                tabMenuSetupTwo.Text = "Manage SlideShow Two"
            Case Else
                tabMenuSetupTwo.Name = "tabMenuSetupTwo"
                tabMenuSetupTwo.Text = "Menu Setup Two"
        End Select
    End Sub

    Private Sub tabSec_SelectedIndexChanged(sender As Object, e As EventArgs) Handles tabSec.SelectedIndexChanged
        If tabSec.Controls.Count <> 0 Then
            updateSecForm()
        End If
    End Sub

    Private Sub showMainForm()
        Select Case tabMain.SelectedTab.Name
            Case "tabItemSetup"
                _tabOne = all
            Case "tabMenuSetup"
                _tabOne = setup
            Case "tabMenuSetupTwo"
                _tabOne = setupTwo
            Case "tabMgSlideOne"
                _tabOne = mgOne
            Case "tabMgSlideTwo"
                _tabOne = mgTwo
        End Select

        tabMain.SelectedTab.Controls.Add(_tabOne)
        _tabOne.Show()
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
                _tabTwo = addItem
            Case "Update Item"
                _tabTwo = updateItem
                Try
                    updateItem.setFormData(_tabOne.selectedItemId)
                Catch ex As Exception

                End Try
            Case "Delete Item"
                checkDelete()
                Return
            Case "Category Setup"
                _tabTwo = cateSetup
            Case "Admin"
                _tabTwo = admin
            Case "Font"
                _tabTwo = fontD
            Case "Color"
                _tabTwo = colorD
            Case "Font Two"
                _tabTwo = fontD2
            Case "Color Two"
                _tabTwo = colorD2
            Case "View SlideShow One"
                _tabTwo = vwOne
            Case "View SlideShow Two"
                _tabTwo = vwTwo
        End Select

        admin.hideAdminControl()
        tabSec.SelectedTab.Controls.Add(_tabTwo)
        _tabTwo.Show()
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        digital.Dispose()
        GC.Collect()

        PowerManager.PowerSaveOn()
        Me.Dispose()
    End Sub

    Private Sub checkDelete()
        Dim Box As MsgBoxResult = MsgBox("Are You Sure You Wish To Delete This Item?", MsgBoxStyle.YesNo)
        If Box = MsgBoxResult.Yes Then
            Dim result As Boolean = DataLayer.DeleteItem(CInt(_tabOne.selectedItemId))
            If result = True Then
                MsgBox("Item Deleted!")
                _tabOne.loadItems()
                reloadData()
                Try
                    'Update the digital board at run time
                    digital.updateDespPanel()
                    digital.updateSlideShow()
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
        setup.BinaryDeserialize()

        If TypeOf digital Is FrmDigitalBB Or TypeOf digital Is FrmDigitalBBTwo Then
            digital.Dispose()
        End If

        Select Case digitalForm
            Case 2
                digital = New FrmDigitalBBTwo
            Case Else
                digital = New FrmDigitalBB
        End Select
        digital.loadform()
        Application.DoEvents()

        If btnFull.Checked = True Then
            digital.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        Else
            digital.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
        End If

        Dim screens = Screen.AllScreens
        If screens.Length = 2 Then
            If btnSec.Checked = True Then
                setFormLocation(digital, screens(1), True)
            Else
                setFormLocation(digital, screens(0), False)
            End If
        End If

        digital.Show()
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