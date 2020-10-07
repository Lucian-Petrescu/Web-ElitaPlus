Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.Web.Forms
'Imports Assurant.Elita.Web.Forms
Imports Assurant.ElitaPlus.Security
Imports Microsoft.VisualBasic

Partial Class RepairAndLogisticsListForm
    Inherits ElitaPlusSearchPage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblBlank As System.Web.UI.WebControls.Label
    Protected WithEvents trSortBy As System.Web.UI.HtmlControls.HtmlTableRow

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

    Public Const GRID_COL_CLAIM_NUMBER_CTRL As String = "btnEditClaim"
    Public Const GRID_COL_AUTHORIZATION_NUMBER_CTRL As String = "btnEditAuthorization"
    Public Const SELECT_ACTION_CLAIM_COMMAND As String = "SelectActionClaim"
    Public Const SELECT_ACTION_AUTHORIZATION_COMMAND As String = "SelectActionAuthorization"

    Public Const GRID_COL_CLAIM_ID_IDX As Integer = 0
    Public Const GRID_COL_CLAIM_STATUS_IDX As Integer = 1
    Public Const GRID_COL_AUTHORIZATION_ID_IDX As Integer = 2
    Public Const GRID_COL_AUTHORIZATION_STATUS_IDX As Integer = 3
    Public Const GRID_COL_CUSTOMER_NAME_IDX As Integer = 4
    Public Const GRID_COL_TAX_ID_IDX As Integer = 5
    Public Const GRID_COL_CELL_PHONE_IDX As Integer = 6
    Public Const GRID_COL_SERIAL_NUMBER_IDX As Integer = 7
    Public Const GRID_COL_SERVICE_CENTER_IDX As Integer = 8
    Public Const GRID_COL_VERIFICATION_NUMBER_IDX As Integer = 9
    Public Const GRID_COL_HIDDEN_CLAIM_ID_IDX As Integer = 10
    Public Const GRID_COL_HIDDEN_AUTHORIZATION_ID_IDX As Integer = 11

    Public Const GRID_TOTAL_COLUMNS As Integer = 12


#End Region

#Region "Page State"
    Private IsReturningFromChild As Boolean = False

    Class MyState
        Public PageIndex As Integer = 0
        Public SortExpression As String = String.Empty
        Public selectedClaimId As Guid = Guid.Empty
        Public selectedAuthorizationId As Guid = Guid.Empty
        Public claimNumber As String
        Public serialNumber As String
        Public customerName As String
        Public taxId As String
        Public ServiceCenterId As Guid = Guid.Empty

        Public PageSize As Integer = DEFAULT_NEW_UI_PAGE_SIZE
        Public serviceCenterName As String = String.Empty
        Public selectedSortById As Guid = Guid.Empty
        Public verificationNumber As String
        Public claimAuthorizationNumber As String
        Public cellphoneNumber As String
        Public authorizedAmount As String
        Public searchDV As RepairAndLogistics.RepairLogisticsSearchDV = Nothing
        Public SearchClicked As Boolean
        Public authorizedAmountCulture As String





    End Class

    Public Sub New()
        MyBase.New(New MyState)
    End Sub

    Protected Shadows ReadOnly Property State() As MyState
        Get

            Return CType(MyBase.State, MyState)

        End Get
    End Property

#Region "Ajax State"

    Private Shared ReadOnly Property AjaxState() As MyState
        Get
            Return CType(NavPage.ClientNavigator.PageState, MyState)
        End Get

    End Property

#End Region
#End Region


#Region "Page_Events"

    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        MasterPage.MessageController.Clear()
        'Put user code to initialize the page here
        Page.RegisterHiddenField("__EVENTTARGET", btnSearch.ClientID)
        Try
            If Not IsPostBack Then
                SetDefaultButton(TextBoxSearchClaimNumber, btnSearch)
                SetDefaultButton(TextBoxSearchVerificationNumber, btnSearch)
                SetDefaultButton(TextBoxSearchAuthNumber, btnSearch)
                SetDefaultButton(TextBoxSearchCustomerName, btnSearch)
                SetDefaultButton(TextBoxSerialNumber, btnSearch)
                SetDefaultButton(TextBoxTaxId, btnSearch)
                SetDefaultButton(TextBoxSearchCellPhone, btnSearch)
                SetDefaultButton(cboSortBy, btnSearch)
                UpdateBreadCrum()

                TranslateGridHeader(Grid)

                ControlMgr.SetVisibleControl(Me, trPageSize, False)
                PopulateSortByDropDown()
                PopulateSearchFieldsFromState()
                If IsReturningFromChild Then
                    ' It is returning from detail
                    PopulateGrid()
                End If

                'Me.SetGridItemStyleColor(Me.Grid)
                cboPageSize.SelectedValue = State.PageSize.ToString()

                SetFocus(TextBoxSearchClaimNumber)
            End If
            DisplayNewProgressBarOnClick(btnSearch, "Loading_Claims")
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
        ShowMissingTranslations(MasterPage.MessageController)
    End Sub


    Private Sub UpdateBreadCrum()
        If (State IsNot Nothing) Then
            If (State IsNot Nothing) Then
                MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator &
                    TranslationBase.TranslateLabelOrMessage("REPAIR_AND_LOGISTICS_SEARCH")
                MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("REPAIR_AND_LOGISTICS_SEARCH")
            End If
        End If
    End Sub

    Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles MyBase.PageReturn
        Try
            MenuEnabled = True
            IsReturningFromChild = True
            Dim retObj As ClaimForm.ReturnType = CType(ReturnedValues, ClaimForm.ReturnType)
            If retObj IsNot Nothing AndAlso retObj.BoChanged Then
                State.searchDV = Nothing
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Controlling Logic"



    Sub PopulateSortByDropDown()
        Try

            cboSortBy.Populate(CommonConfigManager.Current.ListManager.GetList("RLSDR", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                                                    {
                                                        .AddBlankItem = False
                                                    })

            Dim defaultSelectedCodeId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_REPAIR_LOGISTICS_SEARCH_FIELDS, Codes.DEFAULT_SORT_FOR_CLAIMS)

            If (State.selectedSortById.Equals(Guid.Empty)) Then
                SetSelectedItem(cboSortBy, defaultSelectedCodeId)
                State.selectedSortById = defaultSelectedCodeId
            Else
                SetSelectedItem(cboSortBy, State.selectedSortById)
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Public Sub PutInvisibleSvcColumns(oGrid As GridView)
        Try
            If ElitaPlusIdentity.Current.ActiveUser.IsServiceCenter Then
                oGrid.Columns(GRID_COL_SERVICE_CENTER_IDX).Visible = False
                oGrid.Columns(GRID_COL_VERIFICATION_NUMBER_IDX).Visible = False
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Public Sub PopulateGrid()
        Dim ServiceCenterIDs As ArrayList = New ArrayList()
        Try
            Dim sortBy As String
            If Not PopulateStateFromSearchFields() Then Exit Sub

            If (State.searchDV Is Nothing) Then
                If (Not (State.selectedSortById.Equals(Guid.Empty))) Then
                    sortBy = LookupListNew.GetCodeFromId(LookupListNew.LK_REPAIR_LOGISTICS_SEARCH_FIELDS, State.selectedSortById)
                End If
                If Not (State.ServiceCenterId = Guid.Empty) Then
                    ServiceCenterIDs.Add(State.ServiceCenterId)
                End If
                State.searchDV = RepairAndLogistics.getListFromArray(State.claimNumber,
                                                                        State.serialNumber,
                                                                      State.customerName,
                                                                      State.taxId,
                                                                      State.verificationNumber,
                                                                      State.cellphoneNumber,
                                                                      ServiceCenterIDs,
                                                                      State.claimAuthorizationNumber,
                                                                      sortBy)
                If (State.SearchClicked) Then
                    ValidSearchResultCountNew(State.searchDV.Count, True)
                    State.SearchClicked = False
                End If
            End If

            Grid.PageSize = State.PageSize
            SetPageAndSelectedIndexFromGuid(State.searchDV, State.selectedClaimId, Grid, State.PageIndex)
            Grid.DataSource = State.searchDV
            State.PageIndex = Grid.PageIndex
            PutInvisibleSvcColumns(Grid)

            If (Not State.SortExpression.Equals(String.Empty)) Then
                State.searchDV.Sort = State.SortExpression
            Else
                State.SortExpression = sortBy
            End If

            HighLightSortColumn(Grid, State.SortExpression, IsNewUI)

            Grid.DataBind()

            ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)

            Session("recCount") = State.searchDV.Count


            If Grid.Visible Then
                lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_CLAIMS_AUTHORIZATION_FOUND)
            End If

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Public Sub PopulateSearchFieldsFromState()
        Try


            TextBoxSearchClaimNumber.Text = State.claimNumber
            TextBoxSerialNumber.Text = State.serialNumber
            TextBoxSearchCustomerName.Text = State.customerName
            TextBoxTaxId.Text = State.taxId
            TextBoxSearchVerificationNumber.Text = State.verificationNumber
            TextBoxSearchAuthNumber.Text = State.claimAuthorizationNumber
            TextBoxSearchCellPhone.Text = State.cellphoneNumber

            inpServiceCenterId.Value = State.ServiceCenterId.ToString

            inpServiceCenterDesc.Value = LookupListNew.GetDescriptionFromId(LookupListNew.LK_SERVICE_CENTERS, State.ServiceCenterId)
            TextBoxServiceCenter.Text = inpServiceCenterDesc.Value
            SetSelectedItem(cboSortBy, State.selectedSortById)

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Public Function PopulateStateFromSearchFields() As Boolean
        Dim dblAmount As Double

        Try
            State.claimNumber = TextBoxSearchClaimNumber.Text
            State.customerName = TextBoxSearchCustomerName.Text
            State.serialNumber = TextBoxSerialNumber.Text
            State.taxId = TextBoxTaxId.Text
            State.cellphoneNumber = TextBoxSearchCellPhone.Text
            State.verificationNumber = TextBoxSearchVerificationNumber.Text
            State.claimAuthorizationNumber = TextBoxSearchAuthNumber.Text
            State.selectedSortById = GetSelectedItem(cboSortBy)
            If AjaxController.IsAutoCompleteEmpty(TextBoxServiceCenter, inpServiceCenterDesc) = True Then
                inpServiceCenterId.Value = Guid.Empty.ToString
            End If

            State.ServiceCenterId = New Guid(inpServiceCenterId.Value.ToString)


            Return True

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Function

    'This method will change the Page Index and the Selected Index
    Public Function FindDVSelectedRowIndex(dv As RepairAndLogistics.RepairLogisticsSearchDV) As Integer
        Try
            If State.selectedClaimId.Equals(Guid.Empty) Then
                Return -1
            Else
                'Jump to the Right Page
                Dim i As Integer
                For i = 0 To dv.Count - 1
                    If New Guid(CType(dv(i)(RepairAndLogistics.RepairLogisticsSearchDV.COL_NAME_CLAIM_ID), Byte())).Equals(State.selectedClaimId) Then
                        Return i
                    End If
                Next
            End If
            Return -1
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Function

    Public Sub ClearSearch()
        Try
            TextBoxSearchClaimNumber.Text = String.Empty
            TextBoxSearchCustomerName.Text = String.Empty
            TextBoxSerialNumber.Text = String.Empty
            TextBoxTaxId.Text = String.Empty
            TextBoxSearchCellPhone.Text = String.Empty
            TextBoxServiceCenter.Text = String.Empty
            TextBoxSearchVerificationNumber.Text = String.Empty
            TextBoxSearchAuthNumber.Text = String.Empty

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#End Region


#Region " Datagrid Related "

    Private Sub Grid_SortCommand(source As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting
        Try
            If State.SortExpression.StartsWith(e.SortExpression) Then
                If State.SortExpression.EndsWith(" DESC") Then
                    State.SortExpression = e.SortExpression
                Else
                    State.SortExpression &= " DESC"
                End If
            Else
                State.SortExpression = e.SortExpression
            End If
            State.PageIndex = 0
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

    Private Sub Grid_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
        Try
            Grid.PageIndex = e.NewPageIndex
            State.PageIndex = Grid.PageIndex
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    'The Binding LOgic is here
    Private Sub Grid_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
        Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
        Dim btnEditClaimItem As LinkButton
        Dim btnEditAuthorizationItem As LinkButton
        Dim isClaimAuthorization As Boolean = False


        Try
            If e.Row.RowType = DataControlRowType.DataRow OrElse (e.Row.RowType = DataControlRowType.Separator) Then

                isClaimAuthorization = (Not IsDBNull(dvRow(RepairAndLogistics.RepairLogisticsSearchDV.COL_NAME_AUTHORIZATION_ID)))

                If (e.Row.Cells(GRID_COL_CLAIM_ID_IDX).FindControl(GRID_COL_CLAIM_NUMBER_CTRL) IsNot Nothing) Then
                    btnEditClaimItem = CType(e.Row.Cells(GRID_COL_CLAIM_ID_IDX).FindControl(GRID_COL_CLAIM_NUMBER_CTRL), LinkButton)
                    btnEditClaimItem.CommandArgument = e.Row.RowIndex.ToString
                    btnEditClaimItem.CommandName = SELECT_ACTION_CLAIM_COMMAND
                    btnEditClaimItem.Text = dvRow(RepairAndLogistics.RepairLogisticsSearchDV.COL_NAME_CLAIM_NUMBER).ToString
                    btnEditClaimItem.Enabled = (Not isClaimAuthorization)
                End If
                e.Row.Cells(GRID_COL_CLAIM_STATUS_IDX).Text = LookupListNew.GetDescriptionFromCode("CLSTAT", dvRow(RepairAndLogistics.RepairLogisticsSearchDV.COL_NAME_CLAIM_STATUS).ToString)
                If (dvRow(RepairAndLogistics.RepairLogisticsSearchDV.COL_NAME_CLAIM_STATUS).ToString = Codes.CLAIM_STATUS__ACTIVE) Then
                    e.Row.Cells(GRID_COL_CLAIM_STATUS_IDX).CssClass = "StatActive"
                Else
                    e.Row.Cells(GRID_COL_CLAIM_STATUS_IDX).CssClass = "StatInactive"
                End If

                If ((e.Row.Cells(GRID_COL_AUTHORIZATION_ID_IDX).FindControl(GRID_COL_AUTHORIZATION_NUMBER_CTRL) IsNot Nothing) AndAlso isClaimAuthorization) Then
                    btnEditAuthorizationItem = CType(e.Row.Cells(GRID_COL_AUTHORIZATION_ID_IDX).FindControl(GRID_COL_AUTHORIZATION_NUMBER_CTRL), LinkButton)
                    btnEditAuthorizationItem.CommandArgument = e.Row.RowIndex.ToString
                    btnEditAuthorizationItem.CommandName = SELECT_ACTION_AUTHORIZATION_COMMAND
                    btnEditAuthorizationItem.Text = dvRow(RepairAndLogistics.RepairLogisticsSearchDV.COL_NAME_AUTHORIZATION_NUMBER).ToString
                End If
                If (isClaimAuthorization) Then
                    e.Row.Cells(GRID_COL_AUTHORIZATION_STATUS_IDX).Text = LookupListNew.GetDescriptionFromId("CLM_AUTH_STAT", New Guid(GetGuidStringFromByteArray(CType(dvRow(RepairAndLogistics.RepairLogisticsSearchDV.COL_NAME_AUTHORIZATION_STATUS_ID), Byte()))))
                    If (LookupListNew.GetCodeFromId("CLM_AUTH_STAT", New Guid(GetGuidStringFromByteArray(CType(dvRow(RepairAndLogistics.RepairLogisticsSearchDV.COL_NAME_AUTHORIZATION_STATUS_ID), Byte())))) = Codes.CLAIM_AUTHORIZATION_STATUS__AUTHORIZED) Then
                        e.Row.Cells(GRID_COL_AUTHORIZATION_STATUS_IDX).CssClass = "StatActive"
                    Else
                        e.Row.Cells(GRID_COL_AUTHORIZATION_STATUS_IDX).CssClass = "StatInactive"
                    End If

                End If
                PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_CUSTOMER_NAME_IDX), dvRow(RepairAndLogistics.RepairLogisticsSearchDV.COL_NAME_CUSTOMER_NAME))
                PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_TAX_ID_IDX), dvRow(RepairAndLogistics.RepairLogisticsSearchDV.COL_NAME_TAX_ID))
                PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_CELL_PHONE_IDX), dvRow(RepairAndLogistics.RepairLogisticsSearchDV.COL_NAME_CELL_PHONE_NUMBER))
                PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_SERIAL_NUMBER_IDX), dvRow(RepairAndLogistics.RepairLogisticsSearchDV.COL_NAME_SERIAL_NUMBER))
                PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_SERVICE_CENTER_IDX), dvRow(RepairAndLogistics.RepairLogisticsSearchDV.COL_NAME_SERVICE_CENTER))
                PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_VERIFICATION_NUMBER_IDX), dvRow(RepairAndLogistics.RepairLogisticsSearchDV.COL_NAME_VERIFICATION_NUMBER))
                PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_HIDDEN_CLAIM_ID_IDX), GetGuidStringFromByteArray(CType(dvRow(RepairAndLogistics.RepairLogisticsSearchDV.COL_NAME_CLAIM_ID), Byte())))
                If (isClaimAuthorization) Then
                    PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_HIDDEN_AUTHORIZATION_ID_IDX), GetGuidStringFromByteArray(CType(dvRow(RepairAndLogistics.RepairLogisticsSearchDV.COL_NAME_AUTHORIZATION_ID), Byte())))
                End If
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub cboPageSize_SelectedIndexChanged(source As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            State.PageSize = CType(cboPageSize.SelectedValue, Integer)
            State.PageIndex = NewCurrentPageIndex(Grid, State.searchDV.Count, State.PageSize)
            Grid.PageIndex = State.PageIndex
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Grid.RowCommand
        Dim param As RepairAndLogisticsForm.Parameters = New RepairAndLogisticsForm.Parameters()
        Dim rowIndex As Integer = 0
        Dim claimid As String = String.Empty
        Dim Authorizationid As String = String.Empty
        Try
            If (Not e.CommandArgument.ToString().Equals(String.Empty)) AndAlso (e.CommandName = SELECT_ACTION_CLAIM_COMMAND OrElse e.CommandName = SELECT_ACTION_AUTHORIZATION_COMMAND) Then
                rowIndex = CInt(e.CommandArgument)
                claimid = Grid.Rows(rowIndex).Cells(GRID_COL_HIDDEN_CLAIM_ID_IDX).Text
                State.selectedClaimId = New Guid(claimid)

                If e.CommandName = SELECT_ACTION_CLAIM_COMMAND Then

                    param.ClaimId = State.selectedClaimId
                    param.selectedLvl = RepairAndLogisticsForm.SelectedLevel.Claim

                ElseIf e.CommandName = SELECT_ACTION_AUTHORIZATION_COMMAND Then
                    Authorizationid = Grid.Rows(rowIndex).Cells(GRID_COL_HIDDEN_AUTHORIZATION_ID_IDX).Text
                    State.selectedAuthorizationId = New Guid(Authorizationid)

                    param.AuthorizationId = State.selectedAuthorizationId
                    param.ClaimId = State.selectedClaimId


                    param.selectedLvl = RepairAndLogisticsForm.SelectedLevel.Authorization
                End If
                callPage(RepairAndLogisticsForm.URL, param)

            End If


        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_PageIndexChanged(source As Object, e As System.EventArgs) Handles Grid.PageIndexChanged
        Try
            State.PageIndex = Grid.PageIndex
            State.selectedClaimId = Guid.Empty
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region " Button Clicks "

    Private Sub btnSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnSearch.Click
        Try
            'Me.PopulateSearchFieldsFromState()
            State.SearchClicked = True
            State.PageIndex = 0
            State.selectedClaimId = Guid.Empty
            State.searchDV = Nothing
            State.selectedSortById = New Guid(cboSortBy.SelectedValue)
            State.SortExpression = String.Empty
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub



    Private Sub btnClearSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnClearSearch.Click
        Try
            ClearSearch()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region " Ajax"

    <System.Web.Services.WebMethod()>
    <Script.Services.ScriptMethod()>
    Public Shared Function PopulateServiceCenterDrop(prefixText As String, count As Integer) As String()
        Dim dv As DataView = LookupListNew.GetServiceCenterLookupList(ElitaPlusIdentity.Current.ActiveUser.Countries)
        Return AjaxController.BindAutoComplete(prefixText, dv)
    End Function
#End Region
End Class



