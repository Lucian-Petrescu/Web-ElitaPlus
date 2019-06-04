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

        Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
            Try
                If Not Me.CallingParameters Is Nothing Then
                    Dim CalPar As ReturnType = CType(CallingPar, ReturnType)
                    If Not CalPar.SelectedGuid.Equals(Guid.Empty) Then
                        Me.State.MyBO = New AcctCompany(CalPar.SelectedGuid)
                        Exit Sub
                    End If
                End If
                Me.State.IsNew = True
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub



#End Region

#Region "Page Return Type"
        Public Class ReturnType
            Public EditingBo As AcctCompany
            Public SelectedGuid As Guid = Guid.Empty

            Public Sub New(ByVal AcctCompanyId As Guid, ByVal curEditingBo As AcctCompany)
                Me.EditingBo = curEditingBo
                SelectedGuid = AcctCompanyId
            End Sub

        End Class
#End Region


#Region "Page Events"

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

            Me.ErrControllerMaster.Clear_Hide()

            Me.SetFormTitle(PAGETITLE)
            Me.SetFormTab(PAGETAB)

            Try
                If Not Me.IsPostBack Then
                    Me.MenuEnabled = False
                    Me.AddConfirmation(Me.btnDelete_WRITE, Message.DELETE_RECORD_PROMPT)

                    PopulateAll()

                    If Me.State.IsNew = True Then
                        CreateNew()
                    End If

                    Me.PopulateFormFromBOs()
                    Me.EnableDisableFields()

                End If
                BindBoPropertiesToLabels()
                CheckIfComingFromSaveConfirm()
                If Not Me.IsPostBack Then
                    Me.AddLabelDecorations(Me.State.MyBO)
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
            Me.ShowMissingTranslations(Me.ErrControllerMaster)
        End Sub




#End Region

#Region "Controlling Logic"

        Private Sub PopulateAll()

            Dim YESNOdv As DataView = LookupListNew.DropdownLookupList(YesNo, ElitaPlusIdentity.Current.ActiveUser.LanguageId, True)
            Me.State.YESNOdv = YESNOdv

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

            Me.UseAccountingDropdown.Populate(YesNoList.ToArray(), populateOptions)
            Me.RptCommissionDropDown.Populate(YesNoList.ToArray(), populateOptions)
            Me.UseElitaBankInfoDropdown.Populate(YesNoList.ToArray(), populateOptions)
            Me.CoverageEntityByRegionDropdown.Populate(YesNoList.ToArray(), populateOptions)
            Me.UseCoverageEntityDropdown.Populate(YesNoList.ToArray(), populateOptions)

            Dim AccountingSystem As DataElements.ListItem() =
               CommonConfigManager.Current.ListManager.GetList(listCode:="ACCTSYS",
                                                               languageCode:=ElitaPlusIdentity.Current.ActiveUser.LanguageCode)

            Me.AccountingSystemDropDown.Populate(AccountingSystem.ToArray(), populateOptions)

            Dim ProcessMethod As DataElements.ListItem() =
               CommonConfigManager.Current.ListManager.GetList(listCode:="ACCTPROC",
                                                               languageCode:=ElitaPlusIdentity.Current.ActiveUser.LanguageCode)

            Me.ProcessMethodDropDown.Populate(ProcessMethod.ToArray(), populateOptions)

        End Sub

        Protected Sub PopulateFormFromBOs()

            Dim YesNodv As DataView = Me.State.YESNOdv

            With Me.State.MyBO

                'fill text boxes
                Me.PopulateControlFromBOProperty(Me.DescriptionTextBox, .Description)
                Me.PopulateControlFromBOProperty(Me.CodeTextBox, .Code)
                Me.PopulateControlFromBOProperty(Me.ftpDirectoryTextBox, .FTPDirectory)
                Me.PopulateControlFromBOProperty(Me.balanceDirectoryTextBox, .BalanceDirectory)
                Me.PopulateControlFromBOProperty(Me.notifyEmailTextBox, .NotifyEmail)

                'fill dropdowns
                Me.PopulateControlFromBOProperty(Me.AccountingSystemDropDown, .AcctSystemId)
                Me.PopulateControlFromBOProperty(Me.UseAccountingDropdown, LookupListNew.GetIdFromCode(YesNodv, .UseAccounting))

                Me.PopulateControlFromBOProperty(Me.ProcessMethodDropDown, .ProcessMethodId)
                Me.PopulateControlFromBOProperty(Me.UseElitaBankInfoDropdown, .UseElitaBankInfoId)
                Me.PopulateControlFromBOProperty(Me.RptCommissionDropDown, LookupListNew.GetIdFromCode(YesNodv, .ReportCommissionBreakdown))
                Me.PopulateControlFromBOProperty(Me.UseCoverageEntityDropdown, .UseCoverageEntityId)
                Me.PopulateControlFromBOProperty(Me.CoverageEntityByRegionDropdown, .CoverageEntityByRegion)

            End With

        End Sub

        Protected Sub PopulateBOsFromForm()

            With Me.State

                'fill text boxes
                Me.PopulateBOProperty(.MyBO, Me.DESCRIPTION_PROPERTY, Me.DescriptionTextBox)
                Me.PopulateBOProperty(.MyBO, Me.CODE_PROPERTY, Me.CodeTextBox)
                Me.PopulateBOProperty(.MyBO, Me.FTPDIRECTORY_PROPERTY, Me.ftpDirectoryTextBox)
                Me.PopulateBOProperty(.MyBO, Me.BALANCEDIRECTORY_PROPERTY, Me.balanceDirectoryTextBox)
                Me.PopulateBOProperty(.MyBO, Me.NOTIFYEMAIL_PROPERTY, Me.notifyEmailTextBox)

                'fill dropdowns
                Me.PopulateBOProperty(.MyBO, Me.ACCOUNTING_SYSTEM_PROPERTY, Me.AccountingSystemDropDown)

                Me.PopulateBOProperty(.MyBO, Me.PROCESSMETHOD_PROPERTY, Me.ProcessMethodDropDown)
                Me.PopulateBOProperty(.MyBO, Me.USE_BANK_INFO_PROPERTY, Me.UseElitaBankInfoDropdown)
                Me.PopulateBOProperty(.MyBO, Me.USE_CVG_ENTITY_PROPERTY, Me.UseCoverageEntityDropdown)
                Me.PopulateBOProperty(.MyBO, Me.CVG_ENTITY_BY_REGION_PROPERTY, Me.CoverageEntityByRegionDropdown)


                Me.PopulateBOProperty(.MyBO, Me.USE_ACCOUNTING_PROPERTY, LookupListNew.GetCodeFromId(.YESNOdv, New Guid(Me.UseAccountingDropdown.SelectedItem.Value)))
                Me.PopulateBOProperty(.MyBO, Me.RPT_COMMISSION_PROPERTY, LookupListNew.GetCodeFromId(.YESNOdv, New Guid(Me.RptCommissionDropDown.SelectedItem.Value)))


            End With

        End Sub

        Protected Sub BindBoPropertiesToLabels()

            With Me.State

                Me.BindBOPropertyToLabel(.MyBO, Me.DESCRIPTION_PROPERTY, Me.DescriptionLabel)
                Me.BindBOPropertyToLabel(.MyBO, Me.CODE_PROPERTY, Me.CodeLabel)
                Me.BindBOPropertyToLabel(.MyBO, Me.FTPDIRECTORY_PROPERTY, Me.ftpDirectoryLabel)
                Me.BindBOPropertyToLabel(.MyBO, Me.BALANCEDIRECTORY_PROPERTY, Me.balanceDirectoryLabel)
                Me.BindBOPropertyToLabel(.MyBO, Me.NOTIFYEMAIL_PROPERTY, Me.notifyEmailLabel)
                Me.BindBOPropertyToLabel(.MyBO, Me.ACCOUNTING_SYSTEM_PROPERTY, Me.AccountingSystemLabel)
                Me.BindBOPropertyToLabel(.MyBO, Me.PROCESSMETHOD_PROPERTY, Me.ProcessMethodLabel)
                Me.BindBOPropertyToLabel(.MyBO, Me.USE_BANK_INFO_PROPERTY, Me.UseElitaBankInfoLabel)
                Me.BindBOPropertyToLabel(.MyBO, Me.USE_CVG_ENTITY_PROPERTY, Me.UseCoverageEntityLabel)
                Me.BindBOPropertyToLabel(.MyBO, Me.CVG_ENTITY_BY_REGION_PROPERTY, Me.CoverageEntityByRegionLabel)
                Me.BindBOPropertyToLabel(.MyBO, Me.USE_ACCOUNTING_PROPERTY, Me.UseAccountingLabel)
                Me.BindBOPropertyToLabel(.MyBO, Me.RPT_COMMISSION_PROPERTY, Me.RptCommissionLabel)

            End With

        End Sub

        Protected Sub EnableDisableFields()
            'Enabled by Default
            ControlMgr.SetEnableControl(Me, Me.btnDelete_WRITE, True)
            ControlMgr.SetEnableControl(Me, Me.btnBack, True)
            ControlMgr.SetEnableControl(Me, Me.btnSave_WRITE, True)
            ControlMgr.SetEnableControl(Me, Me.btnUndo_Write, True)
            ControlMgr.SetEnableControl(Me, Me.btnCopy_WRITE, True)
            ControlMgr.SetEnableControl(Me, Me.btnNew_WRITE, True)

            If Me.State.MyBO.IsNew Then
                ControlMgr.SetEnableControl(Me, Me.btnDelete_WRITE, False)
                ControlMgr.SetEnableControl(Me, Me.btnUndo_Write, False)
                ControlMgr.SetEnableControl(Me, Me.btnCopy_WRITE, False)
                ControlMgr.SetEnableControl(Me, Me.btnNew_WRITE, False)
            End If

        End Sub

        Protected Sub CheckIfComingFromSaveConfirm()
            Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value
            If Not confResponse Is Nothing AndAlso confResponse = Me.CONFIRM_MESSAGE_OK Then
                If Me.State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr Then
                    Me.State.MyBO.Save()
                End If
                Select Case Me.State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Back
                        Me.ReturnToCallingPage(New AccountingCompanyForm.ReturnType(Me.State.ActionInProgress, Me.State.MyBO, Me.State.HasDataChanged))
                    Case ElitaPlusPage.DetailPageCommand.New_
                        Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                        Me.CreateNew()
                    Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                        Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                        Me.CreateNewWithCopy()
                    Case ElitaPlusPage.DetailPageCommand.BackOnErr
                        Me.ReturnToCallingPage(New AccountingCompanyForm.ReturnType(Me.State.ActionInProgress, Me.State.MyBO, Me.State.HasDataChanged))
                End Select
            ElseIf Not confResponse Is Nothing AndAlso confResponse = Me.CONFIRM_MESSAGE_CANCEL Then
                Select Case Me.State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Back
                        Me.ReturnToCallingPage(New AccountingCompanyForm.ReturnType(Me.State.ActionInProgress, Me.State.MyBO, Me.State.HasDataChanged))
                    Case ElitaPlusPage.DetailPageCommand.New_
                        Me.CreateNew()
                        'Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                        '    Me.CreateNewWithCopy()
                    Case ElitaPlusPage.DetailPageCommand.BackOnErr
                        Me.ErrControllerMaster.AddErrorAndShow(Me.State.LastErrMsg)
                End Select
            End If
            'Clean after consuming the action
            Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
            Me.HiddenSaveChangesPromptResponse.Value = ""
        End Sub


        Protected Sub CreateNew()

            Me.State.ScreenSnapShotBO = Nothing
            Me.State.MyBO = New AcctCompany
            Me.State.IsNew = True
            Me.PopulateFormFromBOs()
            Me.EnableDisableFields()

        End Sub

        Protected Sub CreateNewWithCopy()

            Dim newBO As New AcctCompany
            Me.State.MyBO = newBO

            Me.State.IsNew = True
            Me.EnableDisableFields()
            Me.PopulateBOsFromForm()

            Me.State.MyBO.Description = Nothing
            DescriptionTextBox.Text = String.Empty

            'create the backup copy
            Me.State.ScreenSnapShotBO = New AcctCompany
            Me.State.ScreenSnapShotBO.Clone(Me.State.MyBO)

        End Sub

#End Region


#Region "Button Clicks"

        Private Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
            Try
                Me.PopulateBOsFromForm()
                If Me.State.MyBO.IsDirty Then
                    Me.AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    Me.ReturnToCallingPage(New AccountingCompanyForm.ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.HasDataChanged))
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
                Me.AddConfirmMsg(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
                Me.State.LastErrMsg = Me.ErrControllerMaster.Text
            End Try
        End Sub

        Private Sub btnUndo_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUndo_Write.Click
            Try
                If Not Me.State.MyBO.IsNew Then
                    'Reload from the DB
                    Me.State.MyBO = New AcctCompany(Me.State.MyBO.Id)
                ElseIf Not Me.State.ScreenSnapShotBO Is Nothing Then
                    'It was a new with copy
                    Me.State.MyBO.Clone(Me.State.ScreenSnapShotBO)
                Else
                    CreateNew()
                End If
                PopulateAll()
                Me.PopulateFormFromBOs()
                Me.EnableDisableFields()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Private Sub btnApply_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave_WRITE.Click
            Try
                Me.PopulateBOsFromForm()
                If Me.State.MyBO.IsDirty Then
                    Me.State.MyBO.Save()
                    Me.State.HasDataChanged = True
                    Me.PopulateFormFromBOs()
                    Me.EnableDisableFields()
                    Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                Else
                    Me.AddInfoMsg(Message.MSG_RECORD_NOT_SAVED)
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Private Sub btnDelete_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete_WRITE.Click
            Try
                Me.State.MyBO.Delete()
                Me.State.MyBO.Save()
                Me.State.HasDataChanged = True
                Me.ReturnToCallingPage(New AccountingCompanyForm.ReturnType(Me.State.ActionInProgress, Me.State.MyBO, Me.State.HasDataChanged))
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                'undo the delete
                Me.State.MyBO.RejectChanges()
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Protected Sub btnCopy_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopy_WRITE.Click
            Try
                Me.PopulateBOsFromForm()
                If Me.State.MyBO.IsDirty Then
                    Me.AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
                Else
                    Me.CreateNewWithCopy()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Private Sub btnNew_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew_WRITE.Click
            Try
                Me.PopulateBOsFromForm()
                If (Me.State.MyBO.IsDirty) Then
                    Me.AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
                Else
                    Me.CreateNew()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub
#End Region
    End Class

End Namespace
