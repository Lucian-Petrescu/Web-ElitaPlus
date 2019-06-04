Imports System.Diagnostics
Imports System.Threading
Imports Assurant.ElitaPlus.DALObjects
Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms

Namespace Interfaces
    Partial Class VendorInventoryReconWrkForm
        Inherits ElitaPlusSearchPage
#Region "Constants"
        Public Const Url As String = "VendorInventoryReconWrkForm.aspx"
        Private Const Empty As String = ""
        Private Const DefaultPageIndex As Integer = 0
        Private Const RecordTypeCol As Integer = 1
        Private Const RejectCodeCol As Integer = 2
        Private Const RejectReasonCol As Integer = 3
        Private Const VendorSkuCol As Integer = 4
        Private Const InventoryQuantityCol As Integer = 5
        Private Const ModifiedDateCol As Integer = 6
        'Property Name
        Private Const RecordTypeProperty As String = "RecordType"
        Private Const VendorskuProperty As String = "VendorSku"
        Private Const InventoryquantityProperty As String = "InventoryQuantity"
        Private Const RejectCodeProperty As String = "RejectCode"
        Private Const RejectReasonProperty As String = "RejectReason"

        Private Const EquipmentInventoryFileDetails As String = "INVENTORY_FILE_DETAILS"
        Private Const DefaultSortColumn As String = "RECORD_TYPE"
#End Region
#Region "Member Variables"
#End Region
#Region "Properties"
        Public ReadOnly Property IsEditing() As Boolean
            Get
                IsEditing = (moDataGrid.EditIndex > NO_ITEM_SELECTED_INDEX)
            End Get
        End Property

        Private Shadows ReadOnly Property ThePage() As ElitaPlusSearchPage
            Get
                Return CType(Page, ElitaPlusSearchPage)
            End Get
        End Property
#End Region

#Region "Page State"
        Public Class PageStatus
            Public Sub New()
            End Sub
        End Class

        Class MyState
            Public PageIndex As Integer = 0
            Public FileProcessedId As Guid
            Public BundlesHashTable As Hashtable
            Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
            Public SelectedPageSize As Integer = 30
            Public SelectedPageIndex As Integer = DefaultPageIndex
            Public OReturnType As FileProcessedControllerNew.ReturnType
            Public SrchRecordType As String
            Public SrchRejectCode As String
            Public SrchRejectReason As String
            Public SortColumn As String = DefaultSortColumn
            Public SortDirection As WebControls.SortDirection = WebControls.SortDirection.Ascending
            Public ReadOnly Property SortExpression As String
                Get
                    Return String.Format("{0} {1}", SortColumn, If(SortDirection = WebControls.SortDirection.Ascending, "ASC", "DESC"))
                End Get
            End Property
        End Class

        Public Sub New()
            MyBase.New(New MyState)
        End Sub

        Protected Shadows ReadOnly Property State() As MyState
            Get
                Return CType(MyBase.State, MyState)
            End Get
        End Property

        Private Sub SetStateProperties()
            State.OReturnType = CType(CallingParameters, FileProcessedControllerNew.ReturnType)
            State.FileProcessedId = State.OReturnType.FileProcessedId
        End Sub

#End Region
#Region "Page Events"
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
            'Put user code to initialize the page here
            MasterPage.MessageController.Clear_Hide()
            MenuEnabled = False
            SetStateProperties()
            If Not Page.IsPostBack Then
                UpdateBreadCrum()
                PopulateRecordTypeDrop(moRecordTypeSearchDrop, True)
                SetGridItemStyleColor(moDataGrid)
                ShowMissingTranslations(MasterPage.MessageController)
                State.PageIndex = 0
                TranslateGridHeader(moDataGrid)
                TranslateGridControls(moDataGrid)
                PopulateReadOnly()
                PopulateGrid()
                'Set page size
                cboPageSize.SelectedValue = State.SelectedPageSize.ToString()
            Else
                CheckIfComingFromSaveConfirm()
            End If
        End Sub
#End Region
#Region "Controlling Logic"
        Private Sub UpdateBreadCrum()
            Dim pageTab As String
            Dim pageTitle As String
            Dim breadCrum As String

            pageTab = TranslationBase.TranslateLabelOrMessage("INTERFACES")
            pageTitle = TranslationBase.TranslateLabelOrMessage(EquipmentInventoryFileDetails)
            breadCrum = pageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("EQUIPMENT") _
                                    & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("EQUIPMENT_INVENTORY_FILE")

            MasterPage.UsePageTabTitleInBreadCrum = False
            MasterPage.PageTab = pageTab
            MasterPage.PageTitle = pageTitle
            MasterPage.BreadCrum = breadCrum

        End Sub
        Protected Sub CheckIfComingFromSaveConfirm()
            Dim confResponse As String = HiddenSavePagePromptResponse.Value
            Try
                If Not confResponse.Equals(Empty) Then
                    If confResponse = MSG_VALUE_YES Then
                        SavePage()
                    ElseIf confResponse = MSG_VALUE_NO Then
                        State.BundlesHashTable = Nothing
                    End If
                    HiddenSavePagePromptResponse.Value = Empty
                    HiddenIsPageDirty.Value = Empty
                    Select Case State.ActionInProgress
                        Case DetailPageCommand.Back
                            State.OReturnType.LastOperation = DetailPageCommand.Back
                            ReturnToCallingPage(State.OReturnType)
                        Case DetailPageCommand.GridPageSize
                            moDataGrid.PageIndex = NewCurrentPageIndex(moDataGrid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                            State.SelectedPageSize = CType(cboPageSize.SelectedValue, Integer)
                        Case DetailPageCommand.GridColSort
                            'Me.State.sortBy = CType(e.CommandArgument, String)
                        Case Else
                            moDataGrid.PageIndex = State.SelectedPageIndex
                    End Select
                    PopulateGrid()
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Function CreateBoFromGrid(ByVal index As Integer) As VendorloadInvReconWrk
            Dim vendorloadInvReconWrkId As Guid
            Dim vendorloadInvReconWrkInfo As VendorloadInvReconWrk
            Dim sModifiedDate As String

            moDataGrid.SelectedIndex = index
            vendorloadInvReconWrkId = New Guid(CType(moDataGrid.Rows(index).FindControl("moVendorInventoryReconWrkIdLabel"), Label).Text)
            sModifiedDate = GetGridText(moDataGrid, index, ModifiedDateCol)
            vendorloadInvReconWrkInfo = New VendorloadInvReconWrk(vendorloadInvReconWrkId, sModifiedDate)
            Return vendorloadInvReconWrkInfo
        End Function
        Private Sub SavePage()
            Dim index As Integer
            Dim vendorloadInvReconWrkInfo As VendorloadInvReconWrk
            Dim totItems As Integer = moDataGrid.Rows.Count

            If totItems > 0 Then
                vendorloadInvReconWrkInfo = CreateBoFromGrid(0)
                BindBoPropertiesToGridHeaders(vendorloadInvReconWrkInfo)
                PopulateBoFromForm(vendorloadInvReconWrkInfo)
                vendorloadInvReconWrkInfo.Save()
            End If

            totItems = totItems - 1
            For index = 1 To totItems
                vendorloadInvReconWrkInfo = CreateBoFromGrid(index)
                BindBoPropertiesToGridHeaders(vendorloadInvReconWrkInfo)
                PopulateBoFromForm(vendorloadInvReconWrkInfo)
                vendorloadInvReconWrkInfo.Save()
            Next
        End Sub
        Function IsDataGPageDirty() As Boolean
            Dim result As String = HiddenIsPageDirty.Value
            Return result.Equals("YES")
        End Function
        Private Sub EnableDisableControl(ByVal oWebControl As WebControl, ByVal grid As GridView)
            Select Case State.OReturnType.RecMode
                Case FileProcessedControllerNew.RecordMode.Rejected
                    ControlMgr.SetEnableControl(Me, oWebControl, True)
                Case FileProcessedControllerNew.RecordMode.Bypassed
                    ControlMgr.SetEnableControl(Me, oWebControl, True)
                Case FileProcessedControllerNew.RecordMode.Validated
                    ControlMgr.SetEnableControl(Me, oWebControl, False)
                    EnableAllGridControls(grid, False)
                Case FileProcessedControllerNew.RecordMode.Loaded
                    ControlMgr.SetEnableControl(Me, oWebControl, False)
                    EnableAllGridControls(grid, False)
                Case Else
                    ControlMgr.SetEnableControl(Me, oWebControl, True)
            End Select
        End Sub

        Private Sub EnableAGridControl(ByVal oControl As Control, ByVal enable As Boolean)
            Dim oWebControl As WebControl

            If TypeOf oControl Is Button Then Return
            If TypeOf oControl Is WebControl Then
                oWebControl = CType(oControl, WebControl)
                oWebControl.Enabled = enable
            End If
        End Sub

        Private Sub EnableAllGridControls(ByVal grid As GridView, ByVal enable As Boolean)
            Dim row, column As Integer
            Dim oControl As Control

            For row = 0 To (grid.Rows.Count - 1)
                For column = 0 To (grid.Rows(row).Cells.Count - 1)
                    oControl = GetGridControl(grid, row, column)
                    EnableAGridControl(oControl, enable)
                    If grid.Rows(row).Cells(column).Controls.Count = CELL_NEXT_TEMPLATE_CONTROL_SIZE Then
                        ' Image Button
                        oControl = GetGridControl(grid, row, column, True)
                        EnableAGridControl(oControl, enable)
                    End If
                Next
            Next
        End Sub
#End Region

#Region "Populate"
        Private Sub SaveGuiState()
            With State
                .SrchRejectCode = moRejectCodeText.Text.Trim
                .SrchRejectReason = moRejectReasonText.Text.Trim
                .SrchRecordType = moRecordTypeSearchDrop.SelectedItem.Text.Trim
            End With
        End Sub
        Private Sub PopulateReadOnly()
            Try
                Dim oFile As FileProcessed = New FileProcessed(State.FileProcessedId)
                With oFile
                    moFileNameText.Text = .FileName
                    moCountryText.Text = .CountryName
                    moServiceCenterText.Text = State.OReturnType.ReferenceDescription
                End With
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
        Sub PopulateRecordTypeDrop(ByVal recordTypeDrop As DropDownList, Optional ByVal addNothingSelected As Boolean = False)
            Try
                Dim oLangId As Guid = Authentication.LangId
                Dim recordTypeList As DataView = LookupListNew.GetRecordTypeLookupList(oLangId)
                'BindListControlToDataView(recordTypeDrop, recordTypeList, , , addNothingSelected)
                recordTypeDrop.Populate(CommonConfigManager.Current.ListManager.GetList("RECTYP", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                {
                 .AddBlankItem = addNothingSelected
                })
            Catch ex As Exception
                ThePage.HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
        Private Function GetGridDataView() As DataView
            With State
                Return (VendorloadInvReconWrk.LoadList(.FileProcessedId, .OReturnType.RecMode, .SrchRecordType,
                                                                                             .SrchRejectCode, .SrchRejectReason, .SelectedPageIndex,
                                                                                             .SelectedPageSize, .SortExpression))
            End With
        End Function
        Private Function GetDv() As DataView
            Dim dv As DataView
            dv = GetGridDataView()
            dv.Sort = moDataGrid.DataMember()
            Return (dv)
        End Function
        Private Sub PopulateGrid()
            Dim dv As DataView
            Dim recCount As Integer
            Try
                dv = GetDv()
                If Not State.SortExpression.Equals(Empty) Then
                    dv.Sort = State.SortExpression
                    HighLightSortColumn(moDataGrid, State.SortExpression, True)
                End If
                recCount = dv.Count
                Session("recCount") = recCount
                moDataGrid.PageSize = State.SelectedPageSize
                moDataGrid.DataSource = dv
                moDataGrid.DataBind()
                lblRecordCount.Text = recCount & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                ControlMgr.DisableAllGridControlsIfNotEditAuth(Me, moDataGrid)
                EnableDisableControl(SaveButton_WRITE, moDataGrid)
                EnableDisableControl(btnUndo_WRITE, moDataGrid)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub PopulateBoItem(ByVal vendorloadInvReconWrkInfo As VendorloadInvReconWrk, ByVal oPropertyName As String, ByVal oCellPosition As Integer)
            PopulateBOProperty(vendorloadInvReconWrkInfo, oPropertyName,
                                            CType(GetSelectedGridControl(moDataGrid, oCellPosition), TextBox))
        End Sub

        Private Sub PopulateBoDrop(ByVal vendorloadInvReconWrkInfo As VendorloadInvReconWrk, ByVal oPropertyName As String, ByVal oCellPosition As Integer)
            PopulateBOProperty(vendorloadInvReconWrkInfo, oPropertyName,
                                CType(GetSelectedGridControl(moDataGrid, oCellPosition), DropDownList), False)
        End Sub

        Private Sub PopulateBoFromForm(ByVal vendorloadInvReconWrkInfo As VendorloadInvReconWrk)

            PopulateBoDrop(vendorloadInvReconWrkInfo, RecordTypeProperty, RecordTypeCol)
            PopulateBoItem(vendorloadInvReconWrkInfo, RejectCodeProperty, RejectCodeCol)
            PopulateBoItem(vendorloadInvReconWrkInfo, RejectReasonProperty, RejectReasonCol)
            PopulateBoItem(vendorloadInvReconWrkInfo, VendorskuProperty, VendorSkuCol)
            PopulateBoItem(vendorloadInvReconWrkInfo, InventoryquantityProperty, InventoryQuantityCol)

            If ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub

#End Region
#Region "GridHandlers"

        Private Sub moDataGrid_PageIndexChanged(ByVal source As Object, ByVal e As GridViewPageEventArgs) Handles moDataGrid.PageIndexChanging
            Try
                State.SelectedPageIndex = e.NewPageIndex
                If IsDataGPageDirty() Then
                    DisplayMessage(Message.MSG_PAGE_SAVE_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSavePagePromptResponse)
                Else
                    moDataGrid.PageIndex = e.NewPageIndex
                    PopulateGrid()
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub moDataGrid_PageSizeChanged(ByVal source As Object, ByVal e As EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                If IsDataGPageDirty() Then
                    State.ActionInProgress = DetailPageCommand.GridPageSize
                    DisplayMessage(Message.MSG_PAGE_SAVE_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSavePagePromptResponse)
                Else
                    State.SelectedPageSize = CType(cboPageSize.SelectedValue, Integer)
                    PopulateGrid()
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub RowDataBound(ByVal source As Object, ByVal e As GridViewRowEventArgs) Handles moDataGrid.RowDataBound
            Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
            Dim oTextBox As TextBox

            If (itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem) AndAlso e.Row.RowIndex <> -1 Then
                With e.Row
                    PopulateControlFromBOProperty(.FindControl("moVendorInventoryReconWrkIdLabel"), dvRow(VendorloadInvReconWrkDal.ColNameVendorloadInvReconWrkId))
                    Dim oDrop As DropDownList = CType(e.Row.FindControl("moRecordTypeDrop"), DropDownList)
                    oDrop.Attributes.Add("onchange", "setDirty()")
                    PopulateRecordTypeDrop(oDrop)
                    Dim oValue As String = CType(dvRow(VendorloadInvReconWrkDal.ColNameRecordType), String)
                    SetSelectedItemByText(oDrop, oValue)
                    oTextBox = CType(.FindControl("moVendorSkuTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(VendorloadInvReconWrkDal.ColNameVendorSku))
                    oTextBox = CType(.FindControl("moInventoryQuantityTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(VendorloadInvReconWrkDal.ColNameInventoryQuantity))
                    oTextBox = CType(.FindControl("RejectReasonTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(VendorloadInvReconWrkDal.ColNameRejectReason))
                    oTextBox = CType(.FindControl("moRejectCode"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(VendorloadInvReconWrkDal.ColNameRejectCode))
                End With
            End If
            BaseItemBound(source, e)
        End Sub

        Private Sub Grid_SortCommand(ByVal source As Object, ByVal e As GridViewSortEventArgs) Handles moDataGrid.Sorting
            Try
                If (State.SortColumn = e.SortExpression) Then
                    State.SortDirection = If(State.SortDirection = WebControls.SortDirection.Ascending, WebControls.SortDirection.Descending, WebControls.SortDirection.Ascending)
                Else
                    State.SortDirection = WebControls.SortDirection.Ascending
                End If
                State.SortColumn = e.SortExpression
                State.PageIndex = 0
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
        Protected Overloads Sub ItemCreated(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
            BaseItemCreated(sender, e)
        End Sub

        Protected Overloads Sub ItemCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
            BaseItemCreated(sender, e)
        End Sub
        Protected Sub BindBoPropertiesToGridHeaders(ByVal vendorloadInvReconWrkInfo As VendorloadInvReconWrk)
            BindBOPropertyToGridHeader(vendorloadInvReconWrkInfo, "RecordType", moDataGrid.Columns(RecordTypeCol))
            BindBOPropertyToGridHeader(vendorloadInvReconWrkInfo, "RejectReason", moDataGrid.Columns(RejectReasonCol))
            BindBOPropertyToGridHeader(vendorloadInvReconWrkInfo, "RejectCode", moDataGrid.Columns(RejectCodeCol))
            BindBOPropertyToGridHeader(vendorloadInvReconWrkInfo, "VendorSku", moDataGrid.Columns(VendorSkuCol))
            BindBOPropertyToGridHeader(vendorloadInvReconWrkInfo, "InventoryQuantity", moDataGrid.Columns(InventoryQuantityCol))

            ClearGridViewHeadersAndLabelsErrorSign()
        End Sub
#End Region

#Region "Button Click Events"

        Private Sub btnBack_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBack.Click
            Try
                If IsDataGPageDirty() Or (Not State.BundlesHashTable Is Nothing AndAlso State.BundlesHashTable.Count > 0) Then
                    DisplayMessage(Message.MSG_PAGE_SAVE_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSavePagePromptResponse)
                    State.ActionInProgress = DetailPageCommand.Back
                Else
                    State.OReturnType.LastOperation = DetailPageCommand.Back
                    ReturnToCallingPage(State.OReturnType)
                End If
            Catch ex As ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub SaveButton_WRITE_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SaveButton_WRITE.Click
            Try
                SavePage()
                DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)
                HiddenIsPageDirty.Value = Empty
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnUndo_WRITE_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUndo_WRITE.Click
            Try
                State.BundlesHashTable = Nothing
                PopulateGrid()
                HiddenIsPageDirty.Value = Empty
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
        Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
            Try
                SaveGuiState()
                State.SortColumn = DefaultSortColumn
                SetGridItemStyleColor(moDataGrid)
                ShowMissingTranslations(MasterPage.MessageController)
                State.PageIndex = 0
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
        Private Sub btnClearSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnClearSearch.Click
            Try
                State.SrchRecordType = String.Empty
                State.SrchRejectCode = String.Empty
                State.SrchRejectReason = String.Empty

                moRecordTypeSearchDrop.SelectedIndex = 0
                moRejectCodeText.Text = String.Empty
                moRejectReasonText.Text = String.Empty

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
#End Region
    End Class

End Namespace