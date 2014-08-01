Imports System.Net.Mail
Imports System.Net
Imports System.Threading
Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary

Public Class Digital_Board

    Public Shared mainFrm As MainMenu
    Public Shared digital As Object
    Public Shared all As frmAllItems
    Public Shared admin As frmAdmin
    Public Shared addItem As frmAddItem
    Public Shared updateItem As frmUpdateItem
    Public Shared cateSetup As frmCategorySetup
    Public Shared setup As frmSetup
    Public Shared setupTwo As frmSetupTwo
    Public Shared fontD As frmFontDialog
    Public Shared colorD As frmColorDialog
    Public Shared fontD2 As frmFontDialogTwo
    Public Shared colorD2 As frmColorDialogTwo
    Public Shared mgOne As FrmManageSlideshow
    Public Shared mgTwo As FrmManageSlideshowTwo
    Public Shared vwOne As FrmViewSlideShow
    Public Shared vwTwo As FrmViewSlideShowTwo

    Public Shared endDate As Date

    Private Shared digitalForm As Integer = 0


    <STAThread()> _
    Public Shared Sub Main()
        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)

        AddHandler System.Windows.Forms.Application.ThreadException, AddressOf ThreadExceptionHandler
        Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException)
        Dim currentDomain As AppDomain = AppDomain.CurrentDomain
        AddHandler currentDomain.UnhandledException, AddressOf UnhandledExceptionHandler

        If checkTrial() Then
            styleSetup()
            idleSetup()
            digitalSetup()
            formSetup()
            mdiSetup()
            DataLayer.Validate()
            mainFrm = New MainMenu
            Try
                Application.Run(mainFrm)
            Catch ex As Exception
                Log(ex)
            End Try
        Else
            MsgBox("Trial has ended")
        End If

    End Sub

    Private Shared Function checkTrial() As Boolean
        Try
            If (File.Exists("Admin.bin")) Then
                Using str As FileStream = File.OpenRead("Admin.bin")
                    Dim bf As New BinaryFormatter()
                    Dim adminList = DirectCast(bf.Deserialize(str), String())
                    endDate = Date.ParseExact(adminList(4), "yyyy-MM-dd", System.Globalization.DateTimeFormatInfo.InvariantInfo)
                End Using
                If Date.Now.CompareTo(endDate) < 1 Then
                    Return True
                Else
                    Return False
                End If
            End If
        Catch ex As Exception

        End Try
        Return True
    End Function

    Private Shared Sub styleSetup()
        Try
            Dim styleString As String = "TS"
            If (File.Exists("Admin.bin")) Then
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
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Shared Sub digitalSetup()
        Select Case digitalForm
            Case 2
                digital = New FrmDigitalBBTwo
            Case Else
                digital = New FrmDigitalBB
        End Select
    End Sub


    Private Shared Sub formSetup()
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

    Private Shared Sub mdiSetup()
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
            formTemp.Size = New System.Drawing.Size(408, 471)
        Next
    End Sub

    Public Shared Sub reloadData()
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

    Private Shared Sub idleSetup()
        PowerManager.PowerSaveOff()
    End Sub

    Shared Sub UnhandledExceptionHandler(sender As Object, args As UnhandledExceptionEventArgs)
        Dim e As Exception = DirectCast(args.ExceptionObject, Exception)
        Log(e)
    End Sub

    Shared Sub ThreadExceptionHandler(sender As Object, args As ThreadExceptionEventArgs)
        Dim e As Exception = args.Exception
        Log(e)
    End Sub

    Public Shared Sub Log(ByVal ex As Exception)
        Dim logDirectory As String = IO.Path.Combine(Application.StartupPath, "ErrorLog")
        Dim logName As String = DateTime.Now.ToString("yyyyMMdd") & ".txt"
        Dim fullName As String = IO.Path.Combine(logDirectory, logName)
        Dim errorString As String = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss") & " >> " & ex.Message & Environment.NewLine & "" & Environment.NewLine & ex.StackTrace & Environment.NewLine

        Try
            If Not IO.Directory.Exists(logDirectory) Then
                IO.Directory.CreateDirectory(logDirectory)
            End If

            IO.File.AppendAllText(fullName, errorString)

        Catch ignore As Exception

        End Try

        Try
            Dim message As New MailMessage()
            Dim smtp As New SmtpClient()

            message.From = New MailAddress("nexdigital@hotmail.com")
            message.[To].Add(New MailAddress("lukezhang213@gmail.com"))
            message.Subject = logName + " " + ex.GetType.ToString()
            message.Body = errorString

            smtp.Port = 587
            smtp.Host = "smtp.live.com"
            smtp.EnableSsl = True
            smtp.UseDefaultCredentials = False
            smtp.Credentials = New NetworkCredential("nexdigital@hotmail.com", "19930213LUKE")
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network
            smtp.Send(message)
        Catch exc As Exception

        End Try
    End Sub
End Class
