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

        Public Sub New(ByVal CompanygrpID As Guid)

            Me.CompanyGroupID = CompanygrpID
        End Sub
    End Class
#End Region
#Region "Page Return Type"
    Public Class ReturnType
        Public LastOperation As DetailPageCommand
        Public EditingBo As CompanyGroup
        Public BoChanged As Boolean = False
        Public Sub New(ByVal LastOp As DetailPageCommand, ByVal curEditingBo As CompanyGroup, Optional ByVal boChanged As Boolean = False)
            Me.LastOperation = LastOp
            Me.EditingBo = curEditingBo
            Me.BoChanged = boChanged
        End Sub
        Public Sub New(ByVal LastOp As DetailPageCommand)
            Me.LastOperation = LastOp
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

    Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
        Try
            If Not Me.CallingParameters Is Nothing Then
                'Get the id from the parent
                Me.State.Pageparameters = CType(Me.CallingParameters, Parameters)
                Me.State.MyBO = New CompanyGroup(Me.State.Pageparameters.CompanyGroupID)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

#End Region

#Region "Page Events"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.MasterPage.MessageController.Clear()
            Me.MasterPage.UsePageTabTitleInBreadCrum = False
            Me.MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(ADMIN)
            Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(COMPANY_GROUP_DETAIL)
            Me.UpdateBreadCrum()


            If Not Me.IsPostBack Then


                If Me.State.MyBO Is Nothing Then
                    Me.State.MyBO = New CompanyGroup
                    Me.State.IsNew = True
                End If

                If Me.State.IsNew Then
                    ControlMgr.SetEnableControl(Me, ddlInvoicegrpnumbering, True)
                    ControlMgr.SetEnableControl(Me, ddlpmtgrpnumbering, True)
                Else
                    ControlMgr.SetEnableControl(Me, ddlInvoicegrpnumbering, False)
                    ControlMgr.SetEnableControl(Me, ddlpmtgrpnumbering, False)
                End If


                Me.PopulateDropdowns()
                Me.PopulateFormFromBOs()
                EnableDisableFields()

            End If
            Me.CheckIfComingFromSaveConfirm()
            BindBoPropertiesToLabels()

            If Not Me.IsPostBack Then
                'BindBoPropertiesToLabels()
                Me.AddLabelDecorations(Me.State.MyBO)
            End If

        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
        Me.ShowMissingTranslations(Me.MasterPage.MessageController)

    End Sub
#End Region

    Private Sub UpdateBreadCrum()

        If (Not Me.State Is Nothing) Then
            Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator &
                TranslationBase.TranslateLabelOrMessage(COMPANY_GROUP_DETAIL)
        End If

    End Sub
#Region "Controlling Logic"
    Protected Sub PopulateFormFromBOs()
        Try
            Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            With Me.State.MyBO
                Me.PopulateControlFromBOProperty(Me.TextboxCompanyGroupName, .Description)
                Me.PopulateControlFromBOProperty(Me.TextboxCompanyGroupCode, .Code)
                Me.SetSelectedItem(Me.ddlClaimNumbering, .ClaimNumberingById)
                Me.SetSelectedItem(Me.ddlinvoicenumbering, .InvoiceNumberingById)
                Me.SetSelectedItem(Me.ddlInvoicegrpnumbering, .InvoiceGrpNumberingById)
                Me.SetSelectedItem(Me.ddlAuthorizationNumbering, .AuthorizationNumberingById)
                Me.SetSelectedItem(Me.ddlftpsite, .FtpSiteId)
                Me.SetSelectedItem(Me.ddlpmtgrpnumbering, .PaymentGrpNumberingById)
                Dim yesnoid As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, .AccountingByCompany)
                Me.SetSelectedItem(Me.ddlacctbycomp, yesnoid)
                Me.PopulateControlFromBOProperty(Me.txtyrstoinactiveusedvehicles, .InactiveUsedVehiclesOlderThan)
                Me.SetSelectedItem(Me.ddlinactivenewvehiclesbasedon, .InactiveNewVehiclesBasedOn)
                'req 5547
                Me.SetSelectedItem(Me.ddlFastApproval, .ClaimFastApprovalId)
                If .IsNew Or .ClaimFastApprovalId.Equals(Guid.Empty) Then
                    Me.SetSelectedItem(ddlFastApproval, LookupListNew.GetIdFromCode(LookupListNew.LK_FAST_APPROVAL_TYPE, "N"))
                End If
                'REQ-5773
                If .UseCommEntityTypeId.Equals(System.Guid.Empty) Then
                    Me.SetSelectedItem(Me.ddlUseCommEntityTypeId, LookupListNew.GetIdFromCode(LookupListNew.DropdownLookupList("YESNO", ElitaPlusIdentity.Current.ActiveUser.LanguageId), ""))
                Else
                    Me.SetSelectedItem(Me.ddlUseCommEntityTypeId, .UseCommEntityTypeId)
                End If


                'BindSelectItem(Me.State.MyBO.CaseNumberingByXcd, Me.ddlCaseNumbering)
                ' BindSelectItem(Me.State.MyBO.InteractionNumberingByXcd, Me.ddlInteractionNumbering)

                'REQ - 6155
                If .IsNew Or .CaseNumberingByXcd Is Nothing Then
                    Me.SetSelectedItem(Me.ddlCaseNumbering, "CASENUM-CMP") '"CASENUM-CMP"
                Else
                    Me.SetSelectedItem(Me.ddlCaseNumbering, .CaseNumberingByXcd)
                End If

                If .IsNew Or .InteractionNumberingByXcd Is Nothing Then
                    Me.SetSelectedItem(Me.ddlInteractionNumbering, "INTNUM-CMP")
                Else
                    Me.SetSelectedItem(Me.ddlInteractionNumbering, .InteractionNumberingByXcd)
                End If



            End With
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub BindBoPropertiesToLabels()
        Try
            Me.BindBOPropertyToLabel(Me.State.MyBO, "Description", Me.LabelCompanyGroupName)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "Code", Me.LabelCompanyGroupCode)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "ClaimNumberingById", Me.LabelClaimNumbering)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "InvoiceNumberingById", Me.LabelInvoiceNumbering)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "InvoiceGrpNumberingById", Me.lblInvoicegrpnumbering)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "AuthorizationNumberingById", Me.LabelAuthorizationNumbering)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "FtpSiteId", Me.lblftpsite)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "PaymentGrpNumberingById", Me.lblpmtgrpnumbering)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "AccountingByCompany", Me.lblAccountingbycompany)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "InactiveUsedVehiclesOlderThan", Me.lblyrstoinactiveusedvehicles)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "InactiveNewVehiclesBasedOn", Me.lblinactivenewvehiclesbasedon)
            'req 5547
            Me.BindBOPropertyToLabel(Me.State.MyBO, "ClaimFastApprovalId", Me.lblClaimFastApprovalId)
            'REQ-5773
            Me.BindBOPropertyToLabel(Me.State.MyBO, "UseCommEntityTypeId", Me.LabelUseCommEntityTypeId)

            'REQ - 6155
            Me.BindBOPropertyToLabel(Me.State.MyBO, "CaseNumberingByXcd", Me.LabelCaseNumbering)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "InteractionNumberingByXcd", Me.LabelInteractionNumbering)

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

    Protected Sub PopulateBosFromForm()
        Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
        Try
            Me.PopulateBOProperty(Me.State.MyBO, "Description", Me.TextboxCompanyGroupName)
            Me.PopulateBOProperty(Me.State.MyBO, "Code", Me.TextboxCompanyGroupCode)
            Me.PopulateBOProperty(Me.State.MyBO, "ClaimNumberingById", Me.ddlClaimNumbering)
            Me.PopulateBOProperty(Me.State.MyBO, "InvoiceNumberingById", Me.ddlinvoicenumbering)
            Me.PopulateBOProperty(Me.State.MyBO, "InvoiceGrpNumberingById", Me.ddlInvoicegrpnumbering)
            Me.PopulateBOProperty(Me.State.MyBO, "AuthorizationNumberingById", Me.ddlAuthorizationNumbering)
            Me.PopulateBOProperty(Me.State.MyBO, "FtpSiteId", Me.ddlftpsite)
            Me.PopulateBOProperty(Me.State.MyBO, "PaymentGrpNumberingById", Me.ddlpmtgrpnumbering)
            Dim acctbycomp As String = LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, New Guid(Me.ddlacctbycomp.SelectedValue))
            Me.PopulateBOProperty(Me.State.MyBO, "AccountingByCompany", acctbycomp)
            Me.PopulateBOProperty(Me.State.MyBO, "InactiveUsedVehiclesOlderThan", Me.txtyrstoinactiveusedvehicles)
            Me.PopulateBOProperty(Me.State.MyBO, "InactiveNewVehiclesBasedOn", Me.ddlinactivenewvehiclesbasedon)
            'req 5547
            Me.PopulateBOProperty(Me.State.MyBO, "ClaimFastApprovalId", Me.ddlFastApproval)
            'REQ-5773
            Me.PopulateBOProperty(Me.State.MyBO, "UseCommEntityTypeId", Me.ddlUseCommEntityTypeId)
            'REQ - 6155
            Me.PopulateBOProperty(Me.State.MyBO, "CaseNumberingByXcd", Me.ddlCaseNumbering, False, True)
            Me.PopulateBOProperty(Me.State.MyBO, "InteractionNumberingByXcd", Me.ddlInteractionNumbering, False, True)

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
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
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub CreateNew()
        Try
            Me.State.ScreenSnapShotBO = Nothing 'Reset the backup copy
            Me.State.MyBO = New CompanyGroup
            Me.State.IsNew = True
            Me.PopulateFormFromBOs()
            EnableDisableFields()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
    Protected Sub DoDelete()
        Try
            Me.State.MyBO.Delete()

            Me.State.MyBO.Save()
            Me.State.HasDataChanged = True
            Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Delete, Me.State.MyBO, Me.State.HasDataChanged))
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub EnableDisableFields()
        If Me.State.IsNew Then
            ControlMgr.SetVisibleControl(Me, btnNew_WRITE, False)
            ControlMgr.SetVisibleControl(Me, btnDelete_WRITE, False)
            ControlMgr.SetVisibleControl(Me, btnUndo_Write, False)

        End If
    End Sub

    Protected Sub CheckIfComingFromSaveConfirm()
        Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value
        If Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_YES Then
            If Me.State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr AndAlso Me.State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.Accept Then
                Me.BindBoPropertiesToLabels()
                Me.State.MyBO.Save()
            End If
            Select Case Me.State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.New_
                    Me.MasterPage.MessageController.AddSuccess(ElitaPlus.ElitaPlusWebApp.Message.SAVE_RECORD_CONFIRMATION)
                    Me.CreateNew()
                Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                    Me.MasterPage.MessageController.AddSuccess(ElitaPlus.ElitaPlusWebApp.Message.SAVE_RECORD_CONFIRMATION)
                'Me.CreateNewWithCopy()
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    Me.ReturnToCallingPage(New ReturnType(Me.State.ActionInProgress, Me.State.MyBO, Me.State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.Delete
                    Me.DoDelete()
            End Select
        ElseIf Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_NO Then
            Select Case Me.State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.New_
                    Me.CreateNew()
                Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                    'Me.CreateNewWithCopy()
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    Me.MasterPage.MessageController.AddErrorAndShow(Me.State.LastErrMsg)
                Case ElitaPlusPage.DetailPageCommand.Accept
                    Me.EnableDisableFields()
                Case ElitaPlusPage.DetailPageCommand.Delete
            End Select
        End If
        Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
        Me.HiddenSaveChangesPromptResponse.Value = ""
    End Sub
#End Region

#Region "Button Click"

    Private Sub btnSave_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave_WRITE.Click
        Try
            Me.State.ForEdit = True
            Me.PopulateBosFromForm()

            If Me.State.MyBO.IsDirty Then
                Me.State.MyBO.Save()
                Me.State.IsNew = False
                Me.State.HasDataChanged = True
                Me.PopulateFormFromBOs()
                Me.MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION, True)


            Else
                Me.MasterPage.MessageController.AddError(Message.MSG_RECORD_NOT_SAVED, True)
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnUndo_Write_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUndo_Write.Click
        Try
            If Not Me.State.MyBO.IsNew Then
                'Reload from the DB
                Me.State.MyBO = New CompanyGroup(Me.State.MyBO.Id)
            ElseIf Not Me.State.ScreenSnapShotBO Is Nothing Then
                'It was a new with copy
                Me.State.MyBO.Clone(Me.State.ScreenSnapShotBO)
            Else
                Me.State.MyBO = New CompanyGroup

            End If
            Me.PopulateFormFromBOs()

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnDelete_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete_WRITE.Click


        Me.State.MyBO = New CompanyGroup(Me.State.MyBO.Id)
        Try
            Me.DisplayMessage(Message.DELETE_RECORD_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
            Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete

        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
            Me.State.MyBO.RejectChanges()
        End Try
    End Sub

    Private Sub btnNew_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew_WRITE.Click
        Try
            Me.PopulateBosFromForm()
            If (Me.State.MyBO.IsDirty) Then
                Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
            Else
                Me.CreateNew()
                ControlMgr.SetEnableControl(Me, ddlInvoicegrpnumbering, True)
                ControlMgr.SetEnableControl(Me, ddlpmtgrpnumbering, True)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
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
    Private Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            Me.PopulateBosFromForm()
            If (Me.State.MyBO.IsDirty) Then

                Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
            Else
                Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.HasDataChanged))
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception

            Me.HandleErrors(ex, Me.MasterPage.MessageController)
            Me.DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
            Me.State.LastErrMsg = Me.MasterPage.MessageController.Text
            Me.MasterPage.MessageController.AddErrorAndShow(Me.State.LastErrMsg)
        End Try
    End Sub
#End Region
End Class
