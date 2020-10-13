Imports System.IO
Imports System.Text
Imports Assurant.Common.FTP

Public Class MiscUtil

#Region "Constants"

    Public Const DATE_FORMAT As String = "dd-MMM-yyyy"
    Public Const DECIMAL_FORMAT As String = "N"
    Public Const DECIMAL_FOR_4_DIGITS_FORMAT As String = "N4"
    'Public Const Reg_Exp As String = "(^[a-z]([a-z_\.]*)@([a-z_\.]*)([.][a-z]{3})$)|(^[a-z]([a-z_\.]*)@([a-z_\.]*)(\.[a-z]{3})(\.[a-z]{2})*$)/i"

    'Public Const Reg_Exp As String = "^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" _
    '                               & "\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" _
    '                               & ".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
    Public Const Reg_Exp As String = "[\w-]+@([\w-]+\.)+[\w-]+"
    Private Const PORT As Integer = 21
    Private Const ENCODING_LATIN As String = "iso-8859-1"
#End Region

#Region "Common Culture Related Helper Function"
    Public Shared Function GetDateFormattedString(value As Date) As String
        'Return value.ToString(DATE_FORMAT, LocalizationMgr.CurrentCulture)
        Return value.ToString(DATE_FORMAT, System.Threading.Thread.CurrentThread.CurrentCulture)
    End Function

    Public Shared Function GetAmountFormattedString(value As Decimal, Optional ByVal format As String = Nothing) As String
        If format Is Nothing Then format = DECIMAL_FORMAT
        'Return value.ToString(format, LocalizationMgr.CurrentCulture)
        Return value.ToString(format, System.Threading.Thread.CurrentThread.CurrentCulture)
    End Function

    Public Shared Function GetAmountFormattedDoubleString(value As String, Optional ByVal format As String = Nothing) As String
        If format Is Nothing Then format = DECIMAL_FORMAT
        Return Convert.ToDouble(value).ToString(format, System.Threading.Thread.CurrentThread.CurrentCulture)
    End Function

    Public Shared Function GetDecimalSeperator(culturecode As String) As String
        Dim decimalsep As String
        decimalsep = System.Globalization.CultureInfo.CreateSpecificCulture(culturecode).NumberFormat.CurrencyDecimalSeparator
        Return decimalsep
    End Function

    Public Shared Function GetGroupSeperator(culturecode As String) As String
        Dim groupsep As String
        groupsep = System.Globalization.CultureInfo.CreateSpecificCulture(culturecode).NumberFormat.CurrencyGroupSeparator
        Return groupsep
    End Function

    Public Shared Function GetCurrencySymbol(culturecode As String) As String
        Dim currencysymbol As String
        currencysymbol = System.Globalization.CultureInfo.CreateSpecificCulture(culturecode).NumberFormat.CurrencySymbol
        Return currencysymbol
    End Function

    Public Shared Function GetShortDateFormat(culturecode As String) As String
        Dim dateformat As String
        dateformat = System.Globalization.CultureInfo.CreateSpecificCulture(culturecode).DateTimeFormat.ShortDatePattern
        Return dateformat
    End Function

    Public Shared Function GetDateSeperator(culturecode As String) As String
        Dim dateSeperator As String
        dateSeperator = System.Globalization.CultureInfo.CreateSpecificCulture(culturecode).DateTimeFormat.DateSeparator
        Return dateSeperator
    End Function


#End Region

#Region "Data Set Exporting"

#Region "Constants"
    Private Const COMMA As String = ","
#End Region

    Public Shared Sub GenerateCommaSeparatedFormat(table As DataTable, columnHeaders() As String, outStream As System.IO.Stream)

        Dim dt As DataTable = table

        Dim row As DataRow
        Dim col As DataColumn
        Dim colIndex As Integer

        Dim strWriter As StreamWriter = New StreamWriter(outStream)

        'Write the header first
        WriteHeader(columnHeaders, strWriter)
        For Each row In dt.Rows
            WriteRow(row, strWriter)
        Next

        strWriter.Flush()
    End Sub


    Private Shared Sub WriteHeader(columnHeaders() As String, strWriter As StreamWriter)
        Dim hdrIndex As Integer

        For hdrIndex = 0 To (columnHeaders.Length - 1)
            strWriter.Write(columnHeaders(hdrIndex))
            If hdrIndex < (columnHeaders.Length - 1) Then
                strWriter.Write(COMMA)
            End If
        Next
        strWriter.WriteLine()
    End Sub


    Private Shared Sub WriteRow(row As DataRow, strWriter As StreamWriter)
        Dim colIndx As Integer
        For colIndx = 0 To (row.Table.Columns.Count - 1)
            strWriter.Write(row.Item(colIndx))
            If colIndx < (row.Table.Columns.Count - 1) Then
                strWriter.Write(COMMA)
            End If
        Next
        strWriter.WriteLine()
    End Sub

#End Region

#Region "Accounting Dates functions"
    'This functions will return the First Friday After a Date or First Friday befor a Date 
    'or First Friday Of a Month or Last Friday Of a Month or return true if it is LeapYear

    Public Shared Function FirstFridayAfterDate(DateVal As Date) As Date
        Dim WkDay As Integer

        WkDay = Weekday(DateVal)
        If WkDay = 6 Then
            Return DateVal.AddDays(7)
        ElseIf WkDay = 7 Then
            Return DateVal.AddDays(6)
        Else
            Return DateVal.AddDays((6 - WkDay))
        End If
    End Function

    Public Shared Function FirstFridayBeforeDate(DateVal As Date) As Date
        Dim WkDay As Integer

        WkDay = Weekday(DateVal)
        If WkDay = 7 Then
            Return DateVal.AddDays(-1)
        Else
            Return DateVal.AddDays(-(WkDay + 1))
        End If
    End Function

    Public Shared Function FirstFridayOfMonth(DateVal As Date) As Date
        Dim Mnth As Integer
        Dim Yr As Integer
        Dim DateTmp As Date

        Mnth = DatePart("m", DateVal)
        Yr = DatePart("yyyy", DateVal)
        DateTmp = DateSerial(Yr, Mnth, 1)
        If Weekday(DateTmp) = 6 Then
            Return DateTmp
        Else
            Return FirstFridayAfterDate(DateTmp)
        End If

    End Function

    Public Shared Function LastFridayOfMonth(DateVal As Date) As Date
        Dim Mnth As Integer
        Dim Yr As Integer
        Dim DateTmp As Date

        Mnth = DatePart("m", DateVal)
        Yr = DatePart("yyyy", DateVal)

        DateTmp = DateSerial(Yr, Mnth, DaysInMonth(Mnth, Yr))
        If Weekday(DateTmp) = 6 Then
            Return DateTmp
        Else
            Return FirstFridayBeforeDate(DateTmp)
        End If

    End Function

    Public Shared Function DaysInMonth(Month As Integer, Year As Integer) As Integer

        Select Case Month
            Case 2
                If IsLeapYear(Year) Then
                    Return 29
                Else
                    Return 28
                End If
            Case 4, 6, 9, 11
                Return 30
            Case Else
                Return 31
        End Select

    End Function

    Public Shared Function IsLeapYear(Year As Integer) As Boolean
        Return (Month(DateSerial(Year, 2, 29)) = 2)
    End Function

#End Region

#Region "DataBase"

#Region "Constants"

    Private Shared SQL_INVALID_CHARACTERS As String() = {"'", ";", "--", "/*"} 'SQL invalid characters

#End Region
    'Public Shared Function GuidToSQLString(ByVal Value As Guid) As String
    '    Dim byteArray As Byte() = Value.ToByteArray
    '    Dim i As Integer
    '    Dim result As New StringBuilder("")
    '    For i = 0 To byteArray.Length - 1
    '        Dim hexStr As String = byteArray(i).ToString("X")
    '        If hexStr.Length < 2 Then
    '            hexStr = "0" & hexStr
    '        End If
    '        result.Append(hexStr)
    '    Next
    '    Return result.ToString
    'End Function

    Public Shared Function CleanseSQLInjectChars(sSQL As String) As String
        Dim sNewSQL As String = sSQL
        Dim index As Integer

        For index = 0 To SQL_INVALID_CHARACTERS.Length - 1
            'replace the invalid character with blank
            sNewSQL = sNewSQL.Replace(SQL_INVALID_CHARACTERS(index), "")
        Next

        Return sNewSQL

    End Function


    Public Shared Function IsCriteriaSelected(oList As ArrayList) As Boolean
        Dim bIsSelected As Boolean = False

        If ((oList IsNot Nothing) AndAlso (oList.Count > 0) AndAlso _
             Not (CType(oList(0), Guid).Equals(Guid.Empty))) Then
            bIsSelected = True
        End If
        Return bIsSelected
    End Function

    Public Shared Function GuidToSQLString(Value As Guid) As String
        Dim byteArray As Byte() = Value.ToByteArray
        Dim i As Integer
        Dim result As New StringBuilder("")
        For i = 0 To byteArray.Length - 1
            Dim hexStr As String = byteArray(i).ToString("X")
            If hexStr.Length < 2 Then
                hexStr = "0" & hexStr
            End If
            result.Append(hexStr)
        Next
        Return result.ToString
    End Function

    Public Shared Function GetDbStringFromGuid(oItem As Object, Optional ByVal isHEXTORAW As Boolean = False) As String
        Dim value As Guid = CType(oItem, Guid)

        If value.Equals(Guid.Empty) Then
            Return "NULL"
        Else
            If isHEXTORAW = True Then
                Return "HEXTORAW('" & GuidControl.GuidToHexString(value) & "')"
            Else
                Return "'" & GuidControl.GuidToHexString(value) & "'"
            End If

        End If

    End Function

    Public Shared Function BuildListForSql(colName As String, oList As ArrayList, Optional ByVal isHEXTORAW As Boolean = False) As String
        Dim sqlList As String = String.Empty
        Dim index As Integer
        Dim oItem As Object

        If oList.Count = 1 Then
            sqlList = colName & " = " & GetDbStringFromGuid(oList(0), isHEXTORAW)
        ElseIf oList.Count > 1 Then
            sqlList = colName & " in ( " & GetDbStringFromGuid(oList(0), isHEXTORAW)
            For index = 1 To oList.Count - 1
                sqlList &= ", " & GetDbStringFromGuid(oList(index), isHEXTORAW)
            Next
            sqlList &= " )"
        End If

        Return sqlList
    End Function

    Public Shared Function BuildListForNetSql(colName As String, oList As ArrayList) As String
        Dim sqlList As String = String.Empty
        Dim index As Integer
        Dim oItem As Object

        If oList.Count = 1 Then
            sqlList = colName & " = '" & GuidToSQLString(oList(0)) & "'"
        ElseIf oList.Count > 1 Then
            sqlList = colName & " in ( '" & GuidToSQLString(oList(0)) & "'"
            For index = 1 To oList.Count - 1
                sqlList &= ", '" & GuidToSQLString(oList(index)) & "'"
            Next
            sqlList &= " )"
        End If


        Return sqlList
    End Function

    Public Shared Function BuildListSVCSql(oList As ArrayList) As String
        Dim sqlList As String = String.Empty
        Dim index As Integer
        Dim oItem As Object

        If oList.Count = 1 Then
            sqlList = GuidToSQLString(oList(0))
        ElseIf oList.Count > 1 Then
            sqlList = GuidToSQLString(oList(0))
            For index = 1 To oList.Count - 1
                sqlList &= ", " & GuidToSQLString(oList(index))
            Next
        End If
        Return sqlList
    End Function

    Public Shared Function BuildNotInListForSql(colName As String, oList As ArrayList, Optional ByVal isHEXTORAW As Boolean = False) As String
        Dim sqlList As String = String.Empty
        Dim index As Integer
        Dim oItem As Object

        If oList.Count = 1 Then
            sqlList = colName & " <> " & GetDbStringFromGuid(oList(0), isHEXTORAW)
        ElseIf oList.Count > 1 Then
            sqlList = colName & " not in ( " & GetDbStringFromGuid(oList(0), isHEXTORAW)
            For index = 1 To oList.Count - 1
                sqlList &= ", " & GetDbStringFromGuid(oList(index), isHEXTORAW)
            Next
            sqlList &= " )"
        End If

        Return sqlList
    End Function

    Public Shared Function BuildNotInListForNetSql(colName As String, oList As ArrayList) As String
        Dim sqlList As String = String.Empty
        Dim index As Integer
        Dim oItem As Object

        If oList.Count = 1 Then
            sqlList = colName & " <> '" & GuidToSQLString(oList(0)) & "'"
        ElseIf oList.Count > 1 Then
            sqlList = colName & " not in ( '" & GuidToSQLString(oList(0)) & "'"
            For index = 1 To oList.Count - 1
                sqlList &= ", '" & GuidToSQLString(oList(index)) & "'"
            Next
            sqlList &= " )"
        End If


        Return sqlList
    End Function
#End Region

#Region "String Format"

    Public Shared Function ConvertToUpper(source As String) As String
        Dim target As String = source

        If ((source IsNot Nothing) AndAlso (source <> String.Empty)) Then
            target = source.ToUpper
        End If
        Return target
    End Function

    Public Shared Function EmailAddressValidation(email As String) As Boolean

        Dim objRegEx As New System.Text.RegularExpressions.Regex(Reg_Exp, RegularExpressions.RegexOptions.IgnoreCase Or RegularExpressions.RegexOptions.CultureInvariant)
        Dim strToValidate As String = email.Trim(" ".ToCharArray)

        Return objRegEx.IsMatch(strToValidate)

    End Function

    'ALR - Created this conversion class to override the limitation of the StringWriter to only use UTF-16 encoding.
    '      UTF-8 is needed for html and xml files.
    Public Class EncodedStringWriter
        Inherits StringWriter

        'Private property setter
        Private _Encoding As Encoding

        '''<summary>Default constructor for the EncodedStringWriter class.</summary>
        '''<param name=“sb“>The formatted result to output.</param>
        '''<param name=“Encoding“>A member of the System.Text.Encoding class.</param>
        Public Sub New(sb As StringBuilder, Encoding As Encoding)
            MyBase.New(sb)
            _Encoding = Encoding
        End Sub

        '''<summary>Gets the Encoding in which the output is written.</summary>
        '''<param name=“Encoding“>The Encoding in which the output is written.</param>
        '''<remarks>This property is necessary for some XML scenarios where a header must be written containing the encoding used by the StringWriter. This allows the XML code to consume an arbitrary StringWriter and generate the correct XML header.</remarks>
        Public Overrides ReadOnly Property Encoding() As Encoding
            Get
                Return _Encoding
            End Get
        End Property

    End Class

#End Region

#Region "File Management"

#Region "Constants"

    Private Shared FILE_NAME_INVALID_CHARACTERS As String() = {"\", "/", ":", "*", "?", """", "<", ">", "|"} 'Operating system invalid characters

#End Region

    Public Shared Function RemoveInvalidChar(filename As String) As String
        Dim index As Integer
        For index = 0 To FILE_NAME_INVALID_CHARACTERS.Length - 1
            'replace the invalid character with blank
            filename = filename.Replace(FILE_NAME_INVALID_CHARACTERS(index), " ")
        Next
        Return filename
    End Function

    Public Shared Function ReplaceSpaceByUnderscore(filename As String) As String
        Return filename.Replace(" ", "_")
    End Function

    Public Shared Function GetUniqueDirectory(path As String, username As String) As String
        Dim uniqueIdDirectory As String = path & username _
                                 & "_" & RemoveInvalidChar(Date.Now.ToString)
        Return uniqueIdDirectory
    End Function

    Public Shared Function GetUniqueFullPath(path As String, username As String, _
                                            filename As String) As String

        Dim fullFileName As String = path & username & "_" & MiscUtil.RemoveInvalidChar(Date.Now.ToString) _
              & "_" & MiscUtil.RemoveInvalidChar(filename)
        fullFileName = fullFileName.Replace(" ", "_")
        Return fullFileName
    End Function

    Public Shared Function GetModifiedUniqueFullPath(path As String, modifiedName As String, _
                                            filename As String) As String

        Dim fullFileName As String = path & modifiedName & "_" & MiscUtil.RemoveInvalidChar(Date.Now.ToString) _
              & "_" & MiscUtil.RemoveInvalidChar(filename)
        fullFileName = fullFileName.Replace(" ", "_")
        Return fullFileName
    End Function

    'Public Shared Sub FromExcelToCsv(ByVal xlsFullPath As String, ByVal csvFullPath As String)
    '    Dim xl As New Excel.Application
    '    Dim xlwbook As Excel.Workbook

    '    '     Try

    '    xlwbook = xl.Workbooks.Open(xlsFullPath)
    '    xlwbook.SaveAs(csvFullPath, xlwbook.FileFormat.xlCSV)
    '    '   Catch ex As Exception

    '    '  Finally
    '    xlwbook.Close(False, csvFullPath)
    '    xl.Quit()
    '    '     End Try
    'End Sub

    Public Shared Sub CreateFolder(folderName As String)
        Dim objDir As New DirectoryInfo(folderName)
        If Not objDir.Exists() Then
            objDir.Create()
        End If
    End Sub

    Public Shared Sub DeleteFolder(folderName As String)
        Dim objDir As New DirectoryInfo(folderName)
        If objDir.Exists Then
            objDir.Delete(True)
        End If
    End Sub

    Public Shared Function FromStringToStream(source As String) As Stream
        '  Dim count As Integer
        '   Dim byteArray As Byte()
        '  Dim charArray As Char()
        '  Dim uniEncoding As New UnicodeEncoding()
        ' Dim uniEncoding As New UTF8Encoding
        Dim uniEncoding As Encoding
        uniEncoding = Encoding.GetEncoding(ENCODING_LATIN)

        ' Create the data to write to the stream.
        Dim sourceString As Byte() = _
            uniEncoding.GetBytes(source)

        Dim memStream As New MemoryStream(sourceString.Length)
        '   Try
        ' Write the source string to the stream.
        memStream.Write(sourceString, 0, sourceString.Length)

        ' Write the second string to the stream, byte by byte.
        'count = 0
        'While (count < secondString.Length)
        '    memStream.WriteByte(secondString(count))
        '    count += 1
        'End While

        ' Write the stream properties to the console.
        'Console.WriteLine( _
        '    "Capacity = {0}, Length = {1}, Position = {2}", _
        '    memStream.Capacity.ToString(), _
        '    memStream.Length.ToString(), _
        '    memStream.Position.ToString())

        ' Set the stream position to the beginning of the stream.
        '    memStream.Seek(0, SeekOrigin.Begin)

        ' Read the first 20 bytes from the stream.
        'byteArray = _
        '    New Byte(CType(memStream.Length, Integer)) {}
        'count = memStream.Read(byteArray, 0, memStream.Length)

        '' Read the remaining Bytes, Byte by Byte.
        'While (count < memStream.Length)
        '    byteArray(count) = _
        '        Convert.ToByte(memStream.ReadByte())
        '    count += 1
        'End While

        ' Decode the Byte array into a Char array 
        ' and write it to the console.
        'charArray = _
        '    New Char(uniEncoding.GetCharCount( _
        '    byteArray, 0, count)) {}
        'uniEncoding.GetDecoder().GetChars( _
        '    byteArray, 0, count, charArray, 0)
        'Console.WriteLine(charArray)
        '  Finally
        ' memStream.Close()
        ' End Try
        Return memStream

    End Function

    Public Shared Sub DownloadFileToWebServer(webServerPath As String, fileNameFullPath As String, _
                                              fileLen As Integer, objStream As System.IO.Stream)
        Dim fileBytes(fileLen - 1) As Byte

        objStream.Read(fileBytes, 0, fileLen)
        CreateFolder(webServerPath)
        File.WriteAllBytes(fileNameFullPath, fileBytes)
        objStream.Close()
    End Sub

#End Region

#Region "Ftp"

    Public Shared Sub SendFileToUnix(inputStream As Stream, targetFileName As String)
        Dim unixPath As String = AppConfig.UnixServer.FtpDirectory
        Dim objUnixFTP As New aFtp(AppConfig.UnixServer.HostName, unixPath, AppConfig.UnixServer.UserId, _
                                         AppConfig.UnixServer.Password, PORT)
        Try
            If (objUnixFTP.Login()) Then
                objUnixFTP.UploadFile(inputStream, targetFileName, False)
            End If
        Catch ex As Exception
            Throw ex
        Finally
            objUnixFTP.CloseConnection()
            inputStream.Close()
        End Try
    End Sub

    Public Shared Sub SendFileToUnix(targetFileNameFullPath As String)
        Dim objUnixFTP As New sFtp(AppConfig.UnixServer.HostName, AppConfig.UnixServer.FtpDirectory, AppConfig.UnixServer.UserId, AppConfig.UnixServer.Password)

        objUnixFTP.UploadFile(targetFileNameFullPath)
    End Sub

#End Region

End Class
