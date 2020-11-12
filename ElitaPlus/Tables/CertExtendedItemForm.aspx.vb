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

#Region "Page State"
    Class MyState
        Public SelectedPageSize As Integer = 10
        Public TableName As String = "ELP_CERT_EXTENDED_ITEM"
        Public CodeMask As String
        Public Description As String
        Public SortExpression As String = DEFAULT_SORT
        Public PageIndex As Integer
        Public Count As Integer
        Public MyBo As CertExtendedItem
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
            ReturnToCallingPage()
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            'HandleErrors(ex, MasterPage.MessageController)
            DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
            State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
            State.LastErrMsg = MasterPage.MessageController.Text
        End Try
    End Sub
    Public Sub BtnAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAdd.Click
        Try
            State.IsGridVisible = True
            State.CertExtConfigId = Guid.Empty
            State.IsRowEdit = False
            BeginEdit(Guid.Empty)
            PopulateGrid()
            State.ActionInProgress = DetailPageCommand.New_
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Private Sub BeginEdit(ByVal currentCertExtConfigId As Guid)
        Try
            State.IsRowBeingEdited = True
            With State
                If Not currentCertExtConfigId.Equals(Guid.Empty) Then
                    .MyBo = New CertExtendedItem(currentCertExtConfigId)
                Else
                    .MyBo = New CertExtendedItem()
                    .CertExtConfigId = State.MyBo.Id
                    If .SearchDv Is Nothing Then
                        .SearchDv = .MyBo.GetNewDataViewRow(CertExtendedItem.GetList(State.CodeMask.ToUpper().Trim()), .CertExtConfigId)
                    Else
                        .SearchDv = .MyBo.GetNewDataViewRow(.SearchDv, .CertExtConfigId)
                    End If
                End If
            End With
            ChangeEnabledProperty(btnAdd, False)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
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
#End Region
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
                Case ElitaPlusSearchPage.DELETE_COMMAND_NAME
                    nIndex = CInt(e.CommandArgument)
                    State.CertExtConfigId = New Guid(GridViewCertItemConfig.Rows(nIndex).Cells(GRID_COL_ID_IDX).Text)
                    BeginEdit(State.CertExtConfigId)
                    EndEdit(ElitaPlusPage.DetailPageCommand.Delete)
                    DisableFields()
                Case ElitaPlusSearchPage.SAVE_COMMAND_NAME
                    nIndex = CInt(e.CommandArgument)
                    PopulateBoFromControls(GridViewCertItemConfig.Rows(nIndex))
                    'Check if fieldName / code is duplicated
                    ValidateConfigRecords()
                    EndEdit(ElitaPlusPage.DetailPageCommand.OK)
                    DisableFields()
                    State.IsRowEdit = False
                Case ElitaPlusSearchPage.EDIT_COMMAND_NAME
                    nIndex = CInt(e.CommandArgument)
                    GridViewCertItemConfig.SelectedIndex = nIndex
                    GridViewCertItemConfig.EditIndex = nIndex
                    State.CertExtConfigId = New Guid(GridViewCertItemConfig.Rows(nIndex).Cells(GRID_COL_ID_IDX).Text)
                    BeginEdit(State.CertExtConfigId)
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
                Dim attribute As CertExtendedItem = New CertExtendedItem(dvRow.Row)
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
    Private Sub SetStateProperties()
        State.CodeMask = TextboxCertItemConfigCode.Text.ToUpper().Trim()
    End Sub

#Region "Page Events"

    Private Sub UpdateBreadCrum()
        MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & CERTITEMEXTENDEDCONTROL
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
                SetStateProperties()
                SetGridItemStyleColor(GridViewCertItemConfig)
                TranslateGridHeader(GridViewCertItemConfig)
                ShowMissingTranslations(MasterPage.MessageController)
                MenuEnabled = False
                SortDirection = DEFAULT_SORT
                PopulateEmptyGrid()
                If State.MyBo Is Nothing Then
                    State.MyBo = New CertExtendedItem
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
    Public Sub PopulateGrid()
        If ((State.SearchDv Is Nothing) OrElse (State.HasDataChanged)) Then
            State.SearchDv = CertExtendedItem.GetList(State.CodeMask)
        End If
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
    Private Sub EndEdit(ByVal detailPageCommand As DetailPageCommand)
        Try
            With State
                Select Case detailPageCommand
                    Case ElitaPlusPage.DetailPageCommand.OK
                        .MyBo.Save()
                        .MyBo.EndEdit()
                        MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION, True)
                    Case ElitaPlusPage.DetailPageCommand.Delete
                        .MyBo.Delete()
                        .MyBo.Save()
                        MasterPage.MessageController.AddSuccess(Message.DELETE_RECORD_CONFIRMATION, True)
                End Select
                PopulateUserConctrols()
                State.CodeMask = TextboxCertItemConfigCode.Text.ToUpper().Trim()
                .MyBo = Nothing
                .CertExtConfigId = Guid.Empty
                .HasDataChanged = True
                .IsRowBeingEdited = False
                .SearchDv = Nothing
                ChangeEnabledProperty(btnAdd, True)
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
    Private Function ValidateConfigRecords() As Boolean

        If (String.IsNullOrEmpty(State.MyBo.Code)) Then
            Throw New GUIException(Message.MSG_CERT_EXT_CODE_VALUE_REQUIRED, Assurant.ElitaPlus.Common.ErrorCodes.MSG_CERT_EXT_CODE_VALUE_REQUIRED)
        End If

        If (String.IsNullOrEmpty(State.MyBo.FieldName)) Then
            Throw New GUIException(Message.MSG_CERT_EXT_FIELD_NAME_REQUIRED, Assurant.ElitaPlus.Common.ErrorCodes.MSG_CERT_EXT_FIELD_NAME_REQUIRED)
        End If

        Dim dvCertConfigDv As CertExtendedItem.CertExtendedItemSearchDV = CertExtendedItem.GetList(State.MyBo.Code)
        If Not dvCertConfigDv Is Nothing AndAlso dvCertConfigDv.Count > 0 Then
            For Each certItemConfig As DataRow In dvCertConfigDv.Table.Rows
                'For Template Fields
                Dim currentCertConfigId As Guid = New Guid(CType(certItemConfig(CertExtendedItem.CertExtendedItemSearchDV.COL_CERT_EXT_CONFIG_ID), Byte()))

                If Not currentCertConfigId.Equals(State.MyBo.Id) AndAlso certItemConfig(CertExtendedItem.CertExtendedItemSearchDV.COL_FIELD_NAME).ToString().ToUpper() = State.MyBo.FieldName.ToUpper() AndAlso State.IsRowEdit = False Then
                    Throw New GUIException(Message.MSG_DUPLICATE_FIELD_NAME, Assurant.ElitaPlus.Common.ErrorCodes.MSG_DUPLICATE_FIELD_NAME)
                End If
            Next
        End If

        Dim dv As DataView
        dv = CertExtendedItem.GetFieldConfigExist(State.MyBo.Code, State.MyBo.FieldName)

        If Not (dv.Table(0).Table.Rows(0)(0).ToString().ToUpper.Trim().Equals("SUCCESS")) Then
            Throw New GUIException(Message.MSG_DUPLICATE_FIELD_NAME_DEALER_COMPANY, Assurant.ElitaPlus.Common.ErrorCodes.MSG_DUPLICATE_FIELD_NAME_DEALER_COMPANY)
        End If

    End Function
    Private Function ValidateDealerCompanyRecords() As Boolean
        'First Validate Dealer or Company list already exits against some other code and field combination
        DealerCompanyConfigExist()

        'Clear Dealer or Company List if option chosen vice-versa
        Dim clearList As Boolean
        clearList = ClearDealerCompanyList()
        If clearList Then
            Return True
        End If
        If rdoCompanies.Checked AndAlso (UserControlAvailableSelectedCompanies.SelectedList.Count) < 1 Then
            Throw New GUIException(Message.MSG_EMPTY_SELECTED_COMPANY_REQUIRED, Assurant.ElitaPlus.Common.ErrorCodes.MSG_EMPTY_SELECTED_COMPANY_REQUIRED)
        End If
        If rdoDealers.Checked AndAlso (UserControlAvailableSelectedDealers.SelectedList.Count) < 1 Then
            Throw New GUIException(Message.MSG_EMPTY_SELECTED_DEALER_REQUIRED, Assurant.ElitaPlus.Common.ErrorCodes.MSG_EMPTY_SELECTED_DEALER_REQUIRED)
        End If
        Return True
    End Function
    Private Function ClearDealerCompanyList() As Boolean
        Dim dv As DataView
        'Detach companies from fields if nothing selected from CompanyList
        dv = State.MyBo.GetSelectedCompanies(State.CodeMask)
        If dv.Count > 0 AndAlso (rdoDealers.Checked OrElse (UserControlAvailableSelectedCompanies.SelectedList.Count) <= 0) Then
            State.MyBo.ClearCompanyList(State.CodeMask)
            Return True
        End If
        'Detach dealers from fields if nothing selected from DealerList
        dv = State.MyBo.GetSelectedDealers(State.CodeMask)
        If dv.Count > 0 AndAlso (rdoCompanies.Checked OrElse (UserControlAvailableSelectedDealers.SelectedList.Count) <= 0) Then
            State.MyBo.ClearDealerList(State.CodeMask)
            Return True
        End If
    End Function
    Private Function DealerCompanyConfigExist() As Boolean
        Dim dv As DataView
        If rdoCompanies.Checked Then
            For Each str As String In UserControlAvailableSelectedCompanies.SelectedList
                Dim companyId As Guid = New Guid(str)
                dv = CertExtendedItem.GetDealerCompanyConfigExist(State.CodeMask, "ELP_COMPANY", companyId)
                If Not (dv.Table(0).Table.Rows(0)(0).ToString().ToUpper.Trim().Equals("SUCCESS")) Then
                    Throw New GUIException(Message.MSG_DUPLICATE_FIELD_NAME_DEALER_COMPANY, Assurant.ElitaPlus.Common.ErrorCodes.MSG_DUPLICATE_FIELD_NAME_DEALER_COMPANY)
                End If
            Next
        ElseIf rdoDealers.Checked Then
            For Each Str As String In UserControlAvailableSelectedDealers.SelectedList
                Dim dealerId As Guid = New Guid(Str)
                dv = CertExtendedItem.GetDealerCompanyConfigExist(State.CodeMask, "ELP_DEALER", dealerId)
                If Not (dv.Table(0).Table.Rows(0)(0).ToString().ToUpper.Trim().Equals("SUCCESS")) Then
                    Throw New GUIException(Message.MSG_DUPLICATE_FIELD_NAME_DEALER_COMPANY, Assurant.ElitaPlus.Common.ErrorCodes.MSG_DUPLICATE_FIELD_NAME_DEALER_COMPANY)
                End If
            Next
        End If
    End Function
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
    Sub DisableFields()
        TextboxCertItemConfigCode.Enabled = False
    End Sub
    Protected Sub BtnSaveConfig_Click(sender As Object, e As EventArgs) Handles btnSaveConfig.Click
        Try
            State.MyBo = New CertExtendedItem()
            If Not String.IsNullOrEmpty(TextboxCertItemConfigCode.Text) AndAlso GridViewCertItemConfig.Rows.Count > 0 AndAlso ValidateDealerCompanyRecords() Then
                SaveDealerCompanyList()
                If State.MyBo.IsDirty Then
                    State.HasDataChanged = False
                    DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)
                    DisableFields()
                Else
                    DisplayMessage(Message.MSG_RECORD_NOT_SAVED, "", MSG_BTN_OK, MSG_TYPE_INFO)
                End If
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub TextBoxCertItemConfigCode_TextChanged(sender As Object, e As EventArgs) Handles TextboxCertItemConfigCode.TextChanged
        Try
            If btnAdd.Enabled = True AndAlso Not State.CodeMask.Equals(TextboxCertItemConfigCode.Text.ToUpper().Trim()) Then
                SetStateProperties()
                TextboxCertItemConfigDesc.Text = ""
                State.HasDataChanged = True
                State.IsGridVisible = True
                State.MyBo = New CertExtendedItem
                PopulateUserConctrols()
                PopulateGrid()
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
End Class