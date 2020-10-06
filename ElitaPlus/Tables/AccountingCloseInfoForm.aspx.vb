Imports Microsoft.VisualBasic
Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms

Namespace Tables

    Partial Class AccountingCloseInfoForm
        Inherits ElitaPlusSearchPage

#Region "Constants"
        Public Const URL As String = "AccountingCloseInfoForm.aspx"

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
        Private Const CLOSE_DATE_PROPERTY As String = "ClosingDate"

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
        Protected WithEvents tblContainer As System.Web.UI.HtmlControls.HtmlTable
        Protected WithEvents lblColon As System.Web.UI.WebControls.Label


#End Region

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Protected WithEvents Label3 As System.Web.UI.WebControls.Label
        Protected WithEvents ErrController As ErrorController

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
            Public MyBO As AccountingCloseInfo
            Public AccountingCloseInfoId As Guid
            Public CompanyId As Guid
            Public OtherCompanyId As Guid = Guid.Empty
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
                    'Me.State.CompanyId = CType(Me.CallingParameters, Guid)
                    State.OldCompanies = CType(CallingParameters, ArrayList)
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
                    State.year = DatePart("yyyy", Now).ToString
                    State.CompanyName = String.Empty
                    PopulateYearsDropdown()
                    PopulateCompaniesDropdown()
                    setYearSelection(State.year)
                    setCompanySelection(State.CompanyName)
                    If IsSingleCompanyUser() Then
                        State.CompanyId = ElitaPlusIdentity.Current.ActiveUser.Company.Id
                        State.SetClosingDateByCompany = True
                        moByCompanyLabel.Visible = False
                        moCompanyDropDownList.Visible = False
                        lblColon.Visible = False
                        tblContainer.Visible = True
                    Else
                        State.SetClosingDateByCompany = False
                        lblRecordCount.Visible = False
                        moDataGrid.Visible = False
                        btnUndo_WRITE.Visible = False
                        SaveButton_WRITE.Visible = False
                        btnNew_WRITE.Visible = False
                        tblContainer.Visible = False
                    End If
                    State.oCurrentCulture = New CultureInfo(CultureInfo.CurrentCulture.ToString())
                    'Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")
                    If CallingParameters IsNot Nothing Then
                        btnNew_WRITE.Enabled = False
                        State.prevSelectedYear = State.year
                        State.prevSelectedCompanyName = State.CompanyName
                        State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Undo
                        State.isNew = True
                        LocateNewCompany()
                        PopulateGrid(True)
                    Else
                        PopulateGrid()
                    End If
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
        Private Function CreateBoFromGrid(index As Integer) As AccountingCloseInfo
            Dim AccountingCloseInfoId As Guid
            Dim AccountingCloseInfo As AccountingCloseInfo

            moDataGrid.SelectedIndex = index
            AccountingCloseInfoId = New Guid(moDataGrid.Items(index).Cells(ID_COL).Text)

            If AccountingCloseInfoId.Equals(Guid.Empty) Then
                AccountingCloseInfo = New AccountingCloseInfo
            Else
                AccountingCloseInfo = New AccountingCloseInfo(AccountingCloseInfoId)
            End If
            Return AccountingCloseInfo
        End Function

        Private Sub SavePage()

            Dim AccountingCloseInfoCompaniesArrayList As ArrayList
            Dim companyGroupID As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id

            Dim ActiveUserOtherCompaniesArrayList As ArrayList

            If State.SetClosingDateByCompany Then
                'When Closing Date is By Company, Set up the  Arraylist with current company
                ActiveUserOtherCompaniesArrayList = New ArrayList
                ActiveUserOtherCompaniesArrayList.Add(State.CompanyId)
            Else
                'The Company Group functionality is deprecated and so the code below should never execute, just retaining this for consistency
                'When Closing Date is By Company Group, load the  Arraylist with all the companies in the current Company's Group
                ActiveUserOtherCompaniesArrayList = ElitaPlusIdentity.Current.ActiveUser.LoadUserOtherCompaniesIDs(ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID, companyGroupID)
            End If

            If ActiveUserOtherCompaniesArrayList.Count > 0 Then
                If State.isNew Then
                    AccountingCloseInfoCompaniesArrayList = New ArrayList
                    For i As Integer = 0 To ActiveUserOtherCompaniesArrayList.Count - 1
                        Dim AccountingCloseInfosArrayList As New ArrayList
                        For j As Integer = 0 To 11
                            Dim accountingCloseDateInfo As New AccountingCloseInfo
                            accountingCloseDateInfo.CompanyId = CType(ActiveUserOtherCompaniesArrayList.Item(i), Guid)
                            AccountingCloseInfosArrayList.Add(accountingCloseDateInfo)
                        Next
                        AccountingCloseInfoCompaniesArrayList.Add(AccountingCloseInfosArrayList)
                    Next
                Else
                    AccountingCloseInfoCompaniesArrayList = New ArrayList
                    For i As Integer = 0 To ActiveUserOtherCompaniesArrayList.Count - 1
                        Dim AccountingCloseInfosArrayList As New ArrayList
                        Dim dv As DataView = Assurant.ElitaPlus.BusinessObjectsNew.AccountingCloseInfo.GetAccountingCloseDates(
                                                CType(ActiveUserOtherCompaniesArrayList.Item(i), Guid), State.year)
                        If moDataGrid.Items.Count <> dv.Count Then
                            Throw New GUIException("", Assurant.ElitaPlus.Common.ErrorCodes.INVALID_RECORD_COUNT_ON_OTHER_COMPANY_ACCOUNTING_CLOSE_DATES_ERR)
                        End If
                        For j As Integer = 0 To 11
                            Dim accCloseInfoId As Guid = GuidControl.ByteArrayToGuid(dv.Table.Rows(j).Item(DV_ID_COL))
                            AccountingCloseInfosArrayList.Add(New AccountingCloseInfo(accCloseInfoId))
                        Next
                        AccountingCloseInfoCompaniesArrayList.Add(AccountingCloseInfosArrayList)
                    Next
                End If
            End If

            Dim AccountingCloseInfo As AccountingCloseInfo
            Dim totItems As Integer = moDataGrid.Items.Count

            If totItems > 0 Then
                AccountingCloseInfo = CreateBoFromGrid(0)
                AccountingCloseInfo.isDateEnable = False
                BindBoPropertiesToGridHeaders(AccountingCloseInfo)
                If moDataGrid.Items(0).Cells(2).Enabled Then
                    AccountingCloseInfo.isDateEnable = True
                End If
                PopulateBOFromForm(AccountingCloseInfo, AccountingCloseInfoCompaniesArrayList, 0)

                AccountingCloseInfo.Save()

            End If

            totItems = totItems - 1
            For index As Integer = 1 To totItems
                AccountingCloseInfo.isDateEnable = False
                If moDataGrid.Items(index).Cells(CLOSE_DATE_COL).Enabled Then
                    AccountingCloseInfo.isDateEnable = True
                End If
                AccountingCloseInfo = CreateBoFromGrid(index)
                PopulateBOFromForm(AccountingCloseInfo, AccountingCloseInfoCompaniesArrayList, index)

                AccountingCloseInfo.Save()
            Next

        End Sub

        Function IsDataGPageDirty() As Boolean
            Dim Result As String = HiddenIsPageDirty.Value

            Return Result.Equals("YES")
        End Function

        Public Function Get_A_New_Date() As AccountinIfoDataview
            Dim oDataTable As DataTable = AccountinIfoDataview.CreateTable
            Dim dv As AccountingCloseInfo.AccountingCloseInfoSearchDV = AccountingCloseInfo.GetLastClosingDate(State.CompanyId)
            Dim coverageRow As DataRow = dv.Table.Rows(0)
            Dim LastDate As Date
            If coverageRow(AccountingCloseInfo.AccountingCloseInfoSearchDV.COL_NAME_CLOSE_DATE) Is System.DBNull.Value Then
                LastDate = (DateAdd("YYYY", -1, DateAdd("m", -Month(Now) + 12, DateAdd("d", -Date.Today.Day + 1, Now))))
            Else
                LastDate = CType(coverageRow(AccountingCloseInfo.AccountingCloseInfoSearchDV.COL_NAME_CLOSE_DATE), Date)
            End If

            LoadLaterYear(oDataTable, LastDate)
            AddNewAccountingRow(oDataTable, LastDate)

            Return New AccountinIfoDataview(oDataTable)

        End Function
        Private Sub AddNewAccountingRow(oDataTable As DataTable, oDate As Date)
            For I As Integer = 1 To 12
                Dim dr As DataRow = oDataTable.NewRow()
                dr(AccountinIfoDataview.COL_ID) = Guid.Empty
                dr(AccountinIfoDataview.COL_NAME) = FormatDateTime(MiscUtil.LastFridayOfMonth((oDate.AddMonths(I))), DateFormat.ShortDate)
                'FormatDateTime(CDate(objDate), DateFormat.ShortDate)
                oDataTable.Rows.Add(dr)
            Next
            setdirty()
        End Sub
        Private Sub LoadLaterYear(oDataTable As DataTable, oDate As Date)
            State.year = DatePart("yyyy", oDate).ToString
            Dim NewYear As String = CType(CType(State.year, Integer) + 1, String)
            Dim itm As WebControls.ListItem = New WebControls.ListItem()
            itm.Text = NewYear
            itm.Value = NewYear
            moYearDropDownList.Items.Add(itm)
            setYearSelection(NewYear)
            Dim odv As DataView = GetDV()

            If odv.Count > 0 And odv.Count < 12 Then
                For i As Integer = 0 To odv.Count - 1
                    Dim dr As DataRow = oDataTable.NewRow()
                    dr(AccountinIfoDataview.COL_ID) = GuidControl.ByteArrayToGuid(odv.Item(i).Row.Item(DV_ID_COL))
                    dr(AccountinIfoDataview.COL_NAME) = odv.Item(i).Row.Item(DV_DATE_COL)
                    oDataTable.Rows.Add(dr)
                Next
            End If
        End Sub

        Private Sub setdirty()
            HiddenIsPageDirty.Value = "YES"
        End Sub

        Private Sub ValidateGrig()

            Dim SelectedYear As String = GetSelectedDescription(moYearDropDownList)
            Dim DuplicatedMothFound As Boolean = False

            For index As Integer = 0 To moDataGrid.Items.Count - 1
                Dim odate As TextBox = CType(moDataGrid.Items(index).Cells(CLOSE_DATE_COL).FindControl("moDateCompTextGrid"), TextBox)
                Dim EnterYear As String = (DatePart("yyyy", CType(odate.Text, Date)).ToString)
                Dim omonth As String = (DatePart("m", CType(odate.Text, Date)).ToString)
                DuplicatedMothFound = False
                For i As Integer = 0 To moDataGrid.Items.Count - 1
                    Dim tbdate As TextBox = CType(moDataGrid.Items(i).Cells(CLOSE_DATE_COL).FindControl("moDateCompTextGrid"), TextBox)
                    Dim smonth As String = (DatePart("m", CType(tbdate.Text, Date)).ToString)
                    If DuplicatedMothFound AndAlso omonth = smonth Then
                        Throw New GUIException("You enter a duplicated Month", Assurant.ElitaPlus.Common.ErrorCodes.GUI_ENTER_MONTH_ERROR)
                    End If
                    If omonth = smonth Then
                        DuplicatedMothFound = True
                    End If
                Next
                If SelectedYear <> EnterYear Then
                    Throw New GUIException("You cannot modify the year", Assurant.ElitaPlus.Common.ErrorCodes.GUI_ENTER_YEAR_ERROR)
                End If
            Next

        End Sub

        Private Sub ReturnPage()
            'CultureInfo.CurrentCulture
            Thread.CurrentThread.CurrentCulture = New CultureInfo(State.oCurrentCulture.ToString)
            If CallingParameters IsNot Nothing Then
                Dim oUser As User = ElitaPlusIdentity.Current.ActiveUser
                oUser.UpdateUserCompanies(State.OldCompanies)
                ElitaPlusIdentity.Current.ActiveUser.ResetUserCompany()
                Dim retType As New ClaimForm.ReturnType(ElitaPlusPage.DetailPageCommand.Back)
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

        Private Sub PopulateGrid(Optional ByVal blnPageState As Boolean = False)

            Dim dv As DataView
            Dim recCount As Integer = 0

            Try
                If State.isNew Then
                    State.isNew = blnPageState
                    dv = Get_A_New_Date()
                Else
                    If IsSingleCompanyUser() AndAlso State.year = "" Then
                        State.year = DatePart("yyyy", Now).ToString
                    End If
                    dv = GetDV(State.CompanyId)
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

        Private Function GetLastClosingDate() As AccountingCloseInfo.AccountingCloseInfoSearchDV

            With State
                Return (Assurant.ElitaPlus.BusinessObjectsNew.AccountingCloseInfo.GetLastClosingDate(.CompanyId))
            End With

        End Function

        Private Function GetGridDataView(Optional ByVal newCompanyID As Object = Nothing) As DataView

            With State
                If newCompanyID Is Nothing Then
                    Return (Assurant.ElitaPlus.BusinessObjectsNew.AccountingCloseInfo.GetAccountingCloseDates(.CompanyId, .year))
                Else
                    Return (Assurant.ElitaPlus.BusinessObjectsNew.AccountingCloseInfo.GetAccountingCloseDates(CType(newCompanyID, Guid), .year))
                    'Return (Assurant.ElitaPlus.BusinessObjectsNew.AccountingCloseInfo.GetAllAccountingCloseDates(CType(newCompanyID, Guid)))
                End If
            End With

        End Function

        Private Sub PopulateBOItem(AccountingCloseInfo As AccountingCloseInfo, oPropertyName As String, oCellPosition As Integer)
            PopulateBOProperty(AccountingCloseInfo, oPropertyName,
                                            CType(GetSelectedGridControl(moDataGrid, oCellPosition), TextBox))
        End Sub

        Private Sub PopulateBOFromForm(AccountingCloseInfo As AccountingCloseInfo, AccountingCloseInfoCompaniesArrayList As ArrayList, accountingCloseDateIndex As Integer)

            PopulateBOItem(AccountingCloseInfo, CLOSE_DATE_PROPERTY, CLOSE_DATE_COL)
            If State.isNew Then AccountingCloseInfo.CompanyId = State.CompanyId
            'Update the other company(s)
            If AccountingCloseInfoCompaniesArrayList IsNot Nothing AndAlso AccountingCloseInfoCompaniesArrayList.Count > 0 Then
                For i As Integer = 0 To AccountingCloseInfoCompaniesArrayList.Count - 1
                    Dim AccountingCloseInfosArrayList As ArrayList = CType(AccountingCloseInfoCompaniesArrayList.Item(i), ArrayList)
                    PopulateBOItem(CType(AccountingCloseInfosArrayList.Item(accountingCloseDateIndex), AccountingCloseInfo), CLOSE_DATE_PROPERTY, CLOSE_DATE_COL)
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

            ' Dim dv As DataView = AccountingCloseInfo.GetClosingYears(Me.State.CompanyId)
            Dim listcontext As ListContext = New ListContext()
            listcontext.CompanyId = State.CompanyId
            Dim YearListLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.ClosingYearsByCompany, ElitaPlusIdentity.Current.ActiveUser.LanguageCode, listcontext)
            moYearDropDownList.Populate(YearListLkl, New PopulateOptions() With
                 {
                .AddBlankItem = True,
                .ValueFunc = AddressOf .GetCode
                 })

            If IsSingleCompanyUser() Then
                If moCompanyDropDownList.SelectedIndex > 0 AndAlso State.isDateSectionLoading AndAlso YearListLkl.Count = 0 Then
                    tblContainer.Visible = True
                End If
            End If

        End Sub


        Private Sub PopulateCompaniesDropdown()

            Dim companyGroupID As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
            Dim selectedCompaniesDv As DataView = ElitaPlusIdentity.Current.ActiveUser.GetSelectedCompanies(companyGroupID, ElitaPlusIdentity.Current.ActiveUser.Id)
            'Me.BindListControlToDataView(Me.moCompanyDropDownList, selectedCompaniesDv, , , True) 'need to implement
            Dim listcontext As ListContext = New ListContext()
            listcontext.CompanyGroupId = companyGroupID
            listcontext.UserId = ElitaPlusIdentity.Current.ActiveUser.Id
            Dim compLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.CompanyByCompanyGroupAndUser, ElitaPlusIdentity.Current.ActiveUser.LanguageCode, listcontext)
            moCompanyDropDownList.Populate(compLkl, New PopulateOptions() With
                 {
                .AddBlankItem = True
                 })

        End Sub

        Private Function IsSingleCompanyUser() As Boolean
            Dim companyGroupID As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
            Dim selectedCompaniesDv As DataView = ElitaPlusIdentity.Current.ActiveUser.GetSelectedCompanies(companyGroupID, ElitaPlusIdentity.Current.ActiveUser.Id)

            If selectedCompaniesDv.Count = 1 Then
                Return True
            Else
                Return False
            End If

        End Function

        Private Sub setYearSelection(Year As String)
            moYearDropDownList.SelectedIndex = moYearDropDownList.Items.IndexOf(moYearDropDownList.Items.FindByText(Year))
        End Sub

        Private Sub setCompanySelection(CompanyName As String)
            moCompanyDropDownList.SelectedIndex = moCompanyDropDownList.Items.IndexOf(moCompanyDropDownList.Items.FindByText(CompanyName))
        End Sub


        Private Sub LocateNewCompany()

            Dim dv As DataView
            Dim ActiveUserCompanyIdDV As DataView = GetGridDataView()
            State.OtherCompanyId = Guid.Empty

            If ActiveUserCompanyIdDV.Count = 0 Then
                State.OtherCompanyId = State.CompanyId
                'Dim companyGroupID As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                'Dim ActiveUserOtherCompaniesArrayList As ArrayList = ElitaPlusIdentity.Current.ActiveUser.LoadUserOtherCompaniesIDs(ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID, companyGroupID)

                'For i As Integer = 0 To ActiveUserOtherCompaniesArrayList.Count - 1
                '    dv = GetGridDataView(CType(ActiveUserOtherCompaniesArrayList.Item(i), Object))
                '    If dv.Count > 0 Then
                '        Me.State.OtherCompanyId = CType(ActiveUserOtherCompaniesArrayList.Item(i), Guid)
                '        Exit For
                '    End If
                'Next
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

                    State.AccountingCloseInfoId = New Guid(moDataGrid.Items(e.Item.ItemIndex).Cells(ID_COL).Text)

                    State.MyBO = New Assurant.ElitaPlus.BusinessObjectsNew.AccountingCloseInfo(State.AccountingCloseInfoId)
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

            If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                With e.Item
                    PopulateControlFromBOProperty(.Cells(ID_COL), dvRow(AccountingCloseInfo.COL_NAME_ACCOUNTING_CLOSE_INFO_ID))
                    oTextBox = CType(e.Item.Cells(CLOSE_DATE_COL).FindControl("moDateCompTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Dim oDateCompImage As ImageButton = CType(e.Item.Cells(CLOSE_DATE_COL).FindControl("moDateCompImageGrid"), ImageButton)
                    Dim oDeleteCompImage As ImageButton = CType(e.Item.Cells(1).FindControl("DeleteButton_WRITE"), ImageButton)
                    If (oDateCompImage IsNot Nothing) Then
                        AddCalendar(oDateCompImage, oTextBox)
                    End If

                    PopulateControlFromBOProperty(oTextBox, dvRow(AccountingCloseInfo.COL_NAME_CLOSE_DATE))
                    If CDate(CType(dvRow(AccountingCloseInfo.COL_NAME_CLOSE_DATE), String)) < Now Then
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

        Protected Sub BindBoPropertiesToGridHeaders(AccountingCloseInfo As AccountingCloseInfo)
            BindBOPropertyToGridHeader(AccountingCloseInfo, AccountingCloseInfo.COL_NAME_CLOSE_DATE, moDataGrid.Columns(CLOSE_DATE_COL))
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
                    setYearSelection(State.year)

                    PopulateCompaniesDropdown()
                    setCompanySelection(State.CompanyName)
                End If

                btnNew_WRITE.Enabled = True
                State.isNew = False
                PopulateGrid()
                HiddenIsPageDirty.Value = EMPTY
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
                If IsDataGPageDirty() Then
                    DisplayMessage(Message.MSG_PAGE_SAVE_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSavePagePromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Accept
                Else
                    State.year = GetSelectedDescription(moYearDropDownList)
                    Dim SelectedYear As String = GetSelectedDescription(moYearDropDownList)
                    PopulateYearsDropdown()
                    setYearSelection(SelectedYear)
                    btnNew_WRITE.Enabled = True
                    If Not IsSingleCompanyUser() Then
                        State.CompanyId = New Guid(moCompanyDropDownList.SelectedItem.Value)
                    End If
                    PopulateGrid()
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrController)
            End Try
            '

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

        Public Class AccountinIfoDataview
            Inherits DataView

            Public Const COL_ID As String = "ACCOUNTING_CLOSE_INFO_ID"
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
