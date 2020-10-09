Imports Microsoft.VisualBasic
Imports System.Globalization
Imports System.Threading
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.Web.Forms

Namespace Tables
    Partial Class CompanyCreditCardScheduleForm
        Inherits ElitaPlusSearchPage

#Region "Constants"
        Public Const URL As String = "CompanyCreditCardScheduleForm.aspx"

        Private Const MSG_CONFIRM_PROMPT As String = "MSG_CONFIRM_PROMPT"
        Private Const MSG_RECORD_DELETED_OK As String = "MSG_RECORD_DELETED_OK"
        Private Const MSG_RECORD_SAVED_OK As String = "MSG_RECORD_SAVED_OK"
        Private Const MSG_RECORD_NOT_SAVED As String = "MSG_RECORD_NOT_SAVED"

        Private Const DV_ID_COL As Integer = 0
        Private Const DV_DATE_COL As Integer = 1
        Private Const ID_COL As Integer = 3
        Private Const CLOSE_DATE_COL As Integer = 2

        Private Const RECORD_TYPE_CONTROL_NAME As String = "moRecordTypeTextGrid"

        Private Const EMPTY As String = ""
        Private Const DEFAULT_PAGE_INDEX As Integer = 0

        Private Const ID_CONTROL_NAME As String = "IdLabel"

        Private Const DELETE_COMMAND As String = "DeleteRecord"
        Private Const SORT_COMMAND As String = "Sort"
        Private Const NO_ROW_SELECTED_INDEX As Integer = -1

        ' Property Name

        Private Const COMPANY_ID_PROPERTY As String = "CompanyID"
        Private Const BILLING_DATE_PROPERTY As String = "BillingDate"
        Private Const COMPANY_CREDIT_CARD_ID_PROPERTY As String = "CompanyCreditCardId"
#End Region

#Region "Member Variables"

        Private Shared pageIndex As Integer
        Private Shared pageCount As Integer
        Public selectedPageSize As Integer = DEFAULT_PAGE_SIZE
        'Protected WithEvents btnSave As System.Web.UI.WebControls.Button
        'Protected WithEvents btnUndo As System.Web.UI.WebControls.Button
        Protected WithEvents LbPage As System.Web.UI.WebControls.Label
        'Protected WithEvents Button1 As System.Web.UI.WebControls.Button
        'Protected WithEvents Button2 As System.Web.UI.WebControls.Button
        Public selectedPageIndex As Integer = DEFAULT_PAGE_INDEX
        Protected WithEvents tsHoriz As Microsoft.Web.UI.WebControls.TabStrip
        Protected WithEvents lblPageSize As System.Web.UI.WebControls.Label
        Protected WithEvents mpHoriz As Microsoft.Web.UI.WebControls.MultiPage
        'Protected WithEvents tblContainer As System.Web.UI.HtmlControls.HtmlTable
        'Protected WithEvents lblColon As System.Web.UI.WebControls.Label


#End Region

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Protected WithEvents Label3 As System.Web.UI.WebControls.Label
        ' Protected WithEvents ErrController As ErrorController

        'NOTE: The following placeholder declaration is required by the Web Form Designer.
        'Do not delete or move it.
        Private designerPlaceholderDeclaration As System.Object

        Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

#Region "Properties"

        Public ReadOnly Property IsEditing() As Boolean
            Get
                IsEditing = (moDataGrid.EditItemIndex > NO_ITEM_SELECTED_INDEX)
            End Get
        End Property

#End Region

#Region "Page State"

        Class MyState
            Public PageIndex As Integer = 0
            Public MyBO As CcBillingSchedule
            Public CCBILLINGSCHEDULEID As Guid
            Public CompanyId As Guid
            Public OtherCompanyId As Guid = Guid.Empty
            Public CompanyCreditCardId As Guid = Guid.Empty
            Public OldCompanies As ArrayList
            Public isNewCompany As Boolean = False
            Public year As String
            Public CompanyName As String
            Public prevSelectedYear As String
            Public prevSelectedCompanyName As String
            Public isDateSectionLoading As Boolean = False
            Public SetClosingDateByCompany As Boolean = False
            Public ActionInProgress As DetailPageCommand = ElitaPlusPage.DetailPageCommand.Nothing_
            Public selectedPageSize As Integer = DEFAULT_PAGE_SIZE
            Public selectedPageIndex As Integer = DEFAULT_PAGE_INDEX
            Public sortBy As String
            Public isNew As Boolean = False
            Public oCurrentCulture As CultureInfo
            Public HasDataChanged As Boolean
            Public CompanyCreditType As String
            Public addNewYearOnLoad As Boolean = True
            Public NewYearSelected As String
            Public NewYearAdded As Boolean = False

        End Class

        Public Sub New()
            MyBase.New(New MyState)
        End Sub

        Protected Shadows ReadOnly Property State() As MyState
            Get
                Return CType(MyBase.State, MyState)
            End Get
        End Property

        Private Sub SetStateProperties()
            If IsSingleCompanyUser() Then
                State.CompanyId = ElitaPlusIdentity.Current.ActiveUser.Company.Id
            Else
                If State.SetClosingDateByCompany Then
                    State.CompanyId = New Guid(moCompanyDropDownList.SelectedItem.Value)
                Else
                    State.CompanyId = Guid.Empty
                End If
            End If
        End Sub

        Private Sub Page_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall
            Try
                If CallingParameters IsNot Nothing Then
                    'Get the id from the parent
                    Dim boCompanyCreditCard As CompanyCreditCard
                    Dim boCompany As Company
                    Dim paramList As ArrayList
                    'boCompanyCreditCard = New CompanyCreditCard(CType(Me.CallingParameters, Guid))
                    paramList = CType(CallingParameters, ArrayList)
                    boCompanyCreditCard = New CompanyCreditCard(CType(paramList(0), Guid))
                    State.CompanyCreditCardId = boCompanyCreditCard.Id
                    State.CompanyCreditType = CType(paramList(1), String)
                    State.CompanyId = boCompanyCreditCard.CompanyId
                    boCompany = New Company(State.CompanyId)
                    State.CompanyName = boCompany.Description
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrController)
            End Try

        End Sub

#End Region

#Region "Page Events"

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Try
                ErrController.Clear_Hide()
                SetStateProperties()
                If Not Page.IsPostBack Then
                    SetGridItemStyleColor(moDataGrid)
                    State.PageIndex = 0
                    State.year = DatePart("yyyy", DateHelper.GetDateValue(Now.ToString())).ToString
                    PopulateYearsDropdown()
                    'DEF-1726 Set Current year as the year selected in Year dropdown
                    setYearSelection(State.year)
                    'End of DEF-1726
                    PopulateCompaniesDropdown()
                    setCompanySelection(State.CompanyName)
                    moYearLabel.Text = State.CompanyCreditType
                    'If IsSingleCompanyUser() Then
                    'Me.State.CompanyId = ElitaPlusIdentity.Current.ActiveUser.Company.Id
                    State.SetClosingDateByCompany = True
                    moByCompanyLabel.Visible = True
                    moCompanyDropDownList.Visible = True
                    lblColon.Visible = True
                    tblContainer.Visible = True
                    btnNew_WRITE.Visible = True
                    btnNew_WRITE.Enabled = True
                    'Else
                    '    Me.State.SetClosingDateByCompany = False
                    '    lblRecordCount.Visible = False
                    '    moDataGrid.Visible = False
                    '    btnUndo_WRITE.Visible = False
                    '    SaveButton_WRITE.Visible = False
                    '    btnNew_WRITE.Visible = False
                    '    tblContainer.Visible = False
                    'End If
                    State.oCurrentCulture = New CultureInfo(CultureInfo.CurrentCulture.ToString())
                    'Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")
                    If CallingParameters IsNot Nothing AndAlso State.addNewYearOnLoad Then
                        btnNew_WRITE.Enabled = False
                        State.prevSelectedYear = State.year
                        State.prevSelectedCompanyName = State.CompanyName
                        State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Undo
                        State.isNew = True
                        LocateNewCompany()
                        PopulateGrid(True)
                        If IsDataGPageDirty() Then
                            DisplayMessage(Message.MSG_PAGE_SAVE_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSavePagePromptResponse)
                            State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                            State.NewYearAdded = True
                            HiddenIsPageDirty.Value = "NO"
                        End If

                    Else
                        PopulateGrid()
                    End If
                    setYearSelection(State.year)

                Else
                    CheckIfComingFromSaveConfirm()
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrController)
            End Try
            ShowMissingTranslations(ErrController)
        End Sub

#End Region

#Region "Controlling Logic"

        Protected Sub CheckIfComingFromSaveConfirm()

            Dim confResponse As String = HiddenSavePagePromptResponse.Value

            If confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_YES Then
                If State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr Then
                    'DEF-1562
                    ValidateGrig()
                    'End of DEF-1562
                    SavePage()
                End If
                Select Case State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Back
                        ReturnPage()
                    Case ElitaPlusPage.DetailPageCommand.New_
                        AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                        State.isNew = True
                        PopulateGrid()
                End Select
            ElseIf confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_NO Then
                State.NewYearAdded = False
                HiddenIsPageDirty.Value = "NO"
                Select Case State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Back
                        ReturnPage()
                    Case ElitaPlusPage.DetailPageCommand.New_
                        State.isNew = True
                        PopulateGrid()
                    Case ElitaPlusPage.DetailPageCommand.Accept
                        State.isNew = False
                        State.year = GetSelectedDescription(moYearDropDownList)
                        PopulateYearsDropdown()
                        setYearSelection(State.year)
                        State.CompanyName = GetSelectedDescription(moCompanyDropDownList)
                        PopulateCompaniesDropdown()
                        setCompanySelection(State.CompanyName)
                        PopulateGrid()
                End Select
            End If

            If Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Undo Then
                'Me.State.year = Me.State.prevSelectedYear
                'PopulateYearsDropdown()
                'setYearSelection(Me.State.year)
                Return
            End If

            'Clean after consuming the action
            State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
            HiddenSavePagePromptResponse.Value = ""

        End Sub
        Private Function CreateBoFromGrid(index As Integer) As CcBillingSchedule
            Dim CcBillingScheduleId As Guid
            Dim CcBillingSchedule As CcBillingSchedule

            moDataGrid.SelectedIndex = index
            CcBillingScheduleId = New Guid(moDataGrid.Items(index).Cells(ID_COL).Text)

            If CcBillingScheduleId.Equals(Guid.Empty) Then
                CcBillingSchedule = New CcBillingSchedule
            Else
                CcBillingSchedule = New CcBillingSchedule(CcBillingScheduleId)
            End If
            Return CcBillingSchedule
        End Function

        Private Sub SavePage()

            Dim ScheduleBillingInfoCompaniesArrayList As ArrayList
            Dim companyGroupID As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id

            Dim ActiveUserOtherCompaniesArrayList As ArrayList

            'When Closing Date is By Company, Set up the  Arraylist with current company
            ActiveUserOtherCompaniesArrayList = New ArrayList
            ActiveUserOtherCompaniesArrayList.Add(State.CompanyCreditCardId)

            If ActiveUserOtherCompaniesArrayList.Count > 0 Then
                If State.isNew Then
                    ScheduleBillingInfoCompaniesArrayList = New ArrayList
                    For i As Integer = 0 To ActiveUserOtherCompaniesArrayList.Count - 1
                        Dim SchedulebillingInfosArrayList As New ArrayList
                        For j As Integer = 0 To 11
                            Dim ScheduleBillingDateInfo As New CcBillingSchedule
                            ScheduleBillingDateInfo.CompanyCreditCardId = CType(ActiveUserOtherCompaniesArrayList.Item(i), Guid)
                            SchedulebillingInfosArrayList.Add(ScheduleBillingDateInfo)
                        Next
                        ScheduleBillingInfoCompaniesArrayList.Add(SchedulebillingInfosArrayList)
                    Next
                Else
                    ScheduleBillingInfoCompaniesArrayList = New ArrayList
                    For i As Integer = 0 To ActiveUserOtherCompaniesArrayList.Count - 1
                        Dim BillingScheduleInfosArrayList As New ArrayList
                        Dim dv As DataView = Assurant.ElitaPlus.BusinessObjectsNew.CcBillingSchedule.GetCCBillingScheduleDates(
                                                CType(ActiveUserOtherCompaniesArrayList.Item(i), Guid), State.year)
                        If moDataGrid.Items.Count <> dv.Count Then
                            Throw New GUIException("", Assurant.ElitaPlus.Common.ErrorCodes.INVALID_RECORD_COUNT_ON_OTHER_COMPANY_ACCOUNTING_CLOSE_DATES_ERR)
                        End If
                        For j As Integer = 0 To 11
                            Dim accCloseInfoId As Guid = GuidControl.ByteArrayToGuid(dv.Table.Rows(j).Item(DV_ID_COL))
                            BillingScheduleInfosArrayList.Add(New CcBillingSchedule(accCloseInfoId))
                        Next
                        ScheduleBillingInfoCompaniesArrayList.Add(BillingScheduleInfosArrayList)
                    Next
                End If
            End If

            Dim CcBillingSchedule As CcBillingSchedule
            Dim totItems As Integer = moDataGrid.Items.Count

            If totItems > 0 Then
                CcBillingSchedule = CreateBoFromGrid(0)
                CcBillingSchedule.isDateEnable = False
                BindBoPropertiesToGridHeaders(CcBillingSchedule)
                If moDataGrid.Items(0).Cells(2).Enabled Then
                    CcBillingSchedule.isDateEnable = True
                End If

                PopulateBOFromForm(CcBillingSchedule, ScheduleBillingInfoCompaniesArrayList, 0)

                CcBillingSchedule.Save()

            End If

            totItems = totItems - 1
            For index As Integer = 1 To totItems
                CcBillingSchedule.isDateEnable = False
                If moDataGrid.Items(index).Cells(CLOSE_DATE_COL).Enabled Then
                    CcBillingSchedule.isDateEnable = True
                End If
                CcBillingSchedule = CreateBoFromGrid(index)
                PopulateBOFromForm(CcBillingSchedule, ScheduleBillingInfoCompaniesArrayList, index)

                CcBillingSchedule.Save()
            Next

        End Sub

        Function IsDataGPageDirty() As Boolean
            Dim Result As String = HiddenIsPageDirty.Value

            Return Result.Equals("YES")
        End Function

        Public Function Get_A_New_Date() As BillingSchedulingIfoDataview
            Dim oDataTable As DataTable = BillingSchedulingIfoDataview.CreateTable
            Dim dv As CcBillingSchedule.CCSchedulingInfoSearchDV = CcBillingSchedule.GetLastBillingDate(State.CompanyCreditCardId)
            Dim coverageRow As DataRow = dv.Table.Rows(0)
            Dim LastDate As Date
            If coverageRow(CcBillingSchedule.CCSchedulingInfoSearchDV.COL_NAME_CLOSE_DATE) Is System.DBNull.Value Then
                LastDate = (DateAdd("YYYY", -1, DateAdd("m", -Month(Now) + 12, DateAdd("d", -Date.Today.Day + 1, Now))))
            Else
                LastDate = CType(coverageRow(CcBillingSchedule.CCSchedulingInfoSearchDV.COL_NAME_CLOSE_DATE), Date)
            End If

            LoadLaterYear(oDataTable, LastDate)
            AddNewBillingRow(oDataTable, LastDate)

            Return New BillingSchedulingIfoDataview(oDataTable)

        End Function
        Private Sub AddNewBillingRow(oDataTable As DataTable, oDate As Date)
            For I As Integer = 1 To 12
                Dim dr As DataRow = oDataTable.NewRow()
                dr(BillingSchedulingIfoDataview.COL_ID) = Guid.Empty
                dr(BillingSchedulingIfoDataview.COL_NAME) = FormatDateTime(MiscUtil.LastFridayOfMonth((oDate.AddMonths(I))), DateFormat.ShortDate)
                'FormatDateTime(CDate(objDate), DateFormat.ShortDate)
                oDataTable.Rows.Add(dr)
            Next
            setdirty()
        End Sub
        Private Sub LoadLaterYear(oDataTable As DataTable, oDate As Date)
            State.year = DatePart("yyyy", Now).ToString
            Dim NewYear As String = CType(CType(State.year, Integer) + 1, String)
            Dim lstSelectedVal As ListItem = moYearDropDownList.Items.FindByText(NewYear)
            If (lstSelectedVal Is Nothing) Then
                moYearDropDownList.Items.Add(NewYear)
            End If

            setYearSelection(NewYear)
            State.NewYearSelected = NewYear


            Dim odv As DataView = GetDV()

            If odv.Count > 0 AndAlso odv.Count < 12 Then
                For i As Integer = 0 To odv.Count - 1
                    Dim dr As DataRow = oDataTable.NewRow()
                    dr(BillingSchedulingIfoDataview.COL_ID) = GuidControl.ByteArrayToGuid(odv.Item(i).Row.Item(DV_ID_COL))
                    dr(BillingSchedulingIfoDataview.COL_NAME) = odv.Item(i).Row.Item(DV_DATE_COL)
                    oDataTable.Rows.Add(dr)
                Next
            End If
        End Sub

        Private Sub setdirty()
            HiddenIsPageDirty.Value = "YES"
        End Sub

        'Private Sub ValidateGrig()
        '    Dim SelectedYear As String = Me.GetSelectedDescription(Me.moYearDropDownList)
        '    Dim DuplicatedMothFound As Boolean = False
        '    For index As Integer = 0 To moDataGrid.Items.Count - 1
        '        Dim odate As TextBox = CType(moDataGrid.Items(index).Cells(CLOSE_DATE_COL).FindControl("moDateCompTextGrid"), TextBox)
        '        Dim EnterYear As String = (DatePart("yyyy", CType(odate.Text, Date)).ToString)
        '        Dim omonth As String = (DatePart("m", CType(odate.Text, Date)).ToString)
        '        DuplicatedMothFound = False
        '        For i As Integer = 0 To moDataGrid.Items.Count - 1
        '            Dim tbdate As TextBox = CType(moDataGrid.Items(i).Cells(CLOSE_DATE_COL).FindControl("moDateCompTextGrid"), TextBox)
        '            Dim smonth As String = (DatePart("m", CType(tbdate.Text, Date)).ToString)
        '            If DuplicatedMothFound AndAlso omonth = smonth Then
        '                Throw New GUIException("You enter a duplicated Month", Assurant.ElitaPlus.Common.ErrorCodes.GUI_ENTER_MONTH_ERROR)
        '            End If
        '            If omonth = smonth Then
        '                DuplicatedMothFound = True
        '            End If
        '        Next
        '        If SelectedYear <> EnterYear Then
        '            Throw New GUIException("You cannot modify the year", Assurant.ElitaPlus.Common.ErrorCodes.GUI_ENTER_YEAR_ERROR)
        '        End If
        '        'Dim format As String = "dd-MMM-yyyy"
        '        'Dim dateTime As DateTime
        '        'If Not dateTime.TryParseExact(odate.Text, format, CultureInfo.CurrentCulture, DateTimeStyles.NoCurrentDateDefault, dateTime) Then
        '        '    Throw New GUIException("Invalid Date", Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_DATE_ENTERED_ERROR)
        '        'End If
        '        'If DateTimeCondition Then 
        '        'End If 
        '    Next
        'End Sub
        Private Sub ValidateGrig()
            'Pratap DEF 2068
            Dim SelectedYear As String = GetSelectedDescription(moYearDropDownList)
            Dim DuplicatedMothFound As Boolean = False


            For index As Integer = 0 To moDataGrid.Items.Count - 1
                Dim odate As TextBox = DirectCast(moDataGrid.Items(index).Cells(CLOSE_DATE_COL).FindControl("moDateCompTextGrid"), TextBox)
                Dim format As String = "dd-MMM-yyyy"
                Dim dateTime__1 As DateTime

                DateHelper.GetDateValue(odate.Text)
                'If DateTime.TryParseExact(odate.Text, format, CultureInfo.InvariantCulture, DateTimeStyles.None, dateTime__1) Then
                Dim EnterYear As String = (DatePart("yyyy", DateHelper.GetDateValue(odate.Text)).ToString)
                Dim omonth As String = (DatePart("m", DateHelper.GetDateValue(odate.Text)).ToString)
                DuplicatedMothFound = False
                For i As Integer = 0 To moDataGrid.Items.Count - 1
                    Dim tbdate As TextBox = DirectCast(moDataGrid.Items(i).Cells(CLOSE_DATE_COL).FindControl("moDateCompTextGrid"), TextBox)
                    'If DateTime.TryParseExact(tbdate.Text, format, CultureInfo.InvariantCulture, DateTimeStyles.None, dateTime__1) Then
                    DateHelper.GetDateValue(tbdate.Text)
                    Dim smonth As String = DateHelper.GetDateValue(tbdate.Text) '(DatePart("m", Convert.ToDateTime(tbdate.Text)).ToString)
                    If DuplicatedMothFound AndAlso omonth = smonth Then
                        Throw New GUIException("You enter a duplicated Month", Assurant.ElitaPlus.Common.ErrorCodes.GUI_ENTER_MONTH_ERROR)
                    End If
                    If omonth = smonth Then
                        DuplicatedMothFound = True


                    End If
                    'End If
                Next
                If SelectedYear <> EnterYear Then
                    Throw New GUIException("You cannot modify the year", Assurant.ElitaPlus.Common.ErrorCodes.GUI_ENTER_YEAR_ERROR)
                End If
                'Else


                'Throw New GUIException("Invalid Date", Assurant.ElitaPlus.Common.ErrorCodes.INVALID_DATE_ERR)


                'If DateTimeCondition Then


                'End If
                ' End If
            Next



        End Sub


        Private Sub ReturnPage()
            'CultureInfo.CurrentCulture
            Thread.CurrentThread.CurrentCulture = New CultureInfo(State.oCurrentCulture.ToString)
            If CallingParameters IsNot Nothing Then
                Dim retType As New CompanyCreditCardForm.ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.CompanyCreditCardId, State.HasDataChanged)
                ReturnToCallingPage(retType)

            Else
                ReturnToTabHomePage()
            End If
        End Sub

        'Public Function DateTimeformat(ByVal objDate As Object) As String
        '    'Dim strRet As DateTime = CDate(objDate)

        '    Dim TestString As String = FormatDateTime(CDate(objDate), DateFormat.ShortDate)


        '    'Try
        '    '    Dim dtVal As DateTime = CType(objDate, DateTime)
        '    '    strRet = dtVal.ToString("dd-Mon-yyyy")
        '    'Catch ex As Exception
        '    'End Try
        '    'Return strRet


        '    Return (TestString)

        'End Function

#End Region

#Region "Populate"

        Private Sub PopulateData(year As String)
            Dim dv As DataView

            If year = State.NewYearSelected AndAlso State.NewYearAdded = False Then
                dv = Get_A_New_Date()
            Else
                dv = GetDV(State.CompanyCreditCardId)

            End If
            moDataGrid.DataSource = dv
            moDataGrid.DataBind()
        End Sub


        Private Sub PopulateGrid(Optional ByVal blnPageState As Boolean = False)

            Dim dv As DataView
            Dim recCount As Integer = 0

            Try
                If State.isNew Then
                    State.isNew = blnPageState
                    dv = Get_A_New_Date()
                Else
                    If IsSingleCompanyUser() AndAlso State.year = "" Then
                        State.year = DatePart("yyyy", DateHelper.GetDateValue(Now.ToString())).ToString
                    End If
                    dv = GetDV(State.CompanyCreditCardId)
                End If

                dv.Sort = State.sortBy
                recCount = dv.Count
                Session("recCount") = recCount
                TranslateGridControls(moDataGrid)
                moDataGrid.DataSource = dv
                moDataGrid.DataBind()
                lblRecordCount.Text = recCount & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                ControlMgr.DisableAllGridControlsIfNotEditAuth(Me, moDataGrid)

                State.year = GetSelectedDescription(moYearDropDownList)
                If Not State.isNew Then
                    PopulateYearsDropdown()
                    setYearSelection(State.year)
                End If

            Catch ex As Exception
                HandleErrors(ex, ErrController)
            End Try

        End Sub

        Private Function GetDV(Optional ByVal Id As Object = Nothing) As DataView

            Dim dv As DataView

            dv = GetGridDataView(Id)
            dv.Sort = moDataGrid.DataMember()

            Return (dv)

        End Function

        Private Function GetLastClosingDate() As CcBillingSchedule.CCSchedulingInfoSearchDV

            With State
                Return (Assurant.ElitaPlus.BusinessObjectsNew.CcBillingSchedule.GetLastBillingDate(.CompanyId))
            End With

        End Function

        Private Function GetGridDataView(Optional ByVal newCompanyID As Object = Nothing) As DataView

            With State
                If newCompanyID Is Nothing Then
                    Return (Assurant.ElitaPlus.BusinessObjectsNew.CcBillingSchedule.GetCCBillingScheduleDates(.CompanyId, .year))
                Else
                    Return (Assurant.ElitaPlus.BusinessObjectsNew.CcBillingSchedule.GetCCBillingScheduleDates(CType(newCompanyID, Guid), .year))
                    'Return (Assurant.ElitaPlus.BusinessObjectsNew.CcBillingSchedule.GetAllAccountingCloseDates(CType(newCompanyID, Guid)))
                End If
            End With

        End Function

        Private Sub PopulateBOItem(CcBillingSchedule As CcBillingSchedule, oPropertyName As String, oCellPosition As Integer)
            PopulateBOProperty(CcBillingSchedule, oPropertyName,
                                            CType(GetSelectedGridControl(moDataGrid, oCellPosition), TextBox))
        End Sub

        Private Sub PopulateBOFromForm(CcBillingSchedule As CcBillingSchedule, CCSchedulingBillingInfoCompaniesArrayList As ArrayList, BilligScheduleDateIndex As Integer)

            PopulateBOProperty(CcBillingSchedule, COMPANY_CREDIT_CARD_ID_PROPERTY, State.CompanyCreditCardId)
            PopulateBOItem(CcBillingSchedule, BILLING_DATE_PROPERTY, CLOSE_DATE_COL)
            'If Me.State.isNew Then CcBillingSchedule.CompanyId = Me.State.CompanyId
            'Update the other company(s)
            If CCSchedulingBillingInfoCompaniesArrayList IsNot Nothing AndAlso CCSchedulingBillingInfoCompaniesArrayList.Count > 0 Then
                For i As Integer = 0 To CCSchedulingBillingInfoCompaniesArrayList.Count - 1
                    Dim BillingScheduleInfosArrayList As ArrayList = CType(CCSchedulingBillingInfoCompaniesArrayList.Item(i), ArrayList)
                    PopulateBOItem(CType(BillingScheduleInfosArrayList.Item(BilligScheduleDateIndex), CcBillingSchedule), BILLING_DATE_PROPERTY, CLOSE_DATE_COL)
                Next
            End If
            If ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub

        Private Sub PopulateFormItem(oCellPosition As Integer, oPropertyValue As Object)
            PopulateControlFromBOProperty(GetSelectedGridControl(moDataGrid, oCellPosition), oPropertyValue)
        End Sub

        Private Sub PopulateYearsDropdown()
            Dim oListContext As New ListContext
            oListContext.CompanyCreditCardId = State.CompanyCreditCardId
            Dim billingYears As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:=ListCodes.BillingYearsByCompanyCreditCard, context:=oListContext)

            'Dim dv As DataView = CcBillingSchedule.GetBillingYears(Me.State.CompanyCreditCardId)

            If IsSingleCompanyUser() Then
                moYearDropDownList.Populate(billingYears, New PopulateOptions() With
                {
                    .TextFunc = AddressOf .GetDescription,
                    .ValueFunc = AddressOf .GetCode,
                .AddBlankItem = False
                })
                'Me.BindListTextToDataView(Me.moYearDropDownList, dv, , , False)
            Else
                moYearDropDownList.Populate(billingYears, New PopulateOptions() With
                {
                    .TextFunc = AddressOf .GetDescription,
                    .ValueFunc = AddressOf .GetCode,
                .AddBlankItem = True,
                    .BlankItemValue = "0"
                })
                'Me.BindListTextToDataView(Me.moYearDropDownList, dv, , , True)

                'If moCompanyDropDownList.SelectedIndex > 0 AndAlso Me.State.isDateSectionLoading AndAlso dv.Count = 0 Then
                'tblContainer.Visible = True
                'End If

                If moCompanyDropDownList.SelectedIndex > 0 AndAlso State.isDateSectionLoading AndAlso billingYears.Count = 0 Then
                    tblContainer.Visible = True
                End If
            End If

            'Dim dvItem As DataRowView
            'For i As Int32 = 0 To dv.Count - 1
            '    dvItem = dv(i)
            '    If dvItem(LookupListNew.COL_DESCRIPTION_NAME).ToString = Me.State.year Then
            '        'comment -1 for def-2014
            '        If i < dv.Count Then '- 1 Then
            '            Me.State.addNewYearOnLoad = False
            '        End If
            '    End If
            'Next

            If (From b In billingYears Where b.Translation = State.year Select b).Any() Then
                State.addNewYearOnLoad = False
            End If

        End Sub

        Private Sub PopulateCompaniesDropdown()
            Dim oListContext As New ListContext
            oListContext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
            Dim compLkl As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="CompanyByCompanyGroup", context:=oListContext)

            Dim list As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
            Dim filteredList As DataElements.ListItem() = (From x In compLkl
                                                           Where list.Contains(x.ListItemId)
                                                           Select x).ToArray()

            moCompanyDropDownList.Populate(filteredList.ToArray(), New PopulateOptions() With
                {
                    .AddBlankItem = True
                })

            'Dim companyGroupID As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
            'Dim selectedCompaniesDv As DataView = ElitaPlusIdentity.Current.ActiveUser.GetSelectedCompanies(companyGroupID, ElitaPlusIdentity.Current.ActiveUser.Id)
            'Me.BindListControlToDataView(Me.moCompanyDropDownList, selectedCompaniesDv, , , True)

            ControlMgr.SetEnableControl(Me, moCompanyDropDownList, False)
        End Sub

        Private Function IsSingleCompanyUser() As Boolean
            Dim oListContext As New ListContext
            oListContext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
            Dim compLkl As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="CompanyByCompanyGroup", context:=oListContext)

            Dim list As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
            Dim filteredList As DataElements.ListItem() = (From x In compLkl
                                                           Where list.Contains(x.ListItemId)
                                                           Select x).ToArray()

            Return filteredList.Count = 1

            'Dim companyGroupID As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
            'Dim selectedCompaniesDv As DataView = ElitaPlusIdentity.Current.ActiveUser.GetSelectedCompanies(companyGroupID, ElitaPlusIdentity.Current.ActiveUser.Id)

            'If selectedCompaniesDv.Count = 1 Then
            '    Return True
            'Else
            '    Return False
            'End If
        End Function

        Private Sub setYearSelection(Year As String)
            moYearDropDownList.SelectedIndex = moYearDropDownList.Items.IndexOf(moYearDropDownList.Items.FindByText(Year))
        End Sub

        Private Sub setCompanySelection(CompanyName As String)
            moCompanyDropDownList.SelectedIndex = moCompanyDropDownList.Items.IndexOf(moCompanyDropDownList.Items.FindByText(CompanyName))
        End Sub

        Private Sub LocateNewCompany()
            Dim ActiveUserCompanyIdDV As DataView = GetGridDataView()
            State.OtherCompanyId = Guid.Empty

            If ActiveUserCompanyIdDV.Count = 0 Then
                State.OtherCompanyId = State.CompanyId
            End If
        End Sub

#End Region

#Region "GridHandlers"

        Private Sub moDataGrid_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles moDataGrid.PageIndexChanged
            Try
                State.selectedPageIndex = e.NewPageIndex
                If IsDataGPageDirty() Then
                    DisplayMessage(Message.MSG_PAGE_SAVE_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSavePagePromptResponse)
                Else
                    moDataGrid.CurrentPageIndex = e.NewPageIndex
                    PopulateGrid()
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrController)
            End Try
        End Sub

        Private Sub moDataGrid_PageSizeChanged(source As System.Object, e As System.EventArgs)
            Try
                If IsDataGPageDirty() Then
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.GridPageSize
                    DisplayMessage(Message.MSG_PAGE_SAVE_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSavePagePromptResponse)
                Else
                    'moDataGrid.CurrentPageIndex = NewCurrentPageIndex(moDataGrid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                    'Me.State.selectedPageSize = CType(cboPageSize.SelectedValue, Integer)
                    'Me.PopulateGrid()
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrController)
            End Try
        End Sub

        Protected Sub ItemCommand(source As Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs)
            Try
                If (e.CommandName = SORT_COMMAND_NAME) Then
                    State.sortBy = CType(e.CommandArgument, String)
                    PopulateGrid()
                ElseIf (e.CommandName = DELETE_COMMAND) Then
                    'Do the delete here

                    'Clear the SelectedItemStyle to remove the highlight from the previously saved row
                    moDataGrid.SelectedIndex = NO_ROW_SELECTED_INDEX

                    'Save the Id in the Session

                    State.CCBILLINGSCHEDULEID = New Guid(moDataGrid.Items(e.Item.ItemIndex).Cells(ID_COL).Text)

                    State.MyBO = New Assurant.ElitaPlus.BusinessObjectsNew.CcBillingSchedule(State.CCBILLINGSCHEDULEID)
                    Try
                        State.MyBO.Delete()
                        'Call the Save() method in the AccountingCloseInfo Business Object here
                        State.MyBO.Save()
                    Catch ex As Exception
                        State.MyBO.RejectChanges()
                        Throw ex
                    End Try

                    PopulateGrid()

                End If
            Catch ex As Exception
                HandleErrors(ex, ErrController)
            End Try
        End Sub

        Protected Sub ItemBound(source As Object, e As DataGridItemEventArgs) Handles moDataGrid.ItemDataBound
            Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)
            Dim oTextBox As TextBox

            If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem Then
                With e.Item
                    PopulateControlFromBOProperty(.Cells(ID_COL), dvRow(CcBillingSchedule.COL_NAME_CC_BILLING_SCHEDULE_IDD))
                    oTextBox = CType(e.Item.Cells(CLOSE_DATE_COL).FindControl("moDateCompTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Dim oDateCompImage As ImageButton = CType(e.Item.Cells(CLOSE_DATE_COL).FindControl("moDateCompImageGrid"), ImageButton)
                    Dim oDeleteCompImage As ImageButton = CType(e.Item.Cells(1).FindControl("DeleteButton_WRITE"), ImageButton)
                    If (oDateCompImage IsNot Nothing) Then
                        AddCalendar(oDateCompImage, oTextBox)
                    End If

                    PopulateControlFromBOProperty(oTextBox, GetDateFormattedString(DateHelper.GetDateValue(dvRow(CcBillingSchedule.COL_NAME_BILLING_DATE))))
                    If CDate(DateHelper.GetDateValue(dvRow(CcBillingSchedule.COL_NAME_BILLING_DATE))) < Now Then
                        e.Item.Cells(CLOSE_DATE_COL).Enabled = False
                        oTextBox.Enabled = False
                        oDeleteCompImage.Enabled = False
                        oDateCompImage.Enabled = False
                    End If
                End With
            End If
            BaseItemBound(source, e)

        End Sub

        Protected Sub ItemCreated(sender As Object, e As DataGridItemEventArgs)
            BaseItemCreated(sender, e)
        End Sub

        Protected Sub BindBoPropertiesToGridHeaders(CCSchedulingInfo As CcBillingSchedule)
            BindBOPropertyToGridHeader(CCSchedulingInfo, CcBillingSchedule.COL_NAME_BILLING_DATE, moDataGrid.Columns(CLOSE_DATE_COL))
            ClearGridHeadersAndLabelsErrSign()
        End Sub

        Private Sub SetFocusOnEditableFieldInGrid(grid As DataGrid, cellPosition As Integer, controlName As String, itemIndex As Integer)
            'Set focus on the Description TextBox for the EditItemIndex row
            Dim desc As TextBox = CType(grid.Items(itemIndex).Cells(cellPosition).FindControl(controlName), TextBox)
            SetFocus(desc)
        End Sub

#End Region

#Region "Button Click Events"

        Private Sub btnBack_Click(sender As System.Object, e As System.EventArgs) Handles btnBack.Click
            Try
                If IsDataGPageDirty() Then
                    DisplayMessage(Message.MSG_PAGE_SAVE_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSavePagePromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    ReturnPage()
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrController)
            End Try
        End Sub

        Private Sub SaveButton_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles SaveButton_WRITE.Click
            Try
                If IsDataGPageDirty() Then
                    ValidateGrig()
                    SavePage()
                    HiddenIsPageDirty.Value = EMPTY
                    State.year = GetSelectedDescription(moYearDropDownList)
                    State.isNew = False
                    PopulateGrid()
                    DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)
                Else
                    DisplayMessage(Message.MSG_RECORD_NOT_SAVED, "", MSG_BTN_OK, MSG_TYPE_INFO)
                End If

                btnNew_WRITE.Enabled = True

            Catch ex As Exception
                HandleErrors(ex, ErrController)
            End Try
        End Sub

        Private Sub btnUndo_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnUndo_WRITE.Click
            Try

                If Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Undo Then
                    PopulateYearsDropdown()
                    'DEF-1726 Set Current year as the year selected in Year dropdown
                    State.year = GetSelectedDescription(moYearDropDownList)
                    'End of DEF-1726
                    setYearSelection(State.year)

                    PopulateCompaniesDropdown()
                    setCompanySelection(State.CompanyName)

                End If

                btnNew_WRITE.Enabled = True
                State.isNew = False
                PopulateGrid()
                HiddenIsPageDirty.Value = EMPTY
                'Pratap DEF 2064
                ErrController.Clear_Hide()
            Catch ex As Exception
                HandleErrors(ex, ErrController)
            End Try
        End Sub

        Private Sub btnNew_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnNew_WRITE.Click

            Try
                btnNew_WRITE.Enabled = False
                State.prevSelectedYear = State.year
                State.prevSelectedCompanyName = State.CompanyName

                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Undo

                If Not IsSingleCompanyUser() Then
                    lblRecordCount.Visible = True
                    moDataGrid.Visible = True
                    btnUndo_WRITE.Visible = True
                    SaveButton_WRITE.Visible = True
                End If

                If IsDataGPageDirty() Then
                    DisplayMessage(Message.MSG_PAGE_SAVE_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSavePagePromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
                Else
                    State.isNew = True
                    LocateNewCompany()
                    PopulateGrid(True)
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrController)
            End Try
        End Sub

#End Region

#Region "DropDownHandlers"

        Private Sub moYearDropDownList_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles moYearDropDownList.SelectedIndexChanged

            Try
                tblContainer.Visible = True
                'If IsDataGPageDirty() Then
                ''DisplayMessage(Message.MSG_PAGE_SAVE_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, Me.HiddenSavePagePromptResponse)
                'Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Accept
                'Else
                State.year = GetSelectedDescription(moYearDropDownList)
                Dim SelectedYear As String = GetSelectedDescription(moYearDropDownList)
                PopulateData(SelectedYear)
                'PopulateYearsDropdown()
                'setYearSelection(SelectedYear)
                btnNew_WRITE.Enabled = True
                If Not IsSingleCompanyUser() Then
                    State.CompanyId = New Guid(moCompanyDropDownList.SelectedItem.Value)
                End If
                'PopulateGrid()
                ' End If
            Catch ex As Exception
                HandleErrors(ex, ErrController)
            End Try
            ''

        End Sub

        Private Sub moCompanyDropDownList_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles moCompanyDropDownList.SelectedIndexChanged

            Try
                State.SetClosingDateByCompany = True
                tblContainer.Visible = False
                If IsDataGPageDirty() Then
                    DisplayMessage(Message.MSG_PAGE_SAVE_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSavePagePromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Accept
                Else
                    If Not IsSingleCompanyUser() Then
                        lblRecordCount.Visible = True
                        moDataGrid.Visible = True
                        btnUndo_WRITE.Visible = True
                        SaveButton_WRITE.Visible = True
                        btnNew_WRITE.Visible = True
                    End If
                    State.CompanyName = GetSelectedDescription(moCompanyDropDownList)
                    Dim SelectedCompanyName As String = GetSelectedDescription(moCompanyDropDownList)
                    PopulateCompaniesDropdown()
                    setCompanySelection(SelectedCompanyName)
                    btnNew_WRITE.Enabled = True
                    State.CompanyId = New Guid(moCompanyDropDownList.SelectedItem.Value)
                    State.isDateSectionLoading = True
                    PopulateGrid()
                    State.isDateSectionLoading = False
                    moYearDropDownList.SelectedIndex = -1
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrController)
            End Try

        End Sub


#End Region

#Region "Dataview"

        Public Class BillingSchedulingIfoDataview
            Inherits DataView

            Public Const COL_ID As String = "CC_BILLING_SCHEDULE_ID"
            Public Const COL_NAME As String = "Description"
            Public Const NEW_DATE As String = "New"

            Public Sub New(t As DataTable)
                MyBase.New(t)
            End Sub

            Public Shared Function CreateTable() As DataTable
                Dim t As New DataTable
                t.Columns.Add(COL_ID, GetType(Guid))
                t.Columns.Add(COL_NAME, GetType(String))
                Return t
            End Function

        End Class

#End Region

    End Class

End Namespace
