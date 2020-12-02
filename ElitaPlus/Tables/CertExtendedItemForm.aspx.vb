Imports Assurant.ElitaPlus.DALObjects
Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms

Public Class CertExtendedItemForm
    Inherits ElitaPlusSearchPage

#Region "Private Variables and Properties"
    Private _yesNoDataView As DataView
#End Region

#Region "Constants"
    Public Const Url As String = "CertExtendedItemForm.aspx"
    Private Const DEFAULT_SORT As String = "FIELD_NAME ASC"

    Private Const GRID_COL_ID_IDX As Integer = 0
    Private Const GRID_COL_FIELD_NAME_IDX As Integer = 1
    Private Const GRID_COL_DEFAULT_VALUE_IDX As Integer = 2
    Private Const GRID_COL_ALLOW_UPDATE_IDX As Integer = 3
    Private Const GRID_COL_ALLOW_DISPLAY_IDX As Integer = 4

    Private Const MSG_RECORD_DELETED_OK As String = "MSG_RECORD_DELETED_OK"
    Private Const MSG_RECORD_SAVED_OK As String = "MSG_RECORD_SAVED_OK"
    Private Const MSG_RECORD_NOT_SAVED As String = "MSG_RECORD_NOT_SAVED"

    Private Const FIELD_NAME_LABEL_NAME As String = "FieldNameLabel"
    Private Const FIELD_NAME_LABEL_NAME_EDIT As String = "FieldNameLabelEdit"
    Private Const DEFAULT_VALUE_LABEL_NAME As String = "DefaultValueLabel"
    Private Const ALLOW_UPDATE_LABEL_NAME As String = "AllowUpdateLabel"
    Private Const ALLOW_DISPLAY_LABEL_NAME As String = "AllowDisplayLabel"
    Private Const EDIT_BUTTON_NAME As String = "EditButton"
    Private Const DELETE_BUTTON_NAME As String = "DeleteButton"

    Private Const FIELD_NAME_TEXTBOX_NAME As String = "FieldNameTextBox"
    Private Const DEFAULT_VALUE_TEXTBOX_NAME As String = "DefaultValueTextBox"
    Private Const ALLOW_UPDATE_DROPDOWN_NAME As String = "AllowUpdateDropDown"
    Private Const ALLOW_DISPLAY_DROPDOWN_NAME As String = "AllowDisplayDropDown"
    Private Const CANCEL_LINK_BUTTON_NAME As String = "CancelLinkButton"
    Private Const SAVE_BUTTON_NAME As String = "SaveButton"

    Private Const FIELD_NAME_TEXTBOX_MAX_LENGTH As Integer = 25
    Private Const DEFAULT_VALUE_TEXTBOX_MAX_LENGTH As Integer = 20
    Private Const CODE_TEXTBOX_MAX_LENGTH As Integer = 9
    Private Const DESCRIPTION_TEXTBOX_MAX_LENGTH As Integer = 50

    Private Const ADMIN As String = "Admin"
    Private Const CERTITEMEXTENDEDCONTROL As String = "Certificate Extended Item Control"
#End Region
#Region "Page Return Type"
    Public Class ReturnType
        Public LastOperation As DetailPageCommand
        Public EditingBo As CertExtendedItem
        Public HasDataChanged As Boolean
        Public Sub New(ByVal lastOp As DetailPageCommand, ByVal curEditingBo As CertExtendedItem, ByVal hasDataChanged As Boolean)
            Me.LastOperation = lastOp
            Me.EditingBo = curEditingBo
            Me.HasDataChanged = hasDataChanged
        End Sub
    End Class
#End Region

#Region "Page State"
    Class MyState
        Public SelectedPageSize As Integer = 10
        Public TableName As String = "ELP_CERT_EXTENDED_ITEM"
        Public CodeMask As String
        Public Description As String
        Public SortExpression As String = DEFAULT_SORT
        Public PageIndex As Integer
        Public Count As Integer
        Public MyBo As CertExtendedItem= Nothing
        Public DataSet As DataSet
        Public Dv As DataView
        Public ActionInProgress As DetailPageCommand
        Public LastErrMsg As String
        Public CertExtConfigId As Guid = Guid.Empty
        Public HasDataChanged As Boolean
        Public IsRowBeingEdited As Boolean = False
        Public SearchDv As CertExtendedItem.CertExtendedItemSearchDV = Nothing
        Public IsGridVisible As Boolean
        Public BnoRow As Boolean = False
        Public IsRowEdit As Boolean = False
        Public TempDataSet As DataSet
        Public CallingFrom As Boolean=False
        Public BlnIsComingFromNew As Boolean = False
        Public IsBoDirty as Boolean=False
    End Class

    Public Sub New()
        MyBase.New(New MyState())
    End Sub

    Protected Shadows ReadOnly Property State() As MyState
        Get
            Return CType(MyBase.State, MyState)
        End Get
    End Property
#End Region

#Region "Button-Events"
    Public Sub BtnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            If State.IsBoDirty OrElse State.MyBo.IsDealerListModified(UserControlAvailableSelectedDealers.SelectedList,TextboxCertItemConfigCode.Text.ToUpper().Trim()) OrElse State.MyBo.IsCompanyListModified(UserControlAvailableSelectedCompanies.SelectedList,TextboxCertItemConfigCode.Text.ToUpper().Trim())  Then
                Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
            Else
                Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.HasDataChanged))
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.HasDataChanged))
        End Try
    End Sub
    Public Sub BtnAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAdd.Click
        Try
            State.IsGridVisible = True
            State.CertExtConfigId = Guid.Empty
            State.IsRowEdit = False
            If State.DataSet IsNot Nothing Then
                State.TempDataSet=State.DataSet.Copy()
            Else
                LoadData()
            End If
            BeginEdit(Guid.Empty)
            PopulateGrid()
            State.ActionInProgress = DetailPageCommand.New_
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Protected Sub BtnSaveConfig_Click(sender As Object, e As EventArgs) Handles btnSaveConfig.Click
        Try
            SaveMain()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Protected Sub TextBoxCertItemConfigCode_TextChanged(sender As Object, e As EventArgs) Handles TextboxCertItemConfigCode.TextChanged
        Try
            If btnAdd.Enabled = True AndAlso Not State.CodeMask.Equals(TextboxCertItemConfigCode.Text.ToUpper().Trim()) Then
                LoadData()
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Protected Sub LoadData()
        SetStateProperties()
        TextboxCertItemConfigDesc.Text = ""
        State.HasDataChanged = True
        State.IsGridVisible = True
        State.MyBo = New CertExtendedItem
        PopulateUserConctrols()
        State.SearchDv = CertExtendedItem.GetList(TextboxCertItemConfigCode.Text.ToUpper().Trim())
        PopulateGrid()
    End Sub
#End Region
    Private Sub SetGridViewButtonsVisibility(ByVal gridrow As GridViewRow, ByVal bEditButtonVisible As Boolean, ByVal bDeleteButtonVisible As Boolean, ByVal bSaveButtonVisible As Boolean, ByVal bCancelButtonVisible As Boolean)
        Try
            Dim editButton As ImageButton = CType(gridrow.Cells(EDIT_COL).FindControl(EDIT_CONTROL_NAME), ImageButton)
            If (Not editButton Is Nothing) Then
                editButton.Enabled = bEditButtonVisible
                editButton.Visible = bEditButtonVisible
            End If

            Dim deleteButton As ImageButton = CType(gridrow.Cells(EDIT_COL).FindControl(DELETE_CONTROL_NAME), ImageButton)
            If (Not deleteButton Is Nothing) Then
                deleteButton.Enabled = bDeleteButtonVisible
                deleteButton.Visible = bDeleteButtonVisible
            End If

            Dim saveButton As Button = CType(gridrow.Cells(EDIT_COL).FindControl(SAVE_CONTROL_NAME), Button)
            If (Not saveButton Is Nothing) Then
                saveButton.Enabled = bSaveButtonVisible
                saveButton.Visible = bSaveButtonVisible
            End If

            Dim cancelButton As Button = CType(gridrow.Cells(EDIT_COL).FindControl(CANCEL_CONTROL_NAME), Button)
            If (Not cancelButton Is Nothing) Then
                cancelButton.Enabled = bCancelButtonVisible
                cancelButton.Visible = bCancelButtonVisible
            End If


        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#Region "Grid-Events"
    Private Sub Grid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            GridViewCertItemConfig.PageIndex = NewCurrentPageIndex(GridViewCertItemConfig, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
            State.SelectedPageSize = CType(cboPageSize.SelectedValue, Integer)
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Public Property SortDirection() As String
        Get
            Return ViewState("SortDirection").ToString
        End Get
        Set(ByVal value As String)
            ViewState("SortDirection") = value
        End Set
    End Property

    Public Sub GridViewCertItemConfig_Sorting(ByVal source As Object, ByVal e As GridViewSortEventArgs) Handles GridViewCertItemConfig.Sorting
        Try
            Dim spaceIndex As Integer = SortDirection.LastIndexOf(" ")

            If spaceIndex > 0 AndAlso SortDirection.Substring(0, spaceIndex).Equals(e.SortExpression) Then
                If SortDirection.EndsWith(" ASC") Then
                    SortDirection = e.SortExpression & " DESC"
                Else
                    SortDirection = e.SortExpression & " ASC"
                End If
            Else
                SortDirection = e.SortExpression & " ASC"
            End If
            State.SortExpression = SortDirection
            State.PageIndex = 0
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Public Sub GridViewCertItemConfig_PageIndexChanged(ByVal sender As Object, ByVal e As GridViewPageEventArgs) Handles GridViewCertItemConfig.PageIndexChanging
        Try
            State.PageIndex = e.NewPageIndex
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub BindBoPropertiesToGridHeaders()
        BindBOPropertyToGridHeader(State.MyBo, "FieldName", GridViewCertItemConfig.Columns(GRID_COL_FIELD_NAME_IDX))
        BindBOPropertyToGridHeader(State.MyBo, "DefaultValue", GridViewCertItemConfig.Columns(GRID_COL_DEFAULT_VALUE_IDX))
        BindBOPropertyToGridHeader(State.MyBo, "AllowUpdate", GridViewCertItemConfig.Columns(GRID_COL_ALLOW_UPDATE_IDX))
        BindBOPropertyToGridHeader(State.MyBo, "AllowDisplay", GridViewCertItemConfig.Columns(GRID_COL_ALLOW_DISPLAY_IDX))
        ClearGridViewHeadersAndLabelsErrorSign()
    End Sub
    Protected Sub PopulateUserConctrols()
        ClearOptionSelection()
        UserControlAvailableSelectedCompanies.ClearLists()
        UserControlAvailableSelectedCompanies.SetAvailableData(State.MyBo.GetAvailableCompanies(), "Description", "COMPANY_ID")
        UserControlAvailableSelectedCompanies.SetSelectedData(State.MyBo.GetSelectedCompanies(State.CodeMask), "Description", "ID")
        UserControlAvailableSelectedCompanies.BackColor = "#d5d6e4"
        UserControlAvailableSelectedCompanies.RemoveSelectedFromAvailable()

        UserControlAvailableSelectedDealers.ClearLists()
        UserControlAvailableSelectedDealers.SetAvailableData(State.MyBo.GetAvailableDealers(), "Description", "ID")
        UserControlAvailableSelectedDealers.SetSelectedData(State.MyBo.GetSelectedDealers(State.CodeMask), "Description", "ID")
        UserControlAvailableSelectedDealers.BackColor = "#d5d6e4"
        UserControlAvailableSelectedDealers.RemoveSelectedFromAvailable()

        If UserControlAvailableSelectedDealers.SelectedList.Count > 0 Then
            rdoDealers.Checked = True
        Else
            rdoCompanies.Checked = True
        End If
    End Sub
    Public Sub GridViewCertItemConfig_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles GridViewCertItemConfig.RowCommand
        Try
            Dim nIndex As Integer

            Select Case e.CommandName
                Case ElitaPlusSearchPage.CANCEL_COMMAND_NAME
                    GridViewCertItemConfig.EditIndex = NO_ITEM_SELECTED_INDEX
                    EndEdit(ElitaPlusPage.DetailPageCommand.Cancel)
                    State.IsRowEdit = False
                    State.DataSet=State.TempDataSet.Copy()
                Case ElitaPlusSearchPage.DELETE_COMMAND_NAME
                    nIndex = CInt(e.CommandArgument)
                    State.CertExtConfigId = New Guid(GridViewCertItemConfig.Rows(nIndex).Cells(GRID_COL_ID_IDX).Text)
                    BeginEdit(State.CertExtConfigId,State.DataSet)
                    EndEdit(ElitaPlusPage.DetailPageCommand.Delete)
                    DisableFields()
                    State.IsBoDirty=True
                Case ElitaPlusSearchPage.SAVE_COMMAND_NAME
                    nIndex = CInt(e.CommandArgument)
                    PopulateBoFromControls(GridViewCertItemConfig.Rows(nIndex))
                    State.SearchDv=CertExtendedItem.GetExistingList(State.DataSet)
                    If (String.IsNullOrEmpty(TextboxCertItemConfigCode.Text)) Then
                        Throw New GUIException(Message.MSG_CERT_EXT_CODE_VALUE_REQUIRED, Assurant.ElitaPlus.Common.ErrorCodes.MSG_CERT_EXT_CODE_VALUE_REQUIRED)
                    End If
                    EndEdit(ElitaPlusPage.DetailPageCommand.OK)
                    DisableFields()
                    State.IsRowEdit = False
                    State.IsBoDirty=True
                Case ElitaPlusSearchPage.EDIT_COMMAND_NAME
                    nIndex = CInt(e.CommandArgument)
                    GridViewCertItemConfig.SelectedIndex = nIndex
                    GridViewCertItemConfig.EditIndex = nIndex
                    State.CertExtConfigId = New Guid(GridViewCertItemConfig.Rows(nIndex).Cells(GRID_COL_ID_IDX).Text)
                    BeginEdit(State.CertExtConfigId,State.DataSet)
                    State.TempDataSet=State.DataSet.Copy()
                    DisableFields()
                    State.IsRowEdit = True
            End Select
            PopulateGrid()
        Catch ex As Oracle.ManagedDataAccess.Client.OracleException
            Select Case ex.Number
                Case 20005
                    MasterPage.MessageController.AddInformation(TranslationBase.TranslateLabelOrMessage("FIELDNAME_CANNOT_BE_ADDED"), True)
                Case 20006
                    MasterPage.MessageController.AddInformation(TranslationBase.TranslateLabelOrMessage("FIELDNAME_CANNOT_BE_MODIFIED"), True)
                Case 20007
                    MasterPage.MessageController.AddInformation(TranslationBase.TranslateLabelOrMessage("FIELDNAME_CANNOT_BE_DELETED"), True)
            End Select
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Public Sub GridViewCertItemConfig_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles GridViewCertItemConfig.RowDataBound
        Try
            Dim rowType As DataControlRowType = CType(e.Row.RowType, DataControlRowType)
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

            Dim languageCode = Thread.CurrentPrincipal.GetLanguageCode()

            Dim yesNoList As DataElements.ListItem() =
                       CommonConfigManager.Current.ListManager.GetList(listCode:="YESNO",
                                                                       languageCode:=languageCode)

            If (rowType = DataControlRowType.DataRow) AndAlso (dvRow IsNot Nothing) Then
                Dim attribute As CertExtendedItem = new CertExtendedItem(dvRow.Row)
                Dim rowState As DataControlRowState = CType(e.Row.RowState, DataControlRowState)

                e.Row.Cells(GRID_COL_ID_IDX).Text = attribute.Id.ToString()

                If (rowState And DataControlRowState.Edit) = DataControlRowState.Edit Then

                    Dim defaultValueTextBox As TextBox = CType(e.Row.FindControl(DEFAULT_VALUE_TEXTBOX_NAME), TextBox)
                    Dim fieldNameTextBox As TextBox = CType(e.Row.FindControl(FIELD_NAME_TEXTBOX_NAME), TextBox)
                    Dim allowUpdateDropDown As DropDownList = CType(e.Row.FindControl(ALLOW_UPDATE_DROPDOWN_NAME), DropDownList)
                    Dim allowDisplayDropDown As DropDownList = CType(e.Row.FindControl(ALLOW_DISPLAY_DROPDOWN_NAME), DropDownList)

                    Dim populateOptions = New PopulateOptions() With
                                            {
                                               .AddBlankItem = False
                                            }

                    allowUpdateDropDown.Populate(yesNoList.ToArray(), populateOptions)
                    allowDisplayDropDown.Populate(yesNoList.ToArray(), populateOptions)

                    Dim defaultSelectedCodeId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_Y)

                    SetSelectedItem(allowUpdateDropDown, defaultSelectedCodeId)
                    SetSelectedItem(allowDisplayDropDown, defaultSelectedCodeId)

                    If Not String.IsNullOrEmpty(attribute.FieldName) Then
                        fieldNameTextBox.Text = attribute.FieldName
                    End If
                    fieldNameTextBox.MaxLength = FIELD_NAME_TEXTBOX_MAX_LENGTH

                    If Not String.IsNullOrEmpty(attribute.DefaultValue) Then
                        defaultValueTextBox.Text = attribute.DefaultValue
                    End If
                    defaultValueTextBox.MaxLength = DEFAULT_VALUE_TEXTBOX_MAX_LENGTH

                    If attribute.AllowUpdate IsNot Nothing Then
                        defaultSelectedCodeId = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, attribute.AllowUpdate)
                        SetSelectedItem(allowUpdateDropDown, defaultSelectedCodeId)
                    End If

                    If attribute.AllowDisplay IsNot Nothing Then
                        defaultSelectedCodeId = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, attribute.AllowDisplay)
                        SetSelectedItem(allowDisplayDropDown, defaultSelectedCodeId)
                    End If
                ElseIf Not IsEmpty(attribute.Id) Then
                    _yesNoDataView = LookupListNew.GetYesNoLookupList(LookupListNew.GetIdFromCode(LookupListNew.LK_LANGUAGES, languageCode), False)

                    Dim fieldNameLabel As Label = CType(e.Row.FindControl(FIELD_NAME_LABEL_NAME), Label)
                    Dim defaultValueLabel As Label = CType(e.Row.FindControl(DEFAULT_VALUE_LABEL_NAME), Label)
                    Dim allowUpdateLabel As Label = CType(e.Row.FindControl(ALLOW_UPDATE_LABEL_NAME), Label)
                    Dim allowDisplayLabel As Label = CType(e.Row.FindControl(ALLOW_DISPLAY_LABEL_NAME), Label)

                    fieldNameLabel.Text = attribute.FieldName
                    defaultValueLabel.Text = attribute.DefaultValue
                    allowUpdateLabel.Text = LookupListNew.GetDescriptionFromCode(_yesNoDataView, attribute.AllowUpdate)
                    allowDisplayLabel.Text = LookupListNew.GetDescriptionFromCode(_yesNoDataView, attribute.AllowDisplay)

                    TextboxCertItemConfigCode.Text = attribute.Code
                    TextboxCertItemConfigDesc.Text = attribute.Description
                End If
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Public Sub GridViewCertItemConfig_OnRowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles GridViewCertItemConfig.RowCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
#End Region
#Region "Page Events"
    Private Sub SetStateProperties()
        State.CodeMask = TextboxCertItemConfigCode.Text.ToUpper().Trim()
        State.Description = TextboxCertItemConfigDesc.Text.Trim()
    End Sub
    Private Sub UpdateBreadCrum()
        MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & CERTITEMEXTENDEDCONTROL
    End Sub
    Private Sub Page_PageCall(ByVal callFromUrl As String, ByVal callingPar As Object) Handles MyBase.PageCall

        Try
            If Not Me.CallingParameters Is Nothing Then
                'Get the id from the parent
                State.CodeMask=callingPar
                TextboxCertItemConfigCode.Enabled=False
                State.CallingFrom=True
                State.MyBo=New CertExtendedItem(Guid.Empty)
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Put user code to initialize the page here
        MasterPage.MessageController.Clear_Hide()
        Try
            MasterPage.MessageController.Clear()
            MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(ADMIN)
            MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(CERTITEMEXTENDEDCONTROL)
            UpdateBreadCrum()
            If Not IsPostBack Then
                'SetStateProperties()
                SetGridItemStyleColor(GridViewCertItemConfig)
                TranslateGridHeader(GridViewCertItemConfig)
                ShowMissingTranslations(MasterPage.MessageController)
                MenuEnabled = False
                SortDirection = DEFAULT_SORT
                If State.MyBo Is Nothing Then
                    State.MyBo = New CertExtendedItem
                End If
                If State.CallingFrom Then
                    State.IsGridVisible = True
                    PopulateGrid()
                Else
                    State.CodeMask = TextboxCertItemConfigCode.Text.ToUpper().Trim()
                    State.Description = TextboxCertItemConfigDesc.Text.Trim()
                    PopulateEmptyGrid()
                End If
                PopulateUserConctrols()
                rdoDealers.Attributes.Add("onClick", "javascript:changeSelection()")
                rdoCompanies.Attributes.Add("onClick", "javascript:changeSelection()")
                hrefCompany.Attributes.Add("onClick", "javascript:changeSelectionCompany()")
                hrefDealer.Attributes.Add("onClick", "javascript:changeSelectionDealer()")
                TextboxCertItemConfigCode.MaxLength = CODE_TEXTBOX_MAX_LENGTH
                TextboxCertItemConfigDesc.MaxLength = DESCRIPTION_TEXTBOX_MAX_LENGTH
            Else
                BindBoPropertiesToGridHeaders()
            End If
            CheckIfComingFromSaveConfirm()

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
        ShowMissingTranslations(MasterPage.MessageController)
    End Sub

    Private Sub CertExtendedItemForm_PageCall() Handles Me.PageCall
        If (State.DataSet Is Nothing) Then
        End If
    End Sub

#End Region

#Region "Controlling Logic"
    Protected Sub CheckIfComingFromSaveConfirm()
            Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value
            If Not State.BlnIsComingFromNew Then

                If Not confResponse Is Nothing AndAlso confResponse = MSG_VALUE_YES Then 'Me.CONFIRM_MESSAGE_OK Then
                If State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr Then
                    If State.IsBoDirty OrElse State.MyBo.IsDealerListModified(UserControlAvailableSelectedDealers.SelectedList,TextboxCertItemConfigCode.Text.ToUpper().Trim()) OrElse State.MyBo.IsCompanyListModified(UserControlAvailableSelectedCompanies.SelectedList,TextboxCertItemConfigCode.Text.ToUpper().Trim())  Then
                        SaveMain()
                    End If
                End If
                Select Case Me.State.ActionInProgress
                        Case ElitaPlusPage.DetailPageCommand.Back
                            Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.HasDataChanged))
                        Case ElitaPlusPage.DetailPageCommand.New_
                            Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                        Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                            Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                        Case ElitaPlusPage.DetailPageCommand.BackOnErr
                            Me.ReturnToCallingPage(New ReturnType(Me.State.ActionInProgress, Me.State.MyBO, Me.State.HasDataChanged))
                    End Select
                ElseIf Not confResponse Is Nothing AndAlso confResponse = MSG_VALUE_NO Then 
                    Select Case Me.State.ActionInProgress
                        Case ElitaPlusPage.DetailPageCommand.Back
                            Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.HasDataChanged))
                        Case ElitaPlusPage.DetailPageCommand.New_
                        Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                        Case ElitaPlusPage.DetailPageCommand.BackOnErr
                            Me.MasterPage.MessageController.AddErrorAndShow(Me.State.LastErrMsg)
                    End Select
                End If
                'Clean after consuming the action
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
            Else
                If Not confResponse Is Nothing AndAlso confResponse = MSG_VALUE_NO Then 'Me.CONFIRM_MESSAGE_CANCEL Then
                    Select Case Me.State.ActionInProgress
                        Case ElitaPlusPage.DetailPageCommand.Back
                            Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.HasDataChanged))
                    End Select
                End If
            End If
            Me.HiddenSaveChangesPromptResponse.Value = ""
        End Sub
    Public Sub PopulateGrid()
        If ((State.DataSet Is Nothing) OrElse (State.HasDataChanged)) Then
            State.DataSet = CertExtendedItem.GeDataSet(State.CodeMask)
        End If
        If (State.SearchDv Is Nothing) then
            State.SearchDv=CertExtendedItem.GetExistingList(State.DataSet)
        End if
        
        State.SearchDv.Sort = SortDirection
        GridViewCertItemConfig.AutoGenerateColumns = False
        SetPageAndSelectedIndexFromGuid(State.SearchDv, State.CertExtConfigId, GridViewCertItemConfig, State.PageIndex, State.IsRowBeingEdited)
        SortAndBindGrid()
        State.HasDataChanged = False
    End Sub

    Private Sub SortAndBindGrid()
        State.PageIndex = GridViewCertItemConfig.PageIndex

        If (State.SearchDv Is Nothing OrElse State.SearchDv.Count = 0) Then
            Session("recCount") = 0
            State.SearchDv.AddNew()
            State.SearchDv(0)(0) = Guid.Empty.ToByteArray
            GridViewCertItemConfig.DataSource = State.SearchDv
            GridViewCertItemConfig.DataBind()
            GridViewCertItemConfig.Rows(0).Visible = False
            GridViewCertItemConfig.Rows(0).Controls.Clear()
            State.BnoRow = True
        Else
            Session("recCount") = State.SearchDv.Count
            State.BnoRow = False
            GridViewCertItemConfig.Enabled = True
            GridViewCertItemConfig.DataSource = State.SearchDv
            HighLightSortColumn(GridViewCertItemConfig, SortDirection)
            GridViewCertItemConfig.DataBind()
        End If
        If Not GridViewCertItemConfig.BottomPagerRow.Visible Then GridViewCertItemConfig.BottomPagerRow.Visible = True

        ControlMgr.SetVisibleControl(Me, GridViewCertItemConfig, State.IsGridVisible)

        ControlMgr.SetVisibleControl(Me, trPageSize, GridViewCertItemConfig.Visible)

        If GridViewCertItemConfig.Visible Then
            lblRecordCount.Text = Session("recCount").ToString & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
        End If

    End Sub
    Private Sub BeginEdit(ByVal currentCertExtConfigId As Guid,Optional ByVal familyDs As DataSet=Nothing)
        Try
            State.IsRowBeingEdited = True
            With State
                If Not currentCertExtConfigId.Equals(Guid.Empty) Then
                    .MyBo = New CertExtendedItem(currentCertExtConfigId,familyDs)
                Else
                    .MyBo = New CertExtendedItem(State.DataSet)
                    .CertExtConfigId = State.MyBo.Id
                    If .SearchDv Is Nothing Then
                        .SearchDv = .MyBo.GetNewDataViewRow(CertExtendedItem.GeDataSet(State.CodeMask.ToUpper().Trim()), .CertExtConfigId)
                    Else
                        .SearchDv = .MyBo.GetNewDataViewRow(State.DataSet, .CertExtConfigId) '.SearchDv.Table.DataSet
                    End If
                End If
            End With
            EnableDisablePageButton(False)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Private Sub EndEdit(ByVal detailPageCommand As DetailPageCommand)
        Try
            With State
                Select Case detailPageCommand
                    Case ElitaPlusPage.DetailPageCommand.OK
                        .DataSet= .SearchDv.Table.DataSet
                        .MyBo.Save()
                        .MyBo.EndEdit()
                    Case ElitaPlusPage.DetailPageCommand.Delete
                        .MyBo.Delete()
                        .MyBo.Save()
                End Select
                SetStateProperties()
                PopulateUserConctrols()
                .MyBo = Nothing
                .CertExtConfigId = Guid.Empty
                .IsRowBeingEdited = False
                .SearchDv = Nothing
                EnableDisablePageButton(True)
            End With
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Sub PopulateEmptyGrid()
        Session("recCount") = 0
        GridViewCertItemConfig.EditIndex = -1
        ' Bind Data to Grid
        GridViewCertItemConfig.DataSource = New ArrayList()
        GridViewCertItemConfig.DataBind()
        If GridViewCertItemConfig.Visible Then
            lblRecordCount.Text = Session("recCount").ToString & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
        End If
    End Sub
#End Region

#Region "Save Logic"
    Protected Sub SaveDealerCompanyList()
        'populate selection of rule, dealer and company to children
        If rdoDealers.Checked AndAlso ((UserControlAvailableSelectedDealers.SelectedList.Count) > 0) Then
            State.MyBo.SaveDealerList(UserControlAvailableSelectedDealers.SelectedList, State.CodeMask)
        ElseIf rdoCompanies.Checked AndAlso ((UserControlAvailableSelectedCompanies.SelectedList.Count) > 0) Then
            State.MyBo.SaveCompanyList(UserControlAvailableSelectedCompanies.SelectedList, State.CodeMask)
        End If
        PopulateUserConctrols()
        If ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If
    End Sub
    Private Sub SaveMain()
        Try
            If ValidateCodeWithDataGrid() AndAlso ValidateDealerCompanyRecords() Then
                DealerCompanyConfigExist()
                ClearDealerCompanyList()
                State.MyBo.SaveConfig(State.DataSet)
                If (State.DataSet) IsNot Nothing  AndAlso (State.DataSet.Tables(0).Rows.Count>0) Then
                    SaveDealerCompanyList()
                    SaveDescription()
                End If
                If State.MyBo.IsDirty Then
                    State.IsBoDirty=False
                    State.blnIsComingFromNew = True
                    State.HasDataChanged = False
                    MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION, True)
                    DisableFields()
                    PopulateUserConctrols()
                    State.MyBo=Nothing
                    State.SearchDv=Nothing
                    State.DataSet=Nothing
                    PopulateGrid()
                Else
                    MasterPage.MessageController.AddSuccess(Message.MSG_RECORD_NOT_SAVED, True)
                End If
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Private Sub SaveDescription()
        Try
            Dim dvCertConfigDv As CertExtendedItem.CertExtendedItemSearchDv = CertExtendedItem.GetList(State.CodeMask)
            If Not dvCertConfigDv Is Nothing AndAlso dvCertConfigDv.Count > 0 Then
                Dim description As String = dvCertConfigDv.Table.Rows(0)(2).ToString()
                If Not TextboxCertItemConfigDesc.Text.Equals(description) AndAlso TextboxCertItemConfigDesc.Text.Length > 0 Then
                    State.MyBo.SaveDescription(State.CodeMask, TextboxCertItemConfigDesc.Text.Trim())
                End If
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
#End Region
    Private Sub PopulateBoFromControls(ByVal gridViewRow As GridViewRow)
        Try
            With State
                Dim txtFieldName As TextBox = CType(gridViewRow.FindControl(FIELD_NAME_TEXTBOX_NAME), TextBox)
                PopulateBOProperty(.MyBo, "FieldName", txtFieldName.Text.Trim())

                Dim txtDefaultValue As TextBox = CType(gridViewRow.FindControl(DEFAULT_VALUE_TEXTBOX_NAME), TextBox)
                PopulateBOProperty(.MyBo, "DefaultValue", txtDefaultValue.Text.Trim())

                Dim ddAllowUpdate As DropDownList = CType(gridViewRow.FindControl(ALLOW_UPDATE_DROPDOWN_NAME), DropDownList)
                PopulateBOProperty(.MyBo, "AllowUpdate", ddAllowUpdate.SelectedItem.Text.Substring(0, 1).Trim())

                Dim ddAllowDisplay As DropDownList = CType(gridViewRow.FindControl(ALLOW_DISPLAY_DROPDOWN_NAME), DropDownList)
                PopulateBOProperty(.MyBo, "AllowDisplay", ddAllowDisplay.SelectedItem.Text.Substring(0, 1).Trim())

                PopulateBOProperty(.MyBo, "Code", TextboxCertItemConfigCode.Text.ToUpper().Trim())
                PopulateBOProperty(.MyBo, "Description", TextboxCertItemConfigDesc.Text.Trim())

            End With
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
#Region "UI Validation Logic"
    Private Function ValidateCodeWithDataGrid() As Boolean
        If (String.IsNullOrEmpty(TextboxCertItemConfigCode.Text)) Then
            Throw New GUIException(Message.MSG_CERT_EXT_CODE_VALUE_REQUIRED, Assurant.ElitaPlus.Common.ErrorCodes.MSG_CERT_EXT_CODE_VALUE_REQUIRED)
        End If

        If (State.SearchDv.Count = 0) OrElse (State.SearchDv.Table.Rows.Count = 0) Then
            Throw New GUIException(Message.MSG_CERT_EXT_FIELD_NAME_REQUIRED, Assurant.ElitaPlus.Common.ErrorCodes.MSG_CERT_EXT_FIELD_NAME_REQUIRED)
        End If
        Return True
    End Function
    Private Function ValidateDealerCompanyRecords() As Boolean
        If rdoCompanies.Checked AndAlso (UserControlAvailableSelectedCompanies.SelectedList.Count) < 1 Then
            Throw New GUIException(Message.MSG_EMPTY_SELECTED_COMPANY_REQUIRED, Assurant.ElitaPlus.Common.ErrorCodes.MSG_EMPTY_SELECTED_COMPANY_REQUIRED)
        End If
        If rdoDealers.Checked AndAlso (UserControlAvailableSelectedDealers.SelectedList.Count) < 1 Then
            Throw New GUIException(Message.MSG_EMPTY_SELECTED_DEALER_REQUIRED, Assurant.ElitaPlus.Common.ErrorCodes.MSG_EMPTY_SELECTED_DEALER_REQUIRED)
        End If
        Return True
    End Function
    Private Sub DealerCompanyConfigExist()
        Dim dv As DataView
        If State.MyBo Is Nothing Then
            State.MyBo = New CertExtendedItem
        End If
        If rdoCompanies.Checked Then
            State.MyBo.DealerCompanyConfigExist(UserControlAvailableSelectedCompanies.SelectedList,"ELP_COMPANY",State.CodeMask,State.DataSet)

        ElseIf rdoDealers.Checked Then
            State.MyBo.DealerCompanyConfigExist(UserControlAvailableSelectedDealers.SelectedList,"ELP_DEALER",State.CodeMask,State.DataSet)
        End If

    End Sub
#End Region
#Region "Clear Selections"
    Private Sub ClearDealerCompanyList()
        If State.MyBo Is Nothing Then
            State.MyBo = New CertExtendedItem
        End If
        Dim dv As DataView
        'Detach companies from fields if nothing selected from CompanyList
        dv = State.MyBo.GetSelectedCompanies(State.CodeMask)
        If rdoDealers.Checked AndAlso dv.Count > 0 Then
            State.MyBo.ClearCompanyList(State.CodeMask)
        End If
        'Detach dealers from fields if nothing selected from DealerList
        dv = State.MyBo.GetSelectedDealers(State.CodeMask)
        If rdoCompanies.Checked AndAlso dv.Count > 0 Then
            State.MyBo.ClearDealerList(State.CodeMask)
        End If
    End Sub
    Private Sub ClearOptionSelection()
        rdoDealers.Checked = False
        rdoCompanies.Checked = False
    End Sub
#End Region
#Region "Enable-Diable Fields"
    Private Sub DisableFields()
        TextboxCertItemConfigCode.Enabled = False
    End Sub
    Private Sub EnableDisablePageButton(ByVal value As Boolean)
        ChangeEnabledProperty(btnAdd, value)
        'ChangeEnabledProperty(btnSaveConfig, value)
    End Sub
#End Region


End Class