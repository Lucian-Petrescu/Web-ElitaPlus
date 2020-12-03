Imports System.Collections.Generic
Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports Assurant.Elita.Web.Forms

Public Class ArInvoiceListForm
    Inherits ElitaPlusSearchPage

#Region "Constants"


    Public Const PageTitle = "AR_INVOICE_SEARCH"
    Public Const PageTab As String = "INTERFACES"

    Public Const GridColCtrlInvoiceNumber As String = "btnInvoiceDetails"
    Public Const GridColCtrlSelect As String = "checkBoxSelected"
    Public Const GridColCtrlSelectAll As String = "checkBoxAll"

    Public Const GridColIdxCheckbox As Integer = 0
    Public Const GridColIdxDealer As Integer = 1
    Public Const GridColIdxInvoiceNumber As Integer = 2
    Public Const GridColIdxInvoiceDate As Integer = 3
    Public Const GridColIdxInvoiceAmount As Integer = 4
    Public Const GridColIdxDocumentType As Integer = 5
    Public Const GridColIdxReference As Integer = 6
    Public Const GridColIdxStatus As Integer = 7
    Public Const GridColIdxSource As Integer = 8
    Public Const GridColIdxDocumentUniqueId As Integer = 9
    Public Const GridColIdxInvoiceHeaderId As Integer = 10
    Public Const GridColIdxAllowSelection As Integer = 11

    Public Const MaxResultRowCount As Integer = 200

    Public Const InvoiceStatusXcdPending As String = "CMSTAUS-PENDING"
    Public Const AllowSelectionYes As String = "Y"
    Public Const AllowSelectionNo As String = "N"

#End Region

#Region "Page Return Type"
    Public Class ReturnType
        Public DataChanged As Boolean = False
        Public Sub New(ByVal boChanged As Boolean)
            DataChanged = boChanged
        End Sub
    End Class
#End Region

#Region "Page State"

    'Private _isReturningFromChild As Boolean = False

    Class ArInvoiceSearchCriteria
        Public CompanyId As Guid = Guid.Empty
        Public DealerId as Guid?
        Public InvoiceNumber As String = String.Empty
        Public Source As String = String.Empty
        Public Reference As String = String.Empty
        Public ReferenceNumber As String = String.Empty
        Public DocumentType As String = String.Empty
        Public StatusXcd As String = String.Empty
        Public DocumentUniqueId As String = String.Empty

        Private _invoiceDateString As String = String.Empty
        Private _invoiceDate As Date?


        Public Property InvoiceDateString As String
            Set(value As String)
                _invoiceDateString = value
                Dim dt As Date
                If DateTime.TryParseExact(_invoiceDateString.Trim(), DATE_FORMAT, Threading.Thread.CurrentThread.CurrentCulture, DateTimeStyles.None, dt) Then
                    _invoiceDate = dt
                End If
            End Set
            Get
                Return _invoiceDateString
            End Get
        End Property

        Public ReadOnly Property InvoiceDate As Date?
            Get
                Return _invoiceDate
            End Get
        End Property

        Public Sub New()
        End Sub
    End Class

    Class MyState
        Public PageIndex As Integer = 0
        Public SelectedInvoiceId As Guid = Guid.Empty

        Public IsGridVisible As Boolean = False
        Public SelectedPageSize As Integer = DEFAULT_NEW_UI_PAGE_SIZE
        Public SearchDv As ArInvoiceHeader.ArInvoiceSearchDv= Nothing
        Public Criteria As ArInvoiceSearchCriteria = New ArInvoiceSearchCriteria()
        Public GridSortExpression as String = "dealer_code" 'default sort by dealer

        Public InvoiceToBeReviewed As List(Of Guid)

        Public Sub New()
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

#Region "Page Events"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        'Put user code to initialize the page here
        ClientScript.RegisterHiddenField("__EVENTTARGET", btnSearch.ClientID)
        MasterPage.MessageController.Clear_Hide()

        Try
            If Not IsPostBack Then
                UpdateBreadCrumb()
                SetDefaultButtonToControls()

                ControlMgr.SetVisibleControl(Me, trPageSize, False)
                AddCalendar(ImageInvoiceDate, txtInvoiceDate)
                SetFormTitle(PageTitle)
                SetFormTab(PageTab)

                PopulateDropDown()
                PopulateSearchFieldsFromState()

                TranslateGridHeader(Grid)

                If State.IsGridVisible Then
                    cboPageSize.SelectedValue = CType(State.selectedPageSize, String)
                    Grid.PageSize = State.selectedPageSize
                    PopulateGrid()
                    SetGridItemStyleColor(Grid)
                End If

                SetFocus(txtInvoiceNumber)
            End If
            ClearLabelError(lblInvoiceDate)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
        ShowMissingTranslations(MasterPage.MessageController)
    End Sub

    Private Sub Page_PageReturn(ByVal returnFromUrl As String, ByVal returnPar As Object) Handles MyBase.PageReturn
        Try
            MenuEnabled = True
            '_isReturningFromChild = True
            Dim retObj As ReturnType = CType(ReturnedValues, ReturnType)
            If Not retObj Is Nothing AndAlso retObj.DataChanged Then
                'refresh the search since invoice data changed
                State.SearchDv = Nothing
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Private Sub ArInvoiceListForm_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
        
        Dim searchResultCount As Integer = 0
        If State.IsGridVisible = True AndAlso Not State.SearchDv Is Nothing AndAlso State.SearchDv.Count > 0 Then
            'add CCS class to check box so that jquery script will work correctly
            Dim chkBox As CheckBox, strAllowSelection as String
            chkBox = CType(Grid.HeaderRow.FindControl(GridColCtrlSelectAll), CheckBox)
            If (chkBox IsNot Nothing) Then
                chkBox.InputAttributes("class") = "checkboxHeader"
            End If

            For Each row As GridViewRow In Grid.Rows
                If row.RowType = DataControlRowType.DataRow Then
                    chkBox = CType(row.Cells(GridColIdxCheckbox).FindControl(GridColCtrlSelect), CheckBox)
                    strAllowSelection = row.Cells(GridColIdxAllowSelection).Text
                    If (chkBox IsNot Nothing) Then
                        If strAllowSelection = AllowSelectionYes Then
                            chkBox.InputAttributes("class") = "checkboxLine"
                            searchResultCount = searchResultCount + 1
                        Else 
                            chkBox.InputAttributes("style") = "display:none;"
                        End If
                    End If
                End If
            Next
        End If
        if searchResultCount > 0 Then
            btnReviewDecision_WRITE.Visible = True
        Else 
            btnReviewDecision_WRITE.Visible = False
        End If
    End Sub

#End Region
    

#Region "Controlling logic"
    Private Sub UpdateBreadCrumb()
        If (Not State Is Nothing) Then
            If (Not State Is Nothing) Then
                MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator &
                                          TranslationBase.TranslateLabelOrMessage(PageTitle)
                MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(PageTitle)
            End If
        End If
    End Sub

    Private Sub SetDefaultButtonToControls()
        Try
            SetDefaultButton(ddlCompany, btnSearch)
            SetDefaultButton(ddlDealer, btnSearch)
            SetDefaultButton(ddlDocType, btnSearch)
            SetDefaultButton(ddlReference, btnSearch)
            SetDefaultButton(ddlStatus, btnSearch)

            SetDefaultButton(txtInvoiceNumber, btnSearch)
            SetDefaultButton(txtSource, btnSearch)
            SetDefaultButton(txtInvoiceDate, btnSearch)
            SetDefaultButton(txtDocumentUniqueId, btnSearch)
            SetDefaultButton(txtReferenceNumber, btnSearch)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub PopulateSearchFieldsFromState()

        Try
            If Not State.Criteria Is Nothing Then
                with State.Criteria
                    txtInvoiceNumber.Text = .InvoiceNumber
                    txtSource.Text = .Source
                    txtInvoiceDate.Text = .InvoiceDateString
                    txtDocumentUniqueId.Text = .DocumentUniqueId
                    txtReferenceNumber.Text = .ReferenceNumber

                    ddlCompany.SelectedValue = .CompanyId.ToString()
                    ddlDealer.SelectedValue = .DealerId.ToString()
                    ddlDocType.SelectedValue = .DocumentType
                    ddlReference.SelectedValue = .Reference
                    ddlStatus.SelectedValue = .StatusXcd
                End With
                
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Private Sub PopulateGrid()

        Try

            If State.SearchDv Is Nothing Then

                State.SearchDv = ArInvoiceHeader.GetArInvoices(
                    State.Criteria.CompanyId,State.Criteria.DealerId,
                    State.Criteria.InvoiceNumber.Trim().ToUpper(),
                    State.Criteria.Source.Trim().ToUpper(),
                    State.Criteria.InvoiceDate,
                    State.Criteria.Reference,
                    State.Criteria.ReferenceNumber.Trim().ToUpper(),
                    State.Criteria.DocumentType,
                    State.Criteria.DocumentUniqueId.Trim().ToUpper(),
                    State.Criteria.StatusXcd,
                    MaxResultRowCount
                    )
            End If

            Grid.PageSize = State.selectedPageSize

            SetPageAndSelectedIndexFromGuid(State.SearchDv, State.SelectedInvoiceId, Grid, State.PageIndex)
            State.SearchDv.Sort = State.GridSortExpression
            Grid.DataSource = State.SearchDv
            State.PageIndex = Grid.PageIndex

            HighLightSortColumn(Grid, State.GridSortExpression, IsNewUI)

            Grid.DataBind()

            ControlMgr.SetVisibleControl(Me, Grid, State.IsGridVisible)

            ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)

            Session("recCount") = State.SearchDv.Count

            If Grid.Visible Then
                lblRecordCount.Text = String.Format("{0} {1} {2} {3}", 
                                                    State.SearchDv.TotalCount, 
                                                    TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND), 
                                                    State.SearchDv.Count, 
                                                    TranslationBase.TranslateLabelOrMessage("MSG_RECORDS_DISPLAYED"))
            End If

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

    Private Function PopulateStateFromSearchFields() As Boolean
        Dim dt As Date, blnSuccess As Boolean = True

        Try
            State.Criteria = New ArInvoiceSearchCriteria()
            With State.Criteria
                .CompanyId = GetSelectedItem(ddlCompany)
                .DealerId = GetSelectedItem(ddlDealer)
                If .DealerId = Guid.Empty Then
                    .DealerId = nothing
                End If 
                .Reference = ddlReference.SelectedValue
                .StatusXcd = ddlStatus.SelectedValue
                .DocumentType = ddlDocType.SelectedValue

                .InvoiceNumber = txtInvoiceNumber.Text
                .Source = txtSource.Text
                .ReferenceNumber = txtReferenceNumber.Text
                .DocumentUniqueId = txtDocumentUniqueId.Text
                
                If String.IsNullOrWhiteSpace(txtInvoiceDate.Text) = False Then
                    If Date.TryParseExact(txtInvoiceDate.Text.Trim(), DATE_FORMAT, Threading.Thread.CurrentThread.CurrentCulture, DateTimeStyles.None, dt) Then
                        If (dt <> Date.MinValue) Then
                            .InvoiceDateString = txtInvoiceDate.Text.Trim()
                        End If
                    Else
                        SetLabelError(lblInvoiceDate)
                        Throw New GUIException(Message.MSG_INVALID_DATE, ElitaPlus.Common.ErrorCodes.INVALID_DATE_ERR)
                    End If
                End If
            End With
            
            If blnSuccess = True Then
                'check at least one search condition is populated
                Dim hasValue As Boolean = False
                With State.Criteria
                    If .DealerId.HasValue AndAlso .DealerId.Value <> guid.Empty Then hasValue = True
                    If .InvoiceDate.HasValue = True Then hasValue = True
                    If String.IsNullOrWhiteSpace(.InvoiceNumber) = False Then hasValue = True
                    If String.IsNullOrWhiteSpace(.Source) = False Then hasValue = True
                    If String.IsNullOrWhiteSpace(.ReferenceNumber) = False Then hasValue = True
                    If String.IsNullOrWhiteSpace(.Reference) = False Then hasValue = True
                    If String.IsNullOrWhiteSpace(.DocumentType) = False Then hasValue = True
                    If String.IsNullOrWhiteSpace(.StatusXcd) = False Then hasValue = True
                    If String.IsNullOrWhiteSpace(.DocumentUniqueId) = False Then hasValue = True
                End With
                If hasValue = False Then
                    blnSuccess = False
                    MasterPage.MessageController.AddErrorAndShow("SEARCH_CRITERION_001", True)
                End If
            End If
        Catch ex As Exception
            blnSuccess = False
            HandleErrors(ex, MasterPage.MessageController)
        End Try

        Return blnSuccess
    End Function

    Private Sub RefreshSearchGrid()
        State.PageIndex = 0
        State.SelectedInvoiceId = Guid.Empty
        State.IsGridVisible = True
        State.SearchDv = Nothing
        PopulateGrid()
    End Sub

    Private Sub PopulateDealerDropdown()
        Dim oDealerList As ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="DealerListByCompany", 
                                                                                        context:=New ListContext() With
                                                                                           {
                                                                                           .CompanyId = GetSelectedItem(ddlCompany)
                                                                                           })
        

        Dim dealerTextFunc As Func(Of ListItem, String) = Function(li As ListItem)
                                                                  Return li.Code + " - " + li.Translation
                                                              End Function
        ddlDealer.Populate(oDealerList, New PopulateOptions() With
                                  {
                                  .AddBlankItem = True,
                                  .TextFunc = dealerTextFunc,
                                  .SortFunc = dealerTextFunc
                                  })

    End Sub
    Private Sub PopulateCompanyDropdown()
        Dim companyList As ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="Company")

        Dim filteredCompanyList As ListItem() = (From x In companyList
                                                 Where Authentication.CurrentUser.Companies.Contains(x.ListItemId)
                                                 Select x).ToArray()

        Dim companyTextFunc As Func(Of ListItem, String) = Function(li As ListItem)
                                                               Return li.Code + " - " + li.Translation
                                                           End Function

        ddlCompany.Populate(filteredCompanyList, New PopulateOptions() With
                               {
                               .AddBlankItem = False,
                               .TextFunc = companyTextFunc
                               })

        If State IsNot Nothing AndAlso State.Criteria isNot Nothing AndAlso State.Criteria.CompanyId <> guid.Empty Then
            Try
                SetSelectedItem(ddlCompany, State.Criteria.CompanyId)
            Catch ex As Exception
                ddlCompany.SelectedIndex = 0
            End Try
        Else
            ddlCompany.SelectedIndex = 0
        End If
    End Sub

    Private Sub PopulateDropDown()

        Try
            PopulateCompanyDropdown()
            PopulateDealerDropdown()
            
            ddlStatus.Populate(CommonConfigManager.Current.ListManager.GetList($"CMSTAUS", Authentication.CurrentUser.LanguageCode), New PopulateOptions() With
                           {
                               .AddBlankItem = True,
                               .BlankItemValue = String.Empty,
                               .TextFunc = AddressOf PopulateOptions.GetDescription,
                               .ValueFunc = AddressOf PopulateOptions.GetExtendedCode,
                               .SortFunc = AddressOf PopulateOptions.GetDescription
                           })


        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

        'add hard-coded dropdown list
        ddlDocType.Items.Add(New WebControls.ListItem() With {.Value = String.Empty, .Text = String.Empty, .Selected = True})
        ddlDocType.Items.Add(New WebControls.ListItem() With {.Value = $"CM", .Text = TranslationBase.TranslateLabelOrMessage("CREDIT_MEMO")})
        ddlDocType.Items.Add(New WebControls.ListItem() With {.Value = $"INV", .Text = TranslationBase.TranslateLabelOrMessage("INVOICE")})

        ddlReference.Items.Add(New WebControls.ListItem() With {.Value = String.Empty, .Text = String.Empty, .Selected = True})
        ddlReference.Items.Add(New WebControls.ListItem() With {.Value = $"ELP_CERT", .Text = TranslationBase.TranslateLabelOrMessage("CERTIFICATE")})
        ddlReference.Items.Add(New WebControls.ListItem() With {.Value = $"ELP_CLAIM", .Text = TranslationBase.TranslateLabelOrMessage("CLAIM")})
        ddlReference.Items.Add(New WebControls.ListItem() With {.Value = $"ELP_CLAIM_AUTHORIZATION", .Text = TranslationBase.TranslateLabelOrMessage("CLAIM_AUTHORIZATION")})

        ddlReviewDecision.Items.Add(New WebControls.ListItem() With {.Value = String.Empty, .Text = String.Empty, .Selected = True})
        ddlReviewDecision.Items.Add(New WebControls.ListItem() With {.Value = $"A", .Text = TranslationBase.TranslateLabelOrMessage("APPROVE")})
        ddlReviewDecision.Items.Add(New WebControls.ListItem() With {.Value = $"D", .Text = TranslationBase.TranslateLabelOrMessage("DENY")})

    End Sub

#End Region

#Region "button handling"
    Private Sub btnClearSearch_Click(sender As Object, e As EventArgs) Handles btnClearSearch.Click
        Try
            'clear state
            State.Criteria = New ArInvoiceSearchCriteria()
            State.SearchDv = Nothing

            'clear search controls
            txtInvoiceNumber.Text = String.Empty
            txtSource.Text = String.Empty
            txtInvoiceDate.Text = String.Empty
            txtReferenceNumber.Text = String.Empty
            txtDocumentUniqueId.Text = String.Empty

            ddlDealer.SelectedIndex = NO_ITEM_SELECTED_INDEX
            ddlDocType.SelectedIndex = NO_ITEM_SELECTED_INDEX
            ddlReference.SelectedIndex = NO_ITEM_SELECTED_INDEX
            ddlStatus.SelectedIndex = NO_ITEM_SELECTED_INDEX
            
            'hide search results
            ControlMgr.SetVisibleControl(Me, Grid, False)
            ControlMgr.SetVisibleControl(Me, trPageSize, False)

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            ControlMgr.SetVisibleControl(Me, Grid, False)
            ControlMgr.SetVisibleControl(Me, trPageSize, False)
            State.IsGridVisible = False
 
            If PopulateStateFromSearchFields() Then
                RefreshSearchGrid()
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

    Private Sub ddlCompany_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCompany.SelectedIndexChanged
        PopulateDealerDropdown()
    End Sub

#End Region


#Region "Grid Event handlers"

    Private Sub Grid_PageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs) Handles Grid.PageIndexChanging
        Try
            Grid.PageIndex = e.NewPageIndex
            State.PageIndex = Grid.PageIndex
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles Grid.RowDataBound
        Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
        Dim btnEditItem As LinkButton
        Dim strTemp As String, strStatusXcd as String
        Try

            If e.Row.RowType = DataControlRowType.DataRow Then
                If (e.Row.Cells(GridColIdxInvoiceNumber).FindControl(GridColCtrlInvoiceNumber) IsNot Nothing) Then
                    btnEditItem = CType(e.Row.Cells(GridColIdxInvoiceNumber).FindControl(GridColCtrlInvoiceNumber), LinkButton)
                    btnEditItem.CommandArgument = e.Row.RowIndex.ToString
                    btnEditItem.CommandName = SELECT_COMMAND_NAME
                    btnEditItem.Text = dvRow(ArInvoiceHeader.ArInvoiceSearchDV.ColInvoiceNumber).ToString
                    If String.IsNullOrWhiteSpace(btnEditItem.Text) Then 'invoice is empty
                        btnEditItem.Text = String.Format("({0})",  TranslationBase.TranslateLabelOrMessage("INVOICE_DETAIL"))
                    End If
                End If

                ' populate invoice date
                If Not dvRow(ArInvoiceHeader.ArInvoiceSearchDV.ColInvoiceDate) Is DBNull.Value Then
                    PopulateControlFromBOProperty(e.Row.Cells(GridColIdxInvoiceDate), GetDateFormattedString(CType(dvRow(ArInvoiceHeader.ArInvoiceSearchDV.ColInvoiceDate), Date)))
                End If

                'populate field needs translations
                If Not dvRow(ArInvoiceHeader.ArInvoiceSearchDV.ColDocumentType) Is DBNull.Value Then
                    strTemp = dvRow(ArInvoiceHeader.ArInvoiceSearchDV.ColDocumentType)
                    PopulateControlFromBOProperty(e.Row.Cells(GridColIdxDocumentType), GetDescriptionFromDropdownCode(ddlDocType, strTemp))
                End If

                If Not dvRow(ArInvoiceHeader.ArInvoiceSearchDV.ColStatusXcd) Is DBNull.Value Then
                    strStatusXcd = dvRow(ArInvoiceHeader.ArInvoiceSearchDV.ColStatusXcd)
                    PopulateControlFromBOProperty(e.Row.Cells(GridColIdxStatus), GetDescriptionFromDropdownCode(ddlStatus, strStatusXcd))
                End If

                if strStatusXcd = InvoiceStatusXcdPending Then
                    e.Row.Cells(GridColIdxAllowSelection).Text = AllowSelectionYes
                Else 
                    e.Row.Cells(GridColIdxAllowSelection).Text = AllowSelectionNo
                End If

                If Not dvRow(ArInvoiceHeader.ArInvoiceSearchDV.ColReference) Is DBNull.Value Then
                    strTemp = GetDescriptionFromDropdownCode(ddlReference, dvRow(ArInvoiceHeader.ArInvoiceSearchDV.ColReference))
                    PopulateControlFromBOProperty(e.Row.Cells(GridColIdxReference), strTemp + " - " + dvRow(ArInvoiceHeader.ArInvoiceSearchDV.ColReferenceNumber))
                End If

                'populate the id field
                PopulateControlFromBOProperty(e.Row.Cells(GridColIdxInvoiceHeaderId), dvRow(ArInvoiceHeader.ArInvoiceSearchDV.ColInvoiceHeaderId))

            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Function GetDescriptionFromDropdownCode(ByVal ddl As DropDownList, codeValue As String) As String
        For Each i As WebControls.ListItem In ddl.Items
            If i.Value = codeValue then
                Return i.Text
            End If
        Next
        Return codeValue
    End Function
    Private Sub cboPageSize_SelectedIndexChanged(ByVal source As Object, ByVal e As EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            State.selectedPageSize = CType(cboPageSize.SelectedValue, Integer)
            State.PageIndex = NewCurrentPageIndex(Grid, State.SearchDv.Count, State.selectedPageSize)
            Grid.PageIndex = NewCurrentPageIndex(Grid, Session("recCount"), CType(cboPageSize.SelectedValue, Int32))
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Public Sub Grid_RowCommand(ByVal source As Object, ByVal e As GridViewCommandEventArgs) Handles Grid.RowCommand
        Dim rowIndex As Integer
        Dim invoiceHeaderId As String

        Try
            If (Not e.CommandArgument.ToString().Equals(String.Empty)) And (e.CommandName = SELECT_COMMAND_NAME) Then
                rowIndex = CInt(e.CommandArgument)

                invoiceHeaderId = Grid.Rows(rowIndex).Cells(GridColIdxInvoiceHeaderId).Text
                State.SelectedInvoiceId = New Guid(invoiceHeaderId)
                'call AR invoice detail pages
                callPage(ArInvoiceDetailsForm.URL, State.SelectedInvoiceId)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

    Public Sub RowCreated(ByVal sender As System.Object, ByVal e As GridViewRowEventArgs) Handles Grid.RowCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_PageIndexChanged(ByVal source As Object, ByVal e As EventArgs) Handles Grid.PageIndexChanged
        Try
            State.PageIndex = Grid.PageIndex
            State.SelectedInvoiceId = Guid.Empty
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    

    Private Sub Grid_Sorting(sender As Object, e As GridViewSortEventArgs) Handles Grid.Sorting
        Try
            If State.GridSortExpression.StartsWith(e.SortExpression) Then
                If State.GridSortExpression.EndsWith(" DESC") Then
                    State.GridSortExpression = e.SortExpression
                Else
                    State.GridSortExpression &= " DESC"
                End If
            Else
                State.GridSortExpression = e.SortExpression
            End If
            State.PageIndex = 0
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Invoice review logic"
    Private Function GetSelectedInvoices() As List(Of Guid)
        Dim chkBox As CheckBox, invoiceHeaderIdStr As String
        Dim selectedInvoices As List(Of Guid) = New List(Of Guid)

        For Each row As GridViewRow In Grid.Rows
            If row.RowType = DataControlRowType.DataRow Then
                chkBox = CType(row.Cells(GridColIdxCheckbox).FindControl(GridColCtrlSelect), CheckBox)
                If chkBox IsNot nothing AndAlso chkBox.Checked Then
                    invoiceHeaderIdStr = row.Cells(GridColIdxInvoiceHeaderId).Text.Trim
                    If String.IsNullOrEmpty(invoiceHeaderIdStr) = False Then
                        selectedInvoices.Add(New Guid(invoiceHeaderIdStr))
                    End If
                End If
            End If
        Next
        Return selectedInvoices
    End Function

    Private Function ValidInvoiceSelectionForReview(ByVal invoiceList As List(Of Guid)) As Boolean
        Dim isSelectionValid As Boolean = True
        If invoiceList.Count > 0 Then
            Dim dv As DataView = State.SearchDv
            dv.Sort = ArInvoiceHeader.ArInvoiceSearchDV.ColInvoiceHeaderId

            Dim strStatusXcd As String, index As Integer
            For Each guidtemp As Guid In invoiceList
                index = dv.Find(guidtemp.ToByteArray)
                If index <> -1 Then 'found the record
                    strStatusXcd = dv(index)(ArInvoiceHeader.ArInvoiceSearchDv.ColStatusXcd)

                    If String.IsNullOrEmpty(strStatusXcd) OrElse strStatusXcd <>  InvoiceStatusXcdPending Then 'invoice can be only reviewed when in pending status
                        MasterPage.MessageController.AddError(String.Format("{0}", TranslationBase.TranslateLabelOrMessage("INVOICE_NOT_IN_PENDING_STATUS")), False)
                        isSelectionValid = False
                    End If
                End If
            Next
        Else
            'must select at least one invoice to review
            MasterPage.MessageController.AddError("MSG_NO_RECORD_SELECTED", True)
            isSelectionValid = False
        End If

        If isSelectionValid = False Then
            MasterPage.MessageController.Show()
        End If

        Return isSelectionValid
    End Function

    Private sub ClearReviewErrorLabels() 
        ClearLabelErrSign(lblReviewDecision)
        ClearLabelErrSign(lblReviewComments)
    End sub

    Private Sub InitInvoiceReviewControls()
        ClearReviewErrorLabels()

        'erase the previous process result message 
        lblReviewDecisionResult.Visible = False
        lblReviewDecisionResult.Text = String.Empty
        ddlReviewDecision.SelectedIndex = -1
        txtReviewComments.Text = String.Empty
    End Sub
    Private Function ValidReviewInputs() As Boolean
        dim blnValid As Boolean = True

        if string.IsNullOrEmpty(ddlReviewDecision.SelectedValue) Then
            blnValid = False
            lblReviewDecision.ForeColor = Color.Red
        End If

        If String.IsNullOrEmpty(txtReviewComments.Text.Trim()) Then
            blnValid = False
            lblReviewComments.ForeColor = Color.Red
        End If

        Return blnValid
    End Function
    
    Private Sub btnReviewDecisionSave_Click(sender As Object, e As EventArgs) Handles btnReviewDecisionSave.Click
        Try
            ClearReviewErrorLabels()
            lblReviewDecisionResult.Visible = True

            if ValidReviewInputs() Then
                Dim errCode As Integer, errMsg As String
                ArInvoiceHeader.UpdateReviewDecisions(State.InvoiceToBeReviewed,
                                                      ddlReviewDecision.SelectedValue,
                                                      String.Format("{0}: {1}", TranslationBase.TranslateLabelOrMessage("REVIEW_COMMENTS"),txtReviewComments.Text.Trim()),
                                                      errCode,
                                                      errMsg)

                if errCode <> 0 Then
                    lblReviewDecisionResult.Text = errMsg
                    lblReviewDecisionResult.CssClass = MessageController.ERROR_CSS
                Else 
                    HiddenFieldReviewDecision.Value = "N" ' 
                    MasterPage.MessageController.AddSuccess("MSG_RECORD_SAVED_OK")
                    RefreshSearchGrid()
                End If
            Else 
                lblReviewDecisionResult.Text = TranslationBase.TranslateLabelOrMessage("Required fields are missing.")
                lblReviewDecisionResult.CssClass = MessageController.ERROR_CSS
            End If
            
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnReviewDecision_WRITE_Click(sender As Object, e As EventArgs) Handles btnReviewDecision_WRITE.Click
        Try
            InitInvoiceReviewControls()

            State.InvoiceToBeReviewed = GetSelectedInvoices()

            If ValidInvoiceSelectionForReview(State.InvoiceToBeReviewed) Then
                'show the review dialog box
                HiddenFieldReviewDecision.Value = "Y"
            Else 
                HiddenFieldReviewDecision.Value = "N"
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub


#End Region
End Class