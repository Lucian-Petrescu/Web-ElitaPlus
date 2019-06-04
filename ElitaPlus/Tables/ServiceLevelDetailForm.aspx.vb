Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Partial Class ServiceLevelDetailForm
    Inherits ElitaPlusSearchPage
#Region " Web Form Designer Generated Code "

    Protected WithEvents ErrorCtrl As ErrorController
    'Protected WithEvents Dealer As System.Web.UI.WebControls.Label

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

#Region "Constants"
    Public Const GRID_COL_EDIT_IDX As Integer = 0
    Private Const GRID_COL_DELETE_IDX As Integer = 1
    Public Const GRID_COL_SERVICE_LEVEL_CODE_IDX As Integer = 2
    Public Const GRID_COL_SERVICE_LEVEL_DESCRIPTION_IDX As Integer = 3
    Public Const GRID_COL_RISK_TYPE_IDX As Integer = 4
    Public Const GRID_COL_COST_TYPE_IDX As Integer = 5
    Public Const GRID_COL_SERVICE_LEVEL_COST_IDX As Integer = 6
    Public Const GRID_COL_EFFECTIVE_DATE_IDX As Integer = 7
    Public Const GRID_COL_EXPIRATION_DATE_IDX As Integer = 8
    Public Const GRID_COL_SERVICE_LEVEL_DETAIL_ID_IDX As Integer = 9

    Public Const GRID_TOTAL_COLUMNS As Integer = 10
    Private Const NOTHING_SELECTED As String = "00000000-0000-0000-0000-000000000000"
    Public Const URL As String = "ServiceLevelDetailForm.aspx"
    Private Const ServiceLevelDetailForm As String = "ServiceLevelDetailForm.aspx"


    Private Const MSG_CONFIRM_PROMPT As String = "MSG_CONFIRM_PROMPT"
    Private Const MSG_RECORD_DELETED_OK As String = "MSG_RECORD_DELETED_OK"
    Private Const MSG_RECORD_SAVED_OK As String = "MSG_RECORD_SAVED_OK"
    Private Const MSG_RECORD_NOT_SAVED As String = "MSG_RECORD_NOT_SAVED"

#End Region

#Region "Page Return Type"
    Public Class ReturnType
        Public LastOperation As DetailPageCommand
        Public EditingBo As ServiceLevelGroup
        Public HasDataChanged As Boolean
        Public Sub New(ByVal LastOp As DetailPageCommand, ByVal curEditingBo As ServiceLevelGroup, ByVal hasDataChanged As Boolean)
            Me.LastOperation = LastOp
            Me.EditingBo = curEditingBo
            Me.HasDataChanged = hasDataChanged
        End Sub
    End Class
#End Region

#Region "Page State"
    Private IsReturningFromChild As Boolean = False

    ' This class keeps the current state for the search page.
    Class MyState
        Public PageIndex As Integer = 0
        Public DescriptionMask As String
        Public CodeMask As String
        Public DealerId As Guid = Guid.Empty
        Public ServiceLevelDetailId As Guid = Guid.Empty
        Public IsGridVisible As Boolean = True
        Public searchDV As ServiceLevelDetail.ServiceLevelDetailSearchDV = Nothing
        Public selectedPageSize As Integer = DEFAULT_PAGE_SIZE
        Public SortExpression As String = ServiceLevelDetail.ServiceLevelDetailSearchDV.COL_SERVICE_LEVEL_CODE
        Public HasDataChanged As Boolean
        Public bnoRow As Boolean = False
        Public IsEditMode As Boolean
        Public myBO As ServiceLevelGroup
        Public myChildBO As ServiceLevelDetail
        Public tempBO As ServiceLevelDetail
        Public IsGridAddNew As Boolean = False
        Public Canceling As Boolean
        Public AddingNewRow As Boolean
        Public IsAfterSave As Boolean
        Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
        Public DateMask As String = String.Empty

        Public GridFormEditIndex As Integer = NO_ITEM_SELECTED_INDEX
        Public IsFormValueChanged As Boolean = False

        Sub New()

        End Sub

    End Class

    Public Sub New()
        MyBase.New(New MyState)
    End Sub

    Protected Shadows ReadOnly Property State() As MyState
        Get
            Return CType(MyBase.State, MyState)
        End Get
    End Property

    Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
        Try
            If Not Me.CallingParameters Is Nothing Then
                'Get the id from the parent
                Me.State.myBO = New ServiceLevelGroup(CType(Me.CallingParameters, Guid))
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try

    End Sub

#End Region
#Region "Properties"





    Public ReadOnly Property IsGridFormInEditMode() As Boolean
        Get
            Return Me.Grid.EditIndex > Me.NO_ITEM_SELECTED_INDEX
        End Get
    End Property

#End Region
#Region "Page_Events"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Me.ErrorCtrl.Clear_Hide()


        Try
            If Not Me.IsPostBack Then

                ControlMgr.SetVisibleControl(Me, trPageSize, False)
                Me.AddCalendar(Me.btnDate, Me.txtDate)
                Me.SortDirection = Me.State.SortExpression
                PopulateHeaderTextbox()
                Me.txtDate.Text = Date.Now.ToString("dd-MMM-yy", LocalizationMgr.CurrentFormatProvider)
                Me.State.DateMask = DateHelper.GetFormattedDate(Me.txtDate.Text, "dd-MMM-yy").ToString("yyyyMMdd")
                SetButtonsState()
                If Me.State.myBO Is Nothing Then
                    Me.State.myBO = New ServiceLevelGroup
                End If
                'If Not Me.State.IsGridVisible Then
                If Not (Me.State.selectedPageSize = DEFAULT_PAGE_SIZE) Then
                    cboPageSize.SelectedValue = CType(Me.State.selectedPageSize, String)
                    Grid.PageSize = Me.State.selectedPageSize
                End If
                Me.TranslateGridHeader(Me.Grid)
                Me.TranslateGridControls(Me.Grid)
                Me.PopulateGrid()
                'End If
                Me.SetGridItemStyleColor(Me.Grid)

            End If
            'Me.BindBoPropertiesToGridHeaders()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
        Me.ShowMissingTranslations(Me.ErrorCtrl)
    End Sub



#End Region

#Region "Controlling Logic"

    Public Sub PopulateGrid()
        If ((Me.State.searchDV Is Nothing) OrElse (Me.State.HasDataChanged)) Then
            Me.State.searchDV = ServiceLevelDetail.getList(Me.State.myBO.Id, Me.TextboxServiceLevelCode.Text.ToUpper, Me.TextboxServiceLevelDesc.Text.ToUpper, Me.State.DateMask)

        End If
        If Not (Me.State.searchDV Is Nothing) Then

            If Me.SortDirection.ToUpper.Contains("SERVICE_LEVEL_CODE") Then
                Me.SortDirection = Me.SortDirection.ToUpper.Replace("SERVICE_LEVEL_CODE", "CODE")
            ElseIf Me.SortDirection.ToUpper.Contains("SERVICE_LEVEL_DESCRIPTION") Then
                Me.SortDirection = Me.SortDirection.ToUpper.Replace("SERVICE_LEVEL_DESCRIPTION", "DESCRIPTION")
            End If

            Me.State.searchDV.Sort = Me.SortDirection

            Me.Grid.AutoGenerateColumns = False

            If (Me.State.IsAfterSave) Then
                Me.State.IsAfterSave = False
                SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.ServiceLevelDetailId, Me.Grid, Me.State.PageIndex)
            ElseIf (Me.State.IsEditMode) Then
                SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.ServiceLevelDetailId, Me.Grid, Me.State.PageIndex, Me.State.IsEditMode)
            Else
                SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Guid.Empty, Me.Grid, Me.State.PageIndex, Me.State.IsEditMode)
            End If

            Me.SortAndBindGrid()

            'Save the current record to a BO if editing detail record with effective date in the past
            If Not Me.State.IsGridAddNew AndAlso Me.State.IsEditMode Then
                PopulateBOFromForm()
                If (Not State.myChildBO.EffectiveDate Is Nothing) AndAlso (CDate(State.myChildBO.EffectiveDate) < Date.Now) Then
                    Me.State.tempBO = New ServiceLevelDetail
                    Me.State.tempBO.CopyFrom(State.myChildBO)
                    Me.State.tempBO.ExpirationDate = CType(Date.Now.AddDays(-1), DateType)
                End If
            End If
        End If
    End Sub





    Private Sub SortAndBindGrid()



        If (Me.State.searchDV.Count = 0) Then
            Me.State.bnoRow = True
            CreateHeaderForEmptyGrid(Grid, Me.SortDirection)
            ControlMgr.SetVisibleControl(Me, Grid, Me.State.IsGridVisible)
            ControlMgr.SetVisibleControl(Me, trPageSize, Me.Grid.Visible)
            Grid.PagerSettings.Visible = True
            If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True
        Else
            Me.State.bnoRow = False
            Me.Grid.Enabled = True
            Me.Grid.DataSource = Me.State.searchDV
            HighLightSortColumn(Grid, Me.SortDirection)
            Me.Grid.DataBind()
            ControlMgr.SetVisibleControl(Me, Grid, Me.State.IsGridVisible)
            ControlMgr.SetVisibleControl(Me, trPageSize, Me.Grid.Visible)
            If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True
        End If

        If Not Grid.BottomPagerRow Is Nothing AndAlso Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True


        If Me.State.searchDV.Count > 0 Then
            If Me.Grid.Visible Then
                If (Me.State.AddingNewRow) Then
                    Me.lblRecordCount.Text = (Me.State.searchDV.Count - 1) & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                Else
                    Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            Else
                If Me.Grid.Visible Then
                    Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            End If
        Else
            If Me.Grid.Visible Then
                Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If
        End If

        ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Grid)
    End Sub

#End Region

#Region " GridView Related "
    Public Property SortDirection() As String
        Get
            Return ViewState("SortDirection").ToString
        End Get
        Set(ByVal value As String)
            ViewState("SortDirection") = value
        End Set
    End Property

    'The Binding Logic is here
    Private Sub Grid_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
        Try
            Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
            Dim txt As TextBox




            If Not dvRow Is Nothing And Not Me.State.bnoRow Then
                If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                    CType(e.Row.Cells(Me.GRID_COL_SERVICE_LEVEL_DETAIL_ID_IDX).FindControl("ServiceLevelDetailIdLabel"), Label).Text = GetGuidStringFromByteArray(CType(dvRow(ServiceLevelDetail.ServiceLevelDetailSearchDV.COL_SERVICE_LEVEL_DETAIL_ID), Byte()))

                    If (Me.State.IsEditMode = True AndAlso Me.State.ServiceLevelDetailId.ToString.Equals(GetGuidStringFromByteArray(CType(dvRow(ServiceLevelDetail.ServiceLevelDetailSearchDV.COL_SERVICE_LEVEL_DETAIL_ID), Byte())))) Then


                        CType(e.Row.Cells(Me.GRID_COL_SERVICE_LEVEL_CODE_IDX).FindControl("ServceLevelCodeTextBox"), TextBox).Text = dvRow(ServiceLevelDetail.ServiceLevelDetailSearchDV.COL_SERVICE_LEVEL_CODE).ToString
                        CType(e.Row.Cells(Me.GRID_COL_SERVICE_LEVEL_DESCRIPTION_IDX).FindControl("ServceLevelDescTextBox"), TextBox).Text = dvRow(ServiceLevelDetail.ServiceLevelDetailSearchDV.COL_SERVICE_LEVEL_DESCRIPTION).ToString
                        Dim RiskTypeList As DropDownList = CType(e.Row.Cells(Me.GRID_COL_RISK_TYPE_IDX).FindControl("RiskTypeDropdown"), DropDownList)
                        Dim CostTypeList As DropDownList = CType(e.Row.Cells(Me.GRID_COL_COST_TYPE_IDX).FindControl("CostTypeDropdown"), DropDownList)
                        PopulateDropdown(RiskTypeList, CostTypeList)
                        If Not dvRow(ServiceLevelDetail.ServiceLevelDetailSearchDV.COL_RISK_TYPE_ID) Is System.DBNull.Value Then
                            Me.SetSelectedItem(RiskTypeList, New Guid(CType(dvRow(ServiceLevelDetail.ServiceLevelDetailSearchDV.COL_RISK_TYPE_ID), Byte())))
                        Else
                            Me.SetSelectedItem(RiskTypeList, Guid.Empty)
                        End If
                        If Not dvRow(ServiceLevelDetail.ServiceLevelDetailSearchDV.COL_COST_TYPE_ID) Is System.DBNull.Value Then
                            Me.SetSelectedItem(CostTypeList, New Guid(CType(dvRow(ServiceLevelDetail.ServiceLevelDetailSearchDV.COL_COST_TYPE_ID), Byte())))
                        Else
                            Me.SetSelectedItem(CostTypeList, Guid.Empty)
                        End If

                        If Not dvRow(ServiceLevelDetail.ServiceLevelDetailSearchDV.COL_SERVICE_LEVEL_COST) Is System.DBNull.Value Then
                            CType(e.Row.Cells(Me.GRID_COL_SERVICE_LEVEL_COST_IDX).FindControl("ServiceLevelCostTextBox"), TextBox).Text = GetAmountFormattedDoubleString(dvRow(ServiceLevelDetail.ServiceLevelDetailSearchDV.COL_SERVICE_LEVEL_COST).ToString)
                        Else
                            CType(e.Row.Cells(Me.GRID_COL_SERVICE_LEVEL_COST_IDX).FindControl("ServiceLevelCostTextBox"), TextBox).Text = "0.00"
                        End If
                        If Not dvRow(ServiceLevelDetail.ServiceLevelDetailSearchDV.COL_EFFECTIVE_DATE) Is System.DBNull.Value Then
                            CType(e.Row.Cells(Me.GRID_COL_EFFECTIVE_DATE_IDX).FindControl("EffectiveDateTextBox"), TextBox).Text = Me.GetDateFormattedString(CType(dvRow(ServiceLevelDetail.ServiceLevelDetailSearchDV.COL_EFFECTIVE_DATE), Date))
                        Else
                            CType(e.Row.Cells(Me.GRID_COL_EFFECTIVE_DATE_IDX).FindControl("EffectiveDateTextBox"), TextBox).Text = String.Empty
                        End If
                        If Not dvRow(ServiceLevelDetail.ServiceLevelDetailSearchDV.COL_EXPIRATION_DATE) Is System.DBNull.Value Then
                            CType(e.Row.Cells(Me.GRID_COL_EXPIRATION_DATE_IDX).FindControl("ExpirationDateTextBox"), TextBox).Text = Me.GetDateFormattedString(CType(dvRow(ServiceLevelDetail.ServiceLevelDetailSearchDV.COL_EXPIRATION_DATE), Date))
                        Else
                            CType(e.Row.Cells(Me.GRID_COL_EXPIRATION_DATE_IDX).FindControl("ExpirationDateTextBox"), TextBox).Text = String.Empty
                        End If

                        Dim btnEffectiveDate As ImageButton = CType(e.Row.Cells(Me.GRID_COL_EFFECTIVE_DATE_IDX).FindControl("moEffectiveDateImageGrid"), ImageButton)
                        Dim txtEffectiveDate As TextBox = CType(e.Row.Cells(Me.GRID_COL_EFFECTIVE_DATE_IDX).FindControl("EffectiveDateTextBox"), TextBox)
                        Me.AddCalendar(btnEffectiveDate, txtEffectiveDate)

                        Dim btnExpirationDate As ImageButton = CType(e.Row.Cells(Me.GRID_COL_EXPIRATION_DATE_IDX).FindControl("moExpirationDateImageGrid"), ImageButton)
                        Dim txtExpirationDate As TextBox = CType(e.Row.Cells(Me.GRID_COL_EXPIRATION_DATE_IDX).FindControl("ExpirationDateTextBox"), TextBox)
                        Me.AddCalendar(btnExpirationDate, txtExpirationDate)



                    Else
                        CType(e.Row.Cells(Me.GRID_COL_SERVICE_LEVEL_CODE_IDX).FindControl("ServceLevelCodeLabel"), Label).Text = dvRow(ServiceLevelDetail.ServiceLevelDetailSearchDV.COL_SERVICE_LEVEL_CODE).ToString
                        CType(e.Row.Cells(Me.GRID_COL_SERVICE_LEVEL_DESCRIPTION_IDX).FindControl("ServceLevelDescLabel"), Label).Text = dvRow(ServiceLevelDetail.ServiceLevelDetailSearchDV.COL_SERVICE_LEVEL_DESCRIPTION).ToString
                        CType(e.Row.Cells(Me.GRID_COL_RISK_TYPE_IDX).FindControl("RiskTypeLabel"), Label).Text = dvRow(ServiceLevelDetail.ServiceLevelDetailSearchDV.COL_RISK_TYPE).ToString 'LookupListNew.GetDescriptionFromId("RISK_TYPES", New Guid(GetGuidStringFromByteArray(CType(dvRow(ServiceLevelDetail.ServiceLevelDetailSearchDV.COL_RISK_TYPE_ID), Byte()))))
                        CType(e.Row.Cells(Me.GRID_COL_COST_TYPE_IDX).FindControl("CostTypeLabel"), Label).Text = dvRow(ServiceLevelDetail.ServiceLevelDetailSearchDV.COL_COST_TYPE).ToString 'LookupListNew.GetDescriptionFromId("COST_TYPE", New Guid(GetGuidStringFromByteArray(CType(dvRow(ServiceLevelDetail.ServiceLevelDetailSearchDV.COL_COST_TYPE_ID), Byte()))))
                        CType(e.Row.Cells(Me.GRID_COL_SERVICE_LEVEL_COST_IDX).FindControl("ServiceLevelCostLabel"), Label).Text = GetAmountFormattedDoubleString(dvRow(ServiceLevelDetail.ServiceLevelDetailSearchDV.COL_SERVICE_LEVEL_COST).ToString)
                        If Not dvRow(ServiceLevelDetail.ServiceLevelDetailSearchDV.COL_EFFECTIVE_DATE) Is System.DBNull.Value Then
                            CType(e.Row.Cells(Me.GRID_COL_EFFECTIVE_DATE_IDX).FindControl("EffectiveDateLabel"), Label).Text = Me.GetDateFormattedString(CType(dvRow(ServiceLevelDetail.ServiceLevelDetailSearchDV.COL_EFFECTIVE_DATE), Date))
                            If CType(dvRow(ServiceLevelDetail.ServiceLevelDetailSearchDV.COL_EFFECTIVE_DATE), Date) < Date.Now Then
                                CType(e.Row.Cells(Me.GRID_COL_DELETE_IDX).FindControl("DeleteButton_WRITE"), ImageButton).Visible = False
                            End If
                        End If
                        If Not dvRow(ServiceLevelDetail.ServiceLevelDetailSearchDV.COL_EXPIRATION_DATE) Is System.DBNull.Value Then
                            CType(e.Row.Cells(Me.GRID_COL_EXPIRATION_DATE_IDX).FindControl("ExpirationDateLabel"), Label).Text = Me.GetDateFormattedString(CType(dvRow(ServiceLevelDetail.ServiceLevelDetailSearchDV.COL_EXPIRATION_DATE), Date))
                        End If

                    End If
                End If
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub Grid_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grid.PreRender
        If (Grid.EditIndex <> -1) Then

            Dim editRow As GridViewRow = Grid.Rows(Grid.EditIndex)
            'get the textbox  
            Dim effDateTxtBox As TextBox = CType(editRow.FindControl("EffectiveDateTextBox"), TextBox)
            If (Not effDateTxtBox.Text Is String.Empty) AndAlso (CType(effDateTxtBox.Text, Date) > Date.Now) Then
                Dim expDateTxtBox As TextBox = CType(editRow.FindControl("ExpirationDateTextBox"), TextBox)
                'get the cell where the textbox is located  
                Dim cellEffDate As TableCell = CType(effDateTxtBox.Parent, TableCell)
                cellEffDate.Text = effDateTxtBox.Text

                Dim cellExpDate As TableCell = CType(expDateTxtBox.Parent, TableCell)
                cellExpDate.Text = expDateTxtBox.Text
            End If
        End If
    End Sub

    Private Sub Grid_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Grid.RowCommand
        Try
            'ignore other commands
            Dim index As Integer
            If (e.CommandName = "EditRecord") Then

                index = CInt(e.CommandArgument)

                'Do the Edit here

                'Set the IsEditMode flag to TRUE
                Me.State.IsEditMode = True

                Me.State.ServiceLevelDetailId = New Guid(CType(Grid.Rows(index).Cells(GRID_COL_SERVICE_LEVEL_DETAIL_ID_IDX).FindControl("ServiceLevelDetailIdLabel"), Label).Text)
                Me.State.myChildBO = New ServiceLevelDetail(Me.State.ServiceLevelDetailId)

                Me.PopulateGrid()

                Me.State.PageIndex = Grid.PageIndex

                'Disable all Edit and Delete icon buttons on the Grid
                SetGridControls(Me.Grid, False)

                'Set focus on the Description TextBox for the EditItemIndex row
                'Me.SetFocusOnEditableFieldInGrid(Me.Grid, Me.DESCRIPTION_COL_IDX, Me.DESCRIPTION_CONTROL_NAME, index)

                PopulateFormFromBO()

                Me.SetButtonsState()
            ElseIf (e.CommandName = "DeleteAction") Then
                Dim lblCtrl As Label
                Dim row As GridViewRow = CType(CType(e.CommandSource, Control).Parent.Parent, GridViewRow)
                Dim RowInd As Integer = row.RowIndex
                lblCtrl = CType(Grid.Rows(RowInd).Cells(GRID_COL_SERVICE_LEVEL_DETAIL_ID_IDX).FindControl("ServiceLevelDetailIdLabel"), Label)
                State.ServiceLevelDetailId = New Guid(lblCtrl.Text)
                'lblCtrl = Nothing
                'If e.CommandName = "SelectAction" Then
                '    Grid.EditIndex = RowInd
                '    PopulateForms()
                '    'Disable all Edit and Delete icon buttons on the Grid
                '    SetGridControls(Me.Grid, False)
                'ElseIf e.CommandName = "DeleteAction" Then
                Dim intErrCode As Integer, strErrMsg As String
                ServiceLevelDetail.DeleteServiceLevelDetail(State.ServiceLevelDetailId)
                State.searchDV = Nothing
                PopulateGrid()

                ' End If
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Public Sub RowCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        BaseItemCreated(sender, e)
    End Sub

    Private Sub Grid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            Grid.PageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
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
            Me.State.SortExpression = Me.SortDirection
            Me.State.PageIndex = 0
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub


    Private Sub Grid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
        Try
            Me.State.PageIndex = e.NewPageIndex
            Me.State.DealerId = Guid.Empty
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub


#End Region

#Region " Button Clicks "


    Private Sub moBtnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles moBtnSearch.Click
        Try
            Me.State.PageIndex = 0
            Me.State.DealerId = Guid.Empty
            Me.State.IsGridVisible = True
            Me.State.searchDV = Nothing
            Me.State.HasDataChanged = False

            If txtDate.Text <> "" Then
                Me.State.DateMask = DateHelper.GetDateValue(Me.txtDate.Text).ToString("yyyyMMdd")
            End If

            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub


    Private Sub moBtnClearSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles moBtnClearSearch.Click
        ClearSearchCriteria()
    End Sub
    Private Sub ClearSearchCriteria()

        Try

            TextboxServiceLevelCode.Text = String.Empty
            TextboxServiceLevelDesc.Text = String.Empty
            txtDate.Text = String.Empty
            Me.State.DateMask = String.Empty



        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try

    End Sub


    Private Sub btnAdd_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd_WRITE.Click

        Try
            Me.State.IsEditMode = True
            Me.State.IsGridVisible = True
            Me.State.IsGridAddNew = True
            Me.State.HasDataChanged = True
            Me.State.AddingNewRow = True
            AddNew()
            SetButtonsState()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try

    End Sub

    Private Sub SaveButton_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveButton_WRITE.Click
        Try

            PopulateBOFromForm()

            If Me.State.IsGridAddNew Then
                With State.myChildBO
                    If (Not .EffectiveDate Is Nothing) AndAlso Not (ServiceLevelDetail.IsServiceLevelDetailValid(.ServiceLevelGroupId, .Code, .RiskTypeId, .CostTypeId, CDate(.EffectiveDate))) Then
                        Me.DisplayMessage(Message.MSG_SVC_LVL_DTL_UNIQUE, "", Me.MSG_BTN_OK, Me.MSG_TYPE_ALERT, , True)
                        Exit Sub
                    End If
                End With
            End If

            If (Me.State.myChildBO.IsDirty) Then
                'Insert new record when saving service level detail with effective date in the past
                If Not Me.State.IsGridAddNew AndAlso (Not Me.State.tempBO Is Nothing) Then
                    Me.State.myChildBO.EffectiveDate = CType(Date.Now, DateType)
                    Me.State.myChildBO.Save()
                    Me.State.tempBO.GetNewDataViewRow(Me.State.searchDV, Me.State.tempBO)
                    Me.State.tempBO.Save()
                Else
                    Me.State.myChildBO.Save()
                End If

                Me.State.IsGridAddNew = False
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
            Me.HandleErrors(ex, Me.ErrorCtrl)
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
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try

    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            If Grid.EditIndex <> -1 Then
                Me.PopulateBOFromForm()
            End If
            If Me.State.myBO.IsDirty Then
                'Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO_CANCEL, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
            Else
                Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.myBO, Me.State.HasDataChanged))
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
            'Me.DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", Me.MSG_BTN_YES_NO_CANCEL, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
            Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
            'Me.State.LastErrMsg = Me.ErrorCtrl.Text
        End Try
    End Sub

#End Region

#Region "Helper Functions"

    Private Sub AddNew()
        Me.State.searchDV = GetGridDataView()

        Me.State.myChildBO = New ServiceLevelDetail
        Me.State.ServiceLevelDetailId = Me.State.myChildBO.Id
        Me.State.myChildBO.ServiceLevelGroupId = Me.State.myBO.Id

        Me.State.searchDV = Me.State.myChildBO.GetNewDataViewRow(Me.State.searchDV, Me.State.myChildBO)

        Grid.DataSource = Me.State.searchDV

        Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.ServiceLevelDetailId, Me.Grid, Me.State.PageIndex, Me.State.IsEditMode)

        Me.Grid.AutoGenerateColumns = False


        SortAndBindGrid()
        SetGridControls(Me.Grid, False)


        PopulateFormFromBO()
    End Sub



    Private Sub SetButtonsState()

        If (Me.State.IsEditMode) Then
            ControlMgr.SetVisibleControl(Me, SaveButton_WRITE, True)
            ControlMgr.SetVisibleControl(Me, CancelButton, True)
            ControlMgr.SetVisibleControl(Me, btnAdd_WRITE, False)
            ControlMgr.SetEnableControl(Me, moBtnSearch, False)
            ControlMgr.SetEnableControl(Me, moBtnClearSearch, False)
            Me.MenuEnabled = False
            If (Me.cboPageSize.Visible) Then
                ControlMgr.SetEnableControl(Me, cboPageSize, False)
            End If
        Else
            ControlMgr.SetVisibleControl(Me, SaveButton_WRITE, False)
            ControlMgr.SetVisibleControl(Me, CancelButton, False)
            ControlMgr.SetVisibleControl(Me, btnAdd_WRITE, True)
            ControlMgr.SetEnableControl(Me, moBtnSearch, True)
            ControlMgr.SetEnableControl(Me, moBtnClearSearch, True)
            Me.MenuEnabled = True
            If (Me.cboPageSize.Visible) Then
                ControlMgr.SetEnableControl(Me, Me.cboPageSize, True)
            End If
        End If

    End Sub


    Private Function GetGridDataView() As ServiceLevelDetail.ServiceLevelDetailSearchDV

        With State
            Return (ServiceLevelDetail.getList(Me.State.myBO.Id, Me.TextboxServiceLevelCode.Text.ToUpper, Me.TextboxServiceLevelDesc.Text.ToUpper, Me.State.DateMask))
        End With

    End Function


    Private Sub ReturnFromEditing()

        Grid.EditIndex = NO_ITEM_SELECTED_INDEX

        If Me.Grid.PageCount = 0 Then
            'if returning to the "1st time in" screen
            ControlMgr.SetVisibleControl(Me, Grid, False)
        Else
            ControlMgr.SetVisibleControl(Me, Grid, True)
        End If

        SetGridControls(Grid, True)
        Me.State.IsEditMode = False
        Me.PopulateGrid()
        Me.State.PageIndex = Grid.PageIndex
        SetButtonsState()

    End Sub

    Private Sub PopulateDropdown(ByVal RiskTypeList As DropDownList, ByVal CostTypeList As DropDownList)
        Try
            '  Me.BindListControlToDataView(RiskTypeList, LookupListNew.GetRiskTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id))
            Dim listcontext As ListContext = New ListContext()
            listcontext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
            Dim riskTypeLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("RiskTypeByCompanyGroup", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
            RiskTypeList.Populate(riskTypeLkl, New PopulateOptions() With
             {
               .AddBlankItem = True
             })
            'Me.BindListControlToDataView(CostTypeList, LookupListNew.GetCostTypeList(Authentication.LangId, True))
            CostTypeList.Populate(CommonConfigManager.Current.ListManager.GetList("CT", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
            {
              .AddBlankItem = True
            })
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try

    End Sub


    Private Sub PopulateBOFromForm()

        Dim editRow As GridViewRow = Grid.Rows(Grid.EditIndex)
        Dim effDateTxtBox As TextBox = CType(editRow.FindControl("EffectiveDateTextBox"), TextBox)
        Dim expDateTxtBox As TextBox = CType(editRow.FindControl("ExpirationDateTextBox"), TextBox)


        Try
            With Me.State.myChildBO

                .Code = CType(Me.Grid.Rows(Me.Grid.EditIndex).Cells(Me.GRID_COL_SERVICE_LEVEL_CODE_IDX).FindControl("ServceLevelCodeTextBox"), TextBox).Text
                .Description = CType(Me.Grid.Rows(Me.Grid.EditIndex).Cells(Me.GRID_COL_SERVICE_LEVEL_DESCRIPTION_IDX).FindControl("ServceLevelDescTextBox"), TextBox).Text
                .RiskTypeId = New Guid(CType(Me.Grid.Rows(Me.Grid.EditIndex).Cells(Me.GRID_COL_RISK_TYPE_IDX).FindControl("RiskTypeDropdown"), DropDownList).SelectedValue)
                .CostTypeId = New Guid(CType(Me.Grid.Rows(Me.Grid.EditIndex).Cells(Me.GRID_COL_COST_TYPE_IDX).FindControl("CostTypeDropdown"), DropDownList).SelectedValue)
                Me.PopulateBOProperty(Me.State.myChildBO, "ServiceLevelCost", CType(Me.Grid.Rows(Me.Grid.EditIndex).Cells(Me.GRID_COL_SERVICE_LEVEL_COST_IDX).FindControl("ServiceLevelCostTextBox"), TextBox))
                If (Not effDateTxtBox Is Nothing) AndAlso (Not effDateTxtBox.Text Is String.Empty) Then
                    Me.PopulateBOProperty(Me.State.myChildBO, "EffectiveDate", effDateTxtBox)
                Else
                    Me.PopulateBOProperty(Me.State.myChildBO, "EffectiveDate", Me.Grid.Rows(Me.Grid.EditIndex).Cells(Me.GRID_COL_EFFECTIVE_DATE_IDX).Text)
                End If
                If (Not expDateTxtBox Is Nothing) AndAlso (Not expDateTxtBox.Text Is String.Empty) Then
                    Me.PopulateBOProperty(Me.State.myChildBO, "ExpirationDate", expDateTxtBox)
                Else
                    Me.PopulateBOProperty(Me.State.myChildBO, "ExpirationDate", Me.Grid.Rows(Me.Grid.EditIndex).Cells(Me.GRID_COL_EXPIRATION_DATE_IDX).Text)
                End If

            End With
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try

    End Sub

    Private Sub PopulateFormFromBO()

        Dim gridRowIdx As Integer = Me.Grid.EditIndex
        Try
            With Me.State.myChildBO


                If (Not .Code Is Nothing) Then
                    CType(Me.Grid.Rows(gridRowIdx).Cells(Me.GRID_COL_SERVICE_LEVEL_CODE_IDX).FindControl("ServceLevelCodeTextBox"), TextBox).Text = .Code
                End If
                If (Not .Description Is Nothing) Then
                    CType(Me.Grid.Rows(gridRowIdx).Cells(Me.GRID_COL_SERVICE_LEVEL_DESCRIPTION_IDX).FindControl("ServceLevelDescTextBox"), TextBox).Text = .Description
                End If

                Dim RiskTypeList As DropDownList = CType(Me.Grid.Rows(gridRowIdx).Cells(Me.GRID_COL_RISK_TYPE_IDX).FindControl("RiskTypeDropdown"), DropDownList)
                Dim CostTypeList As DropDownList = CType(Me.Grid.Rows(gridRowIdx).Cells(Me.GRID_COL_COST_TYPE_IDX).FindControl("CostTypeDropdown"), DropDownList)
                PopulateDropdown(RiskTypeList, CostTypeList)

                Me.SetSelectedItem(RiskTypeList, .RiskTypeId)
                Me.SetSelectedItem(CostTypeList, .CostTypeId)

                If (Not .ServiceLevelCost Is Nothing) Then
                    CType(Me.Grid.Rows(gridRowIdx).Cells(Me.GRID_COL_SERVICE_LEVEL_COST_IDX).FindControl("ServiceLevelCostTextBox"), TextBox).Text = .ServiceLevelCost.ToString
                End If

                If (Not .EffectiveDate Is Nothing) Then
                    CType(Me.Grid.Rows(gridRowIdx).Cells(Me.GRID_COL_EFFECTIVE_DATE_IDX).FindControl("EffectiveDateTextBox"), TextBox).Text = Me.GetDateFormattedString(CType(.EffectiveDate, Date))
                End If

                If (Not .ExpirationDate Is Nothing) Then
                    CType(Me.Grid.Rows(gridRowIdx).Cells(Me.GRID_COL_EXPIRATION_DATE_IDX).FindControl("ExpirationDateTextBox"), TextBox).Text = Me.GetDateFormattedString(CType(.ExpirationDate, Date))
                End If


            End With
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try

    End Sub

    Private Sub SetFocusOnEditableFieldInGrid(ByVal grid As GridView, ByVal cellPosition As Integer, ByVal controlName As String, ByVal itemIndex As Integer)
        'Set focus on the Description TextBox for the EditItemIndex row
        Dim desc As TextBox = CType(grid.Rows(itemIndex).Cells(cellPosition).FindControl(controlName), TextBox)
        SetFocus(desc)
    End Sub

    Private Sub PopulateHeaderTextbox()
        Dim oCountry As Country

        oCountry = New Country(Me.State.myBO.CountryId)
        Me.PopulateControlFromBOProperty(moCountryText_NO_TRANSLATE, oCountry.Description)
        Me.PopulateControlFromBOProperty(Me.TextboxShortDesc_WRITE, Me.State.myBO.Code)
        Me.PopulateControlFromBOProperty(Me.TextboxDescription_WRITE, Me.State.myBO.Description)


    End Sub


#End Region

End Class


