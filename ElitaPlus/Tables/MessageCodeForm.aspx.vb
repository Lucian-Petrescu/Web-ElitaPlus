﻿Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Partial Public Class MessageCodeForm
    Inherits ElitaPlusSearchPage
#Region "Constants"
    Public Const URL As String = "Tables/InvoiceControlListForm.aspx"
    Public Const PAGETITLE As String = "MSG_CODE"
    Public Const PAGETAB As String = "ADMIN"

    Private Const GRID_NO_SELECTEDITEM_INX As Integer = -1

    Private Const GRID_COL_EDIT_IDX As Integer = 0
    Private Const GRID_COL_DELETE_IDX As Integer = 1
    Private Const GRID_COL_MSG_CODE_ID_IDX As Integer = 2
    Private Const GRID_COL_MSG_TYPE_IDX As Integer = 2
    Private Const GRID_COL_MSG_CODE_IDX As Integer = 3
    Private Const GRID_COL_UI_PROG_CODE_IDX As Integer = 4
    Private Const GRID_COL_MSG_PARAM_COUNT_IDX As Integer = 5
    Private Const GRID_COL_MSG_TEXT_IDX As Integer = 6

    Private Const GRID_CTRL_NAME_MSG_CODE_ID As String = "lblMSGCodeID"
    Private Const GRID_CTRL_NAME_MSG_TYPE As String = "ddlMSGType"
    Private Const GRID_CTRL_NAME_MSG_TYPE_Label As String = "lblMSGType"
    Private Const GRID_CTRL_NAME_MSG_CODE As String = "txtMSGCode"
    Private Const GRID_CTRL_NAME_UI_PROG_CODE As String = "txtUIProgCode"
    Private Const GRID_CTRL_NAME_MSG_PARAM_COUNT As String = "ddlMSGParamCnt"
    Private Const GRID_CTRL_NAME_MSG_TEXT As String = "lblMSGText"

    Public Const MSG_NONE_OR_MORE_THAN_ONE_RECORD_FOUND As String = "NONE_OR_MORE_THAN_ONE_RECORD_FOUND"

    'Private MSGTYPELIST As DataView
    Dim MessageTypeList As DataElements.ListItem()
#End Region

#Region "Page State"
    ' This class keeps the current state for the search page.
    Class MyState
        Public MyBO As MessageCode
        Public MSGCodeID As Guid
        Public PageIndex As Integer = 0
        Public PageSize As Integer = DEFAULT_PAGE_SIZE
        Public searchDV As MessageCode.MessageCodeSearchDV = Nothing
        Public HasDataChanged As Boolean
        Public IsGridAddNew As Boolean = False
        Public IsGridVisible As Boolean
        Public SortExpression As String = MessageCode.MessageCodeSearchDV.COL_MSG_CODE

        Public searchUIProgCode As String = ""
        Public searchMsgCode As String = ""
        Public searchMsgType As Guid = Guid.Empty

        Public editUIProgCode As String = ""
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
            Return Me.Grid.EditIndex > Me.NO_ITEM_SELECTED_INDEX
        End Get
    End Property
#End Region

#Region "Page Events"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.ErrControllerMaster.Clear_Hide()
        Me.Form.DefaultButton = btnSearch.UniqueID
        Try
            'MSGTYPELIST = LookupListNew.DropdownLookupList(LookupListNew.LK_MSG_TYPE, ElitaPlusIdentity.Current.ActiveUser.LanguageId, True)

            MessageTypeList = CommonConfigManager.Current.ListManager.GetList(listCode:="MSGTYPE",
                                                                              languageCode:=Thread.CurrentPrincipal.GetLanguageCode())

            If Not Me.IsPostBack Then
                Me.SetFormTitle(PAGETITLE)
                Me.SetFormTab(PAGETAB)
                populateSearchControls()
                TranslateGridHeader(Grid)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
        Me.ShowMissingTranslations(Me.ErrControllerMaster)
    End Sub

    Private Sub MessageCodeForm_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        cboPageSize.Visible = Grid.Visible
        lblRecordCount.Visible = Grid.Visible

        If ErrControllerMaster.Visible Then
            If Grid.Visible And Grid.Rows.Count < 10 Then
                Dim fillerHight As Integer = 200
                fillerHight = fillerHight - Grid.Rows.Count * 20
                Me.spanFiller.Text = "<tr><td colspan=""2"" style=""height:" & fillerHight & "px"">&nbsp;</td></tr>"
            End If
        Else
            Me.spanFiller.Text = ""
        End If
    End Sub
#End Region

#Region "Grid Handler"

    Private Sub Grid_PageIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grid.PageIndexChanged
        Try
            Me.State.PageIndex = Grid.PageIndex
            Me.State.MSGCodeID = Guid.Empty
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Private Sub Grid_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
        Try
            Grid.PageIndex = e.NewPageIndex
            State.PageIndex = Grid.PageIndex
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Private Sub Grid_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Grid.RowCommand
        Try
            'ignore other commands
            If e.CommandName = "SelectAction" OrElse e.CommandName = "DeleteRecord" Then
                Dim lblCtrl As Label
                Dim row As GridViewRow = CType(CType(e.CommandSource, Control).Parent.Parent, GridViewRow)
                Dim RowInd As Integer = row.RowIndex
                lblCtrl = CType(Grid.Rows(RowInd).Cells(GRID_COL_MSG_CODE_ID_IDX).FindControl(Me.GRID_CTRL_NAME_MSG_CODE_ID), Label)
                State.MSGCodeID = New Guid(lblCtrl.Text)
                State.MyBO = New MessageCode(State.MSGCodeID)
                If e.CommandName = "SelectAction" Then
                    Grid.EditIndex = RowInd
                    PopulateGrid()
                ElseIf e.CommandName = "DeleteRecord" Then
                    Try
                        Me.State.MyBO.Delete()
                        Me.State.MyBO.Save()
                    Catch ex As Exception
                        Me.State.MyBO.RejectChanges()
                        Throw ex
                    End Try
                    State.searchDV.Delete(RowInd)
                    PopulateGrid()
                End If
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Private Sub Grid_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Private Sub Grid_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
        Try
            Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
            Dim lblTemp As Label, ddl As DropDownList, txt As TextBox

            If (itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem) AndAlso e.Row.RowIndex <> -1 Then
                With e.Row
                    If .RowIndex = Grid.EditIndex Then
                        ddl = CType(e.Row.FindControl(GRID_CTRL_NAME_MSG_TYPE), DropDownList)
                        If Not ddl Is Nothing Then
                            Try
                                'Me.BindListControlToDataView(ddl, MSGTYPELIST)

                                ddl.Populate(MessageTypeList.ToArray(),
                                                New PopulateOptions() With
                                                {
                                                    .AddBlankItem = True
                                                })

                                Me.SetSelectedItem(ddl, Me.State.MyBO.MsgType)
                            Catch ex As Exception
                            End Try
                        End If
                        ddl = Nothing
                        ddl = CType(e.Row.FindControl(GRID_CTRL_NAME_MSG_PARAM_COUNT), DropDownList)
                        If Not ddl Is Nothing Then
                            ddl.SelectedValue = State.MyBO.MsgParameterCount.ToString
                        End If

                        If State.IsGridAddNew Then
                            'populate controls from BO
                            txt = CType(e.Row.FindControl(GRID_CTRL_NAME_MSG_CODE), TextBox)
                            If Not txt Is Nothing Then txt.Text = State.MyBO.MsgCode
                            txt = Nothing
                            txt = CType(e.Row.FindControl(GRID_CTRL_NAME_UI_PROG_CODE), TextBox)
                            If Not txt Is Nothing Then txt.Text = State.editUIProgCode
                        End If
                    End If
                End With
            ElseIf itemType = ListItemType.EditItem Then
                ddl = CType(e.Row.FindControl(GRID_CTRL_NAME_MSG_TYPE), DropDownList)
                If Not ddl Is Nothing Then
                    Try
                        'Me.BindListControlToDataView(ddl, MSGTYPELIST)

                        ddl.Populate(MessageTypeList.ToArray(),
                                                New PopulateOptions() With
                                                {
                                                    .AddBlankItem = True
                                                })

                        Me.SetSelectedItem(ddl, Me.State.MyBO.MsgType)
                    Catch ex As Exception
                    End Try
                End If
            End If
            BaseItemBound(sender, e)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Private Sub Grid_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting
        Try
            Dim strSort As String = e.SortExpression
            With State
                If .SortExpression.StartsWith(e.SortExpression) Then
                    If Not .SortExpression.EndsWith("DESC") Then
                        strSort = strSort & " DESC"
                    End If
                End If
                .SortExpression = strSort
            End With
            PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Protected Sub cboPageSize_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            State.PageSize = CType(cboPageSize.SelectedValue, Integer)
            Me.State.PageIndex = NewCurrentPageIndex(Grid, State.searchDV.Count, State.PageSize)
            Me.Grid.PageIndex = Me.State.PageIndex
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub
#End Region

#Region "Button click Handler"
    Protected Sub btnClearSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnClearSearch.Click
        Try
            Me.txtMSGCodeSearch.Text = String.Empty
            Me.txtUIProgCodeSearch.Text = String.Empty
            Me.ddlSMSGTypeSearch.SelectedIndex = Me.NO_ITEM_SELECTED_INDEX

            Grid.EditIndex = Me.NO_ITEM_SELECTED_INDEX
            With State
                .MSGCodeID = Guid.Empty
            End With

        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        Try
            With State
                .PageIndex = 0
                .MSGCodeID = Guid.Empty
                .IsGridVisible = True
                .searchDV = Nothing
                .HasDataChanged = False

                .searchUIProgCode = Me.txtUIProgCodeSearch.Text.Trim
                .searchMsgCode = txtMSGCodeSearch.Text.Trim.Trim
                .searchMsgType = New Guid(ddlSMSGTypeSearch.SelectedValue)
            End With


            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnNew.Click
        Try
            State.IsGridVisible = True
            AddNew()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSave.Click
        Try
            Dim ErrMsg As New Collections.Generic.List(Of String)
            If PopulateBOFromForm(ErrMsg) Then
                With State
                    If (.MyBO.IsDirty) Then
                        If (Not .IsGridAddNew) OrElse (.IsGridAddNew AndAlso IsMsgCodeNew(.MyBO)) Then
                            .MyBO.Save()
                            Grid.EditIndex = Me.NO_ITEM_SELECTED_INDEX
                            If .IsGridAddNew And .searchDV.Count = 1 Then
                                .searchMsgCode = .MyBO.MsgCode
                                .searchMsgType = .MyBO.MsgType
                            End If
                            .editUIProgCode = ""
                            .IsGridAddNew = False
                            Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                            State.searchDV = Nothing
                        Else
                            ErrControllerMaster.AddErrorAndShow(MessageCode.Err_MSG_CODE_EXISTS)
                        End If
                    Else
                        Me.AddInfoMsg(Message.MSG_RECORD_NOT_SAVED)
                    End If
                End With
            Else
                Me.ErrControllerMaster.AddErrorAndShow(ErrMsg.ToArray, False)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
        Me.PopulateGrid()
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
        Try
            With State
                If .IsGridAddNew Then
                    RemoveNewRowFromSearchDV()
                    .editUIProgCode = ""
                    .IsGridAddNew = False
                    Grid.PageIndex = .PageIndex
                End If
                .MSGCodeID = Guid.Empty
                .MSGCodeID = Nothing
            End With
            Grid.EditIndex = NO_ITEM_SELECTED_INDEX
            PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub
#End Region

#Region "Helper functions"
    Private Function IsMsgCodeNew(ByVal objMC As MessageCode) As Boolean
        Dim blnIsValid As Boolean = True
        Dim strMsgType As String = (From MessageType In MessageTypeList
                                    Where MessageType.ListItemId = objMC.MsgType
                                    Select MessageType.Code).FirstOrDefault().ToString()

        'LookupListNew.GetCodeFromId(MSGTYPELIST, objMC.MsgType)

        If Not MessageCode.IsNewMsgCode(State.MyBO.MsgCode, strMsgType, State.editUIProgCode) Then
            blnIsValid = False
        End If
        Return blnIsValid
    End Function
    Private Function PopulateBOFromForm(ByRef errMsg As Collections.Generic.List(Of String)) As Boolean
        Dim blnSuccess As Boolean = True
        Dim ind As Integer = Grid.EditIndex
        With Me.State.MyBO
            Dim ddl As DropDownList = CType(Grid.Rows(ind).Cells(GRID_COL_MSG_TYPE_IDX).FindControl(GRID_CTRL_NAME_MSG_TYPE), DropDownList)
            Me.PopulateBOProperty(Me.State.MyBO, "MsgType", ddl)
            If .MsgType = Guid.Empty Then
                blnSuccess = False
                errMsg.Add(TranslationBase.TranslateLabelOrMessage("MSG_TYPE") & ":" & TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_VALUE_MANDATORY_ERR))
            End If
            ddl = Nothing

            .MsgCode = CType(Grid.Rows(ind).Cells(GRID_COL_MSG_CODE_IDX).FindControl(GRID_CTRL_NAME_MSG_CODE), TextBox).Text.Trim.ToUpper
            If .MsgCode = String.Empty Then
                blnSuccess = False
                errMsg.Add(TranslationBase.TranslateLabelOrMessage("MSG_CODE") & ":" & TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_VALUE_MANDATORY_ERR))
            End If

            ddl = CType(Grid.Rows(ind).Cells(GRID_COL_MSG_PARAM_COUNT_IDX).FindControl(GRID_CTRL_NAME_MSG_PARAM_COUNT), DropDownList)
            Dim intTemp As Integer = 0
            Integer.TryParse(ddl.SelectedValue, intTemp)
            .MsgParameterCount = intTemp

            Dim strUIProgCode As String = CType(Grid.Rows(ind).Cells(GRID_COL_UI_PROG_CODE_IDX).FindControl(GRID_CTRL_NAME_UI_PROG_CODE), TextBox).Text.Trim.ToUpper
            State.editUIProgCode = strUIProgCode
            If strUIProgCode = String.Empty Then
                blnSuccess = False
                errMsg.Add(TranslationBase.TranslateLabelOrMessage("UI_PROG_CODE") & ":" & TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_VALUE_MANDATORY_ERR))
            Else
                Dim dv As Label_Extended.LabelSearchDV = Label_Extended.LoadList(strUIProgCode, True)
                If dv.Count <> 1 Then
                    blnSuccess = False
                    errMsg.Add(TranslationBase.TranslateLabelOrMessage("UI_PROG_CODE") & ":" & TranslationBase.TranslateLabelOrMessage(MSG_NONE_OR_MORE_THAN_ONE_RECORD_FOUND))
                Else
                    Dim guidDID As Guid = New Guid(CType(dv(0)(Label_Extended.LabelSearchDV.DICT_ITEM_ID), Byte()))
                    Dim ds As DataSet = New DataSet
                    Dim oLabel As Label_Extended = New Label_Extended(guidDID, ds, True)
                    .LabelId = oLabel.Id
                End If
            End If
        End With
        Return blnSuccess
    End Function

    Private Sub populateSearchControls()
        Try
            'Dim dv As DataView = MSGTYPELIST
            'dv.Sort = "DESCRIPTION"
            'BindListControlToDataView(Me.ddlSMSGTypeSearch, dv, "DESCRIPTION", "ID", True)

            ddlSMSGTypeSearch.Populate(MessageTypeList.ToArray(),
                                        New PopulateOptions() With
                                        {
                                            .AddBlankItem = True
                                        })

        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Public Sub PopulateGrid()
        With State
            If (.searchDV Is Nothing) Then
                .searchDV = MessageCode.getList(.searchMsgType, .searchMsgCode, .searchUIProgCode)
            End If
            .searchDV.Sort = .SortExpression
            If .IsGridAddNew Then .PageIndex = Grid.PageIndex
        End With

        If State.searchDV.Count = 0 Then
            Dim dt As DataTable = State.searchDV.Table.Clone()
            Dim dv As New MessageCode.MessageCodeSearchDV(dt)
            Dim objEmpty As MessageCode = New MessageCode
            MessageCode.AddNewRowToMSGCodeSearchDV(dv, objEmpty)
            SetPageAndSelectedIndexFromGuid(dv, Me.State.MSGCodeID, Me.Grid, Me.State.PageIndex, (Me.IsGridInEditMode OrElse State.IsGridAddNew))
            SortAndBindGrid(dv, True)
        Else
            SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.MSGCodeID, Me.Grid, Me.State.PageIndex, (Me.IsGridInEditMode OrElse State.IsGridAddNew))
            SortAndBindGrid(State.searchDV)
        End If
        If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True
    End Sub

    Private Sub SortAndBindGrid(ByVal dvBinding As MessageCode.MessageCodeSearchDV, Optional ByVal blnEmptyList As Boolean = False)
        Me.Grid.DataSource = dvBinding
        HighLightSortColumn(Grid, Me.State.SortExpression)
        Me.Grid.DataBind()

        ControlMgr.SetVisibleControl(Me, Grid, Me.State.IsGridVisible)
        ControlMgr.SetVisibleControl(Me, trPageSize, Me.Grid.Visible)

        If Me.State.searchDV.Count > 0 Then
            If Me.Grid.Visible Then
                Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If
        Else
            If Me.Grid.Visible Then
                Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If
        End If

        SetControlState()
        If blnEmptyList Then
            For Each gvRow As GridViewRow In Grid.Rows
                gvRow.Visible = False
                gvRow.Controls.Clear()
            Next
        End If
    End Sub

    Private Sub SetControlState()

        If (Me.IsGridInEditMode) Then
            ControlMgr.SetVisibleControl(Me, btnNew, False)
            ControlMgr.SetVisibleControl(Me, btnCancel, True)
            ControlMgr.SetVisibleControl(Me, btnSave, True)
            ControlMgr.SetEnableControl(Me, btnSearch, False)
            ControlMgr.SetEnableControl(Me, btnClearSearch, False)
            Me.MenuEnabled = False
            If (Me.cboPageSize.Enabled) Then
                ControlMgr.SetEnableControl(Me, cboPageSize, False)
            End If
        Else
            ControlMgr.SetVisibleControl(Me, btnNew, True)
            ControlMgr.SetVisibleControl(Me, btnCancel, False)
            ControlMgr.SetVisibleControl(Me, btnSave, False)
            ControlMgr.SetEnableControl(Me, btnSearch, True)
            ControlMgr.SetEnableControl(Me, btnClearSearch, True)
            Me.MenuEnabled = True
            If Not (Me.cboPageSize.Enabled) Then
                ControlMgr.SetEnableControl(Me, Me.cboPageSize, True)
            End If
        End If

    End Sub

    Private Sub AddNew()
        'If State.searchDV Is Nothing Then State.searchDV = MessageCode.getList(Guid.Empty, "@@@@GOTADUMMYRECORD@@@@@", "")
        Me.State.MyBO = New MessageCode
        Me.State.MSGCodeID = Me.State.MyBO.Id
        State.MyBO.AddNewRowToMSGCodeSearchDV(Me.State.searchDV, Me.State.MyBO)
        State.IsGridAddNew = True
        PopulateGrid()
        'Set focus on the message code TextBox for the EditItemIndex row
        Dim objCtrl As WebControl = CType(Grid.Rows(Grid.EditIndex).Cells(GRID_COL_MSG_CODE_IDX).FindControl(GRID_CTRL_NAME_MSG_CODE), WebControl)
        If Not objCtrl Is Nothing Then SetFocus(objCtrl)
    End Sub

    Public Function GetMsgTypeDropDownDescription(ByVal MsgTypeID As Object) As String
        'Return LookupListNew.GetDescriptionFromId(MSGTYPELIST, New Guid(CType(MsgTypeID, Byte())))

        Dim rtnValue As String = (From MessageType In MessageTypeList
                                  Where MessageType.ListItemId = New Guid(CType(MsgTypeID, Byte()))
                                  Select MessageType.Translation).FirstOrDefault()

        If String.IsNullOrEmpty(rtnValue) Then
            Return String.Empty
        Else
            Return rtnValue
        End If

    End Function

    Public Function TranlateLabel(ByVal strLabel As String) As String
        If strLabel.Trim <> String.Empty Then
            Return TranslationBase.TranslateLabelOrMessage(strLabel)
        Else
            Return String.Empty
        End If
    End Function

    Private Function GetRowIndexFromSearchDVByID(ByVal MSGCodeID As Guid) As Integer
        Dim rowind As Integer = NO_ITEM_SELECTED_INDEX
        With State
            If Not .searchDV Is Nothing Then
                rowind = FindSelectedRowIndexFromGuid(.searchDV, MSGCodeID)
            End If
        End With
        Return rowind
    End Function
    Private Sub RemoveNewRowFromSearchDV()
        Dim rowind As Integer = GetRowIndexFromSearchDVByID(State.MSGCodeID)
        If rowind <> NO_ITEM_SELECTED_INDEX Then State.searchDV.Delete(rowind)
    End Sub
#End Region


End Class