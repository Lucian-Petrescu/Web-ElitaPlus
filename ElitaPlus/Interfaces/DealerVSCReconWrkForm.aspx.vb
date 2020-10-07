Imports Assurant.ElitaPlus.DALObjects
Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Namespace Interfaces

    Partial Class DealerVSCReconWrkForm
        Inherits ElitaPlusSearchPage

#Region "Constants"
        Public Const URL As String = "DealerVSCReconWrkForm.aspx"
        Private Const CANCELLATIONS_TYPE As String = "N"

        Private Const MSG_CONFIRM_PROMPT As String = "MSG_CONFIRM_PROMPT"
        Private Const MSG_RECORD_DELETED_OK As String = "MSG_RECORD_DELETED_OK"
        Private Const MSG_RECORD_SAVED_OK As String = "MSG_RECORD_SAVED_OK"
        Private Const MSG_RECORD_NOT_SAVED As String = "MSG_RECORD_NOT_SAVED"

        Private Const ID_COL As Integer = 0
        Private Const RECORD_TYPE_COL As Integer = 1
        Private Const REJECT_CODE_COL As Integer = 2
        Private Const REJECT_REASON_COL As Integer = 3
        Private Const COMPANY_CODE_COL As Integer = 4
        Private Const DEALER_CODE_COL As Integer = 5
        Private Const BRANCH_CODE_COL As Integer = 6
        Private Const CERTIFICATE_NUMBER_COL As Integer = 7
        Private Const CUSTOMER_NAME_COL As Integer = 8
        Private Const ADDRESS1_COL As Integer = 9
        Private Const CITY_COL As Integer = 10
        Private Const POSTAL_CODE_COL As Integer = 11
        Private Const REGION_COL As Integer = 12
        Private Const COUNTRY_CODE_COL As Integer = 13
        Private Const HOME_PHONE_COL As Integer = 14
        Private Const MODEL_YEAR_COL As Integer = 15
        Private Const MANUFACTURER_COL As Integer = 16
        Private Const MODEL_COL As Integer = 17
        Private Const VIN_COL As Integer = 18
        Private Const ENGINE_VERSION_COL As Integer = 19
        Private Const EXTERNAL_CAR_CODE_COL As Integer = 20
        Private Const VEHICLE_LICENSE_TAG_COL As Integer = 21
        Private Const ODOMETER_COL As Integer = 22
        Private Const PURCHASE_PRICE_COL As Integer = 23
        Private Const PURCHASE_DATE_COL As Integer = 24
        Private Const IN_SERVICE_DATE_COL As Integer = 25
        Private Const DELIVERY_DATE_COL As Integer = 26
        Private Const PLAN_CODE_COL As Integer = 27
        Private Const DEDUCTIBLE_COL As Integer = 28
        Private Const TERM_MONTHS_COL As Integer = 29
        Private Const TERM_KM_MI_COL As Integer = 30
        Private Const AGENT_NUMBER_COL As Integer = 31
        Private Const WARRANTY_SALE_DATE_COL As Integer = 32
        Private Const PLAN_AMOUNT_COL As Integer = 33
        Private Const DOCUMENT_TYPE_COL As Integer = 34
        Private Const IDENTITY_DOC_NO_COL As Integer = 35
        Private Const RG_NO_COL As Integer = 36
        Private Const ID_TYPE_COL As Integer = 37
        Private Const DOCUMENT_ISSUE_DATE_COL As Integer = 38
        Private Const DOCUMENT_AGENCY_COL As Integer = 39
        Private Const NEW_USED_COL As Integer = 40
        Private Const OPTIONAL_COVERAGE_COL As Integer = 41
        Private Const BIRTH_DATE_COL As Integer = 42
        Private Const WORK_PHONE_COL As Integer = 43
        Private Const PAYMENT_TYPE_COL As Integer = 44
        Private Const PAYMENT_INSTRUMENT_COL As Integer = 45
        Private Const INSTALLMENT_NUMBER_COL As Integer = 46
        Private Const PLAN_AMOUNT_WITH_MARKUP_COL As Integer = 47
        Private Const PAYMENT_DATE_COL As Integer = 48
        Private Const CANCELLATION_DATE_COL As Integer = 49
        Private Const CANCELLATION_REASON_CODE_COL As Integer = 50
        Private Const CANCEL_COMMENT_TYPE_CODE_COL As Integer = 51
        Private Const CANCELLATION_COMMENT_COL As Integer = 52
        Private Const MODIFIED_DATE_COL As Integer = 53

        Private Const FINANCING_AGENCY_COL As Integer = 54
        Private Const BANK_ID_COL As Integer = 55
        Private Const NC_PAYMENT_METHOD_CODE_COL As Integer = 56
        Private Const NAME_ON_ACCOUNT_COL As Integer = 57
        Private Const ACCOUNT_TYPE_CODE_COL As Integer = 58
        Private Const TAX_ID_COL As Integer = 59
        Private Const BRANCH_DIGIT_COL As Integer = 60
        Private Const ACCOUNT_DIGIT_COL As Integer = 61
        Private Const REFUND_AMOUNT_COL As Integer = 62

        Private Const RECORD_TYPE_CONTROL_NAME As String = "moRecordTypeTextGrid"

        Private Const EMPTY As String = ""
        Private Const DEFAULT_PAGE_INDEX As Integer = 0

        ' Property Name

        Private Const DEALER_VSC_RECON_WRK_ID As String = "dealer_vsc_recon_wrk_id"
        Private Const DEALERFILE_PROCESSED_ID As String = "dealerfile_processed_id"
        Private Const ENROLLMENT_ID As String = "enrollment_id"
        Private Const REJECT_CODE As String = "reject_code"
        Private Const REJECT_MSG_PARMS As String = "reject_msg_parms"
        Private Const REJECT_REASON As String = "reject_reason"
        Private Const CERTIFICATE_LOADED As String = "certificate_loaded"
        Private Const DEALER_ID As String = "dealer_id"
        Private Const USER_ID As String = "user_id"
        Private Const RECORD_TYPE As String = "record_type"
        Private Const COMPANY_CODE As String = "company_code"
        Private Const CERTIFICATE_NUMBER As String = "certificate_number"
        Private Const CUSTOMERS As String = "customers"
        Private Const ADDRESS1 As String = "address1"
        Private Const CITY As String = "city"
        Private Const REGION As String = "region"
        Private Const POSTAL_CODE As String = "postal_code"
        Private Const COUNTRY_CODE As String = "country_code"
        Private Const HOME_PHONE As String = "home_phone"
        Private Const MODEL_YEAR As String = "model_year"
        Private Const MODEL As String = "model"
        Private Const MANUFACTURER As String = "manufacturer"
        Private Const ENGINE_VERSION As String = "engine_version"
        Private Const EXTERNAL_CAR_CODE As String = "external_car_code"
        Private Const VEHICLE_LICENSE_TAG As String = "vehicle_license_tag"
        Private Const ODOMETER As String = "odometer"
        Private Const VIN As String = "vin"
        Private Const PURCHASE_PRICE As String = "purchase_price"
        Private Const PURCHASE_DATE As String = "purchase_date"
        Private Const IN_SERVICE_DATE As String = "in_service_date"
        Private Const DELIVERY_DATE As String = "delivery_date"
        Private Const PLAN_CODE As String = "plan_code"
        Private Const DEDUCTIBLE As String = "deductible"
        Private Const TERM_MONTHS As String = "term_months"
        Private Const TERM_KM_MI As String = "term_km_mi"
        Private Const AGENT_NUMBER As String = "agent_number"
        Private Const WARRANTY_SALE_DATE As String = "warranty_sale_date"
        Private Const PLAN_AMOUNT As String = "plan_amount"
        Private Const DOCUMENT_TYPE As String = "document_type"
        Private Const IDENTITY_DOC_NO As String = "identity_doc_no"
        Private Const RG_NO As String = "rg_no"
        Private Const ID_TYPE As String = "id_type"
        Private Const DOCUMENT_ISSUE_DATE As String = "document_issue_date"
        Private Const DOCUMENT_AGENCY As String = "document_agency"
        Private Const QUOTE_NUMBER As String = "quote_number"
        Private Const QUOTE_ITEM_NUMBER As String = "quote_item_number"
        Private Const NEW_USED As String = "new_used"
        Private Const OPTIONAL_COVERAGE As String = "optional_coverage"
        Private Const BIRTH_DATE As String = "birth_date"
        Private Const WORK_PHONE As String = "work_phone"
        Private Const PAYMENT_TYPE_CODE As String = "payment_type_code"
        Private Const PAYMENT_INSTRUMENT_CODE As String = "payment_instrument_code"
        Private Const INSTALLMENT_NUMBER As String = "installments_number"


        Private Const PLAN_AMOUNT_WITH_MARKUP As String = "plan_amount_with_markup"
        Private Const PAYMENT_DATE As String = "payment_date"
        Private Const CANCELLATION_DATE As String = "cancellation_date"
        Private Const CANCELLATION_REASON_CODE As String = "cancellation_reason_code"
        Private Const CANCEL_COMMENT_TYPE_CODE As String = "cancel_comment_type_code"
        Private Const CANCELLATION_COMMENT As String = "cancellation_comment"

        Private Const FINANCING_AGENCY As String = "financing_agency"
        Private Const BANK_ID As String = "bank_id"
        Private Const NC_PAYMENT_METHOD_CODE As String = "nc_payment_method_code"
        Private Const NAME_ON_ACCOUNT As String = "name_on_account"
        Private Const ACCOUNT_TYPE_CODE As String = "account_type_code"
        Private Const TAX_ID As String = "tax_id"
        Private Const BRANCH_DIGIT As String = "branch_digit"
        Private Const ACCOUNT_DIGIT As String = "account_digit"
        Private Const REFUND_AMOUNT As String = "refund_amount"


        Private Const BRANCH_CODE As String = "branch_code"

        Private Const FIRST_POS As Integer = 0
        Private Const COL_NAME As String = "ID"

#End Region

#Region "Member Variables"

        Protected TempDataView As DataView = New DataView
        Private Shared pageIndex As Integer
        Private Shared pageCount As Integer
        Public selectedPageSize As Integer = DEFAULT_PAGE_SIZE
        Protected WithEvents btnSave As System.Web.UI.WebControls.Button
        Protected WithEvents btnUndo As System.Web.UI.WebControls.Button
        Protected WithEvents LbPage As System.Web.UI.WebControls.Label
        Protected WithEvents Button1 As System.Web.UI.WebControls.Button
        Protected WithEvents Button2 As System.Web.UI.WebControls.Button
        Public selectedPageIndex As Integer = DEFAULT_PAGE_INDEX
        Protected WithEvents tsHoriz As Microsoft.Web.UI.WebControls.TabStrip
        Protected WithEvents ddlCancellationReason As System.Web.UI.WebControls.DropDownList
        Protected WithEvents mpHoriz As Microsoft.Web.UI.WebControls.MultiPage


#End Region

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        'NOTE: The following placeholder declaration is required by the Web Form Designer.
        'Do not delete or move it.
        Private designerPlaceholderDeclaration As System.Object

        Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region
        'Protected WithEvents ErrController As ErrorController
        'Protected WithEvents ErrController2 As ErrorController

#Region "Properties"

        Public ReadOnly Property IsEditing() As Boolean
            Get
                IsEditing = (moDataGrid.EditIndex > NO_ITEM_SELECTED_INDEX)
            End Get
        End Property

        Private Shadows ReadOnly Property ThePage() As ElitaPlusSearchPage
            Get
                Return CType(MyBase.Page, ElitaPlusSearchPage)
            End Get
        End Property

        Public Property SortDirection() As String
            Get
                Return ViewState("SortDirection").ToString
            End Get
            Set(value As String)
                ViewState("SortDirection") = value
            End Set
        End Property


#End Region

#Region "Page State"

        Private Class PageStatus

            Public Sub New()
                pageIndex = 0
                pageCount = 0
            End Sub

        End Class

        Class MyState
            Public SortExpression As String = Assurant.ElitaPlus.BusinessObjectsNew.User.UserSearchDV.COL_NETWORK_ID
            Public PageIndex As Integer = 0
            Public DealerfileProcessedId As Guid
            Public BundlesHashTable As Hashtable
            Public DealerVscReconWrkId As Guid
            Public ActionInProgress As DetailPageCommand = ElitaPlusPage.DetailPageCommand.Nothing_
            Public selectedPageSize As Integer = DEFAULT_NEW_UI_PAGE_SIZE
            Public selectedPageIndex As Integer = DEFAULT_PAGE_INDEX
            Public sortBy As String
            Public comingFromBundlesScreen As String = ""
            Public DealerGrpId As Guid
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
            State.DealerfileProcessedId = CType(CallingParameters, Guid)
        End Sub

#End Region

#Region "Page Events"

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            MasterPage.MessageController.Clear_Hide()
            'Me.ErrController2.Clear_Hide()
            SetStateProperties()

            If Not Page.IsPostBack Then
                MasterPage.MessageController.Clear()
                MasterPage.UsePageTabTitleInBreadCrum = False
                MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Interfaces")
                UpdateBreadCrum()
                SortDirection = EMPTY
                SetGridItemStyleColor(moDataGrid)
                ShowMissingTranslations(MasterPage.MessageController)
                State.PageIndex = 0
                TranslateGridHeader(moDataGrid)
                TranslateGridHeader(gvPop)
                TranslateGridControls(moDataGrid)
                PopulateReadOnly()
                cboPageSize.SelectedValue = CType(State.selectedPageSize, String)
                PopulateGrid()

            Else
                CheckIfComingFromSaveConfirm()
                'CheckIfComingFromBundlesScreen()
            End If
        End Sub

#End Region

#Region "Controlling Logic"

        Protected Sub AssignDropDownToCtr(control As System.Web.UI.WebControls.WebControl, textbox As System.Web.UI.WebControls.WebControl, Optional ByVal caller As String = "")
            Dim AppPath As String = Request.ApplicationPath
            Dim ServerName As String = Request.ServerVariables("SERVER_NAME")
            Dim url As String = ELPWebConstants.APPLICATION_PATH & "/Common/CalendarForm.aspx"
            control.Attributes.Add("onchange", "javascript:UpdateCtr(this,'" & textbox.UniqueID.Replace(":", "_") & "')")
            textbox.Attributes.Add("onkeyup", "javascript:UpdateDropDownCtr(this,'" & control.UniqueID.Replace(":", "_") & "')")
        End Sub

        Protected Sub CheckIfComingFromSaveConfirm()
            Dim confResponse As String = HiddenSavePagePromptResponse.Value
            Try
                If Not confResponse.Equals(EMPTY) Then
                    If confResponse = MSG_VALUE_YES Then
                        SavePage()
                        HiddenIsBundlesPageDirty.Value = EMPTY
                        'Me.HiddenIfComingFromBundlesScreen.Value = EMPTY
                    ElseIf confResponse = MSG_VALUE_NO Then
                        State.BundlesHashTable = Nothing
                    End If
                    HiddenSavePagePromptResponse.Value = EMPTY
                    HiddenIsPageDirty.Value = EMPTY

                    Select Case State.ActionInProgress
                        Case ElitaPlusPage.DetailPageCommand.Back
                            Dim retType As New DealerFileProcessedController_New.ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.DealerfileProcessedId)
                            ReturnToCallingPage(retType)
                        Case ElitaPlusPage.DetailPageCommand.GridPageSize
                            moDataGrid.PageIndex = NewCurrentPageIndex(moDataGrid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                            State.selectedPageSize = CType(cboPageSize.SelectedValue, Integer)
                        Case ElitaPlusPage.DetailPageCommand.GridColSort
                            'Me.State.sortBy = CType(e.CommandArgument, String)
                        Case Else
                            moDataGrid.PageIndex = State.selectedPageIndex
                    End Select
                    PopulateGrid()
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Function CreateBoFromGrid(index As Integer) As DealerVscReconWrk
            Dim DealerVscReconWrkId As Guid
            Dim DealerVscReconWrkInfo As DealerVscReconWrk
            Dim sModifiedDate As String

            moDataGrid.SelectedIndex = index
            DealerVscReconWrkId = New Guid(CType(moDataGrid.Rows(index).FindControl("moDealerVscReconWrkIdLabel"), Label).Text)
            sModifiedDate = GetGridText(moDataGrid, index, MODIFIED_DATE_COL)
            DealerVscReconWrkInfo = New DealerVscReconWrk(DealerVscReconWrkId, sModifiedDate)
            Return DealerVscReconWrkInfo
        End Function

        Private Sub SavePage()
            Dim totc As Integer = moDataGrid.Columns.Count()
            Dim index As Integer = 0
            Dim DealerVscReconWrkInfo As DealerVscReconWrk
            Dim totItems As Integer = moDataGrid.Rows.Count

            If totItems > 0 Then
                DealerVscReconWrkInfo = CreateBoFromGrid(0)
                BindBoPropertiesToGridHeaders(DealerVscReconWrkInfo)
                PopulateBOFromForm(DealerVscReconWrkInfo)
                DealerVscReconWrkInfo.Save()
            End If

            totItems = totItems - 1
            For index = 1 To totItems
                DealerVscReconWrkInfo = CreateBoFromGrid(index)
                BindBoPropertiesToGridHeaders(DealerVscReconWrkInfo)
                PopulateBOFromForm(DealerVscReconWrkInfo)
                DealerVscReconWrkInfo.Save()
            Next
        End Sub

        Function IsDataGPageDirty() As Boolean
            Dim Result As String = HiddenIsPageDirty.Value
            Return Result.Equals("YES")
        End Function

        Private Sub SetColumnState(column As Byte, state As Boolean)
            moDataGrid.Columns(column).Visible = state
        End Sub

        'Function IsCancellationsFileType(ByVal fileName As String) As Boolean
        '    Return fileName.Substring(4, 1).Equals(CANCELLATIONS_TYPE)
        'End Function

        'Private Sub CheckFileTypeColums()
        '    If IsCancellationsFileType(moFileNameText.Text) Then
        '        SetColumnState(RECORD_TYPE_COL, False)
        '        SetColumnState(ITEM_CODE_COL, False)
        '        SetColumnState(ITEM_COL, False)
        '        SetColumnState(SR_COL, False)
        '        SetColumnState(BRANCH_CODE_COL, False)
        '        SetColumnState(IDENTIFICATION_NUMBER_COL, False)
        '        SetColumnState(ADDRESS1_COL, False)
        '        SetColumnState(CITY_COL, False)
        '        SetColumnState(HOME_PHONE_COL, False)
        '        SetColumnState(WORK_PHONE_COL, False)
        '        SetColumnState(MANUFACTURER_COL, False)
        '        SetColumnState(MODEL_COL, False)
        '        SetColumnState(SERIAL_NUMBER_COL, False)
        '        SetColumnState(NEW_PRODUCT_CODE_COL, False)
        '        SetColumnState(DOCUMENT_TYPE_COL, False)
        '        SetColumnState(DOCUMENT_AGENCY_COL, False)
        '        SetColumnState(DOCUMENT_ISSUE_DATE_COL, False)
        '        SetColumnState(RG_NUMBER_COL, False)
        '        SetColumnState(ID_TYPE_COL, False)
        '        SetColumnState(NEW_BRANCH_CODE_COL, False)
        '        SetColumnState(BILLING_FREQUENCY_COL, False)
        '        SetColumnState(NUMBER_OF_INSTALLMENTS_COL, False)
        '        SetColumnState(INSTALLMENT_AMOUNT_COL, False)
        '        SetColumnState(BANK_RTN_NUMBER_COL, False)
        '        SetColumnState(BANK_ACCOUNT_NUMBER_COL, False)
        '        SetColumnState(BANK_ACCT_OWNER_NAME_COL, False)

        '        SetColumnState(SALES_TAX_COL, False)
        '        SetColumnState(EMAIL_COL, False)
        '        SetColumnState(ADDRESS2_COL, False)
        '        SetColumnState(CUST_COUNTRY_COL, False)
        '        SetColumnState(COUNTRY_PURCH_COL, False)

        '        SetColumnState(ADDRESS3_COL, False)
        '        SetColumnState(ORIGINAL_RETAIL_PRICE_COL, False)
        '        SetColumnState(BILLING_PLAN_COL, False)
        '        SetColumnState(BILLING_CYCLE_COL, False)
        '        SetColumnState(SUBSCRIBER_STATUS_COL, False)

        '        SetColumnState(MOBILE_TYPE_COL, False)
        '        SetColumnState(FIRST_USE_DATE_COL, False)
        '        SetColumnState(LAST_USE_DATE_COL, False)
        '        SetColumnState(SIM_CARD_NUMBER_COL, False)
        '        SetColumnState(REGION_COL, False)
        '    Else
        '        SetColumnState(CANCELLATION_CODE_COL, False)
        '    End If
        'End Sub

        Private Sub UpdateBreadCrum()

            MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("DEALER_FILE")
            MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("DEALER_FILE")

        End Sub

#End Region

#Region "Populate"

        Sub PopulateRecordTypeDrop(recordTypeDrop As DropDownList)
            Try
                Dim oLangId As Guid = Authentication.LangId
                ' Dim recordTypeList As DataView = LookupListNew.GetRecordTypeLookupList(oLangId)
                'Me.BindListControlToDataView(recordTypeDrop, recordTypeList, , , False)
                recordTypeDrop.Populate(CommonConfigManager.Current.ListManager.GetList("RECTYP", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions())
            Catch ex As Exception
                ThePage.HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        'Sub PopulateCancellationReasonDropDown()
        '    Try
        '        Dim oCompanyId As Guid = DealerVscReconWrk.CompanyId(Me.State.DealerfileProcessedId)
        '        TempDataView = LookupListNew.GetCancellationReasonDealerFileLookupList(oCompanyId)
        '        TempDataView.Sort = "DESCRIPTION"
        '        TempDataView.AddNew()
        '    Catch ex As Exception
        '        ThePage.HandleErrors(ex, Me.ErrController)
        '    End Try
        'End Sub

        Private Sub PopulateReadOnly()
            Try
                Dim oDealerFile As DealerFileProcessed = New DealerFileProcessed(State.DealerfileProcessedId)
                With oDealerFile
                    If Not .DealerId = Guid.Empty Then
                        moDealerNameText.Text = .DealerNameLoad
                        moFileNameText.Text = .Filename
                        ControlMgr.SetVisibleControl(ThePage, moDealerNameLabel, True)
                        ControlMgr.SetVisibleControl(ThePage, moDealerNameText, True)
                        ControlMgr.SetVisibleControl(ThePage, moDealerGrpNameLabel, False)
                        ControlMgr.SetVisibleControl(ThePage, moDealerGrpNameText, False)
                        If Not moDealerNameLabel.Text.EndsWith(":") Then
                            moDealerNameLabel.Text = moDealerNameLabel.Text & ":"
                        End If
                    Else
                        moDealerGrpNameText.Text = .DealerGroupNameLoad
                        moFileNameText.Text = .Filename
                        ControlMgr.SetVisibleControl(ThePage, moDealerNameLabel, False)
                        ControlMgr.SetVisibleControl(ThePage, moDealerNameText, False)
                        ControlMgr.SetVisibleControl(ThePage, moDealerGrpNameLabel, True)
                        ControlMgr.SetVisibleControl(ThePage, moDealerGrpNametext, True)
                        State.DealerGrpId = .DealerGroupId
                        If Not moDealerGrpNameLabel.Text.EndsWith(":") Then
                            moDealerGrpNameLabel.Text = moDealerGrpNameLabel.Text & ":"
                        End If
                    End If

                End With
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub PopulateGrid()

            Dim dv As DataView
            Dim recCount As Integer = 0

            Try
                dv = GetDV()
                'dv.Sort = Me.State.sortBy
                If Not SortDirection.Equals(EMPTY) Then
                    dv.Sort = SortDirection
                    HighLightSortColumn(moDataGrid, SortDirection)
                End If
                recCount = dv.Count
                Session("recCount") = recCount
                ' Me.moDataGrid.PageSize = Me.State.selectedPageSize

                SetPageAndSelectedIndexFromGuid(dv, State.DealerfileProcessedId, moDataGrid, State.PageIndex)

                moDataGrid.DataSource = dv
                moDataGrid.DataBind()
                lblRecordCount.Text = recCount & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)

                If Not moDataGrid.BottomPagerRow.Visible Then moDataGrid.BottomPagerRow.Visible = True
                ControlMgr.DisableAllGridControlsIfNotEditAuth(Me, moDataGrid)

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Private Function GetDV() As DataView

            Dim dv As DataView

            dv = GetGridDataView()
            dv.Sort = moDataGrid.DataMember()

            Return (dv)

        End Function

        Private Function GetGridDataView() As DataView

            With State
                Return (Assurant.ElitaPlus.BusinessObjectsNew.DealerVscReconWrk.LoadList(.DealerfileProcessedId))
            End With

        End Function


        Private Sub PopulateBOItem(DealerVscReconWrkInfo As DealerVscReconWrk, oPropertyName As String, oCellPosition As Integer)
            PopulateBOProperty(DealerVscReconWrkInfo, oPropertyName,
                                            CType(GetSelectedGridControl(moDataGrid, oCellPosition), TextBox))
        End Sub

        Private Sub PopulateBODrop(DealerVscReconWrkInfo As DealerVscReconWrk, oPropertyName As String, oCellPosition As Integer)
            PopulateBOProperty(DealerVscReconWrkInfo, oPropertyName,
                                CType(GetSelectedGridControl(moDataGrid, oCellPosition), DropDownList), False)
        End Sub

        Private Sub PopulateBOFromForm(DealerVscReconWrkInfo As DealerVscReconWrk)
            Dim dealerid As Guid

            PopulateBODrop(DealerVscReconWrkInfo, "RecordType", (RECORD_TYPE_COL))
            'PopulateBOItem(DealerVscReconWrkInfo, "RecordType", (Me.RECORD_TYPE_COL))
            'PopulateBOItem(DealerVscReconWrkInfo, "RejectCode", (Me.REJECT_CODE_COL))
            'PopulateBOItem(DealerVscReconWrkInfo, "RejectReason", (Me.REJECT_REASON_COL))
            'PopulateBOItem(DealerVscReconWrkInfo, "CompanyCode", (Me.COMPANY_CODE_COL))
            If Not State.DealerGrpId = Guid.Empty Then
                dealerid = Dealer.GetDealerIDbyCodeAndDealerGroup(State.DealerGrpId, CType(GetSelectedGridControl(moDataGrid, (DEALER_CODE_COL)), TextBox).Text.Trim.ToString)
                PopulateBOProperty(DealerVscReconWrkInfo, "DealerId", dealerid)
                'Else
                '   PopulateBOItem(DealerVscReconWrkInfo, "DealerId", Me.DEALER_CODE_COL)
            End If
            PopulateBOItem(DealerVscReconWrkInfo, "BranchCode", (BRANCH_CODE_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "CertificateNumber", (CERTIFICATE_NUMBER_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "Customers", (CUSTOMER_NAME_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "Address1", (ADDRESS1_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "City", (CITY_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "PostalCode", (POSTAL_CODE_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "Region", (REGION_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "CountryCode", (COUNTRY_CODE_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "HomePhone", (HOME_PHONE_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "ModelYear", (MODEL_YEAR_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "Manufacturer", (MANUFACTURER_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "Model", (MODEL_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "Vin", (VIN_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "EngineVersion", (ENGINE_VERSION_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "ExternalCarCode", (EXTERNAL_CAR_CODE_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "VehicleLicenseTag", (VEHICLE_LICENSE_TAG_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "Odometer", (ODOMETER_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "PurchasePrice", (PURCHASE_PRICE_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "PurchaseDate", (PURCHASE_DATE_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "InServiceDate", (IN_SERVICE_DATE_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "DeliveryDate", (DELIVERY_DATE_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "PlanCode", (PLAN_CODE_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "Deductible", (DEDUCTIBLE_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "TermMonths", (TERM_MONTHS_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "TermKmMi", (TERM_KM_MI_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "AgentNumber", (AGENT_NUMBER_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "WarrantySaleDate", (WARRANTY_SALE_DATE_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "PlanAmount", (PLAN_AMOUNT_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "DocumentType", (DOCUMENT_TYPE_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "IdentityDocNo", (IDENTITY_DOC_NO_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "RgNo", (RG_NO_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "IdType", (ID_TYPE_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "DocumentIssueDate", (DOCUMENT_ISSUE_DATE_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "DocumentAgency", (DOCUMENT_AGENCY_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "NewUsed", (NEW_USED_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "OptionalCoverage", (OPTIONAL_COVERAGE_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "BirthDate", (BIRTH_DATE_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "WorkPhone", (WORK_PHONE_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "PaymentTypeCode", (PAYMENT_TYPE_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "PaymentInstrumentCode", (PAYMENT_INSTRUMENT_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "InstallmentsNumber", (INSTALLMENT_NUMBER_COL))

            PopulateBOItem(DealerVscReconWrkInfo, "PlanAmountWithMarkup", (PLAN_AMOUNT_WITH_MARKUP_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "PaymentDate", (PAYMENT_DATE_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "CancellationDate", (CANCELLATION_DATE_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "CancellationReasonCode", (CANCELLATION_REASON_CODE_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "CancelCommentTypeCode", (CANCEL_COMMENT_TYPE_CODE_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "CancellationComment", (CANCELLATION_COMMENT_COL))

            PopulateBOItem(DealerVscReconWrkInfo, "FinancingAgency", (FINANCING_AGENCY_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "BankId", (BANK_ID_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "NcPaymentMethodCode", (NC_PAYMENT_METHOD_CODE_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "NameOnAccount", (NAME_ON_ACCOUNT_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "AccountTypeCode", (ACCOUNT_TYPE_CODE_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "TaxId", (TAX_ID_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "BranchDigit", (BRANCH_DIGIT_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "AccountDigit", (ACCOUNT_DIGIT_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "RefundAmount", (REFUND_AMOUNT_COL))


            If ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub


        Private Sub PopulateFormItem(oCellPosition As Integer, oPropertyValue As Object)
            PopulateControlFromBOProperty(GetSelectedGridControl(moDataGrid, oCellPosition), oPropertyValue)
        End Sub

#End Region

#Region "GridHandlers"

        Private Sub moDataGrid_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles moDataGrid.PageIndexChanging
            Try
                State.PageIndex = e.NewPageIndex
                If IsDataGPageDirty() Then
                    DisplayMessage(Message.MSG_PAGE_SAVE_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSavePagePromptResponse)
                Else
                    ' moDataGrid.PageIndex = NewCurrentPageIndex(moDataGrid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                    ' Me.State.PageIndex = moDataGrid.PageSize
                    'Me.moDataGrid.PageIndex = e.NewPageIndex
                    PopulateGrid()
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub moDataGrid_PageSizeChanged(source As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                If IsDataGPageDirty() Then
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.GridPageSize
                    DisplayMessage(Message.MSG_PAGE_SAVE_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSavePagePromptResponse)
                Else
                    'moDataGrid.PageIndex = NewCurrentPageIndex(moDataGrid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                    'Me.State.selectedPageSize = CType(cboPageSize.SelectedValue, Integer)
                    moDataGrid.PageIndex = NewCurrentPageIndex(moDataGrid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                    State.PageIndex = moDataGrid.PageSize
                    PopulateGrid()
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub




        Protected Sub ItemBound(source As Object, e As GridViewRowEventArgs) Handles moDataGrid.RowDataBound
            Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
            Dim oTextBox As TextBox

            'translate the reject message
            Dim strMsg As String, dr As DataRow, intParamCnt As Integer = 0, strParamList As String = String.Empty
            If dvRow IsNot Nothing Then
                dr = dvRow.Row
                strMsg = dr(DealerVscReconWrkDAL.COL_TRANSLATED_MSG).ToString.Trim
                If strMsg <> String.Empty Then
                    If dr(DealerVscReconWrkDAL.COL_MSG_PARAMETER_COUNT) IsNot DBNull.Value Then
                        intParamCnt = CType(dr(DealerVscReconWrkDAL.COL_MSG_PARAMETER_COUNT), Integer)
                    End If
                    If intParamCnt > 0 Then
                        If dr(DealerVscReconWrkDAL.COL_REJECT_MSG_PARMS) IsNot DBNull.Value Then
                            strParamList = dr(DealerVscReconWrkDAL.COL_REJECT_MSG_PARMS).ToString.Trim
                        End If
                        strMsg = TranslationBase.TranslateParameterizedMsg(strMsg, intParamCnt, strParamList).Trim
                    End If
                    If strMsg <> "" Then dr(DealerVscReconWrkDAL.COL_REJECT_REASON) = strMsg
                    dr.AcceptChanges()
                End If
            End If

            If (itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem) AndAlso e.Row.RowIndex <> -1 Then
                With e.Row

                    PopulateControlFromBOProperty(.FindControl("moDealerVscReconWrkIdLabel"), dvRow(DealerVscReconWrk.COL_NAME_DEALER_VSC_RECON_WRK_ID))

                    Dim oDrop As DropDownList = CType(e.Row.FindControl("moRecordTypeDrop"), DropDownList)
                    oDrop.Attributes.Add("onchange", "setDirty()")
                    PopulateRecordTypeDrop(oDrop)
                    Dim oValue As String = CType(dvRow(DealerVscReconWrk.COL_NAME_RECORD_TYPE), String)
                    SetSelectedItemByText(oDrop, oValue)

                    oTextBox = CType(.FindControl("moRejectCodeTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_REJECT_CODE))

                    oTextBox = CType(.FindControl("moRejectReasonTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_REJECT_REASON))

                    oTextBox = CType(.FindControl("moCompanyCodeTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_COMPANY_CODE))

                    oTextBox = CType(.FindControl("moDealerCodeTextGrid"), TextBox)
                    If Not State.DealerGrpId = Guid.Empty Then
                        oTextBox.Enabled = True
                    Else
                        oTextBox.Enabled = False
                    End If

                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_DEALER_CODE))

                    oTextBox = CType(.FindControl("moBranchCodeTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_BRANCH_CODE))

                    oTextBox = CType(.FindControl("moCertificateTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_CERTIFICATE_NUMBER))

                    oTextBox = CType(.FindControl("moCustNameTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_CUSTOMERS))

                    oTextBox = CType(.FindControl("moAddressTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_ADDRESS1))

                    oTextBox = CType(.FindControl("moCityTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_CITY))

                    oTextBox = CType(.FindControl("moPostalCodeTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_POSTAL_CODE))

                    oTextBox = CType(.FindControl("moRegionTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_REGION))

                    oTextBox = CType(.FindControl("moCountryCodeTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_COUNTRY_CODE))

                    oTextBox = CType(.FindControl("moHomePhoneTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_HOME_PHONE))

                    oTextBox = CType(.FindControl("moModelYearTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_MODEL_YEAR))

                    oTextBox = CType(.FindControl("moManufacturerTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_MANUFACTURER))

                    oTextBox = CType(.FindControl("moModelTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_MODEL))

                    oTextBox = CType(.FindControl("moVINTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_VIN))

                    oTextBox = CType(.FindControl("moEngineVersionTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_ENGINE_VERSION))

                    oTextBox = CType(.FindControl("moExternalCarCodeTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_EXTERNAL_CAR_CODE))

                    oTextBox = CType(.FindControl("movVehicleLicenseTagTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_VEHICLE_LICENSE_TAG))

                    oTextBox = CType(.FindControl("moOdometereTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_ODOMETER))

                    oTextBox = CType(.FindControl("moPurchasePriceTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_PURCHASE_PRICE))

                    oTextBox = CType(.FindControl("moPurchaseDateTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Dim oDatePurchaseImage As ImageButton = CType(.FindControl("moPurchaseDateImageGrid"), ImageButton)
                    If (oDatePurchaseImage IsNot Nothing) Then
                        AddCalendar(oDatePurchaseImage, oTextBox)
                    End If
                    PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_PURCHASE_DATE))

                    oTextBox = CType(.FindControl("moInServiceDateTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Dim oDateInServiceImage As ImageButton = CType(.FindControl("moInServiceDateImageGrid"), ImageButton)
                    If (oDateInServiceImage IsNot Nothing) Then
                        AddCalendar(oDateInServiceImage, oTextBox)
                    End If
                    PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_IN_SERVICE_DATE))

                    oTextBox = CType(.FindControl("moDeliveryDateTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Dim oDateDeliveryImage As ImageButton = CType(.FindControl("moDeliveryDateImageGrid"), ImageButton)
                    If (oDateDeliveryImage IsNot Nothing) Then
                        AddCalendar(oDateDeliveryImage, oTextBox)
                    End If
                    PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_DELIVERY_DATE))

                    oTextBox = CType(.FindControl("moPlanCodeTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_PLAN_CODE))

                    oTextBox = CType(.FindControl("moDeductibleTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_DEDUCTIBLE))

                    oTextBox = CType(.FindControl("moTermMonthsTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_TERM_MONTHS))

                    oTextBox = CType(.FindControl("moTermKmMiTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_TERM_KM_MI))

                    oTextBox = CType(.FindControl("moAgentNumberTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_AGENT_NUMBER))

                    oTextBox = CType(.FindControl("moWarrantySaleDateTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Dim oDateWarrantySaleImage As ImageButton = CType(.FindControl("moWarrantySaleDateImageGrid"), ImageButton)
                    If (oDateWarrantySaleImage IsNot Nothing) Then
                        AddCalendar(oDateWarrantySaleImage, oTextBox)
                    End If
                    PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_WARRANTY_SALE_DATE))

                    oTextBox = CType(.FindControl("moPlanAmountTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_PLAN_AMOUNT))

                    oTextBox = CType(.FindControl("moDocumentTypeTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_DOCUMENT_TYPE))

                    oTextBox = CType(.FindControl("moIdentityDocNoTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_IDENTITY_DOC_NO))

                    oTextBox = CType(.FindControl("moRgNoTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_RG_NO))

                    oTextBox = CType(.FindControl("moIdTypeTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_ID_TYPE))

                    oTextBox = CType(.FindControl("moDocumentIssueDateTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Dim oDateDocumentIssueImage As ImageButton = CType(.FindControl("moDocumentIssueDateImageGrid"), ImageButton)
                    If (oDateDocumentIssueImage IsNot Nothing) Then
                        AddCalendar(oDateDocumentIssueImage, oTextBox)
                    End If
                    PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_DOCUMENT_ISSUE_DATE))

                    oTextBox = CType(.FindControl("moDocumentAgencyTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_DOCUMENT_AGENCY))

                    oTextBox = CType(.FindControl("moNewUsedTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_NEW_USED))

                    oTextBox = CType(.FindControl("moOptionalCoverageTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_OPTIONAL_COVERAGE))

                    oTextBox = CType(.FindControl("moBirthDateTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Dim oDateBirthImage As ImageButton = CType(.FindControl("moBirthDateImageGrid"), ImageButton)
                    If (oDateBirthImage IsNot Nothing) Then
                        AddCalendar(oDateBirthImage, oTextBox)
                    End If
                    PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_BIRTH_DATE))

                    oTextBox = CType(.FindControl("moWorkPhoneTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_WORK_PHONE))

                    oTextBox = CType(.FindControl("moPaymentTypeTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_PAYMENT_TYPE_CODE))

                    oTextBox = CType(.FindControl("moPaymentInstrumentTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_PAYMENT_INSTRUMENT_CODE))

                    oTextBox = CType(.FindControl("moInstallmentNumberTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_INSTALLMENTS_NUMBER))

                    oTextBox = CType(.FindControl("moPlanAmtwithMarkupTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_PLAN_AMOUNT_WITH_MARKUP))


                    oTextBox = CType(.FindControl("moPaymentDateTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Dim oDatePaymentImage As ImageButton = CType(.FindControl("moPaymentDateImageGrid"), ImageButton)
                    If (oDatePurchaseImage IsNot Nothing) Then
                        AddCalendar(oDatePaymentImage, oTextBox)
                    End If
                    PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_PAYMENT_DATE))

                    oTextBox = CType(.FindControl("moCancellationDateTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Dim oDateCancellationImage As ImageButton = CType(.FindControl("moCancellationDateImageGrid"), ImageButton)
                    If (oDateCancellationImage IsNot Nothing) Then
                        AddCalendar(oDateCancellationImage, oTextBox)
                    End If
                    PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_CANCELLATION_DATE))

                    'oTextBox = CType(.FindControl("moPaymentDateTextGrid"), TextBox)
                    'oTextBox.Attributes.Add("onchange", "setDirty()")
                    'Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_PAYMENT_DATE))

                    'oTextBox = CType(.FindControl("moCancellationDateTextGrid"), TextBox)
                    'oTextBox.Attributes.Add("onchange", "setDirty()")
                    'Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_CANCELLATION_DATE))

                    oTextBox = CType(.FindControl("moCancelReasonCodeTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_CANCELLATION_REASON_CODE))

                    oTextBox = CType(.FindControl("moCancelCommentTypeCodeTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_CANCEL_COMMENT_TYPE_CODE))

                    oTextBox = CType(.FindControl("moCancellationCommentTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_CANCELLATION_COMMENT))

                    oTextBox = CType(.FindControl("moFinancingAgencyTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_FINANCING_AGENCY))

                    oTextBox = CType(.FindControl("moBankIdTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_BANK_ID))

                    oTextBox = CType(.FindControl("moNCPaymentMethodCodeTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_NC_PAYMENT_METHOD_CODE))

                    oTextBox = CType(.FindControl("moNameOnAccountTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_NAME_ON_ACCOUNT))

                    oTextBox = CType(.FindControl("moAccountTypeCodeTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_ACCOUNT_TYPE_CODE))

                    oTextBox = CType(.FindControl("moTaxIDTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_TAX_ID))

                    oTextBox = CType(.FindControl("moBranchDigitTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_BRANCH_DIGIT))

                    oTextBox = CType(.FindControl("moAccountDigitTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_ACCOUNT_DIGIT))

                    oTextBox = CType(.FindControl("moRefundAmountTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_REFUND_AMOUNT))


                End With
            End If
            BaseItemBound(source, e)

        End Sub

        Private Sub Grid_SortCommand(source As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles moDataGrid.Sorting
            Try

                Dim spaceIndex As Integer = SortDirection.LastIndexOf(" ")
                If spaceIndex > 0 AndAlso SortDirection.Substring(0, spaceIndex).Equals(e.SortExpression) Then
                    If SortDirection.EndsWith(" ASC") Then
                        SortDirection = e.SortExpression + " DESC"
                    Else
                        SortDirection = e.SortExpression + " ASC"
                    End If
                Else
                    SortDirection = e.SortExpression + " ASC"
                End If


                State.PageIndex = 0
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub


        Protected Overloads Sub ItemCreated(sender As Object, e As GridViewRowEventArgs)
            BaseItemCreated(sender, e)

        End Sub

        Protected Sub BindBoPropertiesToGridHeaders(DealerVscReconWrkInfo As DealerVscReconWrk)
            BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "RecordType", moDataGrid.Columns(RECORD_TYPE_COL))
            BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "RejectCode", moDataGrid.Columns(REJECT_CODE_COL))
            BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "RejectReason", moDataGrid.Columns(REJECT_REASON_COL))
            BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "CompanyCode", moDataGrid.Columns(COMPANY_CODE_COL))
            BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "DealerId", moDataGrid.Columns(DEALER_CODE_COL))
            BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "BranchCode", moDataGrid.Columns(BRANCH_CODE_COL))
            BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "CertificateNumber", moDataGrid.Columns(CERTIFICATE_NUMBER_COL))
            BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "Customers", moDataGrid.Columns(CUSTOMER_NAME_COL))
            BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "Address1", moDataGrid.Columns(ADDRESS1_COL))
            BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "City", moDataGrid.Columns(CITY_COL))
            BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "PostalCode", moDataGrid.Columns(POSTAL_CODE_COL))
            BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "Region", moDataGrid.Columns(REGION_COL))
            BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "CountryCode", moDataGrid.Columns(COUNTRY_CODE_COL))
            BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "HomePhone", moDataGrid.Columns(HOME_PHONE_COL))
            BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "ModelYear", moDataGrid.Columns(MODEL_YEAR_COL))
            BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "Manufacturer", moDataGrid.Columns(MANUFACTURER_COL))
            BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "Model", moDataGrid.Columns(MODEL_COL))
            BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "Vin", moDataGrid.Columns(VIN_COL))
            BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "EngineVersion", moDataGrid.Columns(ENGINE_VERSION_COL))
            BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "ExternalCarCode", moDataGrid.Columns(EXTERNAL_CAR_CODE_COL))
            BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "VehicleLicenseTag", moDataGrid.Columns(VEHICLE_LICENSE_TAG_COL))
            BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "Odometer", moDataGrid.Columns(ODOMETER_COL))
            BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "PurchasePrice", moDataGrid.Columns(PURCHASE_PRICE_COL))
            BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "PurchaseDate", moDataGrid.Columns(PURCHASE_DATE_COL))
            BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "InServiceDate", moDataGrid.Columns(IN_SERVICE_DATE_COL))
            BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "DeliveryDate", moDataGrid.Columns(DELIVERY_DATE_COL))
            BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "PlanCode", moDataGrid.Columns(PLAN_CODE_COL))
            BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "Deductible", moDataGrid.Columns(DEDUCTIBLE_COL))
            BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "TermMonths", moDataGrid.Columns(TERM_MONTHS_COL))
            BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "TermKmMi", moDataGrid.Columns(TERM_KM_MI_COL))
            BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "AgentNumber", moDataGrid.Columns(AGENT_NUMBER_COL))
            BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "WarrantySaleDate", moDataGrid.Columns(WARRANTY_SALE_DATE_COL))
            BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "PlanAmount", moDataGrid.Columns(PLAN_AMOUNT_COL))
            BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "DocumentType", moDataGrid.Columns(DOCUMENT_TYPE_COL))
            BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "IdentityDocNo", moDataGrid.Columns(IDENTITY_DOC_NO_COL))
            BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "RgNo", moDataGrid.Columns(RG_NO_COL))
            BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "IdType", moDataGrid.Columns(ID_TYPE_COL))
            BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "DocumentIssueDate", moDataGrid.Columns(DOCUMENT_ISSUE_DATE_COL))
            BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "DocumentAgency", moDataGrid.Columns(DOCUMENT_AGENCY_COL))
            BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "NewUsed", moDataGrid.Columns(NEW_USED_COL))
            BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "OptionalCoverage", moDataGrid.Columns(OPTIONAL_COVERAGE_COL))
            BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "BirthDate", moDataGrid.Columns(BIRTH_DATE_COL))
            BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "WorkPhone", moDataGrid.Columns(WORK_PHONE_COL))
            BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "PaymentTypeCode", moDataGrid.Columns(PAYMENT_TYPE_COL))
            BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "PaymentInstrumentCode", moDataGrid.Columns(PAYMENT_INSTRUMENT_COL))
            BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "InstallmentsNumber", moDataGrid.Columns(INSTALLMENT_NUMBER_COL))

            BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "PlanAmountWithMarkup", moDataGrid.Columns(PLAN_AMOUNT_WITH_MARKUP_COL))
            BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "PaymentDate", moDataGrid.Columns(PAYMENT_DATE_COL))
            BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "CancellationDate", moDataGrid.Columns(CANCELLATION_DATE_COL))
            BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "CancellationReasonCode", moDataGrid.Columns(CANCELLATION_REASON_CODE_COL))
            BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "CancelCommentTypeCode", moDataGrid.Columns(CANCEL_COMMENT_TYPE_CODE_COL))
            BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "CancellationComment", moDataGrid.Columns(CANCELLATION_COMMENT_COL))

            BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "FinancingAgency", moDataGrid.Columns(FINANCING_AGENCY_COL))
            BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "BankId", moDataGrid.Columns(BANK_ID_COL))
            BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "NcPaymentMethodCode", moDataGrid.Columns(NC_PAYMENT_METHOD_CODE_COL))
            BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "NameOnAccount", moDataGrid.Columns(NAME_ON_ACCOUNT_COL))
            BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "AccountTypeCode", moDataGrid.Columns(ACCOUNT_TYPE_CODE_COL))
            BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "TaxId", moDataGrid.Columns(TAX_ID_COL))
            BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "BranchDigit", moDataGrid.Columns(BRANCH_DIGIT_COL))
            BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "AccountDigit", moDataGrid.Columns(ACCOUNT_DIGIT_COL))
            BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "RefundAmount", moDataGrid.Columns(REFUND_AMOUNT_COL))


            ClearGridViewHeadersAndLabelsErrSign()
        End Sub

        Private Sub SetFocusOnEditableFieldInGrid(grid As DataGrid, cellPosition As Integer, controlName As String, itemIndex As Integer)
            'Set focus on the Description TextBox for the EditItemIndex row
            Dim desc As TextBox = CType(grid.Items(itemIndex).Cells(cellPosition).FindControl(controlName), TextBox)
            SetFocus(desc)
        End Sub

        Protected Sub BtnViewBundles_Click(sender As Object, e As System.EventArgs)
            Dim reconWorkIDForm As String = CType(moDataGrid.Rows(moDataGrid.SelectedIndex).FindControl("moDealerVscReconWrkIdLabel"), Label).Text
            Dim reconWorkIDFormGUID As Guid = GetGuidFromString(reconWorkIDForm)
            State.DealerVscReconWrkId = reconWorkIDFormGUID

            'update the contents in the detail panel
            updPnlBundles.Update()
            'show the modal popup
            mdlPopup.Show()
        End Sub

#End Region

#Region "Button Click Events"

        Private Sub btnBack_Click(sender As System.Object, e As System.EventArgs) Handles btnBack.Click
            Try
                If IsDataGPageDirty() OrElse (State.BundlesHashTable IsNot Nothing AndAlso State.BundlesHashTable.Count > 0) Then
                    DisplayMessage(Message.MSG_PAGE_SAVE_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSavePagePromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    Dim retType As New DealerFileProcessedController_New.ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.DealerfileProcessedId)
                    ReturnToCallingPage(retType)
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub SaveButton_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles SaveButton_WRITE.Click
            Try
                SavePage()
                DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)
                HiddenIsPageDirty.Value = EMPTY
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnUndo_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnUndo_WRITE.Click
            Try
                State.BundlesHashTable = Nothing
                PopulateGrid()
                HiddenIsPageDirty.Value = EMPTY
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub btnApply_Click(sender As System.Object, e As System.EventArgs) Handles btnApply.Click
            Dim hashTable As New Hashtable
            ''            ApplyBundles()
        End Sub

        Protected Sub btnClose_Click(sender As System.Object, e As System.EventArgs)
            ''If IsDataGBundlesPageDirty() Then
            ''    ApplyBundles()
            ''End If
            ' Me.ErrController2.Clear_Hide()
            mdlPopup.Hide()
            ClearGridViewHeadersAndLabelsErrSign()
            'Me.ClearGridHeaders(gvPop)
        End Sub

#End Region
    End Class

End Namespace
