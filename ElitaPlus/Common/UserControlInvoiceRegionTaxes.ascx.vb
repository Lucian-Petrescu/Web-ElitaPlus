Imports System.ComponentModel
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.Web.Forms
Imports Assurant.ElitaPlus.DALObjects
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Collections.Generic
Imports System.Linq

Partial Class UserControlInvoiceRegionTaxes
    Inherits System.Web.UI.UserControl

    Public Class RequestDataEventArgs
        Inherits EventArgs

        Public Data As InvoiceRegionTax.InvoiceRegionTaxDV

    End Class

    Public Delegate Sub RequestData(sender As Object, ByRef e As RequestDataEventArgs)

    Public Event RequestIIBBTaxesData As RequestData
    Public Event PropertyChanged As PropertyChangedEventHandler
    Dim moRegionDropDown As DropDownList
    Dim cboinvoicetype As DropDownList
    Private isControlEditable As Boolean = True

#Region "Constants"
    Private Const GRID_COL_INVOICE_REGION_TAX_ID As Integer = 0
    Private Const GRID_COL_INVOICE_TRANS_ID As Integer = 1
    Private Const GRID_COL_REGION As Integer = 2
    Private Const GRID_COL_REGION_DESCRIPTION As Integer = 3
    Private Const GRID_COL_TAX_TYPE As Integer = 4
    Private Const GRID_COL_TAX_AMOUNT As Integer = 5
    Private Const GRID_COL_EDIT As Integer = 6

    Private Const GRID_CTRL_NAME_LABEL_REGION As String = "lblRegion"
    Private Const GRID_CTRL_NAME_LABEL_IIBB_TAX As String = "lblIIBBTax"
    Private Const GRID_CTRL_NAME_LABEL_INVOICE_TYPE As String = "lblinvoicetype"

    Private Const GRID_CTRL_NAME_EDIT_REGION As String = "cboRegion"
    Private Const GRID_CTRL_NAME_EDIT_INVOICE_TYPE As String = "cboinvoicetype"
    Private Const GRID_CTRL_NAME_EDIT_IIBB_TAX As String = "txtIIBBTax"

    Private Const GRID_CTRL_NAME_DELETE_RISK_TYPE As String = "DeleteButton_WRITE"

    Private Const MSG_CONFIRM_PROMPT As String = "MSG_CONFIRM_PROMPT"
    Private Const MSG_RECORD_DELETED_OK As String = "MSG_RECORD_DELETED_OK"
    Private Const MSG_RECORD_SAVED_OK As String = "MSG_RECORD_SAVED_OK"
    Private Const MSG_RECORD_NOT_SAVED As String = "MSG_RECORD_NOT_SAVED"

    Private Const EDIT_COMMAND As String = "SelectAction"
    Private Const DELETE_COMMAND As String = "DeleteRecord"
    Private Const SORT_COMMAND As String = "Sort"

    Private Const NO_ROW_SELECTED_INDEX As Integer = -1
    Dim dtexistingvalues As DataTable = New DataTable

    Public Const INVOICE_STATUS_INPROGRESS As String = "IP"
    Public Const INVOICE_STATUS_PAID As String = "P"
    Public Const INVOICE_STATUS_REJECTED As String = "R"
    Private Const EDIT_BUTTON_NAME As String = "EditButton_WRITE"
    Private Const DELETE_BUTTON_NAME As String = "DeleteButton_WRITE"
    Private Const SAVE_BUTTON_NAME As String = "SaveButton"
    Private Const CANCEL_BUTTON_NAME As String = "CancelButton"

#End Region

    Public Property InvoicetransId As Nullable(Of Guid)
        Get
            Return TheState.invoicetransid
        End Get
        Set(value As Nullable(Of Guid))
            TheState.invoicetransid = CType(value, Guid)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("invoice_trans_id"))
        End Set
    End Property

    Public Property InvoiceStatus() As String
        Get
            Return TheState.InvoiceStatus
        End Get
        Set(value As String)
            TheState.InvoiceStatus = value
            'RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("invoice_status"))
        End Set
    End Property

    Private Shadows ReadOnly Property Page() As ElitaPlusPage
        Get
            Return CType(MyBase.Page, ElitaPlusPage)
        End Get
    End Property

#Region "Page State"
    Class MyState
        Public MyBO As InvoiceRegionTax
        Public DefaultInvoiceTransID As Guid
        Public PageIndex As Integer = 0
        Public PageSize As Integer = 10
        Public SelectedPageSize As Integer = 10
        Public InvoiceRegionTaxDV As InvoiceRegionTax.InvoiceRegionTaxDV = Nothing
        Public HasDataChanged As Boolean
        Public IsGridAddNew As Boolean = False
        Public IsEditMode As Boolean = False
        Public IsGridVisible As Boolean
        Public IsAfterSave As Boolean
        Public ActionInProgress As Integer = ElitaPlusPage.DetailPageCommand.Nothing_
        Public SortExpression As String = InvoiceRegionTax.InvoiceRegionTaxDV.COL_INVOICE_REGION_TAX_ID
        Public bnoRow As Boolean = False
        Public companyId As Guid = Guid.Empty
        Public companyCode As String = String.Empty
        Public invoicetransid As Guid = Guid.Empty
        Public dealer As String = String.Empty
        Public deleteRowIndex As Integer = 0
        Public isNew As String = "N"
        Public InvoiceStatus As String = String.Empty
        Public IsEditable As Boolean = True
    End Class

    Protected ReadOnly Property TheState() As MyState
        Get
            Try
                If ThePage.StateSession.Item(UniqueID) Is Nothing Then
                    ThePage.StateSession.Item(UniqueID) = New MyState
                End If
                Return CType(ThePage.StateSession.Item(UniqueID), MyState)

            Catch ex As Exception
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
            Return GridIIBBTaxes.EditIndex > ThePage.NO_ITEM_SELECTED_INDEX
        End Get
    End Property

    Public Property IsGridEditable() As Boolean
        Get
            Return TheState.IsEditable
        End Get
        Set(value As Boolean)
            TheState.IsEditable = value
        End Set
    End Property

    Public Property SortDirection() As String
        Get
            If ViewState("SortDirection") IsNot Nothing Then
                Return ViewState("SortDirection").ToString
            Else
                Return InvoiceRegionTax.COL_NAME_REGION_DESCRIPTION
            End If

        End Get
        Set(value As String)
            ViewState("SortDirection") = value
        End Set
    End Property

    'Public Shared Property InvoiceTransactionId As String
    'Public Property InvoiceStatus As String

#End Region

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            'SetControlState()
            If Page.IsPostBack Then
                Dim confResponse As String = HiddenDIDeletePromptResponse.Value
                Dim confResponseDel As String = HiddenDIDeletePromptResponse.Value
                If confResponseDel IsNot Nothing AndAlso confResponseDel = ThePage.MSG_VALUE_YES Then
                    If TheState.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete Then
                        TheState.DefaultInvoiceTransID = GuidControl.ByteArrayToGuid(GridIIBBTaxes.DataKeys(TheState.deleteRowIndex).Values(0))
                        ThePage.MasterPage.MessageController.AddSuccess(MSG_RECORD_DELETED_OK, True)
                        TheState.PageIndex = GridIIBBTaxes.PageIndex
                        TheState.IsAfterSave = True
                        TheState.InvoiceRegionTaxDV = Nothing
                        TheState.PageIndex = GridIIBBTaxes.PageIndex
                        TheState.IsEditMode = False
                        'SetControlState()
                        TheState.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                        HiddenDIDeletePromptResponse.Value = ""
                    End If
                ElseIf confResponseDel IsNot Nothing AndAlso confResponseDel = ThePage.MSG_VALUE_NO Then
                    TheState.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                    HiddenDIDeletePromptResponse.Value = ""
                End If
            Else
                SortDirection = InvoiceRegionTax.COL_NAME_REGION_DESCRIPTION
            End If
            SetControlState()

        Catch ex As Exception
            ThePage.HandleErrors(ex, ThePage.MasterPage.MessageController)
        End Try

    End Sub

#Region "Grid related"


    Public Sub Populate()

        Dim e As New RequestDataEventArgs
        If (InvoicetransId.Equals(Guid.Empty)) Then
            Throw New GUIException("You must select a region", Assurant.ElitaPlus.Common.ErrorCodes.GUI_REGION_MUST_BE_SELECTED_ERR)
        End If

        RaiseEvent RequestIIBBTaxesData(Me, e)
        TheState.InvoiceRegionTaxDV = e.Data
        PopulateGrid()

    End Sub

    Public Sub PopulateGrid()

        If (Not Page.IsPostBack) Then
            ThePage.TranslateGridHeader(GridIIBBTaxes)
        End If

        Dim blnNewSearch As Boolean = False
        cboDiPageSize.SelectedValue = CType(TheState.PageSize, String)
        Dim objInvoiceRegionTaxes As New InvoiceRegionTax

        Try
            With TheState
                If (.InvoiceRegionTaxDV Is Nothing) Then
                    objInvoiceRegionTaxes.InvoiceTransactionId = InvoicetransId 'Me.TheState.invoicetransid
                    .InvoiceRegionTaxDV = objInvoiceRegionTaxes.GetInvoiceRegionTax()
                    dtexistingvalues = .InvoiceRegionTaxDV.Table.Copy()
                    'blnNewSearch = True
                End If
            End With

            If TheState.InvoiceRegionTaxDV IsNot Nothing AndAlso Not TheState.IsGridAddNew = True Then
                TheState.InvoiceRegionTaxDV.Sort = SortDirection
            End If

            If (TheState.IsAfterSave) Then
                TheState.IsAfterSave = False
                ThePage.SetPageAndSelectedIndexFromGuid(TheState.InvoiceRegionTaxDV, TheState.DefaultInvoiceTransID, GridIIBBTaxes, TheState.PageIndex)
            ElseIf (TheState.IsEditMode) Then
                ThePage.SetPageAndSelectedIndexFromGuid(TheState.InvoiceRegionTaxDV, TheState.DefaultInvoiceTransID, GridIIBBTaxes, TheState.PageIndex, TheState.IsEditMode)
            Else
                If TheState.InvoiceRegionTaxDV IsNot Nothing Then
                    ThePage.SetPageAndSelectedIndexFromGuid(TheState.InvoiceRegionTaxDV, Guid.Empty, GridIIBBTaxes, TheState.PageIndex)
                End If
            End If
            GridIIBBTaxes.Columns(GRID_COL_REGION).SortExpression = InvoiceRegionTax.COL_NAME_REGION_DESCRIPTION

            If TheState.InvoiceRegionTaxDV IsNot Nothing AndAlso TheState.InvoiceRegionTaxDV.Count = 0 Then
                For Each gvRow As GridViewRow In GridIIBBTaxes.Rows
                    gvRow.Visible = False
                    gvRow.Controls.Clear()
                Next
                lblPageSize.Visible = False
                cboDiPageSize.Visible = False
                colonSepertor.Visible = False
            Else
                lblPageSize.Visible = True
                cboDiPageSize.Visible = True
                colonSepertor.Visible = True
            End If
            GridIIBBTaxes.AutoGenerateColumns = False
            SortAndBindGrid()

            SetControlState()
        Catch ex As Exception
            ThePage.HandleErrors(ex, ThePage.MasterPage.MessageController)
        End Try

    End Sub

    Private Sub Grid_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridIIBBTaxes.RowDataBound
        Try
            Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
            Dim strID As String '= InvoiceTransactionId

            If dvRow IsNot Nothing AndAlso Not TheState.bnoRow Then
                strID = ThePage.GetGuidStringFromByteArray(CType(dvRow(InvoiceRegionTax.InvoiceRegionTaxDV.COL_INVOICE_REGION_TAX_ID), Byte()))

                If (TheState.IsEditMode = True AndAlso TheState.DefaultInvoiceTransID.ToString.Equals(strID)) Then


                    Dim moRegionDropDown As DropDownList = CType(e.Row.Cells(GRID_COL_REGION_DESCRIPTION).FindControl(GRID_CTRL_NAME_EDIT_REGION), DropDownList)
                    Dim oCountry As Country = ElitaPlusIdentity.Current.ActiveUser.Country(ElitaPlusIdentity.Current.ActiveUser.CompanyId)
                    Dim CountryID As Guid = ElitaPlusIdentity.Current.ActiveUser.Country(ElitaPlusIdentity.Current.ActiveUser.CompanyId).Id
                    Dim dv As DataView = LookupListNew.GetRegionLookupList(oCountry.Id)
                    Dim existingValues As List(Of String)
                    existingValues = (From r In dtexistingvalues.AsEnumerable() Select r.Field(Of String)("REGION_DESCRIPTION")).ToList()
                    If TheState.MyBO.IsNew = False Then
                        existingValues.Remove(GridIIBBTaxes.DataKeys(e.Row.RowIndex).Values(GRID_COL_REGION_DESCRIPTION))
                    End If
                    Dim regionlist As EnumerableRowCollection(Of DataRow) = From d In dv.ToTable() Where Not existingValues.Contains(d.Field(Of String)("Description")) Select d
                    ElitaPlusPage.BindListControlToDataView(moRegionDropDown, regionlist.AsDataView(), "DESCRIPTION",, False)
                    'Dim regionId As String = dvRow(InvoiceRegionTax.InvoiceRegionTaxDV.COL_REGION_ID).ToString
                    If Not TheState.IsGridAddNew Then
                        Dim regionId As String = ThePage.GetGuidStringFromByteArray(CType(dvRow(InvoiceRegionTax.InvoiceRegionTaxDV.COL_REGION_ID), Byte()))
                        SetSelectedItem(moRegionDropDown, regionId)
                    End If
                    Dim cboinvoicetype As DropDownList = CType(e.Row.Cells(GRID_COL_TAX_TYPE).FindControl(GRID_CTRL_NAME_EDIT_INVOICE_TYPE), DropDownList)

                    Dim selectedIndex As Integer = cboinvoicetype.SelectedIndex
                    cboinvoicetype.Populate(CommonConfigManager.Current.ListManager.GetList("TTYP", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
             {
                .ValueFunc = AddressOf .GetExtendedCode,
                .TextFunc = AddressOf .GetDescription
             })
                    Dim taxtypedv As DataView = LookupListNew.GetTaxTypeList(Thread.CurrentPrincipal.GetLanguageId())
                    If TheState.IsGridAddNew = True Then
                        'cboinvoicetype.SelectedItem.Text = LookupListNew.GetDescriptionFromCode(taxtypedv, "PERCEPTION_IIBB")
                        'cboinvoicetype.SelectedItem.Value = "TTYP-PERCEPTION_IIBB"
                        SetSelectedItem(cboinvoicetype, "TTYP-PERCEPTION_IIBB")
                    Else
                        Dim code As String = dvRow(InvoiceRegionTax.InvoiceRegionTaxDV.COL_INVOICE_TYPE).ToString
                        SetSelectedItem(cboinvoicetype, code)
                    End If
                    'CType(e.Row.Cells(Me.GRID_COL_TAX_AMOUNT).FindControl(Me.GRID_CTRL_NAME_EDIT_IIBB_TAX), TextBox).Text = dvRow(InvoiceRegionTax.InvoiceRegionTaxDV.COL_TAX_AMOUNT).ToString

                    If Not dvRow.Row.IsNull(InvoiceRegionTax.InvoiceRegionTaxDV.COL_TAX_AMOUNT) Then
                        CType(e.Row.Cells(GRID_COL_TAX_AMOUNT).FindControl(GRID_CTRL_NAME_EDIT_IIBB_TAX), TextBox).Text = Page.GetAmountFormattedString(CType(dvRow(InvoiceRegionTax.InvoiceRegionTaxDV.COL_TAX_AMOUNT), Decimal))
                    End If

                Else

                    Dim replTaxTypeLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("TTYP", Thread.CurrentPrincipal.GetLanguageCode())
                    Dim InvDes As String
                    Dim code As String = dvRow(InvoiceRegionTax.InvoiceRegionTaxDV.COL_INVOICE_TYPE).ToString

                    For Each s As ListItem In replTaxTypeLkl
                        If (s.ExtendedCode = code) Then
                            InvDes = s.Translation
                            Exit For
                        End If
                    Next
                    CType(e.Row.Cells(GRID_COL_REGION).FindControl(GRID_CTRL_NAME_LABEL_REGION), Label).Text = dvRow(InvoiceRegionTax.InvoiceRegionTaxDV.COL_REGION_DESCRIPTION).ToString
                    CType(e.Row.Cells(GRID_COL_TAX_TYPE).FindControl(GRID_CTRL_NAME_LABEL_INVOICE_TYPE), Label).Text = InvDes
                    CType(e.Row.Cells(GRID_COL_TAX_AMOUNT).FindControl(GRID_CTRL_NAME_LABEL_IIBB_TAX), Label).Text = Page.GetAmountFormattedString(CType(dvRow(InvoiceRegionTax.InvoiceRegionTaxDV.COL_TAX_AMOUNT), Decimal))
                End If
            End If

        Catch ex As Exception
            ThePage.HandleErrors(ex, ThePage.MasterPage.MessageController)
        End Try

    End Sub

    Public Sub RowCommand(source As System.Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridIIBBTaxes.RowCommand

        Try
            Dim index As Integer
            If (e.CommandName = EDIT_COMMAND) Then
                index = CInt(e.CommandArgument)
                TheState.IsEditMode = True
                TheState.DefaultInvoiceTransID = GuidControl.ByteArrayToGuid(GridIIBBTaxes.DataKeys(index).Values(0))
                TheState.MyBO = New InvoiceRegionTax(InvoicetransId, TheState.DefaultInvoiceTransID)
                Populate()
                TheState.PageIndex = GridIIBBTaxes.PageIndex
                SetControlState()
                Try
                    GridIIBBTaxes.Rows(index).Focus()
                Catch ex As Exception
                    GridIIBBTaxes.Focus()
                End Try

            ElseIf (e.CommandName = DELETE_COMMAND) Then

                Try
                    index = CInt(e.CommandArgument)
                    TheState.DefaultInvoiceTransID = GuidControl.ByteArrayToGuid(GridIIBBTaxes.DataKeys(index).Values(0))
                    TheState.MyBO = New InvoiceRegionTax(InvoicetransId, TheState.DefaultInvoiceTransID)
                    TheState.MyBO.Delete()
                    TheState.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete
                    TheState.MyBO.Save()
                    TheState.IsAfterSave = True
                    Populate()
                    ThePage.MasterPage.MessageController.AddSuccess(Assurant.ElitaPlus.Common.ErrorCodes.MSG_RECORD_DELETED_OK, True)
                Catch ex As Exception
                    TheState.MyBO.RejectChanges()
                    Throw ex
                End Try

            End If
        Catch ex As Exception
            ThePage.HandleErrors(ex, ThePage.MasterPage.MessageController)
        End Try

    End Sub

    Protected Sub OnRowEditing(sender As Object, e As GridViewEditEventArgs)
        GridIIBBTaxes.EditIndex = e.NewEditIndex
    End Sub

    Private Sub SortAndBindGrid()

        Dim objInvoiceRegionTaxes As New InvoiceRegionTax
        TheState.PageIndex = GridIIBBTaxes.PageIndex

        If (TheState.InvoiceRegionTaxDV IsNot Nothing AndAlso TheState.InvoiceRegionTaxDV.Count = 0) Then
            Dim dv As DataView = objInvoiceRegionTaxes.GetInvoiceRegionTax()
            TheState.bnoRow = True
            If dv IsNot Nothing Then
                objInvoiceRegionTaxes.GetEmptyList(dv)
            End If
            TheState.InvoiceRegionTaxDV = Nothing
            TheState.MyBO = New InvoiceRegionTax
            If TheState.MyBO.IsNew Then
                TheState.MyBO.AddNewRowToSearchDV(TheState.InvoiceRegionTaxDV, TheState.MyBO)
            End If
            GridIIBBTaxes.DataSource = TheState.InvoiceRegionTaxDV
            ThePage.HighLightSortColumn(GridIIBBTaxes, SortDirection, True)
            GridIIBBTaxes.DataBind()
            If Not GridIIBBTaxes.BottomPagerRow.Visible Then GridIIBBTaxes.BottomPagerRow.Visible = True
            GridIIBBTaxes.Rows(0).Visible = False
            TheState.IsGridAddNew = True
            'Me.TheState.IsGridVisible = False
            lblRecordCount.Text = "0 " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            'If blnShowErr Then
            '    Me.ThePage.MasterPage.MessageController.AddInformation(ElitaPlus.ElitaPlusWebApp.Message.MSG_NO_RECORDS_FOUND, True)
            'End If
        Else
            TheState.bnoRow = False
            GridIIBBTaxes.Enabled = True
            GridIIBBTaxes.PageSize = TheState.PageSize
            GridIIBBTaxes.DataSource = TheState.InvoiceRegionTaxDV
            TheState.IsGridVisible = True
            ThePage.HighLightSortColumn(GridIIBBTaxes, SortDirection, True)
            GridIIBBTaxes.DataBind()
            If Not GridIIBBTaxes.BottomPagerRow.Visible Then GridIIBBTaxes.BottomPagerRow.Visible = True
        End If

        ControlMgr.SetVisibleControl(ThePage, GridIIBBTaxes, TheState.IsGridVisible)

        If GridIIBBTaxes.Visible AndAlso TheState.InvoiceRegionTaxDV IsNot Nothing Then

            Session("recCount") = TheState.InvoiceRegionTaxDV.Count
            If Not GridIIBBTaxes.BottomPagerRow.Visible Then GridIIBBTaxes.BottomPagerRow.Visible = True
            If (TheState.IsGridAddNew) Then
                lblRecordCount.Text = String.Format("{0} {1}", (TheState.InvoiceRegionTaxDV.Count - 1), TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND))
            Else
                lblRecordCount.Text = String.Format("{0} {1}", TheState.InvoiceRegionTaxDV.Count, TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND))
            End If
        End If
        ControlMgr.DisableEditDeleteGridIfNotEditAuth(ThePage, GridIIBBTaxes)

    End Sub

    Private Sub Grid_PageIndexChanged(sender As Object, e As System.EventArgs) Handles GridIIBBTaxes.PageIndexChanged
        Try
            If (Not (TheState.IsEditMode)) Then
                TheState.PageIndex = GridIIBBTaxes.PageIndex
                TheState.DefaultInvoiceTransID = Guid.Empty
                PopulateGrid()
            End If
        Catch ex As Exception
            ThePage.HandleErrors(ex, ThePage.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridIIBBTaxes.PageIndexChanging
        Try
            GridIIBBTaxes.PageIndex = e.NewPageIndex
            TheState.PageIndex = GridIIBBTaxes.PageIndex
        Catch ex As Exception
            ThePage.HandleErrors(ex, ThePage.MasterPage.MessageController)
        End Try
    End Sub

    Public Sub RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridIIBBTaxes.RowCreated
        Try
            ThePage.BaseItemCreated(sender, e)
        Catch ex As Exception
            ThePage.HandleErrors(ex, ThePage.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_SortCommand(source As Object, e As GridViewSortEventArgs) Handles GridIIBBTaxes.Sorting
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
            TheState.PageIndex = 0
            PopulateGrid()
        Catch ex As Exception
            ThePage.HandleErrors(ex, ThePage.MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Helper functions"

    Public Sub SetControlState()
        Dim recCount As Integer
        If (TheState.IsEditMode) Then
            ControlMgr.SetVisibleControl(ThePage, NewButton_WRITE, False)
            If (cboDiPageSize.Enabled) Then
                ControlMgr.SetEnableControl(ThePage, cboDiPageSize, False)
            End If

        Else
            If TheState.InvoiceRegionTaxDV IsNot Nothing Then
                recCount = TheState.InvoiceRegionTaxDV.Count
            End If
            If TheState.IsEditable Then
                If InvoiceStatus IsNot Nothing AndAlso InvoiceStatus <> String.Empty Then
                    If InvoiceStatus = INVOICE_STATUS_PAID OrElse InvoiceStatus = INVOICE_STATUS_REJECTED Then
                        ControlMgr.SetVisibleControl(ThePage, NewButton_WRITE, False)
                    Else
                        If recCount <= 23 Then
                            ControlMgr.SetVisibleControl(ThePage, NewButton_WRITE, True)
                        Else
                            ControlMgr.SetVisibleControl(ThePage, NewButton_WRITE, False)
                        End If
                    End If
                Else
                    If recCount <= 23 Then
                        ControlMgr.SetVisibleControl(ThePage, NewButton_WRITE, True)
                    Else
                        ControlMgr.SetVisibleControl(ThePage, NewButton_WRITE, False)
                    End If
                End If
            Else
                ControlMgr.SetVisibleControl(ThePage, NewButton_WRITE, False)
            End If
            If Not (cboDiPageSize.Enabled) Then
                ControlMgr.SetEnableControl(ThePage, cboDiPageSize, True)
            End If
        End If
    End Sub

    Private Sub ReturnFromEditing()

        GridIIBBTaxes.EditIndex = NO_ROW_SELECTED_INDEX

        If (GridIIBBTaxes.PageCount = 0) Then
            ControlMgr.SetVisibleControl(ThePage, GridIIBBTaxes, False)
        Else
            ControlMgr.SetVisibleControl(ThePage, GridIIBBTaxes, True)
        End If

        TheState.IsEditMode = False
        PopulateGrid()
        TheState.PageIndex = GridIIBBTaxes.PageIndex
        SetControlState()
    End Sub

    Private Sub RemoveNewRowFromSearchDV()
        Dim rowind As Integer = ThePage.NO_ITEM_SELECTED_INDEX
        With TheState
            If .InvoiceRegionTaxDV IsNot Nothing Then
                rowind = ThePage.FindSelectedRowIndexFromGuid(.InvoiceRegionTaxDV, .DefaultInvoiceTransID)
            End If
        End With
        If rowind <> ThePage.NO_ITEM_SELECTED_INDEX Then TheState.InvoiceRegionTaxDV.Delete(rowind)
    End Sub

    Private Sub AddNew()
        If TheState.InvoiceRegionTaxDV IsNot Nothing Then
            dtexistingvalues = TheState.InvoiceRegionTaxDV.Table.Copy()
        End If
        If TheState.MyBO Is Nothing OrElse TheState.MyBO.IsNew = False Then
            TheState.MyBO = New InvoiceRegionTax
            TheState.MyBO.AddNewRowToSearchDV(TheState.InvoiceRegionTaxDV, TheState.MyBO)
        End If
        TheState.DefaultInvoiceTransID = TheState.MyBO.Id
        TheState.IsGridAddNew = True
        PopulateGrid()
        'Set focus on the Code TextBox for the EditItemIndex row
        ThePage.SetPageAndSelectedIndexFromGuid(TheState.InvoiceRegionTaxDV, TheState.DefaultInvoiceTransID, GridIIBBTaxes,
                                                TheState.PageIndex, TheState.IsEditMode)
        ControlMgr.DisableEditDeleteGridIfNotEditAuth(ThePage, GridIIBBTaxes)
        ThePage.SetGridControls(GridIIBBTaxes, False)

        Try
            'Me.GridIIBBTaxes.Rows(Me.GridIIBBTaxes.SelectedIndex).Focus()
        Catch ex As Exception
            GridIIBBTaxes.Focus()
        End Try

    End Sub
    Private Function PopulateBOFromForm() As Boolean
        Dim objDropDownList As DropDownList
        Dim objTaxtypeList As DropDownList
        Dim objRegionDescription As DropDownList
        Dim IIBBTaxAmount As TextBox

        With TheState.MyBO

            IIBBTaxAmount = CType(GridIIBBTaxes.Rows(GridIIBBTaxes.EditIndex).Cells(GRID_COL_TAX_AMOUNT).FindControl(GRID_CTRL_NAME_EDIT_IIBB_TAX), TextBox)
            If (TheState.IsEditMode = True AndAlso TheState.IsGridAddNew = False) Then

                ThePage.PopulateBOProperty(TheState.MyBO, "InvoiceRegionTaxId", New Guid(CType(GridIIBBTaxes.DataKeys(GridIIBBTaxes.EditIndex).Values(GRID_COL_INVOICE_REGION_TAX_ID), Byte())))
            ElseIf TheState.IsGridAddNew = True Then
                ThePage.PopulateBOProperty(TheState.MyBO, "InvoiceRegionTaxId", Guid.NewGuid())
            End If

            ThePage.PopulateBOProperty(TheState.MyBO, "InvoiceTransactionId", InvoicetransId)

            objDropDownList = CType(GridIIBBTaxes.Rows((GridIIBBTaxes.EditIndex)).Cells(GRID_COL_REGION).FindControl(GRID_CTRL_NAME_EDIT_REGION), DropDownList)
            ThePage.PopulateBOProperty(TheState.MyBO, "RegionId", objDropDownList, True)
            objRegionDescription = CType(GridIIBBTaxes.Rows((GridIIBBTaxes.EditIndex)).Cells(GRID_COL_REGION_DESCRIPTION).FindControl(GRID_CTRL_NAME_EDIT_REGION), DropDownList)
            ThePage.PopulateBOProperty(TheState.MyBO, "RegionDescription", objRegionDescription, False, True)
            objTaxtypeList = CType(GridIIBBTaxes.Rows((GridIIBBTaxes.EditIndex)).Cells(GRID_COL_TAX_TYPE).FindControl(GRID_CTRL_NAME_EDIT_INVOICE_TYPE), DropDownList)
            ThePage.PopulateBOProperty(TheState.MyBO, "TaxType", objTaxtypeList, False, True)
            ThePage.PopulateBOProperty(TheState.MyBO, "TaxAmount", IIBBTaxAmount)

        End With
        If ThePage.ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If
    End Function
#End Region

#Region "Control Event Handlers"
    Protected Sub NewButton_WRITE_Click(sender As Object, e As EventArgs) Handles NewButton_WRITE.Click
        Try
            TheState.IsEditMode = True
            TheState.IsGridVisible = True
            TheState.IsGridAddNew = True
            AddNew()
            TheState.IsEditMode = False
            SetControlState()

        Catch ex As Exception
            ThePage.HandleErrors(ex, ThePage.MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)

        Try
            PopulateBOFromForm()

            If (TheState.MyBO.IsDirty) Then
                Try
                    TheState.MyBO.Save()
                Catch ex As DataBaseUniqueKeyConstraintViolationException
                    Throw New GUIException("Unique constraint violation", Assurant.ElitaPlus.Common.ErrorCodes.DUPLICATE_KEY_CONSTRAINT_VIOLATED)
                End Try

                TheState.IsAfterSave = True
                TheState.IsGridAddNew = False
                ThePage.MasterPage.MessageController.AddSuccess(MSG_RECORD_SAVED_OK, True)
                TheState.InvoiceRegionTaxDV = Nothing
                TheState.MyBO = Nothing
                ReturnFromEditing()
            Else
                ThePage.MasterPage.MessageController.AddWarning(MSG_RECORD_NOT_SAVED, True)
                ReturnFromEditing()
            End If
        Catch ex As Exception
            ThePage.HandleErrors(ex, ThePage.MasterPage.MessageController)
        End Try

    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try
            With TheState
                If .IsGridAddNew Then
                    RemoveNewRowFromSearchDV()
                    .IsGridAddNew = False
                    GridIIBBTaxes.PageIndex = .PageIndex
                End If
                .DefaultInvoiceTransID = Guid.Empty
                TheState.MyBO = Nothing
                .IsEditMode = False
            End With
            GridIIBBTaxes.EditIndex = ThePage.NO_ITEM_SELECTED_INDEX

            PopulateGrid()
            SetControlState()
            GridIIBBTaxes.Focus()
        Catch ex As Exception
            ThePage.HandleErrors(ex, ThePage.MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub GridIIBBTaxes_DataBound(sender As Object, e As EventArgs) Handles GridIIBBTaxes.DataBound
        If TheState.IsEditable Then
            If InvoiceStatus IsNot Nothing AndAlso InvoiceStatus <> String.Empty Then
                If InvoiceStatus = INVOICE_STATUS_PAID OrElse InvoiceStatus = INVOICE_STATUS_REJECTED Then
                    GridIIBBTaxes.Columns(GRID_COL_EDIT).Visible = False

                Else
                    GridIIBBTaxes.Columns(GRID_COL_EDIT).Visible = True
                End If
            Else
                GridIIBBTaxes.Columns(GRID_COL_EDIT).Visible = True
            End If
        Else
            GridIIBBTaxes.Columns(GRID_COL_EDIT).Visible = False
        End If
    End Sub

    Protected Sub cboDiPageSize_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cboDiPageSize.SelectedIndexChanged
        Try
            TheState.PageSize = CType(cboDiPageSize.SelectedValue, Integer)
            TheState.SelectedPageSize = TheState.PageSize
            TheState.PageIndex = NewCurrentPageIndex(GridIIBBTaxes, TheState.InvoiceRegionTaxDV.Count, TheState.PageSize)
            GridIIBBTaxes.PageIndex = TheState.PageIndex
            TheState.IsEditMode = False
            PopulateGrid()
        Catch ex As Exception
            Page.HandleErrors(ex, Page.MasterPage.MessageController)
        End Try
    End Sub

    Protected Function NewCurrentPageIndex(dg As GridView, intRecordCount As Integer, intNewPageSize As Integer) As Integer
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
            If intFirstRecordIndex >= (i - 1) * intNewPageSize + 1 AndAlso intFirstRecordIndex <= i * intNewPageSize Then
                intNewCurrentPageIndex = i - 1
                Exit For
            End If
        Next i

        NewCurrentPageIndex = intNewCurrentPageIndex

    End Function

    Public Sub SetSelectedItem(lstControl As ListControl, SelectItem As String)
        Dim item As System.Web.UI.WebControls.ListItem = lstControl.SelectedItem
        If item IsNot Nothing Then item.Selected = False
        Try
            lstControl.Items.FindByValue(SelectItem.ToString).Selected = True
            lstControl.Style.Remove("background")
        Catch ex As Exception
            lstControl.Style.Add("background", "red")
            Throw New GUIException(Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_VALUE, TranslationBase.TranslateLabelOrMessage(Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_VALUE) & ": ControlID : " & lstControl.ClientID & " for value : " & SelectItem.ToString(), ex)
        End Try
    End Sub
#End Region


End Class