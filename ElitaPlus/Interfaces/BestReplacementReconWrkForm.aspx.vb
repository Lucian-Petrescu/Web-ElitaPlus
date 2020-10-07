﻿Imports Assurant.ElitaPlus.DALObjects
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

        Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub
#End Region
        Protected WithEvents ErrController2 As ErrorController
#Region "Properties"
        Public ReadOnly Property IsEditing() As Boolean
            Get
                IsEditing = (moDataGrid.EditIndex > NO_ITEM_SELECTED_INDEX)
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
            Set(value As String)
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
            State.oReturnType = CType(CallingParameters, FileProcessedController.ReturnType)
            State.fileProcessedId = State.oReturnType.FileProcessedId
        End Sub

#End Region
#Region "Page Events"
        Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
            'Put user code to initialize the page here
            ErrController.Clear_Hide()
            MenuEnabled = False
            SetStateProperties()
            If Not Page.IsPostBack Then
                SortDirection = EMPTY
                SetGridItemStyleColor(moDataGrid)
                ShowMissingTranslations(ErrController)
                State.PageIndex = 0
                TranslateGridHeader(moDataGrid)
                TranslateGridControls(moDataGrid)
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
            Dim confResponse As String = HiddenSavePagePromptResponse.Value
            Try
                If Not confResponse.Equals(EMPTY) Then
                    If confResponse = MSG_VALUE_YES Then
                        SavePage()
                        HiddenIsBundlesPageDirty.Value = EMPTY
                    ElseIf confResponse = MSG_VALUE_NO Then
                        State.BundlesHashTable = Nothing
                    End If
                    HiddenSavePagePromptResponse.Value = EMPTY
                    HiddenIsPageDirty.Value = EMPTY
                    Select Case State.ActionInProgress
                        Case ElitaPlusPage.DetailPageCommand.Back
                            State.oReturnType.LastOperation = ElitaPlusPage.DetailPageCommand.Back
                            ReturnToCallingPage(State.oReturnType)
                        Case ElitaPlusPage.DetailPageCommand.GridPageSize
                            moDataGrid.PageIndex = NewCurrentPageIndex(moDataGrid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                            State.selectedPageSize = CType(cboPageSize.SelectedValue, Integer)
                        Case ElitaPlusPage.DetailPageCommand.GridColSort
                            'Me.State.sortBy = CType(e.CommandArgument, String)
                        Case Else
                            moDataGrid.PageIndex = State.selectedPageIndex
                    End Select
                    PopulateGrid()
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrController)
            End Try
        End Sub

        Private Function CreateBoFromGrid(index As Integer) As BestReplacementRecon
            Dim BestReplacementReconWrkId As Guid
            Dim BestReplacementReconWrkInfo As BestReplacementRecon
            Dim sModifiedDate As String

            moDataGrid.SelectedIndex = index
            BestReplacementReconWrkId = New Guid(CType(moDataGrid.Rows(index).FindControl("moBestReplacementReconWrkIdLabel"), Label).Text)
            sModifiedDate = GetGridText(moDataGrid, index, MODIFIED_DATE_COL)
            BestReplacementReconWrkInfo = New BestReplacementRecon(BestReplacementReconWrkId, sModifiedDate)
            Return BestReplacementReconWrkInfo
        End Function
        Private Sub SavePage()
            Dim totc As Integer = moDataGrid.Columns.Count()
            Dim index As Integer = 0
            Dim BestReplacementReconWrkInfo As BestReplacementRecon
            Dim totItems As Integer = moDataGrid.Rows.Count

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
            Dim Result As String = HiddenIsPageDirty.Value
            Return Result.Equals("YES")
        End Function

        Function IsDataGBundlesPageDirty() As Boolean
            Dim Result As String = HiddenIsBundlesPageDirty.Value
            Return Result.Equals("YES")
        End Function

        Private Sub SetColumnState(column As Byte, state As Boolean)
            moDataGrid.Columns(column).Visible = state
        End Sub

        Function IsCancellationsFileType(fileName As String) As Boolean
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
                Dim oFile As FileProcessed = New FileProcessed(State.fileProcessedId)
                With oFile
                    moFileNameText.Text = .FileName
                    moCompanyGroupText.Text = State.oReturnType.CompanyGroupDescription
                    moMigrationPathText.Text = State.oReturnType.ReferenceCode
                End With
            Catch ex As Exception
                HandleErrors(ex, ErrController)
            End Try
        End Sub
        Sub PopulateRecordTypeDrop(recordTypeDrop As DropDownList)
            Try
                '  Dim oLangId As Guid = Authentication.LangId
                ' Dim recordTypeList As DataView = LookupListNew.GetRecordTypeLookupList(oLangId)
                'Me.BindListControlToDataView(recordTypeDrop, recordTypeList, , , False) 'RECTYP
                recordTypeDrop.Populate(CommonConfigManager.Current.ListManager.GetList("RECTYP", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions())
            Catch ex As Exception
                ThePage.HandleErrors(ex, ErrController)
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
                If Not SortDirection.Equals(EMPTY) Then
                    dv.Sort = SortDirection
                    HighLightSortColumn(moDataGrid, SortDirection)
                End If
                recCount = dv.Count
                Session("recCount") = recCount
                moDataGrid.PageSize = State.selectedPageSize
                moDataGrid.DataSource = dv
                moDataGrid.DataBind()
                lblRecordCount.Text = recCount & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                ControlMgr.DisableAllGridControlsIfNotEditAuth(Me, moDataGrid)
            Catch ex As Exception
                HandleErrors(ex, ErrController)
            End Try
        End Sub

        Private Sub PopulateBOItem(BestReplacementReconWrkInfo As BestReplacementRecon, oPropertyName As String, oCellPosition As Integer)
            PopulateBOProperty(BestReplacementReconWrkInfo, oPropertyName,
                                            CType(GetSelectedGridControl(moDataGrid, oCellPosition), TextBox))
        End Sub

        Private Sub PopulateBODrop(BestReplacementReconWrkInfo As BestReplacementRecon, oPropertyName As String, oCellPosition As Integer)
            PopulateBOProperty(BestReplacementReconWrkInfo, oPropertyName,
                                CType(GetSelectedGridControl(moDataGrid, oCellPosition), DropDownList), False)
        End Sub
        Sub PopulateCancellationReasonDropDown()
            Try
                Dim oCompanyId As Guid = DealerReconWrk.CompanyId(State.fileProcessedId)
                TempDataView = LookupListNew.GetCancellationReasonDealerFileLookupList(oCompanyId)
                TempDataView.Sort = "DESCRIPTION"
                TempDataView.AddNew()
            Catch ex As Exception
                ThePage.HandleErrors(ex, ErrController)
            End Try
        End Sub

        Private Sub PopulateBOFromForm(BestReplacementReconWrkInfo As BestReplacementRecon)

            PopulateBODrop(BestReplacementReconWrkInfo, RECORD_TYPE_Property, RECORD_TYPE_COL)
            PopulateBOItem(BestReplacementReconWrkInfo, REJECT_CODE_Property, REJECT_CODE_COL)
            PopulateBOItem(BestReplacementReconWrkInfo, REJECT_REASON_Property, REJECT_REASON_COL)
            PopulateBOItem(BestReplacementReconWrkInfo, MANUFACTURER_Property, MANUFACTURER_COL)
            PopulateBOItem(BestReplacementReconWrkInfo, MODEL_Property, MODEL_COL)
            PopulateBOItem(BestReplacementReconWrkInfo, REPLACEMENT_MANUFACTURER_Property, REPLACEMENT_MANUFACTURER_COL)
            PopulateBOItem(BestReplacementReconWrkInfo, REPLACEMENT_MODEL_Property, REPLACEMENT_MODEL_COL)
            PopulateBOItem(BestReplacementReconWrkInfo, PRIORITY_Property, PRIORITY_COL)

            If ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub

        Private Sub PopulateFormItem(oCellPosition As Integer, oPropertyValue As Object)
            PopulateControlFromBOProperty(GetSelectedGridControl(moDataGrid, oCellPosition), oPropertyValue)
        End Sub
#End Region
#Region "GridHandlers"

        Private Sub moDataGrid_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles moDataGrid.PageIndexChanging
            Try
                State.selectedPageIndex = e.NewPageIndex
                If IsDataGPageDirty() Then
                    DisplayMessage(Message.MSG_PAGE_SAVE_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSavePagePromptResponse)
                Else
                    moDataGrid.PageIndex = e.NewPageIndex
                    PopulateGrid()
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrController)
            End Try
        End Sub

        Private Sub moDataGrid_PageSizeChanged(source As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                If IsDataGPageDirty() Then
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.GridPageSize
                    DisplayMessage(Message.MSG_PAGE_SAVE_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSavePagePromptResponse)
                Else
                    'moDataGrid.PageIndex = NewCurrentPageIndex(moDataGrid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                    State.selectedPageSize = CType(cboPageSize.SelectedValue, Integer)
                    PopulateGrid()
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrController)
            End Try
        End Sub

        Protected Sub ItemBound(source As Object, e As GridViewRowEventArgs) Handles moDataGrid.RowDataBound
            Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
            Dim oTextBox As TextBox

            'translate the reject message
            Dim strMsg As String, dr As DataRow, intParamCnt As Integer = 0, strParamList As String = String.Empty
            If dvRow IsNot Nothing Then
                dr = dvRow.Row
                strMsg = dr(BestReplacementReconDAL.COL_TRANSLATED_MSG).ToString.Trim
                If strMsg <> String.Empty Then
                    If dr(BestReplacementReconDAL.COL_MSG_PARAMETER_COUNT) IsNot DBNull.Value Then
                        intParamCnt = CType(dr(BestReplacementReconDAL.COL_MSG_PARAMETER_COUNT), Integer)
                    End If
                    If intParamCnt > 0 Then
                        If dr(BestReplacementReconDAL.COL_REJECT_MSG_PARMS) IsNot DBNull.Value Then
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
                    PopulateControlFromBOProperty(.FindControl("moBestReplacementReconWrkIdLabel"), dvRow(BestReplacementRecon.COL_NAME_BEST_REPLACEMENT_RECON_WRK_ID))
                    Dim oDrop As DropDownList = CType(e.Row.FindControl("moRecordTypeDrop"), DropDownList)
                    oDrop.Attributes.Add("onchange", "setDirty()")
                    PopulateRecordTypeDrop(oDrop)
                    Dim oValue As String = CType(dvRow(BestReplacementRecon.COL_NAME_RECORD_TYPE), String)
                    SetSelectedItemByText(oDrop, oValue)
                    oTextBox = CType(.FindControl("moManufacturerTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(BestReplacementRecon.COL_NAME_MANUFACTURER))
                    oTextBox = CType(.FindControl("moModelTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(BestReplacementRecon.COL_NAME_MODEL))
                    oTextBox = CType(.FindControl("moreplacementmanufacturerTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(BestReplacementRecon.COL_NAME_REPLACEMENT_MANUFACTURER))
                    oTextBox = CType(.FindControl("moreplacementmodelTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(BestReplacementRecon.COL_NAME_REPLACEMENT_MODEL))
                    oTextBox = CType(.FindControl("moPriorityTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(BestReplacementRecon.COL_NAME_PRIORITY))
                    oTextBox = CType(.FindControl("RejectReasonTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(BestReplacementRecon.COL_NAME_REJECT_REASON))
                    oTextBox = CType(.FindControl("moRejectCode"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(BestReplacementRecon.COL_NAME_REJECT_CODE))
                End With
            End If
            BaseItemBound(source, e)
        End Sub

        Private Sub Grid_SortCommand(source As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles moDataGrid.Sorting
            Try
                Dim spaceIndex As Integer = SortDirection.LastIndexOf(" ")
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
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, ErrController)
            End Try
        End Sub
        Protected Overloads Sub ItemCreated(sender As Object, e As DataGridItemEventArgs)
            BaseItemCreated(sender, e)
        End Sub

        Protected Overloads Sub ItemCreated(sender As Object, e As GridViewRowEventArgs)
            BaseItemCreated(sender, e)
        End Sub
        Protected Sub BindBoPropertiesToGridHeaders(BestReplacementconWrkInfo As BestReplacementRecon)
            BindBOPropertyToGridHeader(BestReplacementconWrkInfo, "RecordType", moDataGrid.Columns(RECORD_TYPE_COL))
            BindBOPropertyToGridHeader(BestReplacementconWrkInfo, "Manufacturer", moDataGrid.Columns(MANUFACTURER_COL))
            BindBOPropertyToGridHeader(BestReplacementconWrkInfo, "Model", moDataGrid.Columns(MODEL_COL))
            BindBOPropertyToGridHeader(BestReplacementconWrkInfo, "ReplacementManufacturer", moDataGrid.Columns(REPLACEMENT_MANUFACTURER_COL))
            BindBOPropertyToGridHeader(BestReplacementconWrkInfo, "ReplacementModel", moDataGrid.Columns(REPLACEMENT_MODEL_COL))
            BindBOPropertyToGridHeader(BestReplacementconWrkInfo, "Priority", moDataGrid.Columns(PRIORITY_COL))
            BindBOPropertyToGridHeader(BestReplacementconWrkInfo, "RejectReason", moDataGrid.Columns(REJECT_REASON_COL))
            BindBOPropertyToGridHeader(BestReplacementconWrkInfo, "RejectCode", moDataGrid.Columns(REJECT_CODE_COL))
            ClearGridViewHeadersAndLabelsErrSign()
        End Sub
        Private Sub SetFocusOnEditableFieldInGrid(grid As DataGrid, cellPosition As Integer, controlName As String, itemIndex As Integer)
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

        Private Sub btnBack_Click(sender As System.Object, e As System.EventArgs) Handles btnBack.Click
            Try
                If IsDataGPageDirty() Or (State.BundlesHashTable IsNot Nothing AndAlso State.BundlesHashTable.Count > 0) Then
                    DisplayMessage(Message.MSG_PAGE_SAVE_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSavePagePromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    State.oReturnType.LastOperation = ElitaPlusPage.DetailPageCommand.Back
                    ReturnToCallingPage(State.oReturnType)
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, ErrController)
            End Try
        End Sub

        Private Sub SaveButton_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles SaveButton_WRITE.Click
            Try
                SavePage()
                DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)
                HiddenIsPageDirty.Value = EMPTY
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, ErrController)
            End Try
        End Sub

        Private Sub btnUndo_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnUndo_WRITE.Click
            Try
                State.BundlesHashTable = Nothing
                PopulateGrid()
                HiddenIsPageDirty.Value = EMPTY
            Catch ex As Exception
                HandleErrors(ex, ErrController)
            End Try
        End Sub
#End Region
    End Class

End Namespace