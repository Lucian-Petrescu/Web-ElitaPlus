Imports Assurant.ElitaPlus.DALObjects
Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Namespace Interfaces

    Partial Class DealerReconWrkForm
        Inherits ElitaPlusSearchPage

#Region "Constants"
        Public Const URL As String = "DealerReconWrkForm.aspx"
        Private Const CANCELLATIONS_TYPE As String = "N"

        Private Const MSG_CONFIRM_PROMPT As String = "MSG_CONFIRM_PROMPT"
        Private Const MSG_RECORD_DELETED_OK As String = "MSG_RECORD_DELETED_OK"
        Private Const MSG_RECORD_SAVED_OK As String = "MSG_RECORD_SAVED_OK"
        Private Const MSG_RECORD_NOT_SAVED As String = "MSG_RECORD_NOT_SAVED"

        Private Const ID_COL As Integer = 1
        Private Const RECORD_TYPE_COL As Integer = 2
        Private Const REJECT_CODE_COL As Integer = 3
        Private Const REJECT_REASON_COL As Integer = 4
        Private Const DEALER_COL As Integer = 5
        Private Const CANCELLATION_CODE_COL As Integer = 6
        Private Const CERTIFICATE_COL As Integer = 7
        Private Const DATE_COMP_COL As Integer = 8
        Private Const EXTWARR_SALEDATE_COL As Integer = 9
        Private Const PRODUCT_CODE_COL As Integer = 10
        Private Const NEW_PRODUCT_CODE_COL As Integer = 11
        Private Const PRODUCT_PRICE_COL As Integer = 12
        Private Const ORIGINAL_RETAIL_PRICE_COL As Integer = 13
        Private Const PRICE_POL_COL As Integer = 14
        Private Const SALES_TAX_COL As Integer = 15
        Private Const MAN_WARRANTY_COL As Integer = 16
        Private Const EXT_WARRANTY_COL As Integer = 17
        Private Const BRANCH_CODE_COL As Integer = 18
        Private Const NEW_BRANCH_CODE_COL As Integer = 19
        Private Const MANUFACTURER_COL As Integer = 20
        Private Const SR_COL As Integer = 21
        Private Const MODEL_COL As Integer = 22
        Private Const SERIAL_NUMBER_COL As Integer = 23
        Private Const IMEI_NUMBER_COL As Integer = 24
        Private Const ITEM_CODE_COL As Integer = 25
        Private Const ITEM_COL As Integer = 26
        Private Const SKU_Number As Integer = 27
        Private Const SALUTATION_COL As Integer = 28
        Private Const CUSTOMER_NAME_COL As Integer = 29
        Private Const IDENTIFICATION_NUMBER_COL As Integer = 30
        Private Const LANGUAGE_PREF_COL As Integer = 31
        Private Const EMAIL_COL As Integer = 32
        Private Const ADDRESS1_COL As Integer = 33
        Private Const ADDRESS2_COL As Integer = 34
        Private Const ADDRESS3_COL As Integer = 35
        Private Const CITY_COL As Integer = 36
        Private Const ZIP_COL As Integer = 37
        Private Const STATE_PROVINCE_COL As Integer = 38
        Private Const CUST_COUNTRY_COL As Integer = 39
        Private Const HOME_PHONE_COL As Integer = 40
        Private Const WORK_PHONE_COL As Integer = 41
        Private Const COUNTRY_PURCH_COL As Integer = 42
        Private Const ISO_CODE_COL As Integer = 43
        Private Const NUMBER_COMP_COL As Integer = 44
        Private Const TYPE_PAYMENT_COL As Integer = 45
        Private Const DOCUMENT_TYPE_COL As Integer = 46
        Private Const DOCUMENT_AGENCY_COL As Integer = 47
        Private Const DOCUMENT_ISSUE_DATE_COL As Integer = 48
        Private Const RG_NUMBER_COL As Integer = 49
        Private Const ID_TYPE_COL As Integer = 50
        Private Const BILLING_FREQUENCY_COL As Integer = 51
        Private Const NUMBER_OF_INSTALLMENTS_COL As Integer = 52
        Private Const INSTALLMENT_AMOUNT_COL As Integer = 53
        Private Const BANK_RTN_NUMBER_COL As Integer = 54
        Private Const BANK_ACCOUNT_NUMBER_COL As Integer = 55
        Private Const BANK_ACCT_OWNER_NAME_COL As Integer = 56
        Private Const BANK_BRANCH_NUMBER_COL As Integer = 57
        Private Const POST_PRE_PAID_COL As Integer = 58
        Private Const BILLING_PLAN_COL As Integer = 59
        Private Const BILLING_CYCLE_COL As Integer = 60
        Private Const DATE_PAID_FOR_COL As Integer = 61
        Private Const MODIFIED_DATE_COL As Integer = 62
        Private Const MEMBERSHIP_NUMBER_COL As Integer = 63
        Private Const SUBSCRIBER_STATUS_COL As Integer = 64
        Private Const SUSPENDED_REASON_COL As Integer = 65
        Private Const MOBILE_TYPE_COL As Integer = 66
        Private Const FIRST_USE_DATE_COL As Integer = 67
        Private Const LAST_USE_DATE_COL As Integer = 68
        Private Const SIM_CARD_NUMBER_COL As Integer = 69
        Private Const REGION_COL As Integer = 70
        Private Const MEMBERSHIP_TYPE_COL As Integer = 71
        Private Const CESS_OFFICE_COL As Integer = 72
        Private Const CESS_SALESREP_COL As Integer = 73
        Private Const BUSINESSLINE_COL As Integer = 74
        Private Const SALES_DEPARTMENT_COL As Integer = 75
        Private Const LINKED_CERT_NUMBER_COL As Integer = 76
        Private Const ADDITIONAL_INFO_COL As Integer = 77
        Private Const CREDITCARD_LAST_FOUR_DIGIT_COL As Integer = 78 'REQ-1169
        Private Const FINANCED_AMOUNT_COL As Integer = 79
        Private Const FINANCED_FREQUENCY_COL As Integer = 80
        Private Const FINANCED_INSTALLMENT_NUMBER_COL As Integer = 81
        Private Const FINANCED_INSTALLMENT_AMOUNT_COL As Integer = 82
        Private Const FINANCE_DATE_COL As Integer = 83
        Private Const DOWN_PAYMENT_COL As Integer = 84
        Private Const ADVANCE_PAYMENT_COL As Integer = 85
        Private Const UPGRADE_TERM_COL As Integer = 86
        Private Const BILLING_ACCOUNT_NUMBER_COL As Integer = 87
        'REQ-5619 - Start
        Private Const GENDER_COL As Integer = 88
        Private Const MARITAL_STATUS_COL As Integer = 89
        Private Const NATIONALITY_COL As Integer = 90
        Private Const BIRTH_DATE_COL As Integer = 91
        'REQ-5619 - End

        'REQ-5681 - Start
        Private Const PLACE_OF_BIRTH_COL As Integer = 92
        Private Const CUIT_CUIL_COL As Integer = 93
        Private Const PERSON_TYPE_COL As Integer = 94
        'REQ-5681 - End

        Private Const SERVICE_LINE_NUMBER_COL As Integer = 95

        Private Const NUM_OF_CONSECUTIVE_PAYMENTS_COL As Integer = 96
        Private Const LOAN_CODE_COL As Integer = 97
        Private Const PAYMENT_SHIFT_NUMBER_COL As Integer = 98
        Private Const DEALER_CURRENT_PLAN_CODE_COL As Integer = 99
        Private Const DEALER_SCHEDULED_PLAN_CODE_COL As Integer = 100
        Private Const DEALER_UPDATE_REASON_COL As Integer = 101
        Private Const UPGRADE_DATE_COL As Integer = 102
        Private Const VOUCHER_NUMBER_COL As Integer = 103
        Private Const RMA_COL As Integer = 104
        Private Const OUTSTANDING_BALANCE_AMT_COL As Integer = 105
        Private Const OUTSTANDING_BALANCE_DUE_DATE_COL As Integer = 106
        Private Const REFUND_AMOUNT_COL As Integer = 107
        Private Const DEVICE_TYPE_COL As Integer = 108
        Private Const APPLECARE_FEE As Integer = 109
        Private Const OCCUPATION_COL As Integer = 110

        Private Const ITEM_NUMBER_COL As Integer = 0
        Private Const ITEM_MANUFACTURER_COL As Integer = 1
        Private Const ITEM_MODEL_COL As Integer = 2
        Private Const ITEM_SERIAL_NUMBER_COL As Integer = 3
        Private Const ITEM_DESCRIPTION_COL As Integer = 4
        Private Const ITEM_PRICE_COL As Integer = 5
        Private Const ITEM_BUNDLE_VALUE_COL As Integer = 6
        Private Const ITEM_MAN_WARRANTY_COL As Integer = 7

        Private Const RECORD_TYPE_CONTROL_NAME As String = "moRecordTypeTextGrid"

        Private Const EMPTY As String = ""
        Private Const DEFAULT_PAGE_INDEX As Integer = 0

        ' Property Name
        Private Const RECORD_TYPE_PROPERTY As String = "RecordType"
        'Req 846
        Private Const SKU_NUMBER_PROPERTY As String = "SkuNumber"
        Private Const REJECT_CODE_PROPERTY As String = "RejectCode"
        Private Const REJECT_REASON_PROPERTY As String = "RejectReason"
        Private Const PRODUCT_CODE_PROPERTY As String = "ProductCode"
        Private Const PRODUCT_PRICE_PROPERTY As String = "ProductPrice"
        Private Const MAN_WARRANTY_PROPERTY As String = "ManWarranty"
        Private Const ITEM_CODE_PROPERTY As String = "ItemCode"
        Private Const ITEM_PROPERTY As String = "Item"
        Private Const EXT_WARRANTY_PROPERTY As String = "ExtWarranty"
        Private Const PRICE_POL_PROPERTY As String = "PricePol"
        Private Const SR_PROPERTY As String = "Sr"
        Private Const BRANCH_CODE_PROPERTY As String = "BranchCode"
        Private Const NEW_BRANCH_CODE_PROPERTY As String = "NewBranchCode"
        Private Const NUMBER_COMP_PROPERTY As String = "NumberComp"
        Private Const DATE_COMP_PROPERTY As String = "DateComp"
        Private Const CERTIFICATE_PROPERTY As String = "Certificate"
        Private Const IDENTIFICATION_NUMBER_PROPERTY As String = "IdentificationNumber"
        Private Const SALUTATION_PROPERTY As String = "Salutation"
        Private Const CUSTOMER_NAME_PROPERTY As String = "CustomerName"
        Private Const LANGUAGE_PREF_PROPERTY As String = "LanguagePref"
        Private Const ADDRESS1_PROPERTY As String = "Address1"
        Private Const CITY_PROPERTY As String = "City"
        Private Const ZIP_PROPERTY As String = "Zip"
        Private Const STATE_PROVINCE_PROPERTY As String = "StateProvince"
        Private Const HOME_PHONE_PROPERTY As String = "HomePhone"
        Private Const WORK_PHONE_PROPERTY As String = "WorkPhone"
        Private Const ISO_CODE_PROPERTY As String = "IsoCode"
        Private Const EXTWARR_SALEDATE_PROPERTY As String = "ExtwarrSaledate"
        Private Const TYPE_PAYMENT_PROPERTY As String = "TypePayment"
        Private Const CANCELLATION_CODE_PROPERTY As String = "CancellationCode"
        Private Const MANUFACTURER_PROPERTY As String = "Manufacturer"
        Private Const MODEL_PROPERTY As String = "Model"
        Private Const SERIAL_NUMBER_PROPERTY As String = "SerialNumber"
        Private Const IMEI_NUMBER_PROPERTY As String = "IMEINumber"
        Private Const NEW_PRODUCT_CODE_PROPERTY As String = "NewProductCode"
        Private Const DOCUMENT_TYPE_PROPERTY As String = "DocumentType"
        Private Const DOCUMENT_AGENCY_PROPERTY As String = "DocumentAgency"
        Private Const DOCUMENT_ISSUE_DATE_PROPERTY As String = "DocumentIssueDate"
        Private Const RG_NUMBER_PROPERTY As String = "RGNumber"
        Private Const ID_TYPE_PROPERTY As String = "IDType"
        Private Const BILLING_FREQUENCY_PROPERTY As String = "BillingFrequency"
        Private Const NUMBER_OF_INSTALLMENTS_PROPERTY As String = "NumberOfInstallments"
        Private Const INSTALLMENT_AMOUNT_PROPERTY As String = "InstallmentAmount"
        Private Const BANK_RTN_NUMBER_PROPERTY As String = "BankRtnNumber"
        Private Const BANK_ACCOUNT_NUMBER_PROPERTY As String = "BankAccountNumber"
        Private Const BANK_BRANCH_NUMBER_PROPERTY As String = "BankBranchNumber"
        Private Const BANK_ACCT_OWNER_NAME_PROPERTY As String = "BankAcctOwnerName"
        Private Const POST_PRE_PAID_PROPERTY As String = "PostPrePaid"
        Private Const DATE_PAID_FOR_PROPERTY As String = "DatePaidFor"
        Private Const MEMBERSHIP_NUMBER_PROPERTY As String = "MembershipNumber"
        Private Const SALES_TAX_PROPERTY As String = "SalesTax"
        Private Const ADDRESS2_PROPERTY As String = "Address2"
        Private Const ADDRESS3_PROPERTY As String = "Address3"
        Private Const EMAIL_PROPERTY As String = "Email"
        Private Const CUST_COUNTRY_PROPERTY As String = "CustCountry"
        Private Const COUNTRY_PURCH_PROPERTY As String = "CountryPurch"
        Private Const ORIGINAL_RETAIL_PRICE_PROPERTY As String = "OriginalRetailPrice"
        Private Const BILLING_PLAN_PROPERTY As String = "BillingPlan"
        Private Const BILLING_CYCLE_PROPERTY As String = "BillingCycle"
        Private Const SUBSCRIBER_STATUS_PROPERTY As String = "SubscriberStatus"
        Private Const SUSPENDED_REASON_PROPERTY As String = "SuspendedReason"
        Private Const MOBILE_TYPE_PROPERTY As String = "MobileType"
        Private Const FIRST_USE_DATE_PROPERTY As String = "FirstUseDate"
        Private Const LAST_USE_DATE_PROPERTY As String = "LastUseDate"
        Private Const SIM_CARD_NUMBER_PROPERTY As String = "SimCardNumber"
        Private Const REGION_PROPERTY As String = "Region"
        Private Const MEMBERSHIP_TYPE_PROPERTY As String = "MembershipType"
        Private Const CESS_OFFICE_PROPERTY As String = "CessOffice"
        Private Const CESS_SALESREP_PROPERTY As String = "CessSalesrep"
        Private Const BUSINESSLINE_PROPERTY As String = "Businessline"
        Private Const SALES_DEPARTMENT_PROPERTY As String = "SalesDepartment"
        Private Const LINKED_CERT_NUMBER_PROPERTY As String = "LinkedCertNumber"
        Private Const ADDITIONAL_INFO_PROPERTY As String = "AdditionalInfo"
        Private Const CREDITCARD_LASTFOURDIGIT_PROPERTY As String = "CreditCardLastFourDigit" 'REQ-1169

        Private Const FINANCED_AMOUNT As String = "Financed_Amount"
        Private Const FINANCED_FREQUENCY As String = "FinancedFrequency"
        Private Const FINANCED_INSTALLMENT_NUMBER As String = "FinancedInstallmentNumber"
        Private Const FINANCED_INSTALLMENT_AMOUNT As String = "FinancedInstallmentAmount"

        Private Const FINANCE_DATE_PROPERTY As String = "FinanceDate"
        Private Const DOWN_PAYMENT_PROPERTY As String = "DownPayment"
        Private Const ADVANCE_PAYMENT_PROPERTY As String = "AdvancePayment"
        Private Const UPGRADE_TERM_PROPERTY As String = "UpgradeTerm"
        Private Const BILLING_ACCOUNT_NUMBER_PROPERTY As String = "BillingAccountNumber"

        'REQ-5619 - Start
        Private Const GENDER_PROPERTY As String = "Gender"
        Private Const MARITAL_STATUS_PROPERTY As String = "Marital_Status"
        Private Const NATIONALITY_PROPERTY As String = "Nationality"
        Private Const BIRTH_DATE_PROPERTY As String = "Birth_Date"
        'REQ-5619 - End

        'REQ-5681 - Start
        Private Const PLACE_OF_BIRTH_PROPERTY As String = "Place_Of_Birth"
        Private Const CUIT_CUIL_PROPERTY As String = "CUIT_CUIL"
        Private Const PERSON_TYPE_PROPERTY As String = "Person_Type"

        'REQ-5681 - End
        Private Const NUM_OF_CONSECUTIVE_PAYMENTS_PROPERTY As String = "Num_Of_Consecutive_Payments"
        Private Const DEALER_CURRENT_PLAN_CODE_PROPERTY As String = "dealer_current_plan_code"
        Private Const DEALER_SCHEDULED_PLAN_CODE_PROPERTY As String = "dealer_scheduled_plan_code"
        Private Const DEALER_UPDATE_REASON_PROPERTY As String = "dealer_update_reason"

        Private Const SERVICE_LINE_NUMBER_PROPERTY As String = "ServiceLineNumber"
        Private Const LOAN_CODE_PROPERTY As String = "LoanCode"
        Private Const PAYMENT_SHIFT_NUMBER_PROPERTY As String = "PaymentShiftNumber"
        Private Const OCCUPATION_PROPERTY As String = "Occupation"

        Private Const BUNDLES_INFO As String = "BUNDLES"
        Private Const ITEM_NUMBER As String = "ItemNumber"
        Private Const ITEM_MANUFACTURER As String = "ItemManufacturer"
        Private Const ITEM_MODEL As String = "ItemModel"
        Private Const ITEM_SERIAL_NUMBER As String = "ItemSerialNumber"
        Private Const ITEM_DESCRIPTION As String = "ItemDescription"
        Private Const ITEM_PRICE As String = "ItemPrice"
        Private Const ITEM_BUNDLE_VALUE As String = "ItemBundleValue"
        Private Const ITEM_MAN_WARRANTY As String = "ItemManWarranty"

        Private Const UPGRADE_DATE_PROPERTY As String = "Upgrade_Date"
        Private Const VOUCHER_NUMBER_PROPERTY As String = "Voucher_Number"
        Private Const RMA_PROPERTY As String = "RMA"

        Private Const REFUND_AMONT_PROPERTY As String = "RefundAmount"
        Private Const DEVICE_TYPE_PROPERTY As String = "DeviceType"
        Private Const APPLECARE_FEE_PROPERTY As String = "AppleCareFee"
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

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
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
            Public SortExpression1 As String = Assurant.ElitaPlus.BusinessObjectsNew.User.UserSearchDV.COL_NETWORK_ID
            Public SortExpression As String
            Public SortDirection As String
            Public PageIndex As Integer = 0
            Public DealerfileProcessedId As Guid
            Public BundlesHashTable As Hashtable
            Public DealerReconWrkId As Guid
            Public ActionInProgress As DetailPageCommand = ElitaPlusPage.DetailPageCommand.Nothing_
            Public selectedPageSize As Integer = 30
            Public selectedPageIndex As Integer = DEFAULT_PAGE_INDEX
            Public sortBy As String
            Public comingFromBundlesScreen As String = ""
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
            Me.State.DealerfileProcessedId = CType(Me.CallingParameters, Guid)
        End Sub

        Private Sub SetQueryStringParam()
            Try
                If Not Request.QueryString("RECORDMODE") Is Nothing Then
                    Me.State.RecordMode = Request.QueryString("RECORDMODE")
                End If
                If Not Request.QueryString("PARENTFILE") Is Nothing Then
                    Me.State.ParentFile = Request.QueryString("PARENTFILE")
                End If
            Catch ex As Exception

            End Try
        End Sub

#End Region



#Region "Page Events"

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Me.MasterPage.MessageController.Clear_Hide()
            'Me.ErrController2.Clear_Hide()
            Me.SetStateProperties()

            'moDataGrid.VirtualItemCount = CType(Session("TotalRecords"), Double)
            PopulateCancellationReasonDropDown()
            If Not Page.IsPostBack Then
                Me.MasterPage.MessageController.Clear()
                Me.MasterPage.UsePageTabTitleInBreadCrum = False
                Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Interfaces")
                UpdateBreadCrum()
                SetQueryStringParam()
                PopulateRecordTypeDrop(moRecordTypeSearchDrop, True)
                Me.SortDirection = EMPTY
                Me.SetGridItemStyleColor(moDataGrid)
                Me.ShowMissingTranslations(MasterPage.MessageController)
                Me.State.PageIndex = 0
                Me.TranslateGridHeader(moDataGrid)
                Me.TranslateGridHeader(gvPop)
                Me.TranslateGridControls(moDataGrid)
                PopulateReadOnly()
                PopulateGrid()
                CheckFileTypeColums()
                'Set page size
                cboPageSize.SelectedValue = Me.State.selectedPageSize.ToString()
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
                        Select Case SaveBundles()
                            Case 1, 2
                                Exit Select
                        End Select
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

        Private Function CreateBoFromGrid(ByVal index As Integer) As DealerReconWrk
            Dim DealerReconWrkId As Guid
            Dim dealerReconWrkInfo As DealerReconWrk
            Dim sModifiedDate As String

            moDataGrid.SelectedIndex = index
            DealerReconWrkId = New Guid(CType(moDataGrid.Rows(index).FindControl("moDealerReconWrkIdLabel"), Label).Text)
            sModifiedDate = Me.GetGridText(moDataGrid, index, Me.MODIFIED_DATE_COL)
            dealerReconWrkInfo = New DealerReconWrk(DealerReconWrkId, sModifiedDate)
            Return dealerReconWrkInfo
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
            Dim dealerReconWrkInfo As DealerReconWrk
            Dim totItems As Integer = Me.moDataGrid.Rows.Count

            If totItems > 0 Then
                dealerReconWrkInfo = CreateBoFromGrid(0)
                BindBoPropertiesToGridHeaders(dealerReconWrkInfo)
                PopulateBOFromForm(dealerReconWrkInfo)
                dealerReconWrkInfo.Save()
            End If

            totItems = totItems - 1
            For index = 1 To totItems
                dealerReconWrkInfo = CreateBoFromGrid(index)
                BindBoPropertiesToGridHeaders(dealerReconWrkInfo)
                PopulateBOFromForm(dealerReconWrkInfo)
                dealerReconWrkInfo.Save()
            Next
            DealerReconWrk.UpdateHeaderCount(dealerReconWrkInfo.DealerfileProcessedId)
        End Sub

        Private Function SaveBundles() As Integer
            Dim dv As DataView = GetGridDataView()
            Dim ds As DataSet

            For i As Integer = 0 To moDataGrid.Rows.Count - 1
                Dim moDealerReconWrkIdLabel As String = CType(moDataGrid.Rows(i).FindControl("moDealerReconWrkIdLabel"), Label).Text.Trim
                Dim moDealerReconWrkIdLabelID As Guid = GetGuidFromString(moDealerReconWrkIdLabel)
                If Not Me.State.BundlesHashTable Is Nothing Then
                    If Me.State.BundlesHashTable.ContainsKey(moDealerReconWrkIdLabelID) Then
                        ds = CType(Me.State.BundlesHashTable.Item(moDealerReconWrkIdLabelID), DataSet)
                        If ds.HasChanges Then
                            For Each row As DataRow In ds.Tables(DealerReconWrkBundlesDAL.TABLE_NAME).Rows
                                If row.RowState = DataRowState.Unchanged Then
                                    row.Delete()
                                End If
                            Next
                            ds.AcceptChanges()
                            Dim dealerReconWrkBundlesInfo As New DealerReconWrkBundles()
                            Dim ret As Integer = dealerReconWrkBundlesInfo.SaveBundles(ds)
                            If ret = 0 Then
                                ds = GetBundlesDataSet()
                                Me.State.BundlesHashTable.Item(moDealerReconWrkIdLabelID) = ds
                                Return 2
                            Else
                                Me.ErrController.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.DB_WRITE_ERROR)
                            End If
                        End If
                    End If
                End If
            Next
            Return 1

        End Function
        Private Sub ApplyBundles()
            Try
                Dim dealerReconWrkBundlesInfo As DealerReconWrkBundles

                Dim ds As DataSet = GetBundlesDataSet()

                For i As Integer = 0 To ds.Tables(DealerReconWrkBundlesDAL.TABLE_NAME).Rows.Count - 1
                    With ds.Tables(DealerReconWrkBundlesDAL.TABLE_NAME).Rows
                        gvPop.SelectedIndex = i
                        Dim oDealerReconWrkBundles As New DealerReconWrkBundles(.Item(i), ds)
                        BindBoPropertiesToBundlesGridHeaders(oDealerReconWrkBundles)
                        PopulateBOFromBundlesForm(oDealerReconWrkBundles)
                        oDealerReconWrkBundles.Validate()

                    End With
                Next

                If Me.State.BundlesHashTable Is Nothing Then
                    Me.State.BundlesHashTable = New Hashtable
                End If
                If Me.State.BundlesHashTable.Contains(Me.State.DealerReconWrkId) Then
                    Me.State.BundlesHashTable.Item(Me.State.DealerReconWrkId) = ds
                Else
                    Me.State.BundlesHashTable.Add(Me.State.DealerReconWrkId, ds)
                End If

                Me.updPnlBundles.Update()
                'hide the modal popup
                Me.mdlPopup.Hide()

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
                ' Me.HandleErrors(ex, Me.ErrController2)
                Me.mdlPopup.Show()

            End Try
        End Sub

        Function IsDataGPageDirty() As Boolean
            Dim Result As String = Me.HiddenIsPageDirty.Value
            Return Result.Equals("YES")
        End Function

        Function IsDataGBundlesPageDirty() As Boolean
            Dim Result As String = Me.HiddenIsBundlesPageDirty.Value
            Return Result.Equals("YES")
        End Function

        Private Sub SetColumnState(ByVal column As Byte, ByVal state As Boolean)
            Me.moDataGrid.Columns(column).Visible = state
        End Sub

        Function IsCancellationsFileType(ByVal fileName As String) As Boolean
            Return fileName.Substring(4, 1).Equals(CANCELLATIONS_TYPE)
        End Function

        Private Sub CheckFileTypeColums()
            If IsCancellationsFileType(moFileNameText.Text) Then
                'SetColumnState(RECORD_TYPE_COL, False)
                SetColumnState(ITEM_CODE_COL, False)
                SetColumnState(ITEM_COL, False)
                SetColumnState(SR_COL, False)
                SetColumnState(BRANCH_CODE_COL, False)
                SetColumnState(IDENTIFICATION_NUMBER_COL, False)
                SetColumnState(ADDRESS1_COL, False)
                SetColumnState(CITY_COL, False)
                SetColumnState(HOME_PHONE_COL, False)
                SetColumnState(WORK_PHONE_COL, False)
                SetColumnState(MANUFACTURER_COL, False)
                SetColumnState(MODEL_COL, False)
                SetColumnState(SERIAL_NUMBER_COL, False)
                SetColumnState(IMEI_NUMBER_COL, False)
                SetColumnState(NEW_PRODUCT_CODE_COL, False)
                SetColumnState(DOCUMENT_TYPE_COL, False)
                SetColumnState(DOCUMENT_AGENCY_COL, False)
                SetColumnState(DOCUMENT_ISSUE_DATE_COL, False)
                SetColumnState(RG_NUMBER_COL, False)
                SetColumnState(ID_TYPE_COL, False)
                SetColumnState(NEW_BRANCH_CODE_COL, False)
                SetColumnState(BILLING_FREQUENCY_COL, False)
                SetColumnState(NUMBER_OF_INSTALLMENTS_COL, False)
                SetColumnState(INSTALLMENT_AMOUNT_COL, False)
                SetColumnState(BANK_RTN_NUMBER_COL, False)
                SetColumnState(BANK_ACCOUNT_NUMBER_COL, False)
                SetColumnState(BANK_ACCT_OWNER_NAME_COL, False)
                SetColumnState(BANK_BRANCH_NUMBER_COL, False)

                SetColumnState(SALES_TAX_COL, False)
                SetColumnState(EMAIL_COL, False)
                SetColumnState(ADDRESS2_COL, False)
                SetColumnState(CUST_COUNTRY_COL, False)
                SetColumnState(COUNTRY_PURCH_COL, False)

                SetColumnState(ADDRESS3_COL, False)
                SetColumnState(ORIGINAL_RETAIL_PRICE_COL, False)
                SetColumnState(BILLING_PLAN_COL, False)
                SetColumnState(BILLING_CYCLE_COL, False)
                SetColumnState(SUBSCRIBER_STATUS_COL, False)
                SetColumnState(SUSPENDED_REASON_COL, False)

                SetColumnState(MOBILE_TYPE_COL, False)
                SetColumnState(FIRST_USE_DATE_COL, False)
                SetColumnState(LAST_USE_DATE_COL, False)
                SetColumnState(SIM_CARD_NUMBER_COL, False)
                SetColumnState(REGION_COL, False)
                SetColumnState(MEMBERSHIP_TYPE_COL, False)
                SetColumnState(CESS_OFFICE_COL, False)
                SetColumnState(CESS_SALESREP_COL, False)
                SetColumnState(BUSINESSLINE_COL, False)
                SetColumnState(SALES_DEPARTMENT_COL, False)
                SetColumnState(LINKED_CERT_NUMBER_COL, False)
                SetColumnState(ADDITIONAL_INFO_COL, False)
                SetColumnState(CREDITCARD_LAST_FOUR_DIGIT_COL, False) 'REQ-1169
                SetColumnState(LOAN_CODE_COL, False)
                SetColumnState(PAYMENT_SHIFT_NUMBER_COL, False)
            Else
                SetColumnState(CANCELLATION_CODE_COL, False)
            End If
        End Sub

        Private Sub UpdateBreadCrum()

            Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("DEALER_FILE")
            Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("DEALER_FILE")

        End Sub

        Private Sub EnableDisableControl(ByVal oWebControl As WebControl, ByVal grid As GridView)
            Try
                Dim oDealerFile As DealerFileProcessed = New DealerFileProcessed(Me.State.DealerfileProcessedId)

                Select Case Me.State.RecordMode
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
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub EnableAGridControl(ByVal oControl As Control, ByVal enable As Boolean)
            Dim oWebControl As WebControl

            If TypeOf oControl Is Button Then Return
            If TypeOf oControl Is WebControl Then
                oWebControl = CType(oControl, WebControl)
                oWebControl.Enabled = enable
            End If
        End Sub

        Private Sub EnableAllGridControls(ByVal grid As GridView, ByVal enable As Boolean)
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
            With Me.State
                .srchRejectCode = Me.moRejectCodeText.Text.Trim
                .srchRejectReason = Me.moRejectReasonText.Text.Trim
                .srchRecordType = Me.moRecordTypeSearchDrop.SelectedItem.Text.Trim
            End With
        End Sub

        Sub PopulateRecordTypeDrop(ByVal recordTypeDrop As DropDownList, Optional ByVal AddNothingSelected As Boolean = False)
            Try
                Dim oLangId As Guid = Authentication.LangId
                Dim recordTypeList As DataView = LookupListNew.GetRecordTypeLookupList(oLangId)
                '  Me.BindListControlToDataView(recordTypeDrop, recordTypeList, , , AddNothingSelected)
                recordTypeDrop.Populate(CommonConfigManager.Current.ListManager.GetList("RECTYP", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                 {
                  .AddBlankItem = AddNothingSelected
                 })
            Catch ex As Exception
                ThePage.HandleErrors(ex, Me.ErrController)
            End Try
        End Sub

        Sub PopulateCancellationReasonDropDown()
            Try
                Dim oCompanyId As Guid = DealerReconWrk.CompanyId(Me.State.DealerfileProcessedId)
                TempDataView = LookupListNew.GetCancellationReasonDealerFileLookupList(oCompanyId)
                TempDataView.Sort = "DESCRIPTION"
                TempDataView.AddNew()
            Catch ex As Exception
                ThePage.HandleErrors(ex, Me.ErrController)
            End Try
        End Sub

        Private Sub PopulateReadOnly()
            Try
                Dim oDealerFile As DealerFileProcessed = New DealerFileProcessed(Me.State.DealerfileProcessedId)
                With oDealerFile
                    If Me.State.ParentFile = "N" Then
                        moDealerNameText.Text = .DealerNameLoad
                    Else
                        moDealerNameText.Text = EMPTY
                    End If
                    moFileNameText.Text = .Filename
                End With
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub PopulateGrid()
            Dim dv As DataView
            Dim recCount As Integer = 0
            Dim objdealerreconwrk As New DealerReconWrkDAL

            Try
                dv = GetDV()
                'dv.Sort = Me.State.sortBy
                If Not Me.SortDirection.Equals(EMPTY) Then
                    'dv.Sort = Me.SortDirection
                    HighLightSortColumn(moDataGrid, Me.SortDirection)
                End If

                Me.moDataGrid.PageSize = Me.State.selectedPageSize

                If Not Me.State.ParentFile = "Y" Then
                    searchtotal = objdealerreconwrk.CountSearch(Me.State.DealerfileProcessedId,
                                                                Me.State.RecordMode,
                                                                Me.State.srchRecordType,
                                                                Me.State.srchRejectCode,
                                                                Me.State.srchRejectReason,
                                                                Me.State.srchRecordType,
                                                                Me.State.srchRejectCode,
                                                                Me.State.srchRejectReason)
                    Me.lblRecordCount.Text = CType(searchtotal, String) & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                    moDataGrid.VirtualItemCount = CType(searchtotal, String)
                Else
                    searchtotal = objdealerreconwrk.ParentCount(Me.State.DealerfileProcessedId,
                                                                    Me.State.RecordMode,
                                                                    Me.State.srchRecordType,
                                                                    Me.State.srchRejectCode,
                                                                    Me.State.srchRejectReason,
                                                                    Me.State.srchRecordType,
                                                                    Me.State.srchRejectCode,
                                                                    Me.State.srchRejectReason)
                    Me.lblRecordCount.Text = CType(searchtotal, String) & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                    moDataGrid.VirtualItemCount = CType(searchtotal, String)
                End If


                'Check if Reconciliation flag is on for the dealer
                Me.moDataGrid.DataSource = dv.ToTable
                Me.moDataGrid.DataBind()
                ControlMgr.DisableAllGridControlsIfNotEditAuth(Me, moDataGrid)

                'Check if Reconciliation flag is on for the dealer
                Dim oDealerFile As DealerFileProcessed = New DealerFileProcessed(Me.State.DealerfileProcessedId)

                With oDealerFile
                    Dim objDealer As New Dealer(.DealerId)

                    If LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, objDealer.ReconRejRecTypeId) = Codes.YESNO_Y Then
                        If Me.State.RecordMode = RECORD_MODE__REMAINING_REJECTED Then
                            If oDealerFile.IsChildFile Then
                                EnableAllGridControls(Me.moDataGrid, False)
                            Else
                                EnableAllGridControls(Me.moDataGrid, True)
                            End If
                        Else
                            EnableAllGridControls(Me.moDataGrid, False)
                        End If
                    End If
                End With
                EnableDisableControl(Me.SaveButton_WRITE, Me.moDataGrid)
                EnableDisableControl(Me.btnUndo_WRITE, Me.moDataGrid)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

        Private Sub PopulateBundlesGrid(ByVal guid As Guid)

            Dim dv As DataView

            Try
                If Me.State.BundlesHashTable Is Nothing Then
                    dv = GetBundlesDV()
                Else
                    If Me.State.BundlesHashTable.Contains(guid) Then
                        dv = CType(Me.State.BundlesHashTable.Item(guid), DataSet).Tables(DealerReconWrkBundlesDAL.TABLE_NAME).DefaultView
                    Else
                        dv = GetBundlesDV()
                    End If
                End If

                Me.gvPop.DataSource = dv
                Me.gvPop.DataBind()
                ControlMgr.DisableAllGridControlsIfNotEditAuth(Me, gvPop)

                EnableDisableControl(Me.btnApply, Me.gvPop)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

        Private Function GetDV() As DataView

            Dim dv As DataView

            dv = GetGridDataView()
            'dv.Sort = moDataGrid.DataMember()

            Return (dv)

        End Function

        Private Function GetBundlesDV() As DataView
            Dim dv As DataView

            dv = GetBundlesDataSet().Tables(DealerReconWrkBundlesDAL.TABLE_NAME).DefaultView
            Return dv
        End Function

        Private Function GetGridDataView() As DataView

            With State
                Return (Assurant.ElitaPlus.BusinessObjectsNew.DealerReconWrk.LoadList(.DealerfileProcessedId,
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

        Private Function GetBundlesDataSet() As DataSet
            With State
                Return (Assurant.ElitaPlus.BusinessObjectsNew.DealerReconWrkBundles.LoadList(.DealerReconWrkId))
            End With
        End Function

        Private Sub PopulateBOItem(ByVal dealerReconWrkInfo As DealerReconWrk, ByVal oPropertyName As String, ByVal oCellPosition As Integer)
            Me.PopulateBOProperty(dealerReconWrkInfo, oPropertyName,
                                            CType(Me.GetSelectedGridControl(moDataGrid, oCellPosition), TextBox))
        End Sub

        Private Sub PopulateBODrop(ByVal dealerReconWrkInfo As DealerReconWrk, ByVal oPropertyName As String, ByVal oCellPosition As Integer)
            Me.PopulateBOProperty(dealerReconWrkInfo, oPropertyName,
                                CType(Me.GetSelectedGridControl(moDataGrid, oCellPosition), DropDownList), False)
        End Sub

        Private Sub PopulateBundlesBOItem(ByVal dealerReconWrkBundlesInfo As DealerReconWrkBundles, ByVal oPropertyName As String, ByVal oCellPosition As Integer)
            Me.PopulateBundlesBOProperty(dealerReconWrkBundlesInfo, oPropertyName,
                                            CType(Me.GetSelectedGridControl(gvPop, oCellPosition), TextBox))
        End Sub

        Private Sub PopulateBOFromForm(ByVal dealerReconWrkInfo As DealerReconWrk)

            PopulateBODrop(dealerReconWrkInfo, RECORD_TYPE_PROPERTY, RECORD_TYPE_COL)
            'PopulateBOItem(dealerReconWrkInfo, REJECT_CODE_PROPERTY, REJECT_CODE_COL)
            'PopulateBOItem(dealerReconWrkInfo, REJECT_REASON_PROPERTY, REJECT_REASON_COL)
            PopulateBOItem(dealerReconWrkInfo, PRODUCT_CODE_PROPERTY, PRODUCT_CODE_COL)
            PopulateBOItem(dealerReconWrkInfo, PRODUCT_PRICE_PROPERTY, PRODUCT_PRICE_COL)
            PopulateBOItem(dealerReconWrkInfo, MAN_WARRANTY_PROPERTY, MAN_WARRANTY_COL)
            PopulateBOItem(dealerReconWrkInfo, EXT_WARRANTY_PROPERTY, EXT_WARRANTY_COL)
            PopulateBOItem(dealerReconWrkInfo, PRICE_POL_PROPERTY, PRICE_POL_COL)
            PopulateBOItem(dealerReconWrkInfo, NUMBER_COMP_PROPERTY, NUMBER_COMP_COL)
            PopulateBOItem(dealerReconWrkInfo, DATE_COMP_PROPERTY, DATE_COMP_COL)
            PopulateBOItem(dealerReconWrkInfo, CERTIFICATE_PROPERTY, CERTIFICATE_COL)
            PopulateBOItem(dealerReconWrkInfo, SALUTATION_PROPERTY, SALUTATION_COL)
            PopulateBOItem(dealerReconWrkInfo, CUSTOMER_NAME_PROPERTY, CUSTOMER_NAME_COL)
            PopulateBOItem(dealerReconWrkInfo, LANGUAGE_PREF_PROPERTY, LANGUAGE_PREF_COL)
            PopulateBOItem(dealerReconWrkInfo, ZIP_PROPERTY, ZIP_COL)
            PopulateBOItem(dealerReconWrkInfo, STATE_PROVINCE_PROPERTY, STATE_PROVINCE_COL)
            PopulateBOItem(dealerReconWrkInfo, ISO_CODE_PROPERTY, ISO_CODE_COL)
            PopulateBOItem(dealerReconWrkInfo, EXTWARR_SALEDATE_PROPERTY, EXTWARR_SALEDATE_COL)
            PopulateBOItem(dealerReconWrkInfo, TYPE_PAYMENT_PROPERTY, TYPE_PAYMENT_COL)
            PopulateBOItem(dealerReconWrkInfo, SERVICE_LINE_NUMBER_PROPERTY, SERVICE_LINE_NUMBER_COL)

            If Not IsCancellationsFileType(moFileNameText.Text) Then
                '  PopulateBOItem(dealerReconWrkInfo, RECORD_TYPE_PROPERTY, RECORD_TYPE_COL)
                'PopulateBODrop(dealerReconWrkInfo, RECORD_TYPE_PROPERTY, RECORD_TYPE_COL)

                PopulateBOItem(dealerReconWrkInfo, ITEM_CODE_PROPERTY, ITEM_CODE_COL)
                PopulateBOItem(dealerReconWrkInfo, ITEM_PROPERTY, ITEM_COL)
                'Req 846
                PopulateBOItem(dealerReconWrkInfo, SKU_NUMBER_PROPERTY, SKU_Number)
                PopulateBOItem(dealerReconWrkInfo, SR_PROPERTY, SR_COL)
                PopulateBOItem(dealerReconWrkInfo, BRANCH_CODE_PROPERTY, BRANCH_CODE_COL)
                PopulateBOItem(dealerReconWrkInfo, IDENTIFICATION_NUMBER_PROPERTY, IDENTIFICATION_NUMBER_COL)
                PopulateBOItem(dealerReconWrkInfo, ADDRESS1_PROPERTY, ADDRESS1_COL)
                PopulateBOItem(dealerReconWrkInfo, CITY_PROPERTY, CITY_COL)
                PopulateBOItem(dealerReconWrkInfo, HOME_PHONE_PROPERTY, HOME_PHONE_COL)
                PopulateBOItem(dealerReconWrkInfo, WORK_PHONE_PROPERTY, WORK_PHONE_COL)
                PopulateBOItem(dealerReconWrkInfo, MANUFACTURER_PROPERTY, MANUFACTURER_COL)
                PopulateBOItem(dealerReconWrkInfo, MODEL_PROPERTY, MODEL_COL)
                PopulateBOItem(dealerReconWrkInfo, SERIAL_NUMBER_PROPERTY, SERIAL_NUMBER_COL)
                PopulateBOItem(dealerReconWrkInfo, IMEI_NUMBER_PROPERTY, IMEI_NUMBER_COL)
                PopulateBOItem(dealerReconWrkInfo, NEW_PRODUCT_CODE_PROPERTY, NEW_PRODUCT_CODE_COL)
                PopulateBOItem(dealerReconWrkInfo, DOCUMENT_TYPE_PROPERTY, DOCUMENT_TYPE_COL)
                PopulateBOItem(dealerReconWrkInfo, DOCUMENT_AGENCY_PROPERTY, DOCUMENT_AGENCY_COL)
                PopulateBOItem(dealerReconWrkInfo, DOCUMENT_ISSUE_DATE_PROPERTY, DOCUMENT_ISSUE_DATE_COL)
                PopulateBOItem(dealerReconWrkInfo, RG_NUMBER_PROPERTY, RG_NUMBER_COL)
                PopulateBOItem(dealerReconWrkInfo, ID_TYPE_PROPERTY, ID_TYPE_COL)

                PopulateBOItem(dealerReconWrkInfo, SALES_TAX_PROPERTY, SALES_TAX_COL)
                PopulateBOItem(dealerReconWrkInfo, EMAIL_PROPERTY, EMAIL_COL)
                PopulateBOItem(dealerReconWrkInfo, ADDRESS2_PROPERTY, ADDRESS2_COL)
                PopulateBOItem(dealerReconWrkInfo, ADDRESS3_PROPERTY, ADDRESS3_COL)
                PopulateBOItem(dealerReconWrkInfo, CUST_COUNTRY_PROPERTY, CUST_COUNTRY_COL)
                PopulateBOItem(dealerReconWrkInfo, COUNTRY_PURCH_PROPERTY, COUNTRY_PURCH_COL)
                PopulateBOItem(dealerReconWrkInfo, NEW_BRANCH_CODE_PROPERTY, NEW_BRANCH_CODE_COL)
                PopulateBOItem(dealerReconWrkInfo, BILLING_FREQUENCY_PROPERTY, BILLING_FREQUENCY_COL)
                PopulateBOItem(dealerReconWrkInfo, NUMBER_OF_INSTALLMENTS_PROPERTY, NUMBER_OF_INSTALLMENTS_COL)
                PopulateBOItem(dealerReconWrkInfo, INSTALLMENT_AMOUNT_PROPERTY, INSTALLMENT_AMOUNT_COL)
                PopulateBOItem(dealerReconWrkInfo, BANK_RTN_NUMBER_PROPERTY, BANK_RTN_NUMBER_COL)
                PopulateBOItem(dealerReconWrkInfo, BANK_ACCOUNT_NUMBER_PROPERTY, BANK_ACCOUNT_NUMBER_COL)
                PopulateBOItem(dealerReconWrkInfo, BANK_ACCT_OWNER_NAME_PROPERTY, BANK_ACCT_OWNER_NAME_COL)
                PopulateBOItem(dealerReconWrkInfo, BANK_BRANCH_NUMBER_PROPERTY, BANK_BRANCH_NUMBER_COL)
                PopulateBOItem(dealerReconWrkInfo, POST_PRE_PAID_PROPERTY, POST_PRE_PAID_COL)
                PopulateBOItem(dealerReconWrkInfo, DATE_PAID_FOR_PROPERTY, DATE_PAID_FOR_COL)
                PopulateBOItem(dealerReconWrkInfo, MEMBERSHIP_NUMBER_PROPERTY, MEMBERSHIP_NUMBER_COL)
                PopulateBOItem(dealerReconWrkInfo, ORIGINAL_RETAIL_PRICE_PROPERTY, ORIGINAL_RETAIL_PRICE_COL)
                PopulateBOItem(dealerReconWrkInfo, BILLING_PLAN_PROPERTY, BILLING_PLAN_COL)
                PopulateBOItem(dealerReconWrkInfo, BILLING_CYCLE_PROPERTY, BILLING_CYCLE_COL)
                PopulateBOItem(dealerReconWrkInfo, SUBSCRIBER_STATUS_PROPERTY, SUBSCRIBER_STATUS_COL)
                PopulateBOItem(dealerReconWrkInfo, SUSPENDED_REASON_PROPERTY, SUSPENDED_REASON_COL)
                PopulateBOItem(dealerReconWrkInfo, MOBILE_TYPE_PROPERTY, MOBILE_TYPE_COL)
                PopulateBOItem(dealerReconWrkInfo, FIRST_USE_DATE_PROPERTY, FIRST_USE_DATE_COL)
                PopulateBOItem(dealerReconWrkInfo, LAST_USE_DATE_PROPERTY, LAST_USE_DATE_COL)
                PopulateBOItem(dealerReconWrkInfo, SIM_CARD_NUMBER_PROPERTY, SIM_CARD_NUMBER_COL)
                PopulateBOItem(dealerReconWrkInfo, REGION_PROPERTY, REGION_COL)
                PopulateBOItem(dealerReconWrkInfo, MEMBERSHIP_TYPE_PROPERTY, MEMBERSHIP_TYPE_COL)
                PopulateBOItem(dealerReconWrkInfo, CESS_OFFICE_PROPERTY, CESS_OFFICE_COL)
                PopulateBOItem(dealerReconWrkInfo, CESS_SALESREP_PROPERTY, CESS_SALESREP_COL)
                PopulateBOItem(dealerReconWrkInfo, BUSINESSLINE_PROPERTY, BUSINESSLINE_COL)
                PopulateBOItem(dealerReconWrkInfo, SALES_DEPARTMENT_PROPERTY, SALES_DEPARTMENT_COL)
                PopulateBOItem(dealerReconWrkInfo, LINKED_CERT_NUMBER_PROPERTY, LINKED_CERT_NUMBER_COL)
                PopulateBOItem(dealerReconWrkInfo, ADDITIONAL_INFO_PROPERTY, ADDITIONAL_INFO_COL)
                PopulateBOItem(dealerReconWrkInfo, CREDITCARD_LASTFOURDIGIT_PROPERTY, CREDITCARD_LAST_FOUR_DIGIT_COL) 'REQ-1169
                PopulateBOItem(dealerReconWrkInfo, FINANCED_AMOUNT, FINANCED_AMOUNT_COL)
                PopulateBOItem(dealerReconWrkInfo, FINANCED_FREQUENCY, FINANCED_FREQUENCY_COL)
                PopulateBOItem(dealerReconWrkInfo, FINANCED_INSTALLMENT_NUMBER, FINANCED_INSTALLMENT_NUMBER_COL)
                PopulateBOItem(dealerReconWrkInfo, FINANCED_INSTALLMENT_AMOUNT, FINANCED_INSTALLMENT_AMOUNT_COL)
                PopulateBOItem(dealerReconWrkInfo, GENDER_PROPERTY, GENDER_COL)
                PopulateBOItem(dealerReconWrkInfo, MARITAL_STATUS_PROPERTY, MARITAL_STATUS_COL)
                PopulateBOItem(dealerReconWrkInfo, NATIONALITY_PROPERTY, NATIONALITY_COL)
                PopulateBOItem(dealerReconWrkInfo, BIRTH_DATE_PROPERTY, BIRTH_DATE_COL)
                PopulateBOItem(dealerReconWrkInfo, PLACE_OF_BIRTH_PROPERTY, PLACE_OF_BIRTH_COL)
                PopulateBOItem(dealerReconWrkInfo, CUIT_CUIL_PROPERTY, CUIT_CUIL_COL)
                PopulateBOItem(dealerReconWrkInfo, PERSON_TYPE_PROPERTY, PERSON_TYPE_COL)

                Dim strNumOfConsecutivePayments As String = CType(Me.GetSelectedGridControl(moDataGrid, NUM_OF_CONSECUTIVE_PAYMENTS_COL), TextBox).Text
                If strNumOfConsecutivePayments.Equals(String.Empty) Then
                    dealerReconWrkInfo.Num_Of_Consecutive_Payments = Nothing
                Else
                    PopulateBOItem(dealerReconWrkInfo, NUM_OF_CONSECUTIVE_PAYMENTS_PROPERTY, NUM_OF_CONSECUTIVE_PAYMENTS_COL)
                End If

                PopulateBOItem(dealerReconWrkInfo, DEALER_CURRENT_PLAN_CODE_PROPERTY, DEALER_CURRENT_PLAN_CODE_COL)
                PopulateBOItem(dealerReconWrkInfo, DEALER_SCHEDULED_PLAN_CODE_PROPERTY, DEALER_SCHEDULED_PLAN_CODE_COL)
                PopulateBOItem(dealerReconWrkInfo, DEALER_UPDATE_REASON_PROPERTY, DEALER_UPDATE_REASON_COL)

                PopulateBOItem(dealerReconWrkInfo, FINANCE_DATE_PROPERTY, FINANCE_DATE_COL)
                PopulateBOItem(dealerReconWrkInfo, DOWN_PAYMENT_PROPERTY, DOWN_PAYMENT_COL)
                PopulateBOItem(dealerReconWrkInfo, ADVANCE_PAYMENT_PROPERTY, ADVANCE_PAYMENT_COL)
                PopulateBOItem(dealerReconWrkInfo, UPGRADE_TERM_PROPERTY, UPGRADE_TERM_COL)
                PopulateBOItem(dealerReconWrkInfo, BILLING_ACCOUNT_NUMBER_PROPERTY, BILLING_ACCOUNT_NUMBER_COL)
                PopulateBOItem(dealerReconWrkInfo, LOAN_CODE_PROPERTY, LOAN_CODE_COL)
                PopulateBOItem(dealerReconWrkInfo, PAYMENT_SHIFT_NUMBER_PROPERTY, PAYMENT_SHIFT_NUMBER_COL)
                PopulateBOItem(dealerReconWrkInfo, REFUND_AMONT_PROPERTY, REFUND_AMOUNT_COL)
                PopulateBOItem(dealerReconWrkInfo, DEVICE_TYPE_PROPERTY, DEVICE_TYPE_COL)
                PopulateBOItem(dealerReconWrkInfo, APPLECARE_FEE_PROPERTY, APPLECARE_FEE)
                PopulateBOItem(dealerReconWrkInfo, OCCUPATION_PROPERTY, OCCUPATION_COL)
            Else

                'SetColumnState(CANCELLATION_CODE_COL, False)
                PopulateBOItem(dealerReconWrkInfo, CANCELLATION_CODE_PROPERTY, CANCELLATION_CODE_COL)
            End If
            If Me.ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub

        Private Sub PopulateBOFromBundlesForm(ByVal dealerReconWrkBundlesInfo As DealerReconWrkBundles)
            PopulateBundlesBOItem(dealerReconWrkBundlesInfo, ITEM_NUMBER, ITEM_NUMBER_COL)
            PopulateBundlesBOItem(dealerReconWrkBundlesInfo, ITEM_MANUFACTURER, ITEM_MANUFACTURER_COL)
            PopulateBundlesBOItem(dealerReconWrkBundlesInfo, ITEM_MODEL, ITEM_MODEL_COL)
            PopulateBundlesBOItem(dealerReconWrkBundlesInfo, ITEM_SERIAL_NUMBER, ITEM_SERIAL_NUMBER_COL)
            PopulateBundlesBOItem(dealerReconWrkBundlesInfo, ITEM_DESCRIPTION, ITEM_DESCRIPTION_COL)
            PopulateBundlesBOItem(dealerReconWrkBundlesInfo, ITEM_PRICE, ITEM_PRICE_COL)
            PopulateBundlesBOItem(dealerReconWrkBundlesInfo, ITEM_BUNDLE_VALUE, ITEM_BUNDLE_VALUE_COL)
            PopulateBundlesBOItem(dealerReconWrkBundlesInfo, ITEM_MAN_WARRANTY, ITEM_MAN_WARRANTY_COL)

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
                Me.State.selectedPageIndex = e.NewPageIndex
                If IsDataGPageDirty() Then
                    DisplayMessage(Message.MSG_PAGE_SAVE_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, Me.HiddenSavePagePromptResponse)
                Else
                    Me.moDataGrid.PageIndex = e.NewPageIndex
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
                    Me.State.selectedPageIndex = 0
                    Me.State.selectedPageSize = CType(cboPageSize.SelectedValue, Integer)
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
            Dim oLabel As Label

            'translate the reject message
            Dim strMsg As String, dr As DataRow, intParamCnt As Integer = 0, strParamList As String = String.Empty
            If Not dvRow Is Nothing Then
                dr = dvRow.Row
                strMsg = dr(DealerReconWrkDAL.COL_TRANSLATED_MSG).ToString.Trim
                If strMsg <> String.Empty Then
                    If Not dr(DealerReconWrkDAL.COL_MSG_PARAMETER_COUNT) Is DBNull.Value Then
                        intParamCnt = CType(dr(DealerReconWrkDAL.COL_MSG_PARAMETER_COUNT), Integer)
                    End If
                    If intParamCnt > 0 Then
                        If Not dr(DealerReconWrkDAL.COL_REJECT_MSG_PARMS) Is DBNull.Value Then
                            strParamList = dr(DealerReconWrkDAL.COL_REJECT_MSG_PARMS).ToString.Trim
                        End If
                        strMsg = TranslationBase.TranslateParameterizedMsg(strMsg, intParamCnt, strParamList).Trim
                    End If
                    If strMsg <> "" Then dr(DealerReconWrkDAL.COL_REJECT_REASON) = strMsg
                    dr.AcceptChanges()
                End If
            End If


            'Freeze Headers - Start
            If (itemType = ListItemType.Header) Then
                'moDataGrid.HeaderStyle.CssClass = "locked"
                'moDataGrid.HeaderRow.Cells(1).CssClass = "locked"
                'moDataGrid.HeaderRow.Cells(2).CssClass = "locked"
                'moDataGrid.HeaderRow.Cells(3).CssClass = "locked"
            End If
            'Freeze Headers - Start

            If (itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem) AndAlso e.Row.RowIndex <> -1 Then
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
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_CANCELLATION_CODE))

                    Dim ddl As DropDownList = CType(e.Row.FindControl("ddl1"), DropDownList)
                    ddl.SelectedIndex = ddl.Items.IndexOf(ddl.Items.FindByValue(oTextBox.Text))
                    AssignDropDownToCtr(ddl, oTextBox)
                    Me.PopulateControlFromBOProperty(.FindControl("moDealerReconWrkIdLabel"), dvRow(DealerReconWrk.COL_NAME_DEALER_RECON_WRK_ID))
                    Dim oDrop As DropDownList = CType(e.Row.FindControl("moRecordTypeDrop"), DropDownList)
                    oDrop.Attributes.Add("onchange", "setDirty()")
                    PopulateRecordTypeDrop(oDrop)
                    Dim oValue As String = CType(dvRow(DealerReconWrk.COL_NAME_RECORD_TYPE), String)
                    Me.SetSelectedItemByText(oDrop, oValue)
                    oTextBox = CType(.FindControl("moRejectCode"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_REJECT_CODE))
                    oTextBox = CType(.FindControl("RejectReasonTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_REJECT_REASON))

                    oTextBox = CType(.Cells(DEALER_COL).FindControl("moDealerTextGrid"), TextBox)
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_DEALER))

                    oTextBox = CType(.FindControl("moProductCodeTextGrid"), TextBox)
                    If dvRow(DealerReconWrk.COL_NAME_IS_RECON_RECORD_PARENT).ToString.ToUpper = "T" Then
                        oTextBox.Enabled = False
                    Else
                        oTextBox.Attributes.Add("onchange", "setDirty()")
                    End If
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_PRODUCT_CODE))

                    oTextBox = CType(.FindControl("moProductPriceTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_PRODUCT_PRICE))
                    oTextBox = CType(.FindControl("moManWarrantyTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_MAN_WARRANTY))
                    oTextBox = CType(.FindControl("moExtWarrantyTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_EXT_WARRANTY))
                    oTextBox = CType(.FindControl("moPricePolTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_PRICE_POL))
                    oTextBox = CType(.FindControl("moNumberCompTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_NUMBER_COMP))
                    oTextBox = CType(.FindControl("moDateCompTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Dim oDateCompImage As ImageButton = CType(.FindControl("moDateCompImageGrid"), ImageButton)
                    If (Not oDateCompImage Is Nothing) Then
                        Me.AddCalendar(oDateCompImage, oTextBox)
                    End If
                    If (Not String.IsNullOrEmpty(dvRow(DealerReconWrk.COL_NAME_DATE_COMP).ToString())) Then
                        Me.PopulateControlFromBOProperty(oTextBox, GetDateFormattedString(dvRow(DealerReconWrk.COL_NAME_DATE_COMP)))
                    End If
                    oTextBox = CType(.FindControl("moCertificateTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_CERTIFICATE))
                    oTextBox = CType(.FindControl("moSalutationTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_SALUTATION))
                    oTextBox = CType(.FindControl("moCustNameTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_CUSTOMER_NAME))
                    oTextBox = CType(.FindControl("moLangPrefTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_LANGUAGE_PREF))
                    oTextBox = CType(.FindControl("moZipTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_ZIP))
                    oTextBox = CType(.FindControl("moStateProvTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_STATE_PROVINCE))
                    'oTextBox = CType(e.Row.Cells(CURRENCY_COL).FindControl("moCurrencyTextGrid"), TextBox)
                    oTextBox = CType(.FindControl("moCurrencyTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    'Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_CURRENCY))
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_ISO_CODE))
                    oTextBox = CType(.FindControl("moExtWarrSaleDateTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Dim oExtWarrSaleDateImage As ImageButton = CType(.FindControl("moExtWarrSaleDateImageGrid"), ImageButton)
                    If (Not oExtWarrSaleDateImage Is Nothing) Then
                        Me.AddCalendar(oExtWarrSaleDateImage, oTextBox)
                    End If
                    If (Not String.IsNullOrEmpty(dvRow(DealerReconWrk.COL_NAME_EXTWARR_SALEDATE).ToString())) Then
                        Me.PopulateControlFromBOProperty(oTextBox, GetDateFormattedString(dvRow(DealerReconWrk.COL_NAME_EXTWARR_SALEDATE)))
                    End If
                    oTextBox = CType(.FindControl("moTypePaymentTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_TYPE_PAYMENT))

                    oTextBox = CType(.FindControl("moServiceLineNumberTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_SERVICE_LINE_NUMBER))



                    If Not IsCancellationsFileType(moFileNameText.Text) Then
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
                        Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_ITEM_CODE))
                        oTextBox = CType(.FindControl("moItemTextGrid"), TextBox)
                        oTextBox.Attributes.Add("onchange", "setDirty()")
                        Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_ITEM))
                        'Req 846
                        oTextBox = CType(.FindControl("moSkuNumberTextGrid"), TextBox)
                        oTextBox.Attributes.Add("onchange", "setDirty()")
                        Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_SKU_NUMBER))
                        oTextBox = CType(.FindControl("moSrTextGrid"), TextBox)
                        oTextBox.Attributes.Add("onchange", "setDirty()")
                        Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_SR))
                        oTextBox = CType(.FindControl("moBranchCodeTextGrid"), TextBox)
                        oTextBox.Attributes.Add("onchange", "setDirty()")
                        Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_BRANCH_CODE))
                        oTextBox = CType(.FindControl("moIdNumTextGrid"), TextBox)
                        oTextBox.Attributes.Add("onchange", "setDirty()")
                        Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_IDENTIFICATION_NUMBER))
                        oTextBox = CType(.FindControl("moAddressTextGrid"), TextBox)
                        oTextBox.Attributes.Add("onchange", "setDirty()")
                        Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_ADDRESS1))
                        oTextBox = CType(.FindControl("moCityTextGrid"), TextBox)
                        Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_CITY))
                        oTextBox = CType(.FindControl("moHomePhoneTextGrid"), TextBox)
                        oTextBox.Attributes.Add("onchange", "setDirty()")
                        Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_HOME_PHONE))
                        'SetColumnState(WORK_PHONE_COL, False)
                        oTextBox = CType(.FindControl("moWorkPhoneTextGrid"), TextBox)
                        oTextBox.Attributes.Add("onchange", "setDirty()")
                        Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_WORK_PHONE))
                        'SetColumnState(MANUFACTURER_COL, False)
                        oTextBox = CType(.FindControl("moManufacturerTextGrid"), TextBox)
                        oTextBox.Attributes.Add("onchange", "setDirty()")
                        Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_MANUFACTURER))
                        'SetColumnState(MODEL_COL, False)
                        oTextBox = CType(.FindControl("moModelTextGrid"), TextBox)
                        oTextBox.Attributes.Add("onchange", "setDirty()")
                        Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_MODEL))
                        'SetColumnState(SERIAL_NUMBER_COL, False)
                        oTextBox = CType(.FindControl("moSerialNumTextGrid"), TextBox)
                        oTextBox.Attributes.Add("onchange", "setDirty()")
                        Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_SERIAL_NUMBER))
                        oTextBox = CType(.FindControl("moIMEINumTextGrid"), TextBox)
                        oTextBox.Attributes.Add("onchange", "setDirty()")
                        Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_IMEI_NUMBER))
                        'SetColumnState(NEW_PRODUCT_CODE_COL, False)
                        oTextBox = CType(.FindControl("moNewProdCodeTextGrid"), TextBox)
                        If dvRow(DealerReconWrk.COL_NAME_IS_RECON_RECORD_PARENT).ToString.ToUpper = "T" Then
                            oTextBox.Enabled = False
                        Else
                            oTextBox.Attributes.Add("onchange", "setDirty()")
                        End If
                        Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_NEW_PRODUCT_CODE))
                        oTextBox = CType(.FindControl("moDocumentTypeTextGrid"), TextBox)
                        oTextBox.Attributes.Add("onchange", "setDirty()")
                        Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_DOCUMENT_TYPE))
                        oTextBox = CType(.FindControl("moDocumentAgencyTextGrid"), TextBox)
                        oTextBox.Attributes.Add("onchange", "setDirty()")
                        Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_DOCUMENT_AGENCY))
                        oTextBox = CType(.FindControl("moDocumentIssueDateTextGrid"), TextBox)
                        oTextBox.Attributes.Add("onchange", "setDirty()")
                        Dim oDocumentIssueDateImage As ImageButton = CType(.FindControl("moDocumentIssueDateImageGrid"), ImageButton)
                        If (Not oDocumentIssueDateImage Is Nothing) Then
                            Me.AddCalendar(oDocumentIssueDateImage, oTextBox)
                        End If
                        If (Not String.IsNullOrEmpty(dvRow(DealerReconWrk.COL_NAME_DOCUMENT_ISSUE_DATE).ToString())) Then
                            Me.PopulateControlFromBOProperty(oTextBox, GetDateFormattedString(dvRow(DealerReconWrk.COL_NAME_DOCUMENT_ISSUE_DATE)))
                        End If
                        oTextBox = CType(.FindControl("moRGNumberTextGrid"), TextBox)
                        oTextBox.Attributes.Add("onchange", "setDirty()")
                        Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_RG_NUMBER))
                        oTextBox = CType(.FindControl("moIDTypeTextGrid"), TextBox)
                        oTextBox.Attributes.Add("onchange", "setDirty()")
                        Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_ID_TYPE))

                        oTextBox = CType(.FindControl("moSalesTaxTextGrid"), TextBox)
                        oTextBox.Attributes.Add("onchange", "setDirty()")
                        Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_SALES_TAX))

                        oTextBox = CType(.FindControl("moEmailTextGrid"), TextBox)
                        oTextBox.Attributes.Add("onchange", "setDirty()")
                        Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_EMAIL))

                        oTextBox = CType(.FindControl("moAddress2TextGrid"), TextBox)
                        oTextBox.Attributes.Add("onchange", "setDirty()")
                        Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_ADDRESS2))

                        oTextBox = CType(.FindControl("moAddress3TextGrid"), TextBox)
                        oTextBox.Attributes.Add("onchange", "setDirty()")
                        Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_ADDRESS3))

                        oTextBox = CType(.FindControl("moCustCountryTextGrid"), TextBox)
                        oTextBox.Attributes.Add("onchange", "setDirty()")
                        Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_CUST_COUNTRY))

                        oTextBox = CType(.FindControl("moCountryPurchTextGrid"), TextBox)
                        oTextBox.Attributes.Add("onchange", "setDirty()")
                        Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_COUNTRY_PURCH))

                        oTextBox = CType(.FindControl("moNewBranchCodeTextGrid"), TextBox)
                        oTextBox.Attributes.Add("onchange", "setDirty()")
                        Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_NEW_BRANCH_CODE))

                        oTextBox = CType(.FindControl("moBillFreqTextGrid"), TextBox)
                        oTextBox.Attributes.Add("onchange", "setDirty()")
                        Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_BILLING_FREQUENCY))

                        oTextBox = CType(.FindControl("moTextNoOfInstGrid"), TextBox)
                        oTextBox.Attributes.Add("onchange", "setDirty()")
                        Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_NUMBER_OF_INSTALLMENTS))

                        oTextBox = CType(.FindControl("moInstAmtTextGrid"), TextBox)
                        oTextBox.Attributes.Add("onchange", "setDirty()")
                        Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_INSTALLMENT_AMOUNT))

                        oTextBox = CType(.FindControl("moBnkRtnNoTextGrid"), TextBox)
                        oTextBox.Attributes.Add("onchange", "setDirty()")
                        Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_BANK_RTN_NUMBER))

                        oTextBox = CType(.FindControl("moBnkAcctNoTextGrid"), TextBox)
                        oTextBox.Attributes.Add("onchange", "setDirty()")
                        Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_BANK_ACCOUNT_NUMBER))

                        oTextBox = CType(.FindControl("moBnkAcctOwnerNameTextGrid"), TextBox)
                        oTextBox.Attributes.Add("onchange", "setDirty()")
                        Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_BANK_ACCT_OWNER_NAME))

                        oTextBox = CType(.FindControl("moBnkBranchNumberTextGrid"), TextBox)
                        oTextBox.Attributes.Add("onchange", "setDirty()")
                        Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_BANK_BRANCH_NUMBER))

                        oTextBox = CType(.FindControl("moPostPrePaidTextGrid"), TextBox)
                        oTextBox.Attributes.Add("onchange", "setDirty()")
                        Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_POST_PRE_PAID))

                        oTextBox = CType(.FindControl("moDatePaidForTextGrid"), TextBox)
                        oTextBox.Attributes.Add("onchange", "setDirty()")
                        Dim oDatePaidForImage As ImageButton = CType(.FindControl("moDatePaidForImageGrid"), ImageButton)
                        If (Not oDateCompImage Is Nothing) Then
                            Me.AddCalendar(oDatePaidForImage, oTextBox)
                        End If
                        If (Not String.IsNullOrEmpty(dvRow(DealerReconWrk.COL_NAME_DATE_PAID_FOR).ToString())) Then
                            Me.PopulateControlFromBOProperty(oTextBox, GetDateFormattedString(dvRow(DealerReconWrk.COL_NAME_DATE_PAID_FOR)))
                        End If
                        oTextBox = CType(.FindControl("moMembershipNumTextGrid"), TextBox)
                        oTextBox.Attributes.Add("onchange", "setDirty()")
                        Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_MEMBERSHIP_NUMBER))

                        oTextBox = CType(.FindControl("moOriginalRetailPriceTextGrid"), TextBox)
                        oTextBox.Attributes.Add("onchange", "setDirty()")
                        Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_ORIGINAL_RETAIL_PRICE))

                        oTextBox = CType(.FindControl("moBillingPlanTextGrid"), TextBox)
                        oTextBox.Attributes.Add("onchange", "setDirty()")
                        Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_BILLING_PLAN))

                        oTextBox = CType(.FindControl("moBillingCycleTextGrid"), TextBox)
                        oTextBox.Attributes.Add("onchange", "setDirty()")
                        Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_BILLING_CYCLE))

                        oTextBox = CType(.FindControl("moSubscriberStatusTextGrid"), TextBox)
                        oTextBox.Attributes.Add("onchange", "setDirty()")
                        Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_SUBSCRIBER_STATUS))

                        oTextBox = CType(.FindControl("moSuspendedReasonTextGrid"), TextBox)
                        oTextBox.Attributes.Add("onchange", "setDirty()")
                        Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_SUSPENDED_REASON))

                        oTextBox = CType(.FindControl("moMobileTypeTextGrid"), TextBox)
                        oTextBox.Attributes.Add("onchange", "setDirty()")
                        Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_MOBILE_TYPE))

                        oTextBox = CType(.FindControl("moFirstUseDateTextGrid"), TextBox)
                        oTextBox.Attributes.Add("onchange", "setDirty()")
                        Dim oFirstUseDateImage As ImageButton = CType(.FindControl("moFirstUseDateImageGrid"), ImageButton)
                        If (Not oDateCompImage Is Nothing) Then
                            Me.AddCalendar(oFirstUseDateImage, oTextBox)
                        End If
                        If (Not String.IsNullOrEmpty(dvRow(DealerReconWrk.COL_NAME_FIRST_USE_DATE).ToString())) Then
                            Me.PopulateControlFromBOProperty(oTextBox, GetDateFormattedString(dvRow(DealerReconWrk.COL_NAME_FIRST_USE_DATE)))
                        End If
                        oTextBox = CType(.FindControl("moLastUseDateTextGrid"), TextBox)
                        oTextBox.Attributes.Add("onchange", "setDirty()")
                        Dim oLastUseDateImage As ImageButton = CType(.FindControl("moLastUseDateImageGrid"), ImageButton)
                        If (Not oDateCompImage Is Nothing) Then
                            Me.AddCalendar(oLastUseDateImage, oTextBox)
                        End If
                        If (Not String.IsNullOrEmpty(dvRow(DealerReconWrk.COL_NAME_LAST_USE_DATE).ToString())) Then
                            Me.PopulateControlFromBOProperty(oTextBox, GetDateFormattedString(dvRow(DealerReconWrk.COL_NAME_LAST_USE_DATE)))
                        End If
                        oTextBox = CType(.FindControl("moSimCardNumTextGrid"), TextBox)
                        oTextBox.Attributes.Add("onchange", "setDirty()")
                        Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_SIM_CARD_NUMBER))

                        oTextBox = CType(.FindControl("moRegionTextGrid"), TextBox)
                        oTextBox.Attributes.Add("onchange", "setDirty()")
                        Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_REGION))

                        oTextBox = CType(.FindControl("moMembershipTypeTextGrid"), TextBox)
                        oTextBox.Attributes.Add("onchange", "setDirty()")
                        Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_MEMBERSHIP_TYPE))

                        oTextBox = CType(.FindControl("moCessOfficeTextGrid"), TextBox)
                        oTextBox.Attributes.Add("onchange", "setDirty()")
                        Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_CESS_OFFICE))

                        oTextBox = CType(.FindControl("moCessSalesrepTextGrid"), TextBox)
                        oTextBox.Attributes.Add("onchange", "setDirty()")
                        Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_CESS_SALESREP))

                        oTextBox = CType(.FindControl("moBusinesslineTextGrid"), TextBox)
                        oTextBox.Attributes.Add("onchange", "setDirty()")
                        Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_BUSINESSLINE))

                        oTextBox = CType(.FindControl("moSalesDepartmentTextGrid"), TextBox)
                        oTextBox.Attributes.Add("onchange", "setDirty()")
                        Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_SALES_DEPARTMENT))

                        oTextBox = CType(.FindControl("moLinkedCertNumberTextGrid"), TextBox)
                        oTextBox.Attributes.Add("onchange", "setDirty()")
                        Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_LINKED_CERT_NUMBER))

                        oTextBox = CType(.FindControl("moAdditionalInfoTextGrid"), TextBox)
                        oTextBox.Attributes.Add("onchange", "setDirty()")
                        Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_ADDITIONAL_INFO))

                        oTextBox = CType(.FindControl("moCreditCardLast4DigitTextGrid"), TextBox)
                        oTextBox.Attributes.Add("onchange", "setDirty()")
                        Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_CREDITCARD_LAST_FOUR_DIGIT))

                        oTextBox = CType(.FindControl("moFinanceAmount"), TextBox)
                        oTextBox.Attributes.Add("onchange", "setDirty()")
                        Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_FINANCED_AMOUNT))

                        oTextBox = CType(.FindControl("moFinanceFrequency"), TextBox)
                        oTextBox.Attributes.Add("onchange", "setDirty()")
                        Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_FINANCED_FREQUENCY))

                        oTextBox = CType(.FindControl("moFinanceInstallmentNum"), TextBox)
                        oTextBox.Attributes.Add("onchange", "setDirty()")
                        Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_FINANCED_INSTALLMENT_NUMBER))

                        oTextBox = CType(.FindControl("moFinanceInstallmentAmount"), TextBox)
                        oTextBox.Attributes.Add("onchange", "setDirty()")
                        Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_FINANCED_INSTALLMENT_AMOUNT))

                        oTextBox = CType(.FindControl("moGenderTextGrid"), TextBox)
                        oTextBox.Attributes.Add("onchange", "setDirty()")
                        Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_GENDER))

                        oTextBox = CType(.FindControl("moPlaceOfBirthTextGrid"), TextBox)
                        oTextBox.Attributes.Add("onchange", "setDirty()")
                        Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_PLACE_OF_BIRTH))

                        oTextBox = CType(.FindControl("moPersonTypeTextGrid"), TextBox)
                        oTextBox.Attributes.Add("onchange", "setDirty()")
                        Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_PERSON_TYPE))

                        oTextBox = CType(.FindControl("moNumOfConsecutivePaymentsTextGrid"), TextBox)
                        oTextBox.Attributes.Add("onchange", "setDirty()")
                        Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_NUM_OF_CONSECUTIVE_PAYMENTS))

                        oTextBox = CType(.FindControl("moDealerCurrentPlanCodeTextGrid"), TextBox)
                        oTextBox.Attributes.Add("onchange", "setDirty()")
                        Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_DEALER_CURRENT_PLAN_CODE))

                        oTextBox = CType(.FindControl("moDealerScheduledPlanCodeTextGrid"), TextBox)
                        oTextBox.Attributes.Add("onchange", "setDirty()")
                        Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_DEALER_SCHEDULED_PLAN_CODE))

                        oTextBox = CType(.FindControl("moDealerUpdateReasonTextGrid"), TextBox)
                        oTextBox.Attributes.Add("onchange", "setDirty()")
                        Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_DEALER_UPDATE_REASON))

                        oTextBox = CType(.FindControl("moCUIT_CUILTextGrid"), TextBox)
                        oTextBox.Attributes.Add("onchange", "setDirty()")
                        Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_CUIT_CUIL))

                        oTextBox = CType(.FindControl("momaritalstatusTextGrid"), TextBox)
                        oTextBox.Attributes.Add("onchange", "setDirty()")
                        Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_MARITAL_STATUS))

                        oTextBox = CType(.FindControl("moNationalityTextGrid"), TextBox)
                        oTextBox.Attributes.Add("onchange", "setDirty()")
                        Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_NATIONALITY))

                        oTextBox = CType(.FindControl("moDateOfBirthTextGrid"), TextBox)
                        oTextBox.Attributes.Add("onchange", "setDirty()")
                        Dim oDateofbirthImage As ImageButton = CType(.FindControl("moDateOfBirthImageGrid"), ImageButton)
                        If (Not oDateofbirthImage Is Nothing) Then
                            Me.AddCalendar(oDateofbirthImage, oTextBox)
                        End If
                        If (Not String.IsNullOrEmpty(dvRow(DealerReconWrk.COL_NAME_BIRTH_DATE).ToString())) Then
                            Me.PopulateControlFromBOProperty(oTextBox, GetDateFormattedString(dvRow(DealerReconWrk.COL_NAME_BIRTH_DATE)))
                        End If
                        oTextBox = CType(.FindControl("moFinanceDateTextGrid"), TextBox)
                        oTextBox.Attributes.Add("onchange", "setDirty()")
                        Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_FINANCED_DATE))

                        oTextBox = CType(.FindControl("moDownPaymentTextGrid"), TextBox)
                        oTextBox.Attributes.Add("onchange", "setDirty()")
                        Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_DOWN_PAYMENT))

                        oTextBox = CType(.FindControl("moAdvancePaymentTextGrid"), TextBox)
                        oTextBox.Attributes.Add("onchange", "setDirty()")
                        Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_ADVANCE_PAYMENT))

                        oTextBox = CType(.FindControl("moUpgradeTermTextGrid"), TextBox)
                        oTextBox.Attributes.Add("onchange", "setDirty()")
                        Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_UPGRADE_TERM))

                        oTextBox = CType(.FindControl("moBillingAccountNumberTextGrid"), TextBox)
                        oTextBox.Attributes.Add("onchange", "setDirty()")
                        Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_BILLING_ACCOUNT_NUMBER))

                        oTextBox = CType(.FindControl("moLoanCodeTextGrid"), TextBox)
                        oTextBox.Attributes.Add("onchange", "setDirty()")
                        Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_LOAN_CODE))

                        oTextBox = CType(.FindControl("moPaymentShiftNumberTextGrid"), TextBox)
                        oTextBox.Attributes.Add("onchange", "setDirty()")
                        Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_PAYMENT_SHIFT_NUMBER))

                        oTextBox = CType(.FindControl("moVoucherNumberTextGrid"), TextBox)
                        Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_VOUCHER_NUMBER))

                        oTextBox = CType(.FindControl("moRMATextGrid"), TextBox)
                        Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_RMA))

                        oTextBox = CType(.FindControl("moOutstandingBalanceAmountTextGrid"), TextBox)
                        Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_OUTSTANDING_BALANCE_AMOUNT))

                        oTextBox = CType(.FindControl("moOutstandingBalanceDueDateTextGrid"), TextBox)
                        Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_OUTSTANDING_BALANCE_DUE_DATE))

                        oTextBox = CType(.FindControl("moRefundAmountTextGrid"), TextBox)
                        Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_REFUND_AMOUNT))

                        oTextBox = CType(.FindControl("moDeviceTYpeTextGrid"), TextBox)
                        Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_DEVICE_TYPE))

                        oTextBox = CType(.FindControl("moUpgradeDateTextGrid"), TextBox)
                        Dim oUpgradeDateImage As ImageButton = CType(.FindControl("moUpgradeDateImageGrid"), ImageButton)

                        If (Not oUpgradeDateImage Is Nothing) Then
                            Me.AddCalendar(oUpgradeDateImage, oTextBox)
                        End If
                        If (Not String.IsNullOrEmpty(dvRow(DealerReconWrk.COL_NAME_UPGRADE_DATE).ToString())) Then
                            Me.PopulateControlFromBOProperty(oTextBox, GetDateFormattedString(dvRow(DealerReconWrk.COL_NAME_UPGRADE_DATE)))
                        End If

                        oTextBox = CType(.FindControl("moAppleCareFeeTextGrid"), TextBox)
                            Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_APPLECARE_FEE))

                            oTextBox = CType(.FindControl("moOccupationTextGrid"), TextBox)
                            Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_OCCUPATION))

                        Else
                            'SetColumnState(CANCELLATION_CODE_COL, False)
                            oTextBox = CType(.FindControl("moCancelCodeTextGrid"), TextBox)
                        oTextBox.Attributes.Add("onchange", "setDirty()")
                        Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerReconWrk.COL_NAME_CANCELLATION_CODE))
                    End If

                End With
            End If
            BaseItemBound(source, e)

        End Sub

        Private Sub Grid_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles moDataGrid.Sorting
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
                Me.State.SortExpression = e.SortExpression
                'Me.SortDirection = e.SortDirection.ToString()
                If Me.SortDirection = "Ascending" Then
                    Me.SortDirection = "Descending"
                    Me.State.SortExpression = e.SortExpression + " DESC"
                Else
                    Me.SortDirection = "Ascending"
                    Me.State.SortExpression = e.SortExpression + " ASC"
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


                Me.State.PageIndex = 0
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

        Protected Overloads Sub ItemCreated(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
            BaseItemCreated(sender, e)
        End Sub

        Protected Overloads Sub ItemCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
            BaseItemCreated(sender, e)
        End Sub

        Protected Sub BindBoPropertiesToGridHeaders(ByVal dealerReconWrkInfo As DealerReconWrk)
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "RecordType", Me.moDataGrid.Columns(RECORD_TYPE_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, SKU_NUMBER_PROPERTY, Me.moDataGrid.Columns(SKU_Number))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "RejectReason", Me.moDataGrid.Columns(REJECT_REASON_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "Dealer", Me.moDataGrid.Columns(DEALER_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "ProductCode", Me.moDataGrid.Columns(PRODUCT_CODE_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "ProductPrice", Me.moDataGrid.Columns(PRODUCT_PRICE_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "ManWarranty", Me.moDataGrid.Columns(MAN_WARRANTY_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "ItemCode", Me.moDataGrid.Columns(ITEM_CODE_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "Item", Me.moDataGrid.Columns(ITEM_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "ExtWarranty", Me.moDataGrid.Columns(EXT_WARRANTY_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "PricePol", Me.moDataGrid.Columns(PRICE_POL_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "Sr", Me.moDataGrid.Columns(SR_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "BranchCode", Me.moDataGrid.Columns(BRANCH_CODE_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "NewBranchCode", Me.moDataGrid.Columns(NEW_BRANCH_CODE_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "NumberComp", Me.moDataGrid.Columns(NUMBER_COMP_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "DateComp", Me.moDataGrid.Columns(DATE_COMP_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "Certificate", Me.moDataGrid.Columns(CERTIFICATE_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "IdentificationNumber", Me.moDataGrid.Columns(IDENTIFICATION_NUMBER_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "Salutation", Me.moDataGrid.Columns(SALUTATION_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "CustomerName", Me.moDataGrid.Columns(CUSTOMER_NAME_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "LanguagePref", Me.moDataGrid.Columns(LANGUAGE_PREF_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "Address1", Me.moDataGrid.Columns(ADDRESS1_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "City", Me.moDataGrid.Columns(CITY_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "Zip", Me.moDataGrid.Columns(ZIP_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "StateProvince", Me.moDataGrid.Columns(STATE_PROVINCE_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "HomePhone", Me.moDataGrid.Columns(HOME_PHONE_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "WorkPhone", Me.moDataGrid.Columns(WORK_PHONE_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "IsoCode", Me.moDataGrid.Columns(ISO_CODE_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "ExtwarrSaledate", Me.moDataGrid.Columns(EXTWARR_SALEDATE_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "TypePayment", Me.moDataGrid.Columns(TYPE_PAYMENT_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "CancellationCode", Me.moDataGrid.Columns(CANCELLATION_CODE_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "Manufacturer", Me.moDataGrid.Columns(MANUFACTURER_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "Model", Me.moDataGrid.Columns(MODEL_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "SerialNumber", Me.moDataGrid.Columns(SERIAL_NUMBER_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "IMEINumber", Me.moDataGrid.Columns(IMEI_NUMBER_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "PostPrePaid", Me.moDataGrid.Columns(POST_PRE_PAID_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "NewProductCode", Me.moDataGrid.Columns(NEW_PRODUCT_CODE_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "DocumentType", Me.moDataGrid.Columns(DOCUMENT_TYPE_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "DocumentAgency", Me.moDataGrid.Columns(DOCUMENT_AGENCY_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "DocumentIssueDate", Me.moDataGrid.Columns(DOCUMENT_ISSUE_DATE_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "RGNumber", Me.moDataGrid.Columns(RG_NUMBER_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "IDType", Me.moDataGrid.Columns(ID_TYPE_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "BillingFrequency", Me.moDataGrid.Columns(BILLING_FREQUENCY_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "NumberOfInstallments", Me.moDataGrid.Columns(NUMBER_OF_INSTALLMENTS_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "InstallmentAmount", Me.moDataGrid.Columns(INSTALLMENT_AMOUNT_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "BankRtnNumber", Me.moDataGrid.Columns(BANK_RTN_NUMBER_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "BankAccountNumber", Me.moDataGrid.Columns(BANK_ACCOUNT_NUMBER_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "BankAcctOwnerName", Me.moDataGrid.Columns(BANK_ACCT_OWNER_NAME_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "BankBranchNumber", Me.moDataGrid.Columns(BANK_BRANCH_NUMBER_COL))

            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "SalesTax", Me.moDataGrid.Columns(SALES_TAX_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "Address2", Me.moDataGrid.Columns(ADDRESS2_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "Email", Me.moDataGrid.Columns(EMAIL_COL))

            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "CustCountry", Me.moDataGrid.Columns(CUST_COUNTRY_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "CountryPurch", Me.moDataGrid.Columns(COUNTRY_PURCH_COL))

            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "DatePaidFor", Me.moDataGrid.Columns(DATE_PAID_FOR_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, MEMBERSHIP_NUMBER_PROPERTY, Me.moDataGrid.Columns(MEMBERSHIP_NUMBER_COL))

            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "Address3", Me.moDataGrid.Columns(ADDRESS3_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "OriginalRetailPrice", Me.moDataGrid.Columns(ORIGINAL_RETAIL_PRICE_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "BillingPlan", Me.moDataGrid.Columns(BILLING_PLAN_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "BillingCycle", Me.moDataGrid.Columns(BILLING_CYCLE_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "SubscriberStatus", Me.moDataGrid.Columns(SUBSCRIBER_STATUS_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "SuspendedReason", Me.moDataGrid.Columns(SUSPENDED_REASON_COL))

            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "MobileType", Me.moDataGrid.Columns(MOBILE_TYPE_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "FirstUseDate", Me.moDataGrid.Columns(FIRST_USE_DATE_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "LastUseDate", Me.moDataGrid.Columns(LAST_USE_DATE_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "SimCardNumber", Me.moDataGrid.Columns(SIM_CARD_NUMBER_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, "Region", Me.moDataGrid.Columns(REGION_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, MEMBERSHIP_TYPE_PROPERTY, Me.moDataGrid.Columns(MEMBERSHIP_TYPE_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, CESS_OFFICE_PROPERTY, Me.moDataGrid.Columns(CESS_OFFICE_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, CESS_SALESREP_PROPERTY, Me.moDataGrid.Columns(CESS_SALESREP_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, BUSINESSLINE_PROPERTY, Me.moDataGrid.Columns(BUSINESSLINE_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, SALES_DEPARTMENT_PROPERTY, Me.moDataGrid.Columns(SALES_DEPARTMENT_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, LINKED_CERT_NUMBER_PROPERTY, Me.moDataGrid.Columns(LINKED_CERT_NUMBER_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, ADDITIONAL_INFO_PROPERTY, Me.moDataGrid.Columns(ADDITIONAL_INFO_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, CREDITCARD_LASTFOURDIGIT_PROPERTY, Me.moDataGrid.Columns(CREDITCARD_LAST_FOUR_DIGIT_COL)) 'REQ-1169

            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, FINANCED_AMOUNT, Me.moDataGrid.Columns(FINANCED_AMOUNT_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, FINANCED_FREQUENCY, Me.moDataGrid.Columns(FINANCED_FREQUENCY_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, FINANCED_INSTALLMENT_NUMBER, Me.moDataGrid.Columns(FINANCED_INSTALLMENT_NUMBER_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, FINANCED_INSTALLMENT_AMOUNT, Me.moDataGrid.Columns(FINANCED_INSTALLMENT_AMOUNT_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, GENDER_PROPERTY, Me.moDataGrid.Columns(GENDER_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, PLACE_OF_BIRTH_PROPERTY, Me.moDataGrid.Columns(PLACE_OF_BIRTH_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, CUIT_CUIL_PROPERTY, Me.moDataGrid.Columns(CUIT_CUIL_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, PERSON_TYPE_PROPERTY, Me.moDataGrid.Columns(PERSON_TYPE_COL))

            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, NUM_OF_CONSECUTIVE_PAYMENTS_PROPERTY, Me.moDataGrid.Columns(NUM_OF_CONSECUTIVE_PAYMENTS_COL))

            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, DEALER_CURRENT_PLAN_CODE_PROPERTY, Me.moDataGrid.Columns(DEALER_CURRENT_PLAN_CODE_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, DEALER_SCHEDULED_PLAN_CODE_PROPERTY, Me.moDataGrid.Columns(DEALER_SCHEDULED_PLAN_CODE_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, DEALER_UPDATE_REASON_PROPERTY, Me.moDataGrid.Columns(DEALER_UPDATE_REASON_COL))

            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, MARITAL_STATUS_PROPERTY, Me.moDataGrid.Columns(MARITAL_STATUS_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, NATIONALITY_PROPERTY, Me.moDataGrid.Columns(NATIONALITY_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, BIRTH_DATE_PROPERTY, Me.moDataGrid.Columns(BIRTH_DATE_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, FINANCE_DATE_PROPERTY, Me.moDataGrid.Columns(FINANCE_DATE_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, DOWN_PAYMENT_PROPERTY, Me.moDataGrid.Columns(DOWN_PAYMENT_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, ADVANCE_PAYMENT_PROPERTY, Me.moDataGrid.Columns(ADVANCE_PAYMENT_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, UPGRADE_TERM_PROPERTY, Me.moDataGrid.Columns(UPGRADE_TERM_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, BILLING_ACCOUNT_NUMBER_PROPERTY, Me.moDataGrid.Columns(BILLING_ACCOUNT_NUMBER_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, SERVICE_LINE_NUMBER_PROPERTY, Me.moDataGrid.Columns(SERVICE_LINE_NUMBER_COL))

            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, LOAN_CODE_PROPERTY, Me.moDataGrid.Columns(LOAN_CODE_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, PAYMENT_SHIFT_NUMBER_PROPERTY, Me.moDataGrid.Columns(PAYMENT_SHIFT_NUMBER_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, UPGRADE_DATE_PROPERTY, Me.moDataGrid.Columns(UPGRADE_DATE_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, VOUCHER_NUMBER_PROPERTY, Me.moDataGrid.Columns(VOUCHER_NUMBER_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, RMA_PROPERTY, Me.moDataGrid.Columns(RMA_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, REFUND_AMONT_PROPERTY, Me.moDataGrid.Columns(REFUND_AMOUNT_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, DEVICE_TYPE_PROPERTY, Me.moDataGrid.Columns(DEVICE_TYPE_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, APPLECARE_FEE_PROPERTY, Me.moDataGrid.Columns(APPLECARE_FEE))
            Me.ClearGridViewHeadersAndLabelsErrSign()
        End Sub

        Protected Sub BindBoPropertiesToBundlesGridHeaders(ByVal dealerReconWrkBundlesInfo As DealerReconWrkBundles)
            Me.BindBOPropertyToGridHeader(dealerReconWrkBundlesInfo, "ItemNumber", Me.gvPop.Columns(ITEM_NUMBER_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkBundlesInfo, "ItemManufacturer", Me.gvPop.Columns(ITEM_MANUFACTURER_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkBundlesInfo, "ItemModel", Me.gvPop.Columns(ITEM_MODEL_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkBundlesInfo, "ItemSerialNumber", Me.gvPop.Columns(ITEM_SERIAL_NUMBER_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkBundlesInfo, "ItemDescription", Me.gvPop.Columns(ITEM_DESCRIPTION_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkBundlesInfo, "ItemPrice", Me.gvPop.Columns(ITEM_PRICE_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkBundlesInfo, "ItemBundleValue", Me.gvPop.Columns(ITEM_BUNDLE_VALUE_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkBundlesInfo, "ItemManWarranty", Me.gvPop.Columns(ITEM_MAN_WARRANTY_COL))
            Me.ClearGridViewHeadersAndLabelsErrSign()
        End Sub

        Private Sub SetFocusOnEditableFieldInGrid(ByVal grid As DataGrid, ByVal cellPosition As Integer, ByVal controlName As String, ByVal itemIndex As Integer)
            'Set focus on the Description TextBox for the EditItemIndex row
            Dim desc As TextBox = CType(grid.Items(itemIndex).Cells(cellPosition).FindControl(controlName), TextBox)
            SetFocus(desc)
        End Sub

        Protected Sub BtnViewBundles_Click(ByVal sender As Object, ByVal e As System.EventArgs)
            Dim reconWorkIDForm As String = CType(Me.moDataGrid.Rows(Me.moDataGrid.SelectedIndex).FindControl("moDealerReconWrkIdLabel"), Label).Text
            Dim reconWorkIDFormGUID As Guid = GetGuidFromString(reconWorkIDForm)
            Me.State.DealerReconWrkId = reconWorkIDFormGUID
            PopulateBundlesGrid(reconWorkIDFormGUID)

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
                Select Case SaveBundles()
                    Case 1, 2
                        Me.DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                End Select
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
            ApplyBundles()
        End Sub

        Protected Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
            If IsDataGBundlesPageDirty() Then
                ApplyBundles()
            End If
            'Me.ErrController2.Clear_Hide()
            Me.mdlPopup.Hide()
            Me.ClearGridViewHeadersAndLabelsErrSign()
            'Me.ClearGridHeaders(gvPop)
        End Sub

        Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
            Try
                Me.SaveGuiState()
                Me.SortDirection = EMPTY
                Me.SetGridItemStyleColor(moDataGrid)
                Me.ShowMissingTranslations(MasterPage.MessageController)
                Me.State.PageIndex = 0
                'Me.TranslateGridHeader(moDataGrid)
                'Me.TranslateGridHeader(gvPop)
                'Me.TranslateGridControls(moDataGrid)
                PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnClearSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClearSearch.Click
            Try
                Me.State.srchRecordType = String.Empty
                Me.State.srchRejectCode = String.Empty
                Me.State.srchRejectReason = String.Empty

                Me.moRecordTypeSearchDrop.SelectedIndex = 0
                Me.moRejectCodeText.Text = String.Empty
                Me.moRejectReasonText.Text = String.Empty

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub


#End Region

    End Class

End Namespace
