Imports System.Web.RegularExpressions
Imports System.Text.RegularExpressions
Imports System.Net

Public Class clsNews
    Private FullTextXML As String
    Private PartTextXML As String
    Private newsArrayList As New ArrayList

    Private Function GetXML(ByVal str As String) As String
        Try
            Dim index1 As Integer
            Dim index2 As Integer
            index1 = (str.IndexOf("<channel>")) + Len("<channel>")
            index2 = (str.IndexOf("</channel>"))
            Dim str1 As String = str.Substring(index1, index2 - index1)
            Return str1
        Catch ex As Exception
            Return "Error"
        End Try
    End Function

    Private Sub PopulateItemArray(ByVal str As String)
        Dim index1 As Integer
        Dim index2 As Integer
        index1 = (str.IndexOf("<title>")) + Len("<title>")
        index2 = (str.IndexOf("</title>"))
        Dim strTemp As String = str.Substring(index1, index2 - index1)
        newsArrayList.Add(strTemp)
    End Sub

    Function GetNewsArray() As ArrayList
        Return newsArrayList
    End Function

    Private Sub Disect(ByVal str As String)
        While True
            Dim index1 As Integer
            Dim index2 As Integer
            index1 = (str.IndexOf("<item>")) + Len("<item>")
            index2 = (str.IndexOf("</item>"))
            If index1 = -1 Or index2 = -1 Then
                Return
            End If
            Dim strTemp As String = str.Substring(index1, index2 - index1 + Len("</item>"))
            PopulateItemArray(strTemp)
            str = str.Substring(index2 + Len("</item>"))
        End While
    End Sub

    Sub New()
        Try
            If My.Computer.Network.IsAvailable = True Then
                Dim rq As HttpWebRequest = WebRequest.Create("http://news.yahoo.com/rss/")
                rq.KeepAlive = False
                rq.Timeout = 1500

                Dim response = rq.GetResponse

                Dim reader = response.GetResponseStream

                Dim doc As New System.Xml.XmlDocument()

                'Load data   
                doc.Load(reader)
                Dim str = doc.OuterXml
                FullTextXML = doc.OuterXml
                PartTextXML = GetXML(str)
                Disect(PartTextXML)
            End If
        Catch ex As WebException

        End Try
        
    End Sub

End Class