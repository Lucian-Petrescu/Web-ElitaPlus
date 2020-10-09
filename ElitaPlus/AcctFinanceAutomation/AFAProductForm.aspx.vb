Imports Assurant.ElitaPlus.DALObjects
Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms

Public Class AFAProductForm
    Inherits ElitaPlusSearchPage


#Region "Constants"
    Public Const URL As String = "AcctFinanceAutomation/AFAProductForm.aspx"
    Public Const PAGETITLE As String = "AFA_PRODUCT"
    Public Const PAGETAB As String = "FINANCE_AUTOMATION"
    Public Const SUMMARYTITLE As String = "SEARCH"

    Public Const EDIT_INVRATE_COMMAND As String = "EditInvoiceRate"

    Private Const MSG_CONFIRM_PROMPT As String = "MSG_CONFIRM_PROMPT"
    Private Const MSG_RECORD_DELETED_OK As String = "MSG_RECORD_DELETED_OK"
    Private Const MSG_RECORD_SAVED_OK As String = "MSG_RECORD_SAVED_OK"
    Private Const MSG_RECORD_NOT_SAVED As String = "MSG_RECORD_NOT_SAVED"

    Private Const GRID_COL_PRODUCT_ID_IDX As Integer = 0
    Private Const GRID_COL_DEALER_CODE_IDX As Integer = 1
    Private Const GRID_COL_CODE_IDX As Integer = 2
    Private Const GRID_COL_DESC_IDX As Integer = 3
    Private Const GRID_COL_PROD_TYPE_IDX As Integer = 4
    Private Const GRID_COL_EDIT_IDX As Integer = 5
    Private Const GRID_COL_DELETE_IDX As Integer = 6

    Private Const GRID_CTRL_NAME_LABEL_PRODUCT_ID As String = "lblProductID"
    Private Const GRID_CTRL_NAME_LABEL_DEALER As String = "lblDealer"
    'Private Const GRID_CTRL_NAME_LABEL_CODE As String = "lblCode"
    Private Const GRID_CTRL_NAME_LINKBTN_CODE As String = "btnEditInvoiceRate"
    Private Const GRID_CTRL_NAME_LABEL_DESC As String = "lblDesc"
    Private Const GRID_CTRL_NAME_LABEL_PROD_TYPE As String = "lblProdType"

    Private Const GRID_CTRL_NAME_EDIT_CODE As String = "txtCode"
    Private Const GRID_CTRL_NAME_EDIT_DEALER As String = "lblEditDealer"
    Private Const GRID_CTRL_NAME_EDIT_DEALER_LIST As String = "moDealer"
    Private Const GRID_CTRL_NAME_EDIT_DESC As String = "txtDesc"
    Private Const GRID_CTRL_NAME_EDIT_PROD_TYPE_LIST As String = "ddlProdType"

    Private Const EDIT_COMMAND As String = "SelectAction"
    Private Const DELETE_COMMAND As String = "DeleteRecord"
    Private Const SORT_COMMAND As String = "Sort"

    Private Const NO_ROW_SELECTED_INDEX As Integer = -1
#End Region

#Region "Bread Crum"
    Private Sub UpdateBreadCrum()
        MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(PAGETAB)
        MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(SUMMARYTITLE)
        MasterPage.UsePageTabTitleInBreadCrum = False
        MasterPage.BreadCrum = TranslationBase.TranslateLabelOrMessage(PAGETAB) + ElitaBase.Sperator + TranslationBase.TranslateLabelOrMessage(PAGETITLE)
    End Sub
#End Region

#Region "Page State"
    Private IsReturningFromChild As Boolean = False

    ' This class keeps the current state for the search page.
    Class MyState
        Public MyBO As AfAProduct
        Public productID As Guid
        Public dealer As String
        Public PageIndex As Integer = 0
        Public PageSize As Integer = DEFAULT_NEW_UI_PAGE_SIZE
        Public searchDV As AfAProduct.AFAProductSearchDV = Nothing
        Public HasDataChanged As Boolean
        Public IsGridAddNew As Boolean = False
        Public IsEditMode As Boolean = False
        Public IsGridVisible As Boolean
        Public IsAfterSave As Boolean
        Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
        Public SortExpression As String = AfAProduct.AFAProductSearchDV.COL_CODE
        Public DealerIdInSearch As Guid = Guid.Empty
        Public selectedAFAProductId As Guid = Guid.Empty
        Public searchProdCode As String = ""
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


#End Region

#Region "Page Events"

    Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles MyBase.PageReturn
        Try
            'Me.MenuEnabled = True
            IsReturningFromChild = True
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        MasterPage.MessageController.Clear()
        Form.DefaultButton = btnSearch.UniqueID
        Try
            If Not IsPostBack Then
                UpdateBreadCrum()
                ControlMgr.SetVisibleControl(Me, moSearchResults, False)

                State.PageIndex = 0
                TranslateGridHeader(Grid)
                TranslateGridControls(Grid)
                ' Me.BindCodeNameToListControl(ddlDealer, LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, True, "Code"), , , , True)
                Dim oDealerList = GetDealerListByCompanyForUser()
                Dim dealerTextFunc As Func(Of DataElements.ListItem, String) = Function(li As DataElements.ListItem)
                                                                                   Return li.ExtendedCode + " - " + li.Translation + " " + "(" + li.Code + ")"
                                                                               End Function
                ddlDealer.Populate(oDealerList, New PopulateOptions() With
                                           {
                                            .TextFunc = dealerTextFunc,
                                            .AddBlankItem = True
                                           })
                If IsReturningFromChild Then
                    PopulateSearchFieldsFromState()
                    State.searchDV = Nothing
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
    Private Function GetDealerListByCompanyForUser() As Assurant.Elita.CommonConfiguration.DataElements.ListItem()
        Dim Index As Integer
        Dim oListContext As New Assurant.Elita.CommonConfiguration.ListContext

        Dim UserCompanies As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies

        Dim oDealerList As New Collections.Generic.List(Of Assurant.Elita.CommonConfiguration.DataElements.ListItem)

        For Index = 0 To UserCompanies.Count - 1
            'UserCompanyList &= ",'" & GuidControl.GuidToHexString(UserCompanyies(Index))
            oListContext.CompanyId = UserCompanies(Index)
            Dim oDealerListForCompany As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="DealerListByCompany", context:=oListContext)
            If oDealerListForCompany.Count > 0 Then
                If oDealerList IsNot Nothing Then
                    oDealerList.AddRange(oDealerListForCompany)
                Else
                    oDealerList = oDealerListForCompany.Clone()
                End If

            End If
        Next

        Return oDealerList.ToArray()

    End Function
#End Region


#Region "Helper functions"

    Public Sub PopulateSearchFieldsFromState()
        SetSelectedItem(ddlDealer, State.DealerIdInSearch)
        txtProductCode.Text = State.searchProdCode
    End Sub

    Public Sub PopulateProductTypeDropdown(ddlProdType As DropDownList)

        Dim productTypeLk As DataView
        Try
            productTypeLk = LookupListNew.GetAFAProductTypeList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            ' Me.BindCodeToListControl(ddlProdType, productTypeLk, , , True) 'AFAPT
            Dim ProductTypeLKl As ListItem() = CommonConfigManager.Current.ListManager.GetList("AFAPT", Thread.CurrentPrincipal.GetLanguageCode())
            ddlProdType.Populate(ProductTypeLKl, New PopulateOptions() With
             {
              .AddBlankItem = True,
              .ValueFunc = AddressOf .GetCode
             })
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

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
        'Save the RiskTypeId in the Session

        Dim obj As AfAProduct = New AfAProduct(State.productID)

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
            ControlMgr.SetEnableControl(Me, btnSearch, False)
            ControlMgr.SetEnableControl(Me, btnClearSearch, False)
            MenuEnabled = False
            If (cboPageSize.Enabled) Then
                ControlMgr.SetEnableControl(Me, cboPageSize, False)
            End If
        Else
            ControlMgr.SetVisibleControl(Me, btnNew, True)
            ControlMgr.SetEnableControl(Me, btnSearch, True)
            ControlMgr.SetEnableControl(Me, btnClearSearch, True)
            MenuEnabled = True
            If Not (cboPageSize.Enabled) Then
                ControlMgr.SetEnableControl(Me, cboPageSize, True)
            End If
        End If
    End Sub

    Protected Sub BindBoPropertiesToGridHeaders()
        BindBOPropertyToGridHeader(State.MyBO, "DealerId", Grid.Columns(GRID_COL_DEALER_CODE_IDX))
        BindBOPropertyToGridHeader(State.MyBO, "Code", Grid.Columns(GRID_COL_CODE_IDX))
        BindBOPropertyToGridHeader(State.MyBO, "Description", Grid.Columns(GRID_COL_DESC_IDX))
        BindBOPropertyToGridHeader(State.MyBO, "ProductType", Grid.Columns(GRID_COL_PROD_TYPE_IDX))
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
                rowind = FindSelectedRowIndexFromGuid(.searchDV, .productID)
            End If
        End With
        If rowind <> NO_ITEM_SELECTED_INDEX Then State.searchDV.Delete(rowind)
    End Sub

    Private Function PopulateBOFromForm() As Boolean
        Dim objtxt As TextBox
        Dim ddlDealer As DropDownList
        Dim ddlProdType As DropDownList
        With State.MyBO

            State.MyBO.DealerId = State.DealerIdInSearch

            objtxt = CType(Grid.Rows((Grid.EditIndex)).Cells(GRID_COL_CODE_IDX).FindControl(GRID_CTRL_NAME_EDIT_CODE), TextBox)
            objtxt.Text = objtxt.Text.ToUpper
            PopulateBOProperty(State.MyBO, "Code", objtxt)

            objtxt = CType(Grid.Rows((Grid.EditIndex)).Cells(GRID_COL_DESC_IDX).FindControl(GRID_CTRL_NAME_EDIT_DESC), TextBox)
            PopulateBOProperty(State.MyBO, "Description", objtxt)

            ddlProdType = CType(Grid.Rows((Grid.EditIndex)).Cells(GRID_COL_PROD_TYPE_IDX).FindControl(GRID_CTRL_NAME_EDIT_PROD_TYPE_LIST), DropDownList)
            If ddlProdType.SelectedItem.Text = String.Empty Then
                State.MyBO.ProductType = String.Empty
            Else
                PopulateBOProperty(State.MyBO, "ProductType", ddlProdType, False, True)
            End If

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
                If (.searchDV Is Nothing) AndAlso Not State.IsGridAddNew = True Then
                    .searchDV = AfAProduct.getList(.DealerIdInSearch, .searchProdCode)
                    blnNewSearch = True
                End If
            End With

            If State.searchDV IsNot Nothing Then
                State.searchDV.Sort = State.SortExpression
                If (State.IsAfterSave) Then
                    State.IsAfterSave = False
                    SetPageAndSelectedIndexFromGuid(State.searchDV, State.productID, Grid, State.PageIndex)
                ElseIf (State.IsEditMode) Then
                    SetPageAndSelectedIndexFromGuid(State.searchDV, State.productID, Grid, State.PageIndex, State.IsEditMode)
                Else
                    SetPageAndSelectedIndexFromGuid(State.searchDV, Guid.Empty, Grid, State.PageIndex)
                End If
            End If

            Grid.AutoGenerateColumns = False
            Grid.Columns(GRID_COL_CODE_IDX).SortExpression = AfAProduct.AFAProductSearchDV.COL_CODE
            Grid.Columns(GRID_COL_DESC_IDX).SortExpression = AfAProduct.AFAProductSearchDV.COL_DESCRIPTION
            SortAndBindGrid(blnNewSearch)

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

    Private Sub SortAndBindGrid(Optional ByVal blnShowErr As Boolean = True)

        TranslateGridControls(Grid)

        If State.searchDV IsNot Nothing AndAlso (State.searchDV.Count = 0) Then
            State.searchDV = Nothing
            State.MyBO = New AfAProduct
            State.MyBO.AddNewRowToSearchDV(State.searchDV, State.MyBO)
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
            HighLightSortColumn(Grid, State.SortExpression)
            Grid.DataBind()
        End If

        ControlMgr.SetVisibleControl(Me, Grid, State.IsGridVisible)
        ControlMgr.SetVisibleControl(Me, moSearchResults, State.IsGridVisible)

        If State.searchDV IsNot Nothing Then
            Session("recCount") = State.searchDV.Count

            If Grid.Visible Then
                If (State.IsGridAddNew) Then
                    lblRecordCount.Text = (State.searchDV.Count - 1) & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                Else
                    lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            End If
        End If

        ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Grid)

    End Sub

    Private Sub Grid_PageIndexChanged(sender As Object, e As System.EventArgs) Handles Grid.PageIndexChanged
        Try
            If (Not (State.IsEditMode)) Then
                State.PageIndex = Grid.PageIndex
                State.productID = Guid.Empty
                State.selectedAFAProductId = Guid.Empty
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
            Dim spaceIndex As Integer = State.SortExpression.LastIndexOf(" ")


            If spaceIndex > 0 AndAlso State.SortExpression.Substring(0, spaceIndex).Equals(e.SortExpression) Then
                If State.SortExpression.EndsWith(" ASC") Then
                    State.SortExpression = e.SortExpression + " DESC"
                Else
                    State.SortExpression = e.SortExpression + " ASC"
                End If
            Else
                State.SortExpression = e.SortExpression + " ASC"
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
            Dim dealerLabel As Label = CType(e.Row.FindControl(GRID_CTRL_NAME_LABEL_DEALER), Label)
            Dim dealerEditLabel As Label = CType(e.Row.FindControl(GRID_CTRL_NAME_EDIT_DEALER), Label)
            Dim ddlProductType As DropDownList = CType(e.Row.FindControl(GRID_CTRL_NAME_EDIT_PROD_TYPE_LIST), DropDownList)

            Dim dealerId As Guid
            Dim objDealer As Dealer
            Dim btnEditItem As LinkButton

            If dvRow IsNot Nothing Then
                strID = GetGuidStringFromByteArray(CType(dvRow(AfAProduct.AFAProductSearchDV.COL_AFA_PRODUCT_ID), Byte()))
                If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem Then
                    CType(e.Row.Cells(GRID_COL_PRODUCT_ID_IDX).FindControl(GRID_CTRL_NAME_LABEL_PRODUCT_ID), Label).Text = strID

                    If (State.IsEditMode = True AndAlso State.productID.ToString.Equals(strID)) Then

                        CType(e.Row.Cells(GRID_COL_CODE_IDX).FindControl(GRID_CTRL_NAME_EDIT_CODE), TextBox).Text = dvRow(AfAProduct.AFAProductSearchDV.COL_CODE).ToString

                        objDealer = New Dealer(State.DealerIdInSearch)
                        PopulateControlFromBOProperty(dealerEditLabel, objDealer.Dealer)
                        CType(e.Row.Cells(GRID_COL_DEALER_CODE_IDX).FindControl(GRID_CTRL_NAME_EDIT_DEALER), Label).Visible = True

                        CType(e.Row.Cells(GRID_COL_DESC_IDX).FindControl(GRID_CTRL_NAME_EDIT_DESC), TextBox).Text = dvRow(AfAProduct.AFAProductSearchDV.COL_DESCRIPTION).ToString

                        'PRODUCT_TYPE
                        PopulateProductTypeDropdown(ddlProductType)
                        If (State.IsGridAddNew) OrElse (dvRow(AfAProduct.AFAProductSearchDV.COL_PRODUCT_TYPE) Is DBNull.Value) Then
                            ddlProductType.SelectedIndex = NO_ITEM_SELECTED_INDEX
                        Else
                            SetSelectedItem(ddlProductType, dvRow(AfAProduct.AFAProductSearchDV.COL_PRODUCT_TYPE).ToString)
                        End If

                    Else
                        dealerId = New Guid(CType(dvRow(AfAProduct.AFAProductSearchDV.COL_DEALER_ID), Byte()))
                        objDealer = New Dealer(dealerId)
                        PopulateControlFromBOProperty(dealerLabel, objDealer.Dealer)

                        If (e.Row.Cells(GRID_COL_CODE_IDX).FindControl(GRID_CTRL_NAME_LINKBTN_CODE) IsNot Nothing) Then
                            btnEditItem = CType(e.Row.Cells(GRID_COL_CODE_IDX).FindControl(GRID_CTRL_NAME_LINKBTN_CODE), LinkButton)
                            btnEditItem.CommandArgument = GetGuidStringFromByteArray(CType(dvRow(AfAProduct.AFAProductSearchDV.COL_AFA_PRODUCT_ID), Byte()))
                            btnEditItem.CommandName = EDIT_INVRATE_COMMAND
                            btnEditItem.Text = dvRow(AfAProduct.AFAProductSearchDV.COL_CODE).ToString
                        End If

                        CType(e.Row.Cells(GRID_COL_DESC_IDX).FindControl(GRID_CTRL_NAME_LABEL_DESC), Label).Text = dvRow(AfAProduct.AFAProductSearchDV.COL_DESCRIPTION).ToString

                        CType(e.Row.Cells(GRID_COL_PROD_TYPE_IDX).FindControl(GRID_CTRL_NAME_LABEL_PROD_TYPE), Label).Text =
                            LookupListNew.GetDescrionFromListCode(LookupListNew.LK_AFA_PRODUCT_TYPE, dvRow(AfAProduct.AFAProductSearchDV.COL_PRODUCT_TYPE).ToString)

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

                State.productID = New Guid(CType(Grid.Rows(index).Cells(GRID_COL_PRODUCT_ID_IDX).FindControl(GRID_CTRL_NAME_LABEL_PRODUCT_ID), Label).Text)
                State.MyBO = New AfAProduct(State.productID)

                PopulateGrid()

                State.PageIndex = Grid.PageIndex

                SetControlState()

            ElseIf e.CommandName = EDIT_INVRATE_COMMAND Then
                If Not e.CommandArgument.ToString().Equals(String.Empty) Then
                    State.selectedAFAProductId = New Guid(e.CommandArgument.ToString())
                    callPage(InvoiceRateForm.URL, State.selectedAFAProductId)
                End If

            ElseIf (e.CommandName = DELETE_COMMAND) Then
                index = CInt(e.CommandArgument)
                State.productID = New Guid(CType(Grid.Rows(index).Cells(GRID_COL_PRODUCT_ID_IDX).FindControl(GRID_CTRL_NAME_LABEL_PRODUCT_ID), Label).Text)
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

    Protected Sub btnClearSearch_Click(sender As Object, e As EventArgs) Handles btnClearSearch.Click
        Try

            ddlDealer.SelectedIndex = NO_ROW_SELECTED_INDEX
            txtProductCode.Text = String.Empty
            Grid.EditIndex = NO_ITEM_SELECTED_INDEX

            With State
                .IsGridAddNew = False
                .productID = Guid.Empty
                .MyBO = Nothing
                .DealerIdInSearch = Guid.Empty
                .searchProdCode = String.Empty
            End With
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            If GetSelectedItem(ddlDealer) = Guid.Empty Then
                MasterPage.MessageController.AddInformation(Message.MSG_DEALER_REQUIRED)
                Exit Sub
            End If
            With State
                .PageIndex = 0
                .productID = Guid.Empty
                .MyBO = Nothing
                .IsGridVisible = True
                .searchDV = Nothing
                .HasDataChanged = False
                .IsGridAddNew = False
                'get search control value
                .DealerIdInSearch = GetSelectedItem(ddlDealer)
                .searchProdCode = txtProductCode.Text.Trim
                State.selectedAFAProductId = Guid.Empty
            End With
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub ddlDealer_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlDealer.SelectedIndexChanged
        State.DealerIdInSearch = GetSelectedItem(ddlDealer)
        If State.DealerIdInSearch.Equals(Guid.Empty) Then
            If State.searchDV IsNot Nothing Then
                State.searchDV = Nothing
                PopulateGrid()
            End If
        End If
    End Sub

    Protected Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click

        Try
            If State.DealerIdInSearch.Equals(Guid.Empty) Then
                MasterPage.MessageController.AddInformation(Message.MSG_DEALER_REQUIRED)
                Return
            End If
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
        If State.MyBO Is Nothing OrElse State.MyBO.IsNew = False Then
            State.MyBO = New AfAProduct
            State.MyBO.AddNewRowToSearchDV(State.searchDV, State.MyBO)
        End If
        State.productID = State.MyBO.Id
        State.selectedAFAProductId = Guid.Empty
        State.IsGridAddNew = True
        PopulateGrid()
        'Set focus on the Code TextBox for the EditItemIndex row
        SetPageAndSelectedIndexFromGuid(State.searchDV, State.productID, Grid,
        State.PageIndex, State.IsEditMode)
        ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Grid)
        SetGridControls(Grid, False)
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)

        Try
            PopulateBOFromForm()
            If (State.MyBO.IsDirty) Then
                State.MyBO.Save()
                State.IsAfterSave = True
                State.IsGridAddNew = False
                MasterPage.MessageController.AddSuccess(MSG_RECORD_SAVED_OK, True)
                State.searchDV = Nothing
                State.MyBO = Nothing
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
                    .searchDV = Nothing
                End If
                .productID = Guid.Empty
                State.MyBO = Nothing
                .IsEditMode = False
                .ActionInProgress = DetailPageCommand.Cancel
            End With
            Grid.EditIndex = NO_ITEM_SELECTED_INDEX

            PopulateGrid()

            SetControlState()
            State.ActionInProgress = DetailPageCommand.Nothing_
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
#End Region


End Class