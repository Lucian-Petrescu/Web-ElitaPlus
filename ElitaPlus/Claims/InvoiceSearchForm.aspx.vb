Imports Assurant.ElitaPlus.DALObjects
Imports System.Globalization
Imports System.Web.Services

Public Class InvoiceSearchForm
    Inherits ElitaPlusSearchPage

#Region "Constants"
    Public Const PAGETAB As String = "CLAIMS"
    Public Const PAGESUBTAB As String = "INVOICE"
    Public Const PAGETITLE As String = "INVOICE_SEARCH"
    Public Const URL As String = "~/Claims/InvoiceSearchForm.aspx"
#End Region

#Region "Return Type"
    Public Class InvoiceReturnType
        Public InvoiceNumber As SearchCriteriaStringType
        Public InvoiceAmount As SearchCriteriaStructType(Of Double)
        Public InvoiceDate As SearchCriteriaStructType(Of Date)
        Public BatchNumber As SearchCriteriaStringType
        Public ClaimNumber As SearchCriteriaStringType
        Public DateCreated As SearchCriteriaStructType(Of Date)
        Public AuthorizationNumber As SearchCriteriaStringType
        Public ServiceCenterName As String
        Public InvoiceId As Guid
    End Class
#End Region

#Region "Page State"
    Private IsReturningFromChild As Boolean = False
    Private ReturnObject As PageReturnType(Of InvoiceReturnType)

    Class MyState
        ' Selected Item Information
        Public PageIndex As Integer ' Stores Current Page Index
        Public PageSize As Integer = DEFAULT_NEW_UI_PAGE_SIZE ' Stores Current Page Size
        Public SortExpression As String = Invoice.InvoiceSearchDV.COL_NAME_INVOICE_NUMBER
        Public InvoiceResults As Invoice.InvoiceSearchDV
        Public InvoiceResultsPagedDataSource As New PagedDataSource

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
#End Region

#Region "Page Event Handlers"
    Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnParameter As Object) Handles Me.PageReturn
        MasterPage.MessageController.Clear()
        Try
            MenuEnabled = True
            IsReturningFromChild = True
            If (ReturnParameter Is Nothing) Then
                Exit Sub
            End If

            Dim returnObj As PageReturnType(Of InvoiceReturnType) = CType(ReturnParameter, PageReturnType(Of InvoiceReturnType))
            If returnObj.HasDataChanged Then
                State.InvoiceResults = Nothing
            End If
            ReturnObject = returnObj

            Select Case returnObj.LastOperation
                Case ElitaPlusPage.DetailPageCommand.Delete
                    MasterPage.MessageController.AddSuccess(Message.DELETE_RECORD_CONFIRMATION)
            End Select
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim updatePageIndex As Boolean = False
        Try
            If (IsReturningFromChild) Then
                If (ReturnObject IsNot Nothing AndAlso ReturnObject.EditingBo IsNot Nothing) Then
                    moInvoiceNumber.Value = ReturnObject.EditingBo.InvoiceNumber
                    moInvoiceAmount.Value = ReturnObject.EditingBo.InvoiceAmount
                    moInvoiceDate.Value = ReturnObject.EditingBo.InvoiceDate
                    moServiceCenter.Text = ReturnObject.EditingBo.ServiceCenterName
                    moBatchNumber.Value = ReturnObject.EditingBo.BatchNumber
                    moClaimNumber.Value = ReturnObject.EditingBo.ClaimNumber
                    moDateCreated.Value = ReturnObject.EditingBo.DateCreated
                    moAuthorizationNumber.Value = ReturnObject.EditingBo.AuthorizationNumber
                End If
            Else
                MasterPage.MessageController.Clear()
            End If
            MasterPage.MessageController.Clear()
            Form.DefaultButton = btnSearch.UniqueID

            If IsPostBack Then
                If ((Request("__EVENTARGUMENT") IsNot Nothing) AndAlso (Request("__EVENTARGUMENT").StartsWith(PAGE_COMMAND_NAME))) Then
                    State.PageIndex = Integer.Parse(Request("__EVENTARGUMENT").Split(New Char() {":"c})(1))
                End If
            End If

            UpdateBreadCrum()

            If (IsPostBack AndAlso State.InvoiceResults IsNot Nothing) Then PopulateGrid(updatePageIndex)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
        ShowMissingTranslations(MasterPage.MessageController)
    End Sub
#End Region

#Region "Events"
    Protected Sub cboPageSize_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            State.PageSize = CType(cboPageSize.SelectedValue, Integer)
            PopulateGrid(True)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub btnClear_Click(sender As Object, e As System.EventArgs) Handles btnClear.Click
        Try
            moInvoiceNumber.Clear()
            moInvoiceAmount.Clear()
            moInvoiceDate.Clear()
            moBatchNumber.Clear()
            moClaimNumber.Clear()
            moDateCreated.Clear()
            moAuthorizationNumber.Clear()
            moServiceCenter.Text = ""
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As System.EventArgs) Handles btnSearch.Click
        Try
            If (moInvoiceNumber.IsEmpty AndAlso moInvoiceAmount.IsEmpty AndAlso moInvoiceDate.IsEmpty AndAlso moBatchNumber.IsEmpty AndAlso _
                moClaimNumber.IsEmpty AndAlso moDateCreated.IsEmpty AndAlso moAuthorizationNumber.IsEmpty AndAlso _
                moServiceCenter.Text.Trim() = String.Empty) Then
                MasterPage.MessageController.AddErrorAndShow(ElitaPlus.Common.ErrorCodes.GUI_SEARCH_FIELD_NOT_SUPPLIED_ERR, True)
                Exit Sub
            End If
            Dim hasErrors As Boolean = False
            ' Validate each Search Criteria Supplied
            If (Not moInvoiceNumber.IsEmpty) Then hasErrors = hasErrors Or Not moInvoiceNumber.Validate()
            If (Not moInvoiceAmount.IsEmpty) Then hasErrors = hasErrors Or Not moInvoiceAmount.Validate()
            If (Not moInvoiceDate.IsEmpty) Then hasErrors = hasErrors Or Not moInvoiceDate.Validate()
            If (Not moBatchNumber.IsEmpty) Then hasErrors = hasErrors Or Not moBatchNumber.Validate()
            If (Not moClaimNumber.IsEmpty) Then hasErrors = hasErrors Or Not moClaimNumber.Validate()
            If (Not moDateCreated.IsEmpty) Then hasErrors = hasErrors Or Not moDateCreated.Validate()
            If (Not moAuthorizationNumber.IsEmpty) Then hasErrors = hasErrors Or Not moAuthorizationNumber.Validate()
            If (hasErrors) Then Exit Sub

            ' Reset the Caching on Search Results
            State.InvoiceResults = Nothing
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnAdd_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnAdd_WRITE.Click
        Try
            Dim returnType As InvoiceReturnType = New InvoiceReturnType()
            With returnType
                .InvoiceNumber = DirectCast(moInvoiceNumber.Value, SearchCriteriaStringType)
                .InvoiceAmount = DirectCast(moInvoiceAmount.Value, SearchCriteriaStructType(Of Double))
                .InvoiceDate = DirectCast(moInvoiceDate.Value, SearchCriteriaStructType(Of Date))
                .ServiceCenterName = moServiceCenter.Text
                .BatchNumber = DirectCast(moBatchNumber.Value, SearchCriteriaStringType)
                .ClaimNumber = DirectCast(moClaimNumber.Value, SearchCriteriaStringType)
                .DateCreated = DirectCast(moDateCreated.Value, SearchCriteriaStructType(Of Date))
                .AuthorizationNumber = DirectCast(moAuthorizationNumber.Value, SearchCriteriaStringType)
            End With
            callPage(InvoiceDetailForm.URL, returnType)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub moInvoiceNumber_Click(sender As Object, e As EventArgs)
        Try
            Dim returnType As InvoiceReturnType = New InvoiceReturnType()
            With returnType
                .InvoiceNumber = DirectCast(moInvoiceNumber.Value, SearchCriteriaStringType)
                .InvoiceAmount = DirectCast(moInvoiceAmount.Value, SearchCriteriaStructType(Of Double))
                .InvoiceDate = DirectCast(moInvoiceDate.Value, SearchCriteriaStructType(Of Date))
                .ServiceCenterName = moServiceCenter.Text
                .BatchNumber = DirectCast(moBatchNumber.Value, SearchCriteriaStringType)
                .ClaimNumber = DirectCast(moClaimNumber.Value, SearchCriteriaStringType)
                .DateCreated = DirectCast(moDateCreated.Value, SearchCriteriaStructType(Of Date))
                .AuthorizationNumber = DirectCast(moAuthorizationNumber.Value, SearchCriteriaStringType)
            End With

            With DirectCast(sender, LinkButton)
                returnType.InvoiceId = New Guid(.CommandArgument)
                callPage(InvoiceDetailForm.URL, returnType)
            End With
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub moInvoiceRepeater_ItemCommand(source As Object, e As System.Web.UI.WebControls.RepeaterCommandEventArgs) Handles moInvoiceRepeater.ItemCommand
        Select Case e.CommandName
            Case SORT_COMMAND_NAME
                If State.SortExpression.StartsWith(e.CommandArgument.ToString()) Then
                    If State.SortExpression.StartsWith(e.CommandArgument.ToString() & " DESC") Then
                        State.SortExpression = e.CommandArgument.ToString()
                    Else
                        State.SortExpression = e.CommandArgument.ToString() & " DESC"
                    End If
                Else
                    State.SortExpression = e.CommandArgument.ToString()
                End If
                PopulateGrid()
        End Select
    End Sub

    Protected Sub moInvoiceRepeater_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles moInvoiceRepeater.ItemDataBound
        Select Case e.Item.ItemType
            Case ListItemType.Item, ListItemType.AlternatingItem
                Dim label As Label
                Dim invoiceDr As DataRow = DirectCast(e.Item.DataItem, System.Data.DataRowView).Row
                DirectCast(e.Item.FindControl("moServiceCenter"), Label).Text = Invoice.InvoiceSearchDV.ServiceCenterDescription(invoiceDr)
                With DirectCast(e.Item.FindControl("moInvoiceNumber"), LinkButton)
                    .Text = Invoice.InvoiceSearchDV.InvoiceNumber(invoiceDr)
                    .CommandArgument = Invoice.InvoiceSearchDV.InvoiceId(invoiceDr).ToString()
                    .CommandName = "Invoice"
                End With
                DirectCast(e.Item.FindControl("moInvoiceDate"), Label).Text = GetDateFormattedStringNullable(Invoice.InvoiceSearchDV.InvoiceDate(invoiceDr).Value)
                DirectCast(e.Item.FindControl("moInvoiceAmount"), Label).Text = GetAmountFormattedString(Invoice.InvoiceSearchDV.InvoiceAmount(invoiceDr).Value)
                With DirectCast(e.Item.FindControl("moInvoiceStatus"), Label)
                    .Text = Invoice.InvoiceSearchDV.InvoiceStatus(invoiceDr)
                    Select Case Invoice.InvoiceSearchDV.InvoiceStatusCode(invoiceDr)
                        Case Codes.INVOICE_STATUS__BALANCED
                            .CssClass += " StatActive"
                        Case Codes.INVOICE_STATUS__OVER, Codes.INVOICE_STATUS__UNDER
                            .CssClass += " StatClosed"
                    End Select
                End With
            Case ListItemType.Header
                BaseItemCreated(DirectCast(sender, Repeater), e)
                HighLightSortColumn(DirectCast(e.Item.FindControl("moServiceCenterSort"), LinkButton), State.SortExpression, Invoice.InvoiceSearchDV.COL_NAME_SERVICE_CENTER_DESCRIPTION)
                HighLightSortColumn(DirectCast(e.Item.FindControl("moInvoiceNumberSort"), LinkButton), State.SortExpression, Invoice.InvoiceSearchDV.COL_NAME_INVOICE_NUMBER)
                HighLightSortColumn(DirectCast(e.Item.FindControl("moInvoiceDateSort"), LinkButton), State.SortExpression, Invoice.InvoiceSearchDV.COL_NAME_INVOICE_DATE)
                HighLightSortColumn(DirectCast(e.Item.FindControl("moInvoiceAmountSort"), LinkButton), State.SortExpression, Invoice.InvoiceSearchDV.COL_NAME_INVOICE_AMOUNT)
                HighLightSortColumn(DirectCast(e.Item.FindControl("moInvoiceStatusSort"), LinkButton), State.SortExpression, Invoice.InvoiceSearchDV.COL_NAME_INVOICE_STATUS)
            Case ListItemType.Footer
                BaseItemCreated(DirectCast(sender, Repeater), e)
        End Select
    End Sub
#End Region

#Region "Methods"
    Private Sub UpdateBreadCrum()
        MasterPage.BreadCrum = TranslationBase.TranslateLabelOrMessage(PAGETAB) & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage(PAGESUBTAB)
        MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(PAGETITLE)
        MasterPage.UsePageTabTitleInBreadCrum = False
    End Sub

    Private Sub PopulateGrid(Optional ByVal updatePageIndex As Boolean = False)
        Dim recCount As Integer
        Try
            If (State.InvoiceResults Is Nothing) Then
                State.InvoiceResults = Invoice.GetList(DirectCast(moInvoiceNumber.Value, SearchCriteriaStringType), _
                                                          DirectCast(moInvoiceAmount.Value, SearchCriteriaStructType(Of Double)), _
                                                          DirectCast(moInvoiceDate.Value, SearchCriteriaStructType(Of Date)), _
                                                          moServiceCenter.Text, _
                                                          DirectCast(moBatchNumber.Value, SearchCriteriaStringType), _
                                                          DirectCast(moClaimNumber.Value, SearchCriteriaStringType), _
                                                          DirectCast(moDateCreated.Value, SearchCriteriaStructType(Of Date)), _
                                                          DirectCast(moAuthorizationNumber.Value, SearchCriteriaStringType))
                updatePageIndex = True
            End If
            recCount = State.InvoiceResults.Count
            If (updatePageIndex) Then
                State.PageIndex = NewCurrentPageIndex(State.PageIndex, State.InvoiceResultsPagedDataSource.PageSize, State.PageSize)
            End If

            State.InvoiceResults.Sort = State.SortExpression
            State.InvoiceResultsPagedDataSource.DataSource = State.InvoiceResults
            State.InvoiceResultsPagedDataSource.AllowPaging = True
            State.InvoiceResultsPagedDataSource.PageSize = State.PageSize
            State.InvoiceResultsPagedDataSource.CurrentPageIndex = State.PageIndex
            moInvoiceRepeater.DataSource = State.InvoiceResultsPagedDataSource
            moInvoiceRepeater.DataBind()

            MasterPage.MessageController.Clear()
            If (recCount = 0) Then
                MasterPage.MessageController.AddInformation(Message.MSG_NO_RECORDS_FOUND, True)
                ControlMgr.SetVisibleControl(Me, moSearchResults, False)
            Else
            ControlMgr.SetVisibleControl(Me, moSearchResults, True)
                lblRecordCount.Text = recCount & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
#End Region

#Region "Web Methods"
    <WebMethod(), Script.Services.ScriptMethod()> _
    Public Shared Function GetAuthorizations(invoiceId As String) As String
        Try
            Dim invoiceAuthorizations As Invoice.InvoiceAuthorizationSearchDV
            Dim ds As New DataSet
            ds.DataSetName = "InvoiceAuthorizationDs"

            Dim dtHeaders As New DataTable("Headers")
            dtHeaders.Columns.Add("UI_PROG_CODE", GetType(String))
            dtHeaders.Columns.Add("TRANSLATION", GetType(String))
            ds.Tables.Add(dtHeaders)
            dtHeaders.Rows.Add(New String() {"CLAIM_NUMBER", TranslationBase.TranslateLabelOrMessage("CLAIM_NUMBER")})
            dtHeaders.Rows.Add(New String() {"AUTHORIZATION_NUMBER", TranslationBase.TranslateLabelOrMessage("AUTHORIZATION_NUMBER")})
            dtHeaders.Rows.Add(New String() {"BATCH_NUMBER", TranslationBase.TranslateLabelOrMessage("BATCH_NUMBER")})
            dtHeaders.Rows.Add(New String() {"AUTHORIZATION_STATUS", TranslationBase.TranslateLabelOrMessage("AUTHORIZATION_STATUS")})
            dtHeaders.Rows.Add(New String() {"INVOICE_AUTHORIZATION_AMOUNT", TranslationBase.TranslateLabelOrMessage("INVOICE_AUTHORIZATION_AMOUNT")})

            invoiceAuthorizations = Invoice.GetAuthorizationList(New Guid(invoiceId))
            invoiceAuthorizations.Table.TableName = "InvoiceAuthorization"
            ds.Tables.Add(invoiceAuthorizations.Table.Copy())

            Return ds.GetXml()
        Catch ex As Exception
            Return "Error!"
        End Try
    End Function

#End Region

End Class