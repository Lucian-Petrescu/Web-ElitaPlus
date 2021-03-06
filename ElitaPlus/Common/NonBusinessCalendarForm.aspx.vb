Imports System.Globalization

Partial Class NonBusinessCalendarForm
    Inherits ElitaPlusPage

#Region " Members "


#Region "Page State"
    Class MyState
        Public MyBO As NonbusinessCalendar
        Public nonBusinessDayDV As DataView = Nothing
        Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
        Public HasDataChanged As Boolean
        Public LastErrMsg As String
    End Class

    Public Sub New()
        MyBase.New(New MyState)
    End Sub

    Protected Shadows ReadOnly Property State() As MyState
        Get
            Return CType(MyBase.State, MyState)
        End Get
    End Property

#End Region


#Region " Controls "
    ' labels
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents ErrorCtrl As ErrorController
    Protected WithEvents btnBack As System.Web.UI.WebControls.Button
    Protected WithEvents btnUndo_WRITE As System.Web.UI.WebControls.Button
    Protected WithEvents checkDates As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents uncheckDates As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents isBtnBackClicked As System.Web.UI.HtmlControls.HtmlInputHidden

    Protected checkDatesArray() As String
    Protected uncheckDatesArray() As String

#End Region

#Region " Constants "
    Private Const PREVIOUS_YEARS_COUNT As Int32 = 125
    Public Const MMM_DD_YYYY As String = "MMM-dd-yyyy"
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

#Region "Page Return Type"
    Public Class ReturnType
        Public LastOperation As DetailPageCommand
        Public EditingBo As NonbusinessCalendar
        Public HasDataChanged As Boolean

        Public Sub New(ByVal LastOp As DetailPageCommand, ByVal curEditingBo As NonbusinessCalendar, ByVal hasDataChanged As Boolean)
            Me.LastOperation = LastOp
            Me.EditingBo = curEditingBo
            Me.HasDataChanged = hasDataChanged
        End Sub
    End Class
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

        Me.ErrorCtrl.Clear_Hide()
        fieldNameParam = "NonBusinessCalendarForm" 'HttpContext.Current.Request.QueryString("formname")
        sOpenerButton = fieldNameParam.Replace("Form1.", "")

        'sJavaScript = "<SCRIPT>" & Environment.NewLine
        'sJavaScript &= "StartParentWindowChecking('" & sOpenerButton & "');" & Environment.NewLine
        'sJavaScript &= "</SCRIPT>" & Environment.NewLine

        'Page.RegisterClientScriptBlock("CheckIfParentOpen", sJavaScript)

        If Not IsPostBack Then

            dateParam = HttpContext.Current.Request.QueryString("initdate")

            If (DateHelper.IsDate(dateParam) = False) Then
                dateParam = Nothing
            End If

            If Not dateParam Is Nothing AndAlso dateParam <> "" Then
                Me.MyCalendar.SelectedDate = DateHelper.GetDateValue(dateParam)
                Me.MyCalendar.VisibleDate = Me.MyCalendar.SelectedDate
            End If

            LoadMonthYear(dateParam)
            TranslatePageLabels()
            LoadNonBusinessCalender()
        End If

        InitDates()
        CheckIfComingFromSaveConfirm()

    End Sub

#Region "Member Function"

    Public Sub InitDates()
        If (Not checkDates.Value Is Nothing) Then
            checkDatesArray = checkDates.Value.Split("|"c)
        End If

        If (Not uncheckDates.Value Is Nothing) Then
            uncheckDatesArray = uncheckDates.Value.Split("|"c)
        End If

    End Sub

    Public Sub ResetDates()
        checkDates.Value = ""
        uncheckDates.Value = ""
    End Sub

    Private Sub LoadNonBusinessCalender()
        Try
            Me.State.nonBusinessDayDV = NonbusinessCalendar.LoadList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try

    End Sub

    Public Function IsChecked(ByVal dateStr As String) As Boolean
        Dim retVar As Boolean = False
        Dim enCulture As New CultureInfo("en-us")
        Dim dt As DateTime = DateHelper.GetDateInMonddyyyy(dateStr)
        dateStr = dt.ToString(Me.MMM_DD_YYYY, enCulture)

        If Not Me.State.nonBusinessDayDV Is Nothing Then
            Me.State.nonBusinessDayDV.RowFilter = "NonBusiness_Date = '" & dateStr & "'"
            If Me.State.nonBusinessDayDV.Count >= 1 Then
                retVar = True
            Else
                retVar = False
            End If
        End If

        Return retVar

    End Function

    Public Function GetNonbusinessCalendarID(ByVal dateStr As String) As Guid
        Dim retVar As Guid = Guid.Empty

        If Not Me.State.nonBusinessDayDV Is Nothing Then
            Me.State.nonBusinessDayDV.RowFilter = "NonBusiness_Date = '" & dateStr & "'"
            If Me.State.nonBusinessDayDV.Count >= 1 Then
                retVar = GuidControl.ByteArrayToGuid(Me.State.nonBusinessDayDV(0).Item("nonbusiness_calendar_id"))
            End If
        End If

        Return retVar

    End Function

    Protected Sub CheckIfComingFromSaveConfirm()
        Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value
        If Not confResponse Is Nothing AndAlso confResponse = Me.CONFIRM_MESSAGE_OK Then
            If Me.State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr Then
                Me.SaveNonBusinessCalendar()
            End If
            Select Case Me.State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    NavAction()
                Case ElitaPlusPage.DetailPageCommand.New_
                    Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                    Me.SaveNonBusinessCalendar()
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    NavAction()
            End Select
        ElseIf Not confResponse Is Nothing AndAlso confResponse = Me.CONFIRM_MESSAGE_CANCEL Then
            Select Case Me.State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    NavAction()
                Case ElitaPlusPage.DetailPageCommand.New_
                    Me.SaveNonBusinessCalendar()
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    Me.ErrorCtrl.AddErrorAndShow(Me.State.LastErrMsg)
            End Select
        End If
        'Clean after consuming the action
        Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
        Me.HiddenSaveChangesPromptResponse.Value = ""
    End Sub

    Private Sub SaveNonBusinessCalendar()
        Dim i As Int32
        Dim enCulture As New CultureInfo("en-us")

        For i = 0 To checkDatesArray.Length - 1
            If (Not checkDatesArray(i) Is Nothing And checkDatesArray(i) <> "") Then
                Dim dt As DateTime = DateHelper.GetDateInMonddyyyy(checkDatesArray(i))
                Dim dateStr As String = dt.ToString(Me.MMM_DD_YYYY, enCulture)
                Dim id As Guid = GetNonbusinessCalendarID(dateStr)

                If id = Guid.Empty Then
                    Dim Calendar As New NonbusinessCalendar(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id, dateStr)
                    Calendar.Save()
                End If
            End If
        Next

        For i = 0 To uncheckDatesArray.Length - 1
            If (Not uncheckDatesArray(i) Is Nothing And uncheckDatesArray(i) <> "") Then
                Dim dt As DateTime = DateHelper.GetDateInMonddyyyy(uncheckDatesArray(i))
                Dim dateStr As String = dt.ToString(Me.MMM_DD_YYYY, enCulture)
                Dim id As Guid = GetNonbusinessCalendarID(dateStr)

                If Not id = Guid.Empty Then
                    Dim Calendar As New NonbusinessCalendar(id)
                    Calendar.Delete()
                    Calendar.Save()
                End If
            End If
        Next

        ResetDates()
        LoadNonBusinessCalender()

    End Sub

    Function IsPageDirty() As Boolean
        Return (checkDates.Value <> "" Or uncheckDates.Value <> "")
    End Function

    Private Sub ConfirmationCheck()
        Try
            If IsPageDirty() Then
                Me.AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
            Else
                NavAction()
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
            Me.AddConfirmMsg(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, Me.HiddenSaveChangesPromptResponse)
            Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
            Me.State.LastErrMsg = Me.ErrorCtrl.Text
        End Try
    End Sub

    Private Sub NavAction()
        If Not isBtnBackClicked Is Nothing And isBtnBackClicked.Value = "Y" Then
            Me.ReturnToTabHomePage()
        Else
            ResetDates()
        End If
    End Sub

#End Region

#Region "Button Click"

    Private Sub btnSave_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave_WRITE.Click
        Try
            If Me.IsPageDirty Then
                SaveNonBusinessCalendar()
                Me.DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
            Else
                Me.DisplayMessage(Message.MSG_RECORD_NOT_SAVED, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        isBtnBackClicked.Value = "Y"
        ConfirmationCheck()
    End Sub

    Private Sub btnUndo_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUndo_WRITE.Click
        ResetDates()
    End Sub

#End Region


#Region "Calendar Related"

    Public Sub TranslatePageLabels()
        Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
        Me.TranslateControlByPropertyInfo(Me.LabelMonth, langId)
        Me.TranslateControlByPropertyInfo(Me.LabelYear, langId)
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

    Private Sub TranslateControlByPropertyInfo(ByVal Control As WebControl, ByVal LangId As Guid)
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
    Protected Sub MyCalendar_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyCalendar.SelectionChanged

        Dim sJscript As String
        Dim fieldNameParam As String

        ConfirmationCheck()

        fieldNameParam = HttpContext.Current.Request.QueryString("formname")

        sJscript = "<script language=""javascript"">" & Environment.NewLine

        ' check to see if calendar caller is time sheet
        If Request.QueryString("caller") = "ts" Then
            ' need to get the previous sat date from selected date

            Dim day As DayOfWeek = Me.MyCalendar.SelectedDate().DayOfWeek
            Dim dSelStartDate As Date
            Dim i As Int32 = 1

            'ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id

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
            sJscript &= "    window.opener.document." & HttpContext.Current.Request.QueryString("formname") & ".value = '" & _
              ElitaPlusPage.GetDateFormattedString(Me.MyCalendar.SelectedDate) & "';" & Environment.NewLine
            'sJscript &= "window.opener.document." & HttpContext.Current.Request.QueryString("formname") & ".fireEvent('onchange')" & Environment.NewLine
            sJscript &= " var element = window.opener.document.getElementById('" & HttpContext.Current.Request.QueryString("formname") & "'); if(element.onchange) { element.onchange();} " & Environment.NewLine

        Else
            fieldNameParam = fieldNameParam.Replace(":", "_")
            sJscript &= "    window.opener.document.getElementById('" & fieldNameParam & "').value = '" & _
              ElitaPlusPage.GetDateFormattedString(Me.MyCalendar.SelectedDate) & "';" & Environment.NewLine
        End If

        sJscript &= "    window.close();" & Environment.NewLine

        If Request.QueryString("caller") = "ts" Then
            sJscript &= "    window.opener.document.Form1.submit();" & Environment.NewLine
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
        ConfirmationCheck()
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
        ConfirmationCheck()
        Dim strSelMonth As String = cboMonthList.SelectedItem.Text
        SetNewVisibleDate(strSelMonth, cboYearList.SelectedItem.Text)
    End Sub

    ' *************************************************************************** '
    '   Sub YearSelected: Handles the select change event of the year dropdown and
    '                     sets the visible year of the calendar
    ' *************************************************************************** '
    Private Sub YearSelected(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboYearList.SelectedIndexChanged
        ConfirmationCheck()
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
            DateTime.TryParseExact(strDateVal, "MMM/YYYY", System.Threading.Thread.CurrentThread.CurrentCulture, DateTimeStyles.None, sNewDate)
        End Try
        MyCalendar.VisibleDate = sNewDate
    End Sub

    ' *************************************************************************** '
    '   Sub Calendar1_DayRender: Add checkbox to the calendar
    ' *************************************************************************** '
    Private Sub Calendar1_DayRender(ByVal sender As Object, ByVal e As DayRenderEventArgs) Handles MyCalendar.DayRender

        Dim d As CalendarDay = e.Day
        Dim cb As CheckBox = New CheckBox
        Dim aLabel As Label = New Label
        Dim sID As String = e.Day.Date.ToString(MMM_DD_YYYY)

        If e.Day.IsOtherMonth Then
            e.Day.IsSelectable = False
        Else
            cb.ID = sID
            cb.Text = e.Day.DayNumberText
            cb.TextAlign = TextAlign.Right
            e.Cell.Controls.Item(0).Visible = False
            e.Cell.Controls.Add(cb)
            e.Cell.HorizontalAlign = HorizontalAlign.Left
            cb.Attributes.Add("onclick", "CheckboxAction('" & sID & "','" & cb.ClientID & "')")

            If (IsChecked(sID)) Then
                cb.Checked = True
            End If

        End If

    End Sub
#End Region

#End Region

End Class
