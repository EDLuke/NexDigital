﻿Imports System.IO
Imports System.Drawing.Drawing2D
Imports System.Reflection

Public Class FrmViewSlideShow

    Private SlideShowPicsList As New ArrayList
    Private SlideShowPics As New ArrayList
    Private Pic(100) As Image
    Private imageNumber As Integer = 0

    'Stores the frequency for the slide shows
    Public Shared slideShowFreq As Integer

    Private Sub FrmViewSlideShow_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        loadSlideShowPic()
        loadTimerFreq()

        cmbAnimationType.DataSource = System.Enum.GetValues(GetType(AnimationTypes))
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If SlideShowPicsList.Count <> 0 Then

            PictureBox1.AnimatedFadeImage = Pic(imageNumber)
            PictureBox1.BackgroundImage = Pic(imageNumber)
            PictureBox1.BackColor = Color.Transparent

            imageNumber = imageNumber + 1
            If imageNumber = SlideShowPicsList.Count Then
                imageNumber = 0
            End If

            PictureBox1.AnimatedImage = Pic(imageNumber)
        End If

        Timer1.Stop()
        TimerDelay.Start()
    End Sub

    Public Sub loadSlideShowPic()
        Timer1.Stop()
        Me.Cursor = Cursors.WaitCursor
        bgw.RunWorkerAsync()
    End Sub

    Private Sub bgw_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgw.DoWork
        imageNumber = 0
        SlideShowPicsList.Clear()
        SlideShowPics = DataLayer.GetSlideShowItems()
        For i = 1 To SlideShowPics.Count Step 3
            SlideShowPicsList.Add(SlideShowPics(i))
        Next

        For i = 1 To SlideShowPics.Count Step 3
            Pic((i - 1) / 3) = resizeImage(Image.FromFile(Directory.GetCurrentDirectory() & "\images\" & SlideShowPics(i).ToString()))
        Next
    End Sub

    Private Sub bgw_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgw.RunWorkerCompleted
        PictureBox1.AnimatedFadeImage = Pic(imageNumber)
        PictureBox1.BackgroundImage = Pic(imageNumber)
        PictureBox1.BackColor = Color.Transparent

        Me.Cursor = Cursors.Arrow
        Timer1.Start()
    End Sub

    Public Sub loadTimerFreq()
        slideShowFreq = MainMenu.digital.frqOne / 1000 * 2
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
        PictureBox1.Animate(30)
        TimerDelay.Stop()
        Timer1.Start()
    End Sub

    Public Function btnAdd_Click(sender As Object, e As EventArgs) As AnimationTypes Handles btnAdd.Click
        MainMenu.tabOne.AddAnimation(cmbAnimationType.SelectedItem)
        MainMenu.digital.updateSlideShow()
    End Function

    Private Sub cmbAnimationType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbAnimationType.SelectedIndexChanged
        PictureBox1.AnimationType = cmbAnimationType.SelectedItem
        Me.ActiveControl = btnAdd
    End Sub

    Private Sub trkOne_Scroll(sender As Object, e As EventArgs) Handles trkOne.MouseLeave
        slideShowFreq = trkOne.Value / 2 * 1000
        TimerDelay.Interval = slideShowFreq
        MainMenu.digital.frqOne = slideShowFreq
        MainMenu.digital.setFrequency(1)
    End Sub

    Public Sub changeAnimaSelected(ByVal animaIn As String)
        For Each anima As AnimationTypes In cmbAnimationType.Items
            If anima.ToString = animaIn.ToString() Then
                cmbAnimationType.SelectedItem = anima
            End If
        Next
    End Sub
End Class