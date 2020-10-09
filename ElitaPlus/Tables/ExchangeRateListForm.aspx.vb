Imports Microsoft.VisualBasic
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common

Namespace Tables


Partial Class ExchangeRateListForm
    Inherits ElitaPlusSearchPage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

    Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
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

    Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles MyBase.PageReturn
        Try
            MenuEnabled = True
            IsReturningFromChild = True
            If State.searchDV Is Nothing Then
                State.IsGridVisible = False
            Else
                State.IsGridVisible = True
            End If
            Dim retObj As ExchangeRateForm.ReturnType = CType(ReturnPar, ExchangeRateForm.ReturnType)
            State.HasDataChanged = retObj.HasDataChanged
            Select Case retObj.LastOperation
                Case ElitaPlusPage.DetailPageCommand.Back
                    If retObj IsNot Nothing Then
                            'If Not retObj.EditingBo.IsNew Then
                            State.SelectedCurencyConversionId = retObj.EditingBo.Id
                            '    End If
                            State.IsGridVisible = True
                            PopulateDealerDropDown()
                        End If
                    Case ElitaPlusPage.DetailPageCommand.Delete
                        AddInfoMsg(Message.DELETE_RECORD_CONFIRMATION)
                End Select
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

#End Region

#Region "Page_Events"
    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
            ErrorCtrl.Clear_Hide()
            ClearErrLabels()
            Try
                If Not IsPostBack Then
                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    AddCalendar(btnFromDate, txtFromDate)
                    AddCalendar(btnToDate, txtToDate)
                    PopulateDealerDropDown()
                    If State.IsGridVisible Then
                        If Not (State.selectedPageSize = DEFAULT_PAGE_SIZE) Then
                            cboPageSize.SelectedValue = CType(State.selectedPageSize, String)
                            Grid.PageSize = State.selectedPageSize
                        End If
                        PopulateGrid()
                    End If
                    SetGridItemStyleColor(Grid)
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
            ShowMissingTranslations(ErrorCtrl)
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
            TheDealerControl.SelectedGuid = State.SelectedDealerId
            TheDealerControl.AutoPostBackDD = True
        End Sub

    Public Sub PopulateGrid()

            If ((State.searchDV Is Nothing) OrElse (State.HasDataChanged)) Then
                State.SelectedDealerId = TheDealerControl.SelectedGuid
                State.searchDV = State.MyBO.GetList(State.SelectedDealerId, State.FromDateMask, State.TomDateMask)
            End If

            ' If Me.State.searchDV.Count = 0 Then Exit Sub

            State.searchDV.Sort = State.SortExpression
            Grid.AutoGenerateColumns = False
            'Me.Grid.Columns(Me.GRID_COL_DEALER_CODE_IDX).SortExpression = CurrencyConversion.CurrencyRateDV.COL_DEALER_CODE
            'Me.Grid.Columns(Me.GRID_COL_DEALER_NAME_IDX).SortExpression = CurrencyConversion.CurrencyRateDV.COL_DEALER_NAME
            Grid.Columns(GRID_COL_EFFECTIVE_IDX).SortExpression = CurrencyConversion.CurrencyRateDV.COL_EFFECTIVE
            ' Me.Grid.Columns(Me.GRID_COL_CURRENCY_CONVERSION_ID_IDX) = GetGuidStringFromByteArray(CType(dvRow(CurrencyConversion.CurrencyRateDV.COL_NAME_CURRENCY_CONVERSION_ID), Byte()))

            SetPageAndSelectedIndexFromGuid(State.searchDV, State.SelectedCurencyConversionId, Grid, State.PageIndex)

            SortAndBindGrid()

    End Sub

    Private Sub SortAndBindGrid()
        State.PageIndex = Grid.CurrentPageIndex
        Grid.DataSource = State.searchDV
        HighLightSortColumn(Grid, State.SortExpression)
        Grid.DataBind()

        ControlMgr.SetVisibleControl(Me, Grid, State.IsGridVisible)

        ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)

        Session("recCount") = State.searchDV.Count

        If State.searchDV.Count > 0 Then

            If Grid.Visible Then
                    lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If
        Else
            If Grid.Visible Then
                    lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If
        End If
    End Sub


    'This method will change the Page Index and the Selected Index
    Public Function FindDVSelectedRowIndex(dv As CurrencyConversion.CurrencyRateDV) As Integer
        Try
            If State.SelectedCurencyConversionId.Equals(Guid.Empty) Then
                Return -1
            Else
                'Jump to the Right Page
                Dim i As Integer
                For i = 0 To dv.Count - 1
                    If New Guid(CType(dv(i)(CurrencyConversion.CurrencyRateDV.COL_NAME_CURRENCY_CONVERSION_ID), Byte())).Equals(State.SelectedCurencyConversionId) Then
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
    Private Sub Grid_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Grid.ItemDataBound
        Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
        Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)

        Try
            If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem Then
                e.Item.Cells(GRID_COL_CURRENCY_CONVERSION_ID_IDX).Text = GetGuidStringFromByteArray(CType(dvRow(CurrencyConversion.CurrencyRateDV.COL_NAME_CURRENCY_CONVERSION_ID), Byte()))
                e.Item.Cells(GRID_COL_DEALER_CODE_IDX).Text = dvRow(CurrencyConversion.CurrencyRateDV.COL_DEALER_CODE).ToString
                e.Item.Cells(GRID_COL_DEALER_NAME_IDX).Text = dvRow(CurrencyConversion.CurrencyRateDV.COL_DEALER_NAME).ToString
                    e.Item.Cells(GRID_COL_EFFECTIVE_IDX).Text = GetDateFormattedString(DateHelper.GetDateValue(dvRow(CurrencyConversion.CurrencyRateDV.COL_EFFECTIVE).ToString()))
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Public Sub ItemCommand(source As System.Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs)
        Try
            If e.CommandName = "SelectAction" Then
                State.SelectedCurencyConversionId = New Guid(e.Item.Cells(GRID_COL_CURRENCY_CONVERSION_ID_IDX).Text)
                callPage(ExchangeRateForm.URL, State.SelectedCurencyConversionId)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try

    End Sub

    Public Sub ItemCreated(sender As System.Object, e As System.Web.UI.WebControls.DataGridItemEventArgs)
        BaseItemCreated(sender, e)
    End Sub

    Private Sub Grid_SortCommand(source As System.Object, e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles Grid.SortCommand
        Try
            If State.SortExpression.StartsWith(e.SortExpression) Then
                If State.SortExpression.EndsWith(" DESC") Then
                    State.SortExpression = e.SortExpression
                Else
                    State.SortExpression &= " DESC"
                End If
            Else
                State.SortExpression = e.SortExpression
            End If
            State.PageIndex = 0
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub Grid_PageSizeChanged(source As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            Grid.CurrentPageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub Grid_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Grid.PageIndexChanged
        Try
            State.PageIndex = e.NewPageIndex
            State.SelectedCurencyConversionId = Guid.Empty
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub
#End Region

#Region " Button Clicks "

        Private Sub btnSearch_Click1(sender As Object, e As System.EventArgs) Handles btnSearch.Click
            Try
                State.PageIndex = 0
                State.SelectedCurencyConversionId = Guid.Empty
                State.IsGridVisible = True
                State.searchDV = Nothing
                State.HasDataChanged = False
                State.MyBO = New CurrencyConversion

                If txtFromDate.Text <> "" AndAlso txtToDate.Text = "" Then
                    txtToDate = txtFromDate
                End If

                If txtToDate.Text <> "" AndAlso txtFromDate.Text = "" Then
                    txtFromDate = txtToDate
                End If

                If TheDealerControl.SelectedGuid.Equals(Guid.Empty) Then
                    Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
                End If
                'Dates
                'High date must be higher than low date.
                If txtFromDate.Text <> "" OrElse txtToDate.Text <> "" Then
                    ValidateBeginEndDate(lblFromDate, txtFromDate.Text, lblToDate, txtToDate.Text)
                End If

                If txtFromDate.Text <> "" Then
                    State.FromDateMask = DateHelper.GetDateValue(txtFromDate.Text).ToString(SP_DATE_FORMAT)
                End If

                If txtToDate.Text <> "" Then
                    State.TomDateMask = DateHelper.GetDateValue(txtToDate.Text).ToString(SP_DATE_FORMAT)
                End If

                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub


        Private Sub btnAdd_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnAdd_WRITE.Click
            Try
                callPage(ExchangeRateForm.URL)
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

        Private Sub btnClearSearch_Click(sender As Object, e As System.EventArgs) Handles btnClearSearch.Click
            Try
                ' Me.cboDealer.SelectedIndex = 0
                TheDealerControl.SelectedIndex = BLANK_ITEM_SELECTED
                txtFromDate.Text = String.Empty
                txtToDate.Text = String.Empty
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

#End Region

#Region "Error Handling"


#End Region

    Public Shared Sub ValidateBeginEndDate(beginLbl As Label, beginDate As String, endLbl As Label, endDate As String)
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
            ClearLabelErrSign(lblFromDate)
            ClearLabelErrSign(lblToDate)
        End Sub

        Private Sub Grid_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles Grid.SelectedIndexChanged

        End Sub

    End Class
End Namespace