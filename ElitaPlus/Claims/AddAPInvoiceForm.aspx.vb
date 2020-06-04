Imports System.Diagnostics
Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.Web.Forms
Imports Assurant.ElitaPlus.BusinessObjectsNew.Tables.Accounting.AccountPayable
Imports Assurant.ElitaPlus.Security

Namespace Claims
    Public Class AddApInvoiceForm
        Inherits ElitaPlusSearchPage

#Region "Constants"

        Private Const AP_INVOICE_LINE_ID_COL As Integer = 0
        Private Const LINE_NO_COL As Integer = 1
        Private Const LINE_TYPE_COL As Integer = 2
        Private Const ITEM_CODE_COL As Integer = 3
        Private Const DESCRIPTION_COL As Integer = 4
        Private Const QUANTITY_COL As Integer = 5
        Private Const UNIT_PRICE_COL As Integer = 6
        Private Const TOTAL_PRICE_COL As Integer = 7
        Private Const UNIT_OF_MEASUREMENT_COL As Integer = 8
        Private Const PO_NUMBER_COL As Integer = 9
        Private Const MSG_RECORD_SAVED_OK As String = "MSG_RECORD_SAVED_OK"
        Private Const MSG_RECORD_NOT_SAVED As String = "MSG_RECORD_NOT_SAVED"

        Private Const AP_INVOICE_LINE_ID_CONTROL_LABEL As String = "moInvoiceLineId"
        Private Const LINE_NO_CONTROL_LABEL As String = "moLineNumber"
        Private Const LINE_TYPE_CONTROL_LABEL As String = "moLineType"
        Private Const ITEM_CODE_CONTROL_LABEL As String = "moItemCode"
        Private Const DESCRIPTION_CONTROL_LABEL As String = "moItemDescriptionLabel"
        Private Const QUANTITY_CONTROL_LABEL As String = "moQuantityLabel"
        Private Const UNIT_PRICE_CONTROL_LABEL As String = "moUnitPriceLabel"
        Private Const TOTAL_PRICE_CONTROL_LABEL As String = "moTotalPriceLabel"
        Private Const UNIT_OF_MEASUREMENT_CONTROL_LABEL As String = "moUnitOfMeasurement"
        Private Const PO_NUMBER_CONTROL_LABEL As String = "moPoNumber"

        Private Const LINE_NO_CONTROL_NAME As String = "moLineNumberText"
        Private Const LINE_TYPE_CONTROL_NAME As String = "ddlLineType"
        Private Const ITEM_CODE_CONTROL_NAME As String = "moVendorItemCodeText"
        Private Const ITEM_DESCRIPTION_CONTROL_NAME As String = "moVendorItemDescriptionText"
        Private Const QUANTITY_CONTROL_NAME As String = "moQuanitityText"
        Private Const UNIT_PRICE_CONTROL_NAME As String = "moUnitPriceText"
        Private Const TOTAL_CONTROL_NAME As String = "moTotalPriceText"
        Private Const UNIT_OF_MEASUREMENT_CONTROL_NAME As String = "ddlUnitOfMeasurement"
        Private Const PO_NUMBER_CONTROL_NAME As String = "moPoNumberText"



        Private Const EDIT_COMMAND As String = "EditRecord"
        Public Const PAGETITLE As String = "AP_INVOICE"
        Public Const PAGETAB As String = "CLAIM"
        Private Const NO_ROW_SELECTED_INDEX As Integer = -1
        Private Const DBINVOICELINE_ID As Integer = 0

        'Actions
        Private Const ACTION_NONE As String = "ACTION_NONE"
        Private Const ACTION_SAVE As String = "ACTION_SAVE"
        Private Const ACTION_CANCEL_DELETE As String = "ACTION_CANCEL_DELETE"
        Private Const ACTION_EDIT As String = "ACTION_EDIT"
        Private Const ACTION_NEW As String = "ACTION_NEW"

        'Invoice Header Default value
        Private Const SOURCE As String = "MANNUAL"
        Private Const PAID_AMOUNT As Decimal = 0
        Private Const PAYMENT_STATUS_XCD As String = "APINVPYMTSTATUS-NOPYMT"
        Private Const CURRENCY_ISO_CODE As String = "USD"
        Private Const EXCHANGE_RATE As Decimal = 1
        Private Const APPROVED_XCD As String = "N"
        Private Const ACCOUNTING_PERIOD As String = "N"
        Private Const DISTRIBUTED As String = "N"
        Private Const POSTED As String = "N"



#End Region

        Public Sub New()
            MyBase.New(New MyState)
        End Sub

#Region "Form Events"

        Private Sub SetStateProperties()
            State.APInvoiceNumber = moInvoiceNumber.Text
            State.APInvoiceAmount = moInvoiceAmount.Text
            State.APInvoiceDate = If(String.IsNullOrEmpty(moInvoiceDate.Text), Date.MinValue, Convert.ToDateTime(moInvoiceDate.Text))
            State.Term = moAPInvoiceTerm.SelectedValue
            State.ServiceCenter = moVendorDropDown.SelectedValue
            State.CompanyId = ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID

        End Sub

        Private Sub AddApInvoiceForm_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall
            Try
                If CallingPar = Nothing Then
                    State.IsNewInvoice = True
                    State.APInvoiceHeaderBO = New ApInvoiceHeader
                Else
                    State.IsNewInvoice = False
                    Dim tempGuid As Guid = CallingPar
                    State.APInvoiceHeaderBO = New ApInvoiceHeader(tempGuid)
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
            Try
                MasterPage.MessageController.Clear_Hide()

                If Not Page.IsPostBack Then
                    Me.AddCalendar_New(btnInvoiceDate, moInvoiceDate)
                    SetFormTitle(PAGETITLE)
                    SetFormTab(PAGETAB)
                    SetDefaultButton(moInvoiceNumber, btnApply_WRITE)

                    If State.IsGridVisible Then
                        If Not (State.SelectedPageSize = DEFAULT_PAGE_SIZE) Then
                            cboPageSize.SelectedValue = CType(State.SelectedPageSize, String)
                            PoGrid.PageSize = State.SelectedPageSize
                        End If
                        PopulatePoGrid()
                    End If

                    SetGridItemStyleColor(PoGrid)
                    PopulateDropDown()
                    SetStateProperties()
                    SetButtonsState()
                    State.PageIndex = 0
                    TranslateGridControls(PoGrid)
                    TranslateGridHeader(PoGrid)
                End If
                BindBoPropertiesToGridHeaders()

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
            ShowMissingTranslations(MasterPage.MessageController)
        End Sub

#End Region

#Region "Properties"
        Public ReadOnly Property IsEditing() As Boolean
            Get
                IsEditing = (PoGrid.EditIndex > NO_ROW_SELECTED_INDEX)
            End Get
        End Property

#End Region

#Region "Page State"
        <DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Private Sub Page_Init(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Init
            InitializeComponent()
        End Sub
        Class MyState
            Public PageIndex As Integer = 0
            Public APInvoiceHeaderBO As ApInvoiceHeader
            Public APInvoiceLinesBO As ApInvoiceLines
            Public APInvoiceHeaderId As Guid
            Public APInvoiceLineId As Guid
            Public APInvoiceNumber As String
            Public APInvoiceAmount As String
            Public LangId As Guid
            Public APInvoiceDate As Date
            Public Term As String
            Public ServiceCenter As String
            Public CompanyId As Guid
            Public IsNewInvoice As Boolean = False
            Public IsNewInvoiceLine As Boolean = False
            Public IsAfterSave As Boolean
            Public IsEditMode As Boolean
            Public IsGridVisible As Boolean
            Public SearchDv As DataView = Nothing
            Public Canceling As Boolean
            Public SelectedPageSize As Integer = DEFAULT_PAGE_SIZE
            Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
            Public BnoRow As Boolean = False
            Sub New()

            End Sub
        End Class

        Protected Shadows ReadOnly Property State() As MyState
            Get
                Return CType(MyBase.State, MyState)
            End Get
        End Property

#End Region

#Region "Control"
        Private Sub SetButtonsState()

            ControlMgr.SetVisibleControl(Me, btnApply_WRITE, True)
            ControlMgr.SetVisibleControl(Me, btnBack, True)

        End Sub

        Private Sub SetGridButtonState(ByVal visible As Boolean)
            ControlMgr.SetVisibleControl(Me, BtnNewLine, visible)
            ControlMgr.SetVisibleControl(Me, BtnCancelLine, Not visible)
            ControlMgr.SetVisibleControl(Me, BtnSaveLines, Not visible)
        End Sub

        Protected Sub BindBoPropertiesToGridHeaders()

            If State.APInvoiceLinesBO Is Nothing Then
                State.APInvoiceLinesBO = New ApInvoiceLines
            End If

            BindBOPropertyToGridHeader(State.APInvoiceLinesBO, "LineNumber", PoGrid.Columns(LINE_NO_COL))
            BindBOPropertyToGridHeader(State.APInvoiceLinesBO, "LineType", PoGrid.Columns(LINE_TYPE_COL))
            BindBOPropertyToGridHeader(State.APInvoiceLinesBO, "VendorItemCode", PoGrid.Columns(ITEM_CODE_COL))
            BindBOPropertyToGridHeader(State.APInvoiceLinesBO, "Description", PoGrid.Columns(DESCRIPTION_COL))
            BindBOPropertyToGridHeader(State.APInvoiceLinesBO, "Quantity", PoGrid.Columns(QUANTITY_COL))
            BindBOPropertyToGridHeader(State.APInvoiceLinesBO, "UnitPrice", PoGrid.Columns(UNIT_PRICE_COL))
            BindBOPropertyToGridHeader(State.APInvoiceLinesBO, "TotalPrice", PoGrid.Columns(TOTAL_PRICE_COL))
            BindBOPropertyToGridHeader(State.APInvoiceLinesBO, "UomXcd", PoGrid.Columns(UNIT_OF_MEASUREMENT_COL))
            BindBOPropertyToGridHeader(State.APInvoiceLinesBO, "PoNumber", PoGrid.Columns(PO_NUMBER_COL))


            ClearGridViewHeadersAndLabelsErrorSign()
        End Sub

        Private Sub SetFocusOnEditableFieldInGrid(ByVal grid As GridView, ByVal cellPosition As Integer, ByVal controlName As String, ByVal itemIndex As Integer)
            'Set focus on the Description TextBox for the EditItemIndex row
            Dim desc As TextBox = CType(grid.Rows(itemIndex).Cells(cellPosition).FindControl(controlName), TextBox)
            SetFocus(desc)
        End Sub

#End Region

#Region "Populate"
        Private Sub PopulateDropDown()

            Try

                Dim ServiceCenterList As New Collections.Generic.List(Of DataElements.ListItem)
                For Each Country_id As Guid In ElitaPlusIdentity.Current.ActiveUser.Countries
                    Dim ServiceCenters As DataElements.ListItem() =
                    CommonConfigManager.Current.ListManager.GetList(listCode:="ServiceCenterListByCountry",
                                                                    context:=New ListContext() With
                                                                    {
                                                                      .CountryId = Country_id
                                                                    })

                    If ServiceCenters.Count > 0 Then
                        If Not ServiceCenterList Is Nothing Then
                            ServiceCenterList.AddRange(ServiceCenters)
                        Else
                            ServiceCenterList = ServiceCenters.Clone()
                        End If
                    End If
                Next

                Me.moVendorDropDown.Populate(ServiceCenterList.ToArray(),
                                    New PopulateOptions() With
                                    {
                                        .AddBlankItem = True
                                    })

                Dim TermList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList("PMTTRM", Thread.CurrentPrincipal.GetLanguageCode())
                moAPInvoiceTerm.Populate(CommonConfigManager.Current.ListManager.GetList("PMTTRM", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                               {
                                   .AddBlankItem = True,
                                   .BlankItemValue = String.Empty,
                                   .TextFunc = AddressOf PopulateOptions.GetDescription,
                                   .ValueFunc = AddressOf PopulateOptions.GetExtendedCode
                               })

                Dim DealerList As New Collections.Generic.List(Of DataElements.ListItem)
                For Each CompanyId As Guid In ElitaPlusIdentity.Current.ActiveUser.Companies
                    Dim Dealers As DataElements.ListItem() =
                    CommonConfigManager.Current.ListManager.GetList(listCode:="DealerListByCompany",
                                                        context:=New ListContext() With
                                                        {
                                                          .CompanyId = CompanyId
                                                        })

                    If Dealers.Count > 0 Then
                        If Not DealerList Is Nothing Then
                            DealerList.AddRange(Dealers)
                        Else
                            DealerList = Dealers.Clone()
                        End If
                    End If
                Next

                moDealer.Populate(DealerList.ToArray(),
                        New PopulateOptions() With
                        {
                            .AddBlankItem = True
                        })

                'Dim InvoiceType As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="INVTYP",
                'languageCode:=Thread.CurrentPrincipal.GetLanguageCode())
                'Me.moAPInvoiceTerm.Populate(InvoiceType.ToArray(),
                '                        New PopulateOptions() With
                '                        {
                '                            .AddBlankItem = True
                '                        })

                'Dim upgtermUnitOfMeasureLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("UNIT_OF_MEASURE", Thread.CurrentPrincipal.GetLanguageCode())
                'moUpgTermUOMDrop.Populate(upgtermUnitOfMeasureLkl, New PopulateOptions() With
                '                                      {
                '                                        .AddBlankItem = True
                '                                       })

                'moAPInvoiceTerm.Populate(CommonConfigManager.Current.ListManager.GetList("INVOICE_REC_TYP", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                '   {
                '       .AddBlankItem = True
                '   })
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

        Private Sub PopulatePoGrid(Optional ByVal oAction As String = ACTION_NONE)

            Try

                If (State.SearchDv Is Nothing) Then
                    GetDV()
                End If
                State.SearchDv.Sort = SortDirection

                Select Case oAction
                    Case ACTION_NONE
                        SetPageAndSelectedIndexFromGuid(State.SearchDv, Guid.Empty, PoGrid, 0)
                        SetGridButtonState(True)
                    Case ACTION_SAVE
                        SetPageAndSelectedIndexFromGuid(State.SearchDv, State.APInvoiceLineId, PoGrid, PoGrid.PageIndex)
                        SetGridButtonState(True)
                    Case ACTION_CANCEL_DELETE
                        Me.SetPageAndSelectedIndexFromGuid(State.SearchDv, Guid.Empty, PoGrid, PoGrid.PageIndex)
                        SetGridButtonState(True)
                    Case ACTION_EDIT
                        SetPageAndSelectedIndexFromGuid(State.SearchDv, State.APInvoiceLineId, PoGrid, State.PageIndex, True)
                        SetGridButtonState(False)
                    Case ACTION_NEW
                        If Me.State.IsNewInvoiceLine Then State.SearchDv.Table.DefaultView.Sort() = Nothing
                        Dim oRow As DataRow = State.SearchDv.Table.NewRow
                        oRow(DBINVOICELINE_ID) = State.APInvoiceHeaderId.ToByteArray
                        State.SearchDv.Table.Rows.Add(oRow)
                        Me.SetPageAndSelectedIndexFromGuid(State.SearchDv, State.APInvoiceLineId, PoGrid, PoGrid.PageIndex, True)
                        SetGridButtonState(True)

                End Select

                PoGrid.AutoGenerateColumns = False
                PoGrid.Columns(ITEM_CODE_COL).SortExpression = ApInvoiceLines.ITEM_CODE_COL
                PoGrid.Columns(PO_NUMBER_COL).SortExpression = ApInvoiceLines.PO_NUMBER_COL
                PoGrid.Columns(LINE_NO_COL).SortExpression = ApInvoiceLines.LINE_NUMBER_COL
                PoGrid.Columns(ITEM_CODE_COL).SortExpression = ApInvoiceLines.ITEM_CODE_COL
                PoGrid.Columns(DESCRIPTION_COL).SortExpression = ApInvoiceLines.DESCRIPTION_COL
                PoGrid.Columns(QUANTITY_COL).SortExpression = ApInvoiceLines.QUANTITY_COL
                PoGrid.Columns(UNIT_PRICE_COL).SortExpression = ApInvoiceLines.UNIT_PRICE_COL
                PoGrid.Columns(TOTAL_PRICE_COL).SortExpression = ApInvoiceLines.TOTAL_PRICE_COL

                SortAndBindGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Private Function GetDV() As DataView

            State.SearchDv = GetPoGridDataView()
            State.SearchDv.Sort = PoGrid.DataMember()
            PoGrid.DataSource = State.SearchDv

            Return (State.SearchDv)

        End Function

        Private Function GetPoGridDataView() As DataView

            With State
                Return .APInvoiceLinesBO.GetApInvoiceLines(State.APInvoiceHeaderId)
            End With

        End Function

        Private Sub PopulateLinesBoFromForm()
            Try
                With State.APInvoiceLinesBO
                    .LineNumber = CType(PoGrid.Rows(PoGrid.EditIndex).Cells(LINE_NO_COL).FindControl(LINE_NO_CONTROL_LABEL), Label).Text
                    .LineType = CType(PoGrid.Rows(PoGrid.EditIndex).Cells(LINE_TYPE_COL).FindControl(LINE_TYPE_CONTROL_LABEL), DropDownList).SelectedValue
                    .PoNumber = CType(PoGrid.Rows(PoGrid.EditIndex).Cells(PO_NUMBER_COL).FindControl(PO_NUMBER_CONTROL_LABEL), Label).Text
                    .VendorItemCode = CType(PoGrid.Rows(PoGrid.EditIndex).Cells(ITEM_CODE_COL).FindControl(ITEM_CODE_CONTROL_LABEL), Label).Text
                    .Description = CType(PoGrid.Rows(PoGrid.EditIndex).Cells(DESCRIPTION_COL).FindControl(DESCRIPTION_CONTROL_LABEL), Label).Text
                    .Quantity = CType(PoGrid.Rows(PoGrid.EditIndex).Cells(QUANTITY_COL).FindControl(QUANTITY_CONTROL_NAME), TextBox).Text
                    .UnitPrice = CType(PoGrid.Rows(PoGrid.EditIndex).Cells(UNIT_PRICE_COL).FindControl(UNIT_PRICE_CONTROL_LABEL), Label).Text
                    .TotalPrice = CType(PoGrid.Rows(PoGrid.EditIndex).Cells(TOTAL_PRICE_COL).FindControl(TOTAL_PRICE_CONTROL_LABEL), Label).Text
                    .UomXcd = CType(PoGrid.Rows(PoGrid.EditIndex).Cells(UNIT_OF_MEASUREMENT_COL).FindControl(UNIT_OF_MEASUREMENT_CONTROL_LABEL), DropDownList).SelectedValue

                End With
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Private Sub PopulateBoFromForm()
            Try

                PopulateBOProperty(State.APInvoiceHeaderBO, "InvoiceNumber", moInvoiceNumber)
                PopulateBOProperty(State.APInvoiceHeaderBO, "InvoiceAmount", moInvoiceAmount)
                PopulateBOProperty(State.APInvoiceHeaderBO, "InvoiceDate", moInvoiceDate)
                PopulateBOProperty(State.APInvoiceHeaderBO, "VendorId", moVendorDropDown)
                PopulateBOProperty(State.APInvoiceHeaderBO, "VendorAddressId", moVendorDropDown)
                PopulateBOProperty(State.APInvoiceHeaderBO, "DealerId", moDealer)
                PopulateBOProperty(State.APInvoiceHeaderBO, "TermXcd", moAPInvoiceTerm, False, True)
                State.APInvoiceHeaderBO.Source = SOURCE
                State.APInvoiceHeaderBO.PaidAmount = PAID_AMOUNT
                State.APInvoiceHeaderBO.PaymentStatusXcd = PAYMENT_STATUS_XCD
                State.APInvoiceHeaderBO.CurrencyIsoCode = CURRENCY_ISO_CODE
                State.APInvoiceHeaderBO.ExchangeRate = EXCHANGE_RATE
                State.APInvoiceHeaderBO.ApprovedXcd = APPROVED_XCD
                State.APInvoiceHeaderBO.AccountingPeriod = ACCOUNTING_PERIOD
                State.APInvoiceHeaderBO.Distributed = DISTRIBUTED
                State.APInvoiceHeaderBO.Posted = POSTED
                State.APInvoiceHeaderBO.CompanyId = ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Private Sub PopulateFormFromBO()

            With State.APInvoiceHeaderBO

                moInvoiceNumber.Text = .InvoiceNumber
                moInvoiceAmount.Text = .InvoiceAmount
                moInvoiceDate.Text = .InvoiceDate
                moAPInvoiceTerm.SelectedValue = .TermXcd
                moVendorDropDown.SelectedValue = .VendorId.ToString
                moDealer.SelectedValue = .DealerId.ToString

            End With

        End Sub

        Private Sub PopulateLineFormFromBo()

            Dim gridRowIdx As Integer = PoGrid.EditIndex
            Try
                With State.APInvoiceLinesBO
                    If (Not .LineNumber Is Nothing) Then
                        CType(PoGrid.Rows(gridRowIdx).Cells(LINE_NO_COL).FindControl(LINE_NO_CONTROL_NAME), TextBox).Text = .LineNumber
                    End If

                    If (Not .LineType Is Nothing) Then

                        Dim moLineTypeDropDown As DropDownList = CType(PoGrid.Rows(gridRowIdx).Cells(LINE_TYPE_COL).FindControl(LINE_TYPE_CONTROL_NAME), DropDownList)
                        ElitaPlusPage.BindListControlToDataView(moLineTypeDropDown, LookupListNew.GetMonthsLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), "CODE",, False)
                        SetSelectedItemByText(moLineTypeDropDown, .LineType)
                    End If

                    If (Not .VendorItemCode Is Nothing) Then
                        CType(PoGrid.Rows(gridRowIdx).Cells(ITEM_CODE_COL).FindControl(ITEM_CODE_CONTROL_LABEL), TextBox).Text = .VendorItemCode
                    End If

                    If (Not .Description Is Nothing) Then
                        CType(PoGrid.Rows(gridRowIdx).Cells(DESCRIPTION_COL).FindControl(DESCRIPTION_CONTROL_LABEL), Label).Text = .Description
                    End If

                    If (Not .Quantity Is Nothing) Then
                        CType(PoGrid.Rows(gridRowIdx).Cells(QUANTITY_COL).FindControl(QUANTITY_CONTROL_NAME), TextBox).Text = .Quantity
                    End If

                    If (Not .UnitPrice Is Nothing) Then
                        CType(PoGrid.Rows(gridRowIdx).Cells(UNIT_PRICE_COL).FindControl(UNIT_PRICE_CONTROL_NAME), TextBox).Text = .UnitPrice
                    End If

                    If (Not .TotalPrice Is Nothing) Then
                        CType(PoGrid.Rows(gridRowIdx).Cells(TOTAL_PRICE_COL).FindControl(TOTAL_CONTROL_NAME), TextBox).Text = .UnitPrice
                    End If

                    If (Not .UomXcd Is Nothing) Then

                        Dim moUomDropDown As DropDownList = CType(PoGrid.Rows(gridRowIdx).Cells(UNIT_OF_MEASUREMENT_COL).FindControl(UNIT_OF_MEASUREMENT_CONTROL_NAME), DropDownList)
                        ElitaPlusPage.BindListControlToDataView(moUomDropDown, LookupListNew.GetMonthsLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), "CODE",, False)
                        SetSelectedItemByText(moUomDropDown, .UomXcd)
                    End If

                    If (Not .PoNumber Is Nothing) Then
                        CType(PoGrid.Rows(gridRowIdx).Cells(PO_NUMBER_COL).FindControl(PO_NUMBER_CONTROL_NAME), TextBox).Text = .PoNumber
                    End If

                    CType(PoGrid.Rows(gridRowIdx).Cells(AP_INVOICE_LINE_ID_COL).FindControl(AP_INVOICE_LINE_ID_CONTROL_LABEL), Label).Text = .Id.ToString
                End With
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub



#End Region

#Region "Grid Events"

        Private Sub Grid_PageSizeChanged(ByVal source As Object, ByVal e As EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                PoGrid.PageIndex = NewCurrentPageIndex(PoGrid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                State.SelectedPageSize = CType(cboPageSize.SelectedValue, Integer)
                PopulatePoGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Public Property SortDirection() As String
            Get
                Return If(ViewState("SortDirection") = Nothing, String.Empty, ViewState("SortDirection").ToString())
            End Get
            Set(ByVal value As String)
                ViewState("SortDirection") = value
            End Set
        End Property

        Protected Sub ItemCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
            Try
                BaseItemCreated(sender, e)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub ItemCommand(ByVal source As Object, ByVal e As GridViewCommandEventArgs)

            Try
                Dim index As Integer

                If (e.CommandName = EDIT_COMMAND) Then
                    index = CInt(e.CommandArgument)
                    State.IsEditMode = True

                    State.APInvoiceLineId = New Guid(CType(PoGrid.Rows(index).Cells(AP_INVOICE_LINE_ID_COL).FindControl(AP_INVOICE_LINE_ID_CONTROL_LABEL), Label).Text)
                    State.APInvoiceLinesBO = New ApInvoiceLines(State.APInvoiceLineId)

                    PopulatePoGrid()
                    State.PageIndex = PoGrid.PageIndex
                    SetGridControls(PoGrid, False)
                    SetFocusOnEditableFieldInGrid(PoGrid, QUANTITY_COL, QUANTITY_CONTROL_NAME, index)
                    PopulateLineFormFromBo()
                    SetGridControls(PoGrid, False)
                    SetButtonsState()

                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Private Sub SortAndBindGrid()
            TranslateGridControls(PoGrid)

            If (State.SearchDv.Count = 0) Then

                State.BnoRow = True
                CreateHeaderForEmptyGrid(PoGrid, SortDirection)

            Else
                State.BnoRow = False
                PoGrid.Enabled = True
                PoGrid.DataSource = State.SearchDv
                HighLightSortColumn(PoGrid, SortDirection)
                PoGrid.DataBind()
            End If
            If Not PoGrid.BottomPagerRow Is Nothing AndAlso Not PoGrid.BottomPagerRow.Visible Then PoGrid.BottomPagerRow.Visible = True
            ControlMgr.SetVisibleControl(Me, PoGrid, State.IsGridVisible)
            Session("recCount") = State.SearchDv.Count

            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, PoGrid)
        End Sub
        Private Sub POGrid_PageIndexChanged(ByVal source As Object, ByVal e As GridViewPageEventArgs) Handles PoGrid.PageIndexChanging

            Try
                If (Not (State.IsEditMode)) Then
                    State.PageIndex = e.NewPageIndex
                    PoGrid.PageIndex = State.PageIndex
                    PopulatePoGrid()
                    PoGrid.SelectedIndex = NO_ITEM_SELECTED_INDEX
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Private Sub Grid_ItemDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles PoGrid.RowDataBound
            Try

                Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

                If Not dvRow Is Nothing And Not State.BnoRow Then

                    If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then

                        CType(e.Row.Cells(AP_INVOICE_LINE_ID_COL).FindControl(AP_INVOICE_LINE_ID_CONTROL_LABEL), Label).Text = GetGuidStringFromByteArray(CType(dvRow(ApInvoiceLines.AP_LINE_ID), Byte()))

                        If (State.IsEditMode = True _
                                AndAlso State.APInvoiceLineId.ToString.Equals(GetGuidStringFromByteArray(CType(dvRow(ApInvoiceLines.AP_LINE_ID), Byte())))) Then

                            Dim moLineTypeDropDown As DropDownList = CType(e.Row.Cells(LINE_TYPE_COL).FindControl(LINE_TYPE_CONTROL_NAME), DropDownList)
                            ElitaPlusPage.BindListControlToDataView(moLineTypeDropDown, LookupListNew.GetMonthsLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), "CODE",, False)
                            SetSelectedItemByText(moLineTypeDropDown, dvRow(ApInvoiceLines.LINE_TYPE_COL).ToString)

                            CType(e.Row.Cells(LINE_TYPE_COL).FindControl(LINE_TYPE_CONTROL_NAME), TextBox).Text = dvRow(ApInvoiceLines.LINE_TYPE_COL).ToString
                            CType(e.Row.Cells(LINE_NO_COL).FindControl(LINE_NO_CONTROL_NAME), TextBox).Text = dvRow(ApInvoiceLines.LINE_NUMBER_COL).ToString
                            CType(e.Row.Cells(ITEM_CODE_COL).FindControl(ITEM_CODE_CONTROL_NAME), TextBox).Text = dvRow(ApInvoiceLines.ITEM_CODE_COL).ToString
                            CType(e.Row.Cells(DESCRIPTION_COL).FindControl(ITEM_DESCRIPTION_CONTROL_NAME), TextBox).Text = dvRow(ApInvoiceLines.DESCRIPTION_COL).ToString
                            CType(e.Row.Cells(QUANTITY_COL).FindControl(QUANTITY_CONTROL_NAME), TextBox).Text = dvRow(ApInvoiceLines.QUANTITY_COL).ToString
                            CType(e.Row.Cells(UNIT_PRICE_COL).FindControl(UNIT_PRICE_CONTROL_NAME), TextBox).Text = dvRow(ApInvoiceLines.UNIT_PRICE_COL).ToString
                            CType(e.Row.Cells(TOTAL_PRICE_COL).FindControl(TOTAL_CONTROL_NAME), TextBox).Text = dvRow(ApInvoiceLines.TOTAL_PRICE_COL).ToString
                            CType(e.Row.Cells(PO_NUMBER_COL).FindControl(PO_NUMBER_CONTROL_NAME), TextBox).Text = dvRow(ApInvoiceLines.PO_NUMBER_COL).ToString

                            Dim moUomDropDown As DropDownList = CType(e.Row.Cells(UNIT_OF_MEASUREMENT_COL).FindControl(UNIT_OF_MEASUREMENT_CONTROL_NAME), DropDownList)
                            ElitaPlusPage.BindListControlToDataView(moUomDropDown, LookupListNew.GetMonthsLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), "CODE",, False)
                            SetSelectedItemByText(moUomDropDown, dvRow(ApInvoiceLines.UNIT_OF_MEASUREMENT_COL).ToString)
                        Else
                            CType(e.Row.Cells(LINE_TYPE_COL).FindControl(LINE_TYPE_CONTROL_LABEL), Label).Text = dvRow(ApInvoiceLines.LINE_TYPE_COL).ToString
                            CType(e.Row.Cells(LINE_NO_COL).FindControl(LINE_NO_CONTROL_LABEL), Label).Text = dvRow(ApInvoiceLines.LINE_NUMBER_COL).ToString
                            CType(e.Row.Cells(ITEM_CODE_COL).FindControl(ITEM_CODE_CONTROL_LABEL), Label).Text = dvRow(ApInvoiceLines.ITEM_CODE_COL).ToString
                            CType(e.Row.Cells(DESCRIPTION_COL).FindControl(DESCRIPTION_CONTROL_LABEL), Label).Text = dvRow(ApInvoiceLines.DESCRIPTION_COL).ToString
                            CType(e.Row.Cells(QUANTITY_COL).FindControl(QUANTITY_CONTROL_LABEL), Label).Text = dvRow(ApInvoiceLines.QUANTITY_COL).ToString
                            CType(e.Row.Cells(UNIT_PRICE_COL).FindControl(UNIT_PRICE_CONTROL_LABEL), Label).Text = dvRow(ApInvoiceLines.UNIT_PRICE_COL).ToString
                            CType(e.Row.Cells(TOTAL_PRICE_COL).FindControl(TOTAL_PRICE_CONTROL_LABEL), Label).Text = dvRow(ApInvoiceLines.TOTAL_PRICE_COL).ToString
                            CType(e.Row.Cells(UNIT_OF_MEASUREMENT_COL).FindControl(UNIT_OF_MEASUREMENT_CONTROL_LABEL), Label).Text = dvRow(ApInvoiceLines.UNIT_OF_MEASUREMENT_COL).ToString
                            CType(e.Row.Cells(PO_NUMBER_COL).FindControl(PO_NUMBER_CONTROL_LABEL), Label).Text = dvRow(ApInvoiceLines.PO_NUMBER_COL).ToString
                        End If
                    End If
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_SortCommand(ByVal source As Object, ByVal e As GridViewSortEventArgs) Handles PoGrid.Sorting
            Try
                Dim spaceIndex As Integer = SortDirection.LastIndexOf(" ", StringComparison.Ordinal)


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
                PopulatePoGrid()
            Catch ex As Exception
                HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub ItemBound(ByVal source As Object, ByVal e As GridViewRowEventArgs) Handles PoGrid.RowDataBound

            Try
                If Not State.BnoRow Then
                    BaseItemBound(source, e)
                End If

            Catch ex As Exception
                HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Button Click Events"
        Protected Sub SaveButton_WRITE_Click(sender As Object, e As EventArgs) Handles btnApply_WRITE.Click
            Try
                PopulateBoFromForm()
                If (State.APInvoiceHeaderBO.IsDirty) Then
                    State.APInvoiceHeaderBO.SaveInvoiceHeader()
                    State.IsAfterSave = True
                    Me.MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
                    State.SearchDv = Nothing
                    ReturnFromEditing()
                Else
                    Me.MasterPage.MessageController.AddInformation(Message.MSG_RECORD_NOT_SAVED)
                    ReturnFromEditing()
                End If

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
        Private Sub ReturnFromEditing(Optional ByVal oAction As String = ACTION_NONE)

            PoGrid.EditIndex = NO_ROW_SELECTED_INDEX

            State.IsEditMode = False
            State.IsGridVisible = True
            If State.IsNewInvoice Then
                State.IsNewInvoiceLine = True
                PopulatePoGrid(ACTION_NEW)
            Else
                PopulatePoGrid(ACTION_NONE)
            End If

            State.PageIndex = PoGrid.PageIndex
            SetButtonsState()

        End Sub

#End Region

    End Class
End Namespace