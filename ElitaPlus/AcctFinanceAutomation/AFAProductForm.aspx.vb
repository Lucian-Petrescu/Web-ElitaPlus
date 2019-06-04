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
        Me.MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(PAGETAB)
        Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(SUMMARYTITLE)
        Me.MasterPage.UsePageTabTitleInBreadCrum = False
        Me.MasterPage.BreadCrum = TranslationBase.TranslateLabelOrMessage(PAGETAB) + ElitaBase.Sperator + TranslationBase.TranslateLabelOrMessage(PAGETITLE)
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
            Return Me.Grid.EditIndex > Me.NO_ITEM_SELECTED_INDEX
        End Get
    End Property


#End Region

#Region "Page Events"

    Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
        Try
            'Me.MenuEnabled = True
            Me.IsReturningFromChild = True
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.MasterPage.MessageController.Clear()
        Me.Form.DefaultButton = btnSearch.UniqueID
        Try
            If Not Me.IsPostBack Then
                UpdateBreadCrum()
                ControlMgr.SetVisibleControl(Me, moSearchResults, False)

                Me.State.PageIndex = 0
                Me.TranslateGridHeader(Grid)
                Me.TranslateGridControls(Grid)
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
                If Me.IsReturningFromChild Then
                    PopulateSearchFieldsFromState()
                    Me.State.searchDV = Nothing
                    Me.PopulateGrid()
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
                If Not oDealerList Is Nothing Then
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
        Me.SetSelectedItem(ddlDealer, Me.State.DealerIdInSearch)
        txtProductCode.Text = Me.State.searchProdCode
    End Sub

    Public Sub PopulateProductTypeDropdown(ByVal ddlProdType As DropDownList)

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
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

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
        'Save the RiskTypeId in the Session

        Dim obj As AfAProduct = New AfAProduct(Me.State.productID)

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
            ControlMgr.SetEnableControl(Me, btnSearch, False)
            ControlMgr.SetEnableControl(Me, btnClearSearch, False)
            Me.MenuEnabled = False
            If (Me.cboPageSize.Enabled) Then
                ControlMgr.SetEnableControl(Me, cboPageSize, False)
            End If
        Else
            ControlMgr.SetVisibleControl(Me, btnNew, True)
            ControlMgr.SetEnableControl(Me, btnSearch, True)
            ControlMgr.SetEnableControl(Me, btnClearSearch, True)
            Me.MenuEnabled = True
            If Not (Me.cboPageSize.Enabled) Then
                ControlMgr.SetEnableControl(Me, Me.cboPageSize, True)
            End If
        End If
    End Sub

    Protected Sub BindBoPropertiesToGridHeaders()
        Me.BindBOPropertyToGridHeader(Me.State.MyBO, "DealerId", Me.Grid.Columns(Me.GRID_COL_DEALER_CODE_IDX))
        Me.BindBOPropertyToGridHeader(Me.State.MyBO, "Code", Me.Grid.Columns(Me.GRID_COL_CODE_IDX))
        Me.BindBOPropertyToGridHeader(Me.State.MyBO, "Description", Me.Grid.Columns(Me.GRID_COL_DESC_IDX))
        Me.BindBOPropertyToGridHeader(Me.State.MyBO, "ProductType", Me.Grid.Columns(Me.GRID_COL_PROD_TYPE_IDX))
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
                rowind = FindSelectedRowIndexFromGuid(.searchDV, .productID)
            End If
        End With
        If rowind <> NO_ITEM_SELECTED_INDEX Then State.searchDV.Delete(rowind)
    End Sub

    Private Function PopulateBOFromForm() As Boolean
        Dim objtxt As TextBox
        Dim ddlDealer As DropDownList
        Dim ddlProdType As DropDownList
        With Me.State.MyBO

            State.MyBO.DealerId = State.DealerIdInSearch

            objtxt = CType(Grid.Rows((Me.Grid.EditIndex)).Cells(GRID_COL_CODE_IDX).FindControl(GRID_CTRL_NAME_EDIT_CODE), TextBox)
            objtxt.Text = objtxt.Text.ToUpper
            PopulateBOProperty(State.MyBO, "Code", objtxt)

            objtxt = CType(Grid.Rows((Me.Grid.EditIndex)).Cells(GRID_COL_DESC_IDX).FindControl(GRID_CTRL_NAME_EDIT_DESC), TextBox)
            PopulateBOProperty(State.MyBO, "Description", objtxt)

            ddlProdType = CType(Grid.Rows((Me.Grid.EditIndex)).Cells(GRID_COL_PROD_TYPE_IDX).FindControl(GRID_CTRL_NAME_EDIT_PROD_TYPE_LIST), DropDownList)
            If ddlProdType.SelectedItem.Text = String.Empty Then
                State.MyBO.ProductType = String.Empty
            Else
                PopulateBOProperty(State.MyBO, "ProductType", ddlProdType, False, True)
            End If

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
                If (.searchDV Is Nothing) AndAlso Not Me.State.IsGridAddNew = True Then
                    .searchDV = AfAProduct.getList(.DealerIdInSearch, .searchProdCode)
                    blnNewSearch = True
                End If
            End With

            If Not State.searchDV Is Nothing Then
                Me.State.searchDV.Sort = Me.State.SortExpression
                If (Me.State.IsAfterSave) Then
                    Me.State.IsAfterSave = False
                    Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.productID, Me.Grid, Me.State.PageIndex)
                ElseIf (Me.State.IsEditMode) Then
                    Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.productID, Me.Grid, Me.State.PageIndex, Me.State.IsEditMode)
                Else
                    Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Guid.Empty, Me.Grid, Me.State.PageIndex)
                End If
            End If

            Me.Grid.AutoGenerateColumns = False
            Me.Grid.Columns(Me.GRID_COL_CODE_IDX).SortExpression = AfAProduct.AFAProductSearchDV.COL_CODE
            Me.Grid.Columns(Me.GRID_COL_DESC_IDX).SortExpression = AfAProduct.AFAProductSearchDV.COL_DESCRIPTION
            SortAndBindGrid(blnNewSearch)

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

    Private Sub SortAndBindGrid(Optional ByVal blnShowErr As Boolean = True)

        Me.TranslateGridControls(Grid)

        If Not State.searchDV Is Nothing AndAlso (Me.State.searchDV.Count = 0) Then
            Me.State.searchDV = Nothing
            Me.State.MyBO = New AfAProduct
            State.MyBO.AddNewRowToSearchDV(Me.State.searchDV, Me.State.MyBO)
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
            HighLightSortColumn(Grid, Me.State.SortExpression)
            Me.Grid.DataBind()
        End If

        ControlMgr.SetVisibleControl(Me, Grid, Me.State.IsGridVisible)
        ControlMgr.SetVisibleControl(Me, moSearchResults, Me.State.IsGridVisible)

        If Not State.searchDV Is Nothing Then
            Session("recCount") = Me.State.searchDV.Count

            If Me.Grid.Visible Then
                If (Me.State.IsGridAddNew) Then
                    Me.lblRecordCount.Text = (Me.State.searchDV.Count - 1) & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                Else
                    Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            End If
        End If

        ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Grid)

    End Sub

    Private Sub Grid_PageIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grid.PageIndexChanged
        Try
            If (Not (Me.State.IsEditMode)) Then
                Me.State.PageIndex = Grid.PageIndex
                Me.State.productID = Guid.Empty
                Me.State.selectedAFAProductId = Guid.Empty
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
            Dim spaceIndex As Integer = Me.State.SortExpression.LastIndexOf(" ")


            If spaceIndex > 0 AndAlso Me.State.SortExpression.Substring(0, spaceIndex).Equals(e.SortExpression) Then
                If Me.State.SortExpression.EndsWith(" ASC") Then
                    Me.State.SortExpression = e.SortExpression + " DESC"
                Else
                    Me.State.SortExpression = e.SortExpression + " ASC"
                End If
            Else
                Me.State.SortExpression = e.SortExpression + " ASC"
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
            Dim dealerLabel As Label = CType(e.Row.FindControl(Me.GRID_CTRL_NAME_LABEL_DEALER), Label)
            Dim dealerEditLabel As Label = CType(e.Row.FindControl(Me.GRID_CTRL_NAME_EDIT_DEALER), Label)
            Dim ddlProductType As DropDownList = CType(e.Row.FindControl(GRID_CTRL_NAME_EDIT_PROD_TYPE_LIST), DropDownList)

            Dim dealerId As Guid
            Dim objDealer As Dealer
            Dim btnEditItem As LinkButton

            If Not dvRow Is Nothing Then
                strID = GetGuidStringFromByteArray(CType(dvRow(AfAProduct.AFAProductSearchDV.COL_AFA_PRODUCT_ID), Byte()))
                If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                    CType(e.Row.Cells(Me.GRID_COL_PRODUCT_ID_IDX).FindControl(Me.GRID_CTRL_NAME_LABEL_PRODUCT_ID), Label).Text = strID

                    If (Me.State.IsEditMode = True AndAlso Me.State.productID.ToString.Equals(strID)) Then

                        CType(e.Row.Cells(Me.GRID_COL_CODE_IDX).FindControl(Me.GRID_CTRL_NAME_EDIT_CODE), TextBox).Text = dvRow(AfAProduct.AFAProductSearchDV.COL_CODE).ToString

                        objDealer = New Dealer(Me.State.DealerIdInSearch)
                        Me.PopulateControlFromBOProperty(dealerEditLabel, objDealer.Dealer)
                        CType(e.Row.Cells(Me.GRID_COL_DEALER_CODE_IDX).FindControl(Me.GRID_CTRL_NAME_EDIT_DEALER), Label).Visible = True

                        CType(e.Row.Cells(Me.GRID_COL_DESC_IDX).FindControl(Me.GRID_CTRL_NAME_EDIT_DESC), TextBox).Text = dvRow(AfAProduct.AFAProductSearchDV.COL_DESCRIPTION).ToString

                        'PRODUCT_TYPE
                        PopulateProductTypeDropdown(ddlProductType)
                        If (Me.State.IsGridAddNew) OrElse (dvRow(AfAProduct.AFAProductSearchDV.COL_PRODUCT_TYPE) Is DBNull.Value) Then
                            ddlProductType.SelectedIndex = Me.NO_ITEM_SELECTED_INDEX
                        Else
                            Me.SetSelectedItem(ddlProductType, dvRow(AfAProduct.AFAProductSearchDV.COL_PRODUCT_TYPE).ToString)
                        End If

                    Else
                        dealerId = New Guid(CType(dvRow(AfAProduct.AFAProductSearchDV.COL_DEALER_ID), Byte()))
                        objDealer = New Dealer(dealerId)
                        Me.PopulateControlFromBOProperty(dealerLabel, objDealer.Dealer)

                        If (Not e.Row.Cells(Me.GRID_COL_CODE_IDX).FindControl(Me.GRID_CTRL_NAME_LINKBTN_CODE) Is Nothing) Then
                            btnEditItem = CType(e.Row.Cells(Me.GRID_COL_CODE_IDX).FindControl(GRID_CTRL_NAME_LINKBTN_CODE), LinkButton)
                            btnEditItem.CommandArgument = GetGuidStringFromByteArray(CType(dvRow(AfAProduct.AFAProductSearchDV.COL_AFA_PRODUCT_ID), Byte()))
                            btnEditItem.CommandName = EDIT_INVRATE_COMMAND
                            btnEditItem.Text = dvRow(AfAProduct.AFAProductSearchDV.COL_CODE).ToString
                        End If

                        CType(e.Row.Cells(Me.GRID_COL_DESC_IDX).FindControl(Me.GRID_CTRL_NAME_LABEL_DESC), Label).Text = dvRow(AfAProduct.AFAProductSearchDV.COL_DESCRIPTION).ToString

                        CType(e.Row.Cells(Me.GRID_COL_PROD_TYPE_IDX).FindControl(Me.GRID_CTRL_NAME_LABEL_PROD_TYPE), Label).Text =
                            LookupListNew.GetDescrionFromListCode(LookupListNew.LK_AFA_PRODUCT_TYPE, dvRow(AfAProduct.AFAProductSearchDV.COL_PRODUCT_TYPE).ToString)

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

                Me.State.productID = New Guid(CType(Me.Grid.Rows(index).Cells(Me.GRID_COL_PRODUCT_ID_IDX).FindControl(Me.GRID_CTRL_NAME_LABEL_PRODUCT_ID), Label).Text)
                Me.State.MyBO = New AfAProduct(Me.State.productID)

                Me.PopulateGrid()

                Me.State.PageIndex = Grid.PageIndex

                Me.SetControlState()

            ElseIf e.CommandName = EDIT_INVRATE_COMMAND Then
                If Not e.CommandArgument.ToString().Equals(String.Empty) Then
                    Me.State.selectedAFAProductId = New Guid(e.CommandArgument.ToString())
                    Me.callPage(InvoiceRateForm.URL, Me.State.selectedAFAProductId)
                End If

            ElseIf (e.CommandName = Me.DELETE_COMMAND) Then
                index = CInt(e.CommandArgument)
                Me.State.productID = New Guid(CType(Me.Grid.Rows(index).Cells(Me.GRID_COL_PRODUCT_ID_IDX).FindControl(Me.GRID_CTRL_NAME_LABEL_PRODUCT_ID), Label).Text)
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

    Protected Sub btnClearSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnClearSearch.Click
        Try

            ddlDealer.SelectedIndex = Me.NO_ROW_SELECTED_INDEX
            Me.txtProductCode.Text = String.Empty
            Grid.EditIndex = Me.NO_ITEM_SELECTED_INDEX

            With State
                .IsGridAddNew = False
                .productID = Guid.Empty
                .MyBO = Nothing
                .DealerIdInSearch = Guid.Empty
                .searchProdCode = String.Empty
            End With
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        Try
            If Me.GetSelectedItem(ddlDealer) = Guid.Empty Then
                Me.MasterPage.MessageController.AddInformation(Message.MSG_DEALER_REQUIRED)
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
                .DealerIdInSearch = Me.GetSelectedItem(ddlDealer)
                .searchProdCode = txtProductCode.Text.Trim
                Me.State.selectedAFAProductId = Guid.Empty
            End With
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub ddlDealer_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDealer.SelectedIndexChanged
        Me.State.DealerIdInSearch = Me.GetSelectedItem(ddlDealer)
        If Me.State.DealerIdInSearch.Equals(Guid.Empty) Then
            If Not Me.State.searchDV Is Nothing Then
                Me.State.searchDV = Nothing
                PopulateGrid()
            End If
        End If
    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnNew.Click

        Try
            If Me.State.DealerIdInSearch.Equals(Guid.Empty) Then
                Me.MasterPage.MessageController.AddInformation(Message.MSG_DEALER_REQUIRED)
                Return
            End If
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
        If Me.State.MyBO Is Nothing OrElse Me.State.MyBO.IsNew = False Then
            Me.State.MyBO = New AfAProduct
            State.MyBO.AddNewRowToSearchDV(Me.State.searchDV, Me.State.MyBO)
        End If
        Me.State.productID = Me.State.MyBO.Id
        Me.State.selectedAFAProductId = Guid.Empty
        State.IsGridAddNew = True
        PopulateGrid()
        'Set focus on the Code TextBox for the EditItemIndex row
        Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.productID, Me.Grid,
        Me.State.PageIndex, Me.State.IsEditMode)
        ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Grid)
        SetGridControls(Me.Grid, False)
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs)

        Try
            PopulateBOFromForm()
            If (Me.State.MyBO.IsDirty) Then
                Me.State.MyBO.Save()
                Me.State.IsAfterSave = True
                Me.State.IsGridAddNew = False
                Me.MasterPage.MessageController.AddSuccess(Me.MSG_RECORD_SAVED_OK, True)
                Me.State.searchDV = Nothing
                Me.State.MyBO = Nothing
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
                    .searchDV = Nothing
                End If
                .productID = Guid.Empty
                Me.State.MyBO = Nothing
                .IsEditMode = False
                .ActionInProgress = DetailPageCommand.Cancel
            End With
            Grid.EditIndex = NO_ITEM_SELECTED_INDEX

            PopulateGrid()

            SetControlState()
            Me.State.ActionInProgress = DetailPageCommand.Nothing_
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
#End Region


End Class