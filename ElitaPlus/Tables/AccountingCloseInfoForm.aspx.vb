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

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

#Region "Properties"

        Public ReadOnly Property IsEditing() As Boolean
            Get
                IsEditing = (Me.moDataGrid.EditItemIndex > Me.NO_ITEM_SELECTED_INDEX)
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
                Me.State.CompanyId = ElitaPlusIdentity.Current.ActiveUser.Company.Id
            Else
                If Me.State.SetClosingDateByCompany Then
                    Me.State.CompanyId = New Guid(moCompanyDropDownList.SelectedItem.Value)
                Else
                    Me.State.CompanyId = Guid.Empty
                End If
            End If
        End Sub

        Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
            Try
                If Not Me.CallingParameters Is Nothing Then
                    'Get the id from the parent
                    'Me.State.CompanyId = CType(Me.CallingParameters, Guid)
                    Me.State.OldCompanies = CType(Me.CallingParameters, ArrayList)
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrController)
            End Try

        End Sub

#End Region

#Region "Page Events"

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Try
                Me.ErrController.Clear_Hide()
                Me.SetStateProperties()
                If Not Page.IsPostBack Then
                    Me.SetGridItemStyleColor(moDataGrid)
                    Me.State.PageIndex = 0
                    Me.State.year = DatePart("yyyy", Now).ToString
                    Me.State.CompanyName = String.Empty
                    PopulateYearsDropdown()
                    PopulateCompaniesDropdown()
                    setYearSelection(Me.State.year)
                    setCompanySelection(Me.State.CompanyName)
                    If IsSingleCompanyUser() Then
                        Me.State.CompanyId = ElitaPlusIdentity.Current.ActiveUser.Company.Id
                        Me.State.SetClosingDateByCompany = True
                        moByCompanyLabel.Visible = False
                        moCompanyDropDownList.Visible = False
                        lblColon.Visible = False
                        tblContainer.Visible = True
                    Else
                        Me.State.SetClosingDateByCompany = False
                        lblRecordCount.Visible = False
                        moDataGrid.Visible = False
                        btnUndo_WRITE.Visible = False
                        SaveButton_WRITE.Visible = False
                        btnNew_WRITE.Visible = False
                        tblContainer.Visible = False
                    End If
                    Me.State.oCurrentCulture = New CultureInfo(CultureInfo.CurrentCulture.ToString())
                    'Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")
                    If Not Me.CallingParameters Is Nothing Then
                        Me.btnNew_WRITE.Enabled = False
                        Me.State.prevSelectedYear = Me.State.year
                        Me.State.prevSelectedCompanyName = Me.State.CompanyName
                        Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Undo
                        Me.State.isNew = True
                        LocateNewCompany()
                        PopulateGrid(True)
                    Else
                        PopulateGrid()
                    End If
                Else
                    CheckIfComingFromSaveConfirm()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrController)
            End Try
            Me.ShowMissingTranslations(ErrController)
        End Sub

#End Region

#Region "Controlling Logic"

        Protected Sub CheckIfComingFromSaveConfirm()

            Dim confResponse As String = Me.HiddenSavePagePromptResponse.Value

            If Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_YES Then
                If Me.State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr Then
                    Me.SavePage()
                End If
                Select Case Me.State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Back
                        ReturnPage()
                    Case ElitaPlusPage.DetailPageCommand.New_
                        Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                        Me.State.isNew = True
                        Me.PopulateGrid()
                End Select
            ElseIf Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_NO Then
                Me.HiddenIsPageDirty.Value = "NO"
                Select Case Me.State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Back
                        ReturnPage()
                    Case ElitaPlusPage.DetailPageCommand.New_
                        Me.State.isNew = True
                        Me.PopulateGrid()
                    Case ElitaPlusPage.DetailPageCommand.Accept
                        Me.State.isNew = False
                        Me.State.year = Me.GetSelectedDescription(Me.moYearDropDownList)
                        PopulateYearsDropdown()
                        setYearSelection(Me.State.year)
                        Me.State.CompanyName = Me.GetSelectedDescription(Me.moCompanyDropDownList)
                        PopulateCompaniesDropdown()
                        setCompanySelection(Me.State.CompanyName)
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
            Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
            Me.HiddenSavePagePromptResponse.Value = ""

        End Sub
        Private Function CreateBoFromGrid(ByVal index As Integer) As AccountingCloseInfo
            Dim AccountingCloseInfoId As Guid
            Dim AccountingCloseInfo As AccountingCloseInfo

            moDataGrid.SelectedIndex = index
            AccountingCloseInfoId = New Guid(moDataGrid.Items(index).Cells(Me.ID_COL).Text)

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

            If Me.State.SetClosingDateByCompany Then
                'When Closing Date is By Company, Set up the  Arraylist with current company
                ActiveUserOtherCompaniesArrayList = New ArrayList
                ActiveUserOtherCompaniesArrayList.Add(Me.State.CompanyId)
            Else
                'The Company Group functionality is deprecated and so the code below should never execute, just retaining this for consistency
                'When Closing Date is By Company Group, load the  Arraylist with all the companies in the current Company's Group
                ActiveUserOtherCompaniesArrayList = ElitaPlusIdentity.Current.ActiveUser.LoadUserOtherCompaniesIDs(ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID, companyGroupID)
            End If

            If ActiveUserOtherCompaniesArrayList.Count > 0 Then
                If Me.State.isNew Then
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
                                                CType(ActiveUserOtherCompaniesArrayList.Item(i), Guid), Me.State.year)
                        If Me.moDataGrid.Items.Count <> dv.Count Then
                            Throw New GUIException("", Assurant.ElitaPlus.Common.ErrorCodes.INVALID_RECORD_COUNT_ON_OTHER_COMPANY_ACCOUNTING_CLOSE_DATES_ERR)
                        End If
                        For j As Integer = 0 To 11
                            Dim accCloseInfoId As Guid = GuidControl.ByteArrayToGuid(dv.Table.Rows(j).Item(Me.DV_ID_COL))
                            AccountingCloseInfosArrayList.Add(New AccountingCloseInfo(accCloseInfoId))
                        Next
                        AccountingCloseInfoCompaniesArrayList.Add(AccountingCloseInfosArrayList)
                    Next
                End If
            End If

            Dim AccountingCloseInfo As AccountingCloseInfo
            Dim totItems As Integer = Me.moDataGrid.Items.Count

            If totItems > 0 Then
                AccountingCloseInfo = CreateBoFromGrid(0)
                AccountingCloseInfo.isDateEnable = False
                BindBoPropertiesToGridHeaders(AccountingCloseInfo)
                If Me.moDataGrid.Items(0).Cells(2).Enabled Then
                    AccountingCloseInfo.isDateEnable = True
                End If
                PopulateBOFromForm(AccountingCloseInfo, AccountingCloseInfoCompaniesArrayList, 0)

                AccountingCloseInfo.Save()

            End If

            totItems = totItems - 1
            For index As Integer = 1 To totItems
                AccountingCloseInfo.isDateEnable = False
                If Me.moDataGrid.Items(index).Cells(CLOSE_DATE_COL).Enabled Then
                    AccountingCloseInfo.isDateEnable = True
                End If
                AccountingCloseInfo = CreateBoFromGrid(index)
                PopulateBOFromForm(AccountingCloseInfo, AccountingCloseInfoCompaniesArrayList, index)

                AccountingCloseInfo.Save()
            Next

        End Sub

        Function IsDataGPageDirty() As Boolean
            Dim Result As String = Me.HiddenIsPageDirty.Value

            Return Result.Equals("YES")
        End Function

        Public Function Get_A_New_Date() As AccountinIfoDataview
            Dim oDataTable As DataTable = AccountinIfoDataview.CreateTable
            Dim dv As AccountingCloseInfo.AccountingCloseInfoSearchDV = AccountingCloseInfo.GetLastClosingDate(Me.State.CompanyId)
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
        Private Sub AddNewAccountingRow(ByVal oDataTable As DataTable, ByVal oDate As Date)
            For I As Integer = 1 To 12
                Dim dr As DataRow = oDataTable.NewRow()
                dr(AccountinIfoDataview.COL_ID) = Guid.Empty
                dr(AccountinIfoDataview.COL_NAME) = FormatDateTime(MiscUtil.LastFridayOfMonth((oDate.AddMonths(I))), DateFormat.ShortDate)
                'FormatDateTime(CDate(objDate), DateFormat.ShortDate)
                oDataTable.Rows.Add(dr)
            Next
            setdirty()
        End Sub
        Private Sub LoadLaterYear(ByVal oDataTable As DataTable, ByVal oDate As Date)
            Me.State.year = DatePart("yyyy", oDate).ToString
            Dim NewYear As String = CType(CType(Me.State.year, Integer) + 1, String)
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
            Me.HiddenIsPageDirty.Value = "YES"
        End Sub

        Private Sub ValidateGrig()

            Dim SelectedYear As String = Me.GetSelectedDescription(Me.moYearDropDownList)
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
            Thread.CurrentThread.CurrentCulture = New CultureInfo(Me.State.oCurrentCulture.ToString)
            If Not Me.CallingParameters Is Nothing Then
                Dim oUser As User = ElitaPlusIdentity.Current.ActiveUser
                oUser.UpdateUserCompanies(Me.State.OldCompanies)
                ElitaPlusIdentity.Current.ActiveUser.ResetUserCompany()
                Dim retType As New ClaimForm.ReturnType(ElitaPlusPage.DetailPageCommand.Back)
                Me.ReturnToCallingPage(retType)
            Else
                Me.ReturnToTabHomePage()
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
                If Me.State.isNew Then
                    Me.State.isNew = blnPageState
                    dv = Get_A_New_Date()
                Else
                    If IsSingleCompanyUser() AndAlso Me.State.year = "" Then
                        Me.State.year = DatePart("yyyy", Now).ToString
                    End If
                    dv = GetDV(Me.State.CompanyId)
                End If

                dv.Sort = Me.State.sortBy
                recCount = dv.Count
                Session("recCount") = recCount
                Me.TranslateGridControls(moDataGrid)
                Me.moDataGrid.DataSource = dv
                Me.moDataGrid.DataBind()
                Me.lblRecordCount.Text = recCount & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                ControlMgr.DisableAllGridControlsIfNotEditAuth(Me, moDataGrid)

                Me.State.year = Me.GetSelectedDescription(Me.moYearDropDownList)
                If Not Me.State.isNew Then
                    PopulateYearsDropdown()
                    setYearSelection(Me.State.year)
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrController)
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

        Private Sub PopulateBOItem(ByVal AccountingCloseInfo As AccountingCloseInfo, ByVal oPropertyName As String, ByVal oCellPosition As Integer)
            Me.PopulateBOProperty(AccountingCloseInfo, oPropertyName,
                                            CType(Me.GetSelectedGridControl(moDataGrid, oCellPosition), TextBox))
        End Sub

        Private Sub PopulateBOFromForm(ByVal AccountingCloseInfo As AccountingCloseInfo, ByVal AccountingCloseInfoCompaniesArrayList As ArrayList, ByVal accountingCloseDateIndex As Integer)

            PopulateBOItem(AccountingCloseInfo, CLOSE_DATE_PROPERTY, CLOSE_DATE_COL)
            If Me.State.isNew Then AccountingCloseInfo.CompanyId = Me.State.CompanyId
            'Update the other company(s)
            If Not AccountingCloseInfoCompaniesArrayList Is Nothing AndAlso AccountingCloseInfoCompaniesArrayList.Count > 0 Then
                For i As Integer = 0 To AccountingCloseInfoCompaniesArrayList.Count - 1
                    Dim AccountingCloseInfosArrayList As ArrayList = CType(AccountingCloseInfoCompaniesArrayList.Item(i), ArrayList)
                    PopulateBOItem(CType(AccountingCloseInfosArrayList.Item(accountingCloseDateIndex), AccountingCloseInfo), CLOSE_DATE_PROPERTY, CLOSE_DATE_COL)
                Next
            End If
            If Me.ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub

        Private Sub PopulateFormItem(ByVal oCellPosition As Integer, ByVal oPropertyValue As Object)
            Me.PopulateControlFromBOProperty(Me.GetSelectedGridControl(moDataGrid, oCellPosition), oPropertyValue)
        End Sub

        Private Sub PopulateYearsDropdown()

            ' Dim dv As DataView = AccountingCloseInfo.GetClosingYears(Me.State.CompanyId)
            Dim listcontext As ListContext = New ListContext()
            listcontext.CompanyId = Me.State.CompanyId
            Dim YearListLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.ClosingYearsByCompany, ElitaPlusIdentity.Current.ActiveUser.LanguageCode, listcontext)
            Me.moYearDropDownList.Populate(YearListLkl, New PopulateOptions() With
                 {
                .AddBlankItem = True,
                .ValueFunc = AddressOf .GetCode
                 })

            If IsSingleCompanyUser() Then
                If moCompanyDropDownList.SelectedIndex > 0 AndAlso Me.State.isDateSectionLoading AndAlso YearListLkl.Count = 0 Then
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
            Me.moCompanyDropDownList.Populate(compLkl, New PopulateOptions() With
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

        Private Sub setYearSelection(ByVal Year As String)
            moYearDropDownList.SelectedIndex = moYearDropDownList.Items.IndexOf(moYearDropDownList.Items.FindByText(Year))
        End Sub

        Private Sub setCompanySelection(ByVal CompanyName As String)
            moCompanyDropDownList.SelectedIndex = moCompanyDropDownList.Items.IndexOf(moCompanyDropDownList.Items.FindByText(CompanyName))
        End Sub


        Private Sub LocateNewCompany()

            Dim dv As DataView
            Dim ActiveUserCompanyIdDV As DataView = GetGridDataView()
            Me.State.OtherCompanyId = Guid.Empty

            If ActiveUserCompanyIdDV.Count = 0 Then
                Me.State.OtherCompanyId = Me.State.CompanyId
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

        Private Sub moDataGrid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles moDataGrid.PageIndexChanged
            Try
                Me.State.selectedPageIndex = e.NewPageIndex
                If IsDataGPageDirty() Then
                    DisplayMessage(Message.MSG_PAGE_SAVE_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, Me.HiddenSavePagePromptResponse)
                Else
                    Me.moDataGrid.CurrentPageIndex = e.NewPageIndex
                    PopulateGrid()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrController)
            End Try
        End Sub

        Private Sub moDataGrid_PageSizeChanged(ByVal source As System.Object, ByVal e As System.EventArgs)
            Try
                If IsDataGPageDirty() Then
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.GridPageSize
                    DisplayMessage(Message.MSG_PAGE_SAVE_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, Me.HiddenSavePagePromptResponse)
                Else
                    'moDataGrid.CurrentPageIndex = NewCurrentPageIndex(moDataGrid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                    'Me.State.selectedPageSize = CType(cboPageSize.SelectedValue, Integer)
                    'Me.PopulateGrid()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrController)
            End Try
        End Sub

        Protected Sub ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs)
            Try
                If (e.CommandName = SORT_COMMAND_NAME) Then
                    Me.State.sortBy = CType(e.CommandArgument, String)
                    PopulateGrid()
                ElseIf (e.CommandName = Me.DELETE_COMMAND) Then
                    'Do the delete here

                    'Clear the SelectedItemStyle to remove the highlight from the previously saved row
                    moDataGrid.SelectedIndex = Me.NO_ROW_SELECTED_INDEX

                    'Save the Id in the Session

                    Me.State.AccountingCloseInfoId = New Guid(Me.moDataGrid.Items(e.Item.ItemIndex).Cells(Me.ID_COL).Text)

                    Me.State.MyBO = New Assurant.ElitaPlus.BusinessObjectsNew.AccountingCloseInfo(Me.State.AccountingCloseInfoId)
                    Try
                        Me.State.MyBO.Delete()
                        'Call the Save() method in the AccountingCloseInfo Business Object here
                        Me.State.MyBO.Save()
                    Catch ex As Exception
                        Me.State.MyBO.RejectChanges()
                        Throw ex
                    End Try

                    PopulateGrid()

                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrController)
            End Try
        End Sub

        Protected Sub ItemBound(ByVal source As Object, ByVal e As DataGridItemEventArgs) Handles moDataGrid.ItemDataBound
            Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)
            Dim oTextBox As TextBox

            If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                With e.Item
                    Me.PopulateControlFromBOProperty(.Cells(Me.ID_COL), dvRow(AccountingCloseInfo.COL_NAME_ACCOUNTING_CLOSE_INFO_ID))
                    oTextBox = CType(e.Item.Cells(CLOSE_DATE_COL).FindControl("moDateCompTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Dim oDateCompImage As ImageButton = CType(e.Item.Cells(CLOSE_DATE_COL).FindControl("moDateCompImageGrid"), ImageButton)
                    Dim oDeleteCompImage As ImageButton = CType(e.Item.Cells(1).FindControl("DeleteButton_WRITE"), ImageButton)
                    If (Not oDateCompImage Is Nothing) Then
                        Me.AddCalendar(oDateCompImage, oTextBox)
                    End If

                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(AccountingCloseInfo.COL_NAME_CLOSE_DATE))
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

        Protected Sub ItemCreated(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
            BaseItemCreated(sender, e)
        End Sub

        Protected Sub BindBoPropertiesToGridHeaders(ByVal AccountingCloseInfo As AccountingCloseInfo)
            Me.BindBOPropertyToGridHeader(AccountingCloseInfo, AccountingCloseInfo.COL_NAME_CLOSE_DATE, Me.moDataGrid.Columns(Me.CLOSE_DATE_COL))
            Me.ClearGridHeadersAndLabelsErrSign()
        End Sub

        Private Sub SetFocusOnEditableFieldInGrid(ByVal grid As DataGrid, ByVal cellPosition As Integer, ByVal controlName As String, ByVal itemIndex As Integer)
            'Set focus on the Description TextBox for the EditItemIndex row
            Dim desc As TextBox = CType(grid.Items(itemIndex).Cells(cellPosition).FindControl(controlName), TextBox)
            SetFocus(desc)
        End Sub

#End Region

#Region "Button Click Events"

        Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
            Try
                If IsDataGPageDirty() Then
                    DisplayMessage(Message.MSG_PAGE_SAVE_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, Me.HiddenSavePagePromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    ReturnPage()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrController)
            End Try
        End Sub

        Private Sub SaveButton_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveButton_WRITE.Click
            Try
                If IsDataGPageDirty() Then
                    ValidateGrig()
                    SavePage()
                    Me.HiddenIsPageDirty.Value = EMPTY
                    Me.State.year = Me.GetSelectedDescription(Me.moYearDropDownList)
                    Me.State.isNew = False
                    PopulateGrid()
                    Me.DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                Else
                    Me.DisplayMessage(Message.MSG_RECORD_NOT_SAVED, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                End If

                Me.btnNew_WRITE.Enabled = True

            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrController)
            End Try
        End Sub

        Private Sub btnUndo_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUndo_WRITE.Click
            Try

                If Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Undo Then
                    PopulateYearsDropdown()
                    setYearSelection(Me.State.year)

                    PopulateCompaniesDropdown()
                    setCompanySelection(Me.State.CompanyName)
                End If

                Me.btnNew_WRITE.Enabled = True
                Me.State.isNew = False
                PopulateGrid()
                Me.HiddenIsPageDirty.Value = EMPTY
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrController)
            End Try
        End Sub

        Private Sub btnNew_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew_WRITE.Click

            Try
                Me.btnNew_WRITE.Enabled = False
                Me.State.prevSelectedYear = Me.State.year
                Me.State.prevSelectedCompanyName = Me.State.CompanyName

                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Undo

                If Not IsSingleCompanyUser() Then
                    lblRecordCount.Visible = True
                    moDataGrid.Visible = True
                    btnUndo_WRITE.Visible = True
                    SaveButton_WRITE.Visible = True
                End If

                If IsDataGPageDirty() Then
                    DisplayMessage(Message.MSG_PAGE_SAVE_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, Me.HiddenSavePagePromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
                Else
                    Me.State.isNew = True
                    LocateNewCompany()
                    PopulateGrid(True)
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrController)
            End Try
        End Sub

#End Region

#Region "DropDownHandlers"

        Private Sub moYearDropDownList_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles moYearDropDownList.SelectedIndexChanged

            Try
                tblContainer.Visible = True
                If IsDataGPageDirty() Then
                    DisplayMessage(Message.MSG_PAGE_SAVE_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, Me.HiddenSavePagePromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Accept
                Else
                    Me.State.year = Me.GetSelectedDescription(Me.moYearDropDownList)
                    Dim SelectedYear As String = Me.GetSelectedDescription(Me.moYearDropDownList)
                    PopulateYearsDropdown()
                    setYearSelection(SelectedYear)
                    Me.btnNew_WRITE.Enabled = True
                    If Not IsSingleCompanyUser() Then
                        Me.State.CompanyId = New Guid(moCompanyDropDownList.SelectedItem.Value)
                    End If
                    PopulateGrid()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrController)
            End Try
            '

        End Sub

        Private Sub moCompanyDropDownList_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles moCompanyDropDownList.SelectedIndexChanged

            Try
                Me.State.SetClosingDateByCompany = True
                tblContainer.Visible = False
                If IsDataGPageDirty() Then
                    DisplayMessage(Message.MSG_PAGE_SAVE_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, Me.HiddenSavePagePromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Accept
                Else
                    If Not IsSingleCompanyUser() Then
                        lblRecordCount.Visible = True
                        moDataGrid.Visible = True
                        btnUndo_WRITE.Visible = True
                        SaveButton_WRITE.Visible = True
                        btnNew_WRITE.Visible = True
                    End If
                    Me.State.CompanyName = Me.GetSelectedDescription(Me.moCompanyDropDownList)
                    Dim SelectedCompanyName As String = Me.GetSelectedDescription(Me.moCompanyDropDownList)
                    PopulateCompaniesDropdown()
                    setCompanySelection(SelectedCompanyName)
                    Me.btnNew_WRITE.Enabled = True
                    Me.State.CompanyId = New Guid(moCompanyDropDownList.SelectedItem.Value)
                    Me.State.isDateSectionLoading = True
                    PopulateGrid()
                    Me.State.isDateSectionLoading = False
                    moYearDropDownList.SelectedIndex = -1
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrController)
            End Try

        End Sub


#End Region

#Region "Dataview"

        Public Class AccountinIfoDataview
            Inherits DataView

            Public Const COL_ID As String = "ACCOUNTING_CLOSE_INFO_ID"
            Public Const COL_NAME As String = "Description"
            Public Const NEW_DATE As String = "New"

            Public Sub New(ByVal t As DataTable)
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
