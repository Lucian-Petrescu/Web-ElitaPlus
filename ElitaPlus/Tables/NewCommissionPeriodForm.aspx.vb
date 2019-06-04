Namespace Tables

    Partial Class NewCommissionPeriodForm
        Inherits ElitaPlusSearchPage
        'Inherits System.Web.UI.Page

        '#Region "MyState"

        '        Class MyState
        '            Public MyBo As CommissionPeriod = New CommissionPeriod
        '            Public PageIndex As Integer = 0
        '            Public selectedPageSize As Integer = DEFAULT_PAGE_SIZE
        '        End Class

        '        Public Sub New()
        '            MyBase.New(New MyState)
        '        End Sub

        '        Protected Shadows ReadOnly Property State() As MyState
        '            Get
        '                Return CType(MyBase.State, MyState)
        '            End Get
        '        End Property

        '#End Region

        '#Region "Constants"

        '        Public Const URL As String = "NewCommissionPeriodForm.aspx"

        '        ' Property Name
        '        Public Const COMMISSION_PERIOD_ID_PROPERTY As String = "CommissionPeriodId"
        '        Public Const DEALER_ID_PROPERTY As String = "DealerId"
        '        Public Const EFFECTIVE_DATE_PROPERTY As String = "EffectiveDate"
        '        Public Const EXPIRATION_DATE_PROPERTY As String = "ExpirationDate"

        '#Region "Constants-Breakdown"

        '        Private Const COMMISSION_BREAKDOWN_ID_COL As Integer = 2
        '        Private Const ALLOWED_MARKUP_COL As Integer = 3
        '        Private Const TOLERANCE_COL As Integer = 4
        '        Private Const DEALER_MARKUP_COL As Integer = 5
        '        Private Const BROKER_MARKUP_COL As Integer = 6
        '        Private Const BROKER2_MARKUP_COL As Integer = 7
        '        Private Const BROKER3_MARKUP_COL As Integer = 8
        '        Private Const BROKER4_MARKUP_COL As Integer = 9
        '        Private Const DEALER_COMM_COL As Integer = 10
        '        Private Const BROKER_COMM_COL As Integer = 11
        '        Private Const BROKER2_COMM_COL As Integer = 12
        '        Private Const BROKER3_COMM_COL As Integer = 13
        '        Private Const BROKER4_COMM_COL As Integer = 14

        '        ' Property Name
        '        Public Const ALLOWED_MARKUP_PCT_PROPERTY As String = "AllowedMarkupPct"
        '        Public Const TOLERANCE_PROPERTY As String = "Tolerance"
        '        Public Const DEALER_MARKUP_PCT_PROPERTY As String = "DealerMarkupPct"
        '        Public Const DEALER_COMM_PCT_PROPERTY As String = "DealerCommPct"
        '        Public Const BROKER_MARKUP_PCT_PROPERTY As String = "BrokerMarkupPct"
        '        Public Const BROKER_COMM_PCT_PROPERTY As String = "BrokerCommPct"
        '        Public Const BROKER2_MARKUP_PCT_PROPERTY As String = "Broker2MarkupPct"
        '        Public Const BROKER2_COMM_PCT_PROPERTY As String = "Broker2CommPct"
        '        Public Const BROKER3_MARKUP_PCT_PROPERTY As String = "Broker3MarkupPct"
        '        Public Const BROKER3_COMM_PCT_PROPERTY As String = "Broker3CommPct"
        '        Public Const BROKER4_MARKUP_PCT_PROPERTY As String = "Broker4MarkupPct"
        '        Public Const BROKER4_COMM_PCT_PROPERTY As String = "Broker4CommPct"

        '#End Region

        '#End Region
        '#Region "Properties"

        '        Private ReadOnly Property ThePeriod() As CommissionPeriod
        '            Get
        '                If moPeriod Is Nothing Then
        '                    If Me.State.IsPeriodNew = True Then
        '                        ' For creating, inserting
        '                        moPeriod = New CommissionPeriod
        '                        Me.State.moCommissionPeriodId = moPeriod.Id
        '                    Else
        '                        ' For updating, deleting
        '                        moPeriod = New CommissionPeriod(Me.State.moCommissionPeriodId)
        '                    End If
        '                End If

        '                Return moPeriod
        '            End Get
        '        End Property

        '        Private ReadOnly Property ExpirationCount() As Integer
        '            Get
        '                If moExpirationData Is Nothing Then
        '                    moExpirationData = New CommissionPeriodData
        '                    moExpirationData.dealerId = Me.GetSelectedItem(moDealerDrop_WRITE)
        '                End If
        '                Return ThePeriod.ExpirationCount(moExpirationData)
        '            End Get
        '        End Property

        '        Private ReadOnly Property MaxExpiration() As Date
        '            Get
        '                If moExpirationData Is Nothing Then
        '                    moExpirationData = New CommissionPeriodData
        '                    moExpirationData.dealerId = Me.GetSelectedItem(moDealerDrop_WRITE)
        '                End If
        '                Return ThePeriod.MaxExpiration(moExpirationData)
        '            End Get
        '        End Property

        '#Region "Properties-Breakdown"

        '        Private ReadOnly Property TheBreakdown() As CommissionBreakdown
        '            Get
        '                If Me.State.IsBreakdownNew = True Then
        '                    ' For creating, inserting
        '                    moBreakdown = New CommissionBreakdown
        '                    Me.State.moCommissionBreakdownId = moBreakdown.Id
        '                Else
        '                    ' For updating, deleting
        '                    moBreakdown = New CommissionBreakdown(Me.State.moCommissionBreakdownId)
        '                End If

        '                Return moBreakdown
        '            End Get
        '        End Property

        '#End Region

        '#End Region


        '#Region " Web Form Designer Generated Code "

        '        'This call is required by the Web Form Designer.
        '        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        '        End Sub

        '        Private moPeriod As CommissionPeriod
        '        Private moExpirationData As CommissionPeriodData
        '        Protected WithEvents moErrorController As ErrorController
        '        Protected WithEvents moErrorControllerGrid As ErrorController
        '        Protected WithEvents moTitleLabel1 As System.Web.UI.WebControls.Label
        '        Protected WithEvents moTitleLabel2 As System.Web.UI.WebControls.Label
        '        Protected WithEvents Label9 As System.Web.UI.WebControls.Label
        '        Protected WithEvents moDealerLabel As System.Web.UI.WebControls.Label
        '        Protected WithEvents moDealerDrop_WRITE As System.Web.UI.WebControls.DropDownList
        '        Protected WithEvents moEffectiveLabel As System.Web.UI.WebControls.Label
        '        Protected WithEvents moEffectiveText_WRITE As System.Web.UI.WebControls.TextBox
        '        Protected WithEvents BtnEffectiveDate_WRITE As System.Web.UI.WebControls.ImageButton
        '        Protected WithEvents moExpirationLabel As System.Web.UI.WebControls.Label
        '        Protected WithEvents moExpirationText_WRITE As System.Web.UI.WebControls.TextBox
        '        Protected WithEvents BtnExpirationDate_WRITE As System.Web.UI.WebControls.ImageButton
        '        Protected WithEvents moPeriodPanel_WRITE As System.Web.UI.WebControls.Panel
        '        Protected WithEvents EditPanel As System.Web.UI.WebControls.Panel
        '        Protected WithEvents btnBack As System.Web.UI.WebControls.Button
        '        Protected WithEvents btnSave_WRITE As System.Web.UI.WebControls.Button
        '        Protected WithEvents btnUndo_WRITE As System.Web.UI.WebControls.Button
        '        Protected WithEvents btnNew_WRITE As System.Web.UI.WebControls.Button
        '        Protected WithEvents btnCopy_WRITE As System.Web.UI.WebControls.Button
        '        Protected WithEvents btnDelete_WRITE As System.Web.UI.WebControls.Button
        '        Protected WithEvents moPeriodButtonPanel As System.Web.UI.WebControls.Panel
        '        Protected WithEvents HiddenSaveChangesPromptResponse As System.Web.UI.HtmlControls.HtmlInputHidden
        '        Protected WithEvents moAllowedMarkupPctDetailLabel As System.Web.UI.WebControls.Label
        '        Protected WithEvents moAllowedMarkupPctDetailText As System.Web.UI.WebControls.TextBox
        '        Protected WithEvents moToleranceDetailLabel As System.Web.UI.WebControls.Label
        '        Protected WithEvents moToleranceDetailText As System.Web.UI.WebControls.TextBox
        '        Protected WithEvents moDealerMarkupPctDetailLabel As System.Web.UI.WebControls.Label
        '        Protected WithEvents moDealerMarkupPctDetailText As System.Web.UI.WebControls.TextBox
        '        Protected WithEvents moDealerCommPctDetailLabel As System.Web.UI.WebControls.Label
        '        Protected WithEvents moDealerCommPctDetailText As System.Web.UI.WebControls.TextBox
        '        Protected WithEvents moRestrictDetailPanel As System.Web.UI.WebControls.Panel
        '        Protected WithEvents Panel1 As System.Web.UI.WebControls.Panel
        '        Protected WithEvents lblPageSize As System.Web.UI.WebControls.Label
        '        Protected WithEvents cboPageSize As System.Web.UI.WebControls.DropDownList
        '        Protected WithEvents lblRecordCount As System.Web.UI.WebControls.Label
        '        Protected WithEvents Grid As System.Web.UI.WebControls.DataGrid
        '        Protected WithEvents moGridPanel As System.Web.UI.WebControls.Panel
        '        Protected WithEvents BtnNewGrid_WRITE As System.Web.UI.WebControls.Button
        '        Protected WithEvents BtnSaveGrid_WRITE As System.Web.UI.WebControls.Button
        '        Protected WithEvents moDetailButtonPanel As System.Web.UI.WebControls.Panel
        '        Protected WithEvents trPageSize As System.Web.UI.HtmlControls.HtmlTableRow
        '        Protected WithEvents BtnUndoGrid As System.Web.UI.WebControls.Button

        '        'NOTE: The following placeholder declaration is required by the Web Form Designer.
        '        'Do not delete or move it.
        '        Private designerPlaceholderDeclaration As System.Object

        '        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        '            'CODEGEN: This method call is required by the Web Form Designer
        '            'Do not modify it using the code editor.
        '            InitializeComponent()
        '        End Sub

        '#End Region

        '        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '            'Put user code to initialize the page here
        '        End Sub
        '#Region "Populate"

        '        Private Sub PopulateDealer()
        '            Me.BindListControlToDataView(moDealerDrop_WRITE, _
        '                LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies), , , True)

        '            If Me.State.IsPeriodNew = True Then
        '                BindSelectItem(Nothing, moDealerDrop_WRITE)
        '            Else
        '                BindSelectItem(ThePeriod.DealerId.ToString, moDealerDrop_WRITE)
        '            End If

        '        End Sub

        '        Private Sub PopulateDates()
        '            Me.PopulateControlFromBOProperty(moEffectiveText_WRITE, ThePeriod.EffectiveDate)
        '            Me.PopulateControlFromBOProperty(moExpirationText_WRITE, ThePeriod.ExpirationDate)
        '        End Sub

        '        Private Sub PopulatePeriod()
        '            Try
        '                PopulateDealer()
        '                PopulateDates()
        '                EnableDateFields()
        '                PopulateBreakdown()
        '            Catch ex As Exception
        '                Me.HandleErrors(ex, Me.moErrorController)
        '            End Try

        '        End Sub

        '#End Region
        '#Region "Clear"

        '        Private Sub ClearDealer()
        '            If Me.State.IsPeriodNew = True Then
        '                moDealerDrop_WRITE.SelectedIndex = 0
        '            Else
        '                Me.SetSelectedItem(moDealerDrop_WRITE, ThePeriod.DealerId)
        '            End If

        '        End Sub

        '        Private Sub ClearPeriod()
        '            ClearDealer()
        '            '        EnableDateFields()
        '            ClearBreakdown()
        '        End Sub

        '#End Region
    End Class
End Namespace