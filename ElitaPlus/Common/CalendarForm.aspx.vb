'/*-----------------------------------------------------------------------------------------------------------------

'  AA      SSS  SSS  UU  UU RRRRR      AA    NN   NN  TTTTTTTT
'A    A   SS    SS   UU  UU RR   RR  A    A  NNN  NN     TT 
'AAAAAA   SSS   SSS  UU  UU RRRR     AAAAAA  NN N NN     TT
'AA  AA     SS    SS UU  UU RR RR    AA  AA  NN  NNN     TT
'AA  AA  SSSSS SSSSS  UUUU  RR   RR  AA  AA  NN  NNN     TT

'Copyright 2004, Assurant Group Inc..  All Rights Reserved.
'------------------------------------------------------------------------------
'This information is CONFIDENTIAL and for Assurant Group's exclusive use ONLY.
'Any reproduction or use without Assurant Group's explicit, written consent 
'is PROHIBITED.
'------------------------------------------------------------------------------

'Purpose: Calendar tool to choose dates
'
'Author:  Dagoberto Cabrera
'
'Date:    02/02/2004     

'MODIFICATION HISTORY:
'
'===========================================================================================

Imports System
Imports System.Globalization

Partial Class CalendarForm
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

        ' sets the corresponding text field's value to the selected date from the calendar
        If fieldNameParam.IndexOf(":") < 0 Then
            'sJscript &= "    window.opener.document." & HttpContext.Current.Request.QueryString("formname") & ".value = '" & _
            '  ElitaPlusPage.GetDateFormattedString(Me.MyCalendar.SelectedDate) & "';" & Environment.NewLine
            'sJscript &= "window.opener.document." & HttpContext.Current.Request.QueryString("formname") & ".fireEvent('onchange')" & Environment.NewLine
            If Not setDateTime Is Nothing AndAlso setDateTime = "Y" Then
                sJscript &= " window.opener.document.getElementById('" & HttpContext.Current.Request.QueryString("formname") & "').value = '" &
                        ElitaPlusPage.GetDateFormattedString(Me.MyCalendar.SelectedDate) & " " & DateTime.Now.ToString("HH:mm:ss") & "';" & Environment.NewLine
            Else
                sJscript &= " window.opener.document.getElementById('" & HttpContext.Current.Request.QueryString("formname") & "').value = '" &
                        ElitaPlusPage.GetDateFormattedString(Me.MyCalendar.SelectedDate) & "';" & Environment.NewLine
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
                  ElitaPlusPage.GetDateFormattedString(Me.MyCalendar.SelectedDate) & "';" & Environment.NewLine
            End If
        End If

        sJscript &= "window.close();" & Environment.NewLine

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
            'vcp commented out the original code because it will cause exception if the year is beyond the range.
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

#End Region

    Protected Sub MyCalendar_DayRender(sender As Object, e As DayRenderEventArgs) Handles MyCalendar.DayRender
        Dim disablePreviousDates As String = HttpContext.Current.Request.QueryString("disablePreviousDates")
        If disablePreviousDates = "Y" Then
            If e.Day.Date < DateTime.Now.Date Then
                e.Day.IsSelectable = False
                e.Cell.ForeColor = Color.Gray
            End If
        End If
    End Sub
End Class
