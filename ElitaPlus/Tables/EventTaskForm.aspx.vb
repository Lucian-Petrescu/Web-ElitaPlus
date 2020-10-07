Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Imports System.Collections.Generic

Namespace Tables
    Partial Public Class EventTaskForm
        Inherits ElitaPlusPage

#Region "Constants"
        Public Const URL As String = "EventTaskForm.aspx"
        Public Const PAGETITLE As String = "EVENT_TASK"
        Public Const PAGETAB As String = "ADMIN"
        Public Const SUMMARYTITLE As String = "EVENT_TASK"
#End Region

#Region "Variables"
        Private mbIsFirstPass As Boolean = True
#End Region

#Region "Page State"
        Class MyState
            Public MyBO As EventTask
            Public ScreenSnapShotBO As EventTask

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
            Public EditingBo As EventTask
            Public HasDataChanged As Boolean
            Public Sub New(LastOp As DetailPageCommand, curEditingBo As EventTask, hasDataChanged As Boolean)
                LastOperation = LastOp
                EditingBo = curEditingBo
                Me.HasDataChanged = hasDataChanged
            End Sub
        End Class

        Private Sub UpdateBreadCrum()
            MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(PAGETAB)
            'Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(SUMMARYTITLE)
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
                        State.MyBO = New EventTask
                    End If

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
                        State.MyBO = New EventTask(objID)
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

        Private Function GetEventArgumentBasedOnEventType(EventTypeCode As String) As DataView
            If (EventTypeCode = "ISSUE_OPENED" _
                    Or EventTypeCode = "ISSUE_RESOLVED" _
                    Or EventTypeCode = "ISSUE_REJECTED" _
                    Or EventTypeCode = "ISSUE_CLOSED" _
                    Or EventTypeCode = "ISSUE_PENDING" _
                    Or EventTypeCode = "ISSUE_WAIVED" _
                    Or EventTypeCode = "ISSUE_REOPENED") Then
                Return LookupListNew.GetIssueLookupListGlobal()
            ElseIf (EventTypeCode = "CLM_EXT_STATUS") Then
                Return LookupListNew.GetExtendedStatusLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            Else
                Return Nothing
            End If
        End Function

        Protected Sub BindBoPropertiesToLabels()
            Try
                BindBOPropertyToLabel(State.MyBO, "CompanyGroupId", lblCompnayGrp)
                BindBOPropertyToLabel(State.MyBO, "CompanyId", lblCompany)
                BindBOPropertyToLabel(State.MyBO, "CountryId", lblCountry)
                BindBOPropertyToLabel(State.MyBO, "DealerGroupId", lblDealerGroup)
                BindBOPropertyToLabel(State.MyBO, "DealerId", lblDealer)
                BindBOPropertyToLabel(State.MyBO, "ProductCode", lblProdCode)
                BindBOPropertyToLabel(State.MyBO, "EventTypeId", lblEventType)
                BindBOPropertyToLabel(State.MyBO, "TimeoutSeconds", lblTimeout)
                BindBOPropertyToLabel(State.MyBO, "TaskId", lblTask)
                BindBOPropertyToLabel(State.MyBO, "RetryCount", lblRetryCount)
                BindBOPropertyToLabel(State.MyBO, "RetryDelaySeconds", lblRetryDelay)
                BindBOPropertyToLabel(State.MyBO, "CoverageTypeId", lblCoverageType)
                BindBOPropertyToLabel(State.MyBO, "EventTaskParameters", lblEventTaskParameters)
                BindBOPropertyToLabel(State.MyBO, "EventArgumentId", lblEventArgument)
                BindBOPropertyToLabel(State.MyBO, "InitDelayMinutes", lblInitDelayMinutes)
                ClearGridHeadersAndLabelsErrSign()
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
                            'Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                            MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
                            CreateNew()
                        Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                            'Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                            MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
                            CreateNewWithCopy()
                        Case ElitaPlusPage.DetailPageCommand.Delete
                            DoDelete()
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
                Dim dv As DataView = LookupListNew.GetUserCompanyGroupList()
                ddlCompanyGroup.Items.Add(New System.Web.UI.WebControls.ListItem("", Guid.Empty.ToString))
                If dv.Count > 0 Then
                    ddlCompanyGroup.Items.Add(New System.Web.UI.WebControls.ListItem(dv(0)("DESCRIPTION").ToString, New Guid(CType(dv(0)("ID"), Byte())).ToString))
                End If

                ' Me.BindListControlToDataView(Me.ddlCompany, LookupListNew.GetUserCompaniesLookupList(), , , True)
                Dim compLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("Company", Thread.CurrentPrincipal.GetLanguageCode())
                Dim list As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies

                Dim filteredList As ListItem() = (From x In compLkl
                                                  Where list.Contains(x.ListItemId)
                                                  Select x).ToArray()

                ddlCompany.Populate(filteredList, New PopulateOptions() With
                  {
                   .AddBlankItem = True
                  })
                'Me.BindListControlToDataView(Me.ddlCountry, LookupListNew.GetUserCountriesLookupList(), , , True)
                Dim countryLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.Country, Thread.CurrentPrincipal.GetLanguageCode())
                Dim cList As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Countries

                Dim filteredcountryList As ListItem() = (From x In countryLkl
                                                         Where cList.Contains(x.ListItemId)
                                                         Select x).ToArray()

                ddlCountry.Populate(filteredcountryList, New PopulateOptions() With
                  {
                   .AddBlankItem = True
                  })

                Dim oDealerGroupList = GetDealerGroupListByCompanyForUser()
                ddlDealerGroup.Populate(oDealerGroupList, New PopulateOptions() With
                                                     {
                                                     .AddBlankItem = True,
                                                     .SortFunc = AddressOf .GetCode
                                                     })

                'Me.BindListControlToDataView(Me.ddlDealer, LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "Code"), , , True)
                Dim oDealerList = GetDealerListByCompanyForUser()
                ddlDealer.Populate(oDealerList, New PopulateOptions() With
                                           {
                                            .AddBlankItem = True
                                           })
                'Me.BindListControlToDataView(Me.ddlEventType, LookupListNew.DropdownLookupList("EVNT_TYP", ElitaPlusIdentity.Current.ActiveUser.LanguageId), , , True)
                ddlEventType.Populate(CommonConfigManager.Current.ListManager.GetList("EVNT_TYP", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                                          {
                                           .AddBlankItem = True
                                          })
                ' Me.BindListControlToDataView(Me.ddlCoverageType, LookupListNew.DropdownLookupList("CTYP", ElitaPlusIdentity.Current.ActiveUser.LanguageId), , , True)
                ddlCoverageType.Populate(CommonConfigManager.Current.ListManager.GetList("CTYP", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                                          {
                                           .AddBlankItem = True
                                          })

                'dv = Task.getList(String.Empty, String.Empty) 'GetTaskList
                'BindListControlToDataView(ddlTask, dv, Task.TaskSearchDV.COL_DESCRIPTION, Task.TaskSearchDV.COL_TASK_ID, True)
                Dim taskLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("GetTaskList", Thread.CurrentPrincipal.GetLanguageCode())
                'Dim filterlist As ListItem() = (From x In taskLkl
                '                                Where x.Code = String.Empty And x.Translation = String.Empty
                '                                Select x).ToArray()
                ddlTask.Populate(taskLkl, New PopulateOptions() With
                                         {
                                          .AddBlankItem = True
                                         })

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

        Private Function GetDealerGroupListByCompanyForUser() As ListItem()
            Dim Index As Integer
            Dim oListContext As New ListContext

            Dim UserCompanies As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies

            Dim oDealerGroupList As New Collections.Generic.List(Of ListItem)

            For Index = 0 To UserCompanies.Count - 1
                'UserCompanyList &= ",'" & GuidControl.GuidToHexString(UserCompanyies(Index))
                oListContext.CompanyId = UserCompanies(Index)
                Dim oDealerGroupListForCompany As ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="DealerGroupByCompany", context:=oListContext)
                If oDealerGroupListForCompany.Count > 0 Then
                    If oDealerGroupList IsNot Nothing Then
                        oDealerGroupList.AddRange(oDealerGroupListForCompany)
                    Else
                        oDealerGroupList = oDealerGroupListForCompany.Clone()
                    End If

                End If
            Next

            Return oDealerGroupList.ToArray()

        End Function

        Protected Sub CreateNew()
            State.ScreenSnapShotBO = Nothing 'Reset the backup copy
            State.MyBO = New EventTask
            PopulateFormFromBOs()
            EnableDisableFields()
        End Sub

        Protected Sub CreateNewWithCopy()

            PopulateBOsFromForm()

            'create the backup copy
            State.ScreenSnapShotBO = New EventTask
            State.ScreenSnapShotBO.Clone(State.MyBO)

            Dim newObj As New EventTask

            newObj.CopyFrom(State.MyBO)

            State.MyBO = newObj

            PopulateFormFromBOs()
            EnableDisableFields()
        End Sub

        Protected Sub PopulateFormFromBOs()
            With State.MyBO
                PopulateControlFromBOProperty(ddlCompanyGroup, .CompanyGroupId)
                PopulateControlFromBOProperty(ddlCompany, .CompanyId)
                PopulateControlFromBOProperty(ddlCountry, .CountryId)
                PopulateControlFromBOProperty(ddlDealerGroup, .DealerGroupId)
                PopulateControlFromBOProperty(ddlDealer, .DealerId)
                PopulateControlFromBOProperty(ddlEventType, .EventTypeId)
                PopulateControlFromBOProperty(txtProdCode, .ProductCode)
                PopulateControlFromBOProperty(txtTimeout, .TimeoutSeconds)
                PopulateControlFromBOProperty(txtRetryCount, .RetryCount)
                PopulateControlFromBOProperty(txtRetryDelay, .RetryDelaySeconds)
                PopulateControlFromBOProperty(ddlTask, .TaskId)
                PopulateControlFromBOProperty(ddlCoverageType, .CoverageTypeId)
                PopulateControlFromBOProperty(txtEventTaskParameters, .EventTaskParameters)
                PopulateControlFromBOProperty(txtInitiDelayMinutes, .InitDelayMinutes)

                ' Populating Event Argument List based on Event Type
                Dim EventTypeCode As String = LookupListNew.GetCodeFromId(Codes.EVNT_TYP, .EventTypeId)
                '   Dim dv As DataView = GetEventArgumentBasedOnEventType(EventTypeCode)

                'If (dv Is Nothing) Then
                '    Me.ddlEventArgument.Items.Clear()
                'Else
                '    Me.BindListControlToDataView(Me.ddlEventArgument, dv, , , True)
                If (EventTypeCode = "ISSUE_OPENED" _
                  Or EventTypeCode = "ISSUE_RESOLVED" _
                  Or EventTypeCode = "ISSUE_REJECTED" _
                  Or EventTypeCode = "ISSUE_CLOSED" _
                  Or EventTypeCode = "ISSUE_PENDING" _
                  Or EventTypeCode = "ISSUE_WAIVED" _
                  Or EventTypeCode = "ISSUE_REOPENED") Then
                    ddlEventArgument.Populate(CommonConfigManager.Current.ListManager.GetList(ListCodes.GetIssue, Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                                      {
                                       .AddBlankItem = True
                                      })
                    PopulateControlFromBOProperty(ddlEventArgument, .EventArgumentId)
                ElseIf (EventTypeCode = "CLM_EXT_STATUS") Then
                    Dim ocListContext As New ListContext
                    ocListContext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                    ddlEventArgument.Populate(CommonConfigManager.Current.ListManager.GetList(ListCodes.ExtendedStatusByCompanyGroup, languageCode:=Thread.CurrentPrincipal.GetLanguageCode(), context:=ocListContext), New PopulateOptions() With
                                               {
                                                .AddBlankItem = True
                                               })
                    PopulateControlFromBOProperty(ddlEventArgument, .EventArgumentId)
                ElseIf (EventTypeCode = "CRT_CANCEL") Then
                    Dim ocListContext As New ListContext
                    Dim oCancellationList As New List(Of ListItem)
                    For Each _company As Guid In ElitaPlusIdentity.Current.ActiveUser.Companies
                        ocListContext.CompanyId = _company

                        Dim oCancellationListForCompany As ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.CancellationReasonsByCompany, context:=ocListContext)
                        If oCancellationListForCompany.Count > 0 Then
                            If oCancellationList IsNot Nothing Then
                                oCancellationList.AddRange(oCancellationListForCompany)
                            Else
                                oCancellationList = oCancellationListForCompany.Clone()
                            End If
                        End If
                    Next

                    ddlEventArgument.Items.Clear()
                    ddlEventArgument.Populate(oCancellationList.ToArray(), New PopulateOptions() With
                                                {
                                                .AddBlankItem = True
                                                })
                    PopulateControlFromBOProperty(ddlEventArgument, .EventArgumentId)
                ElseIf (EventTypeCode = "CLAIM_DENIED") Then
                    Dim ocListContext As New ListContext
                    ocListContext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                    ddlEventArgument.Populate(CommonConfigManager.Current.ListManager.GetList("DNDREASON", languageCode:=Thread.CurrentPrincipal.GetLanguageCode(), context:=ocListContext), New PopulateOptions() With
                                                    {
                                                    .AddBlankItem = True
                                                    })
                    PopulateControlFromBOProperty(ddlEventArgument, .EventArgumentId)
                Else
                    ddlEventArgument.Items.Clear()

                End If

                'End If
            End With
        End Sub

        Protected Sub EnableDisableFields()

            If State.MyBO.IsNew Then
                ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
                ControlMgr.SetEnableControl(Me, btnNew_WRITE, False)
                ControlMgr.SetEnableControl(Me, btnCopy_WRITE, False)
            Else
                ControlMgr.SetEnableControl(Me, btnDelete_WRITE, True)
                ControlMgr.SetEnableControl(Me, btnNew_WRITE, True)
                ControlMgr.SetEnableControl(Me, btnCopy_WRITE, True)
            End If

        End Sub

        Protected Sub PopulateBOsFromForm()

            txtProdCode.Text = txtProdCode.Text.Trim.ToUpper
            With State.MyBO
                PopulateBOProperty(State.MyBO, "DealerId", ddlDealer)
                PopulateBOProperty(State.MyBO, "DealerGroupId", ddlDealerGroup)
                PopulateBOProperty(State.MyBO, "CompanyGroupId", ddlCompanyGroup)
                PopulateBOProperty(State.MyBO, "CompanyId", ddlCompany)
                PopulateBOProperty(State.MyBO, "CountryId", ddlCountry)
                PopulateBOProperty(State.MyBO, "EventTypeId", ddlEventType)
                PopulateBOProperty(State.MyBO, "TaskId", ddlTask)
                PopulateBOProperty(State.MyBO, "ProductCode", txtProdCode)
                PopulateBOProperty(State.MyBO, "TimeoutSeconds", txtTimeout)
                PopulateBOProperty(State.MyBO, "RetryCount", txtRetryCount)
                PopulateBOProperty(State.MyBO, "RetryDelaySeconds", txtRetryDelay)
                PopulateBOProperty(State.MyBO, "CoverageTypeId", ddlCoverageType)
                PopulateBOProperty(State.MyBO, "EventTaskParameters", txtEventTaskParameters)
                PopulateBOProperty(State.MyBO, "EventArgumentId", ddlEventArgument)
                PopulateBOProperty(State.MyBO, "InitDelayMinutes", txtInitiDelayMinutes)
            End With
            If ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub

        Private Sub DoDelete()
            Try
                State.MyBO.DeleteAndSave()
                State.HasDataChanged = True
                ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Delete, State.MyBO, State.HasDataChanged))
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
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

        Private Sub btnDelete_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnDelete_WRITE.Click
            Try
                Try
                    DisplayMessage(Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete
                Catch ex As Threading.ThreadAbortException
                Catch ex As Exception
                    HandleErrors(ex, MasterPage.MessageController)
                End Try

            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

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
                    State.MyBO = New EventTask(State.MyBO.Id)
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

        Private Sub ddlEventType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlEventType.SelectedIndexChanged
            Dim EventArgumentDDL As DropDownList
            Dim SelectedEventTypeDDL As DropDownList = CType(sender, DropDownList)

            Dim EventTypeCode As String = LookupListNew.GetCodeFromId(Codes.EVNT_TYP, New Guid(SelectedEventTypeDDL.SelectedValue))

            Dim dv As DataView = GetEventArgumentBasedOnEventType(EventTypeCode)

            'If (dv Is Nothing) Then
            '    Me.ddlEventArgument.Items.Clear()
            'Else
            'Me.BindListControlToDataView(Me.ddlEventArgument, dv, , , True)
            If (EventTypeCode = "ISSUE_OPENED" _
                  Or EventTypeCode = "ISSUE_RESOLVED" _
                  Or EventTypeCode = "ISSUE_REJECTED" _
                  Or EventTypeCode = "ISSUE_CLOSED" _
                  Or EventTypeCode = "ISSUE_PENDING" _
                  Or EventTypeCode = "ISSUE_WAIVED" _
                  Or EventTypeCode = "ISSUE_REOPENED") Then
                ddlEventArgument.Populate(CommonConfigManager.Current.ListManager.GetList(ListCodes.GetIssue, Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                                          {
                                           .AddBlankItem = True
                                          })
            ElseIf (EventTypeCode = "CLM_EXT_STATUS") Then
                Dim ocListContext As New ListContext
                ocListContext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                ddlEventArgument.Populate(CommonConfigManager.Current.ListManager.GetList(ListCodes.ExtendedStatusByCompanyGroup, languageCode:=Thread.CurrentPrincipal.GetLanguageCode(), context:=ocListContext), New PopulateOptions() With
                                           {
                                            .AddBlankItem = True
                                           })
            ElseIf (EventTypeCode = "CRT_CANCEL") Then
                Dim ocListContext As New ListContext
                Dim oCancellationList As New List(Of ListItem)
                For Each _company As Guid In ElitaPlusIdentity.Current.ActiveUser.Companies
                    ocListContext.CompanyId = _company

                    Dim oCancellationListForCompany As ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.CancellationReasonsByCompany, context:=ocListContext)
                    If oCancellationListForCompany.Count > 0 Then
                        If oCancellationList IsNot Nothing Then
                            oCancellationList.AddRange(oCancellationListForCompany)
                        Else
                            oCancellationList = oCancellationListForCompany.Clone()
                        End If
                    End If
                Next

                ddlEventArgument.Items.Clear()
                ddlEventArgument.Populate(oCancellationList.ToArray(), New PopulateOptions() With
                                            {
                                            .AddBlankItem = True
                                            })
            ElseIf (EventTypeCode = "CLAIM_DENIED") Then
                Dim ocListContext As New ListContext
                ocListContext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                ddlEventArgument.Items.Clear()
                ddlEventArgument.Populate(CommonConfigManager.Current.ListManager.GetList("DNDREASON", languageCode:=Thread.CurrentPrincipal.GetLanguageCode(), context:=ocListContext), New PopulateOptions() With
                                                {
                                                .AddBlankItem = True
                                                })
            Else
                ddlEventArgument.Items.Clear()
            End If
            ' End If
            SelectedEventTypeDDL.Focus()
        End Sub
#End Region

    End Class
End Namespace