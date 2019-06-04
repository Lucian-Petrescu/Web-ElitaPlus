Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Imports Assurant.Elita.CommonConfiguration.DataElements

Public Class InvoiceRateForm
    Inherits ElitaPlusSearchPage


#Region "Constants"
    Public Const URL As String = "InvoiceRateForm.aspx"
    Public Const PAGETITLE As String = "INVOICE_RATE"
    Public Const PAGETAB As String = "FINANCE_AUTOMATION"
    Public Const SUMMARYTITLE As String = "INVOICE_RATE"

    Public Const EDIT_RPTRATE_COMMAND As String = "EditReportingRate"

    Private Const MSG_CONFIRM_PROMPT As String = "MSG_CONFIRM_PROMPT"
    Private Const MSG_RECORD_DELETED_OK As String = "MSG_RECORD_DELETED_OK"
    Private Const MSG_RECORD_SAVED_OK As String = "MSG_RECORD_SAVED_OK"
    Private Const MSG_RECORD_NOT_SAVED As String = "MSG_RECORD_NOT_SAVED"

    Private Const GRID_COL_INV_RATE_ID_IDX As Integer = 0
    Private Const GRID_COL_INS_CODE_IDX As Integer = 1
    Private Const GRID_COL_HAND_TIER_IDX As Integer = 2
    Private Const GRID_COL_REGULATORY_STATE_IDX As Integer = 3
    Private Const GRID_COL_LOSS_TYPE_IDX As Integer = 4
    Private Const GRID_COL_RETAIL_AMT_IDX As Integer = 5
    Private Const GRID_COL_PREMIUM_AMT_IDX As Integer = 6
    Private Const GRID_COL_COMM_AMT_IDX As Integer = 7
    Private Const GRID_COL_ADMIN_AMT_IDX As Integer = 8
    Private Const GRID_COL_ANCIL_AMT_IDX As Integer = 9
    Private Const GRID_COL_OTHER_AMT_IDX As Integer = 10
    Private Const GRID_COL_EFF_DATE_IDX As Integer = 11
    Private Const GRID_COL_EXP_DATE_IDX As Integer = 12
    Private Const GRID_COL_EDIT_IDX As Integer = 13
    Private Const GRID_COL_DELETE_IDX As Integer = 14

    Private Const GRID_CTRL_NAME_LABEL_INV_RATE_ID As String = "lblInvoiceRateID"
    Private Const GRID_CTRL_NAME_LABEL_INS_CODE As String = "lblInsCode"
    Private Const GRID_CTRL_NAME_LABEL_HAND_TIER As String = "lblHandsetTier"
    Private Const GRID_CTRL_NAME_LABEL_REGULATORY_STATE As String = "lblRegulatoryState"
    Private Const GRID_CTRL_NAME_LINKBTN_LOSS_TYPE As String = "btnLossType"
    Private Const GRID_CTRL_NAME_LABEL_RETAIL_AMT As String = "lblRetailAmt"
    Private Const GRID_CTRL_NAME_LABEL_PREM_AMT As String = "lblPremAmt"
    Private Const GRID_CTRL_NAME_LABEL_COMM_AMT As String = "lblCommAmt"
    Private Const GRID_CTRL_NAME_LABEL_ADMIN_AMT As String = "lblAdminAmt"
    Private Const GRID_CTRL_NAME_LABEL_ANCILL_AMT As String = "lblAncillAmt"
    Private Const GRID_CTRL_NAME_LABEL_OTHER_AMT As String = "lblOtherAmt"
    Private Const GRID_CTRL_NAME_LABEL_EFF_DATE As String = "lblEffectiveDate"
    Private Const GRID_CTRL_NAME_LABEL_EXP_DATE As String = "lblExpirationDate"

    Private Const GRID_CTRL_NAME_EDIT_INS_CODE As String = "txtEditInsCode"
    Private Const GRID_CTRL_NAME_EDIT_HANDSET_TIER As String = "txtEditHandsetTier"
    Private Const GRID_CTRL_NAME_EDIT_REGULATORY_STATE_LIST As String = "ddlEditRegulatoryState"
    Private Const GRID_CTRL_NAME_EDIT_LOSS_TYPE_LIST As String = "ddlEditLossType"
    Private Const GRID_CTRL_NAME_EDIT_RETAIL_AMT As String = "txtEditRetailAmt"
    Private Const GRID_CTRL_NAME_EDIT_PREM_AMT As String = "txtEditPremAmt"
    Private Const GRID_CTRL_NAME_EDIT_COMM_AMT As String = "txtEditCommAmt"
    Private Const GRID_CTRL_NAME_EDIT_ADMIN_AMT As String = "txtEditAdminAmt"
    Private Const GRID_CTRL_NAME_EDIT_ANCILL_AMT As String = "txtEditAncillAmt"
    Private Const GRID_CTRL_NAME_EDIT_OTHER_AMT As String = "txtEditOtherAmt"

    Private Const GRID_CTRL_NAME_EDIT_EFF_DATE As String = "txtEffectiveDate"
    Private Const GRID_CTRL_NAME_EDIT_EFF_IMAGE As String = "imgEffectiveDate"
    Private Const GRID_CTRL_NAME_EDIT_EXP_DATE As String = "txtExpirationDate"
    Private Const GRID_CTRL_NAME_EDIT_EXP_IMAGE As String = "imgExpirationDate"

    Private Const EDIT_COMMAND As String = "SelectRecord"
    Private Const DELETE_COMMAND As String = "DeleteRecord"
    Private Const SORT_COMMAND As String = "Sort"

    Private Const NO_ROW_SELECTED_INDEX As Integer = -1
#End Region

#Region "Page State"
    Private IsReturningFromChild As Boolean = False

    Class MyState
        Public PageIndex As Integer = 0
        Public PageSize As Integer = DEFAULT_NEW_UI_PAGE_SIZE
        'Public MyPagedDataSource As New PagedDataSource
        Public searchDV As AfaInvoiceRate.InvRateSearchDV = Nothing
        Public HasDataChanged As Boolean
        Public IsGridAddNew As Boolean = False
        Public IsEditMode As Boolean = False
        Public IsGridVisible As Boolean
        Public IsAfterSave As Boolean
        Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_

        Public selectedAFAProductId As Guid = Guid.Empty
        Public MyProductBO As AfAProduct
        Public InvoiceRateID As Guid
        Public MyInvoiceRateBO As AfaInvoiceRate
        'Public SortExpression As String = ClaimPaymentGroup.PaymentGroupSearchDV.COL_NAME_PAYMENT_GROUP_NUMBER

        'Public IsReadOnly As Boolean = False
        'Public SortExpressionItem As String = ClaimPaymentGroupDetail.PaymentGroupDetailSearchDV.COL_NAME_CLAIM_NUMBER

        Sub New()
        End Sub
    End Class

    Public Sub New()
        MyBase.New(New MyState)
    End Sub

    Protected Shadows ReadOnly Property State() As MyState
        Get
            Return CType(MyBase.State, MyState)
        End Get
    End Property

    Public ReadOnly Property IsGridInEditMode() As Boolean
        Get
            Return Me.Grid.EditIndex > Me.NO_ITEM_SELECTED_INDEX
        End Get
    End Property

    Public Property SortDirection() As String
        Get
            If Not ViewState("SortDirection") Is Nothing Then
                Return ViewState("SortDirection").ToString
            Else
                Return String.Empty
            End If

        End Get
        Set(ByVal value As String)
            ViewState("SortDirection") = value
        End Set
    End Property

#End Region

#Region "Page Parameters"

    Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
        Try
            If Not Me.CallingParameters Is Nothing Then
                Me.State.selectedAFAProductId = CType(Me.CallingParameters, Guid)

                If Not Me.State.selectedAFAProductId.Equals(Guid.Empty) Then
                    Me.State.MyProductBO = New AfAProduct(Me.State.selectedAFAProductId)
                End If
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
#End Region

#Region "Bread Crum"
    Private Sub UpdateBreadCrum()
        Me.MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(PAGETAB)
        Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(SUMMARYTITLE)
        Me.MasterPage.UsePageTabTitleInBreadCrum = False
        Me.MasterPage.BreadCrum = TranslationBase.TranslateLabelOrMessage(PAGETAB) + ElitaBase.Sperator + TranslationBase.TranslateLabelOrMessage(PAGETITLE)
    End Sub
#End Region

#Region "Page Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.MasterPage.MessageController.Clear()
        Try
            If Not Me.IsPostBack Then
                UpdateBreadCrum()

                ControlMgr.SetVisibleControl(Me, moSearchResults, False)
                'SetControlState()
                'Me.AddCalendar_New(Me.imgExpectedPymntDate, Me.moExpectedPaymentDate)
                Me.State.PageIndex = 0
                Me.TranslateGridHeader(Grid)
                Me.TranslateGridControls(Grid)
                PopulateHeader()
                Me.State.searchDV = Nothing
                If Not Me.State.selectedAFAProductId.Equals(Guid.Empty) Then
                    PopulateGrid()
                End If
            Else
                CheckIfComingFromDeleteConfirm()
                BindBoPropertiesToGridHeaders()
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
        'Me.ShowMissingTranslations(Me.MasterPage.MessageController)
    End Sub

#End Region

#Region "Helper functions"
    Protected Sub CheckIfComingFromDeleteConfirm()
        Dim confResponse As String = Me.HiddenDeletePromptResponse.Value
        If Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_YES Then
            If Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete Then
                DoDelete()
            End If
            Select Case Me.State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Delete
            End Select
        ElseIf Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_NO Then
            Select Case Me.State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Delete
            End Select
        End If
        'Clean after consuming the action
        Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
        Me.HiddenDeletePromptResponse.Value = ""
    End Sub

    Private Sub DoDelete()
        'Do the delete here
        Me.State.ActionInProgress = DetailPageCommand.Nothing_

        Dim obj As AfaInvoiceRate = New AfaInvoiceRate(Me.State.InvoiceRateID)

        obj.Delete()

        'Call the Save() method in the Role Business Object here

        obj.Save()

        Me.MasterPage.MessageController.AddSuccess(Me.MSG_RECORD_DELETED_OK, True)

        Me.State.PageIndex = Grid.PageIndex

        'Set the IsAfterSave flag to TRUE so that the Paging logic gets invoked
        Me.State.IsAfterSave = True
        Me.State.searchDV = Nothing
        PopulateGrid()
        Me.State.PageIndex = Grid.PageIndex
        Me.State.IsEditMode = False
        SetControlState()
    End Sub

    Private Sub SetControlState()
        If (Me.State.IsEditMode) Then
            ControlMgr.SetVisibleControl(Me, btnNew, False)
            Me.MenuEnabled = False
            If (Me.cboPageSize.Enabled) Then
                ControlMgr.SetEnableControl(Me, cboPageSize, False)
            End If
        Else
            ControlMgr.SetVisibleControl(Me, btnNew, True)
            Me.MenuEnabled = True
            If Not (Me.cboPageSize.Enabled) Then
                ControlMgr.SetEnableControl(Me, Me.cboPageSize, True)
            End If
        End If
    End Sub

    Protected Sub BindBoPropertiesToGridHeaders()
        Me.BindBOPropertyToGridHeader(Me.State.MyInvoiceRateBO, "InsuranceCode", Me.Grid.Columns(Me.GRID_COL_INS_CODE_IDX))
        Me.BindBOPropertyToGridHeader(Me.State.MyInvoiceRateBO, "Tier", Me.Grid.Columns(Me.GRID_COL_HAND_TIER_IDX))
        Me.BindBOPropertyToGridHeader(Me.State.MyInvoiceRateBO, "RegulatoryState", Me.Grid.Columns(Me.GRID_COL_REGULATORY_STATE_IDX))
        Me.BindBOPropertyToGridHeader(Me.State.MyInvoiceRateBO, "LossType", Me.Grid.Columns(Me.GRID_COL_LOSS_TYPE_IDX))
        Me.BindBOPropertyToGridHeader(Me.State.MyInvoiceRateBO, "RetailAmt", Me.Grid.Columns(Me.GRID_COL_RETAIL_AMT_IDX))
        Me.BindBOPropertyToGridHeader(Me.State.MyInvoiceRateBO, "PremiumAmt", Me.Grid.Columns(Me.GRID_COL_PREMIUM_AMT_IDX))
        Me.BindBOPropertyToGridHeader(Me.State.MyInvoiceRateBO, "CommAmt", Me.Grid.Columns(Me.GRID_COL_COMM_AMT_IDX))
        Me.BindBOPropertyToGridHeader(Me.State.MyInvoiceRateBO, "AdminAmt", Me.Grid.Columns(Me.GRID_COL_ADMIN_AMT_IDX))
        Me.BindBOPropertyToGridHeader(Me.State.MyInvoiceRateBO, "AncillaryAmt", Me.Grid.Columns(Me.GRID_COL_ANCIL_AMT_IDX))
        Me.BindBOPropertyToGridHeader(Me.State.MyInvoiceRateBO, "OtherAmt", Me.Grid.Columns(Me.GRID_COL_OTHER_AMT_IDX))
        Me.BindBOPropertyToGridHeader(Me.State.MyInvoiceRateBO, "Effective", Me.Grid.Columns(Me.GRID_COL_EFF_DATE_IDX))
        Me.BindBOPropertyToGridHeader(Me.State.MyInvoiceRateBO, "Expiration", Me.Grid.Columns(Me.GRID_COL_EXP_DATE_IDX))
        Me.ClearGridViewHeadersAndLabelsErrSign()
    End Sub

    Private Sub ReturnFromEditing()

        Grid.EditIndex = NO_ROW_SELECTED_INDEX

        If (Me.Grid.PageCount = 0) Then
            'if returning to the "1st time in" screen
            ControlMgr.SetVisibleControl(Me, Grid, False)
        Else
            ControlMgr.SetVisibleControl(Me, Grid, True)
        End If

        Me.State.IsEditMode = False
        Me.PopulateGrid()
        Me.State.PageIndex = Grid.PageIndex
        SetControlState()
    End Sub

    Private Sub RemoveNewRowFromSearchDV()
        Dim rowind As Integer = NO_ITEM_SELECTED_INDEX
        With State
            If Not .searchDV Is Nothing Then
                rowind = FindSelectedRowIndexFromGuid(.searchDV, .InvoiceRateID)
            End If
        End With
        If rowind <> NO_ITEM_SELECTED_INDEX Then State.searchDV.Delete(rowind)
    End Sub

    Private Function PopulateBOFromForm() As Boolean
        Dim objtxt As TextBox
        Dim ddlLossType As DropDownList
        Dim ddlRegulatoryState As DropDownList
        With Me.State.MyInvoiceRateBO
            objtxt = CType(Grid.Rows((Me.Grid.EditIndex)).Cells(GRID_COL_INS_CODE_IDX).FindControl(GRID_CTRL_NAME_EDIT_INS_CODE), TextBox)
            objtxt.Text = objtxt.Text.ToUpper.Trim
            PopulateBOProperty(State.MyInvoiceRateBO, "InsuranceCode", objtxt)
            objtxt = CType(Grid.Rows((Me.Grid.EditIndex)).Cells(GRID_COL_HAND_TIER_IDX).FindControl(GRID_CTRL_NAME_EDIT_HANDSET_TIER), TextBox)
            objtxt.Text = objtxt.Text.Trim
            PopulateBOProperty(State.MyInvoiceRateBO, "Tier", objtxt)

            ddlRegulatoryState = CType(Grid.Rows((Me.Grid.EditIndex)).Cells(GRID_COL_REGULATORY_STATE_IDX).FindControl(GRID_CTRL_NAME_EDIT_REGULATORY_STATE_LIST), DropDownList)
            REM Me.State.MyInvoiceRateBO.RegulatoryState = Me.GetSelectedValue(ddlRegulatoryState)

            PopulateBOProperty(State.MyInvoiceRateBO, "RegulatoryState", ddlRegulatoryState, isGuidValue:=False, isStringValue:=True)

            ddlLossType = CType(Grid.Rows((Me.Grid.EditIndex)).Cells(GRID_COL_LOSS_TYPE_IDX).FindControl(GRID_CTRL_NAME_EDIT_LOSS_TYPE_LIST), DropDownList)
            'PopulateBOProperty(State.MyInvoiceRateBO, "LossType", ddlLossType, False)
            Me.State.MyInvoiceRateBO.LossType = Me.GetSelectedValue(ddlLossType)

            objtxt = CType(Grid.Rows((Me.Grid.EditIndex)).Cells(GRID_COL_RETAIL_AMT_IDX).FindControl(GRID_CTRL_NAME_EDIT_RETAIL_AMT), TextBox)
            PopulateBOProperty(State.MyInvoiceRateBO, "RetailAmt", objtxt)
            objtxt = CType(Grid.Rows((Me.Grid.EditIndex)).Cells(GRID_COL_PREMIUM_AMT_IDX).FindControl(GRID_CTRL_NAME_EDIT_PREM_AMT), TextBox)
            PopulateBOProperty(State.MyInvoiceRateBO, "PremiumAmt", objtxt)
            objtxt = CType(Grid.Rows((Me.Grid.EditIndex)).Cells(GRID_COL_COMM_AMT_IDX).FindControl(GRID_CTRL_NAME_EDIT_COMM_AMT), TextBox)
            PopulateBOProperty(State.MyInvoiceRateBO, "CommAmt", objtxt)
            objtxt = CType(Grid.Rows((Me.Grid.EditIndex)).Cells(GRID_COL_ADMIN_AMT_IDX).FindControl(GRID_CTRL_NAME_EDIT_ADMIN_AMT), TextBox)
            PopulateBOProperty(State.MyInvoiceRateBO, "AdminAmt", objtxt)
            objtxt = CType(Grid.Rows((Me.Grid.EditIndex)).Cells(GRID_COL_ANCIL_AMT_IDX).FindControl(GRID_CTRL_NAME_EDIT_ANCILL_AMT), TextBox)
            PopulateBOProperty(State.MyInvoiceRateBO, "AncillaryAmt", objtxt)
            objtxt = CType(Grid.Rows((Me.Grid.EditIndex)).Cells(GRID_COL_OTHER_AMT_IDX).FindControl(GRID_CTRL_NAME_EDIT_OTHER_AMT), TextBox)
            PopulateBOProperty(State.MyInvoiceRateBO, "OtherAmt", objtxt)
            objtxt = CType(Grid.Rows((Me.Grid.EditIndex)).Cells(GRID_COL_EFF_DATE_IDX).FindControl(GRID_CTRL_NAME_EDIT_EFF_DATE), TextBox)
            PopulateBOProperty(State.MyInvoiceRateBO, "Effective", objtxt)
            objtxt = CType(Grid.Rows((Me.Grid.EditIndex)).Cells(GRID_COL_EXP_DATE_IDX).FindControl(GRID_CTRL_NAME_EDIT_EXP_DATE), TextBox)
            PopulateBOProperty(State.MyInvoiceRateBO, "Expiration", objtxt)
        End With
        If Me.ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If
    End Function

#End Region


#Region "Grid related"

    Public Sub PopulateGrid()
        Dim blnNewSearch As Boolean = False
        Dim dv As DataView
        Try
            'Refresh the DataView and Call SetPageAndSelectedIndexFromGuid() to go to the Page 
            'where the most recently saved Record exists in the DataView
            With State
                If (.searchDV Is Nothing) Then
                    .searchDV = AfaInvoiceRate.getList(.selectedAFAProductId)
                    blnNewSearch = True
                End If
            End With

            Me.State.searchDV.Sort = Me.SortDirection
            If (Me.State.IsAfterSave) Then
                Me.State.IsAfterSave = False
                Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.InvoiceRateID, Me.Grid, Me.State.PageIndex)
            ElseIf (Me.State.IsEditMode) Then
                Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.InvoiceRateID, Me.Grid, Me.State.PageIndex, Me.State.IsEditMode)
            Else
                Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Guid.Empty, Me.Grid, Me.State.PageIndex)
            End If

            Me.Grid.AutoGenerateColumns = False
            'Me.Grid.Columns(Me.GRID_COL_CODE_IDX).SortExpression = AfaInvoiceRate.InvoiceSearchDV.COL_CODE
            'Me.Grid.Columns(Me.GRID_COL_DESC_IDX).SortExpression = AfaInvoiceRate.InvoiceSearchDV.COL_DESCRIPTION
            SortAndBindGrid(blnNewSearch)

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

    Private Sub SortAndBindGrid(Optional ByVal blnShowErr As Boolean = True)

        Me.TranslateGridControls(Grid)

        If (Me.State.searchDV.Count = 0) Then
            Me.State.searchDV = Nothing
            Me.State.MyInvoiceRateBO = New AfaInvoiceRate
            Me.State.MyInvoiceRateBO.AddNewRowToSearchDV(Me.State.searchDV, Me.State.MyInvoiceRateBO)
            Me.Grid.DataSource = Me.State.searchDV
            Me.Grid.DataBind()
            Me.Grid.Rows(0).Visible = False
            Me.State.IsGridAddNew = True
            Me.State.IsGridVisible = False
            If blnShowErr Then
                Me.MasterPage.MessageController.AddInformation(ElitaPlus.ElitaPlusWebApp.Message.MSG_NO_RECORDS_FOUND, True)
            End If
        Else
            Me.Grid.Enabled = True
            Me.Grid.PageSize = Me.State.PageSize
            Me.Grid.DataSource = Me.State.searchDV
            Me.State.IsGridVisible = True
            HighLightSortColumn(Grid, Me.SortDirection)
            Me.Grid.DataBind()
        End If

        ControlMgr.SetVisibleControl(Me, Grid, Me.State.IsGridVisible)
        ControlMgr.SetVisibleControl(Me, moSearchResults, Me.State.IsGridVisible)

        Session("recCount") = Me.State.searchDV.Count

        If Me.Grid.Visible Then
            If (Me.State.IsGridAddNew) Then
                Me.lblRecordCount.Text = (Me.State.searchDV.Count - 1) & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            Else
                Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If
        End If
        ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Grid)

    End Sub

    Private Sub Grid_PageIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grid.PageIndexChanged
        Try
            If (Not (Me.State.IsEditMode)) Then
                Me.State.PageIndex = Grid.PageIndex
                Me.State.InvoiceRateID = Guid.Empty
                Me.PopulateGrid()
            End If
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

    Private Sub Grid_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting
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

            Me.State.PageIndex = 0
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub cboPageSize_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            State.PageSize = CType(cboPageSize.SelectedValue, Integer)
            Me.State.PageIndex = NewCurrentPageIndex(Grid, State.searchDV.Count, State.PageSize)
            Me.Grid.PageIndex = Me.State.PageIndex
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
        Try
            Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
            Dim strID As String
            Dim lossTypeDv As DataView
            Dim regulatoryStateDv As DataView
            Dim moLabel As Label
            Dim moTextBox As TextBox
            Dim moImageButton As ImageButton
            Dim btnEditItem As LinkButton
            Dim ddlLossType As DropDownList = CType(e.Row.FindControl(GRID_CTRL_NAME_EDIT_LOSS_TYPE_LIST), DropDownList)
            Dim ddlRegulatoryState As DropDownList = CType(e.Row.FindControl(GRID_CTRL_NAME_EDIT_REGULATORY_STATE_LIST), DropDownList)

            If Not dvRow Is Nothing Then
                strID = GetGuidStringFromByteArray(CType(dvRow(AfaInvoiceRate.InvRateSearchDV.COL_AFA_INVOICE_RATE_ID), Byte()))
                If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                    CType(e.Row.Cells(Me.GRID_COL_INV_RATE_ID_IDX).FindControl(Me.GRID_CTRL_NAME_LABEL_INV_RATE_ID), Label).Text = strID

                    If (Me.State.IsEditMode = True AndAlso Me.State.InvoiceRateID.ToString.Equals(strID)) Then

                        CType(e.Row.Cells(Me.GRID_COL_INS_CODE_IDX).FindControl(Me.GRID_CTRL_NAME_EDIT_INS_CODE), TextBox).Text = dvRow(AfaInvoiceRate.InvRateSearchDV.COL_INSURANCE_CODE).ToString

                        CType(e.Row.Cells(Me.GRID_COL_HAND_TIER_IDX).FindControl(Me.GRID_CTRL_NAME_EDIT_HANDSET_TIER), TextBox).Text = dvRow(AfaInvoiceRate.InvRateSearchDV.COL_TIER).ToString

                        'REGULATORY_STATE
                        Dim oListContext As New Assurant.Elita.CommonConfiguration.ListContext
                        Dim companyID As Guid = ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID
                        Dim companyObj As Company = New Company(companyID)
                        oListContext.CountryId = companyObj.CountryId

                        Dim regulatoryStateList As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.RegionsByCountry, Thread.CurrentPrincipal.GetLanguageCode(), oListContext)
                        ddlRegulatoryState.Populate(regulatoryStateList, New PopulateOptions() With {.AddBlankItem = True, .BlankItemValue = String.Empty, .ValueFunc = AddressOf PopulateOptions.GetCode})
                        If (Me.State.IsGridAddNew Or dvRow(AfaInvoiceRate.InvRateSearchDV.COL_REGULATORY_STATE) Is Nothing) Then
                            ddlRegulatoryState.SelectedIndex = Me.NO_ITEM_SELECTED_INDEX
                        Else
                            Me.SetSelectedItem(ddlRegulatoryState, dvRow(AfaInvoiceRate.InvRateSearchDV.COL_REGULATORY_STATE).ToString)
                        End If

                        'LOSS_TYPE
                        lossTypeDv = ReppolicyClaimCount.GetCoverageTypeListByDealer(Me.State.MyProductBO.DealerId)
                        Me.BindCodeToListControl(ddlLossType, lossTypeDv, , , True)

                        If (Me.State.IsGridAddNew) Then
                            ddlLossType.SelectedIndex = Me.NO_ITEM_SELECTED_INDEX
                        Else
                            Me.SetSelectedItem(ddlLossType, dvRow(AfaInvoiceRate.InvRateSearchDV.COL_LOSS_TYPE).ToString)
                        End If

                        CType(e.Row.Cells(Me.GRID_COL_RETAIL_AMT_IDX).FindControl(Me.GRID_CTRL_NAME_EDIT_RETAIL_AMT), TextBox).Text = dvRow(AfaInvoiceRate.InvRateSearchDV.COL_RETAIL_AMT).ToString
                        CType(e.Row.Cells(Me.GRID_COL_PREMIUM_AMT_IDX).FindControl(Me.GRID_CTRL_NAME_EDIT_PREM_AMT), TextBox).Text = dvRow(AfaInvoiceRate.InvRateSearchDV.COL_PREMIUM_AMT).ToString
                        CType(e.Row.Cells(Me.GRID_COL_COMM_AMT_IDX).FindControl(Me.GRID_CTRL_NAME_EDIT_COMM_AMT), TextBox).Text = dvRow(AfaInvoiceRate.InvRateSearchDV.COL_COMM_AMT).ToString
                        CType(e.Row.Cells(Me.GRID_COL_ADMIN_AMT_IDX).FindControl(Me.GRID_CTRL_NAME_EDIT_ADMIN_AMT), TextBox).Text = dvRow(AfaInvoiceRate.InvRateSearchDV.COL_ADMIN_AMT).ToString
                        CType(e.Row.Cells(Me.GRID_COL_ANCIL_AMT_IDX).FindControl(Me.GRID_CTRL_NAME_EDIT_ANCILL_AMT), TextBox).Text = dvRow(AfaInvoiceRate.InvRateSearchDV.COL_ANCILLARY_AMT).ToString
                        CType(e.Row.Cells(Me.GRID_COL_OTHER_AMT_IDX).FindControl(Me.GRID_CTRL_NAME_EDIT_OTHER_AMT), TextBox).Text = dvRow(AfaInvoiceRate.InvRateSearchDV.COL_OTHER_AMT).ToString

                        'Effective Date
                        moTextBox = CType(e.Row.Cells(Me.GRID_COL_EFF_DATE_IDX).FindControl(Me.GRID_CTRL_NAME_EDIT_EFF_DATE), TextBox)
                        moTextBox.Visible = True
                        Dim effectiveDate As DateTime = DateTime.Now
                        If (Not dvRow(AfaInvoiceRate.InvRateSearchDV.COL_EFFECTIVE_DATE) Is DBNull.Value) Then
                            effectiveDate = DirectCast(dvRow(AfaInvoiceRate.InvRateSearchDV.COL_EFFECTIVE_DATE), DateTime)
                        End If
                        moTextBox.Text = Me.GetDateFormattedString(effectiveDate)
                        moImageButton = CType(e.Row.Cells(Me.GRID_COL_EFF_DATE_IDX).FindControl(Me.GRID_CTRL_NAME_EDIT_EFF_IMAGE), ImageButton)
                        moImageButton.Visible = True
                        Me.AddCalendar_New(moImageButton, moTextBox)

                        'Expiration Date
                        moTextBox = CType(e.Row.Cells(Me.GRID_COL_EXP_DATE_IDX).FindControl(Me.GRID_CTRL_NAME_EDIT_EXP_DATE), TextBox)
                        moTextBox.Visible = True
                        Dim expirationDate As DateTime = New DateTime(2099, 12, 31) 'DateTime.MaxValue 'DEF-24564
                        If (Not dvRow(AfaInvoiceRate.InvRateSearchDV.COL_EXPIRATION_DATE) Is DBNull.Value) Then
                            expirationDate = DirectCast(dvRow(AfaInvoiceRate.InvRateSearchDV.COL_EXPIRATION_DATE), DateTime)
                        End If
                        moTextBox.Text = Me.GetDateFormattedString(expirationDate)
                        moImageButton = CType(e.Row.Cells(Me.GRID_COL_EXP_DATE_IDX).FindControl(Me.GRID_CTRL_NAME_EDIT_EXP_IMAGE), ImageButton)
                        moImageButton.Visible = True
                        Me.AddCalendar_New(moImageButton, moTextBox)

                    Else

                        'If (Not e.Row.Cells(Me.GRID_COL_RPTRATE_CODE_IDX).FindControl(Me.GRID_CTRL_NAME_LINKBTN_CODE) Is Nothing) Then
                        If (Not e.Row.Cells(Me.GRID_COL_LOSS_TYPE_IDX).FindControl(Me.GRID_CTRL_NAME_LINKBTN_LOSS_TYPE) Is Nothing) Then
                            btnEditItem = CType(e.Row.Cells(Me.GRID_COL_LOSS_TYPE_IDX).FindControl(GRID_CTRL_NAME_LINKBTN_LOSS_TYPE), LinkButton)
                            btnEditItem.CommandArgument = GetGuidStringFromByteArray(CType(dvRow(AfaInvoiceRate.InvRateSearchDV.COL_AFA_INVOICE_RATE_ID), Byte()))
                            btnEditItem.CommandName = EDIT_RPTRATE_COMMAND
                            btnEditItem.Text = LookupListNew.GetDescrionFromListCode("CTYP", dvRow(AfaInvoiceRate.InvRateSearchDV.COL_LOSS_TYPE).ToString) '"RptRate"
                        End If

                        CType(e.Row.Cells(Me.GRID_COL_INS_CODE_IDX).FindControl(Me.GRID_CTRL_NAME_LABEL_INS_CODE), Label).Text = dvRow(AfaInvoiceRate.InvRateSearchDV.COL_INSURANCE_CODE).ToString

                        CType(e.Row.Cells(Me.GRID_COL_HAND_TIER_IDX).FindControl(Me.GRID_CTRL_NAME_LABEL_HAND_TIER), Label).Text = dvRow(AfaInvoiceRate.InvRateSearchDV.COL_TIER).ToString

                        If Not dvRow(AfaInvoiceRate.InvRateSearchDV.COL_REGULATORY_STATE) Is Nothing Then
                            CType(e.Row.Cells(Me.GRID_COL_REGULATORY_STATE_IDX).FindControl(Me.GRID_CTRL_NAME_LABEL_REGULATORY_STATE), Label).Text = dvRow(AfaInvoiceRate.InvRateSearchDV.COL_REGULATORY_STATE).ToString
                        Else
                            CType(e.Row.Cells(Me.GRID_COL_REGULATORY_STATE_IDX).FindControl(Me.GRID_CTRL_NAME_LABEL_REGULATORY_STATE), Label).Text = String.Empty
                        End If
                        'CType(e.Row.Cells(Me.GRID_COL_LOSS_TYPE_IDX).FindControl(Me.GRID_CTRL_NAME_LABEL_LOSS_TYPE), Label).Text = _
                        'LookupListNew.GetDescrionFromListCode("CTYP", dvRow(AfaInvoiceRate.InvRateSearchDV.COL_LOSS_TYPE).ToString)

                        CType(e.Row.Cells(Me.GRID_COL_RETAIL_AMT_IDX).FindControl(Me.GRID_CTRL_NAME_LABEL_RETAIL_AMT), Label).Text = dvRow(AfaInvoiceRate.InvRateSearchDV.COL_RETAIL_AMT).ToString
                        CType(e.Row.Cells(Me.GRID_COL_PREMIUM_AMT_IDX).FindControl(Me.GRID_CTRL_NAME_LABEL_PREM_AMT), Label).Text = dvRow(AfaInvoiceRate.InvRateSearchDV.COL_PREMIUM_AMT).ToString
                        CType(e.Row.Cells(Me.GRID_COL_COMM_AMT_IDX).FindControl(Me.GRID_CTRL_NAME_LABEL_COMM_AMT), Label).Text = dvRow(AfaInvoiceRate.InvRateSearchDV.COL_COMM_AMT).ToString
                        CType(e.Row.Cells(Me.GRID_COL_ADMIN_AMT_IDX).FindControl(Me.GRID_CTRL_NAME_LABEL_ADMIN_AMT), Label).Text = dvRow(AfaInvoiceRate.InvRateSearchDV.COL_ADMIN_AMT).ToString
                        CType(e.Row.Cells(Me.GRID_COL_ANCIL_AMT_IDX).FindControl(Me.GRID_CTRL_NAME_LABEL_ANCILL_AMT), Label).Text = dvRow(AfaInvoiceRate.InvRateSearchDV.COL_ANCILLARY_AMT).ToString
                        CType(e.Row.Cells(Me.GRID_COL_OTHER_AMT_IDX).FindControl(Me.GRID_CTRL_NAME_LABEL_OTHER_AMT), Label).Text = dvRow(AfaInvoiceRate.InvRateSearchDV.COL_OTHER_AMT).ToString

                        moLabel = CType(e.Row.Cells(Me.GRID_COL_EFF_DATE_IDX).FindControl(Me.GRID_CTRL_NAME_LABEL_EFF_DATE), Label)
                        moLabel.Visible = True
                        If (Not dvRow(AfaInvoiceRate.InvRateSearchDV.COL_EFFECTIVE_DATE) Is DBNull.Value) Then
                            moLabel.Text = Me.GetDateFormattedString(DirectCast(dvRow(AfaInvoiceRate.InvRateSearchDV.COL_EFFECTIVE_DATE), Date))
                        End If

                        moLabel = CType(e.Row.Cells(Me.GRID_COL_EXP_DATE_IDX).FindControl(Me.GRID_CTRL_NAME_LABEL_EXP_DATE), Label)
                        moLabel.Visible = True
                        If (Not dvRow(AfaInvoiceRate.InvRateSearchDV.COL_EXPIRATION_DATE) Is DBNull.Value) Then
                            moLabel.Text = Me.GetDateFormattedString(DirectCast(dvRow(AfaInvoiceRate.InvRateSearchDV.COL_EXPIRATION_DATE), Date))
                        End If

                    End If
                End If
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

    Public Sub RowCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)

        Try
            Dim index As Integer
            If (e.CommandName = Me.EDIT_COMMAND) Then
                index = CInt(e.CommandArgument)
                'Do the Edit here

                'Set the IsEditMode flag to TRUE
                Me.State.IsEditMode = True

                Me.State.InvoiceRateID = New Guid(CType(Me.Grid.Rows(index).Cells(Me.GRID_COL_INV_RATE_ID_IDX).FindControl(Me.GRID_CTRL_NAME_LABEL_INV_RATE_ID), Label).Text)
                Me.State.MyInvoiceRateBO = New AfaInvoiceRate(Me.State.InvoiceRateID)

                Me.PopulateGrid()

                Me.State.PageIndex = Grid.PageIndex

                Me.SetControlState()

            ElseIf e.CommandName = EDIT_RPTRATE_COMMAND Then
                If Not e.CommandArgument.ToString().Equals(String.Empty) Then
                    Me.State.InvoiceRateID = New Guid(e.CommandArgument.ToString())
                    Me.callPage(ReportingRatesForm.URL, Me.State.InvoiceRateID)
                End If

            ElseIf (e.CommandName = Me.DELETE_COMMAND) Then
                index = CInt(e.CommandArgument)
                Me.State.InvoiceRateID = New Guid(CType(Me.Grid.Rows(index).Cells(Me.GRID_COL_INV_RATE_ID_IDX).FindControl(Me.GRID_CTRL_NAME_LABEL_INV_RATE_ID), Label).Text)
                Me.DisplayMessage(Message.DELETE_RECORD_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenDeletePromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete

            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

    Public Sub RowCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

#End Region


#Region "Control Handler"

    Private Sub PopulateHeader()
        Try
            If Not Me.State.selectedAFAProductId.Equals(Guid.Empty) Then
                txtProductCode.Text = Me.State.MyProductBO.Code
                txtProductDesc.Text = Me.State.MyProductBO.Description
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnNew.Click

        Try
            Me.State.IsEditMode = True
            Me.State.IsGridVisible = True
            Me.State.IsGridAddNew = True
            AddNew()
            Me.SetControlState()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

    Private Sub AddNew()
        'If Me.State.MyInvoiceRateBO Is Nothing OrElse Me.State.MyInvoiceRateBO.IsNew = False Then
        Me.State.MyInvoiceRateBO = New AfaInvoiceRate
        Me.State.MyInvoiceRateBO.AfaProductId = Me.State.MyProductBO.Id
        State.MyInvoiceRateBO.AddNewRowToSearchDV(Me.State.searchDV, Me.State.MyInvoiceRateBO)
        'End If
        Me.State.InvoiceRateID = Me.State.MyInvoiceRateBO.Id
        State.IsGridAddNew = True
        PopulateGrid()
        'Set focus on the Code TextBox for the EditItemIndex row
        Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.InvoiceRateID, Me.Grid,
                                           Me.State.PageIndex, Me.State.IsEditMode)
        ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Grid)
        SetGridControls(Me.Grid, False)
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs)

        Try
            PopulateBOFromForm()
            If (Me.State.MyInvoiceRateBO.IsDirty) Then
                Me.State.MyInvoiceRateBO.Save()
                Me.State.IsAfterSave = True
                Me.State.IsGridAddNew = False
                Me.MasterPage.MessageController.AddSuccess(Me.MSG_RECORD_SAVED_OK, True)
                Me.State.searchDV = Nothing
                Me.State.MyInvoiceRateBO = Nothing
                Me.ReturnFromEditing()
            Else
                Me.MasterPage.MessageController.AddWarning(Me.MSG_RECORD_NOT_SAVED, True)
                Me.ReturnFromEditing()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            With State
                If .IsGridAddNew Then
                    RemoveNewRowFromSearchDV()
                    .IsGridAddNew = False
                    Grid.PageIndex = .PageIndex
                End If
                .InvoiceRateID = Guid.Empty
                Me.State.MyInvoiceRateBO = Nothing
                .IsEditMode = False
            End With
            Grid.EditIndex = NO_ITEM_SELECTED_INDEX
            'Me.State.searchDV = Nothing
            PopulateGrid()
            SetControlState()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
#End Region



#Region "Button Click Handlers"

    Private Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            Me.ReturnToCallingPage()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

#End Region

End Class