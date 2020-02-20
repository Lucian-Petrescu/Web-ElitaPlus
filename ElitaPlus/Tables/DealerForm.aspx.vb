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


        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
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
                Return Me.State.IsMerchantCodeNew
            End Get
            Set(ByVal Value As Boolean)
                Me.State.IsMerchantCodeNew = Value
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

        Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall

            Try
                If Not Me.CallingParameters Is Nothing Then
                    'Get the id from the parent
                    Me.State.MyBO = New Dealer(CType(Me.CallingParameters, Guid))
                    Me.State.IsCertificateExists = (Me.State.MyBO.GetDealerCertificatesCount() > 0)
                    Me.State.Ocompany = New Company(Me.State.MyBO.CompanyId)
                Else
                    Me.State.IsNew = True
                    Me.State.IsCertificateExists = False
                    Me.State.Ocompany = New Company(CType(ElitaPlusIdentity.Current.ActiveUser.Companies.Item(0), Guid))
                    cboImeiNoUse.Enabled = "True"
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

        Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
            Try
                Me.MenuEnabled = True
                Me.IsReturningFromChild = True
                Dim retObj As ServiceCenterForm.ReturnType = CType(ReturnPar, ServiceCenterForm.ReturnType)
                Me.State.HasDataChanged = retObj.HasDataChanged
                Select Case retObj.LastOperation
                    Case ElitaPlusPage.DetailPageCommand.Back
                        If Not retObj Is Nothing Then
                            Me.State.MyBO = New Dealer(retObj.EditingBo.OriginalDealerId)
                        End If
                    Case ElitaPlusPage.DetailPageCommand.Save
                        If Not retObj Is Nothing Then
                            Me.State.MyBO = New Dealer(retObj.EditingBo.OriginalDealerId)
                            Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                        End If
                End Select
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Page Return Type"
        Public Class ReturnType
            Public LastOperation As DetailPageCommand
            Public EditingBo As Dealer
            Public HasDataChanged As Boolean
            Public Sub New(ByVal LastOp As DetailPageCommand, ByVal curEditingBo As Dealer, ByVal hasDataChanged As Boolean)
                Me.LastOperation = LastOp
                Me.EditingBo = curEditingBo
                Me.HasDataChanged = hasDataChanged
            End Sub
        End Class
#End Region

#Region "Page Events"
        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load
            'Put user code to initialize the page here
            If mbIsFirstPass = True Then
                mbIsFirstPass = False
            Else
                ' Do not load again the Page that was already loaded
                Return
            End If
            Me.MasterPage.MessageController.Clear_Hide()
            'hide the user control...since we are doing our ownlist.
            'ControlMgr.SetVisibleControl(Me, PostalCodeFormatLists, False)
            Try
                If Not Me.IsPostBack Then
                    Me.MasterPage.MessageController.Clear()
                    Me.MasterPage.UsePageTabTitleInBreadCrum = False
                    Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Tables")
                    UpdateBreadCrum()
                    'Date Calendars
                    Me.MenuEnabled = False
                    'Me.AddConfirmation(Me.btnDelete_WRITE, Message.DELETE_RECORD_PROMPT)
                    IsNewMerchantCode = False
                    If Me.State.MyBO Is Nothing Then
                        Me.State.MyBO = New Dealer
                    End If
                    If Me.State.companyDV Is Nothing Then
                        Me.State.companyDV = LookupListNew.GetUserCompaniesLookupList()
                    End If
                    Me.State.MyBO.UseSvcOrderAddress = False
                    If LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, Me.State.Ocompany.ServiceOrdersByDealerId) = Codes.YESNO_Y Then
                        Me.State.MyBO.UseSvcOrderAddress = True
                    End If
                    PopulateCompanyDropDown()
                    PopulateDropdowns()
                    PopulateAddressFields()
                    PopulateMailingAddressFields()
                    If Me.State.IsNew = True Then
                        CreateNew()
                    End If
                    If Me.State.MyBO.UseSvcOrderAddress Then
                        Me.State.SvcOrdersByDealer = Me.State.MyBO.SvcOrdersAddress
                        PopulateSvcOrdersAddressFields()
                    End If

                    moBankInfo.ReAssignTabIndex(BankInfoStartIndex)
                    If State.MyBO.CompanyId <> Guid.Empty Then
                        State.DealerCountryID = New Company(State.MyBO.CompanyId).CountryId
                    End If

                    AttributeValues.ParentBusinessObject = CType(Me.State.MyBO, IAttributable)
                    'AttributeValues.TranslateHeaders()--Commented since it is calling two times
                    If Not Me.State.IsNew = True Then ' Added condition since we are already calling PopulateFormFromBOs during creation of new dealer
                        Me.PopulateFormFromBOs()
                    End If
                    Me.EnableDisableFields()
                    Me.CheckUseClientDealerCodeFlag()
                    Me.PopulateMyMerchantCodeGrid()

                    'If Me.State.MerchantCodeUsed Then
                    SetMerchantCodeButtonsState(False)
                    If Not Me.State.MerchantCodeGridTranslated Then
                        Me.TranslateGridHeader(Me.moMerchantCodesDatagrid)
                        'Me.TranslateGridControls(Me.moMerchantCodesDatagrid)
                        Me.State.MerchantCodeGridTranslated = True
                    End If

                    Me.PopulateMyMerchantCodeGrid()
                    ' End If
                Else
                    AttributeValues.ParentBusinessObject = CType(Me.State.MyBO, IAttributable)
                    CheckIfComingFromDeleteConfirm()
                    GetDisabledTabs()
                End If

                'CheckIfComingFromSaveConfirm()
                AttributeValues.BindBoProperties()
                BindBoPropertiesToLabels()
                CheckIfComingFromSaveConfirm()


                If Not Me.IsPostBack Then
                    Me.AddLabelDecorations(Me.State.MyBO)
                End If

                ShareCustLookup()

            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                ' Clean Popup Input
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                Me.HiddenSaveChangesPromptResponse.Value = ""
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
            Me.ShowMissingTranslations(Me.MasterPage.MessageController)
        End Sub
#End Region
        Private Sub ShareCustLookup()
            If Me.cboShareCustomers.SelectedValue = SHARE_CUSTOMER Then
                Me.cboCustomerIdLookUpBy.ClearSelection()
                Me.cboCustomerIdLookUpBy.SelectedIndex = 0
                Me.PopulateBOProperty(Me.State.MyBO, "CustomerLookup", Me.cboCustomerIdLookUpBy, False, True)
                Me.cboCustomerIdLookUpBy.Enabled = False
            Else
                Me.cboCustomerIdLookUpBy.Enabled = True
            End If

        End Sub
        Private Sub ShareCustSave()
            If cboShareCustomers.SelectedValue <> SHARE_CUSTOMER Then
                Me.cboCustomerIdLookUpBy.Enabled = True
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

                If Not Me.State.MyBO.Id.Equals(Guid.Empty) Then
                    'Available
                    Dim availableClaimTypesDv As DataView
                    Dim availableCoverageTypesDv As DataView

                    Dim availClaimTypeDS As DataSet = Me.State.MyBO.GetAvailablClaimTypes(Me.State.MyBO.Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                    If availClaimTypeDS.Tables.Count > 0 Then
                        availableClaimTypesDv = New DataView(availClaimTypeDS.Tables(0))
                    End If

                    Dim availCoverageTypeDS As DataSet = Me.State.MyBO.GetAvailableCoverageTypes(Me.State.MyBO.Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                    If availCoverageTypeDS.Tables.Count > 0 Then
                        availableCoverageTypesDv = New DataView(availCoverageTypeDS.Tables(0))
                    End If

                    Me.UserControlAvailableSelectedClaimTypes.AvailableList.Clear()
                    Me.UserControlAvailableSelectedCoverageTypes.AvailableList.Clear()
                    Me.UserControlAvailableSelectedClaimTypes.SetAvailableData(availableClaimTypesDv, LookupListNew.COL_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)
                    Me.UserControlAvailableSelectedCoverageTypes.SetAvailableData(availableCoverageTypesDv, LookupListNew.COL_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)
                    Me.UserControlAvailableSelectedClaimTypes.AvailableDesc = TranslationBase.TranslateLabelOrMessage(AVAILABLE_CLAIM_TYPES)
                    Me.UserControlAvailableSelectedCoverageTypes.AvailableDesc = TranslationBase.TranslateLabelOrMessage(AVAILABLE_COVERAGE_TYPES)

                    'Selected
                    Dim selectedClaimTypesDv As DataView
                    Dim selectedCoverageTypesDv As DataView

                    Dim selectedClaimTypeDS As DataSet = Me.State.MyBO.GetSelectedClaimTypes(Me.State.MyBO.Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                    If selectedClaimTypeDS.Tables.Count > 0 Then
                        selectedClaimTypesDv = New DataView(selectedClaimTypeDS.Tables(0))
                    End If

                    Dim selectedCoverageTypeDS As DataSet = Me.State.MyBO.GetSelectedCoverageTypes(Me.State.MyBO.Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                    If selectedCoverageTypeDS.Tables.Count > 0 Then
                        selectedCoverageTypesDv = New DataView(selectedCoverageTypeDS.Tables(0))
                    End If

                    Me.UserControlAvailableSelectedClaimTypes.SelectedList.Clear()
                    Me.UserControlAvailableSelectedCoverageTypes.SelectedList.Clear()
                    Me.UserControlAvailableSelectedClaimTypes.SetSelectedData(selectedClaimTypesDv, LookupListNew.COL_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)
                    Me.UserControlAvailableSelectedCoverageTypes.SetSelectedData(selectedCoverageTypesDv, LookupListNew.COL_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)
                    Me.UserControlAvailableSelectedClaimTypes.SelectedDesc = TranslationBase.TranslateLabelOrMessage(SELECTED_CLAIM_TYPES)
                    Me.UserControlAvailableSelectedCoverageTypes.SelectedDesc = TranslationBase.TranslateLabelOrMessage(SELECTED_COVERAGE_TYPES)

                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub


#Region "Controlling Logic"

        Protected Sub EnableDisableFields()
            'Enabled by Default
            If Me.State.IsCertificateExists Then
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
            If LookupListNew.GetCodeFromId(LookupListNew.LK_FLP, Me.State.MyBO.UseFullFileProcessId) <> Codes.FLP_NO Then
                lblMaxNCRecords.Visible = True
                txtMaxNCRecords.Visible = True
            End If
            'Req-1297 End

            If Me.State.MyBO.IsNew Then
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

            Me.State.MyBO.Address.AddressIsRequire = False
            'Req-1142 start
            ControlMgr.SetVisibleControl(Me, trVscLicenseTag, False)
            'Req-1142 end

            'Req-5723 start
            ControlMgr.SetVisibleControl(Me, trVscVinRestrict, False)
            'Req-5723 end

            If Me.State.MyBO.DealerTypeDesc = Me.State.MyBO.DEALER_TYPE_DESC Then
                ControlMgr.SetVisibleControl(Me, trLineHid1, True)
                ControlMgr.SetVisibleControl(Me, trHid1, True)
                ControlMgr.SetVisibleControl(Me, trHid2, True)
                MailingAddressCtr.EnableControls(False, True)
                ControlMgr.SetVisibleControl(Me, dlstRetailerID, False)
                ControlMgr.SetVisibleControl(Me, lblDealerIsRetailer, False)
                ControlMgr.SetVisibleControl(Me, lblValidateSerialNumber, False)
                ControlMgr.SetVisibleControl(Me, moValidateSerialNumberDrop, False)
                Me.State.MyBO.Address.AddressIsRequire = True
                'Req-1142 start
                ControlMgr.SetVisibleControl(Me, trVscLicenseTag, True)
                'Req-1142 End
                'Req-5723 start
                ControlMgr.SetVisibleControl(Me, trVscVinRestrict, True)
                'Req-5723 End
            End If

            If Me.State.MyBO.CertificatesAutonumberId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_Y)) Then
                ControlMgr.SetVisibleControl(Me, Me.lblCertificatesAutonumberPrefix, True)
                ControlMgr.SetVisibleControl(Me, Me.txtCertificatesAutonumberPrefix, True)
                ControlMgr.SetVisibleControl(Me, Me.lblMaxCertNumLengthAlwd, True)
                ControlMgr.SetVisibleControl(Me, Me.txtMaxCertNumLengthAlwd, True)
            Else
                txtCertificatesAutonumberPrefix.Text = String.Empty
                ControlMgr.SetVisibleControl(Me, Me.lblCertificatesAutonumberPrefix, False)
                ControlMgr.SetVisibleControl(Me, Me.txtCertificatesAutonumberPrefix, False)
                txtMaxCertNumLengthAlwd.Text = String.Empty
                ControlMgr.SetVisibleControl(Me, Me.lblMaxCertNumLengthAlwd, False)
                ControlMgr.SetVisibleControl(Me, Me.txtMaxCertNumLengthAlwd, False)
            End If

            ControlMgr.SetEnableControl(Me, Me.cboCertificatesAutonumberId, True)
            ControlMgr.SetEnableControl(Me, Me.txtCertificatesAutonumberPrefix, True)

            If Not Me.State.MyBO.IsNew AndAlso Me.State.MyBO.IsLastContractPolicyAutoGenerated() Then
                ' Individual policy change (US 258694. Disable if Last contract policy no is auto generated)
                ControlMgr.SetEnableControl(Me, Me.cboCertificatesAutonumberId, False)
                ControlMgr.SetEnableControl(Me, Me.txtCertificatesAutonumberPrefix, False)
            End If

            'Enable only for BENEFIT dealer
            ControlMgr.SetVisibleControl(Me, trBenefitDlrTypeCtls1, False)
            ControlMgr.SetVisibleControl(Me, trBenefitDlrTypeCtls2, False)
            ControlMgr.SetVisibleControl(Me, trBenefitDlrTypeCtls3, False)

            If Me.State.MyBO.DealerTypeDesc = Me.State.MyBO.DEALER_TYPE_DESC_WEPP Then
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
            ElseIf (Not State.MyBO.DealerTypeDesc Is Nothing AndAlso State.MyBO.DealerTypeDesc.ToUpper() = Me.State.MyBO.DEALER_TYPE_BENEFIT) Then
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

            Me.SvcOrderAddressCtr.EnableControls(False, True)
            If Not Me.State.MyBO.UseSvcOrderAddress Then
                DisabledTabsList.Add(Tab_ServiceOrderAddress)
            End If

            If Not Me.State.Ocompany.CertnumlookupbyId.Equals(Guid.Empty) Then
                ControlMgr.SetVisibleControl(Me, Me.lblCertNumLookUpBy, False)
                ControlMgr.SetVisibleControl(Me, Me.ddlCertNumLookUpBy, False)
            Else
                ControlMgr.SetVisibleControl(Me, Me.lblCertNumLookUpBy, True)
                ControlMgr.SetVisibleControl(Me, Me.ddlCertNumLookUpBy, True)
            End If

            If Not Me.State.MyBO.ClaimSystemId.Equals(System.Guid.Empty) AndAlso Me.State.MyBO.ClaimSystemId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_CLAIM_SYSTEM, "ELITA")) Then
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
            Dim dealergrpID As Guid = Me.GetSelectedItem(Me.moDealerGroupDrop)
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
            Me.BindBOPropertyToLabel(Me.State.MyBO, "CompanyId", moMultipleColumnDrop.CaptionLabel)
            'Me.BindBOPropertyToLabel(Me.State.MyBO, "CompanyId", CompanyMultipleDrop.CaptionLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "Dealer", Me.lblDealerCode)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "ClientDealerCode", Me.lblClientDealerCode)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "DealerName", Me.lblDealerName)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "RetailerId", Me.lblDealerIsRetailer)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "TaxIdNumber", Me.lblTaxId)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "DealerGroupId", Me.lblDealerGroupel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "ContactName", Me.lblContactName)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "ContactPhone", Me.lblContactPhone)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "ContactFax", Me.lblContactFax)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "ContactEmail", Me.lblContactEmail)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "ContactExt", Me.lblContactPhoneExt)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "ServiceNetworkId", Me.lblServiceNetwork)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "IBNRComputationMethodId", Me.lblIBNR_COMPUTATION_METHOD)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "IBNRFactor", Me.lblIBNR_FACTOR)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "STATIBNRComputationMethodId", Me.lblSTATIBNR_COMPUT_MTHD)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "STATIBNRFactor", Me.lblSTATIBNR_FACTOR)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "LAEIBNRComputationMethodId", Me.lblLAEIBNR_COMPUT_MTHD)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "LAEIBNRFactor", Me.lblLAEIBNR_FACTOR)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "ConvertProductCodeId", Me.LabelConvertProdCode)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "DealerTypeId", Me.lblDealerType)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "BranchValidationId", Me.lblBranchValidation)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "RequireCustomerAMLInfoId", Me.lblRequireCustomerAMLInfo)

            'REQ-1297
            Me.BindBOPropertyToLabel(Me.State.MyBO, "UseFullFileProcessId", Me.lblFullfileDealer)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "MaxNCRecords", Me.lblMaxNCRecords)
            'Req-1297 end
            Me.BindBOPropertyToLabel(Me.State.MyBO, "EditBranchId", Me.lblEditBranch)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "DelayFactorFlagId", Me.LabelDelayFactor)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "InstallmentFactorFlagId", Me.LabelInstallmentFactor)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "RegistrationProcessFlagId", Me.moRegistrationProcessLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "RegistrationEmailFrom", Me.moRegistrationEmailFromLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "UseWarrantyMasterID", Me.moUseWarrantyMasterLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "UseIncomingSalesTaxID", Me.moUseIncomingSalesTaxLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "AutoProcessFileID", Me.moUseWarrantyMasterLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "AutoProcessPymtFileID", Me.moAutoProcessPymtFileLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "AutoRejErrTypeId", Me.lblAutoRejErrType)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "ReconRejRecTypeId", Me.lblReconRejRecType)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "PriceMatrixUsesWpId", Me.lblPriceMatrix)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "OlitaSearch", Me.lblOlitaSearch)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "CancellationRequestFlagId", Me.lblCancelRequestFlag)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "MaxManWarr", Me.lblMaxManWarr)
            'Req-877
            Me.BindBOPropertyToLabel(Me.State.MyBO, "MinManWarr", Me.lblMinManWarr)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "BusinessName", Me.lblBusinessName)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "StateTaxIdNumber", Me.lblStateTaxIdNumber)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "CityTaxIdNumber", Me.lblCityTaxIdNumber)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "WebAddress", Me.lblWebAddress)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "NumberOfOtherLocations", Me.lblNumbOfOtherLocations)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "NumberOfOtherLocations", Me.lblNumbOfOtherLocations)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "ManualEnrollmentAllowedId", Me.lblManualEnrollmentAllowed)

            'REQ-5761
            Me.BindBOPropertyToLabel(Me.State.MyBO, "UseNewBillForm", Me.lblUseNewBillPay)

            'Me.BindBOPropertyToLabel(Me.State.MyBO, "ShareCustomers", Me.lblShareCustomers)
            'Me.BindBOPropertyToLabel(Me.State.MyBO, "CustomerIdentityLookup", Me.lblCustomerIdLookUpBy)

            Me.BindBOPropertyToLabel(Me.State.MyBO, "RoundCommFlagId", Me.moRoundCommLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "CertCancelById", Me.moCancelByLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "UseInstallmentDefnId", Me.moUseInstallmentDefnLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "ProgramName", Me.moProgramNameLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "ExpectedPremiumIsWPId", Me.lblExpectedPremiumIsWP)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "ClaimSystemId", Me.lblClaimSystem)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "BestReplacementGroupId", Me.lblBestReplacement)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "UseEquipmentId", Me.lblUseEquipment)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "EquipmentListCode", Me.lblEquipmentList)
            'REQ-860 Elita Buildout - Issues/Adjudication
            Me.BindBOPropertyToLabel(Me.State.MyBO, "QuestionListCode", Me.lblQuestionList)
            'Req-846
            Me.BindBOPropertyToLabel(Me.State.MyBO, "ValidateSKUId", Me.lblskunumber)

            'REQ-874
            Me.BindBOPropertyToLabel(Me.State.MyBO, "ValidateBillingCycleId", Me.lblValidateBillingCycleId)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "ValidateSerialNumberId", Me.lblValidateSerialNumber)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "PayDeductibleId", Me.lblPayDeductible)
            'REQ-1294
            'Me.BindBOPropertyToLabel(Me.State.MyBO, "CustInfoMandatoryId", Me.lblCustInfoMandatory)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "BankInfoMandatoryId", Me.lblBankInfoMandatory)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "DeductibleCollectionId", Me.lblCollectDeductible)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "AssurantIsObligorId", Me.lblAObligor)

            If Me.State.MyBO.UseSvcOrderAddress Then
                Me.BindBOPropertyToLabel(Me.State.SvcOrdersByDealer, "Name", Me.lblName)
                Me.BindBOPropertyToLabel(Me.State.SvcOrdersByDealer, "TaxIdNumber", Me.lblOtherTaxId)
            End If

            'Me.BindBOPropertyToLabel(Me.State.MyBO, "AuthAmtBasedOnId", Me.lblAuthAmtBasedOn)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "ProductByRegionId", Me.lblProdByRegion)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "ClaimVerfificationNumLength", Me.lblClaimVerfificationNumLength)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "ClaimExtendedStatusEntryId", Me.lblClaim_Extended_Status_Entry)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "AllowUpdateCancellationId", Me.lblAlwupdateCancel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "RejectAfterCancellationId", Me.lblRejectaftercancel)

            Me.BindBOPropertyToLabel(Me.State.MyBO, "AllowUpdateCancellationId", Me.lblAlwupdateCancel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "RejectAfterCancellationId", Me.lblRejectaftercancel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "AllowFutureCancelDateId", Me.lblAllowfuturecancel)
            'REQ-1153 
            Me.BindBOPropertyToLabel(Me.State.MyBO, "DealerSupportWebClaimsId", Me.lblDEALER_SUPPORT_WEB_CLAIMS)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "ClaimStatusForExtSystemId", Me.lblCLAIM_STATUS_FOR_EXT_SYSTEM)
            'REQ-1153 end

            'Req 1157
            Me.BindBOPropertyToLabel(Me.State.MyBO, "NewDeviceSkuRequiredId", Me.lblReplacementSKURequired)
            'req 1157
            Me.BindBOPropertyToLabel(Me.State.MyBO, "IsLawsuitMandatoryId", Me.lblIsLawsuitMandatory)


            'REQ-1190
            Me.BindBOPropertyToLabel(Me.State.MyBO, "EnrollfilepreprocessprocId", Me.lblEnrFilePreProcess)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "CertnumlookupbyId", Me.lblCertNumLookUpBy)
            'REQ-1190
            'Req 1157 end
            'Req-1142
            Me.BindBOPropertyToLabel(Me.State.MyBO, "LicenseTagValidationId", Me.lblLicenseTagMandatory)
            'Req-1142 end
            'Req-5723 Start
            Me.BindBOPropertyToLabel(Me.State.MyBO, "VINRestrictMandatoryId", Me.lblVinrestrictMandatory)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "PlanCodeInQuote", Me.lblplancodeinquote)
            'Req-5723 end
            'REQ-1244
            Me.BindBOPropertyToLabel(Me.State.MyBO, "Replaceclaimdedtolerancepct", Me.lblRepClaimDedTolerancePct)

            'REQ_1274
            Me.BindBOPropertyToLabel(Me.State.MyBO, "BillingProcessCodeId", Me.lblBillingProcessCode)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "BillresultExceptionDestId", Me.lblBillResultExpFTPSite)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "BillresultNotificationEmail", Me.lblBillResultNotifyEmail)

            Me.BindBOPropertyToLabel(Me.State.MyBO, "CertificatesAutonumberId", Me.lblCertificatesAutonumberId)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "CertificatesAutonumberPrefix", Me.lblCertificatesAutonumberPrefix)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "MaximumCertNumberLengthAllowed", Me.lblMaxCertNumLengthAlwd)

            Me.BindBOPropertyToLabel(Me.State.MyBO, "FileLoadNotificationEmail", Me.lblFileLoadNotificationEmail)

            '5623
            Me.BindBOPropertyToLabel(Me.State.MyBO, "GracePeriodMonths", Me.lblGracePeriod)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "GracePeriodDays", Me.lblGracePeriod)
            '---end 5623

            'REQ-5466
            Me.BindBOPropertyToLabel(Me.State.MyBO, "AutoSelectServiceCenter", Me.lblAutoSelectSvcCenter)
            'REQ-5586
            Me.BindBOPropertyToLabel(Me.State.MyBO, "PolicyEventNotificationEmail", Me.lblPolicyEventNotifyEmail)

            Me.BindBOPropertyToLabel(Me.State.MyBO, "ClaimAutoApproveId", Me.lblClaimAutoApprove)

            Me.BindBOPropertyToLabel(Me.State.MyBO, "DefaultSalvgeCenterId", Me.moDefaultSalvageCenterLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "ReuseSerialNumberId", Me.lblReuseSerialNumber)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "MaximumCommissionPercent", Me.lblMaxCommissionPercent)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "AutoGenerateRejectedPaymentFileId", Me.lblAutoGenRejPymtFile)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "PaymentRejectedRecordReconcileId", Me.lblPymtRejRecRecon)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "IdentificationNumberType", Me.lblIdentificationNumberType)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "ShareCustomers", Me.lblShareCustomers)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "CustomerLookup", Me.lblCustomerIdLookUpBy)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "UseQuote", Me.lblUseQuote)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "ContractManualVerification", Me.lblContractManualVerification)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "ImeiUseXcd", Me.lblImeiNoUse)
            'REQ-6155
            Me.BindBOPropertyToLabel(Me.State.MyBO, "ClaimRecordingXcd", Me.lblClaimRecording)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "EnableFraudMonitoringId", Me.lblUseFraudMonitoring)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "InvoiceCutoffDay", Me.lblInvoiceCutOffDay)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "BenefitSoldToAccount", Me.lblBenefitSoldToAccount)

            Me.BindBOPropertyToLabel(Me.State.MyBO, "CloseCaseGracePeriodDays", Me.lblClosecaseperiod)

            Me.ClearGridViewHeadersAndLabelsErrorSign()
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
            Me.moDealerGroupDrop.Populate(CommonConfigManager.Current.ListManager.GetList("DealerGroupByCompanyGroup", Thread.CurrentPrincipal.GetLanguageCode(), listcontext1), populateOptions)
            'BindListControlToDataView(Me.dlstRetailerID, yesNoLkL)
            Me.dlstRetailerID.Populate(oYesNoList, populateOptions)
            'BindListControlToDataView(Me.moServiceNetworkDrop, LookupListNew.GetServiceNetworkLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id), , , True)
            Me.moServiceNetworkDrop.Populate(CommonConfigManager.Current.ListManager.GetList("ServiceNetworkByCompanyGroup", Thread.CurrentPrincipal.GetLanguageCode(), listcontext1), populateOptions)

            'BindListControlToDataView(Me.moIBNR_COMPUTATION_METHODDropd, LookupListNew.DropdownLookupList(LookupListNew.LK_IBNR_COMPUTE_METHODS, langId, True))
            Me.moIBNR_COMPUTATION_METHODDropd.Populate(CommonConfigManager.Current.ListManager.GetList("IBNR", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions)
            'BindListControlToDataView(Me.moSTATIBNR_COMPUT_MTHDDropd, LookupListNew.DropdownLookupList(LookupListNew.LK_STAT_IBNR_COMPUTE_METHODS, langId, True))
            Me.moSTATIBNR_COMPUT_MTHDDropd.Populate(CommonConfigManager.Current.ListManager.GetList("STAT_IBNR", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions)
            'BindListControlToDataView(Me.moLAEIBNR_COMPUT_MTHDDropd, LookupListNew.DropdownLookupList(LookupListNew.LK_LAE_IBNR_COMPUTE_METHODS, langId, True))
            Me.moLAEIBNR_COMPUT_MTHDDropd.Populate(CommonConfigManager.Current.ListManager.GetList("LAE_IBNR", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions)
            'BindListControlToDataView(Me.moClaimSystemDrop, LookupListNew.GetClaimSystemLookupList())
            Me.moClaimSystemDrop.Populate(CommonConfigManager.Current.ListManager.GetList("ClaimSystem", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions)
            'BindListControlToDataView(Me.moBestReplacementDrop, LookupListNew.GetBestReplacementLookupList())
            Me.moBestReplacementDrop.Populate(CommonConfigManager.Current.ListManager.GetList("BestReplacement", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions)
            'BindListControlToDataView(Me.moEquipmentListDrop, LookupListNew.GetEquipmentListLookupList(DateTime.Now))
            Me.moEquipmentListDrop.Populate(CommonConfigManager.Current.ListManager.GetList("CurrentEquipmentList", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions4)

            'BindListControlToDataView(Me.moUseEquipment, LookupListNew.GetYesNoLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId))
            Me.moUseEquipment.Populate(oYesNoList, populateOptions)

            'Dim flFileProcess As DataView = LookupListNew.DropdownLookupList("FLP", langId, True)
            'BindListControlToDataView(Me.ddlFullfileProcess, flFileProcess, "DESCRIPTION", "ID", False)
            Me.ddlFullfileProcess.Populate(CommonConfigManager.Current.ListManager.GetList("FLP", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions1)
            'BindListControlToDataView(Me.moSkuNumberDrop, LookupListNew.DropdownLookupList(LookupListNew.LK_SKU_VALIDATION_CODE, langId, True))
            Me.moSkuNumberDrop.Populate(CommonConfigManager.Current.ListManager.GetList("SKUVAL", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions)

            'BindListControlToDataView(Me.moValidateBillingCycleIdDrop, yesNoLkL, "DESCRIPTION", "ID", False)
            Me.moValidateBillingCycleIdDrop.Populate(oYesNoList, populateOptions1)
            'BindListControlToDataView(Me.moValidateSerialNumberDrop, LookupListNew.DropdownLookupList(LookupListNew.LK_SERIAL_NUMBER_VALIDATION, langId, True), "DESCRIPTION", "ID", False)
            Me.moValidateSerialNumberDrop.Populate(CommonConfigManager.Current.ListManager.GetList("SNV", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions1)

            'BindListControlToDataView(Me.moUseClaimAutorization, yesNoLkL, "DESCRIPTION", "ID", False)
            Me.moUseClaimAutorization.Populate(oYesNoList, populateOptions1)
            'BindListControlToDataView(Me.moPayDeductible, LookupListNew.GetYesNoLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId))
            Me.moPayDeductible.Populate(oYesNoPayDeductList, populateOptions)
            'BindListControlToDataView(Me.moBankInfoMandatory, LookupListNew.GetYesNoLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), , , False)
            Me.moBankInfoMandatory.Populate(oYesNoList, populateOptions1)
            'BindListControlToDataView(Me.moCollectDeductible, LookupListNew.GetYesNoLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), , , False)
            Me.moCollectDeductible.Populate(oYesNoList, populateOptions1)

            'BindListControlToDataView(Me.cboConvertProdCode, LookupListNew.DropdownLookupList("TPRDC", langId, True))
            Me.cboConvertProdCode.Populate(CommonConfigManager.Current.ListManager.GetList("TPRDC", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions)
            'BindListControlToDataView(Me.moDealerTypeDrop, LookupListNew.DropdownLookupList(LookupListNew.LK_DEALER_TYPE_CODE, langId, True))
            Me.moDealerTypeDrop.Populate(CommonConfigManager.Current.ListManager.GetList("DLTYP", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions)

            'BindListControlToDataView(Me.moBranchValidationDrop, yesNoLkL)
            Me.moBranchValidationDrop.Populate(oYesNoList, populateOptions)
            'BindListControlToDataView(Me.moEditBranchDrop, yesNoLkL)
            Me.moEditBranchDrop.Populate(oYesNoList, populateOptions)
            'BindListControlToDataView(Me.moRoundCommId, yesNoLkL, "DESCRIPTION", "ID", False)
            Me.moRoundCommId.Populate(oYesNoList, populateOptions1)
            'BindListControlToDataView(Me.moDelayFactorDrop, yesNoLkL, "DESCRIPTION", "ID", False)
            Me.moDelayFactorDrop.Populate(oYesNoList, populateOptions1)
            'BindListControlToDataView(Me.moInstallmentFactorDrop, yesNoLkL, "DESCRIPTION", "ID", False)
            Me.moInstallmentFactorDrop.Populate(oYesNoList, populateOptions1)
            'BindListControlToDataView(Me.moRegistrationProcessDrop, yesNoLkL, "DESCRIPTION", "ID", False)
            Me.moRegistrationProcessDrop.Populate(oYesNoList, populateOptions1)
            'BindListControlToDataView(Me.moUseWarrantyMasterDrop, yesNoLkL, "DESCRIPTION", "ID", False)
            Me.moUseWarrantyMasterDrop.Populate(oYesNoList, populateOptions1)
            'BindListControlToDataView(Me.moInsertIfMakeNotExists, yesNoLkL, "DESCRIPTION", "ID", False)
            Me.moInsertIfMakeNotExists.Populate(oYesNoList, populateOptions1)
            'BindListControlToDataView(Me.moUseIncomingSalesTaxDrop, yesNoLkL, "DESCRIPTION", "ID", False)
            Me.moUseIncomingSalesTaxDrop.Populate(oYesNoList, populateOptions1)
            'BindListControlToDataView(Me.moCancelRequestFlagDrop, yesNoLkL, "DESCRIPTION", "ID", False)
            Me.moCancelRequestFlagDrop.Populate(oYesNoList, populateOptions1)
            'BindListControlToDataView(Me.moAutoProcessFileDrop, yesNoLkL, "DESCRIPTION", "ID", False)
            Me.moAutoProcessFileDrop.Populate(oYesNoList, populateOptions1)
            'BindListControlToDataView(Me.moAutoProcessPymtFiledrop, yesNoLkL, "DESCRIPTION", "ID", False)
            Me.moAutoProcessPymtFiledrop.Populate(oYesNoList, populateOptions1)
            'BindListControlToDataView(Me.moPriceMatrixDrop, yesNoLkL)
            Me.moPriceMatrixDrop.Populate(oYesNoList, populateOptions)
            'BindListControlToDataView(Me.ddlInvByBranch, yesNoLkL, "DESCRIPTION", "ID", False)
            Me.ddlInvByBranch.Populate(oYesNoList, populateOptions1)
            'BindListControlToDataView(Me.ddlSeparatedCN, yesNoLkL, "DESCRIPTION", "ID", False)
            Me.ddlSeparatedCN.Populate(oYesNoList, populateOptions1)
            'BindListControlToDataView(Me.moManualEnrollmentAllowedId, yesNoLkL, "DESCRIPTION", "ID", False)
            Me.moManualEnrollmentAllowedId.Populate(oYesNoList, populateOptions1)
            'BindListControlToDataView(Me.cboUseNewBillPay, yesNoLkL, "DESCRIPTION", "ID", False)
            Me.cboUseNewBillPay.Populate(oYesNoList, populateOptions1)

            'Me.cboShareCustomers.PopulateOld("SHARE_CUSTOMER", ListValueType.Description, ListValueType.ExtendedCode, PopulateBehavior.None, String.Empty, ListValueType.Description)
            Me.cboShareCustomers.Populate(CommonConfigManager.Current.ListManager.GetList("SHARE_CUSTOMER", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions2)
            'Me.cboCustomerIdLookUpBy.PopulateOld("DLR_CUST_IDNTY_LOOKUP", ListValueType.Description, ListValueType.ExtendedCode, PopulateBehavior.AddBlankListItem, String.Empty, ListValueType.Description)
            Me.cboCustomerIdLookUpBy.Populate(CommonConfigManager.Current.ListManager.GetList("DLR_CUST_IDNTY_LOOKUP", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions3)
            'Me.cboIdentificationNumberType.PopulateOld("CUSTOMER_IDENTIFICATION_TYPE", ListValueType.Description, ListValueType.ExtendedCode, PopulateBehavior.None, String.Empty, ListValueType.Description)
            Me.cboIdentificationNumberType.Populate(CommonConfigManager.Current.ListManager.GetList("CUSTOMER_IDENTIFICATION_TYPE", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions2)
            'Me.cboUseQuote.PopulateOld("YESNO", ListValueType.Description, ListValueType.ExtendedCode, PopulateBehavior.None, String.Empty, ListValueType.Description)
            Me.cboUseQuote.Populate(oYesNoList, populateOptions2)
            'Me.cboContractManualVerification.PopulateOld("YESNO", ListValueType.Description, ListValueType.ExtendedCode, PopulateBehavior.None, String.Empty, ListValueType.Description)
            Me.cboContractManualVerification.Populate(oYesNoList, populateOptions2)
            'Me.moAcceptPaymentByCheckDrop.PopulateOld("YESNO", ListValueType.Description, ListValueType.ExtendedCode, PopulateBehavior.None, String.Empty, ListValueType.Description)
            Me.moAcceptPaymentByCheckDrop.Populate(oYesNoList, populateOptions2)
            'Me.moClaimRecordingDrop.PopulateOld("CLMREC", ListValueType.Description, ListValueType.ExtendedCode, PopulateBehavior.None, String.Empty, ListValueType.Description)
            Me.moClaimRecordingDrop.Populate(CommonConfigManager.Current.ListManager.GetList("CLMREC", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions2)
            Me.ddlUseFraudMonitoring.Populate(oYesNoFraudMonitorList, populateOptions2)
            'Me.cboImeiNoUse.PopulateOld("IMEI_USE_LST", ListValueType.Description, ListValueType.ExtendedCode, PopulateBehavior.None, String.Empty, ListValueType.Description)
            Me.cboImeiNoUse.Populate(CommonConfigManager.Current.ListManager.GetList("IMEI_USE_LST", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions2)

            'BindListControlToDataView(Me.moDEALER_SUPPORT_WEB_CLAIMS, yesNoLkL, "DESCRIPTION", "ID", False)
            Me.moDEALER_SUPPORT_WEB_CLAIMS.Populate(oYesNoList, populateOptions1)
            'BindListControlToDataView(Me.moExtSystemClaimStatus, LookupListNew.DropdownLookupList(LookupListNew.LK_CLAIM_STATUS, langId, True))
            Me.moExtSystemClaimStatus.Populate(CommonConfigManager.Current.ListManager.GetList("CLSTAT", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions)
            'BindListControlToDataView(Me.moOlitaSearchDrop, LookupListNew.DropdownLookupList(LookupListNew.LK_OLITA_SEARCH, langId, True), , , False)
            Me.moOlitaSearchDrop.Populate(CommonConfigManager.Current.ListManager.GetList("OLTASRCH", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions1)
            'BindListControlToDataView(Me.DDCancelBy, LookupListNew.DropdownLookupList(LookupListNew.LK_CERT_CANCEL_BY, langId, True), , , False)
            Me.DDCancelBy.Populate(CommonConfigManager.Current.ListManager.GetList("CCANBY", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions1)
            'BindListControlToDataView(Me.DDRequireCustomerAMLInfo, LookupListNew.DropdownLookupList(LookupListNew.LK_REQUIRE_CUSTOMER_AML_INFO, langId, True), , , False)
            Me.DDRequireCustomerAMLInfo.Populate(CommonConfigManager.Current.ListManager.GetList("CAIT", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions1)
            'BindListControlToDataView(Me.moUseInstallmentDefnId, LookupListNew.DropdownLookupList(LookupListNew.LK_INSTDEF, langId, True))
            Me.moUseInstallmentDefnId.Populate(CommonConfigManager.Current.ListManager.GetList("INSTDEF", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions)

            'BindListControlToDataView(Me.moExpectedPremiumIsWPDrop, yesNoLkL)
            Me.moExpectedPremiumIsWPDrop.Populate(oYesNoList, populateOptions)
            'BindListControlToDataView(Me.moAObligorDrop, yesNoLkL, , , False)
            Me.moAObligorDrop.Populate(oYesNoList, populateOptions1)
            'BindListControlToDataView(Me.moProdByRegion, yesNoLkL, "DESCRIPTION", "ID", False)
            Me.moProdByRegion.Populate(oYesNoList, populateOptions1)

            'BindListControlToDataView(Me.moClaim_Extended_Status_Entry, LookupListNew.DropdownLookupList(LookupListNew.LK_CLAIM_EXTENDED_STATUS_ENTRY_LIST_CODE, langId, False), , , False)
            Me.moClaim_Extended_Status_Entry.Populate(CommonConfigManager.Current.ListManager.GetList("EXTSTATENT", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions1)

            'BindListControlToDataView(Me.moAlwupdateCancel, yesNoLkL, "DESCRIPTION", "ID", False)
            Me.moAlwupdateCancel.Populate(oYesNoList, populateOptions1)
            'BindListControlToDataView(Me.moRejectaftercancel, yesNoLkL, "DESCRIPTION", "ID", False)
            Me.moRejectaftercancel.Populate(oYesNoList, populateOptions1)
            'BindListControlToDataView(Me.moAllowfuturecancel, yesNoLkL, "DESCRIPTION", "ID", False)
            Me.moAllowfuturecancel.Populate(oYesNoList, populateOptions1)
            'BindListControlToDataView(Me.ddlReplacementSKURequired, yesNoLkL, "DESCRIPTION", "ID", True)
            Me.ddlReplacementSKURequired.Populate(oYesNoList, populateOptions)
            'BindListControlToDataView(Me.moIsLawsuitMandatory, LookupListNew.GetYesNoLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), , , False)
            Me.moIsLawsuitMandatory.Populate(oYesNoList, populateOptions1)

            'BindListControlToDataView(Me.moQuestionListDrop, LookupListNew.GetQuestionListLookupList(DateTime.Now))
            Me.moQuestionListDrop.Populate(CommonConfigManager.Current.ListManager.GetList("CurrentQuestionList", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions4)
            'BindListControlToDataView(Me.moAlwupdateCancel, yesNoLkL, "DESCRIPTION", "ID", False)
            Me.moAlwupdateCancel.Populate(oYesNoList, populateOptions1)
            'BindListControlToDataView(Me.moRejectaftercancel, yesNoLkL, "DESCRIPTION", "ID", False)
            Me.moRejectaftercancel.Populate(oYesNoList, populateOptions1)

            'BindListControlToDataView(Me.ddlEnrFilePreProcess, LookupListNew.DropdownLookupList("EPP", langId, True))
            Me.ddlEnrFilePreProcess.Populate(CommonConfigManager.Current.ListManager.GetList("EPP", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions)
            'BindListControlToDataView(Me.ddlCertNumLookUpBy, LookupListNew.DropdownLookupList("CL", langId, True))
            Me.ddlCertNumLookUpBy.Populate(CommonConfigManager.Current.ListManager.GetList("CL", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions)
            'BindListControlToDataView(Me.ddlAutoRejErrType, LookupListNew.DropdownLookupList("AUTO_REJ_ERR_TYPE", langId, True))
            Me.ddlAutoRejErrType.Populate(CommonConfigManager.Current.ListManager.GetList("AUTO_REJ_ERR_TYPE", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions)
            'BindListControlToDataView(Me.ddlDealerExtractPeriod, LookupListNew.DropdownLookupList("DEALER_EXTRACT_PERIOD", langId, True))
            Me.ddlDealerExtractPeriod.Populate(CommonConfigManager.Current.ListManager.GetList("DEALER_EXTRACT_PERIOD", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions)
            'BindListControlToDataView(Me.ddlReconRejRecType, yesNoLkL, "DESCRIPTION", "ID", False)
            Me.ddlReconRejRecType.Populate(oYesNoList, populateOptions1)

            'BindListControlToDataView(Me.moLicenseTagMandatory, LookupListNew.DropdownLookupList(LookupListNew.LK_LICENSE_TAG_FLAG, langId), , , False)
            Me.moLicenseTagMandatory.Populate(CommonConfigManager.Current.ListManager.GetList("LCNSTAG", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions1)
            'BindListControlToDataView(Me.moVinrestrictMandatory, LookupListNew.DropdownLookupList(LookupListNew.LK_VRSTID, langId), , , True)
            Me.moVinrestrictMandatory.Populate(CommonConfigManager.Current.ListManager.GetList("VRSTID", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions)
            'BindListControlToDataView(Me.moplancodeinquote, LookupListNew.DropdownLookupList(LookupListNew.LK_PLAN_QUOTE_IN_QUOTE_OUTPUT, langId), , , True)
            Me.moplancodeinquote.Populate(CommonConfigManager.Current.ListManager.GetList("PLNQT", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions)
            'BindListControlToDataView(Me.ddlBillingProcessCode, LookupListNew.DropdownLookupList("BILLRESULTPROC", langId, True))
            Me.ddlBillingProcessCode.Populate(CommonConfigManager.Current.ListManager.GetList("BILLRESULTPROC", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions)
            'BindListControlToDataView(Me.ddlBillResultExpFTPSite, LookupListNew.GetFtpSiteLookupList(), , , True)
            Me.ddlBillResultExpFTPSite.Populate(CommonConfigManager.Current.ListManager.GetList("FTPSITE", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions)

            'BindListControlToDataView(Me.cboCertificatesAutonumberId, yesNoLkL)
            Me.cboCertificatesAutonumberId.Populate(oYesNoList, populateOptions)
            'BindListControlToDataView(Me.moAutoSelectServiceCenter, LookupListNew.GetYesNoLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId))
            Me.moAutoSelectServiceCenter.Populate(oYesNoList, populateOptions)

            'BindListControlToDataView(Me.moClaimAutoApproveDrop, yesNoLkL, "DESCRIPTION", "ID", False)
            Me.moClaimAutoApproveDrop.Populate(oYesNoList, populateOptions1)
            'BindListControlToDataView(Me.moReuseSerialNumberDrop, yesNoLkL, "DESCRIPTION", "ID", False)
            Me.moReuseSerialNumberDrop.Populate(oYesNoList, populateOptions1)
            'BindListControlToDataView(Me.ddlAutoGenRejPymtFile, LookupListNew.DropdownLookupList(LookupListNew.LK_AUTO_GEN_REJ_PYMT_FILE, langId), "DESCRIPTION", "ID", True)
            Me.ddlAutoGenRejPymtFile.Populate(CommonConfigManager.Current.ListManager.GetList("AUTO_GEN_REJ_PYMT_FILE", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions)
            'BindListControlToDataView(Me.ddlPymtRejRecRecon, yesNoLkL, "DESCRIPTION", "ID", True)
            Me.ddlPymtRejRecRecon.Populate(oYesNoList, populateOptions)

            'Me.ddlClaimRecordingCheckInventory.PopulateOld("YESNO", ListValueType.Description, ListValueType.ExtendedCode, PopulateBehavior.None, String.Empty, ListValueType.Description)
            Me.ddlClaimRecordingCheckInventory.Populate(oYesNoList, populateOptions2)
            'Me.ddlSuspenseApplies.PopulateOld("YESNO", ListValueType.Description, ListValueType.ExtendedCode, PopulateBehavior.None, String.Empty, ListValueType.Description)
            Me.ddlSuspenseApplies.Populate(oYesNoList, populateOptions2)
            'Me.ddlSourceSystem.PopulateOld("POLICYSYS", ListValueType.Description, ListValueType.ExtendedCode, PopulateBehavior.None, String.Empty, ListValueType.Description)
            Me.ddlSourceSystem.Populate(CommonConfigManager.Current.ListManager.GetList("POLICYSYS", Thread.CurrentPrincipal.GetLanguageCode()), populateOptions2)

            ' KDDI changes '

            Me.cancelShipmentAllowedDrop.Populate(oYesNoList, populateOptions2)
            Me.reshipmentAllowedDrop.Populate(oYesNoList, populateOptions2)
            Me.moValidateAddress.Populate(oYesNoList, populateOptions2)
            Me.moShowPrevCallerInfo.Populate(oYesNoList, populateOptions2)
            Me.ddlDealerNameFlag.Populate(oYesNoList, populateOptions3)
            'ddlCaseProfile.Populate(CommonConfigManager.Current.ListManager.GetList("CaseProfile", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
            '                           {
            '                           .AddBlankItem = True,
            '                           .BlankItemValue = String.Empty,
            '                           .TextFunc = AddressOf PopulateOptions.GetDescription,
            '                           .ValueFunc = AddressOf PopulateOptions.GetCode
            '                           })

        End Sub

        Private Sub ClearAll()
            Me.dlstRetailerID.ClearSelection()
            Me.moBranchValidationDrop.ClearSelection()
            Me.moEditBranchDrop.ClearSelection()
            Me.moDelayFactorDrop.ClearSelection()
            Me.moInstallmentFactorDrop.ClearSelection()
            Me.moRegistrationProcessDrop.ClearSelection()
            Me.moDealerGroupDrop.ClearSelection()
            Me.moServiceNetworkDrop.ClearSelection()
            Me.cboConvertProdCode.ClearSelection()
            Me.moPriceMatrixDrop.ClearSelection()
            Me.moDealerTypeDrop.ClearSelection()
            moOlitaSearchDrop.ClearSelection()
            moUseWarrantyMasterDrop.ClearSelection()
            moInsertIfMakeNotExists.ClearSelection()
            moUseIncomingSalesTaxDrop.ClearSelection()
            moAutoProcessFileDrop.ClearSelection()
            moAutoProcessPymtFiledrop.ClearSelection()
            Me.moExpectedPremiumIsWPDrop.ClearSelection()
            Me.moClaimSystemDrop.ClearSelection()
            Me.moBestReplacementDrop.ClearSelection()
            Me.moEquipmentListDrop.ClearSelection()
            Me.moUseEquipment.ClearSelection()
            Me.moQuestionListDrop.ClearSelection()
            Me.moPayDeductible.ClearSelection()
            Me.moCancelRequestFlagDrop.ClearSelection()
            'REQ-1294
            'Me.moCustInfoMandatory.ClearSelection()
            Me.moBankInfoMandatory.ClearSelection()
            Me.moCollectDeductible.ClearSelection()
            Me.moAObligorDrop.ClearSelection()
            Me.ddlCertNumLookUpBy.ClearSelection()
            Me.ddlEnrFilePreProcess.ClearSelection()

            Me.moLicenseTagMandatory.ClearSelection()
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

            Me.ddlAutoRejErrType.ClearSelection()
            Me.ddlReconRejRecType.ClearSelection()
            Me.ddlDealerExtractPeriod.ClearSelection()
            Me.moDefaultSalvageCenter.Text = String.Empty
            Me.txtMaxCommissionPercent.Text = String.Empty
            ddlAutoGenRejPymtFile.ClearSelection()
            ddlPymtRejRecRecon.ClearSelection()

            cboIdentificationNumberType.ClearSelection()

            'REQ
            Me.moClaimRecordingDrop.ClearSelection()
            Me.ddlUseFraudMonitoring.ClearSelection()
            ddlClaimRecordingCheckInventory.ClearSelection()

            'KDDI Changes'

            moValidateAddress.ClearSelection()
            cancelShipmentAllowedDrop.ClearSelection()
            reshipmentAllowedDrop.ClearSelection()

            txtCancelShipmentGracePeriod.Text = String.Empty
            ddlCaseProfile.ClearSelection()
            moShowPrevCallerInfo.ClearSelection()
            ddlDealerNameFlag.ClearSelection()
        End Sub

        Private Sub SetAssurantIsObligor()
            Dim oCompany As Company
            Dim oComputeTaxBasedId, oComputeTaxBased_CustomAddrId As Guid
            oCompany = New Company(Me.State.MyBO.CompanyId)

            oComputeTaxBased_CustomAddrId = LookupListNew.GetIdFromCode(LookupListNew.DropdownLookupList(
                Codes.COMPUTE_TAX_BASED, Authentication.LangId), Codes.COMPUTE_TAX_BASED_CUSTOMERS_ADDRESS)

            If (oCompany.ComputeTaxBasedId.Equals(oComputeTaxBased_CustomAddrId)) Then
                ' Yes => Invisible
                'SetSelectedItem(Me.moAObligorDrop, _
                '    LookupListNew.GetIdFromCode(LookupListNew.DropdownLookupList( _
                '                Codes.YESNO, ElitaPlusIdentity.Current.ActiveUser.LanguageId), Codes.YESNO_Y))
                ControlMgr.SetVisibleControl(Me, Me.lblAObligor, False)
                ControlMgr.SetVisibleControl(Me, Me.moAObligorDrop, False)
            Else    ' No => Visible
                ControlMgr.SetVisibleControl(Me, Me.lblAObligor, True)
                ControlMgr.SetVisibleControl(Me, Me.moAObligorDrop, True)
            End If

            If Me.State.MyBO.AssurantIsObligorId.Equals(System.Guid.Empty) Then
                ' Default
                SetSelectedItem(Me.moAObligorDrop,
                                LookupListNew.GetIdFromCode(LookupListNew.DropdownLookupList(
                                    Codes.YESNO, ElitaPlusIdentity.Current.ActiveUser.LanguageId), Codes.YESNO_Y))
            Else
                SetSelectedItem(Me.moAObligorDrop, Me.State.MyBO.AssurantIsObligorId)
            End If
        End Sub

        Protected Sub PopulateFormFromBOs()
            ClearAll()
            With Me.State.MyBO

                If ElitaPlusIdentity.Current.ActiveUser.Companies.Count > 1 AndAlso Me.State.MyBO.IsNew Then
                    'CompanyMultipleDrop.SelectedGuid = .CompanyId
                    moMultipleColumnDrop.SelectedGuid = .CompanyId
                Else
                    'CompanyMultipleDrop.CodeTextBox.Text = LookupListNew.GetCodeFromId(Me.State.companyDV, .CompanyId)
                    'CompanyMultipleDrop.DescriptionTextBox.Text = LookupListNew.GetDescriptionFromId(Me.State.companyDV, .CompanyId)
                    moMultipleColumnDrop.CodeTextBox.Text = LookupListNew.GetCodeFromId(Me.State.companyDV, .CompanyId)
                    moMultipleColumnDrop.DescriptionTextBox.Text = LookupListNew.GetDescriptionFromId(Me.State.companyDV, .CompanyId)
                End If

                Me.PopulateControlFromBOProperty(Me.txtTaxIdNumber, .TaxIdNumber)
                Me.PopulateControlFromBOProperty(Me.txtDealerCode, .Dealer)
                Me.PopulateControlFromBOProperty(Me.txtClientDealerCode, .ClientDealerCode)
                Me.PopulateControlFromBOProperty(Me.txtDealerName, .DealerName)
                Me.PopulateControlFromBOProperty(Me.txtContactName, .ContactName)
                Me.PopulateControlFromBOProperty(Me.txtContactExt, .ContactExt)
                Me.PopulateControlFromBOProperty(Me.txtContactEmail, .ContactEmail)
                Me.PopulateControlFromBOProperty(Me.txtContactPhone, .ContactPhone)
                Me.PopulateControlFromBOProperty(Me.txtContactFax, .ContactFax)
                Me.PopulateControlFromBOProperty(Me.txtMaxManWarr, .MaxManWarr)
                '5623
                Me.PopulateControlFromBOProperty(Me.txtGracePeriodMonths, .GracePeriodMonths)
                Me.PopulateControlFromBOProperty(Me.txtGracePeriodDays, .GracePeriodDays)
                '---5623

                'Req-877
                Me.PopulateControlFromBOProperty(Me.txtMinManWarr, .MinManWarr)

                'REQ-1297
                BindSelectItem(Me.State.MyBO.UseFullFileProcessId.ToString, Me.ddlFullfileProcess)
                Me.PopulateControlFromBOProperty(Me.txtMaxNCRecords, .MaxNCRecords)
                'REQ-1297 End

                Me.PopulateControlFromBOProperty(Me.txtServLineEmail, .ServiceLineEmail)
                Me.PopulateControlFromBOProperty(Me.txtServLinePhoneNum, .ServiceLinePhone)
                Me.PopulateControlFromBOProperty(Me.txtServLineFax, .ServiceLineFax)
                Me.PopulateControlFromBOProperty(Me.txtESCInsuranceLable, .EscInsuranceLabel)

                Me.PopulateControlFromBOProperty(Me.txtIBNR_Factor, .IBNRFactor, PERCENT_FORMAT)
                Me.PopulateControlFromBOProperty(Me.txtSTATIBNR_Factor, .STATIBNRFactor, PERCENT_FORMAT)
                Me.PopulateControlFromBOProperty(Me.txtLAEIBNR_Factor, .LAEIBNRFactor, PERCENT_FORMAT)

                SetSelectedItem(Me.moIBNR_COMPUTATION_METHODDropd, .IBNRComputationMethodId)
                SetSelectedItem(Me.moSTATIBNR_COMPUT_MTHDDropd, .STATIBNRComputationMethodId)
                SetSelectedItem(Me.moLAEIBNR_COMPUT_MTHDDropd, .LAEIBNRComputationMethodId)
                SetSelectedItem(Me.moClaimSystemDrop, .ClaimSystemId)
                SetSelectedItem(Me.moBestReplacementDrop, .BestReplacementGroupId)
                SetSelectedItem(Me.moEquipmentListDrop, LookupListNew.GetIdFromCode(LookupListNew.GetEquipmentListLookupList(DateTime.Now), .EquipmentListCode))
                'REQ-860 Elita Buildout - Issues/Adjudication
                SetSelectedItem(Me.moQuestionListDrop, LookupListNew.GetIdFromCode(LookupListNew.GetQuestionListLookupList(DateTime.Now), .QuestionListCode))
                SetSelectedItem(Me.moUseEquipment, .UseEquipmentId)
                SetSelectedItem(Me.moPayDeductible, .PayDeductibleId)
                'REQ-1294
                'SetSelectedItem(Me.moCustInfoMandatory, .CustInfoMandatoryId)
                SetSelectedItem(Me.moBankInfoMandatory, .BankInfoMandatoryId)
                SetSelectedItem(Me.moCollectDeductible, .DeductibleCollectionId)


                If Not Me.State.MyBO.CancellationRequestFlagId.Equals(Guid.Empty) Then
                    SetSelectedItem(Me.moCancelRequestFlagDrop, .CancellationRequestFlagId)
                End If

                Me.PopulateControlFromBOProperty(Me.txtBusinessName, .BusinessName)
                Me.PopulateControlFromBOProperty(Me.txtStateTaxIdNumber, .StateTaxIdNumber)
                Me.PopulateControlFromBOProperty(Me.txtCityTaxIdNumber, .CityTaxIdNumber)
                Me.PopulateControlFromBOProperty(Me.txtWebAddress, .WebAddress)
                Me.PopulateControlFromBOProperty(Me.txtNumbOfOtherLocations, .NumberOfOtherLocations)
                Me.PopulateControlFromBOProperty(Me.moRegistrationEmailFromText, .RegistrationEmailFrom)
                Me.PopulateControlFromBOProperty(Me.txtProgramName, .ProgramName)
                Me.PopulateControlFromBOProperty(Me.txtClaimVerfificationNumLength, .ClaimVerfificationNumLength)

                BindSelectItem(Me.State.MyBO.DealerGroupId.ToString, Me.moDealerGroupDrop)
                BindSelectItem(Me.State.MyBO.RetailerId.ToString, Me.dlstRetailerID)
                BindSelectItem(Me.State.MyBO.ServiceNetworkId.ToString, Me.moServiceNetworkDrop)
                BindSelectItem(Me.State.MyBO.ConvertProductCodeId.ToString, Me.cboConvertProdCode)
                BindSelectItem(Me.State.MyBO.DealerTypeId.ToString, Me.moDealerTypeDrop)
                BindSelectItem(Me.State.MyBO.OlitaSearch.ToString, Me.moOlitaSearchDrop)
                BindSelectItem(Me.State.MyBO.BranchValidationId.ToString, Me.moBranchValidationDrop)
                BindSelectItem(Me.State.MyBO.EditBranchId.ToString, Me.moEditBranchDrop)
                BindSelectItem(Me.State.MyBO.DelayFactorFlagId.ToString, Me.moDelayFactorDrop)
                BindSelectItem(Me.State.MyBO.RoundCommFlagId.ToString, Me.moRoundCommId)
                BindSelectItem(Me.State.MyBO.InstallmentFactorFlagId.ToString, Me.moInstallmentFactorDrop)
                BindSelectItem(Me.State.MyBO.RegistrationProcessFlagId.ToString, Me.moRegistrationProcessDrop)
                BindSelectItem(Me.State.MyBO.PriceMatrixUsesWpId.ToString, Me.moPriceMatrixDrop)
                BindSelectItem(Me.State.MyBO.UseWarrantyMasterID.ToString, Me.moUseWarrantyMasterDrop)
                BindSelectItem(Me.State.MyBO.InsertIfMakeNotExists.ToString, Me.moInsertIfMakeNotExists)
                BindSelectItem(Me.State.MyBO.UseIncomingSalesTaxID.ToString, Me.moUseIncomingSalesTaxDrop)
                BindSelectItem(Me.State.MyBO.AutoProcessFileID.ToString, Me.moAutoProcessFileDrop)
                BindSelectItem(Me.State.MyBO.AutoProcessPymtFileID.ToString, Me.moAutoProcessPymtFiledrop)
                BindSelectItem(Me.State.MyBO.SeparatedCreditNotesId.ToString, Me.ddlSeparatedCN)
                'Req 846
                BindSelectItem(Me.State.MyBO.ValidateSKUId.ToString, Me.moSkuNumberDrop)

                'REQ-874
                BindSelectItem(Me.State.MyBO.ValidateBillingCycleId.ToString, Me.moValidateBillingCycleIdDrop)
                BindSelectItem(Me.State.MyBO.ValidateSerialNumberId.ToString, Me.moValidateSerialNumberDrop)
                BindSelectItem(Me.State.MyBO.ManualEnrollmentAllowedId.ToString, Me.moManualEnrollmentAllowedId)
                'REQ-5761
                BindSelectItem(Me.State.MyBO.UseNewBillForm.ToString, Me.cboUseNewBillPay)

                'REQ-5932
                BindSelectItem(Me.State.MyBO.ShareCustomers, Me.cboShareCustomers)
                'If Me.cboShareCustomers.SelectedValue = SHARE_CUSTOMER Then
                '    Me.cboCustomerIdLookUpBy.Enabled = False
                'End If
                BindSelectItem(Me.State.MyBO.CustomerLookup, Me.cboCustomerIdLookUpBy)
                BindSelectItem(Me.State.MyBO.InvoiceByBranchId.ToString, Me.ddlInvByBranch)
                BindSelectItem(Me.State.MyBO.CertCancelById.ToString, Me.DDCancelBy)
                BindSelectItem(Me.State.MyBO.RequireCustomerAMLInfoId.ToString, Me.DDRequireCustomerAMLInfo)
                'Me.moUseInstallmentDefnId.ClearSelection()
                BindSelectItem(Me.State.MyBO.UseInstallmentDefnId.ToString, Me.moUseInstallmentDefnId)

                BindSelectItem(Me.State.MyBO.ExpectedPremiumIsWPId.ToString, Me.moExpectedPremiumIsWPDrop)

                If Me.State.MyBO.DealerTypeDesc = Me.State.MyBO.DEALER_TYPE_DESC Then
                    Me.State.MyBO.Address.AddressIsRequire = True
                End If

                MailingAddressCtr.Bind(Me.State.MyBO.MailingAddress)
                AddressCtr.Bind(Me.State.MyBO.Address)

                ' Dim _bankinfo As BankInfo
                If Me.State.MyBO.BankInfoId <> Guid.Empty Then
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

                If Me.State.MyBO.UseSvcOrderAddress Then
                    Me.State.SvcOrdersByDealer = Me.State.MyBO.SvcOrdersAddress
                    Me.PopulateControlFromBOProperty(Me.txtName, Me.State.SvcOrdersByDealer.Name)
                    Me.PopulateControlFromBOProperty(Me.txtTaxId, Me.State.SvcOrdersByDealer.TaxIdNumber)
                    Me.State.SvcOrdersByDealer.CompanyId = .CompanyId
                    Me.SvcOrderAddressCtr.Bind(Me.State.SvcOrdersByDealer.Address)
                End If
                SetAssurantIsObligor()

                'SetSelectedItem(Me.moAuthAmtBasedOn, .AuthAmtBasedOnId)
                BindSelectItem(Me.State.MyBO.ProductByRegionId.ToString, Me.moProdByRegion)

                Me.PopulateControlFromBOProperty(Me.moClaim_Extended_Status_Entry, .ClaimExtendedStatusEntryId)

                'Req-1000
                BindSelectItem(Me.State.MyBO.AllowUpdateCancellationId.ToString, Me.moAlwupdateCancel)
                BindSelectItem(Me.State.MyBO.RejectAfterCancellationId.ToString, Me.moRejectaftercancel)
                BindSelectItem(Me.State.MyBO.AllowFutureCancelDateId.ToString, Me.moAllowfuturecancel)

                BindSelectItem(Me.State.MyBO.IsLawsuitMandatoryId.ToString, Me.moIsLawsuitMandatory)

                'REQ-1153 
                BindSelectItem(Me.State.MyBO.DealerSupportWebClaimsId.ToString, Me.moDEALER_SUPPORT_WEB_CLAIMS)
                BindSelectItem(Me.State.MyBO.ClaimStatusForExtSystemId.ToString, Me.moExtSystemClaimStatus)
                'REQ-1153 end

                'req 1157
                BindSelectItem(Me.State.MyBO.NewDeviceSkuRequiredId.ToString, Me.ddlReplacementSKURequired)
                BindSelectItem(Me.State.MyBO.UseClaimAuthorizationId.ToString, Me.moUseClaimAutorization)
                'Req-1142
                If (Me.moLicenseTagMandatory.Visible) Then
                    BindSelectItem(Me.State.MyBO.LicenseTagValidationId.ToString, Me.moLicenseTagMandatory)
                End If
                'Req-5723 start
                If (Me.moVinrestrictMandatory.Visible) Then
                    BindSelectItem(Me.State.MyBO.VINRestrictMandatoryId.ToString, Me.moVinrestrictMandatory)
                End If
                If (Me.moplancodeinquote.Visible) Then
                    BindSelectItem(Me.State.MyBO.PlanCodeInQuote.ToString, Me.moplancodeinquote)
                End If
                'Req-5723 End 
                'REQ-1190
                BindSelectItem(Me.State.MyBO.CertnumlookupbyId.ToString, Me.ddlCertNumLookUpBy)
                BindSelectItem(Me.State.MyBO.EnrollfilepreprocessprocId.ToString, Me.ddlEnrFilePreProcess)
                'REQ-1190
                BindSelectItem(Me.State.MyBO.AutoRejErrTypeId.ToString, Me.ddlAutoRejErrType)
                BindSelectItem(Me.State.MyBO.ReconRejRecTypeId.ToString, Me.ddlReconRejRecType)
                BindSelectItem(Me.State.MyBO.DealerExtractPeriodId.ToString, Me.ddlDealerExtractPeriod)

                'REQ-1244
                Me.PopulateControlFromBOProperty(Me.txtRepClaimDedTolerancePct, .Replaceclaimdedtolerancepct)

                'REQ-1274
                BindSelectItem(Me.State.MyBO.BillingProcessCodeId.ToString, Me.ddlBillingProcessCode)
                BindSelectItem(Me.State.MyBO.BillresultExceptionDestId.ToString, Me.ddlBillResultExpFTPSite)
                Me.PopulateControlFromBOProperty(Me.txtBillResultNotifyEmail, .BillresultNotificationEmail)

                Me.PopulateControlFromBOProperty(Me.cboCertificatesAutonumberId, .CertificatesAutonumberId)
                Me.PopulateControlFromBOProperty(Me.txtCertificatesAutonumberPrefix, .CertificatesAutonumberPrefix)
                Me.PopulateControlFromBOProperty(Me.txtMaxCertNumLengthAlwd, .MaximumCertNumberLengthAllowed)

                Me.PopulateControlFromBOProperty(Me.txtFileLoadNotificationEmail, .FileLoadNotificationEmail)
                Me.PopulateControlFromBOProperty(Me.txtPolicyEventNotifiyEmail, .PolicyEventNotificationEmail)

                'Save the Lawsuit Value at the time of loading the Dealer Form
                Me.State.LawsuitMandatoryIdAtLoad = .IsLawsuitMandatoryId
                'REQ-5466
                SetSelectedItem(Me.moAutoSelectServiceCenter, .AutoSelectServiceCenter)

                'REQ-5598
                '''''claim close rules control
                If (Not Me.State.blnIsComingFromCopy) Then
                    ClaimCloseRules.CompanyId = Me.State.Ocompany.Id
                    ClaimCloseRules.DealerId = Me.State.MyBO.Id
                    ClaimCloseRules.companyCode = Me.State.Ocompany.Code
                    ClaimCloseRules.Dealer = Me.State.MyBO.Dealer
                    ClaimCloseRules.Populate()
                End If
                
                'Load Dealer Inflation user control
                DealerInflation.DealerId =Me.State.MyBO.Id
                DealerInflation.Populate()

                'Load Risk Type Tolerance
                RiskTypeTolerance.DealerId = Me.State.MyBO.Id
                RiskTypeTolerance.Populate()

                If Not Me.State.MyBO.ClaimAutoApproveId.Equals(Guid.Empty) Then
                    Me.PopulateControlFromBOProperty(Me.moClaimAutoApproveDrop, .ClaimAutoApproveId)
                End If

                If Not Me.State.MyBO.ReuseSerialNumberId.Equals(Guid.Empty) Then
                    Me.PopulateControlFromBOProperty(Me.moReuseSerialNumberDrop, .ReuseSerialNumberId)
                End If

                If .ClaimAutoApproveId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_Y)) Then
                    'ControlMgr.SetVisibleControl(Me, Me.ClaimTypesRow, True)
                    'ControlMgr.SetVisibleControl(Me, Me.CoverageTypesRow, True)
                    ControlMgr.SetVisibleControl(Me, Me.pnlTypesRow, True)
                    Me.PopulateClaimAutoApproveControls()
                Else
                    'ControlMgr.SetVisibleControl(Me, Me.ClaimTypesRow, False)
                    'ControlMgr.SetVisibleControl(Me, Me.CoverageTypesRow, False)
                    ControlMgr.SetVisibleControl(Me, Me.pnlTypesRow, False)
                End If
                Me.inputServiceCenterId.Value = .DefaultSalvgeCenterId.ToString()
                Me.inputServiceCenterDesc.Value = LookupListNew.GetDescriptionFromId(LookupListNew.LK_SERVICE_CENTERS, .DefaultSalvgeCenterId)
                Me.moDefaultSalvageCenter.Text = Me.inputServiceCenterDesc.Value
                Me.PopulateControlFromBOProperty(Me.txtMaxCommissionPercent, .MaximumCommissionPercent, PERCENT_FORMAT)
                BindSelectItem(Me.State.MyBO.AutoGenerateRejectedPaymentFileId.ToString, Me.ddlAutoGenRejPymtFile)
                BindSelectItem(Me.State.MyBO.PaymentRejectedRecordReconcileId.ToString, Me.ddlPymtRejRecRecon)

                BindSelectItem(Me.State.MyBO.IdentificationNumberType, Me.cboIdentificationNumberType)
                BindSelectItem(Me.State.MyBO.UseQuote, Me.cboUseQuote)
                BindSelectItem(Me.State.MyBO.ContractManualVerification, Me.cboContractManualVerification)
                'REQ-6406
                BindSelectItem(Me.State.MyBO.AcceptPaymentByCheck, Me.moAcceptPaymentByCheckDrop)

                BindSelectItem(Me.State.MyBO.ClaimRecordingXcd, Me.moClaimRecordingDrop)
                BindSelectItem(Me.State.MyBO.UseFraudMonitoringXcd, ddlUseFraudMonitoring)
                'REQ 6156
                BindSelectItem(Me.State.MyBO.ImeiUseXcd, Me.cboImeiNoUse)

                BindSelectItem(.ClaimRecordingCheckInventoryXcd, ddlClaimRecordingCheckInventory)

                'KDDI Changes'
                BindSelectItem(Me.State.MyBO.Is_Cancel_Shipment_Allowed, Me.cancelShipmentAllowedDrop)
                BindSelectItem(Me.State.MyBO.Is_Reshipment_Allowed, Me.reshipmentAllowedDrop)
                BindSelectItem(Me.State.MyBO.Validate_Address, Me.moValidateAddress)

                Me.PopulateControlFromBOProperty(Me.txtClosecaseperiod, .CloseCaseGracePeriodDays)

                BindSelectItem(Me.State.MyBO.Show_Previous_Caller_Info, Me.moShowPrevCallerInfo)

                BindSelectItem(Me.State.MyBO.DisplayDobXcd, Me.ddlDealerNameFlag)

                Me.PopulateControlFromBOProperty(Me.txtCancelShipmentGracePeriod, .Cancel_Shipment_Grace_Period)

                BindSelectItem(.CaseProfileCode, Me.ddlCaseProfile)

                ControlMgr.SetVisibleControl(Me, trBenefitDlrTypeCtls4, True)
                Me.PopulateControlFromBOProperty(Me.txtBenefitSoldToAccount, .BenefitSoldToAccount)

                If (Not .DealerTypeDesc Is Nothing AndAlso .DealerTypeDesc.ToUpper() = State.MyBO.DEALER_TYPE_BENEFIT) Then
                    ControlMgr.SetVisibleControl(Me, trBenefitDlrTypeCtls1, True)
                    ControlMgr.SetVisibleControl(Me, trBenefitDlrTypeCtls2, True)
                    ControlMgr.SetVisibleControl(Me, trBenefitDlrTypeCtls3, True)


                    BindSelectItem(.SuspendAppliesXcd, ddlSuspenseApplies)
                    BindSelectItem(.SourceSystemXcd, ddlSourceSystem)

                    Me.PopulateControlFromBOProperty(Me.txtVoidDuration, .VoidDuration)
                    Me.PopulateControlFromBOProperty(Me.txtInvCutOffDay, .InvoiceCutoffDay)
                    Me.PopulateControlFromBOProperty(Me.txtBenefitCarrierCode, .BenefitCarrierCode)


                    If (ddlSuspenseApplies.SelectedItem.Value.ToUpper() = "YESNO-Y") Then
                        Me.PopulateControlFromBOProperty(Me.txtSuspensePeriod, State.MyBO.SuspendPeriod)
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
                AttributeValues.ParentBusinessObject = CType(Me.State.MyBO, IAttributable)
                AttributeValues.TranslateHeaders()
            End If
            AttributeValues.DataBind()



        End Sub

        Protected Sub PopulateBOsFromForm()

            With Me.State.MyBO

                Me.PopulateBOProperty(Me.State.MyBO, "Dealer", Me.txtDealerCode)
                Me.PopulateBOProperty(Me.State.MyBO, "ClientDealerCode", Me.txtClientDealerCode)
                Me.PopulateBOProperty(Me.State.MyBO, "DealerName", Me.txtDealerName)
                Me.PopulateBOProperty(Me.State.MyBO, "RetailerId", Me.dlstRetailerID)
                Me.PopulateBOProperty(Me.State.MyBO, "TaxIdNumber", Me.txtTaxIdNumber)
                Me.PopulateBOProperty(Me.State.MyBO, "DealerGroupId", Me.moDealerGroupDrop)
                Me.PopulateBOProperty(Me.State.MyBO, "DealerTypeId", Me.moDealerTypeDrop)
                Me.PopulateBOProperty(Me.State.MyBO, "OlitaSearch", Me.moOlitaSearchDrop)
                Me.PopulateBOProperty(Me.State.MyBO, "CancellationRequestFlagId", Me.moCancelRequestFlagDrop)
                Me.PopulateBOProperty(Me.State.MyBO, "BranchValidationId", Me.moBranchValidationDrop)
                Me.PopulateBOProperty(Me.State.MyBO, "RoundCommFlagId", Me.moRoundCommId)
                Me.PopulateBOProperty(Me.State.MyBO, "EditBranchId", Me.moEditBranchDrop)
                Me.PopulateBOProperty(Me.State.MyBO, "DelayFactorFlagId", Me.moDelayFactorDrop)
                Me.PopulateBOProperty(Me.State.MyBO, "InstallmentFactorFlagId", Me.moInstallmentFactorDrop)
                Me.PopulateBOProperty(Me.State.MyBO, "RegistrationProcessFlagId", Me.moRegistrationProcessDrop)
                Me.PopulateBOProperty(Me.State.MyBO, "RegistrationEmailFrom", Me.moRegistrationEmailFromText)
                Me.PopulateBOProperty(Me.State.MyBO, "UseWarrantyMasterID", Me.moUseWarrantyMasterDrop)
                Me.PopulateBOProperty(Me.State.MyBO, "InsertIfMakeNotExists", Me.moInsertIfMakeNotExists)

                Me.PopulateBOProperty(Me.State.MyBO, "UseIncomingSalesTaxID", Me.moUseIncomingSalesTaxDrop)
                Me.PopulateBOProperty(Me.State.MyBO, "AutoProcessFileID", Me.moAutoProcessFileDrop)
                Me.PopulateBOProperty(Me.State.MyBO, "AutoProcessPymtFileID", Me.moAutoProcessPymtFiledrop)
                Me.PopulateBOProperty(Me.State.MyBO, "AutoRejErrTypeId", Me.ddlAutoRejErrType)
                Me.PopulateBOProperty(Me.State.MyBO, "ReconRejRecTypeId", Me.ddlReconRejRecType)
                Me.PopulateBOProperty(Me.State.MyBO, "DealerExtractPeriodId", Me.ddlDealerExtractPeriod)

                '5623
                Me.PopulateBOProperty(Me.State.MyBO, "GracePeriodMonths", Me.txtGracePeriodMonths)
                Me.PopulateBOProperty(Me.State.MyBO, "GracePeriodDays", Me.txtGracePeriodDays)
                '----end 5623

                Me.PopulateBOProperty(Me.State.MyBO, "ContactName", Me.txtContactName)
                'Req-1297
                Me.PopulateBOProperty(Me.State.MyBO, "UseFullFileProcessId", Me.ddlFullfileProcess)
                Me.PopulateBOProperty(Me.State.MyBO, "MaxNCRecords", Me.txtMaxNCRecords)
                'Req-1297 End

                Me.PopulateBOProperty(Me.State.MyBO, "ContactPhone", Me.txtContactPhone)
                Me.PopulateBOProperty(Me.State.MyBO, "ContactFax", Me.txtContactFax)
                Me.PopulateBOProperty(Me.State.MyBO, "ContactEmail", Me.txtContactEmail)
                Me.PopulateBOProperty(Me.State.MyBO, "ContactExt", Me.txtContactExt)
                Me.PopulateBOProperty(Me.State.MyBO, "ServiceNetworkId", Me.moServiceNetworkDrop)
                Me.PopulateBOProperty(Me.State.MyBO, "ConvertProductCodeId", Me.cboConvertProdCode)
                Me.PopulateBOProperty(Me.State.MyBO, "MaxManWarr", Me.txtMaxManWarr)
                'Req-877
                Me.PopulateBOProperty(Me.State.MyBO, "MinManWarr", Me.txtMinManWarr)

                Me.PopulateBOProperty(Me.State.MyBO, "EscInsuranceLabel", Me.txtESCInsuranceLable)
                Me.PopulateBOProperty(Me.State.MyBO, "ServiceLinePhone", Me.txtServLinePhoneNum)
                Me.PopulateBOProperty(Me.State.MyBO, "ServiceLineFax", Me.txtServLineFax)
                Me.PopulateBOProperty(Me.State.MyBO, "ServiceLineEmail", Me.txtServLineEmail)

                Me.PopulateBOProperty(Me.State.MyBO, "PriceMatrixUsesWpId", Me.moPriceMatrixDrop)
                Me.PopulateBOProperty(Me.State.MyBO, "BusinessName", Me.txtBusinessName)
                Me.PopulateBOProperty(Me.State.MyBO, "StateTaxIdNumber", Me.txtStateTaxIdNumber)
                Me.PopulateBOProperty(Me.State.MyBO, "CityTaxIdNumber", Me.txtCityTaxIdNumber)
                Me.PopulateBOProperty(Me.State.MyBO, "WebAddress", Me.txtWebAddress)
                Me.PopulateBOProperty(Me.State.MyBO, "NumberOfOtherLocations", Me.txtNumbOfOtherLocations)
                Me.PopulateBOProperty(Me.State.MyBO, "ProgramName", Me.txtProgramName)

                Me.PopulateBOProperty(Me.State.MyBO, "InvoiceByBranchId", Me.ddlInvByBranch)
                Me.PopulateBOProperty(Me.State.MyBO, "SeparatedCreditNotesId", Me.ddlSeparatedCN)
                Me.PopulateBOProperty(Me.State.MyBO, "ManualEnrollmentAllowedId", Me.moManualEnrollmentAllowedId)

                'REQ-5761
                Me.PopulateBOProperty(Me.State.MyBO, "UseNewBillForm", Me.cboUseNewBillPay)

                'REQ-5932
                Me.PopulateBOProperty(Me.State.MyBO, "ShareCustomers", Me.cboShareCustomers, False, True)

                If Not cboCustomerIdLookUpBy.SelectedItem.Text = String.Empty Then
                    Me.PopulateBOProperty(Me.State.MyBO, "CustomerLookup", Me.cboCustomerIdLookUpBy, False, True)
                End If

                Me.PopulateBOProperty(Me.State.MyBO, "CertCancelById", Me.DDCancelBy)
                Me.PopulateBOProperty(Me.State.MyBO, "RequireCustomerAMLInfoId", Me.DDRequireCustomerAMLInfo)
                .ActiveFlag = "Y"

                Me.PopulateBOProperty(Me.State.MyBO, "IBNRFactor", Me.txtIBNR_Factor)
                Me.PopulateBOProperty(Me.State.MyBO, "IBNRComputationMethodId", Me.moIBNR_COMPUTATION_METHODDropd)
                Me.PopulateBOProperty(Me.State.MyBO, "STATIBNRFactor", Me.txtSTATIBNR_Factor)
                Me.PopulateBOProperty(Me.State.MyBO, "STATIBNRComputationMethodId", Me.moSTATIBNR_COMPUT_MTHDDropd)
                Me.PopulateBOProperty(Me.State.MyBO, "LAEIBNRFactor", Me.txtLAEIBNR_Factor)
                Me.PopulateBOProperty(Me.State.MyBO, "LAEIBNRComputationMethodId", Me.moLAEIBNR_COMPUT_MTHDDropd)
                Me.PopulateBOProperty(Me.State.MyBO, "UseInstallmentDefnId", Me.moUseInstallmentDefnId)

                Me.PopulateBOProperty(Me.State.MyBO, "ExpectedPremiumIsWPId", Me.moExpectedPremiumIsWPDrop)
                Me.PopulateBOProperty(Me.State.MyBO, "ClaimSystemId", Me.moClaimSystemDrop)
                Me.PopulateBOProperty(Me.State.MyBO, "BestReplacementGroupId", Me.moBestReplacementDrop)
                Me.PopulateBOProperty(Me.State.MyBO, "EquipmentListCode", LookupListNew.GetCodeFromId(LookupListNew.GetEquipmentListLookupList(DateTime.Now), New Guid(Me.moEquipmentListDrop.SelectedValue)))
                'REQ-860 Elita Buildout - Issues/Adjudication
                Me.PopulateBOProperty(Me.State.MyBO, "QuestionListCode", LookupListNew.GetCodeFromId(LookupListNew.GetQuestionListLookupList(DateTime.Now), New Guid(Me.moQuestionListDrop.SelectedValue)))
                Me.PopulateBOProperty(Me.State.MyBO, "UseEquipmentId", Me.moUseEquipment)
                'Req 846
                Me.PopulateBOProperty(Me.State.MyBO, "ValidateSKUId", Me.moSkuNumberDrop)




                'REQ-874
                Me.PopulateBOProperty(Me.State.MyBO, "ValidateBillingCycleId", Me.moValidateBillingCycleIdDrop)
                Me.PopulateBOProperty(Me.State.MyBO, "ValidateSerialNumberId", Me.moValidateSerialNumberDrop)
                Me.PopulateBOProperty(Me.State.MyBO, "PayDeductibleId", Me.moPayDeductible)
                'REQ-1294
                'Me.PopulateBOProperty(Me.State.MyBO, "CustInfoMandatoryId", Me.moCustInfoMandatory)
                Me.PopulateBOProperty(Me.State.MyBO, "BankInfoMandatoryId", Me.moBankInfoMandatory)
                Me.PopulateBOProperty(Me.State.MyBO, "DeductibleCollectionId", Me.moCollectDeductible)

                Me.PopulateBOProperty(Me.State.MyBO, "AssurantIsObligorId", Me.moAObligorDrop)

                Me.PopulateBOProperty(Me.State.MyBO, "CertificatesAutonumberId", Me.cboCertificatesAutonumberId)
                Me.PopulateBOProperty(Me.State.MyBO, "ClaimAutoApproveId", Me.moClaimAutoApproveDrop)
                Me.PopulateBOProperty(Me.State.MyBO, "ReuseSerialNumberId", Me.moReuseSerialNumberDrop)
                Me.PopulateBOProperty(Me.State.MyBO, "CertificatesAutonumberPrefix", Me.txtCertificatesAutonumberPrefix)
                Me.PopulateBOProperty(Me.State.MyBO, "MaximumCertNumberLengthAllowed", Me.txtMaxCertNumLengthAlwd)

                Me.PopulateBOProperty(Me.State.MyBO, "FileLoadNotificationEmail", Me.txtFileLoadNotificationEmail)

                If Me.State.MyBO.IsNew Then
                    If ElitaPlusIdentity.Current.ActiveUser.Companies.Count > 1 Then
                        '.CompanyId = CompanyMultipleDrop.SelectedGuid 'Me.GetSelectedItem(Me.cboCompanyCode)
                        .CompanyId = moMultipleColumnDrop.SelectedGuid
                    Else
                        .CompanyId = CType(ElitaPlusIdentity.Current.ActiveUser.Companies.Item(0), Guid)
                    End If
                End If

                If Me.State.MyBO.UseSvcOrderAddress Then
                    Me.State.SvcOrdersByDealer = Me.State.MyBO.SvcOrdersAddress
                    Me.PopulateBOProperty(Me.State.SvcOrdersByDealer, "Name", Me.txtName)
                    Me.PopulateBOProperty(Me.State.SvcOrdersByDealer, "TaxIdNumber", Me.txtTaxId)
                    Me.State.SvcOrdersByDealer.DealerId = Me.State.MyBO.Id
                    Me.SvcOrderAddressCtr.PopulateBOFromControl(True)


                    If ((Me.SvcOrderAddressCtr.MyBO.IsDeleted = False) AndAlso
                        (Me.SvcOrderAddressCtr.MyBO.IsEmpty = False)) Then
                        Me.State.SvcOrdersByDealer.AddressId = Me.SvcOrderAddressCtr.MyBO.Id
                    End If

                End If

                Me.AddressCtr.PopulateBOFromControl(True)
                If Not AddressCtr.MyBO Is Nothing Then
                    If ((AddressCtr.MyBO.IsDeleted = False) AndAlso
                        (AddressCtr.MyBO.IsEmpty = False)) AndAlso Not Me.State.MyBO.AddressId = Me.AddressCtr.MyBO.Id Then
                        Me.State.MyBO.AddressId = Me.AddressCtr.MyBO.Id
                    End If
                End If

                Me.MailingAddressCtr.PopulateBOFromControl(True)
                If Not Me.MailingAddressCtr.MyBO Is Nothing Then
                    If ((Me.MailingAddressCtr.MyBO.IsDeleted = False) AndAlso
                        (Me.MailingAddressCtr.MyBO.IsEmpty = False)) Then
                        Me.State.MyBO.MailingAddressId = Me.MailingAddressCtr.MyBO.Id
                    End If
                End If
                moBankInfo.PopulateBOFromControl(True, False)

                'Me.PopulateBOProperty(Me.State.MyBO, "AuthAmtBasedOnId", Me.moAuthAmtBasedOn)
                Me.PopulateBOProperty(Me.State.MyBO, "ProductByRegionId", Me.moProdByRegion)
                Me.PopulateBOProperty(Me.State.MyBO, "ClaimVerfificationNumLength", Me.txtClaimVerfificationNumLength)
                Me.PopulateBOProperty(Me.State.MyBO, "ClaimExtendedStatusEntryId", Me.moClaim_Extended_Status_Entry)
                Me.PopulateBOProperty(Me.State.MyBO, "AllowUpdateCancellationId", Me.moAlwupdateCancel)
                Me.PopulateBOProperty(Me.State.MyBO, "RejectAfterCancellationId", Me.moRejectaftercancel)

                'Req-1000

                Me.PopulateBOProperty(Me.State.MyBO, "AllowUpdateCancellationId", Me.moAlwupdateCancel)
                Me.PopulateBOProperty(Me.State.MyBO, "RejectAfterCancellationId", Me.moRejectaftercancel)
                Me.PopulateBOProperty(Me.State.MyBO, "AllowFutureCancelDateId", Me.moAllowfuturecancel)
                Me.PopulateBOProperty(Me.State.MyBO, "IsLawsuitMandatoryId", Me.moIsLawsuitMandatory)

                'REQ-1153
                Me.PopulateBOProperty(Me.State.MyBO, "DealerSupportWebClaimsId", Me.moDEALER_SUPPORT_WEB_CLAIMS)
                Me.PopulateBOProperty(Me.State.MyBO, "ClaimStatusForExtSystemId", Me.moExtSystemClaimStatus)
                'REQ-1153 end

                'req 1157
                Me.PopulateBOProperty(Me.State.MyBO, "NewDeviceSkuRequiredId", Me.ddlReplacementSKURequired)
                'req 1157 end
                Me.PopulateBOProperty(Me.State.MyBO, "UseClaimAuthorizationId", Me.moUseClaimAutorization)

                'REQ-1190
                Me.PopulateBOProperty(Me.State.MyBO, "EnrollfilepreprocessprocId", Me.ddlEnrFilePreProcess)
                Me.PopulateBOProperty(Me.State.MyBO, "CertnumlookupbyId", Me.ddlCertNumLookUpBy)
                'REQ-1190
                'Req-1142
                If (Me.moLicenseTagMandatory.Visible AndAlso GetSelectedItem(Me.moLicenseTagMandatory) <> Guid.Empty) Then
                    Me.PopulateBOProperty(Me.State.MyBO, "LicenseTagValidationId", Me.moLicenseTagMandatory)
                End If
                'Req-1142 end
                'Req-5723 start
                If (Me.moVinrestrictMandatory.Visible) Then
                    Me.PopulateBOProperty(Me.State.MyBO, "VINRestrictMandatoryId", Me.moVinrestrictMandatory)
                End If
                If (Me.moplancodeinquote.Visible) Then
                    Me.PopulateBOProperty(Me.State.MyBO, "PlanCodeInQuote", Me.moplancodeinquote)
                End If
                'Req-5723 End

                'REQ-1244
                Me.PopulateBOProperty(Me.State.MyBO, "Replaceclaimdedtolerancepct", Me.txtRepClaimDedTolerancePct)

                'REQ-1274
                Me.PopulateBOProperty(Me.State.MyBO, "BillingProcessCodeId", Me.ddlBillingProcessCode)
                Me.PopulateBOProperty(Me.State.MyBO, "BillresultExceptionDestId", Me.ddlBillResultExpFTPSite)
                Me.PopulateBOProperty(Me.State.MyBO, "BillresultNotificationEmail", Me.txtBillResultNotifyEmail)

                'REQ-5466
                Me.PopulateBOProperty(Me.State.MyBO, "AutoSelectServiceCenter", Me.moAutoSelectServiceCenter)
                'REQ-5586
                Me.PopulateBOProperty(Me.State.MyBO, "PolicyEventNotificationEmail", Me.txtPolicyEventNotifiyEmail)
                Me.PopulateBOProperty(Me.State.MyBO, "MaximumCommissionPercent", Me.txtMaxCommissionPercent)



                If Not Me.inputServiceCenterId.Value = Guid.Empty.ToString Then
                    AjaxController.PopulateBOAutoComplete(Me.moDefaultSalvageCenter, Me.inputServiceCenterDesc,
                                                          Me.inputServiceCenterId, Me.State.MyBO, "DefaultSalvgeCenterId")
                End If
                Me.PopulateBOProperty(Me.State.MyBO, "AutoGenerateRejectedPaymentFileId", Me.ddlAutoGenRejPymtFile)
                Me.PopulateBOProperty(Me.State.MyBO, "PaymentRejectedRecordReconcileId", Me.ddlPymtRejRecRecon)

                Me.PopulateBOProperty(Me.State.MyBO, "IdentificationNumberType", Me.cboIdentificationNumberType, False, True)
                Me.PopulateBOProperty(Me.State.MyBO, "UseQuote", Me.cboUseQuote, False, True)
                Me.PopulateBOProperty(Me.State.MyBO, "ContractManualVerification", Me.cboContractManualVerification, False, True)
                'REQ-6406
                Me.PopulateBOProperty(Me.State.MyBO, "AcceptPaymentByCheck", Me.moAcceptPaymentByCheckDrop, False, True)

                'REQ
                Me.PopulateBOProperty(Me.State.MyBO, "ClaimRecordingXcd", Me.moClaimRecordingDrop, False, True)
                Me.PopulateBOProperty(Me.State.MyBO, "ClaimRecordingCheckInventoryXcd", Me.ddlClaimRecordingCheckInventory, False, True)

                Me.PopulateBOProperty(Me.State.MyBO, "UseFraudMonitoringXcd", Me.ddlUseFraudMonitoring, False, True)

                'REQ 6156
                Me.PopulateBOProperty(Me.State.MyBO, "ImeiUseXcd", Me.cboImeiNoUse, False, True)

                Me.PopulateBOProperty(Me.State.MyBO, "BenefitSoldToAccount", Me.txtBenefitSoldToAccount)

                'US32,33,34 -- Thunder
                If (Not State.MyBO.DealerTypeDesc Is Nothing AndAlso State.MyBO.DealerTypeDesc.ToUpper() = State.MyBO.DEALER_TYPE_BENEFIT) Then
                    Me.PopulateBOProperty(Me.State.MyBO, "SuspendAppliesXcd", Me.ddlSuspenseApplies, False, True)
                    Me.PopulateBOProperty(Me.State.MyBO, "SourceSystemXcd", Me.ddlSourceSystem, False, True)
                    Me.PopulateBOProperty(Me.State.MyBO, "VoidDuration", Me.txtVoidDuration)
                    Me.PopulateBOProperty(Me.State.MyBO, "SuspendPeriod", Me.txtSuspensePeriod)
                    Me.PopulateBOProperty(Me.State.MyBO, "InvoiceCutoffDay", Me.txtInvCutOffDay)
                    Me.PopulateBOProperty(Me.State.MyBO, "BenefitCarrierCode", Me.txtBenefitCarrierCode)

                Else
                    Me.PopulateBOProperty(Me.State.MyBO, "SuspendAppliesXcd", Me.ddlSuspenseApplies, False, True)
                    Me.PopulateBOProperty(Me.State.MyBO, "SourceSystemXcd", Me.ddlSourceSystem, False, True)
                    Me.PopulateBOProperty(Me.State.MyBO, "VoidDuration", "")
                    Me.PopulateBOProperty(Me.State.MyBO, "SuspendPeriod", "")
                    Me.PopulateBOProperty(Me.State.MyBO, "InvoiceCutoffDay", "")
                    Me.PopulateBOProperty(Me.State.MyBO, "BenefitCarrierCode", "")
                End If

                'KDDI Changes'

                Me.PopulateBOProperty(Me.State.MyBO, "Is_Cancel_Shipment_Allowed", Me.cancelShipmentAllowedDrop, False, True)
                Me.PopulateBOProperty(Me.State.MyBO, "Cancel_Shipment_Grace_Period", Me.txtCancelShipmentGracePeriod)
                Me.PopulateBOProperty(Me.State.MyBO, "Is_Reshipment_Allowed", Me.reshipmentAllowedDrop, False, True)
                Me.PopulateBOProperty(Me.State.MyBO, "Validate_Address", Me.moValidateAddress, False, True)

                Me.PopulateBOProperty(Me.State.MyBO, "CaseProfileCode", Me.ddlCaseProfile, False, True)

                Me.PopulateBOProperty(Me.State.MyBO, "CloseCaseGracePeriodDays", Me.txtClosecaseperiod)
                Me.PopulateBOProperty(Me.State.MyBO, "Show_Previous_Caller_Info", Me.moShowPrevCallerInfo, False, True)
                Me.PopulateBOProperty(Me.State.MyBO, "DisplayDobXcd", Me.ddlDealerNameFlag, False, True)

            End With
            If Me.ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub

        Private Sub PopulateCompanyDropDown()

            'CompanyMultipleDrop.NothingSelected = True
            moMultipleColumnDrop.NothingSelected = True

            If ElitaPlusIdentity.Current.ActiveUser.Companies.Count > 1 AndAlso Me.State.MyBO.IsNew Then
                'CompanyMultipleDrop.SetControl(True, CompanyMultipleDrop.MODES.NEW_MODE, False, Me.State.companyDV, "* " & TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_COMPANY), True, False)
                moMultipleColumnDrop.SetControl(True, moMultipleColumnDrop.MODES.NEW_MODE, False, Me.State.companyDV, "* " & TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_COMPANY), True, False)
            Else
                'CompanyMultipleDrop.SetControl(True, CompanyMultipleDrop.MODES.EDIT_MODE, False, Me.State.companyDV, "", True, False)
                moMultipleColumnDrop.SetControl(True, moMultipleColumnDrop.MODES.EDIT_MODE, False, Me.State.companyDV, "", True, False)
            End If

        End Sub


        Protected Sub CreateNew()
            Me.State.ScreenSnapShotBO = Nothing 'Reset the backup copy

            Me.State.MyBO = New Dealer
            Me.State.MyBO.CompanyId = moMultipleColumnDrop.SelectedGuid 'CompanyMultipleDrop.SelectedGuid
            Me.State.IsCertificateExists = False

            Me.State.Ocompany = New Company(Me.State.MyBO.CompanyId)
            Me.State.SvcOrdersByDealer = Nothing
            State.DealerCountryID = State.Ocompany.CountryId

            Me.State.MyBO.UseSvcOrderAddress = False
            If LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, Me.State.Ocompany.ServiceOrdersByDealerId) = Codes.YESNO_Y Then
                Me.State.MyBO.UseSvcOrderAddress = True
                Me.State.SvcOrdersByDealer = Me.State.MyBO.SvcOrdersAddress
            End If

            Me.PopulateFormFromBOs()
            Me.EnableDisableFields()

            Me.PopulateCompanyDropDown()

        End Sub

        Protected Sub CreateNewWithCopy()
            Me.State.blnIsComingFromCopy = True
            Me.ClaimCloseRules.HideNewButton(True)
            Me.State.OldDealerId = Me.State.MyBO.Id

            Me.PopulateBOsFromForm()

            Dim newObj As New Dealer
            newObj.Copy(Me.State.MyBO)

            Me.State.MyBO = newObj
            Me.State.IsCertificateExists = False
            With State.MyBO
                .Dealer = Nothing
                .DealerName = Nothing
                .BankInfoId = Guid.Empty
                If .ClaimAutoApproveId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_Y)) Then
                    'Claim Types
                    Dim DlrClaimTypesIDs As New ArrayList
                    Dim dlrClaimTypeIdStr As String
                    For Each dlrClaimTypeIdStr In Me.UserControlAvailableSelectedClaimTypes.SelectedList
                        DlrClaimTypesIDs.Add(dlrClaimTypeIdStr)
                    Next
                    Me.State.MyBO.AttachClaimType(DlrClaimTypesIDs)

                    'Coverage Types
                    Dim DlrCoverageTypesIDs As New ArrayList
                    Dim dlrCoverageTypeIdStr As String
                    For Each dlrCoverageTypeIdStr In Me.UserControlAvailableSelectedCoverageTypes.SelectedList
                        DlrCoverageTypesIDs.Add(dlrCoverageTypeIdStr)
                    Next
                    Me.State.MyBO.AttachCoverageType(DlrCoverageTypesIDs)
                End If

            End With

            Me.PopulateFormFromBOs()
            Me.EnableDisableFields()

            'create the backup copy
            Me.State.ScreenSnapShotBO = New Dealer
            Me.State.ScreenSnapShotBO.Clone(Me.State.MyBO)

        End Sub

        Protected Sub SetupServiceCenterforVSC()

            Try
                Me.callPage(ServiceCenterForm.URL, New ServiceCenterForm.Parameters(Me.State.MyBO.Id, True))
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

        Protected Sub CheckIfComingFromSaveConfirm()
            Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value
            If Not Me.State.blnIsComingFromNew Then

                If Not confResponse Is Nothing AndAlso confResponse = MSG_VALUE_YES Then 'Me.CONFIRM_MESSAGE_OK Then
                    If Me.State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr Then
                        If Me.State.MyBO.IsDirty Then
                            Me.State.MyBO.Save()
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
                    Select Case Me.State.ActionInProgress
                        Case ElitaPlusPage.DetailPageCommand.Back
                            Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.HasDataChanged))
                        Case ElitaPlusPage.DetailPageCommand.New_
                            Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                            Me.CreateNew()
                        Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                            Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                            Me.CreateNewWithCopy()
                        Case ElitaPlusPage.DetailPageCommand.BackOnErr
                            Me.ReturnToCallingPage(New ReturnType(Me.State.ActionInProgress, Me.State.MyBO, Me.State.HasDataChanged))
                    End Select
                ElseIf Not confResponse Is Nothing AndAlso confResponse = MSG_VALUE_NO Then 'Me.CONFIRM_MESSAGE_CANCEL Then
                    Select Case Me.State.ActionInProgress
                        Case ElitaPlusPage.DetailPageCommand.Back
                            Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.HasDataChanged))
                        Case ElitaPlusPage.DetailPageCommand.New_
                            Me.CreateNew()
                        Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                            Me.CreateNewWithCopy()
                        Case ElitaPlusPage.DetailPageCommand.BackOnErr
                            Me.MasterPage.MessageController.AddErrorAndShow(Me.State.LastErrMsg)
                    End Select
                End If
                'Clean after consuming the action
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
            Else
                If Not confResponse Is Nothing AndAlso confResponse = MSG_VALUE_YES Then
                    'setup service center for this dealer, ONLY for VSC
                    SetupServiceCenterforVSC()
                ElseIf Not confResponse Is Nothing AndAlso confResponse = MSG_VALUE_NO Then 'Me.CONFIRM_MESSAGE_CANCEL Then
                    Select Case Me.State.ActionInProgress
                        Case ElitaPlusPage.DetailPageCommand.Back
                            Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.HasDataChanged))
                    End Select
                End If
            End If
            'Clean after consuming the action
            'Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
            Me.HiddenSaveChangesPromptResponse.Value = ""
        End Sub

        Private Function isDealerDuplicate() As Boolean

            Dim oCompanyNew As Company = New Company(Me.State.MyBO.CompanyId)
            Dim oCompanyTypeCode As String = LookupListNew.GetCodeFromId(LookupListNew.LK_COMPANY_TYPE, oCompanyNew.CompanyTypeId)
            Dim InsuranceTypeId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_COMPANY_TYPE, "1")

            If Me.State.MyBO.IsNew Then
                If Me.State.MyBO.GetDealerCountByCode() Then
                    Me.DisplayMessage(Message.MSG_DUPLICATE_DEALER_NOT_ALLOWED, "", MSG_BTN_OK, MSG_TYPE_ALERT, HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Cancel
                    Return True
                End If
            End If

            Return False

        End Function
#End Region

#Region "Button Clicks"

        Private Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
            Try
                Me.PopulateBOsFromForm()
                If Me.State.MyBO.IsDirty OrElse moBankInfo.State.IsBODirty Then
                    Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.HasDataChanged))
                End If

            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception

                If Me.State.MyBO.ConstrVoilation = False Then
                    Me.HandleErrors(ex, Me.MasterPage.MessageController)
                    Me.DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
                    Me.State.LastErrMsg = Me.MasterPage.MessageController.Text
                Else
                    Me.ReturnToCallingPage(New ReturnType(Me.State.ActionInProgress, Me.State.MyBO, Me.State.HasDataChanged))
                End If
            End Try
        End Sub

        Private Sub btnCopy_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCopy_WRITE.Click
            Try
                Me.PopulateBOsFromForm()
                If Me.State.MyBO.IsDirty OrElse moBankInfo.State.IsBODirty Then
                    Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                    'Me.AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
                Else
                    Me.CreateNewWithCopy()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnDelete_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete_WRITE.Click
            Try
                'Delete the Address
                'Me.State.MyBO.DeleteAndSave()
                'Me.State.HasDataChanged = True
                'Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Delete, Me.State.MyBO, Me.State.HasDataChanged))
                Me.DisplayMessage(Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, Me.HiddenDelDeletePromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

        Private Sub btnNew_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew_WRITE.Click
            Try
                Me.PopulateBOsFromForm()
                If Me.State.MyBO.IsDirty OrElse moBankInfo.State.IsBODirty Then
                    Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
                Else
                    Me.CreateNew()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnApply_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnApply_WRITE.Click

            'Dim blnIsComingFromNew As Boolean
            Dim exitingDealerCode As String = String.Empty

            Try
                exitingDealerCode = Me.State.MyBO.Dealer
                Me.PopulateBOsFromForm()
                If Not isDealerDuplicate() Then
                    If Me.State.MyBO.IsDirty OrElse moBankInfo.State.IsBODirty OrElse Me.State.MyBO.IsFamilyDirty Then
                        ' check HERE !! if coming from clicking on New or New with Copy button
                        Me.State.blnIsComingFromNew = Me.State.MyBO.IsNew
                        If Me.State.MyBO.UseSvcOrderAddress Then
                            Me.State.SvcOrdersByDealer.Validate()
                        End If

                        If Me.State.MyBO.ClaimAutoApproveId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_Y)) Then
                            Me.State.MyBO.DealerClaimTypeSelectionCount = UserControlAvailableSelectedClaimTypes.SelectedList.Count
                            Me.State.MyBO.DealerCoverageTypeSelectionCount = UserControlAvailableSelectedCoverageTypes.SelectedList.Count
                        End If

                        If Me.State.MyBO.IsDirty OrElse Me.State.MyBO.IsFamilyDirty Then
                            ' Checking for Existing Certificates if Dealer Code has changes
                            If Not Me.State.blnIsComingFromNew Then
                                ' There is change in Dealer Code
                                If exitingDealerCode <> Me.State.MyBO.Dealer Then
                                    If (Me.State.MyBO.GetDealerCertificatesCount() > 0) Then
                                        Me.DisplayMessage(Message.MSG_DEALER_ALREADY_HAS_CERTIFICATES_DEFINITIONS_DEALERCODE_CANNOT_BE_UPDATED, "", Me.MSG_BTN_OK, Me.MSG_TYPE_ALERT)
                                        Exit Try
                                    End If
                                End If
                            End If

                            Me.ShareCustSave()
                            Me.State.MyBO.Save()

                            If Me.ddlFullfileProcess.Visible = True And LookupListNew.GetCodeFromId(LookupListNew.LK_FLP, Me.State.MyBO.UseFullFileProcessId) <> Codes.FLP_NO Then
                                Me.State.MyBO.CreateExternalTable()
                            End If
                            'REQ-5467 : If LawsuitMandatory Value has changed from No to YES then create a Asynchronous Job to Update any Claims that needs to catch up
                            If Me.State.LawsuitMandatoryIdAtLoad.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_N)) AndAlso
                               Me.State.MyBO.IsLawsuitMandatoryId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_Y)) Then
                                Me.State.MyBO.UpdateClaimsAsyncForLawsuit()
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
                        If (Me.State.blnIsComingFromCopy) Then
                            ''''clone Claim Close Rules
                            Dim objCloseClaimRules As New ClaimCloseRules
                            objCloseClaimRules.CopyClaimCloseRulesToNewDealer(Me.State.Ocompany.Id, Me.State.OldDealerId, Me.State.MyBO.Id)
                            Me.State.blnIsComingFromCopy = False
                            Me.ClaimCloseRules.HideNewButton(False)
                        End If

                        Me.State.HasDataChanged = True
                        If Me.State.IsNew = True Then
                            Me.State.IsNew = False
                        End If
                        Me.PopulateFormFromBOs()
                        Me.EnableDisableFields()
                        If Me.State.blnIsComingFromNew And
                           LookupListNew.GetCodeFromId(LookupListNew.LK_DEALER_TYPE, Me.State.MyBO.DealerTypeId) = Codes.DEALER_TYPES__VSC Then
                            Me.DisplayMessage(Message.MSG_PROMPT_FOR_SETUP_SERVICE_CENTER_OF_DEALER, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                        Else
                            'Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                            Me.MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
                        End If
                    Else
                        'Me.AddInfoMsg(Message.MSG_RECORD_NOT_SAVED)
                        Me.MasterPage.MessageController.AddInformation(Message.MSG_RECORD_NOT_SAVED)
                    End If
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnUndo_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUndo_WRITE.Click
            Try
                If Not Me.State.MyBO.IsNew Then
                    'Reload from the DB
                    Me.State.MyBO = New Dealer(Me.State.MyBO.Id)
                ElseIf Not Me.State.ScreenSnapShotBO Is Nothing Then
                    'It was a new with copy
                    Me.State.MyBO.Clone(Me.State.ScreenSnapShotBO)
                Else
                    'Me.PopulateBOsFromForm()
                    CreateNew()
                    'Me.State.MyBO = New Dealer
                End If

                Me.State.blnIsComingFromCopy = False
                'Me.ClaimCloseRules.HideNewButton(False)

                Me.PopulateFormFromBOs()
                Me.EnableDisableFields()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
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
        Private Sub moClaimSystemDrop_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles moClaimSystemDrop.SelectedIndexChanged

            If Me.moClaimSystemDrop.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
                If Not GetSelectedItem(Me.moClaimSystemDrop).Equals(System.Guid.Empty) AndAlso GetSelectedItem(Me.moClaimSystemDrop).Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_CLAIM_SYSTEM, "ELITA")) Then
                    ControlMgr.SetVisibleControl(Me, trClaimRec, True)
                Else
                    ControlMgr.SetVisibleControl(Me, trClaimRec, False)
                End If
            End If
        End Sub
        Private Sub cboCertificatesAutonumberId_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCertificatesAutonumberId.SelectedIndexChanged

            If Me.cboCertificatesAutonumberId.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
                If GetSelectedItem(Me.cboCertificatesAutonumberId).Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_Y)) Then
                    ControlMgr.SetVisibleControl(Me, Me.lblCertificatesAutonumberPrefix, True)
                    ControlMgr.SetVisibleControl(Me, Me.txtCertificatesAutonumberPrefix, True)
                    ControlMgr.SetVisibleControl(Me, Me.lblMaxCertNumLengthAlwd, True)
                    ControlMgr.SetVisibleControl(Me, Me.txtMaxCertNumLengthAlwd, True)
                Else
                    txtCertificatesAutonumberPrefix.Text = String.Empty
                    ControlMgr.SetVisibleControl(Me, Me.lblCertificatesAutonumberPrefix, False)
                    ControlMgr.SetVisibleControl(Me, Me.txtCertificatesAutonumberPrefix, False)
                    txtMaxCertNumLengthAlwd.Text = String.Empty
                    ControlMgr.SetVisibleControl(Me, Me.lblMaxCertNumLengthAlwd, False)
                    ControlMgr.SetVisibleControl(Me, Me.txtMaxCertNumLengthAlwd, False)
                End If
            End If
        End Sub

        Protected Sub moClaimAutoApproveDrop_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles moClaimAutoApproveDrop.SelectedIndexChanged

            If GetSelectedItem(Me.moClaimAutoApproveDrop).Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_Y)) Then
                'ControlMgr.SetVisibleControl(Me, Me.ClaimTypesRow, True)
                'ControlMgr.SetVisibleControl(Me, Me.CoverageTypesRow, True)
                ControlMgr.SetVisibleControl(Me, Me.pnlTypesRow, True)
                Me.PopulateClaimAutoApproveControls()
            Else
                'ControlMgr.SetVisibleControl(Me, Me.ClaimTypesRow, False)
                'ControlMgr.SetVisibleControl(Me, Me.CoverageTypesRow, False)
                ControlMgr.SetVisibleControl(Me, Me.pnlTypesRow, False)
                Me.State.MyBO.DetachClaimType(Me.UserControlAvailableSelectedClaimTypes.SelectedList)
                Me.State.MyBO.DetachCoverageType(Me.UserControlAvailableSelectedCoverageTypes.SelectedList)
            End If

        End Sub

        Private Sub moAutoProcessFileDrop_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles moAutoProcessFileDrop.SelectedIndexChanged
            'Keep This For Future Use
        End Sub

        Private Sub OnFromDrop_Changed(ByVal fromMultipleDrop As MultipleColumnDDLabelControl_New) _
            Handles moMultipleColumnDrop.SelectedDropChanged
            Try
                Me.State.MyBO.CompanyId = moMultipleColumnDrop.SelectedGuid
                Me.State.Ocompany = New Company(Me.State.MyBO.CompanyId)
                If Not Me.State.Ocompany.CertnumlookupbyId.Equals(Guid.Empty) Then
                    Me.ddlCertNumLookUpBy.ClearSelection()
                    ControlMgr.SetVisibleControl(Me, Me.lblCertNumLookUpBy, False)
                    ControlMgr.SetVisibleControl(Me, Me.ddlCertNumLookUpBy, False)
                Else
                    ControlMgr.SetVisibleControl(Me, Me.lblCertNumLookUpBy, True)
                    ControlMgr.SetVisibleControl(Me, Me.ddlCertNumLookUpBy, True)
                End If

                PopulateDropdowns()
                PopulateAddressFields()

                Me.State.SvcOrdersByDealer = Nothing
                Me.State.MyBO.UseSvcOrderAddress = False
                If LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, Me.State.Ocompany.ServiceOrdersByDealerId) = Codes.YESNO_Y Then
                    Me.State.MyBO.UseSvcOrderAddress = True
                    Me.State.SvcOrdersByDealer = Me.State.MyBO.SvcOrdersAddress
                    PopulateSvcOrdersAddressFields()
                End If
            Catch ex As Exception
                HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub ddlFullfileProcess_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlFullfileProcess.SelectedIndexChanged
            If LookupListNew.GetCodeFromId(LookupListNew.LK_FLP, GetSelectedItem(Me.ddlFullfileProcess)) <> Codes.FLP_NO Then
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

            If Me.State.IsNew Then
                'Set country to the country of selected company
                If Not Me.State.MyBO.CompanyId.Equals(Guid.Empty) Then
                    'Dim allCountryList As DataView = LookupListNew.GetCountryLookupList()
                    'BindListControlToDataView(CType(Me.AddressCtr.FindControl("moCountryDrop_WRITE"), DropDownList), allCountryList, , , True)
                    Dim countryList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="Country", languageCode:=Thread.CurrentPrincipal.GetLanguageCode())
                    CType(Me.AddressCtr.FindControl("moCountryDrop_WRITE"), DropDownList).Populate(countryList, populateOptions)

                    'Dim oCountryList As DataView = LookupListNew.GetCountryLookupList(Me.State.MyBO.CompanyId)
                    Dim ListContext1 As New ListContext
                    ListContext1.CompanyId = Me.State.MyBO.CompanyId
                    Dim countryListForCompany As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="CountryByCompany", context:=ListContext1)

                    If countryListForCompany.Count > 0 Then
                        Me.State.MyBO.Address.CountryId = countryListForCompany.FirstOrDefault().ListItemId
                        SetSelectedItem(CType(Me.AddressCtr.FindControl("moCountryDrop_WRITE"), DropDownList), Me.State.MyBO.Address.CountryId)
                    End If

                    If Not Me.State.MyBO.Address.CountryId.Equals(Guid.Empty) Then
                        CType(Me.AddressCtr.FindControl("moCountryText"), TextBox).Text = (From lst In countryList
                            Where lst.ListItemId = Me.State.MyBO.Address.CountryId
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
                    listcontext2.CountryId = Me.State.MyBO.Address.CountryId
                    CType(Me.AddressCtr.FindControl("moRegionDrop_WRITE"), DropDownList).Populate(CommonConfigManager.Current.ListManager.GetList("RegionsByCountry", Thread.CurrentPrincipal.GetLanguageCode(), listcontext2), populateOptions)
                End If
            End If
        End Sub

        Private Sub PopulateMailingAddressFields()
            Dim populateOptions As PopulateOptions = New PopulateOptions() With
                    {
                    .AddBlankItem = True
                    }

            If Me.State.IsNew Then
                'Set country to the country of selected company
                If Not Me.State.MyBO.CompanyId.Equals(Guid.Empty) Then
                    'Dim allCountryList As DataView = LookupListNew.GetCountryLookupList()
                    'BindListControlToDataView(CType(Me.MailingAddressCtr.FindControl("moCountryDrop_WRITE"), DropDownList), allCountryList, , , True)
                    Dim countryList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="Country", languageCode:=Thread.CurrentPrincipal.GetLanguageCode())
                    CType(Me.MailingAddressCtr.FindControl("moCountryDrop_WRITE"), DropDownList).Populate(countryList, populateOptions)

                    'Dim oCountryList As DataView = LookupListNew.GetCountryLookupList(Me.State.MyBO.CompanyId)
                    Dim ListContext1 As New ListContext
                    ListContext1.CompanyId = Me.State.MyBO.CompanyId
                    Dim countryListForCompany As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="CountryByCompany", context:=ListContext1)

                    If countryListForCompany.Count > 0 Then
                        Me.State.MyBO.MailingAddress.CountryId = countryListForCompany.FirstOrDefault().ListItemId
                        SetSelectedItem(CType(Me.MailingAddressCtr.FindControl("moCountryDrop_WRITE"), DropDownList), Me.State.MyBO.MailingAddress.CountryId)
                    End If

                    If Not Me.State.MyBO.MailingAddress.CountryId.Equals(Guid.Empty) Then
                        CType(Me.MailingAddressCtr.FindControl("moCountryText"), TextBox).Text = (From lst In countryList
                            Where lst.ListItemId = Me.State.MyBO.MailingAddress.CountryId
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
                    listcontext2.CountryId = Me.State.MyBO.MailingAddress.CountryId
                    CType(Me.MailingAddressCtr.FindControl("moRegionDrop_WRITE"), DropDownList).Populate(CommonConfigManager.Current.ListManager.GetList("RegionsByCountry", Thread.CurrentPrincipal.GetLanguageCode(), listcontext2), populateOptions)
                End If
            End If

        End Sub

        Private Sub PopulateSvcOrdersAddressFields()
            Dim populateOptions As PopulateOptions = New PopulateOptions() With
                    {
                    .AddBlankItem = True
                    }

            If Me.State.IsNew Then
                'Set country to the country of selected company
                If Not Me.State.MyBO.CompanyId.Equals(Guid.Empty) Then
                    'Dim allCountryList As DataView = LookupListNew.GetCountryLookupList()
                    'BindListControlToDataView(CType(Me.SvcOrderAddressCtr.FindControl("moCountryDrop_WRITE"), DropDownList), allCountryList, , , True)
                    Dim countryList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="Country", languageCode:=Thread.CurrentPrincipal.GetLanguageCode())
                    CType(Me.SvcOrderAddressCtr.FindControl("moCountryDrop_WRITE"), DropDownList).Populate(countryList, populateOptions)

                    'Dim oCountryList As DataView = LookupListNew.GetCountryLookupList(Me.State.MyBO.CompanyId)
                    Dim ListContext1 As New ListContext
                    ListContext1.CompanyId = Me.State.MyBO.CompanyId
                    Dim countryListForCompany As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="CountryByCompany", context:=ListContext1)

                    If countryListForCompany.Count > 0 Then
                        Me.State.SvcOrdersByDealer.Address.CountryId = countryListForCompany.FirstOrDefault().ListItemId
                        SetSelectedItem(CType(Me.SvcOrderAddressCtr.FindControl("moCountryDrop_WRITE"), DropDownList), Me.State.SvcOrdersByDealer.Address.CountryId)
                    End If

                    If Not Me.State.SvcOrdersByDealer.Address.CountryId.Equals(Guid.Empty) Then
                        CType(Me.SvcOrderAddressCtr.FindControl("moCountryText"), TextBox).Text = (From lst In countryList
                            Where lst.ListItemId = Me.State.SvcOrdersByDealer.Address.CountryId
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
                    listcontext2.CountryId = Me.State.SvcOrdersByDealer.Address.CountryId
                    CType(Me.SvcOrderAddressCtr.FindControl("moRegionDrop_WRITE"), DropDownList).Populate(CommonConfigManager.Current.ListManager.GetList("RegionsByCountry", Thread.CurrentPrincipal.GetLanguageCode(), listcontext2), populateOptions)
                End If
            End If

        End Sub

        Private Sub moUseEquipment_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles moUseEquipment.SelectedIndexChanged
            Me.PopulateBOProperty(Me.State.MyBO, "UseEquipmentId", Me.moUseEquipment)
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
            If (Not Me.State Is Nothing) Then
                If (Not Me.State Is Nothing) Then
                    Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("DEALER")
                    Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("DEALER")
                End If
            End If
        End Sub
        Private Sub moDealerGroupDrop_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles moDealerGroupDrop.SelectedIndexChanged
            CheckUseClientDealerCodeFlag()
        End Sub
        Private Sub moDealerTypeDrop_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles moDealerTypeDrop.SelectedIndexChanged
            Me.PopulateBOProperty(Me.State.MyBO, "DealerTypeId", Me.moDealerTypeDrop)
            ControlMgr.SetVisibleControl(Me, trLineHid1, False)
            ControlMgr.SetVisibleControl(Me, trHid1, False)
            ControlMgr.SetVisibleControl(Me, trHid2, False)
            ControlMgr.SetVisibleControl(Me, dlstRetailerID, True)
            ControlMgr.SetVisibleControl(Me, lblDealerIsRetailer, True)
            DisabledTabsList.Add(Tab_MailingAddress)
            Me.State.MyBO.Address.AddressIsRequire = False

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

            If Me.State.MyBO.DealerTypeDesc = Me.State.MyBO.DEALER_TYPE_DESC Then
                ControlMgr.SetVisibleControl(Me, trLineHid1, True)
                ControlMgr.SetVisibleControl(Me, trHid1, True)
                ControlMgr.SetVisibleControl(Me, trHid2, True)
                MailingAddressCtr.EnableControls(False, True)
                PopulateMailingAddressFields()
                ControlMgr.SetVisibleControl(Me, dlstRetailerID, False)
                BindSelectItem(LookupListNew.GetIdFromCode(LookupListNew.GetYesNoLookupList(Authentication.LangId), "Y").ToString, Me.dlstRetailerID)
                'Me.State.MyBO.RetailerId = 
                ControlMgr.SetVisibleControl(Me, lblDealerIsRetailer, False)
                Me.State.MyBO.Address.AddressIsRequire = True
                PopulateBOProperty(Me.State.MyBO, "UseEquipmentId", LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, "N"))
                SetSelectedItem(Me.moUseEquipment, Me.State.MyBO.UseEquipmentId)
                'Req-1142 Start
                ControlMgr.SetVisibleControl(Me, trVscLicenseTag, True)
                ControlMgr.SetVisibleControl(Me, trVscVinRestrict, True)
                If Me.State.MyBO.LicenseTagValidationId.Equals(Guid.Empty) Then
                    BindSelectItem(LookupListNew.GetIdFromCode(LookupListNew.GetLicenseTagFlag(Authentication.LangId), LICENSE_TAG_FLAG_YES).ToString, moLicenseTagMandatory)
                End If
                'Req-1142 End
            ElseIf (Me.State.MyBO.DealerTypeDesc = Me.State.MyBO.DEALER_TYPE_DESC_WEPP) Then
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

                Me.PopulateControlFromBOProperty(Me.txtVoidDuration, State.MyBO.VoidDuration)
                Me.PopulateControlFromBOProperty(Me.txtInvCutOffDay, State.MyBO.InvoiceCutoffDay)
                Me.PopulateControlFromBOProperty(Me.txtBenefitCarrierCode, State.MyBO.BenefitCarrierCode)
                Me.PopulateControlFromBOProperty(Me.txtBenefitSoldToAccount, State.MyBO.BenefitSoldToAccount)

                If (ddlSuspenseApplies.Items.Count > 0 AndAlso Not ddlSuspenseApplies.SelectedItem Is Nothing AndAlso ddlSuspenseApplies.SelectedItem.Value.ToUpper() = "YESNO-Y") Then
                    Me.PopulateControlFromBOProperty(Me.txtSuspensePeriod, State.MyBO.SuspendPeriod)
                    txtSuspensePeriod.Style.Add("display", "block")
                    lblSuspensePeriod.Style.Add("display", "block")
                Else
                    txtSuspensePeriod.Style.Add("display", "none")
                    lblSuspensePeriod.Style.Add("display", "none")
                End If


            Else
                PopulateBOProperty(Me.State.MyBO, "UseEquipmentId", LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, "N"))
                SetSelectedItem(Me.moUseEquipment, Me.State.MyBO.UseEquipmentId)
            End If

        End Sub

        Public Sub ServiceCenterMessage()
            Me.DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)
        End Sub

#Region "MerchantCode_Handlers_Grid"

        Protected Sub RowCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
            Dim nIndex As Integer

            Try
                If e.CommandName = EDIT_COMMAND_NAME Then

                    nIndex = CInt(e.CommandArgument)
                    moMerchantCodesDatagrid.EditIndex = nIndex
                    moMerchantCodesDatagrid.SelectedIndex = nIndex

                    'DEF-3066
                    Me.State.SelectedCompanyCreditCardType = CType(Me.moMerchantCodesDatagrid.Rows(nIndex).Cells(Me.COMPANY_CREDIT_CARD_TYPE).FindControl(Me.COMPANY_CREDIT_CARD_TYPE_LABEL_CONTROL_NAME), Label).Text
                    'DEF-3066

                    Me.State.IsMerchantCodeEditMode = True
                    Me.State.MerchantCodeID = New Guid(CType(Me.moMerchantCodesDatagrid.Rows(nIndex).Cells(Me.MERCHANT_CODE_ID).FindControl(Me.ID_CONTROL_NAME), Label).Text)
                    Me.State.MyMerchantCodeBO = New MerchantCode(Me.State.MerchantCodeID)
                    PopulateMyMerchantCodeGrid()

                    'Disable all Edit and Delete icon buttons on the Grid
                    SetGridControls(Me.moMerchantCodesDatagrid, False)
                    Me.State.PageIndex = moMerchantCodesDatagrid.PageIndex

                    'DEF-3066
                    Me.State.MerchantCodeEdit = True
                    'DEF-3066

                    PopulateMerchantCodeFormFromBO(nIndex)
                    SetFocusOnEditableFieldInGrid(Me.moMerchantCodesDatagrid, COMPANY_CREDIT_CARD_TYPE, COMPANY_CREDIT_CARD_CONTROL_NAME, nIndex)
                    SetMerchantCodeButtonsState(True)

                ElseIf (e.CommandName = DELETE_COMMAND) Then

                    'Do the delete here
                    nIndex = CInt(e.CommandArgument)

                    Me.PopulateMyMerchantCodeGrid()
                    Me.State.PageIndex = moMerchantCodesDatagrid.PageIndex

                    'Clear the SelectedItemStyle to remove the highlight from the previously saved row
                    moMerchantCodesDatagrid.SelectedIndex = Me.NO_ROW_SELECTED_INDEX
                    'Save the Id in the Session
                    Me.State.MerchantCodeID = New Guid(CType(Me.moMerchantCodesDatagrid.Rows(nIndex).Cells(Me.MERCHANT_CODE_ID).FindControl(Me.ID_CONTROL_NAME), Label).Text)
                    Me.DisplayMessage(Message.DELETE_RECORD_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenDeletePromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete

                ElseIf (e.CommandName = Me.SAVE_COMMAND) Then

                    SaveRecord()
                ElseIf (e.CommandName = Me.CANCEL_COMMAND) Then
                    CancelRecord()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

        Protected Sub RowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
            BaseItemCreated(sender, e)
        End Sub

        Private Sub Grid_Sorting(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles moMerchantCodesDatagrid.Sorting

            Try
                If Me.State.MerchantCodeSortExpression.StartsWith(e.SortExpression) Then
                    If Me.State.MerchantCodeSortExpression.EndsWith(" DESC") Then
                        Me.State.MerchantCodeSortExpression = e.SortExpression
                    Else
                        Me.State.MerchantCodeSortExpression &= " DESC"
                    End If
                Else
                    Me.State.MerchantCodeSortExpression = e.SortExpression
                End If

                Me.State.MerchantCodeID = Guid.Empty
                Me.moMerchantCodesDatagrid.PageIndex = 0
                Me.moMerchantCodesDatagrid.SelectedIndex = -1
                Me.PopulateMyMerchantCodeGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

        Private Sub Grid_PageIndexChanging(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles moMerchantCodesDatagrid.PageIndexChanging
            Try
                If (Not (Me.State.IsMerchantCodeEditMode)) Then
                    Me.State.PageIndex = e.NewPageIndex
                    Me.moMerchantCodesDatagrid.PageIndex = Me.State.PageIndex
                    Me.PopulateMyMerchantCodeGrid()
                    Me.moMerchantCodesDatagrid.SelectedIndex = Me.NO_ITEM_SELECTED_INDEX
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

        Private Sub Grid_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles moMerchantCodesDatagrid.RowDataBound
            Try
                Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

                Dim populateOptions As PopulateOptions = New PopulateOptions() With
                        {
                        .AddBlankItem = True
                        }

                If Not dvRow Is Nothing And Not State.MerchantCodeSearchDV.Count > 0 Then
                    If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Or itemType = ListItemType.EditItem Then
                        CType(e.Row.Cells(MERCHANT_CODE_ID).FindControl(ID_CONTROL_NAME), Label).Text = GetGuidStringFromByteArray(CType(dvRow(MerchantCode.MerchantCodeSearchDV.COL_MERCHANT_CODE_ID), Byte()))
                        If (Me.State.IsMerchantCodeEditMode = True AndAlso Me.State.MerchantCodeID.ToString.Equals(GetGuidStringFromByteArray(CType(dvRow(MerchantCode.MerchantCodeSearchDV.COL_MERCHANT_CODE_ID), Byte())))) Then
                            'BindListControlToDataView(CType(e.Row.Cells(Me.COMPANY_CREDIT_CARD_ID).FindControl(Me.COMPANY_CREDIT_CARD_CONTROL_NAME), DropDownList), LookupListNew.GetCompanyCreditCardsLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId, Me.State.MyBO.CompanyId))
                            Dim listcontext1 As ListContext = New ListContext()
                            listcontext1.CompanyId = Me.State.MyBO.CompanyId
                            CType(e.Row.Cells(Me.COMPANY_CREDIT_CARD_ID).FindControl(Me.COMPANY_CREDIT_CARD_CONTROL_NAME), DropDownList).Populate(CommonConfigManager.Current.ListManager.GetList(listCode:="CreditCardByCompany", languageCode:=Thread.CurrentPrincipal.GetLanguageCode(), context:=listcontext1), populateOptions)

                            SetSelectedItem(CType(e.Row.Cells(Me.COMPANY_CREDIT_CARD_ID).FindControl(Me.COMPANY_CREDIT_CARD_CONTROL_NAME), DropDownList), dvRow(MerchantCode.MerchantCodeSearchDV.COL_COMPANY_CREDIT_CARD_ID).ToString)
                            CType(e.Row.Cells(Me.MERCHANT_CODE).FindControl(MERCHANT_CODE_TEXTBOX_CONTROL_NAME), TextBox).Text = dvRow(MerchantCode.MerchantCodeSearchDV.COL_MERCHANT_CODE).ToString

                        Else
                            CType(e.Row.Cells(Me.COMPANY_CREDIT_CARD_TYPE).FindControl(COMPANY_CREDIT_CARD_TYPE_LABEL_CONTROL_NAME), Label).Text = dvRow(MerchantCode.MerchantCodeSearchDV.COL_COMPANY_CREDIT_CARD_TYPE).ToString
                            CType(e.Row.Cells(Me.MERCHANT_CODE).FindControl(MERCHANT_CODE_LABEL_CONTROL_NAME), Label).Text = dvRow(MerchantCode.MerchantCodeSearchDV.COL_MERCHANT_CODE).ToString
                        End If
                    End If
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub PopulateMyMerchantCodeGrid()

            Try
                If Me.State.MerchantCodeSearchDV Is Nothing Then
                    Me.State.MerchantCodeSearchDV = GetMerchantCodeDV()

                    'DEF-3066
                    If Me.State.PreviousMerchantCodeSearchDV Is Nothing Then
                        Me.State.PreviousMerchantCodeSearchDV = GetMerchantCodeDV()
                    End If
                    'DEF-3066
                End If
                Dim dv As MerchantCode.MerchantCodeSearchDV

                If State.MerchantCodeSearchDV.Count = 0 Then
                    dv = Me.State.MerchantCodeSearchDV.AddNewRowToEmptyDV
                    SetPageAndSelectedIndexFromGuid(dv, Me.State.MerchantCodeID, Me.moMerchantCodesDatagrid, Me.State.PageIndex)
                    Me.moMerchantCodesDatagrid.DataSource = dv
                Else
                    SetPageAndSelectedIndexFromGuid(Me.State.MerchantCodeSearchDV, Me.State.MerchantCodeID, Me.moMerchantCodesDatagrid, Me.State.PageIndex)
                    Me.moMerchantCodesDatagrid.DataSource = Me.State.MerchantCodeSearchDV
                End If

                Me.State.MerchantCodeSearchDV.Sort = Me.State.MerchantCodeSortExpression
                If (Me.State.IsMerchantCodeAfterSave) Then
                    Me.State.IsMerchantCodeAfterSave = False
                    Me.SetPageAndSelectedIndexFromGuid(Me.State.MerchantCodeSearchDV, Me.State.MerchantCodeID, Me.moMerchantCodesDatagrid, Me.moMerchantCodesDatagrid.PageIndex)
                ElseIf (Me.State.IsMerchantCodeEditMode) Then
                    Me.SetPageAndSelectedIndexFromGuid(Me.State.MerchantCodeSearchDV, Me.State.MerchantCodeID, Me.moMerchantCodesDatagrid, Me.moMerchantCodesDatagrid.PageIndex, Me.State.IsMerchantCodeEditMode)
                Else
                    'In a Delete scenario...
                    Me.SetPageAndSelectedIndexFromGuid(Me.State.MerchantCodeSearchDV, Guid.Empty, Me.moMerchantCodesDatagrid, Me.moMerchantCodesDatagrid.PageIndex, Me.State.IsMerchantCodeEditMode)
                End If

                Me.moMerchantCodesDatagrid.AutoGenerateColumns = False

                If State.MerchantCodeSearchDV.Count = 0 Then
                    SortAndBindGrid(dv)
                Else
                    SortAndBindGrid(Me.State.MerchantCodeSearchDV)
                End If

                If State.MerchantCodeSearchDV.Count = 0 Then
                    For Each gvRow As GridViewRow In Me.moMerchantCodesDatagrid.Rows
                        gvRow.Visible = False
                        gvRow.Controls.Clear()
                    Next
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

        Protected Sub BindBoPropertiesToGridHeaders()
            Me.BindBOPropertyToGridHeader(Me.State.MyMerchantCodeBO, "CompanyCreditCardId", Me.moMerchantCodesDatagrid.Columns(Me.COMPANY_CREDIT_CARD_TYPE))
            Me.BindBOPropertyToGridHeader(Me.State.MyMerchantCodeBO, "MerchantCode", Me.moMerchantCodesDatagrid.Columns(Me.MERCHANT_CODE))
            Me.ClearGridViewHeadersAndLabelsErrorSign()
        End Sub

        Private Sub SetFocusOnEditableFieldInGrid(ByVal grid As GridView, ByVal cellPosition As Integer, ByVal controlName As String, ByVal itemIndex As Integer)
            'Set focus on the Description TextBox for the EditItemIndex row
            Dim code As DropDownList = CType(grid.Rows(itemIndex).Cells(cellPosition).FindControl(controlName), DropDownList)
            SetFocus(code)
        End Sub

        Private Sub PopulateMerchantCodeFormFromBO(Optional ByVal gridRowIdx As Integer = Nothing)

            If gridRowIdx.Equals(Nothing) Then gridRowIdx = Me.moMerchantCodesDatagrid.EditIndex

            'DEF-3066        
            Dim cardDV As DataView = LookupListNew.GetCompanyCreditCardsLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId, Me.State.MyBO.CompanyId)

            ' A variable to store the DESCRIPTION of company credit card type which can be applied in row filter
            Dim filterStr As String = ""
            For Each dr As DataRowView In Me.State.PreviousMerchantCodeSearchDV
                If Me.State.MerchantCodeAddNew Then
                    If filterStr = "" Then
                        filterStr = "'" + dr.Row(Me.State.MerchantCodeSearchDV.COL_COMPANY_CREDIT_CARD_TYPE).ToString() + "'"
                    Else
                        filterStr = filterStr + ",'" + dr.Row(Me.State.MerchantCodeSearchDV.COL_COMPANY_CREDIT_CARD_TYPE).ToString() + "'"
                    End If
                ElseIf Me.State.MerchantCodeEdit Then
                    If dr.Row(Me.State.MerchantCodeSearchDV.COL_COMPANY_CREDIT_CARD_TYPE).ToString() <> Me.State.SelectedCompanyCreditCardType Then
                        If filterStr = "" Then
                            filterStr = "'" + dr.Row(Me.State.MerchantCodeSearchDV.COL_COMPANY_CREDIT_CARD_TYPE).ToString() + "'"
                        Else
                            filterStr = filterStr + ",'" + dr.Row(Me.State.MerchantCodeSearchDV.COL_COMPANY_CREDIT_CARD_TYPE).ToString() + "'"
                        End If
                    End If
                End If
            Next

            If filterStr <> "" Then
                ' Row filter of card data view already contains the language id condition which we are passing below as it is
                cardDV.RowFilter = String.Format("{0} and DESCRIPTION not in ({1})", cardDV.RowFilter, filterStr)
            End If

            BindListControlToDataView(CType(Me.moMerchantCodesDatagrid.Rows(gridRowIdx).Cells(COMPANY_CREDIT_CARD_ID).FindControl(COMPANY_CREDIT_CARD_CONTROL_NAME), DropDownList), cardDV)

            Me.State.MerchantCodeAddNew = False
            Me.State.MerchantCodeEdit = False

            'DEF-3066

            Try
                With Me.State.MyMerchantCodeBO

                    If (Not .Id.Equals(Guid.Empty)) Then
                        Dim cboCompanyCreditCard As DropDownList = CType(Me.moMerchantCodesDatagrid.Rows(gridRowIdx).Cells(COMPANY_CREDIT_CARD_ID).FindControl(COMPANY_CREDIT_CARD_CONTROL_NAME), DropDownList)
                        If (Not .IsNew AndAlso Not .CreditCardFormatId.Equals(Guid.Empty)) Then Me.PopulateControlFromBOProperty(cboCompanyCreditCard, .CreditCardFormatId)
                    End If

                    Dim txtMerchantCode As TextBox = CType(Me.moMerchantCodesDatagrid.Rows(gridRowIdx).Cells(MERCHANT_CODE).FindControl(MERCHANT_CODE_TEXTBOX_CONTROL_NAME), TextBox)
                    Me.PopulateControlFromBOProperty(txtMerchantCode, .MerchantCode)

                    CType(Me.moMerchantCodesDatagrid.Rows(gridRowIdx).Cells(MERCHANT_CODE_ID).FindControl(ID_CONTROL_NAME), Label).Text = .Id.ToString
                    CType(Me.moMerchantCodesDatagrid.Rows(gridRowIdx).Cells(COMPANY_CREDIT_CARD_ID).FindControl(COMPANY_CREDIT_CARD_ID_LABEL_CONTROL_NAME), Label).Text = .CompanyCreditCardId.ToString


                End With
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

        Private Sub SetMerchantCodeButtonsState(ByVal bIsEdit As Boolean)

            If Me.State.IsEditMode Then
                ControlMgr.SetEnableControl(Me, btnNew_WRITE, False)
                ' Me.MenuEnabled = False
            ElseIf Me.State.IsReadOnly Then
                ControlMgr.SetEnableControl(Me, btnNew_WRITE, False)
                'Diable the Edit and Delete Buttons as disabled
            Else
                If Me.State.Action <> Me.COPY_SCHEDULE Then
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


        Public Overrides Sub BaseSetButtonsState(ByVal bIsEdit As Boolean)
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
            dv.Sort = Me.moMerchantCodesDatagrid.DataMember()
            Me.moMerchantCodesDatagrid.DataSource = dv

            Return (dv)

        End Function

        Private Function GetDataView() As MerchantCode.MerchantCodeSearchDV

            Return MerchantCode.LoadList(Me.State.MyBO.Id)

        End Function

        Private Sub SortAndBindGrid(ByVal dvBinding As DataView, Optional ByVal blnEmptyList As Boolean = False)
            Me.moMerchantCodesDatagrid.DataSource = dvBinding
            HighLightSortColumn(Me.moMerchantCodesDatagrid, Me.State.MerchantCodeSortExpression)
            Me.moMerchantCodesDatagrid.DataBind()
            If Not Me.moMerchantCodesDatagrid.BottomPagerRow.Visible Then Me.moMerchantCodesDatagrid.BottomPagerRow.Visible = True
            If blnEmptyList Then
                For Each gvRow As GridViewRow In Me.moMerchantCodesDatagrid.Rows
                    'gvRow.Visible = False
                    gvRow.Controls.Clear()
                Next
            End If
            Session("recCount") = Me.State.MerchantCodeSearchDV.Count

            'If Me.moMerchantCodesDatagrid.Visible Then
            '    If (Me.State.AddingMerchantCodeNewRow) Then
            '        Me.lblRecordCount.Text = (Me.State.MerchantCodeSearchDV.Count - 1) & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            '    Else
            '        Me.lblRecordCount.Text = Me.State.MerchantCodeSearchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            '    End If
            'End If
            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Me.moMerchantCodesDatagrid)
        End Sub

        Private Sub AddNew()
            Me.State.MerchantCodeSearchDV = GetMerchantCodeDV()
            Me.State.PreviousMerchantCodeSearchDV = Me.State.MerchantCodeSearchDV

            Me.State.MyMerchantCodeBO = New MerchantCode
            Me.State.MerchantCodeID = Me.State.MyMerchantCodeBO.Id

            Me.State.MerchantCodeSearchDV = Me.State.MyMerchantCodeBO.GetNewDataViewRow(Me.State.MerchantCodeSearchDV, Me.State.MerchantCodeID, Me.State.MyMerchantCodeBO)
            'Me.State.searchDV = Me.State.searchDV.AddNewRowToEmptyDV
            Me.moMerchantCodesDatagrid.DataSource = Me.State.MerchantCodeSearchDV

            Me.SetPageAndSelectedIndexFromGuid(Me.State.MerchantCodeSearchDV, Me.State.MerchantCodeID, Me.moMerchantCodesDatagrid, Me.State.PageIndex, Me.State.IsMerchantCodeEditMode)

            Me.moMerchantCodesDatagrid.AutoGenerateColumns = False

            SortAndBindGrid(Me.State.MerchantCodeSearchDV)

            SetGridControls(Me.moMerchantCodesDatagrid, False)

            'DEF-3066
            Me.State.MerchantCodeAddNew = True
            'DEF-3066

            PopulateMerchantCodeFormFromBO()
        End Sub

        Private Sub ReturnMerchantCodeFromEditing()

            Me.moMerchantCodesDatagrid.EditIndex = NO_ROW_SELECTED_INDEX

            If Me.moMerchantCodesDatagrid.PageCount = 0 Then
                'if returning to the "1st time in" screen
                ControlMgr.SetVisibleControl(Me, Me.moMerchantCodesDatagrid, False)
            Else
                ControlMgr.SetVisibleControl(Me, Me.moMerchantCodesDatagrid, True)
            End If

            SetGridControls(Me.moMerchantCodesDatagrid, True)
            Me.State.IsMerchantCodeEditMode = False
            Me.PopulateMyMerchantCodeGrid()
            Me.State.PageIndex = Me.moMerchantCodesDatagrid.PageIndex
            SetMerchantCodeButtonsState(False)

        End Sub

        Private Sub PopulateBOFromForm()
            Try
                Dim cboCompanyCreditCard As DropDownList = CType(Me.moMerchantCodesDatagrid.Rows(Me.moMerchantCodesDatagrid.EditIndex).Cells(Me.COMPANY_CREDIT_CARD_ID).FindControl(Me.COMPANY_CREDIT_CARD_CONTROL_NAME), DropDownList)
                Dim CreditCardFormatId As Guid = Me.GetSelectedItem(cboCompanyCreditCard)
                If Not CreditCardFormatId.Equals(Guid.Empty) Then
                    Dim objCompanyCreditCard As CompanyCreditCard = New CompanyCreditCard(CreditCardFormatId, Me.State.MyBO.CompanyId)
                    Me.State.MyMerchantCodeBO.CompanyCreditCardId = objCompanyCreditCard.Id
                Else
                    Me.State.MyMerchantCodeBO.CompanyCreditCardId = Nothing
                End If



                Dim txtMerchantCode As TextBox = CType(Me.moMerchantCodesDatagrid.Rows(Me.moMerchantCodesDatagrid.EditIndex).Cells(Me.MERCHANT_CODE).FindControl(Me.MERCHANT_CODE_TEXTBOX_CONTROL_NAME), TextBox)


                Me.PopulateBOProperty(Me.State.MyMerchantCodeBO, "DealerId", Me.State.MyBO.Id)
                Me.PopulateBOProperty(Me.State.MyMerchantCodeBO, "MerchantCode", txtMerchantCode)

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub
#End Region
#Region "MerchantCodeHandlers_buttons"

        Private Sub BtnNewMerchantCode_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewMerchantCode_WRITE.Click
            Try
                Me.State.IsMerchantCodeEditMode = True
                Me.State.MerchantCodeSearchDV = Nothing
                Me.State.PreviousMerchantCodeSearchDV = Nothing
                AddNew()
                SetMerchantCodeButtonsState(True)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub CancelRecord()
            Try
                IsNewMerchantCode = True
                SetGridControls(Me.moMerchantCodesDatagrid, True)
                If (IsNewMerchantCode) Then
                    Me.State.MerchantCodeSearchDV = Nothing
                    Me.State.PreviousMerchantCodeSearchDV = Nothing
                End If
                ReturnMerchantCodeFromEditing()
                IsNewMerchantCode = False
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
        Private Sub SaveRecord()
            Try
                PopulateBOFromForm()
                If (Me.State.MyMerchantCodeBO.IsDirty) Then
                    Me.State.MyMerchantCodeBO.Save()
                    Me.State.IsMerchantCodeAfterSave = True
                    Me.State.AddingMerchantCodeNewRow = False
                    'Me.AddInfoMsg(Me.MSG_RECORD_SAVED_OK)
                    Me.State.MyMerchantCodeBO.EndEdit()
                    Me.State.IsEditMode = False
                    ' Me.State.IsMerchantCodeNew = False
                    'Me.State.AddingNewRow = False
                    Me.State.Action = ""
                    Me.State.MerchantCodeSearchDV = Nothing
                    Me.State.PreviousMerchantCodeSearchDV = Nothing
                    Me.ReturnMerchantCodeFromEditing()
                Else
                    Me.AddInfoMsg(Me.MSG_RECORD_NOT_SAVED)
                    Me.ReturnMerchantCodeFromEditing()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
        Private Sub DeleteDealer()
            Try
                Me.State.MyBO.DeleteAndSave()
                Me.State.HasDataChanged = True
                Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Delete, Me.State.MyBO, Me.State.HasDataChanged))
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub DoDelete()

            Me.State.MyMerchantCodeBO = New MerchantCode(Me.State.MerchantCodeID)
            Try
                Me.State.MyMerchantCodeBO.Delete()
                Me.State.MyMerchantCodeBO.Save()
                Me.State.MyMerchantCodeBO.EndEdit()
                ' Me.State.MyMerchantCodeBO.Id = Guid.Empty
                Me.State.MyMerchantCodeBO = Nothing
                Me.State.MerchantCodeSearchDV = Nothing

            Catch ex As Exception
                Me.State.MyBO.RejectChanges()
                Throw ex
            End Try

            ReturnMerchantCodeFromEditing()

            'Set the IsAfterSave flag to TRUE so that the Paging logic gets invoked
            Me.State.IsEditMode = False
        End Sub

        Protected Sub CheckIfComingFromDeleteConfirm()
            Dim confResponse As String = Me.HiddenDeletePromptResponse.Value
            Dim confResponseDel As String = Me.HiddenDelDeletePromptResponse.Value
            If Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_YES Then
                If Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete Then
                    DoDelete()
                    'Clean after consuming the action
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                    Me.HiddenDeletePromptResponse.Value = ""
                End If
            ElseIf Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_NO Then
                'Me.State.searchDV = Nothing
                ReturnMerchantCodeFromEditing()
                'Clean after consuming the action
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                Me.HiddenDeletePromptResponse.Value = ""
            End If
            If Not confResponseDel Is Nothing AndAlso confResponseDel = Me.MSG_VALUE_YES Then
                If Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete Then
                    Me.DeleteDealer()
                    'Clean after consuming the action
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                    Me.HiddenDelDeletePromptResponse.Value = ""
                End If
            ElseIf Not confResponseDel Is Nothing AndAlso confResponseDel = Me.MSG_VALUE_NO Then
                'Clean after consuming the action
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                Me.HiddenDelDeletePromptResponse.Value = ""
            End If
        End Sub
#End Region

#Region "Attach_Detach_buttons"

        Private Sub UserControlAvailableSelectedClaimTypes_Attach(ByVal aSrc As Generic.UserControlAvailableSelected_New, ByVal attachedList As System.Collections.ArrayList) Handles UserControlAvailableSelectedClaimTypes.Attach
            Try
                If attachedList.Count > 0 Then
                    Me.State.MyBO.AttachClaimType(attachedList)
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub UserControlAvailableSelectedClaimTypes_Detach(ByVal aSrc As Generic.UserControlAvailableSelected_New, ByVal detachedList As System.Collections.ArrayList) Handles UserControlAvailableSelectedClaimTypes.Detach
            Try
                If detachedList.Count > 0 Then
                    Me.State.MyBO.DetachClaimType(detachedList)
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub UserControlAvailableSelectedCoverageTypes_Attach(ByVal aSrc As Generic.UserControlAvailableSelected_New, ByVal attachedList As System.Collections.ArrayList) Handles UserControlAvailableSelectedCoverageTypes.Attach
            Try
                If attachedList.Count > 0 Then
                    Me.State.MyBO.AttachCoverageType(attachedList)
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub UserControlAvailableSelectedCoverageTypes_Detach(ByVal aSrc As Generic.UserControlAvailableSelected_New, ByVal detachedList As System.Collections.ArrayList) Handles UserControlAvailableSelectedCoverageTypes.Detach
            Try
                If detachedList.Count > 0 Then
                    Me.State.MyBO.DetachCoverageType(detachedList)
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

#End Region
#Region "REQ-5598 - ClaimCloseRules"
        Private Sub ClaimCloseRules_RequestClaimCloseRulesData(ByVal sender As Object, ByRef e As UserControlClaimCloseRules.RequestDataEventArgs) Handles ClaimCloseRules.RequestClaimCloseRulesData
            Dim claimCloseRules As New ClaimCloseRules
            claimCloseRules.CompanyId = Me.State.Ocompany.Id
            claimCloseRules.DealerId = Me.State.MyBO.Id
            e.Data = claimCloseRules.GetClaimCloseRules()
        End Sub

#End Region
#Region "Dealer Inflation and Risk Type Tolerance Delegate Handler"
        Private Sub DealerInflation_RequestDealerInflationData(ByVal sender As Object, ByRef e As UserControlDealerInflation.RequestDataEventArgs) Handles DealerInflation.RequestDealerInflationData
            Dim dlInflation As New DealerInflation
            dlInflation.DealerId = Me.State.MyBO.Id
            e.Data = dlInflation.GetDealerInflation()
        End Sub
        Private Sub RiskTypeTolerance_RequestRiskTypeTolerance(ByVal sender As Object, ByRef e As UserControlRiskTypeTolerance.RequestDataEventArgs) Handles RiskTypeTolerance.RequestRiskTypeToleranceData
            Dim riskTolerance As New RiskTypeTolerance
            riskTolerance.DealerId = Me.State.MyBO.Id
            e.Data = riskTolerance.GetRiskTypeTolerance()
        End Sub

#End Region

#Region "Ajax Autocomplete"
        <System.Web.Services.WebMethod(), Script.Services.ScriptMethod()>
        Public Shared Function PopulateSalvageCenterDrop(ByVal prefixText As String, ByVal count As Integer) As String()
            Dim dv As DataView = LookupListNew.GetServiceCenterLookupList(ElitaPlusIdentity.Current.ActiveUser.Countries())
            Return AjaxController.BindAutoComplete(prefixText, dv)
        End Function


#End Region
        Private Sub DealerForm_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
            hdnDisabledTab.Value = String.Join(",", DisabledTabsList)
        End Sub
    End Class
End Namespace
