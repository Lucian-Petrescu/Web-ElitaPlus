Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports Assurant.Elita.Web.Forms
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Assurant.ElitaPlus.Security

Namespace Claims.AccountPayable
    Public Class AddApInvoiceForm
        Inherits ElitaPlusSearchPage

#Region "Constants"

        Public Const Url As String = "~/Claims/AccountPayable/AddAPInvoiceForm.aspx"
        Public Const InvoiceSource As String = "MANNUAL-ELITA"
        Private Const ApInvoiceLineIdCol As Integer = 0
        Private Const LineNoCol As Integer = 1
        Private Const LineTypeCol As Integer = 2
        Private Const ItemCodeCol As Integer = 3
        Private Const DescriptionCol As Integer = 4
        Private Const QuantityCol As Integer = 5
        Private Const UnitPriceCol As Integer = 6
        Private Const TotalPriceCol As Integer = 7
        Private Const UnitOfMeasurementCol As Integer = 8
        Private Const PoNumberCol As Integer = 9

        Private Const ApInvoiceLineIdControlLabel As String = "moInvoiceLineId"
        Private Const LineNoControlLabel As String = "moLineNumber"
        Private Const LineTypeControlLabel As String = "moLineType"
        Private Const ItemCodeControlLabel As String = "moItemCode"
        Private Const DescriptionControlLabel As String = "moItemDescriptionLabel"
        Private Const QuantityControlLabel As String = "moQuantityLabel"
        Private Const UnitPriceControlLabel As String = "moUnitPriceLabel"
        Private Const TotalPriceControlLabel As String = "moTotalPriceLabel"
        Private Const UnitOfMeasurementControlLabel As String = "moUnitOfMeasurement"
        Private Const PoNumberControlLabel As String = "moPoNumber"

        Private Const LineNoControlName As String = "moLineNumberText"
        Private Const LineTypeControlName As String = "ddlLineType"
        Private Const ItemCodeControlName As String = "moVendorItemCodeText"
        Private Const ItemDescriptionControlName As String = "moVendorItemDescriptionText"
        Private Const QuantityControlName As String = "moQuantityText"
        Private Const UnitPriceControlName As String = "moUnitPriceText"
        Private Const TotalControlName As String = "moTotalPriceText"
        Private Const UnitOfMeasurementControlName As String = "ddlUnitOfMeasurement"
        Private Const PoNumberControlName As String = "moPoNumberText"



        Private Const EditCommand As String = "EditRecord"
        Private Const DeleteCommand As String = "DeleteRecord"
        Public Const PageTitle As String = "AP_INVOICE"
        Public Const PageTab As String = "CLAIM"
        Private Const NoRowSelectedIndex As Integer = -1
        Private Const ZeroRowIndex As Integer = 0

        'Actions
        Private Const ActionNone As String = "ACTION_NONE"
        Private Const ActionSave As String = "ACTION_SAVE"
        Private Const ActionCancelDelete As String = "ACTION_CANCEL_DELETE"
        Private Const ActionEdit As String = "ACTION_EDIT"
        Private Const ActionNew As String = "ACTION_NEW"

        'Invoice Header Default value
        Private Const Source As String = "MANNUAL"
        Private Const PaidAmount As Decimal = 0
        Public Const InvoiceStatusIncomplete As String = "APINVPYMTSTATUS-INCOMPLETE"
        private Const InvoiceStatusComplete = "APINVPYMTSTATUS-NOPYMT"
        Private Const CurrencyIsoCode As String = "USD"
        Private Const ExchangeRate As Decimal = 1
        Private Const ApprovedXcd As String = "N"
        Private Const AccountingPeriod As String = "N"
        Private Const Distributed As String = "N"
        Private Const Posted As String = "N"



#End Region

        Public Sub New()
            MyBase.New(New MyState)
        End Sub


#Region "Form Events"

        Private Sub SetStateProperties()
            State.ApInvoiceNumber = moInvoiceNumber.Text
            State.ApInvoiceAmount = moInvoiceAmount.Text
            State.ApInvoiceDate = If(String.IsNullOrEmpty(moInvoiceDate.Text), Date.MinValue, Convert.ToDateTime(moInvoiceDate.Text))
            State.Term = moAPInvoiceTerm.SelectedValue
            State.ServiceCenter = moVendorDropDown.SelectedValue
            State.CompanyId = ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID

        End Sub

        Private Sub AddApInvoiceForm_PageCall(callFromUrl As String, callingPar As Object) Handles MyBase.PageCall
            Try
                If callingPar = Nothing Then
                    State.IsNewInvoice = True
                    ControlMgr.SetVisibleControl(Me, btnFinalize, False)
                    State.ApInvoiceHeaderBo = New ApInvoiceHeader
                    State.ApInvoiceLinesBo = New ApInvoiceLines
                Else
                    State.IsNewInvoice = False
                    Dim tempGuid As Guid = callingPar
                    State.ApInvoiceHeaderBo = New ApInvoiceHeader(tempGuid)
                    State.ApInvoiceLinesBo = New ApInvoiceLines
                    State.IsGridVisible=true
                    If Not State.ApInvoiceHeaderBo.PaymentStatusXcd = InvoiceStatusIncomplete Then
                        SetFinalizedInvoiceControls()
                    End If
                End If
                    SetInvoiceHeaderControl()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
            Try
                MasterPage.MessageController.Clear_Hide()
                RewireUserControlHandler(ucApInvoiceLinesSearch)
                If Not Page.IsPostBack Then
                    AddCalendar_New(btnInvoiceDate, moInvoiceDate)
                    SetFormTitle(PageTitle)
                    SetFormTab(PageTab)
                    SetDefaultButton(moInvoiceNumber, btnApply_WRITE)

                    PopulateDropDown()
                    PopulateFormFromBo()
                    SetButtonsState()
                    BindBoPropertiesToGridHeaders()
                    TranslateGridHeader(InvoiceLinesGrid)
                    SetGridPageSize()
                End If
                SetStateProperties()
                BindBoPropertiesToLabels
                AddLabelDecorations(State.ApInvoiceHeaderBo)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
            ShowMissingTranslations(MasterPage.MessageController)
        End Sub
        Private Sub RewireUserControlHandler(userControl As UserControlApInvoiceLinesSearch)
            userControl.TranslationFunc = Function(value As String)
                Return TranslationBase.TranslateLabelOrMessage(value)
            End Function

            userControl.TranslateGridHeaderFunc = Sub(grid As GridView)
                TranslateGridHeader(grid)
            End Sub
           userControl.NewCurrentPageIndexFunc = Function(grid As GridView, intRecordCount As Integer, intNewPageSize As Integer)
                Return NewCurrentPageIndex(grid, intRecordCount, intNewPageSize)
            End Function
            userControl.HostMessageController = MasterPage.MessageController

            userControl.SortAndHighlightGridHeaderFunc = sub (grid as GridView,gridSortDirection as string)
                HighLightSortColumn(grid, gridSortDirection)
            End sub
            
        End Sub
        Protected Sub BindBoPropertiesToLabels()

            BindBOPropertyToLabel(State.ApInvoiceHeaderBo, "InvoiceNumber", moInvoiceNumberLabel)
            BindBOPropertyToLabel(State.ApInvoiceHeaderBo, "InvoiceAmount", moInvoiceAmountLabel)
            BindBOPropertyToLabel(State.ApInvoiceHeaderBo, "DealerId", moDealerLabel)
            BindBOPropertyToLabel(State.ApInvoiceHeaderBo, "InvoiceDate", moInvoiceDateLabel)
            BindBOPropertyToLabel(State.ApInvoiceHeaderBo, "VendorId", moServiceCenterLabel)
            BindBOPropertyToLabel(State.ApInvoiceHeaderBo, "TermXcd", moTermLabel)
            ClearGridViewHeadersAndLabelsErrorSign()

        End Sub

#End Region

#Region "Properties"
        Public ReadOnly Property IsEditing() As Boolean
            Get
                IsEditing = (InvoiceLinesGrid.EditIndex > NoRowSelectedIndex)
            End Get
        End Property

#End Region

#Region "Page State"
        <DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        Private Sub Page_Init(sender As Object, e As EventArgs) Handles MyBase.Init
            InitializeComponent()
        End Sub

        Class MyState
            Public PageIndex As Integer = 0
            Public ApInvoiceHeaderBo As ApInvoiceHeader
            Public ApInvoiceLinesBo As ApInvoiceLines
            Public ApInvoiceLineId As Guid
            Public ApInvoiceNumber As String
            Public ApInvoiceAmount As String
            Public LangId As Guid
            Public ApInvoiceDate As Date
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
            Public TotalLines As integer
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
            If State.IsNewInvoice Then
                ControlMgr.SetVisibleControl(Me, btnFinalize, False)
            Else
                ControlMgr.SetVisibleControl(Me, btnFinalize, True)
            End If

        End Sub

        Private Sub SetGridButtonState(isVisible As Boolean)
            ControlMgr.SetVisibleControl(Me, BtnNewLine, isVisible)
            ControlMgr.SetVisibleControl(Me, BtnSearchLines, isVisible)
            ControlMgr.SetVisibleControl(Me, BtnCancelLine, Not isVisible)
            ControlMgr.SetVisibleControl(Me, BtnSaveLines, Not isVisible)
            ControlMgr.SetVisibleControl(Me, btnApply_WRITE, isVisible)
            ControlMgr.SetVisibleControl(Me, btnFinalize, isVisible)
        End Sub

        Private Sub SetInvoiceHeaderControl()
            ControlMgr.SetEnableControl(Me, moInvoiceNumber, State.IsNewInvoice)
        End Sub
        Private Sub SetFinalizedInvoiceControls()
            ControlMgr.SetVisibleControl(Me, btnFinalize, False)
            ControlMgr.SetVisibleControl(Me, btnApply_WRITE, False)
            ControlMgr.SetEnableControl(Me, InvoiceLinesGrid, False)
            ControlMgr.SetVisibleControl(Me, BtnNewLine, False)
            ControlMgr.SetVisibleControl(Me, BtnSearchLines, false)
        End Sub

        Private Sub SetGridPageSize()
            If State.IsGridVisible Then
                If Not (State.SelectedPageSize = DEFAULT_PAGE_SIZE) Then
                    cboPageSize.SelectedValue = CType(State.SelectedPageSize, String)
                    InvoiceLinesGrid.PageSize = State.SelectedPageSize
                End If
                GetDv()
                PopulateApLinesGrid()
                SetGridItemStyleColor(InvoiceLinesGrid)
            End If

            If State.IsNewInvoice Then
                ControlMgr.SetVisibleControl(Me, BtnNewLine, False)
                ControlMgr.SetVisibleControl(Me, BtnSearchLines, false)
                ControlMgr.SetVisibleControl(Me, BtnCancelLine, False)
                ControlMgr.SetVisibleControl(Me, BtnSaveLines, False)
            End If
        End Sub

        Protected Sub BindBoPropertiesToGridHeaders()

            If State.ApInvoiceLinesBo Is Nothing Then
                State.ApInvoiceLinesBo = New ApInvoiceLines
            End If
            BindBOPropertyToGridHeader(State.ApInvoiceLinesBo, "Id", InvoiceLinesGrid.Columns(ApInvoiceLineIdCol))
            BindBOPropertyToGridHeader(State.ApInvoiceLinesBo, "LineNumber", InvoiceLinesGrid.Columns(LineNoCol))
            BindBOPropertyToGridHeader(State.ApInvoiceLinesBo, "LineType", InvoiceLinesGrid.Columns(LineTypeCol))
            BindBOPropertyToGridHeader(State.ApInvoiceLinesBo, "VendorItemCode", InvoiceLinesGrid.Columns(ItemCodeCol))
            BindBOPropertyToGridHeader(State.ApInvoiceLinesBo, "Description", InvoiceLinesGrid.Columns(DescriptionCol))
            BindBOPropertyToGridHeader(State.ApInvoiceLinesBo, "Quantity", InvoiceLinesGrid.Columns(QuantityCol))
            BindBOPropertyToGridHeader(State.ApInvoiceLinesBo, "UnitPrice", InvoiceLinesGrid.Columns(UnitPriceCol))
            BindBOPropertyToGridHeader(State.ApInvoiceLinesBo, "TotalPrice", InvoiceLinesGrid.Columns(TotalPriceCol))
            BindBOPropertyToGridHeader(State.ApInvoiceLinesBo, "UomXcd", InvoiceLinesGrid.Columns(UnitOfMeasurementCol))
            BindBOPropertyToGridHeader(State.ApInvoiceLinesBo, "PoNumber", InvoiceLinesGrid.Columns(PoNumberCol))


            ClearGridViewHeadersAndLabelsErrorSign()
        End Sub
#End Region

#Region "Populate"
        Private Sub PopulateDropDown()

            Try

                Dim serviceCenterList As New List(Of ListItem)
                For Each countryId As Guid In ElitaPlusIdentity.Current.ActiveUser.Countries
                    Dim serviceCenters As ListItem() =
                    CommonConfigManager.Current.ListManager.GetList(listCode:="ServiceCenterListByCountry",
                                                                    context:=New ListContext() With
                                                                    {
                                                                      .CountryId = countryId
                                                                    })
                    For lstIndex As Integer = 0 To serviceCenters.Count-1
                        if Not serviceCenters(lstIndex).Translation.Contains(serviceCenters(lstIndex).Code) Then
                            serviceCenters(lstIndex).Translation = serviceCenters(lstIndex).Translation +" - " + serviceCenters(lstIndex).Code
                        End If
                    Next
                    
                    
                    
                    If serviceCenters.Count > 0 Then
                        If serviceCenterList IsNot Nothing Then
                            serviceCenterList.AddRange(serviceCenters)
                        Else
                            serviceCenterList = serviceCenters.Clone()
                        End If
                    End If
                Next

                moVendorDropDown.Populate(serviceCenterList.ToArray(),
                                    New PopulateOptions() With
                                    {
                                        .AddBlankItem = True
                                    })

                moAPInvoiceTerm.Populate(CommonConfigManager.Current.ListManager.GetList("PMTTRM", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                               {
                                   .AddBlankItem = True,
                                   .BlankItemValue = String.Empty,
                                   .TextFunc = AddressOf PopulateOptions.GetDescription,
                                   .ValueFunc = AddressOf PopulateOptions.GetExtendedCode
                               })


                Dim dealerList As New List(Of ListItem)
                For Each companyId As Guid In ElitaPlusIdentity.Current.ActiveUser.Companies
                    Dim dealers As ListItem() =
                    CommonConfigManager.Current.ListManager.GetList(listCode:="DealerListByCompany",
                                                        context:=New ListContext() With
                                                        {
                                                          .CompanyId = companyId
                                                        })

                    If dealers.Count > 0 Then
                        If dealerList IsNot Nothing Then
                            dealerList.AddRange(dealers)
                        Else
                            dealerList = dealers.Clone()
                        End If
                    End If
                Next

                moDealer.Populate(dealerList.ToArray(),
                        New PopulateOptions() With
                        {
                            .AddBlankItem = True
                        })

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Private Sub PopulateApLinesGrid(Optional ByVal oAction As String = ActionNone)

            Try

                If (State.SearchDv Is Nothing) Then
                    GetDv()
                End If
                State.SearchDv.Sort = SortDirection
                State.IsGridVisible = True
                State.TotalLines = State.SearchDv.Count
                  Select Case oAction
                        Case ActionNone
                            State.BnoRow = False
                            State.ApInvoiceLineId = Guid.Empty
                            SetPageAndSelectedIndexFromGuid(State.SearchDv, Guid.Empty, InvoiceLinesGrid, 0)
                            SetGridButtonState(True)
                        Case ActionSave
                            State.BnoRow = False
                            State.ApInvoiceLineId = Guid.Empty
                            SetPageAndSelectedIndexFromGuid(State.SearchDv, State.ApInvoiceLineId, InvoiceLinesGrid, InvoiceLinesGrid.PageIndex)
                            SetGridButtonState(True)
                        Case ActionCancelDelete
                            State.BnoRow = False
                            State.ApInvoiceLineId = Guid.Empty
                            SetPageAndSelectedIndexFromGuid(State.SearchDv, Guid.Empty, InvoiceLinesGrid, InvoiceLinesGrid.PageIndex)
                            SetGridButtonState(True)
                        Case ActionEdit
                            State.BnoRow = False
                            SetPageAndSelectedIndexFromGuid(State.SearchDv, State.ApInvoiceLineId, InvoiceLinesGrid, State.PageIndex, True)
                            SetGridButtonState(False)
                        Case ActionNew
                            If State.IsNewInvoiceLine Then State.SearchDv.Table.DefaultView.Sort() = Nothing
                            Dim oRow As DataRow = State.SearchDv.Table.NewRow
                            State.ApInvoiceLinesBo = New ApInvoiceLines
                            oRow(ZeroRowIndex) = State.ApInvoiceLinesBo.Id.ToByteArray
                            oRow(ApInvoiceLines.APInvoiceLinesDV.COL_LINE_NUMBER) = State.SearchDv.Count + 1
                            State.ApInvoiceLineId = State.ApInvoiceLinesBo.Id
                            State.SearchDv.Table.Rows.Add(oRow)
                            SetPageAndSelectedIndexFromGuid(State.SearchDv, State.ApInvoiceLinesBo.Id, InvoiceLinesGrid, InvoiceLinesGrid.PageIndex, True)
                            SetGridButtonState(False)
                            State.BnoRow = True

                    End Select

                InvoiceLinesGrid.AutoGenerateColumns = False
                InvoiceLinesGrid.Columns(ItemCodeCol).SortExpression = ApInvoiceLines.ITEM_CODE_COL
                InvoiceLinesGrid.Columns(PoNumberCol).SortExpression = ApInvoiceLines.PO_NUMBER_COL
                InvoiceLinesGrid.Columns(LineNoCol).SortExpression = ApInvoiceLines.LINE_NUMBER_COL
                InvoiceLinesGrid.Columns(ItemCodeCol).SortExpression = ApInvoiceLines.ITEM_CODE_COL
                InvoiceLinesGrid.Columns(DescriptionCol).SortExpression = ApInvoiceLines.DESCRIPTION_COL
                InvoiceLinesGrid.Columns(QuantityCol).SortExpression = ApInvoiceLines.QUANTITY_COL
                InvoiceLinesGrid.Columns(UnitPriceCol).SortExpression = ApInvoiceLines.UNIT_PRICE_COL
                InvoiceLinesGrid.Columns(TotalPriceCol).SortExpression = ApInvoiceLines.TOTAL_PRICE_COL

                SortAndBindGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Private Sub GetDv()

            State.SearchDv = GetApInvoiceLineGridDataView()
            State.SearchDv.Sort = InvoiceLinesGrid.DataMember()
            InvoiceLinesGrid.DataSource = State.SearchDv

            Return
        End Sub

        Private Function GetApInvoiceLineGridDataView() As DataView

            With State
                Return .ApInvoiceLinesBo.GetApInvoiceLines(State.ApInvoiceHeaderBo.Id)
            End With

        End Function

        Private Sub PopulateLinesBoFromForm()
            Try
                With State.ApInvoiceLinesBo

                    .ApInvoiceHeaderId = State.ApInvoiceHeaderBo.Id
                    .PaidQuantity = If(.PaidQuantity, 0)
                    .MatchedQuantity = If(.MatchedQuantity, 0)
                    PopulateBOProperty(State.ApInvoiceLinesBo, "LineNumber", CType(GetSelectedGridControl(InvoiceLinesGrid, LineNoCol), TextBox))
                    PopulateBOProperty(State.ApInvoiceLinesBo, "LineType", CType(GetDropDownControlFromGrid(InvoiceLinesGrid, LineTypeCol), DropDownList), False, True)
                    PopulateBOProperty(State.ApInvoiceLinesBo, "VendorItemCode", CType(GetSelectedGridControl(InvoiceLinesGrid, ItemCodeCol), TextBox))
                    PopulateBOProperty(State.ApInvoiceLinesBo, "Description", CType(GetSelectedGridControl(InvoiceLinesGrid, ItemCodeCol), TextBox))
                    PopulateBOProperty(State.ApInvoiceLinesBo, "Quantity", CType(GetSelectedGridControl(InvoiceLinesGrid, QuantityCol), TextBox))
                    PopulateBOProperty(State.ApInvoiceLinesBo, "UnitPrice", CType(GetSelectedGridControl(InvoiceLinesGrid, UnitPriceCol), TextBox))
                   .TotalPrice = Convert.ToDecimal(.Quantity.ToString)* Convert.ToDecimal(.UnitPrice.ToString())
                    PopulateBOProperty(State.ApInvoiceLinesBo, "UomXcd", CType(GetDropDownControlFromGrid(InvoiceLinesGrid, UnitOfMeasurementCol), DropDownList), False, True)
                    PopulateBOProperty(State.ApInvoiceLinesBo, "PoNumber", CType(GetSelectedGridControl(InvoiceLinesGrid, PoNumberCol), TextBox))
                    
                End With
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Private Function GetDropDownControlFromGrid(oDataGrid As GridView, cellPosition As Integer) As Control
            Dim oItem As GridViewRow = oDataGrid.Rows(oDataGrid.SelectedIndex)
            Dim oControl As Control

            For Each gridControl As Control In oItem.Cells(cellPosition).Controls

                If gridControl.GetType().FullName.Equals("System.Web.UI.WebControls.DropDownList") Then
                    oControl = gridControl
                End If
            Next

            Return oControl
        End Function

        Private Sub PopulateBoFromForm()
            Try

                If State.ApInvoiceHeaderBo Is Nothing Then
                    State.ApInvoiceHeaderBo = New ApInvoiceHeader
                End If
                PopulateBOProperty(State.ApInvoiceHeaderBo, "InvoiceNumber", moInvoiceNumber)
                PopulateBOProperty(State.ApInvoiceHeaderBo, "InvoiceAmount", moInvoiceAmount)
                PopulateBOProperty(State.ApInvoiceHeaderBo, "InvoiceDate", moInvoiceDate)
                PopulateBOProperty(State.ApInvoiceHeaderBo, "VendorId", moVendorDropDown)
                PopulateBOProperty(State.ApInvoiceHeaderBo, "VendorAddressId", moVendorDropDown)
                PopulateBOProperty(State.ApInvoiceHeaderBo, "DealerId", moDealer)
                PopulateBOProperty(State.ApInvoiceHeaderBo, "TermXcd", moAPInvoiceTerm, False, True)
                State.ApInvoiceHeaderBo.Source = Source
                State.ApInvoiceHeaderBo.PaidAmount = PaidAmount
                State.ApInvoiceHeaderBo.PaymentStatusXcd = InvoiceStatusIncomplete
                State.ApInvoiceHeaderBo.CurrencyIsoCode = CurrencyIsoCode
                State.ApInvoiceHeaderBo.ExchangeRate = ExchangeRate
                State.ApInvoiceHeaderBo.ApprovedXcd = ApprovedXcd
                State.ApInvoiceHeaderBo.AccountingPeriod = AccountingPeriod
                State.ApInvoiceHeaderBo.Distributed = Distributed
                State.ApInvoiceHeaderBo.Posted = Posted
                State.ApInvoiceHeaderBo.CompanyId = ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Private Sub PopulateFormFromBo()

            If State.ApInvoiceHeaderBo IsNot Nothing Then

                With State.ApInvoiceHeaderBo
                    PopulateControlFromBOProperty(moInvoiceNumber, .InvoiceNumber)
                    PopulateControlFromBOProperty(moInvoiceAmount, .InvoiceAmount)
                    PopulateControlFromBOProperty(moInvoiceDate, If(.InvoiceDate Is Nothing, Date.Now.Date, State.ApInvoiceHeaderBo.InvoiceDate))
                    SetSelectedItem(moAPInvoiceTerm, If(String.IsNullOrEmpty(.TermXcd), "PMTTRM-INV_0D", .TermXcd))
                    SetSelectedItem(moVendorDropDown, .VendorId)
                    SetSelectedItem(moDealer, .DealerId)
                End With

            End If
        End Sub
#End Region

#Region "Grid Events"
        Private Sub Grid_PageSizeChanged(pageSource As Object, e As EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                InvoiceLinesGrid.PageIndex = NewCurrentPageIndex(InvoiceLinesGrid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                State.SelectedPageSize = CType(cboPageSize.SelectedValue, Integer)
                PopulateApLinesGrid()
                If State.ApInvoiceHeaderBo IsNot Nothing AndAlso Not State.ApInvoiceHeaderBo.PaymentStatusXcd = InvoiceStatusIncomplete Then
                    SetFinalizedInvoiceControls()
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Public Property SortDirection() As String
            Get
                If ViewState("SortDirection") IsNot Nothing Then
                    Return ViewState("SortDirection").ToString
                Else
                    Return ApInvoiceLines.APInvoiceLinesDV.COL_LINE_NUMBER
                End If

            End Get
            Set(value As String)
                ViewState("SortDirection") = value
            End Set

        End Property

        Protected Sub ItemCreated(sender As Object, e As GridViewRowEventArgs)
            Try
                BaseItemCreated(sender, e)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub ItemCommand(rowSource As Object, e As GridViewCommandEventArgs)

            Try
                Dim index As Integer

                If (e.CommandName = EditCommand) Then
                    index = CInt(e.CommandArgument)
                    State.IsEditMode = True

                    State.ApInvoiceLineId = New Guid(CType(InvoiceLinesGrid.Rows(index).Cells(ApInvoiceLineIdCol).FindControl(ApInvoiceLineIdControlLabel), Label).Text)
                    State.ApInvoiceLinesBo = New ApInvoiceLines(State.ApInvoiceLineId)

                    PopulateApLinesGrid(ActionEdit)
                    State.PageIndex = InvoiceLinesGrid.PageIndex
                    SetGridControls(InvoiceLinesGrid, False)
                    ControlMgr.SetVisibleControl(Me, btnFinalize, False)
                    Try
                        InvoiceLinesGrid.Rows(index).Focus()
                    Catch ex As Exception
                        InvoiceLinesGrid.Focus()
                    End Try

                ElseIf (e.CommandName = DeleteCommand) Then

                    Try
                        index = CInt(e.CommandArgument)
                        State.ApInvoiceLineId = New Guid(CType(InvoiceLinesGrid.Rows(index).Cells(ApInvoiceLineIdCol).FindControl(ApInvoiceLineIdControlLabel), Label).Text)
                        State.ApInvoiceLinesBo = New ApInvoiceLines(State.ApInvoiceLineId)
                        State.ApInvoiceLinesBo.Delete() ' Mark row as deleted
                        State.ApInvoiceLinesBo.DeleteInvoiceLine()
                        State.ActionInProgress = DetailPageCommand.Delete
                        State.IsAfterSave = True
                        State.SearchDv = Nothing
                        PopulateApLinesGrid(ActionCancelDelete)
                        MasterPage.MessageController.AddSuccess(Assurant.ElitaPlus.Common.ErrorCodes.MSG_RECORD_DELETED_OK, True)
                    Catch ex As Exception
                        State.ApInvoiceLinesBo.RejectChanges()
                        Throw
                    End Try

                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Private Sub SortAndBindGrid()
            If (State.SearchDv.Count = 0) Then

                CreateHeaderForEmptyGrid(InvoiceLinesGrid, SortDirection)

            Else
                InvoiceLinesGrid.Enabled = True
                InvoiceLinesGrid.PageSize = State.SelectedPageSize
                HighLightSortColumn(InvoiceLinesGrid, SortDirection)
                InvoiceLinesGrid.DataSource = State.SearchDv
                InvoiceLinesGrid.DataBind()
            End If
            lblRecordCount.Text = $"{State.SearchDv.Count} {TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)}"
            ControlMgr.SetVisibleControl(Me, InvoiceLinesGrid, State.IsGridVisible)
            Session("recCount") = State.SearchDv.Count
           
            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, InvoiceLinesGrid)
        End Sub
        Private Sub InvoiceLinesGrid_PageIndexChanging(pageSource As Object, e As GridViewPageEventArgs) Handles InvoiceLinesGrid.PageIndexChanging

            Try
                If (Not (State.IsEditMode)) Then
                    State.PageIndex = e.NewPageIndex
                    InvoiceLinesGrid.PageIndex = State.PageIndex
                   End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub
        Private Sub InvoiceLinesGrid_PageIndexChanged(sender As Object, e As EventArgs) Handles InvoiceLinesGrid.PageIndexChanged
            Try
                State.PageIndex  = NewCurrentPageIndex(InvoiceLinesGrid,CType(Session("RecCount"), Integer),CType(CboPageSize.SelectedValue, Integer))
                PopulateApLinesGrid(ActionSave)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
           End Try
        End Sub

        Private Sub Grid_ItemDataBound(sender As Object, e As GridViewRowEventArgs) Handles InvoiceLinesGrid.RowDataBound
            Try

                Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

                If dvRow IsNot Nothing AndAlso dvRow.Row.Table.Columns.Count > 0 Then

                    If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem Then

                        CType(e.Row.Cells(ApInvoiceLineIdCol).FindControl(ApInvoiceLineIdControlLabel), Label).Text = GetGuidStringFromByteArray(CType(dvRow(ApInvoiceLines.AP_LINE_ID), Byte()))

                        If ((State.IsEditMode OrElse State.IsNewInvoiceLine) AndAlso State.ApInvoiceLineId.ToString.Equals(GetGuidStringFromByteArray(CType(dvRow(ApInvoiceLines.AP_LINE_ID), Byte())))) Then

                            Dim moLineTypeDropDown As DropDownList = CType(e.Row.Cells(LineTypeCol).FindControl(LineTypeControlName), DropDownList)
                            SetSelectedItemByText(moLineTypeDropDown, If(String.IsNullOrEmpty(dvRow(ApInvoiceLines.LINE_TYPE_COL).ToString), "LINE", dvRow(ApInvoiceLines.LINE_TYPE_COL).ToString))

                            CType(e.Row.Cells(LineNoCol).FindControl(LineNoControlName), TextBox).Text = dvRow(ApInvoiceLines.LINE_NUMBER_COL).ToString
                            CType(e.Row.Cells(ItemCodeCol).FindControl(ItemCodeControlName), TextBox).Text = dvRow(ApInvoiceLines.ITEM_CODE_COL).ToString
                            CType(e.Row.Cells(DescriptionCol).FindControl(ItemDescriptionControlName), TextBox).Text = dvRow(ApInvoiceLines.DESCRIPTION_COL).ToString
                            CType(e.Row.Cells(QuantityCol).FindControl(QuantityControlName), TextBox).Text = dvRow(ApInvoiceLines.QUANTITY_COL).ToString
                            CType(e.Row.Cells(UnitPriceCol).FindControl(UnitPriceControlName), TextBox).Text = dvRow(ApInvoiceLines.UNIT_PRICE_COL).ToString
                            CType(e.Row.Cells(TotalPriceCol).FindControl(TotalControlName), TextBox).Text = dvRow(ApInvoiceLines.TOTAL_PRICE_COL).ToString
                            CType(e.Row.Cells(PoNumberCol).FindControl(PoNumberControlName), TextBox).Text = dvRow(ApInvoiceLines.PO_NUMBER_COL).ToString

                            Dim moUomDropDown As DropDownList = CType(e.Row.Cells(UnitOfMeasurementCol).FindControl(UnitOfMeasurementControlName), DropDownList)
                            Dim upgtermUnitOfMeasureLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("UNIT_OF_MEASURE", Thread.CurrentPrincipal.GetLanguageCode())
                            moUomDropDown.Populate(upgtermUnitOfMeasureLkl, New PopulateOptions() With
                                                 {
                                                   .AddBlankItem = True,
                                                   .BlankItemValue = String.Empty,
                                                   .TextFunc = AddressOf PopulateOptions.GetDescription,
                                                   .ValueFunc = AddressOf PopulateOptions.GetExtendedCode
                                                  })
                            if Not State.IsNewInvoiceLine Then
                                SetSelectedItem(moUomDropDown, If(String.IsNullOrEmpty(dvRow(ApInvoiceLines.UNIT_OF_MEASUREMENT_COL).ToString), "UNIT_OF_MEASURE-EACH", dvRow(ApInvoiceLines.UNIT_OF_MEASUREMENT_COL).ToString))
                            End If
                        Else
                            CType(e.Row.Cells(LineTypeCol).FindControl(LineTypeControlLabel), Label).Text = dvRow(ApInvoiceLines.LINE_TYPE_COL).ToString
                            CType(e.Row.Cells(LineNoCol).FindControl(LineNoControlLabel), Label).Text = dvRow(ApInvoiceLines.LINE_NUMBER_COL).ToString
                            CType(e.Row.Cells(ItemCodeCol).FindControl(ItemCodeControlLabel), Label).Text = dvRow(ApInvoiceLines.ITEM_CODE_COL).ToString
                            CType(e.Row.Cells(DescriptionCol).FindControl(DescriptionControlLabel), Label).Text = dvRow(ApInvoiceLines.DESCRIPTION_COL).ToString
                            CType(e.Row.Cells(QuantityCol).FindControl(QuantityControlLabel), Label).Text = dvRow(ApInvoiceLines.QUANTITY_COL).ToString
                            CType(e.Row.Cells(UnitPriceCol).FindControl(UnitPriceControlLabel), Label).Text = dvRow(ApInvoiceLines.UNIT_PRICE_COL).ToString
                            CType(e.Row.Cells(TotalPriceCol).FindControl(TotalPriceControlLabel), Label).Text = dvRow(ApInvoiceLines.TOTAL_PRICE_COL).ToString
                            CType(e.Row.Cells(UnitOfMeasurementCol).FindControl(UnitOfMeasurementControlLabel), Label).Text = dvRow(ApInvoiceLines.UNIT_OF_MEASUREMENT_COL).ToString
                            CType(e.Row.Cells(PoNumberCol).FindControl(PoNumberControlLabel), Label).Text = dvRow(ApInvoiceLines.PO_NUMBER_COL).ToString
                        End If
                    End If
                   
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_SortCommand(sortSource As Object, e As GridViewSortEventArgs) Handles InvoiceLinesGrid.Sorting
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
                PopulateApLinesGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub ItemBound(rowSource As Object, e As GridViewRowEventArgs) Handles InvoiceLinesGrid.RowDataBound

            Try
                If Not State.BnoRow Then
                    BaseItemBound(Source, e)
                End If

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Button Click Events"
        Protected Sub SaveButton_WRITE_Click(sender As Object, e As EventArgs) Handles btnApply_WRITE.Click
            Try
                Dim dvInvoice as DataView
                PopulateBoFromForm()
                If State.IsNewInvoice Then
                    dvInvoice = State.ApInvoiceHeaderBo.GetApInvoice(State.ApInvoiceHeaderBo.InvoiceNumber,State.ApInvoiceHeaderBo.VendorId)
                End If
               
                If dvInvoice IsNot Nothing AndAlso dvInvoice.Count > 0 Then
                    MasterPage.MessageController.AddError(Message.MSG_ERR_DUPLICATE_INVOICE)
                Else 
                    SaveInvoiceHeader(true)
                End If
              
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
        Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
            Try
                ReturnToCallingPage()
            Catch ex As ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
        Private Sub btnFinalize_Click(sender As Object, e As EventArgs) Handles btnFinalize.Click
            Try
                HiddenFieldPoLineSearch.Value = "N"
                If (MatchInvoice()) Then
                    PopulateBoFromForm()
                    State.ApInvoiceHeaderBo.PaymentStatusXcd = InvoiceStatusComplete
                    SaveInvoiceHeader(false)
                    MasterPage.MessageController.AddSuccess(Message.MSG_ERR_INVOICE_AMOUNT_MATCHED_WITH_LINES)
                    RunInvoiceMatching()
                    SetFinalizedInvoiceControls()
                Else
                    MasterPage.MessageController.AddError(Message.MSG_ERR_INVOICE_AMOUNT_NOT_MATCHED_WITH_LINES)
                End If

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub AddInvoiceLines_Click(sender As Object, e As EventArgs) Handles BtnNewLine.Click
            Try
                State.IsGridVisible = True
                State.IsNewInvoiceLine = True
                State.ApInvoiceLineId = Guid.Empty
                PopulateApLinesGrid(ActionNew)
                State.PageIndex = InvoiceLinesGrid.PageIndex
                SetGridControls(InvoiceLinesGrid, False)

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub BtnSaveLines_WRITE_Click(sender As Object, e As EventArgs) Handles BtnSaveLines.Click
            Try
                If SaveApInvoiceLine() = True Then
                    State.IsNewInvoiceLine = False
                    GetDv()
                    State.IsNewInvoiceLine = False
                    PopulateApLinesGrid(ActionSave)
                    SetGridControls(InvoiceLinesGrid, True)
                End If

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub BtnCancelRate_Click(sender As Object, e As EventArgs) Handles BtnCancelLine.Click

            Try
                State.IsNewInvoiceLine = False
                GetDv()
                PopulateApLinesGrid(ActionCancelDelete)
                SetGridControls(InvoiceLinesGrid, True)

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub ReturnFromEditing()

            InvoiceLinesGrid.EditIndex = NoRowSelectedIndex
            State.IsEditMode = False
            State.IsGridVisible = True
            SetInvoiceHeaderControl()
            PopulateFormFromBo()
            SetGridButtonState(True)
            SetButtonsState()

        End Sub
        Private Sub btnAddPoLines_Click(sender As Object, e As EventArgs) Handles btnAddPoLines.Click
            Try
              
                HiddenFieldPoLineSearch.Value = "N"
                State.SearchDv = Nothing
                PopulateApLinesGrid(ActionSave)

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
        Private Sub btnCancelLineSearch_Click(sender As Object, e As EventArgs) Handles btnCancelLineSearch.Click
            Try
                HiddenFieldPoLineSearch.Value = "N"
                ucApInvoiceLinesSearch.CancelAndClearSearchResult()
                State.SearchDv = Nothing
                PopulateApLinesGrid(ActionSave)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
        Private Sub BtnSearchLines_Click(sender As Object, e As EventArgs) Handles BtnSearchLines.Click
            Try
                InitPoLinesSearch()
                HiddenFieldPoLineSearch.Value = "Y"

            Catch ex As ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub InitPoLinesSearch()
            If String.IsNullOrEmpty(moVendorDropDown.SelectedValue) andAlso State.ApInvoiceHeaderBo.Id = Guid.Empty  Then Exit Sub
            ucApInvoiceLinesSearch.HostMessageController = MasterPage.MessageController

            ucApInvoiceLinesSearch.TranslationFunc = Function(value As String)
                Return TranslationBase.TranslateLabelOrMessage(value)
            End Function

            ucApInvoiceLinesSearch.TranslateGridHeaderFunc = Sub(grid As GridView)
                TranslateGridHeader(grid)
            End Sub

            ucApInvoiceLinesSearch.NewCurrentPageIndexFunc = Function(grid As GridView, intRecordCount As Integer, intNewPageSize As Integer)
                Return NewCurrentPageIndex(grid, intRecordCount, intNewPageSize)
            End Function

            ucApInvoiceLinesSearch.SortAndHighlightGridHeaderFunc = sub (grid as GridView,gridSortDirection as string)
                HighLightSortColumn(grid, gridSortDirection)
            End sub

            ucApInvoiceLinesSearch.CompanyId = State.CompanyId
            ucApInvoiceLinesSearch.ServiceCenterId =  New Guid(moVendorDropDown.SelectedItem.Value)
            ucApInvoiceLinesSearch.ServiceCenter = moVendorDropDown.SelectedItem.Text
            ucApInvoiceLinesSearch.ApInvoiceHeaderId= State.ApInvoiceHeaderBo.Id
            ucApInvoiceLinesSearch.TotalInvoiceLines= State.TotalLines
            ucApInvoiceLinesSearch.InitializeComponent()
            
        End Sub

#End Region

#Region "Invoice Operation"
        Private Function MatchInvoice() As Boolean
            Dim invoiceLinesMatched As Boolean
            Dim invoiceLineTotal As Decimal
            Try
                With State
                    If .ApInvoiceLinesBo IsNot Nothing AndAlso Not .ApInvoiceHeaderBo.Id = Guid.Empty Then

                        Dim invoiceLines = .ApInvoiceLinesBo.GetApInvoiceLines(.ApInvoiceHeaderBo.Id)

                        For Each lineRow As DataRowView In invoiceLines
                            invoiceLineTotal += Convert.ToDecimal(lineRow(ApInvoiceLines.TOTAL_PRICE_COL).ToString())
                        Next

                        If .ApInvoiceHeaderBo.InvoiceAmount = invoiceLineTotal Then
                            invoiceLinesMatched = True
                        End If

                    End If
                End With
                Return invoiceLinesMatched
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Function

        Private Sub SaveInvoiceHeader(showStatus As Boolean)
            Try

                If (State.ApInvoiceHeaderBo.IsDirty) Then
                    State.ApInvoiceHeaderBo.SaveInvoiceHeader()
                    State.IsAfterSave = True
                    State.IsNewInvoice = False
                    if showStatus Then
                        MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
                    End If
                   ReturnFromEditing()
                Else
                    MasterPage.MessageController.AddInformation(Message.MSG_RECORD_NOT_SAVED)
                    ReturnFromEditing()
                End If

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
               
            End Try
        End Sub

        Private Function SaveApInvoiceLine() As Boolean
            Dim bIsOk As Boolean = True
            Dim bIsDirty As Boolean
            If InvoiceLinesGrid.EditIndex < 0 Then Return False
            'BindBoPropertiesToGridHeader()
            With State.ApInvoiceLinesBo
                PopulateLinesBoFromForm()
                bIsDirty = .IsDirty
                .Save()
            End With
            If (bIsOk = True) Then
                If bIsDirty = True Then
                    MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION, True)
                Else
                    MasterPage.MessageController.AddError(Message.MSG_RECORD_NOT_SAVED, True)
                End If
            End If
            Return bIsOk
        End Function
       private sub RunInvoiceMatching()
           Try
               Dim matchedCnt As Integer = ApInvoiceHeader.MatchInvoice(State.ApInvoiceHeaderBo.Id)
               Dim strSuccessMsg As String = TranslationBase.TranslateLabelOrMessage("AP_INVOICE_MATCH_SUCCESS") & ": " & matchedCnt
               MasterPage.MessageController.AddSuccess(strSuccessMsg, False)

           Catch ex As Exception
               HandleErrors(ex, MasterPage.MessageController)
           End Try
       End sub
       
#End Region

    End Class
End Namespace