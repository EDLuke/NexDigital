Imports System.IO
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.Net
Imports System.ComponentModel
Imports System.Threading
Imports System.Reflection

Public Class FrmDigitalBBTwo
    Public weatherLocationCode As String = "2396503"
    Public internet As Boolean
    Public frqOne As Integer = 5000
    Public frqTwo As Integer = 5000

    Private imageNumber, imageNumber2, imageCount, imageCount2, newsNumber, newsCount As Integer
    Private Pics(200), Pics2(100) As Object
    Private weatherPic As Image
    Private Full(100), slideFullSeparte, fullAnimate As Boolean
    Private slideNum As String
    Private despFontArray(2) As Font
    Private despBgColor, itemPanelColor, itemBorderColor, despColorArray(2) As Color
    Private newsArrayList, animationOneList, animationTwoList, SlideShowPics, SlideShowPics2, SlideShowFull As ArrayList
    Private w_Thread As CallBackThread
    Private n_Thread As CallBackThread
    Private i_Thread As CallBackThread
    Private n As New clsNews
    Private w As New clsWeather(weatherLocationCode, "f")

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

    Private Sub bgwLoad_RunWorkerComplete(sender As Object, e As RunWorkerCompletedEventArgs) Handles bgwLoad.RunWorkerCompleted
        'Check for the option to have only one slideshow or multiple
        checkFullSeparte()
        fullAnimate = False
    End Sub

    Public Sub checkFullSeparte()
        If slideNum = "One" Then
            slideFullSeparte = True
        ElseIf slideNum = "Two" Then
            slideFullSeparte = False
        End If

        Try
            If slideFullSeparte Then
                PictureBox1.Visible = False
                PictureBox2.Visible = False
                FullPictureBox.Visible = True
            Else
                PictureBox1.Visible = True
                PictureBox2.Visible = True
                FullPictureBox.Visible = False
            End If
        Catch ex As InvalidOperationException

        End Try
    End Sub

    Private Sub loadFrq()
        frqOne = TimerDelay.Interval
        frqTwo = TimerSecondDelay.Interval
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
                If (Me.IsHandleCreated) Then
                    w_Thread.UpdateUI(internet.ToString)
                    Threading.Thread.Sleep(10000)
                End If

            End While
        Catch ex As Exception
            loadNews()
        End Try
    End Sub

    Private Sub newsThread()
        Try
            While True
                If (Me.IsHandleCreated) Then
                    n_Thread.UpdateUI(internet.ToString)
                    Threading.Thread.Sleep(50)
                End If

            End While
        Catch ex As Exception

        End Try
    End Sub

    Public Sub stretchScreen()
        Me.SuspendLayout()

        'Full Screen
        Me.WindowState = FormWindowState.Maximized

        'Scretch Everything
        PictureBox1.Size = New Size(Me.Width * 0.65, Me.Height * 0.48)
        PictureBox2.Size = New Size(Me.Width * 0.65, Me.Height * 0.48)
        FullPictureBox.Size = New Size(Me.Width * 0.65, Me.Height * 0.96)

        flowPanel.Width = Me.Width * 0.35
        flowPanel.Height = Me.Height * 0.7

        lblNews.Width = Me.Width
        lblNews.Height = Me.Height * 0.05

        lblLogo.AutoSize = False
        lblLogo.Width = Me.Height * 0.3
        lblLogo.Height = Me.Height * 0.3


        'Change Locations as well
        PictureBox1.Location = New Point(0, 0)
        PictureBox2.Location = New Point(0, Me.Height * 0.48)
        FullPictureBox.Location = New Point(0, 0)
        flowPanel.Location = New Point(Me.Width * 0.65, Me.Height * 0.3)
        lblNews.Location = New Point(0, Me.Height * 0.95)
        lblLogo.Location = New Point(Me.Width * 0.67, 0)
        lblNow.Location = New Point(Me.Width * 0.85, Me.Height * 0.2)

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
            If w.now() <> "" Then
                lblNow.Text = w.now
            End If
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
        Array.Clear(Pics2, 0, Pics2.Length)
        Array.Clear(Full, 0, Full.Length)

        SlideShowPics = DataLayer.GetSlideShowItems(1)
        SlideShowPics2 = DataLayer.GetSlideShowItems(2)
        SlideShowFull = DataLayer.GetSlideShowFull()
        imageCount = SlideShowPics.Count / 3
        imageCount2 = SlideShowPics2.Count / 3

        For i = 1 To SlideShowPics.Count Step 3
            Pics((i - 1) / 3) = resizeImage(Image.FromFile(Directory.GetCurrentDirectory() & "\images\" & SlideShowPics(i).ToString()))
        Next

        For i = 1 To SlideShowPics2.Count Step 3
            Pics2((i - 1) / 3) = resizeImage(Image.FromFile(Directory.GetCurrentDirectory() & "\images\" & SlideShowPics2(i).ToString()))
            Full((i - 1) / 3) = CBool(SlideShowFull(i).ToString())
        Next

        imageNumber = 0
        imageNumber2 = 0

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
        Digital_Board.setup.loadDefaults()

        Dim despList = DataLayer.GetMenuItems(1)

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

        Timer1.Start()
    End Sub

    Private Sub updatePanel(ByVal goFull As Boolean)
        Try
            Dim SlideShowPics As ArrayList = DataLayer.GetSlideShowItems(1)

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
            Case 2
                TimerSecondDelay.Interval = frqTwo
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

        If FullPictureBox.AnimatedImage Is Nothing Then
            FullPictureBox.AnimatedFadeImage = Nothing
            FullPictureBox.BackgroundImage = Nothing
        Else
            FullPictureBox.AnimatedFadeImage = FullPictureBox.AnimatedImage
            FullPictureBox.BackgroundImage = FullPictureBox.AnimatedImage
        End If

        
        Timer1.Interval = frqOne
        Try
            If Not slideFullSeparte Then
                PictureBox1.Visible = True
                PictureBox2.Visible = True
                FullPictureBox.Visible = False
                If (Not Full(imageNumber2)) Then
                    'Change Background Image
                    PictureBox1.AnimatedFadeImage = Pics(imageNumber)
                    PictureBox1.BackgroundImage = Pics(imageNumber)
                    PictureBox1.BackColor = Color.Transparent
                End If

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

                FullPictureBox.Visible = False
                PictureBox1.Animate(frqOne / 100)
            Else
                FullPictureBox.AnimatedImage = Pics(imageNumber)
                FullPictureBox.Animate(30)

                FullPictureBox.BackColor = Color.Transparent

                'Change image number
                imageNumber += 1
                If imageNumber >= imageCount Then
                    imageNumber = 0
                End If

                'Highlight Menu Item
                updatePanel(FullPictureBox.Visible)
            End If
        Catch exArg As ArgumentOutOfRangeException
            updateSlideShow()
        Catch ex As IndexOutOfRangeException
            updateSlideShow()
        Catch exNull As NullReferenceException
            updateSlideShow()
        End Try

        Timer1.Stop()
        TimerDelay.Start()
    End Sub

    Private Sub TimerDelay_Tick(sender As Object, e As EventArgs) Handles TimerDelay.Tick
        If Not slideFullSeparte Then
            separateSlideShowAnimate()
        Else
            fullSlideShowAnimate()
        End If
        
    End Sub

    Private Sub fullSlideShowAnimate()
        If FullPictureBox.AnimatedImage Is Nothing Then
            FullPictureBox.AnimatedFadeImage = Nothing
            FullPictureBox.BackgroundImage = Nothing
        Else
            FullPictureBox.AnimatedFadeImage = FullPictureBox.AnimatedImage
            FullPictureBox.BackgroundImage = FullPictureBox.AnimatedImage
        End If

        TimerDelay.Interval = frqOne
        FullPictureBox.AnimatedImage = Pics(imageNumber)

        imageNumber += 1
        If imageNumber >= imageCount Then
            imageNumber = 0
        End If

        'Un-highlight when Full Image is shown
        updatePanel(FullPictureBox.Visible)

        If animationOneList IsNot Nothing And SlideShowPics.Count <> 0 Then
            'Search for Unique AnimationType
            Dim animationSearch = animationOneList.IndexOf(SlideShowPics(imageNumber * 3 + 2))
            If animationSearch <> -1 Then
                FullPictureBox.AnimationType = DirectCast([Enum].Parse(GetType(AnimationTypes), animationOneList(animationSearch + 1)), AnimationTypes)
            End If
        End If

        fullAnimate = True
        FullPictureBox.Animate(frqOne / 100)

        TimerDelay.Stop()
        TimerSecondDelay.Start()
    End Sub

    Private Sub separateSlideShowAnimate()
        If PictureBox2.AnimatedImage Is Nothing Then
            PictureBox2.AnimatedFadeImage = Nothing
            PictureBox2.BackgroundImage = Nothing
        Else
            PictureBox2.AnimatedFadeImage = PictureBox2.AnimatedImage
            PictureBox2.BackgroundImage = PictureBox2.AnimatedImage
        End If


        TimerDelay.Interval = frqTwo

        If (Full(imageNumber2)) Then
            Dim bmp As New Bitmap(CInt(Me.Width * 0.65), CInt(Me.Height * 0.96))
            Using g As Graphics = Graphics.FromImage(bmp)
                g.DrawImage(PictureBox1.AnimatedImage, New Point(0, 0))
                g.DrawImage(PictureBox2.BackgroundImage, New Point(0, Me.Height * 0.48))
            End Using

            FullPictureBox.AnimatedFadeImage = bmp
            FullPictureBox.BackgroundImage = bmp
            FullPictureBox.BackColor = Color.Transparent
            FullPictureBox.Visible = True

            'Change background image
            Dim bmpBottom As New Bitmap(CInt(Me.Width * 0.65), CInt(Me.Height * 0.48))
            Using gBottom As Graphics = Graphics.FromImage(bmpBottom)
                Dim fm_rect As New Rectangle(0, FullPictureBox.AnimatedImage.Height / 2, FullPictureBox.AnimatedImage.Width, FullPictureBox.AnimatedImage.Height / 2)
                Dim to_rect As New Rectangle(0, 0, Me.Width * 0.65, Me.Height * 0.48)

                gBottom.DrawImage(FullPictureBox.AnimatedImage, to_rect, fm_rect, GraphicsUnit.Pixel)
            End Using
            Dim bmpTop As New Bitmap(CInt(Me.Width * 0.65), CInt(Me.Height * 0.48))
            Using gTop As Graphics = Graphics.FromImage(bmpTop)
                Dim fm_rect As New Rectangle(0, 0, FullPictureBox.AnimatedImage.Width, FullPictureBox.AnimatedImage.Height / 2)
                Dim to_rect As New Rectangle(0, 0, Me.Width * 0.65, Me.Height * 0.48)

                gTop.DrawImage(FullPictureBox.AnimatedImage, to_rect, fm_rect, GraphicsUnit.Pixel)
            End Using

            PictureBox1.AnimatedImage = bmpTop
            PictureBox1.AnimatedFadeImage = bmpTop
            PictureBox1.BackgroundImage = bmpTop
            PictureBox1.BackColor = Color.Transparent

            PictureBox2.AnimatedImage = bmpBottom
            PictureBox2.AnimatedFadeImage = bmpBottom
            PictureBox2.BackgroundImage = bmpBottom
            PictureBox2.BackColor = Color.Transparent

            'highlight when Full Image is shown
            updatePanel(FullPictureBox.Visible)

            imageNumber2 += 1
            If imageNumber2 >= imageCount2 Then
                imageNumber2 = 0
            End If

            Try
                If animationTwoList IsNot Nothing And SlideShowPics2 IsNot Nothing And SlideShowPics2.Count <> 0 Then
                    'Search for Unique AnimationType
                    Dim animationSearch = animationTwoList.IndexOf(SlideShowPics2(imageNumber2 * 3 + 2))
                    If animationSearch <> -1 Then
                        FullPictureBox.AnimationType = DirectCast([Enum].Parse(GetType(AnimationTypes), animationTwoList(animationSearch + 1)), AnimationTypes)
                    End If
                End If
            Catch ex As Exception

            End Try
            FullPictureBox.Animate(frqOne / 100)
        Else
            FullPictureBox.Visible = False

            Try
                If animationTwoList IsNot Nothing And SlideShowPics2 IsNot Nothing And SlideShowPics2.Count <> 0 Then
                    'Search for Unique AnimationType
                    Dim animationSearch = animationTwoList.IndexOf(SlideShowPics2(imageNumber2 * 3 + 2))
                    If animationSearch <> -1 Then
                        PictureBox2.AnimationType = animationTwoList(animationSearch + 1)
                    End If
                End If
            Catch ex As Exception

            End Try

            imageNumber2 += 1
            If imageNumber2 >= imageCount2 Then
                imageNumber2 = 0
            End If

            If TypeOf Pics2(imageNumber2) Is Image Then
                'Animate (Foreground) Image
                PictureBox2.AnimatedImage = Pics2(imageNumber2)
            End If

            PictureBox2.Animate(frqTwo / 100)
        End If
    End Sub

    Public Sub loadBinaryDeserialize()
        If File.Exists("AdnimationListOne.bin") Then
            Using str As FileStream = File.OpenRead("AnimationListOne.bin")
                Dim bf As New BinaryFormatter()
                animationOneList = DirectCast(bf.Deserialize(str), ArrayList)
            End Using
        End If

        If File.Exists("AdnimationListTwo.bin") Then
            Using str As FileStream = File.OpenRead("AnimationListTwo.bin")
                Dim bf As New BinaryFormatter()
                animationTwoList = DirectCast(bf.Deserialize(str), ArrayList)
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
            Dim r As New Rectangle(Me.Width * 0.73, Me.Height * (-0.08), Me.Width * 0.2, Me.Width * 0.2)
            e.Graphics.DrawImage(weatherPic, r)
        End If
    End Sub

    Private Sub TimerSecondDelay_Tick(sender As Object, e As EventArgs) Handles TimerSecondDelay.Tick
        fullAnimate = False
        TimerSecondDelay.Stop()
        Timer1.Start()
    End Sub
End Class
