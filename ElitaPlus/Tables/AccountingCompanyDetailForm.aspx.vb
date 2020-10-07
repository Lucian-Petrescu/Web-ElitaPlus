Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Namespace Tables

    Partial Class AccountingCompanyDetailForm
        Inherits ElitaPlusPage

#Region "Constants"

        Public Shared URL As String = "AccountingCompanyDetailForm.aspx"
        Public Const YES_VALUE As String = "'Y'"
        Public Const NO_VALUE As String = "'N'"
        Private Const YESNO As String = "YESNO"
        Public Const PAGETITLE As String = "ACCOUNTING_COMPANY"
        Public Const PAGETAB As String = "Admin"

        'Properties
        Public Const DESCRIPTION_PROPERTY As String = "Description"
        Public Const CODE_PROPERTY As String = "Code"
        Public Const FTPDIRECTORY_PROPERTY As String = "FTPDirectory"
        Public Const BALANCEDIRECTORY_PROPERTY As String = "BalanceDirectory"
        Public Const ACCOUNTING_SYSTEM_PROPERTY As String = "AcctSystemId"
        Public Const USE_ACCOUNTING_PROPERTY As String = "UseAccounting"
        Public Const PROCESSMETHOD_PROPERTY As String = "ProcessMethodId"
        Public Const USE_BANK_INFO_PROPERTY As String = "UseElitaBankInfoId"
        Public Const RPT_COMMISSION_PROPERTY As String = "ReportCommissionBreakdown"
        Public Const NOTIFYEMAIL_PROPERTY As String = "NotifyEmail"
        Public Const USE_CVG_ENTITY_PROPERTY As String = "UseCoverageEntityId"
        Public Const CVG_ENTITY_BY_REGION_PROPERTY As String = "CoverageEntityByRegion"


#End Region


#Region "Page State"

        Class MyState
            Public MyBO As AcctCompany
            Public ScreenSnapShotBO As AcctCompany
            Public IsNew As Boolean = False
            Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
            Public LastErrMsg As String
            Public HasDataChanged As Boolean
            Public YESNOdv As DataView
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
                    Dim CalPar As ReturnType = CType(CallingPar, ReturnType)
                    If Not CalPar.SelectedGuid.Equals(Guid.Empty) Then
                        State.MyBO = New AcctCompany(CalPar.SelectedGuid)
                        Exit Sub
                    End If
                End If
                State.IsNew = True
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub



#End Region

#Region "Page Return Type"
        Public Class ReturnType
            Public EditingBo As AcctCompany
            Public SelectedGuid As Guid = Guid.Empty

            Public Sub New(AcctCompanyId As Guid, curEditingBo As AcctCompany)
                EditingBo = curEditingBo
                SelectedGuid = AcctCompanyId
            End Sub

        End Class
#End Region


#Region "Page Events"

        Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

            ErrControllerMaster.Clear_Hide()

            SetFormTitle(PAGETITLE)
            SetFormTab(PAGETAB)

            Try
                If Not IsPostBack Then
                    MenuEnabled = False
                    AddConfirmation(btnDelete_WRITE, Message.DELETE_RECORD_PROMPT)

                    PopulateAll()

                    If State.IsNew = True Then
                        CreateNew()
                    End If

                    PopulateFormFromBOs()
                    EnableDisableFields()

                End If
                BindBoPropertiesToLabels()
                CheckIfComingFromSaveConfirm()
                If Not IsPostBack Then
                    AddLabelDecorations(State.MyBO)
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
            ShowMissingTranslations(ErrControllerMaster)
        End Sub




#End Region

#Region "Controlling Logic"

        Private Sub PopulateAll()

            Dim YESNOdv As DataView = LookupListNew.DropdownLookupList(YesNo, ElitaPlusIdentity.Current.ActiveUser.LanguageId, True)
            State.YESNOdv = YESNOdv

            'Me.BindListControlToDataView(Me.UseAccountingDropdown, YESNOdv)
            'Me.BindListControlToDataView(Me.RptCommissionDropDown, YESNOdv)
            'Me.BindListControlToDataView(Me.UseElitaBankInfoDropdown, YESNOdv)
            'Me.BindListControlToDataView(Me.CoverageEntityByRegionDropdown, YESNOdv)
            'Me.BindListControlToDataView(Me.UseCoverageEntityDropdown, YESNOdv)
            'Me.BindListControlToDataView(Me.AccountingSystemDropDown, LookupListNew.DropdownLookupList(LookupListNew.LK_ACCT_SYSTEM, ElitaPlusIdentity.Current.ActiveUser.LanguageId, True))
            'Me.BindListControlToDataView(Me.ProcessMethodDropDown, LookupListNew.DropdownLookupList(LookupListNew.LK_ACCOUNTING_PROCESS_METHOD, ElitaPlusIdentity.Current.ActiveUser.LanguageId, True))

            Dim populateOptions = New PopulateOptions() With
                                {
                                   .AddBlankItem = True
                                }

            Dim YesNoList As DataElements.ListItem() =
               CommonConfigManager.Current.ListManager.GetList(listCode:="YESNO",
                                                               languageCode:=ElitaPlusIdentity.Current.ActiveUser.LanguageCode)

            UseAccountingDropdown.Populate(YesNoList.ToArray(), populateOptions)
            RptCommissionDropDown.Populate(YesNoList.ToArray(), populateOptions)
            UseElitaBankInfoDropdown.Populate(YesNoList.ToArray(), populateOptions)
            CoverageEntityByRegionDropdown.Populate(YesNoList.ToArray(), populateOptions)
            UseCoverageEntityDropdown.Populate(YesNoList.ToArray(), populateOptions)

            Dim AccountingSystem As DataElements.ListItem() =
               CommonConfigManager.Current.ListManager.GetList(listCode:="ACCTSYS",
                                                               languageCode:=ElitaPlusIdentity.Current.ActiveUser.LanguageCode)

            AccountingSystemDropDown.Populate(AccountingSystem.ToArray(), populateOptions)

            Dim ProcessMethod As DataElements.ListItem() =
               CommonConfigManager.Current.ListManager.GetList(listCode:="ACCTPROC",
                                                               languageCode:=ElitaPlusIdentity.Current.ActiveUser.LanguageCode)

            ProcessMethodDropDown.Populate(ProcessMethod.ToArray(), populateOptions)

        End Sub

        Protected Sub PopulateFormFromBOs()

            Dim YesNodv As DataView = State.YESNOdv

            With State.MyBO

                'fill text boxes
                PopulateControlFromBOProperty(DescriptionTextBox, .Description)
                PopulateControlFromBOProperty(CodeTextBox, .Code)
                PopulateControlFromBOProperty(ftpDirectoryTextBox, .FTPDirectory)
                PopulateControlFromBOProperty(balanceDirectoryTextBox, .BalanceDirectory)
                PopulateControlFromBOProperty(notifyEmailTextBox, .NotifyEmail)

                'fill dropdowns
                PopulateControlFromBOProperty(AccountingSystemDropDown, .AcctSystemId)
                PopulateControlFromBOProperty(UseAccountingDropdown, LookupListNew.GetIdFromCode(YesNodv, .UseAccounting))

                PopulateControlFromBOProperty(ProcessMethodDropDown, .ProcessMethodId)
                PopulateControlFromBOProperty(UseElitaBankInfoDropdown, .UseElitaBankInfoId)
                PopulateControlFromBOProperty(RptCommissionDropDown, LookupListNew.GetIdFromCode(YesNodv, .ReportCommissionBreakdown))
                PopulateControlFromBOProperty(UseCoverageEntityDropdown, .UseCoverageEntityId)
                PopulateControlFromBOProperty(CoverageEntityByRegionDropdown, .CoverageEntityByRegion)

            End With

        End Sub

        Protected Sub PopulateBOsFromForm()

            With State

                'fill text boxes
                PopulateBOProperty(.MyBO, DESCRIPTION_PROPERTY, DescriptionTextBox)
                PopulateBOProperty(.MyBO, CODE_PROPERTY, CodeTextBox)
                PopulateBOProperty(.MyBO, FTPDIRECTORY_PROPERTY, ftpDirectoryTextBox)
                PopulateBOProperty(.MyBO, BALANCEDIRECTORY_PROPERTY, balanceDirectoryTextBox)
                PopulateBOProperty(.MyBO, NOTIFYEMAIL_PROPERTY, notifyEmailTextBox)

                'fill dropdowns
                PopulateBOProperty(.MyBO, ACCOUNTING_SYSTEM_PROPERTY, AccountingSystemDropDown)

                PopulateBOProperty(.MyBO, PROCESSMETHOD_PROPERTY, ProcessMethodDropDown)
                PopulateBOProperty(.MyBO, USE_BANK_INFO_PROPERTY, UseElitaBankInfoDropdown)
                PopulateBOProperty(.MyBO, USE_CVG_ENTITY_PROPERTY, UseCoverageEntityDropdown)
                PopulateBOProperty(.MyBO, CVG_ENTITY_BY_REGION_PROPERTY, CoverageEntityByRegionDropdown)


                PopulateBOProperty(.MyBO, USE_ACCOUNTING_PROPERTY, LookupListNew.GetCodeFromId(.YESNOdv, New Guid(UseAccountingDropdown.SelectedItem.Value)))
                PopulateBOProperty(.MyBO, RPT_COMMISSION_PROPERTY, LookupListNew.GetCodeFromId(.YESNOdv, New Guid(RptCommissionDropDown.SelectedItem.Value)))


            End With

        End Sub

        Protected Sub BindBoPropertiesToLabels()

            With State

                BindBOPropertyToLabel(.MyBO, DESCRIPTION_PROPERTY, DescriptionLabel)
                BindBOPropertyToLabel(.MyBO, CODE_PROPERTY, CodeLabel)
                BindBOPropertyToLabel(.MyBO, FTPDIRECTORY_PROPERTY, ftpDirectoryLabel)
                BindBOPropertyToLabel(.MyBO, BALANCEDIRECTORY_PROPERTY, balanceDirectoryLabel)
                BindBOPropertyToLabel(.MyBO, NOTIFYEMAIL_PROPERTY, notifyEmailLabel)
                BindBOPropertyToLabel(.MyBO, ACCOUNTING_SYSTEM_PROPERTY, AccountingSystemLabel)
                BindBOPropertyToLabel(.MyBO, PROCESSMETHOD_PROPERTY, ProcessMethodLabel)
                BindBOPropertyToLabel(.MyBO, USE_BANK_INFO_PROPERTY, UseElitaBankInfoLabel)
                BindBOPropertyToLabel(.MyBO, USE_CVG_ENTITY_PROPERTY, UseCoverageEntityLabel)
                BindBOPropertyToLabel(.MyBO, CVG_ENTITY_BY_REGION_PROPERTY, CoverageEntityByRegionLabel)
                BindBOPropertyToLabel(.MyBO, USE_ACCOUNTING_PROPERTY, UseAccountingLabel)
                BindBOPropertyToLabel(.MyBO, RPT_COMMISSION_PROPERTY, RptCommissionLabel)

            End With

        End Sub

        Protected Sub EnableDisableFields()
            'Enabled by Default
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, True)
            ControlMgr.SetEnableControl(Me, btnBack, True)
            ControlMgr.SetEnableControl(Me, btnSave_WRITE, True)
            ControlMgr.SetEnableControl(Me, btnUndo_Write, True)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, True)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, True)

            If State.MyBO.IsNew Then
                ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
                ControlMgr.SetEnableControl(Me, btnUndo_Write, False)
                ControlMgr.SetEnableControl(Me, btnCopy_WRITE, False)
                ControlMgr.SetEnableControl(Me, btnNew_WRITE, False)
            End If

        End Sub

        Protected Sub CheckIfComingFromSaveConfirm()
            Dim confResponse As String = HiddenSaveChangesPromptResponse.Value
            If confResponse IsNot Nothing AndAlso confResponse = CONFIRM_MESSAGE_OK Then
                If State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr Then
                    State.MyBO.Save()
                End If
                Select Case State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Back
                        ReturnToCallingPage(New AccountingCompanyForm.ReturnType(State.ActionInProgress, State.MyBO, State.HasDataChanged))
                    Case ElitaPlusPage.DetailPageCommand.New_
                        AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                        CreateNew()
                    Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                        AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                        CreateNewWithCopy()
                    Case ElitaPlusPage.DetailPageCommand.BackOnErr
                        ReturnToCallingPage(New AccountingCompanyForm.ReturnType(State.ActionInProgress, State.MyBO, State.HasDataChanged))
                End Select
            ElseIf confResponse IsNot Nothing AndAlso confResponse = CONFIRM_MESSAGE_CANCEL Then
                Select Case State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Back
                        ReturnToCallingPage(New AccountingCompanyForm.ReturnType(State.ActionInProgress, State.MyBO, State.HasDataChanged))
                    Case ElitaPlusPage.DetailPageCommand.New_
                        CreateNew()
                        'Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                        '    Me.CreateNewWithCopy()
                    Case ElitaPlusPage.DetailPageCommand.BackOnErr
                        ErrControllerMaster.AddErrorAndShow(State.LastErrMsg)
                End Select
            End If
            'Clean after consuming the action
            State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
            HiddenSaveChangesPromptResponse.Value = ""
        End Sub


        Protected Sub CreateNew()

            State.ScreenSnapShotBO = Nothing
            State.MyBO = New AcctCompany
            State.IsNew = True
            PopulateFormFromBOs()
            EnableDisableFields()

        End Sub

        Protected Sub CreateNewWithCopy()

            Dim newBO As New AcctCompany
            State.MyBO = newBO

            State.IsNew = True
            EnableDisableFields()
            PopulateBOsFromForm()

            State.MyBO.Description = Nothing
            DescriptionTextBox.Text = String.Empty

            'create the backup copy
            State.ScreenSnapShotBO = New AcctCompany
            State.ScreenSnapShotBO.Clone(State.MyBO)

        End Sub

#End Region


#Region "Button Clicks"

        Private Sub btnBack_Click(sender As Object, e As System.EventArgs) Handles btnBack.Click
            Try
                PopulateBOsFromForm()
                If State.MyBO.IsDirty Then
                    AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    ReturnToCallingPage(New AccountingCompanyForm.ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.HasDataChanged))
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
                AddConfirmMsg(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
                State.LastErrMsg = ErrControllerMaster.Text
            End Try
        End Sub

        Private Sub btnUndo_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnUndo_Write.Click
            Try
                If Not State.MyBO.IsNew Then
                    'Reload from the DB
                    State.MyBO = New AcctCompany(State.MyBO.Id)
                ElseIf State.ScreenSnapShotBO IsNot Nothing Then
                    'It was a new with copy
                    State.MyBO.Clone(State.ScreenSnapShotBO)
                Else
                    CreateNew()
                End If
                PopulateAll()
                PopulateFormFromBOs()
                EnableDisableFields()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub btnApply_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnSave_WRITE.Click
            Try
                PopulateBOsFromForm()
                If State.MyBO.IsDirty Then
                    State.MyBO.Save()
                    State.HasDataChanged = True
                    PopulateFormFromBOs()
                    EnableDisableFields()
                    AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                Else
                    AddInfoMsg(Message.MSG_RECORD_NOT_SAVED)
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub btnDelete_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnDelete_WRITE.Click
            Try
                State.MyBO.Delete()
                State.MyBO.Save()
                State.HasDataChanged = True
                ReturnToCallingPage(New AccountingCompanyForm.ReturnType(State.ActionInProgress, State.MyBO, State.HasDataChanged))
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                'undo the delete
                State.MyBO.RejectChanges()
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Protected Sub btnCopy_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnCopy_WRITE.Click
            Try
                PopulateBOsFromForm()
                If State.MyBO.IsDirty Then
                    AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
                Else
                    CreateNewWithCopy()
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub btnNew_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnNew_WRITE.Click
            Try
                PopulateBOsFromForm()
                If (State.MyBO.IsDirty) Then
                    AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
                Else
                    CreateNew()
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub
#End Region
    End Class

End Namespace
