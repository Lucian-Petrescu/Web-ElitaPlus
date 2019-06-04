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

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
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
        Public Sub New(ByVal ObjectType As TargetType, ByVal hasDataChanged As Boolean)
            Me.ObjectType = ObjectType
            Me.HasDataChanged = hasDataChanged
        End Sub

        Public Sub New(ByVal Id As Guid, ByVal ObjectType As TargetType, ByVal ObjectId As Guid, _
                    ByVal ObjectTitle As String, ByVal hasDataChanged As Boolean, _
                    Optional ByVal otherParam As String = "")
            Me.Id = Id
            Me.ObjectType = ObjectType
            Me.ObjectId = ObjectId
            Me.HasDataChanged = hasDataChanged
            Me.ObjectTitle = ObjectTitle
            Me.addParam = otherParam
        End Sub
        Public Sub New(ByVal LastOp As DetailPageCommand, ByVal curEditingBo As AcctSetting, ByVal hasDataChanged As Boolean)
            Me.LastOperation = LastOp
            Me.EditingBo = curEditingBo
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

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim Params As ReturnType
        Me.ErrControllerMaster.Clear_Hide()
        Try
            If Not Me.IsPostBack Then

                Me.SetFormTab(Me.PAGETAB)
                Me.SetFormTitle(Me.PAGETITLE)


                Me.AddConfirmation(Me.btnDelete_WRITE, Message.DELETE_RECORD_PROMPT)
                If Me.State.MyBO_R Is Nothing OrElse Me.State.MyBO_P Is Nothing Then
                    CreateNew()
                Else
                    PopulateAll()
                    Me.PopulateFormFromBOs()
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
                Me.MenuEnabled = False
                ExecuteEvents()

                TabContainer1.Tabs(0).HeaderText = TranslationBase.TranslateLabelOrMessage("ACCOUNT_RECEIVABLES")
                TabContainer1.Tabs(1).HeaderText = TranslationBase.TranslateLabelOrMessage("ACCOUNT_PAYABLES")
                TabContainer1.Tabs(2).HeaderText = TranslationBase.TranslateLabelOrMessage("BANK_INFO")
                SetTargetControls()
            End If
            SetVisibleAttributes()
            BindBoPropertiesToLabels()
            CheckIfComingFromSaveConfirm()
            If Not Me.IsPostBack Then
                Me.AddLabelDecorations(Me.State.MyBO_R)
                Me.AddLabelDecorations(Me.State.MyBO_P)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
        Me.ShowMissingTranslations(Me.ErrControllerMaster)
    End Sub

    Private Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        Try
            Select Case Me.State.objectType
                Case ReturnType.TargetType.Branch ' Branch
                    If ddlTargetist.Visible Then ddlTargetist.AutoPostBack = True
            End Select
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
        Try
            If Not Me.CallingParameters Is Nothing Then
                Dim Params As ReturnType
                Params = CType(Me.CallingParameters, ReturnType)
                Me.State.objectType = Params.ObjectType
                State.OrigAcctSettingsId = CType(Params.Id, Guid)
                If Not (State.OrigAcctSettingsId.Equals(Guid.Empty)) Then
                    LoadObjects(State.OrigAcctSettingsId)
                    Me.State.accountName = Server.HtmlDecode(Params.ObjectTitle)
                    Me.State.branchName = Server.HtmlDecode(Params.addParam)
                End If
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try

    End Sub

 #End Region

#Region "Button Clicks"
    Private Sub btnSave_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave_WRITE.Click
        Try
            Dim blnChanged As Boolean = False
            If cboCompany.Visible = False Then
                If moCompId.Equals(Guid.Empty) Then
                    'no account companies found for the user
                    Me.ErrControllerMaster.AddErrorAndShow(ElitaPlus.Common.ErrorCodes.GUI_ACCOUNTING_COMPANIES_NOT_FOUND_ERR)
                    Exit Sub
                End If
            Else
                Dim selectedCompId As Guid = Me.GetSelectedItem(Me.cboCompany)
                If selectedCompId.Equals(Guid.Empty) Then
                    ElitaPlusPage.SetLabelError(moCompanyLABEL)
                    Me.ErrControllerMaster.AddErrorAndShow(ElitaPlus.Common.ErrorCodes.GUI_SELECT_ACCOUNTING_COMPANY_ERR)
                    Exit Sub
                End If
            End If

            If Me.State.TargetStatus = ObjectStatus.isNew OrElse Me.State.TargetStatus = ObjectStatus.isACopy Then
                Dim guidSelectedId As Guid
                Select Case Me.State.objectType
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
                        Dim selectedBranchId As Guid = Me.GetSelectedItem(Me.moBranchNameDROP)
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
            Me.PopulateBOsFromForm()

            blnChanged = SaveObjects()

            If blnChanged Then
                Me.State.HasDataChanged = True
                SetVisibleAttributes()
                EnableDisableFields()
                'Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                AddInfoMsgWithDelay(Message.SAVE_RECORD_CONFIRMATION)
                If ddlTargetist.Items.Count > 0 Then SetSelectedItem(ddlTargetist, NOTHING_SELECTED)
                State.TargetStatus = ObjectStatus.isExisting
                SetVisibleAttributes()
                Me.PopulateFormFromBOs()
            Else
                'Me.AddInfoMsg(Message.MSG_RECORD_NOT_SAVED)
                AddInfoMsgWithDelay(Message.MSG_RECORD_NOT_SAVED)
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Private Sub btnDelete_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete_WRITE.Click
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

            Me.State.HasDataChanged = True
            Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Delete, Nothing, Me.State.HasDataChanged))
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            Me.PopulateBOsFromForm()
            If IsBODirty() Then
                Me.DisplayMessageWithDelay(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
            Else
                Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.GetOrigObj, Me.State.HasDataChanged))
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
            Me.DisplayMessageWithDelay(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
            Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
            Me.State.LastErrMsg = Me.ErrControllerMaster.Text
        End Try
    End Sub

    Private Sub btnNew_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew_WRITE.Click
        Try
            Me.PopulateBOsFromForm()
            If (IsBODirty()) Then
                Me.DisplayMessageWithDelay(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
            Else
                Me.State.TargetStatus = ObjectStatus.isNew
                Me.CreateNew()
                TabContainer1.ActiveTab = TabPanel1
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Private Sub btnCopy_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopy_WRITE.Click
        Try
            Me.PopulateBOsFromForm()
            If (IsBODirty()) Then
                Me.DisplayMessageWithDelay(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
            Else
                State.TargetStatus = ObjectStatus.isACopy
                Me.CreateNewWithCopy()
                TabContainer1.ActiveTab = TabPanel1
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Private Sub btnUndo_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUndo_WRITE.Click
        Try
            If State.TargetStatus = ObjectStatus.isExisting Then
                If State.ObjStatus_R = ObjectStatus.isExisting Then
                    LoadObjects(State.MyBO_R.Id, True)
                ElseIf True Then
                    LoadObjects(State.MyBO_P.Id, True)
                End If
            ElseIf Not State.ScreenSnapShotBO_R Is Nothing Then
                State.MyBO_R.Clone(State.ScreenSnapShotBO_R)
                State.MyBO_P.Clone(State.ScreenSnapShotBO_P)
                If State.TargetId <> Guid.Empty Then
                    PopulateBankInfo(State.TargetId, True)
                End If
            End If
                PopulateAll()
                Me.PopulateFormFromBOs()
                EnableDisableFields()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

#End Region

#Region "Handler Dropdown"

    Protected Sub moDealerNameDROP_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlTargetist.SelectedIndexChanged
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
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Protected Sub moBranchNameDROP_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles moBranchNameDROP.SelectedIndexChanged
        Try
            State.TargetId = GetSelectedItem(moBranchNameDROP)
            Me.State.branchName = GetSelectedDescription(moBranchNameDROP)
            PopulateBankInfo(State.TargetId)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
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

        Select Case Me.State.objectType
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
        Me.RegisterStartupScript("ToggleBranchScript", sJavaScript.ToString)

        'Add script for Felita / SmartStream fields
        If ElitaPlusIdentity.Current.ActiveUser.AccountingCompanies.Count > 0 Then
            Me.ACCT_SYSTEM = LookupListNew.GetCodeFromId(LookupListNew.DropdownLookupList(LookupListNew.LK_ACCT_SYSTEM, ElitaPlusIdentity.Current.ActiveUser.LanguageId), ElitaPlusIdentity.Current.ActiveUser.AccountingCompanies(0).AcctSystemId)
            Me.litScriptVars.Text += String.Format("var ACCT_SYSTEM_ID = '{0}';SetAcctSystem();", Me.ACCT_SYSTEM)
        End If

        If Not Me.ACCT_SYSTEM.Equals(FelitaEngine.FELITA_PREFIX) Then
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
            If (Not blnDirty) AndAlso (Not .MyBO_R Is Nothing) AndAlso (Not .MyBO_R.IsNew) Then
                If .MyBO_R.IsDirty Then blnDirty = True
            End If
            If (Not blnDirty) AndAlso (Not .MyBO_P Is Nothing) AndAlso (Not .MyBO_P.IsNew) Then
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

    Private Sub UpdateObjWithBankInfoId(ByVal guidBankId As Guid)
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

    Private Sub LoadObjects(ByVal guidID As Guid, Optional ByVal blnBindBankInfo As Boolean = False)
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
        Me.PopulateBOsFromForm()

        State.TargetStatus = ObjectStatus.isACopy
        State.TargetId = Guid.Empty

        Dim newObj As New AcctSetting
        If Not State.MyBO_R.IsNew Then
            newObj.Copy(Me.State.MyBO_R)
            Me.State.MyBO_R = newObj
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
            newObj.Copy(Me.State.MyBO_P)
            Me.State.MyBO_P = newObj
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
        Me.PopulateFormFromBOs()
        Me.EnableDisableFields()

        'create the backup copy
        Me.State.ScreenSnapShotBO_R = New AcctSetting
        Me.State.ScreenSnapShotBO_R.Clone(Me.State.MyBO_R)
        Me.State.ScreenSnapShotBO_P = New AcctSetting
        Me.State.ScreenSnapShotBO_P.Clone(Me.State.MyBO_P)

        SetVisibleAttributes()
        SetDefaults()
    End Sub

    Protected Sub CreateNew()
        State.TargetStatus = ObjectStatus.isNew
        State.TargetId = Guid.Empty

        'Reset the backup copy
        Me.State.ScreenSnapShotBO_R = Nothing
        Me.State.ScreenSnapShotBO_P = Nothing
        Me.State.MyBO_R = New AcctSetting
        State.MyBO_R.AccountType = ACCOUNT_TYPE_RECEIVABLE
        Me.State.MyBO_P = New AcctSetting
        State.MyBO_P.AccountType = ACCOUNT_TYPE_PAYABLE
        State.ObjStatus_R = ObjectStatus.isNew
        State.ObjStatus_P = ObjectStatus.isNew

        EnableDisableFields()

        PopulateAll()
        Me.PopulateFormFromBOs()
        SetVisibleAttributes()
        SetDefaults()
    End Sub

    Protected Sub CheckIfComingFromSaveConfirm()
        Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value
        Dim objRet As AcctSetting = State.MyBO_R
        If State.MyBO_P.Id = State.OrigAcctSettingsId Then
            objRet = State.MyBO_P
        End If

        If Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_YES Then
            If Me.State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr Then
                Try
                    SaveObjects()
                Catch ex As Exception
                    Me.HandleErrors(ex, Me.ErrControllerMaster)
                End Try
            End If
            Select Case Me.State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, objRet, Me.State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.New_
                    'Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                    AddInfoMsgWithDelay(Message.SAVE_RECORD_CONFIRMATION)
                    Me.CreateNew()
                Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                    'Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                    AddInfoMsgWithDelay(Message.SAVE_RECORD_CONFIRMATION)
                    Me.CreateNewWithCopy()
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    Me.ReturnToCallingPage(New ReturnType(Me.State.ActionInProgress, objRet, Me.State.HasDataChanged))
            End Select
        ElseIf Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_NO Then
            Select Case Me.State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, objRet, Me.State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.New_
                    Me.CreateNew()
                Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                    Me.CreateNewWithCopy()
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    Me.ErrControllerMaster.AddErrorAndShow(Me.State.LastErrMsg)
            End Select
        End If
        'Clean after consuming the action
        Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
        Me.HiddenSaveChangesPromptResponse.Value = String.Empty
    End Sub
#End Region

#Region "POPULATE"
    Private Sub PopulateBankInfo(ByVal objID As Guid, Optional ByVal blnBind As Boolean = True)
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
                        If Not List2 Is Nothing Then
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
                        If Not List3 Is Nothing Then
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
                        If Not List4 Is Nothing Then
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
        Dim oDealerId As Guid = GetSelectedItem(Me.ddlTargetist) 'GetSelectedItem(Me.moDealerNameDROP)
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
        Me.ClearList(moBranchNameDROP)
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
            If Not UserAcctCompanyList Is Nothing Then
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
            Me.cboCompany.Populate(UserAcctCompanyList.ToArray(),
                                New PopulateOptions() With
                                {
                                    .AddBlankItem = True
                                })
            Me.cboCompany_P.Populate(UserAcctCompanyList.ToArray(),
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

        Me.BindBOPropertyToLabel(Me.State.MyBO_R, ACCOUNTCODE_PROPERTY, Me.moAccountCodeLABEL)
        Me.BindBOPropertyToLabel(Me.State.MyBO_R, ACCTCOMPANYID_PROPERTY, Me.moCompanyLABEL)
        Me.BindBOPropertyToLabel(Me.State.MyBO_R, BALANCETYPE_PROPERTY, Me.moBalanceTypeLABEL)
        Me.BindBOPropertyToLabel(Me.State.MyBO_R, CONVERSIONCODECONTROL_PROPERTY, Me.moConversionCodeControlLABEL)
        Me.BindBOPropertyToLabel(Me.State.MyBO_R, REPORTCONVERSIONCONTROL_PROPERTY, Me.moReportConversionControlLABEL)
        Me.BindBOPropertyToLabel(Me.State.MyBO_R, DATAACCESSGROUPCODE_PROPERTY, Me.moDataAccessGroupCodeLABEL)
        Me.BindBOPropertyToLabel(Me.State.MyBO_R, DEFAULTCURRENCYCODE_PROPERTY, Me.moDefaultCurrencyCodeLABEL)
        Me.BindBOPropertyToLabel(Me.State.MyBO_R, SUPPRESSREVALUATION_PROPERTY, Me.moSuppressReevaluationLABEL)
        Me.BindBOPropertyToLabel(Me.State.MyBO_R, PAYASPAIDACCOUNTTYPE_PROPERTY, Me.moPayAsPaidAccountTypeLABEL)
        Me.BindBOPropertyToLabel(Me.State.MyBO_R, SUPPLIERLOOKUPCODE_PROPERTY, Me.moSupplierLookUpCodeLABEL)
        Me.BindBOPropertyToLabel(Me.State.MyBO_R, PAYMENTMETHOD_PROPERTY, Me.moPaymentMethodLABEL)
        Me.BindBOPropertyToLabel(Me.State.MyBO_R, SUPPLIERSTATUS_PROPERTY, Me.moSupplierStatusLABEL)
        Me.BindBOPropertyToLabel(Me.State.MyBO_R, ACCOUNTANALYSIS_A_1_PROPERTY, Me.moAcctAnal1LABEL)
        Me.BindBOPropertyToLabel(Me.State.MyBO_R, ACCOUNTANALYSIS_A_2_PROPERTY, Me.moAcctAnal2LABEL)
        Me.BindBOPropertyToLabel(Me.State.MyBO_R, ACCOUNTANALYSIS_A_3_PROPERTY, Me.moAcctAnal3LABEL)
        Me.BindBOPropertyToLabel(Me.State.MyBO_R, ACCOUNTANALYSIS_A_4_PROPERTY, Me.moAcctAnal4LABEL)
        Me.BindBOPropertyToLabel(Me.State.MyBO_R, ACCOUNTANALYSIS_A_5_PROPERTY, Me.moAcctAnal5LABEL)
        Me.BindBOPropertyToLabel(Me.State.MyBO_R, ACCOUNTANALYSIS_A_6_PROPERTY, Me.moAcctAnal6LABEL)
        Me.BindBOPropertyToLabel(Me.State.MyBO_R, ACCOUNTANALYSIS_A_7_PROPERTY, Me.moAcctAnal7LABEL)
        Me.BindBOPropertyToLabel(Me.State.MyBO_R, ACCOUNTANALYSIS_A_8_PROPERTY, Me.moAcctAnal8LABEL)
        Me.BindBOPropertyToLabel(Me.State.MyBO_R, ACCOUNTANALYSIS_A_9_PROPERTY, Me.moAcctAnal9LABEL)
        Me.BindBOPropertyToLabel(Me.State.MyBO_R, ACCOUNTANALYSIS_A_10_PROPERTY, Me.moAcctAnal10LABEL)
        Me.BindBOPropertyToLabel(Me.State.MyBO_R, SUPPLIERANALYSIS1_PROPERTY, Me.moSuppAnal1LABEL)
        Me.BindBOPropertyToLabel(Me.State.MyBO_R, SUPPLIERANALYSIS2_PROPERTY, Me.moSuppAnal2LABEL)
        Me.BindBOPropertyToLabel(Me.State.MyBO_R, SUPPLIERANALYSIS3_PROPERTY, Me.moSuppAnal3LABEL)
        Me.BindBOPropertyToLabel(Me.State.MyBO_R, SUPPLIERANALYSIS4_PROPERTY, Me.moSuppAnal4LABEL)
        Me.BindBOPropertyToLabel(Me.State.MyBO_R, SUPPLIERANALYSIS5_PROPERTY, Me.moSuppAnal5LABEL)
        Me.BindBOPropertyToLabel(Me.State.MyBO_R, SUPPLIERANALYSIS6_PROPERTY, Me.moSuppAnal6LABEL)
        Me.BindBOPropertyToLabel(Me.State.MyBO_R, SUPPLIERANALYSIS7_PROPERTY, Me.moSuppAnal7LABEL)
        Me.BindBOPropertyToLabel(Me.State.MyBO_R, SUPPLIERANALYSIS8_PROPERTY, Me.moSuppAnal8LABEL)
        Me.BindBOPropertyToLabel(Me.State.MyBO_R, SUPPLIERANALYSIS9_PROPERTY, Me.moSuppAnal9LABEL)
        Me.BindBOPropertyToLabel(Me.State.MyBO_R, SUPPLIERANALYSIS10_PROPERTY, Me.moSuppAnal10LABEL)
        Me.BindBOPropertyToLabel(Me.State.MyBO_R, ADDRESSLOOKUPCODE_PROPERTY, Me.moAddressLookupCodeLABEL)
        Me.BindBOPropertyToLabel(Me.State.MyBO_R, ADDRESSSEQUENCENUMBER_PROPERTY, Me.moAddressSequenceNumberLABEL)
        Me.BindBOPropertyToLabel(Me.State.MyBO_R, ADDRESSSTATUS_PROPERTY, Me.moAddressStatusLABEL)
        Me.BindBOPropertyToLabel(Me.State.MyBO_R, ACCOUNTANALYSIS1_PROPERTY, Me.moAcctAnalT1LABEL)
        Me.BindBOPropertyToLabel(Me.State.MyBO_R, ACCOUNTANALYSIS2_PROPERTY, Me.moAcctAnalT2LABEL)
        Me.BindBOPropertyToLabel(Me.State.MyBO_R, ACCOUNTANALYSIS3_PROPERTY, Me.moAcctAnalT3LABEL)
        Me.BindBOPropertyToLabel(Me.State.MyBO_R, ACCOUNTANALYSIS4_PROPERTY, Me.moAcctAnalT4LABEL)
        Me.BindBOPropertyToLabel(Me.State.MyBO_R, ACCOUNTANALYSIS5_PROPERTY, Me.moAcctAnalT5LABEL)
        Me.BindBOPropertyToLabel(Me.State.MyBO_R, ACCOUNTANALYSIS6_PROPERTY, Me.moAcctAnalT6LABEL)
        Me.BindBOPropertyToLabel(Me.State.MyBO_R, ACCOUNTANALYSIS7_PROPERTY, Me.moAcctAnalT7LABEL)
        Me.BindBOPropertyToLabel(Me.State.MyBO_R, ACCOUNTANALYSIS8_PROPERTY, Me.moAcctAnalT8LABEL)
        Me.BindBOPropertyToLabel(Me.State.MyBO_R, ACCOUNTANALYSIS9_PROPERTY, Me.moAcctAnalT9LABEL)
        Me.BindBOPropertyToLabel(Me.State.MyBO_R, ACCOUNTANALYSIS10_PROPERTY, Me.moAcctAnalT10LABEL)
        Me.BindBOPropertyToLabel(Me.State.MyBO_R, PAYMENT_TERMS_ID_PROPERTY, Me.moPaymentTermsLABEL)
        Me.BindBOPropertyToLabel(Me.State.MyBO_R, USER_AREA_PROPERTY, Me.moUserAreaLABEL)
        Me.BindBOPropertyToLabel(Me.State.MyBO_R, DESCRIPTION_PROPERTY, Me.moDescriptionLABEL)

        'Account payable controls
        Me.BindBOPropertyToLabel(Me.State.MyBO_P, ACCOUNTCODE_PROPERTY, Me.moAccountCodeLABEL_P)
        Me.BindBOPropertyToLabel(Me.State.MyBO_P, ACCTCOMPANYID_PROPERTY, Me.moCompanyLABEL_P)
        Me.BindBOPropertyToLabel(Me.State.MyBO_P, BALANCETYPE_PROPERTY, Me.moBalanceTypeLABEL_P)
        Me.BindBOPropertyToLabel(Me.State.MyBO_P, CONVERSIONCODECONTROL_PROPERTY, Me.moConversionCodeControlLABEL_P)
        Me.BindBOPropertyToLabel(Me.State.MyBO_P, REPORTCONVERSIONCONTROL_PROPERTY, Me.moReportConversionControlLABEL_P)
        Me.BindBOPropertyToLabel(Me.State.MyBO_P, DATAACCESSGROUPCODE_PROPERTY, Me.moDataAccessGroupCodeLABEL_P)
        Me.BindBOPropertyToLabel(Me.State.MyBO_P, DEFAULTCURRENCYCODE_PROPERTY, Me.moDefaultCurrencyCodeLABEL_P)
        Me.BindBOPropertyToLabel(Me.State.MyBO_P, SUPPRESSREVALUATION_PROPERTY, Me.moSuppressReevaluationLABEL_P)
        Me.BindBOPropertyToLabel(Me.State.MyBO_P, PAYASPAIDACCOUNTTYPE_PROPERTY, Me.moPayAsPaidAccountTypeLABEL_P)
        Me.BindBOPropertyToLabel(Me.State.MyBO_P, SUPPLIERLOOKUPCODE_PROPERTY, Me.moSupplierLookUpCodeLABEL_P)
        Me.BindBOPropertyToLabel(Me.State.MyBO_P, PAYMENTMETHOD_PROPERTY, Me.moPaymentMethodLABEL_P)
        Me.BindBOPropertyToLabel(Me.State.MyBO_P, SUPPLIERSTATUS_PROPERTY, Me.moSupplierStatusLABEL_P)
        Me.BindBOPropertyToLabel(Me.State.MyBO_P, ACCOUNTANALYSIS_A_1_PROPERTY, Me.moAcctAnal1LABEL_P)
        Me.BindBOPropertyToLabel(Me.State.MyBO_P, ACCOUNTANALYSIS_A_2_PROPERTY, Me.moAcctAnal2LABEL_P)
        Me.BindBOPropertyToLabel(Me.State.MyBO_P, ACCOUNTANALYSIS_A_3_PROPERTY, Me.moAcctAnal3LABEL_P)
        Me.BindBOPropertyToLabel(Me.State.MyBO_P, ACCOUNTANALYSIS_A_4_PROPERTY, Me.moAcctAnal4LABEL_P)
        Me.BindBOPropertyToLabel(Me.State.MyBO_P, ACCOUNTANALYSIS_A_5_PROPERTY, Me.moAcctAnal5LABEL_P)
        Me.BindBOPropertyToLabel(Me.State.MyBO_P, ACCOUNTANALYSIS_A_6_PROPERTY, Me.moAcctAnal6LABEL_P)
        Me.BindBOPropertyToLabel(Me.State.MyBO_P, ACCOUNTANALYSIS_A_7_PROPERTY, Me.moAcctAnal7LABEL_P)
        Me.BindBOPropertyToLabel(Me.State.MyBO_P, ACCOUNTANALYSIS_A_8_PROPERTY, Me.moAcctAnal8LABEL_P)
        Me.BindBOPropertyToLabel(Me.State.MyBO_P, ACCOUNTANALYSIS_A_9_PROPERTY, Me.moAcctAnal9LABEL_P)
        Me.BindBOPropertyToLabel(Me.State.MyBO_P, ACCOUNTANALYSIS_A_10_PROPERTY, Me.moAcctAnal10LABEL_P)
        Me.BindBOPropertyToLabel(Me.State.MyBO_P, SUPPLIERANALYSIS1_PROPERTY, Me.moSuppAnal1LABEL_P)
        Me.BindBOPropertyToLabel(Me.State.MyBO_P, SUPPLIERANALYSIS2_PROPERTY, Me.moSuppAnal2LABEL_P)
        Me.BindBOPropertyToLabel(Me.State.MyBO_P, SUPPLIERANALYSIS3_PROPERTY, Me.moSuppAnal3LABEL_P)
        Me.BindBOPropertyToLabel(Me.State.MyBO_P, SUPPLIERANALYSIS4_PROPERTY, Me.moSuppAnal4LABEL_P)
        Me.BindBOPropertyToLabel(Me.State.MyBO_P, SUPPLIERANALYSIS5_PROPERTY, Me.moSuppAnal5LABEL_P)
        Me.BindBOPropertyToLabel(Me.State.MyBO_P, SUPPLIERANALYSIS6_PROPERTY, Me.moSuppAnal6LABEL_P)
        Me.BindBOPropertyToLabel(Me.State.MyBO_P, SUPPLIERANALYSIS7_PROPERTY, Me.moSuppAnal7LABEL_P)
        Me.BindBOPropertyToLabel(Me.State.MyBO_P, SUPPLIERANALYSIS8_PROPERTY, Me.moSuppAnal8LABEL_P)
        Me.BindBOPropertyToLabel(Me.State.MyBO_P, SUPPLIERANALYSIS9_PROPERTY, Me.moSuppAnal9LABEL_P)
        Me.BindBOPropertyToLabel(Me.State.MyBO_P, SUPPLIERANALYSIS10_PROPERTY, Me.moSuppAnal10LABEL_P)
        Me.BindBOPropertyToLabel(Me.State.MyBO_P, ADDRESSLOOKUPCODE_PROPERTY, Me.moAddressLookupCodeLABEL_P)
        Me.BindBOPropertyToLabel(Me.State.MyBO_P, ADDRESSSEQUENCENUMBER_PROPERTY, Me.moAddressSequenceNumberLABEL_P)
        Me.BindBOPropertyToLabel(Me.State.MyBO_P, ADDRESSSTATUS_PROPERTY, Me.moAddressStatusLABEL_P)

        Me.BindBOPropertyToLabel(Me.State.MyBO_P, ACCOUNTANALYSIS1_PROPERTY, Me.moAcctAnalT1LABEL_P)
        Me.BindBOPropertyToLabel(Me.State.MyBO_P, ACCOUNTANALYSIS2_PROPERTY, Me.moAcctAnalT2LABEL_P)
        Me.BindBOPropertyToLabel(Me.State.MyBO_P, ACCOUNTANALYSIS3_PROPERTY, Me.moAcctAnalT3LABEL_P)
        Me.BindBOPropertyToLabel(Me.State.MyBO_P, ACCOUNTANALYSIS4_PROPERTY, Me.moAcctAnalT4LABEL_P)
        Me.BindBOPropertyToLabel(Me.State.MyBO_P, ACCOUNTANALYSIS5_PROPERTY, Me.moAcctAnalT5LABEL_P)
        Me.BindBOPropertyToLabel(Me.State.MyBO_P, ACCOUNTANALYSIS6_PROPERTY, Me.moAcctAnalT6LABEL_P)
        Me.BindBOPropertyToLabel(Me.State.MyBO_P, ACCOUNTANALYSIS7_PROPERTY, Me.moAcctAnalT7LABEL_P)
        Me.BindBOPropertyToLabel(Me.State.MyBO_P, ACCOUNTANALYSIS8_PROPERTY, Me.moAcctAnalT8LABEL_P)
        Me.BindBOPropertyToLabel(Me.State.MyBO_P, ACCOUNTANALYSIS9_PROPERTY, Me.moAcctAnalT9LABEL_P)
        Me.BindBOPropertyToLabel(Me.State.MyBO_P, ACCOUNTANALYSIS10_PROPERTY, Me.moAcctAnalT10LABEL_P)
        Me.BindBOPropertyToLabel(Me.State.MyBO_P, PAYMENT_TERMS_ID_PROPERTY, Me.moPaymentTermsLABEL_P)
        Me.BindBOPropertyToLabel(Me.State.MyBO_P, USER_AREA_PROPERTY, Me.moUserAreaLABEL_P)
        Me.BindBOPropertyToLabel(Me.State.MyBO_P, DESCRIPTION_PROPERTY, Me.moDescriptionLABEL_P)
        Me.BindBOPropertyToLabel(Me.State.MyBO_P, DEFAULT_BANK_SUBCODE_PROPERTY, Me.moDefaultBankSubCodeLABEL_P)


        Select Case State.objectType
            Case ReturnType.TargetType.DealerGroup
                Me.BindBOPropertyToLabel(Me.State.MyBO_R, "DealerGroupId", Me.lblTargetName)
            Case ReturnType.TargetType.Dealer
                Me.BindBOPropertyToLabel(Me.State.MyBO_R, "DealerId", Me.lblTargetName)
            Case ReturnType.TargetType.ServiceCenter
                Me.BindBOPropertyToLabel(Me.State.MyBO_R, "ServiceCenterId", Me.lblTargetName)
            Case ReturnType.TargetType.Branch
                Me.BindBOPropertyToLabel(Me.State.MyBO_R, "BranchId", Me.lblTargetName)
            Case ReturnType.TargetType.CommissionEntity
                Me.BindBOPropertyToLabel(Me.State.MyBO_R, "CommissionEntityId", Me.lblTargetName)
        End Select

        Me.ClearGridHeadersAndLabelsErrSign()

    End Sub

    Protected Sub PopulateFormFromBOs()
        'Fill lists
        Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
        Dim AnalCodedv As DataView = LookupListNew.DropdownLookupList(LookupListNew.LK_ANYLSCODE, langId, True)
        Dim vendStdv As DataView = LookupListNew.DropdownLookupList(LookupListNew.LK_VENSTAT, langId, True)
        moAccountNameTEXT.Text = Me.State.accountName
        Select Case Me.State.objectType
            Case ReturnType.TargetType.Branch ' Branch
                moBranchNameTEXT.Text = Me.State.branchName
        End Select

        With Me.State.MyBO_R
            Me.PopulateControlFromBOProperty(Me.moAccountCodeTEXT, .AccountCode)
            Me.PopulateControlFromBOProperty(Me.moBalanceTypeTEXT, .BalanceType)
            Me.PopulateControlFromBOProperty(Me.moDataAccessGroupCodeTEXT, .DataAccessGroupCode)
            Me.PopulateControlFromBOProperty(Me.moDefaultCurrencyCodeTEXT, .DefaultCurrencyCode)
            Me.PopulateControlFromBOProperty(Me.moSuppressReevaluationTEXT, .SuppressRevaluation)
            Me.PopulateControlFromBOProperty(Me.moAddressLookupCodeTEXT, .AddressLookupCode)
            Me.PopulateControlFromBOProperty(Me.moAddressSequenceNumberTEXT, .AddressSequenceNumber)
            Me.PopulateControlFromBOProperty(Me.moSupplierLookUpCodeTEXT, .SupplierLookupCode)

            Me.PopulateControlFromBOProperty(Me.moAcctAnal1TEXT, .AccountAnalysisACode1)
            Me.PopulateControlFromBOProperty(Me.moAcctAnal2TEXT, .AccountAnalysisACode2)
            Me.PopulateControlFromBOProperty(Me.moAcctAnal3TEXT, .AccountAnalysisACode3)
            Me.PopulateControlFromBOProperty(Me.moAcctAnal4TEXT, .AccountAnalysisACode4)
            Me.PopulateControlFromBOProperty(Me.moAcctAnal5TEXT, .AccountAnalysisACode5)
            Me.PopulateControlFromBOProperty(Me.moAcctAnal6TEXT, .AccountAnalysisACode6)
            Me.PopulateControlFromBOProperty(Me.moAcctAnal7TEXT, .AccountAnalysisACode7)
            Me.PopulateControlFromBOProperty(Me.moAcctAnal8TEXT, .AccountAnalysisACode8)
            Me.PopulateControlFromBOProperty(Me.moAcctAnal9TEXT, .AccountAnalysisACode9)
            Me.PopulateControlFromBOProperty(Me.moAcctAnal10TEXT, .AccountAnalysisACode10)

            Me.PopulateControlFromBOProperty(Me.moSuppAnal1TEXT, .SupplierAnalysisCode1)
            Me.PopulateControlFromBOProperty(Me.moSuppAnal2TEXT, .SupplierAnalysisCode2)
            Me.PopulateControlFromBOProperty(Me.moSuppAnal3TEXT, .SupplierAnalysisCode3)
            Me.PopulateControlFromBOProperty(Me.moSuppAnal4TEXT, .SupplierAnalysisCode4)
            Me.PopulateControlFromBOProperty(Me.moSuppAnal5TEXT, .SupplierAnalysisCode5)
            Me.PopulateControlFromBOProperty(Me.moSuppAnal6TEXT, .SupplierAnalysisCode6)
            Me.PopulateControlFromBOProperty(Me.moSuppAnal7TEXT, .SupplierAnalysisCode7)
            Me.PopulateControlFromBOProperty(Me.moSuppAnal8TEXT, .SupplierAnalysisCode8)
            Me.PopulateControlFromBOProperty(Me.moSuppAnal9TEXT, .SupplierAnalysisCode9)
            Me.PopulateControlFromBOProperty(Me.moSuppAnal10TEXT, .SupplierAnalysisCode10)

            Me.PopulateControlFromBOProperty(Me.moAcctAnalT1TEXT, .AccountAnalysisCode1)
            Me.PopulateControlFromBOProperty(Me.moAcctAnalT2TEXT, .AccountAnalysisCode2)
            Me.PopulateControlFromBOProperty(Me.moAcctAnalT3TEXT, .AccountAnalysisCode3)
            Me.PopulateControlFromBOProperty(Me.moAcctAnalT4TEXT, .AccountAnalysisCode4)
            Me.PopulateControlFromBOProperty(Me.moAcctAnalT5TEXT, .AccountAnalysisCode5)
            Me.PopulateControlFromBOProperty(Me.moAcctAnalT6TEXT, .AccountAnalysisCode6)
            Me.PopulateControlFromBOProperty(Me.moAcctAnalT7TEXT, .AccountAnalysisCode7)
            Me.PopulateControlFromBOProperty(Me.moAcctAnalT8TEXT, .AccountAnalysisCode8)
            Me.PopulateControlFromBOProperty(Me.moAcctAnalT9TEXT, .AccountAnalysisCode9)
            Me.PopulateControlFromBOProperty(Me.moAcctAnalT10TEXT, .AccountAnalysisCode10)

            Me.PopulateControlFromBOProperty(Me.moUserAreaTEXT, .UserArea)
            Me.PopulateControlFromBOProperty(Me.moDescriptionTEXT, .Description)

            Me.PopulateControlFromBOProperty(Me.cboAddressStatus, BusinessObjectsNew.LookupListNew.GetIdFromCode(vendStdv, .AddressStatus))
            Me.PopulateControlFromBOProperty(Me.cboSupplierStatus, BusinessObjectsNew.LookupListNew.GetIdFromCode(vendStdv, .SupplierStatus))

            Me.PopulateControlFromBOProperty(Me.cboConversionCodeControl, BusinessObjectsNew.LookupListNew.GetIdFromCode(LookupListNew.DropdownLookupList(LookupListNew.LK_CONVCTL, langId, True), .ConversionCodeControl))
            Me.PopulateControlFromBOProperty(Me.cboReportConversionControl, BusinessObjectsNew.LookupListNew.GetIdFromCode(LookupListNew.DropdownLookupList(LookupListNew.LK_YESNO_NUM, langId, True), .ReportConversionControl))
            Me.PopulateControlFromBOProperty(Me.cboPaymentMethod, BusinessObjectsNew.LookupListNew.GetIdFromCode(LookupListNew.DropdownLookupList(LookupListNew.LK_PMTHD_NUM, langId, True), .PaymentMethod))
            Me.PopulateControlFromBOProperty(Me.cboPayAsPaidAccountType, BusinessObjectsNew.LookupListNew.GetIdFromCode(LookupListNew.DropdownLookupList(LookupListNew.LK_PAPAT, langId, True), .PayAsPaidAccountType))

            Me.PopulateControlFromBOProperty(Me.cboPaymentTerms, .PaymentTermsId)

            If State.ObjStatus_R <> ObjectStatus.isNew Then
                Me.PopulateControlFromBOProperty(Me.cboAcctAnal1, BusinessObjectsNew.LookupListNew.GetIdFromCode(AnalCodedv, .AccountAnalysis1))
                Me.PopulateControlFromBOProperty(Me.cboAcctAnal2, BusinessObjectsNew.LookupListNew.GetIdFromCode(AnalCodedv, .AccountAnalysis2))
                Me.PopulateControlFromBOProperty(Me.cboAcctAnal3, BusinessObjectsNew.LookupListNew.GetIdFromCode(AnalCodedv, .AccountAnalysis3))
                Me.PopulateControlFromBOProperty(Me.cboAcctAnal4, BusinessObjectsNew.LookupListNew.GetIdFromCode(AnalCodedv, .AccountAnalysis4))
                Me.PopulateControlFromBOProperty(Me.cboAcctAnal5, BusinessObjectsNew.LookupListNew.GetIdFromCode(AnalCodedv, .AccountAnalysis5))
                Me.PopulateControlFromBOProperty(Me.cboAcctAnal6, BusinessObjectsNew.LookupListNew.GetIdFromCode(AnalCodedv, .AccountAnalysis6))
                Me.PopulateControlFromBOProperty(Me.cboAcctAnal7, BusinessObjectsNew.LookupListNew.GetIdFromCode(AnalCodedv, .AccountAnalysis7))
                Me.PopulateControlFromBOProperty(Me.cboAcctAnal8, BusinessObjectsNew.LookupListNew.GetIdFromCode(AnalCodedv, .AccountAnalysis8))
                Me.PopulateControlFromBOProperty(Me.cboAcctAnal9, BusinessObjectsNew.LookupListNew.GetIdFromCode(AnalCodedv, .AccountAnalysis9))
                Me.PopulateControlFromBOProperty(Me.cboAcctAnal10, BusinessObjectsNew.LookupListNew.GetIdFromCode(AnalCodedv, .AccountAnalysis10))
            End If

        End With

        With Me.State.MyBO_P
            Me.PopulateControlFromBOProperty(Me.moAccountCodeTEXT_P, .AccountCode)
            Me.PopulateControlFromBOProperty(Me.moBalanceTypeTEXT_P, .BalanceType)
            Me.PopulateControlFromBOProperty(Me.moDataAccessGroupCodeTEXT_P, .DataAccessGroupCode)
            Me.PopulateControlFromBOProperty(Me.moDefaultCurrencyCodeTEXT_P, .DefaultCurrencyCode)
            Me.PopulateControlFromBOProperty(Me.moSuppressReevaluationTEXT_P, .SuppressRevaluation)
            Me.PopulateControlFromBOProperty(Me.moAddressLookupCodeTEXT_P, .AddressLookupCode)
            Me.PopulateControlFromBOProperty(Me.moAddressSequenceNumberTEXT_P, .AddressSequenceNumber)
            Me.PopulateControlFromBOProperty(Me.moSupplierLookUpCodeTEXT_P, .SupplierLookupCode)

            Me.PopulateControlFromBOProperty(Me.moAcctAnal1TEXT_P, .AccountAnalysisACode1)
            Me.PopulateControlFromBOProperty(Me.moAcctAnal2TEXT_P, .AccountAnalysisACode2)
            Me.PopulateControlFromBOProperty(Me.moAcctAnal3TEXT_P, .AccountAnalysisACode3)
            Me.PopulateControlFromBOProperty(Me.moAcctAnal4TEXT_P, .AccountAnalysisACode4)
            Me.PopulateControlFromBOProperty(Me.moAcctAnal5TEXT_P, .AccountAnalysisACode5)
            Me.PopulateControlFromBOProperty(Me.moAcctAnal6TEXT_P, .AccountAnalysisACode6)
            Me.PopulateControlFromBOProperty(Me.moAcctAnal7TEXT_P, .AccountAnalysisACode7)
            Me.PopulateControlFromBOProperty(Me.moAcctAnal8TEXT_P, .AccountAnalysisACode8)
            Me.PopulateControlFromBOProperty(Me.moAcctAnal9TEXT_P, .AccountAnalysisACode9)
            Me.PopulateControlFromBOProperty(Me.moAcctAnal10TEXT_P, .AccountAnalysisACode10)

            Me.PopulateControlFromBOProperty(Me.cboSuppAnal1_P, BusinessObjectsNew.LookupListNew.GetIdFromCode(LookupListNew.DropdownLookupList(LookupListNew.LK_ACCOUNTING_PAYMENT_METHOD, langId, True), .SupplierAnalysisCode1))
            Me.PopulateControlFromBOProperty(Me.moSuppAnal2TEXT_P, .SupplierAnalysisCode2)
            Me.PopulateControlFromBOProperty(Me.moSuppAnal3TEXT_P, .SupplierAnalysisCode3)
            Me.PopulateControlFromBOProperty(Me.moSuppAnal4TEXT_P, .SupplierAnalysisCode4)
            Me.PopulateControlFromBOProperty(Me.moSuppAnal5TEXT_P, .SupplierAnalysisCode5)
            Me.PopulateControlFromBOProperty(Me.moSuppAnal6TEXT_P, .SupplierAnalysisCode6)
            Me.PopulateControlFromBOProperty(Me.moSuppAnal7TEXT_P, .SupplierAnalysisCode7)
            Me.PopulateControlFromBOProperty(Me.moSuppAnal8TEXT_P, .SupplierAnalysisCode8)
            Me.PopulateControlFromBOProperty(Me.moSuppAnal9TEXT_P, .SupplierAnalysisCode9)
            Me.PopulateControlFromBOProperty(Me.moSuppAnal10TEXT_P, .SupplierAnalysisCode10)
            Me.PopulateControlFromBOProperty(Me.moAcctAnalT1TEXT_P, .AccountAnalysisCode1)
            Me.PopulateControlFromBOProperty(Me.moAcctAnalT2TEXT_P, .AccountAnalysisCode2)
            Me.PopulateControlFromBOProperty(Me.moAcctAnalT3TEXT_P, .AccountAnalysisCode3)
            Me.PopulateControlFromBOProperty(Me.moAcctAnalT4TEXT_P, .AccountAnalysisCode4)
            Me.PopulateControlFromBOProperty(Me.moAcctAnalT5TEXT_P, .AccountAnalysisCode5)
            Me.PopulateControlFromBOProperty(Me.moAcctAnalT6TEXT_P, .AccountAnalysisCode6)
            Me.PopulateControlFromBOProperty(Me.moAcctAnalT7TEXT_P, .AccountAnalysisCode7)
            Me.PopulateControlFromBOProperty(Me.moAcctAnalT8TEXT_P, .AccountAnalysisCode8)
            Me.PopulateControlFromBOProperty(Me.moAcctAnalT9TEXT_P, .AccountAnalysisCode9)
            Me.PopulateControlFromBOProperty(Me.moAcctAnalT10TEXT_P, .AccountAnalysisCode10)
            Me.PopulateControlFromBOProperty(Me.moUserAreaTEXT_P, .UserArea)
            Me.PopulateControlFromBOProperty(Me.moDescriptionTEXT_P, .Description)
            Me.PopulateControlFromBOProperty(Me.moDefaultBankSubCodeTEXT_P, .DefaultBankSubCode)
            Me.PopulateControlFromBOProperty(Me.cboAddressStatus_P, BusinessObjectsNew.LookupListNew.GetIdFromCode(vendStdv, .AddressStatus))
            Me.PopulateControlFromBOProperty(Me.cboSupplierStatus_P, BusinessObjectsNew.LookupListNew.GetIdFromCode(vendStdv, .SupplierStatus))
            Me.PopulateControlFromBOProperty(Me.cboConversionCodeControl_P, BusinessObjectsNew.LookupListNew.GetIdFromCode(LookupListNew.DropdownLookupList(LookupListNew.LK_CONVCTL, langId, True), .ConversionCodeControl))
            Me.PopulateControlFromBOProperty(Me.cboReportConversionControl_P, BusinessObjectsNew.LookupListNew.GetIdFromCode(LookupListNew.DropdownLookupList(LookupListNew.LK_YESNO_NUM, langId, True), .ReportConversionControl))
            Me.PopulateControlFromBOProperty(Me.cboPaymentMethod_P, BusinessObjectsNew.LookupListNew.GetIdFromCode(LookupListNew.DropdownLookupList(LookupListNew.LK_PMTHD_NUM, langId, True), .PaymentMethod))
            Me.PopulateControlFromBOProperty(Me.cboPayAsPaidAccountType_P, BusinessObjectsNew.LookupListNew.GetIdFromCode(LookupListNew.DropdownLookupList(LookupListNew.LK_PAPAT, langId, True), .PayAsPaidAccountType))
            Me.PopulateControlFromBOProperty(Me.cboPaymentTerms_P, .PaymentTermsId)

            If State.ObjStatus_P <> ObjectStatus.isNew Then
                Me.PopulateControlFromBOProperty(Me.cboAcctAnal1_P, BusinessObjectsNew.LookupListNew.GetIdFromCode(AnalCodedv, .AccountAnalysis1))
                Me.PopulateControlFromBOProperty(Me.cboAcctAnal2_P, BusinessObjectsNew.LookupListNew.GetIdFromCode(AnalCodedv, .AccountAnalysis2))
                Me.PopulateControlFromBOProperty(Me.cboAcctAnal3_P, BusinessObjectsNew.LookupListNew.GetIdFromCode(AnalCodedv, .AccountAnalysis3))
                Me.PopulateControlFromBOProperty(Me.cboAcctAnal4_P, BusinessObjectsNew.LookupListNew.GetIdFromCode(AnalCodedv, .AccountAnalysis4))
                Me.PopulateControlFromBOProperty(Me.cboAcctAnal5_P, BusinessObjectsNew.LookupListNew.GetIdFromCode(AnalCodedv, .AccountAnalysis5))
                Me.PopulateControlFromBOProperty(Me.cboAcctAnal6_P, BusinessObjectsNew.LookupListNew.GetIdFromCode(AnalCodedv, .AccountAnalysis6))
                Me.PopulateControlFromBOProperty(Me.cboAcctAnal7_P, BusinessObjectsNew.LookupListNew.GetIdFromCode(AnalCodedv, .AccountAnalysis7))
                Me.PopulateControlFromBOProperty(Me.cboAcctAnal8_P, BusinessObjectsNew.LookupListNew.GetIdFromCode(AnalCodedv, .AccountAnalysis8))
                Me.PopulateControlFromBOProperty(Me.cboAcctAnal9_P, BusinessObjectsNew.LookupListNew.GetIdFromCode(AnalCodedv, .AccountAnalysis9))
                Me.PopulateControlFromBOProperty(Me.cboAcctAnal10_P, BusinessObjectsNew.LookupListNew.GetIdFromCode(AnalCodedv, .AccountAnalysis10))
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
            Select Case Me.State.objectType
                Case ReturnType.TargetType.DealerGroup
                    'dealer group
                    Me.PopulateBOProperty(Me.State.MyBO_R, Me.DEALER_GROUP_PROPERTY, State.TargetId)
                    Me.PopulateBOProperty(Me.State.MyBO_P, Me.DEALER_GROUP_PROPERTY, State.TargetId)
                Case ReturnType.TargetType.Dealer
                    ' Dealer
                    Me.PopulateBOProperty(Me.State.MyBO_R, Me.DEALER_PROPERTY, State.TargetId)
                    Me.PopulateBOProperty(Me.State.MyBO_P, Me.DEALER_PROPERTY, State.TargetId)
                Case ReturnType.TargetType.ServiceCenter
                    'ServiceCenter
                    Me.PopulateBOProperty(Me.State.MyBO_R, Me.SERVICE_CENTER_PROPERTY, State.TargetId)
                    Me.PopulateBOProperty(Me.State.MyBO_P, Me.SERVICE_CENTER_PROPERTY, State.TargetId)
                Case ReturnType.TargetType.Branch
                    'Branch
                    Me.PopulateBOProperty(Me.State.MyBO_R, Me.DEALER_PROPERTY, State.BranchDealerId)
                    Me.PopulateBOProperty(Me.State.MyBO_P, Me.DEALER_PROPERTY, State.BranchDealerId)
                    'Dim selectedBranchId As Guid = Me.GetSelectedItem(Me.moBranchNameDROP)
                    Me.PopulateBOProperty(Me.State.MyBO_R, Me.BRANCH_ID_PROPERTY, State.TargetId)
                    Me.PopulateBOProperty(Me.State.MyBO_P, Me.BRANCH_ID_PROPERTY, State.TargetId)
                    'Me.State.branchName = Me.GetSelectedDescription(moBranchNameDROP)
                Case ReturnType.TargetType.CommissionEntity
                    Me.PopulateBOProperty(Me.State.MyBO_R, Me.COMMISSION_ENTITY_ID_PROPERTY, State.TargetId)
                    Me.PopulateBOProperty(Me.State.MyBO_P, Me.COMMISSION_ENTITY_ID_PROPERTY, State.TargetId)
            End Select
        End If

        'Fill lists
        Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
        Dim AnalCodedv As DataView = LookupListNew.DropdownLookupList(LookupListNew.LK_ANYLSCODE, langId, True)
        Dim vendStdv As DataView = LookupListNew.DropdownLookupList(LookupListNew.LK_VENSTAT, langId, True)
        Dim blnBOChanged As Boolean = False, guidItem As Guid, strObj As String

        With Me.State.MyBO_R
            If .IsNew AndAlso .AccountCode <> moAccountCodeTEXT.Text Then blnBOChanged = True
            Me.PopulateBOProperty(Me.State.MyBO_R, Me.ACCOUNTCODE_PROPERTY, Me.moAccountCodeTEXT)

            If cboCompany.Visible = False Then
                Me.PopulateBOProperty(Me.State.MyBO_R, Me.ACCTCOMPANYID_PROPERTY, moCompId)
            Else
                Dim selectedCompId As Guid = Me.GetSelectedItem(Me.cboCompany)
                If .IsNew AndAlso .AcctCompanyId <> selectedCompId Then blnBOChanged = True
                Me.PopulateBOProperty(Me.State.MyBO_R, Me.ACCTCOMPANYID_PROPERTY, selectedCompId)
            End If

            If .IsNew AndAlso .BalanceType <> moBalanceTypeTEXT.Text Then blnBOChanged = True
            Me.PopulateBOProperty(Me.State.MyBO_R, Me.BALANCETYPE_PROPERTY, Me.moBalanceTypeTEXT)

            If .IsNew AndAlso .DataAccessGroupCode <> moDataAccessGroupCodeTEXT.Text Then blnBOChanged = True
            Me.PopulateBOProperty(Me.State.MyBO_R, Me.DATAACCESSGROUPCODE_PROPERTY, Me.moDataAccessGroupCodeTEXT)

            If .IsNew AndAlso .DefaultCurrencyCode <> moDefaultCurrencyCodeTEXT.Text Then blnBOChanged = True
            Me.PopulateBOProperty(Me.State.MyBO_R, Me.DEFAULTCURRENCYCODE_PROPERTY, Me.moDefaultCurrencyCodeTEXT)

            If .IsNew AndAlso .SuppressRevaluation <> moSuppressReevaluationTEXT.Text Then blnBOChanged = True
            Me.PopulateBOProperty(Me.State.MyBO_R, Me.SUPPRESSREVALUATION_PROPERTY, Me.moSuppressReevaluationTEXT)

            If .IsNew AndAlso .AccountAnalysisACode1 <> moAcctAnal1TEXT.Text Then blnBOChanged = True
            Me.PopulateBOProperty(Me.State.MyBO_R, Me.ACCOUNTANALYSIS_A_1_PROPERTY, Me.moAcctAnal1TEXT)

            If .IsNew AndAlso .AccountAnalysisACode2 <> moAcctAnal2TEXT.Text Then blnBOChanged = True
            Me.PopulateBOProperty(Me.State.MyBO_R, Me.ACCOUNTANALYSIS_A_2_PROPERTY, Me.moAcctAnal2TEXT)

            If .IsNew AndAlso .AccountAnalysisACode3 <> moAcctAnal3TEXT.Text Then blnBOChanged = True
            Me.PopulateBOProperty(Me.State.MyBO_R, Me.ACCOUNTANALYSIS_A_3_PROPERTY, Me.moAcctAnal3TEXT)

            If .IsNew AndAlso .AccountAnalysisACode4 <> moAcctAnal4TEXT.Text Then blnBOChanged = True
            Me.PopulateBOProperty(Me.State.MyBO_R, Me.ACCOUNTANALYSIS_A_4_PROPERTY, Me.moAcctAnal4TEXT)

            If .IsNew AndAlso .AccountAnalysisACode5 <> moAcctAnal5TEXT.Text Then blnBOChanged = True
            Me.PopulateBOProperty(Me.State.MyBO_R, Me.ACCOUNTANALYSIS_A_5_PROPERTY, Me.moAcctAnal5TEXT)

            If .IsNew AndAlso .AccountAnalysisACode6 <> moAcctAnal6TEXT.Text Then blnBOChanged = True
            Me.PopulateBOProperty(Me.State.MyBO_R, Me.ACCOUNTANALYSIS_A_6_PROPERTY, Me.moAcctAnal6TEXT)

            If .IsNew AndAlso .AccountAnalysisACode7 <> moAcctAnal7TEXT.Text Then blnBOChanged = True
            Me.PopulateBOProperty(Me.State.MyBO_R, Me.ACCOUNTANALYSIS_A_7_PROPERTY, Me.moAcctAnal7TEXT)

            If .IsNew AndAlso .AccountAnalysisACode8 <> moAcctAnal8TEXT.Text Then blnBOChanged = True
            Me.PopulateBOProperty(Me.State.MyBO_R, Me.ACCOUNTANALYSIS_A_8_PROPERTY, Me.moAcctAnal8TEXT)

            If .IsNew AndAlso .AccountAnalysisACode9 <> moAcctAnal9TEXT.Text Then blnBOChanged = True
            Me.PopulateBOProperty(Me.State.MyBO_R, Me.ACCOUNTANALYSIS_A_9_PROPERTY, Me.moAcctAnal9TEXT)

            If .IsNew AndAlso .AccountAnalysisACode10 <> moAcctAnal10TEXT.Text Then blnBOChanged = True
            Me.PopulateBOProperty(Me.State.MyBO_R, Me.ACCOUNTANALYSIS_A_10_PROPERTY, Me.moAcctAnal10TEXT)

            If .IsNew AndAlso .AccountAnalysisCode1 <> moAcctAnalT1TEXT.Text Then blnBOChanged = True
            Me.PopulateBOProperty(Me.State.MyBO_R, Me.ACCOUNTANALYSIS1_PROPERTY, Me.moAcctAnalT1TEXT)

            If .IsNew AndAlso .AccountAnalysisCode2 <> moAcctAnalT2TEXT.Text Then blnBOChanged = True
            Me.PopulateBOProperty(Me.State.MyBO_R, Me.ACCOUNTANALYSIS2_PROPERTY, Me.moAcctAnalT2TEXT)

            If .IsNew AndAlso .AccountAnalysisCode3 <> moAcctAnalT3TEXT.Text Then blnBOChanged = True
            Me.PopulateBOProperty(Me.State.MyBO_R, Me.ACCOUNTANALYSIS3_PROPERTY, Me.moAcctAnalT3TEXT)

            If .IsNew AndAlso .AccountAnalysisCode4 <> moAcctAnalT4TEXT.Text Then blnBOChanged = True
            Me.PopulateBOProperty(Me.State.MyBO_R, Me.ACCOUNTANALYSIS4_PROPERTY, Me.moAcctAnalT4TEXT)

            If .IsNew AndAlso .AccountAnalysisCode5 <> moAcctAnalT5TEXT.Text Then blnBOChanged = True
            Me.PopulateBOProperty(Me.State.MyBO_R, Me.ACCOUNTANALYSIS5_PROPERTY, Me.moAcctAnalT5TEXT)

            If .IsNew AndAlso .AccountAnalysisCode6 <> moAcctAnalT6TEXT.Text Then blnBOChanged = True
            Me.PopulateBOProperty(Me.State.MyBO_R, Me.ACCOUNTANALYSIS6_PROPERTY, Me.moAcctAnalT6TEXT)

            If .IsNew AndAlso .AccountAnalysisCode7 <> moAcctAnalT7TEXT.Text Then blnBOChanged = True
            Me.PopulateBOProperty(Me.State.MyBO_R, Me.ACCOUNTANALYSIS7_PROPERTY, Me.moAcctAnalT7TEXT)

            If .IsNew AndAlso .AccountAnalysisCode8 <> moAcctAnalT8TEXT.Text Then blnBOChanged = True
            Me.PopulateBOProperty(Me.State.MyBO_R, Me.ACCOUNTANALYSIS8_PROPERTY, Me.moAcctAnalT8TEXT)

            If .IsNew AndAlso .AccountAnalysisCode9 <> moAcctAnalT9TEXT.Text Then blnBOChanged = True
            Me.PopulateBOProperty(Me.State.MyBO_R, Me.ACCOUNTANALYSIS9_PROPERTY, Me.moAcctAnalT9TEXT)

            If .IsNew AndAlso .AccountAnalysisCode10 <> moAcctAnalT10TEXT.Text Then blnBOChanged = True
            Me.PopulateBOProperty(Me.State.MyBO_R, Me.ACCOUNTANALYSIS10_PROPERTY, Me.moAcctAnalT10TEXT)

            If .IsNew AndAlso .SupplierAnalysisCode1 <> moSuppAnal1TEXT.Text Then blnBOChanged = True
            Me.PopulateBOProperty(Me.State.MyBO_R, Me.SUPPLIERANALYSIS1_PROPERTY, Me.moSuppAnal1TEXT)

            If .IsNew AndAlso .SupplierAnalysisCode2 <> moSuppAnal2TEXT.Text Then blnBOChanged = True
            Me.PopulateBOProperty(Me.State.MyBO_R, Me.SUPPLIERANALYSIS2_PROPERTY, Me.moSuppAnal2TEXT)

            If .IsNew AndAlso .SupplierAnalysisCode3 <> moSuppAnal3TEXT.Text Then blnBOChanged = True
            Me.PopulateBOProperty(Me.State.MyBO_R, Me.SUPPLIERANALYSIS3_PROPERTY, Me.moSuppAnal3TEXT)

            If .IsNew AndAlso .SupplierAnalysisCode4 <> moSuppAnal4TEXT.Text Then blnBOChanged = True
            Me.PopulateBOProperty(Me.State.MyBO_R, Me.SUPPLIERANALYSIS4_PROPERTY, Me.moSuppAnal4TEXT)

            If .IsNew AndAlso .SupplierAnalysisCode5 <> moSuppAnal5TEXT.Text Then blnBOChanged = True
            Me.PopulateBOProperty(Me.State.MyBO_R, Me.SUPPLIERANALYSIS5_PROPERTY, Me.moSuppAnal5TEXT)

            If .IsNew AndAlso .SupplierAnalysisCode6 <> moSuppAnal6TEXT.Text Then blnBOChanged = True
            Me.PopulateBOProperty(Me.State.MyBO_R, Me.SUPPLIERANALYSIS6_PROPERTY, Me.moSuppAnal6TEXT)

            If .IsNew AndAlso .SupplierAnalysisCode7 <> moSuppAnal7TEXT.Text Then blnBOChanged = True
            Me.PopulateBOProperty(Me.State.MyBO_R, Me.SUPPLIERANALYSIS7_PROPERTY, Me.moSuppAnal7TEXT)

            If .IsNew AndAlso .SupplierAnalysisCode8 <> moSuppAnal8TEXT.Text Then blnBOChanged = True
            Me.PopulateBOProperty(Me.State.MyBO_R, Me.SUPPLIERANALYSIS8_PROPERTY, Me.moSuppAnal8TEXT)

            If .IsNew AndAlso .SupplierAnalysisCode9 <> moSuppAnal9TEXT.Text Then blnBOChanged = True
            Me.PopulateBOProperty(Me.State.MyBO_R, Me.SUPPLIERANALYSIS9_PROPERTY, Me.moSuppAnal9TEXT)

            If .IsNew AndAlso .SupplierAnalysisCode10 <> moSuppAnal10TEXT.Text Then blnBOChanged = True
            Me.PopulateBOProperty(Me.State.MyBO_R, Me.SUPPLIERANALYSIS10_PROPERTY, Me.moSuppAnal10TEXT)

            If .IsNew AndAlso .AddressLookupCode <> moAddressLookupCodeTEXT.Text Then blnBOChanged = True
            Me.PopulateBOProperty(Me.State.MyBO_R, Me.ADDRESSLOOKUPCODE_PROPERTY, Me.moAddressLookupCodeTEXT)

            If .IsNew AndAlso .AddressSequenceNumber <> moAddressSequenceNumberTEXT.Text Then blnBOChanged = True
            Me.PopulateBOProperty(Me.State.MyBO_R, Me.ADDRESSSEQUENCENUMBER_PROPERTY, Me.moAddressSequenceNumberTEXT)

            If .IsNew AndAlso .SupplierLookupCode <> moSupplierLookUpCodeTEXT.Text Then blnBOChanged = True
            Me.PopulateBOProperty(Me.State.MyBO_R, Me.SUPPLIERLOOKUPCODE_PROPERTY, Me.moSupplierLookUpCodeTEXT)

            If .IsNew AndAlso .UserArea <> moUserAreaTEXT.Text Then blnBOChanged = True
            Me.PopulateBOProperty(Me.State.MyBO_R, Me.USER_AREA_PROPERTY, Me.moUserAreaTEXT)

            If .IsNew AndAlso .Description <> moDescriptionTEXT.Text Then blnBOChanged = True
            Me.PopulateBOProperty(Me.State.MyBO_R, Me.DESCRIPTION_PROPERTY, Me.moDescriptionTEXT)

            guidItem = .PaymentTermsId
            Me.PopulateBOProperty(Me.State.MyBO_R, Me.PAYMENT_TERMS_ID_PROPERTY, Me.cboPaymentTerms)
            If .IsNew AndAlso .PaymentTermsId <> guidItem Then blnBOChanged = True

            strObj = .ConversionCodeControl
            Me.PopulateBOProperty(Me.State.MyBO_R, Me.CONVERSIONCODECONTROL_PROPERTY, LookupListNew.GetCodeFromId(LookupListNew.DropdownLookupList(LookupListNew.LK_CONVCTL, langId, True), New Guid(Me.cboConversionCodeControl.SelectedItem.Value)))
            If .IsNew AndAlso .ConversionCodeControl <> strObj Then blnBOChanged = True

            strObj = .ReportConversionControl
            Me.PopulateBOProperty(Me.State.MyBO_R, Me.REPORTCONVERSIONCONTROL_PROPERTY, LookupListNew.GetCodeFromId(LookupListNew.DropdownLookupList(LookupListNew.LK_YESNO_NUM, langId, True), New Guid(Me.cboReportConversionControl.SelectedItem.Value)))
            If .IsNew AndAlso .ReportConversionControl <> strObj Then blnBOChanged = True

            strObj = .PaymentMethod
            Me.PopulateBOProperty(Me.State.MyBO_R, Me.PAYMENTMETHOD_PROPERTY, LookupListNew.GetCodeFromId(LookupListNew.DropdownLookupList(LookupListNew.LK_PMTHD_NUM, langId, True), New Guid(Me.cboPaymentMethod.SelectedItem.Value)))
            If .IsNew AndAlso .PaymentMethod <> strObj Then blnBOChanged = True

            strObj = .PayAsPaidAccountType
            Me.PopulateBOProperty(Me.State.MyBO_R, Me.PAYASPAIDACCOUNTTYPE_PROPERTY, LookupListNew.GetCodeFromId(LookupListNew.DropdownLookupList(LookupListNew.LK_PAPAT, langId, True), New Guid(Me.cboPayAsPaidAccountType.SelectedItem.Value)))
            If .IsNew AndAlso .PayAsPaidAccountType <> strObj Then blnBOChanged = True

            strObj = .AddressStatus
            Me.PopulateBOProperty(Me.State.MyBO_R, Me.ADDRESSSTATUS_PROPERTY, LookupListNew.GetCodeFromId(vendStdv, New Guid(Me.cboAddressStatus.SelectedItem.Value)))
            If .IsNew AndAlso .AddressStatus <> strObj Then blnBOChanged = True

            strObj = .SupplierStatus
            Me.PopulateBOProperty(Me.State.MyBO_R, Me.SUPPLIERSTATUS_PROPERTY, LookupListNew.GetCodeFromId(vendStdv, New Guid(Me.cboSupplierStatus.SelectedItem.Value)))
            If .IsNew AndAlso .SupplierStatus <> strObj Then blnBOChanged = True

            strObj = .AccountAnalysis1
            Me.PopulateBOProperty(Me.State.MyBO_R, Me.ACCOUNT_ANALYSIS_FLAG1_PROPERTY, LookupListNew.GetCodeFromId(AnalCodedv, New Guid(Me.cboAcctAnal1.SelectedItem.Value)))
            If .IsNew AndAlso .AccountAnalysis1 <> strObj Then blnBOChanged = True

            strObj = .AccountAnalysis2
            Me.PopulateBOProperty(Me.State.MyBO_R, Me.ACCOUNT_ANALYSIS_FLAG2_PROPERTY, LookupListNew.GetCodeFromId(AnalCodedv, New Guid(Me.cboAcctAnal2.SelectedItem.Value)))
            If .IsNew AndAlso .AccountAnalysis2 <> strObj Then blnBOChanged = True

            strObj = .AccountAnalysis3
            Me.PopulateBOProperty(Me.State.MyBO_R, Me.ACCOUNT_ANALYSIS_FLAG3_PROPERTY, LookupListNew.GetCodeFromId(AnalCodedv, New Guid(Me.cboAcctAnal3.SelectedItem.Value)))
            If .IsNew AndAlso .AccountAnalysis3 <> strObj Then blnBOChanged = True

            strObj = .AccountAnalysis4
            Me.PopulateBOProperty(Me.State.MyBO_R, Me.ACCOUNT_ANALYSIS_FLAG4_PROPERTY, LookupListNew.GetCodeFromId(AnalCodedv, New Guid(Me.cboAcctAnal4.SelectedItem.Value)))
            If .IsNew AndAlso .AccountAnalysis4 <> strObj Then blnBOChanged = True

            strObj = .AccountAnalysis5
            Me.PopulateBOProperty(Me.State.MyBO_R, Me.ACCOUNT_ANALYSIS_FLAG5_PROPERTY, LookupListNew.GetCodeFromId(AnalCodedv, New Guid(Me.cboAcctAnal5.SelectedItem.Value)))
            If .IsNew AndAlso .AccountAnalysis5 <> strObj Then blnBOChanged = True

            strObj = .AccountAnalysis6
            Me.PopulateBOProperty(Me.State.MyBO_R, Me.ACCOUNT_ANALYSIS_FLAG6_PROPERTY, LookupListNew.GetCodeFromId(AnalCodedv, New Guid(Me.cboAcctAnal6.SelectedItem.Value)))
            If .IsNew AndAlso .AccountAnalysis6 <> strObj Then blnBOChanged = True

            strObj = .AccountAnalysis7
            Me.PopulateBOProperty(Me.State.MyBO_R, Me.ACCOUNT_ANALYSIS_FLAG7_PROPERTY, LookupListNew.GetCodeFromId(AnalCodedv, New Guid(Me.cboAcctAnal7.SelectedItem.Value)))
            If .IsNew AndAlso .AccountAnalysis7 <> strObj Then blnBOChanged = True

            strObj = .AccountAnalysis8
            Me.PopulateBOProperty(Me.State.MyBO_R, Me.ACCOUNT_ANALYSIS_FLAG8_PROPERTY, LookupListNew.GetCodeFromId(AnalCodedv, New Guid(Me.cboAcctAnal8.SelectedItem.Value)))
            If .IsNew AndAlso .AccountAnalysis8 <> strObj Then blnBOChanged = True

            strObj = .AccountAnalysis9
            Me.PopulateBOProperty(Me.State.MyBO_R, Me.ACCOUNT_ANALYSIS_FLAG9_PROPERTY, LookupListNew.GetCodeFromId(AnalCodedv, New Guid(Me.cboAcctAnal9.SelectedItem.Value)))
            If .IsNew AndAlso .AccountAnalysis9 <> strObj Then blnBOChanged = True

            strObj = .AccountAnalysis10
            Me.PopulateBOProperty(Me.State.MyBO_R, Me.ACCOUNT_ANALYSIS_FLAG10_PROPERTY, LookupListNew.GetCodeFromId(AnalCodedv, New Guid(Me.cboAcctAnal10.SelectedItem.Value)))
            If .IsNew AndAlso .AccountAnalysis10 <> strObj Then blnBOChanged = True
        End With

        If blnBOChanged Then
            State.IsNewBORDirty = (True OrElse State.IsNewBORDirty)
            blnBOChanged = False
        End If


        'payable 
        With Me.State.MyBO_P
            If .IsNew AndAlso .AccountCode <> moAccountCodeTEXT_P.Text Then blnBOChanged = True
            Me.PopulateBOProperty(Me.State.MyBO_P, Me.ACCOUNTCODE_PROPERTY, Me.moAccountCodeTEXT_P)

            If cboCompany_P.Visible = False Then
                Me.PopulateBOProperty(Me.State.MyBO_P, Me.ACCTCOMPANYID_PROPERTY, moCompId)
            Else
                Dim selectedCompId As Guid = Me.GetSelectedItem(Me.cboCompany_P)
                If .IsNew AndAlso .AcctCompanyId <> selectedCompId Then blnBOChanged = True
                Me.PopulateBOProperty(Me.State.MyBO_P, Me.ACCTCOMPANYID_PROPERTY, selectedCompId)
            End If

            If .IsNew AndAlso .BalanceType <> moBalanceTypeTEXT_P.Text Then blnBOChanged = True
            Me.PopulateBOProperty(Me.State.MyBO_P, Me.BALANCETYPE_PROPERTY, Me.moBalanceTypeTEXT_P)

            If .IsNew AndAlso .DataAccessGroupCode <> moDataAccessGroupCodeTEXT_P.Text Then blnBOChanged = True
            Me.PopulateBOProperty(Me.State.MyBO_P, Me.DATAACCESSGROUPCODE_PROPERTY, Me.moDataAccessGroupCodeTEXT_P)

            If .IsNew AndAlso .DefaultCurrencyCode <> moDefaultCurrencyCodeTEXT_P.Text Then blnBOChanged = True
            Me.PopulateBOProperty(Me.State.MyBO_P, Me.DEFAULTCURRENCYCODE_PROPERTY, Me.moDefaultCurrencyCodeTEXT_P)

            If .IsNew AndAlso .SuppressRevaluation <> moSuppressReevaluationTEXT_P.Text Then blnBOChanged = True
            Me.PopulateBOProperty(Me.State.MyBO_P, Me.SUPPRESSREVALUATION_PROPERTY, Me.moSuppressReevaluationTEXT_P)

            If .IsNew AndAlso .AccountAnalysisACode1 <> moAcctAnal1TEXT_P.Text Then blnBOChanged = True
            Me.PopulateBOProperty(Me.State.MyBO_P, Me.ACCOUNTANALYSIS_A_1_PROPERTY, Me.moAcctAnal1TEXT_P)

            If .IsNew AndAlso .AccountAnalysisACode2 <> moAcctAnal2TEXT_P.Text Then blnBOChanged = True
            Me.PopulateBOProperty(Me.State.MyBO_P, Me.ACCOUNTANALYSIS_A_2_PROPERTY, Me.moAcctAnal2TEXT_P)

            If .IsNew AndAlso .AccountAnalysisACode3 <> moAcctAnal3TEXT_P.Text Then blnBOChanged = True
            Me.PopulateBOProperty(Me.State.MyBO_P, Me.ACCOUNTANALYSIS_A_3_PROPERTY, Me.moAcctAnal3TEXT_P)

            If .IsNew AndAlso .AccountAnalysisACode4 <> moAcctAnal4TEXT_P.Text Then blnBOChanged = True
            Me.PopulateBOProperty(Me.State.MyBO_P, Me.ACCOUNTANALYSIS_A_4_PROPERTY, Me.moAcctAnal4TEXT_P)

            If .IsNew AndAlso .AccountAnalysisACode5 <> moAcctAnal5TEXT_P.Text Then blnBOChanged = True
            Me.PopulateBOProperty(Me.State.MyBO_P, Me.ACCOUNTANALYSIS_A_5_PROPERTY, Me.moAcctAnal5TEXT_P)

            If .IsNew AndAlso .AccountAnalysisACode6 <> moAcctAnal6TEXT_P.Text Then blnBOChanged = True
            Me.PopulateBOProperty(Me.State.MyBO_P, Me.ACCOUNTANALYSIS_A_6_PROPERTY, Me.moAcctAnal6TEXT_P)

            If .IsNew AndAlso .AccountAnalysisACode7 <> moAcctAnal7TEXT_P.Text Then blnBOChanged = True
            Me.PopulateBOProperty(Me.State.MyBO_P, Me.ACCOUNTANALYSIS_A_7_PROPERTY, Me.moAcctAnal7TEXT_P)

            If .IsNew AndAlso .AccountAnalysisACode8 <> moAcctAnal8TEXT_P.Text Then blnBOChanged = True
            Me.PopulateBOProperty(Me.State.MyBO_P, Me.ACCOUNTANALYSIS_A_8_PROPERTY, Me.moAcctAnal8TEXT_P)

            If .IsNew AndAlso .AccountAnalysisACode9 <> moAcctAnal9TEXT_P.Text Then blnBOChanged = True
            Me.PopulateBOProperty(Me.State.MyBO_P, Me.ACCOUNTANALYSIS_A_9_PROPERTY, Me.moAcctAnal9TEXT_P)

            If .IsNew AndAlso .AccountAnalysisACode10 <> moAcctAnal10TEXT_P.Text Then blnBOChanged = True
            Me.PopulateBOProperty(Me.State.MyBO_P, Me.ACCOUNTANALYSIS_A_10_PROPERTY, Me.moAcctAnal10TEXT_P)

            If .IsNew AndAlso .AccountAnalysisCode1 <> moAcctAnalT1TEXT_P.Text Then blnBOChanged = True
            Me.PopulateBOProperty(Me.State.MyBO_P, Me.ACCOUNTANALYSIS1_PROPERTY, Me.moAcctAnalT1TEXT_P)

            If .IsNew AndAlso .AccountAnalysisCode2 <> moAcctAnalT2TEXT_P.Text Then blnBOChanged = True
            Me.PopulateBOProperty(Me.State.MyBO_P, Me.ACCOUNTANALYSIS2_PROPERTY, Me.moAcctAnalT2TEXT_P)

            If .IsNew AndAlso .AccountAnalysisCode3 <> moAcctAnalT3TEXT_P.Text Then blnBOChanged = True
            Me.PopulateBOProperty(Me.State.MyBO_P, Me.ACCOUNTANALYSIS3_PROPERTY, Me.moAcctAnalT3TEXT_P)

            If .IsNew AndAlso .AccountAnalysisCode4 <> moAcctAnalT4TEXT_P.Text Then blnBOChanged = True
            Me.PopulateBOProperty(Me.State.MyBO_P, Me.ACCOUNTANALYSIS4_PROPERTY, Me.moAcctAnalT4TEXT_P)

            If .IsNew AndAlso .AccountAnalysisCode5 <> moAcctAnalT5TEXT_P.Text Then blnBOChanged = True
            Me.PopulateBOProperty(Me.State.MyBO_P, Me.ACCOUNTANALYSIS5_PROPERTY, Me.moAcctAnalT5TEXT_P)

            If .IsNew AndAlso .AccountAnalysisCode6 <> moAcctAnalT6TEXT_P.Text Then blnBOChanged = True
            Me.PopulateBOProperty(Me.State.MyBO_P, Me.ACCOUNTANALYSIS6_PROPERTY, Me.moAcctAnalT6TEXT_P)

            If .IsNew AndAlso .AccountAnalysisCode7 <> moAcctAnalT7TEXT_P.Text Then blnBOChanged = True
            Me.PopulateBOProperty(Me.State.MyBO_P, Me.ACCOUNTANALYSIS7_PROPERTY, Me.moAcctAnalT7TEXT_P)

            If .IsNew AndAlso .AccountAnalysisCode8 <> moAcctAnalT8TEXT_P.Text Then blnBOChanged = True
            Me.PopulateBOProperty(Me.State.MyBO_P, Me.ACCOUNTANALYSIS8_PROPERTY, Me.moAcctAnalT8TEXT_P)

            If .IsNew AndAlso .AccountAnalysisCode9 <> moAcctAnalT9TEXT_P.Text Then blnBOChanged = True
            Me.PopulateBOProperty(Me.State.MyBO_P, Me.ACCOUNTANALYSIS9_PROPERTY, Me.moAcctAnalT9TEXT_P)

            If .IsNew AndAlso .AccountAnalysisCode10 <> moAcctAnalT10TEXT_P.Text Then blnBOChanged = True
            Me.PopulateBOProperty(Me.State.MyBO_P, Me.ACCOUNTANALYSIS10_PROPERTY, Me.moAcctAnalT10TEXT_P)

            strObj = .SupplierAnalysisCode1
            Me.PopulateBOProperty(Me.State.MyBO_P, Me.SUPPLIERANALYSIS1_PROPERTY, LookupListNew.GetCodeFromId(LookupListNew.DropdownLookupList(LookupListNew.LK_ACCOUNTING_PAYMENT_METHOD, langId, True), New Guid(Me.cboSuppAnal1_P.SelectedItem.Value)))
            If .IsNew AndAlso .SupplierAnalysisCode1 <> strObj Then blnBOChanged = True

            If .IsNew AndAlso .SupplierAnalysisCode2 <> moSuppAnal2TEXT_P.Text Then blnBOChanged = True
            Me.PopulateBOProperty(Me.State.MyBO_P, Me.SUPPLIERANALYSIS2_PROPERTY, Me.moSuppAnal2TEXT_P)

            If .IsNew AndAlso .SupplierAnalysisCode3 <> moSuppAnal3TEXT_P.Text Then blnBOChanged = True
            Me.PopulateBOProperty(Me.State.MyBO_P, Me.SUPPLIERANALYSIS3_PROPERTY, Me.moSuppAnal3TEXT_P)

            If .IsNew AndAlso .SupplierAnalysisCode4 <> moSuppAnal4TEXT_P.Text Then blnBOChanged = True
            Me.PopulateBOProperty(Me.State.MyBO_P, Me.SUPPLIERANALYSIS4_PROPERTY, Me.moSuppAnal4TEXT_P)

            If .IsNew AndAlso .SupplierAnalysisCode5 <> moSuppAnal5TEXT_P.Text Then blnBOChanged = True
            Me.PopulateBOProperty(Me.State.MyBO_P, Me.SUPPLIERANALYSIS5_PROPERTY, Me.moSuppAnal5TEXT_P)

            If .IsNew AndAlso .SupplierAnalysisCode6 <> moSuppAnal6TEXT_P.Text Then blnBOChanged = True
            Me.PopulateBOProperty(Me.State.MyBO_P, Me.SUPPLIERANALYSIS6_PROPERTY, Me.moSuppAnal6TEXT_P)

            If .IsNew AndAlso .SupplierAnalysisCode7 <> moSuppAnal7TEXT_P.Text Then blnBOChanged = True
            Me.PopulateBOProperty(Me.State.MyBO_P, Me.SUPPLIERANALYSIS7_PROPERTY, Me.moSuppAnal7TEXT_P)

            If .IsNew AndAlso .SupplierAnalysisCode8 <> moSuppAnal8TEXT_P.Text Then blnBOChanged = True
            Me.PopulateBOProperty(Me.State.MyBO_P, Me.SUPPLIERANALYSIS8_PROPERTY, Me.moSuppAnal8TEXT_P)

            If .IsNew AndAlso .SupplierAnalysisCode9 <> moSuppAnal9TEXT_P.Text Then blnBOChanged = True
            Me.PopulateBOProperty(Me.State.MyBO_P, Me.SUPPLIERANALYSIS9_PROPERTY, Me.moSuppAnal9TEXT_P)

            If .IsNew AndAlso .SupplierAnalysisCode10 <> moSuppAnal10TEXT_P.Text Then blnBOChanged = True
            Me.PopulateBOProperty(Me.State.MyBO_P, Me.SUPPLIERANALYSIS10_PROPERTY, Me.moSuppAnal10TEXT_P)

            If .IsNew AndAlso .AddressLookupCode <> moAddressLookupCodeTEXT_P.Text Then blnBOChanged = True
            Me.PopulateBOProperty(Me.State.MyBO_P, Me.ADDRESSLOOKUPCODE_PROPERTY, Me.moAddressLookupCodeTEXT_P)

            If .IsNew AndAlso .AddressSequenceNumber <> moAddressSequenceNumberTEXT_P.Text Then blnBOChanged = True
            Me.PopulateBOProperty(Me.State.MyBO_P, Me.ADDRESSSEQUENCENUMBER_PROPERTY, Me.moAddressSequenceNumberTEXT_P)

            If .IsNew AndAlso .SupplierLookupCode <> moSupplierLookUpCodeTEXT_P.Text Then blnBOChanged = True
            Me.PopulateBOProperty(Me.State.MyBO_P, Me.SUPPLIERLOOKUPCODE_PROPERTY, Me.moSupplierLookUpCodeTEXT_P)

            If .IsNew AndAlso .UserArea <> moUserAreaTEXT_P.Text Then blnBOChanged = True
            Me.PopulateBOProperty(Me.State.MyBO_P, Me.USER_AREA_PROPERTY, Me.moUserAreaTEXT_P)

            If .IsNew AndAlso .Description <> moDescriptionTEXT_P.Text Then blnBOChanged = True
            Me.PopulateBOProperty(Me.State.MyBO_P, Me.DESCRIPTION_PROPERTY, Me.moDescriptionTEXT_P)

            If .IsNew AndAlso .DefaultBankSubCode <> moDefaultBankSubCodeTEXT_P.Text Then blnBOChanged = True
            Me.PopulateBOProperty(Me.State.MyBO_P, Me.DEFAULT_BANK_SUBCODE_PROPERTY, Me.moDefaultBankSubCodeTEXT_P)

            guidItem = .PaymentTermsId
            Me.PopulateBOProperty(Me.State.MyBO_P, Me.PAYMENT_TERMS_ID_PROPERTY, Me.cboPaymentTerms_P)
            If .IsNew AndAlso .PaymentTermsId <> guidItem Then blnBOChanged = True

            strObj = .ConversionCodeControl
            Me.PopulateBOProperty(Me.State.MyBO_P, Me.CONVERSIONCODECONTROL_PROPERTY, LookupListNew.GetCodeFromId(LookupListNew.DropdownLookupList(LookupListNew.LK_CONVCTL, langId, True), New Guid(Me.cboConversionCodeControl_P.SelectedItem.Value)))
            If .IsNew AndAlso .ConversionCodeControl <> strObj Then blnBOChanged = True

            strObj = .ReportConversionControl
            Me.PopulateBOProperty(Me.State.MyBO_P, Me.REPORTCONVERSIONCONTROL_PROPERTY, LookupListNew.GetCodeFromId(LookupListNew.DropdownLookupList(LookupListNew.LK_YESNO_NUM, langId, True), New Guid(Me.cboReportConversionControl_P.SelectedItem.Value)))
            If .IsNew AndAlso .ReportConversionControl <> strObj Then blnBOChanged = True

            strObj = .PaymentMethod
            Me.PopulateBOProperty(Me.State.MyBO_P, Me.PAYMENTMETHOD_PROPERTY, LookupListNew.GetCodeFromId(LookupListNew.DropdownLookupList(LookupListNew.LK_PMTHD_NUM, langId, True), New Guid(Me.cboPaymentMethod_P.SelectedItem.Value)))
            If .IsNew AndAlso .PaymentMethod <> strObj Then blnBOChanged = True

            strObj = .PayAsPaidAccountType
            Me.PopulateBOProperty(Me.State.MyBO_P, Me.PAYASPAIDACCOUNTTYPE_PROPERTY, LookupListNew.GetCodeFromId(LookupListNew.DropdownLookupList(LookupListNew.LK_PAPAT, langId, True), New Guid(Me.cboPayAsPaidAccountType_P.SelectedItem.Value)))
            If .IsNew AndAlso .PayAsPaidAccountType <> strObj Then blnBOChanged = True

            strObj = .AddressStatus
            Me.PopulateBOProperty(Me.State.MyBO_P, Me.ADDRESSSTATUS_PROPERTY, LookupListNew.GetCodeFromId(vendStdv, New Guid(Me.cboAddressStatus_P.SelectedItem.Value)))
            If .IsNew AndAlso .AddressStatus <> strObj Then blnBOChanged = True

            strObj = .SupplierStatus
            Me.PopulateBOProperty(Me.State.MyBO_P, Me.SUPPLIERSTATUS_PROPERTY, LookupListNew.GetCodeFromId(vendStdv, New Guid(Me.cboSupplierStatus_P.SelectedItem.Value)))
            If .IsNew AndAlso .SupplierStatus <> strObj Then blnBOChanged = True

            strObj = .AccountAnalysis1
            Me.PopulateBOProperty(Me.State.MyBO_P, Me.ACCOUNT_ANALYSIS_FLAG1_PROPERTY, LookupListNew.GetCodeFromId(AnalCodedv, New Guid(Me.cboAcctAnal1_P.SelectedItem.Value)))
            If .IsNew AndAlso .AccountAnalysis1 <> strObj Then blnBOChanged = True

            strObj = .AccountAnalysis2
            Me.PopulateBOProperty(Me.State.MyBO_P, Me.ACCOUNT_ANALYSIS_FLAG2_PROPERTY, LookupListNew.GetCodeFromId(AnalCodedv, New Guid(Me.cboAcctAnal2_P.SelectedItem.Value)))
            If .IsNew AndAlso .AccountAnalysis2 <> strObj Then blnBOChanged = True

            strObj = .AccountAnalysis3
            Me.PopulateBOProperty(Me.State.MyBO_P, Me.ACCOUNT_ANALYSIS_FLAG3_PROPERTY, LookupListNew.GetCodeFromId(AnalCodedv, New Guid(Me.cboAcctAnal3_P.SelectedItem.Value)))
            If .IsNew AndAlso .AccountAnalysis3 <> strObj Then blnBOChanged = True

            strObj = .AccountAnalysis4
            Me.PopulateBOProperty(Me.State.MyBO_P, Me.ACCOUNT_ANALYSIS_FLAG4_PROPERTY, LookupListNew.GetCodeFromId(AnalCodedv, New Guid(Me.cboAcctAnal4_P.SelectedItem.Value)))
            If .IsNew AndAlso .AccountAnalysis4 <> strObj Then blnBOChanged = True

            strObj = .AccountAnalysis5
            Me.PopulateBOProperty(Me.State.MyBO_P, Me.ACCOUNT_ANALYSIS_FLAG5_PROPERTY, LookupListNew.GetCodeFromId(AnalCodedv, New Guid(Me.cboAcctAnal5_P.SelectedItem.Value)))
            If .IsNew AndAlso .AccountAnalysis5 <> strObj Then blnBOChanged = True

            strObj = .AccountAnalysis6
            Me.PopulateBOProperty(Me.State.MyBO_P, Me.ACCOUNT_ANALYSIS_FLAG6_PROPERTY, LookupListNew.GetCodeFromId(AnalCodedv, New Guid(Me.cboAcctAnal6_P.SelectedItem.Value)))
            If .IsNew AndAlso .AccountAnalysis6 <> strObj Then blnBOChanged = True

            strObj = .AccountAnalysis7
            Me.PopulateBOProperty(Me.State.MyBO_P, Me.ACCOUNT_ANALYSIS_FLAG7_PROPERTY, LookupListNew.GetCodeFromId(AnalCodedv, New Guid(Me.cboAcctAnal7_P.SelectedItem.Value)))
            If .IsNew AndAlso .AccountAnalysis7 <> strObj Then blnBOChanged = True

            strObj = .AccountAnalysis8
            Me.PopulateBOProperty(Me.State.MyBO_P, Me.ACCOUNT_ANALYSIS_FLAG8_PROPERTY, LookupListNew.GetCodeFromId(AnalCodedv, New Guid(Me.cboAcctAnal8_P.SelectedItem.Value)))
            If .IsNew AndAlso .AccountAnalysis8 <> strObj Then blnBOChanged = True

            strObj = .AccountAnalysis9
            Me.PopulateBOProperty(Me.State.MyBO_P, Me.ACCOUNT_ANALYSIS_FLAG9_PROPERTY, LookupListNew.GetCodeFromId(AnalCodedv, New Guid(Me.cboAcctAnal9_P.SelectedItem.Value)))
            If .IsNew AndAlso .AccountAnalysis9 <> strObj Then blnBOChanged = True

            strObj = .AccountAnalysis10
            Me.PopulateBOProperty(Me.State.MyBO_P, Me.ACCOUNT_ANALYSIS_FLAG10_PROPERTY, LookupListNew.GetCodeFromId(AnalCodedv, New Guid(Me.cboAcctAnal10_P.SelectedItem.Value)))
            If .IsNew AndAlso .AccountAnalysis10 <> strObj Then blnBOChanged = True
        End With

        If blnBOChanged Then State.IsNewBOPDirty = (True OrElse State.IsNewBOPDirty)
        Me.moBankInfo.PopulateBOFromControl(True, False)

        If Me.ErrCollection.Count > 0 Then
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
                Me.SetSelectedItem(cboAcctAnal1, oGuidOptional)
                Me.SetSelectedItem(cboAcctAnal2, oGuidOptional)
                Me.SetSelectedItem(cboAcctAnal3, oGuidOptional)
                Me.SetSelectedItem(cboAcctAnal4, oGuidOptional)
                Me.SetSelectedItem(cboAcctAnal5, oGuidOptional)
                Me.SetSelectedItem(cboAcctAnal6, oGuidOptional)
                Me.SetSelectedItem(cboAcctAnal7, oGuidOptional)
                Me.SetSelectedItem(cboAcctAnal8, oGuidOptional)
                Me.SetSelectedItem(cboAcctAnal9, oGuidOptional)
                Me.SetSelectedItem(cboAcctAnal10, oGuidOptional)
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
                Me.SetSelectedItem(cboAcctAnal1_P, oGuidOptional)
                Me.SetSelectedItem(cboAcctAnal2_P, oGuidOptional)
                Me.SetSelectedItem(cboAcctAnal3_P, oGuidOptional)
                Me.SetSelectedItem(cboAcctAnal4_P, oGuidOptional)
                Me.SetSelectedItem(cboAcctAnal5_P, oGuidOptional)
                Me.SetSelectedItem(cboAcctAnal6_P, oGuidOptional)
                Me.SetSelectedItem(cboAcctAnal7_P, oGuidOptional)
                Me.SetSelectedItem(cboAcctAnal8_P, oGuidOptional)
                Me.SetSelectedItem(cboAcctAnal9_P, oGuidOptional)
                Me.SetSelectedItem(cboAcctAnal10_P, oGuidOptional)
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
            Me.cboConversionCodeControl.Populate(conVctdv, populate_options)
            'Me.BindListControlToDataView(Me.cboReportConversionControl, LookupListNew.DropdownLookupList(LookupListNew.LK_YESNO_NUM, langId, True))
            Me.cboReportConversionControl.Populate(yesNumdv, populate_options)
            'Me.BindListControlToDataView(Me.cboPaymentMethod, LookupListNew.DropdownLookupList(LookupListNew.LK_PMTHD_NUM, langId, True))
            Me.cboPaymentMethod.Populate(pytmMthddv, populate_options)
            'Me.BindListControlToDataView(Me.cboSupplierStatus, vendStdv)
            Me.cboSupplierStatus.Populate(vendStdv, populate_options)
            'Me.BindListControlToDataView(Me.cboPayAsPaidAccountType, LookupListNew.DropdownLookupList(LookupListNew.LK_PAPAT, langId, True))
            Me.cboPayAsPaidAccountType.Populate(payasPddv, populate_options)
            'Me.BindListControlToDataView(Me.cboAddressStatus, vendStdv)
            Me.cboAddressStatus.Populate(vendStdv, populate_options)

            'Me.BindListControlToDataView(cboAcctAnal1, AnalCodedv)
            Me.cboAcctAnal1.Populate(AnalCodedv, populate_options)
            'Me.BindListControlToDataView(cboAcctAnal2, AnalCodedv)
            Me.cboAcctAnal2.Populate(AnalCodedv, populate_options)
            'Me.BindListControlToDataView(cboAcctAnal3, AnalCodedv)
            Me.cboAcctAnal3.Populate(AnalCodedv, populate_options)
            'Me.BindListControlToDataView(cboAcctAnal4, AnalCodedv)
            Me.cboAcctAnal4.Populate(AnalCodedv, populate_options)
            'Me.BindListControlToDataView(cboAcctAnal5, AnalCodedv)
            Me.cboAcctAnal5.Populate(AnalCodedv, populate_options)
            'Me.BindListControlToDataView(cboAcctAnal6, AnalCodedv)
            Me.cboAcctAnal6.Populate(AnalCodedv, populate_options)
            'Me.BindListControlToDataView(cboAcctAnal7, AnalCodedv)
            Me.cboAcctAnal7.Populate(AnalCodedv, populate_options)
            'Me.BindListControlToDataView(cboAcctAnal8, AnalCodedv)
            Me.cboAcctAnal8.Populate(AnalCodedv, populate_options)
            'Me.BindListControlToDataView(cboAcctAnal9, AnalCodedv)
            Me.cboAcctAnal9.Populate(AnalCodedv, populate_options)
            'Me.BindListControlToDataView(cboAcctAnal10, AnalCodedv)
            Me.cboAcctAnal10.Populate(AnalCodedv, populate_options)

            'Me.BindListControlToDataView(Me.cboPaymentTerms, LookupListNew.DropdownLookupList(LookupListNew.LK_PAYMENT_TERMS, langId, True))
            Me.cboPaymentTerms.Populate(pmtTrmdv, populate_options)

            'Payable tab
            'Me.BindListControlToDataView(Me.cboConversionCodeControl_P, LookupListNew.DropdownLookupList(LookupListNew.LK_CONVCTL, langId, True))
            Me.cboConversionCodeControl_P.Populate(conVctdv, populate_options)
            'Me.BindListControlToDataView(Me.cboReportConversionControl_P, LookupListNew.DropdownLookupList(LookupListNew.LK_YESNO_NUM, langId, True))
            Me.cboReportConversionControl_P.Populate(yesNumdv, populate_options)
            'Me.BindListControlToDataView(Me.cboPaymentMethod_P, LookupListNew.DropdownLookupList(LookupListNew.LK_PMTHD_NUM, langId, True))
            Me.cboPaymentMethod_P.Populate(pytmMthddv, populate_options)
            'Me.BindListControlToDataView(Me.cboSuppAnal1_P, LookupListNew.DropdownLookupList(LookupListNew.LK_ACCOUNTING_PAYMENT_METHOD, langId, True))
            Me.cboSuppAnal1_P.Populate(acctPmtMthddv, populate_options)

            'Me.BindListControlToDataView(Me.cboSupplierStatus_P, vendStdv)
            Me.cboSupplierStatus_P.Populate(vendStdv, populate_options)
            'Me.BindListControlToDataView(Me.cboPayAsPaidAccountType_P, LookupListNew.DropdownLookupList(LookupListNew.LK_PAPAT, langId, True))
            Me.cboPayAsPaidAccountType_P.Populate(payasPddv, populate_options)
            'Me.BindListControlToDataView(Me.cboAddressStatus_P, vendStdv)
            Me.cboAddressStatus_P.Populate(vendStdv, populate_options)

            'Me.BindListControlToDataView(cboAcctAnal1_P, AnalCodedv)
            Me.cboAcctAnal1_P.Populate(AnalCodedv, populate_options)
            'Me.BindListControlToDataView(cboAcctAnal2_P, AnalCodedv)
            Me.cboAcctAnal2_P.Populate(AnalCodedv, populate_options)
            'Me.BindListControlToDataView(cboAcctAnal3_P, AnalCodedv)
            Me.cboAcctAnal3_P.Populate(AnalCodedv, populate_options)
            'Me.BindListControlToDataView(cboAcctAnal4_P, AnalCodedv)
            Me.cboAcctAnal4_P.Populate(AnalCodedv, populate_options)
            'Me.BindListControlToDataView(cboAcctAnal5_P, AnalCodedv)
            Me.cboAcctAnal5_P.Populate(AnalCodedv, populate_options)
            'Me.BindListControlToDataView(cboAcctAnal6_P, AnalCodedv)
            Me.cboAcctAnal6_P.Populate(AnalCodedv, populate_options)
            'Me.BindListControlToDataView(cboAcctAnal7_P, AnalCodedv)
            Me.cboAcctAnal7_P.Populate(AnalCodedv, populate_options)
            'Me.BindListControlToDataView(cboAcctAnal8_P, AnalCodedv)
            Me.cboAcctAnal8_P.Populate(AnalCodedv, populate_options)
            'Me.BindListControlToDataView(cboAcctAnal9_P, AnalCodedv)
            Me.cboAcctAnal9_P.Populate(AnalCodedv, populate_options)
            'Me.BindListControlToDataView(cboAcctAnal10_P, AnalCodedv)
            Me.cboAcctAnal10_P.Populate(AnalCodedv, populate_options)

            'Me.BindListControlToDataView(Me.cboPaymentTerms_P, LookupListNew.DropdownLookupList(LookupListNew.LK_PAYMENT_TERMS, langId, True))
            Me.cboPaymentTerms_P.Populate(pmtTrmdv, populate_options)

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
                          Where lst.Code = Me.ACCT_ANALYSIS_MANDATORY
                          Select lst).ToArray()

            If AnalCodedv.Count = 1 Then
                Me.litScriptVars.Text += "var prohib = '" + AnalCodedv.First().ListItemId.ToString + "';"
            End If

            AnalCodedv = (From lst In AnalCodedv
                          Where lst.Code = Me.ACCT_ANALYSIS_PROHIBIT
                          Select lst).ToArray()

            If AnalCodedv.Count = 1 Then
                Me.litScriptVars.Text += "var prohib = '" + AnalCodedv.First().ListItemId.ToString + "';"
            End If

        Catch ex As Exception
            ErrControllerMaster.AddError(ACCTSETT_LIST_FORM001)
            ErrControllerMaster.AddError(ex.Message, False)
            ErrControllerMaster.Show()
        End Try

    End Sub

    Public Shared Sub SetLabelColor(ByVal lbl As Label)
        lbl.ForeColor = Color.Black
    End Sub

#End Region

End Class
