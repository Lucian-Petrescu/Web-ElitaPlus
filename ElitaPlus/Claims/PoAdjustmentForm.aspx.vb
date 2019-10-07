Imports Assurant.ElitaPlus.BusinessObjectsNew.Tables.Accounting.AccountPayable

Namespace Claims.AccountPayable
    Public Class PoAdjustmentForm
        Inherits ElitaPlusSearchPage

#Region "Constants"

        Private Const PO_LINE_ID_COL As Integer = 1
        Private Const VENDOR_COL As Integer = 2
        Private Const PO_NUMBER_COL As Integer = 3
        Private Const LINE_NO_COL As Integer = 4
        Private Const ITEM_CODE_COL As Integer = 5
        Private Const DESCRIPTION_COL As Integer = 6
        Private Const QUANTITY_COL As Integer = 7
        Private Const UNIT_PRICE_COL As Integer = 8
        Private Const EXTENDED_PRICE_COL As Integer = 9
        Private Const MSG_CONFIRM_PROMPT As String = "MSG_CONFIRM_PROMPT"
        Private Const MSG_RECORD_SAVED_OK As String = "MSG_RECORD_SAVED_OK"
        Private Const MSG_RECORD_NOT_SAVED As String = "MSG_RECORD_NOT_SAVED"

        Private Const PO_LINE_ID_CONTROL_LABEL As String = "lblPoLineId"
        Private Const VENDOR_CONTROL_LABEL As String = "lblVendor"
        Private Const PO_NUMBER_CONTROL_LABEL As String = "lblPoNumber"
        Private Const LINE_NO_CONTROL_LABEL As String = "lblLineNumber"
        Private Const ITEM_CODE_CONTROL_LABEL As String = "lblItemCode"
        Private Const DESCRIPTION_CONTROL_LABEL As String = "lblDescription"
        Private Const QUANTITY_CONTROL_LABEL As String = "lblQuantity"
        Private Const QUANTITY_CONTROL_NAME As String = "txtQuantity"
        Private Const UNIT_PRICE_CONTROL_LABEL As String = "lblUnitprice"
        Private Const EXTENDED_PRICE_CONTROL_LABEL As String = "lblExtendePrice"


        Private Const EDIT_COMMAND As String = "EditRecord"
        Private Const SORT_COMMAND As String = "Sort"



        Private Const NO_ROW_SELECTED_INDEX As Integer = -1

#End Region

#Region "Form Events"

        Private Sub SetStateProperties()

        State.PoNumber = txtPoNumber.Text
        State.VendorMask =txtVendorcode.Text
        State.CompanyId = ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID
            
    End Sub

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Try 
                POAdjustmentErrorController.Clear_Hide()
                SetStateProperties()
                If Not Page.IsPostBack Then
                    SortDirection = PoAdjustment.PO_NUMBER_COL
                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    SetDefaultButton(txtVendorcode, SearchButton)
                    SetDefaultButton(txtPoNumber, SearchButton)
                    SetGridItemStyleColor(PoGrid)
                    If State.PoAdjustmentBo Is Nothing Then
                        State.PoAdjustmentBo = New PoAdjustment
                    End If
                   SetButtonsState()
                    State.PageIndex = 0
                    TranslateGridHeader(PoGrid)
                    TranslateGridControls(PoGrid)
                End If
                BindBoPropertiesToGridHeaders()
            Catch ex As Exception
                HandleErrors(ex, POAdjustmentErrorController)
            End Try
            ShowMissingTranslations(POAdjustmentErrorController)
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
        Class MyState
            Public PageIndex As Integer = 0
            Public PoAdjustmentBo As PoAdjustment
            Public PoLineId As Guid
            public PoNumber As string
            Public LangId As Guid
            Public DescriptionMask As String
            Public VendorMask As String
            public LineNumber As string
            Public CompanyId As Guid
            Public IsAfterSave As Boolean
            Public IsEditMode As Boolean
            Public IsGridVisible As Boolean
            Public SearchDv As DataView = Nothing
            Public Canceling As Boolean
            Public SelectedPageSize As Integer = DEFAULT_PAGE_SIZE
            Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
            Public BnoRow As Boolean = False
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

#Region "Control"
        Private Sub SetButtonsState()

            If (State.IsEditMode) Then
                ControlMgr.SetVisibleControl(Me, SaveButton_WRITE, True)
                ControlMgr.SetVisibleControl(Me, CancelButton, True)
                ControlMgr.SetEnableControl(Me, SearchButton, False)
                ControlMgr.SetEnableControl(Me, ClearButton, False)
                MenuEnabled = False
            Else
                ControlMgr.SetVisibleControl(Me, SaveButton_WRITE, False)
                ControlMgr.SetVisibleControl(Me, CancelButton, False)
                ControlMgr.SetEnableControl(Me, SearchButton, True)
                ControlMgr.SetEnableControl(Me, ClearButton, True)
                MenuEnabled = True
            End If

        End Sub

       Protected Sub BindBoPropertiesToGridHeaders()

            BindBOPropertyToGridHeader(State.PoAdjustmentBo, "Vendor", PoGrid.Columns(VENDOR_COL))
            BindBOPropertyToGridHeader(State.PoAdjustmentBo, "PoNumber", PoGrid.Columns(PO_NUMBER_COL))
            BindBOPropertyToGridHeader(State.PoAdjustmentBo, "LineNumber", PoGrid.Columns(LINE_NO_COL))
            BindBOPropertyToGridHeader(State.PoAdjustmentBo, "ItemCode", PoGrid.Columns(ITEM_CODE_COL))
            BindBOPropertyToGridHeader(State.PoAdjustmentBo, "Quantity", PoGrid.Columns(QUANTITY_COL))
            BindBOPropertyToGridHeader(State.PoAdjustmentBo, "UnitPrice", PoGrid.Columns(UNIT_PRICE_COL))
            BindBOPropertyToGridHeader(State.PoAdjustmentBo, "ExtendedPrice", PoGrid.Columns(EXTENDED_PRICE_COL))
            BindBOPropertyToGridHeader(State.PoAdjustmentBo, "Description", PoGrid.Columns(DESCRIPTION_COL))
            
           ClearGridViewHeadersAndLabelsErrorSign()
        End Sub

        Private Sub SetFocusOnEditableFieldInGrid(ByVal grid As GridView, ByVal cellPosition As Integer, ByVal controlName As String, ByVal itemIndex As Integer)
            'Set focus on the Description TextBox for the EditItemIndex row
            Dim desc As TextBox = CType(grid.Rows(itemIndex).Cells(cellPosition).FindControl(controlName), TextBox)
            SetFocus(desc)
        End Sub

        Private Sub HideEditAndDeleteButtons()
            For Each row As GridViewRow In PoGrid.Rows
                row.Cells(0).Controls.RemoveAt(0)
                If row.RowIndex <> PoGrid.EditIndex Then
                    row.Cells(1).Controls.RemoveAt(0)
                End If
            Next
        End Sub


#End Region

#Region "Populate"

        Private Sub PopulatePoGrid()

            Dim dv As DataView
            Try
               
                If (State.searchDV Is Nothing) Then
                    State.searchDV = GetDV()
                End If
                State.searchDV.Sort = SortDirection
                If (State.IsAfterSave) Then
                    State.IsAfterSave = False
                    SetPageAndSelectedIndexFromGuid(State.searchDV, State.PoLineId, PoGrid, State.PageIndex)
                ElseIf (State.IsEditMode) Then
                    SetPageAndSelectedIndexFromGuid(State.searchDV, State.PoLineId, PoGrid, State.PageIndex, State.IsEditMode)
                Else
                    SetPageAndSelectedIndexFromGuid(State.searchDV, Guid.Empty, PoGrid, State.PageIndex)
                End If

                PoGrid.AutoGenerateColumns = False
                PoGrid.Columns(VENDOR_COL).SortExpression = PoAdjustment.VENDOR_COL
                PoGrid.Columns(PO_NUMBER_COL).SortExpression = PoAdjustment.PO_NUMBER_COL
                PoGrid.Columns(LINE_NO_COL).SortExpression = PoAdjustment.LINE_NUMBER_COL
                PoGrid.Columns(ITEM_CODE_COL).SortExpression = PoAdjustment.ITEM_CODE_COL
                PoGrid.Columns(DESCRIPTION_COL).SortExpression = PoAdjustment.DESCRIPTION_COL
                PoGrid.Columns(QUANTITY_COL).SortExpression = PoAdjustment.QUANTITY_COL
                PoGrid.Columns(UNIT_PRICE_COL).SortExpression = PoAdjustment.UNIT_PRICE_COL
                PoGrid.Columns(EXTENDED_PRICE_COL).SortExpression = PoAdjustment.EXTENDED_PRICE_COL

                SortAndBindGrid()
            Catch ex As Exception
                HandleErrors(ex, POAdjustmentErrorController)
            End Try

        End Sub

        Private Function GetDV() As DataView

            Dim dv As DataView

            State.searchDV = GetPoGridDataView()
            State.searchDV.Sort = PoGrid.DataMember()
            PoGrid.DataSource = State.searchDV

            Return (State.searchDV)

        End Function

        Private Function GetPoGridDataView() As DataView

            With State
                Return .PoAdjustmentBo.GetApPoLines(.VendorMask, .PoNumber, .CompanyId)
            End With

        End Function

     Private Sub PopulateBoFromForm()
            Try
                With State.PoAdjustmentBo
                    .PoLineId = Guid.Parse( CType(PoGrid.Rows(PoGrid.EditIndex).Cells(PO_LINE_ID_COL).FindControl(PO_LINE_ID_CONTROL_LABEL), Label).Text)
                    .Vendor = CType(PoGrid.Rows(PoGrid.EditIndex).Cells(VENDOR_COL).FindControl(VENDOR_CONTROL_LABEL), Label).Text
                    .PoNumber = CType(PoGrid.Rows(PoGrid.EditIndex).Cells(PO_NUMBER_COL).FindControl(PO_NUMBER_CONTROL_LABEL), Label).Text
                    .LineNumber = CType(PoGrid.Rows(PoGrid.EditIndex).Cells(LINE_NO_COL).FindControl(LINE_NO_CONTROL_LABEL), Label).Text
                    .ItemCode = CType(PoGrid.Rows(PoGrid.EditIndex).Cells(ITEM_CODE_COL).FindControl(ITEM_CODE_CONTROL_LABEL), Label).Text
                    .Description = CType(PoGrid.Rows(PoGrid.EditIndex).Cells(DESCRIPTION_COL).FindControl(DESCRIPTION_CONTROL_LABEL), Label).Text
                    .Quantity = CType(PoGrid.Rows(PoGrid.EditIndex).Cells(QUANTITY_COL).FindControl(QUANTITY_CONTROL_NAME), TextBox).Text
                    .UnitPrice = CType(PoGrid.Rows(PoGrid.EditIndex).Cells(UNIT_PRICE_COL).FindControl(UNIT_PRICE_CONTROL_LABEL), Label).Text
                    .ExtendedPrice = CType(PoGrid.Rows(PoGrid.EditIndex).Cells(EXTENDED_PRICE_COL).FindControl(EXTENDED_PRICE_CONTROL_LABEL), Label).Text
                    .CompanyId = ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID
                End With
            Catch ex As Exception
                HandleErrors(ex, POAdjustmentErrorController)
            End Try

        End Sub

        Private Sub PopulateFormFromBo()

            Dim gridRowIdx As Integer = PoGrid.EditIndex
            Try
                With State.PoAdjustmentBo
                    If (Not .vendor Is Nothing) Then
                        CType(PoGrid.Rows(gridRowIdx).Cells(VENDOR_COL).FindControl(VENDOR_CONTROL_LABEL), Label).Text = .Vendor
                    End If

                    If (Not .Ponumber Is Nothing) Then
                        CType(PoGrid.Rows(gridRowIdx).Cells(PO_NUMBER_COL).FindControl(PO_NUMBER_CONTROL_LABEL), Label).Text = .PoNumber
                    End If

                    If (Not .LineNumber Is Nothing) Then
                        CType(PoGrid.Rows(gridRowIdx).Cells(LINE_NO_COL).FindControl(LINE_NO_CONTROL_LABEL), Label).Text = .LineNumber
                    End If

                    If (Not .Itemcode Is Nothing) Then
                        CType(PoGrid.Rows(gridRowIdx).Cells(ITEM_CODE_COL).FindControl(ITEM_CODE_CONTROL_LABEL), Label).Text = .ItemCode
                    End If

                    If (Not .Description Is Nothing) Then
                        CType(PoGrid.Rows(gridRowIdx).Cells(DESCRIPTION_COL).FindControl(DESCRIPTION_CONTROL_LABEL), Label).Text = .Description
                    End If

                   CType(PoGrid.Rows(gridRowIdx).Cells(QUANTITY_COL).FindControl(QUANTITY_CONTROL_NAME), textbox).Text = .Quantity
                   CType(PoGrid.Rows(gridRowIdx).Cells(UNIT_PRICE_COL).FindControl(UNIT_PRICE_CONTROL_LABEL), label).Text = .UnitPrice
                   CType(PoGrid.Rows(gridRowIdx).Cells(EXTENDED_PRICE_COL).FindControl(EXTENDED_PRICE_CONTROL_LABEL), label).Text = .ExtendedPrice
                   CType(PoGrid.Rows(gridRowIdx).Cells(PO_LINE_ID_COL).FindControl(PO_LINE_ID_CONTROL_LABEL), Label).Text = .PoLineId.ToString
                End With
            Catch ex As Exception
                HandleErrors(ex, POAdjustmentErrorController)
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
                HandleErrors(ex, POAdjustmentErrorController)
            End Try
        End Sub

        Protected Sub ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)

            Try
                Dim index As Integer

                If (e.CommandName = EDIT_COMMAND) Then
                    index = CInt(e.CommandArgument)
                    'Do the Edit here

                    'Set the IsEditMode flag to TRUE
                    State.IsEditMode = True

                    State.PoLineId = New Guid(CType(PoGrid.Rows(index).Cells(PO_LINE_ID_COL).FindControl(PO_LINE_ID_CONTROL_LABEL), Label).Text)
                    State.PoAdjustmentBo = New PoAdjustment(State.VendorMask,State.PoNumber,State.PoLineId,State.CompanyId)

                    PopulatePoGrid()
                    State.PageIndex = PoGrid.PageIndex
                    SetGridControls(PoGrid, False)
                    SetFocusOnEditableFieldInGrid(PoGrid, QUANTITY_COL, QUANTITY_CONTROL_NAME, index)
                    PopulateFormFromBO()
                    SetButtonsState()

                End If
            Catch ex As Exception
                HandleErrors(ex, POAdjustmentErrorController)
            End Try

        End Sub
        Private Sub SortAndBindGrid()
            TranslateGridControls(PoGrid)

            If (State.searchDV.Count = 0) Then

                State.bnoRow = True
                CreateHeaderForEmptyGrid(PoGrid, SortDirection)
            Else
                State.bnoRow = False
                PoGrid.Enabled = True
                PoGrid.DataSource = State.searchDV
                HighLightSortColumn(PoGrid, SortDirection)
                PoGrid.DataBind()
            End If
            If Not PoGrid.BottomPagerRow.Visible Then PoGrid.BottomPagerRow.Visible = True
            ControlMgr.SetVisibleControl(Me, PoGrid, State.IsGridVisible)
            ControlMgr.SetVisibleControl(Me, trPageSize, PoGrid.Visible)

            Session("recCount") = State.searchDV.Count

            If PoGrid.Visible Then
                    lblRecordCount.Text =
                        $"{State.searchDV.Count} {TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND) }"
            End If
            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, PoGrid)
        End Sub

        Private Sub Grid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                PoGrid.PageIndex = NewCurrentPageIndex(PoGrid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                State.selectedPageSize = CType(cboPageSize.SelectedValue, Integer)
                PopulatePoGrid()
            Catch ex As Exception
                HandleErrors(ex, POAdjustmentErrorController)
            End Try
        End Sub

        Private Sub POGrid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles PoGrid.PageIndexChanging

            Try
                If (Not (State.IsEditMode)) Then
                    State.PageIndex = e.NewPageIndex
                    PoGrid.PageIndex = State.PageIndex
                    PopulatePoGrid()
                    PoGrid.SelectedIndex = NO_ITEM_SELECTED_INDEX
                End If
            Catch ex As Exception
                HandleErrors(ex, POAdjustmentErrorController)
            End Try

        End Sub

        Private Sub Grid_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles PoGrid.RowDataBound
            Try
                'BaseItemBound(source, e)
                Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

                If Not dvRow Is Nothing And Not State.bnoRow Then

                    If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                        CType(e.Row.Cells(PO_LINE_ID_COL).FindControl(PO_LINE_ID_CONTROL_LABEL), Label).Text = GetGuidStringFromByteArray(CType(dvRow(PoAdjustment.PO_LINE_ID_COL), Byte()))

                        If (State.IsEditMode = True _
                                AndAlso State.PoLineId.ToString.Equals(GetGuidStringFromByteArray(CType(dvRow(PoAdjustment.PO_LINE_ID_COL), Byte())))) Then
                            CType(e.Row.Cells(QUANTITY_COL).FindControl(QUANTITY_CONTROL_NAME), TextBox).Text = dvRow(PoAdjustment.QUANTITY_COL).ToString
                        Else
                            CType(e.Row.Cells(UNIT_PRICE_COL).FindControl(UNIT_PRICE_CONTROL_LABEL), label).Text = dvRow(PoAdjustment.UNIT_PRICE_COL).ToString
                        End If

                        CType(e.Row.Cells(VENDOR_COL).FindControl(VENDOR_CONTROL_LABEL), label).Text = dvRow(PoAdjustment.VENDOR_COL).ToString
                        CType(e.Row.Cells(PO_NUMBER_COL).FindControl(PO_NUMBER_CONTROL_LABEL), label).Text = dvRow(PoAdjustment.PO_NUMBER_COL).ToString
                        CType(e.Row.Cells(LINE_NO_COL).FindControl(LINE_NO_CONTROL_LABEL), label).Text = dvRow(PoAdjustment.LINE_NUMBER_COL).ToString
                        CType(e.Row.Cells(ITEM_CODE_COL).FindControl(ITEM_CODE_CONTROL_LABEL), label).Text = dvRow(PoAdjustment.ITEM_CODE_COL).ToString
                        CType(e.Row.Cells(DESCRIPTION_COL).FindControl(DESCRIPTION_CONTROL_LABEL), label).Text = dvRow(PoAdjustment.DESCRIPTION_COL).ToString
                        CType(e.Row.Cells(UNIT_PRICE_COL).FindControl(UNIT_PRICE_CONTROL_LABEL), label).Text = dvRow(PoAdjustment.UNIT_PRICE_COL).ToString
                        CType(e.Row.Cells(EXTENDED_PRICE_COL).FindControl(EXTENDED_PRICE_CONTROL_LABEL), label).Text = dvRow(PoAdjustment.EXTENDED_PRICE_COL).ToString
                       
                    End If
                End If
            Catch ex As Exception
                HandleErrors(ex, POAdjustmentErrorController)
            End Try
        End Sub
        Private Sub Grid_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles PoGrid.Sorting
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
                HandleErrors(ex, POAdjustmentErrorController)
            End Try
        End Sub
        Protected Sub ItemBound(ByVal source As Object, ByVal e As GridViewRowEventArgs) Handles PoGrid.RowDataBound

            Try
                If Not State.bnoRow Then
                    BaseItemBound(source, e)
                End If

            Catch ex As Exception
                HandleErrors(ex, POAdjustmentErrorController)
            End Try
        End Sub

#End Region

#Region "Button Click Events"
        Protected Sub ClearButton_Click(sender As Object, e As EventArgs) Handles ClearButton.Click
            ClearSearchCriteria()
        End Sub

        Protected Sub SearchButton_Click(sender As Object, e As EventArgs) Handles SearchButton.Click
            Try
                State.IsGridVisible = True
                State.searchDV = Nothing
                PopulatePogrid()
                State.PageIndex = PoGrid.PageIndex
            Catch ex As Exception
                HandleErrors(ex, Me.POAdjustmentErrorController)
            End Try
        End Sub

        Protected Sub SaveButton_WRITE_Click(sender As Object, e As EventArgs) Handles SaveButton_WRITE.Click
            Try
                PopulateBOFromForm()
                If (State.PoAdjustmentBo.IsDirty) Then
                    State.PoAdjustmentBo.Save()
                    State.IsAfterSave = True
                   
                    AddInfoMsg(MSG_RECORD_SAVED_OK)
                    State.searchDV = Nothing
                    ReturnFromEditing()
                Else
                    AddInfoMsg(MSG_RECORD_NOT_SAVED)
                    ReturnFromEditing()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, POAdjustmentErrorController)
            End Try
        End Sub

        Protected Sub CancelButton_Click(sender As Object, e As EventArgs) Handles CancelButton.Click
            Try
                Me.PoGrid.SelectedIndex = NO_ITEM_SELECTED_INDEX
                Me.State.Canceling = True
                ReturnFromEditing()
            Catch ex As Exception
                Me.HandleErrors(ex, POAdjustmentErrorController)
            End Try
        End Sub

        Private Sub ClearSearchCriteria()

            Try
                txtVendorcode.Text = String.Empty
                txtPoNumber.Text = String.Empty
               'Update Page State
                With Me.State
                    .VendorMask = txtVendorcode.Text
                    .PoNumber = txtPoNumber.Text
                End With
            Catch ex As Exception
                Me.HandleErrors(ex, POAdjustmentErrorController)
            End Try

        End Sub

        Private Sub ReturnFromEditing()

            PoGrid.EditIndex = NO_ROW_SELECTED_INDEX

            If (Me.PoGrid.PageCount = 0) Then
               ControlMgr.SetVisibleControl(Me, PoGrid, False)
            Else
                ControlMgr.SetVisibleControl(Me, PoGrid, True)
            End If

            State.IsEditMode = False
            PopulatePoGrid()
            State.PageIndex = PoGrid.PageIndex
            SetButtonsState()

        End Sub

#End Region

    End Class
End Namespace