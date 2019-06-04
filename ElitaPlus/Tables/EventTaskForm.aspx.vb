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
            Public Sub New(ByVal LastOp As DetailPageCommand, ByVal curEditingBo As EventTask, ByVal hasDataChanged As Boolean)
                Me.LastOperation = LastOp
                Me.EditingBo = curEditingBo
                Me.HasDataChanged = hasDataChanged
            End Sub
        End Class

        Private Sub UpdateBreadCrum()
            Me.MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(PAGETAB)
            'Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(SUMMARYTITLE)
            Me.MasterPage.UsePageTabTitleInBreadCrum = False
            Me.MasterPage.BreadCrum = TranslationBase.TranslateLabelOrMessage(PAGETAB) + ElitaBase.Sperator + TranslationBase.TranslateLabelOrMessage(PAGETITLE)
        End Sub

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If mbIsFirstPass = True Then
                mbIsFirstPass = False
            Else
                ' Do not load again the Page that was already loaded
                Return
            End If
            Me.MasterPage.MessageController.Clear()
            Me.UpdateBreadCrum()

            Try
                If Not Me.IsPostBack Then
                    If Me.State.MyBO Is Nothing Then
                        Me.State.MyBO = New EventTask
                    End If

                    PopulateDropdowns()
                    PopulateFormFromBOs()
                    EnableDisableFields()
                    AddLabelDecorations(Me.State.MyBO)
                End If
                BindBoPropertiesToLabels()
                CheckIfComingFromSaveConfirm()
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                CleanPopupInput()
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
            Me.ShowMissingTranslations(Me.MasterPage.MessageController)
        End Sub

        Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
            Try
                If Not Me.CallingParameters Is Nothing Then
                    'Get the id from the parent
                    Dim objID As Guid = CType(Me.CallingParameters, Guid)
                    If objID <> Guid.Empty Then
                        Me.State.MyBO = New EventTask(objID)
                    End If
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub
#End Region

#Region "Helper functions"

        Private Sub CleanPopupInput()
            Try
                If Not Me.State Is Nothing Then
                    'Clean after consuming the action 
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                    Me.HiddenSaveChangesPromptResponse.Value = ""
                End If
            Catch ex As Exception

            End Try
        End Sub

        Private Function GetEventArgumentBasedOnEventType(ByVal EventTypeCode As String) As DataView
            If (EventTypeCode = "ISSUE_OPENED" _
                    Or EventTypeCode = "ISSUE_RESOLVED" _
                    Or EventTypeCode = "ISSUE_REJECTED" _
                    Or EventTypeCode = "ISSUE_CLOSED" _
                    Or EventTypeCode = "ISSUE_PENDING" _
                    Or EventTypeCode = "ISSUE_WAIVED") Then
                Return LookupListNew.GetIssueLookupListGlobal()
            ElseIf (EventTypeCode = "CLM_EXT_STATUS") Then
                Return LookupListNew.GetExtendedStatusLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            Else
                Return Nothing
            End If
        End Function

        Protected Sub BindBoPropertiesToLabels()
            Try
                Me.BindBOPropertyToLabel(Me.State.MyBO, "CompanyGroupId", Me.lblCompnayGrp)
                Me.BindBOPropertyToLabel(Me.State.MyBO, "CompanyId", Me.lblCompany)
                Me.BindBOPropertyToLabel(Me.State.MyBO, "CountryId", Me.lblCountry)
                Me.BindBOPropertyToLabel(Me.State.MyBO, "DealerGroupId", Me.lblDealerGroup)
                Me.BindBOPropertyToLabel(Me.State.MyBO, "DealerId", Me.lblDealer)
                Me.BindBOPropertyToLabel(Me.State.MyBO, "ProductCode", Me.lblProdCode)
                Me.BindBOPropertyToLabel(Me.State.MyBO, "EventTypeId", Me.lblEventType)
                Me.BindBOPropertyToLabel(Me.State.MyBO, "TimeoutSeconds", Me.lblTimeout)
                Me.BindBOPropertyToLabel(Me.State.MyBO, "TaskId", Me.lblTask)
                Me.BindBOPropertyToLabel(Me.State.MyBO, "RetryCount", Me.lblRetryCount)
                Me.BindBOPropertyToLabel(Me.State.MyBO, "RetryDelaySeconds", Me.lblRetryDelay)
                Me.BindBOPropertyToLabel(Me.State.MyBO, "CoverageTypeId", Me.lblCoverageType)
                Me.BindBOPropertyToLabel(Me.State.MyBO, "EventTaskParameters", Me.lblEventTaskParameters)
                Me.BindBOPropertyToLabel(Me.State.MyBO, "EventArgumentId", Me.lblEventArgument)
                Me.BindBOPropertyToLabel(Me.State.MyBO, "InitDelayMinutes", Me.lblInitDelayMinutes)
                Me.ClearGridHeadersAndLabelsErrSign()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub CheckIfComingFromSaveConfirm()
            Try
                Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value
                Dim actionInProgress As ElitaPlusPage.DetailPageCommand = Me.State.ActionInProgress
                If Not confResponse Is Nothing AndAlso (confResponse = Me.MSG_VALUE_YES OrElse confResponse.ToUpper = "OK") Then
                    If actionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr Then
                        Me.State.MyBO.Save()
                        State.HasDataChanged = True
                    End If
                    Select Case Me.State.ActionInProgress
                        Case ElitaPlusPage.DetailPageCommand.Back
                            Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.HasDataChanged))
                        Case ElitaPlusPage.DetailPageCommand.New_
                            'Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                            Me.MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
                            Me.CreateNew()
                        Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                            'Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                            Me.MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
                            Me.CreateNewWithCopy()
                        Case ElitaPlusPage.DetailPageCommand.Delete
                            DoDelete()
                        Case ElitaPlusPage.DetailPageCommand.BackOnErr
                            Me.ReturnToCallingPage(New ReturnType(Me.State.ActionInProgress, Me.State.MyBO, Me.State.HasDataChanged))
                    End Select
                ElseIf Not confResponse Is Nothing AndAlso (confResponse = Me.MSG_VALUE_NO OrElse confResponse.ToUpper = "CANCEL") Then
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
                CleanPopupInput()
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
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

                Me.ddlCompany.Populate(filteredList, New PopulateOptions() With
                  {
                   .AddBlankItem = True
                  })
                'Me.BindListControlToDataView(Me.ddlCountry, LookupListNew.GetUserCountriesLookupList(), , , True)
                Dim countryLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.Country, Thread.CurrentPrincipal.GetLanguageCode())
                Dim cList As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Countries

                Dim filteredcountryList As ListItem() = (From x In countryLkl
                                                         Where cList.Contains(x.ListItemId)
                                                         Select x).ToArray()

                Me.ddlCountry.Populate(filteredcountryList, New PopulateOptions() With
                  {
                   .AddBlankItem = True
                  })

                Dim oDealerGroupList = GetDealerGroupListByCompanyForUser()
                Me.ddlDealerGroup.Populate(oDealerGroupList, New PopulateOptions() With
                                                     {
                                                     .AddBlankItem = True,
                                                     .SortFunc = AddressOf .GetCode
                                                     })

                'Me.BindListControlToDataView(Me.ddlDealer, LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "Code"), , , True)
                Dim oDealerList = GetDealerListByCompanyForUser()
                Me.ddlDealer.Populate(oDealerList, New PopulateOptions() With
                                           {
                                            .AddBlankItem = True
                                           })
                'Me.BindListControlToDataView(Me.ddlEventType, LookupListNew.DropdownLookupList("EVNT_TYP", ElitaPlusIdentity.Current.ActiveUser.LanguageId), , , True)
                Me.ddlEventType.Populate(CommonConfigManager.Current.ListManager.GetList("EVNT_TYP", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                                          {
                                           .AddBlankItem = True
                                          })
                ' Me.BindListControlToDataView(Me.ddlCoverageType, LookupListNew.DropdownLookupList("CTYP", ElitaPlusIdentity.Current.ActiveUser.LanguageId), , , True)
                Me.ddlCoverageType.Populate(CommonConfigManager.Current.ListManager.GetList("CTYP", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
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
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
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
                    If Not oDealerList Is Nothing Then
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
            Me.State.ScreenSnapShotBO = Nothing 'Reset the backup copy
            Me.State.MyBO = New EventTask
            Me.PopulateFormFromBOs()
            Me.EnableDisableFields()
        End Sub

        Protected Sub CreateNewWithCopy()

            Me.PopulateBOsFromForm()

            'create the backup copy
            Me.State.ScreenSnapShotBO = New EventTask
            Me.State.ScreenSnapShotBO.Clone(Me.State.MyBO)

            Dim newObj As New EventTask

            newObj.CopyFrom(Me.State.MyBO)

            Me.State.MyBO = newObj

            Me.PopulateFormFromBOs()
            Me.EnableDisableFields()
        End Sub

        Protected Sub PopulateFormFromBOs()
            With Me.State.MyBO
                Me.PopulateControlFromBOProperty(Me.ddlCompanyGroup, .CompanyGroupId)
                Me.PopulateControlFromBOProperty(Me.ddlCompany, .CompanyId)
                Me.PopulateControlFromBOProperty(Me.ddlCountry, .CountryId)
                Me.PopulateControlFromBOProperty(Me.ddlDealerGroup, .DealerGroupId)
                Me.PopulateControlFromBOProperty(Me.ddlDealer, .DealerId)
                Me.PopulateControlFromBOProperty(Me.ddlEventType, .EventTypeId)
                Me.PopulateControlFromBOProperty(Me.txtProdCode, .ProductCode)
                Me.PopulateControlFromBOProperty(Me.txtTimeout, .TimeoutSeconds)
                Me.PopulateControlFromBOProperty(Me.txtRetryCount, .RetryCount)
                Me.PopulateControlFromBOProperty(Me.txtRetryDelay, .RetryDelaySeconds)
                Me.PopulateControlFromBOProperty(Me.ddlTask, .TaskId)
                Me.PopulateControlFromBOProperty(Me.ddlCoverageType, .CoverageTypeId)
                Me.PopulateControlFromBOProperty(Me.txtEventTaskParameters, .EventTaskParameters)
                Me.PopulateControlFromBOProperty(Me.txtInitiDelayMinutes, .InitDelayMinutes)

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
                  Or EventTypeCode = "ISSUE_WAIVED") Then
                    Me.ddlEventArgument.Populate(CommonConfigManager.Current.ListManager.GetList(ListCodes.GetIssue, Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                                      {
                                       .AddBlankItem = True
                                      })
                    Me.PopulateControlFromBOProperty(Me.ddlEventArgument, .EventArgumentId)
                ElseIf (EventTypeCode = "CLM_EXT_STATUS") Then
                    Dim ocListContext As New ListContext
                    ocListContext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                    Me.ddlEventArgument.Populate(CommonConfigManager.Current.ListManager.GetList(ListCodes.ExtendedStatusByCompanyGroup, languageCode:=Thread.CurrentPrincipal.GetLanguageCode(), context:=ocListContext), New PopulateOptions() With
                                               {
                                                .AddBlankItem = True
                                               })
                    Me.PopulateControlFromBOProperty(Me.ddlEventArgument, .EventArgumentId)
                ElseIf (EventTypeCode = "CRT_CANCEL") Then
                    Dim ocListContext As New ListContext
                    Dim oCancellationList As New List(Of ListItem)
                    For Each _company As Guid In ElitaPlusIdentity.Current.ActiveUser.Companies
                        ocListContext.CompanyId = _company

                        Dim oCancellationListForCompany As ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.CancellationReasonsByCompany, context:=ocListContext)
                        If oCancellationListForCompany.Count > 0 Then
                            If Not oCancellationList Is Nothing Then
                                oCancellationList.AddRange(oCancellationListForCompany)
                            Else
                                oCancellationList = oCancellationListForCompany.Clone()
                            End If
                        End If
                    Next

                    Me.ddlEventArgument.Items.Clear()
                    Me.ddlEventArgument.Populate(oCancellationList.ToArray(), New PopulateOptions() With
                                                {
                                                .AddBlankItem = True
                                                })
                    Me.PopulateControlFromBOProperty(Me.ddlEventArgument, .EventArgumentId)
                ElseIf (EventTypeCode = "CLAIM_DENIED") Then
                    Dim ocListContext As New ListContext
                    ocListContext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                    Me.ddlEventArgument.Populate(CommonConfigManager.Current.ListManager.GetList("DNDREASON", languageCode:=Thread.CurrentPrincipal.GetLanguageCode(), context:=ocListContext), New PopulateOptions() With
                                                    {
                                                    .AddBlankItem = True
                                                    })
                    Me.PopulateControlFromBOProperty(Me.ddlEventArgument, .EventArgumentId)
                Else
                    Me.ddlEventArgument.Items.Clear()

                End If

                'End If
            End With
        End Sub

        Protected Sub EnableDisableFields()

            If Me.State.MyBO.IsNew Then
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
            With Me.State.MyBO
                Me.PopulateBOProperty(Me.State.MyBO, "DealerId", ddlDealer)
                Me.PopulateBOProperty(Me.State.MyBO, "DealerGroupId", Me.ddlDealerGroup)
                Me.PopulateBOProperty(Me.State.MyBO, "CompanyGroupId", Me.ddlCompanyGroup)
                Me.PopulateBOProperty(Me.State.MyBO, "CompanyId", Me.ddlCompany)
                Me.PopulateBOProperty(Me.State.MyBO, "CountryId", Me.ddlCountry)
                Me.PopulateBOProperty(Me.State.MyBO, "EventTypeId", Me.ddlEventType)
                Me.PopulateBOProperty(Me.State.MyBO, "TaskId", Me.ddlTask)
                Me.PopulateBOProperty(Me.State.MyBO, "ProductCode", Me.txtProdCode)
                Me.PopulateBOProperty(Me.State.MyBO, "TimeoutSeconds", Me.txtTimeout)
                Me.PopulateBOProperty(Me.State.MyBO, "RetryCount", Me.txtRetryCount)
                Me.PopulateBOProperty(Me.State.MyBO, "RetryDelaySeconds", Me.txtRetryDelay)
                Me.PopulateBOProperty(Me.State.MyBO, "CoverageTypeId", Me.ddlCoverageType)
                Me.PopulateBOProperty(Me.State.MyBO, "EventTaskParameters", Me.txtEventTaskParameters)
                Me.PopulateBOProperty(Me.State.MyBO, "EventArgumentId", Me.ddlEventArgument)
                Me.PopulateBOProperty(Me.State.MyBO, "InitDelayMinutes", Me.txtInitiDelayMinutes)
            End With
            If Me.ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub

        Private Sub DoDelete()
            Try
                Me.State.MyBO.DeleteAndSave()
                Me.State.HasDataChanged = True
                Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Delete, Me.State.MyBO, Me.State.HasDataChanged))
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Button event handlers"
        Private Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
            Try
                Me.PopulateBOsFromForm()
                If Me.State.MyBO.IsDirty Then
                    If (Not State.MyBO.IsNew) OrElse (State.MyBO.IsNew AndAlso State.MyBO.DirtyColumns.Count > 1) Then
                        Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO_CANCEL, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                        Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                    Else
                        Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.HasDataChanged))
                    End If
                Else
                    Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.HasDataChanged))
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
                Me.DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", Me.MSG_BTN_YES_NO_CANCEL, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
                Me.State.LastErrMsg = Me.MasterPage.MessageController.Text
            End Try
        End Sub

        Private Sub btnCopy_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCopy_WRITE.Click
            Try
                Me.PopulateBOsFromForm()
                If Me.State.MyBO.IsDirty Then
                    Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO_CANCEL, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
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
                Try
                    Me.DisplayMessage(Message.DELETE_RECORD_PROMPT, "", Me.MSG_BTN_YES_NO_CANCEL, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete
                Catch ex As Threading.ThreadAbortException
                Catch ex As Exception
                    Me.HandleErrors(ex, Me.MasterPage.MessageController)
                End Try

            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnNew_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew_WRITE.Click
            Try
                Me.PopulateBOsFromForm()
                If (Me.State.MyBO.IsDirty) Then
                    Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO_CANCEL, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
                Else
                    Me.CreateNew()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnSave_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave_WRITE.Click
            Try
                Me.PopulateBOsFromForm()
                If Me.State.MyBO.IsDirty Then
                    Me.State.MyBO.Save()
                    Me.State.HasDataChanged = True
                    Me.PopulateFormFromBOs()
                    Me.EnableDisableFields()
                    Me.MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
                Else
                    Me.MasterPage.MessageController.AddInformation(Message.MSG_RECORD_NOT_SAVED)
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnUndo_Write_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUndo_Write.Click
            Try
                If Not Me.State.MyBO.IsNew Then
                    'Reload from the DB
                    Me.State.MyBO = New EventTask(Me.State.MyBO.Id)
                ElseIf Not Me.State.ScreenSnapShotBO Is Nothing Then
                    'It was a new with copy
                    Me.State.MyBO.Clone(Me.State.ScreenSnapShotBO)
                Else
                    CreateNew()
                End If
                Me.PopulateFormFromBOs()
                Me.EnableDisableFields()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
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
                  Or EventTypeCode = "ISSUE_WAIVED") Then
                Me.ddlEventArgument.Populate(CommonConfigManager.Current.ListManager.GetList(ListCodes.GetIssue, Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                                          {
                                           .AddBlankItem = True
                                          })
            ElseIf (EventTypeCode = "CLM_EXT_STATUS") Then
                Dim ocListContext As New ListContext
                ocListContext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                Me.ddlEventArgument.Populate(CommonConfigManager.Current.ListManager.GetList(ListCodes.ExtendedStatusByCompanyGroup, languageCode:=Thread.CurrentPrincipal.GetLanguageCode(), context:=ocListContext), New PopulateOptions() With
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
                        If Not oCancellationList Is Nothing Then
                            oCancellationList.AddRange(oCancellationListForCompany)
                        Else
                            oCancellationList = oCancellationListForCompany.Clone()
                        End If
                    End If
                Next

                Me.ddlEventArgument.Items.Clear()
                Me.ddlEventArgument.Populate(oCancellationList.ToArray(), New PopulateOptions() With
                                            {
                                            .AddBlankItem = True
                                            })
            ElseIf (EventTypeCode = "CLAIM_DENIED") Then
                Dim ocListContext As New ListContext
                ocListContext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                Me.ddlEventArgument.Items.Clear()
                Me.ddlEventArgument.Populate(CommonConfigManager.Current.ListManager.GetList("DNDREASON", languageCode:=Thread.CurrentPrincipal.GetLanguageCode(), context:=ocListContext), New PopulateOptions() With
                                                {
                                                .AddBlankItem = True
                                                })
            Else
                Me.ddlEventArgument.Items.Clear()
            End If
            ' End If
            SelectedEventTypeDDL.Focus()
        End Sub
#End Region

    End Class
End Namespace