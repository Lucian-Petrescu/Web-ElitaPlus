
Imports System
Imports System.Globalization

Public Class CalendarWithtimeForm
    Inherits System.Web.UI.Page
#Region " Members "

#Region " Controls "
    ' labels
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label

    ' drop down lists

    ' literal

#End Region

#Region " Constants "
    Private Const PREVIOUS_YEARS_COUNT As Int32 = 125
    Private Const TWENTYFOUR_HOUR As String = "00,01,02,03,04,05,06,07,08,09,10,11,12,13,14,15,16,17,18,19,20,21,22,23"
    Private Const TWELVE_HOUR As String = "01,02,03,04,05,06,07,08,09,10,11,12"
    Private Const MINUTE As String = "00,01,02,03,04,05,06,07,08,09,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50,51,52,53,54,55,56,57,58,59"
    Private Const SECOND As String = "00,01,02,03,04,05,06,07,08,09,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50,51,52,53,54,55,56,57,58,59"
#End Region

#End Region

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region " Form Related "

    ' *************************************************************************** '
    '   Sub Page_Load: User code to initialize the page
    ' *************************************************************************** '
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Dim dateParam As String
        Dim sOpenerButton As String
        Dim sJavaScript As String
        Dim fieldNameParam As String

        fieldNameParam = HttpContext.Current.Request.QueryString("formname")

        sOpenerButton = fieldNameParam.Replace("Form1.", "")

        sJavaScript = "<SCRIPT>" & Environment.NewLine
        sJavaScript &= "StartParentWindowChecking('" & sOpenerButton & "');" & Environment.NewLine
        sJavaScript &= "</SCRIPT>" & Environment.NewLine

        Page.RegisterClientScriptBlock("CheckIfParentOpen", sJavaScript)

        If Not IsPostBack Then
            loadTimeDropdowns()
            dateParam = HttpContext.Current.Request.QueryString("initdate")
            'ARF to correct problems with invalid input Dates

            If (DateHelper.IsDate(dateParam) = False) Then
                dateParam = Nothing
            End If

            If Not dateParam Is Nothing AndAlso dateParam <> "" Then
                Me.MyCalendar.SelectedDate = DateHelper.GetDateValue(dateParam)
                Me.MyCalendar.VisibleDate = Me.MyCalendar.SelectedDate
            End If

            LoadMonthYear(dateParam)
            TranslatePageLabels()
        End If
    End Sub

    Public Sub TranslatePageLabels()
        Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
        Me.TranslateControl(Me.LabelMonth, langId)
        Me.TranslateControl(Me.LabelYear, langId)
        Me.TranslateControl(Me.lblhour, langId)
        Me.TranslateControl(Me.lblminute, langId)
        Me.TranslateControl(Me.lblsecond, langId)
    End Sub

    Public Function TranslateLabelOrMessage(ByVal UIProgCode As String, ByVal LangId As Guid) As String
        Dim TransProcObj As New TranslationProcess
        Dim oTranslationItem As New TranslationItem
        Dim Coll As New TranslationItemArray
        With oTranslationItem
            .TextToTranslate = UIProgCode.ToUpper
        End With
        Coll.Add(oTranslationItem)
        TransProcObj.TranslateList(Coll, LangId)
        Return oTranslationItem.Translation
    End Function

    Public Sub TranslateControl(ByVal Control As WebControl, ByVal LangId As Guid)
        Dim ControlType As Type = Control.GetType
        Dim propInfo As System.Reflection.PropertyInfo = ControlType.GetProperty("Text", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Public)
        If Not propInfo Is Nothing Then
            Dim originalValue As String = CType(propInfo.GetValue(Control, Nothing), String)
            If Not originalValue Is Nothing Then
                Dim newValue As String = TranslateLabelOrMessage(originalValue, LangId)
                propInfo.SetValue(Control, newValue, Nothing)
            End If
        End If
    End Sub


    ' *************************************************************************** '
    '   Sub MyCalendar_SelectionChanged: Handles the selection change event of the calendar
    ' *************************************************************************** '
    Private Sub MyCalendar_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyCalendar.SelectionChanged

        Dim sJscript As String
        Dim fieldNameParam As String
        Dim setDateTime As String
        Dim timeSeperator As String = LocalizationMgr.CurrentCulture.DateTimeFormat.TimeSeparator
        fieldNameParam = HttpContext.Current.Request.QueryString("formname")
        setDateTime = HttpContext.Current.Request.QueryString("setDateTime")

        sJscript = "<script language=""javascript"">" & Environment.NewLine

        ' check to see if calendar caller is time sheet
        If Request.QueryString("caller") = "ts" Then
            ' need to get the previous sat date from selected date

            Dim day As DayOfWeek = Me.MyCalendar.SelectedDate().DayOfWeek
            Dim dSelStartDate As Date
            Dim i As Int32 = 1

            While day <> DayOfWeek.Saturday
                dSelStartDate = Me.MyCalendar.SelectedDate().AddDays(-i)
                day = dSelStartDate.DayOfWeek
                i += 1
            End While

            If Not dSelStartDate = #12:00:00 AM# Then
                Me.MyCalendar.SelectedDate() = dSelStartDate
            Else
                Me.MyCalendar.SelectedDate() = Me.MyCalendar.SelectedDate()
            End If

        End If
        Dim timeString As String
        If Not LocalizationMgr.CurrentCulture.DateTimeFormat.ShortTimePattern.Contains("H") Then
            '12 hour time
            timeString = ddlhour.SelectedItem.Text & timeSeperator & ddlMinutes.SelectedItem.Text & timeSeperator & ddlSeconds.SelectedItem.Text & " " & ddlampm.SelectedItem.Text
        Else
            '24 hour time
            timeString = ddlhour.SelectedItem.Text & timeSeperator & ddlMinutes.SelectedItem.Text & timeSeperator & ddlSeconds.SelectedItem.Text
        End If

        ' sets the corresponding text field's value to the selected date from the calendar
        If fieldNameParam.IndexOf(":") < 0 Then
            'sJscript &= "    window.opener.document." & HttpContext.Current.Request.QueryString("formname") & ".value = '" & _
            '  ElitaPlusPage.GetDateFormattedString(Me.MyCalendar.SelectedDate) & "';" & Environment.NewLine
            'sJscript &= "window.opener.document." & HttpContext.Current.Request.QueryString("formname") & ".fireEvent('onchange')" & Environment.NewLine
            If Not setDateTime Is Nothing AndAlso setDateTime = "Y" Then
                sJscript &= "    window.opener.document.getElementById('" & HttpContext.Current.Request.QueryString("formname") & "').value = '" & _
                        ElitaPlusPage.GetDateFormattedString(Me.MyCalendar.SelectedDate) & " " & DateTime.Now.ToString("HH:mm:ss") & "';" & Environment.NewLine
            Else
                sJscript &= "    window.opener.document.getElementById('" & HttpContext.Current.Request.QueryString("formname") & "').value = '" & _
                        ElitaPlusPage.GetDateFormattedString(Me.MyCalendar.SelectedDate) & " " & timeString & "';" & Environment.NewLine
            End If

            'sJscript &= "window.opener.document.getElementById('" & HttpContext.Current.Request.QueryString("formname") & "').fireEvent('onchange')" & Environment.NewLine
            sJscript &= " var element = window.opener.document.getElementById('" & HttpContext.Current.Request.QueryString("formname") & "'); if(element.onchange) { element.onchange();} " & Environment.NewLine

        Else
            fieldNameParam = fieldNameParam.Replace(":", "_")

            If Not setDateTime Is Nothing AndAlso setDateTime = "Y" Then
                sJscript &= "    window.opener.document.getElementById('" & fieldNameParam & "').value = '" & _
                  ElitaPlusPage.GetDateFormattedString(Me.MyCalendar.SelectedDate) & " " & DateTime.Now.ToString("HH:mm:ss") & "';" & Environment.NewLine
            Else
                sJscript &= "    window.opener.document.getElementById('" & fieldNameParam & "').value = '" & _
                  ElitaPlusPage.GetDateFormattedString(Me.MyCalendar.SelectedDate) & " " & timeString & "';" & Environment.NewLine
            End If
        End If

        sJscript &= "    window.close();" & Environment.NewLine

        If Request.QueryString("caller") = "ts" Then
            sJscript &= "    window.opener.document.forms[0].submit();" & Environment.NewLine
        End If

        sJscript &= "</script>"

        'Set the literal control's text to the JScript code
        Me.Literal1.Text = sJscript

    End Sub

    ' ************************************************************************************ '
    '   Sub ValidateYear: If the Year is bigger than Today +(4 * Me.PREVIOUS_YEARS_COUNT)
    '                         Indicates the error and reset to Today Year
    '                       Otherwise, it sets the selected year
    ' ************************************************************************************ '
    Private Sub ValidateYear(ByVal oYear As String)
        Dim yearItem As ListItem = Me.cboYearList.Items.FindByText(oYear)
        If Not yearItem Is Nothing Then
            ' The year is OK
            yearItem.Selected = True
        Else
            ' The Year is incorrect
            ' It Will Send the error Message
            Dim sJavaScript As String
            Dim sMsg As String = Me.TranslateLabelOrMessage(Message.MSG_INVALID_DATE, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            sJavaScript = "<SCRIPT>" & Environment.NewLine
            sJavaScript &= "showMessageWithSubmit('" & sMsg & "', '" & sMsg & "', '" & ElitaPlusPage.MSG_BTN_OK & "', '" & ElitaPlusPage.MSG_TYPE_ALERT & "');" & Environment.NewLine
            sJavaScript &= "</SCRIPT>" & Environment.NewLine
            Me.RegisterStartupScript("ShowConfirmation", sJavaScript)
            ' It will set today's year
            Me.cboYearList.Items.FindByText(Date.Today.Year.ToString( _
                                            LocalizationMgr.CurrentFormatProvider)).Selected = True
        End If
    End Sub
    ' *************************************************************************** '
    '   Sub MyCalendar_VisibleMonthChanged: Handles the visible month change event
    '                                       of the calendar and sets the dropdown lists
    ' *************************************************************************** '
    Private Sub MyCalendar_VisibleMonthChanged(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.MonthChangedEventArgs) Handles MyCalendar.VisibleMonthChanged
        SetMonthYear(MyCalendar.VisibleDate.ToString("MMM", LocalizationMgr.CurrentFormatProvider), MyCalendar.VisibleDate.Year.ToString(LocalizationMgr.CurrentFormatProvider))
    End Sub

    ' *************************************************************************** '
    '   Sub LoadMonthYear: Loads the dropdown lists and sets the default values
    ' *************************************************************************** '
    Private Sub LoadMonthYear(ByVal selectedDate As String)

        Dim i As Int32 = 0
        Dim strMonth As String = ""
        Dim dCurrDate As DateTime = #1/1/1995#
        Dim myCal As Calendar = LocalizationMgr.CurrentCulture.Calendar()

        For i = Date.Today.Year - Me.PREVIOUS_YEARS_COUNT To Date.Today.Year + (4 * Me.PREVIOUS_YEARS_COUNT)
            Me.cboYearList.Items.Add(New ListItem(i.ToString(LocalizationMgr.CurrentFormatProvider), i.ToString(LocalizationMgr.CurrentFormatProvider)))
        Next

        If Not selectedDate Is Nothing AndAlso selectedDate <> "" Then
           ValidateYear(DateHelper.GetDateValue(selectedDate).Year.ToString())
        Else
            Me.cboYearList.Items.FindByText(Date.Today.Year.ToString(LocalizationMgr.CurrentFormatProvider)).Selected = True
        End If

        For i = 1 To 12
            strMonth = dCurrDate.ToString("MMM", LocalizationMgr.CurrentFormatProvider)
            dCurrDate = myCal.AddMonths(dCurrDate, 1)
            cboMonthList.Items.Add(New ListItem(strMonth, i.ToString(LocalizationMgr.CurrentFormatProvider)))
        Next i

        If Not selectedDate Is Nothing AndAlso selectedDate <> "" Then
            Me.cboMonthList.Items.FindByText(DateHelper.GetDateValue(selectedDate).ToString("MMM", LocalizationMgr.CurrentFormatProvider)).Selected = True
        Else
            Me.cboMonthList.Items.FindByText(Date.Today.ToString("MMM", LocalizationMgr.CurrentFormatProvider)).Selected = True
        End If
        If Not selectedDate Is Nothing AndAlso selectedDate <> "" Then
            Me.ddlhour.Items.FindByText(DateHelper.GetDateValue(selectedDate).ToString("hh", LocalizationMgr.CurrentFormatProvider)).Selected = True
        Else
            Me.ddlhour.Items.FindByText(Date.Now.ToString("hh", LocalizationMgr.CurrentFormatProvider)).Selected = True
        End If
        If Not selectedDate Is Nothing AndAlso selectedDate <> "" Then
            Me.ddlMinutes.Items.FindByText(DateHelper.GetDateValue(selectedDate).ToString("mm", LocalizationMgr.CurrentFormatProvider)).Selected = True
        Else
            Me.ddlMinutes.Items.FindByText(Date.Now.ToString("mm", LocalizationMgr.CurrentFormatProvider)).Selected = True
        End If
        If Not selectedDate Is Nothing AndAlso selectedDate <> "" Then
            Me.ddlSeconds.Items.FindByText(DateHelper.GetDateValue(selectedDate).ToString("ss", LocalizationMgr.CurrentFormatProvider)).Selected = True
        Else
            Me.ddlSeconds.Items.FindByText(Date.Now.ToString("ss", LocalizationMgr.CurrentFormatProvider)).Selected = True
        End If
        If LocalizationMgr.CurrentCulture.DateTimeFormat.LongTimePattern.Contains("tt") Then
            If Not selectedDate Is Nothing AndAlso selectedDate <> "" Then
                Me.ddlampm.Items.FindByText(DateHelper.GetDateValue(selectedDate).ToString("tt", LocalizationMgr.CurrentFormatProvider)).Selected = True
            Else
                Me.ddlampm.Items.FindByText(Date.Now.ToString("tt", LocalizationMgr.CurrentFormatProvider)).Selected = True
            End If
        End If
        SetNewVisibleDate(cboMonthList.SelectedItem.Text, cboYearList.SelectedItem.Text)


    End Sub

    ' *************************************************************************** '
    '   Sub SetMonthYear: Sets the month and year dropdown values to the calendar values
    ' *************************************************************************** '
    Private Sub SetMonthYear(ByVal month As String, ByVal year As String)
        Me.cboYearList.SelectedIndex = -1
        Me.cboMonthList.SelectedIndex = -1

        ValidateYear(year)

        '  Me.cboYearList.Items.FindByText(year).Selected = True
        Me.cboMonthList.Items.FindByText(month).Selected = True
        SetNewVisibleDate(cboMonthList.SelectedItem.Text, cboYearList.SelectedItem.Text)
    End Sub

    ' *************************************************************************** '
    '   Sub MonthSelected: Handles the select change event of the month dropdown and
    '                      sets the visible month of the calendar
    ' *************************************************************************** '
    Private Sub MonthSelected(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboMonthList.SelectedIndexChanged
        Dim strSelMonth As String = cboMonthList.SelectedItem.Text
        SetNewVisibleDate(strSelMonth, cboYearList.SelectedItem.Text)
    End Sub

    ' *************************************************************************** '
    '   Sub YearSelected: Handles the select change event of the year dropdown and
    '                     sets the visible year of the calendar
    ' *************************************************************************** '
    Private Sub YearSelected(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboYearList.SelectedIndexChanged
        Dim strSelYear As String = cboYearList.SelectedItem.Text
        SetNewVisibleDate(cboMonthList.SelectedItem.Text, strSelYear)
    End Sub

    ' *************************************************************************** '
    '   Sub SetNewVisibleDate: Sets the visible date of the calendar
    ' *************************************************************************** '
    Private Sub SetNewVisibleDate(ByVal month As String, ByVal year As String)
        Dim sNewDate As Date
        Dim strDateVal As String
        Try
            If (month.ToUpper() = "MAR") Then
                month = "03"  ' es-CL , es-AR etc. represent march month differently and it fails in case culture is spanish and user selects march month
            End If
            strDateVal = month & "/" & year
            sNewDate = CType(strDateVal, Date)
        Catch ex As Exception
            DateTime.TryParseExact(strDateVal, "MMM/YYYY", System.Threading.Thread.CurrentThread.CurrentCulture, Globalization.DateTimeStyles.None, sNewDate)
        End Try
        MyCalendar.VisibleDate = sNewDate
    End Sub

    ' *************************************************************************** '
    '   Sub loadTimeDropdowns: loads values in time drop downs for hour, minute and secod
    ' *************************************************************************** '
    Private Sub loadTimeDropdowns()

        'check if the time format is 12 hour or 24 hour
        Dim twelvehour As Boolean = True
        If LocalizationMgr.CurrentCulture.DateTimeFormat.ShortTimePattern.Contains("H") Then
            twelvehour = False
        End If

        Dim splitchar As Char = ","c
        If Not twelvehour Then
            For Each str As String In TWENTYFOUR_HOUR.Split(splitchar)
                ddlhour.Items.Add(New ListItem(str, str))
            Next
            ddlampm.Visible = False
        ElseIf twelvehour Then
            For Each str As String In TWELVE_HOUR.Split(splitchar)
                ddlhour.Items.Add(New ListItem(str, str))
            Next
            ddlampm.Visible = True
        End If
        For Each str As String In MINUTE.Split(splitchar)
            ddlMinutes.Items.Add(New ListItem(str, str))
        Next
        For Each str As String In SECOND.Split(splitchar)
            ddlSeconds.Items.Add(New ListItem(str, str))
        Next

        Dim str1 As String = LocalizationMgr.CurrentCulture.DateTimeFormat.AMDesignator
        Dim str2 As String = LocalizationMgr.CurrentCulture.DateTimeFormat.PMDesignator
        ddlampm.Items.Add(New ListItem(str1, str1))
        ddlampm.Items.Add(New ListItem(str2, str2))
    End Sub


#End Region
End Class




