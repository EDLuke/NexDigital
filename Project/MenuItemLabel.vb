Public Class MenuItemLabel
    Inherits Label

#Region "Field"
    Public Shared BorderColor As Color

    Private _Money As Decimal = 0
    Private _Title As String = ""
    Private _Desp As String = ""
    Private _ID As Decimal = 0
    Private _TitleFont As Font = Me.Font
    Private _PriceFont As Font = Me.Font
    Private _DespFont As Font = Me.Font
    Private _TitleColor As Color = Me.ForeColor
    Private _PriceColor As Color = Me.ForeColor
    Private _DespColor As Color = Me.ForeColor
    Private BorderOn As Boolean = False

    Property Money() As Decimal
        Get
            Return _Money
        End Get
        Set(ByVal value As Decimal)
            _Money = value
            Me.Invalidate()
        End Set
    End Property

    Property Title() As String
        Get
            Return _Title
        End Get
        Set(ByVal value As String)
            _Title = value
            Me.Invalidate()
        End Set
    End Property

    Property Desp() As String
        Get
            Return _Desp
        End Get
        Set(value As String)
            _Desp = value
            Me.Invalidate()
        End Set
    End Property

    Property ID() As Decimal
        Get
            Return _ID
        End Get
        Set(ByVal value As Decimal)
            _ID = value
            Me.Invalidate()
        End Set
    End Property

    Property TitleFont() As Font
        Get
            Return _TitleFont
        End Get

        Set(value As Font)
            _TitleFont = value
            Me.Invalidate()
        End Set
    End Property

    Property PriceFont() As Font
        Get
            Return _PriceFont
        End Get

        Set(value As Font)
            _PriceFont = value
            Me.Invalidate()
        End Set
    End Property

    Property DespFont() As Font
        Get
            Return _DespFont
        End Get

        Set(value As Font)
            _DespFont = value
            Me.Invalidate()
        End Set
    End Property

    Property TitleColor() As Color
        Get
            Return _TitleColor
        End Get

        Set(value As Color)
            _TitleColor = value
            Me.Invalidate()
        End Set
    End Property

    Property PriceColor() As Color
        Get
            Return _PriceColor
        End Get

        Set(value As Color)
            _PriceColor = value
            Me.Invalidate()
        End Set
    End Property

    Property DespColor() As Color
        Get
            Return _DespColor
        End Get

        Set(value As Color)
            _DespColor = value
            Me.Invalidate()
        End Set
    End Property

#End Region

#Region "Method"
    Public Sub TurnOnBorder()
        BorderOn = True
        Me.Invalidate()
    End Sub

    Public Sub TurnOffBorder()
        BorderOn = False
        Me.Invalidate()
    End Sub
#End Region


    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        MyBase.OnPaint(e)

        Dim moneyText As String = "$" & String.Format("{0:N2}", _Money)
        Dim titleText As String = _Title
        Dim despText As String = _Desp
        Dim rectOne As Rectangle
        Dim rectTwo As Rectangle
        Dim rectOneBorder As Rectangle
        Dim rectTwoBorder As Rectangle

        Dim titleWidth As Integer = TextRenderer.MeasureText(e.Graphics, titleText, TitleFont).Width
        Dim titleHeight As Integer = TextRenderer.MeasureText(e.Graphics, titleText, TitleFont).Height + 5
        Dim moneyWidth As Integer = TextRenderer.MeasureText(e.Graphics, moneyText, PriceFont).Width + 20
        Dim despWidth As Integer = TextRenderer.MeasureText(e.Graphics, despText, DespFont).Width
        Dim despHeight As Integer = TextRenderer.MeasureText(e.Graphics, despText, DespFont).Height

        Dim titleHeightRatio = titleWidth / Me.ClientSize.Width + 1
        Dim despHeightRatio = despWidth / Me.ClientSize.Width + 1

        rectOne = New Rectangle(0, 0, Me.ClientSize.Width - moneyWidth, titleHeight)
        rectTwo = New Rectangle(0, rectOne.Height, Me.ClientSize.Width, despHeight * despHeightRatio)
        rectOneBorder = New Rectangle(0, 0, Me.ClientSize.Width - moneyWidth - 20, titleHeight)
        rectTwoBorder = New Rectangle(0, rectOne.Height, Me.ClientSize.Width - 20, despHeight * despHeightRatio + 12)

        Me.Height = rectOne.Height + rectTwo.Height + 13

        TextRenderer.DrawText(e.Graphics, titleText, TitleFont, rectOne, TitleColor, TextFormatFlags.HorizontalCenter Or TextFormatFlags.VerticalCenter Or TextFormatFlags.WordBreak)
        TextRenderer.DrawText(e.Graphics, moneyText, PriceFont, New Point(Me.ClientSize.Width - (moneyWidth), 0), PriceColor)
        TextRenderer.DrawText(e.Graphics, despText, DespFont, rectTwo, DespColor, TextFormatFlags.WordBreak Or TextFormatFlags.Bottom)

        If (BorderOn) Then
            Dim rectThree = Rectangle.Union(rectOneBorder, rectTwoBorder)
            Dim pen As New Pen(BorderColor, 7)
            e.Graphics.DrawRectangle(pen, Rectangle.Round(rectThree))
        End If

    End Sub

End Class