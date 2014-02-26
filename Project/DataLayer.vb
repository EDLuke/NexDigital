Imports System
Imports System.Data
Imports System.Configuration
' The database access Namespaces
Imports System.Data.SqlClient
Imports System.Data.SqlServerCe
Imports System.IO

''' <summary>
''' Summary description for DataLayer
''' </summary>
Public Class DataLayer

    Public Shared conString As String = "Data Source=|DataDirectory|\Database.sdf"

    Public Shared Function InsertItem(ByVal categoryId As Integer, ByVal item As String, ByVal desc As String, ByVal price As Double, ByVal pictureUrl As String) As Boolean
        Dim recordSaved As Boolean
        ' Create a connection object and open the connection
        Dim sqlConn As New SqlCeConnection(conString)
        sqlConn.Open()

        Dim command As SqlCeCommand = sqlConn.CreateCommand()
        Dim trans As SqlCeTransaction
        Dim strSQL As String

        'Start a local transaction
        trans = sqlConn.BeginTransaction()

        'Must assign both transation object and connection to Command object for a pending local transation
        command.Connection = sqlConn
        command.Transaction = trans

        Try
            ' SqlQuery for inserting data into the table
            strSQL = "Insert into Items(Name, Description, Price, picture, CategoryID) values (@item, @desc, @price, @pictureUrl, @categoryId)"

            ' Set command type to text and set the query to strSql
            command.CommandType = CommandType.Text
            command.CommandText = strSQL

            command.Parameters.AddWithValue("@item", item)
            command.Parameters.AddWithValue("@desc", desc)
            command.Parameters.AddWithValue("@price", price)
            command.Parameters.AddWithValue("@pictureUrl", pictureUrl)
            command.Parameters.AddWithValue("@categoryId", categoryId)

            ' Run the query
            command.ExecuteNonQuery()
            trans.Commit()

            recordSaved = True
        Catch exc As Exception
            Try
                'Prevent unwanted changes
                trans.Rollback()
            Catch ex As SqlException
                If Not trans.Connection Is Nothing Then
                    Console.WriteLine("An exception of type " & ex.GetType().ToString() & _
                                      " was encountered while attempting to roll back the transaction.")
                End If
            End Try
            Console.Write(exc)
            recordSaved = False
        Finally
            ' Close the connection
            sqlConn.Close()
        End Try


        Return recordSaved
    End Function

    Public Shared Function UpdateItem(ByVal itemid As Integer, ByVal item As String, ByVal desc As String, ByVal price As Double, ByVal pictureUrl As String, ByVal slideFull As Boolean, ByVal categoryid As Integer) As Boolean

        Dim recordSaved As Boolean
        ' Create a connection object and open the connection
        Dim sqlConn As New SqlCeConnection(conString)
        sqlConn.Open()

        Dim command As SqlCeCommand = sqlConn.CreateCommand()
        Dim trans As SqlCeTransaction
        Dim strSQL As String

        'Start a local transaction
        trans = sqlConn.BeginTransaction()

        'Must assign both transation object and connection to Command object for a pending local transation
        command.Connection = sqlConn
        command.Transaction = trans

        'Dim descFixed = FixString(desc, "'", "\'")

        Try
            ' SqlQuery for updating data into the table
            strSQL = "Update Items SET Name= @item, Description = @desc , Price = @price , picture =  @pictureUrl , slideFull = @slideFull, categoryId = @categoryId WHERE itemID= @itemid"

            ' Set command type to text and set the query to strSql
            command.CommandType = CommandType.Text
            command.CommandText = strSQL
            command.Parameters.AddWithValue("@item", item)
            command.Parameters.AddWithValue("@desc", desc)
            command.Parameters.AddWithValue("@price", price)
            command.Parameters.AddWithValue("@pictureUrl", pictureUrl)
            command.Parameters.AddWithValue("@itemid", itemid)
            command.Parameters.AddWithValue("@slideFull", slideFull)
            command.Parameters.AddWithValue("@categoryId", categoryid)

            ' Run the query
            command.ExecuteNonQuery()
            trans.Commit()

            recordSaved = True
        Catch exc As Exception
            Try
                'Prevent unwanted changes
                trans.Rollback()
            Catch ex As SqlException
                If Not trans.Connection Is Nothing Then
                    Console.WriteLine("An exception of type " & ex.GetType().ToString() & _
                                      " was encountered while attempting to roll back the transaction.")
                End If
            End Try
            Console.Write(exc)
            recordSaved = False
        Finally
            ' Close the connection
            sqlConn.Close()
        End Try

        Return recordSaved
    End Function

    Public Shared Function DeleteItem(ByVal itemid As Integer) As Boolean
        Dim maxMenuOrder As Integer
        Dim maxMenuOrderTwo As Integer
        Dim maxSlideOrder As Integer
        Dim maxSlideOrderTwo As Integer
        Dim recordSaved As Boolean

        ' Create a connection object and open the connection
        Dim sqlConn As New SqlCeConnection(conString)
        sqlConn.Open()

        Dim trans = sqlConn.BeginTransaction()
        Dim command As SqlCeCommand = sqlConn.CreateCommand()
        Dim strSQL As String

        ' OleDbTransaction object to handle the transaction
        'Dim myTransaction As SqlCeTransaction

        Try
            maxMenuOrder = FindMaxItemOrder(1)
            maxMenuOrderTwo = FindMaxItemOrder(2)
            maxSlideOrder = FindMaxSlideShowOrder(1)
            maxSlideOrderTwo = FindMaxSlideShowOrder(2)

            'Find the deleted Menu item order
            strSQL = "select menuItem from items WHERE itemID=" & itemid & ""
            command.CommandText = strSQL

            ' Run the query
            Dim deletedMenuOrder = command.ExecuteScalar()

            'Find the deleted Menu item order
            strSQL = "select menuItem2 from items WHERE itemID=" & itemid & ""
            command.CommandText = strSQL

            ' Run the query
            Dim deletedMenuOrderTwo = command.ExecuteScalar()

            'Find the deleted Menu item order
            strSQL = "select slideshow from items WHERE itemID=" & itemid & ""
            command.CommandText = strSQL

            ' Run the query
            Dim deletedSlideOrder = command.ExecuteScalar()

            'Find the deleted Menu item order
            strSQL = "select slideshow2 from items WHERE itemID=" & itemid & ""
            command.CommandText = strSQL

            ' Run the query
            Dim deletedSlideOrderTwo = command.ExecuteScalar()

            'Now Delete the item
            strSQL = "Delete from Items WHERE itemID=" & itemid & ""

            ' Set command type to text and set the query to strSql
            command.CommandType = CommandType.Text
            command.CommandText = strSQL

            ' Run the query
            command.ExecuteNonQuery()

            'Move all the item behind the delete item in front in menu
            If deletedMenuOrder <> maxMenuOrder And deletedMenuOrder <> 0 Then
                For i = deletedMenuOrder To maxMenuOrder - 1
                    strSQL = "Update Items SET menuItem =" & i & " WHERE menuItem=" & (i + 1) & ""
                    command.CommandText = strSQL
                    command.ExecuteNonQuery()
                Next
            End If

            'Move all the item behind the delete item in front in menu2
            If deletedMenuOrderTwo <> maxMenuOrderTwo And deletedMenuOrderTwo <> 0 Then
                For i = deletedMenuOrderTwo To maxMenuOrder - 1
                    strSQL = "Update Items SET menuItem2 =" & i & " WHERE menuItem2=" & (i + 1) & ""
                    command.CommandText = strSQL
                    command.ExecuteNonQuery()
                Next
            End If

            'Move all the item behind the delete item in front in slideshow
            If deletedSlideOrder <> maxSlideOrder And deletedSlideOrder <> 0 Then
                For i = deletedSlideOrder To maxSlideOrder - 1
                    strSQL = "Update Items SET slideshow =" & i & " WHERE slideshow=" & (i + 1) & ""
                    command.CommandText = strSQL
                    command.ExecuteNonQuery()
                Next
            End If

            'Move all the item behind the delete item in front in slideshow2
            If deletedSlideOrderTwo <> maxSlideOrderTwo And deletedMenuOrderTwo <> 0 Then
                For i = deletedSlideOrderTwo To maxSlideOrderTwo - 1
                    strSQL = "Update Items SET slideshow2 =" & i & " WHERE slideshow2 =" & (i + 1) & ""
                    command.CommandText = strSQL
                    command.ExecuteNonQuery()
                Next
            End If

            trans.Commit()

            ' Close the connection
            sqlConn.Close()
            recordSaved = True
        Catch ex As Exception
            Console.Write(ex)
            recordSaved = False
            trans.Rollback()
        End Try

        Return recordSaved
    End Function

    Public Shared Function GetItemDetails(ByVal itemid As Integer) As ArrayList

        Dim result As New ArrayList

        Dim sqlConn As SqlCeConnection
        Dim sqlDA As SqlCeDataAdapter
        ' create connection using the Conection String
        sqlConn = New SqlCeConnection(conString)
        ' Query statement to get all the records from the tblPersonnel 
        sqlDA = New SqlCeDataAdapter("select * from items WHERE itemid=" & itemid, sqlConn)

        ' Create the dataset
        Dim datasetitems As New DataSet()
        ' Fill the dataset from the database table using the data adapter
        sqlDA.Fill(datasetitems)

        For Each row As DataRow In datasetitems.Tables(0).Rows
            'Dim  As [String] = row("Product_ID").ToString()
            Dim name As [String] = row("Name").ToString()

            Dim desc As [String] = row("Description").ToString()
            Dim price As [String] = row("Price").ToString()
            Dim image As [String] = row("Picture").ToString()
            Dim full As [String] = row("SlideFull").ToString()
            Dim categoryId As [String] = row("CategoryId").ToString()
            'Dim quantity As [String] = row("Product_Quantity").ToString()
            ' Add to result arraylist
            'result.Add(prodid)
            result.Add(name)
            result.Add(desc)
            result.Add(price)
            'result.Add(quantity)
            result.Add(image)
            result.Add(full)
            result.Add(categoryId)
        Next

        Return result

    End Function

    Public Shared Function GetItemOfCategory(ByVal categoryid As Integer) As ArrayList
        Dim result As New ArrayList

        Dim sqlConn As SqlCeConnection
        Dim sqlDA As SqlCeDataAdapter
        ' create connection using the Conection String
        sqlConn = New SqlCeConnection(conString)
        ' Query statement to get all the records from the tblPersonnel 
        sqlDA = New SqlCeDataAdapter("select itemid, name, price, description from items WHERE categoryid=" & categoryid, sqlConn)
        ' Create the dataset
        Dim datasetitems As New DataSet()
        ' Fill the dataset from the database table using the data adapter
        sqlDA.Fill(datasetitems)

        For Each row As DataRow In datasetitems.Tables(0).Rows
            Dim itemid As String = row("ItemId").ToString()
            Dim name As [String] = row("Name").ToString()
            Dim price As [String] = row("Price").ToString()
            Dim desp As [String] = row("Description").ToString()
            result.Add(itemid)
            result.Add(name)
            result.Add(price)
            result.Add(desp)
        Next

        Return result

    End Function

    Public Shared Function GetMenuItems(ByVal menu As Integer) As ArrayList
        Dim menuNum As String = ""
        Select Case menu
            Case 1
                menuNum = ""
            Case 2
                menuNum = "2"
        End Select

        Dim result As New ArrayList

        Dim sqlConn As SqlCeConnection
        Dim sqlDA As SqlCeDataAdapter
        ' create connection using the Conection String
        sqlConn = New SqlCeConnection(conString)
        ' Query statement to get all the records from the tblPersonnel 
        sqlDA = New SqlCeDataAdapter("select itemid, name, price, description, menuItem" & menuNum & " from items WHERE menuItem" & menuNum & " != 0", sqlConn)

        ' Create the dataset
        Dim datasetitems As New DataSet()
        ' Fill the dataset from the database table using the data adapter
        sqlDA.Fill(datasetitems)

        For Each row As DataRow In datasetitems.Tables(0).Rows
            Dim itemid As String = row("ItemId").ToString()
            Dim name As [String] = row("Name").ToString()
            Dim price As [String] = row("Price").ToString()
            Dim desp As [String] = row("Description").ToString()
            Dim menuOrder As [String] = row("MenuItem" & menuNum).ToString()
            result.Add(itemid)
            result.Add(name)
            result.Add(price)
            result.Add(desp)
        Next

        SortMenu(result, menu)
        Return result
    End Function

    Public Shared Sub SortMenu(ByRef result As ArrayList, ByVal menu As Integer)
        Dim menuNum As String = ""
        Select Case menu
            Case 1
                menuNum = ""
            Case 2
                menuNum = "2"
        End Select

        Dim sqlConn As SqlCeConnection
        ' create connection using the Conection String
        sqlConn = New SqlCeConnection(conString)
        ' Query statement to get all the records from the tblPersonnel 
        ' Fill the dataset from the database table using the data adapter

        Dim maxMenuOrder = FindMaxItemOrder(menu)
        Dim arrayTemp As New ArrayList

        For i = 1 To maxMenuOrder
            ' Create the dataset
            Dim datasetitems As New DataSet()
            Dim sqlDA As SqlCeDataAdapter
            sqlDA = New SqlCeDataAdapter("select itemid, name, price, description from items WHERE menuItem" & menuNum & " = " & i, sqlConn)
            sqlDA.Fill(datasetitems)
            For Each row As DataRow In datasetitems.Tables(0).Rows
                Dim itemid As String = row("ItemId").ToString()
                Dim name As [String] = row("Name").ToString()
                Dim price As [String] = row("Price").ToString()
                Dim desp As [String] = row("Description").ToString()
                arrayTemp.Add(itemid)
                arrayTemp.Add(name)
                arrayTemp.Add(price)
                arrayTemp.Add(desp)
            Next
        Next

        result = arrayTemp.GetRange(0, maxMenuOrder * 4)
    End Sub

    Public Shared Sub SortSlideShow(ByRef result As ArrayList, ByVal slideShow As Integer)
        Dim slideShowNum As String = ""
        Select Case slideShow
            Case 1
                slideShowNum = ""
            Case 2
                slideShowNum = "2"
        End Select

        Dim sqlConn As SqlCeConnection
        ' create connection using the Conection String
        sqlConn = New SqlCeConnection(conString)
        ' Query statement to get all the records from the tblPersonnel 
        ' Fill the dataset from the database table using the data adapter

        Dim maxSlideShowOrder = FindMaxSlideShowOrder(slideShow)
        Dim arrayTemp As New ArrayList

        For i = 1 To maxSlideShowOrder
            ' Create the dataset
            Dim datasetitems As New DataSet()
            Dim sqlDA As SqlCeDataAdapter
            sqlDA = New SqlCeDataAdapter("select itemid, Picture, name from items WHERE slideShow" & slideShowNum & " = " & i, sqlConn)
            sqlDA.Fill(datasetitems)
            For Each row As DataRow In datasetitems.Tables(0).Rows
                Dim itemid As String = row("ItemId").ToString()
                Dim pic As [String] = row("Picture").ToString()
                Dim name As [String] = row("Name").ToString()
                arrayTemp.Add(itemid)
                arrayTemp.Add(pic)
                arrayTemp.Add(name)
            Next
        Next

        result = arrayTemp.GetRange(0, maxSlideShowOrder * 3)
    End Sub

    Public Shared Sub SwitchMenuOrder(ByVal itemIdOne As Integer, ByVal itemIdTwo As Integer, ByVal menu As Integer)
        Dim menuNum As String = ""
        Select Case menu
            Case 1
                menuNum = ""
            Case 2
                menuNum = "2"
        End Select

        Dim sqlConn As New SqlCeConnection(conString)
        Dim command As SqlCeCommand = sqlConn.CreateCommand()
        Dim strSQL As String

        sqlConn.Open()

        strSQL = "select menuItem" & menuNum & " from items where itemid = " & itemIdOne
        command.CommandText = strSQL

        ' Run the query
        Dim menuOrderOne = command.ExecuteScalar()

        strSQL = "select menuItem" & menuNum & " from items where itemid = " & itemIdTwo
        command.CommandText = strSQL

        ' Run the query
        Dim menuOrderTwo = command.ExecuteScalar()


        ' SqlQuery for updating data into the table
        strSQL = "Update Items SET MenuItem" & menuNum & "=" & menuOrderTwo & " WHERE itemID=" & itemIdOne & ""

        command.CommandType = CommandType.Text
        command.CommandText = strSQL
        ' Run the query
        command.ExecuteNonQuery()

        ' SqlQuery for updating data into the table
        strSQL = "Update Items SET MenuItem" & menuNum & "=" & menuOrderOne & " WHERE itemID=" & itemIdTwo & ""

        command.CommandText = strSQL
        ' Run the query
        command.ExecuteNonQuery()

        sqlConn.Close()
    End Sub

    Public Shared Sub SwitchSlideShowOrder(ByVal itemIdOne As Integer, ByVal itemIdTwo As Integer, ByVal slideShow As Integer)
        Dim slideShowNum As String = ""
        Select Case slideShow
            Case 1
                slideShowNum = ""
            Case 2
                slideShowNum = "2"
        End Select

        Dim sqlConn As New SqlCeConnection(conString)
        Dim command As SqlCeCommand = sqlConn.CreateCommand()

        Dim strSQL As String

        sqlConn.Open()

        strSQL = "select slideShow" & slideShowNum & " from items where itemid = " & itemIdOne
        command.CommandText = strSQL

        ' Run the query
        Dim slideShowOrderOne = command.ExecuteScalar()

        strSQL = "select slideShow" & slideShowNum & " from items where itemid = " & itemIdTwo
        command.CommandText = strSQL

        ' Run the query
        Dim slideShowOrderTwo = command.ExecuteScalar()

        ' SqlQuery for updating data into the table
        strSQL = "Update Items SET slideShow" & slideShowNum & "=" & slideShowOrderTwo & " WHERE itemID=" & itemIdOne & ""

        command.CommandType = CommandType.Text
        command.CommandText = strSQL
        ' Run the query
        command.ExecuteNonQuery()

        ' SqlQuery for updating data into the table
        strSQL = "Update Items SET slideShow" & slideShowNum & "=" & slideShowOrderOne & " WHERE itemID=" & itemIdTwo & ""

        command.CommandText = strSQL
        ' Run the query
        command.ExecuteNonQuery()

        sqlConn.Close()
    End Sub

    Public Shared Function FindMaxItemOrder(ByVal menu As Integer) As Integer
        Dim menuNum As String = ""
        Select Case menu
            Case 1
                menuNum = ""
            Case 2
                menuNum = "2"
        End Select

        Using sqlConn As New SqlCeConnection(conString)
            Try
                ' Create a connection object and open the connection
                Dim command As SqlCeCommand = sqlConn.CreateCommand()
                Dim strSQL As String

                sqlConn.Open()

                'Find the max MenuItem int
                strSQL = "select max(menuItem" & menuNum & ") from items"
                command.CommandText = strSQL

                ' Run the query
                Dim max = command.ExecuteScalar()
                sqlConn.Close()
                Return max
            Catch ex As Exception
                Console.Write(ex)
            Finally
                If sqlConn.State = ConnectionState.Open Then
                    sqlConn.Close()
                End If
            End Try

        End Using

        Return 0
    End Function

    Public Shared Function FindMaxSlideShowOrder(ByVal slideShow As Integer) As Integer
        Threading.Thread.SpinWait(1)

        Dim slideShowNum As String = ""
        Select Case slideShow
            Case 1
                slideShowNum = ""
            Case 2
                slideShowNum = "2"
        End Select

        Using sqlConn As New SqlCeConnection(conString)
            Try
                ' Create a connection object and open the connection

                Dim command As SqlCeCommand = sqlConn.CreateCommand()
                Dim strSQL As String

                sqlConn.Open()

                'Find the max SlideShow int

                strSQL = "select max(slideShow" & slideShowNum & ") from items"
                command.CommandText = strSQL

                ' Run the query
                Dim max = command.ExecuteScalar()
                sqlConn.Close()
                Return max
            Catch ex As Exception
                Console.Write(ex)
            Finally
                If sqlConn.State = ConnectionState.Open Then
                    sqlConn.Close()
                End If
            End Try
        End Using

        Return 0
    End Function

    Public Shared Function AddToMenu(ByVal itemid As Integer, ByVal menu As Integer) As Boolean
        Dim menuNum As String = ""
        Select Case menu
            Case 1
                menuNum = ""
            Case 2
                menuNum = "2"
        End Select

        Dim recordSaved As Boolean
        Dim maxMenuItem = FindMaxItemOrder(menu) + 1
        ' OleDbTransaction object to handle the transaction
        'Dim myTransaction As SqlCeTransaction

        Try
            ' Create a connection object and open the connection
            Dim sqlConn As New SqlCeConnection(conString)
            Dim command As SqlCeCommand = sqlConn.CreateCommand()
            Dim strSQL As String

            sqlConn.Open()

            ' SqlQuery for updating data into the table
            strSQL = "Update Items SET MenuItem" & menuNum & "=" & maxMenuItem & " WHERE itemID=" & itemid & ""

            ' Set command type to text and set the query to strSql
            command.CommandType = CommandType.Text
            command.CommandText = strSQL

            ' Run the query
            command.ExecuteNonQuery()

            ' Close the connection
            sqlConn.Close()
            recordSaved = True
        Catch ex As Exception
            Console.Write(ex)
            recordSaved = False
        End Try
        Return recordSaved
    End Function

    Public Shared Function RemoveFromMenu(ByVal itemid As Integer, ByVal menu As Integer) As Boolean
        Dim menuNum As String = ""
        Select Case menu
            Case 1
                menuNum = ""
            Case 2
                menuNum = "2"
        End Select

        Dim recordSaved As Boolean
        Dim maxMenuOrder = FindMaxItemOrder(menu)
        ' OleDbTransaction object to handle the transaction
        'Dim myTransaction As SqlCeTransaction

        Try
            ' Create a connection object and open the connection
            Dim sqlConn As New SqlCeConnection(conString)

            Dim command As SqlCeCommand = sqlConn.CreateCommand()
            Dim trans As SqlCeTransaction
            Dim strSQL As String

            sqlConn.Open()

            'Start a local transaction
            trans = sqlConn.BeginTransaction()

            'Find the Removed Menu item order
            strSQL = "select menuItem" & menuNum & " from items WHERE itemID=" & itemid & ""
            command.CommandType = CommandType.Text
            command.CommandText = strSQL

            ' Run the query
            Dim deletedMenuOrder = command.ExecuteScalar()

            ' SqlQuery for updating data into the table
            'strSQL = "Insert into Items(Name, Description, Price, picture, CategoryID) values ('" & item & "','" & desc & "', " & price & ",'" & pictureUrl & "', " & categoryId & ")"

            strSQL = "Update Items SET MenuItem" & menuNum & "=" & 0 & " WHERE itemID=" & itemid & ""

            ' Set command type to text and set the query to strSql
            command.CommandText = strSQL
            command.ExecuteNonQuery()

            'Move all the item behind the delete item in front in menu
            If maxMenuOrder <> 1 Then
                If deletedMenuOrder <> maxMenuOrder Then
                    For i = deletedMenuOrder To maxMenuOrder
                        strSQL = "Update Items SET menuItem" & menuNum & " =" & i & " WHERE menuItem" & menuNum & "=" & (i + 1) & ""
                        command.CommandText = strSQL
                        command.ExecuteNonQuery()
                    Next
                End If
            End If

            trans.Commit()

            ' Close the connection
            sqlConn.Close()
            recordSaved = True
        Catch ex As Exception
            Console.Write(ex)
            recordSaved = False
        End Try
        Return recordSaved
    End Function

    Public Shared Function CreateCategory(ByVal categoryName As String) As Boolean

        Dim recordSaved As Boolean

        Try
            ' Create a connection object and open the connection
            Dim sqlConn As New SqlCeConnection(conString)

            Dim command As SqlCeCommand = sqlConn.CreateCommand()
            Dim strSQL As String

            ' SqlQuery for inserting data into the table
            strSQL = "Insert into Category(CategoryName) values ('" & categoryName & "')"

            ' Set command type to text and set the query to strSql
            command.CommandType = CommandType.Text
            command.CommandText = strSQL

            sqlConn.Open()
            ' Run the query
            command.ExecuteNonQuery()

            ' Close the connection
            sqlConn.Close()
            recordSaved = True
        Catch ex As Exception
            Console.Write(ex)
            recordSaved = False
        End Try
        Return recordSaved
    End Function

    Public Shared Function DeleteCategory(ByVal categoryid As Integer) As Boolean
        Dim recordDeleted As Boolean

        'Delete All items of the category
        Try
            'First Find All the items within the category
            Dim sqlConn As New SqlCeConnection(conString)
            Dim result As New ArrayList
            Dim sqlDA As SqlCeDataAdapter
            ' create connection using the Conection String
            sqlConn = New SqlCeConnection(conString)
            ' Query statement to get all the records from the tblPersonnel 
            sqlDA = New SqlCeDataAdapter("select itemid from items WHERE categoryid =" & categoryid, sqlConn)

            ' Create the dataset
            Dim datasetitems As New DataSet()
            ' Fill the dataset from the database table using the data adapter
            sqlDA.Fill(datasetitems)

            For Each row As DataRow In datasetitems.Tables(0).Rows
                Dim itemid As String = row("ItemId").ToString()
                result.Add(itemid)
            Next

            'Then call DeleteItem on all of them
            For Each itemid As String In result
                DeleteItem(CInt(itemid))
            Next

            ' Close the connection
            sqlConn.Close()
            recordDeleted = True
        Catch ex As Exception
            Console.Write(ex)
            recordDeleted = False
        End Try

        Try
            ' Create a connection object and open the connection
            Dim sqlConn As New SqlCeConnection(conString)

            Dim command As SqlCeCommand = sqlConn.CreateCommand()
            Dim strSQL As String

            strSQL = "Delete from Category WHERE categoryId=" & categoryid & ""

            ' Set command type to text and set the query to strSql
            command.CommandType = CommandType.Text
            command.CommandText = strSQL

            sqlConn.Open()
            ' Run the query
            command.ExecuteNonQuery()

            ' Close the connection
            sqlConn.Close()
            recordDeleted = True
        Catch ex As Exception
            Console.Write(ex)
            recordDeleted = False
        End Try


        Return recordDeleted
    End Function

    Public Shared Function GetPicsOfCategoryItems(ByVal categoryid As Integer) As ArrayList

        Dim result As New ArrayList

        Dim sqlConn As SqlCeConnection
        Dim sqlDA As SqlCeDataAdapter
        ' create connection using the Conection String
        sqlConn = New SqlCeConnection(conString)
        ' Query statement to get all the records from the tblPersonnel 
        sqlDA = New SqlCeDataAdapter("select itemid, Picture, Name from items WHERE categoryid=" & categoryid & " AND Picture != '' ", sqlConn)

        ' Create the dataset
        Dim datasetitems As New DataSet()
        ' Fill the dataset from the database table using the data adapter
        sqlDA.Fill(datasetitems)

        For Each row As DataRow In datasetitems.Tables(0).Rows
            Dim itemid As String = row("ItemId").ToString()
            Dim picUrl As [String] = row("Picture").ToString()
            Dim name As [String] = row("Name").ToString
            result.Add(itemid)
            result.Add(picUrl)
            result.Add(name)
        Next

        sqlConn.Close()

        Return result
    End Function

    ' Add item picture to slide show
    Public Shared Function AddToSlideShow(ByVal itemid As Integer, ByVal slideShow As Integer) As Boolean
        Dim slideShowNum As String = ""
        Select Case slideShow
            Case 1
                slideShowNum = ""
            Case 2
                slideShowNum = "2"
        End Select

        Dim recordSaved As Boolean
        Dim maxSlideShowItem = FindMaxSlideShowOrder(slideShow) + 1

        Try
            ' Create a connection object and open the connection
            Dim sqlConn As New SqlCeConnection(conString)

            Dim command As SqlCeCommand = sqlConn.CreateCommand()
            Dim strSQL As String

            strSQL = "Update Items SET SlideShow" & slideShowNum & "=" & maxSlideShowItem & " WHERE itemID = " & itemid & ""


            ' Set command type to text and set the query to strSql
            command.CommandType = CommandType.Text
            command.CommandText = strSQL

            sqlConn.Open()
            ' Run the query
            command.ExecuteNonQuery()

            ' Close the connection
            sqlConn.Close()
            recordSaved = True
        Catch ex As Exception
            Console.Write(ex)
            recordSaved = False
        End Try
        Return recordSaved
    End Function

    Public Shared Function RemoveFromSlideShow(ByVal itemid As Integer, ByVal slideShow As Integer) As Boolean
        Dim slideShowNum As String = ""
        Select Case slideShow
            Case 1
                slideShowNum = ""
            Case 2
                slideShowNum = "2"
        End Select

        Dim recordSaved As Boolean
        Dim maxSlideShowItem = FindMaxSlideShowOrder(slideShow)

        Try
            ' Create a connection object and open the connection
            Dim sqlConn As New SqlCeConnection(conString)
            Dim command As SqlCeCommand = sqlConn.CreateCommand()
            Dim trans As SqlCeTransaction
            Dim strSQL As String

            sqlConn.Open()

            'Start a local transaction
            trans = sqlConn.BeginTransaction()

            'Find the Removed Menu item order
            strSQL = "select slideShow" & slideShowNum & " from items WHERE itemID=" & itemid & ""
            command.CommandType = CommandType.Text
            command.CommandText = strSQL

            ' Run the query
            Dim deletedSlideShowOrder = command.ExecuteScalar()

            strSQL = "Update Items SET SlideShow" & slideShowNum & " = " & 0 & " WHERE itemID = " & itemid & ""

            ' Set command type to text and set the query to strSql
            command.CommandType = CommandType.Text
            command.CommandText = strSQL

            ' Run the query
            command.ExecuteNonQuery()

            'Move all the item behind the delete item in front in menu
            If maxSlideShowItem <> 1 Then
                If deletedSlideShowOrder <> maxSlideShowItem Then
                    For i = deletedSlideShowOrder To maxSlideShowItem
                        strSQL = "Update Items SET slideShow" & slideShowNum & " =" & i & " WHERE slideShow" & slideShowNum & " =" & (i + 1) & ""
                        command.CommandText = strSQL
                        command.ExecuteNonQuery()
                    Next
                End If
            End If

            trans.Commit()

            ' Close the connection
            sqlConn.Close()
            recordSaved = True
        Catch ex As Exception
            Console.Write(ex)
            recordSaved = False
        End Try
        Return recordSaved
    End Function

    Public Shared Function GetSlideShowItems(ByVal slideShow As Integer) As ArrayList
        Dim slideShowNum As String = ""
        Select Case slideShow
            Case 1
                slideShowNum = ""
            Case 2
                slideShowNum = "2"
        End Select

        Dim result As New ArrayList

        Dim sqlConn As SqlCeConnection
        Dim sqlDA As SqlCeDataAdapter
        ' create connection using the Conection String
        sqlConn = New SqlCeConnection(conString)
        ' Query statement to get all the records from the tblPersonnel 
        sqlDA = New SqlCeDataAdapter("select itemid, Picture, Name from items WHERE SlideShow" & slideShowNum & " != 0", sqlConn)

        ' Create the dataset
        Dim datasetitems As New DataSet()
        ' Fill the dataset from the database table using the data adapter
        sqlDA.Fill(datasetitems)

        For Each row As DataRow In datasetitems.Tables(0).Rows
            Dim itemid As String = row("ItemId").ToString()
            Dim pic As [String] = row("Picture").ToString()
            Dim name As [String] = row("Name").ToString()
            result.Add(itemid)
            result.Add(pic)
            result.Add(name)
        Next

        SortSlideShow(result, slideShow)

        sqlConn.Close()

        Return result

    End Function

    Public Shared Function GetSlideShowFull() As ArrayList

        Dim result As New ArrayList

        Dim sqlConn As SqlCeConnection
        Dim sqlDA As SqlCeDataAdapter
        ' create connection using the Conection String
        sqlConn = New SqlCeConnection(conString)
        ' Query statement to get all the records from the tblPersonnel 
        sqlDA = New SqlCeDataAdapter("select itemid, SlideFull from items WHERE SlideShow2 != 0", sqlConn)

        ' Create the dataset
        Dim datasetitems As New DataSet()
        ' Fill the dataset from the database table using the data adapter
        sqlDA.Fill(datasetitems)

        For Each row As DataRow In datasetitems.Tables(0).Rows
            Dim itemid As String = row("ItemId").ToString()
            Dim full As [String] = row("SlideFull").ToString()
            result.Add(itemid)
            result.Add(full)
            result.Add("")
        Next

        sqlConn.Close()

        Return result

    End Function

    Public Shared Function FixString(ByVal SourceString As String, ByVal StringToReplace As String, ByVal StringReplacement As String)
        SourceString = SourceString.Replace(StringToReplace, StringReplacement)
        Return SourceString
    End Function

    Public Shared Sub Validate()
        TestConnection()

        For i = 1 To 2
            Dim missingMenu As List(Of Integer) = ValidateMenu(i)
            ReorderMenu(missingMenu, i)

        Next
        For i = 1 To 2
            Dim missingSlide As List(Of Integer) = ValidateSlideShow(i)
            ReorderSlideShow(missingSlide, i)
        Next
    End Sub

    Private Shared Sub TestConnection()
        Using conn = New SqlCeConnection(conString)
            Try
                conn.Open()
            Catch generatedExceptionName As SqlCeException
                Console.WriteLine("Connection Failed")
            Finally
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If
            End Try
        End Using
    End Sub

    Private Shared Sub ReorderMenu(ByRef missingMenu As List(Of Integer), ByVal menu As Integer)
        If missingMenu.Count = 0 Then
            Return
        Else
            Dim menuNum As String = ""
            Select Case menu
                Case 1
                    menuNum = ""
                Case 2
                    menuNum = "2"
            End Select

            Dim sqlConn As New SqlCeConnection(conString)
            Dim command As SqlCeCommand = sqlConn.CreateCommand()
            Dim strSQL As String

            sqlConn.Open()

            While missingMenu.Count > 0
                Dim maxMenuOrder As Integer = FindMaxItemOrder(menu)
                For i = missingMenu(0) + 1 To maxMenuOrder
                    ' SqlQuery for updating data into the table
                    strSQL = "Update Items SET menuItem" & menuNum & "=" & i - 1 & " WHERE menuItem" & menuNum & "=" & i & ""

                    command.CommandType = CommandType.Text
                    command.CommandText = strSQL
                    ' Run the query
                    command.ExecuteNonQuery()
                Next
                missingMenu.RemoveAt(0)
                If missingMenu.Count <> 0 Then
                    For i = 0 To missingMenu.Count
                        missingMenu(i) -= 1
                    Next
                End If

            End While
        End If
    End Sub

    Private Shared Sub ReorderSlideShow(ByRef missingSlide As List(Of Integer), ByVal slide As Integer)
        If missingSlide.Count = 0 Then
            Return
        Else
            Dim slideNum As String = ""
            Select Case slide
                Case 1
                    slideNum = ""
                Case 2
                    slideNum = "2"
            End Select

            Dim sqlConn As New SqlCeConnection(conString)
            Dim command As SqlCeCommand = sqlConn.CreateCommand()
            Dim strSQL As String

            sqlConn.Open()

            While missingSlide.Count > 0
                Dim maxMenuOrder As Integer = FindMaxItemOrder(slide)
                For i = missingSlide(0) + 1 To maxMenuOrder
                    ' SqlQuery for updating data into the table
                    strSQL = "Update Items SET slideshow" & slideNum & "=" & i - 1 & " WHERE slideshow" & slideNum & "=" & i & ""

                    command.CommandType = CommandType.Text
                    command.CommandText = strSQL
                    ' Run the query
                    command.ExecuteNonQuery()
                Next
                missingSlide.RemoveAt(0)
                If missingSlide.Count <> 0 Then
                    For i = 0 To missingSlide.Count
                        missingSlide(i) -= 1
                    Next
                End If

            End While
        End If
    End Sub

    Private Shared Function ValidateSlideShow(ByVal slideShow As Integer)
        Dim slideShowNum As String = ""
        Select Case slideShow
            Case 1
                slideShowNum = ""
            Case 2
                slideShowNum = "2"
        End Select

        Dim maxSlideShowItem = FindMaxSlideShowOrder(slideShow)

        Dim result As New ArrayList
        Dim missing As New List(Of Integer)

        Dim sqlConn As SqlCeConnection
        Dim sqlDA As SqlCeDataAdapter
        ' create connection using the Conection String
        sqlConn = New SqlCeConnection(conString)
        ' Query statement to get all the records from the tblPersonnel 
        sqlDA = New SqlCeDataAdapter("select itemid, menuItem" & slideShowNum & " from items WHERE menuItem" & slideShowNum & " != 0", sqlConn)

        ' Create the dataset
        Dim datasetitems As New DataSet()
        ' Fill the dataset from the database table using the data adapter
        sqlDA.Fill(datasetitems)

        For Each row As DataRow In datasetitems.Tables(0).Rows
            Dim itemid As String = row("ItemId").ToString()
            Dim menuOrder As Integer = CInt(row("MenuItem" & slideShowNum & "").ToString())
            result.Add(itemid)
            result.Add(menuOrder)
        Next

        For i = 1 To maxSlideShowItem
            If Not result.Contains(i) Then
                missing.Add(i)
            End If
        Next

        sqlConn.Close()

        Return missing
    End Function

    Private Shared Function ValidateMenu(ByVal menu As Integer)
        Dim menuNum As String = ""
        Select Case menu
            Case 1
                menuNum = ""
            Case 2
                menuNum = "2"
        End Select

        Dim maxMenuItem = FindMaxItemOrder(menu)

        Dim result As New ArrayList
        Dim missing As New List(Of Integer)

        Dim sqlConn As SqlCeConnection
        Dim sqlDA As SqlCeDataAdapter
        ' create connection using the Conection String
        sqlConn = New SqlCeConnection(conString)
        ' Query statement to get all the records from the tblPersonnel 
        sqlDA = New SqlCeDataAdapter("select itemid, menuItem" & menuNum & " from items WHERE menuItem" & menuNum & " != 0", sqlConn)

        ' Create the dataset
        Dim datasetitems As New DataSet()
        ' Fill the dataset from the database table using the data adapter
        sqlDA.Fill(datasetitems)

        For Each row As DataRow In datasetitems.Tables(0).Rows
            Dim itemid As String = row("ItemId").ToString()
            Dim menuOrder As Integer = CInt(row("MenuItem" & menuNum & "").ToString())
            result.Add(itemid)
            result.Add(menuOrder)
        Next

        For i = 1 To maxMenuItem
            If Not result.Contains(i) Then
                missing.Add(i)
            End If
        Next

        sqlConn.Close()

        Return missing
    End Function
End Class

