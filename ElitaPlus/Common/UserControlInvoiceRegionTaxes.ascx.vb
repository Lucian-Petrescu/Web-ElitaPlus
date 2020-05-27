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

    Public Delegate Sub RequestData(ByVal sender As Object, ByRef e As RequestDataEventArgs)

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
            Return Me.TheState.invoicetransid
        End Get
        Set(ByVal value As Nullable(Of Guid))
            Me.TheState.invoicetransid = CType(value, Guid)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("invoice_trans_id"))
        End Set
    End Property

    Public Property InvoiceStatus() As String
        Get
            Return Me.TheState.InvoiceStatus
        End Get
        Set(ByVal value As String)
            Me.TheState.InvoiceStatus = value
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
                If Me.ThePage.StateSession.Item(Me.UniqueID) Is Nothing Then
                    Me.ThePage.StateSession.Item(Me.UniqueID) = New MyState
                End If
                Return CType(Me.ThePage.StateSession.Item(Me.UniqueID), MyState)

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
            Return Me.GridIIBBTaxes.EditIndex > Me.ThePage.NO_ITEM_SELECTED_INDEX
        End Get
    End Property

    Public Property IsGridEditable() As Boolean
        Get
            Return Me.TheState.IsEditable
        End Get
        Set(ByVal value As Boolean)
            Me.TheState.IsEditable = value
        End Set
    End Property

    Public Property SortDirection() As String
        Get
            If Not ViewState("SortDirection") Is Nothing Then
                Return ViewState("SortDirection").ToString
            Else
                Return InvoiceRegionTax.COL_NAME_REGION_DESCRIPTION
            End If

        End Get
        Set(ByVal value As String)
            ViewState("SortDirection") = value
        End Set
    End Property

    'Public Shared Property InvoiceTransactionId As String
    'Public Property InvoiceStatus As String

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'SetControlState()
            If Page.IsPostBack Then
                Dim confResponse As String = Me.HiddenDIDeletePromptResponse.Value
                Dim confResponseDel As String = Me.HiddenDIDeletePromptResponse.Value
                If Not confResponseDel Is Nothing AndAlso confResponseDel = Me.ThePage.MSG_VALUE_YES Then
                    If Me.TheState.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete Then
                        Me.TheState.DefaultInvoiceTransID = GuidControl.ByteArrayToGuid(GridIIBBTaxes.DataKeys(Me.TheState.deleteRowIndex).Values(0))
                        Me.ThePage.MasterPage.MessageController.AddSuccess(Me.MSG_RECORD_DELETED_OK, True)
                        Me.TheState.PageIndex = GridIIBBTaxes.PageIndex
                        Me.TheState.IsAfterSave = True
                        Me.TheState.InvoiceRegionTaxDV = Nothing
                        Me.TheState.PageIndex = GridIIBBTaxes.PageIndex
                        Me.TheState.IsEditMode = False
                        'SetControlState()
                        Me.TheState.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                        Me.HiddenDIDeletePromptResponse.Value = ""
                    End If
                ElseIf Not confResponseDel Is Nothing AndAlso confResponseDel = Me.ThePage.MSG_VALUE_NO Then
                    Me.TheState.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                    Me.HiddenDIDeletePromptResponse.Value = ""
                End If
            Else
                SortDirection = InvoiceRegionTax.COL_NAME_REGION_DESCRIPTION
            End If
            SetControlState()

        Catch ex As Exception
            Me.ThePage.HandleErrors(ex, Me.ThePage.MasterPage.MessageController)
        End Try

    End Sub

#Region "Grid related"


    Public Sub Populate()

        Dim e As New RequestDataEventArgs
        If (Me.InvoicetransId.Equals(Guid.Empty)) Then
            Throw New GUIException("You must select a region", Assurant.ElitaPlus.Common.ErrorCodes.GUI_REGION_MUST_BE_SELECTED_ERR)
        End If

        RaiseEvent RequestIIBBTaxesData(Me, e)
        Me.TheState.InvoiceRegionTaxDV = e.Data
        Me.PopulateGrid()

    End Sub

    Public Sub PopulateGrid()

        If (Not Page.IsPostBack) Then
            Me.ThePage.TranslateGridHeader(GridIIBBTaxes)
        End If

        Dim blnNewSearch As Boolean = False
        cboDiPageSize.SelectedValue = CType(Me.TheState.PageSize, String)
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

            If Not TheState.InvoiceRegionTaxDV Is Nothing AndAlso Not TheState.IsGridAddNew = True Then
                TheState.InvoiceRegionTaxDV.Sort = SortDirection
            End If

            If (Me.TheState.IsAfterSave) Then
                Me.TheState.IsAfterSave = False
                Me.ThePage.SetPageAndSelectedIndexFromGuid(Me.TheState.InvoiceRegionTaxDV, Me.TheState.DefaultInvoiceTransID, Me.GridIIBBTaxes, Me.TheState.PageIndex)
            ElseIf (Me.TheState.IsEditMode) Then
                Me.ThePage.SetPageAndSelectedIndexFromGuid(Me.TheState.InvoiceRegionTaxDV, Me.TheState.DefaultInvoiceTransID, Me.GridIIBBTaxes, Me.TheState.PageIndex, Me.TheState.IsEditMode)
            Else
                If Not TheState.InvoiceRegionTaxDV Is Nothing Then
                    Me.ThePage.SetPageAndSelectedIndexFromGuid(Me.TheState.InvoiceRegionTaxDV, Guid.Empty, Me.GridIIBBTaxes, Me.TheState.PageIndex)
                End If
            End If
            GridIIBBTaxes.Columns(GRID_COL_REGION).SortExpression = InvoiceRegionTax.COL_NAME_REGION_DESCRIPTION

            If Not TheState.InvoiceRegionTaxDV Is Nothing AndAlso Me.TheState.InvoiceRegionTaxDV.Count = 0 Then
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
            Me.GridIIBBTaxes.AutoGenerateColumns = False
            SortAndBindGrid()

            Me.SetControlState()
        Catch ex As Exception
            Me.ThePage.HandleErrors(ex, Me.ThePage.MasterPage.MessageController)
        End Try

    End Sub

    Private Sub Grid_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridIIBBTaxes.RowDataBound
        Try
            Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
            Dim strID As String '= InvoiceTransactionId

            If Not dvRow Is Nothing And Not Me.TheState.bnoRow Then
                strID = Me.ThePage.GetGuidStringFromByteArray(CType(dvRow(InvoiceRegionTax.InvoiceRegionTaxDV.COL_INVOICE_REGION_TAX_ID), Byte()))

                If (Me.TheState.IsEditMode = True AndAlso Me.TheState.DefaultInvoiceTransID.ToString.Equals(strID)) Then


                    Dim moRegionDropDown As DropDownList = CType(e.Row.Cells(GRID_COL_REGION_DESCRIPTION).FindControl(Me.GRID_CTRL_NAME_EDIT_REGION), DropDownList)
                    Dim oCountry As Country = ElitaPlusIdentity.Current.ActiveUser.Country(ElitaPlusIdentity.Current.ActiveUser.CompanyId)
                    Dim CountryID As Guid = ElitaPlusIdentity.Current.ActiveUser.Country(ElitaPlusIdentity.Current.ActiveUser.CompanyId).Id
                    Dim dv As DataView = LookupListNew.GetRegionLookupList(oCountry.Id)
                    Dim existingValues As List(Of String)
                    existingValues = (From r In dtexistingvalues.AsEnumerable() Select r.Field(Of String)("REGION_DESCRIPTION")).ToList()
                    If Me.TheState.MyBO.IsNew = False Then
                        existingValues.Remove(GridIIBBTaxes.DataKeys(e.Row.RowIndex).Values(GRID_COL_REGION_DESCRIPTION))
                    End If
                    Dim regionlist As EnumerableRowCollection(Of DataRow) = From d In dv.ToTable() Where Not existingValues.Contains(d.Field(Of String)("Description")) Select d
                    ElitaPlusPage.BindListControlToDataView(moRegionDropDown, regionlist.AsDataView(), "DESCRIPTION",, False)
                    'Dim regionId As String = dvRow(InvoiceRegionTax.InvoiceRegionTaxDV.COL_REGION_ID).ToString
                    If Not Me.TheState.IsGridAddNew Then
                        Dim regionId As String = Me.ThePage.GetGuidStringFromByteArray(CType(dvRow(InvoiceRegionTax.InvoiceRegionTaxDV.COL_REGION_ID), Byte()))
                        SetSelectedItem(moRegionDropDown, regionId)
                    End If
                    Dim cboinvoicetype As DropDownList = CType(e.Row.Cells(Me.GRID_COL_TAX_TYPE).FindControl(Me.GRID_CTRL_NAME_EDIT_INVOICE_TYPE), DropDownList)

                    Dim selectedIndex As Integer = cboinvoicetype.SelectedIndex
                        cboinvoicetype.Populate(CommonConfigManager.Current.ListManager.GetList("TTYP", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                 {
                    .ValueFunc = AddressOf .GetExtendedCode,
                    .TextFunc = AddressOf .GetDescription
                 })
                        Dim taxtypedv As DataView = LookupListNew.GetTaxTypeList(Thread.CurrentPrincipal.GetLanguageId())
                        If Me.TheState.IsGridAddNew = True Then
                        'cboinvoicetype.SelectedItem.Text = LookupListNew.GetDescriptionFromCode(taxtypedv, "PERCEPTION_IIBB")
                        'cboinvoicetype.SelectedItem.Value = "TTYP-PERCEPTION_IIBB"
                        SetSelectedItem(cboinvoicetype, "TTYP-PERCEPTION_IIBB")
                    Else
                            Dim code As String = dvRow(InvoiceRegionTax.InvoiceRegionTaxDV.COL_INVOICE_TYPE).ToString
                        SetSelectedItem(cboinvoicetype, code)
                    End If
                        CType(e.Row.Cells(Me.GRID_COL_TAX_AMOUNT).FindControl(Me.GRID_CTRL_NAME_EDIT_IIBB_TAX), TextBox).Text = dvRow(InvoiceRegionTax.InvoiceRegionTaxDV.COL_TAX_AMOUNT).ToString

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
                    CType(e.Row.Cells(Me.GRID_COL_REGION).FindControl(Me.GRID_CTRL_NAME_LABEL_REGION), Label).Text = dvRow(InvoiceRegionTax.InvoiceRegionTaxDV.COL_REGION_DESCRIPTION).ToString
                    CType(e.Row.Cells(Me.GRID_COL_TAX_TYPE).FindControl(Me.GRID_CTRL_NAME_LABEL_INVOICE_TYPE), Label).Text = InvDes
                End If
            End If

        Catch ex As Exception
            Me.ThePage.HandleErrors(ex, Me.ThePage.MasterPage.MessageController)
        End Try

    End Sub

    Public Sub RowCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridIIBBTaxes.RowCommand

        Try
            Dim index As Integer
            If (e.CommandName = Me.EDIT_COMMAND) Then
                index = CInt(e.CommandArgument)
                Me.TheState.IsEditMode = True
                Me.TheState.DefaultInvoiceTransID = GuidControl.ByteArrayToGuid(GridIIBBTaxes.DataKeys(index).Values(0))
                Me.TheState.MyBO = New InvoiceRegionTax(InvoicetransId, Me.TheState.DefaultInvoiceTransID)
                Me.Populate()
                Me.TheState.PageIndex = GridIIBBTaxes.PageIndex
                Me.SetControlState()
                Try
                    Me.GridIIBBTaxes.Rows(index).Focus()
                Catch ex As Exception
                    Me.GridIIBBTaxes.Focus()
                End Try

            ElseIf (e.CommandName = Me.DELETE_COMMAND) Then

                Try
                    index = CInt(e.CommandArgument)
                    Me.TheState.DefaultInvoiceTransID = GuidControl.ByteArrayToGuid(GridIIBBTaxes.DataKeys(index).Values(0))
                    Me.TheState.MyBO = New InvoiceRegionTax(InvoicetransId, Me.TheState.DefaultInvoiceTransID)
                    Me.TheState.MyBO.Delete()
                    Me.TheState.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete
                    Me.TheState.MyBO.Save()
                    Me.TheState.IsAfterSave = True
                    Populate()
                    Me.ThePage.MasterPage.MessageController.AddSuccess(Assurant.ElitaPlus.Common.ErrorCodes.MSG_RECORD_DELETED_OK, True)
                Catch ex As Exception
                    Me.TheState.MyBO.RejectChanges()
                    Throw ex
                End Try

            End If
        Catch ex As Exception
            Me.ThePage.HandleErrors(ex, Me.ThePage.MasterPage.MessageController)
        End Try

    End Sub

    Protected Sub OnRowEditing(sender As Object, e As GridViewEditEventArgs)
        GridIIBBTaxes.EditIndex = e.NewEditIndex
    End Sub

    Private Sub SortAndBindGrid()

        Dim objInvoiceRegionTaxes As New InvoiceRegionTax
        Me.TheState.PageIndex = Me.GridIIBBTaxes.PageIndex

        If (Not Me.TheState.InvoiceRegionTaxDV Is Nothing AndAlso Me.TheState.InvoiceRegionTaxDV.Count = 0) Then
            Dim dv As DataView = objInvoiceRegionTaxes.GetInvoiceRegionTax()
            Me.TheState.bnoRow = True
            If Not dv Is Nothing Then
                objInvoiceRegionTaxes.GetEmptyList(dv)
            End If
            Me.TheState.InvoiceRegionTaxDV = Nothing
            Me.TheState.MyBO = New InvoiceRegionTax
            If Me.TheState.MyBO.IsNew Then
                TheState.MyBO.AddNewRowToSearchDV(Me.TheState.InvoiceRegionTaxDV, Me.TheState.MyBO)
            End If
            Me.GridIIBBTaxes.DataSource = Me.TheState.InvoiceRegionTaxDV
            Me.ThePage.HighLightSortColumn(GridIIBBTaxes, Me.SortDirection, True)
            Me.GridIIBBTaxes.DataBind()
            If Not GridIIBBTaxes.BottomPagerRow.Visible Then GridIIBBTaxes.BottomPagerRow.Visible = True
            Me.GridIIBBTaxes.Rows(0).Visible = False
            Me.TheState.IsGridAddNew = True
            'Me.TheState.IsGridVisible = False
            Me.lblRecordCount.Text = "0 " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            'If blnShowErr Then
            '    Me.ThePage.MasterPage.MessageController.AddInformation(ElitaPlus.ElitaPlusWebApp.Message.MSG_NO_RECORDS_FOUND, True)
            'End If
        Else
            Me.TheState.bnoRow = False
            Me.GridIIBBTaxes.Enabled = True
            Me.GridIIBBTaxes.PageSize = Me.TheState.PageSize
            Me.GridIIBBTaxes.DataSource = Me.TheState.InvoiceRegionTaxDV
            Me.TheState.IsGridVisible = True
            Me.ThePage.HighLightSortColumn(GridIIBBTaxes, Me.SortDirection, True)
            Me.GridIIBBTaxes.DataBind()
            If Not GridIIBBTaxes.BottomPagerRow.Visible Then GridIIBBTaxes.BottomPagerRow.Visible = True
        End If

        ControlMgr.SetVisibleControl(Me.ThePage, GridIIBBTaxes, Me.TheState.IsGridVisible)

        If Me.GridIIBBTaxes.Visible AndAlso Not Me.TheState.InvoiceRegionTaxDV Is Nothing Then

            Session("recCount") = Me.TheState.InvoiceRegionTaxDV.Count
            If Not GridIIBBTaxes.BottomPagerRow.Visible Then GridIIBBTaxes.BottomPagerRow.Visible = True
            If (Me.TheState.IsGridAddNew) Then
                Me.lblRecordCount.Text = String.Format("{0} {1}", (Me.TheState.InvoiceRegionTaxDV.Count - 1), TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND))
            Else
                Me.lblRecordCount.Text = String.Format("{0} {1}", Me.TheState.InvoiceRegionTaxDV.Count, TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND))
            End If
        End If
        ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me.ThePage, GridIIBBTaxes)

    End Sub

    Private Sub Grid_PageIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridIIBBTaxes.PageIndexChanged
        Try
            If (Not (Me.TheState.IsEditMode)) Then
                Me.TheState.PageIndex = GridIIBBTaxes.PageIndex
                Me.TheState.DefaultInvoiceTransID = Guid.Empty
                Me.PopulateGrid()
            End If
        Catch ex As Exception
            Me.ThePage.HandleErrors(ex, Me.ThePage.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridIIBBTaxes.PageIndexChanging
        Try
            GridIIBBTaxes.PageIndex = e.NewPageIndex
            TheState.PageIndex = GridIIBBTaxes.PageIndex
        Catch ex As Exception
            Me.ThePage.HandleErrors(ex, Me.ThePage.MasterPage.MessageController)
        End Try
    End Sub

    Public Sub RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridIIBBTaxes.RowCreated
        Try
            ThePage.BaseItemCreated(sender, e)
        Catch ex As Exception
            Me.ThePage.HandleErrors(ex, Me.ThePage.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_SortCommand(ByVal source As Object, ByVal e As GridViewSortEventArgs) Handles GridIIBBTaxes.Sorting
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
            Me.TheState.PageIndex = 0
            PopulateGrid()
        Catch ex As Exception
            Me.ThePage.HandleErrors(ex, Me.ThePage.MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Helper functions"

    Public Sub SetControlState()
        Dim recCount As Integer
        If (Me.TheState.IsEditMode) Then
            ControlMgr.SetVisibleControl(Me.ThePage, NewButton_WRITE, False)
            If (Me.cboDiPageSize.Enabled) Then
                ControlMgr.SetEnableControl(Me.ThePage, cboDiPageSize, False)
            End If

        Else
            If Not TheState.InvoiceRegionTaxDV Is Nothing Then
                recCount = TheState.InvoiceRegionTaxDV.Count
            End If
            If Me.TheState.IsEditable Then
                If Not Me.InvoiceStatus Is Nothing And Me.InvoiceStatus <> String.Empty Then
                    If Me.InvoiceStatus = INVOICE_STATUS_PAID Or Me.InvoiceStatus = INVOICE_STATUS_REJECTED Then
                        ControlMgr.SetVisibleControl(Me.ThePage, NewButton_WRITE, False)
                    Else
                        If recCount <= 23 Then
                            ControlMgr.SetVisibleControl(Me.ThePage, NewButton_WRITE, True)
                        Else
                            ControlMgr.SetVisibleControl(Me.ThePage, NewButton_WRITE, False)
                        End If
                    End If
                Else
                    If recCount <= 23 Then
                        ControlMgr.SetVisibleControl(Me.ThePage, NewButton_WRITE, True)
                    Else
                        ControlMgr.SetVisibleControl(Me.ThePage, NewButton_WRITE, False)
                    End If
                End If
            Else
                ControlMgr.SetVisibleControl(Me.ThePage, NewButton_WRITE, False)
            End If
            If Not (Me.cboDiPageSize.Enabled) Then
                ControlMgr.SetEnableControl(Me.ThePage, Me.cboDiPageSize, True)
            End If
        End If
    End Sub

    Private Sub ReturnFromEditing()

        GridIIBBTaxes.EditIndex = NO_ROW_SELECTED_INDEX

        If (Me.GridIIBBTaxes.PageCount = 0) Then
            ControlMgr.SetVisibleControl(Me.ThePage, GridIIBBTaxes, False)
        Else
            ControlMgr.SetVisibleControl(Me.ThePage, GridIIBBTaxes, True)
        End If

        Me.TheState.IsEditMode = False
        Me.PopulateGrid()
        Me.TheState.PageIndex = GridIIBBTaxes.PageIndex
        SetControlState()
    End Sub

    Private Sub RemoveNewRowFromSearchDV()
        Dim rowind As Integer = Me.ThePage.NO_ITEM_SELECTED_INDEX
        With TheState
            If Not .InvoiceRegionTaxDV Is Nothing Then
                rowind = Me.ThePage.FindSelectedRowIndexFromGuid(.InvoiceRegionTaxDV, .DefaultInvoiceTransID)
            End If
        End With
        If rowind <> Me.ThePage.NO_ITEM_SELECTED_INDEX Then TheState.InvoiceRegionTaxDV.Delete(rowind)
    End Sub

    Private Sub AddNew()
        If Not Me.TheState.InvoiceRegionTaxDV Is Nothing Then
            dtexistingvalues = Me.TheState.InvoiceRegionTaxDV.Table.Copy()
        End If
        If TheState.MyBO Is Nothing OrElse Me.TheState.MyBO.IsNew = False Then
            TheState.MyBO = New InvoiceRegionTax
            TheState.MyBO.AddNewRowToSearchDV(Me.TheState.InvoiceRegionTaxDV, Me.TheState.MyBO)
        End If
        TheState.DefaultInvoiceTransID = Me.TheState.MyBO.Id
        TheState.IsGridAddNew = True
        PopulateGrid()
        'Set focus on the Code TextBox for the EditItemIndex row
        ThePage.SetPageAndSelectedIndexFromGuid(Me.TheState.InvoiceRegionTaxDV, Me.TheState.DefaultInvoiceTransID, Me.GridIIBBTaxes,
                                                TheState.PageIndex, Me.TheState.IsEditMode)
        ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me.ThePage, GridIIBBTaxes)
        ThePage.SetGridControls(Me.GridIIBBTaxes, False)

        Try
            'Me.GridIIBBTaxes.Rows(Me.GridIIBBTaxes.SelectedIndex).Focus()
        Catch ex As Exception
            Me.GridIIBBTaxes.Focus()
        End Try

    End Sub
    Private Function PopulateBOFromForm() As Boolean
        Dim objDropDownList As DropDownList
        Dim objTaxtypeList As DropDownList
        Dim objRegionDescription As DropDownList
        Dim IIBBTaxAmount As TextBox

        With Me.TheState.MyBO

            IIBBTaxAmount = CType(GridIIBBTaxes.Rows(Me.GridIIBBTaxes.EditIndex).Cells(GRID_COL_TAX_AMOUNT).FindControl(GRID_CTRL_NAME_EDIT_IIBB_TAX), TextBox)

            If (Me.TheState.IsEditMode = True AndAlso Me.TheState.IsGridAddNew = False) Then

                Me.ThePage.PopulateBOProperty(TheState.MyBO, "InvoiceRegionTaxId", New Guid(CType(GridIIBBTaxes.DataKeys(GridIIBBTaxes.EditIndex).Values(GRID_COL_INVOICE_REGION_TAX_ID), Byte())))
            ElseIf Me.TheState.IsGridAddNew = True Then
                Me.ThePage.PopulateBOProperty(TheState.MyBO, "InvoiceRegionTaxId", Guid.NewGuid())
            End If

            Me.ThePage.PopulateBOProperty(TheState.MyBO, "InvoiceTransactionId", InvoicetransId)

            objDropDownList = CType(GridIIBBTaxes.Rows((Me.GridIIBBTaxes.EditIndex)).Cells(GRID_COL_REGION).FindControl(GRID_CTRL_NAME_EDIT_REGION), DropDownList)
            Me.ThePage.PopulateBOProperty(TheState.MyBO, "RegionId", objDropDownList, True)
            objRegionDescription = CType(GridIIBBTaxes.Rows((Me.GridIIBBTaxes.EditIndex)).Cells(GRID_COL_REGION_DESCRIPTION).FindControl(GRID_CTRL_NAME_EDIT_REGION), DropDownList)
            Me.ThePage.PopulateBOProperty(TheState.MyBO, "RegionDescription", objRegionDescription, False, True)
            objTaxtypeList = CType(GridIIBBTaxes.Rows((Me.GridIIBBTaxes.EditIndex)).Cells(GRID_COL_TAX_TYPE).FindControl(GRID_CTRL_NAME_EDIT_INVOICE_TYPE), DropDownList)
            Me.ThePage.PopulateBOProperty(TheState.MyBO, "TaxType", objTaxtypeList, False, True)
            Me.ThePage.PopulateBOProperty(TheState.MyBO, "TaxAmount", IIBBTaxAmount)

        End With
        If Me.ThePage.ErrCollection.Count > 0 Then
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
            Me.SetControlState()

        Catch ex As Exception
            Me.ThePage.HandleErrors(ex, Me.ThePage.MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs)

        Try
            PopulateBOFromForm()

            If (Me.TheState.MyBO.IsDirty) Then
                Try
                    Me.TheState.MyBO.Save()
                Catch ex As DataBaseUniqueKeyConstraintViolationException
                    Throw New GUIException("Unique constraint violation", Assurant.ElitaPlus.Common.ErrorCodes.DUPLICATE_KEY_CONSTRAINT_VIOLATED)
                End Try

                Me.TheState.IsAfterSave = True
                Me.TheState.IsGridAddNew = False
                Me.ThePage.MasterPage.MessageController.AddSuccess(Me.MSG_RECORD_SAVED_OK, True)
                Me.TheState.InvoiceRegionTaxDV = Nothing
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
                    GridIIBBTaxes.PageIndex = .PageIndex
                End If
                .DefaultInvoiceTransID = Guid.Empty
                Me.TheState.MyBO = Nothing
                .IsEditMode = False
            End With
            GridIIBBTaxes.EditIndex = Me.ThePage.NO_ITEM_SELECTED_INDEX

            PopulateGrid()
            SetControlState()
            Me.GridIIBBTaxes.Focus()
        Catch ex As Exception
            Me.ThePage.HandleErrors(ex, Me.ThePage.MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub GridIIBBTaxes_DataBound(sender As Object, e As EventArgs) Handles GridIIBBTaxes.DataBound
        If Me.TheState.IsEditable Then
            If Not Me.InvoiceStatus Is Nothing And Me.InvoiceStatus <> String.Empty Then
                If Me.InvoiceStatus = INVOICE_STATUS_PAID Or Me.InvoiceStatus = INVOICE_STATUS_REJECTED Then
                    Me.GridIIBBTaxes.Columns(GRID_COL_EDIT).Visible = False

                Else
                    Me.GridIIBBTaxes.Columns(GRID_COL_EDIT).Visible = True
                End If
            Else
                Me.GridIIBBTaxes.Columns(GRID_COL_EDIT).Visible = True
            End If
        Else
            Me.GridIIBBTaxes.Columns(GRID_COL_EDIT).Visible = False
        End If
    End Sub

    Protected Sub cboDiPageSize_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboDiPageSize.SelectedIndexChanged
        Try
            Me.TheState.PageSize = CType(cboDiPageSize.SelectedValue, Integer)
            Me.TheState.SelectedPageSize = Me.TheState.PageSize
            Me.TheState.PageIndex = NewCurrentPageIndex(GridIIBBTaxes, TheState.InvoiceRegionTaxDV.Count, TheState.PageSize)
            Me.GridIIBBTaxes.PageIndex = Me.TheState.PageIndex
            Me.TheState.IsEditMode = False
            Me.PopulateGrid()
        Catch ex As Exception
            Me.Page.HandleErrors(ex, Me.Page.MasterPage.MessageController)
        End Try
    End Sub

    Protected Function NewCurrentPageIndex(ByVal dg As GridView, ByVal intRecordCount As Integer, ByVal intNewPageSize As Integer) As Integer
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

    Public Sub SetSelectedItem(ByVal lstControl As ListControl, ByVal SelectItem As String)
        Dim item As System.Web.UI.WebControls.ListItem = lstControl.SelectedItem
        If Not item Is Nothing Then item.Selected = False
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