﻿Imports System.IO
Imports System.Drawing.Drawing2D
Imports System.Reflection
Imports System.Runtime.Serialization.Formatters.Binary

Public Class FrmViewSlideShowTwo

    Private SlideShowPicsList As New ArrayList
    Private SlideShowPics As New ArrayList
    Private Pic(100) As Object
    Private imageNumber As Integer = 0

    'Stores the animation for the first slideshow
    Public Shared animation As AnimationTypes

    'Stores the frequency for the slide shows
    Public Shared slideShowFreq As Integer

    Private Sub FrmViewSlideShow_Shown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown
        BinaryDeserialize()
        loadSlideShowPic()
        loadTimerFreq()

        cmbAnimationType.DataSource = System.Enum.GetValues(GetType(AnimationTypes))
    End Sub

    'MouseEnter Handler for the three Controls
    Private Sub cmbAnimationType_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.MouseEnter
        cmbAnimationType.Select()
    End Sub

    Private Sub cmbAnimationType_MouseWheel(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseWheel
        If (e.Delta > 0) Then
            If cmbAnimationType.SelectedIndex - 1 >= 0 Then
                cmbAnimationType.SelectedIndex -= 1
            End If
        Else
            If cmbAnimationType.SelectedIndex + 1 < cmbAnimationType.Items.Count Then
                cmbAnimationType.SelectedIndex += 1
            End If
        End If
    End Sub

    Public Sub StopTimer()
        Timer1.Stop()
    End Sub

    Public Sub StartTimer()
        Timer1.Start()
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

            Timer1.Interval = slideShowFreq
            PictureBox1.AnimatedImage = Pic(imageNumber)
            PictureBox1.Animate(30)
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
            Pic((i - 1) / 3) = resizeImage(Image.FromFile(Directory.GetCurrentDirectory() & "\images\" & SlideShowPics(i).ToString()))
        Next
    End Sub

    Private Sub bgw_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgw.RunWorkerCompleted
        Me.Cursor = Cursors.Arrow
        Timer1.Start()
    End Sub

    Public Sub loadTimerFreq()
        slideShowFreq = Digital_Board.digital.frqTwo / 1000 * 2
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

    Public Function btnSetAnima_Click(sender As Object, e As EventArgs) As AnimationTypes Handles btnSetAnima.Click
        BinarySerialize()
        Digital_Board.digital.updateSlideShow()
    End Function

    Private Sub cmbAnimationType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbAnimationType.SelectedIndexChanged
        PictureBox1.AnimationType = cmbAnimationType.SelectedItem
        Me.ActiveControl = btnSetAnima
    End Sub

    Private Sub trkOne_Scroll(sender As Object, e As EventArgs) Handles trkOne.Scroll
        slideShowFreq = trkOne.Value / 2 * 1000
        TimerDelay.Interval = slideShowFreq
        Digital_Board.digital.frqTwo = slideShowFreq
        Digital_Board.digital.setFrequency(2)
    End Sub

    Public Sub changeAnimaSelected(ByVal animaIn As String)
        For Each anima As AnimationTypes In cmbAnimationType.Items
            If anima.ToString = animaIn.ToString() Then
                cmbAnimationType.SelectedItem = anima
            End If
        Next
    End Sub

    Private Shared Sub BinarySerialize()
        Using str As FileStream = File.Create("AnimationTwo.bin")
            Dim bf As New BinaryFormatter()
            bf.Serialize(str, animation)
        End Using
    End Sub

    ' Deserialize an Animation object from a binary file.
    Private Shared Sub BinaryDeserialize()
        If (File.Exists("AnimationTwo.bin")) Then
            Using str As FileStream = File.OpenRead("AnimationTwo.bin")
                Dim bf As New BinaryFormatter()
                animation = DirectCast(bf.Deserialize(str), AnimationTypes)
            End Using
        End If
    End Sub
End Class