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
            Return Grid.EditIndex > NO_ITEM_SELECTED_INDEX
        End Get
    End Property

    Public Property SortDirection() As String
        Get
            If ViewState("SortDirection") IsNot Nothing Then
                Return ViewState("SortDirection").ToString
            Else
                Return String.Empty
            End If

        End Get
        Set(value As String)
            ViewState("SortDirection") = value
        End Set
    End Property

#End Region

#Region "Page Parameters"

    Private Sub Page_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall
        Try
            If CallingParameters IsNot Nothing Then
                State.selectedAFAProductId = CType(CallingParameters, Guid)

                If Not State.selectedAFAProductId.Equals(Guid.Empty) Then
                    State.MyProductBO = New AfAProduct(State.selectedAFAProductId)
                End If
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
#End Region

#Region "Bread Crum"
    Private Sub UpdateBreadCrum()
        MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(PAGETAB)
        MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(SUMMARYTITLE)
        MasterPage.UsePageTabTitleInBreadCrum = False
        MasterPage.BreadCrum = TranslationBase.TranslateLabelOrMessage(PAGETAB) + ElitaBase.Sperator + TranslationBase.TranslateLabelOrMessage(PAGETITLE)
    End Sub
#End Region

#Region "Page Events"

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        MasterPage.MessageController.Clear()
        Try
            If Not IsPostBack Then
                UpdateBreadCrum()

                ControlMgr.SetVisibleControl(Me, moSearchResults, False)
                'SetControlState()
                'Me.AddCalendar_New(Me.imgExpectedPymntDate, Me.moExpectedPaymentDate)
                State.PageIndex = 0
                TranslateGridHeader(Grid)
                TranslateGridControls(Grid)
                PopulateHeader()
                State.searchDV = Nothing
                If Not State.selectedAFAProductId.Equals(Guid.Empty) Then
                    PopulateGrid()
                End If
            Else
                CheckIfComingFromDeleteConfirm()
                BindBoPropertiesToGridHeaders()
            End If

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
        'Me.ShowMissingTranslations(Me.MasterPage.MessageController)
    End Sub

#End Region

#Region "Helper functions"
    Protected Sub CheckIfComingFromDeleteConfirm()
        Dim confResponse As String = HiddenDeletePromptResponse.Value
        If confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_YES Then
            If Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete Then
                DoDelete()
            End If
            Select Case State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Delete
            End Select
        ElseIf confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_NO Then
            Select Case State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Delete
            End Select
        End If
        'Clean after consuming the action
        State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
        HiddenDeletePromptResponse.Value = ""
    End Sub

    Private Sub DoDelete()
        'Do the delete here
        State.ActionInProgress = DetailPageCommand.Nothing_

        Dim obj As AfaInvoiceRate = New AfaInvoiceRate(State.InvoiceRateID)

        obj.Delete()

        'Call the Save() method in the Role Business Object here

        obj.Save()

        MasterPage.MessageController.AddSuccess(MSG_RECORD_DELETED_OK, True)

        State.PageIndex = Grid.PageIndex

        'Set the IsAfterSave flag to TRUE so that the Paging logic gets invoked
        State.IsAfterSave = True
        State.searchDV = Nothing
        PopulateGrid()
        State.PageIndex = Grid.PageIndex
        State.IsEditMode = False
        SetControlState()
    End Sub

    Private Sub SetControlState()
        If (State.IsEditMode) Then
            ControlMgr.SetVisibleControl(Me, btnNew, False)
            MenuEnabled = False
            If (cboPageSize.Enabled) Then
                ControlMgr.SetEnableControl(Me, cboPageSize, False)
            End If
        Else
            ControlMgr.SetVisibleControl(Me, btnNew, True)
            MenuEnabled = True
            If Not (cboPageSize.Enabled) Then
                ControlMgr.SetEnableControl(Me, cboPageSize, True)
            End If
        End If
    End Sub

    Protected Sub BindBoPropertiesToGridHeaders()
        BindBOPropertyToGridHeader(State.MyInvoiceRateBO, "InsuranceCode", Grid.Columns(GRID_COL_INS_CODE_IDX))
        BindBOPropertyToGridHeader(State.MyInvoiceRateBO, "Tier", Grid.Columns(GRID_COL_HAND_TIER_IDX))
        BindBOPropertyToGridHeader(State.MyInvoiceRateBO, "RegulatoryState", Grid.Columns(GRID_COL_REGULATORY_STATE_IDX))
        BindBOPropertyToGridHeader(State.MyInvoiceRateBO, "LossType", Grid.Columns(GRID_COL_LOSS_TYPE_IDX))
        BindBOPropertyToGridHeader(State.MyInvoiceRateBO, "RetailAmt", Grid.Columns(GRID_COL_RETAIL_AMT_IDX))
        BindBOPropertyToGridHeader(State.MyInvoiceRateBO, "PremiumAmt", Grid.Columns(GRID_COL_PREMIUM_AMT_IDX))
        BindBOPropertyToGridHeader(State.MyInvoiceRateBO, "CommAmt", Grid.Columns(GRID_COL_COMM_AMT_IDX))
        BindBOPropertyToGridHeader(State.MyInvoiceRateBO, "AdminAmt", Grid.Columns(GRID_COL_ADMIN_AMT_IDX))
        BindBOPropertyToGridHeader(State.MyInvoiceRateBO, "AncillaryAmt", Grid.Columns(GRID_COL_ANCIL_AMT_IDX))
        BindBOPropertyToGridHeader(State.MyInvoiceRateBO, "OtherAmt", Grid.Columns(GRID_COL_OTHER_AMT_IDX))
        BindBOPropertyToGridHeader(State.MyInvoiceRateBO, "Effective", Grid.Columns(GRID_COL_EFF_DATE_IDX))
        BindBOPropertyToGridHeader(State.MyInvoiceRateBO, "Expiration", Grid.Columns(GRID_COL_EXP_DATE_IDX))
        ClearGridViewHeadersAndLabelsErrSign()
    End Sub

    Private Sub ReturnFromEditing()

        Grid.EditIndex = NO_ROW_SELECTED_INDEX

        If (Grid.PageCount = 0) Then
            'if returning to the "1st time in" screen
            ControlMgr.SetVisibleControl(Me, Grid, False)
        Else
            ControlMgr.SetVisibleControl(Me, Grid, True)
        End If

        State.IsEditMode = False
        PopulateGrid()
        State.PageIndex = Grid.PageIndex
        SetControlState()
    End Sub

    Private Sub RemoveNewRowFromSearchDV()
        Dim rowind As Integer = NO_ITEM_SELECTED_INDEX
        With State
            If .searchDV IsNot Nothing Then
                rowind = FindSelectedRowIndexFromGuid(.searchDV, .InvoiceRateID)
            End If
        End With
        If rowind <> NO_ITEM_SELECTED_INDEX Then State.searchDV.Delete(rowind)
    End Sub

    Private Function PopulateBOFromForm() As Boolean
        Dim objtxt As TextBox
        Dim ddlLossType As DropDownList
        Dim ddlRegulatoryState As DropDownList
        With State.MyInvoiceRateBO
            objtxt = CType(Grid.Rows((Grid.EditIndex)).Cells(GRID_COL_INS_CODE_IDX).FindControl(GRID_CTRL_NAME_EDIT_INS_CODE), TextBox)
            objtxt.Text = objtxt.Text.ToUpper.Trim
            PopulateBOProperty(State.MyInvoiceRateBO, "InsuranceCode", objtxt)
            objtxt = CType(Grid.Rows((Grid.EditIndex)).Cells(GRID_COL_HAND_TIER_IDX).FindControl(GRID_CTRL_NAME_EDIT_HANDSET_TIER), TextBox)
            objtxt.Text = objtxt.Text.Trim
            PopulateBOProperty(State.MyInvoiceRateBO, "Tier", objtxt)

            ddlRegulatoryState = CType(Grid.Rows((Grid.EditIndex)).Cells(GRID_COL_REGULATORY_STATE_IDX).FindControl(GRID_CTRL_NAME_EDIT_REGULATORY_STATE_LIST), DropDownList)
            REM Me.State.MyInvoiceRateBO.RegulatoryState = Me.GetSelectedValue(ddlRegulatoryState)

            PopulateBOProperty(State.MyInvoiceRateBO, "RegulatoryState", ddlRegulatoryState, isGuidValue:=False, isStringValue:=True)

            ddlLossType = CType(Grid.Rows((Grid.EditIndex)).Cells(GRID_COL_LOSS_TYPE_IDX).FindControl(GRID_CTRL_NAME_EDIT_LOSS_TYPE_LIST), DropDownList)
            'PopulateBOProperty(State.MyInvoiceRateBO, "LossType", ddlLossType, False)
            State.MyInvoiceRateBO.LossType = GetSelectedValue(ddlLossType)

            objtxt = CType(Grid.Rows((Grid.EditIndex)).Cells(GRID_COL_RETAIL_AMT_IDX).FindControl(GRID_CTRL_NAME_EDIT_RETAIL_AMT), TextBox)
            PopulateBOProperty(State.MyInvoiceRateBO, "RetailAmt", objtxt)
            objtxt = CType(Grid.Rows((Grid.EditIndex)).Cells(GRID_COL_PREMIUM_AMT_IDX).FindControl(GRID_CTRL_NAME_EDIT_PREM_AMT), TextBox)
            PopulateBOProperty(State.MyInvoiceRateBO, "PremiumAmt", objtxt)
            objtxt = CType(Grid.Rows((Grid.EditIndex)).Cells(GRID_COL_COMM_AMT_IDX).FindControl(GRID_CTRL_NAME_EDIT_COMM_AMT), TextBox)
            PopulateBOProperty(State.MyInvoiceRateBO, "CommAmt", objtxt)
            objtxt = CType(Grid.Rows((Grid.EditIndex)).Cells(GRID_COL_ADMIN_AMT_IDX).FindControl(GRID_CTRL_NAME_EDIT_ADMIN_AMT), TextBox)
            PopulateBOProperty(State.MyInvoiceRateBO, "AdminAmt", objtxt)
            objtxt = CType(Grid.Rows((Grid.EditIndex)).Cells(GRID_COL_ANCIL_AMT_IDX).FindControl(GRID_CTRL_NAME_EDIT_ANCILL_AMT), TextBox)
            PopulateBOProperty(State.MyInvoiceRateBO, "AncillaryAmt", objtxt)
            objtxt = CType(Grid.Rows((Grid.EditIndex)).Cells(GRID_COL_OTHER_AMT_IDX).FindControl(GRID_CTRL_NAME_EDIT_OTHER_AMT), TextBox)
            PopulateBOProperty(State.MyInvoiceRateBO, "OtherAmt", objtxt)
            objtxt = CType(Grid.Rows((Grid.EditIndex)).Cells(GRID_COL_EFF_DATE_IDX).FindControl(GRID_CTRL_NAME_EDIT_EFF_DATE), TextBox)
            PopulateBOProperty(State.MyInvoiceRateBO, "Effective", objtxt)
            objtxt = CType(Grid.Rows((Grid.EditIndex)).Cells(GRID_COL_EXP_DATE_IDX).FindControl(GRID_CTRL_NAME_EDIT_EXP_DATE), TextBox)
            PopulateBOProperty(State.MyInvoiceRateBO, "Expiration", objtxt)
        End With
        If ErrCollection.Count > 0 Then
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

            State.searchDV.Sort = SortDirection
            If (State.IsAfterSave) Then
                State.IsAfterSave = False
                SetPageAndSelectedIndexFromGuid(State.searchDV, State.InvoiceRateID, Grid, State.PageIndex)
            ElseIf (State.IsEditMode) Then
                SetPageAndSelectedIndexFromGuid(State.searchDV, State.InvoiceRateID, Grid, State.PageIndex, State.IsEditMode)
            Else
                SetPageAndSelectedIndexFromGuid(State.searchDV, Guid.Empty, Grid, State.PageIndex)
            End If

            Grid.AutoGenerateColumns = False
            'Me.Grid.Columns(Me.GRID_COL_CODE_IDX).SortExpression = AfaInvoiceRate.InvoiceSearchDV.COL_CODE
            'Me.Grid.Columns(Me.GRID_COL_DESC_IDX).SortExpression = AfaInvoiceRate.InvoiceSearchDV.COL_DESCRIPTION
            SortAndBindGrid(blnNewSearch)

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

    Private Sub SortAndBindGrid(Optional ByVal blnShowErr As Boolean = True)

        TranslateGridControls(Grid)

        If (State.searchDV.Count = 0) Then
            State.searchDV = Nothing
            State.MyInvoiceRateBO = New AfaInvoiceRate
            State.MyInvoiceRateBO.AddNewRowToSearchDV(State.searchDV, State.MyInvoiceRateBO)
            Grid.DataSource = State.searchDV
            Grid.DataBind()
            Grid.Rows(0).Visible = False
            State.IsGridAddNew = True
            State.IsGridVisible = False
            If blnShowErr Then
                MasterPage.MessageController.AddInformation(ElitaPlus.ElitaPlusWebApp.Message.MSG_NO_RECORDS_FOUND, True)
            End If
        Else
            Grid.Enabled = True
            Grid.PageSize = State.PageSize
            Grid.DataSource = State.searchDV
            State.IsGridVisible = True
            HighLightSortColumn(Grid, SortDirection)
            Grid.DataBind()
        End If

        ControlMgr.SetVisibleControl(Me, Grid, State.IsGridVisible)
        ControlMgr.SetVisibleControl(Me, moSearchResults, State.IsGridVisible)

        Session("recCount") = State.searchDV.Count

        If Grid.Visible Then
            If (State.IsGridAddNew) Then
                lblRecordCount.Text = (State.searchDV.Count - 1) & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            Else
                lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If
        End If
        ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Grid)

    End Sub

    Private Sub Grid_PageIndexChanged(sender As Object, e As System.EventArgs) Handles Grid.PageIndexChanged
        Try
            If (Not (State.IsEditMode)) Then
                State.PageIndex = Grid.PageIndex
                State.InvoiceRateID = Guid.Empty
                PopulateGrid()
            End If
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

    Private Sub Grid_Sorting(sender As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting
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

            State.PageIndex = 0
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub cboPageSize_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            State.PageSize = CType(cboPageSize.SelectedValue, Integer)
            State.PageIndex = NewCurrentPageIndex(Grid, State.searchDV.Count, State.PageSize)
            Grid.PageIndex = State.PageIndex
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
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

            If dvRow IsNot Nothing Then
                strID = GetGuidStringFromByteArray(CType(dvRow(AfaInvoiceRate.InvRateSearchDV.COL_AFA_INVOICE_RATE_ID), Byte()))
                If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem Then
                    CType(e.Row.Cells(GRID_COL_INV_RATE_ID_IDX).FindControl(GRID_CTRL_NAME_LABEL_INV_RATE_ID), Label).Text = strID

                    If (State.IsEditMode = True AndAlso State.InvoiceRateID.ToString.Equals(strID)) Then

                        CType(e.Row.Cells(GRID_COL_INS_CODE_IDX).FindControl(GRID_CTRL_NAME_EDIT_INS_CODE), TextBox).Text = dvRow(AfaInvoiceRate.InvRateSearchDV.COL_INSURANCE_CODE).ToString

                        CType(e.Row.Cells(GRID_COL_HAND_TIER_IDX).FindControl(GRID_CTRL_NAME_EDIT_HANDSET_TIER), TextBox).Text = dvRow(AfaInvoiceRate.InvRateSearchDV.COL_TIER).ToString

                        'REGULATORY_STATE
                        Dim oListContext As New Assurant.Elita.CommonConfiguration.ListContext
                        Dim companyID As Guid = ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID
                        Dim companyObj As Company = New Company(companyID)
                        oListContext.CountryId = companyObj.CountryId

                        Dim regulatoryStateList As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.RegionsByCountry, Thread.CurrentPrincipal.GetLanguageCode(), oListContext)
                        ddlRegulatoryState.Populate(regulatoryStateList, New PopulateOptions() With {.AddBlankItem = True, .BlankItemValue = String.Empty, .ValueFunc = AddressOf PopulateOptions.GetCode})
                        If (State.IsGridAddNew OrElse dvRow(AfaInvoiceRate.InvRateSearchDV.COL_REGULATORY_STATE) Is Nothing) Then
                            ddlRegulatoryState.SelectedIndex = NO_ITEM_SELECTED_INDEX
                        Else
                            SetSelectedItem(ddlRegulatoryState, dvRow(AfaInvoiceRate.InvRateSearchDV.COL_REGULATORY_STATE).ToString)
                        End If

                        'LOSS_TYPE
                        lossTypeDv = ReppolicyClaimCount.GetCoverageTypeListByDealer(State.MyProductBO.DealerId)
                        BindCodeToListControl(ddlLossType, lossTypeDv, , , True)

                        If (State.IsGridAddNew) Then
                            ddlLossType.SelectedIndex = NO_ITEM_SELECTED_INDEX
                        Else
                            SetSelectedItem(ddlLossType, dvRow(AfaInvoiceRate.InvRateSearchDV.COL_LOSS_TYPE).ToString)
                        End If

                        CType(e.Row.Cells(GRID_COL_RETAIL_AMT_IDX).FindControl(GRID_CTRL_NAME_EDIT_RETAIL_AMT), TextBox).Text = dvRow(AfaInvoiceRate.InvRateSearchDV.COL_RETAIL_AMT).ToString
                        CType(e.Row.Cells(GRID_COL_PREMIUM_AMT_IDX).FindControl(GRID_CTRL_NAME_EDIT_PREM_AMT), TextBox).Text = dvRow(AfaInvoiceRate.InvRateSearchDV.COL_PREMIUM_AMT).ToString
                        CType(e.Row.Cells(GRID_COL_COMM_AMT_IDX).FindControl(GRID_CTRL_NAME_EDIT_COMM_AMT), TextBox).Text = dvRow(AfaInvoiceRate.InvRateSearchDV.COL_COMM_AMT).ToString
                        CType(e.Row.Cells(GRID_COL_ADMIN_AMT_IDX).FindControl(GRID_CTRL_NAME_EDIT_ADMIN_AMT), TextBox).Text = dvRow(AfaInvoiceRate.InvRateSearchDV.COL_ADMIN_AMT).ToString
                        CType(e.Row.Cells(GRID_COL_ANCIL_AMT_IDX).FindControl(GRID_CTRL_NAME_EDIT_ANCILL_AMT), TextBox).Text = dvRow(AfaInvoiceRate.InvRateSearchDV.COL_ANCILLARY_AMT).ToString
                        CType(e.Row.Cells(GRID_COL_OTHER_AMT_IDX).FindControl(GRID_CTRL_NAME_EDIT_OTHER_AMT), TextBox).Text = dvRow(AfaInvoiceRate.InvRateSearchDV.COL_OTHER_AMT).ToString

                        'Effective Date
                        moTextBox = CType(e.Row.Cells(GRID_COL_EFF_DATE_IDX).FindControl(GRID_CTRL_NAME_EDIT_EFF_DATE), TextBox)
                        moTextBox.Visible = True
                        Dim effectiveDate As DateTime = DateTime.Now
                        If (dvRow(AfaInvoiceRate.InvRateSearchDV.COL_EFFECTIVE_DATE) IsNot DBNull.Value) Then
                            effectiveDate = DirectCast(dvRow(AfaInvoiceRate.InvRateSearchDV.COL_EFFECTIVE_DATE), DateTime)
                        End If
                        moTextBox.Text = GetDateFormattedString(effectiveDate)
                        moImageButton = CType(e.Row.Cells(GRID_COL_EFF_DATE_IDX).FindControl(GRID_CTRL_NAME_EDIT_EFF_IMAGE), ImageButton)
                        moImageButton.Visible = True
                        AddCalendar_New(moImageButton, moTextBox)

                        'Expiration Date
                        moTextBox = CType(e.Row.Cells(GRID_COL_EXP_DATE_IDX).FindControl(GRID_CTRL_NAME_EDIT_EXP_DATE), TextBox)
                        moTextBox.Visible = True
                        Dim expirationDate As DateTime = New DateTime(2099, 12, 31) 'DateTime.MaxValue 'DEF-24564
                        If (dvRow(AfaInvoiceRate.InvRateSearchDV.COL_EXPIRATION_DATE) IsNot DBNull.Value) Then
                            expirationDate = DirectCast(dvRow(AfaInvoiceRate.InvRateSearchDV.COL_EXPIRATION_DATE), DateTime)
                        End If
                        moTextBox.Text = GetDateFormattedString(expirationDate)
                        moImageButton = CType(e.Row.Cells(GRID_COL_EXP_DATE_IDX).FindControl(GRID_CTRL_NAME_EDIT_EXP_IMAGE), ImageButton)
                        moImageButton.Visible = True
                        AddCalendar_New(moImageButton, moTextBox)

                    Else

                        'If (Not e.Row.Cells(Me.GRID_COL_RPTRATE_CODE_IDX).FindControl(Me.GRID_CTRL_NAME_LINKBTN_CODE) Is Nothing) Then
                        If (e.Row.Cells(GRID_COL_LOSS_TYPE_IDX).FindControl(GRID_CTRL_NAME_LINKBTN_LOSS_TYPE) IsNot Nothing) Then
                            btnEditItem = CType(e.Row.Cells(GRID_COL_LOSS_TYPE_IDX).FindControl(GRID_CTRL_NAME_LINKBTN_LOSS_TYPE), LinkButton)
                            btnEditItem.CommandArgument = GetGuidStringFromByteArray(CType(dvRow(AfaInvoiceRate.InvRateSearchDV.COL_AFA_INVOICE_RATE_ID), Byte()))
                            btnEditItem.CommandName = EDIT_RPTRATE_COMMAND
                            btnEditItem.Text = LookupListNew.GetDescrionFromListCode("CTYP", dvRow(AfaInvoiceRate.InvRateSearchDV.COL_LOSS_TYPE).ToString) '"RptRate"
                        End If

                        CType(e.Row.Cells(GRID_COL_INS_CODE_IDX).FindControl(GRID_CTRL_NAME_LABEL_INS_CODE), Label).Text = dvRow(AfaInvoiceRate.InvRateSearchDV.COL_INSURANCE_CODE).ToString

                        CType(e.Row.Cells(GRID_COL_HAND_TIER_IDX).FindControl(GRID_CTRL_NAME_LABEL_HAND_TIER), Label).Text = dvRow(AfaInvoiceRate.InvRateSearchDV.COL_TIER).ToString

                        If dvRow(AfaInvoiceRate.InvRateSearchDV.COL_REGULATORY_STATE) IsNot Nothing Then
                            CType(e.Row.Cells(GRID_COL_REGULATORY_STATE_IDX).FindControl(GRID_CTRL_NAME_LABEL_REGULATORY_STATE), Label).Text = dvRow(AfaInvoiceRate.InvRateSearchDV.COL_REGULATORY_STATE).ToString
                        Else
                            CType(e.Row.Cells(GRID_COL_REGULATORY_STATE_IDX).FindControl(GRID_CTRL_NAME_LABEL_REGULATORY_STATE), Label).Text = String.Empty
                        End If
                        'CType(e.Row.Cells(Me.GRID_COL_LOSS_TYPE_IDX).FindControl(Me.GRID_CTRL_NAME_LABEL_LOSS_TYPE), Label).Text = _
                        'LookupListNew.GetDescrionFromListCode("CTYP", dvRow(AfaInvoiceRate.InvRateSearchDV.COL_LOSS_TYPE).ToString)

                        CType(e.Row.Cells(GRID_COL_RETAIL_AMT_IDX).FindControl(GRID_CTRL_NAME_LABEL_RETAIL_AMT), Label).Text = dvRow(AfaInvoiceRate.InvRateSearchDV.COL_RETAIL_AMT).ToString
                        CType(e.Row.Cells(GRID_COL_PREMIUM_AMT_IDX).FindControl(GRID_CTRL_NAME_LABEL_PREM_AMT), Label).Text = dvRow(AfaInvoiceRate.InvRateSearchDV.COL_PREMIUM_AMT).ToString
                        CType(e.Row.Cells(GRID_COL_COMM_AMT_IDX).FindControl(GRID_CTRL_NAME_LABEL_COMM_AMT), Label).Text = dvRow(AfaInvoiceRate.InvRateSearchDV.COL_COMM_AMT).ToString
                        CType(e.Row.Cells(GRID_COL_ADMIN_AMT_IDX).FindControl(GRID_CTRL_NAME_LABEL_ADMIN_AMT), Label).Text = dvRow(AfaInvoiceRate.InvRateSearchDV.COL_ADMIN_AMT).ToString
                        CType(e.Row.Cells(GRID_COL_ANCIL_AMT_IDX).FindControl(GRID_CTRL_NAME_LABEL_ANCILL_AMT), Label).Text = dvRow(AfaInvoiceRate.InvRateSearchDV.COL_ANCILLARY_AMT).ToString
                        CType(e.Row.Cells(GRID_COL_OTHER_AMT_IDX).FindControl(GRID_CTRL_NAME_LABEL_OTHER_AMT), Label).Text = dvRow(AfaInvoiceRate.InvRateSearchDV.COL_OTHER_AMT).ToString

                        moLabel = CType(e.Row.Cells(GRID_COL_EFF_DATE_IDX).FindControl(GRID_CTRL_NAME_LABEL_EFF_DATE), Label)
                        moLabel.Visible = True
                        If (dvRow(AfaInvoiceRate.InvRateSearchDV.COL_EFFECTIVE_DATE) IsNot DBNull.Value) Then
                            moLabel.Text = GetDateFormattedString(DirectCast(dvRow(AfaInvoiceRate.InvRateSearchDV.COL_EFFECTIVE_DATE), Date))
                        End If

                        moLabel = CType(e.Row.Cells(GRID_COL_EXP_DATE_IDX).FindControl(GRID_CTRL_NAME_LABEL_EXP_DATE), Label)
                        moLabel.Visible = True
                        If (dvRow(AfaInvoiceRate.InvRateSearchDV.COL_EXPIRATION_DATE) IsNot DBNull.Value) Then
                            moLabel.Text = GetDateFormattedString(DirectCast(dvRow(AfaInvoiceRate.InvRateSearchDV.COL_EXPIRATION_DATE), Date))
                        End If

                    End If
                End If
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

    Public Sub RowCommand(source As System.Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs)

        Try
            Dim index As Integer
            If (e.CommandName = EDIT_COMMAND) Then
                index = CInt(e.CommandArgument)
                'Do the Edit here

                'Set the IsEditMode flag to TRUE
                State.IsEditMode = True

                State.InvoiceRateID = New Guid(CType(Grid.Rows(index).Cells(GRID_COL_INV_RATE_ID_IDX).FindControl(GRID_CTRL_NAME_LABEL_INV_RATE_ID), Label).Text)
                State.MyInvoiceRateBO = New AfaInvoiceRate(State.InvoiceRateID)

                PopulateGrid()

                State.PageIndex = Grid.PageIndex

                SetControlState()

            ElseIf e.CommandName = EDIT_RPTRATE_COMMAND Then
                If Not e.CommandArgument.ToString().Equals(String.Empty) Then
                    State.InvoiceRateID = New Guid(e.CommandArgument.ToString())
                    callPage(ReportingRatesForm.URL, State.InvoiceRateID)
                End If

            ElseIf (e.CommandName = DELETE_COMMAND) Then
                index = CInt(e.CommandArgument)
                State.InvoiceRateID = New Guid(CType(Grid.Rows(index).Cells(GRID_COL_INV_RATE_ID_IDX).FindControl(GRID_CTRL_NAME_LABEL_INV_RATE_ID), Label).Text)
                DisplayMessage(Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenDeletePromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete

            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

    Public Sub RowCreated(sender As System.Object, e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#End Region


#Region "Control Handler"

    Private Sub PopulateHeader()
        Try
            If Not State.selectedAFAProductId.Equals(Guid.Empty) Then
                txtProductCode.Text = State.MyProductBO.Code
                txtProductDesc.Text = State.MyProductBO.Description
            End If

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Protected Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click

        Try
            State.IsEditMode = True
            State.IsGridVisible = True
            State.IsGridAddNew = True
            AddNew()
            SetControlState()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

    Private Sub AddNew()
        'If Me.State.MyInvoiceRateBO Is Nothing OrElse Me.State.MyInvoiceRateBO.IsNew = False Then
        State.MyInvoiceRateBO = New AfaInvoiceRate
        State.MyInvoiceRateBO.AfaProductId = State.MyProductBO.Id
        State.MyInvoiceRateBO.AddNewRowToSearchDV(State.searchDV, State.MyInvoiceRateBO)
        'End If
        State.InvoiceRateID = State.MyInvoiceRateBO.Id
        State.IsGridAddNew = True
        PopulateGrid()
        'Set focus on the Code TextBox for the EditItemIndex row
        SetPageAndSelectedIndexFromGuid(State.searchDV, State.InvoiceRateID, Grid,
                                           State.PageIndex, State.IsEditMode)
        ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Grid)
        SetGridControls(Grid, False)
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)

        Try
            PopulateBOFromForm()
            If (State.MyInvoiceRateBO.IsDirty) Then
                State.MyInvoiceRateBO.Save()
                State.IsAfterSave = True
                State.IsGridAddNew = False
                MasterPage.MessageController.AddSuccess(MSG_RECORD_SAVED_OK, True)
                State.searchDV = Nothing
                State.MyInvoiceRateBO = Nothing
                ReturnFromEditing()
            Else
                MasterPage.MessageController.AddWarning(MSG_RECORD_NOT_SAVED, True)
                ReturnFromEditing()
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try
            With State
                If .IsGridAddNew Then
                    RemoveNewRowFromSearchDV()
                    .IsGridAddNew = False
                    Grid.PageIndex = .PageIndex
                End If
                .InvoiceRateID = Guid.Empty
                State.MyInvoiceRateBO = Nothing
                .IsEditMode = False
            End With
            Grid.EditIndex = NO_ITEM_SELECTED_INDEX
            'Me.State.searchDV = Nothing
            PopulateGrid()
            SetControlState()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
#End Region



#Region "Button Click Handlers"

    Private Sub btnBack_Click(sender As Object, e As System.EventArgs) Handles btnBack.Click
        Try
            ReturnToCallingPage()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#End Region

End Class