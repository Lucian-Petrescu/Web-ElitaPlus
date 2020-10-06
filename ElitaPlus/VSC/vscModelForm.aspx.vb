Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Namespace Tables

    Partial Class vscModelForm
        Inherits ElitaPlusPage

        'Protected WithEvents moErrorController As ErrorController


#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        'NOTE: The following placeholder declaration is required by the Web Form Designer.
        'Do not delete or move it.
        Private designerPlaceholderDeclaration As System.Object

        Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

#Region "Constants"
        Public Shared URL As String = "vscModelForm.aspx"
        Private Const NOTHING_SELECTED As Int16 = -1

#End Region

#Region "Private Members"

        Private moVSCModel As VSCModel
        Private coverageLimitDV As DataView
        Private coverageLimitDT As DataTable
#End Region

#Region "Properties"

        Private ReadOnly Property TheVSCModel() As VSCModel
            Get

                If moVSCModel Is Nothing Then
                    If State.IsVSCModelNew = True Then
                        ' For creating, inserting
                        moVSCModel = New VSCModel
                        State.moVSCModelId = moVSCModel.Id
                    Else
                        ' For updating, deleting
                        moVSCModel = New VSCModel(State.moVSCModelId)
                    End If
                End If

                Return moVSCModel
            End Get
        End Property


#End Region

#Region "Page State"

        Class MyState
            Public MyBO As VSCModel
            Public moVSCModelId As Guid = Guid.Empty
            Public IsVSCModelNew As Boolean = False

            Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
            Public boChanged As Boolean = False
            Public LastErrMsg As String
            Public HasDataChanged As Boolean
            Public ScreenSnapShotBO As VSCModel
        End Class


        Public Sub New()
            MyBase.New(New MyState)
        End Sub

        Protected Shadows ReadOnly Property State() As MyState
            Get
                Return CType(MyBase.State, MyState)
            End Get
        End Property

        Private Sub SetStateProperties()
            State.moVSCModelId = CType(CallingParameters, Guid)
            If State.moVSCModelId.Equals(Guid.Empty) Then
                State.IsVSCModelNew = True
                'ClearAll()
                'SetButtonsState(True)
            Else
                State.IsVSCModelNew = False
                'SetButtonsState(False)
            End If
        End Sub
        Private Sub Page_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall
            Try
                If CallingParameters IsNot Nothing Then
                    'Get the id from the parent
                    State.MyBO = New VSCModel(CType(CallingParameters, Guid))
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub
#End Region

#Region "Page Return Type"
        Public Class ReturnType
            Public LastOperation As DetailPageCommand
            Public EditingBo As VSCModel
            Public HasDataChanged As Boolean
            Public Sub New(LastOp As DetailPageCommand, curEditingBo As VSCModel, hasDataChanged As Boolean)
                LastOperation = LastOp
                EditingBo = curEditingBo
                Me.HasDataChanged = hasDataChanged
            End Sub
        End Class
#End Region

#Region "Page Events"



        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Try
                MasterPage.MessageController.Clear_Hide()
                'ClearLabelsErrSign()
                If Not Page.IsPostBack Then
                    MasterPage.MessageController.Clear_Hide()
                    'Me.ResolveShippingFeeVisibility()
                    MasterPage.UsePageTabTitleInBreadCrum = False
                    MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage("Tables")
                    MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("VSC_MODEL")
                    UpdateBreadCrum()
                    If State.MyBO Is Nothing Then
                        State.MyBO = New VSCModel
                        State.IsVSCModelNew = True
                    End If
                    SetStateProperties()
                    PopulateDropDowns()
                    PopulateFormFromBOs()
                    ' Action = ACTION_NONE                    
                    EnableDisableFields()
                Else
                    coverageLimitDT = CType(Session("COVERAGE_LIMIT_TABLE"), DataTable)
                End If
                BindBoPropertiesToLabels()
                If Not IsPostBack Then
                    AddLabelDecorations(TheVSCModel)
                Else
                    CheckIfComingFromSaveConfirm()
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
            ShowMissingTranslations(MasterPage.MessageController)
        End Sub

#End Region


#Region "Controlling Logic"

        Protected Sub EnableDisableFields()
            'Enabled by Default
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, True)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, True)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, True)

            If State.MyBO.IsNew Then
                ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
                ControlMgr.SetEnableControl(Me, btnNew_WRITE, False)
                ControlMgr.SetEnableControl(Me, btnCopy_WRITE, False)
            End If


        End Sub

        Protected Sub BindBoPropertiesToLabels()

            BindBOPropertyToLabel(State.MyBO, "ManufacturerId", moMakeLabel)
            BindBOPropertyToLabel(State.MyBO, "Model", moModelLabel)
            BindBOPropertyToLabel(State.MyBO, "ModelYear", moYearLabel)
            BindBOPropertyToLabel(State.MyBO, "NewClassCodeId", moNewClassCodeLable)
            BindBOPropertyToLabel(State.MyBO, "UsedClassCodeId", moUsedClassCodeLable)
            BindBOPropertyToLabel(State.MyBO, "Description", moEngineVersionLabel)
            BindBOPropertyToLabel(State.MyBO, "ActiveNewId", moActiveNewLabel)
            BindBOPropertyToLabel(State.MyBO, "ActiveUsedId", moActiveUsedLabel)
            BindBOPropertyToLabel(State.MyBO, "CarCode", moCarCodeLabel)
            BindBOPropertyToLabel(State.MyBO, "CoverageLimitId", moCoverageLimitCodeLabel)
            'REQ-1142
            BindBOPropertyToLabel(State.MyBO, "ExternalCarCode", moExternalCarCodeLabel)
            ClearGridHeadersAndLabelsErrSign()
        End Sub



        Protected Sub PopulateFormFromBOs()
            With State.MyBO
                BindSelectItem(State.MyBO.ManufacturerId().ToString, moMakeDrop)
                BindSelectItem(State.MyBO.NewClassCodeId.ToString, moClassCodeNewDrop)
                BindSelectItem(State.MyBO.UsedClassCodeId.ToString, moClassCodeUsedDrop)
                BindSelectItem(State.MyBO.CoverageLimitId().ToString, moCoverageLimitCodeDrop)

                If .ModelYear = 0 Then
                    moYearText.Text = Nothing
                Else
                    PopulateControlFromBOProperty(moYearText, .ModelYear)
                End If

                'If .CarCode = 0 Then
                'Me.moCarCodeText.Text = Nothing
                'Else
                PopulateControlFromBOProperty(moCarCodeText, .CarCode)
                'End If
                'REQ-1142
                PopulateControlFromBOProperty(moExternalCarCodeText, .ExternalCarCode)

                PopulateControlFromBOProperty(moEngineVersionText, .Description)
                PopulateControlFromBOProperty(moModelText, .Model)
                Dim yesID As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, "Y")

                If State.MyBO.ActiveNewId.Equals(yesID) Then
                    CHK_NEW_ACTIVE.Checked = True
                Else
                    CHK_NEW_ACTIVE.Checked = False
                End If

                If State.MyBO.ActiveUsedId.Equals(yesID) Then
                    CHK_USED_ACTIVE.Checked = True
                Else
                    CHK_USED_ACTIVE.Checked = False
                End If

            End With

        End Sub

        Protected Sub PopulateBOsFromForm()

            With State.MyBO

                PopulateBOProperty(State.MyBO, "ManufacturerId", moMakeDrop)
                PopulateBOProperty(State.MyBO, "NewClassCodeId", moClassCodeNewDrop)
                PopulateBOProperty(State.MyBO, "UsedClassCodeId", moClassCodeUsedDrop)
                PopulateBOProperty(State.MyBO, "ModelYear", moYearText)
                PopulateBOProperty(State.MyBO, "Description", moEngineVersionText)
                PopulateBOProperty(State.MyBO, "Model", moModelText)
                PopulateBOProperty(State.MyBO, "CarCode", moCarCodeText)
                PopulateBOProperty(State.MyBO, "CoverageLimitId", moCoverageLimitCodeDrop)
                'Req-1142
                PopulateBOProperty(State.MyBO, "ExternalCarCode", moExternalCarCodeText)
                coverageLimitDV = LookupListNew.GetVSCCoverageLimitLookupList(Authentication.CurrentUser.CompanyGroup.Id)
                State.MyBO.CoverageLimitCode = CInt(LookupListNew.GetCodeFromId(coverageLimitDV, New Guid(moCoverageLimitCodeDrop.SelectedValue.ToString)))

                State.MyBO.CompanyGgroupId = Authentication.CurrentUser.CompanyGroup.Id

                Dim yesID As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, "Y")
                Dim noID As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, "N")

                If CHK_NEW_ACTIVE.Checked Then
                    State.MyBO.ActiveNewId = yesID
                Else
                    State.MyBO.ActiveNewId = noID
                End If

                If CHK_USED_ACTIVE.Checked Then
                    State.MyBO.ActiveUsedId = yesID
                Else
                    State.MyBO.ActiveUsedId = noID
                End If

            End With
            If ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub

        Protected Sub CheckIfComingFromSaveConfirm()
            Dim confResponse As String = HiddenSaveChangesPromptResponse.Value
            If confResponse IsNot Nothing AndAlso (confResponse = CONFIRM_MESSAGE_OK OrElse confResponse = MSG_VALUE_YES) Then
                If State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr Then
                    State.MyBO.Save()
                End If
                Select Case State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Back
                        ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.HasDataChanged))
                    Case ElitaPlusPage.DetailPageCommand.New_
                        AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                        CreateNew()
                    Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                        AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                        CreateNewWithCopy()
                    Case ElitaPlusPage.DetailPageCommand.Delete
                        State.MyBO.Delete()
                        State.MyBO.Save()
                        State.HasDataChanged = True
                        ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Delete, State.MyBO, State.HasDataChanged))
                    Case ElitaPlusPage.DetailPageCommand.BackOnErr
                        ReturnToCallingPage(New ReturnType(State.ActionInProgress, State.MyBO, State.HasDataChanged))
                End Select
            ElseIf confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_NO Then
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
            State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
            HiddenSaveChangesPromptResponse.Value = ""
        End Sub



        Private Sub GoBack()
            Dim retType As New vscModelSearchForm.ReturnType(ElitaPlusPage.DetailPageCommand.Back,
                                                                                State.moVSCModelId, State.boChanged)
            ReturnToCallingPage(retType)
        End Sub

        Private Sub CreateNew()
            State.ScreenSnapShotBO = Nothing
            State.moVSCModelId = Guid.Empty
            State.IsVSCModelNew = True
            State.MyBO = New VSCModel
            ClearAll()
            moCarCodeText.Text = Nothing
            'Req-1142
            moExternalCarCodeText.Text = Nothing
            moYearText.Text = Nothing
            EnableDisableFields()
        End Sub

        Private Sub CreateNewWithCopy()
            Dim newObj As New VSCModel
            newObj.Copy(State.MyBO)

            State.MyBO = newObj
            PopulateFormFromBOs()
            moCarCodeText.Text = Nothing
            moExternalCarCodeText.Text = Nothing 'REQ-1142
            'create the backup copy
            State.ScreenSnapShotBO = New VSCModel
            State.ScreenSnapShotBO.Copy(State.MyBO)

            EnableDisableFields()
        End Sub

        Private Sub UpdateBreadCrum()
            If (State IsNot Nothing) Then
                MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("VSC_MODEL")
            End If
        End Sub

#End Region

#Region "Handlers-Buttons"
        Private Sub btnBack_Click(sender As System.Object, e As System.EventArgs) Handles btnBack.Click
            Try
                PopulateBOsFromForm()
                If State.MyBO.IsDirty Then
                    DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
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

        Private Sub btnApply_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnApply_WRITE.Click
            Try
                PopulateBOsFromForm()
                If State.MyBO.IsDirty Then
                    State.MyBO.Save()
                    State.HasDataChanged = True
                    ClearAll()
                    PopulateFormFromBOs()
                    'Me.DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                    MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
                Else
                    'Me.DisplayMessage(Message.MSG_RECORD_NOT_SAVED, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                    MasterPage.MessageController.AddInformation(Message.MSG_RECORD_NOT_SAVED)
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnUndo_Write_Click(sender As System.Object, e As System.EventArgs) Handles btnUndo_WRITE.Click
            Try
                If Not State.MyBO.IsNew Then
                    'Reload from the DB
                    State.MyBO = New VSCModel(State.MyBO.Id)
                ElseIf State.ScreenSnapShotBO IsNot Nothing Then
                    'It was a new with copy
                    State.MyBO.Clone(State.ScreenSnapShotBO)
                Else
                    State.MyBO = New VSCModel
                End If
                PopulateFormFromBOs()
                EnableDisableFields()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnDelete_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnDelete_WRITE.Click
            Try
                DisplayMessage(Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                'undo the delete
                State.MyBO.RejectChanges()
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnNew_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnNew_WRITE.Click
            Try
                PopulateBOsFromForm()
                If State.MyBO.IsDirty Then
                    DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
                Else
                    CreateNew()
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnCopy_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnCopy_WRITE.Click
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

#End Region

#Region "Populate Dropdowns"

        Private Sub PopulateDropDowns()
            Try
                'Me.BindListControlToDataView(Me.moMakeDrop, LookupListNew.GetVSCMakeLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id, "MAKES_BY_COMPANY_GROUP"))

                ' New  Need More changes - Add  ACTIVE_FLAG = 'Y'
                Dim Manufacturer As DataElements.ListItem() =
                    CommonConfigManager.Current.ListManager.GetList(listCode:="ManufacturerByCompanyGroup",
                                                                    context:=New ListContext() With
                                                                    {
                                                                      .CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                                                                    })

                moMakeDrop.Populate(Manufacturer.ToArray(),
                                            New PopulateOptions() With
                                            {
                                             .AddBlankItem = True
                                            })

                'coverageLimitDV = LookupListNew.GetVSCCoverageLimitLookupList(Authentication.CurrentUser.CompanyGroup.Id)
                'Session("COVERAGE_LIMIT_TABLE") = coverageLimitDV.Table
                'Me.coverageLimitDT = coverageLimitDV.Table
                'Me.BindListControlToDataView(Me.moCoverageLimitCodeDrop, coverageLimitDV)

                Dim CoverageLimits As DataElements.ListItem() =
                CommonConfigManager.Current.ListManager.GetList(listCode:="CoverageLimitByCompanyGroup",
                                                                context:=New ListContext() With
                                                                {
                                                                  .CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                                                                })

                moCoverageLimitCodeDrop.Populate(CoverageLimits.ToArray(),
                                        New PopulateOptions() With
                                        {
                                            .AddBlankItem = True
                                        })

                'Dim classCodeDV As DataView = LookupListNew.GetVSCClassCodesLookupList(Authentication.CurrentUser.CompanyGroup.Id)
                'Me.BindListControlToDataView(Me.moClassCodeNewDrop, classCodeDV)
                'Me.BindListControlToDataView(Me.moClassCodeUsedDrop, classCodeDV)

                'Need CommonConfiguration Changes
                Dim ClassCodes As DataElements.ListItem() =
                CommonConfigManager.Current.ListManager.GetList(listCode:="VscClassCodesByCompanyGroup",
                                                                context:=New ListContext() With
                                                                {
                                                                  .CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                                                                })

                moClassCodeNewDrop.Populate(ClassCodes.ToArray(),
                                        New PopulateOptions() With
                                        {
                                            .AddBlankItem = True
                                        })

                moClassCodeUsedDrop.Populate(ClassCodes.ToArray(),
                                        New PopulateOptions() With
                                        {
                                            .AddBlankItem = True
                                        })




                If State.IsVSCModelNew = True Then
                    BindSelectItem(Nothing, moMakeDrop)
                    BindSelectItem(Nothing, moClassCodeNewDrop)
                    BindSelectItem(Nothing, moClassCodeUsedDrop)
                    BindSelectItem(Nothing, moCoverageLimitCodeDrop)
                Else
                    BindSelectItem(TheVSCModel.ManufacturerId().ToString, moMakeDrop)
                    BindSelectItem(TheVSCModel.NewClassCodeId.ToString, moClassCodeNewDrop)
                    BindSelectItem(TheVSCModel.UsedClassCodeId.ToString, moClassCodeUsedDrop)
                End If
            Catch ex As Exception
                MasterPage.MessageController.Show()
            End Try
        End Sub

#End Region

#Region "Clear"

        Private Sub ClearAll()
            moYearText.Text = Nothing
            moEngineVersionText.Text = Nothing
            moYearText.Text = Nothing
            moEngineVersionText.Text = Nothing
            moModelText.Text = Nothing
            moMakeDrop.SelectedIndex = NOTHING_SELECTED
            moClassCodeNewDrop.SelectedIndex = NOTHING_SELECTED
            moClassCodeUsedDrop.SelectedIndex = NOTHING_SELECTED
            moCoverageLimitCodeDrop.SelectedIndex = NOTHING_SELECTED
            moCarCodeText.Text = Nothing
            moExternalCarCodeText.Text = Nothing 'REQ-1142
            'ClearList(moMakeDrop)
            'ClearList(moClassCodeNewDrop)
            'ClearList(moClassCodeUsedDrop)
            CHK_NEW_ACTIVE.Checked = True
            CHK_USED_ACTIVE.Checked = True

        End Sub

#End Region

#Region "State-Management"


#End Region


    End Class

End Namespace
