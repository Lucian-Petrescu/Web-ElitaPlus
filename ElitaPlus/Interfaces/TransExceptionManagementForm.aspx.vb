Imports Assurant.ElitaPlus.ElitaPlusWebApp.Reports
Imports Assurant.ElitaPlus.Common
Imports Microsoft.VisualBasic
Imports Assurant.ElitaPlus.ElitaPlusWebApp.ElitaPlusPage

Imports Assurant.ElitaPlus.Common.MiscUtil
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports System.IO
Imports System.Collections.Generic


Partial Public Class TransExceptionManagementForm
    Inherits ElitaPlusSearchPage

#Region "Constants"
    Public Const URL As String = "Tables/TransExceptionManagementForm.aspx"
    Public Const PAGETITLE As String = "TRANSACTION_EXCEPTION_MANAGEMENT"
    Public Const PAGETAB As String = "INTERFACES"
    Public Const GVS_TRANSACTION_UPDATE As Integer = 4
    Public Const ELITA_TRANSACTION_UPDATE As Integer = 7

    Public Const ERR_MSG_FORM_CATEGORY_IN_USE As String = "FORM_CATEGORY_IN_USE"

    Private Const GRID_NO_SELECTEDITEM_INX As Integer = -1

    Private Const GRID_COL_EDIT_IDX As Integer = 0
    Private Const GRID_COL_CHECKBOX_IDX As Integer = 1
    Private Const GRID_COL_SHOW_CHECKBOX_IDX As Integer = 2
    Private Const GRID_COL_TRANS_ID_IDX As Integer = 3
    Private Const GRID_COL_CLAIM_NUM_IDX As Integer = 4
    Private Const GRID_COL_TRANS_DATE_IDX As Integer = 5
    Private Const GRID_COL_CUST_NAME_IDX As Integer = 6
    Private Const GRID_COL_SVC_NAME_IDX As Integer = 7
    Private Const GRID_COL_AUTH_NUM_IDX As Integer = 8
    Private Const GRID_COL_ERR_CODE_IDX As Integer = 9
    Private Const GRID_COL_ORIGINATOR_IDX As Integer = 10
    Private Const GRID_COL_FUNCTION_TYPE_IDX As Integer = 11
    Private Const GRID_CTRL_NAME_TRANS_ID As String = "lblTransactionLogHeaderID"
    Private Const GRID_CTRL_NAME_FUNCTION_TYPE As String = "lblFunctionType"
    Private Const GRID_CTRL_NAME_EDIT_BUTTON As String = "EditButton_WRITE"
    Private Const GRID_CTRL_NAME_CHECKBOX As String = "CheckBoxItemSel"
    Private Const GRID_CTRL_NAME_SHOW_CHECKBOX As String = "lblShowCheckboxID"
    Private Const GRID_CTRL_NAME_ERROR_CODE As String = "lblErrCode"
    Public Const NO_TRANSMISSION_FOUND As String = "NO_TRANSMISSION_FOUND"
    Public Const EMPTY_GRID_ID As String = "00000000000000000000000000000000"

    Private _TabList As DataView
    Private IsReturnFromChild As Boolean = False
    Protected lastSuccessfulElitaMessage As String = String.Empty
    Protected lastSuccessfulGVSMessage As String = String.Empty

    Protected checkValueArray() As String

#End Region

#Region "Page State"
    ' This class keeps the current state for the search page.
    Class MyState
        Public MyBO As TransactionLogHeader
        Public TransactionLogHeaderId As Guid
        Public PageIndex As Integer = 0
        Public PageSize As Integer = DEFAULT_PAGE_SIZE
        Public HasDataChanged As Boolean
        Public searchClick As Boolean = False
        Public IsGridAddNew As Boolean = False
        Public IsGridVisible As Boolean
        Public GridEditIndex As Integer = NO_ITEM_SELECTED_INDEX
        Public searchDV As TransactionLogHeader.ExceptionSearchDV
        Public SortExpression As String = TransactionLogHeader.ExceptionSearchDV.COL_TRANSACTION_DATE & " DESC"
        Public searchClaimNumber As String = ""
        Public searchAuthNumber As String = ""
        Public searchFrom As String = ""
        Public searchTo As String = ""
        Public searchErrorCode As String = ""
        Public ErrorCode As String = ""
        Public searchServiceCenterCode As String = ""
        Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
        Public cmdProcessRecord As String = String.Empty
    End Class

    Public Sub New()
        MyBase.New(New MyState)
    End Sub

    Protected Shadows ReadOnly Property State() As MyState
        Get
            Return CType(MyBase.State, MyState)
        End Get
    End Property

    Public ReadOnly Property IsGridInEditMode() As Boolean
        Get
            Return Grid.EditIndex > NO_ITEM_SELECTED_INDEX
        End Get
    End Property

    Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles MyBase.PageReturn
        Try
            MenuEnabled = True
            If ReturnFromUrl = Interfaces.TransExceptionDetail.RETURN_URL Then

                Dim retObj As Interfaces.TransExceptionDetail.ReturnType = CType(ReturnPar, Interfaces.TransExceptionDetail.ReturnType)
                State.HasDataChanged = retObj.HasDataChanged
                If retObj IsNot Nothing AndAlso retObj.HasDataChanged Then
                    State.searchDV = Nothing
                End If
                State.IsGridVisible = True
                Select Case retObj.LastOperation
                    Case ElitaPlusPage.DetailPageCommand.Back
                        If retObj IsNot Nothing Then
                            State.TransactionLogHeaderId = New Guid(GuidControl.HexToByteArray(retObj.EditingBo))
                        End If
                End Select
            Else
                Dim retObj As ReturnType = CType(ReturnPar, ReturnType)
                State.HasDataChanged = retObj.HasDataChanged
                If retObj IsNot Nothing AndAlso retObj.HasDataChanged Then
                    State.searchDV = Nothing
                End If
                State.IsGridVisible = True
                Select Case retObj.LastOperation
                    Case ElitaPlusPage.DetailPageCommand.Back
                        If retObj IsNot Nothing Then
                            State.searchClaimNumber = retObj.searchClaimNumber
                            State.searchAuthNumber = retObj.searchAuthNumber
                            State.searchServiceCenterCode = retObj.searchServiceCenterCode
                            State.searchFrom = retObj.searchFrom
                            State.searchTo = retObj.searchTo
                            State.searchErrorCode = retObj.searchErrorCode
                            State.PageIndex = retObj.page_index
                        End If
                End Select

            End If

        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Public Class ReturnType
        Public LastOperation As ElitaPlusPage.DetailPageCommand
        Public searchClaimNumber As String = ""
        Public searchAuthNumber As String = ""
        Public searchFrom As String = ""
        Public searchTo As String = ""
        Public searchErrorCode As String = ""
        Public searchServiceCenterCode As String = ""
        Public ostate As New MyState
        Public page_index As Integer
        Public HasDataChanged As Boolean = False      
        Public Sub New(LastOp As ElitaPlusPage.DetailPageCommand, returnPar As Object)
            LastOperation = LastOp

            'DirectCast(returnPar, Assurant.ElitaPlus.ElitaPlusWebApp.Reports.ExceptionsEnhancementReportForm.MyState)
            searchClaimNumber = CType(returnPar, ExceptionsEnhancementReportForm.MyState).CLAIM_NUMBER.ToString
            searchAuthNumber = CType(returnPar, ExceptionsEnhancementReportForm.MyState).AUTH_NUMBER.ToString
            searchServiceCenterCode = CType(returnPar, ExceptionsEnhancementReportForm.MyState).SVC_CODE.ToString
            searchFrom = CType(returnPar, ExceptionsEnhancementReportForm.MyState).TRANS_DATE_FROM.ToString
            searchTo = CType(returnPar, ExceptionsEnhancementReportForm.MyState).TRANS_DATE_TO.ToString
            searchErrorCode = CType(returnPar, ExceptionsEnhancementReportForm.MyState).ERROR_CODE.ToString
            page_index = CType(returnPar, ExceptionsEnhancementReportForm.MyState).Page_Index

        End Sub
    End Class
#End Region

#Region "Page Events"
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        ErrControllerMaster.Clear_Hide()
        Form.DefaultButton = btnSearch.UniqueID
        Try
            GetLastSuccessfulDateTime()
            If Not IsPostBack Then
                SetFormTitle(PAGETITLE)
                SetFormTab(PAGETAB)
                AddCalendar(imgBtnFrom, txtFrom)
                AddCalendar(imgBtnTo, txtTo)
                SetFocus(txtClaimNumber)
                'populateSearchControls()
                TranslateGridHeader(Grid)
                If State.IsGridVisible Then
                    If Not (State.PageSize = DEFAULT_PAGE_SIZE) Then
                        cboPageSize.SelectedValue = CType(State.PageSize, String)
                        Grid.PageSize = State.PageSize
                    End If
                End If

                If IsReturnFromChild Then
                    With State
                        txtClaimNumber.Text = .searchClaimNumber
                        txtAuthNumber.Text = .searchAuthNumber
                        txtFrom.Text = .searchFrom
                        txtTo.Text = .searchTo
                        txtErrorCode.Text = .searchErrorCode
                        txtServiceCenterName.Text = .searchServiceCenterCode
                        .searchDV = Nothing
                        PopulateGrid()
                    End With

                    btnExportResults.Enabled = True
                Else
                    btnExportResults.Enabled = False
                End If
                ' BindData()
            Else
                btnExportResults.Enabled = True
            End If

            CheckIfComingFromSaveConfirm()

            If (checkRecords IsNot Nothing AndAlso checkRecords.Value IsNot Nothing AndAlso checkRecords.Value.Length > 0) Then
                btnProcessRecords.Enabled = True
            Else
                btnProcessRecords.Enabled = False
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
        ShowMissingTranslations(ErrControllerMaster)
    End Sub

    'Private Sub BindData()
    '    Dim dirInfo As New DirectoryInfo(Request.PhysicalApplicationPath)

    '    Grid.DataSource = dirInfo.GetFiles()
    '    Grid.DataBind()
    'End Sub
    Protected Sub GetLastSuccessfulDateTime()

        lastSuccessfulElitaMessage = String.Format("{0} - {1}: ", TranslationBase.TranslateLabelOrMessage("ELITAGVS", ElitaPlusIdentity.Current.ActiveUser.LanguageId), TranslationBase.TranslateLabelOrMessage("LAST_SUCCESSFUL_TRANSMISSION_TIME", ElitaPlusIdentity.Current.ActiveUser.LanguageId))
        lastSuccessfulGVSMessage = String.Format("{0} - {1}: ", TranslationBase.TranslateLabelOrMessage("GVSELITA", ElitaPlusIdentity.Current.ActiveUser.LanguageId), TranslationBase.TranslateLabelOrMessage("LAST_SUCCESSFUL_TRANSMISSION_TIME", ElitaPlusIdentity.Current.ActiveUser.LanguageId))

        Dim transDateElita, transDateGVS As Date
        Dim dv As DataView = TransactionLogHeader.GetLastSuccessfulTransmissionTimeByType
        If dv IsNot Nothing Then

        'Check Returned records for valid dates
            For Each dItem As DataRowView In dv
                If dItem(DALObjects.TransactionLogHeaderDAL.COL_NAME_TRANSACTION_STATUS_SYSTEM) IsNot Nothing AndAlso dItem(DALObjects.TransactionLogHeaderDAL.COL_NAME_TRANSACTION_STATUS_SYSTEM).ToString = TransactionLogHeader.SYSTEM_TYPE_ELITA Then
                    If dItem(DALObjects.TransactionLogHeaderDAL.COL_NAME_TRANSACTION_STATUS_DATE) IsNot Nothing AndAlso DateHelper.IsDate(dItem(DALObjects.TransactionLogHeaderDAL.COL_NAME_TRANSACTION_STATUS_DATE)) Then
                        transDateElita = DateHelper.GetDateValue(dItem(DALObjects.TransactionLogHeaderDAL.COL_NAME_TRANSACTION_STATUS_DATE).ToString())
                    End If
                ElseIf dItem(DALObjects.TransactionLogHeaderDAL.COL_NAME_TRANSACTION_STATUS_SYSTEM) IsNot Nothing AndAlso dItem(DALObjects.TransactionLogHeaderDAL.COL_NAME_TRANSACTION_STATUS_SYSTEM).ToString = TransactionLogHeader.SYSTEM_TYPE_GVS Then
                    If dItem(DALObjects.TransactionLogHeaderDAL.COL_NAME_TRANSACTION_STATUS_DATE) IsNot Nothing AndAlso DateHelper.IsDate(dItem(DALObjects.TransactionLogHeaderDAL.COL_NAME_TRANSACTION_STATUS_DATE)) Then
                        transDateGVS = DateHelper.GetDateValue(dItem(DALObjects.TransactionLogHeaderDAL.COL_NAME_TRANSACTION_STATUS_DATE).ToString())
                    End If
                End If
            Next
        End If

        If Not DateHelper.IsDate(transDateElita) OrElse transDateElita = DateTime.MinValue Then
            lastSuccessfulElitaMessage &= TranslationBase.TranslateLabelOrMessage("GREATERTHAN5", ElitaPlusIdentity.Current.ActiveUser.LanguageId)
        Else
            lastSuccessfulElitaMessage &= transDateElita.ToString
        End If
        If Not DateHelper.IsDate(transDateGVS) OrElse transDateGVS = DateTime.MinValue Then
            lastSuccessfulGVSMessage &= TranslationBase.TranslateLabelOrMessage("GREATERTHAN5", ElitaPlusIdentity.Current.ActiveUser.LanguageId)
        Else
            lastSuccessfulGVSMessage &= transDateGVS.ToString
        End If
    End Sub

    Private Sub TransExceptionManagementForm_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles Me.PageReturn
        Try
            MenuEnabled = True
            IsReturnFromChild = True
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub
#End Region

#Region "Grid Handler"

    Private Sub Grid_PageIndexChanged(sender As Object, e As System.EventArgs) Handles Grid.PageIndexChanged
        Try
            State.PageIndex = Grid.PageIndex
            State.TransactionLogHeaderId = Guid.Empty
            PopulateGrid()

        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Private Sub Grid_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
        Try
            Grid.PageIndex = e.NewPageIndex
            State.PageIndex = Grid.PageIndex
            'BindData()
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Private Sub Grid_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Grid.RowCommand
        Try
            If e.CommandName = "SelectAction" Then
                Dim lblCtrl As Label
                Dim lblErrorCodeCtrl As Label
                Dim chkBox As CheckBox
                Dim reprocess As String = "N"
                Dim row As GridViewRow = CType(CType(e.CommandSource, Control).Parent.Parent, GridViewRow)
                Dim RowInd As Integer = row.RowIndex
                lblCtrl = CType(Grid.Rows(RowInd).Cells(GRID_COL_TRANS_ID_IDX).FindControl(GRID_CTRL_NAME_TRANS_ID), Label)
                chkBox = CType(Grid.Rows(RowInd).Cells(GRID_COL_TRANS_ID_IDX).FindControl(GRID_CTRL_NAME_CHECKBOX), CheckBox)
                lblErrorCodeCtrl = CType(Grid.Rows(RowInd).Cells(GRID_COL_ERR_CODE_IDX).FindControl(GRID_CTRL_NAME_ERROR_CODE), Label)
                If chkBox IsNot Nothing AndAlso chkBox.Visible = True Then
                    reprocess = "Y"
                End If
                State.TransactionLogHeaderId = New Guid(lblCtrl.Text)
                State.ErrorCode = lblErrorCodeCtrl.Text
                Dim params As ArrayList = New ArrayList
                params.Add(State.TransactionLogHeaderId)
                params.Add(reprocess)
                params.Add(State.ErrorCode)
                callPage(Interfaces.TransExceptionDetail.URL, params)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Private Sub Grid_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Private Sub Grid_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
        Try
            BaseItemBound(sender, e)



            If e.Row.RowType = ListItemType.Item OrElse e.Row.RowType = ListItemType.AlternatingItem OrElse e.Row.RowType = ListItemType.EditItem Then
                Dim drv As DataRowView = CType(e.Row.DataItem, DataRowView)
                Dim transIdStr As String = String.Empty

                'BuildCheckBoxIDsArray()

                'If Not e.Row.Cells(Me.GRID_COL_ERR_CODE_IDX).FindControl(Me.GRID_CTRL_NAME_ERROR_CODE) Is Nothing Then
                '    Dim errCode As String
                '    Dim errTrans As String = String.Empty
                '    errCode = CType(e.Row.Cells(Me.GRID_COL_ERR_CODE_IDX).FindControl(Me.GRID_CTRL_NAME_ERROR_CODE), Label).Text
                '    Try
                '        errTrans = TranslationBase.TranslateLabelOrMessage(errCode, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                '        If Not errCode Is Nothing AndAlso Not errTrans Is Nothing AndAlso errCode.ToUpper = errTrans.ToUpper Then
                '            errTrans = errCode
                '        End If

                '        If Not errTrans Is Nothing AndAlso Not errTrans.Equals(String.Empty) Then
                '            Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_ERR_CODE_IDX).FindControl(Me.GRID_CTRL_NAME_ERROR_CODE), errTrans)
                '        End If
                '    Catch ex As Exception

                '    End Try
                'End If

                If e.Row.Cells(GRID_COL_EDIT_IDX).FindControl(GRID_CTRL_NAME_EDIT_BUTTON) IsNot Nothing Then
                    If e.Row.Cells(GRID_COL_FUNCTION_TYPE_IDX).FindControl(GRID_CTRL_NAME_FUNCTION_TYPE) IsNot Nothing Then
                        Try

                            Dim functionType As Integer = CType(CType(e.Row.Cells(GRID_COL_FUNCTION_TYPE_IDX).FindControl(GRID_CTRL_NAME_FUNCTION_TYPE), Label).Text, Integer)
                            'Dim transId As Guid = GetGuidFromString(CType(e.Row.Cells(Me.GRID_COL_TRANS_ID_IDX).FindControl(Me.GRID_CTRL_NAME_TRANS_ID), Label).Text)
                            'transIdStr = GuidControl.GuidToHexString(transId)

                            'If Guid.Empty.Equals(transId) Then
                            If functionType.Equals(GVS_TRANSACTION_UPDATE) OrElse functionType.Equals(ELITA_TRANSACTION_UPDATE) Then
                                ControlMgr.SetVisibleControl(Me, e.Row.Cells(GRID_COL_EDIT_IDX).FindControl(GRID_CTRL_NAME_EDIT_BUTTON), False)
                            End If
                        Catch ex As Exception

                        End Try

                    End If
                End If

                If e.Row.Cells(GRID_COL_CHECKBOX_IDX).FindControl(GRID_CTRL_NAME_CHECKBOX) IsNot Nothing Then
                    If e.Row.Cells(GRID_COL_SHOW_CHECKBOX_IDX).FindControl(GRID_CTRL_NAME_SHOW_CHECKBOX) IsNot Nothing Then
                        Dim showCheckbox As String = CType(e.Row.Cells(GRID_COL_SHOW_CHECKBOX_IDX).FindControl(GRID_CTRL_NAME_SHOW_CHECKBOX), Label).Text

                        If showCheckbox IsNot Nothing AndAlso showCheckbox = "Y" Then
                            Dim checkBox As CheckBox = New CheckBox
                            checkBox = CType(e.Row.Cells(GRID_COL_CHECKBOX_IDX).FindControl(GRID_CTRL_NAME_CHECKBOX), CheckBox)
                            checkBox.Attributes.Add("onclick", "CheckboxAction('" & transIdStr & "','" & checkBox.ClientID & "','" & btnProcessRecords.ClientID & "','" & checkRecords.ClientID & "') ; ChangeHeaderAsNeeded();")
                            ControlMgr.SetVisibleControl(Me, checkBox, True)
                        Else
                            ControlMgr.SetVisibleControl(Me, e.Row.Cells(GRID_COL_CHECKBOX_IDX).FindControl(GRID_CTRL_NAME_CHECKBOX), False)
                        End If
                    End If
                End If


            End If

        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Private Sub BuildCheckBoxIDsArray()
        'Each time the data is bound to the grid we need to build up the CheckBoxIDs array

        'Get the header CheckBox
        Dim cbHeader As CheckBox = CType(Grid.HeaderRow.FindControl("HeaderLevelCheckBox"), CheckBox)

        'Run the ChangeCheckBoxState client-side function whenever the
        'header checkbox is checked/unchecked
        cbHeader.Attributes("onclick") = "ChangeAllCheckBoxStates(this.checked, '" & btnProcessRecords.ClientID & "');"

        'Add the CheckBox's ID to the client-side CheckBoxIDs array
        Dim ArrayValues As New List(Of String)
        ArrayValues.Add(String.Concat("'", cbHeader.ClientID, "'"))

        For Each gvr As GridViewRow In Grid.Rows
            'Get a programmatic reference to the CheckBox control
            Dim cb As CheckBox = CType(gvr.FindControl("CheckBoxItemSel"), CheckBox)

            'If the checkbox is unchecked, ensure that the Header CheckBox is unchecked
            'cb.Attributes("onclick") = "ChangeHeaderAsNeeded();"

            'Add the CheckBox's ID to the client-side CheckBoxIDs array
            If cb IsNot Nothing Then ArrayValues.Add(String.Concat("'", cb.ClientID, "'"))
        Next

        'Output the array to the Literal control (CheckBoxIDsArray)
        CheckBoxIDsArray.Text = "<script type=""text/javascript"">" & vbCrLf & _
                                "<!--" & vbCrLf & _
                                String.Concat("var CheckBoxIDs =  new Array(", String.Join(",", ArrayValues.ToArray()), ");") & vbCrLf & _
                                "// -->" & vbCrLf & _
                                "</script>"
    End Sub

    Private Sub Grid_Sorting(sender As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting
        Try
            Dim strSort As String = e.SortExpression
            With State
                If .SortExpression.StartsWith(e.SortExpression) Then
                    If Not .SortExpression.EndsWith(" DESC") Then
                        strSort = strSort & " DESC"
                    End If
                End If
                .SortExpression = strSort
            End With
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Protected Sub cboPageSize_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            State.PageSize = CType(cboPageSize.SelectedValue, Integer)
            State.PageIndex = NewCurrentPageIndex(Grid, State.searchDV.Count, State.PageSize)
            Grid.PageIndex = State.PageIndex
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub
#End Region

#Region "Helper functions"

    Public Sub PopulateGrid()
        Try
            With State
                If (.searchDV Is Nothing) Then

                    Dim fromDate As Date = Date.MinValue
                    Dim toDate As Date = Date.MinValue
                    If DateHelper.IsDate(.searchFrom) Then
                        fromDate = DateHelper.GetDateValue(.searchFrom)
                    End If
                    If DateHelper.IsDate(.searchTo) Then
                        toDate = DateHelper.GetDateValue(.searchTo)
                    End If

                    .searchDV = TransactionLogHeader.GetExceptionList(.searchClaimNumber, .searchAuthNumber, .searchServiceCenterCode, fromDate, toDate, .searchErrorCode)

                    If .searchClick Then
                        ValidSearchResultCount(.searchDV.Count, True)
                        .searchClick = False
                    End If
                End If

                If .IsGridAddNew Then
                    .PageIndex = Grid.PageIndex
                Else
                    .searchDV.Sort = .SortExpression
                End If
            End With

            Grid.PageSize = State.PageSize
            If State.searchDV.Count = 0 Then
                Dim dv As TransactionLogHeader.ExceptionSearchDV = State.searchDV.AddNewRowToEmptyDV()
                SetPageAndSelectedIndexFromGuid(dv, State.TransactionLogHeaderId, Grid, State.PageIndex, (IsGridInEditMode OrElse State.IsGridAddNew))
                Grid.DataSource = dv
            Else
                SetPageAndSelectedIndexFromGuid(State.searchDV, State.TransactionLogHeaderId, Grid, State.PageIndex, (IsGridInEditMode OrElse State.IsGridAddNew))
                Grid.DataSource = State.searchDV
            End If

            State.PageIndex = Grid.PageIndex
            Grid.DataBind()

            HighLightGridViewSortColumnoOverRide(Grid, State.SortExpression)
            ControlMgr.SetVisibleControl(Me, Grid, True)
            ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)

            If Grid.Visible Then
                lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If
            If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True

            If State.searchDV.Count = 0 Then
                For Each gvRow As GridViewRow In Grid.Rows
                    gvRow.Visible = False
                    gvRow.Controls.Clear()
                Next
            End If

            BuildCheckBoxIDsArray()

        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Private Sub HighLightGridViewSortColumnoOverRide(grid As GridView, sortExp As String)
        If grid.HeaderRow IsNot Nothing Then
            Dim img As New System.Web.UI.WebControls.Image()
            img.CssClass = "SORTARROW"
            If sortExp.ToUpper.EndsWith("DESC") Then
                img.ImageUrl = DOWN_ARROW_IMG_SOURCE
            Else
                img.ImageUrl = UP_ARROW_IMG_SOURCE
            End If
            Dim lnk As LinkButton
            Dim cellCount As Integer = 0
            For Each tc As TableCell In grid.HeaderRow.Cells

                If tc.HasControls AndAlso cellCount > 2 Then

                    lnk = CType(tc.Controls(0), LinkButton)
                    If lnk IsNot Nothing Then
                        If sortExp.ToUpper.EndsWith("DESC") OrElse sortExp.ToUpper.EndsWith("ASC") Then
                            If sortExp.ToUpper.StartsWith(lnk.CommandArgument.ToUpper & " ") Then
                                tc.Controls.Add(img)
                            End If
                        Else
                            If sortExp.ToUpper.Trim = lnk.CommandArgument.ToUpper.Trim Then
                                tc.Controls.Add(img)
                            End If
                        End If

                    End If
                End If
                cellCount = cellCount + 1
            Next
        End If
    End Sub
    Private Sub SetControlState()
        If (IsGridInEditMode) Then
            ControlMgr.SetEnableControl(Me, btnSearch, False)
            ControlMgr.SetEnableControl(Me, btnClearSearch, False)
            MenuEnabled = False
            If (cboPageSize.Enabled) Then
                ControlMgr.SetEnableControl(Me, cboPageSize, False)
            End If
        Else
            ControlMgr.SetEnableControl(Me, btnSearch, True)
            ControlMgr.SetEnableControl(Me, btnClearSearch, True)
            MenuEnabled = True
            If Not (cboPageSize.Enabled) Then
                ControlMgr.SetEnableControl(Me, cboPageSize, True)
            End If
        End If
        cboPageSize.Visible = Grid.Visible
        lblRecordCount.Visible = Grid.Visible
    End Sub

    Private Function PopulateBOFromForm(ByRef errMsg As Collections.Generic.List(Of String)) As Boolean
        Dim blnSuccess As Boolean = True
        Dim ind As Integer = Grid.EditIndex

        Return blnSuccess
    End Function

    Private Function GetRowIndexFromSearchDVByID(MSGCodeID As Guid) As Integer
        Dim rowind As Integer = NO_ITEM_SELECTED_INDEX
        With State
            If .searchDV IsNot Nothing Then
                rowind = FindSelectedRowIndexFromGuid(.searchDV, MSGCodeID)
            End If
        End With
        Return rowind
    End Function

#End Region

#Region "Button click handlers"

    Protected Sub btnClearSearch_Click(sender As Object, e As EventArgs) Handles btnClearSearch.Click
        Try
            txtClaimNumber.Text = String.Empty
            txtAuthNumber.Text = String.Empty
            txtFrom.Text = String.Empty
            txtTo.Text = String.Empty
            txtServiceCenterName.Text = String.Empty
            txtErrorCode.Text = String.Empty
            Grid.EditIndex = NO_ITEM_SELECTED_INDEX
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            With State
                .PageIndex = 0
                .IsGridVisible = True
                .searchDV = Nothing
                .HasDataChanged = False
                .searchClick = True
                .searchClaimNumber = txtClaimNumber.Text.Trim
                .searchAuthNumber = txtAuthNumber.Text.Trim
                .searchFrom = txtFrom.Text.Trim
                .searchTo = txtTo.Text.Trim
                .searchErrorCode = txtErrorCode.Text.Trim
                .searchServiceCenterCode = txtServiceCenterName.Text.Trim
            End With

            PopulateGrid()
            checkRecords.Value = ""
            btnProcessRecords.Enabled = False
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Private Sub btnProcessRecords_Click(sender As System.Object, e As System.EventArgs) Handles btnProcessRecords.Click
        Try
            'Resend confirmation
            DisplayMessage(Message.MSG_PROMPT_FOR_PROCESS_RECORDS, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
            State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Accept
            State.cmdProcessRecord = DALObjects.TransactionLogHeaderDAL.CMD_RESEND
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Public Sub btnExportResults_Click(sender As System.Object, e As System.EventArgs) Handles btnExportResults.Click
        Try
            'Dim sessionStr As String = String.Empty

            'If Not Me.State.searchClaimNumber Is Nothing Then
            '    sessionStr = Me.State.searchClaimNumber
            'End If
            'sessionStr = sessionStr & "|"

            'If Not Me.State.searchAuthNumber Is Nothing Then
            '    sessionStr = sessionStr & Me.State.searchAuthNumber
            'End If
            'sessionStr = sessionStr & "|"

            'If Not Me.State.searchServiceCenterCode Is Nothing Then
            '    sessionStr = sessionStr & Me.State.searchServiceCenterCode
            'End If
            'sessionStr = sessionStr & "|"

            'If Not Me.State.searchErrorCode Is Nothing Then
            '    sessionStr = sessionStr & Me.State.searchErrorCode
            'End If
            'sessionStr = sessionStr & "|"

            'If Not Me.State.searchFrom Is Nothing AndAlso Me.State.searchFrom.Length > 0 Then
            '    sessionStr = sessionStr & CType(Me.State.searchFrom, Date).ToString("MM/dd/yyyy")
            'End If
            'sessionStr = sessionStr & "|"

            'If Not Me.State.searchTo Is Nothing AndAlso Me.State.searchTo.Length > 0 Then
            '    sessionStr = sessionStr & CType(Me.State.searchTo, Date).ToString("MM/dd/yyyy")
            'End If

            'Session("GVS_PARAM") = sessionStr
            'Response.Redirect(ELPWebConstants.APPLICATION_PATH & "/Reports/ExceptionsEnhancementReportForm.aspx")

            With State
                .searchClaimNumber = txtClaimNumber.Text.Trim
                .searchAuthNumber = txtAuthNumber.Text.Trim
                .searchFrom = txtFrom.Text.Trim
                .searchTo = txtTo.Text.Trim
                .searchErrorCode = txtErrorCode.Text.Trim
                .searchServiceCenterCode = txtServiceCenterName.Text.Trim
                .PageIndex = Grid.PageIndex
            End With
            callPage(ExceptionsEnhancementReportForm.URL, State)

        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub


    Protected Sub CheckIfComingFromSaveConfirm()
        Dim confResponse As String = HiddenSaveChangesPromptResponse.Value
        Try
            If confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_YES Then
                Select Case State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Accept
                        'Process transaction
                        Dim checkValues As String = String.Empty
                        Dim i As Integer
                        checkValueArray = checkRecords.Value.Split(":"c)

                        For i = 0 To checkValueArray.Length - 1
                            If (checkValueArray(i) IsNot Nothing AndAlso checkValueArray(i) <> "") Then
                                checkValues = checkValueArray(i).ToString & ":" & checkValues
                            End If
                        Next
                        checkRecords.Value = GetCheckedItemsValues()
                        ProcessRecords()
                        checkRecords.Value = ""
                        State.searchDV = Nothing
                        PopulateGrid()
                End Select
            ElseIf confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_NO Then
                Select Case State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Accept
                        btnProcessRecords.Enabled = True
                        btnExportResults.Enabled = True
                End Select
            End If

            'Clean after consuming the action
            State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
            HiddenSaveChangesPromptResponse.Value = ""
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Private Function GetCheckedItemsValues() As String
        Dim checkedValues As String = String.Empty
        For Each gvrow As GridViewRow In Grid.Rows
            Dim CheckBox1 As CheckBox = DirectCast(gvrow.FindControl(GRID_CTRL_NAME_CHECKBOX), CheckBox)

            If CheckBox1.Checked Then
                Dim lblTransID As Label = DirectCast(gvrow.FindControl(GRID_CTRL_NAME_TRANS_ID), Label)
                Dim transId As Guid = GetGuidFromString(lblTransID.Text)
                'transIdStr = GuidControl.GuidToHexString(transId)

                checkedValues += GuidControl.GuidToHexString(transId) & ":"
            End If

        Next
        Return checkedValues
    End Function
    Protected Function ProcessRecords() As Boolean
        Try
            Dim outputParameters() As DALObjects.DBHelper.DBHelperParameter
            outputParameters = TransactionLogHeader.ProcessRecords(State.cmdProcessRecord, checkRecords.Value)

            If CType(outputParameters(0).Value, Integer) = 0 Then
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.OK
                HiddenSaveChangesPromptResponse.Value = MSG_BTN_OK
                DisplayMessageWithSubmit(Message.SAVE_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)
            Else
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.OK
                HiddenSaveChangesPromptResponse.Value = MSG_BTN_OK
                DisplayMessageWithSubmit(CType(outputParameters(1).Value, String), "", MSG_BTN_OK, MSG_TYPE_INFO)
            End If

            PopulateGrid()
            Return True
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
            Return False
        End Try
    End Function


#End Region

End Class