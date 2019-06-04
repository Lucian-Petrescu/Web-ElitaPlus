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

    Public Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            ReturnToCallingPage()
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
            Me.DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", Me.MSG_BTN_YES_NO_CANCEL, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
            Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
            Me.State.LastErrMsg = Me.MasterPage.MessageController.Text
        End Try
    End Sub

    Public Sub btnAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAdd.Click
        Try
            Me.AddNew()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Grid-Events"
    Private Sub Grid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            moAttributeGridView.PageIndex = NewCurrentPageIndex(moAttributeGridView, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
            Me.State.SelectedPageSize = CType(cboPageSize.SelectedValue, Integer)
            Me.PopulateGrid(Nothing)
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

    Public Sub moAttributeGridView_Sorting(ByVal source As Object, ByVal e As GridViewSortEventArgs) Handles moAttributeGridView.Sorting
        Try
            Dim spaceIndex As Integer = Me.SortDirection.LastIndexOf(" ")

            If spaceIndex > 0 AndAlso Me.SortDirection.Substring(0, spaceIndex).Equals(e.SortExpression) Then
                If Me.SortDirection.EndsWith(" ASC") Then
                    Me.SortDirection = e.SortExpression + " DESC"
                Else
                    Me.SortDirection = e.SortExpression + " ASC"
                End If
            Else
                Me.SortDirection = e.SortExpression + " ASC"
            End If
            Me.State.SortExpression = Me.SortDirection
            Me.State.PageIndex = 0
            Me.PopulateGrid(Nothing)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Public Sub moAttributeGridView_OnPageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs) Handles moAttributeGridView.PageIndexChanging
        Try
            Me.moAttributeGridView.PageIndex = e.NewPageIndex
            Me.PopulateGrid(Nothing)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub BindBoPropertiesToGridHeaders()
        Me.BindBOPropertyToGridHeader(Me.State.MyBO, "UiProgCode", Me.moAttributeGridView.Columns(Me.GRID_COL_UI_PROG_CODE_IDX))
        Me.BindBOPropertyToGridHeader(Me.State.MyBO, "DataTypeId", Me.moAttributeGridView.Columns(Me.GRID_COL_DATA_TYPE_IDX))
        Me.BindBOPropertyToGridHeader(Me.State.MyBO, "UseEffectiveDate", Me.moAttributeGridView.Columns(Me.GRID_COL_USE_EFFECTIVE_DATE_IDX))
        Me.BindBOPropertyToGridHeader(Me.State.MyBO, "AllowDuplicates", Me.moAttributeGridView.Columns(Me.GRID_COL_ALLOW_DUPLICATES_IDX))
        Me.ClearGridViewHeadersAndLabelsErrSign()
    End Sub

    Private Sub PopulateBOFromForm()
        If (_YesNoDataView Is Nothing) OrElse (_AttributeCodeDataView Is Nothing) OrElse (_DataTypeDataView Is Nothing) Then
            PopulateDataViews()
        End If

        Dim gvRow As GridViewRow = moAttributeGridView.Rows(Me.moAttributeGridView.EditIndex)

        Dim uiProgCodeDropDown As DropDownList = CType(gvRow.FindControl(Me.UI_PROG_CODE_DROPDOWN_NAME), DropDownList)
        Dim dataTypeDropDown As DropDownList = CType(gvRow.FindControl(Me.DATA_TYPE_DROPDOWN_NAME), DropDownList)
        Dim useEffectiveDateDropDown As DropDownList = CType(gvRow.FindControl(Me.USE_EFFECTIVE_DATE_DROPDOWN_NAME), DropDownList)
        Dim allowDuplicatesDropDown As DropDownList = CType(gvRow.FindControl(Me.ALLOW_DUPLICATES_DROPDOWN_NAME), DropDownList)

        PopulateBOProperty(Me.State.MyBO, "DataTypeId", dataTypeDropDown)
        If (uiProgCodeDropDown.SelectedIndex = NO_ITEM_SELECTED_INDEX) Then
            Me.State.MyBO.UiProgCode = Nothing
        Else
            Me.State.MyBO.UiProgCode = LookupListNew.GetCodeFromId(Me._AttributeCodeDataView, New Guid(uiProgCodeDropDown.SelectedValue))
        End If

        If (useEffectiveDateDropDown.SelectedIndex = NO_ITEM_SELECTED_INDEX) Then
            Me.State.MyBO.UseEffectiveDate = Nothing
        Else
            Me.State.MyBO.UseEffectiveDate = LookupListNew.GetCodeFromId(Me._YesNoDataView, New Guid(useEffectiveDateDropDown.SelectedValue))
        End If

        If (allowDuplicatesDropDown.SelectedIndex = NO_ITEM_SELECTED_INDEX) Then
            Me.State.MyBO.AllowDuplicates = Nothing
        Else
            Me.State.MyBO.AllowDuplicates = LookupListNew.GetCodeFromId(Me._YesNoDataView, New Guid(allowDuplicatesDropDown.SelectedValue))
        End If

        If Me.ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If
    End Sub

    Public Sub moAttributeGridView_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles moAttributeGridView.RowCommand
        Try
            Select Case e.CommandName
                Case "CancelRecord"
                    If (Me.State.ActionInProgress = DetailPageCommand.New_) Then
                        Me.State.MyBO.Delete()
                        Me.State.MyBO.Save()
                    Else
                        Me.State.DataSet = Nothing
                    End If
                    Me.moAttributeGridView.EditIndex = NO_ITEM_SELECTED_INDEX
                    Me.State.MyBO = Nothing
                    Me.PopulateGrid(Nothing)
                Case "SaveRecord"
                    PopulateBOFromForm()
                    If (Me.State.MyBO.IsDirty) Then
                        Me.State.MyBO.Save()
                        Me.MasterPage.MessageController.AddSuccess(Me.MSG_RECORD_SAVED_OK, True)
                    Else
                        Me.MasterPage.MessageController.AddWarning(Me.MSG_RECORD_NOT_SAVED, True)
                    End If
                    Me.State.DataSet = Nothing
                    Me.State.MyBO = Nothing
                    Me.PopulateGrid(Nothing)
                Case "EditRecord"
                    Me.State.MyBO = New ElitaAttribute(New Guid(CType(e.CommandArgument, String)), Me.State.DataSet)
                    Me.PopulateGrid(Me.State.MyBO.Id)
                Case "DeleteRecord"
                    Me.State.MyBO = New ElitaAttribute(New Guid(CType(e.CommandArgument, String)), Me.State.DataSet)
                    Me.State.MyBO.Delete()
                    Me.State.MyBO.Save()
                    Me.State.DataSet = Nothing
                    Me.PopulateGrid(Nothing)
            End Select
        Catch ex As Oracle.ManagedDataAccess.Client.OracleException
            Select Case ex.Number
                Case 20005
                    Me.MasterPage.MessageController.AddInformation(TranslationBase.TranslateLabelOrMessage("ATTRIBUTE_CANNOT_BE_ADDED"), True)
                Case 20006
                    Me.MasterPage.MessageController.AddInformation(TranslationBase.TranslateLabelOrMessage("ATTRIBUTE_CANNOT_BE_MODIFIED"), True)
                Case 20007
                    Me.MasterPage.MessageController.AddInformation(TranslationBase.TranslateLabelOrMessage("ATTRIBUTE_CANNOT_BE_DELETED"), True)
            End Select
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Public Sub moAttributeGridView_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles moAttributeGridView.RowDataBound
        Try
            If (_YesNoDataView Is Nothing) OrElse (_AttributeCodeDataView Is Nothing) OrElse (_DataTypeDataView Is Nothing) Then
                PopulateDataViews()
            End If

            Dim rowType As DataControlRowType = CType(e.Row.RowType, DataControlRowType)
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

            If (rowType = DataControlRowType.DataRow) AndAlso (Not dvRow Is Nothing) AndAlso (Not dvRow.Row.IsNull(ElitaAttribute.COL_NAME_ATTRIBUTE_ID)) Then
                Dim attribute As ElitaAttribute = New ElitaAttribute(dvRow.Row)
                Dim rowState As DataControlRowState = CType(e.Row.RowState, DataControlRowState)

                If (rowState And DataControlRowState.Edit) = DataControlRowState.Edit Then

                    Dim uiProgCodeDropDown As DropDownList = CType(e.Row.FindControl(Me.UI_PROG_CODE_DROPDOWN_NAME), DropDownList)
                    Dim descriptionDropDown As DropDownList = CType(e.Row.FindControl(Me.DESCRIPTION_DROPDOWN_NAME), DropDownList)
                    Dim dataTypeDropDown As DropDownList = CType(e.Row.FindControl(Me.DATA_TYPE_DROPDOWN_NAME), DropDownList)
                    Dim useEffectiveDateDropDown As DropDownList = CType(e.Row.FindControl(Me.USE_EFFECTIVE_DATE_DROPDOWN_NAME), DropDownList)
                    Dim allowDuplicatesDropDown As DropDownList = CType(e.Row.FindControl(Me.ALLOW_DUPLICATES_DROPDOWN_NAME), DropDownList)
                    Dim editButton As Button = CType(e.Row.FindControl(Me.EDIT_BUTTON_NAME), Button)
                    Dim cancelLinkButton As LinkButton = CType(e.Row.FindControl(Me.CANCEL_LINK_BUTTON_NAME), LinkButton)

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

                    Dim uiProgCodeLabel As Label = CType(e.Row.FindControl(Me.UI_PROG_CODE_LABEL_NAME), Label)
                    Dim descriptionLabel As Label = CType(e.Row.FindControl(Me.DESCRIPTION_LABEL_NAME), Label)
                    Dim dataTypeLabel As Label = CType(e.Row.FindControl(Me.DATA_TYPE_LABEL_NAME), Label)
                    Dim useEffectiveDateLabel As Label = CType(e.Row.FindControl(Me.USE_EFFECTIVE_DATE_LABEL_NAME), Label)
                    Dim allowDuplicatesLabel As Label = CType(e.Row.FindControl(Me.ALLOW_DUPLICATES_LABEL_NAME), Label)
                    Dim editButton As ImageButton = CType(e.Row.FindControl(Me.EDIT_BUTTON_NAME), ImageButton)
                    Dim deleteButton As ImageButton = CType(e.Row.FindControl(Me.DELETE_BUTTON_NAME), ImageButton)

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
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Public Sub moAttributeGridView_OnRowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles moAttributeGridView.RowCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
#End Region

#Region "Page Events"

    Private Sub UpdateBreadCrum()
        Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator & ATTRIBUTE
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Put user code to initialize the page here
        Try
            Me.MasterPage.MessageController.Clear()
            Me.MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(ADMIN)
            Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(ATTRIBUTE)
            Me.UpdateBreadCrum()
            If Not Me.IsPostBack Then
                Me.SetGridItemStyleColor(moAttributeGridView)
                Me.TranslateGridHeader(moAttributeGridView)
                Me.ShowMissingTranslations(Me.MasterPage.MessageController)
                Me.MenuEnabled = False
                Me.moTableName.Text = Me.State.TableName
                Me.SortDirection = Me.DEFAULT_SORT
                Me.PopulateGrid(Nothing)
            Else
                BindBoPropertiesToGridHeaders()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
        Me.ShowMissingTranslations(Me.MasterPage.MessageController)
    End Sub

    Private Sub AttributeForm_PageCall(ByVal CallFromUrl As String, ByVal CallingParameter As Object) Handles Me.PageCall
        Me.State.TableName = CallingParameter.ToString()
        If (Me.State.DataSet Is Nothing) Then
            Me.State.DataSet = ElitaAttribute.GetTableAttributes(Me.State.TableName)
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
        Me.State.MyBO = New ElitaAttribute(Me.State.DataSet)
        With Me.State.MyBO
            .TableName = Me.State.TableName
        End With

        ' Request Re-Population of Grid
        PopulateGrid(Me.State.MyBO.Id)

        ' Set Action for Action Workflow
        Me.State.ActionInProgress = DetailPageCommand.New_


    End Sub

    Sub PopulateGrid(ByVal editItemId As Nullable(Of Guid))
        ' Read Data from Database if not already done
        If (Me.State.DataSet Is Nothing) Then
            Me.State.DataSet = ElitaAttribute.GetTableAttributes(Me.State.TableName)
        End If

        ' Get View based on Sort Data
        Dim dv As DataView = New DataView(Me.State.DataSet.Tables(AttributeDAL.TABLE_NAME))
        dv.Sort = Me.SortDirection

        ' Find Index of Item when being Added or Edited
        If (editItemId.HasValue) Then
            'moAttributeGridView.EditIndex = DataSourceExtensions.GetSelectedRowIndex(dv, Function(drv) New ElitaAttribute(drv.Row).Id = editItemId.Value)
            moAttributeGridView.EditIndex = (DataSourceExtensions.GetSelectedRowIndex(dv, Function(drv) New ElitaAttribute(drv.Row).Id = editItemId.Value)) Mod moAttributeGridView.PageSize 'bug 291185
        Else
            moAttributeGridView.EditIndex = -1
        End If

        ' Set Page Index Correctly


        ' Bind Data to Grid
        Me.State.dv = dv
        Me.moAttributeGridView.DataSource = Me.State.dv
        Me.moAttributeGridView.DataBind()

    End Sub

#End Region

End Class