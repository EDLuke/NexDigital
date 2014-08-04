Imports System.IO
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.Net
Imports System.ComponentModel
Imports System.Threading
Imports System.Reflection
Imports Microsoft.DirectX.AudioVideoPlayback

Public Class FrmDigitalBB
    Public weatherLocationCode As String = "2396503"
    Public internet As Boolean
    Public frqOne As Integer = 5000
    Public frqTwo As Integer = 5000

    Private imageNumber, imageCount, newsNumber, newsCount As Integer
    Private Pics(100) As Object
    Private weatherPic As Image
    Private Full(100), slideFullSeparte, fullAnimate As Boolean
    Private slideNum As String
    Private despFontArray(2) As Font
    Private despBgColor, itemPanelColor, itemBorderColor, despColorArray(2) As Color
    Private newsArrayList, animationOneList, animationTwoList, SlideShowPics As ArrayList
    Private w_Thread As CallBackThread
    Private n_Thread As CallBackThread
    Private n As New clsNews
    Private w As New clsWeather(weatherLocationCode, "f")
    Private video As Video

    Private Sub FrmViewSlideShow_Shown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown
        Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
        loadForm()
        Threading.Thread.CurrentThread.Priority = ThreadPriority.Highest
    End Sub

    Public Sub loadForm()
        'Start BackgroundWorker
        If Not bgwLoad.IsBusy Then
            bgwLoad.RunWorkerAsync()
        End If

        'Stretch everything according to the screen
        stretchScreen()

        fullAnimate = False

        'Update the description panel and the slideshow()
        updateDespPanel()
        updateSlideShow()
        Me.Activate()
    End Sub

    Private Sub bgwLoad_DoWork(sender As Object, e As DoWorkEventArgs) Handles bgwLoad.DoWork
        'Load Frequenct and News
        loadFrq()
        loadNews()
        loadThread()
        loadBinaryDeserialize()

        'Update News Information with boolean 'internet' which is set by loadNews()
        updateNews(internet)
    End Sub

    Private Sub loadFrq()
        frqOne = Timer1.Interval
        frqTwo = Timer1.Interval
    End Sub

    Private Sub loadThread()
        w_Thread = New CallBackThread(Me, AddressOf weatherThread, AddressOf updateWeather)
        n_Thread = New CallBackThread(Me, AddressOf newsThread, AddressOf updateNews)
        n_Thread.Start()
        w_Thread.Start()
    End Sub

    Private Sub weatherThread()
        Try
            While True
                w = New clsWeather(weatherLocationCode, "f")
                w_Thread.UpdateUI(internet.ToString)
                Threading.Thread.Sleep(10000)
            End While
        Catch ex As Exception
            loadNews()
        End Try
    End Sub

    Private Sub newsThread()
        Try
            While True
                n_Thread.UpdateUI(internet.ToString)
                Threading.Thread.Sleep(50)
            End While
        Catch ex As Exception

        End Try
    End Sub

    Public Sub stretchScreen()
        Me.SuspendLayout()

        'Full Screen
        Me.WindowState = FormWindowState.Maximized

        'Scretch Everything
        PictureBox1.Size = New Size(Me.Height * 0.475, Me.Height * 0.475)

        flowPanel.Width = (Me.Width - Me.Height * 0.475) / 2
        flowPanel.Height = Me.Height * 0.95

        flowPanelTwo.Width = (Me.Width - Me.Height * 0.475) / 2
        flowPanelTwo.Height = Me.Height * 0.95

        lblNews.Width = Me.Width
        lblNews.Height = Me.Height * 0.05

        lblLogo.AutoSize = False
        lblLogo.Width = Me.Height * 0.475
        lblLogo.Height = Me.Height * 0.475

        'Change Locations as well
        PictureBox1.Location = New Point(Me.Width - Me.Height * 0.475, Me.Height * 0.475)
        flowPanel.Location = New Point(0, 0)
        flowPanelTwo.Location = New Point((Me.Width - Me.Height * 0.475) / 2, 0)
        lblNews.Location = New Point(0, Me.Height * 0.95)
        lblLogo.Location = New Point(Me.Width - Me.Height * 0.475, 0)
        lblNow.Location = New Point(Me.Width - Me.Height * 0.485, 0)

        Me.ResumeLayout()
        Me.Invalidate()
    End Sub

    Public Sub changeRestLogo(ByVal img As Image)
        lblLogo.Image = img
    End Sub

    Public Sub changeBackColor(ByVal color As Color)
        Me.BackColor = color
        lblNews.BackColor = color
        lblNow.BackColor = color
        flowPanel.BackColor = color
    End Sub

    Private Sub updateNews(ByVal status As String)
        If Not fullAnimate Then
            If lblNews.tick = False Or newsArrayList Is Nothing Then
                loadNews()
            End If
        End If
    End Sub

    Private Sub loadNews()
        Try
            n = New clsNews
            newsArrayList = n.GetNewsArray().GetRange(0, n.GetNewsArray.Count)
            internet = True
            newsCount = newsArrayList.Count
            newsNumber = 0
            lblNews.loadNews(newsArrayList)
        Catch ex As WebException
            internet = False
        End Try
    End Sub

    Private Sub updateWeather(ByVal status As String)
        If CBool(status) Then
            lblNow.Text = w.now
            weatherPic = w.image
            Me.Invalidate()
        Else
            lblNow.Text = ""
        End If
    End Sub

    Public Sub updateSlideShow()
        Timer1.Stop()

        loadBinaryDeserialize()

        Array.Clear(Pics, 0, Pics.Length)
        Array.Clear(Full, 0, Full.Length)

        SlideShowPics = DataLayer.GetSlideShowItems(1)
        imageCount = SlideShowPics.Count / 3

        For i = 1 To SlideShowPics.Count Step 3
            If (SlideShowPics(i).ToString.Contains(".avi")) Then
                Pics((i - 1) / 3) = Directory.GetCurrentDirectory() & "\images\" & SlideShowPics(i).ToString()
            Else
                Pics((i - 1) / 3) = resizeImage(Image.FromFile(Directory.GetCurrentDirectory() & "\images\" & SlideShowPics(i).ToString()))
            End If
        Next

        imageNumber = 0

        Timer1.Start()
    End Sub

    Private Function resizeImage(ByVal image As Image) As Image
        Dim thumbBit = New Bitmap(PictureBox1.Width, PictureBox1.Height)
        Dim thumbnail = Graphics.FromImage(thumbBit)
        thumbnail.CompositingQuality = CompositingQuality.Invalid
        thumbnail.SmoothingMode = SmoothingMode.None
        thumbnail.InterpolationMode = InterpolationMode.Low

        Dim imageRectangel = New Rectangle(0, 0, PictureBox1.Width, PictureBox1.Height)
        thumbnail.DrawImage(image, imageRectangel)

        Return thumbBit
    End Function

    Public Sub updateDespPanel()
        Timer1.Stop()

        flowPanel.Controls.Clear()
        flowPanelTwo.Controls.Clear()
        Digital_Board.setup.loadDefaults()

        Dim despList = DataLayer.GetMenuItems(1)
        Dim despListTwo = DataLayer.GetMenuItems(2)

        Array.Copy(Digital_Board.setup.despColorArray, despColorArray, 3)
        Array.Copy(Digital_Board.setup.despFontArray, despFontArray, 3)

        despBgColor = Digital_Board.setup.despBgColor
        itemPanelColor = Digital_Board.setup.itemPanelColor
        itemBorderColor = Digital_Board.setup.itemBorderColor
        lblNews.NewsColor = Digital_Board.setup.newsColor
        lblNow.ForeColor = Digital_Board.setup.weatherColor

        'Change the background color
        Me.BackColor = despBgColor

        For i = 1 To despList.Count Step 4
            Dim labelPanel As New Panel
            labelPanel.BackColor = despBgColor
            labelPanel.AutoSize = True

            Dim labelName As New MenuItemLabel
            labelName.TitleFont = despFontArray(0)
            labelName.PriceFont = despFontArray(1)
            labelName.DespFont = despFontArray(2)
            labelName.TitleColor = despColorArray(0)
            labelName.PriceColor = despColorArray(1)
            labelName.DespColor = despColorArray(2)
            MenuItemLabel.BorderColor = itemBorderColor
            labelName.Width = (Me.Width - Me.Height * 0.485) / 2

            labelName.ID = despList(i - 1)
            labelName.Title = despList(i)
            labelName.Money = despList(i + 1)
            labelName.Desp = despList(i + 2)

            labelPanel.Controls.Add(labelName)
            flowPanel.Controls.Add(labelPanel)
        Next

        For i = 1 To despListTwo.Count Step 4
            Dim labelPanel As New Panel
            labelPanel.BackColor = despBgColor
            labelPanel.AutoSize = True

            Dim labelName As New MenuItemLabel
            labelName.TitleFont = despFontArray(0)
            labelName.PriceFont = despFontArray(1)
            labelName.DespFont = despFontArray(2)
            labelName.TitleColor = despColorArray(0)
            labelName.PriceColor = despColorArray(1)
            labelName.DespColor = despColorArray(2)
            MenuItemLabel.BorderColor = itemBorderColor
            labelName.Width = (Me.Width - Me.Height * 0.485) / 2

            labelName.ID = despListTwo(i - 1)
            labelName.Title = despListTwo(i)
            labelName.Money = despListTwo(i + 1)
            labelName.Desp = despListTwo(i + 2)

            labelPanel.Controls.Add(labelName)
            flowPanelTwo.Controls.Add(labelPanel)
        Next

        Timer1.Start()
    End Sub

    Private Sub updatePanel(ByVal goFull As Boolean)
        Try
            Dim SlideShowPics As ArrayList = DataLayer.GetSlideShowItems(1)
            If (SlideShowPics.Count <> 0) Then

                For Each pn As Panel In flowPanel.Controls
                    Dim tempLabelPanel As MenuItemLabel = pn.Controls(0)
                    If goFull Then
                        If CInt(tempLabelPanel.ID) = SlideShowPics((imageNumber) * 3) Then
                            turnOnBorder(pn)
                        Else
                            turnOffBorder(pn)
                        End If
                    Else
                        turnOffBorder(pn)
                    End If
                Next

                For Each pn As Panel In flowPanelTwo.Controls
                    Dim tempLabelPanel As MenuItemLabel = pn.Controls(0)
                    If goFull Then
                        If CInt(tempLabelPanel.ID) = SlideShowPics((imageNumber) * 3) Then
                            turnOnBorder(pn)
                        Else
                            turnOffBorder(pn)
                        End If
                    Else
                        turnOffBorder(pn)
                    End If
                Next
            End If
        Catch ex As Exception
            updateSlideShow()
        End Try

    End Sub

    Private Sub turnOnBorder(ByRef panel As Panel)
        panel.BackColor = itemPanelColor
        Dim tempLabelPanel As MenuItemLabel = panel.Controls(0)
        tempLabelPanel.TurnOnBorder()
    End Sub

    Private Sub turnOffBorder(ByRef panel As Panel)
        panel.BackColor = despBgColor
        Dim tempLabelPanel As MenuItemLabel = panel.Controls(0)
        tempLabelPanel.TurnOffBorder()
    End Sub

    Public Sub setFrequency(ByVal picbox As Integer)
        Select Case picbox
            Case 1
                TimerDelay.Interval = frqOne
        End Select

        updateSlideShow()
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If PictureBox1.AnimatedImage Is Nothing Then
            PictureBox1.AnimatedFadeImage = Nothing
            PictureBox1.BackgroundImage = Nothing
        Else
            PictureBox1.AnimatedFadeImage = PictureBox1.AnimatedImage
            PictureBox1.BackgroundImage = PictureBox1.AnimatedImage
        End If

        If TypeOf Pics(imageNumber) Is String Then
            'video = New Video(Pics(imageNumber))
            'Dim duration As Integer = video.Duration * 1000
            'If (duration > TimerDelay.Interval) Then
            '    TimerDelay.Interval = duration
            'End If
            'video.Owner = PictureBox1
            'video.Size = New Size(Me.Height * 0.475, Me.Height * 0.475)
            'PictureBox1.Size = New Size(Me.Height * 0.475, Me.Height * 0.475)
            'video.Play()
            'AddHandler video.Ending, AddressOf BackLoopHandler

            
            'Change image number
            imageNumber += 1
            If imageNumber >= imageCount Then
                imageNumber = 0
            End If

            If TypeOf Pics(imageNumber) Is Image Then
                'Animate (Foreground) Image
                PictureBox1.AnimatedImage = Pics(imageNumber)
            End If

            'PictureBox1 = New AnimationControl
            'PictureBox1.Size = New Size(Me.Height * 0.475, Me.Height * 0.475)
            'PictureBox1.AnimationType = Project.AnimationTypes.RighTotLeft
            'PictureBox1.Location = New System.Drawing.Point(0, 0)
            'PictureBox1.MinimumSize = New System.Drawing.Size(100, 100)
            'PictureBox1.Transparent = True

        Else
            Timer1.Interval = frqOne
            Try
                'Change Background Image   
                PictureBox1.AnimatedFadeImage = Pics(imageNumber)
                PictureBox1.BackgroundImage = Pics(imageNumber)
                PictureBox1.BackColor = Color.Transparent

                'Change image number
                imageNumber += 1
                If imageNumber >= imageCount Then
                    imageNumber = 0
                End If

                If TypeOf Pics(imageNumber) Is Image Then
                    'Animate (Foreground) Image
                    PictureBox1.AnimatedImage = Pics(imageNumber)
                End If

                'Highlight Menu Item
                updatePanel(Not FullPictureBox.Visible)

                If animationOneList IsNot Nothing Then
                    'Search for Unique AnimationType
                    Dim animationSearch = animationOneList.IndexOf(SlideShowPics(imageNumber * 3 + 2))
                    If animationSearch <> -1 Then
                        PictureBox1.AnimationType = DirectCast([Enum].Parse(GetType(AnimationTypes), animationOneList(animationSearch + 1)), AnimationTypes)
                    End If
                End If

                PictureBox1.Animate(frqOne / 100)
            Catch exArg As ArgumentOutOfRangeException
                updateSlideShow()
            Catch ex As IndexOutOfRangeException
                updateSlideShow()
            Catch exNull As NullReferenceException
                updateSlideShow()
            End Try
        End If

        Timer1.Stop()
        TimerDelay.Start()
    End Sub

    Private Sub TimerDelay_Tick(sender As Object, e As EventArgs) Handles TimerDelay.Tick
        TimerDelay.Stop()
        Timer1.Start()
    End Sub

    Private Sub loadBinaryDeserialize()
        If File.Exists("AnimationListOne.bin") Then
            Using str As FileStream = File.OpenRead("AnimationListOne.bin")
                Dim bf As New BinaryFormatter()
                animationOneList = DirectCast(bf.Deserialize(str), ArrayList)
            End Using
        End If


        If File.Exists("Admin.bin") Then
            Using str As FileStream = File.OpenRead("Admin.bin")
                Dim bf As New BinaryFormatter()
                Dim adminList = DirectCast(bf.Deserialize(str), String())
                If adminList(0) IsNot Nothing Then
                    lblLogo.Image = Image.FromFile(adminList(0))
                End If
                weatherLocationCode = adminList(1)
                slideNum = adminList(3)
            End Using
        End If
    End Sub

    Public Sub changeLogo(image As Image)
        lblLogo.Image = image
    End Sub

    Sub Form_Paint(s As Object, e As PaintEventArgs) Handles Me.Paint
        If weatherPic IsNot Nothing Then
            Dim r As New Rectangle(Me.Width * 0.8, Me.Height * (-0.08), Me.Width * 0.2, Me.Width * 0.2)
            e.Graphics.DrawImage(weatherPic, r)
        End If
    End Sub

    Sub BackLoopHandler(sender As Object, args As EventArgs)
        If (Not video.Disposed) Then
            video.Stop()
            'video.Dispose()
        End If
    End Sub
End Class
