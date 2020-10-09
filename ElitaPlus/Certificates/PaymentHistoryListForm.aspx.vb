Imports System.Collections.Generic

Namespace Certificates
    Public Class PaymentHistoryListForm
        Inherits ElitaPlusSearchPage

#Region "Constants"
        Protected WithEvents moCertificateInfoController As Global.Assurant.ElitaPlus.ElitaPlusWebApp.Certificates.UserControlCertificateInfo_New

        'Actions
        Private Const ACTION_NONE As String = "ACTION_NONE"

        Public Const URL As String = "~/Certificates/PaymentHistoryListForm.aspx"
        Public Const PAGETITLE As String = "PAYMENT_HISTORY"
        Public Const PAGETAB As String = "CERTIFICATES"

        'Payment Grid
        Public Const GRID_COL_SERIAL_NUMBER As Integer = 0
        Public Const GRID_COL_COVERAGE_SEQUENCE As Integer = 1
        Public Const GRID_COL_PAID_FROM As Integer = 2
        Public Const GRID_COL_PAID_TO As Integer = 3
        Public Const GRID_COL_DATE_OF_PAYMENT As Integer = 4
        Public Const GRID_COL_DATE_PROCESSED As Integer = 5
        Public Const GRID_COL_PAYMENT_AMOUNT As Integer = 6
        Public Const GRID_COL_INCOMING_PAYMENT_AMOUNT As Integer = 7
        Public Const GRID_COL_INSTALLMENT_NUM As Integer = 8
        Public Const GRID_COL_PAYMENT_INFO As Integer = 9
        Public Const GRID_COL_SOURCE As Integer = 10
        Public Const GRID_COL_PAYMENT_REFERENCE_NUMBER As Integer = 11

        'Collection Grid        
        Public Const GRID_COL_COL_BILLING_START_DATE As Integer = 0
        Public Const GRID_COL_COLLECTED_DATE As Integer = 1
        Public Const GRID_COL_CREATED_DATE As Integer = 2
        Public Const GRID_COL_COLLECTED_AMOUNT As Integer = 3
        Public Const GRID_COL_INCOMING_AMOUNT As Integer = 4
        Public Const GRID_COL_COLL_INSTALLMENT_NUM As Integer = 5

#End Region
#Region "Tabs"
        Public Const Tab_BillingHistory As String = "0"
        Public Const Tab_PaymentCollected As String = "1"

        Dim DisabledTabsList As New List(Of String)()

#End Region
#Region "Page State"
        ' This class keeps the current state for the page.
        Class MyState
            Public MyBO_P As PaymentDetail
            Public MyBO_C As CollectedDetail

            Public oCert As Certificate
            Public oPaymentTotalAmount As Decimal = 0
            Public oPaymentCount As Int32

            Public oCollectedTotalAmount As Decimal = 0
            Public oCollectedCount As Int32

            Public oDv As DataView = Nothing
            Public searchPaymentDV As DataView = Nothing
            Public searchCollectedDV As DataView = Nothing

            Public IsPaymentGridVisible As Boolean
            Public IsCollectionGridVisible As Boolean

            Public PaymentGridPageIndex As Integer = 0
            Public CollectedGridPageIndex As Integer = 0

            Public PaymentGridPageSize As Integer = 10
            Public CollectedGridPageSize As Integer = 10

            Public PaymentSortExp As String = Nothing
            Public CollectedSortExp As String = Nothing
            Public IsInstallmentPayment As Boolean

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


#Region "Properties"

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
                Dim companyBO As Company = New Company(State.oCert.CompanyId)

                Return companyBO.Code
            End Get

        End Property

        Public ReadOnly Property GetInstallmentPaymentFlag() As Boolean
            Get
                Dim contractBO As Contract
                contractBO = Contract.GetContract(State.oCert.DealerId, CDate(State.oCert.WarrantySalesDate))

                If contractBO IsNot Nothing Then
                    If contractBO.InstallmentPaymentId.Equals(Guid.Empty) Then Return False
                    If LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, contractBO.InstallmentPaymentId) = Codes.YESNO_Y Then
                        Return True
                    End If
                End If

                Return False
            End Get
        End Property

#End Region

#Region "Page Events"

        Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
            Try
                MasterPage.MessageController.Clear()

                If Not IsPostBack Then
                    SetFormTitle(PAGETITLE)
                    SetFormTab(PAGETAB)

                    moCertificateInfoController = UserCertificateCtr
                    moCertificateInfoController.InitController(State.oCert.Id, , GetCompanyCode)

                    State.IsInstallmentPayment = GetInstallmentPaymentFlag

                    'Payment Grid
                    cboPageSize.SelectedValue = CType(State.PaymentGridPageSize, String)
                    Grid.PageSize = State.PaymentGridPageSize
                    MyBase.SetGridItemStyleColor(Grid)

                    PopulatePaymentGrid()

                    'Collection Grid
                    PopulateCollectionGrid()
                Else
                    GetDisabledTabs()
                End If

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
            ShowMissingTranslations(MasterPage.MessageController)
        End Sub

        Private Sub GetDisabledTabs()
            Dim DisabledTabs As Array = hdnDisabledTab.Value.Split(",")
            If DisabledTabs.Length > 0 AndAlso DisabledTabs(0) IsNot String.Empty Then
                DisabledTabsList.AddRange(DisabledTabs)
                hdnDisabledTab.Value = String.Empty
            End If
        End Sub

        Private Sub Page_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall
            Try
                If CallingParameters IsNot Nothing Then
                    State.oCert = New Certificate(CType(CallingParameters, Guid))
                End If

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController, False)
            End Try
        End Sub

#End Region

#Region "Controlling Logic"

        Private Sub PopulatePaymentGrid(Optional ByVal oAction As String = ACTION_NONE)

            If State.PaymentSortExp Is Nothing Then
                State.PaymentSortExp = Grid.Columns(2).SortExpression & " DESC"
            End If

            State.oPaymentCount = 0
            State.oPaymentTotalAmount = 0

            State.searchPaymentDV = PaymentDetail.getHistoryList(CType(CallingParameters, Guid), State.PaymentSortExp)

            If State.searchPaymentDV.Count > 0 Then
                State.IsPaymentGridVisible = True

                State.oPaymentCount = State.searchPaymentDV.Count
                State.oPaymentTotalAmount = 0
                For Each xrow As DataRow In State.searchPaymentDV.Table.Rows
                    State.oPaymentTotalAmount = State.oPaymentTotalAmount + CDec(xrow.Item(PaymentDetail.PaymentHistorySearchDV.COL_NAME_PAYMENT_AMOUNT))
                Next

                'Me.State.oDv = Me.State.MyBO_P.getPaymentTotals(Me.State.oCert.Id)

                'Me.State.oPaymentCount = CType(Me.State.oDv.Table.Rows(0).Item(PaymentDetail.PaymentTotals.COL_DETAIL_COUNT), Integer)
                'If Me.State.oPaymentCount > 0 Then
                '    Me.State.oPaymentTotalAmount = CType(Me.State.oDv.Table.Rows(0).Item(PaymentDetail.PaymentTotals.COL_DETAIL_PAYMENT_AMOUNT_TOTAL), Decimal)
                'End If
            Else
                State.IsPaymentGridVisible = False
            End If

            SortAndBindPaymentGrid()

        End Sub

        Private Sub PopulateCollectionGrid(Optional ByVal oAction As String = ACTION_NONE)

            If State.CollectedSortExp Is Nothing Then
                State.CollectedSortExp = CollectionGrid.Columns(1).SortExpression & " DESC"
            End If

            State.oCollectedCount = 0
            State.oCollectedTotalAmount = 0

            State.searchCollectedDV = CollectedDetail.getCollectedHistoryList(CType(CallingParameters, Guid), State.CollectedSortExp)

            If State.searchCollectedDV.Count > 0 Then
                State.oCollectedCount = State.searchCollectedDV.Count

                For Each xrow As DataRow In State.searchCollectedDV.Table.Rows
                    State.oCollectedTotalAmount += CDec(xrow.Item(CollectedDetail.CollectedHistorySearchDV.COL_NAME_COLLECTED_AMOUNT))
                Next

                'Me.State.oDv = Me.State.MyBO_C.getCollectedTotals(Me.State.oCert.Id)

                'Me.State.oCollectedCount = CType(Me.State.oDv.Table.Rows(0).Item(CollectedDetail.CollectedTotals.COL_DETAIL_COUNT), Integer)

                'If Me.State.oCollectedCount > 0 Then
                '    Me.State.oCollectedTotalAmount = CType(Me.State.oDv.Table.Rows(0).Item(CollectedDetail.CollectedTotals.COL_DETAIL_COLLECTED_AMOUNT_TOTAL), Decimal)
                'End If

                State.IsCollectionGridVisible = True

                cboPageSize2.SelectedValue = CType(State.CollectedGridPageSize, String)
                CollectionGrid.PageSize = State.CollectedGridPageSize
                MyBase.SetGridItemStyleColor(CollectionGrid)

                CollectionGrid.AutoGenerateColumns = False

                SortAndBindCollectionGrid()
            Else
                State.IsCollectionGridVisible = False
            End If

            If Not State.IsCollectionGridVisible Then
                DisabledTabsList.Add(Tab_PaymentCollected)
            End If

            SortAndBindCollectionGrid()

        End Sub

        Private Sub SortAndBindPaymentGrid()
            State.PaymentGridPageIndex = Grid.CurrentPageIndex

            Grid.AutoGenerateColumns = False
            Grid.DataSource = State.searchPaymentDV

            HighLightSortColumn(Grid, State.PaymentSortExp, IsNewUI)

            Grid.DataBind()

            ControlMgr.SetVisibleControl(Me, Grid, State.IsPaymentGridVisible)
            PopulateControlFromBOProperty(moBillingTotalText, State.oPaymentTotalAmount)
            Session("recCount") = State.searchPaymentDV.Count

            If Grid.Visible Then
                lblRecordCount.Text = State.searchPaymentDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If
        End Sub

        Private Sub SortAndBindCollectionGrid()
            State.CollectedGridPageIndex = CollectionGrid.CurrentPageIndex
            CollectionGrid.DataSource = State.searchCollectedDV

            HighLightSortColumn(CollectionGrid, State.CollectedSortExp, IsNewUI)

            CollectionGrid.DataBind()

            ControlMgr.SetVisibleControl(Me, CollectionGrid, State.IsCollectionGridVisible)
            PopulateControlFromBOProperty(moCollectedTotalText, State.oCollectedTotalAmount)
            Session("recCount2") = State.searchCollectedDV.Count

            If CollectionGrid.Visible Then
                lblRecordCount2.Text = State.searchCollectedDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If
        End Sub

#End Region

#Region "Datagrid Related"

#Region "Payment Datagrid "
        Private Sub Grid_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Grid.PageIndexChanged
            Try
                Grid.CurrentPageIndex = e.NewPageIndex
                SortAndBindPaymentGrid()

                'Me.PopulatePaymentGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_PageSizeChanged(source As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                Grid.CurrentPageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                State.PaymentGridPageSize = Grid.PageSize
                SortAndBindPaymentGrid()
                'Me.PopulatePaymentGrid()

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
        Private Sub Grid_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Grid.ItemDataBound
            Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)

            Try
                Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)

                If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem Then
                    PopulateControlFromBOProperty(e.Item.Cells(GRID_COL_PAYMENT_AMOUNT), dvRow(PaymentDetail.PaymentHistorySearchDV.COL_NAME_PAYMENT_AMOUNT))

                    If (dvRow(PaymentDetail.PaymentHistorySearchDV.COL_NAME_INCOMING_AMOUNT).ToString IsNot Nothing _
                       AndAlso Not String.IsNullOrEmpty(dvRow(PaymentDetail.PaymentHistorySearchDV.COL_NAME_INCOMING_AMOUNT).ToString)) Then

                        Dim IncomingAmount As Decimal = Decimal.Parse(dvRow(PaymentDetail.PaymentHistorySearchDV.COL_NAME_INCOMING_AMOUNT).ToString)
                        e.Item.Cells(GRID_COL_INCOMING_PAYMENT_AMOUNT).Text = Decimal.Round(IncomingAmount, 3).ToString("0.000")
                    End If

                    e.Item.Cells(GRID_COL_DATE_OF_PAYMENT).Text = dvRow(PaymentDetail.PaymentHistorySearchDV.COL_NAME_DATE_OF_PAYMENT).ToString
                    e.Item.Cells(GRID_COL_DATE_PROCESSED).Text = dvRow(PaymentDetail.PaymentHistorySearchDV.COL_NAME_DATE_PROCESSED).ToString

                    'REQ-5373 start
                    e.Item.Cells(GRID_COL_COVERAGE_SEQUENCE).Text = dvRow(PaymentDetail.PaymentHistorySearchDV.COL_NAME_COVERAGE_SEQ).ToString

                    If State.IsInstallmentPayment Then
                        'Req - 1016 Start
                        e.Item.Cells(GRID_COL_PAID_FROM).Text = dvRow(PaymentDetail.PaymentHistorySearchDV.COL_NAME_PAYMENT_DUE_DATE).ToString
                    Else
                        e.Item.Cells(GRID_COL_PAID_FROM).Text = dvRow(PaymentDetail.PaymentHistorySearchDV.COL_NAME_DATE_PAID_FROM).ToString
                    End If
                    'REQ-5373 end

                    e.Item.Cells(GRID_COL_PAID_TO).Text = dvRow(PaymentDetail.PaymentHistorySearchDV.COL_NAME_DATE_PAID_FOR).ToString
                    e.Item.Cells(GRID_COL_SERIAL_NUMBER).Text = dvRow(PaymentDetail.PaymentHistorySearchDV.COL_NAME_SERIAL_NUMBER).ToString
                    e.Item.Cells(GRID_COL_SOURCE).Text = dvRow(PaymentDetail.PaymentHistorySearchDV.COL_NAME_SOURCE).ToString
                    e.Item.Cells(GRID_COL_INSTALLMENT_NUM).Text = dvRow(PaymentDetail.PaymentHistorySearchDV.COL_NAME_INSTALLMENT_NUM).ToString
                    e.Item.Cells(GRID_COL_PAYMENT_INFO).Text = dvRow(PaymentDetail.PaymentHistorySearchDV.COL_NAME_PAYMENT_INFO).ToString
                    e.Item.Cells(GRID_COL_PAYMENT_REFERENCE_NUMBER).Text = dvRow(PaymentDetail.PaymentHistorySearchDV.COL_NAME_PAYMENT_REFERENCE_NUMBER).ToString
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub ItemCreated(sender As Object, e As DataGridItemEventArgs) Handles Grid.ItemCreated, CollectionGrid.ItemCreated
            BaseItemCreated(sender, e)
        End Sub

        Private Sub Grid_SortCommand(source As Object, e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles Grid.SortCommand
            Dim SortExp As String = State.PaymentSortExp
            Try
                If State.PaymentSortExp.StartsWith(e.SortExpression) Then
                    If SortExp.EndsWith(" DESC") Then
                        SortExp = e.SortExpression
                    Else
                        SortExp &= " DESC"
                    End If
                Else
                    SortExp = e.SortExpression
                End If

                State.PaymentSortExp = SortExp
                Grid.CurrentPageIndex = 0

                PopulatePaymentGrid()

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
#End Region

#Region "Collection Datagrid "

        Private Sub CollectionGrid_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles CollectionGrid.PageIndexChanged
            Try
                CollectionGrid.CurrentPageIndex = e.NewPageIndex
                SortAndBindCollectionGrid()
                'Me.PopulateCollectionGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub CollectionGrid_PageSizeChanged(source As Object, e As System.EventArgs) Handles cboPageSize2.SelectedIndexChanged
            Try
                CollectionGrid.CurrentPageIndex = NewCurrentPageIndex(CollectionGrid, CType(Session("recCount2"), Int32), CType(cboPageSize2.SelectedValue, Int32))
                State.CollectedGridPageSize = CollectionGrid.PageSize
                SortAndBindCollectionGrid()
                'Me.PopulateCollectionGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub CollectionGrid_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles CollectionGrid.ItemDataBound
            Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)

            Try
                Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)

                If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem Then
                    PopulateControlFromBOProperty(e.Item.Cells(GRID_COL_COLLECTED_AMOUNT), dvRow(CollectedDetail.CollectedHistorySearchDV.COL_NAME_COLLECTED_AMOUNT))
                    e.Item.Cells(GRID_COL_COLLECTED_DATE).Text = dvRow(CollectedDetail.CollectedHistorySearchDV.COL_NAME_COLLECTED_DATE).ToString
                    e.Item.Cells(GRID_COL_COL_BILLING_START_DATE).Text = dvRow(CollectedDetail.CollectedHistorySearchDV.COL_NAME_BILLING_START_DATE).ToString
                    e.Item.Cells(GRID_COL_CREATED_DATE).Text = dvRow(CollectedDetail.CollectedHistorySearchDV.COL_NAME_CREATED_DATE).ToString
                    e.Item.Cells(GRID_COL_INCOMING_AMOUNT).Text = dvRow(CollectedDetail.CollectedHistorySearchDV.COL_NAME_INCOMING_AMOUNT).ToString
                    e.Item.Cells(GRID_COL_COLL_INSTALLMENT_NUM).Text = dvRow(CollectedDetail.CollectedHistorySearchDV.COL_NAME_INSTALLMENT_NUM).ToString
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub CollectionGrid_SortCommand(source As Object, e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles CollectionGrid.SortCommand
            Dim SortExp As String = State.CollectedSortExp
            Try
                If State.CollectedSortExp.StartsWith(e.SortExpression) Then
                    If SortExp.EndsWith(" DESC") Then
                        SortExp = e.SortExpression
                    Else
                        SortExp &= " DESC"
                    End If
                Else
                    SortExp = e.SortExpression
                End If

                State.CollectedSortExp = SortExp
                CollectionGrid.CurrentPageIndex = 0

                PopulateCollectionGrid()

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
#End Region

#End Region

#Region "Button Handlers"
        Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
            Try
                ReturnToCallingPage()
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub PaymentHistoryListForm_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
            hdnDisabledTab.Value = String.Join(",", DisabledTabsList)
        End Sub
#End Region


    End Class
End Namespace
