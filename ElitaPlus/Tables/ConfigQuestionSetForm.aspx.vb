﻿Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.Elita.Web.Forms
Imports System.Collections.Generic

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
        Private _mbIsFirstPass As Boolean = True
#End Region

#Region "Page State"
        Class MyState
            Public MyBO As ConfigQuestionSet
            Public ScreenSnapShotBo As ConfigQuestionSet
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
            Public Sub New(lastOp As DetailPageCommand, curEditingBo As ConfigQuestionSet, hasDataChanged As Boolean)
                LastOperation = lastOp
                EditingBo = curEditingBo
                HasDataChanged = hasDataChanged
            End Sub
        End Class

        Private Sub UpdateBreadCrum()
            MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(PAGETAB)
            MasterPage.UsePageTabTitleInBreadCrum = False
            MasterPage.BreadCrum = TranslationBase.TranslateLabelOrMessage(PAGETAB) + ElitaBase.Sperator + TranslationBase.TranslateLabelOrMessage(PAGETITLE)
        End Sub

        Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
            If _mbIsFirstPass = True Then
                _mbIsFirstPass = False
            Else
                ' Do not load again the Page that was already loaded
                Return
            End If

            MasterPage.MessageController.Clear()
            UpdateBreadCrum()

            Try
                txtProductCode.Visible = False
                If Not IsPostBack Then
                    If State.MyBO Is Nothing Then
                        State.MyBO = New ConfigQuestionSet
                    End If

                    PopulateDropdowns()
                    PopulateFormFromBOs()
                    EnableDisableFields()
                    AddLabelDecorations(State.MyBO)
                End If
                BindBoPropertiesToLabels()
                CheckIfComingFromSaveConfirm()
            Catch ex As ThreadAbortException
            Catch ex As Exception
                CleanPopupInput()
                HandleErrors(ex, MasterPage.MessageController)
            End Try
            ShowMissingTranslations(MasterPage.MessageController)
        End Sub

        Private Sub Page_PageCall(callFromUrl As String, callingPar As Object) Handles MyBase.PageCall
            Try
                If Not CallingParameters Is Nothing Then
                    'Get the id from the parent
                    Dim objId As Guid = CType(CallingParameters, Guid)
                    If objId <> Guid.Empty Then
                        State.MyBO = New ConfigQuestionSet(objId)
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
                If Not State Is Nothing Then
                    'Clean after consuming the action 
                    State.ActionInProgress = DetailPageCommand.Nothing_
                    HiddenSaveChangesPromptResponse.Value = ""
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
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub CheckIfComingFromSaveConfirm()
            Try
                Dim confResponse As String = HiddenSaveChangesPromptResponse.Value
                Dim actionInProgress As DetailPageCommand = State.ActionInProgress
                If Not confResponse Is Nothing AndAlso (confResponse = MSG_VALUE_YES OrElse confResponse.ToUpper = "OK") Then
                    If actionInProgress <> DetailPageCommand.BackOnErr Then
                        State.MyBO.Save()
                        State.HasDataChanged = True
                    End If
                    Select Case State.ActionInProgress
                        Case DetailPageCommand.Back
                            ReturnToCallingPage(New ReturnType(DetailPageCommand.Back, State.MyBO, State.HasDataChanged))
                        Case DetailPageCommand.New_
                            MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
                            CreateNew()
                        Case DetailPageCommand.NewAndCopy
                            MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
                            CreateNewWithCopy()
                        Case DetailPageCommand.Delete
                            Try
                                Dim bal As New ConfigQuestionSet
                                bal.DeleteConfiguration(State.MyBO.Id)
                                ReturnToCallingPage(New ReturnType(DetailPageCommand.Back, State.MyBO, State.HasDataChanged))
                            Catch ex As Exception
                                HandleErrors(ex, MasterPage.MessageController)
                            End Try
                        Case DetailPageCommand.BackOnErr
                            ReturnToCallingPage(New ReturnType(State.ActionInProgress, State.MyBO, State.HasDataChanged))
                    End Select
                ElseIf Not confResponse Is Nothing AndAlso (confResponse = MSG_VALUE_NO OrElse confResponse.ToUpper = "CANCEL") Then
                    Select Case State.ActionInProgress
                        Case DetailPageCommand.Back
                            ReturnToCallingPage(New ReturnType(DetailPageCommand.Back, State.MyBO, State.HasDataChanged))
                        Case DetailPageCommand.New_
                            CreateNew()
                        Case DetailPageCommand.NewAndCopy
                            CreateNewWithCopy()
                        Case DetailPageCommand.BackOnErr
                            MasterPage.MessageController.AddErrorAndShow(State.LastErrMsg)
                    End Select
                End If
                'Clean after consuming the action
                CleanPopupInput()
            Catch ex As ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub PopulateDropdowns()
            Try
                Dim textFun As Func(Of ListItem, String) = Function(li As ListItem)
                                                               Return li.Code + " - " + li.Translation
                                                           End Function

                'Company Group
                Dim dv As DataView = LookupListNew.GetUserCompanyGroupList()
                ddlCompanyGroup.Items.Add(New WebControls.ListItem("", Guid.Empty.ToString))
                If dv.Count > 0 Then
                    ddlCompanyGroup.Items.Add(New WebControls.ListItem(dv(0)("DESCRIPTION").ToString, New Guid(CType(dv(0)("ID"), Byte())).ToString))
                End If

                'Company
                Dim oCompanyList = GetCompanyListByCompanyGroup(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)
                ddlCompany.Populate(oCompanyList, New PopulateOptions() With
                {
                    .AddBlankItem = True
                })

                'Dealer Group
                If Not State Is Nothing Then
                    If Not State.MyBO Is Nothing Then
                        If Not State.MyBO.CompanyId = Nothing Then
                            Dim oDealerGroupList = GetDealerGroupListByCompany(State.MyBO.CompanyId)
                            ddlDealerGroup.Populate(oDealerGroupList, New PopulateOptions() With
                            {
                                .AddBlankItem = True,
                                .SortFunc = AddressOf PopulateOptions.GetCode
                            })
                        Else
                            Dim oDealerGroupList = GetDealerGroupListByCompanyForUser()
                            ddlDealerGroup.Populate(oDealerGroupList, New PopulateOptions() With
                            {
                                .AddBlankItem = True,
                                .SortFunc = AddressOf PopulateOptions.GetCode
                            })
                        End If
                    End If
                End If

                'Dealer
                If Not State Is Nothing Then
                    If Not State.MyBO Is Nothing Then
                        If Not State.MyBO.CompanyId = Nothing Then
                            Dim oDealerList = GetDealerListByCompany(State.MyBO.CompanyId)
                            ddlDealer.Populate(oDealerList, New PopulateOptions() With
                            {
                                .AddBlankItem = True
                            })
                        Else
                            Dim oDealerList = GetDealerListByCompanyForUser()
                            ddlDealer.Populate(oDealerList, New PopulateOptions() With
                            {
                                .AddBlankItem = True
                            })
                        End If
                    End If
                End If

                'ProductCode
                ddlProductCode.Populate(New ListItem(0) {}, New PopulateOptions() With
                    {
                        .AddBlankItem = True,
                        .TextFunc = textFun
                    })
                ddlProductCode.Enabled = False

                If Not State Is Nothing Then
                    If Not State.MyBO Is Nothing Then
                        If Not State.MyBO.DealerId = Nothing Then
                            Dim oProductCodeList = GetProductCodeListByDealer(State.MyBO.DealerId)
                            ddlProductCode.Items.Clear()
                            ddlProductCode.Populate(oProductCodeList, New PopulateOptions() With
                            {
                                .AddBlankItem = True,
                                .TextFunc = textFun
                            })
                            ddlProductCode.Enabled = True
                        End If
                    End If
                End If

                'Device Type
                ddlDeviceType.Populate(CommonConfigManager.Current.ListManager.GetList("DEVICE", Authentication.CurrentUser.LanguageCode), New PopulateOptions() With
                {
                    .AddBlankItem = True
                })

                'Coverage Type
                ddlCoverageType.Populate(CommonConfigManager.Current.ListManager.GetList("CTYP", Authentication.CurrentUser.LanguageCode), New PopulateOptions() With
                {
                    .AddBlankItem = True
                })

                'Risk Type
                Dim listContext As ListContext = New ListContext()
                Dim compGroupId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                listContext.CompanyGroupId = compGroupId
                Dim riskLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("RiskTypeByCompanyGroup", Authentication.CurrentUser.LanguageCode, listContext)
                ddlRiskType.Populate(riskLkl, New PopulateOptions() With
                {
                    .AddBlankItem = True
                })

                'Purpose
                ddlPurpose.Populate(CommonConfigManager.Current.ListManager.GetList("CASEPUR", Authentication.CurrentUser.LanguageCode), New PopulateOptions() With
                {
                    .AddBlankItem = True,
                    .BlankItemValue = String.Empty,
                    .TextFunc = textFun,
                    .ValueFunc = AddressOf PopulateOptions.GetExtendedCode
                })

                'QuestionSetCode
                ddlQuestionSetCode.Populate(CommonConfigManager.Current.ListManager.GetList("DcmQuestionSet", Authentication.CurrentUser.LanguageCode), New PopulateOptions() With
                {
                    .AddBlankItem = True,
                    .BlankItemValue = String.Empty,
                    .ValueFunc = AddressOf PopulateOptions.GetCode,
                    .TextFunc = textFun
                })

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Function GetDealerListByCompanyForUser() As ListItem()
            Dim index As Integer
            Dim oListContext As New ListContext
            Dim userCompanies As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
            Dim oDealerList As New List(Of ListItem)

            For index = 0 To userCompanies.Count - 1
                oListContext.CompanyId = userCompanies(index)
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
            Dim index As Integer
            Dim oListContext As New ListContext
            Dim userCompanies As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
            Dim oDealerGroupList As New List(Of ListItem)

            For index = 0 To userCompanies.Count - 1
                oListContext.CompanyId = userCompanies(index)
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

        Private Function GetCompanyListByCompanyGroup(companyGroupId As Guid) As ListItem()
            Dim listContext As ListContext = New ListContext()

            listContext.CompanyGroupId = companyGroupId
            Dim companyListForCompanyGroup As ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="GetCompanyByCompanyGroup", context:=listContext)

            Return companyListForCompanyGroup.ToArray()
        End Function

        Private Function GetProductCodeListByDealer(dealerId As Guid) As ListItem()
            Dim listContext As ListContext = New ListContext()

            listContext.DealerId = dealerId
            Dim oProductCodeListForDealer As ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.ProductCodeByDealer, Authentication.CurrentUser.LanguageCode, listContext)

            Return oProductCodeListForDealer.ToArray()
        End Function

        Protected Sub CreateNew()
            State.ScreenSnapShotBo = Nothing 'Reset the backup copy
            State.MyBO = New ConfigQuestionSet
            PopulateFormFromBOs()
            EnableDisableFields()
        End Sub

        Protected Sub CreateNewWithCopy()
            PopulateBOsFromForm()

            'create the backup copy
            State.ScreenSnapShotBo = New ConfigQuestionSet
            State.ScreenSnapShotBo.Clone(State.MyBO)

            Dim newObj As New ConfigQuestionSet
            newObj.CopyFrom(State.MyBO)
            State.MyBO = newObj

            PopulateFormFromBOs()
            EnableDisableFields()
        End Sub

        Protected Sub PopulateFormFromBOs()
            With State.MyBO
                PopulateControlFromBOProperty(ddlCompanyGroup, .CompanyGroupId)
                PopulateControlFromBOProperty(ddlCompany, .CompanyId)
                PopulateControlFromBOProperty(ddlDealerGroup, .DealerGroupId)
                PopulateControlFromBOProperty(ddlDealer, .DealerId)
                PopulateControlFromBOProperty(ddlDeviceType, .DeviceTypeId)
                PopulateControlFromBOProperty(ddlRiskType, .RiskTypeId)
                PopulateControlFromBOProperty(ddlCoverageType, .CoverageTypeId)

                If Not .PurposeXCD Is Nothing Then
                    ddlPurpose.Items.FindByValue(.PurposeXCD).Selected = True
                    ddlPurpose.Style.Remove("background")
                Else
                    ddlPurpose.Items.FindByText(String.Empty).Selected = True
                    ddlPurpose.Style.Remove("background")
                End If

                If Not .QuestionSetCode Is Nothing Then
                    ddlQuestionSetCode.Items.FindByValue(.QuestionSetCode).Selected = True
                    ddlQuestionSetCode.Style.Remove("background")
                Else
                    ddlQuestionSetCode.Items.FindByText(String.Empty).Selected = True
                    ddlQuestionSetCode.Style.Remove("background")
                End If

                If (Not .ProductCodeId = Guid.Empty) Or (.ProductCode = String.Empty) Then
                    PopulateControlFromBOProperty(ddlProductCode, .ProductCodeId)
                    txtProductCode.Text = String.Empty
                    txtProductCode.Visible = False
                    ddlProductCode.Visible = True
                Else
                    txtProductCode.Text = .ProductCode
                    txtProductCode.Visible = True
                    ddlProductCode.Visible = False
                End If

                If (.DealerGroupId <> Guid.Empty) And (.ProductCode <> String.Empty) Then
                    ddlDealerGroup.Enabled = True
                    txtProductCode.Visible = True
                    ddlDealer.SelectedIndex = BLANK_ITEM_SELECTED
                    ddlDealer.Enabled = False
                End If

                If (.DealerId <> Guid.Empty) And (.ProductCodeId <> Guid.Empty) Then
                    ddlDealer.Enabled = True
                    txtProductCode.Text = String.Empty
                    txtProductCode.Visible = False
                    ddlDealerGroup.SelectedIndex = BLANK_ITEM_SELECTED
                    ddlDealerGroup.Enabled = False
                End If

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
            With State.MyBO
                PopulateBOProperty(State.MyBO, "CompanyGroupId", ddlCompanyGroup)
                PopulateBOProperty(State.MyBO, "CompanyId", ddlCompany)
                PopulateBOProperty(State.MyBO, "DealerGroupId", ddlDealerGroup)
                PopulateBOProperty(State.MyBO, "DealerId", ddlDealer)
                PopulateBOProperty(State.MyBO, "ProductCodeId", ddlProductCode)
                PopulateBOProperty(State.MyBO, "DeviceTypeId", ddlDeviceType)
                PopulateBOProperty(State.MyBO, "CoverageTypeId", ddlCoverageType)
                PopulateBOProperty(State.MyBO, "RiskTypeId", ddlRiskType)
                PopulateBOProperty(State.MyBO, "PurposeXCD", ddlPurpose, False, True)
                PopulateBOProperty(State.MyBO, "QuestionSetCode", ddlQuestionSetCode, False, True)
                PopulateBOProperty(State.MyBO, "ProductCode", txtProductCode)
            End With

            If ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub

#End Region

#Region "Button event handlers"
        Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
            Try
                PopulateBOsFromForm()
                If State.MyBO.IsDirty Then
                    If (Not State.MyBO.IsNew) OrElse (State.MyBO.IsNew AndAlso State.MyBO.DirtyColumns.Count > 1) Then
                        DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                        State.ActionInProgress = DetailPageCommand.Back
                    Else
                        ReturnToCallingPage(New ReturnType(DetailPageCommand.Back, State.MyBO, State.HasDataChanged))
                    End If
                Else
                    ReturnToCallingPage(New ReturnType(DetailPageCommand.Back, State.MyBO, State.HasDataChanged))
                End If
            Catch ex As ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
                DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = DetailPageCommand.BackOnErr
                State.LastErrMsg = MasterPage.MessageController.Text
            End Try
        End Sub

        Private Sub btnCopy_WRITE_Click(sender As Object, e As EventArgs) Handles btnCopy_WRITE.Click
            Try
                PopulateBOsFromForm()
                If State.MyBO.IsDirty Then
                    DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = DetailPageCommand.NewAndCopy
                Else
                    CreateNewWithCopy()
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnDelete_WRITE_Click(sender As Object, e As EventArgs) Handles btnDelete_WRITE.Click
            Try
                Try
                    DisplayMessage(Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = DetailPageCommand.Delete
                Catch ex As ThreadAbortException
                Catch ex As Exception
                    HandleErrors(ex, MasterPage.MessageController)
                End Try
            Catch ex As ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnNew_WRITE_Click(sender As Object, e As EventArgs) Handles btnNew_WRITE.Click
            Try
                PopulateBOsFromForm()
                If (State.MyBO.IsDirty) Then
                    DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = DetailPageCommand.New_
                Else
                    State.ScreenSnapShotBo = Nothing 'Reset the backup copy
                    State.MyBO = New ConfigQuestionSet
                    PopulateDropdowns()
                    PopulateFormFromBOs()
                    EnableDisableFields()
                    AddLabelDecorations(State.MyBO)
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnSave_WRITE_Click(sender As Object, e As EventArgs) Handles btnSave_WRITE.Click
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
                With State.MyBO
                    If Not .ProductCode Is Nothing Then
                        txtProductCode.Text = .ProductCode
                        txtProductCode.Visible = True
                        ddlProductCode.Visible = False
                    Else
                        txtProductCode.Text = String.Empty
                        txtProductCode.Visible = False
                        ddlProductCode.Visible = True
                    End If
                End With
            End Try
        End Sub

        Private Sub btnUndo_Write_Click(sender As Object, e As EventArgs) Handles btnUndo_Write.Click
            Try
                If Not State.MyBO.IsNew Then
                    'Reload from the DB
                    State.MyBO = New ConfigQuestionSet(State.MyBO.Id)
                ElseIf Not State.ScreenSnapShotBo Is Nothing Then
                    'It was a new with copy
                    State.MyBO.Clone(State.ScreenSnapShotBo)
                Else
                    State.ScreenSnapShotBo = Nothing 'Reset the backup copy
                    State.MyBO = New ConfigQuestionSet
                End If

                PopulateFormFromBOs()
                EnableDisableFields()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub ddlCompany_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCompany.SelectedIndexChanged
            Dim textFun As Func(Of ListItem, String) = Function(li As ListItem)
                                                                        Return li.Code + " - " + li.Translation
                                                                    End Function

            If ddlCompany.SelectedIndex > NO_ITEM_SELECTED_INDEX Then

                If ddlCompany.SelectedIndex = BLANK_ITEM_SELECTED Then
                    Exit Sub
                End If

                'DealerGroup
                Dim oDealerGroupList = GetDealerGroupListByCompany(Guid.Parse(ddlCompany.SelectedValue))
                ddlDealerGroup.Populate(oDealerGroupList, New PopulateOptions() With
                {
                    .AddBlankItem = True,
                    .SortFunc = AddressOf PopulateOptions.GetCode
                })

                'Dealer
                Dim oDealerList = GetDealerListByCompany(Guid.Parse(ddlCompany.SelectedValue))
                ddlDealer.Populate(oDealerList, New PopulateOptions() With
                {
                    .AddBlankItem = True
                })

                'ProductCode
                ddlProductCode.Populate(New ListItem(0) {}, New PopulateOptions() With
                {
                    .AddBlankItem = True,
                    .TextFunc = textFun
                })
                ddlProductCode.Enabled = False

            End If
        End Sub

        Protected Sub ddlDealerGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDealerGroup.SelectedIndexChanged
            Try
                If ddlDealerGroup.SelectedIndex > NO_ITEM_SELECTED_INDEX And ddlDealerGroup.SelectedIndex <> BLANK_ITEM_SELECTED Then
                    ddlProductCode.Visible = False
                    txtProductCode.Visible = True
                    ddlDealer.SelectedIndex = NO_ITEM_SELECTED_INDEX
                    ddlDealer.Enabled = False
                Else
                    ddlProductCode.Visible = True
                    txtProductCode.Text = String.Empty
                    txtProductCode.Visible = False
                    ddlDealer.Enabled = True
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub ddlDealer_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDealer.SelectedIndexChanged
            Dim textFun As Func(Of ListItem, String) = Function(li As ListItem)
                                                                        Return li.Code + " - " + li.Translation
                                                                    End Function

            If ddlDealer.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
                If ddlDealer.SelectedIndex = BLANK_ITEM_SELECTED Then
                    'ProductCode
                    ddlProductCode.Populate(New ListItem(0) {}, New PopulateOptions() With
                    {
                        .AddBlankItem = True,
                        .TextFunc = textFun
                    })
                    ddlProductCode.Enabled = False
                    'DealerGroup
                    ddlDealerGroup.Enabled = True
                Else
                    'ProductCode
                    ddlProductCode.Enabled = True
                    ddlProductCode.Items.Clear()
                    Dim oProductCodeList = GetProductCodeListByDealer(Guid.Parse(ddlDealer.SelectedValue))
                    ddlProductCode.Populate(oProductCodeList, New PopulateOptions() With
                    {
                        .AddBlankItem = True,
                        .TextFunc = textFun
                    })
                    'DealerGroup
                    ddlDealerGroup.Enabled = False
                End If
            Else
                ddlProductCode.Populate(New ListItem(0) {}, New PopulateOptions() With
                {
                    .AddBlankItem = True,
                    .TextFunc = textFun
                })
                ddlProductCode.Enabled = False
            End If
        End Sub

        Private Function GetDealerGroupListByCompany(ByVal companyId As Guid) As ListItem()
            Dim oListContext As New ListContext
            oListContext.CompanyId = companyId
            Dim oDealerGroupListForCompany As ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="DealerGroupByCompany", context:=oListContext)
            Return oDealerGroupListForCompany.ToArray()
        End Function

        Private Function GetDealerListByCompany(ByVal companyId As Guid) As ListItem()
            Dim oListContext As New ListContext
            oListContext.CompanyId = companyId
            Dim oDealerListForCompany As ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="DealerListByCompany", context:=oListContext)
            Return oDealerListForCompany.ToArray()
        End Function

#End Region

    End Class
End Namespace