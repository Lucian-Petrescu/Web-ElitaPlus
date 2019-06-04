Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common

Partial Public Class DirectBillingForm
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
    Private Const GRID_CONTROL_NAME_BILLING_IDX As String = "moBillingHeaderId"

    Public Const GRID_COL_EDIT_IDX As Integer = 0
    Public Const GRID_COL_BILLING_IDX As Integer = 1
    Public Const GRID_COL_DEALER_CODE_IDX As Integer = 2
    Public Const GRID_COL_DATE_FILE_SENT_IDX As Integer = 3
    Public Const GRID_COL_FILENAME_IDX As Integer = 4
    Public Const GRID_COL_REFERENCE_NUMBER_IDX As Integer = 5
    Public Const GRID_COL_TOTAL_BILLED_AMT_IDX As Integer = 5
    Public Const GRID_VSC_COL_TOTAL_BILLED_AMT_IDX As Integer = 6

    Public Const GRID_TOTAL_COLUMNS As Integer = 7
    Private Const NOTHING_SELECTED As String = "00000000-0000-0000-0000-000000000000"
    Private Const LABEL_SELECT_DEALERCODE As String = "DEALER"
    Private Const LABEL_SELECT_COMPANY As String = "COMPANY"

    Private Const GRID_NO_SELECTEDITEM_INX As Integer = -1
    Private Const COL_DEALER_CODE As String = "Dealer"
    Private Const COL_DATE_FILE_SENT As String = "Date_File_Sent"
    Private Const COL_FILE_NAME As String = "FileName"
    Private Const COL_TOTAL_BILLED_AMT As String = "total_billed_amt"
    Private Const COL_REFERENCE_NUMBER As String = "reference_number"
    Private Const GRID_CTRL_EDIT_BUTTON_IMAGE_ID As String = "imgbtnEdit"
    Private Const COL_BILLING_HEADER_SOURCE As String = "source"
    Private Const BILLING_HEADER_REJECT_SOURCE As String = "BHR"

#End Region

#Region "Properties"

    Public ReadOnly Property TheDealerControl() As MultipleColumnDDLabelControl
        Get
            If multipleDropControl Is Nothing Then
                multipleDropControl = CType(FindControl("multipleDropControl"), MultipleColumnDDLabelControl)
            End If
            Return multipleDropControl
        End Get
    End Property

    Public ReadOnly Property TheCompanyControl() As MultipleColumnDDLabelControl
        Get
            If companyMultipleDropControl Is Nothing Then
                companyMultipleDropControl = CType(FindControl("CompanyMultipleDropControl"), MultipleColumnDDLabelControl)
            End If
            Return companyMultipleDropControl
        End Get
    End Property

#End Region

#Region "Page State"
    Private IsReturningFromChild As Boolean = False

    ' This class keeps the current state for the search page.
    Class MyState
        Public BillingHeaderID As Guid = Guid.Empty
        Public PageIndex As Integer = 0
        Public IsGridVisible As Boolean = False
        Private mnPageSize As Integer = DEFAULT_PAGE_SIZE
        Public searchDV As DataView = Nothing
        Public errLabel As String
        Public SearchedComanyID As Guid
        Public SelectedComanyBillingFileByDealer As Boolean = False
        Public SearchedDealerID As Guid
        Public SearchedBeginDate As Date
        Public SearchedEndDate As Date
        Public selectedPageSize As Integer = DEFAULT_PAGE_SIZE
        'Public BillingFileByDealer As Boolean = False
        Public BillingFileByDealerForAllUserCompanies As Boolean = False
        Public NoBillingFileByDealerForAllUserCompanies As Boolean = False
        Public MixedBillingFileByDealerForUserCompanies As Boolean = False
        Public companyDV As DataView
        Public alCompanies As ArrayList
        Public SortExpression As String = " dealer, date_file_sent desc"
        Public SortExpression_ByCompany As String = "date_file_sent desc"

        Public Property PageSize() As Integer
            Get
                Return mnPageSize
            End Get
            Set(ByVal Value As Integer)
                mnPageSize = Value
            End Set
        End Property
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

#Region "Page_Return"
    Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
        Me.MenuEnabled = True
        Me.IsReturningFromChild = True
    End Sub
#End Region

#Region "Page_Events"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Me.ErrController.Clear_Hide()

        Try
            If Not Me.IsPostBack Then
                PopulateBillingByDealerFlag()
                Me.AddCalendar(Me.btnBeginDate, Me.txtBeginDate)
                Me.AddCalendar(Me.btnEndDate, Me.txtEndDate)
                ControlMgr.SetVisibleControl(Me, trPageSize, False)
                If Me.IsReturningFromChild And Me.State.IsGridVisible Then
                    cboPageSize.SelectedValue = CType(Me.State.PageSize, String)
                    If Me.State.BillingFileByDealerForAllUserCompanies Or Me.State.SelectedComanyBillingFileByDealer Then
                        moBillingGrid.PageSize = Me.State.PageSize
                    Else
                        moVSCBillingGrid.PageSize = Me.State.PageSize
                    End If
                    If Not Me.State.SearchedComanyID.Equals(Guid.Empty) Then
                        TheCompanyControl.SelectedGuid = Me.State.SearchedComanyID
                    End If
                    If Not Me.State.SearchedDealerID.Equals(Guid.Empty) Then
                        TheDealerControl.SelectedGuid = Me.State.SearchedDealerID
                        TheDealerControl.ChangeEnabledControlProperty(True)
                    End If
                    Me.PopulateGrid()
                End If
            Else
                If Me.State.errLabel <> "" Then
                    Me.ClearLabelErrSign(CType(Me.FindControl(Me.State.errLabel), Label))
                    Me.State.errLabel = ""
                End If
            End If

            If Me.IsReturningFromChild = True Then
                Me.IsReturningFromChild = False
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try
        Me.ShowMissingTranslations(Me.ErrController)
    End Sub

#End Region

#Region "Button Handlers"
    Private Sub mobtnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mobtnSearch.Click
        Try
            Me.State.PageIndex = 0
            Me.State.IsGridVisible = True
            Me.State.BillingHeaderID = Guid.Empty
            Me.State.searchDV = Nothing
            If Me.State.BillingFileByDealerForAllUserCompanies Or Me.State.MixedBillingFileByDealerForUserCompanies Then
                Me.State.SearchedComanyID = TheCompanyControl.SelectedGuid
                If Me.State.alCompanies.Count > 1 AndAlso Me.State.SearchedComanyID.Equals(Guid.Empty) Then
                    ElitaPlusPage.SetLabelError(TheCompanyControl.CaptionLabel)
                    Throw New GUIException(Message.MSG_INVALID_DEALER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_COMPANY_REQUIRED)
                End If
            End If

            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try

    End Sub

    Private Sub mobtnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mobtnClear.Click
        TheDealerControl.SelectedIndex = 0
        Me.txtBeginDate.Text = ""
        Me.txtEndDate.Text = ""
    End Sub
#End Region

#Region "Grid Handler"
    Public Sub ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs)
        Dim lblControl As Label
        Try
            Dim blnBillingFileByDealer As Boolean
            If e.CommandName = "SelectAction" Then
                If Me.State.BillingFileByDealerForAllUserCompanies Or Me.State.SelectedComanyBillingFileByDealer Then
                    lblControl = CType(Me.moBillingGrid.Items(e.Item.ItemIndex).Cells(Me.GRID_COL_BILLING_IDX).FindControl(Me.GRID_CONTROL_NAME_BILLING_IDX), Label)
                    blnBillingFileByDealer = True
                Else
                    lblControl = CType(Me.moVSCBillingGrid.Items(e.Item.ItemIndex).Cells(Me.GRID_COL_BILLING_IDX).FindControl(Me.GRID_CONTROL_NAME_BILLING_IDX), Label)
                    blnBillingFileByDealer = False
                End If

                Me.State.BillingHeaderID = New Guid(lblControl.Text)
                Me.callPage(DirectBillingDetailForm.URL, New DirectBillingDetailForm.Parameters(Me.State.BillingHeaderID, blnBillingFileByDealer, Me.State.SearchedDealerID))
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try
    End Sub

    Public Sub ItemCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        BaseItemCreated(sender, e)
    End Sub

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

    Private Sub moVSCBillingGrid_SortCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles moVSCBillingGrid.SortCommand
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

    Private Sub moBillingGrid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles moBillingGrid.PageIndexChanged
        Try
            Me.State.PageIndex = e.NewPageIndex
            Me.State.BillingHeaderID = Guid.Empty
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try
    End Sub

    Private Sub moVSCBillingGrid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles moVSCBillingGrid.PageIndexChanged
        Try
            Me.State.PageIndex = e.NewPageIndex
            Me.State.BillingHeaderID = Guid.Empty
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try
    End Sub

    Protected Sub cboPageSize_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            State.PageSize = CType(cboPageSize.SelectedValue, Integer)
            If Me.State.BillingFileByDealerForAllUserCompanies Or Me.State.SelectedComanyBillingFileByDealer Then
                Me.State.PageIndex = NewCurrentPageIndex(moBillingGrid, State.searchDV.Count, State.PageSize)
                Me.moBillingGrid.CurrentPageIndex = Me.State.PageIndex
            Else
                Me.State.PageIndex = NewCurrentPageIndex(moVSCBillingGrid, State.searchDV.Count, State.PageSize)
                Me.moVSCBillingGrid.CurrentPageIndex = Me.State.PageIndex
            End If

            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try
    End Sub
    Private Sub Grid_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles moVSCBillingGrid.ItemDataBound
        Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
        Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)
        Try
            If Not dvRow Is Nothing And Me.State.searchDV.Count > 0 Then
                If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                    If dvRow(BillingHeader.BillingSearchDV.COL_SOURCE).ToString = Me.BILLING_HEADER_REJECT_SOURCE Then
                        CType(e.Item.Cells(Me.GRID_COL_EDIT_IDX).FindControl(Me.GRID_CTRL_EDIT_BUTTON_IMAGE_ID), ImageButton).Visible = False
                    End If

                End If
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

#End Region

#Region "Populate"
    Private Sub PopulateDealer(ByVal objCompanyIds As ArrayList)
        Try
            Dim oDealerview As DataView = LookupListNew.GetDealerLookupList(objCompanyIds)
            TheDealerControl.SetControl(False,
                                        TheDealerControl.MODES.NEW_MODE,
                                        True,
                                        oDealerview,
                                        " &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_DEALERCODE),
                                        True, True,
                                        ,
                                        "multipleDropControl_moMultipleColumnDrop",
                                        "multipleDropControl_moMultipleColumnDropDesc",
                                        "multipleDropControl_lb_DropDown",
                                        False,
                                        0)

        Catch ex As Exception
            ErrController.AddError(ex.Message, False)
            ErrController.Show()
        End Try
    End Sub

    Private Sub PopulateCompanyDropDown()

        TheCompanyControl.NothingSelected = True

        If Me.State.companyDV Is Nothing Then
            Me.State.companyDV = LookupListNew.GetUserCompaniesLookupList()
        End If

        TheCompanyControl.SetControl(True,
                            TheCompanyControl.MODES.NEW_MODE,
                            True,
                            Me.State.companyDV,
                            " " + TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_COMPANY),
                            True, True,
                            ,
                            "multipleDropControl_moMultipleColumnDrop",
                            "multipleDropControl_moMultipleColumnDropDesc",
                            "multipleDropControl_lb_DropDown",
                             False,
                            0)

        'If ElitaPlusIdentity.Current.ActiveUser.Companies.Count > 1 AndAlso Me.State.MyBO.IsNew Then
        '    TheCompanyControl.SetControl(True,
        '                                 TheCompanyControl.MODES.NEW_MODE,
        '                                 False,
        '                                 Me.State.companyDV,
        '                                 "* " & TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_COMPANY),
        '                                 True,
        '                                 False)
        'Else
        '    CompanyMultipleDrop.SetControl(True, CompanyMultipleDrop.MODES.EDIT_MODE, False, Me.State.companyDV, "", True, False)
        'End If

    End Sub

    Private Sub PopulateBillingByDealerFlag()
        Try
            Dim index As Integer
            Dim indexByDelaer As Integer = 0
            Dim indexByCompany As Integer = 0
            Me.State.alCompanies = ElitaPlusIdentity.Current.ActiveUser.Companies
            For index = 0 To ElitaPlusIdentity.Current.ActiveUser.Companies.Count - 1
                Dim objCompany As Company = New Company(CType(Me.State.alCompanies(index), Guid))
                If LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, objCompany.BillingByDealerId) = Codes.YESNO_N Then
                    'Me.State.BillingFileByDealer = False
                    indexByCompany = indexByCompany + 1
                Else
                    indexByDelaer = indexByDelaer + 1
                End If
            Next

            If indexByCompany = Me.State.alCompanies.Count Then
                Me.State.NoBillingFileByDealerForAllUserCompanies = True
            ElseIf indexByDelaer = Me.State.alCompanies.Count Then
                Me.State.BillingFileByDealerForAllUserCompanies = True
            Else
                Me.State.MixedBillingFileByDealerForUserCompanies = True
            End If


            If Me.State.companyDV Is Nothing Then
                Me.State.companyDV = LookupListNew.GetUserCompaniesLookupList()
            End If

            If Me.State.NoBillingFileByDealerForAllUserCompanies Then 'Billing Header file by company
                Me.moCompanyInformation.Attributes("style") = "display: none"
                TheDealerControl.ChangeEnabledControlProperty(False)
                PopulateDealer(Me.State.alCompanies)
            Else 'Billing Header file by dealer or mix
                If Me.State.alCompanies.Count > 1 Then
                    TheCompanyControl.SetControl(True, TheCompanyControl.MODES.NEW_MODE, True, Me.State.companyDV, "* " & TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_COMPANY), True, False)
                    TheDealerControl.ChangeEnabledControlProperty(False)
                    PopulateDealer(Me.State.alCompanies)
                    TheCompanyControl.Visible = True
                    TheCompanyControl.ChangeEnabledControlProperty(True)
                ElseIf Me.State.alCompanies.Count = 1 Then
                    Me.moCompanyInformation.Attributes("style") = "display: none"
                    PopulateDealer(Me.State.alCompanies)
                End If

            End If


        Catch ex As Exception
            ErrController.AddError(ex.Message, False)
            ErrController.Show()
        End Try
    End Sub


    Private Sub PopulateGrid(Optional ByVal refreshData As Boolean = False)

        If Me.State.searchDV Is Nothing Then
            GetBillingData()
        End If


        If Me.State.BillingFileByDealerForAllUserCompanies Or Me.State.SelectedComanyBillingFileByDealer Then  'Company dropdown is hidden, no company selection
            ' hide the VSC grid
            Me.moVSCBillingInformation.Attributes("style") = "display: none"
            ' Show the ESC grid
            Me.moESCBillingInformation.Attributes("style") = ""
            Me.moBillingGrid.AutoGenerateColumns = False
            Me.moBillingGrid.AllowSorting = True
            Me.State.searchDV.Sort = Me.State.SortExpression
            Me.moBillingGrid.Columns(Me.GRID_COL_DEALER_CODE_IDX).SortExpression = COL_DEALER_CODE
            Me.moBillingGrid.Columns(Me.GRID_COL_DATE_FILE_SENT_IDX).SortExpression = COL_DATE_FILE_SENT
            Me.moBillingGrid.Columns(Me.GRID_COL_FILENAME_IDX).SortExpression = COL_FILE_NAME
            Me.moBillingGrid.Columns(Me.GRID_COL_TOTAL_BILLED_AMT_IDX).SortExpression = COL_TOTAL_BILLED_AMT

            SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.BillingHeaderID, Me.moBillingGrid, Me.State.PageIndex, (moBillingGrid.EditItemIndex > GRID_NO_SELECTEDITEM_INX))

            Me.State.PageIndex = Me.moBillingGrid.CurrentPageIndex
            Me.moBillingGrid.DataSource = Me.State.searchDV
            HighLightSortColumn(moBillingGrid, Me.State.SortExpression)
            Me.moBillingGrid.DataBind()
            ControlMgr.SetVisibleControl(Me, moBillingGrid, Me.State.IsGridVisible)
            ControlMgr.SetVisibleControl(Me, trPageSize, Me.moBillingGrid.Visible)

            If Me.moBillingGrid.Visible Then
                Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If

        Else
            ' hide the ESC grid
            Me.moESCBillingInformation.Attributes("style") = "display: none"
            ' Show the VSC grid
            Me.moVSCBillingInformation.Attributes("style") = ""

            Me.moVSCBillingGrid.AutoGenerateColumns = False
            Me.moVSCBillingGrid.AllowSorting = True
            Me.State.searchDV.Sort = Me.State.SortExpression_ByCompany
            'Me.moVSCBillingGrid.Columns(Me.GRID_COL_DEALER_CODE_IDX).SortExpression = COL_DEALER_CODE
            Me.moVSCBillingGrid.Columns(Me.GRID_COL_DATE_FILE_SENT_IDX).SortExpression = COL_DATE_FILE_SENT
            Me.moVSCBillingGrid.Columns(Me.GRID_COL_FILENAME_IDX).SortExpression = COL_FILE_NAME
            Me.moVSCBillingGrid.Columns(Me.GRID_COL_REFERENCE_NUMBER_IDX).SortExpression = COL_REFERENCE_NUMBER
            Me.moVSCBillingGrid.Columns(Me.GRID_VSC_COL_TOTAL_BILLED_AMT_IDX).SortExpression = COL_TOTAL_BILLED_AMT

            SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.BillingHeaderID, Me.moVSCBillingGrid, Me.State.PageIndex, (moVSCBillingGrid.EditItemIndex > GRID_NO_SELECTEDITEM_INX))

            Me.State.PageIndex = Me.moVSCBillingGrid.CurrentPageIndex
            Me.moVSCBillingGrid.DataSource = Me.State.searchDV
            HighLightSortColumn(moVSCBillingGrid, Me.State.SortExpression)
            Me.moVSCBillingGrid.DataBind()
            ControlMgr.SetVisibleControl(Me, moVSCBillingGrid, Me.State.IsGridVisible)
            ControlMgr.SetVisibleControl(Me, trPageSize, Me.moVSCBillingGrid.Visible)

            If Me.moVSCBillingGrid.Visible Then
                Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If


        End If


    End Sub
    Private Sub OnFromDrop_Changed(ByVal fromMultipleDrop As Assurant.ElitaPlus.ElitaPlusWebApp.Common.MultipleColumnDDLabelControl) _
    Handles companyMultipleDropControl.SelectedDropChanged
        Try
            Me.State.SearchedComanyID = TheCompanyControl.SelectedGuid
            Dim index As Integer
            Dim alCompanies As New ArrayList
            alCompanies.Add(Me.State.SearchedComanyID)
            PopulateDealer(alCompanies)

            Dim objCompany As Company = New Company(Me.State.SearchedComanyID)
            If LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, objCompany.BillingByDealerId) = Codes.YESNO_N Then
                Me.State.SelectedComanyBillingFileByDealer = False
                TheDealerControl.ChangeEnabledControlProperty(False)
            Else
                Me.State.SelectedComanyBillingFileByDealer = True
                TheDealerControl.ChangeEnabledControlProperty(True)
            End If

        Catch ex As Exception
            HandleErrors(ex, Me.ErrController)
        End Try
    End Sub

    Public Function formatDateTime(ByVal objDate As Object) As String
        Dim strRet As String = objDate.ToString
        Try
            Dim dtVal As DateTime = CType(objDate, DateTime)
            strRet = dtVal.ToString("dd-MMM-yyyy") & " " & dtVal.ToLongTimeString()
        Catch ex As Exception
        End Try
        Return strRet
    End Function

    Private Sub GetBillingData()

        Dim DealerID As Guid, strInvNum As String, dtStart As Date, dtEnd As Date
        Dim strBeginDtTemp As String = String.Empty
        Dim strEndDtTemp As String = String.Empty

        'clear stored search criteria
        Me.State.SearchedComanyID = Guid.Empty
        Me.State.SearchedDealerID = Guid.Empty
        Me.State.SearchedBeginDate = Date.MinValue
        Me.State.SearchedEndDate = Date.MinValue

        If Me.State.BillingFileByDealerForAllUserCompanies Or Me.State.MixedBillingFileByDealerForUserCompanies Then
            Me.State.SearchedComanyID = TheCompanyControl.SelectedGuid
        End If

        DealerID = TheDealerControl.SelectedGuid

        strBeginDtTemp = Me.txtBeginDate.Text.Trim()
        If strBeginDtTemp <> "" Then
            If Not Date.TryParse(strBeginDtTemp, dtStart) Then
                Me.SetLabelError(Me.moBeginDateLabel)
                Me.State.errLabel = Me.moBeginDateLabel.UniqueID
                Throw New GUIException(Message.MSG_INVALID_DATE, Message.MSG_INVALID_DATE)
            End If
        Else
            dtStart = Date.MinValue
        End If

        strEndDtTemp = Me.txtEndDate.Text.Trim()
        If strEndDtTemp <> "" Then
            If Not Date.TryParse(strEndDtTemp, dtEnd) Then
                Me.SetLabelError(Me.moEndDateLabel)
                Me.State.errLabel = Me.moEndDateLabel.UniqueID
                Throw New GUIException(Message.MSG_INVALID_DATE, Message.MSG_INVALID_DATE)
            End If
        Else
            dtEnd = Date.MinValue
        End If
        If strBeginDtTemp = "" And strEndDtTemp <> "" Then
            Me.SetLabelError(Me.moBeginDateLabel)
            Me.State.errLabel = Me.moBeginDateLabel.UniqueID
            Throw New GUIException(Message.MSG_INVALID_DATE, Message.MSG_INVALID_DATE)
        End If

        If strBeginDtTemp <> "" And strEndDtTemp = "" Then
            Me.SetLabelError(Me.moEndDateLabel)
            Me.State.errLabel = Me.moEndDateLabel.UniqueID
            Throw New GUIException(Message.MSG_INVALID_DATE, Message.MSG_INVALID_DATE)
        End If

        If dtEnd < dtStart Then
            Me.SetLabelError(moEndDateLabel)
            Me.SetLabelError(moBeginDateLabel)
            Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_BEGIN_END_DATE_ERR)
        End If

        Me.State.SearchedDealerID = DealerID
        Me.State.SearchedBeginDate = dtStart
        Me.State.SearchedEndDate = dtEnd

        If Me.State.BillingFileByDealerForAllUserCompanies Then
            Me.State.searchDV = BillingHeader.getList(Me.State.alCompanies, DealerID, dtStart, dtEnd)
        ElseIf Me.State.NoBillingFileByDealerForAllUserCompanies Then
            Me.State.searchDV = BillingHeader.getListByCompany(Me.State.alCompanies, dtStart, dtEnd)
        Else ' Mix
            Dim alCompanies As New ArrayList
            alCompanies.Add(Me.State.SearchedComanyID)
            If Me.State.SelectedComanyBillingFileByDealer Then
                Me.State.searchDV = BillingHeader.getList(alCompanies, DealerID, dtStart, dtEnd)
            Else
                Me.State.searchDV = BillingHeader.getListByCompany(alCompanies, dtStart, dtEnd)
            End If
        End If


    End Sub
#End Region

End Class