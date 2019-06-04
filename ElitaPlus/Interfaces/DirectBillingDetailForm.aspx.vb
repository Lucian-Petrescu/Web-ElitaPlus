Partial Public Class DirectBillingDetailForm
    Inherits ElitaPlusSearchPage

#Region " Web Form Designer Generated Code "

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
    Protected WithEvents ErrController As ErrorController


#Region "Constants"
    Private Const GRID_COL_BILLING_IDX As Integer = 1
    Private Const GRID_NO_SELECTEDITEM_INX As Integer = -1

    Public Const GRID_COL_EDIT_IDX As Integer = 0
    Public Const GRID_COL_CERT_NUMBER_IDX As Integer = 1
    Public Const GRID_COL_INSTALLMENT_NUMBER_IDX As Integer = 2
    Public Const GRID_COL_BANK_ACCT_OWNER_NAME_IDX As Integer = 3
    Public Const GRID_COL_BANK_ACCT_NUMBER_IDX As Integer = 4
    Public Const GRID_COL_BANK_RTN_NUMBER_IDX As Integer = 5
    Public Const GRID_COL_BILLED_AMOUNT_IDX As Integer = 6
    Public Const GRID_COL_REASON_IDX As Integer = 7

    Public Const GRID_VSC_COL_EDIT_IDX As Integer = 0
    Public Const GRID_VSC_COL_ACCOUNT_TYPE_IDX As Integer = 1
    Public Const GRID_VSC_COL_CERT_NUMBER_IDX As Integer = 2
    Public Const GRID_VSC_COL_INSTALLMENT_NUMBER_IDX As Integer = 3
    Public Const GRID_VSC_COL_BANK_ACCT_OWNER_NAME_IDX As Integer = 4
    Public Const GRID_VSC_COL_BANK_ACCT_NUMBER_IDX As Integer = 5
    Public Const GRID_VSC_COL_BANK_RTN_NUMBER_IDX As Integer = 6
    Public Const GRID_VSC_COL_BILLED_AMOUNT_IDX As Integer = 7
    Public Const GRID_VSC_COL_STATUS_IDX As Integer = 8
    Public Const GRID_VSC_COL_STATUS_DESCRIPTION_IDX As Integer = 9

    Public Const GRID_TOTAL_COLUMNS As Integer = 8

    Private Const COL_CERT_NUMBER As String = "cert_number"
    Private Const COL_INSTALLMENT_NUMBER As String = "Installment_Number"
    Private Const COL_BANK_ACCT_OWNER_NAME As String = "bank_owner_name"
    Private Const COL_BANK_ACCT_NUMBER As String = "bank_acct_number"
    Private Const COL_BANK_RTN_NUMBER As String = "bank_rtn_number"
    Private Const COL_BILLED_AMOUNT As String = "billed_amount"
    Private Const COL_ACCOUNT_TYPE As String = "account_type"
    Private Const COL_STATUS As String = "status"
    Private Const COL_STATUS_DESCRIPTION As String = "Status_Description"
    Private Const COL_REASON As String = "reason"

    Public Const URL As String = "DirectBillingDetailForm.aspx"
#End Region

#Region "Parameters"

    Public Class Parameters
        Public BillingHeaderID As Guid
        Public BillingFileByDealer As Boolean = True
        Public BillingDealerID As Guid
        Public Sub New(ByVal BillingHeaderID As Guid, ByVal BillingFileByDealer As Boolean, ByVal BillingDealerID As Guid)
            Me.BillingHeaderID = BillingHeaderID
            Me.BillingFileByDealer = BillingFileByDealer
            Me.BillingDealerID = BillingDealerID
        End Sub

    End Class

#End Region

#Region "Page State"
    Class MyState
        Public BillingHeaderId As Guid
        Public searchDV As DataView = Nothing
        Public SortExpression As String = " cert_number"
        Public PageIndex As Integer = 0
        Public IsGridVisible As Boolean
        Public selectedPageSize As Integer = DEFAULT_PAGE_SIZE
        Public PageSize As Integer = DEFAULT_PAGE_SIZE
        Public moParams As Parameters
        Public BillingFileByDealer As Boolean
        Public DealerBO As Dealer
    End Class

    Public Sub New()
        MyBase.New(New MyState)
    End Sub

    Protected Shadows ReadOnly Property State() As MyState
        Get
            Return CType(MyBase.State, MyState)
        End Get
    End Property

    Private Sub SetStateProperties()

        Try
            Me.State.moParams = CType(Me.CallingParameters, Parameters)
            If (Me.State.moParams Is Nothing) OrElse (Me.State.moParams.BillingHeaderID.Equals(Guid.Empty)) Then
                If Not (Guid.Empty.Equals(Me.State.BillingHeaderId)) Then
                    Me.State.BillingHeaderId = Me.State.BillingHeaderId
                    Me.State.BillingFileByDealer = Me.State.BillingFileByDealer
                    Me.State.DealerBO = New Assurant.ElitaPlus.BusinessObjectsNew.Dealer(Me.State.moParams.BillingDealerID)
                Else
                    Throw New DataNotFoundException
                End If
            Else
                Me.State.BillingHeaderId = Me.State.moParams.BillingHeaderID
                Me.State.BillingFileByDealer = Me.State.moParams.BillingFileByDealer
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try
    End Sub

    Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
        Try
            Me.MenuEnabled = True
            Dim retObj As ReturnType = CType(ReturnPar, ReturnType)
            'Me.State.HasDataChanged = retObj.HasDataChanged
            If Not retObj Is Nothing AndAlso retObj.HasDataChanged Then
                Me.State.searchDV = Nothing
            End If
            Me.State.IsGridVisible = True
            Select Case retObj.LastOperation
                Case ElitaPlusPage.DetailPageCommand.Back
                    If Not retObj Is Nothing Then
                        Me.State.BillingHeaderId = retObj.BillingHeaderId
                        Me.State.BillingFileByDealer = retObj.BillingFileByDealer
                        Me.State.PageIndex = retObj.page_index
                    End If
            End Select

        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Public Class ReturnType
        Public LastOperation As ElitaPlusPage.DetailPageCommand
        Public BillingHeaderId As Guid
        Public BillingFileByDealer As Boolean
        Public ostate As New MyState
        Public page_index As Integer
        Public HasDataChanged As Boolean = False
        Public Sub New(ByVal LastOp As ElitaPlusPage.DetailPageCommand, ByVal returnPar As Object)
            Me.LastOperation = LastOp

            'DirectCast(returnPar, Assurant.ElitaPlus.ElitaPlusWebApp.Reports.ExceptionsEnhancementReportForm.MyState)
            Me.BillingHeaderId = CType(returnPar, Reports.DirectBillingDetailReportForm.MyState).BillingHeaderId
            If CType(returnPar, Reports.DirectBillingDetailReportForm.MyState).ByDealer = "Y" Then
                Me.BillingFileByDealer = True
            Else
                Me.BillingFileByDealer = False
            End If
            Me.page_index = CType(returnPar, Reports.DirectBillingDetailReportForm.MyState).Page_Index

        End Sub
    End Class
#End Region

#Region "Page Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.ErrController.Clear_Hide()
        Me.SetStateProperties()
        Try
            If Not Me.IsPostBack Then
                PopulateReadOnlyFields()
                ControlMgr.SetVisibleControl(Me, trPageSize, False)
                If Me.State.BillingFileByDealer Then
                    Me.PopulateGrid()
                Else
                    Me.moDealerNameLabel.Visible = False
                    Me.moDealerNameText.Visible = False
                    Me.PopulateGridForNonBillingFileByDealer()
                End If

            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try
        Me.ShowMissingTranslations(Me.ErrController)
    End Sub

#End Region

#Region "Populate"
    Private Sub PopulateReadOnlyFields()
        Try
            Dim oBillingHeader As BillingHeader = New BillingHeader(Me.State.BillingHeaderId)
            With oBillingHeader
                moDealerNameText.Text = .DealerNameLoad
                moFileNameText.Text = .Filename
            End With
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try
    End Sub

    Private Sub PopulateGrid()
        ' hide the VSC grid
        Me.moVSCBillingInformation.Attributes("style") = "display: none"
        ' Show the ESC grid
        Me.moESCBillingInformation.Attributes("style") = ""

        If Me.State.searchDV Is Nothing Then
            If Me.State.DealerBO.UseNewBillForm.Equals(LookupListNew.GetIdFromCode(LookupListNew.GetYesNoLookupList(Authentication.LangId), "Y")) Then
                Me.State.searchDV = BillingPayDetail.getloadBillpayList(Me.State.BillingHeaderId)
            Else
                Me.State.searchDV = BillingDetail.getList(Me.State.BillingHeaderId)
            End If

        End If

        Me.moBillingGrid.AutoGenerateColumns = False
        Me.moBillingGrid.AllowSorting = True
        Me.State.searchDV.Sort = Me.State.SortExpression
        Me.moBillingGrid.Columns(Me.GRID_COL_CERT_NUMBER_IDX).SortExpression = COL_CERT_NUMBER
        Me.moBillingGrid.Columns(Me.GRID_COL_INSTALLMENT_NUMBER_IDX).SortExpression = COL_INSTALLMENT_NUMBER
        Me.moBillingGrid.Columns(Me.GRID_COL_BANK_ACCT_OWNER_NAME_IDX).SortExpression = COL_BANK_ACCT_OWNER_NAME
        Me.moBillingGrid.Columns(Me.GRID_COL_BANK_ACCT_NUMBER_IDX).SortExpression = COL_BANK_ACCT_NUMBER
        Me.moBillingGrid.Columns(Me.GRID_COL_BANK_RTN_NUMBER_IDX).SortExpression = COL_BANK_RTN_NUMBER
        Me.moBillingGrid.Columns(Me.GRID_COL_BILLED_AMOUNT_IDX).SortExpression = COL_BILLED_AMOUNT
        Me.moBillingGrid.Columns(Me.GRID_COL_REASON_IDX).SortExpression = COL_REASON

        SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Guid.Empty, Me.moBillingGrid, Me.State.PageIndex, False)

        Me.State.IsGridVisible = True
        Me.State.PageIndex = Me.moBillingGrid.CurrentPageIndex
        Me.moBillingGrid.DataSource = Me.State.searchDV
        HighLightSortColumn(moBillingGrid, Me.State.SortExpression)
        Me.moBillingGrid.DataBind()
        ControlMgr.SetVisibleControl(Me, moBillingGrid, Me.State.IsGridVisible)
        ControlMgr.SetVisibleControl(Me, trPageSize, Me.moBillingGrid.Visible)

        If Me.moBillingGrid.Visible Then
            Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
        End If
    End Sub

    Private Sub PopulateGridForNonBillingFileByDealer()

        ' hide the ESC grid
        Me.moESCBillingInformation.Attributes("style") = "display: none"
        ' Show the VSC grid
        Me.moVSCBillingInformation.Attributes("style") = ""

        If Me.State.searchDV Is Nothing Then
            If Me.State.DealerBO.UseNewBillForm.Equals(LookupListNew.GetIdFromCode(LookupListNew.GetYesNoLookupList(Authentication.LangId), "Y")) Then
                Me.State.searchDV = BillingPayDetail.getloadBillpayListForNonBillingDealer(Me.State.BillingHeaderId)
            Else
                Me.State.searchDV = BillingDetail.getListForNonBillingByDealer(Me.State.BillingHeaderId)
            End If
        End If

        Me.moVSCBillingGrid.AutoGenerateColumns = False
        Me.moVSCBillingGrid.AllowSorting = True
        Me.State.searchDV.Sort = Me.State.SortExpression
        Me.moVSCBillingGrid.Columns(Me.GRID_VSC_COL_CERT_NUMBER_IDX).SortExpression = COL_CERT_NUMBER
        Me.moVSCBillingGrid.Columns(Me.GRID_VSC_COL_INSTALLMENT_NUMBER_IDX).SortExpression = COL_INSTALLMENT_NUMBER
        Me.moVSCBillingGrid.Columns(Me.GRID_VSC_COL_BANK_ACCT_OWNER_NAME_IDX).SortExpression = COL_BANK_ACCT_OWNER_NAME
        Me.moVSCBillingGrid.Columns(Me.GRID_VSC_COL_BANK_ACCT_NUMBER_IDX).SortExpression = COL_BANK_ACCT_NUMBER
        Me.moVSCBillingGrid.Columns(Me.GRID_VSC_COL_BANK_RTN_NUMBER_IDX).SortExpression = COL_BANK_RTN_NUMBER
        Me.moVSCBillingGrid.Columns(Me.GRID_VSC_COL_BILLED_AMOUNT_IDX).SortExpression = COL_BILLED_AMOUNT
        Me.moVSCBillingGrid.Columns(Me.GRID_VSC_COL_ACCOUNT_TYPE_IDX).SortExpression = COL_ACCOUNT_TYPE
        Me.moVSCBillingGrid.Columns(Me.GRID_VSC_COL_STATUS_IDX).SortExpression = COL_STATUS
        Me.moVSCBillingGrid.Columns(Me.GRID_VSC_COL_STATUS_DESCRIPTION_IDX).SortExpression = COL_STATUS_DESCRIPTION

        SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Guid.Empty, Me.moVSCBillingGrid, Me.State.PageIndex, False)

        Me.State.IsGridVisible = True
        Me.State.PageIndex = Me.moVSCBillingGrid.CurrentPageIndex
        Me.moVSCBillingGrid.DataSource = Me.State.searchDV
        HighLightSortColumn(moVSCBillingGrid, Me.State.SortExpression)
        Me.moVSCBillingGrid.DataBind()
        ControlMgr.SetVisibleControl(Me, moVSCBillingGrid, Me.State.IsGridVisible)
        ControlMgr.SetVisibleControl(Me, trPageSize, Me.moVSCBillingGrid.Visible)

        If Me.moVSCBillingGrid.Visible Then
            Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
        End If
    End Sub
#End Region

#Region "Datagrid Related"

    Private Sub moBillingGrid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles moBillingGrid.PageIndexChanged
        Try
            Me.State.PageIndex = e.NewPageIndex
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try
    End Sub
    ' Start DEF-2771
    Private Sub moVSCBillingGrid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles moVSCBillingGrid.PageIndexChanged
        Try
            Me.State.PageIndex = e.NewPageIndex
            Me.State.BillingHeaderId = Guid.Empty
            Me.PopulateGridForNonBillingFileByDealer()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try
    End Sub

    Public Sub ItemCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        BaseItemCreated(sender, e)
    End Sub
    ' End DEF-2771



    Private Sub moBillingGrid_SortCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles moBillingGrid.SortCommand
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
            Me.State.PageIndex = 0
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try
    End Sub

    Private Sub cboPageSize_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            State.PageSize = CType(cboPageSize.SelectedValue, Integer)

            If Me.State.BillingFileByDealer Then
                Me.State.PageIndex = NewCurrentPageIndex(moBillingGrid, State.searchDV.Count, State.PageSize)
                Me.moBillingGrid.CurrentPageIndex = Me.State.PageIndex
                Me.PopulateGrid()
            Else
                Me.State.PageIndex = NewCurrentPageIndex(moVSCBillingGrid, State.searchDV.Count, State.PageSize)
                Me.moVSCBillingGrid.CurrentPageIndex = Me.State.PageIndex
                Me.moDealerNameLabel.Visible = False
                Me.moDealerNameText.Visible = False
                Me.PopulateGridForNonBillingFileByDealer()
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try
    End Sub

#End Region

#Region "Button Handlers"
    Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBack.Click
        Try
            Me.ReturnToCallingPage()
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try
    End Sub

    Protected Sub btnExportResults_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnExportResults.Click
        Try

            callPage(Reports.DirectBillingDetailReportForm.URL, State)

        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

#End Region



End Class