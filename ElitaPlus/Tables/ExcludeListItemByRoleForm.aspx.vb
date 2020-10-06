Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common

Namespace Tables

    Partial Class ExcludeListItemByRoleForm
        Inherits ElitaPlusSearchPage

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
        Private Const LABEL_COMPANY As String = "COMPANY"
        Private Const LABEL_LIST As String = "LIST"
        Private Const LABEL_LIST_ITEM As String = "LIST_ITEM"
        Private Const EXCLUDE_LIST_ITEM_BY_ROLE_FORM As String = "EXCLUDE_LIST_ITEM_BY_ROLE_FORM" ' Maintain Exclude List By Role Exception
        Private Const LIST_ITEM_ID_PROPERTY As String = "ListItemId"
        Private Const LIST_ID_PROPERTY As String = "ListId"
        Private Const COMPANY_ID_PROPERTY As String = "CompanyId"
        Public Const URL As String = "ExcludeListItemByRoleForm.aspx"

#End Region

#Region "Parameters"
        Public Class Parameters
            Public ExcludeListItemByRoleId As Guid = Nothing
            Public CompanyId As Guid = Nothing
            Public ListId As Guid = Nothing
            Public ListItemId As Guid = Nothing
            Public RoleId As Guid = Nothing

            Public Sub New(ExcludeListItemByRoleId As Guid, CompanyId As Guid, ListId As Guid, ListItemId As Guid, RoleId As Guid)
                Me.ExcludeListItemByRoleId = ExcludeListItemByRoleId
                Me.CompanyId = CompanyId
                Me.ListId = ListId
                Me.ListItemId = ListItemId
                Me.RoleId = RoleId
            End Sub

        End Class
#End Region

#Region "Page State"

        Class MyState
            Public moExcludeListitemByRole As ExcludeListitemByRole = Nothing
            Public moCompanyId As Guid = Guid.Empty
            Public moListId As Guid = Guid.Empty
            Public moListItemId As Guid = Guid.Empty
            Public moExcludeListItemByRoleId As Guid = Guid.Empty
            Public ScreenSnapShotBO As ExcludeListitemByRole
            Public SelectedListCode As String = Nothing
            Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
            Public boChanged As Boolean = False
            Public InputParameters As Parameters

            Public LastOperation As DetailPageCommand = DetailPageCommand.Nothing_

            Public IsExcludeListItemByRoleNew As Boolean = False
            Public IsEditMode As Boolean
            Public IsReadOnly As Boolean = False
            Public Action As String


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

            If State.moExcludeListItemByRoleId.Equals(Guid.Empty) Then
                State.IsExcludeListItemByRoleNew = True
                ClearAll()
                SetButtonsState(True)
                State.IsEditMode = True
            Else
                State.IsExcludeListItemByRoleNew = False
                SetButtonsState(False)
                State.IsEditMode = False

                State.moExcludeListitemByRole = New ExcludeListitemByRole(State.moExcludeListItemByRoleId)
                State.moCompanyId = State.moExcludeListitemByRole.CompanyId
                State.moListItemId = State.moExcludeListitemByRole.ListItemId
                State.moListId = State.moExcludeListitemByRole.ListId
            End If
            PopulateAll()
        End Sub

#End Region
        'test
#Region "Properties"

        Private ReadOnly Property TheExcludeListitemByRole(Optional ByVal obj As ExcludeListitemByRole = Nothing) As ExcludeListitemByRole
            Get
                '  If obj Is Nothing Then
                If State.moExcludeListitemByRole Is Nothing Then
                    If State.IsExcludeListItemByRoleNew = True Then
                        ' For creating, inserting
                        State.moExcludeListitemByRole = New ExcludeListitemByRole
                        State.moExcludeListItemByRoleId = State.moExcludeListitemByRole.Id
                    Else
                        ' For updating, deleting
                        State.moExcludeListitemByRole = New ExcludeListitemByRole(State.moExcludeListItemByRoleId)

                    End If
                End If

                Return State.moExcludeListitemByRole
            End Get
        End Property
        Public ReadOnly Property TheListItemControl() As MultipleColumnDDLabelControl_New
            Get
                If ListItemDropControl Is Nothing Then
                    ListItemDropControl = CType(FindControl("ListItemDropControl"), MultipleColumnDDLabelControl_New)
                End If
                Return ListItemDropControl
            End Get
        End Property

        Public ReadOnly Property TheListControl() As MultipleColumnDDLabelControl_New
            Get
                If ListDropControl Is Nothing Then
                    ListDropControl = CType(FindControl("ListDropControl"), MultipleColumnDDLabelControl_New)
                End If
                Return ListDropControl
            End Get
        End Property

        Public ReadOnly Property TheCompanyControl() As MultipleColumnDDLabelControl_New
            Get
                If CompanyDropControl Is Nothing Then
                    CompanyDropControl = CType(FindControl("CompanyDropControl"), MultipleColumnDDLabelControl_New)
                End If
                Return CompanyDropControl
            End Get
        End Property

#End Region

#Region "Handlers"

#Region "Handlers-Init"
        Protected WithEvents moTheListItemControl As MultipleColumnDDLabelControl
        Protected WithEvents moListMultipleDrop As MultipleColumnDDLabelControl


        Private Sub Page_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall
            Try
                If CallingParameters IsNot Nothing Then
                    'Get the id from the parent
                    State.InputParameters = CType(CallingParameters, Parameters)
                    State.moExcludeListItemByRoleId = State.InputParameters.ExcludeListItemByRoleId

                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles Me.PageReturn
            Dim retObj As ExcludeListItemByRoleSearchForm.ReturnType = CType(ReturnPar, ExcludeListItemByRoleSearchForm.ReturnType)
            State.moExcludeListItemByRoleId = retObj.ExcludeListItemByRoleId
            SetStateProperties()
            State.LastOperation = DetailPageCommand.Redirect_

            EnableDisableFields()

        End Sub



        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Try
                MasterPage.MessageController.Clear_Hide()
                ' ClearLabelsErrSign()
                If Not Page.IsPostBack Then
                    MasterPage.MessageController.Clear()
                    MasterPage.UsePageTabTitleInBreadCrum = False
                    MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Tables")
                    UpdateBreadCrum()
                    SetStateProperties()
                    AddControlMsg(btnDelete_WRITE, Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO,
                                                                        MSG_TYPE_CONFIRM, True)
                    EnableDisableFields()
                Else
                    CheckIfComingFromDeleteConfirm()
                End If

                BindBoPropertiesToLabels()

                CheckIfComingFromConfirm()
                If Not IsPostBack Then
                    AddLabelDecorations(TheExcludeListitemByRole)
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
            If Me.State.LastOperation = DetailPageCommand.Redirect_ Then
                MasterPage.MessageController.Clear_Hide()
                ClearLabelsErrSign()
                State.LastOperation = DetailPageCommand.Nothing_
            Else
                ShowMissingTranslations(MasterPage.MessageController)
            End If
        End Sub

#End Region

#Region "Handlers-Buttons"

        Private Sub btnApply_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnApply_WRITE.Click
            ApplyChanges()
        End Sub

        Private Sub GoBack()
            Dim retType As New ExcludeListItemByRoleSearchForm.ReturnType(ElitaPlusPage.DetailPageCommand.Back,
                                                              State.moExcludeListItemByRoleId, State.InputParameters, State.boChanged)
            ReturnToCallingPage(retType)
            'Me.callPage(ExcludeListItemByRoleForm.PRODUCTCODE_LIST, param)

        End Sub

        Private Sub btnBack_Click(sender As System.Object, e As System.EventArgs) Handles btnBack.Click
            Try
                If IsDirtyBO() = True Then
                    DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM,
                                                HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    GoBack()
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnUndo_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnUndo_WRITE.Click
            Try
                If Not State.IsExcludeListItemByRoleNew Then
                    'Reload from the DB
                    State.moExcludeListitemByRole = New ExcludeListitemByRole(State.moExcludeListItemByRoleId)
                ElseIf State.ScreenSnapShotBO IsNot Nothing Then
                    'It was a new with copy
                    State.moExcludeListitemByRole.Clone(State.ScreenSnapShotBO)
                    State.moCompanyId = State.moExcludeListitemByRole.CompanyId
                    State.moListId = State.moExcludeListitemByRole.ListId
                    State.moListItemId = State.moExcludeListitemByRole.ListItemId
                Else
                    State.moExcludeListitemByRole = New ExcludeListitemByRole
                End If
                PopulateAll()
                SetButtonsState(False)
                State.IsEditMode = True
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub CreateNew()
            State.ScreenSnapShotBO = Nothing
            State.moExcludeListItemByRoleId = Guid.Empty
            State.IsExcludeListItemByRoleNew = True
            State.moExcludeListitemByRole = New ExcludeListitemByRole
            'ClearAll()
            SetButtonsState(True)
            PopulateAll()
            State.IsEditMode = True

            TheCompanyControl.SelectedIndex = -1
            TheListControl.SelectedIndex = -1
            TheListItemControl.SelectedIndex = -1

            TheCompanyControl.ChangeEnabledControlProperty(True)
            TheListControl.ChangeEnabledControlProperty(True)
            TheListItemControl.ChangeEnabledControlProperty(True)

        End Sub

        Private Sub btnNew_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnNew_WRITE.Click
            Try
                If IsDirtyBO() = True Then
                    DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
                Else
                    ClearAll()
                    CreateNew()
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub CreateNewCopy()

            ' Dim newObjDummy As New ExcludeListitemByRole
            Dim newObj As New ExcludeListitemByRole
            newObj.Copy(TheExcludeListitemByRole)

            State.moExcludeListitemByRole = newObj
            'newObjDummy = TheExcludeListitemByRole(newObj)

            State.moExcludeListItemByRoleId = Guid.Empty
            State.IsExcludeListItemByRoleNew = True

            SetButtonsState(True)
            TheCompanyControl.ChangeEnabledControlProperty(True)
            TheCompanyControl.SelectedIndex = -1

            'create the backup copy
            State.ScreenSnapShotBO = New ExcludeListitemByRole
            State.ScreenSnapShotBO.Copy(TheExcludeListitemByRole)

            With TheExcludeListitemByRole '(newObj)
                .CompanyId = Guid.Empty

            End With
        End Sub


        Private Sub btnCopy_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnCopy_WRITE.Click
            Try
                If IsDirtyBO() = True Then
                    DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
                Else
                    CreateNewCopy()
                    State.IsEditMode = True
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnDelete_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnDelete_WRITE.Click
            Try
                If DeleteExcludeListitemByRole() = True Then
                    State.boChanged = True
                    'Dim param As New ProductCodeSearchForm.ReturnType(ElitaPlusPage.DetailPageCommand.Delete, _
                    '                Me.State.moExcludeListitemByRoleId)
                    'param.BoChanged = True
                    'Me.callPage(ExcludeListItemByRoleForm.PRODUCTCODE_LIST, param)
                    Dim retType As New ExcludeListItemByRoleSearchForm.ReturnType(ElitaPlusPage.DetailPageCommand.Back,
                                                              State.moExcludeListItemByRoleId, State.InputParameters, State.boChanged)
                    retType.BoChanged = True
                    ReturnToCallingPage(retType)
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Handlers-DropDown"

        Private Sub OnFromDrop_Changed(fromMultipleDrop As MultipleColumnDDLabelControl_New) _
                        Handles CompanyDropControl.SelectedDropChanged
            Try
                State.moCompanyId = TheCompanyControl.SelectedGuid
                PopulateUserControlAvailableSelectedExcludeRoles()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub OnFromListDrop_Changed(fromMultipleDrop As MultipleColumnDDLabelControl_New) _
                Handles ListDropControl.SelectedDropChanged
            Try
                State.moListId = TheListControl.SelectedGuid
                PopulateListItemDropDowns(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                PopulateUserControlAvailableSelectedExcludeRoles()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub OnFromListItemDrop_Changed(fromMultipleDrop As MultipleColumnDDLabelControl_New) _
        Handles ListItemDropControl.SelectedDropChanged
            Try
                State.moListItemId = TheListItemControl.SelectedGuid
                PopulateUserControlAvailableSelectedExcludeRoles()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
#End Region

#End Region

#Region "Clear"


        Private Sub ClearAll()

            'TheCompanyControl.ClearMultipleDrop()
            'TheListControl.ClearMultipleDrop()
            'TheListItemControl.ClearMultipleDrop()

        End Sub

#End Region

#Region "Populate"

        Private Sub UpdateBreadCrum()
            If (State IsNot Nothing) Then
                If (State IsNot Nothing) Then
                    MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("EXCLUDE_LIST_ITEM_BY_ROLE")
                    MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("EXCLUDE_LIST_ITEM_BY_ROLE")
                End If
            End If
        End Sub


        Private Sub PopulateDropDowns()
            Try
                Dim oLanguageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
                Dim oCompanyDataView As DataView = LookupListNew.GetCompanyLookupList()
                TheCompanyControl.Caption = TranslationBase.TranslateLabelOrMessage(LABEL_COMPANY)
                TheCompanyControl.NothingSelected = True
                TheCompanyControl.BindData(oCompanyDataView)
                TheCompanyControl.AutoPostBackDD = True
                TheCompanyControl.NothingSelected = True
                TheCompanyControl.SelectedGuid = State.moCompanyId

                Dim oListDataView As DataView = LookupListNew.GetList(oLanguageId)
                TheListControl.Caption = "    " & TranslationBase.TranslateLabelOrMessage(LABEL_LIST)
                TheListControl.NothingSelected = True
                TheListControl.BindData(oListDataView)
                TheListControl.AutoPostBackDD = True
                TheListControl.NothingSelected = True
                TheListControl.SelectedGuid = State.moListId

                PopulateListItemDropDowns(oLanguageId)

            Catch ex As Exception
                MasterPage.MessageController.AddError(EXCLUDE_LIST_ITEM_BY_ROLE_FORM)
                MasterPage.MessageController.AddError(ex.Message, False)
                MasterPage.MessageController.Show()
            End Try
        End Sub

        Private Sub PopulateListItemDropDowns(oLanguageId As Guid)


            If Not State.moListId.Equals(Guid.Empty) Then

                State.SelectedListCode = LookupListNew.GetCodeFromId(LookupListCache.LK_LIST, State.moListId)

                Dim oListItemDataView As DataView = LookupListNew.DropdownLookupList(State.SelectedListCode, oLanguageId)


                TheListItemControl.NothingSelected = True
                TheListItemControl.BindData(oListItemDataView)
                TheListItemControl.AutoPostBackDD = True
                TheListItemControl.NothingSelected = True
                TheListItemControl.SelectedGuid = State.moListItemId
            End If

            TheListItemControl.Caption = TranslationBase.TranslateLabelOrMessage(LABEL_LIST_ITEM)

        End Sub
        Private Sub PopulateAll()
            If State.IsExcludeListItemByRoleNew = True Then
                PopulateDropDowns()
                PopulateUserControlAvailableSelectedExcludeRoles()

            Else
                ClearAll()
                PopulateDropDowns()
                PopulateUserControlAvailableSelectedExcludeRoles()

            End If
        End Sub

        Sub PopulateUserControlAvailableSelectedExcludeRoles()
            UserControlAvailableSelectedExcludeRoles.BackColor = "#d5d6e4"
            ControlMgr.SetVisibleControl(Me, UserControlAvailableSelectedExcludeRoles, False)

            With TheExcludeListitemByRole
                If Not .Id.Equals(Guid.Empty) Then

                    Dim availableDv As DataView = .GetAvailableRoles()
                    Dim selectedDv As DataView = .GetSelectedRoles()
                    UserControlAvailableSelectedExcludeRoles.SetAvailableData(availableDv, LookupListNew.COL_CODE_AND_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)
                    UserControlAvailableSelectedExcludeRoles.SetSelectedData(selectedDv, LookupListNew.COL_CODE_AND_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)
                    ControlMgr.SetVisibleControl(Me, UserControlAvailableSelectedExcludeRoles, True)

                End If
            End With

        End Sub
#End Region

#Region "Gui-Validation"

        Private Sub SetButtonsState(bIsNew As Boolean)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, Not bIsNew)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, Not bIsNew)
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, Not bIsNew)
            TheCompanyControl.ChangeEnabledControlProperty(bIsNew)
            TheListControl.ChangeEnabledControlProperty(bIsNew)
            TheListItemControl.ChangeEnabledControlProperty(bIsNew)
        End Sub

#End Region

#Region "Business Part"

        Private Function IsDirtyBO() As Boolean
            Dim bIsDirty As Boolean = True

            Try
                With TheExcludeListitemByRole
                    bIsDirty = .IsDirty
                End With
            Catch ex As Exception
                MasterPage.MessageController.AddError(EXCLUDE_LIST_ITEM_BY_ROLE_FORM)
                MasterPage.MessageController.AddError(ex.Message, False)
                MasterPage.MessageController.Show()
            End Try
            Return bIsDirty
        End Function

        Private Function ApplyChanges() As Boolean

            Try
                If State.IsEditMode Then
                    State.moExcludeListitemByRole.CompanyId = TheCompanyControl.SelectedGuid
                    State.moExcludeListitemByRole.ListId = TheListControl.SelectedGuid
                    State.moExcludeListitemByRole.ListItemId = TheListItemControl.SelectedGuid
                    State.moExcludeListitemByRole.ExcludedRolesCount = UserControlAvailableSelectedExcludeRoles.SelectedList.Count
                    'If Me.UserControlAvailableSelectedExcludeRoles.SelectedList.Count = 0 Then
                    '    Throw New GUIException(Message.MSG_COMPANY_REQUIRED, Assurant.ElitaPlus.Common.ErrorCodes.BO_EXCLUDED_ROLES_MUST_BE_SELECTED_ERR)
                    'End If
                End If

                If TheExcludeListitemByRole.IsDirty() Then
                    TheExcludeListitemByRole.Save()
                    State.boChanged = True
                    If State.IsExcludeListItemByRoleNew = True Then
                        State.IsExcludeListItemByRoleNew = False
                    End If
                    PopulateAll()
                    EnableDisableFields()
                    SetButtonsState(State.IsExcludeListItemByRoleNew)
                    DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)
                Else
                    DisplayMessage(Message.MSG_RECORD_NOT_SAVED, "", MSG_BTN_OK, MSG_TYPE_INFO)
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Function

        Private Function DeleteExcludeListitemByRole() As Boolean
            Dim bIsOk As Boolean = True

            Try
                With TheExcludeListitemByRole
                    .BeginEdit()
                    .Delete()
                    .Save()
                End With
            Catch ex As Exception
                MasterPage.MessageController.AddError(EXCLUDE_LIST_ITEM_BY_ROLE_FORM)
                MasterPage.MessageController.AddError(ex.Message, False)
                MasterPage.MessageController.Show()
                bIsOk = False
            End Try
            Return bIsOk
        End Function

        Protected Sub CheckIfComingFromDeleteConfirm()
            Dim confResponse As String = HiddenDeletePromptResponse.Value
            If confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_YES Then
                If Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete Then
                    DoDelete()
                    'Clean after consuming the action
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                    HiddenDeletePromptResponse.Value = ""
                End If
            ElseIf confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_NO Then
                'Me.State.searchDV = Nothing
                'ReturnProductPolicyFromEditing()
                'Clean after consuming the action
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                HiddenDeletePromptResponse.Value = ""
            End If
        End Sub
#End Region

#Region "Handlers-Labels"

        Private Sub BindBoPropertiesToLabels()

            BindBOPropertyToLabel(TheExcludeListitemByRole, LIST_ITEM_ID_PROPERTY, TheListItemControl.CaptionLabel)
            BindBOPropertyToLabel(TheExcludeListitemByRole, COMPANY_ID_PROPERTY, TheCompanyControl.CaptionLabel)
            BindBOPropertyToLabel(TheExcludeListitemByRole, LIST_ID_PROPERTY, TheListControl.CaptionLabel)
            ClearGridHeadersAndLabelsErrSign()

        End Sub

        Private Sub ClearLabelsErrSign()

            ClearLabelErrSign(TheCompanyControl.CaptionLabel)
            ClearLabelErrSign(TheListItemControl.CaptionLabel)

        End Sub
#End Region

#Region "State-Management"

        Protected Sub ComingFromBack()
            Dim confResponse As String = HiddenSaveChangesPromptResponse.Value

            If Not confResponse = String.Empty Then
                ' Return from the Back Button

                Select Case confResponse
                    Case MSG_VALUE_YES
                        ' Save and go back to Search Page
                        If ApplyChanges() = True Then
                            State.boChanged = True
                            GoBack()
                        End If
                    Case MSG_VALUE_NO
                        GoBack()
                End Select
            End If

        End Sub

        Protected Sub ComingFromNewCopy()
            Dim confResponse As String = HiddenSaveChangesPromptResponse.Value

            If Not confResponse = String.Empty Then
                ' Return from the New Copy Button

                Select Case confResponse
                    Case MSG_VALUE_YES
                        ' Save and create a new Copy BO
                        If ApplyChanges() = True Then
                            State.boChanged = True
                            CreateNewCopy()
                        End If
                    Case MSG_VALUE_NO
                        ' create a new BO
                        CreateNewCopy()
                End Select
            End If

        End Sub
        Protected Sub ComingFromNew()
            Dim confResponse As String = HiddenSaveChangesPromptResponse.Value

            If Not confResponse = String.Empty Then
                ' Return from the New Copy Button

                Select Case confResponse
                    Case MSG_VALUE_YES
                        ' Save and create a new Copy BO
                        If ApplyChanges() = True Then
                            State.boChanged = True
                            CreateNew()
                        End If
                    Case MSG_VALUE_NO
                        ' create a new BO
                        CreateNew()
                End Select
            End If

        End Sub


        Protected Sub CheckIfComingFromConfirm()
            Try
                Select Case State.ActionInProgress
                    ' Period
                    Case ElitaPlusPage.DetailPageCommand.Back
                        ComingFromBack()
                    Case ElitaPlusPage.DetailPageCommand.New_
                        ComingFromNew()
                    Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                        ComingFromNewCopy()
                End Select

                'Clean after consuming the action
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                HiddenSaveChangesPromptResponse.Value = String.Empty
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub DoDelete()

            'Me.State.MyProdPolicyDetailChildBO = TheProductCode.GetProductPolicyDetailChild(Me.State.ProductPolicyId)
            'Try
            '    Me.State.MyProdPolicyDetailChildBO.Delete()
            '    Me.State.MyProdPolicyDetailChildBO.Save()
            '    Me.State.MyProdPolicyDetailChildBO.EndEdit()
            '    ' Me.State.MyProductPolicyBO.Id = Guid.Empty
            '    Me.State.MyProdPolicyDetailChildBO = Nothing
            '    Me.State.ProductPolicySearchDV = Nothing

            'Catch ex As Exception
            '    TheProductCode.RejectChanges()
            '    Throw ex
            'End Try

            'ReturnProductPolicyFromEditing()

            'Set the IsAfterSave flag to TRUE so that the Paging logic gets invoked
            State.IsEditMode = False
        End Sub

#End Region

#Region "Controlling Logic"

        Protected Sub EnableDisableFields()

            TheCompanyControl.ChangeEnabledControlProperty(State.IsEditMode)
            TheListControl.ChangeEnabledControlProperty(State.IsEditMode)
            TheListItemControl.ChangeEnabledControlProperty(State.IsEditMode)

            'If (Me.State.DealerTypeID.Equals(Me.State.dealerTypeVSC)) Then
            '    ControlMgr.SetVisibleControl(Me, TRPrdCode, False)
            '    'ControlMgr.SetVisibleControl(Me, TRPrdDesc, False)
            '    ControlMgr.SetVisibleControl(Me, TRPlanCode, True)
            '    'ControlMgr.SetVisibleControl(Me, TRHR, True)
            '    tab_ExtProdCode_Policy_WRITE.Enabled = True
            '    ControlMgr.SetVisibleControl(Me, moProductPolicyDatagrid, True)
            'Else
            '    ControlMgr.SetVisibleControl(Me, TRPrdCode, True)
            '    'ControlMgr.SetVisibleControl(Me, TRPrdDesc, True)
            '    ControlMgr.SetVisibleControl(Me, TRPlanCode, False)
            '    'ControlMgr.SetVisibleControl(Me, TRHR, False)
            '    tab_ExtProdCode_Policy_WRITE.Enabled = False
            '    ControlMgr.SetVisibleControl(Me, moProductPolicyDatagrid, False)
            'End If


        End Sub

#End Region

#Region "Regions: Attach - Detach Event Handlers"

        Private Sub UserControlAvailableSelectedExcludeRoles_Attach(aSrc As Generic.UserControlAvailableSelected_New, attachedList As System.Collections.ArrayList) Handles UserControlAvailableSelectedExcludeRoles.Attach
            Try
                If attachedList.Count > 0 Then
                    State.IsEditMode = True
                    TheExcludeListitemByRole.AttachRoles(attachedList)
                Else

                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub UserControlAvailableSelectedExcludeRoles_Detach(aSrc As Generic.UserControlAvailableSelected_New, detachedList As System.Collections.ArrayList) Handles UserControlAvailableSelectedExcludeRoles.Detach
            Try
                If detachedList.Count > 0 Then
                    State.IsEditMode = True
                    TheExcludeListitemByRole.DetachRoles(detachedList)
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "ProductPolicy_Handlers_Grid"




        Private Sub PopulateBOFromForm()
            '  Try

            'Dim cboEquipmentType As DropDownList = CType(Me.moProductPolicyDatagrid.Rows(Me.moProductPolicyDatagrid.EditIndex).Cells(Me.TYPE_OF_EQUIPMENT_ID).FindControl(Me.TYPE_OF_EQUIPMENT_CONTROL_NAME), DropDownList)
            'Dim EquipmentTypeId As Guid = Me.GetSelectedItem(cboEquipmentType)
            'Dim EquipmemtTypedesc As String = LookupListNew.GetDescriptionFromId("TEQP", EquipmentTypeId)

            'Dim cboExtProdCode As DropDownList = CType(Me.moProductPolicyDatagrid.Rows(Me.moProductPolicyDatagrid.EditIndex).Cells(Me.EXTERNAL_PROD_CODE_ID).FindControl(Me.EXTERNAL_PROD_CODE_CONTROL_NAME), DropDownList)
            'Dim ExtProdCodeId As Guid = Me.GetSelectedItem(cboExtProdCode)


            'Dim ExtProdCodeDV As DataView = LookupListNew.DropdownLookupList("ACSPC", ElitaPlusIdentity.Current.ActiveUser.LanguageId, True)
            'Dim ExtProdDesc As String = LookupListNew.GetDescriptionFromId(ExtProdCodeDV, ExtProdCodeId)

            'Dim txtProductPolicy As TextBox = CType(Me.moProductPolicyDatagrid.Rows(Me.moProductPolicyDatagrid.EditIndex).Cells(Me.POLICY_NUM).FindControl(Me.POLICY_NUM_TEXTBOX_CONTROL_NAME), TextBox)

            'Me.PopulateBOProperty(Me.State.MyProductPolicyBO, "ProductCodeId", TheExcludeListitemByRole.Id)
            'Me.PopulateBOProperty(Me.State.MyProductPolicyBO, "TypeOfEquipmentId", EquipmentTypeId)
            'Me.PopulateBOProperty(Me.State.MyProductPolicyBO, "TypeOfEquipment", EquipmemtTypedesc)
            'Me.PopulateBOProperty(Me.State.MyProductPolicyBO, "ExternalProdCodeId", ExtProdCodeId)
            'Me.PopulateBOProperty(Me.State.MyProductPolicyBO, "ExternalProdCode", ExtProdDesc)
            'Me.PopulateBOProperty(Me.State.MyProductPolicyBO, "Policy", txtProductPolicy)

            'If Me.ErrCollection.Count > 0 Then
            '    Throw New PopulateBOErrorException
            'End If

        End Sub

        Private Sub PopulateFormFromBO()
            '  Try

            'Dim cboEquipmentType As DropDownList = CType(Me.moProductPolicyDatagrid.Rows(Me.moProductPolicyDatagrid.EditIndex).Cells(Me.TYPE_OF_EQUIPMENT_ID).FindControl(Me.TYPE_OF_EQUIPMENT_CONTROL_NAME), DropDownList)
            'Dim EquipmentTypeId As Guid = Me.GetSelectedItem(cboEquipmentType)
            'Dim EquipmemtTypedesc As String = LookupListNew.GetDescriptionFromId("TEQP", EquipmentTypeId)

            'Dim cboExtProdCode As DropDownList = CType(Me.moProductPolicyDatagrid.Rows(Me.moProductPolicyDatagrid.EditIndex).Cells(Me.EXTERNAL_PROD_CODE_ID).FindControl(Me.EXTERNAL_PROD_CODE_CONTROL_NAME), DropDownList)
            'Dim ExtProdCodeId As Guid = Me.GetSelectedItem(cboExtProdCode)


            'Dim ExtProdCodeDV As DataView = LookupListNew.DropdownLookupList("ACSPC", ElitaPlusIdentity.Current.ActiveUser.LanguageId, True)
            'Dim ExtProdDesc As String = LookupListNew.GetDescriptionFromId(ExtProdCodeDV, ExtProdCodeId)

            'Dim txtProductPolicy As TextBox = CType(Me.moProductPolicyDatagrid.Rows(Me.moProductPolicyDatagrid.EditIndex).Cells(Me.POLICY_NUM).FindControl(Me.POLICY_NUM_TEXTBOX_CONTROL_NAME), TextBox)

            'Me.PopulateBOProperty(Me.State.MyProductPolicyBO, "ProductCodeId", TheExcludeListitemByRole.Id)
            'Me.PopulateBOProperty(Me.State.MyProductPolicyBO, "TypeOfEquipmentId", EquipmentTypeId)
            'Me.PopulateBOProperty(Me.State.MyProductPolicyBO, "TypeOfEquipment", EquipmemtTypedesc)
            'Me.PopulateBOProperty(Me.State.MyProductPolicyBO, "ExternalProdCodeId", ExtProdCodeId)
            'Me.PopulateBOProperty(Me.State.MyProductPolicyBO, "ExternalProdCode", ExtProdDesc)
            'Me.PopulateBOProperty(Me.State.MyProductPolicyBO, "Policy", txtProductPolicy)

            'If Me.ErrCollection.Count > 0 Then
            '    Throw New PopulateBOErrorException
            'End If

        End Sub
#End Region


    End Class

End Namespace
