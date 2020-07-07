Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Imports System.Collections.Generic
Imports System.Linq

Namespace Tables
    Partial Public Class ConfigQuestionSetForm
        Inherits ElitaPlusPage

#Region "Constants"
        Public Const URL As String = "ConfigQuestionSetForm.aspx"
        Public Const PAGETITLE As String = "CONFIG_QUESTION_SET"
        Public Const PAGETAB As String = "TABLES"
        Public Const SUMMARYTITLE As String = "CONFIG_QUESTION_SET"
#End Region

#Region "Variables"
        Private mbIsFirstPass As Boolean = True
#End Region

#Region "Page State"
        Class MyState
            Public MyBO As ConfigQuestionSet
            Public ScreenSnapShotBO As ConfigQuestionSet

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
            Public EditingBo As ConfigQuestionSet
            Public HasDataChanged As Boolean
            Public Sub New(ByVal LastOp As DetailPageCommand, ByVal curEditingBo As ConfigQuestionSet, ByVal hasDataChanged As Boolean)
                Me.LastOperation = LastOp
                Me.EditingBo = curEditingBo
                Me.HasDataChanged = hasDataChanged
            End Sub
        End Class

        Private Sub UpdateBreadCrum()
            Me.MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(PAGETAB)
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
                        Me.State.MyBO = New ConfigQuestionSet
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
                        Me.State.MyBO = New ConfigQuestionSet(objID)
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

        Protected Sub BindBoPropertiesToLabels()
            Try
                BindBOPropertyToLabel(State.MyBO, "CompanyGroupId", lblCompnayGrp)
                BindBOPropertyToLabel(State.MyBO, "CompanyId", lblCompany)
                BindBOPropertyToLabel(State.MyBO, "DealerGroupId", lblDealerGroup)
                BindBOPropertyToLabel(State.MyBO, "DealerId", lblDealer)
                BindBOPropertyToLabel(State.MyBO, "ProductCode", lblProdCode)
                BindBOPropertyToLabel(State.MyBO, "EventTypeId", lblDeviceType)
                BindBOPropertyToLabel(State.MyBO, "CoverageTypeId", lblCoverageType)
                BindBOPropertyToLabel(State.MyBO, "PurposeCode", lblPurposeCode)
                BindBOPropertyToLabel(State.MyBO, "QuestionSetCode", lblQuestionSetCode)
                ClearGridViewHeadersAndLabelsErrorSign()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub CheckIfComingFromSaveConfirm()
            Try
                Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value
                Dim actionInProgress As ElitaPlusPage.DetailPageCommand = Me.State.ActionInProgress
                If Not confResponse Is Nothing AndAlso (confResponse = MSG_VALUE_YES OrElse confResponse.ToUpper = "OK") Then
                    If actionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr Then
                        Me.State.MyBO.Save()
                        State.HasDataChanged = True
                    End If
                    Select Case Me.State.ActionInProgress
                        Case ElitaPlusPage.DetailPageCommand.Back
                            Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.HasDataChanged))
                        Case ElitaPlusPage.DetailPageCommand.New_
                            Me.MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
                            Me.CreateNew()
                        Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                            Me.MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
                            Me.CreateNewWithCopy()
                        Case ElitaPlusPage.DetailPageCommand.Delete
                            Try
                                Dim bal As New ConfigQuestionSet
                                bal.DeleteConfiguration(Me.State.MyBO.Id)
                                Me.ReturnToCallingPage(New ReturnType(DetailPageCommand.Back, State.MyBO, State.HasDataChanged))
                            Catch ex As Exception
                                Me.HandleErrors(ex, Me.MasterPage.MessageController)
                            End Try
                        Case ElitaPlusPage.DetailPageCommand.BackOnErr
                            Me.ReturnToCallingPage(New ReturnType(Me.State.ActionInProgress, Me.State.MyBO, Me.State.HasDataChanged))
                    End Select
                ElseIf Not confResponse Is Nothing AndAlso (confResponse = MSG_VALUE_NO OrElse confResponse.ToUpper = "CANCEL") Then
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
                Dim textFun As Func(Of DataElements.ListItem, String) = Function(li As DataElements.ListItem)
                                                                            Return li.Code + " - " + li.Translation
                                                                        End Function

                'Company Group
                Dim dv As DataView = LookupListNew.GetUserCompanyGroupList()
                ddlCompanyGroup.Items.Add(New System.Web.UI.WebControls.ListItem("", Guid.Empty.ToString))
                If dv.Count > 0 Then
                    ddlCompanyGroup.Items.Add(New System.Web.UI.WebControls.ListItem(dv(0)("DESCRIPTION").ToString, New Guid(CType(dv(0)("ID"), Byte())).ToString))
                End If

                'Company
                Dim compLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("Company", Thread.CurrentPrincipal.GetLanguageCode())
                Dim list As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
                Dim filteredList As ListItem() = (From x In compLkl
                                                  Where list.Contains(x.ListItemId)
                                                  Select x).ToArray()

                Me.ddlCompany.Populate(filteredList, New PopulateOptions() With
                {
                    .AddBlankItem = True
                })

                'Dealer Group
                Dim oDealerGroupList = GetDealerGroupListByCompanyForUser()
                Me.ddlDealerGroup.Populate(oDealerGroupList, New PopulateOptions() With
                {
                    .AddBlankItem = True,
                    .SortFunc = AddressOf .GetCode
                })

                'Dealer
                Dim oDealerList = GetDealerListByCompanyForUser()
                Me.ddlDealer.Populate(oDealerList, New PopulateOptions() With
                {
                    .AddBlankItem = True
                })

                'ProductCode
                Dim oProductCodeList = GetProductCodeListByCompanyForUser()
                Me.ddlProductCode.Populate(oProductCodeList, New PopulateOptions() With
                {
                    .AddBlankItem = True,
                    .TextFunc = textFun
                })

                'Device Type
                Me.ddlDeviceType.Populate(CommonConfigManager.Current.ListManager.GetList("DEVICE", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                {
                    .AddBlankItem = True
                })

                'Coverage Type
                Me.ddlCoverageType.Populate(CommonConfigManager.Current.ListManager.GetList("CTYP", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                {
                    .AddBlankItem = True
                })

                'Risk Type
                Dim listcontext As ListContext = New ListContext()
                Dim compGroupId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                listcontext.CompanyGroupId = compGroupId
                Dim riskLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("RiskTypeByCompanyGroup", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
                ddlRiskType.Populate(riskLkl, New PopulateOptions() With
                {
                    .AddBlankItem = True
                })

                'Purpose
                ddlPurpose.Populate(CommonConfigManager.Current.ListManager.GetList("CASEPUR", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                {
                    .AddBlankItem = True,
                    .BlankItemValue = String.Empty,
                    .TextFunc = textFun,
                    .ValueFunc = AddressOf .GetExtendedCode
                })

                'QuestionSetCode
                Me.ddlQuestionSetCode.Populate(CommonConfigManager.Current.ListManager.GetList("DcmQuestionSet", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                {
                    .AddBlankItem = True,
                    .BlankItemValue = String.Empty,
                    .ValueFunc = AddressOf .GetCode,
                    .TextFunc = textFun
                })

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Function GetDealerListByCompanyForUser() As ListItem()
            Dim Index As Integer
            Dim oListContext As New ListContext

            Dim UserCompanies As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies

            Dim oDealerList As New List(Of Assurant.Elita.CommonConfiguration.DataElements.ListItem)

            For Index = 0 To UserCompanies.Count - 1
                oListContext.CompanyId = UserCompanies(Index)
                Dim oDealerListForCompany As ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="DealerListByCompany", context:=oListContext)
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

        Private Function GetProductCodeListByCompanyForUser() As ListItem()
            Dim Index As Integer
            Dim oListContext As New ListContext

            Dim UserCompanies As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies

            Dim oProductCodeList As New Collections.Generic.List(Of ListItem)

            For Index = 0 To UserCompanies.Count - 1
                oListContext.CompanyId = UserCompanies(Index)
                Dim oProductCodeListForCompany As ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="ProductCodeByCompany", context:=oListContext)
                If oProductCodeListForCompany.Count > 0 Then
                    If oProductCodeList IsNot Nothing Then
                        oProductCodeList.AddRange(oProductCodeListForCompany)
                    Else
                        oProductCodeList = oProductCodeListForCompany.Clone()
                    End If
                End If
            Next

            Return oProductCodeList.ToArray()

        End Function

        Protected Sub CreateNew()
            Me.State.ScreenSnapShotBO = Nothing 'Reset the backup copy
            Me.State.MyBO = New ConfigQuestionSet
            Me.PopulateFormFromBOs()
            Me.EnableDisableFields()
        End Sub

        Protected Sub CreateNewWithCopy()

            Me.PopulateBOsFromForm()

            'create the backup copy
            Me.State.ScreenSnapShotBO = New ConfigQuestionSet
            Me.State.ScreenSnapShotBO.Clone(Me.State.MyBO)

            Dim newObj As New ConfigQuestionSet

            newObj.CopyFrom(Me.State.MyBO)

            Me.State.MyBO = newObj

            Me.PopulateFormFromBOs()
            Me.EnableDisableFields()
        End Sub

        Protected Sub PopulateFormFromBOs()
            With Me.State.MyBO
                Me.PopulateControlFromBOProperty(ddlCompanyGroup, .CompanyGroupId)
                Me.PopulateControlFromBOProperty(ddlCompany, .CompanyId)
                Me.PopulateControlFromBOProperty(ddlDealerGroup, .DealerGroupId)
                Me.PopulateControlFromBOProperty(ddlDealer, .DealerId)
                Me.PopulateControlFromBOProperty(ddlProductCode, .ProductCodeId)
                Me.PopulateControlFromBOProperty(ddlDeviceType, .DeviceTypeId)
                Me.PopulateControlFromBOProperty(ddlRiskType, .RiskTypeId)
                Me.PopulateControlFromBOProperty(ddlCoverageType, .CoverageTypeId)

                If Not .PurposeXCD Is Nothing Then
                    Me.ddlPurpose.Items.FindByValue(.PurposeXCD).Selected = True
                    Me.ddlPurpose.Style.Remove("background")
                Else
                    Me.ddlPurpose.Items.FindByText(String.Empty).Selected = True
                    Me.ddlPurpose.Style.Remove("background")
                End If

                If Not .QuestionSetCode Is Nothing Then
                    Me.ddlQuestionSetCode.Items.FindByValue(.QuestionSetCode).Selected = True
                    Me.ddlQuestionSetCode.Style.Remove("background")
                Else
                    Me.ddlQuestionSetCode.Items.FindByText(String.Empty).Selected = True
                    Me.ddlQuestionSetCode.Style.Remove("background")
                End If
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
            With Me.State.MyBO
                Me.PopulateBOProperty(Me.State.MyBO, "CompanyGroupId", Me.ddlCompanyGroup)
                Me.PopulateBOProperty(Me.State.MyBO, "CompanyId", Me.ddlCompany)
                Me.PopulateBOProperty(Me.State.MyBO, "DealerGroupId", Me.ddlDealerGroup)
                Me.PopulateBOProperty(Me.State.MyBO, "DealerId", ddlDealer)
                Me.PopulateBOProperty(Me.State.MyBO, "ProductCodeId", ddlProductCode)
                Me.PopulateBOProperty(Me.State.MyBO, "DeviceTypeId", Me.ddlDeviceType)
                Me.PopulateBOProperty(Me.State.MyBO, "CoverageTypeId", Me.ddlCoverageType)
                Me.PopulateBOProperty(Me.State.MyBO, "RiskTypeId", Me.ddlRiskType)
                Me.PopulateBOProperty(Me.State.MyBO, "PurposeXCD", Me.ddlPurpose, False, True)
                Me.PopulateBOProperty(Me.State.MyBO, "QuestionSetCode", Me.ddlQuestionSetCode, False, True)
            End With
            If Me.ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub

#End Region

#Region "Button event handlers"
        Private Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
            Try
                Me.PopulateBOsFromForm()
                If Me.State.MyBO.IsDirty Then
                    If (Not State.MyBO.IsNew) OrElse (State.MyBO.IsNew AndAlso State.MyBO.DirtyColumns.Count > 1) Then
                        Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                        Me.State.ActionInProgress = DetailPageCommand.Back
                    Else
                        Me.ReturnToCallingPage(New ReturnType(DetailPageCommand.Back, State.MyBO, State.HasDataChanged))
                    End If
                Else
                    Me.ReturnToCallingPage(New ReturnType(DetailPageCommand.Back, State.MyBO, State.HasDataChanged))
                End If
            Catch ex As ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, MasterPage.MessageController)
                Me.DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = DetailPageCommand.BackOnErr
                Me.State.LastErrMsg = MasterPage.MessageController.Text
            End Try
        End Sub

        Private Sub btnCopy_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCopy_WRITE.Click
            Try
                Me.PopulateBOsFromForm()
                If Me.State.MyBO.IsDirty Then
                    Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = DetailPageCommand.NewAndCopy
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
                    Me.DisplayMessage(Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = DetailPageCommand.Delete
                Catch ex As ThreadAbortException
                Catch ex As Exception
                    Me.HandleErrors(ex, Me.MasterPage.MessageController)
                End Try

            Catch ex As ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnNew_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew_WRITE.Click
            Try
                Me.PopulateBOsFromForm()
                If (Me.State.MyBO.IsDirty) Then
                    Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
                Else
                    Me.State.ScreenSnapShotBO = Nothing 'Reset the backup copy
                    Me.State.MyBO = New ConfigQuestionSet
                    PopulateDropdowns()
                    Me.PopulateFormFromBOs()
                    Me.EnableDisableFields()
                    AddLabelDecorations(Me.State.MyBO)
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
                    Me.State.MyBO = New ConfigQuestionSet(Me.State.MyBO.Id)
                ElseIf Not Me.State.ScreenSnapShotBO Is Nothing Then
                    'It was a new with copy
                    Me.State.MyBO.Clone(Me.State.ScreenSnapShotBO)
                Else
                    Me.State.ScreenSnapShotBO = Nothing 'Reset the backup copy
                    Me.State.MyBO = New ConfigQuestionSet
                End If

                Me.PopulateFormFromBOs()
                Me.EnableDisableFields()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub ddlCompany_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCompany.SelectedIndexChanged
            Dim textFun As Func(Of DataElements.ListItem, String) = Function(li As DataElements.ListItem)
                                                                        Return li.Code + " - " + li.Translation
                                                                    End Function

            If ddlCompany.SelectedIndex > NO_ITEM_SELECTED_INDEX Then

                If ddlCompany.SelectedIndex = BLANK_ITEM_SELECTED Then
                    Exit Sub
                End If

                'DealerGroup
                Dim oDealerGroupList = GetDealerGroupListByCompany()
                Me.ddlDealerGroup.Populate(oDealerGroupList, New PopulateOptions() With
                {
                    .AddBlankItem = True,
                    .SortFunc = AddressOf .GetCode
                })

                'Dealer
                Dim oDealerList = GetDealerListByCompany()
                Me.ddlDealer.Populate(oDealerList, New PopulateOptions() With
                {
                    .AddBlankItem = True
                })

                'ProductCode
                Dim oProductCodeList = GetProductListByCompany()
                Me.ddlProductCode.Populate(oProductCodeList, New PopulateOptions() With
                {
                    .AddBlankItem = True,
                    .TextFunc = textFun
                })

            End If
        End Sub

        Private Sub ddlDealer_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDealer.SelectedIndexChanged
            Dim textFun As Func(Of DataElements.ListItem, String) = Function(li As DataElements.ListItem)
                                                                        Return li.Code + " - " + li.Translation
                                                                    End Function

            If ddlDealer.SelectedIndex > NO_ITEM_SELECTED_INDEX Then

                If ddlDealer.SelectedIndex = BLANK_ITEM_SELECTED Then
                    Exit Sub
                End If

                'ProductCode
                Dim oProductCodeList = GetProductListByDealer()
                Me.ddlProductCode.Populate(oProductCodeList, New PopulateOptions() With
                {
                    .AddBlankItem = True,
                    .TextFunc = textFun
                })

            End If
        End Sub

        Private Function GetDealerGroupListByCompany() As ListItem()
            Dim oListContext As New ListContext
            oListContext.CompanyId = Guid.Parse(ddlCompany.SelectedValue)
            Dim oDealerGroupListForCompany As ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="DealerGroupByCompany", context:=oListContext)
            Return oDealerGroupListForCompany.ToArray()
        End Function

        Private Function GetDealerListByCompany() As ListItem()
            Dim oListContext As New ListContext
            oListContext.CompanyId = Guid.Parse(ddlCompany.SelectedValue)
            Dim oDealerListForCompany As ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="DealerListByCompany", context:=oListContext)
            Return oDealerListForCompany.ToArray()
        End Function

        Private Function GetProductListByCompany() As ListItem()
            Dim oListContext As New ListContext
            oListContext.CompanyId = Guid.Parse(ddlCompany.SelectedValue)
            Dim oProductListForCompany As ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="ProductCodeByCompany", context:=oListContext)
            Return oProductListForCompany.ToArray()
        End Function

        Private Function GetProductListByDealer() As ListItem()
            Dim oListContext As New ListContext
            oListContext.DealerId = Guid.Parse(ddlDealer.SelectedValue)
            Dim oProductListForCompany As ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="ProductCodeByDealer", context:=oListContext)
            Return oProductListForCompany.ToArray()
        End Function

#End Region

    End Class
End Namespace