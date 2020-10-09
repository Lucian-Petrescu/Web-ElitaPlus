Imports Assurant.ElitaPlus.DALObjects
Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms

Public Class AttributeForm
    Inherits ElitaPlusSearchPage

#Region "Private Variables and Properties"
    Private _YesNoDataView As DataView
    Private _DataTypeDataView As DataView
    Private _AttributeCodeDataView As DataView
#End Region

#Region "Constants"
    Public Const URL As String = "AttributeForm.aspx"
    Private Const DEFAULT_SORT As String = "UI_PROG_CODE ASC"


    Private Const GRID_COL_UI_PROG_CODE_IDX As Integer = 0
    Private Const GRID_COL_DESCRIPTION_IDX As Integer = 1
    Private Const GRID_COL_DATA_TYPE_IDX As Integer = 2
    Private Const GRID_COL_USE_EFFECTIVE_DATE_IDX As Integer = 3
    Private Const GRID_COL_ALLOW_DUPLICATES_IDX As Integer = 4

    Private Const MSG_RECORD_DELETED_OK As String = "MSG_RECORD_DELETED_OK"
    Private Const MSG_RECORD_SAVED_OK As String = "MSG_RECORD_SAVED_OK"
    Private Const MSG_RECORD_NOT_SAVED As String = "MSG_RECORD_NOT_SAVED"

    Private Const UI_PROG_CODE_LABEL_NAME As String = "UiProgCodeLabel"
    Private Const DESCRIPTION_LABEL_NAME As String = "DescriptionLabel"
    Private Const DATA_TYPE_LABEL_NAME As String = "DataTypeLabel"
    Private Const USE_EFFECTIVE_DATE_LABEL_NAME As String = "UseEffectiveDateLabel"
    Private Const ALLOW_DUPLICATES_LABEL_NAME As String = "AllowDuplicatesLabel"
    Private Const EDIT_BUTTON_NAME As String = "EditButton"
    Private Const DELETE_BUTTON_NAME As String = "DeleteButton"

    Private Const UI_PROG_CODE_DROPDOWN_NAME As String = "UiProgCodeDropDown"
    Private Const DESCRIPTION_DROPDOWN_NAME As String = "DescriptionDropDown"
    Private Const DATA_TYPE_DROPDOWN_NAME As String = "DataTypeDropDown"
    Private Const USE_EFFECTIVE_DATE_DROPDOWN_NAME As String = "UseEffectiveDateDropDown"
    Private Const ALLOW_DUPLICATES_DROPDOWN_NAME As String = "AllowDuplicatesDropDown"
    Private Const CANCEL_LINK_BUTTON_NAME As String = "CancelLinkButton"
    Private Const SAVE_BUTTON_NAME As String = "SaveButton"

    Private Const ADMIN As String = "Admin"
    Private Const ATTRIBUTE As String = "Attribute"
#End Region

#Region "Page State"
    Class MyState
        Public SelectedPageSize As Integer = 10
        Public TableName As String = String.Empty
        Public SortExpression As String = DEFAULT_SORT
        Public PageIndex As Integer
        Public Count As Integer
        Public MyBO As ElitaAttribute
        Public DataSet As DataSet
        Public dv As DataView
        Public ActionInProgress As DetailPageCommand
        Public LastErrMsg As String
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

    Public Sub btnBack_Click(sender As System.Object, e As System.EventArgs) Handles btnBack.Click
        Try
            ReturnToCallingPage()
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
            DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
            State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
            State.LastErrMsg = MasterPage.MessageController.Text
        End Try
    End Sub

    Public Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            AddNew()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Grid-Events"
    Private Sub Grid_PageSizeChanged(source As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            moAttributeGridView.PageIndex = NewCurrentPageIndex(moAttributeGridView, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
            State.SelectedPageSize = CType(cboPageSize.SelectedValue, Integer)
            PopulateGrid(Nothing)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Public Property SortDirection() As String
        Get
            Return ViewState("SortDirection").ToString
        End Get
        Set(value As String)
            ViewState("SortDirection") = value
        End Set
    End Property

    Public Sub moAttributeGridView_Sorting(source As Object, e As GridViewSortEventArgs) Handles moAttributeGridView.Sorting
        Try
            Dim spaceIndex As Integer = SortDirection.LastIndexOf(" ")

            If spaceIndex > 0 AndAlso SortDirection.Substring(0, spaceIndex).Equals(e.SortExpression) Then
                If SortDirection.EndsWith(" ASC") Then
                    SortDirection = e.SortExpression + " DESC"
                Else
                    SortDirection = e.SortExpression + " ASC"
                End If
            Else
                SortDirection = e.SortExpression + " ASC"
            End If
            State.SortExpression = SortDirection
            State.PageIndex = 0
            PopulateGrid(Nothing)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Public Sub moAttributeGridView_OnPageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles moAttributeGridView.PageIndexChanging
        Try
            moAttributeGridView.PageIndex = e.NewPageIndex
            PopulateGrid(Nothing)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub BindBoPropertiesToGridHeaders()
        BindBOPropertyToGridHeader(State.MyBO, "UiProgCode", moAttributeGridView.Columns(GRID_COL_UI_PROG_CODE_IDX))
        BindBOPropertyToGridHeader(State.MyBO, "DataTypeId", moAttributeGridView.Columns(GRID_COL_DATA_TYPE_IDX))
        BindBOPropertyToGridHeader(State.MyBO, "UseEffectiveDate", moAttributeGridView.Columns(GRID_COL_USE_EFFECTIVE_DATE_IDX))
        BindBOPropertyToGridHeader(State.MyBO, "AllowDuplicates", moAttributeGridView.Columns(GRID_COL_ALLOW_DUPLICATES_IDX))
        ClearGridViewHeadersAndLabelsErrSign()
    End Sub

    Private Sub PopulateBOFromForm()
        If (_YesNoDataView Is Nothing) OrElse (_AttributeCodeDataView Is Nothing) OrElse (_DataTypeDataView Is Nothing) Then
            PopulateDataViews()
        End If

        Dim gvRow As GridViewRow = moAttributeGridView.Rows(moAttributeGridView.EditIndex)

        Dim uiProgCodeDropDown As DropDownList = CType(gvRow.FindControl(UI_PROG_CODE_DROPDOWN_NAME), DropDownList)
        Dim dataTypeDropDown As DropDownList = CType(gvRow.FindControl(DATA_TYPE_DROPDOWN_NAME), DropDownList)
        Dim useEffectiveDateDropDown As DropDownList = CType(gvRow.FindControl(USE_EFFECTIVE_DATE_DROPDOWN_NAME), DropDownList)
        Dim allowDuplicatesDropDown As DropDownList = CType(gvRow.FindControl(ALLOW_DUPLICATES_DROPDOWN_NAME), DropDownList)

        PopulateBOProperty(State.MyBO, "DataTypeId", dataTypeDropDown)
        If (uiProgCodeDropDown.SelectedIndex = NO_ITEM_SELECTED_INDEX) Then
            State.MyBO.UiProgCode = Nothing
        Else
            State.MyBO.UiProgCode = LookupListNew.GetCodeFromId(_AttributeCodeDataView, New Guid(uiProgCodeDropDown.SelectedValue))
        End If

        If (useEffectiveDateDropDown.SelectedIndex = NO_ITEM_SELECTED_INDEX) Then
            State.MyBO.UseEffectiveDate = Nothing
        Else
            State.MyBO.UseEffectiveDate = LookupListNew.GetCodeFromId(_YesNoDataView, New Guid(useEffectiveDateDropDown.SelectedValue))
        End If

        If (allowDuplicatesDropDown.SelectedIndex = NO_ITEM_SELECTED_INDEX) Then
            State.MyBO.AllowDuplicates = Nothing
        Else
            State.MyBO.AllowDuplicates = LookupListNew.GetCodeFromId(_YesNoDataView, New Guid(allowDuplicatesDropDown.SelectedValue))
        End If

        If ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If
    End Sub

    Public Sub moAttributeGridView_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles moAttributeGridView.RowCommand
        Try
            Select Case e.CommandName
                Case "CancelRecord"
                    If (Me.State.ActionInProgress = DetailPageCommand.New_) Then
                        State.MyBO.Delete()
                        State.MyBO.Save()
                    Else
                        State.DataSet = Nothing
                    End If
                    moAttributeGridView.EditIndex = NO_ITEM_SELECTED_INDEX
                    State.MyBO = Nothing
                    PopulateGrid(Nothing)
                Case "SaveRecord"
                    PopulateBOFromForm()
                    If (State.MyBO.IsDirty) Then
                        State.MyBO.Save()
                        MasterPage.MessageController.AddSuccess(MSG_RECORD_SAVED_OK, True)
                    Else
                        MasterPage.MessageController.AddWarning(MSG_RECORD_NOT_SAVED, True)
                    End If
                    State.DataSet = Nothing
                    State.MyBO = Nothing
                    PopulateGrid(Nothing)
                Case "EditRecord"
                    State.MyBO = New ElitaAttribute(New Guid(CType(e.CommandArgument, String)), State.DataSet)
                    PopulateGrid(State.MyBO.Id)
                Case "DeleteRecord"
                    State.MyBO = New ElitaAttribute(New Guid(CType(e.CommandArgument, String)), State.DataSet)
                    State.MyBO.Delete()
                    State.MyBO.Save()
                    State.DataSet = Nothing
                    PopulateGrid(Nothing)
            End Select
        Catch ex As Oracle.ManagedDataAccess.Client.OracleException
            Select Case ex.Number
                Case 20005
                    MasterPage.MessageController.AddInformation(TranslationBase.TranslateLabelOrMessage("ATTRIBUTE_CANNOT_BE_ADDED"), True)
                Case 20006
                    MasterPage.MessageController.AddInformation(TranslationBase.TranslateLabelOrMessage("ATTRIBUTE_CANNOT_BE_MODIFIED"), True)
                Case 20007
                    MasterPage.MessageController.AddInformation(TranslationBase.TranslateLabelOrMessage("ATTRIBUTE_CANNOT_BE_DELETED"), True)
            End Select
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Public Sub moAttributeGridView_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles moAttributeGridView.RowDataBound
        Try
            If (_YesNoDataView Is Nothing) OrElse (_AttributeCodeDataView Is Nothing) OrElse (_DataTypeDataView Is Nothing) Then
                PopulateDataViews()
            End If

            Dim rowType As DataControlRowType = CType(e.Row.RowType, DataControlRowType)
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

            If (rowType = DataControlRowType.DataRow) AndAlso (dvRow IsNot Nothing) AndAlso (Not dvRow.Row.IsNull(ElitaAttribute.COL_NAME_ATTRIBUTE_ID)) Then
                Dim attribute As ElitaAttribute = New ElitaAttribute(dvRow.Row)
                Dim rowState As DataControlRowState = CType(e.Row.RowState, DataControlRowState)

                If (rowState And DataControlRowState.Edit) = DataControlRowState.Edit Then

                    Dim uiProgCodeDropDown As DropDownList = CType(e.Row.FindControl(UI_PROG_CODE_DROPDOWN_NAME), DropDownList)
                    Dim descriptionDropDown As DropDownList = CType(e.Row.FindControl(DESCRIPTION_DROPDOWN_NAME), DropDownList)
                    Dim dataTypeDropDown As DropDownList = CType(e.Row.FindControl(DATA_TYPE_DROPDOWN_NAME), DropDownList)
                    Dim useEffectiveDateDropDown As DropDownList = CType(e.Row.FindControl(USE_EFFECTIVE_DATE_DROPDOWN_NAME), DropDownList)
                    Dim allowDuplicatesDropDown As DropDownList = CType(e.Row.FindControl(ALLOW_DUPLICATES_DROPDOWN_NAME), DropDownList)
                    Dim editButton As Button = CType(e.Row.FindControl(EDIT_BUTTON_NAME), Button)
                    Dim cancelLinkButton As LinkButton = CType(e.Row.FindControl(CANCEL_LINK_BUTTON_NAME), LinkButton)

                    Dim populateOptions = New PopulateOptions() With
                                            {
                                               .AddBlankItem = True
                                            }
                    Dim LanguageCode = Thread.CurrentPrincipal.GetLanguageCode()

                    Dim YesNoList As DataElements.ListItem() =
                       CommonConfigManager.Current.ListManager.GetList(listCode:="YESNO",
                                                                       languageCode:=LanguageCode)

                    useEffectiveDateDropDown.Populate(YesNoList.ToArray(), populateOptions)
                    allowDuplicatesDropDown.Populate(YesNoList.ToArray(), populateOptions)

                    Dim AttributeDataTypeList As DataElements.ListItem() =
                       CommonConfigManager.Current.ListManager.GetList(listCode:="ATBDTYP",
                                                                       languageCode:=LanguageCode)

                    dataTypeDropDown.Populate(AttributeDataTypeList.ToArray(), populateOptions)

                    Dim AttributeList As DataElements.ListItem() =
                       CommonConfigManager.Current.ListManager.GetList(listCode:="ATTRIBUTE",
                                                                       languageCode:=LanguageCode)

                    uiProgCodeDropDown.Populate(AttributeList.ToArray(),
                                                New PopulateOptions() With
                                                {
                                                    .AddBlankItem = True,
                                                    .TextFunc = AddressOf .GetCode,
                                                    .SortFunc = AddressOf .GetCode
                                                })

                    descriptionDropDown.Populate(AttributeList.ToArray(), populateOptions)

                    'BindListControlToDataView(uiProgCodeDropDown, _AttributeCodeDataView, "CODE", "ID")
                    'BindListControlToDataView(descriptionDropDown, _AttributeCodeDataView, "DESCRIPTION", "ID")
                    'BindListControlToDataView(dataTypeDropDown, _DataTypeDataView)
                    'BindListControlToDataView(useEffectiveDateDropDown, _YesNoDataView)
                    'BindListControlToDataView(allowDuplicatesDropDown, _YesNoDataView)

                    SetSelectedItemByText(uiProgCodeDropDown, attribute.UiProgCode)
                    SetSelectedItem(descriptionDropDown, GetSelectedItem(uiProgCodeDropDown))
                    SetSelectedItem(dataTypeDropDown, attribute.DataTypeId)
                    SetSelectedItem(useEffectiveDateDropDown, LookupListNew.GetIdFromCode(_YesNoDataView, attribute.UseEffectiveDate))
                    SetSelectedItem(allowDuplicatesDropDown, LookupListNew.GetIdFromCode(_YesNoDataView, attribute.AllowDuplicates))

                    uiProgCodeDropDown.Attributes.Add("onchange", String.Format("ToggleSelection('{0}', '{1}', '{2}', '{3}')", uiProgCodeDropDown.ClientID, descriptionDropDown.ClientID, "D", String.Empty))
                    descriptionDropDown.Attributes.Add("onchange", String.Format("ToggleSelection('{0}', '{1}', '{2}', '{3}')", uiProgCodeDropDown.ClientID, descriptionDropDown.ClientID, "C", String.Empty))
                Else

                    Dim uiProgCodeLabel As Label = CType(e.Row.FindControl(UI_PROG_CODE_LABEL_NAME), Label)
                    Dim descriptionLabel As Label = CType(e.Row.FindControl(DESCRIPTION_LABEL_NAME), Label)
                    Dim dataTypeLabel As Label = CType(e.Row.FindControl(DATA_TYPE_LABEL_NAME), Label)
                    Dim useEffectiveDateLabel As Label = CType(e.Row.FindControl(USE_EFFECTIVE_DATE_LABEL_NAME), Label)
                    Dim allowDuplicatesLabel As Label = CType(e.Row.FindControl(ALLOW_DUPLICATES_LABEL_NAME), Label)
                    Dim editButton As ImageButton = CType(e.Row.FindControl(EDIT_BUTTON_NAME), ImageButton)
                    Dim deleteButton As ImageButton = CType(e.Row.FindControl(DELETE_BUTTON_NAME), ImageButton)

                    'If (Not Me.State.MyBO Is Nothing) Then
                    '    editButton.Visible = False
                    '    deleteButton.Visible = False
                    'End If

                    uiProgCodeLabel.Text = attribute.UiProgCode
                    descriptionLabel.Text = LookupListNew.GetDescriptionFromCode(_AttributeCodeDataView, attribute.UiProgCode)
                    dataTypeLabel.Text = LookupListNew.GetDescriptionFromId(_DataTypeDataView, attribute.DataTypeId)
                    useEffectiveDateLabel.Text = LookupListNew.GetDescriptionFromCode(_YesNoDataView, attribute.UseEffectiveDate)
                    allowDuplicatesLabel.Text = LookupListNew.GetDescriptionFromCode(_YesNoDataView, attribute.AllowDuplicates)

                End If
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Public Sub moAttributeGridView_OnRowCreated(sender As Object, e As GridViewRowEventArgs) Handles moAttributeGridView.RowCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
#End Region

#Region "Page Events"

    Private Sub UpdateBreadCrum()
        MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & ATTRIBUTE
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        'Put user code to initialize the page here
        Try
            MasterPage.MessageController.Clear()
            MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(ADMIN)
            MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(ATTRIBUTE)
            UpdateBreadCrum()
            If Not IsPostBack Then
                SetGridItemStyleColor(moAttributeGridView)
                TranslateGridHeader(moAttributeGridView)
                ShowMissingTranslations(MasterPage.MessageController)
                MenuEnabled = False
                moTableName.Text = State.TableName
                SortDirection = DEFAULT_SORT
                PopulateGrid(Nothing)
            Else
                BindBoPropertiesToGridHeaders()
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
        ShowMissingTranslations(MasterPage.MessageController)
    End Sub

    Private Sub AttributeForm_PageCall(CallFromUrl As String, CallingParameter As Object) Handles Me.PageCall
        State.TableName = CallingParameter.ToString()
        If (State.DataSet Is Nothing) Then
            State.DataSet = ElitaAttribute.GetTableAttributes(State.TableName)
        End If
    End Sub

#End Region

#Region "Controlling Logic"

    Sub PopulateDataViews()
        Dim languageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
        _YesNoDataView = LookupListNew.GetYesNoLookupList(languageId, False)
        _DataTypeDataView = LookupListNew.DropdownLookupList(LookupListNew.LK_ATTRIBUTE_DATA_TYPE, languageId, True)
        _AttributeCodeDataView = LookupListNew.DropdownLookupList(LookupListNew.LK_ATTRIBUTE, languageId, True)
    End Sub

    Sub AddNew()
        ' Create new BO and Assign Default Values
        State.MyBO = New ElitaAttribute(State.DataSet)
        With State.MyBO
            .TableName = State.TableName
        End With

        ' Request Re-Population of Grid
        PopulateGrid(State.MyBO.Id)

        ' Set Action for Action Workflow
        State.ActionInProgress = DetailPageCommand.New_


    End Sub

    Sub PopulateGrid(editItemId As Nullable(Of Guid))
        ' Read Data from Database if not already done
        If (State.DataSet Is Nothing) Then
            State.DataSet = ElitaAttribute.GetTableAttributes(State.TableName)
        End If

        ' Get View based on Sort Data
        Dim dv As DataView = New DataView(State.DataSet.Tables(AttributeDAL.TABLE_NAME))
        dv.Sort = SortDirection

        ' Find Index of Item when being Added or Edited
        If (editItemId.HasValue) Then
            'moAttributeGridView.EditIndex = DataSourceExtensions.GetSelectedRowIndex(dv, Function(drv) New ElitaAttribute(drv.Row).Id = editItemId.Value)
            moAttributeGridView.EditIndex = (DataSourceExtensions.GetSelectedRowIndex(dv, Function(drv) New ElitaAttribute(drv.Row).Id = editItemId.Value)) Mod moAttributeGridView.PageSize 'bug 291185
        Else
            moAttributeGridView.EditIndex = -1
        End If

        ' Set Page Index Correctly


        ' Bind Data to Grid
        State.dv = dv
        moAttributeGridView.DataSource = State.dv
        moAttributeGridView.DataBind()

    End Sub

#End Region

End Class