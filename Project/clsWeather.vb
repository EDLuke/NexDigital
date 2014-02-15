Imports System.Web.RegularExpressions
Imports System.Text.RegularExpressions
Imports System.IO
Imports System.Drawing.Imaging
Imports System.Net
Imports System.Reflection

Public Class clsWeather
    Dim FullTextXML As String
    Dim PartTextXML As String

    Private Function GetStr(ByVal str As String) As String
        Try
            Dim index1 As Integer
            Dim index2 As Integer
            index1 = (str.IndexOf("<![CDATA[")) + Len("<![CDATA[")
            index2 = (str.IndexOf("]]>"))
            ' MsgBox(index1, MsgBoxStyle.Critical, index2)
            Dim str1 As String = str.Substring(index1, index2 - index1)
            Return str1
        Catch ex As Exception
            Return "Error"
        End Try
    End Function

    Private Function GetNow(ByVal str As String) As String
        Try
            Dim index1 As Integer
            Dim index2 As Integer
            index1 = (str.IndexOf("Current Condition")) + Len("Current Condition")
            index2 = (str.IndexOf("Forecast"))
            Dim str1 As String = str.Substring(index1, index2 - index1)
            Return str1
        Catch ex As Exception
            Return "Error"
        End Try
    End Function

    Function now() As String
        Dim str = GetNow(PartTextXML)
        If str = "Error" Then
            Return ""
        End If
        Dim index1 As Integer
        Dim index2 As Integer
        index1 = (str.IndexOf("</b><br />")) + Len("</b><br />")
        index2 = (str.IndexOf("<BR />"))
        Dim str1 As String = str.Substring(index1, index2 - index1).Trim()
        Return str1
    End Function

    Function temprature() As String
        Dim str = now()
        Dim index = str.IndexOf(",")
        Return str.Substring(index + 2)
    End Function

    Function getXML() As String
        Return FullTextXML
    End Function

    Private Function GetWeather(ByVal str As String) As String
        Dim index1 As Integer
        Dim index2 As Integer
        Dim NewString As String
        index1 = (str.IndexOf("<yweather:forecast")) + Len("<yweather:forecast")
        NewString = str.Substring(index1)
        index2 = NewString.IndexOf("/>")
        Dim str1 As String = NewString.Substring(0, index2)
        Return str1
    End Function

    Sub New(ByVal p As String, ByVal U As String)
        Try
            If My.Computer.Network.IsAvailable = True Then
                Dim rq As HttpWebRequest = WebRequest.Create("http://xml.weather.yahoo.com/forecastrss?w=" & p & "&u=" & U)
                rq.KeepAlive = False
                rq.Timeout = 3000

                Dim response = rq.GetResponse

                Dim reader = response.GetResponseStream

                Dim doc As New System.Xml.XmlDocument()

                'Load data   
                doc.Load(reader)
                Dim str = doc.OuterXml
                FullTextXML = doc.OuterXml
                PartTextXML = GetStr(str)
                response.Close()
                reader.Close()
            End If
        Catch ex As Exception

        End Try

    End Sub

    Function GetImage(ByVal str As String) As String
        Try
            Dim index1 As Integer
            Dim index2 As Integer
            index1 = (str.IndexOf("<img src=")) + Len("<img src=")
            index2 = (str.IndexOf("/><br />"))

            Dim str1 As String = str.Substring(index1, index2 - index1)
            Return str1
        Catch ex As Exception
            Return "Error"
        End Try
    End Function

    Function image() As Image
        Dim nowStr = now()
        Dim index = nowStr.IndexOf(",")
        Dim am = True
        nowStr = nowStr.Substring(0, index)
        If Date.Now.Hour >= 18 Then
            am = False
        End If

        Select Case nowStr
            Case "Partly Cloudy"
                If am Then
                    Return image.FromFile(Directory.GetCurrentDirectory() & "\images\weather\Partly Cloudy.png")
                Else
                    Return image.FromFile(Directory.GetCurrentDirectory() & "\images\weather\Cloudy Night.png")
                End If
            Case "Fair"
                If am Then
                    Return image.FromFile(Directory.GetCurrentDirectory() & "\images\weather\Sunny.png")
                Else
                    Return image.FromFile(Directory.GetCurrentDirectory() & "\images\weather\Moon.png")
                End If
            Case "Cloudy"
            Case "Mostly Cloudy"
                If am Then
                    Return image.FromFile(Directory.GetCurrentDirectory() & "\images\weather\Cloudy.png")
                Else
                    Return image.FromFile(Directory.GetCurrentDirectory() & "\images\weather\Cloudy Night.png")
                End If
            Case "Drizzle"
            Case "Light Rain"
                Return image.FromFile(Directory.GetCurrentDirectory() & "\images\weather\Drizzle.png")
            Case "Snow"
                Return image.FromFile(Directory.GetCurrentDirectory() & "\images\weather\Drizzle Snow.png")
            Case "Showers"
                Return image.FromFile(Directory.GetCurrentDirectory() & "\images\weather\Slight Drizzle.png")
            Case "Haze"
            Case "Foggy"
                Return image.FromFile(Directory.GetCurrentDirectory() & "\images\weather\Haze.png")
            Case "Thunderstorms"
                If am Then
                    Return image.FromFile(Directory.GetCurrentDirectory() & "\images\weather\Thunderstorms.png")
                Else
                    Return image.FromFile(Directory.GetCurrentDirectory() & "\images\weather\Thunderstorms Snow.png")
                End If

        End Select

        Return image.FromFile(Directory.GetCurrentDirectory() & "\images\weather\Cloudy.png")
    End Function

    Function GetWeather() As String
        Return GetWeather(FullTextXML)
    End Function
End Class