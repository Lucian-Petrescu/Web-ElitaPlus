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

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
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
                IsEditing = (Me.moDataGrid.EditIndex > Me.NO_ITEM_SELECTED_INDEX)
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
            Set(ByVal value As String)
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
            Me.State.DealerfileProcessedId = CType(Me.CallingParameters, Guid)
        End Sub

#End Region

#Region "Page Events"

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Me.MasterPage.MessageController.Clear_Hide()
            'Me.ErrController2.Clear_Hide()
            Me.SetStateProperties()

            If Not Page.IsPostBack Then
                Me.MasterPage.MessageController.Clear()
                Me.MasterPage.UsePageTabTitleInBreadCrum = False
                Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Interfaces")
                UpdateBreadCrum()
                Me.SortDirection = EMPTY
                Me.SetGridItemStyleColor(moDataGrid)
                Me.ShowMissingTranslations(MasterPage.MessageController)
                Me.State.PageIndex = 0
                Me.TranslateGridHeader(moDataGrid)
                Me.TranslateGridHeader(gvPop)
                Me.TranslateGridControls(moDataGrid)
                PopulateReadOnly()
                cboPageSize.SelectedValue = CType(Me.State.selectedPageSize, String)
                PopulateGrid()

            Else
                CheckIfComingFromSaveConfirm()
                'CheckIfComingFromBundlesScreen()
            End If
        End Sub

#End Region

#Region "Controlling Logic"

        Protected Sub AssignDropDownToCtr(ByVal control As System.Web.UI.WebControls.WebControl, ByVal textbox As System.Web.UI.WebControls.WebControl, Optional ByVal caller As String = "")
            Dim AppPath As String = Request.ApplicationPath
            Dim ServerName As String = Request.ServerVariables("SERVER_NAME")
            Dim url As String = ELPWebConstants.APPLICATION_PATH & "/Common/CalendarForm.aspx"
            control.Attributes.Add("onchange", "javascript:UpdateCtr(this,'" & textbox.UniqueID.Replace(":", "_") & "')")
            textbox.Attributes.Add("onkeyup", "javascript:UpdateDropDownCtr(this,'" & control.UniqueID.Replace(":", "_") & "')")
        End Sub

        Protected Sub CheckIfComingFromSaveConfirm()
            Dim confResponse As String = Me.HiddenSavePagePromptResponse.Value
            Try
                If Not confResponse.Equals(EMPTY) Then
                    If confResponse = Me.MSG_VALUE_YES Then
                        SavePage()
                        Me.HiddenIsBundlesPageDirty.Value = EMPTY
                        'Me.HiddenIfComingFromBundlesScreen.Value = EMPTY
                    ElseIf confResponse = Me.MSG_VALUE_NO Then
                        Me.State.BundlesHashTable = Nothing
                    End If
                    Me.HiddenSavePagePromptResponse.Value = EMPTY
                    Me.HiddenIsPageDirty.Value = EMPTY

                    Select Case Me.State.ActionInProgress
                        Case ElitaPlusPage.DetailPageCommand.Back
                            Dim retType As New DealerFileProcessedController_New.ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.DealerfileProcessedId)
                            Me.ReturnToCallingPage(retType)
                        Case ElitaPlusPage.DetailPageCommand.GridPageSize
                            Me.moDataGrid.PageIndex = NewCurrentPageIndex(moDataGrid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                            Me.State.selectedPageSize = CType(cboPageSize.SelectedValue, Integer)
                        Case ElitaPlusPage.DetailPageCommand.GridColSort
                            'Me.State.sortBy = CType(e.CommandArgument, String)
                        Case Else
                            Me.moDataGrid.PageIndex = Me.State.selectedPageIndex
                    End Select
                    PopulateGrid()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Function CreateBoFromGrid(ByVal index As Integer) As DealerVscReconWrk
            Dim DealerVscReconWrkId As Guid
            Dim DealerVscReconWrkInfo As DealerVscReconWrk
            Dim sModifiedDate As String

            moDataGrid.SelectedIndex = index
            DealerVscReconWrkId = New Guid(CType(moDataGrid.Rows(index).FindControl("moDealerVscReconWrkIdLabel"), Label).Text)
            sModifiedDate = Me.GetGridText(moDataGrid, index, Me.MODIFIED_DATE_COL)
            DealerVscReconWrkInfo = New DealerVscReconWrk(DealerVscReconWrkId, sModifiedDate)
            Return DealerVscReconWrkInfo
        End Function

        Private Sub SavePage()
            Dim totc As Integer = Me.moDataGrid.Columns.Count()
            Dim index As Integer = 0
            Dim DealerVscReconWrkInfo As DealerVscReconWrk
            Dim totItems As Integer = Me.moDataGrid.Rows.Count

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
            Dim Result As String = Me.HiddenIsPageDirty.Value
            Return Result.Equals("YES")
        End Function

        Private Sub SetColumnState(ByVal column As Byte, ByVal state As Boolean)
            Me.moDataGrid.Columns(column).Visible = state
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

            Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("DEALER_FILE")
            Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("DEALER_FILE")

        End Sub

#End Region

#Region "Populate"

        Sub PopulateRecordTypeDrop(ByVal recordTypeDrop As DropDownList)
            Try
                Dim oLangId As Guid = Authentication.LangId
                ' Dim recordTypeList As DataView = LookupListNew.GetRecordTypeLookupList(oLangId)
                'Me.BindListControlToDataView(recordTypeDrop, recordTypeList, , , False)
                recordTypeDrop.Populate(CommonConfigManager.Current.ListManager.GetList("RECTYP", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions())
            Catch ex As Exception
                ThePage.HandleErrors(ex, Me.MasterPage.MessageController)
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
                Dim oDealerFile As DealerFileProcessed = New DealerFileProcessed(Me.State.DealerfileProcessedId)
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
                        Me.State.DealerGrpId = .DealerGroupId
                        If Not moDealerGrpNameLabel.Text.EndsWith(":") Then
                            moDealerGrpNameLabel.Text = moDealerGrpNameLabel.Text & ":"
                        End If
                    End If

                End With
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub PopulateGrid()

            Dim dv As DataView
            Dim recCount As Integer = 0

            Try
                dv = GetDV()
                'dv.Sort = Me.State.sortBy
                If Not Me.SortDirection.Equals(EMPTY) Then
                    dv.Sort = Me.SortDirection
                    HighLightSortColumn(moDataGrid, Me.SortDirection)
                End If
                recCount = dv.Count
                Session("recCount") = recCount
                ' Me.moDataGrid.PageSize = Me.State.selectedPageSize

                SetPageAndSelectedIndexFromGuid(dv, Me.State.DealerfileProcessedId, Me.moDataGrid, Me.State.PageIndex)

                Me.moDataGrid.DataSource = dv
                Me.moDataGrid.DataBind()
                Me.lblRecordCount.Text = recCount & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)

                If Not moDataGrid.BottomPagerRow.Visible Then moDataGrid.BottomPagerRow.Visible = True
                ControlMgr.DisableAllGridControlsIfNotEditAuth(Me, moDataGrid)

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
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


        Private Sub PopulateBOItem(ByVal DealerVscReconWrkInfo As DealerVscReconWrk, ByVal oPropertyName As String, ByVal oCellPosition As Integer)
            Me.PopulateBOProperty(DealerVscReconWrkInfo, oPropertyName,
                                            CType(Me.GetSelectedGridControl(moDataGrid, oCellPosition), TextBox))
        End Sub

        Private Sub PopulateBODrop(ByVal DealerVscReconWrkInfo As DealerVscReconWrk, ByVal oPropertyName As String, ByVal oCellPosition As Integer)
            Me.PopulateBOProperty(DealerVscReconWrkInfo, oPropertyName,
                                CType(Me.GetSelectedGridControl(moDataGrid, oCellPosition), DropDownList), False)
        End Sub

        Private Sub PopulateBOFromForm(ByVal DealerVscReconWrkInfo As DealerVscReconWrk)
            Dim dealerid As Guid

            Me.PopulateBODrop(DealerVscReconWrkInfo, "RecordType", (Me.RECORD_TYPE_COL))
            'PopulateBOItem(DealerVscReconWrkInfo, "RecordType", (Me.RECORD_TYPE_COL))
            'PopulateBOItem(DealerVscReconWrkInfo, "RejectCode", (Me.REJECT_CODE_COL))
            'PopulateBOItem(DealerVscReconWrkInfo, "RejectReason", (Me.REJECT_REASON_COL))
            'PopulateBOItem(DealerVscReconWrkInfo, "CompanyCode", (Me.COMPANY_CODE_COL))
            If Not Me.State.DealerGrpId = Guid.Empty Then
                dealerid = Dealer.GetDealerIDbyCodeAndDealerGroup(Me.State.DealerGrpId, CType(Me.GetSelectedGridControl(moDataGrid, (Me.DEALER_CODE_COL)), TextBox).Text.Trim.ToString)
                PopulateBOProperty(DealerVscReconWrkInfo, "DealerId", dealerid)
                'Else
                '   PopulateBOItem(DealerVscReconWrkInfo, "DealerId", Me.DEALER_CODE_COL)
            End If
            PopulateBOItem(DealerVscReconWrkInfo, "BranchCode", (Me.BRANCH_CODE_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "CertificateNumber", (Me.CERTIFICATE_NUMBER_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "Customers", (Me.CUSTOMER_NAME_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "Address1", (Me.ADDRESS1_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "City", (Me.CITY_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "PostalCode", (Me.POSTAL_CODE_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "Region", (Me.REGION_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "CountryCode", (Me.COUNTRY_CODE_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "HomePhone", (Me.HOME_PHONE_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "ModelYear", (Me.MODEL_YEAR_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "Manufacturer", (Me.MANUFACTURER_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "Model", (Me.MODEL_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "Vin", (Me.VIN_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "EngineVersion", (Me.ENGINE_VERSION_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "ExternalCarCode", (Me.EXTERNAL_CAR_CODE_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "VehicleLicenseTag", (Me.VEHICLE_LICENSE_TAG_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "Odometer", (Me.ODOMETER_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "PurchasePrice", (Me.PURCHASE_PRICE_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "PurchaseDate", (Me.PURCHASE_DATE_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "InServiceDate", (Me.IN_SERVICE_DATE_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "DeliveryDate", (Me.DELIVERY_DATE_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "PlanCode", (Me.PLAN_CODE_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "Deductible", (Me.DEDUCTIBLE_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "TermMonths", (Me.TERM_MONTHS_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "TermKmMi", (Me.TERM_KM_MI_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "AgentNumber", (Me.AGENT_NUMBER_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "WarrantySaleDate", (Me.WARRANTY_SALE_DATE_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "PlanAmount", (Me.PLAN_AMOUNT_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "DocumentType", (Me.DOCUMENT_TYPE_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "IdentityDocNo", (Me.IDENTITY_DOC_NO_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "RgNo", (Me.RG_NO_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "IdType", (Me.ID_TYPE_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "DocumentIssueDate", (Me.DOCUMENT_ISSUE_DATE_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "DocumentAgency", (Me.DOCUMENT_AGENCY_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "NewUsed", (Me.NEW_USED_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "OptionalCoverage", (Me.OPTIONAL_COVERAGE_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "BirthDate", (Me.BIRTH_DATE_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "WorkPhone", (Me.WORK_PHONE_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "PaymentTypeCode", (Me.PAYMENT_TYPE_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "PaymentInstrumentCode", (Me.PAYMENT_INSTRUMENT_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "InstallmentsNumber", (Me.INSTALLMENT_NUMBER_COL))

            PopulateBOItem(DealerVscReconWrkInfo, "PlanAmountWithMarkup", (Me.PLAN_AMOUNT_WITH_MARKUP_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "PaymentDate", (Me.PAYMENT_DATE_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "CancellationDate", (Me.CANCELLATION_DATE_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "CancellationReasonCode", (Me.CANCELLATION_REASON_CODE_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "CancelCommentTypeCode", (Me.CANCEL_COMMENT_TYPE_CODE_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "CancellationComment", (Me.CANCELLATION_COMMENT_COL))

            PopulateBOItem(DealerVscReconWrkInfo, "FinancingAgency", (Me.FINANCING_AGENCY_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "BankId", (Me.BANK_ID_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "NcPaymentMethodCode", (Me.NC_PAYMENT_METHOD_CODE_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "NameOnAccount", (Me.NAME_ON_ACCOUNT_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "AccountTypeCode", (Me.ACCOUNT_TYPE_CODE_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "TaxId", (Me.TAX_ID_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "BranchDigit", (Me.BRANCH_DIGIT_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "AccountDigit", (Me.ACCOUNT_DIGIT_COL))
            PopulateBOItem(DealerVscReconWrkInfo, "RefundAmount", (Me.REFUND_AMOUNT_COL))


            If Me.ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub


        Private Sub PopulateFormItem(ByVal oCellPosition As Integer, ByVal oPropertyValue As Object)
            Me.PopulateControlFromBOProperty(Me.GetSelectedGridControl(moDataGrid, oCellPosition), oPropertyValue)
        End Sub

#End Region

#Region "GridHandlers"

        Private Sub moDataGrid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles moDataGrid.PageIndexChanging
            Try
                Me.State.PageIndex = e.NewPageIndex
                If IsDataGPageDirty() Then
                    DisplayMessage(Message.MSG_PAGE_SAVE_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, Me.HiddenSavePagePromptResponse)
                Else
                    ' moDataGrid.PageIndex = NewCurrentPageIndex(moDataGrid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                    ' Me.State.PageIndex = moDataGrid.PageSize
                    'Me.moDataGrid.PageIndex = e.NewPageIndex
                    PopulateGrid()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub moDataGrid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                If IsDataGPageDirty() Then
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.GridPageSize
                    DisplayMessage(Message.MSG_PAGE_SAVE_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, Me.HiddenSavePagePromptResponse)
                Else
                    'moDataGrid.PageIndex = NewCurrentPageIndex(moDataGrid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                    'Me.State.selectedPageSize = CType(cboPageSize.SelectedValue, Integer)
                    moDataGrid.PageIndex = NewCurrentPageIndex(moDataGrid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                    Me.State.PageIndex = moDataGrid.PageSize
                    Me.PopulateGrid()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub




        Protected Sub ItemBound(ByVal source As Object, ByVal e As GridViewRowEventArgs) Handles moDataGrid.RowDataBound
            Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
            Dim oTextBox As TextBox

            'translate the reject message
            Dim strMsg As String, dr As DataRow, intParamCnt As Integer = 0, strParamList As String = String.Empty
            If Not dvRow Is Nothing Then
                dr = dvRow.Row
                strMsg = dr(DealerVscReconWrkDAL.COL_TRANSLATED_MSG).ToString.Trim
                If strMsg <> String.Empty Then
                    If Not dr(DealerVscReconWrkDAL.COL_MSG_PARAMETER_COUNT) Is DBNull.Value Then
                        intParamCnt = CType(dr(DealerVscReconWrkDAL.COL_MSG_PARAMETER_COUNT), Integer)
                    End If
                    If intParamCnt > 0 Then
                        If Not dr(DealerVscReconWrkDAL.COL_REJECT_MSG_PARMS) Is DBNull.Value Then
                            strParamList = dr(DealerVscReconWrkDAL.COL_REJECT_MSG_PARMS).ToString.Trim
                        End If
                        strMsg = TranslationBase.TranslateParameterizedMsg(strMsg, intParamCnt, strParamList).Trim
                    End If
                    If strMsg <> "" Then dr(DealerVscReconWrkDAL.COL_REJECT_REASON) = strMsg
                    dr.AcceptChanges()
                End If
            End If

            If (itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem) AndAlso e.Row.RowIndex <> -1 Then
                With e.Row

                    Me.PopulateControlFromBOProperty(.FindControl("moDealerVscReconWrkIdLabel"), dvRow(DealerVscReconWrk.COL_NAME_DEALER_VSC_RECON_WRK_ID))

                    Dim oDrop As DropDownList = CType(e.Row.FindControl("moRecordTypeDrop"), DropDownList)
                    oDrop.Attributes.Add("onchange", "setDirty()")
                    PopulateRecordTypeDrop(oDrop)
                    Dim oValue As String = CType(dvRow(DealerVscReconWrk.COL_NAME_RECORD_TYPE), String)
                    Me.SetSelectedItemByText(oDrop, oValue)

                    oTextBox = CType(.FindControl("moRejectCodeTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_REJECT_CODE))

                    oTextBox = CType(.FindControl("moRejectReasonTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_REJECT_REASON))

                    oTextBox = CType(.FindControl("moCompanyCodeTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_COMPANY_CODE))

                    oTextBox = CType(.FindControl("moDealerCodeTextGrid"), TextBox)
                    If Not Me.State.DealerGrpId = Guid.Empty Then
                        oTextBox.Enabled = True
                    Else
                        oTextBox.Enabled = False
                    End If

                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_DEALER_CODE))

                    oTextBox = CType(.FindControl("moBranchCodeTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_BRANCH_CODE))

                    oTextBox = CType(.FindControl("moCertificateTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_CERTIFICATE_NUMBER))

                    oTextBox = CType(.FindControl("moCustNameTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_CUSTOMERS))

                    oTextBox = CType(.FindControl("moAddressTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_ADDRESS1))

                    oTextBox = CType(.FindControl("moCityTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_CITY))

                    oTextBox = CType(.FindControl("moPostalCodeTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_POSTAL_CODE))

                    oTextBox = CType(.FindControl("moRegionTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_REGION))

                    oTextBox = CType(.FindControl("moCountryCodeTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_COUNTRY_CODE))

                    oTextBox = CType(.FindControl("moHomePhoneTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_HOME_PHONE))

                    oTextBox = CType(.FindControl("moModelYearTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_MODEL_YEAR))

                    oTextBox = CType(.FindControl("moManufacturerTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_MANUFACTURER))

                    oTextBox = CType(.FindControl("moModelTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_MODEL))

                    oTextBox = CType(.FindControl("moVINTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_VIN))

                    oTextBox = CType(.FindControl("moEngineVersionTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_ENGINE_VERSION))

                    oTextBox = CType(.FindControl("moExternalCarCodeTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_EXTERNAL_CAR_CODE))

                    oTextBox = CType(.FindControl("movVehicleLicenseTagTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_VEHICLE_LICENSE_TAG))

                    oTextBox = CType(.FindControl("moOdometereTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_ODOMETER))

                    oTextBox = CType(.FindControl("moPurchasePriceTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_PURCHASE_PRICE))

                    oTextBox = CType(.FindControl("moPurchaseDateTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Dim oDatePurchaseImage As ImageButton = CType(.FindControl("moPurchaseDateImageGrid"), ImageButton)
                    If (Not oDatePurchaseImage Is Nothing) Then
                        Me.AddCalendar(oDatePurchaseImage, oTextBox)
                    End If
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_PURCHASE_DATE))

                    oTextBox = CType(.FindControl("moInServiceDateTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Dim oDateInServiceImage As ImageButton = CType(.FindControl("moInServiceDateImageGrid"), ImageButton)
                    If (Not oDateInServiceImage Is Nothing) Then
                        Me.AddCalendar(oDateInServiceImage, oTextBox)
                    End If
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_IN_SERVICE_DATE))

                    oTextBox = CType(.FindControl("moDeliveryDateTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Dim oDateDeliveryImage As ImageButton = CType(.FindControl("moDeliveryDateImageGrid"), ImageButton)
                    If (Not oDateDeliveryImage Is Nothing) Then
                        Me.AddCalendar(oDateDeliveryImage, oTextBox)
                    End If
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_DELIVERY_DATE))

                    oTextBox = CType(.FindControl("moPlanCodeTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_PLAN_CODE))

                    oTextBox = CType(.FindControl("moDeductibleTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_DEDUCTIBLE))

                    oTextBox = CType(.FindControl("moTermMonthsTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_TERM_MONTHS))

                    oTextBox = CType(.FindControl("moTermKmMiTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_TERM_KM_MI))

                    oTextBox = CType(.FindControl("moAgentNumberTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_AGENT_NUMBER))

                    oTextBox = CType(.FindControl("moWarrantySaleDateTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Dim oDateWarrantySaleImage As ImageButton = CType(.FindControl("moWarrantySaleDateImageGrid"), ImageButton)
                    If (Not oDateWarrantySaleImage Is Nothing) Then
                        Me.AddCalendar(oDateWarrantySaleImage, oTextBox)
                    End If
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_WARRANTY_SALE_DATE))

                    oTextBox = CType(.FindControl("moPlanAmountTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_PLAN_AMOUNT))

                    oTextBox = CType(.FindControl("moDocumentTypeTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_DOCUMENT_TYPE))

                    oTextBox = CType(.FindControl("moIdentityDocNoTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_IDENTITY_DOC_NO))

                    oTextBox = CType(.FindControl("moRgNoTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_RG_NO))

                    oTextBox = CType(.FindControl("moIdTypeTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_ID_TYPE))

                    oTextBox = CType(.FindControl("moDocumentIssueDateTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Dim oDateDocumentIssueImage As ImageButton = CType(.FindControl("moDocumentIssueDateImageGrid"), ImageButton)
                    If (Not oDateDocumentIssueImage Is Nothing) Then
                        Me.AddCalendar(oDateDocumentIssueImage, oTextBox)
                    End If
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_DOCUMENT_ISSUE_DATE))

                    oTextBox = CType(.FindControl("moDocumentAgencyTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_DOCUMENT_AGENCY))

                    oTextBox = CType(.FindControl("moNewUsedTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_NEW_USED))

                    oTextBox = CType(.FindControl("moOptionalCoverageTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_OPTIONAL_COVERAGE))

                    oTextBox = CType(.FindControl("moBirthDateTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Dim oDateBirthImage As ImageButton = CType(.FindControl("moBirthDateImageGrid"), ImageButton)
                    If (Not oDateBirthImage Is Nothing) Then
                        Me.AddCalendar(oDateBirthImage, oTextBox)
                    End If
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_BIRTH_DATE))

                    oTextBox = CType(.FindControl("moWorkPhoneTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_WORK_PHONE))

                    oTextBox = CType(.FindControl("moPaymentTypeTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_PAYMENT_TYPE_CODE))

                    oTextBox = CType(.FindControl("moPaymentInstrumentTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_PAYMENT_INSTRUMENT_CODE))

                    oTextBox = CType(.FindControl("moInstallmentNumberTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_INSTALLMENTS_NUMBER))

                    oTextBox = CType(.FindControl("moPlanAmtwithMarkupTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_PLAN_AMOUNT_WITH_MARKUP))


                    oTextBox = CType(.FindControl("moPaymentDateTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Dim oDatePaymentImage As ImageButton = CType(.FindControl("moPaymentDateImageGrid"), ImageButton)
                    If (Not oDatePurchaseImage Is Nothing) Then
                        Me.AddCalendar(oDatePaymentImage, oTextBox)
                    End If
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_PAYMENT_DATE))

                    oTextBox = CType(.FindControl("moCancellationDateTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Dim oDateCancellationImage As ImageButton = CType(.FindControl("moCancellationDateImageGrid"), ImageButton)
                    If (Not oDateCancellationImage Is Nothing) Then
                        Me.AddCalendar(oDateCancellationImage, oTextBox)
                    End If
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_CANCELLATION_DATE))

                    'oTextBox = CType(.FindControl("moPaymentDateTextGrid"), TextBox)
                    'oTextBox.Attributes.Add("onchange", "setDirty()")
                    'Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_PAYMENT_DATE))

                    'oTextBox = CType(.FindControl("moCancellationDateTextGrid"), TextBox)
                    'oTextBox.Attributes.Add("onchange", "setDirty()")
                    'Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_CANCELLATION_DATE))

                    oTextBox = CType(.FindControl("moCancelReasonCodeTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_CANCELLATION_REASON_CODE))

                    oTextBox = CType(.FindControl("moCancelCommentTypeCodeTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_CANCEL_COMMENT_TYPE_CODE))

                    oTextBox = CType(.FindControl("moCancellationCommentTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_CANCELLATION_COMMENT))

                    oTextBox = CType(.FindControl("moFinancingAgencyTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_FINANCING_AGENCY))

                    oTextBox = CType(.FindControl("moBankIdTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_BANK_ID))

                    oTextBox = CType(.FindControl("moNCPaymentMethodCodeTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_NC_PAYMENT_METHOD_CODE))

                    oTextBox = CType(.FindControl("moNameOnAccountTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_NAME_ON_ACCOUNT))

                    oTextBox = CType(.FindControl("moAccountTypeCodeTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_ACCOUNT_TYPE_CODE))

                    oTextBox = CType(.FindControl("moTaxIDTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_TAX_ID))

                    oTextBox = CType(.FindControl("moBranchDigitTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_BRANCH_DIGIT))

                    oTextBox = CType(.FindControl("moAccountDigitTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_ACCOUNT_DIGIT))

                    oTextBox = CType(.FindControl("moRefundAmountTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerVscReconWrk.COL_NAME_REFUND_AMOUNT))


                End With
            End If
            BaseItemBound(source, e)

        End Sub

        Private Sub Grid_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles moDataGrid.Sorting
            Try

                Dim spaceIndex As Integer = Me.SortDirection.LastIndexOf(" ")
                If spaceIndex > 0 AndAlso Me.SortDirection.Substring(0, spaceIndex).Equals(e.SortExpression) Then
                    If Me.SortDirection.EndsWith(" ASC") Then
                        Me.SortDirection = e.SortExpression + " DESC"
                    Else
                        Me.SortDirection = e.SortExpression + " ASC"
                    End If
                Else
                    Me.SortDirection = e.SortExpression + " ASC"
                End If


                Me.State.PageIndex = 0
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub


        Protected Overloads Sub ItemCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
            BaseItemCreated(sender, e)

        End Sub

        Protected Sub BindBoPropertiesToGridHeaders(ByVal DealerVscReconWrkInfo As DealerVscReconWrk)
            Me.BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "RecordType", Me.moDataGrid.Columns(Me.RECORD_TYPE_COL))
            Me.BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "RejectCode", Me.moDataGrid.Columns(Me.REJECT_CODE_COL))
            Me.BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "RejectReason", Me.moDataGrid.Columns(Me.REJECT_REASON_COL))
            Me.BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "CompanyCode", Me.moDataGrid.Columns(Me.COMPANY_CODE_COL))
            Me.BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "DealerId", Me.moDataGrid.Columns(Me.DEALER_CODE_COL))
            Me.BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "BranchCode", Me.moDataGrid.Columns(Me.BRANCH_CODE_COL))
            Me.BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "CertificateNumber", Me.moDataGrid.Columns(Me.CERTIFICATE_NUMBER_COL))
            Me.BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "Customers", Me.moDataGrid.Columns(Me.CUSTOMER_NAME_COL))
            Me.BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "Address1", Me.moDataGrid.Columns(Me.ADDRESS1_COL))
            Me.BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "City", Me.moDataGrid.Columns(Me.CITY_COL))
            Me.BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "PostalCode", Me.moDataGrid.Columns(Me.POSTAL_CODE_COL))
            Me.BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "Region", Me.moDataGrid.Columns(Me.REGION_COL))
            Me.BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "CountryCode", Me.moDataGrid.Columns(Me.COUNTRY_CODE_COL))
            Me.BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "HomePhone", Me.moDataGrid.Columns(Me.HOME_PHONE_COL))
            Me.BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "ModelYear", Me.moDataGrid.Columns(Me.MODEL_YEAR_COL))
            Me.BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "Manufacturer", Me.moDataGrid.Columns(Me.MANUFACTURER_COL))
            Me.BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "Model", Me.moDataGrid.Columns(Me.MODEL_COL))
            Me.BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "Vin", Me.moDataGrid.Columns(Me.VIN_COL))
            Me.BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "EngineVersion", Me.moDataGrid.Columns(Me.ENGINE_VERSION_COL))
            Me.BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "ExternalCarCode", Me.moDataGrid.Columns(Me.EXTERNAL_CAR_CODE_COL))
            Me.BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "VehicleLicenseTag", Me.moDataGrid.Columns(Me.VEHICLE_LICENSE_TAG_COL))
            Me.BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "Odometer", Me.moDataGrid.Columns(Me.ODOMETER_COL))
            Me.BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "PurchasePrice", Me.moDataGrid.Columns(Me.PURCHASE_PRICE_COL))
            Me.BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "PurchaseDate", Me.moDataGrid.Columns(Me.PURCHASE_DATE_COL))
            Me.BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "InServiceDate", Me.moDataGrid.Columns(Me.IN_SERVICE_DATE_COL))
            Me.BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "DeliveryDate", Me.moDataGrid.Columns(Me.DELIVERY_DATE_COL))
            Me.BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "PlanCode", Me.moDataGrid.Columns(Me.PLAN_CODE_COL))
            Me.BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "Deductible", Me.moDataGrid.Columns(Me.DEDUCTIBLE_COL))
            Me.BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "TermMonths", Me.moDataGrid.Columns(Me.TERM_MONTHS_COL))
            Me.BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "TermKmMi", Me.moDataGrid.Columns(Me.TERM_KM_MI_COL))
            Me.BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "AgentNumber", Me.moDataGrid.Columns(Me.AGENT_NUMBER_COL))
            Me.BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "WarrantySaleDate", Me.moDataGrid.Columns(Me.WARRANTY_SALE_DATE_COL))
            Me.BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "PlanAmount", Me.moDataGrid.Columns(Me.PLAN_AMOUNT_COL))
            Me.BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "DocumentType", Me.moDataGrid.Columns(Me.DOCUMENT_TYPE_COL))
            Me.BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "IdentityDocNo", Me.moDataGrid.Columns(Me.IDENTITY_DOC_NO_COL))
            Me.BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "RgNo", Me.moDataGrid.Columns(Me.RG_NO_COL))
            Me.BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "IdType", Me.moDataGrid.Columns(Me.ID_TYPE_COL))
            Me.BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "DocumentIssueDate", Me.moDataGrid.Columns(Me.DOCUMENT_ISSUE_DATE_COL))
            Me.BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "DocumentAgency", Me.moDataGrid.Columns(Me.DOCUMENT_AGENCY_COL))
            Me.BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "NewUsed", Me.moDataGrid.Columns(Me.NEW_USED_COL))
            Me.BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "OptionalCoverage", Me.moDataGrid.Columns(Me.OPTIONAL_COVERAGE_COL))
            Me.BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "BirthDate", Me.moDataGrid.Columns(Me.BIRTH_DATE_COL))
            Me.BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "WorkPhone", Me.moDataGrid.Columns(Me.WORK_PHONE_COL))
            Me.BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "PaymentTypeCode", Me.moDataGrid.Columns(Me.PAYMENT_TYPE_COL))
            Me.BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "PaymentInstrumentCode", Me.moDataGrid.Columns(Me.PAYMENT_INSTRUMENT_COL))
            Me.BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "InstallmentsNumber", Me.moDataGrid.Columns(Me.INSTALLMENT_NUMBER_COL))

            Me.BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "PlanAmountWithMarkup", Me.moDataGrid.Columns(Me.PLAN_AMOUNT_WITH_MARKUP_COL))
            Me.BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "PaymentDate", Me.moDataGrid.Columns(Me.PAYMENT_DATE_COL))
            Me.BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "CancellationDate", Me.moDataGrid.Columns(Me.CANCELLATION_DATE_COL))
            Me.BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "CancellationReasonCode", Me.moDataGrid.Columns(Me.CANCELLATION_REASON_CODE_COL))
            Me.BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "CancelCommentTypeCode", Me.moDataGrid.Columns(Me.CANCEL_COMMENT_TYPE_CODE_COL))
            Me.BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "CancellationComment", Me.moDataGrid.Columns(Me.CANCELLATION_COMMENT_COL))

            Me.BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "FinancingAgency", Me.moDataGrid.Columns(Me.FINANCING_AGENCY_COL))
            Me.BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "BankId", Me.moDataGrid.Columns(Me.BANK_ID_COL))
            Me.BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "NcPaymentMethodCode", Me.moDataGrid.Columns(Me.NC_PAYMENT_METHOD_CODE_COL))
            Me.BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "NameOnAccount", Me.moDataGrid.Columns(Me.NAME_ON_ACCOUNT_COL))
            Me.BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "AccountTypeCode", Me.moDataGrid.Columns(Me.ACCOUNT_TYPE_CODE_COL))
            Me.BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "TaxId", Me.moDataGrid.Columns(Me.TAX_ID_COL))
            Me.BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "BranchDigit", Me.moDataGrid.Columns(Me.BRANCH_DIGIT_COL))
            Me.BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "AccountDigit", Me.moDataGrid.Columns(Me.ACCOUNT_DIGIT_COL))
            Me.BindBOPropertyToGridHeader(DealerVscReconWrkInfo, "RefundAmount", Me.moDataGrid.Columns(Me.REFUND_AMOUNT_COL))


            Me.ClearGridViewHeadersAndLabelsErrSign()
        End Sub

        Private Sub SetFocusOnEditableFieldInGrid(ByVal grid As DataGrid, ByVal cellPosition As Integer, ByVal controlName As String, ByVal itemIndex As Integer)
            'Set focus on the Description TextBox for the EditItemIndex row
            Dim desc As TextBox = CType(grid.Items(itemIndex).Cells(cellPosition).FindControl(controlName), TextBox)
            SetFocus(desc)
        End Sub

        Protected Sub BtnViewBundles_Click(ByVal sender As Object, ByVal e As System.EventArgs)
            Dim reconWorkIDForm As String = CType(Me.moDataGrid.Rows(Me.moDataGrid.SelectedIndex).FindControl("moDealerVscReconWrkIdLabel"), Label).Text
            Dim reconWorkIDFormGUID As Guid = GetGuidFromString(reconWorkIDForm)
            Me.State.DealerVscReconWrkId = reconWorkIDFormGUID

            'update the contents in the detail panel
            Me.updPnlBundles.Update()
            'show the modal popup
            Me.mdlPopup.Show()
        End Sub

#End Region

#Region "Button Click Events"

        Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
            Try
                If IsDataGPageDirty() Or (Not Me.State.BundlesHashTable Is Nothing AndAlso Me.State.BundlesHashTable.Count > 0) Then
                    DisplayMessage(Message.MSG_PAGE_SAVE_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, Me.HiddenSavePagePromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    Dim retType As New DealerFileProcessedController_New.ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.DealerfileProcessedId)
                    Me.ReturnToCallingPage(retType)
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub SaveButton_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveButton_WRITE.Click
            Try
                SavePage()
                Me.DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                Me.HiddenIsPageDirty.Value = EMPTY
                PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnUndo_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUndo_WRITE.Click
            Try
                Me.State.BundlesHashTable = Nothing
                PopulateGrid()
                Me.HiddenIsPageDirty.Value = EMPTY
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub btnApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApply.Click
            Dim hashTable As New Hashtable
            ''            ApplyBundles()
        End Sub

        Protected Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
            ''If IsDataGBundlesPageDirty() Then
            ''    ApplyBundles()
            ''End If
            ' Me.ErrController2.Clear_Hide()
            Me.mdlPopup.Hide()
            Me.ClearGridViewHeadersAndLabelsErrSign()
            'Me.ClearGridHeaders(gvPop)
        End Sub

#End Region
    End Class

End Namespace
