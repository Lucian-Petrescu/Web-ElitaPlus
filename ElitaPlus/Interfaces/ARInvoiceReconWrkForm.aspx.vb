Imports Assurant.ElitaPlus.DALObjects
Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Namespace Interfaces

    Partial Class ARInvoiceReconWrkForm
        Inherits ElitaPlusSearchPage

#Region "Constants"
        Public Const URL As String = "ARInvoiceReconWrkForm.aspx"
        Private Const CANCELLATIONS_TYPE As String = "N"

        Private Const MSG_CONFIRM_PROMPT As String = "MSG_CONFIRM_PROMPT"
        Private Const MSG_RECORD_DELETED_OK As String = "MSG_RECORD_DELETED_OK"
        Private Const MSG_RECORD_SAVED_OK As String = "MSG_RECORD_SAVED_OK"
        Private Const MSG_RECORD_NOT_SAVED As String = "MSG_RECORD_NOT_SAVED"

        Private Const ID_COL As Integer = 1
        Private Const RECORD_TYPE_COL As Integer = 2
        Private Const REJECT_CODE_COL As Integer = 3
        Private Const REJECT_REASON_COL As Integer = 4
        Private Const INVOICE_NUMBER_COL As Integer = 5
        'Private Const CANCELLATION_CODE_COL As Integer = 6
        Private Const CERTIFICATE_COL As Integer = 6
        Private Const INVOICE_PERIOD_START_DATE_COL As Integer = 7
        Private Const INVOICE_PERIOD_END_DATE_COL As Integer = 8
        Private Const INVOICE_DATE_COL As Integer = 9
        Private Const INVOICE_DUE_DATE_COL As Integer = 10
        Private Const CURRENCY_CODE_COL As Integer = 11
        Private Const EXCHANGE_RATE_COL As Integer = 12
        Private Const LINE_TYPE_COL As Integer = 13
        Private Const ITEM_CODE_COL As Integer = 14
        Private Const EARNING_PARTER_COL As Integer = 15
        Private Const SOURCE_COL As Integer = 16
        Private Const INSTALLMENT_NUMBER_COL As Integer = 17
        Private Const AMOUNT_COL As Integer = 18
        Private Const PARENT_LINE_NUMBER_COL As Integer = 19
        Private Const DESCRIPTION_COL As Integer = 20
        Private Const ENTIRE_RECORD_COL As Integer = 21
        Private Const REFERENCE_COL As Integer = 22
        Private Const INVOICE_LOADED_COL As Integer = 10

        'Private Const IMEI_NUMBER_COL As Integer = 24
        'Private Const ITEM_CODE_COL As Integer = 25
        'Private Const ITEM_COL As Integer = 26

        Private Const CUSTOMER_NAME_COL As Integer = 24
        'Private Const IDENTIFICATION_NUMBER_COL As Integer = 30

        'Private Const ADDRESS1_COL As Integer = 33
        'Private Const ADDRESS2_COL As Integer = 34
        'Private Const ADDRESS3_COL As Integer = 35
        'Private Const CITY_COL As Integer = 36
        'Private Const ZIP_COL As Integer = 37
        'Private Const STATE_PROVINCE_COL As Integer = 38
        'Private Const CUST_COUNTRY_COL As Integer = 39
        'Private Const HOME_PHONE_COL As Integer = 40
        'Private Const WORK_PHONE_COL As Integer = 41
        'Private Const COUNTRY_PURCH_COL As Integer = 42
        'Private Const ISO_CODE_COL As Integer = 43
        'Private Const NUMBER_COMP_COL As Integer = 44
        'Private Const TYPE_PAYMENT_COL As Integer = 45
        'Private Const DOCUMENT_TYPE_COL As Integer = 46
        'Private Const DOCUMENT_AGENCY_COL As Integer = 47
        'Private Const DOCUMENT_ISSUE_DATE_COL As Integer = 48
        'Private Const RG_NUMBER_COL As Integer = 49
        'Private Const ID_TYPE_COL As Integer = 50
        'Private Const BILLING_FREQUENCY_COL As Integer = 51
        'Private Const NUMBER_OF_INSTALLMENTS_COL As Integer = 52
        'Private Const INSTALLMENT_AMOUNT_COL As Integer = 53
        'Private Const BANK_RTN_NUMBER_COL As Integer = 54
        'Private Const BANK_ACCOUNT_NUMBER_COL As Integer = 55
        'Private Const BANK_ACCT_OWNER_NAME_COL As Integer = 56
        'Private Const BANK_BRANCH_NUMBER_COL As Integer = 57
        'Private Const POST_PRE_PAID_COL As Integer = 58
        'Private Const BILLING_PLAN_COL As Integer = 59
        'Private Const BILLING_CYCLE_COL As Integer = 60
        'Private Const DATE_PAID_FOR_COL As Integer = 61
        'Private Const MODIFIED_DATE_COL As Integer = 62
        'Private Const MEMBERSHIP_NUMBER_COL As Integer = 63
        'Private Const SUBSCRIBER_STATUS_COL As Integer = 64
        'Private Const SUSPENDED_REASON_COL As Integer = 65
        'Private Const MOBILE_TYPE_COL As Integer = 66
        'Private Const FIRST_USE_DATE_COL As Integer = 67
        'Private Const LAST_USE_DATE_COL As Integer = 68
        'Private Const SIM_CARD_NUMBER_COL As Integer = 69
        'Private Const REGION_COL As Integer = 70
        'Private Const MEMBERSHIP_TYPE_COL As Integer = 71
        'Private Const CESS_OFFICE_COL As Integer = 72
        'Private Const CESS_SALESREP_COL As Integer = 73
        'Private Const BUSINESSLINE_COL As Integer = 74
        'Private Const SALES_DEPARTMENT_COL As Integer = 75
        'Private Const LINKED_CERT_NUMBER_COL As Integer = 76
        'Private Const ADDITIONAL_INFO_COL As Integer = 77
        'Private Const CREDITCARD_LAST_FOUR_DIGIT_COL As Integer = 78 'REQ-1169
        'Private Const FINANCED_AMOUNT_COL As Integer = 79
        'Private Const FINANCED_FREQUENCY_COL As Integer = 80
        'Private Const FINANCED_INSTALLMENT_NUMBER_COL As Integer = 81
        'Private Const FINANCED_INSTALLMENT_AMOUNT_COL As Integer = 82
        'Private Const FINANCE_DATE_COL As Integer = 83
        'Private Const DOWN_PAYMENT_COL As Integer = 84
        'Private Const ADVANCE_PAYMENT_COL As Integer = 85
        'Private Const UPGRADE_TERM_COL As Integer = 86
        'Private Const BILLING_ACCOUNT_NUMBER_COL As Integer = 87
        ''REQ-5619 - Start
        'Private Const GENDER_COL As Integer = 88
        'Private Const MARITAL_STATUS_COL As Integer = 89
        'Private Const NATIONALITY_COL As Integer = 90
        'Private Const BIRTH_DATE_COL As Integer = 91
        ''REQ-5619 - End

        ''REQ-5681 - Start
        'Private Const PLACE_OF_BIRTH_COL As Integer = 92
        'Private Const CUIT_CUIL_COL As Integer = 93
        'Private Const PERSON_TYPE_COL As Integer = 94
        ''REQ-5681 - End

        'Private Const SERVICE_LINE_NUMBER_COL As Integer = 95

        'Private Const NUM_OF_CONSECUTIVE_PAYMENTS_COL As Integer = 96
        'Private Const LOAN_CODE_COL As Integer = 97
        'Private Const PAYMENT_SHIFT_NUMBER_COL As Integer = 98
        'Private Const DEALER_CURRENT_PLAN_CODE_COL As Integer = 99
        'Private Const DEALER_SCHEDULED_PLAN_CODE_COL As Integer = 100
        'Private Const DEALER_UPDATE_REASON_COL As Integer = 101
        'Private Const UPGRADE_DATE_COL As Integer = 102
        'Private Const VOUCHER_NUMBER_COL As Integer = 103
        'Private Const RMA_COL As Integer = 104
        'Private Const OUTSTANDING_BALANCE_AMT_COL As Integer = 105
        'Private Const OUTSTANDING_BALANCE_DUE_DATE_COL As Integer = 106
        'Private Const REFUND_AMOUNT_COL As Integer = 107
        'Private Const DEVICE_TYPE_COL As Integer = 108
        'Private Const APPLECARE_FEE As Integer = 109

        'Private Const ITEM_NUMBER_COL As Integer = 0
        'Private Const ITEM_MANUFACTURER_COL As Integer = 1
        'Private Const ITEM_MODEL_COL As Integer = 2
        'Private Const ITEM_SERIAL_NUMBER_COL As Integer = 3
        'Private Const ITEM_DESCRIPTION_COL As Integer = 4
        'Private Const ITEM_PRICE_COL As Integer = 5
        'Private Const ITEM_BUNDLE_VALUE_COL As Integer = 6
        'Private Const ITEM_MAN_WARRANTY_COL As Integer = 7

        Private Const RECORD_TYPE_CONTROL_NAME As String = "moRecordTypeTextGrid"

        Private Const EMPTY As String = ""
        Private Const DEFAULT_PAGE_INDEX As Integer = 0

        ' Property Name
        Public Const TABLE_NAME As String = "ELP_AR_INVOICE_INTERFACE"
        Public Const TABLE_KEY_NAME As String = COL_NAME_INVOICE_INTERFACE_ID

        Public Const COL_NAME_INVOICE_INTERFACE_ID As String = "invoice_interface_id"
        Public Const COL_NAME_DEALERFILE_PROCESSED_ID As String = "dealerfile_processed_id"
        Public Const COL_NAME_RECORD_TYPE As String = "record_type"
        Public Const COL_NAME_CERTIFICATE As String = "certificate"
        Public Const COL_NAME_INVOICE_NUMBER As String = "invoice_number"
        Public Const COL_NAME_INVOICE_PERIOD_START_DATE As String = "invoice_period_start_date"
        Public Const COL_NAME_INVOICE_PERIOD_END_DATE As String = "invoice_period_end_date"
        Public Const COL_NAME_INVOICE_DATE As String = "invoice_date"
        Public Const COL_NAME_INVOCE_DUE_DATE As String = "invoce_due_date"
        Public Const COL_NAME_BILL_TO_ADDRESS_ID As String = "bill_to_address_id"
        Public Const COL_NAME_SHIP_TO_ADDRESS_ID As String = "ship_to_address_id"
        Public Const COL_NAME_CURRENCY_CODE As String = "currency_code"
        Public Const COL_NAME_EXCHANGE_RATE As String = "exchange_rate"
        Public Const COL_NAME_LINE_TYPE As String = "line_type"
        Public Const COL_NAME_ITEM_CODE As String = "item_code"
        Public Const COL_NAME_EARNING_PARTER As String = "earning_parter"
        Public Const COL_NAME_SOURCE As String = "source"
        Public Const COL_NAME_REFERENCE As String = "reference"
        Public Const COL_NAME_REFERENCE_ID As String = "reference_id"
        Public Const COL_NAME_INSTALLMENT_NUMBER As String = "installment_number"
        Public Const COL_NAME_AMOUNT As String = "amount"
        Public Const COL_NAME_PARENT_LINE_NUMBER As String = "parent_line_number"
        Public Const COL_NAME_DESCRIPTION As String = "description"
        Public Const COL_NAME_INVOICE_HEADER_ID As String = "invoice_header_id"
        Public Const COL_NAME_INVOICE_LINE_ID As String = "invoice_line_id"
        Public Const COL_NAME_ENTIRE_RECORD As String = "entire_record"
        Public Const COL_NAME_INVOICE_LOADED As String = "invoice_loaded"
        Public Const COL_NAME_REJECT_CODE As String = "reject_code"
        Public Const COL_NAME_REJECT_REASON As String = "reject_reason"
        Public Const COL_NAME_REJECT_MSG_PARAMS As String = "reject_msg_params"
        Public Const COL_NAME_DEALER_ID As String = "dealer_id"
        Public Const COL_UI_PROD_CODE As String = "UI_PROG_CODE"

        Public Const COL_MSG_PARAMETER_COUNT As String = "MSG_PARAMETER_COUNT"
        Public Const COL_REJECT_REASON As String = "reject_reason"
        Public Const COL_TRANSLATED_MSG As String = "Translated_MSG"
#End Region
#Region "propertyNames"
        ' Property Name
        Private Const RECORD_TYPE_PROPERTY As String = "RecordType"
        'Req 846
        Private Const INVOICE_NUMBER_PROPERTY As String = "InvoiceNumber"
        Private Const REJECT_CODE_PROPERTY As String = "RejectCode"
        Private Const REJECT_REASON_PROPERTY As String = "RejectReason"
        Private Const CURRENCY_CODE_PROPERTY As String = "CurrencyCode"
        Private Const EXCHANGE_RATE_PROPERTY As String = "ExchangeRate"
        Private Const LINE_TYPE_PROPERTY As String = "LineType"
        Private Const ITEM_CODE_PROPERTY As String = "ItemCode"
        Private Const INVOICE_PERIOD_START_DATE_PROPERTY As String = "InvoicePeriodStartDate"
        Private Const INVOICE_PERIOD_END_DATE_PROPERTY As String = "InvoicePeriodEndDate"
        Private Const INVOICE_DATE_PROPERTY As String = "InvoiceDate"
        Private Const INVOICEE_DUE_DATE_PROPERTY As String = "InvoiceDueDate"
        Private Const EARNING_PARTNER_PROPERTY As String = "EarningPartner"
        Private Const SOURCE_PROPERTY As String = "Source"
        Private Const REFERENCES_PROPERTY As String = "Reference"
        Private Const INSTALLMENT_NUMBER_PROPERTY As String = "InstallmentNumber"
        Private Const CERTIFICATE_PROPERTY As String = "Certificate"
        Private Const AMOUNT_PROPERTY As String = "Amount"
        Private Const PARENT_LINE_NUMBER_PROPERTY As String = "ParentLineNumber"
        Private Const CUSTOMER_NAME_PROPERTY As String = "CustomerName"
        Private Const DESCRIPTION_PROPERTY As String = "Description"
        Private Const ENTIRE_RECORD_PROPERTY As String = "EntireRecord"
        Private Const INVOICE_LOADED_PROPERTY As String = "InvoiceLoaded"


#End Region

#Region "Member Variables"

        Protected TempDataView As DataView = New DataView
        Private Shared pageIndex As Integer
        Private Shared pageCount As Integer
        Public selectedPageSize As Integer = 30
        Protected WithEvents btnSave As System.Web.UI.WebControls.Button
        Protected WithEvents btnUndo As System.Web.UI.WebControls.Button
        Protected WithEvents LbPage As System.Web.UI.WebControls.Label
        Protected WithEvents Button1 As System.Web.UI.WebControls.Button
        Protected WithEvents Button2 As System.Web.UI.WebControls.Button
        Public selectedPageIndex As Integer = DEFAULT_PAGE_INDEX
        Protected WithEvents tsHoriz As Microsoft.Web.UI.WebControls.TabStrip
        Protected WithEvents ddlCancellationReason As System.Web.UI.WebControls.DropDownList
        Protected WithEvents mpHoriz As Microsoft.Web.UI.WebControls.MultiPage

        Public Const RECORD_MODE__REJECTED As String = "REJ"
        Public Const RECORD_MODE__REMAINING_REJECTED As String = "REMREJ"
        Public Const RECORD_MODE__BYPASSED As String = "BYP"
        Public Const RECORD_MODE__VALIDATED As String = "VAL"
        Public Const RECORD_MODE__LOADED As String = "LOD"
        Public searchtotal As Double



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
        Protected WithEvents ErrController As ErrorController
        Protected WithEvents ErrController2 As ErrorController


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
            Public SortExpression1 As String = Assurant.ElitaPlus.BusinessObjectsNew.User.UserSearchDV.COL_NETWORK_ID
            Public SortExpression As String
            Public SortDirection As String
            Public PageIndex As Integer = 0
            Public DealerfileProcessedId As Guid
            'Public BundlesHashTable As Hashtable
            Public ARInvoiceReconWrkId As Guid
            Public ActionInProgress As DetailPageCommand = ElitaPlusPage.DetailPageCommand.Nothing_
            Public selectedPageSize As Integer = 30
            Public selectedPageIndex As Integer = DEFAULT_PAGE_INDEX
            Public sortBy As String
            'Public comingFromBundlesScreen As String = ""
            Public RecordMode As String
            Public srchRecordType As String
            Public srchRejectCode As String
            Public srchRejectReason As String
            Public ParentFile As String = "N"
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

        Private Sub SetQueryStringParam()
            Try
                If Request.QueryString("RECORDMODE") IsNot Nothing Then
                    State.RecordMode = Request.QueryString("RECORDMODE")
                End If
                If Request.QueryString("PARENTFILE") IsNot Nothing Then
                    State.ParentFile = Request.QueryString("PARENTFILE")
                End If
            Catch ex As Exception

            End Try
        End Sub

#End Region



#Region "Page Events"

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            MasterPage.MessageController.Clear_Hide()
            'Me.ErrController2.Clear_Hide()
            SetStateProperties()

            'moDataGrid.VirtualItemCount = CType(Session("TotalRecords"), Double)
            PopulateCancellationReasonDropDown()
            If Not Page.IsPostBack Then
                MasterPage.MessageController.Clear()
                MasterPage.UsePageTabTitleInBreadCrum = False
                MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Interfaces")
                UpdateBreadCrum()
                SetQueryStringParam()
                PopulateRecordTypeDrop(moRecordTypeSearchDrop, True)
                SortDirection = EMPTY
                SetGridItemStyleColor(moDataGrid)
                ShowMissingTranslations(MasterPage.MessageController)
                State.PageIndex = 0
                TranslateGridHeader(moDataGrid)
                TranslateGridHeader(gvPop)
                TranslateGridControls(moDataGrid)
                PopulateReadOnly()
                PopulateGrid()
                'CheckFileTypeColums()
                'Set page size
                cboPageSize.SelectedValue = State.selectedPageSize.ToString()
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
                        'Select Case SaveBundles()
                        '    Case 1, 2
                        '        Exit Select
                        'End Select
                        HiddenIsBundlesPageDirty.Value = EMPTY
                        'Me.HiddenIfComingFromBundlesScreen.Value = EMPTY
                    ElseIf confResponse = MSG_VALUE_NO Then
                        'Me.State.BundlesHashTable = Nothing
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
        'Protected Sub CheckIfComingFromBundlesScreen()
        '    Dim confResponse As String = Me.HiddenIfComingFromBundlesScreen.Value
        '    If Not confResponse.Equals(EMPTY) Then
        '        If confResponse = Me.MSG_VALUE_YES Then
        '            ApplyBundles()
        '        End If
        '        Me.HiddenIsBundlesPageDirty.Value = EMPTY
        '        Me.HiddenIfComingFromBundlesScreen.Value = EMPTY
        '    End If
        'End Sub

        Private Function CreateBoFromGrid(index As Integer) As ARInvoiceReconWrk
            Dim ARInvoiceReconWrkId As Guid
            Dim ARInvoiceReconWrkInfo As ARInvoiceReconWrk
            Dim sModifiedDate As String

            moDataGrid.SelectedIndex = index
            ARInvoiceReconWrkId = New Guid(CType(moDataGrid.Rows(index).FindControl("moDealerReconWrkIdLabel"), Label).Text)
            ' sModifiedDate = Me.GetGridText(moDataGrid, index, Me.MODIFIED_DATE_COL)
            ARInvoiceReconWrkInfo = New ARInvoiceReconWrk(ARInvoiceReconWrkId, sModifiedDate)
            Return ARInvoiceReconWrkInfo
        End Function

        'Private Function CreateBoFromBundlesGrid(ByVal index As Integer) As DealerReconWrkBundles
        '    Dim DealerReconWrkBundlesId As Guid
        '    Dim dealerReconWrkBundlesInfo As DealerReconWrkBundles

        '    gvPop.SelectedIndex = index
        '    DealerReconWrkBundlesId = New Guid(CType(moDataGrid.Rows(index).FindControl("moDealerReconWrkIdLabel"), Label).Text)
        '    dealerReconWrkBundlesInfo = New DealerReconWrkBundles(DealerReconWrkBundlesId)
        '    Return dealerReconWrkBundlesInfo
        'End Function


        Private Sub SavePage()
            'Dim totc As Integer = Me.moDataGrid.Columns.Count()
            Dim index As Integer = 0
            Dim ARInvoiceReconWrkInfo As ARInvoiceReconWrk
            Dim totItems As Integer = moDataGrid.Rows.Count

            If totItems > 0 Then
                ARInvoiceReconWrkInfo = CreateBoFromGrid(0)
                BindBoPropertiesToGridHeaders(ARInvoiceReconWrkInfo)
                PopulateBOFromForm(ARInvoiceReconWrkInfo)
                ARInvoiceReconWrkInfo.Save()
            End If

            totItems = totItems - 1
            For index = 1 To totItems
                ARInvoiceReconWrkInfo = CreateBoFromGrid(index)
                BindBoPropertiesToGridHeaders(ARInvoiceReconWrkInfo)
                PopulateBOFromForm(ARInvoiceReconWrkInfo)
                ARInvoiceReconWrkInfo.Save()
            Next
            'ARInvoiceReconWrk.UpdateHeaderCount(ARInvoiceReconWrkInfo.DealerfileProcessedId)
        End Sub

        'Private Function SaveBundles() As Integer
        '    Dim dv As DataView = GetGridDataView()
        '    Dim ds As DataSet

        '    For i As Integer = 0 To moDataGrid.Rows.Count - 1
        '        Dim moDealerReconWrkIdLabel As String = CType(moDataGrid.Rows(i).FindControl("moDealerReconWrkIdLabel"), Label).Text.Trim
        '        Dim moDealerReconWrkIdLabelID As Guid = GetGuidFromString(moDealerReconWrkIdLabel)
        '        If Not Me.State.BundlesHashTable Is Nothing Then
        '            If Me.State.BundlesHashTable.ContainsKey(moDealerReconWrkIdLabelID) Then
        '                ds = CType(Me.State.BundlesHashTable.Item(moDealerReconWrkIdLabelID), DataSet)
        '                If ds.HasChanges Then
        '                    For Each row As DataRow In ds.Tables(DealerReconWrkBundlesDAL.TABLE_NAME).Rows
        '                        If row.RowState = DataRowState.Unchanged Then
        '                            row.Delete()
        '                        End If
        '                    Next
        '                    ds.AcceptChanges()
        '                    Dim dealerReconWrkBundlesInfo As New DealerReconWrkBundles()
        '                    Dim ret As Integer = dealerReconWrkBundlesInfo.SaveBundles(ds)
        '                    If ret = 0 Then
        '                        'ds = GetBundlesDataSet()
        '                        Me.State.BundlesHashTable.Item(moDealerReconWrkIdLabelID) = ds
        '                        Return 2
        '                    Else
        '                        Me.ErrController.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.DB_WRITE_ERROR)
        '                    End If
        '                End If
        '            End If
        '        End If
        '    Next
        '    Return 1

        'End Function
        'Private Sub ApplyBundles()
        '    Try
        '        Dim dealerReconWrkBundlesInfo As DealerReconWrkBundles

        '        'Dim ds As DataSet = GetBundlesDataSet()

        '        For i As Integer = 0 To ds.Tables(DealerReconWrkBundlesDAL.TABLE_NAME).Rows.Count - 1
        '            With ds.Tables(DealerReconWrkBundlesDAL.TABLE_NAME).Rows
        '                gvPop.SelectedIndex = i
        '                Dim oDealerReconWrkBundles As New DealerReconWrkBundles(.Item(i), ds)
        '                BindBoPropertiesToBundlesGridHeaders(oDealerReconWrkBundles)
        '                PopulateBOFromBundlesForm(oDealerReconWrkBundles)
        '                oDealerReconWrkBundles.Validate()

        '            End With
        '        Next

        '        If Me.State.BundlesHashTable Is Nothing Then
        '            Me.State.BundlesHashTable = New Hashtable
        '        End If
        '        If Me.State.BundlesHashTable.Contains(Me.State.DealerReconWrkId) Then
        '            Me.State.BundlesHashTable.Item(Me.State.DealerReconWrkId) = ds
        '        Else
        '            'Me.State.BundlesHashTable.Add(Me.State.DealerReconWrkId, ds)
        '        End If

        '        Me.updPnlBundles.Update()
        '        'hide the modal popup
        '        Me.mdlPopup.Hide()

        '    Catch ex As Exception
        '        Me.HandleErrors(ex, Me.MasterPage.MessageController)
        '        ' Me.HandleErrors(ex, Me.ErrController2)
        '        Me.mdlPopup.Show()

        '    End Try
        'End Sub

        Function IsDataGPageDirty() As Boolean
            Dim Result As String = HiddenIsPageDirty.Value
            Return Result.Equals("YES")
        End Function

        Function IsDataGBundlesPageDirty() As Boolean
            Dim Result As String = HiddenIsBundlesPageDirty.Value
            Return Result.Equals("YES")
        End Function

        Private Sub SetColumnState(column As Byte, state As Boolean)
            moDataGrid.Columns(column).Visible = state
        End Sub

        Function IsCancellationsFileType(fileName As String) As Boolean
            Return fileName.Substring(4, 1).Equals(CANCELLATIONS_TYPE)
        End Function

        'Private Sub CheckFileTypeColums()
        '    If IsCancellationsFileType(moFileNameText.Text) Then
        '        'SetColumnState(RECORD_TYPE_COL, False)
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
        '        SetColumnState(IMEI_NUMBER_COL, False)
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
        '        SetColumnState(BANK_BRANCH_NUMBER_COL, False)

        '        SetColumnState(SALES_TAX_COL, False)
        '        'SetColumnState(EMAIL_COL, False)
        '        SetColumnState(ADDRESS2_COL, False)
        '        SetColumnState(CUST_COUNTRY_COL, False)
        '        SetColumnState(COUNTRY_PURCH_COL, False)

        '        SetColumnState(ADDRESS3_COL, False)
        '        SetColumnState(ORIGINAL_RETAIL_PRICE_COL, False)
        '        SetColumnState(BILLING_PLAN_COL, False)
        '        SetColumnState(BILLING_CYCLE_COL, False)
        '        SetColumnState(SUBSCRIBER_STATUS_COL, False)
        '        SetColumnState(SUSPENDED_REASON_COL, False)

        '        SetColumnState(MOBILE_TYPE_COL, False)
        '        SetColumnState(FIRST_USE_DATE_COL, False)
        '        SetColumnState(LAST_USE_DATE_COL, False)
        '        SetColumnState(SIM_CARD_NUMBER_COL, False)
        '        SetColumnState(REGION_COL, False)
        '        SetColumnState(MEMBERSHIP_TYPE_COL, False)
        '        SetColumnState(CESS_OFFICE_COL, False)
        '        SetColumnState(CESS_SALESREP_COL, False)
        '        SetColumnState(BUSINESSLINE_COL, False)
        '        SetColumnState(SALES_DEPARTMENT_COL, False)
        '        SetColumnState(LINKED_CERT_NUMBER_COL, False)
        '        SetColumnState(ADDITIONAL_INFO_COL, False)
        '        SetColumnState(CREDITCARD_LAST_FOUR_DIGIT_COL, False) 'REQ-1169
        '        SetColumnState(LOAN_CODE_COL, False)
        '        SetColumnState(PAYMENT_SHIFT_NUMBER_COL, False)
        '    Else
        '        SetColumnState(CANCELLATION_CODE_COL, False)
        '    End If
        'End Sub

        Private Sub UpdateBreadCrum()

            MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("DEALER_INVOICE_FILE")
            MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("DEALER_INVOICE_FILE")

        End Sub

        Private Sub EnableDisableControl(oWebControl As WebControl, grid As GridView)
            Try
                Dim oDealerFile As DealerFileProcessed = New DealerFileProcessed(State.DealerfileProcessedId)

                Select Case State.RecordMode
                    Case RECORD_MODE__REJECTED
                        If oDealerFile.IsChildFile Then
                            ControlMgr.SetEnableControl(Me, oWebControl, False)
                            EnableAllGridControls(grid, False)
                        Else
                            ControlMgr.SetEnableControl(Me, oWebControl, True)
                        End If
                    Case RECORD_MODE__REMAINING_REJECTED
                        If oDealerFile.IsChildFile Then
                            ControlMgr.SetEnableControl(Me, oWebControl, False)
                            EnableAllGridControls(grid, False)
                        Else
                            ControlMgr.SetEnableControl(Me, oWebControl, True)
                        End If
                    Case RECORD_MODE__BYPASSED
                        If oDealerFile.IsChildFile Then
                            ControlMgr.SetEnableControl(Me, oWebControl, False)
                            EnableAllGridControls(grid, False)
                        Else
                            ControlMgr.SetEnableControl(Me, oWebControl, True)
                        End If
                    Case RECORD_MODE__VALIDATED
                        ControlMgr.SetEnableControl(Me, oWebControl, False)
                        EnableAllGridControls(grid, False)
                    Case RECORD_MODE__LOADED
                        ControlMgr.SetEnableControl(Me, oWebControl, False)
                        EnableAllGridControls(grid, False)
                    Case Else
                        If oDealerFile.IsChildFile Then
                            ControlMgr.SetEnableControl(Me, oWebControl, False)
                            EnableAllGridControls(grid, False)
                        Else
                            ControlMgr.SetEnableControl(Me, oWebControl, True)
                        End If
                End Select

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub EnableAGridControl(oControl As Control, enable As Boolean)
            Dim oWebControl As WebControl

            If TypeOf oControl Is Button Then Return
            If TypeOf oControl Is WebControl Then
                oWebControl = CType(oControl, WebControl)
                oWebControl.Enabled = enable
            End If
        End Sub

        Private Sub EnableAllGridControls(grid As GridView, enable As Boolean)
            Dim row, column As Integer
            Dim oControl As Control

            For row = 0 To (grid.Rows.Count - 1)
                For column = 0 To (grid.Rows(row).Cells.Count - 1)
                    oControl = ElitaPlusSearchPage.GetGridControl(grid, row, column)
                    EnableAGridControl(oControl, enable)
                    If grid.Rows(row).Cells(column).Controls.Count = ElitaPlusSearchPage.CELL_NEXT_TEMPLATE_CONTROL_SIZE Then
                        ' Image Button
                        oControl = ElitaPlusSearchPage.GetGridControl(grid, row, column, True)
                        EnableAGridControl(oControl, enable)
                    End If
                Next
            Next
        End Sub
#End Region

#Region "Populate"
        Private Sub SaveGuiState()
            With State
                .srchRejectCode = moRejectCodeText.Text.Trim
                .srchRejectReason = moRejectReasonText.Text.Trim
                .srchRecordType = moRecordTypeSearchDrop.SelectedItem.Text.Trim
            End With
        End Sub

        Sub PopulateRecordTypeDrop(recordTypeDrop As DropDownList, Optional ByVal AddNothingSelected As Boolean = False)
            Try
                'Dim oLangId As Guid = Authentication.LangId
                'Dim recordTypeList As DataView = LookupListNew.GetInvoiceRecordTypeLookupList(oLangId)
                'Me.BindListControlToDataView(recordTypeDrop, recordTypeList, , , AddNothingSelected) 'INVOICE_REC_TYP
                recordTypeDrop.Populate(CommonConfigManager.Current.ListManager.GetList("INVOICE_REC_TYP", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                    {
                        .AddBlankItem = AddNothingSelected
                    })
            Catch ex As Exception
                ThePage.HandleErrors(ex, ErrController)
            End Try
        End Sub

        Sub PopulateCancellationReasonDropDown()
            Try
                Dim oCompanyId As Guid = DealerReconWrk.CompanyId(State.DealerfileProcessedId)
                TempDataView = LookupListNew.GetCancellationReasonDealerFileLookupList(oCompanyId)
                TempDataView.Sort = "DESCRIPTION"
                TempDataView.AddNew()
            Catch ex As Exception
                ThePage.HandleErrors(ex, ErrController)
            End Try
        End Sub

        Private Sub PopulateReadOnly()
            Try
                Dim oDealerFile As DealerFileProcessed = New DealerFileProcessed(State.DealerfileProcessedId)
                With oDealerFile
                    If State.ParentFile = "N" Then
                        moDealerNameText.Text = .DealerNameLoad
                    Else
                        moDealerNameText.Text = EMPTY
                    End If
                    moFileNameText.Text = .Filename
                End With
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub PopulateGrid()
            Dim dv As DataView
            Dim recCount As Integer = 0
            Dim objARInvoicereconwrk As New ARInvoiceReconWrkDAL

            Try
                dv = GetDV()
                'dv.Sort = Me.State.sortBy
                If Not SortDirection.Equals(EMPTY) Then
                    'dv.Sort = Me.SortDirection
                    HighLightSortColumn(moDataGrid, SortDirection)
                End If
                recCount = dv.Count
                Session("recCount") = recCount
                moDataGrid.PageSize = State.selectedPageSize
                lblRecordCount.Text = recCount & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)

                'Me.lblRecordCount.Text = CType(searchtotal, String) & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                'moDataGrid.VirtualItemCount = CType(searchtotal, String)

                'Me.lblRecordCount.Text = CType(searchtotal, String) & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                '    moDataGrid.VirtualItemCount = CType(searchtotal, String)



                'Check if Reconciliation flag is on for the dealer
                moDataGrid.DataSource = dv.ToTable
                moDataGrid.DataBind()
                ControlMgr.DisableAllGridControlsIfNotEditAuth(Me, moDataGrid)

                'Check if Reconciliation flag is on for the dealer
                Dim oDealerFile As DealerFileProcessed = New DealerFileProcessed(State.DealerfileProcessedId)

                With oDealerFile
                    Dim objDealer As New Dealer(.DealerId)

                    If LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, objDealer.ReconRejRecTypeId) = Codes.YESNO_Y Then
                        If State.RecordMode = RECORD_MODE__REMAINING_REJECTED Then
                            If oDealerFile.IsChildFile Then
                                EnableAllGridControls(moDataGrid, False)
                            Else
                                EnableAllGridControls(moDataGrid, True)
                            End If
                        Else
                            EnableAllGridControls(moDataGrid, False)
                        End If
                    End If
                End With
                EnableDisableControl(SaveButton_WRITE, moDataGrid)
                EnableDisableControl(btnUndo_WRITE, moDataGrid)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        'Private Sub PopulateBundlesGrid(ByVal guid As Guid)

        '    Dim dv As DataView

        '    Try
        '        If Me.State.BundlesHashTable Is Nothing Then
        '            dv = GetBundlesDV()
        '        Else
        '            If Me.State.BundlesHashTable.Contains(guid) Then
        '                dv = CType(Me.State.BundlesHashTable.Item(guid), DataSet).Tables(DealerReconWrkBundlesDAL.TABLE_NAME).DefaultView
        '            Else
        '                dv = GetBundlesDV()
        '            End If
        '        End If

        '        Me.gvPop.DataSource = dv
        '        Me.gvPop.DataBind()
        '        ControlMgr.DisableAllGridControlsIfNotEditAuth(Me, gvPop)

        '        EnableDisableControl(Me.btnApply, Me.gvPop)
        '    Catch ex As Exception
        '        Me.HandleErrors(ex, Me.MasterPage.MessageController)
        '    End Try

        'End Sub

        Private Function GetDV() As DataView

            Dim dv As DataView

            dv = GetGridDataView()
            'dv.Sort = moDataGrid.DataMember()

            Return (dv)

        End Function

        'Private Function GetBundlesDV() As DataView
        '    Dim dv As DataView

        '    dv = GetBundlesDataSet().Tables(DealerReconWrkBundlesDAL.TABLE_NAME).DefaultView
        '    Return dv
        'End Function

        Private Function GetGridDataView() As DataView

            With State
                Return (Assurant.ElitaPlus.BusinessObjectsNew.ARInvoiceReconWrk.LoadList(.DealerfileProcessedId,
                                                                                      .RecordMode,
                                                                                      .srchRecordType,
                                                                                      .srchRejectCode,
                                                                                      .srchRejectReason,
                                                                                      .ParentFile,
                                                                                      .selectedPageIndex,
                                                                                      .selectedPageSize,
                                                                                      .SortExpression))


            End With

        End Function

        'Private Function GetBundlesDataSet() As DataSet
        '    With State
        '        Return (Assurant.ElitaPlus.BusinessObjectsNew.DealerReconWrkBundles.LoadList(.DealerReconWrkId))
        '    End With
        'End Function

        Private Sub PopulateBOItem(ARInvoiceReconWrkInfo As ARInvoiceReconWrk, oPropertyName As String, oCellPosition As Integer)
            PopulateBOProperty(ARInvoiceReconWrkInfo, oPropertyName,
                                            CType(GetSelectedGridControl(moDataGrid, oCellPosition), TextBox))
        End Sub

        Private Sub PopulateBODrop(ARInvoiceReconWrkInfo As ARInvoiceReconWrk, oPropertyName As String, oCellPosition As Integer)
            PopulateBOProperty(ARInvoiceReconWrkInfo, oPropertyName,
                                CType(GetSelectedGridControl(moDataGrid, oCellPosition), DropDownList), False)
        End Sub

        'Private Sub PopulateBundlesBOItem(ByVal dealerReconWrkBundlesInfo As DealerReconWrkBundles, ByVal oPropertyName As String, ByVal oCellPosition As Integer)
        '    Me.PopulateBundlesBOProperty(dealerReconWrkBundlesInfo, oPropertyName,
        '                                    CType(Me.GetSelectedGridControl(gvPop, oCellPosition), TextBox))
        'End Sub

        Private Sub PopulateBOFromForm(ARInvoiceReconWrkInfo As ARInvoiceReconWrk)

            PopulateBODrop(ARInvoiceReconWrkInfo, RECORD_TYPE_PROPERTY, RECORD_TYPE_COL)
            PopulateBOItem(ARInvoiceReconWrkInfo, REJECT_CODE_PROPERTY, REJECT_CODE_COL)
            PopulateBOItem(ARInvoiceReconWrkInfo, REJECT_REASON_PROPERTY, REJECT_REASON_COL)
            PopulateBOItem(ARInvoiceReconWrkInfo, INVOICE_NUMBER_PROPERTY, INVOICE_NUMBER_COL)
            PopulateBOItem(ARInvoiceReconWrkInfo, CERTIFICATE_PROPERTY, CERTIFICATE_COL)
            PopulateBOItem(ARInvoiceReconWrkInfo, INVOICE_PERIOD_START_DATE_PROPERTY, INVOICE_PERIOD_START_DATE_COL)
            PopulateBOItem(ARInvoiceReconWrkInfo, INVOICE_PERIOD_END_DATE_PROPERTY, INVOICE_PERIOD_END_DATE_COL)
            PopulateBOItem(ARInvoiceReconWrkInfo, INVOICE_DATE_PROPERTY, INVOICE_DATE_COL)
            PopulateBOItem(ARInvoiceReconWrkInfo, INVOICE_LOADED_PROPERTY, INVOICE_LOADED_COL)
            PopulateBOItem(ARInvoiceReconWrkInfo, CURRENCY_CODE_PROPERTY, CURRENCY_CODE_COL)
            PopulateBOItem(ARInvoiceReconWrkInfo, EXCHANGE_RATE_PROPERTY, EXCHANGE_RATE_COL)
            PopulateBOItem(ARInvoiceReconWrkInfo, LINE_TYPE_PROPERTY, LINE_TYPE_COL)
            PopulateBOItem(ARInvoiceReconWrkInfo, ITEM_CODE_PROPERTY, ITEM_CODE_COL)
            PopulateBOItem(ARInvoiceReconWrkInfo, EARNING_PARTNER_PROPERTY, EARNING_PARTER_COL)
            PopulateBOItem(ARInvoiceReconWrkInfo, SOURCE_PROPERTY, SOURCE_COL)
            PopulateBOItem(ARInvoiceReconWrkInfo, INSTALLMENT_NUMBER_PROPERTY, INSTALLMENT_NUMBER_COL)
            PopulateBOItem(ARInvoiceReconWrkInfo, AMOUNT_PROPERTY, AMOUNT_COL)
            PopulateBOItem(ARInvoiceReconWrkInfo, PARENT_LINE_NUMBER_PROPERTY, PARENT_LINE_NUMBER_COL)
            PopulateBOItem(ARInvoiceReconWrkInfo, DESCRIPTION_PROPERTY, DESCRIPTION_COL)
            PopulateBOItem(ARInvoiceReconWrkInfo, ENTIRE_RECORD_PROPERTY, ENTIRE_RECORD_COL)

            PopulateBOItem(ARInvoiceReconWrkInfo, REFERENCES_PROPERTY, REFERENCE_COL)






            'PopulateBOItem(ARInvoiceReconWrkInfo, RECORD_TYPE_PROPERTY, RECORD_TYPE_COL)


            'PopulateBOItem(ARInvoiceReconWrkInfo, ITEM_CODE_PROPERTY, ITEM_CODE_COL)


            'PopulateBOItem(dealerReconWrkInfo, SR_PROPERTY, SR_COL)
            '    PopulateBOItem(dealerReconWrkInfo, BRANCH_CODE_PROPERTY, BRANCH_CODE_COL)
            '    PopulateBOItem(dealerReconWrkInfo, IDENTIFICATION_NUMBER_PROPERTY, IDENTIFICATION_NUMBER_COL)
            '    PopulateBOItem(dealerReconWrkInfo, ADDRESS1_PROPERTY, ADDRESS1_COL)
            '    PopulateBOItem(dealerReconWrkInfo, CITY_PROPERTY, CITY_COL)
            '    PopulateBOItem(dealerReconWrkInfo, HOME_PHONE_PROPERTY, HOME_PHONE_COL)
            '    PopulateBOItem(dealerReconWrkInfo, WORK_PHONE_PROPERTY, WORK_PHONE_COL)
            '    PopulateBOItem(dealerReconWrkInfo, MANUFACTURER_PROPERTY, MANUFACTURER_COL)

            If ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub

        'Private Sub PopulateBOFromBundlesForm(ByVal dealerReconWrkBundlesInfo As DealerReconWrkBundles)
        '    PopulateBundlesBOItem(dealerReconWrkBundlesInfo, ITEM_NUMBER, ITEM_NUMBER_COL)
        '    PopulateBundlesBOItem(dealerReconWrkBundlesInfo, ITEM_MANUFACTURER, ITEM_MANUFACTURER_COL)
        '    PopulateBundlesBOItem(dealerReconWrkBundlesInfo, ITEM_MODEL, ITEM_MODEL_COL)
        '    PopulateBundlesBOItem(dealerReconWrkBundlesInfo, ITEM_SERIAL_NUMBER, ITEM_SERIAL_NUMBER_COL)
        '    PopulateBundlesBOItem(dealerReconWrkBundlesInfo, ITEM_DESCRIPTION, ITEM_DESCRIPTION_COL)
        '    PopulateBundlesBOItem(dealerReconWrkBundlesInfo, ITEM_PRICE, ITEM_PRICE_COL)
        '    PopulateBundlesBOItem(dealerReconWrkBundlesInfo, ITEM_BUNDLE_VALUE, ITEM_BUNDLE_VALUE_COL)
        '    PopulateBundlesBOItem(dealerReconWrkBundlesInfo, ITEM_MAN_WARRANTY, ITEM_MAN_WARRANTY_COL)

        '    If Me.ErrCollection.Count > 0 Then
        '        Throw New PopulateBOErrorException
        '    End If
        'End Sub

        Private Sub PopulateFormItem(oCellPosition As Integer, oPropertyValue As Object)
            PopulateControlFromBOProperty(GetSelectedGridControl(moDataGrid, oCellPosition), oPropertyValue)
        End Sub

#End Region

#Region "GridHandlers"

        Private Sub moDataGrid_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles moDataGrid.PageIndexChanging
            Try
                State.selectedPageIndex = e.NewPageIndex
                If IsDataGPageDirty() Then
                    DisplayMessage(Message.MSG_PAGE_SAVE_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSavePagePromptResponse)
                Else
                    moDataGrid.PageIndex = e.NewPageIndex
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
                    State.selectedPageIndex = 0
                    State.selectedPageSize = CType(cboPageSize.SelectedValue, Integer)
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
            Dim oLabel As Label

            'translate the reject message
            'Dim strMsg As String, dr As DataRow, intParamCnt As Integer = 0, strParamList As String = String.Empty
            'If Not dvRow Is Nothing Then
            '    dr = dvRow.Row
            '    strMsg = dr(ARInvoiceReconWrkDAL.COL_NAME_TRANSLATED_MSG).ToString.Trim
            '    If strMsg <> String.Empty Then
            '        If Not dr(ARInvoiceReconWrkDAL.COL_NAME_MSG_PARAMETER_COUNT) Is DBNull.Value Then
            '            intParamCnt = CType(dr(ARInvoiceReconWrkDAL.COL_NAME_MSG_PARAMETER_COUNT), Integer)
            '        End If
            '        If intParamCnt > 0 Then
            '            If Not dr(ARInvoiceReconWrkDAL.COL_NAME_REJECT_MSG_PARMS) Is DBNull.Value Then
            '                strParamList = dr(ARInvoiceReconWrkDAL.COL_NAME_REJECT_MSG_PARMS).ToString.Trim
            '            End If
            '            strMsg = TranslationBase.TranslateParameterizedMsg(strMsg, intParamCnt, strParamList).Trim
            '        End If
            '        If strMsg <> "" Then dr(ARInvoiceReconWrkDAL.COL_NAME_REJECT_REASON) = strMsg
            '        dr.AcceptChanges()
            '    End If
            'End If


            'Freeze Headers - Start
            If (itemType = ListItemType.Header) Then
                'moDataGrid.HeaderStyle.CssClass = "locked"
                'moDataGrid.HeaderRow.Cells(1).CssClass = "locked"
                'moDataGrid.HeaderRow.Cells(2).CssClass = "locked"
                'moDataGrid.HeaderRow.Cells(3).CssClass = "locked"
            End If
            'Freeze Headers - Start

            If (itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem) AndAlso e.Row.RowIndex <> -1 Then
                With e.Row
                    .Attributes("onclick") = "setHighlighter(this)"
                    'Freeze Columns - Start
                    '.Cells(0).CssClass = "locked"
                    '.Cells(1).CssClass = "locked"
                    '.Cells(2).CssClass = "locked"
                    '.Cells(3).CssClass = "locked"
                    'Freeze columns - End

                    'Determine whether to show the "Bundles" button in the grid
                    'If Not IsDBNull(dvRow(BUNDLES_INFO)) Then
                    '    e.Row.Cells(e.Row.Cells.Count - 1).Visible = CBool(dvRow(BUNDLES_INFO))
                    'Else
                    '    e.Row.Cells(e.Row.Cells.Count - 1).Visible = False
                    'End If

                    ' Retrieve the foreign key value
                    oTextBox = CType(.FindControl("moCancelCodeTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(ARInvoiceReconWrk.COL_NAME_INVOICE_NUMBER))

                    'Dim ddl As DropDownList = CType(e.Row.FindControl("ddl1"), DropDownList)
                    'ddl.SelectedIndex = ddl.Items.IndexOf(ddl.Items.FindByValue(oTextBox.Text))
                    'AssignDropDownToCtr(ddl, oTextBox)
                    PopulateControlFromBOProperty(.FindControl("moDealerReconWrkIdLabel"), dvRow(ARInvoiceReconWrk.COL_NAME_INVOICE_INTERFACE_ID))
                    Dim oDrop As DropDownList = CType(e.Row.FindControl("moRecordTypeDrop"), DropDownList)
                    oDrop.Attributes.Add("onchange", "setDirty()")
                    PopulateRecordTypeDrop(oDrop)
                    Dim oValue As String = CType(dvRow(ARInvoiceReconWrk.COL_NAME_RECORD_TYPE), String)
                    SetSelectedItemByText(oDrop, oValue)
                    oTextBox = CType(.FindControl("moRejectCode"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(ARInvoiceReconWrk.COL_NAME_REJECT_CODE))
                    oTextBox = CType(.FindControl("RejectReasonTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(ARInvoiceReconWrk.COL_NAME_REJECT_REASON))

                    'oTextBox = CType(.Cells(DEALER_COL).FindControl("moDealerTextGrid"), TextBox)
                    'Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_DEALER))

                    oTextBox = CType(.FindControl("moProductCodeTextGrid"), TextBox)

                    oTextBox.Enabled = True
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(ARInvoiceReconWrk.COL_NAME_INVOICE_DATE))

                    oTextBox = CType(.FindControl("moOriginalRetailPriceTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(ARInvoiceReconWrk.COL_NAME_INVOICE_LOADED))
                    oTextBox = CType(.FindControl("moManWarrantyTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(ARInvoiceReconWrk.COL_NAME_LINE_TYPE))
                    oTextBox = CType(.FindControl("moExtWarrantyTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(ARInvoiceReconWrk.COL_NAME_ITEM_CODE))
                    oTextBox = CType(.FindControl("moPricePolTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(ARInvoiceReconWrk.COL_NAME_CURRENCY_CODE))
                    'oTextBox = CType(.FindControl("moNumberCompTextGrid"), TextBox)
                    'oTextBox.Attributes.Add("onchange", "setDirty()")
                    'Me.PopulateControlFromBOProperty(oTextBox, dvRow(ARInvoiceReconWrk.COL_NAME_INVOICE_NUMBER))
                    oTextBox = CType(.FindControl("moDateCompTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Dim oDateCompImage As ImageButton = CType(.FindControl("moDateCompImageGrid"), ImageButton)
                    AddCalendar(oDateCompImage, oTextBox)

                    PopulateControlFromBOProperty(oTextBox, dvRow(ARInvoiceReconWrk.COL_NAME_INVOICE_PERIOD_START_DATE))
                    oTextBox = CType(.FindControl("moCertificateTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(ARInvoiceReconWrk.COL_NAME_CERTIFICATE))
                    oTextBox = CType(.FindControl("moNewBranchCodeTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(ARInvoiceReconWrk.COL_NAME_SOURCE))
                    'oTextBox = CType(.FindControl("moCustNameTextGrid"), TextBox)
                    'oTextBox.Attributes.Add("onchange", "setDirty()")
                    'Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_CUSTOMER_NAME))
                    'oTextBox = CType(.FindControl("moLangPrefTextGrid"), TextBox)
                    'oTextBox.Attributes.Add("onchange", "setDirty()")
                    'Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_LANGUAGE_PREF))
                    'oTextBox = CType(.FindControl("moZipTextGrid"), TextBox)
                    'oTextBox.Attributes.Add("onchange", "setDirty()")
                    'Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_ZIP))
                    'oTextBox = CType(.FindControl("moStateProvTextGrid"), TextBox)
                    'oTextBox.Attributes.Add("onchange", "setDirty()")
                    'Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_STATE_PROVINCE))
                    ''oTextBox = CType(e.Row.Cells(CURRENCY_COL).FindControl("moCurrencyTextGrid"), TextBox)
                    'oTextBox = CType(.FindControl("moCurrencyTextGrid"), TextBox)
                    'oTextBox.Attributes.Add("onchange", "setDirty()")
                    'Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_CURRENCY))
                    'Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_ISO_CODE))
                    oTextBox = CType(.FindControl("moExtWarrSaleDateTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Dim oExtWarrSaleDateImage As ImageButton = CType(.FindControl("moExtWarrSaleDateImageGrid"), ImageButton)

                    AddCalendar(oExtWarrSaleDateImage, oTextBox)

                    PopulateControlFromBOProperty(oTextBox, dvRow(ARInvoiceReconWrk.COL_NAME_INVOICE_PERIOD_END_DATE))
                    'oTextBox = CType(.FindControl("moTypePaymentTextGrid"), TextBox)
                    'oTextBox.Attributes.Add("onchange", "setDirty()")
                    'Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_TYPE_PAYMENT))

                    'oTextBox = CType(.FindControl("moServiceLineNumberTextGrid"), TextBox)
                    'oTextBox.Attributes.Add("onchange", "setDirty()")
                    'Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_SERVICE_LINE_NUMBER))



                    'If Not IsCancellationsFileType(moFileNameText.Text) Then
                    'oTextBox = CType(.FindControl("moRecordTypeTextGrid"), TextBox)
                    ' oTextBox.Attributes.Add("onchange", "setDirty()")

                    '' ''Dim oDrop As DropDownList = CType(e.Row.FindControl("moRecordTypeDrop"), DropDownList)
                    '' ''oDrop.Attributes.Add("onchange", "setDirty()")
                    '' ''PopulateRecordTypeDrop(oDrop)
                    '' ''Dim oValue As String = CType(dvRow(DealerReconWrk.COL_NAME_RECORD_TYPE), String)
                    '' ''Me.SetSelectedItemByText(oDrop, oValue)

                    '  Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_RECORD_TYPE))
                    oTextBox = CType(.FindControl("moItemCodeTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(ARInvoiceReconWrk.COL_NAME_ITEM_CODE))
                    'oTextBox = CType(.FindControl("moItemTextGrid"), TextBox)
                    'oTextBox.Attributes.Add("onchange", "setDirty()")
                    'Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_ITEM))
                    'Req 846
                    'oTextBox = CType(.FindControl("moSkuNumberTextGrid"), TextBox)
                    'oTextBox.Attributes.Add("onchange", "setDirty()")
                    'Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_SKU_NUMBER))
                    oTextBox = CType(.FindControl("moSrTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(ARInvoiceReconWrk.COL_NAME_AMOUNT))
                    oTextBox = CType(.FindControl("moBranchCodeTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(ARInvoiceReconWrk.COL_NAME_EARNING_PARTER))
                    oTextBox = CType(.FindControl("moSalesTaxTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(ARInvoiceReconWrk.COL_NAME_EXCHANGE_RATE))
                    'oTextBox = CType(.FindControl("moAddressTextGrid"), TextBox)
                    'oTextBox.Attributes.Add("onchange", "setDirty()")
                    'Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_ADDRESS1))
                    'oTextBox = CType(.FindControl("moCityTextGrid"), TextBox)
                    'Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_CITY))
                    'oTextBox = CType(.FindControl("moHomePhoneTextGrid"), TextBox)
                    'oTextBox.Attributes.Add("onchange", "setDirty()")
                    'Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_HOME_PHONE))
                    ''SetColumnState(WORK_PHONE_COL, False)
                    'oTextBox = CType(.FindControl("moWorkPhoneTextGrid"), TextBox)
                    'oTextBox.Attributes.Add("onchange", "setDirty()")
                    'Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_WORK_PHONE))
                    ''SetColumnState(MANUFACTURER_COL, False)
                    oTextBox = CType(.FindControl("moManufacturerTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(ARInvoiceReconWrk.COL_NAME_INSTALLMENT_NUMBER))
                    'SetColumnState(MODEL_COL, False)
                    oTextBox = CType(.FindControl("moModelTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(ARInvoiceReconWrk.COL_NAME_PARENT_LINE_NUMBER))
                    'SetColumnState(SERIAL_NUMBER_COL, False)
                    oTextBox = CType(.FindControl("moSerialNumTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(ARInvoiceReconWrk.COL_NAME_DESCRIPTION))
                    oTextBox = CType(.FindControl("moIMEINumTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(ARInvoiceReconWrk.COL_NAME_ENTIRE_RECORD))
                    'SetColumnState(NEW_PRODUCT_CODE_COL, False)
                    ''oTextBox = CType(.FindControl("moNewProdCodeTextGrid"), TextBox)
                    ''oTextBox.Enabled = True

                    ''oTextBox.Attributes.Add("onchange", "setDirty()")

                    ''    Me.PopulateControlFromBOProperty(oTextBox, dvRow(ARInvoiceReconWrk.COL_NAME_INVOCE_DUE_DATE))
                    'oTextBox = CType(.FindControl("moDocumentTypeTextGrid"), TextBox)
                    'oTextBox.Attributes.Add("onchange", "setDirty()")



                    ' Else
                    'SetColumnState(CANCELLATION_CODE_COL, False)
                    'oTextBox = CType(.FindControl("moCancelCodeTextGrid"), TextBox)
                    '    oTextBox.Attributes.Add("onchange", "setDirty()")
                    '    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_CANCELLATION_CODE))
                    'End If

                End With
            End If
            BaseItemBound(source, e)

        End Sub

        Private Sub Grid_SortCommand(source As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles moDataGrid.Sorting
            Try
                'If Me.State.sortBy.StartsWith(e.SortExpression) Then
                'If Me.SortDirection = " ASC" Then
                '    e.SortExpression = e.SortExpression + " DESC"
                '    Me.SortDirection = " DESC"
                '    Me.State.sortBy = e.SortExpression
                'Else
                '    e.SortExpression = e.SortExpression + "  ASC"
                '    Me.SortDirection = " ASC"
                '    Me.State.sortBy = e.SortExpression
                'End If
                'Else
                'Me.State.sortBy = e.SortExpression
                'End If
                'Dim spaceIndex As Integer = Me.SortDirection.LastIndexOf(" ")
                State.SortExpression = e.SortExpression
                'Me.SortDirection = e.SortDirection.ToString()
                If SortDirection = "Ascending" Then
                    SortDirection = "Descending"
                    State.SortExpression = e.SortExpression + " DESC"
                Else
                    SortDirection = "Ascending"
                    State.SortExpression = e.SortExpression + " ASC"
                End If
                'If Me.SortDirection = "Ascending" Then
                '    Me.State.SortDirection = " ASC"
                'End If
                'If spaceIndex > 0 AndAlso Me.SortDirection.Substring(0, spaceIndex).Equals(e.SortExpression) Then
                'If Me.SortDirection.EndsWith(" ASC") Then
                'Me.SortDirection = e.SortExpression + " DESC"
                'Me.State.SortDirection = " DESC"

                'Else

                'End If
                'Else
                'Me.State.SortDirection = " ASC"
                'End If

                'If Me.State.SortDirection = "DESC" Then
                '    Me.State.SortDir = 2

                'Else
                '    Me.State.SortDir = 1
                'End If


                State.PageIndex = 0
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Protected Overloads Sub ItemCreated(sender As Object, e As DataGridItemEventArgs)
            BaseItemCreated(sender, e)
        End Sub

        Protected Overloads Sub ItemCreated(sender As Object, e As GridViewRowEventArgs)
            BaseItemCreated(sender, e)
        End Sub

        Protected Sub BindBoPropertiesToGridHeaders(ARInvoiceReconWrkInfo As ARInvoiceReconWrk)
            BindBOPropertyToGridHeader(ARInvoiceReconWrkInfo, "RecordType", moDataGrid.Columns(RECORD_TYPE_COL))
            ' Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, Description, Me.moDataGrid.Columns(SKU_Number))
            BindBOPropertyToGridHeader(ARInvoiceReconWrkInfo, "RejectReason", moDataGrid.Columns(REJECT_REASON_COL))
            BindBOPropertyToGridHeader(ARInvoiceReconWrkInfo, "CurrencyCode", moDataGrid.Columns(CURRENCY_CODE_COL))
            BindBOPropertyToGridHeader(ARInvoiceReconWrkInfo, "ExchangeRate", moDataGrid.Columns(EXCHANGE_RATE_COL))
            BindBOPropertyToGridHeader(ARInvoiceReconWrkInfo, "LineType", moDataGrid.Columns(LINE_TYPE_COL))
            BindBOPropertyToGridHeader(ARInvoiceReconWrkInfo, "InvoiceNumber", moDataGrid.Columns(INVOICE_NUMBER_COL))
            BindBOPropertyToGridHeader(ARInvoiceReconWrkInfo, "ItemCode", moDataGrid.Columns(ITEM_CODE_COL))
            BindBOPropertyToGridHeader(ARInvoiceReconWrkInfo, "EarningPartner", moDataGrid.Columns(EARNING_PARTER_COL))
            BindBOPropertyToGridHeader(ARInvoiceReconWrkInfo, "InvoicePeriodEndDate", moDataGrid.Columns(INVOICE_PERIOD_END_DATE_COL))
            BindBOPropertyToGridHeader(ARInvoiceReconWrkInfo, "InvoicePeriodStartDate", moDataGrid.Columns(INVOICE_PERIOD_START_DATE_COL))
            BindBOPropertyToGridHeader(ARInvoiceReconWrkInfo, "InvoiceDate", moDataGrid.Columns(INVOICE_DATE_COL))
            BindBOPropertyToGridHeader(ARInvoiceReconWrkInfo, "InvoiceDueDate", moDataGrid.Columns(INVOICE_DUE_DATE_COL))
            BindBOPropertyToGridHeader(ARInvoiceReconWrkInfo, "Source", moDataGrid.Columns(SOURCE_COL))
            BindBOPropertyToGridHeader(ARInvoiceReconWrkInfo, "Reference", moDataGrid.Columns(REFERENCE_COL))
            BindBOPropertyToGridHeader(ARInvoiceReconWrkInfo, "InstallmentNumber", moDataGrid.Columns(INSTALLMENT_NUMBER_COL))
            BindBOPropertyToGridHeader(ARInvoiceReconWrkInfo, "Certificate", moDataGrid.Columns(CERTIFICATE_COL))
            BindBOPropertyToGridHeader(ARInvoiceReconWrkInfo, "Amount", moDataGrid.Columns(AMOUNT_COL))
            BindBOPropertyToGridHeader(ARInvoiceReconWrkInfo, "ParentLineNumber", moDataGrid.Columns(PARENT_LINE_NUMBER_COL))
            BindBOPropertyToGridHeader(ARInvoiceReconWrkInfo, "Description", moDataGrid.Columns(DESCRIPTION_COL))
            BindBOPropertyToGridHeader(ARInvoiceReconWrkInfo, "EntireRecord", moDataGrid.Columns(ENTIRE_RECORD_COL))
            ' Me.BindBOPropertyToGridHeader(ARInvoiceReconWrkInfo, "InvoiceLoaded", Me.moDataGrid.Columns(INVOICE_LOADED_COL))
            'Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "City", Me.moDataGrid.Columns(CITY_COL))
            'Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "Zip", Me.moDataGrid.Columns(ZIP_COL))
            'Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "StateProvince", Me.moDataGrid.Columns(STATE_PROVINCE_COL))
            'Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "HomePhone", Me.moDataGrid.Columns(HOME_PHONE_COL))
            'Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "WorkPhone", Me.moDataGrid.Columns(WORK_PHONE_COL))
            'Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "IsoCode", Me.moDataGrid.Columns(ISO_CODE_COL))
            'Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "ExtwarrSaledate", Me.moDataGrid.Columns(EXTWARR_SALEDATE_COL))
            'Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "TypePayment", Me.moDataGrid.Columns(TYPE_PAYMENT_COL))
            'Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "CancellationCode", Me.moDataGrid.Columns(CANCELLATION_CODE_COL))
            'Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "Manufacturer", Me.moDataGrid.Columns(MANUFACTURER_COL))
            'Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "Model", Me.moDataGrid.Columns(MODEL_COL))
            'Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "SerialNumber", Me.moDataGrid.Columns(SERIAL_NUMBER_COL))
            'Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "IMEINumber", Me.moDataGrid.Columns(IMEI_NUMBER_COL))
            'Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "PostPrePaid", Me.moDataGrid.Columns(POST_PRE_PAID_COL))
            'Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "NewProductCode", Me.moDataGrid.Columns(NEW_PRODUCT_CODE_COL))
            'Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "DocumentType", Me.moDataGrid.Columns(DOCUMENT_TYPE_COL))
            'Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "DocumentAgency", Me.moDataGrid.Columns(DOCUMENT_AGENCY_COL))
            'Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "DocumentIssueDate", Me.moDataGrid.Columns(DOCUMENT_ISSUE_DATE_COL))
            'Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "RGNumber", Me.moDataGrid.Columns(RG_NUMBER_COL))
            'Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "IDType", Me.moDataGrid.Columns(ID_TYPE_COL))
            'Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "BillingFrequency", Me.moDataGrid.Columns(BILLING_FREQUENCY_COL))
            'Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "NumberOfInstallments", Me.moDataGrid.Columns(NUMBER_OF_INSTALLMENTS_COL))
            'Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "InstallmentAmount", Me.moDataGrid.Columns(INSTALLMENT_AMOUNT_COL))
            'Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "BankRtnNumber", Me.moDataGrid.Columns(BANK_RTN_NUMBER_COL))
            'Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "BankAccountNumber", Me.moDataGrid.Columns(BANK_ACCOUNT_NUMBER_COL))
            'Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "BankAcctOwnerName", Me.moDataGrid.Columns(BANK_ACCT_OWNER_NAME_COL))
            'Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "BankBranchNumber", Me.moDataGrid.Columns(BANK_BRANCH_NUMBER_COL))

            'Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "SalesTax", Me.moDataGrid.Columns(SALES_TAX_COL))
            'Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "Address2", Me.moDataGrid.Columns(ADDRESS2_COL))
            ''Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "Email", Me.moDataGrid.Columns(EMAIL_COL))

            'Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "CustCountry", Me.moDataGrid.Columns(CUST_COUNTRY_COL))
            'Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "CountryPurch", Me.moDataGrid.Columns(COUNTRY_PURCH_COL))

            'Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "DatePaidFor", Me.moDataGrid.Columns(DATE_PAID_FOR_COL))
            'Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, MEMBERSHIP_NUMBER_PROPERTY, Me.moDataGrid.Columns(MEMBERSHIP_NUMBER_COL))

            'Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "Address3", Me.moDataGrid.Columns(ADDRESS3_COL))
            'Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "OriginalRetailPrice", Me.moDataGrid.Columns(ORIGINAL_RETAIL_PRICE_COL))
            'Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "BillingPlan", Me.moDataGrid.Columns(BILLING_PLAN_COL))
            'Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "BillingCycle", Me.moDataGrid.Columns(BILLING_CYCLE_COL))
            'Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "SubscriberStatus", Me.moDataGrid.Columns(SUBSCRIBER_STATUS_COL))
            'Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "SuspendedReason", Me.moDataGrid.Columns(SUSPENDED_REASON_COL))

            'Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "MobileType", Me.moDataGrid.Columns(MOBILE_TYPE_COL))
            'Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "FirstUseDate", Me.moDataGrid.Columns(FIRST_USE_DATE_COL))
            'Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "LastUseDate", Me.moDataGrid.Columns(LAST_USE_DATE_COL))
            'Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "SimCardNumber", Me.moDataGrid.Columns(SIM_CARD_NUMBER_COL))
            'Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "Region", Me.moDataGrid.Columns(REGION_COL))
            'Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, MEMBERSHIP_TYPE_PROPERTY, Me.moDataGrid.Columns(MEMBERSHIP_TYPE_COL))
            'Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, CESS_OFFICE_PROPERTY, Me.moDataGrid.Columns(CESS_OFFICE_COL))
            'Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, CESS_SALESREP_PROPERTY, Me.moDataGrid.Columns(CESS_SALESREP_COL))
            'Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, BUSINESSLINE_PROPERTY, Me.moDataGrid.Columns(BUSINESSLINE_COL))
            'Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, SALES_DEPARTMENT_PROPERTY, Me.moDataGrid.Columns(SALES_DEPARTMENT_COL))
            'Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, LINKED_CERT_NUMBER_PROPERTY, Me.moDataGrid.Columns(LINKED_CERT_NUMBER_COL))
            'Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, ADDITIONAL_INFO_PROPERTY, Me.moDataGrid.Columns(ADDITIONAL_INFO_COL))
            'Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, CREDITCARD_LASTFOURDIGIT_PROPERTY, Me.moDataGrid.Columns(CREDITCARD_LAST_FOUR_DIGIT_COL)) 'REQ-1169

            'Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, FINANCED_AMOUNT, Me.moDataGrid.Columns(FINANCED_AMOUNT_COL))
            'Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, FINANCED_FREQUENCY, Me.moDataGrid.Columns(FINANCED_FREQUENCY_COL))
            'Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, FINANCED_INSTALLMENT_NUMBER, Me.moDataGrid.Columns(FINANCED_INSTALLMENT_NUMBER_COL))
            'Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, FINANCED_INSTALLMENT_AMOUNT, Me.moDataGrid.Columns(FINANCED_INSTALLMENT_AMOUNT_COL))
            'Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, GENDER_PROPERTY, Me.moDataGrid.Columns(GENDER_COL))
            'Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, PLACE_OF_BIRTH_PROPERTY, Me.moDataGrid.Columns(PLACE_OF_BIRTH_COL))
            'Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, CUIT_CUIL_PROPERTY, Me.moDataGrid.Columns(CUIT_CUIL_COL))
            'Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, PERSON_TYPE_PROPERTY, Me.moDataGrid.Columns(PERSON_TYPE_COL))

            'Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, NUM_OF_CONSECUTIVE_PAYMENTS_PROPERTY, Me.moDataGrid.Columns(NUM_OF_CONSECUTIVE_PAYMENTS_COL))

            'Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, DEALER_CURRENT_PLAN_CODE_PROPERTY, Me.moDataGrid.Columns(DEALER_CURRENT_PLAN_CODE_COL))
            'Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, DEALER_SCHEDULED_PLAN_CODE_PROPERTY, Me.moDataGrid.Columns(DEALER_SCHEDULED_PLAN_CODE_COL))
            'Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, DEALER_UPDATE_REASON_PROPERTY, Me.moDataGrid.Columns(DEALER_UPDATE_REASON_COL))

            'Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, MARITAL_STATUS_PROPERTY, Me.moDataGrid.Columns(MARITAL_STATUS_COL))
            'Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, NATIONALITY_PROPERTY, Me.moDataGrid.Columns(NATIONALITY_COL))
            'Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, BIRTH_DATE_PROPERTY, Me.moDataGrid.Columns(BIRTH_DATE_COL))
            'Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, FINANCE_DATE_PROPERTY, Me.moDataGrid.Columns(FINANCE_DATE_COL))
            'Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, DOWN_PAYMENT_PROPERTY, Me.moDataGrid.Columns(DOWN_PAYMENT_COL))
            'Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, ADVANCE_PAYMENT_PROPERTY, Me.moDataGrid.Columns(ADVANCE_PAYMENT_COL))
            'Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, UPGRADE_TERM_PROPERTY, Me.moDataGrid.Columns(UPGRADE_TERM_COL))
            'Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, BILLING_ACCOUNT_NUMBER_PROPERTY, Me.moDataGrid.Columns(BILLING_ACCOUNT_NUMBER_COL))
            'Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, SERVICE_LINE_NUMBER_PROPERTY, Me.moDataGrid.Columns(SERVICE_LINE_NUMBER_COL))


            ClearGridViewHeadersAndLabelsErrSign()
        End Sub

        'Protected Sub BindBoPropertiesToBundlesGridHeaders(ByVal dealerReconWrkBundlesInfo As DealerReconWrkBundles)
        '    Me.BindBOPropertyToGridHeader(dealerReconWrkBundlesInfo, "ItemNumber", Me.gvPop.Columns(ITEM_NUMBER_COL))
        '    Me.BindBOPropertyToGridHeader(dealerReconWrkBundlesInfo, "ItemManufacturer", Me.gvPop.Columns(ITEM_MANUFACTURER_COL))
        '    Me.BindBOPropertyToGridHeader(dealerReconWrkBundlesInfo, "ItemModel", Me.gvPop.Columns(ITEM_MODEL_COL))
        '    Me.BindBOPropertyToGridHeader(dealerReconWrkBundlesInfo, "ItemSerialNumber", Me.gvPop.Columns(ITEM_SERIAL_NUMBER_COL))
        '    Me.BindBOPropertyToGridHeader(dealerReconWrkBundlesInfo, "ItemDescription", Me.gvPop.Columns(ITEM_DESCRIPTION_COL))
        '    Me.BindBOPropertyToGridHeader(dealerReconWrkBundlesInfo, "ItemPrice", Me.gvPop.Columns(ITEM_PRICE_COL))
        '    Me.BindBOPropertyToGridHeader(dealerReconWrkBundlesInfo, "ItemBundleValue", Me.gvPop.Columns(ITEM_BUNDLE_VALUE_COL))
        '    Me.BindBOPropertyToGridHeader(dealerReconWrkBundlesInfo, "ItemManWarranty", Me.gvPop.Columns(ITEM_MAN_WARRANTY_COL))
        '    Me.ClearGridViewHeadersAndLabelsErrSign()
        'End Sub

        Private Sub SetFocusOnEditableFieldInGrid(grid As DataGrid, cellPosition As Integer, controlName As String, itemIndex As Integer)
            'Set focus on the Description TextBox for the EditItemIndex row
            Dim desc As TextBox = CType(grid.Items(itemIndex).Cells(cellPosition).FindControl(controlName), TextBox)
            SetFocus(desc)
        End Sub

        'Protected Sub BtnViewBundles_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        '    Dim reconWorkIDForm As String = CType(Me.moDataGrid.Rows(Me.moDataGrid.SelectedIndex).FindControl("moDealerReconWrkIdLabel"), Label).Text
        '    Dim reconWorkIDFormGUID As Guid = GetGuidFromString(reconWorkIDForm)
        '    Me.State.DealerReconWrkId = reconWorkIDFormGUID
        '    'PopulateBundlesGrid(reconWorkIDFormGUID)

        '    'update the contents in the detail panel
        '    Me.updPnlBundles.Update()
        '    'show the modal popup
        '    Me.mdlPopup.Show()
        'End Sub

#End Region

#Region "Button Click Events"

        Private Sub btnBack_Click(sender As System.Object, e As System.EventArgs) Handles btnBack.Click
            Try
                If IsDataGPageDirty() Then
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
                'Select Case SaveBundles()
                '    Case 1, 2
                DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)
                'End Select
                HiddenIsPageDirty.Value = EMPTY
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnUndo_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnUndo_WRITE.Click
            Try
                'Me.State.BundlesHashTable = Nothing
                PopulateGrid()
                HiddenIsPageDirty.Value = EMPTY
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub btnApply_Click(sender As System.Object, e As System.EventArgs) Handles btnApply.Click
            Dim hashTable As New Hashtable
            ' ApplyBundles()
        End Sub

        Protected Sub btnClose_Click(sender As System.Object, e As System.EventArgs)
            'If IsDataGBundlesPageDirty() Then
            'ApplyBundles()
            'End If
            'Me.ErrController2.Clear_Hide()
            mdlPopup.Hide()
            ClearGridViewHeadersAndLabelsErrSign()
            'Me.ClearGridHeaders(gvPop)
        End Sub

        Private Sub btnSearch_Click(sender As Object, e As System.EventArgs) Handles btnSearch.Click
            Try
                SaveGuiState()
                SortDirection = EMPTY
                SetGridItemStyleColor(moDataGrid)
                ShowMissingTranslations(MasterPage.MessageController)
                State.PageIndex = 0
                'Me.TranslateGridHeader(moDataGrid)
                'Me.TranslateGridHeader(gvPop)
                'Me.TranslateGridControls(moDataGrid)
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnClearSearch_Click(sender As Object, e As System.EventArgs) Handles btnClearSearch.Click
            Try
                State.srchRecordType = String.Empty
                State.srchRejectCode = String.Empty
                State.srchRejectReason = String.Empty

                moRecordTypeSearchDrop.SelectedIndex = 0
                moRejectCodeText.Text = String.Empty
                moRejectReasonText.Text = String.Empty

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub


#End Region

    End Class

End Namespace
