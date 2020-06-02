Imports System.Diagnostics
Imports Assurant.ElitaPlus.BusinessObjectsNew.Tables.Accounting.AccountPayable

Namespace Claims
    Public Class AddApInvoiceForm
        Inherits ElitaPlusSearchPage

#Region "Constants"

        Private Const AP_INVOICE_LINE_ID_COL As Integer = 1
        Private Const LINE_NO_COL As Integer = 2
        Private Const LINE_TYPE_COL As Integer = 3
        Private Const ITEM_CODE_COL As Integer = 4
        Private Const DESCRIPTION_COL As Integer = 5
        Private Const QUANTITY_COL As Integer = 6
        Private Const UNIT_PRICE_COL As Integer = 7
        Private Const TOTAL_PRICE_COL As Integer = 8
        Private Const UNIT_OF_MEASUREMENT_COL As Integer = 9
        Private Const PO_NUMBER_COL As Integer = 10
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

#End Region

        Public Sub New()
            MyBase.New(New MyState)
        End Sub

#Region "Form Events"

        Private Sub SetStateProperties()

            State.APInvoiceNumber = moInvoiceNumber.Text
            State.APInvoiceAmount = moInvoiceAmount.Text
            State.APInvoiceDate = moInvoiceDate.Text
            State.Term = moAPInvoiceTerm.SelectedValue
            State.ServiceCenter = moVendorDropDown.SelectedValue
            State.CompanyId = ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID

        End Sub

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
            Try
                MasterPage.MessageController.Clear_Hide()
                SetStateProperties()
                If Not Page.IsPostBack Then
                    SetFormTitle(PAGETITLE)
                    SetFormTab(PAGETAB)
                    SetDefaultButton(moInvoiceNumber, btnApply_WRITE)
                    SetDefaultButton(moInvoiceAmount, btnApply_WRITE)
                    SetDefaultButton(moInvoiceDate, btnApply_WRITE)
                    SetDefaultButton(moAPInvoiceTerm, btnApply_WRITE)
                    SetDefaultButton(moVendorDropDown, btnApply_WRITE)
                    SetGridItemStyleColor(PoGrid)
                    If State.APInvoiceHeaderBO Is Nothing Then
                        State.APInvoiceHeaderBO = New ApInvoiceHeader
                    End If
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
            ControlMgr.SetVisibleControl(Me, BtnCancelLine, visible)
            ControlMgr.SetVisibleControl(Me, BtnSaveLines, visible)
        End Sub

        Protected Sub BindBoPropertiesToGridHeaders()

            BindBOPropertyToGridHeader(State.APInvoiceLinesBO, "LineNumber", PoGrid.Columns(LINE_NO_COL))
            BindBOPropertyToGridHeader(State.APInvoiceLinesBO, "LineType", PoGrid.Columns(LINE_TYPE_COL))
            BindBOPropertyToGridHeader(State.APInvoiceLinesBO, "VendorItemCode", PoGrid.Columns(ITEM_CODE_COL))
            BindBOPropertyToGridHeader(State.APInvoiceLinesBO, "ItemCode", PoGrid.Columns(ITEM_CODE_COL))
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

        Private Sub PopulatePoGrid()

            Try

                If (State.SearchDv Is Nothing) Then
                    State.SearchDv = GetDV()
                End If
                State.SearchDv.Sort = SortDirection
                If (State.IsAfterSave) Then
                    State.IsAfterSave = False
                    SetPageAndSelectedIndexFromGuid(State.SearchDv, State.APInvoiceLineId, PoGrid, State.PageIndex)
                ElseIf (State.IsEditMode) Then
                    SetPageAndSelectedIndexFromGuid(State.SearchDv, State.APInvoiceLineId, PoGrid, State.PageIndex, State.IsEditMode)
                Else
                    SetPageAndSelectedIndexFromGuid(State.SearchDv, Guid.Empty, PoGrid, State.PageIndex)
                End If

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
                Return .APInvoiceLinesBO.GetApInvoiceLines()
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

                Me.PopulateBOProperty(State.APInvoiceHeaderBO, "InvoiceNumber", moInvoiceNumber)
                Me.PopulateBOProperty(State.APInvoiceHeaderBO, "InvoiceAmount", moInvoiceAmount)
                Me.PopulateBOProperty(State.APInvoiceHeaderBO, "InvoiceDate", moInvoiceDate)
                Me.PopulateBOProperty(State.APInvoiceHeaderBO, "VendorId", moVendorDropDown)
                Me.PopulateBOProperty(State.APInvoiceHeaderBO, "TermXcd", moAPInvoiceTerm)

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

        Public Property SortDirection() As String
            Get
                Return ViewState("SortDirection").ToString
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
            If Not PoGrid.BottomPagerRow.Visible Then PoGrid.BottomPagerRow.Visible = True
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
                    State.APInvoiceHeaderBO.Save()
                    State.IsAfterSave = True

                    Me.MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
                    State.SearchDv = Nothing
                    ReturnFromEditing()
                Else
                    Me.MasterPage.MessageController.AddInformation(Message.MSG_RECORD_NOT_SAVED)
                    ReturnFromEditing()
                End If
                ReturnFromEditing()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
        Private Sub ClearSearchCriteria()


        End Sub

        Private Sub ReturnFromEditing()

            PoGrid.EditIndex = NO_ROW_SELECTED_INDEX

            If (PoGrid.PageCount = 0) Then
                CreateHeaderForEmptyGrid(PoGrid, SortDirection)
                ControlMgr.SetVisibleControl(Me, PoGrid, True)
                SetGridButtonState(True)

            End If

            State.IsEditMode = False
            PopulatePoGrid()
            State.PageIndex = PoGrid.PageIndex
            SetButtonsState()

        End Sub

#End Region

    End Class
End Namespace