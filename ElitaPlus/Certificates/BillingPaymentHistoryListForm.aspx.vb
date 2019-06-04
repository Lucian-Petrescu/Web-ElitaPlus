Imports System.Collections.Generic

Namespace Certificates
    Public Class BillingPaymentHistoryListForm
        Inherits ElitaPlusSearchPage

#Region "Constants"
        Protected WithEvents moCertificateInfoController As Global.Assurant.ElitaPlus.ElitaPlusWebApp.Certificates.UserControlCertificateInfo_New

        Private Const EDIT_COMMAND As String = "SelectAction"
        Private Const PAID_COMMAND As String = "PaidAction"
        Private Const REJECT_COMMAND As String = "SelectRecord"


        'Actions
        Private Const ACTION_NONE As String = "ACTION_NONE"
        Private Const ACTION_SAVE As String = "ACTION_SAVE"
        Private Const ACTION_NO_EDIT As String = "ACTION_NO_EDIT"
        Private Const ACTION_EDIT As String = "ACTION_EDIT"

        Public Const URL As String = "~/Certificates/BillingPaymentHistoryListForm.aspx"
        Public Const PAGETITLE As String = "BILLING_COLLECTION_HISTORY"
        Public Const PAGETAB As String = "CERTIFICATES"

        'Billing&Payment Grid
        Public Const GRID_COL_BILLING_DETAIL_ID As Integer = 1
        Public Const GRID_COL_COVERAGE_SEQUENCE As Integer = 2
        Public Const GRID_COL_INSTALLMENT_NUMBER As Integer = 3
        Public Const GRID_COL_DATE_PROCESSED As Integer = 4
        Public Const GRID_COL_BILLING_DATE As Integer = 5
        Public Const GRID_COL_FROM_DATE As Integer = 6
        Public Const GRID_COL_TO_DATE As Integer = 7
        Public Const GRID_COL_INCOMING_AMOUNT As Integer = 8
        Public Const GRID_COL_BILLED_AMOUNT As Integer = 9
        Public Const GRID_COL_OPEN_AMOUNT As Integer = 10
        Public Const GRID_COL_BILLING_STATUS As Integer = 11
        Public Const GRID_COL_REJECT_CODE As Integer = 12
        Public Const GRID_COL_SOURCE As Integer = 13
        Public Const GRID_COL_PAID As Integer = 14
        Public Const GRID_COL_INVOICE_NUMBER As Integer = 15

        'PayCollection Grid        
        Public Const GRID_COL_PAYMENT_ID As Integer = 1
        Public Const GRID_COL_COVERAGE_SEQUENCE_PC As Integer = 2
        Public Const GRID_COL_INSTALLMENT_NUMB_PC As Integer = 3
        Public Const GRID_COL_COLLECTED_DATE As Integer = 4
        Public Const GRID_COL_DATE_PROCESSED_PC As Integer = 5
        Public Const GRID_COL_PAYMENT_AMOUNT As Integer = 6
        Public Const GRID_COL_COLLECTED_AMOUNT As Integer = 7
        Public Const GRID_COL_PAYMENT_STATUS As Integer = 8
        Public Const GRID_COL_DATE_SEND As Integer = 9
        Public Const GRID_COL_PAYMENT_METHOD As Integer = 10
        Public Const GRID_COL_PAYMENT_INSTRUMENT_NUMBER As Integer = 11
        Public Const GRID_COL_REJECT_DATE_PC As Integer = 12
        Public Const GRID_COL_REJECT_CODE_PC As Integer = 13
        Public Const GRID_COL_REJECT_REASON_PC As Integer = 14
        'Public Const GRID_COL_SOURCE As Integer = 13


        'Public Const COL_ORDER_BY As String = "created_date1, installment_number"
        Public Const COL_PAY_ORDER_BY As String = "installment_number"

        Public Const COL_BILLING_STATUS_ID As String = "BILLING_STATUS_ID"
        Public Const COL_REJECT_CODE_ID As String = "REJECT_CODE_ID"
        Public Const COL_REJECT_PAID_ID As String = "PAID_ID"

        Public Const BILLING_STATUS_ACTIVE As String = "A"
        Public Const BILLING_STATUS_REJECT As String = "R"
        Public Const BILLING_STATUS_ONHOLD As String = "H"

        Public Const CREATE_NEW_REJECT_PAYMENT As String = "new_reject_payment"
        Public Const CREATE_NEW_CHECK_PAYMENT As String = "new_check_payment"


#End Region
#Region "Tabs"
        Public Const Tab_BillingHistory As String = "0"
        Public Const Tab_PaymentCollected As String = "1"

        Dim DisabledTabsList As New List(Of String)()

#End Region

#Region "Page State"
        Class BaseState
            Public NavCtrl As INavigationController
        End Class

        ' This class keeps the current state for the page.
        Class MyState
            Public MyBO_P As BillingPayDetail
            Public MyBO_C As CollectedPayDetail

            Public oCert As Certificate
            Public oCertInstallment As CertInstallment
            Public oBillPayTotalAmount As Decimal = 0
            Public oBillPayCount As Int32

            Public oCollectedTotalAmount As Decimal = 0
            Public oCollectedCount As Int32

            Public oDv As DataView = Nothing
            Public searchBillPayDV As DataView = Nothing
            Public SelectBillPayHistId As Guid = Guid.Empty
            Public searchCollectedDV As DataView = Nothing

            Public IsBillPayGridVisible As Boolean
            Public IsCollectionGridVisible As Boolean

            Public BillPayGridPageIndex As Integer = 0
            Public CollectedGridPageIndex As Integer = 0

            Public BillPayGridPageSize As Integer = 10
            Public CollectedGridPageSize As Integer = 10

            Public BillPaySortExp As String = Nothing
            Public CollectedSortExp As String = Nothing

            Public IsInstallmentPayment As Boolean
            Public IsEditMode As Boolean
            Public IsAfterSave As Boolean

            Public SelectPaymentId As Guid = Guid.Empty
            Public PayInstrumentNo As String = Nothing
            Public PaymentDate As String = Nothing


        End Class

        Public Sub New()
            MyBase.New(New BaseState)
        End Sub


#End Region

#Region "Properties"

        Public ReadOnly Property GridObject() As DataGrid
            Get
                Return Me.Grid
            End Get
        End Property

        Public ReadOnly Property PayGridObject() As DataGrid
            Get
                Return Me.CollectionGrid
            End Get
        End Property

        Public ReadOnly Property UserCertificateCtr() As UserControlCertificateInfo_New
            Get
                If moCertificateInfoController Is Nothing Then
                    moCertificateInfoController = CType(FindControl("moCertificateInfoController"), UserControlCertificateInfo_New)
                End If
                Return moCertificateInfoController
            End Get
        End Property

        Public ReadOnly Property GetCompanyCode() As String
            Get
                Dim companyBO As Company = New Company(Me.State.oCert.CompanyId)

                Return companyBO.Code
            End Get

        End Property

        Public ReadOnly Property GetInstallmentPaymentFlag() As Boolean
            Get
                Dim contractBO As Contract
                contractBO = Contract.GetContract(Me.State.oCert.DealerId, CDate(Me.State.oCert.WarrantySalesDate))

                If Not contractBO Is Nothing Then
                    If contractBO.InstallmentPaymentId.Equals(Guid.Empty) Then Return False
                    If LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, contractBO.InstallmentPaymentId) = Codes.YESNO_Y Then
                        Return True
                    End If
                End If

                Return False
            End Get
        End Property

#End Region

#Region "Parameters"
        Public Class Parameters
            Public CertId As Guid
            Public Sub New(ByVal certid As Guid)
                Me.CertId = certid
            End Sub
        End Class
#End Region

#Region "Page Events"

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Try
                Me.MasterPage.MessageController.Clear()

                If Not Me.IsPostBack Then
                    Me.SetFormTitle(PAGETITLE)
                    Me.SetFormTab(PAGETAB)

                    moCertificateInfoController = Me.UserCertificateCtr
                    moCertificateInfoController.InitController(Me.State.oCert.Id, , GetCompanyCode)

                    Me.State.IsInstallmentPayment = Me.GetInstallmentPaymentFlag

                    'BillingPay Grid
                    cboPageSize.SelectedValue = CType(Me.State.BillPayGridPageSize, String)
                    GridObject.PageSize = Me.State.BillPayGridPageSize
                    MyBase.SetGridItemStyleColor(Me.GridObject)

                    If Not String.IsNullOrEmpty(Me.State.oCert.Dealer.AcceptPaymentByCheck) AndAlso
                    Me.State.oCert.Dealer.AcceptPaymentByCheck = "YESNO-Y" Then
                        ControlMgr.SetVisibleControl(Me, btnAddCheckPayment, True)
                    Else
                        ControlMgr.SetVisibleControl(Me, btnAddCheckPayment, False)
                    End If
                    ControlMgr.SetVisibleControl(Me, btnAddRejectPayment, False)
                    Me.PopulateBillPayGrid()
                    Me.SetButtonState()

                    'Collection Grid
                    Me.PopulateCollectPayGrid()
                Else
                    GetDisabledTabs()
                End If
                CheckIfComingFromPayConfirm()

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
            Me.ShowMissingTranslations(Me.MasterPage.MessageController)
        End Sub

        Private Sub GetDisabledTabs()
            Dim DisabledTabs As Array = hdnDisabledTab.Value.Split(",")
            If DisabledTabs.Length > 0 AndAlso DisabledTabs(0) IsNot String.Empty Then
                DisabledTabsList.AddRange(DisabledTabs)
                hdnDisabledTab.Value = String.Empty
            End If
        End Sub

        Protected Shadows ReadOnly Property State() As MyState
            Get
                If Me.NavController.State Is Nothing Then
                    Me.NavController.State = New MyState

                    Me.State.IsBillPayGridVisible = True
                    Me.State.IsCollectionGridVisible = True

                    Me.State.searchBillPayDV = BillingPayDetail.getLoadBillHistList(CType(Me.NavController.ParametersPassed, Parameters).CertId, Me.State.BillPaySortExp)
                    'If Me.State.searchBillPayDV.Count > 0 Then
                    'Me.State.IsBillPayGridVisible = True
                    'End If

                    Me.State.oCert = New Certificate(CType(Me.NavController.ParametersPassed, Parameters).CertId)
                    'Me.State.oCertInstallment = New CertInstallment(CType(Me.NavController.ParametersPassed, Parameters).CertId, True)

                    Me.State.oDv = Me.State.MyBO_P.getBillPayTotals(Me.State.oCert.Id)

                    Me.State.oBillPayCount = CType(Me.State.oDv.Table.Rows(0).Item(BillingPayDetail.BillPayTotals.COL_DETAIL_COUNT), Integer)
                    If Me.State.oBillPayCount > 0 Then
                        Me.State.oBillPayTotalAmount = CType(Me.State.oDv.Table.Rows(0).Item(BillingPayDetail.BillPayTotals.COL_BILLPAY_AMOUNT_TOTAL), Decimal)
                        'For Each xrow As DataRow In Me.State.searchBillPayDV.Table.Rows
                        '    Me.State.oBillPayTotalAmount = Me.State.oBillPayTotalAmount + CDec(xrow.Item(BillingPayDetail.BillPayTotals.COL_BILLPAY_AMOUNT_TOTAL))
                        'Next
                    End If

                End If
                Return CType(Me.NavController.State, MyState)
            End Get
        End Property

        Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
            Try
                If Not Me.CallingParameters Is Nothing Then
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController, False)
            End Try
        End Sub

#End Region

#Region "Controlling Logic"

        Protected Sub CheckIfComingFromPayConfirm()
            Dim confResponse As String = Me.HiddenPayResp.Value
            Me.HiddenPayResp.Value = ""
            If Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_YES Then
                Dim oBillPayDetail As BillingPayDetail
                oBillPayDetail = New BillingPayDetail(Me.State.SelectBillPayHistId)

                Dim selectedBilllingStatusId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_BILLING_STATUS, BILLING_STATUS_ACTIVE)
                Dim selectedRejectCodeId As Guid = Guid.Empty
                Dim InstNum As Integer = CType(oBillPayDetail.InstallmentNumber, Integer)
                Dim retVal As Integer

                retVal = BillingPayDetail.CreateBillPayForRejOrAct(selectedBilllingStatusId, Me.State.oCert.Id, InstNum, selectedRejectCodeId, Me.State.SelectBillPayHistId)
                If retVal = 0 Then
                    Me.DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                    Me.GridObject.SelectedIndex = Me.NO_ITEM_SELECTED_INDEX
                    Me.State.searchBillPayDV = Nothing
                    ReturnFromEditing()
                End If
            ElseIf Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_NO Then
                'Do Nothing
            End If
        End Sub


        Private Sub PopulateBillPayGrid(Optional ByVal oAction As String = ACTION_NONE)

            If Me.State.BillPaySortExp Is Nothing Then
                Me.State.BillPaySortExp = Me.Grid.Columns(2).SortExpression & " DESC"
            End If

            Me.State.searchBillPayDV = BillingPayDetail.getLoadBillHistList(Me.State.oCert.Id, Me.State.BillPaySortExp)
            Me.State.oDv = Me.State.MyBO_P.getBillPayTotals(Me.State.oCert.Id)

            If Not State.searchBillPayDV Is Nothing Then
                Me.SetPageAndSelectedIndexFromGuid(Me.State.searchBillPayDV, Me.State.SelectBillPayHistId, Me.Grid, Me.State.BillPayGridPageIndex)
                If (Me.State.IsEditMode) Then
                    Me.SetPageAndSelectedIndexFromGuid(Me.State.searchBillPayDV, Me.State.SelectBillPayHistId, Me.Grid, Me.State.BillPayGridPageIndex, Me.State.IsEditMode)
                Else
                    Me.SetPageAndSelectedIndexFromGuid(Me.State.searchBillPayDV, Guid.Empty, Me.Grid, Me.State.BillPayGridPageIndex)
                End If
            End If

            Me.State.oBillPayCount = CType(Me.State.oDv.Table.Rows(0).Item(BillingPayDetail.BillPayTotals.COL_DETAIL_COUNT), Integer)
            If Me.State.oBillPayCount > 0 Then
                Me.State.oBillPayTotalAmount = CType(Me.State.oDv.Table.Rows(0).Item(BillingPayDetail.BillPayTotals.COL_BILLPAY_AMOUNT_TOTAL), Decimal)
                'Me.State.IsBillPayGridVisible = True
                'Else
                'Me.State.IsBillPayGridVisible = False
            End If

            'Me.State.searchBillPayDV.Sort = COL_ORDER_BY
            'SetSelectedIndexFromGuid()
            'ControlMgr.SetEnableTabStrip(Me, tsHoriz.Items(1), Me.State.IsBillPayGridVisible)
            Me.GridObject.AutoGenerateColumns = False
            Me.SortAndBindBillPayGrid()

        End Sub

        Private Sub PopulateCollectPayGrid(Optional ByVal oAction As String = ACTION_NONE)

            If Me.State.CollectedSortExp Is Nothing Then
                Me.State.CollectedSortExp = Me.CollectionGrid.Columns(1).SortExpression & " DESC"
            End If

            Me.State.searchCollectedDV = CollectedPayDetail.getCollectPayHistList(Me.State.oCert.Id, Me.State.CollectedSortExp)
            Me.State.oDv = Me.State.MyBO_C.getCollectPayTotals(Me.State.oCert.Id)

            If Me.State.searchCollectedDV.Count > 0 Then
                Me.State.oCollectedCount = Me.State.searchCollectedDV.Count
                Me.State.oCollectedTotalAmount = CType(Me.State.oDv.Table.Rows(0).Item(CollectedPayDetail.CollectPayTotals.COL_DETAIL_PAYMENT_AMOUNT_TOTAL), Decimal)

                'Me.State.IsCollectionGridVisible = True

                cboPageSize2.SelectedValue = CType(Me.State.CollectedGridPageSize, String)
                CollectionGrid.PageSize = Me.State.CollectedGridPageSize
                MyBase.SetGridItemStyleColor(Me.CollectionGrid)

                Me.CollectionGrid.AutoGenerateColumns = False

                Me.SortAndBindCollectionGrid()
                'Else
                'Me.State.IsCollectionGridVisible = False
            End If

            'If Not Me.State.IsCollectionGridVisible Then
            '    DisabledTabsList.Add(Tab_PaymentCollected)
            'End If

            Me.State.searchCollectedDV.Sort = COL_PAY_ORDER_BY
            Me.SortAndBindCollectionGrid()

        End Sub

        Private Sub SetSelectedIndexFromGuid()
            Dim nSelectedRow As Integer

            nSelectedRow = Me.FindSelectedRowIndexFromGuid(Me.State.searchBillPayDV, Me.State.SelectBillPayHistId)
            GridObject.SelectedIndex = NO_ITEM_SELECTED_INDEX
            GridObject.EditItemIndex = NO_ITEM_SELECTED_INDEX
            If (nSelectedRow > NO_ITEM_SELECTED_INDEX) Then
                GridObject.SelectedIndex = nSelectedRow
                If (Me.State.IsEditMode) Then
                    GridObject.EditItemIndex = GridObject.SelectedIndex
                End If
            End If
            If (Me.State.IsEditMode) Then
                GridObject.AllowSorting = False
            Else
                GridObject.AllowSorting = True
            End If
        End Sub

        Private Sub SortAndBindBillPayGrid()
            Me.State.BillPayGridPageIndex = Me.GridObject.CurrentPageIndex

            'Me.Grid.AutoGenerateColumns = False
            Me.GridObject.DataSource = Me.State.searchBillPayDV

            HighLightSortColumn(Me.Grid, Me.State.BillPaySortExp, Me.IsNewUI)

            Me.GridObject.DataBind()

            'ControlMgr.SetVisibleControl(Me, Me.GridObject, Me.State.IsBillPayGridVisible)
            Me.PopulateControlFromBOProperty(Me.moBillingTotalText, Me.State.oBillPayTotalAmount)
            Session("recCount") = Me.State.searchBillPayDV.Count

            If Me.GridObject.Visible Then
                Me.lblRecordCount.Text = Me.State.searchBillPayDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If
        End Sub

        Private Sub SortAndBindCollectionGrid()
            Me.State.CollectedGridPageIndex = Me.CollectionGrid.CurrentPageIndex
            Me.CollectionGrid.DataSource = Me.State.searchCollectedDV

            HighLightSortColumn(Me.CollectionGrid, Me.State.CollectedSortExp, Me.IsNewUI)

            Me.CollectionGrid.DataBind()

            'ControlMgr.SetVisibleControl(Me, Me.CollectionGrid, Me.State.IsCollectionGridVisible)
            Me.PopulateControlFromBOProperty(Me.moCollectedTotalText, Me.State.oCollectedTotalAmount)
            Session("recCount2") = Me.State.searchCollectedDV.Count

            If Me.CollectionGrid.Visible Then
                Me.lblRecordCount2.Text = Me.State.searchCollectedDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If
        End Sub

        Private Sub SetButtonState()
            If (Me.State.IsEditMode) Then
                ControlMgr.SetVisibleControl(Me, btnSave_WRITE, True)
                ControlMgr.SetVisibleControl(Me, btnUndo_WRITE, True)
                Me.MenuEnabled = False
            Else
                ControlMgr.SetVisibleControl(Me, btnSave_WRITE, False)
                ControlMgr.SetVisibleControl(Me, btnUndo_WRITE, False)
                Me.MenuEnabled = True
            End If
        End Sub

        Private Sub SetFocusOnEditFieldGrid(ByVal grid As DataGrid, ByVal cellPosition As Integer, ByVal itemIndex As Integer)
            Dim ddl As DropDownList = CType(grid.Items(itemIndex).Cells(cellPosition).FindControl("BillingStatusDD"), DropDownList)
            SetFocus(ddl)
        End Sub

#End Region

#Region "DataGrid Related"

#Region "BillPay Datagrid "

        Public Sub EnblDisblRjctCodeDD(ByVal sender As Object, ByVal e As System.EventArgs)
            Dim oBSCode As String
            Dim ddl As DropDownList
            oBSCode = CType(sender, DropDownList).SelectedItem.Value
            ddl = CType(Me.GridObject.Items(Me.GridObject.EditItemIndex).Cells(Me.GRID_COL_REJECT_CODE).FindControl("RejectCodeDD"), DropDownList)
            If oBSCode = BILLING_STATUS_REJECT Then
                ControlMgr.SetEnableControl(Me, ddl, True)
            Else
                Me.SetSelectedItem(ddl, "")
                ControlMgr.SetEnableControl(Me, ddl, False)
            End If
        End Sub

        Public Sub Grid_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Grid.ItemCreated
            Dim elemType As ListItemType = e.Item.ItemType

            If elemType = ListItemType.EditItem Then
                AddHandler CType(e.Item.FindControl("BillingStatusDD"), DropDownList).SelectedIndexChanged, AddressOf EnblDisblRjctCodeDD
            End If
            If elemType = ListItemType.AlternatingItem Or elemType = ListItemType.Item Then
                If elemType = ListItemType.AlternatingItem Or elemType = ListItemType.Item Then
                    Dim objButton As Button
                    objButton = CType(e.Item.Cells(Me.GRID_COL_PAID).FindControl("btnPaid"), Button)
                    objButton.Style.Add("background-color", "#eee3e7")
                    objButton.Style.Add("cursor", "hand")
                    objButton.CssClass = "FLATBUTTON"
                End If
            End If
        End Sub

        Private Sub Grid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Grid.PageIndexChanged
            Try
                Me.GridObject.CurrentPageIndex = e.NewPageIndex
                Me.SortAndBindBillPayGrid()
                'Me.PopulateBillPayGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                GridObject.CurrentPageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                Me.State.BillPayGridPageSize = GridObject.PageSize
                Me.SortAndBindBillPayGrid()
                'Me.PopulateBillPayGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
        Private Sub Grid_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Grid.ItemDataBound

            Try
                Dim oBSCode As String
                Dim blnIncludeFirstPayment As Boolean
                Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)
                Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
                'Dim oMaxActiveInstNoForCert As String = BillingPayDetail.GetMaxActiveInstNoForCert(CType(Me.CallingParameters, Guid)).ToString
                Dim oGetAllRejInstNoForCert As DataView = BillingPayDetail.GetAllRejInstNoForCert(Me.State.oCert.Id)
                'Dim oGetLatestRejInstNoForCert As String = BillingPayDetail.GetLatestRejInstNoForCert(CType(Me.CallingParameters, Guid)).ToString

                Dim oContractId As Guid = Contract.GetContractID(Me.State.oCert.Id)
                Dim oContract As New Contract(oContractId)
                Dim dsplyBtn As Boolean = True
                If Not e.Item.DataItem Is Nothing Then
                    Dim dataChk As DataRow()

                    Dim allRejectTable As DataTable = New DataTable()
                    allRejectTable = oGetAllRejInstNoForCert.ToTable()
                    If Not allRejectTable Is Nothing And allRejectTable.Rows.Count > 0 Then
                        If (Not dvRow(BillingPayDetail.BillPayHistorySearchDV.COL_NAME_INSTALLMENT_NUMBER).ToString Is Nothing _
                       AndAlso Not String.IsNullOrEmpty(dvRow(BillingPayDetail.BillPayHistorySearchDV.COL_NAME_INSTALLMENT_NUMBER).ToString)) Then
                            dataChk = allRejectTable.Select("installment_number=" & dvRow(BillingPayDetail.BillPayHistorySearchDV.COL_NAME_INSTALLMENT_NUMBER).ToString())
                            If (Not dataChk Is Nothing And dataChk.Length > 0) Then  ' Data available 
                                dsplyBtn = True
                            Else
                                dsplyBtn = False
                            End If
                        End If
                        dsplyBtn = True
                    Else
                        dsplyBtn = False
                    End If
                End If

                If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                    'e.Item.Cells(Me.GRID_COL_BILLING_DETAIL_ID).Text = New Guid(CType(dvRow(BillingPayDetail.BillPayHistorySearchDV.COL_NAME_BILLING_DETAIL_ID), Byte())).ToString
                    'e.Item.Cells(Me.GRID_COL_INSTALLMENT_NUMBER).Text = dvRow(BillingPayDetail.BillPayHistorySearchDV.COL_NAME_INSTALLMENT_NUMBER).ToString

                    'If (Not dvRow(BillingPayDetail.BillPayHistorySearchDV.COL_NAME_BILLING_DETAIL_ID).ToString Is Nothing _
                    '   AndAlso Not String.IsNullOrEmpty(dvRow(BillingPayDetail.BillPayHistorySearchDV.COL_NAME_BILLING_DETAIL_ID).ToString)) Then
                    '    e.Item.Cells(Me.GRID_COL_BILLING_DETAIL_ID).Text = New Guid(CType(dvRow(BillingPayDetail.BillPayHistorySearchDV.COL_NAME_BILLING_DETAIL_ID), Byte())).ToString
                    'End If
                    If (Not dvRow(BillingPayDetail.BillPayHistorySearchDV.COL_NAME_INSTALLMENT_NUMBER).ToString Is Nothing _
                       AndAlso Not String.IsNullOrEmpty(dvRow(BillingPayDetail.BillPayHistorySearchDV.COL_NAME_INSTALLMENT_NUMBER).ToString)) Then
                        e.Item.Cells(Me.GRID_COL_INSTALLMENT_NUMBER).Text = dvRow(BillingPayDetail.BillPayHistorySearchDV.COL_NAME_INSTALLMENT_NUMBER).ToString
                    End If
                    e.Item.Cells(Me.GRID_COL_DATE_PROCESSED).Text = dvRow(BillingPayDetail.BillPayHistorySearchDV.COL_NAME_DATE_PROCESSED).ToString
                    e.Item.Cells(Me.GRID_COL_BILLING_STATUS).Text = dvRow(BillingPayDetail.BillPayHistorySearchDV.COL_NAME_BILLING_STATUS).ToString
                    e.Item.Cells(Me.GRID_COL_REJECT_CODE).Text = dvRow(BillingPayDetail.BillPayHistorySearchDV.COL_NAME_REJECT_CODE).ToString
                    e.Item.Cells(Me.GRID_COL_COVERAGE_SEQUENCE).Text = dvRow(BillingPayDetail.BillPayHistorySearchDV.COL_NAME_COVERAGE_SEQ).ToString
                    e.Item.Cells(Me.GRID_COL_BILLING_DATE).Text = dvRow(BillingPayDetail.BillPayHistorySearchDV.COL_NAME_BILLING_DATE).ToString
                    e.Item.Cells(Me.GRID_COL_FROM_DATE).Text = dvRow(BillingPayDetail.BillPayHistorySearchDV.COL_NAME_FROM_DATE).ToString
                    e.Item.Cells(Me.GRID_COL_TO_DATE).Text = dvRow(BillingPayDetail.BillPayHistorySearchDV.COL_NAME_TO_DATE).ToString
                    e.Item.Cells(Me.GRID_COL_SOURCE).Text = dvRow(BillingPayDetail.BillPayHistorySearchDV.COL_NAME_SOURCE).ToString
                    e.Item.Cells(Me.GRID_COL_INVOICE_NUMBER).Text = dvRow(BillingPayDetail.BillPayHistorySearchDV.COL_NAME_INVOICE_NUMBER).ToString

                    If (Not dvRow(BillingPayDetail.BillPayHistorySearchDV.COL_NAME_OPEN_AMOUNT).ToString Is Nothing _
                       AndAlso Not String.IsNullOrEmpty(dvRow(BillingPayDetail.BillPayHistorySearchDV.COL_NAME_OPEN_AMOUNT).ToString)) Then

                        Dim OpenAmount As Decimal = Decimal.Parse(dvRow(BillingPayDetail.BillPayHistorySearchDV.COL_NAME_OPEN_AMOUNT).ToString)
                        e.Item.Cells(Me.GRID_COL_OPEN_AMOUNT).Text = Decimal.Round(OpenAmount, 2).ToString("0.00")
                    End If

                    If (Not dvRow(BillingPayDetail.BillPayHistorySearchDV.COL_NAME_INCOMING_AMOUNT).ToString Is Nothing _
                                       AndAlso Not String.IsNullOrEmpty(dvRow(BillingPayDetail.BillPayHistorySearchDV.COL_NAME_INCOMING_AMOUNT).ToString)) Then

                        Dim IncomingAmount As Decimal = Decimal.Parse(dvRow(BillingPayDetail.BillPayHistorySearchDV.COL_NAME_INCOMING_AMOUNT).ToString)
                        e.Item.Cells(Me.GRID_COL_INCOMING_AMOUNT).Text = Decimal.Round(IncomingAmount, 2).ToString("0.00")
                    End If

                    oBSCode = LookupListNew.GetCodeFromDescription(LookupListNew.GetBillingStatusList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), dvRow(BillingPayDetail.BillPayHistorySearchDV.COL_NAME_BILLING_STATUS).ToString)

                    Me.PopulateControlFromBOProperty(e.Item.Cells(Me.GRID_COL_BILLED_AMOUNT), dvRow(BillingPayDetail.BillPayHistorySearchDV.COL_NAME_BILLED_AMOUNT))

                    If LookupListNew.GetCodeFromId(LookupListNew.LK_ALLOW_CC_REJECTIONS, oContract.AllowMultipleRejectionsId) = Codes.ALLOW_CC_REJECTIONS_NO Then
                        If (Not dvRow(BillingPayDetail.BillPayHistorySearchDV.COL_NAME_INSTALLMENT_NUMBER).ToString Is Nothing _
                       AndAlso Not String.IsNullOrEmpty(dvRow(BillingPayDetail.BillPayHistorySearchDV.COL_NAME_INSTALLMENT_NUMBER).ToString)) Then

                            If oBSCode = BILLING_STATUS_REJECT And dvRow(BillingPayDetail.BillPayHistorySearchDV.COL_NAME_PAID).ToString = "" And
                                dsplyBtn And Integer.Parse(dvRow(BillingDetail.BillingHistorySearchDV.COL_NAME_INSTALLMENT_NUMBER).ToString) < 0 Then
                                Dim btnpd As Button = CType(e.Item.Cells(Me.GRID_COL_PAID).FindControl("btnPaid"), Button)
                                ControlMgr.SetVisibleControl(Me, btnpd, True)
                            Else
                                e.Item.Cells(Me.GRID_COL_PAID).Text = dvRow(BillingPayDetail.BillPayHistorySearchDV.COL_NAME_PAID).ToString
                            End If
                        End If

                    Else
                        Dim btnpd As Button = CType(e.Item.Cells(Me.GRID_COL_PAID).FindControl("btnPaid"), Button)
                        Dim lblpd As Label = CType(e.Item.Cells(Me.GRID_COL_PAID).FindControl("lblPaid"), Label)

                        lblpd.Text = dvRow(BillingPayDetail.BillPayHistorySearchDV.COL_NAME_PAID).ToString
                        ControlMgr.SetVisibleControl(Me, lblpd, True)
                        ControlMgr.SetVisibleControl(Me, btnpd, False)
                        Dim i As Integer
                        For i = 0 To oGetAllRejInstNoForCert.Count - 1

                            If oGetAllRejInstNoForCert(i)(0).ToString = dvRow(BillingPayDetail.BillPayHistorySearchDV.COL_NAME_INSTALLMENT_NUMBER).ToString AndAlso
                                                                                oGetAllRejInstNoForCert(i)(1).ToString = dvRow(BillingPayDetail.BillPayHistorySearchDV.COL_NAME_CREATED_DATE1).ToString And
                                                                                dsplyBtn Then
                                ControlMgr.SetVisibleControl(Me, btnpd, True)
                                ControlMgr.SetVisibleControl(Me, lblpd, False)
                            End If
                        Next
                    End If

                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs)
            Try
                Dim index As Integer = e.Item.ItemIndex

                If (e.CommandName = Me.EDIT_COMMAND) Then

                    Me.State.IsEditMode = True
                    'Me.State.SelectBillPayHistId = New Guid(Me.GridObject.Items(e.Item.ItemIndex).Cells(Me.GRID_COL_BILLING_DETAIL_ID).Text)
                    Me.PopulateBillPayGrid(Me.ACTION_EDIT)

                    'Me.State.BillPayGridPageIndex = Grid.CurrentPageIndex

                    Me.SetFocusOnEditFieldGrid(Me.GridObject, Me.GRID_COL_BILLING_STATUS, index)
                    Me.SetButtonState()
                ElseIf (e.CommandName = Me.PAID_COMMAND) Then
                    Me.State.SelectBillPayHistId = New Guid(Me.GridObject.Items(e.Item.ItemIndex).Cells(Me.GRID_COL_BILLING_DETAIL_ID).Text)
                    Me.DisplayMessage(Message.MSG_PROMPT_FOR_PAY, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenPayResp)
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
        Private Sub Grid_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles Grid.SortCommand
            Dim SortExp As String = Me.State.BillPaySortExp
            Try
                If Me.State.BillPaySortExp.StartsWith(e.SortExpression) Then
                    If SortExp.EndsWith(" DESC") Then
                        SortExp = e.SortExpression
                    Else
                        SortExp &= " DESC"
                    End If
                Else
                    SortExp = e.SortExpression
                End If

                Me.State.BillPaySortExp = SortExp
                Me.Grid.CurrentPageIndex = 0

                Me.PopulateBillPayGrid()

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
#End Region

#Region "Pay Collection Datagrid "


        Protected Sub CollectionGridItemCreated(ByVal sender As Object, ByVal e As DataGridItemEventArgs) Handles Grid.ItemCreated, CollectionGrid.ItemCreated
            BaseItemCreated(sender, e)
        End Sub

        Private Sub CollectionGrid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles CollectionGrid.PageIndexChanged
            Try
                Me.CollectionGrid.CurrentPageIndex = e.NewPageIndex
                SortAndBindCollectionGrid()
                'Me.PopulateCollectionGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub CollectionGrid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize2.SelectedIndexChanged
            Try
                CollectionGrid.CurrentPageIndex = NewCurrentPageIndex(CollectionGrid, CType(Session("recCount2"), Int32), CType(cboPageSize2.SelectedValue, Int32))
                Me.State.CollectedGridPageSize = CollectionGrid.PageSize
                SortAndBindCollectionGrid()
                'Me.PopulateCollectionGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub CollectionGrid_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles CollectionGrid.ItemDataBound
            Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)

            Try
                Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)

                If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem Then
                    Me.PopulateControlFromBOProperty(e.Item.Cells(Me.GRID_COL_COLLECTED_AMOUNT), dvRow(CollectedPayDetail.CollectPayHistorySearchDV.COL_NAME_COLLECTED_AMOUNT))
                    e.Item.Cells(Me.GRID_COL_PAYMENT_ID).Text = New Guid(CType(dvRow(CollectedPayDetail.CollectPayHistorySearchDV.COL_NAME_PAYMENT_ID), Byte())).ToString
                    e.Item.Cells(Me.GRID_COL_COLLECTED_DATE).Text = dvRow(CollectedPayDetail.CollectPayHistorySearchDV.COL_NAME_COLLECTED_DATE).ToString
                    e.Item.Cells(Me.GRID_COL_COVERAGE_SEQUENCE_PC).Text = dvRow(CollectedPayDetail.CollectPayHistorySearchDV.COL_NAME_COVERAGE_SEQ_PC).ToString
                    e.Item.Cells(Me.GRID_COL_DATE_SEND).Text = dvRow(CollectedPayDetail.CollectPayHistorySearchDV.COL_NAME_DATE_SEND).ToString
                    e.Item.Cells(Me.GRID_COL_DATE_PROCESSED_PC).Text = dvRow(CollectedPayDetail.CollectPayHistorySearchDV.COL_NAME_DATE_PROCESSED_PC).ToString
                    e.Item.Cells(Me.GRID_COL_INSTALLMENT_NUMB_PC).Text = dvRow(CollectedPayDetail.CollectPayHistorySearchDV.COL_NAME_INSTALLMENT_NUMB_PC).ToString
                    e.Item.Cells(Me.GRID_COL_PAYMENT_STATUS).Text = dvRow(CollectedPayDetail.CollectPayHistorySearchDV.COL_NAME_PAYMENT_STATUS).ToString
                    e.Item.Cells(Me.GRID_COL_REJECT_CODE_PC).Text = dvRow(CollectedPayDetail.CollectPayHistorySearchDV.COL_NAME_REJECT_CODE_PC).ToString
                    e.Item.Cells(Me.GRID_COL_REJECT_DATE_PC).Text = dvRow(CollectedPayDetail.CollectPayHistorySearchDV.COL_NAME_REJECT_DATE_PC).ToString
                    e.Item.Cells(Me.GRID_COL_REJECT_REASON_PC).Text = dvRow(CollectedPayDetail.CollectPayHistorySearchDV.COL_NAME_REJECT_REASON_PC).ToString
                    'e.Item.Cells(Me.GRID_COL_CREDIT_CARD_TYPE).Text = dvRow(CollectedPayDetail.CollectPayHistorySearchDV.COL_NAME_CREDIT_CARD_TYPE).ToString
                    'e.Item.Cells(Me.GRID_COL_CREDIT_CARD_NUMBER).Text = dvRow(CollectedPayDetail.CollectPayHistorySearchDV.COL_NAME_CREDIT_CARD_NUMBER).ToString
                    e.Item.Cells(Me.GRID_COL_PAYMENT_METHOD).Text = dvRow(CollectedPayDetail.CollectPayHistorySearchDV.COL_NAME_PAYMENT_METHOD).ToString
                    e.Item.Cells(Me.GRID_COL_PAYMENT_INSTRUMENT_NUMBER).Text = dvRow(CollectedPayDetail.CollectPayHistorySearchDV.COL_NAME_PAYMENT_INSTRUMENT_NUMBER).ToString
                    CType(e.Item.Cells(Me.GRID_COL_PAYMENT_ID).FindControl("Payment_Type_XCD"), HiddenField).Value =
                    dvRow(CollectedPayDetail.CollectPayHistorySearchDV.COL_NAME_PAYMENT_TYPE_XCD).ToString
                    Dim img As ImageButton = CType(e.Item.Cells(0).FindControl("btnSelect"), ImageButton)
                    'e.Item.Cells(Me.GRID_COL_SOURCE).Text = dvRow(CollectedPayDetail.CollectPayHistorySearchDV.COL_NAME_SOURCE).ToString

                    If (dvRow(CollectedPayDetail.CollectPayHistorySearchDV.COL_NAME_REJECT_CODE_PC).ToString = "") Then
                        img.Visible = True
                    Else
                        img.Visible = False
                    End If

                    If dvRow(CollectedPayDetail.CollectPayHistorySearchDV.COL_NAME_COLLECTED_AMOUNT) < 0 Then
                        e.Item.BackColor = Color.FromName("#FFFF99")
                    End If

                    If (Not dvRow(CollectedPayDetail.CollectPayHistorySearchDV.COL_NAME_PAYMENT_AMOUNT).ToString Is Nothing _
                                       AndAlso Not String.IsNullOrEmpty(dvRow(CollectedPayDetail.CollectPayHistorySearchDV.COL_NAME_PAYMENT_AMOUNT).ToString)) Then

                        Dim IncomingAmount As Decimal = Decimal.Parse(dvRow(CollectedPayDetail.CollectPayHistorySearchDV.COL_NAME_PAYMENT_AMOUNT).ToString)
                        e.Item.Cells(Me.GRID_COL_PAYMENT_AMOUNT).Text = Decimal.Round(IncomingAmount, 2).ToString("0.00")
                    End If
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub CollectionGrid_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles CollectionGrid.SortCommand
            Dim SortExp As String = Me.State.CollectedSortExp
            Try
                If Me.State.CollectedSortExp.StartsWith(e.SortExpression) Then
                    If SortExp.EndsWith(" DESC") Then
                        SortExp = e.SortExpression
                    Else
                        SortExp &= " DESC"
                    End If
                Else
                    SortExp = e.SortExpression
                End If

                Me.State.CollectedSortExp = SortExp
                Me.CollectionGrid.CurrentPageIndex = 0

                Me.PopulateCollectPayGrid()

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
#End Region

#End Region

#Region "Button Handlers"
        Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBack.Click
            Try
                Me.NavController.Navigate(Me, FlowEvents.EVENT_BACK)
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnSave_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave_WRITE.Click
            Try
                Dim billingStsGrdClm As Integer
                Dim rjctCodeGrdClm As Integer

                Dim ddl As DropDownList = CType(Me.GridObject.Items(Me.GridObject.EditItemIndex).Cells(billingStsGrdClm).FindControl("BillingStatusDD"), DropDownList)
                Dim slctBillingCode As String = Me.GetSelectedValue(ddl)

                Dim slctRjctCode As String
                ddl = CType(Me.GridObject.Items(Me.GridObject.EditItemIndex).Cells(rjctCodeGrdClm).FindControl("RejectCodeDD"), DropDownList)
                slctRjctCode = Me.GetSelectedValue(ddl)

                If slctBillingCode <> BILLING_STATUS_REJECT Then
                    Me.ErrControllerMaster.AddErrorAndShow(ElitaPlus.Common.ErrorCodes.GUI_SELECT_REJECT_CODE_ERR)
                    Exit Sub
                Else
                    If slctRjctCode = "" Then
                        Me.ErrControllerMaster.AddErrorAndShow(ElitaPlus.Common.ErrorCodes.GUI_SELECT_REJECT_CODE_ERR)
                        Exit Sub
                    End If
                End If

                Dim slctBillingStsId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_BILLING_STATUS, slctBillingCode)
                Dim SlctRjctCodeId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_REJECT_CODES, slctRjctCode)
                Dim retVal As Integer
                Dim txtInstNo As TextBox = CType(Me.GridObject.Items(Me.GridObject.EditItemIndex).Cells(Me.GRID_COL_INSTALLMENT_NUMBER).FindControl("txtInstallNumb"), TextBox)

                retVal = BillingPayDetail.CreateBillPayForRejOrAct(slctBillingStsId, Me.State.oCert.Id, CType(txtInstNo.Text, Integer), SlctRjctCodeId, Me.State.SelectBillPayHistId)
                If retVal = 0 Then
                    Me.DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                    Me.GridObject.SelectedIndex = Me.NO_ITEM_SELECTED_INDEX
                    Me.State.searchBillPayDV = Nothing
                    ReturnFromEditing()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnUndo_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUndo_WRITE.Click
            Me.GridObject.SelectedIndex = Me.NO_ITEM_SELECTED_INDEX
            Me.State.searchBillPayDV = Nothing
            ReturnFromEditing()
        End Sub

        Private Sub ReturnFromEditing()

            GridObject.EditItemIndex = Me.NO_ITEM_SELECTED_INDEX

            If (Me.GridObject.PageCount = 0) Then
                ControlMgr.SetVisibleControl(Me, GridObject, False)
            Else
                ControlMgr.SetVisibleControl(Me, GridObject, True)
            End If

            Me.State.IsEditMode = False
            Me.PopulateBillPayGrid()
            SetButtonState()
        End Sub

        Private Sub BillingPaymentHistoryListForm_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
            hdnDisabledTab.Value = String.Join(",", DisabledTabsList)
        End Sub

        Private Sub btnAddReject_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddRejectPayment.Click
            Try
                Me.NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE) = Me.State.oCert
                Me.NavController.Navigate(Me, CREATE_NEW_REJECT_PAYMENT, New CheckRejectPaymentForm.Parameters(Me.State.SelectPaymentId, Me.State.oCert.Id, Me.State.PayInstrumentNo, Me.State.PaymentDate))
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnAddCheckPayment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddCheckPayment.Click
            Try
                Me.NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE) = Me.State.oCert
                Me.NavController.Navigate(Me, CREATE_NEW_CHECK_PAYMENT, New CheckPaymentForm.Parameters(Me.State.oCert.Id))
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub CollectionGrid_ItemCommand(source As Object, e As DataGridCommandEventArgs)
            Try
                Dim index As Integer = e.Item.ItemIndex
                If (e.CommandName = Me.REJECT_COMMAND) Then
                    Me.State.SelectPaymentId = New Guid(Me.PayGridObject.Items(e.Item.ItemIndex).Cells(Me.GRID_COL_PAYMENT_ID).Text)
                    Me.State.PayInstrumentNo = Me.PayGridObject.Items(e.Item.ItemIndex).Cells(Me.GRID_COL_PAYMENT_INSTRUMENT_NUMBER).Text
                    Me.State.PaymentDate = Me.PayGridObject.Items(e.Item.ItemIndex).Cells(Me.GRID_COL_COLLECTED_DATE).Text
                    ControlMgr.SetVisibleControl(Me, btnAddCheckPayment, False)
                    ControlMgr.SetVisibleControl(Me, btnAddRejectPayment, True)
                    If Me.State.oBillPayTotalAmount < 0 Or
                        CType(e.Item.Cells(0).FindControl("Payment_Type_XCD"), HiddenField).Value = "RFM-SEPA" Then

                        ControlMgr.SetVisibleControl(Me, btnAddRejectPayment, False)
                    Else
                        ControlMgr.SetVisibleControl(Me, btnAddRejectPayment, True)
                    End If
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
#End Region

    End Class
End Namespace