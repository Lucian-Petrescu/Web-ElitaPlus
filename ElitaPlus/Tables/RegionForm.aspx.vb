Option Strict On
Option Explicit On
Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms

Partial Class RegionForm
    Inherits ElitaPlusSearchPage

#Region "Page State"
    'Class MyState
    '    Public PageIndex As Integer = 0
    '    Public myBO As BusinessObjectsNew.Region
    '    Public DescriptionMask As String
    '    Public CodeMask As String
    '    Public CompanyId As Guid
    '    Public SearchCountryId As Guid = Guid.Empty
    '    Public Id As Guid
    '    Public IsAfterSave As Boolean
    '    Public IsEditMode As Boolean
    '    Public IsGridVisible As Boolean
    '    Public searchDV As DataView = Nothing
    '    Public SortExpression As String = BusinessObjectsNew.Region.RegionSearchDV.COL_DESCRIPTION
    '    Public selectedPageSize As Integer = DEFAULT_PAGE_SIZE
    '    Public AddingNewRow As Boolean
    '    Public Canceling As Boolean
    'End Class

    Class MyState
        Public PageIndex As Integer = 0
        Public MyBO As BusinessObjectsNew.Region  '= New DealerGroup
        Public DescriptionMask As String
        Public CodeMask As String
        Public Id As Guid
        Public IsAfterSave As Boolean
        Public IsEditMode As Boolean
        Public IsGridVisible As Boolean
        Public searchDV As BusinessObjectsNew.Region.RegionSearchDV = Nothing
        Public SortExpression As String = BusinessObjectsNew.Region.RegionSearchDV.COL_EXTENDED_CODE
        Public selectedPageSize As Integer = DEFAULT_PAGE_SIZE
        Public AddingNewRow As Boolean
        Public Canceling As Boolean
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

    'Private Class PageStatus

    '    Public Sub New()
    '        pageIndex = 0
    '        pageCount = 0
    '    End Sub

    'End Class


#End Region

#Region "Constants"

    Private Const NO_ROW_SELECTED_INDEX As Integer = -1
    Private Const REGION_ID As Integer = 2
    Private Const COUNTRY_ID As Integer = 3
    Private Const DESCRIPTION As Integer = 4
    Private Const REGION_CODE As Integer = 5
    Private Const ACCOUNTING_CODE As Integer = 6
    Private Const INVOICE_TAX As Integer = 7
    Private Const SORT_VALUE As Integer = 8

    'Actions
    Private Const ACTION_NONE As String = "ACTION_NONE"
    Private Const ACTION_SAVE As String = "ACTION_SAVE"
    Private Const ACTION_NO_EDIT As String = "ACTION_NO_EDIT"
    Private Const ACTION_EDIT As String = "ACTION_EDIT"
    Private Const ACTION_NEW As String = "ACTION_NEW"

    Private Const COUNTRY_IN_GRID_CONTROL_NAME As String = "cboCountryInGrid"
    Private Const DESCRIPTION_IN_GRID_CONTROL_NAME As String = "TextBoxGridDescription"
    Private Const SHORT_DESC_IN_GRID_CONTROL_NAME As String = "TextBoxShortDesc"
    Private Const ACCT_CODE_IN_GRID_CONTROL_NAME As String = "TextBoxAcctCode"
    Private Const INVOICE_TAX_IN_GRID_CONTROL_NAME As String = "TextBoxInvoiceTaxGL"
    Private Const EXTENDED_CODE_IN_GRID_CONTROL_NAME As String = "TextBoxExtendedCode"
    Private Const REGION_ID_LABEL As String = "LABELRegionId"
    Private Const LABEL_DESCRIPTION As String = "DescriptionLabel"
    Private Const LABEL_SHORTDESCRIPTION As String = "ShortDescriptionLabel"
    Private Const LABEL_ACCTCODE As String = "AccountingCodeLabel"
    Private Const LABEL_COUNTRY As String = "CountryLabel"
    Private Const LABEL_INVOICE_TAX As String = "InvoiceTaxGLLabel"
    Private Const LABEL_EXTENDED_CODE As String = "ExtendedCodeLabel"

    Private Const MSG_CONFIRM_PROMPT As String = "MSG_CONFIRM_PROMPT"
    Private Const MSG_RECORD_DELETED_OK As String = "MSG_RECORD_DELETED_OK"
    Private Const MSG_RECORD_SAVED_OK As String = "MSG_RECORD_SAVED_OK"
    Private Const MSG_RECORD_NOT_SAVED As String = "MSG_RECORD_NOT_SAVED"


#End Region

#Region "Variables"

    Private moRegion As BusinessObjectsNew.Region
    Private moRegionId As String
    Private isReturning As Boolean = False


    'Private Shared pageCount As Integer
    'Private Shared pageIndex As Integer

#End Region

#Region "Properties"

    Private ReadOnly Property theRegion() As BusinessObjectsNew.Region
        Get
            If IsNewRegion() = True Then
                ' For creating, inserting
                moRegion = New BusinessObjectsNew.Region
                RegionId = moRegion.Id.ToString
            Else
                ' For updating, deleting
                moRegion = New BusinessObjectsNew.Region(Me.State.Id)
            End If

            Return moRegion
        End Get
    End Property

    Private Property RegionId() As String
        Get
            If Grid.SelectedIndex > Me.NO_ITEM_SELECTED_INDEX Then
                moRegionId = CType(Me.Grid.Rows(Me.Grid.SelectedIndex).Cells(Me.REGION_ID).FindControl(Me.REGION_ID_LABEL), Label).Text
            End If
            Return moRegionId
        End Get
        Set(ByVal Value As String)
            If Grid.SelectedIndex > Me.NO_ITEM_SELECTED_INDEX Then
                Me.SetSelectedGridText(Grid, REGION_ID, Value)
            End If
            moRegionId = Value
        End Set
    End Property

    Private Property IsNewRegion() As Boolean
        Get
            Return Me.State.AddingNewRow
        End Get
        Set(ByVal Value As Boolean)
            Me.State.AddingNewRow = Value
        End Set
    End Property

#End Region

#Region "Handlers"

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents moErrorController As ErrorController

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Handlers-Init"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Try
            moErrorController.Clear_Hide()
            If Me.RegionId Is Nothing Then
                moRegionId = Guid.Empty.ToString
            Else
                moRegionId = Me.RegionId
            End If

            If Not Page.IsPostBack Then
                Me.SetDefaultButton(Me.SearchDescriptionTextBox, Me.SearchButton)
                Me.SetDefaultButton(Me.SearchCodeTextbox, Me.SearchButton)
                ControlMgr.SetVisibleControl(Me, trPageSize, False)
                Me.SetGridItemStyleColor(Grid)
                IsNewRegion = False
                SetButtonsState(False)
                PopulateCountry()

                Me.TranslateGridHeader(Me.Grid)
                Me.TranslateGridControls(Me.Grid)
            Else
                CheckIfComingFromDeleteConfirm()
            End If
            BindBoPropertiesToGridHeaders()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.moErrorController)
        End Try
        Me.ShowMissingTranslations(moErrorController)
    End Sub

#End Region

#Region "Handlers-Buttons"

    Private Sub SearchButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SearchButton.Click
        Try
            Grid.PageIndex = Me.NO_PAGE_INDEX
            Me.State.IsGridVisible = True
            Me.State.searchDV = Nothing
            PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.moErrorController)
        End Try

    End Sub

    Private Sub ClearButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClearButton.Click
        ClearSearchCriteria()

    End Sub

    Private Sub NewButton_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewButton_WRITE.Click
        Try
            Me.State.IsEditMode = True
            Me.State.IsGridVisible = True
            Me.State.AddingNewRow = True
            AddNew()
            SetButtonsState()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.moErrorController)
        End Try

    End Sub

    Private Sub SaveButton_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveButton_WRITE.Click
        Try
            SaveChanges()

        Catch ex As Exception
            Me.HandleErrors(ex, Me.moErrorController)
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
            Me.HandleErrors(ex, Me.moErrorController)
        End Try

    End Sub
    Private Sub ClearSearchCriteria()

        Try
            SearchDescriptionTextBox.Text = String.Empty
            SearchCodeTextbox.Text = String.Empty
            moCountryDrop.SelectedIndex = Me.BLANK_ITEM_SELECTED
            PopulateGrid()

        Catch ex As Exception
            Me.HandleErrors(ex, Me.moErrorController)
        End Try

    End Sub

#End Region

#Region "Grid"

    Private Sub PopulateGrid()

        Try
            'Refresh the DataView and Call SetPageAndSelectedIndexFromGuid() to go to the Page 
            'where the most recently saved Record exists in the DataView

            If (Me.State.searchDV Is Nothing) Then
                Me.State.searchDV = GetGridDataView()
                '  Me.State.searchDV.Sort = Me.Grid.DataMember
            End If
            'Ticket # 748479 - Search grids in Tables tab should not show pop-up message when number of retrieved record is over 1,000
            'Me.ValidSearchResultCount(Me.State.searchDV.Count, True)
            Me.State.searchDV.Sort = Me.State.SortExpression

            If (Me.State.IsAfterSave) Then
                Me.State.IsAfterSave = False
                Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.Id, Me.Grid, Me.State.PageIndex)
            ElseIf (Me.State.IsEditMode) Then
                Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.Id, Me.Grid, Me.State.PageIndex, Me.State.IsEditMode)
            Else
                'In a Delete scenario...
                Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Guid.Empty, Me.Grid, Me.State.PageIndex, Me.State.IsEditMode)
            End If
            Me.Grid.AutoGenerateColumns = False
            'Me.Grid.Columns(Me.DESCRIPTION).SortExpression = DealerGroup.DealerGroupSearchDV.COL_DESCRIPTION
            'Me.Grid.Columns(Me.REGION_ID).SortExpression = DealerGroup.DealerGroupSearchDV.COL_CODE

            SortAndBindGrid()

        Catch ex As Exception
            Me.HandleErrors(ex, Me.moErrorController)
        End Try

    End Sub

    Private Sub PopulateGridWithNoSort()

        Try
            'Refresh the DataView and Call SetPageAndSelectedIndexFromGuid() to go to the Page 
            'where the most recently saved Record exists in the DataView

            If (Me.State.searchDV Is Nothing) Then
                Me.State.searchDV = GetGridDataView()
                '  Me.State.searchDV.Sort = Me.Grid.DataMember
            End If
            'Ticket # 748479 - Search grids in Tables tab should not show pop-up message when number of retrieved record is over 1,000
            'Me.ValidSearchResultCount(Me.State.searchDV.Count, True)
            ' Me.State.searchDV.Sort = Me.State.SortExpression

            If (Me.State.IsAfterSave) Then
                Me.State.IsAfterSave = False
                Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.Id, Me.Grid, Me.State.PageIndex)
            ElseIf (Me.State.IsEditMode) Then
                Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.Id, Me.Grid, Me.State.PageIndex, Me.State.IsEditMode)
            Else
                'In a Delete scenario...
                Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Guid.Empty, Me.Grid, Me.State.PageIndex, Me.State.IsEditMode)
            End If
            Me.Grid.AutoGenerateColumns = False
            'Me.Grid.Columns(Me.DESCRIPTION).SortExpression = DealerGroup.DealerGroupSearchDV.COL_DESCRIPTION
            'Me.Grid.Columns(Me.REGION_ID).SortExpression = DealerGroup.DealerGroupSearchDV.COL_CODE

            'SortAndBindGrid()

            Me.State.PageIndex = Me.Grid.PageIndex
            Me.Grid.DataSource = Me.State.searchDV
            Me.Grid.DataBind()

            ControlMgr.SetVisibleControl(Me, Grid, Me.State.IsGridVisible)

            ControlMgr.SetVisibleControl(Me, trPageSize, Me.Grid.Visible)

            Session("recCount") = Me.State.searchDV.Count
            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Grid)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.moErrorController)
        End Try

    End Sub

    Private Sub SortAndBindGrid()
        Me.State.PageIndex = Me.Grid.PageIndex

        If BusinessObjectsNew.CountryTax.isInvoiceTaxEnabled Then
            Grid.Columns(INVOICE_TAX).Visible = True
        Else
            Grid.Columns(INVOICE_TAX).Visible = False
        End If

        If (Me.State.searchDV.Count = 0) Then
            CreateHeaderForEmptyGrid(Grid, Me.State.SortExpression)
            ControlMgr.SetVisibleControl(Me, Grid, Me.State.IsGridVisible)
            ControlMgr.SetVisibleControl(Me, trPageSize, Me.Grid.Visible)
            Grid.PagerSettings.Visible = True
            If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True
        Else
            Me.Grid.DataSource = Me.State.searchDV
            HighLightSortColumn(Grid, Me.State.SortExpression)
            Grid.PagerSettings.Visible = True
            Me.Grid.DataBind()

            ControlMgr.SetVisibleControl(Me, Grid, Me.State.IsGridVisible)
            ControlMgr.SetVisibleControl(Me, trPageSize, Me.Grid.Visible)
            If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True
        End If

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

    'The Binding Logic is here
    Private Sub Grid_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
        Try
            Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

            If Not dvRow Is Nothing And State.searchDV.Count > 0 Then
                If (itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem) And CType(e.Row.RowState, Int16) < 4 Then
                    CType(e.Row.Cells(Me.REGION_ID).FindControl(Me.REGION_ID_LABEL), Label).Text = GetGuidStringFromByteArray(CType(dvRow(BusinessObjectsNew.Region.RegionSearchDV.COL_REGION_ID), Byte()))
                    CType(e.Row.Cells(Me.DESCRIPTION).FindControl(LABEL_DESCRIPTION), Label).Text = dvRow(BusinessObjectsNew.Region.RegionSearchDV.COL_DESCRIPTION).ToString
                    CType(e.Row.Cells(Me.REGION_CODE).FindControl(LABEL_SHORTDESCRIPTION), Label).Text = dvRow(BusinessObjectsNew.Region.RegionSearchDV.COL_CODE).ToString
                    CType(e.Row.Cells(Me.ACCOUNTING_CODE).FindControl(LABEL_ACCTCODE), Label).Text = dvRow(BusinessObjectsNew.Region.RegionSearchDV.COL_ACCOUNTING_CODE).ToString
                    CType(e.Row.Cells(Me.COUNTRY_ID).FindControl(LABEL_COUNTRY), Label).Text = dvRow(BusinessObjectsNew.Region.RegionSearchDV.COL_COUNTRY_NAME).ToString
                    CType(e.Row.Cells(Me.INVOICE_TAX).FindControl(LABEL_INVOICE_TAX), Label).Text = dvRow(BusinessObjectsNew.Region.RegionSearchDV.COL_INVOICE_TAX_GL).ToString
                    CType(e.Row.Cells(Me.SORT_VALUE).FindControl(LABEL_EXTENDED_CODE), Label).Text = dvRow(BusinessObjectsNew.Region.RegionSearchDV.COL_EXTENDED_CODE).ToString

                End If
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.moErrorController)
        End Try
    End Sub

    Private Sub Grid_PageIndexChanging(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging

        Try
            If (Not (Me.State.IsEditMode)) Then
                Me.State.PageIndex = e.NewPageIndex
                Me.Grid.PageIndex = Me.State.PageIndex
                Me.PopulateGrid()
                Me.Grid.SelectedIndex = Me.NO_ITEM_SELECTED_INDEX
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.moErrorController)
        End Try

    End Sub

    Protected Sub RowCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)

        Try
            Dim index As Integer

            If (e.CommandName = Me.EDIT_COMMAND_NAME) Then

                index = CInt(e.CommandArgument)

                'Do the Edit here

                'Set the IsEditMode flag to TRUE
                Me.State.IsEditMode = True

                Me.State.Id = New Guid(CType(Me.Grid.Rows(index).Cells(Me.REGION_ID).FindControl(Me.REGION_ID_LABEL), Label).Text)
                Me.State.MyBO = New BusinessObjectsNew.Region(Me.State.Id)

                Me.PopulateGrid()

                Me.State.PageIndex = Grid.PageIndex

                'Disable all Edit and Delete icon buttons on the Grid
                SetGridControls(Me.Grid, False)

                'Set focus on the Description TextBox for the EditItemIndex row
                Me.SetFocusOnEditableFieldInGrid(Me.Grid, Me.DESCRIPTION, Me.DESCRIPTION_IN_GRID_CONTROL_NAME, index)

                PopulateFormFromBO()

                Me.SetButtonsState()

            ElseIf (e.CommandName = Me.DELETE_COMMAND_NAME) Then
                index = CInt(e.CommandArgument)

                'Clear the SelectedItemStyle to remove the highlight from the previously saved row
                Grid.SelectedIndex = Me.NO_ROW_SELECTED_INDEX
                Me.State.Id = New Guid(CType(Me.Grid.Rows(index).Cells(Me.REGION_ID).FindControl(Me.REGION_ID_LABEL), Label).Text)

                Me.DisplayMessage(Message.DELETE_RECORD_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenDeletePromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.moErrorController)
        End Try

    End Sub
    Private Sub PopulateFormFromBO()

        Dim gridRowIdx As Integer = Me.Grid.EditIndex
        Dim txtShortDesc, txtAcctCode, txtInvoiceTax, txtExtendedCode As TextBox

        Try
            With Me.State.MyBO

                'populateAcctCode()

                txtShortDesc = CType(Me.Grid.Rows(gridRowIdx).Cells(Me.REGION_CODE).FindControl(SHORT_DESC_IN_GRID_CONTROL_NAME), TextBox)
                If (Not .ShortDesc Is Nothing) Then
                    txtShortDesc.Text = .ShortDesc
                End If

                If (Not .Description Is Nothing) Then
                    CType(Me.Grid.Rows(gridRowIdx).Cells(Me.DESCRIPTION).FindControl(DESCRIPTION_IN_GRID_CONTROL_NAME), TextBox).Text = .Description
                End If

                txtAcctCode = CType(Me.Grid.Rows(gridRowIdx).Cells(Me.ACCOUNTING_CODE).FindControl(ACCT_CODE_IN_GRID_CONTROL_NAME), TextBox)
                If (Not .AccountingCode Is Nothing) Then
                    txtAcctCode.Text = .AccountingCode
                End If

                txtInvoiceTax = CType(Me.Grid.Rows(gridRowIdx).Cells(Me.INVOICE_TAX).FindControl(INVOICE_TAX_IN_GRID_CONTROL_NAME), TextBox)
                If (Not .AccountingCode Is Nothing) Then
                    'Def-26691: Added condition to check if the value for InvoiceTaxGLAcct is not null.
                    If (Not .InvoiceTaxGLAcct Is Nothing) Then
                        txtInvoiceTax.Text = .InvoiceTaxGLAcct
                    End If
                End If

                txtExtendedCode = CType(Me.Grid.Rows(gridRowIdx).Cells(Me.SORT_VALUE).FindControl(EXTENDED_CODE_IN_GRID_CONTROL_NAME), TextBox)
                If (Not .ExtendedCode Is Nothing) Then
                    txtExtendedCode.Text = .ExtendedCode
                End If

                txtShortDesc.Attributes.Add("onBlur", "populateAcctCode(this, getElementById('" + txtAcctCode.ClientID + "'));")

                CType(Me.Grid.Rows(gridRowIdx).Cells(Me.REGION_ID).FindControl(Me.REGION_ID_LABEL), Label).Text = .Id.ToString

                Dim oCboCountry As DropDownList = CType(Me.Grid.Rows(gridRowIdx).Cells(Me.COUNTRY_ID).FindControl(COUNTRY_IN_GRID_CONTROL_NAME), DropDownList)
                '   Me.BindListControlToDataView(oCboCountry, LookupListNew.GetUserCountriesLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies))
                Dim countryLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.Country, Thread.CurrentPrincipal.GetLanguageCode())
                Dim list As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Countries
                Dim filteredList As ListItem() = (From x In countryLkl
                                                  Where list.Contains(x.ListItemId)
                                                  Select x).ToArray()

                oCboCountry.Populate(filteredList, New PopulateOptions() With
                {
               .AddBlankItem = True
                 })
                oCboCountry.SelectedValue = moCountryDrop.SelectedValue

            End With
        Catch ex As Exception
            Me.HandleErrors(ex, Me.moErrorController)
        End Try

    End Sub
    Protected Sub RowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        BaseItemCreated(sender, e)
    End Sub

    Private Sub Grid_Sorting(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting

        Try
            If Me.State.SortExpression.StartsWith(e.SortExpression) Then
                If Me.State.SortExpression.EndsWith(" DESC") Then
                    Me.State.SortExpression = e.SortExpression
                Else
                    Me.State.SortExpression &= " DESC"
                End If
            Else
                Me.State.SortExpression = e.SortExpression
            End If
            'To handle the requirement of always going to the FIRST page on the Grid whenever the user switches the sorting criterion
            'Set the Me.State.selectedClaimId = Guid.Empty and set Me.State.PageIndex = 0
            Me.State.Id = Guid.Empty
            Me.State.PageIndex = 0

            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.moErrorController)
        End Try

    End Sub

    Protected Sub BindBoPropertiesToGridHeaders()
        Me.BindBOPropertyToGridHeader(Me.State.MyBO, "Description", Me.Grid.Columns(Me.DESCRIPTION))
        Me.BindBOPropertyToGridHeader(Me.State.MyBO, "ShortDesc", Me.Grid.Columns(Me.REGION_CODE))
        Me.BindBOPropertyToGridHeader(Me.State.MyBO, "AccountingCode", Me.Grid.Columns(Me.ACCOUNTING_CODE))
        Me.BindBOPropertyToGridHeader(Me.State.MyBO, "CountryId", Me.Grid.Columns(Me.COUNTRY_ID))
        Me.BindBOPropertyToGridHeader(Me.State.MyBO, "InvoiceTaxGLAcct", Me.Grid.Columns(Me.INVOICE_TAX))
        Me.ClearGridViewHeadersAndLabelsErrSign()
    End Sub

    Private Sub SetFocusOnEditableFieldInGrid(ByVal grid As GridView, ByVal cellPosition As Integer, ByVal controlName As String, ByVal itemIndex As Integer)
        'Set focus on the Description TextBox for the EditItemIndex row
        Dim desc As TextBox = CType(grid.Rows(itemIndex).Cells(cellPosition).FindControl(controlName), TextBox)
        SetFocus(desc)
    End Sub
    Protected Sub CheckIfComingFromDeleteConfirm()
        Dim confResponse As String = Me.HiddenDeletePromptResponse.Value
        If Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_YES Then
            If Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete Then
                DeleteSelectedRegion()
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
#End Region

#Region "Handlers_DropDowns"

    Private Sub Grid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            Grid.PageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
            Me.State.selectedPageSize = CType(cboPageSize.SelectedValue, Integer)
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.moErrorController)
        End Try
    End Sub

#End Region

#End Region

#Region "Buttons-Management"

    Private Sub SetButtonsState(ByVal bIsEdit As Boolean)

        If (bIsEdit) Then
            ControlMgr.SetVisibleControl(Me, SaveButton_WRITE, True)
            ControlMgr.SetVisibleControl(Me, CancelButton, True)
            ControlMgr.SetVisibleControl(Me, NewButton_WRITE, False)
            ControlMgr.SetEnableControl(Me, SearchButton, False)
            ControlMgr.SetEnableControl(Me, ClearButton, False)
            Me.MenuEnabled = False
            If (Me.cboPageSize.Visible) Then
                ControlMgr.SetEnableControl(Me, cboPageSize, False)
            End If
        Else
            ControlMgr.SetVisibleControl(Me, SaveButton_WRITE, False)
            ControlMgr.SetVisibleControl(Me, CancelButton, False)
            ControlMgr.SetVisibleControl(Me, NewButton_WRITE, True)
            ControlMgr.SetEnableControl(Me, SearchButton, True)
            ControlMgr.SetEnableControl(Me, ClearButton, True)
            Me.MenuEnabled = True
            If (Me.cboPageSize.Visible) Then
                ControlMgr.SetEnableControl(Me, cboPageSize, True)
            End If
        End If

    End Sub

    'Private Sub SetButtonsState()

    '    If (Me.State.IsEditMode) Then
    '        ControlMgr.SetVisibleControl(Me, SaveButton_WRITE, True)
    '        ControlMgr.SetVisibleControl(Me, CancelButton, True)
    '        ControlMgr.SetVisibleControl(Me, NewButton_WRITE, False)
    '        ControlMgr.SetEnableControl(Me, SearchButton, False)
    '        ControlMgr.SetEnableControl(Me, ClearButton, False)
    '        Me.MenuEnabled = False
    '        If (Me.cboPageSize.Visible) Then
    '            ControlMgr.SetEnableControl(Me, cboPageSize, False)
    '        End If
    '    Else
    '        ControlMgr.SetVisibleControl(Me, SaveButton_WRITE, False)
    '        ControlMgr.SetVisibleControl(Me, CancelButton, False)
    '        ControlMgr.SetVisibleControl(Me, NewButton_WRITE, True)
    '        ControlMgr.SetEnableControl(Me, SearchButton, True)
    '        ControlMgr.SetEnableControl(Me, ClearButton, True)
    '        Me.MenuEnabled = True
    '        If (Me.cboPageSize.Visible) Then
    '            ControlMgr.SetEnableControl(Me, cboPageSize, True)
    '        End If
    '    End If

    'End Sub
#End Region

#Region "Populate"

    Private Sub PopulateCountry()
        'Me.BindListControlToDataView(moCountryDrop, LookupListNew.GetUserCountriesLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies))
        Dim countryLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.Country, Thread.CurrentPrincipal.GetLanguageCode())
        Dim list As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Countries
        Dim filteredList As ListItem() = (From x In countryLkl
                                          Where list.Contains(x.ListItemId)
                                          Select x).ToArray()

        moCountryDrop.Populate(filteredList, New PopulateOptions() With
             {
            .AddBlankItem = True
             })
        If moCountryDrop.Items.Count < 3 Then
            ControlMgr.SetVisibleControl(Me, moCountryLabel, False)
            ControlMgr.SetVisibleControl(Me, moCountryDrop, False)
        End If
    End Sub

    Public Overrides Sub BaseSetButtonsState(ByVal bIsEdit As Boolean)
        SetButtonsState(bIsEdit)
    End Sub

#End Region

#Region "Business"

    Private Sub PopulateBOFromForm(ByVal oRegion As BusinessObjectsNew.Region)

        Try
            With oRegion
                .Description = CType(Me.Grid.Rows(Me.Grid.EditIndex).Cells(Me.DESCRIPTION).FindControl(Me.DESCRIPTION_IN_GRID_CONTROL_NAME), TextBox).Text
                .ShortDesc = CType(Me.Grid.Rows(Me.Grid.EditIndex).Cells(Me.REGION_CODE).FindControl(Me.SHORT_DESC_IN_GRID_CONTROL_NAME), TextBox).Text
                .AccountingCode = CType(Me.Grid.Rows(Me.Grid.EditIndex).Cells(Me.ACCOUNTING_CODE).FindControl(Me.ACCT_CODE_IN_GRID_CONTROL_NAME), TextBox).Text
                .ExtendedCode = CType(Me.Grid.Rows(Me.Grid.EditIndex).Cells(Me.SORT_VALUE).FindControl(Me.EXTENDED_CODE_IN_GRID_CONTROL_NAME), TextBox).Text

                'Def-26691: Added condition to check if the value for invoice tax is not empty.
                If Not (CType(Me.Grid.Rows(Me.Grid.EditIndex).Cells(Me.INVOICE_TAX).FindControl(Me.INVOICE_TAX_IN_GRID_CONTROL_NAME), TextBox).Text = String.Empty) AndAlso _
                    Not CType(Me.Grid.Rows(Me.Grid.EditIndex).Cells(Me.INVOICE_TAX).FindControl(Me.INVOICE_TAX_IN_GRID_CONTROL_NAME), TextBox).Text.Equals("") Then
                    .InvoiceTaxGLAcct = CType(Me.Grid.Rows(Me.Grid.EditIndex).Cells(Me.INVOICE_TAX).FindControl(Me.INVOICE_TAX_IN_GRID_CONTROL_NAME), TextBox).Text
                End If

                Dim CountryIdList As DropDownList = CType(Me.Grid.Rows(Grid.EditIndex).Cells(Me.COUNTRY_ID).FindControl(Me.COUNTRY_IN_GRID_CONTROL_NAME), DropDownList)
                Me.PopulateBOProperty(Me.State.MyBO, "CountryId", CountryIdList)

            End With
        Catch ex As Exception
            Me.HandleErrors(ex, Me.moErrorController)
        End Try

    End Sub
    Private Sub SaveChanges()
        Try
            PopulateBOFromForm(Me.State.MyBO)
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
            Me.HandleErrors(ex, Me.moErrorController)
        End Try
    End Sub
    Private Function GetDV() As DataView

        Dim dv As DataView

        dv = GetGridDataView()
        dv.Sort = Grid.DataMember()
        Grid.DataSource = dv

        Return (dv)

    End Function
    Private Function GetGridDataView() As BusinessObjectsNew.Region.RegionSearchDV
        Dim sDescription As String = "*"
        Dim sCode As String = "*"

        If Not SearchDescriptionTextBox.Text.Trim.Equals("") Then
            sDescription = SearchDescriptionTextBox.Text
        End If

        If Not SearchCodeTextbox.Text.Trim.Equals("") Then
            sCode = SearchCodeTextbox.Text
        End If

        If Not moCountryDrop.Visible Then moCountryDrop.SelectedIndex = 1
        Return New BusinessObjectsNew.Region.RegionSearchDV(BusinessObjectsNew.Region.LoadList(sDescription, sCode, Me.GetGuidFromString(moCountryDrop.SelectedValue.ToString)).Table)

    End Function
    Private Function ApplyChanges() As Boolean
        Dim bIsOk As Boolean = True
        Dim bIsDirty As Boolean
        If Grid.EditIndex = Me.NO_ITEM_SELECTED_INDEX Then Return False ' Product Code Conversion is not in edit mode
        Dim oRegion As BusinessObjectsNew.Region
        Try
            oRegion = theRegion
            BindBoPropertiesToGridHeaders(oRegion)
            With oRegion
                PopulateBOFromForm(oRegion)
                bIsDirty = .IsDirty
                .Save()
                SetButtonsState(False)
                SetGridControls(Me.Grid, True)
            End With
        Catch ex As Exception
            Me.HandleErrors(ex, Me.moErrorController)
            bIsOk = False
        End Try
        If (bIsOk = True) Then
            If bIsDirty = True Then
                Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
            Else
                Me.AddInfoMsg(Message.MSG_RECORD_NOT_SAVED)
            End If
        End If

        Return bIsOk
    End Function
    Private Function DeleteSelectedRegion() As Boolean
        Dim bIsOk As Boolean = True
        Try
            With theRegion()
                .Delete()
                .Save()
            End With

            'Set the IsAfterSave flag to TRUE so that the Paging logic gets invoked
            Me.State.IsAfterSave = True
            Me.State.searchDV = Nothing
            PopulateGrid()
            Me.State.PageIndex = Grid.PageIndex

        Catch ex As Exception
            Me.HandleErrors(ex, Me.moErrorController)
            bIsOk = False
        End Try
        Return bIsOk
    End Function
    Protected Sub BindBoPropertiesToGridHeaders(ByVal oRegion As BusinessObjectsNew.Region)
        Me.BindBOPropertyToGridHeader(oRegion, "CountryId", Grid.Columns(Me.COUNTRY_ID))
        Me.BindBOPropertyToGridHeader(oRegion, "Description", Grid.Columns(Me.DESCRIPTION))
        Me.BindBOPropertyToGridHeader(oRegion, "ShortDesc", Grid.Columns(Me.REGION_CODE))
        Me.BindBOPropertyToGridHeader(oRegion, "AccountingCode", Grid.Columns(Me.ACCOUNTING_CODE))
        Me.BindBOPropertyToGridHeader(oRegion, "InvoiceTaxGLAcct", Grid.Columns(Me.INVOICE_TAX))
        Me.ClearGridHeadersAndLabelsErrSign()
    End Sub
    Private Sub AddNew()

        Me.State.searchDV = GetGridDataView()

        Me.State.MyBO = New BusinessObjectsNew.Region
        Me.State.Id = Me.State.MyBO.Id

        Me.State.searchDV = CType(Me.State.MyBO.GetNewDataViewRow(Me.State.searchDV, Me.State.Id), BusinessObjectsNew.Region.RegionSearchDV)
        Grid.DataSource = Me.State.searchDV

        Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.Id, Me.Grid, Me.State.PageIndex, Me.State.IsEditMode)

        SortAndBindGrid()

        Me.State.PageIndex = Grid.PageIndex

        SetGridControls(Me.Grid, False)

        'Set focus on the Description TextBox for the EditItemIndex row
        Me.SetFocusOnEditableFieldInGrid(Me.Grid, Me.DESCRIPTION, Me.DESCRIPTION_IN_GRID_CONTROL_NAME, Me.Grid.EditIndex)
        PopulateFormFromBO()

        'Me.TranslateGridControls(Grid)
        Me.SetButtonsState()
        ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Grid)

    End Sub
    Private Sub SetButtonsState()

        If (Me.State.IsEditMode) Then
            ControlMgr.SetVisibleControl(Me, SaveButton_WRITE, True)
            ControlMgr.SetVisibleControl(Me, CancelButton, True)
            ControlMgr.SetVisibleControl(Me, NewButton_WRITE, False)
            ControlMgr.SetEnableControl(Me, SearchButton, False)
            ControlMgr.SetEnableControl(Me, ClearButton, False)
            Me.MenuEnabled = False
            If (Me.cboPageSize.Visible) Then
                ControlMgr.SetEnableControl(Me, cboPageSize, False)
            End If
        Else
            ControlMgr.SetVisibleControl(Me, SaveButton_WRITE, False)
            ControlMgr.SetVisibleControl(Me, CancelButton, False)
            ControlMgr.SetVisibleControl(Me, NewButton_WRITE, True)
            ControlMgr.SetEnableControl(Me, SearchButton, True)
            ControlMgr.SetEnableControl(Me, ClearButton, True)
            Me.MenuEnabled = True
            If (Me.cboPageSize.Visible) Then
                ControlMgr.SetEnableControl(Me, Me.cboPageSize, True)
            End If
        End If

    End Sub
    Private Sub ReturnFromEditing()

        Grid.EditIndex = NO_ROW_SELECTED_INDEX

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


#End Region

#Region "Overrides"

    Public Overrides Sub AddNewBoRow(ByVal oDataView As DataView)

        Dim oId As Guid = Guid.NewGuid
        Me.BaseAddNewGridRow(Grid, oDataView, oId)
        Me.State.Id = oId

    End Sub

    Protected Sub Grid_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Grid.SelectedIndexChanged

    End Sub

#End Region


End Class
