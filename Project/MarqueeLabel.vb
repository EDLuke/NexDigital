Public Class MarqueeLabel
    Inherits Control

#Region "Field"
    Private movePercent = 0
    Private newsListLength = 0
    Private newsList = ""
    Private _NewsColor As Color = Me.ForeColor

    Property NewsColor() As Color
        Get
            Return _NewsColor
        End Get

        Set(value As Color)
            _NewsColor = value
            Me.Invalidate()
        End Set
    End Property
#End Region

#Region "Constructor"
    Public Sub New()
        SetStyle(ControlStyles.SupportsTransparentBackColor, True)
        SetStyle(ControlStyles.DoubleBuffer, True)
        SetStyle(ControlStyles.AllPaintingInWmPaint, True)
    End Sub
#End Region
   
#Region "Method"

    Public Sub loadNews(ByVal newsArray As ArrayList)
        newsList = "          "
        For i = 0 To newsArray.Count - 1
            newsList = newsList & newsArray(i) & "    " & Chr(149) & "    "
        Next
        newsList = newsList.Substring(0, newsList.Length - 5)
        newsListLength = TextRenderer.MeasureText(newsList, New Font("Microsoft Sans Serif", 18, FontStyle.Bold)).Width
    End Sub

    Public Function tick() As Boolean
        If (movePercent >= newsListLength - Me.Width * 0.75) Then
            movePercent = 0
            Return False
        End If

        movePercent += newsListLength * 0.00015
        Invalidate()

        Return True
    End Function

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        TextRenderer.DrawText(e.Graphics, newsList, New Font("Microsoft Sans Serif", 18, FontStyle.Bold), New Point(0 - movePercent, Me.Height * 0.25), NewsColor)
    End Sub
#End Region
End Class
