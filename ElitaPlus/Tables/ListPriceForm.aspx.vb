Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports System.Collections.Generic
Imports System.Web.Services
Imports System.Threading
Imports Assurant.Elita.Web.Forms
Imports System.Web.Script.Services
Imports Assurant.ElitaPlus.Security
Imports System.Web.Script.Serialization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements

Namespace Tables

    Public Class ListPriceForm
        Inherits ElitaPlusSearchPage


#Region "Constants"
        Public Const SP_DATE_FORMAT As String = "yyyyMMdd"
        Public Const PAGETITLE As String = "LIST_PRICE"
        Public Const PAGETAB As String = "TABLES"

        Private Const LABEL_DEALER As String = "DEALER"

        Public Const GRID_COL_LIST_PRICE_ID As Integer = 0
        Public Const GRID_COL_DEALER_CODE As Integer = 1
        Public Const GRID_COL_DEALER_NAME As Integer = 2
        Public Const GRID_COL_SKU_NUMBER As Integer = 3
        Public Const GRID_COL_MANUFACTURER_NAME As Integer = 4
        Public Const GRID_COL_MODEL_NUMBER As Integer = 5
        Public Const GRID_COL_PRICE As Integer = 7
        Public Const GRID_COL_EFFECTIVE As Integer = 8
        Public Const GRID_COL_EXPIRATION As Integer = 9

        Public Const GRID_TOTAL_COLUMNS As Integer = 10
#End Region

#Region "Properties"

        Public ReadOnly Property TheDealerControl() As MultipleColumnDDLabelControl
            Get
                If multipleDropControl Is Nothing Then
                    multipleDropControl = CType(FindControl("multipleDropControl"), MultipleColumnDDLabelControl)
                End If
                Return multipleDropControl
            End Get
        End Property
#End Region

#Region "Page State"

        ' This class keeps the current state for the search page.
        Class MyState
            Public searchDV As ListPrice.ListPriceSearchDV = Nothing
            Public SortExpression As String = ListPrice.ListPriceSearchDV.COL_NAME_SKU_NUMBER

            Public myBO As ListPrice
            Public PageIndex As Integer = 0
            Public PageSort As String
            Public PageSize As Integer = DEFAULT_PAGE_SIZE
            Public FromDateMask As String = String.Empty
            Public ToDateMask As String = String.Empty
            Public SearchDataView As ListPrice.ListPriceSearchDV
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

#Region "Handlers"

#Region "Hanlers-Init"
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Me.ErrControllerMaster.Clear_Hide()
            Try
                Page.Master.Page.Form.DefaultButton = moBtnSearch.UniqueID

                If Not Me.IsPostBack Then

                    Me.SetFormTitle(PAGETITLE)
                    Me.SetFormTab(PAGETAB)
                    Me.AddCalendar(Me.btnFromDate, Me.txtFromDate)
                    Me.AddCalendar(Me.btnToDate, Me.txtToDate)
                    ControlMgr.SetVisibleControl(Me, trPageSize, False)

                    Me.MenuEnabled = True
                    Me.SetGridItemStyleColor(moListPriceGrid)
                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    PopulateDealer()
                    PopulateDropdowns()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
            Me.ShowMissingTranslations(Me.ErrControllerMaster)
        End Sub

#End Region

#Region "Handlers-Grid"

        Private Sub Grid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                moListPriceGrid.CurrentPageIndex = NewCurrentPageIndex(moListPriceGrid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                Me.State.PageSize = moListPriceGrid.PageSize
                PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Private Sub moListPriceGrid_PageIndexChanged(ByVal source As Object, ByVal e As DataGridPageChangedEventArgs) Handles moListPriceGrid.PageIndexChanged
            Try
                moListPriceGrid.CurrentPageIndex = e.NewPageIndex
                PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Private Sub moListPriceGrid_ItemCreated(ByVal sender As Object, ByVal e As DataGridItemEventArgs) Handles moListPriceGrid.ItemCreated
            Try
                BaseItemCreated(sender, e)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Private Sub moListPriceGrid_SortCommand(ByVal source As Object, ByVal e As DataGridSortCommandEventArgs) Handles moListPriceGrid.SortCommand
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
                Me.moListPriceGrid.CurrentPageIndex = 0
                Me.moListPriceGrid.SelectedIndex = -1
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Private Sub moListPriceGrid_ItemDataBound(ByVal sender As Object, ByVal e As DataGridItemEventArgs) Handles moListPriceGrid.ItemDataBound
            Try
                Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)

                If (itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem _
                    OrElse itemType = ListItemType.SelectedItem) Then
                    e.Item.Cells(Me.GRID_COL_EFFECTIVE).Text = Me.GetDateFormattedString(DateHelper.GetDateValue(dvRow(ListPrice.ListPriceSearchDV.COL_NAME_EFFECTIVE).ToString()))
                    e.Item.Cells(Me.GRID_COL_EXPIRATION).Text = Me.GetDateFormattedString(DateHelper.GetDateValue(dvRow(ListPrice.ListPriceSearchDV.COL_NAME_EXPIRATION).ToString()))
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub
#End Region

#Region "Handlers-Buttons"

        Protected Sub moBtnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles moBtnSearch.Click
            Try

                Me.State.PageIndex = Me.NO_PAGE_INDEX
                Me.State.searchDV = Nothing
                moListPriceGrid.DataMember = Nothing
                moListPriceGrid.SelectedIndex = NO_ITEM_SELECTED_INDEX
                moListPriceGrid.CurrentPageIndex = 0

                If txtFromDate.Text <> "" And txtToDate.Text = "" Then
                    txtToDate = txtFromDate
                End If

                If txtToDate.Text <> "" And txtFromDate.Text = "" Then
                    txtFromDate = txtToDate
                End If

                If TheDealerControl.SelectedGuid.Equals(Guid.Empty) Then
                    Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
                End If

                'Dates
                'High date must be higher than low date.
                If txtFromDate.Text <> "" Or txtToDate.Text <> "" Then
                    ExchangeRateListForm.ValidateBeginEndDate(Me.lblFromDate, Me.txtFromDate.Text, Me.lblToDate, Me.txtToDate.Text)
                End If

                If txtFromDate.Text <> "" Then
                    Me.State.FromDateMask = DateHelper.GetDateValue(Me.txtFromDate.Text).ToString(SP_DATE_FORMAT)
                Else
                    Me.State.FromDateMask = String.Empty
                End If

                If txtToDate.Text <> "" Then
                    Me.State.ToDateMask = DateHelper.GetDateValue(Me.txtToDate.Text).ToString(SP_DATE_FORMAT)
                Else
                    Me.State.ToDateMask = String.Empty
                End If

                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Protected Sub moBtnClear_Click(ByVal sender As Object, ByVal e As EventArgs) Handles moBtnClear.Click
            Try
                ClearSearch()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

#End Region

#End Region

#Region "Controlling Logic"
        Private Sub PopulateDropdowns()
            'Me.BindListControlToDataView(Me.ddlAmountType, LookupListNew.DropdownLookupList("LPAMOUNTTYPE", ElitaPlusIdentity.Current.ActiveUser.LanguageId, True))
            Dim amountTypeLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("LPAMOUNTTYPE", Thread.CurrentPrincipal.GetLanguageCode())
            Me.ddlAmountType.Populate(amountTypeLkl, New PopulateOptions() With
             {
            .AddBlankItem = True
                  })
        End Sub
        Private Sub PopulateDealer()
            Try
                Dim oDataView As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)
                TheDealerControl.Caption = TranslationBase.TranslateLabelOrMessage(LABEL_DEALER)
                TheDealerControl.NothingSelected = True
                TheDealerControl.BindData(oDataView)
                TheDealerControl.AutoPostBackDD = False
                TheDealerControl.NothingSelected = True
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Private Sub PopulateGrid()
            Dim oDataView As DataView
            Dim guidAmtType As Guid = Guid.Empty
            guidAmtType = GetSelectedItem(ddlAmountType)

            Try

                If (Me.State.searchDV Is Nothing) Then
                    Me.State.searchDV = ListPrice.GetList(TheDealerControl.SelectedGuid, tbSearchManufacturer.Text.Trim(), _
                            tbSearchModel.Text.Trim(), tbSearchSKU.Text.Trim(), Me.State.FromDateMask, Me.State.ToDateMask, guidAmtType)
                End If

                Me.State.PageIndex = Me.moListPriceGrid.CurrentPageIndex
                Me.State.searchDV.Sort = Me.State.SortExpression
                Me.moListPriceGrid.DataSource = Me.State.searchDV
                HighLightSortColumn(moListPriceGrid, Me.State.SortExpression)
                Me.moListPriceGrid.DataBind()

                ControlMgr.SetVisibleControl(Me, trPageSize, moListPriceGrid.Visible)

                Session("recCount") = Me.State.searchDV.Count

                If Me.moListPriceGrid.Visible Then
                    Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Private Sub ClearSearch()
            TheDealerControl.SelectedIndex = 0
            Me.tbSearchSKU.Text = ""
            Me.tbSearchManufacturer.Text = ""
            Me.tbSearchModel.Text = ""
            Me.txtFromDate.Text = ""
            Me.txtToDate.Text = ""
            Me.ddlAmountType.SelectedIndex = -1
        End Sub

#End Region

#Region "State-Management"

        Private Sub SetSession()
            With Me.State
                .PageIndex = moListPriceGrid.CurrentPageIndex
                .PageSize = moListPriceGrid.PageSize
                .PageSort = Me.State.SortExpression
                .SearchDataView = Me.State.searchDV
            End With
        End Sub

#End Region


    End Class
End Namespace