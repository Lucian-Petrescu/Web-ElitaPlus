Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports Assurant.Elita.Web.Forms
Imports System.Threading
Partial Class CompanyGroupDetailForm
    Inherits ElitaPlusSearchPage
#Region "Constants"
    Public Const url As String = "CompanyGroupDetailForm.aspx"
    Public Const YESNO_Y As String = "Yes"
    Public Const YESNO_N As String = "No"
    Public Const ADMIN As String = "Admin"
    Public Const COMPANY_GROUP_DETAIL As String = "COMPANY_GROUP_DETAIL"

#End Region

#Region "Page Parameters"
    Public Class Parameters
        Public CompanyGroupBO As CompanyGroup
        Public CompanyGroupID As Guid

        Public Sub New(CompanygrpID As Guid)

            CompanyGroupID = CompanygrpID
        End Sub
    End Class
#End Region
#Region "Page Return Type"
    Public Class ReturnType
        Public LastOperation As DetailPageCommand
        Public EditingBo As CompanyGroup
        Public BoChanged As Boolean = False
        Public Sub New(LastOp As DetailPageCommand, curEditingBo As CompanyGroup, Optional ByVal boChanged As Boolean = False)
            LastOperation = LastOp
            EditingBo = curEditingBo
            Me.BoChanged = boChanged
        End Sub
        Public Sub New(LastOp As DetailPageCommand)
            LastOperation = LastOp
        End Sub
    End Class
#End Region

#Region "Page State"

    Class MyState
        Public MyBO As CompanyGroup
        Public ScreenSnapShotBO As CompanyGroup
        Public companygroupID As Guid
        Public Pageparameters As Parameters
        Public IsNew As Boolean = False
        Public IsACopy As Boolean
        Public year As String
        Public CompanyGroupIdId As Guid
        Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
        Public LastErrMsg As String
        Public HasDataChanged As Boolean
        Public ForEdit As Boolean = False
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
                State.Pageparameters = CType(CallingParameters, Parameters)
                State.MyBO = New CompanyGroup(State.Pageparameters.CompanyGroupID)
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

#End Region

#Region "Page Events"
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            MasterPage.MessageController.Clear()
            MasterPage.UsePageTabTitleInBreadCrum = False
            MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(ADMIN)
            MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(COMPANY_GROUP_DETAIL)
            UpdateBreadCrum()


            If Not IsPostBack Then


                If State.MyBO Is Nothing Then
                    State.MyBO = New CompanyGroup
                    State.IsNew = True
                End If

                If State.IsNew Then
                    ControlMgr.SetEnableControl(Me, ddlInvoicegrpnumbering, True)
                    ControlMgr.SetEnableControl(Me, ddlpmtgrpnumbering, True)
                Else
                    ControlMgr.SetEnableControl(Me, ddlInvoicegrpnumbering, False)
                    ControlMgr.SetEnableControl(Me, ddlpmtgrpnumbering, False)
                End If


                PopulateDropdowns()
                PopulateFormFromBOs()
                EnableDisableFields()

            End If
            CheckIfComingFromSaveConfirm()
            BindBoPropertiesToLabels()

            If Not IsPostBack Then
                'BindBoPropertiesToLabels()
                AddLabelDecorations(State.MyBO)
            End If

        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
        ShowMissingTranslations(MasterPage.MessageController)

    End Sub
#End Region

    Private Sub UpdateBreadCrum()

        If (State IsNot Nothing) Then
            MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator &
                TranslationBase.TranslateLabelOrMessage(COMPANY_GROUP_DETAIL)
        End If

    End Sub
#Region "Controlling Logic"
    Protected Sub PopulateFormFromBOs()
        Try
            Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            With State.MyBO
                PopulateControlFromBOProperty(TextboxCompanyGroupName, .Description)
                PopulateControlFromBOProperty(TextboxCompanyGroupCode, .Code)
                SetSelectedItem(ddlClaimNumbering, .ClaimNumberingById)
                SetSelectedItem(ddlinvoicenumbering, .InvoiceNumberingById)
                SetSelectedItem(ddlInvoicegrpnumbering, .InvoiceGrpNumberingById)
                SetSelectedItem(ddlAuthorizationNumbering, .AuthorizationNumberingById)
                SetSelectedItem(ddlftpsite, .FtpSiteId)
                SetSelectedItem(ddlpmtgrpnumbering, .PaymentGrpNumberingById)
                Dim yesnoid As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, .AccountingByCompany)
                SetSelectedItem(ddlacctbycomp, yesnoid)
                PopulateControlFromBOProperty(txtyrstoinactiveusedvehicles, .InactiveUsedVehiclesOlderThan)
                SetSelectedItem(ddlinactivenewvehiclesbasedon, .InactiveNewVehiclesBasedOn)
                'req 5547
                SetSelectedItem(ddlFastApproval, .ClaimFastApprovalId)
                If .IsNew Or .ClaimFastApprovalId.Equals(Guid.Empty) Then
                    SetSelectedItem(ddlFastApproval, LookupListNew.GetIdFromCode(LookupListNew.LK_FAST_APPROVAL_TYPE, "N"))
                End If
                'REQ-5773
                If .UseCommEntityTypeId.Equals(System.Guid.Empty) Then
                    SetSelectedItem(ddlUseCommEntityTypeId, LookupListNew.GetIdFromCode(LookupListNew.DropdownLookupList("YESNO", ElitaPlusIdentity.Current.ActiveUser.LanguageId), ""))
                Else
                    SetSelectedItem(ddlUseCommEntityTypeId, .UseCommEntityTypeId)
                End If


                'BindSelectItem(Me.State.MyBO.CaseNumberingByXcd, Me.ddlCaseNumbering)
                ' BindSelectItem(Me.State.MyBO.InteractionNumberingByXcd, Me.ddlInteractionNumbering)

                'REQ - 6155
                If .IsNew Or .CaseNumberingByXcd Is Nothing Then
                    SetSelectedItem(ddlCaseNumbering, "CASENUM-CMP") '"CASENUM-CMP"
                Else
                    SetSelectedItem(ddlCaseNumbering, .CaseNumberingByXcd)
                End If

                If .IsNew Or .InteractionNumberingByXcd Is Nothing Then
                    SetSelectedItem(ddlInteractionNumbering, "INTNUM-CMP")
                Else
                    SetSelectedItem(ddlInteractionNumbering, .InteractionNumberingByXcd)
                End If



            End With
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub BindBoPropertiesToLabels()
        Try
            BindBOPropertyToLabel(State.MyBO, "Description", LabelCompanyGroupName)
            BindBOPropertyToLabel(State.MyBO, "Code", LabelCompanyGroupCode)
            BindBOPropertyToLabel(State.MyBO, "ClaimNumberingById", LabelClaimNumbering)
            BindBOPropertyToLabel(State.MyBO, "InvoiceNumberingById", LabelInvoiceNumbering)
            BindBOPropertyToLabel(State.MyBO, "InvoiceGrpNumberingById", lblInvoicegrpnumbering)
            BindBOPropertyToLabel(State.MyBO, "AuthorizationNumberingById", LabelAuthorizationNumbering)
            BindBOPropertyToLabel(State.MyBO, "FtpSiteId", lblftpsite)
            BindBOPropertyToLabel(State.MyBO, "PaymentGrpNumberingById", lblpmtgrpnumbering)
            BindBOPropertyToLabel(State.MyBO, "AccountingByCompany", lblAccountingbycompany)
            BindBOPropertyToLabel(State.MyBO, "InactiveUsedVehiclesOlderThan", lblyrstoinactiveusedvehicles)
            BindBOPropertyToLabel(State.MyBO, "InactiveNewVehiclesBasedOn", lblinactivenewvehiclesbasedon)
            'req 5547
            BindBOPropertyToLabel(State.MyBO, "ClaimFastApprovalId", lblClaimFastApprovalId)
            'REQ-5773
            BindBOPropertyToLabel(State.MyBO, "UseCommEntityTypeId", LabelUseCommEntityTypeId)

            'REQ - 6155
            BindBOPropertyToLabel(State.MyBO, "CaseNumberingByXcd", LabelCaseNumbering)
            BindBOPropertyToLabel(State.MyBO, "InteractionNumberingByXcd", LabelInteractionNumbering)

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

    Protected Sub PopulateBosFromForm()
        Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
        Try
            PopulateBOProperty(State.MyBO, "Description", TextboxCompanyGroupName)
            PopulateBOProperty(State.MyBO, "Code", TextboxCompanyGroupCode)
            PopulateBOProperty(State.MyBO, "ClaimNumberingById", ddlClaimNumbering)
            PopulateBOProperty(State.MyBO, "InvoiceNumberingById", ddlinvoicenumbering)
            PopulateBOProperty(State.MyBO, "InvoiceGrpNumberingById", ddlInvoicegrpnumbering)
            PopulateBOProperty(State.MyBO, "AuthorizationNumberingById", ddlAuthorizationNumbering)
            PopulateBOProperty(State.MyBO, "FtpSiteId", ddlftpsite)
            PopulateBOProperty(State.MyBO, "PaymentGrpNumberingById", ddlpmtgrpnumbering)
            Dim acctbycomp As String = LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, New Guid(ddlacctbycomp.SelectedValue))
            PopulateBOProperty(State.MyBO, "AccountingByCompany", acctbycomp)
            PopulateBOProperty(State.MyBO, "InactiveUsedVehiclesOlderThan", txtyrstoinactiveusedvehicles)
            PopulateBOProperty(State.MyBO, "InactiveNewVehiclesBasedOn", ddlinactivenewvehiclesbasedon)
            'req 5547
            PopulateBOProperty(State.MyBO, "ClaimFastApprovalId", ddlFastApproval)
            'REQ-5773
            PopulateBOProperty(State.MyBO, "UseCommEntityTypeId", ddlUseCommEntityTypeId)
            'REQ - 6155
            PopulateBOProperty(State.MyBO, "CaseNumberingByXcd", ddlCaseNumbering, False, True)
            PopulateBOProperty(State.MyBO, "InteractionNumberingByXcd", ddlInteractionNumbering, False, True)

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub
    Protected Sub PopulateDropdowns()

        Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
        'Dim yesNoLkL As DataView = LookupListNew.DropdownLookupList("YESNO", langId, True)
        Dim yesNoLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("YESNO", Thread.CurrentPrincipal.GetLanguageCode())
        Try
            'Me.BindListControlToDataView(Me.ddlClaimNumbering, LookupListNew.GetCompanyGroupClaimNumberingLookupList(langId))
            Dim claimNumberingLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("CLNUM", Thread.CurrentPrincipal.GetLanguageCode())
            ddlClaimNumbering.Populate(claimNumberingLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })
            'Me.BindListControlToDataView(Me.ddlinvoicenumbering, LookupListNew.GetCompanyGroupInvoiceNumberingLookupList(langId))
            Dim invoiceNumberingLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("INVNUM", Thread.CurrentPrincipal.GetLanguageCode())
            ddlinvoicenumbering.Populate(invoiceNumberingLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })
            'Me.BindListControlToDataView(Me.ddlInvoicegrpnumbering, LookupListNew.GetCompanyGroupInvoicegrpNumberingLookupList(langId))
            Dim invoicegrpNumberingLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("INVGRPNUM", Thread.CurrentPrincipal.GetLanguageCode())
            ddlInvoicegrpnumbering.Populate(invoicegrpNumberingLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })
            'Me.BindListControlToDataView(Me.ddlAuthorizationNumbering, LookupListNew.GetCompanyGroupAuthorizationNumberingLookupList(langId))
            Dim authNumberLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("AUTHNUM", Thread.CurrentPrincipal.GetLanguageCode())
            ddlAuthorizationNumbering.Populate(authNumberLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })
            'Me.BindListControlToDataView(Me.ddlpmtgrpnumbering, LookupListNew.DropdownLookupList(LookupListNew.LK_COMPANY_GROUP_PAYMENT_GROUP_NUMBERING, langId))
            Dim paymentgrpNumberingLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("PMTGRPNUM", Thread.CurrentPrincipal.GetLanguageCode())
            ddlpmtgrpnumbering.Populate(paymentgrpNumberingLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })
            'Me.BindListControlToDataView(Me.ddlacctbycomp, LookupListNew.GetYesNoLookupList(langId))
            ddlacctbycomp.Populate(yesNoLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })
            'Me.BindListControlToDataView(Me.ddlftpsite, LookupListNew.GetFtpSiteLookupList())
            Dim ftpSiteLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("FTPSITE", Thread.CurrentPrincipal.GetLanguageCode())
            ddlftpsite.Populate(ftpSiteLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })
            'Me.BindListControlToDataView(Me.ddlinactivenewvehiclesbasedon, LookupListNew.GetInactivatNewVehicleLookupList(langId))
            Dim newvehicleLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("INCVTNEWVHCL", Thread.CurrentPrincipal.GetLanguageCode())
            ddlinactivenewvehiclesbasedon.Populate(newvehicleLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })
            'req 5547
            'Me.BindListControlToDataView(Me.ddlFastApproval, LookupListNew.GetFastApprovalList(langId))
            Dim fastApprovalLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("FSTAPRVL", Thread.CurrentPrincipal.GetLanguageCode())
            ddlFastApproval.Populate(fastApprovalLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })
            'REQ-5733
            'Me.BindListControlToDataView(Me.ddlUseCommEntityTypeId, yesNoLkL)
            ddlUseCommEntityTypeId.Populate(yesNoLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })
            'REQ - 6155
            ' Me.ddlCaseNumbering.PopulateOld("CASENUM", ListValueType.Description, ListValueType.ExtendedCode, PopulateBehavior.None, String.Empty, ListValueType.Description)
            Dim caseNumberingLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("CASENUM", Thread.CurrentPrincipal.GetLanguageCode())
            ddlCaseNumbering.Populate(caseNumberingLkl, New PopulateOptions() With
                {
                   .ValueFunc = AddressOf PopulateOptions.GetExtendedCode
                 })

            'Me.ddlInteractionNumbering.PopulateOld("INTNUM", ListValueType.Description, ListValueType.ExtendedCode, PopulateBehavior.None, String.Empty, ListValueType.Description)
            Dim interactionNumberingLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("INTNUM", Thread.CurrentPrincipal.GetLanguageCode())
            ddlInteractionNumbering.Populate(interactionNumberingLkl, New PopulateOptions() With
                    {
                      .ValueFunc = AddressOf PopulateOptions.GetExtendedCode
                          })
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub CreateNew()
        Try
            State.ScreenSnapShotBO = Nothing 'Reset the backup copy
            State.MyBO = New CompanyGroup
            State.IsNew = True
            PopulateFormFromBOs()
            EnableDisableFields()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Protected Sub DoDelete()
        Try
            State.MyBO.Delete()

            State.MyBO.Save()
            State.HasDataChanged = True
            ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Delete, State.MyBO, State.HasDataChanged))
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub EnableDisableFields()
        If State.IsNew Then
            ControlMgr.SetVisibleControl(Me, btnNew_WRITE, False)
            ControlMgr.SetVisibleControl(Me, btnDelete_WRITE, False)
            ControlMgr.SetVisibleControl(Me, btnUndo_Write, False)

        End If
    End Sub

    Protected Sub CheckIfComingFromSaveConfirm()
        Dim confResponse As String = HiddenSaveChangesPromptResponse.Value
        If confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_YES Then
            If State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr AndAlso State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.Accept Then
                BindBoPropertiesToLabels()
                State.MyBO.Save()
            End If
            Select Case State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.New_
                    MasterPage.MessageController.AddSuccess(ElitaPlus.ElitaPlusWebApp.Message.SAVE_RECORD_CONFIRMATION)
                    CreateNew()
                Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                    MasterPage.MessageController.AddSuccess(ElitaPlus.ElitaPlusWebApp.Message.SAVE_RECORD_CONFIRMATION)
                'Me.CreateNewWithCopy()
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    ReturnToCallingPage(New ReturnType(State.ActionInProgress, State.MyBO, State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.Delete
                    DoDelete()
            End Select
        ElseIf confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_NO Then
            Select Case State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.New_
                    CreateNew()
                Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                    'Me.CreateNewWithCopy()
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    MasterPage.MessageController.AddErrorAndShow(State.LastErrMsg)
                Case ElitaPlusPage.DetailPageCommand.Accept
                    EnableDisableFields()
                Case ElitaPlusPage.DetailPageCommand.Delete
            End Select
        End If
        State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
        HiddenSaveChangesPromptResponse.Value = ""
    End Sub
#End Region

#Region "Button Click"

    Private Sub btnSave_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnSave_WRITE.Click
        Try
            State.ForEdit = True
            PopulateBosFromForm()

            If State.MyBO.IsDirty Then
                State.MyBO.Save()
                State.IsNew = False
                State.HasDataChanged = True
                PopulateFormFromBOs()
                MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION, True)


            Else
                MasterPage.MessageController.AddError(Message.MSG_RECORD_NOT_SAVED, True)
            End If

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnUndo_Write_Click(sender As System.Object, e As System.EventArgs) Handles btnUndo_Write.Click
        Try
            If Not State.MyBO.IsNew Then
                'Reload from the DB
                State.MyBO = New CompanyGroup(State.MyBO.Id)
            ElseIf State.ScreenSnapShotBO IsNot Nothing Then
                'It was a new with copy
                State.MyBO.Clone(State.ScreenSnapShotBO)
            Else
                State.MyBO = New CompanyGroup

            End If
            PopulateFormFromBOs()

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnDelete_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnDelete_WRITE.Click


        State.MyBO = New CompanyGroup(State.MyBO.Id)
        Try
            DisplayMessage(Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
            State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete

        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
            State.MyBO.RejectChanges()
        End Try
    End Sub

    Private Sub btnNew_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnNew_WRITE.Click
        Try
            PopulateBosFromForm()
            If (State.MyBO.IsDirty) Then
                DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
            Else
                CreateNew()
                ControlMgr.SetEnableControl(Me, ddlInvoicegrpnumbering, True)
                ControlMgr.SetEnableControl(Me, ddlpmtgrpnumbering, True)
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    'Private Sub btnCopy_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopy_WRITE.Click
    '    Try
    '        Me.PopulateBOsFromForm()
    '        If (Me.State.MyBO.IsDirty) Then
    '            '    If (Me.State.MyBO.IsFamilyDirty) Then
    '            Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
    '            Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
    '        Else
    '            Me.CreateNewWithCopy()
    '        End If
    '    Catch ex As Exception
    '        Me.HandleErrors(ex, Me.MasterPage.MessageController)
    '    End Try
    'End Sub
    Private Sub btnBack_Click(sender As Object, e As System.EventArgs) Handles btnBack.Click
        Try
            PopulateBosFromForm()
            If (State.MyBO.IsDirty) Then

                DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
            Else
                ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.HasDataChanged))
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception

            HandleErrors(ex, MasterPage.MessageController)
            DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
            State.LastErrMsg = MasterPage.MessageController.Text
            MasterPage.MessageController.AddErrorAndShow(State.LastErrMsg)
        End Try
    End Sub
#End Region
End Class
