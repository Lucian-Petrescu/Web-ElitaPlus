Imports Microsoft.VisualBasic
Imports System.Collections.Generic
Imports AjaxControlToolkit
Imports System.Text

Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Imports Assurant.Elita.CommonConfiguration.DataElements


Partial Class ClaimAdjusterInboxForm
    Inherits ElitaPlusSearchPage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents ErrorCtrl As ErrorController
    Protected WithEvents lblBlank As System.Web.UI.WebControls.Label
    Protected WithEvents trSortBy As System.Web.UI.HtmlControls.HtmlTableRow

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
    Public Const GRID_COL_CLAIM_ID_IDX As Integer = 0
    Public Const GRID_COL_CHK_BOX_IDX As Integer = 1
    Public Const GRID_COL_EDIT_IDX As Integer = 2
    Public Const GRID_COL_CLAIM_TAT_IDX As Integer = 3
    Public Const GRID_COL_SVC_TAT_IDX As Integer = 4
    Public Const GRID_COL_CLAIM_NUMBER_IDX As Integer = 5
    Public Const GRID_COL_STATUS_IDX As Integer = 6
    Public Const GRID_COL_CLAIM_ADJUSTER_IDX As Integer = 7
    Public Const GRID_COL_ADDED_BY_IDX As Integer = 8
    Public Const GRID_COL_AUTO_APPROVED_IDX As Integer = 9
    Public Const GRID_COL_CLAIM_EXT_STATUS_IDX As Integer = 10
    Public Const GRID_COL_STATUS_OWNER_IDX As Integer = 11
    Public Const GRID_COL_AUTHORIZATION_NUMBER_IDX As Integer = 12
    Public Const GRID_COL_SERVICE_CENTER_IDX As Integer = 13
    Public Const GRID_COL_CLAIM_TYPE_IDX As Integer = 14
    Public Const GRID_COL_PRODUCT_IDX As Integer = 15

    Public Const GRID_COL_SALES_PRICE_IDX As Integer = 16
    Public Const GRID_COL_COVERAGE_TYPE_IDX As Integer = 17
    Public Const GRID_COL_END_DATE_IDX As Integer = 18
    Public Const GRID_COL_PAYMENT_AMOUNT_IDX As Integer = 19

    Public Const GRID_COL_AUTH_AMOUNT_IDX As Integer = 20
    Public Const GRID_COL_PROPOSED_AMOUNT_IDX As Integer = 21
    Public Const GRID_COL_LABOR_AMOUNT_IDX As Integer = 22
    Public Const GRID_COL_PARTS_AMOUNT_IDX As Integer = 23
    Public Const GRID_COL_OTHER_AMOUNT_IDX As Integer = 24
    Public Const GRID_COL_SHIPPING_AMOUNT_IDX As Integer = 25
    Public Const GRID_COL_TRIP_AMOUNT_IDX As Integer = 26
    Public Const GRID_COL_SVC_CHARGE_AMOUNT_IDX As Integer = 27

    Public Const GRID_COL_RISK_TYPE_ID_IDX As Integer = 28
    Public Const GRID_COL_PROBLEM_DESCRIPTION_IDX As Integer = 29
    Public Const GRID_COL_TECHNICAL_REPORT_IDX As Integer = 30
    Public Const GRID_COL_EXTENDED_STATUS_COMMENT_IDX As Integer = 31

    Public Const GRID_COL_INBOUND_TRACKING_NUMBER_IDX As Integer = 32
    Public Const GRID_COL_OUTBOUND_TRACKING_NUMBER_IDX As Integer = 33
    Public Const GRID_COL_REPLACEMENT_DEVICE_IDX As Integer = 34
    Public Const GRID_COL_REPLACEMENT_DEVICE_COMMENTS_IDX As Integer = 35

    Public Const GRID_COL_COMMENT_IDX As Integer = 36

    'Public Const GRID_COL_CLAIM_ID_IDX As Integer = 7

    Public Const GRID_TOTAL_COLUMNS As Integer = 37

    Private Const DEFAULT_PAGE_INDEX As Integer = 0

    Private Const GRID_CTRL_NAME_CHECKBOX As String = "btnSelected"
    Private Const GRID_CTRL_NAME_TEXTBOX As String = "txtComment"
    Private Const GRID_CTRL_NAME_CLAIM_ID As String = "lblTransactionLogHeaderID"

    Protected checkValueArray() As String

    Private Const EDIT_COMMAND As String = "EditRecord"

#End Region

#Region "Page State"
    Private IsReturningFromChild As Boolean = False

    Class MyState
        Public PageIndex As Integer = 0
        Public SortExpression As String = Claim.ClaimAdjusterSearchDV.COL_CLAIM_NUMBER
        Public selectedClaimId As Guid = Guid.Empty
        Public claimNumber As String

        '  Public selectedServiceCenterId As Guid = Guid.Empty
        '  Public selectedServiceCenterIds As ArrayList
        Public serviceCenterName As String = String.Empty
        Public selectedSortById As Guid = Guid.Empty
        Public selectedSortOrderId As Guid = Guid.Empty
        Public selectedClaimTypeId As Guid = Guid.Empty
        Public selectedClaimExtendedStatusId As Guid = Guid.Empty
        Public selectedClaimExtendedStatusOwnerId As Guid = Guid.Empty
        Public selectedTATId As Guid = Guid.Empty
        Public selectedAutoApproveId As Guid = Guid.Empty
        Public selectedAutoApproveDesc As String
        Public BeginDate As String
        Public EndDate As String
        Public ClaimAdjuster As String = String.Empty
        Public ClaimAddedBy As String = String.Empty
        Public selectedPageSize As Integer = DEFAULT_PAGE_SIZE
        Public authorizationNumber As String
        Public authorizedAmount As String
        Public IsGridVisible As Boolean = False
        Public searchDV As Claim.ClaimAdjusterSearchDV = Nothing
        Public SearchClicked As Boolean
        Public authorizedAmountCulture As String
        Public selectedClaimStausCode As String = String.Empty
        Public ActionInProgress As DetailPageCommand = ElitaPlusPage.DetailPageCommand.Nothing_
        Public cmdProcessRecord As String = String.Empty
        Public selectedPageIndex As Integer = DEFAULT_PAGE_INDEX

        Public TATDV As DataView = Nothing

        Sub New()
            'selectedServiceCenterIds = New ArrayList
            'selectedServiceCenterIds.Add(Guid.Empty)
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

#End Region
#Region "Properties"


    Public Property SortDirection() As String
        Get
            Return ViewState("SortDirection").ToString
        End Get
        Set(ByVal value As String)
            ViewState("SortDirection") = value
        End Set
    End Property

    Private comments As String
    Private risktypes As String


#End Region
#Region "Page_Events"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Page.RegisterHiddenField("__EVENTTARGET", Me.btnSearch.ClientID)
        Me.ErrorCtrl.Clear_Hide()
        Try
            If Not Me.IsPostBack Then


                Me.SortDirection = Me.State.SortExpression
                Me.SetDefaultButton(Me.txtClaimNumber, btnSearch)
                Me.SetDefaultButton(Me.txtAuthorizationNumber, btnSearch)
                Me.SetDefaultButton(txtServiceCenterName, btnSearch)
                ControlMgr.SetVisibleControl(Me, trPageSize, False)
                Me.tDDataGrid.Visible = False
                Me.tDDataGrid1.Visible = False
                PopulateDropDowns()
                PopulateSearchFieldsFromState()
                TranslateGridHeader(Me.modataGrid)

                Me.AddCalendar(Me.BtnBeginDate, Me.txtExt_Sts_Date_From)
                Me.AddCalendar(Me.BtnEndDate, Me.txtExt_Sts_Date_To)

                If Me.State.IsGridVisible Then
                    If Not (Me.State.selectedPageSize = DEFAULT_PAGE_SIZE) Then
                        cboPageSize.SelectedValue = CType(Me.State.selectedPageSize, String)
                        modataGrid.PageSize = Me.State.selectedPageSize
                    End If
                    Me.PopulateGrid()
                    Me.tDDataGrid.Visible = True
                    Me.tDDataGrid1.Visible = True
                End If
                Me.SetGridItemStyleColor(Me.modataGrid)
                SetFocus(Me.txtClaimNumber)

            End If

            Dim objCompanyGroup As CompanyGroup = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup
            If (GetFastApprovalNoID() = objCompanyGroup.ClaimFastApprovalId) Then
                modataGrid.Columns(Me.GRID_COL_SALES_PRICE_IDX).Visible = False
                modataGrid.Columns(Me.GRID_COL_PAYMENT_AMOUNT_IDX).Visible = False
                modataGrid.Columns(Me.GRID_COL_END_DATE_IDX).Visible = False
                modataGrid.Columns(Me.GRID_COL_COVERAGE_TYPE_IDX).Visible = False
                modataGrid.Columns(Me.GRID_COL_PROBLEM_DESCRIPTION_IDX).Visible = False
                modataGrid.Columns(Me.GRID_COL_TECHNICAL_REPORT_IDX).Visible = False
                modataGrid.Columns(Me.GRID_COL_EXTENDED_STATUS_COMMENT_IDX).Visible = False
                modataGrid.Columns(Me.GRID_COL_COMMENT_IDX).Visible = False
            End If

            Me.DisplayProgressBarOnClick(Me.btnSearch, "Loading_Claims")
            Me.DisplayProgressBarOnClick(Me.ApproveButton_WRITE, "Processing")
            Me.DisplayProgressBarOnClick(Me.RejectButton_WRITE, "Processing")
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
        Me.ShowMissingTranslations(Me.ErrorCtrl)
    End Sub

    'Private Sub SetDefaultButton(ByVal txt As TextBox, ByVal defaultButton As Button)
    '    txt.Attributes.Add("onkeydown", "fnTrapKD(" + defaultButton.ClientID + ",event)")
    'End Sub

    Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
        Try
            Me.MenuEnabled = True
            Me.IsReturningFromChild = True
            Dim retObj As ClaimForm.ReturnType = CType(Me.ReturnedValues, ClaimForm.ReturnType)
            If Not retObj Is Nothing AndAlso retObj.BoChanged Then
                Me.State.searchDV = Nothing
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

#End Region

#Region "Controlling Logic"

    'Request 5547
    Private Function GetFastApprovalNoID() As Guid
        Dim noId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_FAST_APPROVAL_TYPE, Codes.YESNO_N)
        Return noId
    End Function

    Sub PopulateDropDowns()

        Try
            'Sort By

            Dim sortBy As ListItem() = CommonConfigManager.Current.ListManager.GetList("ADJ_INBOX", Thread.CurrentPrincipal.GetLanguageCode(), Nothing)
            cboSortBy.Populate(sortBy, New PopulateOptions() With
                                                  {
                                                    .AddBlankItem = False
                                                   })

            Dim defaultSelectedSortByCodeId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_ADJUSTER_CLAIM_SEARCH_FIELDS, Codes.DEFAULT_SORT_FOR_ADJUSTER_INBOX)
            If (Me.State.selectedSortById.Equals(Guid.Empty)) Then
                Me.SetSelectedItem(Me.cboSortBy, defaultSelectedSortByCodeId)
                Me.State.selectedSortById = defaultSelectedSortByCodeId
            Else
                Me.SetSelectedItem(Me.cboSortBy, Me.State.selectedSortById)
            End If

            Dim oSortOrder As ListItem() = CommonConfigManager.Current.ListManager.GetList("SORT_ORDER", Thread.CurrentPrincipal.GetLanguageCode())
            cboSortOrder.Populate(oSortOrder, New PopulateOptions() With
                                                  {
                                                    .AddBlankItem = False
                                                   })


            Dim defaultSelectedSortOrderCodeId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_SORT_ORDER, Codes.DEFAULT_SORT_ORDER_FOR_ADJUSTER_INBOX)
            If (Me.State.selectedSortOrderId.Equals(Guid.Empty)) Then
                Me.SetSelectedItem(Me.cboSortOrder, defaultSelectedSortOrderCodeId)
                Me.State.selectedSortOrderId = defaultSelectedSortOrderCodeId
            Else
                Me.SetSelectedItem(Me.cboSortOrder, Me.State.selectedSortOrderId)
            End If

            'Claim Types

            Dim oClaimTypes As ListItem() = CommonConfigManager.Current.ListManager.GetList("CLTYP", Thread.CurrentPrincipal.GetLanguageCode())
            cboClaimType.Populate(oClaimTypes, New PopulateOptions() With
                                                  {
                                                    .AddBlankItem = True
                                                   })


            If Not (Me.State.selectedClaimTypeId.Equals(Guid.Empty)) Then
                Me.SetSelectedItem(Me.cboClaimType, Me.State.selectedClaimTypeId)
            End If

            'Claim Extended Status

            Dim listcontext As ListContext = New ListContext()
            listcontext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id

            Dim oClaimExtendedStatus As ListItem() = CommonConfigManager.Current.ListManager.GetList("ExtendedStatusByCompanyGroup", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
            cboClaimExtendedStatus.Populate(oClaimExtendedStatus, New PopulateOptions() With
                                                  {
                                                    .AddBlankItem = True
                                                   })


            If Not (Me.State.selectedClaimExtendedStatusId.Equals(Guid.Empty)) Then
                Me.SetSelectedItem(Me.cboClaimExtendedStatus, Me.State.selectedClaimExtendedStatusId)
            End If
            Me.cboClaimExtendedStatus.Attributes.Add("onchange", "ToggleSelection1('" & cboClaimExtendedStatus.ClientID & "','" & cboStatusOwner.ClientID & "')")

            'Claim Extended Status Owner

            Dim oExtendedStatusOwner As ListItem() = CommonConfigManager.Current.ListManager.GetList("EXTOWN", Thread.CurrentPrincipal.GetLanguageCode())
            cboStatusOwner.Populate(oExtendedStatusOwner, New PopulateOptions() With
                                                  {
                                                    .AddBlankItem = True
                                                   })

            If Not (Me.State.selectedClaimExtendedStatusOwnerId.Equals(Guid.Empty)) Then
                Me.SetSelectedItem(Me.cboStatusOwner, Me.State.selectedClaimExtendedStatusOwnerId)
            End If
            Me.cboStatusOwner.Attributes.Add("onchange", "ToggleSelection2('" & cboStatusOwner.ClientID & "','" & cboClaimExtendedStatus.ClientID & "')")

            'SC_Turn_Around_Time list
            'Dim oSCTurnAroundTimeDv As DataView
            'oSCTurnAroundTimeDv = LookupListNew.GetSCTurnAroundTimeByGroupLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)
            'Me.BindListControlToDataView(Me.cboSCTurnAroundTime, oSCTurnAroundTimeDv, , , True)

            Dim SCTurnAroundTime As ListItem() = CommonConfigManager.Current.ListManager.GetList("LkScTatByGroupList", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
            cboSCTurnAroundTime.Populate(SCTurnAroundTime, New PopulateOptions() With
                                                  {
                                                    .AddBlankItem = True
                                                   })


            If Not (Me.State.selectedTATId.Equals(Guid.Empty)) Then
                Me.SetSelectedItem(Me.cboSCTurnAroundTime, Me.State.selectedTATId)
            End If

            'AutoApproved
            'Dim oAutoApprovedDv As DataView = LookupListNew.DropdownLookupList("YESNO", ElitaPlusIdentity.Current.ActiveUser.LanguageId, True)
            ' Me.BindListControlToDataView(Me.cboAutoApproved, oAutoApprovedDv, , , True)

            Dim oAutoApproved As ListItem() = CommonConfigManager.Current.ListManager.GetList("YESNO", Thread.CurrentPrincipal.GetLanguageCode())
            cboAutoApproved.Populate(oAutoApproved, New PopulateOptions() With
                                                  {
                                                    .AddBlankItem = True
                                                   })

            If Not (Me.State.selectedAutoApproveId.Equals(Guid.Empty)) Then
                Me.SetSelectedItem(Me.cboAutoApproved, Me.State.selectedAutoApproveId)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Public Sub PutInvisibleSvcColumns(ByVal oGrid As DataGrid)
        Try
            'If ElitaPlusIdentity.Current.ActiveUser.IsServiceCenter Then
            '    'oGrid.Columns(GRID_COL_SERVICE_CENTER_IDX).Visible = False
            '    ' oGrid.Columns(GRID_COL_AUTHORIZATION_NUMBER_IDX).Visible = False
            '    'oGrid.Columns(GRID_COL_AUTHORIZED_AMOUNT_IDX).Visible = False
            'End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub BuildCheckBoxIDsArray()
        'Each time the data is bound to the grid we need to build up the CheckBoxIDs array

        'Get the header CheckBox
        Dim cbHeader As CheckBox = CType(Me.modataGrid.HeaderRow.FindControl("HeaderLevelCheckBox"), CheckBox)

        'Run the ChangeCheckBoxState client-side function whenever the
        'header checkbox is checked/unchecked
        cbHeader.Attributes("onclick") = "ChangeAllCheckBoxStates(this.checked, '" & Me.ApproveButton_WRITE.ClientID & "', '" & Me.RejectButton_WRITE.ClientID & "');"

        'Add the CheckBox's ID to the client-side CheckBoxIDs array
        Dim ArrayValues As New List(Of String)
        ArrayValues.Add(String.Concat("'", cbHeader.ClientID, "'"))

        For Each gvr As GridViewRow In Me.modataGrid.Rows
            'Get a programmatic reference to the CheckBox control
            Dim cb As CheckBox = CType(gvr.FindControl("btnSelected"), CheckBox)

            'If the checkbox is unchecked, ensure that the Header CheckBox is unchecked
            'cb.Attributes("onclick") = "ChangeHeaderAsNeeded();"

            'Add the CheckBox's ID to the client-side CheckBoxIDs array
            If Not cb Is Nothing Then ArrayValues.Add(String.Concat("'", cb.ClientID, "'"))
        Next

        'Output the array to the Literal control (CheckBoxIDsArray)
        CheckBoxIDsArray.Text = "<script type=""text/javascript"">" & vbCrLf &
                                "<!--" & vbCrLf &
                                String.Concat("var CheckBoxIDs =  new Array(", String.Join(",", ArrayValues.ToArray()), ");") & vbCrLf &
                                "// -->" & vbCrLf &
                                "</script>"
    End Sub

    Public Sub PopulateGrid()

        Try
            If Not PopulateStateFromSearchFields() Then Exit Sub

            If (Me.State.searchDV Is Nothing) Then
                Dim sortBy As String
                sortBy = LookupListNew.GetCodeFromId(LookupListNew.LK_ADJUSTER_CLAIM_SEARCH_FIELDS, Me.State.selectedSortById)

                Dim SortOrderCode As String
                SortOrderCode = LookupListNew.GetCodeFromId(LookupListNew.LK_SORT_ORDER, Me.State.selectedSortOrderId)

                Me.State.searchDV = Claim.getAdjusterList(Me.State.claimNumber,
                                                          Me.State.serviceCenterName,
                                                          Me.State.authorizationNumber,
                                                          Me.State.selectedClaimStausCode,
                                                          Me.State.selectedClaimTypeId,
                                                          Me.State.selectedClaimExtendedStatusId,
                                                          Me.State.selectedClaimExtendedStatusOwnerId,
                                                          Me.State.selectedTATId,
                                                          Me.State.selectedAutoApproveDesc,
                                                          Me.State.BeginDate,
                                                          Me.State.EndDate,
                                                          Me.State.ClaimAdjuster,
                                                          Me.State.ClaimAddedBy,
                                                          SortOrderCode,
                                                          sortBy)
                If (Me.State.SearchClicked) Then
                    Me.ValidSearchResultCount(Me.State.searchDV.Count, True)
                    Me.State.SearchClicked = False
                End If
            End If

            Me.modataGrid.AutoGenerateColumns = False
            Me.modataGrid.AllowSorting = True
            'Me.State.searchDV.Sort = Me.State.SortExpression

            Me.modataGrid.DataSource = Me.State.searchDV

            HighLightSortColumn(modataGrid, Me.SortDirection)

            Me.modataGrid.DataBind()

            ControlMgr.SetVisibleControl(Me, modataGrid, Me.State.IsGridVisible)

            ControlMgr.SetVisibleControl(Me, trPageSize, Me.State.IsGridVisible)
            ControlMgr.SetVisibleControl(Me, lblRecordCount, Me.State.IsGridVisible)

            Session("recCount") = Me.State.searchDV.Count

            If Me.State.searchDV.Count > 0 Then
                BuildCheckBoxIDsArray()
                If Me.State.IsGridVisible Then
                    Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_CLAIMS_FOUND)
                End If
                Me.ApproveButton_WRITE.Visible = True
                Me.RejectButton_WRITE.Visible = True
                Me.tDDataGrid.Visible = True
                Me.tDDataGrid1.Visible = True
            Else
                If Me.modataGrid.Visible Then
                    Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_CLAIMS_FOUND)
                End If
                Me.ApproveButton_WRITE.Visible = False
                Me.RejectButton_WRITE.Visible = False
                Me.tDDataGrid.Visible = True
                'Me.tDDataGrid.Visible = False
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Public Sub PopulateSearchFieldsFromState()
        Try

            Me.txtAuthorizationNumber.Text = Me.State.authorizationNumber
            Me.txtClaimNumber.Text = Me.State.claimNumber
            If Not Me.State.selectedClaimTypeId.Equals(Guid.Empty) Then
                Me.SetSelectedItem(Me.cboClaimType, Me.State.selectedClaimTypeId)
            End If
            Me.txtServiceCenterName.Text = Me.State.serviceCenterName

            If Not Me.State.selectedClaimExtendedStatusId.Equals(Guid.Empty) Then
                Me.SetSelectedItem(Me.cboClaimExtendedStatus, Me.State.selectedClaimExtendedStatusId)
            End If

            If Not Me.State.selectedClaimStausCode.Equals(String.Empty) Then
                Me.SetSelectedItem(Me.cboClaimStatus, Me.State.selectedClaimStausCode)
            End If

            If Not Me.State.selectedClaimExtendedStatusOwnerId.Equals(Guid.Empty) Then
                Me.SetSelectedItem(Me.cboStatusOwner, Me.State.selectedClaimExtendedStatusOwnerId)
            End If

            If Not Me.State.selectedTATId.Equals(Guid.Empty) Then
                Me.SetSelectedItem(Me.cboSCTurnAroundTime, Me.State.selectedTATId)
            End If

            Me.SetSelectedItem(Me.cboSortBy, Me.State.selectedSortById)
            Me.SetSelectedItem(Me.cboSortOrder, Me.State.selectedSortOrderId)

            If Not Me.State.selectedAutoApproveId.Equals(Guid.Empty) Then
                Me.SetSelectedItem(Me.cboAutoApproved, Me.State.selectedAutoApproveId)
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Public Function PopulateStateFromSearchFields() As Boolean
        Dim dblAmount As Double

        Try
            Me.State.claimNumber = Me.txtClaimNumber.Text

            Me.State.selectedClaimTypeId = Me.GetSelectedItem(Me.cboClaimType)
            Me.State.selectedClaimStausCode = Me.GetSelectedValue(Me.cboClaimStatus)
            Me.State.selectedClaimExtendedStatusId = Me.GetSelectedItem(Me.cboClaimExtendedStatus)
            Me.State.selectedClaimExtendedStatusOwnerId = Me.GetSelectedItem(Me.cboStatusOwner)
            Me.State.selectedTATId = Me.GetSelectedItem(Me.cboSCTurnAroundTime)

            Me.State.serviceCenterName = Me.txtServiceCenterName.Text
            Me.State.authorizationNumber = Me.txtAuthorizationNumber.Text
            Me.State.selectedSortById = Me.GetSelectedItem(Me.cboSortBy)
            Me.State.selectedSortOrderId = Me.GetSelectedItem(Me.cboSortOrder)
            Me.State.selectedAutoApproveId = Me.GetSelectedItem(Me.cboAutoApproved)
            Me.State.selectedAutoApproveDesc = Me.GetSelectedDescription(Me.cboAutoApproved)
            If Me.txtExt_Sts_Date_From.Text <> String.Empty Then
                Me.State.BeginDate = DateTime.Parse(Me.txtExt_Sts_Date_From.Text).ToString(DALObjects.DALBase.DOTNET_QUERY_DATEFORMAT)
            End If
            If Me.txtExt_Sts_Date_To.Text <> String.Empty Then
                Me.State.EndDate = DateTime.Parse(Me.txtExt_Sts_Date_To.Text).ToString(DALObjects.DALBase.DOTNET_QUERY_DATEFORMAT)
            End If
            Me.State.ClaimAdjuster = Me.txtClaimAdjuster.Text
            Me.State.ClaimAddedBy = Me.txtCreatedBy.Text
            Return True

        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try

    End Function

    'This method will change the Page Index and the Selected Index
    Public Function FindDVSelectedRowIndex(ByVal dv As Claim.ClaimAdjusterSearchDV) As Integer
        Try
            If Me.State.selectedClaimId.Equals(Guid.Empty) Then
                Return -1
            Else
                'Jump to the Right Page
                Dim i As Integer
                For i = 0 To dv.Count - 1
                    If New Guid(CType(dv(i)(Claim.ClaimAdjusterSearchDV.COL_CLAIM_ID), Byte())).Equals(Me.State.selectedClaimId) Then
                        Return i
                    End If
                Next
            End If
            Return -1
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Function

    Public Function FindTATColor(ByVal TAT As Integer) As String
        If Me.State.TATDV Is Nothing Then
            Me.State.TATDV = TurnAroundTimeRange.LoadListWithColor()
        End If

        If Not Me.State.TATDV Is Nothing AndAlso Me.State.TATDV.Count > 0 Then

            Dim i As Integer
            For i = 0 To Me.State.TATDV.Count - 1
                If CType(Me.State.TATDV.Item(i)("min_days"), Integer) <= TAT AndAlso CType(Me.State.TATDV.Item(i)("max_days"), Integer) >= TAT Then
                    Try
                        Dim c As Color = System.Drawing.ColorTranslator.FromHtml(Me.State.TATDV.Item(i)("color_code").ToString)
                    Catch ex As Exception
                        Throw New GUIException(Message.MSG_INVALID_COLOR_CODE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_COLOR_CODE)
                    End Try
                    Return (Me.State.TATDV.Item(i)("color_code").ToString)
                End If
            Next

        Else
            Return Nothing
        End If

    End Function

    Function IsDataGPageDirty() As Boolean
        Dim Result As String = Me.HiddenIsPageDirty.Value
        Return Result.Equals("YES")
    End Function
    Private Sub ProcessCommand()
        'Process transaction
        Dim checkValues As String = String.Empty
        Dim i As Integer
        checkValueArray = checkRecords.Value.Split(":"c)

        For i = 0 To checkValueArray.Length - 1
            If (Not checkValueArray(i) Is Nothing And checkValueArray(i) <> "") Then
                checkValues = checkValueArray(i).ToString & ":" & checkValues
            End If
        Next
        checkRecords.Value = GetCheckedItemsValues()
        'req 5547
        comments = GetCheckedItemsComments()
        risktypes = GetCheckedItemsRiskType()
        ProcessRecords()
        checkRecords.Value = ""
        comments = String.Empty
        risktypes = String.Empty
        Me.State.searchDV = Nothing
        PopulateGrid()
    End Sub

    Private Function GetCheckedItemsValues() As String
        Dim checkedValues As String = String.Empty
        For Each gvrow As GridViewRow In Me.modataGrid.Rows
            Dim CheckBox1 As CheckBox = DirectCast(gvrow.FindControl(Me.GRID_CTRL_NAME_CHECKBOX), CheckBox)

            If CheckBox1.Checked Then
                Dim claimId As Guid = New Guid(gvrow.Cells(Me.GRID_COL_CLAIM_ID_IDX).Text)

                checkedValues += GuidControl.GuidToHexString(claimId) & ":"
            End If

        Next
        Return checkedValues
    End Function
    'req 5547
    Private Function GetCheckedItemsComments() As String
        Dim claimComments As String = String.Empty
        For Each gvrow As GridViewRow In Me.modataGrid.Rows
            Dim txtBox As TextBox = DirectCast(gvrow.FindControl(Me.GRID_CTRL_NAME_TEXTBOX), TextBox)
            Dim CheckBox1 As CheckBox = DirectCast(gvrow.FindControl(Me.GRID_CTRL_NAME_CHECKBOX), CheckBox)

            If CheckBox1.Checked Then
                Dim comment As String = txtBox.Text

                claimComments += comment & ":"
            End If

        Next
        Return claimComments

    End Function

    Private Function GetCheckedItemsRiskType() As String
        Dim checkedValues As String = String.Empty
        For Each gvrow As GridViewRow In Me.modataGrid.Rows
            Dim CheckBox1 As CheckBox = DirectCast(gvrow.FindControl(Me.GRID_CTRL_NAME_CHECKBOX), CheckBox)

            If CheckBox1.Checked Then
                Dim riskTypeId As Guid = New Guid(gvrow.Cells(Me.GRID_COL_RISK_TYPE_ID_IDX).Text)

                checkedValues += GuidControl.GuidToHexString(riskTypeId) & ":"
            End If

        Next
        Return checkedValues
    End Function

    Protected Function ProcessRecords() As Boolean
        Try
            Dim outputParameters() As DALObjects.DBHelper.DBHelperParameter
            outputParameters = Claim.ApproveOrRejectClaims(Me.State.cmdProcessRecord, checkRecords.Value, comments, risktypes)

            If CType(outputParameters(0).Value, Integer) = 0 Then
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.OK
                Me.HiddenSaveChangesPromptResponse.Value = Me.MSG_BTN_OK
                Me.DisplayMessageWithSubmit(Message.SAVE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
            ElseIf CType(outputParameters(0).Value, Integer) = 100 Then
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.OK
                Me.HiddenSaveChangesPromptResponse.Value = Me.MSG_BTN_OK
                Me.DisplayMessageWithSubmit(CType(outputParameters(1).Value, String), "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO, False)
            ElseIf CType(outputParameters(0).Value, Integer) = 300 Then
                'Throw New GUIException(Assurant.ElitaPlus.Common.ErrorCodes.DB_ERROR, Assurant.ElitaPlus.Common.ErrorCodes.DB_ERROR)
                Me.ErrControllerMaster.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.DB_ERROR)
            Else
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.OK
                Me.HiddenSaveChangesPromptResponse.Value = Me.MSG_BTN_OK
                Me.DisplayMessageWithSubmit(CType(outputParameters(1).Value, String), "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO, False)
            End If

            PopulateGrid()
            Return True
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
            Return False
        End Try
    End Function

    Public Sub ValidateDates()

        If txtExt_Sts_Date_From.Text.Trim() <> String.Empty Then
            GUIException.ValidateDate(lblExt_Sts_Date_From, txtExt_Sts_Date_From.Text)
        End If

        If txtExt_Sts_Date_To.Text.Trim() <> String.Empty Then
            GUIException.ValidateDate(lblExt_Sts_Date_To, txtExt_Sts_Date_To.Text)

            If txtExt_Sts_Date_From.Text.Trim() <> String.Empty AndAlso CDate(txtExt_Sts_Date_From.Text) > CDate(txtExt_Sts_Date_To.Text) Then
                ElitaPlusPage.SetLabelError(lblExt_Sts_Date_From)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_BEGIN_END_DATE_ERR)
            End If
        End If

        Me.lblExt_Sts_Date_From.ForeColor = Me.lblClaimStatus.ForeColor
        Me.lblExt_Sts_Date_To.ForeColor = Me.lblClaimStatus.ForeColor

        If txtExt_Sts_Date_From.Text.Trim = String.Empty Then
            Me.State.BeginDate = Nothing
        Else
            Me.State.BeginDate = Me.txtExt_Sts_Date_From.Text
        End If

        If txtExt_Sts_Date_To.Text.Trim = String.Empty Then
            Me.State.EndDate = Nothing
        Else
            Me.State.EndDate = Me.txtExt_Sts_Date_To.Text
        End If

    End Sub
#End Region


#Region " Datagrid Related "

    Private Sub moDataGrid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles modataGrid.PageIndexChanging
        Try
            Me.State.selectedPageIndex = e.NewPageIndex
            If IsDataGPageDirty() Then
                Me.ApproveButton_WRITE.Enabled = True
                Me.RejectButton_WRITE.Enabled = True
                DisplayMessage(Message.MSG_PAGE_ALERT_PROMPT, "", MSG_BTN_OK, MSG_TYPE_ALERT)
            Else
                Me.modataGrid.PageIndex = e.NewPageIndex
                PopulateGrid()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub moDataGrid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            If IsDataGPageDirty() Then
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.GridPageSize
                Me.ApproveButton_WRITE.Enabled = True
                Me.RejectButton_WRITE.Enabled = True
                DisplayMessage(Message.MSG_PAGE_ALERT_PROMPT, "", MSG_BTN_OK, MSG_TYPE_ALERT)
            Else
                modataGrid.PageIndex = NewCurrentPageIndex(modataGrid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                Me.State.selectedPageSize = CType(cboPageSize.SelectedValue, Integer)
                Me.PopulateGrid()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Protected Sub ItemBound(ByVal source As Object, ByVal e As GridViewRowEventArgs) Handles modataGrid.RowDataBound
        Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
        Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

        'For Each myCell As TableCell In e.Row.Cells
        '    myCell.Style.Add("word-break", "break-all")
        '    myCell.Width = 140
        'Next

        If itemType = ListItemType.Header Then
            e.Row.Cells(GRID_COL_CLAIM_TAT_IDX).Attributes.Add("style", "word-break:break-all;word-wrap:break-word")
            e.Row.Cells(GRID_COL_SVC_TAT_IDX).Attributes.Add("style", "word-break:break-all;word-wrap:break-word")
            'e.Row.Cells(GRID_COL_CLAIM_NUMBER_IDX).Text = Server.HtmlDecode(e.Row.Cells(GRID_COL_CLAIM_NUMBER_IDX).Text)

            Dim str As String

            str = Server.HtmlDecode(e.Row.Cells(5).Text)
            'e.Row.Cells(5).Text = Server.HtmlDecode(e.Row.Cells(5).Text)

        End If

        Try
            If Not dvRow Is Nothing Then


                If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then

                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_CLAIM_ID_IDX), dvRow(Claim.ClaimAdjusterSearchDV.COL_CLAIM_ID))
                    If Not dvRow(Claim.ClaimAdjusterSearchDV.COL_CLAIM_TAT) Is System.DBNull.Value Then
                        Dim claim_TAT As Integer = CType(dvRow(Claim.ClaimAdjusterSearchDV.COL_CLAIM_TAT), Integer)
                        Dim claim_TAT_colorCode As String = FindTATColor(claim_TAT)
                        If Not claim_TAT_colorCode Is Nothing AndAlso Not claim_TAT_colorCode.Equals(String.Empty) Then e.Row.Cells(Me.GRID_COL_CLAIM_TAT_IDX).BackColor = System.Drawing.ColorTranslator.FromHtml(claim_TAT_colorCode)
                    End If

                    If Not dvRow(Claim.ClaimAdjusterSearchDV.COL_SVC_TAT) Is System.DBNull.Value Then
                        Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_SVC_TAT_IDX), dvRow(Claim.ClaimAdjusterSearchDV.COL_SVC_TAT).ToString)
                        Dim SVC_TAT As Integer = CType(dvRow(Claim.ClaimAdjusterSearchDV.COL_SVC_TAT), Integer)
                        Dim SVC_TAT_colorCode As String = FindTATColor(SVC_TAT)
                        If Not SVC_TAT_colorCode Is Nothing AndAlso Not SVC_TAT_colorCode.Equals(String.Empty) Then e.Row.Cells(Me.GRID_COL_SVC_TAT_IDX).BackColor = System.Drawing.ColorTranslator.FromHtml(SVC_TAT_colorCode)
                    Else
                        e.Row.Cells(Me.GRID_COL_SVC_TAT_IDX).Text = "N/A"
                    End If

                    'Checkbox logic
                    Dim transIdStr As String = String.Empty
                    Dim checkBox As CheckBox = New CheckBox
                    checkBox = CType(e.Row.Cells(Me.GRID_COL_CHK_BOX_IDX).FindControl(Me.GRID_CTRL_NAME_CHECKBOX), CheckBox)
                    checkBox.Attributes.Add("onclick", "CheckboxAction('" & transIdStr & "','" & checkBox.ClientID & "','" & Me.ApproveButton_WRITE.ClientID & "', '" & Me.RejectButton_WRITE.ClientID & "','" & checkRecords.ClientID & "') ; ChangeHeaderAsNeeded();")

                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_CLAIM_TAT_IDX), dvRow(Claim.ClaimAdjusterSearchDV.COL_CLAIM_TAT).ToString)

                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_CLAIM_EXT_STATUS_IDX), dvRow(Claim.ClaimAdjusterSearchDV.COL_CLAIM_EXTENDED_STATUS))
                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_STATUS_IDX), dvRow(Claim.ClaimAdjusterSearchDV.COL_CLAIM_STATUS))
                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_CLAIM_ADJUSTER_IDX), dvRow(Claim.ClaimAdjusterSearchDV.COL_CLAIM_ADJUSTER))
                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_ADDED_BY_IDX), dvRow(Claim.ClaimAdjusterSearchDV.COL_ADDED_BY))
                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_AUTO_APPROVED_IDX), dvRow(Claim.ClaimAdjusterSearchDV.COL_AUTO_APPROVED))

                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_STATUS_OWNER_IDX), dvRow(Claim.ClaimAdjusterSearchDV.COL_CLAIM_EXTENDED_STATUS_OWNER))
                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_CLAIM_NUMBER_IDX), dvRow(Claim.ClaimAdjusterSearchDV.COL_CLAIM_NUMBER))
                    'CType(e.Row.Cells(Me.GRID_COL_CLAIM_NUMBER_IDX).FindControl("Claim_NumberItemLabel"), Label).Text = dvRow(Claim.ClaimAdjusterSearchDV.COL_CLAIM_NUMBER).ToString
                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_SERVICE_CENTER_IDX), dvRow(Claim.ClaimAdjusterSearchDV.COL_SERVICE_CENTER_NAME))
                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_CLAIM_TYPE_IDX), dvRow(Claim.ClaimAdjusterSearchDV.COL_CLAIM_TYPE_DESCRIPTION))
                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_AUTHORIZATION_NUMBER_IDX), dvRow(Claim.ClaimAdjusterSearchDV.COL_AUTHORIZATION_NUMBER))
                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_PRODUCT_IDX), dvRow(Claim.ClaimAdjusterSearchDV.COL_PRODUCT_DESCRIPTION))

                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_SALES_PRICE_IDX), dvRow(Claim.ClaimAdjusterSearchDV.COL_SALES_PRICE))
                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_COVERAGE_TYPE_IDX), dvRow(Claim.ClaimAdjusterSearchDV.COL_COVERAGE_TYPE))
                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_END_DATE_IDX), dvRow(Claim.ClaimAdjusterSearchDV.COL_EXPIRATION_DATE))
                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_PAYMENT_AMOUNT_IDX), dvRow(Claim.ClaimAdjusterSearchDV.COL_PAYMENT_AMOUNT))

                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_AUTH_AMOUNT_IDX), dvRow(Claim.ClaimAdjusterSearchDV.COL_AUTHORIZED_AMOUNT))
                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_PROPOSED_AMOUNT_IDX), dvRow(Claim.ClaimAdjusterSearchDV.COL_PROPOSED_AMOUNT))
                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_LABOR_AMOUNT_IDX), dvRow(Claim.ClaimAdjusterSearchDV.COL_LABOR_AMOUNT))
                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_PARTS_AMOUNT_IDX), dvRow(Claim.ClaimAdjusterSearchDV.COL_PART_AMOUNT))
                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_SVC_CHARGE_AMOUNT_IDX), dvRow(Claim.ClaimAdjusterSearchDV.COL_SERVICE_CHARGE))
                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_RISK_TYPE_ID_IDX), dvRow(Claim.ClaimAdjusterSearchDV.COL_RISK_TYPE_ID))

                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_INBOUND_TRACKING_NUMBER_IDX), dvRow(Claim.ClaimAdjusterSearchDV.COL_INBOUND_TRACKING_NUMBER))
                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_OUTBOUND_TRACKING_NUMBER_IDX), dvRow(Claim.ClaimAdjusterSearchDV.COL_OUTBOUND_TRACKING_NUMBER))
                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_REPLACEMENT_DEVICE_IDX), dvRow(Claim.ClaimAdjusterSearchDV.COL_REPLACEMENT_DEVICE))
                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_REPLACEMENT_DEVICE_COMMENTS_IDX), dvRow(Claim.ClaimAdjusterSearchDV.COL_REPLACEMENT_DEVICE_COMMENTS))
                    

                    'Checkbox should be grayed out (unable to be selected) if:
                    'Proposed Amount sent by the Service Center is greater than the current claim’s ‘Authorized Amount’
                    'Claim Status is different than ‘A’
                    Dim curAuthAmount As Decimal
                    Dim curPrposedAmount As Decimal
                    Dim cb As CheckBox = CType(e.Row.Cells(Me.GRID_COL_CHK_BOX_IDX).FindControl("btnSelected"), CheckBox)
                    If Not dvRow(Claim.ClaimAdjusterSearchDV.COL_AUTHORIZED_AMOUNT) Is System.DBNull.Value Then
                        curAuthAmount = CType(dvRow(Claim.ClaimAdjusterSearchDV.COL_AUTHORIZED_AMOUNT), Decimal)
                    End If
                    'If Not dvRow(Claim.ClaimAdjusterSearchDV.COL_PROPOSED_AMOUNT) Is System.DBNull.Value Then
                    '    curPrposedAmount = CType(dvRow(Claim.ClaimAdjusterSearchDV.COL_PROPOSED_AMOUNT), Decimal)
                    '    If curPrposedAmount > curAuthAmount Then
                    '        cb.Enabled = False
                    '    End If
                    'End If

                    'Request 5547
                    If Not dvRow(Claim.ClaimAdjusterSearchDV.COL_PROPOSED_AMOUNT) Is System.DBNull.Value Then
                        curPrposedAmount = CType(dvRow(Claim.ClaimAdjusterSearchDV.COL_PROPOSED_AMOUNT), Decimal)
                        If curPrposedAmount > curAuthAmount Then
                            Dim objCompanyGroup As CompanyGroup = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup
                            If (GetFastApprovalNoID() = objCompanyGroup.ClaimFastApprovalId) Then
                                cb.Enabled = False

                            End If

                        End If
                    End If

                    If Not dvRow(Claim.ClaimAdjusterSearchDV.COL_CLAIM_STATUS).ToString.Equals("A") Then
                        cb.Enabled = False
                    End If

                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_TRIP_AMOUNT_IDX), dvRow(Claim.ClaimAdjusterSearchDV.COL_TRIP_AMOUNT))
                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_OTHER_AMOUNT_IDX), dvRow(Claim.ClaimAdjusterSearchDV.COL_OTHER_AMOUNT))
                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_SHIPPING_AMOUNT_IDX), dvRow(Claim.ClaimAdjusterSearchDV.COL_SHIPPING_AMOUNT))

                End If
            End If
            BaseItemBound(source, e)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try

    End Sub

    Private Sub Grid_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles modataGrid.Sorting
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

    'Private Sub SortAndBindGrid()
    '    Dim dv As New DataView
    '    Me.State.PageIndex = Me.modataGrid.PageIndex

    '    If Not (Me.State.searchDV.Count = 0) Then
    '        Me.modataGrid.Enabled = True
    '        Me.modataGrid.DataSource = Me.State.searchDV
    '        HighLightSortColumn(modataGrid, Me.SortDirection)
    '        Me.modataGrid.DataBind()
    '    End If

    '    If Not modataGrid.BottomPagerRow.Visible Then modataGrid.BottomPagerRow.Visible = True

    '    ControlMgr.SetVisibleControl(Me, modataGrid, Me.State.IsGridVisible)

    '    ControlMgr.SetVisibleControl(Me, trPageSize, Me.modataGrid.Visible)

    '    Session("recCount") = Me.State.searchDV.Count

    '    'If Me.modataGrid.Visible Then
    '    '    If (Me.State.AddingNewRow) Then
    '    '        Me.lblRecordCount.Text = (Me.State.searchDV.Count - 1) & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
    '    '    Else
    '    '        Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
    '    '    End If
    '    'End If
    '    ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, modataGrid)
    'End Sub

    Protected Overloads Sub ItemCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Public Sub ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles modataGrid.RowCommand
        Try

            If IsDataGPageDirty() Then
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.GridPageSize
                Me.ApproveButton_WRITE.Enabled = True
                Me.RejectButton_WRITE.Enabled = True
                DisplayMessage(Message.MSG_PAGE_ALERT_PROMPT, "", MSG_BTN_OK, MSG_TYPE_ALERT)
            ElseIf (e.CommandName = Me.EDIT_COMMAND) Then
                Dim index As Integer = -1
                If e.CommandName = "EditRecord" Then
                    index = CInt(e.CommandArgument)
                End If

                If Me.State Is Nothing Then
                    Me.Trace(Me, "Restoring State")
                    Me.RestoreState(New MyState)
                End If
                Me.State.selectedClaimId = New Guid(Me.modataGrid.Rows(index).Cells(Me.GRID_COL_CLAIM_ID_IDX).Text)

                Me.callPage(ClaimForm.URL, Me.State.selectedClaimId)

            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try

    End Sub

#End Region

#Region " Button Clicks "


    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            'Me.PopulateSearchFieldsFromState()
            Me.State.SearchClicked = True
            Me.State.PageIndex = 0
            Me.State.selectedClaimId = Guid.Empty
            Me.State.IsGridVisible = True
            Me.State.searchDV = Nothing
            Me.State.selectedSortById = New Guid(Me.cboSortBy.SelectedValue)
            Me.ValidateDates()
            Me.PopulateGrid()
            checkRecords.Value = ""
            Me.tDDataGrid1.Visible = True
            Me.tDDataGrid.Visible = True
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub ApproveButton_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ApproveButton_WRITE.Click
        Try

            Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Accept
            Me.State.cmdProcessRecord = DALObjects.ClaimDAL.CMD_APPROVE
            Me.ProcessCommand()

        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Private Sub RejectButton_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RejectButton_WRITE.Click
        Try
            'Resend confirmation
            Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Accept
            Me.State.cmdProcessRecord = DALObjects.ClaimDAL.CMD_REJECT
            Me.ProcessCommand()
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Private Sub btnClearSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearSearch.Click
        Try
            Me.txtClaimNumber.Text = String.Empty
            Me.txtAuthorizationNumber.Text = String.Empty
            Me.cboClaimStatus.SelectedIndex = 0
            Me.txtServiceCenterName.Text = String.Empty
            Me.cboClaimType.SelectedIndex = 0
            Me.cboClaimExtendedStatus.SelectedIndex = 0
            Me.cboStatusOwner.SelectedIndex = 0
            Me.cboSCTurnAroundTime.SelectedIndex = 0
            cboAutoApproved.SelectedIndex = 0
            txtExt_Sts_Date_From.Text = String.Empty
            txtExt_Sts_Date_To.Text = String.Empty
            txtClaimAdjuster.Text = String.Empty
            txtCreatedBy.Text = String.Empty
            Me.SetSelectedItem(Me.cboSortBy, Me.State.selectedSortById)
            Me.SetSelectedItem(Me.cboSortOrder, Me.State.selectedSortOrderId)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

#End Region

    Private Sub modataGrid_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles modataGrid.RowCreated

        Try
              If e.Row.RowType = DataControlRowType.DataRow Then
                Dim pce1 As PopupControlExtender = CType(e.Row.FindControl("PopupControlExtender1"), PopupControlExtender)
                Dim pce2 As PopupControlExtender = CType(e.Row.FindControl("PopupControlExtender2"), PopupControlExtender)
                Dim pce3 As PopupControlExtender = CType(e.Row.FindControl("PopupControlExtender3"), PopupControlExtender)

                Dim behaviorID1 As String = "pce1_" + e.Row.RowIndex.ToString
                pce1.BehaviorID = behaviorID1
                Dim behaviorID2 As String = "pce2_" + e.Row.RowIndex.ToString
                pce2.BehaviorID = behaviorID2
                Dim behaviorID3 As String = "pce3_" + e.Row.RowIndex.ToString
                pce3.BehaviorID = behaviorID3

                Dim img1 As System.Web.UI.WebControls.Image = CType(e.Row.FindControl("Image1"), System.Web.UI.WebControls.Image)
                Dim img2 As System.Web.UI.WebControls.Image = CType(e.Row.FindControl("Image2"), System.Web.UI.WebControls.Image)
                Dim img3 As System.Web.UI.WebControls.Image = CType(e.Row.FindControl("Image3"), System.Web.UI.WebControls.Image)

                Dim OnMouseOverScript1 As String = String.Format("$find('{0}').showPopup();", behaviorID1)
                Dim OnMouseOutScript1 As String = String.Format("$find('{0}').hidePopup();", behaviorID1)

                Dim OnMouseOverScript2 As String = String.Format("$find('{0}').showPopup();", behaviorID2)
                Dim OnMouseOutScript2 As String = String.Format("$find('{0}').hidePopup();", behaviorID2)

                Dim OnMouseOverScript3 As String = String.Format("$find('{0}').showPopup();", behaviorID3)
                Dim OnMouseOutScript3 As String = String.Format("$find('{0}').hidePopup();", behaviorID3)

                img1.Attributes.Add("onmouseover", OnMouseOverScript1)
                img1.Attributes.Add("onmouseout", OnMouseOutScript1)

                img2.Attributes.Add("onmouseover", OnMouseOverScript2)
                img2.Attributes.Add("onmouseout", OnMouseOutScript2)

                img3.Attributes.Add("onmouseover", OnMouseOverScript3)
                img3.Attributes.Add("onmouseout", OnMouseOutScript3)

              End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
      

    End Sub


    <System.Web.Script.Services.ScriptMethod()> _
    <System.Web.Services.WebMethod()>
    Public Shared Function GetProblemDescription(ByVal contextKey As String) As String

        Dim description As String = Claim.GetProblemDescription(contextKey)

        Dim b As StringBuilder = New StringBuilder()

        b.Append("<table style='background-color:#f3f3f3; border: #336699 3px solid; ")
        b.Append("width:350px; font-size:10pt; font-family:Verdana;' cellspacing='0' cellpadding='3'>")

        b.Append("<tr><td colspan='3' style='background-color:#336699; color:white;'>")
        b.Append("<b>Problem Description</b>")
        b.Append("</td></tr>")
        b.Append("<tr><td style='width:330px;'><b>")
        b.Append(description)
        b.Append("</b></td></tr>")
        b.Append("</table>")

        Return b.ToString()

    End Function

    <System.Web.Script.Services.ScriptMethod()> _
    <System.Web.Services.WebMethod()>
    Public Shared Function GetTechnicalReport(ByVal contextKey As String) As String

        Dim description As String = Claim.GetTechnicalReport(contextKey)

        Dim b As StringBuilder = New StringBuilder()

        b.Append("<table style='background-color:#f3f3f3; border: #336699 3px solid; ")
        b.Append("width:350px; font-size:10pt; font-family:Verdana;' cellspacing='0' cellpadding='3'>")

        b.Append("<tr><td colspan='3' style='background-color:#336699; color:white;'>")
        b.Append("<b>Technical Report</b>")
        b.Append("</td></tr>")
        b.Append("<tr><td style='width:330px;'><b>")
        b.Append(description)
        b.Append("</b></td></tr>")
        b.Append("</table>")

        Return b.ToString()

    End Function

    <System.Web.Script.Services.ScriptMethod()> _
    <System.Web.Services.WebMethod()>
    Public Shared Function GetExtendedStatusComment(ByVal contextKey As String) As String

        Dim description As String = Claim.GetExtendedStatusComment(contextKey)

        Dim b As StringBuilder = New StringBuilder()

        b.Append("<table style='background-color:#f3f3f3; border: #336699 3px solid; ")
        b.Append("width:350px; font-size:10pt; font-family:Verdana;' cellspacing='0' cellpadding='3'>")

        b.Append("<tr><td colspan='3' style='background-color:#336699; color:white;'>")
        b.Append("<b>Extended Status Comment</b>")
        b.Append("</td></tr>")
        b.Append("<tr><td style='width:330px;'><b>")
        b.Append(description)
        b.Append("</b></td></tr>")
        b.Append("</table>")

        Return b.ToString()

    End Function



End Class



