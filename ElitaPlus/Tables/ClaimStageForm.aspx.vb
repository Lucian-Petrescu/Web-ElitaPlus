Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports Assurant.Elita.Web.Forms
Imports System.Threading

Namespace Tables
    Partial Public Class ClaimStageForm
        Inherits ElitaPlusPage

#Region "Constants"
        Public Const URL As String = "ClaimStageForm.aspx"
        Public Const PAGETITLE As String = "CLAIM_STAGE"
        Public Const PAGETAB As String = "TABLES"
        Public Const SUMMARYTITLE As String = "CLAIM_STAGE"
        Public Const UC_END_STATUS_AVASEL_AVA_TEXT_COLUMN = "DESCRIPTION"
        Public Const UC_END_STATUS_AVASEL_AVA_GUID_COLUMN = "ID"
        Public Const UC_END_STATUS_AVASEL_SEL_TEXT_COLUMN = "DESCRIPTION"
        Public Const UC_END_STATUS_AVASEL_SEL_GUID_COLUMN = "ID"
#End Region

#Region "Variables"
        Private mbIsFirstPass As Boolean = True
#End Region

#Region "Page State"
        Class MyState
            Public MyBO As ClaimStage
            Public ScreenSnapShotBO As ClaimStage
            'Public MyChildStageEndStatusList As ArrayList

            Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
            Public LastErrMsg As String
            Public HasDataChanged As Boolean
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

#Region "Page Events"
        Public Class ReturnType
            Public LastOperation As DetailPageCommand
            Public EditingBo As ClaimStage
            Public HasDataChanged As Boolean
            Public Sub New(LastOp As DetailPageCommand, curEditingBo As ClaimStage, hasDataChanged As Boolean)
                LastOperation = LastOp
                EditingBo = curEditingBo
                Me.HasDataChanged = hasDataChanged
            End Sub
        End Class

        Private Sub UpdateBreadCrum()
            MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(PAGETAB)
            MasterPage.UsePageTabTitleInBreadCrum = False
            MasterPage.BreadCrum = TranslationBase.TranslateLabelOrMessage(PAGETAB) + ElitaBase.Sperator + TranslationBase.TranslateLabelOrMessage(PAGETITLE)
        End Sub

        Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
            If mbIsFirstPass = True Then
                mbIsFirstPass = False
            Else
                ' Do not load again the Page that was already loaded
                Return
            End If

            MasterPage.MessageController.Clear()
            UpdateBreadCrum()

            Try
                If Not IsPostBack Then
                    If State.MyBO Is Nothing Then
                        State.MyBO = New ClaimStage
                    End If

                    ' Setting Calendar
                    AddCalendarwithTime_New(imgEffectiveDate, txtEffectiveDate)
                    AddCalendarwithTime_New(imgExpirationDate, txtExpirationDate)

                    PopulateDropdowns()
                    PopulateFormFromBOs()
                    EnableDisableFields()

                    AddLabelDecorations(State.MyBO)
                End If
                BindBoPropertiesToLabels()
                CheckIfComingFromSaveConfirm()
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                CleanPopupInput()
                HandleErrors(ex, MasterPage.MessageController)
            End Try
            ShowMissingTranslations(MasterPage.MessageController)
        End Sub

        Private Sub Page_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall
            Try
                If CallingParameters IsNot Nothing Then
                    'Get the id from the parent
                    Dim objID As Guid = CType(CallingParameters, Guid)
                    If objID <> Guid.Empty Then
                        State.MyBO = New ClaimStage(objID, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                    End If
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

#End Region

#Region "Helper functions"

        Private Sub CleanPopupInput()
            Try
                If State IsNot Nothing Then
                    'Clean after consuming the action 
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                    HiddenSaveChangesPromptResponse.Value = ""
                End If
            Catch ex As Exception

            End Try
        End Sub

        Protected Sub BindBoPropertiesToLabels()
            Try
                BindBOPropertyToLabel(State.MyBO, "StageNameId", lblStageName)
                BindBOPropertyToLabel(State.MyBO, "CompanyGroupId", lblCompnayGrp)
                BindBOPropertyToLabel(State.MyBO, "CompanyId", lblCompany)
                BindBOPropertyToLabel(State.MyBO, "DealerId", lblDealer)
                BindBOPropertyToLabel(State.MyBO, "ProductCode", lblProdCode)
                BindBOPropertyToLabel(State.MyBO, "CoverageTypeId", lblCoverageType)
                BindBOPropertyToLabel(State.MyBO, "EffectiveDate", lblEffectiveDate)
                BindBOPropertyToLabel(State.MyBO, "ExpirationDate", lblExpirationDate)
                BindBOPropertyToLabel(State.MyBO, "Sequence", lblSequence)
                BindBOPropertyToLabel(State.MyBO, "ScreenId", lblScreen)
                BindBOPropertyToLabel(State.MyBO, "PortalId", lblPortal)
                BindBOPropertyToLabel(State.MyBO, "StatusStartId", lblStartStatus)

                ClearGridViewHeadersAndLabelsErrorSign()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub CheckIfComingFromSaveConfirm()
            Try
                Dim confResponse As String = HiddenSaveChangesPromptResponse.Value
                Dim actionInProgress As ElitaPlusPage.DetailPageCommand = State.ActionInProgress
                If confResponse IsNot Nothing AndAlso (confResponse = MSG_VALUE_YES OrElse confResponse.ToUpper = "OK") Then
                    If actionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr Then
                        State.MyBO.Save()
                        State.HasDataChanged = True
                    End If
                    Select Case State.ActionInProgress
                        Case ElitaPlusPage.DetailPageCommand.Back
                            ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.HasDataChanged))
                        Case ElitaPlusPage.DetailPageCommand.New_
                            MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
                            CreateNew()
                        Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                            MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
                            CreateNewWithCopy()
                            'Case ElitaPlusPage.DetailPageCommand.Delete
                            'DoDelete()
                        Case ElitaPlusPage.DetailPageCommand.BackOnErr
                            ReturnToCallingPage(New ReturnType(State.ActionInProgress, State.MyBO, State.HasDataChanged))
                    End Select
                ElseIf confResponse IsNot Nothing AndAlso (confResponse = MSG_VALUE_NO OrElse confResponse.ToUpper = "CANCEL") Then
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
                CleanPopupInput()
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub PopulateDropdowns()
            Try
                Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
                'Dim dv As DataView = LookupListNew.DropdownLookupList("CLM_STAGE_NAME", langId)
                'Me.BindListControlToDataView(Me.ddlStageName, dv, , , True)
                Dim clmStage As ListItem() = CommonConfigManager.Current.ListManager.GetList("CLM_STAGE_NAME", Thread.CurrentPrincipal.GetLanguageCode())
                ddlStageName.Populate(clmStage, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })

                Dim dv As DataView = LookupListNew.GetUserCompanyGroupList()
                ddlCompanyGroup.Items.Add(New System.Web.UI.WebControls.ListItem("", Guid.Empty.ToString))
                If dv.Count > 0 Then
                    ddlCompanyGroup.Items.Add(New System.Web.UI.WebControls.ListItem(dv(0)("DESCRIPTION").ToString, New Guid(CType(dv(0)("ID"), Byte())).ToString))
                End If

                '  Me.BindListControlToDataView(Me.ddlCompany, LookupListNew.GetUserCompaniesLookupList(), , , True)
                Dim compLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("Company", Thread.CurrentPrincipal.GetLanguageCode())
                Dim list As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies

                Dim filteredList As ListItem() = (From x In compLkl
                                                  Where list.Contains(x.ListItemId)
                                                  Select x).ToArray()

                ddlCompany.Populate(filteredList, New PopulateOptions() With
                  {
                   .AddBlankItem = True
                  })
                'Me.BindListControlToDataView(Me.ddlDealer, LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "Code"), , , True)
                Dim oDealerList = GetDealerListByCompanyForUser()
                ddlDealer.Populate(oDealerList, New PopulateOptions() With
                                           {
                                            .AddBlankItem = True
                                           })
                'Me.BindListControlToDataView(Me.ddlCoverageType, LookupListNew.DropdownLookupList("CTYP", langId), , , True)
                Dim coverageTypes As ListItem() = CommonConfigManager.Current.ListManager.GetList("CTYP", Thread.CurrentPrincipal.GetLanguageCode())
                ddlCoverageType.Populate(coverageTypes, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })

                'dv = LookupListNew.GetYesNoLookupList(langId)
                'Me.BindListControlToDataView(Me.ddlScreen, dv, , , True)
                'Me.BindListControlToDataView(Me.ddlPortal, dv, , , True)
                Dim yesNoLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("YESNO", Thread.CurrentPrincipal.GetLanguageCode())
                ddlScreen.Populate(yesNoLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })
                ddlPortal.Populate(yesNoLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })
                Dim newObj As New ClaimStage
                '' Available Claim Stage Start Status - CLMSTAT
                ' dv = newObj.GetAvailableStageStartStatusList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id, langId) 'ClaimStatusByCompanyGroup
                'Me.BindListControlToDataView(Me.ddlStartStatus, dv, , , True)
                Dim listcontext As ListContext = New ListContext()
                listcontext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                listcontext.LanguageId = langId
                Dim claimLKl As ListItem() = CommonConfigManager.Current.ListManager.GetList("ClaimStatusByCompanyGroup", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
                ddlStartStatus.Populate(claimLKl, New PopulateOptions() With
                {
                .AddBlankItem = True
                })
                '' Available Claim Stage End Status - CLMSTAT
                BindAvailableStageEndStatus()

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
        Private Function GetDealerListByCompanyForUser() As Assurant.Elita.CommonConfiguration.DataElements.ListItem()
            Dim Index As Integer
            Dim oListContext As New Assurant.Elita.CommonConfiguration.ListContext

            Dim UserCompanies As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies

            Dim oDealerList As New Collections.Generic.List(Of Assurant.Elita.CommonConfiguration.DataElements.ListItem)

            For Index = 0 To UserCompanies.Count - 1
                'UserCompanyList &= ",'" & GuidControl.GuidToHexString(UserCompanyies(Index))
                oListContext.CompanyId = UserCompanies(Index)
                Dim oDealerListForCompany As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="DealerListByCompany", context:=oListContext)
                If oDealerListForCompany.Count > 0 Then
                    If oDealerList IsNot Nothing Then
                        oDealerList.AddRange(oDealerListForCompany)
                    Else
                        oDealerList = oDealerListForCompany.Clone()
                    End If

                End If
            Next

            Return oDealerList.ToArray()

        End Function

        Protected Sub CreateNew()
            State.ScreenSnapShotBO = Nothing 'Reset the backup copy
            State.MyBO = New ClaimStage
            PopulateFormFromBOs()
            EnableDisableFields()
        End Sub

        Protected Sub CreateNewWithCopy()

            PopulateBOsFromForm()
            BindAvailableStageEndStatus()
            BindSelectedStageEndStatus()

            'create the backup copy
            State.ScreenSnapShotBO = New ClaimStage
            State.ScreenSnapShotBO.Clone(State.MyBO)

            Dim newObj As New ClaimStage

            newObj.CopyFrom(State.MyBO)

            State.MyBO = newObj
            PopulateFormFromBOsWithoutChild()
            EnableDisableFields()
        End Sub

        Protected Sub PopulateFormFromBOs()
            PopulateFormFromBOsWithoutChild()
            BindAvailableStageEndStatus()
            BindSelectedStageEndStatus()
        End Sub

        Protected Sub PopulateFormFromBOsWithoutChild()
            With State.MyBO
                PopulateControlFromBOProperty(ddlStageName, .StageNameId)
                PopulateControlFromBOProperty(ddlCompanyGroup, .CompanyGroupId)
                PopulateControlFromBOProperty(ddlCompany, .CompanyId)
                PopulateControlFromBOProperty(ddlDealer, .DealerId)
                PopulateControlFromBOProperty(ddlCoverageType, .CoverageTypeId)
                PopulateControlFromBOProperty(txtProdCode, .ProductCode)
                PopulateControlFromBOProperty(txtEffectiveDate, .EffectiveDate)
                PopulateControlFromBOProperty(txtExpirationDate, .ExpirationDate)
                PopulateControlFromBOProperty(ddlScreen, .ScreenId)
                PopulateControlFromBOProperty(ddlPortal, .PortalId)
                PopulateControlFromBOProperty(txtSequence, .Sequence)
                PopulateControlFromBOProperty(ddlStartStatus, .StartStatusId)
            End With
        End Sub

        Protected Sub EnableDisableFields()

            If State.MyBO.IsNew Then
                'ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
                ControlMgr.SetEnableControl(Me, btnNew_WRITE, False)
                ControlMgr.SetEnableControl(Me, btnCopy_WRITE, False)
            Else
                'ControlMgr.SetEnableControl(Me, btnDelete_WRITE, True)
                ControlMgr.SetEnableControl(Me, btnNew_WRITE, True)
                ControlMgr.SetEnableControl(Me, btnCopy_WRITE, True)
            End If

        End Sub

        Protected Sub PopulateBOsFromForm()

            With State.MyBO
                PopulateBOProperty(State.MyBO, "StageNameId", ddlStageName)
                PopulateBOProperty(State.MyBO, "CompanyGroupId", ddlCompanyGroup)
                PopulateBOProperty(State.MyBO, "CompanyId", ddlCompany)
                PopulateBOProperty(State.MyBO, "DealerId", ddlDealer)
                PopulateBOProperty(State.MyBO, "ProductCode", txtProdCode)
                PopulateBOProperty(State.MyBO, "CoverageTypeId", ddlCoverageType)
                PopulateBOProperty(State.MyBO, "Sequence", txtSequence)
                PopulateBOProperty(State.MyBO, "EffectiveDate", txtEffectiveDate)
                PopulateBOProperty(State.MyBO, "ExpirationDate", txtExpirationDate)
                PopulateBOProperty(State.MyBO, "ScreenId", ddlScreen)
                PopulateBOProperty(State.MyBO, "PortalId", ddlPortal)
                PopulateBOProperty(State.MyBO, "StartStatusId", ddlStartStatus)

                ' if actual selected rows are not in the selected list, they must have been removed. 
                ' if the original selected status list (db) are not in Control Selected list
                ' so they must have removed.
                Dim selEndStatus As DataView = State.MyBO.GetSelectedStageEndStatusList(.Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                For iRow As Integer = 0 To selEndStatus.Count - 1
                    Dim EndStatusId As Guid = New Guid(CType(selEndStatus(iRow)("ID"), Byte()))
                    If UC_END_STATUS_AVASEL.SelectedListListBox.Items.FindByValue(EndStatusId.ToString) Is Nothing Then
                        .DetachStageEndStatus(EndStatusId.ToString, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                    End If
                Next

                ' if the Control Selected list is not present in original selected status list (db)
                ' so they must be added
                Dim recordExist As Boolean = False
                For iRow As Integer = 0 To UC_END_STATUS_AVASEL.SelectedListListBox.Items.Count - 1
                    Dim EndStatusId As Guid = New Guid(UC_END_STATUS_AVASEL.SelectedListListBox.Items(iRow).Value)
                    recordExist = False
                    For iRowSelect As Integer = 0 To selEndStatus.Count - 1
                        If Not recordExist Then
                            Dim SelectedEndStatusId As Guid = New Guid(CType(selEndStatus(iRowSelect)("ID"), Byte()))
                            If EndStatusId = SelectedEndStatusId Then
                                recordExist = True
                            End If
                        End If
                    Next
                    If Not recordExist Then
                        .AttachStageEndStatus(EndStatusId.ToString, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                    End If
                Next

            End With

            If ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub

        Private Sub BindSelectedStageEndStatus()
            Dim dvSelected As DataView = State.MyBO.GetSelectedStageEndStatusList(State.MyBO.Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            If dvSelected IsNot Nothing Then
                With UC_END_STATUS_AVASEL
                    .SetSelectedData(dvSelected, UC_END_STATUS_AVASEL_SEL_TEXT_COLUMN, UC_END_STATUS_AVASEL_SEL_GUID_COLUMN)
                    .RemoveSelectedFromAvailable()
                End With
            End If
        End Sub

        Private Sub BindAvailableStageEndStatus()
            Dim dvSelected As DataView = State.MyBO.GetAvailableStageEndStatusList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            If dvSelected IsNot Nothing Then
                With UC_END_STATUS_AVASEL
                    .SetAvailableData(dvSelected, UC_END_STATUS_AVASEL_AVA_TEXT_COLUMN, UC_END_STATUS_AVASEL_AVA_GUID_COLUMN)
                End With
            End If
        End Sub

#End Region

#Region "Button event handlers"
        Private Sub btnBack_Click(sender As Object, e As System.EventArgs) Handles btnBack.Click
            Try
                PopulateBOsFromForm()
                If State.MyBO.IsDirty Then
                    If (Not State.MyBO.IsNew) OrElse (State.MyBO.IsNew AndAlso State.MyBO.DirtyColumns.Count > 1) Then
                        DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                        State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                    Else
                        ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.HasDataChanged))
                    End If
                Else
                    ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.HasDataChanged))
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
                DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
                State.LastErrMsg = MasterPage.MessageController.Text
            End Try
        End Sub

        Private Sub btnCopy_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnCopy_WRITE.Click
            Try
                PopulateBOsFromForm()
                If State.MyBO.IsDirty Then
                    DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
                Else
                    CreateNewWithCopy()
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        'Private Sub DoDelete()
        '    Try
        '        Me.State.MyBO.Delete()
        '        Me.State.MyBO.Save()
        '        Me.State.HasDataChanged = True
        '        Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Delete, Me.State.MyBO, Me.State.HasDataChanged))
        '    Catch ex As Threading.ThreadAbortException
        '    Catch ex As Exception
        '        Me.HandleErrors(ex, Me.MasterPage.MessageController)
        '    End Try
        'End Sub

        'Private Sub btnDelete_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete_WRITE.Click
        '    Try
        '        Try
        '            Me.DisplayMessage(Message.DELETE_RECORD_PROMPT, "", Me.MSG_BTN_YES_NO_CANCEL, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
        '            Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete
        '        Catch ex As Threading.ThreadAbortException
        '        Catch ex As Exception
        '            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        '        End Try

        '    Catch ex As Threading.ThreadAbortException
        '    Catch ex As Exception
        '        Me.HandleErrors(ex, Me.MasterPage.MessageController)
        '    End Try
        'End Sub

        Private Sub btnNew_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnNew_WRITE.Click
            Try
                PopulateBOsFromForm()
                If (State.MyBO.IsDirty) Then
                    DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
                Else
                    CreateNew()
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnSave_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnSave_WRITE.Click
            Try
                If UC_END_STATUS_AVASEL.SelectedListListBox.Items.Count <= 0 Then
                    Throw New GUIException(ElitaPlus.Common.ErrorCodes.STAGE_END_STATUS_REQUIRED, ElitaPlus.Common.ErrorCodes.STAGE_END_STATUS_REQUIRED)
                End If
                PopulateBOsFromForm()
                If State.MyBO.IsDirty Then
                    State.MyBO.Save()
                    State.HasDataChanged = True
                    PopulateFormFromBOs()
                    EnableDisableFields()
                    MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
                Else
                    MasterPage.MessageController.AddInformation(Message.MSG_RECORD_NOT_SAVED)
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnUndo_Write_Click(sender As Object, e As System.EventArgs) Handles btnUndo_Write.Click
            Try
                If Not State.MyBO.IsNew Then
                    'Reload from the DB
                    State.MyBO = New ClaimStage(State.MyBO.Id)
                ElseIf State.ScreenSnapShotBO IsNot Nothing Then
                    'It was a new with copy
                    State.MyBO.Clone(State.ScreenSnapShotBO)
                Else
                    CreateNew()
                End If
                PopulateFormFromBOs()
                EnableDisableFields()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
#End Region

    End Class
End Namespace