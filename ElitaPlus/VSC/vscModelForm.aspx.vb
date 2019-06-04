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

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
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
                    If Me.State.IsVSCModelNew = True Then
                        ' For creating, inserting
                        moVSCModel = New VSCModel
                        Me.State.moVSCModelId = moVSCModel.Id
                    Else
                        ' For updating, deleting
                        moVSCModel = New VSCModel(Me.State.moVSCModelId)
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
            Me.State.moVSCModelId = CType(Me.CallingParameters, Guid)
            If Me.State.moVSCModelId.Equals(Guid.Empty) Then
                Me.State.IsVSCModelNew = True
                'ClearAll()
                'SetButtonsState(True)
            Else
                Me.State.IsVSCModelNew = False
                'SetButtonsState(False)
            End If
        End Sub
        Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
            Try
                If Not Me.CallingParameters Is Nothing Then
                    'Get the id from the parent
                    Me.State.MyBO = New VSCModel(CType(Me.CallingParameters, Guid))
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub
#End Region

#Region "Page Return Type"
        Public Class ReturnType
            Public LastOperation As DetailPageCommand
            Public EditingBo As VSCModel
            Public HasDataChanged As Boolean
            Public Sub New(ByVal LastOp As DetailPageCommand, ByVal curEditingBo As VSCModel, ByVal hasDataChanged As Boolean)
                Me.LastOperation = LastOp
                Me.EditingBo = curEditingBo
                Me.HasDataChanged = hasDataChanged
            End Sub
        End Class
#End Region

#Region "Page Events"



        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Try
                Me.MasterPage.MessageController.Clear_Hide()
                'ClearLabelsErrSign()
                If Not Page.IsPostBack Then
                    Me.MasterPage.MessageController.Clear_Hide()
                    'Me.ResolveShippingFeeVisibility()
                    Me.MasterPage.UsePageTabTitleInBreadCrum = False
                    Me.MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage("Tables")
                    Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("VSC_MODEL")
                    Me.UpdateBreadCrum()
                    If Me.State.MyBO Is Nothing Then
                        Me.State.MyBO = New VSCModel
                        Me.State.IsVSCModelNew = True
                    End If
                    Me.SetStateProperties()
                    Me.PopulateDropDowns()
                    Me.PopulateFormFromBOs()
                    ' Action = ACTION_NONE                    
                    EnableDisableFields()
                Else
                    Me.coverageLimitDT = CType(Session("COVERAGE_LIMIT_TABLE"), DataTable)
                End If
                BindBoPropertiesToLabels()
                If Not Me.IsPostBack Then
                    Me.AddLabelDecorations(TheVSCModel)
                Else
                    CheckIfComingFromSaveConfirm()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
            Me.ShowMissingTranslations(Me.MasterPage.MessageController)
        End Sub

#End Region


#Region "Controlling Logic"

        Protected Sub EnableDisableFields()
            'Enabled by Default
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, True)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, True)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, True)

            If Me.State.MyBO.IsNew Then
                ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
                ControlMgr.SetEnableControl(Me, btnNew_WRITE, False)
                ControlMgr.SetEnableControl(Me, btnCopy_WRITE, False)
            End If


        End Sub

        Protected Sub BindBoPropertiesToLabels()

            Me.BindBOPropertyToLabel(Me.State.MyBO, "ManufacturerId", Me.moMakeLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "Model", Me.moModelLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "ModelYear", Me.moYearLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "NewClassCodeId", Me.moNewClassCodeLable)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "UsedClassCodeId", Me.moUsedClassCodeLable)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "Description", Me.moEngineVersionLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "ActiveNewId", Me.moActiveNewLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "ActiveUsedId", Me.moActiveUsedLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "CarCode", Me.moCarCodeLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "CoverageLimitId", Me.moCoverageLimitCodeLabel)
            'REQ-1142
            Me.BindBOPropertyToLabel(Me.State.MyBO, "ExternalCarCode", Me.moExternalCarCodeLabel)
            Me.ClearGridHeadersAndLabelsErrSign()
        End Sub



        Protected Sub PopulateFormFromBOs()
            With Me.State.MyBO
                BindSelectItem(Me.State.MyBO.ManufacturerId().ToString, Me.moMakeDrop)
                BindSelectItem(Me.State.MyBO.NewClassCodeId.ToString, Me.moClassCodeNewDrop)
                BindSelectItem(Me.State.MyBO.UsedClassCodeId.ToString, Me.moClassCodeUsedDrop)
                BindSelectItem(Me.State.MyBO.CoverageLimitId().ToString, Me.moCoverageLimitCodeDrop)

                If .ModelYear = 0 Then
                    Me.moYearText.Text = Nothing
                Else
                    Me.PopulateControlFromBOProperty(Me.moYearText, .ModelYear)
                End If

                'If .CarCode = 0 Then
                'Me.moCarCodeText.Text = Nothing
                'Else
                Me.PopulateControlFromBOProperty(Me.moCarCodeText, .CarCode)
                'End If
                'REQ-1142
                Me.PopulateControlFromBOProperty(Me.moExternalCarCodeText, .ExternalCarCode)

                Me.PopulateControlFromBOProperty(Me.moEngineVersionText, .Description)
                Me.PopulateControlFromBOProperty(Me.moModelText, .Model)
                Dim yesID As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, "Y")

                If Me.State.MyBO.ActiveNewId.Equals(yesID) Then
                    Me.CHK_NEW_ACTIVE.Checked = True
                Else
                    Me.CHK_NEW_ACTIVE.Checked = False
                End If

                If Me.State.MyBO.ActiveUsedId.Equals(yesID) Then
                    Me.CHK_USED_ACTIVE.Checked = True
                Else
                    Me.CHK_USED_ACTIVE.Checked = False
                End If

            End With

        End Sub

        Protected Sub PopulateBOsFromForm()

            With Me.State.MyBO

                Me.PopulateBOProperty(Me.State.MyBO, "ManufacturerId", Me.moMakeDrop)
                Me.PopulateBOProperty(Me.State.MyBO, "NewClassCodeId", Me.moClassCodeNewDrop)
                Me.PopulateBOProperty(Me.State.MyBO, "UsedClassCodeId", Me.moClassCodeUsedDrop)
                Me.PopulateBOProperty(Me.State.MyBO, "ModelYear", Me.moYearText)
                Me.PopulateBOProperty(Me.State.MyBO, "Description", Me.moEngineVersionText)
                Me.PopulateBOProperty(Me.State.MyBO, "Model", Me.moModelText)
                Me.PopulateBOProperty(Me.State.MyBO, "CarCode", Me.moCarCodeText)
                Me.PopulateBOProperty(Me.State.MyBO, "CoverageLimitId", Me.moCoverageLimitCodeDrop)
                'Req-1142
                Me.PopulateBOProperty(Me.State.MyBO, "ExternalCarCode", Me.moExternalCarCodeText)
                coverageLimitDV = LookupListNew.GetVSCCoverageLimitLookupList(Authentication.CurrentUser.CompanyGroup.Id)
                Me.State.MyBO.CoverageLimitCode = CInt(LookupListNew.GetCodeFromId(coverageLimitDV, New Guid(Me.moCoverageLimitCodeDrop.SelectedValue.ToString)))

                Me.State.MyBO.CompanyGgroupId = Authentication.CurrentUser.CompanyGroup.Id

                Dim yesID As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, "Y")
                Dim noID As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, "N")

                If Me.CHK_NEW_ACTIVE.Checked Then
                    Me.State.MyBO.ActiveNewId = yesID
                Else
                    Me.State.MyBO.ActiveNewId = noID
                End If

                If Me.CHK_USED_ACTIVE.Checked Then
                    Me.State.MyBO.ActiveUsedId = yesID
                Else
                    Me.State.MyBO.ActiveUsedId = noID
                End If

            End With
            If Me.ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub

        Protected Sub CheckIfComingFromSaveConfirm()
            Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value
            If Not confResponse Is Nothing AndAlso (confResponse = Me.CONFIRM_MESSAGE_OK OrElse confResponse = Me.MSG_VALUE_YES) Then
                If Me.State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr Then
                    Me.State.MyBO.Save()
                End If
                Select Case Me.State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Back
                        Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.HasDataChanged))
                    Case ElitaPlusPage.DetailPageCommand.New_
                        Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                        Me.CreateNew()
                    Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                        Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                        Me.CreateNewWithCopy()
                    Case ElitaPlusPage.DetailPageCommand.Delete
                        Me.State.MyBO.Delete()
                        Me.State.MyBO.Save()
                        Me.State.HasDataChanged = True
                        Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Delete, Me.State.MyBO, Me.State.HasDataChanged))
                    Case ElitaPlusPage.DetailPageCommand.BackOnErr
                        Me.ReturnToCallingPage(New ReturnType(Me.State.ActionInProgress, Me.State.MyBO, Me.State.HasDataChanged))
                End Select
            ElseIf Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_NO Then
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
            Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
            Me.HiddenSaveChangesPromptResponse.Value = ""
        End Sub



        Private Sub GoBack()
            Dim retType As New vscModelSearchForm.ReturnType(ElitaPlusPage.DetailPageCommand.Back,
                                                                                Me.State.moVSCModelId, Me.State.boChanged)
            Me.ReturnToCallingPage(retType)
        End Sub

        Private Sub CreateNew()
            Me.State.ScreenSnapShotBO = Nothing
            Me.State.moVSCModelId = Guid.Empty
            Me.State.IsVSCModelNew = True
            Me.State.MyBO = New VSCModel
            ClearAll()
            Me.moCarCodeText.Text = Nothing
            'Req-1142
            Me.moExternalCarCodeText.Text = Nothing
            Me.moYearText.Text = Nothing
            EnableDisableFields()
        End Sub

        Private Sub CreateNewWithCopy()
            Dim newObj As New VSCModel
            newObj.Copy(Me.State.MyBO)

            Me.State.MyBO = newObj
            Me.PopulateFormFromBOs()
            Me.moCarCodeText.Text = Nothing
            Me.moExternalCarCodeText.Text = Nothing 'REQ-1142
            'create the backup copy
            Me.State.ScreenSnapShotBO = New VSCModel
            Me.State.ScreenSnapShotBO.Copy(Me.State.MyBO)

            EnableDisableFields()
        End Sub

        Private Sub UpdateBreadCrum()
            If (Not Me.State Is Nothing) Then
                Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("VSC_MODEL")
            End If
        End Sub

#End Region

#Region "Handlers-Buttons"
        Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
            Try
                Me.PopulateBOsFromForm()
                If Me.State.MyBO.IsDirty Then
                    Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO_CANCEL, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
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

        Private Sub btnApply_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApply_WRITE.Click
            Try
                Me.PopulateBOsFromForm()
                If Me.State.MyBO.IsDirty Then
                    Me.State.MyBO.Save()
                    Me.State.HasDataChanged = True
                    ClearAll()
                    Me.PopulateFormFromBOs()
                    'Me.DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                    Me.MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
                Else
                    'Me.DisplayMessage(Message.MSG_RECORD_NOT_SAVED, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                    Me.MasterPage.MessageController.AddInformation(Message.MSG_RECORD_NOT_SAVED)
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnUndo_Write_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUndo_WRITE.Click
            Try
                If Not Me.State.MyBO.IsNew Then
                    'Reload from the DB
                    Me.State.MyBO = New VSCModel(Me.State.MyBO.Id)
                ElseIf Not Me.State.ScreenSnapShotBO Is Nothing Then
                    'It was a new with copy
                    Me.State.MyBO.Clone(Me.State.ScreenSnapShotBO)
                Else
                    Me.State.MyBO = New VSCModel
                End If
                Me.PopulateFormFromBOs()
                EnableDisableFields()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnDelete_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete_WRITE.Click
            Try
                Me.DisplayMessage(Message.DELETE_RECORD_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                'undo the delete
                Me.State.MyBO.RejectChanges()
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnNew_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew_WRITE.Click
            Try
                Me.PopulateBOsFromForm()
                If Me.State.MyBO.IsDirty Then
                    Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO_CANCEL, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
                Else
                    Me.CreateNew()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnCopy_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopy_WRITE.Click
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

                Me.moMakeDrop.Populate(Manufacturer.ToArray(),
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

                Me.moCoverageLimitCodeDrop.Populate(CoverageLimits.ToArray(),
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

                Me.moClassCodeNewDrop.Populate(ClassCodes.ToArray(),
                                        New PopulateOptions() With
                                        {
                                            .AddBlankItem = True
                                        })

                Me.moClassCodeUsedDrop.Populate(ClassCodes.ToArray(),
                                        New PopulateOptions() With
                                        {
                                            .AddBlankItem = True
                                        })




                If Me.State.IsVSCModelNew = True Then
                    BindSelectItem(Nothing, Me.moMakeDrop)
                    BindSelectItem(Nothing, Me.moClassCodeNewDrop)
                    BindSelectItem(Nothing, Me.moClassCodeUsedDrop)
                    BindSelectItem(Nothing, Me.moCoverageLimitCodeDrop)
                Else
                    BindSelectItem(TheVSCModel.ManufacturerId().ToString, Me.moMakeDrop)
                    BindSelectItem(TheVSCModel.NewClassCodeId.ToString, Me.moClassCodeNewDrop)
                    BindSelectItem(TheVSCModel.UsedClassCodeId.ToString, Me.moClassCodeUsedDrop)
                End If
            Catch ex As Exception
                Me.MasterPage.MessageController.Show()
            End Try
        End Sub

#End Region

#Region "Clear"

        Private Sub ClearAll()
            Me.moYearText.Text = Nothing
            Me.moEngineVersionText.Text = Nothing
            Me.moYearText.Text = Nothing
            Me.moEngineVersionText.Text = Nothing
            Me.moModelText.Text = Nothing
            moMakeDrop.SelectedIndex = NOTHING_SELECTED
            moClassCodeNewDrop.SelectedIndex = NOTHING_SELECTED
            moClassCodeUsedDrop.SelectedIndex = NOTHING_SELECTED
            moCoverageLimitCodeDrop.SelectedIndex = NOTHING_SELECTED
            Me.moCarCodeText.Text = Nothing
            Me.moExternalCarCodeText.Text = Nothing 'REQ-1142
            'ClearList(moMakeDrop)
            'ClearList(moClassCodeNewDrop)
            'ClearList(moClassCodeUsedDrop)
            Me.CHK_NEW_ACTIVE.Checked = True
            Me.CHK_USED_ACTIVE.Checked = True

        End Sub

#End Region

#Region "State-Management"


#End Region


    End Class

End Namespace
