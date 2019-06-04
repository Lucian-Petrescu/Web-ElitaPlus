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

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
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

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.MasterPage.MessageController.Clear()
        'Put user code to initialize the page here
        Page.RegisterHiddenField("__EVENTTARGET", Me.btnSearch.ClientID)
        Try
            If Not Me.IsPostBack Then
                Me.SetDefaultButton(Me.TextBoxSearchClaimNumber, btnSearch)
                Me.SetDefaultButton(Me.TextBoxSearchVerificationNumber, btnSearch)
                Me.SetDefaultButton(Me.TextBoxSearchAuthNumber, btnSearch)
                Me.SetDefaultButton(Me.TextBoxSearchCustomerName, btnSearch)
                Me.SetDefaultButton(Me.TextBoxSerialNumber, btnSearch)
                Me.SetDefaultButton(Me.TextBoxTaxId, btnSearch)
                Me.SetDefaultButton(Me.TextBoxSearchCellPhone, btnSearch)
                Me.SetDefaultButton(Me.cboSortBy, btnSearch)
                UpdateBreadCrum()

                TranslateGridHeader(Grid)

                ControlMgr.SetVisibleControl(Me, trPageSize, False)
                PopulateSortByDropDown()
                PopulateSearchFieldsFromState()
                If Me.IsReturningFromChild Then
                    ' It is returning from detail
                    Me.PopulateGrid()
                End If

                'Me.SetGridItemStyleColor(Me.Grid)
                cboPageSize.SelectedValue = Me.State.PageSize.ToString()

                SetFocus(Me.TextBoxSearchClaimNumber)
            End If
            Me.DisplayNewProgressBarOnClick(Me.btnSearch, "Loading_Claims")
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
        Me.ShowMissingTranslations(Me.MasterPage.MessageController)
    End Sub


    Private Sub UpdateBreadCrum()
        If (Not Me.State Is Nothing) Then
            If (Not Me.State Is Nothing) Then
                Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator &
                    TranslationBase.TranslateLabelOrMessage("REPAIR_AND_LOGISTICS_SEARCH")
                Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("REPAIR_AND_LOGISTICS_SEARCH")
            End If
        End If
    End Sub

    Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
        Try
            Me.MenuEnabled = True
            Me.IsReturningFromChild = True
            Dim retObj As ClaimForm.ReturnType = CType(Me.ReturnedValues, ClaimForm.ReturnType)
            If Not retObj Is Nothing AndAlso retObj.BoChanged Then
                Me.State.searchDV = Nothing
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
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

            If (Me.State.selectedSortById.Equals(Guid.Empty)) Then
                Me.SetSelectedItem(Me.cboSortBy, defaultSelectedCodeId)
                Me.State.selectedSortById = defaultSelectedCodeId
            Else
                Me.SetSelectedItem(Me.cboSortBy, Me.State.selectedSortById)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Public Sub PutInvisibleSvcColumns(ByVal oGrid As GridView)
        Try
            If ElitaPlusIdentity.Current.ActiveUser.IsServiceCenter Then
                oGrid.Columns(GRID_COL_SERVICE_CENTER_IDX).Visible = False
                oGrid.Columns(GRID_COL_VERIFICATION_NUMBER_IDX).Visible = False
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Public Sub PopulateGrid()
        Dim ServiceCenterIDs As ArrayList = New ArrayList()
        Try
            Dim sortBy As String
            If Not PopulateStateFromSearchFields() Then Exit Sub

            If (Me.State.searchDV Is Nothing) Then
                If (Not (Me.State.selectedSortById.Equals(Guid.Empty))) Then
                    sortBy = LookupListNew.GetCodeFromId(LookupListNew.LK_REPAIR_LOGISTICS_SEARCH_FIELDS, Me.State.selectedSortById)
                End If
                If Not (Me.State.ServiceCenterId = Guid.Empty) Then
                    ServiceCenterIDs.Add(Me.State.ServiceCenterId)
                End If
                Me.State.searchDV = RepairAndLogistics.getListFromArray(Me.State.claimNumber,
                                                                        Me.State.serialNumber,
                                                                      Me.State.customerName,
                                                                      Me.State.taxId,
                                                                      Me.State.verificationNumber,
                                                                      Me.State.cellphoneNumber,
                                                                      ServiceCenterIDs,
                                                                      Me.State.claimAuthorizationNumber,
                                                                      sortBy)
                If (Me.State.SearchClicked) Then
                    Me.ValidSearchResultCountNew(Me.State.searchDV.Count, True)
                    Me.State.SearchClicked = False
                End If
            End If

            Grid.PageSize = State.PageSize
            SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.selectedClaimId, Me.Grid, Me.State.PageIndex)
            Me.Grid.DataSource = Me.State.searchDV
            Me.State.PageIndex = Me.Grid.PageIndex
            PutInvisibleSvcColumns(Me.Grid)

            If (Not Me.State.SortExpression.Equals(String.Empty)) Then
                Me.State.searchDV.Sort = Me.State.SortExpression
            Else
                Me.State.SortExpression = sortBy
            End If

            HighLightSortColumn(Me.Grid, Me.State.SortExpression, Me.IsNewUI)

            Me.Grid.DataBind()

            ControlMgr.SetVisibleControl(Me, trPageSize, Me.Grid.Visible)

            Session("recCount") = Me.State.searchDV.Count


            If Me.Grid.Visible Then
                Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_CLAIMS_AUTHORIZATION_FOUND)
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Public Sub PopulateSearchFieldsFromState()
        Try


            Me.TextBoxSearchClaimNumber.Text = Me.State.claimNumber
            Me.TextBoxSerialNumber.Text = Me.State.serialNumber
            Me.TextBoxSearchCustomerName.Text = Me.State.customerName
            Me.TextBoxTaxId.Text = Me.State.taxId
            Me.TextBoxSearchVerificationNumber.Text = Me.State.verificationNumber
            Me.TextBoxSearchAuthNumber.Text = Me.State.claimAuthorizationNumber
            Me.TextBoxSearchCellPhone.Text = Me.State.cellphoneNumber

            inpServiceCenterId.Value = Me.State.ServiceCenterId.ToString

            inpServiceCenterDesc.Value = LookupListNew.GetDescriptionFromId(LookupListNew.LK_SERVICE_CENTERS, Me.State.ServiceCenterId)
            Me.TextBoxServiceCenter.Text = inpServiceCenterDesc.Value
            Me.SetSelectedItem(Me.cboSortBy, Me.State.selectedSortById)

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Public Function PopulateStateFromSearchFields() As Boolean
        Dim dblAmount As Double

        Try
            Me.State.claimNumber = Me.TextBoxSearchClaimNumber.Text
            Me.State.customerName = Me.TextBoxSearchCustomerName.Text
            Me.State.serialNumber = Me.TextBoxSerialNumber.Text
            Me.State.taxId = Me.TextBoxTaxId.Text
            Me.State.cellphoneNumber = Me.TextBoxSearchCellPhone.Text
            Me.State.verificationNumber = Me.TextBoxSearchVerificationNumber.Text
            Me.State.claimAuthorizationNumber = Me.TextBoxSearchAuthNumber.Text
            Me.State.selectedSortById = Me.GetSelectedItem(Me.cboSortBy)
            If AjaxController.IsAutoCompleteEmpty(TextBoxServiceCenter, inpServiceCenterDesc) = True Then
                inpServiceCenterId.Value = Guid.Empty.ToString
            End If

            Me.State.ServiceCenterId = New Guid(inpServiceCenterId.Value.ToString)


            Return True

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Function

    'This method will change the Page Index and the Selected Index
    Public Function FindDVSelectedRowIndex(ByVal dv As RepairAndLogistics.RepairLogisticsSearchDV) As Integer
        Try
            If Me.State.selectedClaimId.Equals(Guid.Empty) Then
                Return -1
            Else
                'Jump to the Right Page
                Dim i As Integer
                For i = 0 To dv.Count - 1
                    If New Guid(CType(dv(i)(RepairAndLogistics.RepairLogisticsSearchDV.COL_NAME_CLAIM_ID), Byte())).Equals(Me.State.selectedClaimId) Then
                        Return i
                    End If
                Next
            End If
            Return -1
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Function

    Public Sub ClearSearch()
        Try
            Me.TextBoxSearchClaimNumber.Text = String.Empty
            Me.TextBoxSearchCustomerName.Text = String.Empty
            Me.TextBoxSerialNumber.Text = String.Empty
            Me.TextBoxTaxId.Text = String.Empty
            Me.TextBoxSearchCellPhone.Text = String.Empty
            Me.TextBoxServiceCenter.Text = String.Empty
            Me.TextBoxSearchVerificationNumber.Text = String.Empty
            Me.TextBoxSearchAuthNumber.Text = String.Empty

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

#End Region


#Region " Datagrid Related "

    Private Sub Grid_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting
        Try
            If Me.State.SortExpression.StartsWith(e.SortExpression) Then
                If Me.State.SortExpression.EndsWith(" DESC") Then
                    Me.State.SortExpression = e.SortExpression
                Else
                    Me.State.SortExpression &= " DESC"
                End If
            Else
                Me.State.SortExpression = e.SortExpression
            End If
            Me.State.PageIndex = 0
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

    Private Sub Grid_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
        Try
            Grid.PageIndex = e.NewPageIndex
            State.PageIndex = Grid.PageIndex
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    'The Binding LOgic is here
    Private Sub Grid_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
        Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
        Dim btnEditClaimItem As LinkButton
        Dim btnEditAuthorizationItem As LinkButton
        Dim isClaimAuthorization As Boolean = False


        Try
            If e.Row.RowType = DataControlRowType.DataRow OrElse (e.Row.RowType = DataControlRowType.Separator) Then

                isClaimAuthorization = (Not IsDBNull(dvRow(RepairAndLogistics.RepairLogisticsSearchDV.COL_NAME_AUTHORIZATION_ID)))

                If (Not e.Row.Cells(Me.GRID_COL_CLAIM_ID_IDX).FindControl(GRID_COL_CLAIM_NUMBER_CTRL) Is Nothing) Then
                    btnEditClaimItem = CType(e.Row.Cells(Me.GRID_COL_CLAIM_ID_IDX).FindControl(GRID_COL_CLAIM_NUMBER_CTRL), LinkButton)
                    btnEditClaimItem.CommandArgument = e.Row.RowIndex.ToString
                    btnEditClaimItem.CommandName = SELECT_ACTION_CLAIM_COMMAND
                    btnEditClaimItem.Text = dvRow(RepairAndLogistics.RepairLogisticsSearchDV.COL_NAME_CLAIM_NUMBER).ToString
                    btnEditClaimItem.Enabled = (Not isClaimAuthorization)
                End If
                e.Row.Cells(Me.GRID_COL_CLAIM_STATUS_IDX).Text = LookupListNew.GetDescriptionFromCode("CLSTAT", dvRow(RepairAndLogistics.RepairLogisticsSearchDV.COL_NAME_CLAIM_STATUS).ToString)
                If (dvRow(RepairAndLogistics.RepairLogisticsSearchDV.COL_NAME_CLAIM_STATUS).ToString = Codes.CLAIM_STATUS__ACTIVE) Then
                    e.Row.Cells(Me.GRID_COL_CLAIM_STATUS_IDX).CssClass = "StatActive"
                Else
                    e.Row.Cells(Me.GRID_COL_CLAIM_STATUS_IDX).CssClass = "StatInactive"
                End If

                If ((Not e.Row.Cells(Me.GRID_COL_AUTHORIZATION_ID_IDX).FindControl(GRID_COL_AUTHORIZATION_NUMBER_CTRL) Is Nothing) And isClaimAuthorization) Then
                    btnEditAuthorizationItem = CType(e.Row.Cells(Me.GRID_COL_AUTHORIZATION_ID_IDX).FindControl(GRID_COL_AUTHORIZATION_NUMBER_CTRL), LinkButton)
                    btnEditAuthorizationItem.CommandArgument = e.Row.RowIndex.ToString
                    btnEditAuthorizationItem.CommandName = SELECT_ACTION_AUTHORIZATION_COMMAND
                    btnEditAuthorizationItem.Text = dvRow(RepairAndLogistics.RepairLogisticsSearchDV.COL_NAME_AUTHORIZATION_NUMBER).ToString
                End If
                If (isClaimAuthorization) Then
                    e.Row.Cells(Me.GRID_COL_AUTHORIZATION_STATUS_IDX).Text = LookupListNew.GetDescriptionFromId("CLM_AUTH_STAT", New Guid(GetGuidStringFromByteArray(CType(dvRow(RepairAndLogistics.RepairLogisticsSearchDV.COL_NAME_AUTHORIZATION_STATUS_ID), Byte()))))
                    If (LookupListNew.GetCodeFromId("CLM_AUTH_STAT", New Guid(GetGuidStringFromByteArray(CType(dvRow(RepairAndLogistics.RepairLogisticsSearchDV.COL_NAME_AUTHORIZATION_STATUS_ID), Byte())))) = Codes.CLAIM_AUTHORIZATION_STATUS__AUTHORIZED) Then
                        e.Row.Cells(Me.GRID_COL_AUTHORIZATION_STATUS_IDX).CssClass = "StatActive"
                    Else
                        e.Row.Cells(Me.GRID_COL_AUTHORIZATION_STATUS_IDX).CssClass = "StatInactive"
                    End If

                End If
                Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_CUSTOMER_NAME_IDX), dvRow(RepairAndLogistics.RepairLogisticsSearchDV.COL_NAME_CUSTOMER_NAME))
                Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_TAX_ID_IDX), dvRow(RepairAndLogistics.RepairLogisticsSearchDV.COL_NAME_TAX_ID))
                Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_CELL_PHONE_IDX), dvRow(RepairAndLogistics.RepairLogisticsSearchDV.COL_NAME_CELL_PHONE_NUMBER))
                Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_SERIAL_NUMBER_IDX), dvRow(RepairAndLogistics.RepairLogisticsSearchDV.COL_NAME_SERIAL_NUMBER))
                Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_SERVICE_CENTER_IDX), dvRow(RepairAndLogistics.RepairLogisticsSearchDV.COL_NAME_SERVICE_CENTER))
                Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_VERIFICATION_NUMBER_IDX), dvRow(RepairAndLogistics.RepairLogisticsSearchDV.COL_NAME_VERIFICATION_NUMBER))
                Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_HIDDEN_CLAIM_ID_IDX), GetGuidStringFromByteArray(CType(dvRow(RepairAndLogistics.RepairLogisticsSearchDV.COL_NAME_CLAIM_ID), Byte())))
                If (isClaimAuthorization) Then
                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_HIDDEN_AUTHORIZATION_ID_IDX), GetGuidStringFromByteArray(CType(dvRow(RepairAndLogistics.RepairLogisticsSearchDV.COL_NAME_AUTHORIZATION_ID), Byte())))
                End If
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub cboPageSize_SelectedIndexChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            State.PageSize = CType(cboPageSize.SelectedValue, Integer)
            Me.State.PageIndex = NewCurrentPageIndex(Grid, State.searchDV.Count, State.PageSize)
            Me.Grid.PageIndex = Me.State.PageIndex
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Grid.RowCommand
        Dim param As RepairAndLogisticsForm.Parameters = New RepairAndLogisticsForm.Parameters()
        Dim rowIndex As Integer = 0
        Dim claimid As String = String.Empty
        Dim Authorizationid As String = String.Empty
        Try
            If (Not e.CommandArgument.ToString().Equals(String.Empty)) And (e.CommandName = SELECT_ACTION_CLAIM_COMMAND Or e.CommandName = SELECT_ACTION_AUTHORIZATION_COMMAND) Then
                rowIndex = CInt(e.CommandArgument)
                claimid = Grid.Rows(rowIndex).Cells(Me.GRID_COL_HIDDEN_CLAIM_ID_IDX).Text
                Me.State.selectedClaimId = New Guid(claimid)

                If e.CommandName = SELECT_ACTION_CLAIM_COMMAND Then

                    param.ClaimId = Me.State.selectedClaimId
                    param.selectedLvl = RepairAndLogisticsForm.SelectedLevel.Claim

                ElseIf e.CommandName = SELECT_ACTION_AUTHORIZATION_COMMAND Then
                    Authorizationid = Grid.Rows(rowIndex).Cells(Me.GRID_COL_HIDDEN_AUTHORIZATION_ID_IDX).Text
                    Me.State.selectedAuthorizationId = New Guid(Authorizationid)

                    param.AuthorizationId = Me.State.selectedAuthorizationId
                    param.ClaimId = Me.State.selectedClaimId


                    param.selectedLvl = RepairAndLogisticsForm.SelectedLevel.Authorization
                End If
                Me.callPage(RepairAndLogisticsForm.URL, param)

            End If


        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_PageIndexChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles Grid.PageIndexChanged
        Try
            Me.State.PageIndex = Grid.PageIndex
            Me.State.selectedClaimId = Guid.Empty
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region " Button Clicks "

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            'Me.PopulateSearchFieldsFromState()
            Me.State.SearchClicked = True
            Me.State.PageIndex = 0
            Me.State.selectedClaimId = Guid.Empty
            Me.State.searchDV = Nothing
            Me.State.selectedSortById = New Guid(Me.cboSortBy.SelectedValue)
            Me.State.SortExpression = String.Empty
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub



    Private Sub btnClearSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearSearch.Click
        Try
            Me.ClearSearch()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region " Ajax"

    <System.Web.Services.WebMethod()>
    <Script.Services.ScriptMethod()>
    Public Shared Function PopulateServiceCenterDrop(ByVal prefixText As String, ByVal count As Integer) As String()
        Dim dv As DataView = LookupListNew.GetServiceCenterLookupList(ElitaPlusIdentity.Current.ActiveUser.Countries)
        Return AjaxController.BindAutoComplete(prefixText, dv)
    End Function
#End Region
End Class



