Imports Assurant.ElitaPlus.DALObjects
Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Namespace Tables


    Partial Class ProductConversionForm
        Inherits ElitaPlusSearchPage

#Region "Page State"
        Class MyState
            Public IsNew As Boolean
            Public AddingNewRow As Boolean
            Public searchDV As DataView = Nothing
            Public moProductCodeConversionId As Guid
            Public IsProductCodeConvNew As Boolean = False
            Public SortExpression As String = ProductCodeConversionDAL.COL_NAME_DEALER_NAME
            Public searchData As ProductConversionData

            Public IsGridVisible As Boolean = False
            Public PageIndex As Integer = 0
            Public PageSort As String
            Public PageSize As Integer = 15

            Public ProductCodeMask As String
            Public DealerId As Guid = Guid.Empty
            Public ProductcodeId As Guid = Guid.Empty
            Public Productcode As String
            Public ExternalProductcode As String
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
        Private Const GRID_COL_EDIT_IDX As Integer = 0
        Private Const GRID_COL_DEALER_NAME As Integer = 0
        Private Const GRID_COL_EXTERNAL_PRODUCT_CODE As Integer = 1
        Private Const GRID_COL_PRODUCT_CODE As Integer = 2
        Private Const GRID_COL_PRODUCT_CODE_CONVERSION_ID As Integer = 3

        'Actions
        Private Const ACTION_NONE As String = "ACTION_NONE"
        Private Const ACTION_SAVE As String = "ACTION_SAVE"
        Private Const ACTION_NO_EDIT As String = "ACTION_NO_EDIT"
        Private Const ACTION_EDIT As String = "ACTION_EDIT"
        Private Const ACTION_NEW As String = "ACTION_NEW"
        Private Const LABEL_DEALER As String = "DEALER"
        Private Const LABEL_PRODUCT_CODE As String = "PRODUCT_CODE"

        Public Const COL_ProductConversion_ID As String = "PRODUCT_CONVERSION_ID"
        Public Const COL_DEALER_NAME As String = "DEALER_NAME"
        Public Const COL_PRODUCT_CODE As String = "PRODUCT_CODE"
        Public Const COL_EXT_PRODUCT_CODE As String = "EXTERNAL_PROD_CODE"

#End Region

#Region "Parameters"
        Public Class Parameters
            Public ProductCodeConvId As Guid = Nothing
            Public DealerId As Guid = Nothing
            Public ProductCodeId As Guid = Nothing
            Public ProductCode As String = Nothing
            Public DealerProductCode As String = Nothing

            Public Sub New(ByVal ProductCodeConvId As Guid, ByVal DealerId As Guid, ByVal ProductCodeId As Guid, ByVal ProductCode As String, ByVal DealerProductCode As String)
                Me.ProductCodeConvId = ProductCodeConvId
                Me.DealerId = DealerId
                Me.ProductCodeId = ProductCodeId
                Me.ProductCode = ProductCode
                Me.DealerProductCode = DealerProductCode
            End Sub

        End Class
#End Region


#Region "Variables"

        Private moProductCodeConversion As ProductCodeConversion
        Private moProductCodeConversionId As String
        Private isReturning As Boolean = False
        Private isPageReturn As Boolean = False

#End Region

#Region "Properties"

        'Private ReadOnly Property TheProductCodeConversion() As ProductCodeConversion
        '    Get
        '        If IsNewProductConversion() = True Then
        '            ' For creating, inserting
        '            moProductCodeConversion = New ProductCodeConversion
        '            ProductCodeConversionId = moProductCodeConversion.Id.ToString
        '        Else
        '            ' For updating, deleting
        '            Dim oProductCodeConversionId As Guid = Me.GetGuidFromString(ProductCodeConversionId)
        '            moProductCodeConversion = New ProductCodeConversion(oProductCodeConversionId)
        '        End If

        '        Return moProductCodeConversion
        '    End Get
        'End Property

        Private Property ProductCodeConversionId() As String
            Get
                If Grid.SelectedIndex > Me.NO_ITEM_SELECTED_INDEX Then
                    moProductCodeConversionId = Me.GetSelectedGridText(Grid, GRID_COL_PRODUCT_CODE_CONVERSION_ID)
                End If
                Return moProductCodeConversionId
            End Get
            Set(ByVal Value As String)
                If Grid.SelectedIndex > Me.NO_ITEM_SELECTED_INDEX Then
                    Me.SetSelectedGridText(Grid, GRID_COL_PRODUCT_CODE_CONVERSION_ID, Value)
                End If
                moProductCodeConversionId = Value
            End Set
        End Property

        Private Property IsNewProductConversion() As Boolean
            Get
                Return Me.State.IsNew
            End Get
            Set(ByVal Value As Boolean)
                Me.State.IsNew = Value
            End Set
        End Property

#End Region

#Region "Handlers"

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        ' Protected WithEvents MasterPage.MessageController As ErrorController


        'NOTE: The following placeholder declaration is required by the Web Form Designer.
        'Do not delete or move it.
        Private designerPlaceholderDeclaration As System.Object

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

#End Region

#Region "Handlers-Init"

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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
                        Me.PopulateProductConversionGrid()
                    End If
                    Me.SetGridItemStyleColor(Me.Grid)
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
            Me.ShowMissingTranslations(Me.MasterPage.MessageController)
        End Sub

#End Region

#Region "Handlers-Dropdowns"

        Private Sub DealerDropChanged(ByVal DealerMultipleDrop As Assurant.ElitaPlus.ElitaPlusWebApp.Common.MultipleColumnDDLabelControl_New) Handles DealerMultipleDrop.SelectedDropChanged
            Try
                PopulateProductCode()
                Me.PopulateProductConversionGrid(, False)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Handlers-Buttons"

        Private Sub SearchButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles moBtnSearch.Click
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
                Grid.PageIndex = Me.NO_PAGE_INDEX
                Grid.DataMember = Nothing
                Me.State.searchDV = Nothing
                SetSession()
                Grid.PageIndex = Me.NO_PAGE_INDEX
                Grid.DataMember = Nothing
                Me.State.searchDV = Nothing
                Me.PopulateProductConversionGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub ClearButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles moBtnClear.Click
            ClearSearchCriteria()

        End Sub

        Private Sub NewButton_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew_WRITE.Click
            Try

                Me.State.moProductCodeConversionId = Guid.Empty
                Me.State.IsProductCodeConvNew = True
                SetSession()
                '     Response.Redirect(PRODUCTCODE_DETAIL_PAGE)
                '  Me.callPage(ProductConversionExtendedForm.URL, Me.State.moProductCodeConversionId)

                Me.callPage(ProductConversionExtendedForm.URL, New ProductConversionExtendedForm.Parameters(Me.State.moProductCodeConversionId, Me.State.DealerId, Me.State.ProductcodeId, Me.State.Productcode, Me.State.ExternalProductcode))

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Handlers-Grid"

        'Protected Sub ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs)
        '    Dim nIndex As Integer
        '    Dim oDealerDrop, oProductCodeDrop As DropDownList
        '    Dim dv As DataView

        '    Try
        '        If e.CommandName = Me.EDIT_COMMAND_NAME Then
        '            nIndex = e.Item.ItemIndex
        '            Grid.EditItemIndex = nIndex
        '            Grid.SelectedIndex = nIndex
        '            Me.SetGridControls(Grid, False)

        '            PopulateProductConversionGrid(ACTION_EDIT)

        '            dv = LookupListNew.GetDealerForProductCodeConvLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)
        '            dv.RowFilter = "DEALER_ID = '" + GuidControl.GuidToHexString(TheProductCodeConversion.DealerId()) + "'"

        '            'If dv.Count >= 1 AndAlso (Not dv(0).Item("CONV_CODE") Is DBNull.Value) AndAlso (dv(0).Item("CONV_CODE").ToString.Equals("EXT") Or dv(0).Item("CONV_CODE").ToString.Equals("P")) Then
        '            '    Dim oProductCodeConversion As ProductCodeConversion
        '            '    oProductCodeConversion = Me.TheProductCodeConversion
        '            '    Session(ProductConversionExtendedForm.SESSION_BO) = oProductCodeConversion
        '            '    Me.callPage(ProductConversionExtendedForm.URL)
        '            '    Exit Sub
        '            'End If

        '            oDealerDrop = CType(Me.GetSelectedGridControl(Grid, GRID_COL_DEALER_NAME), DropDownList)
        '            oProductCodeDrop = CType(Me.GetSelectedGridControl(Grid, GRID_COL_PRODUCT_CODE), DropDownList)
        '            PopulateDealerGrid(oDealerDrop, oProductCodeDrop)

        '        ElseIf (e.CommandName = Me.DELETE_COMMAND_NAME) Then

        '            nIndex = e.Item.ItemIndex
        '            ProductCodeConversionId = Me.GetGridText(Grid, nIndex, GRID_COL_PRODUCT_CODE_CONVERSION_ID)
        '            If DeleteSelectedProductCodeConversion() = True Then
        '                Me.State.searchDV = Nothing
        '                PopulateProductConversionGrid(ACTION_NO_EDIT)
        '            End If

        '        ElseIf e.CommandName = Me.SORT_COMMAND_NAME Then

        '        End If
        '    Catch ex As Exception
        '        Me.HandleErrors(ex, Me.MasterPage.MessageController)
        '    End Try

        'End Sub

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
                PopulateProductConversionGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
            Try
                Me.State.PageIndex = e.NewPageIndex
                Me.State.DealerId = Guid.Empty
                PopulateProductConversionGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Public Sub RowCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
            BaseItemCreated(sender, e)
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
                PopulateProductConversionGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
            Try
                Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
                Dim btnEditItem As LinkButton
                If Not dvRow Is Nothing And Not Me.State.bnoRow Then
                    If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                        btnEditItem = CType(e.Row.Cells(Me.GRID_COL_EDIT_IDX).FindControl("SelectAction"), LinkButton)
                        btnEditItem.Text = dvRow(COL_DEALER_NAME).ToString
                        '    e.Row.Cells(Me.GRID_COL_DEALER_NAME).Text = dvRow(COL_DEALER_NAME).ToString
                        e.Row.Cells(Me.GRID_COL_PRODUCT_CODE).Text = dvRow(COL_PRODUCT_CODE).ToString
                        e.Row.Cells(Me.GRID_COL_EXTERNAL_PRODUCT_CODE).Text = dvRow(COL_EXT_PRODUCT_CODE).ToString
                        e.Row.Cells(Me.GRID_COL_PRODUCT_CODE_CONVERSION_ID).Text = GetGuidStringFromByteArray(CType(dvRow(COL_ProductConversion_ID), Byte()))
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
                    Me.State.moProductCodeConversionId = New Guid(Me.Grid.Rows(index).Cells(Me.GRID_COL_PRODUCT_CODE_CONVERSION_ID).Text)
                    ' Me.callPage(ProductConversionExtendedForm.URL, Me.State.moProductCodeConversionId)
                    Me.callPage(ProductConversionExtendedForm.URL, New ProductConversionExtendedForm.Parameters(Me.State.moProductCodeConversionId, Me.State.DealerId, Me.State.ProductcodeId, Me.State.Productcode, Me.State.ExternalProductcode))
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

#End Region

#End Region

#Region "Buttons-Management"

        'Private Sub SetButtonsState(ByVal bIsEdit As Boolean)

        '    If (bIsEdit) Then                
        '        ControlMgr.SetVisibleControl(Me, btnNew_WRITE, False)
        '        ControlMgr.SetEnableControl(Me, moBtnSearch, False)
        '        ControlMgr.SetEnableControl(Me, moBtnClear, False)
        '        ControlMgr.SetEnableControl(Me, moDealerDrop, False)
        '        ControlMgr.SetEnableControl(Me, moExternalProductCodeText, False)
        '        ControlMgr.SetEnableControl(Me, moAssurantProductCodeDrop, False)
        '        Me.MenuEnabled = False
        '        If (Me.cboPageSize.Visible) Then
        '            ControlMgr.SetEnableControl(Me, cboPageSize, False)
        '        End If
        '        Me.SetGridControls(Me.Grid, False)
        '    Else
        '        ControlMgr.SetVisibleControl(Me, SaveButton_WRITE, False)
        '        ControlMgr.SetVisibleControl(Me, CancelButton, False)
        '        ControlMgr.SetVisibleControl(Me, NewButton_WRITE, True)
        '        ControlMgr.SetEnableControl(Me, SearchButton, True)
        '        ControlMgr.SetEnableControl(Me, ClearButton, True)
        '        ControlMgr.SetEnableControl(Me, moDealerDrop, True)
        '        ControlMgr.SetEnableControl(Me, moExternalProductCodeText, True)
        '        ControlMgr.SetEnableControl(Me, moAssurantProductCodeDrop, True)
        '        Me.MenuEnabled = True
        '        If (Me.cboPageSize.Visible) Then
        '            ControlMgr.SetEnableControl(Me, cboPageSize, True)
        '        End If
        '    End If

        'End Sub
#End Region

#Region "Populate"

        Private Sub PopulateProductConversionGrid(Optional ByVal oAction As String = ACTION_NONE, Optional ByVal refresh As Boolean = True)

            Dim oDataView As DataView
            Try
                If refresh = True Then

                    If (Me.State.searchDV Is Nothing) Then
                        Me.State.searchDV = GetDV()
                    End If
                End If

                If Not Me.State.searchDV Is Nothing Then

                    If (Me.State.searchDV.Count = 0) Then

                        Me.State.bnoRow = True
                        CreateHeaderForEmptyGrid(Grid, Me.SortDirection)
                    Else
                        Me.State.bnoRow = False
                        Me.Grid.Enabled = True
                    End If

                    Me.State.searchDV.Sort = Me.State.SortExpression
                    Grid.AutoGenerateColumns = False

                    Grid.Columns(Me.GRID_COL_DEALER_NAME).SortExpression = COL_DEALER_NAME
                    Grid.Columns(Me.GRID_COL_PRODUCT_CODE).SortExpression = COL_PRODUCT_CODE
                    Grid.Columns(Me.GRID_COL_EXTERNAL_PRODUCT_CODE).SortExpression = COL_EXT_PRODUCT_CODE
                    HighLightSortColumn(Grid, Me.State.SortExpression)
                    ' BasePopulateGrid(Grid, Me.State.searchDV, Me.State.moProductCodeId, oAction)

                    SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.DealerId, Me.Grid, Me.State.PageIndex)

                    Me.Grid.DataSource = Me.State.searchDV
                    HighLightSortColumn(Grid, Me.SortDirection)
                    Me.Grid.DataBind()

                    ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)

                    Session("recCount") = Me.State.searchDV.Count

                    If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True

                    Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)

                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

        Private Sub PopulateDealer()
            Try

                Dim dv As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, True)
                DealerMultipleDrop.SetControl(True, DealerMultipleDrop.MODES.NEW_MODE, True, dv, "*" + TranslationBase.TranslateLabelOrMessage(LABEL_DEALER), True, True)

                'Dim oDataView As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)
                DealerMultipleDrop.Caption = TranslationBase.TranslateLabelOrMessage(LABEL_DEALER)
                DealerMultipleDrop.BindData(dv)
                DealerMultipleDrop.AutoPostBackDD = True
                DealerMultipleDrop.NothingSelected = True
                DealerMultipleDrop.SelectedGuid = Me.State.DealerId
                PopulateProductCode()

            Catch ex As Exception
                Me.MasterPage.MessageController.AddError(ex.Message, False)
                Me.MasterPage.MessageController.Show()
            End Try
        End Sub

        Private Sub PopulateProductCode()
            Try
                Dim oCompanyId As Guid = ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID
                Dim oDealerId As Guid = DealerMultipleDrop.SelectedGuid

                If Not oDealerId.Equals(Guid.Empty) Then
                    '  Me.BindListControlToDataView(dpAssurantProdCode,
                    ' LookupListNew.GetProductCodeLookupList(oDealerId), "CODE", "ID", True)
                    Dim listcontext As ListContext = New ListContext()
                    listcontext.DealerId = oDealerId
                    Dim ProdLKL As ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.ProductCodeByDealer, Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
                    dpAssurantProdCode.Populate(ProdLKL, New PopulateOptions() With
                {
                .AddBlankItem = True,
                .TextFunc = AddressOf .GetCode,
                .SortFunc = AddressOf .GetCode
                })
                    BindSelectItem(Me.State.ProductcodeId.ToString, dpAssurantProdCode)
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub PopulateExternalProductCode()
            Try
                PopulateControlFromBOProperty(Me.txtExternalProductCode, Me.State.ExternalProductcode)
                ' Me.SetSelectedGridText(Grid, GRID_COL_EXTERNAL_PRODUCT_CODE, TheProductCodeConversion.ExternalProdCode)

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        'Private Sub PopulateProductCodeGrid(ByVal dealerList As DropDownList, ByVal productCodeList As DropDownList)
        '    Try
        '        Dim oDealerId As Guid = Me.GetSelectedItem(dealerList)

        '        Me.BindListControlToDataView(productCodeList, _
        '                LookupListNew.GetProductCodeLookupList(oDealerId), "CODE", "ID", False)
        '        ' BindSelectItem(TheProductCodeConversion.ProductCodeId.ToString, productCodeList)
        '        'PopulateExternalProductCode()
        '    Catch ex As Exception
        '        Me.HandleErrors(ex, Me.MasterPage.MessageController)
        '    End Try
        'End Sub

        Private Sub ClearSearchCriteria()

            Try
                txtExternalProductCode.Text = String.Empty
                'Set the 1st Item in the dropdpwms List to ""
                DealerMultipleDrop.NothingSelected = True
                dpAssurantProdCode.SelectedIndex = -1

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

        Private Sub UpdateBreadCrum()
            If (Not Me.State Is Nothing) Then
                If (Not Me.State Is Nothing) Then
                    Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("PRODUCT_CONVERSION")
                    Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("PRODUCT_CONVERSION")
                End If
            End If
        End Sub

        Private Sub PopulateDropdown()
            PopulateDealer()
            PopulateExternalProductCode()
        End Sub

#End Region

#Region "Business"

        Private Function GetDV() As DataView

            Dim dv As DataView

            dv = GetDataView()
            dv.Sort = Grid.DataMember()
            Grid.DataSource = dv

            Return (dv)

        End Function

        Private Function GetDataView() As DataView
            'Dim oProdConv As ProductConversionData = New ProductConversionData
            Dim oDataView As DataView
            If State.searchData Is Nothing Then State.searchData = New ProductConversionData

            With State.searchData
                .companyIds = ElitaPlusIdentity.Current.ActiveUser.Companies
                If DealerMultipleDrop.SelectedIndex > Me.BLANK_ITEM_SELECTED Then
                    .dealerId = DealerMultipleDrop.SelectedGuid
                Else
                    .dealerId = Guid.Empty
                End If
                If dpAssurantProdCode.SelectedIndex > Me.BLANK_ITEM_SELECTED Then
                    .productCodeId = Me.GetSelectedItem(dpAssurantProdCode)
                Else
                    .productCodeId = Guid.Empty
                End If
                .externalProductCode = txtExternalProductCode.Text.ToUpper
                oDataView = ProductCodeConversion.ProductConversionList(State.searchData)
                .externalProductCode = txtExternalProductCode.Text.ToUpper
            End With

            Return oDataView
        End Function

        Private Sub PopulateBOFromForm(ByVal oProductCodeConversion As ProductCodeConversion)
            Try
                With oProductCodeConversion
                    .DealerId = Me.GetSelectedGridDropItem(Grid, GRID_COL_DEALER_NAME)
                    .ExternalProdCode = Me.GetSelectedGridText(Grid, GRID_COL_PRODUCT_CODE).ToUpper
                    .ProductCodeId = Me.GetSelectedGridDropItem(Grid, GRID_COL_EXTERNAL_PRODUCT_CODE)
                End With
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

#End Region

#Region "State-Management"

        Private Sub SetSession()
            With Me.State
                .Productcode = dpAssurantProdCode.SelectedValue
                .ProductcodeId = Me.GetSelectedItem(dpAssurantProdCode)
                .DealerId = DealerMultipleDrop.SelectedGuid
                .ExternalProductcode = txtExternalProductCode.Text.ToString
                .PageIndex = Grid.PageIndex
                .PageSize = grid.PageSize
                .PageSort = Me.State.SortExpression
                .searchDV = Me.State.searchDV
            End With
        End Sub


#End Region

#Region "Page Return"

        Private IsReturningFromChild As Boolean = False
        Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles Me.PageReturn
            Try
                Me.IsReturningFromChild = True
                Dim retObj As ProductConversionForm.ReturnType = CType(ReturnPar, ProductConversionForm.ReturnType)
                'Dim retObj As ReturnType = CType(ReturnPar, ReturnType)
                If Not retObj Is Nothing AndAlso retObj.BoChanged Then
                    Me.State.searchDV = Nothing
                End If
                If Not retObj Is Nothing Then
                    Select Case retObj.LastOperation
                        Case ElitaPlusPage.DetailPageCommand.Back
                            Me.State.moProductCodeConversionId = retObj.ProductCodeConversionId
                            Me.State.IsGridVisible = True
                        Case ElitaPlusPage.DetailPageCommand.Delete
                            Me.AddInfoMsg(Message.DELETE_RECORD_CONFIRMATION)
                            Me.State.moProductCodeConversionId = Guid.Empty
                        Case Else
                            Me.State.moProductCodeConversionId = Guid.Empty
                    End Select
                    Grid.PageIndex = Me.State.PageIndex
                    cboPageSize.SelectedValue = CType(Me.State.PageSize, String)
                    Grid.PageSize = Me.State.PageSize
                    ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)
                    Me.State.DealerId = retObj.DealerIdSearchParam
                    Me.State.ProductcodeId = retObj.ProductCodeIdSearchParam
                    Me.State.Productcode = retObj.ProductCodeSearchParam
                    Me.State.ExternalProductcode = retObj.DealerProductCodeSearchParam

                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub


        Public Class ReturnType
            Public LastOperation As ElitaPlusPage.DetailPageCommand
            Public ProductCodeConversionId As Guid
            Public DealerIdSearchParam As Guid
            Public ProductCodeIdSearchParam As Guid
            Public ProductCodeSearchParam As String
            Public DealerProductCodeSearchParam As String
            Public BoChanged As Boolean = False

            Public Sub New(ByVal LastOp As ElitaPlusPage.DetailPageCommand, ByVal ProductCodeConversionId As Guid, ByVal SearchParam As ProductConversionExtendedForm.Parameters, Optional ByVal boChanged As Boolean = False)
                Me.LastOperation = LastOp
                Me.ProductCodeConversionId = ProductCodeConversionId
                Me.BoChanged = boChanged
                If Not SearchParam Is Nothing Then
                    Me.DealerIdSearchParam = SearchParam.DealerId
                    Me.ProductCodeIdSearchParam = SearchParam.ProductCodeId
                    Me.ProductCodeSearchParam = SearchParam.Productcode
                    Me.DealerProductCodeSearchParam = SearchParam.ExternalProductcode
                End If
            End Sub

        End Class


#End Region

    End Class

End Namespace
