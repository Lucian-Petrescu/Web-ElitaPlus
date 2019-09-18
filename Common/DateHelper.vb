Imports System.Configuration
Imports System.Globalization
Imports System.Reflection
Imports System.Threading

Public NotInheritable Class DateHelper

    Private Const DATE_FORMAT As String = "dd-MMM-yyyy"
    Private Const DATE_FORMAT_NONBUSINESS As String = "MMM-dd-yyyy"
    Private Const DATE_TIME_FORMAT As String = "dd-MMM-yyyy HH:mm:ss"
    Private Const DATE_TIME_FORMAT_12 As String = "dd-MMM-yyyy hh:mm:ss tt"

    Public Shared Function GetDateValue(ByVal inputDate As String) As DateTime
        Dim strChkDateFormat As String = String.Empty
        Dim dt As DateTime
        Try
            If (inputDate.Length > DATE_TIME_FORMAT.Length) Then ' There are some places where value is set as DateType but page passes value as dd-MMM-yyyy 00:00:00.
                strChkDateFormat = DATE_TIME_FORMAT_12
                dt = DateTime.Parse(inputDate)
            ElseIf (inputDate.Length > DATE_FORMAT.Length) Then
                strChkDateFormat = DATE_TIME_FORMAT
                dt = DateTime.Parse(inputDate)
            Else
                If Thread.CurrentThread.CurrentCulture.ToString() = "ja-JP" Then
                    strChkDateFormat = convertDateFrmt(inputDate)
                    dt = Date.Parse(strChkDateFormat)
                Else
                    strChkDateFormat = DATE_FORMAT
                    dt = Date.Parse(inputDate)
                End If

            End If
        Catch ex As Exception
            DateTime.TryParseExact(inputDate, strChkDateFormat, System.Threading.Thread.CurrentThread.CurrentCulture, Globalization.DateTimeStyles.None, dt)
        End Try
        Return dt
    End Function

    Public Shared Function convertDateFrmt(txtDate As String) As String
        If Not (String.IsNullOrEmpty(txtDate)) Then
            If (CultureInfo.CurrentCulture.Name.Equals("ja-JP")) Then
                Dim parsedDate As DateTime
                parsedDate = DateTime.ParseExact(txtDate, "dd-M-yyyy", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
                txtDate = parsedDate.ToString("dd-MMM-yyyy", CultureInfo.CreateSpecificCulture("en-US"))
                Return txtDate
            End If
        End If
        Return txtDate
    End Function

    Public Shared Function GetDateInMonddyyyy(ByVal inputDate As String) As DateTime
        Dim strChkDateFormat As String = String.Empty
        Dim dt As DateTime
        Try
            strChkDateFormat = DATE_FORMAT_NONBUSINESS
            dt = Date.Parse(inputDate)
        Catch ex As Exception
            DateTime.TryParseExact(inputDate, strChkDateFormat, System.Threading.Thread.CurrentThread.CurrentCulture, Globalization.DateTimeStyles.None, dt)
        End Try
        Return dt
    End Function

    Public Shared Function GetFormattedDate(ByVal inputDate As String, ByVal strChkDateFormat As String) As DateTime
        Dim dt As DateTime
        Try
            dt = Date.Parse(inputDate)
        Catch ex As Exception
            DateTime.TryParseExact(inputDate, strChkDateFormat, System.Threading.Thread.CurrentThread.CurrentCulture, Globalization.DateTimeStyles.None, dt)
        End Try
        Return dt
    End Function

    Public Shared Function GetEnglishDate(ByVal dt As Date) As String
        Dim strDay As String
        Dim strMonth As String
        Dim strYear As String
        Dim strDateString As String
        If (Not dt = DateTime.MinValue) Then
            strDay = Microsoft.VisualBasic.Day(dt).ToString()
            If (Microsoft.VisualBasic.Strings.Len(strDay) < 2) Then
                strDay = "0" + strDay
            End If
            Select Case Microsoft.VisualBasic.Month(dt)
                Case 1
                    strMonth = "Jan"
                Case 2
                    strMonth = "Feb"
                Case 3
                    strMonth = "Mar"
                Case 4
                    strMonth = "Apr"
                Case 5
                    strMonth = "May"
                Case 6
                    strMonth = "Jun"
                Case 7
                    strMonth = "Jul"
                Case 8
                    strMonth = "Aug"
                Case 9
                    strMonth = "Sep"
                Case 10
                    strMonth = "Oct"
                Case 11
                    strMonth = "Nov"
                Case 12
                    strMonth = "Dec"
            End Select

            strYear = Microsoft.VisualBasic.Year(dt).ToString()
            strDateString = strDay + "-" + strMonth + "-" + strYear

            Return strDateString

        Else
            Return dt.ToString()
        End If
    End Function

    Public Shared Function IsDate(ByVal objVal As Object) As Boolean
        Dim blnDt As Boolean
        Dim strVal As String = String.Empty

        If (objVal Is Nothing OrElse objVal Is DBNull.Value) Then
            Return False
        End If

        strVal = objVal.ToString()
        blnDt = Microsoft.VisualBasic.IsDate(strVal)
        If (blnDt) Then
            Return True
        Else
            Dim dtVal As DateTime = GetDateValue(strVal)
            If (dtVal = DateTime.MinValue) Then
                Return False
            Else
                Return True
            End If
        End If

    End Function


End Class
