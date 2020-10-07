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

        Public Sub New(LastOp As ElitaPlusPage.DetailPageCommand, benefitProductCodeId As Guid, Optional ByVal boChanged As Boolean = False)
            LastOperation = LastOp
            Me.BenefitProductCodeId = benefitProductCodeId
            Me.BoChanged = boChanged
        End Sub

    End Class

    Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles Me.PageReturn
        Try
            IsReturningFromChild = True
            Dim retObj As ReturnType = CType(ReturnPar, ReturnType)
            If retObj IsNot Nothing AndAlso retObj.BoChanged Then
                State.searchDV = Nothing
            End If
            If retObj IsNot Nothing Then
                Select Case retObj.LastOperation
                    Case ElitaPlusPage.DetailPageCommand.Back
                        State.BenefitProductCodeId = retObj.BenefitProductCodeId
                        State.IsGridVisible = True
                    Case ElitaPlusPage.DetailPageCommand.Delete
                        AddInfoMsg(Message.DELETE_RECORD_CONFIRMATION)
                        State.BenefitProductCodeId = Guid.Empty
                    Case Else
                        State.BenefitProductCodeId = Guid.Empty
                End Select
                Grid.PageIndex = State.PageIndex
                cboPageSize.SelectedValue = CType(State.PageSize, String)
                Grid.PageSize = State.PageSize
                ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)
            End If

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub


#End Region

#Region "Page Events"
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        'Put user code to initialize the page here
        Try
            MasterPage.MessageController.Clear_Hide()

            If Not Page.IsPostBack Then

                MasterPage.MessageController.Clear()
                MasterPage.UsePageTabTitleInBreadCrum = False
                MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Tables")
                UpdateBreadCrum()

                TranslateGridHeader(Grid)

                ControlMgr.SetVisibleControl(Me, trPageSize, False)
                SortDirection = State.SortExpression
                cboPageSize.SelectedValue = CType(State.PageSize, String)
                PopulateDropdown()
                If State.IsGridVisible Then
                    If Not (State.PageSize = DEFAULT_NEW_UI_PAGE_SIZE) OrElse Not (State.PageSize = Grid.PageSize) Then
                        Grid.PageSize = State.PageSize
                    End If
                    PopulateGrid()
                End If
                SetGridItemStyleColor(Grid)
            End If

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
        ShowMissingTranslations(MasterPage.MessageController)
    End Sub
#End Region

#Region "Controlling Logic"

    Private Sub PopulateDropdown()
        PopulateDealer()
        PopulateProductCode()
    End Sub
    Private Sub UpdateBreadCrum()
        If (State IsNot Nothing) Then
            If (State IsNot Nothing) Then
                MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage(LABEL_BENEFIT_PRODUCT_CODE)
                MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(LABEL_BENEFIT_PRODUCT_CODE)
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
            DealerMultipleDrop.SelectedGuid = State.DealerId

        Catch ex As Exception
            MasterPage.MessageController.AddError(PRODUCTCODE_LIST_FORM001)
            MasterPage.MessageController.AddError(ex.Message, False)
            MasterPage.MessageController.Show()
        End Try
    End Sub



    Private Sub PopulateProductCode()
        moBenefitProductCodeText.Text = State.ProductCodeMask
    End Sub

    Private Sub PopulateGrid(Optional ByVal oAction As String = POPULATE_ACTION_NONE)

        Try
            If (State.searchDV Is Nothing) Then
                State.searchDV = BenefitProductCode.GetList(ElitaPlusIdentity.Current.ActiveUser.Companies,
                                                               DealerMultipleDrop.SelectedGuid,
                                                               State.ProductCodeMask,
                                                               ElitaPlusIdentity.Current.ActiveUser.LanguageId)

            End If

            If (State.searchDV.Count = 0) Then

                State.bnoRow = True
                CreateHeaderForEmptyGrid(Grid, SortDirection)
            Else
                State.bnoRow = False
                Grid.Enabled = True
            End If

            State.searchDV.Sort = State.SortExpression
            Grid.AutoGenerateColumns = False

            Grid.Columns(GridDefenitionEnum.DealerName).SortExpression = BenefitProductCode.BenefitProductCodeSearchDV.COL_DEALER_NAME
            Grid.Columns(GridDefenitionEnum.BenefitProductCode).SortExpression = BenefitProductCode.BenefitProductCodeSearchDV.COL_BENEFIT_PRODUCT_CODE
            Grid.Columns(GridDefenitionEnum.Description).SortExpression = BenefitProductCode.BenefitProductCodeSearchDV.COL_DESCRIPTION
            Grid.Columns(GridDefenitionEnum.EffectiveDate).SortExpression = BenefitProductCode.BenefitProductCodeSearchDV.COL_EFFECTIVE_DATE
            Grid.Columns(GridDefenitionEnum.ExpirationDate).SortExpression = BenefitProductCode.BenefitProductCodeSearchDV.COL_EXPIRATION_DATE
            HighLightSortColumn(Grid, State.SortExpression)

            SetPageAndSelectedIndexFromGuid(State.searchDV, State.DealerId, Grid, State.PageIndex)

            Grid.DataSource = State.searchDV
            HighLightSortColumn(Grid, SortDirection)
            Grid.DataBind()

            ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)

            Session("recCount") = State.searchDV.Count

            If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True

            lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub ClearSearch()
        DealerMultipleDrop.SelectedIndex = 0
        moBenefitProductCodeText.Text = Nothing
        State.BenefitProductCodeId = Guid.Empty
    End Sub
   
#End Region

#Region "Datagrid Related"

    Public Property SortDirection() As String
        Get
            Return ViewState("SortDirection").ToString
        End Get
        Set(value As String)
            ViewState("SortDirection") = value
        End Set
    End Property

    Private Sub Grid_PageSizeChanged(source As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            Grid.PageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
            State.PageIndex = Grid.PageSize
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
        Try
            State.PageIndex = e.NewPageIndex
            State.DealerId = Guid.Empty
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_SortCommand(source As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting
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
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Public Sub RowCreated(sender As System.Object, e As System.Web.UI.WebControls.GridViewRowEventArgs)
        BaseItemCreated(sender, e)
    End Sub

    Private Sub Grid_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
        Try
            Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
            Dim btnEditItem As LinkButton
            If dvRow IsNot Nothing AndAlso Not State.bnoRow Then
                If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem Then
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
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Public Sub RowCommand(source As System.Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Try
            If e.CommandName = "SelectAction" Then
                Dim index As Integer = CInt(e.CommandArgument)
                State.BenefitProductCodeId = New Guid(Grid.Rows(index).Cells(GridDefenitionEnum.BenefitProductCodeIdx).Text)
                callPage(BenefitProductCodeSearchForm.URL, State.BenefitProductCodeId)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub
#End Region

#Region " Button Clicks "
    Private Sub moBtnSearch_Click(sender As System.Object, e As System.EventArgs) Handles moBtnSearch.Click
        Try
            ' Dim oState As TheState
            If Not State.IsGridVisible Then
                cboPageSize.SelectedValue = CType(State.PageSize, String)
                If Not (Grid.PageSize = DEFAULT_NEW_UI_PAGE_SIZE) Then
                    cboPageSize.SelectedValue = CType(State.PageSize, String)
                    Grid.PageSize = State.PageSize
                End If
                State.IsGridVisible = True
            End If
            Grid.PageIndex = NO_PAGE_INDEX
            Grid.DataMember = Nothing
            State.searchDV = Nothing
            SetSession()
            Grid.PageIndex = NO_PAGE_INDEX
            Grid.DataMember = Nothing
            State.searchDV = Nothing
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub moBtnClear_Click(sender As System.Object, e As System.EventArgs) Handles moBtnClear.Click
        Try
            ClearSearch()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnNew_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnNew_WRITE.Click
        Try
            State.BenefitProductCodeId = Guid.Empty
            SetSession()

            callPage(BenefitProductCodeForm.URL, State.BenefitProductCodeId)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
#End Region
#Region "State-Management"

    Private Sub SetSession()
        With State
            .ProductCodeMask = moBenefitProductCodeText.Text.ToUpper
            .DealerId = DealerMultipleDrop.SelectedGuid
            .PageIndex = Grid.PageIndex
            .PageSize = Grid.PageSize
            .PageSort = State.SortExpression
            .SearchDataView = State.searchDV
        End With
    End Sub


#End Region
End Class