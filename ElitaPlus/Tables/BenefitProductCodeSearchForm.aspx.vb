Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Public Class BenefitProductCodeSearchForm
    Inherits ElitaPlusSearchPage

#Region "Page State"
    ' This class keeps the current state for the search page.
    Class MyState
        Public IsGridVisible As Boolean = False
        Public searchDV As BenefitProductCode.BenefitProductCodeSearchDV = Nothing

        Public SortExpression As String = BenefitProductCode.BenefitProductCodeSearchDV.COL_BENEFIT_PRODUCT_CODE
        Public PageIndex As Integer = 0
        Public PageSort As String
        Public PageSize As Integer = 15
        Public SearchDataView As BenefitProductCode.BenefitProductCodeSearchDV
        Public BenefitProductCodeId As Guid = Guid.Empty

        Public ProductCodeMask As String
        Public DealerId As Guid = Guid.Empty
        Public bnoRow As Boolean = False
    End Class

    Public Sub New()
        MyBase.New(New MyState)
    End Sub

    Protected Shadows ReadOnly Property State() As MyState
        Get
            Return CType(MyBase.State, MyState)
        End Get
    End Property
#End Region

#Region "Constants"

    Public Const URL As String = "BenefitProductCodeForm.aspx"

    Private Const DEALER_TYPE_BENEFIT As String = "BENEFIT"
    Private Const PRODUCTCODE_LIST_FORM001 As String = "PRODUCTCODE_LIST_FORM001" ' Maintain Product Code List Exception
    Private Const LABEL_DEALER As String = "DEALER"
    Private Const LABEL_BENEFIT_PRODUCT_CODE As String = "BENEFIT_PRODUCT_CODE"
#End Region

#Region "Constants-DataGrid"
    Public Const NO_ITEM_SELECTED_INDEX As Integer = -1
    Public Const NO_PAGE_INDEX As Integer = 0
    Public Const BLANK_ITEM_SELECTED As Integer = 0
#End Region

#Region "Grid Column"
    Public Enum GridDefenitionEnum
        'EditIdx = 0
        BenefitProductCode = 0
        DealerName
        Description
        VendorName
        EffectiveDate
        ExpirationDate
        BenefitProductCodeIdx
    End Enum
#End Region

#Region "Page Navigation"
    Private IsReturningFromChild As Boolean = False

    Public Class ReturnType
        Public LastOperation As ElitaPlusPage.DetailPageCommand
        Public BenefitProductCodeId As Guid
        Public BoChanged As Boolean = False

        Public Sub New(ByVal LastOp As ElitaPlusPage.DetailPageCommand, ByVal benefitProductCodeId As Guid, Optional ByVal boChanged As Boolean = False)
            Me.LastOperation = LastOp
            Me.BenefitProductCodeId = benefitProductCodeId
            Me.BoChanged = boChanged
        End Sub

    End Class

    Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles Me.PageReturn
        Try
            Me.IsReturningFromChild = True
            Dim retObj As ReturnType = CType(ReturnPar, ReturnType)
            If Not retObj Is Nothing AndAlso retObj.BoChanged Then
                Me.State.searchDV = Nothing
            End If
            If Not retObj Is Nothing Then
                Select Case retObj.LastOperation
                    Case ElitaPlusPage.DetailPageCommand.Back
                        Me.State.BenefitProductCodeId = retObj.BenefitProductCodeId
                        Me.State.IsGridVisible = True
                    Case ElitaPlusPage.DetailPageCommand.Delete
                        Me.AddInfoMsg(Message.DELETE_RECORD_CONFIRMATION)
                        Me.State.BenefitProductCodeId = Guid.Empty
                    Case Else
                        Me.State.BenefitProductCodeId = Guid.Empty
                End Select
                Grid.PageIndex = Me.State.PageIndex
                cboPageSize.SelectedValue = CType(Me.State.PageSize, String)
                Grid.PageSize = Me.State.PageSize
                ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub


#End Region

#Region "Page Events"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Put user code to initialize the page here
        Try
            Me.MasterPage.MessageController.Clear_Hide()

            If Not Page.IsPostBack Then

                Me.MasterPage.MessageController.Clear()
                Me.MasterPage.UsePageTabTitleInBreadCrum = False
                Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Tables")
                UpdateBreadCrum()

                TranslateGridHeader(Grid)

                ControlMgr.SetVisibleControl(Me, trPageSize, False)
                Me.SortDirection = Me.State.SortExpression
                cboPageSize.SelectedValue = CType(Me.State.PageSize, String)
                PopulateDropdown()
                If Me.State.IsGridVisible Then
                    If Not (Me.State.PageSize = DEFAULT_NEW_UI_PAGE_SIZE) Or Not (State.PageSize = Grid.PageSize) Then
                        Grid.PageSize = Me.State.PageSize
                    End If
                    Me.PopulateGrid()
                End If
                Me.SetGridItemStyleColor(Me.Grid)
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
        Me.ShowMissingTranslations(Me.MasterPage.MessageController)
    End Sub
#End Region

#Region "Controlling Logic"

    Private Sub PopulateDropdown()
        PopulateDealer()
        PopulateProductCode()
    End Sub
    Private Sub UpdateBreadCrum()
        If (Not Me.State Is Nothing) Then
            If (Not Me.State Is Nothing) Then
                Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage(LABEL_BENEFIT_PRODUCT_CODE)
                Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(LABEL_BENEFIT_PRODUCT_CODE)
            End If
        End If
    End Sub


    Private Sub PopulateDealer()
        Try
            'GetDealerLookupListByDealerType
            'Dim oDataView As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)
            Dim oDataView As DataView = LookupListNew.GetDealerLookupListByDealerType(ElitaPlusIdentity.Current.ActiveUser.Companies, DEALER_TYPE_BENEFIT)
            DealerMultipleDrop.Caption = TranslationBase.TranslateLabelOrMessage(LABEL_DEALER)
            DealerMultipleDrop.NothingSelected = True
            DealerMultipleDrop.BindData(oDataView)
            DealerMultipleDrop.AutoPostBackDD = False
            DealerMultipleDrop.NothingSelected = True
            DealerMultipleDrop.SelectedGuid = Me.State.DealerId

        Catch ex As Exception
            Me.MasterPage.MessageController.AddError(PRODUCTCODE_LIST_FORM001)
            Me.MasterPage.MessageController.AddError(ex.Message, False)
            Me.MasterPage.MessageController.Show()
        End Try
    End Sub



    Private Sub PopulateProductCode()
        moBenefitProductCodeText.Text = Me.State.ProductCodeMask
    End Sub

    Private Sub PopulateGrid(Optional ByVal oAction As String = POPULATE_ACTION_NONE)

        Try
            If (Me.State.searchDV Is Nothing) Then
                Me.State.searchDV = BenefitProductCode.GetList(ElitaPlusIdentity.Current.ActiveUser.Companies,
                                                               DealerMultipleDrop.SelectedGuid,
                                                               Me.State.ProductCodeMask,
                                                               ElitaPlusIdentity.Current.ActiveUser.LanguageId)

            End If

            If (Me.State.searchDV.Count = 0) Then

                Me.State.bnoRow = True
                CreateHeaderForEmptyGrid(Grid, Me.SortDirection)
            Else
                Me.State.bnoRow = False
                Me.Grid.Enabled = True
            End If

            Me.State.searchDV.Sort = Me.State.SortExpression
            Grid.AutoGenerateColumns = False

            Grid.Columns(GridDefenitionEnum.DealerName).SortExpression = BenefitProductCode.BenefitProductCodeSearchDV.COL_DEALER_NAME
            Grid.Columns(GridDefenitionEnum.BenefitProductCode).SortExpression = BenefitProductCode.BenefitProductCodeSearchDV.COL_BENEFIT_PRODUCT_CODE
            Grid.Columns(GridDefenitionEnum.Description).SortExpression = BenefitProductCode.BenefitProductCodeSearchDV.COL_DESCRIPTION
            Grid.Columns(GridDefenitionEnum.EffectiveDate).SortExpression = BenefitProductCode.BenefitProductCodeSearchDV.COL_EFFECTIVE_DATE
            Grid.Columns(GridDefenitionEnum.ExpirationDate).SortExpression = BenefitProductCode.BenefitProductCodeSearchDV.COL_EXPIRATION_DATE
            HighLightSortColumn(Grid, Me.State.SortExpression)

            SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.DealerId, Me.Grid, Me.State.PageIndex)

            Me.Grid.DataSource = Me.State.searchDV
            HighLightSortColumn(Grid, Me.SortDirection)
            Me.Grid.DataBind()

            ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)

            Session("recCount") = Me.State.searchDV.Count

            If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True

            Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub ClearSearch()
        DealerMultipleDrop.SelectedIndex = 0
        moBenefitProductCodeText.Text = Nothing
        Me.State.BenefitProductCodeId = Guid.Empty
    End Sub
   
#End Region

#Region "Datagrid Related"

    Public Property SortDirection() As String
        Get
            Return ViewState("SortDirection").ToString
        End Get
        Set(ByVal value As String)
            ViewState("SortDirection") = value
        End Set
    End Property

    Private Sub Grid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            Grid.PageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
            Me.State.PageIndex = Grid.PageSize
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
        Try
            Me.State.PageIndex = e.NewPageIndex
            Me.State.DealerId = Guid.Empty
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting
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
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
    Public Sub RowCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        BaseItemCreated(sender, e)
    End Sub

    Private Sub Grid_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
        Try
            Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
            Dim btnEditItem As LinkButton
            If Not dvRow Is Nothing And Not Me.State.bnoRow Then
                If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                    btnEditItem = CType(e.Row.Cells(GridDefenitionEnum.BenefitProductCode).FindControl("SelectAction"), LinkButton)
                    btnEditItem.Text = dvRow(BenefitProductCode.BenefitProductCodeSearchDV.COL_BENEFIT_PRODUCT_CODE).ToString
                    e.Row.Cells(GridDefenitionEnum.DealerName).Text = dvRow(BenefitProductCode.BenefitProductCodeSearchDV.COL_DEALER_NAME).ToString
                    e.Row.Cells(GridDefenitionEnum.Description).Text = dvRow(BenefitProductCode.BenefitProductCodeSearchDV.COL_DESCRIPTION).ToString
                    e.Row.Cells(GridDefenitionEnum.VendorName).Text = dvRow(BenefitProductCode.BenefitProductCodeSearchDV.COL_VENDOR_NAME).ToString
                    e.Row.Cells(GridDefenitionEnum.BenefitProductCodeIdx).Text = GetGuidStringFromByteArray(CType(dvRow(BenefitProductCode.BenefitProductCodeSearchDV.COL_BENEFIT_PRODUCT_CODE_ID), Byte()))
                    e.Row.Cells(GridDefenitionEnum.EffectiveDate).Text = GetDateFormattedString(CType(dvRow.Row(BenefitProductCode.BenefitProductCodeSearchDV.COL_EFFECTIVE_DATE), Date))
                    e.Row.Cells(GridDefenitionEnum.ExpirationDate).Text = GetDateFormattedString(CType(dvRow.Row(BenefitProductCode.BenefitProductCodeSearchDV.COL_EXPIRATION_DATE), Date))
                End If
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
    Public Sub RowCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Try
            If e.CommandName = "SelectAction" Then
                Dim index As Integer = CInt(e.CommandArgument)
                Me.State.BenefitProductCodeId = New Guid(Me.Grid.Rows(index).Cells(GridDefenitionEnum.BenefitProductCodeIdx).Text)
                Me.callPage(BenefitProductCodeSearchForm.URL, Me.State.BenefitProductCodeId)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub
#End Region

#Region " Button Clicks "
    Private Sub moBtnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles moBtnSearch.Click
        Try
            ' Dim oState As TheState
            If Not State.IsGridVisible Then
                cboPageSize.SelectedValue = CType(State.PageSize, String)
                If Not (Grid.PageSize = DEFAULT_NEW_UI_PAGE_SIZE) Then
                    cboPageSize.SelectedValue = CType(State.PageSize, String)
                    Grid.PageSize = State.PageSize
                End If
                Me.State.IsGridVisible = True
            End If
            Grid.PageIndex = NO_PAGE_INDEX
            Grid.DataMember = Nothing
            Me.State.searchDV = Nothing
            SetSession()
            Grid.PageIndex = NO_PAGE_INDEX
            Grid.DataMember = Nothing
            Me.State.searchDV = Nothing
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub moBtnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles moBtnClear.Click
        Try
            ClearSearch()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnNew_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew_WRITE.Click
        Try
            Me.State.BenefitProductCodeId = Guid.Empty
            SetSession()

            Me.callPage(BenefitProductCodeForm.URL, Me.State.BenefitProductCodeId)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
#End Region
#Region "State-Management"

    Private Sub SetSession()
        With Me.State
            .ProductCodeMask = moBenefitProductCodeText.Text.ToUpper
            .DealerId = DealerMultipleDrop.SelectedGuid
            .PageIndex = Grid.PageIndex
            .PageSize = Grid.PageSize
            .PageSort = Me.State.SortExpression
            .SearchDataView = Me.State.searchDV
        End With
    End Sub


#End Region
End Class