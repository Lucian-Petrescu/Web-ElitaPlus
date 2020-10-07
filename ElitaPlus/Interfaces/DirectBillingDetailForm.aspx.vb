Partial Public Class DirectBillingDetailForm
    Inherits ElitaPlusSearchPage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
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
        Public Sub New(BillingHeaderID As Guid, BillingFileByDealer As Boolean, BillingDealerID As Guid)
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
            State.moParams = CType(CallingParameters, Parameters)
            If (State.moParams Is Nothing) OrElse (State.moParams.BillingHeaderID.Equals(Guid.Empty)) Then
                If Not (Guid.Empty.Equals(State.BillingHeaderId)) Then
                    State.BillingHeaderId = State.BillingHeaderId
                    State.BillingFileByDealer = State.BillingFileByDealer
                    State.DealerBO = New Assurant.ElitaPlus.BusinessObjectsNew.Dealer(State.moParams.BillingDealerID)
                Else
                    Throw New DataNotFoundException
                End If
            Else
                State.BillingHeaderId = State.moParams.BillingHeaderID
                State.BillingFileByDealer = State.moParams.BillingFileByDealer
            End If

        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try
    End Sub

    Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles MyBase.PageReturn
        Try
            MenuEnabled = True
            Dim retObj As ReturnType = CType(ReturnPar, ReturnType)
            'Me.State.HasDataChanged = retObj.HasDataChanged
            If retObj IsNot Nothing AndAlso retObj.HasDataChanged Then
                State.searchDV = Nothing
            End If
            State.IsGridVisible = True
            Select Case retObj.LastOperation
                Case ElitaPlusPage.DetailPageCommand.Back
                    If retObj IsNot Nothing Then
                        State.BillingHeaderId = retObj.BillingHeaderId
                        State.BillingFileByDealer = retObj.BillingFileByDealer
                        State.PageIndex = retObj.page_index
                    End If
            End Select

        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Public Class ReturnType
        Public LastOperation As ElitaPlusPage.DetailPageCommand
        Public BillingHeaderId As Guid
        Public BillingFileByDealer As Boolean
        Public ostate As New MyState
        Public page_index As Integer
        Public HasDataChanged As Boolean = False
        Public Sub New(LastOp As ElitaPlusPage.DetailPageCommand, returnPar As Object)
            LastOperation = LastOp

            'DirectCast(returnPar, Assurant.ElitaPlus.ElitaPlusWebApp.Reports.ExceptionsEnhancementReportForm.MyState)
            BillingHeaderId = CType(returnPar, Reports.DirectBillingDetailReportForm.MyState).BillingHeaderId
            If CType(returnPar, Reports.DirectBillingDetailReportForm.MyState).ByDealer = "Y" Then
                BillingFileByDealer = True
            Else
                BillingFileByDealer = False
            End If
            page_index = CType(returnPar, Reports.DirectBillingDetailReportForm.MyState).Page_Index

        End Sub
    End Class
#End Region

#Region "Page Events"

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        ErrController.Clear_Hide()
        SetStateProperties()
        Try
            If Not IsPostBack Then
                PopulateReadOnlyFields()
                ControlMgr.SetVisibleControl(Me, trPageSize, False)
                If State.BillingFileByDealer Then
                    PopulateGrid()
                Else
                    moDealerNameLabel.Visible = False
                    moDealerNameText.Visible = False
                    PopulateGridForNonBillingFileByDealer()
                End If

            End If
        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try
        ShowMissingTranslations(ErrController)
    End Sub

#End Region

#Region "Populate"
    Private Sub PopulateReadOnlyFields()
        Try
            Dim oBillingHeader As BillingHeader = New BillingHeader(State.BillingHeaderId)
            With oBillingHeader
                moDealerNameText.Text = .DealerNameLoad
                moFileNameText.Text = .Filename
            End With
        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try
    End Sub

    Private Sub PopulateGrid()
        ' hide the VSC grid
        moVSCBillingInformation.Attributes("style") = "display: none"
        ' Show the ESC grid
        moESCBillingInformation.Attributes("style") = ""

        If State.searchDV Is Nothing Then
            If State.DealerBO.UseNewBillForm.Equals(LookupListNew.GetIdFromCode(LookupListNew.GetYesNoLookupList(Authentication.LangId), "Y")) Then
                State.searchDV = BillingPayDetail.getloadBillpayList(State.BillingHeaderId)
            Else
                State.searchDV = BillingDetail.getList(State.BillingHeaderId)
            End If

        End If

        moBillingGrid.AutoGenerateColumns = False
        moBillingGrid.AllowSorting = True
        State.searchDV.Sort = State.SortExpression
        moBillingGrid.Columns(GRID_COL_CERT_NUMBER_IDX).SortExpression = COL_CERT_NUMBER
        moBillingGrid.Columns(GRID_COL_INSTALLMENT_NUMBER_IDX).SortExpression = COL_INSTALLMENT_NUMBER
        moBillingGrid.Columns(GRID_COL_BANK_ACCT_OWNER_NAME_IDX).SortExpression = COL_BANK_ACCT_OWNER_NAME
        moBillingGrid.Columns(GRID_COL_BANK_ACCT_NUMBER_IDX).SortExpression = COL_BANK_ACCT_NUMBER
        moBillingGrid.Columns(GRID_COL_BANK_RTN_NUMBER_IDX).SortExpression = COL_BANK_RTN_NUMBER
        moBillingGrid.Columns(GRID_COL_BILLED_AMOUNT_IDX).SortExpression = COL_BILLED_AMOUNT
        moBillingGrid.Columns(GRID_COL_REASON_IDX).SortExpression = COL_REASON

        SetPageAndSelectedIndexFromGuid(State.searchDV, Guid.Empty, moBillingGrid, State.PageIndex, False)

        State.IsGridVisible = True
        State.PageIndex = moBillingGrid.CurrentPageIndex
        moBillingGrid.DataSource = State.searchDV
        HighLightSortColumn(moBillingGrid, State.SortExpression)
        moBillingGrid.DataBind()
        ControlMgr.SetVisibleControl(Me, moBillingGrid, State.IsGridVisible)
        ControlMgr.SetVisibleControl(Me, trPageSize, moBillingGrid.Visible)

        If moBillingGrid.Visible Then
            lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
        End If
    End Sub

    Private Sub PopulateGridForNonBillingFileByDealer()

        ' hide the ESC grid
        moESCBillingInformation.Attributes("style") = "display: none"
        ' Show the VSC grid
        moVSCBillingInformation.Attributes("style") = ""

        If State.searchDV Is Nothing Then
            If State.DealerBO.UseNewBillForm.Equals(LookupListNew.GetIdFromCode(LookupListNew.GetYesNoLookupList(Authentication.LangId), "Y")) Then
                State.searchDV = BillingPayDetail.getloadBillpayListForNonBillingDealer(State.BillingHeaderId)
            Else
                State.searchDV = BillingDetail.getListForNonBillingByDealer(State.BillingHeaderId)
            End If
        End If

        moVSCBillingGrid.AutoGenerateColumns = False
        moVSCBillingGrid.AllowSorting = True
        State.searchDV.Sort = State.SortExpression
        moVSCBillingGrid.Columns(GRID_VSC_COL_CERT_NUMBER_IDX).SortExpression = COL_CERT_NUMBER
        moVSCBillingGrid.Columns(GRID_VSC_COL_INSTALLMENT_NUMBER_IDX).SortExpression = COL_INSTALLMENT_NUMBER
        moVSCBillingGrid.Columns(GRID_VSC_COL_BANK_ACCT_OWNER_NAME_IDX).SortExpression = COL_BANK_ACCT_OWNER_NAME
        moVSCBillingGrid.Columns(GRID_VSC_COL_BANK_ACCT_NUMBER_IDX).SortExpression = COL_BANK_ACCT_NUMBER
        moVSCBillingGrid.Columns(GRID_VSC_COL_BANK_RTN_NUMBER_IDX).SortExpression = COL_BANK_RTN_NUMBER
        moVSCBillingGrid.Columns(GRID_VSC_COL_BILLED_AMOUNT_IDX).SortExpression = COL_BILLED_AMOUNT
        moVSCBillingGrid.Columns(GRID_VSC_COL_ACCOUNT_TYPE_IDX).SortExpression = COL_ACCOUNT_TYPE
        moVSCBillingGrid.Columns(GRID_VSC_COL_STATUS_IDX).SortExpression = COL_STATUS
        moVSCBillingGrid.Columns(GRID_VSC_COL_STATUS_DESCRIPTION_IDX).SortExpression = COL_STATUS_DESCRIPTION

        SetPageAndSelectedIndexFromGuid(State.searchDV, Guid.Empty, moVSCBillingGrid, State.PageIndex, False)

        State.IsGridVisible = True
        State.PageIndex = moVSCBillingGrid.CurrentPageIndex
        moVSCBillingGrid.DataSource = State.searchDV
        HighLightSortColumn(moVSCBillingGrid, State.SortExpression)
        moVSCBillingGrid.DataBind()
        ControlMgr.SetVisibleControl(Me, moVSCBillingGrid, State.IsGridVisible)
        ControlMgr.SetVisibleControl(Me, trPageSize, moVSCBillingGrid.Visible)

        If moVSCBillingGrid.Visible Then
            lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
        End If
    End Sub
#End Region

#Region "Datagrid Related"

    Private Sub moBillingGrid_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles moBillingGrid.PageIndexChanged
        Try
            State.PageIndex = e.NewPageIndex
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try
    End Sub
    ' Start DEF-2771
    Private Sub moVSCBillingGrid_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles moVSCBillingGrid.PageIndexChanged
        Try
            State.PageIndex = e.NewPageIndex
            State.BillingHeaderId = Guid.Empty
            PopulateGridForNonBillingFileByDealer()
        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try
    End Sub

    Public Sub ItemCreated(sender As System.Object, e As System.Web.UI.WebControls.DataGridItemEventArgs)
        BaseItemCreated(sender, e)
    End Sub
    ' End DEF-2771



    Private Sub moBillingGrid_SortCommand(source As System.Object, e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles moBillingGrid.SortCommand
        Try
            If State.SortExpression.StartsWith(e.SortExpression) Then
                If State.SortExpression.EndsWith(" DESC") Then
                    State.SortExpression = e.SortExpression
                Else
                    State.SortExpression &= " DESC"
                End If
            Else
                State.SortExpression = e.SortExpression
            End If
            State.PageIndex = 0
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try
    End Sub

    Private Sub cboPageSize_PageSizeChanged(source As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            State.PageSize = CType(cboPageSize.SelectedValue, Integer)

            If State.BillingFileByDealer Then
                State.PageIndex = NewCurrentPageIndex(moBillingGrid, State.searchDV.Count, State.PageSize)
                moBillingGrid.CurrentPageIndex = State.PageIndex
                PopulateGrid()
            Else
                State.PageIndex = NewCurrentPageIndex(moVSCBillingGrid, State.searchDV.Count, State.PageSize)
                moVSCBillingGrid.CurrentPageIndex = State.PageIndex
                moDealerNameLabel.Visible = False
                moDealerNameText.Visible = False
                PopulateGridForNonBillingFileByDealer()
            End If

        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try
    End Sub

#End Region

#Region "Button Handlers"
    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Try
            ReturnToCallingPage()
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try
    End Sub

    Protected Sub btnExportResults_Click(sender As Object, e As EventArgs) Handles btnExportResults.Click
        Try

            callPage(Reports.DirectBillingDetailReportForm.URL, State)

        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

#End Region



End Class