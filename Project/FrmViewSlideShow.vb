Imports System.IO
Imports System.Drawing.Drawing2D
Imports System.Reflection
Imports Microsoft.DirectX.AudioVideoPlayback

Public Class FrmViewSlideShow

    Private SlideShowPicsList As New ArrayList
    Private SlideShowPics As New ArrayList
    Private Pic(100) As Object
    Private imageNumber As Integer = 0
    Private video As Video

    'Stores the frequency for the slide shows
    Public Shared slideShowFreq As Integer

    Private Sub FrmViewSlideShow_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        loadSlideShowPic()
        loadTimerFreq()

        cmbAnimationType.DataSource = System.Enum.GetValues(GetType(AnimationTypes))
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If SlideShowPicsList.Count <> 0 Then
            If PictureBox1.AnimatedImage Is Nothing Then
                PictureBox1.AnimatedFadeImage = Nothing
                PictureBox1.BackgroundImage = Nothing
            Else
                PictureBox1.AnimatedFadeImage = PictureBox1.AnimatedImage
                PictureBox1.BackgroundImage = PictureBox1.AnimatedImage
            End If

            If TypeOf Pic(imageNumber) Is String Then
                PictureBox1.BackgroundImage = Nothing
                video = New Video(Pic(imageNumber))
                Dim duration As Integer = video.Duration * 1000
                If (duration > Timer1.Interval) Then
                    Timer1.Interval = duration
                End If
                video.Owner = PictureBox1
                video.Size = New Size(366, 295)
                PictureBox1.Size = New Size(366, 295)
                video.Play()
                AddHandler video.Ending, AddressOf BackLoopHandler
            Else
                Timer1.Interval = slideShowFreq
                PictureBox1.Size = New Size(366, 295)
                If (video IsNot Nothing) Then
                    video.Dispose()
                End If

                PictureBox1.AnimatedImage = Pic(imageNumber)
                PictureBox1.Animate(30)
            End If


            PictureBox1.BackColor = Color.Transparent

            imageNumber = imageNumber + 1
            If imageNumber = SlideShowPicsList.Count Then
                imageNumber = 0
            End If

        End If

        Timer1.Stop()
        TimerDelay.Start()
    End Sub

    Public Sub loadSlideShowPic()
        Timer1.Stop()
        Me.Cursor = Cursors.WaitCursor
        If (Not bgw.IsBusy) Then
            bgw.RunWorkerAsync()
        End If
    End Sub

    Private Sub bgw_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgw.DoWork
        imageNumber = 0
        SlideShowPicsList.Clear()
        SlideShowPics = DataLayer.GetSlideShowItems(1)
        For i = 1 To SlideShowPics.Count Step 3
            SlideShowPicsList.Add(SlideShowPics(i))
        Next

        For i = 1 To SlideShowPics.Count Step 3
            If (SlideShowPics(i).ToString.Contains(".avi")) Then
                Pic((i - 1) / 3) = Directory.GetCurrentDirectory() & "\images\" & SlideShowPics(i).ToString()
            Else
                Pic((i - 1) / 3) = resizeImage(Image.FromFile(Directory.GetCurrentDirectory() & "\images\" & SlideShowPics(i).ToString()))
            End If
        Next
    End Sub

    Private Sub bgw_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgw.RunWorkerCompleted
        Me.Cursor = Cursors.Arrow
        Timer1.Start()
    End Sub

    Sub BackLoopHandler(sender As Object, args As EventArgs)
        If (Not video.Disposed) Then
            video.Stop()
            'video.Dispose()
        End If
    End Sub

    Public Sub loadTimerFreq()
        slideShowFreq = Digital_Board.mainFrm.digital.frqOne / 1000 * 2
    End Sub

    Private Function resizeImage(ByVal image As Image) As Image
        Dim thumbBit = New Bitmap(PictureBox1.Width, PictureBox1.Height)
        Dim thumbnail = Graphics.FromImage(thumbBit)
        thumbnail.CompositingQuality = CompositingQuality.HighSpeed
        thumbnail.SmoothingMode = SmoothingMode.HighSpeed
        thumbnail.InterpolationMode = InterpolationMode.HighQualityBicubic

        Dim imageRectangel = New Rectangle(0, 0, PictureBox1.Width, PictureBox1.Height)
        thumbnail.DrawImage(image, imageRectangel)

        Return thumbBit
    End Function

    Private Sub TimerDelay_Tick(sender As Object, e As EventArgs) Handles TimerDelay.Tick
        TimerDelay.Stop()
        Timer1.Start()
    End Sub

    Public Function btnAdd_Click(sender As Object, e As EventArgs) As AnimationTypes Handles btnAdd.Click
        Digital_Board.mainFrm.tabOne.AddAnimation(cmbAnimationType.SelectedItem)
        Digital_Board.mainFrm.digital.updateSlideShow()
    End Function

    Private Sub cmbAnimationType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbAnimationType.SelectedIndexChanged
        PictureBox1.AnimationType = cmbAnimationType.SelectedItem
        Me.ActiveControl = btnAdd
    End Sub

    Private Sub trkOne_Scroll(sender As Object, e As EventArgs) Handles trkOne.MouseLeave
        slideShowFreq = trkOne.Value / 2 * 1000
        TimerDelay.Interval = slideShowFreq
        Digital_Board.mainFrm.digital.frqOne = slideShowFreq
        Digital_Board.mainFrm.digital.setFrequency(1)
    End Sub

    Public Sub changeAnimaSelected(ByVal animaIn As String)
        For Each anima As AnimationTypes In cmbAnimationType.Items
            If anima.ToString = animaIn.ToString() Then
                cmbAnimationType.SelectedItem = anima
            End If
        Next
    End Sub
End Class