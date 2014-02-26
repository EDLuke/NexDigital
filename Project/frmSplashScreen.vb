Imports System.Threading
Imports System.Xml
Imports System.IO

Public Class frmSplashScreen

#Region "Member Variables"
    ' Threading
    Private Shared ms_frmSplash As frmSplashScreen = Nothing
    Private Shared ms_oThread As Thread = Nothing

    ' Fade in and out.
    Private m_dblOpacityIncrement As Double = 0.05
    Private m_dblOpacityDecrement As Double = 0.08
    Private Const TIMER_INTERVAL As Integer = 50

    ' Status and progress bar
    Private m_sStatus As String
    Private m_sTimeRemaining As String
    Private m_dblCompletionFraction As Double = 0.0
    Private m_rProgress As Rectangle

    ' Progress smoothing
    Private m_dblLastCompletionFraction As Double = 0.0
    Private m_dblPBIncrementPerTimerInterval As Double = 0.015

    ' Self-calibration support
    Private m_iIndex As Integer = 1
    Private m_iActualTicks As Integer = 0
    Private m_alPreviousCompletionFraction As ArrayList
    Private m_alActualTimes As New ArrayList()
    Private m_dtStart As DateTime
    Private m_bFirstLaunch As Boolean = False
    Private m_bDTSet As Boolean = False

#End Region

    Private Sub frmSplashScreen_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Opacity = 0.0
        UpdateTimer.Interval = TIMER_INTERVAL
        UpdateTimer.Start()
        Me.ClientSize = Me.BackgroundImage.Size
    End Sub

#Region "Public Static Methods"

    Public Shared Sub showSplashScreen()
        If (ms_frmSplash IsNot Nothing) Then
            Return
        End If

        ms_oThread = New Thread(New ThreadStart(AddressOf frmSplashScreen.showForm))
        ms_oThread.IsBackground = True
        ms_oThread.SetApartmentState(ApartmentState.STA)
        ms_oThread.Start()
    End Sub

    Public Shared Sub closeForm()
        If (ms_frmSplash IsNot Nothing) And (ms_frmSplash.IsDisposed = False) Then
            ms_frmSplash.m_dblOpacityIncrement = -1 * ms_frmSplash.m_dblOpacityDecrement
        End If

        ms_oThread = Nothing
        ms_frmSplash = Nothing

    End Sub

    ' A static method to set the status and update the reference.
    Public Shared Sub SetStatus(newStatus As String)
        SetStatus(newStatus, True)
    End Sub

    ' A static method to set the status and optionally update the reference.
    ' This is useful if you are in a section of code that has a variable
    ' set of status string updates.  In that case, don't set the reference.
    Public Shared Sub SetStatus(newStatus As String, setReference As Boolean)
        If ms_frmSplash Is Nothing Then
            Return
        End If

        ms_frmSplash.m_sStatus = newStatus

        If setReference Then
            ms_frmSplash.SetReferenceInternal()
        End If
    End Sub

    ' Static method called from the initializing application to 
    ' give the splash screen reference points.  Not needed if
    ' you are using a lot of status strings.
    Public Shared Sub SetReferencePoint()
        If ms_frmSplash Is Nothing Then
            Return
        End If
        ms_frmSplash.SetReferenceInternal()

    End Sub
#End Region

#Region "Private Methods"

    ' A private entry point for the thread.
    Private Shared Sub showForm()
        ms_frmSplash = New frmSplashScreen()
        Application.Run(ms_frmSplash)
    End Sub

    ' Internal method for setting reference points.
    Private Sub SetReferenceInternal()
        If m_bDTSet = False Then
            m_bDTSet = True
            m_dtStart = DateTime.Now
            ReadIncrements()
        End If
        Dim dblMilliseconds As Double = ElapsedMilliSeconds()
        m_alActualTimes.Add(dblMilliseconds)
        m_dblLastCompletionFraction = m_dblCompletionFraction
        If m_alPreviousCompletionFraction IsNot Nothing AndAlso m_iIndex < m_alPreviousCompletionFraction.Count Then
            m_dblCompletionFraction = CDbl(m_alPreviousCompletionFraction(System.Math.Max(System.Threading.Interlocked.Increment(m_iIndex), m_iIndex - 1)))
        Else
            m_dblCompletionFraction = If((m_iIndex > 0), 1, 0)
        End If
    End Sub

    ' Utility function to return elapsed Milliseconds since the 
    ' SplashScreen was launched.
    Private Function ElapsedMilliSeconds() As Double
        Dim ts As TimeSpan = DateTime.Now - m_dtStart
        Return ts.TotalMilliseconds
    End Function

    ' Function to read the checkpoint intervals from the previous invocation of the
    ' splashscreen from the XML file.
    Private Sub ReadIncrements()
        Dim sPBIncrementPerTimerInterval As String = SplashScreenXMLStorage.Interval
        Dim dblResult As Double

        If [Double].TryParse(sPBIncrementPerTimerInterval, System.Globalization.NumberStyles.Float, System.Globalization.NumberFormatInfo.InvariantInfo, dblResult) = True Then
            m_dblPBIncrementPerTimerInterval = dblResult
        Else
            m_dblPBIncrementPerTimerInterval = 0.0015
        End If

        Dim sPBPreviousPctComplete As String = SplashScreenXMLStorage.Percents

        If sPBPreviousPctComplete <> "" Then
            Dim aTimes As String() = sPBPreviousPctComplete.Split(Nothing)
            m_alPreviousCompletionFraction = New ArrayList()

            For i As Integer = 0 To aTimes.Length - 1
                Dim dblVal As Double
                If [Double].TryParse(aTimes(i), System.Globalization.NumberStyles.Float, System.Globalization.NumberFormatInfo.InvariantInfo, dblVal) = True Then
                    m_alPreviousCompletionFraction.Add(dblVal)
                Else
                    m_alPreviousCompletionFraction.Add(1.0)
                End If
            Next
        Else
            m_bFirstLaunch = True
            m_sTimeRemaining = ""
        End If
    End Sub

    ' Method to store the intervals (in percent complete) from the current invocation of
    ' the splash screen to XML storage.
    Private Sub StoreIncrements()
        Dim sPercent As String = ""
        Dim dblElapsedMilliseconds As Double = ElapsedMilliSeconds()
        For i As Integer = 0 To m_alActualTimes.Count - 1
            sPercent += (CDbl(m_alActualTimes(i)) / dblElapsedMilliseconds).ToString("0.####", System.Globalization.NumberFormatInfo.InvariantInfo) & " "
        Next

        SplashScreenXMLStorage.Percents = sPercent

        m_dblPBIncrementPerTimerInterval = 1.0 / CDbl(m_iActualTicks)

        SplashScreenXMLStorage.Interval = m_dblPBIncrementPerTimerInterval.ToString("#.000000", System.Globalization.NumberFormatInfo.InvariantInfo)
    End Sub

    Public Shared Function GetSplashScreen() As frmSplashScreen
        Return ms_frmSplash
    End Function

#End Region

#Region "Event Handlers"
    ' Tick Event handler for the Timer control.  Handle fade in and fade out and paint progress bar. 
    Private Sub UpdateTimer_Tick(sender As Object, e As System.EventArgs)
        lblStatus.Text = m_sStatus

        ' Calculate opacity
        If m_dblOpacityIncrement > 0 Then
            ' Starting up splash screen
            m_iActualTicks += 1
            If Me.Opacity < 1 Then
                Me.Opacity += m_dblOpacityIncrement
            End If
        Else
            ' Closing down splash screen
            If Me.Opacity > 0 Then
                Me.Opacity += m_dblOpacityIncrement
            Else
                StoreIncrements()
                UpdateTimer.[Stop]()
                Me.Close()
            End If
        End If

        ' Paint progress bar
        If m_bFirstLaunch = False AndAlso m_dblLastCompletionFraction < m_dblCompletionFraction Then
            m_dblLastCompletionFraction += m_dblPBIncrementPerTimerInterval
            Dim width As Integer = CInt(Math.Floor(pnlStatus.ClientRectangle.Width * m_dblLastCompletionFraction))
            Dim height As Integer = pnlStatus.ClientRectangle.Height
            Dim x As Integer = pnlStatus.ClientRectangle.X
            Dim y As Integer = pnlStatus.ClientRectangle.Y
            If width > 0 AndAlso height > 0 Then
                m_rProgress = New Rectangle(x, y, width, height)
                If Not pnlStatus.IsDisposed Then
                    Dim g As Graphics = pnlStatus.CreateGraphics()
                    Dim brBackground As New LinearGradientBrush(m_rProgress, Color.FromArgb(58, 96, 151), Color.FromArgb(181, 237, 254), LinearGradientMode.Horizontal)
                    g.FillRectangle(brBackground, m_rProgress)
                    g.Dispose()
                End If
                Dim iSecondsLeft As Integer = 1 + CInt(Math.Truncate(TIMER_INTERVAL * ((1.0 - m_dblLastCompletionFraction) / m_dblPBIncrementPerTimerInterval))) \ 1000
                m_sTimeRemaining = If((iSecondsLeft = 1), String.Format("1 second remaining"), String.Format("{0} seconds remaining", iSecondsLeft))
            End If
        End If
        lblTimeRemaining.Text = m_sTimeRemaining
    End Sub

    ' Close the form if they double click on it.
    Private Sub SplashScreen_DoubleClick(sender As Object, e As System.EventArgs)
        ' Use the overload that doesn't set the parent form to this very window.
        CloseForm()
    End Sub
#End Region

#Region "Auxiliary Classes"
    ''' <summary>
    ''' A specialized class for managing XML storage for the splash screen.
    ''' </summary>
    Friend Class SplashScreenXMLStorage
        Private Shared ms_StoredValues As String = "SplashScreen.xml"
        Private Shared ms_DefaultPercents As String = ""
        Private Shared ms_DefaultIncrement As String = ".015"


        ' Get or set the string storing the percentage complete at each checkpoint.
        Public Shared Property Percents() As String
            Get
                Return GetValue("Percents", ms_DefaultPercents)
            End Get
            Set(value As String)
                SetValue("Percents", value)
            End Set
        End Property
        ' Get or set how much time passes between updates.
        Public Shared Property Interval() As String
            Get
                Return GetValue("Interval", ms_DefaultIncrement)
            End Get
            Set(value As String)
                SetValue("Interval", value)
            End Set
        End Property

        ' Store the file in a location where it can be written with only User rights. (Don't use install directory).
        Private Shared ReadOnly Property StoragePath() As String
            Get
                Return Path.Combine(Application.UserAppDataPath, ms_StoredValues)
            End Get
        End Property

        ' Helper method for getting inner text of named element.
        Private Shared Function GetValue(name As String, defaultValue As String) As String
            If Not File.Exists(StoragePath) Then
                Return defaultValue
            End If

            Try
                Dim docXML As New XmlDocument()
                docXML.Load(StoragePath)
                Dim elValue As XmlElement = TryCast(docXML.DocumentElement.SelectSingleNode(name), XmlElement)
                Return If((elValue Is Nothing), defaultValue, elValue.InnerText)
            Catch
                Return defaultValue
            End Try
        End Function

        ' Helper method for setting inner text of named element.  Creates document if it doesn't exist.
        Public Shared Sub SetValue(name As String, stringValue As String)
            Dim docXML As New XmlDocument()
            Dim elRoot As XmlElement = Nothing
            If Not File.Exists(StoragePath) Then
                elRoot = docXML.CreateElement("root")
                docXML.AppendChild(elRoot)
            Else
                docXML.Load(StoragePath)
                elRoot = docXML.DocumentElement
            End If
            Dim value As XmlElement = TryCast(docXML.DocumentElement.SelectSingleNode(name), XmlElement)
            If value Is Nothing Then
                value = docXML.CreateElement(name)
                elRoot.AppendChild(value)
            End If
            value.InnerText = stringValue
            docXML.Save(StoragePath)
        End Sub
    End Class
#End Region

    Private Sub UpdateTimer_Tick(sender As Object, e As EventArgs) Handles UpdateTimer.Tick

        ' Calculate opacity
        If m_dblOpacityIncrement > 0 Then
            ' Starting up splash screen
            m_iActualTicks += 1
            If Me.Opacity < 1 Then
                Me.Opacity += m_dblOpacityIncrement
            End If
        Else
            ' Closing down splash screen
            If Me.Opacity > 0 Then
                Me.Opacity += m_dblOpacityIncrement
            Else
                StoreIncrements()
                UpdateTimer.[Stop]()
                Me.Close()
            End If
        End If
    End Sub
End Class