Option Strict On
Option Explicit On
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Namespace Tables


    Partial Public Class MfgCoverageExtForm
        Inherits ElitaPlusSearchPage

        Public Shared URL As String = "MfgCoverageExtForm.aspx"

#Region "Constants"
        Private Const MSG_CONFIRM_PROMPT As String = "MSG_CONFIRM_PROMPT"
        Private Const MSG_RECORD_DELETED_OK As String = "MSG_RECORD_DELETED_OK"
        Private Const MSG_RECORD_SAVED_OK As String = "MSG_RECORD_SAVED_OK"
        Private Const MSG_RECORD_NOT_SAVED As String = "MSG_RECORD_NOT_SAVED"

        Private Const EDIT_COMMAND As String = "EditRecord"
        Private Const DELETE_COMMAND As String = "DeleteRecord"
        Private Const SORT_COMMAND As String = "Sort"

        Private Const NO_ROW_SELECTED_INDEX As Integer = -1

        Private Const MFG_COVERAGE_EXT_ID_COL As Integer = 2
        Private Const DEALER_COL As Integer = 3
        Private Const EXT_WARRANTY_COL As Integer = 4

        Private Const DEALER_DDL_CONTROL As String = "ddlstDealer"
        Private Const DEALER_LABEL_CONTROL As String = "lblDealer"
        Private Const EXT_WARRANTY_CONTROL As String = "txtExtWarranty"
        Private Const EXT_WARRANTY_LABEL_CONTROL As String = "lblExtWarranty"
        Private Const MFG_COVERAGE_EXT_ID_CONTROL As String = "lblMfgCoverageExtID"

        Public Const PAGETITLE As String = "EXT_WARRANTY"
        Public Const PAGETAB As String = "TABLES"
#End Region

        Private Class PageStatus

            Public Sub New()
                pageIndex = 0
                pageCount = 0
            End Sub

        End Class

#Region "Page Return Type"
        Public Class ReturnType
            Public mfgCoverageId As Guid
            Public RiskType As String
            Public Manufacturer As String
            Public Model As String

            Public Sub New(ByVal _mfgCoverageId As Guid, ByVal _riskType As String, ByVal _Manafacturer As String, ByVal _Model As String)
                Me.mfgCoverageId = _mfgCoverageId
                Me.RiskType = _riskType
                Me.Manufacturer = _Manafacturer
                Me.Model = _Model
            End Sub
        End Class
#End Region

#Region "Member Variables"

        Private Shared pageIndex As Integer
        Private Shared pageCount As Integer
#End Region

#Region "Properties"

        Public ReadOnly Property IsEditing() As Boolean
            Get
                IsEditing = (Me.Grid.EditIndex > NO_ROW_SELECTED_INDEX)
            End Get
        End Property

        Private WriteOnly Property EnableControls() As Boolean
            Set(ByVal Value As Boolean)
                ControlMgr.SetEnableControl(Me, Me.NewButton_WRITE, Value)
            End Set
        End Property

#End Region

#Region "Page State"
        Class MyState
            Public PageIndex As Integer = 0
            Public MyBO As MfgCoverageExt
            Public MfgCoverageExtId As Guid
            Public MfgCoverageId As Guid
            Public RiskType As String
            Public Manufacturer As String
            Public Model As String
            Public IsGridVisible As Boolean
            Public IsAfterSave As Boolean
            Public IsEditMode As Boolean
            Public AddingNewRow As Boolean
            Public Canceling As Boolean
            Public searchDV As DataView = Nothing
            Public YESNOdv As DataView = Nothing
            Public editRowIndex As Integer
            Public selectedPageSize As Integer = DEFAULT_PAGE_SIZE
            Public bnoRow As Boolean = False
            Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
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

#Region "Button Click Handlers"

        Private Sub NewButton_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewButton_WRITE.Click

            Try
                Me.State.IsEditMode = True
                Me.State.IsGridVisible = True
                Me.State.AddingNewRow = True
                AddNew()
                SetButtonsState()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try

        End Sub

        Private Sub SaveButton_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveButton_WRITE.Click

            Try
                AssignBOFromSelectedRecord()
                If (Me.State.MyBO.IsDirty) Then
                    Me.State.MyBO.Save()
                    Me.State.IsAfterSave = True
                    Me.State.AddingNewRow = False
                    Me.AddInfoMsg(Me.MSG_RECORD_SAVED_OK)
                    Me.State.searchDV = Nothing
                    Me.ReturnFromEditing()
                Else
                    Me.AddInfoMsg(Me.MSG_RECORD_NOT_SAVED)
                    Me.ReturnFromEditing()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try

        End Sub

        Private Sub CancelButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CancelButton.Click

            Try
                Me.Grid.SelectedIndex = Me.NO_ITEM_SELECTED_INDEX
                Me.State.Canceling = True
                If (Me.State.AddingNewRow) Then
                    Me.State.AddingNewRow = False
                    Me.State.searchDV = Nothing
                End If
                ReturnFromEditing()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try

        End Sub

        Private Sub BackButton_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BackButton_WRITE.Click

            Try
                Me.ReturnToCallingPage(New ReturnType(Me.State.MfgCoverageId, Me.State.RiskType, Me.State.Manufacturer, Me.State.Model))
            Catch ex As Threading.ThreadAbortException
            End Try

        End Sub

#End Region

#Region "Private Methods"

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            'Put user code to initialize the page here
            Try
                ErrControllerMaster.Clear_Hide()

                Me.SetFormTitle(PAGETITLE)
                Me.SetFormTab(PAGETAB)

                If Not Page.IsPostBack Then
                    Me.SortDirection = MfgCoverageExt.COL_DEALER_NAME
                    Me.SetGridItemStyleColor(Grid)
                    Me.State.PageIndex = 0
                    If Me.State.MyBO Is Nothing Then
                        Me.State.MyBO = New MfgCoverageExt
                    End If
                    SetButtonsState()
                    Me.TranslateGridHeader(Me.Grid)
                    Me.TranslateGridControls(Me.Grid)
                    PopulateGrid()
                Else
                    CheckIfComingFromDeleteConfirm()
                End If
                BindBoPropertiesToGridHeaders()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
            Me.ShowMissingTranslations(ErrControllerMaster)
        End Sub

        Private Sub PopulateGrid()
            Dim dv As DataView

            Try
                If (Me.State.searchDV Is Nothing) Then
                    Me.State.searchDV = GetDV()
                End If
                Me.State.searchDV.Sort = Me.SortDirection
                Me.State.IsGridVisible = True

                If (Me.State.IsAfterSave) Then
                    Me.State.IsAfterSave = False
                    Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.MfgCoverageExtId, Me.Grid, Me.State.PageIndex)
                ElseIf (Me.State.IsEditMode) Then
                    Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.MfgCoverageExtId, Me.Grid, Me.State.PageIndex, Me.State.IsEditMode)
                Else
                    Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Guid.Empty, Me.Grid, Me.State.PageIndex)
                End If

                Me.Grid.AutoGenerateColumns = False
                Me.SortAndBindGrid()

                If Me.State.searchDV.Count = 0 Then
                    Me.trPageSize.Visible = False
                Else
                    Me.trPageSize.Visible = True
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try

        End Sub

        Protected Sub CheckIfComingFromDeleteConfirm()
            Dim confResponse As String = Me.HiddenDeletePromptResponse.Value
            If Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_YES Then
                If Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete Then
                    DoDelete()
                End If
                Select Case Me.State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Delete
                End Select
            ElseIf Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_NO Then
                Select Case Me.State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Delete
                End Select
            End If
            'Clean after consuming the action
            Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
            Me.HiddenDeletePromptResponse.Value = ""
        End Sub

        Private Sub DoDelete()
            Me.State.MyBO = New MfgCoverageExt(Me.State.MfgCoverageExtId)

            Try
                Me.State.MyBO.Delete()
                Me.State.MyBO.Save()
            Catch ex As Exception
                Me.State.MyBO.RejectChanges()
                Throw ex
            End Try

            Me.State.PageIndex = Grid.PageIndex

            'Set the IsAfterSave flag to TRUE so that the Paging logic gets invoked
            Me.State.IsAfterSave = True
            Me.State.searchDV = Nothing
            PopulateGrid()
            Me.State.PageIndex = Grid.PageIndex
        End Sub

        Private Function GetDV() As DataView

            Dim dv As DataView

            Me.State.searchDV = GetGridDataView()
            Me.State.searchDV.Sort = Grid.DataMember()

            Return (Me.State.searchDV)

        End Function

        Private Function GetGridDataView() As DataView

            With State
                Return (MfgCoverageExt.LoadList(Me.State.MfgCoverageId))
            End With

        End Function

        Private Sub PopulateDealerDropdown(ByVal listCtl As System.Web.UI.WebControls.DropDownList)

            Try
                'Dim dealerDv As DataView = MfgCoverageExt.GetAvailableDealers(Me.State.MfgCoverageId, Me.State.MyBO.DealerId) 'ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                '  BindListControlToDataView(listCtl, dealerDv, "DEALER_NAME", "DEALER_ID", True) 'DealerByCompanyGroupMfgCoverage
                Dim listcontext As ListContext = New ListContext()
                listcontext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                listcontext.DealerId = Me.State.MyBO.DealerId
                listcontext.ManufacturerId = Me.State.MfgCoverageId
                Dim compLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.DealerByCompanyGroupMfgCoverage, Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
                listCtl.Populate(compLkl, New PopulateOptions() With
                     {
                    .AddBlankItem = True
                     })
            Catch ex As Exception
                Me.ErrControllerMaster.AddErrorAndShow(ex.Message)
                EnableControls = False
                Exit Sub
            End Try


        End Sub

        Private Sub AddNew()

            If Me.State.searchDV Is Nothing Then
                Me.State.searchDV = GetGridDataView()
            End If


            Me.State.MyBO = New MfgCoverageExt
            Me.State.MfgCoverageExtId = Me.State.MyBO.Id

            Me.State.searchDV = Me.State.MyBO.GetNewDataViewRow(Me.State.searchDV, Me.State.MfgCoverageExtId, Me.State.MyBO)
            Grid.DataSource = Me.State.searchDV

            Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.MfgCoverageExtId, Me.Grid, Me.State.PageIndex, Me.State.IsEditMode)

            Me.State.bnoRow = False
            Grid.DataBind()

            Me.State.PageIndex = Grid.PageIndex

            SetGridControls(Me.Grid, False)

            'Set focus on the Dealer Dropdown for the EditItemIndex row
            Me.SetFocusOnEditableFieldInGrid(Me.Grid, Me.DEALER_COL, Me.DEALER_DDL_CONTROL, Me.Grid.EditIndex)

            Me.AssignSelectedRecordFromBO()

            'Me.TranslateGridControls(Grid)
            Me.SetButtonsState()
            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Grid)
        End Sub

        Private Sub SortAndBindGrid()
            Me.State.PageIndex = Me.Grid.PageIndex

            If (Me.State.searchDV.Count = 0) Then

                Me.State.bnoRow = True
                CreateHeaderForEmptyGrid(Grid, Me.SortDirection)
            Else
                Me.State.bnoRow = False
                Me.Grid.Enabled = True
                Me.Grid.DataSource = Me.State.searchDV
                HighLightSortColumn(Grid, Me.SortDirection)
                Me.Grid.DataBind()
            End If

            If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True

            ControlMgr.SetVisibleControl(Me, Grid, Me.State.IsGridVisible)

            Session("recCount") = Me.State.searchDV.Count

            If Me.Grid.Visible Then
                If (Me.State.AddingNewRow) Then
                    Me.lblRecordCount.Text = (Me.State.searchDV.Count - 1) & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                Else
                    Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            End If
            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Grid)
        End Sub

        Private Sub AssignBOFromSelectedRecord()

            Dim extWarr As Long

            Try
                With Me.State.MyBO
                    .MfgCoverageId = Me.State.MfgCoverageId
                    .DealerId = Me.GetGuidFromString(CType(Me.Grid.Rows(Me.Grid.EditIndex).Cells(Me.DEALER_COL).FindControl(Me.DEALER_DDL_CONTROL), DropDownList).SelectedValue())
                    If Long.TryParse(CType(Me.Grid.Rows(Me.Grid.EditIndex).Cells(Me.EXT_WARRANTY_COL).FindControl(Me.EXT_WARRANTY_CONTROL), TextBox).Text, extWarr) Then
                        .ExtWarranty = extWarr
                    Else
                        .ExtWarranty = 0
                    End If
                End With
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try

        End Sub

        Private Sub AssignSelectedRecordFromBO()

            Dim gridRowIdx As Integer = Me.Grid.EditIndex
            Try
                With Me.State.MyBO
                    If Not .ExtWarranty Is Nothing Then
                        CType(Me.Grid.Rows(gridRowIdx).Cells(Me.EXT_WARRANTY_COL).FindControl(Me.EXT_WARRANTY_CONTROL), TextBox).Text = .ExtWarranty.ToString
                    End If

                    Dim dealerLst As DropDownList = CType(Grid.Rows(Grid.EditIndex).Cells(Me.DEALER_COL).FindControl(Me.DEALER_DDL_CONTROL), DropDownList)

                    PopulateDealerDropdown(dealerLst)
                    Me.SetSelectedItem(dealerLst, .DealerId)

                    CType(Me.Grid.Rows(gridRowIdx).Cells(Me.MFG_COVERAGE_EXT_ID_COL).FindControl(Me.MFG_COVERAGE_EXT_ID_CONTROL), Label).Text = .Id.ToString
                End With
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try

        End Sub

        Private Sub ReturnFromEditing()

            Grid.EditIndex = NO_ROW_SELECTED_INDEX

            If Me.Grid.PageCount = 0 Then
                'if returning to the "1st time in" screen
                ControlMgr.SetVisibleControl(Me, Grid, False)
            Else
                ControlMgr.SetVisibleControl(Me, Grid, True)
            End If

            Me.State.IsEditMode = False
            Me.PopulateGrid()
            Me.State.PageIndex = Grid.PageIndex
            SetButtonsState()

        End Sub

        Private Sub SetButtonsState()
            If (Me.State.IsEditMode) Then
                ControlMgr.SetVisibleControl(Me, SaveButton_WRITE, True)
                ControlMgr.SetVisibleControl(Me, CancelButton, True)
                ControlMgr.SetVisibleControl(Me, NewButton_WRITE, False)
                ControlMgr.SetVisibleControl(Me, BackButton_WRITE, False)
                Me.MenuEnabled = False
                If (Me.cboPageSize.Visible) Then
                    ControlMgr.SetEnableControl(Me, cboPageSize, False)
                End If
            Else
                ControlMgr.SetVisibleControl(Me, SaveButton_WRITE, False)
                ControlMgr.SetVisibleControl(Me, CancelButton, False)
                ControlMgr.SetVisibleControl(Me, NewButton_WRITE, True)
                ControlMgr.SetVisibleControl(Me, BackButton_WRITE, True)
                Me.MenuEnabled = True
                If (Me.cboPageSize.Visible) Then
                    ControlMgr.SetEnableControl(Me, cboPageSize, True)
                End If
            End If
        End Sub

        Private Sub Grid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                Grid.PageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                Me.State.selectedPageSize = CType(cboPageSize.SelectedValue, Integer)
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
            Try
                If Not Me.CallingParameters Is Nothing Then

                    Dim _ret As ReturnType = CType(Me.CallingParameters, ReturnType)

                    Me.State.MfgCoverageId = _ret.mfgCoverageId
                    Me.State.Model = _ret.Model
                    Me.State.RiskType = _ret.RiskType
                    Me.State.Manufacturer = _ret.Manufacturer

                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

#End Region

#Region " Datagrid Related "
        Public Property SortDirection() As String
            Get
                Return ViewState("SortDirection").ToString
            End Get
            Set(ByVal value As String)
                ViewState("SortDirection") = value
            End Set
        End Property

        Private Sub Grid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging

            Try
                If (Not (Me.State.IsEditMode)) Then
                    Me.State.PageIndex = e.NewPageIndex
                    Me.Grid.PageIndex = Me.State.PageIndex
                    Me.PopulateGrid()
                    Me.Grid.SelectedIndex = Me.NO_ITEM_SELECTED_INDEX
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try

        End Sub

        Protected Sub ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)

            Try
                Dim index As Integer

                If (e.CommandName = Me.EDIT_COMMAND) Then
                    index = CInt(e.CommandArgument)
                    Me.State.IsEditMode = True
                    Me.State.MfgCoverageExtId = New Guid(CType(Me.Grid.Rows(index).Cells(Me.MFG_COVERAGE_EXT_ID_COL).FindControl(Me.MFG_COVERAGE_EXT_ID_CONTROL), Label).Text)
                    Me.State.MyBO = New MfgCoverageExt(Me.State.MfgCoverageExtId)

                    Me.PopulateGrid()

                    Me.State.PageIndex = Grid.PageIndex

                    'Disable all Edit and Delete icon buttons on the Grid
                    SetGridControls(Me.Grid, False)

                    'Set focus on the Dealer dropdown for the EditItemIndex row
                    Me.SetFocusOnEditableFieldInGrid(Me.Grid, Me.DEALER_COL, Me.DEALER_DDL_CONTROL, index)

                    Me.AssignSelectedRecordFromBO()

                    Me.SetButtonsState()

                ElseIf (e.CommandName = Me.DELETE_COMMAND) Then
                    index = CInt(e.CommandArgument)

                    'Clear the SelectedItemStyle to remove the highlight from the previously saved row
                    Grid.SelectedIndex = Me.NO_ROW_SELECTED_INDEX

                    Me.State.MfgCoverageExtId = New Guid(CType(Me.Grid.Rows(index).Cells(Me.MFG_COVERAGE_EXT_ID_COL).FindControl(Me.MFG_COVERAGE_EXT_ID_CONTROL), Label).Text)
                    Me.DisplayMessage(Message.DELETE_RECORD_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenDeletePromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try

        End Sub

        Private Sub Grid_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
            Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

            If Not dvRow Is Nothing And Not Me.State.bnoRow Then

                If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                    CType(e.Row.Cells(Me.MFG_COVERAGE_EXT_ID_COL).FindControl(Me.MFG_COVERAGE_EXT_ID_CONTROL), Label).Text = GetGuidStringFromByteArray(CType(dvRow(MfgCoverageExt.COL_MFG_COVERAGE_EXT_ID), Byte()))

                    If (Me.State.IsEditMode = True _
                            AndAlso Me.State.MfgCoverageExtId.ToString.Equals(GetGuidStringFromByteArray(CType(dvRow(MfgCoverageExt.COL_MFG_COVERAGE_EXT_ID), Byte())))) Then

                        CType(e.Row.Cells(Me.EXT_WARRANTY_COL).FindControl(Me.EXT_WARRANTY_CONTROL), TextBox).Text = dvRow(MfgCoverageExt.COL_EXT_WARRANTY).ToString

                        Dim dlrId() As Byte
                        If dvRow(MfgCoverageExt.COL_DEALER_ID) Is DBNull.Value Then
                            dlrId = Nothing
                        Else
                            dlrId = CType(dvRow(MfgCoverageExt.COL_DEALER_ID), Byte())
                        End If
                        Dim dvDealer As DataView = MfgCoverageExt.GetAvailableDealers(Me.State.MfgCoverageId, CType(If(dlrId Is Nothing, Guid.Empty, GuidControl.ByteArrayToGuid(dlrId)), Guid))
                        ' BindListControlToDataView(CType(e.Row.Cells(Me.DEALER_COL).FindControl(Me.DEALER_DDL_CONTROL), DropDownList), dvDealer, MfgCoverageExt.COL_DEALER_NAME, MfgCoverageExt.COL_DEALER_ID, True)
                        Dim listcontext As ListContext = New ListContext()
                        listcontext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                        listcontext.DealerId = CType(If(dlrId Is Nothing, Guid.Empty, GuidControl.ByteArrayToGuid(dlrId)), Guid)
                        listcontext.ManufacturerId = Me.State.MfgCoverageId
                        Dim compLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.DealerByCompanyGroupMfgCoverage, Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
                        CType(e.Row.Cells(Me.DEALER_COL).FindControl(Me.DEALER_DDL_CONTROL), DropDownList).Populate(compLkl, New PopulateOptions() With
                     {
                    .AddBlankItem = True
                     })
                        If Not dvRow(MfgCoverageExt.COL_DEALER_ID) Is DBNull.Value Then
                            Me.SetSelectedItem(CType(e.Row.Cells(Me.DEALER_COL).FindControl(Me.DEALER_DDL_CONTROL), DropDownList), GuidControl.ByteArrayToGuid(dlrId).ToString)
                        End If

                    Else

                        CType(e.Row.Cells(Me.DEALER_COL).FindControl(Me.DEALER_LABEL_CONTROL), Label).Text = dvRow(MfgCoverageExt.COL_DEALER_NAME).ToString
                        CType(e.Row.Cells(Me.EXT_WARRANTY_COL).FindControl(Me.EXT_WARRANTY_LABEL_CONTROL), Label).Text = dvRow(MfgCoverageExt.COL_EXT_WARRANTY).ToString

                    End If
                End If
            End If
        End Sub


        Protected Sub ItemBound(ByVal source As Object, ByVal e As GridViewRowEventArgs) Handles Grid.RowDataBound

            Try
                If Not Me.State.bnoRow Then
                    BaseItemBound(source, e)
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Public Sub ItemCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
            BaseItemCreated(sender, e)
        End Sub

        Protected Sub BindBoPropertiesToGridHeaders()
            Me.BindBOPropertyToGridHeader(Me.State.MyBO, "ExtWarranty", Me.Grid.Columns(Me.EXT_WARRANTY_COL))
            Me.BindBOPropertyToGridHeader(Me.State.MyBO, "DealerId", Me.Grid.Columns(Me.DEALER_COL))
            Me.ClearGridViewHeadersAndLabelsErrSign()
        End Sub

        Private Sub SetFocusOnEditableFieldInGrid(ByVal grid As GridView, ByVal cellPosition As Integer, ByVal controlName As String, ByVal itemIndex As Integer)
            'Set focus on the Description TextBox for the EditItemIndex row
            Dim ctl As DropDownList = CType(grid.Rows(itemIndex).Cells(cellPosition).FindControl(controlName), DropDownList)
            SetFocus(ctl)
        End Sub

        Private Sub Grid_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting
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
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub
#End Region

    End Class

End Namespace