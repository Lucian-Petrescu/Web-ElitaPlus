Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports Assurant.Elita.Web.Forms
Imports System.Threading
Namespace Tables

    Partial Public Class CertExtendedItemListForm
        Inherits ElitaPlusSearchPage

#Region "Constants"
        Public Const Url As String = "Tables/CertExtendedItemListForm.aspx"
        Private Const Admin As String = "Admin"
        Private Const CertItemExtendedControl As String = "Certificate Extended Item Configuration Search"
        Private Const SelectCommand As String = "SelectAction"
        Private Const NoRowSelectedIndex As Integer = -1
        Private const GridCtrlNameLabelCertExtCode="lblCertExtConfigCode"
        Private Const DefaultSort As String = "CODE ASC"
        Private Const LabelSelectDealerCode As String  = "DEALER"
        Private Const LabelSelectCompanyCode As String = "COMPANY"
        Private Const GridColCodeIdx As Integer = 0
        Private Const GridColDescriptionIdx As Integer = 1
        Private Const GridColCreatedDateIdx As Integer = 2
        Private Const GridColModifiedDateIdx As Integer = 3
        Private Const ColCreatedDate As String = "CREATED_DATE"
        Private Const ColModifiedDate As String = "MODIFIED_DATE"
#End Region

#Region "Page State"
        Private _isReturningFromChild As Boolean = False
        Private _childMessage As String = String.Empty
        Class MyState
            Public CertExtendedItemCode As string
            Public PageIndex As Integer = 0
            Public PageSize As Integer = DEFAULT_NEW_UI_PAGE_SIZE
            Public SearchDv As CertExtendedItem.CertExtendedItemSearchDv = Nothing
            Public HasDataChanged As Boolean
            Public IsGridVisible As Boolean = False
            Public BnoRow As Boolean = False
            Public SortExpression As String = DefaultSort
            Public SearchStageName As Guid = Guid.Empty
            Public SearchCompanyGrp As Guid = Guid.Empty
            Public CompanyId As Guid = Guid.Empty
            Public DealerId As Guid = Guid.Empty
            Public CertExtCodeId As Guid = Guid.Empty
            Public CertExtCode As String = String.Empty
            Public ReferenceId as Guid= Guid.Empty
            Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
        End Class

        Public Sub New()
            MyBase.New(New MyState)
        End Sub

        Protected Shadows ReadOnly Property State() As MyState
            Get
                Return CType(MyBase.State, MyState)
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

#Region "Page event"
        Private Sub UpdateBreadCrum()
            MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & CERTITEMEXTENDEDCONTROL
        End Sub

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Try
                MasterPage.MessageController.Clear()
                If Not IsPostBack Then
                    SortDirection = State.SortExpression
                    MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(ADMIN)
                    MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(CERTITEMEXTENDEDCONTROL)
                    UpdateBreadCrum()
                    SetDefaultButton(ddlSearchConfigCode, btnSearch)
                    PopulateDropdown()
                    TranslateGridHeader(Grid)
                    cboPageSize.SelectedValue = CType(State.PageSize, String)
                    Grid.PageSize = State.PageSize
                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    If State.IsGridVisible Then
                        PopulateGrid()
                    End If

                    SetGridItemStyleColor(Grid)

                    If _isReturningFromChild AndAlso _childMessage.Trim <> String.Empty Then
                        MasterPage.MessageController.AddSuccess(_childMessage)
                    End If
                End If

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
            ShowMissingTranslations(MasterPage.MessageController)
        End Sub

        Private Sub Page_PageReturn(ByVal returnFromUrl As String, ByVal returnPar As Object) Handles MyBase.PageReturn
            Try
                MenuEnabled = True
                _isReturningFromChild = True
                Dim retObj As CertExtendedItemForm.ReturnType = CType(ReturnPar, CertExtendedItemForm.ReturnType)
                State.HasDataChanged = retObj.HasDataChanged

                If Not retObj Is Nothing AndAlso retObj.HasDataChanged Then
                    State.searchDV = Nothing
                End If

                Select Case retObj.LastOperation
                    Case ElitaPlusPage.DetailPageCommand.Back
                        If Not retObj Is Nothing Then
                            If Not retObj.EditingBo.IsNew Then
                                State.DealerId = retObj.EditingBo.Id
                            End If
                        End If
                    Case ElitaPlusPage.DetailPageCommand.Delete
                        _childMessage = Message.DELETE_RECORD_CONFIRMATION
                End Select
                With State
                    .DealerId=Guid.Empty
                    .CompanyId=Guid.Empty
                    .CertExtCode=String.Empty
                    .ReferenceId=Guid.Empty
                End With
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
#End Region

#Region "Button Clicks "
        Private Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
            callPage(CertExtendedItemForm.URL)
        End Sub

        Private Sub btnClearSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClearSearch.Click
            Try
                ddlSearchConfigCode.SelectedIndex = -1
                moDealerMultipleDrop.SelectedIndex = -1
                moCompanyMultipleDrop.SelectedIndex=-1

                Grid.EditIndex = NO_ITEM_SELECTED_INDEX

                With State
                    .DealerId=Guid.Empty
                    .CompanyId=Guid.Empty
                    .CertExtCode=String.Empty
                    .ReferenceId=Guid.Empty
                End With
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
            Try
                With State
                    .PageIndex = 0
                    .IsGridVisible = True
                    .searchDV = Nothing
                    .HasDataChanged = False
                End With
                If Not  State.CompanyId.Equals(Guid.Empty) AndAlso Not  State.DealerId.Equals(Guid.Empty) Then
                    State.SearchDv=new CertExtendedItem.CertExtendedItemSearchDv()
                    SortAndBindGrid(true)
                    MasterPage.MessageController.AddInformation(Message.MsgDealerCompanyExclusive, True)
                Else 
                    PopulateGrid()
                End If
                
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
#End Region

#Region "Helper Functions"
        Private Sub PopulateDropdown()
            Dim mCertExtendedItem as New CertExtendedItem
            moDealerMultipleDrop.Caption = "    " & TranslationBase.TranslateLabelOrMessage(LabelSelectDealerCode)
            moDealerMultipleDrop.NothingSelected = True

            moDealerMultipleDrop.BindData(mCertExtendedItem.GetAvailableDealers)
            moDealerMultipleDrop.AutoPostBackDD = True

            moCompanyMultipleDrop.Caption = TranslationBase.TranslateLabelOrMessage(LabelSelectCompanyCode)
            moCompanyMultipleDrop.NothingSelected = True
            
            Dim dv As New DataView
            Dim dt as New DataTable
            dt=mCertExtendedItem.GetAvailableCompanies.ToTable()
            dt.Columns("company_id").ColumnName="ID"
            dv=dt.DefaultView

            moCompanyMultipleDrop.BindData(dv)
            moCompanyMultipleDrop.AutoPostBackDD = True

            ddlSearchConfigCode.DataSource=CertExtendedItem.GetConfigCode().Tables(0)
            ddlSearchConfigCode.DataTextField = "Code"
            ddlSearchConfigCode.DataValueField = "Code"
            ddlSearchConfigCode.DataBind()
            ddlSearchConfigCode.Items.Insert(0, "")
        End Sub

        Private Sub OnFromDrop_Changed(ByVal fromMultipleDrop As Assurant.ElitaPlus.ElitaPlusWebApp.Common.MultipleColumnDDLabelControl_New) _
            Handles moDealerMultipleDrop.SelectedDropChanged
            Try
                Me.State.DealerId = moDealerMultipleDrop.SelectedGuid()
            Catch ex As Exception
                HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub
        Private Sub OnFromCompanyDrop_Changed(ByVal fromMultipleDrop As Assurant.ElitaPlus.ElitaPlusWebApp.Common.MultipleColumnDDLabelControl_New) _
            Handles moCompanyMultipleDrop.SelectedDropChanged
            Try
                Me.State.CompanyId = moCompanyMultipleDrop.SelectedGuid()
            Catch ex As Exception
                HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub
        Public Sub PopulateGrid()
            Dim blnNewSearch As Boolean = False
            With State
                If ((.searchDV Is Nothing) OrElse (.HasDataChanged)) Then
                    If Not (.CompanyId =Guid.Empty) And Not (.DealerId =Guid.Empty) then
                        .ReferenceId=Guid.Empty
                    ElseIf Not (.CompanyId =Guid.Empty)  then
                        .ReferenceId=.CompanyId
                    ElseIf Not (.DealerId =Guid.Empty)  then
                        .ReferenceId=.DealerId
                    End If
                    .searchDV = CertExtendedItem.GetDealerCompanyConfig(.CertExtCode,.ReferenceId)
                    blnNewSearch = True
                End If
                If Not (.searchDV Is Nothing) Then
                    If .searchDV.Count>0 Then
                        .searchDV.Sort = SortDirection
                    End If

                    Grid.AutoGenerateColumns = False

                    SetPageAndSelectedIndexFromGuid(.searchDV, .CertExtCodeId, Grid, State.PageIndex)
                    SortAndBindGrid(blnNewSearch)
                End If
            End With

        End Sub

        Private Sub SortAndBindGrid(Optional ByVal blnShowErr As Boolean = True)
            State.PageIndex = Grid.PageIndex

            If (State.searchDV.Count = 0) Then
                Me.State.bnoRow = True
                State.IsGridVisible = False
                
                Dim dt As DataTable = New DataTable()
                Dim dv As New DataView(dt)

                dv.Table.Rows.InsertAt(dv.Table.NewRow(), 0)
                Grid.PagerSettings.Visible = True
                Grid.DataSource = dv
                Grid.DataBind()
                Grid.Rows(0).Visible = False
                If blnShowErr Then
                    MasterPage.MessageController.AddInformation(ElitaPlus.ElitaPlusWebApp.Message.MSG_NO_RECORDS_FOUND, True)
                End If
            Else
                State.IsGridVisible = True
                State.bnoRow = False
                Grid.Enabled = True
                Grid.DataSource = State.searchDV
                HighLightSortColumn(Grid, SortDirection, True)
                Grid.DataBind()
                ControlMgr.SetVisibleControl(Me, trPageSize, Me.Grid.Visible)
            End If

            If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True

            Session("recCount") = State.searchDV.Count

            
            lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)


        End Sub
#End Region

#Region "Grid related"
        Private Sub Grid_PageIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grid.PageIndexChanged
            Try
                State.PageIndex = Grid.PageIndex
                State.CertExtendedItemCode = String.Empty
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
            Try
                Grid.PageIndex = e.NewPageIndex
                State.PageIndex = Grid.PageIndex
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting
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

        Protected Sub cboPageSize_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                State.PageSize = CType(cboPageSize.SelectedValue, Integer)
                State.PageIndex = NewCurrentPageIndex(Grid, State.searchDV.Count, State.PageSize)
                Grid.PageIndex = State.PageIndex
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
        Private Sub Grid_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
            Try
                Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
                Dim btnEditItem As LinkButton
                If Not dvRow Is Nothing And Not Me.State.bnoRow Then
                    If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                        btnEditItem = CType(e.Row.Cells(GridColCodeIdx).FindControl("SelectAction"), LinkButton)
                        btnEditItem.Text = dvRow(CertExtendedItem.CertExtendedItemSearchDv.COL_CODE).ToString
                        e.Row.Cells(GridColDescriptionIdx).Text = dvRow(CertExtendedItem.CertExtendedItemSearchDv.COL_DESCRIPTION).ToString
                        e.Row.Cells(GridColCreatedDateIdx).Text = dvRow(ColCreatedDate).ToString
                        e.Row.Cells(GridColModifiedDateIdx).Text = dvRow(ColModifiedDate).ToString
                    End If
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
                                
        End Sub
        Public Sub RowCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
            Try
                BaseItemCreated(sender, e)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Public Sub RowCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)

            Try
                Dim index As Integer
                If (e.CommandName = SelectCommand) Then
                    index = CInt(e.CommandArgument)
                    State.CertExtendedItemCode = CType(Grid.Rows(index).Cells(GridColCodeIdx).FindControl("SelectAction"), LinkButton).Text.ToUpper() 'Grid.Rows(index).Cells(GridColCodeIdx).Text.ToUpper()
                    callPage(CertExtendedItemForm.URL, State.CertExtendedItemCode)
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Private Sub DdlSearchConfigCode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSearchConfigCode.SelectedIndexChanged
            Try
                State.CertExtCode=ddlSearchConfigCode.SelectedValue
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
            
        End Sub
#End Region

    End Class
End Namespace