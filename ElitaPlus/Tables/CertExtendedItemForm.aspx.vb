Imports Assurant.ElitaPlus.DALObjects
Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms

Public Class CertExtendedItemForm
    Inherits ElitaPlusSearchPage

#Region "Private Variables and Properties"
    Private _YesNoDataView As DataView
#End Region

#Region "Constants"
    Public Const URL As String = "CertExtendedItemForm.aspx"
    Private Const DEFAULT_SORT As String = "FIELD_NAME ASC"

    Private Const GRID_COL_ID_IDX As Integer = 0
    Private Const GRID_COL_FIELD_NAME_IDX As Integer = 1
    'Private Const GRID_COL_IN_ENROLLMENT_IDX As Integer = 2
    Private Const GRID_COL_DEFAULT_VALUE_IDX As Integer = 2
    Private Const GRID_COL_ALLOW_UPDATE_IDX As Integer = 3
    Private Const GRID_COL_ALLOW_DISPLAY_IDX As Integer = 4

    Private Const MSG_RECORD_DELETED_OK As String = "MSG_RECORD_DELETED_OK"
    Private Const MSG_RECORD_SAVED_OK As String = "MSG_RECORD_SAVED_OK"
    Private Const MSG_RECORD_NOT_SAVED As String = "MSG_RECORD_NOT_SAVED"

    Private Const FIELD_NAME_LABEL_NAME As String = "FieldNameLabel"
    Private Const FIELD_NAME_LABEL_NAME_EDIT As String = "FieldNameLabelEdit"
    'Private Const IN_ENROLLMENT_LABEL_NAME As String = "InEnrollmentLabel"
    Private Const DEFAULT_VALUE_LABEL_NAME As String = "DefaultValueLabel"
    Private Const ALLOW_UPDATE_LABEL_NAME As String = "AllowUpdateLabel"
    Private Const ALLOW_DISPLAY_LABEL_NAME As String = "AllowDisplayLabel"
    Private Const EDIT_BUTTON_NAME As String = "EditButton"
    Private Const DELETE_BUTTON_NAME As String = "DeleteButton"

    Private Const FIELD_NAME_TEXTBOX_NAME As String = "FieldNameTextBox"
    'Private Const IN_ENROLLMENT_DROPDOWN_NAME As String = "InEnrollmentDropDown"
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
        Public MyBO As CertExtendedItemFormBO
        Public DataSet As DataSet
        Public dv As DataView
        Public ActionInProgress As DetailPageCommand
        Public LastErrMsg As String
        Public CertExtConfigID As Guid = Guid.Empty
        Public HasDataChanged As Boolean
        Public IsRowBeingEdited As Boolean = False
        Public searchDV As CertExtendedItemFormBO.CertExtendedItemSearchDV = Nothing
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
    Public Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            ReturnToCallingPage()
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            'Me.HandleErrors(ex, Me.MasterPage.MessageController)
            Me.DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
            Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
            Me.State.LastErrMsg = Me.MasterPage.MessageController.Text
        End Try
    End Sub
    Public Sub btnAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAdd.Click
        Try
            Me.State.IsGridVisible = True
            Me.State.CertExtConfigID = Guid.Empty
            Me.State.IsRowEdit = False
            Me.BeginEdit(Guid.Empty)
            PopulateGrid()
            Me.State.ActionInProgress = DetailPageCommand.New_
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
    Private Sub BeginEdit(ByVal CurrentCertExtConfigID As Guid)
        Try
            Me.State.IsRowBeingEdited = True
            With Me.State
                If Not CurrentCertExtConfigID.Equals(Guid.Empty) Then
                    .MyBO = New CertExtendedItemFormBO(CurrentCertExtConfigID)
                Else
                    .MyBO = New CertExtendedItemFormBO()
                    .CertExtConfigID = Me.State.MyBO.Id
                    If .searchDV Is Nothing Then
                        .searchDV = .MyBO.GetNewDataViewRow(CertExtendedItemFormBO.GetList(Me.State.CodeMask.ToUpper().Trim()), .CertExtConfigID)
                    Else
                        .searchDV = .MyBO.GetNewDataViewRow(.searchDV, .CertExtConfigID)
                    End If
                End If
            End With
            Me.ChangeEnabledProperty(Me.btnAdd, False)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
    Private Sub SetGridViewButtonsVisibility(ByVal gridrow As GridViewRow, ByVal bEditButtonVisible As Boolean, ByVal bDeleteButtonVisible As Boolean, ByVal bSaveButtonVisible As Boolean, ByVal bCancelButtonVisible As Boolean)
        Try
            Dim EditButton As ImageButton = CType(gridrow.Cells(EDIT_COL).FindControl(EDIT_CONTROL_NAME), ImageButton)
            If (Not EditButton Is Nothing) Then
                EditButton.Enabled = bEditButtonVisible
                EditButton.Visible = bEditButtonVisible
            End If

            Dim DeleteButton As ImageButton = CType(gridrow.Cells(EDIT_COL).FindControl(DELETE_CONTROL_NAME), ImageButton)
            If (Not DeleteButton Is Nothing) Then
                DeleteButton.Enabled = bDeleteButtonVisible
                DeleteButton.Visible = bDeleteButtonVisible
            End If

            Dim SaveButton As Button = CType(gridrow.Cells(EDIT_COL).FindControl(SAVE_CONTROL_NAME), Button)
            If (Not SaveButton Is Nothing) Then
                SaveButton.Enabled = bSaveButtonVisible
                SaveButton.Visible = bSaveButtonVisible
            End If

            Dim CancelButton As Button = CType(gridrow.Cells(EDIT_COL).FindControl(CANCEL_CONTROL_NAME), Button)
            If (Not CancelButton Is Nothing) Then
                CancelButton.Enabled = bCancelButtonVisible
                CancelButton.Visible = bCancelButtonVisible
            End If


        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
#End Region
#Region "Grid-Events"
    Private Sub Grid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            GridViewCertItemConfig.PageIndex = NewCurrentPageIndex(GridViewCertItemConfig, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
            Me.State.SelectedPageSize = CType(cboPageSize.SelectedValue, Integer)
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
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
            Dim spaceIndex As Integer = Me.SortDirection.LastIndexOf(" ")

            If spaceIndex > 0 AndAlso Me.SortDirection.Substring(0, spaceIndex).Equals(e.SortExpression) Then
                If Me.SortDirection.EndsWith(" ASC") Then
                    Me.SortDirection = e.SortExpression & " DESC"
                Else
                    Me.SortDirection = e.SortExpression & " ASC"
                End If
            Else
                Me.SortDirection = e.SortExpression & " ASC"
            End If
            Me.State.SortExpression = Me.SortDirection
            Me.State.PageIndex = 0
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Public Sub GridViewCertItemConfig_PageIndexChanged(ByVal sender As Object, ByVal e As GridViewPageEventArgs) Handles GridViewCertItemConfig.PageIndexChanging
        Try
            Me.State.PageIndex = e.NewPageIndex
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub BindBoPropertiesToGridHeaders()
        Me.BindBOPropertyToGridHeader(Me.State.MyBO, "FieldName", Me.GridViewCertItemConfig.Columns(GRID_COL_FIELD_NAME_IDX))
        'Me.BindBOPropertyToGridHeader(Me.State.MyBO, "InEnrollment", Me.GridViewCertItemConfig.Columns(GRID_COL_IN_ENROLLMENT_IDX))
        Me.BindBOPropertyToGridHeader(Me.State.MyBO, "DefaultValue", Me.GridViewCertItemConfig.Columns(GRID_COL_DEFAULT_VALUE_IDX))
        Me.BindBOPropertyToGridHeader(Me.State.MyBO, "AllowUpdate", Me.GridViewCertItemConfig.Columns(GRID_COL_ALLOW_UPDATE_IDX))
        Me.BindBOPropertyToGridHeader(Me.State.MyBO, "AllowDisplay", Me.GridViewCertItemConfig.Columns(GRID_COL_ALLOW_DISPLAY_IDX))
        Me.ClearGridViewHeadersAndLabelsErrorSign()
    End Sub
    Protected Sub PopulateUserConctrols()
        UserControlAvailableSelectedCompanies.ClearLists()
        UserControlAvailableSelectedCompanies.SetAvailableData(Me.State.MyBO.GetAvailableCompanies(), "Description", "COMPANY_ID")
        UserControlAvailableSelectedCompanies.SetSelectedData(Me.State.MyBO.GetSelectedCompanies(Me.State.CodeMask), "Description", "ID")
        UserControlAvailableSelectedCompanies.BackColor = "#d5d6e4"
        UserControlAvailableSelectedCompanies.RemoveSelectedFromAvailable()

        UserControlAvailableSelectedDealers.ClearLists()
        UserControlAvailableSelectedDealers.SetAvailableData(Me.State.MyBO.GetAvailableDealers(), "Description", "ID")
        UserControlAvailableSelectedDealers.SetSelectedData(Me.State.MyBO.GetSelectedDealers(Me.State.CodeMask), "Description", "ID")
        UserControlAvailableSelectedDealers.BackColor = "#d5d6e4"
        UserControlAvailableSelectedDealers.RemoveSelectedFromAvailable()

        If UserControlAvailableSelectedDealers.SelectedList.Count > 0 Then
            Me.rdoDealers.Checked = True
        Else
            Me.rdoCompanies.Checked = True
        End If
    End Sub
    Public Sub GridViewCertItemConfig_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles GridViewCertItemConfig.RowCommand
        Try
            Dim nIndex As Integer

            Select Case e.CommandName
                Case ElitaPlusSearchPage.CANCEL_COMMAND_NAME
                    Me.GridViewCertItemConfig.EditIndex = NO_ITEM_SELECTED_INDEX
                    Me.EndEdit(ElitaPlusPage.DetailPageCommand.Cancel)
                    Me.State.IsRowEdit = False
                Case ElitaPlusSearchPage.DELETE_COMMAND_NAME
                    nIndex = CInt(e.CommandArgument)
                    Me.State.CertExtConfigID = New Guid(GridViewCertItemConfig.Rows(nIndex).Cells(GRID_COL_ID_IDX).Text)
                    Me.BeginEdit(Me.State.CertExtConfigID)
                    Me.EndEdit(ElitaPlusPage.DetailPageCommand.Delete)
                    DisableFields()
                Case ElitaPlusSearchPage.SAVE_COMMAND_NAME
                    nIndex = CInt(e.CommandArgument)
                    Me.PopulateBoFromControls(GridViewCertItemConfig.Rows(nIndex))
                    'Check if fieldName / code is duplicated
                    ValidateConfigRecords()
                    Me.EndEdit(ElitaPlusPage.DetailPageCommand.OK)
                    DisableFields()
                    Me.State.IsRowEdit = False
                Case ElitaPlusSearchPage.EDIT_COMMAND_NAME
                    nIndex = CInt(e.CommandArgument)
                    Me.GridViewCertItemConfig.SelectedIndex = nIndex
                    Me.GridViewCertItemConfig.EditIndex = nIndex
                    Me.State.CertExtConfigID = New Guid(GridViewCertItemConfig.Rows(nIndex).Cells(GRID_COL_ID_IDX).Text)
                    Me.BeginEdit(Me.State.CertExtConfigID)
                    DisableFields()
                    Me.State.IsRowEdit = True
            End Select
            Me.PopulateGrid()
        Catch ex As Oracle.ManagedDataAccess.Client.OracleException
            Select Case ex.Number
                Case 20005
                    Me.MasterPage.MessageController.AddInformation(TranslationBase.TranslateLabelOrMessage("FIELDNAME_CANNOT_BE_ADDED"), True)
                Case 20006
                    Me.MasterPage.MessageController.AddInformation(TranslationBase.TranslateLabelOrMessage("FIELDNAME_CANNOT_BE_MODIFIED"), True)
                Case 20007
                    Me.MasterPage.MessageController.AddInformation(TranslationBase.TranslateLabelOrMessage("FIELDNAME_CANNOT_BE_DELETED"), True)
            End Select
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
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
                Dim attribute As CertExtendedItemFormBO = New CertExtendedItemFormBO(dvRow.Row)
                Dim rowState As DataControlRowState = CType(e.Row.RowState, DataControlRowState)

                e.Row.Cells(GRID_COL_ID_IDX).Text = attribute.Id.ToString()

                If (rowState And DataControlRowState.Edit) = DataControlRowState.Edit Then

                    'Dim inEnrollmentDropDown As DropDownList = CType(e.Row.FindControl(IN_ENROLLMENT_DROPDOWN_NAME), DropDownList)
                    Dim defaultValueTextBox As TextBox = CType(e.Row.FindControl(DEFAULT_VALUE_TEXTBOX_NAME), TextBox)
                    Dim fieldNameTextBox As TextBox = CType(e.Row.FindControl(FIELD_NAME_TEXTBOX_NAME), TextBox)
                    Dim allowUpdateDropDown As DropDownList = CType(e.Row.FindControl(ALLOW_UPDATE_DROPDOWN_NAME), DropDownList)
                    Dim allowDisplayDropDown As DropDownList = CType(e.Row.FindControl(ALLOW_DISPLAY_DROPDOWN_NAME), DropDownList)
                    'Dim fieldNameLabelEdit As Label = CType(e.Row.FindControl(FIELD_NAME_LABEL_NAME_EDIT), Label)

                    Dim populateOptions = New PopulateOptions() With
                                            {
                                               .AddBlankItem = False
                                            }


                    'inEnrollmentDropDown.Populate(yesNoList.ToArray(), populateOptions)
                    allowUpdateDropDown.Populate(yesNoList.ToArray(), populateOptions)
                    allowDisplayDropDown.Populate(yesNoList.ToArray(), populateOptions)

                    Dim defaultSelectedCodeId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_Y)

                    'SetSelectedItem(inEnrollmentDropDown, defaultSelectedCodeId)
                    SetSelectedItem(allowUpdateDropDown, defaultSelectedCodeId)
                    SetSelectedItem(allowDisplayDropDown, defaultSelectedCodeId)

                    If Not String.IsNullOrEmpty(attribute.FieldName) Then
                        'fieldNameTextBox.Visible = False
                        'fieldNameLabelEdit.Visible = True
                        'fieldNameLabelEdit.Text = attribute.FieldName
                        fieldNameTextBox.Text = attribute.FieldName
                    End If
                    fieldNameTextBox.MaxLength = FIELD_NAME_TEXTBOX_MAX_LENGTH

                    If Not String.IsNullOrEmpty(attribute.DefaultValue) Then
                        defaultValueTextBox.Text = attribute.DefaultValue
                    End If
                    defaultValueTextBox.MaxLength = DEFAULT_VALUE_TEXTBOX_MAX_LENGTH

                    'If attribute.InEnrollment IsNot Nothing Then
                    '    defaultSelectedCodeId = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, attribute.InEnrollment)
                    '    SetSelectedItem(inEnrollmentDropDown, defaultSelectedCodeId)
                    'End If

                    If attribute.AllowUpdate IsNot Nothing Then
                        defaultSelectedCodeId = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, attribute.AllowUpdate)
                        SetSelectedItem(allowUpdateDropDown, defaultSelectedCodeId)
                    End If

                    If attribute.AllowDisplay IsNot Nothing Then
                        defaultSelectedCodeId = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, attribute.AllowDisplay)
                        SetSelectedItem(allowDisplayDropDown, defaultSelectedCodeId)
                    End If
                ElseIf Not IsEmpty(attribute.Id) Then
                    _YesNoDataView = LookupListNew.GetYesNoLookupList(LookupListNew.GetIdFromCode(LookupListNew.LK_LANGUAGES, languageCode), False)

                    Dim fieldNameLabel As Label = CType(e.Row.FindControl(FIELD_NAME_LABEL_NAME), Label)
                    'Dim inEnrollmentLabel As Label = CType(e.Row.FindControl(IN_ENROLLMENT_LABEL_NAME), Label)
                    Dim defaultValueLabel As Label = CType(e.Row.FindControl(DEFAULT_VALUE_LABEL_NAME), Label)
                    Dim allowUpdateLabel As Label = CType(e.Row.FindControl(ALLOW_UPDATE_LABEL_NAME), Label)
                    Dim allowDisplayLabel As Label = CType(e.Row.FindControl(ALLOW_DISPLAY_LABEL_NAME), Label)

                    fieldNameLabel.Text = attribute.FieldName
                    'inEnrollmentLabel.Text = LookupListNew.GetDescriptionFromCode(_YesNoDataView, attribute.InEnrollment)
                    defaultValueLabel.Text = attribute.DefaultValue
                    allowUpdateLabel.Text = LookupListNew.GetDescriptionFromCode(_YesNoDataView, attribute.AllowUpdate)
                    allowDisplayLabel.Text = LookupListNew.GetDescriptionFromCode(_YesNoDataView, attribute.AllowDisplay)

                    Me.TextboxCertItemConfigCode.Text = attribute.Code
                    Me.TextboxCertItemConfigDesc.Text = attribute.Description
                End If
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Public Sub GridViewCertItemConfig_OnRowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles GridViewCertItemConfig.RowCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
#End Region
    Private Sub SetStateProperties()
        Me.State.CodeMask = TextboxCertItemConfigCode.Text.ToUpper().Trim()
    End Sub

#Region "Page Events"

    Private Sub UpdateBreadCrum()
        Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator & CERTITEMEXTENDEDCONTROL
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Put user code to initialize the page here
        Me.MasterPage.MessageController.Clear_Hide()
        Try
            Me.MasterPage.MessageController.Clear()
            Me.MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(ADMIN)
            Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(CERTITEMEXTENDEDCONTROL)
            Me.UpdateBreadCrum()
            If Not Me.IsPostBack Then
                Me.SetStateProperties()
                Me.SetGridItemStyleColor(GridViewCertItemConfig)
                Me.TranslateGridHeader(GridViewCertItemConfig)
                Me.ShowMissingTranslations(Me.MasterPage.MessageController)
                Me.MenuEnabled = False
                Me.SortDirection = DEFAULT_SORT
                Me.PopulateEmptyGrid()
                If Me.State.MyBO Is Nothing Then
                    Me.State.MyBO = New CertExtendedItemFormBO
                End If
                Me.PopulateUserConctrols()
                rdoDealers.Attributes.Add("onClick", "javascript:changeSelection()")
                rdoCompanies.Attributes.Add("onClick", "javascript:changeSelection()")
                hrefCompany.Attributes.Add("onClick", "javascript:changeSelectionCompany()")
                hrefDealer.Attributes.Add("onClick", "javascript:changeSelectionDealer()")
                Me.TextboxCertItemConfigCode.MaxLength = CODE_TEXTBOX_MAX_LENGTH
                Me.TextboxCertItemConfigDesc.MaxLength = DESCRIPTION_TEXTBOX_MAX_LENGTH
            Else
                BindBoPropertiesToGridHeaders()
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
        Me.ShowMissingTranslations(Me.MasterPage.MessageController)
    End Sub

    Private Sub CertExtendedItemForm_PageCall() Handles Me.PageCall
        If (Me.State.DataSet Is Nothing) Then
            'Me.State.DataSet = CertExtendedItemFormBO.GetList(Me.State.CodeMask)
        End If
    End Sub

#End Region

#Region "Controlling Logic"
    Public Sub PopulateGrid()
        If ((Me.State.searchDV Is Nothing) OrElse (Me.State.HasDataChanged)) Then
            Me.State.searchDV = CertExtendedItemFormBO.GetList(Me.State.CodeMask)
        End If
        Me.State.searchDV.Sort = Me.SortDirection
        Me.GridViewCertItemConfig.AutoGenerateColumns = False
        SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.CertExtConfigID, Me.GridViewCertItemConfig, Me.State.PageIndex, Me.State.IsRowBeingEdited)
        Me.SortAndBindGrid()
        Me.State.HasDataChanged = False
    End Sub
    Private Sub SortAndBindGrid()
        Me.State.PageIndex = Me.GridViewCertItemConfig.PageIndex

        If (Me.State.searchDV Is Nothing OrElse Me.State.searchDV.Count = 0) Then
            Session("recCount") = 0
            Me.State.searchDV.AddNew()
            Me.State.searchDV(0)(0) = Guid.Empty.ToByteArray
            GridViewCertItemConfig.DataSource = Me.State.searchDV
            GridViewCertItemConfig.DataBind()
            GridViewCertItemConfig.Rows(0).Visible = False
            GridViewCertItemConfig.Rows(0).Controls.Clear()
            Me.State.BnoRow = True
        Else
            Session("recCount") = Me.State.searchDV.Count
            Me.State.BnoRow = False
            Me.GridViewCertItemConfig.Enabled = True
            Me.GridViewCertItemConfig.DataSource = Me.State.searchDV
            HighLightSortColumn(GridViewCertItemConfig, Me.SortDirection)
            Me.GridViewCertItemConfig.DataBind()
        End If
        If Not GridViewCertItemConfig.BottomPagerRow.Visible Then GridViewCertItemConfig.BottomPagerRow.Visible = True

        ControlMgr.SetVisibleControl(Me, GridViewCertItemConfig, Me.State.IsGridVisible)

        ControlMgr.SetVisibleControl(Me, trPageSize, Me.GridViewCertItemConfig.Visible)

        If Me.GridViewCertItemConfig.Visible Then
            Me.lblRecordCount.Text = Session("recCount").ToString & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
        End If

    End Sub
    Private Sub EndEdit(ByVal detailPageCommand As DetailPageCommand)
        Try
            With Me.State
                Select Case detailPageCommand
                    Case ElitaPlusPage.DetailPageCommand.OK
                        .MyBO.Save()
                        .MyBO.EndEdit()
                        Me.MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION, True)
                    Case ElitaPlusPage.DetailPageCommand.Delete
                        .MyBO.Delete()
                        .MyBO.Save()
                        Me.MasterPage.MessageController.AddSuccess(Message.DELETE_RECORD_CONFIRMATION, True)
                End Select
                PopulateUserConctrols()
                .MyBO = Nothing
                .CertExtConfigID = Guid.Empty
                .HasDataChanged = True
                .IsRowBeingEdited = False
                .searchDV = Nothing
                Me.ChangeEnabledProperty(Me.btnAdd, True)
            End With
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
    Sub PopulateEmptyGrid()
        Session("recCount") = 0
        GridViewCertItemConfig.EditIndex = -1
        ' Bind Data to Grid
        Me.GridViewCertItemConfig.DataSource = New ArrayList()
        Me.GridViewCertItemConfig.DataBind()
        If Me.GridViewCertItemConfig.Visible Then
            Me.lblRecordCount.Text = Session("recCount").ToString & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
        End If
    End Sub
#End Region
    Private Sub PopulateBoFromControls(ByVal gridViewRow As GridViewRow)
        Try
            With Me.State
                Dim txtFieldName As TextBox = CType(gridViewRow.FindControl(FIELD_NAME_TEXTBOX_NAME), TextBox)
                Me.PopulateBOProperty(.MyBO, "FieldName", txtFieldName.Text.Trim())

                Dim txtDefaultValue As TextBox = CType(gridViewRow.FindControl(DEFAULT_VALUE_TEXTBOX_NAME), TextBox)
                Me.PopulateBOProperty(.MyBO, "DefaultValue", txtDefaultValue.Text.Trim())

                'Dim ddInEnrollment As DropDownList = CType(gridViewRow.FindControl(IN_ENROLLMENT_DROPDOWN_NAME), DropDownList)
                'Me.PopulateBOProperty(.MyBO, "InEnrollment", ddInEnrollment.SelectedItem.Text.Substring(0, 1).Trim())

                Dim ddAllowUpdate As DropDownList = CType(gridViewRow.FindControl(ALLOW_UPDATE_DROPDOWN_NAME), DropDownList)
                Me.PopulateBOProperty(.MyBO, "AllowUpdate", ddAllowUpdate.SelectedItem.Text.Substring(0, 1).Trim())

                Dim ddAllowDisplay As DropDownList = CType(gridViewRow.FindControl(ALLOW_DISPLAY_DROPDOWN_NAME), DropDownList)
                Me.PopulateBOProperty(.MyBO, "AllowDisplay", ddAllowDisplay.SelectedItem.Text.Substring(0, 1).Trim())

                Me.PopulateBOProperty(.MyBO, "Code", Me.TextboxCertItemConfigCode.Text.ToUpper().Trim())
                Me.PopulateBOProperty(.MyBO, "Description", Me.TextboxCertItemConfigDesc.Text.Trim())

            End With
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
    Private Function ValidateConfigRecords() As Boolean

        If (String.IsNullOrEmpty(Me.State.MyBO.Code)) Then
            Throw New GUIException(Message.MSG_CERT_EXT_CODE_VALUE_REQUIRED, Assurant.ElitaPlus.Common.ErrorCodes.MSG_CERT_EXT_CODE_VALUE_REQUIRED)
        End If

        'If (String.IsNullOrEmpty(Me.State.MyBO.Description)) Then
        '    Throw New GUIException(Message.MSG_CERT_EXT_DESC_VALUE_REQUIRED, Assurant.ElitaPlus.Common.ErrorCodes.MSG_CERT_EXT_DESC_VALUE_REQUIRED)
        'End If

        If (String.IsNullOrEmpty(Me.State.MyBO.FieldName)) Then
            Throw New GUIException(Message.MSG_CERT_EXT_FIELD_NAME_REQUIRED, Assurant.ElitaPlus.Common.ErrorCodes.MSG_CERT_EXT_FIELD_NAME_REQUIRED)
        End If

        'If Not (String.IsNullOrEmpty(Me.State.MyBO.InEnrollment)) AndAlso (Me.State.MyBO.InEnrollment.Substring(0, 1)) = Codes.YESNO_N AndAlso (String.IsNullOrEmpty(Me.State.MyBO.DefaultValue)) Then
        '    Throw New GUIException(Message.MSG_CERT_EXT_DEFAULT_VALUE_REQUIRED, Assurant.ElitaPlus.Common.ErrorCodes.MSG_CERT_EXT_DEFAULT_VALUE_REQUIRED)
        'End If

        Dim dvCertConfigDV As CertExtendedItemFormBO.CertExtendedItemSearchDV = CertExtendedItemFormBO.GetList(Me.State.MyBO.Code)
        If Not dvCertConfigDV Is Nothing AndAlso dvCertConfigDV.Count > 0 Then
            For Each certItemConfig As DataRow In dvCertConfigDV.Table.Rows
                'For Template Fields
                Dim currentCertConfigID As Guid = New Guid(CType(certItemConfig(CertExtendedItemFormBO.CertExtendedItemSearchDV.COL_CERT_EXT_CONFIG_ID), Byte()))

                If Not currentCertConfigID.Equals(Me.State.MyBO.Id) AndAlso certItemConfig(CertExtendedItemFormBO.CertExtendedItemSearchDV.COL_FIELD_NAME).ToString().ToUpper() = Me.State.MyBO.FieldName.ToUpper() AndAlso Me.State.IsRowEdit = False Then
                    Throw New GUIException(Message.MSG_DUPLICATE_FIELD_NAME, Assurant.ElitaPlus.Common.ErrorCodes.MSG_DUPLICATE_FIELD_NAME)
                End If
            Next
        End If

        Dim dv As DataView
        dv = CertExtendedItemFormBO.GetFieldConfigExist(Me.State.MyBO.Code, Me.State.MyBO.FieldName)

        If Not (dv.Table(0).Table.Rows(0)(0).ToString().ToUpper.Trim().Equals("SUCCESS")) Then
            Throw New GUIException(Message.MSG_DUPLICATE_FIELD_NAME, Assurant.ElitaPlus.Common.ErrorCodes.MSG_DUPLICATE_FIELD_NAME)
        End If

    End Function
    Private Function ValidateDealerCompanyRecords() As Boolean
        'First Validate Dealer or Comapny list already exits against someother code and field combination
        DealerCompanyConfigExist()

        'Clear Dealer or Company List if option choosen vice-versa
        Dim clearList As Boolean
        clearList = ClearDealerCompanyList()
        If clearList Then
            Return True
        End If
        If Me.rdoCompanies.Checked AndAlso (UserControlAvailableSelectedCompanies.SelectedList.Count) < 1 Then
            Throw New GUIException(Message.MSG_EMPTY_SELECTED_COMPANY_REQUIRED, Assurant.ElitaPlus.Common.ErrorCodes.MSG_EMPTY_SELECTED_COMPANY_REQUIRED)
        End If
        If Me.rdoDealers.Checked AndAlso (UserControlAvailableSelectedDealers.SelectedList.Count) < 1 Then
            Throw New GUIException(Message.MSG_EMPTY_SELECTED_DEALER_REQUIRED, Assurant.ElitaPlus.Common.ErrorCodes.MSG_EMPTY_SELECTED_DEALER_REQUIRED)
        End If
        Return True
    End Function
    Private Function ClearDealerCompanyList() As Boolean
        Dim dv As DataView
        'Dettach comapnies from fields if nothing selected from CompanyList
        dv = Me.State.MyBO.GetSelectedCompanies(Me.State.CodeMask)
        If dv.Count > 0 AndAlso (rdoDealers.Checked OrElse (UserControlAvailableSelectedCompanies.SelectedList.Count) <= 0) Then
            Me.State.MyBO.ClearCompanyList(Me.State.CodeMask)
            Return True
        End If
        'Dettach dealers from fields if nothing selected from DealerList
        dv = Me.State.MyBO.GetSelectedDealers(Me.State.CodeMask)
        If dv.Count > 0 AndAlso (rdoCompanies.Checked OrElse (UserControlAvailableSelectedDealers.SelectedList.Count) <= 0) Then
            Me.State.MyBO.ClearDealerList(Me.State.CodeMask)
            Return True
        End If
    End Function
    Private Function DealerCompanyConfigExist() As Boolean
        Dim dv As DataView
        If Me.rdoCompanies.Checked Then
            For Each Str As String In UserControlAvailableSelectedCompanies.SelectedList
                Dim company_id As Guid = New Guid(Str)
                dv = CertExtendedItemFormBO.GetDealerCompanyConfigExist(Me.State.CodeMask, "ELP_COMPANY", company_id)
                If Not (dv.Table(0).Table.Rows(0)(0).ToString().ToUpper.Trim().Equals("SUCCESS")) Then
                    Throw New GUIException(Message.MSG_DUPLICATE_FIELD_NAME, Assurant.ElitaPlus.Common.ErrorCodes.MSG_DUPLICATE_FIELD_NAME)
                End If
            Next
        ElseIf Me.rdoDealers.Checked Then
            For Each Str As String In UserControlAvailableSelectedDealers.SelectedList
                Dim dealer_id As Guid = New Guid(Str)
                dv = CertExtendedItemFormBO.GetDealerCompanyConfigExist(Me.State.CodeMask, "ELP_DEALER", dealer_id)
                If Not (dv.Table(0).Table.Rows(0)(0).ToString().ToUpper.Trim().Equals("SUCCESS")) Then
                    Throw New GUIException(Message.MSG_DUPLICATE_FIELD_NAME, Assurant.ElitaPlus.Common.ErrorCodes.MSG_DUPLICATE_FIELD_NAME)
                End If
            Next
        End If
    End Function
    Protected Sub SaveDealerCompanyList()
        'populate selection of rule, dealer and company to children
        If Me.rdoDealers.Checked AndAlso ((UserControlAvailableSelectedDealers.SelectedList.Count) > 0) Then
            Me.State.MyBO.SaveDealerList(UserControlAvailableSelectedDealers.SelectedList, Me.State.CodeMask)
        ElseIf Me.rdoCompanies.Checked AndAlso ((UserControlAvailableSelectedCompanies.SelectedList.Count) > 0) Then
            Me.State.MyBO.SaveCompanyList(UserControlAvailableSelectedCompanies.SelectedList, Me.State.CodeMask)
        End If
        PopulateUserConctrols()
        If Me.ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If
    End Sub
    Sub DisableFields()
        Me.TextboxCertItemConfigCode.Enabled = False
    End Sub
    Protected Sub btnSaveConfig_Click(sender As Object, e As EventArgs) Handles btnSaveConfig.Click
        Try
            Me.State.MyBO = New CertExtendedItemFormBO()
            If Not String.IsNullOrEmpty(Me.TextboxCertItemConfigCode.Text) AndAlso Me.GridViewCertItemConfig.Rows.Count > 0 AndAlso ValidateDealerCompanyRecords() Then
                Me.SaveDealerCompanyList()
                If Me.State.MyBO.IsDirty Then
                    Me.State.HasDataChanged = False
                    Me.DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)
                    DisableFields()
                Else
                    Me.DisplayMessage(Message.MSG_RECORD_NOT_SAVED, "", MSG_BTN_OK, MSG_TYPE_INFO)
                End If
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub TextboxCertItemConfigCode_TextChanged(sender As Object, e As EventArgs) Handles TextboxCertItemConfigCode.TextChanged
        Try
            If Me.btnAdd.Enabled = True AndAlso Not Me.State.CodeMask.Equals(TextboxCertItemConfigCode.Text.ToUpper().Trim()) Then
                Me.SetStateProperties()
                Me.TextboxCertItemConfigDesc.Text = ""
                Me.State.HasDataChanged = True
                Me.State.IsGridVisible = True
                Me.State.MyBO = New CertExtendedItemFormBO
                PopulateUserConctrols()
                PopulateGrid()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
End Class