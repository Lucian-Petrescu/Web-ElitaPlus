Imports System.ComponentModel
Imports Assurant.ElitaPlus.BusinessObjects.Common
Imports Assurant.ElitaPlus.DALObjects

Partial Class UserControlClaimCloseRules
    Inherits System.Web.UI.UserControl

    Public Class RequestDataEventArgs
        Inherits EventArgs

        Public Data As ClaimCloseRules.CloseClaimRulesDV

    End Class


    Public Delegate Sub RequestData(ByVal sender As Object, ByRef e As RequestDataEventArgs)

    Public Event RequestClaimCloseRulesData As RequestData
    Public Event CloneDealerData As RequestData
    Public Event PropertyChanged As PropertyChangedEventHandler

    Private _companyId As Guid
    Private _dealerId As Nullable(Of Guid)

#Region "Constants"
    Private moState As MyState

    Private Const GRID_COL_CLOSE_CLAIM_RULE_ID As Integer = 0
    Private Const GRID_COL_COMPANY_ID_IDX As Integer = 1
    Private Const GRID_COL_DEALER_ID_IDX As Integer = 2
    Private Const GRID_COL_CLOSE_RULE_BASED_ON_ID_IDX As Integer = 3
    Private Const GRID_COL_CLAIM_STATUS_BY_GROUP_ID_IDX As Integer = 4
    Private Const GRID_COL_CLAIM_ISSUE_ID_IDX As Integer = 5
    Private Const GRID_COL_TIME_PERIOD_IDX As Integer = 6
    Private Const GRID_COL_REASON_CLOSED_ID_IDX As Integer = 7
    Private Const GRID_COL_ACTIVE_FLAG_IDX As Integer = 8
    Private Const GRID_COL_EDIT_IDX As Integer = 9
    Private Const GRID_COL_DELETE_IDX As Integer = 10

    Private Const GRID_CTRL_NAME_LABEL_CLOSE_RULE_ID As String = "lblCloseClaimRuleID"
    Private Const GRID_CTRL_NAME_LABEL_COMPANY As String = "lblCompany"
    Private Const GRID_CTRL_NAME_LABLE_DEALER As String = "lblDealer"
    Private Const GRID_CTRL_NAME_LABEL_CLOSE_RULE_BASED_ON As String = "lblCloseRuleBasedOn"
    Private Const GRID_CTRL_NAME_LABEL_CLAIM_STATUS_BY_GROUP As String = "lblClaimStatusByGroup"
    Private Const GRID_CTRL_NAME_LABEL_CLAIM_ISSUE As String = "lblClaimIssue"
    Private Const GRID_CTRL_NAME_LABEL_TIME_PERIOD As String = "lblTimePeriod"
    Private Const GRID_CTRL_NAME_LABEL_REASON_CLOSED As String = "lblReasonClosed"

    Private Const GRID_CTRL_NAME_EDIT_CLOSE_RULE_BASED_ON As String = "cboRuleBasedOn"
    Private Const GRID_CTRL_NAME_EDIT_CLM_STAT_BY_GRP As String = "cboClaimStatusByGroup"
    Private Const GRID_CTRL_NAME_EDIT_CLM_ISSUE As String = "cboClaimIssue"
    Private Const GRID_CTRL_NAME_EDIT_REASON_CLOSED As String = "cboReasonClosed"
    Private Const GRID_CTRL_NAME_EDIT_TIME_PERIOD As String = "txtTimePeriod"
    Private Const GRID_CTRL_NAME_EDIT_ACTIVE_FLAG As String = "txtActiveFlag"

    ''Def-25716: Added a constant to validate and get delete button reference
    Private Const GRID_CTRL_NAME_DELETE_CLAIM_RULE As String = "DeleteButton_WRITE"

    Private Const MSG_CONFIRM_PROMPT As String = "MSG_CONFIRM_PROMPT"
    Private Const MSG_RECORD_DELETED_OK As String = "MSG_RECORD_DELETED_OK"
    Private Const MSG_RECORD_SAVED_OK As String = "MSG_RECORD_SAVED_OK"
    Private Const MSG_RECORD_NOT_SAVED As String = "MSG_RECORD_NOT_SAVED"

    Private Const EDIT_COMMAND As String = "SelectAction"
    Private Const DELETE_COMMAND As String = "DeleteRecord"
    Private Const SORT_COMMAND As String = "Sort"

    Private Const NO_ROW_SELECTED_INDEX As Integer = -1
    Private Const CONST_ACTIVE_FLAG_YES As String = "Y"

    Public Const SESSION_LOCALSTATE_KEY As String = "CLAIMCLOSERULES_SESSION_LOCALSTATE_KEY"
    Public Const MIN_TIME_PERIOD As Integer = 0

#End Region

#Region "Page State"
    ' This class keeps the current state for the search page.
    Class MyState
        Public MyBO As ClaimCloseRules
        Public dtClaimCloseRules As DataTable
        Public DefaultClaimCloseRuleID As Guid
        Public PageIndex As Integer = 0
        Public PageSize As Integer = 10
        Public claimCloseRulesDV As ClaimCloseRules.CloseClaimRulesDV = Nothing
        Public HasDataChanged As Boolean
        Public IsGridAddNew As Boolean = False
        Public IsEditMode As Boolean = False
        Public IsGridVisible As Boolean
        Public IsAfterSave As Boolean
        Public ActionInProgress As Integer = ElitaPlusPage.DetailPageCommand.Nothing_
        Public SortExpression As String = ClaimCloseRules.CloseClaimRulesDV.COL_CLAIM_STATUS_BY_GROUP
        Public bnoRow As Boolean = False
        Public companyId As Guid = Guid.Empty
        Public companyCode As String = String.Empty
        Public dealerId As Guid = Guid.Empty
        Public dealer As String = String.Empty
        Public deleteRowIndex As Integer = 0
        Public isNew As String = "N"

    End Class

    Protected ReadOnly Property TheState() As MyState
        Get
            Try
                If Me.ThePage.StateSession.Item(Me.UniqueID) Is Nothing Then
                    Me.ThePage.StateSession.Item(Me.UniqueID) = New MyState
                End If
                Return CType(Me.ThePage.StateSession.Item(Me.UniqueID), MyState)

            Catch ex As Exception
                'When we are in design mode there is no session object
                Return Nothing
            End Try
        End Get
    End Property

    Private Shadows ReadOnly Property ThePage() As ElitaPlusSearchPage
        Get
            Return CType(MyBase.Page, ElitaPlusSearchPage)
        End Get
    End Property

    Public ReadOnly Property IsGridInEditMode() As Boolean
        Get
            Return Me.CloseRulesGrid.EditIndex > Me.ThePage.NO_ITEM_SELECTED_INDEX
        End Get
    End Property

    Public Property SortDirection() As String
        Get
            If Not ViewState("SortDirection") Is Nothing Then
                Return ViewState("SortDirection").ToString
            Else
                Return String.Empty
            End If

        End Get
        Set(ByVal value As String)
            ViewState("SortDirection") = value
        End Set
    End Property
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'AddHandler Me.PropertyChanged, AddressOf MyPropertyChanged
        If Me.IsPostBack Then
            CheckIfComingFromDeleteConfirm()
        End If

    End Sub

    Private Sub MyPropertyChanged(ByVal sender As Object, ByVal e As PropertyChangedEventArgs)
        If (e.PropertyName = "") OrElse
            (e.PropertyName = "") Then
            '' Hide Grid Data
        End If
    End Sub

    Public Sub Populate()

        Dim e As New RequestDataEventArgs

        If (Me.CompanyId.Equals(Guid.Empty)) Then
            Throw New GUIException("You must select a Company first", Assurant.ElitaPlus.Common.ErrorCodes.GUI_COMPANY_IS_REQUIRED)
        End If

        Select Case Me.EntityType
            Case CloseRuleEntityType.Company
                If (Not Me.DealerId.Equals(Guid.Empty)) Then
                    Throw New GUIException("You must select a dealer first", Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
                End If
            Case CloseRuleEntityType.Dealer
                If (Me.DealerId.Equals(Guid.Empty)) Then
                    Throw New GUIException("You must select a dealer first", Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
                End If
        End Select


        RaiseEvent RequestClaimCloseRulesData(Me, e)
        Me.TheState.claimCloseRulesDV = e.Data
        Me.PopulateGrid()

    End Sub

    Public Property EntityType As CloseRuleEntityType

    Public Property CompanyId As Guid
        Get
            Return Me.TheState.companyId
        End Get
        Set(ByVal value As Guid)
            Me.TheState.companyId = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("CompanyId"))
        End Set
    End Property

    Public Property companyCode As String
        Get
            Return Me.TheState.companyCode
        End Get
        Set(ByVal value As String)
            Me.TheState.companyCode = value
        End Set
    End Property

    Public Property DealerId As Nullable(Of Guid)
        Get
            Return Me.TheState.dealerId
        End Get
        Set(ByVal value As Nullable(Of Guid))
            Me.TheState.dealerId = CType(value, Guid)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("DealerId"))
        End Set
    End Property

    Public Property Dealer As String
        Get
            Return Me.TheState.dealer
        End Get
        Set(ByVal value As String)
            Me.TheState.dealer = value
        End Set
    End Property

    Public Sub HideNewButton(ByVal hide As Boolean)
        'when Clone button is clicked then hide the New button and hide the grid
        If hide Then
            NewButton_WRITE.Visible = False
            CloseRulesGrid.DataSource = Nothing
            CloseRulesGrid.DataBind()
            'CloseRulesGrid.Visible = False
        Else
            NewButton_WRITE.Visible = True
            'CloseRulesGrid.Visible = True
        End If

    End Sub



    Public Enum CloseRuleEntityType
        Company = 1
        Dealer = 2
    End Enum


#Region "Helper functions"

    Private Sub SetControlState()
        If (Me.TheState.IsEditMode) Then
            ControlMgr.SetVisibleControl(Me.ThePage, NewButton_WRITE, False)
            If (Me.cboPageSize.Enabled) Then
                ControlMgr.SetEnableControl(Me.ThePage, cboPageSize, False)
            End If
        Else
            ControlMgr.SetVisibleControl(Me.ThePage, NewButton_WRITE, True)
            If Not (Me.cboPageSize.Enabled) Then
                ControlMgr.SetEnableControl(Me.ThePage, Me.cboPageSize, True)
            End If
        End If
    End Sub

    Private Sub ReturnFromEditing()

        CloseRulesGrid.EditIndex = NO_ROW_SELECTED_INDEX

        If (Me.CloseRulesGrid.PageCount = 0) Then
            ControlMgr.SetVisibleControl(Me.ThePage, CloseRulesGrid, False)
        Else
            ControlMgr.SetVisibleControl(Me.ThePage, CloseRulesGrid, True)
        End If

        Me.TheState.IsEditMode = False
        Me.PopulateGrid()
        Me.TheState.PageIndex = CloseRulesGrid.PageIndex
        SetControlState()
    End Sub

    Private Sub RemoveNewRowFromSearchDV()
        Dim rowind As Integer = Me.ThePage.NO_ITEM_SELECTED_INDEX
        With TheState
            If Not .claimCloseRulesDV Is Nothing Then
                rowind = Me.ThePage.FindSelectedRowIndexFromGuid(.claimCloseRulesDV, .DefaultClaimCloseRuleID)
            End If
        End With
        If rowind <> Me.ThePage.NO_ITEM_SELECTED_INDEX Then TheState.claimCloseRulesDV.Delete(rowind)
    End Sub

    Private Function PopulateBOFromForm() As Boolean
        Dim objDropDownList As DropDownList
        Dim objTimePeriodTxt As TextBox
        Dim objActiveFlagTxt As TextBox
        Dim objDealerLbl As Label
        With Me.TheState.MyBO

            'validation
            objTimePeriodTxt = CType(CloseRulesGrid.Rows(Me.CloseRulesGrid.EditIndex).Cells(GRID_COL_TIME_PERIOD_IDX).FindControl(GRID_CTRL_NAME_EDIT_TIME_PERIOD), TextBox)
            objDealerLbl = CType(CloseRulesGrid.Rows(Me.CloseRulesGrid.EditIndex).Cells(GRID_COL_DEALER_ID_IDX).FindControl(GRID_CTRL_NAME_LABLE_DEALER), Label)

            If (Me.TheState.IsEditMode = True AndAlso Me.TheState.IsGridAddNew = False) Then

                'if the entity type is company then insert a new row and update the existing row to in-active
                'if entity type is dealer then keep the company record active and insert a new row with dealer id and parent claim close rule
                'if the record is already updated by the dealer then insert a new row and deactivate the current row

                If ((Me.EntityType = EntityType.Company) OrElse (Me.EntityType = EntityType.Dealer AndAlso CloseRulesGrid.DataKeys(CloseRulesGrid.EditIndex).Values(2).ToString() <> String.Empty)) Then
                    ClaimCloseRules.UpdateClaimCloseRuleInActive(Me.TheState.DefaultClaimCloseRuleID)
                End If

                'create a new object to insert this record
                Me.TheState.MyBO = New ClaimCloseRules()

                If (Me.EntityType = EntityType.Dealer) Then
                    'if the dealer record is added first time, then parent claim rule id will be the company rule id
                    If (CloseRulesGrid.DataKeys(CloseRulesGrid.EditIndex).Values(2).ToString() = String.Empty) Then
                        Me.ThePage.PopulateBOProperty(TheState.MyBO, "ParentClaimCloseRuleId", Me.TheState.DefaultClaimCloseRuleID)
                    Else
                        'if the dealer is updated/added second time, then the parent id would be the original company rule id
                        If (CloseRulesGrid.DataKeys(CloseRulesGrid.EditIndex).Values(5).ToString() <> String.Empty) Then
                            Me.ThePage.PopulateBOProperty(TheState.MyBO, "ParentClaimCloseRuleId", New Guid(CType(CloseRulesGrid.DataKeys(CloseRulesGrid.EditIndex).Values(5), Byte())))
                        End If
                    End If
                End If

            End If

            Me.ThePage.PopulateBOProperty(TheState.MyBO, "CompanyId", Me.TheState.companyId)

            If (Me.EntityType = EntityType.Dealer) Then
                Me.ThePage.PopulateBOProperty(TheState.MyBO, "DealerId", Me.TheState.dealerId)
            Else
                'if a rule is already overridden at Dealer level and when updating that rule at Company level again
                Me.ThePage.PopulateBOProperty(TheState.MyBO, "DealerId", Guid.Empty)
            End If

            objDropDownList = CType(CloseRulesGrid.Rows((Me.CloseRulesGrid.EditIndex)).Cells(GRID_COL_CLOSE_RULE_BASED_ON_ID_IDX).FindControl(GRID_CTRL_NAME_EDIT_CLOSE_RULE_BASED_ON), DropDownList)
            Me.ThePage.PopulateBOProperty(TheState.MyBO, "CloseRuleBasedOnId", objDropDownList)

            objDropDownList = CType(CloseRulesGrid.Rows((Me.CloseRulesGrid.EditIndex)).Cells(GRID_COL_CLAIM_STATUS_BY_GROUP_ID_IDX).FindControl(GRID_CTRL_NAME_EDIT_CLM_STAT_BY_GRP), DropDownList)
            Me.ThePage.PopulateBOProperty(TheState.MyBO, "ClaimStatusByGroupId", objDropDownList)

            objDropDownList = CType(CloseRulesGrid.Rows((Me.CloseRulesGrid.EditIndex)).Cells(GRID_COL_CLAIM_ISSUE_ID_IDX).FindControl(GRID_CTRL_NAME_EDIT_CLM_ISSUE), DropDownList)
            Me.ThePage.PopulateBOProperty(TheState.MyBO, "ClaimIssueId", objDropDownList)

            objDropDownList = CType(CloseRulesGrid.Rows((Me.CloseRulesGrid.EditIndex)).Cells(GRID_COL_REASON_CLOSED_ID_IDX).FindControl(GRID_CTRL_NAME_EDIT_REASON_CLOSED), DropDownList)
            Me.ThePage.PopulateBOProperty(TheState.MyBO, "ReasonClosedId", objDropDownList)

            If (objTimePeriodTxt.Text = String.Empty) Then
                objTimePeriodTxt.Text = "0"
            End If
            Me.ThePage.PopulateBOProperty(TheState.MyBO, "TimePeriod", objTimePeriodTxt)

            objActiveFlagTxt = CType(CloseRulesGrid.Rows(Me.CloseRulesGrid.EditIndex).Cells(GRID_COL_ACTIVE_FLAG_IDX).FindControl(GRID_CTRL_NAME_EDIT_ACTIVE_FLAG), TextBox)
            Me.ThePage.PopulateBOProperty(TheState.MyBO, "ActiveFlag", objActiveFlagTxt)
        End With
        If Me.ThePage.ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If
    End Function
#End Region

#Region "Grid related"

    Public Sub PopulateGrid()
        If (Not Page.IsPostBack) Then
            Me.ThePage.TranslateGridHeader(CloseRulesGrid)
        End If
        Dim blnNewSearch As Boolean = False
        cboPageSize.SelectedValue = CType(Me.TheState.PageSize, String)
        Dim objClaimCloseRules As New ClaimCloseRules
        Dim dv As DataView
        Try
            With TheState
                If (.claimCloseRulesDV Is Nothing) Then
                    objClaimCloseRules.CompanyId = Me.TheState.companyId
                    objClaimCloseRules.DealerId = Me.TheState.dealerId
                    .claimCloseRulesDV = objClaimCloseRules.GetClaimCloseRules()
                    blnNewSearch = True
                End If
            End With
            Me.TheState.claimCloseRulesDV.Sort = Me.SortDirection

            If (Me.TheState.IsAfterSave) Then
                Me.TheState.IsAfterSave = False
                Me.ThePage.SetPageAndSelectedIndexFromGuid(Me.TheState.claimCloseRulesDV, Me.TheState.DefaultClaimCloseRuleID, Me.CloseRulesGrid, Me.TheState.PageIndex)
            ElseIf (Me.TheState.IsEditMode) Then
                Me.ThePage.SetPageAndSelectedIndexFromGuid(Me.TheState.claimCloseRulesDV, Me.TheState.DefaultClaimCloseRuleID, Me.CloseRulesGrid, Me.TheState.PageIndex, Me.TheState.IsEditMode)
            Else
                Me.ThePage.SetPageAndSelectedIndexFromGuid(Me.TheState.claimCloseRulesDV, Guid.Empty, Me.CloseRulesGrid, Me.TheState.PageIndex)
            End If

            If Me.TheState.claimCloseRulesDV.Count = 0 Then
                For Each gvRow As GridViewRow In CloseRulesGrid.Rows
                    gvRow.Visible = False
                    gvRow.Controls.Clear()
                Next
                lblPageSize.Visible = False
                cboPageSize.Visible = False
                colonSepertor.Visible = False
            Else
                lblPageSize.Visible = True
                cboPageSize.Visible = True
                colonSepertor.Visible = True
            End If
            Me.CloseRulesGrid.AutoGenerateColumns = False
            SortAndBindGrid(blnNewSearch)

        Catch ex As Exception
            Me.ThePage.HandleErrors(ex, Me.ThePage.MasterPage.MessageController)
        End Try

    End Sub

    Private Sub SortAndBindGrid(Optional ByVal blnShowErr As Boolean = True)

        Dim dv As New DataView
        Dim objClaimCloseRules As New ClaimCloseRules
        Me.TheState.PageIndex = Me.CloseRulesGrid.PageIndex

        If (Me.TheState.claimCloseRulesDV.Count = 0) Then
            dv = objClaimCloseRules.GetClaimCloseRules()

            Me.TheState.bnoRow = True
            dv = objClaimCloseRules.getEmptyList(dv)
            Me.TheState.claimCloseRulesDV = Nothing
            Me.TheState.MyBO = New ClaimCloseRules
            TheState.MyBO.AddNewRowToSearchDV(Me.TheState.claimCloseRulesDV, Me.TheState.MyBO)
            Me.CloseRulesGrid.DataSource = Me.TheState.claimCloseRulesDV
            Me.ThePage.HighLightSortColumn(CloseRulesGrid, Me.SortDirection, True)
            Me.CloseRulesGrid.DataBind()
            If Not CloseRulesGrid.BottomPagerRow.Visible Then CloseRulesGrid.BottomPagerRow.Visible = True
            Me.CloseRulesGrid.Rows(0).Visible = False
            Me.TheState.IsGridAddNew = True
            Me.TheState.IsGridVisible = False
            Me.lblRecordCount.Text = "0 " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            If blnShowErr Then
                Me.ThePage.MasterPage.MessageController.AddInformation(ElitaPlus.ElitaPlusWebApp.Message.MSG_NO_RECORDS_FOUND, True)
            End If
        Else
            Me.TheState.bnoRow = False
            Me.CloseRulesGrid.Enabled = True
            Me.CloseRulesGrid.PageSize = Me.TheState.PageSize
            Me.CloseRulesGrid.DataSource = Me.TheState.claimCloseRulesDV
            Me.TheState.IsGridVisible = True
            Me.ThePage.HighLightSortColumn(CloseRulesGrid, Me.SortDirection, True)
            Me.CloseRulesGrid.DataBind()
            If Not CloseRulesGrid.BottomPagerRow.Visible Then CloseRulesGrid.BottomPagerRow.Visible = True
        End If

        ControlMgr.SetVisibleControl(Me.ThePage, CloseRulesGrid, Me.TheState.IsGridVisible)

        Session("recCount") = Me.TheState.claimCloseRulesDV.Count

        If Me.CloseRulesGrid.Visible Then
            If (Me.TheState.IsGridAddNew) Then
                Me.lblRecordCount.Text = (Me.TheState.claimCloseRulesDV.Count - 1) & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            Else
                Me.lblRecordCount.Text = Me.TheState.claimCloseRulesDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If
        End If
        ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me.ThePage, CloseRulesGrid)

    End Sub

    Private Sub Grid_PageIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CloseRulesGrid.PageIndexChanged
        Try
            If (Not (Me.TheState.IsEditMode)) Then
                Me.TheState.PageIndex = CloseRulesGrid.PageIndex
                Me.TheState.DefaultClaimCloseRuleID = Guid.Empty
                Me.PopulateGrid()
            End If
        Catch ex As Exception
            Me.ThePage.HandleErrors(ex, Me.ThePage.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles CloseRulesGrid.PageIndexChanging
        Try
            CloseRulesGrid.PageIndex = e.NewPageIndex
            TheState.PageIndex = CloseRulesGrid.PageIndex
        Catch ex As Exception
            Me.ThePage.HandleErrors(ex, Me.ThePage.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles CloseRulesGrid.RowDataBound
        Try
            Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
            Dim strID As String

            If Not dvRow Is Nothing And Not Me.TheState.bnoRow Then
                strID = Me.ThePage.GetGuidStringFromByteArray(CType(dvRow(ClaimCloseRules.CloseClaimRulesDV.COL_CLAIM_CLOSE_RULE_ID), Byte()))

                If (Me.TheState.IsEditMode = True AndAlso Me.TheState.DefaultClaimCloseRuleID.ToString.Equals(strID)) Then

                    Dim moCompanyText As Label = CType(e.Row.Cells(Me.GRID_COL_COMPANY_ID_IDX).FindControl(Me.GRID_CTRL_NAME_LABEL_COMPANY), Label)
                    moCompanyText.Text = Me.TheState.companyCode

                    '''hide the Dealer if this user control is displayed on Company page.
                    If (Me.EntityType = CloseRuleEntityType.Dealer) Then
                        Dim moDealerText As Label = CType(e.Row.Cells(Me.GRID_COL_DEALER_ID_IDX).FindControl(Me.GRID_CTRL_NAME_LABLE_DEALER), Label)
                        moDealerText.Text = Me.TheState.dealer
                        CloseRulesGrid.Columns(Me.GRID_COL_DEALER_ID_IDX).Visible = True
                    Else
                        CloseRulesGrid.Columns(Me.GRID_COL_DEALER_ID_IDX).Visible = False
                    End If

                    ''Close Rule Based On dropdown
                    Dim cboRuleBasedOn As DropDownList = CType(e.Row.Cells(Me.GRID_COL_CLOSE_RULE_BASED_ON_ID_IDX).FindControl(Me.GRID_CTRL_NAME_EDIT_CLOSE_RULE_BASED_ON), DropDownList)
                    ElitaPlusPage.BindListControlToDataView(cboRuleBasedOn, LookupListNew.GetClaimCloseRuleBasedOnList(ElitaPlusIdentity.Current.ActiveUser.LanguageId))
                    If (Not String.IsNullOrWhiteSpace(dvRow(ClaimCloseRules.CloseClaimRulesDV.COL_CLOSE_RULE_BASED_ON_ID).ToString())) Then
                        Me.ThePage.SetSelectedItem(cboRuleBasedOn, GuidControl.ByteArrayToGuid(CloseRulesGrid.DataKeys(e.Row.RowIndex).Values(7)))
                    End If

                    ''Claim Extended status dropdown
                    Dim moClaimStatusByGroupDropDown As DropDownList = CType(e.Row.Cells(Me.GRID_COL_CLAIM_STATUS_BY_GROUP_ID_IDX).FindControl(Me.GRID_CTRL_NAME_EDIT_CLM_STAT_BY_GRP), DropDownList)
                    moClaimStatusByGroupDropDown.Enabled = False
                    ElitaPlusPage.BindListControlToDataView(moClaimStatusByGroupDropDown, LookupListNew.GetExtendedStatusByGroupLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId))
                    If (Not String.IsNullOrWhiteSpace(dvRow(ClaimCloseRules.CloseClaimRulesDV.COL_CLAIM_STATUS_BY_GROUP_ID).ToString())) Then
                        Me.ThePage.SetSelectedItem(moClaimStatusByGroupDropDown, GuidControl.ByteArrayToGuid(CloseRulesGrid.DataKeys(e.Row.RowIndex).Values(3)))
                    End If

                    ''Claim Issue dropdown
                    Dim moClaimIssueDropDown As DropDownList = CType(e.Row.Cells(Me.GRID_COL_CLAIM_ISSUE_ID_IDX).FindControl(Me.GRID_CTRL_NAME_EDIT_CLM_ISSUE), DropDownList)
                    moClaimIssueDropDown.Enabled = False
                    ElitaPlusPage.BindListControlToDataView(moClaimIssueDropDown, Issue.GetList("%", String.Empty, String.Empty), "DESCRIPTION", "ISSUE_ID")
                    If (Not String.IsNullOrWhiteSpace(dvRow(ClaimCloseRules.CloseClaimRulesDV.COL_CLAIM_ISSUE_ID).ToString())) Then
                        Me.ThePage.SetSelectedItem(moClaimIssueDropDown, GuidControl.ByteArrayToGuid(CloseRulesGrid.DataKeys(e.Row.RowIndex).Values(6)))
                    End If

                    'time period text box
                    CType(e.Row.Cells(Me.GRID_COL_TIME_PERIOD_IDX).FindControl(Me.GRID_CTRL_NAME_EDIT_TIME_PERIOD), TextBox).Text = dvRow("time_period").ToString

                    ''Reason Closed dropdown
                    Dim moReasonClosedDropDown As DropDownList = CType(e.Row.Cells(Me.GRID_COL_CLOSE_CLAIM_RULE_ID).FindControl(Me.GRID_CTRL_NAME_EDIT_REASON_CLOSED), DropDownList)
                    ElitaPlusPage.BindListControlToDataView(moReasonClosedDropDown, LookupListNew.GetReasonClosedLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId))
                    If (Not String.IsNullOrWhiteSpace(dvRow(ClaimCloseRules.CloseClaimRulesDV.COL_REASON_CLOSED_ID).ToString())) Then
                        Me.ThePage.SetSelectedItem(moReasonClosedDropDown, GuidControl.ByteArrayToGuid(CloseRulesGrid.DataKeys(e.Row.RowIndex).Values(4)))
                    End If

                Else
                    CType(e.Row.Cells(Me.GRID_COL_COMPANY_ID_IDX).FindControl(Me.GRID_CTRL_NAME_LABEL_COMPANY), Label).Text = dvRow("company_code").ToString

                    If (Me.EntityType = CloseRuleEntityType.Dealer) Then
                        CType(e.Row.Cells(Me.GRID_COL_DEALER_ID_IDX).FindControl(Me.GRID_CTRL_NAME_LABLE_DEALER), Label).Text = dvRow("dealer").ToString
                        CloseRulesGrid.Columns(Me.GRID_COL_DEALER_ID_IDX).Visible = True

                        'Def-25716:The delete button will still not be avaliable at dealer screen for the company level rule)
                        If (CType(e.Row.Cells(Me.GRID_COL_DEALER_ID_IDX).FindControl(Me.GRID_CTRL_NAME_LABLE_DEALER), Label).Text = String.Empty) Then

                            CType(e.Row.Cells(Me.GRID_COL_CLOSE_CLAIM_RULE_ID).FindControl(Me.GRID_CTRL_NAME_DELETE_CLAIM_RULE), ImageButton).Visible = False

                        End If

                    Else
                        CloseRulesGrid.Columns(Me.GRID_COL_DEALER_ID_IDX).Visible = False
                    End If
                    CType(e.Row.Cells(Me.GRID_COL_CLOSE_RULE_BASED_ON_ID_IDX).FindControl(Me.GRID_CTRL_NAME_LABEL_CLOSE_RULE_BASED_ON), Label).Text = dvRow("close_rule_based_on").ToString
                    CType(e.Row.Cells(Me.GRID_COL_CLAIM_STATUS_BY_GROUP_ID_IDX).FindControl(Me.GRID_CTRL_NAME_LABEL_CLAIM_STATUS_BY_GROUP), Label).Text = dvRow("claim_status_by_group").ToString
                    CType(e.Row.Cells(Me.GRID_COL_CLAIM_ISSUE_ID_IDX).FindControl(Me.GRID_CTRL_NAME_LABEL_CLAIM_ISSUE), Label).Text = dvRow("claim_issue").ToString
                    CType(e.Row.Cells(Me.GRID_COL_REASON_CLOSED_ID_IDX).FindControl(Me.GRID_CTRL_NAME_LABEL_REASON_CLOSED), Label).Text = dvRow("reason_closed").ToString
                    CType(e.Row.Cells(Me.GRID_COL_TIME_PERIOD_IDX).FindControl(Me.GRID_CTRL_NAME_LABEL_TIME_PERIOD), Label).Text = dvRow("time_period").ToString

                End If
            End If

        Catch ex As Exception
            Me.ThePage.HandleErrors(ex, Me.ThePage.MasterPage.MessageController)
        End Try

    End Sub

    Public Sub RowCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles CloseRulesGrid.RowCommand

        Try
            Dim index As Integer
            If (e.CommandName = Me.EDIT_COMMAND) Then
                index = CInt(e.CommandArgument)
                'Do the Edit here

                'Set the IsEditMode flag to TRUE
                Me.TheState.IsEditMode = True

                Me.TheState.DefaultClaimCloseRuleID = GuidControl.ByteArrayToGuid(CloseRulesGrid.DataKeys(index).Values(0))

                Me.TheState.MyBO = New ClaimCloseRules(Me.TheState.DefaultClaimCloseRuleID)

                Me.Populate()

                Me.TheState.PageIndex = CloseRulesGrid.PageIndex

                Me.SetControlState()

                Try
                    Me.CloseRulesGrid.Rows(index).Focus()
                Catch ex As Exception
                    Me.CloseRulesGrid.Focus()
                End Try

            ElseIf (e.CommandName = Me.DELETE_COMMAND) Then
                index = CInt(e.CommandArgument)
                Me.TheState.deleteRowIndex = index

                Me.ThePage.DisplayMessage(Message.DELETE_RECORD_PROMPT, "", Me.ThePage.MSG_BTN_YES_NO, Me.ThePage.MSG_TYPE_CONFIRM, Me.HiddenDeletePromptResponse)
                Me.TheState.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete
                Me.CloseRulesGrid.Focus()
            End If
        Catch ex As Exception
            Me.ThePage.HandleErrors(ex, Me.ThePage.MasterPage.MessageController)
        End Try

    End Sub

    Public Sub RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles CloseRulesGrid.RowCreated
        Try
            ThePage.BaseItemCreated(sender, e)
        Catch ex As Exception
            Me.ThePage.HandleErrors(ex, Me.ThePage.MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Dropdowns"

    Private Sub populateReasonsClosed(ByRef cboClosedReason As DropDownList)
        Try
            Dim dv As DataView
            If (dv Is Nothing) Then
                dv = LookupListNew.GetReasonClosedLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            End If

            cboClosedReason.DataSource = dv
            cboClosedReason.DataBind()

        Catch ex As Exception

        End Try
    End Sub
#End Region

#Region "Control Handler"

    Protected Sub NewButton_WRITE_Click(ByVal sender As Object, ByVal e As EventArgs) Handles NewButton_WRITE.Click

        Try
            TheState.IsEditMode = True
            TheState.IsGridVisible = True
            TheState.IsGridAddNew = True
            AddNew()
            Me.SetControlState()

        Catch ex As Exception
            Me.ThePage.HandleErrors(ex, Me.ThePage.MasterPage.MessageController)
        End Try

    End Sub

    Private Sub AddNew()
        If TheState.MyBO Is Nothing OrElse Me.TheState.MyBO.IsNew = False Then
            TheState.MyBO = New ClaimCloseRules
            TheState.MyBO.AddNewRowToSearchDV(Me.TheState.claimCloseRulesDV, Me.TheState.MyBO)
        End If
        TheState.DefaultClaimCloseRuleID = Me.TheState.MyBO.Id
        TheState.IsGridAddNew = True
        PopulateGrid()
        'Set focus on the Code TextBox for the EditItemIndex row
        ThePage.SetPageAndSelectedIndexFromGuid(Me.TheState.claimCloseRulesDV, Me.TheState.DefaultClaimCloseRuleID, Me.CloseRulesGrid, _
          TheState.PageIndex, Me.TheState.IsEditMode)
        ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me.ThePage, CloseRulesGrid)
        ThePage.SetGridControls(Me.CloseRulesGrid, False)

        Try
            Me.CloseRulesGrid.Rows(Me.CloseRulesGrid.SelectedIndex).Focus()
        Catch ex As Exception
            Me.CloseRulesGrid.Focus()
        End Try

    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs)

        Try
            PopulateBOFromForm()
            '25716: Check if the claim close rule already exits  before saving claim close rules.
            Dim objClaimCloseRules As New ClaimCloseRules
            Dim isClaimCloseRulesExists As Integer = objClaimCloseRules.ValidateClaimCloseRule(Me.TheState.companyId, Me.TheState.dealerId, TheState.MyBO.CloseRuleBasedOnId, TheState.MyBO.ClaimStatusByGroupId, Me.EntityType.ToString(), TheState.MyBO.ClaimIssueId)

            If isClaimCloseRulesExists > 0 Then
                Me.ThePage.MasterPage.MessageController.AddWarning(Assurant.ElitaPlus.Common.ErrorCodes.INVALID_CLAIM_CLOASE_RULE, True)
                Return
            End If
            '25716-End
            If (Me.TheState.MyBO.IsDirty) Then
                Try
                    Me.TheState.MyBO.Save()
                Catch ex As DataBaseUniqueKeyConstraintViolationException
                    Throw New GUIException("Unique constraint violation", Assurant.ElitaPlus.Common.ErrorCodes.DUPLICATE_KEY_CONSTRAINT_VIOLATED)
                End Try

                Me.TheState.IsAfterSave = True
                Me.TheState.IsGridAddNew = False
                Me.ThePage.MasterPage.MessageController.AddSuccess(Me.MSG_RECORD_SAVED_OK, True)
                Me.TheState.claimCloseRulesDV = Nothing
                Me.TheState.MyBO = Nothing
                Me.ReturnFromEditing()
            Else
                Me.ThePage.MasterPage.MessageController.AddWarning(Me.MSG_RECORD_NOT_SAVED, True)
                Me.ReturnFromEditing()
            End If
        Catch ex As Exception
            Me.ThePage.HandleErrors(ex, Me.ThePage.MasterPage.MessageController)
        End Try

    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            With TheState
                If .IsGridAddNew Then
                    RemoveNewRowFromSearchDV()
                    .IsGridAddNew = False
                    CloseRulesGrid.PageIndex = .PageIndex
                End If
                .DefaultClaimCloseRuleID = Guid.Empty
                Me.TheState.MyBO = Nothing
                .IsEditMode = False
            End With
            CloseRulesGrid.EditIndex = Me.ThePage.NO_ITEM_SELECTED_INDEX

            PopulateGrid()
            SetControlState()
            Me.CloseRulesGrid.Focus()
        Catch ex As Exception
            Me.ThePage.HandleErrors(ex, Me.ThePage.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try

            CloseRulesGrid.PageIndex = NewCurrentPageIndex(CloseRulesGrid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
            Me.TheState.PageSize = CType(cboPageSize.SelectedValue, Integer)
            Me.PopulateGrid()
        Catch ex As Exception
            Me.ThePage.HandleErrors(ex, Me.ThePage.MasterPage.MessageController)
        End Try
    End Sub

    Private Overloads Function NewCurrentPageIndex(ByVal dg As GridView, ByVal intRecordCount As Integer, ByVal intNewPageSize As Integer) As Integer
        Dim intOldPageSize As Integer    ' old page size    
        Dim intFirstRecordIndex As Integer        ' top record index on current page
        Dim intNewPageCount As Integer  ' new page count
        Dim intNewCurrentPageIndex As Integer

        intOldPageSize = dg.PageSize      ' is given from the DataGrid PageSize property
        ' identifies the current page
        intFirstRecordIndex = dg.PageIndex * intOldPageSize + 1

        ' set the new page size for the Data grig
        dg.PageSize = intNewPageSize

        ' The actual page count of the DataGrid control
        ' is the "old" page count.
        ' The new page count of the DataGrid control will be set
        ' automatically after we bind the Datagrid to the data source
        ' with new page size set.
        ' We we need the new page count arleady now to find out the new current page index,
        ' so we must calculate it.        
        intNewPageCount = CType(Math.Ceiling(intRecordCount / intNewPageSize), Integer)
        ' get the new current page index
        Dim i As Integer
        For i = 1 To intNewPageCount
            If intFirstRecordIndex >= (i - 1) * intNewPageSize + 1 And intFirstRecordIndex <= i * intNewPageSize Then
                intNewCurrentPageIndex = i - 1
                Exit For
            End If
        Next i

        NewCurrentPageIndex = intNewCurrentPageIndex

    End Function
#End Region
    Protected Sub CheckIfComingFromDeleteConfirm()
        Dim confResponse As String = Me.HiddenDeletePromptResponse.Value
        Dim confResponseDel As String = Me.HiddenDeletePromptResponse.Value
        Try
            If Not confResponseDel Is Nothing AndAlso confResponseDel = Me.ThePage.MSG_VALUE_YES Then
                If Me.TheState.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete Then
                    Me.TheState.DefaultClaimCloseRuleID = GuidControl.ByteArrayToGuid(CloseRulesGrid.DataKeys(Me.TheState.deleteRowIndex).Values(0))
                    'make the rule inactive
                    ClaimCloseRules.UpdateClaimCloseRuleInActive(Me.TheState.DefaultClaimCloseRuleID)

                    Me.ThePage.MasterPage.MessageController.AddSuccess(Me.MSG_RECORD_DELETED_OK, True)

                    Me.TheState.PageIndex = CloseRulesGrid.PageIndex

                    'Set the IsAfterSave flag to TRUE so that the Paging logic gets invoked
                    Me.TheState.IsAfterSave = True
                    Me.TheState.claimCloseRulesDV = Nothing
                    PopulateGrid()
                    Me.TheState.PageIndex = CloseRulesGrid.PageIndex
                    Me.TheState.IsEditMode = False
                    SetControlState()
                    'Clean after consuming the action
                    Me.TheState.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                    Me.HiddenDeletePromptResponse.Value = ""
                End If
            ElseIf Not confResponseDel Is Nothing AndAlso confResponseDel = Me.ThePage.MSG_VALUE_NO Then
                'Clean after consuming the action
                Me.TheState.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                Me.HiddenDeletePromptResponse.Value = ""
            End If
        Catch ex As Exception
            Me.ThePage.HandleErrors(ex, Me.ThePage.MasterPage.MessageController)
        End Try

    End Sub

    Protected Sub cboRuleBasedOn_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            Dim dvRuleBasedOn As DataView = LookupListNew.GetClaimCloseRuleBasedOnList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            dvRuleBasedOn.RowFilter += String.Format("And Description='{0}'", CType(sender, DropDownList).SelectedItem.Text)

            Dim selectedCode As String = "CLEXTSTAT"

            If (dvRuleBasedOn.Count = 1) Then
                selectedCode = dvRuleBasedOn.Item(0).Item("code").ToString()
            End If

            Dim SelectedRow As GridViewRow = CloseRulesGrid.Rows(Me.CloseRulesGrid.EditIndex)

            'CLMISSUE and CLEXTSTAT are mutually exclusive - there can be only one.
            Select Case selectedCode
                Case "CLMISSUE"
                    CType(SelectedRow.Cells(GRID_COL_CLAIM_STATUS_BY_GROUP_ID_IDX).FindControl(GRID_CTRL_NAME_EDIT_CLM_ISSUE), DropDownList).Enabled = True

                    'Reset the claim extended status selection to nothing
                    Dim moClaimStatusByGroupDropDown As DropDownList = CType(SelectedRow.Cells(GRID_COL_CLAIM_STATUS_BY_GROUP_ID_IDX).FindControl(GRID_CTRL_NAME_EDIT_CLM_STAT_BY_GRP), DropDownList)
                    moClaimStatusByGroupDropDown.SelectedIndex = 0
                    moClaimStatusByGroupDropDown.Enabled = False

                Case "CLEXTSTAT"
                    CType(SelectedRow.Cells(GRID_COL_CLAIM_STATUS_BY_GROUP_ID_IDX).FindControl(GRID_CTRL_NAME_EDIT_CLM_STAT_BY_GRP), DropDownList).Enabled = True

                    'Reset the claim issue selection to nothing
                    Dim moClaimIssueDropDown As DropDownList = CType(SelectedRow.Cells(GRID_COL_CLAIM_ISSUE_ID_IDX).FindControl(GRID_CTRL_NAME_EDIT_CLM_ISSUE), DropDownList)
                    moClaimIssueDropDown.SelectedIndex = 0
                    moClaimIssueDropDown.Enabled = False
            End Select

            'Reset the other values
            CType(SelectedRow.Cells(GRID_COL_TIME_PERIOD_IDX).FindControl(GRID_CTRL_NAME_EDIT_TIME_PERIOD), TextBox).Text = String.Empty
            CType(SelectedRow.Cells(GRID_COL_REASON_CLOSED_ID_IDX).FindControl(GRID_CTRL_NAME_EDIT_REASON_CLOSED), DropDownList).SelectedIndex = 0

        Catch ex As Exception
            Me.ThePage.HandleErrors(ex, Me.ThePage.MasterPage.MessageController)

        End Try
    End Sub
End Class