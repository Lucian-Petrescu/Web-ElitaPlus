Imports Assurant.ElitaPlus.DALObjects
Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Namespace Interfaces
    Partial Class BestReplacementReconWrkForm
        Inherits ElitaPlusSearchPage
#Region "Constants"
        Private Const EMPTY As String = ""
        Public Const URL As String = "BestReplacementReconWrkForm.aspx"
        Private Const MSG_CONFIRM_PROMPT As String = "MSG_CONFIRM_PROMPT"
        Private Const MSG_RECORD_DELETED_OK As String = "MSG_RECORD_DELETED_OK"
        Private Const MSG_RECORD_SAVED_OK As String = "MSG_RECORD_SAVED_OK"
        Private Const MSG_RECORD_NOT_SAVED As String = "MSG_RECORD_NOT_SAVED"
        Private Const CANCELLATIONS_TYPE As String = "N"
        Private Const DEFAULT_PAGE_INDEX As Integer = 0
        Private Const ID_COL As Integer = 1
        Public Const RECORD_TYPE_COL As Integer = 2
        Public Const REJECT_CODE_COL As Integer = 3
        Private Const REJECT_REASON_COL As Integer = 4
        Public Const MANUFACTURER_COL As Integer = 5
        Public Const MODEL_COL As Integer = 6
        Public Const REPLACEMENT_MANUFACTURER_COL As Integer = 7
        Public Const REPLACEMENT_MODEL_COL As Integer = 8
        Public Const PRIORITY_COL As Integer = 9
        Private Const MODIFIED_DATE_COL As Integer = 10
        Public Const CREATED_DATE_COL As Integer = 11
        Public Const CREATED_BY_COL As Integer = 12
        Public Const REJECT_MSG_PARMS_CODE As Integer = 13
        Public Const MODIFIED_BY As Integer = 14
        'Property Name
        Public Const RECORD_TYPE_Property As String = "RecordType"
        Public Const MANUFACTURER_Property As String = "Manufacturer"
        Public Const MODEL_Property As String = "Model"
        Public Const REPLACEMENT_MANUFACTURER_Property As String = "ReplacementManufacturer"
        Public Const REPLACEMENT_MODEL_Property As String = "ReplacementModel"
        Public Const PRIORITY_Property As String = "Priority"
        Public Const REJECT_CODE_Property As String = "RejectCode"
        Public Const REJECT_REASON_Property As String = "RejectReason"
        Public Const REJECT_MSG_PARMS_Property As String = "Reject_msg_parms"
        Public Const CREATED_DATE_Property As String = "CreatedDate"
        Public Const CREATED_BY_Property As String = "CreatedBy"
        Public Const MODIFIED_DATE_Property As String = "ModifiedDate"
        Public Const MODIFIED_BY_Property As String = "ModifiedBy"
        Private Const BEST_REPLACEMENT_RECON As String = "BEST_REPLACEMENT_RECON_FILE"



#End Region
#Region "Member Variables"

        Protected TempDataView As DataView = New DataView
        Private Shared pageIndex As Integer
        Private Shared pageCount As Integer
        Public selectedPageSize As Integer = DEFAULT_PAGE_SIZE
        Protected WithEvents btnSave As System.Web.UI.WebControls.Button
        Protected WithEvents btnUndo As System.Web.UI.WebControls.Button
        Protected WithEvents LbPage As System.Web.UI.WebControls.Label
        Protected WithEvents Button1 As System.Web.UI.WebControls.Button
        Protected WithEvents Button2 As System.Web.UI.WebControls.Button
        Public selectedPageIndex As Integer = DEFAULT_PAGE_INDEX
        Protected WithEvents tsHoriz As Microsoft.Web.UI.WebControls.TabStrip
        Protected WithEvents ddlCancellationReason As System.Web.UI.WebControls.DropDownList
        Protected WithEvents mpHoriz As Microsoft.Web.UI.WebControls.MultiPage

#End Region

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        'NOTE: The following placeholder declaration is required by the Web Form Designer.
        'Do not delete or move it.
        Private designerPlaceholderDeclaration As System.Object

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub
#End Region
        Protected WithEvents ErrController2 As ErrorController
#Region "Properties"
        Public ReadOnly Property IsEditing() As Boolean
            Get
                IsEditing = (Me.moDataGrid.EditIndex > Me.NO_ITEM_SELECTED_INDEX)
            End Get
        End Property

        Private Shadows ReadOnly Property ThePage() As ElitaPlusSearchPage
            Get
                Return CType(MyBase.Page, ElitaPlusSearchPage)
            End Get
        End Property

        Public Property SortDirection() As String
            Get
                Return ViewState("SortDirection").ToString
            End Get
            Set(ByVal value As String)
                ViewState("SortDirection") = value
            End Set
        End Property
#End Region


#Region "Page State"

        Public Class PageStatus

            Public Sub New()
                pageIndex = 0
                pageCount = 0
            End Sub

        End Class

        Class MyState
            Public SortExpression As String = Assurant.ElitaPlus.BusinessObjectsNew.User.UserSearchDV.COL_NETWORK_ID
            Public PageIndex As Integer = 0
            Public fileProcessedId As Guid
            Public BundlesHashTable As Hashtable
            Public BestReplacementReconWrkId As Guid
            Public ActionInProgress As DetailPageCommand = ElitaPlusPage.DetailPageCommand.Nothing_
            Public selectedPageSize As Integer = DEFAULT_PAGE_SIZE
            Public selectedPageIndex As Integer = DEFAULT_PAGE_INDEX
            Public sortBy As String
            Public oReturnType As FileProcessedController.ReturnType
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
            Me.State.oReturnType = CType(Me.CallingParameters, FileProcessedController.ReturnType)
            Me.State.fileProcessedId = Me.State.oReturnType.FileProcessedId
        End Sub

#End Region
#Region "Page Events"
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            'Put user code to initialize the page here
            Me.ErrController.Clear_Hide()
            Me.MenuEnabled = False
            Me.SetStateProperties()
            If Not Page.IsPostBack Then
                Me.SortDirection = EMPTY
                Me.SetGridItemStyleColor(moDataGrid)
                Me.ShowMissingTranslations(ErrController)
                Me.State.PageIndex = 0
                Me.TranslateGridHeader(moDataGrid)
                Me.TranslateGridControls(moDataGrid)
                PopulateReadOnly()
                PopulateGrid()
                CheckFileTypeColums()
            Else
                CheckIfComingFromSaveConfirm()
            End If


        End Sub
#End Region
#Region "Contorlling Logic"

        Protected Sub CheckIfComingFromSaveConfirm()
            Dim confResponse As String = Me.HiddenSavePagePromptResponse.Value
            Try
                If Not confResponse.Equals(EMPTY) Then
                    If confResponse = Me.MSG_VALUE_YES Then
                        SavePage()
                        Me.HiddenIsBundlesPageDirty.Value = EMPTY
                    ElseIf confResponse = Me.MSG_VALUE_NO Then
                        Me.State.BundlesHashTable = Nothing
                    End If
                    Me.HiddenSavePagePromptResponse.Value = EMPTY
                    Me.HiddenIsPageDirty.Value = EMPTY
                    Select Case Me.State.ActionInProgress
                        Case ElitaPlusPage.DetailPageCommand.Back
                            Me.State.oReturnType.LastOperation = ElitaPlusPage.DetailPageCommand.Back
                            Me.ReturnToCallingPage(Me.State.oReturnType)
                        Case ElitaPlusPage.DetailPageCommand.GridPageSize
                            Me.moDataGrid.PageIndex = NewCurrentPageIndex(moDataGrid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                            Me.State.selectedPageSize = CType(cboPageSize.SelectedValue, Integer)
                        Case ElitaPlusPage.DetailPageCommand.GridColSort
                            'Me.State.sortBy = CType(e.CommandArgument, String)
                        Case Else
                            Me.moDataGrid.PageIndex = Me.State.selectedPageIndex
                    End Select
                    PopulateGrid()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrController)
            End Try
        End Sub

        Private Function CreateBoFromGrid(ByVal index As Integer) As BestReplacementRecon
            Dim BestReplacementReconWrkId As Guid
            Dim BestReplacementReconWrkInfo As BestReplacementRecon
            Dim sModifiedDate As String

            moDataGrid.SelectedIndex = index
            BestReplacementReconWrkId = New Guid(CType(moDataGrid.Rows(index).FindControl("moBestReplacementReconWrkIdLabel"), Label).Text)
            sModifiedDate = Me.GetGridText(moDataGrid, index, Me.MODIFIED_DATE_COL)
            BestReplacementReconWrkInfo = New BestReplacementRecon(BestReplacementReconWrkId, sModifiedDate)
            Return BestReplacementReconWrkInfo
        End Function
        Private Sub SavePage()
            Dim totc As Integer = Me.moDataGrid.Columns.Count()
            Dim index As Integer = 0
            Dim BestReplacementReconWrkInfo As BestReplacementRecon
            Dim totItems As Integer = Me.moDataGrid.Rows.Count

            If totItems > 0 Then
                BestReplacementReconWrkInfo = CreateBoFromGrid(0)
                BindBoPropertiesToGridHeaders(BestReplacementReconWrkInfo)
                PopulateBOFromForm(BestReplacementReconWrkInfo)
                BestReplacementReconWrkInfo.Save()
            End If

            totItems = totItems - 1
            For index = 1 To totItems
                BestReplacementReconWrkInfo = CreateBoFromGrid(index)
                BindBoPropertiesToGridHeaders(BestReplacementReconWrkInfo)
                PopulateBOFromForm(BestReplacementReconWrkInfo)
                BestReplacementReconWrkInfo.Save()
            Next
        End Sub
        Function IsDataGPageDirty() As Boolean
            Dim Result As String = Me.HiddenIsPageDirty.Value
            Return Result.Equals("YES")
        End Function

        Function IsDataGBundlesPageDirty() As Boolean
            Dim Result As String = Me.HiddenIsBundlesPageDirty.Value
            Return Result.Equals("YES")
        End Function

        Private Sub SetColumnState(ByVal column As Byte, ByVal state As Boolean)
            Me.moDataGrid.Columns(column).Visible = state
        End Sub

        Function IsCancellationsFileType(ByVal fileName As String) As Boolean
            Return fileName.Substring(4, 1).Equals(CANCELLATIONS_TYPE)
        End Function
        Private Sub CheckFileTypeColums()
            If IsCancellationsFileType(moFileNameText.Text) Then
                ' SetColumnState(LOAD_STATUS_COL, False)
                SetColumnState(RECORD_TYPE_COL, False)
                SetColumnState(MANUFACTURER_COL, False)
                SetColumnState(MODEL_COL, False)
                SetColumnState(REPLACEMENT_MANUFACTURER_COL, False)
                SetColumnState(REPLACEMENT_MODEL_COL, False)
                SetColumnState(PRIORITY_COL, False)
                SetColumnState(REJECT_REASON_COL, False)
                SetColumnState(REJECT_CODE_COL, False)
                SetColumnState(MODIFIED_DATE_COL, False)
            End If
        End Sub
#End Region

#Region "Populate"
        Private Sub PopulateReadOnly()
            Try
                Dim oFile As FileProcessed = New FileProcessed(Me.State.fileProcessedId)
                With oFile
                    moFileNameText.Text = .FileName
                    moCompanyGroupText.Text = Me.State.oReturnType.CompanyGroupDescription
                    moMigrationPathText.Text = Me.State.oReturnType.ReferenceCode
                End With
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrController)
            End Try
        End Sub
        Sub PopulateRecordTypeDrop(ByVal recordTypeDrop As DropDownList)
            Try
                '  Dim oLangId As Guid = Authentication.LangId
                ' Dim recordTypeList As DataView = LookupListNew.GetRecordTypeLookupList(oLangId)
                'Me.BindListControlToDataView(recordTypeDrop, recordTypeList, , , False) 'RECTYP
                recordTypeDrop.Populate(CommonConfigManager.Current.ListManager.GetList("RECTYP", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions())
            Catch ex As Exception
                ThePage.HandleErrors(ex, Me.ErrController)
            End Try
        End Sub
        Private Function GetGridDataView() As DataView

            With State
                Return (Assurant.ElitaPlus.BusinessObjectsNew.BestReplacementRecon.LoadList(.fileProcessedId))
            End With

        End Function
        Private Function GetDV() As DataView

            Dim dv As DataView

            dv = GetGridDataView()
            dv.Sort = moDataGrid.DataMember()

            Return (dv)

        End Function
        Private Sub PopulateGrid()

            Dim dv As DataView
            Dim recCount As Integer = 0
            Try
                dv = GetDV()
                'dv.Sort = Me.State.sortBy
                If Not Me.SortDirection.Equals(EMPTY) Then
                    dv.Sort = Me.SortDirection
                    HighLightSortColumn(moDataGrid, Me.SortDirection)
                End If
                recCount = dv.Count
                Session("recCount") = recCount
                Me.moDataGrid.PageSize = Me.State.selectedPageSize
                Me.moDataGrid.DataSource = dv
                Me.moDataGrid.DataBind()
                Me.lblRecordCount.Text = recCount & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                ControlMgr.DisableAllGridControlsIfNotEditAuth(Me, moDataGrid)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrController)
            End Try
        End Sub

        Private Sub PopulateBOItem(ByVal BestReplacementReconWrkInfo As BestReplacementRecon, ByVal oPropertyName As String, ByVal oCellPosition As Integer)
            Me.PopulateBOProperty(BestReplacementReconWrkInfo, oPropertyName,
                                            CType(Me.GetSelectedGridControl(moDataGrid, oCellPosition), TextBox))
        End Sub

        Private Sub PopulateBODrop(ByVal BestReplacementReconWrkInfo As BestReplacementRecon, ByVal oPropertyName As String, ByVal oCellPosition As Integer)
            Me.PopulateBOProperty(BestReplacementReconWrkInfo, oPropertyName,
                                CType(Me.GetSelectedGridControl(moDataGrid, oCellPosition), DropDownList), False)
        End Sub
        Sub PopulateCancellationReasonDropDown()
            Try
                Dim oCompanyId As Guid = DealerReconWrk.CompanyId(Me.State.fileProcessedId)
                TempDataView = LookupListNew.GetCancellationReasonDealerFileLookupList(oCompanyId)
                TempDataView.Sort = "DESCRIPTION"
                TempDataView.AddNew()
            Catch ex As Exception
                ThePage.HandleErrors(ex, Me.ErrController)
            End Try
        End Sub

        Private Sub PopulateBOFromForm(ByVal BestReplacementReconWrkInfo As BestReplacementRecon)

            PopulateBODrop(BestReplacementReconWrkInfo, RECORD_TYPE_Property, RECORD_TYPE_COL)
            PopulateBOItem(BestReplacementReconWrkInfo, REJECT_CODE_Property, REJECT_CODE_COL)
            PopulateBOItem(BestReplacementReconWrkInfo, REJECT_REASON_Property, REJECT_REASON_COL)
            PopulateBOItem(BestReplacementReconWrkInfo, MANUFACTURER_Property, MANUFACTURER_COL)
            PopulateBOItem(BestReplacementReconWrkInfo, MODEL_Property, MODEL_COL)
            PopulateBOItem(BestReplacementReconWrkInfo, REPLACEMENT_MANUFACTURER_Property, REPLACEMENT_MANUFACTURER_COL)
            PopulateBOItem(BestReplacementReconWrkInfo, REPLACEMENT_MODEL_Property, REPLACEMENT_MODEL_COL)
            PopulateBOItem(BestReplacementReconWrkInfo, PRIORITY_Property, PRIORITY_COL)

            If Me.ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub

        Private Sub PopulateFormItem(ByVal oCellPosition As Integer, ByVal oPropertyValue As Object)
            Me.PopulateControlFromBOProperty(Me.GetSelectedGridControl(moDataGrid, oCellPosition), oPropertyValue)
        End Sub
#End Region
#Region "GridHandlers"

        Private Sub moDataGrid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles moDataGrid.PageIndexChanging
            Try
                Me.State.selectedPageIndex = e.NewPageIndex
                If IsDataGPageDirty() Then
                    DisplayMessage(Message.MSG_PAGE_SAVE_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, Me.HiddenSavePagePromptResponse)
                Else
                    Me.moDataGrid.PageIndex = e.NewPageIndex
                    PopulateGrid()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrController)
            End Try
        End Sub

        Private Sub moDataGrid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                If IsDataGPageDirty() Then
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.GridPageSize
                    DisplayMessage(Message.MSG_PAGE_SAVE_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, Me.HiddenSavePagePromptResponse)
                Else
                    'moDataGrid.PageIndex = NewCurrentPageIndex(moDataGrid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                    Me.State.selectedPageSize = CType(cboPageSize.SelectedValue, Integer)
                    Me.PopulateGrid()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrController)
            End Try
        End Sub

        Protected Sub ItemBound(ByVal source As Object, ByVal e As GridViewRowEventArgs) Handles moDataGrid.RowDataBound
            Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
            Dim oTextBox As TextBox

            'translate the reject message
            Dim strMsg As String, dr As DataRow, intParamCnt As Integer = 0, strParamList As String = String.Empty
            If Not dvRow Is Nothing Then
                dr = dvRow.Row
                strMsg = dr(BestReplacementReconDAL.COL_TRANSLATED_MSG).ToString.Trim
                If strMsg <> String.Empty Then
                    If Not dr(BestReplacementReconDAL.COL_MSG_PARAMETER_COUNT) Is DBNull.Value Then
                        intParamCnt = CType(dr(BestReplacementReconDAL.COL_MSG_PARAMETER_COUNT), Integer)
                    End If
                    If intParamCnt > 0 Then
                        If Not dr(BestReplacementReconDAL.COL_REJECT_MSG_PARMS) Is DBNull.Value Then
                            strParamList = dr(BestReplacementReconDAL.COL_REJECT_MSG_PARMS).ToString.Trim
                        End If
                        strMsg = TranslationBase.TranslateParameterizedMsg(strMsg, intParamCnt, strParamList).Trim
                    End If
                    ' If strMsg <> "" Then dr(BestReplacementReconDAL.COL_REJECT_REASON) = strMsg
                    dr.AcceptChanges()
                End If
            End If

            If (itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem) AndAlso e.Row.RowIndex <> -1 Then
                With e.Row
                    Dim ddl As DropDownList
                    Me.PopulateControlFromBOProperty(.FindControl("moBestReplacementReconWrkIdLabel"), dvRow(BestReplacementRecon.COL_NAME_BEST_REPLACEMENT_RECON_WRK_ID))
                    Dim oDrop As DropDownList = CType(e.Row.FindControl("moRecordTypeDrop"), DropDownList)
                    oDrop.Attributes.Add("onchange", "setDirty()")
                    PopulateRecordTypeDrop(oDrop)
                    Dim oValue As String = CType(dvRow(BestReplacementRecon.COL_NAME_RECORD_TYPE), String)
                    Me.SetSelectedItemByText(oDrop, oValue)
                    oTextBox = CType(.FindControl("moManufacturerTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(BestReplacementRecon.COL_NAME_MANUFACTURER))
                    oTextBox = CType(.FindControl("moModelTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(BestReplacementRecon.COL_NAME_MODEL))
                    oTextBox = CType(.FindControl("moreplacementmanufacturerTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(BestReplacementRecon.COL_NAME_REPLACEMENT_MANUFACTURER))
                    oTextBox = CType(.FindControl("moreplacementmodelTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(BestReplacementRecon.COL_NAME_REPLACEMENT_MODEL))
                    oTextBox = CType(.FindControl("moPriorityTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(BestReplacementRecon.COL_NAME_PRIORITY))
                    oTextBox = CType(.FindControl("RejectReasonTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(BestReplacementRecon.COL_NAME_REJECT_REASON))
                    oTextBox = CType(.FindControl("moRejectCode"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(BestReplacementRecon.COL_NAME_REJECT_CODE))
                End With
            End If
            BaseItemBound(source, e)
        End Sub

        Private Sub Grid_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles moDataGrid.Sorting
            Try
                Dim spaceIndex As Integer = Me.SortDirection.LastIndexOf(" ")
                If spaceIndex > 0 AndAlso Me.SortDirection.Substring(0, spaceIndex).Equals(e.SortExpression) Then
                    If Me.SortDirection.EndsWith(" ASC") Then
                        Me.SortDirection = e.SortExpression + " DESC"
                    Else
                        Me.SortDirection = e.SortExpression + " ASC"
                    End If
                Else
                    Me.SortDirection = e.SortExpression + " ASC"
                End If
                Me.State.PageIndex = 0
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrController)
            End Try
        End Sub
        Protected Overloads Sub ItemCreated(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
            BaseItemCreated(sender, e)
        End Sub

        Protected Overloads Sub ItemCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
            BaseItemCreated(sender, e)
        End Sub
        Protected Sub BindBoPropertiesToGridHeaders(ByVal BestReplacementconWrkInfo As BestReplacementRecon)
            Me.BindBOPropertyToGridHeader(BestReplacementconWrkInfo, "RecordType", Me.moDataGrid.Columns(Me.RECORD_TYPE_COL))
            Me.BindBOPropertyToGridHeader(BestReplacementconWrkInfo, "Manufacturer", Me.moDataGrid.Columns(Me.MANUFACTURER_COL))
            Me.BindBOPropertyToGridHeader(BestReplacementconWrkInfo, "Model", Me.moDataGrid.Columns(Me.MODEL_COL))
            Me.BindBOPropertyToGridHeader(BestReplacementconWrkInfo, "ReplacementManufacturer", Me.moDataGrid.Columns(Me.REPLACEMENT_MANUFACTURER_COL))
            Me.BindBOPropertyToGridHeader(BestReplacementconWrkInfo, "ReplacementModel", Me.moDataGrid.Columns(Me.REPLACEMENT_MODEL_COL))
            Me.BindBOPropertyToGridHeader(BestReplacementconWrkInfo, "Priority", Me.moDataGrid.Columns(Me.PRIORITY_COL))
            Me.BindBOPropertyToGridHeader(BestReplacementconWrkInfo, "RejectReason", Me.moDataGrid.Columns(Me.REJECT_REASON_COL))
            Me.BindBOPropertyToGridHeader(BestReplacementconWrkInfo, "RejectCode", Me.moDataGrid.Columns(Me.REJECT_CODE_COL))
            Me.ClearGridViewHeadersAndLabelsErrSign()
        End Sub
        Private Sub SetFocusOnEditableFieldInGrid(ByVal grid As DataGrid, ByVal cellPosition As Integer, ByVal controlName As String, ByVal itemIndex As Integer)
            'Set focus on the Description TextBox for the EditItemIndex row
            Dim desc As TextBox = CType(grid.Items(itemIndex).Cells(cellPosition).FindControl(controlName), TextBox)
            SetFocus(desc)
        End Sub
#End Region

        Private Function GetBundlesDV() As DataView
            Dim dv As DataView

            dv = GetBundlesDataSet().Tables(DealerReconWrkBundlesDAL.TABLE_NAME).DefaultView
            Return dv
        End Function
        Private Function GetBundlesDataSet() As DataSet
            With State
                Return (Assurant.ElitaPlus.BusinessObjectsNew.DealerReconWrkBundles.LoadList(.BestReplacementReconWrkId))
            End With
        End Function

#Region "Button Click Events"

        Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
            Try
                If IsDataGPageDirty() Or (Not Me.State.BundlesHashTable Is Nothing AndAlso Me.State.BundlesHashTable.Count > 0) Then
                    DisplayMessage(Message.MSG_PAGE_SAVE_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, Me.HiddenSavePagePromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    Me.State.oReturnType.LastOperation = ElitaPlusPage.DetailPageCommand.Back
                    Me.ReturnToCallingPage(Me.State.oReturnType)
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrController)
            End Try
        End Sub

        Private Sub SaveButton_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveButton_WRITE.Click
            Try
                SavePage()
                Me.DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                Me.HiddenIsPageDirty.Value = EMPTY
                PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrController)
            End Try
        End Sub

        Private Sub btnUndo_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUndo_WRITE.Click
            Try
                Me.State.BundlesHashTable = Nothing
                PopulateGrid()
                Me.HiddenIsPageDirty.Value = EMPTY
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrController)
            End Try
        End Sub
#End Region
    End Class

End Namespace