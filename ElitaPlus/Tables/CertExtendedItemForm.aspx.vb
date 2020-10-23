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


    Private Const GRID_COL_FIELD_NAME_IDX As Integer = 0
    Private Const GRID_COL_IN_ENROLLMENT_IDX As Integer = 1
    Private Const GRID_COL_DEFAULT_VALUE_IDX As Integer = 2
    Private Const GRID_COL_ALLOW_UPDATE_IDX As Integer = 3

    Private Const MSG_RECORD_DELETED_OK As String = "MSG_RECORD_DELETED_OK"
    Private Const MSG_RECORD_SAVED_OK As String = "MSG_RECORD_SAVED_OK"
    Private Const MSG_RECORD_NOT_SAVED As String = "MSG_RECORD_NOT_SAVED"

    Private Const FIELD_NAME_LABEL_NAME As String = "FieldNameLabel"
    Private Const IN_ENROLLMENT_LABEL_NAME As String = "InEnrollmentLabel"
    Private Const DEFAULT_VALUE_LABEL_NAME As String = "DefaultValueLabel"
    Private Const ALLOW_UPDATE_LABEL_NAME As String = "AllowUpdateLabel"
    Private Const EDIT_BUTTON_NAME As String = "EditButton"
    Private Const DELETE_BUTTON_NAME As String = "DeleteButton"

    Private Const FIELD_NAME_TEXTBOX_NAME As String = "FieldNameTextBox"
    Private Const IN_ENROLLMENT_DROPDOWN_NAME As String = "InEnrollmentDropDown"
    Private Const DEFAULT_VALUE_TEXTBOX_NAME As String = "DefaultValueTextBox"
    Private Const ALLOW_UPDATE_DROPDOWN_NAME As String = "AllowUpdateDropDown"
    Private Const CANCEL_LINK_BUTTON_NAME As String = "CancelLinkButton"
    Private Const SAVE_BUTTON_NAME As String = "SaveButton"

    Private Const ADMIN As String = "Admin"
    Private Const CERTITEMEXTENDEDCONTROL As String = "Certificate Extended Item Control"
#End Region

#Region "Page State"
    Class MyState
        Public SelectedPageSize As Integer = 10
        Public TableName As String = "ELP_CERT_EXTENDED_ITEM"
        Public SortExpression As String = DEFAULT_SORT
        Public PageIndex As Integer
        Public Count As Integer
        Public MyBO As CertExtendedItemFormBO
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
            GridViewCertItemConfig.PageIndex = NewCurrentPageIndex(GridViewCertItemConfig, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
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

    Public Sub GridViewCertItemConfig_Sorting(ByVal source As Object, ByVal e As GridViewSortEventArgs) Handles GridViewCertItemConfig.Sorting
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

    Public Sub GridViewCertItemConfig_OnPageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs) Handles GridViewCertItemConfig.PageIndexChanging
        Try
            Me.GridViewCertItemConfig.PageIndex = e.NewPageIndex
            Me.PopulateGrid(Nothing)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub BindBoPropertiesToGridHeaders()
        Me.BindBOPropertyToGridHeader(Me.State.MyBO, "FieldName", Me.GridViewCertItemConfig.Columns(Me.GRID_COL_FIELD_NAME_IDX))
        Me.BindBOPropertyToGridHeader(Me.State.MyBO, "InEnrollment", Me.GridViewCertItemConfig.Columns(Me.GRID_COL_IN_ENROLLMENT_IDX))
        Me.BindBOPropertyToGridHeader(Me.State.MyBO, "DefaultValue", Me.GridViewCertItemConfig.Columns(Me.GRID_COL_DEFAULT_VALUE_IDX))
        Me.BindBOPropertyToGridHeader(Me.State.MyBO, "AllowUpdate", Me.GridViewCertItemConfig.Columns(Me.GRID_COL_ALLOW_UPDATE_IDX))
        Me.ClearGridViewHeadersAndLabelsErrSign()
    End Sub

    Private Sub PopulateBOFromForm()
        If (_YesNoDataView Is Nothing) Then
            PopulateDataViews()
        End If
        If Me.ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If
    End Sub
    Protected Sub PopulateFormFromBOs()
        With Me.State.MyBO
            PopulateUserConctrols()
        End With
    End Sub
    Protected Sub PopulateUserConctrols()

        ''UserControlAvailableSelectedDealers
        'UserControlAvailableSelectedDealers.SetAvailableData(Me.State.MyBO.GetAvailableDealers(), "Description", "ID")
        'UserControlAvailableSelectedDealers.SetSelectedData(Me.State.MyBO.GetDealerRuleListSelectionView, "Description", "DEALER_ID")
        'UserControlAvailableSelectedDealers.BackColor = "#d5d6e4"
        'UserControlAvailableSelectedDealers.RemoveSelectedFromAvailable()

        ''UserControlAvailableSelectedCompanies
        'Me.State.MyBO.GetAvailableCompanys()
        'PopulateCompaniesDropDown
        'UserControlAvailableSelectedCompanies.SetAvailableData(PopulateSelectedAssignedCompanies, "Description", "COMPANY_ID")
        'UserControlAvailableSelectedCompanies.SetSelectedData(Me.State.MyBO.GetCompanyRuleListSelectionView, "Description", "COMPANY_ID")
        'UserControlAvailableSelectedCompanies.BackColor = "#d5d6e4"
        'UserControlAvailableSelectedCompanies.RemoveSelectedFromAvailable()


    End Sub
    Public Sub GridViewCertItemConfig_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles GridViewCertItemConfig.RowCommand
        Try
            Select Case e.CommandName
                Case "CancelRecord"

                    Me.GridViewCertItemConfig.EditIndex = NO_ITEM_SELECTED_INDEX
                    Me.State.DataSet = Nothing
                    Me.PopulateGrid(Nothing)
                Case "SaveRecord", "DeleteRecord"
                    Me.State.DataSet = Nothing
                    Me.PopulateGrid(Nothing)
                Case "EditRecord"

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
    Public Sub GridViewCertItemConfig_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles GridViewCertItemConfig.RowDataBound
        Try
            If (_YesNoDataView Is Nothing) Then
                PopulateDataViews()
            End If

            Dim rowType As DataControlRowType = CType(e.Row.RowType, DataControlRowType)
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

            If (rowType = DataControlRowType.DataRow) AndAlso (dvRow IsNot Nothing) Then
                Dim attribute As CertExtendedItemFormBO = New CertExtendedItemFormBO(dvRow.Row)
                Dim rowState As DataControlRowState = CType(e.Row.RowState, DataControlRowState)

                If (rowState And DataControlRowState.Edit) = DataControlRowState.Edit Then

                    Dim inEnrollmentDropDown As DropDownList = CType(e.Row.FindControl(Me.IN_ENROLLMENT_DROPDOWN_NAME), DropDownList)
                    Dim defaultValuetextBox As TextBox = CType(e.Row.FindControl(Me.DEFAULT_VALUE_TEXTBOX_NAME), TextBox)
                    Dim allowUpdateDropDown As DropDownList = CType(e.Row.FindControl(Me.ALLOW_UPDATE_DROPDOWN_NAME), DropDownList)

                    Dim populateOptions = New PopulateOptions() With
                                            {
                                               .AddBlankItem = True
                                            }
                    Dim languageCode = Thread.CurrentPrincipal.GetLanguageCode()

                    Dim yesNoList As DataElements.ListItem() =
                       CommonConfigManager.Current.ListManager.GetList(listCode:="YESNO",
                                                                       languageCode:=languageCode)

                    inEnrollmentDropDown.Populate(yesNoList.ToArray(), populateOptions)
                    allowUpdateDropDown.Populate(yesNoList.ToArray(), populateOptions)

                    SetSelectedItem(inEnrollmentDropDown, LookupListNew.GetIdFromCode(_YesNoDataView, "Y"))
                    SetSelectedItem(allowUpdateDropDown, LookupListNew.GetIdFromCode(_YesNoDataView, "Y"))

                    defaultValuetextBox.Enabled = False
                Else

                    Dim fieldNameLabel As Label = CType(e.Row.FindControl(Me.FIELD_NAME_LABEL_NAME), Label)
                    Dim inEnrollmentLabel As Label = CType(e.Row.FindControl(Me.IN_ENROLLMENT_LABEL_NAME), Label)
                    Dim defaultValueLabel As Label = CType(e.Row.FindControl(Me.DEFAULT_VALUE_LABEL_NAME), Label)
                    Dim allowUpdateLabel As Label = CType(e.Row.FindControl(Me.ALLOW_UPDATE_LABEL_NAME), Label)

                    fieldNameLabel.Text = attribute.FieldName
                    inEnrollmentLabel.Text = LookupListNew.GetDescriptionFromCode(_YesNoDataView, attribute.InEnrollment)
                    defaultValueLabel.Text = attribute.DefaultValue
                    If (inEnrollmentLabel.Text.ToUpper() = "YES") Then
                        defaultValueLabel.Enabled = False
                    End If
                    allowUpdateLabel.Text = LookupListNew.GetDescriptionFromCode(_YesNoDataView, attribute.AllowUpdate)

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

#Region "Page Events"

    Private Sub UpdateBreadCrum()
        Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator & CERTITEMEXTENDEDCONTROL
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Put user code to initialize the page here
        Try
            Me.MasterPage.MessageController.Clear()
            Me.MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(ADMIN)
            Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(CERTITEMEXTENDEDCONTROL)
            Me.UpdateBreadCrum()
            If Not Me.IsPostBack Then
                Dim CountryList As DataElements.ListItem() =
                            CommonConfigManager.Current.ListManager.GetList(listCode:=ListCodes.Country)
                Dim UserCountries As DataElements.ListItem() = (From Country In CountryList
                                                                Where ElitaPlusIdentity.Current.ActiveUser.Countries.Contains(Country.ListItemId)
                                                                Select Country).ToArray()

                Me.SetGridItemStyleColor(GridViewCertItemConfig)
                Me.TranslateGridHeader(GridViewCertItemConfig)
                Me.ShowMissingTranslations(Me.MasterPage.MessageController)
                Me.MenuEnabled = False
                Me.SortDirection = Me.DEFAULT_SORT
                Me.PopulateGrid(Nothing)
                If Me.State.MyBO Is Nothing Then
                    Me.State.MyBO = New CertExtendedItemFormBO
                End If
                Me.PopulateBOFromForm()
                Me.PopulateFormFromBOs()
                rdoDealers.Attributes.Add("onClick", "javascript:changeSelection()")
                rdoCompanies.Attributes.Add("onClick", "javascript:changeSelection()")
                hrefCompany.Attributes.Add("onClick", "javascript:changeSelectionCompany()")
                hrefDealer.Attributes.Add("onClick", "javascript:changeSelectionDealer()")
            Else
                BindBoPropertiesToGridHeaders()
            End If
            If Me.GridViewCertItemConfig.Visible Then
                Me.lblRecordCount.Text = "1" & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
        Me.ShowMissingTranslations(Me.MasterPage.MessageController)
    End Sub

    Private Sub CertExtendedItemForm_PageCall(ByVal CallFromUrl As String, ByVal CallingParameter As Object) Handles Me.PageCall
        If (Me.State.DataSet Is Nothing) Then
            Me.State.DataSet = CertExtendedItemFormBO.GetData()
        End If
    End Sub

#End Region

#Region "Controlling Logic"

    Sub PopulateDataViews()
        Dim languageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
        _YesNoDataView = LookupListNew.GetYesNoLookupList(languageId, False)
    End Sub

    Sub AddNew()
        ' Create new BO and Assign Default Values
        Me.State.MyBO = New CertExtendedItemFormBO(Me.State.DataSet)
        'With Me.State.MyBO
        '    .TableName = Me.State.TableName
        'End With

        ' Request Re-Population of Grid
        PopulateGrid(Me.State.MyBO.ID)

        ' Set Action for Action Workflow
        Me.State.ActionInProgress = DetailPageCommand.New_

    End Sub

    Sub PopulateGrid(ByVal editItemId As Nullable(Of Guid))
        ' Read Data from Database if not already done
        If (Me.State.DataSet Is Nothing) Then
            Me.State.DataSet = CertExtendedItemFormBO.GetData()
        End If

        ' Get View based on Sort Data
        'Me.State.DataSet.Tables(AttributeDAL.TABLE_NAME)
        Dim dv As DataView = New DataView(Me.State.DataSet.Tables(0))
        dv.Sort = Me.SortDirection

        ' Find Index of Item when being Added or Edited
        If (editItemId.HasValue) Then
            GridViewCertItemConfig.EditIndex = (DataSourceExtensions.GetSelectedRowIndex(dv, Function(drv) New CertExtendedItemFormBO(drv.Row).Id = editItemId.Value)) Mod GridViewCertItemConfig.PageSize
        Else
            GridViewCertItemConfig.EditIndex = -1
        End If

        ' Set Page Index Correctly

        ' Bind Data to Grid
        Me.State.dv = dv
        Me.GridViewCertItemConfig.DataSource = Me.State.dv ' New ArrayList() ' this is only for test purpose 'Me.State.dv
        Me.GridViewCertItemConfig.DataBind()

    End Sub
#End Region

End Class