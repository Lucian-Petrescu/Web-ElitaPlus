Imports Microsoft.VisualBasic
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common

Namespace Tables


Partial Class ExchangeRateListForm
    Inherits ElitaPlusSearchPage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Constants"
    Public Const GRID_COL_EDIT_IDX As Integer = 0

    Public Const GRID_COL_DEALER_CODE_IDX As Integer = 1
    Public Const GRID_COL_DEALER_NAME_IDX As Integer = 2
    Public Const GRID_COL_EFFECTIVE_IDX As Integer = 3
    Public Const GRID_COL_CURRENCY_CONVERSION_ID_IDX As Integer = 4

    'Public Const GRID_TOTAL_COLUMNS As Integer = 5

    Private Const LABEL_DEALER As String = "DEALER"

        Public Const SP_DATE_FORMAT As String = "yyyyMMdd"
        Private Const LABEL_SELECT_DEALERCODE As String = "DEALER"

#End Region

#Region "Variables"
#End Region

#Region "Page State"
    Private IsReturningFromChild As Boolean = False

    Class MyState
        Public MyBO As CurrencyConversion
        Public PageIndex As Integer = 0
        Public SelectedCurencyConversionId As Guid = Guid.Empty
        Public SelectedDealerId As Guid = Guid.Empty
        Public FromDateMask As String = String.Empty
        Public TomDateMask As String = String.Empty
        Public IsGridVisible As Boolean = False
        Public searchDV As CurrencyConversion.CurrencyRateDV = Nothing
        Public selectedPageSize As Integer = DEFAULT_PAGE_SIZE
        Public SortExpression As String = CurrencyConversion.CurrencyRateDV.COL_DEALER_NAME
        Public HasDataChanged As Boolean

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

    Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
        Try
            Me.MenuEnabled = True
            Me.IsReturningFromChild = True
            If Me.State.searchDV Is Nothing Then
                Me.State.IsGridVisible = False
            Else
                Me.State.IsGridVisible = True
            End If
            Dim retObj As ExchangeRateForm.ReturnType = CType(ReturnPar, ExchangeRateForm.ReturnType)
            Me.State.HasDataChanged = retObj.HasDataChanged
            Select Case retObj.LastOperation
                Case ElitaPlusPage.DetailPageCommand.Back
                    If Not retObj Is Nothing Then
                            'If Not retObj.EditingBo.IsNew Then
                            Me.State.SelectedCurencyConversionId = retObj.EditingBo.Id
                            '    End If
                            Me.State.IsGridVisible = True
                            PopulateDealerDropDown()
                        End If
                    Case ElitaPlusPage.DetailPageCommand.Delete
                        Me.AddInfoMsg(Message.DELETE_RECORD_CONFIRMATION)
                End Select
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

#End Region

#Region "Page_Events"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
            Me.ErrorCtrl.Clear_Hide()
            ClearErrLabels()
            Try
                If Not Me.IsPostBack Then
                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    Me.AddCalendar(Me.btnFromDate, Me.txtFromDate)
                    Me.AddCalendar(Me.btnToDate, Me.txtToDate)
                    PopulateDealerDropDown()
                    If Me.State.IsGridVisible Then
                        If Not (Me.State.selectedPageSize = DEFAULT_PAGE_SIZE) Then
                            cboPageSize.SelectedValue = CType(Me.State.selectedPageSize, String)
                            Grid.PageSize = Me.State.selectedPageSize
                        End If
                        Me.PopulateGrid()
                    End If
                    Me.SetGridItemStyleColor(Me.Grid)
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
            Me.ShowMissingTranslations(Me.ErrorCtrl)
    End Sub
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

#Region "Controlling Logic"
    Sub PopulateDealerDropDown()

            Dim oDataView As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)
            TheDealerControl.Caption = "* " + TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_DEALERCODE)
            TheDealerControl.NothingSelected = True
            TheDealerControl.BindData(oDataView)
            TheDealerControl.SelectedGuid = Me.State.SelectedDealerId
            TheDealerControl.AutoPostBackDD = True
        End Sub

    Public Sub PopulateGrid()

            If ((Me.State.searchDV Is Nothing) OrElse (Me.State.HasDataChanged)) Then
                Me.State.SelectedDealerId = TheDealerControl.SelectedGuid
                Me.State.searchDV = Me.State.MyBO.GetList(Me.State.SelectedDealerId, Me.State.FromDateMask, Me.State.TomDateMask)
            End If

            ' If Me.State.searchDV.Count = 0 Then Exit Sub

            Me.State.searchDV.Sort = Me.State.SortExpression
            Me.Grid.AutoGenerateColumns = False
            'Me.Grid.Columns(Me.GRID_COL_DEALER_CODE_IDX).SortExpression = CurrencyConversion.CurrencyRateDV.COL_DEALER_CODE
            'Me.Grid.Columns(Me.GRID_COL_DEALER_NAME_IDX).SortExpression = CurrencyConversion.CurrencyRateDV.COL_DEALER_NAME
            Me.Grid.Columns(Me.GRID_COL_EFFECTIVE_IDX).SortExpression = CurrencyConversion.CurrencyRateDV.COL_EFFECTIVE
            ' Me.Grid.Columns(Me.GRID_COL_CURRENCY_CONVERSION_ID_IDX) = GetGuidStringFromByteArray(CType(dvRow(CurrencyConversion.CurrencyRateDV.COL_NAME_CURRENCY_CONVERSION_ID), Byte()))

            SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.SelectedCurencyConversionId, Me.Grid, Me.State.PageIndex)

            Me.SortAndBindGrid()

    End Sub

    Private Sub SortAndBindGrid()
        Me.State.PageIndex = Me.Grid.CurrentPageIndex
        Me.Grid.DataSource = Me.State.searchDV
        HighLightSortColumn(Grid, Me.State.SortExpression)
        Me.Grid.DataBind()

        ControlMgr.SetVisibleControl(Me, Grid, Me.State.IsGridVisible)

        ControlMgr.SetVisibleControl(Me, trPageSize, Me.Grid.Visible)

        Session("recCount") = Me.State.searchDV.Count

        If Me.State.searchDV.Count > 0 Then

            If Me.Grid.Visible Then
                    Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If
        Else
            If Me.Grid.Visible Then
                    Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If
        End If
    End Sub


    'This method will change the Page Index and the Selected Index
    Public Function FindDVSelectedRowIndex(ByVal dv As CurrencyConversion.CurrencyRateDV) As Integer
        Try
            If Me.State.SelectedCurencyConversionId.Equals(Guid.Empty) Then
                Return -1
            Else
                'Jump to the Right Page
                Dim i As Integer
                For i = 0 To dv.Count - 1
                    If New Guid(CType(dv(i)(CurrencyConversion.CurrencyRateDV.COL_NAME_CURRENCY_CONVERSION_ID), Byte())).Equals(Me.State.SelectedCurencyConversionId) Then
                        Return i
                    End If
                Next
            End If
            Return -1
        Catch ex As Exception
            Return -1
        End Try
    End Function


#End Region


#Region " Datagrid Related "

    'The Binding LOgic is here
    Private Sub Grid_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Grid.ItemDataBound
        Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
        Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)

        Try
            If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                e.Item.Cells(Me.GRID_COL_CURRENCY_CONVERSION_ID_IDX).Text = GetGuidStringFromByteArray(CType(dvRow(CurrencyConversion.CurrencyRateDV.COL_NAME_CURRENCY_CONVERSION_ID), Byte()))
                e.Item.Cells(Me.GRID_COL_DEALER_CODE_IDX).Text = dvRow(CurrencyConversion.CurrencyRateDV.COL_DEALER_CODE).ToString
                e.Item.Cells(Me.GRID_COL_DEALER_NAME_IDX).Text = dvRow(CurrencyConversion.CurrencyRateDV.COL_DEALER_NAME).ToString
                    e.Item.Cells(Me.GRID_COL_EFFECTIVE_IDX).Text = Me.GetDateFormattedString(DateHelper.GetDateValue(dvRow(CurrencyConversion.CurrencyRateDV.COL_EFFECTIVE).ToString()))
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Public Sub ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs)
        Try
            If e.CommandName = "SelectAction" Then
                Me.State.SelectedCurencyConversionId = New Guid(e.Item.Cells(Me.GRID_COL_CURRENCY_CONVERSION_ID_IDX).Text)
                Me.callPage(ExchangeRateForm.URL, Me.State.SelectedCurencyConversionId)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try

    End Sub

    Public Sub ItemCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        BaseItemCreated(sender, e)
    End Sub

    Private Sub Grid_SortCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles Grid.SortCommand
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
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub Grid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            Grid.CurrentPageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub Grid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Grid.PageIndexChanged
        Try
            Me.State.PageIndex = e.NewPageIndex
            Me.State.SelectedCurencyConversionId = Guid.Empty
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub
#End Region

#Region " Button Clicks "

        Private Sub btnSearch_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
            Try
                Me.State.PageIndex = 0
                Me.State.SelectedCurencyConversionId = Guid.Empty
                Me.State.IsGridVisible = True
                Me.State.searchDV = Nothing
                Me.State.HasDataChanged = False
                Me.State.MyBO = New CurrencyConversion

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
                    ValidateBeginEndDate(Me.lblFromDate, Me.txtFromDate.Text, Me.lblToDate, Me.txtToDate.Text)
                End If

                If txtFromDate.Text <> "" Then
                    Me.State.FromDateMask = DateHelper.GetDateValue(Me.txtFromDate.Text).ToString(SP_DATE_FORMAT)
                End If

                If txtToDate.Text <> "" Then
                    Me.State.TomDateMask = DateHelper.GetDateValue(Me.txtToDate.Text).ToString(SP_DATE_FORMAT)
                End If

                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub


        Private Sub btnAdd_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd_WRITE.Click
            Try
                Me.callPage(ExchangeRateForm.URL)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

        Private Sub btnClearSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClearSearch.Click
            Try
                ' Me.cboDealer.SelectedIndex = 0
                TheDealerControl.SelectedIndex = Me.BLANK_ITEM_SELECTED
                Me.txtFromDate.Text = String.Empty
                Me.txtToDate.Text = String.Empty
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

#End Region

#Region "Error Handling"


#End Region

    Public Shared Sub ValidateBeginEndDate(ByVal beginLbl As Label, ByVal beginDate As String, ByVal endLbl As Label, ByVal endDate As String)
        Dim tempEndDate As Date
        Dim tempBeginDate As Date

        GUIException.ValidateDate(beginLbl, beginDate, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_DATE_ERR)
            tempBeginDate = DateHelper.GetDateValue(beginDate)
        GUIException.ValidateDate(endLbl, endDate, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_DATE_ERR)
            tempEndDate = DateHelper.GetDateValue(endDate)
        If tempEndDate < tempBeginDate Then
            ElitaPlusPage.SetLabelError(beginLbl)
            ElitaPlusPage.SetLabelError(endLbl)
            Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_BEGIN_END_DATE_ERR)
        End If
    End Sub

        Private Sub ClearErrLabels()
            Me.ClearLabelErrSign(Me.lblFromDate)
            Me.ClearLabelErrSign(Me.lblToDate)
        End Sub

        Private Sub Grid_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Grid.SelectedIndexChanged

        End Sub

    End Class
End Namespace