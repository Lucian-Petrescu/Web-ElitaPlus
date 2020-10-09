Imports System.Collections.Generic
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms

Namespace Tables
    Partial Class DealerForm
        Inherits ElitaPlusSearchPage

        Protected WithEvents UserControlAvailableSelectedClaimTypes As Assurant.ElitaPlus.ElitaPlusWebApp.Generic.UserControlAvailableSelected_New
        Protected WithEvents UserControlAvailableSelectedCoverageTypes As Assurant.ElitaPlus.ElitaPlusWebApp.Generic.UserControlAvailableSelected_New

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        'Protected WithEvents ErrorCtrl As ErrorController
        'Protected WithEvents moMailingAddressController As UserControlAddress_New
        'Protected WithEvents moaddressController As UserControlAddress_New
        'Protected WithEvents moCompanyMultipleDrop As MultipleColumnDDLabelControl_New
        'Protected WithEvents New_Fields As System.Web.UI.HtmlControls.HtmlTable
        'yogita 
        'Protected WithEvents btnAcctSettings As System.Web.UI.WebControls.Button
        'caio
        'Protected WithEvents btnAssignSC As System.Web.UI.WebControls.Button

        'NOTE: The following placeholder declaration is required by the Web Form Designer.
        'Do not delete or move it.
        Private designerPlaceholderDeclaration As System.Object


        Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

#Region "Constants"
        Public Shared URL As String = "DealerForm.aspx"
        Private Const DEFAULT_LAST_CERTIFICATE_NUMBER As String = "0"
        Private Const INSURANCE_COMPANY_TYPE As String = "1"
        Private Const LABEL_SELECT_COMPANY As String = "COMPANY"
        Private Const MAILING_ADDRESS_TAB As Short = 1
        Private Const NOTHING_SELECTED_GUID As String = "00000000-0000-0000-0000-000000000000"
        Public Const SVC_ORDER_ADDRESS_TAB As Short = 2

        Public Const BankInfoStartIndex As Int16 = 528
        'REQ-1142
        Public Const LICENSE_TAG_FLAG_YES As String = "YES"
        Public Const USE_CLIENT_CODE_YES As String = "Y"
        Private COPY_SCHEDULE As String = "COPY_DEALER"
        Private NEW_SCHEDULE As String = "NEW_DEALER"
        Private INIT_LOAD As String = "INIT_LOAD"
        Private SHARE_CUSTOMER As String = "SHARE_CUSTOMER-NO"
        '  Public Const VIN_RESTRIC_ID As String = "Vin"
        '' Public Const 


        Private Const AVAILABLE_CLAIM_TYPES As String = "AVAILABLE_CLAIM_TYPES"
        Private Const SELECTED_CLAIM_TYPES As String = "SELECTED_CLAIM_TYPES"
        Private Const AVAILABLE_COVERAGE_TYPES As String = "AVAILABLE_COVERAGE_TYPES"
        Private Const SELECTED_COVERAGE_TYPES As String = "SELECTED_COVERAGE_TYPES"

#End Region

#Region "Merchant_Code"
        'Public Const EDIT_COMMAND_NAME As String = "EditRecord"
        'Public Const SELECT_COMMAND_NAME As String = "SelectRecord"

        Private Const MERCHANT_CODE_ID As Integer = 1
        Private Const COMPANY_CREDIT_CARD_ID As Integer = 2
        Private Const COMPANY_CREDIT_CARD_TYPE As Integer = 3
        Private Const MERCHANT_CODE As Integer = 4

        Private Const ID_CONTROL_NAME As String = "IdLabel"
        Private Const COMPANY_CREDIT_CARD_ID_LABEL_CONTROL_NAME As String = "CompanyCreditCardIDLabel"
        Private Const COMPANY_CREDIT_CARD_TYPE_LABEL_CONTROL_NAME As String = "CompanyCreditCardTypeLabel"
        Private Const COMPANY_CREDIT_CARD_CONTROL_NAME As String = "cboCompanyCreditCardInGrid"
        Private Const MERCHANT_CODE_LABEL_CONTROL_NAME As String = "MerchantCodeLabel"
        Private Const MERCHANT_CODE_TEXTBOX_CONTROL_NAME As String = "MerchantCodeTextBox"

        Private Const MSG_RECORD_SAVED_OK As String = "MSG_RECORD_SAVED_OK"
        Private Const MSG_RECORD_NOT_SAVED As String = "MSG_RECORD_NOT_SAVED"
        Private Const NO_ROW_SELECTED_INDEX As Integer = -1


        Private Const EDIT_COMMAND As String = "EditRecord"
        Private Const CANCEL_COMMAND As String = "CancelRecord"
        Private Const SAVE_COMMAND As String = "SaveRecord"
        Private Const DELETE_COMMAND As String = "DeleteRecord"
        Private Const SORT_COMMAND As String = "Sort"

#End Region

#Region "Tabs"
        Public Const Tab_AddressTab As String = "0"
        Public Const Tab_MailingAddress As String = "1"
        Public Const Tab_ServiceOrderAddress As String = "2"
        Public Const Tab_BnakInfo As String = "3"
        Public Const Tab_MerchantCode As String = "4"
        Public Const Tab_ClaimCloseRules As String = "5"
        Public Const Tab_Attributes As String = "6"
        Public Const Tab_DealerInflation as string ="7"
        Public Const Tab_RiskTypeTolerance as string ="8"

        Dim DisabledTabsList As New List(Of String)()
       
#End Region

#Region "ENUMERATIONS"

        Public Enum enumPermissionType
            ENUM_NONE = 0
            ENUM_SINGLE = 1
            ENUM_MULTIPLE = 2
        End Enum

#End Region

#Region "Variables"

        Private mbIsFirstPass As Boolean = True

#End Region

#Region "Properties"

        Public ReadOnly Property AddressCtr() As UserControlAddress_New
            Get
                Return moaddressController
            End Get
        End Property
        'Public ReadOnly Property CompanyMultipleDrop() As MultipleColumnDDLabelControl
        '    Get
        '        If moCompanyMultipleDrop Is Nothing Then
        '            moCompanyMultipleDrop = CType(FindControl("multipleDropControl"), MultipleColumnDDLabelControl)
        '        End If
        '        Return moCompanyMultipleDrop
        '    End Get
        'End Property

        Public ReadOnly Property MailingAddressCtr() As UserControlAddress_New
            Get
                Return moMailingaddressController
            End Get
        End Property

        Public ReadOnly Property SvcOrderAddressCtr() As UserControlAddress_New
            Get
                Return moSvcOrderAddressController
            End Get
        End Property

        Private Property IsNewMerchantCode() As Boolean
            Get
                Return State.IsMerchantCodeNew
            End Get
            Set(Value As Boolean)
                State.IsMerchantCodeNew = Value
            End Set
        End Property

#End Region

#Region "Page State"
        Private IsReturningFromChild As Boolean = False
        Class MyState
            Public MyBO As Dealer
            Public ScreenSnapShotBO As Dealer
            Public IsNew As Boolean = False
            Public IsACopy As Boolean
            Public CmpnId As Guid
            Public Ocompany As Company
            Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
            Public LastErrMsg As String
            Public HasDataChanged As Boolean
            Public companyDV As DataView
            Public blnIsComingFromNew As Boolean = False
            Public blnIsComingFromCopy As Boolean = False
            Public SvcOrdersByDealer As ServiceOrdersAddress = Nothing
            Public DealerCountryID As Guid

            Public IsMerchantCodeEditMode As Boolean
            Public IsMerchantCodeNew As Boolean
            Public MerchantCodeSearchDV As MerchantCode.MerchantCodeSearchDV = Nothing
            Public MerchantCodeID As Guid = Guid.Empty
            Public MyMerchantCodeBO As MerchantCode
            Public PageIndex As Integer = 0
            Public MerchantCodeSortExpression As String = MerchantCode.MerchantCodeSearchDV.COL_COMPANY_CREDIT_CARD_TYPE
            Public IsMerchantCodeAfterSave As Boolean
            Public AddingMerchantCodeNewRow As Boolean
            Public Company_ID As Guid = Guid.Empty
            Public MerchantCodeUsed As Boolean = False
            Public MerchantCodeGridTranslated As Boolean = False

            'DEF-3066
            Public PreviousMerchantCodeSearchDV As MerchantCode.MerchantCodeSearchDV = Nothing
            Public SelectedCompanyCreditCardType As String
            Public MerchantCodeAddNew As Boolean = False
            Public MerchantCodeEdit As Boolean = False

            Public IsEditMode As Boolean
            Public IsCertificateExists As Boolean
            Public IsReadOnly As Boolean = False
            Public Action As String
            'DEF-3066

            Public LawsuitMandatoryIdAtLoad As Guid = Guid.Empty
            Public OldDealerId As Guid = Guid.Empty



        End Class

        Public Sub New()
            MyBase.New(New MyState)
        End Sub

        Protected Shadows ReadOnly Property State() As MyState
            Get
                Return CType(MyBase.State, MyState)
            End Get
        End Property

        Private Sub Page_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall

            Try
                If CallingParameters IsNot Nothing Then
                    'Get the id from the parent
                    State.MyBO = New Dealer(CType(CallingParameters, Guid))
                    State.IsCertificateExists = (State.MyBO.GetDealerCertificatesCount() > 0)
                    State.Ocompany = New Company(State.MyBO.CompanyId)
                Else
                    State.IsNew = True
                    State.IsCertificateExists = False
                    State.Ocompany = New Company(CType(ElitaPlusIdentity.Current.ActiveUser.Companies.Item(0), Guid))
                    cboImeiNoUse.Enabled = "True"
                End If

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles MyBase.PageReturn
            Try
                MenuEnabled = True
                IsReturningFromChild = True
                Dim retObj As ServiceCenterForm.ReturnType = CType(ReturnPar, ServiceCenterForm.ReturnType)
                State.HasDataChanged = retObj.HasDataChanged
                Select Case retObj.LastOperation
                    Case ElitaPlusPage.DetailPageCommand.Back
                        If retObj IsNot Nothing Then
                            State.MyBO = New Dealer(retObj.EditingBo.OriginalDealerId)
                        End If
                    Case ElitaPlusPage.DetailPageCommand.Save
                        If retObj IsNot Nothing Then
                            State.MyBO = New Dealer(retObj.EditingBo.OriginalDealerId)
                            AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                        End If
                End Select
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Page Return Type"
        Public Class ReturnType
            Public LastOperation As DetailPageCommand
            Public EditingBo As Dealer
            Public HasDataChanged As Boolean
            Public Sub New(LastOp As DetailPageCommand, curEditingBo As Dealer, hasDataChanged As Boolean)
                LastOperation = LastOp
                EditingBo = curEditingBo
                Me.HasDataChanged = hasDataChanged
            End Sub
        End Class
#End Region

#Region "Page Events"
        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load, Me.Load
            'Put user code to initialize the page here
            If mbIsFirstPass = True Then
                mbIsFirstPass = False
            Else
                ' Do not load again the Page that was already loaded
                Return
            End If
            MasterPage.MessageController.Clear_Hide()
            'hide the user control...since we are doing our ownlist.
            'ControlMgr.SetVisibleControl(Me, PostalCodeFormatLists, False)
            Try
                If Not IsPostBack Then
                    MasterPage.MessageController.Clear()
                    MasterPage.UsePageTabTitleInBreadCrum = False
                    MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Tables")
                    UpdateBreadCrum()
                    'Date Calendars
                    MenuEnabled = False
                    'Me.AddConfirmation(Me.btnDelete_WRITE, Message.DELETE_RECORD_PROMPT)
                    IsNewMerchantCode = False
                    If State.MyBO Is Nothing Then
                        State.MyBO = New Dealer
                    End If
                    If State.companyDV Is Nothing Then
                        State.companyDV = LookupListNew.GetUserCompaniesLookupList()
                    End If
                    State.MyBO.UseSvcOrderAddress = False
                    If LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, State.Ocompany.ServiceOrdersByDealerId) = Codes.YESNO_Y Then
                        State.MyBO.UseSvcOrderAddress = True
                    End If
                    PopulateCompanyDropDown()
                    PopulateDropdowns()
                    PopulateAddressFields()
                    PopulateMailingAddressFields()
                    If State.IsNew = True Then
                        CreateNew()
                    End If
                    If State.MyBO.UseSvcOrderAddress Then
                        State.SvcOrdersByDealer = State.MyBO.SvcOrdersAddress
                        PopulateSvcOrdersAddressFields()
                    End If

                    moBankInfo.ReAssignTabIndex(BankInfoStartIndex)
                    If State.MyBO.CompanyId <> Guid.Empty Then
                        State.DealerCountryID = New Company(State.MyBO.CompanyId).CountryId
                    End If

                    AttributeValues.ParentBusinessObject = CType(State.MyBO, IAttributable)
                    'AttributeValues.TranslateHeaders()--Commented since it is calling two times
                    If Not State.IsNew = True Then ' Added condition since we are already calling PopulateFormFromBOs during creation of new dealer
                        PopulateFormFromBOs()
                    End If
                    EnableDisableFields()
                    CheckUseClientDealerCodeFlag()
                    PopulateMyMerchantCodeGrid()

                    'If Me.State.MerchantCodeUsed Then
                    SetMerchantCodeButtonsState(False)
                    If Not State.MerchantCodeGridTranslated Then
                        TranslateGridHeader(moMerchantCodesDatagrid)
                        'Me.TranslateGridControls(Me.moMerchantCodesDatagrid)
                        State.MerchantCodeGridTranslated = True
                    End If

                    PopulateMyMerchantCodeGrid()
                    ' End If
                Else
                    AttributeValues.ParentBusinessObject = CType(State.MyBO, IAttributable)
                    CheckIfComingFromDeleteConfirm()
                    GetDisabledTabs()
                End If

                'CheckIfComingFromSaveConfirm()
                AttributeValues.BindBoProperties()
                BindBoPropertiesToLabels()
                CheckIfComingFromSaveConfirm()


                If Not IsPostBack Then
                    AddLabelDecorations(State.MyBO)
                End If

                ShareCustLookup()

            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                ' Clean Popup Input
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                HiddenSaveChangesPromptResponse.Value = ""
                HandleErrors(ex, MasterPage.MessageController)
            End Try
            ShowMissingTranslations(MasterPage.MessageController)
        End Sub
#End Region
        Private Sub ShareCustLookup()
            If cboShareCustomers.SelectedValue = SHARE_CUSTOMER Then
                cboCustomerIdLookUpBy.ClearSelection()
                cboCustomerIdLookUpBy.SelectedIndex = 0
                PopulateBOProperty(State.MyBO, "CustomerLookup", cboCustomerIdLookUpBy, False, True)
                cboCustomerIdLookUpBy.Enabled = False
            Else
                cboCustomerIdLookUpBy.Enabled = True
            End If

        End Sub
        Private Sub ShareCustSave()
            If cboShareCustomers.SelectedValue <> SHARE_CUSTOMER Then
                cboCustomerIdLookUpBy.Enabled = True
                If cboCustomerIdLookUpBy.SelectedIndex = 0 Then
                    Throw New GUIException(Message.MSG_CUSOTMER_IDENTITY_LOOKUP_ERR, Assurant.ElitaPlus.Common.ErrorCodes.ERR_CUSTOMER_IDENTITY_LOOKUP_IS_REQ)
                End If
            End If
        End Sub

        Private Sub GetDisabledTabs()
            Dim DisabledTabs As Array = hdnDisabledTab.Value.Split(",")
            If DisabledTabs.Length > 0 AndAlso DisabledTabs(0) IsNot String.Empty Then
                DisabledTabsList.AddRange(DisabledTabs)
                hdnDisabledTab.Value = String.Empty
            End If
        End Sub

        Private Sub PopulateClaimAutoApproveControls()
            Try
                'Me.UserControlAvailableSelectedClaimTypes.BackColor = "#d5d6e4"
                'Me.UserControlAvailableSelectedCoverageTypes.BackColor = "#d5d6e4"

                If Not State.MyBO.Id.Equals(Guid.Empty) Then
                    'Available
                    Dim availableClaimTypesDv As DataView
                    Dim availableCoverageTypesDv As DataView

                    Dim availClaimTypeDS As DataSet = State.MyBO.GetAvailablClaimTypes(State.MyBO.Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                    If availClaimTypeDS.Tables.Count > 0 Then
                        availableClaimTypesDv = New DataView(availClaimTypeDS.Tables(0))
                    End If

                    Dim availCoverageTypeDS As DataSet = State.MyBO.GetAvailableCoverageTypes(State.MyBO.Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                    If availCoverageTypeDS.Tables.Count > 0 Then
                        availableCoverageTypesDv = New DataView(availCoverageTypeDS.Tables(0))
                    End If

                    UserControlAvailableSelectedClaimTypes.AvailableList.Clear()
                    UserControlAvailableSelectedCoverageTypes.AvailableList.Clear()
                    UserControlAvailableSelectedClaimTypes.SetAvailableData(availableClaimTypesDv, LookupListNew.COL_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)
                    UserControlAvailableSelectedCoverageTypes.SetAvailableData(availableCoverageTypesDv, LookupListNew.COL_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)
                    UserControlAvailableSelectedClaimTypes.AvailableDesc = TranslationBase.TranslateLabelOrMessage(AVAILABLE_CLAIM_TYPES)
                    UserControlAvailableSelectedCoverageTypes.AvailableDesc = TranslationBase.TranslateLabelOrMessage(AVAILABLE_COVERAGE_TYPES)

                    'Selected
                    Dim selectedClaimTypesDv As DataView
                    Dim selectedCoverageTypesDv As DataView

                    Dim selectedClaimTypeDS As DataSet = State.MyBO.GetSelectedClaimTypes(State.MyBO.Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                    If selectedClaimTypeDS.Tables.Count > 0 Then
                        selectedClaimTypesDv = New DataView(selectedClaimTypeDS.Tables(0))
                    End If

                    Dim selectedCoverageTypeDS As DataSet = State.MyBO.GetSelectedCoverageTypes(State.MyBO.Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                    If selectedCoverageTypeDS.Tables.Count > 0 Then
                        selectedCoverageTypesDv = New DataView(selectedCoverageTypeDS.Tables(0))
                    End If

                    UserControlAvailableSelectedClaimTypes.SelectedList.Clear()
                    UserControlAvailableSelectedCoverageTypes.SelectedList.Clear()
                    UserControlAvailableSelectedClaimTypes.SetSelectedData(selectedClaimTypesDv, LookupListNew.COL_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)
                    UserControlAvailableSelectedCoverageTypes.SetSelectedData(selectedCoverageTypesDv, LookupListNew.COL_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)
                    UserControlAvailableSelectedClaimTypes.SelectedDesc = TranslationBase.TranslateLabelOrMessage(SELECTED_CLAIM_TYPES)
                    UserControlAvailableSelectedCoverageTypes.SelectedDesc = TranslationBase.TranslateLabelOrMessage(SELECTED_COVERAGE_TYPES)

                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub


#Region "Controlling Logic"

        Protected Sub EnableDisableFields()
            'Enabled by Default
            If State.IsCertificateExists Then
                ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
                ControlMgr.SetEnableControl(Me, lblDealerCode, False)
                ControlMgr.SetEnableControl(Me, txtDealerCode, False)
            Else
                ControlMgr.SetEnableControl(Me, btnDelete_WRITE, True)
                ControlMgr.SetEnableControl(Me, lblDealerCode, True)
                ControlMgr.SetEnableControl(Me, txtDealerCode, True)
            End If

            ControlMgr.SetEnableControl(Me, btnNew_WRITE, True)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, True)
            'ControlMgr.SetEnableControl(Me, btnAcctSettings, True)
            'Req-1297
            ControlMgr.SetVisibleControl(Me, lblFullfileDealer, True)
            ControlMgr.SetVisibleControl(Me, ddlFullfileProcess, True)
            If LookupListNew.GetCodeFromId(LookupListNew.LK_FLP, State.MyBO.UseFullFileProcessId) <> Codes.FLP_NO Then
                lblMaxNCRecords.Visible = True
                txtMaxNCRecords.Visible = True
            End If
            'Req-1297 End

            If State.MyBO.IsNew Then
                ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
                ControlMgr.SetEnableControl(Me, btnNew_WRITE, False)
                ControlMgr.SetEnableControl(Me, btnCopy_WRITE, False)
                'ControlMgr.SetEnableControl(Me, btnAcctSettings, False)
            End If

            ControlMgr.SetVisibleControl(Me, trLineHid1, False)
            ControlMgr.SetVisibleControl(Me, trHid1, False)
            ControlMgr.SetVisibleControl(Me, trHid2, False)
            AddressCtr.EnableControls(False, True)
            DisabledTabsList.Add(Tab_MailingAddress)
            If GetSelectedItem(moClaimAutoApproveDrop).Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_N)) Then
                DisabledTabsList.Add(Tab_DealerInflation)
                DisabledTabsList.Add(Tab_RiskTypeTolerance)
            end if

            State.MyBO.Address.AddressIsRequire = False
            'Req-1142 start
            ControlMgr.SetVisibleControl(Me, trVscLicenseTag, False)
            'Req-1142 end

            'Req-5723 start
            ControlMgr.SetVisibleControl(Me, trVscVinRestrict, False)
            'Req-5723 end

            If State.MyBO.DealerTypeDesc = State.MyBO.DEALER_TYPE_DESC Then
                ControlMgr.SetVisibleControl(Me, trLineHid1, True)
                ControlMgr.SetVisibleControl(Me, trHid1, True)
                ControlMgr.SetVisibleControl(Me, trHid2, True)
                MailingAddressCtr.EnableControls(False, True)
                ControlMgr.SetVisibleControl(Me, dlstRetailerID, False)
                ControlMgr.SetVisibleControl(Me, lblDealerIsRetailer, False)
                ControlMgr.SetVisibleControl(Me, lblValidateSerialNumber, False)
                ControlMgr.SetVisibleControl(Me, moValidateSerialNumberDrop, False)
                State.MyBO.Address.AddressIsRequire = True
                'Req-1142 start
                ControlMgr.SetVisibleControl(Me, trVscLicenseTag, True)
                'Req-1142 End
                'Req-5723 start
                ControlMgr.SetVisibleControl(Me, trVscVinRestrict, True)
                'Req-5723 End
            End If

            If State.MyBO.CertificatesAutonumberId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_Y)) Then
                ControlMgr.SetVisibleControl(Me, lblCertificatesAutonumberPrefix, True)
                ControlMgr.SetVisibleControl(Me, txtCertificatesAutonumberPrefix, True)
                ControlMgr.SetVisibleControl(Me, lblMaxCertNumLengthAlwd, True)
                ControlMgr.SetVisibleControl(Me, txtMaxCertNumLengthAlwd, True)
            Else
                txtCertificatesAutonumberPrefix.Text = String.Empty
                ControlMgr.SetVisibleControl(Me, lblCertificatesAutonumberPrefix, False)
                ControlMgr.SetVisibleControl(Me, txtCertificatesAutonumberPrefix, False)
                txtMaxCertNumLengthAlwd.Text = String.Empty
                ControlMgr.SetVisibleControl(Me, lblMaxCertNumLengthAlwd, False)
                ControlMgr.SetVisibleControl(Me, txtMaxCertNumLengthAlwd, False)
            End If

            ControlMgr.SetEnableControl(Me, cboCertificatesAutonumberId, True)
            ControlMgr.SetEnableControl(Me, txtCertificatesAutonumberPrefix, True)

            If Not State.MyBO.IsNew AndAlso State.MyBO.IsLastContractPolicyAutoGenerated() Then
                ' Individual policy change (US 258694. Disable if Last contract policy no is auto generated)
                ControlMgr.SetEnableControl(Me, cboCertificatesAutonumberId, False)
                ControlMgr.SetEnableControl(Me, txtCertificatesAutonumberPrefix, False)
            End If

            'Enable only for BENEFIT dealer
            ControlMgr.SetVisibleControl(Me, trBenefitDlrTypeCtls1, False)
            ControlMgr.SetVisibleControl(Me, trBenefitDlrTypeCtls2, False)
            ControlMgr.SetVisibleControl(Me, trBenefitDlrTypeCtls3, False)

            If State.MyBO.DealerTypeDesc = State.MyBO.DEALER_TYPE_DESC_WEPP Then
                ControlMgr.SetVisibleControl(Me, moUseEquipment, True)
                ControlMgr.SetVisibleControl(Me, lblUseEquipment, True)
                'If (Me.State.MyBO.UseEquipmentId = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, "Y")) Then
                ControlMgr.SetVisibleControl(Me, moBestReplacementDrop, True)
                ControlMgr.SetVisibleControl(Me, lblBestReplacement, True)
                'Else
                '    ControlMgr.SetVisibleControl(Me, moBestReplacementDrop, False)
                '    ControlMgr.SetVisibleControl(Me, lblBestReplacement, False)
                'End If
                ControlMgr.SetVisibleControl(Me, lblEquipmentList, True)
                ControlMgr.SetVisibleControl(Me, moEquipmentListDrop, True)
            ElseIf (State.MyBO.DealerTypeDesc IsNot Nothing AndAlso State.MyBO.DealerTypeDesc.ToUpper() = State.MyBO.DEALER_TYPE_BENEFIT) Then
                ControlMgr.SetVisibleControl(Me, trBenefitDlrTypeCtls1, True)
                ControlMgr.SetVisibleControl(Me, trBenefitDlrTypeCtls2, True)
                ControlMgr.SetVisibleControl(Me, trBenefitDlrTypeCtls3, True)
                If (ddlSuspenseApplies.SelectedItem.Value.ToUpper() = "YESNO-Y") Then
                    txtSuspensePeriod.Style.Add("display", "block")
                    lblSuspensePeriod.Style.Add("display", "block")
                Else
                    txtSuspensePeriod.Style.Add("display", "none")
                    lblSuspensePeriod.Style.Add("display", "none")
                End If
            Else
                ControlMgr.SetVisibleControl(Me, moUseEquipment, False)
                ControlMgr.SetVisibleControl(Me, lblUseEquipment, False)
                ControlMgr.SetVisibleControl(Me, moBestReplacementDrop, False)
                ControlMgr.SetVisibleControl(Me, lblBestReplacement, False)
                ControlMgr.SetVisibleControl(Me, moEquipmentListDrop, False)
                ControlMgr.SetVisibleControl(Me, lblEquipmentList, False)
            End If

            SvcOrderAddressCtr.EnableControls(False, True)
            If Not State.MyBO.UseSvcOrderAddress Then
                DisabledTabsList.Add(Tab_ServiceOrderAddress)
            End If

            If Not State.Ocompany.CertnumlookupbyId.Equals(Guid.Empty) Then
                ControlMgr.SetVisibleControl(Me, lblCertNumLookUpBy, False)
                ControlMgr.SetVisibleControl(Me, ddlCertNumLookUpBy, False)
            Else
                ControlMgr.SetVisibleControl(Me, lblCertNumLookUpBy, True)
                ControlMgr.SetVisibleControl(Me, ddlCertNumLookUpBy, True)
            End If

            If Not State.MyBO.ClaimSystemId.Equals(System.Guid.Empty) AndAlso State.MyBO.ClaimSystemId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_CLAIM_SYSTEM, "ELITA")) Then
                ControlMgr.SetVisibleControl(Me, trClaimRec, True)
            Else
                ControlMgr.SetVisibleControl(Me, trClaimRec, False)
            End If

            'REQ-5314 : Deprecate Dealer Endorsement Flag
            '  ControlMgr.SetVisibleControl(Me, cboEndorsementsId, False)
            ' ControlMgr.SetVisibleControl(Me, lblEndorsementsId, False)

            'WRITE YOU OWN CODE HERE

        End Sub

        Public Sub CheckUseClientDealerCodeFlag()
            'Check the elp_dealer_group table if for the given dealer the use_client_code flag is set to 'yes'
            'If set to 'Y' then don't hide the client dealer code field else hide it.
            Dim dealergrpID As Guid = GetSelectedItem(moDealerGroupDrop)
            If dealergrpID <> Guid.Empty Then
                Dim dealerdrp As New DealerGroup(dealergrpID)
                Dim useClientCodeYN As String = dealerdrp.GetUseClientDealerCodeYN(dealergrpID)
                If dealerdrp.GetUseClientDealerCodeYN(dealergrpID) <> USE_CLIENT_CODE_YES Then
                    ControlMgr.SetVisibleControl(Me, lblClientDealerCode, False)
                    ControlMgr.SetVisibleControl(Me, txtClientDealerCode, False)
                Else
                    ControlMgr.SetVisibleControl(Me, lblClientDealerCode, True)
                    ControlMgr.SetVisibleControl(Me, txtClientDealerCode, True)
                End If
            Else
                ControlMgr.SetVisibleControl(Me, lblClientDealerCode, False)
                ControlMgr.SetVisibleControl(Me, txtClientDealerCode, False)
            End If
        End Sub
        Protected Sub BindBoPropertiesToLabels()
            BindBOPropertyToLabel(State.MyBO, "CompanyId", moMultipleColumnDrop.CaptionLabel)
            'Me.BindBOPropertyToLabel(Me.State.MyBO, "CompanyId", CompanyMultipleDrop.CaptionLabel)
            BindBOPropertyToLabel(State.MyBO, "Dealer", lblDealerCode)
            BindBOPropertyToLabel(State.MyBO, "ClientDealerCode", lblClientDealerCode)
            BindBOPropertyToLabel(State.MyBO, "DealerName", lblDealerName)
            BindBOPropertyToLabel(State.MyBO, "RetailerId", lblDealerIsRetailer)
            BindBOPropertyToLabel(State.MyBO, "TaxIdNumber", lblTaxId)
            BindBOPropertyToLabel(State.MyBO, "DealerGroupId", lblDealerGroupel)
            BindBOPropertyToLabel(State.MyBO, "ContactName", lblContactName)
            BindBOPropertyToLabel(State.MyBO, "ContactPhone", lblContactPhone)
            BindBOPropertyToLabel(State.MyBO, "ContactFax", lblContactFax)
            BindBOPropertyToLabel(State.MyBO, "ContactEmail", lblContactEmail)
            BindBOPropertyToLabel(State.MyBO, "ContactExt", lblContactPhoneExt)
            BindBOPropertyToLabel(State.MyBO, "ServiceNetworkId", lblServiceNetwork)
            BindBOPropertyToLabel(State.MyBO, "IBNRComputationMethodId", lblIBNR_COMPUTATION_METHOD)
            BindBOPropertyToLabel(State.MyBO, "IBNRFactor", lblIBNR_FACTOR)
            BindBOPropertyToLabel(State.MyBO, "STATIBNRComputationMethodId", lblSTATIBNR_COMPUT_MTHD)
            BindBOPropertyToLabel(State.MyBO, "STATIBNRFactor", lblSTATIBNR_FACTOR)
            BindBOPropertyToLabel(State.MyBO, "LAEIBNRComputationMethodId", lblLAEIBNR_COMPUT_MTHD)
            BindBOPropertyToLabel(State.MyBO, "LAEIBNRFactor", lblLAEIBNR_FACTOR)
            BindBOPropertyToLabel(State.MyBO, "ConvertProductCodeId", LabelConvertProdCode)
            BindBOPropertyToLabel(State.MyBO, "DealerTypeId", lblDealerType)
            BindBOPropertyToLabel(State.MyBO, "BranchValidationId", lblBranchValidation)
            BindBOPropertyToLabel(State.MyBO, "RequireCustomerAMLInfoId", lblRequireCustomerAMLInfo)

            'REQ-1297
            BindBOPropertyToLabel(State.MyBO, "UseFullFileProcessId", lblFullfileDealer)
            BindBOPropertyToLabel(State.MyBO, "MaxNCRecords", lblMaxNCRecords)
            'Req-1297 end
            BindBOPropertyToLabel(State.MyBO, "EditBranchId", lblEditBranch)
            BindBOPropertyToLabel(State.MyBO, "DelayFactorFlagId", LabelDelayFactor)
            BindBOPropertyToLabel(State.MyBO, "InstallmentFactorFlagId", LabelInstallmentFactor)
            BindBOPropertyToLabel(State.MyBO, "RegistrationProcessFlagId", moRegistrationProcessLabel)
            BindBOPropertyToLabel(State.MyBO, "RegistrationEmailFrom", moRegistrationEmailFromLabel)
            BindBOPropertyToLabel(State.MyBO, "UseWarrantyMasterID", moUseWarrantyMasterLabel)
            BindBOPropertyToLabel(State.MyBO, "UseIncomingSalesTaxID", moUseIncomingSalesTaxLabel)
            BindBOPropertyToLabel(State.MyBO, "AutoProcessFileID", moUseWarrantyMasterLabel)
            BindBOPropertyToLabel(State.MyBO, "AutoProcessPymtFileID", moAutoProcessPymtFileLabel)
            BindBOPropertyToLabel(State.MyBO, "AutoRejErrTypeId", lblAutoRejErrType)
            BindBOPropertyToLabel(State.MyBO, "ReconRejRecTypeId", lblReconRejRecType)
            BindBOPropertyToLabel(State.MyBO, "PriceMatrixUsesWpId", lblPriceMatrix)
            BindBOPropertyToLabel(State.MyBO, "OlitaSearch", lblOlitaSearch)
            BindBOPropertyToLabel(State.MyBO, "CancellationRequestFlagId", lblCancelRequestFlag)
            BindBOPropertyToLabel(State.MyBO, "MaxManWarr", lblMaxManWarr)
            'Req-877
            BindBOPropertyToLabel(State.MyBO, "MinManWarr", lblMinManWarr)
            BindBOPropertyToLabel(State.MyBO, "BusinessName", lblBusinessName)
            BindBOPropertyToLabel(State.MyBO, "StateTaxIdNumber", lblStateTaxIdNumber)
            BindBOPropertyToLabel(State.MyBO, "CityTaxIdNumber", lblCityTaxIdNumber)
            BindBOPropertyToLabel(State.MyBO, "WebAddress", lblWebAddress)
            BindBOPropertyToLabel(State.MyBO, "NumberOfOtherLocations", lblNumbOfOtherLocations)
            BindBOPropertyToLabel(State.MyBO, "NumberOfOtherLocations", lblNumbOfOtherLocations)
            BindBOPropertyToLabel(State.MyBO, "ManualEnrollmentAllowedId", lblManualEnrollmentAllowed)

            'REQ-5761
            BindBOPropertyToLabel(State.MyBO, "UseNewBillForm", lblUseNewBillPay)

            'Me.BindBOPropertyToLabel(Me.State.MyBO, "ShareCustomers", Me.lblShareCustomers)
            'Me.BindBOPropertyToLabel(Me.State.MyBO, "CustomerIdentityLookup", Me.lblCustomerIdLookUpBy)

            BindBOPropertyToLabel(State.MyBO, "RoundCommFlagId", moRoundCommLabel)
            BindBOPropertyToLabel(State.MyBO, "CertCancelById", moCancelByLabel)
            BindBOPropertyToLabel(State.MyBO, "UseInstallmentDefnId", moUseInstallmentDefnLabel)
            BindBOPropertyToLabel(State.MyBO, "ProgramName", moProgramNameLabel)
            BindBOPropertyToLabel(State.MyBO, "ExpectedPremiumIsWPId", lblExpectedPremiumIsWP)
            BindBOPropertyToLabel(State.MyBO, "ClaimSystemId", lblClaimSystem)
            BindBOPropertyToLabel(State.MyBO, "BestReplacementGroupId", lblBestReplacement)
            BindBOPropertyToLabel(State.MyBO, "UseEquipmentId", lblUseEquipment)
            BindBOPropertyToLabel(State.MyBO, "EquipmentListCode", lblEquipmentList)
            'REQ-860 Elita Buildout - Issues/Adjudication
            BindBOPropertyToLabel(State.MyBO, "QuestionListCode", lblQuestionList)
            'Req-846
            BindBOPropertyToLabel(State.MyBO, "ValidateSKUId", lblskunumber)

            'REQ-874
            BindBOPropertyToLabel(State.MyBO, "ValidateBillingCycleId", lblValidateBillingCycleId)
            BindBOPropertyToLabel(State.MyBO, "ValidateSerialNumberId", lblValidateSerialNumber)
            BindBOPropertyToLabel(State.MyBO, "PayDeductibleId", lblPayDeductible)
            'REQ-1294
            'Me.BindBOPropertyToLabel(Me.State.MyBO, "CustInfoMandatoryId", Me.lblCustInfoMandatory)
            BindBOPropertyToLabel(State.MyBO, "BankInfoMandatoryId", lblBankInfoMandatory)
            BindBOPropertyToLabel(State.MyBO, "DeductibleCollectionId", lblCollectDeductible)
            BindBOPropertyToLabel(State.MyBO, "AssurantIsObligorId", lblAObligor)

            If State.MyBO.UseSvcOrderAddress Then
                BindBOPropertyToLabel(State.SvcOrdersByDealer, "Name", lblName)
                BindBOPropertyToLabel(State.SvcOrdersByDealer, "TaxIdNumber", lblOtherTaxId)
            End If

            'Me.BindBOPropertyToLabel(Me.State.MyBO, "AuthAmtBasedOnId", Me.lblAuthAmtBasedOn)
            BindBOPropertyToLabel(State.MyBO, "ProductByRegionId", lblProdByRegion)
            BindBOPropertyToLabel(State.MyBO, "ClaimVerfificationNumLength", lblClaimVerfificationNumLength)
            BindBOPropertyToLabel(State.MyBO, "ClaimExtendedStatusEntryId", lblClaim_Extended_Status_Entry)
            BindBOPropertyToLabel(State.MyBO, "AllowUpdateCancellationId", lblAlwupdateCancel)
            BindBOPropertyToLabel(State.MyBO, "RejectAfterCancellationId", lblRejectaftercancel)

            BindBOPropertyToLabel(State.MyBO, "AllowUpdateCancellationId", lblAlwupdateCancel)
            BindBOPropertyToLabel(State.MyBO, "RejectAfterCancellationId", lblRejectaftercancel)
            BindBOPropertyToLabel(State.MyBO, "AllowFutureCancelDateId", lblAllowfuturecancel)
            'REQ-1153 
            BindBOPropertyToLabel(State.MyBO, "DealerSupportWebClaimsId", lblDEALER_SUPPORT_WEB_CLAIMS)
            BindBOPropertyToLabel(State.MyBO, "ClaimStatusForExtSystemId", lblCLAIM_STATUS_FOR_EXT_SYSTEM)
            'REQ-1153 end

            'Req 1157
            BindBOPropertyToLabel(State.MyBO, "NewDeviceSkuRequiredId", lblReplacementSKURequired)
            'req 1157
            BindBOPropertyToLabel(State.MyBO, "IsLawsuitMandatoryId", lblIsLawsuitMandatory)


            'REQ-1190
            BindBOPropertyToLabel(State.MyBO, "EnrollfilepreprocessprocId", lblEnrFilePreProcess)
            BindBOPropertyToLabel(State.MyBO, "CertnumlookupbyId", lblCertNumLookUpBy)
            'REQ-1190
            'Req 1157 end
            'Req-1142
            BindBOPropertyToLabel(State.MyBO, "LicenseTagValidationId", lblLicenseTagMandatory)
            'Req-1142 end
            'Req-5723 Start
            BindBOPropertyToLabel(State.MyBO, "VINRestrictMandatoryId", lblVinrestrictMandatory)
            BindBOPropertyToLabel(State.MyBO, "PlanCodeInQuote", lblplancodeinquote)
            'Req-5723 end
            'REQ-1244
            BindBOPropertyToLabel(State.MyBO, "Replaceclaimdedtolerancepct", lblRepClaimDedTolerancePct)

            'REQ_1274
            BindBOPropertyToLabel(State.MyBO, "BillingProcessCodeId", lblBillingProcessCode)
            BindBOPropertyToLabel(State.MyBO, "BillresultExceptionDestId", lblBillResultExpFTPSite)
            BindBOPropertyToLabel(State.MyBO, "BillresultNotificationEmail", lblBillResultNotifyEmail)

            BindBOPropertyToLabel(State.MyBO, "CertificatesAutonumberId", lblCertificatesAutonumberId)
            BindBOPropertyToLabel(State.MyBO, "CertificatesAutonumberPrefix", lblCertificatesAutonumberPrefix)
            BindBOPropertyToLabel(State.MyBO, "MaximumCertNumberLengthAllowed", lblMaxCertNumLengthAlwd)

            BindBOPropertyToLabel(State.MyBO, "FileLoadNotificationEmail", lblFileLoadNotificationEmail)

            '5623
            BindBOPropertyToLabel(State.MyBO, "GracePeriodMonths", lblGracePeriod)
            BindBOPropertyToLabel(State.MyBO, "GracePeriodDays", lblGracePeriod)
            '---end 5623

            'REQ-5466
            BindBOPropertyToLabel(State.MyBO, "AutoSelectServiceCenter", lblAutoSelectSvcCenter)
            'REQ-5586
            BindBOPropertyToLabel(State.MyBO, "PolicyEventNotificationEmail", lblPolicyEventNotifyEmail)

            BindBOPropertyToLabel(State.MyBO, "ClaimAutoApproveId", lblClaimAutoApprove)

            BindBOPropertyToLabel(State.MyBO, "DefaultSalvgeCenterId", moDefaultSalvageCenterLabel)
            BindBOPropertyToLabel(State.MyBO, "ReuseSerialNumberId", lblReuseSerialNumber)
            BindBOPropertyToLabel(State.MyBO, "MaximumCommissionPercent", lblMaxCommissionPercent)
            BindBOPropertyToLabel(State.MyBO, "AutoGenerateRejectedPaymentFileId", lblAutoGenRejPymtFile)
            BindBOPropertyToLabel(State.MyBO, "PaymentRejectedRecordReconcileId", lblPymtRejRecRecon)
            BindBOPropertyToLabel(State.MyBO, "IdentificationNumberType", lblIdentificationNumberType)
            BindBOPropertyToLabel(State.MyBO, "ShareCustomers", lblShareCustomers)
            BindBOPropertyToLabel(State.MyBO, "CustomerLookup", lblCustomerIdLookUpBy)
            BindBOPropertyToLabel(State.MyBO, "UseQuote", lblUseQuote)
            BindBOPropertyToLabel(State.MyBO, "ContractManualVerification", lblContractManualVerification)
            BindBOPropertyToLabel(State.MyBO, "ImeiUseXcd", lblImeiNoUse)
            'REQ-6155
            BindBOPropertyToLabel(State.MyBO, "ClaimRecordingXcd", lblClaimRecording)
            BindBOPropertyToLabel(State.MyBO, "EnableFraudMonitoringId", lblUseFraudMonitoring)
            BindBOPropertyToLabel(State.MyBO, "InvoiceCutoffDay", lblInvoiceCutOffDay)
            BindBOPropertyToLabel(State.MyBO, "BenefitSoldToAccount", lblBenefitSoldToAccount)

            BindBOPropertyToLabel(State.MyBO, "CloseCaseGracePeriodDays", lblClosecaseperiod)
            BindBOPropertyToLabel(State.MyBO, "AllowCertCancellationWithClaimXCd", lblAllowCertCancellationWithClaim)

            'US 489857
            BindBOPropertyToLabel(State.MyBO, "AcctBucketsWithSourceXcd", lblAcctBucketsWithSourceXcd)

            ClearGridViewHeadersAndLabelsErrorSign()
        End Sub

        Protected Sub PopulateDropdowns()

            Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            'Dim yesNoLkL As DataView = LookupListNew.DropdownLookupList("YESNO", langId, True)
            Dim oYesNoList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="YESNO", languageCode:=Thread.CurrentPrincipal.GetLanguageCode())
            Dim oYesNoPayDeductList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="CLAIM_PAY_DEDUCTIBLE", languageCode:=Thread.CurrentPrincipal.GetLanguageCode())
            Dim oYesNoFraudMonitorList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="FRAUD_MONITOR_BY", languageCode:=Thread.CurrentPrincipal.GetLanguageCode())
            Dim textFun As Func(Of DataElements.ListItem, String) = Function(li As DataElements.ListItem)
                                                                        Return li.Code + " - " + li.Translation
                                                                    End Function
            Dim populateOptions As PopulateOptions = New PopulateOptions() With
                    {
                    .AddBlankItem = True
                    }
            Dim populateOptions1 As PopulateOptions = New PopulateOptions() With
                    {
                    .AddBlankItem = False
                    }
            Dim populateOptions2 As PopulateOptions = New PopulateOptions() With
                    {
                    .AddBlankItem = False,
                    .ValueFunc = AddressOf .GetExtendedCode
                    }
            Dim populateOptions3 As PopulateOptions = New PopulateOptions() With
                    {
                    .AddBlankItem = True,
                    .BlankItemValue = String.Empty,
                    .ValueFunc = AddressOf .GetExtendedCode
                    }
            Dim populateOptions4 As PopulateOptions = New PopulateOptions() With
                    {
                    .AddBlankItem = True,
                    .TextFunc = textFun,
                    .SortFunc = textFun
                    }

            'Dim dg As DealerGroup
            'BindListControlToDataView(Me.moDealerGroupDrop, LookupListNew.GetDealerGroupLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id), , , True)
            Dim listcontext1 As ListContext = New ListContext()
            listcontext1.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
            moDealerGroupDrop.Populate(CommonConfigManager.Current.ListManager.GetList("DealerGroupByCompanyGroup", Thread.CurrentPrincipal.GetLanguageCode(), listcontext1), populateOptions)
            'BindListControlToDataView(Me.dlstRetailerID, yesNoLkL)
            dlstRetailerID.Populate(oYesNoList, populateOptions)
            'BindListControlToDataView(Me.moServiceNetworkDrop, LookupListNew.GetServiceNetworkLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id), , , True)
            moServiceNetworkDrop.Populate(CommonConfigManager.Current.ListManager.GetList("ServiceNetworkByCompanyGroup", Thread.CurrentPrincipal.GetLanguageCode(), listcontext1), populateOptions)

            'BindListControlToDataView(Me.moIBNR_COMPUTATION_METHODDropd, LookupListNew.DropdownLookupList(LookupListNew.LK_IBNR_COMPUTE_METHODS, langId, True))
            moIBNR_COMPUTATION_METHODDropd.Populate(CommonConfigManager.Current.ListManager.GetList("IBNR", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions)
            'BindListControlToDataView(Me.moSTATIBNR_COMPUT_MTHDDropd, LookupListNew.DropdownLookupList(LookupListNew.LK_STAT_IBNR_COMPUTE_METHODS, langId, True))
            moSTATIBNR_COMPUT_MTHDDropd.Populate(CommonConfigManager.Current.ListManager.GetList("STAT_IBNR", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions)
            'BindListControlToDataView(Me.moLAEIBNR_COMPUT_MTHDDropd, LookupListNew.DropdownLookupList(LookupListNew.LK_LAE_IBNR_COMPUTE_METHODS, langId, True))
            moLAEIBNR_COMPUT_MTHDDropd.Populate(CommonConfigManager.Current.ListManager.GetList("LAE_IBNR", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions)
            'BindListControlToDataView(Me.moClaimSystemDrop, LookupListNew.GetClaimSystemLookupList())
            moClaimSystemDrop.Populate(CommonConfigManager.Current.ListManager.GetList("ClaimSystem", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions)
            'BindListControlToDataView(Me.moBestReplacementDrop, LookupListNew.GetBestReplacementLookupList())
            moBestReplacementDrop.Populate(CommonConfigManager.Current.ListManager.GetList("BestReplacement", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions)
            'BindListControlToDataView(Me.moEquipmentListDrop, LookupListNew.GetEquipmentListLookupList(DateTime.Now))
            moEquipmentListDrop.Populate(CommonConfigManager.Current.ListManager.GetList("CurrentEquipmentList", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions4)

            'BindListControlToDataView(Me.moUseEquipment, LookupListNew.GetYesNoLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId))
            moUseEquipment.Populate(oYesNoList, populateOptions)

            'Dim flFileProcess As DataView = LookupListNew.DropdownLookupList("FLP", langId, True)
            'BindListControlToDataView(Me.ddlFullfileProcess, flFileProcess, "DESCRIPTION", "ID", False)
            ddlFullfileProcess.Populate(CommonConfigManager.Current.ListManager.GetList("FLP", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions1)
            'BindListControlToDataView(Me.moSkuNumberDrop, LookupListNew.DropdownLookupList(LookupListNew.LK_SKU_VALIDATION_CODE, langId, True))
            moSkuNumberDrop.Populate(CommonConfigManager.Current.ListManager.GetList("SKUVAL", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions)

            'BindListControlToDataView(Me.moValidateBillingCycleIdDrop, yesNoLkL, "DESCRIPTION", "ID", False)
            moValidateBillingCycleIdDrop.Populate(oYesNoList, populateOptions1)
            'BindListControlToDataView(Me.moValidateSerialNumberDrop, LookupListNew.DropdownLookupList(LookupListNew.LK_SERIAL_NUMBER_VALIDATION, langId, True), "DESCRIPTION", "ID", False)
            moValidateSerialNumberDrop.Populate(CommonConfigManager.Current.ListManager.GetList("SNV", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions1)

            'BindListControlToDataView(Me.moUseClaimAutorization, yesNoLkL, "DESCRIPTION", "ID", False)
            moUseClaimAutorization.Populate(oYesNoList, populateOptions1)
            'BindListControlToDataView(Me.moPayDeductible, LookupListNew.GetYesNoLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId))
            moPayDeductible.Populate(oYesNoPayDeductList, populateOptions)
            'BindListControlToDataView(Me.moBankInfoMandatory, LookupListNew.GetYesNoLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), , , False)
            moBankInfoMandatory.Populate(oYesNoList, populateOptions1)
            'BindListControlToDataView(Me.moCollectDeductible, LookupListNew.GetYesNoLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), , , False)
            moCollectDeductible.Populate(oYesNoList, populateOptions1)

            'BindListControlToDataView(Me.cboConvertProdCode, LookupListNew.DropdownLookupList("TPRDC", langId, True))
            cboConvertProdCode.Populate(CommonConfigManager.Current.ListManager.GetList("TPRDC", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions)
            'BindListControlToDataView(Me.moDealerTypeDrop, LookupListNew.DropdownLookupList(LookupListNew.LK_DEALER_TYPE_CODE, langId, True))
            moDealerTypeDrop.Populate(CommonConfigManager.Current.ListManager.GetList("DLTYP", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions)

            'BindListControlToDataView(Me.moBranchValidationDrop, yesNoLkL)
            moBranchValidationDrop.Populate(oYesNoList, populateOptions)
            'BindListControlToDataView(Me.moEditBranchDrop, yesNoLkL)
            moEditBranchDrop.Populate(oYesNoList, populateOptions)
            'BindListControlToDataView(Me.moRoundCommId, yesNoLkL, "DESCRIPTION", "ID", False)
            moRoundCommId.Populate(oYesNoList, populateOptions1)
            'BindListControlToDataView(Me.moDelayFactorDrop, yesNoLkL, "DESCRIPTION", "ID", False)
            moDelayFactorDrop.Populate(oYesNoList, populateOptions1)
            'BindListControlToDataView(Me.moInstallmentFactorDrop, yesNoLkL, "DESCRIPTION", "ID", False)
            moInstallmentFactorDrop.Populate(oYesNoList, populateOptions1)
            'BindListControlToDataView(Me.moRegistrationProcessDrop, yesNoLkL, "DESCRIPTION", "ID", False)
            moRegistrationProcessDrop.Populate(oYesNoList, populateOptions1)
            'BindListControlToDataView(Me.moUseWarrantyMasterDrop, yesNoLkL, "DESCRIPTION", "ID", False)
            moUseWarrantyMasterDrop.Populate(oYesNoList, populateOptions1)
            'BindListControlToDataView(Me.moInsertIfMakeNotExists, yesNoLkL, "DESCRIPTION", "ID", False)
            moInsertIfMakeNotExists.Populate(oYesNoList, populateOptions1)
            'BindListControlToDataView(Me.moUseIncomingSalesTaxDrop, yesNoLkL, "DESCRIPTION", "ID", False)
            moUseIncomingSalesTaxDrop.Populate(oYesNoList, populateOptions1)
            'BindListControlToDataView(Me.moCancelRequestFlagDrop, yesNoLkL, "DESCRIPTION", "ID", False)
            moCancelRequestFlagDrop.Populate(oYesNoList, populateOptions1)
            'BindListControlToDataView(Me.moAutoProcessFileDrop, yesNoLkL, "DESCRIPTION", "ID", False)
            moAutoProcessFileDrop.Populate(oYesNoList, populateOptions1)
            'BindListControlToDataView(Me.moAutoProcessPymtFiledrop, yesNoLkL, "DESCRIPTION", "ID", False)
            moAutoProcessPymtFiledrop.Populate(oYesNoList, populateOptions1)
            'BindListControlToDataView(Me.moPriceMatrixDrop, yesNoLkL)
            moPriceMatrixDrop.Populate(oYesNoList, populateOptions)
            'BindListControlToDataView(Me.ddlInvByBranch, yesNoLkL, "DESCRIPTION", "ID", False)
            ddlInvByBranch.Populate(oYesNoList, populateOptions1)
            'BindListControlToDataView(Me.ddlSeparatedCN, yesNoLkL, "DESCRIPTION", "ID", False)
            ddlSeparatedCN.Populate(oYesNoList, populateOptions1)
            'BindListControlToDataView(Me.moManualEnrollmentAllowedId, yesNoLkL, "DESCRIPTION", "ID", False)
            moManualEnrollmentAllowedId.Populate(oYesNoList, populateOptions1)
            'BindListControlToDataView(Me.cboUseNewBillPay, yesNoLkL, "DESCRIPTION", "ID", False)
            cboUseNewBillPay.Populate(oYesNoList, populateOptions1)

            'Me.cboShareCustomers.PopulateOld("SHARE_CUSTOMER", ListValueType.Description, ListValueType.ExtendedCode, PopulateBehavior.None, String.Empty, ListValueType.Description)
            cboShareCustomers.Populate(CommonConfigManager.Current.ListManager.GetList("SHARE_CUSTOMER", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions2)
            'Me.cboCustomerIdLookUpBy.PopulateOld("DLR_CUST_IDNTY_LOOKUP", ListValueType.Description, ListValueType.ExtendedCode, PopulateBehavior.AddBlankListItem, String.Empty, ListValueType.Description)
            cboCustomerIdLookUpBy.Populate(CommonConfigManager.Current.ListManager.GetList("DLR_CUST_IDNTY_LOOKUP", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions3)
            'Me.cboIdentificationNumberType.PopulateOld("CUSTOMER_IDENTIFICATION_TYPE", ListValueType.Description, ListValueType.ExtendedCode, PopulateBehavior.None, String.Empty, ListValueType.Description)
            cboIdentificationNumberType.Populate(CommonConfigManager.Current.ListManager.GetList("CUSTOMER_IDENTIFICATION_TYPE", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions2)
            'Me.cboUseQuote.PopulateOld("YESNO", ListValueType.Description, ListValueType.ExtendedCode, PopulateBehavior.None, String.Empty, ListValueType.Description)
            cboUseQuote.Populate(oYesNoList, populateOptions2)
            'Me.cboContractManualVerification.PopulateOld("YESNO", ListValueType.Description, ListValueType.ExtendedCode, PopulateBehavior.None, String.Empty, ListValueType.Description)
            cboContractManualVerification.Populate(oYesNoList, populateOptions2)
            'Me.moAcceptPaymentByCheckDrop.PopulateOld("YESNO", ListValueType.Description, ListValueType.ExtendedCode, PopulateBehavior.None, String.Empty, ListValueType.Description)
            moAcceptPaymentByCheckDrop.Populate(oYesNoList, populateOptions2)
            'Me.moClaimRecordingDrop.PopulateOld("CLMREC", ListValueType.Description, ListValueType.ExtendedCode, PopulateBehavior.None, String.Empty, ListValueType.Description)
            moClaimRecordingDrop.Populate(CommonConfigManager.Current.ListManager.GetList("CLMREC", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions2)
            ddlUseFraudMonitoring.Populate(oYesNoFraudMonitorList, populateOptions2)
            'Me.cboImeiNoUse.PopulateOld("IMEI_USE_LST", ListValueType.Description, ListValueType.ExtendedCode, PopulateBehavior.None, String.Empty, ListValueType.Description)
            cboImeiNoUse.Populate(CommonConfigManager.Current.ListManager.GetList("IMEI_USE_LST", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions2)

            'BindListControlToDataView(Me.moDEALER_SUPPORT_WEB_CLAIMS, yesNoLkL, "DESCRIPTION", "ID", False)
            moDEALER_SUPPORT_WEB_CLAIMS.Populate(oYesNoList, populateOptions1)
            'BindListControlToDataView(Me.moExtSystemClaimStatus, LookupListNew.DropdownLookupList(LookupListNew.LK_CLAIM_STATUS, langId, True))
            moExtSystemClaimStatus.Populate(CommonConfigManager.Current.ListManager.GetList("CLSTAT", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions)
            'BindListControlToDataView(Me.moOlitaSearchDrop, LookupListNew.DropdownLookupList(LookupListNew.LK_OLITA_SEARCH, langId, True), , , False)
            moOlitaSearchDrop.Populate(CommonConfigManager.Current.ListManager.GetList("OLTASRCH", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions1)
            'BindListControlToDataView(Me.DDCancelBy, LookupListNew.DropdownLookupList(LookupListNew.LK_CERT_CANCEL_BY, langId, True), , , False)
            DDCancelBy.Populate(CommonConfigManager.Current.ListManager.GetList("CCANBY", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions1)
            'BindListControlToDataView(Me.DDRequireCustomerAMLInfo, LookupListNew.DropdownLookupList(LookupListNew.LK_REQUIRE_CUSTOMER_AML_INFO, langId, True), , , False)
            DDRequireCustomerAMLInfo.Populate(CommonConfigManager.Current.ListManager.GetList("CAIT", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions1)
            'BindListControlToDataView(Me.moUseInstallmentDefnId, LookupListNew.DropdownLookupList(LookupListNew.LK_INSTDEF, langId, True))
            moUseInstallmentDefnId.Populate(CommonConfigManager.Current.ListManager.GetList("INSTDEF", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions)

            'BindListControlToDataView(Me.moExpectedPremiumIsWPDrop, yesNoLkL)
            moExpectedPremiumIsWPDrop.Populate(oYesNoList, populateOptions)
            'BindListControlToDataView(Me.moAObligorDrop, yesNoLkL, , , False)
            moAObligorDrop.Populate(oYesNoList, populateOptions1)
            'BindListControlToDataView(Me.moProdByRegion, yesNoLkL, "DESCRIPTION", "ID", False)
            moProdByRegion.Populate(oYesNoList, populateOptions1)

            'BindListControlToDataView(Me.moClaim_Extended_Status_Entry, LookupListNew.DropdownLookupList(LookupListNew.LK_CLAIM_EXTENDED_STATUS_ENTRY_LIST_CODE, langId, False), , , False)
            moClaim_Extended_Status_Entry.Populate(CommonConfigManager.Current.ListManager.GetList("EXTSTATENT", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions1)

            'BindListControlToDataView(Me.moAlwupdateCancel, yesNoLkL, "DESCRIPTION", "ID", False)
            moAlwupdateCancel.Populate(oYesNoList, populateOptions1)
            'BindListControlToDataView(Me.moRejectaftercancel, yesNoLkL, "DESCRIPTION", "ID", False)
            moRejectaftercancel.Populate(oYesNoList, populateOptions1)
            'BindListControlToDataView(Me.moAllowfuturecancel, yesNoLkL, "DESCRIPTION", "ID", False)
            moAllowfuturecancel.Populate(oYesNoList, populateOptions1)
            'BindListControlToDataView(Me.ddlReplacementSKURequired, yesNoLkL, "DESCRIPTION", "ID", True)
            ddlReplacementSKURequired.Populate(oYesNoList, populateOptions)
            'BindListControlToDataView(Me.moIsLawsuitMandatory, LookupListNew.GetYesNoLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), , , False)
            moIsLawsuitMandatory.Populate(oYesNoList, populateOptions1)

            'BindListControlToDataView(Me.moQuestionListDrop, LookupListNew.GetQuestionListLookupList(DateTime.Now))
            moQuestionListDrop.Populate(CommonConfigManager.Current.ListManager.GetList("CurrentQuestionList", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions4)
            'BindListControlToDataView(Me.moAlwupdateCancel, yesNoLkL, "DESCRIPTION", "ID", False)
            moAlwupdateCancel.Populate(oYesNoList, populateOptions1)
            'BindListControlToDataView(Me.moRejectaftercancel, yesNoLkL, "DESCRIPTION", "ID", False)
            moRejectaftercancel.Populate(oYesNoList, populateOptions1)

            'BindListControlToDataView(Me.ddlEnrFilePreProcess, LookupListNew.DropdownLookupList("EPP", langId, True))
            ddlEnrFilePreProcess.Populate(CommonConfigManager.Current.ListManager.GetList("EPP", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions)
            'BindListControlToDataView(Me.ddlCertNumLookUpBy, LookupListNew.DropdownLookupList("CL", langId, True))
            ddlCertNumLookUpBy.Populate(CommonConfigManager.Current.ListManager.GetList("CL", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions)
            'BindListControlToDataView(Me.ddlAutoRejErrType, LookupListNew.DropdownLookupList("AUTO_REJ_ERR_TYPE", langId, True))
            ddlAutoRejErrType.Populate(CommonConfigManager.Current.ListManager.GetList("AUTO_REJ_ERR_TYPE", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions)
            'BindListControlToDataView(Me.ddlDealerExtractPeriod, LookupListNew.DropdownLookupList("DEALER_EXTRACT_PERIOD", langId, True))
            ddlDealerExtractPeriod.Populate(CommonConfigManager.Current.ListManager.GetList("DEALER_EXTRACT_PERIOD", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions)
            'BindListControlToDataView(Me.ddlReconRejRecType, yesNoLkL, "DESCRIPTION", "ID", False)
            ddlReconRejRecType.Populate(oYesNoList, populateOptions1)

            'BindListControlToDataView(Me.moLicenseTagMandatory, LookupListNew.DropdownLookupList(LookupListNew.LK_LICENSE_TAG_FLAG, langId), , , False)
            moLicenseTagMandatory.Populate(CommonConfigManager.Current.ListManager.GetList("LCNSTAG", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions1)
            'BindListControlToDataView(Me.moVinrestrictMandatory, LookupListNew.DropdownLookupList(LookupListNew.LK_VRSTID, langId), , , True)
            moVinrestrictMandatory.Populate(CommonConfigManager.Current.ListManager.GetList("VRSTID", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions)
            'BindListControlToDataView(Me.moplancodeinquote, LookupListNew.DropdownLookupList(LookupListNew.LK_PLAN_QUOTE_IN_QUOTE_OUTPUT, langId), , , True)
            moplancodeinquote.Populate(CommonConfigManager.Current.ListManager.GetList("PLNQT", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions)
            'BindListControlToDataView(Me.ddlBillingProcessCode, LookupListNew.DropdownLookupList("BILLRESULTPROC", langId, True))
            ddlBillingProcessCode.Populate(CommonConfigManager.Current.ListManager.GetList("BILLRESULTPROC", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions)
            'BindListControlToDataView(Me.ddlBillResultExpFTPSite, LookupListNew.GetFtpSiteLookupList(), , , True)
            ddlBillResultExpFTPSite.Populate(CommonConfigManager.Current.ListManager.GetList("FTPSITE", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions)

            'BindListControlToDataView(Me.cboCertificatesAutonumberId, yesNoLkL)
            cboCertificatesAutonumberId.Populate(oYesNoList, populateOptions)
            'BindListControlToDataView(Me.moAutoSelectServiceCenter, LookupListNew.GetYesNoLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId))
            moAutoSelectServiceCenter.Populate(oYesNoList, populateOptions)

            'BindListControlToDataView(Me.moClaimAutoApproveDrop, yesNoLkL, "DESCRIPTION", "ID", False)
            moClaimAutoApproveDrop.Populate(oYesNoList, populateOptions1)
            'BindListControlToDataView(Me.moReuseSerialNumberDrop, yesNoLkL, "DESCRIPTION", "ID", False)
            moReuseSerialNumberDrop.Populate(oYesNoList, populateOptions1)
            'BindListControlToDataView(Me.ddlAutoGenRejPymtFile, LookupListNew.DropdownLookupList(LookupListNew.LK_AUTO_GEN_REJ_PYMT_FILE, langId), "DESCRIPTION", "ID", True)
            ddlAutoGenRejPymtFile.Populate(CommonConfigManager.Current.ListManager.GetList("AUTO_GEN_REJ_PYMT_FILE", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions)
            'BindListControlToDataView(Me.ddlPymtRejRecRecon, yesNoLkL, "DESCRIPTION", "ID", True)
            ddlPymtRejRecRecon.Populate(oYesNoList, populateOptions)

            'Me.ddlClaimRecordingCheckInventory.PopulateOld("YESNO", ListValueType.Description, ListValueType.ExtendedCode, PopulateBehavior.None, String.Empty, ListValueType.Description)
            ddlClaimRecordingCheckInventory.Populate(oYesNoList, populateOptions2)
            'Me.ddlSuspenseApplies.PopulateOld("YESNO", ListValueType.Description, ListValueType.ExtendedCode, PopulateBehavior.None, String.Empty, ListValueType.Description)
            ddlSuspenseApplies.Populate(oYesNoList, populateOptions2)
            'Me.ddlSourceSystem.PopulateOld("POLICYSYS", ListValueType.Description, ListValueType.ExtendedCode, PopulateBehavior.None, String.Empty, ListValueType.Description)
            ddlSourceSystem.Populate(CommonConfigManager.Current.ListManager.GetList("POLICYSYS", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions2)

            ' KDDI changes '

            cancelShipmentAllowedDrop.Populate(oYesNoList, populateOptions2)
            reshipmentAllowedDrop.Populate(oYesNoList, populateOptions2)
            moValidateAddress.Populate(oYesNoList, populateOptions2)
            moShowPrevCallerInfo.Populate(oYesNoList, populateOptions2)
            moUseTatNotification.Populate(oYesNoList, populateOptions2)
            ddlDealerNameFlag.Populate(oYesNoList, populateOptions3)

            ddlAllowCertCancellationWithClaim.Populate(CommonConfigManager.Current.ListManager.GetList("ALLOW_CERT_CANCELLATION_WITH_CLAIM", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions2)
            'ddlCaseProfile.Populate(CommonConfigManager.Current.ListManager.GetList("CaseProfile", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
            '                           {
            '                           .AddBlankItem = True,
            '                           .BlankItemValue = String.Empty,
            '                           .TextFunc = AddressOf PopulateOptions.GetDescription,
            '                           .ValueFunc = AddressOf PopulateOptions.GetCode
            '                           })

            '# US 489857
            cboAcctBucketsWithSourceXcd.Populate(oYesNoList, populateOptions2)
        End Sub

        Private Sub ClearAll()
            dlstRetailerID.ClearSelection()
            moBranchValidationDrop.ClearSelection()
            moEditBranchDrop.ClearSelection()
            moDelayFactorDrop.ClearSelection()
            moInstallmentFactorDrop.ClearSelection()
            moRegistrationProcessDrop.ClearSelection()
            moDealerGroupDrop.ClearSelection()
            moServiceNetworkDrop.ClearSelection()
            cboConvertProdCode.ClearSelection()
            moPriceMatrixDrop.ClearSelection()
            moDealerTypeDrop.ClearSelection()
            moOlitaSearchDrop.ClearSelection()
            moUseWarrantyMasterDrop.ClearSelection()
            moInsertIfMakeNotExists.ClearSelection()
            moUseIncomingSalesTaxDrop.ClearSelection()
            moAutoProcessFileDrop.ClearSelection()
            moAutoProcessPymtFiledrop.ClearSelection()
            moExpectedPremiumIsWPDrop.ClearSelection()
            moClaimSystemDrop.ClearSelection()
            moBestReplacementDrop.ClearSelection()
            moEquipmentListDrop.ClearSelection()
            moUseEquipment.ClearSelection()
            moQuestionListDrop.ClearSelection()
            moPayDeductible.ClearSelection()
            moCancelRequestFlagDrop.ClearSelection()
            'REQ-1294
            'Me.moCustInfoMandatory.ClearSelection()
            moBankInfoMandatory.ClearSelection()
            moCollectDeductible.ClearSelection()
            moAObligorDrop.ClearSelection()
            ddlCertNumLookUpBy.ClearSelection()
            ddlEnrFilePreProcess.ClearSelection()

            moLicenseTagMandatory.ClearSelection()
            moIBNR_COMPUTATION_METHODDropd.ClearSelection()
            moSTATIBNR_COMPUT_MTHDDropd.ClearSelection()
            moLAEIBNR_COMPUT_MTHDDropd.ClearSelection()
            moSkuNumberDrop.ClearSelection()
            moValidateBillingCycleIdDrop.ClearSelection()
            moValidateSerialNumberDrop.ClearSelection()
            moRoundCommId.ClearSelection()
            ddlInvByBranch.ClearSelection()
            ddlSeparatedCN.ClearSelection()
            moManualEnrollmentAllowedId.ClearSelection()
            moDEALER_SUPPORT_WEB_CLAIMS.ClearSelection()
            moExtSystemClaimStatus.ClearSelection()
            DDCancelBy.ClearSelection()
            DDRequireCustomerAMLInfo.ClearSelection()
            moUseInstallmentDefnId.ClearSelection()
            moProdByRegion.ClearSelection()
            'moAuthAmtBasedOn.ClearSelection()
            moClaim_Extended_Status_Entry.ClearSelection()
            moAllowfuturecancel.ClearSelection()
            moIsLawsuitMandatory.ClearSelection()
            ddlReplacementSKURequired.ClearSelection()
            moAlwupdateCancel.ClearSelection()
            moRejectaftercancel.ClearSelection()
            moUseClaimAutorization.ClearSelection()

            cboCertificatesAutonumberId.ClearSelection()
            moClaimAutoApproveDrop.ClearSelection()
            moReuseSerialNumberDrop.ClearSelection()

            'REQ-5761
            cboUseNewBillPay.ClearSelection()

            'REQ-5932
            cboShareCustomers.ClearSelection()
            cboCustomerIdLookUpBy.ClearSelection()

            ddlAutoRejErrType.ClearSelection()
            ddlReconRejRecType.ClearSelection()
            ddlDealerExtractPeriod.ClearSelection()
            moDefaultSalvageCenter.Text = String.Empty
            txtMaxCommissionPercent.Text = String.Empty
            ddlAutoGenRejPymtFile.ClearSelection()
            ddlPymtRejRecRecon.ClearSelection()

            cboIdentificationNumberType.ClearSelection()

            'REQ
            moClaimRecordingDrop.ClearSelection()
            ddlUseFraudMonitoring.ClearSelection()
            ddlClaimRecordingCheckInventory.ClearSelection()

            'KDDI Changes'

            moValidateAddress.ClearSelection()
            cancelShipmentAllowedDrop.ClearSelection()
            reshipmentAllowedDrop.ClearSelection()

            txtCancelShipmentGracePeriod.Text = String.Empty
            ddlCaseProfile.ClearSelection()
            moShowPrevCallerInfo.ClearSelection()
            moUseTatNotification.ClearSelection()
            ddlDealerNameFlag.ClearSelection()
            ddlAllowCertCancellationWithClaim.ClearSelection()

            'US 489857
            cboAcctBucketsWithSourceXcd.ClearSelection()
        End Sub

        Private Sub SetAssurantIsObligor()
            Dim oCompany As Company
            Dim oComputeTaxBasedId, oComputeTaxBased_CustomAddrId As Guid
            oCompany = New Company(State.MyBO.CompanyId)

            oComputeTaxBased_CustomAddrId = LookupListNew.GetIdFromCode(LookupListNew.DropdownLookupList(
                Codes.COMPUTE_TAX_BASED, Authentication.LangId), Codes.COMPUTE_TAX_BASED_CUSTOMERS_ADDRESS)

            If (oCompany.ComputeTaxBasedId.Equals(oComputeTaxBased_CustomAddrId)) Then
                ' Yes => Invisible
                'SetSelectedItem(Me.moAObligorDrop, _
                '    LookupListNew.GetIdFromCode(LookupListNew.DropdownLookupList( _
                '                Codes.YESNO, ElitaPlusIdentity.Current.ActiveUser.LanguageId), Codes.YESNO_Y))
                ControlMgr.SetVisibleControl(Me, lblAObligor, False)
                ControlMgr.SetVisibleControl(Me, moAObligorDrop, False)
            Else    ' No => Visible
                ControlMgr.SetVisibleControl(Me, lblAObligor, True)
                ControlMgr.SetVisibleControl(Me, moAObligorDrop, True)
            End If

            If State.MyBO.AssurantIsObligorId.Equals(System.Guid.Empty) Then
                ' Default
                SetSelectedItem(moAObligorDrop,
                                LookupListNew.GetIdFromCode(LookupListNew.DropdownLookupList(
                                    Codes.YESNO, ElitaPlusIdentity.Current.ActiveUser.LanguageId), Codes.YESNO_Y))
            Else
                SetSelectedItem(moAObligorDrop, State.MyBO.AssurantIsObligorId)
            End If
        End Sub

        Protected Sub PopulateFormFromBOs()
            ClearAll()
            With State.MyBO

                If ElitaPlusIdentity.Current.ActiveUser.Companies.Count > 1 AndAlso State.MyBO.IsNew Then
                    'CompanyMultipleDrop.SelectedGuid = .CompanyId
                    moMultipleColumnDrop.SelectedGuid = .CompanyId
                Else
                    'CompanyMultipleDrop.CodeTextBox.Text = LookupListNew.GetCodeFromId(Me.State.companyDV, .CompanyId)
                    'CompanyMultipleDrop.DescriptionTextBox.Text = LookupListNew.GetDescriptionFromId(Me.State.companyDV, .CompanyId)
                    moMultipleColumnDrop.CodeTextBox.Text = LookupListNew.GetCodeFromId(State.companyDV, .CompanyId)
                    moMultipleColumnDrop.DescriptionTextBox.Text = LookupListNew.GetDescriptionFromId(State.companyDV, .CompanyId)
                End If

                PopulateControlFromBOProperty(txtTaxIdNumber, .TaxIdNumber)
                PopulateControlFromBOProperty(txtDealerCode, .Dealer)
                PopulateControlFromBOProperty(txtClientDealerCode, .ClientDealerCode)
                PopulateControlFromBOProperty(txtDealerName, .DealerName)
                PopulateControlFromBOProperty(txtContactName, .ContactName)
                PopulateControlFromBOProperty(txtContactExt, .ContactExt)
                PopulateControlFromBOProperty(txtContactEmail, .ContactEmail)
                PopulateControlFromBOProperty(txtContactPhone, .ContactPhone)
                PopulateControlFromBOProperty(txtContactFax, .ContactFax)
                PopulateControlFromBOProperty(txtMaxManWarr, .MaxManWarr)
                '5623
                PopulateControlFromBOProperty(txtGracePeriodMonths, .GracePeriodMonths)
                PopulateControlFromBOProperty(txtGracePeriodDays, .GracePeriodDays)
                '---5623

                'Req-877
                PopulateControlFromBOProperty(txtMinManWarr, .MinManWarr)

                'REQ-1297
                BindSelectItem(State.MyBO.UseFullFileProcessId.ToString, ddlFullfileProcess)
                PopulateControlFromBOProperty(txtMaxNCRecords, .MaxNCRecords)
                'REQ-1297 End

                PopulateControlFromBOProperty(txtServLineEmail, .ServiceLineEmail)
                PopulateControlFromBOProperty(txtServLinePhoneNum, .ServiceLinePhone)
                PopulateControlFromBOProperty(txtServLineFax, .ServiceLineFax)
                PopulateControlFromBOProperty(txtESCInsuranceLable, .EscInsuranceLabel)

                PopulateControlFromBOProperty(txtIBNR_Factor, .IBNRFactor, PERCENT_FORMAT)
                PopulateControlFromBOProperty(txtSTATIBNR_Factor, .STATIBNRFactor, PERCENT_FORMAT)
                PopulateControlFromBOProperty(txtLAEIBNR_Factor, .LAEIBNRFactor, PERCENT_FORMAT)

                SetSelectedItem(moIBNR_COMPUTATION_METHODDropd, .IBNRComputationMethodId)
                SetSelectedItem(moSTATIBNR_COMPUT_MTHDDropd, .STATIBNRComputationMethodId)
                SetSelectedItem(moLAEIBNR_COMPUT_MTHDDropd, .LAEIBNRComputationMethodId)
                SetSelectedItem(moClaimSystemDrop, .ClaimSystemId)
                SetSelectedItem(moBestReplacementDrop, .BestReplacementGroupId)
                SetSelectedItem(moEquipmentListDrop, LookupListNew.GetIdFromCode(LookupListNew.GetEquipmentListLookupList(DateTime.Now), .EquipmentListCode))
                'REQ-860 Elita Buildout - Issues/Adjudication
                SetSelectedItem(moQuestionListDrop, LookupListNew.GetIdFromCode(LookupListNew.GetQuestionListLookupList(DateTime.Now), .QuestionListCode))
                SetSelectedItem(moUseEquipment, .UseEquipmentId)
                SetSelectedItem(moPayDeductible, .PayDeductibleId)
                'REQ-1294
                'SetSelectedItem(Me.moCustInfoMandatory, .CustInfoMandatoryId)
                SetSelectedItem(moBankInfoMandatory, .BankInfoMandatoryId)
                SetSelectedItem(moCollectDeductible, .DeductibleCollectionId)


                If Not State.MyBO.CancellationRequestFlagId.Equals(Guid.Empty) Then
                    SetSelectedItem(moCancelRequestFlagDrop, .CancellationRequestFlagId)
                End If

                PopulateControlFromBOProperty(txtBusinessName, .BusinessName)
                PopulateControlFromBOProperty(txtStateTaxIdNumber, .StateTaxIdNumber)
                PopulateControlFromBOProperty(txtCityTaxIdNumber, .CityTaxIdNumber)
                PopulateControlFromBOProperty(txtWebAddress, .WebAddress)
                PopulateControlFromBOProperty(txtNumbOfOtherLocations, .NumberOfOtherLocations)
                PopulateControlFromBOProperty(moRegistrationEmailFromText, .RegistrationEmailFrom)
                PopulateControlFromBOProperty(txtProgramName, .ProgramName)
                PopulateControlFromBOProperty(txtClaimVerfificationNumLength, .ClaimVerfificationNumLength)

                BindSelectItem(State.MyBO.DealerGroupId.ToString, moDealerGroupDrop)
                BindSelectItem(State.MyBO.RetailerId.ToString, dlstRetailerID)
                BindSelectItem(State.MyBO.ServiceNetworkId.ToString, moServiceNetworkDrop)
                BindSelectItem(State.MyBO.ConvertProductCodeId.ToString, cboConvertProdCode)
                BindSelectItem(State.MyBO.DealerTypeId.ToString, moDealerTypeDrop)
                BindSelectItem(State.MyBO.OlitaSearch.ToString, moOlitaSearchDrop)
                BindSelectItem(State.MyBO.BranchValidationId.ToString, moBranchValidationDrop)
                BindSelectItem(State.MyBO.EditBranchId.ToString, moEditBranchDrop)
                BindSelectItem(State.MyBO.DelayFactorFlagId.ToString, moDelayFactorDrop)
                BindSelectItem(State.MyBO.RoundCommFlagId.ToString, moRoundCommId)
                BindSelectItem(State.MyBO.InstallmentFactorFlagId.ToString, moInstallmentFactorDrop)
                BindSelectItem(State.MyBO.RegistrationProcessFlagId.ToString, moRegistrationProcessDrop)
                BindSelectItem(State.MyBO.PriceMatrixUsesWpId.ToString, moPriceMatrixDrop)
                BindSelectItem(State.MyBO.UseWarrantyMasterID.ToString, moUseWarrantyMasterDrop)
                BindSelectItem(State.MyBO.InsertIfMakeNotExists.ToString, moInsertIfMakeNotExists)
                BindSelectItem(State.MyBO.UseIncomingSalesTaxID.ToString, moUseIncomingSalesTaxDrop)
                BindSelectItem(State.MyBO.AutoProcessFileID.ToString, moAutoProcessFileDrop)
                BindSelectItem(State.MyBO.AutoProcessPymtFileID.ToString, moAutoProcessPymtFiledrop)
                BindSelectItem(State.MyBO.SeparatedCreditNotesId.ToString, ddlSeparatedCN)
                'Req 846
                BindSelectItem(State.MyBO.ValidateSKUId.ToString, moSkuNumberDrop)

                'REQ-874
                BindSelectItem(State.MyBO.ValidateBillingCycleId.ToString, moValidateBillingCycleIdDrop)
                BindSelectItem(State.MyBO.ValidateSerialNumberId.ToString, moValidateSerialNumberDrop)
                BindSelectItem(State.MyBO.ManualEnrollmentAllowedId.ToString, moManualEnrollmentAllowedId)
                'REQ-5761
                BindSelectItem(State.MyBO.UseNewBillForm.ToString, cboUseNewBillPay)

                'REQ-5932
                BindSelectItem(State.MyBO.ShareCustomers, cboShareCustomers)
                'If Me.cboShareCustomers.SelectedValue = SHARE_CUSTOMER Then
                '    Me.cboCustomerIdLookUpBy.Enabled = False
                'End If
                BindSelectItem(State.MyBO.CustomerLookup, cboCustomerIdLookUpBy)
                BindSelectItem(State.MyBO.InvoiceByBranchId.ToString, ddlInvByBranch)
                BindSelectItem(State.MyBO.CertCancelById.ToString, DDCancelBy)
                BindSelectItem(State.MyBO.RequireCustomerAMLInfoId.ToString, DDRequireCustomerAMLInfo)
                'Me.moUseInstallmentDefnId.ClearSelection()
                BindSelectItem(State.MyBO.UseInstallmentDefnId.ToString, moUseInstallmentDefnId)

                BindSelectItem(State.MyBO.ExpectedPremiumIsWPId.ToString, moExpectedPremiumIsWPDrop)

                If State.MyBO.DealerTypeDesc = State.MyBO.DEALER_TYPE_DESC Then
                    State.MyBO.Address.AddressIsRequire = True
                End If

                MailingAddressCtr.Bind(State.MyBO.MailingAddress)
                AddressCtr.Bind(State.MyBO.Address)

                ' Dim _bankinfo As BankInfo
                If State.MyBO.BankInfoId <> Guid.Empty Then
                    moBankInfo.State.myBankInfoBo = New BankInfo(State.MyBO.BankInfoId)
                    moBankInfo.State.myBankInfoBo.SourceCountryID = State.DealerCountryID
                    ' moBankInfo.Bind(New BankInfo(State.MyBO.BankInfoId)) ', CType(Me.MasterPage.MessageController, ErrorController))
                Else
                    moBankInfo.State.myBankInfoBo = New BankInfo()
                    moBankInfo.State.myBankInfoBo.SourceCountryID = State.DealerCountryID
                    moBankInfo.State.myBankInfoBo.CountryID = State.DealerCountryID
                    '    moBankInfo.Bind(New BankInfo) ', CType(Me.MasterPage.MessageController, ErrorController))
                End If
                moBankInfo.SetTheRequiredFields()
                moBankInfo.Bind(moBankInfo.State.myBankInfoBo)

                If State.MyBO.UseSvcOrderAddress Then
                    State.SvcOrdersByDealer = State.MyBO.SvcOrdersAddress
                    PopulateControlFromBOProperty(txtName, State.SvcOrdersByDealer.Name)
                    PopulateControlFromBOProperty(txtTaxId, State.SvcOrdersByDealer.TaxIdNumber)
                    State.SvcOrdersByDealer.CompanyId = .CompanyId
                    SvcOrderAddressCtr.Bind(State.SvcOrdersByDealer.Address)
                End If
                SetAssurantIsObligor()

                'SetSelectedItem(Me.moAuthAmtBasedOn, .AuthAmtBasedOnId)
                BindSelectItem(State.MyBO.ProductByRegionId.ToString, moProdByRegion)

                PopulateControlFromBOProperty(moClaim_Extended_Status_Entry, .ClaimExtendedStatusEntryId)

                'Req-1000
                BindSelectItem(State.MyBO.AllowUpdateCancellationId.ToString, moAlwupdateCancel)
                BindSelectItem(State.MyBO.RejectAfterCancellationId.ToString, moRejectaftercancel)
                BindSelectItem(State.MyBO.AllowFutureCancelDateId.ToString, moAllowfuturecancel)

                BindSelectItem(State.MyBO.IsLawsuitMandatoryId.ToString, moIsLawsuitMandatory)

                'REQ-1153 
                BindSelectItem(State.MyBO.DealerSupportWebClaimsId.ToString, moDEALER_SUPPORT_WEB_CLAIMS)
                BindSelectItem(State.MyBO.ClaimStatusForExtSystemId.ToString, moExtSystemClaimStatus)
                'REQ-1153 end

                'req 1157
                BindSelectItem(State.MyBO.NewDeviceSkuRequiredId.ToString, ddlReplacementSKURequired)
                BindSelectItem(State.MyBO.UseClaimAuthorizationId.ToString, moUseClaimAutorization)
                'Req-1142
                If (moLicenseTagMandatory.Visible) Then
                    BindSelectItem(State.MyBO.LicenseTagValidationId.ToString, moLicenseTagMandatory)
                End If
                'Req-5723 start
                If (moVinrestrictMandatory.Visible) Then
                    BindSelectItem(State.MyBO.VINRestrictMandatoryId.ToString, moVinrestrictMandatory)
                End If
                If (moplancodeinquote.Visible) Then
                    BindSelectItem(State.MyBO.PlanCodeInQuote.ToString, moplancodeinquote)
                End If
                'Req-5723 End 
                'REQ-1190
                BindSelectItem(State.MyBO.CertnumlookupbyId.ToString, ddlCertNumLookUpBy)
                BindSelectItem(State.MyBO.EnrollfilepreprocessprocId.ToString, ddlEnrFilePreProcess)
                'REQ-1190
                BindSelectItem(State.MyBO.AutoRejErrTypeId.ToString, ddlAutoRejErrType)
                BindSelectItem(State.MyBO.ReconRejRecTypeId.ToString, ddlReconRejRecType)
                BindSelectItem(State.MyBO.DealerExtractPeriodId.ToString, ddlDealerExtractPeriod)

                'REQ-1244
                PopulateControlFromBOProperty(txtRepClaimDedTolerancePct, .Replaceclaimdedtolerancepct)

                'REQ-1274
                BindSelectItem(State.MyBO.BillingProcessCodeId.ToString, ddlBillingProcessCode)
                BindSelectItem(State.MyBO.BillresultExceptionDestId.ToString, ddlBillResultExpFTPSite)
                PopulateControlFromBOProperty(txtBillResultNotifyEmail, .BillresultNotificationEmail)

                PopulateControlFromBOProperty(cboCertificatesAutonumberId, .CertificatesAutonumberId)
                PopulateControlFromBOProperty(txtCertificatesAutonumberPrefix, .CertificatesAutonumberPrefix)
                PopulateControlFromBOProperty(txtMaxCertNumLengthAlwd, .MaximumCertNumberLengthAllowed)

                PopulateControlFromBOProperty(txtFileLoadNotificationEmail, .FileLoadNotificationEmail)
                PopulateControlFromBOProperty(txtPolicyEventNotifiyEmail, .PolicyEventNotificationEmail)

                'Save the Lawsuit Value at the time of loading the Dealer Form
                State.LawsuitMandatoryIdAtLoad = .IsLawsuitMandatoryId
                'REQ-5466
                SetSelectedItem(moAutoSelectServiceCenter, .AutoSelectServiceCenter)

                'REQ-5598
                '''''claim close rules control
                If (Not State.blnIsComingFromCopy) Then
                    ClaimCloseRules.CompanyId = State.Ocompany.Id
                    ClaimCloseRules.DealerId = State.MyBO.Id
                    ClaimCloseRules.companyCode = State.Ocompany.Code
                    ClaimCloseRules.Dealer = State.MyBO.Dealer
                    ClaimCloseRules.Populate()
                End If
                
                'Load Dealer Inflation user control
                DealerInflation.DealerId =State.MyBO.Id
                DealerInflation.Dealer =State.MyBO.Dealer
                DealerInflation.Populate()

                'Load Risk Type Tolerance
                RiskTypeTolerance.DealerId = State.MyBO.Id
                RiskTypeTolerance.Dealer = State.MyBO.Dealer
                RiskTypeTolerance.Populate()

                If Not State.MyBO.ClaimAutoApproveId.Equals(Guid.Empty) Then
                    PopulateControlFromBOProperty(moClaimAutoApproveDrop, .ClaimAutoApproveId)
                End If

                If Not State.MyBO.ReuseSerialNumberId.Equals(Guid.Empty) Then
                    PopulateControlFromBOProperty(moReuseSerialNumberDrop, .ReuseSerialNumberId)
                End If

                If .ClaimAutoApproveId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_Y)) Then
                    'ControlMgr.SetVisibleControl(Me, Me.ClaimTypesRow, True)
                    'ControlMgr.SetVisibleControl(Me, Me.CoverageTypesRow, True)
                    ControlMgr.SetVisibleControl(Me, pnlTypesRow, True)
                    PopulateClaimAutoApproveControls()
                Else
                    'ControlMgr.SetVisibleControl(Me, Me.ClaimTypesRow, False)
                    'ControlMgr.SetVisibleControl(Me, Me.CoverageTypesRow, False)
                    ControlMgr.SetVisibleControl(Me, pnlTypesRow, False)
                End If
                inputServiceCenterId.Value = .DefaultSalvgeCenterId.ToString()
                inputServiceCenterDesc.Value = LookupListNew.GetDescriptionFromId(LookupListNew.LK_SERVICE_CENTERS, .DefaultSalvgeCenterId)
                moDefaultSalvageCenter.Text = inputServiceCenterDesc.Value
                PopulateControlFromBOProperty(txtMaxCommissionPercent, .MaximumCommissionPercent, PERCENT_FORMAT)
                BindSelectItem(State.MyBO.AutoGenerateRejectedPaymentFileId.ToString, ddlAutoGenRejPymtFile)
                BindSelectItem(State.MyBO.PaymentRejectedRecordReconcileId.ToString, ddlPymtRejRecRecon)

                BindSelectItem(State.MyBO.IdentificationNumberType, cboIdentificationNumberType)
                BindSelectItem(State.MyBO.UseQuote, cboUseQuote)
                BindSelectItem(State.MyBO.ContractManualVerification, cboContractManualVerification)
                'REQ-6406
                BindSelectItem(State.MyBO.AcceptPaymentByCheck, moAcceptPaymentByCheckDrop)

                BindSelectItem(State.MyBO.ClaimRecordingXcd, moClaimRecordingDrop)
                BindSelectItem(State.MyBO.UseFraudMonitoringXcd, ddlUseFraudMonitoring)
                'REQ 6156
                BindSelectItem(State.MyBO.ImeiUseXcd, cboImeiNoUse)

                BindSelectItem(.ClaimRecordingCheckInventoryXcd, ddlClaimRecordingCheckInventory)

                'KDDI Changes'
                BindSelectItem(State.MyBO.Is_Cancel_Shipment_Allowed, cancelShipmentAllowedDrop)
                BindSelectItem(State.MyBO.Is_Reshipment_Allowed, reshipmentAllowedDrop)
                BindSelectItem(State.MyBO.Validate_Address, moValidateAddress)

                PopulateControlFromBOProperty(txtClosecaseperiod, .CloseCaseGracePeriodDays)

                BindSelectItem(State.MyBO.Show_Previous_Caller_Info, moShowPrevCallerInfo)

                BindSelectItem(State.MyBO.UseTurnaroundTimeNotification, moUseTatNotification)
               
                BindSelectItem(State.MyBO.DisplayDobXcd, ddlDealerNameFlag)

                PopulateControlFromBOProperty(txtCancelShipmentGracePeriod, .Cancel_Shipment_Grace_Period)

                BindSelectItem(.CaseProfileCode, ddlCaseProfile)

                BindSelectItem(State.MyBO.AllowCertCancellationWithClaimXCd, ddlAllowCertCancellationWithClaim)

                'US 489857
                BindSelectItem(State.MyBO.AcctBucketsWithSourceXcd, cboAcctBucketsWithSourceXcd)

                ControlMgr.SetVisibleControl(Me, trBenefitDlrTypeCtls4, True)
                PopulateControlFromBOProperty(txtBenefitSoldToAccount, .BenefitSoldToAccount)

                If (.DealerTypeDesc IsNot Nothing AndAlso .DealerTypeDesc.ToUpper() = State.MyBO.DEALER_TYPE_BENEFIT) Then
                    ControlMgr.SetVisibleControl(Me, trBenefitDlrTypeCtls1, True)
                    ControlMgr.SetVisibleControl(Me, trBenefitDlrTypeCtls2, True)
                    ControlMgr.SetVisibleControl(Me, trBenefitDlrTypeCtls3, True)


                    BindSelectItem(.SuspendAppliesXcd, ddlSuspenseApplies)
                    BindSelectItem(.SourceSystemXcd, ddlSourceSystem)

                    PopulateControlFromBOProperty(txtVoidDuration, .VoidDuration)
                    PopulateControlFromBOProperty(txtInvCutOffDay, .InvoiceCutoffDay)
                    PopulateControlFromBOProperty(txtBenefitCarrierCode, .BenefitCarrierCode)


                    If (ddlSuspenseApplies.SelectedItem.Value.ToUpper() = "YESNO-Y") Then
                        PopulateControlFromBOProperty(txtSuspensePeriod, State.MyBO.SuspendPeriod)
                        txtSuspensePeriod.Style.Add("display", "block")
                        lblSuspensePeriod.Style.Add("display", "block")
                    Else
                        txtSuspensePeriod.Style.Add("display", "none")
                        lblSuspensePeriod.Style.Add("display", "none")
                    End If

                Else
                    ControlMgr.SetVisibleControl(Me, trBenefitDlrTypeCtls1, False)
                    ControlMgr.SetVisibleControl(Me, trBenefitDlrTypeCtls2, False)
                    ControlMgr.SetVisibleControl(Me, trBenefitDlrTypeCtls3, False)
                End If

            End With

            ' Populate Attributes
            If (AttributeValues.ParentBusinessObject Is Nothing) Then
                AttributeValues.ParentBusinessObject = CType(State.MyBO, IAttributable)
                AttributeValues.TranslateHeaders()
            End If
            AttributeValues.DataBind()



        End Sub

        Protected Sub PopulateBOsFromForm()

            With State.MyBO

                PopulateBOProperty(State.MyBO, "Dealer", txtDealerCode)
                PopulateBOProperty(State.MyBO, "ClientDealerCode", txtClientDealerCode)
                PopulateBOProperty(State.MyBO, "DealerName", txtDealerName)
                PopulateBOProperty(State.MyBO, "RetailerId", dlstRetailerID)
                PopulateBOProperty(State.MyBO, "TaxIdNumber", txtTaxIdNumber)
                PopulateBOProperty(State.MyBO, "DealerGroupId", moDealerGroupDrop)
                PopulateBOProperty(State.MyBO, "DealerTypeId", moDealerTypeDrop)
                PopulateBOProperty(State.MyBO, "OlitaSearch", moOlitaSearchDrop)
                PopulateBOProperty(State.MyBO, "CancellationRequestFlagId", moCancelRequestFlagDrop)
                PopulateBOProperty(State.MyBO, "BranchValidationId", moBranchValidationDrop)
                PopulateBOProperty(State.MyBO, "RoundCommFlagId", moRoundCommId)
                PopulateBOProperty(State.MyBO, "EditBranchId", moEditBranchDrop)
                PopulateBOProperty(State.MyBO, "DelayFactorFlagId", moDelayFactorDrop)
                PopulateBOProperty(State.MyBO, "InstallmentFactorFlagId", moInstallmentFactorDrop)
                PopulateBOProperty(State.MyBO, "RegistrationProcessFlagId", moRegistrationProcessDrop)
                PopulateBOProperty(State.MyBO, "RegistrationEmailFrom", moRegistrationEmailFromText)
                PopulateBOProperty(State.MyBO, "UseWarrantyMasterID", moUseWarrantyMasterDrop)
                PopulateBOProperty(State.MyBO, "InsertIfMakeNotExists", moInsertIfMakeNotExists)

                PopulateBOProperty(State.MyBO, "UseIncomingSalesTaxID", moUseIncomingSalesTaxDrop)
                PopulateBOProperty(State.MyBO, "AutoProcessFileID", moAutoProcessFileDrop)
                PopulateBOProperty(State.MyBO, "AutoProcessPymtFileID", moAutoProcessPymtFiledrop)
                PopulateBOProperty(State.MyBO, "AutoRejErrTypeId", ddlAutoRejErrType)
                PopulateBOProperty(State.MyBO, "ReconRejRecTypeId", ddlReconRejRecType)
                PopulateBOProperty(State.MyBO, "DealerExtractPeriodId", ddlDealerExtractPeriod)

                '5623
                PopulateBOProperty(State.MyBO, "GracePeriodMonths", txtGracePeriodMonths)
                PopulateBOProperty(State.MyBO, "GracePeriodDays", txtGracePeriodDays)
                '----end 5623

                PopulateBOProperty(State.MyBO, "ContactName", txtContactName)
                'Req-1297
                PopulateBOProperty(State.MyBO, "UseFullFileProcessId", ddlFullfileProcess)
                PopulateBOProperty(State.MyBO, "MaxNCRecords", txtMaxNCRecords)
                'Req-1297 End

                PopulateBOProperty(State.MyBO, "ContactPhone", txtContactPhone)
                PopulateBOProperty(State.MyBO, "ContactFax", txtContactFax)
                PopulateBOProperty(State.MyBO, "ContactEmail", txtContactEmail)
                PopulateBOProperty(State.MyBO, "ContactExt", txtContactExt)
                PopulateBOProperty(State.MyBO, "ServiceNetworkId", moServiceNetworkDrop)
                PopulateBOProperty(State.MyBO, "ConvertProductCodeId", cboConvertProdCode)
                PopulateBOProperty(State.MyBO, "MaxManWarr", txtMaxManWarr)
                'Req-877
                PopulateBOProperty(State.MyBO, "MinManWarr", txtMinManWarr)

                PopulateBOProperty(State.MyBO, "EscInsuranceLabel", txtESCInsuranceLable)
                PopulateBOProperty(State.MyBO, "ServiceLinePhone", txtServLinePhoneNum)
                PopulateBOProperty(State.MyBO, "ServiceLineFax", txtServLineFax)
                PopulateBOProperty(State.MyBO, "ServiceLineEmail", txtServLineEmail)

                PopulateBOProperty(State.MyBO, "PriceMatrixUsesWpId", moPriceMatrixDrop)
                PopulateBOProperty(State.MyBO, "BusinessName", txtBusinessName)
                PopulateBOProperty(State.MyBO, "StateTaxIdNumber", txtStateTaxIdNumber)
                PopulateBOProperty(State.MyBO, "CityTaxIdNumber", txtCityTaxIdNumber)
                PopulateBOProperty(State.MyBO, "WebAddress", txtWebAddress)
                PopulateBOProperty(State.MyBO, "NumberOfOtherLocations", txtNumbOfOtherLocations)
                PopulateBOProperty(State.MyBO, "ProgramName", txtProgramName)

                PopulateBOProperty(State.MyBO, "InvoiceByBranchId", ddlInvByBranch)
                PopulateBOProperty(State.MyBO, "SeparatedCreditNotesId", ddlSeparatedCN)
                PopulateBOProperty(State.MyBO, "ManualEnrollmentAllowedId", moManualEnrollmentAllowedId)

                'REQ-5761
                PopulateBOProperty(State.MyBO, "UseNewBillForm", cboUseNewBillPay)

                'REQ-5932
                PopulateBOProperty(State.MyBO, "ShareCustomers", cboShareCustomers, False, True)

                If Not cboCustomerIdLookUpBy.SelectedItem.Text = String.Empty Then
                    PopulateBOProperty(State.MyBO, "CustomerLookup", cboCustomerIdLookUpBy, False, True)
                End If

                PopulateBOProperty(State.MyBO, "CertCancelById", DDCancelBy)
                PopulateBOProperty(State.MyBO, "RequireCustomerAMLInfoId", DDRequireCustomerAMLInfo)
                .ActiveFlag = "Y"

                PopulateBOProperty(State.MyBO, "IBNRFactor", txtIBNR_Factor)
                PopulateBOProperty(State.MyBO, "IBNRComputationMethodId", moIBNR_COMPUTATION_METHODDropd)
                PopulateBOProperty(State.MyBO, "STATIBNRFactor", txtSTATIBNR_Factor)
                PopulateBOProperty(State.MyBO, "STATIBNRComputationMethodId", moSTATIBNR_COMPUT_MTHDDropd)
                PopulateBOProperty(State.MyBO, "LAEIBNRFactor", txtLAEIBNR_Factor)
                PopulateBOProperty(State.MyBO, "LAEIBNRComputationMethodId", moLAEIBNR_COMPUT_MTHDDropd)
                PopulateBOProperty(State.MyBO, "UseInstallmentDefnId", moUseInstallmentDefnId)

                PopulateBOProperty(State.MyBO, "ExpectedPremiumIsWPId", moExpectedPremiumIsWPDrop)
                PopulateBOProperty(State.MyBO, "ClaimSystemId", moClaimSystemDrop)
                PopulateBOProperty(State.MyBO, "BestReplacementGroupId", moBestReplacementDrop)
                PopulateBOProperty(State.MyBO, "EquipmentListCode", LookupListNew.GetCodeFromId(LookupListNew.GetEquipmentListLookupList(DateTime.Now), New Guid(moEquipmentListDrop.SelectedValue)))
                'REQ-860 Elita Buildout - Issues/Adjudication
                PopulateBOProperty(State.MyBO, "QuestionListCode", LookupListNew.GetCodeFromId(LookupListNew.GetQuestionListLookupList(DateTime.Now), New Guid(moQuestionListDrop.SelectedValue)))
                PopulateBOProperty(State.MyBO, "UseEquipmentId", moUseEquipment)
                'Req 846
                PopulateBOProperty(State.MyBO, "ValidateSKUId", moSkuNumberDrop)




                'REQ-874
                PopulateBOProperty(State.MyBO, "ValidateBillingCycleId", moValidateBillingCycleIdDrop)
                PopulateBOProperty(State.MyBO, "ValidateSerialNumberId", moValidateSerialNumberDrop)
                PopulateBOProperty(State.MyBO, "PayDeductibleId", moPayDeductible)
                'REQ-1294
                'Me.PopulateBOProperty(Me.State.MyBO, "CustInfoMandatoryId", Me.moCustInfoMandatory)
                PopulateBOProperty(State.MyBO, "BankInfoMandatoryId", moBankInfoMandatory)
                PopulateBOProperty(State.MyBO, "DeductibleCollectionId", moCollectDeductible)

                PopulateBOProperty(State.MyBO, "AssurantIsObligorId", moAObligorDrop)

                PopulateBOProperty(State.MyBO, "CertificatesAutonumberId", cboCertificatesAutonumberId)
                PopulateBOProperty(State.MyBO, "ClaimAutoApproveId", moClaimAutoApproveDrop)
                PopulateBOProperty(State.MyBO, "ReuseSerialNumberId", moReuseSerialNumberDrop)
                PopulateBOProperty(State.MyBO, "CertificatesAutonumberPrefix", txtCertificatesAutonumberPrefix)
                PopulateBOProperty(State.MyBO, "MaximumCertNumberLengthAllowed", txtMaxCertNumLengthAlwd)

                PopulateBOProperty(State.MyBO, "FileLoadNotificationEmail", txtFileLoadNotificationEmail)

                If State.MyBO.IsNew Then
                    If ElitaPlusIdentity.Current.ActiveUser.Companies.Count > 1 Then
                        '.CompanyId = CompanyMultipleDrop.SelectedGuid 'Me.GetSelectedItem(Me.cboCompanyCode)
                        .CompanyId = moMultipleColumnDrop.SelectedGuid
                    Else
                        .CompanyId = CType(ElitaPlusIdentity.Current.ActiveUser.Companies.Item(0), Guid)
                    End If
                End If

                If State.MyBO.UseSvcOrderAddress Then
                    State.SvcOrdersByDealer = State.MyBO.SvcOrdersAddress
                    PopulateBOProperty(State.SvcOrdersByDealer, "Name", txtName)
                    PopulateBOProperty(State.SvcOrdersByDealer, "TaxIdNumber", txtTaxId)
                    State.SvcOrdersByDealer.DealerId = State.MyBO.Id
                    SvcOrderAddressCtr.PopulateBOFromControl(True)


                    If ((SvcOrderAddressCtr.MyBO.IsDeleted = False) AndAlso
                        (SvcOrderAddressCtr.MyBO.IsEmpty = False)) Then
                        State.SvcOrdersByDealer.AddressId = SvcOrderAddressCtr.MyBO.Id
                    End If

                End If

                AddressCtr.PopulateBOFromControl(True)
                If AddressCtr.MyBO IsNot Nothing Then
                    If ((AddressCtr.MyBO.IsDeleted = False) AndAlso
                        (AddressCtr.MyBO.IsEmpty = False)) AndAlso Not State.MyBO.AddressId = AddressCtr.MyBO.Id Then
                        State.MyBO.AddressId = AddressCtr.MyBO.Id
                    End If
                End If

                MailingAddressCtr.PopulateBOFromControl(True)
                If MailingAddressCtr.MyBO IsNot Nothing Then
                    If ((MailingAddressCtr.MyBO.IsDeleted = False) AndAlso
                        (MailingAddressCtr.MyBO.IsEmpty = False)) Then
                        State.MyBO.MailingAddressId = MailingAddressCtr.MyBO.Id
                    End If
                End If
                moBankInfo.PopulateBOFromControl(True, False)

                'Me.PopulateBOProperty(Me.State.MyBO, "AuthAmtBasedOnId", Me.moAuthAmtBasedOn)
                PopulateBOProperty(State.MyBO, "ProductByRegionId", moProdByRegion)
                PopulateBOProperty(State.MyBO, "ClaimVerfificationNumLength", txtClaimVerfificationNumLength)
                PopulateBOProperty(State.MyBO, "ClaimExtendedStatusEntryId", moClaim_Extended_Status_Entry)
                PopulateBOProperty(State.MyBO, "AllowUpdateCancellationId", moAlwupdateCancel)
                PopulateBOProperty(State.MyBO, "RejectAfterCancellationId", moRejectaftercancel)

                'Req-1000

                PopulateBOProperty(State.MyBO, "AllowUpdateCancellationId", moAlwupdateCancel)
                PopulateBOProperty(State.MyBO, "RejectAfterCancellationId", moRejectaftercancel)
                PopulateBOProperty(State.MyBO, "AllowFutureCancelDateId", moAllowfuturecancel)
                PopulateBOProperty(State.MyBO, "IsLawsuitMandatoryId", moIsLawsuitMandatory)

                'REQ-1153
                PopulateBOProperty(State.MyBO, "DealerSupportWebClaimsId", moDEALER_SUPPORT_WEB_CLAIMS)
                PopulateBOProperty(State.MyBO, "ClaimStatusForExtSystemId", moExtSystemClaimStatus)
                'REQ-1153 end

                'req 1157
                PopulateBOProperty(State.MyBO, "NewDeviceSkuRequiredId", ddlReplacementSKURequired)
                'req 1157 end
                PopulateBOProperty(State.MyBO, "UseClaimAuthorizationId", moUseClaimAutorization)

                'REQ-1190
                PopulateBOProperty(State.MyBO, "EnrollfilepreprocessprocId", ddlEnrFilePreProcess)
                PopulateBOProperty(State.MyBO, "CertnumlookupbyId", ddlCertNumLookUpBy)
                'REQ-1190
                'Req-1142
                If (moLicenseTagMandatory.Visible AndAlso GetSelectedItem(moLicenseTagMandatory) <> Guid.Empty) Then
                    PopulateBOProperty(State.MyBO, "LicenseTagValidationId", moLicenseTagMandatory)
                End If
                'Req-1142 end
                'Req-5723 start
                If (moVinrestrictMandatory.Visible) Then
                    PopulateBOProperty(State.MyBO, "VINRestrictMandatoryId", moVinrestrictMandatory)
                End If
                If (moplancodeinquote.Visible) Then
                    PopulateBOProperty(State.MyBO, "PlanCodeInQuote", moplancodeinquote)
                End If
                'Req-5723 End

                'REQ-1244
                PopulateBOProperty(State.MyBO, "Replaceclaimdedtolerancepct", txtRepClaimDedTolerancePct)

                'REQ-1274
                PopulateBOProperty(State.MyBO, "BillingProcessCodeId", ddlBillingProcessCode)
                PopulateBOProperty(State.MyBO, "BillresultExceptionDestId", ddlBillResultExpFTPSite)
                PopulateBOProperty(State.MyBO, "BillresultNotificationEmail", txtBillResultNotifyEmail)

                'REQ-5466
                PopulateBOProperty(State.MyBO, "AutoSelectServiceCenter", moAutoSelectServiceCenter)
                'REQ-5586
                PopulateBOProperty(State.MyBO, "PolicyEventNotificationEmail", txtPolicyEventNotifiyEmail)
                PopulateBOProperty(State.MyBO, "MaximumCommissionPercent", txtMaxCommissionPercent)



                If Not inputServiceCenterId.Value = Guid.Empty.ToString Then
                    AjaxController.PopulateBOAutoComplete(moDefaultSalvageCenter, inputServiceCenterDesc,
                                                          inputServiceCenterId, State.MyBO, "DefaultSalvgeCenterId")
                End If
                PopulateBOProperty(State.MyBO, "AutoGenerateRejectedPaymentFileId", ddlAutoGenRejPymtFile)
                PopulateBOProperty(State.MyBO, "PaymentRejectedRecordReconcileId", ddlPymtRejRecRecon)

                PopulateBOProperty(State.MyBO, "IdentificationNumberType", cboIdentificationNumberType, False, True)
                PopulateBOProperty(State.MyBO, "UseQuote", cboUseQuote, False, True)
                PopulateBOProperty(State.MyBO, "ContractManualVerification", cboContractManualVerification, False, True)
                'REQ-6406
                PopulateBOProperty(State.MyBO, "AcceptPaymentByCheck", moAcceptPaymentByCheckDrop, False, True)

                'REQ
                PopulateBOProperty(State.MyBO, "ClaimRecordingXcd", moClaimRecordingDrop, False, True)
                PopulateBOProperty(State.MyBO, "ClaimRecordingCheckInventoryXcd", ddlClaimRecordingCheckInventory, False, True)

                PopulateBOProperty(State.MyBO, "UseFraudMonitoringXcd", ddlUseFraudMonitoring, False, True)

                'REQ 6156
                PopulateBOProperty(State.MyBO, "ImeiUseXcd", cboImeiNoUse, False, True)

                PopulateBOProperty(State.MyBO, "BenefitSoldToAccount", txtBenefitSoldToAccount)

                'US32,33,34 -- Thunder
                If (State.MyBO.DealerTypeDesc IsNot Nothing AndAlso State.MyBO.DealerTypeDesc.ToUpper() = State.MyBO.DEALER_TYPE_BENEFIT) Then
                    PopulateBOProperty(State.MyBO, "SuspendAppliesXcd", ddlSuspenseApplies, False, True)
                    PopulateBOProperty(State.MyBO, "SourceSystemXcd", ddlSourceSystem, False, True)
                    PopulateBOProperty(State.MyBO, "VoidDuration", txtVoidDuration)
                    PopulateBOProperty(State.MyBO, "SuspendPeriod", txtSuspensePeriod)
                    PopulateBOProperty(State.MyBO, "InvoiceCutoffDay", txtInvCutOffDay)
                    PopulateBOProperty(State.MyBO, "BenefitCarrierCode", txtBenefitCarrierCode)

                Else
                    PopulateBOProperty(State.MyBO, "SuspendAppliesXcd", ddlSuspenseApplies, False, True)
                    PopulateBOProperty(State.MyBO, "SourceSystemXcd", ddlSourceSystem, False, True)
                    PopulateBOProperty(State.MyBO, "VoidDuration", "")
                    PopulateBOProperty(State.MyBO, "SuspendPeriod", "")
                    PopulateBOProperty(State.MyBO, "InvoiceCutoffDay", "")
                    PopulateBOProperty(State.MyBO, "BenefitCarrierCode", "")
                End If

                'KDDI Changes'

                PopulateBOProperty(State.MyBO, "Is_Cancel_Shipment_Allowed", cancelShipmentAllowedDrop, False, True)
                PopulateBOProperty(State.MyBO, "Cancel_Shipment_Grace_Period", txtCancelShipmentGracePeriod)
                PopulateBOProperty(State.MyBO, "Is_Reshipment_Allowed", reshipmentAllowedDrop, False, True)
                PopulateBOProperty(State.MyBO, "Validate_Address", moValidateAddress, False, True)

                PopulateBOProperty(State.MyBO, "CaseProfileCode", ddlCaseProfile, False, True)

                PopulateBOProperty(State.MyBO, "CloseCaseGracePeriodDays", txtClosecaseperiod)
                PopulateBOProperty(State.MyBO, "Show_Previous_Caller_Info", moShowPrevCallerInfo, False, True)
                PopulateBOProperty(State.MyBO, "UseTurnaroundTimeNotification", moUseTatNotification, False, True)
                PopulateBOProperty(State.MyBO, "DisplayDobXcd", ddlDealerNameFlag, False, True)
                PopulateBOProperty(State.MyBO, "AllowCertCancellationWithClaimXCd", ddlAllowCertCancellationWithClaim, False, True)

                'US 489857
                PopulateBOProperty(State.MyBO, "AcctBucketsWithSourceXcd", cboAcctBucketsWithSourceXcd, False, True)

            End With
            If ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub

        Private Sub PopulateCompanyDropDown()

            'CompanyMultipleDrop.NothingSelected = True
            moMultipleColumnDrop.NothingSelected = True

            If ElitaPlusIdentity.Current.ActiveUser.Companies.Count > 1 AndAlso State.MyBO.IsNew Then
                'CompanyMultipleDrop.SetControl(True, CompanyMultipleDrop.MODES.NEW_MODE, False, Me.State.companyDV, "* " & TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_COMPANY), True, False)
                moMultipleColumnDrop.SetControl(True, moMultipleColumnDrop.MODES.NEW_MODE, False, State.companyDV, "* " & TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_COMPANY), True, False)
            Else
                'CompanyMultipleDrop.SetControl(True, CompanyMultipleDrop.MODES.EDIT_MODE, False, Me.State.companyDV, "", True, False)
                moMultipleColumnDrop.SetControl(True, moMultipleColumnDrop.MODES.EDIT_MODE, False, State.companyDV, "", True, False)
            End If

        End Sub


        Protected Sub CreateNew()
            State.ScreenSnapShotBO = Nothing 'Reset the backup copy

            State.MyBO = New Dealer
            State.MyBO.CompanyId = moMultipleColumnDrop.SelectedGuid 'CompanyMultipleDrop.SelectedGuid
            State.IsCertificateExists = False

            State.Ocompany = New Company(State.MyBO.CompanyId)
            State.SvcOrdersByDealer = Nothing
            State.DealerCountryID = State.Ocompany.CountryId

            State.MyBO.UseSvcOrderAddress = False
            If LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, State.Ocompany.ServiceOrdersByDealerId) = Codes.YESNO_Y Then
                State.MyBO.UseSvcOrderAddress = True
                State.SvcOrdersByDealer = State.MyBO.SvcOrdersAddress
            End If

            PopulateFormFromBOs()
            EnableDisableFields()

            PopulateCompanyDropDown()

        End Sub

        Protected Sub CreateNewWithCopy()
            State.blnIsComingFromCopy = True
            ClaimCloseRules.HideNewButton(True)
            State.OldDealerId = State.MyBO.Id

            PopulateBOsFromForm()

            Dim newObj As New Dealer
            newObj.Copy(State.MyBO)

            State.MyBO = newObj
            State.IsCertificateExists = False
            With State.MyBO
                .Dealer = Nothing
                .DealerName = Nothing
                .BankInfoId = Guid.Empty
                If .ClaimAutoApproveId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_Y)) Then
                    'Claim Types
                    Dim DlrClaimTypesIDs As New ArrayList
                    Dim dlrClaimTypeIdStr As String
                    For Each dlrClaimTypeIdStr In UserControlAvailableSelectedClaimTypes.SelectedList
                        DlrClaimTypesIDs.Add(dlrClaimTypeIdStr)
                    Next
                    State.MyBO.AttachClaimType(DlrClaimTypesIDs)

                    'Coverage Types
                    Dim DlrCoverageTypesIDs As New ArrayList
                    Dim dlrCoverageTypeIdStr As String
                    For Each dlrCoverageTypeIdStr In UserControlAvailableSelectedCoverageTypes.SelectedList
                        DlrCoverageTypesIDs.Add(dlrCoverageTypeIdStr)
                    Next
                    State.MyBO.AttachCoverageType(DlrCoverageTypesIDs)
                End If

            End With

            PopulateFormFromBOs()
            EnableDisableFields()

            'create the backup copy
            State.ScreenSnapShotBO = New Dealer
            State.ScreenSnapShotBO.Clone(State.MyBO)

        End Sub

        Protected Sub SetupServiceCenterforVSC()

            Try
                callPage(ServiceCenterForm.URL, New ServiceCenterForm.Parameters(State.MyBO.Id, True))
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Protected Sub CheckIfComingFromSaveConfirm()
            Dim confResponse As String = HiddenSaveChangesPromptResponse.Value
            If Not State.blnIsComingFromNew Then

                If confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_YES Then 'Me.CONFIRM_MESSAGE_OK Then
                    If State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr Then
                        If State.MyBO.IsDirty Then
                            State.MyBO.Save()
                        End If

                        If moBankInfo.State.IsBODirty Then
                            moBankInfo.State.myBankInfoBo.Validate()
                            moBankInfo.State.myBankInfoBo.Save()
                            If State.MyBO.BankInfoId = Guid.Empty Then
                                State.MyBO.BankInfoId = moBankInfo.State.myBankInfoBo.Id
                                State.MyBO.Save()
                            End If
                        End If
                    End If
                    Select Case State.ActionInProgress
                        Case ElitaPlusPage.DetailPageCommand.Back
                            ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.HasDataChanged))
                        Case ElitaPlusPage.DetailPageCommand.New_
                            AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                            CreateNew()
                        Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                            AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                            CreateNewWithCopy()
                        Case ElitaPlusPage.DetailPageCommand.BackOnErr
                            ReturnToCallingPage(New ReturnType(State.ActionInProgress, State.MyBO, State.HasDataChanged))
                    End Select
                ElseIf confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_NO Then 'Me.CONFIRM_MESSAGE_CANCEL Then
                    Select Case State.ActionInProgress
                        Case ElitaPlusPage.DetailPageCommand.Back
                            ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.HasDataChanged))
                        Case ElitaPlusPage.DetailPageCommand.New_
                            CreateNew()
                        Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                            CreateNewWithCopy()
                        Case ElitaPlusPage.DetailPageCommand.BackOnErr
                            MasterPage.MessageController.AddErrorAndShow(State.LastErrMsg)
                    End Select
                End If
                'Clean after consuming the action
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
            Else
                If confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_YES Then
                    'setup service center for this dealer, ONLY for VSC
                    SetupServiceCenterforVSC()
                ElseIf confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_NO Then 'Me.CONFIRM_MESSAGE_CANCEL Then
                    Select Case State.ActionInProgress
                        Case ElitaPlusPage.DetailPageCommand.Back
                            ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.HasDataChanged))
                    End Select
                End If
            End If
            'Clean after consuming the action
            'Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
            HiddenSaveChangesPromptResponse.Value = ""
        End Sub

        Private Function isDealerDuplicate() As Boolean

            Dim oCompanyNew As Company = New Company(State.MyBO.CompanyId)
            Dim oCompanyTypeCode As String = LookupListNew.GetCodeFromId(LookupListNew.LK_COMPANY_TYPE, oCompanyNew.CompanyTypeId)
            Dim InsuranceTypeId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_COMPANY_TYPE, "1")

            If State.MyBO.IsNew Then
                If State.MyBO.GetDealerCountByCode() Then
                    DisplayMessage(Message.MSG_DUPLICATE_DEALER_NOT_ALLOWED, "", MSG_BTN_OK, MSG_TYPE_ALERT, HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Cancel
                    Return True
                End If
            End If

            Return False

        End Function
#End Region

#Region "Button Clicks"

        Private Sub btnBack_Click(sender As Object, e As System.EventArgs) Handles btnBack.Click
            Try
                PopulateBOsFromForm()
                If State.MyBO.IsDirty OrElse moBankInfo.State.IsBODirty Then
                    DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.HasDataChanged))
                End If

            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception

                If State.MyBO.ConstrVoilation = False Then
                    HandleErrors(ex, MasterPage.MessageController)
                    DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
                    State.LastErrMsg = MasterPage.MessageController.Text
                Else
                    ReturnToCallingPage(New ReturnType(State.ActionInProgress, State.MyBO, State.HasDataChanged))
                End If
            End Try
        End Sub

        Private Sub btnCopy_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnCopy_WRITE.Click
            Try
                PopulateBOsFromForm()
                If State.MyBO.IsDirty OrElse moBankInfo.State.IsBODirty Then
                    DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                    'Me.AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, Me.HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
                Else
                    CreateNewWithCopy()
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnDelete_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnDelete_WRITE.Click
            Try
                'Delete the Address
                'Me.State.MyBO.DeleteAndSave()
                'Me.State.HasDataChanged = True
                'Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Delete, Me.State.MyBO, Me.State.HasDataChanged))
                DisplayMessage(Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenDelDeletePromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Private Sub btnNew_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnNew_WRITE.Click
            Try
                PopulateBOsFromForm()
                If State.MyBO.IsDirty OrElse moBankInfo.State.IsBODirty Then
                    DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
                Else
                    CreateNew()
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnApply_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnApply_WRITE.Click

            'Dim blnIsComingFromNew As Boolean
            Dim exitingDealerCode As String = String.Empty

            Try
                exitingDealerCode = State.MyBO.Dealer
                PopulateBOsFromForm()
                If Not isDealerDuplicate() Then
                    If State.MyBO.IsDirty OrElse moBankInfo.State.IsBODirty OrElse State.MyBO.IsFamilyDirty Then
                        ' check HERE !! if coming from clicking on New or New with Copy button
                        State.blnIsComingFromNew = State.MyBO.IsNew
                        If State.MyBO.UseSvcOrderAddress Then
                            State.SvcOrdersByDealer.Validate()
                        End If

                        If State.MyBO.ClaimAutoApproveId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_Y)) Then
                            State.MyBO.DealerClaimTypeSelectionCount = UserControlAvailableSelectedClaimTypes.SelectedList.Count
                            State.MyBO.DealerCoverageTypeSelectionCount = UserControlAvailableSelectedCoverageTypes.SelectedList.Count
                        End If

                        If State.MyBO.IsDirty OrElse State.MyBO.IsFamilyDirty Then
                            ' Checking for Existing Certificates if Dealer Code has changes
                            If Not State.blnIsComingFromNew Then
                                ' There is change in Dealer Code
                                If exitingDealerCode <> State.MyBO.Dealer Then
                                    If (State.MyBO.GetDealerCertificatesCount() > 0) Then
                                        DisplayMessage(Message.MSG_DEALER_ALREADY_HAS_CERTIFICATES_DEFINITIONS_DEALERCODE_CANNOT_BE_UPDATED, "", MSG_BTN_OK, MSG_TYPE_ALERT)
                                        Exit Try
                                    End If
                                End If
                            End If

                            ShareCustSave()
                            State.MyBO.Save()

                            If ddlFullfileProcess.Visible = True AndAlso LookupListNew.GetCodeFromId(LookupListNew.LK_FLP, State.MyBO.UseFullFileProcessId) <> Codes.FLP_NO Then
                                State.MyBO.CreateExternalTable()
                            End If
                            'REQ-5467 : If LawsuitMandatory Value has changed from No to YES then create a Asynchronous Job to Update any Claims that needs to catch up
                            If State.LawsuitMandatoryIdAtLoad.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_N)) AndAlso
                               State.MyBO.IsLawsuitMandatoryId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_Y)) Then
                                State.MyBO.UpdateClaimsAsyncForLawsuit()
                            End If

                        End If

                        If moBankInfo.State.IsBODirty Then
                            If moBankInfo.State.myBankInfoBo.BankInfoAddress.Id.Equals(Guid.Empty) OrElse (Not moBankInfo.State.myBankInfoBo.BankInfoAddress.Id.Equals(Guid.Empty) _
                                                                                                           AndAlso Not moBankInfo.State.myBankInfoBo.AddressId.Equals(moBankInfo.State.myBankInfoBo.BankInfoAddress.Id)) Then
                                moBankInfo.State.myBankInfoBo.AddressId = moBankInfo.State.myBankInfoBo.BankInfoAddress.Id
                            End If
                            If moBankInfo.State.myBankInfoBo.IsDirty OrElse moBankInfo.State.myBankInfoBo.BankInfoAddress.IsDirty Then
                                moBankInfo.State.myBankInfoBo.Validate()
                                moBankInfo.State.myBankInfoBo.Save()
                                If State.MyBO.BankInfoId = Guid.Empty Then
                                    State.MyBO.BankInfoId = moBankInfo.State.myBankInfoBo.Id
                                    State.MyBO.Save()
                                End If
                            End If
                        End If

                        ''''REQ-5598
                        If (State.blnIsComingFromCopy) Then
                            ''''clone Claim Close Rules
                            Dim objCloseClaimRules As New ClaimCloseRules
                            objCloseClaimRules.CopyClaimCloseRulesToNewDealer(State.Ocompany.Id, State.OldDealerId, State.MyBO.Id)
                            State.blnIsComingFromCopy = False
                            ClaimCloseRules.HideNewButton(False)
                        End If

                        State.HasDataChanged = True
                        If State.IsNew = True Then
                            State.IsNew = False
                        End If
                        PopulateFormFromBOs()
                        EnableDisableFields()
                        If State.blnIsComingFromNew AndAlso LookupListNew.GetCodeFromId(LookupListNew.LK_DEALER_TYPE, State.MyBO.DealerTypeId) = Codes.DEALER_TYPES__VSC Then
                            DisplayMessage(Message.MSG_PROMPT_FOR_SETUP_SERVICE_CENTER_OF_DEALER, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                        Else
                            'Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                            MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
                        End If
                    Else
                        'Me.AddInfoMsg(Message.MSG_RECORD_NOT_SAVED)
                        MasterPage.MessageController.AddInformation(Message.MSG_RECORD_NOT_SAVED)
                    End If
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnUndo_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnUndo_WRITE.Click
            Try
                If Not State.MyBO.IsNew Then
                    'Reload from the DB
                    State.MyBO = New Dealer(State.MyBO.Id)
                ElseIf State.ScreenSnapShotBO IsNot Nothing Then
                    'It was a new with copy
                    State.MyBO.Clone(State.ScreenSnapShotBO)
                Else
                    'Me.PopulateBOsFromForm()
                    CreateNew()
                    'Me.State.MyBO = New Dealer
                End If

                State.blnIsComingFromCopy = False
                'Me.ClaimCloseRules.HideNewButton(False)

                PopulateFormFromBOs()
                EnableDisableFields()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
                'If Me.State.MyBO.ConstrVoilation = False Then
                '    Me.HandleErrors(ex, Me.MasterPage.MessageController)
                '    Me.AddConfirmMsg(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, Me.HiddenSaveChangesPromptResponse)
                '    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
                '    Me.State.LastErrMsg = Me.MasterPage.MessageController.Text
                'End If
            End Try
        End Sub
        'yogita 
        'Private Sub btnAcctSettings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAcctSettings.Click
        '    Dim _d As New AcctSetting
        '    Dim dv As AcctSetting.DealerAcctSettingsDV = Nothing, blnStatus As Boolean, DealerId As Guid, AcctId As Guid, DealerName As String
        '    Try
        '        dv = _d.GetDealerAcctSettingId(txtDealerCode.Text)
        '        If dv.Count > 0 Then
        '            Dim dvRow As DataRowView = dv(0)
        '            AcctId = GetGuidFromString(GetGuidStringFromByteArray(CType(dvRow(dv.ACCT_SETTINGS_ID), Byte())))
        '            DealerId = GetGuidFromString(GetGuidStringFromByteArray(CType(dvRow(dv.DEALER_ID), Byte())))
        '            DealerName = dvRow(dv.DEALER_NAME).ToString
        '            blnStatus = CType(Microsoft.VisualBasic.IIf(dvRow(dv.STATUS).Equals("Y"), True, False), Boolean)
        '            Me.callPage(AccountingSettingForm.URL, New AccountingSettingForm.ReturnType(AcctId, AccountingSettingForm.ReturnType.TargetType.Dealer, DealerId, DealerName, blnStatus, False))
        '        Else
        '            blnStatus = CType(Microsoft.VisualBasic.IIf(Me.State.MyBO.ActiveFlag.Equals("Y"), True, False), Boolean)
        '            DealerId = Me.State.MyBO.Id
        '            AcctId = New Guid(NOTHING_SELECTED_GUID)
        '            DealerName = txtDealerName.Text
        '            Me.callPage(AccountingSettingForm.URL, New AccountingSettingForm.ReturnType(AcctId, AccountingSettingForm.ReturnType.TargetType.Dealer, DealerId, DealerName, blnStatus, False))
        '        End If
        '    Catch ex As Threading.ThreadAbortException
        '    Catch ex As Exception
        '        Me.HandleErrors(ex, Me.MasterPage.MessageController)
        '    End Try
        'End Sub
#End Region
        Private Sub moClaimSystemDrop_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles moClaimSystemDrop.SelectedIndexChanged

            If moClaimSystemDrop.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
                If Not GetSelectedItem(moClaimSystemDrop).Equals(System.Guid.Empty) AndAlso GetSelectedItem(moClaimSystemDrop).Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_CLAIM_SYSTEM, "ELITA")) Then
                    ControlMgr.SetVisibleControl(Me, trClaimRec, True)
                Else
                    ControlMgr.SetVisibleControl(Me, trClaimRec, False)
                End If
            End If
        End Sub
        Private Sub cboCertificatesAutonumberId_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cboCertificatesAutonumberId.SelectedIndexChanged

            If cboCertificatesAutonumberId.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
                If GetSelectedItem(cboCertificatesAutonumberId).Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_Y)) Then
                    ControlMgr.SetVisibleControl(Me, lblCertificatesAutonumberPrefix, True)
                    ControlMgr.SetVisibleControl(Me, txtCertificatesAutonumberPrefix, True)
                    ControlMgr.SetVisibleControl(Me, lblMaxCertNumLengthAlwd, True)
                    ControlMgr.SetVisibleControl(Me, txtMaxCertNumLengthAlwd, True)
                Else
                    txtCertificatesAutonumberPrefix.Text = String.Empty
                    ControlMgr.SetVisibleControl(Me, lblCertificatesAutonumberPrefix, False)
                    ControlMgr.SetVisibleControl(Me, txtCertificatesAutonumberPrefix, False)
                    txtMaxCertNumLengthAlwd.Text = String.Empty
                    ControlMgr.SetVisibleControl(Me, lblMaxCertNumLengthAlwd, False)
                    ControlMgr.SetVisibleControl(Me, txtMaxCertNumLengthAlwd, False)
                End If
            End If
        End Sub

        Protected Sub moClaimAutoApproveDrop_SelectedIndexChanged(sender As Object, e As EventArgs) Handles moClaimAutoApproveDrop.SelectedIndexChanged

            If GetSelectedItem(moClaimAutoApproveDrop).Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_Y)) Then
                'ControlMgr.SetVisibleControl(Me, Me.ClaimTypesRow, True)
                'ControlMgr.SetVisibleControl(Me, Me.CoverageTypesRow, True)
                ControlMgr.SetVisibleControl(Me, pnlTypesRow, True)
                PopulateClaimAutoApproveControls()
                DisabledTabsList.RemoveAll(Function(i) Tab_DealerInflation.Contains(i))
                DisabledTabsList.RemoveAll(Function(i) Tab_RiskTypeTolerance.Contains(i))
            Else
                'ControlMgr.SetVisibleControl(Me, Me.ClaimTypesRow, False)
                'ControlMgr.SetVisibleControl(Me, Me.CoverageTypesRow, False)
                ControlMgr.SetVisibleControl(Me, pnlTypesRow, False)
                State.MyBO.DetachClaimType(UserControlAvailableSelectedClaimTypes.SelectedList)
                State.MyBO.DetachCoverageType(UserControlAvailableSelectedCoverageTypes.SelectedList)
                DisabledTabsList.Add(Tab_DealerInflation)
                DisabledTabsList.Add(Tab_RiskTypeTolerance)
            End If

        End Sub

        Private Sub moAutoProcessFileDrop_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles moAutoProcessFileDrop.SelectedIndexChanged
            'Keep This For Future Use
        End Sub

        Private Sub OnFromDrop_Changed(fromMultipleDrop As MultipleColumnDDLabelControl_New) _
            Handles moMultipleColumnDrop.SelectedDropChanged
            Try
                State.MyBO.CompanyId = moMultipleColumnDrop.SelectedGuid
                State.Ocompany = New Company(State.MyBO.CompanyId)
                If Not State.Ocompany.CertnumlookupbyId.Equals(Guid.Empty) Then
                    ddlCertNumLookUpBy.ClearSelection()
                    ControlMgr.SetVisibleControl(Me, lblCertNumLookUpBy, False)
                    ControlMgr.SetVisibleControl(Me, ddlCertNumLookUpBy, False)
                Else
                    ControlMgr.SetVisibleControl(Me, lblCertNumLookUpBy, True)
                    ControlMgr.SetVisibleControl(Me, ddlCertNumLookUpBy, True)
                End If

                PopulateDropdowns()
                PopulateAddressFields()

                State.SvcOrdersByDealer = Nothing
                State.MyBO.UseSvcOrderAddress = False
                If LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, State.Ocompany.ServiceOrdersByDealerId) = Codes.YESNO_Y Then
                    State.MyBO.UseSvcOrderAddress = True
                    State.SvcOrdersByDealer = State.MyBO.SvcOrdersAddress
                    PopulateSvcOrdersAddressFields()
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub ddlFullfileProcess_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlFullfileProcess.SelectedIndexChanged
            If LookupListNew.GetCodeFromId(LookupListNew.LK_FLP, GetSelectedItem(ddlFullfileProcess)) <> Codes.FLP_NO Then
                lblMaxNCRecords.Visible = True
                txtMaxNCRecords.Visible = True
            Else
                lblMaxNCRecords.Visible = False
                txtMaxNCRecords.Visible = False
                txtMaxNCRecords.Text = ""
            End If
        End Sub

        Private Sub PopulateAddressFields()
            Dim populateOptions As PopulateOptions = New PopulateOptions() With
                    {
                    .AddBlankItem = True
                    }

            If State.IsNew Then
                'Set country to the country of selected company
                If Not State.MyBO.CompanyId.Equals(Guid.Empty) Then
                    'Dim allCountryList As DataView = LookupListNew.GetCountryLookupList()
                    'BindListControlToDataView(CType(Me.AddressCtr.FindControl("moCountryDrop_WRITE"), DropDownList), allCountryList, , , True)
                    Dim countryList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="Country", languageCode:=Thread.CurrentPrincipal.GetLanguageCode())
                    CType(AddressCtr.FindControl("moCountryDrop_WRITE"), DropDownList).Populate(countryList, populateOptions)

                    'Dim oCountryList As DataView = LookupListNew.GetCountryLookupList(Me.State.MyBO.CompanyId)
                    Dim ListContext1 As New ListContext
                    ListContext1.CompanyId = State.MyBO.CompanyId
                    Dim countryListForCompany As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="CountryByCompany", context:=ListContext1)

                    If countryListForCompany.Count > 0 Then
                        State.MyBO.Address.CountryId = countryListForCompany.FirstOrDefault().ListItemId
                        SetSelectedItem(CType(AddressCtr.FindControl("moCountryDrop_WRITE"), DropDownList), State.MyBO.Address.CountryId)
                    End If

                    If Not State.MyBO.Address.CountryId.Equals(Guid.Empty) Then
                        CType(AddressCtr.FindControl("moCountryText"), TextBox).Text = (From lst In countryList
                            Where lst.ListItemId = State.MyBO.Address.CountryId
                            Select lst.Translation).FirstOrDefault()
                    End If

                    'If oCountryList.Count > 0 Then
                    '    Me.State.MyBO.Address.CountryId = New Guid(CType(oCountryList.Item(0).DataView(0)("ID"), Byte()))
                    '    SetSelectedItem(CType(Me.AddressCtr.FindControl("moCountryDrop_WRITE"), DropDownList), Me.State.MyBO.Address.CountryId)
                    'End If

                    'If Not Me.State.MyBO.Address.CountryId.Equals(Guid.Empty) Then
                    '    CType(Me.AddressCtr.FindControl("moCountryText"), TextBox).Text = LookupListNew.GetDescriptionFromId(LookupListNew.DataView(LookupListNew.LK_COUNTRIES), Me.State.MyBO.Address.CountryId)
                    'End If

                    'Populate Region Dropdown
                    'Dim oRegionList As DataView = LookupListNew.GetRegionLookupList(Me.State.MyBO.Address.CountryId)
                    'BindListControlToDataView(CType(Me.AddressCtr.FindControl("moRegionDrop_WRITE"), DropDownList), oRegionList, , , True)
                    Dim listcontext2 As ListContext = New ListContext()
                    listcontext2.CountryId = State.MyBO.Address.CountryId
                    CType(AddressCtr.FindControl("moRegionDrop_WRITE"), DropDownList).Populate(CommonConfigManager.Current.ListManager.GetList("RegionsByCountry", Thread.CurrentPrincipal.GetLanguageCode(), listcontext2), populateOptions)
                End If
            End If
        End Sub

        Private Sub PopulateMailingAddressFields()
            Dim populateOptions As PopulateOptions = New PopulateOptions() With
                    {
                    .AddBlankItem = True
                    }

            If State.IsNew Then
                'Set country to the country of selected company
                If Not State.MyBO.CompanyId.Equals(Guid.Empty) Then
                    'Dim allCountryList As DataView = LookupListNew.GetCountryLookupList()
                    'BindListControlToDataView(CType(Me.MailingAddressCtr.FindControl("moCountryDrop_WRITE"), DropDownList), allCountryList, , , True)
                    Dim countryList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="Country", languageCode:=Thread.CurrentPrincipal.GetLanguageCode())
                    CType(MailingAddressCtr.FindControl("moCountryDrop_WRITE"), DropDownList).Populate(countryList, populateOptions)

                    'Dim oCountryList As DataView = LookupListNew.GetCountryLookupList(Me.State.MyBO.CompanyId)
                    Dim ListContext1 As New ListContext
                    ListContext1.CompanyId = State.MyBO.CompanyId
                    Dim countryListForCompany As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="CountryByCompany", context:=ListContext1)

                    If countryListForCompany.Count > 0 Then
                        State.MyBO.MailingAddress.CountryId = countryListForCompany.FirstOrDefault().ListItemId
                        SetSelectedItem(CType(MailingAddressCtr.FindControl("moCountryDrop_WRITE"), DropDownList), State.MyBO.MailingAddress.CountryId)
                    End If

                    If Not State.MyBO.MailingAddress.CountryId.Equals(Guid.Empty) Then
                        CType(MailingAddressCtr.FindControl("moCountryText"), TextBox).Text = (From lst In countryList
                            Where lst.ListItemId = State.MyBO.MailingAddress.CountryId
                            Select lst.Translation).FirstOrDefault()
                    End If

                    'If oCountryList.Count > 0 Then
                    '    Me.State.MyBO.MailingAddress.CountryId = countryListForCompany.FirstOrDefault().ListItemId
                    '    SetSelectedItem(CType(Me.MailingAddressCtr.FindControl("moCountryDrop_WRITE"), DropDownList), Me.State.MyBO.MailingAddress.CountryId)
                    'End If

                    'If Not Me.State.MyBO.MailingAddress.CountryId.Equals(Guid.Empty) Then
                    '    CType(Me.MailingAddressCtr.FindControl("moCountryText"), TextBox).Text = LookupListNew.GetDescriptionFromId(LookupListNew.DataView(LookupListNew.LK_COUNTRIES), Me.State.MyBO.MailingAddress.CountryId)
                    'End If

                    'Populate Region Dropdown
                    'Dim oRegionList As DataView = LookupListNew.GetRegionLookupList(Me.State.MyBO.MailingAddress.CountryId)
                    'BindListControlToDataView(CType(Me.MailingAddressCtr.FindControl("moRegionDrop_WRITE"), DropDownList), oRegionList, , , True)
                    Dim listcontext2 As ListContext = New ListContext()
                    listcontext2.CountryId = State.MyBO.MailingAddress.CountryId
                    CType(MailingAddressCtr.FindControl("moRegionDrop_WRITE"), DropDownList).Populate(CommonConfigManager.Current.ListManager.GetList("RegionsByCountry", Thread.CurrentPrincipal.GetLanguageCode(), listcontext2), populateOptions)
                End If
            End If

        End Sub

        Private Sub PopulateSvcOrdersAddressFields()
            Dim populateOptions As PopulateOptions = New PopulateOptions() With
                    {
                    .AddBlankItem = True
                    }

            If State.IsNew Then
                'Set country to the country of selected company
                If Not State.MyBO.CompanyId.Equals(Guid.Empty) Then
                    'Dim allCountryList As DataView = LookupListNew.GetCountryLookupList()
                    'BindListControlToDataView(CType(Me.SvcOrderAddressCtr.FindControl("moCountryDrop_WRITE"), DropDownList), allCountryList, , , True)
                    Dim countryList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="Country", languageCode:=Thread.CurrentPrincipal.GetLanguageCode())
                    CType(SvcOrderAddressCtr.FindControl("moCountryDrop_WRITE"), DropDownList).Populate(countryList, populateOptions)

                    'Dim oCountryList As DataView = LookupListNew.GetCountryLookupList(Me.State.MyBO.CompanyId)
                    Dim ListContext1 As New ListContext
                    ListContext1.CompanyId = State.MyBO.CompanyId
                    Dim countryListForCompany As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="CountryByCompany", context:=ListContext1)

                    If countryListForCompany.Count > 0 Then
                        State.SvcOrdersByDealer.Address.CountryId = countryListForCompany.FirstOrDefault().ListItemId
                        SetSelectedItem(CType(SvcOrderAddressCtr.FindControl("moCountryDrop_WRITE"), DropDownList), State.SvcOrdersByDealer.Address.CountryId)
                    End If

                    If Not State.SvcOrdersByDealer.Address.CountryId.Equals(Guid.Empty) Then
                        CType(SvcOrderAddressCtr.FindControl("moCountryText"), TextBox).Text = (From lst In countryList
                            Where lst.ListItemId = State.SvcOrdersByDealer.Address.CountryId
                            Select lst.Translation).FirstOrDefault()
                    End If

                    'If oCountryList.Count > 0 Then
                    '    Me.State.SvcOrdersByDealer.Address.CountryId = New Guid(CType(oCountryList.Item(0).DataView(0)("ID"), Byte()))
                    '    SetSelectedItem(CType(Me.SvcOrderAddressCtr.FindControl("moCountryDrop_WRITE"), DropDownList), Me.State.SvcOrdersByDealer.Address.CountryId)
                    'End If

                    'If Not Me.State.SvcOrdersByDealer.Address.CountryId.Equals(Guid.Empty) Then
                    '    CType(Me.SvcOrderAddressCtr.FindControl("moCountryText"), TextBox).Text = LookupListNew.GetDescriptionFromId(LookupListNew.DataView(LookupListNew.LK_COUNTRIES), Me.State.SvcOrdersByDealer.Address.CountryId)
                    'End If

                    'Populate Region Dropdown
                    'Dim oRegionList As DataView = LookupListNew.GetRegionLookupList(Me.State.SvcOrdersByDealer.Address.CountryId)
                    'BindListControlToDataView(CType(Me.SvcOrderAddressCtr.FindControl("moRegionDrop_WRITE"), DropDownList), oRegionList, , , True)
                    Dim listcontext2 As ListContext = New ListContext()
                    listcontext2.CountryId = State.SvcOrdersByDealer.Address.CountryId
                    CType(SvcOrderAddressCtr.FindControl("moRegionDrop_WRITE"), DropDownList).Populate(CommonConfigManager.Current.ListManager.GetList("RegionsByCountry", Thread.CurrentPrincipal.GetLanguageCode(), listcontext2), populateOptions)
                End If
            End If

        End Sub

        Private Sub moUseEquipment_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles moUseEquipment.SelectedIndexChanged
            PopulateBOProperty(State.MyBO, "UseEquipmentId", moUseEquipment)
            'ControlMgr.SetVisibleControl(Me, moBestReplacementDrop, False)
            'ControlMgr.SetVisibleControl(Me, lblBestReplacement, False)
            'ControlMgr.SetVisibleControl(Me, moEquipmentListDrop, False)
            'ControlMgr.SetVisibleControl(Me, lblEquipmentList, False)
            'If (Me.State.MyBO.UseEquipmentId = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, "Y")) Then
            'ControlMgr.SetVisibleControl(Me, moBestReplacementDrop, True)
            'ControlMgr.SetVisibleControl(Me, lblBestReplacement, True)
            'ControlMgr.SetVisibleControl(Me, moEquipmentListDrop, True)
            'ControlMgr.SetVisibleControl(Me, lblEquipmentList, True)
            'Else
            '    Me.moBestReplacementDrop.ClearSelection()
            '    '' Me.moEquipmentListDrop.ClearSelection()
            'End If
        End Sub
        Private Sub UpdateBreadCrum()
            If (State IsNot Nothing) Then
                If (State IsNot Nothing) Then
                    MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("DEALER")
                    MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("DEALER")
                End If
            End If
        End Sub
        Private Sub moDealerGroupDrop_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles moDealerGroupDrop.SelectedIndexChanged
            CheckUseClientDealerCodeFlag()
        End Sub
        Private Sub moDealerTypeDrop_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles moDealerTypeDrop.SelectedIndexChanged
            PopulateBOProperty(State.MyBO, "DealerTypeId", moDealerTypeDrop)
            ControlMgr.SetVisibleControl(Me, trLineHid1, False)
            ControlMgr.SetVisibleControl(Me, trHid1, False)
            ControlMgr.SetVisibleControl(Me, trHid2, False)
            ControlMgr.SetVisibleControl(Me, dlstRetailerID, True)
            ControlMgr.SetVisibleControl(Me, lblDealerIsRetailer, True)
            DisabledTabsList.Add(Tab_MailingAddress)
            State.MyBO.Address.AddressIsRequire = False

            ControlMgr.SetVisibleControl(Me, moUseEquipment, False)
            ControlMgr.SetVisibleControl(Me, lblUseEquipment, False)
            ControlMgr.SetVisibleControl(Me, moBestReplacementDrop, False)
            ControlMgr.SetVisibleControl(Me, lblBestReplacement, False)

            'Req-1142 start
            ControlMgr.SetVisibleControl(Me, trVscLicenseTag, False)
            'Req-1142 end

            'Req-5723 start            '
            ControlMgr.SetVisibleControl(Me, trVscVinRestrict, False)
            'Req-5723 end

            'US-32, 33, 34, 21 - Thunder
            ControlMgr.SetVisibleControl(Me, trBenefitDlrTypeCtls1, False)
            ControlMgr.SetVisibleControl(Me, trBenefitDlrTypeCtls2, False)
            ControlMgr.SetVisibleControl(Me, trBenefitDlrTypeCtls3, False)
            ControlMgr.SetVisibleControl(Me, trBenefitDlrTypeCtls4, True)

            If State.MyBO.DealerTypeDesc = State.MyBO.DEALER_TYPE_DESC Then
                ControlMgr.SetVisibleControl(Me, trLineHid1, True)
                ControlMgr.SetVisibleControl(Me, trHid1, True)
                ControlMgr.SetVisibleControl(Me, trHid2, True)
                MailingAddressCtr.EnableControls(False, True)
                PopulateMailingAddressFields()
                ControlMgr.SetVisibleControl(Me, dlstRetailerID, False)
                BindSelectItem(LookupListNew.GetIdFromCode(LookupListNew.GetYesNoLookupList(Authentication.LangId), "Y").ToString, dlstRetailerID)
                'Me.State.MyBO.RetailerId = 
                ControlMgr.SetVisibleControl(Me, lblDealerIsRetailer, False)
                State.MyBO.Address.AddressIsRequire = True
                PopulateBOProperty(State.MyBO, "UseEquipmentId", LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, "N"))
                SetSelectedItem(moUseEquipment, State.MyBO.UseEquipmentId)
                'Req-1142 Start
                ControlMgr.SetVisibleControl(Me, trVscLicenseTag, True)
                ControlMgr.SetVisibleControl(Me, trVscVinRestrict, True)
                If State.MyBO.LicenseTagValidationId.Equals(Guid.Empty) Then
                    BindSelectItem(LookupListNew.GetIdFromCode(LookupListNew.GetLicenseTagFlag(Authentication.LangId), LICENSE_TAG_FLAG_YES).ToString, moLicenseTagMandatory)
                End If
                'Req-1142 End
            ElseIf (State.MyBO.DealerTypeDesc = State.MyBO.DEALER_TYPE_DESC_WEPP) Then
                ControlMgr.SetVisibleControl(Me, moUseEquipment, True)
                ControlMgr.SetVisibleControl(Me, lblUseEquipment, True)
                ControlMgr.SetVisibleControl(Me, moBestReplacementDrop, True)
                ControlMgr.SetVisibleControl(Me, lblBestReplacement, True)
            ElseIf (Not String.IsNullOrWhiteSpace(State.MyBO.DealerTypeDesc) AndAlso State.MyBO.DealerTypeDesc.ToUpper() = State.MyBO.DEALER_TYPE_BENEFIT) Then
                ControlMgr.SetVisibleControl(Me, trBenefitDlrTypeCtls1, True)
                ControlMgr.SetVisibleControl(Me, trBenefitDlrTypeCtls2, True)
                ControlMgr.SetVisibleControl(Me, trBenefitDlrTypeCtls3, True)
                ControlMgr.SetVisibleControl(Me, trBenefitDlrTypeCtls4, True)

                BindSelectItem(State.MyBO.SuspendAppliesXcd, ddlSuspenseApplies)
                BindSelectItem(State.MyBO.SourceSystemXcd, ddlSourceSystem)

                PopulateControlFromBOProperty(txtVoidDuration, State.MyBO.VoidDuration)
                PopulateControlFromBOProperty(txtInvCutOffDay, State.MyBO.InvoiceCutoffDay)
                PopulateControlFromBOProperty(txtBenefitCarrierCode, State.MyBO.BenefitCarrierCode)
                PopulateControlFromBOProperty(txtBenefitSoldToAccount, State.MyBO.BenefitSoldToAccount)

                If (ddlSuspenseApplies.Items.Count > 0 AndAlso ddlSuspenseApplies.SelectedItem IsNot Nothing AndAlso ddlSuspenseApplies.SelectedItem.Value.ToUpper() = "YESNO-Y") Then
                    PopulateControlFromBOProperty(txtSuspensePeriod, State.MyBO.SuspendPeriod)
                    txtSuspensePeriod.Style.Add("display", "block")
                    lblSuspensePeriod.Style.Add("display", "block")
                Else
                    txtSuspensePeriod.Style.Add("display", "none")
                    lblSuspensePeriod.Style.Add("display", "none")
                End If


            Else
                PopulateBOProperty(State.MyBO, "UseEquipmentId", LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, "N"))
                SetSelectedItem(moUseEquipment, State.MyBO.UseEquipmentId)
            End If

        End Sub

        Public Sub ServiceCenterMessage()
            DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)
        End Sub

#Region "MerchantCode_Handlers_Grid"

        Protected Sub RowCommand(source As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs)
            Dim nIndex As Integer

            Try
                If e.CommandName = EDIT_COMMAND_NAME Then

                    nIndex = CInt(e.CommandArgument)
                    moMerchantCodesDatagrid.EditIndex = nIndex
                    moMerchantCodesDatagrid.SelectedIndex = nIndex

                    'DEF-3066
                    State.SelectedCompanyCreditCardType = CType(moMerchantCodesDatagrid.Rows(nIndex).Cells(COMPANY_CREDIT_CARD_TYPE).FindControl(COMPANY_CREDIT_CARD_TYPE_LABEL_CONTROL_NAME), Label).Text
                    'DEF-3066

                    State.IsMerchantCodeEditMode = True
                    State.MerchantCodeID = New Guid(CType(moMerchantCodesDatagrid.Rows(nIndex).Cells(MERCHANT_CODE_ID).FindControl(ID_CONTROL_NAME), Label).Text)
                    State.MyMerchantCodeBO = New MerchantCode(State.MerchantCodeID)
                    PopulateMyMerchantCodeGrid()

                    'Disable all Edit and Delete icon buttons on the Grid
                    SetGridControls(moMerchantCodesDatagrid, False)
                    State.PageIndex = moMerchantCodesDatagrid.PageIndex

                    'DEF-3066
                    State.MerchantCodeEdit = True
                    'DEF-3066

                    PopulateMerchantCodeFormFromBO(nIndex)
                    SetFocusOnEditableFieldInGrid(moMerchantCodesDatagrid, COMPANY_CREDIT_CARD_TYPE, COMPANY_CREDIT_CARD_CONTROL_NAME, nIndex)
                    SetMerchantCodeButtonsState(True)

                ElseIf (e.CommandName = DELETE_COMMAND) Then

                    'Do the delete here
                    nIndex = CInt(e.CommandArgument)

                    PopulateMyMerchantCodeGrid()
                    State.PageIndex = moMerchantCodesDatagrid.PageIndex

                    'Clear the SelectedItemStyle to remove the highlight from the previously saved row
                    moMerchantCodesDatagrid.SelectedIndex = NO_ROW_SELECTED_INDEX
                    'Save the Id in the Session
                    State.MerchantCodeID = New Guid(CType(moMerchantCodesDatagrid.Rows(nIndex).Cells(MERCHANT_CODE_ID).FindControl(ID_CONTROL_NAME), Label).Text)
                    DisplayMessage(Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenDeletePromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete

                ElseIf (e.CommandName = SAVE_COMMAND) Then

                    SaveRecord()
                ElseIf (e.CommandName = CANCEL_COMMAND) Then
                    CancelRecord()
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Protected Sub RowCreated(sender As Object, e As GridViewRowEventArgs)
            BaseItemCreated(sender, e)
        End Sub

        Private Sub Grid_Sorting(source As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles moMerchantCodesDatagrid.Sorting

            Try
                If State.MerchantCodeSortExpression.StartsWith(e.SortExpression) Then
                    If State.MerchantCodeSortExpression.EndsWith(" DESC") Then
                        State.MerchantCodeSortExpression = e.SortExpression
                    Else
                        State.MerchantCodeSortExpression &= " DESC"
                    End If
                Else
                    State.MerchantCodeSortExpression = e.SortExpression
                End If

                State.MerchantCodeID = Guid.Empty
                moMerchantCodesDatagrid.PageIndex = 0
                moMerchantCodesDatagrid.SelectedIndex = -1
                PopulateMyMerchantCodeGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Private Sub Grid_PageIndexChanging(source As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles moMerchantCodesDatagrid.PageIndexChanging
            Try
                If (Not (State.IsMerchantCodeEditMode)) Then
                    State.PageIndex = e.NewPageIndex
                    moMerchantCodesDatagrid.PageIndex = State.PageIndex
                    PopulateMyMerchantCodeGrid()
                    moMerchantCodesDatagrid.SelectedIndex = NO_ITEM_SELECTED_INDEX
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Private Sub Grid_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles moMerchantCodesDatagrid.RowDataBound
            Try
                Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

                Dim populateOptions As PopulateOptions = New PopulateOptions() With
                        {
                        .AddBlankItem = True
                        }

                If dvRow IsNot Nothing AndAlso Not State.MerchantCodeSearchDV.Count > 0 Then
                    If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem OrElse itemType = ListItemType.EditItem Then
                        CType(e.Row.Cells(MERCHANT_CODE_ID).FindControl(ID_CONTROL_NAME), Label).Text = GetGuidStringFromByteArray(CType(dvRow(MerchantCode.MerchantCodeSearchDV.COL_MERCHANT_CODE_ID), Byte()))
                        If (State.IsMerchantCodeEditMode = True AndAlso State.MerchantCodeID.ToString.Equals(GetGuidStringFromByteArray(CType(dvRow(MerchantCode.MerchantCodeSearchDV.COL_MERCHANT_CODE_ID), Byte())))) Then
                            'BindListControlToDataView(CType(e.Row.Cells(Me.COMPANY_CREDIT_CARD_ID).FindControl(Me.COMPANY_CREDIT_CARD_CONTROL_NAME), DropDownList), LookupListNew.GetCompanyCreditCardsLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId, Me.State.MyBO.CompanyId))
                            Dim listcontext1 As ListContext = New ListContext()
                            listcontext1.CompanyId = State.MyBO.CompanyId
                            CType(e.Row.Cells(COMPANY_CREDIT_CARD_ID).FindControl(COMPANY_CREDIT_CARD_CONTROL_NAME), DropDownList).Populate(CommonConfigManager.Current.ListManager.GetList(listCode:="CreditCardByCompany", languageCode:=Thread.CurrentPrincipal.GetLanguageCode(), context:=listcontext1), populateOptions)

                            SetSelectedItem(CType(e.Row.Cells(COMPANY_CREDIT_CARD_ID).FindControl(COMPANY_CREDIT_CARD_CONTROL_NAME), DropDownList), dvRow(MerchantCode.MerchantCodeSearchDV.COL_COMPANY_CREDIT_CARD_ID).ToString)
                            CType(e.Row.Cells(MERCHANT_CODE).FindControl(MERCHANT_CODE_TEXTBOX_CONTROL_NAME), TextBox).Text = dvRow(MerchantCode.MerchantCodeSearchDV.COL_MERCHANT_CODE).ToString

                        Else
                            CType(e.Row.Cells(COMPANY_CREDIT_CARD_TYPE).FindControl(COMPANY_CREDIT_CARD_TYPE_LABEL_CONTROL_NAME), Label).Text = dvRow(MerchantCode.MerchantCodeSearchDV.COL_COMPANY_CREDIT_CARD_TYPE).ToString
                            CType(e.Row.Cells(MERCHANT_CODE).FindControl(MERCHANT_CODE_LABEL_CONTROL_NAME), Label).Text = dvRow(MerchantCode.MerchantCodeSearchDV.COL_MERCHANT_CODE).ToString
                        End If
                    End If
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub PopulateMyMerchantCodeGrid()

            Try
                If State.MerchantCodeSearchDV Is Nothing Then
                    State.MerchantCodeSearchDV = GetMerchantCodeDV()

                    'DEF-3066
                    If State.PreviousMerchantCodeSearchDV Is Nothing Then
                        State.PreviousMerchantCodeSearchDV = GetMerchantCodeDV()
                    End If
                    'DEF-3066
                End If
                Dim dv As MerchantCode.MerchantCodeSearchDV

                If State.MerchantCodeSearchDV.Count = 0 Then
                    dv = State.MerchantCodeSearchDV.AddNewRowToEmptyDV
                    SetPageAndSelectedIndexFromGuid(dv, State.MerchantCodeID, moMerchantCodesDatagrid, State.PageIndex)
                    moMerchantCodesDatagrid.DataSource = dv
                Else
                    SetPageAndSelectedIndexFromGuid(State.MerchantCodeSearchDV, State.MerchantCodeID, moMerchantCodesDatagrid, State.PageIndex)
                    moMerchantCodesDatagrid.DataSource = State.MerchantCodeSearchDV
                End If

                State.MerchantCodeSearchDV.Sort = State.MerchantCodeSortExpression
                If (State.IsMerchantCodeAfterSave) Then
                    State.IsMerchantCodeAfterSave = False
                    SetPageAndSelectedIndexFromGuid(State.MerchantCodeSearchDV, State.MerchantCodeID, moMerchantCodesDatagrid, moMerchantCodesDatagrid.PageIndex)
                ElseIf (State.IsMerchantCodeEditMode) Then
                    SetPageAndSelectedIndexFromGuid(State.MerchantCodeSearchDV, State.MerchantCodeID, moMerchantCodesDatagrid, moMerchantCodesDatagrid.PageIndex, State.IsMerchantCodeEditMode)
                Else
                    'In a Delete scenario...
                    SetPageAndSelectedIndexFromGuid(State.MerchantCodeSearchDV, Guid.Empty, moMerchantCodesDatagrid, moMerchantCodesDatagrid.PageIndex, State.IsMerchantCodeEditMode)
                End If

                moMerchantCodesDatagrid.AutoGenerateColumns = False

                If State.MerchantCodeSearchDV.Count = 0 Then
                    SortAndBindGrid(dv)
                Else
                    SortAndBindGrid(State.MerchantCodeSearchDV)
                End If

                If State.MerchantCodeSearchDV.Count = 0 Then
                    For Each gvRow As GridViewRow In moMerchantCodesDatagrid.Rows
                        gvRow.Visible = False
                        gvRow.Controls.Clear()
                    Next
                End If

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Protected Sub BindBoPropertiesToGridHeaders()
            BindBOPropertyToGridHeader(State.MyMerchantCodeBO, "CompanyCreditCardId", moMerchantCodesDatagrid.Columns(COMPANY_CREDIT_CARD_TYPE))
            BindBOPropertyToGridHeader(State.MyMerchantCodeBO, "MerchantCode", moMerchantCodesDatagrid.Columns(MERCHANT_CODE))
            ClearGridViewHeadersAndLabelsErrorSign()
        End Sub

        Private Sub SetFocusOnEditableFieldInGrid(grid As GridView, cellPosition As Integer, controlName As String, itemIndex As Integer)
            'Set focus on the Description TextBox for the EditItemIndex row
            Dim code As DropDownList = CType(grid.Rows(itemIndex).Cells(cellPosition).FindControl(controlName), DropDownList)
            SetFocus(code)
        End Sub

        Private Sub PopulateMerchantCodeFormFromBO(Optional ByVal gridRowIdx As Integer = Nothing)

            If gridRowIdx.Equals(Nothing) Then gridRowIdx = moMerchantCodesDatagrid.EditIndex

            'DEF-3066        
            Dim cardDV As DataView = LookupListNew.GetCompanyCreditCardsLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId, State.MyBO.CompanyId)

            ' A variable to store the DESCRIPTION of company credit card type which can be applied in row filter
            Dim filterStr As String = ""
            For Each dr As DataRowView In State.PreviousMerchantCodeSearchDV
                If State.MerchantCodeAddNew Then
                    If filterStr = "" Then
                        filterStr = "'" + dr.Row(State.MerchantCodeSearchDV.COL_COMPANY_CREDIT_CARD_TYPE).ToString() + "'"
                    Else
                        filterStr = filterStr + ",'" + dr.Row(State.MerchantCodeSearchDV.COL_COMPANY_CREDIT_CARD_TYPE).ToString() + "'"
                    End If
                ElseIf State.MerchantCodeEdit Then
                    If dr.Row(State.MerchantCodeSearchDV.COL_COMPANY_CREDIT_CARD_TYPE).ToString() <> State.SelectedCompanyCreditCardType Then
                        If filterStr = "" Then
                            filterStr = "'" + dr.Row(State.MerchantCodeSearchDV.COL_COMPANY_CREDIT_CARD_TYPE).ToString() + "'"
                        Else
                            filterStr = filterStr + ",'" + dr.Row(State.MerchantCodeSearchDV.COL_COMPANY_CREDIT_CARD_TYPE).ToString() + "'"
                        End If
                    End If
                End If
            Next

            If filterStr <> "" Then
                ' Row filter of card data view already contains the language id condition which we are passing below as it is
                cardDV.RowFilter = String.Format("{0} and DESCRIPTION not in ({1})", cardDV.RowFilter, filterStr)
            End If

            BindListControlToDataView(CType(moMerchantCodesDatagrid.Rows(gridRowIdx).Cells(COMPANY_CREDIT_CARD_ID).FindControl(COMPANY_CREDIT_CARD_CONTROL_NAME), DropDownList), cardDV)

            State.MerchantCodeAddNew = False
            State.MerchantCodeEdit = False

            'DEF-3066

            Try
                With State.MyMerchantCodeBO

                    If (Not .Id.Equals(Guid.Empty)) Then
                        Dim cboCompanyCreditCard As DropDownList = CType(moMerchantCodesDatagrid.Rows(gridRowIdx).Cells(COMPANY_CREDIT_CARD_ID).FindControl(COMPANY_CREDIT_CARD_CONTROL_NAME), DropDownList)
                        If (Not .IsNew AndAlso Not .CreditCardFormatId.Equals(Guid.Empty)) Then PopulateControlFromBOProperty(cboCompanyCreditCard, .CreditCardFormatId)
                    End If

                    Dim txtMerchantCode As TextBox = CType(moMerchantCodesDatagrid.Rows(gridRowIdx).Cells(MERCHANT_CODE).FindControl(MERCHANT_CODE_TEXTBOX_CONTROL_NAME), TextBox)
                    PopulateControlFromBOProperty(txtMerchantCode, .MerchantCode)

                    CType(moMerchantCodesDatagrid.Rows(gridRowIdx).Cells(MERCHANT_CODE_ID).FindControl(ID_CONTROL_NAME), Label).Text = .Id.ToString
                    CType(moMerchantCodesDatagrid.Rows(gridRowIdx).Cells(COMPANY_CREDIT_CARD_ID).FindControl(COMPANY_CREDIT_CARD_ID_LABEL_CONTROL_NAME), Label).Text = .CompanyCreditCardId.ToString


                End With
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Private Sub SetMerchantCodeButtonsState(bIsEdit As Boolean)

            If State.IsEditMode Then
                ControlMgr.SetEnableControl(Me, btnNew_WRITE, False)
                ' Me.MenuEnabled = False
            ElseIf State.IsReadOnly Then
                ControlMgr.SetEnableControl(Me, btnNew_WRITE, False)
                'Diable the Edit and Delete Buttons as disabled
            Else
                If State.Action <> COPY_SCHEDULE Then
                    ControlMgr.SetEnableControl(Me, btnNew_WRITE, True)
                Else
                    ControlMgr.SetEnableControl(Me, btnNew_WRITE, False)
                End If

            End If

        End Sub


        'Private Sub SetButtonsState()

        '    If Me.State.IsEditMode Then
        '        ControlMgr.SetEnableControl(Me, btnNew_WRITE, False)
        '        Me.MenuEnabled = False
        '    ElseIf Me.State.IsReadOnly Then
        '        ControlMgr.SetEnableControl(Me, btnNew_WRITE, False)
        '        'Diable the Edit and Delete Buttons as disabled
        '    Else
        '        If Me.State.Action <> Me.COPY_SCHEDULE Then
        '            ControlMgr.SetEnableControl(Me, btnNew_WRITE, True)
        '        Else
        '            ControlMgr.SetEnableControl(Me, btnNew_WRITE, False)
        '        End If

        '        ControlMgr.SetEnableControl(Me, btnBack, True)
        '        Me.MenuEnabled = True
        '    End If

        'End Sub

        'End Sub


        Public Overrides Sub BaseSetButtonsState(bIsEdit As Boolean)
            SetMerchantCodeButtonsState(bIsEdit)
        End Sub

        'Private Sub DoDelete()
        '    Me.State.MyMerchantCodeBO = New MerchantCode(Me.State.MerchantCodeID)
        '    Try
        '        Me.State.MyMerchantCodeBO.Delete()
        '        'Call the Save() method in the DealerGroup Business Object here
        '        Me.State.MyMerchantCodeBO.Save()

        '    Catch ex As Exception
        '        Me.State.MyMerchantCodeBO.RejectChanges()
        '        Throw ex
        '    End Try

        '    Me.State.PageIndex = Me.moMerchantCodesDatagrid.PageIndex

        '    'Set the IsAfterSave flag to TRUE so that the Paging logic gets invoked
        '    Me.State.IsMerchantCodeAfterSave = True
        '    Me.State.MerchantCodeSearchDV = Nothing
        '    PopulateMyMerchantCodeGrid()
        '    Me.State.PageIndex = Me.moMerchantCodesDatagrid.PageIndex

        'End Sub
        Private Function GetMerchantCodeDV() As MerchantCode.MerchantCodeSearchDV

            Dim dv As MerchantCode.MerchantCodeSearchDV

            dv = GetDataView()
            dv.Sort = moMerchantCodesDatagrid.DataMember()
            moMerchantCodesDatagrid.DataSource = dv

            Return (dv)

        End Function

        Private Function GetDataView() As MerchantCode.MerchantCodeSearchDV

            Return MerchantCode.LoadList(State.MyBO.Id)

        End Function

        Private Sub SortAndBindGrid(dvBinding As DataView, Optional ByVal blnEmptyList As Boolean = False)
            moMerchantCodesDatagrid.DataSource = dvBinding
            HighLightSortColumn(moMerchantCodesDatagrid, State.MerchantCodeSortExpression)
            moMerchantCodesDatagrid.DataBind()
            If Not moMerchantCodesDatagrid.BottomPagerRow.Visible Then moMerchantCodesDatagrid.BottomPagerRow.Visible = True
            If blnEmptyList Then
                For Each gvRow As GridViewRow In moMerchantCodesDatagrid.Rows
                    'gvRow.Visible = False
                    gvRow.Controls.Clear()
                Next
            End If
            Session("recCount") = State.MerchantCodeSearchDV.Count

            'If Me.moMerchantCodesDatagrid.Visible Then
            '    If (Me.State.AddingMerchantCodeNewRow) Then
            '        Me.lblRecordCount.Text = (Me.State.MerchantCodeSearchDV.Count - 1) & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            '    Else
            '        Me.lblRecordCount.Text = Me.State.MerchantCodeSearchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            '    End If
            'End If
            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, moMerchantCodesDatagrid)
        End Sub

        Private Sub AddNew()
            State.MerchantCodeSearchDV = GetMerchantCodeDV()
            State.PreviousMerchantCodeSearchDV = State.MerchantCodeSearchDV

            State.MyMerchantCodeBO = New MerchantCode
            State.MerchantCodeID = State.MyMerchantCodeBO.Id

            State.MerchantCodeSearchDV = State.MyMerchantCodeBO.GetNewDataViewRow(State.MerchantCodeSearchDV, State.MerchantCodeID, State.MyMerchantCodeBO)
            'Me.State.searchDV = Me.State.searchDV.AddNewRowToEmptyDV
            moMerchantCodesDatagrid.DataSource = State.MerchantCodeSearchDV

            SetPageAndSelectedIndexFromGuid(State.MerchantCodeSearchDV, State.MerchantCodeID, moMerchantCodesDatagrid, State.PageIndex, State.IsMerchantCodeEditMode)

            moMerchantCodesDatagrid.AutoGenerateColumns = False

            SortAndBindGrid(State.MerchantCodeSearchDV)

            SetGridControls(moMerchantCodesDatagrid, False)

            'DEF-3066
            State.MerchantCodeAddNew = True
            'DEF-3066

            PopulateMerchantCodeFormFromBO()
        End Sub

        Private Sub ReturnMerchantCodeFromEditing()

            moMerchantCodesDatagrid.EditIndex = NO_ROW_SELECTED_INDEX

            If moMerchantCodesDatagrid.PageCount = 0 Then
                'if returning to the "1st time in" screen
                ControlMgr.SetVisibleControl(Me, moMerchantCodesDatagrid, False)
            Else
                ControlMgr.SetVisibleControl(Me, moMerchantCodesDatagrid, True)
            End If

            SetGridControls(moMerchantCodesDatagrid, True)
            State.IsMerchantCodeEditMode = False
            PopulateMyMerchantCodeGrid()
            State.PageIndex = moMerchantCodesDatagrid.PageIndex
            SetMerchantCodeButtonsState(False)

        End Sub

        Private Sub PopulateBOFromForm()
            Try
                Dim cboCompanyCreditCard As DropDownList = CType(moMerchantCodesDatagrid.Rows(moMerchantCodesDatagrid.EditIndex).Cells(COMPANY_CREDIT_CARD_ID).FindControl(COMPANY_CREDIT_CARD_CONTROL_NAME), DropDownList)
                Dim CreditCardFormatId As Guid = GetSelectedItem(cboCompanyCreditCard)
                If Not CreditCardFormatId.Equals(Guid.Empty) Then
                    Dim objCompanyCreditCard As CompanyCreditCard = New CompanyCreditCard(CreditCardFormatId, State.MyBO.CompanyId)
                    State.MyMerchantCodeBO.CompanyCreditCardId = objCompanyCreditCard.Id
                Else
                    State.MyMerchantCodeBO.CompanyCreditCardId = Nothing
                End If



                Dim txtMerchantCode As TextBox = CType(moMerchantCodesDatagrid.Rows(moMerchantCodesDatagrid.EditIndex).Cells(MERCHANT_CODE).FindControl(MERCHANT_CODE_TEXTBOX_CONTROL_NAME), TextBox)


                PopulateBOProperty(State.MyMerchantCodeBO, "DealerId", State.MyBO.Id)
                PopulateBOProperty(State.MyMerchantCodeBO, "MerchantCode", txtMerchantCode)

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub
#End Region
#Region "MerchantCodeHandlers_buttons"

        Private Sub BtnNewMerchantCode_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnNewMerchantCode_WRITE.Click
            Try
                State.IsMerchantCodeEditMode = True
                State.MerchantCodeSearchDV = Nothing
                State.PreviousMerchantCodeSearchDV = Nothing
                AddNew()
                SetMerchantCodeButtonsState(True)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub CancelRecord()
            Try
                IsNewMerchantCode = True
                SetGridControls(moMerchantCodesDatagrid, True)
                If (IsNewMerchantCode) Then
                    State.MerchantCodeSearchDV = Nothing
                    State.PreviousMerchantCodeSearchDV = Nothing
                End If
                ReturnMerchantCodeFromEditing()
                IsNewMerchantCode = False
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
        Private Sub SaveRecord()
            Try
                PopulateBOFromForm()
                If (State.MyMerchantCodeBO.IsDirty) Then
                    State.MyMerchantCodeBO.Save()
                    State.IsMerchantCodeAfterSave = True
                    State.AddingMerchantCodeNewRow = False
                    'Me.AddInfoMsg(Me.MSG_RECORD_SAVED_OK)
                    State.MyMerchantCodeBO.EndEdit()
                    State.IsEditMode = False
                    ' Me.State.IsMerchantCodeNew = False
                    'Me.State.AddingNewRow = False
                    State.Action = ""
                    State.MerchantCodeSearchDV = Nothing
                    State.PreviousMerchantCodeSearchDV = Nothing
                    ReturnMerchantCodeFromEditing()
                Else
                    AddInfoMsg(MSG_RECORD_NOT_SAVED)
                    ReturnMerchantCodeFromEditing()
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
        Private Sub DeleteDealer()
            Try
                State.MyBO.DeleteAndSave()
                State.HasDataChanged = True
                ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Delete, State.MyBO, State.HasDataChanged))
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub DoDelete()

            State.MyMerchantCodeBO = New MerchantCode(State.MerchantCodeID)
            Try
                State.MyMerchantCodeBO.Delete()
                State.MyMerchantCodeBO.Save()
                State.MyMerchantCodeBO.EndEdit()
                ' Me.State.MyMerchantCodeBO.Id = Guid.Empty
                State.MyMerchantCodeBO = Nothing
                State.MerchantCodeSearchDV = Nothing

            Catch ex As Exception
                State.MyBO.RejectChanges()
                Throw ex
            End Try

            ReturnMerchantCodeFromEditing()

            'Set the IsAfterSave flag to TRUE so that the Paging logic gets invoked
            State.IsEditMode = False
        End Sub

        Protected Sub CheckIfComingFromDeleteConfirm()
            Dim confResponse As String = HiddenDeletePromptResponse.Value
            Dim confResponseDel As String = HiddenDelDeletePromptResponse.Value
            If confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_YES Then
                If Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete Then
                    DoDelete()
                    'Clean after consuming the action
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                    HiddenDeletePromptResponse.Value = ""
                End If
            ElseIf confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_NO Then
                'Me.State.searchDV = Nothing
                ReturnMerchantCodeFromEditing()
                'Clean after consuming the action
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                HiddenDeletePromptResponse.Value = ""
            End If
            If confResponseDel IsNot Nothing AndAlso confResponseDel = MSG_VALUE_YES Then
                If Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete Then
                    DeleteDealer()
                    'Clean after consuming the action
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                    HiddenDelDeletePromptResponse.Value = ""
                End If
            ElseIf confResponseDel IsNot Nothing AndAlso confResponseDel = MSG_VALUE_NO Then
                'Clean after consuming the action
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                HiddenDelDeletePromptResponse.Value = ""
            End If
        End Sub
#End Region

#Region "Attach_Detach_buttons"

        Private Sub UserControlAvailableSelectedClaimTypes_Attach(aSrc As Generic.UserControlAvailableSelected_New, attachedList As System.Collections.ArrayList) Handles UserControlAvailableSelectedClaimTypes.Attach
            Try
                If attachedList.Count > 0 Then
                    State.MyBO.AttachClaimType(attachedList)
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub UserControlAvailableSelectedClaimTypes_Detach(aSrc As Generic.UserControlAvailableSelected_New, detachedList As System.Collections.ArrayList) Handles UserControlAvailableSelectedClaimTypes.Detach
            Try
                If detachedList.Count > 0 Then
                    State.MyBO.DetachClaimType(detachedList)
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub UserControlAvailableSelectedCoverageTypes_Attach(aSrc As Generic.UserControlAvailableSelected_New, attachedList As System.Collections.ArrayList) Handles UserControlAvailableSelectedCoverageTypes.Attach
            Try
                If attachedList.Count > 0 Then
                    State.MyBO.AttachCoverageType(attachedList)
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub UserControlAvailableSelectedCoverageTypes_Detach(aSrc As Generic.UserControlAvailableSelected_New, detachedList As System.Collections.ArrayList) Handles UserControlAvailableSelectedCoverageTypes.Detach
            Try
                If detachedList.Count > 0 Then
                    State.MyBO.DetachCoverageType(detachedList)
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region
#Region "REQ-5598 - ClaimCloseRules"
        Private Sub ClaimCloseRules_RequestClaimCloseRulesData(sender As Object, ByRef e As UserControlClaimCloseRules.RequestDataEventArgs) Handles ClaimCloseRules.RequestClaimCloseRulesData
            Dim claimCloseRules As New ClaimCloseRules
            claimCloseRules.CompanyId = State.Ocompany.Id
            claimCloseRules.DealerId = State.MyBO.Id
            e.Data = claimCloseRules.GetClaimCloseRules()
        End Sub

#End Region
#Region "Dealer Inflation and Risk Type Tolerance Delegate Handler"
        Private Sub DealerInflation_RequestDealerInflationData(sender As Object, ByRef e As UserControlDealerInflation.RequestDataEventArgs) Handles DealerInflation.RequestDealerInflationData
            Dim dlInflation As New DealerInflation
            dlInflation.DealerId = State.MyBO.Id
            e.Data = dlInflation.GetDealerInflation()
        End Sub
        Private Sub RiskTypeTolerance_RequestRiskTypeTolerance(sender As Object, ByRef e As UserControlRiskTypeTolerance.RequestDataEventArgs) Handles RiskTypeTolerance.RequestRiskTypeToleranceData
            Dim riskTolerance As New RiskTypeTolerance
            riskTolerance.DealerId = State.MyBO.Id
            e.Data = riskTolerance.GetRiskTypeTolerance()
        End Sub

#End Region

#Region "Ajax Autocomplete"
        <System.Web.Services.WebMethod(), Script.Services.ScriptMethod()>
        Public Shared Function PopulateSalvageCenterDrop(prefixText As String, count As Integer) As String()
            Dim dv As DataView = LookupListNew.GetServiceCenterLookupList(ElitaPlusIdentity.Current.ActiveUser.Countries())
            Return AjaxController.BindAutoComplete(prefixText, dv)
        End Function


#End Region
        Private Sub DealerForm_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
            hdnDisabledTab.Value = String.Join(",", DisabledTabsList)
        End Sub
    End Class
End Namespace
