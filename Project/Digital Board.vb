Imports System.Net.Mail
Imports System.Net
Imports System.Threading

Public Class Digital_Board

    <STAThread()> _
    Public Shared Sub Main()
        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)

        AddHandler System.Windows.Forms.Application.ThreadException, AddressOf ThreadExceptionHandler
        Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException)
        Dim currentDomain As AppDomain = AppDomain.CurrentDomain
        AddHandler currentDomain.UnhandledException, AddressOf UnhandledExceptionHandler

        Application.Run(New MainMenu)

    End Sub

    Shared Sub UnhandledExceptionHandler(sender As Object, args As UnhandledExceptionEventArgs)
        Dim e As Exception = DirectCast(args.ExceptionObject, Exception)
        'Log(e)
    End Sub

    Shared Sub ThreadExceptionHandler(sender As Object, args As ThreadExceptionEventArgs)
        Dim e As Exception = args.Exception
        'Log(e)
    End Sub

    Public Shared Sub Log(ByVal ex As Exception)
        Dim logDirectory As String = IO.Path.Combine(Application.StartupPath, "ErrorLog")
        Dim logName As String = DateTime.Now.ToString("yyyyMMdd") & ".txt"
        Dim fullName As String = IO.Path.Combine(logDirectory, logName)
        Dim errorString As String = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss") & " >> " & ex.Message & Environment.NewLine & ex.StackTrace & Environment.NewLine

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
