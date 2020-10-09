Imports Assurant.ElitaPlus.DALObjects
Imports System.Linq
Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms

Partial Class AccountingSettingForm
    Inherits ElitaPlusPage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents moSuppAna5CHECK As System.Web.UI.WebControls.CheckBox
    'Protected WithEvents lblBranchID As System.Web.UI.WebControls.Label
    Protected WithEvents moIsNewBranchLabel As System.Web.UI.WebControls.Label
    Public Shared moCompId As Guid = Guid.Empty
    ' Public Shared moObjectType As ReturnType.TargetType
    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "CONSTANTS"
    Public Enum ObjectStatus
        isNew
        isACopy
        isExisting
    End Enum

    Public Shared URL As String = "AccountingSettingForm.aspx"

    '  Public Shared strAccountName As String

    Private Const NOTHING_SELECTED As String = "00000000-0000-0000-0000-000000000000"

    Public Const ACCOUNTCODE_PROPERTY As String = "AccountCode"
    Public Const ACCOUNTTYPE_PROPERTY As String = "AccountType"
    Public Const CONVERSIONCODECONTROL_PROPERTY As String = "ConversionCodeControl"
    Public Const BALANCETYPE_PROPERTY As String = "BalanceType"
    Public Const ACCTCOMPANYID_PROPERTY As String = "AcctCompanyId"
    Public Const REPORTCONVERSIONCONTROL_PROPERTY As String = "ReportConversionControl"
    Public Const DATAACCESSGROUPCODE_PROPERTY As String = "DataAccessGroupCode"
    Public Const DEFAULTCURRENCYCODE_PROPERTY As String = "DefaultCurrencyCode"
    Public Const ACCOUNTSTATUS_PROPERTY As String = "AccountStatus"
    Public Const SUPPRESSREVALUATION_PROPERTY As String = "SuppressRevaluation"
    Public Const PAYASPAIDACCOUNTTYPE_PROPERTY As String = "PayAsPaidAccountType"
    Public Const ADDRESSLOOKUPCODE_PROPERTY As String = "AddressLookupCode"
    Public Const ADDRESSSEQUENCENUMBER_PROPERTY As String = "AddressSequenceNumber"
    Public Const ADDRESSSTATUS_PROPERTY As String = "AddressStatus"
    Public Const SUPPLIERLOOKUPCODE_PROPERTY As String = "SupplierLookupCode"
    Public Const PAYMENTMETHOD_PROPERTY As String = "PaymentMethod"
    Public Const SUPPLIERSTATUS_PROPERTY As String = "SupplierStatus"
    Public Const ACCOUNTANALYSIS1_PROPERTY As String = "AccountAnalysisCode1"
    Public Const ACCOUNTANALYSIS2_PROPERTY As String = "AccountAnalysisCode2"
    Public Const ACCOUNTANALYSIS3_PROPERTY As String = "AccountAnalysisCode3"
    Public Const ACCOUNTANALYSIS4_PROPERTY As String = "AccountAnalysisCode4"
    Public Const ACCOUNTANALYSIS5_PROPERTY As String = "AccountAnalysisCode5"
    Public Const ACCOUNTANALYSIS6_PROPERTY As String = "AccountAnalysisCode6"
    Public Const ACCOUNTANALYSIS7_PROPERTY As String = "AccountAnalysisCode7"
    Public Const ACCOUNTANALYSIS8_PROPERTY As String = "AccountAnalysisCode8"
    Public Const ACCOUNTANALYSIS9_PROPERTY As String = "AccountAnalysisCode9"
    Public Const ACCOUNTANALYSIS10_PROPERTY As String = "AccountAnalysisCode10"
    Public Const ACCOUNTANALYSIS_A_1_PROPERTY As String = "AccountAnalysisACode1"
    Public Const ACCOUNTANALYSIS_A_2_PROPERTY As String = "AccountAnalysisACode2"
    Public Const ACCOUNTANALYSIS_A_3_PROPERTY As String = "AccountAnalysisACode3"
    Public Const ACCOUNTANALYSIS_A_4_PROPERTY As String = "AccountAnalysisACode4"
    Public Const ACCOUNTANALYSIS_A_5_PROPERTY As String = "AccountAnalysisACode5"
    Public Const ACCOUNTANALYSIS_A_6_PROPERTY As String = "AccountAnalysisACode6"
    Public Const ACCOUNTANALYSIS_A_7_PROPERTY As String = "AccountAnalysisACode7"
    Public Const ACCOUNTANALYSIS_A_8_PROPERTY As String = "AccountAnalysisACode8"
    Public Const ACCOUNTANALYSIS_A_9_PROPERTY As String = "AccountAnalysisACode9"
    Public Const ACCOUNTANALYSIS_A_10_PROPERTY As String = "AccountAnalysisACode10"
    Public Const SUPPLIERANALYSIS1_PROPERTY As String = "SupplierAnalysisCode1"
    Public Const SUPPLIERANALYSIS2_PROPERTY As String = "SupplierAnalysisCode2"
    Public Const SUPPLIERANALYSIS3_PROPERTY As String = "SupplierAnalysisCode3"
    Public Const SUPPLIERANALYSIS4_PROPERTY As String = "SupplierAnalysisCode4"
    Public Const SUPPLIERANALYSIS5_PROPERTY As String = "SupplierAnalysisCode5"
    Public Const SUPPLIERANALYSIS6_PROPERTY As String = "SupplierAnalysisCode6"
    Public Const SUPPLIERANALYSIS7_PROPERTY As String = "SupplierAnalysisCode7"
    Public Const SUPPLIERANALYSIS8_PROPERTY As String = "SupplierAnalysisCode8"
    Public Const SUPPLIERANALYSIS9_PROPERTY As String = "SupplierAnalysisCode9"
    Public Const SUPPLIERANALYSIS10_PROPERTY As String = "SupplierAnalysisCode10"
    Public Const ACCOUNT_ANALYSIS_FLAG1_PROPERTY As String = "AccountAnalysis1"
    Public Const ACCOUNT_ANALYSIS_FLAG2_PROPERTY As String = "AccountAnalysis2"
    Public Const ACCOUNT_ANALYSIS_FLAG3_PROPERTY As String = "AccountAnalysis3"
    Public Const ACCOUNT_ANALYSIS_FLAG4_PROPERTY As String = "AccountAnalysis4"
    Public Const ACCOUNT_ANALYSIS_FLAG5_PROPERTY As String = "AccountAnalysis5"
    Public Const ACCOUNT_ANALYSIS_FLAG6_PROPERTY As String = "AccountAnalysis6"
    Public Const ACCOUNT_ANALYSIS_FLAG7_PROPERTY As String = "AccountAnalysis7"
    Public Const ACCOUNT_ANALYSIS_FLAG8_PROPERTY As String = "AccountAnalysis8"
    Public Const ACCOUNT_ANALYSIS_FLAG9_PROPERTY As String = "AccountAnalysis9"
    Public Const ACCOUNT_ANALYSIS_FLAG10_PROPERTY As String = "AccountAnalysis10"
    Public Const DEALER_PROPERTY As String = "DealerId"
    Public Const DEALER_GROUP_PROPERTY As String = "DealerGroupId"
    Public Const SERVICE_CENTER_PROPERTY As String = "ServiceCenterId"
    Public Const BRANCH_ID_PROPERTY As String = "BranchId"
    Public Const COMMISSION_ENTITY_ID_PROPERTY As String = "CommissionEntityId"

    Public Const PAYMENT_TERMS_ID_PROPERTY As String = "PaymentTermsId"
    Public Const USER_AREA_PROPERTY As String = "UserArea"
    Public Const DESCRIPTION_PROPERTY As String = "Description"
    Public Const DEFAULT_BANK_SUBCODE_PROPERTY As String = "DefaultBankSubCode"

    Private Const ACCTSETT_LIST_FORM001 As String = "ACCTSETT_LIST_FORM001" ' Maintain Accounting Settings List Exception
    Public Const TYPE_DEALER As String = "Dealer"
    Public Const TYPE_SERVICE_CENTER As String = "ServiceCenter"
    Public Const TYPE_BRANCH_CENTER As String = "Branch"

    Public Const AS_DIRTY_COLUMNS_COUNT As Integer = 3

    Public Const ACCT_ANALYSIS_PROHIBIT As String = "3"
    Public Const ACCT_ANALYSIS_MANDATORY As String = "1"
    Public Const ACCT_ANALYSIS_OPTIONAL As String = "2"

    Private Const CODE_FIELD As String = "CODE"

    Private ACCT_SYSTEM As String

    'Form Title
    Public Const PAGETITLE As String = "ACCOUNT_SETTINGS_FORM"
    Public Const PAGETAB As String = "TABLES"

    Public Const ACCOUNT_TYPE_PAYABLE As String = "1"
    Public Const ACCOUNT_TYPE_RECEIVABLE As String = "0"
#End Region

#Region "PAGE STATE"

    Class MyState
        Public MyBO_R As AcctSetting
        Public MyBO_P As AcctSetting
        Public MyBO_BankInfoId As Guid = Guid.Empty
        Public MyBO_BankCountryId As Guid = Guid.Empty

        Public ScreenSnapShotBO_R As AcctSetting
        Public ScreenSnapShotBO_P As AcctSetting
        Public ObjStatus_R As ObjectStatus
        Public ObjStatus_P As ObjectStatus
        Public accountName As String
        Public branchName As String
        Public IsNewBOPDirty As Boolean = False
        Public IsNewBORDirty As Boolean = False

        Public BranchDealerId As Guid
        Public TargetId As Guid
        Public TargetStatus As ObjectStatus

        Public LastErrMsg As String
        Public HasDataChanged As Boolean
        Public ActionInProgress As ElitaPlusPage.DetailPageCommand = DetailPageCommand.Nothing_
        Public objectType As ReturnType.TargetType
        Public OrigAcctSettingsId As Guid

 

        Public Function GetOrigObj() As AcctSetting
            If MyBO_P.Id = OrigAcctSettingsId Then
                Return MyBO_P
            ElseIf MyBO_R.Id = OrigAcctSettingsId Then
                Return MyBO_R
            Else
                Return Nothing
            End If
        End Function
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

#Region "RETURN TYPE"
    Public Class ReturnType
        Public Id As Guid
        Public ObjectType As TargetType
        Public ObjectId As Guid
        Public HasDataChanged As Boolean = False
        Public ObjectTitle As String
        Public addParam As String
        Public ObjectStatus As Boolean
        Public EditingBo As AcctSetting
        Public LastOperation As DetailPageCommand
        Public Sub New(ObjectType As TargetType, hasDataChanged As Boolean)
            Me.ObjectType = ObjectType
            Me.HasDataChanged = hasDataChanged
        End Sub

        Public Sub New(Id As Guid, ObjectType As TargetType, ObjectId As Guid, _
                    ObjectTitle As String, hasDataChanged As Boolean, _
                    Optional ByVal otherParam As String = "")
            Me.Id = Id
            Me.ObjectType = ObjectType
            Me.ObjectId = ObjectId
            Me.HasDataChanged = hasDataChanged
            Me.ObjectTitle = ObjectTitle
            addParam = otherParam
        End Sub
        Public Sub New(LastOp As DetailPageCommand, curEditingBo As AcctSetting, hasDataChanged As Boolean)
            LastOperation = LastOp
            EditingBo = curEditingBo
            Me.HasDataChanged = hasDataChanged
        End Sub

        Public Enum TargetType
            Dealer
            ServiceCenter
            Branch
            DealerGroup            
            Other
            CommissionEntity
        End Enum
    End Class
#End Region

#Region "Handlers"

#Region "PAGE EVENTS"

    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim Params As ReturnType
        ErrControllerMaster.Clear_Hide()
        Try
            If Not IsPostBack Then

                SetFormTab(PAGETAB)
                SetFormTitle(PAGETITLE)


                AddConfirmation(btnDelete_WRITE, Message.DELETE_RECORD_PROMPT)
                If State.MyBO_R Is Nothing OrElse State.MyBO_P Is Nothing Then
                    CreateNew()
                Else
                    PopulateAll()
                    PopulateFormFromBOs()
                    SetDefaults()
                    'bind bank user control
                    If State.MyBO_BankInfoId <> Guid.Empty Then
                        moBankInfo.Bind(New BankInfo(State.MyBO_BankInfoId), ErrControllerMaster)
                        moBankInfo.State.myBankInfoBo.SourceCountryID = State.MyBO_BankCountryId
                    Else
                        moBankInfo.Bind(New BankInfo(), ErrControllerMaster)
                        moBankInfo.State.myBankInfoBo.SourceCountryID = State.MyBO_BankCountryId
                        State.MyBO_BankInfoId = moBankInfo.State.myBankInfoBo.Id
                    End If
                End If
                MenuEnabled = False
                ExecuteEvents()

                TabContainer1.Tabs(0).HeaderText = TranslationBase.TranslateLabelOrMessage("ACCOUNT_RECEIVABLES")
                TabContainer1.Tabs(1).HeaderText = TranslationBase.TranslateLabelOrMessage("ACCOUNT_PAYABLES")
                TabContainer1.Tabs(2).HeaderText = TranslationBase.TranslateLabelOrMessage("BANK_INFO")
                SetTargetControls()
            End If
            SetVisibleAttributes()
            BindBoPropertiesToLabels()
            CheckIfComingFromSaveConfirm()
            If Not IsPostBack Then
                AddLabelDecorations(State.MyBO_R)
                AddLabelDecorations(State.MyBO_P)
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
        ShowMissingTranslations(ErrControllerMaster)
    End Sub

    Private Sub Page_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete
        Try
            Select Case State.objectType
                Case ReturnType.TargetType.Branch ' Branch
                    If ddlTargetist.Visible Then ddlTargetist.AutoPostBack = True
            End Select
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Page_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall
        Try
            If CallingParameters IsNot Nothing Then
                Dim Params As ReturnType
                Params = CType(CallingParameters, ReturnType)
                State.objectType = Params.ObjectType
                State.OrigAcctSettingsId = CType(Params.Id, Guid)
                If Not (State.OrigAcctSettingsId.Equals(Guid.Empty)) Then
                    LoadObjects(State.OrigAcctSettingsId)
                    State.accountName = Server.HtmlDecode(Params.ObjectTitle)
                    State.branchName = Server.HtmlDecode(Params.addParam)
                End If
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try

    End Sub

 #End Region

#Region "Button Clicks"
    Private Sub btnSave_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnSave_WRITE.Click
        Try
            Dim blnChanged As Boolean = False
            Dim selectedCompId As Guid = Guid.Empty
            If cboCompany.Visible = False Then
                If moCompId.Equals(Guid.Empty) Then
                    'no account companies found for the user
                    ErrControllerMaster.AddErrorAndShow(ElitaPlus.Common.ErrorCodes.GUI_ACCOUNTING_COMPANIES_NOT_FOUND_ERR)
                    Exit Sub
                End If
            Else
                selectedCompId = GetSelectedItem(cboCompany)
                If selectedCompId.Equals(Guid.Empty) Then
                    ElitaPlusPage.SetLabelError(moCompanyLABEL)
                    ErrControllerMaster.AddErrorAndShow(ElitaPlus.Common.ErrorCodes.GUI_SELECT_ACCOUNTING_COMPANY_FROM_ACC_RECEIVABLE_TAB_ERR)
                    TabContainer1.ActiveTabIndex = 0
                    Exit Sub
                End If
            End If

            If cboCompany_P.Visible = False Then
                If moCompId.Equals(Guid.Empty) Then
                    'no account companies found for the user
                    ErrControllerMaster.AddErrorAndShow(ElitaPlus.Common.ErrorCodes.GUI_ACCOUNTING_COMPANIES_NOT_FOUND_ERR)
                    Exit Sub
                End If
            Else
                selectedCompId = GetSelectedItem(cboCompany_P)
                If selectedCompId.Equals(Guid.Empty) Then
                    ElitaPlusPage.SetLabelError(moCompanyLABEL)
                    ErrControllerMaster.AddErrorAndShow(ElitaPlus.Common.ErrorCodes.GUI_SELECT_ACCOUNTING_COMPANY_FROM_ACC_PAYABLE_TAB_ERR)
                    TabContainer1.ActiveTabIndex = 1
                    Exit Sub
                End If
            End If

            If Me.State.TargetStatus = ObjectStatus.isNew OrElse Me.State.TargetStatus = ObjectStatus.isACopy Then
                Dim guidSelectedId As Guid
                Select Case State.objectType
                    Case ReturnType.TargetType.DealerGroup
                        guidSelectedId = GetSelectedItem(ddlTargetist)
                        If guidSelectedId.Equals(Guid.Empty) Then
                            ElitaPlusPage.SetLabelError(lblTargetName)
                            Throw New GUIException(Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_GROUP_MUST_BE_SELECTED_ERR, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_GROUP_MUST_BE_SELECTED_ERR)
                            Exit Sub
                        End If
                    Case ReturnType.TargetType.Dealer
                        guidSelectedId = GetSelectedItem(ddlTargetist)
                        If guidSelectedId.Equals(Guid.Empty) Then
                            ElitaPlusPage.SetLabelError(lblTargetName)
                            Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
                            Exit Sub
                        End If
                    Case ReturnType.TargetType.ServiceCenter
                        guidSelectedId = GetSelectedItem(ddlTargetist)
                        If guidSelectedId.Equals(Guid.Empty) Then
                            ElitaPlusPage.SetLabelError(lblTargetName)
                            Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_SERVICE_CENTER_MUST_BE_SELECTED_ERR)
                            Exit Sub
                        End If
                    Case ReturnType.TargetType.Branch
                        guidSelectedId = GetSelectedItem(ddlTargetist)
                        If guidSelectedId.Equals(Guid.Empty) Then
                            ElitaPlusPage.SetLabelError(lblTargetName)
                            Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
                            Exit Sub
                        End If
                        Dim selectedBranchId As Guid = GetSelectedItem(moBranchNameDROP)
                        If selectedBranchId.Equals(Guid.Empty) Then
                            ElitaPlusPage.SetLabelError(moBranchNameLABEL)
                            Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_BRANCH_MUST_BE_SELECTED_ERR)
                            Exit Sub
                        End If
                    Case ReturnType.TargetType.CommissionEntity
                        guidSelectedId = GetSelectedItem(ddlTargetist)
                        If guidSelectedId.Equals(Guid.Empty) Then
                            ElitaPlusPage.SetLabelError(lblTargetName)
                            Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_COMMISSION_ENTITY_MUST_BE_SELECTED_ERR)
                            Exit Sub
                        End If
                End Select
            End If
            SetLabelColor(lblTargetName)
            SetLabelColor(moBranchNameLABEL)
            SetLabelColor(moCompanyLABEL)
            PopulateBOsFromForm()

            blnChanged = SaveObjects()

            If blnChanged Then
                State.HasDataChanged = True
                SetVisibleAttributes()
                EnableDisableFields()
                'Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                AddInfoMsgWithDelay(Message.SAVE_RECORD_CONFIRMATION)
                If ddlTargetist.Items.Count > 0 Then SetSelectedItem(ddlTargetist, NOTHING_SELECTED)
                State.TargetStatus = ObjectStatus.isExisting
                SetVisibleAttributes()
                PopulateFormFromBOs()
            Else
                'Me.AddInfoMsg(Message.MSG_RECORD_NOT_SAVED)
                AddInfoMsgWithDelay(Message.MSG_RECORD_NOT_SAVED)
            End If

        Catch ex As Exception
            If ex.GetType Is GetType(BOValidationException) Then
                TabContainer1.ActiveTabIndex = 0
            End If
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Private Sub btnDelete_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnDelete_WRITE.Click
        Try
            If Not State.MyBO_R.IsNew Then State.MyBO_R.DeleteAndSave()
            If Not State.MyBO_P.IsNew Then State.MyBO_P.DeleteAndSave()
            'delete bank info record if not service center
            If State.objectType <> ReturnType.TargetType.ServiceCenter AndAlso State.objectType <> ReturnType.TargetType.CommissionEntity Then
                UpdateObjWithBankInfoId(Guid.Empty)
                With moBankInfo.State.myBankInfoBo
                    If (Not .IsNew) AndAlso (Not .IsDeleted) Then
                        .Delete()
                        .Save()
                    End If
                End With
            End If

            State.HasDataChanged = True
            ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Delete, Nothing, State.HasDataChanged))
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Private Sub btnBack_Click(sender As System.Object, e As System.EventArgs) Handles btnBack.Click
        Try
            PopulateBOsFromForm()
            If IsBODirty() Then
                DisplayMessageWithDelay(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
            Else
                ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.GetOrigObj, State.HasDataChanged))
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
            DisplayMessageWithDelay(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
            State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
            State.LastErrMsg = ErrControllerMaster.Text
        End Try
    End Sub

    Private Sub btnNew_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnNew_WRITE.Click
        Try
            PopulateBOsFromForm()
            If (IsBODirty()) Then
                DisplayMessageWithDelay(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
            Else
                State.TargetStatus = ObjectStatus.isNew
                CreateNew()
                TabContainer1.ActiveTab = TabPanel1
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Private Sub btnCopy_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnCopy_WRITE.Click
        Try
            PopulateBOsFromForm()
            If (IsBODirty()) Then
                DisplayMessageWithDelay(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
            Else
                State.TargetStatus = ObjectStatus.isACopy
                CreateNewWithCopy()
                TabContainer1.ActiveTab = TabPanel1
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Private Sub btnUndo_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnUndo_WRITE.Click
        Try
            If State.TargetStatus = ObjectStatus.isExisting Then
                If State.ObjStatus_R = ObjectStatus.isExisting Then
                    LoadObjects(State.MyBO_R.Id, True)
                ElseIf True Then
                    LoadObjects(State.MyBO_P.Id, True)
                End If
            ElseIf State.ScreenSnapShotBO_R IsNot Nothing Then
                State.MyBO_R.Clone(State.ScreenSnapShotBO_R)
                State.MyBO_P.Clone(State.ScreenSnapShotBO_P)
                If State.TargetId <> Guid.Empty Then
                    PopulateBankInfo(State.TargetId, True)
                End If
            End If
                PopulateAll()
                PopulateFormFromBOs()
                EnableDisableFields()
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

#End Region

#Region "Handler Dropdown"

    Protected Sub moDealerNameDROP_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles ddlTargetist.SelectedIndexChanged
        Try
            Select Case State.objectType
                Case ReturnType.TargetType.Branch                    
                    PopulateBranches()
                Case Else
                    State.TargetId = GetSelectedItem(ddlTargetist)
                    State.accountName = GetSelectedDescription(ddlTargetist)
                    PopulateBankInfo(State.TargetId)
            End Select

        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Protected Sub moBranchNameDROP_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles moBranchNameDROP.SelectedIndexChanged
        Try
            State.TargetId = GetSelectedItem(moBranchNameDROP)
            State.branchName = GetSelectedDescription(moBranchNameDROP)
            PopulateBankInfo(State.TargetId)
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

#End Region

#End Region

#Region "Client Attributes"

    Private Sub ExecuteEvents()
        btnSave_WRITE.Attributes.Add("onclick", "return validate();")
       
    End Sub

    Private Sub SetVisibleAttributes()
        Dim isNewOrCopy As Boolean = (Me.State.TargetStatus = ObjectStatus.isNew OrElse Me.State.TargetStatus = ObjectStatus.isACopy)
        Dim moreIds As New ArrayList
        Dim sJavaScript As New System.Text.StringBuilder

        sJavaScript.Append("<SCRIPT language='JavaScript' >" & Environment.NewLine)
        sJavaScript.Append("function toggleBranch(){" & Environment.NewLine)
        moreIds.Add("branchName_tr")
        ControlMgr.SetVisibleControl(Me, moAccountNameTEXT, Not isNewOrCopy)
        ControlMgr.SetVisibleControl(Me, ddlTargetist, isNewOrCopy)

        Select Case State.objectType
            Case ReturnType.TargetType.Branch
                sJavaScript.Append(ControlMgr.AddMoreClientVisibleIds(moreIds, "''"))
                ControlMgr.SetVisibleControl(Me, moBranchNameTEXT, Not isNewOrCopy)
                ControlMgr.SetVisibleControl(Me, moBranchNameDROP, isNewOrCopy)
            Case Else
                sJavaScript.Append(ControlMgr.AddMoreClientVisibleIds(moreIds, "'none'"))
        End Select
        sJavaScript.Append("}" & Environment.NewLine)
        sJavaScript.Append("toggleBranch();")
        sJavaScript.Append("</script>" & Environment.NewLine)
        RegisterStartupScript("ToggleBranchScript", sJavaScript.ToString)

        'Add script for Felita / SmartStream fields
        If ElitaPlusIdentity.Current.ActiveUser.AccountingCompanies.Count > 0 Then
            ACCT_SYSTEM = LookupListNew.GetCodeFromId(LookupListNew.DropdownLookupList(LookupListNew.LK_ACCT_SYSTEM, ElitaPlusIdentity.Current.ActiveUser.LanguageId), ElitaPlusIdentity.Current.ActiveUser.AccountingCompanies(0).AcctSystemId)
            litScriptVars.Text += String.Format("var ACCT_SYSTEM_ID = '{0}';SetAcctSystem();", ACCT_SYSTEM)
        End If

        If Not ACCT_SYSTEM.Equals(FelitaEngine.FELITA_PREFIX) Then
            cboAcctAnal1.Enabled = False
            cboAcctAnal1_P.Enabled = False
            cboAcctAnal2.Enabled = False
            cboAcctAnal2_P.Enabled = False
            cboAcctAnal3.Enabled = False
            cboAcctAnal3_P.Enabled = False
            cboAcctAnal4.Enabled = False
            cboAcctAnal4_P.Enabled = False
            cboAcctAnal5.Enabled = False
            cboAcctAnal5_P.Enabled = False
            cboAcctAnal6.Enabled = False
            cboAcctAnal6_P.Enabled = False
            cboAcctAnal7.Enabled = False
            cboAcctAnal7_P.Enabled = False
            cboAcctAnal8.Enabled = False
            cboAcctAnal8_P.Enabled = False
            cboAcctAnal9.Enabled = False
            cboAcctAnal9_P.Enabled = False
            cboAcctAnal10.Enabled = False
            cboAcctAnal10_P.Enabled = False
        End If

    End Sub

    Protected Sub EnableDisableFields()
        'Enabled by Default
        ControlMgr.SetEnableControl(Me, btnDelete_WRITE, True)
        ControlMgr.SetEnableControl(Me, btnNew_WRITE, True)
        ControlMgr.SetEnableControl(Me, btnCopy_WRITE, True)

        If Me.State.TargetStatus = ObjectStatus.isNew OrElse State.TargetStatus = ObjectStatus.isACopy Then
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, False)
        End If
    End Sub

#End Region

#Region "CONTROLLING LOGIC"
    

    Public Function IsBODirty() As Boolean
        Dim blnDirty As Boolean = (State.IsNewBOPDirty OrElse State.IsNewBORDirty OrElse moBankInfo.State.IsBODirty)
        With State
            If (Not blnDirty) AndAlso (.MyBO_R IsNot Nothing) AndAlso (Not .MyBO_R.IsNew) Then
                If .MyBO_R.IsDirty Then blnDirty = True
            End If
            If (Not blnDirty) AndAlso (.MyBO_P IsNot Nothing) AndAlso (Not .MyBO_P.IsNew) Then
                If .MyBO_P.IsDirty Then blnDirty = True
            End If
        End With
        Return blnDirty
    End Function

    Private Function SaveObjects() As Boolean
        Dim blnChanged_R As Boolean = False
        Dim blnChanged_P As Boolean = False
        Dim blnChanged_B As Boolean = False

        With State.MyBO_R
            If (.IsNew AndAlso State.IsNewBORDirty) OrElse ((Not .IsNew) AndAlso .IsDirty) Then
                .Validate()
                blnChanged_R = True
            End If
        End With

        With State.MyBO_P
            If (.IsNew AndAlso State.IsNewBOPDirty) OrElse ((Not .IsNew) AndAlso .IsDirty) Then
                .Validate()
                blnChanged_P = True
            End If
        End With

        With moBankInfo.State
            If .IsBODirty Then
                .myBankInfoBo.Validate()
                blnChanged_B = True
            End If
        End With

        'Save the changes after validation
        If blnChanged_R Then
            State.MyBO_R.Save()
            State.IsNewBORDirty = False
            State.TargetStatus = ObjectStatus.isExisting
        End If

        If blnChanged_P Then
            State.MyBO_P.Save()
            State.IsNewBOPDirty = False
            State.TargetStatus = ObjectStatus.isExisting
        End If

        If blnChanged_B Then
            moBankInfo.State.myBankInfoBo.Save()
            moBankInfo.State.IsNewObjDirty = False
            UpdateObjWithBankInfoId(moBankInfo.State.myBankInfoBo.Id)
        End If

        Return (blnChanged_R OrElse blnChanged_P OrElse blnChanged_B)
    End Function

    Public Function ValidateIDXAcctSettingAndCode(accountCompanyId As Guid, accountCode As String)
        Try
            Return AcctSetting.IsIDXAcctSettingAndCode(accountCompanyId, accountCode)
        Catch ex As Exception

        End Try
    End Function

    Private Sub UpdateObjWithBankInfoId(guidBankId As Guid)
        Select Case State.objectType
            Case ReturnType.TargetType.DealerGroup
                Dim objDG As New DealerGroup(State.TargetId)
                If objDG.BankInfoId <> guidBankId Then
                    objDG.BankInfoId = guidBankId
                    objDG.Save()
                End If
            Case ReturnType.TargetType.Dealer
                Dim objDealer As New Dealer(State.TargetId)
                If objDealer.BankInfoId <> guidBankId Then
                    objDealer.BankInfoId = guidBankId
                    objDealer.Save()
                End If
            Case ReturnType.TargetType.ServiceCenter
                Dim objSC As New ServiceCenter(State.TargetId)
                If objSC.BankInfoId <> guidBankId Then
                    objSC.BankInfoId = guidBankId
                    objSC.Save()
                End If
            Case ReturnType.TargetType.Branch
                Dim objBranch As New Branch(State.TargetId)
                If objBranch.BankInfoId <> guidBankId Then
                    objBranch.BankInfoId = guidBankId
                    objBranch.Save()
                End If
            Case ReturnType.TargetType.CommissionEntity
                Dim objCE As New CommissionEntity(State.TargetId)
                If objCE.BankInfoId <> guidBankId Then
                    objCE.BankInfoId = guidBankId
                    objCE.Save()
                End If
        End Select
    End Sub
    Private Sub SetTargetControls()
        Select Case State.objectType
            Case ReturnType.TargetType.DealerGroup
                lblTargetName.Text = TranslationBase.TranslateLabelOrMessage("DEALER_GROUP_NAME")
            Case ReturnType.TargetType.Dealer
                lblTargetName.Text = TranslationBase.TranslateLabelOrMessage("Dealer_Name")
            Case ReturnType.TargetType.ServiceCenter
                lblTargetName.Text = TranslationBase.TranslateLabelOrMessage("Service_Center_Name")
            Case ReturnType.TargetType.Branch
                'lblTargetName.Text = TranslationBase.TranslateLabelOrMessage("Branch_Name")
                lblTargetName.Text = TranslationBase.TranslateLabelOrMessage("Dealer_Name")
            Case ReturnType.TargetType.CommissionEntity
                lblTargetName.Text = TranslationBase.TranslateLabelOrMessage("COMMISSION_ENTITY")
        End Select
    End Sub

    Private Sub LoadObjects(guidID As Guid, Optional ByVal blnBindBankInfo As Boolean = False)
        Dim objID As Guid
        If guidID = Guid.Empty Then 'New
            State.TargetStatus = ObjectStatus.isNew
            State.MyBO_R = New AcctSetting()
            With State.MyBO_R
                .AccountType = ACCOUNT_TYPE_RECEIVABLE
            End With
            State.ObjStatus_R = ObjectStatus.isNew
            State.MyBO_P = New AcctSetting()
            With State.MyBO_P
                .AccountType = ACCOUNT_TYPE_PAYABLE
            End With
            State.ObjStatus_P = ObjectStatus.isNew
        Else 'Existing
            State.TargetStatus = ObjectStatus.isExisting
            State.MyBO_R = New AcctSetting(guidID)
            objID = State.MyBO_R.GetCounterPartObjectID
            If State.MyBO_R.AccountType = ACCOUNT_TYPE_PAYABLE Then
                State.MyBO_P = State.MyBO_R
                State.ObjStatus_P = ObjectStatus.isExisting
                If objID <> Guid.Empty Then
                    State.MyBO_R = New AcctSetting(objID)
                    State.ObjStatus_R = ObjectStatus.isExisting
                Else
                    State.MyBO_R = New AcctSetting()
                    With State.MyBO_R
                        .DealerGroupId = State.MyBO_P.DealerGroupId
                        .DealerId = State.MyBO_P.DealerId
                        .ServiceCenterId = State.MyBO_P.ServiceCenterId
                        .BranchId = State.MyBO_P.BranchId
                        .CommissionEntityId = State.MyBO_P.CommissionEntityId
                        .AccountType = ACCOUNT_TYPE_RECEIVABLE
                    End With
                    State.ObjStatus_R = ObjectStatus.isNew
                End If
            Else
                State.ObjStatus_R = ObjectStatus.isExisting
                If objID <> Guid.Empty Then
                    State.MyBO_P = New AcctSetting(objID)
                    State.ObjStatus_P = ObjectStatus.isExisting
                Else
                    State.MyBO_P = New AcctSetting()
                    With State.MyBO_P
                        .DealerGroupId = State.MyBO_R.DealerGroupId
                        .DealerId = State.MyBO_R.DealerId
                        .ServiceCenterId = State.MyBO_R.ServiceCenterId
                        .BranchId = State.MyBO_R.BranchId
                        .CommissionEntityId = State.MyBO_R.CommissionEntityId
                        .AccountType = ACCOUNT_TYPE_PAYABLE
                    End With
                    State.ObjStatus_P = ObjectStatus.isNew
                End If
            End If
            objID = Guid.Empty
            With State.MyBO_R
                Select Case State.objectType
                    Case ReturnType.TargetType.DealerGroup
                        objID = .DealerGroupId
                    Case ReturnType.TargetType.Dealer
                        objID = .DealerId
                    Case ReturnType.TargetType.ServiceCenter
                        objID = .ServiceCenterId
                    Case ReturnType.TargetType.Branch
                        objID = .BranchId
                    Case ReturnType.TargetType.CommissionEntity
                        objID = .CommissionEntityId
                End Select
                State.TargetId = objID
                PopulateBankInfo(objID, blnBindBankInfo)
            End With
        End If
    End Sub
    Protected Sub CreateNewWithCopy()
        PopulateBOsFromForm()

        State.TargetStatus = ObjectStatus.isACopy
        State.TargetId = Guid.Empty

        Dim newObj As New AcctSetting
        If Not State.MyBO_R.IsNew Then
            newObj.Copy(State.MyBO_R)
            State.MyBO_R = newObj
            With State.MyBO_R
                .DealerGroupId = Nothing
                .DealerId = Nothing
                .ServiceCenterId = Nothing
                .BranchId = Nothing
                .CommissionEntityId = Nothing
                .AccountCode = ""
            End With
            State.ObjStatus_R = ObjectStatus.isACopy
            State.IsNewBORDirty = True
        Else
            State.MyBO_R = newObj
            State.MyBO_R.AccountType = ACCOUNT_TYPE_RECEIVABLE
            State.ObjStatus_R = ObjectStatus.isNew
        End If


        newObj = New AcctSetting
        If Not State.MyBO_P.IsNew Then
            newObj.Copy(State.MyBO_P)
            State.MyBO_P = newObj
            With State.MyBO_P
                .DealerGroupId = Nothing
                .DealerId = Nothing
                .ServiceCenterId = Nothing
                .BranchId = Nothing
                .CommissionEntityId = Nothing
                .AccountCode = ""
            End With
            State.ObjStatus_P = ObjectStatus.isACopy
            State.IsNewBOPDirty = True
        Else
            State.MyBO_P = newObj
            State.MyBO_P.AccountType = ACCOUNT_TYPE_PAYABLE
            State.ObjStatus_P = ObjectStatus.isNew
        End If

        PopulateAll()
        PopulateFormFromBOs()
        EnableDisableFields()

        'create the backup copy
        State.ScreenSnapShotBO_R = New AcctSetting
        State.ScreenSnapShotBO_R.Clone(State.MyBO_R)
        State.ScreenSnapShotBO_P = New AcctSetting
        State.ScreenSnapShotBO_P.Clone(State.MyBO_P)

        SetVisibleAttributes()
        SetDefaults()
    End Sub

    Protected Sub CreateNew()
        State.TargetStatus = ObjectStatus.isNew
        State.TargetId = Guid.Empty

        'Reset the backup copy
        State.ScreenSnapShotBO_R = Nothing
        State.ScreenSnapShotBO_P = Nothing
        State.MyBO_R = New AcctSetting
        State.MyBO_R.AccountType = ACCOUNT_TYPE_RECEIVABLE
        State.MyBO_P = New AcctSetting
        State.MyBO_P.AccountType = ACCOUNT_TYPE_PAYABLE
        State.ObjStatus_R = ObjectStatus.isNew
        State.ObjStatus_P = ObjectStatus.isNew

        EnableDisableFields()

        PopulateAll()
        PopulateFormFromBOs()
        SetVisibleAttributes()
        SetDefaults()
    End Sub

    Protected Sub CheckIfComingFromSaveConfirm()
        Dim confResponse As String = HiddenSaveChangesPromptResponse.Value
        Dim objRet As AcctSetting = State.MyBO_R
        If State.MyBO_P.Id = State.OrigAcctSettingsId Then
            objRet = State.MyBO_P
        End If

        If confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_YES Then
            If State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr Then
                Try
                    SaveObjects()
                Catch ex As Exception
                    HandleErrors(ex, ErrControllerMaster)
                End Try
            End If
            Select Case State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, objRet, State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.New_
                    'Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                    AddInfoMsgWithDelay(Message.SAVE_RECORD_CONFIRMATION)
                    CreateNew()
                Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                    'Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                    AddInfoMsgWithDelay(Message.SAVE_RECORD_CONFIRMATION)
                    CreateNewWithCopy()
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    ReturnToCallingPage(New ReturnType(State.ActionInProgress, objRet, State.HasDataChanged))
            End Select
        ElseIf confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_NO Then
            Select Case State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, objRet, State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.New_
                    CreateNew()
                Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                    CreateNewWithCopy()
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    ErrControllerMaster.AddErrorAndShow(State.LastErrMsg)
            End Select
        End If
        'Clean after consuming the action
        State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
        HiddenSaveChangesPromptResponse.Value = String.Empty
    End Sub
#End Region

#Region "POPULATE"
    Private Sub PopulateBankInfo(objID As Guid, Optional ByVal blnBind As Boolean = True)
        Dim BankInfoId As Guid, countryID As Guid
        Select Case State.objectType
            Case ReturnType.TargetType.DealerGroup
                Dim objDG As New DealerGroup(objID)
                BankInfoId = objDG.BankInfoId
                State.MyBO_BankCountryId = ElitaPlusIdentity.Current.ActiveUser.Company.CountryId
            Case ReturnType.TargetType.Dealer
                Dim objDealer As Dealer = New Dealer(objID)
                BankInfoId = objDealer.BankInfoId
                State.MyBO_BankCountryId = New Company(objDealer.CompanyId).CountryId
            Case ReturnType.TargetType.ServiceCenter
                Dim objSC As ServiceCenter = New ServiceCenter(objID)
                BankInfoId = objSC.BankInfoId
                State.MyBO_BankCountryId = objSC.CountryId
            Case ReturnType.TargetType.Branch
                Dim objBR As New Branch(objID)
                BankInfoId = objBR.BankInfoId
                State.MyBO_BankCountryId = objBR.CountryId
            Case ReturnType.TargetType.CommissionEntity
                Dim objCE As New CommissionEntity(objID)
                BankInfoId = objCE.BankInfoId
                State.MyBO_BankCountryId = objCE.CountryId
        End Select
        If BankInfoId <> Guid.Empty Then
            If blnBind Then
                moBankInfo.Bind(New BankInfo(BankInfoId), ErrControllerMaster)
                moBankInfo.State.myBankInfoBo.SourceCountryID = State.MyBO_BankCountryId
            End If
            State.MyBO_BankInfoId = BankInfoId
        Else
            If blnBind Then
                moBankInfo.Bind(New BankInfo(), ErrControllerMaster)
                moBankInfo.State.myBankInfoBo.SourceCountryID = State.MyBO_BankCountryId
                State.MyBO_BankInfoId = moBankInfo.State.myBankInfoBo.Id
            End If
        End If
    End Sub
    Private Sub PopulateTargetList()
        Dim dv As DataView
        Select Case State.objectType
            Case ReturnType.TargetType.DealerGroup
                'dv = AcctSetting.GetDealerGroupForAcctSetting()
                'Me.BindListControlToDataView(ddlTargetist, dv, AcctSetting.DealerGroupAcctSettingsDV.DEALER_GROUP_NAME, AcctSetting.DealerGroupAcctSettingsDV.DEALER_GROUP_ID)
                Dim listcontext1 As ListContext = New ListContext()
                listcontext1.CompanyGroupId = Authentication.CompanyGroupId
                Dim List1 As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="DealerGroupAccountingSettingByCompanyGroup", languageCode:=ElitaPlusIdentity.Current.ActiveUser.LanguageCode, context:=listcontext1)
                ddlTargetist.Populate(List1.ToArray(), New PopulateOptions() With
                    {
                        .AddBlankItem = True
                    })
            Case ReturnType.TargetType.Dealer
                'dv = AcctSetting.GetDealersForAcctSetting(ElitaPlusIdentity.Current.ActiveUser.Companies)
                'Me.BindListControlToDataView(ddlTargetist, dv, AcctSetting.DealerAcctSettingsDV.DEALER_NAME, AcctSetting.DealerAcctSettingsDV.DEALER_ID)
                Dim listcontext2 As ListContext = New ListContext()
                Dim List2 As New Collections.Generic.List(Of Assurant.Elita.CommonConfiguration.DataElements.ListItem)
                For Each _company As Guid In Authentication.CompIds
                    listcontext2.CompanyId = _company
                    Dim List21 As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="DealerAccountingSettingByCompany", context:=listcontext2)
                    If List21.Count > 0 Then
                        If List2 IsNot Nothing Then
                            List2.AddRange(List21)
                        Else
                            List2 = List21.Clone()
                        End If
                    End If
                Next
                ddlTargetist.Populate(List2.ToArray(), New PopulateOptions() With
                    {
                        .AddBlankItem = True
                    })
            Case ReturnType.TargetType.ServiceCenter
                'dv = AcctSetting.GetServiceCentersForAcctSetting(ElitaPlusIdentity.Current.ActiveUser.Countries)
                'Me.BindListControlToDataView(ddlTargetist, dv, AcctSetting.ServiceCenterAcctSettingsDV.DESCRIPTION, AcctSetting.ServiceCenterAcctSettingsDV.SERVICE_CENTER_ID)
                Dim listcontext3 As ListContext = New ListContext()
                Dim List3 As New Collections.Generic.List(Of Assurant.Elita.CommonConfiguration.DataElements.ListItem)
                For Each _country As Guid In Authentication.CountryIds
                    listcontext3.CountryId = _country
                    Dim List31 As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="ServiceCenterAccountingSettingByCountry", context:=listcontext3)
                    If List31.Count > 0 Then
                        If List3 IsNot Nothing Then
                            List3.AddRange(List31)
                        Else
                            List3 = List31.Clone()
                        End If
                    End If
                Next
                ddlTargetist.Populate(List3.ToArray(), New PopulateOptions() With
                    {
                        .AddBlankItem = True
                    })
            Case ReturnType.TargetType.Branch
                'dv = AcctSetting.GetDealersForBranch(Authentication.CompIds)
                'Me.BindListControlToDataView(ddlTargetist, dv, AcctSetting.DealerAcctSettingsDV.DEALER_NAME, AcctSetting.DealerAcctSettingsDV.DEALER_ID)
                Dim listcontext4 As ListContext = New ListContext()
                Dim List4 As New Collections.Generic.List(Of Assurant.Elita.CommonConfiguration.DataElements.ListItem)
                For Each _company As Guid In Authentication.CompIds
                    listcontext4.CompanyId = _company
                    Dim List41 As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="DealerForBranchByCompany", context:=listcontext4)
                    If List41.Count > 0 Then
                        If List4 IsNot Nothing Then
                            List4.AddRange(List41)
                        Else
                            List4 = List41.Clone()
                        End If
                    End If
                Next
                ddlTargetist.Populate(List4.ToArray(), New PopulateOptions() With
                    {
                        .AddBlankItem = True
                    })
            Case ReturnType.TargetType.CommissionEntity
                'dv = AcctSetting.GetCommissionEntityListForAcctSetting()
                'Me.BindListControlToDataView(ddlTargetist, dv, AcctSetting.CommissionEntityAcctSettingsDV.ENTITY_NAME, AcctSetting.CommissionEntityAcctSettingsDV.ENTITY_ID)
                Dim listcontext5 As ListContext = New ListContext()
                listcontext5.CompanyGroupId = Authentication.CompanyGroupId
                Dim List5 As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="CommisionEntityForAccountingSettingByCompanyGroup", languageCode:=ElitaPlusIdentity.Current.ActiveUser.LanguageCode, context:=listcontext5)
                ddlTargetist.Populate(List5.ToArray(), New PopulateOptions() With
                    {
                        .AddBlankItem = True
                    })

        End Select
    End Sub

    Private Sub PopulateBranches()
        'Dim dv As DataView
        Dim oDealerId As Guid = GetSelectedItem(ddlTargetist) 'GetSelectedItem(Me.moDealerNameDROP)
        State.BranchDealerId = oDealerId

        If oDealerId.Equals(Guid.Empty) Then
            ElitaPlusPage.SetLabelError(lblTargetName)
            Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
        End If

        'dv = AcctSetting.GetBranchesForAcctSetting(oDealerId)
        'Me.BindListControlToDataView(moBranchNameDROP, dv, BranchDAL.COL_NAME_BRANCH_NAME, BranchDAL.COL_NAME_BRANCH_ID)

        Dim ListContext As New Assurant.Elita.CommonConfiguration.ListContext
        ListContext.DealerId = oDealerId
        Dim BranchListForDealer As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="DealerBranchWithoutAccountingSetting", context:=ListContext)
        moBranchNameDROP.Populate(BranchListForDealer, New PopulateOptions() With
                                           {
                                            .AddBlankItem = True
                                           })

        ControlMgr.SetEnableControl(Me, moBranchNameDROP, True)
    End Sub

    Private Sub CleanBranchesDrop()
        ClearList(moBranchNameDROP)
    End Sub

    Private Sub PopulateAcctCompanies()
        Dim oAcct As New AcctSetting
        'Dim dv As DataView
        'dv = oAcct.GetAccountingCompanies(ElitaPlusIdentity.Current.ActiveUser.Companies)

        Dim AcctCompanyList As DataElements.ListItem() =
                                    CommonConfigManager.Current.ListManager.GetList(listCode:="AcctCompany",
                                                                languageCode:=ElitaPlusIdentity.Current.ActiveUser.LanguageCode)

        Dim UserAcctCompanyList As New Collections.Generic.List(Of Assurant.Elita.CommonConfiguration.DataElements.ListItem)
        For Each _acctCompany As AcctCompany In ElitaPlusIdentity.Current.ActiveUser.AccountingCompanies
            If UserAcctCompanyList IsNot Nothing Then
                UserAcctCompanyList.AddRange(From lst In AcctCompanyList
                                             Where lst.ListItemId = _acctCompany.Id
                                             Select lst)
            Else
                UserAcctCompanyList = (From lst In AcctCompanyList
                                       Where lst.ListItemId = _acctCompany.Id
                                       Select lst).ToArray().Clone()
            End If
        Next

        If UserAcctCompanyList.Count <= 1 Then
            If UserAcctCompanyList.Count = 1 Then
                moCompId = UserAcctCompanyList.FirstOrDefault().ListItemId
            End If
            ControlMgr.SetVisibleControl(Me, cboCompany, False)
            ControlMgr.SetVisibleControl(Me, txtCompany, True)
            txtCompany.Text = UserAcctCompanyList.FirstOrDefault().Translation
            ControlMgr.SetVisibleControl(Me, cboCompany_P, False)
            ControlMgr.SetVisibleControl(Me, txtCompany_P, True)
            txtCompany_P.Text = UserAcctCompanyList.FirstOrDefault().Translation
        Else
            cboCompany.Populate(UserAcctCompanyList.ToArray(),
                                New PopulateOptions() With
                                {
                                    .AddBlankItem = True
                                })
            cboCompany_P.Populate(UserAcctCompanyList.ToArray(),
                                New PopulateOptions() With
                                {
                                    .AddBlankItem = True
                                })
            ControlMgr.SetVisibleControl(Me, cboCompany, True)
            ControlMgr.SetVisibleControl(Me, txtCompany, False)
            ControlMgr.SetVisibleControl(Me, cboCompany_P, True)
            ControlMgr.SetVisibleControl(Me, txtCompany_P, False)
        End If

        'If dv.Count <= 1 Then
        '    If dv.Count = 1 Then
        '        moCompId = New Guid(CType(dv(0)(oAcct.AcctCompanyDV.ACCT_COMPANY_ID), Byte()))
        '    End If
        '    ControlMgr.SetVisibleControl(Me, cboCompany, False)
        '    ControlMgr.SetVisibleControl(Me, txtCompany, True)
        '    txtCompany.Text = dv(0)(oAcct.AcctCompanyDV.DESCRIPTION).ToString
        '    ControlMgr.SetVisibleControl(Me, cboCompany_P, False)
        '    ControlMgr.SetVisibleControl(Me, txtCompany_P, True)
        '    txtCompany_P.Text = dv(0)(oAcct.AcctCompanyDV.DESCRIPTION).ToString
        'Else
        '    Me.BindListControlToDataView(cboCompany, dv, oAcct.AcctCompanyDV.DESCRIPTION, oAcct.AcctCompanyDV.ACCT_COMPANY_ID)
        '    Me.BindListControlToDataView(cboCompany_P, dv, oAcct.AcctCompanyDV.DESCRIPTION, oAcct.AcctCompanyDV.ACCT_COMPANY_ID)
        '    ControlMgr.SetVisibleControl(Me, cboCompany, True)
        '    ControlMgr.SetVisibleControl(Me, txtCompany, False)
        '    ControlMgr.SetVisibleControl(Me, cboCompany_P, True)
        '    ControlMgr.SetVisibleControl(Me, txtCompany_P, False)
        'End If
    End Sub

    Protected Sub BindBoPropertiesToLabels()

        BindBOPropertyToLabel(State.MyBO_R, ACCOUNTCODE_PROPERTY, moAccountCodeLABEL)
        BindBOPropertyToLabel(State.MyBO_R, ACCTCOMPANYID_PROPERTY, moCompanyLABEL)
        BindBOPropertyToLabel(State.MyBO_R, BALANCETYPE_PROPERTY, moBalanceTypeLABEL)
        BindBOPropertyToLabel(State.MyBO_R, CONVERSIONCODECONTROL_PROPERTY, moConversionCodeControlLABEL)
        BindBOPropertyToLabel(State.MyBO_R, REPORTCONVERSIONCONTROL_PROPERTY, moReportConversionControlLABEL)
        BindBOPropertyToLabel(State.MyBO_R, DATAACCESSGROUPCODE_PROPERTY, moDataAccessGroupCodeLABEL)
        BindBOPropertyToLabel(State.MyBO_R, DEFAULTCURRENCYCODE_PROPERTY, moDefaultCurrencyCodeLABEL)
        BindBOPropertyToLabel(State.MyBO_R, SUPPRESSREVALUATION_PROPERTY, moSuppressReevaluationLABEL)
        BindBOPropertyToLabel(State.MyBO_R, PAYASPAIDACCOUNTTYPE_PROPERTY, moPayAsPaidAccountTypeLABEL)
        BindBOPropertyToLabel(State.MyBO_R, SUPPLIERLOOKUPCODE_PROPERTY, moSupplierLookUpCodeLABEL)
        BindBOPropertyToLabel(State.MyBO_R, PAYMENTMETHOD_PROPERTY, moPaymentMethodLABEL)
        BindBOPropertyToLabel(State.MyBO_R, SUPPLIERSTATUS_PROPERTY, moSupplierStatusLABEL)
        BindBOPropertyToLabel(State.MyBO_R, ACCOUNTANALYSIS_A_1_PROPERTY, moAcctAnal1LABEL)
        BindBOPropertyToLabel(State.MyBO_R, ACCOUNTANALYSIS_A_2_PROPERTY, moAcctAnal2LABEL)
        BindBOPropertyToLabel(State.MyBO_R, ACCOUNTANALYSIS_A_3_PROPERTY, moAcctAnal3LABEL)
        BindBOPropertyToLabel(State.MyBO_R, ACCOUNTANALYSIS_A_4_PROPERTY, moAcctAnal4LABEL)
        BindBOPropertyToLabel(State.MyBO_R, ACCOUNTANALYSIS_A_5_PROPERTY, moAcctAnal5LABEL)
        BindBOPropertyToLabel(State.MyBO_R, ACCOUNTANALYSIS_A_6_PROPERTY, moAcctAnal6LABEL)
        BindBOPropertyToLabel(State.MyBO_R, ACCOUNTANALYSIS_A_7_PROPERTY, moAcctAnal7LABEL)
        BindBOPropertyToLabel(State.MyBO_R, ACCOUNTANALYSIS_A_8_PROPERTY, moAcctAnal8LABEL)
        BindBOPropertyToLabel(State.MyBO_R, ACCOUNTANALYSIS_A_9_PROPERTY, moAcctAnal9LABEL)
        BindBOPropertyToLabel(State.MyBO_R, ACCOUNTANALYSIS_A_10_PROPERTY, moAcctAnal10LABEL)
        BindBOPropertyToLabel(State.MyBO_R, SUPPLIERANALYSIS1_PROPERTY, moSuppAnal1LABEL)
        BindBOPropertyToLabel(State.MyBO_R, SUPPLIERANALYSIS2_PROPERTY, moSuppAnal2LABEL)
        BindBOPropertyToLabel(State.MyBO_R, SUPPLIERANALYSIS3_PROPERTY, moSuppAnal3LABEL)
        BindBOPropertyToLabel(State.MyBO_R, SUPPLIERANALYSIS4_PROPERTY, moSuppAnal4LABEL)
        BindBOPropertyToLabel(State.MyBO_R, SUPPLIERANALYSIS5_PROPERTY, moSuppAnal5LABEL)
        BindBOPropertyToLabel(State.MyBO_R, SUPPLIERANALYSIS6_PROPERTY, moSuppAnal6LABEL)
        BindBOPropertyToLabel(State.MyBO_R, SUPPLIERANALYSIS7_PROPERTY, moSuppAnal7LABEL)
        BindBOPropertyToLabel(State.MyBO_R, SUPPLIERANALYSIS8_PROPERTY, moSuppAnal8LABEL)
        BindBOPropertyToLabel(State.MyBO_R, SUPPLIERANALYSIS9_PROPERTY, moSuppAnal9LABEL)
        BindBOPropertyToLabel(State.MyBO_R, SUPPLIERANALYSIS10_PROPERTY, moSuppAnal10LABEL)
        BindBOPropertyToLabel(State.MyBO_R, ADDRESSLOOKUPCODE_PROPERTY, moAddressLookupCodeLABEL)
        BindBOPropertyToLabel(State.MyBO_R, ADDRESSSEQUENCENUMBER_PROPERTY, moAddressSequenceNumberLABEL)
        BindBOPropertyToLabel(State.MyBO_R, ADDRESSSTATUS_PROPERTY, moAddressStatusLABEL)
        BindBOPropertyToLabel(State.MyBO_R, ACCOUNTANALYSIS1_PROPERTY, moAcctAnalT1LABEL)
        BindBOPropertyToLabel(State.MyBO_R, ACCOUNTANALYSIS2_PROPERTY, moAcctAnalT2LABEL)
        BindBOPropertyToLabel(State.MyBO_R, ACCOUNTANALYSIS3_PROPERTY, moAcctAnalT3LABEL)
        BindBOPropertyToLabel(State.MyBO_R, ACCOUNTANALYSIS4_PROPERTY, moAcctAnalT4LABEL)
        BindBOPropertyToLabel(State.MyBO_R, ACCOUNTANALYSIS5_PROPERTY, moAcctAnalT5LABEL)
        BindBOPropertyToLabel(State.MyBO_R, ACCOUNTANALYSIS6_PROPERTY, moAcctAnalT6LABEL)
        BindBOPropertyToLabel(State.MyBO_R, ACCOUNTANALYSIS7_PROPERTY, moAcctAnalT7LABEL)
        BindBOPropertyToLabel(State.MyBO_R, ACCOUNTANALYSIS8_PROPERTY, moAcctAnalT8LABEL)
        BindBOPropertyToLabel(State.MyBO_R, ACCOUNTANALYSIS9_PROPERTY, moAcctAnalT9LABEL)
        BindBOPropertyToLabel(State.MyBO_R, ACCOUNTANALYSIS10_PROPERTY, moAcctAnalT10LABEL)
        BindBOPropertyToLabel(State.MyBO_R, PAYMENT_TERMS_ID_PROPERTY, moPaymentTermsLABEL)
        BindBOPropertyToLabel(State.MyBO_R, USER_AREA_PROPERTY, moUserAreaLABEL)
        BindBOPropertyToLabel(State.MyBO_R, DESCRIPTION_PROPERTY, moDescriptionLABEL)

        'Account payable controls
        BindBOPropertyToLabel(State.MyBO_P, ACCOUNTCODE_PROPERTY, moAccountCodeLABEL_P)
        BindBOPropertyToLabel(State.MyBO_P, ACCTCOMPANYID_PROPERTY, moCompanyLABEL_P)
        BindBOPropertyToLabel(State.MyBO_P, BALANCETYPE_PROPERTY, moBalanceTypeLABEL_P)
        BindBOPropertyToLabel(State.MyBO_P, CONVERSIONCODECONTROL_PROPERTY, moConversionCodeControlLABEL_P)
        BindBOPropertyToLabel(State.MyBO_P, REPORTCONVERSIONCONTROL_PROPERTY, moReportConversionControlLABEL_P)
        BindBOPropertyToLabel(State.MyBO_P, DATAACCESSGROUPCODE_PROPERTY, moDataAccessGroupCodeLABEL_P)
        BindBOPropertyToLabel(State.MyBO_P, DEFAULTCURRENCYCODE_PROPERTY, moDefaultCurrencyCodeLABEL_P)
        BindBOPropertyToLabel(State.MyBO_P, SUPPRESSREVALUATION_PROPERTY, moSuppressReevaluationLABEL_P)
        BindBOPropertyToLabel(State.MyBO_P, PAYASPAIDACCOUNTTYPE_PROPERTY, moPayAsPaidAccountTypeLABEL_P)
        BindBOPropertyToLabel(State.MyBO_P, SUPPLIERLOOKUPCODE_PROPERTY, moSupplierLookUpCodeLABEL_P)
        BindBOPropertyToLabel(State.MyBO_P, PAYMENTMETHOD_PROPERTY, moPaymentMethodLABEL_P)
        BindBOPropertyToLabel(State.MyBO_P, SUPPLIERSTATUS_PROPERTY, moSupplierStatusLABEL_P)
        BindBOPropertyToLabel(State.MyBO_P, ACCOUNTANALYSIS_A_1_PROPERTY, moAcctAnal1LABEL_P)
        BindBOPropertyToLabel(State.MyBO_P, ACCOUNTANALYSIS_A_2_PROPERTY, moAcctAnal2LABEL_P)
        BindBOPropertyToLabel(State.MyBO_P, ACCOUNTANALYSIS_A_3_PROPERTY, moAcctAnal3LABEL_P)
        BindBOPropertyToLabel(State.MyBO_P, ACCOUNTANALYSIS_A_4_PROPERTY, moAcctAnal4LABEL_P)
        BindBOPropertyToLabel(State.MyBO_P, ACCOUNTANALYSIS_A_5_PROPERTY, moAcctAnal5LABEL_P)
        BindBOPropertyToLabel(State.MyBO_P, ACCOUNTANALYSIS_A_6_PROPERTY, moAcctAnal6LABEL_P)
        BindBOPropertyToLabel(State.MyBO_P, ACCOUNTANALYSIS_A_7_PROPERTY, moAcctAnal7LABEL_P)
        BindBOPropertyToLabel(State.MyBO_P, ACCOUNTANALYSIS_A_8_PROPERTY, moAcctAnal8LABEL_P)
        BindBOPropertyToLabel(State.MyBO_P, ACCOUNTANALYSIS_A_9_PROPERTY, moAcctAnal9LABEL_P)
        BindBOPropertyToLabel(State.MyBO_P, ACCOUNTANALYSIS_A_10_PROPERTY, moAcctAnal10LABEL_P)
        BindBOPropertyToLabel(State.MyBO_P, SUPPLIERANALYSIS1_PROPERTY, moSuppAnal1LABEL_P)
        BindBOPropertyToLabel(State.MyBO_P, SUPPLIERANALYSIS2_PROPERTY, moSuppAnal2LABEL_P)
        BindBOPropertyToLabel(State.MyBO_P, SUPPLIERANALYSIS3_PROPERTY, moSuppAnal3LABEL_P)
        BindBOPropertyToLabel(State.MyBO_P, SUPPLIERANALYSIS4_PROPERTY, moSuppAnal4LABEL_P)
        BindBOPropertyToLabel(State.MyBO_P, SUPPLIERANALYSIS5_PROPERTY, moSuppAnal5LABEL_P)
        BindBOPropertyToLabel(State.MyBO_P, SUPPLIERANALYSIS6_PROPERTY, moSuppAnal6LABEL_P)
        BindBOPropertyToLabel(State.MyBO_P, SUPPLIERANALYSIS7_PROPERTY, moSuppAnal7LABEL_P)
        BindBOPropertyToLabel(State.MyBO_P, SUPPLIERANALYSIS8_PROPERTY, moSuppAnal8LABEL_P)
        BindBOPropertyToLabel(State.MyBO_P, SUPPLIERANALYSIS9_PROPERTY, moSuppAnal9LABEL_P)
        BindBOPropertyToLabel(State.MyBO_P, SUPPLIERANALYSIS10_PROPERTY, moSuppAnal10LABEL_P)
        BindBOPropertyToLabel(State.MyBO_P, ADDRESSLOOKUPCODE_PROPERTY, moAddressLookupCodeLABEL_P)
        BindBOPropertyToLabel(State.MyBO_P, ADDRESSSEQUENCENUMBER_PROPERTY, moAddressSequenceNumberLABEL_P)
        BindBOPropertyToLabel(State.MyBO_P, ADDRESSSTATUS_PROPERTY, moAddressStatusLABEL_P)

        BindBOPropertyToLabel(State.MyBO_P, ACCOUNTANALYSIS1_PROPERTY, moAcctAnalT1LABEL_P)
        BindBOPropertyToLabel(State.MyBO_P, ACCOUNTANALYSIS2_PROPERTY, moAcctAnalT2LABEL_P)
        BindBOPropertyToLabel(State.MyBO_P, ACCOUNTANALYSIS3_PROPERTY, moAcctAnalT3LABEL_P)
        BindBOPropertyToLabel(State.MyBO_P, ACCOUNTANALYSIS4_PROPERTY, moAcctAnalT4LABEL_P)
        BindBOPropertyToLabel(State.MyBO_P, ACCOUNTANALYSIS5_PROPERTY, moAcctAnalT5LABEL_P)
        BindBOPropertyToLabel(State.MyBO_P, ACCOUNTANALYSIS6_PROPERTY, moAcctAnalT6LABEL_P)
        BindBOPropertyToLabel(State.MyBO_P, ACCOUNTANALYSIS7_PROPERTY, moAcctAnalT7LABEL_P)
        BindBOPropertyToLabel(State.MyBO_P, ACCOUNTANALYSIS8_PROPERTY, moAcctAnalT8LABEL_P)
        BindBOPropertyToLabel(State.MyBO_P, ACCOUNTANALYSIS9_PROPERTY, moAcctAnalT9LABEL_P)
        BindBOPropertyToLabel(State.MyBO_P, ACCOUNTANALYSIS10_PROPERTY, moAcctAnalT10LABEL_P)
        BindBOPropertyToLabel(State.MyBO_P, PAYMENT_TERMS_ID_PROPERTY, moPaymentTermsLABEL_P)
        BindBOPropertyToLabel(State.MyBO_P, USER_AREA_PROPERTY, moUserAreaLABEL_P)
        BindBOPropertyToLabel(State.MyBO_P, DESCRIPTION_PROPERTY, moDescriptionLABEL_P)
        BindBOPropertyToLabel(State.MyBO_P, DEFAULT_BANK_SUBCODE_PROPERTY, moDefaultBankSubCodeLABEL_P)


        Select Case State.objectType
            Case ReturnType.TargetType.DealerGroup
                BindBOPropertyToLabel(State.MyBO_R, "DealerGroupId", lblTargetName)
            Case ReturnType.TargetType.Dealer
                BindBOPropertyToLabel(State.MyBO_R, "DealerId", lblTargetName)
            Case ReturnType.TargetType.ServiceCenter
                BindBOPropertyToLabel(State.MyBO_R, "ServiceCenterId", lblTargetName)
            Case ReturnType.TargetType.Branch
                BindBOPropertyToLabel(State.MyBO_R, "BranchId", lblTargetName)
            Case ReturnType.TargetType.CommissionEntity
                BindBOPropertyToLabel(State.MyBO_R, "CommissionEntityId", lblTargetName)
        End Select

        ClearGridHeadersAndLabelsErrSign()

    End Sub

    Protected Sub PopulateFormFromBOs()
        'Fill lists
        Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
        Dim AnalCodedv As DataView = LookupListNew.DropdownLookupList(LookupListNew.LK_ANYLSCODE, langId, True)
        Dim vendStdv As DataView = LookupListNew.DropdownLookupList(LookupListNew.LK_VENSTAT, langId, True)
        moAccountNameTEXT.Text = State.accountName
        Select Case State.objectType
            Case ReturnType.TargetType.Branch ' Branch
                moBranchNameTEXT.Text = State.branchName
        End Select

        With State.MyBO_R
            PopulateControlFromBOProperty(moAccountCodeTEXT, .AccountCode)
            PopulateControlFromBOProperty(moBalanceTypeTEXT, .BalanceType)
            PopulateControlFromBOProperty(moDataAccessGroupCodeTEXT, .DataAccessGroupCode)
            PopulateControlFromBOProperty(moDefaultCurrencyCodeTEXT, .DefaultCurrencyCode)
            PopulateControlFromBOProperty(moSuppressReevaluationTEXT, .SuppressRevaluation)
            PopulateControlFromBOProperty(moAddressLookupCodeTEXT, .AddressLookupCode)
            PopulateControlFromBOProperty(moAddressSequenceNumberTEXT, .AddressSequenceNumber)
            PopulateControlFromBOProperty(moSupplierLookUpCodeTEXT, .SupplierLookupCode)

            PopulateControlFromBOProperty(moAcctAnal1TEXT, .AccountAnalysisACode1)
            PopulateControlFromBOProperty(moAcctAnal2TEXT, .AccountAnalysisACode2)
            PopulateControlFromBOProperty(moAcctAnal3TEXT, .AccountAnalysisACode3)
            PopulateControlFromBOProperty(moAcctAnal4TEXT, .AccountAnalysisACode4)
            PopulateControlFromBOProperty(moAcctAnal5TEXT, .AccountAnalysisACode5)
            PopulateControlFromBOProperty(moAcctAnal6TEXT, .AccountAnalysisACode6)
            PopulateControlFromBOProperty(moAcctAnal7TEXT, .AccountAnalysisACode7)
            PopulateControlFromBOProperty(moAcctAnal8TEXT, .AccountAnalysisACode8)
            PopulateControlFromBOProperty(moAcctAnal9TEXT, .AccountAnalysisACode9)
            PopulateControlFromBOProperty(moAcctAnal10TEXT, .AccountAnalysisACode10)

            PopulateControlFromBOProperty(moSuppAnal1TEXT, .SupplierAnalysisCode1)
            PopulateControlFromBOProperty(moSuppAnal2TEXT, .SupplierAnalysisCode2)
            PopulateControlFromBOProperty(moSuppAnal3TEXT, .SupplierAnalysisCode3)
            PopulateControlFromBOProperty(moSuppAnal4TEXT, .SupplierAnalysisCode4)
            PopulateControlFromBOProperty(moSuppAnal5TEXT, .SupplierAnalysisCode5)
            PopulateControlFromBOProperty(moSuppAnal6TEXT, .SupplierAnalysisCode6)
            PopulateControlFromBOProperty(moSuppAnal7TEXT, .SupplierAnalysisCode7)
            PopulateControlFromBOProperty(moSuppAnal8TEXT, .SupplierAnalysisCode8)
            PopulateControlFromBOProperty(moSuppAnal9TEXT, .SupplierAnalysisCode9)
            PopulateControlFromBOProperty(moSuppAnal10TEXT, .SupplierAnalysisCode10)

            PopulateControlFromBOProperty(moAcctAnalT1TEXT, .AccountAnalysisCode1)
            PopulateControlFromBOProperty(moAcctAnalT2TEXT, .AccountAnalysisCode2)
            PopulateControlFromBOProperty(moAcctAnalT3TEXT, .AccountAnalysisCode3)
            PopulateControlFromBOProperty(moAcctAnalT4TEXT, .AccountAnalysisCode4)
            PopulateControlFromBOProperty(moAcctAnalT5TEXT, .AccountAnalysisCode5)
            PopulateControlFromBOProperty(moAcctAnalT6TEXT, .AccountAnalysisCode6)
            PopulateControlFromBOProperty(moAcctAnalT7TEXT, .AccountAnalysisCode7)
            PopulateControlFromBOProperty(moAcctAnalT8TEXT, .AccountAnalysisCode8)
            PopulateControlFromBOProperty(moAcctAnalT9TEXT, .AccountAnalysisCode9)
            PopulateControlFromBOProperty(moAcctAnalT10TEXT, .AccountAnalysisCode10)

            PopulateControlFromBOProperty(moUserAreaTEXT, .UserArea)
            PopulateControlFromBOProperty(moDescriptionTEXT, .Description)

            PopulateControlFromBOProperty(cboAddressStatus, BusinessObjectsNew.LookupListNew.GetIdFromCode(vendStdv, .AddressStatus))
            PopulateControlFromBOProperty(cboSupplierStatus, BusinessObjectsNew.LookupListNew.GetIdFromCode(vendStdv, .SupplierStatus))

            PopulateControlFromBOProperty(cboConversionCodeControl, BusinessObjectsNew.LookupListNew.GetIdFromCode(LookupListNew.DropdownLookupList(LookupListNew.LK_CONVCTL, langId, True), .ConversionCodeControl))
            PopulateControlFromBOProperty(cboReportConversionControl, BusinessObjectsNew.LookupListNew.GetIdFromCode(LookupListNew.DropdownLookupList(LookupListNew.LK_YESNO_NUM, langId, True), .ReportConversionControl))
            PopulateControlFromBOProperty(cboPaymentMethod, BusinessObjectsNew.LookupListNew.GetIdFromCode(LookupListNew.DropdownLookupList(LookupListNew.LK_PMTHD_NUM, langId, True), .PaymentMethod))
            PopulateControlFromBOProperty(cboPayAsPaidAccountType, BusinessObjectsNew.LookupListNew.GetIdFromCode(LookupListNew.DropdownLookupList(LookupListNew.LK_PAPAT, langId, True), .PayAsPaidAccountType))

            PopulateControlFromBOProperty(cboPaymentTerms, .PaymentTermsId)

            If State.ObjStatus_R <> ObjectStatus.isNew Then
                PopulateControlFromBOProperty(cboAcctAnal1, BusinessObjectsNew.LookupListNew.GetIdFromCode(AnalCodedv, .AccountAnalysis1))
                PopulateControlFromBOProperty(cboAcctAnal2, BusinessObjectsNew.LookupListNew.GetIdFromCode(AnalCodedv, .AccountAnalysis2))
                PopulateControlFromBOProperty(cboAcctAnal3, BusinessObjectsNew.LookupListNew.GetIdFromCode(AnalCodedv, .AccountAnalysis3))
                PopulateControlFromBOProperty(cboAcctAnal4, BusinessObjectsNew.LookupListNew.GetIdFromCode(AnalCodedv, .AccountAnalysis4))
                PopulateControlFromBOProperty(cboAcctAnal5, BusinessObjectsNew.LookupListNew.GetIdFromCode(AnalCodedv, .AccountAnalysis5))
                PopulateControlFromBOProperty(cboAcctAnal6, BusinessObjectsNew.LookupListNew.GetIdFromCode(AnalCodedv, .AccountAnalysis6))
                PopulateControlFromBOProperty(cboAcctAnal7, BusinessObjectsNew.LookupListNew.GetIdFromCode(AnalCodedv, .AccountAnalysis7))
                PopulateControlFromBOProperty(cboAcctAnal8, BusinessObjectsNew.LookupListNew.GetIdFromCode(AnalCodedv, .AccountAnalysis8))
                PopulateControlFromBOProperty(cboAcctAnal9, BusinessObjectsNew.LookupListNew.GetIdFromCode(AnalCodedv, .AccountAnalysis9))
                PopulateControlFromBOProperty(cboAcctAnal10, BusinessObjectsNew.LookupListNew.GetIdFromCode(AnalCodedv, .AccountAnalysis10))
            End If

        End With

        With State.MyBO_P
            PopulateControlFromBOProperty(moAccountCodeTEXT_P, .AccountCode)
            PopulateControlFromBOProperty(moBalanceTypeTEXT_P, .BalanceType)
            PopulateControlFromBOProperty(moDataAccessGroupCodeTEXT_P, .DataAccessGroupCode)
            PopulateControlFromBOProperty(moDefaultCurrencyCodeTEXT_P, .DefaultCurrencyCode)
            PopulateControlFromBOProperty(moSuppressReevaluationTEXT_P, .SuppressRevaluation)
            PopulateControlFromBOProperty(moAddressLookupCodeTEXT_P, .AddressLookupCode)
            PopulateControlFromBOProperty(moAddressSequenceNumberTEXT_P, .AddressSequenceNumber)
            PopulateControlFromBOProperty(moSupplierLookUpCodeTEXT_P, .SupplierLookupCode)

            PopulateControlFromBOProperty(moAcctAnal1TEXT_P, .AccountAnalysisACode1)
            PopulateControlFromBOProperty(moAcctAnal2TEXT_P, .AccountAnalysisACode2)
            PopulateControlFromBOProperty(moAcctAnal3TEXT_P, .AccountAnalysisACode3)
            PopulateControlFromBOProperty(moAcctAnal4TEXT_P, .AccountAnalysisACode4)
            PopulateControlFromBOProperty(moAcctAnal5TEXT_P, .AccountAnalysisACode5)
            PopulateControlFromBOProperty(moAcctAnal6TEXT_P, .AccountAnalysisACode6)
            PopulateControlFromBOProperty(moAcctAnal7TEXT_P, .AccountAnalysisACode7)
            PopulateControlFromBOProperty(moAcctAnal8TEXT_P, .AccountAnalysisACode8)
            PopulateControlFromBOProperty(moAcctAnal9TEXT_P, .AccountAnalysisACode9)
            PopulateControlFromBOProperty(moAcctAnal10TEXT_P, .AccountAnalysisACode10)

            PopulateControlFromBOProperty(cboSuppAnal1_P, BusinessObjectsNew.LookupListNew.GetIdFromCode(LookupListNew.DropdownLookupList(LookupListNew.LK_ACCOUNTING_PAYMENT_METHOD, langId, True), .SupplierAnalysisCode1))
            PopulateControlFromBOProperty(moSuppAnal2TEXT_P, .SupplierAnalysisCode2)
            PopulateControlFromBOProperty(moSuppAnal3TEXT_P, .SupplierAnalysisCode3)
            PopulateControlFromBOProperty(moSuppAnal4TEXT_P, .SupplierAnalysisCode4)
            PopulateControlFromBOProperty(moSuppAnal5TEXT_P, .SupplierAnalysisCode5)
            PopulateControlFromBOProperty(moSuppAnal6TEXT_P, .SupplierAnalysisCode6)
            PopulateControlFromBOProperty(moSuppAnal7TEXT_P, .SupplierAnalysisCode7)
            PopulateControlFromBOProperty(moSuppAnal8TEXT_P, .SupplierAnalysisCode8)
            PopulateControlFromBOProperty(moSuppAnal9TEXT_P, .SupplierAnalysisCode9)
            PopulateControlFromBOProperty(moSuppAnal10TEXT_P, .SupplierAnalysisCode10)
            PopulateControlFromBOProperty(moAcctAnalT1TEXT_P, .AccountAnalysisCode1)
            PopulateControlFromBOProperty(moAcctAnalT2TEXT_P, .AccountAnalysisCode2)
            PopulateControlFromBOProperty(moAcctAnalT3TEXT_P, .AccountAnalysisCode3)
            PopulateControlFromBOProperty(moAcctAnalT4TEXT_P, .AccountAnalysisCode4)
            PopulateControlFromBOProperty(moAcctAnalT5TEXT_P, .AccountAnalysisCode5)
            PopulateControlFromBOProperty(moAcctAnalT6TEXT_P, .AccountAnalysisCode6)
            PopulateControlFromBOProperty(moAcctAnalT7TEXT_P, .AccountAnalysisCode7)
            PopulateControlFromBOProperty(moAcctAnalT8TEXT_P, .AccountAnalysisCode8)
            PopulateControlFromBOProperty(moAcctAnalT9TEXT_P, .AccountAnalysisCode9)
            PopulateControlFromBOProperty(moAcctAnalT10TEXT_P, .AccountAnalysisCode10)
            PopulateControlFromBOProperty(moUserAreaTEXT_P, .UserArea)
            PopulateControlFromBOProperty(moDescriptionTEXT_P, .Description)
            PopulateControlFromBOProperty(moDefaultBankSubCodeTEXT_P, .DefaultBankSubCode)
            PopulateControlFromBOProperty(cboAddressStatus_P, BusinessObjectsNew.LookupListNew.GetIdFromCode(vendStdv, .AddressStatus))
            PopulateControlFromBOProperty(cboSupplierStatus_P, BusinessObjectsNew.LookupListNew.GetIdFromCode(vendStdv, .SupplierStatus))
            PopulateControlFromBOProperty(cboConversionCodeControl_P, BusinessObjectsNew.LookupListNew.GetIdFromCode(LookupListNew.DropdownLookupList(LookupListNew.LK_CONVCTL, langId, True), .ConversionCodeControl))
            PopulateControlFromBOProperty(cboReportConversionControl_P, BusinessObjectsNew.LookupListNew.GetIdFromCode(LookupListNew.DropdownLookupList(LookupListNew.LK_YESNO_NUM, langId, True), .ReportConversionControl))
            PopulateControlFromBOProperty(cboPaymentMethod_P, BusinessObjectsNew.LookupListNew.GetIdFromCode(LookupListNew.DropdownLookupList(LookupListNew.LK_PMTHD_NUM, langId, True), .PaymentMethod))
            PopulateControlFromBOProperty(cboPayAsPaidAccountType_P, BusinessObjectsNew.LookupListNew.GetIdFromCode(LookupListNew.DropdownLookupList(LookupListNew.LK_PAPAT, langId, True), .PayAsPaidAccountType))
            PopulateControlFromBOProperty(cboPaymentTerms_P, .PaymentTermsId)

            If State.ObjStatus_P <> ObjectStatus.isNew Then
                PopulateControlFromBOProperty(cboAcctAnal1_P, BusinessObjectsNew.LookupListNew.GetIdFromCode(AnalCodedv, .AccountAnalysis1))
                PopulateControlFromBOProperty(cboAcctAnal2_P, BusinessObjectsNew.LookupListNew.GetIdFromCode(AnalCodedv, .AccountAnalysis2))
                PopulateControlFromBOProperty(cboAcctAnal3_P, BusinessObjectsNew.LookupListNew.GetIdFromCode(AnalCodedv, .AccountAnalysis3))
                PopulateControlFromBOProperty(cboAcctAnal4_P, BusinessObjectsNew.LookupListNew.GetIdFromCode(AnalCodedv, .AccountAnalysis4))
                PopulateControlFromBOProperty(cboAcctAnal5_P, BusinessObjectsNew.LookupListNew.GetIdFromCode(AnalCodedv, .AccountAnalysis5))
                PopulateControlFromBOProperty(cboAcctAnal6_P, BusinessObjectsNew.LookupListNew.GetIdFromCode(AnalCodedv, .AccountAnalysis6))
                PopulateControlFromBOProperty(cboAcctAnal7_P, BusinessObjectsNew.LookupListNew.GetIdFromCode(AnalCodedv, .AccountAnalysis7))
                PopulateControlFromBOProperty(cboAcctAnal8_P, BusinessObjectsNew.LookupListNew.GetIdFromCode(AnalCodedv, .AccountAnalysis8))
                PopulateControlFromBOProperty(cboAcctAnal9_P, BusinessObjectsNew.LookupListNew.GetIdFromCode(AnalCodedv, .AccountAnalysis9))
                PopulateControlFromBOProperty(cboAcctAnal10_P, BusinessObjectsNew.LookupListNew.GetIdFromCode(AnalCodedv, .AccountAnalysis10))
            End If
        End With
    End Sub

    Protected Sub PopulateBOsFromForm()
        If Me.State.TargetStatus = ObjectStatus.isNew OrElse State.TargetStatus = ObjectStatus.isACopy Then

            If State.objectType = ReturnType.TargetType.Branch Then
                State.BranchDealerId = GetSelectedItem(ddlTargetist)
                State.TargetId = GetSelectedItem(moBranchNameDROP)
            Else
                State.TargetId = GetSelectedItem(ddlTargetist)
            End If
            State.accountName = GetSelectedDescription(ddlTargetist)
            Select Case State.objectType
                Case ReturnType.TargetType.DealerGroup
                    'dealer group
                    PopulateBOProperty(State.MyBO_R, DEALER_GROUP_PROPERTY, State.TargetId)
                    PopulateBOProperty(State.MyBO_P, DEALER_GROUP_PROPERTY, State.TargetId)
                Case ReturnType.TargetType.Dealer
                    ' Dealer
                    PopulateBOProperty(State.MyBO_R, DEALER_PROPERTY, State.TargetId)
                    PopulateBOProperty(State.MyBO_P, DEALER_PROPERTY, State.TargetId)
                Case ReturnType.TargetType.ServiceCenter
                    'ServiceCenter
                    PopulateBOProperty(State.MyBO_R, SERVICE_CENTER_PROPERTY, State.TargetId)
                    PopulateBOProperty(State.MyBO_P, SERVICE_CENTER_PROPERTY, State.TargetId)
                Case ReturnType.TargetType.Branch
                    'Branch
                    PopulateBOProperty(State.MyBO_R, DEALER_PROPERTY, State.BranchDealerId)
                    PopulateBOProperty(State.MyBO_P, DEALER_PROPERTY, State.BranchDealerId)
                    'Dim selectedBranchId As Guid = Me.GetSelectedItem(Me.moBranchNameDROP)
                    PopulateBOProperty(State.MyBO_R, BRANCH_ID_PROPERTY, State.TargetId)
                    PopulateBOProperty(State.MyBO_P, BRANCH_ID_PROPERTY, State.TargetId)
                    'Me.State.branchName = Me.GetSelectedDescription(moBranchNameDROP)
                Case ReturnType.TargetType.CommissionEntity
                    PopulateBOProperty(State.MyBO_R, COMMISSION_ENTITY_ID_PROPERTY, State.TargetId)
                    PopulateBOProperty(State.MyBO_P, COMMISSION_ENTITY_ID_PROPERTY, State.TargetId)
            End Select
        End If

        'Fill lists
        Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
        Dim AnalCodedv As DataView = LookupListNew.DropdownLookupList(LookupListNew.LK_ANYLSCODE, langId, True)
        Dim vendStdv As DataView = LookupListNew.DropdownLookupList(LookupListNew.LK_VENSTAT, langId, True)
        Dim blnBOChanged As Boolean = False, guidItem As Guid, strObj As String

        With State.MyBO_R
            If .IsNew AndAlso .AccountCode <> moAccountCodeTEXT.Text Then blnBOChanged = True
            PopulateBOProperty(State.MyBO_R, ACCOUNTCODE_PROPERTY, moAccountCodeTEXT)

            If cboCompany.Visible = False Then
                PopulateBOProperty(State.MyBO_R, ACCTCOMPANYID_PROPERTY, moCompId)
            Else
                Dim selectedCompId As Guid = GetSelectedItem(cboCompany)
                If .IsNew AndAlso .AcctCompanyId <> selectedCompId Then blnBOChanged = True
                PopulateBOProperty(State.MyBO_R, ACCTCOMPANYID_PROPERTY, selectedCompId)
            End If

            If .IsNew AndAlso .BalanceType <> moBalanceTypeTEXT.Text Then blnBOChanged = True
            PopulateBOProperty(State.MyBO_R, BALANCETYPE_PROPERTY, moBalanceTypeTEXT)

            If .IsNew AndAlso .DataAccessGroupCode <> moDataAccessGroupCodeTEXT.Text Then blnBOChanged = True
            PopulateBOProperty(State.MyBO_R, DATAACCESSGROUPCODE_PROPERTY, moDataAccessGroupCodeTEXT)

            If .IsNew AndAlso .DefaultCurrencyCode <> moDefaultCurrencyCodeTEXT.Text Then blnBOChanged = True
            PopulateBOProperty(State.MyBO_R, DEFAULTCURRENCYCODE_PROPERTY, moDefaultCurrencyCodeTEXT)

            If .IsNew AndAlso .SuppressRevaluation <> moSuppressReevaluationTEXT.Text Then blnBOChanged = True
            PopulateBOProperty(State.MyBO_R, SUPPRESSREVALUATION_PROPERTY, moSuppressReevaluationTEXT)

            If .IsNew AndAlso .AccountAnalysisACode1 <> moAcctAnal1TEXT.Text Then blnBOChanged = True
            PopulateBOProperty(State.MyBO_R, ACCOUNTANALYSIS_A_1_PROPERTY, moAcctAnal1TEXT)

            If .IsNew AndAlso .AccountAnalysisACode2 <> moAcctAnal2TEXT.Text Then blnBOChanged = True
            PopulateBOProperty(State.MyBO_R, ACCOUNTANALYSIS_A_2_PROPERTY, moAcctAnal2TEXT)

            If .IsNew AndAlso .AccountAnalysisACode3 <> moAcctAnal3TEXT.Text Then blnBOChanged = True
            PopulateBOProperty(State.MyBO_R, ACCOUNTANALYSIS_A_3_PROPERTY, moAcctAnal3TEXT)

            If .IsNew AndAlso .AccountAnalysisACode4 <> moAcctAnal4TEXT.Text Then blnBOChanged = True
            PopulateBOProperty(State.MyBO_R, ACCOUNTANALYSIS_A_4_PROPERTY, moAcctAnal4TEXT)

            If .IsNew AndAlso .AccountAnalysisACode5 <> moAcctAnal5TEXT.Text Then blnBOChanged = True
            PopulateBOProperty(State.MyBO_R, ACCOUNTANALYSIS_A_5_PROPERTY, moAcctAnal5TEXT)

            If .IsNew AndAlso .AccountAnalysisACode6 <> moAcctAnal6TEXT.Text Then blnBOChanged = True
            PopulateBOProperty(State.MyBO_R, ACCOUNTANALYSIS_A_6_PROPERTY, moAcctAnal6TEXT)

            If .IsNew AndAlso .AccountAnalysisACode7 <> moAcctAnal7TEXT.Text Then blnBOChanged = True
            PopulateBOProperty(State.MyBO_R, ACCOUNTANALYSIS_A_7_PROPERTY, moAcctAnal7TEXT)

            If .IsNew AndAlso .AccountAnalysisACode8 <> moAcctAnal8TEXT.Text Then blnBOChanged = True
            PopulateBOProperty(State.MyBO_R, ACCOUNTANALYSIS_A_8_PROPERTY, moAcctAnal8TEXT)

            If .IsNew AndAlso .AccountAnalysisACode9 <> moAcctAnal9TEXT.Text Then blnBOChanged = True
            PopulateBOProperty(State.MyBO_R, ACCOUNTANALYSIS_A_9_PROPERTY, moAcctAnal9TEXT)

            If .IsNew AndAlso .AccountAnalysisACode10 <> moAcctAnal10TEXT.Text Then blnBOChanged = True
            PopulateBOProperty(State.MyBO_R, ACCOUNTANALYSIS_A_10_PROPERTY, moAcctAnal10TEXT)

            If .IsNew AndAlso .AccountAnalysisCode1 <> moAcctAnalT1TEXT.Text Then blnBOChanged = True
            PopulateBOProperty(State.MyBO_R, ACCOUNTANALYSIS1_PROPERTY, moAcctAnalT1TEXT)

            If .IsNew AndAlso .AccountAnalysisCode2 <> moAcctAnalT2TEXT.Text Then blnBOChanged = True
            PopulateBOProperty(State.MyBO_R, ACCOUNTANALYSIS2_PROPERTY, moAcctAnalT2TEXT)

            If .IsNew AndAlso .AccountAnalysisCode3 <> moAcctAnalT3TEXT.Text Then blnBOChanged = True
            PopulateBOProperty(State.MyBO_R, ACCOUNTANALYSIS3_PROPERTY, moAcctAnalT3TEXT)

            If .IsNew AndAlso .AccountAnalysisCode4 <> moAcctAnalT4TEXT.Text Then blnBOChanged = True
            PopulateBOProperty(State.MyBO_R, ACCOUNTANALYSIS4_PROPERTY, moAcctAnalT4TEXT)

            If .IsNew AndAlso .AccountAnalysisCode5 <> moAcctAnalT5TEXT.Text Then blnBOChanged = True
            PopulateBOProperty(State.MyBO_R, ACCOUNTANALYSIS5_PROPERTY, moAcctAnalT5TEXT)

            If .IsNew AndAlso .AccountAnalysisCode6 <> moAcctAnalT6TEXT.Text Then blnBOChanged = True
            PopulateBOProperty(State.MyBO_R, ACCOUNTANALYSIS6_PROPERTY, moAcctAnalT6TEXT)

            If .IsNew AndAlso .AccountAnalysisCode7 <> moAcctAnalT7TEXT.Text Then blnBOChanged = True
            PopulateBOProperty(State.MyBO_R, ACCOUNTANALYSIS7_PROPERTY, moAcctAnalT7TEXT)

            If .IsNew AndAlso .AccountAnalysisCode8 <> moAcctAnalT8TEXT.Text Then blnBOChanged = True
            PopulateBOProperty(State.MyBO_R, ACCOUNTANALYSIS8_PROPERTY, moAcctAnalT8TEXT)

            If .IsNew AndAlso .AccountAnalysisCode9 <> moAcctAnalT9TEXT.Text Then blnBOChanged = True
            PopulateBOProperty(State.MyBO_R, ACCOUNTANALYSIS9_PROPERTY, moAcctAnalT9TEXT)

            If .IsNew AndAlso .AccountAnalysisCode10 <> moAcctAnalT10TEXT.Text Then blnBOChanged = True
            PopulateBOProperty(State.MyBO_R, ACCOUNTANALYSIS10_PROPERTY, moAcctAnalT10TEXT)

            If .IsNew AndAlso .SupplierAnalysisCode1 <> moSuppAnal1TEXT.Text Then blnBOChanged = True
            PopulateBOProperty(State.MyBO_R, SUPPLIERANALYSIS1_PROPERTY, moSuppAnal1TEXT)

            If .IsNew AndAlso .SupplierAnalysisCode2 <> moSuppAnal2TEXT.Text Then blnBOChanged = True
            PopulateBOProperty(State.MyBO_R, SUPPLIERANALYSIS2_PROPERTY, moSuppAnal2TEXT)

            If .IsNew AndAlso .SupplierAnalysisCode3 <> moSuppAnal3TEXT.Text Then blnBOChanged = True
            PopulateBOProperty(State.MyBO_R, SUPPLIERANALYSIS3_PROPERTY, moSuppAnal3TEXT)

            If .IsNew AndAlso .SupplierAnalysisCode4 <> moSuppAnal4TEXT.Text Then blnBOChanged = True
            PopulateBOProperty(State.MyBO_R, SUPPLIERANALYSIS4_PROPERTY, moSuppAnal4TEXT)

            If .IsNew AndAlso .SupplierAnalysisCode5 <> moSuppAnal5TEXT.Text Then blnBOChanged = True
            PopulateBOProperty(State.MyBO_R, SUPPLIERANALYSIS5_PROPERTY, moSuppAnal5TEXT)

            If .IsNew AndAlso .SupplierAnalysisCode6 <> moSuppAnal6TEXT.Text Then blnBOChanged = True
            PopulateBOProperty(State.MyBO_R, SUPPLIERANALYSIS6_PROPERTY, moSuppAnal6TEXT)

            If .IsNew AndAlso .SupplierAnalysisCode7 <> moSuppAnal7TEXT.Text Then blnBOChanged = True
            PopulateBOProperty(State.MyBO_R, SUPPLIERANALYSIS7_PROPERTY, moSuppAnal7TEXT)

            If .IsNew AndAlso .SupplierAnalysisCode8 <> moSuppAnal8TEXT.Text Then blnBOChanged = True
            PopulateBOProperty(State.MyBO_R, SUPPLIERANALYSIS8_PROPERTY, moSuppAnal8TEXT)

            If .IsNew AndAlso .SupplierAnalysisCode9 <> moSuppAnal9TEXT.Text Then blnBOChanged = True
            PopulateBOProperty(State.MyBO_R, SUPPLIERANALYSIS9_PROPERTY, moSuppAnal9TEXT)

            If .IsNew AndAlso .SupplierAnalysisCode10 <> moSuppAnal10TEXT.Text Then blnBOChanged = True
            PopulateBOProperty(State.MyBO_R, SUPPLIERANALYSIS10_PROPERTY, moSuppAnal10TEXT)

            If .IsNew AndAlso .AddressLookupCode <> moAddressLookupCodeTEXT.Text Then blnBOChanged = True
            PopulateBOProperty(State.MyBO_R, ADDRESSLOOKUPCODE_PROPERTY, moAddressLookupCodeTEXT)

            If .IsNew AndAlso .AddressSequenceNumber <> moAddressSequenceNumberTEXT.Text Then blnBOChanged = True
            PopulateBOProperty(State.MyBO_R, ADDRESSSEQUENCENUMBER_PROPERTY, moAddressSequenceNumberTEXT)

            If .IsNew AndAlso .SupplierLookupCode <> moSupplierLookUpCodeTEXT.Text Then blnBOChanged = True
            PopulateBOProperty(State.MyBO_R, SUPPLIERLOOKUPCODE_PROPERTY, moSupplierLookUpCodeTEXT)

            If .IsNew AndAlso .UserArea <> moUserAreaTEXT.Text Then blnBOChanged = True
            PopulateBOProperty(State.MyBO_R, USER_AREA_PROPERTY, moUserAreaTEXT)

            If .IsNew AndAlso .Description <> moDescriptionTEXT.Text Then blnBOChanged = True
            PopulateBOProperty(State.MyBO_R, DESCRIPTION_PROPERTY, moDescriptionTEXT)

            guidItem = .PaymentTermsId
            PopulateBOProperty(State.MyBO_R, PAYMENT_TERMS_ID_PROPERTY, cboPaymentTerms)
            If .IsNew AndAlso .PaymentTermsId <> guidItem Then blnBOChanged = True

            strObj = .ConversionCodeControl
            PopulateBOProperty(State.MyBO_R, CONVERSIONCODECONTROL_PROPERTY, LookupListNew.GetCodeFromId(LookupListNew.DropdownLookupList(LookupListNew.LK_CONVCTL, langId, True), New Guid(cboConversionCodeControl.SelectedItem.Value)))
            If .IsNew AndAlso .ConversionCodeControl <> strObj Then blnBOChanged = True

            strObj = .ReportConversionControl
            PopulateBOProperty(State.MyBO_R, REPORTCONVERSIONCONTROL_PROPERTY, LookupListNew.GetCodeFromId(LookupListNew.DropdownLookupList(LookupListNew.LK_YESNO_NUM, langId, True), New Guid(cboReportConversionControl.SelectedItem.Value)))
            If .IsNew AndAlso .ReportConversionControl <> strObj Then blnBOChanged = True

            strObj = .PaymentMethod
            PopulateBOProperty(State.MyBO_R, PAYMENTMETHOD_PROPERTY, LookupListNew.GetCodeFromId(LookupListNew.DropdownLookupList(LookupListNew.LK_PMTHD_NUM, langId, True), New Guid(cboPaymentMethod.SelectedItem.Value)))
            If .IsNew AndAlso .PaymentMethod <> strObj Then blnBOChanged = True

            strObj = .PayAsPaidAccountType
            PopulateBOProperty(State.MyBO_R, PAYASPAIDACCOUNTTYPE_PROPERTY, LookupListNew.GetCodeFromId(LookupListNew.DropdownLookupList(LookupListNew.LK_PAPAT, langId, True), New Guid(cboPayAsPaidAccountType.SelectedItem.Value)))
            If .IsNew AndAlso .PayAsPaidAccountType <> strObj Then blnBOChanged = True

            strObj = .AddressStatus
            PopulateBOProperty(State.MyBO_R, ADDRESSSTATUS_PROPERTY, LookupListNew.GetCodeFromId(vendStdv, New Guid(cboAddressStatus.SelectedItem.Value)))
            If .IsNew AndAlso .AddressStatus <> strObj Then blnBOChanged = True

            strObj = .SupplierStatus
            PopulateBOProperty(State.MyBO_R, SUPPLIERSTATUS_PROPERTY, LookupListNew.GetCodeFromId(vendStdv, New Guid(cboSupplierStatus.SelectedItem.Value)))
            If .IsNew AndAlso .SupplierStatus <> strObj Then blnBOChanged = True

            strObj = .AccountAnalysis1
            PopulateBOProperty(State.MyBO_R, ACCOUNT_ANALYSIS_FLAG1_PROPERTY, LookupListNew.GetCodeFromId(AnalCodedv, New Guid(cboAcctAnal1.SelectedItem.Value)))
            If .IsNew AndAlso .AccountAnalysis1 <> strObj Then blnBOChanged = True

            strObj = .AccountAnalysis2
            PopulateBOProperty(State.MyBO_R, ACCOUNT_ANALYSIS_FLAG2_PROPERTY, LookupListNew.GetCodeFromId(AnalCodedv, New Guid(cboAcctAnal2.SelectedItem.Value)))
            If .IsNew AndAlso .AccountAnalysis2 <> strObj Then blnBOChanged = True

            strObj = .AccountAnalysis3
            PopulateBOProperty(State.MyBO_R, ACCOUNT_ANALYSIS_FLAG3_PROPERTY, LookupListNew.GetCodeFromId(AnalCodedv, New Guid(cboAcctAnal3.SelectedItem.Value)))
            If .IsNew AndAlso .AccountAnalysis3 <> strObj Then blnBOChanged = True

            strObj = .AccountAnalysis4
            PopulateBOProperty(State.MyBO_R, ACCOUNT_ANALYSIS_FLAG4_PROPERTY, LookupListNew.GetCodeFromId(AnalCodedv, New Guid(cboAcctAnal4.SelectedItem.Value)))
            If .IsNew AndAlso .AccountAnalysis4 <> strObj Then blnBOChanged = True

            strObj = .AccountAnalysis5
            PopulateBOProperty(State.MyBO_R, ACCOUNT_ANALYSIS_FLAG5_PROPERTY, LookupListNew.GetCodeFromId(AnalCodedv, New Guid(cboAcctAnal5.SelectedItem.Value)))
            If .IsNew AndAlso .AccountAnalysis5 <> strObj Then blnBOChanged = True

            strObj = .AccountAnalysis6
            PopulateBOProperty(State.MyBO_R, ACCOUNT_ANALYSIS_FLAG6_PROPERTY, LookupListNew.GetCodeFromId(AnalCodedv, New Guid(cboAcctAnal6.SelectedItem.Value)))
            If .IsNew AndAlso .AccountAnalysis6 <> strObj Then blnBOChanged = True

            strObj = .AccountAnalysis7
            PopulateBOProperty(State.MyBO_R, ACCOUNT_ANALYSIS_FLAG7_PROPERTY, LookupListNew.GetCodeFromId(AnalCodedv, New Guid(cboAcctAnal7.SelectedItem.Value)))
            If .IsNew AndAlso .AccountAnalysis7 <> strObj Then blnBOChanged = True

            strObj = .AccountAnalysis8
            PopulateBOProperty(State.MyBO_R, ACCOUNT_ANALYSIS_FLAG8_PROPERTY, LookupListNew.GetCodeFromId(AnalCodedv, New Guid(cboAcctAnal8.SelectedItem.Value)))
            If .IsNew AndAlso .AccountAnalysis8 <> strObj Then blnBOChanged = True

            strObj = .AccountAnalysis9
            PopulateBOProperty(State.MyBO_R, ACCOUNT_ANALYSIS_FLAG9_PROPERTY, LookupListNew.GetCodeFromId(AnalCodedv, New Guid(cboAcctAnal9.SelectedItem.Value)))
            If .IsNew AndAlso .AccountAnalysis9 <> strObj Then blnBOChanged = True

            strObj = .AccountAnalysis10
            PopulateBOProperty(State.MyBO_R, ACCOUNT_ANALYSIS_FLAG10_PROPERTY, LookupListNew.GetCodeFromId(AnalCodedv, New Guid(cboAcctAnal10.SelectedItem.Value)))
            If .IsNew AndAlso .AccountAnalysis10 <> strObj Then blnBOChanged = True
        End With

        If blnBOChanged Then
            State.IsNewBORDirty = (True OrElse State.IsNewBORDirty)
            blnBOChanged = False
        End If


        'payable 
        With State.MyBO_P
            If .IsNew AndAlso .AccountCode <> moAccountCodeTEXT_P.Text Then blnBOChanged = True
            PopulateBOProperty(State.MyBO_P, ACCOUNTCODE_PROPERTY, moAccountCodeTEXT_P)

            If cboCompany_P.Visible = False Then
                PopulateBOProperty(State.MyBO_P, ACCTCOMPANYID_PROPERTY, moCompId)
            Else
                Dim selectedCompId As Guid = GetSelectedItem(cboCompany_P)
                If .IsNew AndAlso .AcctCompanyId <> selectedCompId Then blnBOChanged = True
                PopulateBOProperty(State.MyBO_P, ACCTCOMPANYID_PROPERTY, selectedCompId)
            End If

            If .IsNew AndAlso .BalanceType <> moBalanceTypeTEXT_P.Text Then blnBOChanged = True
            PopulateBOProperty(State.MyBO_P, BALANCETYPE_PROPERTY, moBalanceTypeTEXT_P)

            If .IsNew AndAlso .DataAccessGroupCode <> moDataAccessGroupCodeTEXT_P.Text Then blnBOChanged = True
            PopulateBOProperty(State.MyBO_P, DATAACCESSGROUPCODE_PROPERTY, moDataAccessGroupCodeTEXT_P)

            If .IsNew AndAlso .DefaultCurrencyCode <> moDefaultCurrencyCodeTEXT_P.Text Then blnBOChanged = True
            PopulateBOProperty(State.MyBO_P, DEFAULTCURRENCYCODE_PROPERTY, moDefaultCurrencyCodeTEXT_P)

            If .IsNew AndAlso .SuppressRevaluation <> moSuppressReevaluationTEXT_P.Text Then blnBOChanged = True
            PopulateBOProperty(State.MyBO_P, SUPPRESSREVALUATION_PROPERTY, moSuppressReevaluationTEXT_P)

            If .IsNew AndAlso .AccountAnalysisACode1 <> moAcctAnal1TEXT_P.Text Then blnBOChanged = True
            PopulateBOProperty(State.MyBO_P, ACCOUNTANALYSIS_A_1_PROPERTY, moAcctAnal1TEXT_P)

            If .IsNew AndAlso .AccountAnalysisACode2 <> moAcctAnal2TEXT_P.Text Then blnBOChanged = True
            PopulateBOProperty(State.MyBO_P, ACCOUNTANALYSIS_A_2_PROPERTY, moAcctAnal2TEXT_P)

            If .IsNew AndAlso .AccountAnalysisACode3 <> moAcctAnal3TEXT_P.Text Then blnBOChanged = True
            PopulateBOProperty(State.MyBO_P, ACCOUNTANALYSIS_A_3_PROPERTY, moAcctAnal3TEXT_P)

            If .IsNew AndAlso .AccountAnalysisACode4 <> moAcctAnal4TEXT_P.Text Then blnBOChanged = True
            PopulateBOProperty(State.MyBO_P, ACCOUNTANALYSIS_A_4_PROPERTY, moAcctAnal4TEXT_P)

            If .IsNew AndAlso .AccountAnalysisACode5 <> moAcctAnal5TEXT_P.Text Then blnBOChanged = True
            PopulateBOProperty(State.MyBO_P, ACCOUNTANALYSIS_A_5_PROPERTY, moAcctAnal5TEXT_P)

            If .IsNew AndAlso .AccountAnalysisACode6 <> moAcctAnal6TEXT_P.Text Then blnBOChanged = True
            PopulateBOProperty(State.MyBO_P, ACCOUNTANALYSIS_A_6_PROPERTY, moAcctAnal6TEXT_P)

            If .IsNew AndAlso .AccountAnalysisACode7 <> moAcctAnal7TEXT_P.Text Then blnBOChanged = True
            PopulateBOProperty(State.MyBO_P, ACCOUNTANALYSIS_A_7_PROPERTY, moAcctAnal7TEXT_P)

            If .IsNew AndAlso .AccountAnalysisACode8 <> moAcctAnal8TEXT_P.Text Then blnBOChanged = True
            PopulateBOProperty(State.MyBO_P, ACCOUNTANALYSIS_A_8_PROPERTY, moAcctAnal8TEXT_P)

            If .IsNew AndAlso .AccountAnalysisACode9 <> moAcctAnal9TEXT_P.Text Then blnBOChanged = True
            PopulateBOProperty(State.MyBO_P, ACCOUNTANALYSIS_A_9_PROPERTY, moAcctAnal9TEXT_P)

            If .IsNew AndAlso .AccountAnalysisACode10 <> moAcctAnal10TEXT_P.Text Then blnBOChanged = True
            PopulateBOProperty(State.MyBO_P, ACCOUNTANALYSIS_A_10_PROPERTY, moAcctAnal10TEXT_P)

            If .IsNew AndAlso .AccountAnalysisCode1 <> moAcctAnalT1TEXT_P.Text Then blnBOChanged = True
            PopulateBOProperty(State.MyBO_P, ACCOUNTANALYSIS1_PROPERTY, moAcctAnalT1TEXT_P)

            If .IsNew AndAlso .AccountAnalysisCode2 <> moAcctAnalT2TEXT_P.Text Then blnBOChanged = True
            PopulateBOProperty(State.MyBO_P, ACCOUNTANALYSIS2_PROPERTY, moAcctAnalT2TEXT_P)

            If .IsNew AndAlso .AccountAnalysisCode3 <> moAcctAnalT3TEXT_P.Text Then blnBOChanged = True
            PopulateBOProperty(State.MyBO_P, ACCOUNTANALYSIS3_PROPERTY, moAcctAnalT3TEXT_P)

            If .IsNew AndAlso .AccountAnalysisCode4 <> moAcctAnalT4TEXT_P.Text Then blnBOChanged = True
            PopulateBOProperty(State.MyBO_P, ACCOUNTANALYSIS4_PROPERTY, moAcctAnalT4TEXT_P)

            If .IsNew AndAlso .AccountAnalysisCode5 <> moAcctAnalT5TEXT_P.Text Then blnBOChanged = True
            PopulateBOProperty(State.MyBO_P, ACCOUNTANALYSIS5_PROPERTY, moAcctAnalT5TEXT_P)

            If .IsNew AndAlso .AccountAnalysisCode6 <> moAcctAnalT6TEXT_P.Text Then blnBOChanged = True
            PopulateBOProperty(State.MyBO_P, ACCOUNTANALYSIS6_PROPERTY, moAcctAnalT6TEXT_P)

            If .IsNew AndAlso .AccountAnalysisCode7 <> moAcctAnalT7TEXT_P.Text Then blnBOChanged = True
            PopulateBOProperty(State.MyBO_P, ACCOUNTANALYSIS7_PROPERTY, moAcctAnalT7TEXT_P)

            If .IsNew AndAlso .AccountAnalysisCode8 <> moAcctAnalT8TEXT_P.Text Then blnBOChanged = True
            PopulateBOProperty(State.MyBO_P, ACCOUNTANALYSIS8_PROPERTY, moAcctAnalT8TEXT_P)

            If .IsNew AndAlso .AccountAnalysisCode9 <> moAcctAnalT9TEXT_P.Text Then blnBOChanged = True
            PopulateBOProperty(State.MyBO_P, ACCOUNTANALYSIS9_PROPERTY, moAcctAnalT9TEXT_P)

            If .IsNew AndAlso .AccountAnalysisCode10 <> moAcctAnalT10TEXT_P.Text Then blnBOChanged = True
            PopulateBOProperty(State.MyBO_P, ACCOUNTANALYSIS10_PROPERTY, moAcctAnalT10TEXT_P)

            strObj = .SupplierAnalysisCode1
            PopulateBOProperty(State.MyBO_P, SUPPLIERANALYSIS1_PROPERTY, LookupListNew.GetCodeFromId(LookupListNew.DropdownLookupList(LookupListNew.LK_ACCOUNTING_PAYMENT_METHOD, langId, True), New Guid(cboSuppAnal1_P.SelectedItem.Value)))
            If .IsNew AndAlso .SupplierAnalysisCode1 <> strObj Then blnBOChanged = True

            If .IsNew AndAlso .SupplierAnalysisCode2 <> moSuppAnal2TEXT_P.Text Then blnBOChanged = True
            PopulateBOProperty(State.MyBO_P, SUPPLIERANALYSIS2_PROPERTY, moSuppAnal2TEXT_P)

            If .IsNew AndAlso .SupplierAnalysisCode3 <> moSuppAnal3TEXT_P.Text Then blnBOChanged = True
            PopulateBOProperty(State.MyBO_P, SUPPLIERANALYSIS3_PROPERTY, moSuppAnal3TEXT_P)

            If .IsNew AndAlso .SupplierAnalysisCode4 <> moSuppAnal4TEXT_P.Text Then blnBOChanged = True
            PopulateBOProperty(State.MyBO_P, SUPPLIERANALYSIS4_PROPERTY, moSuppAnal4TEXT_P)

            If .IsNew AndAlso .SupplierAnalysisCode5 <> moSuppAnal5TEXT_P.Text Then blnBOChanged = True
            PopulateBOProperty(State.MyBO_P, SUPPLIERANALYSIS5_PROPERTY, moSuppAnal5TEXT_P)

            If .IsNew AndAlso .SupplierAnalysisCode6 <> moSuppAnal6TEXT_P.Text Then blnBOChanged = True
            PopulateBOProperty(State.MyBO_P, SUPPLIERANALYSIS6_PROPERTY, moSuppAnal6TEXT_P)

            If .IsNew AndAlso .SupplierAnalysisCode7 <> moSuppAnal7TEXT_P.Text Then blnBOChanged = True
            PopulateBOProperty(State.MyBO_P, SUPPLIERANALYSIS7_PROPERTY, moSuppAnal7TEXT_P)

            If .IsNew AndAlso .SupplierAnalysisCode8 <> moSuppAnal8TEXT_P.Text Then blnBOChanged = True
            PopulateBOProperty(State.MyBO_P, SUPPLIERANALYSIS8_PROPERTY, moSuppAnal8TEXT_P)

            If .IsNew AndAlso .SupplierAnalysisCode9 <> moSuppAnal9TEXT_P.Text Then blnBOChanged = True
            PopulateBOProperty(State.MyBO_P, SUPPLIERANALYSIS9_PROPERTY, moSuppAnal9TEXT_P)

            If .IsNew AndAlso .SupplierAnalysisCode10 <> moSuppAnal10TEXT_P.Text Then blnBOChanged = True
            PopulateBOProperty(State.MyBO_P, SUPPLIERANALYSIS10_PROPERTY, moSuppAnal10TEXT_P)

            If .IsNew AndAlso .AddressLookupCode <> moAddressLookupCodeTEXT_P.Text Then blnBOChanged = True
            PopulateBOProperty(State.MyBO_P, ADDRESSLOOKUPCODE_PROPERTY, moAddressLookupCodeTEXT_P)

            If .IsNew AndAlso .AddressSequenceNumber <> moAddressSequenceNumberTEXT_P.Text Then blnBOChanged = True
            PopulateBOProperty(State.MyBO_P, ADDRESSSEQUENCENUMBER_PROPERTY, moAddressSequenceNumberTEXT_P)

            If .IsNew AndAlso .SupplierLookupCode <> moSupplierLookUpCodeTEXT_P.Text Then blnBOChanged = True
            PopulateBOProperty(State.MyBO_P, SUPPLIERLOOKUPCODE_PROPERTY, moSupplierLookUpCodeTEXT_P)

            If .IsNew AndAlso .UserArea <> moUserAreaTEXT_P.Text Then blnBOChanged = True
            PopulateBOProperty(State.MyBO_P, USER_AREA_PROPERTY, moUserAreaTEXT_P)

            If .IsNew AndAlso .Description <> moDescriptionTEXT_P.Text Then blnBOChanged = True
            PopulateBOProperty(State.MyBO_P, DESCRIPTION_PROPERTY, moDescriptionTEXT_P)

            If .IsNew AndAlso .DefaultBankSubCode <> moDefaultBankSubCodeTEXT_P.Text Then blnBOChanged = True
            PopulateBOProperty(State.MyBO_P, DEFAULT_BANK_SUBCODE_PROPERTY, moDefaultBankSubCodeTEXT_P)

            guidItem = .PaymentTermsId
            PopulateBOProperty(State.MyBO_P, PAYMENT_TERMS_ID_PROPERTY, cboPaymentTerms_P)
            If .IsNew AndAlso .PaymentTermsId <> guidItem Then blnBOChanged = True

            strObj = .ConversionCodeControl
            PopulateBOProperty(State.MyBO_P, CONVERSIONCODECONTROL_PROPERTY, LookupListNew.GetCodeFromId(LookupListNew.DropdownLookupList(LookupListNew.LK_CONVCTL, langId, True), New Guid(cboConversionCodeControl_P.SelectedItem.Value)))
            If .IsNew AndAlso .ConversionCodeControl <> strObj Then blnBOChanged = True

            strObj = .ReportConversionControl
            PopulateBOProperty(State.MyBO_P, REPORTCONVERSIONCONTROL_PROPERTY, LookupListNew.GetCodeFromId(LookupListNew.DropdownLookupList(LookupListNew.LK_YESNO_NUM, langId, True), New Guid(cboReportConversionControl_P.SelectedItem.Value)))
            If .IsNew AndAlso .ReportConversionControl <> strObj Then blnBOChanged = True

            strObj = .PaymentMethod
            PopulateBOProperty(State.MyBO_P, PAYMENTMETHOD_PROPERTY, LookupListNew.GetCodeFromId(LookupListNew.DropdownLookupList(LookupListNew.LK_PMTHD_NUM, langId, True), New Guid(cboPaymentMethod_P.SelectedItem.Value)))
            If .IsNew AndAlso .PaymentMethod <> strObj Then blnBOChanged = True

            strObj = .PayAsPaidAccountType
            PopulateBOProperty(State.MyBO_P, PAYASPAIDACCOUNTTYPE_PROPERTY, LookupListNew.GetCodeFromId(LookupListNew.DropdownLookupList(LookupListNew.LK_PAPAT, langId, True), New Guid(cboPayAsPaidAccountType_P.SelectedItem.Value)))
            If .IsNew AndAlso .PayAsPaidAccountType <> strObj Then blnBOChanged = True

            strObj = .AddressStatus
            PopulateBOProperty(State.MyBO_P, ADDRESSSTATUS_PROPERTY, LookupListNew.GetCodeFromId(vendStdv, New Guid(cboAddressStatus_P.SelectedItem.Value)))
            If .IsNew AndAlso .AddressStatus <> strObj Then blnBOChanged = True

            strObj = .SupplierStatus
            PopulateBOProperty(State.MyBO_P, SUPPLIERSTATUS_PROPERTY, LookupListNew.GetCodeFromId(vendStdv, New Guid(cboSupplierStatus_P.SelectedItem.Value)))
            If .IsNew AndAlso .SupplierStatus <> strObj Then blnBOChanged = True

            strObj = .AccountAnalysis1
            PopulateBOProperty(State.MyBO_P, ACCOUNT_ANALYSIS_FLAG1_PROPERTY, LookupListNew.GetCodeFromId(AnalCodedv, New Guid(cboAcctAnal1_P.SelectedItem.Value)))
            If .IsNew AndAlso .AccountAnalysis1 <> strObj Then blnBOChanged = True

            strObj = .AccountAnalysis2
            PopulateBOProperty(State.MyBO_P, ACCOUNT_ANALYSIS_FLAG2_PROPERTY, LookupListNew.GetCodeFromId(AnalCodedv, New Guid(cboAcctAnal2_P.SelectedItem.Value)))
            If .IsNew AndAlso .AccountAnalysis2 <> strObj Then blnBOChanged = True

            strObj = .AccountAnalysis3
            PopulateBOProperty(State.MyBO_P, ACCOUNT_ANALYSIS_FLAG3_PROPERTY, LookupListNew.GetCodeFromId(AnalCodedv, New Guid(cboAcctAnal3_P.SelectedItem.Value)))
            If .IsNew AndAlso .AccountAnalysis3 <> strObj Then blnBOChanged = True

            strObj = .AccountAnalysis4
            PopulateBOProperty(State.MyBO_P, ACCOUNT_ANALYSIS_FLAG4_PROPERTY, LookupListNew.GetCodeFromId(AnalCodedv, New Guid(cboAcctAnal4_P.SelectedItem.Value)))
            If .IsNew AndAlso .AccountAnalysis4 <> strObj Then blnBOChanged = True

            strObj = .AccountAnalysis5
            PopulateBOProperty(State.MyBO_P, ACCOUNT_ANALYSIS_FLAG5_PROPERTY, LookupListNew.GetCodeFromId(AnalCodedv, New Guid(cboAcctAnal5_P.SelectedItem.Value)))
            If .IsNew AndAlso .AccountAnalysis5 <> strObj Then blnBOChanged = True

            strObj = .AccountAnalysis6
            PopulateBOProperty(State.MyBO_P, ACCOUNT_ANALYSIS_FLAG6_PROPERTY, LookupListNew.GetCodeFromId(AnalCodedv, New Guid(cboAcctAnal6_P.SelectedItem.Value)))
            If .IsNew AndAlso .AccountAnalysis6 <> strObj Then blnBOChanged = True

            strObj = .AccountAnalysis7
            PopulateBOProperty(State.MyBO_P, ACCOUNT_ANALYSIS_FLAG7_PROPERTY, LookupListNew.GetCodeFromId(AnalCodedv, New Guid(cboAcctAnal7_P.SelectedItem.Value)))
            If .IsNew AndAlso .AccountAnalysis7 <> strObj Then blnBOChanged = True

            strObj = .AccountAnalysis8
            PopulateBOProperty(State.MyBO_P, ACCOUNT_ANALYSIS_FLAG8_PROPERTY, LookupListNew.GetCodeFromId(AnalCodedv, New Guid(cboAcctAnal8_P.SelectedItem.Value)))
            If .IsNew AndAlso .AccountAnalysis8 <> strObj Then blnBOChanged = True

            strObj = .AccountAnalysis9
            PopulateBOProperty(State.MyBO_P, ACCOUNT_ANALYSIS_FLAG9_PROPERTY, LookupListNew.GetCodeFromId(AnalCodedv, New Guid(cboAcctAnal9_P.SelectedItem.Value)))
            If .IsNew AndAlso .AccountAnalysis9 <> strObj Then blnBOChanged = True

            strObj = .AccountAnalysis10
            PopulateBOProperty(State.MyBO_P, ACCOUNT_ANALYSIS_FLAG10_PROPERTY, LookupListNew.GetCodeFromId(AnalCodedv, New Guid(cboAcctAnal10_P.SelectedItem.Value)))
            If .IsNew AndAlso .AccountAnalysis10 <> strObj Then blnBOChanged = True
        End With

        If blnBOChanged Then State.IsNewBOPDirty = (True OrElse State.IsNewBOPDirty)
        moBankInfo.PopulateBOFromControl(True, False)

        If ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If
    End Sub

    Private Sub SetDefaults()
        Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
        Dim AnalCodedv As DataView = LookupListNew.DropdownLookupList(LookupListNew.LK_ANYLSCODE, langId, True)
        Dim i As Integer, oGuidOptional As Guid
        'set analysis code to optional for new record
        If State.ObjStatus_P = ObjectStatus.isNew OrElse State.ObjStatus_R = ObjectStatus.isNew Then
            For i = 0 To AnalCodedv.Count - 1
                If AnalCodedv(i)(LookupListNew.COL_CODE_NAME).ToString = ACCT_ANALYSIS_OPTIONAL Then
                    oGuidOptional = New Guid(CType(AnalCodedv(i)(LookupListNew.COL_ID_NAME), Byte()))
                    Exit For
                End If
            Next

            If State.ObjStatus_R = ObjectStatus.isNew Then
                SetSelectedItem(cboAcctAnal1, oGuidOptional)
                SetSelectedItem(cboAcctAnal2, oGuidOptional)
                SetSelectedItem(cboAcctAnal3, oGuidOptional)
                SetSelectedItem(cboAcctAnal4, oGuidOptional)
                SetSelectedItem(cboAcctAnal5, oGuidOptional)
                SetSelectedItem(cboAcctAnal6, oGuidOptional)
                SetSelectedItem(cboAcctAnal7, oGuidOptional)
                SetSelectedItem(cboAcctAnal8, oGuidOptional)
                SetSelectedItem(cboAcctAnal9, oGuidOptional)
                SetSelectedItem(cboAcctAnal10, oGuidOptional)
                With State.MyBO_R
                    .AccountAnalysis1 = ACCT_ANALYSIS_OPTIONAL
                    .AccountAnalysis2 = ACCT_ANALYSIS_OPTIONAL
                    .AccountAnalysis3 = ACCT_ANALYSIS_OPTIONAL
                    .AccountAnalysis4 = ACCT_ANALYSIS_OPTIONAL
                    .AccountAnalysis5 = ACCT_ANALYSIS_OPTIONAL
                    .AccountAnalysis6 = ACCT_ANALYSIS_OPTIONAL
                    .AccountAnalysis7 = ACCT_ANALYSIS_OPTIONAL
                    .AccountAnalysis8 = ACCT_ANALYSIS_OPTIONAL
                    .AccountAnalysis9 = ACCT_ANALYSIS_OPTIONAL
                    .AccountAnalysis10 = ACCT_ANALYSIS_OPTIONAL
                End With
            End If

            If State.ObjStatus_P = ObjectStatus.isNew Then
                SetSelectedItem(cboAcctAnal1_P, oGuidOptional)
                SetSelectedItem(cboAcctAnal2_P, oGuidOptional)
                SetSelectedItem(cboAcctAnal3_P, oGuidOptional)
                SetSelectedItem(cboAcctAnal4_P, oGuidOptional)
                SetSelectedItem(cboAcctAnal5_P, oGuidOptional)
                SetSelectedItem(cboAcctAnal6_P, oGuidOptional)
                SetSelectedItem(cboAcctAnal7_P, oGuidOptional)
                SetSelectedItem(cboAcctAnal8_P, oGuidOptional)
                SetSelectedItem(cboAcctAnal9_P, oGuidOptional)
                SetSelectedItem(cboAcctAnal10_P, oGuidOptional)
                With State.MyBO_P
                    .AccountAnalysis1 = ACCT_ANALYSIS_OPTIONAL
                    .AccountAnalysis2 = ACCT_ANALYSIS_OPTIONAL
                    .AccountAnalysis3 = ACCT_ANALYSIS_OPTIONAL
                    .AccountAnalysis4 = ACCT_ANALYSIS_OPTIONAL
                    .AccountAnalysis5 = ACCT_ANALYSIS_OPTIONAL
                    .AccountAnalysis6 = ACCT_ANALYSIS_OPTIONAL
                    .AccountAnalysis7 = ACCT_ANALYSIS_OPTIONAL
                    .AccountAnalysis8 = ACCT_ANALYSIS_OPTIONAL
                    .AccountAnalysis9 = ACCT_ANALYSIS_OPTIONAL
                    .AccountAnalysis10 = ACCT_ANALYSIS_OPTIONAL
                End With
            End If
        End If
    End Sub

    Private Sub PopulateAll()

        Try

            Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            'Dim AnalCodedv As DataView = LookupListNew.DropdownLookupList(LookupListNew.LK_ANYLSCODE, langId, True)
            'Dim vendStdv As DataView = LookupListNew.DropdownLookupList(LookupListNew.LK_VENSTAT, langId, True)

            Dim AnalCodedv As DataElements.ListItem() =
                                    CommonConfigManager.Current.ListManager.GetList(listCode:="ANYLSCODE",
                                                                languageCode:=ElitaPlusIdentity.Current.ActiveUser.LanguageCode)

            Dim vendStdv As DataElements.ListItem() =
                                    CommonConfigManager.Current.ListManager.GetList(listCode:="VENSTAT",
                                                                languageCode:=ElitaPlusIdentity.Current.ActiveUser.LanguageCode)

            Dim conVctdv As DataElements.ListItem() =
                                    CommonConfigManager.Current.ListManager.GetList(listCode:="CONVCTL",
                                                                languageCode:=ElitaPlusIdentity.Current.ActiveUser.LanguageCode)

            Dim yesNumdv As DataElements.ListItem() =
                                    CommonConfigManager.Current.ListManager.GetList(listCode:="YESNO_NUM",
                                                                languageCode:=ElitaPlusIdentity.Current.ActiveUser.LanguageCode)

            Dim pytmMthddv As DataElements.ListItem() =
                                    CommonConfigManager.Current.ListManager.GetList(listCode:="PMTHD_NUM",
                                                                languageCode:=ElitaPlusIdentity.Current.ActiveUser.LanguageCode)

            Dim payasPddv As DataElements.ListItem() =
                                    CommonConfigManager.Current.ListManager.GetList(listCode:="PAPAT",
                                                                languageCode:=ElitaPlusIdentity.Current.ActiveUser.LanguageCode)

            Dim pmtTrmdv As DataElements.ListItem() =
                                    CommonConfigManager.Current.ListManager.GetList(listCode:="PMTTRM",
                                                                languageCode:=ElitaPlusIdentity.Current.ActiveUser.LanguageCode)

            Dim acctPmtMthddv As DataElements.ListItem() =
                                    CommonConfigManager.Current.ListManager.GetList(listCode:="ACCTPMTMETH",
                                                                languageCode:=ElitaPlusIdentity.Current.ActiveUser.LanguageCode)

            Dim populate_options As PopulateOptions = New PopulateOptions() With
                {
                    .AddBlankItem = True
                }

            PopulateAcctCompanies()
            If Not State.TargetStatus = ObjectStatus.isExisting Then
                PopulateTargetList()
            End If

            'Receivable tab
            'Me.BindListControlToDataView(Me.cboConversionCodeControl, LookupListNew.DropdownLookupList(LookupListNew.LK_CONVCTL, langId, True))
            cboConversionCodeControl.Populate(conVctdv, populate_options)
            'Me.BindListControlToDataView(Me.cboReportConversionControl, LookupListNew.DropdownLookupList(LookupListNew.LK_YESNO_NUM, langId, True))
            cboReportConversionControl.Populate(yesNumdv, populate_options)
            'Me.BindListControlToDataView(Me.cboPaymentMethod, LookupListNew.DropdownLookupList(LookupListNew.LK_PMTHD_NUM, langId, True))
            cboPaymentMethod.Populate(pytmMthddv, populate_options)
            'Me.BindListControlToDataView(Me.cboSupplierStatus, vendStdv)
            cboSupplierStatus.Populate(vendStdv, populate_options)
            'Me.BindListControlToDataView(Me.cboPayAsPaidAccountType, LookupListNew.DropdownLookupList(LookupListNew.LK_PAPAT, langId, True))
            cboPayAsPaidAccountType.Populate(payasPddv, populate_options)
            'Me.BindListControlToDataView(Me.cboAddressStatus, vendStdv)
            cboAddressStatus.Populate(vendStdv, populate_options)

            'Me.BindListControlToDataView(cboAcctAnal1, AnalCodedv)
            cboAcctAnal1.Populate(AnalCodedv, populate_options)
            'Me.BindListControlToDataView(cboAcctAnal2, AnalCodedv)
            cboAcctAnal2.Populate(AnalCodedv, populate_options)
            'Me.BindListControlToDataView(cboAcctAnal3, AnalCodedv)
            cboAcctAnal3.Populate(AnalCodedv, populate_options)
            'Me.BindListControlToDataView(cboAcctAnal4, AnalCodedv)
            cboAcctAnal4.Populate(AnalCodedv, populate_options)
            'Me.BindListControlToDataView(cboAcctAnal5, AnalCodedv)
            cboAcctAnal5.Populate(AnalCodedv, populate_options)
            'Me.BindListControlToDataView(cboAcctAnal6, AnalCodedv)
            cboAcctAnal6.Populate(AnalCodedv, populate_options)
            'Me.BindListControlToDataView(cboAcctAnal7, AnalCodedv)
            cboAcctAnal7.Populate(AnalCodedv, populate_options)
            'Me.BindListControlToDataView(cboAcctAnal8, AnalCodedv)
            cboAcctAnal8.Populate(AnalCodedv, populate_options)
            'Me.BindListControlToDataView(cboAcctAnal9, AnalCodedv)
            cboAcctAnal9.Populate(AnalCodedv, populate_options)
            'Me.BindListControlToDataView(cboAcctAnal10, AnalCodedv)
            cboAcctAnal10.Populate(AnalCodedv, populate_options)

            'Me.BindListControlToDataView(Me.cboPaymentTerms, LookupListNew.DropdownLookupList(LookupListNew.LK_PAYMENT_TERMS, langId, True))
            cboPaymentTerms.Populate(pmtTrmdv, populate_options)

            'Payable tab
            'Me.BindListControlToDataView(Me.cboConversionCodeControl_P, LookupListNew.DropdownLookupList(LookupListNew.LK_CONVCTL, langId, True))
            cboConversionCodeControl_P.Populate(conVctdv, populate_options)
            'Me.BindListControlToDataView(Me.cboReportConversionControl_P, LookupListNew.DropdownLookupList(LookupListNew.LK_YESNO_NUM, langId, True))
            cboReportConversionControl_P.Populate(yesNumdv, populate_options)
            'Me.BindListControlToDataView(Me.cboPaymentMethod_P, LookupListNew.DropdownLookupList(LookupListNew.LK_PMTHD_NUM, langId, True))
            cboPaymentMethod_P.Populate(pytmMthddv, populate_options)
            'Me.BindListControlToDataView(Me.cboSuppAnal1_P, LookupListNew.DropdownLookupList(LookupListNew.LK_ACCOUNTING_PAYMENT_METHOD, langId, True))
            cboSuppAnal1_P.Populate(acctPmtMthddv, populate_options)

            'Me.BindListControlToDataView(Me.cboSupplierStatus_P, vendStdv)
            cboSupplierStatus_P.Populate(vendStdv, populate_options)
            'Me.BindListControlToDataView(Me.cboPayAsPaidAccountType_P, LookupListNew.DropdownLookupList(LookupListNew.LK_PAPAT, langId, True))
            cboPayAsPaidAccountType_P.Populate(payasPddv, populate_options)
            'Me.BindListControlToDataView(Me.cboAddressStatus_P, vendStdv)
            cboAddressStatus_P.Populate(vendStdv, populate_options)

            'Me.BindListControlToDataView(cboAcctAnal1_P, AnalCodedv)
            cboAcctAnal1_P.Populate(AnalCodedv, populate_options)
            'Me.BindListControlToDataView(cboAcctAnal2_P, AnalCodedv)
            cboAcctAnal2_P.Populate(AnalCodedv, populate_options)
            'Me.BindListControlToDataView(cboAcctAnal3_P, AnalCodedv)
            cboAcctAnal3_P.Populate(AnalCodedv, populate_options)
            'Me.BindListControlToDataView(cboAcctAnal4_P, AnalCodedv)
            cboAcctAnal4_P.Populate(AnalCodedv, populate_options)
            'Me.BindListControlToDataView(cboAcctAnal5_P, AnalCodedv)
            cboAcctAnal5_P.Populate(AnalCodedv, populate_options)
            'Me.BindListControlToDataView(cboAcctAnal6_P, AnalCodedv)
            cboAcctAnal6_P.Populate(AnalCodedv, populate_options)
            'Me.BindListControlToDataView(cboAcctAnal7_P, AnalCodedv)
            cboAcctAnal7_P.Populate(AnalCodedv, populate_options)
            'Me.BindListControlToDataView(cboAcctAnal8_P, AnalCodedv)
            cboAcctAnal8_P.Populate(AnalCodedv, populate_options)
            'Me.BindListControlToDataView(cboAcctAnal9_P, AnalCodedv)
            cboAcctAnal9_P.Populate(AnalCodedv, populate_options)
            'Me.BindListControlToDataView(cboAcctAnal10_P, AnalCodedv)
            cboAcctAnal10_P.Populate(AnalCodedv, populate_options)

            'Me.BindListControlToDataView(Me.cboPaymentTerms_P, LookupListNew.DropdownLookupList(LookupListNew.LK_PAYMENT_TERMS, langId, True))
            cboPaymentTerms_P.Populate(pmtTrmdv, populate_options)

            'Populate the javascript variables for the analysis codes
            'Dim currFilter As String = AnalCodedv.RowFilter
            'AnalCodedv.RowFilter = currFilter + Microsoft.VisualBasic.IIf(currFilter.Trim.Length > 0, " and ", "").ToString + " code = '" & Me.ACCT_ANALYSIS_MANDATORY & "'"
            'If AnalCodedv.Count = 1 Then
            '    Me.litScriptVars.Text += "var mand = '" + GuidControl.ByteArrayToGuid(AnalCodedv.Item(0)(LookupListNew.COL_ID_NAME)).ToString + "';"
            'End If

            'AnalCodedv.RowFilter = currFilter + Microsoft.VisualBasic.IIf(currFilter.Trim.Length > 0, " and ", "").ToString + "code = '" & Me.ACCT_ANALYSIS_PROHIBIT & "'"
            'If AnalCodedv.Count = 1 Then
            '    Me.litScriptVars.Text += "var prohib = '" + GuidControl.ByteArrayToGuid(AnalCodedv.Item(0)(LookupListNew.COL_ID_NAME)).ToString + "';"
            'End If

            'AnalCodedv.RowFilter = currFilter

            AnalCodedv = (From lst In AnalCodedv
                          Where lst.Code = ACCT_ANALYSIS_MANDATORY
                          Select lst).ToArray()

            If AnalCodedv.Count = 1 Then
                litScriptVars.Text += "var prohib = '" + AnalCodedv.First().ListItemId.ToString + "';"
            End If

            AnalCodedv = (From lst In AnalCodedv
                          Where lst.Code = ACCT_ANALYSIS_PROHIBIT
                          Select lst).ToArray()

            If AnalCodedv.Count = 1 Then
                litScriptVars.Text += "var prohib = '" + AnalCodedv.First().ListItemId.ToString + "';"
            End If

        Catch ex As Exception
            ErrControllerMaster.AddError(ACCTSETT_LIST_FORM001)
            ErrControllerMaster.AddError(ex.Message, False)
            ErrControllerMaster.Show()
        End Try

    End Sub

    Public Shared Sub SetLabelColor(lbl As Label)
        lbl.ForeColor = Color.Black
    End Sub

#End Region

End Class
